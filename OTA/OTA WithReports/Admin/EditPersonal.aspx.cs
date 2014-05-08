using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.EntityClient;
using System.Data;
using OTA_DBModel;

public partial class Admin_EditPersonal : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblGuide.Visible = false;
        lblMessage.Visible = false;
        imageError.Visible = false;
        imageError0.Visible = false;
        imageSuccess.Visible = false;

        MultiView2.ActiveViewIndex = -1;


        if (!IsPostBack)
        {
            imageError0.Visible = false;
            imageError.Visible = false;
            lblGuide.Visible = false;
            MultiView1.ActiveViewIndex = 0;

            IEnumerable<Departmans> deps = from c in db.Departmans
                                           select c;
            IEnumerable<Jobs> jobs = from c in db.Jobs
                                     select c;
            IEnumerable<AccessLevel> accessLevel = from c in db.AccessLevel
                                      select c;
            IEnumerable<Education> edus = from d in db.Education
                                          select d;

            bindClass.bindDropDownList(ddlEducation, edus, "EduName", "EduId");

            bindClass.bindDropDownList(ddlAccessLevel, accessLevel, "AName", "AId");

            bindClass.bindDropDownList(ddlDepartment, deps, "DepName", "DepId");

            bindClass.bindDropDownList(ddlJob, jobs, "JobName", "JobId");

            //fill lblGuide
            lblGuideClass g = new lblGuideClass();
            lblGuide.Text = g.fillLblGuide("edit");

            if (Request.QueryString["pid"] == null)
            {
                Response.Redirect("SearchPersonals.aspx");
            }
            else
            {
                int personalId = Convert.ToInt32(Request.QueryString["pid"]);

                Personals person = db.Personals.Where(a => a.PersonalID == personalId).Single();

                txtPersonalId.Text = person.PersonalID.ToString();

                txtFirstName.Text = person.FirstName;

                txtLastName.Text = person.LastName;

                txtShSh.Text = person.ShSh;

                txtBirthDate.Text = person.BirthDay.ToShortDateString();

                txtMobile.Text = person.Mobile;

                txtHomePhone.Text = person.Tel;

                ddlAccessLevel.SelectedValue = person.AId.ToString();

                ddlDepartment.SelectedValue = person.DepId.ToString();

                ddlJob.SelectedValue = person.JobId.ToString();

                ddlEducation.SelectedValue = person.EduId.ToString();

                txtAdress.Text = person.Address;

                txtNote.Text = person.PersonalsNote;

                txtSupStartContract.Text = person.StartContract.ToShortDateString();

                txtSupEndContract.Text = person.EndContract.ToShortDateString();


            }
        }
    }
    protected void addCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPersonal.aspx");
    }
    protected void editeCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchPersonals.aspx");
    }
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchPersonals.aspx");
    }
    protected void searchCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchPersonals.aspx");
    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void cvBasicInfo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtFirstName.Text == "" || txtLastName.Text == "" || txtBirthDate.Text == "" || txtShSh.Text == "" || txtMobile.Text == "")
        {
            imageError0.Visible = true;
            args.IsValid = false;

        }
        else
        {
            args.IsValid = true;
            try
            {
                int personalId = Convert.ToInt32(txtPersonalId.Text);

                Personals person = db.Personals.Where(a => a.PersonalID == personalId).Single();

                person.FirstName = txtFirstName.Text;

                person.LastName = txtLastName.Text;

                person.ShSh = txtShSh.Text;

                person.Mobile = txtMobile.Text;

                person.Tel = txtHomePhone.Text;

                person.BirthDay = Convert.ToDateTime(txtBirthDate.Text);

                person.DepId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);

                person.JobId = Convert.ToInt32(ddlJob.SelectedItem.Value);

                person.AId = Convert.ToInt32(ddlAccessLevel.SelectedItem.Value);

                person.PersonalsNote = txtNote.Text;

                person.Address = txtAdress.Text;

                person.StartContract = Convert.ToDateTime(txtSupStartContract.Text);

                person.EndContract = Convert.ToDateTime(txtSupEndContract.Text);

                person.EduId = Convert.ToInt32(ddlEducation.SelectedItem.Value);

                db.SaveChanges();

                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;

                imageSuccess.Visible = true;

                lblMessage.Visible = true;
                imageError.Visible = false;
                imageError0.Visible = false;
                lblMessage.Text = "پیام سیستم";
                errorOl.InnerHtml = "<li>مشخصات پرسنل با شماره " + txtPersonalId.Text + "با موفقیت ویرایش شد." + "</li>";

            }
            catch
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                imageError.Visible = true;
                imageSuccess.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml = "<li>استفاده از کاراکترهایی به جز اعداد و / در تاریخ تولد غیرمجاز است.</li>" +
                    "<li>مشکل در برقراری ارتباط با پایگاه داده رخ داده باشد.</li>";
            }
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int alId = Convert.ToInt32(ddlAccessLevel.SelectedItem.Value);

        ddlJob.Enabled = true;
        int depId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);

        var departman_job = from c in db.Departman_Job
                            where c.DepId == depId
                            select new
                            {
                                c.DepId,
                                c.JobId,
                                c.Jobs.JobName,
                            };


        bindClass.bindDropDownList(ddlJob, departman_job, "JobName", "JobId");
    }
}