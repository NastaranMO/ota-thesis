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

public partial class Admin_SearchPersonals : System.Web.UI.Page
{
    OTA_DBModel.OTA_DBEntities db = new OTA_DBModel.OTA_DBEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblGuide.Visible = false;
        if (!IsPostBack)
        {
            //fill lblGuide
            lblGuideClass g = new lblGuideClass();
            lblGuide.Text = g.fillLblGuide("search");

            IEnumerable<Departmans> deps = from c in db.Departmans select c;
            bindClass.bindDropDownList(ddlDepartment, deps, "DepName", "DepId");
            int count = ddlDepartment.Items.Count;
            ddlDepartment.Items.Add("همه دپارتمان ها");

            ddlDepartment.Items[count].Selected = true;
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            //ddlAccessLevel.Items[3].Selected = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {   
        int PersonelId = 0;

        if (txtPersonalId.Text != "")
        {
            PersonelId = Convert.ToInt32(txtPersonalId.Text);
        }
        string fName = "-=-=-=-";
        if (txtFirstName.Text != "")
        {
            fName = txtFirstName.Text.Replace("ی", "ي");

        }

        string lName = "-=-=-=-";
        if (txtLastName.Text != "")
        {
            lName = txtLastName.Text.Replace("ی", "ي");
        }

        string shsh = "-=-=-=-";
        if (txtShSh.Text != "")
        {
            shsh = txtShSh.Text.Replace("ی", "ي");
        }
        string phone = "-=-=-=-";
        if (txtHomePhone.Text != "")
        {
            phone = txtHomePhone.Text.Replace("ی", "ي");
        }

        int depId = 0;
        if (ddlDepartment.SelectedItem.Text != "همه دپارتمان ها")
        {
            depId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
        }

        try
        {
            if (depId != 0)// Search between Sepcial Department
            {
                string sqlQuery = "";

                if (txtFirstName.Text == "" && txtHomePhone.Text == "" && txtLastName.Text == "" && txtPersonalId.Text == "" && txtShSh.Text == "")
                {
                    sqlQuery = "Select * From Per_Dep_Job Where DepId = " + depId;

                    Departmans dep = db.Departmans.Where(a => a.DepId == depId).Single();

                    listGrid.InnerText = "جستجو در بین پرسنل دپارتمان " + dep.DepName;

                }
                else
                {
                    sqlQuery = "Select * From Per_Dep_Job Where " +
                        "(PersonalId = " + PersonelId + "OR FirstName Like '%" + fName + "%' OR LastName Like '%" + lName + "%' " +
                        "OR ShSh Like '%" + shsh + "' OR Mobile Like '%" + phone + "%' OR Tel LIKE '%" + phone + "%') AND DepId = " + depId;

                    Departmans dep = db.Departmans.Where(a => a.DepId == depId).Single();

                    listGrid.InnerText = "جستجو در بین پرسنل دپارتمان " + dep.DepName;
                }
                
                ObjectResult<Per_Dep_Job> query = db.ExecuteStoreQuery<Per_Dep_Job>(sqlQuery);
                
                bindClass.bindGrid(gvPersonals, query);
                
                MultiView1.ActiveViewIndex = 1;

                lblFooter.Text = "تعداد رکوردها: " + gvPersonals.Rows.Count.ToString();

            }

            else if (depId == 0) //Search between All Department
            {
                string sqlQuery = "";

                if (txtFirstName.Text == "" && txtHomePhone.Text == "" && txtLastName.Text == "" && txtPersonalId.Text == "" && txtShSh.Text == "")
                {
                    sqlQuery = "Select * From Per_Dep_Job ";
                   
                    listGrid.InnerText = "لیست کامل پرسنل";
                }

                else
                {
                    sqlQuery = "Select * From Per_Dep_Job Where " +
                   "PersonalId = " + PersonelId + "OR FirstName Like '%" + fName + "%' OR LastName Like '%" + lName + "%' " +
                   "OR ShSh Like '%" + shsh + "' OR Mobile Like '%" + phone + "%' OR Tel LIKE '%" + phone + "%'";

                    listGrid.InnerText = "جستجو در بین همه دپارتمان ها با مشخصات خاص پرسنلی";
                }
                
                ObjectResult<Per_Dep_Job> query = db.ExecuteStoreQuery<Per_Dep_Job>(sqlQuery);
                
                bindClass.bindGrid(gvPersonals, query);
                
                MultiView1.ActiveViewIndex = 1;

                lblFooter.Text ="تعداد رکوردها: "+ gvPersonals.Rows.Count.ToString();
            }
        }
        catch
        {
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>خطا در برقراری ارتباط با پایگاه داده رخ داده است.</li>";
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
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtHomePhone.Text = "";
        txtPersonalId.Text = "";
        txtShSh.Text = "";
    }
    protected void gvPersonals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;

        int personalId = (int)gvPersonals.DataKeys[index].Value;

        Personals person = db.Personals.Where(a => a.PersonalID == personalId).Single();

        db.DeleteObject(person);

        db.SaveChanges();

    }
    protected void gvPersonals_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = (int)gvPersonals.SelectedIndex;
        int personalId = (int)gvPersonals.DataKeys[index].Value;

        Response.Redirect("EditPersonal.aspx?pid=" + personalId);
    }

    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }

    protected void cvBasicInfo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (txtFirstName.Text == "" && txtHomePhone.Text == "" && txtLastName.Text == "" && txtPersonalId.Text == "" && txtShSh.Text == "" && ddlDepartment.SelectedItem.Text == "همه دپارتمان ها")
        //{
        //    imageError0.Visible = true;
        //    args.IsValid = false;

        //}
        //else
        //{

           
        //}
    }
}