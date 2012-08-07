<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivatMember.aspx.cs" Inherits="Web.ActivatMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbLoginID" runat="server" Text=""/>
        <br/><asp:Label ID="lbMsg1" runat="server" Text=""/>
        <br/><asp:Label ID="lbMsg2" runat="server" Text=""/>
        <br/><asp:TextBox ID="txtActivat" Width="300px" runat="server"/>
        <asp:HiddenField ID="HidMemberID" runat="server" />
        <asp:Button ID="btnActivat" runat="server" Text="激活" onclick="btnActivat_Click" />
        <br/><asp:Label ID="lbMsg3" runat="server" Text=""/>
    </div>
    </form>
</body>
</html>
