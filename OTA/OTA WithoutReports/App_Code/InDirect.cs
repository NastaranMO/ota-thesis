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

/// <summary>
/// Summary description for InDirect
/// </summary>
public class InDirect
{
    OTA_DBEntities db = new OTA_DBEntities();
    private int depId;
    IEnumerable<Personals> person;
    public InDirect()
    {

    }
    public InDirect(int depid)
    {
        this.depId = depid;
        person = from p in db.Personals
                   where p.DepId == depId
                   select p;
    }
    public int[] ReturnDayId()
    {
        int[] pIdc = new int[1];

        string saal = "2012/01/01";
        DateTime startSaal = Convert.ToDateTime(saal);
        DateTime emrooz = DateTime.Now;

        List<DaysOfYear> days = (from d in db.DaysOfYear
                                 where d.Tarikh >= startSaal && d.Tarikh <= emrooz && d.DsId == 1
                                 select d).ToList();
        int numDays = days.Count();
        List<Personals_InDirectCode> pIndirects = (from p in db.Personals_InDirectCode
                                                   where p.DaysOfYear.Tarikh >= startSaal
                                                   select p).ToList();
        int numpIndirects = pIndirects.Count();

        if (numDays > 0 && numpIndirects > 0)
        {
            pIdc = new int[numDays];
            int j = 0;
            int check = 0;
            for (int i = 0; i < numDays; i++)
            {
                check = 0;
                for (int k = 0; k < numpIndirects; k++)
                {
                    if (pIndirects[k].dayId == days[i].dayId)
                    {
                        check++;
                        if (pIndirects[k].DcsId == false)
                            check=0;
                    }
                }
                if (check == 0)
                {

                    pIdc[j] = days[i].dayId;
                    //Response.Write(pIdc[j]);
                    j++;
                }
            }
        }
        else
        {
            if (numDays > 0 && numpIndirects == 0)
            {
                pIdc = new int[1];
                pIdc[0] = -1;
            }
        }
        return pIdc;

    }
    public int[] DirectReturnDayId()
    {
        int[] pdc = new int[1];

        string saal = "2012/01/01";
        DateTime startSaal = Convert.ToDateTime(saal);
        DateTime emrooz = DateTime.Now;

        List<DaysOfYear> days = (from d in db.DaysOfYear
                                 where d.Tarikh >= startSaal && d.Tarikh <= emrooz && d.DsId == 1
                                 select d).ToList();
        int numDays = days.Count();
        List<int> kk = (from n in db.Personals_DirectCode
                        where n.Personals.DepId == depId && n.DaysOfYear.Tarikh >= startSaal && n.DcsId == true
                        select n.PdcId).ToList();
        int numpdirects = kk.Count();

        if (numDays > 0 && numpdirects > 0)
        {
            pdc = new int[numDays];
            int j = 0;
            int check = 0;
            for (int i = 0; i < numDays; i++)
            {
                check = 0;
                for (int k = 0; k < numpdirects; k++)
                {
                    int pdirectid = kk[k];
                    Personals_DirectCode pdirects = (from g in db.Personals_DirectCode
                                                     where g.PdcId == pdirectid
                                                     select g).Single();
                    if (pdirects.dayId == days[i].dayId)
                    {
                        check++;
                        if (pdirects.DcsId == false)
                            check = 0;
                    }

                }
                if (check == 0)
                {

                    pdc[j] = days[i].dayId;
                    j++;
                }

            }
        }
        else
        {
            if (numDays > 0 && numpdirects == 0)
            {
                pdc = new int[1];
                pdc[0] = -1;
            }
        }
        return pdc;
    }

}
