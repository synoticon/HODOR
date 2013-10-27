using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HODOR.src.Globals
{
    public class HodorGlobals
    {
    
            protected static HODOR_entities hodorContext;
            public static HODOR_entities getHodorContext()
            {
                if (hodorContext == null) hodorContext = new HODOR_entities();
                return hodorContext;
            }

            //Saves all additions, deletions and changes made to entities since last call of save()
            public static void save()
            {
                try
                {
                    getHodorContext().SaveChanges();
                }
                catch (Exception)
                {
                    //Something is wrong with our ObjectContext. Most possibly someone managed to get it into an invalid state, despite the meassures to prevent this.
                    //Another possibility would be that the DB-Server was lost, we would have to rebuild the context then anyway.
                    //Only real solution now is to rebuild the context, loosing all changes and additions since the last save.
                    //At least this saves us from restarting the application if this happens and buys some time to fix those issues, if this happen on a prod-system
                    hodorContext = new HODOR_entities();

                    //Exception should be re-thrown, to be able to track those issues
                    throw;
                }
            }
     }
}