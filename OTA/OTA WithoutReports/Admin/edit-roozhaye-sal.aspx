<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="edit-roozhaye-sal.aspx.cs" Inherits="Admin_edit_roozhaye_sal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="Images/7days.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader"> روزهای سال</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">

    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن روزهای جدید</asp:LinkButton>
    </span>
<%--    <hr />
        <span>
        <asp:LinkButton ID="LinkButton1" CssClass="lnkMenuSideBar" runat="server" OnClick="editeCode_Click">ویرایش روزهای ثبت شده</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="deleteCode" CssClass="lnkMenuSideBar" runat="server" OnClick="deleteCode_Click">حذف روزهای ثبت شده</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>--%>
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
    <asp:Label ID="lblLegenName" runat="server" Text="ویرایش روزهای ثبت شده"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            تاریخ</td>
                        <td>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                        <td>
                            روز&nbsp;
                            <asp:Label ID="lblDay" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            وضعیت روز<span><b style="color: Red;">*</b></span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlDayState" CssClass="ddlAmal" runat="server">
                            </asp:DropDownList>
                        </td>
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
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn"
                                Text="ویرایش" ValidationGroup="y" onclick="btnEdit_Click"
                                />&nbsp
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Visible="false" 
                                Text="جستجو" onclick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" 
                                onclick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
                <br />
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
