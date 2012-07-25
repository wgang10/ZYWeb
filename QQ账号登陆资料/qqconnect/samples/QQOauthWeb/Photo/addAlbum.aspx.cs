using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QQOauthWeb.Photo
{
    public partial class addAlbum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           var qzone2 = Session["qzonesdk"] as QzoneSDK.Qzone;
           var addresult = qzone2.AddAlbum(this.albumname.Text,this.albumdesc.Text ,Convert.ToInt32(this.priv.Text),string.Empty,string.Empty);
           this.result.Text = addresult;
        }
    }
}