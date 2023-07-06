using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_PR_OPOUT_PR_MNG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_PR_OPOUT_PR_MNG_SELECT", obj);
            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(decimal))
                    col.DefaultValue = 0;
            }
            return dt;
        }

        public bool Save(DataTable dt)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dt != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameDelete = "UP_CZ_PR_OPOUT_PR_DELETE",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_PR",
                                                    "NO_LINE" }
                });                
            }
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }
    }
}