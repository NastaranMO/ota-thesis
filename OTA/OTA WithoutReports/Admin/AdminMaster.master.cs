using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.EntityClient;
using OTA_DBModel;

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
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
        Timer1.Interval =5000;
        lblTime.Text = DateTime.Now.ToShortTimeString();
    }
    protected void FillAccount()
    {
        DateTime dt=DateTime.Now;
        string dta=dt.ToShortDateString();
        dt=Convert.ToDateTime(dta);
        int aId = Convert.ToInt32(Profile.personelId);
        string adminName;
        string vaziateRooz;
        try
        {
            Admins admin = (from a in db.Admins
                            where a.AdminId == aId
                            select a).Single();
            DaysOfYear day = (from s in db.DaysOfYear
                              where s.Tarikh == dt
                              select s).Single();
            vaziateRooz = day.DayState.DsName;
            adminName = admin.AdminName;
            FillLbl(vaziateRooz, adminName);
            FillDeps();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void FillLbl(string vaziat,string adminName)
    {
        lblFullName.Text = adminName;
        lblvaziatrooz.Text = vaziat;
    }
    protected void FillDeps()
    {
        IEnumerable<Personals> persons = from a in db.Personals
                                         select a;
        List<Departmans> deps = (from d in db.Departmans
                                 select d).ToList();
        int numOdDep = deps.Count();
        int[] numPersonel=new int[numOdDep];
        int j = 0;
        int depid=0;
        for (int i = 0; i < numOdDep; i++)
        {
            depid=deps[i].DepId;
            j = (from a in db.Personals
                 where a.DepId == depid
                 select a).Count();
            lblDeps.Text+= "دپارتمان "+deps[i].DepName+": "+j.ToString()+"<br/>";
            numPersonel[i] = j;
        }
        int sumPersons = persons.Count();
       
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../default.aspx");
    }
}
