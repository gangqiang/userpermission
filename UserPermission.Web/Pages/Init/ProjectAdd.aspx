<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectAdd.aspx.cs" Inherits="UserPermission.Web.Pages.Init.ProjectAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="listtable">
        <colgroup>
            <col width="120px" />
            <col />
        </colgroup>
        <tr>
            <td class="rhead">
                <span class="require">*</span>项目名称：
            </td>
            <td>
                <asp:TextBox ID="txtProjectName" MaxLength="50" CssClass="text" runat="server"></asp:TextBox>
                <span id="txtProjectNameTip">
                </span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>使用接口密钥：
            </td>
            <td>
                <asp:Label ID="lblProjectKey" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                项目描述：
            </td>
            <td>
                <asp:TextBox ID="txtProjectDesc" TextMode="MultiLine" CssClass="text" runat="server"
                    Height="39px" Width="272px"></asp:TextBox>
                <span id="txtProjectDescTip">
                </span>
            </td>
        </tr>
        <tr>
            <td class="ac" colspan="3">
                <asp:Button ID="btnSave" runat="server" CssClass="submitButton" Text=" 保 存 " OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        $(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtProjectName").formValidator({ onshow: "请输入项目名称", onfocus: "请输入项目名称" }).inputValidator({ min: 1, max: 50, onerror: "项目名称长度在1-50之间！" });
            $("#txtProjectDesc").inputValidator({ min: 0, max: 100, onerror: "项目描述长度请控制在100字以内！" });
        });
    </script>
</body>
</html>
