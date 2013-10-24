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
    public partial class ProduktVerwaltung : System.Web.UI.Page
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

                searchContext = DoSearch(tb_SearchInput.Text, mSearchType);
            }
        }

        protected List<String> DoSearch(String searchTerm, SearchType type)
        {
            List<String> searchContext = null;

            switch (type)
            {
                case SearchType.User:
                    {
                        List<Benutzer> allUsers = BenutzerDAO.getAllUsers();
                        foreach (Benutzer user in allUsers)
                        {
                            if(user.Name.Contains(searchTerm) || user.NutzerNr.Contains(searchTerm))
                            {
                                if (lb_User.Items.FindByText(user.NutzerNr) == null)
                                {
                                    this.lb_User.Items.Add(new ListItem(user.NutzerNr));                                    
                                }
                            }
                        }
                    }
                    break;
                case SearchType.Product:
                    {
                        List<Programm> allProducts = ProgrammDAO.getAllProgramme();
                        foreach (Programm program in allProducts)
                        {
                            if(program.Name.Contains(searchTerm))
                            {
                                if (lb_User.Items.FindByText(program.Name) == null)
                                {
                                    this.lb_Product.Items.Add(new ListItem(program.Name));
                                }
                            }
                        }
                    }
                    break;
                case SearchType.NotSet:
                    break;
            }
            return searchContext;
        }

        protected void lb_SelectedIndexChanged_Main(object sender, EventArgs e)
        {
            int lizenz_count = 0;

            if (MultiView1.ActiveViewIndex == 0)
            {
                Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(this.lb_User.SelectedItem.Text);
                if(user != null)
                {
                    this.l_Name.Text = user.Name;
                    this.l_NutzerNr.Text = user.NutzerNr;
                    this.l_Name.Text = user.Rolle.ToString();
                    this.l_EMail.Text = user.Email;
                    List<Lizenz> lizenzen = user.Lizenzs.ToList();
                    foreach (Lizenz lizenz in lizenzen)
                    {
                        lizenz_count++;
                    }
                    this.l_LizenzAnzahl.Text = lizenz_count.ToString();
                    this.l_LizenzZaehler.Visible = true;
                    this.l_ende.Visible = true;
                }
            }
            else if (MultiView1.ActiveViewIndex == 1)
            {
                Programm program = ProgrammDAO.getProgrammByExactNameOrNull(this.lb_Product.SelectedItem.Text);
                List<Release> releases = ReleaseDAO.getAllMajorReleasesFor(program);
                this.lb_Release.Visible = true; 
                if(program != null)
                {
                    this.l_ProgrammName.Text = program.Name;
                    foreach (Release release in releases)
	                {
                        if (lb_User.Items.FindByText(release.Releasenummer.ToString()) == null)
                        {
                            this.lb_Release.Items.Add(new ListItem(release.Releasenummer.ToString()));
                        }
	                }
                }
            }
        }

        protected void lb_SelectedIndexChanged_Sub(object sender, EventArgs e)
        {
            //Subrelease subRelease =  this.lb_Release.SelectedIndex;

            //this.l_ReleaseNr.Text = release.Releasenummer.ToString();
            //this.l_ReleaseDatum.Text = release.Releasedatum.ToString();
            //this.l_Beschreibung.Text = release.Beschreibung;
        }

        /*
         * Upload Funktionalität
         */

        protected void MenuLink_Command(object sender, CommandEventArgs e)
        {
            string viewName = e.CommandName + "View";

            View newView = this.MultiView1.FindControl(viewName) as View;

            if (newView != null)
            {
                this.MultiView1.SetActiveView(newView);
            }

        }
      
    }
}