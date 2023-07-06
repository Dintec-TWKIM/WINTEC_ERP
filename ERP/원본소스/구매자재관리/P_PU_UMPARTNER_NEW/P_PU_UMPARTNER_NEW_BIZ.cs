using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace pur
{
    class P_PU_UMPARTNER_NEW_BIZ
    {
        #region -> 저장

        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
            si.SpNameInsert = "UP_PU_UMPARTNER_NEW_INSERT";
            si.SpNameUpdate = "UP_PU_UMPARTNER_NEW_UPDATE";
            si.SpNameDelete = "UP_PU_UMPARTNER_NEW_DELETE";
            si.SpParamsInsert = new string[] { "CD_ITEM", "CD_PARTNER", "FG_UM", "CD_EXCH", "NO_LINE", "TP_UMMODULE", "CD_COMPANY", "CD_PLANT", "UM_ITEM", "UM_ITEM_LOW", "SDT_UM", "EDT_UM", "ID_INSERT", "FG_REASON", "DC_RMK","CD_USERDEF1" };
            si.SpParamsUpdate = new string[] { "CD_ITEM", "CD_PARTNER", "FG_UM", "CD_EXCH", "NO_LINE", "TP_UMMODULE", "CD_COMPANY", "CD_PLANT", "UM_ITEM", "UM_ITEM_LOW", "SDT_UM", "EDT_UM", "CD_ITEM_ORIGIN", "FG_UM_ORIGIN", "CD_EXCH_ORIGIN", "ID_UPDATE", "SDT_UM_OLD", "FG_REASON", "DC_RMK","CD_USERDEF1" };
            si.SpParamsDelete = new string[] { "CD_ITEM", "CD_PARTNER", "FG_UM", "CD_EXCH", "NO_LINE", "TP_UMMODULE", "CD_COMPANY", "CD_PLANT", "SDT_UM" };

            ResultData result = (ResultData)Global.MainFrame.Save(si);

            return result.Result;
        }

        #endregion

        #region -> 조회

        public DataTable Search(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_UMPARTNER_NEW_SELECT_H";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_UMPARTNER_NEW_SELECT_L";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            T.SetDefaultValue(dt);
            dt.Columns["S"].DefaultValue = "N";

            return dt;
        }

        #endregion

        #region -> GetMaxLine

        internal DataTable GetMaxLine(string 품목, string 거래처)
        {
            return DBHelper.GetDataTable("UP_SA_PTRPRICE_S_EXCEL2", new object[] { MA.Login.회사코드, 품목, 거래처 });
        }

        #endregion

        #region -> TotalLine

        /* 업로드 하는 엑셀의 품목을 키로 넘겨서 해당 품목의 데이타만 라인Grid에서 가져 옴*/
        public DataTable TotalLine(string KEY, string TP_UMMODULE)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_UMPARTNER_SELECT_TOTAL_L";
            si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, KEY, TP_UMMODULE };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;
        }

        #endregion

        #region -> ExcelSearch

        internal DataTable ExcelSearch(string 멀티품목코드, string 구분자, string 공장)
        {
            ArrayListExt arrList = arr엑셀(멀티품목코드);
            DataTable dt_DB결과 = null;

            for (int k = 0; k < arrList.Count; k++)
            {
                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_SA_PTRPRICE_SELECT_L_EXCEL";
                si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, arrList[k].ToString(), 구분자, 공장 };
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

        #endregion

        #region -> 엑셀

        internal DataTable 엑셀(DataTable dt_엑셀)
        {
            dt_엑셀.Columns.Add("LN_PARTNER", typeof(string));
            dt_엑셀.Columns.Add("TP_UMMODULE", typeof(string));
            dt_엑셀.Columns.Add("NM_USER", typeof(string));
            dt_엑셀.Columns.Add("DT_INSERT", typeof(string));
            dt_엑셀.Columns.Add("NO_LINE", typeof(decimal));           

            return dt_엑셀;
        }

        #endregion

        #region -> arr엑셀

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

        internal DataTable SearchMulti(string 멀티거래처번호, object[] obj)
        {
            DataTable dt결과 = null;
            try
            {
                string[] arr = D.StringConvert.GetPipes(멀티거래처번호, 150);
                string arrLen = D.GetString(arr.Length);
                foreach (string 거래처번호 in arr)
                {
                    obj[1] = 거래처번호;
                    DataTable dt = DBHelper.GetDataTable("UP_PU_UMPARTNER_NEW_MULTI_S_L", obj);

                    if (dt결과 == null)
                        dt결과 = dt;
                    else
                        dt결과.Merge(dt);
                }
            }
            finally
            {

            }
            return dt결과;
        }
    }
}