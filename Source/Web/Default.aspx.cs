using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZYSoft.BLL;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly UserBLL bll;

        public Default()
        {
            bll = new UserBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lbMessage.Text = GetData()+ " 获取时间 "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private string GetData()
        {
            return bll.GetUaerName();
        }
    }
}