using AMCLBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class AssignComplainToEmployee : System.Web.UI.Page
    {
        ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["AllComplainResister"] = complainResisterfunction.Get_Complain_Resiter();

            DataTable dtallcomplainresiter = (DataTable)Session["AllComplainResister"];

        }
    }
}