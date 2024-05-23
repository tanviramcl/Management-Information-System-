using AMCLBL;
using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApplicationMIS.UI
{
    public partial class MailSend : System.Web.UI.Page
    {
        DropDownList dropDownListObj = new DropDownList();
        UnitReport reportObj = new UnitReport();
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

                DataTable dtFundNameDropDownList = get_fund_fromDiviPara();
                DataTable branchdropdownlist = dropDownListObj.BRANCHNameDropDownList();


                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();

                //branchDropDownList.DataSource = branchdropdownlist;
                //branchDropDownList.DataTextField = "BRANCH_NAME";
                //branchDropDownList.DataValueField = "BRANCH_ID";
                //branchDropDownList.DataBind();
                //branchDropDownList.SelectedValue = "AMC/01";

                incomeTaxFYDropDownList.DataSource = reportObj.getDtFY();
                incomeTaxFYDropDownList.DataTextField = "F_YEAR";
                incomeTaxFYDropDownList.DataValueField = "F_YEAR";
                incomeTaxFYDropDownList.DataBind();

                fyPartDropDownList.DataSource = reportObj.getDtFYPart();
                fyPartDropDownList.DataTextField = "FY_PART";
                fyPartDropDownList.DataValueField = "FY_PART";
                fyPartDropDownList.DataBind();



            }

            string FY = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
            string fyFrom = " ";
            string fyTo = " ";

            string[] divideFY = FY.Split('-');
            if (divideFY.Length > 1)
            {
                fyFrom = "01-JUL-" + divideFY[0].ToString();
                fyTo = "30-JUN-" + divideFY[1].ToString();
            }
            else
            {
                fyFrom = "01-JUL-" + incomeTaxFYDropDownList.SelectedItem.Text.ToString();
                fyTo = "30-JUN-" + Convert.ToString(Convert.ToUInt16(incomeTaxFYDropDownList.SelectedItem.Text.ToString()) + 1);
            }

            FYFromTextBox.Text = fyFrom;
            FYToTextBox.Text = fyTo;

        }

        protected void Certificatetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Action = DropdownlistCert.SelectedValue.ToString();
            if (Action == "1")
            {
                DataTable dtFundNameDropDownList = get_fund_fromDiviPara();
                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();
            }
            else {
                DataTable dtFundNameDropDownList = get_fund_fromDiviParaInvestMentCert();
                fundNameDropDownList.DataSource = dtFundNameDropDownList;
                fundNameDropDownList.DataTextField = "F_NAME";
                fundNameDropDownList.DataValueField = "F_CD";
                fundNameDropDownList.DataBind();
            }
        }
        protected void sendButton_Click(object sender, EventArgs e)
        {
            string finalmsg = "";
            string Cloasing_date = "";
            string FY = "";
            string FY_PART = "";
            //string branchCode = "";
            string FUND_CODE = "";
            string FYFrom = "";
            string FYTO = "";
            string Msg="";
            int count = 0;


            if (!string.IsNullOrEmpty(fundNameDropDownList.Text.Trim().ToString()) && fundNameDropDownList.Text.Trim().ToString() !="0")
            {
                FUND_CODE = fundNameDropDownList.Text.Trim().ToString();

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Fund');", true);
            }

            //if (!string.IsNullOrEmpty(branchDropDownList.Text.Trim().ToString()))
            //{
            //    branchCode = branchDropDownList.Text.Trim().ToString();

            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select a Branch');", true);
            //}
            if (!string.IsNullOrEmpty(CloasingTextBox.Text.ToString()))
            {

                DateTime date1 = DateTime.ParseExact(CloasingTextBox.Text, "dd/MM/yyyy", null);
                Cloasing_date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
             

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select closing date');", true);
            }
            if (!string.IsNullOrEmpty(incomeTaxFYDropDownList.SelectedItem.Text.ToString()))
            {
                FY = incomeTaxFYDropDownList.SelectedItem.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY');", true);
            }
            if (!string.IsNullOrEmpty(fyPartDropDownList.SelectedItem.Text.ToString()))
            {
                FY_PART = fyPartDropDownList.SelectedItem.Text.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY_PART');", true);
            }

            string Action = DropdownlistCert.SelectedValue.ToString();





            if (Action == "1")
            {
                if (!string.IsNullOrEmpty(FYFromTextBox.Text.ToString()))
                {

                    // DateTime dateFYFromTextBox = DateTime.ParseExact(FYFromTextBox.Text, "dd/MM/yyyy", null);
                    FYFrom = Convert.ToDateTime(FYFromTextBox.Text).ToString("dd-MMM-yyyy");
                    Session["FYFrom"] = FYFrom.ToString();

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY From date');", true);
                }

                if (!string.IsNullOrEmpty(FYToTextBox.Text.ToString()))
                {

                    // DateTime dateFYToTextBox = DateTime.ParseExact(FYToTextBox.Text, "dd/MM/yyyy", null);
                    FYTO = Convert.ToDateTime(FYToTextBox.Text).ToString("dd-MMM-yyyy");
                    Session["FYTO"] = FYTO.ToString();

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select FY To date');", true);
                }

                if (!string.IsNullOrEmpty(ddlInvestmentType.SelectedItem.Text.ToString()))
                {
                    Session["Investment_type"] = ddlInvestmentType.SelectedItem.Text.ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "alert('please select Investment Type');", true);
                }

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


                bool status = IsConnectedToInternet();

          

            string fund_code = fundNameDropDownList.SelectedValue.ToString();
            string fund_name = "";

            DataTable dtopenendFund = new DataTable();
            StringBuilder sbMst = new StringBuilder();
            sbMst.Append(" SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL and f_cd=" + fundNameDropDownList.SelectedValue.ToString() + "  order by F_cd  ");
            dtopenendFund = commonGatewayObj.Select(sbMst.ToString());

            if (dtopenendFund != null && dtopenendFund.Rows.Count > 0)
            {

                for (int i = 0; i < dtopenendFund.Rows.Count; i++)
                {
                    fund_name = dtopenendFund.Rows[i]["OPEN_END_FUND_CD"].ToString();
                    Boolean result = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                        if (result == true)
                        {

                            if (Action == "1")
                            {
                                count = Mail_send(fund_name, FY, FY_PART, Cloasing_date, Action, smtpClient);

                                if (count > 0)
                                {
                                    UpDate_DIVI_Para_IncomeTAX(fund_name, FY, Cloasing_date, FY_PART);
                                    Msg = "success";

                                }
                                else
                                {
                                    Msg = "Error";
                                }
                                

                            }
                            else if (Action == "2")
                            {

                                count= Mail_send_Investment_cert(fund_name, FY, FY_PART, Cloasing_date, FYFrom, FYTO, Action, smtpClient);

                                if (count > 0)
                                {
                                    UpDate_DIVI_Para_InvestmentCERT(fund_name, FY, Cloasing_date, FY_PART);
                                    Msg = "success";
                                }
                                else
                                {
                                    Msg = "Error";
                                }

                            }

                        }

                    }
                    
            }
            if (Msg != "Error")
            {
                Msg = count + " Email send successfully";
                lblProcessing.ForeColor = System.Drawing.Color.Green;
                lblProcessing.Text = Msg;
            }
            else
            {
                lblProcessing.ForeColor = System.Drawing.Color.Red;
                lblProcessing.Text = Msg;
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
      public string [] get_file(string fund_code,string Action)
    {
        string[] fileArray= { };
        if (Action == "1")
        {
            string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\INCOMETAX_CERTIFICATE\\" + fund_code + "\\";
            fileArray = Directory.GetFiles(fileLocation);
        }
        else if (Action == "2")
        {
                string fileLocation = ConfigReader._CERT_FILE_LOCATION.ToString() + "\\INVESTMENT_CERTIFICATE\\" + fund_code + "\\";

                fileArray = Directory.GetFiles(fileLocation);
        }

                
        return fileArray;
    }
        public bool  valid_mailCheck(string mail)
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
        public int Mail_send(string fund_name,string FY,string FY_PART,string Cloasing_date,string Action,SmtpClient smtpClient)
        {
            string[] fileArray;
            int count = 0;
            string finalmsg="";
            fileArray = get_file(fund_name, Action);
            foreach (var file in fileArray)
            {
                //add the file from the fileupload as an attachment
                var stream = new FileStream(file, FileMode.Open);
                string filename = Path.GetFileName(file);

                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("automail@icbamcl.com.bd");
                msg.Subject = txtSubject.Text.ToString();
                msg.Body = TextareaMessage.Value.ToString();

                string file_name = Path.GetFileNameWithoutExtension(file);

                if (filename != "Thumbs.db")
                {

                    int length = file_name.Length;

                    string[] multiArray = file_name.Split('_');

                    string reg_number = multiArray[2].ToString();
                    string branchCode = multiArray[3].ToString().Replace('-', '/');

                    if (multiArray.Length >= 4)
                    {

                        DataTable dtEmail = dtDividendInfo(Convert.ToInt32(reg_number), fund_name, branchCode, FY, FY_PART, Cloasing_date);
                       // DataTable dtEmail = dropDownListObj.TEST_MAIL_DROP_DOWNLIST();
                        string TO = "";

                        if (dtEmail != null && dtEmail.Rows.Count > 0)
                        {
                            TO = dtEmail.Rows[0]["EMAIL"].ToString().ToLower().Trim();
                            //TO = "tanvirjob786@gmail.com";
                            bool status = valid_mailCheck(TO);
                            if (status == true)
                            {
                                msg.To.Add(TO);

                                msg.Attachments.Add(new Attachment(stream, Path.GetFileName(file)));

                                try
                                {
                                    smtpClient.Send(msg);
                                    count++;
                                    msg = new MailMessage();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(ex.Message + "reg_number:" + reg_number);
                                    Logger(finalmsg, fund_name, FY, Cloasing_date, TO, reg_number);
                                }

                                string result = Get_DIVIDEND_INFO(fund_name, FY, Cloasing_date, TO, reg_number, branchCode, FY_PART, Action);



                            }
                            else
                            {

                                Logger(finalmsg, fund_name, FY, Cloasing_date, TO, reg_number);

                            }
                        }
                        else
                        {
                            finalmsg = "Error";

                        }
                    }
                }
                else
                {
                    finalmsg = "File Error";

                }

            }

            return count;
        }
        public int Mail_send_Investment_cert(string fund_name, string FY, string FY_PART, string Cloasing_date, string FYFrom, string FYTO,string Action, SmtpClient smtpClient)
        {
            string[] fileArray;
            int count = 0;
            string finalmsg = "";
            fileArray = get_file(fund_name, Action);
            foreach (var file in fileArray)
            {
                //add the file from the fileupload as an attachment
                var stream = new FileStream(file, FileMode.Open);
                string filename = Path.GetFileName(file);

                if (filename != "Thumbs.db")
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress("automail@icbamcl.com.bd");
                    msg.Subject = txtSubject.Text.ToString();
                    msg.Body = TextareaMessage.Value.ToString();

                    string file_name = Path.GetFileNameWithoutExtension(file);

                    string[] multiArray = file_name.Split('_');

                    string reg_number = multiArray[2].ToString();
                    string branchCode = multiArray[3].ToString().Replace('-', '/');

                    if (multiArray.Length >= 4)
                    {

                        //  dtDividendInfo(Convert.ToInt32(reg_number),);
                        DataTable dtEmail = dtInvestCertHolderInfo(reg_number, fund_name, branchCode);
                        string TO = "";
                        if (dtEmail != null && dtEmail.Rows.Count > 0)
                        {
                            TO = dtEmail.Rows[0]["email"].ToString().ToLower().Trim();
                            bool status = valid_mailCheck(TO);
                            if (status == true)
                            {
                                msg.To.Add(TO);

                                msg.Attachments.Add(new Attachment(stream, Path.GetFileName(file)));

                                try
                                {
                                    smtpClient.Send(msg);
                                    count++;
                                    msg = new MailMessage();
                                }
                                catch (Exception ex)
                                {
                                    //Response.Write(ex.Message + "reg_number:" + reg_number);
                                    Logger(ex.Message + "reg_number:" + reg_number, fund_name, FY, Cloasing_date, TO, reg_number);
                                }

                                string result = Get_DIVIDEND_INFO(fund_name, FY, Cloasing_date, TO, reg_number, branchCode, FY_PART, Action);




                            }
                            else
                            {
                                Logger(finalmsg + "reg_number:" + reg_number, fund_name, FY, Cloasing_date, TO, reg_number);

                            }
                        }
                        else
                        {
                            finalmsg = "Error";

                        }
                    }

                }
                else
                {
                    finalmsg = "File Error";

                }

            }

            return count;
        }
        public DataTable get_fund_fromDiviPara()
        {
            DataTable dtFundName = commonGatewayObj.Select("select distinct c.*,d.TAX_CERT_FILE_GENERATE from (select a.*,b.f_name,b.f_type from (SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL) a " +
            " INNER JOIN INVEST.FUND b ON a.F_CD = b.F_CD order by a.f_cd) c INNER JOIN UNIT.DIVI_PARA d ON c.OPEN_END_FUND_CD = d.FUND_CD where TAX_CERT_FILE_GENERATE is not  null order by F_CD");
            DataTable dtFundNameDropDownList = new DataTable();
            dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
            dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
            DataRow dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = "--Click Here to Select--";
            dr["F_CD"] = "0";
            dtFundNameDropDownList.Rows.Add(dr);
            for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
            {
                dr = dtFundNameDropDownList.NewRow();
                dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
                dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
                dtFundNameDropDownList.Rows.Add(dr);
            }
            return dtFundNameDropDownList;
        }
        public DataTable get_fund_fromDiviParaInvestMentCert()
        {
            DataTable dtFundName = commonGatewayObj.Select("select distinct c.*,d.TAX_CERT_FILE_GENERATE from (select a.*,b.f_name,b.f_type from (SELECT  F_CD, OPEN_END_FUND_CD FROM INVEST.FUND_PARA where FUND_PARA.OPEN_END_FUND_CD  IS NOT NULL) a " +
            " INNER JOIN INVEST.FUND b ON a.F_CD = b.F_CD order by a.f_cd) c INNER JOIN UNIT.DIVI_PARA d ON c.OPEN_END_FUND_CD = d.FUND_CD where INV_CERT_FILE_GENERATE is not null order by F_CD");
            DataTable dtFundNameDropDownList = new DataTable();
            dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
            dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
            DataRow dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = "--Click Here to Select--";
            dr["F_CD"] = "0";
            dtFundNameDropDownList.Rows.Add(dr);
            for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
            {
                dr = dtFundNameDropDownList.NewRow();
                dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
                dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
                dtFundNameDropDownList.Rows.Add(dr);
            }
            return dtFundNameDropDownList;
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
            catch(Exception ex) {
               string exp= ex.StackTrace.ToString();
            }
            return result;
        }
        public string Get_DIVIDEND_INFO(string fund_code,string FY, string closedate,string Email, string reg_number, string branchCode,string FY_PART,string Action)
        {
            string msg = "";
            DataTable dtDividend = new DataTable();

            if (Action == "1")
            {
                dtDividend = dtDividendInfo(Convert.ToInt32(reg_number), fund_code, branchCode, FY, FY_PART, closedate);
            }
            else if (Action == "2")
            {
                
                    dtDividend = dtINV_CER_USER_Info(Convert.ToInt32(reg_number), fund_code, branchCode, FY, FY_PART, closedate);
            }

            
            //StringBuilder sbMst = new StringBuilder();
            //sbMst.Append("SELECT * FROM UNIT.DIVIDEND Where fund_cd='"+ fund_code + "' and FY='"+ FY + "' and close_dt='"+ closedate + "' and Email='"+ Email + "' and REG_NO="+ reg_number + " ");
            //dtDividend = commonGatewayObj.Select(sbMst.ToString());
            string strUpdateDividend = "";
            if (dtDividend != null && dtDividend.Rows.Count > 0)
            {

                if (Action == "1")
                {
                    strUpdateDividend = "update UNIT.DIVIDEND set TAX_CERT_MAIL_SEND = 'Y', TAX_CERT_MAIL_SEND_DATE='"+DateTime.Now.ToString("dd-MMM-yyyy") + "' where FUND_CD ='" + fund_code + "' and CLOSE_DT='" + closedate + "' and FY='" + FY + "' and Email='" + Email + "' and REG_NO=" + reg_number + " ";
                }
                else if (Action == "2")
                {
                    strUpdateDividend = "update UNIT.DIVIDEND set INV_CERT_MAIL_SEND = 'Y' , INV_CERT_MAIL_SEND_DATE='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' where FUND_CD ='" + fund_code + "' and CLOSE_DT='" + closedate + "' and FY='" + FY + "' and Email='" + Email + "' and REG_NO=" + reg_number + " ";
                }
              
                int noUpdRowsDividend = commonGatewayObj.ExecuteNonQuery(strUpdateDividend);
                commonGatewayObj.CommitTransaction();  
                msg = "Success";
                return msg;
            }
            else
            {
                msg = "Error";
                return msg;
            }

               
        }
        public static void VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }
        public static void Logger(string lines, string fund_code, string FY, string closedate, string Email, string reg_number)
        {
            string path = "D:\\Data\\Log\\";
            VerifyDir(path);
            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + fund_code + FY + "_Logs.txt";
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }
        }
        public DataTable dtDividendInfo(int regNumber, string fundCode, string branchCode, string fy, string FYPart, string closedate)
        {

            //StringBuilder sbQuery = new StringBuilder();
            //sbQuery.Append("SELECT DIVIDEND.*,DIVI_PARA.FY_PART ,DIVI_PARA.RATE,DIVI_PARA.TAX_LIMIT FROM UNIT.DIVIDEND INNER JOIN   UNIT.DIVI_PARA ON DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND ");
            //sbQuery.Append(" DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "') AND (DIVIDEND.FY = '" + fy.ToString() + "')  ORDER BY DIVIDEND.DIVI_NO");
            //DataTable dtTaxCal = commonGatewayObj.Select(sbQuery.ToString());
            //return dtTaxCal;


            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtIncomeTax = new DataTable();

            sbQueryString.Append("select * from ( SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
            sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(DIVIDEND.EMAIL) AS EMAIL, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
            sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
            sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
            sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
            sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
            sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQueryString.Append(" WHERE(U_MASTER.REG_BR = '" + branchCode + "') AND(U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + fy + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND(U_MASTER.REG_NO = '" + regNumber + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVIDEND.TAX_CERT_MAIL_SEND is  null AND DIVI_PARA.CLOSE_DT='" + closedate + "') ");


           // string q = sbQueryString.ToString();
            dtIncomeTax = commonGatewayObj.Select(sbQueryString.ToString());
             //string q = sbQueryString.ToString();
            return dtIncomeTax;
        }
        public DataTable dtINV_CER_USER_Info(int regNumber, string fundCode, string branchCode, string fy, string FYPart, string closedate)
        {

            //StringBuilder sbQuery = new StringBuilder();
            //sbQuery.Append("SELECT DIVIDEND.*,DIVI_PARA.FY_PART ,DIVI_PARA.RATE,DIVI_PARA.TAX_LIMIT FROM UNIT.DIVIDEND INNER JOIN   UNIT.DIVI_PARA ON DIVIDEND.FY = DIVI_PARA.F_YEAR AND DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.CLOSE_DT = DIVI_PARA.CLOSE_DT AND ");
            //sbQuery.Append(" DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO WHERE (DIVIDEND.REG_BR = '" + branchCode.ToString() + "') AND (DIVIDEND.REG_NO = " + regNumber + ") AND (DIVIDEND.REG_BK = '" + fundCode.ToString() + "') AND (DIVIDEND.FY = '" + fy.ToString() + "')  ORDER BY DIVIDEND.DIVI_NO");
            //DataTable dtTaxCal = commonGatewayObj.Select(sbQuery.ToString());
            //return dtTaxCal;


            StringBuilder sbQueryString = new StringBuilder();
            DataTable dtInvCERT = new DataTable();

            sbQueryString.Append("select * from ( SELECT  U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO, FUND_INFO.FUND_NM, U_MASTER.HNAME, U_MASTER.ADDRS1, TAX_DIDUCT_RT AS TAX_RATE,");
            sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY,LOWER(DIVIDEND.EMAIL) AS EMAIL, U_JHOLDER.JNT_NAME, DIVI_PARA.F_YEAR, TO_CHAR(DIVI_PARA.CLOSE_DT,'DD-MON-YYYY')AS CLOSE_DT, DIVI_PARA.DIVI_NO,");
            sbQueryString.Append(" DECODE(DIVIDEND.CIP,'Y',TO_CHAR(DIVI_PARA.ISS_DT,'DD-MON-YYYY'),'')AS ISS_DT,DIVI_PARA.FY_PART, DIVI_PARA.RATE, TO_CHAR(DIVI_PARA.AGM_DT,'DD-MON-YYYY') AS AGM_DT, ");
            sbQueryString.Append("  DIVI_PARA.CIP_RATE, DIVIDEND.TOT_DIVI, DIVIDEND.DIDUCT,DIVIDEND.TOT_DIVI-DIVIDEND.DIDUCT AS NET_DIVIDEND,DECODE(UPPER(DIVIDEND.CIP), 'Y', DIVIDEND.FI_DIVI_QTY, 0) AS FRACTION_DIVI,  ");
            sbQueryString.Append(" DIVIDEND.FI_DIVI_QTY,DIVIDEND.CIP_QTY, DIVIDEND.BALANCE FROM UNIT.U_MASTER INNER JOIN UNIT.DIVIDEND INNER JOIN UNIT.DIVI_PARA ON DIVIDEND.FUND_CD = DIVI_PARA.FUND_CD AND DIVIDEND.FY = DIVI_PARA.F_YEAR");
            sbQueryString.Append(" AND DIVIDEND.DIVI_NO = DIVI_PARA.DIVI_NO ON U_MASTER.REG_BR = DIVIDEND.REG_BR AND U_MASTER.REG_BK = DIVIDEND.REG_BK AND U_MASTER.REG_NO = DIVIDEND.REG_NO INNER JOIN UNIT.FUND_INFO ON");
            sbQueryString.Append(" U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND U_MASTER.REG_NO = U_JHOLDER.REG_NO");
            sbQueryString.Append(" WHERE(U_MASTER.REG_BR = '" + branchCode + "') AND(U_MASTER.REG_BK = '" + fundCode + "') AND(DIVI_PARA.F_YEAR = '" + fy + "') AND(DIVI_PARA.FY_PART = '" + FYPart + "') AND(U_MASTER.REG_NO = '" + regNumber + "') AND DIVIDEND.EMAIL IS NOT NULL AND DIVIDEND.EMAIL LIKE'%@%' AND REGEXP_LIKE (DIVIDEND.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') AND DIVIDEND.INV_CERT_MAIL_SEND is  null AND DIVI_PARA.CLOSE_DT='" + closedate + "') ");


            // string q = sbQueryString.ToString();
            dtInvCERT = commonGatewayObj.Select(sbQueryString.ToString());
            //string q = sbQueryString.ToString();
            return dtInvCERT;
        }
        public DataTable dtInvestCertHolderInfo(string regNumber, string fundCode, string branchCode )
        {




            StringBuilder sbQueryString = new StringBuilder();

            sbQueryString = new StringBuilder();
            sbQueryString.Append(" select * from (SELECT FUND_INFO.FUND_NM, U_MASTER.REG_BK, U_MASTER.REG_BR, U_MASTER.REG_NO,TRIM(U_MASTER.email) as email , U_MASTER.HNAME, U_MASTER.ADDRS1, ");
            sbQueryString.Append("  U_MASTER.ADDRS2, U_MASTER.CITY, U_JHOLDER.JNT_NAME FROM UNIT.U_MASTER INNER JOIN  UNIT.FUND_INFO ON U_MASTER.REG_BK = FUND_INFO.FUND_CD LEFT OUTER JOIN ");
            sbQueryString.Append("  UNIT.U_JHOLDER ON U_MASTER.REG_BK = U_JHOLDER.REG_BK AND U_MASTER.REG_BR = U_JHOLDER.REG_BR AND   U_MASTER.REG_NO = U_JHOLDER.REG_NO ");
            sbQueryString.Append("  WHERE (U_MASTER.REG_BR = '" + branchCode + "') AND (U_MASTER.REG_BK = '" + fundCode + "') ) a where a.email is not null and a.EMAIL LIKE'%@%'  and a.reg_no="+ regNumber + "  and REGEXP_LIKE (a.EMAIL, '^[A-Za-z]+[A-Za-z0-9.]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}$') ");
            DataTable dtInvestCertHolderInfo = commonGatewayObj.Select(sbQueryString.ToString());
            //string q = sbQueryString.ToString();
            return dtInvestCertHolderInfo;
        }
        public void UpDate_DIVI_Para_IncomeTAX(string FundCode, string FY, string closedate, string FYPart)
        {
            string strUpdateDivi_para = "update UNIT.DIVI_PARA set TAX_CERT_MAIL_SEND = 'Y'   where FUND_CD ='" + FundCode + "' and CLOSE_DT='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "'";
            int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateDivi_para);
            commonGatewayObj.CommitTransaction();
        }
        public void UpDate_DIVI_Para_InvestmentCERT(string FundCode, string FY, string closedate, string FYPart)
        {
            string strUpdateDivi_para = "update UNIT.DIVI_PARA set  INV_CERT_MAIL_SEND='Y' where FUND_CD ='" + FundCode + "' and CLOSE_DT='" + closedate + "' and F_YEAR='" + FY + "' and FY_PART='" + FYPart + "'";
            int noUpdRowsFundTransHB = commonGatewayObj.ExecuteNonQuery(strUpdateDivi_para);
            commonGatewayObj.CommitTransaction();
        }


    }
}