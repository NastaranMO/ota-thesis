using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ADOConnection
/// </summary>
public class ADOConnection
{
	public ADOConnection()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static SqlConnection GetAdoConnection()
    {
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = "server=.; database= OTA_DB; Trusted_Connection=true;";
        return cn;
    }
}