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
    public partial class DepartmentWebForm : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            DataTable dtGET_Department_ID = emplyeeManagment.GET_Department_ID();
            string Department_ID = dtGET_Department_ID.Rows[0]["Department_ID"].ToString();
         //   TextBox_Department_iD.Text = Department_ID;

        }

        [System.Web.Services.WebMethod]

        public static string InsertUpadateDEPARTMENT(string Departmentnmdl)
        {
            string msg = "";
            EmployeeManagment emplyeeManagment = new EmployeeManagment();
            DataTable dtDEPARTMENT_IDid = null;


            Hashtable htdtdept = new Hashtable();
            List<Department> lstdesignation = new List<Department>();
            List<Department> mdl = new JavaScriptSerializer().Deserialize<List<Department>>(Departmentnmdl);
            foreach (Department dept in mdl)
            {

                htdtdept.Add("DEPARTMENT_ID", dept.Department_ID.ToString());
                htdtdept.Add("DEPARTMENT_NAME", dept.Department_Name.ToString());


            }
            foreach (var item in mdl)
            {

                dtDEPARTMENT_IDid = emplyeeManagment.Get_Department__ByID(item.Department_ID.ToString());

            }

            if (dtDEPARTMENT_IDid != null && dtDEPARTMENT_IDid.Rows.Count > 0)
            {

                // complainResisterfunction.UpdateComplainResiter(htcomplainResister, mdl[0].Complain_ID);
                emplyeeManagment.UpdateDepartment(htdtdept, mdl[0].Department_ID);
                msg = "Update Sucessfully";
            }
            else
            {

                emplyeeManagment.SaveDepartment(htdtdept);
                msg = "Save Sucessfully";

            }




            return msg;

        }
    }
}