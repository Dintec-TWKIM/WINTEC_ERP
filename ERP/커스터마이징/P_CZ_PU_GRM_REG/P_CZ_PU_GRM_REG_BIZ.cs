using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PU_GRM_REG_BIZ
    {
        private string Lang = Global.SystemLanguage.MultiLanguageLpoint.ToString();

        public DataTable Search_Main(
          string CD_COMPANY,
          string DT_FROM,
          string DT_TO,
          string CD_PLANT,
          string CD_SL,
          string CD_PARTNER,
          string FG_TRANS,
          string FG_RETURN,
          string CD_QTIOTP,
          string NO_EMP,
          string IS_CLOSED,
          string NO_IO,
          string CD_PJT,
          string 전용발주번호,
          string CD_TPPO,
          string txt_NO_IO)
        {
            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
            

            SpNameSelect = "SP_CZ_PU_GRM_REGS_H",
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                SpParamsSelect = new string[] { CD_COMPANY,
                                                DT_FROM,
                                                DT_TO,
                                                CD_PLANT,
                                                CD_SL,
                                                CD_PARTNER,
                                                FG_TRANS,
                                                FG_RETURN,
                                                CD_QTIOTP,
                                                NO_EMP,
                                                IS_CLOSED,
                                                NO_IO,
                                                CD_PJT,
                                                전용발주번호,
                                                this.Lang,
                                                CD_TPPO,
                                                txt_NO_IO } })).DataValue;
        }
        public DataTable Search_Detail(
          string CD_COMPANY,
          string NO_IO,
          string IS_CLOSED,
          string CD_PJT,
          string 전용발주번호,
          string CD_TPPO)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PU_GRM_SELECT_L",
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                SpParamsSelect = new string[] { CD_COMPANY,
                                                NO_IO,
                                                IS_CLOSED,
                                                CD_PJT,
                                                전용발주번호,
                                                this.Lang,
                                                CD_TPPO } })).DataValue;
            if (Global.MainFrame.ServerKeyCommon == "SBPN")
                dataValue.Columns.Add("AM_TRAN", typeof(decimal), "NUM_USERDEF1 * NUM_USERDEF2");
            return dataValue;
        }

        public DataTable Search_Print(object[] obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = !Global.MainFrame.ServerKeyCommon.Contains("DAIKIN") ? (!(Global.MainFrame.ServerKeyCommon == "AJNSF") ? (!(Global.MainFrame.ServerKeyCommon == "GITEC") ? "UP_PU_GRM_SELECT_PRINT" : "UP_PU_Z_GITEC_GRM_P1") : "UP_PU_GRM_REG_LOT_PRINT") : "UP_PU_Z_DAIKIN_GRM_PRINT_S",
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpParamsSelect = obj
        })).DataValue;

        public DataTable Search_SERIAL() => DBHelper.GetDataTable("UP_PU_MNG_SER_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

        public bool Delete(DataTable dtH, DataTable dtD)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dtH != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    DataState = DataValueState.Deleted,
                    SpNameDelete = "UP_PU_GRM_DELETE",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpParamsDelete = new string[] { "NO_IO",
                                                    "CD_COMPANY",
                                                    "ID_UPDATE" }
                });
            if (dtD.Rows.Count > 0)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtD,
                    DataState = DataValueState.Deleted,
                    SpNameDelete = "UP_PU_MM_QTIO_PAGE_LINE_DELETE",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpParamsDelete = new string[] { "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_COMPANY" }
                });
            return DBHelper.Save(spInfoCollection);
        }

        public bool Save(DataTable dtH, DataTable dtSERL, string CD_PLANT, DataTable dtL)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dtH != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    SpNameUpdate = "UP_PU_GRM_REG_U",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "DC_RMK",
                                                    "ID_UPDATE",
                                                    "FILE_PATH_MNG" }
                });
            if (dtL != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    SpNameUpdate = "UP_PU_GRM_REG_U2",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "DC_RMK_IO",
                                                    "ID_UPDATE",
                                                    "CD_USERDEF1" }
                });
            if (dtSERL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtSERL;
                spInfo.SpNameInsert = "UP_MM_QTIODS_INSERT";
                spInfo.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                spInfo.SpNameDelete = "UP_MM_QTIODS_DELETE";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20",
                                                       nameof (CD_PLANT),
                                                       "ID_INSERT" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_SERIAL" };
                spInfo.SpParamsValues.Add(ActionState.Insert, nameof(CD_PLANT), CD_PLANT);
                spInfoCollection.Add(spInfo);
            }
            return spInfoCollection.List != null && DBHelper.Save(spInfoCollection);
        }

        public DataTable GetNoIo(string id_memo)
        {
            string str = "SELECT A.NO_IO, A.CD_PLANT, A.QT_CLS   FROM   MM_QTIO A  WHERE A.ID_MEMO = '" + id_memo + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable Get_FileName(string CD_COMPANY, string NO_IO, string ID_MENU)
        {
            string str;
            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                str = "SELECT (MAX(MF.FILE_NAME) + ' ( 총 ' + CONVERT(NVARCHAR(5),COUNT(1)) +' 건)') AS FILE_PATH_MNG  \r\n                                                            FROM MA_FILEINFO MF \r\n                                                            WHERE MF.CD_COMPANY = '" + CD_COMPANY + "' AND MF.ID_MENU = '" + ID_MENU + "' AND MF.CD_MODULE = 'PU'AND MF.CD_FILE = '" + NO_IO + "'";
            else
                str = "SELECT (MAX(MF.FILE_NAME) || ' ( 총 ' || TO_CHAR(COUNT(1)) ||' 건)') AS FILE_PATH_MNG  \r\n                                                            FROM MA_FILEINFO MF \r\n                                                            WHERE MF.CD_COMPANY = '" + CD_COMPANY + "' AND MF.ID_MENU = '" + ID_MENU + "' AND MF.CD_MODULE = 'PU'AND MF.CD_FILE = '" + NO_IO + "'";
            return Global.MainFrame.FillDataTable(str);
        }

        public DataTable GetNoRCV(string NO_IO)
        {
            string str = "SELECT  MAX(A.NO_ISURCV) AS NO_RCV   FROM   MM_QTIO A  WHERE A.NO_IO = '" + NO_IO + "'   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            return Global.MainFrame.FillDataTable(str);
        }
    }
}
