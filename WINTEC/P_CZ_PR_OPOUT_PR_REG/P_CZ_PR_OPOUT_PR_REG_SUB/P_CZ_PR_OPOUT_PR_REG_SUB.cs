using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_PR_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_PR_OPOUT_PR_REG_SUB_BIZ _biz = new P_CZ_PR_OPOUT_PR_REG_SUB_BIZ();
        private DataRow[] _ResultObject;
        private string strExceptList = "";
        public P_CZ_PR_OPOUT_PR_REG_SUB(ArrayList al)
        {
            InitializeComponent();

            foreach (string str in al)
            {
                P_CZ_PR_OPOUT_PR_REG_SUB dialog = this;
                dialog.strExceptList = dialog.strExceptList + str + "|";
            }

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flex.SetCol("CD_OP", "OP.", 40, false);
            this._flex.SetCol("CD_WC", "작업장", 60, false);
            this._flex.SetCol("NM_WC", "작업장명", 120, false);
            this._flex.SetCol("CD_WCOP", "공정", 80, false);
            this._flex.SetCol("NM_OP", "공정명", 120, false);
            this._flex.SetCol("NM_TP_WO", "오더형태", 100, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex.SetCol("STND_ITEM", "규격", 120, false);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex.SetCol("UNIT_IM", "단위", 40, false);
            this._flex.SetCol("QT_WO", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_START", "입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_OUTPR", "요청대상수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_OPOUT_PR", "기요청수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_APPLY_YET", "미요청수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_APPLY", "요청수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("DT_REL", "시작일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_DUE", "종료일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.SetDummyColumn("CHK");
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp작업기간.StartDateToString = Global.MainFrame.GetStringToday.Substring(0, 6) + "01";
            this.dtp작업기간.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitEvent()
        {
            this.btn검색.Click += new EventHandler(this.btn검색_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        private void btn검색_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     Global.MainFrame.LoginInfo.CdPlant,
                                                                     D.GetString(this.txt작업지시번호.Text),
                                                                     this.dtp작업기간.StartDateToString,
                                                                     this.dtp작업기간.EndDateToString,
                                                                     this.strExceptList});
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length > 0)
                {
                    this._ResultObject = dataRowArray;
                    this.DialogResult = DialogResult.OK;
                }
				else
				{
                    Global.MainFrame.ShowMessage("체크된 건이 없습니다.");
                    this._ResultObject = null;
                }

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
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

        public DataRow[] ReturnValues
        {
            get
            {
                return this._ResultObject;
            }
        }
    }
}
