<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addAlbum.aspx.cs" Inherits="QQOauthWeb.Photo.addAlbum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        相册：<asp:TextBox ID="albumname" Text="test" runat="server" /><br/>
        描述：<asp:TextBox ID="albumdesc" Text="test" runat="server" /><br/>
        权限：<asp:TextBox ID="priv" Text="1" runat="server" /><br/>
        <asp:Button ID="btnAdd" runat="server" OnClick="Button1_Click" Text="创建相册" />
    </div>
    <div>
     <asp:Literal ID="result" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
