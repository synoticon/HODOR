using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;

namespace HODOR
{
    public partial class Produkte : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


            String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);

            if (Request.QueryString.Count != 0)
            {
                lb_programmname.Text = ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(Request.QueryString["progID"])).Name;
                
                if (!IsPostBack)
                {
                    fillOutDDL_Release();
                }
            }
            else
            {
                SelectProgrammView(user);
                DDL_Programm.Visible = true;
                
            }
        
        }


        protected void ClearAllDDL()
        {
            DDL_SubRelease.Items.Clear();
            DDL_SubRelease.Items.Add((new ListItem("---Select---", "null")));
            DDL_Build.Items.Clear();
            DDL_Build.Items.Add((new ListItem("---Select---", "null")));
            DDL_Release.Items.Clear();
            DDL_Release.Items.Add((new ListItem("---Select---", "null")));
            b_download.Visible = false;
            l_Builddiscription.Text = "";
            l_Releasediscription.Text = "";
            l_SubReleasediscription.Text = "";

        }

        protected void SelectProgrammView(Benutzer user)
        {
            
            
            if (HodorRoleProvider.isSupportAllowed(user))
            {
                foreach (Programm item in ProgrammDAO.getAllProgramme())
                {

                    if (DDL_Programm.Items.FindByText(item.Name) == null)
                    {
                        DDL_Programm.Items.Add(new ListItem(item.Name, item.ProgrammID.ToString()));
                     
                    }
                }
            }
            else
            {

                foreach (Programm item in BenutzerDAO.getAllProgrammsLicensedForUser(user))
                {
                    if (DDL_Programm.Items.FindByText(item.Name) == null)
                    {
                        DDL_Programm.Items.Add(new ListItem(item.Name, item.ProgrammID.ToString()));
                    }
                }
            }
        }
        protected void fillOutDDL_Release()
        {
            DDL_Release.Items.Clear();
            DDL_Release.Items.Add((new ListItem("---Select---", "null")));
            foreach (Release item in BenutzerDAO.getAllReleasesOfProgrammLicensedForUser
                         (BenutzerDAO.getUserByKundenNrOrNull(System.Threading.Thread.CurrentPrincipal.Identity.Name),
                          ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(Request.QueryString["progID"]))).ToList())
            {
                if (DDL_Release.Items.FindByText(item.Releasenummer.ToString()) == null)
                {
                    DDL_Release.Items.Add(new ListItem(item.Releasenummer.ToString(), item.ReleaseID.ToString()));
                }
            }
        }

        protected void SelectedChangeRelease(object sender, EventArgs e)
        {
            DDL_SubRelease.Items.Clear();
            DDL_SubRelease.Items.Add((new ListItem("---Select---", "null")));
            if (DDL_Release.SelectedValue != "null")
            {
                foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                {
                    if (DDL_SubRelease.Items.FindByText(item.Releasenummer.ToString()) == null)
                    {
                        DDL_SubRelease.Items.Add((new ListItem(item.Releasenummer.ToString(), item.ReleaseID.ToString())));
                    }
                }

                l_Releasediscription.Text = ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Beschreibung;
            }
        }

        protected void SelectedChangeSubRelease(object sender, EventArgs e)
        {
            DDL_Build.Items.Clear();
            DDL_Build.Items.Add((new ListItem("---Select---", "null")));
            if (DDL_SubRelease.SelectedValue != "null")
            {
                foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                {
                    if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
                    {
                        l_SubReleasediscription.Text = item.Beschreibung;
                        foreach (Build build in item.Builds)
                        {
                            if (DDL_Build.Items.FindByText(build.ReleaseID.ToString()) == null)
                            {
                                DDL_Build.Items.Add((new ListItem(build.Releasenummer.ToString(), build.ReleaseID.ToString())));
                            }
                        }
                    }
                }
            }
        }

        protected void SelectedChangeProgramm(object sender, EventArgs e)
        {
            ClearAllDDL();
            if (DDL_Programm.SelectedValue != "null")
            {
                foreach (Release item in BenutzerDAO.getAllReleasesOfProgrammLicensedForUser
                       (BenutzerDAO.getUserByKundenNrOrNull(System.Threading.Thread.CurrentPrincipal.Identity.Name),
                        ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(DDL_Programm.SelectedValue))).ToList())
                {
                    if (DDL_Release.Items.FindByText(item.Releasenummer.ToString()) == null)
                    {
                        DDL_Release.Items.Add(new ListItem(item.Releasenummer.ToString(), item.ReleaseID.ToString()));
                       
                    }
                }
            }
            l_Programmdiscripion.Text = ProgrammDAO.getProgrammByProgrammIDOrNull(int.Parse(DDL_Programm.SelectedValue)).Beschreibung;
        }

        protected void SelectedChangeBuild(object sender, EventArgs e)
        {
          b_download.Visible = true;
          if (DDL_Build.SelectedValue != "null")
          {
            foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
            {
              if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
              {

                foreach (Build build in item.Builds)
                {
                  if (build.ReleaseID.ToString() == DDL_Build.SelectedValue)
                  {
                    l_Builddiscription.Text = build.Beschreibung;
                  }
                }
              }
            }
          }
        }

        protected void OnClick_b_download(object sender, EventArgs e)
        {

            try
            {
                Build buildToDownload = null;
                foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(int.Parse(DDL_Release.SelectedValue)).Subreleases)
                {
                    if (item.ReleaseID == int.Parse(DDL_SubRelease.SelectedValue))
                    {
                        l_SubReleasediscription.Text = item.Beschreibung;
                        foreach (Build build in item.Builds.OrderBy(b => b.Releasenummer))
                        {
                            if (build.ReleaseID == int.Parse(DDL_Build.SelectedValue))
                            {
                                buildToDownload = build;
                            }

                        }
                    }
                }
                if (buildToDownload != null)
                {
                    System.String filename = buildToDownload.Datendateipfad;

                    // set the http content type to "APPLICATION/OCTET-STREAM
                    Response.ContentType = "APPLICATION/OCTET-STREAM";

                    // initialize the http content-disposition header to
                    // indicate a file attachment with the default filename
                    // "myFile.txt"
                    System.String disHeader = "Attachment; Filename=\"" + filename +
                       "\"";
                    Response.AppendHeader("Content-Disposition", disHeader);

                    // transfer the file byte-by-byte to the response object
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                    System.IO.FileInfo fileToDownload = new
                       System.IO.FileInfo(physicalPath + "\\LoadDirectory\\" + buildToDownload.Datendateipfad);
                    Response.Flush();
                    Response.WriteFile(fileToDownload.FullName);

                    String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                    Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);
                    DownloadHistoryDAO.createAndGetDownloadHistory(user, buildToDownload);
                    b_download.Visible = false;
                }
            }
            catch (System.Exception exc)
            // file IO errors
            {
                l_Builddiscription.Text = exc.Message;
            }

        }

    }
}
