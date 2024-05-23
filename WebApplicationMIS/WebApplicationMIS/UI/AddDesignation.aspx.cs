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
    public partial class AddDesignation1 : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            DataTable dtGET_DESIGNATION_ID = emplyeeManagment.GET_DESIGNATION_ID();
            string DESIGNATION_ID = dtGET_DESIGNATION_ID.Rows[0]["DESIGNATION_ID"].ToString();
            TextBox_Designation_iD.Text = DESIGNATION_ID;


        }

        [System.Web.Services.WebMethod]

        public static string InsertUpadateDesignation(string Designationmdl)
        {
            string msg = "";
            EmployeeManagment emplyeeManagment = new EmployeeManagment();
            DataTable dtdesignationyid = null;

         
            Hashtable htdtdesignationy = new Hashtable();
            List<DesignationClass> lstdesignation = new List<DesignationClass>();
            List<DesignationClass> mdl = new JavaScriptSerializer().Deserialize<List<DesignationClass>>(Designationmdl);
            foreach (DesignationClass deg in mdl)
            {

                htdtdesignationy.Add("DESIGNATION_ID", deg.Designation_ID.ToString());
                htdtdesignationy.Add("DESIGNATION_NAME", deg.Designation_Name.ToString());


            }
            foreach (var item in mdl)
            {

                dtdesignationyid = emplyeeManagment.Get_DESIGNATION__ByID(item.Designation_ID.ToString());

            }

            if (dtdesignationyid != null && dtdesignationyid.Rows.Count > 0)
            {

                // complainResisterfunction.UpdateComplainResiter(htcomplainResister, mdl[0].Complain_ID);
                emplyeeManagment.UpdateDESIGNATION(htdtdesignationy, mdl[0].Designation_ID);
                msg = "Update Sucessfully";
            }
            else
            {

                emplyeeManagment.SaveDESIGNATION(htdtdesignationy);
                msg = "Save Sucessfully";

            }




            return msg;

        }
    }
}