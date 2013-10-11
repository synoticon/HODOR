using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HODOR.src.DAO;
using System.Net.Mail;
using HODOR.src.Globals;

namespace HODOR.Members.Administration
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Rolle> roleList = HodorGlobals.getHodorContext().Rolles.ToList<Rolle>();
                foreach(Rolle role in roleList)
                {
                    this.ddl_roles.Attributes.Add(role.RolleID.ToString(), role.Rollenname);
                }
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

        protected void b_Register_Click(object sender, EventArgs e)
        {
            BenutzerDAO.createAndGetUser(this.tb_KdNr.ToString(), this.tb_Firmenname.ToString(), new MailAddress(this.tb_EMail.ToString()), this.ddl_roles.SelectedItem.ToString());
        }

        protected void SelectUser()
        {
            List<Benutzer> sortedList = BenutzerDAO.getAllUsers().OrderBy(o => o.NutzerNr).ToList();
        }
    }
}
