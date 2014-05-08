<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisors/AdminMaster.master"
    AutoEventWireup="true" CodeFile="confirm-direct.aspx.cs" Inherits="Supervisors_confirm_direct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/1334062378_player_time.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">کنترل وظایف</span>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">لیست کارمندان</asp:LinkButton>
    </span>
    <hr />

    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
   کنترل وظایف
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="لیست کارمندان"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr style="text-align: center; font-weight: bold;">
                        <td colspan="2" align="center">
                            نام و نام خانوادگی :&nbsp
                            <asp:Label ID="lblFullName" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="2" align="center" style="font-weight: bold;">
                            <span style="float: right;">کد پرسنلی :&nbsp
                                <asp:Label ID="lblPersonelId" ForeColor="Blue" Font-Size="13px" Font-Names="B nazanin"
                                    runat="server" Text="Label"></asp:Label>
                            </span>&nbsp <span style="float: left; padding-left: 15px;">تاریخ :&nbsp
                                <asp:Label ID="lblTarikh" ForeColor="Blue" Font-Size="13px" Font-Names="B nazanin"
                                    runat="server" Text="Label"></asp:Label>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr style="color: Gray; border: 1px dotted #BABAC0;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            شرح عملیات&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDirectCodes" CssClass="ddlAmal" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            از ساعت &nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtStartMinute"
                                FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtStartHour"
                                FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <span>
                                <asp:TextBox ID="txtStartMinute" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                            <span style="color: Gray;">&nbsp:&nbsp</span> <span>
                                <asp:TextBox ID="txtStartHour" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                            <span style="color: Blue; font-size: 10px;">(مثال 16:40)</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تا ساعت &nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtEndMinute"
                                FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtEndHour"
                                FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <span>
                                <asp:TextBox ID="txtEndMinute" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                            <span style="color: Gray;">&nbsp:&nbsp</span> <span>
                                <asp:TextBox ID="txtEndHour" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
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
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="text-align: center;">
                            &nbsp
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" Text="ویرایش" ValidationGroup="y" />&nbsp
                            <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="بازگشت" OnClick="btnBack_Click" />&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>
                                <asp:Image ID="imgCustomEdit" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cVEdit" runat="server" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامی است."
                                ForeColor="Red" ValidationGroup="y" OnServerValidate="cVEdit_ServerValidate"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <div id="lblErrorTable" visible="false" class="divError" style="margin-left: 13px;"
                                runat="server">
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست پرسنل</asp:LinkButton>
                <br />
                <br />
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
                <div class="data">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 70%;">
                                <tr>
                                    <td>
                                        تاریخ های ثبت(نهایی) نشده در سیستم &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlTarikh" CssClass="dayDdl" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTarikh_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp&nbsp
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTarikh" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="divError" runat="server" id="divError">
                        <asp:Label ID="lblGridError" ForeColor="Black" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="headerGrid">
                    <span runat="server" id="listGrid"></span>
                </div> 
                <asp:GridView ID="gvPersonels" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="PersonalID" CellPadding="3" CssClass="GridViewStyle"
                    ShowHeaderWhenEmpty="True" 
                    OnSelectedIndexChanged="gvPersonels_SelectedIndexChanged" GridLines="None">
                 
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="کد پرسنلی" DataField="PersonalID" ItemStyle-Width="15%"
                            ItemStyle-CssClass="itemStyleNumber">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="نام" DataField="FirstName" ItemStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField HeaderText="نام خانوادگی" DataField="LastName" ItemStyle-Width="20%">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="عنوان کاری" DataField="JobName" ItemStyle-Width="20%">
                        </asp:BoundField>
                        <asp:CommandField HeaderText="لیست عملیات" ShowHeader="True" ItemStyle-Width="20%"
                            ShowSelectButton="True" SelectText="انتخاب" ButtonType="Link" />
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
                    <div id="divTaeed" runat="server" class="divkh">
                        <span>&nbsp
                            <asp:ImageButton ID="imgConfirm" Visible="false" ToolTip="تایید نهایی لیست حضور و غیاب"
                                ImageUrl="~/images/btn/confirm.png" runat="server" OnClick="imgConfirm_Click" />
                            &nbsp&nbsp&nbsp </span>
                    </div>
                </div>
               </div>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <div class="grid">
                <div class="headerGrid" style="line-height: 20px;">
                    <span runat="server" id="listGrid2"></span>
                </div>
                <asp:GridView ID="gvDirect" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="PdcId" CssClass="GridViewStyle" CellPadding="3"
                    ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvDirect_SelectedIndexChanged"
                    OnRowDeleting="gvDirect_RowDeleting">
       
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="عملیات" DataField="DcName" ItemStyle-Width="40%"></asp:BoundField>
                        <asp:BoundField HeaderText="از" DataField="StartTime" ItemStyle-Width="10%" ItemStyle-CssClass="itemStyleNumber">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="تا" DataField="EndTime" ItemStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField HeaderText="وضعیت" DataField="DscName" ItemStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField HeaderText="توضیحات" DataField="Note" ItemStyle-Width="15%"></asp:BoundField>
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
                       <RowStyle CssClass="RowStyle" />

    <EmptyDataRowStyle CssClass="EmptyRowStyle" />

    <PagerStyle CssClass="PagerStyle" />

    <SelectedRowStyle CssClass="SelectedRowStyle" />

    <HeaderStyle CssClass="HeaderStyle" />

    <EditRowStyle CssClass="EditRowStyle" />

    <AlternatingRowStyle CssClass="AltRowStyle" /> 
                </asp:GridView>
                <div class="footerGrid">
                    <asp:Label ID="lblFooter2" CssClass="lblFooter" runat="server" Text=" "></asp:Label>
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
