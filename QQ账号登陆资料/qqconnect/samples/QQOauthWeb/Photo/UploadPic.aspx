<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UploadPic.aspx.cs" Inherits="QQOauthWeb.Photo.UploadPic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="照片描述"></asp:Label>
    <asp:TextBox runat="server"  id="photodesc"  Text="test" /><br/>
    <asp:Label ID="Label2" runat="server" Text="照片拍摄时的地理位置的经度"></asp:Label>
    <asp:TextBox runat="server"  id="x"  Text="1" /><br/>
    <asp:Label ID="Label3" runat="server" Text="照片拍摄时的地理位置的纬度"></asp:Label>
    <asp:TextBox runat="server" ID="y" Text="1" /><br/>
    相册ID：<asp:TextBox runat="server" id="albumid" Text="123" /><br/>
    <asp:Label ID="Label4" runat="server" Text="照片名称"></asp:Label>
    <asp:TextBox runat="server"  id="title"  Text="test" /><br/>
    <asp:Label ID="Label5" runat="server" Text="图片"></asp:Label>
    <asp:FileUpload ID="FileUpload1" runat="server" /><br />
    <asp:Button ID="Button1" runat="server" Text="上传照片" onclick="Button1_Click" />
</asp:Content>
