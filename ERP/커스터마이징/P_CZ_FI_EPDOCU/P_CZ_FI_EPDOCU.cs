using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Windows.Forms;
using account;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.AutoExpense.WinApp.Controls.ImageControls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.FI;
using Duzon.ERPU.FI.전표;
using Duzon.ERPU.Grant;
using Duzon.Windows.Print;
using master;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU : PageBase
    {
        #region 전역변수
        private CommonFunction m_funcLib = null;
        private DataTable _dtBindingDataTable = null;
        private bool _CheckBntEnable = false;
        private Docu _docu = new Docu();
        private string _sTpBudgeAcct = "0";
        private string _sTpBudgetMng = "0";
        private bool _bRowIdSort = false;
        private IDictionary<string, Decimal> DicFlag = null;
        private P_CZ_FI_EPDOCU_BIZ _biz;
        private P_CZ_FI_EPDOCU_CHECK _chk;
        private DataTable _dt오류메세지;
        private bool _엑셀데이타;
        private ReportHelper RDF;

        private bool 수정가능여부;

        private bool Chk회계단위
        {
            get
            {
                return !Checker.IsEmpty(this.ctx회계단위, this.lbl회계단위.Text);
            }
        }

        private bool Chk결의부서
        {
            get
            {
                return !Checker.IsEmpty(this.bpc결의부서, this.lbl결의부서.Text);
            }
        }

        private bool Chk결의내역
        {
            get
            {
                return !Checker.IsEmpty(this.bpc결의내역, this.lbl결의내역.Text);
            }
        }

        private bool Chk결의사원
        {
            get
            {
                return !Checker.IsEmpty(this.ctx결의사원, this.lbl결의사원.Text);
            }
        }

        private bool Chk증빙일자
        {
            get
            {
                return Checker.IsValid(this.dtp증빙일자, true, this.lbl증빙일자.Text);
            }
        }
        #endregion

        #region 생성자 & 초기화
        public P_CZ_FI_EPDOCU()
        {
            StartUp.Certify(this);
            this.InitializeComponent();
            this.MainGrids = new FlexGrid[] { this._flexM, this._flexL, this._flexR };
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this._biz = new P_CZ_FI_EPDOCU_BIZ();
            this.m_funcLib = new CommonFunction();
            this._chk = new P_CZ_FI_EPDOCU_CHECK();
            this.전표환경설정();

            if (this.PageID == "P_CZ_FI_EPDOCU_MNG")
            {
                this.수정가능여부 = false;

                this.btn증빙자료가져오기.Visible = false;
                this.btn합계계정전표처리설정.Visible = false;

                this.btn엑셀양식다운로드.Visible = false;
                this.btn엑셀업로드.Visible = false;
            }
            else
            {
                this.수정가능여부 = true;

                this._CheckBntEnable = this._biz.CheckBntEnable();
                if (this._CheckBntEnable)
                {
                    this.btn증빙자료가져오기.Visible = false;
                    this.btn합계계정전표처리설정.Visible = false;
                }
            }

            this.DicFlag = new Dictionary<string, Decimal>();
            foreach (DataRow dataRow in this._biz.중국버전_부가세율().Rows)
            {
                this.DicFlag.Add(D.GetString(dataRow["CD_SYSDEF"]), D.GetDecimal(dataRow["CD_FLAG1"]));
            }
            
            this.InitGrid();
            this.InitOneGrid();
            this.InitEvent();
        }

        private void 전표환경설정()
        {
            DataTable dt = (DataTable)((ResultData)this.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_FI_ENVD_MULTI_SELECT",
                SpParamsSelect = new object[] { this.LoginInfo.CompanyCode,
                                                "YN_CCEQBUDGET|TP_BUDGETMNG|TP_CHKPC|TP_GWARED|TP_ACCDATE|YN_BACCTCHK|TP_DTWRITE|TP_NODOCU|TP_DDCLOSE|TP_ACCBASIC|YN_CCEQDEPT|YN_BGSTART|YN_DEPTEQBUDGET|TP_EPDOCU|YN_DOCU_CNL_USE|TP_BUDGEACCT|YN_DEPTEQACCT|TP_END_CARD|TP_END_PARTNER|TP_MONNUMBER_CRE|TP_MONNUMBER_CLO|YN_AUTOCDMN|TP_DEPOSIT|TP_AMT|" }
            })).DataValue;

            string str1 = this.환경설정필터(dt, "TP_BUDGETMNG");
            
            if (!str1.Equals(string.Empty))
                this._sTpBudgetMng = str1;
            
            string str2 = this.환경설정필터(dt, "TP_BUDGEACCT");
            
            if (str2.Equals(string.Empty))
                return;
            
            this._sTpBudgeAcct = str2;
        }

        private string 환경설정필터(DataTable dt, string 필터구분)
        {
            string str = string.Empty;
            DataRow[] dataRowArray = dt.Select("TP_ENV = '" + 필터구분 + "'");

            if (dataRowArray.Length > 0)
                str = dataRowArray[0]["CD_ENV"].ToString();
            
            return str;
        }

        private void InitGrid()
        {
            #region Main Grid
            this._flexM.BeginSetting(1, 1, false);

            this._flexM.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("CD_EPCODE", "결의코드", false);
            this._flexM.SetCol("DT_ACCT", "증빙일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("NM_EPCODE", "결의내역", 100, true);
            this._flexM.SetCol("TP_EPNOTE", "구분", false);
            this._flexM.Cols["TP_EPNOTE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.SetCol("NM_NOTE", "상세내역", 100, true);
            this._flexM.SetCol("CD_EVID", "증빙코드", false);
            this._flexM.SetCol("NM_EVID", "증빙유형", 100, true);
            this._flexM.SetCol("CD_BUDGET", "예산단위코드", false);

            if (this._biz.Get예산필수여부)
                this._flexM.SetCol("NM_BUDGET", "예산단위", false);
            else
                this._flexM.SetCol("NM_BUDGET", "예산단위", false);
            
            if (this._biz.Get예산필수여부)
            {
                this._flexM.SetCol("CD_BGACCT", "예산계정", false);
                this._flexM.SetCol("NM_BGACCT", "예산계정", false);
            }
            else
            {
                this._flexM.SetCol("CD_BGACCT", "예산계정", false);
                this._flexM.SetCol("NM_BGACCT", "예산계정", false);
            }
            
            this._flexM.SetCol("CD_BIZPLAN", "사업계획", false);
            
            if (this._biz.Get예산필수여부 && this._biz.Get사업계획필수여부)
                this._flexM.SetCol("NM_BIZPLAN", "사업계획", false);
            else
                this._flexM.SetCol("NM_BIZPLAN", "사업계획", false);

            this._flexM.SetCol("AM_TAXSTD", "공급가액", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_ADDTAX", "부가세", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_SUM", "합계", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("CD_PARTNER", "거래처코드", false);
            this._flexM.SetCol("LN_PARTNER", "거래처", 100);
            this._flexM.SetCol("NO_COMPANY", "사업자등록번호", false);
            this._flexM.Cols["NO_COMPANY"].EditMask = "0##-##-#####";
            this._flexM.Cols["NO_COMPANY"].Format = "0##-##-#####";
            this._flexM.Cols["NO_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.SetStringFormatCol("NO_COMPANY");
            this._flexM.SetNoMaskSaveCol("NO_COMPANY");
            this._flexM.SetCol("CD_BIZAREA", "사업장", false);
            this._flexM.SetCol("NM_BIZAREA", "부가세사업장", false);
            this._flexM.SetCol("NO_BIZAREA", "사업장사업자번호", false);
            this._flexM.Cols["NO_BIZAREA"].Format = "0##-##-#####";
            this._flexM.Cols["NO_BIZAREA"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.SetStringFormatCol("NO_BIZAREA");
            this._flexM.SetCol("CD_CC", "코스트센타", false);
            this._flexM.SetCol("NM_CC", "코스트센타", 100);
            this._flexM.SetCol("CD_DEPT", "부서", false);
            this._flexM.SetCol("NM_DEPT", "부서", false);
            this._flexM.SetCol("CD_PJT", "프로젝트", false);
            this._flexM.SetCol("NM_PJT", "프로젝트", 100);
            this._flexM.SetCol("CD_CARD", "신용카드", false);
            this._flexM.SetCol("NM_CARD", "신용카드", 100);
            this._flexM.SetCol("CD_DEPOSIT", "예적금코드", false);
            this._flexM.SetCol("NO_DEPOSIT", "예적금계좌", 100);
            this._flexM.SetCol("CD_BANK", "금융기관", false);
            this._flexM.SetCol("NM_BANK", "금융기관", false);
            this._flexM.SetCol("CD_EMPLOY", "사원", false);
            this._flexM.SetCol("NM_EMP", "사원", false);
            this._flexM.SetCol("IMAGE_PATH_CHECK", "증빙보기", false);
            this._flexM.SetCol("IMAGE_PATH_D", "문서주소", false);
            this._flexM.SetCol("CD_UMNG1", "사용자정의1", false);
            this._flexM.SetCol("NM_UMNG1", "사용자정의1", false);
            this._flexM.SetCol("CD_UMNG2", "사용자정의2", false);
            this._flexM.SetCol("NM_UMNG2", "사용자정의2", false);
            this._flexM.SetCol("CD_UMNG3", "사용자정의3", false);
            this._flexM.SetCol("NM_UMNG3", "사용자정의3", false);
            this._flexM.SetCol("CD_UMNG4", "사용자정의4", false);
            this._flexM.SetCol("NM_UMNG4", "사용자정의4", false);
            this._flexM.SetCol("CD_UMNG5", "사용자정의5", false);
            this._flexM.SetCol("NM_UMNG5", "사용자정의5", false);
            
            for (int index = 1; index <= 10; ++index)
            {
                this._flexM.SetCol("CD_MNG" + index.ToString(), "관리항목" + index.ToString(), false);
                this._flexM.SetCol("NM_MNG" + index.ToString(), "관리항목" + index.ToString(), false);
                this._flexM.SetCol("YN_CDMNG" + index.ToString(), "코드여부" + index.ToString(), false);
                this._flexM.SetCol("TP_MNGFORM" + index.ToString(), "형태" + index.ToString(), false);
                this._flexM.SetCol("ST_MNG" + index.ToString(), "필수여부" + index.ToString(), false);
                this._flexM.SetCol("CD_MNGD" + index.ToString(), "관리내역코드" + index.ToString(), false);
                this._flexM.SetCol("NM_MNGD" + index.ToString(), "관리내역명" + index.ToString(), false);
            }
            
            this._flexM.SetCol("CD_FUND", "자금과목", false);
            this._flexM.SetCol("NM_FUND", "자금과목", false);
            this._flexM.SetCol("DT_END", "자금예정일자", false);
            this._flexM.SetCol("CD_EXCH", "통화명", false);
            this._flexM.SetCol("NM_EXCH", "통화명", 60, false);
            this._flexM.SetCol("RT_EXCH", "환율", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexM.SetCol("AM_EX", "외화금액", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexM.SetCol("DT_START", "발행일자", false);
            this._flexM.SetCol("ST_MUTUAL", "불공제사유", false);
            this._flexM.SetCol("NM_MUTUAL", "불공제사유", false);
            this._flexM.SetCol("NO_CASH", "현금영수증번호", false);
            this._flexM.SetCol("TP_TAX", "세무구분", false);
            this._flexM.SetCol("NO_TO", "수출신고번호", false);
            this._flexM.SetCol("DT_SHIPPING", "선적일자", false);
            this._flexM.SetCol("CD_WDEPT", "결의부서", false);
            this._flexM.SetCol("NM_WDEPT", "결의부서", false);
            this._flexM.SetCol("ID_WRITE", "결의사원", false);
            this._flexM.SetCol("NM_WRITE", "결의사원", false);
            this._flexM.SetCol("MD_TAX1", "월일(세금계산서)", false);
            this._flexM.Cols["MD_TAX1"].Format = "##\\/##\\";
            this._flexM.Cols["MD_TAX1"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.SetStringFormatCol("MD_TAX1");
            this._flexM.SetCol("NM_ITEM1", "품목(세금계산서)", false);    
            this._flexM.SetCol("NM_SIZE1", "규격(세금계산서)", false);
            this._flexM.SetCol("QT_TAX1", "수량(세금계산서)", false);
            this._flexM.SetCol("AM_PRC1", "단가(세금계산서)", false);
            this._flexM.SetCol("AM_SUPPLY1", "공급가액(세금계산서)", false);
            this._flexM.SetCol("AM_TAX1", "세액(세금계산서)", false);
            this._flexM.SetCol("NM_NOTE1", "비고(세금계산서)", false);
            this._flexM.SetCol("TP_DOCU", "처리여부", 100, 0, false);
            this._flexM.SetCol("TP_RESULT", "결과", 100, 0, false);
            this._flexM.SetCol("NO_DOCU", "전표번호", 100, false);
            this._flexM.SetCol("NM_PC", "회계단위", false);
            this._flexM.SetCol("NM_MNG", "품목군", false);
            this._flexM.SetCol("NM_DUTY_RANK", "직위", false);
            this._flexM.SetCol("CD_STDACCT", "공급가액계정", false);
            this._flexM.SetCol("CD_TAXACCT", "부가세계정", false);
            this._flexM.SetCol("CD_SUMACCT", "합계계정", false);
            this._flexM.SetCol("NM_TP_EVIDENCE", "증빙", false);
            this._flexM.Cols["IMAGE_PATH_CHECK"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexM.SetCodeHelpCol("NM_EPCODE", "H_FI_EPCODE_HELP", ShowHelpEnum.Always, new string[] { "CD_EPCODE", "NM_EPCODE" }, new string[] { "CODE", "NAME" });
            this._flexM.SetCodeHelpCol("NM_EVID", "H_FI_HELP01", ShowHelpEnum.Always, new string[] { "CD_EVID", "NM_EVID" }, new string[] { "CODE", "NAME" });
            this._flexM.SetCodeHelpCol("NM_BGACCT", "NM_BUDGET", "NM_BIZPLAN");
            this._flexM.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_COMPANY" }, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_COMPANY" }, ResultMode.SlowMode);
            this._flexM.SetCodeHelpCol("LN_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_COMPANY" }, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_COMPANY" }, ResultMode.SlowMode);
            this._flexM.SetCodeHelpCol("NO_COMPANY", HelpID.P_FI_PARTNO_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_COMPANY" }, new string[] { "CD_PARTNER", "NM_PARTNER", "NO_COMPANY" }, ResultMode.SlowMode);
            this._flexM.SetCodeHelpCol("NM_BIZAREA", HelpID.P_MA_BIZAREA_SUB, ShowHelpEnum.Always, new string[] { "CD_BIZAREA", "NM_BIZAREA", "NO_BIZAREA" }, new string[] { "CD_BIZAREA", "NM_BIZAREA", "NO_BIZAREA" }, ResultMode.SlowMode);
            this._flexM.SetCodeHelpCol("NM_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_DEPT", HelpID.P_MA_DEPT_SUB, ShowHelpEnum.Always, new string[] { "CD_DEPT", "NM_DEPT" }, new string[] { "CD_DEPT", "NM_DEPT" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_PJT", HelpID.P_SA_PROJECT_SUB, ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("CD_EMPLOY", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "CD_EMPLOY", "NM_EMP" }, new string[] { "NO_EMP", "NM_KOR" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NO_DEPOSIT", HelpID.P_FI_DEPOSIT_SUB, ShowHelpEnum.Always, new string[] { "CD_DEPOSIT", "NO_DEPOSIT" }, new string[] { "CD_DEPOSIT", "NO_DEPOSIT" }, ResultMode.SlowMode);
            this._flexM.SetCodeHelpCol("NM_CARD", HelpID.P_FI_CARD_SUB, ShowHelpEnum.Always, new string[] { "CD_CARD", "NM_CARD" }, new string[] { "NO_CARD", "NM_CARD" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_BANK", HelpID.P_MA_BANK_SUB, ShowHelpEnum.Always, new string[] { "CD_BANK", "NM_BANK" }, new string[] { "CD_PARTNER", "LN_PARTNER" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_FUND", HelpID.P_FI_FUND_SUB, ShowHelpEnum.Always, new string[] { "CD_FUND", "NM_FUND" }, new string[] { "CD_FUND", "NM_FUND" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_UMNG1", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_UMNG1", "NM_UMNG1" }, new string[] { "CD_MNGD", "NM_MNGD" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_UMNG2", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_UMNG2", "NM_UMNG2" }, new string[] { "CD_MNGD", "NM_MNGD" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_UMNG3", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_UMNG3", "NM_UMNG3" }, new string[] { "CD_MNGD", "NM_MNGD" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_UMNG4", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_UMNG4", "NM_UMNG4" }, new string[] { "CD_MNGD", "NM_MNGD" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_UMNG5", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_UMNG5", "NM_UMNG5" }, new string[] { "CD_MNGD", "NM_MNGD" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_EXCH", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_EXCH", "NM_EXCH" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_MUTUAL", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "ST_MUTUAL", "NM_MUTUAL" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_WDEPT", HelpID.P_MA_DEPT_SUB, ShowHelpEnum.Always, new string[] { "CD_WDEPT", "NM_WDEPT" }, new string[] { "CD_DEPT", "NM_DEPT" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_WRITE", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "ID_WRITE", "NM_WRITE" }, new string[] { "NO_EMP", "NM_KOR" }, ResultMode.FastMode);
            this._flexM.SetCodeHelpCol("NM_TP_EVIDENCE", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "TP_EVIDENCE", "NM_TP_EVIDENCE" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.FastMode);
            this._flexM.ExtendLastCol = true;
            this._flexM.SetDummyColumn("S");
            this._flexM.SetDummyColumn("TP_RESULT");
            this._flexM.VerifyAutoDelete = new string[] { "CD_EPCODE" };
            this._flexM.VerifyNotNull = new string[] { "NM_EPCODE", "NM_EVID", "NM_WRITE" };
            this._flexM.CellNoteInfo.EnabledCellNote = true;
            this._flexM.CellNoteInfo.CategoryID = this.Name;
            this._flexM.CellNoteInfo.DisplayColumnForDefaultNote = "NM_NOTE";
            this._flexM.CheckPenInfo.EnabledCheckPen = true;
            this._flexM.SettingVersion = "1.0.2.30";

            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flexM.SelectionMode = SelectionModeEnum.ListBox;
            #endregion

            #region Left Grid
            this._flexL.BeginSetting(6, 1, this.수정가능여부);

            this._flexL.Rows.Fixed = 1;
            this._flexL.SetCol("NM_MNG", "관리항목명", 100, false);
            this._flexL.SetCol("ST_MNG", "필수", 40, false);
            this._flexL.Cols["ST_MNG"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexL.SetCol("CD_MNGD", "내역코드", 80);
            this._flexL.SetCol("NM_MNGD", "내역명", 80);
            this._flexL.SetCol("CD_MNG", "관리항목코드", 0);

            this._flexL.SettingVersion = "1.0.0.4";
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.None, SumPositionEnum.None);

            this._flexL.ExtendLastCol = true;
            this._flexL.NewRowEditable = false;
            this._flexL.EnterKeyAddRow = false;
            this._flexL.AllowDragging = AllowDraggingEnum.None;
            this._flexL.SelectionMode = SelectionModeEnum.Cell;
            this._flexL.HighLight = HighLightEnum.WithFocus;
            this._flexL.Styles.Alternate.BackColor = this._flexL.Styles.Normal.BackColor;
            this._flexL.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;
            this._flexL.SetCodeHelpCol("CD_MNGD");
            this._flexL.UserOwnerDrawCellTextFormat = true;
            this._flexL.UnBindingStart();
            this._flexL.UnBindingEnd();
            #endregion

            #region Right
            this._flexR.BeginSetting(6, 1, this.수정가능여부);

            this._flexR.Rows.Fixed = 1;
            this._flexR.SetCol("NM_MNG", "관리항목명", 100, false);
            this._flexR.SetCol("ST_MNG", "필수", 40, false);
            this._flexR.Cols["ST_MNG"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexR.SetCol("CD_MNGD", "내역코드", 80);
            this._flexR.SetCol("NM_MNGD", "내역명", 80);

            this._flexR.SettingVersion = "1.0.0.4";
            this._flexR.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.None, SumPositionEnum.None);
            
            this._flexR.ExtendLastCol = true;
            this._flexR.NewRowEditable = false;
            this._flexR.EnterKeyAddRow = false;
            this._flexR.AllowDragging = AllowDraggingEnum.None;
            this._flexR.SelectionMode = SelectionModeEnum.Cell;
            this._flexR.HighLight = HighLightEnum.WithFocus;
            this._flexR.Styles.Alternate.BackColor = this._flexR.Styles.Normal.BackColor;
            this._flexR.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;
            this._flexR.SetCodeHelpCol("CD_MNGD");
            this._flexR.UserOwnerDrawCellTextFormat = true;
            this._flexR.UnBindingStart();
            this._flexR.UnBindingEnd();
            #endregion
        }

        private void InitOneGrid()
        {
            if (this.수정가능여부 == true)
            {
                this.oneGrid.Visible = true;
                this.oneGrid1.Visible = false;
                this.oneGrid2.Visible = true;
                this.CornerBox.Visible = true;
            }
            else
            {
                this.oneGrid.Visible = true;
                this.oneGrid1.Visible = true;
                this.oneGrid2.Visible = false;
                this.CornerBox.Visible = false;
            }
        }

        private void InitEvent()
        {
            this.btn코스트센터일괄적용.Click += new EventHandler(this.btn관리항목일괄적용_Click);
            this.btn사원일괄적용.Click += new EventHandler(this.btn관리항목일괄적용_Click);
            this.btn프로젝트일괄적용.Click += new EventHandler(this.btn관리항목일괄적용_Click);
            this.btn신용카드일괄적용.Click += new EventHandler(this.btn관리항목일괄적용_Click);
            this.btn거래처일괄적용.Click += new EventHandler(this.btn관리항목일괄적용_Click);

            this.btn결의코드일괄적용.Click += new EventHandler(this.btn결의코드일괄적용_Click);
            this.btn증빙유형일괄적용.Click += new EventHandler(this.btn증빙유형일괄적용_Click);
            this.btn증빙자료가져오기.Click += new EventHandler(this.bnt증빙가져오기_Click);
            this.btn합계계정전표처리설정.Click += new EventHandler(this.btn합계계정전표처리설정_Click);
            this.btn엑셀양식다운로드.Click += new System.EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn전표처리.Click += new System.EventHandler(this.btn전표처리_Click);
            this.btn전표취소.Click += new System.EventHandler(this.btn전표취소_Click);
            this.btn전자세금계산서처리.Click += new System.EventHandler(this.btn전자세금계산서처리_Click);
            this.bpc결의내역.QueryBefore += new BpQueryHandler(this.bpc결의내역_QueryBefore);
            this.bpc결의부서.QueryAfter += new BpQueryHandler(this.bpc결의부서_QueryAfter);

            this._flexM.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexM.HelpClick += new EventHandler(this._flexM_HelpClick);
            this._flexM.CodeHelp += new CodeHelpEventHandler(this._flexM_CodeHelp);
            this._flexM.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexM_BeforeCodeHelp);
            this._flexM.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flexM_AfterCodeHelp);
            this._flexM.ValidateEdit += new ValidateEditEventHandler(this._flexM_ValidateEdit);
            this._flexM.DoubleClick += new EventHandler(this._flexM_DoubleClick);
            this._flexM.BeforeShowContextMenu += new EventHandler(this._flexM_BeforeShowContextMenu);
            this._flexM.StartEdit += new RowColEventHandler(this._flexM_StartEdit);
            this._flexM.AfterEdit += new RowColEventHandler(this._flexM_AfterEdit);
            this._flexM.Click += new EventHandler(this._flexM_Click);
            this._flexM.CellContentChanged += new CellContentEventHandler(this._flexM_CellContentChanged);
            this._flexL.HelpClick += new EventHandler(this._flexLR_HelpClick);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexLR_ValidateEdit);
            this._flexL.StartEdit += new RowColEventHandler(this._flexLR_StartEdit);
            this._flexL.CodeHelp += new CodeHelpEventHandler(this._flexLR_CodeHelp);
            this._flexL.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flexLR_OwnerDrawCell);
            this._flexR.HelpClick += new EventHandler(this._flexLR_HelpClick);
            this._flexR.ValidateEdit += new ValidateEditEventHandler(this._flexLR_ValidateEdit);
            this._flexR.StartEdit += new RowColEventHandler(this._flexLR_StartEdit);
            this._flexR.CodeHelp += new CodeHelpEventHandler(this._flexLR_CodeHelp);
            this._flexR.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flexLR_OwnerDrawCell);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.ctx사원.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            this.ctx사원.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

            this.ctx코스트센터.CodeValue = Global.MainFrame.LoginInfo.CostCenterCode;
            this.ctx코스트센터.CodeName = Global.MainFrame.LoginInfo.CostCenterName;

            this._flexL[1, 0] = 1;
            this._flexL[2, 0] = 2;
            this._flexL[3, 0] = 3;
            this._flexL[4, 0] = 4;
            this._flexL[5, 0] = 5;
            this._flexR[1, 0] = 6;
            this._flexR[2, 0] = 7;
            this._flexR[3, 0] = 8;
            this._flexR[4, 0] = 9;
            this._flexR[5, 0] = 10;
            this._flexM.Styles.Add("오류").BackColor = Color.FromArgb((int)byte.MaxValue, 192, 192);
            this._flexM.Styles.Add("성공").BackColor = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            this.dtp증빙일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp증빙일자.EndDateToString = Global.MainFrame.GetStringToday;
            DataTable code1 = FI.GetCode("FI_J000003", true);
            DataRow row1 = code1.NewRow();
            row1["CODE"] = "0";
            row1["NAME"] = "미처리";
            code1.Rows.Add(row1);
            DataTable code2 = FI.GetCode("FI_J000096", true);
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            DataRow row2 = dt.NewRow();
            row2["CODE"] = "1";
            row2["NAME"] = "지출";
            dt.Rows.Add(row2);
            DataRow row3 = dt.NewRow();
            row3["CODE"] = "2";
            row3["NAME"] = "수입";
            dt.Rows.Add(row3);
            new SetControl().SetCombobox(this.cbo처리구분, FI.GetCode("FI_C000002", true));
            this.cbo처리구분.SelectedValue = "0";
            this._flexM.SetDataMap("TP_DOCU", code1, "CODE", "NAME");
            this._flexM.SetDataMap("IMAGE_PATH_CHECK", this._biz.ImagesSetDataMap(), "CODE", "NAME");
            this._flexM.SetDataMap("TP_EPNOTE", dt, "CODE", "NAME");
            this._flexL.SetDataMap("ST_MNG", code2, "CODE", "NAME");
            this._flexR.SetDataMap("ST_MNG", code2, "CODE", "NAME");
            this._dt오류메세지 = new DataTable();
            this._dt오류메세지.Columns.Add("ROW", typeof(string));
            this._dt오류메세지.Columns.Add("MSG", typeof(string));
            
            if (Global.MainFrame.LoginInfo.StDocuApp == "2" || Global.MainFrame.LoginInfo.StDocuApp == "3")
            {
                this.chk승인전표.Checked = true;
                this.chk승인전표.Visible = true;
            }

            UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble(this.PageID, "DOCU", this.btn전표처리, true);
            ugrant.GrantButtonEnble(this.PageID, "DOCUCANCLE", this.btn전표취소, true);

            if (this.수정가능여부 == true)
            {
                this.BindingDataTalbe(this._biz.Search(new object[] { string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty }));

                this.SetToolBarButtonState(false, true, this.ToolBarDeleteButtonEnabled, this.ToolBarSaveButtonEnabled, this.ToolBarPrintButtonEnabled);
            }
            else
            {
                this.SetToolBarButtonState(this.ToolBarSearchButtonEnabled, false, this.ToolBarDeleteButtonEnabled, this.ToolBarSaveButtonEnabled, this.ToolBarPrintButtonEnabled);
            }
        }
        #endregion

        #region 메인버튼 이벤트
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in this._flexM.DataTable.Rows)
                {
                    if (dr.RowState == DataRowState.Added && !string.IsNullOrEmpty(D.GetString(dr["CD_UMNG1"])))
                    {
                        Global.MainFrame.ExecuteScalar("DELETE FROM FI_RECEPT" + Environment.NewLine +
                                                       "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                       "AND NO_RECEPT = '" + D.GetString(dr["CD_UMNG1"]) + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return base.OnToolBarExitButtonClicked(sender, e);
        }

        protected override void OnDataChanged(object sender, EventArgs e)
        {
            base.OnDataChanged(sender, e);

            if (this.수정가능여부 == true)
                this.SetToolBarButtonState(false, this.ToolBarAddButtonEnabled, this.ToolBarDeleteButtonEnabled, this.ToolBarSaveButtonEnabled, this.ToolBarPrintButtonEnabled);
            else
                this.SetToolBarButtonState(this.ToolBarSearchButtonEnabled, false, this.ToolBarDeleteButtonEnabled, this.ToolBarSaveButtonEnabled, this.ToolBarPrintButtonEnabled);
        }

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch() && (this.Chk회계단위 && this.Chk증빙일자);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   this.ctx회계단위.CodeValue,
                                                   this.bpc결의부서.QueryWhereIn_Pipe,
                                                   this.dtp증빙일자.StartDateToString,
                                                   this.dtp증빙일자.EndDateToString,
                                                   this.bpc결의내역.QueryWhereIn_Pipe,
                                                   this.ctx결의사원.CodeValue,
                                                   D.GetString(this.cbo처리구분.SelectedValue) };

                this._엑셀데이타 = false;
                this.BindingDataTalbe(this._biz.Search(objArray));
                
                if (!this._flexM.HasNormalRow)
                {
                    this.FillDetailData(-1);
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
                
                this._flexM.AcceptChanges();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd() || (!this.Chk회계단위 || !this.Chk결의부서 || !this.Chk결의사원))
                return false;

            if (this.bpc결의부서.Count == 1)
                return true;

            this.ShowMessage("결의서를 입력할경우에는 결의부서를 여러개 선택할수 없습니다. \n 하나의 결의부서만 선택하세요.");
            
            return false;
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                this._flexM.Rows.Add();
                this._flexM.Row = this._flexM.Rows.Count - 1;
                this._flexM["ROW_ID"] = this._biz.순번();
                this._flexM["CD_PC"] = this.ctx회계단위.CodeValue;
                this._flexM["CD_WDEPT"] = this.bpc결의부서.CodeValues[0];
                this._flexM["NM_WDEPT"] = this.bpc결의부서.CodeNames[0];
                this._flexM["ID_WRITE"] = this.ctx결의사원.CodeValue;
                this._flexM["NM_WRITE"] = this.ctx결의사원.CodeName;
                this._flexM["TP_DOCU"] = "0";

                DataTable dataTable = this._biz.결의사원_추가정보(this.ctx결의사원.CodeValue);
                if (dataTable != null && dataTable.Rows.Count > 0)
                    this._flexM["NM_DUTY_RANK"] = D.GetString(dataTable.Rows[0]["NM_SYSDEF"]);
                
                this._flexM.AddFinished();
                this._flexM.Col = this._flexM.Cols["DT_ACCT"].Index;
                this._flexM.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;

                this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool CheckDetail(int row)
        {
            bool flag1 = true;
            DataTable table = this._dt오류메세지.Clone();
            if (this._엑셀데이타)
            {
                string string1 = D.GetString(this._flexM[row, "CD_EPCODE"]);
                string string2 = D.GetString(this._flexM[row, "NM_EPCODE"]);
                if (!string.IsNullOrEmpty(string1) && string.IsNullOrEmpty(string2))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("결의내역 코드값이 존재하지 않습니다. 코드:" + string1);
                    table.Rows.Add(row1);
                }
                string string3 = D.GetString(this._flexM[row, "CD_BUDGET"]);
                string string4 = D.GetString(this._flexM[row, "NM_BUDGET"]);
                if (!string.IsNullOrEmpty(string3) && string.IsNullOrEmpty(string4))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("예산단위 코드값이 존재하지 않습니다. 코드:" + string3);
                    table.Rows.Add(row1);
                }
                string string5 = D.GetString(this._flexM[row, "CD_BIZPLAN"]);
                string string6 = D.GetString(this._flexM[row, "NM_BIZPLAN"]);
                if (!string.IsNullOrEmpty(string5) && string.IsNullOrEmpty(string6))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("사업계획 코드값이 존재하지 않습니다. 코드:" + string5);
                    table.Rows.Add(row1);
                }
                string string7 = D.GetString(this._flexM[row, "CD_PARTNER"]);
                string string8 = D.GetString(this._flexM[row, "LN_PARTNER"]);
                if (!string.IsNullOrEmpty(string7) && string.IsNullOrEmpty(string8))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("거래처 코드값이 존재하지 않습니다. 코드:" + string7);
                    table.Rows.Add(row1);
                }
                string string9 = D.GetString(this._flexM[row, "CD_BIZAREA"]);
                string string10 = D.GetString(this._flexM[row, "NM_BIZAREA"]);
                if (!string.IsNullOrEmpty(string9) && string.IsNullOrEmpty(string10))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("사업장 코드값이 존재하지 않습니다. 코드:" + string9);
                    table.Rows.Add(row1);
                }
                string string11 = D.GetString(this._flexM[row, "PARTNER_COMPANY"]);
                if (!string.Equals(D.GetString(this._flexM[row, "NO_COMPANY"]), string11))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("거래처코드값과 사업자등록번호가 일치하지 않습니다. 코드:" + string9);
                    table.Rows.Add(row1);
                }
                string string12 = D.GetString(this._flexM[row, "CD_CC"]);
                string string13 = D.GetString(this._flexM[row, "NM_CC"]);
                if (!string.IsNullOrEmpty(string12) && string.IsNullOrEmpty(string13))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("코스트센타 코드값이 존재하지 않습니다. 코드:" + string12);
                    table.Rows.Add(row1);
                }
                string string14 = D.GetString(this._flexM[row, "CD_DEPT"]);
                string string15 = D.GetString(this._flexM[row, "NM_DEPT"]);
                if (!string.IsNullOrEmpty(string14) && string.IsNullOrEmpty(string15))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("부서 코드값이 존재하지 않습니다. 코드:" + string14);
                    table.Rows.Add(row1);
                }
                string string16 = D.GetString(this._flexM[row, "CD_PJT"]);
                string string17 = D.GetString(this._flexM[row, "NM_PJT"]);
                if (!string.IsNullOrEmpty(string16) && string.IsNullOrEmpty(string17))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("프로젝트 코드값이 존재하지 않습니다. 코드:" + string16);
                    table.Rows.Add(row1);
                }
                string string18 = D.GetString(this._flexM[row, "CD_CARD"]);
                string string19 = D.GetString(this._flexM[row, "NM_CARD"]);
                if (!string.IsNullOrEmpty(string18) && string.IsNullOrEmpty(string19))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("신용카드 코드값이 존재하지 않습니다. 코드:" + string18);
                    table.Rows.Add(row1);
                }
                string string20 = D.GetString(this._flexM[row, "CD_DEPOSIT"]);
                string string21 = D.GetString(this._flexM[row, "NO_DEPOSIT"]);
                if (!string.IsNullOrEmpty(string20) && string.IsNullOrEmpty(string21))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("예적금계좌 코드값이 존재하지 않습니다. 코드:" + string20);
                    table.Rows.Add(row1);
                }
                string string22 = D.GetString(this._flexM[row, "CD_BANK"]);
                string string23 = D.GetString(this._flexM[row, "NM_BANK"]);
                if (!string.IsNullOrEmpty(string22) && string.IsNullOrEmpty(string23))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("금융기관 코드값이 존재하지 않습니다. 코드:" + string22);
                    table.Rows.Add(row1);
                }
                string string24 = D.GetString(this._flexM[row, "CD_EMPLOY"]);
                string string25 = D.GetString(this._flexM[row, "NM_EMP"]);
                if (!string.IsNullOrEmpty(string24) && string.IsNullOrEmpty(string25))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("사원 코드값이 존재하지 않습니다. 코드:" + string24);
                    table.Rows.Add(row1);
                }
                for (int index = 1; index <= 5; ++index)
                {
                    string string26 = D.GetString(this._flexM[row, "CD_UMNG" + index.ToString()]);
                    string string27 = D.GetString(this._flexM[row, "NM_UMNG" + index.ToString()]);
                    if (!string.IsNullOrEmpty(string26) && string.IsNullOrEmpty(string27))
                    {
                        DataRow row1 = table.NewRow();
                        row1["ROW"] = this._flexM[row, "ROW_ID"];
                        row1["MSG"] = ("사용자정의" + index.ToString() + " 코드값이 존재하지 않습니다. 코드:" + string26);
                        table.Rows.Add(row1);
                    }
                }
                for (int index = 1; index <= 10; ++index)
                {
                    string string26 = D.GetString(this._flexM[row, "CD_MNGD" + index.ToString()]);
                    string string27 = D.GetString(this._flexM[row, "NM_MNGD" + index.ToString()]);
                    if (!string.IsNullOrEmpty(string26) && string.IsNullOrEmpty(string27))
                    {
                        DataRow row1 = table.NewRow();
                        row1["ROW"] = this._flexM[row, "ROW_ID"];
                        row1["MSG"] = ("관리항목[" + D.GetString(this._flexM[row, "NM_MNG" + index.ToString()]) + "] 코드값이 존재하지 않습니다. 코드:" + string26);
                        table.Rows.Add(row1);
                    }
                    if (D.GetString(this._flexM[row, "ST_MNG" + index.ToString()]) == "Y")
                    {
                        string string28 = D.GetString(this._flexM[row, "YN_CDMNG" + index.ToString()]);
                        if ((string28 == "Y" && string.IsNullOrEmpty(string26) || string28 == "N" && string.IsNullOrEmpty(string27)) && !this.Get관리항목필수체크2(row, D.GetString(this._flexM[row, "CD_MNG" + index.ToString()])))
                        {
                            DataRow row1 = table.NewRow();
                            row1["ROW"] = this._flexM[row, "ROW_ID"];
                            row1["MSG"] = ("필수 관리항목[" + D.GetString(this._flexM[row, "NM_MNG" + index.ToString()]) + "] 이 누락되었습니다.");
                            table.Rows.Add(row1);
                        }
                    }
                }
                string string29 = D.GetString(this._flexM[row, "CD_FUND"]);
                string string30 = D.GetString(this._flexM[row, "NM_FUND"]);
                if (!string.IsNullOrEmpty(string29) && string.IsNullOrEmpty(string30))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("자금과목 코드값이 존재하지 않습니다. 코드:" + string29);
                    table.Rows.Add(row1);
                }
                string string31 = D.GetString(this._flexM[row, "CD_EXCH"]);
                string string32 = D.GetString(this._flexM[row, "NM_EXCH"]);
                if (!string.IsNullOrEmpty(string31) && string.IsNullOrEmpty(string32))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("환종 코드값이 존재하지 않습니다. 코드:" + string31);
                    table.Rows.Add(row1);
                }
                string string33 = D.GetString(this._flexM[row, "ST_MUTUAL"]);
                string string34 = D.GetString(this._flexM[row, "NM_MUTUAL"]);
                if (!string.IsNullOrEmpty(string33) && string.IsNullOrEmpty(string34))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("불공제사유 코드값이 존재하지 않습니다. 코드:" + string33);
                    table.Rows.Add(row1);
                }
            }
            else
            {
                for (int index = 1; index <= 10; ++index)
                {
                    string string1 = D.GetString(this._flexM[row, "CD_MNGD" + index.ToString()]);
                    string string2 = D.GetString(this._flexM[row, "NM_MNGD" + index.ToString()]);
                    if (!string.IsNullOrEmpty(string1) && string.IsNullOrEmpty(string2))
                    {
                        DataRow row1 = table.NewRow();
                        row1["ROW"] = this._flexM[row, "ROW_ID"];
                        row1["MSG"] = ("관리항목[" + D.GetString(this._flexM[row, "NM_MNG" + index.ToString()]) + "] 코드값이 존재하지 않습니다. 코드:" + string1);
                        table.Rows.Add(row1);
                    }
                    if (D.GetString(this._flexM[row, "ST_MNG" + index.ToString()]) == "Y")
                    {
                        string string3 = D.GetString(this._flexM[row, "YN_CDMNG" + index.ToString()]);
                        if ((string3 == "Y" && string.IsNullOrEmpty(string1) || string3 == "N" && string.IsNullOrEmpty(string2)) && D.GetString(this._flexM[row, "CD_MNG" + index.ToString()]) != "C15" && !this.Get관리항목필수체크2(row, D.GetString(this._flexM[row, "CD_MNG" + index.ToString()])))
                        {
                            DataRow row1 = table.NewRow();
                            row1["ROW"] = this._flexM[row, "ROW_ID"];
                            row1["MSG"] = ("필수 관리항목[" + D.GetString(this._flexM[row, "NM_MNG" + index.ToString()]) + "] 이 누락되었습니다.");
                            table.Rows.Add(row1);
                        }
                    }
                }
            }
            string string35 = D.GetString(this._flexM[row, "CD_CARD"]);
            string str = D.GetString(this._flexM[row, "TP_TAX"]);
            if (str == "")
                str = "00";
            if (string.IsNullOrEmpty(string35))
            {
                switch (str)
                {
                    case "24":
                    case "34":
                    case "35":
                    case "36":
                    case "39":
                    case "42":
                    case "29":
                    case "50":
                    case "52":
                        DataRow row2 = table.NewRow();
                        row2["ROW"] = this._flexM[row, "ROW_ID"];
                        row2["MSG"] = "신용카드번호가 누락되었습니다.";
                        table.Rows.Add(row2);
                        break;
                }
            }
            if (string.IsNullOrEmpty(D.GetString(this._flexM[row, "NO_CASH"])))
            {
                switch (str)
                {
                    case "31":
                    case "37":
                        DataRow row3 = table.NewRow();
                        row3["ROW"] = this._flexM[row, "ROW_ID"];
                        row3["MSG"] = "현금영수증번호가 누락되었습니다.";
                        table.Rows.Add(row3);
                        break;
                }
            }
            if (string.IsNullOrEmpty(D.GetString(this._flexM[row, "CD_EPCODE"])))
            {
                DataRow row1 = table.NewRow();
                row1["ROW"] = this._flexM[row, "ROW_ID"];
                row1["MSG"] = "결의내역 정보가 누락되었습니다.";
                table.Rows.Add(row1);
            }
            if (string.IsNullOrEmpty(D.GetString(this._flexM[row, "DT_ACCT"])))
            {
                DataRow row1 = table.NewRow();
                row1["ROW"] = this._flexM[row, "ROW_ID"];
                row1["MSG"] = "증빙일자 정보가 누락되었습니다.";
                table.Rows.Add(row1);
            }
            if (string.IsNullOrEmpty(D.GetString(this._flexM[row, "CD_EVID"])))
            {
                DataRow row1 = table.NewRow();
                row1["ROW"] = this._flexM[row, "ROW_ID"];
                row1["MSG"] = "증빙유형 정보가 누락되었습니다.";
                table.Rows.Add(row1);
            }
            if (D.GetDecimal(this._flexM[row, "AM_TAXSTD"]) == 0)
            {
                DataRow row1 = table.NewRow();
                row1["ROW"] = this._flexM[row, "ROW_ID"];
                row1["MSG"] = "공급가액 정보가 누락되었습니다.";
                table.Rows.Add(row1);
            }
            string string36 = D.GetString(this._flexM[row, "CD_BGACCT"]);
            string string37 = D.GetString(this._flexM[row, "CD_STDACCT"]);
            string string38 = D.GetString(this._flexM[row, "TP_BUNIT"]);
            if (!string.IsNullOrEmpty(string37) && !string.IsNullOrEmpty(string36))
            {
                bool flag2 = false;
                if (!this._biz.Get예산필수여부)
                {
                    if (string38 == "4")
                        flag2 = true;
                }
                else if (string38 == "2" || string38 == "3" || string38 == "4")
                    flag2 = true;
                if (flag2 && string.IsNullOrEmpty(D.GetString(this._flexM[row, "CD_BUDGET"])))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = "예산단위가 누락되었습니다.";
                    table.Rows.Add(row1);
                }
            }
            if (this._biz.Get예산필수여부 && this._biz.Get사업계획필수여부 && !string.IsNullOrEmpty(string36) && string.IsNullOrEmpty(D.GetString(this._flexM[row, "CD_BIZPLAN"])))
            {
                DataRow row1 = table.NewRow();
                row1["ROW"] = this._flexM[row, "ROW_ID"];
                row1["MSG"] = "사업계획이 누락되었습니다.";
                table.Rows.Add(row1);
            }
            switch (str)
            {
                case "14":
                case "15":
                case "16":
                case "17":
                case "19":
                case "1A":
                case "23":
                case "25":
                case "26":
                case "37":
                case "39":
                    if (D.GetDecimal(this._flexM[row, "AM_ADDTAX"]) != 0)
                    {
                        DataRow row1 = table.NewRow();
                        row1["ROW"] = this._flexM[row, "ROW_ID"];
                        row1["MSG"] = "부가세금액은 0이여야 합니다.";
                        table.Rows.Add(row1);
                        break;
                    }
                    break;
                case "11":
                case "12":
                case "13":
                case "18":
                case "1B":
                case "21":
                case "22":
                case "24":
                case "31":
                case "38":
                case "43":
                case "50":
                case "46":
                case "47":
                case "27":
                case "28":
                case "34":
                case "29":
                case "30":
                case "35":
                case "51":
                case "52":
                case "53":
                case "32":
                case "33":
                case "36":
                case "40":
                case "41":
                case "42":
                case "44":
                case "45":
                case "48":
                case "49":
                    if (D.GetDecimal(this._flexM[row, "AM_ADDTAX"]) == 0)
                    {
                        DataRow row1 = table.NewRow();
                        row1["ROW"] = this._flexM[row, "ROW_ID"];
                        row1["MSG"] = "부가세금액이 없습니다.";
                        table.Rows.Add(row1);
                        break;
                    }
                    break;
            }
            if (D.GetString(str) != "00")
            {
                string string1 = D.GetString(this._flexM[row, "CD_BIZAREA"]);
                if (string.IsNullOrEmpty(string1))
                {
                    DataRow row1 = table.NewRow();
                    row1["ROW"] = this._flexM[row, "ROW_ID"];
                    row1["MSG"] = ("부가세사업장 코드값이 존재하지 않습니다. 코드:" + string1);
                    table.Rows.Add(row1);
                }
            }
            if (table.Rows.Count > 0)
                flag1 = false;
            this._dt오류메세지.Merge(table);
            return flag1;
        }

        private bool CheckSave()
        {
            this._dt오류메세지.Clear();
            for (int @fixed = this._flexM.Rows.Fixed; @fixed < this._flexM.Rows.Count; ++@fixed)
            {
                string str = D.GetString(this._flexM[@fixed, "TP_RESULT"]);
                if (str == "")
                    str = "성공";
                bool flag = this.CheckDetail(@fixed);
                this._flexM[@fixed, "TP_RESULT"] = flag ? "성공" : "오류";
                if (D.GetString(this._flexM[@fixed, "TP_RESULT"]) != str)
                    this.Set그리드컬러(@fixed);
            }
            return this._dt오류메세지.Rows.Count <= 0;
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.BeforeSave() || !this.Verify()) return false;

            if (!this.CheckSave())
            {
                this.ShowMessage("오류가 발행했습니다. \n결과컬럼을 클릭하여 오류에 대한 정보를 확인하시기 바랍니다.");
                return false;
            }

            DataTable dataTable;

            if (this._엑셀데이타)
            {
                dataTable = this._flexM.DataTable;
                string str = this._biz.순번();
                int num = 1;
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                {
                    dataTable.Rows[index]["ROW_ID"] = (str + D.GetString(num).PadLeft(20 - str.Length, '0'));
                    ++num;
                }
            }
            else
            {
                DataRow[] dataRowArray = this._flexM.DataTable.Select("ROW_ID IS NULL OR ROW_ID=''");
                if (dataRowArray.Length > 0)
                {
                    string str = this._biz.순번();
                    int num = 1;
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["ROW_ID"] = (str + D.GetString(num).PadLeft(20 - str.Length, '0'));
                        ++num;
                        this._biz.ImageUpdate("", "", "", dataRow["NO_GIAN"].ToString(), "Y");
                        dataRow.AcceptChanges();
                        dataRow.SetAdded();
                    }
                }
                dataTable = this._flexM.DataTable;
            }

            if (dataTable == null) return true;

            foreach (DataRow dr in dataTable.Rows)
            {
                if(dr.RowState == DataRowState.Modified)
                {
                    if (!string.IsNullOrEmpty(D.GetString(dr["CD_UMNG1"])) && D.GetString(dr["CD_STDACCT"]) != "55100")
                    {
                        Global.MainFrame.ExecuteScalar("DELETE FROM FI_RECEPT" + Environment.NewLine +
                                                       "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                       "AND NO_RECEPT = '" + D.GetString(dr["CD_UMNG1"]) + "'");

                        dr["CD_UMNG1"] = string.Empty;
                    }    
                }
            }

            this._biz.Save(dataTable, this._엑셀데이타);

            this._flexM.AcceptChanges();
            this._엑셀데이타 = false;
            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flexM.HasNormalRow) return;

                DataRow[] dataRowArray1 = this._flexM.DataTable.Select("S = 'Y' AND TP_DOCU <> '0' ", "", DataViewRowState.CurrentRows);
                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("전표처리된 데이타는 삭제할수 없습니다. 전표처리된 행이 존재합니다");
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexM.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray2 == null || dataRowArray2.Length == 0)
                    {
                        if (!string.IsNullOrEmpty(D.GetString(this._flexM.Rows[this._flexM.Row]["CD_UMNG1"])))
                        {
                            Global.MainFrame.ExecuteScalar("DELETE FROM FI_RECEPT" + Environment.NewLine +
                                                           "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                           "AND NO_RECEPT = '" + D.GetString(this._flexM.Rows[this._flexM.Row]["CD_UMNG1"]) + "'");
                        }

                        this._flexM.Rows.Remove(this._flexM.Row);
                    }
                    else
                    {
                        foreach (DataRow dataRow in dataRowArray2)
                        {
                            if (!string.IsNullOrEmpty(D.GetString(dataRow["CD_UMNG1"])))
                            {
                                Global.MainFrame.ExecuteScalar("DELETE FROM FI_RECEPT" + Environment.NewLine +
                                                               "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                               "AND NO_RECEPT = '" + D.GetString(dataRow["CD_UMNG1"]) + "'");
                            }

                            dataRow.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flexM.HasNormalRow) return;

                if (this._flexM.GetCheckedRows("S") == null)
                {
                    this.ShowMessage("인쇄할 전표를 선택하세요.");
                }
                else
                {
                    DataTable dt = new DataView(this._flexM.GetTableFromGrid(), "S = 'Y'", "", DataViewRowState.CurrentRows).ToTable();
                    switch (Global.MainFrame.ServerKeyCommon)
                    {
                        case "SWPNC":
                        case "DZSQL":
                        case "SQL_":
                            this.RDF = new ReportHelper("R_FI_EPDOCU_0", "DEFAULT");
                            this.RDF.CreatePrintOption();
                            this.RDF.Printing += new ReportHelper.PrintEventHandler(this.rdf_Printing);
                            this.RDF.SetDataTable(dt);
                            this.RDF.Print();
                            break;
                        default:
                            new PrintRDF("R_FI_EPDOCU_0", true) { NoSheetData = new Dictionary<string, object>() { { "R_FI_EPDOCU_010_SOLIDTECH.RDF", dt } } }.ShowDialog();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool rdf_Printing(object sender, PrintArgs args)
        {
            if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
            {
                this.RDF.SetFixedformStyle(1);
                this.RDF.SetUsePrintApprove(false);
                this.RDF.PrintHelper.SetDecision(101, this.RDF.PrintHelper.GetDecision(), "결재", true);
            }
            return true;
        }

        private bool RDF_Printing(object sender, PrintArgs args)
        {
            try
            {
                if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
                {
                    if (!this.OnPreparePrint(args))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return false;
            }
        }

        private bool OnPreparePrint(PrintArgs args)
        {
            return true;
        }
        #endregion

        #region 버튼 이벤트
        private void btn합계계정전표처리설정_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_FI_EPDOCU_DOCU_OPT pFiEpdocuDocuOpt = new P_CZ_FI_EPDOCU_DOCU_OPT(this._biz.Get_환경설정_TP_EXPENSE());
                if (pFiEpdocuDocuOpt.ShowDialog() != DialogResult.OK)
                    return;
                this._biz.Set_환경설정_TP_EXPENSE(pFiEpdocuDocuOpt.GetResult);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void bnt증빙가져오기_Click(object sender, EventArgs e)
        {
            try
            {
                string 권한 = Global.MainFrame.CurrentGrantMenu;
                if (권한 == "C") 
                    권한 = "P";
                else if (권한 == "B")
                    권한 = "P";

                if (권한 != "E" && this.ShowMessage("해당 부서의 자료를 모두 가져오시겠습니까?", "QY2") == DialogResult.No)
                    권한 = "E";
                
                if (권한 == "P" && D.IsEmpty(this.bpc결의부서.SelectedValue.ToString()))
                {
                    this.ShowMessage("부서를 선택하세요.");
                }
                else if (권한 == "D" && D.IsEmpty(this.bpc결의부서.SelectedValue.ToString()))
                {
                    this.ShowMessage("부서를 선택하세요.");
                }
                else if (권한 == "E" && D.IsEmpty(this.ctx결의사원.CodeValue.ToString()))
                {
                    this.ShowMessage("사원을 선택하세요.");
                }
                else
                {
                    DataTable dt1 = this._biz.증빙조회("Y", 권한, this.dtp증빙일자.StartDateToString, this.dtp증빙일자.EndDateToString, this.ctx회계단위.CodeValue.ToString(), this.ctx결의사원.CodeValue.ToString(), this.bpc결의부서.QueryWhereIn_Pipe);
                    if (dt1.Select("TP_GIAN ='Y'").Length > 0 && this.ShowMessage("처리된 데이터가 있습니다.\n 기존 자료에 대하여 덮어씌우시겠습니까?", "QY2") != DialogResult.Yes)
                    {
                        dt1.DefaultView.RowFilter = "TP_GIAN ='N'";
                        dt1 = dt1.DefaultView.ToTable();
                    }
                    this.ShowMessage("증빙자료를 가져오고 있습니다.");
                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in (InternalDataCollectionBase)dt1.Rows)
                            {
                                dataRow["ID_WRITE"] = Global.MainFrame.LoginInfo.EmployeeNo;
                                dataRow["NM_WRITE"] = Global.MainFrame.LoginInfo.UserName;
                                dataRow["CD_PC"] = this.ctx회계단위.CodeValue;
                                dataRow["CD_WDEPT"] = this.bpc결의부서.SelectedValue;
                            }
                        }
                        this.BindingDataTalbe(dt1);
                    }
                    if (this._flexM.DataTable == null)
                    {
                        MsgControl.CloseMsg();
                    }
                    else
                    {
                        string PaterPipe = string.Empty;
                        if (!this._flexM.HasNormalRow)
                        {
                            MsgControl.CloseMsg();
                        }
                        else
                        {
                            if (Global.MainFrame.ServerKey != "DUZON_NETWORK")
                            {
                                foreach (DataRow dataRow in (InternalDataCollectionBase)dt1.Rows)
                                    PaterPipe = PaterPipe + dataRow["NO_COMPANY"] + "|";
                                if (PaterPipe.Length > 0)
                                {
                                    bool Result = false;
                                    DataTable dt2 = (DataTable)null;
                                    this._biz.CheckPartner(PaterPipe, out dt2, out Result);
                                    if (Result && this.ShowMessage("등록되지 않은 사업자번호가 있습니다. \n거래처를 등록하시겠습니까?", "QY2") == DialogResult.Yes)
                                    {
                                        this._biz.거래처_저장(dt2);
                                        foreach (DataRow dataRow1 in (InternalDataCollectionBase)dt2.Rows)
                                        {
                                            DataRow[] dataRowArray = this._flexM.DataTable.Select("NO_COMPANY = '" + dataRow1["NO_COMPANY"].ToString() + "'");
                                            if (dataRowArray.Length > 0)
                                            {
                                                foreach (DataRow dataRow2 in dataRowArray)
                                                {
                                                    dataRow2["CD_PARTNER"] = dataRow1["CD_PARTNER"].ToString();
                                                    dataRow2["LN_PARTNER"] = dataRow1["LN_PARTNER"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            MsgControl.CloseMsg();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                this.MsgEnd(ex);
            }
        }

        private void btn관리항목일괄적용_Click(object sender, EventArgs e)
        {
            string 관리항목코드, 컬럼코드, 컬럼명, 코드값, 코드이름;

            try
            {
                if (!this._flexM.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexM.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0) return;

                관리항목코드 = string.Empty;
                컬럼코드 = string.Empty;
                컬럼명 = string.Empty;
                코드값 = string.Empty;
                코드이름 = string.Empty;

                if (((Control)sender).Name == this.btn코스트센터일괄적용.Name)
                {
                    관리항목코드 = "A02";
                    컬럼코드 = "CD_CC";
                    컬럼명 = "NM_CC";
                    코드값 = this.ctx코스트센터.CodeValue;
                    코드이름 = this.ctx코스트센터.CodeName;
                }
                else if (((Control)sender).Name == this.btn사원일괄적용.Name)
                {
                    관리항목코드 = "A04";
                    컬럼코드 = "CD_EMPLOY";
                    컬럼명 = "NM_EMP";
                    코드값 = this.ctx사원.CodeValue;
                    코드이름 = this.ctx사원.CodeName;
                }
                else if(((Control)sender).Name == this.btn프로젝트일괄적용.Name)
                {
                    관리항목코드 = "A05";
                    컬럼코드 = "CD_PJT";
                    컬럼명 = "NM_PJT";
                    코드값 = this.ctx프로젝트.CodeValue;
                    코드이름 = this.ctx프로젝트.CodeName;
                }
                else if (((Control)sender).Name == this.btn신용카드일괄적용.Name)
                {
                    관리항목코드 = "A08";
                    컬럼코드 = "CD_CARD";
                    컬럼명 = "NM_CARD";
                    코드값 = this.ctx신용카드.CodeValue;
                    코드이름 = this.ctx신용카드.CodeName;
                }
                else if (((Control)sender).Name == this.btn거래처일괄적용.Name)
                {
                    관리항목코드 = "A06";
                    컬럼코드 = "CD_PARTNER";
                    컬럼명 = "LN_PARTNER";
                    코드값 = this.ctx거래처.CodeValue;
                    코드이름 = this.ctx거래처.CodeName;
                }

                foreach (DataRow dataRow in dataRowArray)
                {
                    if (!(D.GetString(dataRow["TP_DOCU"]) == "1"))
                    {
                        dataRow[컬럼코드] = 코드값;
                        dataRow[컬럼명] = 코드이름;

                        int index1 = 0;
                        for (int index2 = 1; index2 <= 10; ++index2)
                        {
                            if (D.GetString(dataRow["CD_MNG" + index2.ToString()]) == 관리항목코드)
                            {
                                dataRow["CD_MNGD" + index2.ToString()] = 코드값;
                                dataRow["NM_MNGD" + index2.ToString()] = 코드이름;
                                index1 = index2;
                                break;
                            }
                        }

                        if (index1 > 0 && index1 < 6)
                        {
                            this._flexL[index1, "CD_MNGD"] = 코드값;
                            this._flexL[index1, "NM_MNGD"] = 코드이름;
                        }
                        else
                        {
                            this._flexR[index1 - 5, "CD_MNGD"] = 코드값;
                            this._flexR[index1 - 5, "NM_MNGD"] = 코드이름;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn증빙유형일괄적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexM.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0) return;
                
                HelpReturn helpReturn = (HelpReturn)new PageBase().ShowHelp(new HelpParam(HelpID.P_USER)
                {
                    UserCodeName = "NAME",
                    UserCodeValue = "CODE",
                    UserHelpID = "H_FI_HELP01",
                    UserParams = "증빙유형도움창;H_FI_EVID;;"
                });

                if (helpReturn.DialogResult != DialogResult.OK || helpReturn.DataTable.Rows.Count == 0)
                    return;

                foreach (DataRow dataRow in dataRowArray)
                {
                    if (!(D.GetString(dataRow["TP_DOCU"]) == "1"))
                    {
                        dataRow["CD_EVID"] = helpReturn.CodeValue;
                        dataRow["NM_EVID"] = helpReturn.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn결의코드일괄적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexM.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                    return;
                
                HelpReturn helpReturn = (HelpReturn)new PageBase().ShowHelp(new HelpParam(HelpID.P_USER)
                {
                    UserCodeName = "NAME",
                    UserCodeValue = "CODE",
                    UserHelpID = "H_FI_EPCODE_HELP",
                    UserParams = "결의내역도움창;H_FI_EPCODE_HELP;;;"
                });
                
                if (helpReturn.DialogResult != DialogResult.OK || helpReturn.DataTable.Rows.Count == 0)
                    return;
                
                foreach (DataRow dataRow in dataRowArray)
                {
                    if (!(D.GetString(dataRow["TP_DOCU"]) == "1"))
                    {
                        dataRow["CD_EPCODE"] = helpReturn.CodeValue;
                        dataRow["NM_EPCODE"] = helpReturn.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void bpc결의내역_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                this.bpc결의내역.UserParams = "결의내역도움창;H_FI_EPCODE_HELP;" + this.bpc결의부서.QueryWhereIn_Pipe + ";";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc결의부서_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.bpc결의내역.Clear();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk회계단위 || !this.Chk결의부서 || !this.Chk결의사원)
                    return;
                if (this.bpc결의부서.Count != 1)
                {
                    this.ShowMessage("결의서를 입력할경우에는 결의부서를 여러개 선택할수 없습니다. \n 하나의 결의부서만 선택하세요.");
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;
                    DataTable dt = new Excel().StartLoadExcel(openFileDialog.FileName, 0, 3);
                    if (dt == null || dt.Rows.Count == 0)
                        return;
                    foreach (DataRow dataRow in (InternalDataCollectionBase)dt.Rows)
                    {
                        if (dt.Columns.Contains("AM_TAXSTD") && dataRow["AM_TAXSTD"].ToString() == string.Empty)
                            dataRow["AM_TAXSTD"] = 0;
                        if (dt.Columns.Contains("AM_ADDTAX") && dataRow["AM_ADDTAX"].ToString() == string.Empty)
                            dataRow["AM_ADDTAX"] = 0;
                        if (dt.Columns.Contains("AM_EX") && dataRow["AM_EX"].ToString() == string.Empty)
                            dataRow["AM_EX"] = 0;
                        if (dt.Columns.Contains("RT_EXCH") && dataRow["RT_EXCH"].ToString() == string.Empty)
                            dataRow["RT_EXCH"] = 0;
                        if (dt.Columns.Contains("AM_TAX1") && dataRow["AM_TAX1"].ToString() == string.Empty)
                            dataRow["AM_TAX1"] = 0;
                        if (dt.Columns.Contains("AM_SUPPLY1") && dataRow["AM_SUPPLY1"].ToString() == string.Empty)
                            dataRow["AM_SUPPLY1"] = 0;
                        if (dt.Columns.Contains("QT_TAX1") && dataRow["QT_TAX1"].ToString() == string.Empty)
                            dataRow["QT_TAX1"] = 0;
                        if (dt.Columns.Contains("AM_PRC1") && dataRow["AM_PRC1"].ToString() == string.Empty)
                            dataRow["AM_PRC1"] = 0;
                        if (dt.Columns.Contains("NO_COMPANY") && dataRow["NO_COMPANY"].ToString().Length > 10)
                        {
                            this.ShowMessage("사업자등록번호 자릿 수를 넘었습니다.(실제자릿수: 12, 최대자릿수: 10) \n다시 입력하시고 업로드하세요!\n 입력된 값 :@", dataRow["NO_COMPANY"].ToString());
                            return;
                        }
                    }
                    
                    if (this._biz.Save_Excel(dt))
                    {
                        this._flexM.Binding = this._biz.SearchExcel(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   this.ctx회계단위.CodeValue,
                                                                                   D.GetString(this.bpc결의부서.SelectedValue),
                                                                                   this.ctx결의사원.CodeValue });
                        this._엑셀데이타 = true;
                    }

                    this.SetToolBarButtonState(true, false, true, true, false);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표처리_Click(object sender, EventArgs e)
        {
            string noDocuLine;
            int num;

            try
            {
                if (!this._flexM.HasNormalRow) return;

                if (this.IsChanged())
                {
                    this.ShowMessage("변경된데이터가 존재합니다.저장후 처리하세요!");
                    this.ToolBarSaveButtonEnabled = true;
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexM.DataTable.Select("S = 'Y' AND TP_DOCU <> '0' ");
                    if (dataRowArray1 != null && dataRowArray1.Length > 0)
                    {
                        this.ShowMessage("전표처리된 행이 존재합니다");
                    }
                    else
                    {
                        DataRow[] dr_row = this._flexM.DataTable.Select("S = 'Y'");
                        if (dr_row == null || dr_row.Length == 0)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다);
                        }
                        else
                        {
                            if (!this.전표마감(dr_row) || !this.CheckDocu())
                                return;
                            int num4 = 1;
                            string 회계일자 = null;
                            string 작성일자 = null;
                            if (dr_row.Length > 1)
                            {
                                P_CZ_FI_EPDOCU_OPT pFiEpdocuOpt = new P_CZ_FI_EPDOCU_OPT(this._CheckBntEnable);
                                if (pFiEpdocuOpt.ShowDialog() != DialogResult.OK)
                                    return;
                                num4 = pFiEpdocuOpt.GetResult;
                                회계일자 = pFiEpdocuOpt.GetDtAcct;
                                작성일자 = pFiEpdocuOpt.GetDtWrite;
                            }
                            string key1 = string.Empty;
                            if (num4 == 1)
                            {
                                foreach (DataRow dataRow in dr_row)
                                {
                                    string key2 = D.GetString(dataRow["ROW_ID"]) + "|";
                                    object[] result = null;
                                    if (!this._biz.전표처리(key2, this.chk승인전표.Checked, 회계일자, 작성일자, out result))
                                    {
                                        dataRow["TP_RESULT"] = "오류";
                                        DataRow row = this._dt오류메세지.NewRow();
                                        row["ROW"] = dataRow["ROW_ID"];
                                        row["MSG"] = "전표처리중 오류가 발생했습니다.";
                                        this._dt오류메세지.Rows.Add(row);
                                    }
                                    else
                                    {
                                        dataRow["TP_RESULT"] = "성공";
                                        dataRow["NO_DOCU"] = result[0];
                                        dataRow["TP_DOCU"] = "1";

                                        if (!string.IsNullOrEmpty(D.GetString(dataRow["CD_UMNG1"])))
                                        {
                                            DataTable dt = Global.MainFrame.FillDataTable(@"SELECT NO_DOLINE
                                                                                            FROM FI_DOCU WITH(NOLOCK)
                                                                                            WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                                           "AND NO_DOCU = '" + D.GetString(dataRow["NO_DOCU"]) + "'" + Environment.NewLine +
                                                                                           "AND CD_ACCT = '" + D.GetString(dataRow["CD_STDACCT"]) + "'");

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                noDocuLine = D.GetString(dt.Rows[0]["NO_DOLINE"]);

                                                Global.MainFrame.ExecuteScalar("UPDATE FI_DOCU" + Environment.NewLine +
                                                                               "SET CD_MNG = '" + D.GetString(dataRow["CD_UMNG1"]) + "'" + Environment.NewLine +
                                                                               "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                               "AND NO_DOCU = '" + D.GetString(dataRow["NO_DOCU"]) + "'" + Environment.NewLine +
                                                                               "AND NO_DOLINE = '" + noDocuLine + "'");

                                                Global.MainFrame.ExecuteScalar("UPDATE FI_RECEPT" + Environment.NewLine +
                                                                               "SET NO_DOCU = '" + D.GetString(dataRow["NO_DOCU"]) + "'," + Environment.NewLine +
                                                                               "NO_DOLINE = '" + noDocuLine + "'" + Environment.NewLine +
                                                                               "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                               "AND NO_RECEPT = '" + D.GetString(dataRow["CD_UMNG1"]) + "'");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                object[] result = null;
                                if (this._CheckBntEnable)
                                {
                                    DataRow[] dataRowArray2 = this._flexM.DataTable.Select("S = 'Y' AND DT_ACCT <> '" + dr_row[0]["DT_ACCT"] + "'");
                                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                                    {
                                        this.ShowMessage("증빙일자가 다른건이 포함되어 있습니다 \n증빙일자별로 선택 처리하십시오");
                                        return;
                                    }
                                }
                                foreach (DataRow dataRow in dr_row)
                                    key1 = key1 + D.GetString(dataRow["ROW_ID"]) + "|";
                                if (!this._biz.전표처리(key1, this.chk승인전표.Checked, 회계일자, 작성일자, out result))
                                {
                                    foreach (DataRow dataRow in dr_row)
                                    {
                                        dataRow["TP_RESULT"] = "오류";
                                        DataRow row = this._dt오류메세지.NewRow();
                                        row["ROW"] = dataRow["ROW_ID"];
                                        row["MSG"] = "전표처리중 오류가 발생했습니다.";
                                        this._dt오류메세지.Rows.Add(row);
                                    }
                                }
                                else
                                {
                                    DataTable dt = Global.MainFrame.FillDataTable(@"SELECT NO_DOLINE
                                                                                    FROM FI_DOCU WITH(NOLOCK)
                                                                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                                   "AND NO_DOCU = '" + D.GetString(result[0]) + "'" + Environment.NewLine +
                                                                                   "AND CD_ACCT = '55100'");

                                    num = 0;

                                    foreach (DataRow dataRow in dr_row)
                                    {
                                        dataRow["TP_RESULT"] = "성공";
                                        dataRow["NO_DOCU"] = result[0];
                                        dataRow["TP_DOCU"] = "1";

                                        if (!string.IsNullOrEmpty(D.GetString(dataRow["CD_UMNG1"])))
                                        {
                                            if (dt != null && dt.Rows.Count > num)
                                            {
                                                noDocuLine = D.GetString(dt.Rows[num]["NO_DOLINE"]);

                                                Global.MainFrame.ExecuteScalar("UPDATE FI_DOCU" + Environment.NewLine +
                                                                               "SET CD_MNG = '" + D.GetString(dataRow["CD_UMNG1"]) + "'" + Environment.NewLine +
                                                                               "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                               "AND NO_DOCU = '" + D.GetString(result[0]) + "'" + Environment.NewLine +
                                                                               "AND NO_DOLINE = '" + noDocuLine + "'");

                                                Global.MainFrame.ExecuteScalar("UPDATE FI_RECEPT" + Environment.NewLine +
                                                                               "SET NO_DOCU = '" + D.GetString(result[0]) + "'," + Environment.NewLine +
                                                                               "NO_DOLINE = '" + noDocuLine + "'" + Environment.NewLine +
                                                                               "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                               "AND NO_RECEPT = '" + D.GetString(dataRow["CD_UMNG1"]) + "'");

                                                num++;
                                            }
                                        }
                                    }
                                }
                            }
                            this._flexM.AcceptChanges();
                            if (this._dt오류메세지.Rows.Count > 0)
                            {
                                this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다, new string[] { this.DD("전표처리") });
                            }
                            else
                            {
                                this._flexM.RowFilter = string.Empty;
                                this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("전표처리") });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = this._flexM.DataTable.Clone();
                DataRow[] dataRowArray1 = this._flexM.DataTable.Select("S = 'Y' AND TP_DOCU = '0'");
                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("전표미처리된 행이 존재합니다");
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexM.DataTable.Select("S = 'Y' AND TP_DOCU = '2'");
                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this.ShowMessage("전표승인된 행이 존재합니다");
                    }
                    else
                    {
                        DataRow[] dataRowArray3 = this._flexM.DataTable.Select("S = 'Y' AND TP_DOCU = '1'");
                        if (dataRowArray3 == null || dataRowArray3.Length == 0)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다);
                        }
                        else
                        {
                            foreach (DataRow dataRow in dataRowArray3)
                                dataTable.Rows.Add(dataRow.ItemArray);
                            foreach (DataRow dataRow1 in (InternalDataCollectionBase)dataTable.DefaultView.ToTable(1 != 0, "CD_PC", "TP_DOCU", "NO_DOCU").Rows)
                            {
                                string string1 = D.GetString(dataRow1["CD_PC"]);
                                string string2 = D.GetString(dataRow1["NO_DOCU"]);
                                DataRow[] dataRowArray4 = this._flexM.DataTable.Select("S = 'Y' AND TP_DOCU = '1' AND NO_DOCU = '" + string2 + "'");
                                if (!this._biz.전표취소(string1, string2))
                                {
                                    foreach (DataRow dataRow2 in dataRowArray4)
                                    {
                                        dataRow2["TP_RESULT"] = "오류";
                                        DataRow row = this._dt오류메세지.NewRow();
                                        row["ROW"] = dataRow2["ROW_ID"];
                                        row["MSG"] = "전표를 삭제하는중에 오류가 발생했습니다.";
                                        this._dt오류메세지.Rows.Add(row);
                                    }
                                }
                                else
                                {
                                    foreach (DataRow dataRow2 in dataRowArray4)
                                    {
                                        dataRow2["TP_RESULT"] = "성공";
                                        dataRow2["NO_DOCU"] = "";
                                        dataRow2["TP_DOCU"] = "0";
                                        if (dataRow2["IMAGE_PATH_CHECK"].ToString() == "Y")
                                            this._biz.ImageUpdate("", "", "", dataRow2["NO_GIAN"].ToString(), "D");

                                        if (!string.IsNullOrEmpty(D.GetString(dataRow2["CD_UMNG1"])))
                                        {
                                            Global.MainFrame.ExecuteScalar("UPDATE FI_RECEPT" + Environment.NewLine +
                                                                           "SET NO_DOCU = ''," + Environment.NewLine +
                                                                           "NO_DOLINE = 0" + Environment.NewLine +
                                                                           "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                           "AND NO_RECEPT = '" + D.GetString(dataRow2["CD_UMNG1"]) + "'");
                                        }
                                    }
                                }
                            }
                            
                            this._flexM.AcceptChanges();
                            this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("전표삭제") });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전자세금계산서처리_Click(object sender, EventArgs e)
        {
            if (!this.Chk회계단위 || !this.Chk결의부서 || !this.Chk결의사원)
                return;

            if (this.bpc결의부서.Count != 1)
            {
                this.ShowMessage("전자세금계산서를 처리할경우에는 결의부서를 여러개 선택할수 없습니다. \n 하나의 결의부서만 선택하세요.");
            }
            else
            {
                if (new P_CZ_FI_EPDOCU_ETAX(new object[] { this.ctx회계단위.CodeValue,
                                                           D.GetString(this.bpc결의부서.SelectedValue),
                                                           this.ctx결의사원.CodeValue }).ShowDialog() != DialogResult.OK)
                    return;

                this.OnToolBarSearchButtonClicked(null, (EventArgs)null);
            }
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;
                string fileName = folderBrowserDialog.SelectedPath + "\\P_FI_EPDOCU.xls";
                new WebClient().DownloadFile(Global.MainFrame.HostURL + "/shared/FI/P_FI_EPDOCU.xls", fileName);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flexM_BeforeShowContextMenu(object sender, EventArgs e)
        {
            this._flexM.AddMyMenu = true;
            this._flexM.AddMenuSeperator();
            this._flexM.AddMenuItem("입력순_정렬", new EventHandler(this.ContextMenuItem_Click));
            this._flexM.AddMenuSeperator();
            this._flexM.AddMenuItem("전표입력", new EventHandler(this.ContextMenuItem_Click));
            this._flexM.AddMenuItem("전표승인", new EventHandler(this.ContextMenuItem_Click));
        }

        private void ContextMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
                if (toolStripMenuItem == null) return;
                
                string str1 = string.Empty;
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                string str6 = string.Empty;

                if (this._flexM.HasNormalRow)
                {
                    str1 = this._flexM.Rows[this._flexM.Row]["TP_DOCU"].ToString();
                    str2 = this._flexM.Rows[this._flexM.Row]["DT_ACCT"].ToString();
                    str3 = this._flexM.Rows[this._flexM.Row]["CD_WDEPT"].ToString();
                    str4 = this._flexM.Rows[this._flexM.Row]["NM_DEPT"].ToString();
                    str5 = this._flexM.Rows[this._flexM.Row]["ID_WRITE"].ToString();
                    str6 = this._flexM.Rows[this._flexM.Row]["NM_WRITE"].ToString();
                }

                switch (toolStripMenuItem.Name)
                {
                    case "전표입력":
                        if (this._flexM.HasNormalRow && !this._flexM.Rows[this._flexM.Row].IsNode)
                        {
                            if (this._flexM.Cols[this._flexM.Col].Name == "TP_RESULT" && D.GetString(this._flexM["TP_RESULT"]) == "오류")
                                new P_CZ_FI_EPDOCU_MSG(this._dt오류메세지, D.GetString(this._flexM["ROW_ID"])).Show();
                            if (D.GetString(this._flexM["TP_DOCU"]) != "0")
                                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { D.GetString(this._flexM["NO_DOCU"]), 
                                                                                                                                    "0",
                                                                                                                                    D.GetString(this._flexM["CD_PC"]),
                                                                                                                                    this.LoginInfo.CompanyCode });
                            break;
                        }
                        break;
                    case "전표승인":
                        if (this._flexM.HasNormalRow && !this._flexM.Rows[this._flexM.Row].IsNode)
                        {
                            if (this._flexM.Cols[this._flexM.Col].Name == "TP_RESULT" && D.GetString(this._flexM["TP_RESULT"]) == "오류")
                                new P_CZ_FI_EPDOCU_MSG(this._dt오류메세지, D.GetString(this._flexM["ROW_ID"])).Show();
                            if (D.GetString(this._flexM["TP_DOCU"]) != "0")
                                this.CallOtherPageMethod("P_FI_DOCUAPP", "전표승인(" + this.PageName + ")", this.Grant, new object[] { this.ctx회계단위.CodeValue,
                                                                                                                                       this.ctx회계단위.CodeName,
                                                                                                                                       str2,
                                                                                                                                       str2,
                                                                                                                                       str1,
                                                                                                                                       str5,
                                                                                                                                       str6,
                                                                                                                                       str3,
                                                                                                                                       str4 });
                            break;
                        }
                        break;
                    case "입력순_정렬":
                        this._bRowIdSort = true;
                        this.OnToolBarSearchButtonClicked(null, (EventArgs)null);
                        this._bRowIdSort = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_Click(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                if (flexGrid == null || flexGrid.Row < flexGrid.Rows.Fixed || (flexGrid.Row != flexGrid.MouseRow || flexGrid.Col != flexGrid.MouseCol || flexGrid.Cols[flexGrid.Col].Name != "IMAGE_PATH_CHECK") || (!(flexGrid[flexGrid.Row, "IMAGE_PATH_CHECK"].ToString() == "Y") || !(flexGrid[flexGrid.Row, "IMAGE_PATH_D"].ToString() != string.Empty)))
                    return;
                ImageForm imageForm = new ImageForm();
                imageForm.LoadImage(flexGrid[flexGrid.Row, "IMAGE_PATH_D"].ToString());
                imageForm.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_StartEdit(object sender, RowColEventArgs e)
        {
            if (this._flexM.Cols[e.Col].Name != "S" && D.GetString(this._flexM["TP_DOCU"]) != "0")
            {
                e.Cancel = true;
                return;
            }

            if ((this._flexM.Cols[e.Col].Name == "CD_BUDGET") || (this._flexM.Cols[e.Col].Name == "CD_BGACCT") || (this._flexM.Cols[e.Col].Name == "CD_BIZPLAN") || (this._flexM.Cols[e.Col].Name == "NM_DUTY_RANK"))
            {
                e.Cancel = true;
                return;
            }

            if (this._flexM.Cols[e.Col].Name == "AM_TAXSTD" && D.GetInt(Global.MainFrame.ExecuteScalar(@"SELECT COUNT(1)
                                                                                                         FROM FI_ACCTCODE WITH(NOLOCK)
                                                                                                         WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                                                        "AND CD_ACCT = '" + D.GetString(this._flexM["CD_STDACCT"]) + "'" + Environment.NewLine +
                                                                                                        "AND CD_RELATION = '82'")) > 0)
            {
                P_CZ_FI_RECEPTRE dialog = new P_CZ_FI_RECEPTRE(this._flexM.GetDataRow(e.Row));
                dialog.ShowDialog();
                return;
            }
        }

        private void _flexM_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (this._flexM.Cols[e.Col].Name)
                {
                    case "NM_EPCODE":
                    case "NM_EVID":
                        if (!string.IsNullOrEmpty(D.GetString(this._flexM[this._flexM.Cols[e.Col].Name])))
                            break;
                        this.Set관리항목초기화();
                        this._flexM["TP_EPNOTE"] = string.Empty;
                        this._flexM["CD_EVID"] = string.Empty;
                        this._flexM["NM_EVID"] = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (D.GetString(this._flexM["TP_DOCU"]) != "0")
                    e.Cancel = true;
                if (D.GetString(this._flexM["CD_PARTNER"]).Equals("00"))
                {
                    this.Set관리항목("A06", "00", this._flexM["LN_PARTNER"].ToString());
                    e.Cancel = true;
                }
                switch (this._flexM.Cols[e.Col].Name)
                {
                    case "NM_EPCODE":
                        e.Parameter.UserParams = "결의코드도움창;H_FI_EPCODE_HELP;" + this.bpc결의부서.QueryWhereIn_Pipe + ";" + D.GetString(this._flexM[e.Row, "TP_EPNOTE"]);
                        break;
                    case "NM_EVID":
                        e.Parameter.UserParams = "증빙유형도움창;H_FI_EVID;" + D.GetString(this._flexM[e.Row, "TP_EPNOTE"]);
                        break;
                    case "NM_UMNG1":
                    case "NM_UMNG2":
                    case "NM_UMNG3":
                    case "NM_UMNG4":
                    case "NM_UMNG5":
                        e.Parameter.P34_CD_MNG = "A2" + this._flexM.Cols[e.Col].Name.Substring(7, 1);
                        break;
                    case "NM_EXCH":
                        e.Parameter.P41_CD_FIELD1 = "MA_B000005";
                        break;
                    case "NM_MUTUAL":
                        e.Parameter.P41_CD_FIELD1 = "FI_T000002";
                        break;
                    case "NM_CC":
                        e.Parameter.UseAccessGrant = false;
                        e.Parameter.P12_CD_ITEM = this.관리내역(this._flexM.Row, "C31")[0];
                        break;
                    case "NM_TP_EVIDENCE":
                        e.Parameter.P41_CD_FIELD1 = "FI_F000105";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_CodeHelp(object sender, CodeHelpEventArgs e)
        {
            try
            {
                if (this._flexM.Cols[e.Col].Name != "AM_AMT" && !this._flexM.AllowEditing)
                    return;
                switch (this._flexM.Cols[e.Col].Name)
                {
                    case "NM_BIZPLAN":
                        if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue.Length == 0)
                        {
                            this._flexM[e.Row, "CD_BIZPLAN"] = "";
                            this._flexM[e.Row, "NM_BIZPLAN"] = "";
                            if (!(this._sTpBudgeAcct == "1"))
                                break;
                            this.예산관련컬럼셋팅(e.Row, (DataTable)null);
                            break;
                        }
                        HelpParam helpParam1 = new HelpParam(HelpID.P_FI_BIZPLAN2_SUB, this.MainFrameInterface);
                        if (e.Source == CodeHelpEnum.CodeSearch)
                            helpParam1.P92_DETAIL_SEARCH_CODE = e.EditValue;
                        helpParam1.P61_CODE1 = this._flexM[e.Row, "CD_BUDGET"].ToString();
                        HelpReturn helpReturn1 = e.Source != CodeHelpEnum.CodeSearch ? (HelpReturn)this.ShowHelp(helpParam1) : (HelpReturn)this.CodeSearch(helpParam1);
                        if (helpReturn1.DialogResult == DialogResult.Cancel)
                        {
                            this._flexM[e.Row, "NM_BIZPLAN"] = e.OriginValue;
                            break;
                        }
                        if (helpReturn1.DialogResult == DialogResult.OK)
                        {
                            if (this._sTpBudgeAcct == "1" && this._sTpBudgetMng == "1")
                            {
                                if (this._flexM[e.Row, "NM_BGACCT"].ToString() != "")
                                {
                                    if (!this._biz.연결계정체크(this._sTpBudgetMng, null, helpReturn1.CodeValue, this._flexM[e.Row, "CD_BGACCT"].ToString(), this._flexM[e.Row, "CD_STDACCT"].ToString()))
                                        this.예산관련컬럼셋팅(e.Row, (DataTable)null);
                                }
                                else
                                {
                                    DataTable dt = this._biz.연결예산계정(this._flexM[e.Row, "CD_STDACCT"].ToString(), null, helpReturn1.CodeValue);
                                    if (dt != null && dt.Rows.Count > 0)
                                        this.예산관련컬럼셋팅(e.Row, dt);
                                }
                            }
                            this._flexM[e.Row, "CD_BIZPLAN"] = helpReturn1.CodeValue;
                            this._flexM[e.Row, "NM_BIZPLAN"] = helpReturn1.CodeName;
                            break;
                        }
                        break;
                    case "NM_BUDGET":
                        if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue.Length == 0)
                        {
                            this._flexM[e.Row, "CD_BUDGET"] = "";
                            this._flexM[e.Row, "NM_BUDGET"] = "";
                            if (!(this._sTpBudgeAcct == "1") || !(this._sTpBudgetMng == "0"))
                                break;
                            this.예산관련컬럼셋팅(e.Row, (DataTable)null);
                            break;
                        }
                        HelpParam helpParam2 = new HelpParam(HelpID.P_FI_BGCODE_DEPT_SUB, this.MainFrameInterface);
                        helpParam2.P05_CD_DEPT = D.GetString(this.bpc결의부서.SelectedValue);
                        if (e.Source == CodeHelpEnum.CodeSearch)
                        {
                            helpParam2.P92_DETAIL_SEARCH_CODE = e.EditValue;
                            helpParam2.P23_DATE_END = this.MainFrameInterface.GetStringYearMonth;
                        }
                        HelpReturn helpReturn2 = e.Source != CodeHelpEnum.CodeSearch ? (HelpReturn)this.ShowHelp(helpParam2) : (HelpReturn)this.CodeSearch(helpParam2);
                        if (helpReturn2.DialogResult == DialogResult.Cancel)
                        {
                            this._flexM[e.Row, "NM_BUDGET"] = e.OriginValue;
                            break;
                        }
                        if (helpReturn2.DialogResult == DialogResult.OK)
                        {
                            if (this._sTpBudgeAcct == "1" && this._sTpBudgetMng == "0")
                            {
                                if (this._flexM[e.Row, "NM_BGACCT"].ToString().Trim() != "")
                                {
                                    if (!this._biz.연결계정체크(this._sTpBudgetMng, helpReturn2.CodeValue, null, this._flexM[e.Row, "CD_BGACCT"].ToString(), this._flexM[e.Row, "CD_STDACCT"].ToString()))
                                        this.예산관련컬럼셋팅(e.Row, (DataTable)null);
                                }
                                else
                                {
                                    DataTable dt = this._biz.연결예산계정(this._flexM[e.Row, "CD_STDACCT"].ToString(), helpReturn2.CodeValue, null);
                                    if (dt != null && dt.Rows.Count > 0)
                                        this.예산관련컬럼셋팅(e.Row, dt);
                                }
                            }
                            if (e.OriginValue.Length == 0 && this._flexM[e.Row, "CD_STDACCT"].ToString().Trim() != "")
                            {
                                DataTable dataTable = ((HelpReturn)this.CodeSearch(new HelpParam(HelpID.P_FI_ACCTCODE_SUB, this.MainFrameInterface)
                                {
                                    P33_TP_ACSTATS = "2",
                                    P61_CODE1 = this.LoginInfo.YnAcctJo,
                                    P92_DETAIL_SEARCH_CODE = this._flexM[e.Row, "CD_STDACCT"].ToString(),
                                    ResultMode = ResultMode.SlowMode
                                })).DataTable;
                                if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["CD_BGACCT"].ToString().Trim() != "")
                                    this.예산관련컬럼셋팅(e.Row, dataTable);
                            }
                            this._flexM[e.Row, "CD_BUDGET"] = helpReturn2.CodeValue;
                            this._flexM[e.Row, "NM_BUDGET"] = helpReturn2.CodeName;
                            if (this._sTpBudgetMng == "1")
                            {
                                this.GetCdBizplan(this._flexM[e.Row, "CD_BUDGET"].ToString());
                                string str = this._flexM[e.Row, "CD_BIZPLAN"].ToString().Trim();
                                if (str != "")
                                    this._flexM_CodeHelp(sender, new CodeHelpEventArgs(e.Row, this._flexM.Cols["NM_BIZPLAN"].Index, str, str, CodeHelpEnum.CodeSearch));
                            }
                            break;
                        }
                        break;
                    case "NM_BGACCT":
                        if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue.Equals(""))
                        {
                            this.예산관련컬럼셋팅(e.Row, (DataTable)null);
                            break;
                        }
                        string str1 = string.Empty;
                        if (this._sTpBudgeAcct == "1")
                        {
                            str1 = !(this._sTpBudgetMng == "0") ? this.DD("사업계획명") : this.DD("예산단위");
                            bool flag = false;
                            if (this._sTpBudgetMng == "0" && this._flexM[e.Row, "CD_BUDGET"].ToString().Trim() == "")
                                flag = true;
                            else if (this._sTpBudgetMng == "1" && this._flexM[e.Row, "CD_BIZPLAN"].ToString().Trim() == "")
                                flag = true;
                            if (flag)
                            {
                                this.ShowMessage("@을 먼저 입력하세요.", str1);
                                this.예산관련컬럼셋팅(e.Row, (DataTable)null);
                                break;
                            }
                        }
                        HelpParam helpParam3 = new HelpParam(HelpID.P_FI_BGACCT_NB2_SUB, this.MainFrameInterface);
                        helpParam3.P63_CODE3 = this._flexM[e.Row, "CD_STDACCT"].ToString();
                        if (this._sTpBudgeAcct == "1")
                            helpParam3.P65_CODE5 = !(this._sTpBudgetMng == "0") ? this._flexM[e.Row, "CD_BIZPLAN"].ToString() : this._flexM[e.Row, "CD_BUDGET"].ToString();
                        helpParam3.P35_CD_MNGD = "1";
                        helpParam3.ResultMode = ResultMode.SlowMode;
                        if (e.Source == CodeHelpEnum.CodeSearch)
                            helpParam3.P92_DETAIL_SEARCH_CODE = e.EditValue;
                        HelpReturn helpReturn3 = e.Source != CodeHelpEnum.CodeSearch ? (HelpReturn)this.ShowHelp(helpParam3) : (HelpReturn)this.CodeSearch(helpParam3);
                        if (helpReturn3.DialogResult == DialogResult.Cancel)
                        {
                            this._flexM[e.Row, "NM_BGACCT"] = e.OriginValue;
                            break;
                        }
                        if (helpReturn3.DialogResult == DialogResult.OK)
                        {
                            if (this._sTpBudgeAcct == "1" && e.Source == CodeHelpEnum.CodeSearch && !this._biz.연결계정체크(this._sTpBudgetMng, this._flexM[e.Row, "CD_BUDGET"].ToString(), this._flexM[e.Row, "CD_BIZPLAN"].ToString(), helpReturn3.CodeValue, this._flexM[e.Row, "CD_STDACCT"].ToString()))
                            {
                                this.ShowMessage("입력된 계정이 @-회계계정과 연결되지 않은 계정이므로\n사용할 수 없는 계정입니다.", str1);
                                this._flexM[e.Row, "NM_BGACCT"] = e.OriginValue;
                                break;
                            }
                            DataTable dataTable = helpReturn3.DataTable;
                            this.예산관련컬럼셋팅(e.Row, dataTable);
                            this._flexM[e.Row, "NM_BGACCT"] = helpReturn3.CodeName;
                            this._flexM[e.Row, "CD_BGACCT"] = helpReturn3.CodeValue;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                string str1 = string.Empty;
                string 증빙유형 = string.Empty;
                switch (this._flexM.Cols[e.Col].Name)
                {
                    case "NM_EPCODE":
                    case "NM_EVID":
                        string 결의코드;

                        if (this._flexM.Cols[e.Col].Name == "NM_EPCODE")
                        {
                            결의코드 = e.Result.CodeValue;
                            if (string.IsNullOrEmpty(D.GetString(this._flexM["CD_EVID"])))
                            {
                                DataTable dataTable = e.Result.DataTable;
                                if (dataTable.Columns.Contains("YN_USE_EVID") && dataTable.Columns.Contains("NM_EVID"))
                                {
                                    if (string.IsNullOrEmpty(D.GetString(dataTable.Rows[0]["YN_USE_EVID"])))
                                        증빙유형 = string.Empty;
                                    else if (D.GetString(dataTable.Rows[0]["YN_USE_EVID"]) == "Y")
                                    {
                                        증빙유형 = D.GetString(dataTable.Rows[0]["CD_EVID"]);
                                    }
                                    else
                                    {
                                        this.ShowMessage("선택하신 '@'의 증빙유형 '@' 은 사용하지 않는 코드 입니다. \n 결의서환경설정에서 확인해 주세요.", new string[] { e.Result.CodeName, D.GetString(dataTable.Rows[0]["NM_EVID"]) });
                                        e.Cancel = true;
                                    }
                                }
                                else
                                    증빙유형 = D.GetString(dataTable.Rows[0]["CD_EVID"]);
                            }
                            else
                                증빙유형 = D.GetString(this._flexM["CD_EVID"]);
                        }
                        else
                        {
                            증빙유형 = e.Result.CodeValue;
                            결의코드 = D.GetString(this._flexM["CD_EPCODE"]);
                        }

                        if (string.IsNullOrEmpty(결의코드))
                        {
                            this.Set관리항목초기화();
                            this._flexM["TP_TAX"] = D.GetString(e.Result.Rows[0]["TP_TAX"]);
                            break;
                        }

                        if (this._flexM.Cols[e.Col].Name == "NM_EPCODE" && e.OriginValue != e.Result.CodeName)
                            this.Set관리항목초기화();
                        
                        this.Set기초데이타가져오기(결의코드, 증빙유형);
                        
                        if (D.GetString(this._flexM["TP_TAX"]) != "00" && D.GetString(this._flexM["TP_TAX"]) != "" && D.GetString(this._flexM["CD_BIZAREA"]) == "")
                        {
                            this._flexM["CD_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaCode;
                            this._flexM["NM_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaName;
                            this._flexM["NO_BIZAREA"] = Global.MainFrame.LoginInfo.NoBizarea;
                            this.Set관리항목("A01", this._flexM["CD_BIZAREA"].ToString(), this._flexM["NM_BIZAREA"].ToString());
                        }

                        if (this._flexM.Cols[e.Col].Name == "NM_EPCODE" && D.GetInt(Global.MainFrame.ExecuteScalar(@"SELECT COUNT(1) 
                                                                                                                     FROM FI_ACCTCODE WITH(NOLOCK)
                                                                                                                     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_ACCT = '" + D.GetString(this._flexM["CD_STDACCT"]) + "' AND CD_RELATION = '82'")) > 0)
                        {
                            P_CZ_FI_RECEPTRE dialog = new P_CZ_FI_RECEPTRE(this._flexM.GetDataRow(e.Row));
                            dialog.ShowDialog();
                            return;
                        }
                        break;
                    case "NM_BGACCT":
                        if (e.Result.DialogResult != DialogResult.OK)
                            break;
                        if (this._flexM.Cols["NM_BIZPLAN"].Visible)
                            this._flexM.Col = this._flexM.Cols["NM_BIZPLAN"].Index;
                        else
                            this._flexM.Col = this._flexM.Cols["AM_TAXSTD"].Index;
                        break;
                    case "LN_PARTNER":
                        this._flexM["NO_COMPANY"] = D.GetString(e.Result.Rows[0]["NO_COMPANY"]);
                        this.Set관리항목("A06", e.Result.CodeValue, e.Result.CodeName);
                        this.Set관리항목("C01", string.Empty, D.GetString(e.Result.Rows[0]["NO_COMPANY"]));
                        string str2 = e.Result.Rows[0]["NM_DEPOSIT"].ToString();
                        string 관리내역명 = e.Result.Rows[0]["NM_BANK"].ToString() + "/" + e.Result.Rows[0]["NO_DEPOSIT"].ToString() + "/" + str2;
                        this.Set관리항목("C15", e.Result.Rows[0]["CD_DEPOSITNO"].ToString(), 관리내역명);
                        break;
                    case "NO_COMPANY":
                        this.Set관리항목("A06", D.GetString(e.Result.Rows[0]["CD_PARTNER"]), e.Result.CodeName);
                        this.Set관리항목("C01", e.Result.CodeValue, e.Result.CodeValue);
                        break;
                    case "NM_BIZAREA":
                        this.Set관리항목("A01", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_CC":
                        this.Set관리항목("A02", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_DEPT":
                        this.Set관리항목("A03", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_PJT":
                        this.Set관리항목("A05", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NO_DEPOSIT":
                        this.Set관리항목("A07", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_CARD":
                        this.Set관리항목("A08", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_BANK":
                        this.Set관리항목("A09", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_UMNG1":
                    case "NM_UMNG2":
                    case "NM_UMNG3":
                    case "NM_UMNG4":
                    case "NM_UMNG5":
                        this.Set관리항목("A2" + this._flexM.Cols[e.Col].Name.Substring(7, 1), e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_EXCH":
                        this.Set관리항목("B24", e.Result.CodeValue, e.Result.CodeName);
                        break;
                    case "NM_WDEPT":
                        this.Get예산단위(e.Result.CodeValue);
                        break;
                    case "NM_WRITE":
                        DataTable dataTable1 = this._biz.결의사원_추가정보(e.Result.CodeValue);
                        if (dataTable1 != null && dataTable1.Rows.Count > 0)
                        {
                            this._flexM["NM_DUTY_RANK"] = D.GetString(dataTable1.Rows[0]["NM_SYSDEF"]);
                            break;
                        }
                        this._flexM["NM_DUTY_RANK"] = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexM.IsBindingEnd)
                    return;
                if (!this._flexM.HasNormalRow)
                    this.FillDetailData(-1);
                else if (this._flexM.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
                    this.FillDetailData(this._flexM.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (this._flexM.Cols[e.Col].Name)
                {
                    case "DT_ACCT":
                        if (this._flexM.EditData != "" && !this._flexM.IsDate(this._flexM.Cols[this._flexM.Col].Name))
                        {
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            e.Cancel = true;
                            return;
                        }
                        if (MA.기준환율.Option == MA.기준환율옵션.적용안함)
                            return;
                        decimal num1 = MA.기준환율적용(D.GetString(this._flexM["DT_ACCT"]), D.GetString(this._flexM["CD_EXCH"]));
                        this._flexM["RT_EXCH"] = num1;
                        this.Set관리항목("B25", "", D.GetString(num1));
                        break;
                    case "DT_START":
                    case "DT_END":
                        if (this._flexM.EditData != "" && !this._flexM.IsDate(this._flexM.Cols[this._flexM.Col].Name))
                        {
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            e.Cancel = true;
                            return;
                        }
                        break;
                }
                
                string 예산단위 = D.GetString(this._flexM["CD_BUDGET"]);
                string 사업계획 = D.GetString(this._flexM["CD_BIZPLAN"]);
                string 예산계정 = D.GetString(this._flexM["CD_BGACCT"]);
                string 공급가액계정 = D.GetString(this._flexM["CD_STDACCT"]);
                string 년월 = D.GetString(this._flexM["DT_ACCT"]);
                string 예산통제 = D.GetString(this._flexM["TP_BUNIT"]);
                string 예산통제보기 = D.GetString(this._flexM["TP_BMSG"]);
                string ErrMsg = string.Empty;
                string ErrCode = string.Empty;

                switch (this._flexM.Cols[e.Col].Name)
                {
                    case "AM_TAXSTD":
                        string @string = D.GetString(this._flexM["TP_TAX"]);
                        decimal decimal1 = D.GetDecimal(this._flexM.EditData);
                        decimal decimal2 = D.GetDecimal(this._flexM["AM_ADDTAX"]);
                        if (!this._biz.예산조회(예산단위, 사업계획, 예산계정, 년월, decimal1, decimal2, 예산통제, 예산통제보기, 공급가액계정, out ErrCode, out ErrMsg))
                        {
                            e.Cancel = true;
                            this._flexM["AM_TAXSTD"] = 0;
                            if (!(ErrCode != ""))
                                break;
                            if (ErrCode == "예산단위")
                            {
                                MessageBox.Show(ErrMsg);
                                this._flexM.Select(this._flexM.Row, this._flexM.Cols["NM_BUDGET"].Index);
                            }
                            else if (ErrCode == "예산계정")
                            {
                                MessageBox.Show(ErrMsg);
                                this._flexM.Select(this._flexM.Row, this._flexM.Cols["CD_BGACCT"].Index);
                            }
                            break;
                        }
                        if (this.LoginInfo.CompanyLanguage != Language.CH)
                        {
                            if (this._biz.Get_환경설정_TP_VATINPUT() == "0")
                            {
                                decimal pdAmt;
                                switch (@string)
                                {
                                    case "11":
                                    case "12":
                                    case "13":
                                    case "14":
                                    case "18":
                                    case "1B":
                                    case "21":
                                    case "22":
                                    case "24":
                                    case "31":
                                    case "38":
                                    case "43":
                                    case "50":
                                        pdAmt = decimal1 * new decimal(1, 0, 0, false, (byte)1);
                                        break;
                                    case "27":
                                    case "28":
                                    case "34":
                                        pdAmt = decimal1 * new decimal(-247381695, 273088523, 10629433, false, (byte)28);
                                        break;
                                    case "29":
                                    case "30":
                                    case "35":
                                        pdAmt = decimal1 * new decimal(1258779857, 113764787, 15789352, false, (byte)28);
                                        break;
                                    case "51":
                                    case "52":
                                    case "53":
                                        pdAmt = decimal1 * new decimal(-2137159207, -951045806, 20850041, false, (byte)28);
                                        break;
                                    case "32":
                                    case "33":
                                    case "36":
                                        pdAmt = decimal1 * new decimal(-600784116, 1890348499, 25814337, false, (byte)28);
                                        break;
                                    case "40":
                                    case "41":
                                    case "42":
                                    case "44":
                                    case "45":
                                        pdAmt = decimal1 * new decimal(1230751242, 626275652, 30684967, false, (byte)28);
                                        break;
                                    case "48":
                                    case "49":
                                        pdAmt = decimal1 * new decimal(974321285, 77230580, 40155636, false, (byte)28);
                                        break;
                                    case "46":
                                    case "47":
                                        pdAmt = decimal1 * new decimal(1976661085, -295668588, 49281916, false, (byte)28);
                                        break;
                                    default:
                                        pdAmt = 0;
                                        break;
                                }
                                Decimal num2 = this.m_funcLib.ReturnWonUnit(pdAmt, "001");
                                if (@string == "38")
                                    num2 = Decimal.Truncate(num2 / new decimal(10)) * new decimal(10);
                                this._flexM["AM_ADDTAX"] = num2;
                            }
                            this._flexM["AM_SUM"] = (decimal1 + D.GetDecimal(this._flexM["AM_ADDTAX"]));
                            break;
                        }
                        Decimal val;
                        if (this.DicFlag.ContainsKey(@string))
                        {
                            Decimal num2 = this.DicFlag[@string];
                            val = decimal1 * num2 / new Decimal(100);
                        }
                        else
                            val = decimal1 * new Decimal(1, 0, 0, false, (byte)1);
                        Decimal num6 = UDecimal.ConvertToCustom2(val, 올림구분.반올림, 단위.소수점셋째자리, false);
                        this._flexM["AM_ADDTAX"] = num6;
                        this._flexM["AM_SUM"] = (decimal1 + num6);
                        break;
                    case "AM_ADDTAX":
                        Decimal decimal3 = D.GetDecimal(this._flexM["AM_TAXSTD"]);
                        Decimal decimal4 = D.GetDecimal(this._flexM.EditData);
                        if (!this._biz.예산조회(예산단위, 사업계획, 예산계정, 년월, decimal3, decimal4, 예산통제, 예산통제보기, 공급가액계정, out ErrCode, out ErrMsg))
                        {
                            if (ErrCode != "")
                            {
                                if (ErrCode == "예산단위")
                                {
                                    MessageBox.Show(ErrMsg);
                                    this._flexM.Select(this._flexM.Row, this._flexM.Cols["NM_BUDGET"].Index);
                                }
                                else if (ErrCode == "예산계정")
                                {
                                    MessageBox.Show(ErrMsg);
                                    this._flexM.Select(this._flexM.Row, this._flexM.Cols["CD_BGACCT"].Index);
                                }
                            }
                            this._flexM["AM_ADDTAX"] = this._flexM.GetData(e.Row, e.Col).ToString();
                            e.Cancel = true;
                            break;
                        }
                        this._flexM["AM_SUM"] = (D.GetDecimal(this._flexM["AM_TAXSTD"]) + D.GetDecimal(this._flexM.EditData));
                        break;
                    case "DT_END":
                        this.Set관리항목("B22", this._flexM.EditData, this._flexM.EditData);
                        this.Set관리항목("B23", this._flexM.EditData, this._flexM.EditData);
                        break;
                    case "RT_EXCH":
                        this.Set관리항목("B25", this._flexM.EditData, this._flexM.EditData);
                        break;
                    case "AM_EX":
                        this.Set관리항목("B26", this._flexM.EditData, this._flexM.EditData);
                        break;
                    case "DT_START":
                        this.Set관리항목("B21", this._flexM.EditData, this._flexM.EditData);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flexM.HasNormalRow == false) return;
                if (this._flexM.MouseRow < this._flexM.Rows.Fixed) return;

                if (this._flexM.Rows[this._flexM.Row].IsNode)
                    return;

                if (this._flexM.Cols[this._flexM.Col].Name == "TP_RESULT" && D.GetString(this._flexM["TP_RESULT"]) == "오류")
                    new P_CZ_FI_EPDOCU_MSG(this._dt오류메세지, D.GetString(this._flexM["ROW_ID"])).Show();
                
                if ((this._flexM.Cols[this._flexM.Col].Name == "TP_DOCU" || this._flexM.Cols[this._flexM.Col].Name == "NO_DOCU") && D.GetString(this._flexM["TP_DOCU"]) != "0")
                    this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { D.GetString(this._flexM["NO_DOCU"]),
                                                                                                                        "0",
                                                                                                                        D.GetString(this._flexM["CD_PC"]),
                                                                                                                        this.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!(this._flexM.Cols[this._flexM.Col].Name == "DT_ACCT"))
                    return;
                PopupCalendar popupCalendar = new PopupCalendar(DateTime.Today);
                if (popupCalendar.ShowDialog() == DialogResult.OK)
                    this._flexM[this._flexM.Row, "DT_ACCT"] = popupCalendar.GetYMD(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_CellContentChanged(object sender, CellContentEventArgs e)
        {
            string str = string.Empty;
            if (e.ContentType == ContentType.Memo)
            {
                if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                {
                    string query = string.Format("UPDATE FI_EPDOCU SET MEMO_CD = '{0}' WHERE CD_COMPANY = '{1}' AND ROW_ID = '{2}'", e.SettingValue.Replace("'", ""), this.LoginInfo.CompanyCode, this._flexM[e.Row, "ROW_ID"]);
                    Global.MainFrame.ExecuteScalar(query);
                }
                else
                {
                    if (e.CommandType != Dass.FlexGrid.CommandType.Delete)
                        return;
                    string query = string.Format("UPDATE FI_EPDOCU SET MEMO_CD = NULL WHERE CD_COMPANY = '{1}' AND ROW_ID = '{2}'", e.SettingValue.Replace("'", ""), this.LoginInfo.CompanyCode, this._flexM[e.Row, "ROW_ID"]);
                    Global.MainFrame.ExecuteScalar(query);
                }
            }
            else
            {
                if (e.ContentType != ContentType.CheckPen)
                    return;
                if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                {
                    string query = string.Format("UPDATE FI_EPDOCU SET CHECK_PEN = '{0}' WHERE CD_COMPANY = '{1}' AND ROW_ID = '{2}'", e.SettingValue, this.LoginInfo.CompanyCode, this._flexM[e.Row, "ROW_ID"]);
                    Global.MainFrame.ExecuteScalar(query);
                }
                else if (e.CommandType == Dass.FlexGrid.CommandType.Delete)
                {
                    string query = string.Format("UPDATE FI_EPDOCU SET CHECK_PEN = NULL WHERE CD_COMPANY = '{1}' AND ROW_ID = '{2}'", e.SettingValue, this.LoginInfo.CompanyCode, this._flexM[e.Row, "ROW_ID"]);
                    Global.MainFrame.ExecuteScalar(query);
                }
            }
        }

        private void _flexLR_CodeHelp(object sender, CodeHelpEventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow || D.GetString(this._flexM["TP_DOCU"]) != "0")
                    return;
                FlexGrid flexGrid = (FlexGrid)sender;
                string name = flexGrid.Cols[e.Col].Name;
                int num1 = flexGrid.Name.Equals("_flexL") ? e.Row : 5 + e.Row;
                string index1 = "CD_MNGD" + num1.ToString();
                string index2 = "NM_MNGD" + num1.ToString();
                string str1 = "";
                string str2 = this._flexM["CD_MNG" + num1.ToString()].ToString();
                string str3 = this._flexM["YN_CDMNG" + num1.ToString()].ToString();
                str1 = this._flexM["TP_MNGFORM" + num1.ToString()].ToString();
                if (str2 == "" || str3 != "Y")
                    return;
                string str4 = str2;
                if (str2.Substring(0, 2) == "A2" || str2.Substring(0, 1) != "A" && str2.Substring(0, 1) != "B" && str2.Substring(0, 1) != "C")
                    str4 = "DDD";
                if (!(str4 == "A03") && !(str4 == "A04") && (!(str4 == "A07") && !(str4 == "DDD")) && (!(str4 == "B24") && !(str4 == "A06") && (!(str4 == "B05") && !(str4 == "A02"))) && (!(str4 == "A05") && !(str4 == "B02") && (!(str4 == "B12") && !(str4 == "A01")) && (!(str4 == "A08") && !(str4 == "A10") && (!(str4 == "B01") && !(str4 == "B04")))) && (!(str4 == "B11") && !(str4 == "B13") && (!(str4 == "B27") && !(str4 == "A09")) && (!(str4 == "C03") && !(str4 == "C13") && (!(str4 == "B03") && !(str4 == "C14"))) && (!(str4 == "C18") && !(str4 == "C19") && (!(str4 == "C28") && !(str4 == "C31")) && (!(str4 == "B08") && !(str4 == "C22")))) && !(str4 == "C15"))
                    return;
                HelpParam helpParam = (HelpParam)null;
                if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue.Equals(""))
                {
                    flexGrid[e.Row, "CD_MNGD"] = "";
                    flexGrid[e.Row, "NM_MNGD"] = "";
                    this._flexM[this._flexM.Row, index1] = "";
                    this._flexM[this._flexM.Row, index2] = "";
                }
                else
                {
                    switch (str4)
                    {
                        case "A01":
                            helpParam = new HelpParam(HelpID.P_MA_BIZAREA_SUB, this.MainFrameInterface);
                            break;
                        case "A02":
                            helpParam = new HelpParam(HelpID.P_MA_CC_SUB, this.MainFrameInterface);
                            helpParam.UseAccessGrant = false;
                            helpParam.P12_CD_ITEM = this.관리내역(this._flexM.Row, "C31")[0];
                            break;
                        case "A05":
                            helpParam = new HelpParam(HelpID.P_SA_PROJECT_SUB, this.MainFrameInterface);
                            break;
                        case "A06":
                            helpParam = new HelpParam(HelpID.P_MA_PARTNER_SUB, this.MainFrameInterface);
                            if (this.LoginInfo.YnAcctJo == "Y")
                            {
                                helpParam.ResultMode = ResultMode.SlowMode;
                                break;
                            }
                            break;
                        case "A10":
                            helpParam = new HelpParam(HelpID.P_MA_ITEM_SUB, this.MainFrameInterface);
                            break;
                        case "A08":
                            helpParam = new HelpParam(HelpID.P_FI_CARD_SUB, this.MainFrameInterface);
                            break;
                        case "B01":
                            helpParam = new HelpParam(HelpID.P_FI_ASSET_SUB, this.MainFrameInterface);
                            break;
                        case "B02":
                            helpParam = new HelpParam(HelpID.P_FI_BILLREC_SUB, this.MainFrameInterface);
                            break;
                        case "B03":
                            helpParam = new HelpParam(HelpID.P_FI_BILLDEBT_SUB, this.MainFrameInterface);
                            break;
                        case "B04":
                            helpParam = new HelpParam(HelpID.P_FI_LOAN_SUB, this.MainFrameInterface);
                            break;
                        case "B05":
                            helpParam = new HelpParam(HelpID.P_FI_STOCK_SUB, this.MainFrameInterface);
                            break;
                        case "B11":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "FI_A000004";
                            break;
                        case "B12":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "FI_F000006";
                            break;
                        case "B13":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "FI_F000007";
                            break;
                        case "B24":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "PU_C000004";
                            break;
                        case "B27":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "FI_J000005";
                            break;
                        case "C03":
                            helpParam = new HelpParam(HelpID.P_FI_ACCTCODE_SUB, this.MainFrameInterface);
                            helpParam.P33_TP_ACSTATS = "";
                            helpParam.P32_TP_ACLEVEL = "";
                            break;
                        case "C13":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "";
                            break;
                        case "C14":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = !(D.GetString(this._flexM["TP_EPNOTE"]) == "1") ? "MA_B000040" : "MA_B000046";
                            break;
                        case "C18":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "FI_T000002";
                            break;
                        case "C19":
                            helpParam = new HelpParam(HelpID.P_HR_WTETC_SUB, this.MainFrameInterface);
                            break;
                        case "DDD":
                            helpParam = new HelpParam(HelpID.P_FI_MNGD_SUB, this.MainFrameInterface);
                            helpParam.P34_CD_MNG = str2;
                            break;
                        case "A07":
                            helpParam = new HelpParam(HelpID.P_FI_DEPOSIT_SUB, this.MainFrameInterface);
                            helpParam.P14_CD_PARTNER = !flexGrid[e.Row - 1, "NM_MNG"].ToString().Equals("금융기관") ? "" : flexGrid[e.Row - 1, "CD_MNGD"].ToString();
                            break;
                        case "A04":
                            helpParam = new HelpParam(HelpID.P_MA_EMP_SUB, this.MainFrameInterface);
                            break;
                        case "A09":
                            helpParam = new HelpParam(HelpID.P_MA_BANK_SUB, this.MainFrameInterface);
                            break;
                        case "A03":
                            helpParam = new HelpParam(HelpID.P_MA_DEPT_SUB, this.MainFrameInterface);
                            helpParam.UseAccessGrant = false;
                            break;
                        case "C31":
                            helpParam = new HelpParam(HelpID.P_MA_ITEMGP_SUB, this.MainFrameInterface);
                            helpParam.P06_CD_CC = this.관리내역(this._flexM.Row, "A02")[0];
                            break;
                        case "C28":
                            helpParam = new HelpParam(HelpID.P_USER);
                            helpParam.UserHelpID = "H_FI_HELP01";
                            helpParam.UserParams = "지급조건;H_FI_PAYMENT_SUB;";
                            break;
                        case "B08":
                            if (flexGrid["CD_RELATION"].ToString().Equals("71") || flexGrid["CD_RELATION"].ToString().Equals("70"))
                                return;
                            helpParam = new HelpParam(HelpID.P_FI_LEAD_SUB, this.MainFrameInterface);
                            break;
                        case "C22":
                            helpParam = new HelpParam(HelpID.P_MA_CODE_SUB, this.MainFrameInterface);
                            helpParam.P41_CD_FIELD1 = "SA_B000002";
                            break;
                        case "C15":
                            string str5 = this.관리내역(this._flexM.Row, "A06")[0];
                            if (str5 == "")
                            {
                                flexGrid[e.Row, "CD_MNGD"] = "";
                                return;
                            }
                            helpParam = new HelpParam(HelpID.P_USER);
                            helpParam.UserHelpID = "H_MA_PARTNER_DEPOSIT";
                            helpParam.UserParams = "H_MA_PARTNER_DEPOSIT;";
                            helpParam.P41_CD_FIELD1 = "거래처계좌도움창";
                            helpParam.P14_CD_PARTNER = str5;
                            break;
                    }
                    if (e.Source == CodeHelpEnum.CodeSearch)
                        helpParam.P92_DETAIL_SEARCH_CODE = e.EditValue;
                    if (str4 == "C28")
                    {
                        H_FI_HELP01 hFiHelP01 = new H_FI_HELP01(helpParam);
                        if (hFiHelP01.ShowDialog() != DialogResult.OK)
                            return;
                        DataRow[] returnValue = hFiHelP01.ReturnValue;
                        flexGrid[e.Row, "CD_MNGD"] = returnValue[0]["CODE"];
                        flexGrid[e.Row, "NM_MNGD"] = returnValue[0]["NAME"];
                        this._flexM[this._flexM.Row, index1] = returnValue[0]["CODE"];
                        this._flexM[this._flexM.Row, index2] = returnValue[0]["NAME"];
                    }
                    else if (str4 == "C15")
                    {
                        if (e.Source == CodeHelpEnum.CodeSearch)
                            helpParam.P92_DETAIL_SEARCH_CODE = e.EditValue;
                        H_MA_PARTNER_DEPOSIT maPartnerDeposit = new H_MA_PARTNER_DEPOSIT(helpParam);
                        if (maPartnerDeposit.ShowDialog() == DialogResult.OK)
                        {
                            DataRow[] returnValue = maPartnerDeposit.ReturnValue;
                            string str6 = returnValue[0]["CD_DEPOSITNO"].ToString();
                            string str7 = returnValue[0]["NAME"].ToString();
                            flexGrid[e.Row, "CD_MNGD"] = flexGrid[flexGrid.Row, index1] = str6;
                            flexGrid[e.Row, "NM_MNGD"] = flexGrid[flexGrid.Row, index2] = str7;
                        }
                        else
                            flexGrid[e.Row, "CD_MNGD"] = e.OriginValue;
                    }
                    else
                    {
                        HelpReturn helpReturn = e.Source != CodeHelpEnum.CodeSearch ? (HelpReturn)this.ShowHelp(helpParam) : (HelpReturn)this.CodeSearch(helpParam);
                        if (helpReturn.DialogResult == DialogResult.Cancel)
                        {
                            flexGrid[e.Row, "CD_MNGD"] = e.OriginValue;
                        }
                        else
                        {
                            if (helpReturn.DialogResult != DialogResult.OK)
                                return;
                            if (str4 == "A07")
                            {
                                flexGrid[e.Row, "CD_MNGD"] = helpReturn.CodeValue;
                                flexGrid[e.Row, "NM_MNGD"] = helpReturn.Rows[0]["NO_DEPOSIT"].ToString();
                                this._flexM[this._flexM.Row, index1] = helpReturn.CodeValue;
                                this._flexM[this._flexM.Row, index2] = helpReturn.Rows[0]["NO_DEPOSIT"].ToString();
                            }
                            else
                            {
                                flexGrid[e.Row, "CD_MNGD"] = helpReturn.CodeValue;
                                flexGrid[e.Row, "NM_MNGD"] = helpReturn.CodeName;
                                this._flexM[this._flexM.Row, index1] = helpReturn.CodeValue;
                                this._flexM[this._flexM.Row, index2] = helpReturn.CodeName;
                            }
                            string str6 = string.Empty;
                            string str7 = string.Empty;
                            string str8 = string.Empty;
                            switch (str2)
                            {
                                case "A01":
                                    this._flexM[this._flexM.Row, "CD_BIZAREA"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_BIZAREA"] = helpReturn.CodeName;
                                    this._flexM[this._flexM.Row, "NO_BIZAREA"] = D.GetString(helpReturn.Rows[0]["NO_BIZAREA"]);
                                    break;
                                case "A02":
                                    this._flexM[this._flexM.Row, "CD_CC"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_CC"] = helpReturn.CodeName;
                                    break;
                                case "A03":
                                    this._flexM[this._flexM.Row, "CD_DEPT"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_DEPT"] = helpReturn.CodeName;
                                    break;
                                case "A04":
                                    this._flexM[this._flexM.Row, "CD_EMPLOY"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_EMP"] = helpReturn.CodeName;
                                    this.Set관리항목("C15", string.Empty, D.GetString(helpReturn.Rows[0]["CD_BANK2"]) + "/" + D.GetString(helpReturn.Rows[0]["NM_BANK2"]) + "/" + D.GetString(helpReturn.Rows[0]["NO_BANK2"]) + "/" + D.GetString(helpReturn.Rows[0]["NM_DEPOSIT2"]));
                                    break;
                                case "A05":
                                    this._flexM[this._flexM.Row, "CD_PJT"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_PJT"] = helpReturn.CodeName;
                                    break;
                                case "A06":
                                    this._flexM[this._flexM.Row, "CD_PARTNER"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "LN_PARTNER"] = helpReturn.CodeName;
                                    this._flexM[this._flexM.Row, "NO_COMPANY"] = D.GetString(helpReturn.Rows[0]["NO_COMPANY"]);
                                    string str9 = helpReturn.DataTable.Rows[0]["NM_DEPOSIT"].ToString();
                                    string 관리내역명 = helpReturn.DataTable.Rows[0]["NM_BANK"].ToString() + "/" + helpReturn.DataTable.Rows[0]["NO_DEPOSIT"].ToString() + "/" + str9;
                                    this.Set관리항목("C15", helpReturn.DataTable.Rows[0]["CD_DEPOSITNO"].ToString(), 관리내역명);
                                    break;
                                case "A07":
                                    this._flexM[this._flexM.Row, "CD_DEPOSIT"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NO_DEPOSIT"] = D.GetString(helpReturn.Rows[0]["NO_DEPOSIT"]);
                                    break;
                                case "A08":
                                    this._flexM[this._flexM.Row, "CD_CARD"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_CARD"] = helpReturn.CodeName;
                                    break;
                                case "A09":
                                    this._flexM[this._flexM.Row, "CD_BANK"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_BANK"] = helpReturn.CodeName;
                                    break;
                                case "A21":
                                    this._flexM[this._flexM.Row, "CD_UMNG1"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_UMNG1"] = helpReturn.CodeName;
                                    break;
                                case "A22":
                                    this._flexM[this._flexM.Row, "CD_UMNG2"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_UMNG2"] = helpReturn.CodeName;
                                    break;
                                case "A23":
                                    this._flexM[this._flexM.Row, "CD_UMNG3"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_UMNG3"] = helpReturn.CodeName;
                                    break;
                                case "A24":
                                    this._flexM[this._flexM.Row, "CD_UMNG4"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_UMNG4"] = helpReturn.CodeName;
                                    break;
                                case "A25":
                                    this._flexM[this._flexM.Row, "CD_UMNG5"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_UMNG5"] = helpReturn.CodeName;
                                    break;
                                case "B24":
                                    this._flexM[this._flexM.Row, "CD_EXCH"] = helpReturn.CodeValue;
                                    this._flexM[this._flexM.Row, "NM_EXCH"] = helpReturn.CodeName;
                                    if (MA.기준환율.Option == MA.기준환율옵션.적용안함)
                                        break;
                                    Decimal num2 = MA.기준환율적용(D.GetString(this._flexM["DT_ACCT"]), helpReturn.CodeValue);
                                    this._flexM["RT_EXCH"] = num2;
                                    this.Set관리항목("B25", "", D.GetString(num2));
                                    break;
                                case "C01":
                                    this._flexM[this._flexM.Row, "CD_PARTNER"] = D.GetString(helpReturn.Rows[0]["CD_PARTNER"]);
                                    this._flexM[this._flexM.Row, "LN_PARTNER"] = D.GetString(helpReturn.Rows[0]["NM_PARTNER"]);
                                    this._flexM[this._flexM.Row, "NO_COMPANY"] = D.GetString(helpReturn.Rows[0]["NO_COMPANY"]);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexLR_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow || D.GetString(this._flexM["TP_DOCU"]) != "0")
                    return;
                FlexGrid flex = (FlexGrid)sender;
                if (!flex.HasNormalRow)
                    return;
                string cd_code = "";
                switch (flex.HelpColName)
                {
                    case "CD_MNGD":
                    case "NM_MNGD":
                        string cd_mng;
                        string yn_mng;
                        string tp_mngform;
                        if (flex.Name == "_flexL")
                        {
                            FlexGrid flexGrid1 = this._flexM;
                            string str1 = "CD_MNG";
                            int helpRow = flex.HelpRow;
                            string str2 = helpRow.ToString();
                            string index1 = str1 + str2;
                            cd_mng = flexGrid1[index1].ToString() ?? "";
                            FlexGrid flexGrid2 = this._flexM;
                            string str3 = "YN_CDMNG";
                            helpRow = flex.HelpRow;
                            string str4 = helpRow.ToString();
                            string index2 = str3 + str4;
                            yn_mng = flexGrid2[index2].ToString();
                            FlexGrid flexGrid3 = this._flexM;
                            string str5 = "TP_MNGFORM";
                            helpRow = flex.HelpRow;
                            string str6 = helpRow.ToString();
                            string index3 = str5 + str6;
                            tp_mngform = flexGrid3[index3].ToString();
                        }
                        else
                        {
                            FlexGrid flexGrid1 = this._flexM;
                            string str1 = "CD_MNG";
                            int num = 5 + flex.HelpRow;
                            string str2 = num.ToString();
                            string index1 = str1 + str2;
                            cd_mng = flexGrid1[index1].ToString() ?? "";
                            FlexGrid flexGrid2 = this._flexM;
                            string str3 = "YN_CDMNG";
                            num = 5 + flex.HelpRow;
                            string str4 = num.ToString();
                            string index2 = str3 + str4;
                            yn_mng = flexGrid2[index2].ToString();
                            FlexGrid flexGrid3 = this._flexM;
                            string str5 = "TP_MNGFORM";
                            num = 5 + flex.HelpRow;
                            string str6 = num.ToString();
                            string index3 = str5 + str6;
                            tp_mngform = flexGrid3[index3].ToString();
                        }
                        if (cd_mng == "")
                            break;
                        string str = cd_mng;
                        if (cd_mng.Substring(0, 2) == "A2" || cd_mng.Substring(0, 1) != "A" && cd_mng.Substring(0, 1) != "B" && cd_mng.Substring(0, 1) != "C")
                            str = "DDD";
                        if (str == "A03" || str == "A09" || (str == "A04" || str == "A07") || (str == "DDD" || str == "B24" || (str == "A06" || str == "B05")) || (str == "A02" || str == "A05" || (str == "B02" || str == "B03") || (str == "B12" || str == "A01" || (str == "A08" || str == "A10"))) || (str == "B01" || str == "B04" || (str == "B11" || str == "B13") || (str == "B27" || str == "C03" || (str == "C13" || str == "B08"))))
                            break;
                        if (yn_mng == "Y")
                            cd_code = flex[flex.HelpRow, "CD_MNGD"].ToString();
                        else if (tp_mngform == "1")
                            cd_code = flex[flex.HelpRow, "NM_MNGD"].ToString();
                        if (yn_mng == "Y" || tp_mngform == "1")
                            this.FlexLRShowHelp(flex, true, cd_mng, tp_mngform, yn_mng, cd_code);
                        flex.Focus();
                        SendKeys.SendWait("{ENTER}");
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexLR_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = (FlexGrid)sender;
                int num1 = flexGrid.Name.Equals("_flexL") ? e.Row : 5 + e.Row;
                if (e.Checkbox == CheckEnum.None && flexGrid.EditData != flexGrid[e.Row, e.Col].ToString())
                {
                    switch (flexGrid.Cols[e.Col].Name)
                    {
                        case "NM_MNGD":
                            this._flexM["NM_MNGD" + num1.ToString()] = flexGrid.EditData.Replace("-", "").Replace("/", "");
                            if (this._flexM["TP_MNGFORM" + num1.ToString()].ToString() == "1" && (flexGrid.EditData != "" && !this._flexM.IsDate("NM_MNGD" + num1.ToString())))
                            {
                                this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                                e.Cancel = true;
                                this._flexM["NM_MNGD" + num1.ToString()] = "";
                                flexGrid[e.Row, "NM_MNGD"] = "";
                                break;
                            }
                            switch (this._flexM["CD_MNG" + num1.ToString()].ToString())
                            {
                                case "B22":
                                case "B23":
                                    this._flexM["DT_END"] = flexGrid.EditData.ToString().Replace("-", "").Replace("/", "");
                                    break;
                                case "B25":
                                    this._flexM["RT_EXCH"] = D.GetDecimal(flexGrid.EditData.ToString().Replace(",", ""));
                                    break;
                                case "B26":
                                    this._flexM["AM_EX"] = D.GetDecimal(flexGrid.EditData.ToString().Replace(",", ""));
                                    break;
                                case "A06":
                                    this._flexM["LN_PARTNER"] = D.GetString(flexGrid.EditData.ToString().Replace(",", ""));
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

        private void _flexLR_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = (FlexGrid)sender;
                e.Handled = false;
                if (!this._flexM.HasNormalRow || !flexGrid.HasNormalRow || (this._flexM.Row <= this._flexM.Rows.Fixed - 1 || e.Row <= flexGrid.Rows.Fixed - 1 || !flexGrid.Cols[e.Col].Name.Equals("NM_MNGD")))
                    return;
                int num = flexGrid.Name.Equals("_flexL") ? e.Row : 5 + e.Row;
                string string1 = D.GetString(this._flexM["TP_MNGFORM" + num.ToString()]);
                string string2 = D.GetString(this._flexM["CD_MNG" + num.ToString()]);
                Brush brush1 = (Brush)new SolidBrush(e.Style.BackColor);
                Brush brush2 = (Brush)new SolidBrush(e.Style.ForeColor);
                Pen pen = new Pen(e.Style.Border.Color);
                pen.DashStyle = e.Style.Border.Style == BorderStyleEnum.Dotted ? DashStyle.Dot : DashStyle.Dot;
                Rectangle rectangle = e.Bounds;
                int x = rectangle.Left - 1;
                rectangle = e.Bounds;
                int y = rectangle.Top - 1;
                rectangle = e.Bounds;
                int width = rectangle.Width;
                rectangle = e.Bounds;
                int height = rectangle.Height;
                Rectangle rect = new Rectangle(x, y, width, height);
                StringFormat format = new StringFormat();
                try
                {
                    string str1 = string.Empty;
                    switch (string1)
                    {
                        case "1":
                            string sDate = e.Text.Replace("/", string.Empty).Replace("-", string.Empty).Replace(",", string.Empty).Replace("%", string.Empty);
                            if (this._docu.HfxIsDate(sDate))
                            {
                                string str2 = sDate.Substring(0, 4);
                                string str3 = sDate.Substring(4, 2);
                                string str4 = sDate.Substring(6, 2);
                                string str5 = this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT).Substring(5, 1);
                                e.Text = string.Format("{0:000#}" + str5 + "{1:0#}" + str5 + "{2:0#}", str2, str3, str4);
                                e.Graphics.FillRectangle(brush1, rect);
                                e.Graphics.DrawRectangle(pen, rect);
                                format.Alignment = StringAlignment.Center;
                                format.LineAlignment = StringAlignment.Center;
                                e.Graphics.DrawString(e.Text, flexGrid.Font, brush2, (RectangleF)rect, format);
                                e.Handled = true;
                                break;
                            }
                            break;
                        case "2":
                        case "3":
                        case "4":
                            string s = e.Text.Replace("/", string.Empty).Replace(",", string.Empty).Replace("%", string.Empty);
                            if (s != null && !s.Equals(string.Empty))
                            {
                                string str2 = string.Empty;
                                string str3 = !string1.Equals("4") ? this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.MONEY, FormatFgType.SELECT) : this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.EXCHANGE_RATE, FormatFgType.SELECT);
                                if (string2.Equals("B26"))
                                    str3 = this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.FOREIGN_MONEY, FormatFgType.SELECT);
                                e.Text = string.Format("{0:" + str3 + "}", Decimal.Parse(s));
                            }
                            e.Graphics.FillRectangle(brush1, rect);
                            e.Graphics.DrawRectangle(pen, rect);
                            format.Alignment = StringAlignment.Far;
                            format.LineAlignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Text, flexGrid.Font, brush2, (RectangleF)rect, format);
                            e.Handled = true;
                            break;
                        default:
                            e.Graphics.FillRectangle(brush1, rect);
                            e.Graphics.DrawRectangle(pen, rect);
                            format.Alignment = StringAlignment.Near;
                            format.LineAlignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Text, flexGrid.Font, brush2, (RectangleF)rect, format);
                            e.Handled = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    brush1.Dispose();
                    brush2.Dispose();
                    pen.Dispose();
                    format.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexLR_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid flex = (FlexGrid)sender;
                string @string = D.GetString(flex["CD_MNG"]);
                if (!this._flexM.HasNormalRow)
                    e.Cancel = true;
                if (D.GetString(this._flexM["TP_DOCU"]) != "0")
                    e.Cancel = true;
                if (D.GetString(flex["CD_MNG"]) == "B25" && MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    e.Cancel = true;
                else if (flex.Cols[e.Col].Name == "NM_MNGD" && flex[e.Row, "CD_MNGD"].ToString().Equals("00"))
                    this.StartEdit_Format(this._flexM, flex, e, @string);
                else if (e.Cancel)
                    this.StartEdit_Format(this._flexM, flex, e, @string);
                else
                    this.StartEdit_Format(this._flexM, flex, e, @string);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void StartEdit_Format(FlexGrid _flex, FlexGrid flex, RowColEventArgs e, string cd_mng)
        {
            if (!flex.Cols[e.Col].Name.Equals("NM_MNGD"))
                return;
            int num = flex.Name.Equals("_flexL") ? e.Row : 5 + e.Row;
            string @string = D.GetString(_flex["TP_MNGFORM" + num.ToString()]);
            if (this.IsNumeric(@string) && D.GetInt(@string) > 0)
            {
                switch (@string)
                {
                    case "1":
                        flex.SetColChange("NM_MNGD", 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        break;
                    case "2":
                        if (cd_mng == "B26")
                        {
                            flex.SetColChange("NM_MNGD", 19.4, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                            break;
                        }
                        flex.SetColChange("NM_MNGD", 19.4, true, typeof(decimal), FormatTpType.MONEY);
                        break;
                    case "3":
                        flex.SetColChange("NM_MNGD", 17.4, true, typeof(decimal), FormatTpType.QUANTITY);
                        break;
                    case "4":
                        flex.SetColChange("NM_MNGD", 17.4, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                        break;
                    default:
                        flex.SetColChange("NM_MNGD", -1, true, typeof(string));
                        break;
                }
            }
            else
                flex.SetColChange("NM_MNGD", -1, true, typeof(string));
        }

        private void FlexLRShowHelp(FlexGrid flex, bool bHelpClick, string cd_mng, string tp_mngform, string yn_mng, string cd_code)
        {
            object obj1 = null;
            string str1 = cd_mng;
            int index1 = !bHelpClick ? flex.Row : flex.HelpRow;
            if (tp_mngform == "1")
            {
                obj1 = this.LoadHelpWindow("P_PR_CALENDAR", new object[2]
        {
           this.MainFrameInterface,
           cd_code
        });
            }
            else
            {
                if (cd_mng.Substring(0, 2) == "A2" || cd_mng.Substring(0, 1) != "A" && cd_mng.Substring(0, 1) != "B" && cd_mng.Substring(0, 1) != "C")
                    str1 = "DDD";
                switch (str1)
                {
                    case "B06":
                        obj1 = this.LoadHelpWindow("P_TR_LC_NO", new object[1]
            {
               this.MainFrameInterface
            });
                        break;
                    case "B25":
                        obj1 = this.LoadHelpWindow("P_FI_HELPCOM", new object[4]
            {
               this.MainFrameInterface,
               yn_mng,
               19,
               "B27"
            });
                        break;
                    case "C01":
                        obj1 = this.LoadHelpWindow("P_FI_HELPCOM", new object[4]
            {
               this.MainFrameInterface,
               "",
               "",
               20
            });
                        break;
                    case "DDD":
                        obj1 = this.LoadHelpWindow("P_FI_HELPCOM", new object[4]
            {
               this.MainFrameInterface,
               "",
               cd_mng,
               25
            });
                        break;
                }
            }
            if (obj1 == null || ((Form)obj1).ShowDialog() != DialogResult.OK || !(obj1 is IHelpWindow))
                return;
            if (yn_mng == "Y")
            {
                flex[index1, "CD_MNGD"] = ((IHelpWindow)obj1).ReturnValues[0];
                flex[index1, "NM_MNGD"] = ((IHelpWindow)obj1).ReturnValues[1];
            }
            else
                flex[index1, "NM_MNGD"] = !(tp_mngform != "1") ? ((IHelpWindow)obj1).ReturnValues[0].ToString().Replace("/", "") : ((IHelpWindow)obj1).ReturnValues[0];
            switch (cd_mng)
            {
                case " B21":
                    this._flexM["DT_START"] = flex[index1, "NM_MNGD"];
                    break;
                case " B22":
                case " B23":
                    this._flexM["DT_END"] = flex[index1, "NM_MNGD"];
                    break;
                case " B25":
                    this._flexM["RT_EXCH"] = D.GetDecimal(flex[index1, "NM_MNGD"].ToString().Replace(",", ""));
                    break;
                case " C01":
                    this._flexM["CD_PARTNER"] = D.GetString(((IHelpWindow)obj1).ReturnValues[0]);
                    this._flexM["LN_PARTNER"] = D.GetString(((IHelpWindow)obj1).ReturnValues[1]);
                    this._flexM["NO_COMPANY"] = D.GetString(((IHelpWindow)obj1).ReturnValues[2]);
                    break;
                case "A21":
                    this._flexM["CD_UMNG1"] = flex[index1, "CD_MNGD"];
                    this._flexM["NM_UMNG1"] = flex[index1, "NM_MNGD"];
                    break;
                case "A22":
                    this._flexM["CD_UMNG2"] = flex[index1, "CD_MNGD"];
                    this._flexM["NM_UMNG2"] = flex[index1, "NM_MNGD"];
                    break;
                case "A23":
                    this._flexM["CD_UMNG3"] = flex[index1, "CD_MNGD"];
                    this._flexM["NM_UMNG3"] = flex[index1, "NM_MNGD"];
                    break;
                case "A24":
                    this._flexM["CD_UMNG4"] = flex[index1, "CD_MNGD"];
                    this._flexM["NM_UMNG4"] = flex[index1, "NM_MNGD"];
                    break;
                case "A25":
                    this._flexM["CD_UMNG5"] = flex[index1, "CD_MNGD"];
                    this._flexM["NM_UMNG5"] = flex[index1, "NM_MNGD"];
                    break;
            }
            if (flex.Name == "_flexL")
            {
                this._flexM["CD_MNGD" + index1.ToString()] = flex[index1, "CD_MNGD"];
                this._flexM["NM_MNGD" + index1.ToString()] = flex[index1, "NM_MNGD"];
            }
            else
            {
                FlexGrid flexGrid1 = this._flexM;
                string str2 = "CD_MNGD";
                int num = index1 + 5;
                string str3 = num.ToString();
                string index2 = str2 + str3;
                object obj2 = flex[index1, "CD_MNGD"];
                flexGrid1[index2] = obj2;
                FlexGrid flexGrid2 = this._flexM;
                string str4 = "NM_MNGD";
                num = index1 + 5;
                string str5 = num.ToString();
                string index3 = str4 + str5;
                object obj3 = flex[index1, "NM_MNGD"];
                flexGrid2[index3] = obj3;
            }
        }

        private void FillDetailData(int row)
        {
            if (this._flexL.Styles["13"] == null)
            {
                for (int row1 = 1; row1 <= 5; ++row1)
                {
                    CellStyle cellStyle1 = this._flexL.Styles.Add(row1.ToString() + "3");
                    cellStyle1.BackColor = this._flexL.Styles.Normal.BackColor;
                    CellRange cellRange1 = this._flexL.GetCellRange(row1, 3);
                    cellRange1.Style = cellStyle1;
                    CellStyle cellStyle2 = this._flexL.Styles.Add(row1.ToString() + "4");
                    cellStyle2.BackColor = this._flexL.Styles.Normal.BackColor;
                    CellRange cellRange2 = this._flexL.GetCellRange(row1, 4);
                    cellRange2.Style = cellStyle2;
                    CellStyle cellStyle3 = this._flexR.Styles.Add(row1.ToString() + "3");
                    cellStyle3.BackColor = this._flexR.Styles.Normal.BackColor;
                    CellRange cellRange3 = this._flexR.GetCellRange(row1, 3);
                    cellRange3.Style = cellStyle3;
                    CellStyle cellStyle4 = this._flexR.Styles.Add(row1.ToString() + "4");
                    cellStyle4.BackColor = this._flexR.Styles.Normal.BackColor;
                    CellRange cellRange4 = this._flexR.GetCellRange(row1, 4);
                    cellRange4.Style = cellStyle4;
                }
            }
            else
            {
                for (int index = 1; index <= 5; ++index)
                {
                    this._flexL.Styles[index.ToString() + "3"].BackColor = this._flexL.Styles.Normal.BackColor;
                    this._flexL.Styles[index.ToString() + "4"].BackColor = this._flexL.Styles.Normal.BackColor;
                    this._flexR.Styles[index.ToString() + "3"].BackColor = this._flexL.Styles.Normal.BackColor;
                    this._flexR.Styles[index.ToString() + "4"].BackColor = this._flexL.Styles.Normal.BackColor;
                }
            }
            this._flexL.Clear(ClearFlags.Content, this._flexL.Rows.Fixed, this._flexL.Cols.Fixed, this._flexL.Rows.Count - 1, this._flexL.Cols.Count - 1);
            this._flexR.Clear(ClearFlags.Content, this._flexR.Rows.Fixed, this._flexR.Cols.Fixed, this._flexR.Rows.Count - 1, this._flexR.Cols.Count - 1);
            if (row == -1)
                return;
            this._flexL.Redraw = false;
            this._flexR.Redraw = false;
            for (int index = 1; index <= 5; ++index)
            {
                this._flexL[index, "CD_MNG"] = this._flexM["CD_MNG" + index.ToString()];
                this._flexL[index, "NM_MNG"] = this._flexM["NM_MNG" + index.ToString()];
                this._flexL[index, "ST_MNG"] = this._flexM["ST_MNG" + index.ToString()];
                this._flexL[index, "CD_MNGD"] = this._flexM["CD_MNGD" + index.ToString()];
                this._flexL[index, "NM_MNGD"] = this._flexM["NM_MNGD" + index.ToString()];
                if (!this._flexL[index, "NM_MNG"].ToString().Equals(""))
                {
                    if (this._flexM["YN_CDMNG" + index.ToString()].ToString().Equals("Y"))
                        this._flexL.Styles[index.ToString() + "3"].BackColor = Color.Cyan;
                    else
                        this._flexL.Styles[index.ToString() + "4"].BackColor = Color.Cyan;
                }
                this._flexR[index, "NM_MNG"] = this._flexM["NM_MNG" + (5 + index).ToString()];
                this._flexR[index, "ST_MNG"] = this._flexM["ST_MNG" + (5 + index).ToString()];
                this._flexR[index, "CD_MNGD"] = this._flexM["CD_MNGD" + (5 + index).ToString()];
                this._flexR[index, "NM_MNGD"] = this._flexM["NM_MNGD" + (5 + index).ToString()];
                if (!this._flexR[index, "NM_MNG"].ToString().Equals(""))
                {
                    if (this._flexM["YN_CDMNG" + (5 + index).ToString()].ToString().Equals("Y"))
                        this._flexR.Styles[index.ToString() + "3"].BackColor = Color.Cyan;
                    else
                        this._flexR.Styles[index.ToString() + "4"].BackColor = Color.Cyan;
                }
            }
            this._flexL.Redraw = true;
            this._flexR.Redraw = true;
        }

        private void Set관리항목(string 관리항목코드, string 관리내역코드, string 관리내역명)
        {
            int index1 = 0;
            for (int index2 = 1; index2 <= 10; ++index2)
            {
                if (D.GetString(this._flexM["CD_MNG" + index2.ToString()]) == 관리항목코드)
                {
                    this._flexM["CD_MNGD" + index2.ToString()] = 관리내역코드;
                    this._flexM["NM_MNGD" + index2.ToString()] = 관리내역명;
                    index1 = index2;
                    break;
                }
            }
            if (index1 > 0 && index1 < 6)
            {
                this._flexL[index1, "CD_MNGD"] = 관리내역코드;
                this._flexL[index1, "NM_MNGD"] = 관리내역명;
            }
            else
            {
                this._flexR[index1 - 5, "CD_MNGD"] = 관리내역코드;
                this._flexR[index1 - 5, "NM_MNGD"] = 관리내역명;
            }
        }

        private void Set그리드컬러()
        {
            this._flexM.Redraw = false;
            for (int @fixed = this._flexM.Rows.Fixed; @fixed < this._flexM.Rows.Count; ++@fixed)
                this.Set그리드컬러(@fixed);
            this._flexM.Redraw = true;
        }

        private void Set그리드컬러(int i)
        {
            CellRange cellRange = this._flexM.GetCellRange(i, this._flexM.Cols.Fixed, i, this._flexM.Cols.Count - 1);
            if (D.GetString(this._flexM[i, "TP_RESULT"]) == "오류")
                cellRange.Style = this._flexM.Styles["오류"];
            else
                cellRange.Style = this._flexM.Styles["성공"];
        }

        private void Set관리항목초기화()
        {
            this.FillDetailData(-1);
            for (int index = 1; index <= 10; ++index)
            {
                this._flexM["CD_MNG" + index.ToString()] = string.Empty;
                this._flexM["NM_MNG" + index.ToString()] = string.Empty;
                this._flexM["YN_CDMNG" + index.ToString()] = string.Empty;
                this._flexM["TP_MNGFORM" + index.ToString()] = string.Empty;
                this._flexM["ST_MNG" + index.ToString()] = string.Empty;
                this._flexM["CD_MNGD" + index.ToString()] = string.Empty;
                this._flexM["NM_MNGD" + index.ToString()] = string.Empty;
            }
        }

        private void Set기초데이타가져오기(string 결의코드, string 증빙유형)
        {
            DataTable dataTable = this._biz.Edit_Search(결의코드, 증빙유형);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow dataRow = dataTable.Rows[0];
                this._flexM["CD_EPCODE"] = dataRow["CD_EPCODE"];
                this._flexM["NM_EPCODE"] = dataRow["NM_EPCODE"];
                this._flexM["TP_EPNOTE"] = dataRow["TP_EPNOTE"];
                this._flexM["CD_EVID"] = dataRow["CD_EVID"];
                this._flexM["NM_EVID"] = dataRow["NM_EVID"];
                this._flexM["TP_TAX"] = dataRow["TP_TAX"];
                this._flexM["CD_BGACCT"] = dataRow["CD_BGACCT"];
                this._flexM["NM_BGACCT"] = dataRow["NM_BGACCT"];
                this._flexM["CD_STDACCT"] = dataRow["CD_STDACCT"];
                this._flexM["CD_TAXACCT"] = dataRow["CD_TAXACCT"];
                this._flexM["CD_SUMACCT"] = dataRow["CD_SUMACCT"];
                int num;
                for (num = 1; num <= 10; ++num)
                {
                    this._flexM["CD_MNG" + num.ToString()] = dataRow["CD_MNG" + num.ToString()];
                    this._flexM["NM_MNG" + num.ToString()] = dataRow["NM_MNG" + num.ToString()];
                    this._flexM["YN_CDMNG" + num.ToString()] = dataRow["YN_CDMNG" + num.ToString()];
                    this._flexM["TP_MNGFORM" + num.ToString()] = dataRow["TP_MNGFORM" + num.ToString()];
                    this._flexM["ST_MNG" + num.ToString()] = dataRow["ST_MNG" + num.ToString()];
                }
                for (num = 1; num <= 8; ++num)
                {
                    this._flexM["A1_CD_MNG" + num.ToString()] = dataRow["A1_CD_MNG" + num.ToString()];
                    this._flexM["A1_NM_MNG" + num.ToString()] = dataRow["A1_NM_MNG" + num.ToString()];
                    this._flexM["A1_ST_MNG" + num.ToString()] = dataRow["A1_ST_MNG" + num.ToString()];
                    this._flexM["A2_CD_MNG" + num.ToString()] = dataRow["A2_CD_MNG" + num.ToString()];
                    this._flexM["A2_NM_MNG" + num.ToString()] = dataRow["A2_NM_MNG" + num.ToString()];
                    this._flexM["A2_ST_MNG" + num.ToString()] = dataRow["A2_ST_MNG" + num.ToString()];
                    this._flexM["A3_CD_MNG" + num.ToString()] = dataRow["A3_CD_MNG" + num.ToString()];
                    this._flexM["A3_NM_MNG" + num.ToString()] = dataRow["A3_NM_MNG" + num.ToString()];
                    this._flexM["A3_ST_MNG" + num.ToString()] = dataRow["A3_ST_MNG" + num.ToString()];
                }
                this._flexM["NM_BGACCT"] = dataRow["NM_BGACCT"];
                this._flexM["TP_BUNIT"] = dataRow["TP_BUNIT"];
                this._flexM["TP_BMSG"] = dataRow["TP_BMSG"];
                this._flexM["ST_MUTUAL"] = dataRow["ST_MUTUAL"];
                this._flexM["NM_MUTUAL"] = dataRow["NM_MUTUAL"];
            }
            this.FillDetailData(this._flexM.Row);
        }

        private bool Get관리항목필수체크2(int row, string 관리항목코드)
        {
            string str1 = string.Empty;
            switch (관리항목코드)
            {
                case "A06":
                    str1 = D.GetString(this._flexM[row, "CD_PARTNER"]);
                    break;
                case "C01":
                    str1 = D.GetString(this._flexM[row, "NO_COMPANY"]);
                    break;
                case "A01":
                    str1 = D.GetString(this._flexM[row, "CD_BIZAREA"]);
                    break;
                case "A02":
                    str1 = D.GetString(this._flexM[row, "CD_CC"]);
                    break;
                case "A03":
                    str1 = D.GetString(this._flexM[row, "CD_DEPT"]);
                    break;
                case "A05":
                    str1 = D.GetString(this._flexM[row, "CD_PJT"]);
                    break;
                case "A08":
                    str1 = D.GetString(this._flexM[row, "CD_CARD"]);
                    break;
                case "A07":
                    str1 = D.GetString(this._flexM[row, "CD_DEPOSIT"]);
                    break;
                case "A09":
                    str1 = D.GetString(this._flexM[row, "CD_BANK"]);
                    break;
                case "A04":
                    str1 = D.GetString(this._flexM[row, "CD_EMPLOY"]);
                    break;
                case "A21":
                    str1 = D.GetString(this._flexM[row, "CD_UMNG1"]);
                    break;
                case "A22":
                    str1 = D.GetString(this._flexM[row, "CD_UMNG2"]);
                    break;
                case "A23":
                    str1 = D.GetString(this._flexM[row, "CD_UMNG3"]);
                    break;
                case "A24":
                    str1 = D.GetString(this._flexM[row, "CD_UMNG4"]);
                    break;
                case "A25":
                    str1 = D.GetString(this._flexM[row, "CD_UMNG5"]);
                    break;
                case "B22":
                case "B23":
                    str1 = D.GetString(this._flexM[row, "DT_END"]);
                    break;
                case "B24":
                    str1 = D.GetString(this._flexM[row, "CD_EXCH"]);
                    break;
                case "B25":
                    str1 = D.GetString(this._flexM[row, "RT_EXCH"]);
                    break;
                case "B26":
                    str1 = D.GetString(this._flexM[row, "AM_EX"]);
                    break;
                case "B21":
                    string str2 = string.Empty;
                    str1 = !(D.GetString(this._flexM[row, "DT_ACCT"]).Trim() != "") ? D.GetString(this._flexM[row, "DT_START"]) : D.GetString(this._flexM[row, "DT_ACCT"]);
                    break;
                case "C14":
                    str1 = D.GetString(this._flexM[row, "TP_TAX"]);
                    break;
                case "C05":
                    str1 = D.GetString(this._flexM[row, "AM_TAXSTD"]);
                    break;
            }
            return !string.IsNullOrEmpty(str1);
        }

        private string[] 관리내역(int row, string 관리항목코드)
        {
            string[] strArray = new string[2]
      {
        "",
        ""
      };
            for (int index = 1; index <= 10; ++index)
            {
                if (this._flexM[row, "CD_MNG" + index.ToString()].ToString() == 관리항목코드)
                {
                    strArray[0] = this._flexM[row, "CD_MNGD" + index.ToString()].ToString();
                    strArray[1] = this._flexM[row, "NM_MNGD" + index.ToString()].ToString();
                    break;
                }
            }
            return strArray;
        }

        private bool IsNumeric(string value)
        {
            if (value == null || value == string.Empty)
                return false;
            foreach (char c in value)
            {
                if (!char.IsNumber(c))
                    return false;
            }
            return true;
        }

        private void Get예산단위(string ps_결의부서)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_FI_BGDEPT_SELECT_D1", new object[2]
      {
         this.LoginInfo.CompanyCode,
         ps_결의부서
      });
            if (dataTable.Rows.Count == 1)
            {
                this._flexM[this._flexM.Row, "CD_BUDGET"] = dataTable.Rows[0]["CD_BUDGET"];
                this._flexM[this._flexM.Row, "NM_BUDGET"] = dataTable.Rows[0]["NM_BUDGET"];
                this.Get사업계획(D.GetString(dataTable.Rows[0]["CD_BUDGET"]));
            }
            else
            {
                this._flexM[this._flexM.Row, "CD_BUDGET"] = "";
                this._flexM[this._flexM.Row, "NM_BUDGET"] = "";
            }
        }

        private void Get사업계획(string ps_예산단위)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_FI_DOCU_SELECT_BIZPLAN", new object[2]
      {
         this.LoginInfo.CompanyCode,
         ps_예산단위
      });
            if (dataTable.Rows.Count == 1)
            {
                this._flexM[this._flexM.Row, "CD_BIZPLAN"] = dataTable.Rows[0]["CD_BIZPLAN"];
                this._flexM[this._flexM.Row, "NM_BIZPLAN"] = dataTable.Rows[0]["NM_BIZPLAN"];
            }
            else
            {
                this._flexM[this._flexM.Row, "CD_BIZPLAN"] = "";
                this._flexM[this._flexM.Row, "NM_BIZPLAN"] = "";
            }
        }

        private void 예산관련컬럼셋팅(int row, DataTable dt)
        {
            if (dt == null)
            {
                this._flexM[row, "CD_BGACCT"] = "";
                this._flexM[row, "NM_BGACCT"] = "";
                this._flexM[row, "TP_BUNIT"] = "";
                this._flexM[row, "TP_BMSG"] = "";
            }
            else
            {
                this._flexM[row, "CD_BGACCT"] = dt.Rows[0]["CD_BGACCT"];
                this._flexM[row, "NM_BGACCT"] = dt.Rows[0]["NM_BGACCT"];
                this._flexM[row, "TP_BUNIT"] = dt.Rows[0]["TP_BUNIT"];
                this._flexM[row, "TP_BMSG"] = dt.Rows[0]["TP_BMSG"];
            }
        }

        private bool CheckDocu()
        {
            this._dt오류메세지.Clear();
            for (int @fixed = this._flexM.Rows.Fixed; @fixed < this._flexM.Rows.Count; ++@fixed)
            {
                if (D.GetString(this._flexM[@fixed, "S"]) == "Y")
                {
                    string str = D.GetString(this._flexM[@fixed, "TP_RESULT"]);
                    if (str == "")
                        str = "성공";
                    bool flag = this.CheckDetail2(@fixed);
                    this._flexM[@fixed, "TP_RESULT"] = flag ? "성공" : "오류";
                    if (D.GetString(this._flexM[@fixed, "TP_RESULT"]) != str)
                        this.Set그리드컬러(@fixed);
                }
            }
            return this._dt오류메세지.Rows.Count <= 0;
        }

        private bool CheckDetail2(int row)
        {
            bool flag1 = true;
            DataTable table = this._dt오류메세지.Clone();
            string str1 = string.Empty;
            string str2 = string.Empty;
            string string1 = D.GetString(this._flexM[row, "TP_EPNOTE"]);
            for (int index1 = 1; index1 <= 3; ++index1)
            {
                switch (index1)
                {
                    case 1:
                        str1 = D.GetString(this._flexM[row, "CD_STDACCT"]);
                        str2 = !(string1 == "1") ? "C" : "D";
                        break;
                    case 2:
                        str1 = D.GetString(this._flexM[row, "CD_TAXACCT"]);
                        str2 = !(string1 == "1") ? "C" : "D";
                        break;
                    case 3:
                        str1 = D.GetString(this._flexM[row, "CD_SUMACCT"]);
                        str2 = !(string1 == "1") ? "D" : "C";
                        break;
                }
                if (!string.IsNullOrEmpty(str1))
                {
                    for (int index2 = 1; index2 <= 8; ++index2)
                    {
                        string string2 = D.GetString(this._flexM[row, "A" + index1.ToString() + "_CD_MNG" + index2.ToString()]);
                        string string3 = D.GetString(this._flexM[row, "A" + index1.ToString() + "_ST_MNG" + index2.ToString()]);
                        string string4 = D.GetString(this._flexM[row, "A" + index1.ToString() + "_NM_MNG" + index2.ToString()]);
                        if (string3 == "A" || string3 == str2)
                        {
                            bool flag2 = false;
                            for (int index3 = 1; index3 <= 10; ++index3)
                            {
                                if (string2 == D.GetString(this._flexM[row, "CD_MNG" + index3.ToString()]))
                                {
                                    flag2 = true;
                                    if (string.IsNullOrEmpty(D.GetString(this._flexM[row, "CD_MNGD" + index3.ToString()])) && string.IsNullOrEmpty(D.GetString(this._flexM[row, "NM_MNGD" + index3.ToString()])) && !this.Get관리항목필수체크2(row, string2))
                                    {
                                        DataRow row1 = table.NewRow();
                                        row1["ROW"] = this._flexM[row, "ROW_ID"];
                                        row1["MSG"] = ("[" + string4 + "] 필수입력값이 누락되었습니다. 계정코드:" + str1);
                                        table.Rows.Add(row1);
                                        break;
                                    }
                                    break;
                                }
                            }
                            if (!flag2 && !this.Get관리항목필수체크2(row, string2))
                            {
                                DataRow row1 = table.NewRow();
                                row1["ROW"] = this._flexM[row, "ROW_ID"];
                                row1["MSG"] = ("[" + string4 + "]가 존재하지 않습니다. 계정코드:" + str1);
                                table.Rows.Add(row1);
                            }
                        }
                    }
                }
            }
            if (table.Rows.Count > 0)
                flag1 = false;
            this._dt오류메세지.Merge(table);
            return flag1;
        }

        private bool 전표마감(DataRow[] dr_row)
        {
            dr_row[0].Table.DefaultView.RowFilter = "S = 'Y'";
            DataTable dataTable = dr_row[0].Table.DefaultView.ToTable(1 != 0, "DT_ACCT");
            Docu docu = new Docu();
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (!docu.전표마감(this.ctx회계단위.CodeValue, dataRow["DT_ACCT"].ToString(), "11", Global.MainFrame.LoginInfo.DeptCode, dataRow["DT_ACCT"].ToString()))
                    return false;
            }
            return true;
        }

        private void BindingDataTalbe(DataTable dt)
        {
            this._dtBindingDataTable = null;

            try
            {
                if (this._flexM == null) return;

                if (dt.Select("TP_GIAN IN ('Y','N')").Length > 0)
                {
                    if (this._flexM.DataTable != null && this._flexM.DataTable.Rows.Count > 0)
                    {
                        this._dtBindingDataTable = this._flexM.DataTable.Copy();

                        foreach (DataRow row in this._dtBindingDataTable.Select("TP_GIAN IN ('Y','N')"))
                        {
                            this._dtBindingDataTable.Rows.Remove(row);
                        }
                        
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dt.Rows)
                            {
                                this._dtBindingDataTable.Rows.Add(dataRow.ItemArray);
                            }
                        }
                    }
                    else
                        this._dtBindingDataTable = dt;
                }
                else
                    this._dtBindingDataTable = dt;

                if (this._bRowIdSort)
                    this._dtBindingDataTable = new DataView(this._dtBindingDataTable) { Sort = "ROW_ID asc" }.ToTable();

                this._flexM.Binding = this._dtBindingDataTable;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void GetCdBizplan(string ps_CdBudget)
        {
            DataTable dataTable = (DataTable)((ResultData)this.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_FI_DOCU_SELECT_BIZPLAN",
                SpParamsSelect = new object[] { this.LoginInfo.CompanyCode,
                                                ps_CdBudget }
            })).DataValue;

            if (dataTable.Rows.Count == 1)
            {
                this._flexM[this._flexM.Row, "CD_BIZPLAN"] = dataTable.Rows[0]["CD_BIZPLAN"];
                this._flexM[this._flexM.Row, "NM_BIZPLAN"] = dataTable.Rows[0]["NM_BIZPLAN"];
            }
            else
            {
                this._flexM[this._flexM.Row, "CD_BIZPLAN"] = string.Empty;
                this._flexM[this._flexM.Row, "NM_BIZPLAN"] = string.Empty;
            }
        }
        #endregion
    }
}