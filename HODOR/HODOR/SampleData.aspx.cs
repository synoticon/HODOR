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

            Rechte permission = RechteDAO.createAndGetRechte("MayViewUsers");
            permission.Beschreibung = "May view the all users page";

            Rolle role = RolleDAO.createAndGetRolle("Useradmin");
            role.Rechtes.Add(permission);

            Benutzer user = BenutzerDAO.createAndGetUser("Hodor!", "Hodor", new System.Net.Mail.MailAddress("hodor@hodor.com"), "Hodor123", role);

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
                Programm prog = ProgrammDAO.createAndGetProgramm("HODOR"+i);
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

                Rechte permission = RechteDAO.createAndGetRechte("MayViewUsers"+i);
                permission.Beschreibung = "May view the all users page"+i;

                Rolle role = RolleDAO.createAndGetRolle("Useradmin"+i);
                role.Rechtes.Add(permission);

                Benutzer user = BenutzerDAO.createAndGetUser("Hodor!"+i, "Hodor"+i, new System.Net.Mail.MailAddress("hodor"+i+"@hodor.com"), "Hodor123"+i, role);

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
    }
}