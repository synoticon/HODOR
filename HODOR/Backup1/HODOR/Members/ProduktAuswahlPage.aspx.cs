using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace HODOR
{
    public partial class ProduktAuswahlPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            
            if (!IsPostBack)
            {
                foreach (ListItem item in getProgrammListItem())
                {
                    DDL_Programm.Items.Add(item);
                }
            }

        }
        protected List<ListItem> getProgrammListItem()
        {
            List<ListItem> listitem_programm = new List<ListItem>();
            listitem_programm.Add(new ListItem("Test", "1"));
            listitem_programm.Add(new ListItem("Mod", "2"));
            return listitem_programm;
        }

        protected void submit(object sender, EventArgs e)
        {
            string viewName = DDL_Programm.SelectedItem.Text + "View";

            View newView = this.MultiView1.FindControl(viewName) as View;

            if (newView != null)
            {
                this.MultiView1.SetActiveView(newView);
            }
        }
    }
}
