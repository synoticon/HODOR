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
                    this.ddl_roles.Items.Add(new ListItem(role.Rollenname));
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
            Rolle role = RolleDAO.getRoleByNameOrNull(this.ddl_roles.SelectedItem.Text);
            if (role == null)
            {
                //oh nooo, we're screwed! What now? Throw an Exception? Naah, shouldn't happen, because values were supplied by the DB. Just abort, if murphys law strikes.
                this.is_registered.Visible = false;
                return;
            }
            Benutzer user = BenutzerDAO.createAndGetUser(this.tb_KdNr.Text.Trim(), this.tb_Firmenname.Text.Trim(), new MailAddress(this.tb_EMail.Text.Trim()), role);
            if (user == null)
            {
                this.is_registered.Visible = false;
            }
            this.Response.Redirect(this.Request.RawUrl);
        }

        protected void b_DeleteUser_Click(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandName != null && BenutzerDAO.getUserByKundenNrOrNull(e.CommandName) != null)
                {
                    BenutzerDAO.deleteUser(BenutzerDAO.getUserByKundenNrOrNull(e.CommandName));
                }
            }
            else if (e.CommandName == "edit")
            {//Wird momentan noch umgebaut
//                EditIndex = e.Item.ItemIndex;
            }
            else if (e.CommandName == "save")
            {
                //          
            }

//            rpt.DataBind();
        }
    }
}
