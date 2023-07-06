using System;
using System.Collections;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    internal class P_CZ_PU_OPENAM_REG_BIZ
    {
        #region 조회
        public DataSet Search경리재고(object[] obj)
        {
            DataSet dataSet = DBMgr.GetDataSet("SP_CZ_PU_OPENAM_S", obj);
            T.SetDefaultValue(dataSet);

            dataSet.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dataSet.Tables[0].Columns["YY_AIS"].DefaultValue = Global.MainFrame.GetStringToday.Substring(0, 4);
            dataSet.Tables[0].Columns["YM_STANDARD"].DefaultValue = (Global.MainFrame.GetStringToday.Substring(0, 4) + "00");
            dataSet.Tables[0].Columns["YM_FSTANDARD"].DefaultValue = (Global.MainFrame.GetStringToday.Substring(0, 4) + "00");
            dataSet.Tables[0].Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dataSet.Tables[0].Columns["DT_INPUT"].DefaultValue = Global.MainFrame.GetStringToday;
            dataSet.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataSet.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;

            return dataSet;
        }

        public DataSet Search자재재고(object[] obj)
        {
            DataSet dataSet = DBMgr.GetDataSet("SP_CZ_PU_OPENQT_S", obj);
            T.SetDefaultValue(dataSet);

            dataSet.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dataSet.Tables[0].Columns["YY_AIS"].DefaultValue = Global.MainFrame.GetStringToday.Substring(0, 4);
            dataSet.Tables[0].Columns["YM_STANDARD"].DefaultValue = (Global.MainFrame.GetStringToday.Substring(0, 4) + "00");
            dataSet.Tables[0].Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dataSet.Tables[0].Columns["DT_INPUT"].DefaultValue = Global.MainFrame.GetStringToday;
            dataSet.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataSet.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;

            return dataSet;
        }

        public DataTable SearchLOT(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_PU_OPENQTLOT_S", obj);
            T.SetDefaultValue(dataTable);

            return dataTable;
        }
        #endregion

        #region 삭제
        public void Delete경리재고(object[] obj)
        {
            DBHelper.ExecuteScalar("UP_PU_OPENAMH_DELETE", obj);
        }

        public void Delete자재재고(object[] obj)
        {
            DBHelper.ExecuteScalar("UP_PU_OPENQTH_DELETE", obj);
        }
        #endregion

        #region 저장
        public bool Save경리재고(DataTable dt경리재고H, DataTable dt경리재고L)
        {
            #region 경리재고
            string xmlH = Util.GetTO_Xml(dt경리재고H);
            string xmlL = Util.GetTO_Xml(dt경리재고L);

            return (DBMgr.ExecuteNonQuery("SP_CZ_PU_OPENAM_XML", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                xmlH,
                                                                                xmlL,
                                                                                Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
            #endregion
        }


        public bool Save자재재고(DataTable dt자재재고H, DataTable dt자재재고L, DataTable dtLOT)
        {

            bool result = true;

            #region 자재재고
            string xmlH = Util.GetTO_Xml(dt자재재고H);
            string xmlL = Util.GetTO_Xml(dt자재재고L);

            result = (DBMgr.ExecuteNonQuery("SP_CZ_PU_OPENQT_XML", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  xmlH,
                                                                                  xmlL,
                                                                                  Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
            #endregion

            #region LOT
            if (dtLOT != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtLOT;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpNameInsert = "UP_PU_OPENQTLOT_INSERT";
                spInfo.SpNameUpdate = "UP_PU_OPENQTLOT_UPDATE";
                spInfo.SpNameDelete = "UP_PU_OPENQTLOT_DELETE";
                spInfo.SpParamsInsert = new string[] { "CD_ITEM",
                                                       "YM_STANDARD",
                                                       "NO_LOT",
                                                       "CD_SL",
                                                       "CD_PLANT",
                                                       "CD_COMPANY",
                                                       "QT_GOOD_INV",
                                                       "QT_REJECT_INV",
                                                       "QT_INSP_INV",
                                                       "NO_IO",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20",
                                                       "DT_LIMIT",
                                                       "DC_RMK" };
                spInfo.SpParamsUpdate = new string[] { "CD_ITEM",
                                                       "YM_STANDARD",
                                                       "NO_LOT",
                                                       "CD_SL",
                                                       "CD_PLANT",
                                                       "CD_COMPANY",
                                                       "QT_GOOD_INV",
                                                       "QT_REJECT_INV",
                                                       "QT_INSP_INV",
                                                       "NO_IO",
                                                       "QT_GOOD_INV_OLD",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20",
                                                       "DT_LIMIT",
                                                       "DC_RMK" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_LOT",
                                                       "CD_SL",
                                                       "YM_STANDARD",
                                                       "CD_ITEM",
                                                       "NO_IO" };
                result = DBHelper.Save(spInfo);
            }
            #endregion

            return result;
        }
        #endregion

        #region 공통
        internal DataTable 엑셀업로드아이템조회(string CD_PLANT, string NO_KEY)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));

            for (int index = 0; index < ((ArrayList)arrayListExt).Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = ((ArrayList)arrayListExt)[index].ToString();
                dataTable1.Rows.Add(row);
            }

            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable1.Rows)
            {
                DataTable dataTable4 = this.품목조회(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    CD_PLANT, 
                                                                    dataRow["멀티KEY"].ToString() });
                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();

                foreach (DataRow row in (InternalDataCollectionBase)dataTable4.Rows)
                    dataTable3.ImportRow(row);
            }

            return dataTable3;
        }

        private ArrayListExt KEY배열(string 멀티KEY)
        {
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티KEY.Split('|');
            int num1 = 200;
            int num2 = 1;
            string str = string.Empty;
            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index] + "|";
                ++num2;
                if (num2 == num1 || index == strArray.Length - 2)
                {
                    ((ArrayList)arrayListExt).Add(str);
                    str = string.Empty;
                    num2 = 1;
                }
            }
            if (str != string.Empty)
                ((ArrayList)arrayListExt).Add(str);
            return arrayListExt;
        }

        private DataTable 품목조회(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MM_PITEM_S", obj);
        }

        public DataTable 품목전개(object[] obj)
        {
            return DBMgr.GetDataTable("SP_CZ_PU_OPEN_ITEM_S", obj);
        }
        #endregion

        #region 경리재고
        public string 경리재고확인(object[] obj)
        {
            object[] outs = null;

            DBHelper.GetDataTable("SP_CZ_PU_OPENAM_C", obj, out outs);

            return outs[0].ToString();
        }

        public DataTable 경리재고기초자재적용(object[] obj)
        {
            return DBMgr.GetDataTable("SP_CZ_PU_OPENAM_QT_S", obj);
        }
        #endregion

        #region 자재재고
        public string 자재재고확인(object[] obj)
        {
            object[] outs = null;

            DBHelper.GetDataTable("SP_CZ_PU_OPENQT_C", obj, out outs);

            return outs[0].ToString();
        }
        #endregion
    }
}
