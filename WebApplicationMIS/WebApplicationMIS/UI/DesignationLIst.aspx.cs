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
    public partial class DesignationLIst : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["All_DESIGNATION"] = emplyeeManagment.Get_All_DESIGNATION();

            DataTable dtAll_DESIGNATION = (DataTable)Session["All_DESIGNATION"];
        }
    }
}