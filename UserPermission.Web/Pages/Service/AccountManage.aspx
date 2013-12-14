<%@ Page Title="账号管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AccountManage.aspx.cs" Inherits="UserPermission.Web.Pages.Service.AccountManage" %>

<%@ Register Src="../../UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>账号管理</legend>
        <table class="table">
            <tr>
                <td>
                    &nbsp;账号名称：
                    <asp:TextBox ID="txtAccountName" Width="100" runat="server" CssClass="text"></asp:TextBox>
                    &nbsp;姓名：<asp:TextBox ID="txtRealName" Width="100" runat="server" CssClass="text"></asp:TextBox>
                    &nbsp;&nbsp;账号状态：
                    <asp:DropDownList ID="ddlAccountStatus" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" CssClass="button" runat="server" Text=" 搜 索 "
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <input id="btnAddAccount" type="button" class="funbutton" value="新增账号" />
    <table class="listtable">
        <colgroup>
            <col />
            <col width="90" class="ac" />
            <col width="100" class="ac" />
            <col width="100" class="ac" />
            <col width="60" class="ac" />
            <col width="100" class="ac" />
            <col width="80" class="ac" />
            <col width="90" />
        </colgroup>
        <thead>
            <tr>
                <td>
                    账号名称
                </td>
                <td>
                    真实姓名
                </td>
                <td>
                    联系电话
                </td>
                <td>
                    邮箱
                </td>
                <td>
                    管理员
                </td>
                <td>
                    创建日期
                </td>
                <td>
                    状态
                </td>
                <td>
                    操作
                </td>
            </tr>
        </thead>
        <asp:Repeater ID="rptAccountInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem,"ACCOUNTNAME",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem,"REALNAME",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "LINKPHONE", null)%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "EMAIL", null)%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "ISADMIN", null).EndsWith("1")?"是":"否"%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CREATEDATE", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%#UserPermission.Utils.EnumPlus.GetEnumDescription(typeof(UserPermission.Model.ShareEnum.AccountStatus), DataBinder.Eval(Container.DataItem, "STATUS", null))%>
                    </td>
                    <td>
                        <a href="###" name="edit" id="<%#UserPermission.Bll.Enc.Encrypt(DataBinder.Eval(Container.DataItem, "ACCOUNTID", null),UrlEncKey)%>">
                            修改</a>
                        <%#GetOperateStauts(DataBinder.Eval(Container.DataItem, "ACCOUNTID", null), DataBinder.Eval(Container.DataItem, "STATUS", null), DataBinder.Eval(Container.DataItem, "ISADMIN", null))%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="tdpage" colspan="8">
                <uc1:PageBar ID="PageBar1" runat="server" OnPageChange="PageBar_PageChange" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidCId" runat="server" />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <asp:LinkButton ID="lnkStopUse" runat="server" OnClick="lnkStopUse_Click">
    </asp:LinkButton>
    <script type="text/javascript">
        $(function () {
            $("a[name='edit']").each(function () {
                Tw.Win.Open({ oid: this, cover: true, rang: true, width: 680, height: 560, title: '修改账号信息', page: "AccountAdd.aspx?id=" + this.id });
            });

            $("#btnAddAccount").bind("click", function () {
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 680, height: 560, title: '新增账号', page: "AccountAdd.aspx" });
            });

        });
         

        function StopUse(id, status) {
            var tip = $("#" + id + status).text();
            if (confirm("您确认要" + tip + "此账号吗？")) {
                $("#<%=hidCId.ClientID %>").val(id);
                $("#<%=hidStatus.ClientID%>").val(status);
                __doPostBack('<%=lnkStopUse.ClientID.Replace('_','$')%>', '');
            }

        }
    </script>
</asp:Content>
