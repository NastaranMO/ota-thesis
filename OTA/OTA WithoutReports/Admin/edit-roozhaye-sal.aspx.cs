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

public partial class Admin_edit_roozhaye_sal : System.Web.UI.Page
{
    int DayId;
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblGuide.Visible = false;
        if (!IsPostBack)
        {
            //fill lblGuide
            lblGuideClass g = new lblGuideClass();
            lblGuide.Text = g.fillLblGuide("edit");


            IEnumerable<DayState> ds = from c in db.DayState
                                       select c;
            bindClass.bindDropDownList(ddlDayState, ds, "DsName", "DsId");

            if (Request.QueryString["id"] != null)
            {
                DayId = Convert.ToInt32(Request.QueryString["id"]);
                DaysOfYear doy = db.DaysOfYear.Where(a => a.dayId == DayId).Single();
                lblDate.Text = doy.Tarikh.ToShortDateString();
                lblDay.Text = doy.Rooz;

                ddlDayState.SelectedValue = doy.DsId.ToString();
                txtStartLunch.Text = doy.StartLunchTime.ToString().Substring(0, 5);
                txtEndLunch.Text = doy.EndLunchTime.ToString().Substring(0, 5);
                txtStartWork.Text = doy.StartWorkTime.ToString().Substring(0, 5);
                txtEndWork.Text = doy.EndWorkTime.ToString().Substring(0, 5);
                ViewState["dayid"] = DayId;
                MultiView1.ActiveViewIndex = 0;

            }
            else
            {
                Response.Redirect("search-roozhaye-sal.aspx");
            }
        }
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {

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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int dsId = Convert.ToInt32(ddlDayState.SelectedItem.Value);
        TimeSpan StartLunchTime = new TimeSpan();
        TimeSpan EndLunchTime = new TimeSpan();
        TimeSpan StartWorkTime = new TimeSpan();
        TimeSpan EndWorkTime = new TimeSpan();

        try
        {
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

            DayId = (int)ViewState["dayid"];
            DaysOfYear doy = db.DaysOfYear.Where(a => a.dayId == DayId).Single();
            doy.DsId = Convert.ToInt32(ddlDayState.SelectedItem.Value);
            doy.StartLunchTime = StartLunchTime;
            doy.EndLunchTime = EndLunchTime;
            doy.StartWorkTime = StartWorkTime;
            doy.EndWorkTime = EndWorkTime;
            db.SaveChanges();

            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>" +
                "اطلاعات با موفقیت در پایگاه داده ذخیره شد." +
                "</li>";

        }
        catch
        {
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            imageError.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";

            errorOl.InnerHtml = "<li>اطلاعات ایام وارد شده ممکن است، قبلا در پایگاه داده ثبت شده باشد.</li>" +
                                "<li>ساعت شروع کار یا شروع نهار باید کوچکتر از ساعت پایان آن باشد.</li>" +
                                "<li>تاریخ شروع دوره باید کوچکتر از تاریخ پایان دوره باشد.</li>" +
                                 "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";

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
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("search-roozhaye-sal.aspx");
    }
  
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEndLunch.Text = "";
        txtEndWork.Text = "";
        txtStartLunch.Text = "";
        txtStartWork.Text = "";
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {

    }
}