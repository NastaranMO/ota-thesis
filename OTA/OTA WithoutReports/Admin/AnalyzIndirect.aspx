<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AnalyzIndirect.aspx.cs" Inherits="Admin_Reports_AnalyzIndirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/report.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">گزارشات کارمندان</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <asp:LinkButton ID="lnkListPersonnel" runat="server" 
    onclick="lnkListPersonnel_Click">لیست کارمندان</asp:LinkButton>
    <hr />
    <asp:LinkButton ID="lnkTimeshit" runat="server" onclick="lnkTimeshit_Click">گزارش مشروح عملکرد کارمندان</asp:LinkButton>
    <hr />
        <asp:LinkButton ID="lnkIndirect" runat="server" onclick="lnkIndirect_Click">گزارش مشروح ورود و خروج کارمندان</asp:LinkButton>
    <hr />
    <asp:LinkButton ID="lnkVacs" runat="server" onclick="lnkVacs_Click">گزارش مرخصی کارمندان</asp:LinkButton>
    <hr />
    <asp:LinkButton ID="lnkAnalyz" runat="server" onclick="lnkAnalyz_Click">تحلیل عملکرد کارمندان</asp:LinkButton>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    گزارشات کارمندان
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server">راهنما
    </asp:LinkButton>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" Runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" Runat="Server">
</asp:Content>

