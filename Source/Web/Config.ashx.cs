using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Web
{
  /// <summary>
  /// Config 的摘要说明
  /// </summary>
  public class Config : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";
      string type=context.Request.QueryString["Type"];
      string name=context.Request.QueryString["Name"];
      string filePath = context.Server.MapPath("Config.xml");
      string value = string.Empty;
      XmlDocument doc = new XmlDocument();
      doc.Load(filePath);
      if (!string.IsNullOrEmpty(type))
      {
        XmlElement nodeList = doc.ChildNodes (type);
        for (int i = 0; i < nodeList.ChildNodes.Count; i++)
        {
          if (nodeList.ChildNodes[i].Name.Equals(name))
          {
            value = nodeList.ChildNodes[i].Value;
            break;
          }
        }
      }
      context.Response.Write(value);
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