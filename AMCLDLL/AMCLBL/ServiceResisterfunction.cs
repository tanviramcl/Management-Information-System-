using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMCLBL
{
    public class ServiceResisterfunction
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        SupportedDocumentFunction supportedDoc = new SupportedDocumentFunction();
        public ServiceResisterfunction()
        {
        }
        public void SaveResiter(Hashtable htServiceReiter,Hashtable htSupportedDocument, int REGISTER_ID)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();
                string supp_doc_id = "";
                DataTable dtID = Get_ID();
                DataTable dtDocID = Get_SUPPO_DOC_ID();
                if (dtDocID != null && dtDocID.Rows.Count > 0)
                {
                    supp_doc_id = dtDocID.Rows[0]["ID"].ToString();
                }


                //string SUP_DOC_REQ_ID = "";
                string id = "";
                if (REGISTER_ID != 0)
                {
                  
                    id = dtID.Rows[0]["ID"].ToString();
                    htServiceReiter.Add("ID", id);
                    htServiceReiter.Add("RESISTER_ID", REGISTER_ID);
                    htSupportedDocument.Add("RESISTER_ID", REGISTER_ID);
                    htSupportedDocument.Add("SUP_DOC_ID", REGISTER_ID);
                    htSupportedDocument.Add("ID", supp_doc_id);
                     htServiceReiter.Add("SUP_DOC_REQ_ID", REGISTER_ID);
                }
                else
                {
                    htServiceReiter.Add("ID", 1);
                    htServiceReiter.Add("RESISTER_ID", 1);
                    htSupportedDocument.Add("RESISTER_ID", 1);
                    htSupportedDocument.Add("SUP_DOC_ID", 1);
                    htSupportedDocument.Add("ID", 1);
                    htServiceReiter.Add("SUP_DOC_REQ_ID", 1);
                }

                //  DataTable dtSuppDocID = supportedDoc.Get_SUP_DOC_ID();

                //if (!string.IsNullOrEmpty(dtSuppDocID.Rows[0]["SUP_DOC_ID"].ToString()))
                //{
                //    SUP_DOC_REQ_ID = dtSuppDocID.Rows[0]["SUP_DOC_ID"].ToString();
                //    htSupportedDocument.Add("SUP_DOC_ID", SUP_DOC_REQ_ID);
                //    htServiceReiter.Add("SUP_DOC_REQ_ID", SUP_DOC_REQ_ID);
                //}
                //else
                //{
                //    htSupportedDocument.Add("SUP_DOC_ID", 1);
                //    htServiceReiter.Add("SUP_DOC_REQ_ID", 1);
                //}


                this.commonGatewayObj.Insert(htServiceReiter, "CUS_SERVICE");

                supportedDoc.SaveSupportedDocument(htSupportedDocument);
                this.commonGatewayObj.CommitTransaction();
            }
            catch(Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }
          

        }
        public void UpdateServiceResister(Hashtable htServiceReiter ,int REGISTER_ID,string id)
        {
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj.Update(htServiceReiter, "CUS_SERVICE", "ID="+ id + "") ;
            
               // this.commonGatewayObj.Insert(htServiceReiter, "CMS_SERVICE_REGISTER");

            
                this.commonGatewayObj.CommitTransaction();
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                throw e;
            }


        }
        public DataTable Get_IDbyRegID(int REg_id)
        {
            string Query = "Select  ID from CUS_SERVICE Where RESISTER_ID=" + REg_id+"";
            DataTable dtID = this.commonGatewayObj.Select(Query);
            return dtID;
        }
        public DataTable Get_IDbySupportedDocID(int supp_doc_id)
        {
            string Query = "Select  ID from CMS_SUPP_DOC_DETAILS Where SUP_DOC_ID=" + supp_doc_id + "";
            DataTable dtID = this.commonGatewayObj.Select(Query);
            return dtID;
        }
        public DataTable Get_ID()
        {
            string Query = "Select MAX(ID)+1 ID from CUS_SERVICE";
            DataTable dtID= this.commonGatewayObj.Select(Query);
            return dtID;
        }

        public DataTable Get_SUPPO_DOC_ID()
        {
            string Query = "Select MAX(ID)+1 ID from CMS_SUPP_DOC_DETAILS";
            DataTable dtID = this.commonGatewayObj.Select(Query);
            return dtID;
        }
        public DataTable Get_DuplicateresiterID(int REG_NO, string REG_BK, string REG_BR, int SERVICE_ID,int REGISTER_ID)
        {
            string Query = "SELECT  a.RESISTER_ID, a.REG_BK, a.REG_BR, a.REG_NO, a.SERVICE_ID, a.SERVICE_SUB_ID, a.SUP_DOC_REQ_ID, a.USER_NAME, a.ENTRY_DATETIME, a.NAME, a.ADDRS1, a.ADDRS2,   a.EMAIL, a.MOBILE1 FROM CUS_SERVICE a WHERE a.REG_BK='" + REG_BK + "' and  a.REG_BR='"+ REG_BR + "' and  a.REG_NO="+ REG_NO + " and a.SERVICE_ID="+ SERVICE_ID + " and a.RESISTER_ID="+ REGISTER_ID + " ";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }
        public DataTable Get_resiterID()
        {
            string Query = "Select MAX(RESISTER_ID)+1 as RESISTER_ID from CUS_SERVICE";
            DataTable dtResisterID = this.commonGatewayObj.Select(Query);
            return dtResisterID;
        }
        public DataTable Get_Service_Resiter()
        {
            string Query = "  select  e.RESISTER_ID, e.REG_BK, e.REG_BR, e.REG_NO, e.SERVICE_NAME,f.SERVICE_SUB_NAME, e.SUP_DOC_REQ_ID,e.USER_NAME,e.ENTRY_DATETIME, e.NAME, e.ADDRS1, e.ADDRS2,      "
                + "             e.EMAIL, e.MOBILE1,e.STATUS,e.REMARKS,e.COMPLIAN_DATE,e.BO, e.ALLOT_NO, e.FOLIO_NO ,e.URGENCY    from (select c.RESISTER_ID, c.REG_BK, c.REG_BR, c.REG_NO, d.SERVICE_NAME, "
                + "              c.SERVICE_SUB_ID,      c.SUP_DOC_REQ_ID,  c.USER_NAME,c.ENTRY_DATETIME, c.NAME, c.ADDRS1, c.ADDRS2,   c.EMAIL, c.MOBILE1,c.STATUS,c.REMARKS,c.COMPLIAN_DATE,c.BO,c.URGENCY, "
                + "         c.ALLOT_NO, c.FOLIO_NO   from             (SELECT a.RESISTER_ID, a.REG_BK, a.REG_BR,          a.REG_NO, a.SERVICE_ID, a.SERVICE_SUB_ID,a.SUP_DOC_REQ_ID, a.USER_NAME,    "
                + "          a.ENTRY_DATETIME, a.NAME, a.ADDRS1, a.ADDRS2,              a.EMAIL, a.MOBILE1,a.STATUS,a.REMARKS, a.COMPLIAN_DATE,a.BO, b.ALLOT_NO, a.FOLIO_NO, a.URGENCY      "
                + "       FROM UNIT.CUS_SERVICE  a INNER JOIN unit.U_MASTER b  ON a.REG_BK = b.REG_BK     and a.REG_BR= b.REG_BR and a.REG_NO= b.REG_NO  ) c  "
                + "    INNER JOIN unit.CUS_SERVICE d  ON c.SERVICE_ID = d.SERVICE_ID ) e     INNER JOIN unit.SERVICE_SUB f  ON e.SERVICE_SUB_ID = f.SERVICE_SUB_ID  ";

            DataTable dtallServiceResister = this.commonGatewayObj.Select(Query);
            return dtallServiceResister;
        }

        public DataTable ALLGet_SupportedDocumentList()
        {
            string Query = "  SELECT  a.*,b.NAME FROM UNIT.CMS_SUPP_DOC_DETAILS a INNER JOIN UNIT.CUS_SERVICE b ON a.RESISTER_ID = b.RESISTER_ID  order by SUP_DOC_ID ASC   ";

            DataTable dtallSupportedDodumentslist  = this.commonGatewayObj.Select(Query);
            return dtallSupportedDodumentslist;
        }
        public DataTable Get_SupportedDocumentListById(string Id)
        {
            string Query = "   SELECT  a.*,b.NAME FROM UNIT.CMS_SUPP_DOC_DETAILS a INNER JOIN UNIT.CUS_SERVICE b ON a.RESISTER_ID = b.RESISTER_ID  where sup_doc_id=" + Id + " order by SUP_DOC_ID ASC  ";

            DataTable dtallSupportedDodumentslist = this.commonGatewayObj.Select(Query);
            return dtallSupportedDodumentslist;
        }
        public DataTable Get_Service_ResiterByID(string ID)
        {
            string Query = "SELECT * FROM COMPLAIN_REGISTER where COMPLAIN_ID=" + ID+"";
            DataTable dtallServiceResister = this.commonGatewayObj.Select(Query);
            return dtallServiceResister;
        }




    }
}
