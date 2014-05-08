<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="EditPersonal.aspx.cs" Inherits="Admin_EditPersonal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/personel.png" alt="" />&nbsp </span><span>&nbsp</span><span
            id="contentHeader" style="padding-right: 46px; width: 100px;">پرسنل</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <hr />
    <span>
        <asp:LinkButton ID="addCode" runat="server" OnClick="addCode_Click">افزودن پرسنل جدید</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="editCode" runat="server" OnClick="editeCode_Click">ویرایش پرسنل</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="deleteCode" runat="server" OnClick="deleteCode_Click">حذف پرسنل</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" runat="server" OnClick="searchCode_Click">جستجو در بین پرسنل</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    پرسنل
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="ویرایش پرسنل"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="tbl">
                <table style="width: 100%">
                    <tr>
                        <td>
                            شماره پرسنلی <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <span>
                                <asp:TextBox ID="txtPersonalId" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    Enabled="false" MaxLength="13"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtPersonalId_FilteredTextBoxExtender" runat="server"
                                    TargetControlID="txtPersonalId" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                            </span>&nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            نام&nbsp; <span class="validator">*</span>
                        </td>
                        <td>
                            <span>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    MaxLength="50"></asp:TextBox>
                            </span>
                        </td>
                        <td>
                            تاریخ تولد <span class="validator">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBirthDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtBirthDate_CalendarExtender" runat="server" PopupButtonID="img1"
                                TargetControlID="txtBirthDate"></asp:CalendarExtender>
                            <img id="img1" alt="" src="../images/portal/Calendar_scheduleHS.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            نام خانوادگی&nbsp; <span class="validator">*</span>
                        </td>
                        <td>
                            <span>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    MaxLength="50"></asp:TextBox>
                            </span>
                        </td>
                        <td>
                            شماره شناسنامه <span class="validator">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShSh" runat="server" CssClass="txtFullTime" MaxLength="12"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtShSh_FilteredTextBoxExtender" FilterType="Numbers"
                                runat="server" TargetControlID="txtShSh"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تلفن همراه <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="txtNormal"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" FilterType="Numbers"
                                runat="server" TargetControlID="txtMobile"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تلفن ثابت
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtHomePhone" runat="server" CssClass="txtNormal"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtHomePhone_FilteredTextBoxExtender" FilterType="Numbers"
                                runat="server" TargetControlID="txtHomePhone"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            سطح دسترسی <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlAccessLevel" CssClass="dayDdl" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تحصیلات <span class="validator">*</span></td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlEducation" runat="server" CssClass="dayDdl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            آدرس
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAdress" runat="server" CssClass="txtNote" TextMode="MultiLine"
                                Width="356px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            توضیحات
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNote" runat="server" CssClass="txtNote" TextMode="MultiLine"
                                Width="356px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                            <hr style="border: 1px groove #BABAC0; color: #BABAC0; height: 1px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            دپارتمان <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                                CssClass="ddlAmal" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            سمت&nbsp; کاری <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlJob" runat="server" CssClass="ddlAmal" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تاریخ شروع قرارداد <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSupStartContract" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtSupStartContract_CalendarExtender" runat="server" PopupButtonID="img2"
                                TargetControlID="txtSupStartContract"></asp:CalendarExtender>
                            <img id="img4" alt="" src="../images/portal/Calendar_scheduleHS.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تاریخ پایان قرارداد <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSupEndContract" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtSupEndContract_CalendarExtender" runat="server" PopupButtonID="img3"
                                TargetControlID="txtSupEndContract"></asp:CalendarExtender>
                            <img id="img5" alt="" src="../images/portal/Calendar_scheduleHS.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="3">
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" Text="ویرایش"
                                ValidationGroup="x" OnClick="btnEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <span>
                                <asp:Image ID="imageError0" runat="server" ImageUrl="~/images/btn/error.png" Visible="false" />
                                <asp:CustomValidator ID="cvBasicInfo" runat="server" CssClass="errorClass" Display="Dynamic"
                                    ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." Font-Bold="false" ForeColor="Red"
                                    OnServerValidate="cvBasicInfo_ServerValidate" ValidationGroup="x"></asp:CustomValidator>
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" runat="Server">
<asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View2" runat="server">

    <div class="validationDiv">
        <h1>
            <span>
                <asp:Image ID="imageError" ImageUrl="~/images/btn/error.png" runat="server" />
                <asp:Image ID="imageSuccess" ImageUrl="~/images/btn/success.png" runat="server" />
                <asp:Label ID="lblMessage" runat="server" Text="پیام سیستم"></asp:Label></span></h1>
        <ol id="errorOl" runat="server">
          
        </ol>
    </div>
        </asp:View>
        </asp:MultiView>
</asp:Content>
