<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="QQCallback.aspx.cs" Inherits="QQOauthWeb.Account.QQCallback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        //定位左上角 
        self.moveTo(0, 0);
        //调整屏幕 
        self.resizeBy(screen.availWidth, screen.availHeight); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="result" runat="server"></asp:Label>
    <asp:Label ID="Nickname" runat="server" />
    <asp:Label ID="Figureurl" runat="server" />
</asp:Content>
