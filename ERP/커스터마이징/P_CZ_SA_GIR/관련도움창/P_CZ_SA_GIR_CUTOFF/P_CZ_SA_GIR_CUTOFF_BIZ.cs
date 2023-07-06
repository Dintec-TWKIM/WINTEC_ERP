using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.IO;
using Dintec;

namespace cz
{
	internal class P_CZ_SA_GIR_CUTOFF_BIZ
    {
        public DataTable Search(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", obj);
            return dt;
        }
    }
}
