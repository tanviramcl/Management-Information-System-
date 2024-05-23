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
    public partial class SupportedDocuments1 : System.Web.UI.Page
    {
        ServiceResisterfunction serviceresiteralla = new ServiceResisterfunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }

            string Supported_doc_id = Request.QueryString["ID"];

            if (string.IsNullOrEmpty(Supported_doc_id))
            {
                Session["AllSupportedDucuments"] = serviceresiteralla.ALLGet_SupportedDocumentList();

                DataTable dtallserviceresiter = (DataTable)Session["AllSupportedDucuments"];

            }
            else
            {
                Session["AllSupportedDucuments"] = serviceresiteralla.Get_SupportedDocumentListById(Supported_doc_id);

                DataTable dtallserviceresiter = (DataTable)Session["AllSupportedDucuments"];

            }


           
        }
    }
}