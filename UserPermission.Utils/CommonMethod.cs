
using System.Configuration;
using System;

namespace UserPermission.Utils
{
    public class CommonMethod
    {
        #region 字符串处理
        /// <summary>
        /// 字符串简单处理
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns></returns>
        public static string FinalString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            str = str.Trim();
            return str;
        }

        public static string FinalString(object str)
        {
            string strresult = string.Empty;
            if (str == DBNull.Value)
            {
               return string.Empty;
            }
            if (str != null)
            {
                strresult = str.ToString();
                return FinalString(strresult);
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region 获取Config配置信息

        public static string GetConfigValue(string configkey)
        {
            return FinalString(ConfigurationSettings.AppSettings[configkey]);
        }

        #endregion

        #region SQL 注释

        public static string SetSqlComment(string strFor, string strFile, string strFun, string strAuthor)
        {
            return string.Format(" /*Flat:危险品气站管理项目/Author:{0} /For:{1}/{2}/Fun:{3};*/ ", strAuthor, strFor, strFile, strFun);
        }

        #endregion


    }
}
