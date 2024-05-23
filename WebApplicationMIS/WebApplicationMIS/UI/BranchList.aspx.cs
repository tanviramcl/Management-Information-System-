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
    public partial class BranchList : System.Web.UI.Page
    {
        BranchManagment branchMng = new BranchManagment();
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["ALLBRANCHINFO"] = branchMng.ALLBRANCH_INFO();

            DataTable dtallEmployee = (DataTable)Session["ALLBRANCHINFO"];
        }
    }
}