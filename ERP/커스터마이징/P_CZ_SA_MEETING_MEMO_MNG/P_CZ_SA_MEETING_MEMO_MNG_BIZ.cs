using System.Data;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_MEETING_MEMO_MNG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_MEETING_MEMO_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataSet SearchDetail(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_MEETING_ATTENDEE_S", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal bool Save(DataTable dt미팅메모, DataTable dt참석자외부, DataTable dt참석자내부)
        {
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si;

            if (dt미팅메모 != null && dt미팅메모.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt미팅메모);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_MEETING_MEMO_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt참석자외부 != null && dt참석자외부.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt참석자외부);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_MEETING_ATTENDEE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt참석자내부 != null && dt참석자내부.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt참석자내부);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_MEETING_ATTENDEE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
