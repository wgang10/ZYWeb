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
using Newtonsoft.Json;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly UserBLL bll;
        private readonly ExamPaper model;
        //private Member modelMember;

        public Default()
        {
            bll = new UserBLL();
            model = new ExamPaper();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Text = "path:"+Server.MapPath("~/");
            if (!IsPostBack)
            { 
                if (Session["MemberInfo"] == null)
                {
                    GetUserInfo();
                }
                else
                {
                    Response.Redirect("MemberInfo.aspx");
                }
            }
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
            lbLoginMessage.Text = "";
            lbLoginMessage.Visible = false;
            //获取Authorization Code
            if (Request.QueryString["code"] != null)
            {
                #region QQ登录
                try
                {
                    //参考地址 http://wiki.connect.qq.com/%E4%BD%BF%E7%94%A8authorization_code%E8%8E%B7%E5%8F%96access_token
                    //Step1：获取Authorization Code
                    //如果用户成功登录并授权，则会跳转到指定的回调地址，并在redirect_uri地址后带上Authorization Code和原始的state值
                    string code = Request.QueryString["code"].ToString();
                    string apppid = ConfigurationManager.AppSettings["appid"];
                    string appkey = ConfigurationManager.AppSettings["appkey"];
                    string callbackUrl = ConfigurationManager.AppSettings["callbackUrl"];
                    string state = ConfigurationManager.AppSettings["state"];
                    string Url = string.Format("https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}"
                        , apppid, appkey, code, callbackUrl);


                    //Response.Redirect(Url);

                    //Step2：通过Authorization Code获取Access Token
                    WebRequest request = WebRequest.Create(Url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    //lbMessage2.Text = responseFromServer;
                    //如果成功返回，即可在返回包中获取到Access Token
                    //access_token=FE04************************CCE2&expires_in=7776000&refresh_token=88E4************************BE
                    /*
                     * access_token	授权令牌，Access_Token。
                     * expires_in	该access token的有效期，单位为秒。
                     * refresh_token	在授权自动续期步骤中，获取新的Access_Token时需要提供的参数。
                     */
                    //access_token=AEE7091E761C2A571991234AD280E6BA&expires_in=7776000

                    string access_token = responseFromServer.Substring(responseFromServer.IndexOf("=") + 1);
                    access_token = access_token.Substring(0, access_token.IndexOf("&"));
                    //Step3：使用Access Token来获取用户的OpenID
                    Url = string.Format("https://graph.qq.com/oauth2.0/me?access_token={0}", access_token);
                    request = WebRequest.Create(Url);
                    response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    //lbMessage3.Text = responseFromServer;
                    //获取到用户OpenID，返回包如下：
                    //callback( {"client_id":"YOUR_APPID","openid":"YOUR_OPENID"} );
                    //callback( {"client_id":"100289171","openid":"1AC83BAA19BB2E892033E0C07C27AC24"} ); 
                    responseFromServer = responseFromServer.Replace("callback(", "").Replace(" );", "");
                    string openid=string.Empty;
                    var opid = JsonConvert.DeserializeObject<ObjOpenID>(responseFromServer);
                    if (opid != null)
                        openid = opid.openid;
                    //openid = responseFromServer.Replace(@"\", "").Substring(responseFromServer.IndexOf("openid") + 9);
                    //openid = openid.Substring(0, openid.IndexOf("}") - 1);
                    //lbMessage4.Text = "openid=" + openid;

                    //Step4：使用Access Token以及OpenID来访问和修改用户数据
                    //以调用get_user_info接口为例：
                    //发送请求到get_user_info的URL（请将access_token，appid等参数值替换为你自己的）：
                    Url = string.Format("https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}", access_token, apppid, openid);
                    request = WebRequest.Create(Url);
                    response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    var Juser = JsonConvert.DeserializeObject<ObjUser>(responseFromServer);
                    if (Juser != null)
                    {
                        //lbMessage5.Text = responseFromServer;
                        //string[] UserInfo = responseFromServer.Split(',');
                        //lbMessage6.Text = "昵称：" + UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                        //Image1.ImageUrl = UserInfo[3].Substring(UserInfo[3].IndexOf("http"), UserInfo[3].Length - UserInfo[3].IndexOf("http") - 1);
                        //Image2.ImageUrl = UserInfo[4].Substring(UserInfo[4].IndexOf("http"), UserInfo[4].Length - UserInfo[4].IndexOf("http") - 1);
                        //Image3.ImageUrl = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1);

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
                        Member modelMember = new Member();
                        modelMember.OpenId = openid;
                        //modelMember.Nickname = UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                        modelMember.Nickname = Juser.nickname;
                        //modelMember.PhotoURL = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1);
                        modelMember.PhotoURL = Juser.figureurl;
                        if (bll.LoginMember(modelMember, ref msg))
                        {
                            IList<Member> members = bll.GetMemberByOpenID(openid);
                            if (members.Count > 0)
                            {
                                //modelMember.OpenId = openid;
                                //modelMember.Nickname = UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                                //modelMember.LoginTimes = members[0].LoginTimes;
                                //modelMember.LastLoginDateTime = members[0].LastLoginDateTime;
                                //modelMember.Integral = members[0].Integral;
                                //modelMember.PhotoURL = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1);
                                Session["MemberInfo"] = members[0];
                                ZYSoft.Comm.UtilityLog.WriteInfo(string.Format("QQ账号 {0} 登录。{1}", members[0].Nickname, members[0].OpenId));
                                Response.Redirect("MemberInfo.aspx", true);

                                //lbNickname.Text = modelMember.Nickname;
                                //lbMemberNickname.Text = modelMember.Nickname;
                                //lbLoginTimes.Text = modelMember.LoginTimes.ToString();

                                //if (modelMember.LoginTimes<2)
                                //{
                                //    lbLastLoginDateTime.Text = "";
                                //}
                                //else
                                //{
                                //    lbLastLoginDateTime.Text = "上次登陆时间:"+modelMember.LastLoginDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                                //}
                                //lbIntegral.Text = modelMember.Integral.ToString();
                                //imgPhoto.ImageUrl =modelMember.PhotoURL;
                            }

                        }
                        else
                        {
                            lbLoginMessage.Text = msg;
                            lbLoginMessage.Visible = true;
                        }
                    }
                    //Random rdm = new Random(100);
                    //Response.Redirect("Default.aspx?x=" + rdm.Next().ToString());
                }
                catch (Exception ex)
                {
                    ZYSoft.Comm.UtilityLog.WriteError(ex.Message);
                    lbLoginMessage.Text = ex.Message;
                    lbLoginMessage.Visible = true;
                }
                #endregion
            }

            #region 使用第三方组件

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

                qzone2 = Session["qzonesdk"] as QzoneSDK.Qzone;
                var currentUser = qzone2.GetCurrentUser();
                //this.lbMessage.Text = currentUser;
                var user = (BasicProfile)JsonConvert.Import(typeof(BasicProfile), currentUser);
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
            #endregion
        }

        private void SetMemberInfo()
        {
            Member modelMember = (Member)Session["MemberInfo"];
            lbNickname.Text = modelMember.Nickname;
            lbMemberNickname.Text = modelMember.Nickname;
            lbLoginTimes.Text = modelMember.LoginTimes.ToString();
            if (modelMember.LoginTimes<2)
            {
                lbLastLoginDateTime.Text = "";
            }
            else
            {
                lbLastLoginDateTime.Text = "上次登陆时间:" + modelMember.LastLoginDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            }
            lbIntegral.Text = modelMember.Integral.ToString();
            imgPhoto.ImageUrl = modelMember.PhotoURL;
        }

        private string GetData()
        {
            return bll.GetUaerName();
        }

        private void BindGridData()
        {

            //try
            //{
            //    this.lbMessage.Text = String.Format("{0} 获取时间 {1:yyyy-MM-dd HH:mm:ss}", GetData(), DateTime.Now);
            //    this.GridView1.DataSource = bll.GetExamPapersList();
            //}
            //catch (Exception ex)
            //{
            //    this.GridView1.DataSource = null;
            //    lbMessage.Text = ex.Message;
            //    lbMessage.DataBind();
            //}
            //finally
            //{
            //    this.GridView1.DataBind();
            //}
            
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
            //    TextBox1.Text = "";
            //}
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
            lbLoginMessage.Visible = false;
            lbLoginMessage.Text = "";

            if (string.IsNullOrEmpty(txtLoginID.Text.Trim()) || string.IsNullOrEmpty(txtLoginPWD.Text.Trim()))
            {
                lbLoginMessage.Text = "账号和密码不能为空。";
                lbLoginMessage.Visible = true;
                return;
            }

            string msg=string.Empty;
            Member modelMember = new Member();
            if (bll.LoginMember(txtLoginID.Text, txtLoginPWD.Text, ref msg,ref modelMember))
            {
                ZYSoft.Comm.UtilityLog.WriteInfo(string.Format("账号 {0} 登陆成功。",txtLoginID.Text));
                Session["MemberInfo"] = modelMember;
                Response.Redirect("MemberInfo.aspx", true);
            }
            else
            {
                ZYSoft.Comm.UtilityLog.WriteInfo(string.Format("账号 {0} 登陆失败。{1}",txtLoginID.Text,msg));
                lbLoginMessage.Text = msg;
                lbLoginMessage.Visible = true;
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void regsubmit_Click(object sender, EventArgs e)
        {
            lbRegisterMsg.Text = "";
            lbRegisterMsg.Visible = false;
            lbRegisterMsg.DataBind();
            string msg = string.Empty;

            //密码验证
            if (!txtPassWord.Text.Trim().Equals(txtPassWordVerify.Text.Trim()))
            {
                lbRegisterMsg.Visible = true;
                lbRegisterMsg.Text = "两次输入密码不一致。";
                lbRegisterMsg.DataBind();
                return;
            }

            //邮箱验证
            if (!bll.CheckEmail(txtEmail.Text.Trim(), ref msg))
            {
                lbRegisterMsg.Visible = true;
                lbRegisterMsg.Text = msg;
                lbRegisterMsg.DataBind();
                return;
            }
            
            int ID = -1;
            if (bll.RegistMember(txtNickName.Text.Trim(), txtEmail.Text.Trim(), txtPassWord.Text.Trim(), ref msg,ref ID))
            {
                //注册成功
                //邮箱激活
                Response.Redirect(String.Format("ActivatMember.aspx?LoginID={0}&NickName={1}&LimitTime={2}&ID={3}", txtEmail.Text.Trim(), txtNickName.Text.Trim(), msg, ID));
            }
            else
            {
                //注册失败
                lbRegisterMsg.Visible = true;
                lbRegisterMsg.Text = msg;
                lbRegisterMsg.DataBind();
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActivat_Click(object sender, EventArgs e)
        {
            string msg =string.Empty;
            if (bll.ActivatMember(Int32.Parse(HidMemberID.Value), txtActivat.Text, ref msg))
            {
                lbRegisterMsg.Text = "激活成功，请登录";
                divActivat.Visible = false;
            }
            else
            {
                lbRegisterMsg.Text = msg;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ZYSoft.Comm.UtilityLog.WriteInfo(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        }
    }
    class ObjOpenID
    {
        public string client_id
        {
            set;
            get;
        }

        public string openid
        {
            set;
            get;
        }
    }

    class ObjUser
    {
        public string ret 
        {
            set;
            get;
        }

        public string msg 
        {
            set;
            get;
        }
        public string nickname 
        {
            set;
            get;
        }
        public string gender 
        {
            set;
            get;
        }
        public string figureurl 
        {
            set;
            get;
        }
        public string figureurl_1
        {
            set;
            get;
        }
        public string figureurl_2
        {
            set;
            get;
        }
        public string figureurl_qq_1 
        {
            set;
            get;
        }
        public string figureurl_qq_2
        {
            set;
            get;
        }
        public string is_yellow_vip 
        {
            set;
            get;
        }
        public string vip 
        {
            set;
            get;
        }
        public string yellow_vip_level 
        {
            set;
            get;
        }
        public string level 
        {
            set;
            get;
        }
        public string is_yellow_year_vip 
        {
            set;
            get;
        }
    }
}