using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
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
	public partial class P_CZ_PR_ROUT_BATCH_GROUP_SUB : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_PR_ROUT_BATCH_GROUP_SUB_BIZ _biz = new P_CZ_PR_ROUT_BATCH_GROUP_SUB_BIZ();
		public P_CZ_PR_ROUT_BATCH_GROUP_SUB()
		{
			InitializeComponent();

			this.InitGrid();
			this.InitEvent();

			this.조회();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("NM_GPE", "배치그룹명", 150, true);
			this._flex.SetCol("QT_CAPAMIN", "최소투입수량", 100, true, typeof(int), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_CAPAMAX", "최대투입수량", 100, true, typeof(int), FormatTpType.QUANTITY);
			this._flex.SetCol("DC_RMK", "비고", 100, true);

			this._flex.SetDummyColumn(new string[] { "S" });
			this._flex.ExtendLastCol = true;
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.btn추가.Click += new EventHandler(this.Btn추가_Click);
			this.btn삭제.Click += new EventHandler(this.Btn삭제_Click);
			this.btn저장.Click += new EventHandler(this.Btn저장_Click);
			this.btn닫기.Click += new EventHandler(this.Btn닫기_Click);
		}

		private void 조회()
		{
			DataTable dt;
			try
			{
				dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode });
				this._flex.Binding = dt;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex.Rows.Add();
				this._flex.Row = this._flex.Rows.Count - 1;


				this._flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;

				this._flex.AddFinished();
				this._flex.Focus();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			try
			{
				dataRowArray = this._flex.DataTable.Select("S = 'Y'");
				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				foreach (DataRow dr in dataRowArray)
				{
					dr.Delete();
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn저장_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex.IsDataChanged) return;

				foreach (DataRow dr in _flex.GetChanges().Rows)
				{
					if (dr.RowState != DataRowState.Deleted)
					{
						if (string.IsNullOrEmpty(dr["NM_GPE"].ToString()))
						{
							Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "배치그룹명");
							return;
						}
					}
				}

				if (!this._biz.Save(this._flex.GetChanges())) return;

				this._flex.AcceptChanges();

				Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
				this.조회();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn닫기_Click(object sender, EventArgs e)
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
