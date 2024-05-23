using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class SaleSmsReportViewer : System.Web.UI.Page
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        private ReportDocument rdoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            string fund_code = (string)Session["fund_code"];
             string sale_number_from = (string)Session["sale_number_from"];
             string sale_number_to = (string)Session["sale_number_to"];
            string message = (string)Session["message"];
            string rF = (string)Session["rF"];
           

            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtSaleCert = new DataTable();

            sbQueryString.Append(" SELECT    FUND_INFO.FUND_NM, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, U_MASTER.MOBILE1,");
            sbQueryString.Append("  '"+ message + "' AS SMS FROM            UNIT.U_MASTER, UNIT.FUND_INFO, UNIT.SALE");
            sbQueryString.Append(" WHERE        U_MASTER.REG_BK = FUND_INFO.FUND_CD AND U_MASTER.REG_BK = SALE.REG_BK AND U_MASTER.REG_BR = SALE.REG_BR AND U_MASTER.REG_NO = SALE.REG_NO ");
            sbQueryString.Append("  AND (SALE.SL_NO  BETWEEN "+sale_number_from+" AND "+sale_number_to+") AND  SALE.REG_BK='"+fund_code+"' AND  (SALE.SL_TYPE = 'SL') AND (LENGTH(U_MASTER.MOBILE1) = 11) AND (U_MASTER.MOBILE1 LIKE '0%')");
           
            string q = sbQueryString.ToString();

            dtSaleCert = commonGatewayObj.Select(q.ToString());

           // dtSaleCert.WriteXmlSchema(@"D:\Development\Email\MIS\WebApplicationMIS\WebApplicationMIS\UI\ReportViewer\Report\xsdSaleCert.xsd");

            if (dtSaleCert.Rows.Count > 0)
            {
                string Path = Server.MapPath("Report/crSaleSms.rpt");
                rdoc.Load(Path);
                //ds.Tables[0].Merge(dtRptSrcMainReport);
                //ds.Tables[0].Merge(dtRptSrcSubReport);
                // rdoc.SetDataSource(dtReprtSource);
                rdoc.SetDataSource(dtSaleCert);

                //CRV_CapitalGainAllFundsReportViewer.ReportSource = rdoc;
                //rdoc.SetParameterValue("prmFromdate", strFromdate);
                //rdoc.SetParameterValue("prmTodate", strTodate);
                //rdoc = ReportFactory.GetReport(rdoc.GetType());

                ExportFormatType formatType = ExportFormatType.NoFormat;
                switch (rF)
                {
                    case "Word":
                        formatType = ExportFormatType.WordForWindows;
                        break;
                    case "PDF":
                        formatType = ExportFormatType.PortableDocFormat;
                        break;
                    case "Excel":
                        formatType = ExportFormatType.ExcelWorkbook;
                        break;
                    case "CSV":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        break;
                }

                // rdoc.ExportToHttpResponse(formatType, Response, true, "SaleSMS");
                //rdoc.ExportToDisk(ExportFormatType.ExcelRecord, "report.xls");

                //rdoc.ExportToDisk(ExportFormatType.ExcelRecord, "D:\\Data\\IAMCL\\Tax-" + sale_number_from + "-" + fund_code + "");

                rdoc.ExportToHttpResponse(formatType, Response, true, "SaleSMS_" + fund_code+ "_" + DateTime.Now);

                Response.End();


            }
            else
            {
                Response.Write("No Data Found");
            }
        }
    }
}