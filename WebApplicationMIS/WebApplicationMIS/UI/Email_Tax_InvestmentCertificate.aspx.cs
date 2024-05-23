using AMCLBL;
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
    public partial class Email_Tax_InvestmentCertificate : System.Web.UI.Page
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        UnitReport reportObj = new UnitReport();
        DropDownList dropDownListObj = new DropDownList();
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
                DropdownlistCert.Enabled = false;

                DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
                //DataTable branchdropdownlist = dropDownListObj.BRANCHNameDropDownList();

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

                string FYPart = (string)Session["FY_PART"];
                string Cloasing_date = (string)Session["Cloasing_date"];
                string F_Y = (string)Session["FY"];



                //branchDropDownList.DataSource = branchdropdownlist;
                //branchDropDownList.DataTextField = "BRANCH_NAME";
                //branchDropDownList.DataValueField = "BRANCH_ID";
                //branchDropDownList.DataBind();
                //branchDropDownList.SelectedValue = "AMC/01";

                incomeTaxFYDropDownList.DataSource = reportObj.getDtFY();
                incomeTaxFYDropDownList.DataTextField = "F_YEAR";
                incomeTaxFYDropDownList.DataValueField = "F_YEAR";
                incomeTaxFYDropDownList.DataBind();
                incomeTaxFYDropDownList.Enabled = false;
                incomeTaxFYDropDownList.CssClass = "form-control";
                incomeTaxFYDropDownList.SelectedValue = F_Y;

                fyPartDropDownList.DataSource = reportObj.getDtFYPart();
                fyPartDropDownList.DataTextField = "FY_PART";
                fyPartDropDownList.DataValueField = "FY_PART";
                fyPartDropDownList.DataBind();
                fyPartDropDownList.SelectedValue = FYPart;
                CloasingTextBox.Text = Cloasing_date;
                fyPartDropDownList.Enabled = false;
                fyPartDropDownList.CssClass = "form-control";


                string FY = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
                
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
                    fyFrom = "01-JUL-" + incomeTaxFYDropDownList.SelectedItem.Text.ToString();
                    fyTo = "30-JUN-" + Convert.ToString(Convert.ToUInt16(incomeTaxFYDropDownList.SelectedItem.Text.ToString()) + 1);
                }

                FYFromTextBox.Text = fyFrom;
                FYFromTextBox.Enabled = false;
                FYFromTextBox.CssClass = "form-control";
                FYToTextBox.Text = fyTo;
                FYToTextBox.Enabled = false;
                FYToTextBox.CssClass = "form-control";



                if (string.IsNullOrEmpty(Action))
                {
                    ButtonPdfGenarate.Visible = false;
                    ButtonClear.Visible = false;
                    ButtonMaxREgNumber.Visible = false;

                }
                else if (Action == "PDFGenarate")
                {
                    ButtonPdfGenarate.Visible = true;
                    ButtonClear.Visible = true;
                    ButtonMaxREgNumber.Visible = true;
                    DropdownlistCert.SelectedValue = "1";
                    DropdownlistCert.CssClass = "form-control";


                }
                else if (Action == "InvestmentPDFGenarate")
                {
                    ButtonPdfGenarate.Visible = true;
                    ButtonClear.Visible = true;
                    ButtonMaxREgNumber.Visible = true;
                    DropdownlistCert.SelectedValue = "2";
                    DropdownlistCert.CssClass = "form-control";
                }


            }
           

           

        }

        private DataTable GetFundName()
        {
            DataTable dtFundName = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append(" SELECT     FUND.*    FROM         INVEST.FUND  ");
            sbMst.Append(" WHERE     IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD NOT IN('6','7') AND F_TYPE='OPEN END' ");
            sbOrderBy.Append(" ORDER BY FUND.F_CD ");

            sbMst.Append(sbOrderBy.ToString());
            dtFundName = commonGatewayObj.Select(sbMst.ToString());

            Session["dtFundName"] = dtFundName;
            return dtFundName;
        }
        protected void pdfGenarateButton_Click(object sender, EventArgs e)
        {
            string fundCode = "";
            string fund_type = "";
           
            string Cloasing_date = "";
            string FYFrom = "";
            string FYTO = "";

         

            DataTable dtopenendFund = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL and f_cd="+fundNameDropDownList.SelectedValue.ToString()+"  order by F_cd  ");
            dtopenendFund = commonGatewayObj.Select(sbMst.ToString());

            if (dtopenendFund != null && dtopenendFund.Rows.Count > 0)
            {
                fundCode = dtopenendFund.Rows[0]["OPEN_END_FUND_CD"].ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select an Open End Fund');", true);
            }

            DataTable dtFundInfo = FUNDINFO(fundCode);
            if (dtFundInfo != null && dtFundInfo.Rows.Count > 0)
            {
                fund_type= dtFundInfo.Rows[0]["FUND_TYPE"].ToString();
            }
            if (!string.IsNullOrEmpty(CloasingTextBox.Text.ToString()))
            {


                //DateTime date1 = DateTime.ParseExact(CloasingTextBox.Text, "dd/MM/yyyy", null);
                Cloasing_date = Convert.ToDateTime(CloasingTextBox.Text).ToString("dd-MMM-yyyy");
                Session["Cloasing_date"] = Cloasing_date.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select closing date');", true);
            }

            if (!string.IsNullOrEmpty(fundCode))
            {
                Session["fundCode"] = fundCode.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Fund');", true);
            }
            //if (!string.IsNullOrEmpty(branchDropDownList.Text.Trim().ToString()))
            //{
            //    branchCode = branchDropDownList.Text.Trim().ToString();
            //    if (branchCode != "0")
            //    {
            //        Session["branchCode"] = branchDropDownList.Text.Trim().ToString();
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Branch');", true);
            //    }
               
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Branch');", true);
            //}
            if (!string.IsNullOrEmpty(txtFrom.ToString()))
            {
                Session["RangeFrom"] = txtFrom.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Select Range');", true);
            }
            if (!string.IsNullOrEmpty(txtFrom.ToString()))
            {
                Session["RangeTo"] = txtTo.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please Select Range');", true);
            }
            if (!string.IsNullOrEmpty(incomeTaxFYDropDownList.SelectedItem.Text.ToString()))
            {
                Session["FY"] = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(fyPartDropDownList.SelectedItem.Text.ToString()))
            {
                Session["FY_PART"] = fyPartDropDownList.SelectedItem.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY_PART');", true);
            }

            string Action = Request.QueryString["Action"];
            Session["Action"] = Action;

            if (Action == "PDFGenarate")
            {
               
            }
            else if (Action == "InvestmentPDFGenarate")
            {
                if (!string.IsNullOrEmpty(FYFromTextBox.Text.ToString()))
                {

                   // DateTime dateFYFromTextBox = DateTime.ParseExact(FYFromTextBox.Text, "dd/MM/yyyy", null);
                    FYFrom = Convert.ToDateTime(FYFromTextBox.Text).ToString("dd-MMM-yyyy");
                    Session["FYFrom"] = FYFrom.ToString();

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY From date');", true);
                }

                if (!string.IsNullOrEmpty(FYToTextBox.Text.ToString()))
                {

                   // DateTime dateFYToTextBox = DateTime.ParseExact(FYToTextBox.Text, "dd/MM/yyyy", null);
                    FYTO = Convert.ToDateTime(FYToTextBox.Text).ToString("dd-MMM-yyyy");
                    Session["FYTO"] = FYTO.ToString();

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY To date');", true);
                }

                if (!string.IsNullOrEmpty(ddlInvestmentType.SelectedItem.Text.ToString()))
                {
                    Session["Investment_type"] = ddlInvestmentType.SelectedItem.Text.ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select Investment Type');", true);
                }

            }

           
           

            string tax_file_genarate = "";
            string invest_file_genarate = "";

            if (fund_type == "Open End" )
            {
                if (!string.IsNullOrEmpty(CloasingTextBox.Text.ToString()) && !string.IsNullOrEmpty(incomeTaxFYDropDownList.SelectedItem.Text.ToString()) && !string.IsNullOrEmpty(fyPartDropDownList.SelectedItem.Text.ToString()) )
                {
                    DataTable dtMainFundInfo = Get_FundInfo_FROM_DIVIPARA(fundCode, fyPartDropDownList.SelectedItem.Text.ToString(), Cloasing_date, incomeTaxFYDropDownList.SelectedItem.Text.ToString());
                    if (dtMainFundInfo != null && dtMainFundInfo.Rows.Count > 0)
                    {
                        tax_file_genarate = dtMainFundInfo.Rows[0]["TAX_CERT_FILE_status"].ToString();
                        invest_file_genarate = dtMainFundInfo.Rows[0]["INV_CERT_FILE_GENERATE"].ToString();
                    }

                    if (tax_file_genarate == "Y" && invest_file_genarate == "Y")
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Pdf Already Genarated ');", true);
                        // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('No Data found');", true);
                        lblProcessing.ForeColor = System.Drawing.Color.Green;
                        lblProcessing.Text = " Pdf Already Genarated";
                    }
                    else if (tax_file_genarate == "Y" && invest_file_genarate == "")
                    {
                        Response.Redirect("ReportViewer/UnitReportTaxCertReportViewer.aspx");
                    }
                    else if (tax_file_genarate == "" && invest_file_genarate == "Y")
                    {
                        Response.Redirect("ReportViewer/UnitReportTaxCertReportViewer.aspx");
                    }
                    else
                    {
                        Response.Redirect("ReportViewer/UnitReportTaxCertReportViewer.aspx");

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select....!!!');", true);
                }





            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select an Open End Fund');", true);
            }

        

        }

      
        public DataTable FUNDINFO(string fundCode)
        {

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT  FUND_CD, FUND_NM, FUND_TYPE, FUND_ADDR, FUND_SPONSOR, FUND_MANAGER,FUND_TRUSTEE, FUND_AUDITOR, ATHO_CAP,PAID_CAP, NO_SHRS, FC_VAL,");
            sbQuery.Append(" MIN_MK_LOT, FUND_SUB_OPEN_DT, FUND_SUB_CLOSE_DT,CDS, FUND_DESC, ENT_DT, REMARKS, ENT_TM, MAX_MK_LOT,SL_REP_DIFF, SALE_OF_UNIT_BO, REPURCHASE_OF_UNIT_BO,");
            sbQuery.Append(" OMNIBUS_FOLIO_BO, OE_IPO_SUSPENSE_BO, CSS_COLOUR,BANK_AC_NO, BANK_ROUTING_NO, BANK_AC_TYPE,BANK_CODE, ACCOUNT_SCHEMA, ACC_BANK_CODE,ACC_OP_ID, ACC_TERMINAL_NO, CUST_DP_ID,");
            sbQuery.Append("  REP_BO_NAME, SALE_BO_NAME, ISIN_NO, OMNIBUS_FOLIO_BO_NAME, YEAR_START_MONTH, YEAR_END_MONTH, FUND_CD_INVEST FROM UNIT.FUND_INFO WHERE FUND_CD='" + fundCode + "'");

            DataTable dtfundInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtfundInfo;
        }

        public DataTable Get_FundInfo_FROM_DIVIPARA(string fund_code,string FYPart, string closedate, string FY)
        {

            DataTable dtopenendFund = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" select c.*,d.TAX_CERT_FILE_GENERATE as TAX_CERT_FILE_status,d.INV_CERT_FILE_GENERATE as INV_CERT_FILE_GENERATE from (select a.*,b.f_name,b.f_type from (SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL) a INNER JOIN INVEST.FUND b " +
                " ON a.F_CD = b.F_CD order by a.f_cd) c INNER JOIN UNIT.DIVI_PARA d ON c.OPEN_END_FUND_CD = d.FUND_CD where  close_dt='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "'  and  OPEN_END_FUND_CD='"+ fund_code + "' ");
            dtopenendFund = commonGatewayObj.Select(sbMst.ToString());


            return dtopenendFund;
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            string fund_name = "";
            DataTable dtopenendFund = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL and f_cd=" + fundNameDropDownList.SelectedValue.ToString() + "  order by F_cd  ");
            dtopenendFund = commonGatewayObj.Select(sbMst.ToString());

            if (dtopenendFund != null && dtopenendFund.Rows.Count > 0)
            {
                fund_name = dtopenendFund.Rows[0]["OPEN_END_FUND_CD"].ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select an Open End Fund');", true);
            }

            if (fund_name == "IAMCL")
            {

                Clear(fund_name);

            }
            else if (fund_name == "IAMPH")
            {
                Clear(fund_name);

            }
            else if (fund_name == "BDF")
            {
                Clear(fund_name);

            }
            else if (fund_name == "CFUF")
            {
                Clear(fund_name);

            }
            else if (fund_name == "IUF")
            {

                Clear(fund_name);
            }
            else if (fund_name == "ICB1")
            {

                Clear(fund_name);
            }
            else if (fund_name == "ICB2")
            {
                Clear(fund_name);

            }
            else if (fund_name == "ICB3")
            {
                Clear(fund_name);
            }
            else if (fund_name == "ICB4")
            {
                Clear(fund_name);

            }
            else if (fund_name == "ICB5")
            {

                Clear(fund_name);
            }
            else if (fund_name == "ICB6")
            {

                Clear(fund_name);
            }
            else if (fund_name == "ICB7")
            {
                Clear(fund_name);

            }
            else if (fund_name == "ICB8")
            {
                Clear(fund_name);

            }
            else if (fund_name == "INRB2")
            {
                Clear(fund_name);

            }


            

        }
        protected void ButtonMaxREgNumber_Click(object sender, EventArgs e)
        {
            string fundCode = "";
            string fund_type = "";
            string Cloasing_date = "";
            string FY = "";
            string FYPart = "";
            
            string FYFrom = "";
            string FYTO = "";

            DataTable dtopenendFund = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL and f_cd=" + fundNameDropDownList.SelectedValue.ToString() + "  order by F_cd  ");
            dtopenendFund = commonGatewayObj.Select(sbMst.ToString());

            if (dtopenendFund != null && dtopenendFund.Rows.Count > 0)
            {
                fundCode = dtopenendFund.Rows[0]["OPEN_END_FUND_CD"].ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select an Open End Fund');", true);
            }

            DataTable dtFundInfo = FUNDINFO(fundCode);
            if (dtFundInfo != null && dtFundInfo.Rows.Count > 0)
            {
                fund_type = dtFundInfo.Rows[0]["FUND_TYPE"].ToString();
            }
            if (!string.IsNullOrEmpty(CloasingTextBox.Text.ToString()))
            {

               // DateTime date1 = DateTime.ParseExact(CloasingTextBox.Text, "dd/MM/yyyy", null);
                Cloasing_date = Convert.ToDateTime(CloasingTextBox.Text).ToString("dd-MMM-yyyy");
                Session["Cloasing_date"] = Cloasing_date.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select closing date');", true);
            }

            if (!string.IsNullOrEmpty(fundCode))
            {
                Session["fundCode"] = fundCode.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Fund');", true);
            }
            //if (!string.IsNullOrEmpty(branchDropDownList.Text.Trim().ToString()))
            //{
            //    branchCode = branchDropDownList.Text.Trim().ToString();
            //    if (branchCode != "0")
            //    {
            //        Session["branchCode"] = branchDropDownList.Text.Trim().ToString();
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Branch');", true);
            //    }

            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Branch');", true);
            //}
            if (!string.IsNullOrEmpty(incomeTaxFYDropDownList.SelectedItem.Text.ToString()))
            {
                FY = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(fyPartDropDownList.SelectedItem.Text.ToString()))
            {
                FYPart = fyPartDropDownList.SelectedItem.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY_PART');", true);

            }

        

            string Action = Request.QueryString["Action"];


            if (Action == "PDFGenarate")
            {
                DataTable dtTaxCal = dtDividendInfo(fundCode, FY, FYPart, Cloasing_date,Action);

                if (dtTaxCal != null && dtTaxCal.Rows.Count > 0)
                {
                    //ButtonMaxREgNumber.Text = dtTaxCal.Rows[0]["Reg_no"].ToString();
                    ButtonMaxREgNumber.Text = "Max Reg number:" + dtTaxCal.Rows[0]["Reg_no"].ToString() + ",Number of pdf:" + dtTaxCal.Rows[0]["numberofPdf"].ToString();
                }

            }
            else if (Action == "InvestmentPDFGenarate")
            {
                if (!string.IsNullOrEmpty(FYFromTextBox.Text.ToString()))
                {

                    // DateTime dateFYFromTextBox = DateTime.ParseExact(FYFromTextBox.Text, "dd/MM/yyyy", null);
                    FYFrom = Convert.ToDateTime(FYFromTextBox.Text).ToString("dd-MMM-yyyy");

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY From date');", true);
                }

                if (!string.IsNullOrEmpty(FYToTextBox.Text.ToString()))
                {

                    // DateTime dateFYToTextBox = DateTime.ParseExact(FYToTextBox.Text, "dd/MM/yyyy", null);
                    FYTO = Convert.ToDateTime(FYToTextBox.Text).ToString("dd-MMM-yyyy");


                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY To date');", true);
                }

                DataTable InvestCertHolderInfo = dtDividendInfo(fundCode, FY, FYPart, Cloasing_date, Action);


                if (InvestCertHolderInfo != null && InvestCertHolderInfo.Rows.Count > 0)
                {
                    ButtonMaxREgNumber.Text ="Max Reg number:"+ InvestCertHolderInfo.Rows[0]["Reg_no"].ToString();
                }
            }

            

        }
        public void Clear (string fund_code)
        {

            string Action = Request.QueryString["Action"];
            string[] files= { };

            string fileLocation = ""; 
            if (Action == "PDFGenarate")
            {
                fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\INCOMETAX_CERTIFICATE\\" + fund_code;
                files = Directory.GetFiles(fileLocation);
            }
            else if (Action == "InvestmentPDFGenarate")
            {
                fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\INVESTMENT_CERTIFICATE\\" + fund_code;
                files = Directory.GetFiles(fileLocation);
            }

                
           

            //string[] files = Directory.GetFiles(rootFolder);
            foreach (string file in files)
            {
                File.Delete(file);
               
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('PDF clear successfully');", true);
        }

        public DataTable dtDividendInfo( string fundCode, string fy, string FYPart, string closedate,string Action)
        {

            //StringBuilder sbQuery = new StringBuilder();
            //sbQuery.Append("SELECT DIVIDEND.*,DIVI_PARA.FY_PART ,DIVI_PARA.RATE,DIVI_PARA.TAX_LIMIT FROM UNIT.DIVIDEND INNER JOIN   UNIT.DIVI_PARA ON DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND ");
            //sbQuery.Append(" DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "') AND (DIVIDEND.FY = '" + fy.ToString() + "')  ORDER BY DIVIDEND.DIVI_NO");
            //DataTable dtTaxCal = commonGatewayObj.Select(sbQuery.ToString());
            //return dtTaxCal;
            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtPdfinfo = new DataTable();
            if (Action == "PDFGenarate")
            {


                sbQueryString.Append("select max(reg_no)  as Reg_no, count(reg_no) as numberofPdf from ( SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
                sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(TRIM(DIVIDEND.EMAIL)) AS EMAIL , U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
                sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
                sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
                sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
                sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
                sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
                sbQueryString.Append(" WHERE (U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + fy + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND  REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVI_PARA.CLOSE_DT='" + closedate + "') ");



                dtPdfinfo = commonGatewayObj.Select(sbQueryString.ToString());
            }
            else if(Action == "InvestmentPDFGenarate")
            {
                sbQueryString.Append("select max(reg_no)  as Reg_no, count(reg_no) as numberofPdf from   ( SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
                sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(TRIM(DIVIDEND.EMAIL)) AS EMAIL, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
                sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
                sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
                sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE,DIVIDEND.TAX_CERT_MAIL_SEND  FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
                sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
                sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
                sbQueryString.Append(" WHERE (U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + fy + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVI_PARA.CLOSE_DT='" + closedate + "' )  ");

                dtPdfinfo = commonGatewayObj.Select(sbQueryString.ToString());

            }
          
          
            // string q = sbQueryString.ToString();
            return dtPdfinfo;
        }





       

    }
}