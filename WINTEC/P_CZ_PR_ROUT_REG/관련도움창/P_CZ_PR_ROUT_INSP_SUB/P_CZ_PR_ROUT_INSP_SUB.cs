using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_ROUT_INSP_SUB : Duzon.Common.Forms.CommonDialog
	{
        P_CZ_PR_ROUT_INSP_SUB_BIZ _biz = new P_CZ_PR_ROUT_INSP_SUB_BIZ();

        public P_CZ_PR_ROUT_INSP_SUB()
		{
			InitializeComponent();
        }

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, true);

            this._flex.SetCol("CD_ITEM", "품목코드", 100);
            this._flex.SetCol("NM_ITEM", "품목명", 100);
            this._flex.SetCol("CD_OP", "OP코드", 100);
            this._flex.SetCol("CD_WCOP", "공정코드", 100);
            this._flex.SetCol("NM_OP", "공정명", 100);
            this._flex.SetCol("DC_ITEM", "구분", 100);
            this._flex.SetCol("CD_MEASURE", "측정장비", 100, true);
            this._flex.SetCol("DC_MEASURE", "측정장비", 100);
			this._flex.SetCol("DC_LOCATION", "위치", 100);
			this._flex.SetCol("DC_SPEC", "사양", 100);
			this._flex.SetCol("QT_MIN", "최소값", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex.SetCol("QT_MAX", "최대값", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("TP_DATA", "대표값유형", 100);
            this._flex.SetCol("CNT_INSP", "측정포인트", 100);
            this._flex.SetCol("DC_RMK", "비고", 100);
			this._flex.SetCol("YN_CERT", "성적서여부", 60, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("YN_USE", "사용유무", 60, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("YN_SAMPLING", "샘플링검사여부", 60, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("YN_ASSY", "현합치수", 60, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_CLEAR_GRP", "클리어런스그룹", 100, true);

            this._flex.SetDataMap("CD_MEASURE", Global.MainFrame.GetComboDataCombine("S;CZ_WIN0012"), "CODE", "NAME");
            this._flex.SetDataMap("TP_DATA", MA.GetCodeUser(new string[] { "MIN", "MAX" }, new string[] { "최소값", "최대값" }), "CODE", "NAME");
            this._flex.SetDataMap("CNT_INSP", MA.GetCodeUser(new string[] { "1", "2", "3", "4", "5" }, new string[] { "1", "2", "3", "4", "5" }), "CODE", "NAME");
            this._flex.SetDataMap("CD_CLEAR_GRP", MA.GetCode("CZ_WIN0013"), "CODE", "NAME");

            this._flex.SetDummyColumn("YN_USE");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

			this.ctx품목코드.QueryBefore += Ctx품목코드_QueryBefore;
			this.ctx공정.QueryBefore += Ctx공정_QueryBefore;

			this._flex.AfterEdit += _flex_AfterEdit;
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;
                if (flexGrid.Cols[e.Col].Name != "YN_USE") return;

                query = @"UPDATE A
                          SET A.YN_USE = '{7}',
                              A.ID_UPDATE = '{8}',
                              A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                          FROM CZ_PR_ROUT_INSP A
                          WHERE A.CD_COMPANY = '{0}'
                          AND A.CD_PLANT = '{1}'
                          AND A.CD_ITEM = '{2}'
                          AND A.NO_OPPATH = '{3}'
                          AND A.CD_OP = '{4}'
                          AND A.CD_WCOP = '{5}'
                          AND A.NO_INSP = '{6}'";

                DBHelper.ExecuteScalar(string.Format(query, new object[] { flexGrid["CD_COMPANY"].ToString(),
                                                                           flexGrid["CD_PLANT"].ToString(),
                                                                           flexGrid["CD_ITEM"].ToString(),
                                                                           flexGrid["NO_OPPATH"].ToString(),
                                                                           flexGrid["CD_OP"].ToString(),
                                                                           flexGrid["CD_WCOP"].ToString(),
                                                                           flexGrid["NO_INSP"].ToString(),
                                                                           flexGrid["YN_USE"].ToString(),
                                                                           Global.MainFrame.LoginInfo.UserID }));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Ctx공정_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
            try
            {
                e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                e.HelpParam.P20_CD_WC = string.Empty;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		private void Ctx품목코드_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
            try
            {
                e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		protected override void InitPaint()
		{
			base.InitPaint();

            this.cbo공장.DataSource = Global.MainFrame.GetComboDataCombine("N;MA_PLANT");
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
        }

		private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.cbo공장.SelectedValue,
                                                                     this.ctx품목코드.CodeValue,
                                                                     string.Empty,
                                                                     this.txtOp코드.Text,
                                                                     this.ctx공정.CodeValue,
                                                                     (this.chk사용항목만.Checked == true ? "Y" : "N") });

                if (this._flex.DataTable == null || this._flex.DataTable.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }

                this.btn저장.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.Row < 0) return;

                this._flex.RemoveItem(this._flex.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                if (!this._flex.IsDataChanged) return;

                dt = this._flex.GetChanges();

                if (this._flex.DataTable.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows)
                                                .Where(x => x["YN_USE"].ToString() == "Y")
                                                .GroupBy(x => new { key1 = x["CD_ITEM"].ToString(),
                                                                    key2 = x["NO_OPPATH"].ToString(),
                                                                    key3 = x["CD_OP"].ToString(),
                                                                    key4 = x["CD_WCOP"].ToString() }, y => D.GetDecimal(y["CNT_INSP"]), (x, y) => new { key = x, sum = y.Sum() })
                                                .Where(x => x.sum > 15).Count() > 0)
				{
                    Global.MainFrame.ShowMessage("총 15 번까지만 측정 가능 합니다.");
                    return;
                }

                if (dt.Select("ISNULL(DC_ITEM, '') = ''").Length > 0)
				{
                    Global.MainFrame.ShowMessage("구분이 입력되지 않은 행이 존재 합니다.");
                    return;
				}

                if (dt.Select("ISNULL(DC_MEASURE, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("측정장비가 입력되지 않은 행이 존재 합니다.");
                    return;
                }

				if (dt.Select("ISNULL(CD_MEASURE, '') = ''").Length > 0)
				{
					Global.MainFrame.ShowMessage("측정장비가 입력되지 않은 행이 존재 합니다.");
					return;
				}

				if (dt.Select("ISNULL(DC_LOCATION, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("위치가 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (dt.Select("ISNULL(DC_SPEC, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("사양이 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (dt.Select("ISNULL(TP_DATA, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("대표값유형이 입력되지 않은 행이 존재 합니다.");
                    return;
                }

                if (!this._biz.Save(dt)) return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}