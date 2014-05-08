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

public partial class User_direct : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    int pId;
    protected void Page_Load(object sender, EventArgs e)
    {
        pId = Convert.ToInt32(Profile.personelId);
        ViewState["pidd"] = pId;
        //visible
        lblGuide.Visible = false;
        btnBack.Visible = false;
        btnClear.Visible = false;
        imgCustomError.Visible = false;
        lblMessage.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;
        lblSearchMessage.Visible = false;
        MultiView2.ActiveViewIndex = -1;
        //fill lblTarikh
        lblTarikh.Text = DateTime.Now.ToShortDateString();
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
            //fill ddlCode
            FillDdlCodes();
        }
        pId = Convert.ToInt32(ViewState["pidd"]);

    }
    //fill ddlCodes
    private void FillDdlCodes()
    {
        int departmentID;
        int JobId;
        Personals p = (from i in db.Personals
                       where i.PersonalID == pId
                       select i).Single();
        JobId = p.JobId;
        departmentID = p.DepId;
        var dcDepJob = from d in db.DcDepJob
                       where d.Departman_Job.DepId == departmentID
                       && d.Departman_Job.JobId == JobId
                       select new
                       {
                           d.DirectCodes.DcId,
                           d.DirectCodes.DcName
                       };

        bindClass.bindDropDownList(ddlCode, dcDepJob, "DcName", "DcId");
        ddlCode.Items.Add("- انتخاب کنید -");
        ddlCode.SelectedValue = "- انتخاب کنید -";
    }
    //menu edit
    protected void editeCode_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        Clear();
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "ویرایش عملیات ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("edit");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
   
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "ثبت وظایف";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnSearch.Visible = false;
      
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        Clear();
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
      
        btnEdit.Visible = false;
        btnClear.Visible = true;

    }
    //menu delete
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        Clear();
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "حذف عملیات ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("delete");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
    
        btnEdit.Visible = false;
        btnClear.Visible = true;

    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {

        gvDirectCode.SelectedIndex = -1;
        lblLegenName.Text = "لیست وظایف انجام شده";
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
    }
    //Clear TextBoxes
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblErrorTable.Visible = false;
        Clear();
        btnClear.Visible = true;
    }
    //method For Clear TextBoxes
    protected void Clear()
    {
        txtNote.Text = "";
        txtStartHour.Text = "";
        txtStartMinute.Text = "";
        txtEndMinute.Text = "";
        txtEndHour.Text = "";
        ddlCode.SelectedValue = "- انتخاب کنید -";
    }
    //btn Back in Search Page
    protected void btnBack_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
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
        if (txtEndHour.Text == "" || txtEndMinute.Text == "" || txtStartMinute.Text == "" || txtStartHour.Text == "" ||
            ddlCode.SelectedItem.Text == "- انتخاب کنید -")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;

        }

        else
        {
            args.IsValid = true;

            SetTime endTime = new SetTime(txtEndHour.Text + ":" + txtEndMinute.Text);
            SetTime startTime = new SetTime(txtStartHour.Text + ":" + txtStartMinute.Text);
            TimeSpan eT = endTime.GetTime();
            TimeSpan sT = startTime.GetTime();
            if (sT < eT)
            {
                lblErrorTable.Visible = false;
                int numOfRecord;
                DateTime selectDate = Convert.ToDateTime(lblTarikh.Text);
                List<Personals_DirectCode> personalDirect = (from p in db.Personals_DirectCode
                                                             where p.DaysOfYear.Tarikh == selectDate &&
                                                             p.PerId == pId
                                                             select p).ToList();
                if (personalDirect.Count() == 0)
                {
                    addToDb(sT, eT);
                }
                else
                {
                    personalDirect = (from d in db.Personals_DirectCode
                                        where d.DaysOfYear.Tarikh == selectDate &&
                                        d.PerId == pId
                                        select d).ToList();
                    numOfRecord = personalDirect.Count();
                    bool checks = true;

                    for (int i = 0; i < numOfRecord; i++)
                    {
                        TimeSpan pST = personalDirect[i].StartTime;
                        TimeSpan pET = personalDirect[i].EndTime;
                        if (pST >= sT && eT > pST)
                        {
                            checks = false;
                            break;
                        }
                        else
                        {
                            if (pET > sT && pET <= eT)
                            {
                                checks = false;
                                break;
                            }
                        }
                    }
                    try
                    {
                        if (checks)
                        {
                            lblErrorTable.Visible = false;
                            addToDb(sT, eT);
                        }
                        else
                        {
                            throw new Exception("tadakhole zamani");
                        }
                    }
                    catch (Exception)
                    {
                        lblErrorTable.Visible = true;
                        lblErrorTable.InnerHtml = "* تداخل زمانی در ثبت اطلاعات وجود دارد.";
                    }
                }
            }
            else
            {
                lblErrorTable.Visible = true;
                lblErrorTable.InnerHtml = "* ساعات وارد شده معتبر نمی باشد.لطفا مجددا سعی کنید.";
            }
        }

    }
    //
    protected void addToDb(TimeSpan st, TimeSpan et)
    {
        DateTime t= Convert.ToDateTime(lblTarikh.Text);

            int days = (from d in db.DaysOfYear
                        where d.Tarikh == t
                        select d.dayId).Single();
            //try
            //{
                Personals_DirectCode pd = new Personals_DirectCode();
                pd.PerId = pId;
                pd.dayId = days;
                pd.StartTime = st;
                pd.EndTime = et;
                pd.DcId = Convert.ToInt32(ddlCode.SelectedValue);
                if (txtNote.Text != "")
                    pd.Note = txtNote.Text;
                else
                    pd.Note = "-";
                pd.DcsId = false;
                    pd.dayId = days;
                db.AddToPersonals_DirectCode(pd);
                db.SaveChanges();
                imageSuccess.Visible = true;
                lblMessage.Visible = true;
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم";
                errorOl.InnerHtml = "<li>" +
                    "عملیات  " + "<b>" + ddlCode.SelectedItem.Text + "</b>" +
                    " " + "با موفقیت برای شما در پایگاه داده ثبت شد." +
                    "</li>";
            //}

            //catch (Exception)
            //{
            //    MultiView2.ActiveViewIndex = 0;
            //    MultiView1.ActiveViewIndex = -1;
            //    imageError.Visible = true;
            //    lblMessage.Visible = true;
            //    lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            //    errorOl.InnerHtml =
            //        "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
            //}


        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        btnClear.Visible = true;
    }
    //lnkViewGrid
    protected void lnkView_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
    }
    //fill Grid
    protected void FillGrid()
    {
        //try
        //{
            DateTime time;
            if (txtShowTarikh.Text != "")
                time = Convert.ToDateTime(txtShowTarikh.Text);
            else
            {
                time = DateTime.Now;
                string date = time.ToShortDateString();
                time = Convert.ToDateTime(date);
            }

            var directCodes = from d in db.Personals_DirectCode
                              where d.DaysOfYear.Tarikh == time && d.PerId == pId 
                              select new
                              {
                                  d.PerId,
                                  d.PdcId,
                                  d.Note,
                                  d.StartTime,
                                  d.DaysOfYear.Tarikh,
                                  d.EndTime,
                                  d.DirectCodes.DcName,
                                  d.CodeState.DscName
                              };
            int number = directCodes.Count();
            listGrid.InnerHtml = "- لیست وظایف انجام شده در تاریخ" + " " +
                "<b style='color:red;font-family:B nazanin;font-size:13px;'>"
                + time.ToShortDateString() + "</b>" + " -";
            lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
            bindClass.bindGrid(gvDirectCode, directCodes);
        //}
        //catch (Exception)
        //{
        //    MultiView1.ActiveViewIndex = -1;
        //    MultiView2.ActiveViewIndex = 0;
        //    imageError.Visible = true;
        //    lblMessage.Visible = true;
        //    lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
        //    errorOl.InnerHtml =
        //        "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        //}
    }
    protected void gvDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        
        int index = Convert.ToInt32(gvDirectCode.SelectedValue);
        Personals_DirectCode pc = (from p in db.Personals_DirectCode
                                   where p.PdcId == index
                                   select p).Single();
        if (pc.DcsId == false)
        {
            ViewState["id"] = index;
            string startTime = pc.StartTime.ToString();
            string endTime = pc.EndTime.ToString();
            lblTarikh.Text = pc.DaysOfYear.Tarikh.ToShortDateString();
            ddlCode.SelectedValue = pc.DcId.ToString();
            txtNote.Text = pc.Note;
            txtStartHour.Text = startTime.Substring(0, 2);
            txtStartMinute.Text = startTime.Substring(2, 3);
            txtEndHour.Text = endTime.Substring(0, 2);
            txtEndMinute.Text = endTime.Substring(2, 3);
            btnEdit.Visible = true;
            btnSearch.Visible = false;

            btnAdd.Visible = false;
            btnBack.Visible = true;
            MultiView1.ActiveViewIndex = 0;
            lblLegenName.Text = "ویرایش وظایف";
        }
        else
            throw new Exception("");

        }
        catch(Exception)
        {
                        MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            imageError.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml =
                  "<li>این عملیات توسط مدیر بخش به تایید نهایی رسیده و امکان ویرایش آن توسط شما وجود ندارد.</li>"+
                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }

    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int gvRowSelect=Convert.ToInt32(ViewState["id"]);

        if (txtEndHour.Text == "" || txtEndMinute.Text == "" || txtStartMinute.Text == "" || txtStartHour.Text == "" ||
        ddlCode.SelectedItem.Text == "- انتخاب کنید -")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;

        }

        else
        {
            args.IsValid = true;

            SetTime endTime = new SetTime(txtEndHour.Text + ":" + txtEndMinute.Text);
            SetTime startTime = new SetTime(txtStartHour.Text + ":" + txtStartMinute.Text);
            TimeSpan eT = endTime.GetTime();
            TimeSpan sT = startTime.GetTime();
            if (sT < eT)
            {
                lblErrorTable.Visible = false;
                DateTime selectDate = Convert.ToDateTime(lblTarikh.Text);
                int numOfRecord = 0;
                List<Personals_DirectCode> personaldirect = (from d in db.Personals_DirectCode
                                                                 where d.DaysOfYear.Tarikh == selectDate &&
                                                                 d.PdcId != gvRowSelect &&
                                                                 d.PerId == pId
                                                                 select d).ToList();
                if (personaldirect.Count() == 0)
                {
                    EditToJob(sT, eT);
                }
                else
                {
                    personaldirect = (from d in db.Personals_DirectCode
                                        where d.DaysOfYear.Tarikh == selectDate && d.PdcId != gvRowSelect &&
                                        d.PerId == pId
                                        select d).ToList();
                    numOfRecord = personaldirect.Count();
                    bool checks = true;

                    for (int i = 0; i < numOfRecord; i++)
                    {
                        TimeSpan pST = personaldirect[i].StartTime;
                        TimeSpan pET = personaldirect[i].EndTime;
                        if (pST >= sT && eT > pST)
                        {
                            checks = false;
                            break;
                        }
                        else
                        {
                            if (pET > sT && pET <= eT)
                            {
                                checks = false;
                                break;
                            }
                        }
                    }
                    try
                    {
                        if (checks)
                        {
                            EditToJob(sT, eT);
                        }
                        else
                        {
                            throw new Exception("tadakhole zamani");
                        }
                    }
                    catch (Exception)
                    {
                        lblErrorTable.Visible = true;
                        lblErrorTable.InnerHtml = "* تداخل زمانی در ویرایش اطلاعات وجود دارد.";
                    }
                }
            }
            else
            {
                lblErrorTable.Visible = true;
                lblErrorTable.InnerHtml = "* ساعات وارد شده معتبر نمی باشد.لطفا مجددا سعی کنید.";
            }
        }
    }
    protected void EditToJob(TimeSpan st, TimeSpan et)
    {
        //try
        //{
            int dcId = Convert.ToInt32(ddlCode.SelectedValue);
            int gvRowSelect = Convert.ToInt32(ViewState["id"]);
            Personals_DirectCode pc = (from p in db.Personals_DirectCode
                                       where p.PdcId == gvRowSelect
                                       select p).Single();
            if (txtNote.Text != "")
                pc.Note = txtNote.Text;
            else
                pc.Note = "-";
            pc.StartTime = st;
            pc.EndTime = et;
            pc.DcId = dcId;
            db.SaveChanges();
            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>" +
                "عملیات  " + "<b>" + ddlCode.SelectedItem.Text + "</b>" +
                " " + "با موفقیت برای شما در پایگاه داده ویرایش شد." +
                "</li>";
        //}
        //catch (Exception)
        //{
        //    MultiView2.ActiveViewIndex = 0;
        //    MultiView1.ActiveViewIndex = -1;
        //    imageError.Visible = true;
        //    lblMessage.Visible = true;
        //    lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
        //    errorOl.InnerHtml =
        //        "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        //}
    }
    protected void gvDirectCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        int dCId = Convert.ToInt32(gvDirectCode.DataKeys[index].Value);

        //try
        //{
            int number = (from d in db.Personals_DirectCode
                          where d.PdcId == dCId
                          select d).Count();
            if (number > 0)
            {
                Personals_DirectCode dc = (from d in db.Personals_DirectCode
                                           where d.PdcId == dCId
                                           select d).Single();
                string idcname=dc.DirectCodes.DcName;
                db.DeleteObject(dc);
                db.SaveChanges();
                imageSuccess.Visible = true;
                lblMessage.Visible = true;
                imageError.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم";
                errorOl.InnerHtml = "<li>وظیفه ی " +
        "<b>" +idcname+ "</b> " +
        "با موفقیت برای شما از سیستم حذف شد." +
        "</li>";

            }
        //}
        //catch (Exception)
        //{
        //    imageError.Visible = true;
        //    lblMessage.Visible = true;
        //    MultiView2.ActiveViewIndex = 0;
        //    MultiView1.ActiveViewIndex = -1;
        //    lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
        //    errorOl.InnerHtml = "<li>این وظیفه قبلا از سیستم حذف شده است.</li>" +
        //   "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>";

        //}
        

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnBack.Visible = true;
    }
    protected void lnkShowDirectCode_Click(object sender, EventArgs e)
    {
        if (txtShowTarikh.Text == "")
        {
            divErrorShow.Visible = true;
            divErrorShow.InnerHtml = "* برای نمایش لیست عملیات یک تاریخ معتبر را انتخاب کنید.";
        }
        FillGrid();
    }
}