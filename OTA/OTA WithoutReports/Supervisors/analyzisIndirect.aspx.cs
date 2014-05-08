using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.EntityClient;
using OTA_DBModel;

public partial class Supervisors_Reports_analyzisIndirect : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    int pid;
    int depid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pid = 107;
            ViewState["i"] = pid;
            depid = (from d in db.Personals
                     where d.PersonalID == pid
                     select d.DepId).Single();
            ViewState["d"] = depid;
            FillPersonelGrid();
            divError.Visible = false;

        }
        else
        {
            depid = Convert.ToInt32(ViewState["d"]);
            pid = Convert.ToInt32(ViewState["i"]);
        }
    }
    int i = 0;
    protected string GetRow()
    {
        i++;
        return i.ToString();
    }
    protected void FillPersonelGrid()
    {
        var query = from d in db.Personals
                    where d.DepId == depid
                    select new
                    {
                        d.PersonalID,
                        d.FirstName,
                        d.LastName,
                        d.Jobs.JobName
                    };
        bindClass.bindGrid(gvPersonels, query);
    }
    protected void gvPersonels_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvPersonels.SelectedValue);
        Personals person = (from p in db.Personals
                            where p.PersonalID == index
                            select p).Single();
        lblFullName.Text = person.FirstName + " " + person.LastName;
        lblPID.Text = person.PersonalID.ToString();
        MultiView1.ActiveViewIndex = 0;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        int perId=Convert.ToInt32(lblPID.Text);
        DateTime  st =Convert.ToDateTime( txtStartDate.Text);
        DateTime et = Convert.ToDateTime(txtEndDate.Text);
        List<rptInDirects> vacs = (from a in db.rptInDirects
                   where a.IdcTypeName == "مرخصی" && a.PersonalID==perId && a.Tarikh>=st && a.Tarikh<=et
                   select a).ToList();
        List<rptInDirects> mamoriat = (from m in db.rptInDirects
                       where m.IdcTypeName == "ماموریت" && m.PersonalID == perId && m.Tarikh>=st && m.Tarikh<=et
                       select m).ToList();
        List<rptInDirects> hozor =( from h in db.rptInDirects
                    where h.PersonalID == perId && h.IdcName == "حضور" && h.Tarikh>=st && h.Tarikh<=et
                    select h).ToList();
        int numberm = mamoriat.Count();
        int number = vacs.Count();
        int numberh = hozor.Count();
        //Response.Write(numberh);
        float sumM = 0;
        float sum = 0;
        float sumh=0;
        string stdate;
        string etdate;
        for (int i = 0; i < numberm; i++)
        {
            stdate = mamoriat[i].StartTime.ToString();
            etdate = mamoriat[i].EndTime.ToString();
            TimeSpan stSpan = SetTime.GetTime(stdate);
            TimeSpan etSpan = SetTime.GetTime(etdate);
            SetTime aa = new SetTime();
            float ekjhtelaf = aa.CalculateTime(stSpan, etSpan);
            sumM += sumM + ekjhtelaf;

        }
        for (int i = 0; i < number; i++)
        {
            stdate = vacs[i].StartTime.ToString();
            etdate = vacs[i].EndTime.ToString();
            TimeSpan stSpan = SetTime.GetTime(stdate);
            TimeSpan etSpan = SetTime.GetTime(etdate);
            SetTime aa = new SetTime();
            float ekjhtelaf = aa.CalculateTime(stSpan, etSpan);
            sum += sum + ekjhtelaf;
            //Response.Write(sum);
        }
        //Response.Write(sum);
        
        for (int i = 0; i < numberh; i++)
        {
            stdate = hozor[i].StartTime.ToString();
            etdate = hozor[i].EndTime.ToString();
            TimeSpan stSpan = SetTime.GetTime(stdate);
            TimeSpan etSpan = SetTime.GetTime(etdate);
            //Response.Write("<br/>" + stdate + " " + etdate + " " + "<br/>");
            SetTime aa = new SetTime();
            float ekjhtelaf = aa.CalculateTime(stSpan, etSpan);
            sumh += ekjhtelaf;
            
            //Response.Write(sumh);
        }
        //Response.Write(sumh);
        lblvac.Text = sum.ToString() + " ساعت";
       lblhozaor.Text = sumh.ToString() + " ساعت";
        lblmamoriat.Text = sumM.ToString()+" ساعت";
        //Response.Write(numberh);
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