using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU;
using System.IO;
using Dintec;
using System.Data;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_MA_HGS_ITEM_biz
    {
        internal bool Save(DataTable dt)
        {
           if (dt == null || dt.Rows.Count == 0)
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "SP_CZ_SA_HGS_ITEM_SELECT";
            spInfo.SpParamsInsert = new string[] { "NO_IMO",
                                                   "NO_ENGINE",
                                                   "CNT",
                                                   "NM_MODEL"
            };
            return DBHelper.Save(spInfo);
        }
    }
}
