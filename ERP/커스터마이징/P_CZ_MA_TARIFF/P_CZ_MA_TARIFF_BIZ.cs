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
    internal class P_CZ_MA_TARIFF_BIZ
    {
        internal DataTable Search목공포장H(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_CBM_H_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search목공포장L(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_CBM_L_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDHL기본요금H(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_DHL_STD_H_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDHL기본요금L(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_DHL_STD_L_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDHL청구요금H(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_DHL_PAY_H_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDHL청구요금L1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_DHL_PAY_L_S1", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDHL청구요금L2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_TARIFF_DHL_PAY_L_S2", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt목공포장H, DataTable dt목공포장L, DataTable dtDHL기본요금H, DataTable dtDHL기본요금L, DataTable dtDHL청구요금H)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt목공포장H != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt목공포장H);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_TARIFF_CBM_H_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }
            
            if (dt목공포장L != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt목공포장L);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_TARIFF_CBM_L_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dtDHL기본요금H != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtDHL기본요금H);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_TARIFF_DHL_STD_H_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dtDHL기본요금L != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtDHL기본요금L);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_TARIFF_DHL_STD_L_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dtDHL청구요금H != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtDHL청구요금H);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_TARIFF_DHL_PAY_H_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
