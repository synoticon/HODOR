using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class LizenzDAO
    {
        public static Lizenz_Zeitlich createAndGetZeitlizenz(Programm licensedProgramm, DateTime endDate)
        {
            Lizenz_Zeitlich license = new Lizenz_Zeitlich();//Lizenz_Zeitlich.CreateLizenz_Zeitlich(licensedProgramm.ProgrammID, startDate, endDate);
            license.LizensiertProgramm = licensedProgramm.ProgrammID;
            license.StartDatum = DateTime.Now;
            license.EndDatum = endDate;

            HodorGlobals.getHodorContext().Lizenzs.AddObject(license);
            HodorGlobals.save();
            return license;
        }

        public static Lizenz_Zeitlich createAndGetZeitlizenzForUser(Benutzer user, Programm licensedProgramm, DateTime endDate)
        {
            Lizenz_Zeitlich license =createAndGetZeitlizenz(licensedProgramm, endDate);
            BenutzerDAO.addLicenseToUser(user, license);
            return license;
        }

        public static Lizenz_Versionsorientiert createAndGetVersionslizenz(Release licensedRelease, Int32 versionIncremention)
        {
            Lizenz_Versionsorientiert license = new Lizenz_Versionsorientiert();//Lizenz_Versionsorientiert.CreateLizenz_Versionsorientiert(versionIncremention, licensedRelease.ReleaseID);
            license.Versionserhöhung = versionIncremention;
            license.LizensiertRelease = licensedRelease.ReleaseID;

            HodorGlobals.getHodorContext().Lizenzs.AddObject(license);
            HodorGlobals.save();

            return license;
        }

        public static Lizenz_Versionsorientiert createAndGetVersionslizenzForUser(Benutzer user, Release licensedRelease, Int32 versionIncremention)
        {
            Lizenz_Versionsorientiert license = createAndGetVersionslizenz(licensedRelease, versionIncremention);
            BenutzerDAO.addLicenseToUser(user, license);
            return license;
        }

        public static void deleteLicense(Lizenz license)
        {
            if (license is Lizenz_Versionsorientiert)
            {
                Lizenz_Versionsorientiert versionLicense = (Lizenz_Versionsorientiert)license;
                deleteVersionLicense(versionLicense);
            } else if (license is Lizenz_Zeitlich) {
                Lizenz_Zeitlich timespanLicense = (Lizenz_Zeitlich)license;
                deleteTimespanLicense(timespanLicense);
            } else {
                throw new ArgumentException("Unknown type of License: "+license.LizenzID);
            }

        }

        public static void deleteVersionLicense(Lizenz_Versionsorientiert versionLicense)
        {
            HODOR_entities db = HodorGlobals.getHodorContext();

            db.Lizenzs.DeleteObject(versionLicense);

            HodorGlobals.save();
        }

        public static void deleteTimespanLicense(Lizenz_Zeitlich timespanLicense)
        {
            HODOR_entities db = HodorGlobals.getHodorContext();

            db.Lizenzs.DeleteObject(timespanLicense);

            HodorGlobals.save();
        }
    }
}