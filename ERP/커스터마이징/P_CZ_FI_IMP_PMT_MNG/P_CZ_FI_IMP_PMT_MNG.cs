using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.Utils;
using Parsing.Parser.UNIPASS;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_FI_IMP_PMT_MNG : PageBase
    {
        #region 전역변수 & 생성자
        P_CZ_FI_IMP_PMT_MNG_BIZ _biz;

        private string 환급상태
        {
            get
            {
                if (this.rdo환급.Checked == true) return "002";
                else if (this.rdo미환급.Checked == true) return "003";
                else return "001";
            }
        }

        public P_CZ_FI_IMP_PMT_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_CZ_FI_IMP_PMT_MNG_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo자동등록기간.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신고일자", "등록일자" });
            this.cbo자동등록기간.DisplayMember = "NAME";
            this.cbo자동등록기간.ValueMember = "CODE";

            this.cbo일자유형.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "신고일자", "환급일자" });
            this.cbo일자유형.DisplayMember = "NAME";
            this.cbo일자유형.ValueMember = "CODE";

            this.cbo완료여부.DataSource = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "완료", "미완료" }, true);
            this.cbo완료여부.DisplayMember = "NAME";
            this.cbo완료여부.ValueMember = "CODE";
            this.cbo완료여부.SelectedValue = "N";

            this.dtp조회일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp조회일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp조회일자.EndDate = Global.MainFrame.GetDateTimeToday();

            this.dtp자동등록기간.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp자동등록기간.EndDate = Global.MainFrame.GetDateTimeToday();

            if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
            {
                this.ctx은행.CodeValue = "08358";
                this.ctx은행.CodeName = "기업은행";

                this.ctx계좌.CodeValue = "20001";
                this.ctx계좌.CodeName = "보통예금(기업1)(092-073109-01-012)";
            }
            else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
            {
                this.ctx은행.CodeValue = "08358";
                this.ctx은행.CodeName = "중소기업은행";

                this.ctx계좌.CodeValue = "70001";
                this.ctx계좌.CodeName = "보통예금(기업)(092-090795-01-012)";
            }
        }

        private void InitGrid()
        {
            #region 자동등록

            this._flex자동등록H.DetailGrids = new FlexGrid[] { this._flex자동등록L };

            #region Header
            this._flex자동등록H.BeginSetting(1, 1, false);

            this._flex자동등록H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex자동등록H.SetCol("TP_DONE", "상태", 100);
            this._flex자동등록H.SetCol("NO_IMPORT", "수입신고번호", 100);
            this._flex자동등록H.SetCol("DT_IMPORT", "신고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex자동등록H.SetCol("DT_PAYMENT", "납부기한", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex자동등록H.SetCol("CD_OFFICE", "세관과", 100);
            this._flex자동등록H.SetCol("DT_ARRIVAL", "입항일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex자동등록H.SetCol("NO_BL", "BL번호", 100);
            this._flex자동등록H.SetCol("NO_BL_MASTER", "B/L번호(MASTER)", 100);
            this._flex자동등록H.SetCol("NO_CARGO", "화물관리번호", 100);
            this._flex자동등록H.SetCol("DECLARANT", "신고인", 100);
            this._flex자동등록H.SetCol("IMPORTER", "수입자", 100);
            this._flex자동등록H.SetCol("TAXPAYER", "납세의무자", 100);
            this._flex자동등록H.SetCol("FORWARDER", "운송주선인", 100);
            this._flex자동등록H.SetCol("TRADER", "무역거래처", 100);
            this._flex자동등록H.SetCol("GROSS_WEIGHT", "총중량", 100);
            this._flex자동등록H.SetCol("ARRIVAL_PORT", "국내도착항", 100);
            this._flex자동등록H.SetCol("EXPORT_COUNTRY", "적출국", 100);
            this._flex자동등록H.SetCol("ITEM_NAME", "품목명", 100);
            this._flex자동등록H.SetCol("CURRENCY", "통화명", 100);
            this._flex자동등록H.SetCol("EXCHANGE_RATE", "환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex자동등록H.SetCol("TAXABLE_VAT", "부가가치세과표", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_CUSTOMS", "관세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_CONSUMPTION", "개별소비세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_ENERGY", "교통에너지환경세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_LIQUOR", "주세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_EDUCATION", "교육세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_RURAL", "농어촌특별세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_VAT", "부가가치세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_LATE_REPORT", "신고지연가산세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록H.SetCol("TAX_NO_REPORT", "미신고가산세", 100, false, typeof(decimal), FormatTpType.MONEY);
			this._flex자동등록H.SetCol("YN_DONE", "완료여부", false);
			this._flex자동등록H.SetCol("ID_INSERT", "등록자", false);
			this._flex자동등록H.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex자동등록H.SetCol("DC_LOG", "로그", 100);
            this._flex자동등록H.SetCol("CD_PJT", "파일번호", 100);
            this._flex자동등록H.SetCol("NM_EMP", "담당자", 100);
            this._flex자동등록H.SetCol("FILE_PATH_MNG", "첨부파일", 100);

            this._flex자동등록H.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flex자동등록H.SettingVersion = "0.0.0.1";
            this._flex자동등록H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex자동등록H.SetDummyColumn(new string[] { "S" });
            #endregion

            #region Line
            this._flex자동등록L.BeginSetting(1, 1, false);

            this._flex자동등록L.SetCol("SEQ", "라인", 100);
            this._flex자동등록L.SetCol("NO_RAN", "란번호", 100);
            this._flex자동등록L.SetCol("NO_DSP", "순번", 100);
            this._flex자동등록L.SetCol("MODEL", "품명", 100);
            this._flex자동등록L.SetCol("QT", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex자동등록L.SetCol("UM", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex자동등록L.SetCol("AM", "금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록L.SetCol("HSCODE", "세번부호", 100);
            this._flex자동등록L.SetCol("NET_WEIGHT", "순중량", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex자동등록L.SetCol("TAXABLE_USD", "과세가격(USD)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex자동등록L.SetCol("TAXABLE_KRW", "과세가격(KRW)", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록L.SetCol("CUSTOMS", "관세액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록L.SetCol("VAT", "세액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex자동등록L.SetCol("ORIGIN", "원산지", 100);
            this._flex자동등록L.SetCol("NO_PO", "발주번호", false);
            this._flex자동등록L.SetCol("NO_LINE", "발주라인", false);

            this._flex자동등록L.SettingVersion = "0.0.0.1";
            this._flex자동등록L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 발주번호
            this._flex발주번호.BeginSetting(1, 1, false);

            this._flex발주번호.SetCol("NO_PO", "발주번호", 100);
            this._flex발주번호.SetCol("NO_LINE", "발주라인", 100);
            this._flex발주번호.SetCol("NO_DSP", "순번", 100);
            this._flex발주번호.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
            this._flex발주번호.SetCol("NM_ITEM_PARTNER", "품목명", 100);
            this._flex발주번호.SetCol("CD_UNIT_MM", "단위", 100);
            this._flex발주번호.SetCol("QT_PO", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex발주번호.SetCol("UM_EX", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex발주번호.SetCol("AM_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex발주번호.SetCol("UM", "원화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex발주번호.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex발주번호.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex발주번호.SetCol("YN_BL", "면장품목여부", 45, true, CheckTypeEnum.Y_N);

            this._flex발주번호.SettingVersion = "0.0.0.1";
            this._flex발주번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 수동등록
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IMPORT", "수입신고번호", 120);
            this._flexH.SetCol("NO_BL", "BL번호", 100);
            this._flexH.SetCol("DT_IO", "출고일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_IMPORT", "신고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_LIMIT", "납부기한", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_APPLICATION", "신청일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_RETURN", "환급일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_PAYMENT", "납부처", 150, true);
            this._flexH.SetCol("NM_SUPPLIER", "매입처", false);
            this._flexH.SetCol("AM_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_TAX", "납부금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_VAT_BASE", "과세표준(부가세)", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_VAT", "부가세", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_RETURN", "환급금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("NM_EMP", "등록자", 60);
            this._flexH.SetCol("FILE_PATH_MNG", "첨부파일", 100);
            this._flexH.SetCol("NO_PAYMENT_DOCU", "납부전표번호", 100);
            this._flexH.SetCol("NO_SO", "수주번호", 80);
            this._flexH.SetCol("NM_EMP_SALE", "영업담당자", 100);
            this._flexH.SetCol("NM_GIR", "영업물류담당자", 100);
            this._flexH.SetCol("DC_FREIGHT", "운임비고", 100, true);
            this._flexH.SetCol("DC_RMK", "비고", 100, true);

            this._flexH.ExtendLastCol = true;
            this._flexH.SetDummyColumn(new string[] { "S", "FILE_PATH_MNG" });
            this._flexH.SetCodeHelpCol("NM_PAYMENT", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "CD_PAYMENT", "NM_PAYMENT", ResultMode.FastMode);
            this._flexH.KeyActionEnter = KeyActionEnum.MoveDown;

            this._flexH.SettingVersion = "0.0.0.2";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetStringFormatCol2("NO_IMPORT", "AAAAA-AA-AAAAAAA");
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("NO_RAN", "란번호", 60, true);
            this._flexL.SetCol("NO_DSP", "순번", 60, false);
            this._flexL.SetCol("NO_POLINE", "항번", 60);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 200);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flexL.SetCol("UNIT_SO", "단위", 40);
            this._flexL.SetCol("QT_PO", "발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_TAX", "납부수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_RETURN", "환급수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_MAIN_CATEGORY", "의뢰구분(중)", 100);
            this._flexL.SetCol("NM_SUB_CATEGORY", "의뢰구분(소)", 100);
            this._flexL.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_TAX", "관세금액", 80, true, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_RETURN", "환급금액", 80, true, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("NO_SO", "수주번호", 100);
            this._flexL.SetCol("NM_SALEGRP", "영업그룹", 100);
            this._flexL.SetCol("NM_VESSEL", "호선명", 150);
            this._flexL.SetCol("NM_SUPPLIER", "매입처", 150);
            this._flexL.SetCol("NM_EMP", "영업담당자", 60);
            this._flexL.SetCol("NO_RETURN_DOCU", "환급전표번호", 100);

            this._flexL.SetDummyColumn(new string[] { "S" });
            this._flexL.KeyActionEnter = KeyActionEnum.MoveDown;

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
            #endregion
        }

        private void InitEvent()
        {
            this.btn관세납부등록.Click += new EventHandler(this.btn관세납부등록_Click);
            this.btn납부전표처리.Click += new EventHandler(this.btn납부전표처리_Click);
            this.btn납부전표취소.Click += new EventHandler(this.btn납부전표처리_Click);
            this.btn환급전표처리.Click += new EventHandler(this.btn환급전표처리_Click);
            this.btn환급전표취소.Click += new EventHandler(this.btn환급전표처리_Click);
            this.btn일괄환급.Click += new EventHandler(this.btn일괄환급_Click);
            this.btn일괄취소.Click += new EventHandler(this.btn일괄환급_Click);
			this.btn수입면장업로드.Click += Btn수입면장업로드_Click;
			this.btn납부영수증업로드.Click += Btn납부영수증업로드_Click;
			this.btn세금계산서업로드.Click += Btn세금계산서업로드_Click;
			this.btn수동등록.Click += Btn수동등록_Click;
            this.btn기타등록.Click += Btn수동등록_Click;
			this.btn임의처리.Click += Btn임의처리_Click;
			this.btn임의처리해제.Click += Btn임의처리해제_Click;
            this.ctx호선번호.QueryAfter += new BpQueryHandler(this.ctx호선번호_QueryAfter);
            this.ctx호선번호.CodeChanged += new EventHandler(this.ctx호선번호_CodeChanged);

			this._flex자동등록H.DoubleClick += _flex자동등록H_DoubleClick;
			this._flex자동등록H.AfterRowChange += _flex자동등록H_AfterRowChange;
			this._flex발주번호.AfterEdit += _flex발주번호_AfterEdit;

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.DoubleClick += new EventHandler(this._flexL_DoubleClick);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.StartEdit += new RowColEventHandler(this._flexL_StartEdit);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.AfterEdit += new RowColEventHandler(this._flexL_AfterEdit);
        }

		private void _flex발주번호_AfterEdit(object sender, RowColEventArgs e)
		{
            string query;

			try
            {
                if (this._flex발주번호.HasNormalRow == false) return;
                if (this._flex발주번호.Cols[e.Col].Name != "YN_BL") return;

                query = @"UPDATE PU_POL
                          SET YN_BL = '{3}'
                          WHERE CD_COMPANY = '{0}'
                          AND NO_PO = '{1}'
                          AND NO_LINE = '{2}'";

                DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
                                                            this._flex발주번호["NO_PO"].ToString(), 
                                                            this._flex발주번호["NO_LINE"].ToString(),
                                                            this._flex발주번호["YN_BL"].ToString()));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void Btn임의처리해제_Click(object sender, EventArgs e)
		{
            DataRow[] dataRowArray;
            string query;

            try
            {
                if (!this._flex자동등록H.HasNormalRow) return;

                dataRowArray = this._flex자동등록H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (this._flex자동등록H.DataTable.Select("S = 'Y' AND ISNULL(YN_DONE, '') <> 'C'").Length > 0)
                {
                    this.ShowMessage("임의처리 되지 않은 건이 선택되어 있습니다.");
                    return;
                }
                else
                {
                    query = @"UPDATE CZ_PU_IMPORT_DECLARATIONH
                              SET YN_DONE = NULL,
                                  ID_UPDATE = '{2}',
                                  DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                              WHERE CD_COMPANY = '{0}'
                              AND NO_IMPORT = '{1}'
                              AND ISNULL(YN_DONE, '') = 'C'";

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["NO_IMPORT"].ToString(),
                                                                                   Global.MainFrame.LoginInfo.UserID }));
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn임의처리해제.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void Btn임의처리_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string query;

            try
            {
                if (!this._flex자동등록H.HasNormalRow) return;

                dataRowArray = this._flex자동등록H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
				}
                else if (this._flex자동등록H.DataTable.Select("S = 'Y' AND ISNULL(YN_DONE, '') <> ''").Length > 0)
				{
                    this.ShowMessage("완료 된 건이 선택되어 있습니다.");
                    return;
				}
                else
				{
                    query = @"UPDATE CZ_PU_IMPORT_DECLARATIONH
                              SET YN_DONE = 'C',
                                  ID_UPDATE = '{2}',
                                  DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
                              WHERE CD_COMPANY = '{0}'
                              AND NO_IMPORT = '{1}'
                              AND ISNULL(YN_DONE, '') = ''";

                    foreach (DataRow dr in dataRowArray)
					{
                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["NO_IMPORT"].ToString(),
                                                                                   Global.MainFrame.LoginInfo.UserID }));
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn임의처리.Text);
				}
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn세금계산서업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage("세금계산서 업로드 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                foreach (FileInfo file in new DirectoryInfo("C:\\UNIPASS_IMP_BILL").GetFiles())
                {
                    if (file.Extension.ToLower() != ".pdf")
                        continue;

                    //BILL_신고번호.pdf
                    string 신고번호 = file.Name.Replace(file.Extension, string.Empty).Split('_')[1];

                    try
                    {
                        string query = @"SELECT 1
                                         FROM CZ_PU_IMPORT_DECLARATIONH DH WITH(NOLOCK)
                                         WHERE DH.CD_COMPANY = '{0}'
                                         AND DH.NO_IMPORT = '{1}'";

                        DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 신고번호));

                        if (dt == null || dt.Rows.Count == 0)
                        {
                            Messenger.SendMSG(new string[] { "S-391" }, string.Format("세금계산서 업로드 오류 : 신고번호에 해당 하는 건이 없습니다. {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name));
                            continue;
                        }

                        string fileCode = 신고번호.Replace("-", string.Empty) + "_" + Global.MainFrame.LoginInfo.CompanyCode;

                        query = @"SELECT 1
                                  FROM MA_FILEINFO WITH(NOLOCK)
                                  WHERE CD_COMPANY = '{0}'
                                  AND CD_MODULE = 'FI'
                                  AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
                                  AND CD_FILE = '{1}'
                                  AND FILE_NAME = '{2}'";

                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, fileCode, file.Name));

                        if (dt != null && dt.Rows.Count > 0)
                            continue;

                        string 업로드위치 = "Upload/P_CZ_FI_IMP_PMT_MNG";
                        FileUploader.UploadFile(file.Name, file.FullName, 업로드위치, fileCode);
                        this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, fileCode, file, 업로드위치, "P_CZ_FI_IMP_PMT_MNG");
                    }
                    catch (Exception ex)
                    {
                        Messenger.SendMSG(new string[] { "S-391" }, string.Format("세금계산서 업로드 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name + "_" + ex.Message));
                        continue;
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn세금계산서업로드.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn수동등록_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string 수입면장번호, noPayment, name, option;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.btn기타등록.Name)
                    option = "E";
                else
                    option = "N";

                if (Global.MainFrame.ShowMessage(string.Format("선택한 건 {0} 등록 하시겠습니까?", (option == "E" ? "기타" : "수동")), "QY2") != DialogResult.Yes)
                    return;

                dataRowArray = this._flex자동등록H.DataTable.Select("S = 'Y'", string.Empty);

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
				{
                    foreach(DataRow dr in dataRowArray)
					{
                        수입면장번호 = dr["NO_IMPORT"].ToString();

                        DBHelper.ExecuteNonQuery("PW_CZ_PU_IMPORT_DECLARATION", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 수입면장번호, option });

                        if (string.IsNullOrEmpty(dr["DT_PAYMENT"].ToString())) // 샤인 대납건 전표처리 하면 안됨 (대납 건은 납부기한과 납부영수증 없음)
                            continue;

                        try
						{
                            noPayment = this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "08", Global.MainFrame.GetStringToday.Substring(0, 6)).ToString();

                            Global.MainFrame.ExecuteScalar(string.Format(@"UPDATE CZ_FI_IMP_PMTH
                                                                           SET NO_PAYMENT = '{2}'
                                                                           WHERE CD_COMPANY = '{0}'
                                                                           AND NO_IMPORT = '{1}'", Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                   수입면장번호.Replace("-", string.Empty),
                                                                                                   noPayment));

                            string 은행, 계좌;

                            if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                            {
                                은행 = "08358"; // 기업은행
                                계좌 = "20001"; // 092-073109-01-012
                            }
                            else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                            {
                                은행 = "08358"; // 기업은행
                                계좌 = "70001"; // 092-090795-01-012
                            }
                            else
                                continue;

                            if (!this._biz.납부전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      noPayment,
                                                                      은행,
                                                                      계좌,
                                                                      "D03",
                                                                      Global.MainFrame.LoginInfo.UserID }))
                            {
                                this.ShowMessage("납부번호(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", new string[] { 수입면장번호 }, "IK1");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ShowMessage(string.Format("전표처리 오류, 해결 후 수동등록 탭에서 전표 처리 하시기 바랍니다. (수입신고번호 : {0})", 수입면장번호) + Environment.NewLine + ex.Message);
                            return;
						}
					}
				}

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn수동등록.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn납부영수증업로드_Click(object sender, EventArgs e)
        {
            DataTable dt;
            string 납부기한, query;

            try
            {
                if (Global.MainFrame.ShowMessage("전자납부영수증 업로드 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                foreach (FileInfo file in new DirectoryInfo("C:\\UNIPASS_IMP_PAY").GetFiles())
                {
                    if (file.Extension.ToLower() != ".pdf")
                        continue;

                    string 신고번호 = file.Name.Replace(file.Extension, string.Empty);

                    try
                    {
                        query = @"SELECT DH.DT_PAYMENT 
                                  FROM CZ_PU_IMPORT_DECLARATIONH DH WITH(NOLOCK)
                                  WHERE DH.CD_COMPANY = '{0}'
                                  AND DH.NO_IMPORT = '{1}'";

                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 신고번호));

                        if (dt == null || dt.Rows.Count == 0)
						{
                            Messenger.SendMSG(new string[] { "S-391" }, string.Format("전자납부영수증 업로드 오류 : 신고번호에 해당 하는 건이 없습니다. {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name));
                            continue;
                        }

                        납부기한 = dt.Rows[0]["DT_PAYMENT"].ToString();

                        string fileCode = 신고번호.Replace("-", string.Empty) + "_" + Global.MainFrame.LoginInfo.CompanyCode;

                        query = @"SELECT 1
                                  FROM MA_FILEINFO WITH(NOLOCK)
                                  WHERE CD_COMPANY = '{0}'
                                  AND CD_MODULE = 'FI'
                                  AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
                                  AND CD_FILE = '{1}'
                                  AND FILE_NAME = '{2}'";

                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, fileCode, file.Name));

                        if (dt != null && dt.Rows.Count > 0)
                            continue;

                        string 업로드위치 = "Upload/P_CZ_FI_IMP_PMT_MNG";
                        FileUploader.UploadFile(file.Name, file.FullName, 업로드위치, fileCode);
                        this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, fileCode, file, 업로드위치, "P_CZ_FI_IMP_PMT_MNG");
                    }
                    catch (Exception ex)
                    {
                        Messenger.SendMSG(new string[] { "S-391" }, string.Format("전자납부영수증 업로드 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name + "_" + ex.Message));
                        continue;
                    }

                    if (string.IsNullOrEmpty(납부기한)) // 샤인 대납건 전표처리 하면 안됨 (대납 건은 납부기한과 납부영수증 없음)
                        continue;

                    query = @"UPDATE CZ_FI_IMP_PMTH
                              SET DT_LIMIT = '{2}'
                              WHERE CD_COMPANY = '{0}'
                              AND NO_IMPORT = '{1}'";

                    Global.MainFrame.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                        신고번호.Replace("-", string.Empty),
                                                                        납부기한)); // 납부기한 업데이트

                    query = @"SELECT NO_IMPORT 
                              FROM CZ_FI_IMP_PMTH WITH(NOLOCK)
                              WHERE CD_COMPANY = '{0}'
                              AND NO_IMPORT = '{1}'";

                    dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                    신고번호.Replace("-", string.Empty)));

                    if (dt == null || dt.Rows.Count == 0) // 수입면장등록 되지 않은 건 자동전표 처리 안함
                        continue;

                    try
                    {
                        string noPayment = this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "08", Global.MainFrame.GetStringToday.Substring(0, 6)).ToString();

                        Global.MainFrame.ExecuteScalar(string.Format(@"UPDATE CZ_FI_IMP_PMTH
                                                                       SET NO_PAYMENT = '{2}'
                                                                       WHERE CD_COMPANY = '{0}'
                                                                       AND NO_IMPORT = '{1}'", Global.MainFrame.LoginInfo.CompanyCode,
                                                                                               신고번호.Replace("-", string.Empty),
                                                                                               noPayment));

                        string 은행, 계좌;

                        if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                        {
                            은행 = "08358"; // 기업은행
                            계좌 = "20001"; // 092-073109-01-012
                        }
                        else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                        {
                            은행 = "08358"; // 기업은행
                            계좌 = "70001"; // 092-090795-01-012
                        }
                        else
                            continue;

                        if (!this._biz.납부전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  noPayment,
                                                                  은행,
                                                                  계좌,
                                                                  "D03",
                                                                  Global.MainFrame.LoginInfo.UserID }))
                        {
                            Messenger.SendMSG(new string[] { "S-391" }, string.Format("납부번호({0}) 의 전표처리가 정상적으로 처리되지 않았습니다.", 신고번호));
                        }
                    }
                    catch (Exception ex)
                    {
                        Messenger.SendMSG(new string[] { "S-391" }, string.Format("납부 전표 처리 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name + "_" + ex.Message));
                        continue;
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn납부영수증업로드.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex자동등록H_DoubleClick(object sender, EventArgs e)
        {
            string fileCode, query, result;

            try
            {
                if (this._flex자동등록H.HasNormalRow == false) return;
                if (this._flex자동등록H.MouseRow < this._flex자동등록H.Rows.Fixed) return;

                if (this._flex자동등록H.Cols[this._flex자동등록H.Col].Name == "FILE_PATH_MNG")
                {
                    fileCode = D.GetString(this._flex자동등록H["NO_IMPORT"]).Replace("-", "") + "_" + Global.MainFrame.LoginInfo.CompanyCode;
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "FI", "P_CZ_FI_IMP_PMT_MNG", fileCode, "P_CZ_FI_IMP_PMT_MNG");
                    dlg.ShowDialog(this);

                    query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                              FROM MA_FILEINFO WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            @"AND CD_MODULE = 'FI'
                              AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
                              AND CD_FILE = '" + fileCode + "'";

                    result = D.GetString(Global.MainFrame.ExecuteScalar(query));

                    foreach (DataRow dr in this._flex자동등록H.DataTable.Select("NO_IMPORT = '" + D.GetString(this._flex자동등록H["NO_IMPORT"]) + "'"))
                    {
                        dr["FILE_PATH_MNG"] = result;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex자동등록H_AfterRowChange(object sender, RangeEventArgs e)
		{
            DataTable dt, dtP;
            string key, filter;

            try
            {
                if (this._flex자동등록H.HasNormalRow == false) return;

                key = D.GetString(this._flex자동등록H["NO_IMPORT"]);
                filter = "NO_IMPORT = '" + key + "'";
                dt = null;
                dtP = null;

                if (this._flex자동등록H.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetailA(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                key });

                    dtP = this._biz.SearchDetailP(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 key });
                }

                this._flex자동등록L.BindingAdd(dt, filter);
                this._flex자동등록L.AutoSizeRows();

                this._flex발주번호.BindingAdd(dtP, filter);
                this._flex발주번호.AutoSizeRows();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
		#endregion

		#region 메인버튼 이벤트
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.tabControl1.SelectedTab == this.tpg자동등록)
				{
                    this._flex자동등록H.Binding = this._biz.SearchA(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   this.cbo자동등록기간.SelectedValue.ToString(),
                                                                                   this.dtp자동등록기간.StartDateToString,
                                                                                   this.dtp자동등록기간.EndDateToString,
                                                                                   this.txt신고번호S.Text,
                                                                                   this.txtBL번호S.Text,
                                                                                   this.cbo완료여부.SelectedValue.ToString(),
                                                                                   (this.chk등록건제외.Checked == true ? "Y" : "N") });

                    if (!this._flex자동등록H.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
				{
                    this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                          this.cbo일자유형.SelectedValue,
                                                                          this.dtp조회일자.StartDateToString,
                                                                          this.dtp조회일자.EndDateToString,
                                                                          this.txt신고번호.Text,
                                                                          this.txt수주번호.Text,
                                                                          this.txtBL번호.Text,
                                                                          this.ctx영업담당자.CodeValue,
                                                                          this.ctx호선번호.CodeValue,
                                                                          this.ctx매입처.CodeValue,
                                                                          this.환급상태 });

                    if (!this._flexH.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }   
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                P_CZ_FI_IMP_PMT_REG dialog = new P_CZ_FI_IMP_PMT_REG();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.OnToolBarSearchButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (this._flexH.DataTable.Select("S = 'Y' AND ISNULL(NO_PAYMENT_DOCU, '') <> ''").Length > 0)
            {
                Global.MainFrame.ShowMessage("납부전표가 처리 된 건이 선택되어 있습니다.");
                return false;
            }

            if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(NO_RETURN_DOCU, '') <> ''").Length > 0)
            {
                Global.MainFrame.ShowMessage("환급전표가 처리 된 건이 선택되어 있습니다.");
                return false;
            }

            if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(QT_RETURN, 0) > 0").Length > 0)
            {
                Global.MainFrame.ShowMessage("환급수량이 있는 건이 선택되어 있습니다.");
                return false;
            }

            return base.BeforeDelete();
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            DataRow[] dataRowArray, dataRowArray1;
            string filter;

            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!BeforeDelete() || !this._flexH.HasNormalRow) return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                foreach (DataRow dr in dataRowArray)
                {
                    filter = "NO_IMPORT = '" + D.GetString(dr["NO_IMPORT"]) + "'";
                    dataRowArray1 = this._flexL.DataTable.Select("S = 'Y' AND " + filter);

                    foreach(DataRow dr1 in dataRowArray1)
                    {
                        dr1.Delete();
                    }

                    dataRowArray1 = this._flexL.DataTable.Select(filter);
                    if (dataRowArray1.Length == 0)
                        dr.Delete();
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

                if (!this.BeforeSave()) return;

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
            try
            {
                if (!base.SaveData() || !base.Verify()) return false;
                if (this._flexH.IsDataChanged == false && this._flexL.IsDataChanged == false) return false;

                DataTable dtH = this._flexH.GetChanges();
                DataTable dtL = this._flexL.GetChanges();

                if (!this._biz.SaveData(dtH, dtL))
                    return false;

                this._flexH.AcceptChanges();
                this._flexL.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dtL, dtPO;
            string key, filter;

            try
            {
                if (this._flexH.HasNormalRow == false) return;

                key = D.GetString(this._flexH["NO_IMPORT"]);

                filter = "NO_IMPORT = '" + key + "'";
                dtL = null;

                if (this._flexH.DetailQueryNeed == true)
                {
                    dtL = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                Global.MainFrame.LoginInfo.Language,
                                                                key,
                                                                this.txt수주번호.Text,
                                                                this.ctx영업담당자.CodeValue,
                                                                this.ctx호선번호.CodeValue,
                                                                this.ctx매입처.CodeValue });
                }

                this._flexL.BindingAdd(dtL, filter);
                this._flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_DoubleClick(object sender, EventArgs e)
        {
            string fileCode, query, result;

            try
            {
                if (this._flexH.HasNormalRow == false) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;

                if (this._flexH.Cols[this._flexH.Col].Name == "NO_PAYMENT_DOCU" && !string.IsNullOrEmpty(D.GetString(this._flexH["NO_PAYMENT_DOCU"])))
                {
                    this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexH["NO_PAYMENT_DOCU"]), 
                                                                                                                                     "1",
                                                                                                                                     Global.MainFrame.LoginInfo.CdPc,
                                                                                                                                     Global.MainFrame.LoginInfo.CompanyCode });
                }
                else if (this._flexH.Cols[this._flexH.Col].Name == "FILE_PATH_MNG")
                {
                    fileCode = D.GetString(this._flexH["NO_IMPORT"]) + "_" + Global.MainFrame.LoginInfo.CompanyCode;
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "FI", "P_CZ_FI_IMP_PMT_MNG", fileCode, "P_CZ_FI_IMP_PMT_MNG");
                    dlg.ShowDialog(this);

                    query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                              FROM MA_FILEINFO WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            @"AND CD_MODULE = 'FI'
                              AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
                              AND CD_FILE = '" + fileCode + "'";

                    result = D.GetString(Global.MainFrame.ExecuteScalar(query));

                    foreach(DataRow dr in this._flexH.DataTable.Select("NO_IMPORT = '" + D.GetString(this._flexH["NO_IMPORT"]) + "'"))
                    {
                        dr["FILE_PATH_MNG"] = result;    
                    }   
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexH["S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        if (!this._flexH.HasNormalRow) return;

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                        for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                        {
                            this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                            {
                                if (this._flexL.RowState(i) == DataRowState.Deleted) continue;

                                this._flexL[i, "S"] = D.GetString(this._flexH["S"]);
                            }
                        }
                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        if (!this._flexL.HasNormalRow) return;

                        this._flexH["S"] = D.GetString(this._flexL["S"]);

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexL_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flexL.HasNormalRow == false) return;
                if (this._flexL.MouseRow < this._flexL.Rows.Fixed) return;

                if (this._flexL.Cols[this._flexL.Col].Name == "NO_RETURN_DOCU" && !string.IsNullOrEmpty(D.GetString(this._flexL["NO_RETURN_DOCU"])))
                {
                    this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexL["NO_RETURN_DOCU"]), 
                                                                                                                                     "1",
                                                                                                                                     Global.MainFrame.LoginInfo.CdPc,
                                                                                                                                     Global.MainFrame.LoginInfo.CompanyCode });
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;
            string name;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                name = grid.Cols[e.Col].Name;

                if (name == "S")
                {
                    grid["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                    if (grid.Name == this._flexL.Name)
                    {
                        DataRow[] drArr = grid.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                        if (drArr.Length != 0)
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Checked);
                        else
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
                else if (name == "QT_RETURN")
                {
                    if(D.GetDecimal(this._flexL["QT_TAX"]) < D.GetDecimal(this._flexL["QT_RETURN"]))
                    {
                        this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { this.DD("환급수량"), this.DD("납부수량") });
                        e.Cancel = true;
                        return;
                    }

                    this._flexL["AM_RETURN"] = (D.GetDecimal(this._flexL["AM_TAX"]) * (D.GetDecimal(this._flexL["QT_RETURN"]) / D.GetDecimal(this._flexL["QT_TAX"])));
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexL_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (this._flexL.Cols[e.Col].Name == "S" && D.GetString(this._flexH["S"]) == "Y")
                {
                    if (D.GetString(this._flexL["S"]) == "Y") //edit 시작점이므로 n으로 변경하려면 기존값은 y
                        this._flexH["S"] = "N";
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexL_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid grid;
            string name, filter;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                name = grid.Cols[e.Col].Name;
                filter = "NO_IMPORT = '" + D.GetString(this._flexH["NO_IMPORT"]) + "'";

                if (name == "AM_TAX")
                    this._flexH["AM_TAX"] = this._flexL.DataTable.Compute("SUM(AM_TAX)", filter);
                else if (name == "AM_VAT_BASE")
                    this._flexH["AM_VAT_BASE"] = this._flexL.DataTable.Compute("SUM(AM_VAT_BASE)", filter);
                else if (name == "AM_VAT")
                    this._flexH["AM_VAT"] = this._flexL.DataTable.Compute("SUM(AM_VAT)", filter);
                else if (name == "QT_RETURN")
                {
                    this._flexH["AM_RETURN"] = this._flexL.DataTable.Compute("SUM(AM_RETURN)", filter);

                    if (D.GetDecimal(this._flexH["AM_RETURN"]) > 0)
                        this._flexH["DT_RETURN"] = Global.MainFrame.GetStringToday;
                    else
                        this._flexH["DT_RETURN"] = string.Empty;
                }    
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 버튼 이벤트
        private void btn관세납부등록_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_FI_IMP_PMT_REG dialog = new P_CZ_FI_IMP_PMT_REG();
                dialog.Show();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn납부전표처리_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string name, seq;

            try
            {
                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    this.전표버튼활성화(false);

                    name = ((Control)sender).Name;

                    #region 확인
                    if (name == this.btn납부전표처리.Name)
                    {
                        if (this._flexH.DataTable.Select("S = 'Y' AND ISNULL(NO_PAYMENT_DOCU, '') <> ''").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("납부전표가 처리 된 건이 선택되어 있습니다.");
                            return;
                        }

                        if (string.IsNullOrEmpty(this.ctx은행.CodeValue))
                        {
                            Global.MainFrame.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl은행.Text);
                            return;
                        }

                        if (string.IsNullOrEmpty(this.ctx계좌.CodeValue))
                        {
                            Global.MainFrame.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl계좌.Text);
                            return;
                        }

                        if (this._flexH.DataTable.Select("S = 'Y' AND ISNULL(AM_TAX, 0) = 0 AND ISNULL(AM_VAT, 0) = 0").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("납부금액과 부가세가 0 인 건이 선택되어 있습니다.");
                            return;
                        }

                        if (ComFunc.getGridGroupBy(dataRowArray, new string[] { "CD_PAYMENT" }, true).Rows.Count != 1)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._의값이중복되었습니다, "납부처");
                            return;
                        }
                    }
                    else if (name == this.btn납부전표취소.Name)
                    {
                        if (this._flexH.DataTable.Select("S = 'Y' AND ISNULL(NO_PAYMENT_DOCU, '') = ''").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("납부전표를 처리하지 않은 건이 선택되어 있습니다.");
                            return;
                        }
                    }
                    #endregion

                    #region 번호부여
                    if (name == this.btn납부전표처리.Name)
                    {
                        if (this.rdo일괄.Checked)
                        {
                            seq = this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "08", Global.MainFrame.GetStringToday.Substring(0, 6)).ToString();

                            foreach (DataRow dr in dataRowArray)
                            {
                                dr["NO_PAYMENT"] = seq;
                                Global.MainFrame.ExecuteScalar(@"UPDATE CZ_FI_IMP_PMTH
                                                                 SET NO_PAYMENT = '" + seq + "'" + Environment.NewLine +
                                                                "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                "AND NO_IMPORT = '" + D.GetString(dr["NO_IMPORT"]) + "'");
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in dataRowArray)
                            {
                                seq = this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "08", Global.MainFrame.GetStringToday.Substring(0, 6)).ToString();

                                dr["NO_PAYMENT"] = seq;
                                Global.MainFrame.ExecuteScalar(@"UPDATE CZ_FI_IMP_PMTH
                                                                 SET NO_PAYMENT = '" + seq + "'" + Environment.NewLine +
                                                                "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                "AND NO_IMPORT = '" + D.GetString(dr["NO_IMPORT"]) + "'");
                            }
                        }
                    }
                    #endregion

                    #region 전표처리
                    foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_PAYMENT" }, true).Rows)
                    {
                        if (name == this.btn납부전표처리.Name)
                        {
                            if (!this._biz.납부전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       D.GetString(dr["NO_PAYMENT"]),
                                                                       this.ctx은행.CodeValue,
                                                                       this.ctx계좌.CodeValue,
                                                                       "D03",
                                                                       Global.MainFrame.LoginInfo.UserID }))
                            {
                                this.ShowMessage("@(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", new string[] { this.DD("납부번호"), D.GetString(dr["NO_IMPORT"]) }, "IK1");
                                return;
                            }
                        }
                        else
                        {
                            if (!this._biz.전표취소(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   "D03",
                                                                   D.GetString(dr["NO_PAYMENT"]),
                                                                   Global.MainFrame.LoginInfo.UserID }))
                            {
                                this.ShowMessage("@(@) 의 전표처리가 정상적으로 취소되지 않았습니다.", new string[] { this.DD("납부번호"), D.GetString(dr["NO_IMPORT"]) }, "IK1");
                                return;
                            }
                        }
                    }
                    #endregion

                    #region 번호제거
                    if (name == this.btn납부전표취소.Name)
                    {
                        foreach (DataRow dr in dataRowArray)
                        {
                            dr["NO_PAYMENT"] = string.Empty;
                            Global.MainFrame.ExecuteScalar(@"UPDATE CZ_FI_IMP_PMTH
                                                             SET NO_PAYMENT = NULL
                                                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                            "AND NO_IMPORT = '" + D.GetString(dr["NO_IMPORT"]) + "'");
                        }
                    }
                    #endregion

                    if (name == this.btn납부전표처리.Name)
                        this.ShowMessage("전표가 처리 되었습니다");
                    else
                        this.ShowMessage("전표가 취소 되었습니다");

                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this.전표버튼활성화(true);
            }
        }

        private void btn환급전표처리_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string name, seq;

            try
            {
                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    this.전표버튼활성화(false);

                    name = ((Control)sender).Name;

                    #region 확인
                    if (this._flexH.DataTable.Select("S = 'Y' AND ISNULL(DT_RETURN, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage("환급일자가 입력되지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    if (name == this.btn환급전표처리.Name)
                    {
                        if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(NO_RETURN_DOCU, '') <> ''").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("환급전표가 처리 된 건이 선택되어 있습니다.");
                            return;
                        }

                        if (string.IsNullOrEmpty(this.ctx은행.CodeValue))
                        {
                            Global.MainFrame.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl은행.Text);
                            return;
                        }

                        if (string.IsNullOrEmpty(this.ctx계좌.CodeValue))
                        {
                            Global.MainFrame.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl계좌.Text);
                            return;
                        }

                        if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(AM_RETURN, 0) = 0").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("환급금액이 0 인 건이 선택되어 있습니다.");
                            return;
                        }
                    }
                    else
                    {
                        if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(NO_RETURN_DOCU, '') = ''").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("환급전표를 처리하지 않은 건이 선택되어 있습니다.");
                            return;
                        }
                    }
                    #endregion

                    #region 번호부여
                    if (name == this.btn환급전표처리.Name)
                    {
                        seq = this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "08", Global.MainFrame.GetStringToday.Substring(0, 6)).ToString();

                        foreach (DataRow dr in dataRowArray)
                        {
                            dr["NO_RETURN"] = seq;
                            Global.MainFrame.ExecuteScalar(@"UPDATE CZ_FI_IMP_PMTL
                                                             SET NO_RETURN = '" + seq + "'" + Environment.NewLine +
                                                            "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                            "AND NO_IMPORT = '" + D.GetString(dr["NO_IMPORT"]) + "'" + Environment.NewLine +
                                                            "AND NO_PO = '" + D.GetString(dr["NO_PO"]) + "'" + Environment.NewLine +
                                                            "AND NO_POLINE = '" + D.GetString(dr["NO_POLINE"]) + "'");
                        }
                    }
                    #endregion

                    #region 전표처리
                    if (name == this.btn환급전표처리.Name || name == this.btn환급전표취소.Name)
                    {
                        foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_RETURN" }, true).Rows)
                        {
                            if (name == this.btn환급전표처리.Name)
                            {
                                if (!this._biz.환급전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           D.GetString(dr["NO_RETURN"]),
                                                                           this.ctx은행.CodeValue,
                                                                           this.ctx계좌.CodeValue,
                                                                           "D04",
                                                                           Global.MainFrame.LoginInfo.UserID }))
                                {
                                    this.ShowMessage("@(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", new string[] { this.DD("납부번호"), D.GetString(dr["NO_IMPORT"]) }, "IK1");
                                    return;
                                }
                            }
                            else
                            {
                                if (!this._biz.전표취소(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       "D04",
                                                                       D.GetString(dr["NO_RETURN"]),
                                                                       Global.MainFrame.LoginInfo.UserID }))
                                {
                                    this.ShowMessage("@(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", new string[] { this.DD("납부번호"), D.GetString(dr["NO_IMPORT"]) }, "IK1");
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 전표취소
                    if (name == this.btn환급전표취소.Name)
                    {
                        foreach (DataRow dr in dataRowArray)
                        {
                            Global.MainFrame.ExecuteScalar(@"UPDATE CZ_FI_IMP_PMTL
                                                             SET NO_RETURN = NULL
                                                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                            "AND NO_RETURN = '" + D.GetString(dr["NO_RETURN"]) + "'");
                            dr["NO_RETURN"] = string.Empty;
                        }
                    }
                    #endregion

                    if (name == this.btn환급전표처리.Name)
                        this.ShowMessage("전표가 처리 되었습니다");
                    else
                        this.ShowMessage("전표가 취소 되었습니다");

                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this.전표버튼활성화(true);
            }
        }

        private void btn일괄환급_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray, dataRowArray1;
            string name, filter;

            try
            {
                if (this._flexH.HasNormalRow == false) return;

                name = ((Control)sender).Name;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach(DataRow dr in dataRowArray)
                    {
                        filter = "NO_IMPORT = '" + D.GetString(dr["NO_IMPORT"]) + "'";
                        dataRowArray1 = this._flexL.DataTable.Select("S = 'Y' AND " + filter);

                        foreach (DataRow dr1 in dataRowArray1)
                        {
                            if (name == this.btn일괄환급.Name)
							{
                                dr1["QT_RETURN"] = dr1["QT_TAX"];
                                dr1["AM_RETURN"] = D.GetDecimal(dr1["AM_TAX"]);
                            }
                            else
							{
                                dr1["QT_RETURN"] = 0;
                                dr1["AM_RETURN"] = 0;
                            }
                        }

                        dr["AM_RETURN"] = this._flexL.DataTable.Compute("SUM(AM_RETURN)", filter);

                        if (D.GetDecimal(dr["AM_RETURN"]) > 0)
                            dr["DT_RETURN"] = Global.MainFrame.GetStringToday;
                        else
                            dr["DT_RETURN"] = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn수입면장업로드_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (Global.MainFrame.ShowMessage("수입면장 업로드 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                query = @"SELECT DH.NO_IMPORT 
                          FROM CZ_PU_IMPORT_DECLARATIONH DH WITH(NOLOCK)
                          WHERE DH.CD_COMPANY = '{0}'
                          AND REPLACE(DH.NO_IMPORT, '-', '') = '{1}'";

                foreach (FileInfo file in new DirectoryInfo("C:\\UNIPASS_IMP").GetFiles())
				{
                    string 신고번호 = file.Name.Split('_')[1];

                    DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 신고번호));

                    if (dt != null && dt.Rows.Count > 0)
                        continue;

                    UNIPASS_2 parsing = new UNIPASS_2(file.FullName);

                    try
                    {
                        parsing.Parse();
                    }
                    catch (Exception ex)
                    {
                        Messenger.SendMSG(new string[] { "S-458" }, string.Format("수입면장 파싱 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name + "_" + ex.Message));
                        continue;
                    }

                    if (신고번호 != parsing.ItemH.Rows[0]["NO_IMPORT"].ToString().Replace("-", ""))
					{
                        Messenger.SendMSG(new string[] { "S-391" }, string.Format("파일명 신고번호 상이 : 파일이름 : {0}, 신고번호 : {1}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name, parsing.ItemH.Rows[0]["NO_IMPORT"].ToString()));
                        continue;
					}

                    try
                    {
                        string fileCode = 신고번호.Replace("-", string.Empty) + "_" + Global.MainFrame.LoginInfo.CompanyCode;

                        string 업로드위치 = "Upload/P_CZ_FI_IMP_PMT_MNG";
                        FileUploader.UploadFile(file.Name, file.FullName, 업로드위치, fileCode);
                        this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, fileCode, file, 업로드위치, "P_CZ_FI_IMP_PMT_MNG");
                    }
                    catch (Exception ex)
                    {
                        Messenger.SendMSG(new string[] { "S-391" }, string.Format("수입면장 업로드 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name + "_" + ex.Message));
                        continue;
                    }

                    try
					{
                        DBHelper.ExecuteNonQuery("PW_CZ_PU_IMPORT_DECLARATION", new object[] { Global.MainFrame.LoginInfo.CompanyCode, parsing.ItemH.Rows[0]["NO_IMPORT"].ToString(), "Y" });
                    }
                    catch (Exception ex)
                    {
						Messenger.SendMSG(new string[] { "S-343" }, string.Format("수입면장 등록 오류 : {0}", Global.MainFrame.LoginInfo.CompanyCode + "_" + file.Name + "_" + ex.Message));
						continue;
                    }
				}

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn수입면장업로드.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx호선번호_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                this.txt호선명.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx호선번호_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ctx호선번호.Text))
                {
                    this.txt호선명.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타메소드
        private void 전표버튼활성화(bool 활성화여부)
        {
            if (활성화여부 == true)
            {
                this.btn납부전표처리.Enabled = true;
                this.btn납부전표취소.Enabled = true;
                this.btn환급전표처리.Enabled = true;
                this.btn환급전표취소.Enabled = true;
            }
            else
            {
                this.btn납부전표처리.Enabled = false;
                this.btn납부전표취소.Enabled = false;
                this.btn환급전표처리.Enabled = false;
                this.btn환급전표취소.Enabled = false;
            }
        }
        #endregion
    }
}
