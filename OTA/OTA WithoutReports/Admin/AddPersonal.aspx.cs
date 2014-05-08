using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.EntityClient;
using System.Data;
using OTA_DBModel;

public partial class Admin_AddPersonal : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        imageError0.Visible = false;
        imageError1.Visible = false;
        imageError2.Visible = false;

        imageError.Visible = false;
        lblGuide.Visible = false;

        if (!IsPostBack)
        {
            IEnumerable<Departmans> deps = from c in db.Departmans
                                           select c;
            IEnumerable<AccessLevel> al = from c in db.AccessLevel
                                          select c;
            IEnumerable<Education> edus = from c in db.Education
                                          select c;

            bindClass.bindDropDownList(ddlEducation, edus, "EduName", "EduId");
            
            bindClass.bindDropDownList(ddlDepartment, deps, "DepName", "DepId");
            
            bindClass.bindDropDownList(ddlAccessLevel, al, "AName", "AId");

            int count = ddlDepartment.Items.Count;

            ddlDepartment.Items.Add("لیست دپارتمان ها");

            ddlDepartment.Items[count].Selected = true;

            MultiView1.ActiveViewIndex = 0;
            
            MultiView2.ActiveViewIndex = -1;
            
            //fill lblGuide
            lblGuideClass g = new lblGuideClass();
            lblGuide.Text = g.fillLblGuide("add");
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

    protected void btnBasicInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnLoginInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnEmployee_Click(object sender, EventArgs e)
    {

    }
    protected void btnSuperviser_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Personals person = new Personals();

            person.PersonalID = Convert.ToInt32(lblPersonalId.Text);

            person.FirstName = txtFirstName.Text.Replace("ی", "ي");
            person.LastName = txtLastName.Text.Replace("ی", "ي");
            person.ShSh = txtShSh.Text.Replace("ی", "ي");
            person.BirthDay = Convert.ToDateTime(lblBirthDate.Text);
            person.Tel = lblPhone.Text;
            person.Mobile = lblMobile.Text;
            person.PersonalsNote = txtNote.Text.Replace("ی", "ي");
            person.Address = txtAdress.Text;
            person.Password = ViewState["password"].ToString();
            person.LastVisited = DateTime.Now;
            person.AId = Convert.ToInt32(ddlAccessLevel.SelectedItem.Value);
            person.DepId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            person.JobId = Convert.ToInt32(ddlJob.SelectedItem.Value);
            person.StartContract = Convert.ToDateTime(lblStartContract.Text);
            person.EndContract = Convert.ToDateTime(lblEndContract.Text);
            person.EduId = Convert.ToInt32(ddlEducation.SelectedItem.Value);

            db.AddToPersonals(person);
            db.SaveChanges();

            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>" + "پرسنل با شماره" + "<b>" + lblPersonalId.Text + "</b>" +
                " با نام " + "<b>" + lblFullName.Text + "</b>" +
                "با موفقیت درسیستم ثبت شد." +
                "</li>";
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
        }
        catch
        {
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            imageError.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>استفاده از کاراکترهایی به جز اعداد و / در تاریخ تولد غیرمجاز است.</li>" +
                "<li>مشکل در برقراری ارتباط با پایگاه داده رخ داده باشد.</li>";
        }
    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }


    protected void cvBasicInfo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtPersonalId.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtShSh.Text == "" || txtBirthDate.Text == "" || txtMobile.Text == "")
        {
            args.IsValid = false;
            imageError0.Visible = true;
        }
        else
        {
            args.IsValid = true;
           
            MultiView1.ActiveViewIndex = 1;
        }
    }
    protected void cvLoginInfo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtPassword.Text == "" || txtConfirmPass.Text == "")
        {
            args.IsValid = false;
            imageError1.Visible = true;
        }
        else
        {

            args.IsValid = true;
            ViewState["password"] = txtPassword.Text.GetHashCode().ToString();
            MultiView1.ActiveViewIndex = 2;

        }

        ViewState["alname"] = ddlAccessLevel.SelectedItem.Text;
    }


    protected void cvEmployee_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtEmpEndContract.Text == "" || txtEmpStartContract.Text == "" || ddlJob.Enabled == false)
        {
            args.IsValid = false;
            imageError2.Visible = true;
        }
        else
        {
            args.IsValid = true;

            MultiView1.ActiveViewIndex = 3;
            lblAccessLevel.Text = ViewState["alname"].ToString();
            lblFullName.Text = txtFirstName.Text + " " + txtLastName.Text;
            lblPersonalId.Text = txtPersonalId.Text;

            lblDepartment.Text = ddlDepartment.SelectedItem.Text;
            lblJob.Text = ddlJob.SelectedItem.Text;

            lblBirthDate.Text = txtBirthDate.Text;
            lblShSh.Text = txtShSh.Text;
            lblStartContract.Text = txtEmpStartContract.Text;
            lblEndContract.Text = txtEmpEndContract.Text;
            lblPhone.Text = txtHomePhone.Text;
            lblMobile.Text = txtMobile.Text;

            Jobs job = db.Jobs.Where(a => a.JobName == ddlJob.SelectedItem.Text).Single();
            ViewState["jobid"] = job.JobId;
            int depId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            ViewState["depid"] = depId;
        }

    }
  
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        int count = ddlDepartment.Items.Count;

        ddlDepartment.Items.RemoveAt(--count);

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