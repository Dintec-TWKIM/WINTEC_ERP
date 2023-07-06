using System.Data;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_MA_PARTNER_REQ_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_PARTNER_REQ_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_MA_PARTNER_REQ_XML";
            si.SpParamsInsert = new string[] { "CD_COMPANY", "XML", "ID_INSERT" };

            return DBHelper.Save(si);
        }

        internal string 승인(string 회사코드, string 신청번호)
        {
            object[] outs;

            DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_REQ_CONFIRM", new object[] { 회사코드,
                                                                                    신청번호,
                                                                                    "C",
                                                                                    Global.MainFrame.LoginInfo.UserID }, out outs);



            return outs[0].ToString();
        }

        internal void 반려(string 회사코드, string 신청번호)
        {
            DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_REQ_CONFIRM", new object[] { 회사코드,
                                                                                    신청번호,
                                                                                    "R",
                                                                                    Global.MainFrame.LoginInfo.UserID });
        }

        internal bool 취소(string 회사코드, string 신청번호, string 거래처코드)
        {
            DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_REQ_ROLLBACK", new object[] { 회사코드, 신청번호, 거래처코드 });

            return true;
        }

        internal bool 동기화(string 거래처코드, string 원본회사, string 대상회사, string 등록자)
        {
            DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_COPY_I", new object[] { 거래처코드, 원본회사, 대상회사, 등록자 });

            return true;
        }

        internal void 사업자등록번호체크(string 사업자번호, out string 거래처코드, out string 거래처명)
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_PARTNER, LN_PARTNER 
                                                          FROM MA_PARTNER WITH(NOLOCK)  
                                                          WHERE NO_COMPANY = '" + 사업자번호 + "'" +
                                                        @"AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
            if (dataTable.Rows.Count == 0)
            {
                거래처코드 = 거래처명 = string.Empty;
            }
            else
            {
                거래처코드 = D.GetString(dataTable.Rows[0]["CD_PARTNER"]);
                거래처명 = D.GetString(dataTable.Rows[0]["LN_PARTNER"]);
            }
        }

        internal void 주민번호체크(string 주민번호, ref string 등록유무)
        {
            if (주민번호 == "" || 주민번호 == null)
            {
                등록유무 = "N";
            }
            else
            {
                object[] outParameters = null;
                DBHelper.GetDataTable("SP_CZ_MA_PARTNER_S_N", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             주민번호 }, out outParameters);
                등록유무 = D.GetString(outParameters[0]);
            }
        }
    }
}