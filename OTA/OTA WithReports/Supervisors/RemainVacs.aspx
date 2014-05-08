<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisors/AdminMaster.master" AutoEventWireup="true" CodeFile="RemainVacs.aspx.cs" Inherits="Supervisors_Reports_RemainVacs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/report.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">گزارشات کارمندان</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <asp:LinkButton ID="lnkTimeshit" runat="server" onclick="lnkTimeshit_Click">گزارش مشروح عملکرد کارمندان</asp:LinkButton>
    <hr />
        <asp:LinkButton ID="lnkIndirect" runat="server" onclick="lnkIndirect_Click">گزارش مشروح ورود و خروج کارمندان</asp:LinkButton>
    <hr />
    <asp:LinkButton ID="lnkVacs" runat="server" onclick="lnkVacs_Click">گزارش مرخصی کارمندان</asp:LinkButton>
        <hr />
    <asp:LinkButton ID="lnkAnalyzis" runat="server" onclick="lnkAnalyzis_Click" >تحلیل عملکرد کارمندان</asp:LinkButton>
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
    <asp:Label ID="lblLegenName" runat="server" Text="گزارش مرخصی کارمندان"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="data">
                <table style="width: 65%">
                    <tr>
                        <td>
                            از تاریخ
                        </td>
                        <td>
                            <span class="imageStyle">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                            </span>&nbsp;
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                Format="yyyy/MM/d" PopupButtonID="Image1" TargetControlID="txtStartDate">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            تا تاریخ
                        </td>
                        <td>
                            <span class="imageStyle">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                            </span>&nbsp;
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" CssClass="MyCalendar"
                                Format="yyyy/MM/d" PopupButtonID="Image2" TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <asp:Button ID="btnCreateReport" CssClass="btn" runat="server" 
                                Text="تهیه گزارش" onclick="btnCreateReport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="grid">
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvInDirectCode" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="PersonalID" CellPadding="3" CssClass="GridViewStyle"
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                                                <asp:BoundField HeaderText="کد پرسنلی" DataField="PersonalID" ItemStyle-Width="10%" ItemStyle-CssClass="itemStyleNumber">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="سال" DataField="YearName" ItemStyle-Width="5%" ItemStyle-CssClass="itemStyleNumber">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="نوع مرخصی" DataField="IdcName" ItemStyle-Width="20%">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="مانده مرخصی سال جاری" DataField="RemainVac" ItemStyle-Width="20%"
                            ItemStyle-CssClass="itemStyleNumber"></asp:BoundField>
                        <asp:BoundField HeaderText="قابل انتقال" DataField="MaxTransfer" ItemStyle-Width="15%"
                            ItemStyle-CssClass="itemStyleNumber"></asp:BoundField>
                        <%--                  <asp:CommandField HeaderText="ثبت ورود و خروج" ShowHeader="True" ItemStyle-Width="25%"
                            ShowSelectButton="True" SelectText="انتخاب" ButtonType="Link" />--%>
                    </Columns>
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
                <div class="footerGrid">
                    <asp:Label ID="lblFooter" CssClass="lblFooter" runat="server" Text=" "></asp:Label>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" runat="Server">
</asp:Content>
