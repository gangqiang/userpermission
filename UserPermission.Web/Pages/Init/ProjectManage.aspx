<%@ Page Title="使用项目管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ProjectManage.aspx.cs" Inherits="UserPermission.Web.Pages.ProjectManage" %>

<%@ Register Src="../../UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>使用项目管理</legend>
        <table class="table">
            <tr>
                <td>
                    &nbsp;项目名称：
                    <asp:TextBox ID="txtProjectName" runat="server" CssClass="text"></asp:TextBox>
                    &nbsp;&nbsp;项目状态：
                    <asp:DropDownList ID="ddlStauts" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" CssClass="button" runat="server" Text=" 搜 索 "
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <input id="btnAddProject" type="button" class="funbutton" value="注册新项目" />
    <table class="listtable">
        <colgroup>
            <col />
            <col width="320" class="ac" />
            <col width="100" class="ac" />
            <col width="90" class="ac" />
            <col width="110" />
        </colgroup>
        <thead>
            <tr>
                <td>
                    项目名称
                </td>
                <td>
                    使用接口密钥
                </td>
                <td>
                    开通日期
                </td>
                <td>
                    项目状态
                </td>
                <td>
                    操作
                </td>
            </tr>
        </thead>
        <asp:Repeater ID="rptProjectInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td title="<%#DataBinder.Eval(Container.DataItem,"ProjectRemark",null) %>">
                        <%#DataBinder.Eval(Container.DataItem,"ProjectName",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "APISERVICEKEY", null)%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CREATEDATE", "{0:yyyy-MM-dd}")%>
                    </td>
                    <%-- <td>
                        <%#DataBinder.Eval(Container.DataItem, "PROJECTREMARK", null)%>
                    </td>--%>
                    <td>
                        <%#UserPermission.Utils.EnumPlus.GetEnumDescription(typeof(UserPermission.Model.ShareEnum.ProjectStatus), DataBinder.Eval(Container.DataItem, "STATUS", null))%>
                    </td>
                    <td>
                        <a href="###" name="edit" id="<%#UserPermission.Bll.Enc.Encrypt( DataBinder.Eval(Container.DataItem, "PROJECTID", null),UrlEncKey)%>">
                            修改</a><%#GetOperateStr(DataBinder.Eval(Container.DataItem, "PROJECTID", null),DataBinder.Eval(Container.DataItem, "STATUS", null)) %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="tdpage" colspan="5">
                <uc1:PageBar ID="PageBar1" runat="server" OnPageChange="PageBar_PageChange" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <asp:LinkButton ID="lnkStopUse" runat="server" OnClick="lnkStopUse_Click">
    </asp:LinkButton>
    <script type="text/javascript">
        $(function () {
            $("a[name='edit']").each(function () {
                Tw.Win.Open({ oid: this, cover: true, rang: true, width: 560, height: 300, title: '修改项目信息', page: "ProjectAdd.aspx?id=" + this.id });
            });

            $("#btnAddProject").bind("click", function () {
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 560, height: 300, title: '注册新项目', page: "ProjectAdd.aspx" });
            });

        });

        function StopUse(id, status) {
            var tip = $("#" + id + status).text();
            if (confirm("您确认要" + tip + "此项目吗？")) {
                $("#<%=hidProjectId.ClientID %>").val(id);
                $("#<%=hidStatus.ClientID%>").val(status);
                __doPostBack('<%=lnkStopUse.ClientID.Replace('_','$')%>', '');
            }

        }
        
    </script>
</asp:Content>
