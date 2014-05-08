<%@ Page Title="" Language="C#" MasterPageFile="~/User/userMasterPage.master" AutoEventWireup="true"
    CodeFile="IndirectHafte.aspx.cs" Inherits="User_IndirectHafte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/report.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">ورود و خروج</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <asp:LinkButton ID="lnkTimeshit" runat="server" onclick="lnkTimeshit_Click" >ورود و خروج ماه جاری</asp:LinkButton>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    ورود و خروج
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server">راهنما
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="ورود و خروج ماه جاری "></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <div class="tbl">
        <table style="width: 100%">
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
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnCreateReport" CssClass="btn" runat="server" Text="نمایش"
                         ValidationGroup="x" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <span>
                        <asp:Image ID="imgCustomError" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                    </span>
                    <asp:CustomValidator ID="cvAdd" runat="server" ForeColor="Red" Font-Bold="false"
                        CssClass="errorClass" ErrorMessage="تاریخ شروع نباید از تاریخ پایان کوچکتر باشد."
                        ValidationGroup="x" OnServerValidate="cvAdd_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
        </table>
    </div>
    <div id="gv" runat="server" class="grid">
        <div class="headerGrid">
            <span runat="server" id="listGrid"></span>
        </div>
        <asp:GridView ID="gvInDirect" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
            AutoGenerateColumns="False" DataKeyNames="PerId" GridLines="None" CellPadding="3"
            ShowHeaderWhenEmpty="True" CssClass="GridViewStyle">
            <Columns>
                <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label></ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
               <asp:TemplateField HeaderText="تاریخ" ItemStyle-CssClass="itemStyleNumber" ItemStyle-Width="20%">
               <ItemTemplate >
                   <asp:Label ID="Label1" runat="server" Text='<%# GetDate(Eval("Tarikh")) %>' CssClass="itemStyleNumber"></asp:Label>           
               </ItemTemplate>
               </asp:TemplateField> 
                <asp:BoundField HeaderText="روز" DataField="Rooz" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="نوع روز" DataField="DsName" ItemStyle-Width="15%" />
                <asp:BoundField HeaderText="از" DataField="StartTime" ItemStyle-CssClass="itemStyleNumber"
                    ItemStyle-Width="15%">
                    <ItemStyle CssClass="itemStyleNumber" Width="15%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="تا" DataField="EndTime" ItemStyle-CssClass="itemStyleNumber"
                    ItemStyle-Width="15%">
                    <ItemStyle CssClass="itemStyleNumber" Width="15%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="وضعیت" DataField="IdcName" ItemStyle-Width="20%">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
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
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" runat="Server">
</asp:Content>
