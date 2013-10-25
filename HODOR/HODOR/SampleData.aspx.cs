using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;

namespace HODOR
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonClick(Object sender, System.EventArgs e)
        {
            HODOR_entities db = HodorGlobals.getHodorContext();
            Programm prog = ProgrammDAO.createAndGetProgramm("HODOR HODOR");
            Release release = ReleaseDAO.createAndGetRelease(prog);
            Subrelease subrelease = SubreleaseDAO.createAndGetSubrelease(release);
            Build build = BuildDAO.createAndGetBuild(subrelease);

            //Rechte permission = RechteDAO.createAndGetRechte("MayViewUsers");
            //permission.Beschreibung = "May view the all users page";

            //Rolle role = RolleDAO.createAndGetRolle("Useradmin");
            //role.Rechtes.Add(permission);

            Benutzer user = BenutzerDAO.createAndGetUser("Hodor!", "Hodor", new System.Net.Mail.MailAddress("hodor@hodor.com"), "Hodor123", RolleDAO.getRoleByNameOrNull("Administrator"));

            DownloadHistoryDAO.createAndGetDownloadHistory(user, build);

            Lizenz_Versionsorientiert versionLicense = LizenzDAO.createAndGetVersionslizenz(release, 1);
            user.Lizenzs.Add(versionLicense);

            Lizenz_Zeitlich timedLicense = LizenzDAO.createAndGetZeitlizenz(prog, DateTime.Now.AddYears(1));
            user.Lizenzs.Add(timedLicense);

            HodorGlobals.save();
            label.Text = "done";
        }

        protected void buttonClick2(Object sender, System.EventArgs e)
        {
            DateTime start;
            start = DateTime.Now;
            HODOR_entities db = HodorGlobals.getHodorContext();
            for (int i = 1; i <= 5; i++)
            {
                Programm prog = ProgrammDAO.createAndGetProgramm("HODOR" + i);
                Release release1 = ReleaseDAO.createAndGetRelease(prog);
                Release release2 = ReleaseDAO.createAndGetRelease(prog);
                Subrelease subrelease1_1 = SubreleaseDAO.createAndGetSubrelease(release1);
                Subrelease subrelease1_2 = SubreleaseDAO.createAndGetSubrelease(release1);
                Subrelease subrelease2_1 = SubreleaseDAO.createAndGetSubrelease(release2);
                Subrelease subrelease2_2 = SubreleaseDAO.createAndGetSubrelease(release2);
                Build build1 = BuildDAO.createAndGetBuild(subrelease1_1);
                Build build2 = BuildDAO.createAndGetBuild(subrelease1_1);
                Build build3 = BuildDAO.createAndGetBuild(subrelease1_2);
                Build build4 = BuildDAO.createAndGetBuild(subrelease2_1);
                Build build5 = BuildDAO.createAndGetBuild(subrelease2_1);
                Build build6 = BuildDAO.createAndGetBuild(subrelease2_2);

                //Rechte permission = RechteDAO.createAndGetRechte("MayViewUsers"+i);
                //permission.Beschreibung = "May view the all users page"+i;

                Rolle role = RolleDAO.createAndGetRolle("Useradmin" + i);
                //role.Rechtes.Add(permission);

                Benutzer user = BenutzerDAO.createAndGetUser("Hodor!" + i, "Hodor" + i, new System.Net.Mail.MailAddress("hodor" + i + "@hodor.com"), "Hodor123" + i, role);

                DownloadHistoryDAO.createAndGetDownloadHistory(user, build1);

                Lizenz_Versionsorientiert versionLicense = LizenzDAO.createAndGetVersionslizenz(release1, 1);
                user.Lizenzs.Add(versionLicense);

                Lizenz_Zeitlich timedLicense = LizenzDAO.createAndGetZeitlizenz(prog, DateTime.Now.AddYears(1));
                user.Lizenzs.Add(timedLicense);

                HodorGlobals.save();
            }

            label.Text = DateTime.Now.Subtract(start).TotalMinutes.ToString();
        }

        protected void buttonClick3(Object sender, System.EventArgs e)
        {
            //Benutzer user = BenutzerDAO.getUserMatchingKundenNrAndPasswordOrNull("Hodor!1", "Hodor1231"); //works

            //Benutzer user = HodorGlobals.getHodorContext().Benutzers.ToList<Benutzer>()[0];
            //BenutzerDAO.deleteUser(user); //works

            Programm prog = HodorGlobals.getHodorContext().Programms.ToList<Programm>()[0];


            label.Text = ReleaseDAO.getNextReleaseNumberFor(prog).ToString(); ;
        }

        protected void createDemoDataWithInput(Object sender, System.EventArgs e)
        {
            if (checkValidityOfArguments())
            {
                DateTime start;
                start = DateTime.Now;
                int countOfCreatedEntitys = 0;
                int countOfDatabaseTableRows = 0;
                Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(tb_NameOfUserWithLicenseForProgram.Text);
                if (user == null)
                {
                    //user didn't exist, create him
                    user = BenutzerDAO.createAndGetUser(tb_NameOfUserWithLicenseForProgram.Text, tb_NameOfUserWithLicenseForProgram.Text, new System.Net.Mail.MailAddress(tb_NameOfUserWithLicenseForProgram.Text + "@hodor.com"), RolleDAO.getRoleByNameOrNull("Member"));
                    countOfCreatedEntitys++;
                    countOfDatabaseTableRows++;
                    BenutzerDAO.setPasswordForUser(user, tb_NameOfUserWithLicenseForProgram.Text);
                }

                Programm prog = ProgrammDAO.createAndGetProgramm(tb_ProgramName.Text);
                countOfCreatedEntitys++;
                countOfDatabaseTableRows++;

                for (int i = 1; i <= Int32.Parse(tb_NumberOfReleases.Text); i++)
                {
                    Release rel = ReleaseDAO.createAndGetRelease(prog);
                    rel.Beschreibung = "Release " + i + " of " + prog.Name;
                    countOfCreatedEntitys++;
                    countOfDatabaseTableRows++;
                    for (int k = 1; k <= Int32.Parse(tb_NumberOfSubreleasesPerRelease.Text); k++)
                    {
                        Subrelease sub = SubreleaseDAO.createAndGetSubrelease(rel);
                        sub.Beschreibung = "Subrelease " + k + " of Release " + i + " of " + prog.Name;
                        countOfCreatedEntitys++;
                        countOfDatabaseTableRows += 2;
                        for (int j = 1; j <= Int32.Parse(tb_NumberOfBuildsPerSubrelease.Text); j++)
                        {
                            Build build = BuildDAO.createAndGetBuild(sub);
                            build.Beschreibung ="Build " + j + " of Subrelease " + k + " of Release " + i + " of " + prog.Name;
                            countOfCreatedEntitys++;
                            countOfDatabaseTableRows += 2;

                            if (j % 2 == 1)
                            {
                                DownloadHistoryDAO.createAndGetDownloadHistory(user, build);
                                countOfCreatedEntitys++;
                                countOfDatabaseTableRows++;
                            }

                            if (j == 1)
                            {
                                SupportTicket ticket = SupportTicketDAO.createAndGetSupportTicket(user, "Son ding das Grün sein sollte ist Rot irgendwo!", prog, rel.Releasenummer, sub.Releasenummer, build.Releasenummer);
                                ticket.IsOpen = false;
                                HodorGlobals.save();
                                countOfCreatedEntitys++;
                                countOfDatabaseTableRows++;
                            }

                            if (j == Int32.Parse(tb_NumberOfBuildsPerSubrelease.Text))
                            {
                                SupportTicket ticket = SupportTicketDAO.createAndGetSupportTicket(user, "Ich habe in der Suche 'Autoschlüssel' eingegeben, doch obwohl er genau vor mir lag, hat die Suche ihn nicht gefunden!", prog, rel.Releasenummer, sub.Releasenummer, build.Releasenummer);
                                countOfCreatedEntitys++;
                                countOfDatabaseTableRows++;
                            }
                        }
                    }
                }

                Lizenz_Zeitlich lic = LizenzDAO.createAndGetZeitlizenz(prog, DateTime.Now.AddYears(2));
                countOfCreatedEntitys++;
                countOfDatabaseTableRows += 2;

                user.Lizenzs.Add(lic);

                HodorGlobals.save();


                label.Text = "Demodata created in " + DateTime.Now.Subtract(start).TotalMilliseconds.ToString() + " ms. A total amount of " + countOfCreatedEntitys + " entitys were created and " + countOfDatabaseTableRows + " DB-table rows were inserted.";
            }
        }

        private Boolean checkValidityOfArguments()
        {
            if (String.IsNullOrEmpty(tb_ProgramName.Text) || (ProgrammDAO.getProgrammByExactNameOrNull(tb_ProgramName.Text) != null))
            {
                label.Text = "No Program Name entered or Program with that name already exists!";
                return false;
            }

            if ((!hasCharsAndIsIntegerParseable(tb_NumberOfReleases.Text, "Number of releases not set or not a number"))
                || (!hasCharsAndIsIntegerParseable(tb_NumberOfSubreleasesPerRelease.Text, "Number of subreleases not set or not a number"))
                || (!hasCharsAndIsIntegerParseable(tb_NumberOfBuildsPerSubrelease.Text, "Number of builds not set or not a number"))
                )
            {
                return false;
            }

            if (String.IsNullOrEmpty(tb_NameOfUserWithLicenseForProgram.Text))
            {
                label.Text = "Name of user not set!";
                return false;
            }

            return true;
        }

        private Boolean hasCharsAndIsIntegerParseable(String input, String outputIfNot)
        {
            if (!String.IsNullOrEmpty(input))
            {
                try
                {
                    Int32.Parse(input);
                }
                catch (Exception)
                {
                    label.Text = outputIfNot;
                    return false;
                }

                return true;
            }

            label.Text = outputIfNot;
            return false;
        }

    }
}