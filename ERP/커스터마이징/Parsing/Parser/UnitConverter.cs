using System;
using System.Data;

namespace Dintec.Parser
{
    public class UnitConverter
    {
        DataTable dt;

        public UnitConverter()
        {
            dt = GetDb.Code("MA_B000004");
        }

        public string Convert(object unit)
        {
             string newUnit = "";

            if (string.IsNullOrEmpty(unit.ToString()))
                newUnit = "PCS";
            else if (unit.ToString().Equals("TN"))
                newUnit = "TIN";
            else
            {

                DataRow[] row = dt.Select("CD_FLAG3 LIKE '%" + unit + "%'");

                if (row.Length > 0)
                {
                    newUnit = row[0]["CODE"].ToString();
                }

                if (unit.Equals("PCS"))
                {
                    newUnit = "PCS";
                }

                //없는 항목은 그대로 출력(임시)
                if (string.IsNullOrEmpty(newUnit))
                {
                    newUnit = unit.ToString();
                }
            }

            return newUnit;
        }
    }
}
