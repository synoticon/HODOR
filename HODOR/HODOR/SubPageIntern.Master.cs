using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HODOR.src.Globals;
using HODOR.src.DAO;

namespace HODOR
{
    public partial class SubPageIntern : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
         *  @author: Aaron
         *  Hier werden schnell noch die nicht für User zugreifbaren Menuitems entfernt.
         *  Beim Hinzufügen weiterer Menüpunkte bitte neben dem switch Block auch die initiale Kapazität von <code>itemsToRemove</code> entsprechend anpassen.
        */
        protected void evaluateVisibility(object sender, EventArgs e)
        {
            if (sender is Menu)
            {
                Menu naviMenu = (Menu)sender;
                Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Page.User.Identity.Name);

                if (!HodorRoleProvider.isSupportAllowed(user))
                {
                    List<MenuItem> itemsToRemove = new List<MenuItem>(2);
                    foreach (MenuItem item in naviMenu.Items)
                    {
                        switch (item.Text)
                        {
                            case "Verwaltung":
                            case "Ticket Support":
                                itemsToRemove.Add(item);
                                break;
                            default:
                                break;
                        }
                    }
                    foreach (MenuItem item in itemsToRemove)
                    {
                        naviMenu.Items.Remove(item);
                    }
                }
            }
        }
    }
}