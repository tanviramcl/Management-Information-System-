using AMCLBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationMIS.Models;

namespace WebApplicationMIS.UI
{
    public partial class CloseComplain : System.Web.UI.Page
    {
        DropDownList dropDownListObj = new DropDownList();
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable Emplist = dropDownListObj.Get_EmployeeList();
            //DataTable complainlist = dropDownListObj.Get_ComplainCloseList();
            DataTable complainlist = dropDownListObj.Get_ComplainList();
            DataTable dtCompalinActionID = complainResisterfunction.COMPLAIN_ACTION_ID();

            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {




                    inputselectCompalinDropDownList.DataSource = complainlist;
                    inputselectCompalinDropDownList.DataTextField = "COMPLAIN_ID";
                    inputselectCompalinDropDownList.DataValueField = "COMPLAIN_ID";
                    inputselectCompalinDropDownList.DataBind();


                }
            }
        }



        protected void ComplainDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string complain_Id = inputselectCompalinDropDownList.SelectedValue.ToString();
            DataTable dtCompalinResisterByID = complainResisterfunction.Get_Complain_ResiterByID(complain_Id);
            if (dtCompalinResisterByID != null && dtCompalinResisterByID.Rows.Count > 0)
            {
                TextBoxaComplainSubect.Text = dtCompalinResisterByID.Rows[0]["COMPLAIN_SUBJECT"].ToString();
                TextareaComplainDetails.Value = dtCompalinResisterByID.Rows[0]["COMPLAIN_DETAILS"].ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }

        [System.Web.Services.WebMethod]

        public static string CloseComplainToAssign(string Complainesistationmdl)
        {
            string msg = "";

            string strClose_DATE;
            DateTime? dtstrClose_DATE;
            string id = "";
            ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();
            CommonGateway commonGatewayObj = new CommonGateway();
            Hashtable htcomplainResister = new Hashtable();

            DataTable dtcomplainResisterlist = null;



            List<CompalinAction> newUsers = new List<CompalinAction>();
            List<CompalinAction> mdl = new JavaScriptSerializer().Deserialize<List<CompalinAction>>(Complainesistationmdl);



            foreach (CompalinAction ComplainResiter in mdl)
            {


                if (!string.IsNullOrEmpty(ComplainResiter.CLOSE_DATE))
                {
                    // dtcomplainDate = Convert.ToDateTime(serviceResiter.ComplainDate);
                    DateTime date = DateTime.ParseExact(ComplainResiter.CLOSE_DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    strClose_DATE = date.ToString("dd-MMM-yyyy");
                }
                else
                {
                    dtstrClose_DATE = null;
                    strClose_DATE = "";
                }

                string userName = (string)HttpContext.Current.Session["UserName"];



                htcomplainResister.Add("COMPLAIN_ID", ComplainResiter.COMPLAIN_ID);
                String status="";
                if (ComplainResiter.STATUS == "I")
                {
                    // status = "Processing";
                    htcomplainResister.Add("STATUS", "Processing");
                    status = "Processing";
                }
                else if (ComplainResiter.STATUS == "C")
                {
                    htcomplainResister.Add("STATUS", "Close");
                    status = "Close";
                }
                else
                {
                    htcomplainResister.Add("STATUS", "Primary");
                    status = "Primary";
                }
             
                htcomplainResister.Add("CLOSE_DATE", strClose_DATE);

                string strUpdateStatus = "update COMPLAIN_REGISTER set STATUS ='"+ status + "' where COMPLAIN_ID =" + ComplainResiter.COMPLAIN_ID;
                int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateStatus);
                commonGatewayObj.CommitTransaction();
                string strUpdate = "update COMPLAIN_ACTION set STATUS ='" + status + "' ,CLOSE_DATE='" + strClose_DATE + "' where COMPLAIN_ID =" + ComplainResiter.COMPLAIN_ID;
                int noCOMPLAIN_ACTION = commonGatewayObj.ExecuteNonQuery(strUpdate);
                commonGatewayObj.CommitTransaction();

            }

            msg = "Update Sucessfully";



            return msg;

        }
    }
}