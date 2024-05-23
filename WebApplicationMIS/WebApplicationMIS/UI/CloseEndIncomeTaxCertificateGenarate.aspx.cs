using AMCLBL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class CloseEndIncomeTaxCertificateGenarate : System.Web.UI.Page
    {
        DropDownList dropDownListObj = new DropDownList();
        DividendDAO dividendDAOObj = new DividendDAO();
        CommonGateway commonGatewayObj = new CommonGateway();
        private ReportDocument rdoc = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            if (!IsPostBack)
            {

                string Fund_id = Request.QueryString["F_CD"];
                string Action = Request.QueryString["Action"];

                string FY = (string)Session["FY"];
                string Record_date = (string)Session["Record_date"];


                DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
              

                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();

                fundNameDropDownList.Enabled = false;
                fundNameDropDownList.CssClass = "form-control";
                if (string.IsNullOrEmpty(Fund_id))
                {
                    Fund_id = "0";
                }
                else
                {
                    fundNameDropDownList.SelectedValue = Fund_id;
                }

                fyDropDownList.DataSource = dividendDAOObj.dtFY();
                fyDropDownList.DataTextField = "FY";
                fyDropDownList.DataValueField = "FY_VALUE";
                fyDropDownList.DataBind();
                fyDropDownList.SelectedValue = FY;



                fyDropDownList.CssClass = "form-control";


                recordDateDropDownList.DataSource = dividendDAOObj.dtRecordDateFYWise(fyDropDownList.SelectedValue.ToString().Trim());
                recordDateDropDownList.DataTextField = "RECORD_DATE";
                recordDateDropDownList.DataValueField = "RECORD_DATE_VALUE";
                recordDateDropDownList.DataBind();
                recordDateDropDownList.SelectedValue = Record_date;
                recordDateDropDownList.CssClass = "form-control";



                string FYPart = (string)Session["FY_PART"];


               
                    if (Action == "PDFGenarate")
                    {
                        ButtonPdfGenarate.Visible = true;
                        ButtonClear.Visible = true;
                        ButtonMaxREgNumber.Visible = true;



                    }
                    else
                    {
                        ButtonPdfGenarate.Visible = false;
                        ButtonClear.Visible = false;
                       ButtonMaxREgNumber.Visible = false;
                    }

             
               
               


            }

        }

        protected void ButtonMaxREgNumber_Click(object sender, EventArgs e)
        {
            string FY = "";
            string Record_date = "";



            if (!string.IsNullOrEmpty(fyDropDownList.SelectedItem.Text.ToString()))
            {
                FY = fyDropDownList.SelectedItem.Text.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(recordDateDropDownList.SelectedItem.Text.ToString()))
            {
                Record_date = recordDateDropDownList.SelectedItem.Text.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select Record Date');", true);
            }
            DataTable dtCommonData = new DataTable();
            StringBuilder sbMst = new StringBuilder();

            sbMst.Append(" select count(Bo) AS numberofPdf  from (select distinct(BO) from(SELECT INVEST.FUND.*, DIVIDEND.*, DIVIDEND_PARA.* FROM AMCL_DIVIDEND.DIVIDEND, INVEST.FUND, AMCL_DIVIDEND.DIVIDEND_PARA WHERE DIVIDEND.FUND_CODE = INVEST.FUND.F_CD ");
            sbMst.Append(" AND DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND DIVIDEND.FY = DIVIDEND_PARA.FY AND  DIVIDEND.RECORD_DATE = DIVIDEND_PARA.RECORD_DATE AND (DIVIDEND.FY = '" + FY + "') AND DIVIDEND.RECORD_DATE='" + Record_date + "'   and f_cd = " + fundNameDropDownList.SelectedValue.ToString() + " ORDER BY DIVIDEND.ID DESC) where BO is not null and f_cd=" + fundNameDropDownList.SelectedValue.ToString() + "  and ");
            sbMst.Append("EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND REGEXP_LIKE(EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$'))");

            dtCommonData = commonGatewayObj.Select(sbMst.ToString());

            if (dtCommonData != null && dtCommonData.Rows.Count > 0)
            {
                ButtonMaxREgNumber.Text = "Number of pdf:" + dtCommonData.Rows[0]["numberofPdf"].ToString();
            }
        }

        protected void pdfGenarateButton_Click(object sender, EventArgs e)
        {
            string FY = "";
            string Record_date = "";



            if (!string.IsNullOrEmpty(fyDropDownList.SelectedItem.Text.ToString()))
            {
                FY = fyDropDownList.SelectedItem.Text.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(recordDateDropDownList.SelectedItem.Text.ToString()))
            {
                Record_date = recordDateDropDownList.SelectedItem.Text.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select Record Date');", true);
            }

            string tax_file_genarate = "";

            if (!string.IsNullOrEmpty(recordDateDropDownList.SelectedItem.Text.ToString()) && !string.IsNullOrEmpty(fyDropDownList.SelectedItem.Text.ToString()) && !string.IsNullOrEmpty(fundNameDropDownList.SelectedValue.ToString()))
            {
                DataTable dtMainFundInfo = dividendDAOObj.Get_FundInfo_FROM_DIVIPARABYFUND(FY, Record_date, fundNameDropDownList.SelectedValue.ToString());
                tax_file_genarate = dtMainFundInfo.Rows[0]["TAX_CERT_FILE_GENERATE"].ToString();

                if (tax_file_genarate == "Y" )
                {
                    lblProcessing.ForeColor = System.Drawing.Color.Red;
                    lblProcessing.Text = "Pdf Already Genarated";
                    
                    //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Pdf Already Genarated ');", true);
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ClosePopup","alert('Pdf Already Genarated ');", true);
                }
                else
                {
                    DataTable dtCommonData = new DataTable();
                    StringBuilder sbMst = new StringBuilder();

                    sbMst.Append(" select distinct(BO) from(SELECT INVEST.FUND.*, DIVIDEND.*, DIVIDEND_PARA.* FROM AMCL_DIVIDEND.DIVIDEND, INVEST.FUND, AMCL_DIVIDEND.DIVIDEND_PARA WHERE DIVIDEND.FUND_CODE = INVEST.FUND.F_CD ");
                    sbMst.Append(" AND DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND DIVIDEND.FY = DIVIDEND_PARA.FY AND  DIVIDEND.RECORD_DATE = DIVIDEND_PARA.RECORD_DATE AND (DIVIDEND.FY = '" + FY + "') AND DIVIDEND.RECORD_DATE='" + Record_date + "'   and f_cd = " + fundNameDropDownList.SelectedValue.ToString() + " ORDER BY DIVIDEND.ID DESC) where BO is not null and f_cd=" + fundNameDropDownList.SelectedValue.ToString() + "  and ");
                    sbMst.Append("EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND REGEXP_LIKE(EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$')");

                    dtCommonData = commonGatewayObj.Select(sbMst.ToString());

                    if (dtCommonData != null && dtCommonData.Rows.Count > 0)
                    {
                        for (int incmtax = 0; incmtax < dtCommonData.Rows.Count; incmtax++)
                        {
                            DataTable dtDeliveredWarrantInfo = dividendDAOObj.Get_WarrentInfo_BY_BO(FY, Record_date, fundNameDropDownList.SelectedValue.ToString(), dtCommonData.Rows[incmtax]["BO"].ToString());
                            if (dtDeliveredWarrantInfo != null && dtDeliveredWarrantInfo.Rows.Count > 0)
                            {
                                DataTable dtTaxCertificate = new DataTable();
                                dtTaxCertificate = dividendDAOObj.GetWarrantInfoTax(dtDeliveredWarrantInfo.Rows[0]["ID"].ToString());

                                if (dtTaxCertificate.Rows.Count > 0)
                                {


                                    dtTaxCertificate.TableName = "dtTaxCertificate";
                                    //dtIntimationReport.WriteXmlSchema(@"E:\Projects\Web\AMCL.Dividend\UI\ReportViewer\Report\dtIntimationCashDividend.xsd");

                                    //ReportDocument rdoc = new ReportDocument();
                                    string Path = "";
                                    Path = Server.MapPath("ReportViewer/Report/crtIncomeTaxReport.rpt");
                                    rdoc.Load(Path);
                                    rdoc.SetDataSource(dtTaxCertificate);
                                    DataTable fund_info = FUNDINFO(fundNameDropDownList.Text.ToString());

                                    string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\CLOSE_END\\INCOMETAX_CERTIFICATE\\" + fund_info.Rows[0]["CUSTOMER"].ToString();

                                    if (!string.IsNullOrEmpty(fileLocation))
                                    {
                                        rdoc.ExportToDisk(ExportFormatType.PortableDocFormat, "" + fileLocation + "\\TAX-" + FY + "-" + dtCommonData.Rows[incmtax]["BO"].ToString() + ".pdf");
                                        //rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "crtIncomeTaxReport" + DateTime.Now + ".pdf");
                                    }


                                    //rdoc.Close();
                                    //rdoc.Dispose();
                                    //rdoc = null;
                                    //GC.Collect();
                                    UpDate_DIVI_Para(fundNameDropDownList.SelectedValue.ToString(), FY, Record_date);
                                    lblProcessing.ForeColor = System.Drawing.Color.Green;
                                    lblProcessing.Text = "Pdf Generate Successfully";
                                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Pdf Generate Successfully');", true);
                                }
                                else
                                {
                                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data found');", true);
                                    lblProcessing.ForeColor = System.Drawing.Color.Red;
                                    lblProcessing.Text = "No Data found";
                                }

                            }
                        }
                    }

                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please Full fill your Recuirment');", true);
            }


        }

        public void UpDate_DIVI_Para(string FundCode, string FY, string record_date)
        {
            string strUpdateDivi_para = "";
           
            strUpdateDivi_para = "update AMCL_DIVIDEND.DIVIDEND_PARA set TAX_CERT_FILE_GENERATE = 'Y'  where FUND_CODE ='" + FundCode + "' and RECORD_DATE='" + record_date + "' and FY='" + FY + "'";

            int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateDivi_para);
            commonGatewayObj.CommitTransaction();
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            
            string[] files = { };


            DataTable fund_info = FUNDINFO(fundNameDropDownList.Text.ToString());

            string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\CLOSE_END\\INCOMETAX_CERTIFICATE\\" + fund_info.Rows[0]["CUSTOMER"].ToString();

            files = Directory.GetFiles(fileLocation);
            
           




            //string[] files = Directory.GetFiles(rootFolder);
            foreach (string file in files)
            {
                File.Delete(file);

            }
            // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('PDF clear successfully');", true);
            lblProcessing.ForeColor = System.Drawing.Color.Red;
            lblProcessing.Text = "PDF clear successfully";

        }
        public DataTable FUNDINFO(string fundCode)
        {

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT  * FROM INVEST.FUND WHERE F_CD=" + fundCode + "");

            DataTable dtfundInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtfundInfo;
        }
        protected void fyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            recordDateDropDownList.DataSource = dividendDAOObj.dtRecordDateFYWise(fyDropDownList.SelectedValue.ToString().Trim());
            recordDateDropDownList.DataTextField = "RECORD_DATE";
            recordDateDropDownList.DataValueField = "RECORD_DATE_VALUE";
            recordDateDropDownList.DataBind();
        }
    }
}