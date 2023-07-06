using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_PU_STS_BATCH_REG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_STS_BATCH_REGH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_STS_BATCH_REGL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_STS_BATCH_REGL1_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = dt;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameDelete = "SP_CZ_PU_STS_BATCH_REGH_D";
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_GIREQ",
                                                       "ID_DELETE" };

                spInfo.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);
            }

            return DBHelper.Save(spInfo);
        }

        public bool 재고출고(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = dt;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "SP_CZ_PU_STS_BATCH_REG_GI";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_BIZAREA",
                                                       "NO_GIREQ",
                                                       "NO_EMP",
                                                       "CD_DEPT",
                                                       "ID_INSERT" };

                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA", Global.MainFrame.LoginInfo.BizAreaCode);
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_EMP", Global.MainFrame.LoginInfo.EmployeeNo);
                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_DEPT", Global.MainFrame.LoginInfo.DeptCode);
            }

            return DBHelper.Save(spInfo);
        }

        public bool 출고취소(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = dt;
                spInfo.DataState = DataValueState.Deleted;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameDelete = "SP_CZ_PU_STS_BATCH_REG_GI_D";
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_ISURCVLINE",
                                                       "ID_DELETE" };

                spInfo.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);
            }

            return DBHelper.Save(spInfo);
        }

        public bool 요청삭제(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = dt;
                spInfo.DataState = DataValueState.Deleted;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameDelete = "SP_CZ_PU_STS_BATCH_REGL_D";
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_GIREQ",
                                                       "NO_LINE",
                                                       "ID_DELETE" };

                spInfo.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);
            }

            return DBHelper.Save(spInfo);
        }

        internal bool SavePrintLog(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return false;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.DataState = DataValueState.Added;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "SP_CZ_PU_STS_BATCH_REGH_U";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_GIREQ",
                                                   "ID_INSERT" };

            return DBHelper.Save(spInfo);
        }
    }
}
