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

public partial class Admin_code_gheyre_amaliat : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            //visible
            btnAdd.Visible = true;
            btnClear.Visible = true;
            //multiView
            lblLegenName.Text = "لیست کدهای غیر عملیاتی";
            MultiView2.ActiveViewIndex = -1;
            MultiView1.ActiveViewIndex = 1;
            FillGrid();
            listGrid.InnerHtml = "- لیست کدهای غیر عملیاتی -";
            //fillDdlIndirect
            FillDdl();
        }
    }
    //Fill ddlIndirectCodesTypes
    protected void FillDdl()
    {
        IEnumerable<InDirectCodesType> indirectCodesType = (from d in db.InDirectCodesType
                                                            select d).OrderByDescending(a => a.IdcTypeId);
        bindClass.bindDropDownList(ddlIndirectTypes, indirectCodesType, "IdcTypeName", "IdcTypeId");
        ddlIndirectTypes.Items.Add("- انتخاب کنید -");
        ddlIndirectTypes.SelectedValue = "- انتخاب کنید -";
    }
    //menu edit
    protected void editeCode_Click(object sender, EventArgs e)
    {
        gvInDirectCode.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "ویرایش کد غیر عملیاتی ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("edit");
        btnAdd.Visible = false;
        btnSearch.Visible = true;
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu add
    protected void addCode_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "افزودن کد غیر عملیاتی جدید";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("add");
        btnAdd.Visible = true;
        btnSearch.Visible = false;
   
        btnEdit.Visible = false;
        btnClear.Visible = true;
    }
    //menu search
    protected void searchCode_Click(object sender, EventArgs e)
    {
        gvInDirectCode.SelectedIndex = -1;
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
    //menu delete
    protected void deleteCode_Click(object sender, EventArgs e)
    {
        gvInDirectCode.SelectedIndex = -1;
        Clear();
        MultiView1.ActiveViewIndex = 0;
        lblLegenName.Text = "حذف کد غیر عملیاتی ";
        lblGuideClass c = new lblGuideClass();
        lblGuide.Text = c.fillLblGuide("delete");
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
        lblGuide.Text = "لیست کدهای غیر عملیاتی";
        lblLegenName.Text = "لیست کدهای غیر عملیاتی";
        gvInDirectCode.SelectedIndex = -1;
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        listGrid.InnerHtml = "- لیست کدهای غیر عملیاتی -";
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
        txtName.Text = "";
        txtNote.Text = "";
        ddlIndirectTypes.SelectedValue = "- انتخاب کنید -";
    }
    //btn Back in Search Page
    protected void btnBack_Click(object sender, EventArgs e)
    {
        gvInDirectCode.SelectedIndex = -1;
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
        if (txtName.Text == "" ||   ddlIndirectTypes.SelectedValue == "- انتخاب کنید -")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
        }
        else
        {
            try
            {
                args.IsValid = true;
                int Idcs = (from c in db.InDirectCodes
                           select c).Count();

                Checking chk = new Checking("InDirectCodes", txtName.Text.Replace("ی", "ي"), "IdcName");
                if (chk.CheckDuplicateData())
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    InDirectCodes Idc = new InDirectCodes();
                    Idc.IdcTypeId = Convert.ToInt32(ddlIndirectTypes.SelectedValue);
                    Idc.IdcName = txtName.Text.Replace("ی", "ي");
                    if (txtNote.Text == "")
                        Idc.IdcNote = "-";
                    else
                        Idc.IdcNote = txtNote.Text.Replace("ی", "ي");
                    db.AddToInDirectCodes(Idc);
                    db.SaveChanges();
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    MultiView1.ActiveViewIndex = -1;
                    MultiView2.ActiveViewIndex = 0;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" +
                        "عملیات " + "<b>" + txtName.Text + "</b>" +
                        " " + "از نوع " + "<b>" + ddlIndirectTypes.SelectedItem.Text + "</b>" +
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
        lblLegenName.Text = "لیست کدهای غیر عملیاتی";
        gvInDirectCode.SelectedIndex = -1;
        MultiView2.ActiveViewIndex = -1;
        MultiView1.ActiveViewIndex = 1;
        FillGrid();
        listGrid.InnerHtml = "- لیست کدهای غیر عملیاتی -";
    }
    //fill Grid
    protected void FillGrid()
    {
        IEnumerable<InDirectCodes> inDirectCodes = from d in db.InDirectCodes
                                               select d;
        int number = inDirectCodes.Count();
        lblFooter.Text = "تعداد رکوردها : " + "<b style='font-family:B nazanin;font-size:12px;'>" + number.ToString() + "</b>";
        bindClass.bindGrid(gvInDirectCode, inDirectCodes);
    }
    protected void gvDirectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvInDirectCode.SelectedValue);
        ViewState["idcid"] = index;
        InDirectCodes idc = (from d in db.InDirectCodes
                          where d.IdcId == index
                          select d).Single();
        txtName.Text = idc.IdcName;
        txtNote.Text = idc.IdcNote;
        ddlIndirectTypes.SelectedValue = idc.IdcTypeId.ToString();
        btnEdit.Visible = true;
        btnSearch.Visible = false;
        btnAdd.Visible = false;
        btnBack.Visible = true;
        MultiView1.ActiveViewIndex = 0;
    }
    protected void cVEdit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        btnBack.Visible = true;

        int IdcId = Convert.ToInt32(ViewState["idcid"]);

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
                Checking chk = new Checking("InDirectCodes", txtName.Text.Replace("ی", "ي"), "IdcName", IdcId, "IdcId");
                if (chk.EditCheckDuplicateData())
                {
                    throw new Exception("dade tekrari");
                }
                else
                {
                    InDirectCodes dc = (from d in db.InDirectCodes
                                      where d.IdcId == IdcId
                                      select d).Single();
                    dc.IdcTypeId = Convert.ToInt32(ddlIndirectTypes.SelectedValue);
                    dc.IdcName = txtName.Text.Replace("ی", "ي");
                    if (txtNote.Text == "")
                        dc.IdcNote = "-";
                    else
                        dc.IdcNote = txtNote.Text.Replace("ی", "ي");
                    db.SaveChanges();
                    imageSuccess.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "پیام سیستم";
                    errorOl.InnerHtml = "<li>" + "عملیات " + "<b>" + ddlIndirectTypes.SelectedItem.Text + "</b>" +
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
        int IdcId = Convert.ToInt32(gvInDirectCode.DataKeys[index].Value);
        string IdcName = ddlIndirectTypes.SelectedItem.Text;
        try
        {
            int piCheck = (from pi in db.Personals_InDirectCode
                                                          where pi.IdcId == IdcId
                                                          select pi.PidcId).Count();
            int vacInfoCheck = (from vi in db.Vac_Info
                                where vi.VacId == IdcId
                                select vi.VacInfoId).Count();
            if (piCheck == 0 && vacInfoCheck == 0)
            {
                InDirectCodes dc = (from d in db.InDirectCodes
                                    where d.IdcId == IdcId
                                    select d).Single();
                db.DeleteObject(dc);
                db.SaveChanges();
                imageSuccess.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "پیام سیستم";
                errorOl.InnerHtml = "<li>عملیات " +
        "<b>" + IdcName + "</b> " +
        "با موفقیت از سیستم حذف شد." +
        "</li>";
                MultiView1.ActiveViewIndex = -1;
                MultiView2.ActiveViewIndex = 0;
            }
        }
        catch (Exception)
        {
            imageError.Visible = true;
            lblMessage.Visible = true;
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            lblMessage.Text = "پیام سیستم  " + " <b style='color:green;font-size:9px;'>(خطاهای ممکن!)</b>";
            errorOl.InnerHtml = "<li>این عملیات در داده های سیستمی دیگری وجود دارد و امکان حذف آن وجود تدارد.</li>" 
                +"<li>این عملیات قبلا از سیستم حذف شده است.</li>" +
           "<li>در برقراری ارتباط با پایگاه داده مشکلی رخ داده است.</li>";

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvInDirectCode.SelectedIndex = -1;

        btnClear.Visible = true;

        int IdcId;

        ObjectResult<InDirectCodes> IndirectCode;
        if (txtName.Text == "" && ddlIndirectTypes.SelectedValue == "- انتخاب کنید -")
        {
            lblSearchMessage.Visible = true;
            lblSearchMessage.Text = "<br/>" + "<img alt=''  src='../images/btn/error.png' />" + " "
                + "برای انجام عمل جستجو وارد کردن حداقل یکی از فیلدهای ستاره دار الزامی است.";
        }
        else
        {
            if (ddlIndirectTypes.SelectedValue == "- انتخاب کنید -" && txtName.Text != "")
            {
                IndirectCode = db.ExecuteStoreQuery<InDirectCodes>("select * from InDirectCodes where IdcName Like '%" +
        txtName.Text + "%'");
                bindClass.bindGrid(gvInDirectCode, IndirectCode);
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                if (txtName.Text == "" && ddlIndirectTypes.SelectedValue != "- انتخاب کنید -")
                {
                    IdcId = Convert.ToInt32(ddlIndirectTypes.SelectedValue);
                    IndirectCode = db.ExecuteStoreQuery<InDirectCodes>("select * from InDirectCodes where IdcTypeId=" + IdcId);
                    bindClass.bindGrid(gvInDirectCode, IndirectCode);
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    if (txtName.Text != "" && ddlIndirectTypes.SelectedValue != "- انتخاب کنید -")
                    {
                        IdcId = Convert.ToInt32(ddlIndirectTypes.SelectedValue);
                        IndirectCode = db.ExecuteStoreQuery<InDirectCodes>("select * from InDirectCodes where IdcTypeId=" + IdcId +
                            " And IdcName Like '%" + txtName.Text + "%'");
                        bindClass.bindGrid(gvInDirectCode, IndirectCode);
                        MultiView1.ActiveViewIndex = 1;
                    }
                }
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnBack.Visible = true;
    }
}