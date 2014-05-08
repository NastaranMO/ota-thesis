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

public partial class Admin_onvan_kar : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        //visible false
        lblGuide.Visible = false;
        imgCustomError.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;
        MultiView2.ActiveViewIndex = -1;
        txtJobId.Enabled = true;
        btnBack.Visible = false;
        if (!IsPostBack)
        {
            btnAdd.Visible = true;
            lblGuideClass c = new lblGuideClass();
            lblGuide.Text = c.fillLblGuide("add");
            MultiView1.ActiveViewIndex = 1;
            MultiView2.ActiveViewIndex = -1;
            IEnumerable<Jobs> job = from j in db.Jobs
                                    select j;
            int number = job.Count();
            lblLegenName.Text = "لیست عناوین کاری";
            lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
            bindClass.bindGrid(gvJob, job);
        }



    }
    protected void addCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن عنوان کاری جدید";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnSearch.Visible = false;
        btnEdit.Visible = false;
        clearTextBoxes();
        btnClear.Visible = true;

    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        txtJobId.Text = "";
        txtJobName.Text = "";
        txtNote.Text = "";
    }
    //Add To Db 
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    //Search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvJob.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        int jobId = 0;
        string jobName = "";
        ObjectResult<Jobs> jobs;

        if (txtJobId.Text != "" && txtJobName.Text == "")
        {
            jobId = Convert.ToInt32(txtJobId.Text);
            jobs = db.ExecuteStoreQuery<Jobs>("Select * From Jobs Where JobId = " + jobId);
            gvJob.DataSource = jobs;
            gvJob.DataBind();
            MultiView1.ActiveViewIndex = 1;
            lblFooter.Text = "تعداد رکوردها: " + "<b style='font-family:B nazanin;font-size:12px;'>" + gvJob.Rows.Count + "</b>";
        }

        else if (txtJobName.Text != "" && txtJobId.Text == "")
        {
            jobName = txtJobName.Text;
            jobs = db.ExecuteStoreQuery<Jobs>("Select * From Jobs Where JobName Like '%" + jobName + "%'");
            gvJob.DataSource = jobs;
            gvJob.DataBind();
            MultiView1.ActiveViewIndex = 1;
            lblFooter.Text = "تعداد رکوردها: " + "<b style='font-family:B nazanin;font-size:12px;'>" + gvJob.Rows.Count + "</b>";
        }

        else if (txtJobId.Text != "" && txtJobName.Text != "")
        {
            jobId = Convert.ToInt32(txtJobId.Text);
            jobName = txtJobName.Text;
            jobs = db.ExecuteStoreQuery<Jobs>("Select * From Jobs Where JobId = " + jobId + " And JobName Like '%" + jobName + "%'");
            gvJob.DataSource = jobs;
            gvJob.DataBind();
            MultiView1.ActiveViewIndex = 1;
            lblFooter.Text = "تعداد رکوردها: " + "<b style='font-family:B nazanin;font-size:12px;'>" + gvJob.Rows.Count + "</b>";
        }

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        IEnumerable<Jobs> job = from j in db.Jobs
                                select j;
        int number = job.Count();
        lblLegenName.Text = "لیست عناوین کاری";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvJob, job);
    }
    //Definition Row For GridView
    int i = 0;
    public string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void gvJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = gvJob.SelectedIndex;
        int JobId = (int)gvJob.DataKeys[index].Value;
        ViewState["editJobId"] = JobId;

        Jobs job = (db.Jobs.Where(a => a.JobId == JobId)).Single();
        txtJobId.Text = job.JobId.ToString();
        txtJobName.Text = job.JobName;
        txtNote.Text = job.JobNote;
        MultiView1.ActiveViewIndex = 0;
        btnAdd.Visible = false;
        btnEdit.Visible = true;
        txtJobId.Enabled = false;
        btnSearch.Visible = false;
        btnClear.Visible = false;
        btnBack.Visible = true;

    }

    protected void gvJob_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = e.RowIndex;
            int jobId = (int)gvJob.DataKeys[index].Value;

            Jobs job = (db.Jobs.Where(a => a.JobId == jobId).Single());

            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>عنوان کاری " +
                "<b>" + job.JobName + "</b> " +
                "با موفقیت از سیستم حذف شد." +
                "</li>";
            imageSuccess.Visible = true;
            db.DeleteObject(job);
            db.SaveChanges();

        }
        catch
        {
            MultiView2.ActiveViewIndex = 0;
            imageSuccess.Visible = false;
            imgCustomError.Visible = false;
            imageError.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>این عنوان کاری قبلا از سیستم حذف شده باشد.</li>" +
            "<li>خطا در برقراری ارتباط با پایگاه داده رخ داده باشد.</li>";
        }
        IEnumerable<Jobs> jobs = from c in db.Jobs
                                 select c;
        gvJob.DataSource = jobs;
        gvJob.DataBind();

        lblFooter.Text = "تعداد رکوردها: " + gvJob.Rows.Count;
    }
    protected void cvAddJobs_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (txtJobId.Text == "" || txtJobName.Text == "")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            args.IsValid = true;
            Checking chk = new Checking("Jobs", txtJobName.Text, "JobName");
            bool Duplicate = chk.CheckDuplicateData();
            try
            {
                if (Duplicate)
                {
                    throw new Exception("Duplicate Departman Name");
                }
                else
                {
                    if (txtNote.Text == "")
                    {
                        db.sp_jobInsert(Convert.ToInt32(txtJobId.Text), txtJobName.Text.Replace("ی", "ي"), "-");
                        MultiView1.ActiveViewIndex = -1;
                    }
                    else
                    {
                        db.sp_jobInsert(Convert.ToInt32(txtJobId.Text), txtJobName.Text.Replace("ی", "ي"), txtNote.Text);
                        MultiView1.ActiveViewIndex = -1;
                    }

                    db.SaveChanges();
                    imgCustomError.Visible = false;
                    imageError.Visible = false;
                    imageSuccess.Visible = true;

                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    lblMessage.Text = "پیام سیستم  ";
                    errorOl.InnerHtml = "<li>عنوان کاری " +
                        "<b>" + txtJobName.Text + "</b> " +
                        "با کد " +
                        "<b>" + txtJobId.Text + "</b> " +
                        "با موفقیت در سیستم ثبت شد." +
                        "</li>";
                }
            }
            catch (Exception)
            {
                imageError.Visible = true;
                imageSuccess.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml = "<li>شرح عنوان کاری یا کد عنوان کاری تکراری است.</li>" +

                                    "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
            }
        }
    }
    protected void clearTextBoxes()
    {
        txtJobId.Text = "";
        txtJobName.Text = "";
        txtNote.Text = "";
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        btnBack.Visible = true;
        txtJobId.Enabled = false;
        if (txtJobName.Text == "")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            args.IsValid = true;
            Checking chk = new Checking("Jobs", txtJobName.Text.Replace("ی", "ي"), "JobName", Convert.ToInt32(txtJobId.Text), "JobId");
            bool Duplicate = chk.EditCheckDuplicateData();
            try
            {
                if (Duplicate)
                {
                    throw new Exception("Duplicate Departman Name");
                }
                else
                {

                    if (txtNote.Text == "")
                    {
                        db.sp_jobUpdate(Convert.ToInt32(txtJobId.Text), txtJobName.Text.Replace("ی", "ي"), "-");
                        MultiView1.ActiveViewIndex = -1;
                    }
                    else
                    {
                        db.sp_jobUpdate(Convert.ToInt32(txtJobId.Text), txtJobName.Text.Replace("ی", "ي"), txtNote.Text);
                        MultiView1.ActiveViewIndex = -1;
                    }

                    db.SaveChanges();
                    imgCustomError.Visible = false;
                    imageError.Visible = false;
                    imageSuccess.Visible = true;

                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>عنوان کاری " +
                        "<b>" + txtJobName.Text + "</b> " +
                        "با کد " +
                        "<b>" + txtJobId.Text + "</b> " +
                        "با موفقیت ویرایش شد." +
                        "</li>";
                }
            }

            catch (Exception)
            {
                imageSuccess.Visible = false;
                imageError.Visible = true;
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;

                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml = "<li>شرح عنوان کاری تکراری است.</li>" +

                                    "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";

            }



        }
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        MultiView2.ActiveViewIndex = -1;
        IEnumerable<Jobs> job = from j in db.Jobs
                                select j;
        int number = job.Count();
        lblLegenName.Text = "لیست عناوین کاری";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvJob, job);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        MultiView2.ActiveViewIndex = -1;
        IEnumerable<Jobs> job = from j in db.Jobs
                                select j;
        int number = job.Count();
        lblLegenName.Text = "لیست عناوین کاری";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvJob, job);
    }
}

