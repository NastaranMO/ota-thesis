using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTA_DBModel;
using System.Data;
using System.Data.EntityClient;
using System.Web.Profile;

public partial class _default : System.Web.UI.Page
{
    OTA_DBEntities db = new OTA_DBEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void imgEnter_Click(object sender, ImageClickEventArgs e)
    {
        if (txtPass.Text == "" || txtUserName.Text == "")
        {
            Response.RedirectPermanent("/");
            //CustomValidator1.ErrorMessage = "* نام کاربری و رمز عبور خود را وارد کنید. *";
        }
        else
        {
            //args.IsValid = true;
            int userName = Convert.ToInt32(txtUserName.Text);
            string pass = txtPass.Text;
            try
            {
                int number = (from p in db.Personals
                              where p.PersonalID == userName && p.Password == pass
                              select p.PersonalID).Count();
                if (number == 1)
                {
                    Profile.personelId = userName.ToString();
                    int al = (from p in db.Personals
                              where p.PersonalID == userName
                              select p.AId).Single();
                    RedirectToMianPage(al);
                }
                else
                {
                    int admin = (from a in db.Admins
                                 where a.AdminId == userName &&
                                 a.Pass == pass
                                 select a.AdminId).Count();
                    if (admin == 1)
                    {
                        //ProfileBase profile = ProfileBase.Create(userName.ToString());
                       // profile["personelId"] = userName.ToString();
                        RedirectToMianPage(3);
                    }
                    else
                    {
                        //args.IsValid = false;
                       // CustomValidator1.ErrorMessage = "* نام کاربری یا رمز عبور را اشتباه وارد کرده اید. *";
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {


    }
    protected void RedirectToMianPage(int accessLevel)
    {
        if (accessLevel == 1)
            Response.Redirect("Supervisors/inDirect-add.aspx");
        else
        {
            if (accessLevel == 2)
                Response.Redirect("User/add-direct.aspx");
            else
                Response.Redirect("Admin/code-amaliat.aspx");
        }
    }
}
