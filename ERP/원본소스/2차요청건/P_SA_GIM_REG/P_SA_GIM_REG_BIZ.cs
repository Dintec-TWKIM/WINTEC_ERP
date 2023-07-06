using System;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Auth;

namespace sale
{
    class P_SA_GIM_REG_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(object[] obj)
        {
            //UP_SA_GIM_REG_SELECT
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIM_REG_H_S", obj, "NO_IO ASC");
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            //UP_SA_GIM_REG_SELECT1
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIM_REG_L_S", obj, "NO_IO ASC, NO_IOLINE ASC");
            T.SetDefaultValue(dt);

            AuthUserMenu AuthMenu = new AuthUserMenu(dt, Global.MainFrame.CurrentPageID);

            string[] AM = new string[] { "AM_EX", "AM", "VAT" }; //금액컬럼 설정
            string[] QT = new string[] { "QT_IO", "QT_UNIT_MM", "QT_CLS" };   //수량컬럼 설정
            string[] UM = new string[] { "UM_EX", "UM" };   //단가컬럼 설정

            AuthMenu.SetAuthMenu(QT, UM, AM);

            return dt;
        }

        public DataTable Search_Print(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIM_REG_SELECT1_PRINT", obj, "NO_IO ASC, NO_IOLINE ASC");
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search_Print_SERIAL(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIM_REG_SERIAL_P", obj, "NO_IO ASC, NO_IOLINE ASC");
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search_Print_Barcode(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_PO_SERIAL_PRT", obj, "NO_IO ASC, NO_IOLINE ASC, NO_SERIAL ASC");
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable CloseMessage(string 수불번호)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIM_REG_SELECT_MSG", new Object[] { 회사코드, 수불번호 });
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dt_H, DataTable dt_L)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt_H != null)
            {
                SpInfo siM = new SpInfo();

                siM.DataValue = dt_H;
                siM.CompanyID = 회사코드;
                siM.UserID = Global.MainFrame.LoginInfo.UserID;
                siM.SpNameUpdate = "UP_SA_GIM_REG_UPDATE";
                siM.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_IO", "DC_RMK", "FILE_PATH_MNG" };
                sic.Add(siM);
            }

            if (dt_L != null)
            {
                SpInfo siD = new SpInfo();

                siD.DataValue = dt_L;
                siD.CompanyID = 회사코드;
                siD.UserID = Global.MainFrame.LoginInfo.UserID;
                siD.SpNameDelete = "UP_SA_GIM_REG_DELETE";
                siD.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_PSO_MGMT", "NO_PSOLINE_MGMT", "NO_IO_MGMT" };

                sic.Add(siD);
            }

            return DBHelper.Save(sic);
        }

        /*
        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = 회사코드;
            si.SpNameDelete = "UP_SA_GIM_REG_DELETE";
            si.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_PSO_MGMT", "NO_PSOLINE_MGMT", "NO_IO_MGMT" };
            ResultData result = (ResultData)Global.MainFrame.Save(si);
            return result.Result;
        }
         */

        public bool Delete(string 수불번호)
        {
            if (Global.MainFrame.ServerKeyCommon == "KOWA")
            {
                ResultData rtn = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_KOWA_IO_DELETE_HL", new object[] { 회사코드, 수불번호 });
                return rtn.Result;
            }
            else
            {
                ResultData rtn = (ResultData)Global.MainFrame.ExecSp("UP_SA_IO_DELETE_HL", new object[] { 회사코드, 수불번호 });
                return rtn.Result;
            }
        }

        public bool Save_Serial(DataTable dt_SERIAL)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt_SERIAL != null)
            {
                SpInfo si04 = new SpInfo();
                si04.DataValue = dt_SERIAL;
                //si04.DataState = DataValueState.Added;
                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                si04.SpNameDelete = "UP_MM_QTIODS_DELETE";
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si04.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",     "NO_IOLINE",    "CD_ITEM", 
		                                             "CD_QTIOTP",  "FG_IO",     "CD_MNG1",	 "CD_MNG2",	     "CD_MNG3",	
                                                     "CD_MNG4",	   "CD_MNG5",	"CD_MNG6",	 "CD_MNG7",	     "CD_MNG8",	
                                                     "CD_MNG9",	   "CD_MNG10",  "CD_MNG11",	 "CD_MNG12",	 "CD_MNG13",	
                                                     "CD_MNG14",   "CD_MNG15",	"CD_MNG16",	 "CD_MNG17",	 "CD_MNG18",	
                                                     "CD_MNG19",   "CD_MNG20" };
                si04.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",     "NO_IOLINE",    "CD_ITEM", 
		                                             "CD_QTIOTP",  "FG_IO",     "CD_MNG1",	 "CD_MNG2",	     "CD_MNG3",	
                                                     "CD_MNG4",	   "CD_MNG5",	"CD_MNG6",	 "CD_MNG7",	     "CD_MNG8",	
                                                     "CD_MNG9",	   "CD_MNG10",  "CD_MNG11",	 "CD_MNG12",	 "CD_MNG13",	
                                                     "CD_MNG14",   "CD_MNG15",	"CD_MNG16",	 "CD_MNG17",	 "CD_MNG18",	
                                                     "CD_MNG19",   "CD_MNG20" };
                si04.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_SERIAL" };

                sic.Add(si04);
            }

            //저장 내역이 하나도 없을때 Exception 처리를 한다.
            if (sic.List == null) return false;

            return DBHelper.Save(sic);
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, string value)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                sqlQuery = "UPDATE MM_QTIOH SET " + columnName + " = '" + value + "' WHERE NO_IO = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'";
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                sqlQuery = "UPDATE MM_QTIOH SET " + columnName + " = NULL WHERE NO_IO = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'";

            Global.MainFrame.ExecuteScalar(sqlQuery);
        }
    }
}
