using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using System.Windows.Forms;







namespace cz
{
    class P_CZ_CAR_MNG_CONFIRM_BILL_BIZ 
    {

        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_IVHL_S", obj);
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

                //string noDocu = (string)base.GetSeq(company, "FI", "01", Global.MainFrame.GetStringYearMonth);
                //string noIo = (string)base.GetSeq(company, "SA", "07", Global.MainFrame.GetStringYearMonth);


                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataValue = dt;
                si.SpNameUpdate = "UP_SA_IVMNG_TRANSFER_DOCU_DK";
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_IV", "OPT_CD_SVC", "OPT_ITEM_RPT_GUBUN", "OPT_ITEM_RPT_TEXT", "OPT_ITEM_NM_GUBUN", "OPT_SELL_DAM_GUBUN", "OPT_SELL_DAM_NM", "OPT_SELL_DAM_EMAIL", "OPT_SELL_DAM_MOBIL" };

                sc.Add(si);


                DBHelper.Save(sc);

            }

        }
    }
}