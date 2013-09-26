using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HODOR
{
    public partial class Produkte : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            lb_programmname.Text = Request.QueryString["Name"];

            if (!IsPostBack)
            {
                foreach (ListItem item in getReleaseListItem(Request.QueryString["Name"]))
                {
                    DDL_Release.Items.Add(item);
                }

                List<Test> list = new List<Test>()
                {
                    new Test()
                    {
                        Title = "Test 1";
                    }
                };
            }
        }

        protected void SelectedChange(object sender, EventArgs e)
        {
            foreach (ListItem item in getBuildListItem(Request.QueryString["Name"], DDL_Release.SelectedItem.ToString()))
            {
                DDL_Build.Items.Add(item);
            }
        }
        protected void itemSelectedBuild(object sender, EventArgs e)
        {
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
            }
        }
        protected List<ListItem> getReleaseListItem(string programm)
        {
            List<ListItem> listitem_release = new List<ListItem>();
            listitem_release.Add(new ListItem("Release_0", "1"));
            listitem_release.Add(new ListItem("Release_1", "2"));
            return listitem_release;
        }

        protected List<ListItem> getBuildListItem(string programm, string release)
        {
            List<ListItem> listitem_build = new List<ListItem>();
            listitem_build.Add(new ListItem("Build_1", "1"));
            return listitem_build;
        }
    }
}
