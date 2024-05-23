using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMCLBL
{
    public class UnitReport
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        public UnitReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable getDtFY()
        {
            DataTable dtFY = commonGatewayObj.Select("SELECT distinct(F_YEAR) as F_YEAR  FROM UNIT.DIVI_PARA  ORDER BY F_YEAR desc ");
            return dtFY;
        }
        public DataTable getDtFYPart()
        {
            DataTable dtFYPart = commonGatewayObj.Select("SELECT DISTINCT FY_PART FROM UNIT.DIVI_PARA ORDER BY FY_PART DESC ");
            return dtFYPart;

        }
    }
}
