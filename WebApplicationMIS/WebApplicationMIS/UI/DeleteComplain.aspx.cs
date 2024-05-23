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
    public partial class DeleteComplain : System.Web.UI.Page
    {
        DropDownList dropDownListObj = new DropDownList();
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable Emplist = dropDownListObj.Get_EmployeeList();
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

                    string Resister_id = Request.QueryString["ID"];
                   

                    if (string.IsNullOrEmpty(Resister_id))
                    {
                        Resister_id = "0";
                    }
                    else
                    {
                  

                        DataTable dtCompalinResisterByID = complainResisterfunction.Get_Complain_ResiterByID(Resister_id);
                        inputselectCompalinDropDownList.SelectedValue = dtCompalinResisterByID.Rows[0]["COMPLAIN_ID"].ToString();
         
                        if (dtCompalinResisterByID != null && dtCompalinResisterByID.Rows.Count > 0)
                        {
                            TextBoxaComplainSubect.Text = dtCompalinResisterByID.Rows[0]["COMPLAIN_SUBJECT"].ToString();
                            TextareaComplainDetails.Value = dtCompalinResisterByID.Rows[0]["COMPLAIN_DETAILS"].ToString();
                        }
                    }


                    

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

        public static string DeleteComplainList(string Complainesistationmdl)
        {
            string msg = "";

          
            string id = "";
            ComplainResisterFunction complainResisterfunction = new ComplainResisterFunction();
            CommonGateway commonGatewayObj = new CommonGateway();
            Hashtable htcomplainResister = new Hashtable();

           



            List<CompalinAction> newUsers = new List<CompalinAction>();
            List<CompalinAction> mdl = new JavaScriptSerializer().Deserialize<List<CompalinAction>>(Complainesistationmdl);



            foreach (CompalinAction ComplainResiter in mdl)
            {


                string strDelQuery = "delete from COMPLAIN_REGISTER where COMPLAIN_ID='" + ComplainResiter.COMPLAIN_ID + "'";
                int NumOfRows = commonGatewayObj.ExecuteNonQuery(strDelQuery);
                commonGatewayObj.CommitTransaction();
                if (NumOfRows > 0)
                {
                    msg = " Delete Sucessfully";
                }

            }
      
            return msg;

        }
    }
}