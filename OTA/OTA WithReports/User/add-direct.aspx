<%@ Page Title="" Language="C#" MasterPageFile="~/User/userMasterPage.master" AutoEventWireup="true"
    CodeFile="add-direct.aspx.cs" Inherits="User_direct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/1334062378_player_time.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">کنترل وظایف</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">ثبت وظایف </asp:LinkButton>
    </span>
<%--    <hr />
    <span>
        <asp:LinkButton ID="LinkButton1" CssClass="lnkMenuSideBar" runat="server" OnClick="editeCode_Click">ویرایش عملیات</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="deleteCode" CssClass="lnkMenuSideBar" runat="server" OnClick="deleteCode_Click">حذف عملیات</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>--%>
    <hr />
    <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" OnClick="lnkViewAll_Click">لیست وظایف انجام شده</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
  کنترل وظایف
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="ثبت وظایف "></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr style="text-align: center;">
                        <td align="center" colspan="2" style="color: Blue;">
                        <h1>    تاریخ &nbsp

                            <asp:TextBox ID="lblTarikh" CssClass="txtFullTime" ForeColor="Blue" 
                            runat="server" BackColor="#EEEEEE"></asp:TextBox></h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px;" colspan="2">
                            <hr style="color: Gray; border: 1px dotted #BABAC0;" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            شرح عملیات&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:DropDownList ID="ddlCode" CssClass="ddlAmal" runat="server">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            از ساعت&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStartMinute"
                                    FilterType="Numbers" runat="server">
                                </asp:FilteredTextBoxExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtStartHour"
                                    FilterType="Numbers" runat="server">
                                </asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtStartMinute" CssClass="timeTxt" runat="server"></asp:TextBox>
                            </span><span style="color: Gray;">&nbsp:&nbsp</span> <span>
                                <asp:TextBox ID="txtStartHour" CssClass="timeTxt" runat="server"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تا ساعت&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtEndMinute"
                                FilterType="Numbers" runat="server">
                            </asp:FilteredTextBoxExtender>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtEndHour"
                                FilterType="Numbers" runat="server">
                            </asp:FilteredTextBoxExtender>
                            <span>
                                <asp:TextBox ID="txtEndMinute" CssClass="timeTxt" runat="server"></asp:TextBox>
                            </span><span style="color: Gray;">&nbsp:&nbsp</span> <span>
                                <asp:TextBox ID="txtEndHour" CssClass="timeTxt" runat="server"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            توضیحات&nbsp<span><b style="color: #F3F8FE;">&nbsp*</b></span>
                        </td>
                        <td>
                            <br />
                            <span>
                                <asp:TextBox CssClass="txtNote" ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: center;">
                            <asp:Button ID="btnAdd" runat="server" Visible="false" CssClass="btn" Text="ثبت"
                                ValidationGroup="x" OnClick="btnAdd_Click" />&nbsp
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" Visible="false" Text="ویرایش"
                                ValidationGroup="y" OnClick="btnEdit_Click" />&nbsp
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Visible="false" Text="جستجو"
                                OnClick="btnSearch_Click" />&nbsp; &nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="btn" Visible="false" Text="بازگشت"
                                OnClick="btnBack_Click" />&nbsp
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>
                                <asp:Image ID="imgCustomError" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cvAdd" runat="server" ForeColor="Red" Font-Bold="false"
                                CssClass="errorClass" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." ValidationGroup="x"
                                OnServerValidate="cvAdd_ServerValidate"></asp:CustomValidator>
                            <span>
                                <asp:Image ID="imgCustomEdit" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cVEdit" runat="server" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامی است."
                                ForeColor="Red" ValidationGroup="y" OnServerValidate="cVEdit_ServerValidate"></asp:CustomValidator>
                            <span>
                                <asp:Label ID="lblSearchMessage" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
                <div id="lblErrorTable" visible="false" class="divError" style="margin-left: 13px;"
                    runat="server">
                </div>
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست وظایف انجام شده</asp:LinkButton>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
                <div class="data">
                    <table width="50%">
                        <tr>
                            <td>
                                تاریخ
                            </td>
                            <td>
                                <span class="imageStyle">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                                </span>&nbsp;
                                <asp:TextBox ID="txtShowTarikh" runat="server" CssClass="txtFullTime"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                    Format="yyyy/MM/d" PopupButtonID="Image1" TargetControlID="txtShowTarikh">
                                </asp:CalendarExtender>
                                &nbsp&nbsp
                                <asp:LinkButton ID="lnkShowDirectCode" runat="server" 
                                    onclick="lnkShowDirectCode_Click">نمایش</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divErrorShow" visible="false" class="divError" style="margin-left: 13px;"
                    runat="server">
                </div>
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvDirectCode" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="pdcId" GridLines="None" 
                   
                    CellPadding="3" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvDirectCode_SelectedIndexChanged"
                    OnRowDeleting="gvDirectCode_RowDeleting" CssClass="GridViewStyle">
                  
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label></ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText=" شرح عملیات" DataField="DcName" ItemStyle-Width="35%">
                            <ItemStyle Width="31%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="از" DataField="StartTime" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="10%">
                            <ItemStyle CssClass="itemStyleNumber" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="تا" DataField="EndTime" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="10%">
                            <ItemStyle CssClass="itemStyleNumber" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="وضعیت" DataField="DscName" ItemStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="Note" HeaderText="توضیحات" ItemStyle-Width="17%" />
                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandName="Select" ImageUrl="~/images/btn/Edit.png"
                                    runat="server" /></ItemTemplate>
                            <ItemStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="8%" ButtonType="Image" HeaderText="حذف" ShowDeleteButton="True"
                            DeleteImageUrl="~/images/btn/delete.png" ShowHeader="True">
                            <ItemStyle Width="8%" />
                        </asp:CommandField>
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
