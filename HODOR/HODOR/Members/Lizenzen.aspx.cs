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
      String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
      Benutzer loggedinuser = BenutzerDAO.getUserByKundenNrOrNull(Username);
      //Wenn Admin dann darf er weiter zur Bearbeiten Seite, wenn nicht wird der Button ausgeblendet
      if (HodorRoleProvider.isAdminAllowed(loggedinuser))
      {
        lb_Build.Visible = true;
      }
      else
      {
        lb_Build.Visible = false;
      }

      // wenn der Besucher der Seite ein Support oder Höher ist
      if (HodorRoleProvider.isSupportAllowed(loggedinuser))
      {
         //Wenn nichts per Post übergeben wurde wird für den Eingeloggten Benutzer die Lizenzen angezeigt
        if(Request.QueryString.Count == 0)
        {
            this.l_KdNr.Text = loggedinuser.NutzerNr;

            this.lv_User.DataSource = loggedinuser.Lizenzs.OfType<Lizenz_Zeitlich>().ToList();

            this.lv_User.DataBind();  
        }
        else if (!IsPostBack)
        {
          // und eine Nutzernummer übergeben wurde per Post wird dieser Benutzer angezeigt
          string nutzerNr = null;
         
          nutzerNr = Request.QueryString["nutzerNr"];
          
      
          if (nutzerNr != null)
          {
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(nutzerNr);
            if (user != null)
            {
              this.l_KdNr.Text = user.NutzerNr;

              this.lv_User.DataSource = user.Lizenzs.OfType<Lizenz_Zeitlich>().ToList();

              this.lv_User.DataBind();
            }
          }
        }
         
      }
        // wenn der User kein Support oder höher ist, wird für den eingeloggten Benutzer die Lizenzen angezeigt
      else
      {
       
            this.l_KdNr.Text = loggedinuser.NutzerNr;

            this.lv_User.DataSource = loggedinuser.Lizenzs.OfType<Lizenz_Zeitlich>().ToList();

            this.lv_User.DataBind();
            foreach (ListViewItem item in lv_User.Items.ToList())
            {
                LinkButton lb = (LinkButton)item.FindControl("lb_Details1") as LinkButton;
                lb.Visible = false;
                lb = (LinkButton)item.FindControl("lb_delete") as LinkButton;
                lb.Visible = false;
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

    protected void lb_Build_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Members/Administration/NeuAnlegen.aspx?view=LizenzView&nutzernr=" + this.l_KdNr.Text);
    }

  }
}