<%@ Page Title="子杨软件-ZiYangSoft-值得信赖" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MemberInfo.aspx.cs" Inherits="Web.MemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Label ID="lbMemberNickname" runat="server" Text="登陆用户昵称"/>
					        &nbsp;&nbsp;&nbsp;&nbsp;<a href="#">设置</a>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                        ID="btnLoginOut" runat="server" Text="注销" onclick="btnLoginOut_Click" 
                        CausesValidation="False" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allctn">
        <div class="ctnwkg">
            <div class="ctnkg">
                <div class="ctnlinebg">
                    <div class="ctnwzkg">
                        <div class="introbg">
                            <div class="leftkg">
                                <div class="moviekg">
                                    <div id="barScroll" class="mrwm_slide">
                                        <div id="bannerBox">
                                            <div id="bannerImgesBox"> 
                                                <a href="#" style="display:block;"><img src="images/1.png"></a> 
                                                <a href="#"><img src="images/2.png"></a> 
                                                <a href="#"><img src="images/3.png"></a>
                                                <a href="#"><img src="images/4.jpg"></a>
                                                <a href="#"><img src="images/5.jpg"></a>
                                            </div>
                                            <div id="bannerTextBox">
                                            <ul>
                                                <li class="thisHover" set="0"><a href="###">1</a></li>
                                                <li set="1"><a href="###">2</a></li>
                                                <li set="2"><a href="###">3</a></li>
                                                <li set="3"><a href="###">4</a></li>
                                                <li set="4"><a href="###">5</a></li>
                                            </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="noticekg">
                                    <span class="notice"><a title="最新新闻" href="#/">最新新闻</a></span> 
                                    <ul>
                                        <li><a title="子杨软件网站开始试运行" href="#">子杨软件网站开始试运行【2012.8.01】</a></li>
                                        <li><a title="子杨软件成功接入QQ账号登陆" href="#">子杨软件成功接入QQ账号登陆【2012.7.26】</a></li>
                                        <li><a title="子杨软件空间申请成功" href="#">子杨软件空间申请成功【2012.7.18】</a></li>
                                        <li><a title="子杨软件域名申请成功www.ziyangsoft.com【2012.7.16】" href="#">子杨软件域名申请成功www.ziyangsoft.com【2012.7.16】</a></li>
                                        <li><a title="热烈祝贺子杨软件创立！！！【2012.7.16】" href="#">热烈祝贺子杨软件创立！！！【2012.7.16】</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="regkg1">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                <br/><asp:Image ID="imgPhoto" runat="server" />
                                <br/><br/><asp:Label ID="lbNickname" runat="server" Text=""/>
                                <br/><asp:Label ID="lbLoginID" runat="server" Text=""/>
                                <br/><asp:Label ID="lbLoginTimes" runat="server" Text=""/>
                                <br/><asp:Label ID="lbLastLoginDateTime" runat="server" Text=""/>
                                <br/><asp:Label ID="lbIntegral" runat="server" Text=""/>
                                <br/><asp:Label ID="lbBindQQ" runat="server" Text=""/>
                                <br/><asp:Label ID="lbMessageMember" runat="server" Text=""/>
                                <div id="divBindEmail" runat="server" visible="false">
                                    <br/><strong>请绑定您的邮箱</strong>
                                    <br/>
                                    <asp:RadioButton ID="rdbNotExist" runat="server" Text="没有网站账号" 
                                        AutoPostBack="True" Checked="True" 
                                        oncheckedchanged="rdbNotExist_CheckedChanged" GroupName="Exist" />
                                    <asp:RadioButton ID="rdbExist" runat="server"  Text="已有网站账号" 
                                        AutoPostBack="True" oncheckedchanged="rdbExist_CheckedChanged" 
                                        GroupName="Exist"/>
                                    <br/><asp:Label ID="lbEmail" runat="server" Text="邮箱地址:"/>
                                    <asp:TextBox ID="txtEmail" Width="150px" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="请输入正确的邮箱地址" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <br/><asp:Label ID="lbPassWord" runat="server" Text="设置密码:"/>
                                    <asp:TextBox ID="txtPassWord" Width="150px" runat="server" 
                                        TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtPassWord" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <br/><asp:Button ID="btnVerify" Width="80px" runat="server" Text="开始绑定" 
                                        onclick="btnVerify_Click" />
                                </div>
                                <div id="divBingQQ" runat="server" visible="false">
                                    <br/><strong>绑定QQ账号</strong>
                                    <br/>建立绑定后你可使用QQ账号快速登录网站
                                    <br/><a href="#">立刻绑定</a>
                                </div>
                                <br/><asp:Label ID="lbMessage" runat="server" Visible="false" ForeColor="White" BackColor="Red" />
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
				    </div>
			    </div>
		    </div>
        </div>
	    <div class="btlist">
            <div class="btctn">
                <ul>
                    <LI class="wd1"><a href="#"><img title=产品一 alt=产品一 src="images/icon1.jpg"/></a> </li>
                    <LI class="wd2"><a href="#"><FONT class="font14wz b">产品一</FONT><BR>产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介。</a> 
                    </li>
                </ul>
                <ul>
                    <LI class="wd1"><a href="#"><IMG title=产品二 alt=产品二 src="images/icon2.jpg"></a> </li>
                    <LI class="wd2"><a href="#"><FONT class="font14wz b">产品二</FONT><BR>产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介。</a> </li>
                </ul>
                <ul>
                    <LI class="wd1"><a href="#"><IMG title=产品三 alt=产品三 src="images/icon3.jpg"></a> </li>
                    <LI class="wd2"><a href="#"><FONT class="font14wz b enwz1">产品三</FONT><BR>产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介产品简介。</a> 
                    </li>
                </ul>
		    </div>
	    </div>
    </div>
</asp:Content>
