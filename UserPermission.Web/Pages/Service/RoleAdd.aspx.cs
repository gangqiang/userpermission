using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;
using System.Data;


namespace UserPermission.Web.Pages.Service
{
    public partial class RoleAdd : BasePage
    {
        #region 属性

        public int RoleId
        {
            get { return Request.QueryString["id"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["id"], UrlEncKey), 0); }
        }

        #endregion

        #region  初始

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //项目下拉框
                DataTable dt = CompanyBusiness.GetCompanyProjects(CompanyCode.ToString());
                ControlHelper.BindListControl(ddlProject, dt, "PROJECTNAME", "PROJECTID");
                if (dt != null && dt.Rows.Count > 0 && ProjectId == 0)
                {
                    ExecStartScript("$('#trProject').show();");
                }
                else
                {
                    ControlHelper.SelectFlg(ddlProject, ProjectId.ToString());
                    ExecStartScript("$('#trProject').hide();");
                }

                //车辆分组
                if (GroupId.Length > 0)
                {
                    LoadCompanyGroup();
                    ExecStartScript("$('#trGroups').show();");
                }

                #region 账号绑定

                string strWhere = string.Format(" AND ISADMIN=0 AND COMPANYID={0} ", CompanyCode);

                DataTable dtAccounts = AccountBusiness.GetAccountList(strWhere);
                ControlHelper.BindListControl(cblAccounts, dtAccounts, "ARNAME", "ROLEACCOUNTS");

                #endregion

                hidRoleId.Value = RoleId.ToString();
                if (RoleId > 0)
                {
                    USER_SHARE_ROLESMODEL roleModel = RoleBusiness.GetRoleModel(RoleId);
                    if (roleModel != null)
                    {
                        txtRoleName.Text = roleModel.ROLENAME;
                        txtRoleDesc.Text = CommonMethod.FinalString(roleModel.ROLEDESC);
                        ControlHelper.SelectFlg(ddlProject, roleModel.PROJECTID.ToString());

                        #region 判断账号的选中

                        foreach (ListItem accountitem in cblAccounts.Items)
                        {
                            if (accountitem.Value.IndexOf("," + roleModel.ROLEID + ",") >= 0)
                            {
                                accountitem.Selected = true;
                            }
                        }

                        #endregion

                        #region 判断分组的选中

                        foreach (TreeNode tn in tvGroups.Nodes)
                        {
                            tn.Checked = CompanyGroupBusiness.IsRoleContainGroup(RoleId, tn.Value);
                        }

                        #endregion
                    }
                    else
                    {
                        Response.Write("不存在的角色信息！");
                        Response.End();
                    }
                }


            }
        }


        private void LoadCompanyGroup()
        {
            DataTable dt = CompanyGroupBusiness.GetCompanyGroupList(" AND COMPANYCODE=" + CompanyCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                InitTree(dt, tvGroups.Nodes, "0");
            }
        }

        protected void InitTree(DataTable dt, TreeNodeCollection Nds, string parentId)//用递归方法动态生成节点
        {
            TreeNode tmpNode;
            DataRow[] drs = dt.Select("PARENTID=" + parentId, " ID ASC ");
            foreach (DataRow dr in drs)
            {
                tmpNode = new TreeNode(dr["GROUPNAME"].ToString(), dr["ID"].ToString());
                tmpNode.SelectAction = TreeNodeSelectAction.None;//禁用超链接属性
                tmpNode.ExpandAll();//展开所有子节点
                Nds.Add(tmpNode);
                this.InitTree(dt, tmpNode.ChildNodes, tmpNode.Value);
            }
        }


        #endregion

        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 服务端验证

            if (txtRoleName.Text.Trim().Length == 0)
            {
                Alert("请输入角色名称！");
                Select(txtRoleName);
                return;
            }

            if (CommonMethod.FinalString(Request.Form["fun"]).Length == 0)
            {
                Alert("请选择角色拥有的权限！");
                return;
            }

            #endregion

            #region 角色信息保存

            USER_SHARE_ROLESMODEL roleModel = null;

            //日志信息
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;

            if (RoleId > 0)
            {
                roleModel = RoleBusiness.GetRoleModel(RoleId);
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditRole.ToString("d"));
                logModel.OPERATECONTENT = string.Format("修改角色信息，修改后角色名称：{0}， 角色Id:{1} ", txtRoleName.Text.Trim(), RoleId);
            }
            else
            {
                roleModel = new USER_SHARE_ROLESMODEL();
                roleModel.ROLEID = CommonBusiness.GetSeqID("S_USER_SHARE_ROLES");
                roleModel.CreatorId = AccountId;
                roleModel.CreateDate = DateTime.Now;
                roleModel.COMPANYID = CompanyId;
                roleModel.STATUS = int.Parse(ShareEnum.RoleStatus.Normal.ToString("d"));
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddRole.ToString("d"));
                logModel.OPERATECONTENT = string.Format("新增角色信息，角色名称：{0}，公司编码：{1} ", txtRoleName.Text.Trim(), CompanyCode);
            }

            roleModel.PROJECTID = ValidatorHelper.ToInt(ddlProject.SelectedValue, 0);
            roleModel.ROLENAME = txtRoleName.Text.Trim();
            roleModel.ROLEDESC = txtRoleDesc.Text.Trim();

            bool blSuccess = false;
            string strRoleFuns = Request.Form["fun"];

            #region  账号信息

            List<RoleAccountModel> raModel = new List<RoleAccountModel>();
            RoleAccountModel model = null;
            foreach (ListItem item in cblAccounts.Items)
            {
                model = new RoleAccountModel();
                model.AccountId = ValidatorHelper.ToInt(item.Value.Split('$')[0], 0);
                model.IsChecked = item.Selected;
                raModel.Add(model);
            }

            #endregion

            #region 角色拥有车辆分组

            string strGroup = string.Empty;
            foreach (TreeNode tn in tvGroups.Nodes)
            {
                if (tn.Checked)
                {
                    strGroup += tn.Value + ",";
                }
            }

            strGroup = strGroup.TrimEnd(',');

            #endregion

            if (RoleId == 0)
            {
                blSuccess = RoleBusiness.AddARole(roleModel, strRoleFuns, strGroup, raModel, logModel);
            }
            else
            {
                blSuccess = RoleBusiness.EditRole(roleModel, strRoleFuns, strGroup, raModel, logModel);
            }

            Alert((RoleId > 0 ? "修改" : "新增") + "角色" + (blSuccess ? "成功" : "失败，请重试！"));

            ExecScript("parent.__doPostBack('ctl00$MainContent$btnSearch','');");


            #endregion
        }

        #endregion
    }
}