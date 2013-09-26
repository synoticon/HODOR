using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HODOR.Members.Administration
{
    public partial class ProduktVerwaltung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public enum SearchType
        {
            NotSet = -1,
            Products = 0,
            Category = 1
        }

        protected void Button1_Click(Object sender, System.EventArgs e)
        {
            if (MultiView1.ActiveViewIndex > -1)
            {
                String searchTerm = "";
                SearchType mSearchType =
                     (SearchType)MultiView1.ActiveViewIndex;
                switch (mSearchType)
                {
                    case SearchType.Category:
                        DoSearch(textCategory.Text, mSearchType);
                        break;
                    case SearchType.NotSet:
                        break;
                }
            }
        }

        protected void DoSearch(String searchTerm, SearchType type)
        {
            // Code here to perform a search.
        }

        protected void radioButton_CheckedChanged(Object sender,
            System.EventArgs e)
        {
            if (radioProduct.Checked)
            {
                MultiView1.ActiveViewIndex = (int)SearchType.Products;
            }
            else if (radioCategory.Checked)
            {
                MultiView1.ActiveViewIndex = (int)SearchType.Category;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
                try
                {
                    string body = TextArea1.Value;

                    FileUpload1.SaveAs("C:\\Uploads\\" +
                         FileUpload1.FileName);
                    Label1.Text = "File name: " +
                         FileUpload1.PostedFile.FileName + "<br>" +
                         FileUpload1.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         FileUpload1.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    Label1.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                Label1.Text = "You have not specified a file.";
            }
        }

    }
}