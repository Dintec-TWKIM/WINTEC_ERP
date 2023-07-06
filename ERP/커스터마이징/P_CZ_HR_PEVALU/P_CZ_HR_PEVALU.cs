using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.Erpiu.ComponentModel;
using System.Collections.Generic;

namespace cz
{
    public partial class P_CZ_HR_PEVALU : PageBase
    {
        private P_CZ_HR_PEVALU_BIZ _biz = new P_CZ_HR_PEVALU_BIZ();
        private CommonFunction _func = new CommonFunction();
        private string 마감여부 = null;

        public P_CZ_HR_PEVALU()
        {
            StartUp.Certify(this);
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitOneGrid();
            this.InitGrid();
            this.InitEvent();
        }

        private void InitOneGrid()
        {
            this.oneGrid1.UseCustomLayout = false;
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();

            this.bppnl평가코드.IsNecessaryCondition = true;
            this.bppnl평가유형.IsNecessaryCondition = true;
            this.bppnl평가차수.IsNecessaryCondition = true;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_EMPAN", "사번", 80, false);
            this._flexH.SetCol("NM_KOR", "피평가자명", 90, false);

            this._flexH.SetOneGridBinding(null, new IUParentControl[] { this.imagePanel3, this.imagePanel4 });

            this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetOneGridBinding(null, new IUParentControl[] { this.imagePanel1, this.imagePanel2 });
            this._flexL.AllowCache = false;
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);

            this._flexL.StartEdit += new RowColEventHandler(this._flexL_StartEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.AfterEdit += new RowColEventHandler(this._flexL_AfterEdit);

            this.bpc평가코드.QueryBefore += new BpQueryHandler(this.bpc평가코드_QueryBefore);
            this.bpc평가코드.QueryAfter += new BpQueryHandler(this.bpc평가코드_QueryAfter);
            this.btn개인별정보.Click += new EventHandler(this.btn개인별정보_Click);

            this.txt항목별.TextChanged += new EventHandler(this.Control_TextChanged);
            this.txt차수별.TextChanged += new EventHandler(this.Control_TextChanged);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
        }

        protected override bool IsChanged()
        {
            if (this._flexH.GetChanges() != null || this._flexL.GetChanges() != null)
                return true;
            else
                return false;
        }

        protected override bool BeforeSearch()
        {
            if (this.bpc평가코드.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("평가코드") });
                this.bpc평가코드.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(D.GetString(this.cbo평가유형.SelectedValue)))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("평가유형") });
                this.cbo평가유형.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(D.GetString(this.cbo평가차수.SelectedValue)))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("평가차수") });
                this.cbo평가차수.Focus();
                return false;
            }

            return base.BeforeSearch();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                DataTable dataTable = this._biz.Search(new object[] { this.bpc평가코드.CodeValue,
                                                                      this.cbo평가유형.SelectedValue.ToString(),
                                                                      this.cbo평가차수.SelectedValue.ToString(),
                                                                      this.cbo평가그룹.SelectedValue.ToString(),
                                                                      Global.MainFrame.LoginInfo.EmployeeNo });

                this._flexL.DataTable = null;
                this.SetColumn();
                this._flexH.Binding = dataTable;

                if (!this._flexH.HasNormalRow)
                {
                    this._flexL.RemoveViewAll();
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;
            if (!this._flexH.IsDataChanged && !this._flexL.IsDataChanged) return false;
            if (this.마감여부 == "Y")
            {
                this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
                return false;
            }

            if (!this._biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges()))
                return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();
            
            return true;
        }

        private void SetColumn()
        {
            DataTable dataTable, dataTable1;
            List<string> expectSumCol = new List<string>();

            string 목표여부 = D.GetString(DBHelper.GetDataTable(@"SELECT YN_OBJECT AS OBT 
                                                                  FROM HR_PEVALU_CODE WITH(NOLOCK)  
                                                                  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                                @"AND CD_FIELD = '100' 
                                                                  AND CD_EVALU = '" + this.bpc평가코드.CodeValue + "'" + 
                                                                 "AND CD_HCODE = '" + this.cbo평가유형.SelectedValue.ToString() + "'").Rows[0]["OBT"]);

            dataTable = DBHelper.GetDataTable(@"SELECT CD_HCODE, 
                                                	   NM_HCODE 
                                                FROM HR_PEVALU_CODE WITH(NOLOCK) 
                                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                              @"AND CD_EVALU = '" + this.bpc평가코드.CodeValue + "'" +
                                              @"AND CD_FIELD = '300'
                                                AND YN_USE = 'Y'");

            dataTable1 = DBHelper.GetDataTable(@"SELECT CD_DCODE AS CODE, 
                                                        NM_DCODE AS NAME 
                                                FROM HR_PEVALU_CODEDTL WITH(NOLOCK) 
                                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                              @"AND CD_FIELD = '400' 
                                                AND CD_HCODE = '200' 
                                                AND CD_EVALU = '" + this.bpc평가코드.CodeValue + "'");

            this._flexL.BeginSetting(1, 1, false);
            this._flexL.Cols.Count = 1;
            
            if (목표여부 == "Y")
            {
                this._flexL.SetCol("NM_OTASK", "목표(과업)명", 150, false);
                this._flexL.SetCol("DC_DOBJECT", "세부목표", 250, false);
            }
            else
            {
                this._flexL.SetCol("NM_EVITEM_LV1", "항목레벨1", 100, false);
                this._flexL.SetCol("NM_EVITEM_LV2", "항목레벨2", 100, false);
                this._flexL.SetCol("NM_EVITEM_LV3", "항목레벨3", 100, false);
                this._flexL.SetCol("DC_INDEX", "내 용", 150, false);
            }

            this._flexL.SetCol("NUM_EWEIGHT", "가중치", 60, false, typeof(decimal));
            this._flexL.SetCol("PT_SCORE", "점수", 60, true, typeof(decimal));
            this._flexL.SetCol("CD_GRADE", "등급(점수)", 80, true);
            this._flexL.SetDataMap("CD_GRADE", dataTable1, "CODE", "NAME");

            expectSumCol.Add("PT_SCORE");
            
            foreach (DataRow dr in dataTable.Rows)
            {
                if (D.GetDecimal(this.cbo평가차수.SelectedValue) > D.GetDecimal(dr["CD_HCODE"]))
                {
                    this._flexL.SetCol("PT_SCORE_" + D.GetString(dr["CD_HCODE"]), D.GetString(dr["NM_HCODE"]) + "점수", 100, false, typeof(decimal));
                    this._flexL.SetCol("CD_GRADE_" + D.GetString(dr["CD_HCODE"]), D.GetString(dr["NM_HCODE"]) + "등급", 100, false);
                    this._flexL.SetDataMap("CD_GRADE_" + D.GetString(dr["CD_HCODE"]), dataTable1, "CODE", "NAME");

                    expectSumCol.Add("PT_SCORE_" + D.GetString(dr["CD_HCODE"]));
                }
            }

            this._flexL.AllowCache = false;
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.SetExceptSumCol(expectSumCol.ToArray());
            
            if (!(목표여부 == "N"))
                return;
            
            this._flexL.AllowMerging = AllowMergingEnum.RestrictRows;
            this._flexL.Cols["NM_EVITEM_LV1"].AllowMerging = true;
            this._flexL.Cols["NM_EVITEM_LV2"].AllowMerging = true;
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter, comment;
            Dictionary<int, decimal> rptScore, rcdGrade;
            int index;
            decimal ptScore, cdGrade;

            try
            {
                filter = "NO_EMPAN = '" + this._flexH["NO_EMPAN"] + "'";
                dt = null;

                DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_HCODE, 
                                                	                 NM_HCODE 
                                                              FROM HR_PEVALU_CODE WITH(NOLOCK) 
                                                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                            @"AND CD_EVALU = '" + this.bpc평가코드.CodeValue + "'" +
                                                            @"AND CD_FIELD = '300'
                                                              AND YN_USE = 'Y'");

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               D.GetString(this._flexH["CD_EVALU"]), 
                                                               D.GetString(this._flexH["CD_EVTYPE"]),
                                                               D.GetString(this._flexH["CD_EVNUMBER"]),
                                                               D.GetString(this._flexH["CD_GROUP"]),
                                                               D.GetString(this._flexH["NO_EMPM"]),
                                                               D.GetString(this._flexH["NO_EMPAN"]),
                                                               D.GetString(this._flexH["YN_OBJECT"]) });
                }

                if (this._flexL.DataTable == null)
                    this._flexL.Binding = dt;
                else
                    this._flexL.BindingAdd(dt, filter);

                ptScore = 0;
                rptScore = new Dictionary<int, decimal>();
                cdGrade = 0;
                rcdGrade = new Dictionary<int, decimal>();

                foreach(DataRow dr in this._flexL.DataTable.Select(filter))
                {
                    if (D.GetString(this._flexH["YN_OBJECT"]) == "Y")
                    {
                        if (D.GetString(this._flexH["CD_EVNUMBER"]) != "000")
                        {
                            if (string.IsNullOrEmpty(D.GetString(dr["PT_SCORE"])))
                            {
                                dr["PT_SCORE"] = dr["PT_SCORE_" + string.Format("{0:000}", (D.GetDecimal(this._flexH["CD_EVNUMBER"]) - 1))];
                            }

                            if (string.IsNullOrEmpty(D.GetString(dr["DC_COMMENT1"])))
                            {
                                dr["DC_COMMENT1"] = D.GetString(dr["DC_COMMENT2"]);
                            }
                        }

                        ptScore += (D.GetDecimal(dr["PT_SCORE"]) * D.GetDecimal(dr["NUM_EWEIGHT"]));

                        index = 0;

                        foreach (DataRow dr1 in dataTable.Rows)
                        {
                            if (rptScore.ContainsKey(index) == false)
                                rptScore.Add(index, (D.GetDecimal(dr["PT_SCORE_" + D.GetString(dr1["CD_HCODE"])]) * D.GetDecimal(dr["NUM_EWEIGHT"])));
                            else
                                rptScore[index] += (D.GetDecimal(dr["PT_SCORE_" + D.GetString(dr1["CD_HCODE"])]) * D.GetDecimal(dr["NUM_EWEIGHT"]));
                            
                            index++;
                        }
                    }
                    else
                    {
                        cdGrade += (D.GetDecimal(dr["PT_HSCORE"]) * D.GetDecimal(dr["NUM_EWEIGHT"]));

                        index = 0;

                        foreach (DataRow dr1 in dataTable.Rows)
                        {
                            if (rcdGrade.ContainsKey(index) == false)
                                rcdGrade.Add(index, (D.GetDecimal(dr["PT_HSCORE_" + D.GetString(dr1["CD_HCODE"])]) * D.GetDecimal(dr["NUM_EWEIGHT"])));
                            else
                                rcdGrade[index] += (D.GetDecimal(dr["PT_HSCORE_" + D.GetString(dr1["CD_HCODE"])]) * D.GetDecimal(dr["NUM_EWEIGHT"]));

                            index++;
                        }
                    }
                }

                comment = "가. 주요 업무 실적 기술" + Environment.NewLine + Environment.NewLine +
                          "나. 본인 업무와 관련 주요 자기개발 사항 기술" + Environment.NewLine + Environment.NewLine +
                          "다. 현재 수행중인 업무량, 업무의 질, 적성, 업무만족도 기술" + Environment.NewLine +
                          " 1) 업무량    :" + Environment.NewLine +
                          " 2) 업무의 질 :" + Environment.NewLine +
                          " 3) 업무 적성 :" + Environment.NewLine +
                          " 4) 업무만족도:" + Environment.NewLine + Environment.NewLine +
                          "라. 향후 본인 직무와 관련 희망 사항 기술";

                if (string.IsNullOrEmpty(D.GetString(this._flexH["COMMENT1"])))
                {
                    if (D.GetString(this._flexH["CD_EVNUMBER"]) == "000")
                    {
                        this._flexH["COMMENT1"] = comment;
                        this.txt차수별.Text = comment;
                    }
                    else
                    {
                        this._flexH["COMMENT1"] = D.GetString(this._flexH["COMMENT2"]);
                        this.txt차수별.Text = D.GetString(this._flexH["COMMENT2"]);
                    }
                }

                this._flexL[this._flexL.Rows.Fixed - 1, "PT_SCORE"] = string.Format("{0:0.0000}", ptScore);
                this._flexL[this._flexL.Rows.Fixed - 1, "CD_GRADE"] = string.Format("{0:0.0000}", cdGrade);

                index = 0;

                foreach (DataRow dr in dataTable.Rows)
                {
                    if (rptScore.ContainsKey(index))
                        this._flexL[this._flexL.Rows.Fixed - 1, "PT_SCORE_" + D.GetString(dr["CD_HCODE"])] = string.Format("{0:0.0000}", rptScore[index]);

                    if (rcdGrade.ContainsKey(index))
                        this._flexL[this._flexL.Rows.Fixed - 1, "CD_GRADE_" + D.GetString(dr["CD_HCODE"])] = string.Format("{0:0.0000}", rcdGrade[index]);

                    index++;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (this._flexL.Cols[e.Col].Name)
                {
                    case "CD_GRADE":
                        if (D.GetString(this._flexL["CD_SCALE"]) != "200")
                        {
                            this.ShowMessage("CZ_해당 항목의 평가척도는 점수로 설정되어 있습니다.");
                            e.Cancel = true;
                            break;
                        }
                        break;
                    case "PT_SCORE":
                        if (D.GetString(this._flexL["CD_SCALE"]) != "100")
                        {
                            this.ShowMessage("CZ_해당 항목의 평가척도는 등급으로 설정되어 있습니다.");
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string str1, editData;
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                str1 = grid.GetData(e.Row, e.Col).ToString();
                editData = grid.EditData;

                if (str1.ToUpper() == editData.ToUpper())
                    return;
                
                if (grid.Cols[e.Col].Name == "CD_GRADE")
                {
                    string query = @"SELECT CD_FLAG1
                                     FROM HR_PEVALU_CODEDTL WITH(NOLOCK) 
                                     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                    "AND CD_EVALU = '" + this.bpc평가코드.CodeValue + "'" + Environment.NewLine +
                                    "AND CD_DCODE = '" + editData + "'" + Environment.NewLine +
                                   @"AND CD_FIELD = '400'
                                     AND YN_USE = 'Y'
                                     AND CD_HCODE = '200'";

                    this._flexL["PT_HSCORE"] = D.GetDecimal(DBHelper.ExecuteScalar(query));
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_AfterEdit(object sender, RowColEventArgs e)
        {
            string filter;
            decimal score;
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);
                filter = "NO_EMPAN = '" + D.GetString(this._flexH["NO_EMPAN"]) + "'";

                if (grid.Cols[e.Col].Name == "CD_GRADE")
                {
                    score = 0;

                    foreach (DataRow dr in this._flexL.DataTable.Select(filter))
                    {
                        score += (D.GetDecimal(dr["PT_HSCORE"]) * D.GetDecimal(dr["NUM_EWEIGHT"]));
                    }

                    this._flexL[this._flexL.Rows.Fixed - 1, "CD_GRADE"] = string.Format("{0:0.0000}", score);
                }
                else if (grid.Cols[e.Col].Name == "PT_SCORE")
                {
                    score = 0;

                    foreach (DataRow dr in this._flexL.DataTable.Select(filter))
                    {
                        score += (D.GetDecimal(dr["PT_SCORE"]) * D.GetDecimal(dr["NUM_EWEIGHT"]));
                    }

                    this._flexL[this._flexL.Rows.Fixed - 1, "PT_SCORE"] = string.Format("{0:0.0000}", score);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void bpc평가코드_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.bpc평가코드.CodeValue = e.HelpReturn.Rows[0]["CODE"].ToString();
                this.bpc평가코드.CodeName = e.HelpReturn.Rows[0]["NAME"].ToString();
                this.마감여부 = e.HelpReturn.Rows[0]["YNCLOSE"].ToString();
                string str1 = string.Empty;

                this.cbo평가유형.DataSource = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE,
                                                                             NM_HCODE AS NAME
                                                                      FROM HR_PEVALU_CODE WITH(NOLOCK)
                                                                      WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                    @"AND CD_FIELD = '100' 
                                                                      AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
                                                                     "AND YN_USE = 'Y'");
                this.cbo평가유형.DisplayMember = "NAME";
                this.cbo평가유형.ValueMember = "CODE";
                string str2 = string.Empty;

                this.cbo평가차수.DataSource = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE,
                                                                             NM_HCODE AS NAME
                                                                      FROM HR_PEVALU_CODE WITH(NOLOCK)
                                                                      WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                    @"AND CD_FIELD = '300'
                                                                      AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
                                                                     "AND YN_USE = 'Y'");
                this.cbo평가차수.DisplayMember = "NAME";
                this.cbo평가차수.ValueMember = "CODE";
                string str3 = string.Empty;

                this.cbo평가그룹.DataSource = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE,
                                                                             NM_HCODE AS NAME
                                                                      FROM HR_PEVALU_CODE WITH(NOLOCK)
                                                                      WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                    @"AND CD_FIELD = '200'
                                                                      AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
                                                                     "AND YN_USE = 'Y'");
                this.cbo평가그룹.DisplayMember = "NAME";
                this.cbo평가그룹.ValueMember = "CODE";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc평가코드_QueryBefore(object sender, BpQueryArgs e)
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

        private void btn개인별정보_Click(object sender, EventArgs e)
        {
            string 사업장, 사번, 이름;

            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage("CZ_선택된 피평가자가 없습니다.");
                }
                else
                {
                    사업장 = D.GetString(this._flexH[this._flexH.Row, "CD_BIZAREA"]);
                    사번 = D.GetString(this._flexH[this._flexH.Row, "NO_EMPAN"]);
                    이름 = D.GetString(this._flexH[this._flexH.Row, "NM_KOR"]);

                    new P_CZ_HR_PEVALU_DLG(사업장, 사번, 이름).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
