<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="tanzimat-deps-jobs.aspx.cs" Inherits="Admin_tanzimat_deps_jobs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/tanzimat.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">دپارتمان ها و عناوین کاری</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن تنظیمات جدید</asp:LinkButton>
    </span>
    <hr />
        <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" OnClick="lnkViewAll_Click">لیست دپارتمان ها و عناوین کاری</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    دپارتمان ها و عناوین کاری
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="افزودن تنظیمات جدید"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            دپارتمان(های) &nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:CheckBoxList ID="chkDep" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                </asp:CheckBoxList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شامل عنوان کاری&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:DropDownList ID="ddlJob" runat="server" CssClass="ddlAmal">
                                </asp:DropDownList>
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
                                ValidationGroup="y" />&nbsp
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Visible="false" 
                                Text="جستجو"/>&nbsp
                            <asp:Button ID="btnDisEdit" runat="server" CssClass="btnDisabled" Visible="false"
                                Text="ویرایش" Enabled="False" />&nbsp
                            <asp:Button ID="btnBack" runat="server" CssClass="btn" Visible="false" 
                                Text="بازگشت" />&nbsp
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="پاک کردن" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>
                                <asp:Image ID="imgCustomError" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cvAddDepJob" runat="server" ForeColor="Red" Font-Bold="false"
                                CssClass="errorClass" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                ValidationGroup="x" onservervalidate="cvAddDepJob_ServerValidate"></asp:CustomValidator>
                            <asp:CustomValidator ID="cVEdit" runat="server" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامی است."
                                ForeColor="Red" ValidationGroup="y" 
                                onservervalidate="cVEdit_ServerValidate"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست دپارتمان ها و عناوین کاری</asp:LinkButton>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
             <div class="data">
                <table style="width:80%;" >
                    <tr>
                        <td>
                            دپارتمان 
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlDepView" runat="server" CssClass="ddlAmal"></asp:DropDownList>
                            &nbsp&nbsp <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkViewDep_Click">نمایش</asp:LinkButton>
                        </td>
                    </tr>

                </table>
                </div>
           
                &nbsp<div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvDepJob" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="DepJobId" GridLines="Horizontal" CssClass="GridViewStyle"
                    CellPadding="3" ShowHeaderWhenEmpty="True" 
                    onselectedindexchanged="gvDepJob_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText=" شرح عنوان کاری" DataField="JobName" ItemStyle-Width="35%">
                            <ItemStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="کد عنوان" DataField="JobId" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="15%">
                            <ItemStyle CssClass="itemStyleNumber" Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="حذف" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" CommandName="Select" ImageUrl="~/images/btn/delete.png"
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
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
