using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace HODOR
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void b_Login_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage(tb_Kundennummer.Text, false);
        }
    }
}
