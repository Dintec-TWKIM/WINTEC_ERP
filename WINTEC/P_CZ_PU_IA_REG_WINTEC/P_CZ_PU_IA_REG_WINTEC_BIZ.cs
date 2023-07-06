// Decompiled with JetBrains decompiler
// Type: pur.P_PU_IA_REG_BIZ
// Assembly: P_PU_IA_REG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1D976FDF-FED5-49ED-BE76-75E9CF3AF51E
// Assembly location: C:\ERPU\Browser\pur\P_PU_IA_REG.dll

using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace cz
{
    public class P_CZ_PU_IA_REG_WINTEC_BIZ
    {
        private string 회사 = Global.MainFrame.LoginInfo.CompanyCode;
        private string 담당자 = Global.MainFrame.LoginInfo.EmployeeNo;

        public DataSet Search(string P_NO_SV) => (DataSet)((ResultData)Global.MainFrame.FillDataSet("UP_PU_IA_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                      P_NO_SV,
                                                                                                                                      Global.SystemLanguage.MultiLanguageLpoint })).DataValue;

        public ResultData Delete(object[] m_obj) => (ResultData)Global.MainFrame.ExecSp("UP_PU_MM_BALANCEH_DELETE", m_obj);

        public ResultData[] Save(DataTable dtH, DataTable dtL, string p_FG_UM) => (ResultData[])Global.MainFrame.Save(new SpInfoCollection()
    {
      new SpInfo()
      {
        DataValue =  dtH,
        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
        SpNameInsert = "UP_PU_MM_BALANCEH_INSERT",
        SpParamsInsert = new string[] { "NO_BALANCE",
                                        "CD_COMPANY",
                                        "CD_SL",
                                        "CD_PLANT",
                                        "DT_SV",
                                        "CD_DEPT",
                                        "NO_EMP",
                                        "DC_RMK",
                                        "ID_INSERT",
                                        "NO_SV1",
                                        "FG_UM" },
        SpParamsValues = {
          {
            ActionState.Insert,
            "NO_SV1",
             dtL.Rows[0]["NO_SV"].ToString()
          }
        }
      },
      new SpInfo()
      {
        DataState = DataValueState.Added,
        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
        DataValue =  dtL,
        SpNameInsert = "UP_PU_MM_BALANCEL_INSERT",
        SpParamsInsert = new string[] { "NO_BALANCE",
                                        "CD_COMPANY",
                                        "CD_ITEM",
                                        "QT_GOOD",
                                        "QT_REJECT",
                                        "QT_TRANS",
                                        "QT_INSP",
                                        "FG_UM",
                                        "CD_PLANT",
                                        "DT_SV",
                                        "CD_PJT",
                                        "SEQ_PROJECT" },
        SpParamsValues = { { ActionState.Insert, "FG_UM", p_FG_UM },
                           { ActionState.Insert, "CD_PLANT", dtH.Rows[0]["CD_PLANT"].ToString() },
                           { ActionState.Insert, "DT_SV", dtH.Rows[0]["DT_SV"].ToString() }
        }
      }
    });

        public DataTable 실사적용(object[] m_obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PU_IA_PI_SELECT",
            SpParamsSelect = m_obj
        })).DataValue;

        public bool 요청적용(DataTable dtH, DataTable dtL, string 조정번호, string _no_emp, string _fg_post)
        {
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(new SpInfoCollection()
            {
                new SpInfo()
                {
                    DataState = DataValueState.Added,
                    DataValue =  dtH,
                    CompanyID = this.회사,
                    UserID = this.담당자,
                    SpNameInsert = "UP_PU_IA_GIREQH_INSERT",
                    SpParamsInsert = new string[] { "NO_GIREQ",
                                                    "CD_PLANT",
                                                    "CD_GRPLANT",
                                                    "CD_COMPANY",
                                                    "DT_IO",
                                                    "FG_GIREQ",
                                                    "FG_GI",
                                                    "CD_DEPT",
                                                    "NO_EMP",
                                                    "DC50_PO1",
                                                    "YN_APP",
                                                    "FG_MODULE" },
                    SpParamsValues = { { ActionState.Insert, "CD_GRPLANT", "" },
                                       { ActionState.Insert, "DC50_PO1", "" },
                                       { ActionState.Insert, "YN_APP", null } }
                },
                new SpInfo()
                {
                    DataState = DataValueState.Added,
                    DataValue =  dtL,
                    CompanyID = this.회사,
                    UserID = this.담당자,
                    SpNameInsert = "UP_PU_IA_TO_GIREQL_INSERT",
                    SpParamsInsert = new string[] { "NO_GIREQ",
                                                    "NO_GIREQLINE",
                                                    "CD_PLANT1",
                                                    "CD_COMPANY",
                                                    "CD_ITEM",
                                                    "QT_GOOD",
                                                    "QT_REJECT",
                                                    "ID_INSERT",
                                                    "NO_BALANCE1",
                                                    "CD_SL1",
                                                    "DT_GIREQ1",
                                                    "ID_INSERT_EMP",
                                                    "CD_PJT",
                                                    "SEQ_PROJECT",
                                                    "FG_POST" },
                    SpParamsValues = { { ActionState.Insert, "CD_PLANT1", dtH.Rows[0]["CD_PLANT"].ToString() },
                                       { ActionState.Insert, "NO_BALANCE1", 조정번호 },
                                       { ActionState.Insert, "CD_SL1", dtH.Rows[0]["CD_SL"].ToString() },
                                       { ActionState.Insert, "DT_GIREQ1", dtH.Rows[0]["DT_IO"].ToString() },
                                       { ActionState.Insert, "FG_POST", _fg_post },
                                       { ActionState.Insert, "ID_INSERT_EMP", _no_emp } }}}))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public ResultData 요청적용취소(DataTable dt, string 조정번호) => (ResultData)Global.MainFrame.Save(new SpInfo()
        {
            DataState = DataValueState.Added,
            DataValue = dt,
            CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
            SpNameInsert = "UP_PU_IA_TO_GIREQ_DELETE",
            SpParamsInsert = new string[] { "NO_GIREQ",
                                            "CD_COMPANY",
                                            "NO_BALANCE1" },
            SpParamsValues = { { ActionState.Insert, "NO_BALANCE1", 조정번호 } }
        });

        public string strGetAPPRO(string strFgCdQtiotp)
        {
            string appro = "N";
            DataTable dataTable = Global.MainFrame.FillDataTable(" SELECT YN_APPRO_GIR    FROM MM_EJTP  WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  AND   CD_QTIOTP = '" + strFgCdQtiotp + "'");
            if (dataTable.Rows.Count > 0)
                appro = dataTable.Rows[0]["YN_APPRO_GIR"].ToString().Trim() == "" ? "N" : dataTable.Rows[0]["YN_APPRO_GIR"].ToString().Trim();
            return appro;
        }
    }
}
