using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    public class P_CZ_SA_PTRPRICE_BIZ
    {
        public bool Save(DataTable dt)
        {
            return ((ResultData)Global.MainFrame.Save(new SpInfo()
            {
                DataValue = dt,
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                SpNameInsert = "UP_SA_PTRPRICE_NEW_INSERT",
                SpNameUpdate = "UP_SA_PTRPRICE_NEW_UPDATE",
                SpNameDelete = "UP_SA_PTRPRICE_NEW_DELETE",
                SpParamsInsert = new string[] { "CD_ITEM",
                                                "CD_PARTNER",
                                                "FG_UM",
                                                "CD_EXCH",
                                                "NO_LINE",
                                                "TP_UMMODULE",
                                                "CD_COMPANY",
                                                "CD_PLANT",
                                                "UM_ITEM",
                                                "UM_ITEM_LOW",
                                                "SDT_UM",
                                                "EDT_UM",
                                                "ID_INSERT",
                                                "DC_RMK",
                                                "NUM_USERDEF1",
                                                "NUM_USERDEF2" },
                SpParamsUpdate = new string[] { "CD_ITEM",
                                                "CD_PARTNER",
                                                "FG_UM",
                                                "CD_EXCH",
                                                "NO_LINE",
                                                "TP_UMMODULE",
                                                "CD_COMPANY",
                                                "CD_PLANT",
                                                "UM_ITEM",
                                                "UM_ITEM_LOW",
                                                "SDT_UM",
                                                "EDT_UM",
                                                "CD_ITEM_ORIGIN",
                                                "FG_UM_ORIGIN",
                                                "CD_EXCH_ORIGIN",
                                                "ID_UPDATE",
                                                "DC_RMK",
                                                "NUM_USERDEF1",
                                                "NUM_USERDEF2" },
                SpParamsDelete = new string[] { "CD_ITEM",
                                                "CD_PARTNER",
                                                "FG_UM",
                                                "CD_EXCH",
                                                "NO_LINE",
                                                "TP_UMMODULE",
                                                "CD_COMPANY",
                                                "CD_PLANT",
                                                "SDT_UM" }
            })).Result;
        }

        public bool Save_AddRows(DataTable dt, string MULTI_CD_PARTNER)
        {
            return DBHelper.Save(new SpInfo()
            {
                DataValue = dt,
                DataState = DataValueState.Added,
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                SpNameInsert = "UP_SA_PTRPRICE_NEW_ADDROWS_I",
                SpParamsInsert = new string[] { "CD_ITEM",
                                                "CD_PARTNER1",
                                                "FG_UM",
                                                "CD_EXCH",
                                                "NO_LINE",
                                                "TP_UMMODULE",
                                                "CD_COMPANY",
                                                "CD_PLANT",
                                                "UM_ITEM",
                                                "UM_ITEM_LOW",
                                                "SDT_UM",
                                                "EDT_UM",
                                                "ID_INSERT",
                                                "DC_RMK",
                                                "NUM_USERDEF1" },
                SpParamsValues = { { ActionState.Insert, "CD_PARTNER1", MULTI_CD_PARTNER } }
            });
        }

        public DataTable Search(object[] obj)
        {
            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_PTRPRICE_NEW_SELECT_H",
                SpParamsSelect = obj
            })).DataValue;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_PTRPRICE_NEW_SELECT_L",
                SpParamsSelect = obj
            })).DataValue;

            T.SetDefaultValue(dt);
            dt.Columns["S"].DefaultValue = "N";
            
            return dt;
        }

        internal DataTable SearchAddRows(object[] obj)
        {
            return DBHelper.GetDataTable("UP_SA_PTRPRICE_NEW_ADDROWS_S", obj, "CD_PARTNER ASC");
        }

        internal DataTable GetMaxLine(string 품목, string 거래처)
        {
            return DBHelper.GetDataTable("UP_SA_PTRPRICE_S_EXCEL1", new object[] { MA.Login.회사코드,
                                                                                   품목,
                                                                                   거래처 });
        }

        public DataTable TotalLine(string KEY, string TP_UMMODULE)
        {
            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_PTRPRICE_SELECT_TOTAL_L",
                SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                KEY,
                                                TP_UMMODULE }
            })).DataValue;
        }

        internal DataTable ExcelSearch(string 멀티품목코드, string 구분자, string 공장)
        {
            ArrayListExt arrayListExt = this.arr엑셀(멀티품목코드);
            DataTable dataTable1 = (DataTable)null;
            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataTable dataTable2 = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
                {
                    SpNameSelect = "UP_SA_PTRPRICE_SELECT_L_EXCEL",
                    SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                    arrayListExt[index].ToString(),
                                                    구분자,
                                                    공장 }
                })).DataValue;

                if (dataTable1 == null)
                    dataTable1 = dataTable2;
                else
                {
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                        dataTable1.ImportRow(row);
                }
            }
            return dataTable1;
        }

        internal DataTable 엑셀(DataTable dt_엑셀)
        {
            dt_엑셀.Columns.Add("LN_PARTNER", typeof(string));
            dt_엑셀.Columns.Add("TP_UMMODULE", typeof(string));
            dt_엑셀.Columns.Add("NM_USER", typeof(string));
            dt_엑셀.Columns.Add("DT_INSERT", typeof(string));
            dt_엑셀.Columns.Add("NO_LINE", typeof(Decimal));
            return dt_엑셀;
        }

        private ArrayListExt arr엑셀(string 멀티품목코드)
        {
            int num1 = 50;
            int num2 = 1;
            string str = string.Empty;
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티품목코드.Split('|');
            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index].ToString() + "|";
                if (num2 == num1)
                {
                    arrayListExt.Add(str);
                    str = string.Empty;
                    num2 = 0;
                }
                ++num2;
            }
            if (str != string.Empty)
                arrayListExt.Add(str);
            return arrayListExt;
        }

        public DataTable Print(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_SA_PTRPRICE_NEW_PRINT_S", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchMfgAuth()
        {
            return DBHelper.GetDataTable(@"SELECT A.CD_AUTH CODE, 
                                                  B.NM_SYSDEF NAME 
                                           FROM MA_MFG_AUTH A WITH(NOLOCK) 
                                           JOIN MA_CODEDTL B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND B.CD_FIELD = 'SA_B000021' AND A.CD_AUTH = B.CD_SYSDEF
                                           WHERE A.CD_COMPANY = '" + MA.Login.회사코드 + "'" +
                                          "AND A.NO_EMP = '" + MA.Login.사원번호 + "'" +
                                          "AND A.FG_AUTH = 'TP_PRICE'" +
                                          "AND B.USE_YN = 'Y'");
        }
    }
}
