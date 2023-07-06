using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using C1.Win.C1FlexGrid;
using Dintec;
using Duzon.ERPU;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;

namespace cz
{
    public partial class P_CZ_SA_STOCK_RPT : PageBase
    {
        P_CZ_SA_STOCK_RPT_BIZ _biz = new P_CZ_SA_STOCK_RPT_BIZ();

        private enum 탭구분
        {
            수주번호,
            재고코드,
            리스트,
            매입처별수주현황
        }

        public P_CZ_SA_STOCK_RPT()
        {
            StartUp.Certify(this);
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
            #region 수주번호별

            #region Header
            this._flex수주번호별H.DetailGrids = new FlexGrid[] { this._flex수주번호별L };

            this._flex수주번호별H.BeginSetting(1, 1, false);

            this._flex수주번호별H.SetCol("NO_SO", "수주번호", 100);
            this._flex수주번호별H.SetCol("NM_EMP", "영업담당자", 100);
            this._flex수주번호별H.SetCol("NM_SALEGRP", "영업그룹", 100);
            this._flex수주번호별H.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호별H.SetCol("DT_CONTRACT", "계약일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호별H.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호별H.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호별H.SetCol("NM_PARTNER", "매출처", 100);
            this._flex수주번호별H.SetCol("NM_VESSEL", "호선명", 100);
            this._flex수주번호별H.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별H.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별H.SetCol("QT_HOLD", "입고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별H.SetCol("QT_GIR_STOCK", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별H.SetCol("QT_IO", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별H.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별H.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별H.SetCol("AM_BOOK", "출고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별H.SetCol("AM_HOLD", "입고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별H.SetCol("AM_GIR", "의뢰금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별H.SetCol("AM_IO", "출고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별H.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex수주번호별H.SettingVersion = "1.0.0.0";
            this._flex수주번호별H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex수주번호별L.BeginSetting(1, 1, false);

            this._flex수주번호별L.SetCol("NO_LINE", "수주항번", 100);
            this._flex수주번호별L.SetCol("CD_ITEM", "재고코드", 100);
            this._flex수주번호별L.SetCol("NM_ITEM", "품목명", 120);
            this._flex수주번호별L.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex수주번호별L.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flex수주번호별L.SetCol("CD_SPEC", "U코드(견적)", 100);
            this._flex수주번호별L.SetCol("STND_DETAIL_ITEM", "U코드(품목)", 100);
            this._flex수주번호별L.SetCol("STND_ITEM", "파트번호", 100);
            this._flex수주번호별L.SetCol("MAT_ITEM", "아이템번호", 100);
            this._flex수주번호별L.SetCol("NM_CLS_L", "대분류", 100);
            this._flex수주번호별L.SetCol("NM_CLS_M", "중분류", 100);
            this._flex수주번호별L.SetCol("NM_CLS_S", "소분류", 100);
            this._flex수주번호별L.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별L.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별L.SetCol("QT_HOLD", "입고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별L.SetCol("QT_GIR_STOCK", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별L.SetCol("QT_IO", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별L.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호별L.SetCol("UM_STOCK", "재고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex수주번호별L.SetCol("UM_SO", "수주단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex수주번호별L.SetCol("UM_GIR", "의뢰단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex수주번호별L.SetCol("UM_IO", "출고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex수주번호별L.SetCol("UM_IV", "매출단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex수주번호별L.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별L.SetCol("AM_BOOK", "출고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별L.SetCol("AM_HOLD", "입고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별L.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별L.SetCol("AM_GIR", "의뢰금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별L.SetCol("AM_IO", "출고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호별L.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex수주번호별L.SettingVersion = "1.0.0.0";
            this._flex수주번호별L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex수주번호별L.SetExceptSumCol("UM_STOCK", "UM_SO", "UM_GIR", "UM_IO", "UM_IV");
            #endregion

            #endregion

            #region 재고코드

            #region Header
            this._flex재고코드H.DetailGrids = new FlexGrid[] { this._flex재고코드L };

            this._flex재고코드H.BeginSetting(1, 1, false);

            this._flex재고코드H.SetCol("CD_ITEM", "재고코드", 100);
            this._flex재고코드H.SetCol("NM_ITEM", "품목명", 120);
            this._flex재고코드H.SetCol("STND_DETAIL_ITEM", "U코드(품목)", 100);
            this._flex재고코드H.SetCol("STND_ITEM", "파트번호", 100);
            this._flex재고코드H.SetCol("MAT_ITEM", "아이템번호", 100);
            this._flex재고코드H.SetCol("NM_CLS_L", "대분류", 100);
            this._flex재고코드H.SetCol("NM_CLS_M", "중분류", 100);
            this._flex재고코드H.SetCol("NM_CLS_S", "소분류", 100);
            this._flex재고코드H.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드H.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드H.SetCol("QT_HOLD", "입고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드H.SetCol("QT_GIR_STOCK", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드H.SetCol("QT_IO", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드H.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드H.SetCol("STAND_PRC", "평균단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_BOOK", "출고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_HOLD", "입고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_GIR", "의뢰금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_IO", "출고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드H.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex재고코드H.SettingVersion = "1.0.0.0";
            this._flex재고코드H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex재고코드L.BeginSetting(1, 1, false);

            this._flex재고코드L.SetCol("NO_FILE", "수주번호", 100);
            this._flex재고코드L.SetCol("NO_PO_PARTNER", "주문번호", 100);
            this._flex재고코드L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고코드L.SetCol("DT_CONTRACT", "계약일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고코드L.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고코드L.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고코드L.SetCol("NM_PARTNER", "매출처", 100);
            this._flex재고코드L.SetCol("NM_VESSEL", "호선명", 100);
            this._flex재고코드L.SetCol("NM_MODEL_ME", "대형엔진", 100);
            this._flex재고코드L.SetCol("NM_MODEL_GE", "중형엔진", 100);
            this._flex재고코드L.SetCol("NO_LINE", "수주항번", 100);
            this._flex재고코드L.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex재고코드L.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flex재고코드L.SetCol("CD_SPEC", "U코드(견적)", 100);
            this._flex재고코드L.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드L.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드L.SetCol("QT_HOLD", "입고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드L.SetCol("QT_GIR_STOCK", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드L.SetCol("QT_IO", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드L.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드L.SetCol("UM_STOCK", "재고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex재고코드L.SetCol("UM_SO", "수주단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex재고코드L.SetCol("UM_GIR", "의뢰단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex재고코드L.SetCol("UM_IO", "출고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex재고코드L.SetCol("UM_IV", "매출단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex재고코드L.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드L.SetCol("AM_BOOK", "출고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드L.SetCol("AM_HOLD", "입고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드L.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드L.SetCol("AM_GIR", "의뢰금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드L.SetCol("AM_IO", "출고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고코드L.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            
            this._flex재고코드L.SettingVersion = "1.0.0.0";
            this._flex재고코드L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex재고코드L.SetExceptSumCol("UM_STOCK", "UM_SO", "UM_GIR", "UM_IO", "UM_IV");
            #endregion

            #endregion

            #region 리스트
            this._flex리스트.BeginSetting(1, 1, false);

            this._flex리스트.SetCol("NO_SO", "수주번호", 100);
            this._flex리스트.SetCol("NM_EMP", "영업담당자", 100);
            this._flex리스트.SetCol("NM_SALEGRP", "영업그룹", 100);
            this._flex리스트.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("DT_CONTRACT", "계약일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("NM_PARTNER", "매출처", 100);
			this._flex리스트.SetCol("NO_IMO", "IMO 번호", false);
			this._flex리스트.SetCol("NM_VESSEL", "호선명", 100);
            this._flex리스트.SetCol("SEQ_SO", "수주항번", 100);
            this._flex리스트.SetCol("CD_ITEM", "재고코드", 100);
            this._flex리스트.SetCol("NM_ITEM", "품목명", 120);
            this._flex리스트.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex리스트.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flex리스트.SetCol("CD_SPEC", "U코드(견적)", 100);
            this._flex리스트.SetCol("STND_DETAIL_ITEM", "U코드(품목)", 100);
            this._flex리스트.SetCol("STND_ITEM", "파트번호", 100);
            this._flex리스트.SetCol("MAT_ITEM", "아이템번호", 100);
            this._flex리스트.SetCol("NM_CLS_L", "대분류", 100);
            this._flex리스트.SetCol("NM_CLS_M", "중분류", 100);
            this._flex리스트.SetCol("NM_CLS_S", "소분류", 100);
            this._flex리스트.SetCol("NM_PARTNER_GRP", "거래처그룹", 100);
            this._flex리스트.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("QT_HOLD", "입고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("QT_GIR_STOCK", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("QT_IO", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("UM_STOCK", "재고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex리스트.SetCol("UM_SO", "수주단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex리스트.SetCol("UM_GIR", "의뢰단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex리스트.SetCol("UM_IO", "출고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex리스트.SetCol("UM_IV", "매출단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex리스트.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_BOOK", "출고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_HOLD", "입고예약금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_GIR", "의뢰금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_IO", "출고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex리스트.SettingVersion = "1.0.0.0";
            this._flex리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex리스트.SetExceptSumCol("UM_STOCK", "UM_SO", "UM_GIR", "UM_IO", "UM_IV");
            #endregion

            #region 매입처별 수주현황
            this._flex매입처별수주현황.BeginSetting(1, 1, false);

            this._flex매입처별수주현황.SetCol("NM_CLS_M", "매입처", 100);
            this._flex매입처별수주현황.SetCol("AM_0", DateTime.Now.AddMonths(-12).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_1", DateTime.Now.AddMonths(-11).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_2", DateTime.Now.AddMonths(-10).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_3", DateTime.Now.AddMonths(-9).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_4", DateTime.Now.AddMonths(-8).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_5", DateTime.Now.AddMonths(-7).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_6", DateTime.Now.AddMonths(-6).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_7", DateTime.Now.AddMonths(-5).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_8", DateTime.Now.AddMonths(-4).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_9", DateTime.Now.AddMonths(-3).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_10", DateTime.Now.AddMonths(-2).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_11", DateTime.Now.AddMonths(-1).ToString("yyyy/MM"), 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_TOTAL", "총액(12개월)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_AVG", "평균(12개월)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("AM_CURRENT", "당월금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처별수주현황.SetCol("RT_SALES", "판매율(%)", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex매입처별수주현황.SettingVersion = "1.0.0.0";
            this._flex매입처별수주현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex매입처별수주현황.SetExceptSumCol("RT_SALES");
            #endregion
        }

        private void InitEvent()
        {
            this.bpc계정구분.QueryBefore += new BpQueryHandler(this.bpc계정구분_QueryBefore);
            this.btn재고코드엑셀검색.Click += btn재고코드엑셀검색_Click;

			this.ctx대분류.QueryBefore += Control_QueryBefore;
            this.ctx중분류.QueryBefore += Control_QueryBefore;
            this.ctx소분류.QueryBefore += Control_QueryBefore;

            this._flex수주번호별H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flex재고코드H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.ControlName == this.ctx대분류.Name)
                {
                    e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                }
                else if (e.ControlName == this.ctx중분류.Name)
                {
                    e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                    e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx대분류.CodeValue);
                }
                else if (e.ControlName == this.ctx소분류.Name)
                {
                    e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                    e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx중분류.CodeValue);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo검색일자.DataSource = MA.GetCodeUser(new string[] { "001", "002",  "003", "004", "005" }, new string[] { this.DD("수주일자"), this.DD("계약일자"), this.DD("출고일자"), this.DD("등록일자"), this.DD("매출일자") });
            this.cbo검색일자.ValueMember = "CODE";
            this.cbo검색일자.DisplayMember = "NAME";

            string query = @"SELECT CD_FLAG3 AS CODE,
									CD_FLAG3 AS NAME
							 FROM MA_CODEDTL WITH(NOLOCK)
							 WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                           @"AND CD_FIELD = 'CZ_SA00023'
							 AND ISNULL(CD_FLAG3, '') != ''
							 AND USE_YN = 'Y'
							 UNION ALL
							 SELECT CD_FLAG3 AS CODE,
									CD_FLAG3 AS NAME
							 FROM MA_CODEDTL WITH(NOLOCK)
							 WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                           @"AND CD_FIELD = 'CZ_SA00025'
							 AND ISNULL(CD_FLAG3, '') != ''
							 AND USE_YN = 'Y'";

            DataTable dt = DBHelper.GetDataTable(query);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["CODE"] = "";
            dt.Rows[0]["NAME"] = "";

            this.cbo수주번호.DataSource = dt;
            this.cbo수주번호.ValueMember = "CODE";
            this.cbo수주번호.DisplayMember = "NAME";

            this.dtp검색일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
            this.dtp검색일자.EndDateToString = MainFrameInterface.GetStringToday;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;
            object[] obj;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                dt = null;
                obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                     D.GetString(this.cbo검색일자.SelectedValue),
                                     this.dtp검색일자.StartDateToString,
                                     this.dtp검색일자.EndDateToString,
                                     this.txt수주번호.Text,
                                     this.cbo수주번호.SelectedValue,
                                     this.ctx매출처.CodeValue,
                                     this.ctx호선.CodeValue,
                                     this.txt재고코드.Text,
                                     string.Empty,
                                     this.bpc계정구분.QueryWhereIn_Pipe,
                                     this.ctx대분류.CodeValue,
                                     this.ctx중분류.CodeValue,
                                     this.ctx소분류.CodeValue };

                switch((탭구분)tabControl.SelectedIndex)
                {
                    case 탭구분.수주번호:
                        dt = this._biz.SearchHeader1(obj);
                        this._flex수주번호별H.Binding = dt;
                        break;
                    case 탭구분.재고코드:
                        dt = this._biz.SearchHeader2(obj);
                        this._flex재고코드H.Binding = dt;
                        break;
                    case 탭구분.리스트:
                        dt = this._biz.SearchHeader3(obj);
                        this._flex리스트.Binding = dt;
                        break;
                    case 탭구분.매입처별수주현황:
                        dt = this._biz.SearchHeader4(obj);
                        this._flex매입처별수주현황.Binding = dt;

                        decimal 평균금액 = D.GetDecimal(this._flex매입처별수주현황.DataTable.Compute("SUM(AM_AVG)", string.Empty));
                        decimal 당월금액 = D.GetDecimal(this._flex매입처별수주현황.DataTable.Compute("SUM(AM_CURRENT)", string.Empty));

                        this._flex매입처별수주현황[this._flex매입처별수주현황.Rows.Fixed - 1, "RT_SALES"] = string.Format("{0:" + this._flex매입처별수주현황.Cols["RT_SALES"].Format + "}", (평균금액 == 0 ? 0 : Decimal.Round(((당월금액 / 평균금액) * 100), 0, MidpointRounding.AwayFromZero)));
                        break;
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            FlexGrid grid;
            string key, filter;
            object[] obj;
            try
            {
                grid = ((FlexGrid)sender);
                dt = null;

                if (grid.Name == this._flex수주번호별H.Name)
                {
                    key = D.GetString(grid["NO_SO"]);
                    filter = "NO_SO = '" + key + "'";
                }
                else
                {
                    key = D.GetString(grid["CD_ITEM"]);
                    filter = "CD_ITEM = '" + key + "'";
                }

                if (grid.DetailQueryNeed == true)
                {
                    obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                         key,
                                         D.GetString(this.cbo검색일자.SelectedValue),
                                         this.dtp검색일자.StartDateToString,
                                         this.dtp검색일자.EndDateToString,
                                         this.txt수주번호.Text,
                                         this.cbo수주번호.SelectedValue,
                                         this.ctx매출처.CodeValue,
                                         this.ctx호선.CodeValue,
                                         this.txt재고코드.Text,
                                         this.bpc계정구분.QueryWhereIn_Pipe };

                    switch ((탭구분)tabControl.SelectedIndex)
                    {
                        case 탭구분.수주번호:
                            dt = this._biz.SearchLine1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      key,
                                                                      D.GetString(this.cbo검색일자.SelectedValue),
                                                                      this.dtp검색일자.StartDateToString,
                                                                      this.dtp검색일자.EndDateToString,
                                                                      this.txt재고코드.Text,
                                                                      this.bpc계정구분.QueryWhereIn_Pipe,
                                                                      this.ctx대분류.CodeValue,
                                                                      this.ctx중분류.CodeValue,
                                                                      this.ctx소분류.CodeValue });
                            break;
                        case 탭구분.재고코드:
                            dt = this._biz.SearchLine2(obj);
                            break;
                    }
                }

                grid.DetailGrids[0].BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void bpc계정구분_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_CODE_SUB1)
                {
                    if (e.ControlName == this.bpc계정구분.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000010";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn재고코드엑셀검색_Click(object sender, EventArgs e)
        {
            DataTable dt;
            object[] obj;

            try
            {
                OpenFileDialog f = new OpenFileDialog
                {
                    Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx"
                };

                if (f.ShowDialog() != DialogResult.OK)
                    return;

                ExcelReader excel = new ExcelReader();
                DataTable dtExcel = excel.Read(f.FileName);

                if (dtExcel.Rows.Count == 0)
                {
                    ShowMessage("엑셀파일을 읽을 수 없습니다.");
                    return;
                }

                string codes = "";
                codes = string.Join("|", dtExcel.AsEnumerable().Select(x => x[0]).ToArray());

                dt = null;
                obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                     D.GetString(this.cbo검색일자.SelectedValue),
                                     this.dtp검색일자.StartDateToString,
                                     this.dtp검색일자.EndDateToString,
                                     this.txt수주번호.Text,
                                     this.cbo수주번호.SelectedValue,
                                     this.ctx매출처.CodeValue,
                                     this.ctx호선.CodeValue,
                                     this.txt재고코드.Text,
                                     codes,
                                     this.bpc계정구분.QueryWhereIn_Pipe };

                switch ((탭구분)tabControl.SelectedIndex)
                {
                    case 탭구분.수주번호:
                        dt = this._biz.SearchHeader1(obj);
                        this._flex수주번호별H.Binding = dt;
                        break;
                    case 탭구분.재고코드:
                        dt = this._biz.SearchHeader2(obj);
                        this._flex재고코드H.Binding = dt;
                        break;
                    case 탭구분.리스트:
                        dt = this._biz.SearchHeader3(obj);
                        this._flex리스트.Binding = dt;
                        break;
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}
