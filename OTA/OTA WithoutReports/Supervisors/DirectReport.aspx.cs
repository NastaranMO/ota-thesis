using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.EntityClient;
using System.Data;
using System.Data.SqlClient;
using OTA_DBModel;
//using CrystalDecisions.CrystalReports.Engine;
public partial class Supervisors_Reports_DirectReport : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void btnCreateReport_Click(object sender, EventArgs e)
    {

    }
    protected void cvAdd_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //try
        //{

        //    DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
        //    DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
        //    if (startDate > endDate)
        //    {
        //        args.IsValid = false;
        //        imgCustomError.Visible = true;
        //        cvAdd.ErrorMessage = "تاریخ شروع نباید از تاریخ پایان کوچکتر باشد.";
        //    }
        //    else
        //    {
        //        args.IsValid = true;
        //        imgCustomError.Visible = false;

        //        SqlConnection cn = ADOConnection.GetAdoConnection();
        //        string sqlQuery = "";
        //        int pId=Convert.ToInt32(Profile.personelId);
        //        int depId = (from d in db.Personals
        //                     where d.PersonalID == pId
        //                     select d.DepId).Single();
        //        string depName = (from i in db.Departmans
        //                          where i.DepId == depId
        //                          select i.DepName).Single();

        //        sqlQuery = "Select * From rptDirect Where  Tarikh >='" + startDate.ToShortDateString() + "' AND Tarikh <= '" + endDate.ToShortDateString() + "' AND DepName LIKE '" + depName + "'";
        //        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cn);

        //        DataTable dt = new DataTable();

        //        da.Fill(dt);

        //        string strPath = Server.MapPath(@"~\CrystalReports\Direct_Tarikh.rpt");

        //        ReportDocument rpt = new ReportDocument();

        //        rpt.Load(strPath);

        //        rpt.SetDataSource(dt);

        //        string strEndDate = endDate.Year + "/" + endDate.Month + "/" + endDate.Day;
        //        string strStartDate = startDate.Year + "/" + startDate.Month + "/" + startDate.Day;
        //        string strToday = DateTime.Today.Year + "/" + DateTime.Today.Month + "/" + DateTime.Today.Day;

        //        rpt.SetParameterValue("ReportDate", strToday);
        //        rpt.SetParameterValue("StartDate", strStartDate);
        //        rpt.SetParameterValue("EndDate", strEndDate);

        //        int ExportId = Convert.ToInt32(ddlExportFormat.SelectedItem.Value);

        //        if (ExportId == 1)
        //        {
        //            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "Direct_Report");
        //        }

        //        else if (ExportId == 2)
        //        {
        //            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Direct_Report");
        //        }

        //        else if (ExportId == 3)
        //        {
        //            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.RichText, Response, true, "Direct_Report");
        //        }

        //        else
        //        {
        //            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Direct_Report");
        //        }
        //    }
        //}
        //catch
        //{
        //    args.IsValid = false;
        //    imgCustomError.Visible = true;
        //    cvAdd.ErrorMessage = "فرمت تاریخ وارد شده نادرست است.";
        //}

    }
    protected void lnkTimeshit_Click(object sender, EventArgs e)
    {
        Response.Redirect("DirectReport.aspx");
    }
    protected void lnkIndirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndirectReport.aspx");
    }
    protected void lnkVacs_Click(object sender, EventArgs e)
    {
        Response.Redirect("RemainVacs.aspx");
    }
    protected void lnkAnalyzis_Click(object sender, EventArgs e)
    {
        Response.Redirect("analyzisIndirect.aspx");
    }
}