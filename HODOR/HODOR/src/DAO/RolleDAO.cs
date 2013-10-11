using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class RolleDAO
    {
        public static Rolle createAndGetRolle(String roleName)
        {
            Rolle role = new Rolle(); //Rolle.CreateRolle(roleName);
            role.Rollenname = roleName;

            HodorGlobals.getHodorContext().Rolles.AddObject(role);
            HodorGlobals.save();

            return role;
        }

        public static List<Rolle> getAllRoles()
        {
            return HodorGlobals.getHodorContext().Rolles.ToList<Rolle>();
        }

        public static Rolle getRoleByNameOrNull(String rolename)
        {
            List<Rolle> role = HodorGlobals.getHodorContext().Rolles.Where(r => r.Rollenname == rolename).ToList<Rolle>();

            if (role.Count > 1)
            {
                throw new Exception("Data inconsistency detected! More than one role with name: " + rolename);
            }
            if (role.Count == 0)
            {
                return null;
            }
            return role[0];
        }

        //implemented for HODOR.src.Globals.HodorRoleProvider
        public static List<Benutzer> findUsersInRole(Rolle role, string customernumberToMatch)
        {
            return getAllUsersWithRole(role).Where(u => u.NutzerNr.Contains(customernumberToMatch)).ToList<Benutzer>();
        }

        public static void deleteRole(Rolle role)
        {
            if (role.Benutzers.Count != 0)
            {
                throw new ArgumentException("There are still users connected to this Role.");
            }
            else
            {
                HodorGlobals.getHodorContext().Rolles.DeleteObject(role);
                HodorGlobals.save();
            }
        }

        public static List<Benutzer> getAllUsersWithRole(Rolle role)
        {
            return role.Benutzers.ToList<Benutzer>();
        }
    }
}