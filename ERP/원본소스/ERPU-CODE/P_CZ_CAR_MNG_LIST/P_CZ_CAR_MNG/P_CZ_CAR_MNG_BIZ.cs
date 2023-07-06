using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_PARTNER_INTRT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_MGMT_S", obj);
            T.SetDefaultValue(dt);  // 디폴트값 지정
            return dt;



        }

        public bool Save(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.SpNameInsert = "UP_CZ_CAR_MGMT_I";
                si.SpParamsInsert = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };
                si.SpNameUpdate = "UP_CZ_CAR_MGMT_U";
                si.SpParamsUpdate = new string[] { "NO_GIR", "LINE", "CAR_SEQ", "CONFIRM_QTY", "FLAG" };
                si.SpNameDelete = "UP_CZ_CAR_MGMT_D";
                si.SpParamsDelete = new string[] { "NO_GIR", "LINE", "CAR_SEQ" };
                //DBHelper.Save(si);

                ResultData result = (ResultData)Global.MainFrame.Save(si);
                return result.Result;

            }

            return false;



        }





    }
}