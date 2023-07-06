using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dintec;
using System;

namespace cz
{
    public class P_CZ_SA_MONTHLYRPT_PIVOT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBMgr.GetDataTable("SP_CZ_SA_MONTHLYRPT_PIVOT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Get목표데이터(string 귀속년월, string 타입)
        {
            DataTable dt;
            string sql, 귀속년, 귀속월;

            귀속년 = 귀속년월.Substring(0, 4);
            귀속월 = 귀속년월.Substring(4, 2);

            sql = @"SELECT CD_KEY,
                           AM_TOTWON," + Environment.NewLine;

            switch(귀속월)
            {
                case "01":
                    sql += "AM_TOT_JAN AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "02":
                    sql += "AM_TOT_FEB AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "03":
                    sql += "AM_TOT_MAR AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "04":
                    sql += "AM_TOT_APR AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "05":
                    sql += "AM_TOT_MAY AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "06":
                    sql += "AM_TOT_JUN AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "07":
                    sql += "AM_TOT_JUL AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "08":
                    sql += "AM_TOT_AUG AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "09":
                    sql += "AM_TOT_SEP AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "10":
                    sql += "AM_TOT_OCT AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "11":
                    sql += "AM_TOT_NOV AS AM_MONTHWON" + Environment.NewLine;
                    break;
                case "12":
                    sql += "AM_TOT_DEC AS AM_MONTHWON" + Environment.NewLine;
                    break;
            }

            sql += @"FROM CZ_SA_PTR_PLAN WITH(NOLOCK)
                     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +  
                    "AND YY_PLAN = '" + 귀속년 + "'" + Environment.NewLine +  
                    "AND TP_PLAN = '" + 타입 + "'";

            dt = DBHelper.GetDataTable(sql);

            return dt;
        }
    }
}
