<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyGroupAdd.aspx.cs"
    Inherits="UserPermission.Web.Pages.Service.CompanyGroupAdd" %>

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
                <span class="require">*</span>分组名称：
            </td>
            <td>
                <asp:TextBox ID="txtGroupName" runat="server" MaxLength="50" Width="150" CssClass="text"></asp:TextBox>
                <span id="txtGroupNameTip"></span>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                包含车辆：
            </td>
            <td>
                <input type="checkbox" id="chkAll" name="chkAll" /><label for="chkAll">全选</label>
                <div id="divVehicles" style="height: 260px; overflow-x: hidden; overflow-y: scroll;">
                    <asp:CheckBoxList ID="cblVehicles" runat="server" RepeatColumns="4" RepeatLayout="Table">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                分组备注：
            </td>
            <td>
                <asp:TextBox ID="txtGroupDesc" runat="server" TextMode="MultiLine" CssClass="text"
                    Height="41px" Width="337px"></asp:TextBox>
                <span id="txtGroupDescTip"></span>
            </td>
        </tr>
        <tr>
            <td class="ac" colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="submitButton" Text=" 保 存 " OnClientClick="return CheckSubmit();"
                    OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">

        $(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtGroupName").formValidator({ onshow: "请输入分组名称", onfocus: "请输入分组名称" }).inputValidator({ min: 1, max: 50, onerror: "分组名称长度在1-50之间！" });
            $("#cblVehicles").attr("class", "table").attr("style", "width:96%;");
            $("#chkAll").click(function () {
                var chk = $(this).attr("checked");
                $(":checkbox").each(function () {
                    if (chk) {
                        $(this).attr("checked", "checked");
                    }
                    else {
                        $(this).removeAttr("checked");
                    }
                });


            });
        });

        function CheckSubmit() {
            if ($(":checked").length == 0) {
                alert("分组包含的车辆至少选一个！");
                return false;
            }
        }

        //加载功能菜单
        function LoadCompanyFunMenu() {
            var pid = $("#hidProjectId").val();
            var cid = $("#hidCompanyId").val();
            var rid = $("#hidRoleId").val();
            var url = "../../Handler/AjaxHandler.ashx";
            $.get(url, { action: "LoadCompanyFunMenu", ProjectId: $("#ddlProject").val(), RoleId: rid, s: Math.random() },
             function (msg) {
                 if (msg.length > 0) {
                     $("#tdFuns").html(msg);
                 }
             });
        }

   
    </script>
</body>
</html>
