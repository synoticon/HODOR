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
        }

        public enum SearchType
        {
            NotSet = -1,
            User = 0,
            Product = 1,
            Upload = 2
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (this.tb_SearchInput != null)
            {
                if (rb_UserSearch.Checked)
                {
                    if (this.cb_NutzerNr.Checked && !this.cb_Name.Checked)
                    {
                        this.lv_User.DataSourceID = this.UserDataSourceByNutzerNr.ID;
                        //Bis jetzt leider noch keine Lösung gefunden über die EDS einen Join durchzuführen
                        //string searchInput =  this.tb_SearchInput.Text;

                        //HODOR_entities hodorDB = HodorGlobals.getHodorContext();
                        //var nutzerNrQuery = from u in hodorDB.Benutzers
                        //                    join rolle in hodorDB.Rolles
                        //                    on u.RolleID equals rolle.RolleID
                        //                    where u.NutzerNr.Contains(searchInput)
                        //                    select new
                        //                    {
                        //                        nutzerNr = u.NutzerNr,
                        //                        name = u.Name,
                        //                        email = u.Email,
                        //                        rolle = rolle.Rollenname
                        //                    };

                        //this.lv_User.DataSource = nutzerNrQuery.ToList();
                        //this.lv_User.DataBind();
                    }
                    else if (this.cb_Name.Checked && !this.cb_NutzerNr.Checked)
                    {
                        this.lv_User.DataSourceID = this.UserDataSourceByName.ID;
                    }
                    else if (this.cb_Name.Checked && this.cb_NutzerNr.Checked)
                    {
                        this.lv_User.DataSourceID = this.UserDataSourceByNutzerNrAndName.ID;
                    }

                    showResult(1);
                }
                else if (rb_ProductSearch.Checked)
                {
                    this.lb_Product.Items.Clear();
                    MultiView1.ActiveViewIndex = (int)SearchType.Product;
                    DoSearch();
                }
            }
        }

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
                showResult(0);
            }
            else
            {
                this.l_noCatch.Visible = true;
                showResult(0);
            }
        }

        protected void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Programm selectedProgram = ProgrammDAO.getProgrammByExactNameOrNull(this.lb_Product.SelectedItem.Text);
            List<Release> allReleases = ReleaseDAO.getAllMajorReleasesFor(selectedProgram);
                
            if(selectedProgram != null)
            {
                showResult(1);

                this.l_ProgrammName.Visible = true;
                this.l_ProgrammName.Text = selectedProgram.Name;
                this.l_ProgrammID.Text = selectedProgram.ProgrammID.ToString();
                
                this.lv_Product.DataSourceID = this.ProductDataSource.ID;
            }
        }

        private void showResult(int showType)
        {
            if(showType.Equals(0))
            {
                string viewName = "PreResultView";

                View newView = this.MultiView1.FindControl(viewName) as View;

                if (newView != null)
                {
                    this.MultiView1.SetActiveView(newView);
                }
            }
            else if (showType.Equals(1))
            {
                string viewName = "ResultView";

                View newView = this.MultiView1.FindControl(viewName) as View;

                if (newView != null)
                {
                    this.MultiView1.SetActiveView(newView);
                }
            }
            
        }

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


        protected void MenuLink_Command(object sender, CommandEventArgs e)
        {
            string viewName = e.CommandName + "View";

            View newView = this.MultiView1.FindControl(viewName) as View;

            if (newView != null)
            {
                this.MultiView1.SetActiveView(newView);
            }

        }

        protected void l_Rolle_Load(object sender, CommandEventArgs e)
        {
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(e.CommandArgument.ToString());
            if (user != null)
            {
                //this.l_
            }
        }

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

        protected void lb_Build_Command(object sender, CommandEventArgs e)
        {

        }
    }
}