using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.EntityClient;
using OTA_DBModel;

public partial class User_IndirectHafte : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();

    int pid;
    int depid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            imgCustomError.Visible = false;

            pid = 102;
            ViewState["pidd"] = pid;
            depid = (from d in db.Personals
                     where d.PersonalID == pid
                     select d.DepId).Single();
            ViewState["d"] = depid;
            gv.Visible = false;
        }
        else
        {
            pid = Convert.ToInt32(ViewState["pidd"]);
            depid = Convert.ToInt32(ViewState["d"]);
        }
    }
    protected string GetDate(object dt)
    {
        string strDt;
        DateTime dateDt = Convert.ToDateTime(dt);
        strDt = dateDt.ToShortDateString();
        return strDt;
    }
    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void cvAdd_ServerValidate(object source, ServerValidateEventArgs args)
    {
        
        if (txtEndDate.Text == "" || txtStartDate.Text == "")
        {
            args.IsValid = false;
            imgCustomError.Visible = true;
            cvAdd.ErrorMessage = "وارد کردن بازه ی زمانی الزامی است.";
            DateTime st = Convert.ToDateTime(txtStartDate.Text);
            DateTime et = Convert.ToDateTime(txtEndDate.Text);

        }
        else
        {
            DateTime st=Convert.ToDateTime(txtStartDate.Text);
            DateTime et=Convert.ToDateTime(txtEndDate.Text);
            args.IsValid = true;
            imgCustomError.Visible = false;
            var query = from d in db.Personals_InDirectCode
                        where d.PerId == pid && d.DaysOfYear.Tarikh >= st && d.DaysOfYear.Tarikh <= et
                        select new
                        {
                            d.PerId,
                            d.StartTime,
                            d.EndTime,
                            d.DaysOfYear.Tarikh,
                            d.InDirectCodes.IdcName,
                            d.DaysOfYear.Rooz,
                            d.DaysOfYear.DayState.DsName
                        };
            bindClass.bindGrid(gvInDirect, query);
            listGrid.InnerHtml = "ورود و خروج از تاریخ "+st.ToShortDateString()+" تا تاریخ "+et.ToShortDateString();
            gv.Visible = true;
            lblFooter.Text = "تعداد رکوردها: "+query.Count();
        }

    }
    protected void lnkTimeshit_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndirectHafte.aspx");
    }
}