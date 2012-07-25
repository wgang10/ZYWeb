using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace QQOauthWeb.Account
{
    public partial class LoginToQQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRequestToken();
        }

        private void GetRequestToken()
        {
            string key = ConfigurationManager.AppSettings["ConsumerKey"];
            string secret = ConfigurationManager.AppSettings["ConsumerSecret"];

            var context = new QzoneSDK.Context.QzoneContext(key, secret);
            //Get a Request Token
            var callbackUrl = ConfigurationManager.AppSettings["callbackUrl"]; //"/qzone/account/QQCallback.aspx";
            var requestToken = context.GetRequestToken(callbackUrl);
            //request token, request token secret 需要保存起来
            //在demo演示中，直接保存在全局变量中.真实情况需要网站自己处理
            Session["requesttokenkey"] = requestToken.TokenKey;
            Session["requesttokensecret"] = requestToken.TokenSecret;

            //Get the Qzone authentication page for the user to go to in order to authorize the Request Token.
            var authenticationUrl = context.GetAuthorizationUrl(requestToken, callbackUrl);
            Response.Redirect(authenticationUrl);
            
        }
    }
}