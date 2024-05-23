using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMIS.Models
{
    public class ServiceResiter
    {
        public int REGISTER_ID { get; set; }
        public int SERVICE_ID { get; set; }
        public int SERVICE_SUB_ID { get; set; }
        public string REG_BK { get; set; }
        public string BO { get; set; }
        public string FOLIO_NO { get; set; }
        public string REG_BR { get; set; }
        public int REG_NO { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE1 { get; set; }
        public string ADDRS1 { get; set; }
        public string REMARKS { get; set; }
        public string Status { get; set; }
        public string ComplainDate { get; set; }
        public string Urgency { get; set; }
        public string UserName { get; set; }



    }
}