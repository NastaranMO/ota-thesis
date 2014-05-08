using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.EntityClient;
using System.Data;
using System.Data.Objects;
using OTA_DBModel;


public partial class Admin_roozhaye_sal : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

        //visible
        lblGuide.Visible = false;

        btnClear.Visible = false;
        imgCustomError.Visible = false;
        lblMessage.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;
        lblSearchMessage.Visible = false;
        MultiView2.ActiveViewIndex = -1;

        if (!IsPostBack)
        {
            //fill lblGuide
            lblGuideClass c = new lblGuideClass();
            lblGuide.Text = c.fillLblGuide("add");

            //////
            DaysOfYear lastDay = db.DaysOfYear.OrderByDescending(a => a.Tarikh).First();
            lblGuide.Text += "<br />" +
                "آخرین روز ثبت شده در دیتابیس " +
                "<b>" + lastDay.Rooz + " " + lastDay.Tarikh.ToShortDateString() + "</b>" +
                " می باشد." +
                "<br />" +
                "شما " + "<b>" + "باید " + "</b> ثبت روزها را از تاریخ " + "<b>" + lastDay.Tarikh.AddDays(1).ToShortDateString() + "</b>" + " ادامه دهید.";
            //////
            txtStartDate.Text = lastDay.Tarikh.AddDays(1).ToShortDateString();
            //visible
            btnAdd.Visible = true;
            btnClear.Visible = true;
            //multiView
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;

            //fill Drop Down Lists
            IEnumerable<DayState> ds = from d in db.DayState
                                       select d;
            bindClass.bindDropDownList(ddlDayState, ds, "DsName", "DsId");
        }

    }
    protected void cvAdd_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtStartDate.Text == "" || txtEndDate.Text == "")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            args.IsValid = true;

            //چک می کنیم که قبلا تاریخ ثبت نشده ای توی سیستم جا نیفتاده باشه.
            DateTime[] Gaps = SetDate.GapBetweenDates(Convert.ToDateTime(txtStartDate.Text), 1);


            if (Gaps.Count() != 0)
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                imageError.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(عدم پیوستگی در ثبت روز)</b>";

                string errorMessege = "";
                for (int i = 0; i < Gaps.Count(); i++)
                {
                    errorMessege += "<li>تاریخ " +
                    "<b>" + Gaps[i].ToShortDateString() + "</b>" +
                    " در سیستم ثبت نشده است.</li>";
                }

                errorOl.InnerHtml = errorMessege + "<br />" +
                    "<a href=" + "search-roozhaye-sal.aspx" + ">لیست تاریخ های ثبت شده سیستم</a>";
            }
            else
            {
                DaysOfYear d = db.DaysOfYear.OrderByDescending(a => a.Tarikh).First();
                if (d.Tarikh.AddDays(1) != Convert.ToDateTime(txtStartDate.Text))
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    imageError.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(عدم پیوستگی در ثبت روز)</b>";
                    errorOl.InnerHtml = "<li>تاریخ <b>" + d.Tarikh.AddDays(1) + "</b> در سیستم ثبت نشده است." + "</li>";

                }
                else
                {
                    int dsId = Convert.ToInt32(ddlDayState.SelectedItem.Value);
                    TimeSpan StartLunchTime = new TimeSpan();
                    TimeSpan EndLunchTime = new TimeSpan();
                    TimeSpan StartWorkTime = new TimeSpan();
                    TimeSpan EndWorkTime = new TimeSpan();

                    try
                    {
                        ///////
                        DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                        DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
                        int sYear = startDate.Year;
                        int eYear = endDate.Year;

                        bool CheckStartYear = SetDate.CheckCorrectYearId(sYear);
                        bool CheckEndYear = SetDate.CheckCorrectYearId(eYear);
                        bool FalseYearId = false;

                        if (!CheckStartYear || !CheckEndYear)
                        {
                            FalseYearId = true;
                            throw new Exception("Checking YearId => False");
                        }

                        ViewState["FalseYearId"] = FalseYearId;
                        ViewState["msg"] = SetDate.Msg;
                        /////////
                        if (txtStartLunch.Text != "")
                        {
                            SetTime t = new SetTime(txtStartLunch.Text);
                            StartLunchTime = t.GetTime();
                        }
                        else
                        {
                            StartLunchTime = new TimeSpan(0, 0, 0);
                        }

                        if (txtEndLunch.Text != "")
                        {
                            EndLunchTime = SetTime.GetTime(txtEndLunch.Text);
                        }
                        else
                        {
                            EndLunchTime = new TimeSpan(0, 0, 0);
                        }

                        if (txtStartWork.Text != "")
                        {
                            StartWorkTime = SetTime.GetTime(txtStartWork.Text);
                        }
                        else
                        {
                            StartWorkTime = new TimeSpan(0, 0, 0);
                        }

                        if (txtEndWork.Text != "")
                        {
                            SetTime t = new SetTime(txtEndWork.Text);
                            EndWorkTime = t.GetTime();
                        }
                        else
                        {
                            EndWorkTime = new TimeSpan(0, 0, 0);
                        }


                        if (StartLunchTime > EndLunchTime)
                        {
                            throw new Exception("WrongTime");
                        }

                        if (StartWorkTime > EndWorkTime)
                        {
                            throw new Exception("WrongTime");
                        }

                        if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                        {
                            throw new Exception("WrongDate");
                        }

                        Checking chk1 = new Checking("DaysOfYear", txtStartDate.Text, "Tarikh");

                        Checking chk3 = new Checking("DaysOfYear", ddlDayState.SelectedItem.Value.ToString(), "DsId");

                        bool c1 = chk1.CheckDuplicateData();

                        bool c3 = chk3.CheckDuplicateData();

                        if (c1 && c3)
                        {
                            throw new Exception("DuplicateDate");
                        }
                        else
                        {
                            int startDay = Convert.ToInt32(ddlStartDay.SelectedItem.Value);
                            int endDay = Convert.ToInt32(ddlEndDay.SelectedItem.Value);

                            if (endDay < startDay)
                            {
                                throw new Exception("Day Exception");
                            }
                            else
                            {
                                //آخرین روز ثبت شده در دیتابیس چند شنبه است؟
                                DaysOfYear query;
                                int roozIndex = 7;

                                query = db.DaysOfYear.OrderByDescending(a => a.Tarikh).First();

                                //query = db.DaysOfYear.Where(a => a.Tarikh == Convert.ToDateTime(txtStartDate.Text).AddDays(-1)).First();
                                string lastDaySubmit = query.Rooz;
                                switch (lastDaySubmit)
                                {
                                    case "شنبه":
                                        roozIndex = 0;
                                        break;
                                    case "یک شنبه":
                                        roozIndex = 1;
                                        break;
                                    case "دوشنبه":
                                        roozIndex = 2;
                                        break;
                                    case "سه شنبه":
                                        roozIndex = 3;
                                        break;
                                    case "چهارشنبه":
                                        roozIndex = 4;
                                        break;
                                    case "پنج شنبه":
                                        roozIndex = 5;
                                        break;
                                    case "جمعه":
                                        roozIndex = 6;
                                        break;
                                }

                                //////

                                ////// یه روز به آخرین روز ثبت شده به دیتابیس اضافه کن اگر آخرین روز جمعه بود ایندکس رو 0 کن
                                ////// این اولین روزی است که ما باید در دیتا بیس ثبت کنیم
                                if (roozIndex == 6)
                                {
                                    roozIndex = 0;
                                }

                                else if (roozIndex < 6)
                                {
                                    roozIndex++;
                                }
                                ////

                                DateTime sDate = Convert.ToDateTime(txtStartDate.Text);
                                DateTime eDate = Convert.ToDateTime(txtEndDate.Text);

                                SetDate sd = new SetDate(sDate, eDate);
                                int duration = sd.GetDuration();

                                //int index = startDay;
                                int[] week = new int[7];

                                ///آرایه ای برای ایام هفته که تشخیص بدهیم کدام روزها روز کاری و کدام روزهای غیر کاری است
                                ///// اعضا 1 آرایه نشان روزهای کاری و اعضای 0 نشان روزهای غیر کاری هستند
                                for (int w = 0; w < 7; w++)
                                {
                                    if (w <= endDay && w >= startDay)
                                    {
                                        week[w] = 1;
                                    }
                                    else
                                    {
                                        week[w] = 0;
                                    }
                                }

                                for (int i = 0; i <= duration; i++)
                                {
                                    string dayName = "";

                                    switch (roozIndex)
                                    {
                                        case 0:
                                            dayName = "شنبه";
                                            break;
                                        case 1:
                                            dayName = "یک شنبه";
                                            break;
                                        case 2:
                                            dayName = "دوشنبه";
                                            break;
                                        case 3:
                                            dayName = "سه شنبه";
                                            break;
                                        case 4:
                                            dayName = "چهارشنبه";
                                            break;
                                        case 5:
                                            dayName = "پنج شنبه";
                                            break;
                                        case 6:
                                            dayName = "جمعه";
                                            break;
                                    }

                                    string startYear = sYear.ToString();
                                    string endYear = eYear.ToString();
                                    Year year = db.Year.Where(a => a.YearName == startYear).Single();
                                    int yearId = year.YearId;
                                    string errorMsg = "";
                                    //برای روز کاری ثبت ساعات الزامیست.
                                   
                                    if (dsId == 1)
                                    {
                                        if (txtEndLunch.Text == "" || txtEndWork.Text == "" || txtStartLunch.Text == "" || txtStartWork.Text == "")
                                        {

                                            errorMsg = "برای روزهای کاری ثبت ساعات شروع و پایان کار و همچنین ساعات شروع و پایان ناهار الزامیست.";
                                            ViewState["errorMsg"] = errorMsg;
                                            throw new Exception("Exception");
                                        }
                                    }

                                    //تشخیص میدهیم در روزی که قرار است ثبت شود روز کاری است یا غیر کاری

                                    if (week[roozIndex] == 1)//روز کاری
                                    {
                                        DaysOfYear doy = new DaysOfYear();
                                        doy.Tarikh = sDate;
                                        doy.Rooz = dayName;
                                        doy.DsId = dsId;
                                        doy.StartLunchTime = StartLunchTime;
                                        doy.EndLunchTime = EndLunchTime;
                                        doy.StartWorkTime = StartWorkTime;
                                        doy.EndWorkTime = EndWorkTime;
                                        doy.YearId = yearId;
                                        db.AddToDaysOfYear(doy);
                                        db.SaveChanges();
                                    }

                                    else if (week[roozIndex] == 0)//روز تعطیل
                                    {
                                        DaysOfYear doy = new DaysOfYear();
                                        doy.Tarikh = sDate;
                                        doy.Rooz = dayName;
                                        doy.DsId = 2;
                                        doy.YearId = yearId;

                                        doy.StartLunchTime = TimeSpan.Zero;
                                        doy.EndLunchTime = TimeSpan.Zero;
                                        doy.StartWorkTime = TimeSpan.Zero;
                                        doy.EndWorkTime = TimeSpan.Zero;

                                        db.AddToDaysOfYear(doy);
                                        db.SaveChanges();
                                    }

                                    sDate = sDate.AddDays(1);

                                    //برای دور بعد حلقه یک روز جلو میرویم
                                    if (roozIndex == 6)
                                    {
                                        roozIndex = 0;
                                    }

                                    else if (roozIndex < 6)
                                    {
                                        roozIndex++;
                                    }

                                }

                            }

                            imageSuccess.Visible = true;
                            lblMessage.Visible = true;
                            MultiView1.ActiveViewIndex = -1;
                            MultiView2.ActiveViewIndex = 0;
                            lblMessage.Text = "پیام سیستم";
                            errorOl.InnerHtml = "<li>" +
                                "اطلاعات با موفقیت در پایگاه داده ذخیره شد. " +
                                "<br />" +
                                "<a href=search-roozhaye-sal.aspx>مشاهده روزهای ثبت شده در سیستم</a>" +
                                "</li>";
                        }
                    }
                    catch
                    {
                        MultiView2.ActiveViewIndex = 0;
                        MultiView1.ActiveViewIndex = -1;
                        imageError.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";

                        bool falseYearId = (bool)ViewState["FalseYearId"];
                        string msg = (string)ViewState["msg"];

                        if (falseYearId)
                        {
                            errorOl.InnerHtml = "<li>" + msg + "</li>";
                        }
                        else if (ViewState["errorMsg"] != null)
                        {
                            string errorMsg = (string)ViewState["errorMsg"];
                            errorOl.InnerHtml = "<li>" + errorMsg + "</li>";
                        }
                        else
                        {
                            errorOl.InnerHtml = "<li>اطلاعات ایام وارد شده ممکن است، قبلا در پایگاه داده ثبت شده باشد.</li>" +
                                           "<li>ساعت شروع کار یا شروع نهار باید کوچکتر از ساعت پایان آن باشد.</li>" +
                                           "<li>تاریخ شروع دوره باید کوچکتر از تاریخ پایان دوره باشد.</li>" +
                                           "<li>برای ثبت ایام کاری ثبت ساعات شروع و پایان کار و شروع و پایان نهار الزامیست.</li>" +
                                            "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
                                            
                        }


                    }
                }
            }
        }
    }

    protected void addCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("roozhaye-sal.aspx");
    }
    protected void editeCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("search-roozhaye-sal.aspx");
    }
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("search-roozhaye-sal.aspx");
    }
    protected void searchCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("search-roozhaye-sal.aspx");
    }


    protected void Clear()
    {
        txtEndDate.Text = "";
        txtEndLunch.Text = "";
        txtEndWork.Text = "";
        txtStartDate.Text = "";
        txtStartLunch.Text = "";
        txtStartWork.Text = "";
    }

    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        if (!lblGuide.Visible)
        {
            lblGuide.Visible = true;
        }
        else if (lblGuide.Visible)
        {
            lblGuide.Visible = false;
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnClear.Visible = true;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }


    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("search-roozhaye-sal.aspx");
    }
}