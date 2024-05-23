using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplicationMIS.UI
{
    public partial class SaleSms : System.Web.UI.Page
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

            DataTable dtFundNameDropDownList = dropDownListObj.UNIT_FundNameDropDownList();
            if (!IsPostBack)
            {
                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "FUND_NM";
                fundNameDropDownList.DataValueField = "FUND_CD";
                fundNameDropDownList.DataBind();


            }

        }

        protected void sendButton_Click(object sender, EventArgs e)
        {
            string message = "";

            string fund_code,sale_number_from,sale_number_to;



            if (!string.IsNullOrEmpty(fundNameDropDownList.Text.Trim().ToString()))
            {
                fund_code = fundNameDropDownList.Text.Trim().ToString();
                Session["fund_code"] = fund_code.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Fund');", true);
            }


            if (!string.IsNullOrEmpty(txtSaleNumberFrom.Text.Trim().ToString()))
            {
                sale_number_from = txtSaleNumberFrom.Text.Trim().ToString();
                Session["sale_number_from"] = sale_number_from.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please Enter sale number from ');", true);
            }

            if (!string.IsNullOrEmpty(txtSaleNumberTo.Text.Trim().ToString()))
            {
                sale_number_to = txtSaleNumberTo.Text.Trim().ToString();
                Session["sale_number_to"] = sale_number_to.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please Enter sale number To ');", true);
            }

            if (!string.IsNullOrEmpty(TextareaMessage.Value.Trim().ToString()))
            {
                message = TextareaMessage.Value.Trim().ToString();
                Session["message"] = message.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please enter your message ');", true);
            }

            string rF = ddlTest.SelectedItem.Value.ToString();
            Session["rF"] = rF.ToString();

            // var task = Task.Run(async () => await Main());
            //// var result = task.WaitAndUnwrapException();

            //var result = AsyncContext.RunTask(Main).Result;

            Response.Redirect("ReportViewer/SaleSmsReportViewer.aspx");

           

        }

   



    }
    
}