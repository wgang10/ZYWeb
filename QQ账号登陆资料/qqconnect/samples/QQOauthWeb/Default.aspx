<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="QQOauthWeb._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        欢迎使用QQ登陆!
    </h2>
    <p>
       通过“QQ登录”，用户能使用QQ帐号一键登录接入网站，大大降低了用户注册、登录的门槛，借助庞大的QQ用户群，给第三方网站带来更多新用户。已登录用户还可以将在第三方网站发布、分享的信息即时同步到QQ空间，使网站内容通过好友关系得到进一步的传播，从而提升网站的访问量和用户数。
    </p>
    <p>
        你也可以在这里找到更详细的文档<a href="http://connect.opensns.qq.com/"
            title="QQ登陆">腾讯开放社区的QQ登陆文档</a>.
    </p>
</asp:Content>
