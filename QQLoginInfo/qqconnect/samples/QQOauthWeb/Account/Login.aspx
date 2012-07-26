<%@ Page Title="会员登录" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="QQOauthWeb.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openQQ() {
            var A = window.open("/account/logintoqq.aspx", "TencentLogin", "width=450,height=320,menubar=0,scrollbars=0, status=1,titlebar=0,toolbar=0,location=1");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                <div class="regBox login">
                    <h2>
                        会员登录</h2>
                    <div class="accountInfo">
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">用户名</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:CheckBox ID="RememberMe" runat="server" />
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">在这台电脑上记住我</asp:Label>
                        </p>
                        <p class="submitButton">
                            <asp:Button ID="LoginButton" runat="server" Text="登陆" 
                                ValidationGroup="LoginUserValidationGroup" onclick="LoginButton_Click" />
                        </p>
                    </div>
                </div>
            </td>
            <td>
                <div class="login-r Fixed">
                    <div class="up-box">
                        不是会员，<asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">注册</asp:HyperLink>
                    </div>
                    <ul class="login-list">
                        <li>
                            <p class="infor">
                                用QQ帐号登录</p>
                            <div class="account">
                                <input type="image" src="../images/Connect_logo_4.png" name="btQQLogin" value="腾讯QQ登录"
                                    id="Image1" onclick="openQQ();return false;" />
                            </div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
