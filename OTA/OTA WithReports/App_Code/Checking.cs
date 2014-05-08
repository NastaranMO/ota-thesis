using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.EntityClient;

/// <summary>
/// Summary description for Checking
/// </summary>
public class Checking
{
    public string TableName;
    public string Data;
    public string CloumnName;
    public int Id;
    string PrimaryKey;


    public Checking()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Checking(string tableName, string data, string cloumnName)
    {
        this.TableName = tableName;
        this.Data = data.Replace("ی", "ي");
        this.CloumnName = cloumnName;
    }
    public Checking(string tableName, string data, string cloumnName, int id, string primaryKey)
    {
        this.TableName = tableName;
        this.Data = data.Replace("ی", "ي");
        this.CloumnName = cloumnName;
        this.Id = id;
        this.PrimaryKey = primaryKey;
    }
    public bool CheckDuplicateData()
    {
        bool Duplicate = false;
        
        SqlConnection cn = ADOConnection.GetAdoConnection();

        SqlDataAdapter da = new SqlDataAdapter("Select " + CloumnName + " from " + TableName, cn);

        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count!=0)
        {
            DataRow[] rows = dt.Select(CloumnName + " = " + "'" + Data+"'");
        

        if (rows.Count()>0)
        {
            Duplicate = true;
        }
        else
        {
            Duplicate = false;
        }
}

        return Duplicate;
    }
    public bool EditCheckDuplicateData()
    {
        bool Duplicate = false;

        SqlConnection cn = ADOConnection.GetAdoConnection();

        SqlDataAdapter da = new SqlDataAdapter("Select " + CloumnName + " from " + TableName + " where " + PrimaryKey +
            " !=" + Id, cn);

        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count != 0)
        {
            DataRow[] rows = dt.Select(CloumnName + " = " + "'" + Data + "'");


            if (rows.Count() > 0)
            {
                Duplicate = true;
            }
            else
            {
                Duplicate = false;
            }
        }

        return Duplicate;
    }
    //Check 2Column
    public static bool Check2Column(string tblName, string column1, string column2, int col1, int col2)
    {
        bool check = false;
        SqlConnection cn = ADOConnection.GetAdoConnection();

        SqlDataAdapter da = new SqlDataAdapter("Select " + column1+" ,"+column2 + " from " + tblName, cn);

        DataTable dt = new DataTable();
        da.Fill(dt);

        DataRow[] rows = dt.Select(column1 + " = " + col1 + " And "+column2+" = "+col2);
        if (rows.Count() > 0)
        {
            check = false;
        }
        else
        {
            check = true;
        }
        return check;

    }
}