using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using account;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dass.FlexGrid.ContextMenu;
using Dintec;
using Duzon.BizOn.Erpu.BusinessConfig;
using Duzon.BizOn.Erpu.Security;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using Duzon.ERPU.OLD;
using Duzon.ERPU.SA.Settng;

namespace cz
{
    public partial class P_CZ_MA_PARTNER : PageBase
    {
        private P_CZ_MA_PARTNER_BIZ _biz = new P_CZ_MA_PARTNER_BIZ();
        private P_FI_GET_NAME _getNamte = new P_FI_GET_NAME();
        private string _sYN_PARTNERSEQ = string.Empty;
        private bool _Is사업자등록번호중복허용;

        public P_CZ_MA_PARTNER()
        {
            StartUp.Certify(this);

            this.InitializeComponent();
            this.MainGrids = new FlexGrid[] { this._flex };
        }

        public P_CZ_MA_PARTNER(string 거래처코드)
        {
            StartUp.Certify(this);

            this.InitializeComponent();
            this.MainGrids = new FlexGrid[] { this._flex };

            this.txt검색.Text = 거래처코드;
        }

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitOneGrid();
            this.InitGrid();
            this.InitEvent();
            this.InitControl();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.splitContainer1.SplitterDistance = 936;

            this._Is사업자등록번호중복허용 = new 시스템환경설정().Is환경설정(시스템환경코드.사업자등록번호중복허용, true);
            this.Set언어에따른설정변경();

            UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "SYNC", this.btn동기화, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "FILE", this.btn첨부파일, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "EXCELUP", this.btn엑셀업로드, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "EXCEL", this.btn엑셀양식다운로드, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "PTR", this.btn담당자추가, true);
            ugrant.GrantButtonEnble("P_CZ_MA_PARTNER", "DEPOSIT", this.btn계좌자료, true);

            if (ugrant.GrantButton("P_CZ_MA_PARTNER", "CONFIRM"))
                this.meb사업자등록번호.ReadOnly = false;
            else
                this.meb사업자등록번호.ReadOnly = true;

            this._sYN_PARTNERSEQ = this._biz.GetEnv_PartnerSeq();
            this.SetControlEnable(false);

            if (!string.IsNullOrEmpty(this.txt검색.Text))
                this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitOneGrid()
        {
            this.one기본정보.UseCustomLayout = true;
            this.one기본정보.IsSearchControl = false;
            this.one기본정보.InitCustomLayout();
            this.bpPnl_CD_PARTNER.IsNecessaryCondition = true;
            this.one세금계산서기본정보.UseCustomLayout = true;
            this.one세금계산서기본정보.IsSearchControl = false;
            this.one세금계산서기본정보.InitCustomLayout();
            this.bpPnl_NO_COMPANY.IsNecessaryCondition = true;
            this.one전자세금계산서발행정보.UseCustomLayout = true;
            this.one전자세금계산서발행정보.IsSearchControl = false;
            this.one전자세금계산서발행정보.InitCustomLayout();
            this.bpPnl_CD_EMP_PARTNER.IsNecessaryCondition = true;
            this.one계좌정보.UseCustomLayout = true;
            this.one계좌정보.IsSearchControl = false;
            this.one계좌정보.InitCustomLayout();
            this.one기타정보.UseCustomLayout = true;
            this.one기타정보.IsSearchControl = false;
            this.one기타정보.InitCustomLayout();
            this.bpPnl_SN_PARTNER.IsNecessaryCondition = true;
            this.one부가정보.UseCustomLayout = true;
            this.one부가정보.IsSearchControl = false;
            this.one부가정보.InitCustomLayout();
            this.bpPnl_CLS_PARTNER.IsNecessaryCondition = true;
            this.one기타정보1.UseCustomLayout = true;
            this.one기타정보1.IsSearchControl = false;
            this.one기타정보1.InitCustomLayout();
            this.bpPnl_CD_NATION.IsNecessaryCondition = true;
            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("CD_PARTNER", "거래처코드", 70);
            this._flex.SetCol("LN_PARTNER", "거래처명", 160);
            this._flex.SetCol("NO_COMPANY", "사업자등록번호", 100);
            this._flex.SetCol("NM_CEO", "대표자명", false);
            this._flex.SetCol("NO_RES", "주민번호", 100);
            this._flex.SetCol("USE_YN", "사용유무", 100);
            this._flex.SetCol("NM_BANK", "은행명", false);
            this._flex.SetCol("NO_DEPOSIT", "계좌번호", false);
            this._flex.SetCol("NM_DEPOSIT", "예금주", false);
            this._flex.SetCol("NM_TEXT", "비고", false);
            this._flex.SetCol("NO_POST1", "우편번호", false);
            this._flex.SetCol("DC_ADS1_H", "주소", false);
            this._flex.SetCol("DC_ADS1_D", "상세주소", false);
            this._flex.SetCol("SN_PARTNER", "거래처명(약칭)", false);
            this._flex.SetCol("NO_TEL", "전화번호", false);
            this._flex.SetCol("NO_FAX", "FAX번호", false);
            this._flex.SetCol("NO_COR", "법인등록번호", false);
            this._flex.SetCol("NM_EMP_SALE", "영업담당자", false);
            this._flex.SetCol("CD_AREA", "지역구분코드", false);
			this._flex.SetCol("NM_CLS_PARTNER", "거래처분류", false);
            this._flex.SetCol("TP_PRICE_SENS", "가격민감도", false);
            this._flex.SetCol("E_MAIL", "E-MAIL주소", 100);
            this._flex.SetCol("NM_NATION", "국가명", 100);
            
            this._flex.SetOneGridBinding(new object[] { this.txt거래처코드 }, new IUParentControl[] { this.one기본정보,
                                                                                                      this.one세금계산서기본정보,
                                                                                                      this.one전자세금계산서발행정보,
                                                                                                      this.one계좌정보,
                                                                                                      this.one기타정보,
                                                                                                      this.one부가정보,
                                                                                                      this.one기타정보1,
                                                                                                      this.pnl비고 });

            this._flex.SetBindningCheckBox(this.chkCert발송여부, "Y", "N");
            this._flex.SetBindningCheckBox(this.chk입고라벨발송여부, "Y", "N");
            this._flex.SetBindningRadioButton(new RadioButtonExt[] { this.rdo자동등록홀딩_예, this.rdo자동등록홀딩_아니오 }, new string[] { "Y", "N" });

            this._flex.Cols["NO_COMPANY"].Format = "0##-##-#####";
            this._flex.Cols["NO_RES"].Format = this._flex.Cols["NO_RES"].EditMask = "0#####-#######";
            this._flex.Cols["NO_COR"].Format = this._flex.Cols["NO_COR"].EditMask = "0#####-#######";

            this._flex.Cols["NO_RES"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["NO_COR"].TextAlign = TextAlignEnum.CenterCenter;

            this._flex.SetStringFormatCol("NO_COMPANY");
            this._flex.SetStringFormatCol("NM_TEXT");
            this._flex.SetStringFormatCol("NO_RES");
            this._flex.SetStringFormatCol("NO_COR");

            this._flex.SetNoMaskSaveCol("NO_RES");
            this._flex.SetNoMaskSaveCol("NO_COR");

            this._flex.NewRowEditable = false;
            this._flex.EnterKeyAddRow = false;

            this.SetUserGrid(this._flex);
            this._flex.SetContextMenuConfig(FlexGridContextMenuConfig.저장경로설정);
            this._flex.Redraw = true;
            this._flex.SetDummyColumn("OLD_NO_COMPANY");

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this._flex.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
            this._flex.CodeSearch += new CodeSearchEventHandler(this._flex_CodeSearch);
            this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);

            this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn동기화.Click += new EventHandler(this.btn동기화_Click);
            this.btn예금주조회.Click += new EventHandler(this.btn예금주휴페업_Click);
            this.btn휴폐업조회.Click += new EventHandler(this.btn예금주휴페업_Click);
            this.btn계좌자료.Click += new EventHandler(this.btn계좌자료_Click);
            this.btn담당자추가.Click += new EventHandler(this.btn담당자추가_Click);
            this.btn원산지추가.Click += new EventHandler(this.btn원산지추가_Click);
            this.btn본사주소.Click += new EventHandler(this.OnHelpClick);
            this.btn영업주소.Click += new EventHandler(this.OnHelpClick);
            this.btn공장주소.Click += new EventHandler(this.OnHelpClick);

            this.meb사업자등록번호.Validating += new CancelEventHandler(this.meb사업자등록번호_Validating);
            this.meb주민번호.Validating += new CancelEventHandler(this.meb주민번호_Validating);
            this.txt검색.KeyDown += new KeyEventHandler(this.txt검색_KeyDown);
            this.txt비고.KeyDown += new KeyEventHandler(this.txt비고_KeyDown);
            this.txt비고.Leave += new EventHandler(this.txt비고_Leave);
            
            this.cbo연결법인구분.SelectionChangeCommitted += new EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.cbo원산지.SelectionChangeCommitted += new EventHandler(this.cbo원산지_SelectionChangeCommitted);
			this.cbo매입결제조건.SelectionChangeCommitted += Cbo매입결제조건_SelectionChangeCommitted;

            this.ctx수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bct발주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx사용자정의1.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx사용자정의2.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx사용자정의3.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
        }

        private void Cbo매입결제조건_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (string.IsNullOrEmpty(this.cbo매입결제조건.SelectedValue.ToString()))
                    this.cur지급예정일.DecimalValue = 0;
                else
				{
                    query = @"SELECT CD_FLAG1 
                              FROM MA_CODEDTL WITH(NOLOCK)
                              WHERE CD_COMPANY = '{0}'
                              AND CD_FIELD = 'PU_C000014'
                              AND CD_SYSDEF = '{1}'";

                    string 지급예정일 = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                    this.cbo매입결제조건.SelectedValue.ToString())).ToString();

                    this.cur지급예정일.DecimalValue = D.GetDecimal(지급예정일);
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitControl()
        {
            this.txt비고.MaxLength = 500;

            this.cur운송LT.NumberFormatInfoObject.NumberDecimalDigits = 0;
            this.cur운송LT.DecimalValue = 0;
            this.cur생산직원수.NumberFormatInfoObject.NumberDecimalDigits = 0;
            this.cur생산직원수.DecimalValue = 0;
            this.cur관리직원수.NumberFormatInfoObject.NumberDecimalDigits = 0;
            this.cur관리직원수.DecimalValue = 0;
            this.cur자본금.NumberFormatInfoObject.NumberDecimalDigits = 4;
            this.cur자본금.DecimalValue = 0;

            if (BusinessInfo.SysInfo.IpCms != string.Empty)
            {
                this.btn휴폐업조회.Visible = true;
                this.btn예금주조회.Visible = true;
            }
            else
            {
                this.btn휴폐업조회.Visible = false;
                this.btn예금주조회.Visible = false;
            }

            SetControl setControl = new SetControl();

            setControl.SetCombobox(this.cbo거래처구분S, new DataView(MA.GetCode("MA_B000001", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처구분, new DataView(MA.GetCode("MA_B000001", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처타입, new DataView(MA.GetCode("MA_B000002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처분류S, new DataView(MA.GetCode("MA_B000003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처분류, new DataView(MA.GetCode("MA_B000003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo국가명, new DataView(MA.GetCode("MA_B000020", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo부가세계산법, new DataView(MA.GetCode("SA_B000018", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo계좌구분, new DataView(MA.GetCode("MA_B000079", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo은행, new DataView(MA.GetCode("MA_B000043", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo은행소재국, new DataView(MA.GetCode("CZ_MA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo예금주국적, new DataView(MA.GetCode("CZ_MA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo지역구분, new DataView(MA.GetCode("MA_B000062", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처그룹, new DataView(MA.GetCode("MA_B000065", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처그룹S, new DataView(MA.GetCode("MA_B000065", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처그룹2, new DataView(MA.GetCode("MA_B000067", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo수금조건, new DataView(MA.GetCode("SA_B000002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            if (ComFunc.전용코드("거래처정보등록-회계지급관리사용여부") == "Y")
                setControl.SetCombobox(this.cbo지급조건, this._biz.Get지급관리());
            else
                setControl.SetCombobox(this.cbo지급조건, new DataView(MA.GetCode("PU_C000044", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            setControl.SetCombobox(this.cbo휴폐업구분, new DataView(MA.GetCode("MA_B000073", false), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo사용여부S, new DataView(MA.GetCode("사용여부", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo지급보류여부, new DataView(MA.GetCode("MA_B000096", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo자금수금예정일, new DataView(MA.GetCode("MA_B000097", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo자금지급예정일, new DataView(MA.GetCode("MA_B000097", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo사업자단위과세, new DataView(MA.GetCode("MA_B000094", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo계산서발행형태, new DataView(MA.GetCode("MA_B000095", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo연결법인구분, new DataView(MA.GetCode("MA_E000001", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo내외국인, new DataView(MA.GetCode("HR_T000003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboINQ발신방법, new DataView(MA.GetCode("CZ_PU00001", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboPO발신방법, new DataView(MA.GetCode("CZ_PU00002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboINQ수신방법, new DataView(MA.GetCode("CZ_SA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboQTN발신방법, new DataView(MA.GetCode("CZ_SA00017", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출선적조건, new DataView(MA.GetCode("TR_IM00003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출결제조건, new DataView(MA.GetCode("CZ_SA00013", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출통화, new DataView(MA.GetCode("MA_B000005", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출부가세, new DataView(MA.GetCode("MA_B000040", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo결재방법, new DataView(MA.GetCode("SA_B000002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출결제형태, new DataView(MA.GetCode("TR_IM00004", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입가격조건, new DataView(MA.GetCode("TR_IM00002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입통화, new DataView(MA.GetCode("MA_B000005", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입결제조건, new DataView(MA.GetCode("PU_C000014", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입결제조건S, new DataView(MA.GetCode("PU_C000014", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입부가세, new DataView(MA.GetCode("MA_B000046", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출부가세여부, new DataView(MA.GetCode("PU_C000005", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo메일첨부방식, new DataView(MA.GetCode("CZ_MA00018", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo지급구분, new DataView(MA.GetCode("CZ_PU00009", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo포장형태, new DataView(MA.GetCode("TR_IM00011", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            this.cbo인쇄형태.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE, 
																  		 '' AS NAME
																  UNION ALL
																  SELECT CD_SYSDEF AS CODE,
																  		 NM_SYSDEF AS NAME
																  FROM CZ_MA_CODEDTL WITH(NOLOCK)
																  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
															    @"AND CD_FIELD = 'CZ_MA00017'
																  ORDER BY NAME ASC");
			this.cbo인쇄형태.ValueMember = "CODE";
			this.cbo인쇄형태.DisplayMember = "NAME";

			this.cbo가격민감도.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE, 
																  		   '' AS NAME
																  UNION ALL
																  SELECT CD_SYSDEF AS CODE,
																  		 NM_SYSDEF AS NAME
																  FROM CZ_MA_CODEDTL WITH(NOLOCK)
																  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
																@"AND CD_FIELD = 'CZ_MA00028'
																  ORDER BY CODE ASC");
			this.cbo가격민감도.ValueMember = "CODE";
			this.cbo가격민감도.DisplayMember = "NAME";

            this.cboINV발송방법.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE, 
																    	    '' AS NAME
																    UNION ALL
																    SELECT CD_SYSDEF AS CODE,
																    	   NM_SYSDEF AS NAME
																    FROM CZ_MA_CODEDTL WITH(NOLOCK)
																    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                  @"AND CD_FIELD = 'CZ_MA00049'
																    ORDER BY CODE ASC");
            this.cboINV발송방법.ValueMember = "CODE";
            this.cboINV발송방법.DisplayMember = "NAME";

            this.cbo컬러발송.DataSource = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "사용", "미사용" }, true);
            this.cbo컬러발송.ValueMember = "CODE";
            this.cbo컬러발송.DisplayMember = "NAME";

            this.cbo배송방법.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE, 
																  	    '' AS NAME
																  UNION ALL
																  SELECT CD_SYSDEF AS CODE,
																  	   NM_SYSDEF AS NAME
																  FROM CZ_MA_CODEDTL WITH(NOLOCK)
																  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                @"AND CD_FIELD = 'CZ_MA00056'
																  ORDER BY CODE ASC");
            this.cbo배송방법.ValueMember = "CODE";
			this.cbo배송방법.DisplayMember = "NAME";
        }
		#endregion

		#region 메인버튼이벤트
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.MsgAndSave(true, false)) return;

                this.SetControlEnable(true);

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.txt검색.Text, 
                                                                     D.GetString(this.cbo거래처구분S.SelectedValue),
                                                                     D.GetString(this.cbo거래처분류S.SelectedValue),
                                                                     D.GetString(this.cbo사용여부S.SelectedValue),
                                                                     this.txt비고S.Text,
                                                                     D.GetString(this.cbo거래처그룹S.SelectedValue),
                                                                     D.GetString(this.cbo매입결제조건S.SelectedValue) });

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                else
                    this.ComboBox_SelectionChangeCommitted(null, null);
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

                this.SetControlEnable(true);
                string str1 = string.Empty;
                string str2 = string.Empty;

                if (this._flex.HasNormalRow && (this._flex[this._flex.Row, "CD_PARTNER"] != null && D.GetString(this._flex[this._flex.Row, "CD_PARTNER"]).Length > 0))
                {
                    if (this._sYN_PARTNERSEQ == "Y")
                        str1 = D.GetString(this._flex[this._flex.Row, "CD_PARTNER"]);
                    else if (this._flex.Rows.Count != 1)
                        str1 = D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_PARTNER"]);

                    for (int index = str1.Length - 1; index >= 0; --index)
                    {
                        if ((int)str1[index] == 57)
                        {
                            str2 = "0" + str2;
                        }
                        else
                        {
                            try
                            {
                                str2 = (int.Parse(D.GetString(str1[index])) + 1).ToString() + str2;
                                break;
                            }
                            catch
                            {

                            }
                        }
                    }
                }

                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex[this._flex.Row, "TP_PARTNER"] = "EXT";
                this._flex[this._flex.Row, "CLS_PARTNER"] = D.GetString(this.cbo거래처분류S.SelectedValue);
                this._flex[this._flex.Row, "DT_OPEN"] = "00000000";
                this._flex[this._flex.Row, "AM_CAP"] = 0;
                this._flex[this._flex.Row, "MANU_EMP"] = 0;
                this._flex[this._flex.Row, "SD_PARTNER"] = "00000000";
                this._flex[this._flex.Row, "FG_PARTNER"] = "001";
                this._flex[this._flex.Row, "MANA_EMP"] = "0";
                this._flex[this._flex.Row, "USE_YN"] = "Y";
                this._flex[this._flex.Row, "YN_USE_GIR"] = "Y";
                this._flex[this._flex.Row, "FG_CREDIT"] = "N";
                this._flex[this._flex.Row, "LT_TRANS"] = "0";
                this._flex[this._flex.Row, "TP_TAX"] = "001";
                this._flex[this._flex.Row, "CD_CON"] = "001";
                this._flex[this._flex.Row, "TP_DEFER"] = "Y";

                if (!str2.Equals(""))
                    this._flex[this._flex.Row, "CD_PARTNER"] = (str1.Substring(0, str1.Length - str2.Length) + str2);

                this._flex.AddFinished();

                DataRow dataRow = this._flex.GetDataRow(this._flex.Row);
                dataRow.AcceptChanges();
                dataRow.SetAdded();

                this._flex.Col = this._flex.Cols.Fixed;
                this.txt거래처코드.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                string str = string.Empty;
                string code = this._biz.Delete(this._flex["CD_PARTNER"], this._flex["OLD_NO_COMPANY"]);

                if (!code.Equals(""))
                {
                    this.ShowMessage(code);
                }
                else
                    this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		protected override bool BeforeSave()
		{
            DataTable dt = this._flex.GetChanges();

            if (dt.Select().AsEnumerable().Where(x => Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", x["E_MAIL"].ToString()))) == 0).Count() > 0)
            {
                this.ShowMessage("전자세금계산서 발행정보 => E-MAIL주소에 형식에 맞지 않은 이메일 주소가 입력 되어 있습니다.");
                return false;
            }

            return base.BeforeSave();
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (this.MsgAndSave(false, false))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }

                this.cbo거래처구분.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤이벤트
        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection oleDbConnection1 = null;
            try
            {
                bool flag = true;
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;

                string fileName = folderBrowserDialog.SelectedPath + "\\엑셀업로드양식_거래처정보관리_" + Global.MainFrame.GetStringToday + ".xls";
                new WebClient().DownloadFile(Global.MainFrame.HostURL + "/shared/CZ/P_CZ_MA_PARTNER.xls", fileName);

                if (this._flex.HasNormalRow && this.ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                    flag = false;

                this.ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");

                if (!flag)
                    return;

                oleDbConnection1 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;");
                oleDbConnection1.Open();
                string srcTable = string.Empty;
                OleDbConnection oleDbConnection2 = oleDbConnection1;
                Guid schema = OleDbSchemaGuid.Tables;
                object[] objArray = new object[4];
                objArray[3] = "TABLE";
                object[] restrictions = objArray;
                DataTable oleDbSchemaTable = oleDbConnection2.GetOleDbSchemaTable(schema, restrictions);
                DataSet dataSet = new DataSet();
                IEnumerator enumerator = oleDbSchemaTable.Rows.GetEnumerator();

                try
                {
                    if (enumerator.MoveNext())
                    {
                        DataRow dataRow = (DataRow)enumerator.Current;
                        OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(dataRow["TABLE_NAME"].ToString(), oleDbConnection1);
                        oleDbDataAdapter.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
                        oleDbDataAdapter.AcceptChangesDuringFill = false;
                        srcTable = dataRow["TABLE_NAME"].ToString().Replace("$", string.Empty).Replace("'", string.Empty);

                        if (dataRow["TABLE_NAME"].ToString().Contains("$"))
                            oleDbDataAdapter.Fill(dataSet, srcTable);
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;

                    if (disposable != null)
                        disposable.Dispose();
                }

                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();

                foreach (DataColumn dataColumn in dataSet.Tables[0].Columns)
                {
                    if (stringBuilder1.Length > 0)
                    {
                        stringBuilder1.Append(",");
                        stringBuilder2.Append(",");
                    }
                    stringBuilder1.Append("[" + dataColumn.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
                    stringBuilder2.Append(dataColumn.ColumnName.Replace("'", "''"));
                }

                DataTable dataTable = this._flex.DataTable.Copy();

                if (!dataTable.Columns.Contains("CD_COMPANY"))
                    dataTable.Columns.Add("CD_COMPANY", typeof(string));

                dataTable.Columns.Add("DATAUPDATE", typeof(string));

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    dataRow["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    dataRow["DATAUPDATE"] = "UPDATE";
                    StringBuilder stringBuilder3 = new StringBuilder();

                    foreach (DataColumn dataColumn in dataSet.Tables[0].Columns)
                    {
                        if (dataTable.Columns.Contains(dataColumn.ColumnName))
                        {
                            if (stringBuilder3.Length > 0)
                                stringBuilder3.Append(",");
                            stringBuilder3.Append("'" + dataRow[dataColumn.ColumnName].ToString().Replace("'", "''") + "'");
                        }
                    }
                    new OleDbCommand("INSERT INTO [" + srcTable + "$](" + stringBuilder2.ToString() + ") VALUES (" + stringBuilder3.ToString() + ")", oleDbConnection1).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                if (oleDbConnection1 != null)
                    oleDbConnection1.Close();
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            int index;
            DataRow dataRow;
            DataTable dt, dt1;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                DataTable dt엑셀 = new Excel().StartLoadExcel(openFileDialog.FileName, 0, 3);

                if (dt엑셀 == null || dt엑셀.Rows.Count == 0)
                    return;

                dt = dt엑셀.DefaultView.ToTable(1 != 0, new string[] { "CD_COMPANY",
                                                                       "CD_PARTNER" });
                dt1 = dt엑셀.Clone();

                if (dt엑셀.Rows.Count != dt.Rows.Count)
                {
                    this.ShowMessage("엑셀자료에 중복데이터가 있습니다.\n회사코드와 거래처코드를 확인하세요");

                    foreach (DataRow row in dt.Rows)
                    {
                        if (dt엑셀.Select("CD_COMPANY = '" + D.GetString(row["CD_COMPANY"]) + "' AND CD_PARTNER = '" + D.GetString(row["CD_PARTNER"]) + "'").Length > 1)
                            dt1.ImportRow(row);
                    }

                    new P_CZ_MA_PARTNER_EXCEL_SUB(dt1, true).ShowDialog();
                }
                else
                {
                    dt1 = this.DataCheck(dt엑셀);

                    if (dt1.Rows.Count != 0)
                    {
                        new P_CZ_MA_PARTNER_EXCEL_SUB(dt1, false).ShowDialog();
                    }
                    else
                    {
                        this._flex.Redraw = false;

                        index = 0;
                        foreach (DataRow dr in dt엑셀.Rows)
                        {
                            MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dt엑셀.Rows.Count) });

                            dataRow = this._flex.DataTable.NewRow();
                            Data.DataCopy(dr, dataRow);

                            this._flex.DataTable.Rows.Add(dataRow);
                        }

                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn엑셀업로드.Text });

                        if (this._flex.HasNormalRow)
                            this.ToolBarSaveButtonEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
                MsgControl.CloseMsg();
            }
        }
 
        private void btn계좌자료_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(true, false) || !this._flex.HasNormalRow || (this._flex[this._flex.Row, "CD_PARTNER"] == null || D.GetString(this._flex[this._flex.Row, "CD_PARTNER"]) == ""))
                    return;

                P_CZ_MA_PARTNER_DEPOSIT_SUB pMaPartnerSub = new P_CZ_MA_PARTNER_DEPOSIT_SUB(D.GetString(this._flex[this._flex.Row, "CD_PARTNER"]), 
                                                                                            D.GetString(this._flex[this._flex.Row, "LN_PARTNER"]));
                pMaPartnerSub.Show();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn담당자추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;
                string @string = D.GetString(this._flex[this._flex.Row, "CD_PARTNER"]);
                if (@string == string.Empty) return;

                P_CZ_FI_PARTNERPTR_SUB pFiPartnerptrSub = new P_CZ_FI_PARTNERPTR_SUB(@string);
                pFiPartnerptrSub.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            if (!this._flex.HasNormalRow) return;

            P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "MA", "P_CZ_MA_PARTNER", D.GetString(this._flex["CD_PARTNER"]), "P_CZ_MA_PARTNER");
            dlg.ShowDialog(this);
        }

        private void btn동기화_Click(object sender, EventArgs e)
        {
            P_CZ_MA_PARTNER_COPY dialog;

            try
            {
                dialog = new P_CZ_MA_PARTNER_COPY(Global.MainFrame.LoginInfo.CompanyCode, 
                                                  Global.MainFrame.LoginInfo.CompanyName, 
                                                  D.GetString(this._flex["CD_PARTNER"]), 
                                                  D.GetString(this._flex["LN_PARTNER"]));
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn예금주휴페업_Click(object sender, EventArgs e)
        {
            string controlName;

            try
            {
                if (BasicInfo.ActiveDialog) return;

                controlName = ((Control)sender).Name;

                if (controlName == this.btn휴폐업조회.Name)
                    new P_CZ_MA_PARTNER_CLOSE(D.GetString(this._flex["CD_PARTNER"]), D.GetString(this._flex["LN_PARTNER"]), D.GetString(this._flex["NO_COMPANY"])).ShowDialog();
                else if (controlName == this.btn예금주조회.Name)
                    new P_CZ_MA_PARTNER_ACCOUNT(D.GetString(this._flex["CD_PARTNER"]), D.GetString(this._flex["LN_PARTNER"]), D.GetString(this._flex["NO_COMPANY"]), D.GetString(this._flex["CD_BANK"]), D.GetString(this._flex["NO_DEPOSIT"]), D.GetString(this._flex["NM_DEPOSIT"])).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn원산지추가_Click(object sender, EventArgs e)
        {
            string 원산지코드;

            try
            {
                원산지코드 = this._flex["CD_ORIGIN"].ToString();

                P_CZ_MA_PARTNER_ORIGIN_SUB dialog = new P_CZ_MA_PARTNER_ORIGIN_SUB(D.GetString(this._flex["CD_PARTNER"]));
                dialog.ShowDialog();

                this.cbo원산지.DataSource = Global.MainFrame.FillDataTable(@"SELECT '' AS CODE, '' AS NAME
                                                                             UNION ALL
                                                                             SELECT CD_ORIGIN AS CODE,
                                                                                    NM_ORIGIN AS NAME
                                                                             FROM CZ_MA_PARTNER_ORIGIN WITH(NOLOCK)
                                                                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                            "AND CD_PARTNER = '" + this._flex["CD_PARTNER"].ToString() + "'");
                this.cbo원산지.ValueMember = "CODE";
                this.cbo원산지.DisplayMember = "NAME";

                this.cbo원산지.SelectedValue = 원산지코드;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataTable DataCheck(DataTable dt엑셀)
        {
            DataTable dataTable1 = new DataTable();

            try
            {
                DataTable dataTable2 = this._biz.DataCode();
                bool flag = false;
                int num = 4, index;
                dataTable1.Columns.Add("ERRORLINE", typeof(string));
                dataTable1.Columns.Add("ERRORMSG", typeof(string));

                index = 0;

                foreach (DataRow dataRow1 in dt엑셀.Rows)
                {
                    DataRow row = dataTable1.NewRow();

                    MsgControl.ShowMsg("엑셀 데이터 확인 중입니다. (@/@)", new string[] { D.GetString(++index), D.GetString(dt엑셀.Rows.Count) });

                    foreach (DataColumn colIndex in dt엑셀.Columns)
                    {
                        switch (colIndex.ColumnName)
                        {
                            case "CD_COMPANY":
                                if (!(dataRow1["DATAUPDATE"].ToString() == "UPDATE"))
                                {
                                    string str = dataRow1[colIndex].ToString();

                                    if (str.Length > 7)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = string.Concat(new object[] { dataRow2["ERRORMSG"],
                                                                                                "[CD_COMPANY:회사코드] [",
                                                                                                str,
                                                                                                "] 회사 코드는 7자리 까지입니다. \n" });
                                    }

                                    if (str != Global.MainFrame.LoginInfo.CompanyCode)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = string.Concat(new object[] { dataRow2["ERRORMSG"],
                                                                                                "[CD_COMPANY:회사코드] [",
                                                                                                str,
                                                                                                "] 회사 코드는 로그인 회사가 아닙니다. \n" });
                                        flag = true;
                                    }

                                    if (str == "" || str == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_COMPANY:회사코드]는 필수입력사항입니다.\n");
                                        flag = true;
                                        break;
                                    }

                                    break;
                                }
                                break;
                            case "CD_PARTNER":
                                if (!(dataRow1["DATAUPDATE"].ToString() == "UPDATE"))
                                {
                                    string CODE = dataRow1[colIndex].ToString();

                                    if (this._getNamte.GetName(P_FI_GET_NAME.코드종류.마스터.거래처, CODE) != null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = string.Concat(new object[] { dataRow2["ERRORMSG"],
                                                                                                "[CD_PARTNER:거래처코드][",
                                                                                                CODE,
                                                                                                "] 거래처코드는 이미 등록된 코드입니다.\n" });
                                        flag = true;
                                    }

                                    if (CODE == "" || CODE == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_PARTNER:거래처코드]는 필수입력사항입니다.\n");
                                        flag = true;
                                    }

                                    if (CODE.Length > 20)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_PARTNER:거래처코드]는 20자리를 초과하였습니다.\n");
                                        flag = true;
                                        break;
                                    }
                                    break;
                                }
                                break;
                            case "LN_PARTNER":
                                if (!(dataRow1["DATAUPDATE"].ToString() == "UPDATE") && dataRow1[colIndex].ToString() == "")
                                {
                                    DataRow dataRow2;
                                    (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[LN_PARTNER:거래처명]은 필수입력사항입니다.\n");
                                    flag = true;
                                    break;
                                }
                                break;
                            case "NO_COMPANY":
                                if (!(dataRow1["DATAUPDATE"].ToString() == "UPDATE"))
                                {
                                    string str = dataRow1[colIndex].ToString();

                                    if (str == "" || str == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[NO_COMPANY:사업자등록번호]는 필수입력사항입니다.\n");
                                        flag = true;
                                    }

                                    if (str.Length > 20)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[NO_COMPANY:사업자등록번호]는 20자리를 초과하였습니다.\n");
                                        flag = true;
                                        break;
                                    }

                                    break;
                                }
                                break;
                            case "FG_PARTNER":
                                string str1 = dataRow1[colIndex].ToString();

                                if (str1 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str1 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_PARTNER:거래처구분]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str1.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_PARTNER:거래처구분]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "NO_RES":
                                if (dataRow1[colIndex].ToString().Length > 13)
                                {
                                    DataRow dataRow2;
                                    (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[NO_RES:주민등록번호]은 13자리를 초과하였습니다.\n");
                                    flag = true;
                                    break;
                                }
                                break;
                            case "NO_POST1":
                                if (dataRow1[colIndex].ToString().Length > 6)
                                {
                                    DataRow dataRow2;
                                    (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[NO_POST1:본사우편번호]은 6자리를 초과하였습니다.\n");
                                    flag = true;
                                    break;
                                }
                                break;
                            case "YN_BIZTAX":
                                string str2 = dataRow1[colIndex].ToString();

                                if (str2 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str2 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[YN_BIZTAX:사업자단위과세]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str2.Length > 1)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[YN_BIZTAX:사업자단위과세]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "YN_JEONJA":
                                string str3 = dataRow1[colIndex].ToString();

                                if (str3 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str3 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[YN_JEONJA:계산서발행형태]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str3.Length > 1)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[YN_JEONJA:계산서발행형태]은 1자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CD_BANK":
                                string str4 = dataRow1[colIndex].ToString();

                                if (str4 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str4 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_BANK:거래처지급은행]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str4.Length > 20)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_BANK:거래처지급은행]은 20자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CD_CON":
                                string str5 = dataRow1[colIndex].ToString();

                                if (str5 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str5 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_CON:휴폐업구분]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str5.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_CON:휴폐업구분]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "TP_NATION":
                                string str6 = dataRow1[colIndex].ToString();

                                if (str6 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str6 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_NATION:내외국인]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str6.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_NATION:내외국인]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CLS_PARTNER":
                                string str7 = dataRow1[colIndex].ToString();

                                if (str7 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str7 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CLS_PARTNER:거래처분류]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str7.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CLS_PARTNER:거래처분류]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "TP_PARTNER":
                                string str8 = dataRow1[colIndex].ToString();

                                if (str8 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str8 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_PARTNER:거래처타입]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str8.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_PARTNER:거래처타입]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CD_PARTNER_GRP":
                                string str9 = dataRow1[colIndex].ToString();

                                if (str9 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str9 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_PARTNER_GRP:거래처그룹]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str9.Length > 10)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_PARTNER_GRP:거래처그룹]은 10자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CD_PARTNER_GRP_2":
                                string str10 = dataRow1[colIndex].ToString();

                                if (str10 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str10 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_PARTNER_GRP_2:거래처그룹2]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str10.Length > 10)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_PARTNER_GRP_2:거래처그룹2]은 10자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "TP_TAX":
                                string str11 = dataRow1[colIndex].ToString();

                                if (str11 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str11 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_TAX:부가세계산법]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str11.Length > 10)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_TAX:부가세계산법]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "FG_BILL":
                                string str12 = dataRow1[colIndex].ToString();

                                if (str12 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str12 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_BILL:수금조건]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str12.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_BILL:수금조건]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "FG_PAYBILL":
                                string str13 = dataRow1[colIndex].ToString();

                                if (str13 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str13 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_PAYBILL:지급조건]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str13.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_PAYBILL:지급조건]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "FG_CREDIT":
                                string str14 = dataRow1[colIndex].ToString();

                                if (str14 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str14 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_CREDIT:예산관리]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str14.Length > 1)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_CREDIT:예산관리]은 1자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "TP_RCP_DD":
                                string str15 = dataRow1[colIndex].ToString();

                                if (str15 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str15 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_RCP_DD:자금수금예정일구분]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str15.Length > 2)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_RCP_DD:자금수금예정일구분]은 2자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "TP_PAY_DD":
                                string str16 = dataRow1[colIndex].ToString();

                                if (str16 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str16 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_PAY_DD:자금지급예정일구분]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str16.Length > 2)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_PAY_DD:자금지급예정일구분]은 2자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "TP_DEFER":
                                if (D.GetString(dataRow1[colIndex]) == string.Empty)
                                    dataRow1[colIndex] = "Y";

                                string str17 = dataRow1[colIndex].ToString();

                                if (str17 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str17 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_DEFER:지급보류여부]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str17.Length > 1)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[TP_DEFER:지급보류여부]은 1자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "FG_CORP":
                                string str18 = dataRow1[colIndex].ToString();

                                if (str18 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str18 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_CORP:연결법인구분]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str18.Length > 1)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[FG_CORP:연결법인구분]은 1자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CD_NATION":
                                string str19 = dataRow1[colIndex].ToString();

                                if (str19 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str19 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_NATION:국가명]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str19.Length > 20)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_NATION:국가명]은 20자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                            case "CD_AREA":
                                string str20 = dataRow1[colIndex].ToString();

                                if (str20 != string.Empty)
                                {
                                    if (dataTable2.Select("CD_SYSDEF = '" + str20 + "'") == null)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_AREA:지역구분]은 등록된 코드값 외의 값이 입력되었습니다.\n");
                                        flag = true;
                                    }

                                    if (str20.Length > 3)
                                    {
                                        DataRow dataRow2;
                                        (dataRow2 = row)["ERRORMSG"] = ((string)dataRow2["ERRORMSG"] + "[CD_AREA:지역구분]은 3자리를 초과하였습니다.\n");
                                        flag = true;
                                    }

                                    break;
                                }
                                break;
                        }
                    }
                    if (flag)
                    {
                        row["ERRORLINE"] = D.GetString((num++ + " 라인 에러 "));
                        dataTable1.Rows.Add(row);
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

            return dataTable1;
        }

        private void txt비고_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || e.KeyCode != Keys.Return || this._flex.Row - 1 < 0)
                    return;

                this._flex.DataTable.Rows[this._flex.Row - 1]["NM_TEXT"] = this.txt비고.Text;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt비고_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || this._flex.Row - 1 < 0)
                    return;

                this._flex.DataTable.Rows[this._flex.Row - 1]["NM_TEXT"] = this.txt비고.Text;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt검색_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData != Keys.Return)
                    return;

                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.ControlName == this.ctx수주형태.Name)
                {
                    e.HelpParam.P61_CODE1 = "C";
                    e.HelpParam.P62_CODE2 = "Y";
                }
                else if (e.ControlName == this.bct발주형태.Name)
                    e.HelpParam.P61_CODE1 = string.Empty;
                else if (e.ControlName == this.ctx사용자정의1.Name)
                    e.HelpParam.P41_CD_FIELD1 = "MA_DIN0001";
                else if (e.ControlName == this.ctx사용자정의2.Name)
                    e.HelpParam.P41_CD_FIELD1 = "MA_DIN0002";
                else if (e.ControlName == this.ctx사용자정의3.Name)
                    e.HelpParam.P41_CD_FIELD1 = "MA_DIN0003";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void meb사업자등록번호_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.meb사업자등록번호.Modified || this.meb사업자등록번호.Text == string.Empty)
                    return;

                string clipText = this.meb사업자등록번호.ClipText;

                if (clipText.Equals("8888888888") || clipText.Equals("9999999999"))
                    return;

                int row1 = this._flex.FindRow(clipText, this._flex.Rows.Fixed, this._flex.Cols["NO_COMPANY"].Index, true, true, false);

                if (row1 == -1)
                {
                    string 거래처코드;
                    string 거래처명;
                    this._biz.사업자등록번호체크(clipText, out 거래처코드, out 거래처명);

                    if (거래처코드 != string.Empty && !this.Ckeck사업자등록번호(거래처코드, 거래처명, e))
                        return;
                }
                else
                {
                    DataRow row2 = this._flex.DataView[this._flex.DataIndex(row1)].Row;
                    this.GetNoCompany중복메시지(D.GetString(row2["CD_PARTNER"]), D.GetString(row2["LN_PARTNER"]));

                    if (!this.Ckeck사업자등록번호(D.GetString(row2["CD_PARTNER"]), D.GetString(row2["LN_PARTNER"]), e))
                        return;
                }

                if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.CH || new CommonFunction().Chek_SaUpJang(clipText))
                    return;

                string str = string.Empty;

                if (this.ShowMessage(Global.CurLanguage != Language.KR ? "Error resident registration No. inquiry. Allow? \n허용하시겠습니까?" : "사업자등록번호가 올바르지 않습니다. \n허용하시겠습니까?", "QY2") == DialogResult.No)
                {
                    this.meb사업자등록번호.Clear();
                    e.Cancel = true;
                }
                else
                    this.cbo거래처구분.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Ckeck사업자등록번호(string 거래처코드, string 거래처명, CancelEventArgs e)
        {
            string noCompany중복메시지 = this.GetNoCompany중복메시지(거래처코드, 거래처명);

            if (this._Is사업자등록번호중복허용)
            {
                string str = string.Empty;

                if (this.ShowMessage(noCompany중복메시지 + "그래도 등록하시겠습니까?", "QY2") == DialogResult.Yes)
                    return true;

                this.meb사업자등록번호.Clear();
                e.Cancel = true;
                return false;
            }

            this.ShowMessage(noCompany중복메시지);
            this.meb사업자등록번호.Clear();
            e.Cancel = true;
            return false;
        }

        private string GetNoCompany중복메시지(string 거래처코드, string 거래처명)
        {
            string str = string.Empty;

            return "아래 거래처에 등록된 번호입니다. \n\n거래처코드 : " + 거래처코드 + "\n거래처명   : " + 거래처명 + "\n\n";
        }

        private void meb주민번호_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.CH)
                    return;

                if (this.meb주민번호.Text.Replace("-", "").Replace(" ", "").Replace("_", "") != "" && this.meb주민번호.ClipText.Length != 13)
                {
                    this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                    this.meb주민번호.Text = string.Empty;
                    this.meb주민번호.Focus();
                }
                else if (!new CommonFunction().CheckRegiNo(this.meb주민번호.ClipText) && this.meb주민번호.Text.Replace("-", "").Replace(" ", "").Replace("_", "") != "" && this.ShowMessage("주민등록번호가 맞지 않습니다. 허용 하시겠습니까?", "QY2") == DialogResult.No)
                {
                    this.meb주민번호.Text = string.Empty;
                    this.meb주민번호.Focus();
                }
                else
                {
                    string 등록유무 = string.Empty;
                    this._biz.주민번호체크(this.meb주민번호.ClipText, ref 등록유무);
                    if (등록유무 == "Y" && this.ShowMessage("이미 등록된 주민번호입니다.\n허용하시겠습니까?", "QY2") == DialogResult.No)
                    {
                        this.meb주민번호.Text = string.Empty;
                        this.meb주민번호.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnHelpClick(object sender, EventArgs e)
        {
            try
            {
                this.OnHelpClick(sender, "");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo연결법인구분.SelectedValue == null)
                    this.txt연결법인코드.Enabled = false;
                else if (this.cbo연결법인구분.SelectedValue.ToString() != "2")
                    this.txt연결법인코드.Enabled = true;
                else
                    this.txt연결법인코드.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo원산지_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //원산지 기본값 사용 안하도록 함
                //this._flex["CD_ORIGIN"] = this.cbo원산지.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드이벤트
        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (this.IsChanged())
                    this.SetToolBarButtonState(true, true, true, true, false);
                else
                    this.SetToolBarButtonState(true, true, true, false, true);

                if (this._flex.HasNormalRow)
                    return;

                this.ToolBarDeleteButtonEnabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_CodeSearch(object sender, CodeSearchEventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "b_NoPost1":
                        DataTable post1 = this._biz.GetPost(this.meb본사주소.Text.Replace("-", ""));

                        if (post1 == null || post1.Rows.Count == 0)
                        {
                            this.meb본사주소.Text = string.Empty;
                            this.txt본사주소.Text = string.Empty;
                            this.txt본사주소상세.Text = string.Empty;
                            this.OnHelpClick(sender, e.Text);
                            break;
                        }

                        if (post1 == null || post1.Rows.Count != 1)
                            break;

                        this.meb본사주소.Text = post1.Rows[0]["NO_POST"].ToString();
                        this.txt본사주소.Text = post1.Rows[0]["NM_ADDRESS"].ToString();
                        this.txt본사주소상세.Text = "";
                        break;
                    case "b_NoPost2":
                        DataTable post2 = this._biz.GetPost(this.meb영업주소.Text.Replace("-", ""));

                        if (post2 == null || post2.Rows.Count == 0)
                        {
                            this.meb영업주소.Text = string.Empty;
                            this.txt영업주소.Text = string.Empty;
                            this.txt영업주소상세.Text = string.Empty;
                            this.OnHelpClick(sender, e.Text);
                            break;
                        }

                        if (post2 == null || post2.Rows.Count != 1)
                            break;

                        this.meb영업주소.Text = post2.Rows[0]["NO_POST"].ToString();
                        this.txt영업주소.Text = post2.Rows[0]["NM_ADDRESS"].ToString();
                        this.txt영업주소상세.Text = "";
                        break;
                    case "b_NoPost3":
                        DataTable post3 = this._biz.GetPost(this.meb공장주소.Text.Replace("-", ""));

                        if (post3 == null || post3.Rows.Count == 0)
                        {
                            this.meb공장주소.Text = string.Empty;
                            this.txt공장주소.Text = string.Empty;
                            this.txt공장주소상세.Text = string.Empty;
                            this.OnHelpClick(sender, e.Text);
                            break;
                        }

                        if (post3 == null || post3.Rows.Count != 1)
                            break;

                        this.meb공장주소.Text = post3.Rows[0]["NO_POST"].ToString();
                        this.txt공장주소.Text = post3.Rows[0]["NM_ADDRESS"].ToString();
                        this.txt공장주소상세.Text = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            string 원산지코드;

            try
            {
                원산지코드 = this._flex["CD_ORIGIN"].ToString();

                this.cbo원산지.DataSource = Global.MainFrame.FillDataTable(@"SELECT '' AS CODE, '' AS NAME
                                                                             UNION ALL
                                                                             SELECT CD_ORIGIN AS CODE,
                                                                                    NM_ORIGIN AS NAME
                                                                             FROM CZ_MA_PARTNER_ORIGIN WITH(NOLOCK)
                                                                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                            "AND CD_PARTNER = '" + this._flex["CD_PARTNER"].ToString() + "'");
                this.cbo원산지.ValueMember = "CODE";
                this.cbo원산지.DisplayMember = "NAME";

                this.cbo원산지.SelectedValue = 원산지코드;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region 기타메소드
        private void Set언어에따른설정변경()
        {
            if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.CH) return;

            this.meb사업자등록번호.Mask = this.meb주민번호.Mask = this.meb본사주소.Mask = this.meb영업주소.Mask = this.meb공장주소.Mask = string.Empty;
            this.meb사업자등록번호.Text = this.meb주민번호.Text = this.meb본사주소.Text = this.meb영업주소.Text = this.meb공장주소.Text = string.Empty;
            this.meb사업자등록번호.MaxLength = this.meb주민번호.MaxLength = 20;
            this.meb본사주소.MaxLength = this.meb영업주소.MaxLength = this.meb공장주소.MaxLength = 6;
        }

        private bool MsgAndSave(bool displayDialog, bool isExit)
        {
            if (!this.IsChanged())
                return true;
            if (!displayDialog)
                return this.Save();
            if (!this.CanSave)
                return true;
            if (isExit)
            {
                switch (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까))
                {
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        return false;
                }
            }
            else if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까) == DialogResult.No)
                return true;
            
            return this.Save();
        }

        private bool Save()
        {
            if (!this.Check())
                return false;

            DataTable changes = this._flex.DataTable.GetChanges();

            if (changes == null || changes.Rows.Count == 0)
                return true;

            if (!this._biz.Save(changes))
                return false;

            for (int @fixed = this._flex.Rows.Fixed; @fixed < this._flex.Rows.Count; ++@fixed)
            {
                if (this._flex.IsChangedRow(@fixed))
                    this._flex[@fixed, "OLD_NO_COMPANY"] = this._flex[@fixed, "NO_COMPANY"];
            }

            this._flex.AcceptChanges();
            return true;
        }

        private bool Check()
        {
            if (!this._flex.CheckView_DeleteIfNull(new string[] { "CD_PARTNER", "LN_PARTNER" }, "AND"))
            {
                this.ShowMessage(공통메세지.변경된내용이없습니다);
                return false;
            }

            if (Checker.IsEmpty(this.txt거래처코드, "거래처코드") ||
                Checker.IsEmpty(this.txt거래처명, "거래처명") || 
                Checker.IsEmpty(this.cbo휴폐업구분, "휴폐업구분"))
                return false;

            int row;
            string colName;

            if (this._flex.CheckView_Compare(new string[] { "NO_POST1",
                                                            "NO_POST2",
                                                            "NO_POST3" }, "", "!=", new string[] { "DC_ADS1_H",
                                                                                                   "DC_ADS2_H",
                                                                                                   "DC_ADS3_H" }, "", "==", out row, out colName, "OR", new object[] { this.meb본사주소,
                                                                                                                                                                       this.meb영업주소,
                                                                                                                                                                       this.meb공장주소 }))
            {
                if (Global.CurLanguage == Language.KR)
                {
                    this.ShowMessage(this.DD(colName) + "를 바르게 입력하세요");
                }
                else
                {
                    this.ShowMessage(this.DD(colName) + "is incorrect.");
                }

                this._flex.Select(row, colName);
                return false;
            }

            if (this._flex.CheckView_HasDup(new string[] { "CD_PARTNER" }, out row))
            {
                this.ShowMessage("거래처코드 " + D.GetString(this._flex[row, "CD_PARTNER"]) + " 이(가) 중복되었습니다.");
                this._flex.Select(row, "CD_PARTNER");
                this._flex.Focus();
                return false;
            }
            return true;
        }

        private void OnHelpClick(object sender, string psSearch)
        {
            if (BasicInfo.ActiveDialog) return;

            string controlName;

            try
            {
                this._flex.DataIndex();

                controlName = ((Control)sender).Name;

                if (controlName == this.btn본사주소.Name)
                {
                    object obj1 = this.LoadHelpWindow("P_MA_POST", new object[] { this.MainFrameInterface, 
                                                                                  psSearch });

                    if (((Form)obj1).ShowDialog() == DialogResult.OK && obj1 is IHelpWindow)
                    {
                        this.meb본사주소.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[0]) + D.GetString(((IHelpWindow)obj1).ReturnValues[1]);
                        this.txt본사주소.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[2]).Trim();
                        this.txt본사주소상세.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[3]);
                        this.txt본사주소상세.Focus();
                    }
                }
                else if (controlName == this.btn영업주소.Name)
                {
                    object obj2 = this.LoadHelpWindow("P_MA_POST", new object[] { this.MainFrameInterface,
                                                                                  psSearch });

                    if (((Form)obj2).ShowDialog() == DialogResult.OK && obj2 is IHelpWindow)
                    {
                        this.meb영업주소.Text = D.GetString(((IHelpWindow)obj2).ReturnValues[0]) + D.GetString(((IHelpWindow)obj2).ReturnValues[1]);
                        this.txt영업주소.Text = D.GetString(((IHelpWindow)obj2).ReturnValues[2]).Trim();
                        this.txt영업주소상세.Text = D.GetString(((IHelpWindow)obj2).ReturnValues[3]);
                        this.txt영업주소상세.Focus();
                    }
                }
                else if (controlName == this.btn공장주소.Name)
                {
                    object obj3 = this.LoadHelpWindow("P_MA_POST", new object[] { this.MainFrameInterface,
                                                                                  psSearch });

                    if (((Form)obj3).ShowDialog() == DialogResult.OK && obj3 is IHelpWindow)
                    {
                        this.meb공장주소.Text = D.GetString(((IHelpWindow)obj3).ReturnValues[0]) + D.GetString(((IHelpWindow)obj3).ReturnValues[1]);
                        this.txt공장주소.Text = D.GetString(((IHelpWindow)obj3).ReturnValues[2]).Trim();
                        this.txt공장주소상세.Text = D.GetString(((IHelpWindow)obj3).ReturnValues[3]);
                        this.txt공장주소상세.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetControlEnable(bool enabled)
        {
            this.SetControlBase(this.one기본정보, enabled);
            this.SetControlBase(this.one세금계산서기본정보, enabled);
            this.SetControlBase(this.one전자세금계산서발행정보, enabled);
            this.SetControlBase(this.one계좌정보, enabled);
            this.SetControlBase(this.one기타정보, enabled);
            this.SetControlBase(this.one부가정보, enabled);
            this.SetControlBase(this.one기타정보1, enabled);
            this.SetControlBase(this.pnl비고, enabled);

            this.btn본사주소.Enabled = enabled;
            this.btn영업주소.Enabled = enabled;
            this.btn공장주소.Enabled = enabled;
            this.cbo계좌구분.Enabled = false;
            this.cbo은행.Enabled = false;
            this.cbo은행소재국.Enabled = false;
            this.cbo예금주국적.Enabled = false;

            if (enabled)
            {
                UGrant ugrant = new UGrant();
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "PTR", this.btn담당자추가, true);
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "DEPOSIT", this.btn계좌자료, true);
                this.cbo사용여부.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "USE");
                this.cbo협조전사용여부.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "USE");
                this.meb사업자등록번호.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "USE");
                this.cbo계산서발행형태.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "INV");
                this.cbo매입결제조건.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "PAYMENT");
                this.cur수금예정일.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "DT_RCP_DD");
                this.cur수금예정허용일.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "DT_RCP_PRE");
                this.cur지급예정일.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "DT_PAY_DD");
                this.cur매입DC율.Enabled = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "DC");
            }
            else
            {
                this.btn담당자추가.Enabled = enabled;
                this.btn계좌자료.Enabled = enabled;
                this.cbo사용여부.Enabled = enabled;
                this.cbo협조전사용여부.Enabled = enabled;
                this.meb사업자등록번호.Enabled = enabled;
                this.cbo계산서발행형태.Enabled = enabled;
                this.cbo매입결제조건.Enabled = enabled;
                this.cur수금예정일.Enabled = enabled;
                this.cur수금예정허용일.Enabled = enabled;
                this.cur지급예정일.Enabled = enabled;
                this.cur매입DC율.Enabled = enabled;
            }
        }

        private void SetControlBase(Control ctrls, bool enabled)
        {
            Control.ControlCollection controlCollection = null;

            if (ctrls is OneGrid)
                controlCollection = (ctrls as OneGrid).Controls;
            else if (ctrls is OneGridItem)
                controlCollection = (ctrls as OneGridItem).Controls;
            else if (ctrls is BpPanelControl)
                controlCollection = (ctrls as BpPanelControl).Controls;
            else if (ctrls is Panel)
                controlCollection = (ctrls as Panel).Controls;
            else if (ctrls is FlowLayoutPanel)
                controlCollection = (ctrls as FlowLayoutPanel).Controls;

            foreach (Control ctrls1 in controlCollection)
            {
                if (ctrls1.GetType().Name == "CurrencyTextBox")
                    ctrls1.Enabled = enabled;
                else if (ctrls1.GetType().Name == "MaskedEditBox")
                    ctrls1.Enabled = enabled;
                else if (ctrls1.GetType().Name == "TextBoxExt")
                    ctrls1.Enabled = enabled;
                else if (ctrls1.GetType().Name == "DatePicker")
                    ctrls1.Enabled = enabled;
                else if (ctrls1.GetType().Name == "BpCodeTextBox")
                    ((BpCodeTextBox)ctrls1).Enabled = enabled;
                else if (ctrls1.GetType().Name == "DropDownComboBox")
                    ctrls1.Enabled = enabled;
                else if (ctrls1.GetType().Name == "CheckBoxExt")
                    ctrls1.Enabled = enabled;
                else if (ctrls1.GetType().Name == "RadioButtonExt")
                    ctrls1.Enabled = enabled;
                else if (ctrls1 is BpPanelControl || ctrls1 is OneGrid || ctrls1 is OneGridItem || ctrls1.GetType().Name == "FlexibleRoundedCornerBox" || ctrls1 is FlowLayoutPanel)
                    this.SetControlBase(ctrls1, enabled);
            }
        }
		#endregion
	}
}