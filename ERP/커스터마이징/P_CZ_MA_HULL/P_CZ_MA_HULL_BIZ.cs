using System;
using System.Data;
using System.Linq;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.IO;
using Dintec;
using System.Windows.Forms;

namespace cz
{
	class P_CZ_MA_HULL_BIZ
	{
		internal DataTable Search(object[] obj, bool 입항일자)
		{
            DataTable dataTable;

            if (입항일자)
                dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_S1", obj);
            else
                dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_S", obj);

            T.SetDefaultValue(dataTable);
			return dataTable;
		}

        internal DataTable Search엔진정보(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_ENGINE_S", obj);

            if (!Certify.IsLive())
            {
                dataTable.Columns.Remove("CAPACITY");
                dataTable.Columns.Remove("SERIAL");
                dataTable.Columns.Remove("DC_RMK");
            }

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

		internal DataTable Search기부속정보(object[] obj)
		{
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_ENGINE_ITEM_S", obj);

			T.SetDefaultValue(dataTable);
			return dataTable;
		}

        internal DataTable Search매입처정보(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_VENDOR_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

		internal DataTable Search유형정보(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_VENDOR_TYPE_S", obj);

			T.SetDefaultValue(dataTable);
			return dataTable;
		}

		internal DataTable Search기자재정보(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_HULL_VENDOR_ITEM_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt호선정보, DataTable dt엔진정보, DataTable dt기부속정보, DataTable dt매입처정보, DataTable dt유형정보, DataTable dt기자재정보)
		{
            SpInfoCollection sc = new SpInfoCollection();

            if (dt호선정보 != null && dt호선정보.Rows.Count != 0)
            {
                #region 호선정보
                //return this.save_bulk("CZ_MA_HULL", dtH);

                SpInfo si = new SpInfo();

                //dt.RemotingFormat = SerializationFormat.Binary;

                si.DataValue = Util.GetXmlTable(dt호선정보);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_HULL_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt엔진정보 != null && dt엔진정보.Rows.Count != 0)
            {
                #region 엔진정보
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt엔진정보);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_HULL_ENGINE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt기부속정보 != null && dt기부속정보.Rows.Count != 0)
            {
                #region 기부속정보
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt기부속정보);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_HULL_ENGINE_ITEM_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dt매입처정보 != null && dt매입처정보.Rows.Count != 0)
            {
                #region 매입처정보
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt매입처정보);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_HULL_VENDOR_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

			if (dt유형정보 != null && dt유형정보.Rows.Count != 0)
			{
				#region 유형정보
				SpInfo si = new SpInfo();

				si.DataValue = Util.GetXmlTable(dt유형정보);
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameInsert = "SP_CZ_MA_HULL_VENDOR_TYPE_XML";
				si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

				sc.Add(si);
				#endregion
			}

			if (dt기자재정보 != null && dt기자재정보.Rows.Count != 0)
            {
                #region 기자재정보
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt기자재정보);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_HULL_VENDOR_ITEM_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            return DBHelper.Save(sc);
		}

        internal bool SaveExcelEngineItem(DataTable dtExcel, bool 존재여부확인)
        {
            bool result = false;

            try
            {
                DataRow[] dataRowArray = dtExcel.Select();

                DataSet ds = new DataSet();
                int groupUnit = 100000;

                string xml;

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());

                    if (존재여부확인 == true)
                    {
                        xml = Util.GetTO_Xml(ds.Tables[ds.Tables.Count - 1]);
                        DataTable dt = DBMgr.GetDataTable("SP_CZ_MA_HULL_ENGINE_ITEM_CHECK", new object[] { xml, Global.MainFrame.LoginInfo.UserID });

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string check = string.Empty;

                            foreach(DataRow dr in dt.Rows)
                            {
                                check += dr["NO_IMO"].ToString() + "_" + dr["NO_ENGINE"].ToString() + "_" + dr["NO_PLATE"].ToString() + Environment.NewLine;
                            }

                            Global.MainFrame.ShowDetailMessage("동일한 기부속 데이터가 존재합니다.\n[자세히] 버튼을 눌러서 동일 데이터 확인하시기 바랍니다.", check);

                            return false;
                        }
                    }
                }

                int index = 0;
                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    result = (DBMgr.ExecuteNonQuery("SP_CZ_MA_HULL_ENGINE_ITEM_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return result;
        }

        internal bool SaveExcelVendor(DataTable dtExcel)
        {
            bool result = false;

            try
            {
                DataRow[] dataRowArray = dtExcel.Select();

                DataSet ds = new DataSet();    
                int groupUnit = 100000;

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                int index = 0;
                string xml;

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    result = (DBMgr.ExecuteNonQuery("SP_CZ_MA_HULL_VENDOR_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
                }
            }
            catch(Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return result;
        }

		internal bool SaveExcelVendorType(DataTable dtExcel)
		{
            bool result = false;

            try
            {
                DataRow[] dataRowArray = dtExcel.Select();

                DataSet ds = new DataSet();
                int groupUnit = 100000;

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                int index = 0;
                string xml;

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    result = (DBMgr.ExecuteNonQuery("SP_CZ_MA_HULL_VENDOR_TYPE_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return result;
		}

		internal bool SaveExcelVendorItem(DataTable dtExcel)
		{
            bool result = false;

            try
            {
                DataRow[] dataRowArray = dtExcel.Select();

                DataSet ds = new DataSet();
                int groupUnit = 100000;

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                int index = 0;
                string xml;

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    result = (DBMgr.ExecuteNonQuery("SP_CZ_MA_HULL_VENDOR_ITEM_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
                }
            }
            catch(Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return result;
		}

		internal bool SaveExcelEngine(DataTable dtExcel)
		{
            bool result = false;

            try
            {
                DataRow[] dataRowArray = dtExcel.Select();

                DataSet ds = new DataSet();
                int groupUnit = 100000;

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                int index = 0;
                string xml;

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    result = (DBMgr.ExecuteNonQuery("SP_CZ_MA_HULL_ENGINE_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return result;
		}

		public bool IMO번호중복체크(string IMO번호)
		{
			DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_MA_HULL WITH(NOLOCK)
							                                WHERE NO_IMO = '" + IMO번호 + "'");

			if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
				return true;
			
            return false;
		}

        public bool 일련번호중복체크(string 일련번호)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_MA_HULL_ENGINE WITH(NOLOCK)
							                                WHERE SERIAL = '" + 일련번호 + "'" + Environment.NewLine +
                                                           "AND ISNULL(SERIAL, '') <> ''");

            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;

            return false;
        }

        public bool 사용여부체크(string IMO번호)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_SA_QTNH WITH(NOLOCK)
							                                WHERE NO_IMO = '" + IMO번호 + "'");

            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;

            return false;
        }

        public bool 엔진여부체크(string IMO번호)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_MA_HULL_ENGINE WITH(NOLOCK)
							                                WHERE NO_IMO = '" + IMO번호 + "'");

            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;

            return false;
        }

        public bool 기부속여부체크(string IMO번호, string 엔진번호)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_MA_HULL_ENGINE_ITEM WITH(NOLOCK)
							                                WHERE NO_IMO = '" + IMO번호 + "'" +
                                                          @"AND NO_ENGINE = '" + 엔진번호 + "'");

            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;

            return false;
        }

		public bool 유형여부체크(string IMO번호, string 매입처코드)
		{
			DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_MA_HULL_VENDOR_TYPE WITH(NOLOCK)
							                                WHERE NO_IMO = '" + IMO번호 + "'" +
                                                          @"AND CD_VENDOR= '" + 매입처코드 + "'");

			if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
				return true;

			return false;
		}

		public bool 기자재여부체크(string IMO번호, string 매입처코드, string 유형)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_MA_HULL_VENDOR_ITEM WITH(NOLOCK)
							                                WHERE NO_IMO = '" + IMO번호 + "'" +
                                                           "AND CD_VENDOR = '" + 매입처코드 + "'" + Environment.NewLine +
														   "AND NO_TYPE = '" + 유형 + "'");

            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;

            return false;
        }

        public bool 엔진사용여부체크(string IMO번호, string 엔진번호)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1) 
                                                            FROM CZ_SA_QTNH QH WITH(NOLOCK)
                                                            LEFT JOIN CZ_SA_QTNL QL WITH(NOLOCK) ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
                                                            WHERE QH.NO_IMO = '" + IMO번호 + "'" +
                                                          @"AND QL.NO_ENGINE = '" + 엔진번호 + "'");

            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;

            return false;
        }

		public string SearchFileInfo(string fileCode)
		{
			string query, result = string.Empty;

			try
			{
				query = @"SELECT CONVERT(VARCHAR, COUNT(1)) FILE_PATH_MNG
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = 'K100'" + Environment.NewLine +
						@"AND CD_MODULE = 'MA'
                          AND ID_MENU = 'P_CZ_MA_HULL'
                          AND CD_FILE = '" + fileCode + "'";

				result = D.GetString(Global.MainFrame.ExecuteScalar(query));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return result;
		}

		//public bool save_bulk(string tablename, DataTable dtSave)
		//{
		//    DataTable table = DBHelper.GetDataTable("SELECT * FROM " + tablename);
		//    //SqlBulkCopy sqlBulkCopy = new SqlBulkCopy("Data Source=" + D.GetString(DBHelper.GetDataTable("SELECT DB_SERVER FROM CM_SERVER_CONFIG WHERE SERVER_KEY = '" + Global.MainFrame.ServerKey + "'").Rows[0][0]) + ";Initial Catalog=NEOE;Persist Security Info=True;User ID=NEOE;Password=NEOE;", SqlBulkCopyOptions.TableLock);
		//    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy("Data Source=192.168.1.143;Initial Catalog=NEOE;Persist Security Info=True;User ID=NEOE;Password=NEOE;", SqlBulkCopyOptions.TableLock);
		//    sqlBulkCopy.DestinationTableName = "NEOE." + tablename;
		//    sqlBulkCopy.ColumnMappings.Add("CD_COMPANY", "NO_IMO");
		//    sqlBulkCopy.ColumnMappings.Add("NO_IMO", "CD_COMPANY");
		//    sqlBulkCopy.WriteToServer(table);
		//    return true;
		//}
	}
}
