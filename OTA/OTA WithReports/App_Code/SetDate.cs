using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.EntityClient;
using System.Data;
using System.Data.Objects;
using OTA_DBModel;
using System.Data.SqlClient;


/// <summary>
/// Summary description for SetDate
/// </summary>
public class SetDate
{
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
    public static string Msg { get; set; }

	public SetDate(DateTime sDate,DateTime eDate)
	{
        this.StartDate = sDate;
        this.EndDate = eDate;
	}

    public int GetDuration()
    {
        int Days = 0;
        if (EndDate > StartDate)
        {
            TimeSpan Duration = EndDate - StartDate;
            Days = Duration.Days;
        }
        else if(EndDate<StartDate)
        {
            Days = -1;
            throw new Exception("EndDate must greater than StartDate");            
        }

        return Days;
    }

    public static DateTime[] GapBetweenDates(DateTime Date,int CheckDuration)
    {
        OTA_DBEntities db = new OTA_DBEntities();
        
        DateTime startChecking = Date.AddDays(-CheckDuration);

        IEnumerable<DaysOfYear> doys = from c in db.DaysOfYear
                                       where c.Tarikh <= Date && c.Tarikh >= startChecking
                                       select c;
        
        int i = 0;
        DateTime sc = startChecking;
        
        foreach (DaysOfYear li in doys)
        {
            if (li.Tarikh != sc)
            {
                i++;
            }
            sc = sc.AddDays(1);
        }

        DateTime[] GapDates = new DateTime[i];

        int index = 0;
        foreach (DaysOfYear li in doys)
        {           
            if (li.Tarikh != startChecking)
            {
                GapDates[index] = startChecking;
                
                index++;
            }
           startChecking= startChecking.AddDays(1);
        }
        return GapDates;
    }

    public static bool CheckCorrectYearId(int Year)
    {
        SqlConnection cn = ADOConnection.GetAdoConnection();
        SqlDataAdapter da = new SqlDataAdapter("Select * From Year", cn);
        DataTable dt = new DataTable();
        da.Fill(dt); 
        DataRow[] rows= dt.Select("YearName ="+Year);
        bool CorrectYear = false;
        
        if (rows.Count() > 1)
        {
            SetDate.Msg = "سال " + Year + "بیش از یکبار در سیستم ثبت شده.";
            CorrectYear = false;
        }
        else if (rows.Count() == 0)
        {
            SetDate.Msg = "سال " + Year + "در سیستم ثبت نشده است.";
            CorrectYear = false;
        }
        else if (rows.Count() == 1)
        {
            CorrectYear = true;
        }
        return CorrectYear;
    }


}