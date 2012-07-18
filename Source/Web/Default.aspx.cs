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
                this.lbMessage.Text = GetData()+ " 获取时间 "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BindGridData();
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
            }
        }
    }
}