using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using System.IO;
using Dintec;

namespace cz
{
	internal class P_CZ_FI_IMP_PMT_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_IMP_PMT_MNGH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_IMP_PMT_MNGL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

        public DataTable SearchA(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_IMPORT_DECLARATIONH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetailA(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_IMPORT_DECLARATIONL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetailP(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_IMPORT_POL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool SaveData(DataTable dtH, DataTable dtL)
		{
			SpInfoCollection sc = new SpInfoCollection();

			if (dtH != null && dtH.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = dtH;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
				si.SpNameUpdate = "SP_CZ_FI_IMP_PMT_MNGH_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "NO_IMPORT",		
                                                   "NO_BL",
                                                   "DT_APPLICATION",
                                                   "DT_RETURN",
                                                   "DC_FREIGHT",
                                                   "CD_PAYMENT",
                                                   "DT_LIMIT",
                                                   "AM_VAT_BASE",
                                                   "AM_VAT",
                                                   "DC_RMK",
                                                   "ID_UPDATE" };

				si.SpNameDelete = "SP_CZ_FI_IMP_PMT_MNGH_D";
				si.SpParamsDelete = new string[] { "CD_COMPANY",
												   "NO_IMPORT" };

				sc.Add(si);
			}

			if (dtL != null && dtL.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = Util.GetXmlTable(dtL);
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "SP_CZ_FI_IMP_PMT_MNGL_XML";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IMPORT", "XML", "ID_INSERT" };

                si.SpParamsValues.Add(ActionState.Insert, "NO_IMPORT", string.Empty);

				sc.Add(si);
			}

			if (sc.List != null && sc.List.Count > 0)
				return DBHelper.Save(sc);
			else
				return true;
		}

        public bool 납부전표처리(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_IMP_PMT_MNG_APPLICATION_DOCU", obj);
        }

        public bool 환급전표처리(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_IMP_PMT_MNG_RETURN_DOCU", obj);
        }

        public bool 전표취소(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_FI_DOCU_AUTODEL", obj);
        }

        public void SaveFileInfo(string cdCompany, string cdFile, FileInfo fileInfo, string 업로드위치, string 메뉴명)
        {
            string query;

            try
            {
                query = @"SELECT MAX(NO_SEQ) 
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + cdCompany + "'" + Environment.NewLine +
                        @"AND CD_MODULE = 'FI'
                          AND ID_MENU = '" + 메뉴명 + "'" + Environment.NewLine +
                         "AND CD_FILE = '" + cdFile + "'";

                DBHelper.ExecuteNonQuery("UP_MA_FILEINFO_INSERT", new object[] { cdCompany,
                                                                                 "FI",
                                                                                 메뉴명,
                                                                                 cdFile,
                                                                                 (D.GetDecimal(DBHelper.ExecuteScalar(query)) + 1),
                                                                                 fileInfo.Name,
                                                                                 업로드위치,
                                                                                 fileInfo.Extension.Replace(".", ""),
                                                                                 fileInfo.LastWriteTime.ToString("yyyyMMdd"),
                                                                                 fileInfo.LastWriteTime.ToString("hhmm"),
                                                                                 string.Format("{0:0.00}M", ((Decimal)fileInfo.Length / new Decimal(1048576))),
                                                                                 Global.MainFrame.LoginInfo.UserID });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
