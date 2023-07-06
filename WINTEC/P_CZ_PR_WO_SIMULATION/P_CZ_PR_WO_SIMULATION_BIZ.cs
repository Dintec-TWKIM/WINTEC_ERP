using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_PR_WO_SIMULATION_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_SIMULATION_H_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchLine(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_SIMULATION_WL_S", obj);
            return dt;
        }

        public DataTable SearchLineDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_SIMULATION_WLD_S", obj);
            return dt;
        }

        public DataTable 작업지시등록(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_SIMULATION_I", obj);
            return dt;
        }

        public bool 수주추적정보등록(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_SA_SOL_PR_WO_MAPPING_XML", new object[] { GetTo.Xml(dt) });
        }

        public void 작업지시RELEASE(object[] obj)
        {
            string query = @"EXEC UP_PR_WO_REL_INSERT @P_CD_COMPANY = '{0}',
								                      @P_CD_PLANT = '{1}',
								                      @P_NO_WO = '{2}',
							    	                  @P_ST_WO = 'R',
								                      @P_DT_RELEASE = '{3}',
								                      @P_QT_ITEM = '{4}',
								                      @P_ID_INSERT = '{5}',
                                                      @P_NO_LOT = NULL,
								                      @P_DC_RMK = NULL";

            DBHelper.ExecuteScalar(string.Format(query, obj));
        }

        public DataTable 작업지시삭제(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PR_WO_REG_NEW_DELETE", obj);
            return dt;
        }

        public bool 구매요청등록 (DataTable dtH, DataTable dtL)
		{
            SpInfoCollection si = new SpInfoCollection();

            if (dtH != null && dtH.Rows.Count > 0)
			{
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_PU_PRH_I";
                spInfo.SpParamsInsert = new string[] { "NO_PR",
                                                       "CD_PLANT",
                                                       "CD_COMPANY",
                                                       "CD_DEPT",
                                                       "DT_PR",
                                                       "NO_EMP",
                                                       "CD_PJT",
                                                       "FG_PR_TP",
                                                       "NO_PRTYPE",
                                                       "DC_RMK",
                                                       "ID_INSERT" };

                si.Add(spInfo);
            }

            if (dtL != null && dtL.Rows.Count > 0)
			{
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_PU_PRL_I";
                spInfo.SpParamsInsert = new string[] { "NO_PR",
                                                       "NO_PRLINE",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_PURGRP",
                                                       "CD_ITEM",
                                                       "DT_LIMIT",
                                                       "QT_PR",
                                                       "QT_PO",
                                                       "QT_PO_MM",
                                                       "FG_PRCON",
                                                       "CD_SL",
                                                       "FG_POST",
                                                       "NO_SO",
                                                       "NO_SOLINE",
                                                       "RT_PO",
                                                       "ID_INSERT" };

                si.Add(spInfo);
            }

            return DBHelper.Save(si);
        }

        public bool 구매요청추적정보등록(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_SA_SOL_PU_PRL_MAPPING_XML", new object[] { GetTo.Xml(dt) });
        }

        public bool 구매요청삭제(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PU_PR_D", obj);
        }

        public bool 외주요청등록(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection si = new SpInfoCollection();

            if (dtH != null && dtH.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_SU_PRH_I";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PR",
                                                       "DT_PR",
                                                       "CD_DEPT",
                                                       "NO_EMP",
                                                       "CD_PURGRP",
                                                       "FG_PR",
                                                       "CD_PJT",
                                                       "DC_RMK",
                                                       "ID_INSERT" };

                si.Add(spInfo);
            }

            if (dtL != null && dtL.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "SP_CZ_SU_PRL_I";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PR",
                                                       "NO_LINE",
                                                       "DT_DLV",
                                                       "CD_ITEM",
                                                       "QT_PR",
                                                       "QT_PO",
                                                       "CD_PARTNER",
                                                       "ST_PROC",
                                                       "NO_SO",
                                                       "NO_SOLINE",
                                                       "DC_RMK",
                                                       "ID_INSERT" };

                si.Add(spInfo);
            }

            return DBHelper.Save(si);
        }

        public bool 외주요청추적정보등록(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_SA_SOL_SU_PRL_MAPPING_XML", new object[] { GetTo.Xml(dt) });
        }

        public bool 외주요청삭제(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SU_PR_D", obj);
        }

        public bool 데이터정리(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_SIMULATION_D", obj);
        }

        public bool 재고이동등록(string 공장, DataTable dt)
		{
            if (dt == null || dt.Rows.Count == 0) return false;

            string xml = Util.GetTO_Xml(dt);

            return DBHelper.ExecuteNonQuery("SP_CZ_PR_STOCK_GI_XML", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    공장,
                                                                                    Global.MainFrame.LoginInfo.EmployeeNo,
                                                                                    xml,
                                                                                    Global.MainFrame.LoginInfo.UserID });
        }

        public bool 재고이동삭제(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_STOCK_GI_D", obj);
        }
    }
}
