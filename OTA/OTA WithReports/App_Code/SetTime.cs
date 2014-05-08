using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SetTime
/// </summary>
public class SetTime
{
    public string TimeString;

	public SetTime()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SetTime(string timeString)
    {
        this.TimeString = timeString;
    }

    /// <summary>
    /// یه رشته میگیره و معادل تایم اسپن رو بر میگردونه
    /// رشته ساعت وارده می تونه به صورت 2،14،14:30،2:3 و هر شکل دیگه ای باشه.
    /// برای استفاده ازش باید از کلاس آبجکت ساخت و کانستراکتور رو با رشته مقداردهی کرد.
    /// </summary>
    /// <returns>TimeSpan Time</returns>
    public TimeSpan GetTime()
    {        
        string timeStr = TimeString;
        int Index = timeStr.IndexOf(":");
        int strLenght = timeStr.Length;

        if (Index == 1)
        {
            timeStr = "0" + timeStr;
        }

        else if (Index == -1 && strLenght == 2)
        {
            timeStr = timeStr + ":00";
        }

        else if (Index == -1 && strLenght == 1)
        {
            timeStr = "0" + timeStr + ":00";
        }


        Index = timeStr.IndexOf(":");

        string Minutes = timeStr.Substring(++Index);

        if (Minutes.Length == 1)
        {
            Minutes = "0" + Minutes;
        }
        else if (Minutes.Length > 2)
        {
            Minutes = Minutes.Substring(0, 2);
        }

        int Hour = Convert.ToInt32(timeStr.Substring(0, 2));
        int minutes = Convert.ToInt32(Minutes);

        TimeSpan Time = new TimeSpan(Hour, minutes, 0);
        return Time;
    }

    /// <summary>
    /// یه رشته میگیره و معادل تایم اسپن رو بر میگردونه
    /// رشته ساعت وارده می تونه به صورت 2،14،14:30،2:3 و هر شکل دیگه ای باشه.
    /// به صورت استاتیک و با مقداردهی متد استفاده میشه.
    /// نکته: استفاده از متد قبلی بیشتر توصیه میشه این کپی پیست شده است شاید کار نکنه:دی
    /// </summary>
    /// <returns>TimeSpan Time</returns>
    /// 
    
    public static TimeSpan GetTime(string TimeString)
    {
        string timeStr = TimeString;
        int Index = timeStr.IndexOf(":");
        int strLenght = timeStr.Length;

        if (Index == 1)
        {
            timeStr = "0" + timeStr;
        }

        else if (Index == -1 && strLenght == 2)
        {
            timeStr = timeStr + ":00";
        }

        else if (Index == -1 && strLenght == 1)
        {
            timeStr = "0" + timeStr + ":00";
        }


        Index = timeStr.IndexOf(":");

        string Minutes = timeStr.Substring(++Index);

        if (Minutes.Length == 1)
        {
            Minutes = "0" + Minutes;
        }
        else if (Minutes.Length > 2)
        {
            Minutes = Minutes.Substring(0, 2);
        }

        int Hour = Convert.ToInt32(timeStr.Substring(0, 2));
        int minutes = Convert.ToInt32(Minutes);

        TimeSpan Time = new TimeSpan(Hour, minutes, 0);
        return Time;

    }

    public int GetHour()
    {
        string timeStr = TimeString;
        int Index = timeStr.IndexOf(":");
        int strLenght = timeStr.Length;

        if (Index == 1)
        {
            timeStr = "0" + timeStr;
        }

        else if (Index == -1 && strLenght == 2)
        {
            timeStr = timeStr + ":00";
        }

        else if (Index == -1 && strLenght == 1)
        {
            timeStr = "0" + timeStr + ":00";
        }


        Index = timeStr.IndexOf(":");

        string Minutes = timeStr.Substring(++Index);

        if (Minutes.Length == 1)
        {
            Minutes = "0" + Minutes;
        }
        else if (Minutes.Length > 2)
        {
            Minutes = Minutes.Substring(0, 2);
        }

        int Hour = Convert.ToInt32(timeStr.Substring(0, 2));
        int minutes = Convert.ToInt32(Minutes);

        return Hour;
    }

    public int GetMinutes()
    {
        string timeStr = TimeString;
        int Index = timeStr.IndexOf(":");
        int strLenght = timeStr.Length;

        if (Index == 1)
        {
            timeStr = "0" + timeStr;
        }

        else if (Index == -1 && strLenght == 2)
        {
            timeStr = timeStr + ":00";
        }

        else if (Index == -1 && strLenght == 1)
        {
            timeStr = "0" + timeStr + ":00";
        }


        Index = timeStr.IndexOf(":");

        string Minutes = timeStr.Substring(++Index);

        if (Minutes.Length == 1)
        {
            Minutes = "0" + Minutes;
        }
        else if (Minutes.Length > 2)
        {
            Minutes = Minutes.Substring(0, 2);
        }

        int Hour = Convert.ToInt32(timeStr.Substring(0, 2));
        int minutes = Convert.ToInt32(Minutes);

        return minutes;
    }
    public TimeSpan GetDuringTimes(TimeSpan startT, TimeSpan endT)
    {
        TimeSpan duringT = endT - startT;
        return duringT;
    }
    public float CalculateTime(TimeSpan startT, TimeSpan endT)
    {
        TimeSpan duringT = GetDuringTimes(startT,endT);
        float result = 0;
        string strduringT = duringT.ToString();
        SetTime t = new SetTime(strduringT);
        float minuteT = t.GetMinutes();
        float hourT = t.GetHour();
        minuteT = minuteT / 60;
        result = minuteT + hourT;
        return result;
    }
    public string GetCorrectDesimal(float result)
    {
        string strResult = result.ToString();
        int dot = strResult.IndexOf(".");
        if (dot != -1)
        {
            strResult = strResult.Substring(0, dot + 3);
        }
        else
            strResult = strResult.Substring(0, dot + 3);
        return strResult;
    }
}