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
    class P_CZ_CAR_MNG_CONFIRM_Y_BIZ 
    {

        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_MGMT_CONFIRM_Y", obj);
            T.SetDefaultValue(dt);  // 디폴트값 지정
            return dt;
        }



        internal void Save(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count != 0))
            {

                SpInfo si = new SpInfo();
                SpInfoCollection sc = new SpInfoCollection();


                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataValue = dt;
                si.SpNameUpdate = "UP_CZ_IVHL";
                si.SpNameDelete = "UP_CZ_IVHL_D";

                si.SpParamsUpdate = new string[] { "NO_IV", "NO_IOLINE", "CD_COMPANY", "CD_BIZAREA", "NO_BIZAREA", "CD_PARTNER", "DT_PROCESS", "CD_DEPT", "NO_EMP", "ID_INSERT", "DC_RMK", "CD_DOCU", "CD_PLANT", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_CC", "CONFIRM_QTY", "REQ_PRICE" };
                si.SpParamsDelete = new string[] { "NO_IV", "CD_COMPANY" };

                sc.Add(si);


                DBHelper.Save(sc);

            }

        }


    }
}