using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_SA_BUSINESS_TRIP_MNG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_BUSINESS_TRIP_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataSet SearchDetail(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_BUSINESS_TRIP_DETAIL_S", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal bool Save(DataTable dt출장보고서, DataTable dt출장일정, DataTable dt출장자, DataTable dt출장경비, DataTable dt미팅메모)
        {
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si;

            if (dt출장보고서 != null && dt출장보고서.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt출장보고서);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_BUSINESS_TRIP_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt출장일정 != null && dt출장일정.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt출장일정);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_BUSINESS_TRIP_SCHEDULE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt출장자 != null && dt출장자.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt출장자);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_BUSINESS_TRIP_EMP_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt출장경비 != null && dt출장경비.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt출장경비);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_BUSINESS_TRIP_EXPENSE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt미팅메모 != null && dt미팅메모.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt미팅메모);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_BUSINESS_TRIP_MEETING_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
