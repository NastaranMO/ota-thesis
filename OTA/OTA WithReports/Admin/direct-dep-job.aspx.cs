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

public partial class Admin_direct_dep_job : System.Web.UI.Page
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
        btnClear.Visible = true;
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
            //fill ddlDirectCodes
            IEnumerable<DirectCodes> directCodes = from d in db.DirectCodes
                                                   select d;
            bindClass.bindDropDownList(ddlCode, directCodes, "DcName", "DcId");
            //fill ddldeps
            IEnumerable<Departmans> deps = from d in db.Departmans
                                           select d;
            bindClass.bindDropDownList(ddldeps, deps, "DepName", "DepId");
            //fill chkJobs
            FillChkJobs();
            //fill ddlDepView
            bindClass.bindDropDownList(ddlDepView, deps, "DepName", "DepId");
            //fill ddlJobView
            FillddlJobView();
        }

    }
    //fill chkJobs
    protected void FillChkJobs()
    {
        int depId = Convert.ToInt32(ddldeps.Items[0].Value);
        var jobs = from dj in db.Departman_Job
                   where dj.DepId == depId
                   select new { dj.JobId, dj.Jobs.JobName };
        bindClass.bindCheckBoxList(chkJobs, jobs, "JobName", "JobId");
    }
    //fill ddlJobView
    protected void FillddlJobView()
    {
        int depId = Convert.ToInt32(ddlDepView.Items[0].Value);
        var jobs = from dj in db.Departman_Job
                   where dj.DepId == depId
                   select new { dj.Jobs.JobName, dj.JobId };
        bindClass.bindDropDownList(ddlJobView, jobs, "JobName", "JobId");
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن تنظیمات جدید";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnClear.Visible = true;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnClear.Visible = true;

    }
    //menu delete
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 1;
        lblLegenName.Text = "حذف تنظیمات ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("delete");
        btnAdd.Visible = false;
        btnClear.Visible = true;

    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        lblLegenName.Text = "لیست کدهای عملیاتی عناوین کاری";
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
    }
    //Clear TextBoxes
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        btnClear.Visible = true;
        foreach (ListItem item in chkJobs.Items)
        {
            if (item.Selected)
                item.Selected = false;
        }
    }
    //method For Clear TextBoxes
    protected void Clear()
    {
    }
    //btn Back in Search Page
    protected void btnBack_Click(object sender, EventArgs e)
    {
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
        int checkId = 0;
        foreach (ListItem item in chkJobs.Items)
        {
            if (item.Selected)
            {
                checkId++;
            }
        }
        if (checkId == 0)
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            args.IsValid = true;
            try
            {

                int dcId = Convert.ToInt32(ddlCode.SelectedItem.Value);
                int depId = Convert.ToInt32(ddldeps.SelectedItem.Value);
                int number = (from dj in db.Departman_Job
                              where dj.DepId == depId
                              select dj).Count();
                int[] depjobId = new int[number];
                int indexJobId = 0;
                for (int i = 0; i < number; i++)
                {

                    if (chkJobs.Items[i].Selected)
                    {
                        depjobId[indexJobId] = Convert.ToInt32(chkJobs.Items[i].Value);
                        indexJobId++;
                    }
                }
                //Response.Write(depjobId[0].ToString());
                int[] te = new int[indexJobId];
                bool checkDepJobId = false;
                for (int i = 0; i < indexJobId; i++)
                {
                    int jId = depjobId[i];
                    int tekrari = (from t in db.Departman_Job
                                   where t.DepId == depId && t.JobId == jId
                                   select t.DepJobId).Single();
                    int numtekrari = (from t in db.Departman_Job
                                      where t.DepId == depId && t.JobId == jId
                                      select t.DepJobId).Count();

                    if (numtekrari > 0)
                    {
                        int check = (from d in db.DcDepJob
                                     where d.DcId == dcId && d.DepJobId == tekrari
                                     select d).Count();
                        if (check > 0)
                        {
                            checkDepJobId = true;
                            break;
                        }
                        //Response.Write(checkDepJobId.ToString());
                        te[i] = tekrari;
                        //Response.Write(te[i].ToString());
                    }
                }
                if (!checkDepJobId && te.Length == indexJobId)
                {
                    for (int i = 0; i < indexJobId; i++)
                    {
                        DcDepJob dcDepJob = new DcDepJob();
                        dcDepJob.DepJobId = te[i];
                        dcDepJob.DcId = dcId;
                        db.AddToDcDepJob(dcDepJob);
                        db.SaveChanges();
                    }
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" +
                        "عملیات " + "<b>" + ddlCode.SelectedItem.Text + "</b>" +
                        " " + "در دپارتمان " + "<b>" + ddldeps.SelectedItem.Text + "</b>" +
                        " " + "برای عناوین کاری انتخاب شده با موفقیت ثبت شد.";
                }
                else
                    throw new Exception("dade tekrari");
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
        listGrid.InnerHtml = "- لیست کدهای عملیاتی عناوین کاری -";
    }
    //fill Grid
    protected void FillGrid()
    {
        int jobId = Convert.ToInt32(ddlJobView.SelectedItem.Value);
        int depId = Convert.ToInt32(ddlDepView.SelectedItem.Value);
        int depJobId = (from d in db.Departman_Job
                        where d.DepId == depId && d.JobId == jobId
                        select d.DepJobId).Single();
        var query = from d in db.DcDepJob
                    where d.DepJobId == depJobId
                    select new { d.DcId, d.DirectCodes.DcName, d.DirectCodes.DcNote, d.DcDepJobId };
        int number = query.Count();
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        listGrid.InnerHtml = "- لیست کدهای عملیاتی عنوان کاری" + " "
           + "<b style='color:red;'>" + ddlJobView.SelectedItem.Text + "</b>" + " " +
               "از دپارتمان " + "<b style='color:red;'>" + ddlDepView.SelectedItem.Text + "</b>";
        bindClass.bindGrid(gvDirectCode, query);
    }
    protected void gvDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvDirectCode.SelectedValue);

        DcDepJob dcDepJobs = (from d in db.DcDepJob
                              where d.DcDepJobId == index
                              select d).Single();
        try
        {
            db.DeleteObject(dcDepJobs);
            db.SaveChanges();
            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>عملیات " +
    "با موفقیت انجام شد." +
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
            errorOl.InnerHtml = "<li>این عملیات قبلا از سیستم حذف شده است.</li>" +
           "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>";

        }
    }
    protected void ddldeps_SelectedIndexChanged(object sender, EventArgs e)
    {
        int depId = Convert.ToInt32(ddldeps.SelectedItem.Value);
        var jobs = from dj in db.Departman_Job
                   where dj.DepId == depId
                   select new { dj.JobId, dj.Jobs.JobName };
        bindClass.bindCheckBoxList(chkJobs, jobs, "JobName", "JobId");
    }
    protected void lnkViewDep_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlDepView_SelectedIndexChanged(object sender, EventArgs e)
    {
        int depId = Convert.ToInt32(ddlDepView.SelectedItem.Value);
        var jobs = from dj in db.Departman_Job
                   where dj.DepId == depId
                   select new { dj.JobId, dj.Jobs.JobName };
        bindClass.bindDropDownList(ddlJobView, jobs, "JobName", "JobId");
    }
    protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
