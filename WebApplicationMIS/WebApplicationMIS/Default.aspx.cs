using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace WebApplicationMIS
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        { 

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("Login.aspx");

        }
      


    }
}