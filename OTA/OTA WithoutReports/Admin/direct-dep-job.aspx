<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="direct-dep-job.aspx.cs" Inherits="Admin_direct_dep_job" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/tanzimat.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader"> تخصیص کدهای عملیاتی</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن تنظیمات جدید</asp:LinkButton>
    </span>
    <hr />
        <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" OnClick="lnkViewAll_Click">لیست کدهای عملیاتی</asp:LinkButton>
    </span>
    <hr />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
     تخصیص کدهای عملیاتی
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="افزودن تنطیمات جدید"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            دپارتمان&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:DropDownList ID="ddldeps" CssClass="ddlAmal" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddldeps_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            شرح عملیات &nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:DropDownList ID="ddlCode" CssClass="ddlAmal" runat="server" 
                                onselectedindexchanged="ddlCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <br />
                            شامل عناوین کاری&nbsp<span><b style="color: red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <br />
                            <span>
                                <asp:CheckBoxList ID="chkJobs" runat="server" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
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
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" 
                                OnClick="btnClear_Click" />
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
                                ForeColor="Red" ValidationGroup="y"></asp:CustomValidator>
                            <span>
                                <asp:Label ID="lblSearchMessage" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست کدهای عملیاتی عناوین کاری</asp:LinkButton>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">

                <div class="data">
                <table >
                    <tr>
                        <td>
                            دپارتمان 
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlDepView" runat="server" CssClass="ddlAmal" 
                                AutoPostBack="True" onselectedindexchanged="ddlDepView_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            عنوان کاری
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlJobView" CssClass="ddlAmal" runat="server">
                            </asp:DropDownList>

             &nbsp&nbsp
                                            
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkViewDep_Click">نمایش</asp:LinkButton>
                        </td>

                    </tr>

                </table>
                </div>
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvDirectCode" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="DcDepJobId" CssClass="GridViewStyle" GridLines="none"
                    CellPadding="3" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvDirectCode_SelectedIndexChanged">
                
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label></ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText=" شرح عملیات" DataField="DcName" ItemStyle-Width="40%">
                            <ItemStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="کد عملیات" DataField="DcId" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="15%">
                            <ItemStyle CssClass="itemStyleNumber" Width="15%" />
                        </asp:BoundField>
                        <asp:CommandField ItemStyle-Width="15%" ButtonType="Image" HeaderText="حذف" ShowSelectButton="True"
                            SelectImageUrl="~/images/btn/delete.png" ShowHeader="True">
                            <ItemStyle Width="15%" />
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
