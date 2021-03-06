﻿using HODOR.src.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HODOR.src.Globals;
namespace HODOR.Members
{
  public partial class LandingPage : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

      String Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
      Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(Username);
      // Wenn Support oder höher wird die Linkbutton für die Userview nicht angezeigt
      if (HodorRoleProvider.isSupportAllowed(user))
      {
        User.Visible = true;
      }
      // Wenn Support oder höher wird  der Editierbutton für einen User angezeigt
      if (HodorRoleProvider.isSupportAllowed(user))
        b_edit.Visible = true;
  //Postdatenvorhanden sind
      if (Request.QueryString.Count > 0)
      {
        //Wird überprueft ob Rolle Support oder höher ist, und dann wird der User der im Post steht angezeigt.
        if (HodorRoleProvider.isSupportAllowed(user))
        {
          Benutzer otherUser = BenutzerDAO.getUserByKundenNrOrNull(Request.QueryString["otherUserName"].ToString());
          fillUserViewContentbyUser(otherUser);
          filloutSelectProgrammView(otherUser);
          filloutDownloadHistoryView(otherUser);
        
        }
      }
      else if (!IsPostBack)
      {
        // wenn kein Postdaten vorhanden sind wird der eingelogte user angezeigt
        fillUserViewContentbyUser(user);

        filloutSelectProgrammView(BenutzerDAO.getUserByKundenNrOrNull(Username));
        filloutDownloadHistoryView(BenutzerDAO.getUserByKundenNrOrNull(Username));
      }

      filloutSelectUserView(BenutzerDAO.getUserByKundenNrOrNull(Username));
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
    /// <summary>
    /// Füllt die Anzeige des mit Benutzerdaten, wo der Support oder höher einen Benutzer auswählen kann
    /// </summary>
    /// <param name="user"></param>

    protected void filloutSelectUserView(Benutzer user)
    {
      if (HodorRoleProvider.isSupportAllowed(user))
      {

        foreach (Benutzer item in BenutzerDAO.getAllUsers().OrderBy(o => o.NutzerNr).ToList())
        {
          if (listbox_user.Items.FindByText(item.NutzerNr) == null)
          {
            listbox_user.Items.Add(new ListItem(item.NutzerNr));
          }
        }
      }
    }
    /// <summary>
    /// Füllt die Downloadhistorie mit den Daten des 
    /// </summary>
    /// <param name="user"></param>
    protected void filloutDownloadHistoryView(Benutzer user)
    {

      foreach (Download_History item in BenutzerDAO.getDownloadHistoryEntriesForUser(user))
      {

        TableRow r = new TableRow();
        r.Cells.Add(createNewTableCell(item.Benutzer.NutzerNr.ToString()));
        r.Cells.Add(createNewTableCell(item.Build.Programm.Name.ToString()));

        Build build = BuildDAO.getBuildByIDOrNull(item.BuildID);
        if (build == null)
        {
          r.Cells.Add(createNewTableCell("Deleted Build"));
        }
        r.Cells.Add(createNewTableCell(BuildDAO.getVersionStringForBuild(build)));

        r.Cells.Add(createNewTableCell(String.Format("{0:dd.MM.yyyy}", item.DownloadDatum)));
        Table1.Rows.Add(r);
      }

    }

    /// <summary>
    /// Erstellt eine neue Tablecell mit dem übergebenen Inhalt
    /// </summary>
    /// <param name="cellContent"></param>
    /// <returns></returns>
    protected TableCell createNewTableCell(string cellContent)
    {
      TableCell c = new TableCell();
      c.Controls.Add(new LiteralControl(cellContent));
      return c;
    }

    /// <summary>
    /// Füllt die Listebox in dem der Benutzer sich die für sich Lizensierten Programme ansehen kann, wenn er ein Supporter oder höher ist werden ihm alle Programme anzeigen
    /// </summary>
    /// <param name="user"></param>
    protected void filloutSelectProgrammView(Benutzer user)
    {
      if (HodorRoleProvider.isSupportAllowed(user))
      {
        foreach (Programm item in ProgrammDAO.getAllProgramme())
        {
          String idName = item.ProgrammID.ToString() + ":" + item.Name.ToString();
          if (listbox_prog.Items.FindByText(idName) == null)
          {
            listbox_prog.Items.Add(new ListItem(idName));
          }
        }
      }
      else
      {

        foreach (Programm item in BenutzerDAO.getAllProgrammsLicensedForUser(user))
        {
          String idName = item.ProgrammID.ToString() + ":" + item.Name.ToString();
          if (listbox_prog.Items.FindByText(idName) == null)
          {
            listbox_prog.Items.Add(new ListItem(idName));
          }
        }
      }
    }

    /// <summary>
    /// Füllt die Ansicht mit den Daten des übergebenen Benutzers
    /// </summary>
    /// <param name="user"></param>
    protected void fillUserViewContentbyUser(Benutzer user)
    {
      if (user != null)
      {
        l_benutzer.Text = user.Name;
        l_kundenNummer.Text = user.NutzerNr;
        l_eMail.Text = user.Email;
        l_rolle.Text = user.Rolle.Rollenname;
      }
      else
      {
        l_benutzer.Text = "Benutzer nicht gefunden";
      }
    }

    protected void OnClick_b_user_display(object sender, EventArgs e)
    {
      Response.Redirect("LandingPage.aspx?otherUserName=" + Server.UrlEncode(listbox_user.SelectedValue));
    }

    protected void OnClick_b_prog_display(object sender, EventArgs e)
    {
      String[] split = listbox_prog.SelectedValue.Split(':');
      Response.Redirect("Produkte.aspx?progID=" + Server.UrlEncode(split[0]));
    }

    protected void b_edit_Click(object sender, EventArgs e)
    {
      Response.Redirect("Administration/Verwaltung.aspx?view=ResultView&nutzernr=" + this.l_kundenNummer.Text);
    }

    protected void b_licenz_Click(object sender, EventArgs e)
    {
      if (Request.QueryString.Count > 0)
      {
        if (HodorRoleProvider.isSupportAllowed(BenutzerDAO.getUserByKundenNrOrNull(System.Threading.Thread.CurrentPrincipal.Identity.Name)))
        {
          Response.Redirect("Lizenzen.aspx?nutzernr=" + Request.QueryString["otherUserName"].ToString());
        }
      }
      Response.Redirect("Lizenzen.aspx");

    }
  }
}