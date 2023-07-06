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
    class P_CZ_SA_CRM_PARTNER_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search거래내역1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_HISTORY1_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search거래내역2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_HISTORY2_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search미수금(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_OUTSTANDING_INV_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search클레임(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_CLAIM_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search영업활동(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_ACTIVITY_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search미팅메모(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_MEETING_MEMO_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search커미션(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_COMMISSION_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search메모(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_MEMO_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search호선(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_VESSEL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search엔진(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_ENGINE_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search담당자(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_PIC_S1", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search재고판매H(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_STOCKH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search재고판매L(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_STOCKL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search재고판매D(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_STOCKD_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search판매품목H(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_SUPPLIERH_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search판매품목L(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_SUPPLIERL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search판매품목D(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_SUPPLIERD_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search업무인계(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_HIST_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search호선_매입처(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_VESSEL_SUPPLIER_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt활동, DataTable dt메모)
        {
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si;

            if (dt활동 != null && dt활동.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt활동);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_ACTIVITY_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            if (dt메모 != null && dt메모.Rows.Count != 0)
            {
                si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt메모);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_MEMO_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
