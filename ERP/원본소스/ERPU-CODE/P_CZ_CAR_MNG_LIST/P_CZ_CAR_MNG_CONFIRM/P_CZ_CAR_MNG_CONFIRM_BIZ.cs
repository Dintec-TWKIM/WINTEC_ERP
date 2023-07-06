using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_CAR_MNG_CONFIRM_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_MGMT_CONFIRM_S", obj);
            T.SetDefaultValue(dt);  // 디폴트값 지정
            return dt;
        }



        internal void Save(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count != 0))
            {

                SpInfo si = new SpInfo();
                SpInfoCollection sc = new SpInfoCollection();

                //string company = base.LoginInfo.CompanyCode;

                //string noSo = (string)base.GetSeq(company, "SA", "02", Global.MainFrame.GetStringYearMonth);
                //string noIo = (string)base.GetSeq(company, "SA", "07", Global.MainFrame.GetStringYearMonth);


                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataValue = dt;
                si.SpNameUpdate = "UP_CZ_GIR_BATCH_INSERT_H";
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_GIR", "NO_SO", "NO_IO", "DEPTCODE", "TP_BUSI", "DT_GI", "DC_RMK", "DC_RMK2", "DC_RMK3", "NM_EMP", "CURRENT_COMPANY", "NO_IOLINE", "CD_SL", "SEQ_SO" };

                sc.Add(si);


                DBHelper.Save(sc);

            }

        }
    }
}
