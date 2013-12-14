<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserPermission.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录到统一权限管理系统</title>
    <link href="<%=ResolveUrl("~/Resource/Styles/Site.css") %>" rel="stylesheet" type="text/css" />
    <link href="<% =ResolveUrl("~/Resource/Scripts/lhgdialog/lhgdialog.css")%>" rel="stylesheet"
        type="text/css" />
    <link href=" <%=ResolveUrl("~/Resource/Scripts/My97DatePicker/skin/WdatePicker.css") %>"
        rel="stylesheet" type="text/css" />
    <link href="<% =ResolveUrl("~/Resource/Styles/validator.css")%>" rel="stylesheet"
        type="text/css" />
    <script src="<%=ResolveUrl("~/Resource/Scripts/jquery-1.7.1.min.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Resource/Scripts/formValidator_min.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Resource/Scripts/formValidatorRegex.js") %>" type="text/javascript"></script>
    <style type="text/css">
        .LBODY
        {
            padding-right: 32px;
            margin-top: 40px;
            padding-left: 32px;
            font-size: 13px;
            background: #b6b7bc;
            padding-bottom: 32px;
            color: #000;
            padding-top: 32px;
            font-family: Verdana;
        }
        #LMain
        {
            border-right: #bbb 1px solid;
            border-top: #bbb 1px solid;
            background: #fff;
            padding-bottom: 32px;
            border-left: #bbb 1px solid;
            width: 300px;
            padding-top: 20px;
            border-bottom: #bbb 1px solid;
            text-align: left;
            padding-left: 60px;
            padding-right: 50px;
        }
        DIV#LHeading
        {
            padding-right: 0px;
            padding-left: 0px;
            font-weight: bold;
            font-size: 150%;
            padding-bottom: 15px;
            margin: 0px;
            color: #904;
            padding-top: 0px;
            font-family: arial;
        }
        .MyLabel
        {
            display: block;
            font-weight: bold;
            font-size: 12px;
            margin: 6px 0px 2px;
            text-transform: uppercase;
            color: #999;
        }
        INPUT.Textbox
        {
            padding-right: 2px;
            padding-left: 2px;
            padding-bottom: 2px;
            width: 180px;
            padding-top: 2px;
            font-family: verdana, arial, sans-serif;
            height: 18px;
        }
        .Button
        {
            font-weight: bold;
            padding-bottom: 3px;
            color: #904;
            padding-top: 5px;
            font-size: 16px;
            width: 100px;
            font-family: Verdana;
        }
        .error_tips
        {
            display: block;
            background: #F00;
            color: #FFF;
            padding: 3px;
        }
    </style>
</head>
<body class="LBODY">
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div align="center">
            <div id="LMain">
                <div id="LHeading">
                    登录到统一权限管理系统</div>
                <label class="MyLabel">
                    公司编码</label>
                <asp:TextBox ID="txtCompanyCode" MaxLength="9" CssClass="Textbox" runat="server"
                    Width="80" ToolTip="请输入公司编码" Text="117"></asp:TextBox>
                <label class="MyLabel">
                    用户名</label>
                <asp:TextBox ID="txtUserName" MaxLength="50" CssClass="Textbox" runat="server" ToolTip="请输入用户名"></asp:TextBox>
                <label class="MyLabel">
                    密码</label>
                <asp:TextBox ID="txtPassWord" MaxLength="50" TextMode="Password" runat="server" ToolTip="请输入密码"
                    CssClass="Textbox"></asp:TextBox>
                <div>
                    <asp:Button ID="btnLogin" runat="server" CssClass="Button" Text="登  录" Style="margin-top: 8px"
                        OnClick="btnLogin_Click" OnClientClick="return checkSubmit();" />
                </div>
            </div>
        </div>
    </div>
    </form>
    <script src="<%=ResolveUrl("~/Resource/Scripts/My97DatePicker/WdatePicker.js") %>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Resource/Scripts/lhgdialog/lhgdialog.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Resource/Scripts/TZWidget.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $('#txtUserName').focus();
        });

        function checkSubmit() {

            if ($.trim($("#txtCompanyCode").val()).length == 0) {
                alert("请输入公司编码！");
                $("#txtCompanyCode").focus();
                return false;
            }

            if ($.trim($("#txtUserName").val()).length == 0) {
                alert("请输入用户名！");
                $("#txtUserName").focus();
                return false;
            }

            if ($.trim($("#txtPassWord").val()).length == 0) {
                alert("请输入密码！");
                $("#txtPassWord").focus();
                return false;
            }
        }
    </script>
</body>
</html>
