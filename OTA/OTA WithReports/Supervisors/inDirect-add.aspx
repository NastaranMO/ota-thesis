<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisors/AdminMaster.master" 
    AutoEventWireup="true" CodeFile="inDirect-add.aspx.cs" Inherits="Supervisors_inDirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cPHcontentHeader" runat="Server">
    <span id="imgHeader">
        <img src="Image/stock_navigator_edit_entry.ico" alt="" />
    </span><span>&nbsp</span><span id="contentHeader">مدیریت ورود و خروج </span>&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cPHBottomContent" runat="Server">

    <span>
        <asp:LinkButton ID="addCode" CssClass="lnkMenuSideBar" runat="server" OnClick="addCode_Click">ثبت ورود و خروج</asp:LinkButton>
    </span>
<%--    <hr />
    <span>
        <asp:LinkButton ID="LinkButton1" CssClass="lnkMenuSideBar" runat="server" OnClick="editeCode_Click"> ویرایش حضور و غیاب پرسنل</asp:LinkButton>
    </span>
    <hr />
    <span>
        <asp:LinkButton ID="searchCode" CssClass="lnkMenuSideBar" runat="server" OnClick="searchCode_Click">جستجو ساده</asp:LinkButton>
    </span>--%>
    <hr />
    <span>
        <asp:LinkButton ID="lnkViewAll" CssClass="lnkMenuSideBar" runat="server" OnClick="lnkViewAll_Click">لیست حضور و غیاب </asp:LinkButton>
    </span>
    <hr />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cPHheaderContentL" runat="Server">
 مدیریت ورود و خروج
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cPHlnkRahnama" runat="Server">
    <asp:LinkButton ID="lnkGuide" runat="server" OnClick="lnkGuide_Click">راهنما
    </asp:LinkButton>
    <br />
    <span style="padding-right: 0px;">
        <asp:Label ID="lblGuide" runat="server" ForeColor="red" Text=""></asp:Label></span>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cPHLegendName" runat="Server">
    <asp:Label ID="lblLegenName" runat="server" Text="ثبت ورود و خروج"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cPHTableOrGrid" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
          
<%--            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>--%>
            <div class="tbl">
                <table style="width: 100%;">
                    <tr style="text-align: center;font-weight:bold;">
                        <td colspan="2" align="center">
                            نام و نام خانوادگی :&nbsp
                            <asp:Label ID="lblFullName" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="2" align="center" style="font-weight:bold;">
                            <span style="float: right;">کد پرسنلی :&nbsp
                                <asp:Label ID="lblPersonelId" ForeColor="Blue" Font-Size="13px" Font-Names="B nazanin" runat="server" Text="Label"></asp:Label>
                            </span>&nbsp <span style="float: left; padding-left: 15px;">تاریخ :&nbsp
                                <asp:Label ID="lblTarikh" ForeColor="Blue" Font-Size="13px" Font-Names="B nazanin" runat="server" Text="Label"></asp:Label>         
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
                                    <span>
                                        <asp:DropDownList ID="ddlInDirectTypes" CssClass="ddlAmal" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlInDirectCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </span>&nbsp<br />
                                    <br />
                                    <span>
                                        <asp:DropDownList ID="ddlInDirectCodes" CssClass="ddlAmal" runat="server" 
                                        onselectedindexchanged="ddlInDirectCodes_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </span>
                                    <br />
                                    <br />

  
                                   

                        </td>
                    </tr>
                    <tr>
                        <td>
                            از ساعت &nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtStartMinute"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtStartMinute" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                            <span style="color: Gray;">&nbsp:&nbsp</span> <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtStartHour"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtStartHour" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                            <span style="color: Blue; font-size: 10px;">(مثال 16:40)</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            تا ساعت &nbsp<span><b style="color: Red;">&nbsp*</b></span>
                        </td>
                        <td>
                            <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtEndMinute"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtEndMinute" CssClass="timeTxt" runat="server"></asp:TextBox></span>
                            <span style="color: Gray;">&nbsp:&nbsp</span> <span>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtEndHour"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
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
                        </td>
                        <td style="text-align: center;">
                            <asp:Button ID="btnAdd" runat="server" Visible="false" CssClass="btn" Text="ثبت"
                                ValidationGroup="x" OnClick="btnAdd_Click" />&nbsp
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" Visible="false" Text="ویرایش"
                                ValidationGroup="y" OnClick="btnEdit_Click" />&nbsp
                            <asp:Button ID="btnBack" runat="server" CssClass="btn" Visible="false" Text="بازگشت"
                                OnClick="btnBack_Click" />&nbsp
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="خالی کردن فیلدها" OnClick="btnClear_Click" />
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

                <asp:LinkButton ID="lnkView" CssClass="lnk" runat="server" OnClick="lnkView_Click">مشاهده لیست ورود و خروج</asp:LinkButton>
                <%--<asp:LinkButton ID="lnkViewVacationList" CssClass="lnk" runat="server" OnClick="lnkViewVacationList_Click">مشاهده لیست حضور و غیاب</asp:LinkButton>--%>
            </div>
<%--                            </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger  ControlID="btnAdd"/>
                 <asp:AsyncPostBackTrigger ControlID="ddlInDirectTypes" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:PostBackTrigger  ControlID="btnEdit"/>
                <asp:PostBackTrigger  ControlID="btnBack"/>
                <asp:PostBackTrigger ControlID="lnkView" />
                </Triggers>
                </asp:UpdatePanel>--%>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="grid">
                        <div class="data">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        تاریخ های ثبت(نهایی) نشده در سیستم 
                                        &nbsp
                                                                                <asp:DropDownList ID="ddlTarikh" CssClass="dayDdl" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTarikh_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
          
                                </tr>
                            </table>
                        </div>
                        <div class="divError" runat="server" id="divError">
                            <asp:Label ID="lblGridError" ForeColor="black" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="headerGrid">
                            <span runat="server" id="listGrid"></span>
                        </div>
                        <asp:GridView ID="gvInDirectCode" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                            AutoGenerateColumns="False" DataKeyNames="PersonalID" CellPadding="3" CssClass="GridViewStyle" 
                            ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvInDirectCode_SelectedIndexChanged">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" HeaderText="کد پرسنلی" ItemStyle-CssClass="itemStyleNumber">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" runat="server" Text='<%#GetPersonelId(Eval("PersonalID")) %>'
                                            CssClass='<%#GetColor() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="نام" DataField="FirstName" ItemStyle-Width="17%"></asp:BoundField>
                                <asp:BoundField HeaderText="نام خانوادگی" DataField="LastName" ItemStyle-Width="28%">
                                </asp:BoundField>
                                <asp:CommandField HeaderText="ثبت ورود و خروج" ShowHeader="True" ItemStyle-Width="25%"
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
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="gvInDirectCode" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTarikh" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <br />
            <div class="tbl">
                <table width="100%">
                    <tr style="text-align: center;">
                        <td colspan="4" align="center">
                            <b style="color: black; font-weight: normal;">جزئیات ورود و خروج در تاریخ &nbsp<asp:Label
                                ID="lblTarikhDetails" Font-Bold="false" ForeColor="Blue" Font-Size="13px" CssClass="font"
                                runat="server" Text="Label"></asp:Label></b>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="4">
                            <hr style="border: 1px dotted #BABAC0; width: 300px;" />
                            <br />
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td>
                            کد پرسنلی
                        </td>
                        <td>
                            <asp:Label ID="lblpId" ForeColor="Blue" CssClass="itemStyleNumber" runat="server"
                                Text="Label"></asp:Label>
                        </td>
                        <td>
                            نام و نام خانوادگی
                        </td>
                        <td>
                            <asp:Label ID="lblFullNameDetails" ForeColor="Blue" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td>
                            دپارتمان
                        </td>
                        <td>
                            <asp:Label ID="lblDep" ForeColor="Blue" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            عنوان کاری
                        </td>
                        <td>
                            <asp:Label ID="lblJob" ForeColor="Blue" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="4">
                            <hr style="border: 1px dotted #BABAC0; width: 300px;" />
                            <br />
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                   
                            
                       
                        <td colspan="4">شرح عملیات&nbsp&nbsp
                            <asp:Label ID="lblAmaliat" ForeColor="Blue" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                    <br />
                    </td>
                    </tr>
                    <tr style="text-align:center;">
                        <td colspan="4">
                            از ساعت&nbsp
                            <asp:Label ID="lblStartTime" ForeColor="Blue" CssClass="itemStyleNumber" runat="server"
                                Text="Label"></asp:Label>
                 &nbsp&nbsp&nbsp
                            تا ساعت&nbsp
                      
                            <asp:Label ID="lblEndTime" CssClass="itemStyleNumber" ForeColor="Blue" runat="server"
                                Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            توضیحات
                        </td>
                        <td colspan="3">
                            <br />
                            <asp:TextBox ID="txtNoteDetails" ForeColor="Blue" CssClass="txtNote" Enabled="false"
                                TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblpindid" runat="server" Text="Label" Visible="False"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
<%--                            <asp:LinkButton ID="lnkdelete" ForeColor="Red" runat="server" 
                                OnClick="lnkdelete_Click">حذف</asp:LinkButton>--%>
                        </td>
                        <td>
                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnBack_Click">بازگشت</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <div class="grid">
                <div class="data">
                    <table style="width:60%;">
                        <tr>
                            <td>
                              تاریخ &nbsp;
                            </td>
                            <td align="right">
                                <span class="imageStyle">  
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/portal/Calendar_scheduleHS.png" />
                                </span>&nbsp;
                                <asp:TextBox ID="txtShowTarikh" runat="server" CssClass="txtFullTime"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                    Format="yyyy/MM/d" PopupButtonID="Image1" TargetControlID="txtShowTarikh">
                                </asp:CalendarExtender>
                                &nbsp
                                <asp:LinkButton ID="lnkViewListHozor" runat="server" OnClick="lnkViewListHozor_Click">نمایش لیست حضور و غیاب</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <div class="divError" runat="server" id="divError2">
                    </div>
                </div>
                <div class="headerGrid">
  
                    <span runat="server" id="listGrid2"></span>

                </div>
                <asp:GridView ID="gvList" runat="server" Width="100%" EmptyDataText="هیج رکوردی یافت نشد ..."
                    AutoGenerateColumns="False" DataKeyNames="pidcId" CellPadding="3" CssClass="GridViewStyle"
                    ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvList_SelectedIndexChanged" 
                    OnRowDeleting="gvList_RowDeleting" GridLines="None">
                  
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRow" runat="server" Text='<%# GetRow() %>' CssClass="itemStyleNumber"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="نام" DataField="FirstName" ItemStyle-Width="10%">
                        <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="نام خانوادگی" DataField="LastName" ItemStyle-Width="15%">
                        <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="عملیات" DataField="IdcName" ItemStyle-Width="20%">
                        <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="از" DataField="StartTime" ItemStyle-Width="7%" ItemStyle-CssClass="itemStyleNumber">
                        <ItemStyle CssClass="itemStyleNumber" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="تا" DataField="EndTime" ItemStyle-Width="7%" ItemStyle-CssClass="itemStyleNumber">
                        <ItemStyle CssClass="itemStyleNumber" Width="7%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ویرایش" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandName="Select" ImageUrl="~/images/btn/Edit.png"
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:CommandField ItemStyle-Width="10%" ButtonType="Link" HeaderText="جزئیات" ShowDeleteButton="True"
                            ShowHeader="True" DeleteText="بیشتر">
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
                    <asp:Label ID="lblfooter2" CssClass="lblFooter" runat="server" Text=" "></asp:Label>
                    <div id="divTaeed" runat="server" class="divkh">
                                                 <span>
                     &nbsp  <asp:ImageButton ID="imgConfirm" Visible="false" ToolTip="تایید نهایی لیست ورود و خروج" 
                                                          ImageUrl="~/images/btn/confirm.png" runat="server" onclick="imgConfirm_Click" /> &nbsp&nbsp&nbsp
                   </span>  
                 </div>
                </div>
               
            </div>
        </asp:View>
        <asp:View ID="View6" runat="server">
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
