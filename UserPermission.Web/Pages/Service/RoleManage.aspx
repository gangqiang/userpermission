<%@ Page Title="角色管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="RoleManage.aspx.cs" Inherits="UserPermission.Web.Pages.Service.RoleManage" %>

<%@ Register Src="../../UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>角色管理</legend>
        <table class="table">
            <tr>
                <td>
                    &nbsp;角色名称：
                    <asp:TextBox ID="txtRoleName" Width="200" runat="server" CssClass="text"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Label ID="lblProject" runat="server" Text="所属项目："></asp:Label><asp:DropDownList
                        ID="ddlProject" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" CssClass="button" runat="server" Text=" 搜 索 "
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <input id="btnAddRole" type="button" class="funbutton" value="新增角色" />
    <table class="listtable">
        <colgroup>
            <col width="200" class="ac" />
            <col />
            <col width="100" class="ac" />
            <col width="120" class="ac" />
            <col width="100" class="ac" />
        </colgroup>
        <thead>
            <tr>
                <td>
                    角色名称
                </td>
                <td>
                    角色描述
                </td>
                <td>
                    创建人
                </td>
                <td>
                    创建日期
                </td>
                <td>
                    操作
                </td>
            </tr>
        </thead>
        <asp:Repeater ID="rptRoleInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem,"ROLENAME",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem,"ROLEDESC",null) %>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "REALNAME", null)%>
                    </td>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "CREATEDATE", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <a href="###" name="edit" id="<%#UserPermission.Bll.Enc.Encrypt(DataBinder.Eval(Container.DataItem, "ROLEID", null),UrlEncKey)%>">
                            修改</a>
                        <%#GetOperateStauts(DataBinder.Eval(Container.DataItem, "ROLEID", null), DataBinder.Eval(Container.DataItem, "STATUS", null))%>
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
                Tw.Win.Open({ oid: this, cover: true, rang: true, width: 780, height: 560, title: '修改角色信息', page: "RoleAdd.aspx?id=" + this.id });
            });

            $("#btnAddRole").bind("click", function () {
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width:780, height: 560, title: '新增角色', page: "RoleAdd.aspx" });
            });

        });
         

        function StopUse(id, status) {
            var tip = $("#" + id + status).text();
            if (confirm("您确认要" + tip + "此角色吗？")) {

             //判断此角色下是否有账号
             $.get('../../Handler/AjaxHandler.ashx',{action:"IfRoleUse",RoleId:id,s:Math.random()},function(data){
               if(data=="1") {
                 alert("此角色下还有账号信息，无法删除！");    
                 return;           
               }
               else{
                  $("#<%=hidCId.ClientID %>").val(id);
                  $("#<%=hidStatus.ClientID%>").val(status);
                  __doPostBack('<%=lnkStopUse.ClientID.Replace('_','$')%>', '');
               }

             });

           }

        }
    </script>
</asp:Content>
