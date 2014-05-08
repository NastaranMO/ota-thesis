<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static AjaxControlToolkit.Slide[] GetSlides()
    {
        return new AjaxControlToolkit.Slide[] { 
            new AjaxControlToolkit.Slide("images/SlideShow/img01.png", "Blue Hills", "Go Blue"),
            new AjaxControlToolkit.Slide("images/SlideShow/img02.png", "Sunset", "Setting sun"),
            new AjaxControlToolkit.Slide("images/SlideShow/img03.png", "Winter", "Wintery..."),
            new AjaxControlToolkit.Slide("images/SlideShow/img04.png", "Water lillies", "Lillies in the water"),
            new AjaxControlToolkit.Slide("images/SlideShow/img05.png", "Sedona", "Portrait style picture")};
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script>
        alert('adminpanel: UNAME:1000|PWD:123')

    </script>
    <title>شرکت داروسازی الماس دارو</title>
    <link href="styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="styles/default.css" rel="stylesheet" type="text/css" />
    <link href="styles/MenuCss.css" rel="stylesheet" type="text/css" />
    <link href="styles/SlideShowStyle.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <link rel="icon" media="all" type="image/x-icon" href="images/top.PNG" />
    <link rel="shortcut icon" media="all" type="image/x-icon" href="images/top.PNG" />
    <link rel="address bar icon" href="images/top.PNG" />
    <!-- include jQuery library -->
    <script type="text/javascript" src="/jscript/jquery.js"></script>
    <!-- include Cycle plugin -->
    <script type="text/javascript" src="http://cloud.github.com/downloads/malsup/cycle/jquery.cycle.all.latest.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'fade',
                pause: 1,
                prev: '#prev',
                next: '#next'
            });
        });        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="outerDiv">
        <div id="headerDiv">
        </div>
        <div id="outerMenuDiv">
            <div id="menuDiv">
                <ul>
                    <li><a href="#">
                        <asp:LinkButton ID="lnkHomePage" runat="server"><b style="color:#BF0000;">صفحه اصلی</b></asp:LinkButton>
                    </a></li>
                    <li><a href="#">درباره ما</a>
                        <ul>
                            <li>
                                <asp:LinkButton ID="lnkAddTerm" runat="server">تاریخچه</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="lnkAddUni" runat="server">فلسفه وجودی</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="lnkEditUni" runat="server">چشم انداز</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="lnkDeleteUni" runat="server">جوایز و تقدیرنامه ها</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li><a href="#">محصولات</a>
                        <ul>
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server">داروهای انسانی</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButton2" runat="server">داروهای دامی</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li><a href="#">تحقیق و توسعه</a> </li>
                    <li><a href="#">فروش و بازاریابی</a>
                        <ul>
                            <li>
                                <asp:LinkButton ID="LinkButton3" runat="server">فروش</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButton4" runat="server">بازاریابی</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButton5" runat="server">پرسش و پاسخ</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButton6" runat="server">نظرسنجی</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li><a href="#">مطالعات بالینی</a>
                        <ul>
                            <li>
                                <asp:LinkButton ID="LinkButton7" runat="server">بالینی انسانی</asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButton8" runat="server">بالینی دائمی</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li><a href="#">اخبار</a> </li>
                    <li><a href="#">ارتباط با ما</a> </li>
                </ul>
            </div>
        </div>
        <div id="centerDiv">
            <div id="jQueryDiv">
                <asp:Image ID="Image1" runat="server" 
                Width="914px" Height="280px" />
                <div>
       
               <div style="margin-top:-170px;margin-right:880px;"> <asp:ImageButton ID="nextButton" ImageUrl="~/images/1.png" runat="server" /> 
             
            <div>
            <asp:ImageButton ID="prevButton" ImageUrl="~/images/2.png" runat="server" />  
               </div>   
 </div></div>
                <asp:SlideShowExtender ID="slideshowextend1" runat="server" TargetControlID="Image1"
                    SlideShowServiceMethod="GetSlides" AutoPlay="true" 
                    NextButtonID="nextButton" 
                     PreviousButtonID="prevButton" 
                    Loop="true" />
        
            </div>
        </div>
        <div id="bottomDiv">
            <div id="Right">
                <div class="titr">
                    منابع انسانی
                </div>
                <div class="content">
                    <div class="left">
                        <img src="images/manabe.gif" alt="منابع انسانی" /></div>
                    <div>
                        شركت داروسازي الماس 
                        دارو بر اين باور است كه مهمترين عامل در ساخت محصولات با كيفيت
                        وجود افراد متخصص است . اين شركت آموزش پرسنل خود را در اولويت اصلی قرار داده و پيوسته
                        دانش افراد خود را بمنظور تطبيق علوم آنها با منابع علمي مورد نیاز مورد ارزيابي قرار
                        مي دهد. اغلب متخصصان اين شركت در دوره هاي كوتاه مدت و بلند مدت آموزش در زمينه هاي
                        مختلف شرکت می نمایند می باشند.
                    </div>
                </div>
            </div>
            <div id="innereLeft">
                <div id="rightInnerLeft">
                    <div class="titr">
                        تضمین کیفیت</div>
                    <div class="content">
                        <div class="left" style="width: 92px;">
                            <img src="images/kontrol keifi.jpg" alt="تضمین کیفیت" />
                        </div>
                        <div>
                            بخش تضمین کیفیت کنترل تمامی جنبه های ساخت را از سیستم های کیفیت از جمله گواهینامه
                            محصول ، مدیریت شکایت و بازخوانی ، بازرسی و ممیزی داخلی ، و کنترل تغییرات را انجام
                            میدهد. برای تمام این سیستم ها افراد واجد شرایطی در سازمان وجود دارند که در جلسات
                            کیفیت آزادسازی محصول/ رد ، شکایت و بازخوانی، و تغییرات در خطوط تولید و دستورالعمل
                            ها را ارزیابی می نمایند. تمامی اسناد GMP توسط بخش تضمین کیفیت تایید می شود.
                        </div>
                    </div>
                </div>
                <div id="leftInnerLeft">
                    <div class="titr">
                        تحقیق و توسعه
                    </div>
                    <div class="content">
                        <div class="left" style="width: 93px;">
                            <img src="images/finance-img.jpg" alt="تحقیق و توسعه" />
                        </div>
                        <div>
                            تيم تحقيق و توسعه 
                            الماس دارو فرمولاسيون داروهاي جديد را طراحي و تكوين مي نمايند كه
                            انجام اين فرمولاسيون ها در جهت ارتقاي كيفيت محصولات و بهبود نياز و نقطه نظرات مشتري
                            مي باشد. تمام مراحل تکوین یک فرمولاسیون دارویی ، از مرحله پیش فرمولاسیون تا تولید
                            بچ آزمایشی و صنعتی و معتبرسازی روش های ساخت و آزمایش در این شرکت انجام می گیرد.
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="contentDiv">
            <div style="float: left; width: 600px;">
                <div class="titr" style="margin-top: 0px; margin-right: 0px;">
                    تاریخچه شرکت</div>
                <div class="content">
                    <div class="left" style="width: 224px; height: 137px;">
                        <img src="images/history-img_1.jpg" alt="" />
                    </div>
                    <div>
                        شركت داروسازي 
                        الماس دارو درميان برترین تولیدکنندگان دارويي ايران قرار دارد. اين شركت
                        حائز رتبه اول داروهاي هورموني در کشور می باشد.که با توجه به حدود نیم قرن سابقه توليد
                        دارو، نقش مهمي در اعتلاي صنعت داروسازي کشور دارد. اين شرکت در سال 1343 با مالکيت
                        شرکت آلماني شرينک و با نام "برليمد ايران" شروع به فعاليت نموده است. ما برآنیم تا
                        با ارتقای سطح دانش محوری سازمان در راستای ایجاد جامعه ای سالم، کارآمد و پویا به
                        عرضه محصولات درمانی و بهداشتی روز دنیا اهتمام ورزیم.</div>
                </div>
            </div>
            <div id="login">
                <div id="loginLeft">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBoxWatermarkExtender ID="tBWE1" runat="server" TargetControlID="txtUserName"
                                WatermarkText="نام کاربری" WatermarkCssClass="waterMark">
                            </asp:TextBoxWatermarkExtender>
                            <asp:TextBoxWatermarkExtender ID="eBWE2" runat="server" TargetControlID="txtPass"
                                WatermarkText="کلمه عبور" WatermarkCssClass="waterMark">
                            </asp:TextBoxWatermarkExtender>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            TargetControlID="txtUserName" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" MaxLength="20" ToolTip="نام کاربری"></asp:TextBox>
                            <asp:TextBox ID="txtPass" runat="server" CssClass="textBox" MaxLength="15" 
                                ToolTip="کلمه عبور" TextMode="Password"></asp:TextBox>
                            <div id="loginBottom">
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* نام کاربری یا کلمه عبور را اشتباه وارد کرده اید. *"
                                    CssClass="" ValidationGroup="x" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="imgEnter" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
             
                </div>
                <div id="loginRight">
                    <asp:ImageButton ID="imgEnter" ValidationGroup="x" runat="server" ImageUrl="~/images/btnLogin.png" OnClick="imgEnter_Click" />
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
    <div id="footerDiv">
        <hr />
        کلیه ی حقوق مادی و معنوی این سایت متعلق به شرکت داروسازی الماس می باشد.
        <br />
        .Copyright © 2012,<a href="mailto:n.moghadasi@outlook.com"> Nastaran Moghadasi</a>.
        All Rights Reserved
    </div>
    </form>
    <script type="text/javascript">
    </script>
</body>
</html>
