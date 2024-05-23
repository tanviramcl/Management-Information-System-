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
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        protected void Page_Load(object sender, EventArgs e)
        {
            string referer = Request.ServerVariables["HTTP_REFERER"];
            if (string.IsNullOrEmpty(referer))
            {
                Session["UserID"] = null;
                Response.Redirect("../Default.aspx");
            }
            Session["dtPrimarySatus"] = PrimarySatusCount();
            Session["dtCloseSatus"] = CloseSatusCount();
            Session["dtProcessingSatus"] = dtProcessingSatus();
            Session["dtTotalComplain"] = dtTotalComplain();
            string UserType = Session["UserType"].ToString();

            Session["UserType"] = UserType;


        }
        public DataTable PrimarySatusCount()
        {
            DataTable dtPrimarysatus = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append("select count(STATUS) as STATUS from COMPLAIN_REGISTER where STATUS='Primary'");

            sbMst.Append(sbOrderBy.ToString());
            dtPrimarysatus = commonGatewayObj.Select(sbMst.ToString());

            Session["dtPrimarySatus"] = dtPrimarysatus;
            return dtPrimarysatus;
        }
        public DataTable CloseSatusCount()
        {
            DataTable dtCloseSatus = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append("select count(STATUS) as STATUS from COMPLAIN_REGISTER where STATUS='Close'");

            sbMst.Append(sbOrderBy.ToString());
            dtCloseSatus = commonGatewayObj.Select(sbMst.ToString());

            Session["dtCloseSatus"] = dtCloseSatus;
            return dtCloseSatus;
        }
        public DataTable dtProcessingSatus()
        {
            DataTable dtProcessingSatus = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append("select count(STATUS) as STATUS from COMPLAIN_REGISTER where STATUS='Processing'");

            sbMst.Append(sbOrderBy.ToString());
            dtProcessingSatus = commonGatewayObj.Select(sbMst.ToString());

            Session["dtProcessingSatus"] = dtProcessingSatus;
            return dtProcessingSatus;
        }

        public DataTable dtTotalComplain()
        {
            DataTable dtProcessingSatus = new DataTable();

            StringBuilder sbMst = new StringBuilder();
            StringBuilder sbOrderBy = new StringBuilder();
            sbOrderBy.Append("");

            sbMst.Append("select COUNT( COMPLAIN_ID ) as Id from COMPLAIN_REGISTER ");

            sbMst.Append(sbOrderBy.ToString());
            dtProcessingSatus = commonGatewayObj.Select(sbMst.ToString());

            Session["dtTotalComplain"] = dtProcessingSatus;
            return dtProcessingSatus;
        }
    }
}