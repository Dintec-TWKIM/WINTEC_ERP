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
	public partial class P_CZ_PR_MATCHING_GRADE_SUB : Duzon.Common.Forms.CommonDialog
	{
        P_CZ_PR_MATCHING_GRADE_SUB_BIZ _biz = new P_CZ_PR_MATCHING_GRADE_SUB_BIZ();

        public P_CZ_PR_MATCHING_GRADE_SUB()
		{
			InitializeComponent();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_CLEAR_GRP", "클리어런스그룹", 100, true);
            this._flex.SetCol("CD_GRADE", "등급코드", 100, true);
            this._flex.SetCol("IDX_SPEC", "순번", 100);
            this._flex.SetCol("QT_SPEC", "치수", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flex.SetDummyColumn(new string[] { "S" });
            this._flex.SetDataMap("CD_CLEAR_GRP", MA.GetCode("CZ_WIN0013"), "CODE", "NAME");
            this._flex.SetDataMap("CD_GRADE", MA.GetCodeUser(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" }, 
                                                             new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" }), "CODE", "NAME");

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

		private void InitEvent()
		{
			this.btn조회.Click += new EventHandler(this.btn조회_Click);
			this.btn추가.Click += Btn추가_Click;
			this.btn저장.Click += new EventHandler(this.btn저장_Click);
			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
			this.btn닫기.Click += new EventHandler(this.btn닫기_Click);
		}

        private void Btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

                if (this._flex.DataTable == null || this._flex.DataTable.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.IsDataChanged) return;

                if (this._flex.DataTable.Select("ISNULL(CD_CLEAR_GRP, '') = ''").Length > 0)
				{
                    Global.MainFrame.ShowMessage("클리어런스 그룹이 입력되어 있지 않은 항목이 있습니다.");
                    return;
				}

                if (this._flex.DataTable.Select("ISNULL(CD_GRADE, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("등급코드가 입력되어 있지 않은 항목이 있습니다.");
                    return;
                }

                if (!this._biz.Save(this._flex.GetChanges()))
                    return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }
                }
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
