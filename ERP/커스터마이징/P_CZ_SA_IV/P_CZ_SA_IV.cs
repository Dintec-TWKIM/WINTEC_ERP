using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.ERPU.SA;
using System.Drawing.Printing;
using Duzon.Windows.Print;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Duzon.ERPU.Utils;
using System.Diagnostics;
using DX;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace cz
{
    public partial class P_CZ_SA_IV : PageBase
    {
        #region 전역변수 & 생성자
        P_CZ_SA_IV_BIZ _biz = new P_CZ_SA_IV_BIZ();
        DataTable _dtL;
        private string _수금예정일전용코드;
        private string _여신한도전용코드;
        private string _부서;
        private bool _하단그리드필터여부;
        private CommonFunction _commFun = new CommonFunction();

        #region 자동매출등록
        private string 거래처코드;
        private string 매출처;
        private string 매출처주소;
        private string 매출처국가;
        private string 호선번호;
        private string 호선명;
        private string 영업조직명;
        private string 영업조직장;
        private string 서명;
        private string 창봉투매출처;
        private string 창봉투주소;
        private string 창봉투전화번호;
        private string 창봉투이메일;
        private string 창봉투비고;
        private string 국가코드;
        private string IMO번호;
        private string 대표자명;
        private string 인보이스번호;

        Dictionary<string, List<string>> _dic인수증;
        Dictionary<string, string> _dic첨부파일;
        Dictionary<string, string> _dic수주확인서;
        Dictionary<string, string> _dic고객발주서;
        #endregion

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);

        private bool Chk매출일자
        {
            get
            {
                return Checker.IsValid(this.dtp매출일자, true, this.DD("매출일자"));
            }
        }

        private bool Chk작성자
        {
            get
            {
                return !Checker.IsEmpty(this.ctx작성자, this.DD("작성자"));
            }
        }

        public P_CZ_SA_IV()
        {
            try
            {
                StartUp.Certify(this);
                InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_SA_IV(string 파일번호)
        {
            try
            {
                StartUp.Certify(this);
                InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this.MainGrids = new FlexGrid[] { this._flexH };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            this.InitGrid();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(Page_DataChanged);

            this.btn전자세금계산서.Click += new EventHandler(this.btn전자세금계산서_Click);
            this.btn매출관리.Click += new EventHandler(this.btn매출관리_Click);
            this.btn출고적용.Click += new EventHandler(this.btn출고적용_Click);

            this.btn부가세사업장변경.Click += new EventHandler(this.Control_Click);
            this.btn매출처변경.Click += new EventHandler(this.Control_Click);
            this.btn수금처변경.Click += new EventHandler(this.Control_Click);
            this.btn과세구분변경.Click += new EventHandler(this.Control_Click);
            this.btn거래구분변경.Click += new EventHandler(this.Control_Click);
            this.btn계정처리유형변경.Click += new EventHandler(this.Control_Click);
			this.btn자동매출등록.Click += Btn자동매출등록_Click;

            this.cbo거래구분.SelectionChangeCommitted += new EventHandler(this.cbo거래구분_SelectionChangeCommitted);

            this.ctx작성자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flexH_AfterCodeHelp);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);
            this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, true);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IV", "계산서번호", 100, false);
            this._flexH.SetCol("NO_IO", "수불번호", 80, false);
            this._flexH.SetCol("CD_BIZAREA", "사업장", false);
            this._flexH.SetCol("CD_BIZAREA_TAX", "부가세사업장", false);
            this._flexH.SetCol("CD_PARTNER", "매출처코드", false);
            this._flexH.SetCol("LN_PARTNER", "매출처", 120, false);
            this._flexH.SetCol("GI_PARTNER", "납품처코드", false);
            this._flexH.SetCol("CD_GI_PARTNER", "납품처", false);
            this._flexH.SetCol("BILL_PARTNER", "수금처코드", false);
            this._flexH.SetCol("BILL_LN_PARTNER", "수금처", 120, false);
            this._flexH.SetCol("NO_BIZAREA", "사업자등록번호", false);
            this._flexH.SetCol("CD_CON", "휴폐업구분", false);
            this._flexH.SetCol("CD_DOCU", "전표유형", 80, true);
            this._flexH.SetCol("FG_TAX", "과세구분", 80, false);
            this._flexH.SetCol("CD_EXCH", "통화명", 60, false);
            this._flexH.SetCol("RT_EXCH", "환율", 50, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM_EX", "매출(외화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_K", "매출(원화)", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("VAT_TAX", "부가세", 80, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_TOTAL", "매출합계", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("DT_RCP_RSV", "수금예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DC_REMARK", "비고", 200, 100, true);
            this._flexH.SetCol("TP_RECEIPT", "청구\n영수", 60, false);
            this._flexH.SetCol("MAX_LINE", "내역갯수", 60, false, typeof(decimal), FormatTpType.MONEY);         
            this._flexH.SetCol("TP_SUMTAX", "부가세계산법", false);          
            this._flexH.SetCol("FG_MAP", "적용구분", false);
            this._flexH.SetCol("CD_PJT", "특별여신", false);
            this._flexH.SetCol("FG_BILL", "결재방법", 100, true);
            this._flexH.SetCol("FG_CLS_TYPE", "마감유형", false);
            this._flexH.SetCol("AMT_DC", "할인금액", false);
            this._flexH.SetCol("FG_AR_EXC", "채권예외대상", 100, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("DT_RCP_PRETOLERANCE", "수금예정허용일(매출처)", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("DT_RCP_RSV1", "최종수금예정허용일", false);
            this._flexH.ExtendLastCol = true;
            this._flexH.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "CD_PARTNER", "LN_PARTNER");
            this._flexH.SetCodeHelpCol("BILL_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "BILL_PARTNER", "BILL_LN_PARTNER");
            this._flexH.VerifyNotNull = new string[] { "CD_PARTNER", "BILL_PARTNER" };
            this._flexH.SetExceptEditCol(new string[] { "NO_IV",
                                                        "CD_BIZAREA",
                                                        "CD_BIZAREA_TAX",
                                                        "CD_PARTNER",
                                                        "BILL_PARTNER",
                                                        "NO_BIZAREA",
                                                        "FG_TAX",
                                                        "AM_TOTAL",
                                                        "MAX_LINE",
                                                        "NO_IO",
                                                        "TP_SUMTAX",
                                                        "CD_EXCH",
                                                        "RT_EXCH",
                                                        "AM_EX",
                                                        "FG_MAP",
                                                        "CD_PJT",
                                                        "FG_CLS_TYPE",
                                                        "AMT_DC",
                                                        "DT_RCP_PRETOLERANCE" });

            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexH.SetStringFormatCol2("NO_BIZAREA", "AAA-AA-AAAAA");
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_IV", "마감번호", 100);
            this._flexL.SetCol("NO_LINE", "마감항번", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NO_IO", "출고번호", 100);
            this._flexL.SetCol("DT_IO", "출고일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_VESSEL", "호선명", 100, false);
            this._flexL.SetCol("NO_IOLINE", "출고항번", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flexL.SetCol("NM_ITEM", "품목명", 120, false);
            this._flexL.SetCol("STND_ITEM", "규격", 60, false);
            this._flexL.SetCol("UNIT_IM", "단위", 40, false);
            this._flexL.SetCol("QT_CLS_MM", "수량", 40, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_GI_CLS", "마감수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_EX_CLS", "마감외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_EX_CLS", "마감외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("RT_EXCH", "환율", 50, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("UM_ITEM_CLS", "마감단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("AM_CLS", "마감금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_TOT", "합계", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("QT_CLS", "관리수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("DT_LOADING", "선적일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NO_LC", "LC번호", false);
            this._flexL.SetCol("NM_SALEGRP", "영업그룹", 100, false);
            this._flexL.SetCol("NM_PROJECT", "프로젝트", 80, false);
            this._flexL.SetCol("FG_TRANS", "거래구분", 60, false);
            this._flexL.SetCol("NM_QTIOTP", "출고형태", false);
            this._flexL.SetCol("TP_IV", "매출형태", 80, true);
            this._flexL.SetCol("RT_VAT", "과세율(%)", 60, false);
            this._flexL.SetCol("GI_PARTNER", "납품처", 100, false);
            this._flexL.SetCol("DC_RMK", "비고", 150, true);
            this._flexL.SetCol("CD_CC", "C/C코드", false);
            this._flexL.SetCol("NM_CC", "C/C명", 100, false);
            this._flexL.SetCol("NM_MNGD1", "관리내역1", 80, true);
            this._flexL.SetCol("NM_MNGD2", "관리내역2", 80, true);
            this._flexL.SetCol("NM_MNGD3", "관리내역3", 80, true);
            this._flexL.SetCol("CD_MNGD4", "관리내역4", 80, true);
            this._flexL.SetCol("CD_SL", "출고창고코드", false);
            this._flexL.SetCol("NM_SL", "출고창고명", false);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flexL.SetCol("CD_PJTLINE", "UNIT 항번", 100, false, typeof(decimal));
                this._flexL.SetCol("CD_UNIT", "UNIT 코드", 100, false);
                this._flexL.SetCol("NM_UNIT", "UNIT 명", 100, false);
                this._flexL.SetCol("STND_UNIT", "UNIT 규격", 100, false);
            }

            this._flexL.SetCol("TXT_USERDEF1", "수주라인사용자정의TEXT1", 100);
            this._flexL.SetCol("TXT_USERDEF2", "수주라인사용자정의TEXT2", 100);
            this._flexL.Cols["TXT_USERDEF1"].Visible = false;
            this._flexL.Cols["TXT_USERDEF2"].Visible = false;

            if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
            {
                this._flexL.SetCol("CD_ITEM_REF", "SET품목", 120, false);
                this._flexL.SetCol("NM_ITEM_REF", "SET품명", 120, false);
                this._flexL.SetCol("STND_ITEM_REF", "SET규격", 120, false);
            }

            if (BASIC.GetMAEXC("배차사용유무") == "Y")
                this._flexL.SetCol("YN_PICKING", "배송여부", 80, false, CheckTypeEnum.Y_N);

            this._flexL.EnabledHeaderCheck = false;
            this._flexL.SetCodeHelpCol("NM_MNGD1", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD1", "NM_MNGD1" }, new string[] { "CD_MNGD", "NM_MNGD" });
            this._flexL.SetCodeHelpCol("NM_MNGD2", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD2", "NM_MNGD2" }, new string[] { "CD_MNGD", "NM_MNGD" });
            this._flexL.SetCodeHelpCol("NM_MNGD3", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD3", "NM_MNGD3" }, new string[] { "CD_MNGD", "NM_MNGD" });
            this._flexL.SettingVersion = "5.0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
           
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flexL.SetExceptSumCol(new string[] { "NO_LINE",
                                                           "NO_IOLINE",
                                                           "UM_EX_CLS",
                                                           "RT_EXCH",
                                                           "UM_ITEM_CLS",
                                                           "RT_VAT",
                                                           "CD_PJTLINE" });

                this._flexL.VerifyNotNull = new string[] { "CD_PJTLINE" };
            }
            else
                this._flexL.SetExceptSumCol(new string[] { "NO_LINE",
                                                           "NO_IOLINE",
                                                           "UM_EX_CLS",
                                                           "RT_EXCH",
                                                           "UM_ITEM_CLS",
                                                           "RT_VAT" });
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();

            #region 숨김설정
            this.bpc부가세사업장변경.Visible = false;
            this.bpc매출처변경.Visible = false;
            this.bpc수금처변경.Visible = false;
            
            //if (this.MainFrameInterface.LoginInfo.CompanyCode == "S100")
            //    this.bpc과세구분변경.Visible = true;
            //else
            //    this.bpc과세구분변경.Visible = false;

            this.bpc과세구분변경.Visible = true;

            if (this.MainFrameInterface.LoginInfo.CompanyCode == "S100")
                this.bpc거래구분변경.Visible = true;
            else
                this.bpc거래구분변경.Visible = false;
            
            this.bpc계정처리유형변경.Visible = false;
            #endregion

            DataTable dataTable2 = Global.MainFrame.FillDataTable(" SELECT CD_EXC FROM MA_EXC   WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'    AND EXC_TITLE = '여신한도' ");
            if (dataTable2.Rows.Count > 0 && (dataTable2.Rows[0]["CD_EXC"] != DBNull.Value && D.GetString(dataTable2.Rows[0]["CD_EXC"]) != string.Empty))
                this._여신한도전용코드 = D.GetString(dataTable2.Rows[0]["CD_EXC"]);

            this._수금예정일전용코드 = BASIC.GetMAEXC("수금예정일적용옵션");
            if (D.GetString(this._수금예정일전용코드) == string.Empty) this._수금예정일전용코드 = "000";

            this.dtp매출일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.dtp매출일자.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp매출일자.Text = Global.MainFrame.GetStringToday;

            this.ctx작성자.CodeValue = this.LoginInfo.EmployeeNo;
            this.ctx작성자.CodeName = this.LoginInfo.EmployeeName;

            this._부서 = this.LoginInfo.DeptCode;

            DataSet comboData = this.GetComboData(new string[] { "S;MA_BIZAREA",
                                                                 "S;MA_BIZAREA",
                                                                 "N;PU_C000016",
                                                                 "S;MA_B000040",
                                                                 "N;MA_B000005",
                                                                 "N;MA_AISPOSTH;100",
                                                                 "S;SA_B000002",
                                                                 "N;FI_J000002",
                                                                 "S;SA_B000002",
                                                                 "N;MA_B000073",
                                                                 "S;PU_C000016" });

            this.cbo거래구분.DataSource = comboData.Tables[2];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";

            this.cbo거래구분변경.DataSource = comboData.Tables[10];
            this.cbo거래구분변경.DisplayMember = "NAME";
            this.cbo거래구분변경.ValueMember = "CODE";

            this._flexH.SetDataMap("FG_TAX", comboData.Tables[3].Copy(), "CODE", "NAME");

            DataTable dataTable4 = comboData.Tables[3].Clone();
            DataRow row2 = dataTable4.NewRow();
            row2["CODE"] = string.Empty;
            row2["NAME"] = string.Empty;
            dataTable4.Rows.Add(row2);

            foreach (DataRow dataRow in comboData.Tables[3].Copy().Select("CODE IN ('11', '14', '15')"))
                dataTable4.Rows.Add(dataRow.ItemArray);

            this.cbo과세구분변경.DataSource = dataTable4;
            this.cbo과세구분변경.DisplayMember = "NAME";
            this.cbo과세구분변경.ValueMember = "CODE";

            this._flexH.SetDataMap("CD_EXCH", comboData.Tables[4], "CODE", "NAME");
            this._flexL.SetDataMap("FG_TRANS", comboData.Tables[2].Copy(), "CODE", "NAME");
            this._flexL.SetDataMap("TP_IV", comboData.Tables[5].Copy(), "CODE", "NAME");

            this.cbo계정처리유형.DataSource = comboData.Tables[5].Copy();
            this.cbo계정처리유형.DisplayMember = "NAME";
            this.cbo계정처리유형.ValueMember = "CODE";

            DataTable dataTable5 = new DataTable();
            dataTable5.Columns.Add("CODE", typeof(string));
            dataTable5.Columns.Add("NAME", typeof(string));
            DataRow row3 = dataTable5.NewRow();
            row3["CODE"] = "001";
            row3["NAME"] = "합계";
            dataTable5.Rows.Add(row3);
            DataRow row4 = dataTable5.NewRow();
            row4["CODE"] = "002";
            row4["NAME"] = "개별";
            dataTable5.Rows.Add(row4);

            this._flexH.SetDataMap("TP_SUMTAX", dataTable5, "CODE", "NAME");

            DataTable dataTable6 = new DataTable();
            dataTable6.Columns.Add("CODE", typeof(string));
            dataTable6.Columns.Add("NAME", typeof(string));
            DataRow row5 = dataTable6.NewRow();
            row5["CODE"] = "1";
            row5["NAME"] = this.DD("청구");
            dataTable6.Rows.Add(row5);
            DataRow row6 = dataTable6.NewRow();
            row6["CODE"] = "2";
            row6["NAME"] = this.DD("영수");
            dataTable6.Rows.Add(row6);

            this._flexH.SetDataMap("TP_RECEIPT", dataTable6, "CODE", "NAME");
            this._flexH.SetDataMap("CD_BIZAREA", comboData.Tables[0], "CODE", "NAME");
            this._flexH.SetDataMap("CD_BIZAREA_TAX", comboData.Tables[1], "CODE", "NAME");
            this._flexH.SetDataMap("FG_BILL", comboData.Tables[6], "CODE", "NAME");
            this._flexH.SetDataMap("CD_DOCU", comboData.Tables[7], "CODE", "NAME");
            this._flexH.SetDataMap("CD_CON", comboData.Tables[9], "CODE", "NAME");

            this.cbo결재방법.DataSource = (object)comboData.Tables[8];
            this.cbo결재방법.DisplayMember = "NAME";
            this.cbo결재방법.ValueMember = "CODE";

            this.ctx사업장.CodeValue = this.LoginInfo.BizAreaCode;
            this.ctx사업장.CodeName = this.LoginInfo.BizAreaName;

            this.ctx부가세사업장.CodeValue = this.LoginInfo.BizAreaCode;
            this.ctx부가세사업장.CodeName = this.LoginInfo.BizAreaName;

            this.ToolBarSearchButtonEnabled = false;

            this.ColsSetting("TXT_USERDEF", "SA_B000112", 1, 2);

            this.Page_DataChanged(null, null);
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this.ShowMessage("출고 적용버튼을 클릭하세요!");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd() || !this.BeforeSearch()) return;

                this._flexH.DataTable.Rows.Clear();
                this._flexH.AllowEditing = true;
                this._flexH.AcceptChanges();

                this.Page_DataChanged(null, null);

                this.ctx사업장.Focus();

                this.ToolBarSaveButtonEnabled = false;
                this.ToolBarAddButtonEnabled = false;
                this.ToolBarSearchButtonEnabled = false;

                this._flexL.DataTable.Rows.Clear();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            string query;
            DataTable dt, dt1;

            try
            {
                query = @"SELECT 1
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
	         MAX(OL.NO_ISURCV) AS NO_GIR
	  FROM MM_QTIO OL WITH(NOLOCK)
	  GROUP BY OL.CD_COMPANY, OL.NO_IO) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
JOIN CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = OL.CD_COMPANY AND WD.NO_GIR = OL.NO_GIR
LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
		          MAX(GL.NO_INV) AS NO_INV
	       FROM SA_GIRL GL WITH(NOLOCK) 
	       GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL 
ON GL.CD_COMPANY = OL.CD_COMPANY AND GL.NO_GIR = OL.NO_GIR
LEFT JOIN CZ_TR_INVH TH ON TH.CD_COMPANY = GL.CD_COMPANY AND TH.NO_INV = GL.NO_INV
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'
AND WD.CD_MAIN_CATEGORY = '003'
AND WD.CD_SUB_CATEGORY IN ('001', '002', '003')
AND WD.CD_DELIVERY_TO IN ('01107', '02436', '08624', 'DLV230200274')
AND ISNULL(TH.REMARK4, '') = ''";

                foreach (DataRow dr in this._flexH.DataTable.Rows)
				{
                    dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_IO"].ToString()));

                    if (dt != null && dt.Rows.Count > 0)
					{
                        dt1 = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IV_SUBL_S '{0}', '{1}', @P_FG_DT_LOADING = 'A'", Global.MainFrame.LoginInfo.CompanyCode, dr["NO_IO"].ToString()));

                        if (dt1 != null && dt1.Select("CD_ITEM = 'SD0001'").Length == 0)
						{
                            this.ShowMessage("포워딩비용이 청구되지 않는 건이 있습니다.\n출고번호 : " + dr["NO_IO"].ToString());
						}
					}
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return base.BeforeSave();
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);

                this.ToolBarSaveButtonEnabled = false;
                this.ToolBarSearchButtonEnabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            string str1 = string.Empty;
            string 매출일자;
            string 계산서번호, filter;

            try
            {
                if (!base.SaveData() || !this.Verify() || (!this.Chk매출일자 || !this.Chk작성자)) return false;
                if ((int)this._flexL.DataTable.Compute("COUNT(NO_IO)", " DT_IO > '" + this.dtp매출일자.Text.ToString() + "'") > 0 && this.ShowMessage("적용받은 출고건중 출고일이 매출일자보다 이후인건이 있습니다. 계속하시겠습니까?", "IK2") == DialogResult.Cancel)
                    return false;

                for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                {
                    MsgControl.ShowMsg("적용 데이터를 검사중입니다. \r\n잠시만 기다려주세요!\r\n@/@", new string[] { D.GetString(@fixed), D.GetString(this._flexH.Rows.Count) });
                    
                    switch (D.GetString(this._flexH[@fixed, "FG_TAX"]))
                    {
                        case "11":
                        case "12":
                        case "13":
                        case "18":
                            if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR && (D.GetDecimal(this._flexH[@fixed, "AM_K"]) != 0 && D.GetDecimal(this._flexH[@fixed, "VAT_TAX"]) == 0))
                            {
                                MsgControl.CloseMsg();
                                this.ShowMessage("CZ_과세구분이 [과세대상]인데 공급가액에 대한 부가세가 0 입니다.");
                                this._flexH.Select(@fixed, "AM_RCP");
                                this._flexH.Focus();
                                return false;
                            }
                            break;
                    }

                    if (D.GetString(this.cbo거래구분.SelectedValue) == "001" && D.GetString(this._flexH[@fixed, "NO_BIZAREA"]) == string.Empty)
                    {
                        MsgControl.CloseMsg();
                        this.ShowMessage("CZ_매출처[@] 사업장번호가 등록되어 있지 않습니다. \r\n[시스템관리 - 거래처정보]를 확인하세요", D.GetString(this._flexH[@fixed, "LN_PARTNER"]));
                        this._flexH.Select(@fixed, "AM_RCP");
                        this._flexH.Focus();
                        return false;
                    }

                    if (D.GetString(this._flexH[@fixed, "CD_BIZAREA_TAX"]) == string.Empty)
                    {
                        MsgControl.CloseMsg();
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("부가세사업장") });
                        this._flexH.Select(@fixed, "CD_BIZAREA_TAX");
                        this._flexH.Focus();
                        return false;
                    }
                }

                this._flexH.Redraw = false;
                this._flexL.Redraw = false;

                for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                {
                    MsgControl.ShowMsg("적용 데이터를 저장중입니다. \r\n잠시만 기다려주세요!\r\n@/@", new string[] { D.GetString(@fixed), D.GetString(this._flexH.Rows.Count - 1)});
                    
                    if (!(D.GetString(this._flexH[@fixed, "NO_IV"]) != string.Empty))
                    {
                        filter = "NO_IV = '" + D.GetString(this._flexH[@fixed, "NO_IV_PRE"]) + "'";

                        DataRow[] dataRowArray = this._flexL.DataTable.Select(filter, "", DataViewRowState.CurrentRows);
                        if (dataRowArray == null || dataRowArray.Length == 0)
                        {
                            MsgControl.CloseMsg();
                            this.ShowMessage("계산서번호에 해당하는 내역이 존재하지 않습니다.");
                            this._flexH.Select(@fixed, "NO_IV");
                            return false;
                        }

                        if (this._flexH[@fixed, "YN_AUTO_NUM"].ToString() == "Y" ||
                            D.GetString(this.cbo거래구분.SelectedValue) == "001")
                            계산서번호 = D.GetString(this.GetSeq(LoginInfo.CompanyCode, "SA", "05"));
                        else
                            계산서번호 = D.GetString(this.GetSeq(LoginInfo.CompanyCode, "SA", "05")).Substring(0, 2) + this._flexH[@fixed, "NO_IO"].ToString().Substring(2);

                        DataTable dtH = this._flexH.DataTable.Clone();
                        dtH.ImportRow(this._flexH.GetDataRow(@fixed));
                        dtH.Rows[dtH.Rows.Count - 1]["NO_IV"] = 계산서번호;

                        DataTable dtL = this._flexL.DataTable.Clone();
                        foreach (DataRow row in dataRowArray)
                            dtL.ImportRow(row);

                        foreach (DataRow dataRow in dtL.Rows)
                        {
                            if (D.GetString(dataRow["CD_CC"]) == string.Empty && Sa_Global.IVL_CdCc == "001")
                            {
                                MsgControl.CloseMsg();
                                this.ShowMessage("수주유형등록의 해당 거래구분에 대한 C/C 설정이 되지 않았습니다. \n\n해당 거래구분에 대한 C/C를 설정 후 다시 출고적용 받으시기 바랍니다.");
                                return false;
                            }

                            dataRow["NO_IV"] = 계산서번호;
                            if (!(D.GetString(dataRow["YN_RETURN"]) == "N"))
                            {
                                if (D.GetDecimal(dataRow["QT_GI_CLS"]) > 0)
                                {
                                    MsgControl.CloseMsg();
                                    this.ShowMessage("반품인 경우 마감대상잔량이 0 보다 클 수 없습니다.");
                                    return false;
                                }

                                dataRow["QT_CLS_MM"] = - D.GetDecimal(dataRow["QT_CLS_MM"]);
                                dataRow["QT_GI_CLS"] = - D.GetDecimal(dataRow["QT_GI_CLS"]);
                                dataRow["AM_CLS"] = - D.GetDecimal(dataRow["AM_CLS"]);
                                dataRow["VAT"] = - D.GetDecimal(dataRow["VAT"]);
                                dataRow["QT_CLS"] = - D.GetDecimal(dataRow["QT_CLS"]);
                                dataRow["AM_EX_CLS"] = - D.GetDecimal(dataRow["AM_EX_CLS"]);
                                dataRow["UM_ITEM_CLS"] = -D.GetDecimal(dataRow["UM_ITEM_CLS"]);
                                dataRow["UM_EX_CLS"] = -D.GetDecimal(dataRow["UM_EX_CLS"]);
                            }
                        }

                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100" ||
                            D.GetString(this.cbo거래구분.SelectedValue) == "001")
                            매출일자 = this.dtp매출일자.Text;
                        else
                            매출일자 = D.GetString(dtL.Compute("MAX(DT_LOADING)", string.Empty));

                        this._biz.Save(dtH, dtL, D.GetString(this.cbo거래구분.SelectedValue), 매출일자, this._부서, this.ctx작성자.CodeValue);

                        this._flexH[@fixed, "NO_IV_PRE"] = 계산서번호;
                        this._flexH[@fixed, "NO_IV"] = 계산서번호;

                        foreach (DataRow dataRow in dataRowArray)
                            dataRow["NO_IV"] = 계산서번호;
                    }
                }

                foreach (DataRow dr in this._flexH.DataTable.Rows)
                {
                    DataTable dt1 = DBHelper.GetDataTable("SP_CZ_SA_IV_LIV_WARNING", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                    dr["NO_IO"].ToString() });

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            string contents = @"** 물류비 검증 시스템 알림

수주번호 : {0}
이윤 : {1}원
포장금액 : {2}원

이윤 대비 포장금액이 많이 발생 했습니다.

** 발생 원인 확인 후 회신바랍니다. (회신대상: 이정철JM, 부서장, 팀장)
※ 본 쪽지는 발신전용 입니다.";

                            contents = string.Format(contents, dr1["NO_SO"].ToString(), string.Format("{0:#,##0}", dr1["AM_PROFIT"]), string.Format("{0:#,##0}", dr1["AM_PACK"]));

                            Messenger.SendMSG(new string[] { dr1["NO_EMP"].ToString(), dr1["NO_EMP_LOG"].ToString() }, contents);

                            DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_SOH 
SET DT_USERDEF1 = CONVERT(CHAR(8), GETDATE(), 112) 
WHERE CD_COMPANY = '{0}' 
AND NO_SO = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, dr1["NO_SO"].ToString()));

                            DBHelper.ExecuteScalar(string.Format(@"INSERT INTO CZ_SA_AUTO_MSG_LOG
	(
		CD_COMPANY,
		TP_MSG,
		DC_EMP,
		DC_CONTENTS,
		DTS_INSERT
	)
	VALUES
	(
		'{0}',
		'CZ_SA_IV',
		'{1}',
		'{2}',
		NEOE.SF_SYSDATE(GETDATE())
	)", Global.MainFrame.LoginInfo.CompanyCode, (dr1["NO_EMP"].ToString() + "|" + dr1["NO_EMP_LOG"].ToString()), contents));
                        }
                    }
                }

                this._flexH.AcceptChanges();
                this._flexL.AcceptChanges();

                return true;
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;
                this._flexH_AfterRowChange(null, null);
            }
        }

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
            this.임시파일제거();
            return base.OnToolBarExitButtonClicked(sender, e);
		}
		#endregion

		#region 버튼 이벤트
		private void btn전자세금계산서_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsExistPage("P_SA_ETAX36524D_REG", false))
                    this.UnLoadPage("P_SA_ETAX36524D_REG", false);

                this.Grant.CanSearch = true;
                this.Grant.CanPrint = true;
                this.Grant.CanDelete = true;

                this.CallOtherPageMethod("P_SA_ETAX36524D_REG", "전자세금계산서발행(36524D)", "P_SA_ETAX36524D_REG", this.Grant, (object[])null);

                this.Grant.CanSearch = false;
                this.Grant.CanPrint = false;
                this.Grant.CanDelete = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn매출관리_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsExistPage("P_CZ_SA_IVMNG", false))
                    this.UnLoadPage("P_CZ_SA_IVMNG", false);

                this.LoadPageFrom("P_CZ_SA_IVMNG", "매출관리(딘텍)", this.Grant, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn출고적용_Click(object sender, EventArgs e)
        {
            decimal num2;
            string filter, 프로젝트;

            try
            {
                if (string.IsNullOrEmpty(this.ctx사업장.CodeValue))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl사업장.Text });
                    this.ctx사업장.Focus();
                }
                else if (string.IsNullOrEmpty(D.GetString(this.cbo거래구분.SelectedValue)))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl거래구분.Text });
                    this.cbo거래구분.Focus();
                }
                else
                {
                    P_CZ_SA_IV_SUB pSaIvSub = new P_CZ_SA_IV_SUB(D.GetString(this.cbo거래구분.SelectedValue),
                                                                 this.cbo거래구분.Text,
                                                                 this.dtp매출일자.Text);

                    if (pSaIvSub.ShowDialog() == DialogResult.OK)
                    {
                        this._flexL.RowFilter = string.Empty;

                        this._dtL = pSaIvSub.출고데이터;

                        this._flexH.Binding = this._biz.Search();
                        this._flexH.Redraw = false;
                        this._하단그리드필터여부 = false;

                        for (int i = 0; i < this._dtL.Rows.Count; ++i)
                        {
                            MsgControl.ShowMsg("적용 데이터를 집계중입니다. \r\n잠시만 기다려주세요!\r\n@/@", new string[] { i.ToString(), this._dtL.Rows.Count.ToString()});
                            
                            string str2 = D.GetString(this._dtL.Rows[i]["LN_PARTNER"]);
                            string str3 = D.GetString(this._dtL.Rows[i]["NO_BIZAREA"]);
                            string str4 = D.GetString(this._dtL.Rows[i]["TP_SUMTAX"]);
                            string str5 = D.GetString(this._dtL.Rows[i]["DT_RCP_PREARRANGED"]);
                            string str6 = D.GetString(this._dtL.Rows[i]["TP_RCP_DD"]);
                            string str7 = D.GetString(this._dtL.Rows[i]["DT_RCP_DD"]);
                            string str8 = D.GetString(this._dtL.Rows[i]["FG_BILL"]);
                            string str9 = D.GetString(this._dtL.Rows[i]["CD_CON"]);

                            num2 = 0;

                            if (this._dtL.Columns.Contains("DT_RCP_PRETOLERANCE")) 
                                num2 = D.GetDecimal(this._dtL.Rows[i]["DT_RCP_PRETOLERANCE"]);

                            if (str4.Trim() == "") str4 = "001";

                            if (this.rdo일괄.Checked)
                                filter = "CD_PARTNER = '" + D.GetString(this._dtL.Rows[i]["CD_PARTNER"]) + "'";
                            else
                                filter = "NO_IO = '" + D.GetString(this._dtL.Rows[i]["NO_IO"]) + "'";

                            if (this.cbo거래구분.SelectedValue.ToString() == "001")
                                filter += " AND FG_TAX = '" + D.GetString(this._dtL.Rows[i]["FG_TAX"]) + "'";
                            else if (this.rdo환차손YES.Checked)
                                filter += " AND CD_EXCH = '" + D.GetString(this._dtL.Rows[i]["CD_EXCH"]) + "' AND RT_EXCH = '" + D.GetString(this._dtL.Rows[i]["RT_EXCH"]) + "'";

                            프로젝트 = string.Empty;
                            if (this._여신한도전용코드 == "200")
                            {
                                if (D.GetString(this._dtL.Rows[i]["YN_PJTCREDIT"]) == "Y")
                                {
                                    프로젝트 = D.GetString(this._dtL.Rows[i]["CD_PJT"]);
                                    filter += " AND ISNULL(CD_PJT,'') = '" + 프로젝트 + "'";
                                }
                            }

                            DataRow[] dataRowArray = this._flexH.DataTable.Select(filter);
                            string str14;
                            decimal num4;
                            if (dataRowArray.Length == 0)
                            {
                                str14 = this._flexH.Rows.Count.ToString();
                                num4 = 1;
                                int index2 = this._flexH.Rows.Add().Index;
                                this._flexH[index2, "NO_IV_PRE"] = str14;
                                this._flexH[index2, "NO_BIZAREA"] = str3;
                                this._flexH[index2, "CD_BIZAREA_TAX"] = D.GetString(this._dtL.Rows[i]["CD_BIZAREA"]);
                                this._flexH[index2, "CD_BIZAREA"] = D.GetString(this._dtL.Rows[i]["CD_BIZAREA"]);
                                this._flexH[index2, "CD_PARTNER"] = D.GetString(this._dtL.Rows[i]["CD_PARTNER"]);
                                this._flexH[index2, "LN_PARTNER"] = str2;
                                this._flexH[index2, "BILL_PARTNER"] = D.GetString(this._dtL.Rows[i]["CD_PARTNER"]);
                                this._flexH[index2, "BILL_LN_PARTNER"] = str2;
                                this._flexH[index2, "FG_BILL"] = str8;
                                this._flexH[index2, "CD_CON"] = str9;

                                if (this._dtL.Columns.Contains("DT_RCP_PRETOLERANCE")) this._flexH[index2, "DT_RCP_PRETOLERANCE"] = num2;

                                this._flexH[index2, "AM_K"] = (D.GetDecimal(this._flexH[index2, "AM_K"]) + D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]));
                                this._flexH[index2, "VAT_TAX"] = (D.GetDecimal(this._flexH[index2, "VAT_TAX"]) + D.GetDecimal(this._dtL.Rows[i]["VAT"]));
                                this._flexH[index2, "AM_TOTAL"] = (D.GetDecimal(this._flexH[index2, "AM_K"]) + D.GetDecimal(this._flexH[index2, "VAT_TAX"]));
                                this._flexH[index2, "FG_TAX"] = D.GetString(this._dtL.Rows[i]["FG_TAX"]);
                                this._flexH[index2, "TP_FD"] = "D";
                                this._flexH[index2, "TP_RECEIPT"] = "1";
                                this._flexH[index2, "TP_SUMTAX"] = str4;
                                this._flexH[index2, "FG_TAXP"] = (this.rdo일괄.Checked ? "001" : "002");
                                this._flexH[index2, "TP_AIS"] = "N";
                                this._flexH[index2, "YN_EXPIV"] = "N";
                                this._flexH[index2, "NO_LC"] = D.GetString(this._dtL.Rows[i]["NO_LC"]);

                                if (this._수금예정일전용코드 == "000")
                                    this._flexH[index2, "DT_RCP_RSV"] = this._commFun.DateAdd(this.dtp매출일자.Text.ToString(), "D", Convert.ToInt32(str5));
                                else if (this._수금예정일전용코드 == "100")
                                {
                                    this._flexH[index2, "DT_RCP_RSV"] = this.dtp매출일자.Text;
                                    if (str6.Trim() != "")
                                    {
                                        string str13 = this._commFun.DateAdd(this.dtp매출일자.Text.ToString().Substring(0, 6) + "01", "M", D.GetInt(str6));
                                        string str15 = this._commFun.DateAdd(str13.ToString().Substring(0, 6) + "01", "D", D.GetInt(str7) - 1);
                                        if (D.StringDate.IsValidDate(str15, false, ""))
                                            this._flexH[index2, "DT_RCP_RSV"] = str15;
                                        else if (D.GetInt(str7) == 0)
                                            this._flexH[index2, "DT_RCP_RSV"] = str13;
                                        else
                                            this._flexH[index2, "DT_RCP_RSV"] = this._commFun.GetLastDayDate(str13);
                                    }
                                }
                                else if (this._수금예정일전용코드 == "200")
                                {
                                    this._flexH[index2, "DT_RCP_RSV"] = this._commFun.DateAdd(this.dtp매출일자.Text.ToString(), "D", Convert.ToInt32(str5));
                                    this._flexH[index2, "DT_RCP_RSV1"] = this._commFun.DateAdd(D.GetString(this._flexH[index2, "DT_RCP_RSV"]), "D", Convert.ToInt32(num2));
                                }

                                this._flexH[index2, "DC_REMARK"] = D.GetString(this._dtL.Rows[i]["DC_REMARK"]);
                                this._flexH[index2, "MAX_LINE"] = num4;
                                this._flexH[index2, "NO_IO"] = D.GetString(this._dtL.Rows[i]["NO_IO"]);

                                if (this._dtL.Rows[i]["YN_AUTO"].ToString() == "Y" ||
                                    this._dtL.Rows[i]["YN_RETURN"].ToString() == "Y")
                                    this._flexH[index2, "YN_AUTO_NUM"] = "Y";
                                else
                                    this._flexH[index2, "YN_AUTO_NUM"] = "N";

                                if (this.rdo환차손YES.Checked)
                                {
                                    this._flexH[index2, "CD_EXCH"] = D.GetString(this._dtL.Rows[i]["CD_EXCH"]);
                                    this._flexH[index2, "RT_EXCH"] = D.GetDecimal(this._dtL.Rows[i]["RT_EXCH"]);
                                    this._flexH[index2, "AM_EX"] = (D.GetDecimal(this._flexH[index2, "AM_EX"]) + D.GetDecimal(this._dtL.Rows[i]["AM_EX_CLS"]));
                                    this._flexH[index2, "FG_MAP"] = "1";
                                    //this._flexH[index2, "AM_K"] = this.원화계산(D.GetDecimal(this._flexH[index2, "AM_EX"]) * D.GetDecimal(this._dtL.Rows[i]["RT_EXCH"]));
                                }
                                else
                                {
                                    this._flexH[index2, "CD_EXCH"] = "000";
                                    this._flexH[index2, "RT_EXCH"] = 1;
                                    this._flexH[index2, "AM_EX"] = (D.GetDecimal(this._flexH[index2, "AM_EX"]) + D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]));
                                    this._flexH[index2, "FG_MAP"] = "0";
                                }

                                this._flexH[index2, "CD_PJT"] = 프로젝트;
                                this._flexH[index2, "AMT_MAX"] = D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]);
                                this._flexH[index2, "CD_DOCU"] = D.GetString(this._dtL.Rows[i]["CD_DOCU"]);
                                this._flexH[index2, "FG_CLS_TYPE"] = "0";

                                if (this._dtL.Columns.Contains("SH_T_TP_BILL"))
                                    this._flexH[index2, "SH_T_TP_BILL"] = D.GetString(this._dtL.Rows[i]["SH_T_TP_BILL"]);
                                if (this._dtL.Columns.Contains("SH_T_TP_CD"))
                                    this._flexH[index2, "SH_T_TP_CD"] = D.GetString(this._dtL.Rows[i]["SH_T_TP_CD"]);

                                this._flexH[index2, "TP_IV"] = D.GetString(this._dtL.Rows[i]["TP_IV"]);
                            }
                            else
                            {
                                str14 = dataRowArray[0]["NO_IV_PRE"].ToString();
                                num4 = Decimal.Add(D.GetDecimal(dataRowArray[0]["MAX_LINE"]), 1);
                                dataRowArray[0]["MAX_LINE"] = num4;
                                dataRowArray[0]["AM_K"] = (D.GetDecimal(dataRowArray[0]["AM_K"]) + D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]));
                                dataRowArray[0]["VAT_TAX"] = (D.GetDecimal(dataRowArray[0]["VAT_TAX"]) + D.GetDecimal(this._dtL.Rows[i]["VAT"]));
                                dataRowArray[0]["AM_TOTAL"] = (D.GetDecimal(dataRowArray[0]["AM_K"]) + D.GetDecimal(dataRowArray[0]["VAT_TAX"]));

                                if (this.rdo환차손YES.Checked)
                                {
                                    dataRowArray[0]["AM_EX"] = (D.GetDecimal(dataRowArray[0]["AM_EX"]) + D.GetDecimal(this._dtL.Rows[i]["AM_EX_CLS"]));
                                    //dataRowArray[0]["AM_K"] = this.원화계산(D.GetDecimal(dataRowArray[0]["AM_EX"]) * D.GetDecimal(dataRowArray[0]["RT_EXCH"]));
                                }
                                else
                                    dataRowArray[0]["AM_EX"] = (D.GetDecimal(dataRowArray[0]["AM_EX"]) + D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]));

                                dataRowArray[0]["AM_TOT"] = (D.GetDecimal(dataRowArray[0]["AM_K"]) + D.GetDecimal(dataRowArray[0]["VAT_TAX"]));

                                if (D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]) > D.GetDecimal(dataRowArray[0]["AMT_MAX"]))
                                {
                                    dataRowArray[0]["AMT_MAX"] = D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]);
                                    dataRowArray[0]["CD_DOCU"] = this._dtL.Rows[i]["CD_DOCU"].ToString().Trim();
                                }

                                if (D.GetString(this._dtL.Rows[i]["NO_IO"]) != D.GetString(dataRowArray[0]["NO_IO"]))
                                {
                                    dataRowArray[0]["DC_REMARK"] = string.Empty;
                                    
                                    if (this._dtL.Rows[i]["YN_AUTO"].ToString() == "N" &&
                                        this._dtL.Rows[i]["YN_RETURN"].ToString() == "N")
                                    {
                                        dataRowArray[0]["NO_IO"] = this._dtL.Rows[i]["NO_IO"].ToString();
                                        dataRowArray[0]["YN_AUTO_NUM"] = "N";
                                    }
                                }
                            }

                            this._dtL.Rows[i]["NO_IV"] = str14;
                            this._dtL.Rows[i]["NO_LINE"] = num4;
                            this._dtL.Rows[i]["AM_TOT"] = (D.GetDecimal(this._dtL.Rows[i]["AM_CLS"]) + D.GetDecimal(this._dtL.Rows[i]["VAT"]));

                            this._dtL.Rows[i].AcceptChanges();
                            this._dtL.Rows[i].SetAdded();
                        }

                        this._하단그리드필터여부 = true;
                        this._flexH.Redraw = true;
                        this._flexH.IsDataChanged = true;
                        this._flexL.Binding = this._dtL;

                        this._flexH.Row = (this._flexH.Rows.Count - 1);
                        this._flexH_AfterRowChange(null, null);
                    }
                }
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

        private void Btn자동매출등록_Click(object sender, EventArgs e)
        {
            try
            {
				DataTable dt = this._biz.SearchAuto(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty });

				if (dt == null || dt.Rows.Count == 0)
				{
					this.ShowMessage("자동매출등록 대상이 없습니다.");
					return;
				}

				int index = 0;
                bool result = true;
                foreach (DataRow dr in dt.Rows)
                {
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dt.Rows.Count) });

                    if (this.자동매출등록(dr["CD_COMPANY"].ToString(),
                                          dr["NO_IO"].ToString(),
                                          dr["NO_GIR"].ToString(),
                                          dr["DT_IO"].ToString(),
                                          dr["DT_LOADING"].ToString(),
                                          (dr["YN_COLOR"].ToString() == "Y" ? true : false),
                                          dr["TP_INV"].ToString(),
                                          dr["FROM_EMAIL"].ToString(),
                                          dr["TO_EMAIL"].ToString(),
                                          dr["CC"].ToString(),
                                          dr["WEB_EMAIL"].ToString(),
                                          dr["YN_CI"].ToString(),
                                          dr["YN_PL"].ToString(),
                                          dr["YN_ACK"].ToString(),
                                          dr["YN_PO"].ToString(),
                                          dr["YN_STATUS1"].ToString(),
                                          dr["YN_PARTIAL"].ToString()))
                    {
                        this._biz.SaveAutoLog(new object[] { dr["CD_COMPANY"].ToString(),
                                                             dr["NO_IO"].ToString(),
                                                             dr["DT_IO"].ToString(),
                                                             dr["DT_LOADING"].ToString(),
                                                             dr["YN_COLOR"].ToString(),
                                                             dr["TP_INV"].ToString(),
                                                             dr["FROM_EMAIL"].ToString(),
                                                             dr["TO_EMAIL"].ToString(),
                                                             dr["CC"].ToString(),
                                                             dr["WEB_EMAIL"].ToString(),
                                                             "Y",
                                                             Global.MainFrame.LoginInfo.UserID });
                    }
                    else
					{
                        this._biz.SaveAutoLog(new object[] { dr["CD_COMPANY"].ToString(),
                                                             dr["NO_IO"].ToString(),
                                                             dr["DT_IO"].ToString(),
                                                             dr["DT_LOADING"].ToString(),
                                                             dr["YN_COLOR"].ToString(),
                                                             dr["TP_INV"].ToString(),
                                                             dr["FROM_EMAIL"].ToString(),
                                                             dr["TO_EMAIL"].ToString(),
                                                             dr["CC"].ToString(),
                                                             dr["WEB_EMAIL"].ToString(),
                                                             "N",
                                                             Global.MainFrame.LoginInfo.UserID });

                        if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                            Messenger.SendMSG(new string[] { "S-145" }, "자동매출등록 실패 했습니다.\n출고번호 : " + dr["NO_IO"].ToString());
                        else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                            Messenger.SendMSG(new string[] { "D-011" }, "자동매출등록 실패 했습니다.\n출고번호 : " + dr["NO_IO"].ToString());
                        else
						{

						}

                        this.ShowMessage("자동매출등록 실패 했습니다.\n출고번호 : " + dr["NO_IO"].ToString());
                        result = false;
                        break;
                    }
                }

                if (result)
                    this.ShowMessage("자동매출등록 작업을 완료 했습니다.");
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

        private void Control_Click(object sender, EventArgs e)
        {
            string name, 과세구분;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flexH.Redraw = false;

                    name = ((Control)sender).Name;

                    if (name == this.btn매출처변경.Name)
                    {
                        #region 매출처변경
                        if (string.IsNullOrEmpty(this.ctx매출처.CodeValue))
                        {
                            this.ShowMessage("매출처를 지정하십시요");
                            this._flexH.Redraw = true;
                            return;
                        }

                        foreach (DataRow dataRow in dataRowArray1)
                        {
                            dataRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                            dataRow["LN_PARTNER"] = this.ctx매출처.CodeName;
                            
                            object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this.ctx매출처.CodeValue };

                            dataRow["NO_BIZAREA"] = ComFunc.MasterSearch("MA_PARTNER", objArray);
                        }
                        #endregion
                    }
                    else if (name == this.btn수금처변경.Name)
                    {
                        #region 수금처변경
                        if (string.IsNullOrEmpty(this.ctx수금처.CodeValue))
                        {
                            this.ShowMessage("수금처를 지정하십시요");
                            this._flexH.Redraw = true;
                            return;
                        }

                        foreach (DataRow dataRow in dataRowArray1)
                        {
                            dataRow["BILL_PARTNER"] = this.ctx수금처.CodeValue;
                            dataRow["BILL_LN_PARTNER"] = this.ctx수금처.CodeName;
                        }
                        #endregion
                    }
                    else if (name == this.btn부가세사업장변경.Name)
                    {
                        #region 부가세사업장변경
                        if (string.IsNullOrEmpty(this.ctx부가세사업장.CodeValue))
                        {
                            this.ShowMessage("부가세사업장을 지정하십시요");
                            this._flexH.Redraw = true;
                            return;
                        }

                        foreach (DataRow dataRow in dataRowArray1)
                            dataRow["CD_BIZAREA_TAX"] = this.ctx부가세사업장.CodeValue;
                        #endregion
                    }
                    else if (name == this.btn거래구분변경.Name)
                    {
                        #region 거래구분변경
                        if (this._flexH == null || this._flexH.DataTable == null || this._flexH.DataTable.Rows.Count == 0 || (this.cbo거래구분변경.SelectedValue == null ? string.Empty : this.cbo거래구분변경.SelectedValue.ToString()) == string.Empty)
                            return;

                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y'");
                        if (dataRowArray2 == null || dataRowArray2.Length == 0)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다);
                            return;
                        }

                        if (D.GetString(this.cbo거래구분변경.SelectedValue) == "001")
                            과세구분 = "11";
                        else
                            과세구분 = "15";

                        Decimal tpvat1 = BASIC.GetTPVAT(과세구분);
                        foreach (DataRow dataRow1 in dataRowArray2)
                        {
                            this.cbo거래구분.SelectedValue = this.cbo거래구분변경.SelectedValue.ToString();
                            dataRow1["FG_TAX"] = 과세구분;
                            dataRow1["VAT_TAX"] = 0;
                            
                            foreach (DataRow dataRow2 in this._flexL.DataTable.Select("ISNULL(NO_IV, '') = '" + dataRow1["NO_IV_PRE"].ToString() + "'"))
                            {
                                dataRow2["FG_TRANS"] = D.GetString(this.cbo거래구분변경.SelectedValue);
                                dataRow2["FG_TAX"] = 과세구분;
                                dataRow2["FG_TAX_VAT"] = tpvat1;
                                dataRow2["RT_VAT"] = tpvat1;
                                dataRow2["VAT"] = this.원화계산(D.GetDecimal(dataRow2["AM_CLS"]) * (tpvat1 / 100));
                                dataRow2["AM_TOT"] = (D.GetDecimal(dataRow2["AM_CLS"]) + D.GetDecimal(dataRow2["VAT"]));

                                dataRow1["VAT_TAX"] = (D.GetDecimal(dataRow1["VAT_TAX"]) + D.GetDecimal(dataRow2["VAT"]));
                            }

                            dataRow1["AM_TOTAL"] = (D.GetDecimal(dataRow1["AM_K"]) + D.GetDecimal(dataRow1["VAT_TAX"]));
                        }

                        this._flexL.SumRefresh();
                        #endregion
                    }
                    else if (name == this.btn과세구분변경.Name)
                    {
                        #region 과세구분변경
                        if (this._flexH == null || this._flexH.DataTable == null || this._flexH.DataTable.Rows.Count == 0)
                            return;

                        string @string = D.GetString(this.cbo과세구분변경.SelectedValue);
                        if (@string == string.Empty)
                            return;

                        DataRow[] dataRowArray3 = this._flexH.DataTable.Select("S = 'Y'");
                        if (dataRowArray3 == null || dataRowArray3.Length == 0)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다);
                            return;
                        }

                        Decimal tpvat2 = BASIC.GetTPVAT(@string);
                        foreach (DataRow dataRow1 in dataRowArray3)
                        {
                            dataRow1["FG_TAX"] = @string;
                            dataRow1["VAT_TAX"] = 0;
                            
                            foreach (DataRow dataRow2 in this._flexL.DataTable.Select("ISNULL(NO_IV, '') = '" + dataRow1["NO_IV_PRE"].ToString() + "'"))
                            {
                                dataRow2["FG_TAX"] = @string;
                                dataRow2["FG_TAX_VAT"] = tpvat2;
                                dataRow2["RT_VAT"] = tpvat2;
                                dataRow2["VAT"] = this.원화계산(D.GetDecimal(dataRow2["AM_CLS"]) * (tpvat2 / 100));
                                dataRow2["AM_TOT"] = (D.GetDecimal(dataRow2["AM_CLS"]) + D.GetDecimal(dataRow2["VAT"]));

                                dataRow1["VAT_TAX"] = (D.GetDecimal(dataRow1["VAT_TAX"]) + D.GetDecimal(dataRow2["VAT"]));
                            }

                            dataRow1["AM_TOTAL"] = (D.GetDecimal(dataRow1["AM_K"]) + D.GetDecimal(dataRow1["VAT_TAX"]));
                        }

                        this._flexL.SumRefresh();
                        #endregion
                    }
                    else if (name == this.btn계정처리유형변경.Name)
                    {
                        #region 계정처리유형변경
                        if (this._flexH == null || this._flexH.DataTable == null || this._flexH.DataTable.Rows.Count == 0 || (this.cbo계정처리유형.SelectedValue == null ? string.Empty : this.cbo계정처리유형.SelectedValue.ToString()) == string.Empty)
                            return;

                        DataRow[] dataRowArray4 = this._flexH.DataTable.Select("S ='Y'");
                        if (dataRowArray4 == null || dataRowArray4.Length == 0)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다);
                            return;
                        }
                        else
                        {
                            foreach (DataRow dataRow1 in dataRowArray4)
                            {
                                foreach (DataRow dataRow2 in this._flexL.DataTable.Select("ISNULL(NO_IV, '') = '" + dataRow1["NO_IV_PRE"].ToString() + "'"))
                                {
                                    dataRow2["TP_IV"] = this.cbo계정처리유형.SelectedValue.ToString();
                                }

                                dataRow1["TP_IV"] = this.cbo계정처리유형.SelectedValue.ToString();
                            }
                        }
                        #endregion
                    }

                    this._flexH.Redraw = true;
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("변경") });
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.ctx작성자.Name)
                    this._부서 = e.HelpReturn.Rows[0]["CD_DEPT"].ToString();
                else if (name == this.dtp매출일자.Name)
                    this._flexH[this._flexH.Row, "DT_RCP_RSV"] = this._commFun.DateAdd(this.dtp매출일자.Text, "D", Convert.ToInt32(this._flexH[this._flexH.Row, "DT_RCP_PREARRANGED"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo거래구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100" || 
                    D.GetString(this.cbo거래구분.SelectedValue) == "001")
                    this.dtp매출일자.Enabled = true;
                else
                    this.dtp매출일자.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region Grid 이벤트
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ctx사업장.Enabled = true;
                    this.cbo거래구분.Enabled = true;
                    this.rdo일괄.Enabled = true;
                    this.rdo건별.Enabled = true;
                    this.ToolBarSaveButtonEnabled = false;
                }
                else
                {
                    this.ctx사업장.Enabled = false;
                    this.cbo거래구분.Enabled = false;
                    this.rdo일괄.Enabled = false;
                    this.rdo건별.Enabled = false;
                    this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexH.IsBindingEnd || !this._flexH.HasNormalRow || !this._하단그리드필터여부)
                    return;

                this._flexL.RowFilter = "NO_IV = '" + this._flexH[this._flexH.Row, "NO_IV_PRE"].ToString() + "' ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                this._flexH["NO_BIZAREA"] = e.Result.DataTable.Rows[0]["NO_COMPANY"].ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                string str = grid.GetData(e.Row, e.Col).ToString();
                string editData = grid.EditData;

                if (str.ToUpper() == editData.ToUpper()) return;

                if (grid.Cols[e.Col].Name == "AM_K" && Convert.ToDecimal(this._flexH["RT_EXCH"]) != 1 && Math.Abs(this.원화계산(D.GetDecimal(this._flexH[e.Row, "AM_EX"]) * Convert.ToDecimal(this._flexH["RT_EXCH"])) - D.GetDecimal(editData)) > 10)
                {
                    this.ShowMessage("환율대비 원화조정금액은 10원이내입니다.");
                    if (this._flexH.Editor != null) this._flexH.Editor.Text = str;
                    this._flexH["AM_K"] = str;
                }
                else
                {
                    switch (this._flexH.Cols[e.Col].Name)
                    {
                        case "AM_K":
                            if (Convert.ToDecimal(this._flexH["RT_EXCH"]) == 1)
                                this._flexH[e.Row, "AM_EX"] = D.GetDecimal(editData);
                            this._flexH[e.Row, "AM_TOTAL"] = (D.GetDecimal(this._flexH[e.Row, "VAT_TAX"]) + D.GetDecimal(editData));
                            break;
                        case "VAT_TAX":
                            this._flexH[e.Row, "AM_TOTAL"] = (D.GetDecimal(this._flexH[e.Row, "AM_K"]) + D.GetDecimal(editData));
                            break;
                        case "DT_RCP_RSV":
                            if (editData.Trim().Length != 0)
                            {
                                if (editData.Trim().Length != 8)
                                {
                                    this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                                    if (this._flexH.Editor != null) this._flexH.Editor.Text = string.Empty;
                                    e.Cancel = true;
                                    break;
                                }
                                if (!this._flexH.IsDate(this._flexH.Cols[e.Col].Name))
                                {
                                    this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                                    if (this._flexH.Editor != null) this._flexH.Editor.Text = string.Empty;
                                    e.Cancel = true;
                                    break;
                                }
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (this._flexL.Cols[e.Col].Name)
                {
                    case "NM_MNGD1":
                        e.Parameter.P34_CD_MNG = "A21";
                        break;
                    case "NM_MNGD2":
                        e.Parameter.P34_CD_MNG = "A22";
                        break;
                    case "NM_MNGD3":
                        e.Parameter.P34_CD_MNG = "A25";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region 기타
        private void ColsSetting(string colName, string cdField, int startIdx, int endIdx)
        {
            for (int index = startIdx; index <= endIdx; ++index)
                this._flexL.Cols[colName + D.GetString(index)].Visible = false;

            DataTable code = this.GetComboDataCombine("N;" + cdField);
            for (int index = startIdx; index <= code.Rows.Count && index <= endIdx; ++index)
            {
                string @string = D.GetString(code.Rows[index - 1]["NAME"]);
                this._flexL.Cols[colName + D.GetString(index)].Caption = @string;
                this._flexL.Cols[colName + D.GetString(index)].Visible = true;
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

        public bool 자동매출등록(string 회사코드, string 출고번호, string 의뢰번호, string 출고일자, string 선적일자, bool isColor, string 발송방법, string 보내는사람, string 받는사람, string 참조, string 웹포탈메일, string 상업송장, string 포장명세서, string 수주확인서, string 고객발주서, string 선적일자확인제외, string 분납여부)
        {
			DataTable dt, dtH, dtL;
			Language lang = Language.KR;
			string query, 매출번호;
			string defaultPrint = string.Empty;

			try
            {
				#region 기초설정

				#region 프린터 설정 확인
				PrintDocument PrintDocument = new PrintDocument();
				defaultPrint = PrintDocument.PrinterSettings.PrinterName;

				string printerName = string.Empty;

				if (isColor)
					printerName = "INVOICE_COLOR";
				else
					printerName = "INVOICE_BW";

				if (!SetDefaultPrinter(printerName))
				{
					Global.MainFrame.ShowMessage("프린터 설정이 잘못되었습니다.\n프린터명 : " + printerName);
					return false;
				}
				#endregion

				#region 메일주소 확인
				if (발송방법 == "001" || 발송방법 == "003")
                {
                    if (string.IsNullOrEmpty(보내는사람))
					{
                        Global.MainFrame.ShowMessage("보내는사람이 지정되어 있지 않습니다.");
                        return false;
                    }

                    if (string.IsNullOrEmpty(받는사람))
                    {
                        Global.MainFrame.ShowMessage("받는사람이 지정되어 있지 않습니다.");
                        return false;
                    }
                }
                else if (발송방법 == "004")
				{
                    if (string.IsNullOrEmpty(웹포탈메일))
                    {
                        Global.MainFrame.ShowMessage("웹포탈 메일 수신자가 지정되어 있지 않습니다.");
                        return false;
                    }
                }
                #endregion

                if (string.IsNullOrEmpty(발송방법))
				{
                    Global.MainFrame.ShowMessage("발송방법이 지정되어 있지 않습니다.");
                    return false;
                }

                DateTime dt선적일자;

                if (!DateTime.TryParseExact(선적일자, "yyyyMMdd", null, DateTimeStyles.None, out dt선적일자))
				{
                    Global.MainFrame.ShowMessage("선적일자가 날짜형식에 맞지 않게 입력되어 있습니다.");
                    return false;
                }

                if (선적일자확인제외 != "Y")
				{
                    if (Global.MainFrame.GetDateTimeToday().Day >= 18)
                    {
                        if (dt선적일자 < Global.MainFrame.GetDateTimeFirstMonth)
                        {
                            Global.MainFrame.ShowMessage("선적일자가 이전달일 경우에 자동매출등록 진행 할 수 없습니다.\n(매월 18일 이후일 경우 해당 조건 적용)");
                            return false;
                        }
                    }
                    else
                    {
                        DateTime 전월1일 = DateTime.Parse(Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyy-MM") + "-01");

                        if (dt선적일자 < 전월1일)
                        {
                            Global.MainFrame.ShowMessage("선적일자 전월까지 자동매출등록 진행 할 수 있습니다.");
                            return false;
                        }
                    }
                }

				#endregion

				#region 첨부파일 다운로드
				this._dic인수증 = new Dictionary<string, List<string>>();
                this._dic첨부파일 = new Dictionary<string, string>();
                this._dic수주확인서 = new Dictionary<string, string>();
                this._dic고객발주서 = new Dictionary<string, string>();

                if (this.첨부파일다운로드(회사코드, 출고번호, 의뢰번호, 상업송장, 포장명세서, 수주확인서, 고객발주서, true) == false)
                    return false;
				#endregion

				#region 매출등록
				if (DBHelper.GetDataTable(string.Format(@"SELECT 1
                                                          FROM SA_IVL IL
                                                          WHERE CD_COMPANY = '{0}'
                                                          AND NO_IO = '{1}'", 회사코드, 출고번호)).Rows.Count > 0)
				{
                    Global.MainFrame.ShowMessage("이미 매출등록 되어 있습니다.");
                    return false;
                }

				query = string.Format("EXEC SP_CZ_SA_IV_AUTO '{0}', '{1}', '{2}', '{3}'", 회사코드, 출고번호, 출고일자, Global.MainFrame.LoginInfo.EmployeeNo);
				매출번호 = Global.MainFrame.ExecuteScalar(query).ToString();

				query = string.Format(@"UPDATE SA_IVH
                                        SET NM_USERDEF1 = 'Y'
                                        WHERE CD_COMPANY = '{0}'
                                        AND NO_IV = '{1}'", 회사코드, 매출번호);

				DBHelper.ExecuteScalar(query);
                #endregion

                string contents = string.Empty;

                #region 물류비검증시스템
                dt = DBHelper.GetDataTable("SP_CZ_SA_IV_LIV_WARNING", new object[] { 회사코드, 출고번호 });

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        contents = @"** 물류비 검증 시스템 알림

수주번호 : {0}
이윤 : {1}원
포장금액 : {2}원

이윤 대비 포장금액이 많이 발생 했습니다.

** 발생 원인 확인 후 회신바랍니다. (회신대상: 이정철JM, 부서장, 팀장)
※ 본 쪽지는 발신전용 입니다.";

                        contents = string.Format(contents, dr1["NO_SO"].ToString(), string.Format("{0:#,##0}", dr1["AM_PROFIT"]), string.Format("{0:#,##0}", dr1["AM_PACK"]));

                        Messenger.SendMSG(new string[] { dr1["NO_EMP"].ToString(), dr1["NO_EMP_LOG"].ToString() }, contents);

                        DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_SOH 
SET DT_USERDEF1 = CONVERT(CHAR(8), GETDATE(), 112) 
WHERE CD_COMPANY = '{0}' 
AND NO_SO = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, dr1["NO_SO"].ToString()));

                        DBHelper.ExecuteScalar(string.Format(@"INSERT INTO CZ_SA_AUTO_MSG_LOG
	(
		CD_COMPANY,
		TP_MSG,
		DC_EMP,
		DC_CONTENTS,
		DTS_INSERT
	)
	VALUES
	(
		'{0}',
		'CZ_SA_IV_AUTO',
		'{1}',
		'{2}',
		NEOE.SF_SYSDATE(GETDATE())
	)", Global.MainFrame.LoginInfo.CompanyCode, (dr1["NO_EMP"].ToString() + "|" + dr1["NO_EMP_LOG"].ToString()), contents));
                    }
                }
                #endregion

                #region 자동전표처리
                query = @"EXEC SP_CZ_SA_IVMNG_TRANSFER_DOCU @P_CD_COMPANY='{0}', @P_NO_IV='{1}'";

                Global.MainFrame.ExecuteScalar(string.Format(query, 회사코드, 매출번호));
                #endregion

                #region 전표승인
                query = string.Format(@"SELECT FD.NO_DOCU,
       FD.CD_PC
FROM SA_IVH IH WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				  FD.NO_DOCU,
				  FD.CD_PC
           FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, 
				    FD.NO_DOCU,
					FD.CD_PC) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
WHERE IH.CD_COMPANY = '{0}'
AND IH.NO_IV = '{1}'", 회사코드, 매출번호);

                dt = DBHelper.GetDataTable(query);

                string 전표번호 = dt.Rows[0]["NO_DOCU"].ToString();
                string 회계단위 = dt.Rows[0]["CD_PC"].ToString();

                object[] obj = new object[1];
                DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  회계단위,
                                                                                  "FI04",
                                                                                  선적일자 }, out obj);

                decimal 회계번호 = D.GetDecimal(obj[0]);

                DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { 전표번호,
                                                                                회계단위,
                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                선적일자,
                                                                                회계번호,
                                                                                "2",
                                                                                Global.MainFrame.LoginInfo.UserID,
                                                                                Global.MainFrame.LoginInfo.UserID });
                #endregion

                #region 인보이스 발송
                bool 발송여부 = false;

                if (분납여부 == "Y")
				{
                    #region 완납 후 인보이스 발송
                    dtH = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IVH_AUTO_RPT_S @P_CD_COMPANY='{0}', @P_NO_IV='{1}', @P_YN_PARTIAL='{2}'", 회사코드, 매출번호, 분납여부));
                    
                    if (dtH != null && dtH.Rows.Count > 0)
                    {
                        dtL = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IVL_AUTO_RPT_S @P_CD_COMPANY='{0}', @P_NO_IV='{1}', @P_YN_PARTIAL='{2}'", 회사코드, 매출번호, 분납여부));

                        this.인보이스생성(회사코드, 매출번호, dtH, dtL);

                        발송여부 = this.인보이스발송(회사코드, this.호선명, 보내는사람, 받는사람, 참조, 매출번호, 발송방법, 웹포탈메일, 선적일자, dtH, dtL);

                        if (발송여부 == false) return false;
                    }

                    dtH = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IVH_AUTO_PARTIAL_RPT_S @P_CD_COMPANY='{0}', @P_NO_IV='{1}'", 회사코드, 매출번호));

                    if (dtH != null && dtH.Rows.Count > 0)
					{
                        dtL = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IVL_AUTO_PARTIAL_RPT_S @P_CD_COMPANY='{0}', @P_NO_IV='{1}'", 회사코드, 매출번호));

                        foreach (DataRow dr in ComFunc.getGridGroupBy(dtL, new string[] { "NO_SO" }, true).Rows)
                        {
                            매출번호 = string.Empty;

                            foreach (DataRow dr1 in ComFunc.getGridGroupBy(dtL.Select(string.Format("NO_SO = '{0}'", dr["NO_SO"].ToString())).ToDataTable(), new string[] { "NO_IV2" }, true).Rows)
                            {
                                매출번호 += dr1["NO_IV2"].ToString() + ",";
                            }

                            매출번호 = 매출번호.Substring(0, 매출번호.Length - 1);

                            foreach (DataRow dr1 in dtL.Select(string.Format("NO_SO = '{0}'", dr["NO_SO"].ToString())))
                            {
                                dr1["NO_IV2"] = 매출번호;
                            }
                        }

                        this._dic인수증 = new Dictionary<string, List<string>>();
                        this._dic첨부파일 = new Dictionary<string, string>();
                        this._dic수주확인서 = new Dictionary<string, string>();
                        this._dic고객발주서 = new Dictionary<string, string>();

                        foreach (DataRow dr in ComFunc.getGridGroupBy(dtL, new string[] { "NO_IO", "NO_GIR" }, true).Rows)
                        {
                            if (this.첨부파일다운로드(회사코드, dr["NO_IO"].ToString(), dr["NO_GIR"].ToString(), 상업송장, 포장명세서, 수주확인서, 고객발주서, false) == false)
                                return false;
                        }

                        this.인보이스생성(회사코드, 매출번호, dtH, dtL);

                        발송여부 = this.인보이스발송(회사코드, this.호선명, 보내는사람, 받는사람, 참조, 매출번호, 발송방법, 웹포탈메일, 선적일자, dtH, dtL);

                        if (발송여부 == false) return false;
                    }

                    return true;
                    #endregion
                }
                else
				{
                    #region 인보이스 생성
                    dtH = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IVH_AUTO_RPT_S @P_CD_COMPANY='{0}', @P_NO_IV='{1}', @P_YN_PARTIAL='{2}'", 회사코드, 매출번호, 분납여부));
                    dtL = DBHelper.GetDataTable(string.Format("EXEC SP_CZ_SA_IVL_AUTO_RPT_S @P_CD_COMPANY='{0}', @P_NO_IV='{1}', @P_YN_PARTIAL='{2}'", 회사코드, 매출번호, 분납여부));

                    if ((dtH == null || dtH.Rows.Count == 0) ||
                        (dtL == null || dtL.Rows.Count == 0))
                    {
                        this.ShowMessage("매출데이터를 조회 하지 못했습니다.");
                        return false;
                    }

                    this.인보이스생성(회사코드, 매출번호, dtH, dtL);
                    #endregion

                    발송여부 = this.인보이스발송(회사코드, this.호선명, 보내는사람, 받는사람, 참조, 매출번호, 발송방법, 웹포탈메일, 선적일자, dtH, dtL);
                }
                #endregion

                return 발송여부;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(defaultPrint))
                    SetDefaultPrinter(defaultPrint);

                if (lang != Global.CurLanguage)
                    Global.CurLanguage = lang;
            }

            return false;
        }

        private bool 인보이스생성(string 회사코드, string 매출번호, DataTable dtH, DataTable dtL)
        {
            string query;

            try
            {
                foreach (DataRow dr in dtL.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["NM_ITEM_SOL"].ToString()))
                        dr["NM_ITEM_PARTNER"] = dr["NM_ITEM_SOL"].ToString();

                    if (!string.IsNullOrEmpty(dr["DT_IV"].ToString()))
                        dr["DT_LOADING"] = dr["DT_IV"].ToString();

                    dr["DT_TAX"] = Util.GetToDatePrint(dr["DT_TAX"]);
                    dr["DT_SO"] = Util.GetToDatePrint(dr["DT_SO"]);
                    dr["DT_LOADING"] = Util.GetToDatePrint(dr["DT_LOADING"]);
                }

                this.거래처코드 = string.Empty;
                this.매출처 = string.Empty;
                this.매출처주소 = string.Empty;
                this.매출처국가 = string.Empty;
                this.호선번호 = string.Empty;
                this.호선명 = string.Empty;
                this.영업조직명 = string.Empty;
                this.영업조직장 = string.Empty;
                this.서명 = string.Empty;
                this.창봉투매출처 = string.Empty;
                this.창봉투주소 = string.Empty;
                this.창봉투전화번호 = string.Empty;
                this.창봉투이메일 = string.Empty;
                this.창봉투비고 = string.Empty;
                this.국가코드 = string.Empty;
                this.IMO번호 = string.Empty;
                this.대표자명 = string.Empty;
                this.인보이스번호 = string.Empty;

                if (this.중복확인(dtH, "CD_PARTNER", "매출처", ref this.거래처코드) == false) return false;

                query = @"SELECT COUNT(1) 
                          FROM MA_PARTNER WITH(NOLOCK)" + Environment.NewLine +
                         "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND CD_PARTNER = '" + this.거래처코드 + "'" + Environment.NewLine +
                         "AND FG_PARTNER IN ('100', '300')"; //선주사, 선박관리사

                if (D.GetDecimal(Global.MainFrame.ExecuteScalar(query)) > 0)
                {
                    if (this.중복확인(dtL, "INVOICE_COMPANY", "매출처", ref this.매출처) == false) return false;

                    if (string.IsNullOrEmpty(this.매출처))
                    {
                        if (this.중복확인(dtH, "TO_COMPANY", "매출처", ref this.매출처) == false) return false;
                        if (this.중복확인(dtH, "TO_ADRESS", "매출처주소", ref this.매출처주소) == false) return false;

                        if (this.중복확인(dtH, "TO_COMPANY", "창봉투매출처", ref this.창봉투매출처) == false) return false;
                        if (this.중복확인(dtH, "TO_ADRESS", "창봉투주소", ref this.창봉투주소) == false) return false;
                        if (this.중복확인(dtH, "TO_TEL1", "창봉투전화번호", ref this.창봉투전화번호) == false) return false;
                        if (this.중복확인(dtH, "TO_EMAIL", "창봉투이메일", ref this.창봉투이메일) == false) return false;
                        if (this.중복확인(dtH, "TO_RMK", "창봉투비고", ref this.창봉투비고) == false) return false;

                        if (this.중복확인(dtH, "CD_NATION_INVOICE", "국가코드", ref this.국가코드) == false) return false;
                    }
                    else
                    {
                        if (this.중복확인(dtL, "INVOICE_ADDRESS", "매출처주소", ref this.매출처주소) == false) return false;

                        if (this.중복확인(dtL, "INVOICE_COMPANY", "창봉투매출처", ref this.창봉투매출처) == false) return false;
                        if (this.중복확인(dtL, "INVOICE_ADDRESS", "창봉투주소", ref this.창봉투주소) == false) return false;
                        if (this.중복확인(dtL, "INVOICE_TEL", "창봉투전화번호", ref this.창봉투전화번호) == false) return false;
                        if (this.중복확인(dtL, "INVOICE_EMAIL", "창봉투이메일", ref this.창봉투이메일) == false) return false;
                        if (this.중복확인(dtL, "INVOICE_RMK", "창봉투비고", ref this.창봉투비고) == false) return false;

                        if (this.중복확인(dtL, "CD_NATION_INVOICE", "국가코드", ref this.국가코드) == false) return false;
                    }
                }
                else
                {
                    if (this.중복확인(dtH, "TO_COMPANY", "매출처", ref this.매출처) == false) return false;
                    if (this.중복확인(dtH, "TO_ADRESS", "매출처주소", ref this.매출처주소) == false) return false;

                    if (this.중복확인(dtH, "TO_COMPANY", "창봉투매출처", ref this.창봉투매출처) == false) return false;
                    if (this.중복확인(dtH, "TO_ADRESS", "창봉투주소", ref this.창봉투주소) == false) return false;
                    if (this.중복확인(dtH, "TO_TEL1", "창봉투전화번호", ref this.창봉투전화번호) == false) return false;
                    if (this.중복확인(dtH, "TO_EMAIL", "창봉투이메일", ref this.창봉투이메일) == false) return false;
                    if (this.중복확인(dtH, "TO_RMK", "창봉투비고", ref this.창봉투비고) == false) return false;

                    if (this.중복확인(dtH, "CD_NATION_INVOICE", "국가코드", ref this.국가코드) == false) return false;
                }

                if (this.중복확인(dtH, "NM_NATION", "매출처국가", ref this.매출처국가) == false) return false;
                if (this.중복확인(dtH, "FROM_NAME", "대표자명", ref this.대표자명) == false) return false;

                DataRow[] dataRowArray = dtL.Select("ISNULL(NO_HULL, '') <> ''");
                if (dataRowArray.Length > 0)
                    this.호선번호 = D.GetString(dataRowArray[0]["NO_HULL"]);

                dataRowArray = dtL.Select("ISNULL(NM_VESSEL, '') <> ''");
                if (dataRowArray.Length > 0)
                    this.호선명 = D.GetString(dataRowArray[0]["NM_VESSEL"]);

                dataRowArray = dtL.Select("ISNULL(NO_IMO, '') <> ''");
                if (dataRowArray.Length > 0)
                    this.IMO번호 = D.GetString(dataRowArray[0]["NO_IMO"]);

                dataRowArray = dtL.Select("ISNULL(EN_SALEORG, '') <> ''");
                if (dataRowArray.Length > 0)
                    this.영업조직명 = D.GetString(dataRowArray[0]["EN_SALEORG"]);

                dataRowArray = dtL.Select("ISNULL(NM_ENG, '') <> ''");
                if (dataRowArray.Length > 0)
                    this.영업조직장 = D.GetString(dataRowArray[0]["NM_ENG"]);

                dataRowArray = dtL.Select("ISNULL(DC_SIGN, '') <> ''");
                if (dataRowArray.Length > 0)
                    this.서명 = D.GetString(dataRowArray[0]["DC_SIGN"]);

                this.서명 = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.서명;
                this.인보이스번호 = D.GetString(Global.MainFrame.GetSeq(회사코드, "CZ", "IV"));

                #region 인보이스 정보
                DBHelper.ExecuteScalar("SP_CZ_SA_IVMNG_INVOICE_I", new object[] { 회사코드,
                                                                                  this.인보이스번호,
                                                                                  매출번호,
                                                                                  this.거래처코드,
                                                                                  this.IMO번호,
                                                                                  this.창봉투매출처,
                                                                                  this.창봉투주소,
                                                                                  this.창봉투전화번호,
                                                                                  this.국가코드,
                                                                                  Global.MainFrame.LoginInfo.UserID });
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private bool 인보이스발송(string 회사코드, string 호선명, string 보내는사람, string 받는사람, string 참조, string 매출번호, string 발송방법, string 웹포탈메일, string 선적일자, DataTable dtH, DataTable dtL)
        {
            List<string> tmpList;
            ReportHelper reportHelper;
            DataTable dt1, dt2;
            string 파일명, query, filePath;

            try
            {
                string 제목, 본문, 숨은참조;
                List<string> fileList;

                if (회사코드 == "K100")
                    숨은참조 = "hmjo@dintec.co.kr";
                else
                    숨은참조 = "sumin.pi@dubheco.com";

                if (회사코드 == "K100")
                {
                    제목 = string.Format("[DINTEC CO.,LTD.] INVOICES FOR PAYMENT - {0}/INV NO.{1}", 호선명, 매출번호);
                    본문 = string.Format(@"Dear Sir / Madam

Good day to you.
We would like to express our sincere thanks for your kind cooperation to us.

Here we send the scaned original invoice to your e - mail account.
Please refer to the attached file and settle the invoice as per our terms of payment.

Order no. : {0}

We look forward to prompt payment from your good company.

Sincerely,
Dintec Account Department
Managing Director 
Tel: +82-51-664-1000, Fax: +82-51-462-7907", dtH.Rows[0]["NO_PO_PARTNER"].ToString());
                }
                else
                {
                    if (this.거래처코드 == "08420") //VALARIS
                        제목 = string.Format("[DUBHE CO.,LTD.-REP.KOREA-{2}] INVOICE FOR PAYMENT - {0}/INV NO.{1}", 호선명, 매출번호, dtH.Rows[0]["CD_EXCH_NAME"].ToString());
                    else if (this.거래처코드 == "08398" || this.거래처코드 == "13549") //RAFFLES TECHNICAL SERVICES PTE LTD, RAFFLES SHIPMANAGEMENT SERVICES PTE LTD
                        제목 = string.Format("{0} / {1}", dtH.Rows[0]["NM_PTR"].ToString(), dtH.Rows[0]["NO_PO_PARTNER"].ToString());
                    else
                        제목 = string.Format("[DUBHE CO.,LTD.] INVOICE FOR PAYMENT - {0}/INV NO.{1}", 호선명, 매출번호);

                    본문 = string.Format(@"To whom may it concern,

Thank you so much for your kind concern and support to us.
We are pleased to send you our invoices for your purchase orders as attached herewith and it would be highly appreciated should you kindly arrange for the payment soon.

Order no. : {0}

Thank you in advance for your kind cooperation.
Best regards,", dtH.Rows[0]["NO_PO_PARTNER"].ToString());
                }

                filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + 매출번호 + "\\";
                if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath);

                    foreach (string file in files)
                        File.Delete(file);
                }
                else
                    Directory.CreateDirectory(filePath);

                switch (발송방법)
                {
                    case "001":
                        #region Email (분할발송)
                        dt1 = ComFunc.getGridGroupBy(dtL, new string[] { "NO_IV2", "NO_PO_PARTNER", "NO_SO" }, true);

                        if ((회사코드 == "K100" && dt1.Select("NO_SO LIKE 'NB%' OR NO_SO LIKE 'NS%' OR NO_SO LIKE 'SB%'").Length > 0) ||
                            (회사코드 == "K200" && this.거래처코드 == "09942"))
                        {
                            foreach (DataRow dr in dt1.Rows)
                            {
                                if (this.거래처코드 == "17751")
                                {
                                    제목 = dr["NO_PO_PARTNER"].ToString(); 
                                    본문 = string.Format(@"Dear Sir / Madam

Good day to you.
We would like to express our sincere thanks for your kind cooperation to us.

Here we send the scaned original invoice to your e - mail account.
Please refer to the attached file and settle the invoice as per our terms of payment.

Vessel : {0}
Invoice no. : {1}

We look forward to prompt payment from your good company.

Sincerely,
Dintec Account Department
Managing Director 
Tel: +82-51-664-1000, Fax: +82-51-462-7907", 호선명, dr["NO_IV2"].ToString());
                                }
                                else
                                {
                                    if (회사코드 == "K100")
                                        제목 = string.Format("[DINTEC CO.,LTD.] INVOICES FOR PAYMENT - {0}/INV NO.{1}", 호선명, dr["NO_IV2"].ToString());
                                    else
                                        제목 = string.Format("[DUBHE CO.,LTD.] INVOICES FOR PAYMENT - {0}/INV NO.{1}", 호선명, dr["NO_IV2"].ToString());

                                    본문 = string.Format(@"Dear Sir / Madam

Good day to you.
We would like to express our sincere thanks for your kind cooperation to us.

Here we send the scaned original invoice to your e - mail account.
Please refer to the attached file and settle the invoice as per our terms of payment.

Order no. : {0}

We look forward to prompt payment from your good company.

Sincerely,
Dintec Account Department
Managing Director 
Tel: +82-51-664-1000, Fax: +82-51-462-7907", dr["NO_PO_PARTNER"].ToString());
                                }

                                dt2 = new DataView(dtL, "NO_IV2 = '" + dr["NO_IV2"].ToString() + "'", "SEQ_ORDER", DataViewRowState.CurrentRows).ToTable();

                                if (this.거래처코드 == "17751") //CAPITAL GAS SHIP MANAGEMENT CORP
                                    파일명 = this.GetUniqueFileName(filePath + dr["NO_IV2"].ToString() + ".pdf");
                                else if (this.거래처코드 == "09942") //EXCELERATE ENERGY L.P.
                                    파일명 = this.GetUniqueFileName(filePath + "Excelerate Energy " + dr["NO_IV2"].ToString() + ".pdf");
                                else
                                {
                                    파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                    파일명 = this.GetUniqueFileName(filePath + 파일명 + ".pdf");
                                }

                                reportHelper = this.리포트파일생성(dtH, dt2);

                                if (회사코드 == "K100")
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());
                                else
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());

                                tmpList = new List<string>();
                                tmpList.Add(filePath + dr["NO_IV2"].ToString() + "_TMP.pdf");

                                if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.AddRange(this._dic인수증[dr["NO_SO"].ToString()]);

                                if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic수주확인서[dr["NO_SO"].ToString()]);

                                if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic고객발주서[dr["NO_SO"].ToString()]);

                                foreach (string 첨부파일 in this._dic첨부파일.Values)
                                {
                                    tmpList.Add(첨부파일);
                                }

                                PDF.Merge(filePath + 파일명, tmpList.ToArray());

                                Thread.Sleep(5000); //5초

                                if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, 본문, string.Empty, new List<string> { filePath + 파일명 }))
                                {
                                    this.ShowMessage("메일발송실패");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            fileList = new List<string>();

                            foreach (DataRow dr in dt1.Rows)
                            {
                                dt2 = new DataView(dtL, "NO_IV2 = '" + dr["NO_IV2"].ToString() + "'", "SEQ_ORDER", DataViewRowState.CurrentRows).ToTable();

                                if (Global.MainFrame.LoginInfo.CompanyCode == "K200" && 매출처 == "09942")
                                    파일명 = "Dubhe Co., Ltd_" + dr["NO_IV2"].ToString() + "_" + Regex.Replace(dr["NM_VESSEL"].ToString(), "[\\/:*?\"<>|]", string.Empty) + "_" + Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                else
                                    파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);

                                파일명 = this.GetUniqueFileName(filePath + 파일명 + ".pdf");

                                reportHelper = this.리포트파일생성(dtH, dt2);

                                if (회사코드 == "K100")
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());
                                else
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());

                                tmpList = new List<string>();
                                tmpList.Add(filePath + dr["NO_IV2"].ToString() + "_TMP.pdf");

                                if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.AddRange(this._dic인수증[dr["NO_SO"].ToString()]);

                                if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic수주확인서[dr["NO_SO"].ToString()]);

                                if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic고객발주서[dr["NO_SO"].ToString()]);

                                foreach (string 첨부파일 in this._dic첨부파일.Values)
                                {
                                    tmpList.Add(첨부파일);
                                }

                                PDF.Merge(filePath + 파일명, tmpList.ToArray());

                                fileList.Add(filePath + 파일명);
                            }

                            Thread.Sleep(5000); //5초

                            if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, 본문, string.Empty, fileList))
                            {
                                this.ShowMessage("메일발송실패");
                                return false;
                            }
                        }
                        #endregion
                        break;
                    case "002":
                        #region 우편발송
                        reportHelper = this.리포트파일생성(dtH, dtL);

                        if (string.IsNullOrEmpty(창봉투비고))
                        {
                            if (회사코드 == "K100")
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV.DRF", false, true, filePath + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());
                            else
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV.DRF", false, true, filePath + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());

                            PDF.RemovePage(filePath + 매출번호 + "_TMP.pdf", filePath + 매출번호 + "_TMP1.pdf", 1);

                            this.printPDFWithAcrobat(filePath + 매출번호 + "_TMP1.pdf");
                        }
                        else
                        {
                            if (회사코드 == "K100")
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV.DRF", false, false);
                            else
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV.DRF", false, false);
                        }

                        foreach (List<string> 파일리스트 in this._dic인수증.Values)
						{
                            foreach (string 파일 in 파일리스트)
							{
                                this.printPDFWithAcrobat(파일);
                            }
						}
                        
                        foreach (string 파일 in this._dic수주확인서.Values)
                            this.printPDFWithAcrobat(파일);

                        foreach (string 파일 in this._dic고객발주서.Values)
                            this.printPDFWithAcrobat(파일);

                        foreach (string 파일 in this._dic첨부파일.Values)
                            this.printPDFWithAcrobat(파일);
                        #endregion
                        break;
                    case "003":
                        #region Email + 우편발송

                        #region Email
                        dt1 = ComFunc.getGridGroupBy(dtL, new string[] { "NO_IV2", "NO_PO_PARTNER", "NO_SO" }, true);

                        if (회사코드 == "K100" && dt1.Select("NO_SO LIKE 'NB%' OR NO_SO LIKE 'NS%' OR NO_SO LIKE 'SB%'").Length > 0)
                        {
                            foreach (DataRow dr in dt1.Rows)
                            {
                                제목 = string.Format("[DINTEC CO.,LTD.] INVOICES FOR PAYMENT - {0}/INV NO.{1}", 호선명, dr["NO_IV2"].ToString());
                                본문 = string.Format(@"Dear Sir / Madam

Good day to you.
We would like to express our sincere thanks for your kind cooperation to us.

Here we send the scaned original invoice to your e - mail account.
Please refer to the attached file and settle the invoice as per our terms of payment.

Order no. : {0}

We look forward to prompt payment from your good company.

Sincerely,
Dintec Account Department
Managing Director 
Tel: +82-51-664-1000, Fax: +82-51-462-7907", dr["NO_PO_PARTNER"].ToString());

                                dt2 = new DataView(dtL, "NO_IV2 = '" + dr["NO_IV2"].ToString() + "'", "SEQ_ORDER", DataViewRowState.CurrentRows).ToTable();

                                파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                파일명 = this.GetUniqueFileName(filePath + 파일명 + ".pdf");

                                reportHelper = this.리포트파일생성(dtH, dt2);

                                if (회사코드 == "K100")
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());
                                else
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());

                                tmpList = new List<string>();
                                tmpList.Add(filePath + dr["NO_IV2"].ToString() + "_TMP.pdf");

                                if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.AddRange(this._dic인수증[dr["NO_SO"].ToString()]);

                                if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic수주확인서[dr["NO_SO"].ToString()]);

                                if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic고객발주서[dr["NO_SO"].ToString()]);

                                foreach (string 첨부파일 in this._dic첨부파일.Values)
                                {
                                    tmpList.Add(첨부파일);
                                }

                                PDF.Merge(filePath + 파일명, tmpList.ToArray());

                                Thread.Sleep(5000); //5초

                                if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, 본문, string.Empty, new List<string> { filePath + 파일명 }))
                                {
                                    this.ShowMessage("메일발송실패");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            fileList = new List<string>();

                            foreach (DataRow dr in dt1.Rows)
                            {
                                dt2 = new DataView(dtL, "NO_IV2 = '" + dr["NO_IV2"].ToString() + "'", "SEQ_ORDER", DataViewRowState.CurrentRows).ToTable();

                                파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                파일명 = this.GetUniqueFileName(filePath + 파일명 + ".pdf");

                                reportHelper = this.리포트파일생성(dtH, dt2);

                                if (회사코드 == "K100")
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());
                                else
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());

                                tmpList = new List<string>();
                                tmpList.Add(filePath + dr["NO_IV2"].ToString() + "_TMP.pdf");

                                if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.AddRange(this._dic인수증[dr["NO_SO"].ToString()]);

                                if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic수주확인서[dr["NO_SO"].ToString()]);

                                if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic고객발주서[dr["NO_SO"].ToString()]);

                                foreach (string 첨부파일 in this._dic첨부파일.Values)
                                {
                                    tmpList.Add(첨부파일);
                                }

                                PDF.Merge(filePath + 파일명, tmpList.ToArray());

                                fileList.Add(filePath + 파일명);
                            }

                            Thread.Sleep(5000); //5초

                            if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, 본문, string.Empty, fileList))
                            {
                                this.ShowMessage("메일발송실패");
                                return false;
                            }
                        }
                        #endregion

                        #region 우편발송
                        reportHelper = this.리포트파일생성(dtH, dtL);

                        if (string.IsNullOrEmpty(창봉투비고))
                        {
                            if (회사코드 == "K100")
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV.DRF", false, true, filePath + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());
                            else
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV.DRF", false, true, filePath + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());

                            PDF.RemovePage(filePath + 매출번호 + "_TMP.pdf", filePath + 매출번호 + "_TMP1.pdf", 1);

                            this.printPDFWithAcrobat(filePath + 매출번호 + "_TMP1.pdf");
                        }
                        else
                        {
                            if (회사코드 == "K100")
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV.DRF", false, false);
                            else
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV.DRF", false, false);
                        }

                        foreach (List<string> 파일리스트 in this._dic인수증.Values)
						{
                            foreach (string 파일 in 파일리스트)
							{
                                this.printPDFWithAcrobat(파일);
                            }
						}
                        
                        foreach (string 파일 in this._dic수주확인서.Values)
                            this.printPDFWithAcrobat(파일);

                        foreach (string 파일 in this._dic고객발주서.Values)
                            this.printPDFWithAcrobat(파일);

                        foreach (string 파일 in this._dic첨부파일.Values)
                            this.printPDFWithAcrobat(파일);
                        #endregion

                        #endregion
                        break;
                    case "004":
                        #region Web Potal
                        제목 = "자동 매출등록 알림 - Web Potal 등록";

                        본문 = string.Format(@"- 매출처 : {0}
- 파일번호 : {1}
- INV NO. : {2}", 매출처, dtH.Rows[0]["NO_SO"].ToString(), 매출번호);

                        보내는사람 = "admin@dintec.co.kr";
                        받는사람 = 웹포탈메일;


                        if (회사코드 == "K100" && this.거래처코드 == "07300")
                            참조 = "dintec.sales1@dintec.co.kr";
                        else
                            참조 = string.Empty;

                        dt1 = ComFunc.getGridGroupBy(dtL, new string[] { "NO_IV2", "NO_PO_PARTNER", "NO_SO" }, true);
                        fileList = new List<string>();

                        foreach (DataRow dr in dt1.Rows)
                        {
                            dt2 = new DataView(dtL, "NO_IV2 = '" + dr["NO_IV2"].ToString() + "'", "SEQ_ORDER", DataViewRowState.CurrentRows).ToTable();

                            reportHelper = this.리포트파일생성(dtH, dt2);

                            if (this.거래처코드 == "01587")
                            {
                                #region MIDEAST SHIP MANAGEMENT LTD
                                파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                파일명 = this.GetUniqueFileName(filePath + 파일명 + "_INV.pdf");

                                if (회사코드 == "K100")
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_AUT.DRF", false, true, filePath + 파일명, new Dictionary<string, string>());
                                else
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_AUT.DRF", false, true, filePath + 파일명, new Dictionary<string, string>());

                                fileList.Add(filePath + 파일명);

                                파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                파일명 = this.GetUniqueFileName(filePath + 파일명 + "_ETC.pdf");

                                tmpList = new List<string>();

                                if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.AddRange(this._dic인수증[dr["NO_SO"].ToString()]);

                                if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic수주확인서[dr["NO_SO"].ToString()]);

                                if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic고객발주서[dr["NO_SO"].ToString()]);

                                foreach (string 첨부파일 in this._dic첨부파일.Values)
                                {
                                    tmpList.Add(첨부파일);
                                }

                                PDF.Merge(filePath + 파일명, tmpList.ToArray());

                                fileList.Add(filePath + 파일명);
                                #endregion
                            }
                            else
                            {
                                #region ETC
                                파일명 = Regex.Replace(dr["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
                                파일명 = this.GetUniqueFileName(filePath + 파일명 + ".pdf");

                                if (회사코드 == "K100")
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());
                                else
                                    reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_AUT.DRF", false, true, filePath + dr["NO_IV2"].ToString() + "_TMP.pdf", new Dictionary<string, string>());

                                tmpList = new List<string>();
                                tmpList.Add(filePath + dr["NO_IV2"].ToString() + "_TMP.pdf");

                                if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.AddRange(this._dic인수증[dr["NO_SO"].ToString()]);

                                if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic수주확인서[dr["NO_SO"].ToString()]);

                                if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                                    tmpList.Add(this._dic고객발주서[dr["NO_SO"].ToString()]);

                                foreach (string 첨부파일 in this._dic첨부파일.Values)
                                {
                                    tmpList.Add(첨부파일);
                                }

                                PDF.Merge(filePath + 파일명, tmpList.ToArray());

                                fileList.Add(filePath + 파일명);
                                #endregion
                            }
                        }

                        if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, 본문, string.Empty, fileList))
                        {
                            this.ShowMessage("메일발송실패");
                            return false;
                        }

                        query = @"SELECT ISNULL(CD_SYSDEF, '') AS CD_RPA
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_RPA0001'
AND CD_FLAG1 = 'INV'
AND YN_USE = 'Y'
AND CD_FLAG3 = '{0}'
AND CD_FLAG5 = '{1}'";

                        dt2 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.거래처코드));

                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt1.Rows)
                            {
                                RPA rpa = new RPA() { Process = "INV", FileNumber = dr["NO_IV2"].ToString() + "_" + dr["NO_SO"].ToString(), PartnerCode = this.거래처코드 };
                                rpa.AddQueue();
                            }

                            foreach (string file in fileList)
                            {
                                FileInfo fileInfo = new FileInfo(file);

                                string 업로드위치 = "Upload/P_CZ_SA_IV_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 선적일자.Substring(0, 4);
                                FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, 매출번호);
                                this._biz.SaveFileInfo(Global.MainFrame.LoginInfo.CompanyCode, 매출번호, fileInfo, 업로드위치, "P_CZ_SA_IV_MNG");
                            }
                        }
                        #endregion
                        break;
                    case "005":
                        #region Email
                        reportHelper = this.리포트파일생성(dtH, dtL);

                        if (회사코드 == "K100")
                            reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV.DRF", false, true, filePath + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());
                        else
                            reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV.DRF", false, true, filePath + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());

                        PDF.RemovePage(filePath + 매출번호 + "_TMP.pdf", filePath + 매출번호 + "_TMP1.pdf", 1);

                        tmpList = new List<string>();
                        fileList = new List<string>();

                        tmpList.Add(filePath + 매출번호 + "_TMP1.pdf");

                        foreach (List<string> 파일리스트 in this._dic인수증.Values)
						{
                            foreach (string 파일 in 파일리스트)
							{
                                tmpList.Add(파일);
                            }
                        }
                        
                        foreach (string 파일 in this._dic수주확인서.Values)
                            tmpList.Add(파일);

                        foreach (string 파일 in this._dic고객발주서.Values)
                            tmpList.Add(파일);

                        foreach (string 파일 in this._dic첨부파일.Values)
                            tmpList.Add(파일);

                        PDF.Merge(filePath + 매출번호 + ".pdf", tmpList.ToArray());
                        fileList.Add(filePath + 매출번호 + ".pdf");

                        Thread.Sleep(5000); //5초

                        if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, 본문, string.Empty, fileList))
                        {
                            this.ShowMessage("메일발송실패");
                            return false;
                        }
                        #endregion
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private bool 첨부파일다운로드(string 회사코드, string 출고번호, string 의뢰번호, string 상업송장, string 포장명세서, string 수주확인서, string 고객발주서, bool 중복오류여부)
        {
            DataTable dt, dt1, dtH, dtL;
            DataRow tmpRow;
            ReportHelper reportHelper;
            List<string> tmpList;
            string filePath, query, 파일명;

            try
            {
                filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + 출고번호 + "\\";
                if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath);

                    foreach (string file in files)
                        File.Delete(file);
                }
                else
                    Directory.CreateDirectory(filePath);

                #region 인수증 확인
                query = @"SELECT DISTINCT MF.FILE_NAME,
	            '/Upload/P_CZ_SA_GIM_REG/' + OH.CD_COMPANY + '/' + LEFT(GH.DT_GIR, 4) AS FILE_PATH,
                OL.NO_GIR + '_' + OH.CD_COMPANY AS CD_FILE
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
		       MAX(OL.NO_ISURCV) AS NO_GIR
	    FROM MM_QTIO OL WITH(NOLOCK)
	    GROUP BY OL.CD_COMPANY, OL.NO_IO) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
LEFT JOIN SA_GIRH GH WITH(NOLOCK) ON GH.CD_COMPANY = OL.CD_COMPANY AND GH.NO_GIR = OL.NO_GIR
LEFT JOIN MA_FILEINFO MF WITH(NOLOCK) ON MF.CD_COMPANY = OL.CD_COMPANY AND MF.CD_MODULE = 'SA' AND MF.ID_MENU = 'P_CZ_SA_GIM_REG' AND MF.CD_FILE = OL.NO_GIR + '_' + OL.CD_COMPANY
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'
AND UPPER(MF.FILE_EXT) IN ('JPG', 'PNG', 'JPEG', 'PDF', 'TIF')";

                dt = DBHelper.GetDataTable(string.Format(query, 회사코드, 출고번호));

                foreach (DataRow dr in dt.Rows)
                {
                    FileUploader.DownloadFile(dr["FILE_NAME"].ToString(), filePath, dr["FILE_PATH"].ToString(), dr["CD_FILE"].ToString());

                    if (this._dic첨부파일.ContainsKey(dr["FILE_NAME"].ToString()))
                    {
                        if (중복오류여부 == true)
						{
                            Global.MainFrame.ShowMessage(string.Format("동일한 파일이 등록 되어 있습니다. [파일명 : {0}]", dr["FILE_NAME"].ToString()));
                            return false;
                        }
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(filePath + dr["FILE_NAME"].ToString());

                        if (fileInfo.Extension.ToUpper() != ".PDF")
                        {
                            PDF.ImageToPdf(fileInfo.FullName);
                            this._dic첨부파일.Add(fileInfo.Name.Replace(fileInfo.Extension, string.Empty) + ".pdf", filePath + fileInfo.Name.Replace(fileInfo.Extension, string.Empty) + ".pdf");
                        }
                        else
                            this._dic첨부파일.Add(dr["FILE_NAME"].ToString(), filePath + dr["FILE_NAME"].ToString());
                    }
                }

                query = @"SELECT * 
FROM (SELECT CD_COMPANY, 
             NO_ISURCV AS NO_GIR 
      FROM MM_QTIO WITH(NOLOCK)
      WHERE CD_COMPANY = '{0}'
      AND NO_IO = '{1}'
      GROUP BY CD_COMPANY, NO_ISURCV) OL
JOIN (SELECT CD_COMPANY, CD_FILE,
      	     MAX(FILE_NAME) AS NM_FILE,
      	     CONVERT(CHAR(8), MAX(DTS_INSERT), 112) AS DT_INSERT
      FROM MA_FILEINFO WITH(NOLOCK)
      WHERE CD_MODULE = 'SA'
      AND ID_MENU = 'P_CZ_SA_GIM_REG'
      GROUP BY CD_COMPANY, CD_FILE) MF
ON MF.CD_COMPANY = OL.CD_COMPANY AND MF.CD_FILE = OL.NO_GIR + '_' + OL.CD_COMPANY
WHERE MF.NM_FILE LIKE OL.NO_GIR + '_%_' + OL.CD_COMPANY + '%'";

                dt = DBHelper.GetDataTable(string.Format(query, 회사코드, 출고번호));

                if (dt != null && dt.Rows.Count > 0)
                {
                    query = @"SELECT OL.NO_SO,
	   (OL.NO_GIR + '_' + OL.NO_SO + '_' + OH.CD_COMPANY) AS NM_FILE
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
			 OL.NO_ISURCV AS NO_GIR,
			 OL.NO_PSO_MGMT AS NO_SO
	  FROM MM_QTIO OL WITH(NOLOCK)
	  GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.NO_ISURCV, OL.NO_PSO_MGMT) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'";

                    dt1 = DBHelper.GetDataTable(string.Format(query, 회사코드, 출고번호));
                    string errorMsg = string.Empty;

                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (this._dic첨부파일.ContainsKey(dr["NM_FILE"].ToString() + ".pdf"))
                        {
                            if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
							{
                                this._dic인수증[dr["NO_SO"].ToString()].Add(this._dic첨부파일[dr["NM_FILE"].ToString() + ".pdf"]);
                            }
                            else
							{
                                List<string> 파일리스트 = new List<string>();
                                파일리스트.Add(this._dic첨부파일[dr["NM_FILE"].ToString() + ".pdf"]);

                                this._dic인수증.Add(dr["NO_SO"].ToString(), 파일리스트);
                            }

                            this._dic첨부파일.Remove(dr["NM_FILE"].ToString() + ".pdf");
                        }
                        else if (this._dic첨부파일.ContainsKey(dr["NM_FILE"].ToString() + ".PDF"))
                        {
                            if (this._dic인수증.ContainsKey(dr["NO_SO"].ToString()))
                            {
                                this._dic인수증[dr["NO_SO"].ToString()].Add(this._dic첨부파일[dr["NM_FILE"].ToString() + ".PDF"]);
                            }
                            else
                            {
                                List<string> 파일리스트 = new List<string>();
                                파일리스트.Add(this._dic첨부파일[dr["NM_FILE"].ToString() + ".PDF"]);

                                this._dic인수증.Add(dr["NO_SO"].ToString(), 파일리스트);
                            }

                            this._dic첨부파일.Remove(dr["NM_FILE"].ToString() + ".PDF");
                        }
                        else
                        {
                            errorMsg = string.Format("인수증이 누락된 수주가 있습니다. [수주번호 : {0}]", dr["NO_SO"].ToString());
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        Global.MainFrame.ShowMessage(errorMsg);
                        return false;
                    }
                }
                #endregion

                #region 상업송장
                if (상업송장 == "Y")
                {
                    DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_PACK_MNG_GIR_S", new object[] { Global.MainFrame.ServerKey,
                                                                                               회사코드,
                                                                                               Global.MainFrame.LoginInfo.Language,
                                                                                               의뢰번호 });

                    dtH = ds.Tables[0];
                    dtL = ds.Tables[1].Clone();

                    dtL.Columns.Add("TP_ROW");

                    string 수주번호 = string.Empty;

                    foreach (DataRow dr1 in ds.Tables[1].Rows)
                    {
                        // 자품목 제외
                        if (D.GetString(dr1["TP_BOM"]) != "C")
                        {
                            if (수주번호 != D.GetString(dr1["NO_SO"]))
                            {
                                tmpRow = dtL.NewRow();

                                tmpRow["NO_GIR"] = dr1["NO_GIR"];
                                tmpRow["NO_SO"] = dr1["NO_SO"];
                                tmpRow["NO_PO_PARTNER"] = dr1["NO_PO_PARTNER"];
                                tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr1["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr1["NO_SO"]);

                                dtL.Rows.Add(tmpRow);

                                수주번호 = D.GetString(dr1["NO_SO"]);
                            }

                            dtL.ImportRow(dr1);
                            dtL.Rows[dtL.Rows.Count - 1]["TP_ROW"] = "I";
                        }
                    }

                    reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_2", "납품의뢰현황-상업송장", 회사코드, dtH, dtL);

                    reportHelper.SetDataTable(dtH, 1);
                    reportHelper.SetDataTable(dtL, 2);
                    reportHelper.PrintHelper.UseUserFontStyle();

                    if (회사코드 == "K100")
                        reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K100_CI.DRF", false, true, filePath + 의뢰번호 + "_CI.pdf", new Dictionary<string, string>());
                    else if (회사코드 == "K200")
                        reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K200_CI.DRF", false, true, filePath + 의뢰번호 + "_CI.pdf", new Dictionary<string, string>());
                    else if (회사코드 == "S100")
                        reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_S100_CI.DRF", false, true, filePath + 의뢰번호 + "_CI.pdf", new Dictionary<string, string>());

                    if (this._dic첨부파일.ContainsKey(의뢰번호 + "_CI.pdf"))
                    {
                        if (중복오류여부 == true)
						{
                            Global.MainFrame.ShowMessage(string.Format("동일한 파일이 등록 되어 있습니다. [파일명 : {0}]", 의뢰번호 + "_CI.pdf"));
                            return false;
                        }
                    }
                    else
                        this._dic첨부파일.Add(의뢰번호 + "_CI.pdf", filePath + 의뢰번호 + "_CI.pdf");
                }
                #endregion

                #region 포장명세서
                if (포장명세서 == "Y")
                {
                    dtH = new DataTable();
                    dtL = new DataTable();

                    this.포장명세서데이터(회사코드, 의뢰번호, ref dtH, ref dtL);

                    if (dtH.Rows.Count != 0)
                    {
                        reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_1", "납품의뢰현황-포장명세서", 회사코드, dtH, dtL);
                        reportHelper.SetDataTable(dtH, 1);
                        reportHelper.SetDataTable(dtL, 2);
                        reportHelper.PrintHelper.UseUserFontStyle();

                        if (회사코드 == "K100")
                            reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K100_PL.DRF", false, true, filePath + 의뢰번호 + "_PL.pdf", new Dictionary<string, string>());
                        else if (회사코드 == "K200")
                            reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K200_PL.DRF", false, true, filePath + 의뢰번호 + "_PL.pdf", new Dictionary<string, string>());
                        else if (회사코드 == "S100")
                            reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_S100_PL.DRF", false, true, filePath + 의뢰번호 + "_PL.pdf", new Dictionary<string, string>());

                        if (this._dic첨부파일.ContainsKey(의뢰번호 + "_PL.pdf"))
                        {
                            if (중복오류여부 == true)
							{
                                Global.MainFrame.ShowMessage(string.Format("동일한 파일이 등록 되어 있습니다. [파일명 : {0}]", 의뢰번호 + "_PL.pdf"));
                                return false;
                            }
                        }
                        else
                            this._dic첨부파일.Add(의뢰번호 + "_PL.pdf", filePath + 의뢰번호 + "_PL.pdf");
                    }
                    else
                    {
                        this.ShowMessage("포장명세서 데이터가 없습니다.");
                        return false;
                    }
                }
                #endregion

                #region 수주확인서
                if (수주확인서 == "Y")
                {
                    query = @";WITH A AS
(
    SELECT OH.CD_COMPANY,
    	   OL.NO_SO,
    	   WL.NM_FILE_REAL,
           ROW_NUMBER() OVER(PARTITION BY OL.NO_SO ORDER BY WL.DTS_INSERT DESC) AS IDX
    FROM MM_QTIOH OH WITH(NOLOCK)
    JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
    			 OL.NO_ISURCV AS NO_GIR,
    			 OL.NO_PSO_MGMT AS NO_SO
    	  FROM MM_QTIO OL WITH(NOLOCK)
    	  GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.NO_ISURCV, OL.NO_PSO_MGMT) OL
    ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
    JOIN CZ_MA_WORKFLOWL WL ON WL.CD_COMPANY = OH.CD_COMPANY AND WL.NO_KEY = OL.NO_SO
    WHERE OH.CD_COMPANY = '{0}'
    AND OH.NO_IO = '{1}'
    AND WL.TP_STEP = '09'
)
SELECT A.CD_COMPANY,
       A.NO_SO,
       A.NM_FILE_REAL
FROM A 
WHERE IDX = 1";

                    dt = DBHelper.GetDataTable(string.Format(query, 회사코드, 출고번호));

                    foreach (DataRow dr in dt.Rows)
                    {
                        파일명 = FileMgr.Download_WF(dr["CD_COMPANY"].ToString(), dr["NO_SO"].ToString(), dr["NM_FILE_REAL"].ToString(), filePath + dr["NM_FILE_REAL"].ToString(), false);

                        if (this._dic수주확인서.ContainsKey(dr["NO_SO"].ToString()))
                        {
                            if (중복오류여부 == true)
							{
                                Global.MainFrame.ShowMessage(string.Format("동일한 파일이 등록 되어 있습니다. [수주번호 : {0}]", dr["NO_SO"].ToString()));
                                return false;
                            }
                        }
                        else
                            this._dic수주확인서.Add(dr["NO_SO"].ToString(), filePath + 파일명);
                    }
                }
                #endregion

                #region 고객발주서
                if (고객발주서 == "Y")
                {
                    query = @"SELECT OH.CD_COMPANY,
	   OL.NO_SO,
	   WL.NM_FILE_REAL
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
			 OL.NO_ISURCV AS NO_GIR,
			 OL.NO_PSO_MGMT AS NO_SO
	  FROM MM_QTIO OL WITH(NOLOCK)
	  GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.NO_ISURCV, OL.NO_PSO_MGMT) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
JOIN CZ_MA_WORKFLOWL WL ON WL.CD_COMPANY = OH.CD_COMPANY AND WL.NO_KEY = OL.NO_SO
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'
AND WL.TP_STEP = '08'
AND WL.NM_FILE_REAL LIKE '%.PDF'";

                    dt = DBHelper.GetDataTable(string.Format(query, 회사코드, 출고번호));
                    dt1 = ComFunc.getGridGroupBy(dt, new string[] { "NO_SO" }, true);

                    foreach (DataRow dr in dt1.Rows)
                    {
                        tmpList = new List<string>();

                        int index = 0;
                        foreach (DataRow dr1 in dt.Select(string.Format("NO_SO = '{0}'", dr["NO_SO"].ToString())))
                        {
                            파일명 = FileMgr.Download_WF(dr1["CD_COMPANY"].ToString(), dr1["NO_SO"].ToString(), dr1["NM_FILE_REAL"].ToString(), filePath + dr["NO_SO"].ToString() + "_PO_" + index.ToString() + ".pdf", false);

                            tmpList.Add(filePath + 파일명);

                            index++;
                        }

                        PDF.Merge(filePath + dr["NO_SO"].ToString() + ".pdf", tmpList.ToArray());

                        if (this._dic고객발주서.ContainsKey(dr["NO_SO"].ToString()))
                        {
                            if (중복오류여부 == true)
							{
                                Global.MainFrame.ShowMessage(string.Format("동일한 파일이 등록 되어 있습니다. [수주번호 : {0}]", dr["NO_SO"].ToString()));
                                return false;
                            }
                        }
                        else
                            this._dic고객발주서.Add(dr["NO_SO"].ToString(), filePath + dr["NO_SO"].ToString() + ".pdf");
                    }
                }
                #endregion

                #region 은행증명서
                if (회사코드 == "K100" && this.IMO번호 == "7403366")
                {
                    string localPath = filePath + "DINTEC_ACCOUNT_DETAIL_HANA_BANK.pdf";
                    string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "DINTEC_ACCOUNT_DETAIL_HANA_BANK.pdf";

                    if (!File.Exists(localPath))
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(serverPath, localPath);
                    }

                    if (!this._dic첨부파일.ContainsKey("DINTEC_ACCOUNT_DETAIL_HANA_BANK.pdf"))
                    {
                        this._dic첨부파일.Add("DINTEC_ACCOUNT_DETAIL_HANA_BANK.pdf", localPath);
                    }
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private string GetUniqueFileName(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            string newName = file.Name.Replace(file.Extension, "");
            string path = DIR.GetPath(fileName);
            int index = 0;

            while (File.Exists(path + @"\" + newName + file.Extension))
            {
                index++;
                newName = file.Name.Replace(file.Extension, "") + "(" + index + ")";
            }

            return newName + file.Extension;
        }

        private ReportHelper 리포트파일생성(DataTable dtH, DataTable dtL)
        {
            try
            {
                ReportHelper reportHelper = Util.SetRPT("R_CZ_SA_IVMNG_1", "매출관리-INVOICE", Global.MainFrame.LoginInfo.CompanyCode, dtH, dtL);

                if ((dtH == null || dtH.Rows.Count == 0) ||
                    (dtL == null || dtL.Rows.Count == 0))
                {
                    this.ShowMessage("매출데이터에 오류가 발생했습니다.");
                    return null;
                }

                reportHelper.SetDataTable(dtH, 1);
                reportHelper.SetDataTable(dtL, 2);

                reportHelper.SetData("인보이스번호", this.인보이스번호);
                reportHelper.SetData("매출처명", this.매출처);
                reportHelper.SetData("매출처주소", this.매출처주소);
                reportHelper.SetData("매출처국가", this.매출처국가);
                reportHelper.SetData("호선번호", this.호선번호);
                reportHelper.SetData("호선명", this.호선명);
                reportHelper.SetData("영업조직명", this.영업조직명);
                reportHelper.SetData("영업조직장", this.영업조직장);
                reportHelper.SetData("서명", this.서명);
                reportHelper.SetData("창봉투매출처", this.창봉투매출처);
                reportHelper.SetData("창봉투주소", this.창봉투주소);
                reportHelper.SetData("창봉투전화번호", this.창봉투전화번호);
                reportHelper.SetData("창봉투이메일", this.창봉투이메일);
                reportHelper.SetData("창봉투비고", this.창봉투비고);
                reportHelper.SetData("스탬프표시", "ORIGINAL");
                reportHelper.SetData("로고표시", "딘텍");
                reportHelper.SetData("서명표시", "표시");
                reportHelper.SetData("대표자명", this.대표자명);

                reportHelper.PrintHelper.UseUserFontStyle();

                return reportHelper;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return null;
        }

        private bool 중복확인(DataTable sourceDt, string column, string columnName, ref string value)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = ComFunc.getGridGroupBy(sourceDt, new string[1] { column }, true).Select("ISNULL(" + column + ", '') <> ''");

                if (dataRowArray.Length == 0)
                {
                    value = string.Empty;
                    return true;
                }
                else if (dataRowArray.Length > 1)
                {
                    this.ShowMessage(공통메세지._의값이중복되었습니다, columnName);
                    return true;
                }
                else
                {
                    value = D.GetString(dataRowArray[0][0]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

        public bool 메일발송(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string 본문, string html, List<string> 첨부파일List)
        {
            MailMessage mailMessage;
            SmtpClient smtpClient;
            string[] tempText;
            string address, name, id, pw, domain, query;

            try
            {
                #region 기본설정
                mailMessage = new MailMessage();
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                #endregion

                #region 보내는사람
                tempText = 보내는사람.Split('|');

                if (tempText.Length == 1)
                {
                    address = tempText[0];
                    name = tempText[0];
                }
                else if (tempText.Length == 2)
                {
                    address = tempText[0];
                    name = tempText[1];
                }
                else
                    return false;

                tempText = address.Split('@');

                if (tempText.Length != 2) return false;

                id = tempText[0];
                domain = tempText[1];

                query = @"SELECT DM.DM_NAME,
								 DU.DU_USERID,
								 DU.DU_PWD
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID
						  WHERE DM.DM_NAME = '" + domain + "'" + Environment.NewLine +
                         "AND DU.DU_USERID = '" + id + "'";

                DBMgr dbMgr = new DBMgr(DBConn.Mail);
                dbMgr.Query = query;

                pw = dbMgr.GetDataTable().Rows[0]["DU_PWD"].ToString();
                #endregion

                #region 메일정보
                mailMessage.From = new MailAddress(address, name, Encoding.UTF8);

                foreach (string 받는사람1 in 받는사람.Split(';'))
                {
                    if (받는사람1.Trim() != "")
                        mailMessage.To.Add(new MailAddress(받는사람1.Replace(";", "")));
                }

                foreach (string 참조1 in 참조.Split(';'))
                {
                    if (참조1.Trim() != "")
                        mailMessage.CC.Add(new MailAddress(참조1.Replace(";", "")));
                }

                foreach (string 숨은참조1 in 숨은참조.Split(';'))
                {
                    if (숨은참조1.Trim() != "")
                        mailMessage.Bcc.Add(new MailAddress(숨은참조1.Replace(";", "")));
                }

                mailMessage.Subject = 제목;

                // 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
                string body = "";
                string bodyA = "";
                string bodyB = "";
                string bodyC = "";

                int index = 본문.IndexOf("<a href=");

                if (index > 0)
                {
                    bodyA = 본문.Substring(0, index);
                    bodyB = 본문.Substring(index, 본문.IndexOf("</a>") + 4 - index);
                    bodyC = 본문.Substring(본문.IndexOf("</a>") + 4);

                    body = ""
                        + bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
                        + bodyB
                        + bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
                }
                else
                {
                    body = 본문.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
                }

                mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>" + html;

                foreach(string 첨부파일 in 첨부파일List)
				{
                    mailMessage.Attachments.Add(new Attachment(첨부파일));
                }
                #endregion

                #region 메일보내기
                smtpClient = new SmtpClient("113.130.254.131", 587);
                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(address, pw);
                smtpClient.Send(mailMessage);
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        public void printPDFWithAcrobat(string path)
        {
            string Filepath = path;

            using (PrintDialog Dialog = new PrintDialog())
            {
                //Dialog.ShowDialog();

                ProcessStartInfo printProcessInfo = new ProcessStartInfo()
                {
                    Verb = "print",
                    CreateNoWindow = true,
                    FileName = Filepath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process printProcess = new Process();
                printProcess.StartInfo = printProcessInfo;
                printProcess.Start();

                printProcess.WaitForInputIdle();

                Thread.Sleep(3000);

                try
				{
                    if (false == printProcess.CloseMainWindow())
                    {
                        printProcess.Kill();
                    }
                }
                catch(Exception ex)
				{

				}
            }
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

        private void 포장명세서데이터(string 회사코드, string 협조전번호, ref DataTable dtH, ref DataTable dtL)
        {
            DataTable tmpDt;
            DataRow tmpRow;
            string 수주번호;

            try
            {
                DataSet ds = this._biz.포장데이터(new object[] { Global.MainFrame.ServerKey,
                                                                 회사코드,
                                                                 협조전번호 });

                tmpDt = ds.Tables[1].Clone();
                tmpDt.Columns.Add("TP_ROW");

                수주번호 = string.Empty;

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (수주번호 != D.GetString(dr["NO_FILE"]))
                    {
                        tmpRow = tmpDt.NewRow();

                        tmpRow["NO_GIR"] = dr["NO_GIR"];
                        tmpRow["NO_KEY"] = dr["NO_KEY"];
                        tmpRow["NO_FILE"] = dr["NO_FILE"];
                        tmpRow["NO_PO_PARTNER"] = dr["NO_PO_PARTNER"];
                        tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr["NO_FILE"]);

                        tmpDt.Rows.Add(tmpRow);

                        수주번호 = D.GetString(dr["NO_FILE"]);
                    }

                    tmpDt.ImportRow(dr);
                    tmpDt.Rows[tmpDt.Rows.Count - 1]["TP_ROW"] = "I";
                }

                dtH = ds.Tables[0];
                dtH.Columns.Add("CD_QR");

                foreach (DataRow dr in dtH.Rows)
                {
                    dr["CD_QR"] = dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString();
                }

                dtL = tmpDt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion
    }
}