using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMIS.Models
{
    public class ComplainResister
    {
        public int Complain_ID { get; set; }
        public int SERVICE_ID { get; set; }
        public int SERVICE_SUB_ID { get; set; }
        public string Complain_Type { get; set; }
        public string BRANCH_Code { get; set; }
        public string COMPLAIN_SUBJECT { get; set; }
        public string COMPLAIN_DETAILS { get; set; }
        public string REMARKS { get; set; }
        public string Status { get; set; }
        public string ComplainDate { get; set; }
        public string Urgency { get; set; }
        public string UserName { get; set; }
        public string STATUS { get; set;}



    }
}