<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="zh-cn" xml:lang="zh-cn" xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>子杨软件--Zi Yang Soft-值得信赖</title>
<meta name="description" content="Zi Yang Soft,子杨软件。"/>
<meta name="keywords" content="子杨,子杨软件,软件,WMS,考试系统,Silverlight,进销存,考试系统,桌面,体检系统,健康体检"/>
<meta name="title" content="子杨|子杨软件|软件|WMS|考试系统|Silverlight进销存考试系统桌面体检系统健康体检|子杨软件-值得信赖"/>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
<meta content="IE=7" http-equiv="X-UA-Compatible"/>
<meta content="zh-cn" http-equiv="Content-Language"/>
<meta content="no-cache" http-equiv="pragma"/>
<meta content="no-cache" http-equiv="cache-control"/>
<meta content="0" http-equiv="expires"/>
<link rel="stylesheet" type="text/css" href="Styles/index.css"/>
<!--[if IE 6]>
<script type="text/javascript" src="http://st3.dbank.com/js/DD_belatedPNG.js?version=2.6.2d" ></script>
<![endif]--><!-- google code -->
<script type="text/javascript" src="Script/jquery.js"></script>
<script type="text/javascript" src="Script/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="Script/common-index.js"></script>
<script type="text/javascript">
    function openQQ() {
        var A = window.open("/logintoqq.aspx", "TencentLogin", "width=450,height=320,menubar=0,scrollbars=0, status=1,titlebar=0,toolbar=0,location=1");
    }

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-8272227-1']);
    _gaq.push(['_setDomainName', 'dbank.com']);
    _gaq.push(['_trackPageview']);
    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

    //无插件 banner切换效果
    $(function () {
        //获取可点触发编号
        $('#bannerTextBox').find('li').mouseover(function () {
            //停止自动切换
            clearInterval(t1);
            //获取编号自定义值 以获取对应图片
            var i = $(this).attr('set');
            //调用切换效果，切换到当前鼠标焦点编号的banner
            changeBanner('x', i);
            //设置当前编号的颜色 
            setListNo.apply(this, ['x']);
        }).mouseout(function () {
            //鼠标离开编号 自动开始继续切换
            t1 = setInterval(changeBanner, '3000');
        })
        //定义可触发编号 该变量用来自动切换过程中累加计算 用于判断是否到达最好一个banner位置
        var bannerListIndex = 1;

        //设置编号 参数setNo 用来判断方法是setInterval自动触发的还是 mouseover手动触发
        function setListNo(setNo) {
            //清空历史的选中位置
            $('#bannerTextBox').find(".thisHover").removeClass('thisHover');
            //如果是setInterval自动触发
            if (setNo != "x") {
                //通过 定义可触发编号bannerListIndex 累加参数判断选中位置
                $('#bannerTextBox').find("li").eq(setNo).addClass("thisHover");
            }
            //如果是手动触发
            else {
                //通过设置当前鼠标点击对象设置 选中位置
                $(this).addClass("thisHover");
            }
        }

        //切换方法 参数o 用来判断是否是手动出发  i 用来记录手动出发位置，以便真确定义显示图片
        function changeBanner(o, i) {
            $("#bannerImgesBox").find('a').hide();
            if (o != 'x') {
                setListNo(bannerListIndex);
                $("#bannerImgesBox").find('a').eq(bannerListIndex).fadeIn("slow");
            } else {

                $("#bannerImgesBox").find('a').eq(i).fadeIn("slow");
                bannerListIndex = i;
            }
            // 累加bannerListIndex 于判断是否到达最后一个banner位置 
            bannerListIndex++;
            if (bannerListIndex > 4) {
                bannerListIndex = 0
            }
        }
        //changeBanner();
        var t1 = setInterval(changeBanner, '3500');
    })
</script>
<meta name="GENERATOR" content="MSHTML 9.00.8112.16447"/>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
        <div id="allscroll" class="allkg">
	        <div id="scrollctn" class="scrollctn bg1">
		        <div class="allscn">
			        <div class="tpkg">
				        <div class="tphd" runat="server" id="divLogin" visible="true">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
					        <a href="http://www.ziyangsoft.com/"><img style="float:none; MARGIN:0px 0px 15px 0px" title="子杨软件" alt="子杨软件" src="images/TopLogo.png"/></a> 
					        账号<asp:TextBox ID="txtLoginID" autocomplete="off" runat="server" />
					        密码<asp:TextBox ID="txtLoginPWD" TextMode="Password" runat="server" /><asp:CheckBox ID="ckRemberPWD" Text="记住密码" runat="server" />
                            <asp:Button ID="btnLogin" runat="server" Text="登录" CausesValidation="False" 
                                onclick="btnLogin_Click" /> <a href="#">忘记密码？</a> 
					        <a href="https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=100289171&redirect_uri=www.ziyangsoft.com&scope=get_user_info,do_like&state=115039554">
					        <img alt="使用QQ账号登陆" style="float:none; MARGIN:0px 0px 0px 10px" src="images/Connect_logo_3.png" /></a>
				        </div>
                        <div class="tphd" runat="server" id="divLogined" visible="false">
					        <a href="http://www.ziyangsoft.com/"><img style="float:none; MARGIN:0px 0px 15px 0px" title="子杨软件" alt="子杨软件" src="images/TopLogo.png"/></a> 
					        <asp:Label ID="lbMemberNickname" runat="server" Text="登陆用户昵称"/>
					        &nbsp;&nbsp;&nbsp;&nbsp;<a href="MemberInfo.aspx">设置</a>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                                ID="btnLoginOut" runat="server" Text="注销" onclick="btnLoginOut_Click" 
                                CausesValidation="False" />
				        </div>
                        <br/><asp:Label ID="lbLoginMessage" runat="server" ForeColor="White" Visible="false" BackColor="Red"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
			        </div>
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
                                                                <a href="#"><img src="images/2.png"/></a> 
                                                                <a href="#"><img src="images/3.png"/></a>
                                                                <a href="#"><img src="images/4.jpg"/></a>
                                                                <a href="#"><img src="images/5.jpg"/></a>
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
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>                                                      
                                            <div class="regkg" runat="server" id="divRegiste" visible="true">
                                                <ul>
                                                  <li><img title="快速注册" alt="快速注册" src="images/register.png"/>
                                                      <br/><asp:Button ID="Button1" runat="server" Text="Button" 
                                                          CausesValidation="False" onclick="Button1_Click" />
                                                    </li>
                                                  
                                                  <li>设置您的昵称：<br/><asp:TextBox ID="txtNickName" MaxLength="32" autocomplete="off" runat="server" Width="150px"/>
                                                      <asp:RequiredFieldValidator ID="rfvNickName" runat="server" 
                                                          ControlToValidate="txtNickName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </li>
                                                  <li>填写您的常用邮箱：<br/><asp:TextBox ID="txtEmail" MaxLength="50" autocomplete="off" runat="server" Width="150px"/>
                                                      <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" />
                                                      <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="请输入正确的邮箱地址" 
                                                          ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </li>
                                                  <li>设置您的网站密码：<br/><asp:TextBox ID="txtPassWord" MaxLength="32" autocomplete="off" runat="server" TextMode="Password" Width="150px"/>
                                                      <asp:RequiredFieldValidator ID="rfvPassWord" runat="server" 
                                                          ControlToValidate="txtPassWord" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </li>
                                                  <li>确认您的网站密码：<br/><asp:TextBox ID="txtPassWordVerify" MaxLength="32" autocomplete="off" runat="server" TextMode="Password" Width="150px"/>
                                                      <asp:RequiredFieldValidator ID="rfvPassWordVerify" runat="server" 
                                                          ControlToValidate="txtPassWordVerify" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>                                                      
                                                    </li>
                                                  <li><asp:Button ID="regsubmit" runat="server" Text="" 
                                                          onMouseUp="this.className='btn1_over'" class="btn1" 
                                                          onMouseOver="this.className='btn1_over'" onMouseOut="this.className='btn1'" 
                                                          onMouseDown="this.className='btn1_press'" onclick="regsubmit_Click"/>                                            
                                                    <h2><a href="#">《服务协议》</a></h2>
                                                  </li>
                                                  <li>
                                                      <asp:Label ID="lbRegisterMsg" runat="server" BackColor="Red" ForeColor="White" Visible="false" />
                                                      <asp:HiddenField ID="HidMemberID" runat="server" />
                                                  </li>
                                                  <li></li>
                                                  <li>
                                                      <div id="divActivat" runat="server" visible="false">
                                                          <asp:TextBox ID="txtActivat" Width="200px" runat="server"/>
                                                          <asp:Button ID="btnActivat" runat="server"
                                                              Text="激活" onclick="btnActivat_Click" />
                                                      </div>
                                                  </li>
                                                </ul>
                                	        </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div runat="server" id="divUserInfo" visible="false">
                                                欢迎您：<asp:Label ID="lbNickname" runat="server" Text=""/>
                                                <p>当前头像<asp:Image ID="imgPhoto" runat="server" /></p>
                                                <p>这是您第&nbsp;<asp:Label ID="lbLoginTimes" runat="server" Text=""/>&nbsp;次登陆</p>
                                                <p><asp:Label ID="lbLastLoginDateTime" runat="server" Text=""/></p>
                                                <p>您的当前积分为&nbsp;<asp:Label ID="lbIntegral" runat="server" Text=""/></p>
                                                <p><asp:Label ID="Label1" runat="server" Text=""/></p>
                                	        </div>
                            	        </div>
							        </div>
						        </div>
					        </div>
                        </div>
					        <div class="btlist">
                                <div class="btctn">
                                    <ul>
                                      <LI class="wd1"><a href="#"><IMG title=产品一 alt=产品一 src="images/icon1.jpg"></a> </li>
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
			        </div>
    	        </div>
            
            <div class="scrollctn bg2">
                <div class="btmkg"><a href="Help.aspx">网站介绍</a>· <a href="Help.aspx" rel="nofollow">帮助中心</a>· <a href="#" rel=nofollow>法律声明</a>· <a href="#">论坛</a>· <a href="#">开放平台</a>· <a href="#">招聘</a>· <a href="#">客服QQ</a>
                <br><br>陕<font class="enwz1">ICP</font>备<font class=enwz1>12009470</font>号 <a href="#" rel=nofollow>
                <font class="enwz">©2012-2017</font> <font class="enwz">Zi Yang Soft</font>子杨软件</a> 
                </div>
            </div>
        </div>
    </form>
</body>
</html>
