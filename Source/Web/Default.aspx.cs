using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZYSoft.BLL;
using ZYSoft.Comm.Entity;
using System.Configuration;
using System.Net;
using QzoneSDK.OAuth.Common;
using QzoneSDK.OAuth.Tokens;
using QzoneSDK.OAuth.Signature;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly UserBLL bll;
        private readonly ExamPaper model;
        private Member modelMember;

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
                if (Session["MemberInfo"] == null )
                {
                    GetUserInfo();
                }
                else
                {
                    divLogin.Visible = false;
                    divRegiste.Visible = false;
                    divLogined.Visible = true;
                    divUserInfo.Visible = true;
                    SetMemberInfo();
                }
            }
            //GetRequestToken();
            //GetUserInfo();
        }

        /// <summary>
        /// 
        /// </summary>
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
            //Response.Redirect(authenticationUrl);
        }

        /// <summary>
        /// 
        /// </summary>
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
                string Url = string.Format("https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&state={3}&redirect_uri={4}"
                    , apppid, appkey, code, state, callbackUrl);
                
                
                //Response.Redirect(Url);


                WebRequest request = WebRequest.Create(Url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                lbMessage2.Text = responseFromServer;
                //发送后会得到如下信息
                //access_token=AEE7091E761C2A571991234AD280E6BA&expires_in=7776000

                string access_token = responseFromServer.Substring(responseFromServer.IndexOf("=")+1);
                access_token = access_token.Substring(0, access_token.IndexOf("&"));
                Url = string.Format("https://graph.qq.com/oauth2.0/me?access_token={0}",access_token);
                //请求https://graph.qq.com/oauth2.0/me?access_token=AEE7091E761C2A571991234AD280E6BA
                request = WebRequest.Create(Url);
                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                lbMessage3.Text = responseFromServer;
                //得到如下信息
                //callback( {"client_id":"100289171","openid":"1AC83BAA19BB2E892033E0C07C27AC24"} ); 
                string openid = responseFromServer.Replace(@"\","").Substring(responseFromServer.IndexOf("openid") + 9);
                openid = openid.Substring(0, openid.IndexOf("}") - 1);
                lbMessage4.Text = "openid=" + openid;
                

                //以调用get_user_info接口为例：
                //发送请求到get_user_info的URL（请将access_token，appid等参数值替换为你自己的）：
                Url = string.Format("https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}", access_token, apppid, openid);
                request = WebRequest.Create(Url);
                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                lbMessage5.Text = responseFromServer;
                string[] UserInfo = responseFromServer.Split(',');
                lbMessage6.Text = "昵称：" + UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                Image1.ImageUrl = UserInfo[3].Substring(UserInfo[3].IndexOf("http"), UserInfo[3].Length - UserInfo[3].IndexOf("http") - 1);
                Image2.ImageUrl = UserInfo[4].Substring(UserInfo[4].IndexOf("http"), UserInfo[4].Length - UserInfo[4].IndexOf("http") - 1);
                Image3.ImageUrl = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1);

                //（2）成功返回后，即可获取到用户数据：
                /*
                {
                       "ret":0,
                       "msg":"",
                       "nickname":"YOUR_NICK_NAME",
                       ...
                }
                */

                divLogin.Visible = false;
                divRegiste.Visible = false;
                divLogined.Visible = true;
                divUserInfo.Visible = true;

                //用户登录
                string msg = string.Empty;
                if (bll.LoginMember(openid, ref msg))
                {
                    IList<Member> members = bll.GetMemberByOpenID(openid);
                    if (members.Count > 0)
                    {
                        modelMember = new Member();
                        modelMember.Nickname = UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                        modelMember.LoginTimes = members[0].LoginTimes;
                        modelMember.LastLoginDateTime = members[0].LastLoginDateTime;
                        modelMember.Integral = members[0].Integral;
                        modelMember.PhotoURL = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1); 
                        Session["MemberInfo"] = modelMember;


                        lbNickname.Text = modelMember.Nickname;
                        lbMemberNickname.Text = modelMember.Nickname;
                        lbLoginTimes.Text = modelMember.LoginTimes.ToString();

                        if (modelMember.LoginTimes<2)
                        {
                            lbLastLoginDateTime.Text = "";
                        }
                        else
                        {
                            lbLastLoginDateTime.Text = "上次登陆时间:"+modelMember.LastLoginDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        }
                        lbIntegral.Text = modelMember.Integral.ToString();
                        imgPhoto.ImageUrl =modelMember.PhotoURL;
                    }
                    
                }
                else
                {
                    lbMessageMember.Text = msg;
                }
                reader.Close();
                dataStream.Close();
                response.Close();

            }

            /*
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
            */
        }

        private void SetMemberInfo()
        {
            modelMember = (Member)Session["MemberInfo"];
            lbNickname.Text = modelMember.Nickname;
            lbMemberNickname.Text = modelMember.Nickname;
            lbLoginTimes.Text = modelMember.LoginTimes.ToString();
            if (modelMember.LoginTimes<2)
            {
                lbLastLoginDateTime.Text = "";
            }
            else
            {
                lbLastLoginDateTime.Text = "上次登陆时间:" + modelMember.LastLoginDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            }
            lbIntegral.Text = modelMember.Integral.ToString();
            imgPhoto.ImageUrl = modelMember.PhotoURL;
            Response.Redirect("default.aspx");
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

        private bool LoginMember(string OpenID,ref string Msg)
        {

            return true;
        }

        protected void btnLoginOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("default.aspx");
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string msg=string.Empty;
            if (bll.LoginMember(txtLoginID.Text, txtLoginPWD.Text, ref msg))
            {
                modelMember = new Member();
                Session["MemberInfo"] = modelMember;
            }
            else
            {
                lbMessage.Text = msg;
            }
        }
    }
}