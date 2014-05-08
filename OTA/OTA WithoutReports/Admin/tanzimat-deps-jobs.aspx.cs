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

public partial class Admin_tanzimat_deps_jobs : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        //visible
        lblGuide.Visible = false;
        btnBack.Visible = false;
        btnClear.Visible = false;
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
            //fill ChkDep
            IEnumerable<Departmans> deps = from d in db.Departmans
                                           select d;
            bindClass.bindCheckBoxList(chkDep, deps, "DepName", "DepId");

            //fill ddlJob
            IEnumerable<Jobs> jobs = from j in db.Jobs
                                     select j;
            bindClass.bindDropDownList(ddlJob, jobs, "JobName", "JobId");
            //fill ddlDep
            bindClass.bindDropDownList(ddlDepView, deps, "DepName", "DepId");
        }

    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن تنظیمات جدید";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnSearch.Visible = false;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        lblLegenName.Text = "جستجو";

        lblGuide.Text = "برای انجام عمل جستجو یک مورد را از داخل لیست انتخاب کرده و بر روی نمایش کلیک کنید..";
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;

    }
    //menu delete
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        lblLegenName.Text = "حذف تنظیمات ";

        lblGuide.Text = "برای حذف یک رکورد،بعد از انجام عمل جستجو بر روی آیکون حذف کلیک کنید.";
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;

    }
    //menu ViewAll
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        lblLegenName.Text = "لیست دپارتمان ها و عناوین کاری ";
        FillGrid();
    }
    //lnk Guide
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        btnAdd.Visible = true;
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
    }

    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            btnClear.Visible = true;
            bool check = false;
            foreach (ListItem item in chkDep.Items)
            {
                if (item.Selected == true)
                    check = true;
            }
            if (check)
            {
                int indexDeps = 0;
                int number = (from d in db.Departmans
                              select d).Count();
                int[] depIds = new int[number];
                for (int i = 0; i < number; i++)
                {
                    if (chkDep.Items[i].Selected)
                    {
                        depIds[indexDeps] = Convert.ToInt32(chkDep.Items[i].Value);
                        indexDeps++;
                    }
                }
                for (int i = 0; i < indexDeps; i++)
                {
                    if (Checking.Check2Column("Departman_Job", "DepId", "JobId", depIds[i], Convert.ToInt32(ddlJob.SelectedItem.Value)))
                    {
                        Departman_Job depJob = new Departman_Job();
                        depJob.JobId = Convert.ToInt32(ddlJob.SelectedItem.Value);
                        depJob.DepId = depIds[i];
                      
                        db.AddToDepartman_Job(depJob);
                        db.SaveChanges();
                        imageSuccess.Visible = true;
                        imageError.Visible = false;
                        MultiView1.ActiveViewIndex = -1;
                        MultiView2.ActiveViewIndex = 0;
                        lblMessage.Text = "پیام سیستم";
                        errorOl.InnerHtml = "<li>عنوان کاری " + "<b>" +
                            ddlJob.SelectedItem.Text + "</b> " + "با موفقیت به دپارتمان های انتخاب شده اضافه گردید."
                            + "</li>";
                    }
                    else
                    {
                        throw new Exception("kk");

                    }
                }
            }
        }
        catch (Exception)
        {
            imageSuccess.Visible = false;
            imageError.Visible = true;
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;

            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>برای ثبت عملیات بایستی یکی از دپارتمان ها را انتخاب کنید.</li>" +
                "<li>عنوان کاری انتخاب شده در دپارتمان مورد نظر شما ممکن است قبلا در پایگاه داده ثبت شده باشد.</li>" +

                                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }
    }
    protected void lnkViewDep_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    //
    protected void lnkView_Click(object sender, EventArgs e)
    {
        FillGrid();
        lblLegenName.Text = "لیست دپارتمان ها و عناوین کاری";
    }
    //fill GridView
    protected void FillGrid()
    {
        int? depId;
        depId = Convert.ToInt32(ddlDepView.SelectedValue);
        if (depId != null)
        {
            listGrid.InnerHtml = "- لیست عناوین کاری دپارتمان " + "<b style='color:red;'>" + ddlDepView.SelectedItem.Text + "</b>" + " -";
            var depJob = from d in db.Departman_Job
                         where d.DepId == depId
                         select new { d.Jobs.JobName, d.Jobs.JobId, d.DepJobId };

            int Number = depJob.Count();
            lblFooter.Text = "تعداد رکورد ها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + Number.ToString() + "</b>";
            gvDepJob.DataSource = depJob;
            gvDepJob.DataBind();
            MultiView1.ActiveViewIndex = 1;
        }
    }
    protected void gvDepJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(gvDepJob.SelectedValue);
            Departman_Job depjob = (from d in db.Departman_Job
                                    where d.DepJobId == index
                                    select d).Single();
            string depName = depjob.Departmans.DepName;
            string jobName = depjob.Jobs.JobName;
            db.DeleteObject(depjob);
            db.SaveChanges();
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            imageError.Visible = false;
            imageSuccess.Visible = true;
            lblMessage.Text = "پیام سیستم";

            errorOl.InnerHtml = "<li>عنوان کاری " +
                "<b>" + jobName + "</b> " +
                "در دپارتمان " +
                "<b>" + depName + "</b> " +
                "با موفقیت از سیستم حذف شد." +
                "</li>";
        }
        catch (Exception)
        {
            imageSuccess.Visible = false;
            imageError.Visible = true;
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;

            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = 
                "<li>برای این عنوان کاری در این دپارتمان کد عملیاتی تعریف شده است،شما قادر به حذف آن نیستید.</br>"+
                "بدین منظور ابتدا رکوردهای ثبت شده برای آنرا حذف کنید.</li>" +

                                "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
        }


    }


    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }
    protected void cvAddDepJob_ServerValidate(object source, ServerValidateEventArgs args)
    {
    }
}
