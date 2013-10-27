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
            

           
            // Total number of rows.
            int rowCnt;
            // Current row count.
            int rowCtr;


            //Anzahl der Reports
            rowCnt = 10;

            for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
            {
                // Create new row and add it to the table.
                TableRow tRow = new TableRow();

                TableCell tCell = new TableCell();
                tCell.Text = "Ticketnummer";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Erstellt von";
                tRow.Cells.Add(tCell); 

                tCell = new TableCell();
                tCell.Text = "Programm";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Release";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Subrelease";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Build";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Beschreibung";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Status";
                tRow.Cells.Add(tCell);

                //if( stauts== offen)
                Button btn = new Button();
                btn.Text = "Besträtigen";
                btn.CommandName = "text";
                btn.ID = "Button" + rowCtr.ToString();
                btn.CommandArgument = "test";
                btn.Click += Button3_Click;
                tCell.Controls.Add(btn);
                tRow.Cells.Add(tCell);
                //elese tCell.text= Status

                Table1.Rows.Add(tRow);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
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
        protected void ticketview()
        {
            //AARON!!! ich will ein TicketDAO und eine Ticket Tabelle!

            /*  foreach (Ticket item in ticketDao.getAllTickets())
               {

                   TableRow r = new TableRow();
                   r.Cells.Add(createNewTableCell(item.Benutzer.NutzerNr.ToString()));
                   r.Cells.Add(createNewTableCell(item.Build.Programm.Name.ToString()));
                   r.Cells.Add(createNewTableCell(item.BuildID.ToString()));
                   r.Cells.Add(createNewTableCell(item.DownloadDatum.ToString()));
                   Table1.Rows.Add(r);
               }*/

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
    }
}