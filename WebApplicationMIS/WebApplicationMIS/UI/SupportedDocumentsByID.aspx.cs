using AMCLBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationMIS.Models;

namespace WebApplicationMIS.UI
{
    public partial class SupportedDocumentsByID : System.Web.UI.Page
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
                DataTable dtallSupportedDoc = (System.Data.DataTable)Session["AllSupportedDucuments"];
                if (dtallSupportedDoc.Rows.Count > 0)
                {
                    for (int i = 0; i < dtallSupportedDoc.Rows.Count; i++)
                    {
                        TextBoxBRESISTER_ID.Text = dtallSupportedDoc.Rows[i]["RESISTER_ID"].ToString();
                        TextBoxSUP_DOC_ID.Text = dtallSupportedDoc.Rows[i]["SUP_DOC_ID"].ToString();
                        TextBoxBO_COPY.Text = dtallSupportedDoc.Rows[i]["BO_COPY"].ToString();
                        TextBoxNID_COPY.Text= dtallSupportedDoc.Rows[i]["NID_COPY"].ToString();
                        TextBoxE_TIN_COPY.Text = dtallSupportedDoc.Rows[i]["E_TIN_COPY"].ToString();
                        TextBoxAPPLICANT_PIC.Text = dtallSupportedDoc.Rows[i]["APPLICANT_PIC"].ToString();
                        TextBoxNOMINEE_PIC.Text = dtallSupportedDoc.Rows[i]["NOMINEE_PIC"].ToString();
                        TextBoxCHECK_BOOK.Text = dtallSupportedDoc.Rows[i]["CHECK_BOOK"].ToString();
                        TextBoxUTILITY_BILL.Text = dtallSupportedDoc.Rows[i]["UTILITY_BILL"].ToString();
                        TextBoxORIGINAL_CERT.Text = dtallSupportedDoc.Rows[i]["ORIGINAL_CERT"].ToString();
                        TextBoxSIG_SCREEN_PRINT.Text = dtallSupportedDoc.Rows[i]["SIG_SCREEN_PRINT"].ToString();
                        TextBoxALLOTMENT_LETTER.Text = dtallSupportedDoc.Rows[i]["ALLOTMENT_LETTER"].ToString();
                        TextBoxAFFIDAVIT.Text = dtallSupportedDoc.Rows[i]["AFFIDAVIT"].ToString();
                        TextBoxGD.Text = dtallSupportedDoc.Rows[i]["GD"].ToString();
                        TextBoxINDEMNITY_BOND.Text = dtallSupportedDoc.Rows[i]["INDEMNITY_BOND"].ToString();
                        TextBoxPAPER_ADD.Text = dtallSupportedDoc.Rows[i]["PAPER_ADD"].ToString();
                        TextBoxDIVIDEND_NOTICE.Text = dtallSupportedDoc.Rows[i]["DIVIDEND_NOTICE"].ToString();
                        TextBoxDEATH_CERTIFICATES.Text = dtallSupportedDoc.Rows[i]["DEATH_CERTIFICATES"].ToString();
                        TextBoxSUGG_HIGHCOURT.Text = dtallSupportedDoc.Rows[i]["SUGG_HIGHCOURT"].ToString();
                        TextBoxPOWER_ATTORNEY.Text = dtallSupportedDoc.Rows[i]["POWER_ATTORNEY"].ToString();
                        TextBoxCERT_CEMENTRY.Text = dtallSupportedDoc.Rows[i]["CERT_CEMENTRY"].ToString();
                    }
                }
            }

        }

        [System.Web.Services.WebMethod]

        public static string UpdateSupportedDocument(string SupportedDocumentmdl)
        {
            string msg, supp_doc_id = "";
        

            SupportedDocumentFunction supportedDoc = new SupportedDocumentFunction();
            ServiceResisterfunction serviceresiste = new ServiceResisterfunction();
            Hashtable htServiceReiter = new Hashtable();
            Hashtable htSupportedDocument = new Hashtable();
            List<SupportedDocument> mdl = new JavaScriptSerializer().Deserialize<List<SupportedDocument>>(SupportedDocumentmdl);
            foreach (SupportedDocument supportedDocument in mdl)
            {
                htServiceReiter.Add("RESISTER_ID", supportedDocument.RESISTER_ID.ToString());
                htServiceReiter.Add("BO_COPY", supportedDocument.BO_COPY.ToString());
                htServiceReiter.Add("NID_COPY", supportedDocument.NID_COPY);
                htServiceReiter.Add("E_TIN_COPY", supportedDocument.E_TIN_COPY);
                htServiceReiter.Add("APPLICANT_PIC", supportedDocument.APPLICANT_PIC);
                htServiceReiter.Add("NOMINEE_PIC", supportedDocument.NOMINEE_PIC.ToString());
                htServiceReiter.Add("CHECK_BOOK", supportedDocument.CHECK_BOOK.ToString());
                htServiceReiter.Add("UTILITY_BILL", supportedDocument.UTILITY_BILL.ToString());
                htServiceReiter.Add("ORIGINAL_CERT", supportedDocument.ORIGINAL_CERT.ToString());
                htServiceReiter.Add("SIG_SCREEN_PRINT", supportedDocument.SIG_SCREEN_PRINT.ToString());
                htServiceReiter.Add("ALLOTMENT_LETTER", supportedDocument.ALLOTMENT_LETTER.ToString());
                htServiceReiter.Add("GD", supportedDocument.GD.ToString());
                htServiceReiter.Add("INDEMNITY_BOND", supportedDocument.INDEMNITY_BOND.ToString());
                htServiceReiter.Add("PAPER_ADD", supportedDocument.PAPER_ADD.ToString());
                htServiceReiter.Add("DIVIDEND_NOTICE", supportedDocument.DIVIDEND_NOTICE.ToString());
                htServiceReiter.Add("DEATH_CERTIFICATES", supportedDocument.DEATH_CERTIFICATES.ToString());
                htServiceReiter.Add("SUGG_HIGHCOURT", supportedDocument.SUGG_HIGHCOURT.ToString());
                htServiceReiter.Add("POWER_ATTORNEY", supportedDocument.POWER_ATTORNEY.ToString());
                htServiceReiter.Add("CERT_CEMENTRY", supportedDocument.CERT_CEMENTRY.ToString());

          
            }

            DataTable dtDocID = serviceresiste.Get_IDbySupportedDocID(mdl[0].SUP_DOC_ID);
            if (dtDocID != null && dtDocID.Rows.Count > 0)
            {
                supp_doc_id = dtDocID.Rows[0]["ID"].ToString();
            }

            supportedDoc.UpdateSupportedDocument(htServiceReiter, supp_doc_id);
            msg = "Update Sucessfully";

            return msg;

        }

    }
}