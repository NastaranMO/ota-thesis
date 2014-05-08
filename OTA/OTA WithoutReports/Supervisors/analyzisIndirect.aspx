<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisors/AdminMaster.master"
    AutoEventWireup="true" CodeFile="analyzisIndirect.aspx.cs" Inherits="Supervisors_Reports_analyzisIndirect" %>

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
    <asp:Label ID="lblLegenName" runat="server" Text="تحلیل عملکرد کارمندان"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <div class="grid">
        <div class="divError" runat="server" id="divError">
            <asp:Label ID="lblGridError" ForeColor="black" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="headerGrid">
            <span runat="server" id="listGrid"></span>
        </div>
        <asp:GridView ID="gvPersonels" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
            AutoGenerateColumns="False" DataKeyNames="PersonalID" CellPadding="3" CssClass="GridViewStyle"
            ShowHeaderWhenEmpty="True" 
            onselectedindexchanged="gvPersonels_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="کد پرسنلی" DataField="PersonalID" ItemStyle-CssClass="itemStyleNumber"
                    ItemStyle-Width="15%" />
                <asp:BoundField HeaderText="نام" DataField="FirstName" ItemStyle-Width="15%"></asp:BoundField>
                <asp:BoundField HeaderText="نام خانوادگی" DataField="LastName" ItemStyle-Width="20%">
                </asp:BoundField>
                <asp:BoundField HeaderText="عنوان کاری" DataField="JobName" ItemStyle-Width="25%" />
                <asp:CommandField HeaderText="تحلیل عملکرد" ShowHeader="True" ItemStyle-Width="20%"
                    ShowSelectButton="True" SelectText="تحلیل" ButtonType="Link" />
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
            <br />
            <br />
        </div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="data">
                <table>
                    <tr>
                        <td colspan="4" style="text-align: center; font-weight: bold; color: Red;">
                            تحلیل عملکرد
                            <hr style="color: red; border: 1px dotted red;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            شماره پرسنلی:
                        </td>
                        <td>
                            &nbsp
                            <asp:Label ID="lblPID" runat="server" Text="103"></asp:Label>
                        </td>
                        <td>
                            &nbsp نام و نام خانوادگی:
                        </td>
                        <td>
                            &nbsp
                            <asp:Label ID="lblFullName" runat="server" Text="nastaran moghadasi"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp از تاریخ&nbsp
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
                            &nbsp تا تاریخ&nbsp
                        </td>
                        <td>
                            <span class="imageStyle">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                            </span>&nbsp;
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                                Format="yyyy/MM/d" PopupButtonID="Image2" TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
                            &nbsp
                            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">نمایش</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="4"><br /></td>

                    </tr>
                    <tr>
                    <td>
                    ماموریت:&nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblmamoriat" runat="server" Text="-"></asp:Label>         
                    </td>
                    <td>
                    مرخصی:&nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblvac" runat="server" Text="-"></asp:Label>        
                    </td>
                    </tr>
                    <tr>
                    <td>
                    حضور:&nbsp
                    </td>
                    <td>
                     <asp:Label ID="lblhozaor" runat="server" Text="-"></asp:Label>   
                    </td>
                    </tr>
                   
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" runat="Server">
</asp:Content>
