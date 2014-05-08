using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTA_DBModel;
using System.Data.EntityClient;
using System.Collections;


/// <summary>
/// Summary description for bindClass
/// </summary>
public class bindClass
{
	public bindClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void bindGrid(GridView gv, IEnumerable dsName)
    {
        gv.DataSource = dsName;
        gv.DataBind();
    }
    public static void bindDropDownList(DropDownList dl, IEnumerable dsName,string Text,string Val)
    {
        dl.DataSource = dsName;
        dl.DataTextField = Text;
        dl.DataValueField = Val;
        dl.DataBind();
    }
    public static void bindCheckBoxList(CheckBoxList chk, IEnumerable dsName, string Text, string Val)
    {
        chk.DataSource = dsName;
        chk.DataTextField = Text;
        chk.DataValueField = Val;
        chk.DataBind();
    }
}