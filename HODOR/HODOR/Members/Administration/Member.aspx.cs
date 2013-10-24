using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HODOR.src.DAO;
using System.Net.Mail;
using HODOR.src.Globals;

namespace HODOR.Members.Administration
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Rolle> roleList = HodorGlobals.getHodorContext().Rolles.ToList<Rolle>();
                foreach (Rolle role in roleList)
                {
                    this.ddl_roles.Items.Add(new ListItem(role.Rollenname));
                }
            }
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

        protected void b_Register_Click(object sender, EventArgs e)
        {
            Rolle role = RolleDAO.getRoleByNameOrNull(this.ddl_roles.SelectedItem.Text);
            if (role == null)
            {
                //oh nooo, we're screwed! What now? Throw an Exception? Naah, shouldn't happen, because values were supplied by the DB. Just abort, if murphys law strikes.
                this.is_registered.Visible = false;
                return;
            }
            Benutzer user = BenutzerDAO.createAndGetUser(this.tb_KdNr.Text.Trim(), this.tb_Firmenname.Text.Trim(), new MailAddress(this.tb_EMail.Text.Trim()), role);
            if (user == null)
            {
                this.is_registered.Visible = false;
            }
            this.Response.Redirect(this.Request.RawUrl);
        }
        protected void OnClick_b_upload(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
                try
                {
                    string body = l_discription.Value;
                    // transfer the file byte-by-byte to the response object
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                    FileUpload1.SaveAs(physicalPath + "\\LoadDirectory\\" +
                         FileUpload1.FileName);
                    l_message.Text = "File name: " +
                         FileUpload1.PostedFile.FileName + "<br>" +
                         FileUpload1.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         FileUpload1.PostedFile.ContentType;
                    string[] splitfilename = FileUpload1.FileName.Split('_');
                    string message = "";
                    //Überprüfe ob Programm vorhanden, wenn nicht erstellen
                    Programm newProgramm = ProgrammDAO.getProgrammByExactNameOrNull(splitfilename[0]);
                    if (newProgramm == null)
                    {
                        newProgramm = ProgrammDAO.createAndGetProgramm(splitfilename[0]);
                        message +="Neues Programm erstellt: " +newProgramm.Name;
                    }
                      //Überprüfe ob Release vorhanden, wenn nicht erstellen
                   Release newRelease = ReleaseDAO.getSingleReleaseByNumberAndProgramm(newProgramm.ProgrammID, int.Parse(splitfilename[1]));
                    if(newRelease == null)
                    {
                        newRelease = ReleaseDAO.createAndGetRelease(newProgramm, int.Parse(splitfilename[1]));
                        message += "Neues Release erstellt:" + newRelease.Releasenummer;
                    }
                     //Überprüfe ob Subrelease vorhanden, wenn nicht erstellen
                     Subrelease newSubRelease = null;
                     foreach (Subrelease item in ReleaseDAO.getSingleReleaseByID(newRelease.ReleaseID).Subreleases)
                     {
                        if (item.Releasenummer.ToString() == splitfilename[2])
                        {
                            newSubRelease = item;
                        }
                     }
                    if(newSubRelease == null)
                    {
                        newSubRelease = SubreleaseDAO.createAndGetSubrelease(newRelease, int.Parse(splitfilename[2]));
                         message += "Neues Subrelease erstellt:" + newSubRelease.Releasenummer;
                    }
                     //Überprüfe ob Build vorhanden, wenn nicht erstellen
                    Build newBuild = null;                
                    foreach (Build item in newSubRelease.Builds)
                    {
                         if (item.Releasenummer.ToString() == splitfilename[3])
                        {
                            newBuild = item;
                        }
                    }
                    if(newBuild == null)
                    {
                        //AARON!!! ich will den datei Pfad mit uebergeben!
                        //AARON!!! irgendwas stimmt mit der Releasenummer vetreilung nicht!
                        newBuild= BuildDAO.createAndGetBuild(newSubRelease, int.Parse(splitfilename[3]));
                          message += "Neues Build erstellt:" + newBuild.Releasenummer;
                    }
                    l_message.Text =message;
                }
                catch (Exception ex)
                {
                    l_message.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                l_message.Text = "You have not specified a file.";
            }
        }

        protected void lb_EditDeleteUser_Click(object source, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    {
                        if (e.CommandArgument != null && BenutzerDAO.getUserByKundenNrOrNull(e.CommandArgument.ToString()) != null)
                        {
                            BenutzerDAO.deleteUser(BenutzerDAO.getUserByKundenNrOrNull(e.CommandArgument.ToString()));
                        }
                    }
                    break;
                case "edit":
                    {
                        if (e.CommandArgument != null && BenutzerDAO.getUserByKundenNrOrNull(e.CommandArgument.ToString()) != null)
                        {
                            //Insert some edit stuff here!
                        }
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
            this.Response.Redirect(this.Request.RawUrl);
        }
    }
}
