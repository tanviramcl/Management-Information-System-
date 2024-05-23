using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loginErrorLabel.Visible = false;
            loginIDTextBox.Focus();
            if (IsPostBack)
            {
                UserLogin();
            }

        }
        protected void loginButton_Click(object sender, EventArgs e)
        {

            UserLogin();

        }
        public void UserLogin()
        {
            CommonGateway commonGatewayObj = new CommonGateway();
            string loginId = loginIDTextBox.Text.Trim().ToString();
            // string loginPassword = EncodePasswordToBase64(loginPasswardTextBox.Text.Trim());
            string loginPassword = Encrypt(loginPasswardTextBox.Text.Trim());
            string UserType = "";

            DataTable dtUserInfo = new DataTable();
            dtUserInfo = commonGatewayObj.Select("SELECT * FROM USER_INFO WHERE USER_ID='" + loginId + "' AND USER_PASS='" + loginPassword + "' ");
            if (dtUserInfo.Rows.Count > 0)
            {
                string UserID = dtUserInfo.Rows[0]["USER_ID"].ToString();
                string UserName = dtUserInfo.Rows[0]["USER_NM"].ToString();
                string UserLevel = dtUserInfo.Rows[0]["USER_LEVEL"].ToString();


                Session["UserID"] = UserID;
                Session["UserName"] = UserName;
                Session["UserType"] = UserLevel;

                Response.Redirect("UI/Home.aspx");


            }
            else
            {
                loginErrorLabel.Visible = true;
                loginErrorLabel.Text = "Invalid LoginID or Passward";
                loginIDTextBox.Text = "";
                loginPasswardTextBox.Text = "";


            }
        }

        public string Encrypt(string str)
        {
            string encr = null;
            if (str == "")
            {
                return "ÄÆ8¼";
            }
            for (int i = 0; i < str.Length; i++)
            {
                char ch = Convert.ToChar(str.Substring(i, 1));
                int num = ch + 169;
                encr += ((char)(ushort)num).ToString();
            }
            return encr;
        }

    }
}