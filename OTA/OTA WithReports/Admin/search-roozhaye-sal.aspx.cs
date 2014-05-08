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

public partial class Admin_search_roozhaye_sal : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblGuide.Visible = false;
        if (!IsPostBack)
        {
            lblGuideClass gc = new lblGuideClass();
            lblGuide.Text = gc.fillLblGuide("SpecialSearch");
        }
    }
   

    //protected void gvDaysOfYear_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int index = e.RowIndex;
    //    int DayID = (int)gvDaysOfYear.DataKeys[index].Value;
    //    try
    //    {
    //        DaysOfYear doy = db.DaysOfYear.Where(a => a.dayId == DayID).Single();
    //        db.DeleteObject(doy);
    //        db.SaveChanges();

    //        imageSuccess.Visible = true;
    //        lblMessage.Visible = true;
    //        lblMessage.Text = "پیام سیستم";
    //        errorOl.InnerHtml = "<li>عملیات حذف روزهای سال با کد " +
    //"<b>" + doy.dayId + "</b> " +
    //"با موفقیت انجام شد." +
    //"</li>";
    //        MultiView1.ActiveViewIndex = -1;
    //        MultiView2.ActiveViewIndex = 0;

            
    //    }
    //    catch (Exception)
    //    {
    //        imageError.Visible = true;
    //        lblMessage.Visible = true;
    //        MultiView2.ActiveViewIndex = 0;
    //        MultiView1.ActiveViewIndex = -1;
    //        lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
    //        errorOl.InnerHtml = "<li>این مورد قبلا از سیستم حذف شده است.</li>" +
    //       "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>";

    //    }
    //}

    protected void lnkView_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void FillGrid(DateTime sDate , DateTime eDate)
    {
        IEnumerable<utilClass> query = from c in db.DaysOfYear
                                       where c.Tarikh>=sDate && c.Tarikh<=eDate
                                       select new utilClass
                                       {
                                           dayId = c.dayId,
                                           DsName = c.DayState.DsName,
                                           Tarikh = c.Tarikh,
                                           Rooz = c.Rooz
                                       };


        gvDaysOfYear.DataSource = query;
        gvDaysOfYear.DataBind();

        listGrid.InnerHtml = "- لیست روزهای ثبت شده از تاریخ - " +
            "<b>" + sDate.ToShortDateString() + "</b>" +
            "تا تاریخ "+
            "<b>"+eDate.ToShortDateString()+"</b>";
        lblFooter.Text = "تعدا رکوردها : " + query.Count().ToString();
    }

    protected void FillGrid()
    {
        IEnumerable<utilClass> query = from c in db.DaysOfYear                                       
                                       select new utilClass
                                       {
                                           dayId = c.dayId,
                                           DsName = c.DayState.DsName,
                                           Tarikh = c.Tarikh,
                                           Rooz = c.Rooz
                                       };


        gvDaysOfYear.DataSource = query;
        gvDaysOfYear.DataBind();
        listGrid.InnerHtml = "- لیست کامل تمامی روزهای ثبت شده -";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + query.Count().ToString() + "</b>";
    }


    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
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

    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime sDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime eDate = Convert.ToDateTime(txtEndDate.Text);
            ViewState["sDate"] = sDate;
            ViewState["eDate"] = eDate;

            FillGrid(sDate, eDate);
        }
        catch
        {
            FillGrid();
        }
    }

    protected void gvDaysOfYear_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int index = e.NewSelectedIndex;
        int dayId = (int)gvDaysOfYear.DataKeys[index].Value;

        Response.Redirect("edit-roozhaye-sal.aspx?id=" + dayId);

    }

    protected void gvDaysOfYear_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDaysOfYear.PageIndex = e.NewPageIndex;
        try
        {
            DateTime sDate = (DateTime)ViewState["sDate"];
            DateTime eDate = (DateTime)ViewState["eDate"];

            FillGrid(sDate, eDate);
        }
        catch
        {
            FillGrid();
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

    protected string GetDate(object dt)
    {
        DateTime date = Convert.ToDateTime(dt);
        return date.ToShortDateString();
    }

    protected void lnkViewAll_Click1(object sender, EventArgs e)
    {
     
        FillGrid();
    }
}