using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace pur
{
    class P_PU_SERL_SUB_R_BIZ
    {
        #region -> 조회
        internal DataTable Search_Detail(string NO_IO, Decimal NO_IOLINE )//, decimal NO_IOLINE)//, string NO_LOT)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_SERL_SUB_R_SELECT", new object[] { MA.Login.회사코드, NO_IO, NO_IOLINE });
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region -> 엑셀

        internal DataTable ExcelSearch(string 멀티품목코드, string CD_PLANT ,string 구분자)
        {
            ArrayListExt arrList = arr엑셀(멀티품목코드);
            DataTable dt_DB결과 = null;

            for (int k = 0; k < arrList.Count; k++)
            {
                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_SA_PTRPRICE_SELECT_L_EXCEL";
                si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, arrList[k].ToString(), 구분자, CD_PLANT };
                ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
                DataTable dt = (DataTable)rtn.DataValue;

                if (dt_DB결과 == null)
                {
                    dt_DB결과 = dt;
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                        dt_DB결과.ImportRow(row);
                }
            }
            return dt_DB결과;
        }


        internal DataTable 엑셀(DataTable dt_엑셀)
        {
            //dt_엑셀.Columns.Add("NO_IO", typeof(string));
            ////dt_엑셀.Columns.Add("NO_IOLINE", typeof(decimal)); //수정 엑셀서식에 포함됨 20081230
            //dt_엑셀.Columns.Add("DT_IO", typeof(string));
            //dt_엑셀.Columns.Add("FG_IO", typeof(string));
            //dt_엑셀.Columns.Add("CD_QTIOTP", typeof(string));
            //dt_엑셀.Columns.Add("CD_SL", typeof(string));
            //dt_엑셀.Columns.Add("FG_PS", typeof(string));

            //dt_엑셀.Columns.Add("NO_IO", typeof(string));
            //dt_엑셀.Columns.Add("DT_IO", typeof(string));
            dt_엑셀.Columns.Add("FG_IO", typeof(string));
            dt_엑셀.Columns.Add("CD_QTIOTP", typeof(string));
            //dt_엑셀.Columns.Add("CD_SL", typeof(string));
            //dt_엑셀.Columns.Add("FG_PS", typeof(string));

            return dt_엑셀;
        }

        public DataTable TotalLine(string KEY, string TP_UMMODULE)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_UMPARTNER_SELECT_TOTAL_L";
            si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, KEY, TP_UMMODULE };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;
        }

        private ArrayListExt arr엑셀(string 멀티품목코드)
        {
            int MaxCnt = 50;
            int Cnt = 1;
            string 품목코드 = string.Empty;

            ArrayListExt arrList = new ArrayListExt();
            string[] arrstr = 멀티품목코드.Split('|');

            for (int i = 0; i < arrstr.Length - 1; i++)
            {
                품목코드 += arrstr[i].ToString() + "|";
                if (Cnt == MaxCnt)
                {
                    arrList.Add(품목코드);
                    품목코드 = string.Empty;
                    Cnt = 0;
                }
                Cnt++;
            }

            if (품목코드 != string.Empty)
                arrList.Add(품목코드);
            return arrList;
        }


        #endregion

        #region -> 원천LOT조회
        internal DataTable dt_LOT_MGMT(string NO_IO_MGMT)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_LOTSER_MGMT_S", new object[] { MA.Login.회사코드, NO_IO_MGMT, "SER" });
            return dt;
        }
        #endregion

        internal bool Save(DataTable dtSERL, string 공장코드)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dtSERL != null)
            {
                SpInfo si04 = new SpInfo();
                si04.DataValue = dtSERL;

                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                si04.SpNameDelete = "UP_MM_QTIODS_DELETE";
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si04.UserID = Global.MainFrame.LoginInfo.UserID;
                si04.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",    "NO_IOLINE", "CD_ITEM",  "CD_QTIOTP", "FG_IO",
		                                             "CD_MNG1",	   "CD_MNG2",	"CD_MNG3",	"CD_MNG4",	 "CD_MNG5",	 "CD_MNG6",	  "CD_MNG7",	
                                                     "CD_MNG8",	   "CD_MNG9",	"CD_MNG10", "CD_MNG11",	 "CD_MNG12", "CD_MNG13",  "CD_MNG14",	
                                                     "CD_MNG15",   "CD_MNG16",	"CD_MNG17",	"CD_MNG18",	 "CD_MNG19", "CD_MNG20",  "CD_PLANT", 
                                                     "ID_INSERT"
                };
                si04.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",    "NO_IOLINE", "CD_ITEM",  "CD_QTIOTP", "FG_IO",
		                                             "CD_MNG1",	   "CD_MNG2",	"CD_MNG3",	"CD_MNG4",	 "CD_MNG5",	 "CD_MNG6",	  "CD_MNG7",	
                                                     "CD_MNG8",	   "CD_MNG9",	"CD_MNG10", "CD_MNG11",	 "CD_MNG12", "CD_MNG13",  "CD_MNG14",	
                                                     "CD_MNG15",   "CD_MNG16",	"CD_MNG17",	"CD_MNG18",	 "CD_MNG19", "CD_MNG20"
                };
                si04.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_SERIAL" };

                si04.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", 공장코드);
                si04.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", Global.MainFrame.LoginInfo.UserID);

                sic.Add(si04);
            }

            return DBHelper.Save(sic);
        }
    }
}