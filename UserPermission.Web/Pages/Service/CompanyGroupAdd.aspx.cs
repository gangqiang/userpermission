using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;

namespace UserPermission.Web.Pages.Service
{
    public partial class CompanyGroupAdd : BasePage
    {

        public int CompanyGroupId
        {
            get { return Request.QueryString["id"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["id"], UrlEncKey), 0); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 车辆绑定

                if (GroupId.Length > 0)
                {
                    DataTable dt = PlatFormBusiness.GetVechiles(GroupId);
                    ControlHelper.BindListControl(cblVehicles, dt, "TARGET_ID", "MAC_ID");
                }

                //修改时包含的车辆进行选中
                if (CompanyGroupId > 0)
                {
                    foreach (ListItem item in cblVehicles.Items)
                    {
                        if (CompanyGroupBusiness.IsGroupContainVehicel(CompanyGroupId, item.Value))
                        {
                            item.Selected = true;
                        }
                    }

                    USER_SHARE_GROUPMODEL group = CompanyGroupBusiness.GetGroupModel(CompanyGroupId);
                    if (group != null)
                    {
                        txtGroupName.Text = group.GROUPNAME;
                        txtGroupDesc.Text = CommonMethod.FinalString(group.GROUPDESC);
                    }
                }

                #endregion
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 服务端验证

            if (txtGroupName.Text.Trim().Length == 0)
            {
                Alert("请输入分组名称！");
                Select(txtGroupName);
                return;
            }

            List<USER_SHARE_VEHICLE_GROUPMODEL> lstVgModel = new List<USER_SHARE_VEHICLE_GROUPMODEL>();
            USER_SHARE_VEHICLE_GROUPMODEL vgmodel = null;
            foreach (ListItem item in cblVehicles.Items)
            {
                if (item.Selected)
                {
                    vgmodel = new USER_SHARE_VEHICLE_GROUPMODEL();
                    vgmodel.SHAREGROUPID = 0;
                    vgmodel.MACID = item.Value;
                    vgmodel.TARGETID = item.Text;
                    lstVgModel.Add(vgmodel);
                }
            }

            if (lstVgModel.Count == 0)
            {
                Alert("请选择分组包含车辆！");
                return;
            }

            #endregion

            #region 分组信息保存

            USER_SHARE_GROUPMODEL groupModel = null;

            //日志信息
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;

            if (CompanyGroupId > 0)
            {
                groupModel = CompanyGroupBusiness.GetGroupModel(CompanyGroupId);
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditCompanyGroup.ToString("d"));
                logModel.OPERATECONTENT = string.Format("修改分组信息，修改后分组名称：{0}， 分组Id:{1} ", txtGroupName.Text.Trim(), CompanyGroupId);
            }
            else
            {
                groupModel = new USER_SHARE_GROUPMODEL();
                groupModel.ID = CommonBusiness.GetSeqID("S_USER_SHARE_GROUP");
                groupModel.COMPANYCODE = CompanyCode;
                groupModel.PARENTID = Request.QueryString["pid"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["pid"], UrlEncKey), 0);
                groupModel.STATE = int.Parse(ShareEnum.CompanyGroupStatus.Normal.ToString("d"));
                groupModel.GRADE = CompanyGroupBusiness.GetGroupGrade(CompanyCode, groupModel.PARENTID);

                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddCompanyGroup.ToString("d"));
                logModel.OPERATECONTENT = string.Format("新增分组信息，分组名称：{0}，公司编码：{1} ", txtGroupName.Text.Trim(), CompanyCode);
            }


            groupModel.GROUPNAME = txtGroupName.Text.Trim();
            groupModel.VEHICLENUM = lstVgModel.Count;
            groupModel.GROUPDESC = txtGroupDesc.Text.Trim();

            bool blSuccess = false;
            string strRoleFuns = Request.Form["fun"];

            if (CompanyGroupId == 0)
            {
                blSuccess = CompanyGroupBusiness.AddCompanyGroup(groupModel, lstVgModel, logModel);

            }
            else
            {
                blSuccess = CompanyGroupBusiness.EditCompanyGroup(groupModel, lstVgModel, logModel);
            }

            Alert((CompanyGroupId > 0 ? "修改" : "新增") + "分组" + (blSuccess ? "成功" : "失败，请重试！"));

            //刷新父页面
            ExecStartScript(string.Format("parent.location='CompanyGroupManage.aspx?s={0}';", new Random(10000).Next()));

            #endregion
        }


    }
}