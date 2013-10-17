using HODOR.src.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace HODOR.Members
{
    public partial class LandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
          String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
          Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);
          SelectUserView(user);
          if (!IsPostBack)
          {
                fillUserViewContentbyUser(user);
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

        protected void SelectUserView(Benutzer user)
        {
            if (user.Rolle == RolleDAO.getRoleByNameOrNull("Administrator") || user.Rolle == RolleDAO.getRoleByNameOrNull("Support") || user.Rolle == RolleDAO.getRoleByNameOrNull("Useradmin"))
            {
                foreach (Benutzer item in BenutzerDAO.getAllUsers().OrderBy(o => o.NutzerNr).ToList())
                {
                    listbox_user.Items.Add(new ListItem(item.NutzerNr));
                }
            }
        }
        protected void fillUserViewContentbyUser(Benutzer user)
        {
            if (user != null)
            {
                l_benutzer.Text = user.Name;
                l_kundenNummer.Text = user.NutzerNr;
                l_eMail.Text = user.Email;
                l_rolle.Text = user.Rolle.Rollenname;
            }
            else
            {
                l_benutzer.Text = "Benutzer nicht gefunden";
            }
        }

        protected void b_user_display_Click(object sender, EventArgs e)
        {
            
        }
    }
}