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
    }
}