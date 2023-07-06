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
    internal class P_CZ_SA_FORWARD_CHARGE_MNG_BIZ
    {
        internal DataTable SearchHeader(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_FORWARD_CHARGE_H_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchLine(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_FORWARD_CHARGE_L_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtH);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_FORWARD_CHARGE_H_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dtL != null)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtL);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_FORWARD_CHARGE_L_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        public bool 회계전표처리(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_FORWARD_CHARGE_DOCU", obj);
        }

        public bool 회계전표처리택배(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_FORWARD_CHARGE_TEB_DOCU", obj);
        }

        public bool 회계전표처리통관비(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_FORWARD_CHARGE_TAX_DOCU", obj);
        }

        public bool 회계전표취소(string 전표유형, string 전표번호)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    전표유형,
                                                                                    전표번호,
                                                                                    Global.MainFrame.LoginInfo.UserID });
        }
    }
}
