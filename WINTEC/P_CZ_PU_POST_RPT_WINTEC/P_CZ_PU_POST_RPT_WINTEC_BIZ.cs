using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Auth;
using System.Collections;
using System.Data;

namespace cz
{
    internal class P_CZ_PU_POST_RPT_WINTEC_BIZ
    {
        private string Lang = Global.SystemLanguage.MultiLanguageLpoint.ToString();

        public DataTable search(object[] args)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpParamsSelect = args,
                SpNameSelect = !(args[12].ToString() == "SumItem") ? "UP_CZ_PU_POST_RPT_H_SELECT" : "UP_CZ_PU_POST_RPT_SUMITEM_SELECT"
            })).DataValue;
            try
            {
                for (int index = 0; index < dataValue.Rows.Count; ++index)
                {
                    DataRow row = dataValue.Rows[index];
                    if (D.GetString(row["YN_AM"]) == "유한")
                        row["YN_AM"] = Global.MainFrame.DD("유한");
                    if (D.GetString(row["YN_AM"]) == "무한")
                        row["YN_AM"] = Global.MainFrame.DD("무한");
                }
            }
            catch
            {
            }
            new AuthUserMenu(dataValue, Global.MainFrame.CurrentPageID).SetQuantity(new string[] { "QT_PO",
                                                                                                   "QT_REV_MM",
                                                                                                   "QT_REQ",
                                                                                                   "QT_GR",
                                                                                                   "QT_REMAN",
                                                                                                   "QT_INV" });
            return dataValue;
        }

        public DataTable SearchDetail(object[] args)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpParamsSelect = args,
                SpNameSelect = "UP_CZ_PU_POST_RPT_L_SELECT"
            })).DataValue;
            AuthUserMenu authUserMenu = new AuthUserMenu(dataValue, Global.MainFrame.CurrentPageID);
            string[] strArray1 = new string[] { "QT_PO",
                                                "QT_REV_MM",
                                                "QT_REQ",
                                                "QT_GR",
                                                "QT_CLS",
                                                "QT_RETURN",
                                                "QT_TR",
                                                "QT_REMAIN" };
            string[] strArray2 = new string[] { "AM_EX",
                                                "AM",
                                                "VAT" };
            authUserMenu.SetQuantity(strArray1);
            authUserMenu.SetMoney(strArray2);
            return dataValue;
        }

        private ArrayListExt KEY배열(string 멀티KEY)
        {
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티KEY.Split('|');
            int num1 = 200;
            int num2 = 1;
            string str = string.Empty;
            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index] + "|";
                if (num2 == num1)
                {
                    arrayListExt.Add(str);
                    str = string.Empty;
                    num2 = 1;
                }
            }
            if (str != string.Empty)
                arrayListExt.Add(str);
            return arrayListExt;
        }

        internal DataTable SearchPrint(
          string DT_FR,
          string DT_TO,
          string 사업장,
          string 공장,
          string 날짜구분,
          string 발주상태,
          string 거래구분,
          string 구매그룹,
          string 담당자,
          string 단위선택,
          string 처리구분,
          string STATE,
          string NO_KEY,
          string CD_CC,
          string REQ_PURGRP,
          string REQ_DEPT,
          string REQ_EMP)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = arrayListExt[index].ToString();
                dataTable1.Rows.Add(row);
            }
            foreach (DataRow row1 in dataTable1.Rows)
            {
                DataTable dataTable4 = this.프린트(DT_FR, DT_TO, 사업장, 공장, 날짜구분, 발주상태, 거래구분, 구매그룹, 담당자, 단위선택, 처리구분, STATE, row1["멀티KEY"].ToString(), CD_CC, REQ_PURGRP, REQ_DEPT, REQ_EMP);
                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();
                foreach (DataRow row2 in dataTable4.Rows)
                    dataTable3.ImportRow(row2);
            }
            return dataTable3;
        }

        private DataTable 프린트(
          string DT_FR,
          string DT_TO,
          string 사업장,
          string 공장,
          string 날짜구분,
          string 발주상태,
          string 거래구분,
          string 구매그룹,
          string 담당자,
          string 단위선택,
          string 처리구분,
          string STATE,
          string NO_KEY,
          string CD_CC,
          string REQ_PURGRP,
          string REQ_DEPT,
          string REQ_EMP)
        {
            DataTable dataTable;
            if (MA.ServerKey(false, new string[1] { "LEMD" }) && STATE == "0")
                dataTable = DBHelper.GetDataTable("UP_PU_POST_RPT_PRINT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                DT_FR,
                                                                                                DT_TO,
                                                                                                사업장,
                                                                                                공장,
                                                                                                날짜구분,
                                                                                                발주상태,
                                                                                                거래구분,
                                                                                                구매그룹,
                                                                                                담당자,
                                                                                                단위선택,
                                                                                                처리구분,
                                                                                                STATE,
                                                                                                NO_KEY,
                                                                                                CD_CC,
                                                                                                REQ_PURGRP,
                                                                                                REQ_DEPT,
                                                                                                REQ_EMP,
                                                                                                this.Lang }, "발주번호, NO_LINE");
            else
                dataTable = DBHelper.GetDataTable("UP_PU_POST_RPT_PRINT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                DT_FR,
                                                                                                DT_TO,
                                                                                                사업장,
                                                                                                공장,
                                                                                                날짜구분,
                                                                                                발주상태,
                                                                                                거래구분,
                                                                                                구매그룹,
                                                                                                담당자,
                                                                                                단위선택,
                                                                                                처리구분,
                                                                                                STATE,
                                                                                                NO_KEY,
                                                                                                CD_CC,
                                                                                                REQ_PURGRP,
                                                                                                REQ_DEPT,
                                                                                                REQ_EMP,
                                                                                                this.Lang });
            return dataTable;
        }
    }
}
