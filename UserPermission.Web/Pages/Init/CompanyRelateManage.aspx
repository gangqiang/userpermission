<%@ Page Title="公司注册管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CompanyRelateManage.aspx.cs" Inherits="UserPermission.Web.Pages.Init.CompanyRelateManage" %>

<%@ Register Src="../../UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>公司注册管理</legend>
        <table class="table">
            <tr>
                <td>
                    &nbsp;公司名称：
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="text"></asp:TextBox>
                    &nbsp;&nbsp;公司类型：
                    <asp:DropDownList ID="ddlCompanyType" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" CssClass="button" runat="server" Text=" 搜 索 "
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <input id="btnCompanyReg" type="button" class="funbutton" value="公司注册" />
    <table class="listtable">
        <colgroup>
            <col />
            <col width="120" class="ac" />
            <col width="110" class="ac" />
            <col width="90" class="ac" />
            <col width="120" class="ac" />
            <col width="160" />
        </colgroup>
        <thead>
            <tr>
                <td>
                    公司名称
                </td>
                <td>
                    公司类型
                </td>
                <td>
                    公司编码
                </td>
                <td>
                    管理员账号
                </td>
                <td>
                    注册日期
                </td>
                <td>
                    操作
                </td>
            </tr>
        </thead>
        <asp:Repeater ID="rptCompanyRelateInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem,"CompanyName",null) %>
                    </td>
                    <td>
                        <%#UserPermission.Utils.EnumPlus.GetEnumDescription(typeof(UserPermission.Model.ShareEnum.CompanyType),DataBinder.Eval(Container.DataItem, "CompanyType", null))%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CompanyCode", null)%>
                    </td>
                    <td>
                        <%# GetAdminStr(DataBinder.Eval(Container.DataItem, "ADMINID", null), DataBinder.Eval(Container.DataItem, "ACCOUNTNAME", null), DataBinder.Eval(Container.DataItem, "COMPANYCODE", null))%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CREATEDATE", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <a href="###" name="edit" id="<%#UserPermission.Bll.Enc.Encrypt( DataBinder.Eval(Container.DataItem, "CID", null),UrlEncKey)%>">
                            修改</a>
                        <%#GetOperateStauts(DataBinder.Eval(Container.DataItem, "CID", null), DataBinder.Eval(Container.DataItem, "STATUS", null),  DataBinder.Eval(Container.DataItem, "ADMINID", null), DataBinder.Eval(Container.DataItem, "COMPANYCODE", null))%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="tdpage" colspan="6">
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
                Tw.Win.Open({ oid: this, cover: true, rang: true, width: 820, height: 560, title: '修改注册信息', page: "CompanyRelateAdd.aspx?cid=" + this.id });
            });

            $("#btnCompanyReg").bind("click", function () {
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width:820, height: 560, title: '公司注册', page: "CompanyRelateAdd.aspx" });
            });

             $("a[name='account']").each(function () {
                var ids=$(this).attr("id");
                ids=ids.split(',');
                var aid=ids[0];
                var ccode=ids[1]; 
                var tip=(aid!="7EFF0AAD1AC6DCFF"?"修改初始账号信息":"开通公司初始账号");
                Tw.Win.Open({ oid: this, cover: true, rang: true, width: 680, height: 510, title:tip , page: "../Service/AccountAdd.aspx?type=init"+"&code="+ccode+"&id="+aid });
            });

        });
         

        function StopUse(id, status) {
            var tip = $("#" + id + status).text();
            if (confirm("您确认要" + tip + "吗？")) {
                $("#<%=hidCId.ClientID %>").val(id);
                $("#<%=hidStatus.ClientID%>").val(status);
                __doPostBack('<%=lnkStopUse.ClientID.Replace('_','$')%>', '');
            }

        }
    </script>
</asp:Content>
