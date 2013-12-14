<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunMenuEdit.aspx.cs" Inherits="UserPermission.Web.Pages.Service.FunMenuEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="listtable">
        <colgroup>
            <col width="90" />
            <col />
        </colgroup>
        <tr>
            <td class="rhead">
                <span class="require">*</span>菜单名称：
            </td>
            <td>
                <asp:TextBox ID="txtFMName" runat="server" CssClass="text" MaxLength="50"></asp:TextBox>
                <span id="txtFMNameTip"></span>
            </td>
        </tr>
     <%--   <tr id="trPageUrl" style="display:none;">
            <td class="rhead">
                链接地址：
            </td>
            <td>
                <asp:TextBox ID="txtFMPageUrl" Enabled="false" CssClass="text" MaxLength="50" runat="server"
                    Width="260px"></asp:TextBox>
                <span id="txtFMPageUrlTip"></span>
            </td>
        </tr>--%>
        <tr>
            <td class="rhead">
                <span class="require">*</span>排序值：
            </td>
            <td>
                <asp:TextBox ID="txtFMSortNum" Width="30" CssClass="text" MaxLength="9" runat="server"></asp:TextBox>
                <span id="txtFMSortNumTip"></span>&nbsp;<span class="tiptext">输入正整数，越大越靠前</span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                功能描述：
            </td>
            <td>
                <asp:TextBox ID="txtFMDesc" CssClass="text" TextMode="MultiLine" runat="server" Height="36px"
                    Width="279px" MaxLength="100"></asp:TextBox>
                <span id="txtFMDescTip"></span>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="ac">
                <asp:Button ID="btnSave" runat="server" Text=" 保 存 " CssClass="submitButton" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        $(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtFMName").formValidator({ onshow: "请输入菜单名称", onfocus: "请输入菜单名称" }).inputValidator({ min: 1, max: 50, onerror: "菜单名称长度在1-50之间！" });
            $("#txtFMSortNum").formValidator({ onshow: "请输入排序值！", onfocus: "请输入排序值！" }).inputValidator({ min: 1, onerror: "请输入排序值！" }).regexValidator({ regexp: "intege4", datatype: "enum", onerror: "请输入正整数！" })
        });
    </script>
</body>
</html>
