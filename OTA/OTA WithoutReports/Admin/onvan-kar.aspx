<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="onvan-kar.aspx.cs" Inherits="Admin_onvan_kar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/1334062378_player_time.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">عناوین کاری</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن عنوان کاری جدید</asp:LinkButton>
    </span>
    <hr />
                <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" 
        onclick="lnkViewAll_Click" >لیست عناوین کاری</asp:LinkButton>
    </span>
    <hr />
        <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
     عناوین کاری
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="لیست عناوین کاری"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
        <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                        کد عنوان کاری&nbsp<span><b style="color: Red;">
                            <asp:Label ID="Label1" runat="server" Text="*&nbsp"></asp:Label></b></span>
                        </td>
                        <td align="right">
                            <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txtJobId" FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtJobId" CssClass="txtFullTime" Font-Bold="false" 
                                runat="server" MaxLength="5"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                   شرح عنوان کاری&nbsp<span><b style="color: Red;">
                            <asp:Label ID="Label2" runat="server" Text="*&nbsp"></asp:Label></b></span>
                        </td>
                        <td>
                            <span>
                                <asp:TextBox ID="txtJobName" CssClass="txtLarge" Font-Bold="false" 
                                runat="server" MaxLength="140"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            توضیحات
                        </td>
                        <td>
                            <br />
                            <span>
                                <asp:TextBox ID="txtNote" TextMode="MultiLine" CssClass="txtNote" Font-Bold="false"
                                    runat="server" MaxLength="500"></asp:TextBox></span>
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
                            <asp:Button ID="btnAdd" runat="server" Visible="false" CssClass="btn" 
                                Text="ثبت"  ValidationGroup="x" />&nbsp
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" Visible="false" 
                                Text="ویرایش"  ValidationGroup="y" />&nbsp
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Visible="false" Text="جستجو"
                                OnClick="btnSearch_Click" />&nbsp; &nbsp;
                                                            <asp:Button ID="btnBack" runat="server" 
                                CssClass="btn" Visible="false"
                                Text="بازگشت" onclick="btnBack_Click"/>&nbsp
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" 
                                OnClick="btnClear_Click" />
                        </td>
                    </tr>

                                       <tr>
                        <td colspan="2">

                     
                                                        <span>
                            <asp:Image ID="imgCustomError" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cvAddDepartmen" runat="server" ForeColor="Red" 
                                Font-Bold="false" CssClass="errorClass"
                                ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                onservervalidate="cvAddJobs_ServerValidate" ValidationGroup="x"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="cVEdit" runat="server" 
                                                            ErrorMessage="وارد کردن فیلدهای ستاره دار الزامی است." ForeColor="Red" 
                                                            onservervalidate="cVEdit_ServerValidate" ValidationGroup="y"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست عناوین کاری</asp:LinkButton>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
                <div class="headerGrid">
                    - لیست عناوین کاری -
                </div>
                <asp:GridView ID="gvJob" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..." AutoGenerateColumns="False"
                    DataKeyNames="JobId" BackColor="White" GridLines="Both" CellPadding="3" ShowHeaderWhenEmpty="True"
                    OnSelectedIndexChanged="gvJob_SelectedIndexChanged" 
                    onrowdeleting="gvJob_RowDeleting">
                    <AlternatingRowStyle BackColor="#DEE7F4" />
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
                        <asp:BoundField HeaderText="توضیحات" DataField="JobNote" ItemStyle-Width="25%">
                            <ItemStyle Width="25%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandName="Select" ImageUrl="~/images/btn/Edit.png"
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="10%" ButtonType="Image" HeaderText="حذف" ShowDeleteButton="True"
                            DeleteImageUrl="~/images/btn/delete.png" ShowHeader="True">
                            <ItemStyle Width="10%" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="#E7E7E7" ForeColor="#003399" />
                    <HeaderStyle BackColor="#E7E7E7" Font-Size="10px" Font-Names="tahoma" ForeColor="#003399"
                        Font-Bold="True" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <div class="footerGrid">
                    <asp:Label ID="lblFooter" CssClass="lblFooter" runat="server" Text="ggg "></asp:Label>
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
              
                <ol  id="errorOl" runat="server">
                   
                </ol>
      <br />
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
