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

public partial class Admin_vaziat_rooz : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
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
        txtCode.Enabled = true;
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
        }

    }
    protected void addCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن وضعیت جدید برای روزها ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnSearch.Visible = false;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
        Clear();
    }
    protected void editeCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "ویرایش وضعیت های ثبت شده ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("edit");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnDisEdit.Visible = true;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "حذف وضعیت ثبت شده ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("delete");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    protected void searchCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnDisEdit.Visible = false;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        gvDaysState.SelectedIndex = -1;
        lblLegenName.Text = "لیست وضعیت های ثبت شده";
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
        lblFooter.Text = "تعداد رکوردها: " + gvDaysState.Rows.Count.ToString();
    }

    protected void Clear()
    {
        txtCode.Text = "";
        txtName.Text = "";
        txtNote.Text = "";
    }

    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }

    protected void FillGrid()
    {
        IEnumerable<DayState> states = from c in db.DayState
                                       select c;
        listGrid.InnerHtml = "- لیست وضعیت های ثبت شده -";
        gvDaysState.DataSource = states;
        gvDaysState.DataBind();
    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        if (!lblGuide.Visible)
        {
            lblGuide.Visible = true;
        }
        else if (lblGuide.Visible)
        {
            lblGuide.Visible = false;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        btnClear.Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnBack.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvDaysState.SelectedIndex = -1;
        btnClear.Visible = true;
        int dsId = 0;
        ObjectResult<DayState> dayStates;
        
        if (txtCode.Text == "" && txtName.Text != "")
        {
            dayStates = db.ExecuteStoreQuery<DayState>("select * from DayState where DsName Like '%" +
                txtName.Text + "%'");
            bindClass.bindGrid(gvDaysState, dayStates);
            MultiView1.ActiveViewIndex = 1;
            lblFooter.Text = "تعداد رکوردها: " + gvDaysState.Rows.Count.ToString();
        }
        else
        {
            if (txtName.Text == "" && txtCode.Text != "")
            {
                dsId = Convert.ToInt32(txtCode.Text);
                dayStates = db.ExecuteStoreQuery<DayState>("select * from DayState where DsId=" + dsId);
                bindClass.bindGrid(gvDaysState, dayStates);
                MultiView1.ActiveViewIndex = 1;
                lblFooter.Text = "تعداد رکوردها: " + gvDaysState.Rows.Count.ToString();
            }
        }
        if (txtCode.Text != "" && txtName.Text != "")
        {
            dsId = Convert.ToInt32(txtCode.Text);
            dayStates = db.ExecuteStoreQuery<DayState>("select * from DayState where DsId=" + dsId +
                " And DsName Like '%" + txtName.Text + "%'");
            bindClass.bindGrid(gvDaysState, dayStates);
            MultiView1.ActiveViewIndex = 1;
            lblFooter.Text = "تعداد رکوردها: " + gvDaysState.Rows.Count.ToString();
        }
        else
        {
            if (txtCode.Text == "" && txtName.Text == "")
            {
                lblSearchMessage.Visible = true;
                lblSearchMessage.Text = "<br/>" + "<img alt=''  src='../images/btn/error.png' />" + " "
                    + "برای انجام عمل جستجو وارد کردن حداقل یکی از فیلدهای ستاره دار الزامی است.";
            }
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        gvDaysState.SelectedIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        btnClear.Visible = true;
    }
    protected void cvAdd_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtName.Text == "" || txtCode.Text == "")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            args.IsValid = true;

            try
            {
                string dayStateName = txtName.Text.Replace("ی", "ي");

                int dsId = 0;
                try
                {
                    dsId = Convert.ToInt32(txtCode.Text);
                }
                catch
                {
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    imageError.Visible = true;
                    lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                    errorOl.InnerHtml = "<li>استفاده از حروف در فیلد 'کد وضعیت روز' غیر مجاز است.</li>";
                }
                string note = "";

                if (txtNote.Text != "")
                {
                    note = txtNote.Text.Replace("ی", "ي");
                }
                else
                {
                    note = "-";
                }

                Checking chk = new Checking("DayState", dayStateName, "DsName");

                if (chk.CheckDuplicateData())
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    DayState ds = new DayState();

                    ds.DsId = dsId;
                    ds.DsName = dayStateName;
                    ds.DsNote = note;
                    //db.sp_InsertDayState(dsId, dayStateName, note);  Not Working Correctly!!
                    db.AddToDayState(ds);
                    db.SaveChanges();

                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" +
                        "وضعیت " + "<b>" + txtName.Text + "</b>" +
                        " " + "با کد " + "<b>" + txtCode.Text + "</b>" +
                        " " + "با موفقیت در سیستم ثبت شد." +
                        "</li>";
                }
            }
            catch (Exception)
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = -1;
                imageError.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
                
                errorOl.InnerHtml ="<li>اطلاعات وضعیت وارد شده ممکن است، قبلا در پایگاه داده ثبت شده باشد.</li>" +
                                   "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
            }

        }
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        txtCode.Enabled = false;
        btnBack.Visible = true;
        int id = Convert.ToInt32(txtCode.Text);
        
        if (txtName.Text == "")
        {
            args.IsValid = false;
            imgCustomEdit.Visible = true;
        }

        else
        {
            string dsName = txtName.Text.Replace("ی","ي");
            int dsId = Convert.ToInt32(txtCode.Text);
            string note = "";
            if (txtNote.Text != "")
            {
                note = txtNote.Text.Replace("ی", "ي");
            }
            else
            {
                note = "-";
            }

            try
            {
                args.IsValid = true;
                Checking chk = new Checking("DayState", txtName.Text.Replace("ی", "ي"), "DsName", Convert.ToInt32(txtCode.Text), "DsId");
                if (chk.EditCheckDuplicateData())
                {
                    throw new Exception("Duplicate Data");
                }
                else
                {
                    DayState state = db.DayState.Where(a => a.DsId == dsId).Single();                                        
                    db.sp_UpdateDayState(dsId, dsId, dsName, note);
                    db.SaveChanges();

                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" + "کد وضعیت " + "<b>" + id.ToString() + "</b>" +
                        " " + "با موفقیت ویرایش شد."
                        + "</li>";
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;

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
    "<li>رکوردی با این نام وضعیت، در پایگاه داده وجود دارد.</li>" +

                    "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
            }
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {        
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        gvDaysState.SelectedIndex = -1;
        FillGrid();
        lblFooter.Text = "تعداد رکوردها: " + gvDaysState.Rows.Count.ToString();
        listGrid.InnerHtml = "- لیست وضعیت های ثبت شده -";
    }
    protected void gvDirectCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        int dsId = (int)gvDaysState.DataKeys[index].Value;
        try
        {
            DayState ds = db.DayState.Where(a => a.DsId == dsId).Single();
            db.sp_DeleteDayState(dsId);
            db.SaveChanges();

            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>وضعیت " +
    "<b>" + ds.DsName + "</b> " +
    "با موفقیت از سیستم حذف شد." +
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
    protected void gvDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int index = Convert.ToInt32(gvDaysState.SelectedValue);
        int index = gvDaysState.SelectedIndex;
        int dsId =(int) gvDaysState.DataKeys[index].Value;

        DayState ds = db.DayState.Where(a => a.DsId == dsId).Single();

        txtCode.Enabled = false;
        txtCode.Text = ds.DsId.ToString();
        txtName.Text = ds.DsName;
        txtNote.Text = ds.DsNote;

        btnEdit.Visible = true;
        btnSearch.Visible = false;
        btnDisEdit.Visible = false;
        btnAdd.Visible = false;
        btnBack.Visible = true;
        MultiView1.ActiveViewIndex = 0;
    }
}