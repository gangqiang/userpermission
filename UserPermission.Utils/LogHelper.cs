
using Common.Logging;
using System;
namespace UserPermission.Utils
{
    public class LogHelper
    {
        private static readonly ILog LogErr = LogManager.GetLogger("logerror");
        private static readonly ILog LogInfo = LogManager.GetLogger("loginfo");

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="strLogContent"></param>
        /// <param name="ex"></param>
        public static void WriteErr(string strLogContent, Exception ex)
        {
            if (Utils.CommonMethod.FinalString(strLogContent).Length > 0)
            {
                LogErr.Error(strLogContent, ex);
            }
        }


        /// <summary>
        /// 记录文本信息
        /// </summary>
        /// <param name="strInfo"></param>
        public static void WriteInfo(string strInfo)
        {
            LogInfo.Info(strInfo);
        }
    }
}
