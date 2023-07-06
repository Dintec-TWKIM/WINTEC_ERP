using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_MA_WORKFLOW_BIZ
    {
        public DataTable Search(진행단계 선택단계, object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_MA_WORKFLOWH_S_" + ((int)선택단계).ToString(), obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataSet SearchDetail(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_MA_WORKFLOWL_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

        public void 변경사항저장(string 선택회사코드, string 단계, string key, string 코드, string 비고, string 청구예정일)
        {
            try
            {
                DBHelper.ExecuteScalar("SP_CZ_MA_WORKFLOWH_U", new object[] { 선택회사코드,
                                                                              단계,
                                                                              key,
                                                                              Global.MainFrame.LoginInfo.UserID,
                                                                              코드,
                                                                              비고,
                                                                              청구예정일 });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public DataTable 워크플로우비고(진행단계 진행단계)
        {
            string query;
            DataTable dt = null;

            try
            {
                query = @"SELECT CD_SYSDEF,
                                 NM_SYSDEF 
                          FROM MA_CODEDTL WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                        @"AND CD_FIELD = 'CZ_MA00010' 
                          AND CD_FLAG1 = '" + ((int)진행단계).ToString("00") + "'";

                dt = DBHelper.GetDataTable(query);
                dt.Rows.InsertAt(dt.NewRow(), 0);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }
    }
}
