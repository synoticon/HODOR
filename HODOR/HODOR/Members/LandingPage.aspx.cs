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

        protected void b_Register_Click(object sender, EventArgs e)
        {

        }

        protected void SelectUser()
        {
            List<Benutzer> sortedList = BenutzerDAO.getAllUsers().OrderBy(o => o.NutzerNr).ToList();
        }
    }
}