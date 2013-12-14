using System;
using System.Collections.Generic;
using System.Text;
using UserPermission.Model;
using UserPermission.Utils;
using SAMURAI.Data.Connection;
using System.Data;
using SAMURAI.Data.Parameter;


namespace UserPermission.Bll
{
    public class PlatFormBusiness
    {

        public static List<CompanyJsonModel> GetCompanyList(int nCtype, string strCompanyName)
        {
            List<CompanyJsonModel> lstcjModel = new List<CompanyJsonModel>();
            CompanyJsonModel cjModel = null;
            //string strSql = "SELECT COMPANYID,COMPANYNAME FROM USER_APP_COMPANY WHERE COMPANYNAME LIKE :COMPANYNAME ";
            //if (nCtype == int.Parse(ShareEnum.CompanyType.YgCompany.ToString("d")))
            //{
            string strSql = "SELECT COMPANYID,GROUPIDN,COMPNAME AS COMPANYNAME  FROM USER_WEB_YGCOMPANY WHERE COMPNAME LIKE :COMPANYNAME AND DELETED=0 ";
            //}
            ParamList param = new ParamList();
            param["COMPANYNAME"] = "%" + strCompanyName + "%";
            DataTable dtCompany = StaticConnectionProvider.ExecuteDataTable(strSql, param, GlobalConsts.DB_46PLAT);
            if (dtCompany != null)
            {
                foreach (DataRow dr in dtCompany.Rows)
                {
                    cjModel = new CompanyJsonModel();
                    cjModel.CompanyId = ValidatorHelper.ToInt(dr["COMPANYID"], 0);
                    cjModel.CompanyName = CommonMethod.FinalString(dr["COMPANYNAME"]);
                    cjModel.GroupIdn = CommonMethod.FinalString(dr["GROUPIDN"]);
                    lstcjModel.Add(cjModel);
                }
            }
            return lstcjModel;
        }

        /// <summary>
        ///  根据公司名称获取ID
        /// </summary>
        /// <param name="strCname"></param>
        /// <returns></returns>
        public static int GetYgCompanyId(string strCname)
        {
            string strSql = "SELECT COMPANYID  FROM USER_WEB_YGCOMPANY WHERE COMPNAME ='" + strCname + "' AND DELETED=0 ";
            DataTable dtCompany = StaticConnectionProvider.ExecuteDataTable(strSql, GlobalConsts.DB_46PLAT);
            if (dtCompany != null && dtCompany.Rows.Count == 1)
            {
                return ValidatorHelper.ToInt(dtCompany.Rows[0][0], 0);
            }
            return 0;
        }

        public static List<GroupJsonModel> GetGroupList(string strCompanyName)
        {
            List<GroupJsonModel> lstgjModel = new List<GroupJsonModel>();
            GroupJsonModel gjModel = null;

            string strSql = "SELECT GROUP_ID,GROUPIDN,GROUPNAME FROM USER_GROUP_INFO WHERE GROUPNAME LIKE :COMPANYNAME ";
            ParamList param = new ParamList();
            param["COMPANYNAME"] = "%" + strCompanyName + "%";
            DataTable dtCompany = StaticConnectionProvider.ExecuteDataTable(strSql, param, GlobalConsts.DB_46PLAT);
            if (dtCompany != null)
            {
                foreach (DataRow dr in dtCompany.Rows)
                {
                    gjModel = new GroupJsonModel();
                    gjModel.GroupId = CommonMethod.FinalString(dr["GROUP_ID"]);
                    gjModel.GroupName = CommonMethod.FinalString(dr["GROUPNAME"]);
                    gjModel.GroupIdn = CommonMethod.FinalString(dr["GROUPIDN"]);
                    lstgjModel.Add(gjModel);
                }
            }
            return lstgjModel;

        }

        /// <summary>
        /// 根据GroupId获取公司车辆信息
        /// </summary>
        /// <param name="strGroupId"></param>
        /// <returns></returns>
        public static DataTable GetVechiles(string strGroupId)
        {
            string strSql = "SELECT MAC_ID,TARGET_ID FROM USER_TARGET_INFO WHERE GROUP_ID='" + strGroupId + "'";
            return StaticConnectionProvider.ExecuteDataTable(strSql, GlobalConsts.DB_46PLAT);
        }

    }
}
