<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="SearchPersonals.aspx.cs" Inherits="Admin_SearchPersonals" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/code.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">اطلاعات کارمندان</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن کارمند جدید</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="deleteCode" CssClass="lnkMenuSideBar" runat="server" OnClick="deleteCode_Click">لیست کارمندان</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    اطلاعات کارمندان
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="جستجو "></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            شماره پرسنلی
                        </td>
                        <td>
                            <span>
                                <asp:TextBox ID="txtPersonalId" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    MaxLength="13"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtPersonalId_FilteredTextBoxExtender" runat="server"
                                    FilterType="Numbers" TargetControlID="txtPersonalId">
                                </asp:FilteredTextBoxExtender>
                            </span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نام و نام خانوادگی
                        </td>
                        <td>
                            <span>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    MaxLength="50"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtLastName" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    MaxLength="50"></asp:TextBox>
                            </span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره شناسنامه
                        </td>
                        <td>
                            <asp:TextBox ID="txtShSh" runat="server" CssClass="txtFullTime" MaxLength="12"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtShSh_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="txtShSh">
                            </asp:FilteredTextBoxExtender>
                            <br />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تلفن/موبایل
                        </td>
                        <td>
                            <asp:TextBox ID="txtHomePhone" runat="server" CssClass="txtNormal"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtHomePhone_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="txtHomePhone">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            دپارتمان
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddlAmal">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: center;">
                            &nbsp; &nbsp;
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="جستجو" OnClick="btnSearch_Click"
                                ValidationGroup="x" />&nbsp;<asp:Button ID="btnClear" runat="server" CssClass="btn"
                                    Text="پاک کردن" OnClick="btnClear_Click" />
                        </td>
                        <td style="text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                        <td style="text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvPersonals" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="PersonalId"  GridLines="Both" CssClass="GridViewStyle"
                    CellPadding="3" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvPersonals_SelectedIndexChanged"
                    OnRowDeleting="gvPersonals_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="کد پرسنلی" DataField="PersonalId" ItemStyle-Width="10%">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="نام خانوادگی" ItemStyle-Width="25%" />
                        <asp:BoundField HeaderText="نام" DataField="FirstName" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="25%">
                            <ItemStyle CssClass="itemStyleNumber" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EduName" HeaderText="تحصیلات" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="DepName" HeaderText="دپارتمان" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="JobName" HeaderText="سمت" ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandName="Select" ImageUrl="~/images/btn/Edit.png"
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="5%" ButtonType="Image" HeaderText="حذف" ShowDeleteButton="True"
                            DeleteImageUrl="~/images/btn/delete.png" ShowHeader="True">
                            <ItemStyle Width="5%" />
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
