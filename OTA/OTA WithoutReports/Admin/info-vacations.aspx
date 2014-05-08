<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="info-vacations.aspx.cs" Inherits="Admin_info_vacations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/vacInfo.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">تنظیمات انواع مرخصی</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن تنظیمات جدید</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" OnClick="lnkViewAll_Click">لیست تنظیمات انواع مرخصی</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    تنظیمات انواع مرخصی
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                <table style="width: 100%;">
                <tr>
                                        <td>
                            سال&nbsp<span><b style="color: Red;">*&nbsp</b></span>
                        </td>
                        <td colspan="3">
                            <span>
                                <asp:DropDownList ID="ddlYear" CssClass="dayDdl" runat="server">
                                </asp:DropDownList> 
                            </span>&nbsp&nbsp
                            <asp:Label ID="lblNewYear" runat="server" Visible="false" Text="سال : "></asp:Label>
                                <asp:TextBox ID="txtnewYear" Visible="false" Width="40px" CssClass="timeTxt" runat="server"></asp:TextBox>&nbsp
                            <asp:LinkButton ID="lnkSaveNewYear" Visible="false" runat="server" 
                                onclick="lnkSaveNewYear_Click">ثبت</asp:LinkButton>&nbsp
                      
                            <asp:LinkButton ID="lnkNewYear" runat="server" Visible="true" onclick="lnkNewYear_Click">افزودن سال جدید</asp:LinkButton>
                            <asp:Label ID="lblErrorYear" runat="server" Visible="false" ForeColor="Red" Text=""></asp:Label>
                                  <asp:LinkButton ID="lnkCancel" Visible="false" runat="server" onclick="lnkCancel_Click">انصراف</asp:LinkButton>
                        </td>
                </tr>
                    <tr>
                        <td>
                            شرح مرخصی&nbsp<span><b style="color: Red;">*&nbsp</b></span>
                        </td>
                        <td colspan="3">
                            <span>
                                <asp:DropDownList ID="ddlVacation" CssClass="ddlAmal" runat="server">
                                </asp:DropDownList> 
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <hr style=" border: 1px dotted #BABAC0; width: 300px;" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            حداکثر مجاز در سال<span><b style="color: #EEEEEE;">*&nbsp;</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                TargetControlID="txtMaxYear" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtMaxYear" CssClass="txtFullTime" Font-Bold="false" 
                                runat="server" ontextchanged="txtMaxYear_TextChanged"></asp:TextBox></span>
                            <span>&nbspساعت</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           هر&nbsp <span><b style="color: #EEEEEE;">*&nbsp</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:DropDownList CssClass="ddlNumber" ID="ddlMonth" runat="server" 
                                AutoPostBack="True" onselectedindexchanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="سال" Value="1">سال</asp:ListItem>
                                    <asp:ListItem Value="12">1</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                </asp:DropDownList>
                            </span><span>&nbspماه</span> <span>
                            &nbsp
                                <asp:TextBox Enabled="false" ID="txtInMonth" CssClass="txtFullTime" runat="server"></asp:TextBox>
                            </span><span>&nbspساعت</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            TargetControlID="txtTransfer" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                           <span style="color:green;">حداکثر مرخصی قابل انتقال به سال بعد </span> 
                                &nbsp&nbsp&nbsp
                                    <asp:TextBox ID="txtTransfer" Width="64px" CssClass="txtFullTime" runat="server"></asp:TextBox>&nbsp
                                    ساعت </span>
                            <asp:Label ID="lblVacInfoId" Visible="false" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <hr style="color: #BABAC0; border: 1px dotted #BABAC0; width: 300px;" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <span>توضیحات :</span>
                        </td>
                        <td>
                            <br />
                            <span>
                                <asp:TextBox ID="txtNote" TextMode="MultiLine" CssClass="txtNote" runat="server"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
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
                            <asp:Button ID="btnDisEdit" runat="server" CssClass="btnDisabled" Visible="false"
                                Text="ویرایش" Enabled="False" />&nbsp
                            <asp:Button ID="btnBack" runat="server" CssClass="btn" Visible="false" Text="بازگشت"
                                OnClick="btnBack_Click" />&nbsp
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
                                ForeColor="Red" ValidationGroup="y" OnServerValidate="cVEdit_ServerValidate"></asp:CustomValidator>
                            <span>
                                <asp:Label ID="lblSearchMessage" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlMonth" />
                <asp:AsyncPostBackTrigger  ControlID="lnkNewYear" EventName="Click"/>
                <asp:PostBackTrigger ControlID="lnkSaveNewYear" />
                <asp:AsyncPostBackTrigger  ControlID="lnkCancel" EventName="Click"/>
                <asp:PostBackTrigger ControlID="btnAdd" /> 
                <asp:PostBackTrigger ControlID="btnEdit" />
                <asp:PostBackTrigger  ControlID="btnBack"/>
                <asp:PostBackTrigger ControlID="btnClear" />
                </Triggers>
                </asp:UpdatePanel>
                <br />
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست اطلاعات انواع مرخصی</asp:LinkButton>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
            <div class="data">
            <table style="width:35%">
            <tr>
            <td>
            سال &nbsp
            </td>
            <td>
                <asp:DropDownList ID="ddlViewYear" CssClass="dayDdl" runat="server">
                </asp:DropDownList>    
            </td>
            <td>
                <asp:LinkButton ID="lnkShow" runat="server" onclick="lnkShow_Click">نمایش</asp:LinkButton>  
            </td>
            </tr>
            </table>
            </div>
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div>
                <asp:GridView ID="gvVacInfo" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="VacInfoId" GridLines="none" CssClass="GridViewStyle"
                    CellPadding="3" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvDirectCode_SelectedIndexChanged"
                    OnRowDeleting="gvDirectCode_RowDeleting">
            
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="مرخصی" DataField="IdcName" ItemStyle-Width="35%">
                            <ItemStyle Width="17%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="حداکثر در سال" DataField="MaxInYear" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="15%">
                            <ItemStyle CssClass="itemStyleNumber" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Period" HeaderText="هر(ماه)" ItemStyle-Width="5%" ItemStyle-CssClass="itemStyleNumber"/>
                        <asp:BoundField DataField="MaxInPeriod" HeaderText="حداکثر" ItemStyle-Width="5%" ItemStyle-CssClass="itemStyleNumber"/>
                         <asp:BoundField DataField="MaxTransfer" HeaderText="قابل انتقال" ItemStyle-Width="15%" ItemStyle-CssClass="itemStyleNumber" />
                        <asp:BoundField DataField="VacNote" HeaderText="توضیحات" ItemStyle-Width="15%" />
                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandName="Select" ImageUrl="~/images/btn/Edit.png"
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="6%" ButtonType="Image" HeaderText="حذف" ShowDeleteButton="True"
                            DeleteImageUrl="~/images/btn/delete.png" ShowHeader="True">
                            <ItemStyle Width="6%" />
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
