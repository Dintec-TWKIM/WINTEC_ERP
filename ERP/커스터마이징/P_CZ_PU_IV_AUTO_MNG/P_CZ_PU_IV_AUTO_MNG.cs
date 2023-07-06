using Aspose.Email.Outlook;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.Utils;
using Duzon.Windows.Print;
using DX;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PU_IV_AUTO_MNG : PageBase
	{
		P_CZ_PU_IV_AUTO_MNG_BIZ _biz = new P_CZ_PU_IV_AUTO_MNG_BIZ();
        public decimal 부가세 = 0;
        public decimal 외화금액 = 0;
        public decimal 원화금액 = 0;

        public P_CZ_PU_IV_AUTO_MNG()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
            this.InitEvent();   
        }

		protected override void InitPaint()
		{
			base.InitPaint();

            this.dtp작성일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
			this.dtp작성일자.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp입고기간.StartDateToString = "20170101";
            this.dtp입고기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp정산년월S.Text = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMM");

            DataSet ds = Global.MainFrame.GetComboData(new string[] { "N;MA_B000005",
                                                                      "N;PU_C000001",
                                                                      "N;PU_C000014" });

            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo거래처분류, new DataView(MA.GetCode("MA_B000003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            this.cbo처리여부.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "처리", "미처리" }, true);
            this.cbo처리여부.DisplayMember = "NAME";
            this.cbo처리여부.ValueMember = "CODE";

            this.cbo부대비용항목.DataSource = Global.MainFrame.FillDataTable(@"SELECT CD_ITEM, NM_ITEM, STND_ITEM, UNIT_IM 
                                                                              FROM MA_PITEM WITH(NOLOCK)
                                                                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                            @"AND CD_ITEM LIKE 'SD%'
                                                                              UNION ALL
                                                                              SELECT CD_ITEM, NM_ITEM, STND_ITEM, UNIT_IM 
                                                                              FROM MA_PITEM WITH(NOLOCK)" + Environment.NewLine +
                                                                             "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                             "AND CD_ITEM IN ('ADM005', 'ADM006', 'ADM007')");
            this.cbo부대비용항목.DisplayMember = "NM_ITEM";
            this.cbo부대비용항목.ValueMember = "CD_ITEM";

            if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
            {
                this.cur외화부대비용.ReadOnly = true;
                this.cur원화부대비용.ReadOnly = false;
            }
            else
            {
                this.cur외화부대비용.ReadOnly = false;
                this.cur원화부대비용.ReadOnly = true;
            }

            this.cbo부대비용통화명.DataSource = ds.Tables[0].Copy();
            this.cbo부대비용통화명.DisplayMember = "NAME";
            this.cbo부대비용통화명.ValueMember = "CODE";

            this.cbo통화명.DataSource = ds.Tables[0].Copy();
            this.cbo통화명.DisplayMember = "NAME";
            this.cbo통화명.ValueMember = "CODE";

            this._flex매입등록L.SetDataMap("FG_UM", ds.Tables[1], "CODE", "NAME");
            this._flex매입등록L.SetDataMap("FG_PAYMENT", ds.Tables[2], "CODE", "NAME");
        }

		private void InitGrid()
		{
            this.MainGrids = new FlexGrid[] { this._flex세금계산서H, this._flex거래명세서H, this._flex상세내역, this._flex파일번호별 };

            this._flex거래명세서H.DetailGrids = new FlexGrid[] { this._flex상세내역, this._flex파일번호별, this._flex발주내역 };
			this._flex세금계산서H.DetailGrids = new FlexGrid[] { this._flex세금계산서L };
            this._flex매입등록H.DetailGrids = new FlexGrid[] { this._flex매입등록L };
            this._flex파일번호별.DetailGrids = new FlexGrid[] { this._flex발주내역 };

            #region 거래명세서

            #region Header
            this._flex거래명세서H.BeginSetting(2, 1, false);

            this._flex거래명세서H.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
            this._flex거래명세서H.SetCol("LN_PARTNER", "매입처", 100);
            this._flex거래명세서H.SetCol("DT_MONTH", "정산년월", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex거래명세서H.SetCol("NM_INSERT", "등록자", 100);
            this._flex거래명세서H.SetCol("IDX", "순번", 100);
            this._flex거래명세서H.SetCol("AM", "거래명세서", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래명세서H.SetCol("AM_EL", "상세내역", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래명세서H.SetCol("AM_PO", "발주", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래명세서H.SetCol("AM_IO", "입고", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래명세서H.SetCol("AM_IV", "매입", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래명세서H.SetCol("TP_COMPLETE", "확인여부", 100);
            this._flex거래명세서H.SetCol("MISS_ROW", "행누락", 100);
            this._flex거래명세서H.SetCol("DC_LOG", "처리결과", 100);
            this._flex거래명세서H.SetCol("YN_CONFIRM", "확인처리", 60, false, CheckTypeEnum.Y_N);
            this._flex거래명세서H.SetCol("DC_RMK", "비고", 100, true);

            this._flex거래명세서H[0, this._flex거래명세서H.Cols["AM"].Index] = this.DD("금액");
            this._flex거래명세서H[0, this._flex거래명세서H.Cols["AM_EL"].Index] = this.DD("금액");
            this._flex거래명세서H[0, this._flex거래명세서H.Cols["AM_PO"].Index] = this.DD("금액");
            this._flex거래명세서H[0, this._flex거래명세서H.Cols["AM_IO"].Index] = this.DD("금액");
            this._flex거래명세서H[0, this._flex거래명세서H.Cols["AM_IV"].Index] = this.DD("금액");

            this._flex거래명세서H.SettingVersion = "0.0.0.2";
            this._flex거래명세서H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex거래명세서H.SetDummyColumn("S");
            #endregion

            #region 파일번호별
            this._flex파일번호별.BeginSetting(1, 1, false);

            this._flex파일번호별.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
            this._flex파일번호별.SetCol("NO_FILE", "파일번호", 100);
            this._flex파일번호별.SetCol("NO_PO", "발주번호(대표)", 100);
            this._flex파일번호별.SetCol("AM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("AM_IO", "입고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("AM_IV", "매입금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("AM_REMAIN", "미정산금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("AM_DIFF", "차액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex파일번호별.SetCol("DC_RMK", "요청내용", 100);
            this._flex파일번호별.SetCol("DC_RMK_TAX", "비고", 100, true);

            this._flex파일번호별.SetDummyColumn("S", "DC_RMK");

            this._flex파일번호별.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid6 });

            this._flex파일번호별.SettingVersion = "0.0.0.1";
            this._flex파일번호별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex상세내역.BeginSetting(1, 1, false);

            this._flex상세내역.SetCol("SEQ", "순번", 100);
            this._flex상세내역.SetCol("NO_FILE", "파일번호", 100);
            this._flex상세내역.SetCol("AM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex상세내역.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex상세내역.SetCol("DC_RMK", "비고", 100, true);

            this._flex상세내역.SettingVersion = "0.0.0.1";
            this._flex상세내역.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 발주내역
            this._flex발주내역.BeginSetting(1, 1, false);

            this._flex발주내역.SetCol("NO_PO", "발주번호", 100);
            this._flex발주내역.SetCol("NO_IO", "입고번호", 100);
            this._flex발주내역.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex발주내역.SetCol("AM_IO", "입고금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex발주내역.SetCol("AM_IV", "매입금액", 100, false, typeof(decimal), FormatTpType.MONEY);

            this._flex발주내역.SettingVersion = "0.0.0.1";
            this._flex발주내역.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 세금계산서

            #region Header
            this._flex세금계산서H.BeginSetting(1, 1, false);

            this._flex세금계산서H.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
            this._flex세금계산서H.SetCol("YN_MONTHLY", "월말정산", 60, false, CheckTypeEnum.Y_N);
            this._flex세금계산서H.SetCol("PLATFORM", "수신처", 100);
            this._flex세금계산서H.SetCol("NO_ETAX", "승인번호", 100);
            this._flex세금계산서H.SetCol("NO_COMPANY", "등록번호(공급자)", 100);
            this._flex세금계산서H.SetCol("NM_CLS_PARTNER", "거래처분류", 100);
            this._flex세금계산서H.SetCol("CD_PARTNER", "거래처코드", 100);
            this._flex세금계산서H.SetCol("LN_PARTNER", "거래처명", 100);
            this._flex세금계산서H.SetCol("DT_SEND", "작성일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex세금계산서H.SetCol("AM", "공급가액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex세금계산서H.SetCol("VAT", "세액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex세금계산서H.SetCol("DC_REASON", "수정사유", 100);
            this._flex세금계산서H.SetCol("DC_RMK1", "비고1", 100);
            this._flex세금계산서H.SetCol("DC_RMK2", "비고2", 100);
            this._flex세금계산서H.SetCol("NO_FILE", "파일번호", 100);
            this._flex세금계산서H.SetCol("NO_IO", "입고번호", 100);
            this._flex세금계산서H.SetCol("NO_IV", "매입번호", 100);
            this._flex세금계산서H.SetCol("NO_DOCU", "전표번호", 100);
            this._flex세금계산서H.SetCol("TP_READ", "처리구분", 100);
            this._flex세금계산서H.SetCol("DC_LOG", "처리결과", 100);
            this._flex세금계산서H.SetCol("NO_FILE_SEND", "파일번호(쪽지발송)", 100, true);
            this._flex세금계산서H.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex세금계산서H.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex세금계산서H.SetCol("YN_FILE", "파일등록여부", 100);

            this._flex세금계산서H.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid4 });

            this._flex세금계산서H.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";
            this._flex세금계산서H.Cols["DTS_UPDATE"].Format = "####/##/## ##:##:##";

            this._flex세금계산서H.SettingVersion = "0.0.0.2";
            this._flex세금계산서H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex세금계산서H.SetDummyColumn("S", "NO_EMP", "DC_RMK", "NO_PO");
            #endregion

            #region Line
            this._flex세금계산서L.BeginSetting(1, 1, false);

            this._flex세금계산서L.SetCol("SEQ", "순번", 100);
            this._flex세금계산서L.SetCol("NO_PART", "품목명", 100);
            this._flex세금계산서L.SetCol("QT", "수량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex세금계산서L.SetCol("UM", "단가", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex세금계산서L.SetCol("AM", "공급가액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex세금계산서L.SetCol("VAT", "세액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex세금계산서L.SetCol("DC_RMK", "비고", 100);

            this._flex세금계산서L.SettingVersion = "0.0.0.1";
            this._flex세금계산서L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 매입등록

            #region Header
            this._flex매입등록H.BeginSetting(1, 1, false);

            this._flex매입등록H.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex매입등록H.SetCol("YN_CONFIRM", "확인요청", 40, true, CheckTypeEnum.Y_N);
            this._flex매입등록H.SetCol("NO_IO", "입고번호", 100);
            this._flex매입등록H.SetCol("NO_PO", "발주번호", 100); //발주번호 필드 추가 !! 
            this._flex매입등록H.SetCol("NO_ORDER", "견적번호", 100);
            this._flex매입등록H.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex매입등록H.SetCol("CD_PARTNER", "매입처", false);
            this._flex매입등록H.SetCol("LN_PARTNER", "매입처명", 130);
            this._flex매입등록H.SetCol("NM_VESSEL", "호선명", 150);
            this._flex매입등록H.SetCol("AM_PO_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록H.SetCol("AM_PO", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록H.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록H.SetCol("AM_TOT", "총금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록H.SetCol("AM_ADPAY", "선지급금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록H.SetCol("YN_TRANS", "이체여부", 60, false, CheckTypeEnum.Y_N);
            this._flex매입등록H.SetCol("NM_PO_EMP", "발주담당자", 80); //담당자 필드 추가 !! 
            this._flex매입등록H.SetCol("NM_IO_EMP", "입고담당자", 80);
            //this._flex매입등록H.SetCol("NM_TRANS", "거래구분", 80);
            this._flex매입등록H.SetCol("DC_RMK", "비고", 150);
            this._flex매입등록H.SetCol("AM_EX", "대상금액", false);
            this._flex매입등록H.SetCol("DC_RMK_CONFIRM", "요청내용", false);
            this._flex매입등록H.SetCol("DC_RMK_WF", "회신내용", false);

            this._flex매입등록H.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid3 });

            this._flex매입등록H.SetDummyColumn(new string[] { "S", "YN_CONFIRM" });
            this._flex매입등록H.ExtendLastCol = true;
            this._flex매입등록H.EnabledHeaderCheck = false;
            this._flex매입등록H.SettingVersion = "1.0.0.0";
            this._flex매입등록H.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex매입등록L.BeginSetting(1, 1, false);

            this._flex매입등록L.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex매입등록L.SetCol("NO_PO", "발주번호", 80, false);
            this._flex매입등록L.SetCol("NO_DSP", "순번", 40);
            this._flex매입등록L.SetCol("NO_POLINE", "발주항번", 40);
            this._flex매입등록L.SetCol("CD_ITEM_PARTNER", "매출처품번", 100); //아이템코드 필드 추가 !!
            this._flex매입등록L.SetCol("NM_ITEM_PARTNER", "매출처품명", 100); // 디스크립션 필드 추가!!
            this._flex매입등록L.SetCol("CD_ITEM", "품목코드", 100);
            this._flex매입등록L.SetCol("NM_ITEM", "품목명", 120);
            this._flex매입등록L.SetCol("STND_ITEM", "규격", 120);
            this._flex매입등록L.SetCol("UNIT_IM", "단위", 80);
            this._flex매입등록L.SetCol("QT_INV", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입등록L.SetCol("QT_IV", "적용수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입등록L.SetCol("QT_INV_CLS", "매입잔량(관리)", false);
            this._flex매입등록L.SetCol("QT_CLS", "적용수량(관리)", false);
            this._flex매입등록L.SetCol("NM_EXCH", "통화명", 60);
            this._flex매입등록L.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex매입등록L.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex매입등록L.SetCol("UM", "원화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex매입등록L.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록L.SetCol("AM_IV", "원화금액", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록L.SetCol("VAT_IV", "부가세", 70, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록L.SetCol("AM_TOTAL", "총금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록L.SetCol("AM_ADPAY", "선지급금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입등록L.SetCol("NM_TP_UM_TAX", "부가세여부", false);
            this._flex매입등록L.SetCol("NM_QTIOTP", "수불형태", 100);
            this._flex매입등록L.SetCol("PI_PARTNER", "주거래처코드", false);
            this._flex매입등록L.SetCol("PI_LN_PARTNER", "주거래처명", false);
            this._flex매입등록L.SetCol("GI_PARTNER", "납품처코드", false);
            this._flex매입등록L.SetCol("GI_LN_PARTNER", "납품처명", false);
            this._flex매입등록L.SetCol("NO_APP", "품의번호", false);
            this._flex매입등록L.SetCol("CD_PJT", "프로젝트코드", false);
            this._flex매입등록L.SetCol("NM_PROJECT", "프로젝트명", 100);
            this._flex매입등록L.SetCol("NM_TPPO", "발주유형", 100);
            this._flex매입등록L.SetCol("NM_TAX", "과세구분", 100, false);
            this._flex매입등록L.SetCol("FG_PAYMENT", "지급조건(발주)", false);

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flex매입등록L.SetCol("CD_PJT_ITEM", "프로젝트 품목코드", false);
                this._flex매입등록L.SetCol("NM_PJT_ITEM", "프로젝트 품목명", false);
                this._flex매입등록L.SetCol("PJT_ITEM_STND", "프로젝트 품목규격", false);
                this._flex매입등록L.SetCol("NO_WBS", "WBS번호", false);
                this._flex매입등록L.SetCol("NO_CBS", "CBS번호", false);
                this._flex매입등록L.SetCol("CD_ACTIVITY", "ACTIVITY 코드", false);
                this._flex매입등록L.SetCol("NM_ACTIVITY", "ACTIVITY", false);
                this._flex매입등록L.SetCol("CD_COST", "원가코드", false);
                this._flex매입등록L.SetCol("NM_COST", "원가명", false);
            }

            this._flex매입등록L.SetCol("DC_RMK1", "비고1", false);
            this._flex매입등록L.SetCol("DC_RMK2", "비고2", false);
            this._flex매입등록L.SetCol("NM_PURGRP", "구매그룹", 100);
            this._flex매입등록L.SetCol("CD_CC", "CC코드", false);
            this._flex매입등록L.SetCol("NM_CC", "CC명", 80, false);
            this._flex매입등록L.SetCol("NM_KOR", "발주담당자", 80);
            this._flex매입등록L.SetCol("CD_SL", "창고코드", false);
            this._flex매입등록L.SetCol("NM_SL", "창고명", false);
            this._flex매입등록L.SetCol("FG_UM", "단가유형", false);
            this._flex매입등록L.SetCol("CD_PJTGRP", "프로젝트그룹코드", false);
            this._flex매입등록L.SetCol("NM_PJTGRP", "프로젝트그룹명", false);
            this._flex매입등록L.SetCol("DC1", "비고1", 100);
            this._flex매입등록L.SetCol("DC2", "비고2", 100);

            this._flex매입등록L.SetDummyColumn(new string[] { "S" });
            this._flex매입등록L.EnabledHeaderCheck = false;
            this._flex매입등록L.SettingVersion = "1.0.0.0";
            this._flex매입등록L.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex매입등록L.SetExceptSumCol(new string[] { "RT_EXCH" });
            #endregion

            #endregion
        }

		private void InitEvent()
		{
			this.btn세금계산서업로드.Click += Btn세금계산서업로드_Click;
			this.btn자동매입.Click += Btn자동매입_Click;
            this.btn첨부파일.Click += Btn첨부파일_Click;
            this.btn미리보기.Click += Btn미리보기_Click;
			this.btn조회.Click += Btn조회_Click;
			this.btn수동매입.Click += Btn수동매입_Click;
			this.btn부대비용적용.Click += btn부대비용적용_Click;
			this.btn부대비용제거.Click += btn부대비용제거_Click;
			this.btn전체선택.Click += btn전체선택_Click;
			this.btn전체해제.Click += btn전체해제_Click;
			this.btn명세서업로드.Click += Btn명세서업로드_Click;
			this.btn명세서삭제.Click += Btn명세서삭제_Click;
			this.btn지급예정일계산.Click += Btn지급예정일계산_Click;
			this.btn선지급정리.Click += Btn선지급정리_Click;
			this.btn임의처리.Click += Btn임의처리_Click;
			this.btn임의처리해제.Click += Btn임의처리해제_Click;
            this.btn환율변경.Click += new EventHandler(this.btn환율변경_Click);
			this.btn쪽지발송.Click += Btn쪽지발송_Click;
			this.btn확인요청.Click += Btn확인요청_Click;
			this.btn확인요청H.Click += Btn확인요청H_Click;
			this.btn확인처리.Click += Btn확인처리_Click;
			this.btn확인해제.Click += Btn확인해제_Click;

            this.cur외화부대비용.Leave += new EventHandler(this.cur원화부대비용계산);
            this.cur원화부대비용.Leave += new EventHandler(this.cur외화부대비용계산);
            this.cur부대비용환율.TextChanged += new EventHandler(this.cur원화부대비용계산);

            this.cbo통화명.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
            this.cbo부대비용항목.SelectionChangeCommitted += new EventHandler(this.cbo부대비용항목_SelectionChangeCommitted);

			this.bpc수신자.QueryAfter += Bpc수신자_QueryAfter;
            this.ctx발주번호.QueryBefore += Ctx발주번호_QueryBefore;
            this.ctx발주번호.QueryAfter += Ctx발주번호_QueryAfter;

            this._flex세금계산서H.AfterRowChange += _flex세금계산서H_AfterRowChange;
			this._flex세금계산서H.DoubleClick += _flex세금계산서H_DoubleClick;

            this._flex매입등록H.AfterRowChange += new RangeEventHandler(this._flex매입등록H_AfterRowChange);
            this._flex매입등록H.StartEdit += new RowColEventHandler(this._flex매입등록H_StartEdit);
            this._flex매입등록H.AfterEdit += new RowColEventHandler(this._flex매입등록H_AfterEdit);

            this._flex매입등록L.AfterEdit += new RowColEventHandler(this._flex매입등록L_AfterEdit);
            this._flex매입등록L.ValidateEdit += new ValidateEditEventHandler(this._flex매입등록L_ValidateEdit);

			this._flex거래명세서H.AfterRowChange += _flex거래명세서H_AfterRowChange;
			this._flex파일번호별.AfterRowChange += _flex파일번호별_AfterRowChange;
        }

        private void Btn확인해제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string query;

            try
            {
                dataRowArray = this._flex거래명세서H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (this._flex거래명세서H.DataTable.Select("S = 'Y' AND ISNULL(YN_CONFIRM, 'N') = 'N'").Length > 0)
                {
                    this.ShowMessage("확인해제된 건이 선택되어 있습니다.");
                    return;
                }
                else
                {
                    query = @"UPDATE EH
SET EH.YN_CONFIRM = 'N' 
FROM CZ_PU_ETAX_DETAILH EH WITH(NOLOCK)
WHERE EH.CD_COMPANY = '{0}'
AND EH.CD_PARTNER = '{1}'
AND EH.DT_MONTH = '{2}'
AND EH.IDX = {3}";

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["CD_PARTNER"].ToString(),
                                                                                   dr["DT_MONTH"].ToString(),
                                                                                   dr["IDX"].ToString() }));
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확인해제.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn확인처리_Click(object sender, EventArgs e)
		{
            DataRow[] dataRowArray;
            string query;

            try
            {
                dataRowArray = this._flex거래명세서H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
				}
                else if (this._flex거래명세서H.DataTable.Select("S = 'Y' AND ISNULL(YN_CONFIRM, 'N') = 'Y'").Length > 0)
				{
                    this.ShowMessage("확인처리된 건이 선택되어 있습니다.");
                    return;
				}
                else
				{
                    query = @"UPDATE EH
SET EH.YN_CONFIRM = 'Y' 
FROM CZ_PU_ETAX_DETAILH EH WITH(NOLOCK)
WHERE EH.CD_COMPANY = '{0}'
AND EH.CD_PARTNER = '{1}'
AND EH.DT_MONTH = '{2}'
AND EH.IDX = {3}";

                    foreach (DataRow dr in dataRowArray)
					{
                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["CD_PARTNER"].ToString(),
                                                                                   dr["DT_MONTH"].ToString(),
                                                                                   dr["IDX"].ToString() }));
					}
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확인처리.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void Btn명세서삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex거래명세서H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
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
                MsgEnd(ex);
            }
        }

        private void _flex파일번호별_AfterRowChange(object sender, RangeEventArgs e)
        {
            string key, key1, filter;
            DataTable dt = null;

            try
            {
                key = this._flex파일번호별["CD_PARTNER"].ToString();
                key1 = this._flex파일번호별["NO_FILE"].ToString();
                filter = "CD_PARTNER ='" + key + "' AND NO_FILE = '" + key1 + "'";

                if (this._flex파일번호별.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail4(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                key,
                                                                key1 });
                }

                this._flex발주내역.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex거래명세서H_AfterRowChange(object sender, RangeEventArgs e)
		{
            string key, key1, key2, filter;
            DataTable dt = null, dt1 = null;

            try
            {
                key = this._flex거래명세서H["CD_PARTNER"].ToString();
                key1 = this._flex거래명세서H["DT_MONTH"].ToString();
                key2 = this._flex거래명세서H["IDX"].ToString();
                filter = "CD_PARTNER ='" + key + "' AND DT_MONTH = '" + key1 + "' AND IDX = '" + key2 + "'";

                if (this._flex거래명세서H.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                key,
                                                                key1,
                                                                key2,
                                                                this.txt파일번호.Text });

                    dt1 = this._biz.SearchDetail3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 key,
                                                                 key1,
                                                                 key2,
                                                                 this.txt파일번호.Text });
                }

                this._flex상세내역.BindingAdd(dt, filter);
                this._flex파일번호별.BindingAdd(dt1, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void Ctx발주번호_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this._flex세금계산서H["NO_PO"] = e.CodeValue;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Ctx발주번호_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P11_ID_MENU = "H_PU_PO_SUB";
                e.HelpParam.P21_FG_MODULE = "N";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Bpc수신자_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {   
                this._flex세금계산서H["NO_EMP"] = e.HelpReturn.QueryWhereIn_Pipe;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn확인요청H_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            List<string> 수신자;
            string contents, query;

            try
            {
                if (this.tabControl3.SelectedTab == this.tpg거래명세서)
				{
                    #region 거래명세서
                    if (!this._flex파일번호별.HasNormalRow)
                        return;

                    dataRowArray = this._flex파일번호별.DataTable.Select("S = 'Y'");

                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else if (this._flex파일번호별.DataTable.Select("S = 'Y' AND ISNULL(DC_RMK, '') = ''").Length > 0)
                    {
                        this.ShowMessage("요청내용이 입력되지 않은 건이 선택되어 있습니다.");
                        return;
                    }
                    else
                    {
                        contents = @"** 거래명세서 확인 요청

매입처명 : {0}
파일번호 : {1}
미정산금액 : {2}
명세서금액 : {3}

{4}

확인 후 워크플로우 -> 계산서확인 단계를 통해서 회신 바랍니다. 

※ 본 쪽지는 발신전용 입니다.";

                        query = @"SELECT TOP 1 MC.DT_CAL 
FROM MA_CALENDAR MC
WHERE MC.CD_COMPANY = '{0}'
AND MC.FG1_HOLIDAY = 'W'
AND MC.DT_CAL >= CONVERT(CHAR(6), DATEADD(MONTH, 1, '{1}'), 112) + '10'
ORDER BY MC.DT_CAL";

                        foreach (DataRow dr in dataRowArray)
                        {
                            DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_CONFIRM_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                             "004",
                                                                                             string.Empty,
                                                                                             string.Empty,
                                                                                             dr["NO_PO"].ToString(),
                                                                                             DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                         dr["DT_MONTH"].ToString() + "01")),
                                                                                             dr["AM"].ToString(),
                                                                                             dr["AM_REMAIN"].ToString(),
                                                                                             dr["NO_EMP"].ToString(),
                                                                                             dr["DC_RMK"].ToString(),
                                                                                             Global.MainFrame.LoginInfo.UserID });

                            수신자 = new List<string>();

							foreach (string 사원 in dr["NO_EMP"].ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
							{
								수신자.Add(사원);

								if (dr["NO_PO"].ToString().Contains("ST") && 
                                    DBHelper.GetDataTable(string.Format(@"SELECT ME.NO_EMP 
                                                                          FROM MA_EMP ME
                                                                          WHERE ME.CD_COMPANY = 'K100'
                                                                          AND ME.CD_CC = '010900'
                                                                          AND ME.NO_EMP = '{0}'", 사원)).Rows.Count > 0)
								{
									수신자.Add("S-495");
								}
							}

							수신자.Add(Global.MainFrame.LoginInfo.UserID);

                            Messenger.SendMSG(수신자.ToArray(), string.Format(contents, dr["LN_PARTNER"].ToString(),
                                                                                        dr["NO_FILE"].ToString(),
                                                                                        D.GetDecimal(dr["AM_REMAIN"].ToString()).ToString("N"),
                                                                                        D.GetDecimal(dr["AM"].ToString()).ToString("N"),
                                                                                        dr["DC_RMK"].ToString()));
                        }
                    }
                    #endregion
                }
				else
				{
                    #region 세금계산서
                    if (!this._flex세금계산서H.HasNormalRow)
                        return;

                    dataRowArray = this._flex세금계산서H.DataTable.Select("S = 'Y'");

                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else if (this._flex세금계산서H.DataTable.Select("S = 'Y' AND ISNULL(DC_RMK, '') = ''").Length > 0)
                    {
                        this.ShowMessage("요청내용이 입력되지 않은 건이 선택되어 있습니다.");
                        return;
                    }
                    else
                    {
                        contents = @"** 세금계산서 확인 요청

승인번호 : {0}
매입처명 : {1}
발주번호 : {2}

{3}

확인 후 워크플로우 -> 계산서확인 단계를 통해서 회신 바랍니다. 

※ 본 쪽지는 발신전용 입니다.";

                        query = @"SELECT TOP 1 MC.DT_CAL 
FROM MA_CALENDAR MC
WHERE MC.CD_COMPANY = '{0}'
AND MC.FG1_HOLIDAY = 'W'
AND MC.DT_CAL >= CONVERT(CHAR(6), DATEADD(MONTH, 1, '{1}'), 112) + '10'
ORDER BY MC.DT_CAL";

                        foreach (DataRow dr in dataRowArray)
                        {
                            DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_CONFIRM_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                             "002",
                                                                                             dr["NO_ETAX"].ToString(),
                                                                                             string.Empty,
                                                                                             dr["NO_PO"].ToString(),
                                                                                             DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                         dr["DT_SEND"].ToString())),
                                                                                             0,
                                                                                             0,
                                                                                             dr["NO_EMP"].ToString(),
                                                                                             dr["DC_RMK"].ToString(),
                                                                                             Global.MainFrame.LoginInfo.UserID });

                            수신자 = new List<string>();

                            foreach (string 사원 in dr["NO_EMP"].ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                수신자.Add(사원);

                                if (dr["NO_PO"].ToString().Contains("ST") &&
                                    DBHelper.GetDataTable(string.Format(@"SELECT ME.NO_EMP 
                                                                          FROM MA_EMP ME
                                                                          WHERE ME.CD_COMPANY = 'K100'
                                                                          AND ME.CD_CC = '010900'
                                                                          AND ME.NO_EMP = '{0}'", 사원)).Rows.Count > 0)
                                {
                                    수신자.Add("S-495");
                                }
                            }

                            수신자.Add(Global.MainFrame.LoginInfo.UserID);

                            Messenger.SendMSG(수신자.ToArray(), string.Format(contents, dr["NO_ETAX"].ToString(),
                                                                                        dr["LN_PARTNER"].ToString(),
                                                                                        dr["NO_PO"].ToString(),
                                                                                        dr["DC_RMK"].ToString()));
                        }
                    }
                    #endregion
                }

                this.OnToolBarSearchButtonClicked(null, null);

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확인요청.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn확인요청_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            List<string> 수신자;
            string contents, query;

            try
            {
                if (!this._flex세금계산서H.HasNormalRow || !this._flex매입등록H.HasNormalRow)
                    return;

                dataRowArray = this._flex매입등록H.DataTable.Select("YN_CONFIRM = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flex매입등록H.DataTable.Select("YN_CONFIRM = 'Y' AND ISNULL(DC_RMK_CONFIRM, '') = ''").Length > 0)
				{
                    this.ShowMessage("요청내용이 입력되지 않은 건이 선택되어 있습니다.");
                    return;
                }
                else if (this._flex매입등록H.DataTable.Select("YN_CONFIRM = 'Y' AND ISNULL(AM_EX, 0) = 0").Length > 0)
				{
                    this.ShowMessage("대상금액이 입력되지 않은 건이 선택되어 있습니다.");
                    return;
                }
                else if (this._flex매입등록H.DataTable.Select("YN_CONFIRM = 'Y' AND ISNULL(AM_PO_EX, 0) >= ISNULL(AM_EX, 0)").Length > 0)
                {
                    this.ShowMessage("차액이 같거나 마이너스인 건은 확인 요청할 수 없습니다.");
                    return;
                }
                else
				{
                    contents = @"** 세금계산서 확인 요청

매입처명 : {0}
파일번호 : {1}
입고금액 : {2}
계산서금액 : {3} 

{4}

확인 후 워크플로우 -> 계산서확인 단계를 통해서 회신 바랍니다. 

※ 본 쪽지는 발신전용 입니다.";

                    query = @"SELECT TOP 1 MC.DT_CAL 
FROM MA_CALENDAR MC
WHERE MC.CD_COMPANY = '{0}'
AND MC.FG1_HOLIDAY = 'W'
AND MC.DT_CAL >= CONVERT(CHAR(6), DATEADD(MONTH, 1, '{1}'), 112) + '10'
ORDER BY MC.DT_CAL";

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_CONFIRM_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                         "001",
                                                                                         this._flex세금계산서H["NO_ETAX"].ToString(),
                                                                                         dr["NO_IO"].ToString(),
                                                                                         dr["NO_PO"].ToString(),
                                                                                         DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                     this._flex세금계산서H["DT_SEND"].ToString())),
                                                                                         dr["AM_EX"].ToString(),
                                                                                         dr["AM_PO_EX"].ToString(),
                                                                                         dr["NO_EMP"].ToString(),
                                                                                         dr["DC_RMK_CONFIRM"].ToString(),
                                                                                         Global.MainFrame.LoginInfo.UserID });

                        수신자 = new List<string>();
                        수신자.Add(dr["NO_EMP"].ToString());

                        if (dr["NO_PO"].ToString().Contains("ST") && dr["CD_CC"].ToString() == "010900")
                            수신자.Add("S-495");

                        수신자.Add(Global.MainFrame.LoginInfo.UserID);

                        Messenger.SendMSG(수신자.ToArray(), string.Format(contents, dr["LN_PARTNER"].ToString(), 
                                                                                    dr["NO_SO"].ToString(),
                                                                                    D.GetDecimal(dr["AM_PO_EX"].ToString()).ToString("N"),
                                                                                    D.GetDecimal(dr["AM_EX"].ToString()).ToString("N"),
                                                                                    dr["DC_RMK_CONFIRM"].ToString()));

                        dr["YN_CONFIRM"] = "N";
					}

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확인요청.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex세금계산서H_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flex세금계산서H.Cols[this._flex세금계산서H.Col].Name == "NO_DOCU" && 
                    !string.IsNullOrEmpty(D.GetString(this._flex세금계산서H["NO_DOCU"])))
				{
                    this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flex세금계산서H["NO_DOCU"]),
                                                                                                                                     "1",
                                                                                                                                     Global.MainFrame.LoginInfo.CdPc,
                                                                                                                                     Global.MainFrame.LoginInfo.CompanyCode });
                }
                else if (this._flex세금계산서H.Cols[this._flex세금계산서H.Col].Name == "NO_IV" &&
                         !string.IsNullOrEmpty(D.GetString(this._flex세금계산서H["NO_IV"])))
                {
                    if (this.IsExistPage("P_CZ_PU_IV_MNG", false))
                        this.UnLoadPage("P_CZ_PU_IV_MNG", false);

                    this.LoadPageFrom("P_CZ_PU_IV_MNG", this.DD("매입관리(딘텍)"), Grant, new object[] { this._flex세금계산서H["DT_SEND"].ToString(),
                                                                                                        this._flex세금계산서H["NO_IV"].ToString() });
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn쪽지발송_Click(object sender, EventArgs e)
        {
            List<string> 수신자목록;
            string contents, query, 파일번호목록;

            try
            {
				this.Btn자동매입_Click(null, null);

				query = @"SELECT DISTINCT A.value AS NO_FILE,
                ISNULL(PH.NO_EMP, '') AS NO_EMP,
                ISNULL(MP.LN_PARTNER, '') AS LN_PARTNER
FROM CZ_PU_ETAXH ET WITH(NOLOCK)
CROSS APPLY string_split(ISNULL(ET.NO_FILE, '') + ',' + ISNULL(ET.NO_FILE_SEND, ''), ',') A
LEFT JOIN PU_POH PH WITH(NOLOCK) ON PH.CD_COMPANY = ET.CD_COMPANY AND PH.NO_PO = A.value AND PH.NO_PO LIKE 'ST%'
LEFT JOIN (SELECT MP.CD_COMPANY, MP.NO_COMPANY, MP.LN_PARTNER,
                  ROW_NUMBER() OVER (PARTITION BY MP.CD_COMPANY, MP.NO_COMPANY ORDER BY MP.DTS_INSERT DESC) AS IDX
           FROM MA_PARTNER MP WITH(NOLOCK)) MP 
ON MP.CD_COMPANY = ET.CD_COMPANY AND MP.NO_COMPANY = ET.NO_COMPANY AND MP.IDX = 1
WHERE ET.CD_COMPANY = '{0}'
AND (LEFT(ET.DTS_INSERT, 8) <= CONVERT(CHAR(8), DATEADD(DAY, -7, GETDATE()), 112) OR 
     (LEFT(ET.DT_SEND, 6) <= CONVERT(CHAR(6), DATEADD(MONTH, -1, GETDATE()), 112) AND DAY(GETDATE()) >= 5))
AND ISNULL(ET.YN_READ, '') = ''
AND (ISNULL(ET.NO_FILE, '') <> '' OR ISNULL(ET.NO_FILE_SEND, '') <> '')
AND ISNULL(ET.NO_IO, '') = ''
AND ISNULL(A.value, '') <> '' 
AND ET.AM > 0
AND EXISTS (SELECT 1 
            FROM PU_POL PL WITH(NOLOCK)
            WHERE PL.CD_COMPANY = ET.CD_COMPANY
            AND (PL.NO_SO = A.value OR PL.NO_PO = A.value)
            AND ISNULL(PL.QT_PO, 0) > ISNULL(PL.QT_RCV, 0))";

                DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

				if (dt == null || dt.Rows.Count == 0)
				{
					this.ShowMessage("발송대상 건이 없습니다.");
					return;
				}

				if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				{
                    query = @"SELECT MC.CD_SYSDEF AS PREFIX,
	   ISNULL(ME.NO_EMP, A.VALUE) AS NO_EMP
FROM CZ_MA_CODEDTL MC WITH(NOLOCK)
CROSS APPLY string_split(MC.CD_FLAG1, ',') A
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = MC.CD_COMPANY AND ME.CD_CC = A.VALUE AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'
WHERE MC.CD_COMPANY = 'K100'
AND MC.CD_FIELD = 'CZ_PU00010'";
                }
                else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
                    query = @"SELECT ME.NO_EMP
FROM MA_EMP ME WITH(NOLOCK)
WHERE ME.CD_COMPANY = 'K200'
AND ME.CD_CC = '020230'
AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'";
                }

                DataTable dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

                파일번호목록 = string.Empty;
                수신자목록 = new List<string>();

                수신자목록.Add("S-391");

                if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
                    수신자목록.Add("S-485"); // 이승철
                    수신자목록.Add("S-223"); // 김남규

                    foreach (DataRow dr in dt1.Rows)
                        수신자목록.Add(dr["NO_EMP"].ToString());

                    수신자목록.Add("D-011"); // 피수민
                }

                foreach (DataRow dr in dt.Rows)
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
					{
                        if (!string.IsNullOrEmpty(dr["NO_EMP"].ToString()) && 
                            !수신자목록.Contains(dr["NO_EMP"].ToString()))
                            수신자목록.Add(dr["NO_EMP"].ToString());

                        foreach (DataRow dr1 in dt1.Select(string.Format("PREFIX = '{0}'", dr["NO_FILE"].ToString().Left(2))))
                        {
                            if (!수신자목록.Contains(dr1["NO_EMP"].ToString()))
                                수신자목록.Add(dr1["NO_EMP"].ToString());
                        }
                    }
                    
                    파일번호목록 += dr["NO_FILE"].ToString() + " (" + dr["LN_PARTNER"].ToString() + ")" + Environment.NewLine;
				}

                contents = @"** 자동매입정산 미입고 리스트
                             
{0}

세금계산서 접수일자가 지난달이거나 접수 후 7일 이상 경과 건입니다.

확인 후 입고처리 바랍니다.

※ 본 쪽지는 발신전용 입니다.";

				if (Messenger.SendMSG(수신자목록.ToArray(), string.Format(contents, 파일번호목록)) == true)
					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn쪽지발송.Text);
			}
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn임의처리_Click(object sender, EventArgs e)
        {
            string query;
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex세금계산서H.HasNormalRow) return;
                
                dataRowArray = this._flex세금계산서H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (this._flex세금계산서H.DataTable.Select("S = 'Y' AND ISNULL(YN_READ, '') <> ''").Length > 0)
				{
                    this.ShowMessage("처리구분이 설정되어 있는 건이 선택되어 있습니다.");
                    return;
                }
                else
				{
                    foreach (DataRow dr in dataRowArray)
					{
                        query = @"UPDATE CZ_PU_ETAXH
                                  SET YN_READ = 'C',
                                      ID_UPDATE = '{2}',
                                      DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                                  WHERE CD_COMPANY = '{0}'
                                  AND NO_ETAX = '{1}'";

                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["NO_ETAX"].ToString(),
                                                                                   Global.MainFrame.LoginInfo.UserID }));
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn임의처리.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn임의처리해제_Click(object sender, EventArgs e)
        {
            string query;
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex세금계산서H.HasNormalRow) return;

                dataRowArray = this._flex세금계산서H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (this._flex세금계산서H.DataTable.Select("S = 'Y' AND ISNULL(YN_READ, '') <> 'C'").Length > 0)
                {
                    this.ShowMessage("처리구분이 임의처리가 아닌 건이 선택되어 있습니다.");
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        query = @"UPDATE CZ_PU_ETAXH
                                  SET YN_READ = NULL,
                                      ID_UPDATE = '{2}',
                                      DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                                  WHERE CD_COMPANY = '{0}'
                                  AND NO_ETAX = '{1}'";

                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["NO_ETAX"].ToString(),
                                                                                   Global.MainFrame.LoginInfo.UserID }));
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn임의처리해제.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn선지급정리_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._flex세금계산서H["NO_IV"].ToString()))
				{
                    this.ShowMessage("매입정산 완료 건만 선지급 정리 가능 합니다.");
                    return;
				}

                if (this.IsExistPage("P_CZ_PU_ADPAYMENT_BILL", false))
                    this.UnLoadPage("P_CZ_PU_ADPAYMENT_BILL", false);

                this.LoadPageFrom("P_CZ_PU_ADPAYMENT_BILL", this.DD("선지급정리"), Grant, new object[] { new object[] { this._flex세금계산서H["CD_PARTNER"].ToString(),
                                                                                                                        this._flex세금계산서H["LN_PARTNER"].ToString(),
                                                                                                                        "001",
                                                                                                                        this._flex세금계산서H["DT_SEND"].ToString() } });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn지급예정일계산_Click(object sender, EventArgs e)
        {
            try
            {
                DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_AUTO_DT_END_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn지급예정일계산.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn명세서업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx명세서거래처.CodeValue))
				{
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, "명세서거래처");
                    return;
				}

                if (string.IsNullOrEmpty(this.dtp정산년월.Text))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, "정산년월");
                    return;
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                
                FINDER.ETaxDetail(this.ctx명세서거래처.CodeValue, this.dtp정산년월.Text, openFileDialog.FileName);

                string 업로드위치 = "Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp정산년월.Text;
                string key = this.ctx명세서거래처.CodeValue + "_" + this.dtp정산년월.Text;

                FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, key);
                this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, key, fileInfo, 업로드위치, "P_CZ_PU_IV_AUTO_MNG");

                string query = @"UPDATE EH
SET EH.NM_FILE = '{3}'
FROM CZ_PU_ETAX_DETAILH EH
WHERE EH.CD_COMPANY = '{0}'
AND EH.CD_PARTNER = '{1}'
AND EH.DT_MONTH = '{2}'";

                DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
                                                            this.ctx명세서거래처.CodeValue,
                                                            this.dtp정산년월.Text,
                                                            fileInfo.Name));

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn명세서업로드.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn부대비용적용_Click(object sender, EventArgs e)
        {
            DataRow dr입고항목, dr부대비용항목, dr부대비용;
            string filter;

            try
            {
                if (!this._flex매입등록L.HasNormalRow) return;
                if (!this._flex매입등록H.HasNormalRow && !this._flex매입등록L.HasNormalRow) return;

                filter = "NO_IO = '" + this._flex매입등록H["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flex매입등록H["FG_TAX"].ToString() + "'";
                filter += " AND NO_PO = '" + D.GetString(this.cbo부대비용발주번호.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this.cbo부대비용항목.SelectedValue) + "'";

                if (this._flex매입등록L.DataTable.Select(filter).Length > 0)
                {
                    #region 부대비용수정
                    dr부대비용 = this._flex매입등록L.DataTable.Select(filter)[0];

                    dr부대비용["UM_EX"] = this.외화계산(this.cur외화부대비용.DecimalValue);
                    dr부대비용["AM_EX"] = this.외화계산(D.GetDecimal(dr부대비용["QT_INV_CLS"]) * D.GetDecimal(dr부대비용["UM_EX"]));

                    if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
                        dr부대비용["UM"] = this.원화계산(this.cur원화부대비용.DecimalValue);
                    else
                        dr부대비용["UM"] = this.원화계산(D.GetDecimal(dr부대비용["UM_EX"]) * D.GetDecimal(dr부대비용["RT_EXCH"]));

                    dr부대비용["AM_IV"] = this.원화계산(D.GetDecimal(dr부대비용["QT_INV_CLS"]) * D.GetDecimal(dr부대비용["UM"]));

                    if (D.GetDecimal(dr부대비용["TAX_RATE"]) != 0)
                        dr부대비용["VAT_IV"] = this.원화계산(D.GetDecimal(dr부대비용["AM_IV"]) * D.GetDecimal(dr부대비용["TAX_RATE"]) / 100);
                    else
                        dr부대비용["VAT_IV"] = this.원화계산(0);

                    dr부대비용["AM_TOTAL"] = this.원화계산((D.GetDecimal(dr부대비용["AM_IV"]) + D.GetDecimal(dr부대비용["VAT_IV"])) - D.GetDecimal(dr부대비용["AM_ADPAY"]));
                    #endregion
                }
                else
                {
                    #region 부대비용추가
                    filter = "NO_IO = '" + this._flex매입등록H["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flex매입등록H["FG_TAX"].ToString() + "'";
                    filter += " AND NO_PO = '" + D.GetString(this.cbo부대비용발주번호.SelectedValue) + "'";
                    filter += " AND NO_IOLINE = '" + D.GetString(this._flex매입등록L.DataTable.Compute("MAX(NO_IOLINE)", filter)) + "'";

                    dr입고항목 = this._flex매입등록L.DataTable.Select(filter)[0];
                    dr부대비용항목 = ((DataRowView)this.cbo부대비용항목.SelectedItem).Row;

                    this._flex매입등록L.Rows.Add();

                    this._flex매입등록L.Row = this._flex매입등록L.Rows.Count - 1;

                    this._flex매입등록L["S"] = "Y";
                    this._flex매입등록L["NO_IO"] = this._flex매입등록H["NO_IO"];
                    this._flex매입등록L["NO_IOLINE"] = D.GetString(dr입고항목["NO_IOLINE"]);
                    this._flex매입등록L["DT_IO"] = this._flex매입등록H["DT_IO"];
                    this._flex매입등록L["FG_TAX"] = this._flex매입등록H["FG_TAX"];

                    this._flex매입등록L["NO_PO"] = D.GetString(this.cbo부대비용발주번호.SelectedValue);
                    this._flex매입등록L["NO_POLINE"] = D.GetString(dr입고항목["NO_POLINE"]);
                    this._flex매입등록L["CD_ITEM"] = D.GetString(dr부대비용항목["CD_ITEM"]);
                    this._flex매입등록L["NM_ITEM"] = D.GetString(dr부대비용항목["NM_ITEM"]);
                    this._flex매입등록L["STND_ITEM"] = D.GetString(dr부대비용항목["STND_ITEM"]);
                    this._flex매입등록L["UNIT_IM"] = D.GetString(dr부대비용항목["UNIT_IM"]);
                    this._flex매입등록L["CD_PARTNER"] = D.GetString(dr입고항목["CD_PARTNER"]);
                    this._flex매입등록L["LN_PARTNER"] = D.GetString(dr입고항목["LN_PARTNER"]);
                    this._flex매입등록L["CD_DOCU"] = D.GetString(dr입고항목["CD_DOCU"]);
                    this._flex매입등록L["TP_UM_TAX"] = D.GetString(dr입고항목["TP_UM_TAX"]);
                    this._flex매입등록L["NM_TP_UM_TAX"] = D.GetString(dr입고항목["NM_TP_UM_TAX"]);
                    this._flex매입등록L["NM_TAX"] = D.GetString(dr입고항목["NM_TAX"]);
                    this._flex매입등록L["CD_QTIOTP"] = D.GetString(dr입고항목["CD_QTIOTP"]);
                    this._flex매입등록L["NM_QTIOTP"] = D.GetString(dr입고항목["NM_QTIOTP"]);
                    this._flex매입등록L["FG_UM"] = D.GetString(dr입고항목["FG_UM"]);
                    this._flex매입등록L["NO_EMP"] = D.GetString(dr입고항목["NO_EMP"]);
                    this._flex매입등록L["NM_KOR"] = D.GetString(dr입고항목["NM_KOR"]);
                    this._flex매입등록L["CD_CC"] = D.GetString(dr입고항목["CD_CC"]);
                    this._flex매입등록L["NM_CC"] = D.GetString(dr입고항목["NM_CC"]);
                    this._flex매입등록L["CD_PLANT"] = D.GetString(dr입고항목["CD_PLANT"]);
                    this._flex매입등록L["CD_GROUP"] = D.GetString(dr입고항목["CD_GROUP"]);
                    this._flex매입등록L["FG_TPPURCHASE"] = D.GetString(dr입고항목["FG_TPPURCHASE"]);
                    this._flex매입등록L["YN_RETURN"] = D.GetString(dr입고항목["YN_RETURN"]);
                    this._flex매입등록L["NO_LC"] = D.GetString(dr입고항목["NO_LC"]);
                    this._flex매입등록L["CD_PJT"] = D.GetString(dr입고항목["CD_PJT"]);
                    this._flex매입등록L["NM_PROJECT"] = D.GetString(dr입고항목["NM_PROJECT"]);
                    this._flex매입등록L["NM_TPPO"] = D.GetString(dr입고항목["NM_TPPO"]);
                    this._flex매입등록L["NM_PURGRP"] = D.GetString(dr입고항목["NM_PURGRP"]);

                    #region 금액계산
                    this._flex매입등록L["NM_EXCH"] = cbo부대비용통화명.Text;
                    this._flex매입등록L["CD_EXCH"] = cbo부대비용통화명.SelectedValue;
                    this._flex매입등록L["RT_EXCH"] = cur부대비용환율.DecimalValue;

                    this._flex매입등록L["QT_INV"] = 1;
                    this._flex매입등록L["QT_IV"] = 1;
                    this._flex매입등록L["QT_INV_CLS"] = 1;
                    this._flex매입등록L["QT_CLS"] = 1;

                    this._flex매입등록L["AM_ADPAY"] = 0;

                    this._flex매입등록L["UM_EX"] = this.외화계산(this.cur외화부대비용.DecimalValue);
                    this._flex매입등록L["AM_EX"] = this.외화계산(D.GetDecimal(this._flex매입등록L["QT_INV_CLS"]) * D.GetDecimal(this._flex매입등록L["UM_EX"]));

                    if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
                        this._flex매입등록L["UM"] = this.원화계산(this.cur원화부대비용.DecimalValue);
                    else
                        this._flex매입등록L["UM"] = this.원화계산(D.GetDecimal(this._flex매입등록L["UM_EX"]) * D.GetDecimal(this._flex매입등록L["RT_EXCH"]));

                    this._flex매입등록L["AM_IV"] = this.원화계산(D.GetDecimal(this._flex매입등록L["QT_INV_CLS"]) * D.GetDecimal(this._flex매입등록L["UM"]));

                    this._flex매입등록L["TAX_RATE"] = D.GetDecimal(dr입고항목["TAX_RATE"].ToString());
                    if (D.GetDecimal(this._flex매입등록L["TAX_RATE"]) != 0)
                        this._flex매입등록L["VAT_IV"] = this.원화계산(D.GetDecimal(this._flex매입등록L["AM_IV"]) * D.GetDecimal(this._flex매입등록L["TAX_RATE"]) / 100);
                    else
                        this._flex매입등록L["VAT_IV"] = this.원화계산(0);

                    this._flex매입등록L["AM_TOTAL"] = this.원화계산((D.GetDecimal(this._flex매입등록L["AM_IV"]) + D.GetDecimal(this._flex매입등록L["VAT_IV"])) - D.GetDecimal(this._flex매입등록L["AM_ADPAY"]));
                    #endregion

                    this._flex매입등록L.Col = this._flex매입등록L.Cols.Fixed;
                    this._flex매입등록L.AddFinished();
                    this._flex매입등록L.Focus();
                    #endregion
                }

                this.SetHeaderAmt(this._flex매입등록H["NO_IO"].ToString(), this._flex매입등록H["FG_TAX"].ToString());

                this._flex매입등록H.SumRefresh();
                this._flex매입등록L.SumRefresh();
                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn부대비용제거_Click(object sender, EventArgs e)
        {
            string filter;
            DataRow[] dataRowArray;

            try
            {
                filter = "NO_IO = '" + this._flex매입등록H["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flex매입등록H["FG_TAX"].ToString() + "'";
                filter += " AND NO_PO = '" + D.GetString(this.cbo부대비용발주번호.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this.cbo부대비용항목.SelectedValue) + "'";

                dataRowArray = this._flex매입등록L.DataTable.Select(filter, string.Empty, DataViewRowState.Added);

                if (dataRowArray.Length > 0)
                {
                    dataRowArray[0].Delete();
                    this.SetHeaderAmt(D.GetString(this._flex매입등록H["NO_IO"]), D.GetString(this._flex매입등록H["FG_TAX"]));

                    this._flex매입등록H.SumRefresh();
                    this._flex매입등록L.SumRefresh();
                    this.선택금액계산();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn환율변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex매입등록H.HasNormalRow == false || this._flex매입등록L.HasNormalRow == false) return;

                this._flex매입등록H.Redraw = false;
                this._flex매입등록L.Redraw = false;

                foreach (DataRow dr in this._flex매입등록H.DataTable.Select("S = 'Y'"))
                {
                    foreach (DataRow dr1 in this._flex매입등록L.DataTable.Select("S = 'Y' AND NO_IO = '" + D.GetString(dr["NO_IO"]) + "'"))
                    {
                        dr1["CD_EXCH"] = this.cbo통화명.SelectedValue;
                        dr1["NM_EXCH"] = this.cbo통화명.Text;
                        dr1["RT_EXCH"] = this.cur환율.DecimalValue;
                        dr1["CHG_RTEXCH"] = "Y";

                        dr1["UM"] = this.원화계산(D.GetDecimal(dr1["UM_EX"]) * D.GetDecimal(dr1["RT_EXCH"]));
                        dr1["AM_IV"] = this.원화계산(D.GetDecimal(dr1["AM_EX"]) * D.GetDecimal(dr1["RT_EXCH"]));
                        dr1["VAT_IV"] = this.원화계산(D.GetDecimal(dr1["AM_IV"]) * (D.GetDecimal(dr1["TAX_RATE"]) == 0 ? 0 : D.GetDecimal(dr1["TAX_RATE"]) / 100));
                        dr1["AM_TOTAL"] = this.원화계산((D.GetDecimal(dr1["AM_IV"]) + D.GetDecimal(dr1["VAT_IV"])));
                    }

                    dr["AM_PO"] = this._flex매입등록L.DataTable.Compute("SUM(AM_IV)", "NO_IO = '" + D.GetString(dr["NO_IO"]) + "'");
                    dr["VAT"] = this._flex매입등록L.DataTable.Compute("SUM(VAT_IV)", "NO_IO = '" + D.GetString(dr["NO_IO"]) + "'");
                    dr["AM_TOT"] = (D.GetDecimal(dr["AM_PO"]) + D.GetDecimal(dr["VAT"]));
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flex매입등록H.Redraw = true;
                this._flex매입등록L.Redraw = true;

                this._flex매입등록H.SumRefresh();
                this._flex매입등록L.SumRefresh();

                this.선택금액계산();
            }
        }

        private void cbo부대비용항목_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
            {
                this.cur외화부대비용.ReadOnly = true;
                this.cur원화부대비용.ReadOnly = false;
            }
            else
            {
                this.cur외화부대비용.ReadOnly = false;
                this.cur원화부대비용.ReadOnly = true;
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                if (this.tabControl3.SelectedTab == this.tpg거래명세서)
				{
                    this._flex거래명세서H.Binding = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     this.ctx명세서거래처S.CodeValue,
                                                                                     this.dtp정산년월S.Text,
                                                                                     this.txt파일번호.Text,
                                                                                     (this.chk확인제외.Checked == true ? "Y" : "N") });
				}
                else
				{
                    this.splitContainer1.SplitterDistance = 881;
                    this.splitContainer2.SplitterDistance = 1545;
                    this.splitContainer3.SplitterDistance = 405;

                    this._flex세금계산서H.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    this.dtp작성일자.StartDateToString,
                                                                                    this.dtp작성일자.EndDateToString,
                                                                                    this.ctx거래처.CodeValue,
                                                                                    this.txt승인번호.Text,
                                                                                    this.cbo처리여부.SelectedValue.ToString(),
                                                                                    this.cbo거래처분류.SelectedValue.ToString() });

                    if (!this._flex세금계산서H.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		protected override bool SaveData()
		{
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
            
            if (this._flex세금계산서H.IsDataChanged == false && 
                this._flex상세내역.IsDataChanged == false &&
                this._flex거래명세서H.IsDataChanged == false &&
                this._flex파일번호별.IsDataChanged == false)
                return false;

            if (!this._biz.Save(this._flex세금계산서H.GetChanges(), 
                                this._flex거래명세서H.GetChanges(), 
                                this._flex상세내역.GetChanges(),
                                this._flex파일번호별.GetChanges())) return false;

            this._flex세금계산서H.AcceptChanges();
            this._flex상세내역.AcceptChanges();
            this._flex거래명세서H.AcceptChanges();
            this._flex파일번호별.AcceptChanges();

            return true;


        }

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            임시파일제거();
            return base.OnToolBarExitButtonClicked(sender, e);
        }

        private void _flex세금계산서H_AfterRowChange(object sender, RangeEventArgs e)
		{
			string key, filter;
			DataTable dt = null;

			try
			{
				key = D.GetString(this._flex세금계산서H["NO_ETAX"]);
				filter = "NO_ETAX ='" + key + "'";

                this.web미리보기.Navigate(string.Empty);

                if (this._flex세금계산서H.DetailQueryNeed)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key });
				}

				this._flex세금계산서L.BindingAdd(dt, filter);

                if (this._flex매입등록H.DataTable != null && 
                    this._flex매입등록H.DataTable.Rows.Count > 0)
				{
                    this._flex매입등록H.RowFilter = string.Format("CD_PARTNER = '{0}'", this._flex세금계산서H["CD_PARTNER"].ToString());
                    this._flex매입등록L.EmptyRowFilter();
                    this._flex매입등록H_AfterRowChange(null, null);

                    this.선택금액계산();
                }

                this.cur환율.Text = this._biz.환율(this._flex세금계산서H["DT_SEND"].ToString(), this.cbo통화명.SelectedValue.ToString()).ToString();

                #region 수신자
                string query = @"SELECT A.value AS CODE,
	   MU.NM_USER AS NAME
FROM string_split('{0}', '|') A
JOIN MA_USER MU WITH(NOLOCK) ON MU.CD_COMPANY = '{1}' AND MU.ID_USER = A.value";

                this.bpc수신자.Clear();

                DataTable dt1 = Global.MainFrame.FillDataTable(string.Format(query, this._flex세금계산서H["NO_EMP"].ToString(),
                                                                                    Global.MainFrame.LoginInfo.CompanyCode));

                foreach (DataRow dr in dt1.Rows)
                {
                    this.bpc수신자.AddItem(D.GetString(dr["CODE"]), D.GetString(dr["NAME"]));
                }
                #endregion

                this.ctx발주번호.CodeValue = this._flex세금계산서H["NO_PO"].ToString();
                this.ctx발주번호.CodeName = this._flex세금계산서H["NO_PO"].ToString();
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        private void _flex매입등록H_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                this._flex매입등록L.DataTable.Select("NO_IO= '" + this._flex매입등록H[this._flex매입등록H.Row, "NO_IO"].ToString() + "' AND CD_PARTNER= '" + this._flex매입등록H[this._flex매입등록H.Row, "CD_PARTNER"].ToString() + "' AND  FG_TAX = ISNULL('" + this._flex매입등록H[this._flex매입등록H.Row, "FG_TAX"].ToString() + "','')", "", DataViewRowState.CurrentRows);

                if (this._flex매입등록H[e.Row, "S"].ToString() == "N")
                {
                    this._flex매입등록L.Redraw = false;
                    for (int i = this._flex매입등록L.Rows.Fixed; i < this._flex매입등록L.DataView.Count + this._flex매입등록L.Rows.Fixed; ++i)
                        this._flex매입등록L.SetCellCheck(i, 1, CheckEnum.Checked);
                    this._flex매입등록L.Redraw = true;
                }
                else
                {
                    this._flex매입등록L.Redraw = false;
                    for (int i = this._flex매입등록L.Rows.Fixed; i < this._flex매입등록L.DataView.Count + this._flex매입등록L.Rows.Fixed; ++i)
                        this._flex매입등록L.SetCellCheck(i, 1, CheckEnum.Unchecked);
                    this._flex매입등록L.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex매입등록H_AfterEdit(object sender, RowColEventArgs e)
        {
            string 통제설정_총마감금액;

            try
            {
                통제설정_총마감금액 = BASIC.GetMAEXC("매입등록-총마감금액지정");

                if (통제설정_총마감금액 == "100" || 통제설정_총마감금액 == "200")
                {
                    DataRow[] dataRowArray = this._flex매입등록H.DataTable.Select("S = 'Y'");
                    if (dataRowArray.Length == 0)
                    {
                        return;
                    }
                    if (ComFunc.getGridGroupBy(dataRowArray, new string[1] { "CD_PARTNER" }, 1 != 0).Rows.Count > 1)
                    {
                        this._flex매입등록H[this._flex매입등록H.Row, "S"] = "N";
                        Global.MainFrame.ShowMessage("CZ_하나의 @만 선택할 수 있습니다.", Global.MainFrame.DD("매입처"));
                    }
                }

                for (int @fixed = this._flex매입등록L.Rows.Fixed; @fixed < this._flex매입등록L.Rows.Count; ++@fixed)
                    this._flex매입등록L.SetData(@fixed, 1, D.GetString(this._flex매입등록H[this._flex매입등록H.Row, "S"]));

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex매입등록H_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;

            try
            {
                if (!this._flex매입등록H.IsBindingEnd || !this._flex매입등록H.HasNormalRow)
                {
                    this._flex매입등록L.EmptyRowFilter();
                }
                else
                {
                    string key = this._flex매입등록H["NO_IO"].ToString();
                    string key1 = D.GetString(this._flex매입등록H["FG_TAX"]);
                    filter = "NO_IO = '" + key + "' AND ISNULL(FG_TAX,'') = '" + key1 + "'";

                    this._flex매입등록L.RowFilter = filter;

                    if (this._flex매입등록L.HasNormalRow == true)
                    {
                        dt = ComFunc.getGridGroupBy(this._flex매입등록L.DataTable.Select(this._flex매입등록L.RowFilter), new string[] { "NO_PO", "CD_EXCH", "RT_EXCH" }, true);
                        dt.Columns.Add("NM_DISPLAY");

                        foreach (DataRow dr in dt.Rows)
                        {
                            filter = "NO_IO = '" + D.GetString(this._flex매입등록H["NO_IO"]) + "' AND NO_PO = '" + D.GetString(dr["NO_PO"]) + "'";
                            dr["NM_DISPLAY"] = D.GetString(dr["NO_PO"]) + " (" + string.Format("{0:" + this._flex매입등록L.Cols["AM_TOTAL"].Format + "}", D.GetDecimal(this._flex매입등록L.DataTable.Compute("SUM(AM_TOTAL)", filter))) + ")";
                        }

                        this.cbo부대비용발주번호.DataSource = dt;
                        this.cbo부대비용발주번호.DisplayMember = "NM_DISPLAY";
                        this.cbo부대비용발주번호.ValueMember = "NO_PO";

                        if (this.cbo부대비용발주번호.SelectedItem != null)
                        {
                            this.cbo부대비용통화명.SelectedValue = D.GetString(((DataRowView)(this.cbo부대비용발주번호.SelectedItem)).Row["CD_EXCH"]);
                            this.cur부대비용환율.DecimalValue = D.GetDecimal(((DataRowView)(this.cbo부대비용발주번호.SelectedItem)).Row["RT_EXCH"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex매입등록L_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flex매입등록L.Cols[e.Col].Name != "S") return;

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex매입등록L_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            decimal oldValue, newValue;
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;
                string @string = D.GetString(this._flex매입등록L.GetData(e.Row, e.Col));

                switch (_flex.Cols[e.Col].Name)
                {
                    case "S":
                        _flex["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                        if (_flex.Name == this._flex매입등록L.Name)
                        {
                            DataRow[] drArr = _flex.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                            if (drArr.Length != 0)
                                this._flex매입등록H.SetCellCheck(_flex매입등록H.Row, _flex매입등록H.Cols["S"].Index, CheckEnum.Checked);
                            else
                                this._flex매입등록H.SetCellCheck(_flex매입등록H.Row, _flex매입등록H.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                        break;
                    case "QT_IV":
                        if (!this.CheckRowDataQT(double.Parse(this._flex매입등록L[this._flex매입등록L.Row, "QT_INV"].ToString()), double.Parse(this._flex매입등록L.EditData)))
                        {
                            Global.MainFrame.ShowMessage("PU_M000081");
                            e.Cancel = true;
                            break;
                        }

                        if (D.GetString(this._flex매입등록L["YN_RETURN"]) == "Y" && D.GetDecimal(this._flex매입등록L.EditData) > 0)
                        {
                            Global.MainFrame.ShowMessage("CZ_@ 은(는) 0보다 작거나 같아야 합니다.", Global.MainFrame.DD("반품수량"));
                            this._flex매입등록L["QT_IV"] = D.GetDecimal(@string);
                            e.Cancel = true;
                            break;
                        }

                        this.ChangeQT_IV(D.GetDecimal(this._flex매입등록L.EditData));
                        this.선택금액계산();
                        break;
                    case "VAT_IV":
                        oldValue = D.GetDecimal(_flex.GetData(e.Row, e.Col).ToString());
                        newValue = D.GetDecimal(_flex.EditData);

                        if (newValue > oldValue + 99 || newValue < oldValue - 99)
						{
                            this.ShowMessage("수정가능금액은 100원 범위 입니다.");

                            this._flex매입등록L["VAT_IV"] = oldValue;
                            e.Cancel = true;
                        }
                        break;
                    case "AM_IV":
                        oldValue = D.GetDecimal(_flex.GetData(e.Row, e.Col).ToString());
                        newValue = D.GetDecimal(_flex.EditData);

                        if (this._flex매입등록L["CD_EXCH"].ToString() == "000")
						{
                            this.ShowMessage("외화건만 금액 수정 가능 합니다.");

                            this._flex매입등록L["AM_IV"] = oldValue;
                            e.Cancel = true;
                        }

                        if (newValue > oldValue + 99 || newValue < oldValue - 99)
                        {
                            this.ShowMessage("수정가능금액은 100원 범위 입니다.");

                            this._flex매입등록L["AM_IV"] = oldValue;
                            e.Cancel = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool CheckRowDataQT(double oldQT, double newQT)
        {
            try
            {
                if (this._flex매입등록L[this._flex매입등록L.Row, "YN_RETURN"].ToString().Trim() == "Y")
                {
                    if (newQT < oldQT)
                        return false;
                }
                else if (newQT > oldQT)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return true;
        }

        private void ChangeQT_IV(Decimal 적용수량)
        {
            try
            {
                Decimal num = 적용수량 * D.GetDecimal(this._flex매입등록L["RATE_EXCHG"]);
                Decimal decimal1 = D.GetDecimal(this._flex매입등록L["UM_EX"]);
                Decimal decimal2 = D.GetDecimal(this._flex매입등록L["RT_EXCH"]);
                string @string = D.GetString(this._flex매입등록L["FG_TAX"]);
                bool 부가세포함 = D.GetString(this._flex매입등록L["TP_UM_TAX"]) == "001";
                Decimal decimal3 = D.GetDecimal(this._flex매입등록L["TAX_RATE"]);
                Duzon.ERPU.MF.Common.Calc.GetAmt(적용수량, decimal1, decimal2, @string, decimal3, 모듈.PUR, 부가세포함, out this.외화금액, out this.원화금액, out this.부가세);
                this._flex매입등록L["VAT_IV"] = this.부가세;
                this._flex매입등록L["AM_EX"] = this.외화금액;
                this._flex매입등록L["AM_IV"] = this.원화금액;
                this._flex매입등록L["AM_TOTAL"] = this.원화계산((this.원화금액 + this.부가세) - D.GetDecimal(this._flex매입등록L["AM_ADPAY"]));
                this._flex매입등록L["QT_CLS"] = num;
                this.ChangeHeadAM_K();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ChangeHeadAM_K()
        {
            try
            {
                this._flex매입등록H[this._flex매입등록H.Row, "AM_PO"] = this.원화계산((decimal)this._flex매입등록L.DataTable.Compute("SUM(AM_IV)", "NO_IO= '" + this._flex매입등록H["NO_IO"].ToString().Trim() + "' AND CD_PARTNER= '" + this._flex매입등록H["CD_PARTNER"].ToString().Trim() + "' AND FG_TAX ='" + this._flex매입등록H["FG_TAX"].ToString().Trim() + "'"));
                this._flex매입등록H[this._flex매입등록H.Row, "VAT"] = this.원화계산((decimal)this._flex매입등록L.DataTable.Compute("SUM(VAT_IV)", "NO_IO= '" + this._flex매입등록H["NO_IO"].ToString().Trim() + "' AND CD_PARTNER= '" + this._flex매입등록H["CD_PARTNER"].ToString().Trim() + "' AND FG_TAX ='" + this._flex매입등록H["FG_TAX"].ToString().Trim() + "'"));
                this._flex매입등록H[this._flex매입등록H.Row, "AM_TOT"] = this.원화계산(D.GetDecimal(this._flex매입등록H[this._flex매입등록H.Row, "AM_PO"]) + D.GetDecimal(this._flex매입등록H[this._flex매입등록H.Row, "VAT"]));

                this._flex매입등록L["UM"] = this.원화계산(D.GetDecimal(this._flex매입등록L["UM_EX"]) * D.GetDecimal(this._flex매입등록L["RT_EXCH"]));
            }
            catch (coDbException ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn세금계산서업로드_Click(object sender, EventArgs e)
		{
            string query;

			try
			{
                foreach (string fileName in Directory.GetFiles("C:\\RPA_DOWNLOAD\\", "*.pdf"))
				{
                    FileInfo fileInfo = new FileInfo(fileName);

                    string 계산서번호 = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

                    query = @"SELECT CD_FILE 
FROM MA_FILEINFO WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_MODULE = 'CZ'
AND ID_MENU = 'P_CZ_PU_IV_AUTO_MNG'
AND CD_FILE = '{1}'";

                    DataTable dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, 계산서번호 }));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //this.ShowMessage("이미 업로드한 파일 입니다.");
                        this.로그입력(계산서번호, "이미 업로드한 파일 입니다.");
                        //Messenger.SendMSG(new string[] { "S-391", "S-458" }, string.Format("전자세금계산서 업로드 오류 : 이미 업로드한 파일 입니다. {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + 계산서번호));
                        continue;
                    }

                    string 업로드위치 = "Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서번호.Substring(0, 4);
                    FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, 계산서번호);
                    this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, 계산서번호, fileInfo, 업로드위치, "P_CZ_PU_IV_AUTO_MNG");

                    string filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + 계산서번호 + "\\";
                    if (Directory.Exists(filePath))
                    {
                        string[] files = Directory.GetFiles(filePath);

                        foreach (string file in files)
                            File.Delete(file);
                    }
                    else
                        Directory.CreateDirectory(filePath);

                    FileUploader.DownloadFile(fileInfo.Name, filePath, "Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서번호.Substring(0, 4), 계산서번호);

                    if (!File.Exists(filePath + fileInfo.Name))
                    {
                        //this.ShowMessage("전자세금계산서 파일이 정상적으로 업로드 되지 않았습니다.");
                        this.로그입력(계산서번호, "전자세금계산서 파일이 정상적으로 업로드 되지 않았습니다.");
                        Messenger.SendMSG(new string[] { "S-391", "S-458" }, string.Format("전자세금계산서 업로드 오류 : 전자세금계산서 파일이 정상적으로 업로드 되지 않았습니다. {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + 계산서번호));
                        continue;
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn세금계산서업로드.Text);
            }
			catch (Exception ex)
			{
                Messenger.SendMSG(new string[] { "S-391", "S-458" }, string.Format("전자세금계산서 업로드 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + ex.Message));
                MsgEnd(ex);
			}
		}

        private void Btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControl3.SelectedTab == this.tpg거래명세서)
				{
                    if (!this._flex거래명세서H.HasNormalRow) return;

                    P_CZ_MA_FILE_SUB dialog = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_PU_IV_AUTO_MNG", this._flex거래명세서H["CD_PARTNER"].ToString() + "_" + this._flex거래명세서H["DT_MONTH"].ToString(), "P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._flex거래명세서H["DT_MONTH"].ToString());
                    dialog.ShowDialog();
                }
                else
				{
                    if (!this._flex세금계산서H.HasNormalRow) return;

                    P_CZ_MA_FILE_SUB dialog = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_PU_IV_AUTO_MNG", this._flex세금계산서H["NO_ETAX"].ToString(), "P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._flex세금계산서H["NO_ETAX"].ToString().Substring(0, 4));
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn미리보기_Click(object sender, EventArgs e)
        {
            string 계산서번호, fileName, serverPath;

            try
            {
                if (this.tabControl3.SelectedTab == this.tpg거래명세서)
                {
                    if (!this._flex거래명세서H.HasNormalRow) return;

                    fileName = this._flex거래명세서H["NM_FILE"].ToString();
                    serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._flex거래명세서H["DT_MONTH"].ToString() + "/" + this._flex거래명세서H["CD_PARTNER"].ToString() + "_" + this._flex거래명세서H["DT_MONTH"].ToString() + "/";

                    if (string.IsNullOrEmpty(fileName))
                    {
                        this.web거래명세서.Navigate(string.Empty);
                        return;
                    }

                    this.web거래명세서.Navigate(serverPath + fileName);
                }
                else
				{
                    if (!this._flex세금계산서H.HasNormalRow) return;

                    계산서번호 = this._flex세금계산서H["NO_ETAX"].ToString();

                    fileName = string.Format("{0}.pdf", 계산서번호);
                    serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서번호.Substring(0, 4) + "/" + 계산서번호 + "/";

                    if (string.IsNullOrEmpty(fileName))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }

                    this.web미리보기.Navigate(serverPath + fileName);
                }   
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn자동매입_Click(object sender, EventArgs e)
        {
            string query, 입고번호, 매입번호, 계산서발행일자, 전표번호, 계산서번호, 회계단위, 등록일자;
            decimal 공급가액, 부가세, 총금액, 선지급금액, 회계번호;

            try
            {
                DataTable dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               "20160101",
                                                               Global.MainFrame.GetStringToday,
                                                               string.Empty,
                                                               string.Empty,
                                                               "002",
                                                               string.Empty });

                int index = 0;

                foreach (DataRow dr in dt.Rows)
				{
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dt.Rows.Count) });

                    if (string.IsNullOrEmpty(dr["NO_ETAX"].ToString()))
                        continue;

                    공급가액 = 0;
                    부가세 = 0;
                    총금액 = 0;
                    선지급금액 = 0;

                    계산서번호 = dr["NO_ETAX"].ToString();

                    #region 입고번호 찾기
                    FINDER.ETaxRelatedNumber(계산서번호);

                    query = string.Format(@"SELECT DT_SEND,
	   NO_IO,
       LEFT(DTS_INSERT, 8) AS DT_INSERT
FROM CZ_PU_ETAXH WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND NO_ETAX = '{1}'", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                     계산서번호 });

                    DataTable dt1 = DBHelper.GetDataTable(query);

                    if (dt1 == null || dt1.Rows.Count == 0)
                    {
                        this.로그입력(계산서번호, "계산서번호로 조회되는 건이 없습니다.");
                        continue;
                    }

                    입고번호 = dt1.Rows[0]["NO_IO"].ToString();

                    if (string.IsNullOrEmpty(입고번호))
                    {
                        this.로그입력(계산서번호, "입고번호가 없습니다.");
                        continue;
                    }
                    #endregion

                    #region 매입등록 가능 여부 확인
                    계산서발행일자 = dt1.Rows[0]["DT_SEND"].ToString();
                    등록일자 = dt1.Rows[0]["DT_INSERT"].ToString();

                    dt1 = DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     입고번호 });

                    if (dt1 == null || dt1.Rows.Count == 0)
                    {
                        this.로그입력(계산서번호, "매입등록 대상이 없습니다. (미입고 또는 정산완료)");
                        continue;
                    }

                    if (ComFunc.getGridGroupBy(dt1, new string[] { "CD_PARTNER", "FG_TRANS", "FG_TPPURCHASE", "FG_TAX", "CD_EXCH", "RT_EXCH" }, true).Rows.Count > 1)
                    {
                        this.로그입력(계산서번호, "서로 다른 데이터 존재 (거래처, 거래구분, 매입형태, 과세구분, 통화명, 환율)");
                        continue;
                    }

                    query = @"SELECT A.DT_CAL 
FROM (SELECT TOP 1 DT_CAL 
      FROM MA_CALENDAR WITH(NOLOCK)
      WHERE CD_COMPANY = '{0}' 
      AND FG1_HOLIDAY = 'W'
      AND DT_CAL >= CONVERT(CHAR(6), DATEADD(MONTH, 1, '{1}'), 112) + '10'
      ORDER BY DT_CAL) A
WHERE A.DT_CAL < '{2}'";

                    object 마감일자 = DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, 계산서발행일자, 등록일자 }));

                    if (마감일자 != null)
					{
                        this.로그입력(계산서번호, string.Format("정산일자 기준으로 마감일자가 지난 건 입니다. 마감일자 : {0}", 마감일자.ToString()));
                        continue;
                    }
                    #endregion

                    #region 금액 확인
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        공급가액 += D.GetDecimal(dr1["AM"]);
                        부가세 += D.GetDecimal(dr1["VAT"]);
                        총금액 += D.GetDecimal(dr1["AM_TOT"]);
                        선지급금액 += D.GetDecimal(dr1["AM_ADPAY"]);
                    }

                    if (공급가액 != D.GetDecimal(dr["AM"]))
					{
                        this.로그입력(계산서번호, "공급가액이 다른 건 입니다.");
                        continue;
                    }

                    if (부가세 != D.GetDecimal(dr["VAT"]))
                    {
                        this.로그입력(계산서번호, "부가세가 다른 건 입니다.");
                        continue;
                    }

                    if (선지급금액 > 0)
                    {
                        this.로그입력(계산서번호, "선지급 금액이 있는 건 입니다.");
                        continue;
                    }
                    #endregion

                    #region 임시폴더 생성
                    string filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + 계산서번호 + "\\";
                    if (Directory.Exists(filePath))
                    {
                        string[] files = Directory.GetFiles(filePath);

                        foreach (string file in files)
                            File.Delete(file);
                    }
                    else
                        Directory.CreateDirectory(filePath);
                    #endregion

                    #region 세금계산서 다운로드
                    try
					{
                        FileUploader.DownloadFile(string.Format("{0}.pdf", 계산서번호), filePath, "Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서번호.Substring(0, 4), 계산서번호);

                        if (!File.Exists(filePath + string.Format("{0}.pdf", 계산서번호)))
                        {
                            this.로그입력(계산서번호, "전자세금계산서 파일이 없습니다.");
                            continue;
                        }
                    }
                    catch
					{
                        this.로그입력(계산서번호, "전자세금계산서 파일이 서버에 없음");
                        continue;
                    }
                    #endregion

                    #region 매입등록
                    string 지급예정일 = this.지급예정일계산(계산서발행일자, 총금액, D.GetInt(dt1.Rows[0]["DT_PAY_PREARRANGED"]));

                    query = string.Format("EXEC SP_CZ_PU_IV_AUTO '{0}', '{1}', '{2}', '{3}', '{4}'", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                    입고번호,
                                                                                                                    계산서발행일자,
                                                                                                                    지급예정일,
                                                                                                                    Global.MainFrame.LoginInfo.UserID });

                    매입번호 = DBHelper.ExecuteScalar(query).ToString();

                    DBHelper.ExecuteNonQuery("UP_PU_IVH_MODIFY", new object[] { 매입번호,
                                                                                Global.MainFrame.LoginInfo.CompanyCode });
                    #endregion

                    #region 전표처리
                    DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_MNG_TRANS_DOCU", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          매입번호,
                                                                                          "210",
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          Global.MainFrame.LoginInfo.EmployeeNo });
                    #endregion

                    #region 전표승인
                    query = string.Format(@"SELECT FD.NO_DOCU,
       FD.CD_PC
FROM PU_IVH IH WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				  FD.NO_DOCU,
				  FD.CD_PC
           FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, 
				    FD.NO_DOCU,
					FD.CD_PC) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
WHERE IH.CD_COMPANY = '{0}'
AND IH.NO_IV = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, 매입번호);

                    DataTable dt2 = DBHelper.GetDataTable(query);

                    전표번호 = dt2.Rows[0]["NO_DOCU"].ToString();
                    회계단위 = dt2.Rows[0]["CD_PC"].ToString();

                    if (dr["CD_PARTNER"].ToString() != "20505" &&
                        dr["CD_PARTNER"].ToString() != "08488")
					{
                        object[] obj = new object[1];
                        DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          회계단위,
                                                                                          "FI04",
                                                                                          계산서발행일자 }, out obj);

                        회계번호 = D.GetDecimal(obj[0]);

                        DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { 전표번호,
                                                                                        회계단위,
                                                                                        Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        계산서발행일자,
                                                                                        회계번호,
                                                                                        "2",
                                                                                        Global.MainFrame.LoginInfo.UserID,
                                                                                        Global.MainFrame.LoginInfo.UserID });
                    }
					#endregion

					#region 전표출력
					dt2 = DBHelper.GetDataTable("UP_FI_DOCU_PRINT_NEW", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       Global.MainFrame.LoginInfo.CdPc,
                                                                                       전표번호,
                                                                                       (!IFRSConfig.IFRSPage ? "KGAAP" : "IFRS"),
                                                                                       Global.SystemLanguage.MultiLanguageLpoint });

                    Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTableDocuMngd(dt1);

                    dt2.Columns.Add("NMD_MNGD", typeof(string));

                    foreach (DataRow dr1 in dt2.Rows)
                        dr1["NMD_MNGD"] = this.GetNMD_MNGD(dr1);

                    ReportHelper reportHelper = Util.SetReportHelper(string.Format("R_CZ_PU_IV_MNG_1_{0}", Global.MainFrame.LoginInfo.CompanyCode), "매입관리-전표출력", Global.MainFrame.LoginInfo.CompanyCode);

                    reportHelper.SetDataTable("FI_DOCU", dt2);

                    reportHelper.PrintHelper.UseUserFontStyle();

                    reportHelper.PrintDirect(string.Format("R_CZ_PU_IV_MNG_1_{0}_SLIP.DRF", Global.MainFrame.LoginInfo.CompanyCode), false, true, filePath + 전표번호 + "_TMP.pdf", new Dictionary<string, string>());

                    PDF.Merge(filePath + 전표번호 + ".pdf", new string[] { filePath + 전표번호 + "_TMP.pdf",
                                                                           filePath + string.Format("{0}.pdf", 계산서번호) });
                    #endregion

                    #region 첨부파일 등록
                    FileInfo fileInfo = new FileInfo(filePath + 전표번호 + ".pdf");

                    string 업로드위치 = "Upload/P_CZ_PU_IV_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서발행일자.Substring(0, 4);
                    FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, 매입번호);
                    this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, 매입번호, fileInfo, 업로드위치, "P_CZ_PU_IV_MNG");
                    #endregion

                    #region 테이블 업데이트
                    query = @"UPDATE CZ_PU_ETAXH
                              SET YN_READ = 'A',
                                  NO_IV = '{2}',
                                  NO_DOCU = '{3}',
                                  ID_UPDATE = '{4}',
                                  DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                              WHERE CD_COMPANY = '{0}'
                              AND NO_ETAX = '{1}'";

                    DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               계산서번호,
                                                                               매입번호,
                                                                               전표번호,
                                                                               Global.MainFrame.LoginInfo.UserID }));
                    #endregion
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn자동매입.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void 로그입력(string 계산서번호, string 로그)
        {
            string query;

            try
            {
                query = @"UPDATE CZ_PU_ETAXH
SET DC_LOG = '{2}',
	ID_UPDATE = '{3}',
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = '{0}'
AND NO_ETAX = '{1}'
AND ISNULL(DC_LOG, '') = ''";

                DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           계산서번호,
                                                                           로그,
                                                                           Global.MainFrame.LoginInfo.UserID }));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn수동매입_Click(object sender, EventArgs e)
        {
            string query;
            
            try
            {
                if (!this._flex매입등록H.IsBindingEnd) return;

                DataRow[] dataRowArray = this._flex매입등록L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (ComFunc.getGridGroupBy(dataRowArray, new string[] { "RT_EXCH" }, 1 != 0).Rows.Count > 1)
                {
                    Global.MainFrame.ShowMessage("환율이 동일하여야 합니다");
                    return;
                }
                else
                {
                    string 계산서번호 = this._flex세금계산서H["NO_ETAX"].ToString();
                    string filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + 계산서번호 + "\\";
                    if (Directory.Exists(filePath))
                    {
                        string[] files = Directory.GetFiles(filePath);

                        foreach (string file in files)
                            File.Delete(file);
                    }
                    else
                        Directory.CreateDirectory(filePath);

                    FileUploader.DownloadFile(string.Format("{0}.pdf", 계산서번호), filePath, "Upload/P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서번호.Substring(0, 4), 계산서번호);

                    if (!File.Exists(filePath + string.Format("{0}.pdf", 계산서번호)))
                    {
                        this.ShowMessage("전자세금계산서 파일이 없습니다.");
                        return;
                    }

                    DataTable dt = this._flex매입등록L.DataTable.Clone();

                    decimal 공급가액 = 0, 부가세 = 0, 총금액 = 0;
                    bool 선지급여부 = false;

                    foreach (DataRow row in dataRowArray)
                    {
                        if (D.GetDecimal(row["AM_ADPAY"]) > 0)
                            선지급여부 = true;

                        공급가액 += D.GetDecimal(row["AM_IV"]);
                        부가세 += D.GetDecimal(row["VAT_IV"]);
                        총금액 += D.GetDecimal(row["AM_TOTAL"]);

                        if (((DataTable)this.cbo부대비용항목.DataSource).Select("CD_ITEM = '" + row["CD_ITEM"] + "'").Length > 0 && row.RowState == DataRowState.Added)
                            row["QT_IV"] = 0;

                        row.AcceptChanges();

                        dt.ImportRow(row);
                    }

                    if (공급가액 != D.GetDecimal(this._flex세금계산서H["AM"]))
					{
                        this.ShowMessage("공급가액이 일치해야 합니다.");
                        return;
                    }

                    if (부가세 != D.GetDecimal(this._flex세금계산서H["VAT"]))
                    {
                        this.ShowMessage("부가세 금액이 일치해야 합니다.");
                        return;
                    }

                    string 계산서발행일자 = this._flex세금계산서H["DT_SEND"].ToString();
                    string 지급예정일 = this.지급예정일계산(계산서발행일자, 총금액, D.GetInt(dt.Rows[0]["DT_PAY_PREARRANGED"]));

                    string xml = Util.GetTO_Xml(dt);
                    xml = xml.Replace("'", "''");
                    query = string.Format("EXEC SP_CZ_PU_IV_AUTO_XML '{0}', '{1}', '{2}', '{3}', '{4}'", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                        xml,
                                                                                                                        계산서발행일자,
                                                                                                                        지급예정일,
                                                                                                                        Global.MainFrame.LoginInfo.UserID });

                    string 매입번호 = DBHelper.ExecuteScalar(query).ToString();

					DBHelper.ExecuteNonQuery("UP_PU_IVH_MODIFY", new object[] { 매입번호,
																				Global.MainFrame.LoginInfo.CompanyCode });

					#region 전표처리
					DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_MNG_TRANS_DOCU", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          매입번호,
                                                                                          "210",
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          Global.MainFrame.LoginInfo.EmployeeNo });
                    #endregion

                    #region 전표승인
                    query = string.Format(@"SELECT FD.NO_DOCU,
       FD.CD_PC
FROM PU_IVH IH WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				  FD.NO_DOCU,
				  FD.CD_PC
           FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, 
				    FD.NO_DOCU,
					FD.CD_PC) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
WHERE IH.CD_COMPANY = '{0}'
AND IH.NO_IV = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, 매입번호);

                    DataTable dt2 = DBHelper.GetDataTable(query);

                    string 전표번호 = dt2.Rows[0]["NO_DOCU"].ToString();
                    string 회계단위 = dt2.Rows[0]["CD_PC"].ToString();

                    if (this._flex세금계산서H["CD_PARTNER"].ToString() != "20505" &&
                        this._flex세금계산서H["CD_PARTNER"].ToString() != "08488")
                    {
                        object[] obj = new object[1];
                        DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          회계단위,
                                                                                          "FI04",
                                                                                          계산서발행일자 }, out obj);

                        decimal 회계번호 = D.GetDecimal(obj[0]);

                        DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { 전표번호,
                                                                                        회계단위,
                                                                                        Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        계산서발행일자,
                                                                                        회계번호,
                                                                                        "2",
                                                                                        Global.MainFrame.LoginInfo.UserID,
                                                                                        Global.MainFrame.LoginInfo.UserID });
                    }
                    #endregion

                    #region 전표출력
                    DataTable dt1 = DBHelper.GetDataTable("UP_FI_DOCU_PRINT_NEW", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 Global.MainFrame.LoginInfo.CdPc,
                                                                                                 전표번호,
                                                                                                 (!IFRSConfig.IFRSPage ? "KGAAP" : "IFRS"),
                                                                                                 Global.SystemLanguage.MultiLanguageLpoint });

                    Duzon.ERPU.UEncryption.UEncryption.SearchDecryptionTableDocuMngd(dt1);

                    dt1.Columns.Add("NMD_MNGD", typeof(string));

                    foreach (DataRow dr in dt1.Rows)
                        dr["NMD_MNGD"] = this.GetNMD_MNGD(dr);

                    ReportHelper reportHelper = Util.SetReportHelper(string.Format("R_CZ_PU_IV_MNG_1_{0}", Global.MainFrame.LoginInfo.CompanyCode), "매입관리-전표출력", Global.MainFrame.LoginInfo.CompanyCode);

                    reportHelper.SetDataTable("FI_DOCU", dt1);

                    reportHelper.PrintHelper.UseUserFontStyle();

                    reportHelper.PrintDirect(string.Format("R_CZ_PU_IV_MNG_1_{0}_SLIP.DRF", Global.MainFrame.LoginInfo.CompanyCode), false, true, filePath + 전표번호 + "_TMP.pdf", new Dictionary<string, string>());

                    PDF.Merge(filePath + 전표번호 + ".pdf", new string[] { filePath + 전표번호 + "_TMP.pdf",
                                                                           filePath + string.Format("{0}.pdf", 계산서번호) });
                    #endregion

                    #region 첨부파일 등록
                    FileInfo fileInfo = new FileInfo(filePath + 전표번호 + ".pdf");

                    string 업로드위치 = "Upload/P_CZ_PU_IV_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 계산서발행일자.Substring(0, 4);
                    FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, 매입번호);
                    this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, 매입번호, fileInfo, 업로드위치, "P_CZ_PU_IV_MNG");
                    #endregion

                    #region 테이블 업데이트
                    query = @"UPDATE CZ_PU_ETAXH
                              SET YN_READ = 'M',
                                  NO_IV = '{2}',
                                  NO_DOCU = '{3}',
                                  ID_UPDATE = '{4}',
                                  DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                              WHERE CD_COMPANY = '{0}'
                              AND NO_ETAX = '{1}'";

                    DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               계산서번호,
                                                                               매입번호,
                                                                               전표번호,
                                                                               Global.MainFrame.LoginInfo.UserID }));
                    #endregion

                    #region 매입등록 완료 데이터 삭제
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }

                    foreach (DataRow dr in this._flex매입등록H.DataTable.Select("S = 'Y'"))
                    {
                        if (this._flex매입등록L.DataTable.Select(string.Format("NO_IO = '{0}'", dr["NO_IO"])).Length == 0)
                            dr.Delete();
                    }
                    #endregion

                    this.선택금액계산();

                    if (선지급여부 == true)
                    {
                        this.ShowMessage("수동매입 작업을 완료하였습니다.\n선지급 정리 대상 건이 포함되어 있기 때문에 정리 화면으로 자동 링크 됩니다.");

                        if (this.IsExistPage("P_CZ_PU_ADPAYMENT_BILL", false))
                            this.UnLoadPage("P_CZ_PU_ADPAYMENT_BILL", false);

                        this.LoadPageFrom("P_CZ_PU_ADPAYMENT_BILL", this.DD("선지급정리"), Grant, new object[] { new object[] { this._flex세금계산서H["CD_PARTNER"].ToString(),
                                                                                                                                this._flex세금계산서H["LN_PARTNER"].ToString(),
                                                                                                                                "001",
                                                                                                                                this._flex세금계산서H["DT_SEND"].ToString() } });
                    }
                    else
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn수동매입.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSet = this._biz.SearchIv(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    this.dtp입고기간.StartDateToString,
                                                                    this.dtp입고기간.EndDateToString,
                                                                    this.ctx매입처.CodeValue,
                                                                    this.txt발주번호.Text,
                                                                    string.Empty,
                                                                    "001" });

                this._flex매입등록H.Binding = dataSet.Tables[0];
                this._flex매입등록L.Binding = dataSet.Tables[1];

                if (!this._flex매입등록H.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn전체선택_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex매입등록H.HasNormalRow) return;

                MsgControl.ShowMsg("전체선택 작업 중 입니다.\n잠시만 기다려 주세요.");

                this._flex매입등록H.Redraw = false;
                this._flex매입등록L.Redraw = false;

                for (int h = this._flex매입등록H.Rows.Fixed; h < this._flex매입등록H.Rows.Count; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h - 1), D.GetString(this._flex매입등록H.Rows.Count - 2) });

                    this._flex매입등록H.Row = h; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                    this._flex매입등록H[h, "S"] = "Y";
                    this._flex매입등록H.SetCellCheck(h, this._flex매입등록H.Cols["S"].Index, CheckEnum.Checked);

                    for (int l = this._flex매입등록L.Rows.Fixed; l < this._flex매입등록L.Rows.Count; l++)
                    {
                        this._flex매입등록L[l, "S"] = "Y";
                        this._flex매입등록L.SetCellCheck(l, this._flex매입등록L.Cols["S"].Index, CheckEnum.Checked);
                    }
                }

                this._flex매입등록H.Redraw = true;
                this._flex매입등록L.Redraw = true;

                MsgControl.CloseMsg();
                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { Global.MainFrame.DD("전체선택") });

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flex매입등록H.Redraw = true;
                this._flex매입등록L.Redraw = true;

                MsgControl.CloseMsg();
            }
        }

        private void btn전체해제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex매입등록H.HasNormalRow || !this._flex매입등록L.HasNormalRow) return;

                MsgControl.ShowMsg("전체해제 작업 중 입니다.\n잠시만 기다려 주세요.");

                this._flex매입등록H.Redraw = false;
                this._flex매입등록L.Redraw = false;

                for (int h = this._flex매입등록H.Rows.Fixed; h < this._flex매입등록H.Rows.Count; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h - 1), D.GetString(this._flex매입등록H.Rows.Count - 2) });

                    this._flex매입등록H.Row = h; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                    this._flex매입등록H[h, "S"] = "N";
                    this._flex매입등록H.SetCellCheck(h, this._flex매입등록H.Cols["S"].Index, CheckEnum.Unchecked);

                    for (int l = this._flex매입등록L.Rows.Fixed; l < this._flex매입등록L.Rows.Count; l++)
                    {
                        this._flex매입등록L[l, "S"] = "N";
                        this._flex매입등록L.SetCellCheck(l, this._flex매입등록L.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }

                this._flex매입등록H.Redraw = true;
                this._flex매입등록L.Redraw = true;

                MsgControl.CloseMsg();
                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { Global.MainFrame.DD("전체해제") });

                this._flex매입등록H_AfterRowChange(null, null);

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flex매입등록H.Redraw = true;
                this._flex매입등록L.Redraw = true;

                MsgControl.CloseMsg();
            }
        }

        private void cur원화부대비용계산(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                    this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                    this.cbo부대비용항목.SelectedValue.ToString() == "ADM007") return;

                this.cur원화부대비용.DecimalValue = this.원화계산(this.cur외화부대비용.DecimalValue * this.cur부대비용환율.DecimalValue);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cur외화부대비용계산(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo부대비용항목.SelectedValue.ToString() != "ADM005" &&
                    this.cbo부대비용항목.SelectedValue.ToString() != "ADM006" &&
                    this.cbo부대비용항목.SelectedValue.ToString() != "ADM007") return;

                this.cur외화부대비용.DecimalValue = 0;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string GetNMD_MNGD(DataRow dRow)
        {
            string str1 = "";
            for (int index = 1; index <= 8; ++index)
            {
                string str2 = D.GetString(dRow["NM_MNGD" + index.ToString()]);
                if ("A06" == D.GetString(dRow["CD_MNG" + index.ToString()]) && D.GetString(dRow["CD_MNGD" + index.ToString()]) != "00")
                    str2 = str2 + "(" + D.GetString(dRow["CD_MNGD" + index.ToString()]) + ")*";
                str1 = str2 != null && !(str2 == "") ? str1 + str2 + "*" : str1 ?? "";
            }
            if (str1 != string.Empty || str1 != "")
                str1 = str1.Substring(0, str1.Length - 1);
            return str1.Replace("()", "");
        }

        private string 지급예정일계산(string 처리일자, decimal 원화금액, int dtPay)
        {
            try
            {
                string 지급예정일 = string.Empty;
                int 월말일;
                DateTime 월말일자, dt처리일자, 익월;

                dt처리일자 = DateTime.ParseExact(처리일자, "yyyyMMdd", null);

                if (dtPay > 0)
                {
                    switch (dtPay)
                    {
                        case 930:
                            #region 익월 말일 결제 (30일)
                            익월 = dt처리일자.AddMonths(1);
                            지급예정일 = 익월.ToString("yyyyMM") + DateTime.DaysInMonth(익월.Year, 익월.Month).ToString();
                            #endregion
                            break;
                        case 960:
                            #region 익익월 말일 결제 (60일)
                            DateTime 익익월 = dt처리일자.AddMonths(2);
                            지급예정일 = 익익월.ToString("yyyyMM") + DateTime.DaysInMonth(익익월.Year, 익익월.Month).ToString();
                            #endregion
                            break;
                        case 907:
                            #region 세금계산서 발행일 + 7일
                            지급예정일 = dt처리일자.AddDays(7).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 914:
                            #region 세금계산서 발행일 + 14일
                            지급예정일 = dt처리일자.AddDays(14).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 931:
                            #region 세금계산서 발행일 + 30일
                            지급예정일 = dt처리일자.AddDays(30).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 996:
                            #region 세금계산서 발행일 + 59일
                            지급예정일 = dt처리일자.AddDays(59).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 961:
                            #region 세금계산서 발행일 + 60일
                            지급예정일 = dt처리일자.AddDays(60).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 997:
                            #region 익월말 하루전 결제
                            익월 = dt처리일자.AddMonths(1);

                            월말일 = DateTime.DaysInMonth(익월.Year, 익월.Month);
                            월말일자 = DateTime.Parse(익월.ToString("yyyy-MM") + "-" + 월말일.ToString());

                            지급예정일 = 월말일자.AddDays(-1).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 998:
                            #region 선지급
                            지급예정일 = 처리일자;
                            #endregion
                            break;
                        case 999:
                            #region 즉시결제
                            지급예정일 = 처리일자;
                            #endregion
                            break;
                        default:
                            #region 지급예정일이 정해져 있는 매입처 (익월 n일, 1달을 30일로 봄)
                            익월 = dt처리일자.AddMonths(dtPay / 30);

                            월말일 = DateTime.DaysInMonth(익월.Year, 익월.Month);
                            월말일자 = DateTime.Parse(익월.ToString("yyyy-MM") + "-" + 월말일.ToString());

                            지급예정일 = 월말일자.AddDays(dtPay % 30).ToString("yyyyMMdd");
                            #endregion
                            break;
                    }
                }
                //else if (원화금액 <= 1000000)
                //{
                //    #region 100만원 이하, 10일
                //    지급예정일 = dt처리일자.AddDays(10).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 1000000 && 원화금액 <= 3000000)
                //{
                //    #region 100만원 초과 300만원 이하, 익월 15일
                //    지급예정일 = dt처리일자.AddMonths(1).ToString("yyyyMM") + "15";
                //    #endregion
                //}
                //else if (원화금액 > 3000000 && 원화금액 <= 5000000)
                //{
                //    #region 300만원 초과 500만원 이하, 익월 말일
                //    DateTime 익월 = dt처리일자.AddMonths(1);
                //    지급예정일 = 익월.ToString("yyyyMM") + DateTime.DaysInMonth(익월.Year, 익월.Month).ToString();
                //    #endregion
                //}
                //else if (원화금액 > 5000000 && 원화금액 <= 10000000)
                //{
                //    #region 500만원 초과 1000만원 이하, 월 마감 후 45일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(45).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 10000000 && 원화금액 <= 30000000)
                //{
                //    #region 1000만원 초과 3000만원 이하, 월 마감 후 60일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(60).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 30000000)
                //{
                //    #region 3000만원 초과, 월 마감 후 75일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(75).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else
                //{
                //    지급예정일 = 처리일자;
                //}

                return 지급예정일;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return string.Empty;
        }

        private void 임시파일제거()
        {
            DirectoryInfo dirInfo;
            bool isExistFile;

            try
            {
                dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "temp"));
                isExistFile = false;

                if (dirInfo.Exists == true)
                {
                    foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            isExistFile = true;
                            continue;
                        }
                    }

                    if (isExistFile == false)
                        dirInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void SetHeaderAmt(string NO_IO, string FG_TAX)
        {
            string filterExpression = "NO_IO = '" + NO_IO + "' AND FG_TAX = '" + FG_TAX + "'";

            DataRow[] dataRowArray = this._flex매입등록H.DataTable.Select(filterExpression, "", DataViewRowState.CurrentRows);
            if (dataRowArray == null || dataRowArray.Length == 0) return;

            DataRow dataRow1 = dataRowArray[0];
            dataRow1["AM_PO_EX"] = this._flex매입등록L.DataTable.Compute("SUM(AM_EX)", filterExpression);
            dataRow1["AM_PO"] = this._flex매입등록L.DataTable.Compute("SUM(AM_IV)", filterExpression);
            dataRow1["VAT"] = this._flex매입등록L.DataTable.Compute("SUM(VAT_IV)", filterExpression);
            dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM_PO"]) + D.GetDecimal(dataRow1["VAT"]));
        }

        private void 선택금액계산()
        {
            try
            {
                this.cur외화합계.DecimalValue = D.GetDecimal(this._flex매입등록L.DataTable.Compute("SUM(AM_EX)", "S = 'Y'"));
                this.cur원화합계.DecimalValue = D.GetDecimal(this._flex매입등록L.DataTable.Compute("SUM(AM_IV)", "S = 'Y'"));
                this.cur부가세합계.DecimalValue = D.GetDecimal(this._flex매입등록L.DataTable.Compute("SUM(VAT_IV)", "S = 'Y'"));
                this.cur총합계.DecimalValue = D.GetDecimal(this._flex매입등록L.DataTable.Compute("SUM(AM_TOTAL)", "S = 'Y'"));
                this.cur총수량.DecimalValue = D.GetDecimal(this._flex매입등록L.DataTable.Compute("SUM(QT_IV)", "S = 'Y'"));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private decimal 원화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
                else
                    result = Decimal.Round(value, 0, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        private decimal 외화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!this._flex세금계산서H.HasNormalRow) return;

            if (((Control)sender).Name == this.cbo통화명.Name)
            {
                this.cur환율.Text = this._biz.환율(this._flex세금계산서H["DT_SEND"].ToString(), this.cbo통화명.SelectedValue.ToString()).ToString();
            }
        }
    }
}
