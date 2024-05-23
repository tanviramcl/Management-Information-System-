using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMIS.Models
{
    public class SupportedDocument
    {
        public int RESISTER_ID { get; set; }
        public int SUP_DOC_ID { get; set; }
        public int SUP_DOC_NAME { get; set; }
        public string BO_COPY { get; set; }
        public string NID_COPY { get; set; }
        public string E_TIN_COPY { get; set; }
        public string APPLICANT_PIC { get; set; }
        public string NOMINEE_PIC { get; set; }
        public string CHECK_BOOK { get; set; }
        public string UTILITY_BILL { get; set; }
        public string ORIGINAL_CERT { get; set; }
        public string SIG_SCREEN_PRINT { get; set; }
        public string ALLOTMENT_LETTER { get; set; }
        public string AFFIDAVIT { get; set; }
        public string GD { get; set; }
        public string INDEMNITY_BOND { get; set; }
        public string PAPER_ADD { get; set; }
        public string DIVIDEND_NOTICE { get; set; }
        public string DEATH_CERTIFICATES { get; set; }
        public string SUGG_HIGHCOURT { get; set; }
        public string POWER_ATTORNEY { get; set; }
        public string CERT_CEMENTRY { get; set; }
    }
}