using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports_AnalyzIndirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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