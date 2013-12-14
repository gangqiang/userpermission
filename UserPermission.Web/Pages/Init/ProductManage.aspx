<%@ Page Title="项目产品管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ProductManage.aspx.cs" Inherits="UserPermission.Web.Pages.Init.ProductManage" %>

<%@ Register Src="../../UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>项目产品管理</legend>
        <table class="table">
            <tr>
                <td>
                    所属项目：<asp:DropDownList ID="ddlProject" runat="server">
                    </asp:DropDownList>
                    &nbsp; 产品名称：
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="text"></asp:TextBox>
                    &nbsp;&nbsp;产品状态：
                    <asp:DropDownList ID="ddlStauts" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" CssClass="button" runat="server" Text=" 搜 索 "
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <input id="btnAddProduct" type="button" class="funbutton" value="增加新产品" />
    <table class="listtable">
        <colgroup>
            <col width="220px" />
            <col width="130" class="ac" />
            <col />
            <col width="90" class="ac" />
            <col width="110" />
        </colgroup>
        <thead>
            <tr>
                <td>
                    产品名称
                </td>
                <td>
                    所属项目
                </td>
                <td>
                    产品描述
                </td>
                <td>
                    产品状态
                </td>
                <td>
                    操作
                </td>
            </tr>
        </thead>
        <asp:Repeater ID="rptProductInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem,"ProductName",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem,"ProjectName",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "ProductDesc", null)%>
                    </td>
                    <td>
                        <%#UserPermission.Utils.EnumPlus.GetEnumDescription(typeof(UserPermission.Model.ShareEnum.ProjectStatus), DataBinder.Eval(Container.DataItem, "PRODUCTFLAG", null))%>
                    </td>
                    <td>
                        <a href="###" name="edit" id="<%#UserPermission.Bll.Enc.Encrypt(DataBinder.Eval(Container.DataItem, "PRODUCTID", null),UrlEncKey)%>">
                            修改</a><%#GetOperateStr(DataBinder.Eval(Container.DataItem, "PRODUCTID", null), DataBinder.Eval(Container.DataItem, "ProductFlag", null))%>
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
    <asp:HiddenField ID="hidProductId" runat="server" />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <asp:LinkButton ID="lnkStopUse" runat="server" OnClick="lnkStopUse_Click">
    </asp:LinkButton>
    <script type="text/javascript">
        $(function () {
            $("a[name='edit']").each(function () {
                Tw.Win.Open({ oid: this, cover: true, rang: true, width: 720, height: 580, title: '修改产品信息', page: "ProductAdd.aspx?id=" + this.id });
            });

            $("#btnAddProduct").bind("click", function () {
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 720, height: 580, title: '增加新产品', page: "ProductAdd.aspx" });
            });

        });

        function StopUse(id, status) {
            var tip = $("#" + id + status).text();
            if (confirm("您确认要" + tip + "此产品吗？")) {
                $("#<%=hidProductId.ClientID %>").val(id);
                $("#<%=hidStatus.ClientID%>").val(status);
                __doPostBack('<%=lnkStopUse.ClientID.Replace('_','$')%>', '');
            }

        }
        
        
    </script>
</asp:Content>
