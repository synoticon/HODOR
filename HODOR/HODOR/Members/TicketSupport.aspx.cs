using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;

namespace HODOR.Members.Administration
{
    public partial class TicketSupport : System.Web.UI.Page
    {
        protected List<String> searchContext = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);

            if (HodorRoleProvider.isSupportAllowed(user))
            {
                Ticket_Display.Visible = true;
            }

            SelectProgrammView(user);

            ticketview();
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btSender = (Button)sender;

                SupportTicket ticket = SupportTicketDAO.getSupportTicketByIdOrNull(Int32.Parse(btSender.CommandArgument));

                if (ticket == null)
                {
                    //whoops!? Ticket deleted?
                    return;
                }

                //hacky, but updates table properly
                setStatusOfTicketRowAsClosed(ticket.TicketID);

                ticket.IsOpen = false;
                HodorGlobals.save();
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

        protected void setStatusOfTicketRowAsClosed(Int32 ticketId)
        {
            //sry, hacky, but other solutions led to mixed results @author: Aaron
            TableRow row = findRowByIdOrNull(ticketId);
            row.Cells[8].Controls.Clear();
            row.Cells[8].Text = "Abgeschlossen";
        }

        protected TableRow findRowByIdOrNull(Int32 ticketId)
        {
            //sry, hacky as well @author: Aaron
            foreach (TableRow row in Table1.Rows)
            {
                if (!row.Cells[0].Text.Contains("Ticketnummer"))
                {
                    if (row.Cells[0].Controls.OfType<LiteralControl>().ToList<LiteralControl>()[0].Text == ticketId.ToString())
                    {
                        return row;
                    }
                }
            }
            return null;
        }

        protected void ticketview()
        {
            //AARON!!! ich will ein TicketDAO und eine Ticket Tabelle!
            //DAVID!!! hab ich jetzt gemacht :P

            foreach (SupportTicket item in SupportTicketDAO.getAllSupportTickets())
            {
                TableRow r = new TableRow();
                r.Cells.Add(createNewTableCell(item.TicketID.ToString()));

                if (item.Benutzer != null)
                {
                    r.Cells.Add(createNewTableCell(item.Benutzer.NutzerNr));
                }
                else
                {
                    r.Cells.Add(createNewTableCell("Gelöschter Benutzer"));
                }

                r.Cells.Add(createNewTableCell(String.Format("{0:dd.MM.yyyy}", item.EinreichungsDatum)));

                if (item.Programm != null)
                {
                    r.Cells.Add(createNewTableCell(item.Programm.Name));
                }
                else
                {
                    r.Cells.Add(createNewTableCell("Gelöschtes Programm"));
                }

                r.Cells.Add(createNewTableCell(item.ReleaseNummer.ToString()));
                r.Cells.Add(createNewTableCell(item.SubreleaseNummer.ToString()));
                r.Cells.Add(createNewTableCell(item.BuildNummer.ToString()));
                r.Cells.Add(createNewTableCell(item.Fallbeschreibung));

                if (item.IsOpen)
                {
                    TableCell tCell = new TableCell();
                    Button btn = new Button();
                    btn.Text = "Schließen";
                    btn.CommandName = "close";
                    btn.ID = "TicketCloseButton" + item.TicketID.ToString();
                    btn.CommandArgument = item.TicketID.ToString();
                    btn.Click += Button3_Click;
                    tCell.Controls.Add(btn);
                    r.Cells.Add(tCell);
                }
                else
                {
                    r.Cells.Add(createNewTableCell("Abgeschlossen"));
                }
                Table1.Rows.Add(r);
            }

        }

        protected TableCell createNewTableCell(string cellContent)
        {
            TableCell c = new TableCell();
            c.Controls.Add(new LiteralControl(cellContent));
            return c;
        }

        protected void SelectedChangeBuild(object sender, EventArgs e)
        {
           
        }
        protected void ClearAllDDL()
        {
            DDL_SubRelease.Items.Clear();
            DDL_SubRelease.Items.Add((new ListItem("---Select---", "null")));
            DDL_Build.Items.Clear();
            DDL_Build.Items.Add((new ListItem("---Select---", "null")));
            DDL_Release.Items.Clear();
            DDL_Release.Items.Add((new ListItem("---Select---", "null")));
         }

        protected void SelectedChangeRelease(object sender, EventArgs e)
        {
          DDL_SubRelease.Items.Clear();
          DDL_SubRelease.Items.Add((new ListItem("---Select---", "null")));
          DDL_Build.Items.Clear();
          DDL_Build.Items.Add((new ListItem("---Select---", "null")));
            if (DDL_Release.SelectedValue != "null")
            {
                foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                {
                    if (DDL_SubRelease.Items.FindByText(item.Releasenummer.ToString()) == null)
                    {
                        DDL_SubRelease.Items.Add((new ListItem(item.Releasenummer.ToString(), item.ReleaseID.ToString())));
                    }
                }
            }
        }

        protected void SelectedChangeSubRelease(object sender, EventArgs e)
        {
            DDL_Build.Items.Clear();
            DDL_Build.Items.Add((new ListItem("---Select---", "null")));
            if (DDL_SubRelease.SelectedValue != "null")
            {
                foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                {
                    if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
                    {
                        foreach (Build build in item.Builds)
                        {
                            if (DDL_Build.Items.FindByText(build.ReleaseID.ToString()) == null)
                            {
                                DDL_Build.Items.Add((new ListItem(build.Releasenummer.ToString(), build.ReleaseID.ToString())));
                            }
                        }
                    }
                }
            }
        }

        protected void SelectedChangeProgramm(object sender, EventArgs e)
        {
            ClearAllDDL();
            if (DDL_Programm.SelectedValue != "null")
            {
                foreach (Release item in BenutzerDAO.getAllReleasesOfProgrammLicensedForUser
                       (BenutzerDAO.getUserByKundenNrOrNull(System.Threading.Thread.CurrentPrincipal.Identity.Name),
                        ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(DDL_Programm.SelectedValue))).ToList())
                {
                    if (DDL_Release.Items.FindByText(item.Releasenummer.ToString()) == null)
                    {
                        DDL_Release.Items.Add(new ListItem(item.Releasenummer.ToString(), item.ReleaseID.ToString()));
                    }
                }
            }
        }

        protected void SelectProgrammView(Benutzer user)
        {
            
            if (HodorRoleProvider.isSupportAllowed(user))
            {
                foreach (Programm item in ProgrammDAO.getAllProgramme())
                {

                    if (DDL_Programm.Items.FindByText(item.Name) == null)
                    {
                        DDL_Programm.Items.Add(new ListItem(item.Name, item.ProgrammID.ToString()));
                    }
                }
            }
            else
            {

                foreach (Programm item in BenutzerDAO.getAllProgrammsLicensedForUser(user))
                {
                    if (DDL_Programm.Items.FindByText(item.Name) == null)
                    {
                        DDL_Programm.Items.Add(new ListItem(item.Name, item.ProgrammID.ToString()));
                    }
                }
            }
        }

        protected void OnClick_b_erstell(object sender, EventArgs e)
        {
          try
          {
            String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);
            SupportTicketDAO.createAndGetSupportTicket(user, ta_Fallbeispiel.Text, ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(DDL_Programm.SelectedValue)), int.Parse(DDL_Release.SelectedItem.Text), int.Parse(DDL_SubRelease.SelectedItem.Text), int.Parse(DDL_Build.SelectedItem.Text));
          }
          catch (Exception)
          {
            l_error.Text = "Es ist ein Feher bei der Ticketerstellung aufgetreten";
          }
        }


     }
}