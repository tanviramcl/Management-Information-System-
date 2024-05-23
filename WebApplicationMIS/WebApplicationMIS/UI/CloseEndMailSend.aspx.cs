using AMCLBL;
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
    public partial class CloseEndMailSend : System.Web.UI.Page
    {
        DropDownList dropDownListObj = new DropDownList();
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
                DataTable dtFundNameDropDownList = dropDownListObj.CLOSEENDFundNameDropDownList();


                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();

                fyDropDownList.DataSource = dividendDAOObj.dtFY();
                fyDropDownList.DataTextField = "FY";
                fyDropDownList.DataValueField = "FY_VALUE";
                fyDropDownList.DataBind();


                fyDropDownList.CssClass = "form-control";
            }
        }
        protected void fyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            recordDateDropDownList.DataSource = dividendDAOObj.dtRecordDateFYWise(fyDropDownList.SelectedValue.ToString().Trim());
            recordDateDropDownList.DataTextField = "RECORD_DATE";
            recordDateDropDownList.DataValueField = "RECORD_DATE_VALUE";
            recordDateDropDownList.DataBind();
        }
        protected void sendButton_Click(object sender, EventArgs e)
        {
            string FY = "";
            string Record_date = "";
            string Msg = "";



            if (!string.IsNullOrEmpty(fyDropDownList.SelectedItem.Text.ToString()))
            {
                FY = fyDropDownList.SelectedItem.Text.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(recordDateDropDownList.SelectedItem.Text.ToString()))
            {
                Record_date = recordDateDropDownList.SelectedItem.Text.ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select Record Date');", true);
            }
            //var smtpClient = new SmtpClient
            //{
            //    Host = "119.148.45.2",
            //    Port = 25,
            //    EnableSsl = true,
            //    UseDefaultCredentials = true,
            //    Credentials = new System.Net.NetworkCredential
            //    {
            //        UserName = "automail@icbamcl.com.bd",
            //        Password = "(bmJ6eCQltuS"
            //    }
            //};
            var smtpClient = new SmtpClient
            {
                Host = "smtp1.icbamcl.com.bd",
                Port = 25,
                EnableSsl = false,
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential
                {
                    UserName = "automail@icbamcl.com.bd",
                    Password = "(bmJ6eCQltuS"
                }
            };

            //var smtpClient = new SmtpClient
            //{
            //    Host = "mail.icbamcl.com.bd",
            //    Port = 587,
            //    EnableSsl = true,
            //    UseDefaultCredentials = true,
            //    Credentials = new System.Net.NetworkCredential
            //    {
            //        UserName = "automail@icbamcl.com.bd",
            //        Password = "(bmJ6eCQltuS"
            //    }
            //};

          
            string fund_code = fundNameDropDownList.SelectedValue.ToString();
            string fund_name = "";
            DataTable fund_info = FUNDINFO(fundNameDropDownList.Text.ToString());
            if (fund_info != null && fund_info.Rows.Count > 0)
            {
                Boolean result = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                for (int i = 0; i < fund_info.Rows.Count; i++)
                {
                    fund_name = fund_info.Rows[i]["CUSTOMER"].ToString();
                    if (result == true)
                    {
                        Msg = Mail_send(fund_name, FY, Record_date, smtpClient);
                    }
     
                }
            }

            if (Msg != "Error")
            {
                lblProcessing.ForeColor = System.Drawing.Color.Green;
                lblProcessing.Text = Msg;
            }
            else
            {
                lblProcessing.ForeColor = System.Drawing.Color.Red;
                lblProcessing.Text = Msg;
            }

        }

        public string Mail_send(string fund_name, string FY, string Record_date,SmtpClient smtpClient)
        {
            string[] fileArray;
            string finalmsg = "";
            fileArray = get_file(fund_name);


            if (fileArray != null && fileArray.Length != 0)
            {
                foreach (var file in fileArray)
                {
                    //add the file from the fileupload as an attachment
                    var stream = new FileStream(file, FileMode.Open);
                    string filename = Path.GetFileName(file);

                 

                    string file_name = Path.GetFileNameWithoutExtension(file);

                    string[] multiArray = file_name.Split('-');

                    string BO = multiArray[3].ToString();

                    //  dtDividendInfo(Convert.ToInt32(reg_number),);
                    // DataTable dtEmail = dtDividendInfoBYBO(BO, fundNameDropDownList.SelectedValue.ToString(), FY, Record_date);
                    DataTable dtEmail = dropDownListObj.TEST_MAIL_DROP_DOWNLIST();
                    string TO = "";
                    string TO_recipent = "";
                    if (dtEmail != null && dtEmail.Rows.Count > 0)
                    {
                        // TO = dtEmail.Rows[0]["EMAIL"].ToString().ToLower().Trim();

                        //   TO = "tnvraiub@gmail.com";

                        for (int k = 0; k < dtEmail.Rows.Count; k++)
                        {
                            TO_recipent = TO_recipent + "," + dtEmail.Rows[k]["EMAIL"].ToString().ToLower().Trim();
                        }
                        TO = TO_recipent.Remove(0, 1);
                        //TO = "tnvraiub@gmail.com";
                        bool status = valid_mailCheck(TO);


                    
                        if (status == true)
                        {
                            MailMessage msg = new MailMessage();

                            msg.From = new MailAddress("automail@icbamcl.com.bd");
                            msg.Subject = txtSubject.Text.ToString();
                            msg.Body = TextareaMessage.Value.ToString();

                            msg.To.Add(TO);
                            msg.Attachments.Add(new Attachment(stream, Path.GetFileName(file)));

                            //        msg.Attachments.Add(new Attachment(stream, Path.GetFileName(file)));

                            try
                            {
                                smtpClient.Send(msg);
                                msg = new MailMessage();
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message + "BO:" + BO);
                                // Logger(finalmsg, fund_name, FY, Record_date, TO, BO);
                            }
                            string fund_code = fundNameDropDownList.SelectedValue.ToString();
                            string result = DIVIDEND_Update(BO, TO, fund_code, FY, Record_date);
                            if (result == "Success")
                            {
                                finalmsg = result;
                            }
                            else
                            {
                                finalmsg = "Error";
                            }

                            finalmsg = "mail send successfully";

                        }
                        else
                        {
                            finalmsg = "Error";
                            // Logger(finalmsg, fund_name, FY, Cloasing_date, TO, reg_number);

                        }
                    }
                    else
                    {
                        finalmsg = "No Data Found";

                    }



                }
            }
            else
            {
                finalmsg = "Error";

            }


            return finalmsg;
        }

        public string DIVIDEND_Update(string BO,string TO, string fund_code, string FY, string Record_date)
        {
            string msg = "";
            DataTable dtDividend = new DataTable();
            dtDividend = dtDividendInfoBYBO(BO, fundNameDropDownList.SelectedValue.ToString(), FY, Record_date);
            string strUpdateDividend = "";
            if (dtDividend != null && dtDividend.Rows.Count > 0)
            {

                strUpdateDividend = "update AMCL_DIVIDEND.DIVIDEND set TAX_CERT_MAIL_SEND = 'Y', TAX_CERT_MAIL_SEND_DATE='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' where FUND_CODE ='" + fund_code + "' and FY='" + FY + "' and RECORD_DATE='" + Record_date + "' and EMAIL='" + TO + "' and BO=" + BO + " ";

                int noUpdRowsDividend = commonGatewayObj.ExecuteNonQuery(strUpdateDividend);
                commonGatewayObj.CommitTransaction();
                msg = "Success";
            }

            return msg ;
        }
        public DataTable dtDividendInfoBYBO(string BO,string fund_code,string FY,string Record_date)
        {
            DataTable dtCommonData = new DataTable();
            StringBuilder sbMst = new StringBuilder();

            sbMst.Append(" select * from ( select F_CD, BO,EMAIL from(SELECT INVEST.FUND.*, DIVIDEND.*, DIVIDEND_PARA.* FROM AMCL_DIVIDEND.DIVIDEND, INVEST.FUND, AMCL_DIVIDEND.DIVIDEND_PARA WHERE DIVIDEND.FUND_CODE = INVEST.FUND.F_CD ");
            sbMst.Append(" AND DIVIDEND.FUND_CODE = DIVIDEND_PARA.FUND_CODE AND DIVIDEND.DIVI_NO = DIVIDEND_PARA.DIVI_NO AND DIVIDEND.FY = DIVIDEND_PARA.FY AND  DIVIDEND.RECORD_DATE = DIVIDEND_PARA.RECORD_DATE AND (DIVIDEND.FY = '" + FY + "') AND DIVIDEND.RECORD_DATE='" + Record_date + "'   and f_cd = " + fund_code + " ORDER BY DIVIDEND.ID DESC) where BO is not null and f_cd=" + fund_code + "  and ");
            sbMst.Append("EMAIL IS NOT NULL AND EMAIL LIKE'%@%' AND REGEXP_LIKE(EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$')) where BO='"+ BO + "'");

            dtCommonData = commonGatewayObj.Select(sbMst.ToString());
            return dtCommonData;
        }
        public DataTable FUNDINFO(string fundCode)
        {

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT  * FROM INVEST.FUND WHERE F_CD=" + fundCode + "");

            DataTable dtfundInfo = commonGatewayObj.Select(sbQuery.ToString());
            return dtfundInfo;
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
            string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\CLOSE_END\\INCOMETAX_CERTIFICATE\\" + fund_code;
            string[] fileArray = { };
          
                fileArray = Directory.GetFiles(fileLocation);
           

            return fileArray;
        }
    }
}