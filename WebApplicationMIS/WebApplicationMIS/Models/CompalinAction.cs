using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMIS.Models
{
    public class CompalinAction
    {
        public int COMPLAIN_ACTION_ID { get; set; }
        public int COMPLAIN_ID { get; set; }
        public string STATUS { get; set; }
        public string COMMENTS { get; set; }
        public string ASSIGN_BY { get; set; }
        public string ASSIGN_DATE { get; set; }
        public string CLOSE_DATE { get; set; }
        public string COMPLAIN_SUBJECT { get; set; }
        public string COMPLAIN_DETAILS { get; set; }
        public string EMP_ID { get; set; }
      

    }
}