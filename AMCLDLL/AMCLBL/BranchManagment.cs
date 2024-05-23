using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMCLBL
{
   public class BranchManagment
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        public DataTable Get_BRANCH_ByID(string ID)
        {
            string Query = "SELECT * FROM BRANCH_INFO where BRANCH_ID=" + ID + "";
            DataTable dtallEmployee = this.commonGatewayObj.Select(Query);
            return dtallEmployee;
        }

        public void SaveBarnch(Hashtable htbranch)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();




                this.commonGatewayObj.Insert(htbranch, "BRANCH_INFO");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }

        public void UpdateBarnch(Hashtable htbranch, int branchId)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htbranch, "BRANCH_INFO", "BRANCH_ID=" + branchId + "");

                // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public DataTable ALLBRANCH_INFO()
        {
            string Query = " SELECT * FROM CMS.BRANCH_INFO ";

            DataTable dtallemployee = this.commonGatewayObj.Select(Query);
            return dtallemployee;
        }
        public DataTable Get_BRANCH_ID()
        {
            string Query = "Select MAX(BRANCH_ID)+1 as BRANCH_ID  from BRANCH_INFO";
            DataTable dtEmpID = this.commonGatewayObj.Select(Query);
            return dtEmpID;
        }
    }


}
