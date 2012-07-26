<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QQCallback.aspx.cs" Inherits="Web.QQCallback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        //定位左上角 
        self.moveTo(0, 0);
        //调整屏幕 
        self.resizeBy(screen.availWidth, screen.availHeight); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="result" runat="server"></asp:Label>
    <asp:Label ID="Nickname" runat="server" />
    <asp:Label ID="Figureurl" runat="server" />
        <asp:Label ID="lbMoreInfo" runat="server"></asp:Label>
        <asp:Image ID="Image1" runat="server" />
    </div>
    </form>
</body>
</html>
