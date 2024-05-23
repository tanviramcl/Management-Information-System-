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
    public partial class UnitReportTaxCertReportViewer : System.Web.UI.Page
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        protected void Page_Load(object sender, EventArgs e)
        {
            string FYPart = "";
            // string CertType = "";
            string fundCode = "";
            //string branchCode = "";
            string closedate = "";

            string FYFrom = "";
            string FYTO = "";
            string Investment_type = "";
            string action = "";

            string FYPart_para = "";
            string ended_para = "";
            string interim_para = "";

            string RangeFrom = "";
            string RangeTo = "";





            // DataTable dtIncomeTax = (DataTable)Session["dtIncomeTax"];
            string FY = (string)Session["FY"];
            fundCode = (string)Session["FundCode"];
         
            FYPart = (string)Session["FY_PART"];
            FYFrom = (string)Session["FYFrom"];
            FYTO = (string)Session["FYTO"];
            Investment_type = (string)Session["Investment_type"];
            action = (string)Session["Action"];
            closedate = (string)Session["Cloasing_date"];
            RangeFrom = (string)Session["RangeFrom"];
            RangeTo = (string)Session["RangeTo"];
            StringBuilder sbQueryString = new StringBuilder();
            //  ReportDocument rdoc = new ReportDocument();
            if (action == "PDFGenarate")
            {

                DataTable dtIncomeTax = new DataTable();

                sbQueryString.Append("select * from ( SELECT  U_MASTER.REG_BK, REPLACE(U_MASTER.REG_BR,'/','-') AS REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
                sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(DIVIDEND.EMAIL) AS EMAIL, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
                sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
                sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
                sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE,DIVIDEND.TAX_CERT_MAIL_SEND  FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
                sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
                sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
                // sbQueryString.Append(" WHERE(U_MASTER.REG_BR = '" + branchCode + "') AND(U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + FY + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVI_PARA.CLOSE_DT='" + closedate + "' AND DIVIDEND.TAX_CERT_MAIL_SEND is Null )  WHERE REG_NO BETWEEN " + RangeFrom + " AND " + RangeTo + " ");
                sbQueryString.Append(" WHERE (U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + FY + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVI_PARA.CLOSE_DT='" + closedate + "'  )  WHERE REG_NO BETWEEN " + RangeFrom + " AND " + RangeTo + "    order by reg_no  ");


                string q = sbQueryString.ToString();

                dtIncomeTax = commonGatewayObj.Select(sbQueryString.ToString());

                // string q = sbQueryString.ToString();

                dtIncomeTax.TableName = "IncomeTax";

                //dtIncomeTax.WriteXmlSchema(@"D:\Development\MIS\WebApplicationMIS\WebApplicationMIS\UI\ReportViewer\Report\crtINCOMTAX.xsd");

                if (dtIncomeTax != null && dtIncomeTax.Rows.Count > 0)
                {

                    if (string.Compare(fundCode, "IAMPH", true) == 0)
                    {
                        FYPart_para = dtIncomeTax.Rows[0]["FY_PART"].Equals(DBNull.Value) ? "" : dtIncomeTax.Rows[0]["FY_PART"].ToString();
                        FYPart_para = "(" + FYPart.ToString() + ")";
                    }
                    interim_para = dtIncomeTax.Rows[0]["FY_PART"].ToString();

                    if (interim_para.ToString().ToUpper() == "INTERIM")
                    {
                        ended_para = "";
                    }
                    else if (interim_para.ToString().ToUpper() == "FINAL")
                    {
                        ended_para = "ended";
                    }
                    else
                    {
                        interim_para = "";
                        ended_para = "ended";
                    }


                    for (int incmtax = 0; incmtax < dtIncomeTax.Rows.Count; incmtax++)
                    {

                        string REG_NO = dtIncomeTax.Rows[incmtax]["REG_NO"].ToString();
                        string email = dtIncomeTax.Rows[incmtax]["EMAIL"].ToString();
                        string fund_name = dtIncomeTax.Rows[incmtax]["REG_BK"].ToString();
                        string branchCode = dtIncomeTax.Rows[incmtax]["REG_BR"].ToString();

                        DataTable dtDivi_para_Info = GET_DIVI_PARA(fundCode, FYPart, closedate, FY);


                        //strUpdateFundTransHB = "update fund_trans_hb set cost_rate = null,crt_aft_com = null where f_cd =" + tblAllfundInfo.Rows[i]["F_CD"].ToString();
                        //int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateFundTransHB);
                        //commonGatewayObj.CommitTransaction();


                        //DataTable dtTaxCal = dtDividendInfo(Convert.ToInt32(dtIncomeTax.Rows[0]["REG_NO"]), fundCode, branchCode, FY);
                        DataTable dtTaxCal = dtDividendInfo(Convert.ToInt32(REG_NO), branchCode, fundCode, FY, FYPart, closedate);

                        for (int j = 0; j < dtTaxCal.Rows.Count; j++)
                        {

                            string branchCode_Report = dtTaxCal.Rows[j]["REG_BR"].ToString();

                            ReportDocument rdoc = new ReportDocument();

                            //  rdoc = new ReportDocument();
                            string Path = Server.MapPath("Report/rptIncomeTax.rpt");
                            rdoc.Load(Path);
                            rdoc.Refresh();
                            rdoc.SetDataSource(dtTaxCal);
                            // CrystalReportViewer1.ReportSource = rdoc;

                            rdoc.SetParameterValue("fundCode", fundCode);
                            rdoc.SetParameterValue("branchCode", branchCode_Report);
                            rdoc.SetParameterValue("FYPart", FYPart_para.ToString());
                            rdoc.SetParameterValue("Interim", interim_para.ToString());
                            rdoc.SetParameterValue("ended", ended_para.ToString());

                            string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\INCOMETAX_CERTIFICATE\\" + fund_name;

                            if (!string.IsNullOrEmpty(fileLocation))
                            {
                                rdoc.ExportToDisk(ExportFormatType.PortableDocFormat, "" + fileLocation + "\\TAX_" + FY + "_" + REG_NO +"_"+ branchCode+".pdf");
                            }
                            CrystalReportViewer1 = null;
                            rdoc.Close();
                            rdoc.Dispose();
                            rdoc = null;
                            GC.Collect();
                        }


                    }
                     UpDate_DIVI_Para(fundCode, FY, closedate, FYPart, action);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Pdf Generate Successfully');", true);
                    Response.Redirect("../AllFundEmailTaxInvestmentCertificate.aspx");


                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
                    //Response.Redirect("../AllFundEmailTaxInvestmentCertificate.aspx");
                }

            }

            else if (action == "InvestmentPDFGenarate")
            {


                sbQueryString = new StringBuilder();
                //sbQueryString.Append(" select * from (SELECT FUND_INFO.FUND_NM, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO,TRIM(U_MASTER.email) as email , U_MASTER.HNAME,U_MASTER.INV_CERT_MAIL_SEND,U_MASTER.INV_CERT_SEND_DATE, U_MASTER.ADDRS1,");
                //sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY, U_JHOLDER.JNT_NAME FROM UNIT.U_MASTER INNER JOIN  UNIT.FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN ");
                //sbQueryString.Append("  UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND   U_MASTER.REG_NO = U_JHOLDER.REG_NO ");
                //sbQueryString.Append("  WHERE (U_MASTER.REG_BR = '" + branchCode + "') AND (U_MASTER.REG_BK = '" + fundCode + "') ) a where a.email is not null  and a.EMAIL LIKE'%@%'  and REGEXP_LIKE (a.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') and  REG_NO BETWEEN " + RangeFrom + " AND " + RangeTo + "");
                sbQueryString.Append("select * from ( SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
                sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(TRIM(DIVIDEND.EMAIL)) AS EMAIL, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
                sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
                sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
                sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE,DIVIDEND.TAX_CERT_MAIL_SEND  FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
                sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
                sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
                sbQueryString.Append(" WHERE (U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + FY + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVI_PARA.CLOSE_DT='" + closedate + "' AND DIVIDEND.INV_CERT_MAIL_SEND is Null )  WHERE REG_NO BETWEEN " + RangeFrom + " AND " + RangeTo + " order by reg_no ");
               // sbQueryString.Append(" WHERE (U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + FY + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVI_PARA.CLOSE_DT='" + closedate + "' AND DIVIDEND.INV_CERT_MAIL_SEND is Null )  WHERE REG_NO=17 order by reg_no ");


                DataTable dtInvestCertHolderInfo = commonGatewayObj.Select(sbQueryString.ToString());

                if (dtInvestCertHolderInfo != null && dtInvestCertHolderInfo.Rows.Count > 0)
                {
                    string fund_name = dtInvestCertHolderInfo.Rows[0]["REG_BK"].ToString();
                    for (int investment_cert = 0; investment_cert < dtInvestCertHolderInfo.Rows.Count; investment_cert++)
                    {
                        ReportDocument rdoc = new ReportDocument();

                        string REG_NO = dtInvestCertHolderInfo.Rows[investment_cert]["REG_NO"].ToString();
                        string branchCode = dtInvestCertHolderInfo.Rows[investment_cert]["REG_BR"].ToString();



                        string fyFrom = " ";
                        string fyTo = " ";

                        string[] divideFY = FY.Split('-');
                        if (divideFY.Length > 1)
                        {
                            fyFrom = "01-JUL-" + divideFY[0].ToString();
                            fyTo = "30-JUN-" + divideFY[1].ToString();
                        }
                        else
                        {
                            fyFrom = "01-JUL-" + FY.ToString();
                            fyTo = "30-JUN-" + Convert.ToString(Convert.ToUInt16(FY.ToString()) + 1);
                        }

                   

                        sbQueryString = new StringBuilder();
                        sbQueryString.Append(" SELECT SL_NO, TO_CHAR(SL_DT, 'DD-MON-YYYY') AS SL_DT,SL_TYPE, QTY, SL_PRICE, QTY * SL_PRICE AS AMOUNT, REG_BK || '/' || REG_BR || '/' || REG_NO AS REG_NO,REG_BR");
                        sbQueryString.Append(" FROM UNIT.SALE WHERE (REG_BK = '" + fundCode + "') AND (REG_NO = " + REG_NO + ")   AND (REG_BR = '" + branchCode + "') ");
                        sbQueryString.Append(" AND (SL_DT BETWEEN '" + Convert.ToDateTime(fyFrom).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(fyTo.ToString()).ToString("dd-MMM-yyyy") + "')");

                        if (Investment_type == "CIP")
                        {

                            sbQueryString.Append(" AND SL_TYPE='CIP' ");
                        }
                        else if (Investment_type == "NON CIP")
                        {

                            sbQueryString.Append(" AND SL_TYPE='SL' ");
                        }
                        DataTable dtInvestSaleInfo = commonGatewayObj.Select(sbQueryString.ToString());
                        if (dtInvestSaleInfo.Rows.Count > 0)
                        {

                            sbQueryString = new StringBuilder();
                            sbQueryString.Append(" SELECT SUM(QTY) as TOTAL_UNIT, sum(QTY * SL_PRICE) AS TOTAL_AMOUNT");
                            sbQueryString.Append(" FROM UNIT.SALE WHERE (REG_BK = '" + fundCode + "') AND (REG_NO = " + REG_NO + ")  AND (REG_BR = '" + branchCode + "')");
                            sbQueryString.Append(" AND (SL_DT BETWEEN '" + Convert.ToDateTime(fyFrom.ToString()).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(fyTo.ToString()).ToString("dd-MMM-yyyy") + "')");
                            if (Investment_type == "CIP")
                            {
                                sbQueryString.Append(" AND SL_TYPE='CIP' ");
                            }
                            else if (Investment_type == "NON CIP")
                            {
                                sbQueryString.Append(" AND SL_TYPE='SL' ");
                            }

                            DataTable dtInvestTotal = commonGatewayObj.Select(sbQueryString.ToString());

                            string branchCode_Report = dtInvestSaleInfo.Rows[0]["REG_BR"].ToString();

                            rdoc = new ReportDocument();
                            string Path = "";
                            if (Investment_type == "CIP")
                            {
                                Path = Server.MapPath("Report/rptInvestCertCIP.rpt");
                            }
                            else
                            {
                                Path = Server.MapPath("Report/rptInvestCert.rpt");
                            }
                            rdoc.Load(Path);
                            rdoc.Refresh();
                            rdoc.SetDataSource(dtInvestSaleInfo);
                            // CrystalReportViewer1.ReportSource = rdoc;

                            rdoc.SetParameterValue("branchCode", branchCode_Report);
                            rdoc.SetParameterValue("fundCode", fundCode);
                            rdoc.SetParameterValue("FY", fyFrom+" To "+ fyTo);
                            rdoc.SetParameterValue("close_dt", closedate.ToString());

                            rdoc.SetParameterValue("HNAME", dtInvestCertHolderInfo.Rows[0]["HNAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[investment_cert]["HNAME"].ToString());
                            rdoc.SetParameterValue("JHOLDER", dtInvestCertHolderInfo.Rows[0]["JNT_NAME"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[investment_cert]["JNT_NAME"].ToString());
                            rdoc.SetParameterValue("ADDRESS1", dtInvestCertHolderInfo.Rows[0]["ADDRS1"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[investment_cert]["ADDRS1"].ToString());
                            rdoc.SetParameterValue("ADDRESS2", dtInvestCertHolderInfo.Rows[0]["ADDRS2"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[investment_cert]["ADDRS2"].ToString());
                            rdoc.SetParameterValue("CITY", dtInvestCertHolderInfo.Rows[0]["CITY"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[investment_cert]["CITY"].ToString());
                            rdoc.SetParameterValue("FUND_NAME", dtInvestCertHolderInfo.Rows[0]["FUND_NM"].Equals(DBNull.Value) ? "" : dtInvestCertHolderInfo.Rows[investment_cert]["FUND_NM"].ToString());
                            rdoc.SetParameterValue("TotalAmt", Convert.ToDecimal(dtInvestTotal.Rows[0]["TOTAL_AMOUNT"].Equals(DBNull.Value) ? "0" : dtInvestTotal.Rows[0]["TOTAL_AMOUNT"].ToString()));


                            //  rdoc = ReportFactory.GetReport(rdoc.GetType());
                            string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\INVESTMENT_CERTIFICATE\\" + fund_name;

                            if (!string.IsNullOrEmpty(fileLocation))
                            {
                               // rdoc.ExportToDisk(ExportFormatType.PortableDocFormat, "" + fileLocation + "\\Investment_" + FY + "_" + REG_NO + ".pdf");
                               rdoc.ExportToDisk(ExportFormatType.PortableDocFormat, "" + fileLocation + "\\Investment_" + FY + "_"+ REG_NO+"_" + branchCode.Replace('/', '-') + ".pdf");
                            }


                            //  CrystalReportViewer1.Dispose();
                            CrystalReportViewer1 = null;
                            rdoc.Close();
                            rdoc.Dispose();
                            rdoc = null;
                            GC.Collect();

                        }
                    }


                    UpDate_DIVI_Para(fundCode, FY, closedate, FYPart, action);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Pdf Generate Successfully');", true);

                    Response.Redirect("../AllFundEmailTaxInvestmentCertificate.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data Found');", true);
            }



        }


        public DataTable dtDividendInfo(int regNumber,string branch, string fundCode, string fy, string FYPart, string closedate)
        {

            //StringBuilder sbQuery = new StringBuilder();
            //sbQuery.Append("SELECT DIVIDEND.*,DIVI_PARA.FY_PART ,DIVI_PARA.RATE,DIVI_PARA.TAX_LIMIT FROM UNIT.DIVIDEND INNER JOIN   UNIT.DIVI_PARA ON DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND ");
            //sbQuery.Append(" DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "') AND (DIVIDEND.FY = '" + fy.ToString() + "')  ORDER BY DIVIDEND.DIVI_NO");
            //DataTable dtTaxCal = commonGatewayObj.Select(sbQuery.ToString());
            //return dtTaxCal;


            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtIncomeTax = new DataTable();

            sbQueryString.Append("select * from (select * from ( SELECT  U_MASTER.REG_BK,REPLACE(U_MASTER.REG_BR,'/','-') AS REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
            sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(DIVIDEND.EMAIL) AS EMAIL, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
            sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
            sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
            sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
            sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
            sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQueryString.Append(" WHERE   (U_MASTER.REG_BK = '" + fundCode + "')  AND(DIVI_PARA.F_YEAR = '" + fy + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND(U_MASTER.REG_NO = '"+regNumber+"') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND DIVI_PARA.CLOSE_DT='" + closedate + "')) WHERE REG_BR = '" + branch + "' ");



            dtIncomeTax = commonGatewayObj.Select(sbQueryString.ToString());
           // string q = sbQueryString.ToString();
            return dtIncomeTax;
        }

        public DataTable GET_DIVI_PARA(string fundCode,string FYPart,string closedate,string FY)
        {

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("select * from UNIT.divi_para where FUND_CD='"+ fundCode + "' and close_dt='"+ closedate + "' and F_YEAR='"+ FY + "' and FY_PART='"+ FYPart + "' ");
          
            DataTable dtDivi_para_Info = commonGatewayObj.Select(sbQuery.ToString());
            return dtDivi_para_Info;
        }

        public void UpDate_DIVI_Para(string FundCode,string FY,string closedate, string FYPart ,string Action)
        {
            string strUpdateDivi_para = "";
            if (Action == "PDFGenarate")
            {
                strUpdateDivi_para = "update UNIT.DIVI_PARA set TAX_CERT_FILE_GENERATE = 'Y'  where FUND_CD ='" + FundCode + "' and CLOSE_DT='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "'";
            }
            else if (Action == "InvestmentPDFGenarate")
            {
                strUpdateDivi_para = "update UNIT.DIVI_PARA set INV_CERT_FILE_GENERATE = 'Y'  where FUND_CD ='" + FundCode + "' and CLOSE_DT='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "'";

            }
             
            int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateDivi_para);
            commonGatewayObj.CommitTransaction();
        }
    }
}