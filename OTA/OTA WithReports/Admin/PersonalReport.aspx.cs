using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.EntityClient;
using System.Data;
using OTA_DBModel;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

public partial class Admin_Reports_PersonalReport : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IEnumerable<Departmans> deps = from c in db.Departmans
                                           select c;
            bindClass.bindDropDownList(ddlDepartments, deps, "DepName", "DepId");
            ddlDepartments.DataBind();
            ddlDepartments.Items.Add("همه دپارتمان ها");
            int count = ddlDepartments.Items.Count;
            ddlDepartments.Items[--count].Selected = true;
        }
    }
    protected void lnkGuide_Click(object sender, EventArgs e)
    {

    }
    protected void btnCreateReport_Click(object sender, EventArgs e)
    {
        //uncomment in a system with crystal report
        if (ddlDepartments.SelectedItem.Text == "همه دپارتمان ها")
        {
            SqlConnection cn = ADOConnection.GetAdoConnection();
            SqlDataAdapter da = new SqlDataAdapter("Select * From rpt_PersonalList", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string strPath = Server.MapPath(@"~\CrystalReports\rptPersonals_Department.rpt");

            ReportDocument reportDoc = new ReportDocument();

            reportDoc.Load(strPath);

            reportDoc.SetDataSource(dt);

            reportDoc.SetParameterValue("Date", DateTime.Today.ToShortDateString());


            int ExportId = Convert.ToInt32(ddlExportFormat.SelectedItem.Value);
            if (ExportId == 1)
            {
                reportDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "Personal_Report");
            }
            else if (ExportId == 2)
            {
                reportDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Personal_Report");
            }
            else if (ExportId == 3)
            {
                reportDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.RichText, Response, true, "Personal_Report");
            }
            else
            {
                reportDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Personal_Report");
            }

        }
        else
        {
            string depName = ddlDepartments.SelectedItem.Text;
            int depId = Convert.ToInt32(ddlDepartments.SelectedItem.Value);

            SqlConnection cn = ADOConnection.GetAdoConnection();
            SqlDataAdapter da = new SqlDataAdapter("Select * From rpt_PersonalList Where DepId=" + depId, cn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            string strPath = Server.MapPath(@"~/CrystalReports/rptPersonals_Department.rpt");

            ReportDocument rpt = new ReportDocument();

            rpt.Load(strPath);
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("Date", DateTime.Today.ToShortDateString());

            int ExportId = Convert.ToInt32(ddlExportFormat.SelectedItem.Value);
            if (ExportId == 1)
            {
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "Personal_Report");
            }
            else if (ExportId == 2)
            {
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Personal_Report");
            }
            else if (ExportId == 3)
            {
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.RichText, Response, true, "Personal_Report");
            }
            else
            {
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Personal_Report");
            }
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