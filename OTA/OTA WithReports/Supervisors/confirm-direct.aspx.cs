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

public partial class Supervisors_confirm_direct : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    int pId;
    int depId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //visible
        divError.Visible = false;
        lblGridError.Visible = false;
        lblGuide.Visible = false;
        btnBack.Visible = false;
        lblMessage.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;
        MultiView2.ActiveViewIndex = -1;
        if (!IsPostBack)
        {
            pId = Convert.ToInt32(Profile.personelId);
            ViewState["pidd"] = pId;

            depId = (from d in db.Personals
                     where d.PersonalID == pId
                     select d.DepId).Single();
            ViewState["d"] = depId;
            //fill lblGuide
            lblGuide.Text = lblGuide.Text = "برای مشاهده ی لیست عملیات انجام شده هر یک از کارمندان " +
            "بر روی واژه ی " + "<b>" + "انتخاب" + "</b>" + "کلیک نمایید.";
            //multiView
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = -1;
            //fill ddlTarikh
            FillddlTarikh();
            ddlTarikh.Items.Add("- انتخاب کنید -");
            ddlTarikh.SelectedValue = "- انتخاب کنید -";
            //fillPersonelGrid
            FillPersonelGrid();
        }
        depId = Convert.ToInt32(ViewState["d"]);
        pId = Convert.ToInt32(ViewState["pidd"]);
    }
    //fill ddlTarikh
    protected void FillddlTarikh()
    {
        DateTime dtNow = DateTime.Now;
        string yearID = DateTime.Now.Year.ToString();
        InDirect inDirect = new InDirect(depId);
        int[] dayId = inDirect.DirectReturnDayId();
        if (dayId[0] == -1)
        {
            List<DaysOfYear> d = (from p in db.DaysOfYear
                                  where p.Year.YearName == yearID && p.Tarikh <= dtNow && p.DsId!=2
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
    //Fill ddlDirectCode
    protected void FillDdlDirectCodes(int perId)
    {
        int departmentID;
        int JobId;
        Personals p = (from i in db.Personals
                       where i.PersonalID == perId
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

        bindClass.bindDropDownList(ddlDirectCodes, dcDepJob, "DcName", "DcId");
        ddlDirectCodes.Items.Add("- انتخاب کنید -");
        ddlDirectCodes.SelectedValue = "- انتخاب کنید -";

    }
    //menu Edit
    protected void editeCode_Click(object sender, EventArgs e)
    {
        gvDirect.SelectedIndex = -1;
        gvPersonels.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "ویرایش عملیات پرسنل ";
        lblGuide.Text = "برای ویرایش هر یک از عملیات انجام شده توسط کارمند مورد نظر " +
            "ابتدا آن را جستجو کنید. ";
        btnEdit.Visible = false;
        confirmVisible();
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        gvDirect.SelectedIndex = -1;
        gvPersonels.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "لیست پرسنل";
        lblGuide.Text = "برای مشاهده ی لیست عملیات انجام شده هر یک از کارمندان " +
            "بر روی واژه ی " + "<b>" + "انتخاب" + "</b>" + "کلیک نمایید.";
        btnEdit.Visible = false;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvDirect.SelectedIndex = -1;
        gvPersonels.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "جستجو ساده";
        lblGuide.Text = "برای انجام عمل جستجو ابتدا یک تاریخ را انتخاب کنید، سپس بر روی واژه ی " +
            "<b>" + "انتخاب" + "</b>" + " کلیک نمایید.";
        btnEdit.Visible = false;
        confirmVisible();
    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
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
        int id = Convert.ToInt32(lblPersonelId.Text);
        gvDirect.SelectedIndex = -1;
        gvPersonels.SelectedIndex = -1;
        FillGrid(id);
    }
    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    //fillPersonelList
    protected void FillPersonelGrid()
    {
        var emp = from d in db.Personals
                  where d.DepId == depId && d.AId!=1
                  select new
                      {
                          d.LastName,
                          d.FirstName,
                          d.Jobs.JobName,
                          d.PersonalID
                      };
        bindClass.bindGrid(gvPersonels, emp);
        int number = emp.Count();
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        listGrid.InnerHtml = "- لیست کارمندان -";

    }
    //fillGrid
    protected void FillGrid(int id)
    {
        DateTime dt = Convert.ToDateTime(ddlTarikh.SelectedItem.Text);
        var query = from d in db.Personals_DirectCode
                    where d.PerId == id && d.DaysOfYear.Tarikh == dt
                    select new
                    {
                        d.StartTime,
                        d.EndTime,
                        d.DirectCodes.DcName,
                        d.Note,
                        d.Personals.Jobs.JobName,
                        d.CodeState.DscName,
                        d.PdcId,
                        d.Personals.FirstName,
                        d.Personals.LastName
                    };
        Personals p = (from i in db.Personals
                       where i.PersonalID == id
                       select i).Single();
        string firstName = p.FirstName;
        string lastName = p.LastName;
        bindClass.bindGrid(gvDirect, query);
        int number = query.Count();
        lblFooter2.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        listGrid2.InnerHtml = "- لیست عملیات کارمند : " + "<b>" + firstName + " " + lastName + " " + "</b>" + "-" +
            "<br/>" + "در تاریخ" + "<b class='itemStyleNumber'>" + dt.ToShortDateString() + "</b>";


    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        gvDirect.SelectedIndex = -1;
        gvPersonels.SelectedIndex = -1;
        FillPersonelGrid();
        confirmVisible();
    }
    protected void gvPersonels_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDirect.SelectedIndex = -1;
        int id = Convert.ToInt32(gvPersonels.SelectedValue);
        if (ddlTarikh.SelectedValue != "- انتخاب کنید -")
        {
            FillGrid(id);
            MultiView1.ActiveViewIndex = 2;
        }
        else
        {
            divError.Visible = true;
            lblGridError.Visible = true;
            lblGridError.Text = "* برای انتخاب یک کارمند یک تاریخ معتبر را انتخاب کنید.";
            gvDirect.SelectedIndex = -1;
            gvPersonels.SelectedIndex = -1;
        }
    }
    protected void gvDirect_SelectedIndexChanged(object sender, EventArgs e)
    {
        int pdId = Convert.ToInt32(gvDirect.SelectedValue);
        Personals_DirectCode pc = (from d in db.Personals_DirectCode
                                   where d.PdcId == pdId
                                   select d).Single();
        int persID = pc.PerId;

        FillDdlDirectCodes(persID);

        string startTime = pc.StartTime.ToString();
        string endTime = pc.EndTime.ToString();

        Label1.Text = pdId.ToString();
        txtStartHour.Text = startTime.Substring(0, 2);
        txtStartMinute.Text = startTime.Substring(2, 3);
        txtEndHour.Text = endTime.Substring(0, 2);
        txtEndMinute.Text = endTime.Substring(2, 3);
        txtNote.Text = pc.Note;
        ddlDirectCodes.SelectedValue = pc.DcId.ToString();
        MultiView1.ActiveViewIndex = 0;
        MultiView2.ActiveViewIndex = -1;
        lblLegenName.Text = "ویرایش عملیات پرسنل";
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int persId = Convert.ToInt32(lblPersonelId.Text);
        int pdId = Convert.ToInt32(Label1.Text);

        if (ddlDirectCodes.SelectedValue == "- انتخاب کنید -")
        {
            args.IsValid = false;
            imgCustomEdit.Visible = true;
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
                                                             d.PdcId != pdId &&
                                                             d.PerId == persId
                                                             select d).ToList();
                if (personaldirect.Count() == 0)
                {
                    EditToJob(sT, eT);
                }
                else
                {
                    personaldirect = (from d in db.Personals_DirectCode
                                      where d.DaysOfYear.Tarikh == selectDate && d.PdcId != pdId &&
                                      d.PerId == persId
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
        int personelId = Convert.ToInt32(lblPersonelId.Text);
        int dcId = Convert.ToInt32(ddlDirectCodes.SelectedValue);
        int pdId = Convert.ToInt32(Label1.Text);
        string year = DateTime.Now.Year.ToString();
        DateTime selectDate = Convert.ToDateTime(lblTarikh.Text);
        int yId = selectDate.Year;
        int dayId = (from d in db.DaysOfYear
                     where d.Tarikh == selectDate
                     select d.dayId).Single();
        try
        {
        int numberp = (from d in db.Personals_DirectCode
                       where d.PdcId == pdId
                       select d).Count();
        if (numberp > 0)
        {
            Personals_DirectCode p = (from d in db.Personals_DirectCode
                                      where d.PdcId == pdId
                                      select d).Single();
            int pidid = p.PdcId;
            if (ddlDirectCodes.SelectedItem.Text != "- انتخاب کنید -")
            {
                p.StartTime = st;
                p.EndTime = et;
                p.DcId = dcId;
                db.SaveChanges();
                imageSuccess.Visible = true;
                lblMessage.Visible = true;
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم";
                errorOl.InnerHtml = "<li>" + "<b>" + ddlDirectCodes.SelectedItem.Text + "</b>" +
                    " " + "برای کد پرسنلی " + "<b>" + personelId.ToString() + "</b>" + " " +
             "از ساعت " + "<b>" + st.ToString() + "</b>" + " " + "تا ساعت " + "<b>" + et.ToString() + "</b>" +
                    " " + "با موفقیت در پایگاه داده ویرایش شد." +
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

                        "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
                }

    }
    protected void gvDirect_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string name;
        string dcName;
        string st;
        string et;
        try
        {
            int index = e.RowIndex;
            int pdId = Convert.ToInt32(gvDirect.SelectedDataKey[index]);
            Personals_DirectCode pc = (from d in db.Personals_DirectCode
                                       where d.PdcId == pdId
                                       select d).Single();
            dcName = pc.DirectCodes.DcName;
            st = pc.StartTime.ToString();
            et = pc.EndTime.ToString();
            name = pc.Personals.FirstName + " " + pc.Personals.LastName;
            db.DeleteObject(pc);
            db.SaveChanges();
            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>" + "عملیات " + "<b>" + dcName + "</b>" + " با موفقیت از لیست عملیات " +
                "انجام شده توسط " + "<b>" + name + "</b>" + " از ساعت " + st + " تا ساعت " + et + " حذف شد." +
                "</li>";
        }
        catch (Exception)
        {
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            imageError.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml =
                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }
    }
    protected void ddlTarikh_SelectedIndexChanged(object sender, EventArgs e)
    {
        confirmVisible();
    }
    protected void confirmVisible()
    {
        DateTime dt;
        if (ddlTarikh.SelectedValue != "- انتخاب کنید -")
        {
            dt = Convert.ToDateTime(ddlTarikh.SelectedItem.Text);
            IEnumerable<Personals_DirectCode> pc = from d in db.Personals_DirectCode
                                                   where d.Personals.DepId == depId
                                                   && d.DcsId == false
                                                   && d.DaysOfYear.Tarikh == dt
                                                   select d;
            int number = pc.Count();
            imgConfirm.Visible = false;
            if (number > 0)
                imgConfirm.Visible = true;
        }

    }
    protected void imgConfirm_Click(object sender, ImageClickEventArgs e)
    {
        DateTime dt;
        try
        {
            if (ddlTarikh.SelectedValue != "- انتخاب کنید -")
            {
                dt = Convert.ToDateTime(ddlTarikh.SelectedItem.Text);
                List<Personals_DirectCode> pc = (from d in db.Personals_DirectCode
                                                 where d.Personals.DepId == depId
                                               && d.DcsId == false
                                                 && d.DaysOfYear.Tarikh == dt
                                                 select d).ToList();
                int number = pc.Count();
                if (number > 0)
                {

                    for (int i = 0; i < number; i++)
                    {
                        pc[i].DcsId = true;
                        db.SaveChanges();
                    }
                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    imageError.Visible = false;
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "پیام سیستم  ";
                    errorOl.InnerHtml =
        "<li>لیست حضور و غیاب تاریخ " + "<b>" + dt.ToShortDateString() + "</b>" + " " + "تایید نهایی شد، و شما دیگر قادر به تغییر آن نیستید.</li>";
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

    "<li>ممکن است تاریخی را برای تایید نهایی لیست حضور و غیاب انتخاب نکرده باشید.</li>" +
                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }
    }
}
