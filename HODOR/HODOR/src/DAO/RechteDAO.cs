using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class RechteDAO
    {
        public static Rechte createAndGetRechte()
        {
            Rechte permissions = Rechte.CreateRechte();

            HodorGlobals.getHodorContext().Rechtes.AddObject(permissions);
            HodorGlobals.save();

            return permissions;
        }

        public static Rechte createAndGetRechte(String name)
        {
            Rechte permissions = createAndGetRechte();

            permissions.Name = name;

            HodorGlobals.save();

            return permissions;
        }

        public static void deletePermission(Rechte permission)
        {
            HodorGlobals.getHodorContext().Rechtes.DeleteObject(permission);
            HodorGlobals.save();
        }
    }
}