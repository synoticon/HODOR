using HODOR.src.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HODOR.Members
{
    public partial class LandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          SelectUser();
          if (!IsPostBack)
          {
         //   Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Session["name"].ToString());
        //    fillUserViewContentbyUser(user);
          }
          else
          {
            /*Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Session["name"].ToString());
            if(user.Rolle == RolleDAO.getRoleByNameOrNull("Administrator") || user.Rolle == RolleDAO.getRoleByNameOrNull("Support") || user.Rolle == RolleDAO.getRoleByNameOrNull("Useradmin"))
            {
              Benutzer otherUser = BenutzerDAO.getUserByKundenNrOrNull(Request.QueryString["otherUser"]);
              fillUserViewContentbyUser(otherUser);
            }*/
          }
        }

        protected void MenuLink_Command(object sender, CommandEventArgs e)
        {
            string viewName = e.CommandName + "View";

            View newView = this.MultiView1.FindControl(viewName) as View;

            if (newView != null)
            {
                this.MultiView1.SetActiveView(newView);
            }
        }

        protected void SelectUser()
        {
           foreach(Benutzer item in BenutzerDAO.getAllUsers().OrderBy(o => o.NutzerNr).ToList())
           {
             listbox_user.Items.Add(new ListItem(item.NutzerNr));
           }
        }
        protected void fillUserViewContentbyUser(Benutzer user)
        {
            tb_benutzer.Text = user.Name;
            tb_kundenNummer.Text = user.NutzerNr;
            tb_eMail.Text = user.Email;
            tb_rolle.Text = user.Rolle.ToString();
        }

        protected void listbox_user_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}