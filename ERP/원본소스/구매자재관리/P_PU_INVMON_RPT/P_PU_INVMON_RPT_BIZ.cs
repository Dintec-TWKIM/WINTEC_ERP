using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace pur
{
    class P_PU_INVMON_RPT_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        ArrayListExt KEY배열(string 멀티KEY)
        {
            ArrayListExt arrList = new ArrayListExt();
            string[] arrstr = 멀티KEY.Split('|');
            int MaxCnt = 200;
            int cnt = 1;
            string MULTI_KEY = string.Empty;

            for (int i = 0; i < arrstr.Length - 1; i++)
            {
                MULTI_KEY += arrstr[i] + "|";
                if (cnt == MaxCnt)
                {
                    arrList.Add(MULTI_KEY);
                    MULTI_KEY = string.Empty;
                    cnt = 1;
                }
            }

            if (MULTI_KEY != string.Empty)
            {
                arrList.Add(MULTI_KEY);
            }

            return arrList;
        }

        internal DataTable SearchPrint(string 기준년도, string 공장, string 계정구분, string 창고, string 품번FROM, string 품번TO, string STATE, string NO_KEY)
        {
            ArrayListExt arrList = KEY배열(NO_KEY);

            DataTable dt프린트 = new DataTable();
            DataTable dt결과 = new DataTable();
            DataTable dt출력 = null;
            dt프린트.Columns.Add("멀티KEY", typeof(string));

            for (int i = 0; i < arrList.Count; i++)
            {
                DataRow NewRow = dt프린트.NewRow();
                NewRow["멀티KEY"] = arrList[i].ToString();
                dt프린트.Rows.Add(NewRow);
            }

            foreach (DataRow DR_1 in dt프린트.Rows)
            {
                dt결과 = 프린트(기준년도, 공장, 계정구분, 창고, 품번FROM, 품번TO, STATE, DR_1["멀티KEY"].ToString());

                if (dt출력 == null)
                    dt출력 = dt결과.Clone();

                foreach (DataRow DR_2 in dt결과.Rows)
                {
                    dt출력.ImportRow(DR_2);
                }
            }
            return dt출력;
        }

        DataTable 프린트(string 기준년도, string 공장, string 계정구분, string 창고, string 품번FROM, string 품번TO, string STATE, string NO_KEY)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_INVMON_RPT_PRINT";
            si.SpParamsSelect = new object[] { 회사코드, 공장, 품번FROM, 품번TO, 창고, 기준년도, 계정구분, STATE, NO_KEY };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)rtn.DataValue;
        }
    }
}
