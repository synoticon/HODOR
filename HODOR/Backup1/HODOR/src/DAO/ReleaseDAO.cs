using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class ReleaseDAO
    {
        public static List<Release> getAllReleases()
        {
            HODOR_entities ctx = HodorGlobals.getHodorContext();
            var releaseQueryResult = from r in ctx.Releases
                                     where !(ctx.Subreleases.Select(s => s.ReleaseID).Contains(r.ReleaseID))
                                            && !(ctx.Builds.Select(b => b.ReleaseID).Contains(r.ReleaseID))
                                     select r;

            return releaseQueryResult.ToList<Release>();
        }

        public static List<Release> getAllReleasesForProgramm(Programm prog)
        {
            HODOR_entities ctx = HodorGlobals.getHodorContext();
            var releaseQueryResult = from r in ctx.Releases
                                     where !(ctx.Subreleases.Select(s => s.ReleaseID).Contains(r.ReleaseID))
                                            && !(ctx.Builds.Select(b => b.ReleaseID).Contains(r.ReleaseID))
                                            && r.Release_Von_Programm == prog.ProgrammID
                                     select r;

            return releaseQueryResult.ToList<Release>();
        }
        
    }
}