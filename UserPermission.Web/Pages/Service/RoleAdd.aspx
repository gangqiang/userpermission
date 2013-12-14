<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="UserPermission.Web.Pages.Service.RoleAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table class="listtable">
        <colgroup>
            <col width="130" />
            <col />
        </colgroup>
        <tr>
            <td class="rhead">
                <span class="require">*</span>角色名称：
            </td>
            <td>
                <asp:TextBox ID="txtRoleName" runat="server" MaxLength="50" Width="150" CssClass="text"></asp:TextBox>
                <span id="txtRoleNameTip"></span>
            </td>
        </tr>
        <tr id="trProject">
            <td class="rhead">
                所属项目：
            </td>
            <td>
                <asp:DropDownList ID="ddlProject" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>拥有权限：
            </td>
            <td id="tdFuns">
            </td>
        </tr>
        <tr>
            <td class="rhead">
                属于此角色的账号：
            </td>
            <td>
                <div id="divAccounts" style="height: 120px; overflow-x: hidden; overflow-y: scroll;">
                    <asp:CheckBoxList ID="cblAccounts" runat="server" RepeatColumns="3" RepeatLayout="Table">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr id="trGroups" style="display: none;">
            <td class="rhead">
                可查看的车辆分组：
            </td>
            <td>
                <asp:TreeView ID="tvGroups" CssClass="table" runat="server" ShowLines="True" ShowCheckBoxes="All">
                </asp:TreeView>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                角色描述：
            </td>
            <td>
                <asp:TextBox ID="txtRoleDesc" runat="server" TextMode="MultiLine" CssClass="text"
                    Height="41px" Width="337px"></asp:TextBox>
                <span id="txtRoleDescTip"></span>
            </td>
        </tr>
        <tr>
            <td class="ac" colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="submitButton" Text=" 保 存 " OnClientClick="return CheckSubmit();"
                    OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <label id="a">
    </label>
    <asp:HiddenField ID="hidRoleId" runat="server" />
    </form>
    <script type="text/javascript">
        $(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtRoleName").formValidator({ onshow: "请输入角色名称", onfocus: "请输入角色名称" }).inputValidator({ min: 1, max: 50, onerror: "角色名称长度在1-50之间！" });
            LoadCompanyFunMenu();
            $("#ddlProject").bind("change", function () {
                LoadCompanyFunMenu();
            });

            $("#cblAccounts").attr("class", "table").attr("style", "width:96%;");
            $("#tvGroups").attr("style", "height:100px;overflow-x:hidden;overflow-y:scroll;");

            $("table").each(function () {

                if ($(this).attr("style") == "border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px") {
                    $(this).find("td").attr("style","border:0px;"); 
                }
            });

        });

        function CheckSubmit() {
            if ($("input[name='fun']:checked").length == 0) {
                alert("角色拥有的权限至少选一个！");
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
