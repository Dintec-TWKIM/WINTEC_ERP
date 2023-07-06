using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using DevExpress.Data.PivotGrid;

namespace cz
{
    public partial class P_CZ_MM_MONTHLY_STOCK_PIVOT : PageBase
    {
        P_CZ_MM_MONTHLY_STOCK_PIVOT_BIZ _biz = new P_CZ_MM_MONTHLY_STOCK_PIVOT_BIZ();

        public P_CZ_MM_MONTHLY_STOCK_PIVOT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitPivot();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp기준년월.Mask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dtp기준년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp기준년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            this.cbo계정구분.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000010");
            this.cbo계정구분.ValueMember = "CODE";
            this.cbo계정구분.DisplayMember = "NAME";

            this.cbo계정구분.SelectedValue = "009";
        }

        private void InitPivot()
        {
            this._pivot.SetStart();

            this._pivot.AddPivotField("CD_ITEM", "재고번호", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_CLS_S", "소분류", 80, true, PivotArea.FilterArea);

            this._pivot.AddPivotField("SORT", "순서", 1, true, PivotArea.RowArea);
            this._pivot.AddPivotField("NM_CLS_L", "대분류", 120, true, PivotArea.RowArea);
            this._pivot.AddPivotField("NM_CLS_M", "중분류", 120, true, PivotArea.RowArea);

            this._pivot.AddPivotField("AM_INV", "전체금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_GR", "입고금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_GI", "출고금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_PO", "발주금액(미입고)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_SO", "미출고금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_STOCK_MONTH", "수주금액(월별)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_PO_MONTH", "발주금액(월별)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_AVL", "가용금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_BAD_3MONTH", "미판매금액(3개월)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_BAD_3MONTH", "미판매비율(3개월)", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_BAD_6MONTH", "미판매금액(6개월)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_BAD_6MONTH", "미판매비율(6개월)", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_BAD_12MONTH", "미판매금액(12개월)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_BAD_12MONTH", "미판매비율(12개월)", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this._pivot.AddPivotField("QT_INV", "전체수량(현재고)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_SO", "미출고수량", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_AVL", "가용수량", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_HOLD", "입고예약수량", 110, true, PivotArea.DataArea);
			this._pivot.AddPivotField("AM_HOLD", "입고예약금액", 110, false, PivotArea.DataArea);
			this._pivot.AddPivotField("QT_STOCK_MONTH", "수주수량(월별)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_BOOK_MONTH", "출고예약수량(월별)", 110, true, PivotArea.DataArea);
			this._pivot.AddPivotField("AM_BOOK_MONTH", "출고예약금액(월별)", 110, false, PivotArea.DataArea);
			this._pivot.AddPivotField("QT_PO", "발주수량(미입고)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_PO_MONTH", "발주수량(월별)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_GR", "입고수량", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_GI", "출고수량", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_BAD", "악성수량", 110, true, PivotArea.DataArea);
			this._pivot.AddPivotField("AM_BAD", "악성금액", 110, false, PivotArea.DataArea);
			this._pivot.AddPivotField("QT_BAD_3MONTH", "미판매수량(3개월)", 110, false, PivotArea.DataArea);
			this._pivot.AddPivotField("QT_BAD_6MONTH", "미판매수량(6개월)", 110, false, PivotArea.DataArea);
			this._pivot.AddPivotField("QT_BAD_12MONTH", "미판매수량(12개월)", 110, false, PivotArea.DataArea);
			
			//this._pivot.AddPivotField("QT_INV_BEFORE", "전체수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_INV_BEFORE", "전체금액(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_SO_BEFORE", "수주수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_SO_BEFORE", "수주금액(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_AVL_BEFORE", "가용수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_AVL_BEFORE", "가용금액(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_PO_BEFORE", "발주수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_PO_BEFORE", "발주금액(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_GR_BEFORE", "입고수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_GR_BEFORE", "입고금액(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_GI_BEFORE", "출고수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_GI_BEFORE", "출고금액(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_BAD_BEFORE", "악성수량(전월)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_BAD_BEFORE", "악성금액(전월)", 110, false, PivotArea.DataArea);

			//this._pivot.AddPivotField("QT_INV_DIFF", "전체수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_INV_DIFF", "전체금액(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_SO_DIFF", "수주수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_SO_DIFF", "수주금액(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_AVL_DIFF", "가용수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_AVL_DIFF", "가용금액(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_PO_DIFF", "발주수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_PO_DIFF", "발주금액(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_GR_DIFF", "입고수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_GR_DIFF", "입고금액(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_GI_DIFF", "출고수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_GI_DIFF", "출고금액(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("QT_BAD_DIFF", "악성수량(차이)", 110, false, PivotArea.DataArea);
			//this._pivot.AddPivotField("AM_BAD_DIFF", "악성금액(차이)", 110, false, PivotArea.DataArea);

			this._pivot.PivotGridControl.Fields["QT_INV"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_INV"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_INV"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_INV"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_AVL"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_AVL"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_AVL"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_AVL"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_HOLD"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_HOLD"].CellFormat.FormatString = "#0.##";
			this._pivot.PivotGridControl.Fields["AM_HOLD"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["AM_HOLD"].CellFormat.FormatString = "#,###,###,###,##0.##";
			this._pivot.PivotGridControl.Fields["QT_STOCK_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_STOCK_MONTH"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_STOCK_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_STOCK_MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_BOOK_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_BOOK_MONTH"].CellFormat.FormatString = "#0.##";
			this._pivot.PivotGridControl.Fields["AM_BOOK_MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["AM_BOOK_MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
			this._pivot.PivotGridControl.Fields["QT_PO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_PO"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_PO_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_PO_MONTH"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_PO_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_PO_MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_GR"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_GR"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_GR"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_GR"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_GI"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_GI"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["AM_GI"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_GI"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["QT_BAD"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_BAD"].CellFormat.FormatString = "#0.##";
			this._pivot.PivotGridControl.Fields["AM_BAD"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["AM_BAD"].CellFormat.FormatString = "#,###,###,###,##0.##";
			this._pivot.PivotGridControl.Fields["QT_BAD_3MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["QT_BAD_3MONTH"].CellFormat.FormatString = "#0.##";
			this._pivot.PivotGridControl.Fields["AM_BAD_3MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["AM_BAD_3MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
			this._pivot.PivotGridControl.Fields["RT_BAD_3MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["RT_BAD_3MONTH"].CellFormat.FormatString = "#,###,###,###,##0.00%";
			this._pivot.PivotGridControl.Fields["QT_BAD_6MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["QT_BAD_6MONTH"].CellFormat.FormatString = "#0.##";
			this._pivot.PivotGridControl.Fields["AM_BAD_6MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["AM_BAD_6MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
			this._pivot.PivotGridControl.Fields["RT_BAD_6MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["RT_BAD_6MONTH"].CellFormat.FormatString = "#,###,###,###,##0.00%";
			this._pivot.PivotGridControl.Fields["QT_BAD_12MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["QT_BAD_12MONTH"].CellFormat.FormatString = "#0.##";
			this._pivot.PivotGridControl.Fields["AM_BAD_12MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["AM_BAD_12MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
			this._pivot.PivotGridControl.Fields["RT_BAD_12MONTH"].CellFormat.FormatType = FormatType.Numeric;
			this._pivot.PivotGridControl.Fields["RT_BAD_12MONTH"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            //this._pivot.PivotGridControl.Fields["QT_INV_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_INV_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_INV_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_INV_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_AVL_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_AVL_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_AVL_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_AVL_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_SO_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_SO_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_SO_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_SO_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_PO_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_PO_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_PO_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_PO_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_GR_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_GR_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_GR_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_GR_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_GI_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_GI_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_GI_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_GI_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_BAD_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_BAD_BEFORE"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_BAD_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_BAD_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";

            //this._pivot.PivotGridControl.Fields["QT_INV_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_INV_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_INV_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_INV_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_AVL_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_AVL_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_AVL_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_AVL_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_SO_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_SO_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_SO_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_SO_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_PO_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_PO_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_PO_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_PO_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_GR_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_GR_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_GR_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_GR_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_GI_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_GI_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_GI_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_GI_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";
            //this._pivot.PivotGridControl.Fields["QT_BAD_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["QT_BAD_DIFF"].CellFormat.FormatString = "#0.##";
            //this._pivot.PivotGridControl.Fields["AM_BAD_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            //this._pivot.PivotGridControl.Fields["AM_BAD_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this._pivot.PivotGridControl.Fields["SORT"].SortMode = PivotSortMode.Value;


            this._pivot.SetEnd();
        }

        private void InitEvent()
        {
            this.ctx대분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx중분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx소분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.btn도움말.Click += new EventHandler(this.btn도움말_Click);

			this._pivot.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
		}

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (string.IsNullOrEmpty(this.dtp기준년월.Text))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl기준년월.Text);
                return false;
            }

            return true;
        }


        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                MsgControl.ShowMsg("조회중입니다. \r\n잠시만 기다려주세요!");

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.dtp기준년월.Text,
                                                     this.cbo계정구분.SelectedValue,
                                                     this.ctx대분류.CodeValue,
                                                     this.ctx중분류.CodeValue,
                                                     this.ctx소분류.CodeValue,
                                                     this.txt재고코드.Text,
                                                     (this.chk재고수량0제외.Checked == true ? "Y" : "N") });

                dt.TableName = this.PageID;
                this._pivot.DataSource = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_CODE_SUB)
                {
                    if (e.ControlName == this.ctx대분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                        return;
                    }
                    else if (e.ControlName == this.ctx중분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx대분류.CodeValue);
                        return;
                    }
                    else if (e.ControlName == this.ctx소분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx중분류.CodeValue);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void PivotGridControl_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
		{
			PivotDrillDownDataSource drillDownDataSource;
			decimal num1, num2;

			try
			{
				drillDownDataSource = e.CreateDrillDownDataSource();
				num1 = 0;
				num2 = 0;

				switch (e.DataField.FieldName)
				{
					case "RT_BAD_3MONTH":
						for (int index = 0; index < drillDownDataSource.RowCount; index++)
						{
							num1 += D.GetDecimal(drillDownDataSource[index]["AM_AVL"]);
							num2 += D.GetDecimal(drillDownDataSource[index]["AM_BAD_3MONTH"]);
						}
						e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
						break;
					case "RT_BAD_6MONTH":
						for (int index = 0; index < drillDownDataSource.RowCount; index++)
						{
							num1 += D.GetDecimal(drillDownDataSource[index]["AM_AVL"]);
							num2 += D.GetDecimal(drillDownDataSource[index]["AM_BAD_6MONTH"]);
						}
						e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
						break;
					case "RT_BAD_12MONTH":
						for (int index = 0; index < drillDownDataSource.RowCount; index++)
						{
							num1 += D.GetDecimal(drillDownDataSource[index]["AM_AVL"]);
							num2 += D.GetDecimal(drillDownDataSource[index]["AM_BAD_12MONTH"]);
						}
						e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn도움말_Click(object sender, EventArgs e)
        {
            string message;

            try
            {
                message = @"※ 컬럼 설명
전체수량(현재고) : 재고창고에 있는 수량 (당해 기초재고 + 당해 입고수량 - 당해 출고수량)
전체금액 : 전체수량 * 재고평균단가
미출고수량 : 재고예약수량 - (재고포장수량 + 재고출고수량)
미출고금액 : 미출고수량 * 재고평균단가
가용수량 : 전체수량 - 미출고수량
가용금액 : 가용수량 * 재고평균단가
입고예약수량 : 입고예약수량
입고예약금액 : 입고예약수량 * 재고평균단가
수주수량(월별) : 당월 수주 받은 재고 수량
수주금액(월별) : 당월 수주 받은 재고 수량 * 재고평균단가
출고예약수량(월별) : 당월 출고 예약 수량
출고예약금액(월별) : 당월 출고 예약 수량 * 재고평균단가
발주수량(미입고) : 발주수량 - 입고수량
발주금액(미입고) : 발주수량(미입고) * 발주단가
발주수량(월별) : 당월 발주 수량
발주금액(월별) : 당월 발주 금액
입고수량 : 당월 입고 수량
입고금액 : 당월 입고 수량 * 입고 단가
출고수량 : 당월 출고 수량
출고금액 : 당월 출고 수량 * 출고 단가 
악성수량 : 등록일자가 1년 초과, 최근 1년 동안 예약 수량이 0 인 수량 (전체수량)
악성금액 : 등록일자가 1년 초과, 최근 1년 동안 예약 수량이 0 인 금액 (전체금액)
미판매수량(3개월) : 3개월 초과된 입고건 존재, 최근 3개월 동안 예약 수량이 0 인 수량 (가용수량)
미판매금액(3개월) : 3개월 초과된 입고건 존재, 최근 3개월 동안 예약 수량이 0 인 금액 (가용금액)
미판매수량(6개월) : 6개월 초과된 입고건 존재, 최근 6개월 동안 예약 수량이 0 인 수량 (가용수량)
미판매금액(6개월) : 6개월 초과된 입고건 존재, 최근 6개월 동안 예약 수량이 0 인 금액 (가용금액)
미판매수량(12개월) : 12개월 초과된 입고건 존재, 최근 12개월 동안 예약 수량이 0 인 수량 (가용수량)
미판매금액(12개월) : 12개월 초과된 입고건 존재, 최근 12개월 동안 예약 수량이 0 인 금액 (가용금액)";

                this.ShowMessage(message);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}