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
/// Summary description for RemainVacs
/// </summary>
public class RemainVacs
{
    //personelId
    private int personelId;
    //YearId For RemainVac in This Year
    private int yearId;
    //vacations for personel in yera
    public IEnumerable<Personals_InDirectCode> personalVacs;
    public IEnumerable<DaysOfYear> days;
    OTA_DBEntities db = new OTA_DBEntities();
    public RemainVacs()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public RemainVacs(int pID, int yearId)
    {
        this.personelId = pID;
        this.yearId = yearId;
    }
    public RemainVacs(int pID)
    {
        this.personelId = pID;
    }
    public void RefreshRemainVac(int vacInfoId, string StartT, string EndT)
    {
        bool checkVac = CheckVacInfoId(vacInfoId);
        if (checkVac)
        {
            SetTime ts = new SetTime(StartT);
            SetTime te = new SetTime(EndT);
            int tsMinute = ts.GetMinutes();
            int tsHour = ts.GetHour();
            int teMinute = te.GetMinutes();
            int teHour = te.GetHour();
        }
    }
    //این تابع چک میکند که این اطلاعات مرخصی برای فردی با چنین کد پرسنلی 
    //در جدول مانده مرخصی ها وجود دارد یا نه!
    public bool CheckVacInfoId(int vacInoId)
    {
        bool checkRemainVacInfoId = false;
        IEnumerable<RemainVacations> RemainVacs = (from d in db.RemainVacations
                                                   where d.PerId == personelId &&
                                                   d.VacInfoId == vacInoId
                                                   select d);
        if (RemainVacs.Count() > 0)
        {
            checkRemainVacInfoId = true;
        }
        return checkRemainVacInfoId;
    }
//    public string CheckEmptyField()
//    {
//        DateTime dtNow;
//        string dtNowString = "2012/01/01";
//        dtNow = Convert.ToDateTime(dtNowString);
//        string yearName = yearId.ToString();
//        string vacName = "مرخصی";
//        string checkEmpty = "تا کنون هیچ روزی تحت عنوان روز کاری در سال " + yearName +
//            " " + "در سیستم ثبت نشده است." +
//            "<br/>" + "به منظور اطلاعات بیشتر با سوپروایزر بخش خود صحبت کنید.";
//        ///tedade rozaye sabt shode dar sale darkhaty be onvane roz kari
//        int daysNumber = (from y in db.DaysOfYear
//                          where y.Year.YearName == yearName && y.DsId == 1
//                          select y).Count();
//        if (daysNumber > 0)
//        {
//            days = (from y in db.DaysOfYear
//                    where y.Year.YearName == yearName && y.DsId == 1
//                    select y);
//            int personalVacsNumber = (from p in db.Personals_InDirectCode
//                                      where p.PerId == personelId && p.InDirectCodes.IdcName == vacName &&
//                                      p.DaysOfYear.Year.YearName == yearName
//                                      select p).Count();

//            if (personalVacsNumber > 0)
//            {
//                personalVacs = from p in db.Personals_InDirectCode
//                               where p.PerId == personelId &&
//                               p.Tarikh >= dtNow && p.InDirectCodes.IdcName == vacName
//                               select p;
//                checkEmpty = "شخصی با کد پرسنلی " + personelId.ToString() +
//                    " " + "به تعداد " + personalVacsNumber.ToString() + " " + "فیلد مرخصی برایش در پایگاه داده ثبت شده است.";

//            }
//            else
//            {
//                checkEmpty = "شخصی با کد پرسنلی " + personelId.ToString() + " "
//                    + "در سال " + yearName + " " + "تا کنون از هیچ نوع مرخصی استفاده نکرده است.";
//            }
//        }
//        return checkEmpty;
//    }
    public void FillRemainVacs(int vacationInfoID, double? remain)
    {
        List<Personals> AllPerson = (from p in db.Personals
                                     select p).ToList();
        int number = AllPerson.Count();
        if (number > 0)
        {
            try
            {
                for (int i = 0; i < number; i++)
                {
                    RemainVacations remainVacs = new RemainVacations();
                    remainVacs.PerId = AllPerson[i].PersonalID;
                    remainVacs.VacInfoId = vacationInfoID;
                    if (remain != null)
                        remainVacs.RemainVac = (float)remain;
                    else
                        remainVacs.RemainVac = 0;
                    db.AddToRemainVacations(remainVacs);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error!");
            }
        }
    }
    public bool CheckVacationTime(DateTime selectDate, int vacInfoId, float duringTime)
    {
        bool check = false;
        int numberVacInfo = (from v in db.Vac_Info
                             where v.VacInfoId == vacInfoId
                             select v).Count();
        int numberRemainVac = (from n in db.RemainVacations
                               where n.PerId == personelId &&
                               n.VacInfoId == vacInfoId
                               select n).Count();
        if (numberVacInfo > 0 && numberRemainVac > 0)
        {
            Vac_Info vacInfo = (from v in db.Vac_Info
                                where v.VacInfoId == vacInfoId
                                select v).Single();
            RemainVacations remainVacs = (from n in db.RemainVacations
                                          where n.PerId == personelId &&
                                          n.VacInfoId == vacInfoId
                                          select n).Single();
            check = CheckResult(selectDate, vacInfo, remainVacs, vacInfoId, duringTime);
        }

        return check;
    }
    public float RefreshRemainVacs(int vacInfoId, float duringTime)
    {
        float result = 0;
        RemainVacations rv = (from r in db.RemainVacations
                              where r.VacInfoId == vacInfoId && r.PerId == personelId
                              select r).Single();
        float remain = (float)rv.RemainVac;
        result = remain - duringTime;
        rv.RemainVac = result;
        db.SaveChanges();
        return result;
    }
    private bool CheckResult(DateTime selectDate, Vac_Info vacInfo, RemainVacations remainVacs, int vacInfoId, float duringTime)
    {
        bool check = false;
        int vacinfoId = vacInfo.VacInfoId;

        float remain = (float)remainVacs.RemainVac;
        float remainDuring = remain - duringTime;
        float maxYear = (float)vacInfo.MaxInYear;
        byte period = (byte)vacInfo.Period;
        float maxPeriod = (float)vacInfo.MaxInPeriod;
        if (period == 12)
        {
            if (remainDuring >= 0)

                check = CheckMonthPeriod(selectDate, remain, duringTime, maxPeriod, maxYear);
        }
        else
        {
            if (period == 6)
            {
                if (remainDuring >= 0)

                    check = CheckSixPeriod(selectDate, remain, duringTime, maxPeriod, maxYear);
            }
            else
            {
                if (period == 1)
                {
                    if (remainDuring >= 0)

                        check = CheckYearPeriod(remain, duringTime, maxYear);

                }
            }
        }
        return check;
    }
    //period monthly
    private bool CheckMonthPeriod(DateTime selectDate, float remain, float duringTime, float maxPeriod, float maxYear)
    {
        bool check = false;
        float Sum = 0;

        int year = selectDate.Year;
        int month = selectDate.Month;
        int day = selectDate.Day;

        string strSt = year.ToString() + "/" + month.ToString() + "/01";
        DateTime st = Convert.ToDateTime(strSt);

        DateTime et;
        string strEt;

        if (month == 1 || month == 4 || month == 6 ||
            month == 9 || month == 11)
        {
            strEt = year.ToString() + "/" + month.ToString() + "/30";
            et = Convert.ToDateTime(strEt);
        }
        else
        {
            if (month == 3 || month == 5 || month == 7 ||
                month == 8 || month == 10 || month == 12)
            {
                strEt = year.ToString() + "/" + month.ToString() + "/31";
                et = Convert.ToDateTime(strEt);
            }
            else
            {
                strEt = year.ToString() + "/" + month.ToString() + "/29";
                et = Convert.ToDateTime(strEt);
            }
        }
        List<Personals_InDirectCode> pVacs = (from p in db.Personals_InDirectCode
                                              where p.InDirectCodes.InDirectCodesType.IdcTypeId == 2 &&
                                              p.PerId == personelId
                                              && p.DaysOfYear.Tarikh >= st && p.DaysOfYear.Tarikh <= et
                                              select p).ToList();
        int counter = pVacs.Count();
        if (counter > 0)
        {
            for (int i = 0; i < counter; i++)
            {
                TimeSpan startTime = pVacs[i].StartTime;
                TimeSpan endTime = pVacs[i].EndTime;
                SetTime t = new SetTime();
                float tsum = t.CalculateTime(startTime, endTime);
                Sum += tsum;
            }
            float result = duringTime + Sum;
            if (result <= maxPeriod && result >= 0 && remain >= 0)
            {
                check = true;
            }

        }
        else
        {
            if (counter == 0)
            {
                if (remain >= duringTime)
                    check = true;
            }
        }
        return check;
    }
    //period yearly
    public bool CheckYearPeriod(float remainvac, float duringTime, float maxYear)
    {
        bool check = false;
        float sumRemain = remainvac - duringTime;
        if (sumRemain <= maxYear && sumRemain >= 0)
        {
            check = true;
        }
        return check;
    }
    //period SixMonthly
    private bool CheckSixPeriod(DateTime selectDate, float remain, float duringTime, float maxPeriod, float maxYear)
    {
        bool check = false;
        float Sum = 0;

        int year = selectDate.Year;
        int month = selectDate.Month;
        int day = selectDate.Day;

        string strSt;
        DateTime st;
        string strEt;
        DateTime et;

        if (month >= 1 && month <= 6)
        {
            strSt = year.ToString() + "/01/01";
            st = Convert.ToDateTime(strSt);
            strEt = year.ToString() + "/06/30";
            et = Convert.ToDateTime(strEt);
        }
        else
        {
            strSt = year.ToString() + "/07/01";
            st = Convert.ToDateTime(strSt);
            strEt = year.ToString() + "/12/31";
            et = Convert.ToDateTime(strEt);
        }
        List<Personals_InDirectCode> pVacs = (from p in db.Personals_InDirectCode
                                              where p.InDirectCodes.InDirectCodesType.IdcTypeId==2
                                              && p.PerId == personelId
                                              && p.DaysOfYear.Tarikh >= st && p.DaysOfYear.Tarikh <= et
                                              select p).ToList();
        int counter = pVacs.Count();
        if (counter > 0)
        {
            for (int i = 0; i < counter; i++)
            {
                TimeSpan startTime = pVacs[i].StartTime;
                TimeSpan endTime = pVacs[i].EndTime;
                SetTime t = new SetTime();
                float sumt = t.CalculateTime(startTime, endTime);
                Sum += sumt;
            }
            float result = duringTime + Sum;
            if (result <= maxPeriod && result >= 0 && remain >= 0)
            {
                check = true;
            }

        }
        else
        {
            if (counter == 0)
            {
                if (remain >= duringTime)
                    check = true;
            }
        }
        return check;
    }

}
