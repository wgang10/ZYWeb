<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="syntaxhighlighter_3.0.83/styles/shCore.css" />
    <link type="text/css" rel="stylesheet" href="syntaxhighlighter_3.0.83/styles/shThemeDefault.css" />
    
    <script type="text/javascript" src="syntaxhighlighter_3.0.83/scripts/shCore.js"></script>
    <script type="text/javascript" src="syntaxhighlighter_3.0.83/scripts/shBrushes.js"></script>
    <script type="text/javascript" src="ckeditor/ckeditor.js"></script>
    <link type="text/css" rel="Stylesheet" href="syntaxhighlighter_3.0.83/styles/shCoreDefault.css" />
    <script type="text/javascript">        SyntaxHighlighter.all();</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtcontent" runat="server" TextMode="MultiLine" Height="310px" 
            Width="100%"></asp:TextBox>
        <script type="text/javascript">
            CKEDITOR.replace('<%= txtcontent.ClientID %>', { skin: 'office2003' });
        </script>

        <%--<textarea id="TextArea1" cols="20" rows="2" runat="server"></textarea>
        <script type="text/javascript">
　            CKEDITOR.replace('TextArea1');   
        </script>--%>
        <asp:Button runat="server" Text="Button" OnClick="Unnamed1_Click" />
    </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
