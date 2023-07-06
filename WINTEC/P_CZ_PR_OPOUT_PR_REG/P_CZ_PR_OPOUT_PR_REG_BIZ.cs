using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_PR_REG_BIZ
    {
        public DataSet Search(object[] obj)
        {
            DataSet ds = (DataSet)((ResultData)Global.MainFrame.FillDataSet("SP_CZ_PR_OPOUT_PR_REG_S", obj)).DataValue;

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == typeof(decimal))
                        column.DefaultValue = 0;
                }
            }
            ds.Tables[0].Columns["CD_COMPANY"].DefaultValue = Global.MainFrame.LoginInfo.CompanyCode;
            ds.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            ds.Tables[0].Columns["DT_PR"].DefaultValue = Global.MainFrame.GetStringToday;

            return ds;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_OPOUT_PR_REG_ID_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public void DeleteAll(string 공장, string 의뢰번호)
        {
            Global.MainFrame.ExecSp("UP_CZ_PR_OPOUT_PR_REG_DELETE_ALL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       공장,
                                                                                       의뢰번호 });
        }

        public bool Save(DataTable dt, DataTable dtID, DataRow drHeader)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dt != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_CZ_PR_OPOUT_PR_INSERT",
                    SpNameUpdate = "UP_CZ_PR_OPOUT_PR_UPDATE",
                    SpNameDelete = "UP_CZ_PR_OPOUT_PR_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PR",
                                                    "NO_LINE",
                                                    "DT_PR",
                                                    "NO_EMP",
                                                    "NO_WO",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "CD_ITEM",
                                                    "QT_PR",
                                                    "DT_DUE",
                                                    "CD_PARTNER",
                                                    "DC_RMK",
                                                    "ID_INSERT" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PR",
                                                    "NO_LINE",
                                                    "DT_PR",
                                                    "NO_EMP",
                                                    "NO_WO",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "CD_ITEM",
                                                    "QT_PR",
                                                    "DT_DUE",
                                                    "CD_PARTNER",
                                                    "DC_RMK",
                                                    "ID_UPDATE" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PR",
                                                    "NO_LINE" },
                    SpParamsValues = { {  ActionState.Insert, "NO_PR", drHeader["NO_PR"].ToString() },
                                       {  ActionState.Insert, "DT_PR", drHeader["DT_PR"].ToString() },
                                       {  ActionState.Update, "NO_PR", drHeader["NO_PR"].ToString() },
                                       {  ActionState.Update, "DT_PR", drHeader["DT_PR"].ToString() },
                                       {  ActionState.Delete, "NO_PR", drHeader["NO_PR"].ToString() },
                                       {  ActionState.Delete, "DT_PR", drHeader["DT_PR"].ToString() } }
                });
            if (dtID != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtID,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "SP_CZ_PR_OPOUT_PR_REG_INSP_U",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "S",
                                                    "NO_WO",
                                                    "NO_WO_LINE",
                                                    "SEQ_WO",
                                                    "NO_PR",
                                                    "NO_OPOUT_PR",
                                                    "NO_PR_LINE",
                                                    "DT_INSP",
                                                    "ID_UPDATE" },
                    SpParamsValues = { { ActionState.Update, "NO_PR", drHeader["NO_PR"].ToString() },
                                       { ActionState.Update, "DT_INSP", drHeader["DT_PR"].ToString() } }
                });
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }
    }
}