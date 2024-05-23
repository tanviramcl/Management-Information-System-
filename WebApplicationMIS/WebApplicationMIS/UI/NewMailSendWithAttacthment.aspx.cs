using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class NewMailSendWithAttacthment : System.Web.UI.Page
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

            DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownListOpenEnd();
            if (!IsPostBack)
            {
                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();

            }

        }

        protected void sendButton_Click(object sender, EventArgs e)
        {
            try
            {


                var smtpClient = new SmtpClient
                {
                    Host = "smtp.icbamcl.com.bd",
                    Port = 25,
                    EnableSsl = false,
                    UseDefaultCredentials = true,
                    Credentials = new System.Net.NetworkCredential
                    {
                        UserName = "automail@icbamcl.gov.bd",
                        Password = "(bmJ6eCQltuS"
                    }
                };

                string fund_code = fundNameDropDownList.SelectedValue.ToString();
                string fund_name = "";

                DataTable dtopenendFund = new DataTable();
                StringBuilder sbMst = new StringBuilder();
                sbMst.Append(" SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL and f_cd=" + fund_code + "  order by F_cd  ");
                dtopenendFund = commonGatewayObj.Select(sbMst.ToString());

                if (dtopenendFund != null && dtopenendFund.Rows.Count > 0)
                {
                    for (int i = 0; i < dtopenendFund.Rows.Count; i++)
                    {
                        fund_name = dtopenendFund.Rows[i]["OPEN_END_FUND_CD"].ToString();
                        Boolean result = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                        if (result == true)
                        {
                            int count = 0;
                            string rst = "";

                            if ((FileUpload.PostedFile != null) && (FileUpload.PostedFile.ContentLength > 0))
                            {
                                string file_rename = "PortFolio" + "_" + fund_name + ".pdf";
                                string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\PORTFOLIO\\OPEN_END\\" + fund_name + "\\";
                                string SaveLocation = fileLocation + file_rename;
                                if (!File.Exists(SaveLocation))
                                {
                                    FileUpload.PostedFile.SaveAs(SaveLocation);

                                    DataTable dtEmail = TEST_MAIL();
                                    //DataTable dtEmail = GET_EMAIL_FROM_UMASTER(fund_name);


                                    string TO = "";
                                    if (dtEmail != null && dtEmail.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dtEmail.Rows.Count; k++)
                                        {

                                            lblProcessing.Text = "";
                                            TO = dtEmail.Rows[k]["EMAIL"].ToString().ToLower().Trim();
                                            bool status = valid_mailCheck(TO);
                                            if (status == true)
                                            {
                                                MailMessage msg = new MailMessage();

                                                msg.From = new MailAddress("automail@icbamcl.gov.bd");
                                                msg.Subject = txtSubject.Text.ToString();
                                                msg.Body = TextareaMessage.Value.ToString();

                                                msg.To.Add(TO);

                                                //string attachmentsPath = @"C:\Himasagar";
                                                if (Directory.Exists(fileLocation))
                                                {
                                                    string[] files = Directory.GetFiles(fileLocation);
                                                    foreach (string fileName in files)
                                                    {
                                                        Attachment file = new Attachment(fileName);
                                                        msg.Attachments.Add(file);
                                                    }

                                                    try
                                                    {
                                                        smtpClient.Send(msg);
                                                        rst = PFOLO_DATE_Update(TO, fund_name);
                                                        msg = new MailMessage();
                                                        count++;

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Response.Write(ex.Message);

                                                    }

                                                }
                                                else
                                                {
                                                    lblProcessing.ForeColor = System.Drawing.Color.Red;
                                                    lblProcessing.Text = "File already save on location";
                                                }

                                            }
                                            else
                                            {
                                                lblProcessing.ForeColor = System.Drawing.Color.Red;
                                                lblProcessing.Text = "Email Incorrect";
                                            }
                                        }

                                        if (rst == "Success")
                                        {
                                            lblProcessing.ForeColor = System.Drawing.Color.Green;
                                            lblProcessing.Text = count + " mail Send sucessfully";
                                        }
                                        else
                                        {
                                            lblProcessing.ForeColor = System.Drawing.Color.Red;
                                            lblProcessing.Text = "Email Send With Error";
                                        }
                                       
                                    }
                                    else
                                    {
                                        lblProcessing.ForeColor = System.Drawing.Color.Red;
                                        lblProcessing.Text = "Email Not Found";
                                    }

                                }
                                else
                                {
                                    lblProcessing.ForeColor = System.Drawing.Color.Red;
                                    lblProcessing.Text = "File already save on location";
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                lblProcessing.ForeColor = System.Drawing.Color.Red;
                lblProcessing.Text = "Error Mail Configaration" + ex.StackTrace.ToString(); ; 
            }

        }

        public DataTable TEST_MAIL()//For All Funds
        {

            DataTable dtServiceList = new DataTable();
            dtServiceList.Columns.Add("EMAIL", typeof(string));

            DataRow dr = dtServiceList.NewRow();
            dr["EMAIL"] = "tnvraiub@gmail.com";
            dtServiceList.Rows.Add(dr);

            DataRow dr1 = dtServiceList.NewRow();
            dr1["EMAIL"] = "tanvirjob786@gmail.com";
            dtServiceList.Rows.Add(dr1);

            DataRow dr2 = dtServiceList.NewRow();
            dr2["EMAIL"] = "sakha5413@gmail.com";
            dtServiceList.Rows.Add(dr2);

      

            DataRow dr4 = dtServiceList.NewRow();
            dr4["EMAIL"] = "tanvir@icbamcl.gov.bd";
            dtServiceList.Rows.Add(dr4);

            //DataRow dr5 = dtServiceList.NewRow();
            //dr5["EMAIL"] = "jogonnathbd87@gmail.com";
            //dtServiceList.Rows.Add(dr5);


            DataRow dr7 = dtServiceList.NewRow();
            dr7["EMAIL"] = "fulbabuamcl@gmail.com";
            dtServiceList.Rows.Add(dr7);
            return dtServiceList;
        }
        public string PFOLO_DATE_Update(string TO, string fund_code)
        {
            string msg = "";
            DataTable dtUmaster = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" select * from UNIT.U_MASTER where  REG_BK='" + fund_code + "'  and EMAIL='" + TO + "'  ");
            dtUmaster = commonGatewayObj.Select(sbMst.ToString());


            string strUpdateUmaster = "";
            if (dtUmaster != null && dtUmaster.Rows.Count > 0)
            {

                strUpdateUmaster = "update UNIT.U_MASTER  set  LAST_PFOLIO_SEND_DATE='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' where REG_BK ='" + fund_code + "'  and EMAIL='" + TO + "'";

                int noUpdRows = commonGatewayObj.ExecuteNonQuery(strUpdateUmaster);
                commonGatewayObj.CommitTransaction();
                msg = "Success";
            }

            return msg;
        }

        public bool valid_mailCheck(string mail)
        {
            string email = mail.Trim();
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {
                MailAddress copy = new MailAddress(email);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable GET_EMAIL_FROM_UMASTER(string fundCode)
        {
            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtOpenEndEMAIL = new DataTable();

            sbQueryString.Append(" select distinct(trim(LOWER(EMAIL))) EMAIL from UNIT.U_MASTER ");
            sbQueryString.Append("  where REG_BK='" + fundCode + "' AND EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND ");
            sbQueryString.Append(" REGEXP_LIKE (EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') ");



            // string q = sbQueryString.ToString();
            dtOpenEndEMAIL = commonGatewayObj.Select(sbQueryString.ToString());
            //string q = sbQueryString.ToString();
            return dtOpenEndEMAIL;
        }


    }
}