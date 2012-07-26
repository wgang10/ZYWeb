<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>子杨软件</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/reg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function openQQ() {
            var A = window.open("/logintoqq.aspx", "TencentLogin", "width=450,height=320,menubar=0,scrollbars=0, status=1,titlebar=0,toolbar=0,location=1");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p><strong>子杨软件</strong></p>
    <p>Zi Yang Soft</p>
    <p>
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
    </p>
                <div class="login-r Fixed" runat="server">                    
                    <ul class="login-list">
                        <li>
                            <div class="account">
                                <input type="image" src="../images/Connect_logo_4.png" name="btQQLogin" value="腾讯QQ登录"
                                    id="Image1" onclick="openQQ();return false;" />
                            </div>
                        </li>
                    </ul>
                </div>
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
    <p<strong>版权所有:子杨软件</strong></p>
    </form>
</body>
</html>
