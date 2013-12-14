<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountAdd.aspx.cs" Inherits="UserPermission.Web.Pages.Service.AccountAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="listtable">
        <colgroup>
            <col width="110" />
            <col />
        </colgroup>
        <tr>
            <td class="rhead">
                <span class="require">*</span>账号名称：
            </td>
            <td>
                <asp:TextBox ID="txtAccountName" runat="server" MaxLength="50" Width="100" CssClass="text"></asp:TextBox>
                <span id="txtAccountNameTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>真实姓名：
            </td>
            <td>
                <asp:TextBox ID="txtRealName" runat="server" MaxLength="50" Width="90" CssClass="text"></asp:TextBox>
                <span id="txtRealNameTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>登录密码：
            </td>
            <td>
                <asp:TextBox ID="txtPwd" runat="server" MaxLength="50" TextMode="Password" Width="130"
                    CssClass="text"></asp:TextBox>
                <span id="txtPwdTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>确认密码：
            </td>
            <td>
                <asp:TextBox ID="txtPwd2" runat="server" MaxLength="50" TextMode="Password" Width="130"
                    CssClass="text"></asp:TextBox>
                <span id="txtPwd2Tip"></span>
            </td>
        </tr>
        <tr id="trRoles" runat="server">
            <td class="rhead">
                所属角色：
            </td>
            <td id="tdRoles" runat="server">
            </td>
        </tr>
        <tr>
            <td class="rhead">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="txtLinkPhone" MaxLength="50" runat="server" Width="130" CssClass="text"></asp:TextBox>
                <span id="txtLinkPhoneTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>邮箱：
            </td>
            <td>
                <asp:TextBox ID="txtEmail" MaxLength="50" runat="server" Width="160" CssClass="text"></asp:TextBox>
                <span id="txtEmailTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>状态：
            </td>
            <td>
                <asp:RadioButtonList ID="rbtAccountStatus" runat="server" RepeatLayout="Flow" RepeatColumns="3">
                </asp:RadioButtonList>
                <span id="RoleTip"></span>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="ac">
                <asp:Button ID="btnSave" runat="server" CssClass="submitButton" Text=" 保 存 " OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidAccountId" runat="server" />
    <asp:HiddenField ID="hidEmail" runat="server" />
    <asp:HiddenField ID="hidCompanyCode" runat="server" />
    </form>
    <script type="text/javascript">
        $(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtAccountName").formValidator({ onshow: "请输入账号名称", onfocus: "请输入账号名称" }).inputValidator({ min: 1, max: 20, onerror: "账号名称长度在1-20之间！" });
            $("#txtRealName").formValidator({ onshow: "请输入真实姓名", onfocus: "请输入真实姓名" }).inputValidator({ min: 1, max: 10, onerror: "真实姓名长度在1-10之间！" });
            $("#txtPwd").formValidator({ onshow: "请输入登录密码", onfocus: "请输入登录密码" }).inputValidator({ min: 6, max: 12, onerror: "密码长度在6-12之间！" });
            $("#txtPwd2").formValidator({ onshow: "请确认登录密码", onfocus: "请确认登录密码" }).inputValidator({ min: 6, max: 12, onerror: "密码长度在6-12之间！" })
            .compareValidator({ desid: "txtPwd", operateor: "=", onerror: "两次密码不一致,请确认" });

            $("#txtEmail").formValidator({ onshow: "请输入邮箱", onfocus: "邮箱5-100个字符", oncorrect: "恭喜你,你输对了", defaultvalue: $("#hidEmail").val() }).inputValidator({ min: 5, max: 100, onerror: "你输入的邮箱长度不正确,请确认！" }).regexValidator({ regexp: "^([\\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$", onerror: "邮箱格式不正确！" });

            //角色复选框的验证，至少选一个
            $(":checkbox[name='rbtAccountStatus']").formValidator({ tipid: "RoleTip", onshow: "请选择账号角色（至少选一个）", onfocus: "至少选择1个角色", oncorrect: "验证通过" }).inputValidator({ min: 1, onerror: "至少需要选择一个角色！" });


            $("#txtAccountName").bind("blur", function () {

                var accountid = $.trim($("#hidAccountId").val());
                var newname = $.trim($("#txtAccountName").val());
                var companycode = $.trim($("#hidCompanyCode").val());
                if (newname.length > 0) {
                    $.get("../../Handler/AjaxHandler.ashx", { action: "ValidateAccountName", AccountName: escape(newname), CompanyCode: companycode, AccountId: accountid, s: Math.random() }, function (data) {
                        if (data == "1") {
                            alert("已存在相同账号名称，请选用其他账号名称!");
                            $("#txtAccountName").val("");
                            $("#txtAccountName").focus();
                        }
                    });
                }

            });

        });

        function CheckSubmit() {
            if ($("input[name='role']:checked").length == 0) {
                alert("账号所属角色至少选一个！");
                return false;
            }
        }
    </script>
</body>
</html>
