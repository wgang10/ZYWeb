using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using QQOauthWeb.Code.Model;
using QQOauthWeb.Code;

namespace QQOauthWeb.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }
       
        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                User member = new User();
                member.Login = this.UserName.Text;
                member.Password = this.Password.Text.Hash();
                member.ID = Guid.NewGuid();
                Database.Instance.InsertUser(member);

                var newCookie = new HttpCookie("MemberID", member.ID.ToString());
                newCookie.Expires = DateTime.Now.AddDays(10);
                Response.AppendCookie(newCookie);
                Response.Redirect("/default.aspx");
            }
            catch(Exception ex)
            {

            }
        }

    }
}
