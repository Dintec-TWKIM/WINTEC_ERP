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
    class P_CZ_ROUT_UM_BIZ
    {
        public DataTable Search(object[] args)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_PRICE_S", args, "INSERT_DATE desc");
            T.SetDefaultValue(dt);  // 디폴트값 지정
            return dt;
        }

        internal bool Save(DataTable dt)
        {
            //DataTable dt =  _flex.GetChanges();

            if (dt == null) return true;
            if (dt.Rows.Count == 0) return true;


            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = MA.Login.사원번호; //LoginInfo.EmployeeNo;
            si.SpNameInsert = "UP_CZ_CAR_PRICE_I";
            si.SpNameUpdate = "UP_CZ_CAR_PRICE_U";
            si.SpNameDelete = "UP_CZ_CAR_PRICE_D";
            //si.SpParamsInsert = new string[] { "CAR_NO", "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "ID_INSERT", "CD_COMPANY" };
            //si.SpParamsUpdate = new string[] { "CAR_NO", "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "ID_UPDATE", "CD_COMPANY" };
            //si.SpParamsDelete = new string[] { "CAR_NO", "SHIPPER", "LOADING_PLACE", "ALIGHT_PLACE", "CD_COMPANY" };

            si.SpParamsInsert = new string[] { "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "ID_INSERT", "CD_COMPANY", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "NM_REAL_LOADING_PLACE","REQ_UNIT_PRICE" };
            si.SpParamsUpdate = new string[] { "SHIPPER", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "ID_UPDATE", "CD_COMPANY", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "NM_REAL_LOADING_PLACE", "REQ_UNIT_PRICE" };
            si.SpParamsDelete = new string[] { "SHIPPER", "LOADING_PLACE", "ALIGHT_PLACE", "CD_COMPANY" };


            return DBHelper.Save(si);
        }
    }
}