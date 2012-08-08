<%@ Page Title="激活" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ActivatMember.aspx.cs" Inherits="Web.ActivatMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lbLoginID" runat="server" Text=""/>
        <br/><asp:Label ID="lbMsg1" runat="server" Text=""/>
        <br/><asp:Label ID="lbMsg2" runat="server" Text=""/>
        <br/><asp:TextBox ID="txtActivat" Width="300px" runat="server"/>
        <asp:HiddenField ID="HidMemberID" runat="server" />
        <asp:Button ID="btnActivat" runat="server" Text="激活" onclick="btnActivat_Click" />
        <br/><asp:Label ID="lbMsg3" runat="server" Text=""/>
</asp:Content>
