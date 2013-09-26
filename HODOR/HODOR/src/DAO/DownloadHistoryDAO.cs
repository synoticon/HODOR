using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.Globals;

namespace HODOR.src.DAO
{
    public class DownloadHistoryDAO
    {
        public static Download_History createAndGetDownloadHistory(Benutzer downloadingUser, Build downloadedBuild, DateTime downloadDate)
        {
            Download_History dh = Download_History.CreateDownload_History(downloadingUser.BenutzerID, downloadedBuild.ReleaseID, downloadDate);

            HodorGlobals.getHodorContext().Download_History.AddObject(dh);
            HodorGlobals.save();

            return dh;
        }

        public static Download_History createAndGetDownloadHistory(Benutzer downloadingUser, Build downloadedBuild)
        {
            return createAndGetDownloadHistory(downloadingUser, downloadedBuild, DateTime.Now);
        }

        public static void deleteDownloadHistoryEntry(Download_History downloadHistoryEntry)
        {
            HODOR_entities db = HodorGlobals.getHodorContext();

            db.Download_History.DeleteObject(downloadHistoryEntry);

            HodorGlobals.save();
        }
    }
}