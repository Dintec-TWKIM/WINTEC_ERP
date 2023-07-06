using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_PR_OPOUT_REG_SUB_BIZ _biz = new P_CZ_PR_OPOUT_REG_SUB_BIZ();
        private DataRow[] _ResultObject;

        public P_CZ_PR_OPOUT_REG_SUB()
        {
            InitializeComponent();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_WC", "작업장", 60, false);
            this._flex.SetCol("NM_WC", "작업장명", 120, false);
            this._flex.SetCol("CD_WCOP", "공정", 80, false);
            this._flex.SetCol("NM_OP", "공정명", 120, false);
            this._flex.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flex.SetCol("NM_TP_WO", "오더형태", 100, false);
            this._flex.SetCol("CD_OP", "OP.", 40, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex.SetCol("STND_ITEM", "규격", 120, false);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex.SetCol("UNIT_IM", "단위", 40, false);
            this._flex.SetCol("QT_WO", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_OUTPO", "발주대상수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_OPOUT_PO", "발주수량(변환전)", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_APPLY_YET", "미발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_APPLY", "요청수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("DT_REL", "시작일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_DUE", "종료일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_LOT", "LOT번호", 70, 8, false);
            this._flex.SetCol("NM_FG_SERNO", "LOT,S/N관리", false);
            this._flex.SetCol("NM_GRP_ITEM", "품목군", false);
            this._flex.SetCol("DC_RMK1", "비고1", false);
            this._flex.SetCol("NO_WORK", "작업실적번호", false);
            this._flex.SetCol("NO_PJT", "프로젝트번호", 100, false);
            this._flex.SetCol("NM_PJT", "프로젝트명", 100, false);
            this._flex.SetCol("QT_PR", "외주요청수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);

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
                                                                     this.chK작업지시비고적용.Checked == true ? "Y" : "N",
                                                                     Global.MainFrame.LoginInfo.UserID });
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
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length > 0)
                {
                    this._ResultObject = dataRowArray;
                }
                else this._ResultObject = null;

                this.DialogResult = DialogResult.OK;
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
