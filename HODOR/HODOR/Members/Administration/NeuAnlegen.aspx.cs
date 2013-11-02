using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;
using System.Net.Mail;
//using System.Text.RegularExpressions;

namespace HODOR.Members.Administration
{
    public partial class NeuAnlegen : System.Web.UI.Page
    {
        protected const string LICENSE_TIMESPAN = "Zeitlich";
        protected const string LICENSE_VERSION = "Major-Release"; //WAS: Majo-Release...

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String viewString = Request.QueryString["view"];
                String nutzerNrString = Request.QueryString["nutzernr"];
                if (viewString != null)
                {
                    if (viewString == "LizenzView")
                    {
                        MultiView1.SetActiveView(LizenzView);
                        string[] typ = new string[2];
                        typ[0] = LICENSE_TIMESPAN;
                        typ[1] = LICENSE_VERSION;
                        List<String> typList = new List<string>();
                        for (int i = 0; i < typ.Length; i++)
                        {
                            this.ddl_Typ.Items.Add(new ListItem(typ[i]));
                        }

                        fillDDLLicUser();
                        
                        //preselect user if parameter is set
                        if (nutzerNrString != null)
                        {
                            //Let's dance the index Limbo!
                            ListItem initialSelectedItem = ddl_licUser.Items.FindByText(nutzerNrString);
                            if (initialSelectedItem != null)
                            {
                                ddl_licUser.SelectedIndex = ddl_licUser.Items.IndexOf(initialSelectedItem);
                            }
                        }
                        this.l_KundenNr.Text = nutzerNrString;
                    }
                }
                String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);
                SelectProgrammView(user);
            }            
        }

        protected void MenuLink_Command(object sender, CommandEventArgs e)
        {
            string viewName = e.CommandName + "View";

            View newView = this.MultiView1.FindControl(viewName) as View;

            if (newView != null)
            {
                this.MultiView1.SetActiveView(newView);
                List<Rolle> roleList = HodorGlobals.getHodorContext().Rolles.ToList<Rolle>();
                foreach (Rolle role in roleList)
                {
                    this.ddl_roles.Items.Add(new ListItem(role.Rollenname));
                }
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
            Benutzer user = BenutzerDAO.createAndGetUser(this.tb_KdNr.Text.Trim(), this.tb_Firmenname.Text.Trim(), new MailAddress(this.tb_EMail.Text.Trim()), this.tb_PW.Text.Trim(), role);
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
                    string[] dotsplit = FileUpload1.FileName.Split('.');
                    string[] splitfilename = dotsplit[0].Split('_');
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
              ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(DDL_Programm.SelectedValue)).Beschreibung = ta_Programmdiscription.Text;
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

        protected void ddl_Typ_SelectedIndexChanged(object sender, EventArgs e)
        {
            l_ErstellungsErgebnis.Text = "";
            if (ddl_Typ.SelectedIndex != 0)
            {
                updateLicenseTypeSpecificColumns();
                List<Programm> progs = ProgrammDAO.getAllProgramme().OrderBy(p => p.Name).ToList<Programm>();
                foreach (Programm prog in progs)
                {
                    if (ddl_licProgramm.Items.FindByText(prog.Name) == null)
                    {
                        ddl_licProgramm.Items.Add(prog.Name);
                    }
                }
                tc_Program.Visible = true;
                thc_Program.Visible = true;

                fillDDLLicUser();
                tc_User.Visible = true;
                thc_User.Visible = true;
            }
            else
            {
                tc_Program.Visible = false;
                thc_Program.Visible = false;

                tc_User.Visible = false;
                thc_User.Visible = false;

                this.thc_sDatum.Visible = false;
                this.thc_eDatum.Visible = false;
                this.tc_sDatum.Visible = false;
                this.tc_eDatum.Visible = false;

                this.thc_MajorRelease.Visible = false;
                this.tc_MajorRelease.Visible = false;
            }
        }

        protected void fillDDLLicUser()
        {
            List<Benutzer> members = BenutzerDAO.getAllMembers().OrderBy(u => u.NutzerNr).ToList<Benutzer>();
            foreach (Benutzer user in members)
            {
                if (ddl_licUser.Items.FindByText(user.NutzerNr) == null)
                {
                    ddl_licUser.Items.Add(user.NutzerNr);
                }
            }
        }

        protected void updateLicenseTypeSpecificColumns()
        {
            if (ddl_Typ.SelectedItem.Text.Equals(LICENSE_VERSION) && (ddl_licProgramm.SelectedIndex != 0))
            {
                presentVersionLicenseTableColumns();
            }
            else if (ddl_Typ.SelectedItem.Text.Equals(LICENSE_TIMESPAN) && (ddl_licProgramm.SelectedIndex != 0))
            {
                presentTimespanLicenseTableColumns();
            }
        }

        protected void presentVersionLicenseTableColumns()
        {
            if (ddl_Typ.SelectedIndex!=0)
            {
                this.thc_sDatum.Visible = false;
                this.thc_eDatum.Visible = false;
                this.tc_sDatum.Visible = false;
                this.tc_eDatum.Visible = false;

                if (ddl_licProgramm.SelectedIndex != 0)
                {
                    String selectedProgName = ddl_licProgramm.SelectedItem.Text;
                    Programm selectedProg = ProgrammDAO.getProgrammByExactNameOrNull(selectedProgName);

                    ddl_MajorReleases.Items.Clear();
                    List<Release> releaseList = ReleaseDAO.getAllMajorReleasesFor(selectedProg);
                    foreach (Release release in releaseList)
                    {
                        if (ddl_MajorReleases.Items.FindByText(release.Releasenummer.ToString()) == null)
                        {
                            this.ddl_MajorReleases.Items.Add(new ListItem(release.Releasenummer.ToString()));
                        }
                    }
                }

                this.thc_MajorRelease.Visible = true;
                this.tc_MajorRelease.Visible = true;
            }
        }
        protected void presentTimespanLicenseTableColumns()
        {
            if (ddl_Typ.SelectedIndex!=0)
            {
                this.thc_MajorRelease.Visible = false;
                this.tc_MajorRelease.Visible = false;

                this.thc_sDatum.Visible = true;
                this.thc_eDatum.Visible = true;
                this.tc_sDatum.Visible = true;
                this.tc_eDatum.Visible = true;

                tb_StartDatum.Text = DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year;
                tb_EndDatum.Text = DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year;
            }
        }

        protected void ddl_Programm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lizenzTypString = this.ddl_Typ.SelectedIndex;
            l_ErstellungsErgebnis.Text = "";
            updateLicenseTypeSpecificColumns();

            //habe es auf int umgestellt, der nie null ist. war vorher string, der auch nie null ist... deswegen prüfen wir mal lieber auf die zahl 0
            if (lizenzTypString != 0)
            {
                this.tr_button.Visible = true;

                
                if (lizenzTypString == 1)
                {
                    presentTimespanLicenseTableColumns();
                }
                else if (lizenzTypString == 2)
                {
                    presentVersionLicenseTableColumns();
                }
            }
        }

        protected void b_LizenzErstellen_Click(object sender, EventArgs e)
        {
            int lizenzTyp = this.ddl_Typ.SelectedIndex;
            string majorRelease = this.ddl_MajorReleases.SelectedIndex.ToString();
            try
            {
                if ((lizenzTyp != 0) && (ddl_licUser.SelectedIndex != 0) && (ddl_licProgramm.SelectedIndex != 0))
                {
                    Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(ddl_licUser.SelectedItem.Text);
                    string progName = ddl_licProgramm.SelectedItem.Text;

                    if (lizenzTyp == 1)
                    {
                        Programm prog = ProgrammDAO.getProgrammByExactNameOrNull(progName);

                        //string pattern = @"(?<!\d)(?=\d{1,2}\.\d{1,2}\.\d{4})";
                        //string[] dateParts = Regex.Split(tb_EndDatum.Text, pattern);
                        string[] datePartsEnd = tb_EndDatum.Text.Split('.');
                        DateTime endDate = new DateTime(Int32.Parse(datePartsEnd[2]), Int32.Parse(datePartsEnd[1]), Int32.Parse(datePartsEnd[0]));

                        string[] datePartsStart = tb_EndDatum.Text.Split('.');
                        DateTime startDate = new DateTime(Int32.Parse(datePartsStart[2]), Int32.Parse(datePartsStart[1]), Int32.Parse(datePartsStart[0]));

                        Lizenz lizenz = LizenzDAO.createAndGetZeitlizenzForUser(user, prog, startDate, endDate);
                        l_ErstellungsErgebnis.Text = "Die Lizenz wurde erstellt.";
                    }
                    else if (lizenzTyp == 2)
                    {
                        Int32 progId = ProgrammDAO.getProgrammByExactNameOrNull(progName).ProgrammID;
                        Release release = ReleaseDAO.getSingleReleaseByNumberAndProgramm(progId, Int32.Parse(ddl_MajorReleases.SelectedItem.Text));
                        Lizenz lizenz = LizenzDAO.createAndGetVersionslizenzForUser(user, release);
                        l_ErstellungsErgebnis.Text = "Die Lizenz wurde erstellt.";
                    }
                }
            }
            catch (Exception)
            {
                l_ErstellungsErgebnis.Text = "Fehler beim Erstellen der Lizenz, bitte überprüfen Sie Ihre Eingaben.";
            }
        }
    }
}