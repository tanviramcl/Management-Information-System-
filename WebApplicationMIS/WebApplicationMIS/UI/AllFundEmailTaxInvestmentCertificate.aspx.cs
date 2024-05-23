using AMCLBL;
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
    public partial class AllFundEmailTaxInvestmentCertificate : System.Web.UI.Page
    {
        DropDownList ddlist = new DropDownList();
        CommonGateway commonGatewayObj = new CommonGateway();
        UnitReport reportObj = new UnitReport();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            if (!IsPostBack)
            {
                incomeTaxFYDropDownList.DataSource = reportObj.getDtFY();
                incomeTaxFYDropDownList.DataTextField = "F_YEAR";
                incomeTaxFYDropDownList.DataValueField = "F_YEAR";
                incomeTaxFYDropDownList.DataBind();

                fyPartDropDownList.DataSource = reportObj.getDtFYPart();
                fyPartDropDownList.DataTextField = "FY_PART";
                fyPartDropDownList.DataValueField = "FY_PART";
                fyPartDropDownList.DataBind();
            }

           
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string Cloasing_date = "";
            string FY = "";
            string FY_PART = "";
            string f_cd = "";
           
           
            if (!string.IsNullOrEmpty(incomeTaxFYDropDownList.SelectedItem.Text.ToString()))
            {
                 FY = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
                 //FY = "2021";
                Session["FY"] = FY;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(fyPartDropDownList.SelectedItem.Text.ToString()))
            {
                FY_PART = fyPartDropDownList.SelectedItem.Text.ToString();
                Session["FY_PART"] = FY_PART;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY_PART');", true);
            }
            if (!string.IsNullOrEmpty(CloasingTextBox.Text.ToString()))
            {

                DateTime date1 = DateTime.ParseExact(CloasingTextBox.Text, "dd/MM/yyyy", null);
                Cloasing_date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
                Session["Cloasing_date"] = Cloasing_date;

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select closing date');", true);
            }

            DataTable dtfundFromDivipara = GET_DIVI_PARA(FY_PART, Cloasing_date, FY);
            DataTable dtMainFundInfo = Get_FundInfo_FROM_DIVIPARA(FY_PART, Cloasing_date, FY);


            if (dtMainFundInfo != null && dtMainFundInfo.Rows.Count > 0)
            {
                Session["dtFundName"] = dtMainFundInfo;
            }

                // DataTable dtMainFundInfo = new DataTable();


                //for (int i = 0; i < dtfundFromDivipara.Rows.Count; i++)
                //{
                //    f_cd = dtfundFromDivipara.Rows[i]["fund_cd"].ToString();

                //    DataTable dtsubFundInfo = Get_FundInfo(f_cd);




                //    dtMainFundInfo.Merge(dtsubFundInfo);

                //}

             
        }
        public DataTable Get_FundInfo_FROM_DIVIPARA(string FYPart, string closedate, string FY)
        {
          
            DataTable dtopenendFund = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" select c.*,d.TAX_CERT_FILE_GENERATE as TAX_CERT_FILE_status,TAX_CERT_MAIL_SEND as TAX_CERT_MAIL_SEND,d.INV_CERT_FILE_GENERATE as INV_CERT_FILE_STATUS,d.INV_CERT_MAIL_SEND as INV_CERT_MAIL_SEND from (select a.*,b.f_name,b.f_type from (SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL) a INNER JOIN INVEST.FUND b " +
                " ON a.F_CD = b.F_CD order by a.f_cd) c INNER JOIN UNIT.DIVI_PARA d ON c.OPEN_END_FUND_CD = d.FUND_CD where  close_dt='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "'");
            dtopenendFund = commonGatewayObj.Select(sbMst.ToString());

          
            return dtopenendFund;
        }

        public DataTable GET_DIVI_PARA( string FYPart, string closedate, string FY)
        {

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("select * from UNIT.divi_para where  close_dt='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "' ");

            DataTable dtDivi_para_Info = commonGatewayObj.Select(sbQuery.ToString());
            return dtDivi_para_Info;
        }


    }
}