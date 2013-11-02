using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class LizenzDAO
    {
        public static Lizenz_Zeitlich createAndGetZeitlizenz(Programm prog, DateTime startDate, DateTime endDate)
        {
            Lizenz_Zeitlich license = new Lizenz_Zeitlich();
            license.Programm = prog;
            license.StartDatum = startDate;
            license.EndDatum = endDate;

            HodorGlobals.getHodorContext().Lizenzs.AddObject(license);
            HodorGlobals.save();
            return license;
        }

        public static Lizenz_Zeitlich createAndGetZeitlizenz(Programm prog, DateTime endDate)
        {
            return createAndGetZeitlizenz(prog, DateTime.Now, endDate);
        }

        public static Lizenz_Zeitlich createAndGetZeitlizenzForUser(Benutzer user, Programm prog, DateTime startDate, DateTime endDate)
        {
            Lizenz_Zeitlich license = createAndGetZeitlizenz(prog, startDate, endDate);
            BenutzerDAO.addLicenseToUser(user, license);
            return license;
        }

        public static Lizenz_Zeitlich createAndGetZeitlizenzForUser(Benutzer user, Programm prog, DateTime endDate)
        {
            return createAndGetZeitlizenzForUser(user, prog, DateTime.Now, endDate);
        }

        public static Lizenz_Versionsorientiert createAndGetVersionslizenz(Benutzer userOrNull, Release licensedRelease, Int32 versionIncremention)
        {
            Lizenz_Versionsorientiert license = new Lizenz_Versionsorientiert();//Lizenz_Versionsorientiert.CreateLizenz_Versionsorientiert(versionIncremention, licensedRelease.ReleaseID);
            license.Release = licensedRelease;
            //legacy for a stalled feature:
            license.Versionserhöhung = versionIncremention;
            if (userOrNull != null)
            {
                license.Benutzers.Add(userOrNull);
            }

            HodorGlobals.getHodorContext().Lizenzs.AddObject(license);
            HodorGlobals.save();

            return license;
        }

        public static Lizenz_Versionsorientiert createAndGetVersionslizenz(Release licensedRelease)
        {
            return createAndGetVersionslizenz(null, licensedRelease, 0);
        }

        public static Lizenz_Versionsorientiert createAndGetVersionslizenzForUser(Benutzer user, Release licensedRelease)
        {
            Lizenz_Versionsorientiert license = createAndGetVersionslizenz(user, licensedRelease, 0);
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