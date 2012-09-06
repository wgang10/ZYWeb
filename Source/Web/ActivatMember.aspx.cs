using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZYSoft.BLL;
using ZYSoft.Comm.Entity;

namespace Web
{
    public partial class ActivatMember : System.Web.UI.Page
    {
        private readonly UserBLL bll;

        public ActivatMember()
        {
            bll = new UserBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    bool blFlag = false;
                    string strMessage = string.Empty;
                    string strTitle = "欢迎你注册子杨软件";
                    string strMailTo = Request["LoginID"];
                    HidMemberID.Value = Request["ID"];
                    string strNickName = Request["NickName"];
                    string strLimitTime = Request["LimitTime"];
                    string strMailBody = string.Format(@"亲爱的{0}：您好！	

    感谢您注册子杨软件。
    
    您的激活码为：{1}

    请拷贝以上激活码进行激活。

    本邮件为系统自动发送，请勿回复。谢谢！

    子杨软件|www.ziyangsoft.com", strNickName, strLimitTime);
                    blFlag = ZYSoft.Comm.GlobalMethod.SendMail(strMailTo, strTitle, strMailBody, out strMessage);
                    if (blFlag)
                    {
                        lbMsg1.Text = String.Format("注册成功！您的账号<strong>{0}</strong>。", strMailTo);
                        lbMsg2.Text = "请登录您的邮箱！找到我们给您发送的验证码进行激活。";
                        lbMsg3.Text = @"如果没有收到验证邮件：
<br/>1、确认邮箱地址有没有写错。
<br/>2、查看是否在垃圾邮件或广告邮件里。
<br/>3、稍等几分钟再看。
<br/>4、若10分钟左右仍没有收到邮件，请点击<a href='#'>重发</a>";//设置重发
                        lbMsg1.DataBind();
                        lbMsg2.DataBind();
                        lbMsg3.DataBind();
                    }
                    else
                    {
                        lbMsg1.Text = String.Format("发送到邮箱[{0}]时失败.", strMailTo);
                        lbMsg1.DataBind();
                    }
                }
                catch
                {
                    Response.Redirect("MemberInfo.aspx");
                }
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActivat_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (bll.ActivatMember(Int32.Parse(HidMemberID.Value), txtActivat.Text, ref msg))
            {
                lbMsg1.Text = "激活成功，请登录";
                lbMsg2.Text = "";
                lbMsg3.Text = "";
                lbMsg1.DataBind();
                //激活成功后直接登录
                Member modelMember = new Member();
                if (bll.LoginMember(Int32.Parse(HidMemberID.Value), ref msg, ref modelMember))
                {
                    //设置session
                    Session["MemberInfo"] = modelMember;
                    //跳转登录
                    Response.Redirect("MemberInfo.aspx");
                }
                else
                {
                    lbMsg1.Text = msg;
                    lbMsg2.Text = "";
                    lbMsg3.Text = "";
                }
            }
            else
            {
                lbMsg1.Text = msg;
                lbMsg2.Text = "";
                lbMsg3.Text = "";
            }
        }
    }
}