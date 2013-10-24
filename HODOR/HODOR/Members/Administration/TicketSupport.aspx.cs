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
    public partial class TicketSupport : System.Web.UI.Page
    {
        protected List<String> searchContext = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Total number of rows.
            int rowCnt;
            // Current row count.
            int rowCtr;


            //Anzahl der Reports
            rowCnt = 10;

            for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
            {
                // Create new row and add it to the table.
                TableRow tRow = new TableRow();

                TableCell tCell = new TableCell();
                tCell.Text = "Ticketnummer";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Erstellt von";
                tRow.Cells.Add(tCell); 

                tCell = new TableCell();
                tCell.Text = "Programm";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Release";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Subrelease";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Build";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Beschreibung";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = "Status";
                tRow.Cells.Add(tCell);

                //if( stauts== offen)
                Button btn = new Button();
                btn.Text = "Besträtigen";
                btn.CommandName = "text";
                btn.ID = "Button3";
                btn.CommandArgument = "test";
                btn.Click += Button3_Click;
                tCell.Controls.Add(btn);
                tRow.Cells.Add(tCell);
                //elese tCell.text= Status

                Table1.Rows.Add(tRow);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        protected void ticketview()
        {
            //AARON!!! ich will ein TicketDAO und eine Ticket Tabelle!

            /*  foreach (Ticket item in ticketDao.getAllTickets())
               {

                   TableRow r = new TableRow();
                   r.Cells.Add(createNewTableCell(item.Benutzer.NutzerNr.ToString()));
                   r.Cells.Add(createNewTableCell(item.Build.Programm.Name.ToString()));
                   r.Cells.Add(createNewTableCell(item.BuildID.ToString()));
                   r.Cells.Add(createNewTableCell(item.DownloadDatum.ToString()));
                   Table1.Rows.Add(r);
               }*/

        }


    }
}