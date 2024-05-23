using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class PortfolioSummaryForm : System.Web.UI.Page
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

            DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();
            DataTable dtHowlaDateDropDownList = dropDownListObj.HowlaDateDropDownList();
            if (!IsPostBack)
            {
                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();

                portfolioAsOnDropDownList.DataSource = dtHowlaDateDropDownList;
                portfolioAsOnDropDownList.DataTextField = "Howla_Date";
                portfolioAsOnDropDownList.DataValueField = "VCH_DT";
                portfolioAsOnDropDownList.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["fundCode"] = fundNameDropDownList.SelectedValue.ToString();
            Session["balDate"] = portfolioAsOnDropDownList.SelectedValue.ToString();

            //   ClientScript.RegisterStartupScript(this.GetType(), "PortfolioSummaryReportViewer", "window.open('ReportViewer/PortfolioWithNonListedReportViewer.aspx')", true);
            Response.Redirect("ReportViewer/PortfolioSummaryReportViewer.aspx");


        }
    }
}