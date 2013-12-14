using System;
using System.Collections.Generic;
using System.Text;
using SAMURAI.Data.Connection;
using UserPermission.Utils;

namespace UserPermission.Bll
{
    public class CommonBusiness
    {

        #region public static int GetSeqID(string seqname)
        /// <summary>
        /// 获取SEQ
        /// </summary>
        /// <param name="seqname">表名</param>
        /// <returns></returns>
        public static int GetSeqID(string seqname)
        {
            string strSql = @"select " + seqname + ".nextval from dual";
            object obj = StaticConnectionProvider.ExecuteScalar(strSql);
            return ValidatorHelper.ToInt(obj, 0);
        }

        ///// <summary>
        ///// 得到去除公司编码前缀后的账号名称
        ///// </summary>
        ///// <param name="strAccountName"></param>
        ///// <returns></returns>
        //public static string GetAccountName(string strAccountName)
        //{
        //    strAccountName = CommonMethod.FinalString(strAccountName);
        //    return strAccountName.Substring(strAccountName.LastIndexOf('-') + 1);
        //}

        ///// <summary>
        ///// 根据账号名称得到 公司编码前缀 
        ///// </summary>
        ///// <param name="strAccountName"></param>
        ///// <returns></returns>
        //public static string GetAccountCode(string strAccountName)
        //{
        //    string accountname = GetAccountName(strAccountName);
        //    return strAccountName.Replace("-" + accountname, "");
        //}


        #endregion
    }
}
