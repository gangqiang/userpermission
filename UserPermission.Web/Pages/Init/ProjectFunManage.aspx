<%@ Page Title="项目功能菜单管理" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ProjectFunManage.aspx.cs" Inherits="UserPermission.Web.Pages.Init.ProjectFunManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .div_head
        {
            line-height: 30px;
            width: 100%;
            border: 1px solid #CCC;
            margin-top: 5px;
            background-color: #F7F8FC;
            background-image: url('../../Resource/images/searchthead.gif');
            background-repeat: repeat-x;
        }
        #div_tree
        {
            line-height: 30px;
            width: 100%;
            border-left: 1px solid #CCC;
            border-right: 1px solid #CCC;
            font-size: 14px;
        }
        .fontRed
        {
            color: Red;
        }
        .fontBlue
        {
            color: Blue;
        }
    </style>
    <script type="text/javascript" language="javascript">
        window.Tree = [];

        // 构造函数
        function TreeView(Text) {
            this.id = window.Tree.length;
            window.Tree[this.id] = this;
            this.nodes = {
                0: {
                    id: 0,
                    superID: -1,
                    Text: Text,
                    childrenNodes: new Array()
                }
            };
        }

        //创建一个TreeView的原型
        var tr = TreeView.prototype;

        //增加节点
        //key--当前节点的id
        //superid--父节点id
        //text--节点内容 包含部门菜单名称,地址,排序，描述。
        tr.add = function (key, superid, text) {
            this.nodes[key] = {
                id: key,
                superID: superid,
                Text: text,
                childrenNodes: new Array()
            };
            var ch = this.getNode(superid).childrenNodes;
            ch[ch.length] = this.nodes[key];
        }


        //获得某个节点,通过键
        tr.getNode = function (key) {
            if (typeof this.nodes[key] != "undefined") {
                return this.nodes[key];
            }
            return null;
        }


        //获取一个节点的父节点
        tr.getParent = function (key) {
            if (this.getNode(key) != null) {
                var skey = this.getNode(key).superID;
                if (typeof this.nodes[skey] != "undefined") {
                    return this.getNode(skey);
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        //判断是否有孩子
        tr.hasChildren = function (key) {
            return this.getNode(key).childrenNodes.length > 0;
        }

        //获取根
        tr.getRoot = function (key) {
            if (key == 0) {
                return this.getNode(key);
            }
            var par = this.getParent(key);
            if (this.getNode(key).id == 0) {
                return this.getNode(key);
            }
            else {
                return this.getRoot(par.id);
            }
        }


        //生成一个节点
        tr.drawNode = function (key) {
            var arrayHtml = new Array();
            var node = this.getNode(key);
            var rootID = this.getRoot(key);
            var hc = this.hasChildren(key);
            arrayHtml.push('<div style="border-bottom:1px solid #CCC; height:26px;">');
            arrayHtml.push("<div style='float:left;width:19%;height:26px;border-right:1px solid #CCC;'>");
            arrayHtml.push(this.drawIndent(key));
            if (this.hasChildren(key) == true) {
                arrayHtml.push('<image style="float:left;" id="image' + key + '" style="border:0px" height="18px" width="18px" id="expan' + key + '" src=' + '../../Resource/images/tree/folderopen.gif' + ' alt="">');
            }
            else {
                arrayHtml.push('<image style="float:left;" id="image' + key + '" style="border:0px" height="18px" width="18px" id="expan' + key + '" src=' + '../../Resource/images/tree/folder.gif' + ' alt="">');
            }
            arrayHtml.push('<div style="float:left;line-height:26px;height:26px;" id="span' + key + '" >' + node.Text[0] + '</div>');
            arrayHtml.push("</div>");
            arrayHtml.push("<div style='float:left; width:40%;text-align:left;border-right:1px solid #CCC;'>");
            arrayHtml.push("&nbsp;" + node.Text[1]);
            arrayHtml.push("</div>");
            arrayHtml.push("<div style='float:left; width:20%;text-align:left;border-right:1px solid #CCC;' title=" + node.Text[3] + ">");
            arrayHtml.push("&nbsp;&nbsp;" + node.Text[3].substring(0, 12));
            arrayHtml.push("</div>");
            arrayHtml.push("<div style='float:left; width:8%;text-align:center;border-right:1px solid #CCC;'>");
            arrayHtml.push(node.Text[2]);

            arrayHtml.push("</div>");
            arrayHtml.push("<div style='float:left; width:12%;text-align:center;'>");
            arrayHtml.push("<a href='###' onclick=\"Add('" + node.Text[4] + "')\"   >" + "添加" + "</a>&nbsp;");
            arrayHtml.push("<a href='###' onclick=\"Edit('" + node.Text[4] + "')\"  style='margin-left:5px;'>" + "修改" + "</a>&nbsp;");
            arrayHtml.push("<a href='###' onclick=\"Del('" + node.Text[4] + "')\" style='margin-left:5px;'>" + "删除" + "</a>");
            arrayHtml.push("</div>");
            arrayHtml.push("</div>");

            if (hc) {
                var io = key == rootID.id;
                arrayHtml.push('<div id="container' + key + '" style="display:' + (io ? 'none' : '') + '">');
                arrayHtml.push(this.drawChild(key));
                arrayHtml.push('</div>');
            }

            return arrayHtml.join("").toString();
        }


        //画缩进
        tr.drawIndent = function (key) {
            var s = ''
            var ir = this.getRoot(key).id == key;
            var node = this.getNode(key);
            var hc = this.hasChildren(key);
            if (this.getParent(key) != null) {
                s = hc ? '<a href="javascript:void window.Tree[' + this.id + '].openHandler(' + key + ');"><image id="handler' + key + '" style="border:0px;float:left;" height="18px" width="18px" src =' + "../../Resource/images/tree/open.gif" + ' alt=""></a>' : '<div style="height:26px;width:20px;float:left;"></div>';
            }
            var p = this.getParent(key);
            while (p != null) {
                if (this.getParent(p.id) == null) {
                    break;
                }
                s = ('<div style="height:26px;width:20px;float:left;"></div>') + s;
                p = this.getParent(p.id);
            }
            return s;
        }


        //画一个节点的孩子节点
        tr.drawChild = function (key) {
            var node = this.getNode(key);
            var html = "";
            for (var i = 0; i < node.childrenNodes.length; i++) {
                html += this.drawNode(node.childrenNodes[i].id);
            }

            return html;
        }


        //处理点击事件 
        tr.openHandler = function (key) {
            if (this.hasChildren(key) == false) {
                return;
            }
            var handler = document.getElementById("handler" + key);
            var container = document.getElementById("container" + key);
            var fimage = document.getElementById("image" + key);
            if (container.style.display == '') {
                container.style.display = 'none';
                handler.src = "../../Resource/images/tree/close.gif";
                fimage.src = "../../Resource/images/tree/folder.gif";
            }
            else {
                container.style.display = '';
                handler.src = "../../Resource/images/tree/open.gif";
                fimage.src = "../../Resource/images/tree/folderopen.gif";
            }
        }

        // 生成树
        tr.toString = function (key) {
            return this.drawChild(0);
        }

      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>项目功能菜单管理</legend>
        <table class="table">
            <tr>
                <td>
                    所属项目：<asp:DropDownList ID="ddlProject" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </fieldset>
    <div>
        <table style="margin: 0 10px; width: 99.5%;">
            <tr>
                <td style="width: 80%;">
                    <strong class="highlight">*</strong> <font style="font-weight: bold;">注：</font>新增主菜单请单击右侧的“新增主菜单”按钮。
                </td>
                <td style="text-align: right;">
                    <input id="btnAddMainMenu" type="button" value="新增主菜单" class="funbutton" />
                </td>
            </tr>
        </table>
        <div class="div_head">
            <div style="float: left; width: 19%; border-right: 1px solid #CCC; text-align: center;
                color: black; padding-top: 2px; font-weight: bold;">
                功能名称
            </div>
            <div style="float: left; width: 40%; text-align: center; border-right: 1px solid #CCC;
                color: black; padding-top: 2px; font-weight: bold;">
                链接地址
            </div>
            <div style="float: left; width: 20%; text-align: center; border-right: 1px solid #CCC;
                color: black; padding-top: 2px; font-weight: bold;">
                功能描述
            </div>
            <div style="float: left; width: 8%; text-align: center; border-right: 1px solid #CCC;
                color: black; padding-top: 2px; font-weight: bold;">
                排序值
            </div>
            <div style="float: left; width: 12%; text-align: center; color: black; padding-top: 2px;
                font-weight: bold;">
                操作
            </div>
        </div>
        <div id="div_tree">
        </div>
    </div>
    <script type="text/javascript">

        function Add(id) {
            Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 520, height: 350, title: '新增项目功能菜单', page: "ProjectFunAdd.aspx?type=add&fmid=" + id + "&projectid=" + $("#<%=ddlProject.ClientID %>").val() });
        }
        function Edit(id) {
            Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 520, height: 350, title: '修改项目功能菜单', page: "ProjectFunAdd.aspx?type=edit&fmid=" + id + "&projectid=" + $("#<%=ddlProject.ClientID %>").val() });
        }

        //删除 
        function Del(id) {
            if (window.confirm("你确定要删除吗？")) {


                //判断能否被删除
                var url = "../../Handler/AjaxHandler.ashx?action=IfFunDel&fmid=" + id + "&s=" + Math.random();
                $.get(url, function (msg) {


                    if (msg == "0") {
                        $.get("../../Handler/AjaxHandler.ashx?action=DelFunMenu&fmid=" + id + "&s=" + Math.random(), function (msg2) {

                            if (msg2 == "0") {
                                alert("删除成功！");
                                LoadFun();
                            }
                            else {
                                alert("删除失败，请重试！");
                            }
                        });
                    }

                    else {
                        alert("此功能下面还有子功能，无法删除！");
                        return;
                    }

                });

            }
        }

        function LoadFun() {

            var tree = new TreeView('funManager');
            $("#div_tree").html("");
            var pid = $("#<%=ddlProject.ClientID %>").val();
            var url = "../../Handler/AjaxHandler.ashx?action=LoadProjectFunMenu&projectid=" + pid + "&s=" + Math.random();
            $.get(url, function (msg) {

                if (msg != "[") {

                    var arr = eval(msg);
                    var item;
                    for (var i = 0; i < arr.length; i++) {
                        item = arr[i];
                        tree.add(item[0], item[1], item[2]);
                    }
                    var treestring = tree.toString();
                    $("#div_tree").html(treestring);
                }
                else {
                    $("#div_tree").html("&nbsp;&nbsp;&nbsp;&nbsp;此项目尚未录入功能菜单！");
                    $("#div_tree").attr("style", "border-bottom: 1px solid #CCC;");
                }

            });

        }

        $(function () {

            //加载功能菜单列表
            LoadFun();

            $("#<%=ddlProject.ClientID %>").change(function () { LoadFun(); });

            $("#btnAddMainMenu").bind("click", function () {
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 520, height: 330, title: '新增项目主菜单', page: "ProjectFunAdd.aspx?type=main&projectid=" + $("#<%=ddlProject.ClientID %>").val() });
            });

        });


        $(".edit").each(function () {
            $(this).bind("click", function () {
                var id = $(this).attr("id");
                var tip = (id.indexOf('e') >= 0 ? "修改" : "新增");
                var type = (id.indexOf('e') >= 0 ? "add" : "edit");
                id = id.replace('e', '');
                Tw.Win.OpenWithParam({ oid: this, cover: true, rang: true, width: 520, height: 350, title: tip + '项目功能菜单', page: "ProjectFunAdd.aspx?type=edit&fmid=" + id + "&projectid=" + $("#<%=ddlProject.ClientID %>").val() });
            });
        });


    </script>
</asp:Content>
