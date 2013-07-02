using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Web
{
    /// <summary>
    /// GetFileMD5 的摘要说明
    /// </summary>
    public class GetFileMD5 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string md5Code = string.Empty;
            try
            {
                string fileName = context.Request.QueryString["FileName"];
                string filePath = context.Server.MapPath("App/Update/"+fileName);
                FileStream file = new FileStream(filePath, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                md5Code = sb.ToString();
            }
            catch { }
            context.Response.Write(md5Code);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}