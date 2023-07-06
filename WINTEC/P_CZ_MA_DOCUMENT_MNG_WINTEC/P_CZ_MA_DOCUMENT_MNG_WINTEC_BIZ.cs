using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Util;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_MA_DOCUMENT_MNG_WINTEC_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MA_DOCUMENT_MNG_WINTEC_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

		public DataTable Search1(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_MA_DOCUMENT_MNG_WINTEC_S1", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		internal bool Save(string 공장코드, DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_MA_DOCUMENT_MNG_WINTEC_XML";
            si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "XML", "ID_INSERT" };

            si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", 공장코드);

            return DBHelper.Save(si);
        }

		internal bool Save1(string 공장코드, DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = Util.GetXmlTable(dt);
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameInsert = "SP_CZ_MA_DOCUMENT_MNG_WINTEC_XML1";
			si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "XML", "ID_INSERT" };

			si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", 공장코드);

			return DBHelper.Save(si);
		}
	}
}
