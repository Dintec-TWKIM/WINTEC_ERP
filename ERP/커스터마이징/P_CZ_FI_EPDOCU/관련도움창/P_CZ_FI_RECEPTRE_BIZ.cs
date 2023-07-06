using Duzon.BizOn.Erpu.Security;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.UEncryption;
using System.Data;

namespace cz
{
    internal class P_CZ_FI_RECEPTRE_BIZ
    {
        internal DataTable Search(string 접대비코드, bool 취소전표)
        {
            DataTable dataTable1 = DBHelper.GetDataTable(취소전표 ? "UP_FI_RECEPTRE_CNL_SELECT" : "UP_FI_RECEPTRE_SELECT", new object[] { 접대비코드,
                                                                                                                                          MA.Login.회사코드 });
            dataTable1.Columns["CD_DEPT"].DefaultValue = MA.Login.부서코드;
            dataTable1.Columns["NM_DEPT"].DefaultValue = MA.Login.부서명;
            dataTable1.Columns["CD_EMP"].DefaultValue = MA.Login.사원번호;
            dataTable1.Columns["NM_KOR"].DefaultValue = MA.Login.사원명;
            dataTable1.Columns["TP_RECEPT"].DefaultValue = "001";
            dataTable1.Columns["RECEPT_GU"].DefaultValue = "001";
            dataTable1.Columns["USE_AREA"].DefaultValue = "001";
            dataTable1.Columns["RECEPT_GU10"].DefaultValue = "N";
            dataTable1.Columns["TP_RECEPTION"].DefaultValue = "001";
            dataTable1.Columns["TP_COMPANY"].DefaultValue = "1";
            DataTable dataTable2 = new UEncryption().SearchDecryptionTable(dataTable1, new string[] { "NO_RES", "NO_RES1" }, new DataType[] { DataType.Jumin, DataType.Jumin });
            dataTable2.AcceptChanges();

            return dataTable2;
        }

        internal bool 접대비저장(DataTable dt)
        {
            SpInfo spinfo = new SpInfo();
            DataTable dataTable = new UEncryption().SaveEncryptionTable(dt, new string[] { "NO_RES", "NO_RES1" }, new DataType[] { DataType.Jumin, DataType.Jumin });
            spinfo.DataValue = dataTable;
            spinfo.CompanyID = MA.Login.회사코드;
            spinfo.UserID = MA.Login.사용자아이디;
            spinfo.SpNameInsert = "UP_FI_RECEPTRE_INSERT";
            spinfo.SpNameUpdate = "UP_FI_RECEPTRE_UPDATE";
            spinfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_RECEPT",
                                                   "CD_PC",
                                                   "NO_DOCU",
                                                   "NO_DOLINE",
                                                   "CD_ACCT",
                                                   "DT_START",
                                                   "CD_DEPT",
                                                   "CD_EMP",
                                                   "TP_RECEPT",
                                                   "RECEPT_GU",
                                                   "CD_CARD",
                                                   "CD_PARTNER",
                                                   "NO_COMPANY",
                                                   "NM_CEO",
                                                   "NO_RES",
                                                   "CD_PARTNER1",
                                                   "NO_RES1",
                                                   "NM_CEO1",
                                                   "NO_COMPANY1",
                                                   "NM_ADRESS",
                                                   "USE_AREA",
                                                   "USE_COST",
                                                   "AM_MATIRIAL",
                                                   "AM_SERVICE",
                                                   "NM_NOTE",
                                                   "RECEPT_GU10",
                                                   "NM_NOTE1",
                                                   "NM_RECEPT",
                                                   "NM_RECEPT1",
                                                   "TP_RECEPTION",
                                                   "TP_COMPANY" };
            spinfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_RECEPT",
                                                   "CD_PC",
                                                   "NO_DOCU",
                                                   "NO_DOLINE",
                                                   "CD_ACCT",
                                                   "DT_START",
                                                   "CD_DEPT",
                                                   "CD_EMP",
                                                   "TP_RECEPT",
                                                   "RECEPT_GU",
                                                   "CD_CARD",
                                                   "CD_PARTNER",
                                                   "NO_COMPANY",
                                                   "NM_CEO",
                                                   "NO_RES",
                                                   "CD_PARTNER1",
                                                   "NO_RES1",
                                                   "NM_CEO1",
                                                   "NO_COMPANY1",
                                                   "NM_ADRESS",
                                                   "USE_AREA",
                                                   "USE_COST",
                                                   "AM_MATIRIAL",
                                                   "AM_SERVICE",
                                                   "NM_NOTE",
                                                   "RECEPT_GU10",
                                                   "NM_NOTE1",
                                                   "NM_RECEPT",
                                                   "NM_RECEPT1",
                                                   "TP_RECEPTION",
                                                   "TP_COMPANY" };

            return ((ResultData)Global.MainFrame.Save(spinfo)).Result;
        }
    }
}