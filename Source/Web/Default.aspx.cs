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
        //private Member modelMember;

        public Default()
        {
            bll = new UserBLL();
            model = new ExamPaper();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                //BindGridData();
                if (Session["MemberInfo"] == null)
                {
                    GetUserInfo();
                }
                else
                {
                    Response.Redirect("MemberInfo.aspx");
                }
                //else
                //{
                //    divLogin.Visible = false;
                //    divRegiste.Visible = false;
                //    divLogined.Visible = true;
                //    divUserInfo.Visible = true;
                //    SetMemberInfo();
                //}
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
                #region QQ登录
                try
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
                    //lbMessage2.Text = responseFromServer;
                    //发送后会得到如下信息
                    //access_token=AEE7091E761C2A571991234AD280E6BA&expires_in=7776000

                    string access_token = responseFromServer.Substring(responseFromServer.IndexOf("=") + 1);
                    access_token = access_token.Substring(0, access_token.IndexOf("&"));
                    Url = string.Format("https://graph.qq.com/oauth2.0/me?access_token={0}", access_token);
                    //请求https://graph.qq.com/oauth2.0/me?access_token=AEE7091E761C2A571991234AD280E6BA
                    request = WebRequest.Create(Url);
                    response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    //lbMessage3.Text = responseFromServer;
                    //得到如下信息
                    //callback( {"client_id":"100289171","openid":"1AC83BAA19BB2E892033E0C07C27AC24"} ); 
                    string openid = responseFromServer.Replace(@"\", "").Substring(responseFromServer.IndexOf("openid") + 9);
                    openid = openid.Substring(0, openid.IndexOf("}") - 1);
                    //lbMessage4.Text = "openid=" + openid;


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


                    //lbMessage5.Text = responseFromServer;
                    string[] UserInfo = responseFromServer.Split(',');
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
                    modelMember.Nickname = UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                    modelMember.PhotoURL = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1);
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
                        lbMessage.Text = msg;
                    }

                    //Random rdm = new Random(100);
                    //Response.Redirect("Default.aspx?x=" + rdm.Next().ToString());
                }
                catch (Exception ex)
                {
                    lbMessage.Text = ex.Message;
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
            string msg=string.Empty;
            Member modelMember = new Member();
            if (bll.LoginMember(txtLoginID.Text, txtLoginPWD.Text, ref msg,ref modelMember))
            {   
                Session["MemberInfo"] = modelMember;
                Response.Redirect("MemberInfo.aspx", true);
            }
            else
            {
                lbMessage.Text = msg;
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
            string msg=string.Empty;
            int ID = -1;
            if (bll.RegistMember(txtNickName.Text.Trim(), txtEmail.Text.Trim(), txtPassWord.Text.Trim(), ref msg,ref ID))
            {
                //注册成功
                //邮箱激活
                Response.Redirect(String.Format("ActivatMember.aspx?LoginID={0}&NickName={1}&LimitTime={2}&ID={3}", txtEmail.Text.Trim(), txtNickName.Text.Trim(), msg, ID));

//                bool blFlag = false;
//                string strMessage = string.Empty;
//                string strTitle = "欢迎你注册子杨软件";
//                string strMailTo = txtEmail.Text.Trim();
//                string strMailBody = string.Format(@"亲爱的{0}：您好！
//	
//
//	感谢您注册子杨软件。
//    
//	您的激活码为：{1}
//
//    请拷贝以上激活码进行激活。
//
//    本邮件为系统自动发送，请勿回复。
//
//    谢谢！
//
//    子杨软件|www.ziyangsoft.com", txtNickName.Text.Trim(),msg);
//                blFlag = ZYSoft.Comm.GlobalMethod.SendMail(strMailTo, strTitle, strMailBody, out strMessage);
//                if (blFlag)
//                {
//                    //XTHospital.BLL.BLL_Log.AddLog("用户[" + strUserName + "]使用了找回密码功能，将密码发送到了邮箱[" + strMailTo + "].", "1", Page.Request.UserHostAddress);//添加日志
//                    lbRegisterMsg.Text = String.Format("注册成功！已将激活码发送到了邮箱{0}，请进入邮箱查收进行激活。", txtEmail.Text.Trim());
//                    lbRegisterMsg.DataBind();
//                    divActivat.Visible = true;
//                    HidMemberID.Value = ID.ToString();
//                }
//                else
//                {
//                    //XTHospital.BLL.BLL_Log.AddLog("用户[" + strUserName + "]使用了找回密码功能，发送到邮箱[" + strMailTo + "]时失败." + strMessage, "1", Page.Request.UserHostAddress);//添加日志
//                    lbRegisterMsg.Text = String.Format("发送到邮箱[{0}]时失败.", strMailTo);
//                    lbRegisterMsg.DataBind();
//                }
            }
            else
            {
                //注册失败
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
    }
}