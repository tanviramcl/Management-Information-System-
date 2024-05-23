using System;
using AMCLBL;
using System.Data;
using System.Text;

namespace WebApplicationMIS.UI
{
    public partial class AllFundCloseEndTaxICertificate : System.Web.UI.Page
    {
        DividendDAO dividendDAOObj = new DividendDAO();
        CommonGateway commonGatewayObj = new CommonGateway();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }

            if (!IsPostBack)
            {
                fyDropDownList.DataSource = dividendDAOObj.dtFY();
                fyDropDownList.DataTextField = "FY";
                fyDropDownList.DataValueField = "FY_VALUE";
                fyDropDownList.DataBind();

                
            }

        }
        protected void fyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            recordDateDropDownList.DataSource = dividendDAOObj.dtRecordDateFYWise(fyDropDownList.SelectedValue.ToString().Trim());
            recordDateDropDownList.DataTextField = "RECORD_DATE";
            recordDateDropDownList.DataValueField = "RECORD_DATE_VALUE";
            recordDateDropDownList.DataBind();
        }

        protected void ButtonMaxREgNumber_Click(object sender, EventArgs e)
        {
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
          
            string FY = "";
            string Record_date = "";
            


            if (!string.IsNullOrEmpty(fyDropDownList.SelectedItem.Text.ToString()))
            {
                FY = fyDropDownList.SelectedItem.Text.ToString();
                Session["FY"] = FY;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(recordDateDropDownList.SelectedItem.Text.ToString()))
            {
                Record_date = recordDateDropDownList.SelectedItem.Text.ToString();
                Session["Record_date"] = Record_date;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select Record Date');", true);
            }

            DataTable dtMainFundInfo = dividendDAOObj.Get_FundInfo_FROM_DIVIPARA( FY, Record_date);
            if (dtMainFundInfo != null && dtMainFundInfo.Rows.Count > 0)
            {
                Session["dtFundName_closeEnd"] = dtMainFundInfo;
            }
        }

      

    }
}