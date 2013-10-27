using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class BuildDAO
    {
        public static Build createAndGetBuild(Subrelease subreleaseOfBuild, DateTime buildDate, Int32 buildNumber)
        {
            if (subreleaseOfBuild.Builds.Select(b => b.Releasenummer).Contains(buildNumber))
            {
                //Build of this subrelease with that version already present!
                throw new ArgumentException("Program: " + subreleaseOfBuild.Programm.Name + " already has a build with releasenumber: "
                    + buildNumber + " for subrelease " + subreleaseOfBuild.Releasenummer 
                    + " and Release " + subreleaseOfBuild.Release.Releasenummer + "! Aborting creation.");
            }

            subreleaseOfBuild.ProgrammReference.Load();
            Int32 programmID = subreleaseOfBuild.ProgrammReference.Value.ProgrammID;
            Build build = new Build();
            build.Programm = subreleaseOfBuild.ProgrammReference.Value;
            build.Releasedatum = buildDate;
            build.Releasenummer = buildNumber;
            build.BuildVonSubrelease = subreleaseOfBuild.ReleaseID;

            HodorGlobals.getHodorContext().Releases.AddObject(build);
            HodorGlobals.save();

            return build;
        }

        public static Build createAndGetBuild(Subrelease subreleaseOfBuild, Int32 buildNumber)
        {
            return createAndGetBuild(subreleaseOfBuild, DateTime.Now, buildNumber);
        }

        public static Build createAndGetBuild(Subrelease subreleaseOfBuild, Int32 buildNumber, String pathToFile)
        {
            Build build = createAndGetBuild(subreleaseOfBuild, buildNumber);
            build.Datendateipfad = pathToFile;
            HodorGlobals.save();
            return build;
        }

        public static Build createAndGetBuild(Subrelease subreleaseOfBuild)
        {
            Int32 nextBuildNumber = getNextBuildNumberFor(subreleaseOfBuild);
            return createAndGetBuild(subreleaseOfBuild, nextBuildNumber);
        }

        public static void deleteBuild(Build build)
        {
            HODOR_entities db = HodorGlobals.getHodorContext();

            List<Download_History> historyEntries = build.Download_History.ToList<Download_History>();

            foreach (Download_History historyEntry in historyEntries)
            {
                db.Download_History.DeleteObject(historyEntry);
            }

            db.Releases.DeleteObject(build);

            HodorGlobals.save();
        }

        public static String getVersionStringForBuild(Build build)
        {
            StringBuilder sbVersionNumber = new StringBuilder();

            sbVersionNumber.Append(build.Subrelease.Release.Releasenummer);
            sbVersionNumber.Append(".");
            sbVersionNumber.Append(build.Subrelease.Releasenummer);
            sbVersionNumber.Append(".");
            sbVersionNumber.Append(build.Releasenummer);

            return sbVersionNumber.ToString();
        }

        public static Int32 getNextBuildNumberFor(Subrelease sub)
        {
            //sub.Builds.Load();

            Int32 highestCurrentBuild;
            HODOR_entities db = HodorGlobals.getHodorContext();

            IQueryable<Release> queryResult = db.Releases.OfType<Build>()
                                                            .Where(b => b.BuildVonSubrelease == sub.ReleaseID);
            if (queryResult.Count() == 0)
            {
                return 1;
            }
            else
            {
                highestCurrentBuild = queryResult.Max(r => r.Releasenummer);
            }

            Int32 nextBuildNumber = highestCurrentBuild + 1;
            return nextBuildNumber;
        }

        public static List<Int32> getAllBuildIDs()
        {
            List<Int32> result = HodorGlobals.getHodorContext().Releases.OfType<Build>().Select(s => s.ReleaseID).ToList<Int32>();

            return result;
        }

        public static Build getBuildByIDOrNull(Int32 buildId)
        {
            List<Build> resultList = HodorGlobals.getHodorContext().Releases.OfType<Build>().Where(b => b.ReleaseID == buildId).ToList<Build>();

            if (resultList.Count == 0)
            {
                //no Build with that ID found!
                return null;
            }
            if (resultList.Count >= 2)
            {
                throw new Exception("Data inconsistency detected! More than one Build with ReleaseID: " + buildId);
            }

            //safe to assume it's only one result:
            
            return resultList[0];
        }
    }
}