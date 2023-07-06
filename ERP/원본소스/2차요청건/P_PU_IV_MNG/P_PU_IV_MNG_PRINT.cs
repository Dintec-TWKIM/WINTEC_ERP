using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Windows.Print;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;

namespace pur
{
    class P_PU_IV_MNG_PRINT
    {
        ReportHelper _rdf;
        DataSet _ds;
        private string 출력물명;
        Dictionary<string, string> _dic;

        P_PU_IV_MNG_BIZ _biz = new P_PU_IV_MNG_BIZ();

        #region -> 출력 초기화 (인자 받는 부분)

        public P_PU_IV_MNG_PRINT(string 메뉴ID, string 메뉴명, bool 가로여부, DataSet 출력데이터, Dictionary<string, string> 헤더데이터)
        {
            _rdf = new ReportHelper(메뉴ID, 메뉴명, 가로여부);
            _rdf.Printing += new ReportHelper.PrintEventHandler(rptHelper_Printing);
            _ds = 출력데이터;
            _dic = 헤더데이터;
        }

        #endregion

        #region -> 미리보기 시점에서 이벤트 분기하는 부분

        bool rptHelper_Printing(object sender, PrintArgs args)
        {
            try
            {
                if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
                {
                    _rdf.Initialize();
                    출력물명 = args.scriptFile.ToUpper();
                    SetBinding();

                }
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return true;
            }
        }

        #endregion

        #region -> 실제 DB에서 데이타 추출하고 Binding하는 부분

        private void SetBinding()
        {

            foreach (string key in _dic.Keys)
            {
                _rdf.SetData(key, _dic[key]);
            }

            switch (출력물명)
            {

                case "R_PU_IV_MNG_001_AVACO.RDF": //아바코전용
                   DataTable dt = AVACO_ONLY();
                    _rdf.SetDataTable(dt);
                    break;
                case "R_PU_IV_MNG_001_CNP_D.DRF":       //차앤박전용
                    _rdf.SetDataTable(_ds.Tables[0], 1);
                    _rdf.SetDataTable(_ds.Tables[1], 2);
                    break;
                default:
                    if (출력물명.Contains("HEADER") == true)
                    {
                        _rdf.SetDataTable(_ds.Tables[1],1);
                        _rdf.SetDataTable(_ds.Tables[1],2);
                    }
                    else
                    {
                        _rdf.SetDataTable(_ds.Tables[0],1);
                        _rdf.SetDataTable(_ds.Tables[0],2);
                    }
                    break;
            }


        }



        #endregion


        #region -> 최종 출력부분

        public void ShowPrintDialog() { _rdf.Print(); }

        #endregion

        #region -> 아바코전용
        private DataTable AVACO_ONLY()
        {
            DataTable temp_dt = _ds.Tables[0].Copy();
            DataTable dt_subtotal = _ds.Tables[0].Clone();


            DataTable dt_noiv = ComFunc.getGridGroupBy(temp_dt, new[] { "CD_PARTNER", "NO_IV" }, true);

            foreach (DataRow dr in dt_noiv.Rows)
            {
                DataRow[] dtS = temp_dt.Select("NO_IV = '" + D.GetString(dr["NO_IV"]) + "'");

                for (int i = 0; i < dtS.Length; i++)
                {
                    dt_subtotal.ImportRow(dtS[i]);

                    if (dtS.Length == i+1)
                    {
                        dt_subtotal.Rows.Add();
                        int row = dt_subtotal.Rows.Count - 1;

                        dt_subtotal.Rows[row]["AM_CLS"] = dt_subtotal.Rows[row-1]["AM_CLS_SUM"];
                        dt_subtotal.Rows[row]["NO_IV"] = dt_subtotal.Rows[row - 1]["NO_IV"];
                        dt_subtotal.Rows[row]["CD_PARTNER"] = dt_subtotal.Rows[row - 1]["CD_PARTNER"];
                         dt_subtotal.Rows[row]["NO_LINE"] = 99999;
                        dt_subtotal.Rows[row]["CD_ITEM"] = "소계";
                    }
                }
            }


            return dt_subtotal;
        }
        #endregion

    }
}