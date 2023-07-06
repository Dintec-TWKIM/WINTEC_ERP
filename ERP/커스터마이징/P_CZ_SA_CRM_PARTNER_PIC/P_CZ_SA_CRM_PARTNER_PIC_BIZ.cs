using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Dintec;

namespace cz
{
	class P_CZ_SA_CRM_PARTNER_PIC_BIZ
	{
		internal DataTable Search(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_S", obj);

			T.SetDefaultValue(dataTable);
			return dataTable;
		}

		internal DataTable Search부가정보(object[] obj)
		{
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_ITEM_S", obj);

			T.SetDefaultValue(dataTable);
			return dataTable;
		}

        internal DataTable Search관련인물(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_PEOPLE_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataSet Search인물관계도(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_CRM_PARTNER_PIC_PEOPLE_TREE", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataTable Search근무이력(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_COMPANY_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search담당호선(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_HULL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search담당호선리스트(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_HULL_LIST_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search영업활동(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_ACTIVITY_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search미팅메모(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_MEETING_MEMO_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search커미션(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_COMMISSION_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search물류서비스(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_LOG_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public string SearchFileInfo(string idMenu, string fileCode)
        {
            string query, result = string.Empty;

            try
            {
                query = @"SELECT CONVERT(VARCHAR, COUNT(1)) FILE_PATH_MNG
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = 'K100'" + Environment.NewLine +
                        @"AND CD_MODULE = 'SA'
                          AND ID_MENU = '" + idMenu + "'" + Environment.NewLine +
                         "AND CD_FILE = '" + fileCode + "'";

                result = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        internal bool Save(DataTable dt담당자정보, DataTable dt부가정보, DataTable dt관련인물, DataTable dt근무이력, DataTable dt담당호선, DataTable dt물류서비스)
		{
			SpInfoCollection sc = new SpInfoCollection();

            #region 담당자정보
            if (dt담당자정보 != null && dt담당자정보.Rows.Count != 0)
			{
				SpInfo si = new SpInfo();

                si.DataValue = dt담당자정보;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameUpdate = "SP_CZ_SA_CRM_PARTNER_PIC_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "SEQ",
												   "CD_GENDER",
												   "CD_NATION",
												   "CD_RELIGION",
                                                   "CD_RANK",
												   "DT_BERTH",
												   "DC_MEMO",
                                                   "ID_UPDATE" };

				sc.Add(si);
			}
            #endregion

            #region 부가정보
            if (dt부가정보 != null && dt부가정보.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt부가정보);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_PIC_ITEM_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }
            #endregion

            #region 관련인물
            if (dt관련인물 != null && dt관련인물.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt관련인물);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_PIC_PEOPLE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }
            #endregion

            #region 근무이력
            if (dt근무이력 != null && dt근무이력.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt근무이력);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_PIC_COMPANY_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }
            #endregion

            #region 담당호선
            if (dt담당호선 != null && dt담당호선.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt담당호선);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_PIC_HULL_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }
            #endregion

            #region 물류서비스
            if (dt물류서비스 != null && dt물류서비스.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt물류서비스);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_PIC_LOG_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }
            #endregion

            return DBHelper.Save(sc);
		}
	}
}
