using AMCLBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationMIS.Models;

namespace WebApplicationMIS.UI
{
    public partial class AddBranch : System.Web.UI.Page
    {
        BranchManagment barnchmanagment = new BranchManagment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("../Default.aspx");
            }
            DataTable dtGET_Branch_ID = barnchmanagment.Get_BRANCH_ID();
            string Branch_ID = dtGET_Branch_ID.Rows[0]["BRANCH_ID"].ToString();
            TextBox_Branch_iD.Text = Branch_ID;
        }

        [System.Web.Services.WebMethod]

        public static string InsertUpadateBranch(string Barnchmdl)
        {
            string msg = "";
            BranchManagment barnchmanagment = new BranchManagment();
            DataTable dtBarnchyid = null;

         
            Hashtable htbranchlst = new Hashtable();
            List<BranchClass> lstdesignation = new List<BranchClass>();
            List<BranchClass> mdl = new JavaScriptSerializer().Deserialize<List<BranchClass>>(Barnchmdl);
            foreach (BranchClass barnch in mdl)
            {

                htbranchlst.Add("BRANCH_ID", barnch.BRANCH_ID.ToString());
                htbranchlst.Add("BRANCH_NAME", barnch.BRANCH_NAME.ToString());
                htbranchlst.Add("BRANCH_ADDRESS", barnch.BRANCH_ADDRESS.ToString());


            }
            foreach (var item in mdl)
            {

                dtBarnchyid = barnchmanagment.Get_BRANCH_ByID(item.BRANCH_ID.ToString());

            }

            if (dtBarnchyid != null && dtBarnchyid.Rows.Count > 0)
            {

                // complainResisterfunction.UpdateComplainResiter(htcomplainResister, mdl[0].Complain_ID);
                barnchmanagment.UpdateBarnch(htbranchlst, mdl[0].BRANCH_ID);
                msg = "Update Sucessfully";
            }
            else
            {

                barnchmanagment.SaveBarnch(htbranchlst);
                msg = "Save Sucessfully";

            }




            return msg;

        }
    }
}