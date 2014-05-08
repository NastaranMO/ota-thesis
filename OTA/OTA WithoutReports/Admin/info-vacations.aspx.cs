using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTA_DBModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
public partial class Admin_info_vacations : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        //visible
        lblGuide.Visible = false;
        btnBack.Visible = false;
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
            //visible
            btnAdd.Visible = true;
            btnClear.Visible = true;
            //multiView
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            //fill ddlYear
            IEnumerable<Year> years = (from y in db.Year
                                      select y).OrderByDescending(a=>a.YearName);
            bindClass.bindDropDownList(ddlYear, years, "YearName", "YearId");
            ddlYear.Items.Add("- انتخاب کنید -");
            ddlYear.SelectedValue = "- انتخاب کنید -";
            //fill ddlviewYear
            bindClass.bindDropDownList(ddlViewYear, years, "YearName", "YearId");
            ddlViewYear.Items.Add("- انتخاب کنید -");
            ddlViewYear.SelectedValue = "- انتخاب کنید -";
            //fill ddlvacations
            FillDdlVacation();
        }
        lblErrorYear.Visible = false;

    }
    //fill ddlVacations
    protected void FillDdlVacation()
    {
        IEnumerable<InDirectCodes> vacations = from d in db.InDirectCodes
                                               where d.IdcTypeId == 2
                                               select d;
        bindClass.bindDropDownList(ddlVacation, vacations, "IdcName", "IdcId");
        ddlVacation.Items.Add("- انتخاب کنید -");
        ddlVacation.SelectedValue = "- انتخاب کنید -";
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        gvVacInfo.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن تنظیمات جدید";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvVacInfo.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
        FillGrid();

    }
    //menu delete
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        lblGuide.Visible = true;
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        gvVacInfo.SelectedIndex = -1;
        lblLegenName.Text = "لیست تنظیمات انواع مرخصی";
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
    }
    //Clear TextBoxes
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        btnClear.Visible = true;
    }
    //method For Clear TextBoxes
    protected void Clear()
    {
        txtMaxYear.Text = "";
        txtNote.Text = "";
        txtTransfer.Text = "";
        txtInMonth.Text = "";
        ddlVacation.SelectedValue = "- انتخاب کنید -";
       
    }
    //btn Back in Search Page
    protected void btnBack_Click(object sender, EventArgs e)
    {
        gvVacInfo.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
    }
    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void cvAdd_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlVacation.SelectedValue == "- انتخاب کنید -" || ddlYear.SelectedValue == "- انتخاب کنید -")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            try
            {
                imgCustomError.Visible = false;
                args.IsValid = true;

                int vacId = Convert.ToInt32(ddlVacation.SelectedItem.Value);
                int yearId = Convert.ToInt32(ddlYear.SelectedItem.Value);

                var tekrari = (from v in db.Vac_Info
                               where v.VacId == vacId && v.YearId == yearId
                               select v).Count();
                if (tekrari > 0)
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    if (tekrari == 0)
                    {
                        //Response.Write(tekrari.ToString());
                        Vac_Info vacInfo = new Vac_Info();
                        vacInfo.VacId = vacId;
                        vacInfo.YearId = yearId;
                        if (txtMaxYear.Text != "")
                            vacInfo.MaxInYear = Convert.ToDouble(txtMaxYear.Text);
                        else
                            throw new Exception("saal");
                        if (ddlMonth.SelectedItem.Text == "سال")
                        {
                            vacInfo.Period = 1;
                            vacInfo.MaxInPeriod = Convert.ToInt32(txtMaxYear.Text);
                        }
                        else
                        {
                            float period = Convert.ToInt32(ddlMonth.SelectedValue);
                            float maxYear = Convert.ToInt32(txtMaxYear.Text);
                            float result = maxYear / period;
                            string r = result.ToString();
                            int dot = r.IndexOf(".");
                            r = r.Substring(0, dot + 3);
                            vacInfo.Period = Convert.ToByte(ddlMonth.SelectedValue);
                            vacInfo.MaxInPeriod = Convert.ToDouble(r);

                        }
                        if (txtTransfer.Text == "")
                            vacInfo.MaxTransfer = 0;
                        else
                            vacInfo.MaxTransfer = Convert.ToDouble(txtTransfer.Text);
                        if (txtNote.Text == "")
                            vacInfo.VacNote = "-";
                        else
                            vacInfo.VacNote = txtNote.Text;
                        db.AddToVac_Info(vacInfo);
                        db.SaveChanges();
                        int numVacInfoID = (from v in db.Vac_Info
                                            where v.YearId == yearId && v.VacId == vacId
                                            select v.VacInfoId).Count();
                        if (numVacInfoID > 0 && txtMaxYear.Text != "")
                        {
                            Vac_Info vInfoId = (from v in db.Vac_Info
                                                where v.YearId == yearId && v.VacId == vacId
                                                select v).Single();
                            RemainVacs r = new RemainVacs();
                            r.FillRemainVacs(vInfoId.VacInfoId, vInfoId.MaxInYear);
                        }

                        imageSuccess.Visible = true;
                        lblMessage.Visible = true;
                        MultiView1.ActiveViewIndex = -1;
                        MultiView2.ActiveViewIndex = 0;
                        lblMessage.Text = "پیام سیستم";
                        errorOl.InnerHtml = "<li>" +
                              "اطلاعات " + " " + "<b>" + ddlVacation.SelectedItem.Text + "</b>" +
                            " " + "برای سال " + "<b>" + ddlYear.SelectedItem.Text + "</b>" +
                            " " + "با موفقیت در سیستم ثبت شد." +
                            "</li>";
                    }
                }


            }
            catch (Exception)
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                imageError.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml =
    "<li>اطلاعات عملیات وارد شده ممکن است، قبلا در پایگاه داده ثبت شده باشد.</li>" +

                    "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>" +
                    "<li>ممکن است یکی از فیلدها را به درستی وارد نکرده باشید.</li>";
            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        btnClear.Visible = true;
    }
    //lnkViewGrid
    protected void lnkView_Click(object sender, EventArgs e)
    {
        gvVacInfo.SelectedIndex = -1;
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        listGrid.InnerHtml = "- لیست اطلاعات انواع مرخصی در سال" + " " + "<b style='color:red;'>" +
            ddlViewYear.SelectedItem.Text + "</b>" + " -";
    }
    //fill Grid
    protected void FillGrid()
    {
        int yearId = Convert.ToInt32(ddlViewYear.Items[0].Value); ;
        if (ddlViewYear.SelectedValue != "- انتخاب کنید -")
        {
            yearId = Convert.ToInt32(ddlViewYear.SelectedItem.Value);
        }
        var query = from v in db.Vac_Info
                    where v.YearId == yearId
                    select new
                    {
                        v.VacNote,
                        v.VacInfoId,
                        v.MaxInPeriod,
                        v.MaxInYear,
                        v.MaxTransfer,
                        v.Period,
                        v.InDirectCodes.IdcName
                    };
        int number = query.Count();
        listGrid.InnerHtml = "- لیست اطلاعات انواع مرخصی در سال" + " " + "<b style='color:red;'>" +
            ddlViewYear.SelectedItem.Text + "</b>" + " -";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvVacInfo, query);
    }
    protected void gvDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvVacInfo.SelectedValue);
        Vac_Info vacs = (from d in db.Vac_Info
                         where d.VacInfoId == index
                         select d).Single();
        lblVacInfoId.Text = index.ToString();
        ddlYear.SelectedItem.Value = vacs.YearId.ToString();
        ddlVacation.SelectedItem.Value = vacs.VacId.ToString();
        if (vacs.Period != 1)
            ddlMonth.SelectedItem.Value = vacs.Period.ToString();
        else
            ddlMonth.SelectedItem.Text = "سال";
        txtMaxYear.Text = vacs.MaxInYear.ToString();
        txtInMonth.Text = vacs.MaxInPeriod.ToString();
        txtTransfer.Text = vacs.MaxTransfer.ToString();
        txtNote.Text = vacs.VacNote;
        btnEdit.Visible = true;
        btnDisEdit.Visible = false;
        btnAdd.Visible = false;
        btnBack.Visible = true;
        btnClear.Visible = false;
        MultiView1.ActiveViewIndex = 0;
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int vacId = Convert.ToInt32(ddlVacation.SelectedItem.Value);
        int yearId = Convert.ToInt32(ddlYear.SelectedItem.Value);
        int vacInfoId = Convert.ToInt32(lblVacInfoId.Text);

        btnBack.Visible = true;

        if (ddlYear.SelectedValue == "- انتخاب کنید -" && ddlVacation.SelectedValue == "- انتخاب کنید -")
        {
            args.IsValid = true;
            imgCustomEdit.Visible = true;
        }
        else
        {
            try
            {
                args.IsValid = true;
                imgCustomEdit.Visible = true;

                int tekrari = (from t in db.Vac_Info
                               where t.VacId == vacId && t.YearId == yearId && t.VacInfoId != vacInfoId
                               select t).Count();
                if (tekrari > 0)
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    Vac_Info vacInfo = (from v in db.Vac_Info
                                        where v.VacInfoId == vacInfoId
                                        select v).Single();
                    List<RemainVacations> rev = (from d in db.RemainVacations
                                                 where d.VacInfoId == vacInfo.VacInfoId && d.RemainVac != vacInfo.MaxInYear
                                                 select d).ToList();
                    if (rev.Count == 0)
                    {
                        vacInfo.VacId = vacId;
                        vacInfo.YearId = yearId;
                        vacInfo.MaxInYear = Convert.ToInt32(txtMaxYear.Text);
                        if (ddlMonth.SelectedItem.Text == "سال")
                        {
                            vacInfo.Period = 1;
                            vacInfo.MaxInPeriod = Convert.ToInt32(txtMaxYear.Text);
                        }
                        else
                        {
                            float period = Convert.ToInt32(ddlMonth.SelectedValue);
                            float maxYear = Convert.ToInt32(txtMaxYear.Text);
                            float result = maxYear / period;
                            string r = result.ToString();
                            int dot = r.IndexOf(".");
                            r = r.Substring(0, dot + 3);
                            vacInfo.Period = Convert.ToByte(ddlMonth.SelectedValue);
                            vacInfo.MaxInPeriod = Convert.ToDouble(r);
                        }
                        if (txtTransfer.Text == "")
                            vacInfo.MaxTransfer = 0;
                        else
                            vacInfo.MaxTransfer = Convert.ToInt32(txtTransfer.Text);
                        if (txtNote.Text == "")
                            vacInfo.VacNote = "-";
                        else
                            vacInfo.VacNote = txtNote.Text;

                        db.SaveChanges();
                        imageSuccess.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "پیام سیستم";
                        errorOl.InnerHtml = "<li>" + "اطلاعات  " + "<b>" + ddlVacation.SelectedItem.Text + "</b>" +
                            " " + "با موفقیت ویرایش شد."
                            + "</li>";
                        MultiView1.ActiveViewIndex = -1;
                        MultiView2.ActiveViewIndex = 0;
                    }
                    else
                        throw new Exception("a");

                }
            }
            catch (Exception)
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                imageError.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml =
    "<li>رکوردی با این اطلاعات در پایگاه داده وجود دارد.</li>" +
                    "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>" +
    "<li>اطلاعات این نوع مرخصی برای تمامی پرسنل ثبت شده است و آنها از مرخصی خود استفاده کرده اند،" +
    "<br>" + "بهمین منظور شما قادر به ویرایش اطلاعات این نوع مرخصی نخواهید بود.</li>"
    ;
            }
        }

    }
    protected void gvDirectCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        int vacInfoId = Convert.ToInt32(gvVacInfo.DataKeys[index].Value);
        try
        {
            Vac_Info vacInfo = (from d in db.Vac_Info
                                where d.VacInfoId == vacInfoId
                                select d).Single();
            db.DeleteObject(vacInfo);
            db.SaveChanges();
            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>اطلاعات " +
    "<b>" + ddlVacation.SelectedItem.Text + "</b> " +
    "با موفقیت از سیستم حذف شد." +
    "</li>";
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
        }
        catch (Exception)
        {
            imageError.Visible = true;
            lblMessage.Visible = true;
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>این اطلاعات برای تمامی پرسنل ثبت شده و امکان حذف آن وجود ندارد.</li>" +
                "<li>این اطلاعات قبلا از سیستم حذف شده است.</li>" +
           "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>";

        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnBack.Visible = true;
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void lnkNewYear_Click(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        lnkNewYear.Visible = false;
        txtnewYear.Visible = true;
        lnkSaveNewYear.Visible = true;
        lblNewYear.Visible = true;
        lnkCancel.Visible = true;

    }
    protected void lnkSaveNewYear_Click(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        lblErrorYear.Text = "";
        if (txtnewYear.Text == "")
        {
            lblErrorYear.Visible = true;
            lblErrorYear.Text = "* لطفا سال جدید را وارد کنید.";
        }
        else
        {
            try
            {
                int tekrari = (from y in db.Year
                               where y.YearName == txtnewYear.Text
                               select y).Count();
                if (tekrari > 0)
                {
                    Year tekrariYear = (from y in db.Year
                                        where y.YearName == txtnewYear.Text
                                        select y).Single();
                    lblErrorYear.Visible = true;
                    lblErrorYear.Text = "سال " + "<b>" + txtnewYear.Text + "</b>" + " " +
                        "در پایگاه داده موجود است.";
                }
                else
                {
                    Year year = new Year();
                    year.YearName = txtnewYear.Text;
                    db.AddToYear(year);
                    db.SaveChanges();
                    txtnewYear.Visible = false;
                    lnkSaveNewYear.Visible = false;
                    lblNewYear.Visible = false;
                    lblErrorYear.Visible = true;
                    lblErrorYear.Text = "* سال" + " " + "<b>" + txtnewYear.Text + "</b>" + " " +
                        "با موفقیت در سیستم ثبت شد.";
                    IEnumerable<Year> newy = from d in db.Year
                                             select d;
                    ddlYear.Items.Clear();
                    bindClass.bindDropDownList(ddlYear, newy, "YearName", "YearId");
                    ddlYear.SelectedItem.Text = txtnewYear.Text;

                }
            }
            catch (Exception)
            {
                lblErrorYear.Visible = true;
                lblErrorYear.Text = "* خطا در بررقراری ارتباط با پایگاه داده !";

            }
        }
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        lblErrorYear.Visible = false;
        txtnewYear.Visible = false;
        lnkSaveNewYear.Visible = false;
        lblNewYear.Visible = false;
        lnkNewYear.Visible = true;
        lnkCancel.Visible = false;
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        if (txtMaxYear.Text != "")
        {
            float period = Convert.ToInt32(ddlMonth.SelectedValue);
            float maxYear = Convert.ToInt32(txtMaxYear.Text);
            float result = maxYear / period;
            string r = result.ToString();
            int dot = r.IndexOf(".");
            r = r.Substring(0, dot + 3);
            txtInMonth.Text = r;
        }
        else
        {
            txtInMonth.Text = txtMaxYear.Text;
        }

    }
    protected void txtMaxYear_TextChanged(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        if (txtMaxYear.Text != "")
        {
            float period = Convert.ToInt32(ddlMonth.SelectedValue);
            float maxYear = Convert.ToInt32(txtMaxYear.Text);
            float result = maxYear / period;
            string r = result.ToString();
            int dot = r.IndexOf(".");
            r = r.Substring(0, dot + 3);
            txtInMonth.Text = r;
        }
    }

}