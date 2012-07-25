<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PhotoList.aspx.cs" Inherits="QQOauthWeb.Photo.PhotoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/Photo/addAlbum.aspx">创建相册</asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/Photo/UploadPic.aspx">上传图片</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="dataGrid" runat="server">
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
