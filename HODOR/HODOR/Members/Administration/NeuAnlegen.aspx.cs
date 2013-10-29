using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;
using System.Net.Mail;

namespace HODOR.Members.Administration
{
    public partial class NeuAnlegen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Rolle> roleList = HodorGlobals.getHodorContext().Rolles.ToList<Rolle>();
                foreach (Rolle role in roleList)
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

        /*
         *  Methoden für die Benutzererstellung
         */

        protected void b_Register_Click(object sender, EventArgs e)
        {
            Rolle role = RolleDAO.getRoleByNameOrNull(this.ddl_roles.SelectedItem.Text);
            if (role == null)
            {
                //oh nooo, we're screwed! What now? Throw an Exception? Naah, shouldn't happen, because values were supplied by the DB. Just abort, if murphys law strikes.
                this.is_registered.Visible = false;
                return;
            }
            Benutzer user = BenutzerDAO.createAndGetUser(this.tb_KdNr.Text.Trim(), this.tb_Firmenname.Text.Trim(), new MailAddress(this.tb_EMail.Text.Trim()), this.tb_PW.Text, role);
            if (user == null)
            {
                this.is_registered.Visible = false;
            }
            this.Response.Redirect(this.Request.RawUrl);
        }



        /*
         *  Methoden für den Produkterstellung 
         */

        protected void OnClick_b_upload(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
            {
                try
                {

                    // transfer the file byte-by-byte to the response object
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                    FileUpload1.SaveAs(physicalPath + "\\LoadDirectory\\" +
                         FileUpload1.FileName);
                    l_message.Text = "File name: " +
                         FileUpload1.PostedFile.FileName + "<br>" +
                         FileUpload1.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         FileUpload1.PostedFile.ContentType;
                    string[] splitfilename = FileUpload1.FileName.Split('_');
                    string message = "";
                    //Überprüfe ob Programm vorhanden, wenn nicht erstellen
                    Programm newProgramm = ProgrammDAO.getProgrammByExactNameOrNull(splitfilename[0]);
                    if (newProgramm == null)
                    {
                        newProgramm = ProgrammDAO.createAndGetProgramm(splitfilename[0]);
                        message += "Neues Programm erstellt: " + newProgramm.Name;
                    }
                    //Überprüfe ob Release vorhanden, wenn nicht erstellen
                    Release newRelease = ReleaseDAO.getSingleReleaseByNumberAndProgramm(newProgramm.ProgrammID, int.Parse(splitfilename[1]));
                    if (newRelease == null)
                    {
                        newRelease = ReleaseDAO.createAndGetRelease(newProgramm, int.Parse(splitfilename[1]));
                        message += "Neues Release erstellt:" + newRelease.Releasenummer;
                    }
                    //Überprüfe ob Subrelease vorhanden, wenn nicht erstellen
                    Subrelease newSubRelease = null;
                    foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(newRelease.ReleaseID).Subreleases)
                    {
                        if (item.Releasenummer.ToString() == splitfilename[2])
                        {
                            newSubRelease = item;
                        }
                    }
                    if (newSubRelease == null)
                    {
                        newSubRelease = SubreleaseDAO.createAndGetSubrelease(newRelease, int.Parse(splitfilename[2]));
                        message += "Neues Subrelease erstellt:" + newSubRelease.Releasenummer;
                    }
                    //Überprüfe ob Build vorhanden, wenn nicht erstellen
                    Build newBuild = null;
                    foreach (Build item in newSubRelease.Builds)
                    {
                        if (item.Releasenummer.ToString() == splitfilename[3])
                        {
                            newBuild = item;
                        }
                    }
                    if (newBuild == null)
                    {
                        newBuild = BuildDAO.createAndGetBuild(newSubRelease, int.Parse(splitfilename[3]), FileUpload1.FileName);
                        message += "Neues Build erstellt:" + newBuild.Releasenummer;
                    }
                    l_message.Text = message;
                }
                catch (Exception ex)
                {
                    l_message.Text = "ERROR: " + ex.Message.ToString();
                }
            }
            else
            {
                l_message.Text = "You have not specified a file.";
            }

        }

        protected void ClearAllDDL()
        {
            DDL_SubRelease.Items.Clear();
            DDL_SubRelease.Items.Add((new ListItem("---Select---", "null")));
            DDL_Build.Items.Clear();
            DDL_Build.Items.Add((new ListItem("---Select---", "null")));
            DDL_Release.Items.Clear();
            DDL_Release.Items.Add((new ListItem("---Select---", "null")));
            ta_Builddiscription.Text = "";
            ta_Releasediscription.Text = "";
            ta_SubReleasediscription.Text = "";

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
                ta_Releasediscription.Text = ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Beschreibung;
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
                        ta_SubReleasediscription.Text = item.Beschreibung;
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

                // ta_Programmdiscription.Text = ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(DDL_Programm.SelectedValue)).Beschreibung;
            }
        }

        protected void SelectedChangeBuild(object sender, EventArgs e)
        {
            if (DDL_Build.SelectedValue != "null")
            {
                foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                {
                    if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
                    {

                        foreach (Build build in item.Builds)
                        {
                            if (build.ReleaseID.ToString() == DDL_Build.SelectedValue)
                            {
                                ta_Builddiscription.Text = build.Beschreibung;
                            }
                        }
                    }
                }
            }
        }


        protected void OnClick_b_save(object sender, EventArgs e)
        {
            if (ta_Programmdiscription != null)
            {
                //programm discription speichern
            }

            if (ta_Releasediscription != null)
            {
                if (DDL_Release.SelectedValue != "null")
                {
                    ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Beschreibung = ta_Releasediscription.Text;
                }

            }

            if (ta_SubReleasediscription != null)
            {
                if (DDL_SubRelease.SelectedValue != "null")
                {
                    foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                    {
                        if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
                        {
                            item.Beschreibung = ta_SubReleasediscription.Text;
                            break;
                        }
                    }
                }

            }

            if (ta_Builddiscription != null)
            {
                if (DDL_Build.SelectedValue != "null")
                {
                    foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                    {
                        if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
                        {

                            foreach (Build build in item.Builds)
                            {
                                if (build.ReleaseID.ToString() == DDL_Build.SelectedValue)
                                {
                                    build.Beschreibung = ta_Releasediscription.Text;
                                    break;
                                }
                            }
                        }
                    }


                }
                HodorGlobals.save();
            }
        }
    }
}