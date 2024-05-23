using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMCLBL
{
    public class ComplainResisterFunction
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        SupportedDocumentFunction supportedDoc = new SupportedDocumentFunction();
        public ComplainResisterFunction()
        {
        }
        public void SaveComplainResiter(Hashtable htComplaineResister ,int Complain_ID)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                string id = "";
                DataTable dtID = Get_ID();

                if (Complain_ID != 0)
                {

                    id = dtID.Rows[0]["ID"].ToString();

                    htComplaineResister.Add("Complain_ID", id);
                 
                  
                }
                else
                {

                    htComplaineResister.Add("Complain_ID", 1);
                   
                
                }

               ;
        
                this.commonGatewayObj.Insert(htComplaineResister, "COMPLAIN_REGISTER");

            
                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void SaveCOMPLAIN_ACTION(Hashtable htComplaineResister)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                string id = "";
                DataTable dtID = Get_ACCtion_ID();

                if (dtID != null && dtID.Rows.Count > 0)
                {

                    id = dtID.Rows[0]["ID"].ToString();

                    htComplaineResister.Add("COMPLAIN_ACTION_ID", id);


                }
                else
                {

                    htComplaineResister.Add("COMPLAIN_ACTION_ID", 1);


                }

               ;

                this.commonGatewayObj.Insert(htComplaineResister, "COMPLAIN_ACTION");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public DataTable Get_Complain_Resiter()
        {
            string Query = " Select E.COMPLAIN_ID,E.SERVICE_NAME, E.SERVICE_SUB_NAME, F.BRANCH_NAME , E.COMPLAIN_SUBJECT, E.COMPLAIN_DETAILS, "+ 
   " E.STATUS, E.REMARKS, E.COMPLIAN_DATE,  E.URGENCY, E.ENTRY_DATETIME, E.COMPLAIN_TYPE_ID, E.USER_NAME "+
   "from (Select   C.COMPLAIN_ID, C.SERVICE_NAME, D.SERVICE_SUB_NAME, C.BRANCH_ID, C.COMPLAIN_SUBJECT, C.COMPLAIN_DETAILS, "+
   " C.STATUS, C.REMARKS, C.COMPLIAN_DATE, C.URGENCY, C.ENTRY_DATETIME, C.COMPLAIN_TYPE_ID, C.USER_NAME from (SELECT "+
   " A.COMPLAIN_ID, B.SERVICE_NAME, A.SERVICE_SUB_ID, A.BRANCH_ID, A.COMPLAIN_SUBJECT, A.COMPLAIN_DETAILS, A.STATUS, A.REMARKS, A.COMPLIAN_DATE, "+
   " A.URGENCY, A.ENTRY_DATETIME, A.COMPLAIN_TYPE_ID, A.USER_NAME FROM CMS.COMPLAIN_REGISTER a INNER JOIN CUS_SERVICE "+
 " b ON A.SERVICE_ID = B.SERVICE_ID) C inner join SERVICE_SUB D ON C.SERVICE_SUB_ID = D.SERVICE_SUB_ID) E INNER JOIN BRANCH_INFO F "+
  " ON E.BRANCH_ID = F.BRANCH_ID ";

            DataTable dtallServiceResister = this.commonGatewayObj.Select(Query);
            return dtallServiceResister;
        }
        public DataTable Get_Complain_resiterID()
        {
            string Query = "Select MAX(COMPLAIN_ID)+1 as COMPLAIN_ID from COMPLAIN_REGISTER";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }
        public DataTable COMPLAIN_ACTION_ID()
        {
            string Query = "Select MAX(COMPLAIN_ACTION_ID)+1 as COMPLAIN_ACTION_ID from COMPLAIN_ACTION";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }
        public void UpdateComplainResiter(Hashtable htServiceReiter, int complain_id)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htServiceReiter, "COMPLAIN_REGISTER", "COMPLAIN_ID=" + complain_id + "");

                // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public DataTable Get_Complain_ResiterByID(string ID)
        {
            string Query = "SELECT * FROM COMPLAIN_REGISTER where COMPLAIN_ID=" + ID + "";
            DataTable dtallServiceResister = this.commonGatewayObj.Select(Query);
            return dtallServiceResister;
        }
        public DataTable Get_DuplicateComplain_resiterID(int BRANCH_ID, string SERVICE_ID, int Complain_ID)
        {
            string Query = "SELECT * FROM COMPLAIN_REGISTER a WHERE  a.BRANCH_ID=" + BRANCH_ID + " and a.SERVICE_ID=" + SERVICE_ID + " and a.COMPLAIN_ID=" + Complain_ID + " ";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }

        public DataTable Get_ID()
        {
            string Query = "Select MAX(COMPLAIN_ID)+1 ID from COMPLAIN_REGISTER";
            DataTable dtID = this.commonGatewayObj.Select(Query);
            return dtID;
        }
        public DataTable Get_ACCtion_ID()
        {
            string Query = "Select MAX(COMPLAIN_ID)+1 ID from COMPLAIN_REGISTER";
            DataTable dtID = this.commonGatewayObj.Select(Query);
            return dtID;
        }
        public DataTable Get_DuplicateresiterID(int Complain_ID)
        {
            string Query = "SELECT  * FROM COMPLAIN_REGISTER a WHERE a.COMPLAIN_ID='" + Complain_ID + "' ";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }
        public DataTable Get_Duplicate_Compalin_Action_(int COMPLAIN_ACTION_ID)
        {
            string Query = "SELECT  * FROM COMPLAIN_ACTION a WHERE a.COMPLAIN_ACTION_ID='" + COMPLAIN_ACTION_ID + "' ";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }

    }
}
