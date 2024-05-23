using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMCLBL;
using WebApplicationMIS.Models;
using System.Web.Script.Serialization;
using System.Collections;

namespace WebApplicationMIS.UI
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        DropDownList dropDownListObj = new DropDownList();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable DepartmntList = dropDownListObj.Get_DepsrtmentList();
            DataTable Dagignationlist = dropDownListObj.Get_DESIGNATIONList();
            DataTable dtEmpID = emplyeeManagment.Get_Employee_ID();
            DataTable dtEmpALL;
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    inputselectDepartmentDropDownList.DataSource = DepartmntList;
                    inputselectDepartmentDropDownList.DataTextField = "DEPARTMENT_NAME";
                    inputselectDepartmentDropDownList.DataValueField = "DEPARTMENT_ID";
                    inputselectDepartmentDropDownList.DataBind();

                    DropDownListDESIGNATION.DataSource = Dagignationlist;
                    DropDownListDESIGNATION.DataTextField = "DESIGNATION_NAME";
                    DropDownListDESIGNATION.DataValueField = "DESIGNATION_ID";
                    DropDownListDESIGNATION.DataBind();

                    string Emp_id = dtEmpID.Rows[0]["EMP_ID"].ToString();
                    TextBox_Employee_iD.Text = Emp_id;

                    string QueryEmp_id = Request.QueryString["ID"];
                    if (string.IsNullOrEmpty(QueryEmp_id))
                    {
                        string e_id = "";
                        //string SUP_DOC_REQ_ID = "";
                        if (!string.IsNullOrEmpty(dtEmpID.Rows[0]["EMP_ID"].ToString()))
                        {
                            e_id = dtEmpID.Rows[0]["EMP_ID"].ToString();
                            TextBox_Employee_iD.Text = e_id;


                        }
                        else
                        {
                            TextBox_Employee_iD.Text = "1";
                        }
                        //    }
                        ButtonSubmitt.Text = "Save";
                    }
                    else
                    {
                        ButtonSubmitt.Text = "Update";
                        TextBox_Employee_iD.Visible = true;
                        TextBox_Employee_iD.Text = QueryEmp_id.ToString();
                        // DataTable dtCompalinResisterByID = complainResisterfunction.Get_Complain_ResiterByID(Resister_id);
                        DataTable dtGetallEmp = emplyeeManagment.Get_Employee_ByID(QueryEmp_id);
                        if (dtGetallEmp != null && dtGetallEmp.Rows.Count > 0)
                        {
                            txtFirstName.Text = dtGetallEmp.Rows[0]["EMPLOYEE_NAME"].ToString();
                            TextBoxNIDNO.Text = dtGetallEmp.Rows[0]["NID"].ToString();
                            inputEmailTextBox.Text = dtGetallEmp.Rows[0]["EMAIL"].ToString();
                            DateTime dtimeJoinDate = Convert.ToDateTime(dtGetallEmp.Rows[0]["JDATE"].ToString());
                            JoinDateTextBox.Text = dtimeJoinDate.ToString("dd/MM/yyyy");
                            DropDownListDESIGNATION.SelectedValue = dtGetallEmp.Rows[0]["DESIGNATION_ID"].ToString();
                            inputselectDepartmentDropDownList.SelectedValue = dtGetallEmp.Rows[0]["DEPARTMENT_ID"].ToString();
                            AddressTextArea1.Value = dtGetallEmp.Rows[0]["ADDR"].ToString();
                            PermanentTextarea.Value = dtGetallEmp.Rows[0]["ADDR2"].ToString();
                        }
                    }



                }
            }
        }
        protected void TextBox_Employee_ID_TextChanged(object sender, EventArgs e)
        {

            //DataTable dtServiceResisterByID = serviceresiter.Get_Service_ResiterByID(TextBox_RESISTER_iD.Text.ToString());
            //if (dtServiceResisterByID != null && dtServiceResisterByID.Rows.Count > 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "TScript", "alert('Duplicate Complain Id not allow)", true);
            //}


        }

        [System.Web.Services.WebMethod]

        public static string InsertUpadateEmployee(string Employeeresistationmdl)
        {
            string msg = "";

            DataTable dtEmployeelistbyid = null;

            EmployeeManagment emplyeeManagment = new EmployeeManagment();
            Hashtable htemployee = new Hashtable();
            List<EmpLoyee> newUsers = new List<EmpLoyee>();
            List<EmpLoyee> mdl = new JavaScriptSerializer().Deserialize<List<EmpLoyee>>(Employeeresistationmdl);
            foreach (EmpLoyee employee in mdl)
            {

                //if (!string.IsNullOrEmpty(employee.JDATE))
                //{
                //    dtJoinDate = Convert.ToDateTime(employee.JDATE);

                //    strJoindate = dtJoinDate.Value.ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    dtJoinDate = null;
                //    strJoindate = "";
                //}
                DateTime date1 = DateTime.ParseExact(employee.JDATE, "dd/MM/yyyy", null);



                string strJoindate = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
                string userName = (string)HttpContext.Current.Session["UserName"];

                htemployee.Add("EMP_ID", employee.EMP_ID.ToString());
                htemployee.Add("EMPLOYEE_NAME", employee.EMPLOYEE_NAME);
                htemployee.Add("ADDR", employee.ADDR);
                htemployee.Add("ADDR2", employee.ADDR2.ToString());
                htemployee.Add("JDATE", strJoindate);
                htemployee.Add("CONTRACT_NO", employee.CONTRACT_NO.ToString());
                htemployee.Add("NID", employee.NID.ToString());
                htemployee.Add("EMAIL", employee.EMAIL.ToString());
                htemployee.Add("DEPARTMENT_ID", employee.DEPARTMENT_ID.ToString());
                htemployee.Add("DESIGNATION_ID", employee.DESIGNATION_ID.ToString());


                if (!string.IsNullOrEmpty(userName))
                {
                    htemployee.Add("ENTRY_BY", userName.ToString());
                }
            }
            foreach (var item in mdl)
            {

                dtEmployeelistbyid = emplyeeManagment.Get_Employee_ByID(item.EMP_ID.ToString());

            }

            if (dtEmployeelistbyid != null && dtEmployeelistbyid.Rows.Count > 0)
            {

                // complainResisterfunction.UpdateComplainResiter(htcomplainResister, mdl[0].Complain_ID);
                emplyeeManagment.UpdateEmployee(htemployee, mdl[0].EMP_ID);
                msg = "Update Sucessfully";
            }
            else
            {

                emplyeeManagment.SaveEmployee(htemployee);
                msg = "Save Sucessfully";

            }


          
          
             return msg;

        }
    }
}