using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Web
{
    /// <summary>
    /// FileList 的摘要说明
    /// </summary>
    public class FileList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string fileList = string.Empty;
            try
            {
                string path = string.Empty;
                string parameter = context.Request.QueryString["InstallOrUpdate"];
                if (parameter.Equals("Install"))
                {
                    path = context.Server.MapPath("App/Install");
                }
                else if (parameter.Equals("Update"))
                {
                    path = context.Server.MapPath("App/Update");
                }
                DirectoryInfo dr = new DirectoryInfo(path);
                FileInfo[] files = dr.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    fileList += "|" + files[i].Name;
                }
                if (fileList.Length > 0)
                {
                    fileList = fileList.Substring(1);
                }
            }
            catch { }
            context.Response.Write(fileList);
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