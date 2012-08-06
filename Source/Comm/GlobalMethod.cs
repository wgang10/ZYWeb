using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ZYSoft.Comm
{
    public class GlobalMethod
    {
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <returns></returns>
        public static string GenerateVerifictionCode(System.Web.Configuration.FormsAuthPasswordFormat passwordFormat)
        {
            string guid = System.Guid.NewGuid().ToString();
            if(passwordFormat == System.Web.Configuration.FormsAuthPasswordFormat.MD5)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(guid, "MD5");
            }
            else if (passwordFormat == System.Web.Configuration.FormsAuthPasswordFormat.SHA1)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(guid, "SHA1");
            }
            else
            {
                return guid;
            }
        }

        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <returns></returns>
        public static string GenerateVerifictionCode()
        {
            return GenerateVerifictionCode(System.Web.Configuration.FormsAuthPasswordFormat.SHA1);
        }

        /// <summary>
        /// 产生随机密码
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassWord()
        {
            return string.Empty;
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPWD(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        /// <summary>
        /// 解密密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string DecryptPWD(string password)
        {
            return string.Empty;
        }
    }
}
