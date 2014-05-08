<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DirectReport.aspx.cs" Inherits="Admin_Reports_DirectReport" %>
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
    <asp:Label ID="lblLegenName" runat="server" Text="گزارش مشروح عملکرد کارمندان"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" Runat="Server">
    <div id="div1" class="tbl">

    <table style="width: 100%">
        <tr>
            <td>
                از تاریخ</td>
            <td>
                            <span class="imageStyle">
                              
                                <asp:Image ID="Image1" runat="server" 
                                ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                            </span>
                            &nbsp;
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                CssClass="MyCalendar" Format="yyyy/MM/d" PopupButtonID="Image1" 
                                TargetControlID="txtStartDate">
                            </asp:CalendarExtender>
                
            </td>
            <td>
                تا تاریخ</td>
            <td>
                            <span class="imageStyle">
                            <asp:Image ID="Image2" runat="server" 
                                ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                          
                            </span>
                            &nbsp;
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                                CssClass="MyCalendar" Format="yyyy/MM/d" PopupButtonID="Image2" 
                                TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                دپارتمان</td>
            <td colspan="3">
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
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnCreateReport" CssClass="btn" runat="server" 
                    Text="تهیه گزارش" onclick="btnCreateReport_Click" ValidationGroup="x" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                 <span>
                                <asp:Image ID="imgCustomError" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cvAdd" runat="server" ForeColor="Red" Font-Bold="false"
                                CssClass="errorClass" ErrorMessage="تاریخ شروع نباید از تاریخ پایان کوچکتر باشد." 
                                ValidationGroup="x" onservervalidate="cvAdd_ServerValidate"></asp:CustomValidator>
                </td>
        </tr>
    </table>

    </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" Runat="Server">
</asp:Content>

