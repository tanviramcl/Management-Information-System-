using AMCLBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS
{
    public partial class BranchList : System.Web.UI.Page
    {
        BranchManagment barnchmanagment = new BranchManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BranchList"] = barnchmanagment.ALLBRANCH_INFO();

            DataTable dtAll_Department = (DataTable)Session["BranchList"];
        }
    }
}