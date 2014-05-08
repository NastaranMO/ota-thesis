<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="AddPersonal.aspx.cs" Inherits="Admin_AddPersonal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/personel.png" alt="" />
        </span><span>&nbsp</span><span id="contentHeader">اطلاعات کارمندان</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" runat="server" OnClick="addCode_Click">افزودن کارمند جدید</asp:LinkButton>
    </span>
   <hr />
    <span>
        <asp:LinkButton ID="deleteCode" runat="server" OnClick="deleteCode_Click">لیست کارمندان</asp:LinkButton>
    </span>
    <hr />
     
        <span>
        <asp:LinkButton ID="searchCode" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
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
    <asp:Label ID="lblLegenName" runat="server" Text="افزودن کارمند جدید"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="view0BasicInfo" runat="server">
            <div class="tbl">
                <table style="width: 100%">
                    <tr>
                        <td>
                            شماره پرسنلی <span class="validator">*</span>
                        </td>
                        <td colspan="3">
                            <span>
                                <asp:TextBox ID="txtPersonalId" runat="server" CssClass="txtNormal" Font-Bold="false"
                                    MaxLength="13"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtPersonalId_FilteredTextBoxExtender" runat="server"
                                    TargetControlID="txtPersonalId" FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                            </span>
                            &nbsp; &nbsp;
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
                            تاریخ تولد
                        <span class="validator">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBirthDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtBirthDate_CalendarExtender" runat="server" PopupButtonID="img1" TargetControlID="txtBirthDate">
                            </asp:CalendarExtender>
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
                            شماره شناسنامه
                        <span class="validator">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShSh" runat="server" CssClass="txtFullTime" MaxLength="12"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtShSh_FilteredTextBoxExtender" FilterType="Numbers"
                                runat="server" TargetControlID="txtShSh">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تلفن همراه <span class="validator">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="txtNormal"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" 
                                runat="server" FilterType="Numbers" TargetControlID="txtMobile">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td>
                            تحصیلات <span class="validator">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlEducation" CssClass="dayDdl" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تلفن ثابت
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtHomePhone" runat="server" CssClass="txtNormal"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtHomePhone_FilteredTextBoxExtender" FilterType="Numbers"
                                runat="server" TargetControlID="txtHomePhone">
                            </asp:FilteredTextBoxExtender>
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
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            <asp:Button ID="btnBasicInfo" runat="server" CssClass="btn" Text="مرحله بعد" 
                                ValidationGroup="x" onclick="btnBasicInfo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <span>
                            <asp:Image ID="imageError0" runat="server" ImageUrl="~/images/btn/error.png" 
                                Visible="false" />
                            <asp:CustomValidator ID="cvBasicInfo" runat="server" CssClass="errorClass" 
                                Display="Dynamic" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                Font-Bold="false" ForeColor="Red" 
                                onservervalidate="cvBasicInfo_ServerValidate" ValidationGroup="x"></asp:CustomValidator>
                            </span>
                        </td>
                      
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="view1LoginInfo" runat="server">
            <div class="tbl">
                <table style="width: 100%">
                    <tr>
                        <td>
                            سطح دسترسی <span class="validator">*</span> </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="ddlAccessLevel" CssClass="dayDdl" runat="server">
                                <asp:ListItem Selected="True" Value="0">کارمند</asp:ListItem>
                                <asp:ListItem Value="1">سرپرست</asp:ListItem>
                                <asp:ListItem Value="2">مدیر</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            رمز ورود به سیستم <span class="validator">*</span> </td>
                        <td>
                            &nbsp;
                        <span>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtNormal" TextMode="Password" 
                                Font-Bold="false" MaxLength="50"></asp:TextBox>
                          
                            <asp:PasswordStrength ID="txtPassword_PasswordStrength" runat="server" 
                                MinimumLowerCaseCharacters="2" MinimumNumericCharacters="2" 
                                MinimumSymbolCharacters="2" MinimumUpperCaseCharacters="2" 
                                TextStrengthDescriptions="بسیار ضعیف;ضعیف;متوسط;قوی;عالی"
                                PreferredPasswordLength="14" TargetControlID="txtPassword" 
                                DisplayPosition="LeftSide" PrefixText="میزان امنیت: ">
                            </asp:PasswordStrength>
                          
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تکرار رمز ورود <span class="validator">*</span> </td>
                        <td>
                            &nbsp;
                        <span>
                            <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="txtNormal" 
                                Font-Bold="false" MaxLength="50" TextMode="Password"></asp:TextBox>
                            </span>
                            <asp:CompareValidator ID="cvConfirmPass" runat="server" CssClass="errorClass" ForeColor="Red"
                                ErrorMessage="رمزهای عبور با هم مطابقت ندارند." Display="Static" 
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPass" 
                                ValidationGroup="y"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:Button ID="btnLoginInfo" runat="server" CssClass="btn" Text="مرحله بعد" 
                                ValidationGroup="y" onclick="btnLoginInfo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span>
                            <asp:Image ID="imageError1" runat="server" ImageUrl="~/images/btn/error.png" 
                                Visible="false" />
                            <asp:CustomValidator ID="cvLoginInfo" runat="server" CssClass="errorClass" 
                                Display="Dynamic" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                Font-Bold="false" ForeColor="Red" 
                                onservervalidate="cvLoginInfo_ServerValidate" ValidationGroup="y"></asp:CustomValidator>
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        
        <asp:View ID="view2Employee" runat="server">
        <div class="tbl">
        
            <table style="width: 100%">
                <tr>
                    <td>
                        تاریخ شروع قرارداد <span class="validator">*</span> </td>
                    <td>
                        <asp:TextBox ID="txtEmpStartContract" runat="server" CssClass="txtFullTime"></asp:TextBox>
                        
                        <asp:CalendarExtender ID="txtEmpStartContract_CalendarExtender" runat="server" PopupButtonID="img2"
                            TargetControlID="txtEmpStartContract">
                        </asp:CalendarExtender>
                        <img id="img2" alt="" src="../images/portal/Calendar_scheduleHS.png" />
                        
                    </td>
                    <td>
                        تاریخ پایان قرارداد <span class="validator">*</span> </td>
                    <td>
                        <asp:TextBox ID="txtEmpEndContract" runat="server" CssClass="txtFullTime"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEmpEndContract_CalendarExtender" runat="server" PopupButtonID="img3"
                            TargetControlID="txtEmpEndContract">
                        </asp:CalendarExtender>
                        <img id="img3" alt="" src="../images/portal/Calendar_scheduleHS.png" />
                    </td>
                </tr>
                <tr>
                    <td>
                        دپارتمان</td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlDepartment" CssClass="ddlAmal" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        سمت&nbsp; کاری</td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlJob" CssClass="ddlAmal" Enabled="false" runat="server">
                        </asp:DropDownList>
                       
                       
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnEmployee" runat="server" CssClass="btn" Text="مرحله بعد" 
                            ValidationGroup="z" onclick="btnEmployee_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <span>
                        <asp:Image ID="imageError2" runat="server" ImageUrl="~/images/btn/error.png" 
                            Visible="false" />
                        <asp:CustomValidator ID="cvEmployee" runat="server" CssClass="errorClass" 
                            Display="Dynamic" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                            Font-Bold="false" ForeColor="Red" 
                            onservervalidate="cvEmployee_ServerValidate" ValidationGroup="z"></asp:CustomValidator>
                        </span>
                    </td>
                </tr>
            </table>
        
        </div>
        </asp:View>
        <asp:View ID="view4Confirm" runat="server">
        <div class="tbl">
            <table style="width: 100%">
                <tr>
                    <td>
                        شماره پرسنلی</td>
                    <td colspan="3">
                        <asp:Label ID="lblPersonalId" Font-Bold="true" ForeColor="Blue" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        نام و نام خانوادگی</td>
                    <td colspan="3">
                        <asp:Label ID="lblFullName" Font-Bold="true" ForeColor="Blue" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        سطح دسترسی به سیستم</td>
                    <td>
                        <asp:Label ID="lblAccessLevel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        تاریخ تولد</td>
                    <td>
                        <asp:Label ID="lblBirthDate" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        شماره شناسنامه</td>
                    <td>
                        <asp:Label ID="lblShSh" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        تلفن همراه</td>
                    <td>
                        <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        تلفن ثابت</td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        تاریخ شروع قرارداد</td>
                    <td>
                        <asp:Label ID="lblStartContract" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        تاریخ پایان قرارداد</td>
                    <td>
                        <asp:Label ID="lblEndContract" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        دپارتمان</td>
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        سمت کاری</td>
                    <td>
                        <asp:Label ID="lblJob" runat="server" Font-Bold="True"></asp:Label>
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
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="ثبت نهایی" 
                            ValidationGroup="q" onclick="btnSubmit_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </div>
        </asp:View>
        
    </asp:MultiView>
</asp:Content>

    

<asp:Content ID="Content7" ContentPlaceHolderID="cPHvalidationDiv" runat="Server">
    <asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View1" runat="server">

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
