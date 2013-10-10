using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using HODOR.src.Globals;
using System.Net.Mail;

namespace HODOR.src.DAO
{
    public class BenutzerDAO
    {
        public static Benutzer createAndGetUser(String nutzerNr, String name, MailAddress email, Rolle rolle)
        {
            Benutzer user = new Benutzer();
            user.NutzerNr = nutzerNr;
            user.Name = name;
            user.Email = email.Address;
            user.RolleID = rolle.RolleID;
            HodorGlobals.getHodorContext().Benutzers.AddObject(user);
            //HodorGlobals.save(); Other DAOs need this. Here it leads to errors
            return user;
        }

        public static Benutzer createAndGetUser(String nutzerNr, String name, MailAddress email, String password, Rolle rolle)
        {
            Benutzer user = createAndGetUser(nutzerNr, name, email, rolle);
            setPasswordForUser(user, password);
            return user;
        }

        public static void deleteUser(Benutzer user)
        {
            HODOR_entities db = HodorGlobals.getHodorContext();

            //remove referencing history entries
            List<Download_History> historyEntries = user.Download_History.ToList<Download_History>();
            foreach (Download_History historyEntry in historyEntries)
            {
                DownloadHistoryDAO.deleteDownloadHistoryEntry(historyEntry);
            }

            //remove referencing licenses
            List<Lizenz> licenses = user.Lizenzs.ToList<Lizenz>();
            foreach (Lizenz license in licenses)
            {
                LizenzDAO.deleteLicense(license);
            }

            db.Benutzers.DeleteObject(user);

            HodorGlobals.save();
        }

        public static Benutzer getUserByKundenNrOrNull(String KundenNr) 
        {
            var queriedUser = from b in HodorGlobals.getHodorContext().Benutzers.Where(b => b.NutzerNr == KundenNr) select b;
            
            List<Benutzer> userList = queriedUser.ToList<Benutzer>();

            if (userList.Count != 1)
            {
                return null;
            }
            else
            {
                return userList[0];
            }
        }

        public static void setPasswordForUser(Benutzer user, String Password)
        {
            String passwordHash = getSaltedMD5Hash(Password);
            user.PasswortHash = passwordHash;
            HodorGlobals.save();
        }

        public static List<Benutzer> getAllUsers()
        {
            var userQueryResults = from u in HodorGlobals.getHodorContext().Benutzers.Select(u => u) select u;
            List<Benutzer> users = userQueryResults.ToList<Benutzer>();
            return users;
        }

        public static Benutzer getUserMatchingKundenNrAndPasswordOrNull(String KundenNr, String Password)
        {
            //check validity of arguments
            if (KundenNr == null || Password == null || KundenNr.Length == 0 || Password.Length == 0) return null;

            Benutzer user = getUserByKundenNrOrNull(KundenNr);
            
            //no user with this name?
            if (user == null) return null;

            String passwordToCompare = getSaltedMD5Hash(Password);
            if (passwordToCompare.Equals(user.PasswortHash))
            {
                //a user was found and Password matches
                return user;
            }
            else
            {
                //a user was found, but password doesn't match
                return null;
            }
        }

        public static List<Download_History> getDownloadHistoryEntriesForUser(Benutzer user)
        {
            var downloadHistoryQueryResult = from d in HodorGlobals.getHodorContext().Download_History
                                             where d.BenutzerID == user.BenutzerID
                                             select d;

            return downloadHistoryQueryResult.ToList<Download_History>();
        }

        public static List<Programm> getAllProgrammsLicensedForUser(Benutzer user)
        {
            List<Programm> resultList = new List<Programm>();

            var idsOfProgrammsTimespanLicensedQueryResult = from lz in user.Lizenzs.OfType<Lizenz_Zeitlich>()
                                                            where lz.EndDatum <= DateTime.Now && lz.StartDatum >= DateTime.Now
                                                            select lz.LizensiertProgramm;

            var idsOfReleasesVersionLicensedQueryResult = from lv in user.Lizenzs.OfType<Lizenz_Versionsorientiert>()
                                                            select lv.LizensiertRelease;
            
            foreach (Int32 progId in idsOfProgrammsTimespanLicensedQueryResult.ToList<Int32>())
            {
                Programm prog = HodorGlobals.getHodorContext().Programms.Where(p => p.ProgrammID == progId).ToList<Programm>()[0];

                //only add if not already added (if multiple licenses license the same programm)
                if (!resultList.Contains(prog))
                {
                    resultList.Add(prog);
                }
            }

            foreach (Int32 releaseId in idsOfReleasesVersionLicensedQueryResult.ToList<Int32>())
            {
                Programm prog = HodorGlobals.getHodorContext().Releases.Where(r => r.ReleaseID == releaseId).ToList<Release>()[0].Programm;

                //only add if not already added (if multiple licenses license the same programm)
                if (!resultList.Contains(prog))
                {
                    resultList.Add(prog);
                }
            }
                                              
            return resultList;
        }

        public static List<Release> getAllReleasesOfProgrammLicensedForUser(Benutzer user, Programm prog)
        {
            //check if the user has a valid timespan license for this programm
            List<Lizenz_Zeitlich> licensesTimespan = user.Lizenzs.OfType<Lizenz_Zeitlich>().Where(
                lz => lz.LizensiertProgramm == prog.ProgrammID
                    && lz.StartDatum >= DateTime.Now
                    && lz.EndDatum <= DateTime.Now
                ).ToList<Lizenz_Zeitlich>();

            if (licensesTimespan.Count >= 1)
            {
                //that's ok, with a minimum of one valid TimespanLicense for this prog, he may see all releases of it
                return prog.Releases.ToList<Release>();
            }

            List<Lizenz_Versionsorientiert> licensesVersion = user.Lizenzs.OfType<Lizenz_Versionsorientiert>().Where(
                    lv => lv.Release.Programm.ProgrammID == prog.ProgrammID
                ).ToList<Lizenz_Versionsorientiert>();

            List<Release> releaseList = new List<Release>();

            foreach (Lizenz_Versionsorientiert license in licensesVersion)
            {
                releaseList.Add(license.Release);
            }

            return releaseList;
        }

        protected static String getSaltedMD5Hash(String source)
        {
            //some salt for our dear users security
            String toHash = source + "H0d0r";

            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(toHash));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}