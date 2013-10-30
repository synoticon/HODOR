using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.DAO;
using HODOR.src.Globals;


namespace HODOR.Members
{
    public partial class Lizenzen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nutzerNr =  Request.QueryString["nutzerNr"];
                if (nutzerNr != null)
                {
                    Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(nutzerNr);
                    if (user != null)
                    {
                        this.l_KdNr.Text = user.NutzerNr;
                        this.l_FirmenName.Text = user.Name;
                        this.l_EMail.Text = user.Email;

                        this.lv_User.DataSource = user.Lizenzs.OfType<Lizenz_Zeitlich>().ToList();

                        this.lv_User.DataBind();
                    }
                }
            }
        }

        protected void lv_User_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)lv_User.Items[e.NewSelectedIndex];
            Label lablId = (Label)item.FindControl("CONTROL_ID");
        }

        protected void lv_User_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewItem item = (ListViewItem)lv_User.Items[e.NewEditIndex];
            Label lablId = (Label)item.FindControl("LizenzID");
        }
    }
}