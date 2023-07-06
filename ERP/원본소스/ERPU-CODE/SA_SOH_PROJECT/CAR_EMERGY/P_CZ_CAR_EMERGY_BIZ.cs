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
    class P_CZ_CAR_EMERGY_BIZ
    {

        internal DataTable Search(string 차량명, string 성명)
        {

            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_EMEGRNCY_S", new object[] { 차량명, 성명, Global.MainFrame.LoginInfo.CompanyCode }, "INSERT_DATE DESC");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.
 
        }

        internal void Delete(string 차량명, string 성명)
        {
            //MessageBox.Show(차량번호);

            DBHelper.ExecuteNonQuery("UP_CZ_CAR_EMEGRNCY_D", new object[] {차량명, 성명 });
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


        internal void Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            SpInfo si = new SpInfo();
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.DataValue = dt;
            si.SpNameInsert = "UP_CZ_CAR_EMEGRNCY_I";
            si.SpNameUpdate = "UP_CZ_CAR_EMEGRNCY_U";
            si.SpNameDelete = "UP_CZ_CAR_EMEGRNCY_D";
            si.SpParamsInsert = new string[] { "CAR_NO", "NM_CAR", "NO_EMP", "HP", "PWHP", "TEL", "NOTE", "ID_INSERT", "CD_PARTNER", "CD_COMPANY" };
            si.SpParamsUpdate = new string[] { "CAR_NO", "NM_CAR", "NO_EMP", "HP", "PWHP", "TEL", "NOTE", "ID_UPDATE", "CD_PARTNER", "CD_COMPANY" };
            si.SpParamsDelete = new string[] { "CAR_NO", "NO_EMP" };
            DBHelper.Save(si);
        }

    }
}
