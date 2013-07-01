using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections;

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
      string value = string.Empty;
      try
      {
          string type = context.Request.QueryString["SystemName"];
          string name = context.Request.QueryString["ConfigName"];
          string filePath = context.Server.MapPath("Config.xml");
          XmlDocument doc = new XmlDocument();
          doc.Load(filePath);
          XmlNode root = doc.DocumentElement;
          IEnumerator ienum = root.GetEnumerator();
          XmlNode node;
          bool isFound = false;
          while (ienum.MoveNext())
          {
              node = (XmlNode)ienum.Current;
              if (node.Attributes["Name"].Value.Equals(type))
              {
                  for (int i = 0; i < node.ChildNodes.Count; i++)
                  {
                      if (node.ChildNodes[i].Name.Equals(name))
                      {
                          value = node.ChildNodes[i].InnerXml;
                          isFound = true;
                          break;
                      }
                  }
                  if (isFound)
                  {
                      break;
                  }
              }
          }
      }
      catch { }
      finally { }
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