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
            if (!IsPostBack)
            {   
                BindGridData();
            }
            //GetRequestToken();
            GetUserInfo();
        }

        private void GetRequestToken()
        {
            string key = ConfigurationManager.AppSettings["ConsumerKey"];
            string secret = ConfigurationManager.AppSettings["ConsumerSecret"];

            var context = new QzoneSDK.Context.QzoneContext(key, secret);
            //Get a Request Token
            var callbackUrl = ConfigurationManager.AppSettings["callbackUrl"];
            var requestToken = context.GetRequestToken(callbackUrl);
            //request token, request token secret 需要保存起来
            //在demo演示中，直接保存在全局变量中.真实情况需要网站自己处理
            Session["requesttokenkey"] = requestToken.TokenKey;
            Session["requesttokensecret"] = requestToken.TokenSecret;

            //Get the Qzone authentication page for the user to go to in order to authorize the Request Token.
            var authenticationUrl = context.GetAuthorizationUrl(requestToken, callbackUrl);
            Response.Redirect(authenticationUrl);

        }

        private void GetUserInfo()
        {
            //获取Authorization Code
            if (Request.QueryString["code"] != null)
            {
                string apppid = ConfigurationManager.AppSettings["ConsumerKey"];
                string appkey = ConfigurationManager.AppSettings["ConsumerSecret"];
                string code = Request.QueryString["code"].ToString();
                string callbackUrl = ConfigurationManager.AppSettings["callbackUrl"];
                string state = ConfigurationManager.AppSettings["state"];
                string Url = string.Format("graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&state={3}&redirect_uri={4}"
                    , apppid, appkey, code, state, callbackUrl);
                Response.Redirect(Url);
                //发送后会得到如下信息
                //access_token=AEE7091E761C2A571991234AD280E6BA&expires_in=7776000

                //请求https://graph.qq.com/oauth2.0/me?access_token=AEE7091E761C2A571991234AD280E6BA
                //得到如下信息
                //callback( {"client_id":"100289171","openid":"1AC83BAA19BB2E892033E0C07C27AC24"} ); 




                /*
                 Step4：使用Access Token来获取用户的OpenID

                    1. 发送请求到如下地址（请将access_token等参数值替换为你自己的）：
                    https://graph.qq.com/oauth2.0/me?access_token=YOUR_ACCESS_TOKEN

                    2. 获取到用户OpenID，返回包如下：
                    callback( {"client_id":"YOUR_APPID","openid":"YOUR_OPENID"} ); 
                    Step5：使用Access Token以及OpenID来访问和修改用户数据

                    1. 建议网站在用户登录后，即调用get_user_info接口，获得该用户的头像、昵称并显示在网站上，使用户体验统一。
                    2. 调用其他OpenAPI，以访问和修改用户数据。所有OpenAPI详见【QQ登录】API文档。

                    以调用get_user_info接口为例：
                    （1）发送请求到get_user_info的URL（请将access_token，appid等参数值替换为你自己的）：
                    https://graph.qq.com/user/get_user_info?access_token=YOUR_ACCESS_TOKEN&oauth_consumer_key=YOUR_APP_ID&openid=YOUR_OPENID


                    （2）成功返回后，即可获取到用户数据：
                    {
                       "ret":0,
                       "msg":"",
                       "nickname":"YOUR_NICK_NAME",
                       ...
                    }

                 */

            }
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
                this.lbMessage.Text = String.Format("{0} 获取时间 {1:yyyy-MM-dd HH:mm:ss}", GetData(), DateTime.Now);
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
            model.Name = this.TextBox1.Text.Trim();
            model.CreatDateTime = DateTime.Now;
            model.UpdateDateTime = DateTime.Now;
            model.Status = "1";
            if (bll.SaveOrUpdateExamPaper(model) == -1)
            {
                lbMessage.Text = "添加失败";
                lbMessage.DataBind();
            }
            else
            {
                BindGridData();
                TextBox1.Text = "";
            }
        }
    }
}