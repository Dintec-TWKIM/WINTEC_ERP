using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace account
{
    class P_FI_TYPE011_BIZ
    {
        string _CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
        string _UserID = Global.MainFrame.LoginInfo.UserID;

        internal DataSet Search()
        {
            return DBHelper.GetDataSet("", new object[] { });
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null) return true;

            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = _CompanyCode;
            si.UserID = _EmployeeNo;
            si.SpNameInsert = "";
            si.SpParamsInsert = new string[] { };

            si.SpNameUpdate = "";
            si.SpParamsUpdate = new string[] { };

            si.SpNameDelete = "";
            si.SpParamsDelete = new string[] { };

            return DBHelper.Save(si);
        }

        public bool SaveSub(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = _CompanyCode;
            si.UserID = _UserID;

            si.SpNameInsert = "";
            si.SpParamsInsert = new string[] { };
            return DBHelper.Save(si);
        }

        internal DataTable Print()
        {
            return DBHelper.GetDataTable("", new object[] { _CompanyCode });
        }
    }
}