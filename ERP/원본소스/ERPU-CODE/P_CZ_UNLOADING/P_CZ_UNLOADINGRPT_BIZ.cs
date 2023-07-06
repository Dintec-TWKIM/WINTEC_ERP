using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_UNLOADINGRPT_BIZ
    {
        //public bool Save(DataTable dt)
        //{
        //    SpInfo si = new SpInfo();
        //    si.DataValue = dt;
        //    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

        //    si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;


        //    si.SpNameInsert = "UP_CZ_UNLOAGINGRPT_I";
        //    si.SpNameUpdate = "UP_CZ_UNLOAGINGRPT_U";
        //    si.SpNameDelete = "UP_CZ_UNLOAGINGRPT_D";
        //    si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_BL", "UNLOADING_HARBOR", "WT_LOADING", "WT_UNLOADING", "WT_REAL", "LOSS_LOADING", "LOSS_UNLOADING", "LOSS_REAL", "RT_LOADING","RT_UNLOADING","RT_REAL","VALUE_EL1","VALUE_EL2","VALUE_EL3","VALUE_EL4","VALUE_EL5","VALUE_EL6" };
        //    si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_BL", "UNLOADING_HARBOR", "WT_LOADING", "WT_UNLOADING", "WT_REAL", "LOSS_LOADING", "LOSS_UNLOADING", "LOSS_REAL", "RT_LOADING", "RT_UNLOADING", "RT_REAL", "VALUE_EL1", "VALUE_EL2", "VALUE_EL3", "VALUE_EL4", "VALUE_EL5", "VALUE_EL6" };
        //    si.SpParamsDelete = new string[] { "CD_COMPANY", "NO_BL" };

        //    ResultData result = (ResultData)Global.MainFrame.Save(si);

        //    return result.Result;


        //}
        internal bool Save(DataTable dtH, DataTable dtL)
        {
            if ((dtH != null) || (dtL != null))
            {
                //DataTable changes = null;
                //DataTable table2 = null;
                //DataTable table3 = null;
                //if (dtH != null)
                //{
                //    changes = dtH.GetChanges(DataRowState.Added);
                //    table2 = dtH.GetChanges(DataRowState.Modified);
                //    table3 = dtH.GetChanges(DataRowState.Deleted);
                //}


                SpInfoCollection infos = new SpInfoCollection();
                SpInfo info = null;


                //하단 하역량 입력
                if (dtH != null)
                {
                    info = new SpInfo();

                    info.DataValue = dtH;
                    info.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    info.UserID = Global.MainFrame.LoginInfo.UserID;

                    info.SpNameInsert = "UP_CZ_UNLOAGINGRPT_I";
                    info.SpNameUpdate = "UP_CZ_UNLOAGINGRPT_U";
                    info.SpNameDelete = "UP_CZ_UNLOAGINGRPT_D";
                    info.SpParamsInsert = new string[] { "CD_COMPANY", "NO_BL", "DT_UNLOADING", "UNLOADING_HARBOR", "WT_LOADING", "WT_UNLOADING", "WT_REAL", "LOSS_LOADING", "LOSS_UNLOADING", "LOSS_REAL", "RT_LOADING", "RT_UNLOADING", "RT_REAL", "VALUE_EL1", "VALUE_EL2", "VALUE_EL3", "VALUE_EL4", "VALUE_EL5", "VALUE_EL6", "CD_SL", "CD_ITEM" };
                    info.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_BL", "DT_UNLOADING", "UNLOADING_HARBOR", "WT_LOADING", "WT_UNLOADING", "WT_REAL", "LOSS_LOADING", "LOSS_UNLOADING", "LOSS_REAL", "RT_LOADING", "RT_UNLOADING", "RT_REAL", "VALUE_EL1", "VALUE_EL2", "VALUE_EL3", "VALUE_EL4", "VALUE_EL5", "VALUE_EL6", "CD_SL", "CD_ITEM" };
                    info.SpParamsDelete = new string[] { "CD_COMPANY", "NO_BL" };

                    infos.Add(info);
                }


                //중단 - VESSEL 기준 정보 입력
                if (dtL != null)
                {
                    info = new SpInfo();

                    info.DataValue = dtL;
                    info.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    info.UserID = Global.MainFrame.LoginInfo.UserID;

                    info.SpNameInsert = "UP_CZ_ULOADINGRPT_D_I";
                    info.SpNameUpdate = "UP_CZ_ULOADINGRPT_D_U";
                    info.SpNameDelete = "UP_CZ_ULOADINGRPT_D_D";

                    info.SpParamsInsert = new string[] { 
                        "CD_COMPANY","NM_VESSEL","NO_BL","RT_CONTRACT","TM_LOADING","TM_UNLOADING","AM_CONTRACT","UM_CONTRACT","AM_DEM","UM_DEM","YN_DEM"
                    };


                    info.SpParamsUpdate = (new string[] { "CD_COMPANY", "NM_VESSEL", "NO_BL", "RT_CONTRACT", "TM_LOADING", "TM_UNLOADING", "AM_CONTRACT", "UM_CONTRACT", "AM_DEM", "UM_DEM", "YN_DEM" });

                    info.SpParamsDelete = new string[] { "CD_COMPANY", "NM_VESSEL" };

                    infos.Add(info);
                }

                DBHelper.Save(infos);
                
            }
            return true;
        }


        public DataTable Search(object[] obj)
        {

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_CZ_UNLOADINGRPT_SELECT";
            si.SpParamsSelect = obj; // new Object[] { Global.MainFrame.LoginInfo.CompanyCode, obj };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;

        }

        // _FlexL 하역정보 조회
        public DataTable SearchDetail(object[] obj)
        {
            SpInfo info = new SpInfo();
            info.SpNameSelect = "UP_CZ_UNLOAGINGRPT_S";
            info.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(info);
            DataTable table = (DataTable)result.DataValue;
            T.SetDefaultValue(table);
            //table.Columns["S"].DefaultValue = "N";
            return table;


        }

        // _FlexL2 Vessel 기준 조회

        public DataTable SearchDetail2(object[] obj)
        {
            SpInfo info = new SpInfo();
            info.SpNameSelect = "UP_CZ_ULOADINGRPT_D_S";
            info.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(info);
            DataTable table = (DataTable)result.DataValue;
            T.SetDefaultValue(table);
            //table.Columns["S"].DefaultValue = "N";
            return table;


        }


        //internal DataTable GetMaxLine(string 품목, string 거래처)
        //{
        //    return DBHelper.GetDataTable("UP_SA_PTRPRICE_S_EXCEL1", new object[] { MA.Login.회사코드, 품목, 거래처 });
        //}

        /* 업로드 하는 엑셀의 품목을 키로 넘겨서 해당 품목의 데이타만 라인Grid에서 가져 옴*/
        //public DataTable TotalLine(string KEY, string TP_UMMODULE)
        //{
        //    SpInfo si = new SpInfo();
        //    si.SpNameSelect = "UP_SA_PTRPRICE_SELECT_TOTAL_L";
        //    si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, KEY, TP_UMMODULE };
        //    ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
        //    return (DataTable)result.DataValue;
        //}

        //internal DataTable ExcelSearch(string 멀티품목코드, string 구분자, string 공장)
        //{
        //    ArrayListExt arrList = arr엑셀(멀티품목코드);
        //    DataTable dt_DB결과 = null;

        //    for (int k = 0; k < arrList.Count; k++)
        //    {
        //        SpInfo si = new SpInfo();
        //        si.SpNameSelect = "UP_SA_PTRPRICE_SELECT_L_EXCEL";
        //        si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, arrList[k].ToString(), 구분자, 공장 };
        //        ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
        //        DataTable dt = (DataTable)rtn.DataValue;

        //        if (dt_DB결과 == null)
        //        {
        //            dt_DB결과 = dt;
        //        }
        //        else
        //        {
        //            foreach (DataRow row in dt.Rows)
        //                dt_DB결과.ImportRow(row);
        //        }
        //    }
        //    return dt_DB결과;
        //}

        //internal DataTable 엑셀(DataTable dt_엑셀)
        //{
        //    dt_엑셀.Columns.Add("LN_PARTNER", typeof(string));
        //    dt_엑셀.Columns.Add("TP_UMMODULE", typeof(string));
        //    dt_엑셀.Columns.Add("NM_USER", typeof(string));
        //    dt_엑셀.Columns.Add("DT_INSERT", typeof(string));
        //    dt_엑셀.Columns.Add("NO_LINE", typeof(decimal));

        //    return dt_엑셀;
        //}

        //private ArrayListExt arr엑셀(string 멀티품목코드)
        //{
        //    int MaxCnt = 50;
        //    int Cnt = 1;
        //    string 품목코드 = string.Empty;

        //    ArrayListExt arrList = new ArrayListExt();
        //    string[] arrstr = 멀티품목코드.Split('|');

        //    for (int i = 0; i < arrstr.Length - 1; i++)
        //    {
        //        품목코드 += arrstr[i].ToString() + "|";
        //        if (Cnt == MaxCnt)
        //        {
        //            arrList.Add(품목코드);
        //            품목코드 = string.Empty;
        //            Cnt = 0;
        //        }
        //        Cnt++;
        //    }

        //    if (품목코드 != string.Empty)
        //        arrList.Add(품목코드);

        //    return arrList;
        //}
    }
}