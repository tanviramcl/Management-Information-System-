using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMCLBL
{
   public class EmployeeManagment
    {
        CommonGateway commonGatewayObj = new CommonGateway();

       
        public EmployeeManagment()
        {
        }

        public void SaveEmployee(Hashtable htServiceReiter)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();
              

                

                this.commonGatewayObj.Insert(htServiceReiter, "EMP_INFO");

               
                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void SaveDESIGNATION(Hashtable htServiceReiter)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();




                this.commonGatewayObj.Insert(htServiceReiter, "DESIGNATION");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void SaveDepartment(Hashtable htdtdept)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();




                this.commonGatewayObj.Insert(htdtdept, "DEPARTMENT");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void SaveUser(Hashtable htServiceReiter)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();




                this.commonGatewayObj.Insert(htServiceReiter, "USER_INFO");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }

        public void UpdateEmployee(Hashtable htServiceReiter, int Emp_id)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htServiceReiter, "EMP_INFO", "EMP_ID=" + Emp_id + "");

                // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void UpdateDESIGNATION(Hashtable htServiceReiter, int DESIGNATION_ID)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htServiceReiter, "DESIGNATION", "DESIGNATION_ID=" + DESIGNATION_ID + "");

                // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void UpdateDepartment(Hashtable htServiceReiter, int DEPARTMENT_ID)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htServiceReiter, "DEPARTMENT", "DEPARTMENT_ID=" + DEPARTMENT_ID + "");

                // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public DataTable Get_Employee_ByID(string ID)
        {
            string Query = "SELECT * FROM EMP_INFO where EMP_ID=" + ID + "";
            DataTable dtallEmployee = this.commonGatewayObj.Select(Query);
            return dtallEmployee;
        }

        public DataTable Get_DESIGNATION__ByID(string ID)
        {
            string Query = "SELECT * FROM DESIGNATION where DESIGNATION_ID=" + ID + "";
            DataTable dtallEmployee = this.commonGatewayObj.Select(Query);
            return dtallEmployee;
        }
        public DataTable Get_Department__ByID(string ID)
        {
            string Query = "SELECT * FROM DEPARTMENT where DEPARTMENT_ID=" + ID + "";
            DataTable dtallEmployee = this.commonGatewayObj.Select(Query);
            return dtallEmployee;
        }
        public DataTable Get_USER_ByID(string ID)
        {
            string Query = "SELECT * FROM EMP_INFO where EMPLOYEE_NAME='" + ID+"'";
            DataTable dtalluser = this.commonGatewayObj.Select(Query);
            return dtalluser;
        }
        public DataTable Get_Employee_ID()
        {
            string Query = "Select MAX(EMP_ID)+1 as EMP_ID  from EMP_INFO";
            DataTable dtEmpID = this.commonGatewayObj.Select(Query);
            return dtEmpID;
        }

        public DataTable Get_DepsrtmentList()
        {
            string Query = "Select * from INVEST.EMP_DEPARTMENT order by ID asc";
            DataTable dtDepartmentList = this.commonGatewayObj.Select(Query);
            return dtDepartmentList;
        }
        public DataTable Get_DESIGNATIONList()
        {
            string Query = "Select * from DESIGNATION order by DESIGNATION_ID asc";
            DataTable dtDESIGNATIONList = this.commonGatewayObj.Select(Query);
            return dtDESIGNATIONList;
        }
        public DataTable Get_All_employee()
        {
            string Query = " SELECT * FROM INVEST.EMP_INFO ";

            DataTable dtallemployee = this.commonGatewayObj.Select(Query);
            return dtallemployee;
        }
        public DataTable Get_All_DESIGNATION()
        {
            string Query = " SELECT  * FROM INVEST.EMP_DESIGNATION ";

            DataTable dtallemployee = this.commonGatewayObj.Select(Query);
            return dtallemployee;
        }

        public DataTable GET_DESIGNATION_ID()
        {
            string Query = "Select MAX(DESIGNATION_ID)+1 as DESIGNATION_ID  from DESIGNATION";
            DataTable dtEmpID = this.commonGatewayObj.Select(Query);
            return dtEmpID;
        }
        public DataTable GET_Department_ID()
        {
            string Query = "Select MAX(DEPARTMENT_ID)+1 as DEPARTMENT_ID  from DEPARTMENT";
            DataTable dtEmpID = this.commonGatewayObj.Select(Query);
            return dtEmpID;
        }
    }
}
