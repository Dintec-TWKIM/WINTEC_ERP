using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace Dintec
{
    public class EalryWarningSystem
    {
        public EalryWarningSystem()
        {
            
        }

        public void 미수금확인(string 거래처코드, ref WarningLevel 경고레벨, ref string 경고메시지, ref string 제외여부, ref string 지불조건제외여부)
        {
            this.미수금확인(Global.MainFrame.LoginInfo.CompanyCode, 거래처코드, ref 경고레벨, ref 경고메시지, ref 제외여부, ref 지불조건제외여부);
        }

        public void 미수금확인(string 회사코드, string 거래처코드, ref WarningLevel 경고레벨, ref string 경고메시지, ref string 제외여부, ref string 지불조건제외여부)
		{
            DataTable dataTable;
            DataRow dr;
            string 경고메시지1, 경고메시지2, 경고메시지3, 경고메시지4, 경고메시지5, 경고메시지6, 경고메시지7, 경고레벨문구;

            dataTable = this.Search(new object[] { 회사코드,
                                                   거래처코드 });

            경고레벨 = WarningLevel.정상;
            경고메시지 = string.Empty;

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                dr = dataTable.Rows[0];

                제외여부 = dr["YN_EXCEPT"].ToString();
                지불조건제외여부 = dr["YN_PAY"].ToString();

                경고메시지1 = D.GetString(dr["WN_MSG1"]);
                경고메시지2 = D.GetString(dr["WN_MSG2"]);
                경고메시지3 = D.GetString(dr["WN_MSG3"]);
                경고메시지4 = D.GetString(dr["WN_MSG4"]);
                경고메시지5 = D.GetString(dr["WN_MSG5"]);
                경고메시지6 = D.GetString(dr["WN_MSG6"]);
                경고메시지7 = D.GetString(dr["WN_MSG7"]);

                경고레벨 = (WarningLevel)D.GetInt(dr["WN_LEVEL"]);

                switch (경고레벨)
                {
                    case WarningLevel.정상:
                        경고레벨문구 = "정상 (Normal)";
                        break;
                    case WarningLevel.주의요망:
                        경고레벨문구 = "주의요망 (Warning)";
                        break;
                    case WarningLevel.사용불가:
                        경고레벨문구 = "사용불가 (Block)";
                        break;
                    default:
                        return;
                }

                if (!string.IsNullOrEmpty(경고메시지1))
                    경고메시지 += 경고메시지1 + Environment.NewLine;
                if (!string.IsNullOrEmpty(경고메시지2))
                    경고메시지 += 경고메시지2 + Environment.NewLine;
                if (!string.IsNullOrEmpty(경고메시지3))
                    경고메시지 += 경고메시지3 + Environment.NewLine;
                if (!string.IsNullOrEmpty(경고메시지4))
                    경고메시지 += 경고메시지4 + Environment.NewLine;
                if (!string.IsNullOrEmpty(경고메시지5))
                    경고메시지 += 경고메시지5 + Environment.NewLine;
                if (!string.IsNullOrEmpty(경고메시지6))
                    경고메시지 += 경고메시지6 + Environment.NewLine;
                if (!string.IsNullOrEmpty(경고메시지7))
                    경고메시지 += 경고메시지7 + Environment.NewLine;

                if (!string.IsNullOrEmpty(경고메시지))
                    경고메시지 = "※ 조기경보시스템 (Ealry Warning System, EWS)" +
                                 Environment.NewLine +
                                 Environment.NewLine +
                                 경고메시지 +
                                 Environment.NewLine +
                                 "경고레벨 (Warning Level) : " + 경고레벨문구;
            }
        }

        public bool 협조전작성가능여부(string 거래처코드)
        {
            string query;

            query = @"SELECT ISNULL(MP1.YN_USE_GIR, 'N') 
                      FROM MA_PARTNER MP
                      LEFT JOIN CZ_MA_PARTNER MP1 ON MP1.CD_COMPANY = MP.CD_COMPANY AND MP1.CD_PARTNER = MP.CD_PARTNER
                      WHERE MP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                     "AND MP.CD_PARTNER = '" + 거래처코드 + "'";

            return (DBHelper.ExecuteScalar(query).ToString() == "Y" ? true : false);
        }

        private DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_EALRY_WARNING_SYSTEM", obj);
            return dataTable;
        }
    }
}
