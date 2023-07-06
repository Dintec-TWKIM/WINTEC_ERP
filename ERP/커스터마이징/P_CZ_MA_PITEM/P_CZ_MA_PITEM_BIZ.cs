using System;
using System.Data;
using System.Text;
using System.Web;
using System.Linq;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System.Threading;

namespace cz
{
    internal class P_CZ_MA_PITEM_BIZ
    {
        internal DataTable Search(object[] args)
        {
            DataTable dataTable = SQL.GetDataTable("SP_CZ_MA_PITEM_S", SQLDebug.Print, args);
            T.SetDefaultValue(dataTable);

            dataTable.Columns["TP_PROC"].DefaultValue = "P";
            dataTable.Columns["TP_ITEM"].DefaultValue = "SIN";
            dataTable.Columns["TP_PART"].DefaultValue = "P";
            dataTable.Columns["DT_VALID"].DefaultValue = "29991231";
            dataTable.Columns["YN_USE"].DefaultValue = "Y";
            dataTable.Columns["FG_ABC"].DefaultValue = "C";
            dataTable.Columns["YN_PHANTOM"].DefaultValue = "N";
            dataTable.Columns["FG_LONG"].DefaultValue = "N";
            dataTable.Columns["CLS_PO"].DefaultValue = "L4L";
            dataTable.Columns["LOTSIZE"].DefaultValue = 1;
            dataTable.Columns["FG_GIR"].DefaultValue = "Y";

            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo spInfo = null;
            DataSet ds = new DataSet();
            DataRow[] dataRowArray;
            int groupUnit = 1000, index = 0;

            try
            {
				DataTable dtUcode = this.Ucode중복확인(dt);

				if (dtUcode != null && dtUcode.Rows.Count > 0)
				{
					Global.MainFrame.ShowMessage(string.Format("중복 U코드를 등록 할 수 없습니다. [{0}]", dtUcode.Rows[0]["UCODE"].ToString()));
					return false;
				}

				dataRowArray = dt.AsEnumerable().ToArray();
                
                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    spInfo = new SpInfo()
                    {
                        DataValue = Util.GetXmlTable(dt1),
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        UserID = Global.MainFrame.LoginInfo.UserID,

                        SpNameInsert = "SP_CZ_MA_PITEM_XML",
                        SpParamsInsert = new string[] { "XML", "ID_INSERT" }
                    };

                    DBHelper.Save(spInfo);

                    if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                        P_CZ_DX_PITEM_REG.품목동기화(dt1);
                }

                return true;
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

        internal bool SaveExcel(DataTable dtExcel)
        {
            DataSet ds = new DataSet();
            DataRow[] dataRowArray;
            string xml = string.Empty;
            int groupUnit = 50, index = 0;

            try
            {
                DataTable dtUcode = this.Ucode중복확인(dtExcel);

                if (dtUcode != null && dtUcode.Rows.Count > 0)
                {
                    Global.MainFrame.ShowMessage(string.Format("중복 U코드를 등록 할 수 없습니다. [{0}]", dtUcode.Rows[0]["UCODE"].ToString()));
                    return false;
                }

                dataRowArray = dtExcel.AsEnumerable().ToArray();
                
                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    DBHelper.ExecuteNonQuery("SP_CZ_MA_PITEM_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID });

                    if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                        P_CZ_DX_PITEM_REG.품목동기화(dt1);

                    Thread.Sleep(5000);
                }

                return true;
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

        internal int GetDigitPitem()
        {
            int num = D.GetInt(DBHelper.ExecuteScalar(@"SELECT DIGIT_PITEM 
                                                        FROM MA_ENV WITH(NOLOCK)  
                                                        WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'"));
            if (num == 0) num = 20;
            
            return num;
        }

        internal DataTable Ucode중복확인(DataTable dt)
        {
            string xml = string.Empty;

            try
            {
                xml = Util.GetTO_Xml(dt);

                return DBHelper.GetDataTable("SP_CZ_MA_PITEM_UCODE_CHECK", new object[] { Global.MainFrame.LoginInfo.CompanyCode, xml });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return null;
        }
    }
}