using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;
using System.Data.Objects;
using System.Net.Mail;


namespace HODOR.Members.Administration
{
    public partial class Verwaltung : System.Web.UI.Page
    {
        protected List<String> searchContext = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string viewString = Request.QueryString["view"];
                string nutzerNrString = Request.QueryString["nutzernr"];
                string programString = Request.QueryString["programID"];
                string releaseString = Request.QueryString["releasenummer"];
                string subReleaseString = Request.QueryString["subreleasenummer"];

                if (viewString != null)
                {
                    if (viewString == "ResultView")
                    {
                        MultiView1.SetActiveView(ResultView);
                        if(nutzerNrString != null)
                        {
                            this.lv_User.DataKeyNames = null;
                            editUser(nutzerNrString);   
                        }
                        else if (programString != null)
                        {
                            if (releaseString != null && subReleaseString == null)
                            {
                                showSubReleases(programString, releaseString);

                                Programm prog = ProgrammDAO.getProgrammByProgrammIDOrNull(Convert.ToInt32(programString));
                                string progName = prog.Name;

                                this.l_ProgrammName.Text = progName;

                                this.l_SubReleaseVon.Visible = true;
                                this.l_ProgrammName.Visible = true;

                            }
                            else if (releaseString != null && subReleaseString != null)
                            {
                                showBuilds(programString, releaseString, subReleaseString);

                                Programm prog = ProgrammDAO.getProgrammByProgrammIDOrNull(Convert.ToInt32(programString));
                                string progName = prog.Name;

                                this.l_SubReleaseNr.Text = subReleaseString;
                                this.l_ProgrammName.Text = progName;

                                this.l_BuildVon.Visible = true;
                                this.l_SubReleaseNr.Visible = true;
                                this.l_vonXY.Visible = true;
                                this.l_ProgrammName.Visible = true;
                            }
                        }
                    }
                }
            }
            else
            {
                string releaseQueryString = Request.QueryString["releasenummer"];
                this.l_ReleaseNummer.Text = releaseQueryString;
            }
        }

        /*
         *  Wiedergabe des gesuchten Users, welcher im ListView lv_User bearbeitet werden soll 
         */

        protected void editUser(string editInput)
        {
            HODOR_entities hodorDB = HodorGlobals.getHodorContext();
            var nutzerNrQuery = from u in hodorDB.Benutzers
                                join rolle in hodorDB.Rolles
                                on u.RolleID equals rolle.RolleID
                                where u.NutzerNr.Equals(editInput)
                                select new
                                {
                                    nutzerNr = u.NutzerNr,
                                    name = u.Name,
                                    email = u.Email,
                                    rolle = rolle.Rollenname
                                };

            this.lv_User.DataSource = nutzerNrQuery.ToList();
            this.lv_User.DataBind();
        }

        /*
         *  Anzeigen des jeweiligen Contextes (SubRelease, Build) 
         */

        protected void showSubReleases(string programID, string releaseNr)
        {
            Release release = ReleaseDAO.getSingleReleaseByNumberAndProgramm(Convert.ToInt32(programID), Convert.ToInt32(releaseNr));
            List<Subrelease> subReleases = SubreleaseDAO.getAllSubReleasesByRelease(release);
            
            this.lv_subRelease.DataSource = subReleases;
            this.lv_subRelease.DataBind();
        }

        protected void showBuilds(string programID, string releaseNr, string subReleaseNr)
        {
            Subrelease subRelease = SubreleaseDAO.getSingleSubReleaseByID(Convert.ToInt32(subReleaseNr));
            List<Build> buildList = BuildDAO.getAllBuildsBySubReleases(subRelease);

            this.lv_Build.DataSource = buildList;
            this.lv_Build.DataBind();
        }

        public enum SearchType
        {
            NotSet = -1,
            User = 0,
            Product = 1,
            Upload = 2
        }

        /*
         *  Controll für das Auswählen des Searchbutton 
         */

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (this.tb_SearchInput != null)
            {
                if (rb_UserSearch.Checked)
                {
                    if (this.cb_NutzerNr.Checked && !this.cb_Name.Checked)
                    {
                        this.lv_User.DataSourceID = this.UserDataSourceByNutzerNr.ID;
                    }
                    else if (this.cb_Name.Checked && !this.cb_NutzerNr.Checked)
                    {
                        this.lv_User.DataSourceID = this.UserDataSourceByName.ID;
                    }
                    else if (this.cb_Name.Checked && this.cb_NutzerNr.Checked)
                    {
                        this.lv_User.DataSourceID = this.UserDataSourceByNutzerNrAndName.ID;
                    }

                    showResult("Result");
                }
                else if (rb_ProductSearch.Checked)
                {
                    this.lb_Product.Items.Clear();
                    MultiView1.ActiveViewIndex = (int)SearchType.Product;
                    DoSearch();
                }
            }
        }

        /*
         *  Suchvorgang nach Programmnamen, welche teile des Suchfeldes beinhalten 
         */

        protected void DoSearch()
        {
            List<Programm> foundPrograms = ProgrammDAO.getProgrammeWithNameContainingOrNull(tb_SearchInput.Text);
            if (foundPrograms.Count != 0)
            {
                this.lb_Product.Visible = true;
                foreach (Programm program in foundPrograms)
                {
                    if (lb_Product.Items.FindByText(program.Name) == null)
                    {
                        this.lb_Product.Items.Add(new ListItem(program.Name));
                    }
                }
                showResult("PreResult");
            }
            else
            {
                this.l_noCatch.Visible = true;
                showResult("PreResult");
            }
        }

        /*
         *  Controll falls sich ein selektiertes Item wechselt bzw. ausgewählt wird
         *  und gleichzeitige Setzung des neuen Contextes
         */

        protected void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Programm selectedProgram = ProgrammDAO.getProgrammByExactNameOrNull(this.lb_Product.SelectedItem.Text);
            List<Release> allReleases = ReleaseDAO.getAllMajorReleasesFor(selectedProgram);
                
            if(selectedProgram != null)
            {
                showResult("Result");

                this.l_ProgrammName.Visible = true;
                this.l_ProgrammName.Text = selectedProgram.Name;
                this.l_ProgrammID.Text = selectedProgram.ProgrammID.ToString();
                
                this.lv_Product.DataSourceID = this.ProductDataSource.ID;
            }
        }

        /*
         *  Anzeigen des jeweilig angewählten View durch einen angestoßenen Suchvorgang
         */

        protected void showResult(string viewType)
        {
            string viewName = viewType + "View";

            View newView = this.MultiView1.FindControl(viewName) as View;

            if (newView != null)
            {
                this.MultiView1.SetActiveView(newView);
            }

        }

        /*
         *  Controlls für das Feststellen von Änderungen innerhalb eines ListViews 
         *  um den zuverwendenden Context festzulegen
         */

        protected void lvwUsers_SelectedIndexChanging(Object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)lv_User.Items[e.NewSelectedIndex];
            Label lablId = (Label)item.FindControl("CONTROL_ID");
        }

        protected void lvwProducts_SelectedIndexChanging(Object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)lv_Product.Items[e.NewSelectedIndex];
            Label lablId = (Label)item.FindControl("CONTROL_ID");
        }

        protected void lv_subRelease_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)lv_subRelease.Items[e.NewSelectedIndex];                  // <<<<<<<<<<<<<<<<<<<<<<<<<< Eventuell hier
            Label lablId = (Label)item.FindControl("CONTROL_ID");
        }

        protected void lv_Build_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)lv_Build.Items[e.NewSelectedIndex];
            Label lablId = (Label)item.FindControl("CONTROL_ID");
        }

        /*
         *  Umstellung bzw. Erweiterung der Sucheigenschaften 
         */

        protected void rb_UserSearch_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Name.Visible = true;
            this.cb_NutzerNr.Visible = true;
        }

        protected void rb_ProductSearch_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Name.Visible = false;
            this.cb_NutzerNr.Visible = false;
        }

        /*
         *  Verweise auf die jeweiligen Views mit zugeteiltem Release bzw. SubRelease 
         */
        
        protected void lb_subRelease_Command(object sender, CommandEventArgs e)
        {                                                                                   // <<<<<<<<<<<<<<<<<<<<<<<<<< Eventuell hier
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            string queryString = "&programID=" + arg[0] + "&releasenummer=" + arg[1];
            Response.Redirect("~/Members/Administration/Verwaltung.aspx?view=ResultView" + queryString);
        }

        protected void lb_Builds_Command(object sender, CommandEventArgs e)
        {
            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');
            string queryString = "&programID=" + arg[0] + "&releasenummer=" + arg[1] + "&subreleasenummer=" + arg[2];
            Response.Redirect("~/Members/Administration/Verwaltung.aspx?view=ResultView" + queryString);
        }

        protected void lb_Lizenzen_Command(object sender, CommandEventArgs e)
        {
            string argument = e.CommandArgument.ToString();
            string queryString = "?nutzernr=" + argument;
            Response.Redirect("~/Members/Lizenzen.aspx" + queryString);
        }
    }
}