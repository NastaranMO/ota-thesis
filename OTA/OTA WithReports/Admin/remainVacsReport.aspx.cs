using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OTA_DBModel;
using System.Data.EntityClient;

public partial class Admin_Reports_remainVacsReport : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void btnCreateReport_Click(object sender, EventArgs e)
    {
        DateTime st = Convert.ToDateTime(txtStartDate.Text);
        DateTime et = Convert.ToDateTime(txtEndDate.Text);
        int syear = st.Year;
        int eyear = et.Year;
        if (eyear == syear)
        {
            MultiView1.ActiveViewIndex = 1;
            string yName = eyear.ToString();
            var query = from d in db.viewremainVacs
                        where d.YearName == yName
                        select new
                            {
                                d.PersonalID,
                                d.MaxTransfer,
                                d.RemainVac,
                                d.YearName,
                                d.IdcName
                            };
            bindClass.bindGrid(gvInDirectCode, query);
            int number = query.Count();
            lblFooter.Text = "تعداد رکوردها: "+number.ToString();
        }
    }
    protected void lnkListPersonnel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PersonalReport.aspx");
    }
    protected void lnkTimeshit_Click(object sender, EventArgs e)
    {
        Response.Redirect("DirectReport.aspx");
    }
    protected void lnkIndirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndirectTotalReport.aspx");
    }
    protected void lnkVacs_Click(object sender, EventArgs e)
    {
        Response.Redirect("remainVacsReport.aspx");
    }
    protected void lnkAnalyz_Click(object sender, EventArgs e)
    {

    }
}