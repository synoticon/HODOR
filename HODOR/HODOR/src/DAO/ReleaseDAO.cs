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
            if (getAllMajorReleasesFor(ofProgramm).Select(r => r.Releasenummer).Contains(releaseNumber))
            {
                //Release of this program with that version already present!
                throw new ArgumentException("Program: " + ofProgramm.Name + " already has a release with releasenumber: " + releaseNumber + "! Aborting creation.");
            }
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
                return getAllMajorReleases().Where(p => p.ReleaseVonProgramm == prog.ProgrammID).OrderBy(r => r.Releasenummer).ToList<Release>();
            }

        public static Release getSingleReleaseByID(int ReleaseID)
        {
   
            List<Release> releaseList = HodorGlobals.getHodorContext().Releases.Where(r => r.ReleaseID == ReleaseID).ToList<Release>();

            if (releaseList.Count == 1)
            {
                //everything ok
                return releaseList[0];
            }
            else if (releaseList.Count == 0)
            {
                //no Program with that name found
                return null;
            }
            else
            {
                throw new Exception("Entities for Release are inconsistent. Duplicate (" + releaseList.Count + ") ReleaseID detected: " + ReleaseID.ToString());
            }
        }

        public static Release getSingleReleaseByNumberAndProgramm(int ProgrammID,int ReleaseNumber)
        {

            List<Int32> allSubreleaseIDs = SubreleaseDAO.getAllSubreleaseIDs();
            List<Int32> allBuildIDs = BuildDAO.getAllBuildIDs();

            List<Release> releaseList = HodorGlobals.getHodorContext().Releases
                .Where(r => r.Releasenummer == ReleaseNumber 
                    && r.Programm.ProgrammID == ProgrammID 
                    && !allBuildIDs.Contains(r.ReleaseID)
                    && !allSubreleaseIDs.Contains(r.ReleaseID)).ToList<Release>();

            if (releaseList.Count == 1)
            {
                //everything ok
                return releaseList[0];
            }
            else if (releaseList.Count == 0)
            {
                //no Program with that ID found
                return null;
            }
            else
            {
                throw new Exception("Entities for Programm/Release are inconsistent. Duplicate (" + releaseList.Count + ") ReleaseNumber detected: ProgrammID " + ProgrammID.ToString() + " Releasenumber " + ReleaseNumber.ToString());
            }
        }
    }
}