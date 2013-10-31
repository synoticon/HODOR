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

            if (HodorRoleProvider.isSupportAllowed(user))
            {
                User.Visible = true;
            }

            if (HodorRoleProvider.isSupportAllowed(user))
                b_edit.Visible = true;
            if (Request.QueryString.Count > 0)
            {
                if (HodorRoleProvider.isSupportAllowed(user))
                {
                    Benutzer otherUser = BenutzerDAO.getUserByKundenNrOrNull(Request.QueryString["otherUserName"].ToString());
                    fillUserViewContentbyUser(otherUser);
                    SelectProgrammView(otherUser);
                    DownloadHistoryView(otherUser);
                }
            }
            else if (!IsPostBack)
            {
                fillUserViewContentbyUser(user);

                SelectProgrammView(BenutzerDAO.getUserByKundenNrOrNull(Username));
                DownloadHistoryView(BenutzerDAO.getUserByKundenNrOrNull(Username));
            }

            SelectUserView(BenutzerDAO.getUserByKundenNrOrNull(Username));
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
            if (HodorRoleProvider.isAdminAllowed(user))
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

        protected void DownloadHistoryView(Benutzer user)
        {

            foreach (Download_History item in BenutzerDAO.getDownloadHistoryEntriesForUser(user))
            {

                TableRow r = new TableRow();
                r.Cells.Add(createNewTableCell(item.Benutzer.NutzerNr.ToString()));
                r.Cells.Add(createNewTableCell(item.Build.Programm.Name.ToString()));
                
                Build build = BuildDAO.getBuildByIDOrNull(item.BuildID);
                if (build == null)
                {
                    r.Cells.Add(createNewTableCell("Deleted Build"));
                }
                r.Cells.Add(createNewTableCell(BuildDAO.getVersionStringForBuild(build)));

                r.Cells.Add(createNewTableCell(String.Format("{0:dd.MM.yyyy}", item.DownloadDatum)));
                Table1.Rows.Add(r);
            }

        }
        protected TableCell createNewTableCell(string cellContent)
        {
            TableCell c = new TableCell();
            c.Controls.Add(new LiteralControl(cellContent));
            return c;
        }
        protected void SelectProgrammView(Benutzer user)
        {
            if (HodorRoleProvider.isSupportAllowed(user))
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

        protected void b_edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administration/Verwaltung.aspx?view=ResultView&nutzernr=" + this.l_kundenNummer.Text);
        }
    }
}