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
    <div>
    <style>
.bizmail_loginpanel{font-size:12px;width:300px;height:auto;border:1px solid #cccccc;background:#ffffff;}
.bizmail_LoginBox{padding:10px 15px;}
.bizmail_loginpanel h3{padding-bottom:5px;margin:0 0 5px 0;border-bottom:1px solid #cccccc;font-size:14px;}
.bizmail_loginpanel form{margin:0;padding:0;}
.bizmail_loginpanel input.text{font-size:12px;width:100px;height:20px;margin:0 2px;border:1px solid #C3C3C3;border-color:#7C7C7C #C3C3C3 #C3C3C3 #9A9A9A;}
.bizmail_loginpanel .bizmail_column{height:28px;}
.bizmail_loginpanel .bizmail_column label{display:block;float:left;width:30px;height:24px;line-height:24px;font-size:12px;}
.bizmail_loginpanel .bizmail_column .bizmail_inputArea{float:left;width:240px;}
.bizmail_loginpanel .bizmail_column span{font-size:12px;word-wrap:break-word;margin-left: 2px;line-height:200%;}
.bizmail_loginpanel .bizmail_SubmitArea{margin-left:30px;clear:both;}
.bizmail_loginpanel .bizmail_SubmitArea a{font-size:12px;margin-left:5px;}
.bizmail_loginpanel select{width:110px;height:20px;margin:0 2px;}
</style>
<script type="text/javascript" src="http://exmail.qq.com/zh_CN/htmledition/js_biz/outerlogin.js"  charset="gb18030"></script>
<script type="text/javascript">
    writeLoginPanel({ domainlist: "ziyangsoft.com", mode: "vertical" });
</script>
</div>
    <p>
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
    </p>
                <div runat="server">                    
                    <ul>
                        <li>
                            <div class="account">
                                <img src="./images/Connect_logo_4.png" alt="使用腾讯QQ登录" onclick="openQQ();return false;" />
                            </div>
                        </li>
                        <li>
                            <div>
		                        <a href="https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=100289171&redirect_uri=www.ziyangsoft.com&scope=get_user_info">
			                        <img alt="使用腾讯QQ登陆" src="./images/Connect_logo_5.png" />
		                        </a>
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
