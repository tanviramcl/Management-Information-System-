using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationMIS.Models;
using AMCLBL;
using System.Collections;
using System.Globalization;

namespace WebApplicationMIS.UI
{
    public partial class Service_Resister : System.Web.UI.Page
    {
        CommonGateway commonGatewayObj = new CommonGateway();

        DropDownList dropDownListObj = new DropDownList();
        //SupportedDocumentFunction supportedDoc = new SupportedDocumentFunction();
        // ServiceResisterfunction serviceresiter = new ServiceResisterfunction();
        ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();


        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable ServiceNameDropDownList = dropDownListObj.ServiceNameDropDownList();
            DataTable subServiceNameDropDownList = dropDownListObj.SUBServiceNameDropDownList();
            //  DataTable funddropdownlist = dropDownListObj.FundNameDropDownList();
            DataTable branchdropdownlist = dropDownListObj.BRANCHNameDropDownList();
            //DataTable dtSupportedDocument = supportedDoc.GetSupportedDocument();
            DataTable dtresiterID = complainResisterfunction.Get_Complain_resiterID();
            //   DataTable dtServiceResisterByID = serviceresiter.Get_DuplicateresiterID
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    inputselectservicedropdownList.DataSource = ServiceNameDropDownList;
                    inputselectservicedropdownList.DataTextField = "SERVICE_NAME";
                    inputselectservicedropdownList.DataValueField = "SERVICE_ID";
                    inputselectservicedropdownList.DataBind();

                    inputselectsubservicedropdownList.DataSource = subServiceNameDropDownList;
                    inputselectsubservicedropdownList.DataTextField = "SERVICE_SUB_NAME";
                    inputselectsubservicedropdownList.DataValueField = "SERVICE_SUB_ID";
                    inputselectsubservicedropdownList.DataBind();




                    inputselectBRANCHDropDownList.DataSource = branchdropdownlist;
                    inputselectBRANCHDropDownList.DataTextField = "BRANCH_NAME";
                    inputselectBRANCHDropDownList.DataValueField = "BRANCH_ID";
                    inputselectBRANCHDropDownList.DataBind();


                    string id = dtresiterID.Rows[0]["COMPLAIN_ID"].ToString();
                    TextBox_RESISTER_iD.Text = id;


                        //if (dtSupportedDocument.Rows.Count > 0)
                        //{
                        //    chkSupportedDocument.DataSource = dtSupportedDocument;
                        //    chkSupportedDocument.DataValueField = "SUP_DOC_ID";
                        //    chkSupportedDocument.DataTextField = "SUP_DOC_NAME";

                        //    chkSupportedDocument.DataBind();
                        //}
                    string Resister_id = Request.QueryString["ID"];

                    if (string.IsNullOrEmpty(Resister_id))
                    {
                        string reg_id = "";
                        //string SUP_DOC_REQ_ID = "";
                        if (!string.IsNullOrEmpty(dtresiterID.Rows[0]["COMPLAIN_ID"].ToString()))
                        {
                            reg_id = dtresiterID.Rows[0]["COMPLAIN_ID"].ToString();
                            TextBox_RESISTER_iD.Text = reg_id;


                        }
                        else
                        {
                            TextBox_RESISTER_iD.Text = "1";
                        }
                        //    }
                        ButtonSubmitt.Text = "Save";
                    }
                    else
                    {
                        ButtonSubmitt.Text = "Update";
                        TextBox_RESISTER_iD.Visible = true;
                        TextBox_RESISTER_iD.Text = Resister_id.ToString();
                        DataTable dtCompalinResisterByID = complainResisterfunction.Get_Complain_ResiterByID(Resister_id);
                        if (dtCompalinResisterByID != null && dtCompalinResisterByID.Rows.Count > 0)
                        {
                            string service_id = dtCompalinResisterByID.Rows[0]["SERVICE_ID"].ToString();
                            if (service_id != "")
                            {
                                DataTable SubServiceropdownlist = dropDownListObj.SUBServiceNameDropDownList(service_id);

                                if (SubServiceropdownlist != null && SubServiceropdownlist.Rows.Count > 0)
                                {

                                    inputselectsubservicedropdownList.DataSource = SubServiceropdownlist;
                                    inputselectsubservicedropdownList.DataTextField = "SERVICE_SUB_NAME";
                                    inputselectsubservicedropdownList.DataValueField = "SERVICE_SUB_ID";
                                    inputselectsubservicedropdownList.DataBind();

                                    inputselectservicedropdownList.SelectedValue = dtCompalinResisterByID.Rows[0]["SERVICE_ID"].ToString();
                                    inputselectsubservicedropdownList.SelectedValue = dtCompalinResisterByID.Rows[0]["SERVICE_SUB_ID"].ToString();
                                    inputselectBRANCHDropDownList.SelectedValue = dtCompalinResisterByID.Rows[0]["BRANCH_ID"].ToString();

                                    DropDownListUrgency.SelectedValue = dtCompalinResisterByID.Rows[0]["URGENCY"].ToString();
                                }
                            }
                           


                            DropDownListComplain.SelectedValue = dtCompalinResisterByID.Rows[0]["COMPLAIN_TYPE_ID"].ToString();
                            TextBoxaComplainSubect.Text = dtCompalinResisterByID.Rows[0]["COMPLAIN_SUBJECT"].ToString();
                            TextareaComplainDetails.Value = dtCompalinResisterByID.Rows[0]["COMPLAIN_DETAILS"].ToString();
                            TextareaRemarks.Value = dtCompalinResisterByID.Rows[0]["REMARKS"].ToString();
                            DropDownListStatus.Visible = true;
                            DateTime dtimeComplainDate = Convert.ToDateTime(dtCompalinResisterByID.Rows[0]["COMPLIAN_DATE"].ToString());
                            complaindateDateTextBox.Text = dtimeComplainDate.ToString("dd/MM/yyyy");

                        }
                    }
   
                }
            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            // System.Threading.Thread.Sleep(5000);
        }
        protected void TextBox_COmPLAIN_ID_TextChanged(object sender, EventArgs e)
        {

            DataTable dtcomplainesisterByID = complainResisterfunction.Get_Complain_ResiterByID(TextBox_RESISTER_iD.Text.ToString());
            if (dtcomplainesisterByID != null && dtcomplainesisterByID.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Duplicate Complain Id not allow)", true);
            }
          

        }
   

        protected void ResistrationTextBox_TextChanged(object sender, EventArgs e)
        {
            
            
           
        }
       
      

        protected void ServiceDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ServiceId = inputselectservicedropdownList.SelectedValue;

            DataTable SubServiceropdownlist = dropDownListObj.SUBServiceNameDropDownList(ServiceId);
            inputselectsubservicedropdownList.DataSource = SubServiceropdownlist;
            inputselectsubservicedropdownList.DataTextField = "SERVICE_SUB_NAME";
            inputselectsubservicedropdownList.DataValueField = "SERVICE_SUB_ID";
            inputselectsubservicedropdownList.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
        protected void Button1_Click_clear(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
        public void clear()
        {
            //chkSupportedDocument = null;
        }
      

        [System.Web.Services.WebMethod]

        public static string InsertUpdateComplainResister(string Complainesistationmdl)
        {
            string msg = "";
            
            string strComplaindate;
            DateTime? dtcomplainDate;
            ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();
            
            Hashtable htcomplainResister = new Hashtable();
        
            DataTable dtcomplainResisterlist = null;



            List<ComplainResister> newUsers = new List<ComplainResister>();
            List<ComplainResister> mdl = new JavaScriptSerializer().Deserialize<List<ComplainResister>>(Complainesistationmdl);



            foreach (ComplainResister ComplainResiter in mdl)
            {


                if (!string.IsNullOrEmpty(ComplainResiter.ComplainDate))
                {
                    // dtcomplainDate = Convert.ToDateTime(serviceResiter.ComplainDate);
                    DateTime date = DateTime.ParseExact(ComplainResiter.ComplainDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    strComplaindate = date.ToString("dd-MMM-yyyy");
                }
                else
                {
                    dtcomplainDate = null;
                    strComplaindate = "";
                }

                string userName = (string)HttpContext.Current.Session["UserName"];

              //  htcomplainResister.Add("Complain_ID", ComplainResiter.Complain_ID.ToString());
                htcomplainResister.Add("SERVICE_ID", ComplainResiter.SERVICE_ID);
                htcomplainResister.Add("SERVICE_SUB_ID", ComplainResiter.SERVICE_SUB_ID);
                htcomplainResister.Add("COMPLAIN_TYPE_ID", ComplainResiter.Complain_Type);
                htcomplainResister.Add("COMPLAIN_SUBJECT", ComplainResiter.COMPLAIN_SUBJECT);
                htcomplainResister.Add("COMPLAIN_DETAILS", ComplainResiter.COMPLAIN_DETAILS);
                htcomplainResister.Add("REMARKS", ComplainResiter.REMARKS.ToString());
                htcomplainResister.Add("BRANCH_ID", ComplainResiter.BRANCH_Code.ToString());
                htcomplainResister.Add("COMPLIAN_DATE", strComplaindate);
                htcomplainResister.Add("URGENCY", ComplainResiter.Urgency.ToString());
                htcomplainResister.Add("ENTRY_DATETIME", DateTime.Now.ToString());
                if (ComplainResiter.STATUS == "I")
                {
                    // status = "Processing";
                    htcomplainResister.Add("STATUS", "Processing");
                }
                else if (ComplainResiter.STATUS == "C")
                {
                    htcomplainResister.Add("STATUS", "Close");
                }
                else
                {
                    htcomplainResister.Add("STATUS", "Primary");
                }

                if (!string.IsNullOrEmpty(userName))
                {
                    htcomplainResister.Add("USER_NAME", userName.ToString());
                }
            }

            foreach (var item in mdl)
            {

                dtcomplainResisterlist = complainResisterfunction.Get_DuplicateresiterID(item.Complain_ID);

            }

            if (dtcomplainResisterlist != null && dtcomplainResisterlist.Rows.Count > 0)
            {

                complainResisterfunction.UpdateComplainResiter(htcomplainResister, mdl[0].Complain_ID);
                msg = "Update Sucessfully";
            }
            else
            {

                complainResisterfunction.SaveComplainResiter(htcomplainResister, mdl[0].Complain_ID);
                msg = "Save Sucessfully";

            }



            return msg;

        }

    }
}