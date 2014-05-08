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

public partial class Supervisors_inDirect : System.Web.UI.Page
{

    OTA_DBEntities db = new OTA_DBEntities();
    int pId;
    int depId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //visible
        divError2.Visible = false;
        divError.Visible = false;
        lblGridError.Visible = false;
        lblGuide.Visible = false;
        btnBack.Visible = false;
        btnClear.Visible = false;
        imgCustomError.Visible = false;
        lblMessage.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;
        ddlInDirectCodes.Enabled = false;
        MultiView2.ActiveViewIndex = -1;
        if (!IsPostBack)
        {
            pId = Convert.ToInt32(Profile.personelId);
            ViewState["pid"] = pId;

            depId = (from d in db.Personals
                     where d.PersonalID == pId
                     select d.DepId).Single();
            ViewState["d"] = depId;
            //fill ddlTarikh
            FillddlTarikh();
            ddlTarikh.Items.Add("- انتخاب کنید -");
            ddlTarikh.SelectedValue = "- انتخاب کنید -";
            //fill lblGuide
            lblGuideClass c = new lblGuideClass();
            lblGuide.Text = c.fillLblGuide("add");
            //visible
            btnAdd.Visible = true;
            btnClear.Visible = true;
            //multiView
            FillGrid();
            MultiView1.ActiveViewIndex = 3;
            MultiView2.ActiveViewIndex = -1;
            //fill ddlIndirectCodes
            IEnumerable<InDirectCodesType> indirectTypes = from i in db.InDirectCodesType
                                                           select i;
            bindClass.bindDropDownList(ddlInDirectTypes, indirectTypes, "IdcTypeName", "IdcTypeId");
            ddlInDirectTypes.Items.Add("- انتخاب کنید -");
            ddlInDirectTypes.SelectedValue = "- انتخاب کنید -";
            //fill inDirectCodes

            //fillListGrid
            FillListGrid();

        }
        else
        {
            depId = Convert.ToInt32(ViewState["d"]);
            pId = Convert.ToInt32(ViewState["pid"]);
        }
        if (ddlInDirectTypes.SelectedItem.Text != "- انتخاب کنید -")
            ddlInDirectCodes.Enabled = true;
        else
            ddlInDirectCodes.Enabled = false;

        //Response.Write(pId + "   " + depId);
    }
    //Fill ddlTarikh
    protected void FillddlTarikh()
    {
        DateTime dtNow = DateTime.Now;
        string yearID = DateTime.Now.Year.ToString();
        InDirect inDirect = new InDirect(depId);
        int[] dayId = inDirect.ReturnDayId();
        if (dayId[0] == -1)
        {
            List<DaysOfYear> d = (from p in db.DaysOfYear
                                  where p.Year.YearName == yearID && p.Tarikh <= dtNow
                                  select p).OrderBy(a => a.Tarikh).ToList();
            int number = d.Count();
            for (int i = 0; i < number; i++)
            {
                ddlTarikh.Items.Add(d[i].Tarikh.ToShortDateString());
            }
        }
        else
        {
            int num = dayId.Length;
            int tool = 0;
            for (int i = 0; i < num; i++)
            {
                if (dayId[i] != 0)
                    tool++;
            }

            for (int i = 0; i < tool; i++)
            {
                int j = dayId[i];
                DaysOfYear tarikh = (from t in db.DaysOfYear
                                     where t.dayId == j
                                     select t).Single();
                ddlTarikh.Items.Add(tarikh.Tarikh.ToShortDateString());
            }
        }
    }
    //menu Edit
    protected void editeCode_Click(object sender, EventArgs e)
    {

        gvInDirectCode.SelectedIndex = -1;
        gvList.EditIndex = -1;
        txtShowTarikh.Text = "";
        MultiView1.ActiveViewIndex = 3;
        lblLegenName.Text = "ویرایش حضور و غیاب ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = "برای انجام ویرایش اگر عملیات شامل مرخصی باشد و بخواهید نوع آن را تغییر دهید باید آن را حذف کرده و مجددا آن را ثبت کنید." +
            "در غیر اینصورت ویرایش عملیات دیگر مجاز است.";
        btnAdd.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
        lblGuide.Visible = true;
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        divError2.Visible = false;
        Clear();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "ثبت ورود و خروج ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = "برای ثبت حضور و غیاب هر پرسنل ابتدا یک تاریخ را انتخاب کرده سپس از داخل لیست پرسنلی یک نفر را انتخاب کنید.";
        btnAdd.Visible = true;
        btnEdit.Visible = false;
        btnClear.Visible = true;
        gvInDirectCode.SelectedIndex = -1;
        gvList.EditIndex = -1;
        ddlTarikh.Items.Clear();
        FillddlTarikh();
        ddlTarikh.Items.Add("- انتخاب کنید -");
        ddlTarikh.SelectedValue = "- انتخاب کنید -";

    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvList.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 3;
        FillListGrid();
        lblLegenName.Text = "جستجو ساده";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
        gvInDirectCode.SelectedIndex = -1;
        gvList.EditIndex = -1;
    }
    //lnk Rahnama
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }
    //lnk view List Hozor&&Ghiab
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        gvList.SelectedIndex = -1;
        lblLegenName.Text = "لیست حضور و غیاب";
        FillListGrid();
        MultiView1.ActiveViewIndex = 3;
        if (txtShowTarikh.Text == "")
        {
            DateTime dt = DateTime.Now;
            int number = (from c in db.DaysOfYear
                          where c.Tarikh == dt
                          select c).Count();
            if (number > 0)
            {
                List<Personals_InDirectCode> pc = (from d in db.Personals_InDirectCode
                                                   where d.DaysOfYear.Tarikh == dt && d.Personals.DepId == depId
                                                   && d.DcsId == false 
                                                   select d).ToList();
                if (pc.Count() > 0)
                    imgConfirm.Visible = true;
            }
            imgConfirm.Visible = false;
        }
    }
    //btn Clear TextBoxes
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        btnClear.Visible = true;
    }
    //method For Clear TextBoxes
    protected void Clear()
    {
        txtNote.Text = "";
        txtStartHour.Text = "";
        txtStartMinute.Text = "";
        txtEndHour.Text = "";
        txtEndMinute.Text = "";
    }
    //btn Back in Search Page
    protected void btnBack_Click(object sender, EventArgs e)
    {
        lblLegenName.Text = "لیست حضور و غیاب";
        gvList.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 3;
        FillListGrid();
    }
    //method GetRow
    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    //method for addToDb
    protected void addToDb(TimeSpan st, TimeSpan et)
    {
        int personelID = Convert.ToInt32(lblPersonelId.Text);
       
        DateTime selectDate = Convert.ToDateTime(lblTarikh.Text);
        int yId = selectDate.Year;
        string yearName=selectDate.Year.ToString();
        int dayId = (from d in db.DaysOfYear
                     where d.Tarikh == selectDate
                     select d.dayId).Single();
        try
        {
            Personals_InDirectCode p = new Personals_InDirectCode();
            p.PerId = personelID;
            p.StartTime = st;
            p.EndTime = et;
            p.dayId = dayId;
            if (txtNote.Text == "")
                p.Note = "-";
            else
                p.Note = txtNote.Text.Replace("ی", "ي");
            p.DcsId = false;

            if (ddlInDirectTypes.SelectedItem.Text != "مرخصی")
            {
                int IdcId = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
                p.IdcId = IdcId;
                db.AddToPersonals_InDirectCode(p);
                db.SaveChanges();
                imageSuccess.Visible = true;
                lblMessage.Visible = true;
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم";
                errorOl.InnerHtml = "<li>" + ddlInDirectCodes.SelectedItem.Text +
                    " " + "برای کد پرسنلی " + "<b>" + personelID.ToString() + "</b>" + " " +
             "از ساعت " + "<b>" + st.ToString() + "</b>" + " " + "تا ساعت " + "<b>" + et.ToString() + "</b>" +
                    " " + "با موفقیت در پایگاه داده ثبت شد." +
                    "</li>";
            }
            else
            {
                if (ddlInDirectTypes.SelectedItem.Text == "مرخصی")
                {
                    bool checkRV = false;
                    string yName = yId.ToString();

                    int vacID = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
                    int vInfoID = (from z in db.Vac_Info
                                   where z.Year.YearName == yName && z.VacId == vacID
                                   select z.VacInfoId).Single();
                    Vac_Info vInfo = (from z in db.Vac_Info
                                      where z.Year.YearName == yName && z.VacId == vacID
                                      select z).Single();
                    p.IdcId = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
                    RemainVacs rv = new RemainVacs(personelID);

                    SetTime calculate = new SetTime();
                    float duringTime = calculate.CalculateTime(st, et);
                    checkRV = rv.CheckVacationTime(selectDate, vInfoID, duringTime);
                    float maxPeriod = (float)vInfo.MaxInPeriod;
                    //Response.Write("a");
                    Response.Write(checkRV.ToString());
                    //try
                    //{
                    //    if (!checkRV)
                    //    {

                    //        throw new Exception("j");

                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    imageSuccess.Visible = true;
                    //    lblMessage.Visible = true;
                    //    MultiView1.ActiveViewIndex = -1;
                    //    MultiView2.ActiveViewIndex = 0;
                    //    lblMessage.Text = "پیام سیستم";
                    //    errorOl.InnerHtml = "<li>" +
                    //        "شخصی با کد پرسنلی  " + "<b>" + personelID.ToString() + "</b>" +

                    //        "بیش از حد مجاز در مدت زمانی مشخص شده" + "(" + vInfo.Period.ToString() + ")" +
                    //        " " + "مرخصی اخذ کرده است." +
                    //        "</li>";
                    //}
                    if (checkRV)
                    {
                        db.AddToPersonals_InDirectCode(p);
                        db.SaveChanges();
                        //Response.Write(rv.VacationEnabled(vInfo.VacInfoId, resultVacs));
                        int vacAdd = (from v in db.Personals_InDirectCode
                                      where v.DaysOfYear.Tarikh == selectDate &&
                                      v.StartTime == st && v.EndTime == et && v.PerId == personelID
                                      select v.PidcId).Single();
                        int vacInfoId = (from v in db.Vac_Info
                                         where v.Year.YearName ==yearName  && v.VacId == vacID
                                         select v.VacInfoId).Single();
                        Personals_Vacations pVacations = new Personals_Vacations();
                        pVacations.PidcId = vacAdd;
                        pVacations.VacInfoId = vacInfoId;
                        db.AddToPersonals_Vacations(pVacations);
                        db.SaveChanges();
                        float s = rv.RefreshRemainVacs(vInfoID, duringTime);
                        Response.Write(s.ToString());

                        imageSuccess.Visible = true;
                        lblMessage.Visible = true;
                        MultiView1.ActiveViewIndex = -1;
                        MultiView2.ActiveViewIndex = 0;
                        lblMessage.Text = "پیام سیستم  ";
                        errorOl.InnerHtml = "<li>" + "<b>" + ddlInDirectCodes.SelectedItem.Text + "</b>" +
                            " " + "برای کد پرسنلی " + "<b>" + personelID.ToString() + "</b>" + " " +
                     "از ساعت " + "<b>" + st.ToString() + "</b>" + " " + "تا ساعت " + "<b>" + et.ToString() + "</b>" +
                            " " + "با موفقیت در پایگاه داده ثبت شد." +
                            "</li>";
                    }
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
"<li>تعداد مرخصی های این پرسنل به حداکثر ساعت مجاز خود زسیده باشد.</li>" +
                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }

    }
    //check Fill TextBoxes For AddToDb
    protected bool checkValidation()
    {
        //if check=true means TextBoxes Fill
        bool check = true;
        if (ddlInDirectCodes.Enabled == false)
        {
            if (txtStartHour.Text == "" || txtStartMinute.Text == "" || txtEndHour.Text == "" ||
               ddlInDirectTypes.SelectedItem.Text == "- انتخاب کنید -" || txtEndMinute.Text == "")
                check = false;
        }
        else
        {
            if (txtStartHour.Text == "" || txtStartMinute.Text == "" || txtEndHour.Text == "" ||
   ddlInDirectTypes.SelectedItem.Text == "- انتخاب کنید -" || txtEndMinute.Text == "" 
               )
                check = false;
            else
                check = true;
        }
        return check;
    }
    //customValidatorAdd
    protected void cvAdd_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int personelId = Convert.ToInt32(lblPersonelId.Text);
        if (!checkValidation())
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            imgCustomError.Visible = false;
            args.IsValid = true;

            SetTime endTime = new SetTime(txtEndHour.Text + ":" + txtEndMinute.Text);
            SetTime startTime = new SetTime(txtStartHour.Text + ":" + txtStartMinute.Text);
            TimeSpan eT = endTime.GetTime();
            TimeSpan sT = startTime.GetTime();
            if (sT < eT)
            {
                lblErrorTable.Visible = false;
                DateTime selectDate = Convert.ToDateTime(lblTarikh.Text);
                int numOfRecord = (from d in db.Personals_InDirectCode
                                   where d.DaysOfYear.Tarikh == selectDate
                                   select d).Count();
                List<Personals_InDirectCode> personalIndirect = (from d in db.Personals_InDirectCode
                                                                 where d.DaysOfYear.Tarikh == selectDate &&
                                                                 d.PerId == personelId
                                                                 select d).ToList();
                if (personalIndirect.Count() == 0)
                {
                    addToDb(sT, eT);
                }
                else
                {
                    personalIndirect = (from d in db.Personals_InDirectCode
                                        where d.DaysOfYear.Tarikh == selectDate &&
                                        d.PerId == personelId
                                        select d).ToList();
                    numOfRecord = personalIndirect.Count();
                    bool checks = true;

                    for (int i = 0; i < numOfRecord; i++)
                    {
                        TimeSpan pST = personalIndirect[i].StartTime;
                        TimeSpan pET = personalIndirect[i].EndTime;
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        btnClear.Visible = true;
    }
    //lnkViewGrid
    protected void lnkView_Click(object sender, EventArgs e)
    {
        gvInDirectCode.SelectedIndex = -1;
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        listGrid.InnerHtml = "- لیست حضور و غیاب -";
    }
    //fill Grid
    protected void FillGrid()
    {

        IEnumerable<Personals> query = (from v in db.Personals
                                        where v.DepId == depId
                                        select v).OrderBy(a => a.PersonalID);

        int number = query.Count();
        listGrid.InnerHtml = "- لیست حضور و غیاب -";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvInDirectCode, query);
    }
    protected void gvInDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvInDirectCode.SelectedValue);
        ViewState["i"] = index;
        //Response.Write(index.ToString());

        if (ddlTarikh.SelectedValue != "- انتخاب کنید -")
        {
            lblGridError.Visible = false;
            Personals p = (from pe in db.Personals
                           where pe.PersonalID == index
                           select pe).Single();
            int dt = DateTime.Now.Year;
            if (ddlInDirectTypes.SelectedItem.Text == "مرخصی")
                ddlInDirectCodes.Enabled = true;
            lblTarikh.Text = ddlTarikh.SelectedValue;
            lblFullName.Text = p.FirstName + " " + p.LastName;
            lblPersonelId.Text = index.ToString();

            Clear();

            btnEdit.Visible = false;
            btnAdd.Visible = true;
            btnBack.Visible = false;
            btnClear.Visible = true;

            MultiView1.ActiveViewIndex = 0;
        }
        else
        {
            divError.Visible = true;
            lblGridError.Visible = true;
            lblGridError.Text = "* برای انتخاب یک کارمند یک تاریخ معتبر را انتخاب کنید.";
            gvInDirectCode.SelectedIndex = -1;
        }

    }
    protected void EditToJob(TimeSpan st, TimeSpan et)
    {
   
        int personelId = Convert.ToInt32(lblPersonelId.Text);
        int idcId = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
        int idcTypeId = Convert.ToInt32(ddlInDirectTypes.SelectedValue);
        int gvListIndex = Convert.ToInt32(ViewState["index"]);
        string year = DateTime.Now.Year.ToString();
        DateTime selectDate = Convert.ToDateTime(lblTarikh.Text);
        int yId = selectDate.Year;
        int dayId = (from d in db.DaysOfYear
                     where d.Tarikh == selectDate
                     select d.dayId).Single();
        try
        {
            int numberp = (from d in db.Personals_InDirectCode
                           where d.PidcId == gvListIndex
                           select d).Count();
            if (numberp > 0)
            {
                Personals_InDirectCode p = (from d in db.Personals_InDirectCode
                                            where d.PidcId == gvListIndex
                                            select d).Single();
                int pidid = p.PidcId;
                if (ddlInDirectTypes.SelectedItem.Text != "مرخصی" && ddlInDirectTypes.SelectedItem.Text != "- انتخاب کنید -")
                {
                    p.StartTime = st;
                    p.EndTime = et;
                    p.IdcId = idcId;
                    db.SaveChanges();
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" + "<b>" + ddlInDirectCodes.SelectedItem.Text + "</b>" +
                        " " + "برای کد پرسنلی " + "<b>" + personelId.ToString() + "</b>" + " " +
                 "از ساعت " + "<b>" + st.ToString() + "</b>" + " " + "تا ساعت " + "<b>" + et.ToString() + "</b>" +
                        " " + "با موفقیت در پایگاه داده ویرایش شد." +
                        "</li>";
                }
                else
                {
                    TimeSpan pst = p.StartTime;
                    TimeSpan pet = p.EndTime;
                    SetTime pt = new SetTime();
                    float pduringTime = pt.CalculateTime(pst, pet);
                    if (ddlInDirectTypes.SelectedItem.Text == "مرخصی")
                    {
                        bool checkRV = false;
                        string yName = yId.ToString();

                        int vacID = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
                        int vInfoID = (from z in db.Vac_Info
                                       where z.Year.YearName == yName && z.VacId == vacID
                                       select z.VacInfoId).Single();
                        Vac_Info vInfo = (from z in db.Vac_Info
                                          where z.Year.YearName == yName && z.VacId == vacID
                                          select z).Single();

                        RemainVacs rv = new RemainVacs(personelId);

                        SetTime calculate = new SetTime();
                        float duringTime = calculate.CalculateTime(st, et);
                        float ekhtelaf = pduringTime - duringTime;
                        //Response.Write("ekh:"+ekhtelaf);
                        //Response.Write("pd:" + pduringTime + " d: " + duringTime);
                        if (duringTime <= pduringTime)
                        {
                            p.IdcId = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
                            if (txtNote.Text != "")
                                p.Note = txtNote.Text;
                            else
                                p.Note = "-";
                            p.EndTime = et;
                            p.StartTime = st;
                            db.SaveChanges();

                            RemainVacations rvs = (from r in db.RemainVacations
                                                   where r.PerId == personelId && r.VacInfoId == vInfoID
                                                   select r).Single();
                            float remain = (float)rvs.RemainVac;
                            remain = remain + ekhtelaf;
                            rvs.RemainVac = remain;
                            //Response.Write(remain.ToString());
                            db.SaveChanges();
                        }
                        else
                        {
                            float ekhtelaf2 = duringTime - pduringTime;
                            Response.Write("e2:" + ekhtelaf2.ToString());
                            checkRV = rv.CheckVacationTime(selectDate, vInfoID, ekhtelaf2);
                            float maxPeriod = (float)vInfo.MaxInPeriod;
                            Response.Write(checkRV);
                            try
                            {
                                if (!checkRV)
                                {

                                    throw new Exception("j");

                                }
                            }
                            catch (Exception)
                            {
                                imageSuccess.Visible = true;
                                lblMessage.Visible = true;
                                MultiView1.ActiveViewIndex = -1;
                                MultiView2.ActiveViewIndex = 0;
                                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                                errorOl.InnerHtml = "<li>" +
                                    "شخصی با کد پرسنلی  " + "<b>" + personelId.ToString() + "</b>" +

                                    "بیش از حد مجاز در مدت زمانی مشخص شده" + "(" + vInfo.Period.ToString() + ")" +
                                    " " + "مرخصی اخذ کرده است و قادر به ویرایش مرخصی آن نمی باشید." +
                                    "</li>";
                            }
                            if (checkRV)
                            {
                                p.EndTime = et;
                                p.StartTime = st;
                                p.IdcId = Convert.ToInt32(ddlInDirectCodes.SelectedValue);
                                if (txtNote.Text != "")
                                    p.Note = txtNote.Text;
                                else
                                    p.Note = "-";

                                RemainVacations prvs = (from v in db.RemainVacations
                                                        where v.PerId == personelId
                                                        && v.VacInfoId == vInfoID
                                                        select v).Single();
                                float remainPrvs = (float)prvs.RemainVac;
                                if (remainPrvs >= 0)
                                {
                                    remainPrvs = remainPrvs - ekhtelaf2;
                                    prvs.RemainVac = remainPrvs;
                                    db.SaveChanges();
                                    imageSuccess.Visible = true;
                                    lblMessage.Visible = true;
                                    MultiView1.ActiveViewIndex = -1;
                                    MultiView2.ActiveViewIndex = 0;
                                    lblMessage.Text = "پیام سیستم  ";
                                    errorOl.InnerHtml = "<li>" + "<b>" + ddlInDirectCodes.SelectedItem.Text + "</b>" +
                                        " " + "برای کد پرسنلی " + "<b>" + personelId.ToString() + "</b>" + " " +
                                 "از ساعت " + "<b>" + st.ToString() + "</b>" + " " + "تا ساعت " + "<b>" + et.ToString() + "</b>" +
                                        " " + "با موفقیت در پایگاه داده ویرایش شد." +
                                        "</li>";
                                }
                                else
                                    throw new Exception("a");
                                    Response.Write("khataaaaaaaaaaaa");
                                //Response.Write(remainPrvs);
                            }
                        }
                    }
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

                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }
    
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int personelId = Convert.ToInt32(lblPersonelId.Text);
        int gvListIndex = Convert.ToInt32(ViewState["index"]);
        if (!checkValidation())
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
                List<Personals_InDirectCode> personalIndirect = (from d in db.Personals_InDirectCode
                                                                 where d.DaysOfYear.Tarikh == selectDate &&
                                                                 d.PidcId != gvListIndex &&
                                                                 d.PerId == personelId
                                                                 select d).ToList();
                if (personalIndirect.Count() == 0)
                {
                    EditToJob(sT, eT);
                }
                else
                {
                    personalIndirect = (from d in db.Personals_InDirectCode
                                        where d.DaysOfYear.Tarikh == selectDate && d.PidcId != gvListIndex &&
                                        d.PerId == personelId
                                        select d).ToList();
                    numOfRecord = personalIndirect.Count();
                    bool checks = true;

                    for (int i = 0; i < numOfRecord; i++)
                    {
                        TimeSpan pST = personalIndirect[i].StartTime;
                        TimeSpan pET = personalIndirect[i].EndTime;
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        gvList.SelectedIndex = -1;
        btnBack.Visible = true;
        btnEdit.Visible = true;
    }
    protected void ddlInDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        btnClear.Visible = true;

        string yearName=DateTime.Now.Year.ToString();

        if (ddlInDirectTypes.SelectedValue != "- انتخاب کنید -")
        {
            int index = Convert.ToInt32(ddlInDirectTypes.SelectedValue);
            if (index != 2)
            {
                IEnumerable<InDirectCodes> inDirect = from i in db.InDirectCodes
                                                      where i.IdcTypeId == index
                                                      select i;
                bindClass.bindDropDownList(ddlInDirectCodes, inDirect, "IdcName", "IdcId");
            }
            else
            {
                var vacs = from v in db.Vac_Info
                           where v.Year.YearName == yearName
                           select new
                           {
                               v.VacInfoId,
                               v.InDirectCodes.IdcName,
                               v.InDirectCodes.IdcId
                           };
                bindClass.bindDropDownList(ddlInDirectCodes, vacs, "IdcName", "IdcId");
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlTarikh_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTarikh.SelectedValue != "- انتخاب کنید -")
        {
            ViewState["tarikh"] = ddlTarikh.SelectedItem.Text;
            FillGrid();
        }
        else
            ViewState["tarikh"] = "";
    }
    protected void FillListGrid()
    {
        DateTime selectDate = DateTime.Now;
        string strTarikh = selectDate.ToShortDateString();
        selectDate = Convert.ToDateTime(strTarikh);
        if (txtShowTarikh.Text != "")
        {
            selectDate = Convert.ToDateTime(txtShowTarikh.Text);
            //string date = selectDate.ToShortDateString();
            //selectDate = Convert.ToDateTime(date);
         
        }
        else
        {
            string date = selectDate.ToShortDateString();
            selectDate = Convert.ToDateTime(date);
            
        }

        var query1 = from d in db.Personals_InDirectCode
                    where d.Personals.DepId == depId && d.DaysOfYear.Tarikh == selectDate
                    select new
                    {
                        d.PerId,
                        d.DaysOfYear.Tarikh,
                        d.Personals.FirstName,
                        d.Personals.LastName,
                        d.StartTime,
                        d.EndTime,
                        d.CodeState.DscName,
                        d.InDirectCodes.IdcName,
                        d.PidcId,
                        
                    };
        var query = query1.OrderBy(a=> a.PerId);

        bindClass.bindGrid(gvList, query);
        int number = query.Count();
        lblfooter2.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        if (selectDate.ToShortDateString() == DateTime.Now.ToShortDateString())
            listGrid2.InnerHtml = "- لیست حضور و غیاب" + " " + "<b style='color:red;'>" + "روز جاری" + "</b>" + " -";
        else
            listGrid2.InnerHtml = "- لیست حضور و غیاب در تاریخ" + " " + "<b style='color:red;' class='itemStyleNumber'>" + selectDate.ToShortDateString() + "</b>" + " -";
    }
    protected void lnkViewVacationList_Click(object sender, EventArgs e)
    {
        gvList.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 3;
        FillListGrid();
    }
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvList.SelectedValue);
        ViewState["index"] = index;
        ////try
        ////{
        DateTime selectDate = DateTime.Now;
        if (txtShowTarikh.Text != "")
            selectDate = Convert.ToDateTime(txtShowTarikh.Text);
        else
        {
            string date = selectDate.ToShortDateString();
            selectDate = Convert.ToDateTime(date);
        }
        Personals_InDirectCode pindirect = (from p in db.Personals_InDirectCode
                                            where p.PidcId == index
                                            select p).Single();
        int idcTypeId=pindirect.InDirectCodes.InDirectCodesType.IdcTypeId;
            if (pindirect != null)
            {
                string startTime = pindirect.StartTime.ToString();
                string endTime = pindirect.EndTime.ToString();
                lblTarikh.Text = selectDate.ToShortDateString();
                lblPersonelId.Text = pindirect.PerId.ToString();
                lblFullName.Text = pindirect.Personals.FirstName + " " + pindirect.Personals.LastName;
                ddlInDirectTypes.SelectedValue = idcTypeId.ToString();
                ddlInDirectCodes.Enabled = true;
                if (ddlInDirectTypes.SelectedItem.Text == "مرخصی")
                {
                    string yearName = DateTime.Now.Year.ToString();
                    Personals_Vacations pv = (from p in db.Personals_Vacations
                                              where p.PidcId == index
                                              select p).Single();
                    var vacs = from v in db.Vac_Info
                               where v.Year.YearName == yearName
                               select new
                               {
                                   v.VacInfoId,
                                   v.InDirectCodes.IdcName,
                                   v.InDirectCodes.IdcId
                               };
                    bindClass.bindDropDownList(ddlInDirectCodes, vacs, "IdcName", "IdcId");
                    ddlInDirectCodes.SelectedValue = pv.Vac_Info.VacId.ToString();
                }
                else
                {
                    IEnumerable<InDirectCodes> inDirect = from i in db.InDirectCodes
                                                          where i.IdcTypeId ==idcTypeId
                                                          select i;
                    bindClass.bindDropDownList(ddlInDirectCodes, inDirect, "IdcName", "IdcId");
                    ddlInDirectCodes.SelectedValue = pindirect.IdcId.ToString();
                }
                txtStartHour.Text = startTime.Substring(0, 2);
                txtStartMinute.Text = startTime.Substring(2, 3);
                txtEndHour.Text = endTime.Substring(0, 2);
                txtEndMinute.Text = endTime.Substring(2, 3);
                txtNote.Text = pindirect.Note;
                MultiView1.ActiveViewIndex = 0;
                btnAdd.Visible = false;
                btnEdit.Visible = true;
                btnClear.Visible = false;
                btnBack.Visible = true;
            }
            //else
            //{
            //    throw new Exception("khata");
            //}
            lblLegenName.Text = "ویرایش حضور و غیاب پرسنل";
        //}
//        catch (Exception)
//        {
//            MultiView2.ActiveViewIndex = 0;
//            MultiView1.ActiveViewIndex = -1;
//            imageError.Visible = true;
//            lblMessage.Visible = true;
//            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
//            errorOl.InnerHtml =
//"<li>رکوردی با این اطلاعات از پایگاه داده حذف شده است.</li>" +

//                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
//        }

    }
    protected void lnkViewListHozor_Click(object sender, EventArgs e)
    {
        if (txtShowTarikh.Text == "")
        {
            divError2.Visible = true;
            divError2.InnerHtml = "* برای مشاهده ی لیست حضور و غیاب یک تاریخ معتبر را انتخاب کنید.";
            imgConfirm.Visible = false;
        }
        else
        {
            divError2.Visible = false;
            gvList.SelectedIndex = -1;
            FillListGrid();

            DateTime dt = Convert.ToDateTime(txtShowTarikh.Text);
            IEnumerable<Personals_InDirectCode> InddirectFalse = from d in db.Personals_InDirectCode
                                                                 where d.DaysOfYear.Tarikh == dt && d.Personals.DepId == depId
                                                                 && d.DcsId == false 
                                                                 select d;
            imgConfirm.Visible = true;
            if (InddirectFalse.Count() == 0)
                imgConfirm.Visible = false;
        }

    }
    protected void lnkAdvancedSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("inDirect-datails.aspx");
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        ViewState["delete"] = index;
        int Id = Convert.ToInt32(gvList.DataKeys[index].Value);

        //Personals_Vacations pv = (from o in db.Personals_Vacations
        //                          where o.PidcId == Id
        //                          select o).Single();

        Personals_InDirectCode pi = (from a in db.Personals_InDirectCode
                                     where a.PidcId == Id
                                     select a).Single();
        try
        {
            //if (pi != null)
            //{
            lblpindid.Text = pi.PidcId.ToString();
            if (pi.InDirectCodes.InDirectCodesType.IdcTypeId == 2)
                lblAmaliat.Text = "( " + pi.InDirectCodes.InDirectCodesType.IdcTypeName + " )" + "" + pi.InDirectCodes.IdcName;
            else
                lblAmaliat.Text = "( " + pi.InDirectCodes.InDirectCodesType.IdcTypeName + " )" + "" + pi.InDirectCodes.IdcName;
            lblTarikhDetails.Text = pi.DaysOfYear.Tarikh.ToShortDateString();
            lblFullNameDetails.Text = pi.Personals.FirstName + " " + pi.Personals.LastName;
            lblpId.Text = pi.PerId.ToString();
            lblDep.Text = pi.Personals.Departmans.DepName;
            lblJob.Text = pi.Personals.Jobs.JobName;
            lblStartTime.Text = pi.StartTime.ToString();
            lblEndTime.Text = pi.EndTime.ToString();
            txtNoteDetails.Text = pi.Note;
            //}
            //else
            //    throw new Exception("dade vojod nadarad");
            MultiView1.ActiveViewIndex = 2;
            lblLegenName.Text = "جزئیات بیشتر";

        }
        catch (Exception)
        {
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            imageError.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml =
                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }

    }

    protected int GetPersonelId(object pcode)
    {
        int pid = Convert.ToInt32(pcode);
        ViewState["personelId"] = pid;
        return pid;
    }
    protected string GetColor()
    {
        string color = "";
        int pid = Convert.ToInt32(ViewState["personelId"]);
        if (ViewState["tarikh"] != null)
        {
            DateTime dt = Convert.ToDateTime(ViewState["tarikh"]);
            int numberFalse = (from v in db.Personals_InDirectCode
                               where v.DaysOfYear.Tarikh == dt && v.DcsId == false && v.PerId == pid
                               select v).Count();
            if (numberFalse == 0)
            {
                color = "lblPerId";
            }
        }
        else
            color = "";
        return color;
    }

    protected void lnkTaeed_Click(object sender, EventArgs e)
    {

    }
    protected void ddlInDirectCodes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgConfirm_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtShowTarikh.Text != "")
            {
                DateTime dt = Convert.ToDateTime(txtShowTarikh.Text);
                List<Personals_InDirectCode> pc = (from d in db.Personals_InDirectCode
                                                   where d.DaysOfYear.Tarikh == dt && d.Personals.DepId == depId
                                                   && d.DcsId == false 
                                                   select d).ToList();
                int number = pc.Count();
                if (number > 0)
                {
                    for (int i = 0; i < number; i++)
                    {
                        pc[i].DcsId = true;
                    }
                    db.SaveChanges();
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    imageError.Visible = false;
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "پیام سیستم  ";
                    errorOl.InnerHtml =
        "<li>لیست حضور و غیاب تاریخ "+"<b>"+txtShowTarikh.Text+"</b>"+" "+"تایید نهایی شد، و شما دیگر قادر به تغییر آن نیستید.</li>";
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
    "<li>ممکن است برای یکی از پرسنل هیچ عملیاتی ثبت نکرده باشید.</li>" +
    "<li>ممکن است تاریخی را برای تایید نهایی لیست حضور و غیاب انتخاب نکرده باشید.</li>" +
                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }
                                               
    }
}
   