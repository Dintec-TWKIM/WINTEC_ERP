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
    class P_CZ_CAR_PLACE_BIZ
    {
        internal DataTable Search(string PLACE_CODE_NAME, string CD_PLANT)
        {

            SpInfo si = new SpInfo();
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_PLACE_S", new object[] { PLACE_CODE_NAME, CD_PLANT, "CD_COMPANY" }, "INSERT_DATE DESC");
            return dt;

            //DBHelper.GetDataSet() : 하나 이상의 테이블을 포함된 DataSet형태를 return 한다.

        }


        internal void Delete(string PLACE_CODE, string PLACE_CODE_NAME, string CD_COMPANY)
        {

            DBHelper.ExecuteNonQuery("UP_CZ_CAR_PLACE_D", new object[] { PLACE_CODE, PLACE_CODE_NAME, CD_COMPANY });
        }


        internal void Save(DataTable dt)
        {
            if (dt == null) return;

            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {


                SpInfo si = new SpInfo();
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataValue = dt;
                si.SpNameInsert = "UP_CZ_CAR_PLACE_I";
                si.SpNameUpdate = "UP_CZ_CAR_PLACE_U";
                si.SpNameDelete = "UP_CZ_CAR_PLACE_D";
                si.SpParamsInsert = new string[] { "PLACE_NAME", "CD_PLANT", "CD_COMPANY", "ID_INSERT"};
                si.SpParamsUpdate = new string[] { "PLACE_CODE", "PLACE_NAME", "CD_PLANT", "CD_COMPANY", "ID_INSERT" };
                si.SpParamsDelete = new string[] { "PLACE_CODE", "PLACE_NAME" };

                sc.Add(si);
            }



            DBHelper.Save(sc);

        }
    }
}
