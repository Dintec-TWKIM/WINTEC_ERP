using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_PO_REG_BIZ
    {
        public DataTable Day(string 거래처)
        {
            return DBHelper.GetDataTable("UP_MA_PARTNER_SELECT_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  거래처 });
        }

        public DataSet Search(object[] obj)
        {
            DataSet dataValue = (DataSet)((ResultData)Global.MainFrame.FillDataSet("SP_CZ_PR_OPOUT_PO_REG_S", obj)).DataValue;

            foreach (DataTable table in dataValue.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == typeof(decimal))
                        column.DefaultValue = 0;
                }
            }

            dataValue.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dataValue.Tables[0].Columns["DT_PO"].DefaultValue = Global.MainFrame.GetStringToday;
            dataValue.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataValue.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dataValue.Tables[0].Columns["NM_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptName;
            dataValue.Tables[1].Columns["DT_DUE"].DefaultValue = Global.MainFrame.GetStringToday;
            
            return dataValue;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_OPOUT_PO_REG_ID_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataSet Search_Um(object[] obj)
        {
            return DBHelper.GetDataSet("UP_SU_COMMON_UM_S", obj);
        }

        public void DeleteAll(string 공장, string 발주번호)
        {
            Global.MainFrame.ExecSp("UP_PR_OPOUT_PO_REG_DELETE_ALL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    공장,
                                                                                    발주번호 });
        }

        public bool Save(DataTable dtH, DataTable dtL, DataTable dtID, DataRow drHeader)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dtH != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PR_OPOUT_POH_INSERT",
                    SpNameUpdate = "UP_PR_OPOUT_POH_UPDATE",
                    SpNameDelete = "UP_PR_OPOUT_POH_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PO",
                                                    "CD_PARTNER",
                                                    "NO_EMP",
                                                    "DT_PO",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "FG_TAX",
                                                    "VAT_RATE",
                                                    "AM_EX",
                                                    "AM",
                                                    "AM_VAT",
                                                    "DC_RMK",
                                                    "ID_INSERT",
                                                    "COND_PAYMENT",
                                                    "COND_PRICE",
                                                    "DC_RMK_TEXT1" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PO",
                                                    "CD_PARTNER",
                                                    "NO_EMP",
                                                    "DT_PO",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "FG_TAX",
                                                    "VAT_RATE",
                                                    "AM_EX",
                                                    "AM",
                                                    "AM_VAT",
                                                    "DC_RMK",
                                                    "ID_UPDATE",
                                                    "COND_PAYMENT",
                                                    "COND_PRICE",
                                                    "DC_RMK_TEXT1" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PO" }
                });
            if (dtL != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_CZ_PR_OPOUT_POL_INSERT",
                    SpNameUpdate = "UP_CZ_PR_OPOUT_POL_UPDATE",
                    SpNameDelete = "UP_PR_OPOUT_POL_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PO",
                                                    "NO_LINE",
                                                    "NO_WO",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "CD_ITEM",
                                                    "DT_DUE",
                                                    "QT_PO",
                                                    "QT_RCV",
                                                    "QT_CLS",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM",
                                                    "AM",
                                                    "AM_VAT",
                                                    "DC_RMK",
                                                    "ID_INSERT",
                                                    "UM_MATL",
                                                    "UM_SOUL",
                                                    "OLD_QT_PO",
                                                    "UNIT_CH",
                                                    "QT_CHCOEF",
                                                    "NO_WORK",
                                                    "NO_PR"},
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PO",
                                                    "NO_LINE",
                                                    "NO_WO",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "CD_ITEM",
                                                    "DT_DUE",
                                                    "QT_PO",
                                                    "QT_RCV",
                                                    "QT_CLS",
                                                    "UM_EX",
                                                    "AM_EX",
                                                    "UM",
                                                    "AM",
                                                    "AM_VAT",
                                                    "DC_RMK",
                                                    "ID_UPDATE",
                                                    "UM_MATL",
                                                    "UM_SOUL",
                                                    "OLD_QT_PO",
                                                    "UNIT_CH",
                                                    "QT_CHCOEF",
                                                    "NO_WORK",
                                                    "NO_PR"},
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PO",
                                                    "NO_LINE" },
                    SpParamsValues = { { ActionState.Insert, "NO_PO", drHeader["NO_PO"].ToString() },
                                       { ActionState.Update, "NO_PO", drHeader["NO_PO"].ToString() },
                                       { ActionState.Delete, "NO_PO", drHeader["NO_PO"].ToString() } }
                });
            if (dtID != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtID,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "SP_CZ_PR_OPOUT_PO_REG_INSP_U",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "S",
                                                    "NO_WO",
                                                    "NO_WO_LINE",
                                                    "SEQ_WO",
                                                    "NO_PO",
                                                    "NO_OPOUT_PO",
                                                    "NO_OPOUT_PR",
                                                    "NO_PR_LINE",
                                                    "ID_UPDATE"},
                    SpParamsValues = {{ ActionState.Update, "NO_PO", drHeader["NO_PO"].ToString() }}
                });

                foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }

        public DataTable Print_SOLIDTECH(string 공장코드, string 발주번호)
        {
            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo() { SpNameSelect = "UP_PR_OPOUT_PO_REG_P_SOLIDTECH",
                                                                                         SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                         공장코드,
                                                                                                                         발주번호 } })).DataValue;
        }

        public DataTable Print(string 거래처코드)
        {
            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_MA_PARTNER_SELECT_D",
                SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                거래처코드,
                                                Global.SystemLanguage.MultiLanguageLpoint }
            })).DataValue;
        }

        public Decimal ExchangeSearch(object[] obj)
        {
            Decimal num = 1M;
            DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT RATE_BASE   
                                                                        FROM MA_EXCHANGE WITH(NOLOCK)
                                                                        WHERE YYMMDD = '{0}'
                                                                        AND CURR_SOUR = '{1}'
                                                                        AND CURR_DEST = '000'
                                                                        AND CD_COMPANY = '{2}'", obj[1].ToString(), obj[2].ToString(), obj[0].ToString()));
            
            if (dataTable != null && dataTable.Rows.Count > 0 && D.GetDecimal(dataTable.Rows[0]["RATE_BASE"]) != 0M)
                num = D.GetDecimal(dataTable.Rows[0]["RATE_BASE"]);
            
            return num;
        }
    }
}