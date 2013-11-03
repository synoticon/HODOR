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
            string programString = Request.QueryString["programID"];
            string releaseString = Request.QueryString["releasenummer"];
            string subReleaseString = Request.QueryString["subreleasenummer"];
                
            if(!IsPostBack)
            {
                string viewString = Request.QueryString["view"];
                string nutzerNrString = Request.QueryString["nutzernr"];

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
                        else if (subReleaseString != null)
                        {
                            this.lv_Product.Visible = false;
                            this.lv_subRelease.Visible = false;

                            showBuilds(subReleaseString);

                            Programm prog = ProgrammDAO.getProgrammByProgrammIDOrNull(Convert.ToInt32(programString));
                            string progName = prog.Name;

                            this.l_SubReleaseNr.Text = subReleaseString;
                            this.l_ProgrammName.Text = progName;

                            this.l_BuildVon.Visible = true;
                            this.l_SubReleaseNr.Visible = true;
                            this.l_vonXY.Visible = true;
                            this.l_ProgrammName.Visible = true;
                        }
                        else if (programString != null)
                        {
                            if (releaseString != null)
                            {
                                this.lv_Product.Visible = false;
                                this.lv_Build.Visible = false;

                                showSubReleases(programString, releaseString);

                                Programm prog = ProgrammDAO.getProgrammByProgrammIDOrNull(Convert.ToInt32(programString));
                                string progName = prog.Name;

                                this.l_ProgrammName.Text = progName;

                                this.l_SubReleaseVon.Visible = true;
                                this.l_ProgrammName.Visible = true;

                            }
                        }
                    }
                }
            }
            else
            {
                this.l_ReleaseNummer.Text = releaseString;
                if(programString != null && releaseString != null && subReleaseString == null)
                {//Da die DataSourcen in den Fällen SubRelease und Build selbst gebaut sind, müssen hier die Datenquellen neu gebindet werden.
                    showSubReleases(programString, releaseString);  
                }
                else if (programString != null && releaseString != null && subReleaseString == null)
                {
                    showBuilds(subReleaseString); 
                }
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
            List<Subrelease> subReleases = release.Subreleases.OrderBy(s => s.Releasenummer).ToList();

            this.l_ReleaseID.Text = release.ReleaseID.ToString();

            this.lv_subRelease.DataSource = subReleases;
            this.lv_subRelease.DataBind();
        }

        protected void showBuilds(string subReleaseNr)
        {
            Subrelease subRelease = SubreleaseDAO.getSingleSubReleaseByID(Convert.ToInt32(subReleaseNr));
            List<Build> buildList = subRelease.Builds.OrderBy(s => s.Releasenummer).ToList();

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
                        this.lv_Product.Visible = false;
                        this.lv_subRelease.Visible = false;
                        this.lv_Build.Visible = false;
                        this.lv_User.Visible = true;
                        this.lv_User.DataSourceID = this.UserDataSourceByNutzerNr.ID;
                    }
                    else if (this.cb_Name.Checked && !this.cb_NutzerNr.Checked)
                    {
                        this.lv_Product.Visible = false;
                        this.lv_subRelease.Visible = false;
                        this.lv_Build.Visible = false;
                        this.lv_User.Visible = true;
                        this.lv_User.DataSourceID = this.UserDataSourceByName.ID;
                    }
                    else if (this.cb_Name.Checked && this.cb_NutzerNr.Checked)
                    {
                        this.lv_Product.Visible = false;
                        this.lv_subRelease.Visible = false;
                        this.lv_Build.Visible = false;
                        this.lv_User.Visible = true;
                        this.lv_User.DataSourceID = this.UserDataSourceByNutzerNrAndName.ID;
                    }

                    showResult("Result");
                }
                else if (rb_ProductSearch.Checked)
                {
                    this.lv_User.Visible = false;
                    this.lv_Product.Visible = true;
                    this.lv_subRelease.Visible = true;
                    this.lv_Build.Visible = true;
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
            //avoid NPE from empty search Strings
            if (String.IsNullOrEmpty(tb_SearchInput.Text))
            {
                lb_Product.Items.Clear();
                return;
            }

            //Don't show "No Result" label if one of the previous searches was without result
            this.l_noCatch.Visible = false;

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
            ListViewItem item = (ListViewItem)lv_subRelease.Items[e.NewSelectedIndex];
            Label lablId = (Label)item.FindControl("l_test");
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
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            string queryString = "&programID=" + arg[0] + "&releasenummer=" + arg[1];
            Response.Redirect("~/Members/Administration/Verwaltung.aspx?view=ResultView" + queryString);
        }

        protected void lb_Builds_Command(object sender, CommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            string queryString = "&programID=" + arg[0] + "&subreleasenummer=" + arg[1];
            Response.Redirect("~/Members/Administration/Verwaltung.aspx?view=ResultView" + queryString);
        }

        protected void lb_Lizenzen_Command(object sender, CommandEventArgs e)
        {
            string argument = e.CommandArgument.ToString();
            string queryString = "?nutzernr=" + argument;
            Response.Redirect("~/Members/Lizenzen.aspx" + queryString);
        }

        protected void lv_subRelease_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            this.lv_subRelease.EditIndex = -1;

            string programString = Request.QueryString["programID"];
            string subReleaseString = Request.QueryString["subreleasenummer"];

            showSubReleases(programString, subReleaseString);
        }

        protected void lb_delete_Command(object sender, CommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            Subrelease sub = SubreleaseDAO.getSingleSubReleaseByID(Convert.ToInt32(arg[1]));
            SubreleaseDAO.deleteSubrelease(sub);

            string programString = Request.QueryString["programID"];
            string subReleaseString = Request.QueryString["subreleasenummer"];

            showSubReleases(programString, subReleaseString);
        }

        protected void lv_Build_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            this.lv_subRelease.EditIndex = -1;

            string programString = Request.QueryString["programID"];
            string subReleaseString = Request.QueryString["subreleasenummer"];

            showSubReleases(programString, subReleaseString);
        }

        protected void lb_delete_Command2(object sender, CommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            Build build = BuildDAO.getBuildByIDOrNull(Convert.ToInt32(arg[1]));
            BuildDAO.deleteBuild(build);

            string programString = Request.QueryString["programID"];
            string subReleaseString = Request.QueryString["subreleasenummer"];

            showBuilds(subReleaseString);
        }
    }
}