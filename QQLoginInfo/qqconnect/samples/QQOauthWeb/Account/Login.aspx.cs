using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QQOauthWeb.Code;
using QQOauthWeb.Code.Model;

namespace QQOauthWeb.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            User member = (from m in Database.Instance.Users
                             where m.Login.Equals(this.UserName.Text, StringComparison.CurrentCultureIgnoreCase) &&
                             m.Password.Equals(this.Password.Text.Hash())
                             select m).Single();

            var newCookie = new HttpCookie("MemberID", member.ID.ToString());
            newCookie.Expires = DateTime.Now.AddDays(10);
            Response.AppendCookie(newCookie);
            Response.Redirect("/default.aspx");
        }
    }
}
