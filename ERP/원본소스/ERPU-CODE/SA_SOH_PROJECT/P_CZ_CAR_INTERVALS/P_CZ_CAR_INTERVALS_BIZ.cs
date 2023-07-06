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
    class P_CZ_CAR_INTERVALS_BIZ

    {
        internal DataTable Search(string GUBUN, string CD_ITEM, string DT_GIR_INSERT_FROM, string DT_GIR_INSERT_TO, string DT_DUEDATE_INSERT_FROM, string DT_DUEDATE_INSERT_TO, string CD_PARTNER, string NO_GIR, string TABFLAG, string CD_COMPANY)
        {

            SpInfo si = new SpInfo();
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_H_S", new object[] { GUBUN, CD_ITEM, DT_GIR_INSERT_FROM, DT_GIR_INSERT_TO, DT_DUEDATE_INSERT_FROM, DT_DUEDATE_INSERT_TO, CD_PARTNER, NO_GIR, TABFLAG, CD_COMPANY }, "DTS_INSERT DESC");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        internal DataTable SearchL(string NOGIR, string FLAG, string GUBUN, string LINE)
        {

            //MessageBox.Show(NOGIR);
            //MessageBox.Show(LINE);
            //MessageBox.Show(carNo);
            //MessageBox.Show(cdplan);
            //MessageBox.Show(cdplan);
            //MessageBox.Show(carNo);
            //MessageBox.Show(empNo);

            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_L_S", new object[] { NOGIR, FLAG, GUBUN, LINE }, "");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        /*
         * Parameter가 많은 경우
         * * BIZ 코딩
               internal DataTable Search(object[] args)
               {
                  DataTable dt = DBHelper.GetDataTable("프로시져명", args, “정렬할컬럼명 ASC”);

                  T.SetDefaultValue(dt);

                  // 특정 컬럼에 대한 Default값 설정
                  dt.Columns[“CD_DEPT”].DefautValue = Global.MainFrame.LoginInfo.DeptCode;
                  return dt;
               }

         */

        internal void DeleteD(string 의뢰번호, string 항차)
        {
            //MessageBox.Show(차량번호);

            DBHelper.ExecuteNonQuery("UP_CZ_CAR_H_D", new object[] { 의뢰번호, 항차 });
        }

        internal void Delete(string 의뢰번호, string 항차, string 시퀀스)
        {
            //MessageBox.Show(차량번호);

            DBHelper.ExecuteNonQuery("UP_CZ_CAR_L_D", new object[] { 의뢰번호, 항차, 시퀀스 });
        }


        internal void Save(DataTable dt, DataTable dtL)
        {
  
            if (dt == null && dtL == null) return;

            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {


                SpInfo si = new SpInfo();
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataValue = dt;
                si.SpNameInsert = "UP_CZ_CAR_H_I";
                si.SpNameUpdate = "UP_CZ_CAR_H_U";
                si.SpNameDelete = "UP_CZ_CAR_H_D";
                si.SpParamsInsert = new string[] { "NO_GIR", "LINE", "CD_COMPANY", "NM_COMPANY", "CD_PARTNER", "LN_PARTNER", "CD_ITEM", "NM_ITEM", "DT_GIR", "TOTAL_QT_GIR", "UM", "AM_EX", "FLAG", "DC_RMK", "DC_RMK2", "ID_INSERT", "DT_DUEDATE", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE"};
                si.SpParamsUpdate = new string[] { "NO_GIR", "LINE", "CD_COMPANY", "NM_COMPANY", "CD_PARTNER", "LN_PARTNER", "CD_ITEM", "NM_ITEM", "DT_GIR", "TOTAL_QT_GIR", "UM", "AM_EX", "DC_RMK", "DC_RMK2", "ID_INSERT", "DT_DUEDATE", "REAL_LOADING_PLACE", "LOADING_PLACE", "ALIGHT_PLACE" };
                si.SpParamsDelete = new string[] { "NO_GIR", "LINE" };

                sc.Add(si);
            }


            if (dtL != null)
            {
                //MessageBox.Show("13");
                SpInfo siL = new SpInfo();
                siL.UserID = Global.MainFrame.LoginInfo.UserID;
                siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL.DataValue = dtL;
                siL.SpNameInsert = "UP_CZ_CAR_L_I";
                siL.SpNameUpdate = "UP_CZ_CAR_L_U";
                siL.SpNameDelete = "UP_CZ_CAR_L_D";
                siL.SpParamsInsert = new string[] { "NO_GIR", "LINE", "CAR_NO", "SHIPPER", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "CD_COMPANY", "REAL_LOADING_PLACE", "LIFT_LOADING_DATE", "LOADING_DATE", "ALIGHT_DATE", "SASH_NO", "DC_RMK", "DRIVER_DC_RMK", "ID_INSERT", "FLAG", "QT_GIR", "LOADING_QTY", "ALIGHT_QTY", "CONFIRM_QTY", "STANDARD_PRICE", "ORDER_TIME", "NM_CAR", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "NM_REAL_LOADING_PLACE", "NO_EMP","REQ_UNIT_PRICE" };
                siL.SpParamsUpdate = new string[] { "NO_GIR", "LINE", "CAR_SEQ", "CAR_NO", "SHIPPER", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "CD_COMPANY", "REAL_LOADING_PLACE", "LIFT_LOADING_DATE", "LOADING_DATE", "ALIGHT_DATE", "SASH_NO", "DC_RMK", "DRIVER_DC_RMK", "ID_INSERT", "UPDATE_DATE", "FLAG", "QT_GIR", "LOADING_QTY", "ALIGHT_QTY", "CONFIRM_QTY", "STANDARD_PRICE", "ORDER_TIME", "NM_CAR", "NO_EMP","REQ_UNIT_PRICE" };
                siL.SpParamsDelete = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };

                sc.Add(siL);

            }

            DBHelper.Save(sc);

        }

        internal void SaveD(DataTable dtL)
        {
           
            if (dtL == null) return;

            SpInfoCollection sc = new SpInfoCollection();

            if (dtL != null)
            {
                
                SpInfo siL = new SpInfo();
                siL.UserID = Global.MainFrame.LoginInfo.UserID;
                siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL.DataValue = dtL;
                siL.SpNameInsert = "UP_CZ_CAR_L_I";
                siL.SpNameUpdate = "UP_CZ_CAR_L_U";
                siL.SpNameDelete = "UP_CZ_CAR_L_D";
                siL.SpParamsInsert = new string[] { "NO_GIR", "LINE", "CAR_NO", "SHIPPER", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "CD_COMPANY", "REAL_LOADING_PLACE", "LIFT_LOADING_DATE", "LOADING_DATE", "ALIGHT_DATE", "SASH_NO", "DC_RMK", "DRIVER_DC_RMK", "ID_INSERT", "FLAG", "QT_GIR", "LOADING_QTY", "ALIGHT_QTY", "CONFIRM_QTY", "STANDARD_PRICE", "ORDER_TIME", "NM_CAR", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "NM_REAL_LOADING_PLACE", "NO_EMP","REQ_UNIT_PRICE" };
                siL.SpParamsUpdate = new string[] { "NO_GIR", "LINE", "CAR_SEQ", "CAR_NO", "SHIPPER", "LOADING_PLACE", "ALIGHT_PLACE", "REQ_PRICE", "PROVIDE_PRICE", "UNIT_PRICE", "ESTIMATE_PRICE", "FREIGHT_CHARGE_A", "FREIGHT_CHARGE_B", "STANDARD_PLACE", "CD_COMPANY", "REAL_LOADING_PLACE", "LIFT_LOADING_DATE", "LOADING_DATE", "ALIGHT_DATE", "SASH_NO", "DC_RMK", "DRIVER_DC_RMK", "ID_UPDATE", "UPDATE_DATE", "FLAG", "QT_GIR", "LOADING_QTY", "ALIGHT_QTY", "CONFIRM_QTY", "STANDARD_PRICE", "ORDER_TIME", "NM_CAR", "NM_LOADING_PLACE", "NM_ALIGHT_PLACE", "NM_REAL_LOADING_PLACE", "NO_EMP","REQ_UNIT_PRICE" };
                siL.SpParamsDelete = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };

                sc.Add(siL);

            }

            DBHelper.Save(sc);

        }
    }
}
