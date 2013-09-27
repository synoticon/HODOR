using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class ReleaseDAO
    {
        public static Release createAndGetRelease(Programm ofProgramm, DateTime dateAndTimeOfRelease, int releaseNumber)
        {
            Release rel = new Release(); //Release.CreateRelease(ofProgramm.ProgrammID, dateAndTimeOfRelease, releaseNumber);
            rel.ReleaseVonProgramm = ofProgramm.ProgrammID;
            rel.Releasedatum = dateAndTimeOfRelease;
            rel.Releasenummer = releaseNumber;

            HodorGlobals.getHodorContext().Releases.AddObject(rel);
            HodorGlobals.save();

            return rel;
        }

        public static Release createAndGetRelease(Programm ofProgramm, int releaseNumber)
        {
            return createAndGetRelease(ofProgramm, DateTime.Now, releaseNumber);
        }

        public static Release createAndGetRelease(Programm ofProgramm)
        {
            return createAndGetRelease(ofProgramm, getNextReleaseNumberFor(ofProgramm));
        }

        public static void deleteRelease(Release release)
        {
            List<Subrelease> subreleases = release.Subreleases.ToList<Subrelease>();

            foreach (Subrelease subrelease in subreleases)
            {
                SubreleaseDAO.deleteSubrelease(subrelease);
            }

            List<Lizenz> licenses = release.Lizenz_Versionsorientiert.ToList<Lizenz>();

            foreach (Lizenz license in licenses)
            {
                LizenzDAO.deleteLicense(license);
            }

            HodorGlobals.getHodorContext().Releases.DeleteObject(release);

            HodorGlobals.save();
        }

        public static Int32 getNextReleaseNumberFor(Programm prog)
        {
            //prog.Releases.Load();
            Int32 highestCurrentRelease;

            HODOR_entities db = HodorGlobals.getHodorContext();

            IQueryable<Release> queryResult = db.Releases.Except(db.Releases.OfType<Subrelease>()).Except(db.Releases.OfType<Build>())
                                                .Where(r => r.ReleaseVonProgramm == prog.ProgrammID);//Max(r => r.Releasenummer);
            if (queryResult.Count() == 0)
            {
                return 0;
            }
            else
            {
                highestCurrentRelease = queryResult.Max(r => r.Releasenummer);
            }

            Int32 nextReleaseNumber = highestCurrentRelease + 1;
            return nextReleaseNumber;
        }


        public static List<Release> getAllMajorReleases()
        {
            HODOR_entities ctx = HodorGlobals.getHodorContext();
            var allReleaseQueryResult = from r in ctx.Releases.OfType<Release>()
                                        where !(ctx.Releases.OfType<Subrelease>().Select(s => s.ReleaseID).Contains(r.ReleaseID)
                                            || ctx.Releases.OfType<Build>().Select(b => b.ReleaseID).Contains(r.ReleaseID))
                                        select r;

            return allReleaseQueryResult.ToList<Release>();
        }

        public static List<Release> getAllMajorReleasesFor(Programm prog)
        {
            return getAllMajorReleases().Where(p => p.ReleaseVonProgramm == prog.ProgrammID).ToList<Release>();
        }

    }
}