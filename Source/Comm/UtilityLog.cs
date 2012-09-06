using System;

namespace ZYSoft.Comm
{
    public sealed class UtilityLog
    {
        private static string strClsName = "ApplicationInfoLog";
        private static log4net.ILog logger = log4net.LogManager.GetLogger(strClsName);

        #region  Method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetCustomerIP()
        {
            //单机版程序采用取得本计算机名和登录用户名的方式
            //return Environment.UserDomainName + "@" + Environment.UserName;

            //服务器端程序采用取得访问者IP的方式
            string strUserIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strUserIP == null || strUserIP == "")
            {
                strUserIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strUserIP;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebugMsg"></param>
        public static void WriteDebug(string DebugMsg)
        {
            string strCustomerIP = GetCustomerIP();
            logger.Debug(" [" + strCustomerIP + "] " + DebugMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebugMsg"></param>
        public static void WriteInfo(string InfoMsg)
        {
            string strCustomerIP = GetCustomerIP();
            logger.Info(" [" + strCustomerIP + "] " + InfoMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebugMsg"></param>
        public static void WriteWarn(string WarnMsg)
        {
            string strCustomerIP = GetCustomerIP();
            logger.Warn(" [" + strCustomerIP + "] " + WarnMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebugMsg"></param>
        public static void WriteError(string ErrorMsg)
        {
            string strCustomerIP = GetCustomerIP();
            logger.Error(" [" + strCustomerIP + "] " + ErrorMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DebugMsg"></param>
        public static void WriteFatal(string FatalMsg)
        {
            string strCustomerIP = GetCustomerIP();
            logger.Fatal(" [" + strCustomerIP + "] " + FatalMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strErrMsg"></param>
        public static void WriteErrLogWithLog4Net(string strErrMsg)
        {
            string strCustomerIP = GetCustomerIP();
            logger.Error("[9] [" + strCustomerIP + "] " + strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrLogWithLog4Net(Exception ex)
        {
            WriteErrLogWithLog4Net(ex.Message + ex.StackTrace);
        }

        #endregion
    }
}
