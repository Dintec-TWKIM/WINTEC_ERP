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
    internal class P_CZ_MA_PARTNER_PRICE_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MA_PARTNER_PRICEH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBMgr.GetDataTable("SP_CZ_MA_PARTNER_PRICEL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(bool isDelete, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return false;
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si;
            DataTable tempDt;

            if (isDelete)
            {
                DataSet ds = new DataSet();
                DataTable tempDt1;

                tempDt1 = dt.Copy();
                tempDt1.TableName = "D";

                ds.Tables.Add(tempDt1);

                tempDt = new DataTable();
                tempDt.Columns.Add("XML");
                tempDt.Rows.Add(Util.GetTO_Xml(ds));
            }
            else
            {
                tempDt = Util.GetXmlTable(dt);
            }

            si = new SpInfo()
            {
                DataValue = tempDt,
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.UserID,

                SpNameInsert = "SP_CZ_MA_PARTNER_PRICE_XML",
                SpParamsInsert = new string[] { "XML", "ID_INSERT" }
            };
            sc.Add(si);

            tempDt = new DataTable();
            tempDt.Columns.Add("XML");
            tempDt.Rows.Add(GetTo.Xml(dt));

            si = new SpInfo()
            {
                DataValue = tempDt,
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                UserID = Global.MainFrame.LoginInfo.UserID,

                SpNameInsert = "PX_CZ_MA_PITEM_UM_EXT",
                SpParamsInsert = new string[] { "XML", "CD_COMPANY" }
            };
            sc.Add(si);

            return DBHelper.Save(sc);
        }

        internal bool SaveExcel(DataTable dtExcel)
        {
            DataSet ds = new DataSet();
            DataRow[] dataRowArray;
            string xml = string.Empty;
            int groupUnit = 1000, index = 0;

            try
            {
                dataRowArray = dtExcel.AsEnumerable().ToArray();

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    DBHelper.ExecuteNonQuery("SP_CZ_MA_PARTNER_PRICE_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID });
                }

                DataTable tempDt = new DataTable();
                tempDt.Columns.Add("XML");
                tempDt.Rows.Add(GetTo.Xml(dtExcel));

                SpInfo si = new SpInfo()
                {
                    DataValue = tempDt,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,

                    SpNameInsert = "PX_CZ_MA_PITEM_UM_EXT",
                    SpParamsInsert = new string[] { "XML", "CD_COMPANY" }
                };

                return DBHelper.Save(si);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return false;
        }
    }
}
