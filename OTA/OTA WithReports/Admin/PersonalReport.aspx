<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PersonalReport.aspx.cs" Inherits="Admin_Reports_PersonalReport" %>


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
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="لیست کارمندان"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
<div class="tbl">

    <table style="width: 100%">
        <tr>
            <td>
                نام دپارتمان</td>
            <td>
                <asp:DropDownList ID="ddlDepartments" CssClass="ddlAmal" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                فرمت خروجی</td>
            <td>
                <asp:DropDownList ID="ddlExportFormat" CssClass="dayDdl" runat="server">
                    <asp:ListItem Value="0">PDF</asp:ListItem>
                    <asp:ListItem Value="1">Excel</asp:ListItem>
                    <asp:ListItem Value="2">Microsoft Word</asp:ListItem>
                    <asp:ListItem Value="3">Rich Text</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnCreateReport" CssClass="btn" runat="server" 
                    Text="تهیه گزارش" onclick="btnCreateReport_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</div>
    </asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" runat="Server">
    
</asp:Content>
