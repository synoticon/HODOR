using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class SubreleaseDAO
    {
        public static Subrelease createAndGetSubrelease(Release releaseOfSubrelease, DateTime releaseDate, Int32 releaseNumber)
        {
            releaseOfSubrelease.ProgrammReference.Load();
            Subrelease sub = Subrelease.CreateSubrelease(releaseOfSubrelease.ProgrammReference.Value.ProgrammID, releaseDate, releaseNumber, releaseOfSubrelease.ReleaseID);
            HodorGlobals.getHodorContext().Releases.AddObject(sub);
            HodorGlobals.save();

            return sub;
        }

        public static Subrelease createAndGetSubrelease(Release releaseOfSubrelease, Int32 releaseNumber)
        {
            Subrelease sub = createAndGetSubrelease(releaseOfSubrelease, DateTime.Now, releaseNumber);

            return sub;
        }

        public static Subrelease createAndGetSubrelease(Release releaseOfSubrelease)
        {
            return createAndGetSubrelease(releaseOfSubrelease, getNextSubreleaseNumberFor(releaseOfSubrelease));
        }

        public static void deleteSubrelease(Subrelease sub)
        {
            List<Build> builds = sub.Builds.ToList<Build>();

            foreach (Build build in builds)
            {
                BuildDAO.deleteBuild(build);
            }

            HodorGlobals.getHodorContext().Releases.DeleteObject(sub);

            HodorGlobals.save();
        }

        public static Int32 getNextSubreleaseNumberFor(Release rel)
        {
            //rel.Subreleases.Load();
            Int32 highestCurrentSubrelease;
            HODOR_entities db = HodorGlobals.getHodorContext();

            IQueryable<Release> queryResult = db.Releases.OfType<Subrelease>()
                                                .Where(s => s.SubreleaseVonRelease == rel.ReleaseID);
            if (queryResult.Count() == 0)
            {
                return 0;
            }
            else
            {
                highestCurrentSubrelease = queryResult.Max(r => r.Releasenummer);
            }

            Int32 nextSubreleaseNumber = highestCurrentSubrelease + 1;
            return nextSubreleaseNumber;
        }
    }
}