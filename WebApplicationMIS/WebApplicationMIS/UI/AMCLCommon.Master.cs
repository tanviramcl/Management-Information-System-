using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class AMCLCommon : System.Web.UI.MasterPage
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        protected void Page_Load(object sender, EventArgs e)
        {
            string referer = Request.ServerVariables["HTTP_REFERER"];

            string UserType = Session["UserType"].ToString();


            Session["UserType"] = UserType;
            if (string.IsNullOrEmpty(referer))
            {
                Session["UserID"] = null;
                Response.Redirect("../Default.aspx");
            }

        }
    }
}