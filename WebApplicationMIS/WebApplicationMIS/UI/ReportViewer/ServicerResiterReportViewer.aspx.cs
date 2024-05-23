using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI.ReportViewer
{
    public partial class ServicerResiterReportViewer : System.Web.UI.Page
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        private ReportDocument rdoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Fromdate = "";
            string Todate = "";
            string Ststus = "";
            string Finalstatus = "";

            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../../Default.aspx");
            }
            else
            {
                //Fromdate = (string)Session["Fromdate"];
                //Todate = (string)Session["Todate"];
                //fundCode = (string)Session["fundCodes"];
                //companycode = (string)Session["companycode"];
                //transtype = (string)Session["transtype"];

                Fromdate = Convert.ToString(Request.QueryString["p1date"]).Trim();
                Todate = Convert.ToString(Request.QueryString["p2date"]).Trim();
                Ststus = Convert.ToString(Request.QueryString["status"]).Trim();
                if (Ststus == "ALL")
                {
                    Finalstatus = "ALL";
                }
                else
                {
                    Finalstatus = Ststus;
                }

            }
            DataTable dtReprtSource = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbfilter = new StringBuilder();
            sbfilter.Append(" ");

            if (Ststus != null && Ststus != "ALL")
            {

                sbMst.Append("select * from (select  e.RESISTER_ID, e.REG_BK, e.REG_BR, e.REG_NO, e.SERVICE_NAME,f.SERVICE_SUB_NAME, e.SUP_DOC_REQ_ID,e.USER_NAME,e.ENTRY_DATETIME, e.NAME, e.ADDRS1, e.ADDRS2,");
                sbMst.Append(" e.EMAIL, e.MOBILE1,e.STATUS,e.REMARKS,e.COMPLIAN_DATE,e.BO, e.ALLOT_NO, e.FOLIO_NO ,e.URGENCY    from (select c.RESISTER_ID, c.REG_BK, c.REG_BR, c.REG_NO, d.SERVICE_NAME, ");
                sbMst.Append("   c.SERVICE_SUB_ID,      c.SUP_DOC_REQ_ID,  c.USER_NAME,c.ENTRY_DATETIME, c.NAME, c.ADDRS1, c.ADDRS2,   c.EMAIL, c.MOBILE1,c.STATUS,c.REMARKS,c.COMPLIAN_DATE,c.BO,c.URGENCY,");
                sbMst.Append(" c.ALLOT_NO, c.FOLIO_NO   from             (SELECT a.RESISTER_ID, a.REG_BK, a.REG_BR,          a.REG_NO, a.SERVICE_ID, a.SERVICE_SUB_ID,a.SUP_DOC_REQ_ID, a.USER_NAME,  ");
                sbMst.Append("  a.ENTRY_DATETIME, a.NAME, a.ADDRS1, a.ADDRS2,              a.EMAIL, a.MOBILE1,a.STATUS,a.REMARKS, a.COMPLIAN_DATE,a.BO, b.ALLOT_NO, a.FOLIO_NO, a.URGENCY ");
                sbMst.Append("   FROM UNIT.CMS_SERVICE_REGISTER  a INNER JOIN unit.U_MASTER b  ON a.REG_BK = b.REG_BK     and a.REG_BR= b.REG_BR and a.REG_NO= b.REG_NO  ) c");
                sbMst.Append(" INNER JOIN unit.CMS_CUS_SERVICE d  ON c.SERVICE_ID = d.SERVICE_ID ) e     INNER JOIN unit.CMS_SERVICE_SUB f  ON e.SERVICE_SUB_ID = f.SERVICE_SUB_ID ) where  STATUS='" + Ststus + "' and COMPLIAN_DATE BETWEEN '" + Fromdate + "' AND '" + Todate + "' order by RESISTER_ID asc ");


            }
            else
            {
                sbMst.Append("select * from (select  e.RESISTER_ID, e.REG_BK, e.REG_BR, e.REG_NO, e.SERVICE_NAME,f.SERVICE_SUB_NAME, e.SUP_DOC_REQ_ID,e.USER_NAME,e.ENTRY_DATETIME, e.NAME, e.ADDRS1, e.ADDRS2,");
                sbMst.Append(" e.EMAIL, e.MOBILE1,e.STATUS,e.REMARKS,e.COMPLIAN_DATE,e.BO, e.ALLOT_NO, e.FOLIO_NO ,e.URGENCY    from (select c.RESISTER_ID, c.REG_BK, c.REG_BR, c.REG_NO, d.SERVICE_NAME, ");
                sbMst.Append("   c.SERVICE_SUB_ID,      c.SUP_DOC_REQ_ID,  c.USER_NAME,c.ENTRY_DATETIME, c.NAME, c.ADDRS1, c.ADDRS2,   c.EMAIL, c.MOBILE1,c.STATUS,c.REMARKS,c.COMPLIAN_DATE,c.BO,c.URGENCY,");
                sbMst.Append(" c.ALLOT_NO, c.FOLIO_NO   from             (SELECT a.RESISTER_ID, a.REG_BK, a.REG_BR,          a.REG_NO, a.SERVICE_ID, a.SERVICE_SUB_ID,a.SUP_DOC_REQ_ID, a.USER_NAME,  ");
                sbMst.Append("  a.ENTRY_DATETIME, a.NAME, a.ADDRS1, a.ADDRS2,              a.EMAIL, a.MOBILE1,a.STATUS,a.REMARKS, a.COMPLIAN_DATE,a.BO, b.ALLOT_NO, a.FOLIO_NO, a.URGENCY ");
                sbMst.Append("   FROM UNIT.CMS_SERVICE_REGISTER  a INNER JOIN unit.U_MASTER b  ON a.REG_BK = b.REG_BK     and a.REG_BR= b.REG_BR and a.REG_NO= b.REG_NO  ) c");
                sbMst.Append(" INNER JOIN unit.CMS_CUS_SERVICE d  ON c.SERVICE_ID = d.SERVICE_ID ) e     INNER JOIN unit.CMS_SERVICE_SUB f  ON e.SERVICE_SUB_ID = f.SERVICE_SUB_ID ) where   COMPLIAN_DATE BETWEEN '" + Fromdate + "' AND '" + Todate + "' order by RESISTER_ID asc ");

            }
            sbMst.Append(sbfilter.ToString());
            string query= sbMst.ToString();
            dtReprtSource = commonGatewayObj.Select(sbMst.ToString());
            dtReprtSource.TableName = "ServicResister";
            //        dtReprtSource.WriteXmlSchema(@"D:\CMS\WebApplicationCMS\WebApplicationCMS\UI\ReportViewer\Report\crtServicResistersattusWise.xsd");

            DataTable dttotalcomplain = new DataTable();
            StringBuilder totcom = new StringBuilder();

            if (Ststus != null && Ststus != "ALL")
            {

                totcom.Append("select count(RESISTER_ID) totalcomplain from (select  e.RESISTER_ID, e.REG_BK, e.REG_BR, e.REG_NO, e.SERVICE_NAME,f.SERVICE_SUB_NAME, e.SUP_DOC_REQ_ID,e.USER_NAME,e.ENTRY_DATETIME, e.NAME, e.ADDRS1, e.ADDRS2,");
                totcom.Append(" e.EMAIL, e.MOBILE1,e.STATUS,e.REMARKS,e.COMPLIAN_DATE,e.BO, e.ALLOT_NO, e.FOLIO_NO ,e.URGENCY    from (select c.RESISTER_ID, c.REG_BK, c.REG_BR, c.REG_NO, d.SERVICE_NAME, ");
                totcom.Append("   c.SERVICE_SUB_ID,      c.SUP_DOC_REQ_ID,  c.USER_NAME,c.ENTRY_DATETIME, c.NAME, c.ADDRS1, c.ADDRS2,   c.EMAIL, c.MOBILE1,c.STATUS,c.REMARKS,c.COMPLIAN_DATE,c.BO,c.URGENCY,");
                totcom.Append(" c.ALLOT_NO, c.FOLIO_NO   from             (SELECT a.RESISTER_ID, a.REG_BK, a.REG_BR,          a.REG_NO, a.SERVICE_ID, a.SERVICE_SUB_ID,a.SUP_DOC_REQ_ID, a.USER_NAME,  ");
                totcom.Append("  a.ENTRY_DATETIME, a.NAME, a.ADDRS1, a.ADDRS2,              a.EMAIL, a.MOBILE1,a.STATUS,a.REMARKS, a.COMPLIAN_DATE,a.BO, b.ALLOT_NO, a.FOLIO_NO, a.URGENCY ");
                totcom.Append("   FROM UNIT.CMS_SERVICE_REGISTER  a INNER JOIN unit.U_MASTER b  ON a.REG_BK = b.REG_BK     and a.REG_BR= b.REG_BR and a.REG_NO= b.REG_NO  ) c");
                totcom.Append(" INNER JOIN unit.CMS_CUS_SERVICE d  ON c.SERVICE_ID = d.SERVICE_ID ) e     INNER JOIN unit.CMS_SERVICE_SUB f  ON e.SERVICE_SUB_ID = f.SERVICE_SUB_ID ) where  STATUS='" + Ststus + "' and COMPLIAN_DATE BETWEEN '" + Fromdate + "' AND '" + Todate + "' order by RESISTER_ID asc ");
            }
            else
            {

                totcom.Append("select count(RESISTER_ID) totalcomplain from (select  e.RESISTER_ID, e.REG_BK, e.REG_BR, e.REG_NO, e.SERVICE_NAME,f.SERVICE_SUB_NAME, e.SUP_DOC_REQ_ID,e.USER_NAME,e.ENTRY_DATETIME, e.NAME, e.ADDRS1, e.ADDRS2,");
                totcom.Append(" e.EMAIL, e.MOBILE1,e.STATUS,e.REMARKS,e.COMPLIAN_DATE,e.BO, e.ALLOT_NO, e.FOLIO_NO ,e.URGENCY    from (select c.RESISTER_ID, c.REG_BK, c.REG_BR, c.REG_NO, d.SERVICE_NAME, ");
                totcom.Append("   c.SERVICE_SUB_ID,      c.SUP_DOC_REQ_ID,  c.USER_NAME,c.ENTRY_DATETIME, c.NAME, c.ADDRS1, c.ADDRS2,   c.EMAIL, c.MOBILE1,c.STATUS,c.REMARKS,c.COMPLIAN_DATE,c.BO,c.URGENCY,");
                totcom.Append(" c.ALLOT_NO, c.FOLIO_NO   from             (SELECT a.RESISTER_ID, a.REG_BK, a.REG_BR,          a.REG_NO, a.SERVICE_ID, a.SERVICE_SUB_ID,a.SUP_DOC_REQ_ID, a.USER_NAME,  ");
                totcom.Append("  a.ENTRY_DATETIME, a.NAME, a.ADDRS1, a.ADDRS2,              a.EMAIL, a.MOBILE1,a.STATUS,a.REMARKS, a.COMPLIAN_DATE,a.BO, b.ALLOT_NO, a.FOLIO_NO, a.URGENCY ");
                totcom.Append("   FROM UNIT.CMS_SERVICE_REGISTER  a INNER JOIN unit.U_MASTER b  ON a.REG_BK = b.REG_BK     and a.REG_BR= b.REG_BR and a.REG_NO= b.REG_NO  ) c");
                totcom.Append(" INNER JOIN unit.CMS_CUS_SERVICE d  ON c.SERVICE_ID = d.SERVICE_ID ) e     INNER JOIN unit.CMS_SERVICE_SUB f  ON e.SERVICE_SUB_ID = f.SERVICE_SUB_ID ) where  COMPLIAN_DATE BETWEEN '" + Fromdate + "' AND '" + Todate + "' order by RESISTER_ID asc ");
            }

            totcom.Append(sbfilter.ToString());
            dttotalcomplain = commonGatewayObj.Select(totcom.ToString());
            int totalcomplain = 0;

            if (dttotalcomplain.Rows.Count > 0)
            {
                totalcomplain = Convert.ToInt32(dttotalcomplain.Rows[0][0]);

            }

            dttotalcomplain = commonGatewayObj.Select(totcom.ToString());


            if (dtReprtSource.Rows.Count > 0)
            {

                string Path = Server.MapPath("Report/CrystalReportCompalinResister.rpt");
                rdoc.Load(Path);
                rdoc.SetDataSource(dtReprtSource);
                CR_ServiceResiter.ReportSource = rdoc;
                CR_ServiceResiter.DisplayToolbar = true;
                CR_ServiceResiter.HasExportButton = true;
                CR_ServiceResiter.HasPrintButton = true;
                
               
                rdoc = ReportFactory.GetReport(rdoc.GetType());

            }
            else
            {
                Response.Write("No Data Found");
            }
        }
    }
}