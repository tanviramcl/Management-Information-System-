using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace WebApplicationMIS.UI
{
    public partial class OPEN_END_MAILSEND : System.Web.UI.Page
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

        }

        protected void sendButton_Click(object sender, EventArgs e)
        {

            string finalmsg = "";
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

                 Boolean result = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                int count = 0;
                if (result == true)
                 {
                    if ((FileUpload.PostedFile != null) && (FileUpload.PostedFile.ContentLength > 0))
                    {
                        string file_rename = FileUpload.PostedFile.FileName.ToString();
                        string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\MAILAttachment\\";
                        string SaveLocation = fileLocation + file_rename;

                        if (!File.Exists(SaveLocation))
                        {
                            FileUpload.PostedFile.SaveAs(SaveLocation);

                            DataTable dtEmail = dropDownListObj.Get_investors();
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

                                if (count > 0)
                                {
                                    lblProcessing.ForeColor = System.Drawing.Color.Green;
                                    lblProcessing.Text = count + " mail Send sucessfully";
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
                    else
                    {
                       
                        DataTable dtEmail = dropDownListObj.TEST_MAIL_DROP_DOWNLIST();
                        //  DataTable dtEmail = GET_EMAIL_FROM_UMASTER();
                        // DataTable dtEmail = GET_EMAIL_ICB_UF();

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

                                    msg.From = new MailAddress("automail@icbamcl.gov.bd");
                                    msg.Subject = txtSubject.Text.ToString();
                                    msg.Body = TextareaMessage.Value.ToString();

                                    msg.To.Add(TO);

                                    try
                                    {
                                        smtpClient.Send(msg);
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
                                    lblProcessing.Text = "Email Incorrect";
                                }

                            }


                            if (count > 0)
                            {
                                lblProcessing.ForeColor = System.Drawing.Color.Green;
                                lblProcessing.Text = count + " mail Send sucessfully";
                            }
                        }
                        else
                        {
                            finalmsg = "No Data Found";

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




        public DataTable GET_EMAIL_FROM_UMASTER()
        {
            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtOpenEndEMAIL = new DataTable();

            sbQueryString.Append(" select distinct(trim(LOWER(EMAIL))) EMAIL from UNIT.U_MASTER ");
            sbQueryString.Append("  where  EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND ");
            sbQueryString.Append(" REGEXP_LIKE (EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') ");

            // string q = sbQueryString.ToString();
            dtOpenEndEMAIL = commonGatewayObj.Select(sbQueryString.ToString());
            //string q = sbQueryString.ToString();
            return dtOpenEndEMAIL;
        }

        public DataTable GET_EMAIL_ICB_UF()
        {
            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtOpenEndEMAIL = new DataTable();

            sbQueryString.Append(" select distinct(trim(LOWER(EMAIL))) EMAIL from UNIT.SMS_MARKETING ");
            sbQueryString.Append("  where SMS_MARKETING.source in('ICB_UF') AND EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND ");
            sbQueryString.Append(" REGEXP_LIKE (EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') ");



            // string q = sbQueryString.ToString();
            dtOpenEndEMAIL = commonGatewayObj.Select(sbQueryString.ToString());
            //string q = sbQueryString.ToString();
            return dtOpenEndEMAIL;
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
  
            fileArray = Directory.GetFiles(@"D:\Data\PORTFOLIO\OPEN_END\" + fund_code + "");


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