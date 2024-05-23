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
    public partial class ALLEmployee : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AllEmployee"] = emplyeeManagment.Get_All_employee();

            DataTable dtallEmployee = (DataTable)Session["AllEmployee"];
        }
        protected void SendEmailButton_Click(object sender, EventArgs e)
        {

        }
    }

}