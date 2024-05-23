using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class MarketValuationWithNonListedSecuritesCompanyAndAllFunds : System.Web.UI.Page
    {


        CommonGateway commonGatewayObj = new CommonGateway();
        DropDownList dropDownListObj = new DropDownList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }

           
            DataTable dtHowlaDateDropDownList = dropDownListObj.HowlaDateDropDownList();
            if (!IsPostBack)
            {
               

                portfolioAsOnDropDownList.DataSource = dtHowlaDateDropDownList;
                portfolioAsOnDropDownList.DataTextField = "Howla_Date";
                portfolioAsOnDropDownList.DataValueField = "VCH_DT";
                portfolioAsOnDropDownList.DataBind();

                DataTable dtNoOfFunds = GetFundName();
                //  DataTable dtFund = obj.GetFundGridTable();

                if (dtNoOfFunds.Rows.Count > 0)
                {


                    //foreach (DataRow dr in dtNoOfFunds.Rows)
                    //{
                    //    ListItem newItem = new ListItem(dr["F_CD"].ToString()+ dr["F_NAME"].ToString());
                    //    chkFruits.Items.Add(newItem);
                    //}

                    chkFruits.DataSource = dtNoOfFunds;
                    chkFruits.DataValueField = "F_CD";
                    chkFruits.DataTextField = "F_NAME";

                    chkFruits.DataBind();

                    //int fundSerial = 1;
                    dvGridFund.Visible = true;
                    //DataRow drdtGridFund;
                    //for (int looper = 0; looper < dtNoOfFunds.Rows.Count; looper++)
                    //{
                    //    drdtGridFund = dtFund.NewRow();
                    //    drdtGridFund["SI"] = fundSerial;
                    //    drdtGridFund["FUND_CODE"] = dtNoOfFunds.Rows[looper]["F_CD"].ToString().ToUpper();
                    //    drdtGridFund["FUND_NAME"] = dtNoOfFunds.Rows[looper]["F_NAME"].ToString().ToUpper();
                    //    dtFund.Rows.Add(drdtGridFund);
                    //    fundSerial++;


                    //chkFruits.DataSource = dtNoOfFunds;
                    //chkFruits.DataBind();


                }


            }
            else
            {
                dvGridFund.Visible = false;
            }
        
        }
        private string SelectFundCode()
        {
            DataTable dtFundName = (DataTable)Session["dtFundName"];
            string fundCode = "";
            int loop = 0;

            for (int i = 0; i < chkFruits.Items.Count; i++)
            {
                if (chkFruits.Items[i].Selected)
                {
                    if (fundCode.ToString() == "")
                    {
                        fundCode = dtFundName.Rows[loop]["F_CD"].ToString();
                    }
                    else
                    {
                        fundCode = fundCode + "," + dtFundName.Rows[loop]["F_CD"].ToString();
                    }
                }
                loop++;
            }
            return fundCode;

        }
        private DataTable GetFundName()
        {
            DataTable dtFundName = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append(" SELECT     FUND.F_CD, FUND.F_NAME     FROM         INVEST.FUND  ");
            sbMst.Append(" WHERE     IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD NOT IN('6','7') ");
            sbOrderBy.Append(" ORDER BY FUND.F_CD ");

            sbMst.Append(sbOrderBy.ToString());
            dtFundName = commonGatewayObj.Select(sbMst.ToString());

            Session["dtFundName"] = dtFundName;
            return dtFundName;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Session["fundCode"] = "1";
            //Session["balDate"] = portfolioAsOnDropDownList.SelectedValue.ToString();

            ////   ClientScript.RegisterStartupScript(this.GetType(), "PortfolioSummaryReportViewer", "window.open('ReportViewer/PortfolioWithNonListedReportViewer.aspx')", true);
            //Response.Redirect("ReportViewer/PortfolioWithNonListedReportViewer.aspx");

            Session["fundCodes"] = SelectFundCode();

            if (string.IsNullOrEmpty(Session["fundCodes"] as string))
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('Please check mark at least one fund!');", true);
                dvGridFund.Visible = true;
            }
            else
            {

                Session["PortfolioAsOnDate"] = portfolioAsOnDropDownList.SelectedValue.ToString();
                //Session["percentageCheck"] = percentageTextBox.Text.ToString();
                //Session["companyCodes"] = companyCodeTextBox.Text.ToString();
                dvGridFund.Visible = true;
                //  ClientScript.RegisterStartupScript(this.GetType(), "MarketValuationWithProfitLoss", "window.open('ReportViewer/MarketValuationWithProfitLossReportViewer.aspx')", true);
                Response.Redirect("ReportViewer/MarketValuationWithProfitLossReportViewer.aspx");
            }


        }
    }
}