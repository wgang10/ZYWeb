﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

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
        .style2 {
            width: 993px;
            height: 688px;
        }
        .style3 {
            width: 1090px;
            height: 691px;
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
    <img alt="" class="style2" src="./image/2.jpg" />
    </p>
    <p>
    <img alt="" class="style3" src="./image/3.jpg" />
    </p>
    <p class="style5"><strong>版权所有:子杨智能软件</strong></p>
    </form>
</body>
</html>