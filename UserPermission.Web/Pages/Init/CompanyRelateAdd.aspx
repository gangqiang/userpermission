<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyRelateAdd.aspx.cs"
    Inherits="UserPermission.Web.Pages.Init.CompanyRelateAdd" %>

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
                <span class="require">*</span>公司类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompanyType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rhead">
                <span class="require">*</span>公司名称：
            </td>
            <td>
                <asp:TextBox ID="txtCompanyName" CssClass="text" MaxLength="50" runat="server" Width="230px"></asp:TextBox>
                <span id="txtCompanyNameTip"></span>
            </td>
        </tr>
        <tr id="trCompany">
            <td class="rhead">
                <span class="require">*</span>选择公司：
            </td>
            <td>
                <div id="tdCompany" style="height: 100px; overflow-x: hidden; overflow-y: scroll;">
                </div>
            </td>
        </tr>
        <tr id="trGroup">
            <td class="rhead">
                <span class="require">*</span>选择分组：
            </td>
            <td>
                <div id="tdGroup" style="height: 100px; overflow-x: hidden; overflow-y: scroll;">
                </div>
            </td>
        </tr>
    </table>
    <table class="listtable">
        <tr>
            <td class="lhead" colspan="2">
                开通项目和产品
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 200px; overflow-x: hidden; overflow-y: auto;">
                    <asp:DataList ID="dlProject" runat="server" RepeatLayout="Flow" OnItemDataBound="dlProject_ItemDataBound">
                        <ItemTemplate>
                            <table class="listtable" style="width: 98%;">
                                <colgroup>
                                    <col width="150" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td class="rhead">
                                        <input style="display: none;" type="checkbox" name="project" id="<%#DataBinder.Eval(Container.DataItem,"ProjectId",null) %>"
                                            value="<%#DataBinder.Eval(Container.DataItem,"ProjectId",null) %>" <%#GetChecked(DataBinder.Eval(Container.DataItem,"ProjectId",null),string.Empty) %> /><%#DataBinder.Eval(Container.DataItem,"ProjectName",null) %>：
                                        <asp:HiddenField ID="hidProjectId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ProjectId",null) %>' />
                                    </td>
                                    <td>
                                        <asp:Repeater ID="rptProduct" runat="server">
                                            <ItemTemplate>
                                                <input type="checkbox" name="ppfun" <%#GetChecked(DataBinder.Eval(Container.DataItem,"ProjectId",null),DataBinder.Eval(Container.DataItem,"ProductId",null)) %>
                                                    onclick="Resume(<%#DataBinder.Eval(Container.DataItem,"ProjectId",null) %>,this,<%#DataBinder.Eval(Container.DataItem,"ProductId",null) %>);"
                                                    id="<%#DataBinder.Eval(Container.DataItem,"ProductId",null) %>" class="<%#DataBinder.Eval(Container.DataItem,"ProjectId",null) %>"
                                                    value="<%#DataBinder.Eval(Container.DataItem,"ProductId",null) %>" /><%#DataBinder.Eval(Container.DataItem,"ProductName",null) %>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="ac">
                <asp:Button ID="btnSave" CssClass="submitButton" OnClientClick="return CheckSubmit();"
                    runat="server" Text=" 保 存 " OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidCompanyId" runat="server" />
    <asp:HiddenField ID="hidGroupId" runat="server" />
    <asp:HiddenField ID="hidGroupIdn" runat="server" />
    <asp:HiddenField ID="hidProjects" runat="server" />
    <asp:HiddenField ID="hidProducts" runat="server" />
    </form>
    <script type="text/javascript">
        $(function () {

            $("#trGroup").hide();

            $("#txtCompanyName").bind("blur", function () {
                LoadRelate("LoadCompany", $(this).val());
                if ($("#ddlCompanyType").val() == "2") {
                    LoadRelate("LoadGroup", $(this).val());
                }
            });

            $("#ddlCompanyType").bind("change", function () {

                setState($(this).val());
            });

            setState($("#ddlCompanyType").val());

            $.formValidator.initConfig({ formid: "form1", onerror: function () { alert("您填写的内容没有通过验证，具体错误请看错误提示") } });
            $("#txtCompanyName").formValidator({ onshow: "请输入公司名称", onfocus: "请输入公司名称" }).inputValidator({ min: 1, max: 50, onerror: "公司名称长度在1-50之间！" });

        });

        //显示状态控制
        function setState(val) {
            if (val == "2") {
                $("#trGroup").show();
                $("#trCompany").show();
            }
            else {
                $("#trGroup").hide();
                $("#trCompany").show();
            }

            if (val == "3") {
                $("#trGroup").hide();
                $("#trCompany").hide();
            }

            if (val != "3") {
                LoadRelate("LoadCompany", $("#txtCompanyName").val());
                if (val == "2") {
                    LoadRelate("LoadGroup", $("#txtCompanyName").val());
                }
            }
        }

        //加载相关信息
        function LoadRelate(action, name) {
            if (name.length > 0) {
                $.get("../../Handler/AjaxHandler.ashx?action=" + action + "&ctype=" + $("#ddlCompanyType").val() + "&cname=" + escape(name) + "&s=" + Math.random(), function (msg) {
                    if (msg != "") {
                        Draw(action, name, msg);
                    }
                });
            }
        }

        var i = 0;
        //呈现
        function Draw(action, name, msg) {

            if (i > 0) {
                $("#hidCompanyId").val("");
                $("#hidGroupId").val("");
            }
            var infos = new Array();
            var jsonmaterial = eval(msg);
            var id = "";
            var name = "";
            var groupidn = "";
            var group = "group";
            var cid = $("#hidCompanyId").val();
            var gid = $("#hidGroupId").val();
            var ck = "";
            var event = "";
            var tip = "";

            infos.push("<table class=\"table\" style=\"width:96%;\"><tr>");
            for (var i = 0; i < jsonmaterial.length; i++) {
                groupidn = jsonmaterial[i].GroupIdn;
                if (action == "LoadGroup") {
                    id = jsonmaterial[i].GroupId;
                    tip = jsonmaterial[i].GroupName;
                    name = tip.substring(0, 18);
                    ck = (cid.length > 0 && id == gid) ? "checked='checked'" : "";
                    event = "onclick=\"SetName('group','" + escape(name) + "','" + id + "','" + groupidn + "');\"";
                }
                else {
                    id = jsonmaterial[i].CompanyId;
                    tip = jsonmaterial[i].CompanyName;
                    name = tip.substring(0, 18);
                    group = "company";
                    ck = (cid.length > 0 && id == cid) ? "checked='checked'" : "";
                    event = "onclick=\"SetName('company','" + escape(name) + "','" + id + "','" + groupidn + "');\"";
                }

                infos.push("<td><input type='radio' " + ck + " name='" + group + "' value='" + id + "' id='" + id + "'  " + event + " title='" + tip + "'/> " + name + " &nbsp;</td>");
                if ((i + 1) % 2 == 0) {
                    infos.push("</tr><tr>");
                }
            }

            infos.push("</table>");
            var content = infos.join("");

            if (action == "LoadGroup") {
                content = (content.length > 0 ? content : "<span class='tiptext'>没有找到匹配的分组信息</span>");
                $("#tdGroup").html(content);
            }
            else {
                content = (content.length > 0 ? content : "<span class='tiptext'>没有找到匹配的公司信息</span>");
                $("#tdCompany").html(content);
            }
            i++;
        }

        //控制只能选一个
        function Resume(pid, obj, id) {
            if (obj.checked) {
                $("." + pid + "").each(function () {
                    if ($(this).attr("id") != id) {
                        $(this).removeAttr("checked");
                    }
                });

                $("#" + pid + "").attr("checked", "checked");
            }
            else {
                $("#" + pid + "").removeAttr("checked");
            }

        }

        //选择后赋值
        function SetName(type, cname, id, groupidn) {
            if (type == "company") {
                $("#txtCompanyName").val(unescape(cname));
                $("#hidCompanyId").val(id);
            }
            else {
                $("#hidGroupId").val(id);
            }

            $("#hidGroupIdn").val(groupidn);
        }

        //
        function CheckSubmit() {

            var ctype = $("#ddlCompanyType").val();
            var cid = $("#hidCompanyId").val();
            var gid = $("#hidGroupId").val();
            if (ctype != "3" && cid.length == 0) {
                alert("您没有选择公司信息！");
                return false;
            }
            if (ctype == "2" && gid.legth == 0) {
                alert("您没有选择公司所属分组");
                return false;
            }
            if ($("input[name='ppfun']:checked").length == 0) {
                alert("开通项目和产品至少选一个！");
                return false;
            }
        }
    </script>
</body>
</html>
