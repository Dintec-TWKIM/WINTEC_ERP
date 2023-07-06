using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace cz
{
    public partial class P_CZ_HR_PEVALU_RESULT_RPT_SUB : Duzon.Common.Forms.CommonDialog
    {
        string _평가코드, _평가유형, _평가그룹, _평가차수, _피평가자;

        public P_CZ_HR_PEVALU_RESULT_RPT_SUB(string 평가코드, string 평가유형, string 평가차수, string 피평가자)
        {
            InitializeComponent();

            this._평가코드 = 평가코드;
            this._평가유형 = 평가유형;
            this._평가그룹 = "600";
            this._평가차수 = 평가차수;
            this._피평가자 = 피평가자;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.SearchData();
        }

        private void InitGrid()
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_DCODE AS CODE,
                                                                 NM_DCODE AS NAME
                                                          FROM HR_PEVALU_CODEDTL WITH(NOLOCK)
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                        @"AND CD_FIELD = '400'
                                                          AND CD_HCODE = '200' 
                                                          AND CD_EVALU = '" + this._평가코드 + "'");

            this._flex.BeginSetting(1, 1, false);
            this._flex.Cols.Count = 1;

            this._flex.SetCol("NM_EMPM", "평가자", 80, false);
            this._flex.SetCol("NM_EMPAN", "피평가자", 80, false);

            if (this._평가유형 == "300")
            {
                this._flex.SetCol("NM_OTASK", "목표(과업)명", 150, false);
                this._flex.SetCol("DC_DOBJECT", "세부목표", 250, false);
            }
            else
            {
                this._flex.SetCol("NM_EVITEM1", "항목레벨1", 100, false);
                this._flex.SetCol("NM_EVITEM2", "항목레벨2", 100, false);
                this._flex.SetCol("NM_EVITEM3", "항목레벨3", 100, false);
                this._flex.SetCol("DC_INDEX", "내 용", 150, false);
            }

            this._flex.SetCol("RT_WEIGHT", "가중치", 60, false, typeof(decimal));
            this._flex.SetCol("PT_SCORE", "점수", 60, false, typeof(decimal));
            this._flex.SetCol("CD_GRADE", "등급", 80, false);
            this._flex.SetCol("PT_HSCORE", "환산점수", 80, false, typeof(decimal));

            this._flex.SetDataMap("CD_GRADE", dataTable, "CODE", "NAME");

            this._flex.AllowCache = false;
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.AllowMerging = AllowMergingEnum.RestrictRows;

            this._flex.Cols["NM_EMPM"].AllowMerging = true;
            this._flex.Cols["NM_EMPAN"].AllowMerging = true;

            if (this._평가유형 == "300") return;

            this._flex.Cols["NM_EVITEM1"].AllowMerging = true;
            this._flex.Cols["NM_EVITEM2"].AllowMerging = true;
        }

        private void SearchData()
        {
            DataTable dt;

            try
            {
                dt = DBHelper.GetDataTable("SP_CZ_HR_PEVALU_RESULT_RPT_SUB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                              this._평가코드,
                                                                                              this._평가유형,
                                                                                              this._평가차수,
                                                                                              this._평가그룹,
                                                                                              this._피평가자 });

                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    decimal ptScore = 0;
                    decimal cdGrade = 0;

                    foreach (DataRow dr in this._flex.DataTable.Rows)
                    {
                        if (this._평가유형 == "300")
                            ptScore += (D.GetDecimal(dr["PT_SCORE"]) * D.GetDecimal(dr["RT_WEIGHT"]));
                        else
                            cdGrade += (D.GetDecimal(dr["PT_HSCORE"]) * D.GetDecimal(dr["RT_WEIGHT"]));
                    }

                    this._flex[this._flex.Rows.Fixed - 1, "PT_SCORE"] = string.Format("{0:0.0000}", ptScore);
                    this._flex[this._flex.Rows.Fixed - 1, "CD_GRADE"] = string.Format("{0:0.0000}", cdGrade);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
