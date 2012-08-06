using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using ZYSoft.BLL;
using ZYSoft.Comm.Entity;

namespace Web
{
    public partial class MemberInfo : System.Web.UI.Page
    {
        private readonly UserBLL bll;

        public MemberInfo()
        {
            bll = new UserBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberInfo"] == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }
            if (!IsPostBack)
            {
                //GetUserInfo();
            }
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
                string Url = string.Format("https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&state={3}&redirect_uri={4}"
                    , apppid, appkey, code, state, callbackUrl);

                WebRequest request = WebRequest.Create(Url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
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
                //得到如下信息
                //callback( {"client_id":"100289171","openid":"1AC83BAA19BB2E892033E0C07C27AC24"} ); 
                string openid = responseFromServer.Replace(@"\", "").Substring(responseFromServer.IndexOf("openid") + 9);
                openid = openid.Substring(0, openid.IndexOf("}") - 1);

                

                //以调用get_user_info接口为例：
                //发送请求到get_user_info的URL（请将access_token，appid等参数值替换为你自己的）：
                Url = string.Format("https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}", access_token, apppid, openid);
                request = WebRequest.Create(Url);
                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
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

                //用户登录
                string msg = string.Empty;
                if (bll.LoginMember(openid, ref msg))
                {
                    IList<Member> members = bll.GetMemberByOpenID(openid);
                    if (members.Count > 0)
                    {
                        lbMemberNickname.Text = lbNickname.Text = UserInfo[2].Substring(UserInfo[2].IndexOf(":") + 2, UserInfo[2].Length - UserInfo[3].IndexOf(":") - 2);
                        lbLoginTimes.Text = members[0].LoginTimes.ToString();
                        lbLastLoginDateTime.Text = members[0].LastLoginDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        lbIntegral.Text = members[0].Integral.ToString();
                        imgPhoto.ImageUrl = UserInfo[5].Substring(UserInfo[5].IndexOf("http"), UserInfo[5].Length - UserInfo[5].IndexOf("http") - 1);
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
        }
    }
}