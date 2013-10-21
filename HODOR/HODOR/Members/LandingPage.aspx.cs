using HODOR.src.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HODOR.src.Globals;
namespace HODOR.Members
{
    public partial class LandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
          String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
          Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);
          if(Request.QueryString.Count > 0)
          {
            if(user.Rolle == RolleDAO.getRoleByNameOrNull("Administrator") || user.Rolle == RolleDAO.getRoleByNameOrNull("Support") || user.Rolle == RolleDAO.getRoleByNameOrNull("Useradmin"))
            {
              Benutzer otherUser = BenutzerDAO.getUserByKundenNrOrNull(Request.QueryString["otherUserName"].ToString());      
              fillUserViewContentbyUser(otherUser);
            }
          }
          else if (!IsPostBack)
          { 
                fillUserViewContentbyUser(user);
          }
         
              SelectUserView(BenutzerDAO.getUserByKundenNrOrNull(Username));
              SelectProgrammView(BenutzerDAO.getUserByKundenNrOrNull(Username));
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
            if(HodorRoleProvider.isAdminAllowed(user))
            {              
      
                foreach (Benutzer item in BenutzerDAO.getAllUsers().OrderBy(o => o.NutzerNr).ToList())
                {
                    if (listbox_user.Items.FindByText(item.NutzerNr) == null)
                    {
                        listbox_user.Items.Add(new ListItem(item.NutzerNr));
                    }
                }
            }
        }

        protected void SelectProgrammView(Benutzer user)
        {
            if (HodorRoleProvider.isAdminAllowed(user))
            {
                foreach (Programm item in ProgrammDAO.getAllProgramme())
                {
                    String idName = item.ProgrammID.ToString() + ":" + item.Name.ToString();
                    if (listbox_prog.Items.FindByText(idName) == null)
                    {
                        listbox_prog.Items.Add(new ListItem(idName));
                    }
                }
            }
            else
            {
                foreach (Programm item in BenutzerDAO.getAllProgrammsLicensedForUser(user))
                {
                    String idName = item.ProgrammID.ToString() + ":" + item.Name.ToString();
                    if (listbox_prog.Items.FindByText(idName) == null)
                    {
                        listbox_prog.Items.Add(new ListItem(idName));
                    }
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

        protected void OnClick_b_user_display(object sender, EventArgs e)
        {
            Response.Redirect("LandingPage.aspx?otherUserName=" + Server.UrlEncode(listbox_user.SelectedValue));
        }

        protected void OnClick_b_prog_display(object sender, EventArgs e)
        {
            String[] split = listbox_prog.SelectedValue.Split(':');
            Response.Redirect("Produkte.aspx?progID=" + Server.UrlEncode(split[0]));
        }
    }
}