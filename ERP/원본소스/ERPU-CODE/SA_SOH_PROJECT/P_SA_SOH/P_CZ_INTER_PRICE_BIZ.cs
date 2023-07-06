using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_INTER_PRICE_BIZ
    {
        
        internal DataTable SearchH(string  품목정보)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_INTER_PRICEH_S", new object[] { 품목정보, Global.MainFrame.LoginInfo.CompanyCode }, "");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        internal DataTable SearchL(string 헤더순번)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_INTER_PRICEL_S", new object[] { 헤더순번 }, "");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        internal DataTable SearchLDetail(string 헤더순번, string 생성일자from, string 생성일자to)
        {


            DataTable dt = DBHelper.GetDataTable("UP_CZ_INTER_PRICEL_DETAIL_S", new object[] { 헤더순번, 생성일자from, 생성일자to }, "INSERT_DATE ASC");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        internal void DeleteH(string 헤더순번)
        {
            //MessageBox.Show(차량번호);

            DBHelper.ExecuteNonQuery("UP_CZ_INTER_PRICEH_D", new object[] { 헤더순번 });
        }

        internal void DeleteL(string 헤더순번, string 디테일순번)
        {
            //MessageBox.Show(차량번호);

            DBHelper.ExecuteNonQuery("UP_CZ_INTER_PRICEL_D", new object[] { 헤더순번, 디테일순번 });
        }

        internal void Save(DataTable dtH, DataTable dtL)
        {
            if (dtH == null && dtL == null) return;

            SpInfoCollection sc = new SpInfoCollection();

            SpInfo siH = new SpInfo();
            SpInfo siL = new SpInfo();

            if (dtH != null)
            {

                siH.UserID = Global.MainFrame.LoginInfo.UserID;
                siH.DataValue = dtH;
                siH.SpNameInsert = "UP_CZ_INTER_PRICEH_I";
                siH.SpNameUpdate = "UP_CZ_INTER_PRICEH_U";
                siH.SpNameDelete = "UP_CZ_INTER_PRICEH_D";
                siH.SpParamsInsert = new string[] { "ITEM_CODE", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_IM",  "ID_INSERT", "CD_ITEM" };
                siH.SpParamsUpdate = new string[] { "ITEM_CODE", "PRICEH_SEQ", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_IM",  "ID_INSERT", "CD_ITEM" };
                siH.SpParamsDelete = new string[] { "PRICEH_SEQ"};

                sc.Add(siH);
            }

            if (dtL != null)
            {
 
                siL.UserID = Global.MainFrame.LoginInfo.UserID;
                siL.DataValue = dtL;
                siL.SpNameInsert = "UP_CZ_INTER_PRICEL_I";
                siL.SpNameUpdate = "UP_CZ_INTER_PRICEL_U";
                siL.SpNameDelete = "UP_CZ_INTER_PRICEL_D";
                siL.SpParamsInsert = new string[] { "PRICEH_SEQ", "CBOT", "FOB", "COST", "EXCHANGE_KIND", "EXCHANGE_RATE", "NOTE", "ID_INSERT", "INSERT_DATE", "USD", "JPY", "EUR", "WTI", "DUBAI" };
                siL.SpParamsUpdate = new string[] { "PRICEH_SEQ", "PRICEL_SEQ", "CBOT", "FOB", "COST", "EXCHANGE_KIND", "EXCHANGE_RATE", "NOTE", "ID_INSERT", "USD", "JPY", "EUR", "WTI", "DUBAI" };
                siL.SpParamsDelete = new string[] { "PRICEL_SEQ", "PRICEH_SEQ" };

                sc.Add(siL);
            }

            DBHelper.Save(sc);
        }

    }
}
