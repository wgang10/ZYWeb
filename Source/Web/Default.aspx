<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head>
    <title>子杨智能软件</title>
    <style type="text/css">
        .style1 {
            width: 528px;
            height: 387px;
        }
        .style4
        {
            font-family: 微软雅黑;
            font-size: x-large;
        }
        .style5
        {
            font-family: 幼圆;
            font-size: x-small;
        }
        .style6
        {
            font-family: "Gill Sans Ultra Bold";
            font-size: x-large;
        }
    </style>
</head>
</head>
<body>
    <form id="form1" runat="server">
    <p class="style4"><strong>子杨智能软件</strong></p>
    <p class="style6">Zi Yang Soft</p>
    <p>
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
    </p>
    <p>
    <img alt="" class="style1" src="./image/1.jpg" />
    </p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="添加" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TextBox1" ErrorMessage="必须输入"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </p>
    <p class="style5"><strong>版权所有:子杨智能软件</strong></p>
    </form>
</body>
</html>
