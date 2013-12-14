<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductAdd.aspx.cs" Inherits="UserPermission.Web.Pages.Init.ProductAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="listtable">
        <colgroup>
            <col width="100px" />
            <col />
        </colgroup>
        <tr>
            <td class="rhead">
                <span class="require">*</span> 产品名称：
            </td>
            <td>
                <asp:TextBox ID="txtProductName" runat="server" CssClass="text" MaxLength="50" Width="195px"></asp:TextBox>
                <span id="txtProductNameTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span> 所属项目：
            </td>
            <td>
                <asp:DropDownList ID="ddlProjects" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span> 包含功能：
            </td>
            <td id="divFuns">
            </td>
        </tr>
        <tr>
            <td class="rhead">
                产品描述：
            </td>
            <td>
                <asp:TextBox ID="txtProductDesc" runat="server" CssClass="text" MaxLength="100" TextMode="MultiLine"
                    Height="41px" Width="337px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="ac" colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="submitButton" Text=" 保 存 " OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidId" Value="0" runat="server" />
    </form>
    <script type="text/javascript">
        $(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtProductName").formValidator({ onshow: "请输入产品名称", onfocus: "请输入产品名称" }).inputValidator({ min: 1, max: 50, onerror: "产品名称长度在1-50之间！" });

            //改变项目时加载对应的功能菜单
            $("#ddlProjects").bind("change", function () {
                LoadFunMenu($(this).val());
            });

            LoadFunMenu($("#ddlProjects").val());

        });

        //加载项目下的功能菜单
        function LoadFunMenu(pid) {
            var did = $("#hidId").val();
            var url = "../../Handler/AjaxHandler.ashx?action=LoadFunMenu&pid=" + pid + "&did=" + did + "&s=" + Math.random();
            $.get(url, function (msg) {
                if (msg.length > 0) {
                    $("#divFuns").html(msg);
                }
                else {
                    $("#divFuns").html("尚未设置此项目的功能菜单！");
                }
            });
        }

        function CheckAll(fid) {
            $("." + fid).each(function () {
                if ($("#" + fid).attr("checked")) {
                    $(this).attr("checked", "checked");
                }
                else {
                    $(this).removeAttr("checked");
                }
            });
        }
    </script>
</body>
</html>
