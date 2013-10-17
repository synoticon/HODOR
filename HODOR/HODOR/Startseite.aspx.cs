using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using HODOR.src.DAO;
namespace HODOR
{ 
    public partial class Startseite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MenuLink_Command(object sender, CommandEventArgs e)
        {
            string viewName = e.CommandName + "View";

            View newView = this.StartseiteMultiView.FindControl(viewName) as View;

            if (newView != null)
            {
                this.StartseiteMultiView.SetActiveView(newView);
            }
        }
        protected void OnClick_LoginButton(object sender, EventArgs e)
        {
          Benutzer user = BenutzerDAO.getUserMatchingKundenNrAndPasswordOrNull(UserName.Text, Password.Text);
          if (user != null)
          {
            GenericIdentity identity = new GenericIdentity(user.NutzerNr);
            RolePrincipal principal = new RolePrincipal(identity);
            System.Threading.Thread.CurrentPrincipal = principal;

            FormsAuthentication.SetAuthCookie(user.NutzerNr, false);
             
            Response.Redirect("Members/LandingPage.aspx");
          }
          else
          {
            FailureText.Text = "Login fehlgeschlagen.";
          }

        }

    }
}
