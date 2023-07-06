using System;
using System.Collections.Generic;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;

namespace cz
{
    internal class P_CZ_PU_IV_MNG_PRINT
    {
        private P_CZ_PU_IV_MNG_BIZ _biz = new P_CZ_PU_IV_MNG_BIZ();
        private ReportHelper _rdf;
        private DataSet _ds;
        private string 출력물명;
        private Dictionary<string, string> _dic;

        public P_CZ_PU_IV_MNG_PRINT(string 메뉴ID, string 메뉴명, bool 가로여부, DataSet 출력데이터, Dictionary<string, string> 헤더데이터)
        {
            this._rdf = new ReportHelper(메뉴ID, 메뉴명, 가로여부);
            this._rdf.Printing += new ReportHelper.PrintEventHandler(this.rptHelper_Printing);
            this._ds = 출력데이터;
            this._dic = 헤더데이터;
        }

        private bool rptHelper_Printing(object sender, PrintArgs args)
        {
            try
            {
                if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
                {
                    this._rdf.Initialize();
                    this.출력물명 = args.scriptFile.ToUpper();
                    this.SetBinding();
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return true;
            }
        }

        private void SetBinding()
        {
            foreach (string index in this._dic.Keys)
                this._rdf.SetData(index, this._dic[index]);

            switch (this.출력물명)
            {
                case "R_PU_IV_MNG_001_AVACO.RDF":
                    this._rdf.SetDataTable(this.AVACO_ONLY());
                    break;
                case "R_PU_IV_MNG_001_CNP_D.DRF":
                    this._rdf.SetDataTable(this._ds.Tables[0], 1);
                    this._rdf.SetDataTable(this._ds.Tables[1], 2);
                    break;
                default:
                    if (this.출력물명.Contains("HEADER"))
                    {
                        this._rdf.SetDataTable(this._ds.Tables[1], 1);
                        this._rdf.SetDataTable(this._ds.Tables[1], 2);
                        break;
                    }
                    else
                    {
                        this._rdf.SetDataTable(this._ds.Tables[0], 1);
                        this._rdf.SetDataTable(this._ds.Tables[0], 2);
                        break;
                    }
            }
        }

        public void ShowPrintDialog()
        {
            this._rdf.Print();
        }

        private DataTable AVACO_ONLY()
        {
            DataTable dt = this._ds.Tables[0].Copy();
            DataTable dataTable = this._ds.Tables[0].Clone();

            foreach (DataRow dataRow in ComFunc.getGridGroupBy(dt, new string[] { "CD_PARTNER",
                                                                                  "NO_IV" }, 1 != 0).Rows)
            {
                DataRow[] dataRowArray = dt.Select("NO_IV = '" + D.GetString(dataRow["NO_IV"]) + "'");

                for (int index1 = 0; index1 < dataRowArray.Length; ++index1)
                {
                    dataTable.ImportRow(dataRowArray[index1]);

                    if (dataRowArray.Length == index1 + 1)
                    {
                        dataTable.Rows.Add();
                        int index2 = dataTable.Rows.Count - 1;
                        dataTable.Rows[index2]["AM_CLS"] = dataTable.Rows[index2 - 1]["AM_CLS_SUM"];
                        dataTable.Rows[index2]["NO_IV"] = dataTable.Rows[index2 - 1]["NO_IV"];
                        dataTable.Rows[index2]["CD_PARTNER"] = dataTable.Rows[index2 - 1]["CD_PARTNER"];
                        dataTable.Rows[index2]["NO_LINE"] = 99999;
                        dataTable.Rows[index2]["CD_ITEM"] = "소계";
                    }
                }
            }

            return dataTable;
        }
    }
}
