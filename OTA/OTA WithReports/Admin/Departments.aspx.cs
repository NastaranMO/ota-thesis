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


public partial class Admin_Departments : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnAdd.Visible = true;
            btnSearch.Visible = true;
            lblGuideClass c = new lblGuideClass();
            lblGuide.Text = c.fillLblGuide("add");
            MultiView1.ActiveViewIndex = 1;
            IEnumerable<Departmans> dep = from d in db.Departmans
                                          select d;
            int number = dep.Count();
            lblLegenName.Text = "لیست دپارتمان ها";
            lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
            bindClass.bindGrid(gvDep, dep);

        }
        //visible false
        lblGuide.Visible = false;

    }
    protected void ClearTextBoxes()
    {
        txtDepId.Text = "";
        txtDepName.Text = "";
        txtNote.Text = "";
    }
    protected void addCode_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 0;

        imageError0.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;

        lblLegenName.Text = "افزودن دپارتمان جدید";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnSearch.Visible = false;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        txtDepId.Enabled = true;
        ClearTextBoxes();
    }
    protected void editeCode_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        imageError0.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;

        lblLegenName.Text = "لیست دپارتمان ها";
        gvDep.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        IEnumerable<Departmans> dep = from d in db.Departmans
                                      select d;
        int number = dep.Count();
        lblLegenName.Text = "لیست دپارتمان ها";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvDep, dep);
    }
    protected void searchCode_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 0;
        imageError0.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;

        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        txtDepId.Enabled = true;
        ClearTextBoxes();
    }

    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;

    }
    //Clear Text in TextBoxes
    protected void btnClear_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        imageError0.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;

        txtDepId.Text = "";
        txtDepName.Text = "";
        txtNote.Text = "";
    }
    //Add To Db 
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //db.sp_updateDep(,Convert.ToInt32(txtDepId.Text), txtDepName.Text, txtNote.Text);
        //db.SaveChanges();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        errorOl.InnerHtml = "";
        MultiView2.ActiveViewIndex = -1;
        imageError0.Visible = false;
        imageError.Visible = false;
        imageSuccess.Visible = false;
        gvDep.SelectedIndex = -1;

        int depId = 0;
        string depName = "";
        ObjectResult<Departmans> deps;

        if (txtDepId.Text != "" && txtDepName.Text == "")
        {
            depId = Convert.ToInt32(txtDepId.Text);
        }

        else if (txtDepName.Text != "" && txtDepId.Text == "")
        {
            depName = txtDepName.Text;
        }

        else
        {
            if (txtDepId.Text != "" && txtDepName.Text != "")
            {
                depId = Convert.ToInt32(txtDepId.Text);
                depName = txtDepName.Text;
            }
            else
            {
                depId = -1;
                depName = "-";
            }
        }


        deps = db.ExecuteStoreQuery<Departmans>("Select * From Departmans Where DepId = " + depId + " OR DepName Like '%" + depName + "%'");
        gvDep.DataSource = deps;
        gvDep.DataBind();
        MultiView1.ActiveViewIndex = 1;


        lblFooter.Text = "تعداد رکوردها: " + "<b style='font-family:B nazanin;font-size:12px;'>" + +gvDep.Rows.Count + "</b>";
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        IEnumerable<Departmans> dep = from d in db.Departmans
                                      select d;
        int number = dep.Count();
        lblLegenName.Text = "لیست دپارتمان ها";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvDep, dep);

        gvDep.SelectedIndex = -1;
    }
    //Definition Row For GridView
    int i = 0;
    public string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void gvDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = gvDep.SelectedIndex;
        int depId = (int)gvDep.DataKeys[index].Value;
        ViewState["editDepId"] = depId;

        Departmans dep = (db.Departmans.Where(a => a.DepId == depId)).Single();
        txtDepId.Text = dep.DepId.ToString();
        txtDepName.Text = dep.DepName;
        txtNote.Text = dep.DepNote;
        MultiView1.ActiveViewIndex = 0;
        btnAdd.Visible = false;
        btnEdit.Visible = true;
        txtDepId.Enabled = false;
        btnDisEdit.Visible = false;
    }

    protected void gvDep_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = e.RowIndex;
            int depId = (int)gvDep.DataKeys[index].Value;

            Departmans dep = (db.Departmans.Where(a => a.DepId == depId).Single());

            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
            lblMessage.Text = "پیام سیستم  ";
            errorOl.InnerHtml = "<li>دپارتمان " +
                "<b>" + dep.DepName + "</b> " +
                "با موفقیت از سیستم حذف شد." +
                "</li>";
            imageSuccess.Visible = true;
            db.DeleteObject(dep);
            db.SaveChanges();

        }
        catch(Exception)
        {
            MultiView2.ActiveViewIndex = 0;
            imageSuccess.Visible = false;
            imageError0.Visible = false;
            imageError.Visible = true;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>این دپارتمان قبلا از سیستم حذف شده است.</li>" +
           "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>"+
             "<li>شما اجازه ی حذف این دپارتمان را ندارید.</li>";
        }
        IEnumerable<Departmans> deps = from c in db.Departmans
                                       select c;
        gvDep.DataSource = deps;
        gvDep.DataBind();

        lblFooter.Text = "تعداد رکوردها: " + gvDep.Rows.Count;
    }
    protected void btnEdit_Click1(object sender, EventArgs e)
    {

    }
    protected void cvAddDepartmen_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtDepId.Text == "" || txtDepName.Text == "")
        {
            args.IsValid = false;
            imageError0.Visible = true;
        }
        else
        {

            args.IsValid = true;
            Checking chk = new Checking("Departmans", txtDepName.Text.Replace("ی", "ي"), "DepName");
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
                        db.sp_insertDep(Convert.ToInt32(txtDepId.Text), txtDepName.Text.Replace("ی", "ي"), "-");
                        MultiView1.ActiveViewIndex = -1;
                    }
                    else
                    {
                        db.sp_insertDep(Convert.ToInt32(txtDepId.Text), txtDepName.Text.Replace("ی", "ي"), txtNote.Text);
                        MultiView1.ActiveViewIndex = -1;
                    }

                    db.SaveChanges();
                    imageError0.Visible = false;
                    imageError.Visible = false;
                    imageSuccess.Visible = true;

                    MultiView2.ActiveViewIndex = 0;
                    lblMessage.Text = "پیام سیستم  ";
                    errorOl.InnerHtml = "<li>دپارتمان " +
                        "<b>" + txtDepName.Text + "</b> " +
                        "با کد " +
                        "<b>" + txtDepId.Text + "</b> " +
                        "با موفقیت در سیستم ثبت شد." +
                        "</li>";
                }
            }
            catch (Exception)
            {
                imageError.Visible = true;
                imageSuccess.Visible = false;

                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml = "<li>شرح دپارتمان یا کد دپارتمان تکراری است.</li>" +
                                  
                                    "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>";
            }
        }
    }
    protected void cvEditDepartmen_ServerValidate(object source, ServerValidateEventArgs args)
    {
        txtDepId.Enabled = false;
        if (txtDepName.Text == "" || txtDepId.Text == "")
        {
            args.IsValid = false;
            imageError0.Visible = true;
        }
        else
        {
            args.IsValid = true;

            errorOl.InnerHtml = "";
            MultiView2.ActiveViewIndex = -1;
            imageError0.Visible = false;
            imageError.Visible = false;
            imageSuccess.Visible = false;

            int depId = 0;
            Checking chk = new Checking("Departmans", txtDepName.Text.Replace("ی", "ي"), "DepName",Convert.ToInt32( txtDepId.Text),"JobId");
            bool Duplicate = chk.EditCheckDuplicateData();

            try
            {
                if (Duplicate)
                {
                    throw new Exception("Duplicate Departman Name");
                }
                else
                {
                    depId = (int)ViewState["editDepId"];
                    Departmans dep = (db.Departmans.Where(a => a.DepId == depId).Single());
                   
                    dep.DepName = txtDepName.Text.Replace("ی", "ي");
                    dep.DepNote = txtNote.Text;
                    db.SaveChanges();

                    MultiView2.ActiveViewIndex = 0;
                    MultiView1.ActiveViewIndex = -1;

                    imageSuccess.Visible = true;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>دپارتمان " +
                        "<b>" + txtDepName.Text + "</b> " +
                        "با موفقیت ویرایش شد." +
                        "</li>";
                }

            }
            catch (Exception)
            {
                imageSuccess.Visible = false;
                imageError.Visible = true;
                MultiView2.ActiveViewIndex = 0;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                errorOl.InnerHtml = "<li>کد دپارتمان یا نام دپارتمان تکراری است.</li>" +
 "<li>.در برقراری ارتباط با پایگاه داده مشکلی رخ داده است</li>";
   

            }
        }

    }
}
