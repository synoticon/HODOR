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

            public static void save()
            {
                getHodorContext().SaveChanges();
            }
     }
}