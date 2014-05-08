using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for lblGuideClass
/// </summary>
public class lblGuideClass
{
	public lblGuideClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string fillLblGuide(string lnkName)
    {
        string lblGuide = lnkName;
        switch (lblGuide)
        {
            case "edit":
                lblGuide = edit();
                break;
            case "delete":
                lblGuide = delete();
                break;
            case "add":
                lblGuide = add();
                break;
            case "search":
                lblGuide = search();
                break;
            default:
                lblGuide = search();
                break;
        }
        return lblGuide;
    }
    private string edit()
    {
        string lblGuide = "برای ویرایش یک رکورد،ابتدا آنرا جستجو کنید.سپس بر روی آیکون ویرایش کلیک کنید.";
        return lblGuide;
    }
    private string add()
    {
        string lblGuide = "برای افزودن رکورد جدید وارد کردن فیلدهای ستاره دار الزامی است.";
        return lblGuide;
    }
    private string delete()
    {
        string lblGuide = "برای حذف یک رکورد،ابتدا آنرا جستجو کنید.سپس بر روی آیکون حذف کلیک کنید.";
        return lblGuide;
    }
    private string search()
    {
        string lblGuide = "برای انجام عمل جستجو،وارد کردن حداقل یکی از فیلدهای جستجو الزامی است.";
        return lblGuide;
    }
}