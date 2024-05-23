using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class CLOSE_END_EMAILSEND : System.Web.UI.Page
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

            DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownListCLOSEEND();
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

            string FUND_CODE = "";
            string finalmsg = "";

            if (!string.IsNullOrEmpty(fundNameDropDownList.Text.Trim().ToString()) && fundNameDropDownList.Text.Trim().ToString() != "0")
            {
                FUND_CODE = fundNameDropDownList.Text.Trim().ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Fund');", true);
            }



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
                        UserName = "automail@icbamcl.com.bd",
                        Password = "(bmJ6eCQltuS"
                    }
                };


                // bool status = IsConnectedToInternet();

                string fund_code = fundNameDropDownList.SelectedValue.ToString();
                string fund_name = "";
                string rst = "";

                DataTable fund_info = FUNDINFO(fundNameDropDownList.Text.ToString());

                if (fund_info != null && fund_info.Rows.Count > 0)
                {
                    for (int i = 0; i < fund_info.Rows.Count; i++)
                    {
                        fund_name = fund_info.Rows[i]["CUSTOMER"].ToString();
                        Boolean result = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                        if (result == true)
                        {


                            if ((FileUpload.PostedFile != null) && (FileUpload.PostedFile.ContentLength > 0))
                            {
                                string file_rename = "PortFolio" + "_" + fund_name + ".pdf";
                                string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\PORTFOLIO\\CLOSE_END\\" + fund_name + "\\";
                                string SaveLocation = fileLocation + file_rename;
                                if (!File.Exists(SaveLocation))
                                {
                                    FileUpload.PostedFile.SaveAs(SaveLocation);

                                    int count = 0;

                                    DataTable dtEmail = dropDownListObj.TEST_MAIL_DROP_DOWNLIST();
                                    string TO = "";
                                    if (dtEmail != null && dtEmail.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dtEmail.Rows.Count; k++)
                                        {
                                            TO = dtEmail.Rows[k]["EMAIL"].ToString().ToLower().Trim();
                                            bool status = valid_mailCheck(TO);
                                            if (status == true)
                                            {
                                                MailMessage msg = new MailMessage();
                                                msg.From = new MailAddress("automail@icbamcl.com.bd");
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
                finalmsg = "Error Mail  Configaration";
                finalmsg = ex.StackTrace.ToString();
                lblProcessing.ForeColor = System.Drawing.Color.Red;
                lblProcessing.Text = finalmsg;
            }

        }

        public DataTable FUNDINFO(string fundCode)
        {

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT  * FROM INVEST.FUND WHERE F_CD=" + fundCode + "");

            DataTable dtfundInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtfundInfo;
        }

        public string PFOLO_DATE_Update(string TO, string fund_code)
        {
            string msg = "";
            DataTable dtUmaster = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" select * from AMCL_DIVIDEND.CDBL_EMAIL where  FUND_CODE='" + fund_code + "'  and EMAIL='" + TO + "'  ");
            dtUmaster = commonGatewayObj.Select(sbMst.ToString());


            string strUpdateUmaster = "";
            if (dtUmaster != null && dtUmaster.Rows.Count > 0)
            {

                strUpdateUmaster = "update AMCL_DIVIDEND.CDBL_EMAIL  set  LAST_PFOLIO_SEND_DATE='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' where FUND_CODE ='" + fund_code + "'  and EMAIL='" + TO + "'";

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
        public string[] get_file(string fund_code)
        {
            string[] fileArray = { };

            fileArray = Directory.GetFiles(@"D:\Data\PORTFOLIO\CLOSE_END\" + fund_code + "");


            return fileArray;
        }

        public bool IsConnectedToInternet()
        {
            string host = "https://www.google.com";
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch (Exception ex)
            {
                string exp = ex.StackTrace.ToString();
            }
            return result;
        }
        public DataTable GET_EMAIL_FROM_CDBL(string fundCode)
        {
            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtOpenEndEMAIL = new DataTable();

            sbQueryString.Append(" select distinct(trim(LOWER(EMAIL))) EMAIL from AMCL_DIVIDEND.CDBL_EMAIL   ");
            sbQueryString.Append("  where FUND_CODE='" + fundCode + "' AND EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND ");
            sbQueryString.Append(" REGEXP_LIKE (EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') ");



            // string q = sbQueryString.ToString();
            dtOpenEndEMAIL = commonGatewayObj.Select(sbQueryString.ToString());
            //string q = sbQueryString.ToString();
            return dtOpenEndEMAIL;
        }

    }
}