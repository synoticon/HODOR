using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class BenutzerDAO
    {
        private static object userCreationLock = new object();
        public static Benutzer createAndGetUser(String nutzer_Nr, String name, Int32 rolleID)
        {
            Benutzer user;
            //We need this Threadsafe. Otherwise other Threads may try to use the same ID
            lock (userCreationLock)
            {
                var highestID = HodorGlobals.getHodorContext().Benutzers.Max(u => u.BenutzerID);
                Int32 nextID = highestID + 1;
                user = Benutzer.CreateBenutzer(nextID, nutzer_Nr, name, rolleID);
            }
            return user;
        }

        public static Benutzer createAndGetUser(String nutzer_Nr, String name, String password, Int32 rolleID)
        {
            Benutzer user = createAndGetUser(nutzer_Nr, name, rolleID);
            setPasswordForUser(user, password);
            return user;
        }

        public static Benutzer createAndGetUser(String nutzer_Nr, String name, String password, String email, Int32 rolleID)
        {
            Benutzer user = createAndGetUser(nutzer_Nr, name, password, rolleID);
            user.Email = email;
            return user;
        }

        public static Benutzer getUserByKundenNrOrNull(String KundenNr) 
        {
            var queriedUser = from b in HodorGlobals.getHodorContext().Benutzers.Where(b => b.Nutzer_Nr == KundenNr) select b;
            Benutzer user = (Benutzer)queriedUser;

            return user;
        }

        public static void setPasswordForUser(Benutzer user, String Password)
        {
            String passwordHash = getSaltedMD5Hash(Password);
            user.PasswortHash = passwordHash;
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