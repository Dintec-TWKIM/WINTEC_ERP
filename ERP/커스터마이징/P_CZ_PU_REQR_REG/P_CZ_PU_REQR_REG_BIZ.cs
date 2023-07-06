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
    internal class P_CZ_PU_REQR_REG_BIZ
    {
        public DataSet Search(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("UP_PU_REQR_SELECT", obj);
            DataTable dataTable1 = dataSet.Tables[0];
            dataTable1.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dataTable1.Columns["DT_REQ"].DefaultValue = Global.MainFrame.GetStringToday;
            dataTable1.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataTable1.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dataTable1.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dataTable1.Columns["CD_EXCH"].DefaultValue = "000";
            dataTable1.Columns["FG_TRANS"].DefaultValue = "001";
            dataTable1.Columns["FG_PROCESS"].DefaultValue = "";
            dataTable1.Columns["CD_SL"].DefaultValue = "";

            foreach (DataTable dataTable2 in dataSet.Tables)
            {
                foreach (DataColumn dataColumn in dataTable2.Columns)
                {
                    if (dataColumn.DataType == Type.GetType("System.Decimal"))
                        dataColumn.DefaultValue = 0;
                }
            }

            dataTable1.Columns["RT_EXCH"].DefaultValue = 1;
            
            return dataSet;
        }

        public bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            SpInfo spInfo;

            if (dtH != null)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_PU_RCVH_INSERT";
                spInfo.SpNameUpdate = "UP_PU_RCVH_UPDATE";
                spInfo.SpParamsInsert = new string[] { "NO_RCV",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_PARTNER",
                                                       "DT_REQ",
                                                       "NO_EMP",
                                                       "FG_TRANS",
                                                       "FG_PROCESS",
                                                       "CD_EXCH",
                                                       "CD_SL",
                                                       "YN_RETURN",
                                                       "YN_AM",
                                                       "DC_RMK",
                                                       "ID_INSERT",
                                                       "FG_RCV",
                                                       "CD_DEPT",
                                                       "FG_UM" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_RCV",
                                                       "DC_RMK",
                                                       "ID_UPDATE" };

                spCollection.Add(spInfo);

            }
            if (dtL != null)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_PU_REQR_INSERT";
                spInfo.SpNameUpdate = "UP_PU_REQR_UPDATE";
                spInfo.SpNameDelete = "UP_PU_REQR_DELETE";
                spInfo.SpParamsInsert = new string[] { "NO_RCV",
                                                       "NO_LINE",
                                                       "CD_COMPANY",
                                                       "CD_PURGRP",
                                                       "DT_LIMIT",
                                                       "CD_ITEM",
                                                       "QT_REQ",
                                                       "QT_REQ_MM",
                                                       "YN_INSP",
                                                       "CD_UNIT_MM",
                                                       "CD_EXCH",
                                                       "RT_EXCH",
                                                       "UM_EX",
                                                       "AM_EX",
                                                       "UM",
                                                       "AM",
                                                       "VAT",
                                                       "CD_PJT",
                                                       "YN_PURCHASE",
                                                       "YN_RETURN",
                                                       "FG_TPPURCHASE",
                                                       "FG_RCV",
                                                       "FG_TRANS",
                                                       "FG_TAX",
                                                       "FG_TAXP",
                                                       "CD_SL",
                                                       "NO_EMP",
                                                       "NO_IO_MGMT",
                                                       "NO_IOLINE_MGMT",
                                                       "NO_PO_MGMT",
                                                       "NO_POLINE_MGMT",
                                                       "DC_RMK",
                                                       "UM_EX_PO",
                                                       "NO_PO",
                                                       "NO_POLINE",
                                                       "SEQ_PROJECT",
                                                       "NO_WBS",
                                                       "NO_CBS",
                                                       "TP_UM_TAX" };
                spInfo.SpParamsUpdate = new string[] { "NO_RCV",
                                                       "NO_LINE",
                                                       "CD_COMPANY",
                                                       "DT_LIMIT",
                                                       "QT_REQ",
                                                       "QT_REQ_MM",
                                                       "UM_EX",
                                                       "AM_EX",
                                                       "UM",
                                                       "AM",
                                                       "VAT",
                                                       "CD_SL",
                                                       "DC_RMK",
                                                       "UM_EX_PO",
                                                       "FG_TAX",
                                                       "TP_UM_TAX" };
                spInfo.SpParamsDelete = new string[] { "NO_RCV",
                                                       "NO_LINE",
                                                       "CD_COMPANY" };

                spCollection.Add(spInfo);

            }

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public DataSet Search_RATE_EXCHG(object[] obj)
        {
            return DBHelper.GetDataSet("UP_PU_PITEM_RATE_EXCHG_SELECT", obj);
        }

        public DataSet ItemInfo_Search(object[] obj)
        {
            return DBHelper.GetDataSet("UP_PU_REQR_ITEMINFO_SELECT", obj);
        }

        public void Delete(object[] p_param)
        {
            Global.MainFrame.ExecSp("UP_PU_RCVH_DELETE", p_param);
        }

        public DataTable GetPartnerCodeSearch()
        {
            string query = @"SELECT CD_EXC    
                             FROM MA_EXC WITH(NOLOCK)    
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                           @"AND EXC_TITLE = '입고반품-추가옵션'";

            return Global.MainFrame.FillDataTable(query);
        }

        public DataSet ProjectSearch(object[] obj)
        {
            string query = @"SELECT PH.NO_PROJECT, 
                                    PH.NM_PROJECT, 
                                    PH.CD_PARTNER, 
                                    MP.LN_PARTNER,
                                    PH.CD_SALEGRP, 
                                    MS.NM_SALEGRP
                             FROM SA_PROJECTH PH WITH(NOLOCK) 
                             LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON PH.CD_COMPANY = MP.CD_COMPANY AND PH.CD_PARTNER = MP.CD_PARTNER
                             LEFT JOIN MA_SALEGRP MS WITH(NOLOCK) ON PH.CD_COMPANY = MS.CD_COMPANY AND PH.CD_SALEGRP = MS.CD_SALEGRP
                             WHERE PH.CD_COMPANY = '" + obj[0].ToString() + "'" +
                           @"AND (PH.NO_PROJECT  = '" + obj[1].ToString() + "'" + 
                           @"OR '" + obj[1].ToString() + "' = ''" + 
                           @"OR '" + obj[1].ToString() + "' IS NULL" +
                           @"OR PH.NO_PROJECT LIKE '%'+ '" + obj[1].ToString() + "' +'%'" +
                           @"OR  PH.NM_PROJECT  = '" + obj[1].ToString() + "'" + 
                           @"OR '" + obj[1].ToString() + "' = ''" + 
                           @"OR '" + obj[1].ToString() + "' IS NULL" + 
                           @"OR PH.NM_PROJECT LIKE '%'+ '" + obj[1].ToString() + "' +'%')" +
                           @"ORDER  BY PH.NO_PROJECT ";

            return Global.MainFrame.FillDataSet(query);
        }

        public string EnvSearch()
        {
            string str = "N";
            string query = @"SELECT CD_TP   
                             FROM PU_ENV WITH(NOLOCK)   
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + 
                           @"AND FG_TP = '001' ";

            DataTable dataTable = Global.MainFrame.FillDataTable(query);

            if (dataTable.Rows.Count > 0 && (dataTable.Rows[0]["CD_TP"] != DBNull.Value && dataTable.Rows[0]["CD_TP"].ToString().Trim() != string.Empty))
                str = dataTable.Rows[0]["CD_TP"].ToString();
            
            return str;
        }
    }
}
