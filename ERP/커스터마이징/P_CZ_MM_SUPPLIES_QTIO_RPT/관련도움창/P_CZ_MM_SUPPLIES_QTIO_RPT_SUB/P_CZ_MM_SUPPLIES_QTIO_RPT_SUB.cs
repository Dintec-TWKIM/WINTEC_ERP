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
	public partial class P_CZ_MM_SUPPLIES_QTIO_RPT_SUB : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_MM_SUPPLIES_QTIO_RPT_SUB_BIZ _biz = new P_CZ_MM_SUPPLIES_QTIO_RPT_SUB_BIZ();
		public P_CZ_MM_SUPPLIES_QTIO_RPT_SUB()
		{
			InitializeComponent();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("CD_ITEM", "품목코드", 50);
			this._flex.SetCol("NM_ITEM", "품목명", 200, true);
			this._flex.SetCol("MAKER", "MAKER", 50, true);
			this._flex.SetCol("NO_MODEL", "MODEL NO", 100, true);
			this._flex.SetCol("UNIT", "단위", 50, true);
			this._flex.SetCol("CD_DEPT", "부서코드", 100, true);
			this._flex.SetCol("NM_DEPT", "담당부서명", 100);

			this._flex.Cols["CD_ITEM"].Visible = false;
			this._flex.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex.SetDataMap("UNIT", MA.GetCode("MA_B000004", false), "CODE", "NAME");
			this._flex.SetCodeHelpCol("CD_DEPT", HelpID.P_MA_DEPT_SUB, ShowHelpEnum.Always, new string[] { "CD_DEPT", "NM_DEPT" }, new string[] { "CD_DEPT", "NM_DEPT" });
			this._flex.SetDummyColumn(new string[] { "S" });
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.btn조회.Click += new EventHandler(this.Btn조회_Click);
			this.btn추가.Click += new EventHandler(this.Btn추가_Click);
			this.btn삭제.Click += new EventHandler(this.Btn삭제_Click);
			this.btn저장.Click += new EventHandler(this.Btn저장_Click);
			this.btn닫기.Click += new EventHandler(this.Btn닫기_Click);
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt;
			try
			{
				dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
													 this.txt품목명.Text,
													 this.txt모델번호.Text });
				this._flex.Binding = dt;
				if (dt == null || dt.Rows.Count == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
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

				if (!this._biz.Save(this._flex.GetChanges())) return;

				this._flex.AcceptChanges();

				Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
				this.Btn조회_Click(null, null);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
				this.Btn조회_Click(null, null);
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
