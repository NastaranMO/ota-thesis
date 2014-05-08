using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for utilClass
/// </summary>
public class utilClass
{
    public utilClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //For Add Days of Year
    public int dayId { get; set; }
    public string DsName { get; set; }    
    public string Rooz { get; set; }   
    public DateTime Tarikh { get; set;}

    //For Personal Search
    public int PersonalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShSh { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }

}