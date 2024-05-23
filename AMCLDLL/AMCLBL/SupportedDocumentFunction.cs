using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMCLBL
{
    public class SupportedDocumentFunction
    {
        CommonGateway commonGatewayObj = new CommonGateway();

        public SupportedDocumentFunction()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetSupportedDocument()
        {
            DataTable dtSuportedDOC = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append(" SELECT    *     FROM        CMS_SUPP_DOC  ");
            sbOrderBy.Append(" ORDER BY SUP_DOC_ID ASC ");

            sbMst.Append(sbOrderBy.ToString());
            dtSuportedDOC = commonGatewayObj.Select(sbMst.ToString());
            return dtSuportedDOC;
        }
        public DataTable GetSupportedDocumentBYID(string Id)
        {
            DataTable dtSuportedDOC = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append(" SELECT    *     FROM        CMS_SUPP_DOC WHERE SUP_DOC_ID IN("+ Id + ")  ");
            sbOrderBy.Append(" ORDER BY SUP_DOC_ID ASC ");

            sbMst.Append(sbOrderBy.ToString());
            dtSuportedDOC = commonGatewayObj.Select(sbMst.ToString());
            return dtSuportedDOC;
        }
        public DataTable Get_SUP_DOC_ID()
        {
            string Query = "Select MAX(SUP_DOC_ID)+1 as SUP_DOC_ID from CMS_SUPP_DOC_DETAILS";
            DataTable dt_SUP_DOC_ID = this.commonGatewayObj.Select(Query);
            return dt_SUP_DOC_ID;
        }

        public void SaveSupportedDocument(Hashtable htSupportedDocument)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();
                this.commonGatewayObj.Insert(htSupportedDocument, "CMS_SUPP_DOC_DETAILS");
                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public void UpdateSupportedDocument(Hashtable htServiceReiter, string ID)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htServiceReiter, "CMS_SUPP_DOC_DETAILS", "ID=" + ID + "");

                // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");


                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
    }
}
