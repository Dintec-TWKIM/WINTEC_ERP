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
    internal class P_CZ_SA_COMMISSION_MNG_BIZ
    {
        internal DataTable SearchHeader(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNGH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchLine(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNGL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNGD_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNGD1_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNGD2_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt커미션, DataTable dt거래처담당자, DataTable dt대상내역)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt커미션 != null && dt커미션.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt커미션);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_COMMISSION_MNGH_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt거래처담당자 != null && dt거래처담당자.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt거래처담당자);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_COMMISSION_MNGL_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt대상내역 != null && dt대상내역.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt대상내역);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_COMMISSION_MNGD_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
