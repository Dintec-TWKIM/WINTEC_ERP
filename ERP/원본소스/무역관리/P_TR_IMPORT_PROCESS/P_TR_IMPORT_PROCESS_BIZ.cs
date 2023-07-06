using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using System.Data;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace trade
{
    class P_TR_IMPORT_PROCESS_BIZ
    {
        #region
        public DataTable Search(object[] obj)
        {

            DataSet ds = DBHelper.GetDataSet("UP_TR_IMPORT_PROCESS_SELECT", obj);

            return ds.Tables[0];
        }
        #endregion

    }
}
