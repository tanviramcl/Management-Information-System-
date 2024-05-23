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
    public partial class EmloyeeAssigntoLogin : System.Web.UI.Page
    {
        DropDownList dropDownListObj = new DropDownList();
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable Emplist = dropDownListObj.Get_EmployeeList();
            DataTable DepartmntList = dropDownListObj.Get_DepsrtmentList();
            DataTable Dagignationlist = dropDownListObj.Get_DESIGNATIONList();
            DataTable dtEmpID = emplyeeManagment.Get_Employee_ID();
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    inputEmployeeNamedropdownList.DataSource = Emplist;
                    inputEmployeeNamedropdownList.DataTextField = "EMPLOYEE_NAME";
                    inputEmployeeNamedropdownList.DataValueField = "EMP_ID";
                    inputEmployeeNamedropdownList.DataBind();

                    inputselectDepartmentDropDownList.DataSource = DepartmntList;
                    inputselectDepartmentDropDownList.DataTextField = "DEPARTMENT_NAME";
                    inputselectDepartmentDropDownList.DataValueField = "DEPARTMENT_ID";
                    inputselectDepartmentDropDownList.DataBind();

                    DropDownListDESIGNATION.DataSource = Dagignationlist;
                    DropDownListDESIGNATION.DataTextField = "DESIGNATION_NAME";
                    DropDownListDESIGNATION.DataValueField = "DESIGNATION_ID";
                    DropDownListDESIGNATION.DataBind();


                }
            }
        }

        protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Employee_Id = inputEmployeeNamedropdownList.SelectedValue.ToString();
            DataTable dtGetallEmp = emplyeeManagment.Get_Employee_ByID(Employee_Id);
            if (dtGetallEmp != null && dtGetallEmp.Rows.Count > 0)
            {
                inputselectDepartmentDropDownList.SelectedValue = dtGetallEmp.Rows[0]["DEPARTMENT_ID"].ToString();
                DropDownListDESIGNATION.SelectedValue = dtGetallEmp.Rows[0]["DESIGNATION_ID"].ToString();
            }
        }
        [System.Web.Services.WebMethod]

        public static string InsertUSER(string Employeeresistationmdl)
        {
            string msg = "";

            DataTable dtUserlistbyid = null;
            CommonGateway commonGatewayObj = new CommonGateway();
            EmployeeManagment emplyeeManagment = new EmployeeManagment();
            Hashtable htUser = new Hashtable();
            List<User> newUsers = new List<User>();
            List<User> mdl = new JavaScriptSerializer().Deserialize<List<User>>(Employeeresistationmdl);
            foreach (User user in mdl)
            {



                string userName = (string)HttpContext.Current.Session["UserName"];

                htUser.Add("EMP_ID", user.EMP_ID.ToString());
                htUser.Add("USER_ID", user.USER_ID);
                htUser.Add("USER_NM", user.USER_NM);
                htUser.Add("USER_LEVEL", user.USER_LEVEL);

                string encr = null;
                string str = user.USER_PASS.ToString();
                if (str == "")
                {
                    return "ÄÆ8¼";
                }
                for (int i = 0; i < str.Length; i++)
                {
                    char ch = Convert.ToChar(str.Substring(i, 1));
                    int num = ch + 169;
                    encr += ((char)(ushort)num).ToString();
                }
                htUser.Add("USER_PASS", encr);


                foreach (var item in mdl)
                {

                    dtUserlistbyid = emplyeeManagment.Get_USER_ByID(user.USER_NM.ToString());

                }
          

            if (dtUserlistbyid != null && dtUserlistbyid.Rows.Count > 0)
            {
                DataTable dtUserInfo = new DataTable();
                dtUserInfo = commonGatewayObj.Select("SELECT * FROM USER_INFO WHERE USER_ID='" + user.USER_ID + "' AND USER_NM='" + user.USER_NM + "' ");
                    if (dtUserInfo.Rows.Count > 0)
                    {
                        msg = "Duplicate user Found";
                    }
                    else
                    {
                        emplyeeManagment.SaveUser(htUser);
                        msg = "Save Sucessfully";
                    }
            }
            else
            {
                msg = " User Not found";


            }
            }



            return msg;

        }
    }
}