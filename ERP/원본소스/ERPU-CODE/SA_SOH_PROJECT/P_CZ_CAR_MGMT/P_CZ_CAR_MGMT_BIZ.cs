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
    class P_CZ_CAR_MGMT_BIZ
    {

        internal DataTable Search(string cdPartner, string carNo, string empNo)
        {

            //MessageBox.Show(cdPartner);
            //MessageBox.Show(dtsInsertFrom);
            //MessageBox.Show(dtsInsertTo);
            //MessageBox.Show(divisonReq);
            //MessageBox.Show(cdPlan);
            //MessageBox.Show(carNo);
            //MessageBox.Show(empNo);

            DataTable dt = DBHelper.GetDataTable("UP_CZ_EQUIP_S", new object[] { cdPartner, carNo, empNo, Global.MainFrame.LoginInfo.CompanyCode }, "DTS_INSERT DESC");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        internal DataTable SearchL(string cdCompany, string cdPartner, string carNo, string cdplan, string dtsInsertFrom, string dtsInsertTo, string divisonReq)
        {

            //MessageBox.Show(cdCompany);
            //MessageBox.Show(cdPartner);
            //MessageBox.Show(carNo);
            //MessageBox.Show(cdplan);
            //MessageBox.Show(cdplan);
            //MessageBox.Show(carNo);
            //MessageBox.Show(empNo);

            DataTable dt = DBHelper.GetDataTable("UP_CZ_EQUIP_DETAIL_S", new object[] { cdCompany, cdPartner, carNo, cdplan, dtsInsertFrom, dtsInsertTo, divisonReq }, "");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }

        internal void Delete(string 회사, string 화주, string 차량명)
        {
            //MessageBox.Show(차량번호);

            DBHelper.ExecuteNonQuery("UP_CZ_EQUIP_D", new object[] { 회사, 화주,  차량명 });
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

        internal void Save(DataTable dt, DataTable dtL)
        {
            if (dt == null && dtL == null) return;

            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {


                SpInfo si = new SpInfo();
                //si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
               
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataValue = dt;
                si.SpNameInsert = "UP_CZ_EQUIP_I";
                si.SpNameUpdate = "UP_CZ_EQUIP_U";
                si.SpNameDelete = "UP_CZ_EQUIP_D";
                si.SpParamsInsert = new string[] { "CAR_NO", "NM_CAR", "NO_EMP", "CD_PARTNER", "CD_COMPANY", "CD_PLANT", "NO_HP", "NO_TEL", "PWHP", "MATERIAL", "FG_STATUS", "PWD", "LOADAGE", "DC_RMK", "KIND_CAR", "DC_NO_BP", "NO_BIZAREA", "ID_INSERT", "COPER_CODE", "TP_COMPANY", "TP_GOODS", "UNIT_GOODS", "CONTRANCT_FROM", "CONTRANCT_TO", "TP_CAR", "BUSI_COND", "UNIT_CARGO", "ID" };
                si.SpParamsUpdate = new string[] { "CAR_NO", "NM_CAR", "NO_EMP", "CD_PARTNER", "CD_COMPANY", "CD_PLANT", "NO_HP", "NO_TEL", "PWHP", "MATERIAL", "FG_STATUS", "PWD", "LOADAGE", "DC_RMK", "KIND_CAR", "DC_NO_BP", "NO_BIZAREA", "ID_UPDATE", "COPER_CODE", "TP_COMPANY", "TP_GOODS", "UNIT_GOODS", "CONTRANCT_FROM", "CONTRANCT_TO", "TP_CAR", "BUSI_COND", "UNIT_CARGO", "ID" };
                si.SpParamsDelete = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO" };


                sc.Add(si);
            }

            if (dtL != null)
            {

                SpInfo siL = new SpInfo();

                //siL.UserID = Global.MainFrame.LoginInfo.UserID;
                siL.DataValue = dtL;
                siL.SpNameInsert = "UP_CZ_EQUIP_DETAIL_I";
                siL.SpNameUpdate = "UP_CZ_EQUIP_DETAIL_U";
                siL.SpNameDelete = "UP_CZ_EQUIP_DETAIL_D";
                siL.SpParamsInsert = new string[] { "NM_PLAN", "NM_CAR", "ID_INSERT", "DT_PRC_FINAL", "QT_DAYS", "DC_RMK2", "CD_COMPANY", "CD_PARTNER", "CAR_NO", "CD_PLAN", "COST", "DIVISION_REQ" };
                siL.SpParamsUpdate = new string[] { "NM_PLAN", "NM_CAR", "ID_UPDATE", "DT_PRC_FINAL", "QT_DAYS", "DC_RMK2", "CD_COMPANY", "CD_PARTNER", "CAR_NO", "CD_PLAN", "COST", "DIVISION_REQ" };
                siL.SpParamsDelete = new string[] { "CD_COMPANY", "CD_PARTNER", "CAR_NO", "CD_PLAN" };

                sc.Add(siL);
            }

            DBHelper.Save(sc);
        }

    }
}
