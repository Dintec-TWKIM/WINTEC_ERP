using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_ROUT_REG_BIZ
    {
        private IU_Language L = new IU_Language();

        public DataTable Search품목(object[] obj)
		{
            return DBHelper.GetDataTable("SP_CZ_PR_ROUT_PITEM_S", obj);
        }
        
        public DataTable Search공정경로유형(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_PR_ROUT_H_SELECT", obj);

            dataTable.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dataTable.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;

            return dataTable;
        }

        public DataTable Search공정(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_PR_ROUT_REG_L_S", obj);

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(Decimal))
                    column.DefaultValue = 0;
            }

            dataTable.Columns["CD_OP"].DefaultValue = "100";
            dataTable.Columns["TM_SETUP"].DefaultValue = "000000";
            dataTable.Columns["TM"].DefaultValue = "000000";
            dataTable.Columns["TM_MOVE"].DefaultValue = "000000";
            dataTable.Columns["YN_RECEIPT"].DefaultValue = "Y";
            dataTable.Columns["YN_BF"].DefaultValue = "N";
            dataTable.Columns["YN_PAR"].DefaultValue = "N";
            dataTable.Columns["YN_QC"].DefaultValue = "N";
            dataTable.Columns["YN_FINAL"].DefaultValue = "N";
            dataTable.Columns["QT_OVERLAP"].DefaultValue = 2;
            dataTable.Columns["DY_SBCNT"].DefaultValue = 2;
            dataTable.Columns["DY_PLAN"].DefaultValue = 0;

            return dataTable;
        }

        public DataTable Search작업지침서(object[] obj)
		{
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_PR_ROUT_FILE_S", obj);
            return dataTable;
        }

        internal DataTable Search측정항목(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_ROUT_INSP_SUB_S", obj);
        }

        internal DataTable Search설비(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_ROUT_EQUIP_S", obj);
        }

        public ResultData[] SaveData(DataTable dtL, DataTable dtH, DataTable dtFile)
        {
            SpInfoCollection spCollection = new SpInfoCollection();

            if (dtH != null && dtH.Rows.Count > 0)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_PR_ROUT_H_INSERT",
                    SpNameUpdate = "UP_PR_ROUT_H_UPDATE",
                    SpNameDelete = "UP_PR_ROUT_H_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_ITEM",
                                                    "CD_PLANT",
                                                    "NO_OPPATH",
                                                    "DC_OPPATH",
                                                    "CD_DEPT",
                                                    "NO_EMP",
                                                    "ID_INSERT",
                                                    "TP_OPPATH",
                                                    "NO_EMP_WRIT",
                                                    "NO_EMP_CONF",
                                                    "TP_WO" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_ITEM",
                                                    "CD_PLANT",
                                                    "NO_OPPATH",
                                                    "DC_OPPATH",
                                                    "CD_DEPT",
                                                    "NO_EMP",
                                                    "ID_UPDATE",
                                                    "TP_OPPATH",
                                                    "NO_EMP_WRIT",
                                                    "NO_EMP_CONF",
                                                    "TP_WO" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_ITEM",
                                                    "CD_PLANT",
                                                    "NO_OPPATH",
                                                    "TP_OPPATH" }
                });
            }

            if (dtL != null && dtL.Rows.Count > 0)
			{
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "SP_CZ_PR_ROUT_REG_L_I",
                    SpNameUpdate = "SP_CZ_PR_ROUT_REG_L_U",
                    SpNameDelete = "SP_CZ_PR_ROUT_REG_L_D",
                    SpParamsInsert = new string[] { "CD_WCOP",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_ITEM",
                                                    "NO_OPPATH",
                                                    "CD_PLANT",
                                                    "CD_COMPANY",
                                                    "TM_SETUP",
                                                    "TM",
                                                    "TM_MOVE",
                                                    "CD_RSRC",
                                                    "TP_RSRC",
                                                    "YN_RECEIPT",
                                                    "YN_BF",
                                                    "QT_OVERLAP",
                                                    "N_OPSPLIT",
                                                    "YN_PAR",
                                                    "DY_SBCNT",
                                                    "CD_TOOL",
                                                    "ID_INSERT",
                                                    "YN_QC",
                                                    "YN_FINAL",
                                                    "TP_OPPATH",
                                                    "CD_EQUIP",
                                                    "RT_YIELD",
                                                    "QT_LABOR_PLAN",
                                                    "SET_REASON",
                                                    "DC_RMK",
                                                    "SET_METHOD",
                                                    "DY_PLAN",
                                                    "NO_SFT",
                                                    "YN_ROUT_SU_IV",
                                                    "YN_INSP",
                                                    "DC_OP",
                                                    "YN_USE",
                                                    "NUM_USERDEF1",
                                                    "NUM_USERDEF2",
                                                    "NUM_USERDEF3",
                                                    "TXT_USERDEF1",
                                                    "TXT_USERDEF2",
                                                    "TXT_USERDEF3",
                                                    "CD_USERDEF1",
                                                    "CD_USERDEF2",
                                                    "CD_USERDEF3",
                                                    "NUM_USERDEF4",
                                                    "NUM_USERDEF5",
                                                    "NUM_USERDEF6",
                                                    "NUM_USERDEF7",
                                                    "NUM_USERDEF8",
                                                    "NUM_USERDEF9",
                                                    "TXT_USERDEF4",
                                                    "TXT_USERDEF5",
                                                    "TXT_USERDEF6",
                                                    "TXT_USERDEF7",
                                                    "TXT_USERDEF8",
                                                    "TXT_USERDEF9" },
                    SpParamsUpdate = new string[] { "CD_WCOP",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_ITEM",
                                                    "NO_OPPATH",
                                                    "CD_PLANT",
                                                    "CD_COMPANY",
                                                    "TM_SETUP",
                                                    "TM",
                                                    "TM_MOVE",
                                                    "CD_RSRC",
                                                    "TP_RSRC",
                                                    "YN_RECEIPT",
                                                    "YN_BF",
                                                    "QT_OVERLAP",
                                                    "N_OPSPLIT",
                                                    "YN_PAR",
                                                    "DY_SBCNT",
                                                    "CD_TOOL",
                                                    "ID_UPDATE",
                                                    "YN_QC",
                                                    "YN_FINAL",
                                                    "TP_OPPATH",
                                                    "CD_EQUIP",
                                                    "RT_YIELD",
                                                    "QT_LABOR_PLAN",
                                                    "SET_REASON",
                                                    "DC_RMK",
                                                    "SET_METHOD",
                                                    "DY_PLAN",
                                                    "NO_SFT",
                                                    "YN_ROUT_SU_IV",
                                                    "YN_INSP",
                                                    "DC_OP",
                                                    "YN_USE",
                                                    "NUM_USERDEF1",
                                                    "NUM_USERDEF2",
                                                    "NUM_USERDEF3",
                                                    "TXT_USERDEF1",
                                                    "TXT_USERDEF2",
                                                    "TXT_USERDEF3",
                                                    "CD_USERDEF1",
                                                    "CD_USERDEF2",
                                                    "CD_USERDEF3",
                                                    "NUM_USERDEF4",
                                                    "NUM_USERDEF5",
                                                    "NUM_USERDEF6",
                                                    "NUM_USERDEF7",
                                                    "NUM_USERDEF8",
                                                    "NUM_USERDEF9",
                                                    "TXT_USERDEF4",
                                                    "TXT_USERDEF5",
                                                    "TXT_USERDEF6",
                                                    "TXT_USERDEF7",
                                                    "TXT_USERDEF8",
                                                    "TXT_USERDEF9" },
                    SpParamsDelete = new string[] { "CD_OP",
                                                    "CD_ITEM",
                                                    "NO_OPPATH",
                                                    "CD_PLANT",
                                                    "CD_COMPANY",
                                                    "TP_OPPATH" }
                });
            }

            if (dtFile != null && dtFile.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtFile);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_PR_ROUT_FILE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                spCollection.Add(si);
            }

            return (ResultData[])Global.MainFrame.Save(spCollection);
        }

        public bool SaveFile(DataTable dtFile)
		{
            if (dtFile != null && dtFile.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtFile);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_PR_ROUT_FILE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                return DBHelper.Save(si);
            }

            return false;
        }

        internal bool Save측정항목(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_PR_ROUT_INSP_SUB_XML";
            si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

            return DBHelper.Save(si);
        }

        internal bool Save설비(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_PR_ROUT_EQUIP_XML";
            si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

            return DBHelper.Save(si);
        }
    }
}