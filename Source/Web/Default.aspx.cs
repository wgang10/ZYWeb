using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZYSoft.BLL;
using ZYSoft.Comm.Entity;
using System.Configuration;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly UserBLL bll;
        private readonly ExamPaper model;

        public Default()
        {
            bll = new UserBLL();
            model = new ExamPaper();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    this.lbMessage.Text = GetData()+ " 获取时间 "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    BindGridData();
            //}
            GetUserInfo();
        }

        private void GetUserInfo()
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

                //qzone2 = Session["qzonesdk"] as QzoneSDK.Qzone;
                var currentUser = qzone2.GetCurrentUser();
                this.lbMessage.Text = currentUser;
                //var user = (BasicProfile)JsonConvert.Import(typeof(BasicProfile), currentUser);
                //if (null != currentUser)
                //{
                //this.result.Text = "成功登陆";
                //this.Nickname.Text = user.Nickname;
                //this.Figureurl.Text = user.Figureurl;
                //this.lbMoreInfo.Text="User's Msg:"+user.Msg+"User's Ret:"+user.Ret.ToString();
                //Image1.ImageUrl=user.Figureurl;
                //var list = Database.Instance.QzoneOauth.Where(x => x.OpenId == qzone2.OpenID).ToList();
                //if (list.Count > 0)
                //{
                //    QzoneOauth model = list[0];
                //    var newCookie = new HttpCookie("MemberID", model.UserId.ToString());
                //    newCookie.Expires = DateTime.Now.AddDays(10);
                //    Response.AppendCookie(newCookie);
                //    Session["QzoneOauth"] = model;
                //}
                //else
                //{
                //    User member = new User();
                //    member.Login = user.Nickname;
                //    member.Password = "test".Hash();
                //    member.ID = Guid.NewGuid();
                //    Database.Instance.InsertUser(member);
                //    QzoneOauth oauth = new QzoneOauth()
                //    {
                //        AccessTokenKey = qzone2.OAuthTokenKey,
                //        AccessTokenSecret = qzone2.OAuthTokenSecret,
                //        OpenId = qzone2.OpenID,
                //        ID = Guid.NewGuid(),
                //        UserId = member.ID,
                //    };
                //    Database.Instance.InsertQzoneOauth(oauth);
                //    var newCookie = new HttpCookie("MemberID", member.ID.ToString());
                //    newCookie.Expires = DateTime.Now.AddDays(10);
                //    Response.AppendCookie(newCookie);
                //    Session["QzoneOauth"] = oauth;
                //}
                //}
            }
        }

        private string GetData()
        {
            return bll.GetUaerName();
        }

        private void BindGridData()
        {
            try
            {
                this.GridView1.DataSource = bll.GetExamPapersList();
            }
            catch (Exception ex)
            {
                this.GridView1.DataSource = null;
                lbMessage.Text = ex.Message;
                lbMessage.DataBind();
            }
            finally
            {
                this.GridView1.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //model.Name = this.TextBox1.Text.Trim();
            //model.CreatDateTime = DateTime.Now;
            //model.UpdateDateTime = DateTime.Now;
            //model.Status = "1";
            //if (bll.SaveOrUpdateExamPaper(model) == -1)
            //{
            //    lbMessage.Text = "添加失败";
            //    lbMessage.DataBind();
            //}
            //else
            //{
            //    BindGridData();
            //}
        }
    }
}