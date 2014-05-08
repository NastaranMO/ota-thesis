<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="search-roozhaye-sal.aspx.cs" Inherits="Admin_search_roozhaye_sal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="Images/7days.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader"> روزهای سال</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن روزهای جدید</asp:LinkButton>
    </span>
    <hr />
<%--        <span>
        <asp:LinkButton ID="LinkButton1" CssClass="lnkMenuSideBar" runat="server" OnClick="editeCode_Click">ویرایش روزهای ثبت شده</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="deleteCode" CssClass="lnkMenuSideBar" runat="server" OnClick="deleteCode_Click">حذف روزهای ثبت شده</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>
    <hr />--%>
    <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" 
        OnClick="lnkViewAll_Click1">لیست روزهای ثبت شده</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    تعریف روزهای سال
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="جستجو"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
        <div class="tbl">
        
            <table style="width: 100%">
                <tr>
                    <td>
                        از تاریخ</td>
                    <td>
                        <span class="imageStyle">
                        <asp:Image ID="Image1" runat="server" 
                            ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                        </span>&nbsp;
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
                        </span>&nbsp;
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                            CssClass="MyCalendar" Format="yyyy/MM/d" PopupButtonID="Image2" 
                            TargetControlID="txtEndDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: center;" colspan="3">
                        &nbsp; &nbsp;
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                            onclick="btnSearch_Click" Text="جستجو"/>
                        &nbsp;
                        <asp:Button ID="btnClear" runat="server" CssClass="btn" 
                            onclick="btnClear_Click" Text="خالی کردن فیلدها" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
            </table>
        <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" 
                    onclick="lnkView_Click">مشاهده لیست کامل روزهای ثبت شده</asp:LinkButton>
        </div>
            <div class="grid">
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvDaysOfYear" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="dayId" CssClass="GridViewStyle" GridLines="Both"
                    CellPadding="3" ShowHeaderWhenEmpty="True" AllowPaging="True" 
                    onpageindexchanging="gvDaysOfYear_PageIndexChanging" 
                    onselectedindexchanging="gvDaysOfYear_SelectedIndexChanging" PageSize="25" 
                    >

                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="کد" DataField="dayId" ItemStyle-Width="5%" ItemStyle-CssClass="itemStyleNumber">
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="روز" DataField="Rooz" 
                            ItemStyle-Width="10%">
                            <ItemStyle  Width="10%" />
                        </asp:BoundField>                     
                        
                        
                        <asp:TemplateField HeaderText="تاریخ" ItemStyle-CssClass="itemStyleNumber" ItemStyle-Width="20%">
                           <ItemTemplate>
                               <asp:Label ID="lblDate" runat="server" Text='<%#GetDate(Eval("Tarikh")) %>'></asp:Label>
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="وضعیت روز" DataField="DsName" 
                            ItemStyle-Width="20%">
                            <ItemStyle  Width="20%" />
                        </asp:BoundField>

                        
                       
                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandName="Select" ImageUrl="~/images/btn/Edit.png"
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                      <%--  <asp:CommandField ItemStyle-Width="10%" ButtonType="Image" HeaderText="حذف" ShowDeleteButton="True"
                            DeleteImageUrl="~/images/btn/delete.png" ShowHeader="True">
                            <ItemStyle Width="10%" />
                        </asp:CommandField>--%>
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
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
            <div class="validationDiv">
            <br />
                <h1>
                    <span>
                      
                        <asp:Image ID="imageError" ImageUrl="~/images/btn/error.png" runat="server" />
                        <asp:Image ID="imageSuccess" ImageUrl="~/images/btn/success.png" runat="server" />
                        <asp:Label ID="lblMessage" runat="server" Text="پیام سیستم "></asp:Label></span></h1>
                <ol id="errorOl" runat="server">
                </ol>
                <br />
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
