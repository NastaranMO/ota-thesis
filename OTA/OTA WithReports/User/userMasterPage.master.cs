using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTA_DBModel;
using System.Data.EntityClient;
using System.Data;

public partial class User_userMasterPage : System.Web.UI.MasterPage
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTime.Text = DateTime.Now.ToShortTimeString();
            FillAccount();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Timer1.Interval = 5000;
        lblTime.Text = DateTime.Now.ToShortTimeString();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../default.aspx");
    }
    protected void FillAccount()
    {
        DateTime dt=DateTime.Now;
        string dta = dt.ToShortDateString();
        dt = Convert.ToDateTime(dta);
        int pId = Convert.ToInt32(Profile.personelId);
        string FullName;
        string depName;
        string jobName;
        string dayState;
        string launch;
        string work;

        try
        {
            Personals personel = (from p in db.Personals
                                  where p.PersonalID == pId
                                  select p).Single();
            DaysOfYear day = (from i in db.DaysOfYear
                              where i.Tarikh == dt
                              select i).Single();
            FullName = personel.FirstName + " " + personel.LastName;
            depName = personel.Departmans.DepName;
            jobName = personel.Jobs.JobName;
            dayState = day.DayState.DsName;
            launch = day.StartLunchTime.ToString().Substring(0, 5) + " تا " + day.EndLunchTime.ToString().Substring(0, 5);
            work = day.StartWorkTime.ToString().Substring(0, 5) + " تا " + day.EndWorkTime.ToString().Substring(0, 5);
            FillTextBoxes(FullName, depName, jobName,launch,dayState,work);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        
    }
    protected void FillTextBoxes(string fullName,string depName,string jobName,string launch,string daystate,string work)
    {
        lblFullName.Text = fullName;
        lblDepatment.Text = depName;
        lblSemat.Text = jobName;
        lblLaunch.Text = launch;
        lblWorkTime.Text = work;
        lblvaziatrooz.Text = daystate;
    }
}
