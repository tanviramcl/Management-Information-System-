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
    public partial class SupportedDocuments : System.Web.UI.Page
    {
        ServiceResisterfunction serviceresiterall = new ServiceResisterfunction();
        protected void Page_Load(object sender, EventArgs e)
        {
                Session["AllSupportedDucuments"] = serviceresiterall.ALLGet_SupportedDocumentList();

               DataTable dtallserviceresiter = (DataTable)Session["AllSupportedDucuments"];

        }
        
    }
}