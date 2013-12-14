<%@ Control Language="C#" AutoEventWireup="true" Inherits="UC_PageBar" Codebehind="PageBar.ascx.cs" %>
<div runat="server" id="pageCtrl" class="pagectrl">
</div>

<script type="text/javascript">
$(document).ready(function(){
$("a.pagelistnormal").each(function(){$(this).hover(function(){$(this).addClass("pagelistnormalover");},function(){$(this).removeClass("pagelistnormalover");$(this).addClass("pagelistnormal");});});
});
</script>

