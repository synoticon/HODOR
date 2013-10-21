using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
namespace HODOR
{
    public partial class Produkte : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            lb_programmname.Text = Request.QueryString["progID"];

                foreach (Release item in BenutzerDAO.getAllReleasesOfProgrammLicensedForUser
                          (BenutzerDAO.getUserByKundenNrOrNull(System.Threading.Thread.CurrentPrincipal.Identity.Name),
                           ProgrammDAO.getProgrammByExactNameOrNull(Request.QueryString["progID"])).ToList())
                {
                    if (DDL_Release.Items.FindByText(item.Releasenummer.ToString()) == null)
                    {
                        DDL_Release.Items.Add(new ListItem(item.Releasenummer.ToString()));
                    }
                }
            
        }

        protected void SelectedChangeRelease(object sender, EventArgs e)
        {
        /*    foreach (Subrelease item in ReleaseDAO.)
            {
                if (DDL_Release.Items.FindByText(item.Releasenummer.ToString()) == null)
                {
                    DDL_Release.Items.Add(new ListItem(item.Releasenummer.ToString()));
                }
            }*/
        }
        protected void itemSelectedBuild(object sender, EventArgs e)
        {
            /*
            try
            {
                System.String filename = "myFile.txt";

                // set the http content type to "APPLICATION/OCTET-STREAM
                Response.ContentType = "APPLICATION/OCTET-STREAM";

                // initialize the http content-disposition header to
                // indicate a file attachment with the default filename
                // "myFile.txt"
                System.String disHeader = "Attachment; Filename=\"" + filename +
                   "\"";
                Response.AppendHeader("Content-Disposition", disHeader);

                // transfer the file byte-by-byte to the response object
                System.IO.FileInfo fileToDownload = new
                   System.IO.FileInfo("C:\\downloadJSP\\DownloadConv\\myFile.txt");
                Response.Flush();
                Response.WriteFile(fileToDownload.FullName);
            }
            catch (System.Exception exc)
            // file IO errors
            {
            }*/
        }
   
    }
}
