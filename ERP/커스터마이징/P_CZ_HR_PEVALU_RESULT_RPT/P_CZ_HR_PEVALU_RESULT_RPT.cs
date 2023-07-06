using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using System.Windows.Forms;
using System.Collections.Generic;

namespace cz
{
    public partial class P_CZ_HR_PEVALU_RESULT_RPT : PageBase
    {
        P_CZ_HR_PEVALU_RESULT_RPT_BIZ _biz = new P_CZ_HR_PEVALU_RESULT_RPT_BIZ();
        P_CZ_HR_PEVALU_RESULT_RPT_SUB _subDialog;

        public P_CZ_HR_PEVALU_RESULT_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex };
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            try
            {
                this._flex.BeginSetting(2, 1, false);

                this._flex.SetCol("NO_EMPAN", "사번", 85, false);
                this._flex.SetCol("NM_KOR", "이름", 85, false);
                this._flex.SetCol("NM_DEPT", "부서", 85, false);

                this._flex.AllowCache = false;
                this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }   
        }

        private void InitEvent()
        {
            this.ctx평가코드.QueryBefore += new BpQueryHandler(this.ctx평가코드_QueryBefore);
            this.bpc부서.QueryBefore += new BpQueryHandler(this.bpc부서_QueryBefore);
            this.bpc피평가자.QueryBefore += new BpQueryHandler(this.bpc피평가자_QueryBefore);

            this._flex.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
        }

        protected override bool BeforeSearch()
        {
            if (!this.ctx평가코드.IsEmpty())
                return base.BeforeSearch();
            
            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("평가코드") });

            this.ctx평가코드.Focus();
            return false;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt, dt1, dt2;
            string query, query1, code, code1, code2;
            Dictionary<string, decimal> score, weight;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this.동적컬럼생성();

                dt = this._biz.Search(new object[] { this.ctx평가코드.CodeValue,
                                                     this.bpc사업장.QueryWhereIn_Pipe,
                                                     this.bpc부서.QueryWhereIn_Pipe,
                                                     this.bpc피평가자.QueryWhereIn_Pipe });
                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                else
                {
                    query = @"SELECT CD_HCODE, NM_HCODE, RT_WEIGHT1
                              FROM HR_PEVALU_CODE WITH(NOLOCK) 
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            @"AND YN_USE = 'Y'
                              AND CD_EVALU = '" + this.ctx평가코드.CodeValue + "'";

                    query1 = query + Environment.NewLine + "AND CD_FIELD = '100'" + Environment.NewLine + "ORDER BY CD_HCODE";
                    dt1 = DBHelper.GetDataTable(query1);

                    query1 = query + Environment.NewLine + "AND CD_FIELD = '300'" + Environment.NewLine + "ORDER BY CD_HCODE";
                    dt2 = DBHelper.GetDataTable(query1);

                    weight = new Dictionary<string, decimal>();

                    foreach (DataRow dr in dt1.Rows)
                    {
                        code = "SCORE_" + D.GetString(dr["CD_HCODE"]) + "_TOTAL";

                        foreach (DataRow dr1 in this._flex.DataTable.Rows)
                        {
                            score = new Dictionary<string, decimal>();

                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                if (!weight.ContainsKey(D.GetString(dr2["CD_HCODE"])))
                                    weight.Add(D.GetString(dr2["CD_HCODE"]), D.GetDecimal(dr2["RT_WEIGHT1"]));

                                code1 = "SCORE_" + D.GetString(dr["CD_HCODE"]) + "_" + D.GetString(dr2["CD_HCODE"]);
                                score.Add(D.GetString(dr2["CD_HCODE"]), D.GetDecimal(dr1[code1]));
                            }

                            code2 = "CD_EVTYPE_" + D.GetString(dr["CD_HCODE"]);

                            if (D.GetString(dr1[code2]) == "003")
                                dr1[code] = ((D.GetDecimal(score["001"]) * D.GetDecimal(weight["001"])) 
                                          + (D.GetDecimal(score["002"]) * D.GetDecimal(weight["002"])) 
                                          + (D.GetDecimal(score["003"]) * D.GetDecimal(weight["003"])));
                            else if (D.GetString(dr1[code2]) == "002")
                            {
                                if (this.ctx평가코드.CodeValue == "20151101")
                                    dr1[code] = ((D.GetDecimal(score["001"]) * D.GetDecimal(weight["001"])) 
                                              + (D.GetDecimal(score["002"]) * D.GetDecimal(weight["002"])));
                                else
                                    dr1[code] = (D.GetDecimal(score["001"]) * D.GetDecimal(D.GetDecimal(0.50))) 
                                              + (D.GetDecimal(score["002"]) * D.GetDecimal(D.GetDecimal(0.50)));
                            }
                            else if (D.GetString(dr1[code2]) == "001")
                                dr1[code] = D.GetDecimal(score["001"]);
                        }
                    }

                    this._flex.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc부서_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (this.bpc사업장.IsEmpty())
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("사업장"));
                    this.bpc사업장.Focus();
                    e.QueryCancel = true;
                }
                else
                {
                    e.HelpParam.P26_AUTH_BIZAREA = this.bpc사업장.QueryWhereIn_Pipe;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc피평가자_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P00_CHILD_MODE = "인사평가 코드도움창";
                e.HelpParam.P61_CODE1 = "HN.NO_EMPAN AS CODE, EP.NM_KOR AS NAME";
                e.HelpParam.P62_CODE2 = "HR_PEVALU_HUMEMPAN HN WITH(NOLOCK)" + Environment.NewLine +
                                        "LEFT OUTER JOIN MA_EMP EP WITH(NOLOCK) ON HN.CD_COMPANY = EP.CD_COMPANY AND HN.NO_EMPAN = EP.NO_EMP";
                e.HelpParam.P63_CODE3 = "WHERE HN.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        "AND HN.CD_EVALU = '" + this.ctx평가코드.CodeValue + "'";
                e.HelpParam.P64_CODE4 = "GROUP BY HN.NO_EMPAN, EP.NM_KOR";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx평가코드_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P00_CHILD_MODE = "인사평가 코드도움창";
                e.HelpParam.P61_CODE1 = "CD_EVALU AS CODE, NM_EVALU AS NAME, YM_EVALU AS YM, YN_CLOSE AS YNCLOSE ";
                e.HelpParam.P62_CODE2 = "HR_PEVALU_SCHE WITH(NOLOCK) WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
                e.HelpParam.P63_CODE3 = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 동적컬럼생성()
        {
            DataTable dt1, dt2;
            string query, query1, code, name;

            try
            {
                query = "SELECT CD_HCODE, NM_HCODE" + Environment.NewLine +
                        "FROM HR_PEVALU_CODE WITH(NOLOCK)" + Environment.NewLine +
                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                        "AND YN_USE = 'Y'" + Environment.NewLine +
                        "AND CD_EVALU = '" + this.ctx평가코드.CodeValue + "'";

                query1 = query + Environment.NewLine + "AND CD_FIELD = '100'" + Environment.NewLine + "ORDER BY CD_HCODE";
                dt1 = DBHelper.GetDataTable(query1);

                query1 = query + Environment.NewLine + "AND CD_FIELD = '300'" + Environment.NewLine + "ORDER BY CD_HCODE";
                dt2 = DBHelper.GetDataTable(query1);

                this._flex.BeginSetting(2, 1, false);
                this._flex.Cols.Count = 4;

                foreach(DataRow dr1 in dt1.Rows)
                {
                    code = "CD_EVTYPE_" + D.GetString(dr1["CD_HCODE"]);
                    name = "최종차수";

                    this._flex.SetCol(code, name, 85, false, typeof(decimal));
                    this._flex.SetDataMap(code, dt2, "CD_HCODE", "NM_HCODE");
                    this._flex[0, this._flex.Cols[code].Index] = D.GetString(dr1["NM_HCODE"]);

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        code = "SCORE_" + D.GetString(dr1["CD_HCODE"]) + "_" + D.GetString(dr2["CD_HCODE"]);
                        name =  D.GetString(dr2["NM_HCODE"]);

                        this._flex.SetCol(code, name, 85, false, typeof(decimal));
                        this._flex.Cols[code].Format = "##0.#0";

                        this._flex[0, this._flex.Cols[code].Index] = D.GetString(dr1["NM_HCODE"]);
                    }

                    code = "SCORE_" + D.GetString(dr1["CD_HCODE"]) + "_TOTAL";
                    name = "합계점수";

                    this._flex.SetCol(code, name, 85, false, typeof(decimal));
                    this._flex.Cols[code].Format = "##0.#0";

                    this._flex[0, this._flex.Cols[code].Index] = D.GetString(dr1["NM_HCODE"]);    
                }

                foreach (DataRow dr2 in dt2.Rows)
                {
                    code = "DC_COMMENT_" + D.GetString(dr2["CD_HCODE"]);
                    name = D.GetString(dr2["NM_HCODE"]);

                    this._flex.SetCol(code, name, 200, false);

                    this._flex[0, this._flex.Cols[code].Index] = "평가의견";
                }

                this._flex.AllowCache = false;
                this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FlexGrid grid;
            string text, 평가유형, 평가차수;
            string[] tempText;

            try
            {
                grid = (sender as FlexGrid);
                if (grid.HasNormalRow == false) return;
                if (grid.MouseRow < grid.Rows.Fixed) return;

                text = D.GetString(this._flex.Cols[this._flex.ColSel].UserData);

                tempText = text.Split('_');

                if (tempText.Length != 2) return;

                평가유형 = tempText[0];
                평가차수 = tempText[1];

                this._subDialog = new P_CZ_HR_PEVALU_RESULT_RPT_SUB(this.ctx평가코드.CodeValue, 평가유형, 평가차수, D.GetString(this._flex["NO_EMPAN"]));
                this._subDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
