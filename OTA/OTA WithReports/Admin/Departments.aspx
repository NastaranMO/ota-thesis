<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Departments.aspx.cs" Inherits="Admin_Departments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="../images/btn/1334062378_player_time.png" alt="" />
    </span><span>&nbsp</span><span id="contentHeader"> دپارتمان ها </span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">
    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">افزودن دپارتمان جدید</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="editeCode" CssClass="lnkMenuSideBar" runat="server" OnClick="editeCode_Click">لیست دپارتمان ها</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو</asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
    دپارتمان ها 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <span style="padding-right: 0px;">
                <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text="Label"></asp:Label></span>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkGuide" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="افزودن دپارتمان جدید"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="tbl">
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            کد دپارتمان&nbsp<span><b style="color: Red;">*&nbsp</b></span>
                        </td>
                        <td align="right">
                            <span>
                                <asp:TextBox ID="txtDepId" CssClass="txtFullTime" Font-Bold="false" runat="server"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            شرح دپارتمان&nbsp<span><b style="color: Red;">*&nbsp</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:TextBox ID="txtDepName" CssClass="txtLarge" Font-Bold="false" runat="server"></asp:TextBox></span>
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
                                    runat="server"></asp:TextBox></span>
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
                                Text="ثبت" onclick="btnAdd_Click" ValidationGroup="x" />&nbsp
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" Visible="false" 
                                Text="ویرایش" onclick="btnEdit_Click1" ValidationGroup="y" />&nbsp
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Visible="false" Text="جستجو"
                                OnClick="btnSearch_Click" />&nbsp
                            <asp:Button ID="btnDisEdit" runat="server" CssClass="btnDisabled" Visible="false"
                                Text="ویرایش" Enabled="False" />&nbsp
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" 
                                OnClick="btnClear_Click" />
                        </td>
                    </tr>

                                       <tr>
                        <td colspan="2">

                     
                                                        <span>
                            <asp:Image ID="imageError0" runat="server" Visible="false" ImageUrl="~/images/btn/error.png" />
                            </span>
                            <asp:CustomValidator ID="cvAddDepartmen" runat="server" ForeColor="Red" 
                                Font-Bold="false" CssClass="errorClass"
                                ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                onservervalidate="cvAddDepartmen_ServerValidate" ValidationGroup="x" Display="Dynamic"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="cvEditDepartmen" runat="server" CssClass="errorClass" 
                                                            Display="Dynamic" ErrorMessage="وارد کردن فیلدهای ستاره دار الزامیست." 
                                                            Font-Bold="false" ForeColor="Red" 
                                                            onservervalidate="cvEditDepartmen_ServerValidate" ValidationGroup="y"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست دپارتمان ها</asp:LinkButton>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="grid">
                <div class="headerGrid">
                    - لیست دپارتمان ها -
                </div>
                <asp:GridView ID="gvDep" runat="server" Width="100%" EmptyDataText="هیچ رکوردی یافت نشد..." AutoGenerateColumns="False"
                    DataKeyNames="DepId" CssClass="GridViewStyle" GridLines="none" CellPadding="3" ShowHeaderWhenEmpty="True"
                    OnSelectedIndexChanged="gvDep_SelectedIndexChanged" 
                    onrowdeleting="gvDep_RowDeleting">
                  
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="نام دپارتمان" DataField="DepName" ItemStyle-Width="35%">
                            <ItemStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="کد دپارتمان" DataField="DepId" ItemStyle-CssClass="itemStyleNumber"
                            ItemStyle-Width="15%">
                            <ItemStyle CssClass="itemStyleNumber" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="شرح" DataField="DepNote" ItemStyle-Width="25%">
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
                        <RowStyle CssClass="RowStyle" />

    <EmptyDataRowStyle CssClass="EmptyRowStyle" />

    <PagerStyle CssClass="PagerStyle" />

    <SelectedRowStyle CssClass="SelectedRowStyle" />

    <HeaderStyle CssClass="HeaderStyle" />

    <EditRowStyle CssClass="EditRowStyle" />

    <AlternatingRowStyle CssClass="AltRowStyle" /> 
                </asp:GridView>
                <div class="footerGrid">
                    <asp:Label ID="lblFooter" CssClass="lblFooter" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lblMessage" runat="server" Text="پیام سیستم"></asp:Label></span></h1>
              
                <ol  id="errorOl" runat="server">
                   
                </ol>
      <br />
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
