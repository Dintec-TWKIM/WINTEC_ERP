using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace pur
{
    class P_PU_LOT_SUB_R_BIZ
    {
        #region -> 조회
        internal DataTable Search_Detail(string NO_IO)//, decimal NO_IOLINE)//, string NO_LOT)
        {
            DataTable dt = DBHelper.GetDataTable("UP_LOT_R_SELECT_SUB", new Object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_IO });
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region -> 엑셀

        internal DataTable ExcelSearch(string 멀티품목코드, string 구분자, string CD_PLANT)
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
            dt_엑셀.Columns.Add("NO_IO", typeof(string));
            //dt_엑셀.Columns.Add("NO_IOLINE", typeof(decimal)); //수정 엑셀서식에 포함됨 20081230
            dt_엑셀.Columns.Add("DT_IO", typeof(string));
            dt_엑셀.Columns.Add("FG_IO", typeof(string));
            dt_엑셀.Columns.Add("CD_QTIOTP", typeof(string));
            dt_엑셀.Columns.Add("CD_SL", typeof(string));
            dt_엑셀.Columns.Add("FG_PS", typeof(string));

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
        internal DataTable dt_SER_MGMT(string NO_IO_MGMT)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_LOTSER_MGMT_S", new object[] { MA.Login.회사코드, NO_IO_MGMT, "LOT" });
            return dt;
        }
        #endregion

        #region -> MAXLOT번호조회
        public string GetMaxLot(string CD_ITEM)
        {
            string sqlQuery = " SELECT MAX(NO_LOT) NO_LOT "
                            + "   FROM MM_QTIOLOT "
                            + "  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'"
                            + "    AND CD_ITEM    = '" + CD_ITEM + "'";

            DataTable dt = DBHelper.GetDataTable(sqlQuery);

            if (dt != null && dt.Rows.Count != 0)
                return D.GetString(dt.Rows[0]["NO_LOT"]);
            else
                return "";

        }
        #endregion
    }
}