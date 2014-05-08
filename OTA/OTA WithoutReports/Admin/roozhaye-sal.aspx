<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="roozhaye-sal.aspx.cs" Inherits="Admin_roozhaye_sal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="Image/7days.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader"> روزهای سال</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن روزهای جدید</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" OnClick="lnkViewAll_Click">لیست روزهای ثبت شده</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    روزهای سال
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="تعریف روز جدید"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            تاریخ شروع&nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span class="imageStyle">
                              
                                <asp:Image ID="Image1" runat="server" 
                                ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                            </span>
                            &nbsp;
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                CssClass="MyCalendar" Format="yyyy/MM/d" PopupButtonID="Image1" 
                                TargetControlID="txtStartDate">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            تاریخ پایان<span><b style="color: Red;">&nbsp;*</b></span>
                        </td>
                        <td>
                            <span class="imageStyle">
                            <asp:Image ID="Image2" runat="server" 
                                ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                          
                            </span>
                            &nbsp;
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtFullTime"></asp:TextBox>
                            <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                                CssClass="MyCalendar" Format="yyyy/MM/d" PopupButtonID="Image2" 
                                TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            روز شروع<span><b style="color: Red;"> *</b></span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStartDay" runat="server" CssClass="dayDdl">
                                <asp:ListItem Value="0">شنبه</asp:ListItem>
                                <asp:ListItem Value="1">یک شنبه</asp:ListItem>
                                <asp:ListItem Value="2">دوشنبه</asp:ListItem>
                                <asp:ListItem Value="3">سه شنبه</asp:ListItem>
                                <asp:ListItem Value="4">چهارشنبه</asp:ListItem>
                                <asp:ListItem Value="5">پنج شنبه</asp:ListItem>
                                <asp:ListItem Value="6">جمعه</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            روز پایان<span><b style="color: Red;"> *</b></span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEndDay" runat="server" CssClass="dayDdl">
                                <asp:ListItem Value="0">شنبه</asp:ListItem>
                                <asp:ListItem Value="1">یک شنبه</asp:ListItem>
                                <asp:ListItem Value="2">دوشنبه</asp:ListItem>
                                <asp:ListItem Value="3">سه شنبه</asp:ListItem>
                                <asp:ListItem Value="4">چهارشنبه</asp:ListItem>
                                <asp:ListItem Value="5">پنج شنبه</asp:ListItem>
                                <asp:ListItem Value="6">جمعه</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                                        <tr>
                        <td align="right">
                            وضعیت روز<span><b style="color: Red;"> *</b></span>
                        </td>
                        <td>
     
                            <asp:DropDownList ID="ddlDayState" runat="server" CssClass="dayDdl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                    <td><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            ساعت شروع نهار</td>
                        <td>
                            <span>
                            <asp:TextBox ID="txtStartLunch" runat="server" CssClass="txtFullTime" MaxLength="5"></asp:TextBox>
                           
                            </span>
                        </td>
                        <td>
                            ساعت پایان نهار</td>
                        <td>
                            <span>
                            <asp:TextBox ID="txtEndLunch" runat="server" CssClass="txtFullTime" MaxLength="5"></asp:TextBox>
                            
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ساعت شروع کار</td>
                        <td>
                            <span>
                            <asp:TextBox ID="txtStartWork" runat="server" CssClass="txtFullTime" MaxLength="5"></asp:TextBox>
                           
                            </span>
                        </td>
                        <td>
                            ساعت پایان کار</td>
                        <td>
                            <span>
                            <asp:TextBox ID="txtEndWork" runat="server" CssClass="txtFullTime" MaxLength="5"></asp:TextBox>
                          
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: center;" colspan="3">
                            <asp:Button ID="btnAdd" runat="server" Visible="false" CssClass="btn" 
                                Text="ثبت" ValidationGroup="x" onclick="btnAdd_Click"
                                 />&nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" 
                                onclick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <span>
                                <asp:Image ID="imgCustomError" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cvAdd" runat="server" ForeColor="Red" Font-Bold="false"
                                CssClass="errorClass" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                ValidationGroup="x" onservervalidate="cvAdd_ServerValidate"></asp:CustomValidator>
                                <span>
                                    <asp:Label ID="lblSearchMessage" ForeColor="Red" runat="server" Text="Label"></asp:Label> 
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
