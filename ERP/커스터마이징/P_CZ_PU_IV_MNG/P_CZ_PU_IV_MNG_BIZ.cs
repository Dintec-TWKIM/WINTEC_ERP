using System;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;

namespace cz
{
    internal class P_CZ_PU_IV_MNG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PU_IV_MNGH_S", obj);
        }

        public DataTable SearchDetail(string[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PU_IV_MNGL_S", obj);
        }

        public DataSet Print(string CD_COMPANY, string Multikey)
        {
            DataSet dataSet;

            dataSet = DBHelper.GetDataSet("UP_PU_IV_MNG_SELECT_PRINT", new object[] { CD_COMPANY,
                                                                                      Multikey });
            return dataSet;
        }

        public bool Save(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            spInfo.DataState = DataValueState.Modified;
            dt.RemotingFormat = SerializationFormat.Binary;
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameUpdate = "UP_PU_IVH_UPDATE";
            spInfo.SpParamsUpdate = new string[] { "NO_IV",
                                                   "CD_COMPANY",
                                                   "CD_DOCU",
                                                   "FG_PAYBILL",
                                                   "DT_PAY_PREARRANGED",
                                                   "CD_PARTNER",
                                                   "DT_DUE",
                                                   "YN_JEONJA",
                                                   "DC_RMK",
                                                   "NO_BIZAREA",
                                                   "TXT_USERDEF1" };

            return DBHelper.Save(spInfo);
        }

        public void Delete(object[] obj)
        {
            Global.MainFrame.ExecSp("UP_PU_IVH_DELETE", obj);
        }

        public void 관리구분배부취소(object[] obj)
        {
            Global.MainFrame.ExecuteScalar(@"DELETE FROM PU_IVL_SETR_SUB
                                             WHERE CD_COMPANY = '" + D.GetString(obj[0]) + "'" + 
                                            "AND NO_IV = '" + D.GetString(obj[1]) + "'");
        }

        public bool 미결전표처리(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_MNG_TRANS_DOCU", obj);
        }

        public bool 미결전표취소(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_FI_DOCU_AUTODEL", obj);
        }

        public int GetFI_GWDOCU(string NO_INV)
        {
            int num = 999;
            
            DataTable dataTable = Global.MainFrame.FillDataTable(@"SELECT ST_STAT
                                                                   FROM FI_GWDOCU WITH(NOLOCK) 
                                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                                  "AND CD_PC = '" + Global.MainFrame.LoginInfo.CdPc + "'" +
                                                                  "AND NO_DOCU = '" + NO_INV + "'");
            
            if (dataTable.Rows.Count > 0 && dataTable.Rows.Count > 0)
                num = Convert.ToInt32(dataTable.Rows[0]["ST_STAT"].ToString());
            
            return num;
        }

        public DataTable DataSearch_GW_RPT(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PU_IV_MNG_GW_RPT", obj);
        }

        public bool 전자결재_실제사용(DataRow drHeader, string Html_Code, string App_Form_Kind, string Nm_Pumn)
        {
            return ((ResultData)Global.MainFrame.ExecSp("UP_PU_GWDOCU", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       Global.MainFrame.LoginInfo.CdPc,
                                                                                       drHeader["NO_IV"].ToString(),
                                                                                       drHeader["NO_EMP"].ToString(),
                                                                                       drHeader["DT_PROCESS"].ToString(),
                                                                                       App_Form_Kind,
                                                                                       Html_Code,
                                                                                       Nm_Pumn,
                                                                                       테이블구분.NONE.GetHashCode() })).Result;
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, decimal no_ioline, string value)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (contentType == ContentType.Memo)
                str2 = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                str2 = "CHECK_PEN";
            if (commandType == Dass.FlexGrid.CommandType.Add)
                str1 = @"UPDATE PU_IVL 
                         SET " + str2 + " = '" + value + "'" + 
                        "WHERE NO_IV  = '" + noIo + "'" + 
                        "AND CD_COMPANY = '" + MA.Login.회사코드 + "'" +  
                        "AND NO_LINE = " + no_ioline.ToString();
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                str1 = @"UPDATE PU_IVL 
                         SET " + str2 + " = NULL" + 
                        "WHERE NO_IV  = '" + noIo + "'" + 
                        "AND CD_COMPANY = '" + MA.Login.회사코드 + "'" + 
                        "AND NO_LINE = " + no_ioline.ToString();
            Global.MainFrame.ExecuteScalar(str1);
        }

        public DataTable Gw_DYPNF(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PU_Z_DYPNF_IV_MNG_DOCU_S", obj);
        }

        public DataTable 전표출력(string 전표번호)
		{
            DataTable dt = DBHelper.GetDataTable("UP_FI_DOCU_PRINT_NEW", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        Global.MainFrame.LoginInfo.CdPc,
                                                                                        전표번호,
                                                                                        (!IFRSConfig.IFRSPage ? "KGAAP" : "IFRS"),
                                                                                        Global.SystemLanguage.MultiLanguageLpoint });

            Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTableDocuMngd(dt);

            dt.Columns.Add("NMD_MNGD", typeof(string));

            foreach (DataRow dr in dt.Rows)
                dr["NMD_MNGD"] = this.GetNMD_MNGD(dr);

            return dt;
        }

        private string GetNMD_MNGD(DataRow dRow)
        {
            string str1 = "";
            for (int index = 1; index <= 8; ++index)
            {
                string str2 = D.GetString(dRow["NM_MNGD" + index.ToString()]);
                if ("A06" == D.GetString(dRow["CD_MNG" + index.ToString()]) && D.GetString(dRow["CD_MNGD" + index.ToString()]) != "00")
                    str2 = str2 + "(" + D.GetString(dRow["CD_MNGD" + index.ToString()]) + ")*";
                str1 = str2 != null && !(str2 == "") ? str1 + str2 + "*" : str1 ?? "";
            }
            if (str1 != string.Empty || str1 != "")
                str1 = str1.Substring(0, str1.Length - 1);
            return str1.Replace("()", "");
        }
    }
}
