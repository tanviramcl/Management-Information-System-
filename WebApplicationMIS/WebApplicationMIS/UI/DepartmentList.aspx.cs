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
    public partial class DepartmentList : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["DepartmentList"] = emplyeeManagment.Get_DepsrtmentList();

            DataTable dtAll_Department = (DataTable)Session["DepartmentList"];
        }
    }
}