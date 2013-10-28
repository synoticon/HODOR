using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;

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
                    MultiView1.ActiveViewIndex = (int)SearchType.User;
                    getSearchContext();
                }
                else if (rb_ProductSearch.Checked)
                {
                    MultiView1.ActiveViewIndex = (int)SearchType.Product;
                    getSearchContext();
                }
            }
        }

        protected void getSearchContext()
        {
            if (MultiView1.ActiveViewIndex > -1)
            {
                SearchType mSearchType =
                     (SearchType)MultiView1.ActiveViewIndex;

                DoSearch(tb_SearchInput.Text, mSearchType);
            }
        }

        protected void DoSearch(String searchTerm, SearchType type)
        {

            switch (type)
            {
                //case SearchType.User:
                //    {
                //        List<Benutzer> allUsers = BenutzerDAO.getAllUsers();
                //        foreach (Benutzer user in allUsers)
                //        {
                //            if (user.Name.Contains(searchTerm) || user.NutzerNr.Contains(searchTerm))
                //            {
                //                this.lb_User.Visible = true;
                //                if (lb_User.Items.FindByText(user.NutzerNr) == null)
                //                {
                //                    this.lb_User.Items.Add(new ListItem(user.NutzerNr));
                //                }
                //            }
                //            else
                //            {
                //                this.l_noCatch.Visible = true;
                //            }
                //        }
                //    }
                //    break;
                case SearchType.Product:
                    {
                        List<Programm> foundPrograms = ProgrammDAO.getProgrammeWithNameContainingOrNull(tb_SearchInput.Text);
                        if (foundPrograms != null)
                        {
                            this.lb_Product.Visible = true;
                            foreach (Programm program in foundPrograms)
                            {
                                this.lb_Product.Visible = true;
                                if (lb_Product.Items.FindByText(program.Name) == null)
                                {
                                    this.lb_Product.Items.Add(new ListItem(program.Name));
                                }
                            }
                        }
                        else
                        {
                            this.l_noCatch.Visible = true;
                        }
                    }
                    break;
                case SearchType.NotSet:
                    break;
            }
        }

        protected void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MultiView1.ActiveViewIndex == 0)
            {
                //Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(this.lb_User.SelectedItem.Text);
                //if (user != null)
                //{
                //    this.l_Name.Text = user.Name;
                //    this.l_NutzerNr.Text = user.NutzerNr;
                //    this.l_Name.Text = user.Rolle.Rollenname;
                //    this.l_EMail.Text = user.Email;
                //    this.l_LizenzAnzahl.Text = user.Lizenzs.Count.ToString();
                //    this.l_LizenzZaehler.Visible = true;
                //    this.l_ende.Visible = true;
                //}
            }
            else if(MultiView1.ActiveViewIndex == 1)
            {
                Programm selectedProgram = ProgrammDAO.getProgrammByExactNameOrNull(this.lb_Product.SelectedItem.Text);
                List<Release> allReleases = ReleaseDAO.getAllMajorReleasesFor(selectedProgram);
                if(selectedProgram != null)
                {
                    this.l_ProgrammName.Text = selectedProgram.Name;
                    this.l_ProgrammID.Text = selectedProgram.ProgrammID.ToString();

                    string viewName = "ResultView";

                    View newView = this.MultiView1.FindControl(viewName) as View;

                    if (newView != null)
                    {
                        this.MultiView1.SetActiveView(newView);
                    }
                }
            }
        }

        /*
         * Upload Funktionalität
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
    }
}