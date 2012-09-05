using System;
using ZYSoft.BLL;
using ZYSoft.Comm.Entity;

namespace Web
{
    public partial class MemberInfo : System.Web.UI.Page
    {
        private readonly UserBLL bll;

        /// <summary>
        /// 
        /// </summary>
        public MemberInfo()
        {
            bll = new UserBLL();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberInfo"] == null)
            {
                Response.Redirect("Default.aspx",true);
                return;
            }
            if (!IsPostBack)
            {
                SetMemberInfo();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMemberInfo()
        {
            Member modelMember = (Member)Session["MemberInfo"];
            if (!string.IsNullOrEmpty(modelMember.Email))
            {
                lbLoginID.Text = String.Format("邮箱/登录账号:<strong>{0}</strong>", modelMember.Email);
            }
            else
            {
                divBindEmail.Visible = true;
            }

            if (string.IsNullOrEmpty(modelMember.OpenId))
            {
                divBingQQ.Visible = true;
            }
            if (!string.IsNullOrEmpty(modelMember.OpenId) && !string.IsNullOrEmpty(modelMember.Email))
            {
                lbBindQQ.Text = "已经绑定QQ账号       <a href='#'>解除绑定</a>";
            }

            lbMemberNickname.Text = modelMember.Nickname;
            lbNickname.Text = String.Format("欢迎您:<strong>{0}</strong>", modelMember.Nickname);
            lbLoginTimes.Text = String.Format("这是您第 {0} 次登录", modelMember.LoginTimes);
            if (modelMember.LoginTimes < 2)
            {
                lbLastLoginDateTime.Text = "";
            }
            else
            {
                lbLastLoginDateTime.Text = "上次登陆时间:" + modelMember.LastLoginDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            }
            lbIntegral.Text ="您的当前积分为:"+modelMember.Integral.ToString();
            if (string.IsNullOrEmpty(modelMember.PhotoURL))
            {
                imgPhoto.ImageUrl = @"~/images/photo.jpg";
            }
            else
            {
                imgPhoto.ImageUrl = modelMember.PhotoURL;
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoginOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("default.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdbNotExist_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNotExist.Checked)
            {
                lbMessage.Text = "";
                lbMessage.Visible = false;
                lbMessage.DataBind();
                lbEmail.Text = "邮箱地址:";
                lbPassWord.Text = "设置密码:";
                btnVerify.Text = "开始绑定";
                //btnVerify.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdbExist_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbExist.Checked)
            {
                lbMessage.Text = "";
                lbMessage.Visible = false;
                lbMessage.DataBind();
                lbEmail.Text = "登陆账号:";
                lbPassWord.Text = "登录密码:";
                btnVerify.Text = "登录绑定";
                //btnVerify.DataBind();
            }
        }

        /// <summary>
        /// 验证邮箱/绑定已有账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            lbMessage.Text = "";
            lbMessage.Visible = false;

            Member modelMember = (Member)Session["MemberInfo"];
            string strMsg=string.Empty;
            if (rdbNotExist.Checked)
            {
                //邮箱激活
                if (bll.BindNewEmail(txtEmail.Text, txtPassWord.Text, modelMember.Id, ref strMsg))
                {
                    //显示已经激活，QQ账号登录后确实是已经激活状态
                    //输入正确激活码后应该直接登录显示绑定的邮箱
                    //另外现在没有添加历史记录
                    Response.Redirect(String.Format("ActivatMember.aspx?LoginID={0}&NickName={1}&LimitTime={2}&ID={3}", txtEmail.Text.Trim(), modelMember.Nickname, strMsg, modelMember.Id));
                }
                else
                {
                    lbMessage.Visible = true;
                    lbMessage.Text = strMsg;
                }
            }
            else
            {
                if (bll.BindOldEmail(txtEmail.Text, txtPassWord.Text, modelMember.Id, ref strMsg))
                {
                    lbLoginID.Text = String.Format("邮箱/登录账号:<strong>{0}</strong>", txtEmail.Text);
                    modelMember.Email = txtEmail.Text;
                    Session["MemberInfo"] = modelMember;
                    lbMessage.Visible = true;
                    lbMessage.Text = "邮箱绑定成功";
                    divBindEmail.Visible = false;
                }
                else
                {
                    lbMessage.Visible = true;
                    lbMessage.Text = strMsg;
                }
            }
        }
    }
}