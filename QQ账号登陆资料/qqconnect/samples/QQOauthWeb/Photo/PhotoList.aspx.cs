using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QzoneSDK.Models;
using Jayrock.Json.Conversion;

namespace QQOauthWeb.Photo
{
    public partial class PhotoList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var qzone2 = Session["qzonesdk"] as QzoneSDK.Qzone;
            if (qzone2 != null)
            {

                var photolist = qzone2.GetCurrentUserListAlbum();

                UserAlbums albums = (UserAlbums)JsonConvert.Import(typeof(UserAlbums), photolist);
                this.dataGrid.DataSource = albums.Album;
                this.dataGrid.DataBind();
                this.Literal1.Text = string.Format("相册总数{0}个", albums.Albumnum);
            }


        }
    }
}