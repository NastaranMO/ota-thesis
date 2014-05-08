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


public partial class Admin_code_amaliaty : System.Web.UI.Page
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
            //visible
            btnAdd.Visible = true;
            btnClear.Visible = true;
            //multiView
            lblLegenName.Text = "لیست کدهای عملیاتی";
            gvDirectCode.SelectedIndex = -1;
            MultiView2.ActiveViewIndex = -1;
            MultiView1.ActiveViewIndex = 1;
            FillGrid();
            listGrid.InnerHtml = "- لیست کدهای عملیاتی -";
        }

    }
    //menu Edit
    //menu edit
    protected void editeCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "ویرایش و حذف کد عملیاتی ";
        lblGuideClass d = new lblGuideClass();
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("edit") + "<br/>" + d.fillLblGuide("delete");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
  
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن کد عملیاتی جدید";
       
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add") ;
        btnAdd.Visible = true;
        btnSearch.Visible = false;
       
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "جستجو";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("search");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
     
        btnEdit.Visible = false;
        btnClear.Visible = true;

    }
    
    protected void lnkGuide_Click(object sender, EventArgs e)
    {
        lblGuide.Visible = true;
    }
    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        lblGuide.Text = "";
        gvDirectCode.SelectedIndex = -1;
        lblLegenName.Text = "لیست کدهای عملیاتی";
        FillGrid();
        MultiView1.ActiveViewIndex = 1;
    }
    //Clear TextBoxes
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        btnClear.Visible = true;
    }
    //method For Clear TextBoxes
    protected void Clear()
    {
        txtCode.Text = "";
        txtName.Text = "";
        txtNote.Text = "";
    }
    //btn Back in Search Page
    protected void btnBack_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        MultiView1.ActiveViewIndex=1;
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
        if (txtName.Text == "" || txtCode.Text == "")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            try
            {
                args.IsValid = true;
                int dCs = (from c in db.DirectCodes
                           select c).Count();

                Checking chk = new Checking("DirectCodes", txtName.Text.Replace("ی", "ي"), "DcName");
                if (chk.CheckDuplicateData())
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    DirectCodes dc = new DirectCodes();
                    dc.DcId = Convert.ToInt32(txtCode.Text);
                    dc.DcName = txtName.Text.Replace("ی", "ي");
                    if (txtNote.Text == "")
                        dc.DcNote = "-";
                    else
                        dc.DcNote = txtNote.Text.Replace("ی", "ي");
                    db.AddToDirectCodes(dc);
                    db.SaveChanges();
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" +
                        "عملیات " + "<b>" + txtName.Text + "</b>" +
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
        lblLegenName.Text = "لیست کدهای عملیاتی";
        gvDirectCode.SelectedIndex = -1;
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        listGrid.InnerHtml = "- لیست کدهای عملیاتی -";
    }
    //fill Grid
    protected void FillGrid()
    {
        IEnumerable<DirectCodes> directCodes = from d in db.DirectCodes
                                               select d;
        int number = directCodes.Count();
        listGrid.InnerHtml = "- لیست کدهای عملیاتی -";
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvDirectCode, directCodes);
    }
    protected void gvDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvDirectCode.SelectedValue);
        DirectCodes dc = (from d in db.DirectCodes
                          where d.DcId == index
                          select d).Single();
        txtCode.Enabled = false;
        txtCode.Text = dc.DcId.ToString();
        txtName.Text = dc.DcName;
        txtNote.Text = dc.DcNote;
        btnEdit.Visible = true;
        btnSearch.Visible = false;
     
        btnAdd.Visible = false;
        btnBack.Visible = true;
        MultiView1.ActiveViewIndex = 0;
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
            try
            {
                args.IsValid = true;
                Checking chk = new Checking("DirectCodes", txtName.Text.Replace("ی", "ي"), "DcName", Convert.ToInt32(txtCode.Text), "DcId");
                if (chk.EditCheckDuplicateData())
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    DirectCodes dc = (from d in db.DirectCodes
                                      where d.DcId == id
                                      select d).Single();
                    dc.DcName = txtName.Text.Replace("ی", "ي");
                    if (txtNote.Text == "")
                        dc.DcNote = "-";
                    else
                        dc.DcNote = txtNote.Text;
                    db.SaveChanges();
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" + "کد عملیاتی " + "<b>" + id.ToString() + "</b>" +
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
    "<li>رکوردی با این شرح عملیات در پایگاه داده وجود دارد.</li>" +

                    "<li>ممکن است در برقراری ارتباط با پایگاه داده مشکلی رخ داده باشد.</li>";
            }
        }
    }
    protected void gvDirectCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        int dCId = Convert.ToInt32(gvDirectCode.DataKeys[index].Value);
        try
        {
            DirectCodes dc = (from d in db.DirectCodes
                              where d.DcId == dCId
                              select d).Single();
            db.DeleteObject(dc);
            db.SaveChanges();
            imageSuccess.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "پیام سیستم";
            errorOl.InnerHtml = "<li>عملیات " +
    "<b>" + dc.DcName + "</b> " +
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvDirectCode.SelectedIndex = -1;
        btnClear.Visible = true;
        int DcId;

        ObjectResult<DirectCodes> directCode;
        if (txtCode.Text == "" && txtName.Text != "")
        {
            directCode = db.ExecuteStoreQuery<DirectCodes>("select * from DirectCodes where DcName Like '%" +
                txtName.Text + "%'");
            bindClass.bindGrid(gvDirectCode, directCode);
            MultiView1.ActiveViewIndex = 1;
        }
        else
        {
            if (txtName.Text == "" && txtCode.Text != "")
            {
                DcId =Convert.ToInt32(txtCode.Text);
                directCode = db.ExecuteStoreQuery<DirectCodes>("select * from DirectCodes where DcId=" + DcId);
                bindClass.bindGrid(gvDirectCode, directCode);
                MultiView1.ActiveViewIndex = 1;
            }
        }
        if (txtCode.Text != "" && txtName.Text != "")
        {
            DcId = Convert.ToInt32(txtCode.Text);
            directCode = db.ExecuteStoreQuery<DirectCodes>("select * from DirectCodes where DcId=" + DcId +
                " And DcName Like '%" + txtName.Text + "%'");
            bindClass.bindGrid(gvDirectCode, directCode);
            MultiView1.ActiveViewIndex = 1;
        }
        else
        {
            if (txtCode.Text == "" && txtName.Text == "")
            {
                lblSearchMessage.Visible = true;
                lblSearchMessage.Text = "<br/>"+"<img alt=''  src='../images/btn/error.png' />"+" "
                    +"برای انجام عمل جستجو وارد کردن حداقل یکی از فیلدهای ستاره دار الزامی است.";
            }
        }
        
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnBack.Visible = true;
    }
}
