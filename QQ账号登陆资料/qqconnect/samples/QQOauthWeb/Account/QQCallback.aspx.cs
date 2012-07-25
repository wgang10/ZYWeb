using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using QzoneSDK.Models;
using Jayrock.Json.Conversion;
using QQOauthWeb.Code;
using QQOauthWeb.Code.Model;
 
//using QQOauthWeb.Code.Model;
 
//using QQOauthWeb.Code;

namespace QQOauthWeb.Account
{
    public partial class QQCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["oauth_vericode"] != null)
            {
                var requestTokenKey = Session["requesttokenkey"].ToString();
                var requestTokenSecret = Session["requesttokensecret"].ToString();
                var verifier = Request.QueryString["oauth_vericode"];
                string key = ConfigurationManager.AppSettings["ConsumerKey"];
                string secret = ConfigurationManager.AppSettings["ConsumerSecret"];
                QzoneSDK.Qzone qzone = new QzoneSDK.Qzone(key, secret, requestTokenKey, requestTokenSecret, verifier);

                //这里需要将qzone.OAuthTokenKey, qzone.OAuthTokenSecret, qzone.OpenID 存储起来用于后面的API的访问
                QzoneSDK.Qzone qzone2 = new QzoneSDK.Qzone(key, secret, qzone.OAuthTokenKey, qzone.OAuthTokenSecret, string.Empty, true, qzone.OpenID);
                Session["qzonesdk"] = qzone2;
                
                qzone2 = Session["qzonesdk"] as QzoneSDK.Qzone;
                var currentUser = qzone2.GetCurrentUser();

                var user = (BasicProfile)JsonConvert.Import(typeof(BasicProfile), currentUser);
                if (null != user)
                {
                    this.result.Text = "成功登陆";
                    this.Nickname.Text = user.Nickname;
                    this.Figureurl.Text = user.Figureurl;
                    var list = Database.Instance.QzoneOauth.Where(x => x.OpenId == qzone2.OpenID).ToList();
                    if (list.Count > 0)
                    {
                        QzoneOauth model = list[0];
                        var newCookie = new HttpCookie("MemberID", model.UserId.ToString());
                        newCookie.Expires = DateTime.Now.AddDays(10);
                        Response.AppendCookie(newCookie);
                        Session["QzoneOauth"] = model;
                    }
                    else
                    {
                        User member = new User();
                        member.Login = user.Nickname;
                        member.Password = "test".Hash();
                        member.ID = Guid.NewGuid();
                        Database.Instance.InsertUser(member);
                        QzoneOauth oauth = new QzoneOauth()
                        {
                            AccessTokenKey = qzone2.OAuthTokenKey,
                            AccessTokenSecret = qzone2.OAuthTokenSecret,
                            OpenId = qzone2.OpenID,
                            ID = Guid.NewGuid(),
                            UserId = member.ID,
                        };
                        Database.Instance.InsertQzoneOauth(oauth);
                        var newCookie = new HttpCookie("MemberID", member.ID.ToString());
                        newCookie.Expires = DateTime.Now.AddDays(10);
                        Response.AppendCookie(newCookie);
                        Session["QzoneOauth"] = oauth;
                    }
                }

        
            }
        }
    }
}