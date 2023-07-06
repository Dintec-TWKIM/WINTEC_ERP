using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.OLD;
using System.Data.OleDb;
using System.Net;
using Duzon.ERPU.SA.Settng;
using System.Drawing;
using Duzon.Common.Forms.Help;
using System.Linq;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_REQ : PageBase
    {
        P_CZ_MA_PARTNER_REQ_BIZ _biz = new P_CZ_MA_PARTNER_REQ_BIZ();
        P_CZ_MA_PARTNER_GW _gw = new P_CZ_MA_PARTNER_GW();
        P_CZ_MA_PARTNER_REQ_CHECK _dialog;

        public P_CZ_MA_PARTNER_REQ()
        {
			if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
				StartUp.Certify(this);

            InitializeComponent();
        }

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            UGrant ugrant = new UGrant();

            if (ugrant.GrantButton("P_CZ_MA_PARTNER", "CONFIRM") == false || 
                Global.MainFrame.LoginInfo.CompanyCode == "W100")
            {
                this.bpc회사.Enabled = false;
                this.bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode,
                                     Global.MainFrame.LoginInfo.CompanyName);
                
            }

            ugrant.GrantButtonVisible("P_CZ_MA_PARTNER", "CONFIRM", this.btn승인);
            ugrant.GrantButtonVisible("P_CZ_MA_PARTNER", "REJECT", this.btn반려);
            ugrant.GrantButtonVisible("P_CZ_MA_PARTNER", "ROLLBACK", this.btn취소);
            ugrant.GrantButtonVisible("P_CZ_MA_PARTNER", "SYNC", this.btn동기화);
            ugrant.GrantButtonVisible("P_CZ_MA_PARTNER", "FILE", this.btn첨부파일);

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NM_GW_STATUS", "결재상태", 60);
            this._flex.SetCol("YN_CONFIRM", "처리여부", 40, false, CheckTypeEnum.Y_N);
            this._flex.SetCol("NM_COMPANY", "신청회사", 150);
            this._flex.SetCol("NO_REQ", "신청번호", 120);
            this._flex.SetCol("CD_PARTNER", "거래처코드", 100);
            this._flex.SetCol("LN_PARTNER", "거래처명", 160);
            this._flex.SetCol("NO_COMPANY", "사업자등록번호", 100);
            this._flex.SetCol("FG_PARTNER", "거래처구분", false);
            this._flex.SetCol("CLS_PARTNER", "거래처분류", false);
            this._flex.SetCol("QT_PARTNER", "동기화", false);
            this._flex.SetCol("APP_DOC_ID", "문서번호", false);
            this._flex.SetCol("NM_INSERT", "등록자", 80);
            this._flex.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_UPDATE", "수정자", 80);
            this._flex.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.Cols["NO_COMPANY"].Format = "0##-##-#####";
            this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flex.VerifyPrimaryKey = new string[] { "LN_PARTNER", "NO_COMPANY" };

            this._flex.SetDummyColumn("S");
            this._flex.SetOneGridBinding(new object[] { this.txt거래처명, this.meb사업자등록번호 }, new IUParentControl[] { this.one기본정보,
                                                                                                                            this.one담당자,
                                                                                                                            this.one계좌정보,
                                                                                                                            this.one매입정보,
                                                                                                                            this.one매출정보,
                                                                                                                            this.pnl비고 });

            this._flex.SettingVersion = "1.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {

            this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
            this.btn중복확인.Click += new EventHandler(this.btn중복확인_Click);
            this.btn승인.Click += new EventHandler(this.btn승인_Click);
            this.btn반려.Click += new EventHandler(this.btn반려_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
            this.btn동기화.Click += new EventHandler(this.btn동기화_Click);
            this.btn본사주소.Click += new EventHandler(this.btn본사주소_Click);

            this.cbo거래처구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo거래처분류.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo계좌구분.SelectionChangeCommitted += new EventHandler(this.cbo계좌구분_SelectionChangeCommitted);
            this.cbo은행.SelectionChangeCommitted += new EventHandler(this.cbo은행_SelectionChangeCommitted);

            this.txt거래처명.Validating += new CancelEventHandler(this.txt거래처명_Validating);
            this.meb사업자등록번호.Validating += new CancelEventHandler(this.meb사업자등록번호_Validating);
            this.meb주민등록번호.Validating += new CancelEventHandler(this.meb주민번호_Validating);

            this.ctx수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx발주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flex.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.splitContainer1.SplitterDistance = 939;

            SetControl setControl = new SetControl();

            setControl.SetCombobox(this.cbo거래처구분, new DataView(MA.GetCode("MA_B000001", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처분류, new DataView(MA.GetCode("MA_B000003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo지역구분, new DataView(MA.GetCode("MA_B000062", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo국가명, new DataView(MA.GetCode("MA_B000020", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo계좌구분, new DataView(MA.GetCode("MA_B000079", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo은행, new DataView(MA.GetCode("MA_B000043", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo은행소재국, new DataView(MA.GetCode("CZ_MA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo예금주국적, new DataView(MA.GetCode("CZ_MA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처그룹, new DataView(MA.GetCode("MA_B000065", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo거래처그룹2, new DataView(MA.GetCode("MA_B000067", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo담당유형, new DataView(MA.GetCode("CZ_MA00011", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboINQ수신방법, new DataView(MA.GetCode("CZ_SA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboQTN발신방법, new DataView(MA.GetCode("CZ_SA00017", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출선적조건, new DataView(MA.GetCode("TR_IM00003", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출결재조건, new DataView(MA.GetCode("CZ_SA00013", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출통화, new DataView(MA.GetCode("MA_B000005", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매출부가세, new DataView(MA.GetCode("MA_B000040", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입가격조건, new DataView(MA.GetCode("TR_IM00002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입통화, new DataView(MA.GetCode("MA_B000005", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboINQ발신방법, new DataView(MA.GetCode("CZ_PU00001", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboPO발신방법, new DataView(MA.GetCode("CZ_PU00002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입통화, new DataView(MA.GetCode("MA_B000005", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입결재조건, new DataView(MA.GetCode("PU_C000014", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo매입부가세, new DataView(MA.GetCode("MA_B000046", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cbo계산서발행형태, new DataView(MA.GetCode("MA_B000095", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());

            this.cbo처리여부.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "처리", "미처리" }, true);
            this.cbo처리여부.DisplayMember = "NAME";
            this.cbo처리여부.ValueMember = "CODE";

            this.cbo결재상태.DataSource = MA.GetCode("PU_C000065", true);
            this.cbo결재상태.DisplayMember = "NAME";
            this.cbo결재상태.ValueMember = "CODE";

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

            if (Global.MainFrame.LoginInfo.CompanyCode == "W100")
                this.btn전자결재.Visible = false;
        }
        #endregion

        #region 메인버튼이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { this.bpc회사.QueryWhereIn_Pipe,
                                                                     D.GetString(this.cbo처리여부.SelectedValue),
                                                                     D.GetString(this.cbo결재상태.SelectedValue),
                                                                     this.txt거래처명S.Text });

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

                this.btn엑셀양식다운로드.Enabled = true;
                this.btn엑셀업로드.Enabled = true;
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

                if (!BeforeAdd()) return;

                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex["NM_COMPANY"] = Global.MainFrame.LoginInfo.CompanyName;
                
                this._flex.AddFinished();
                this._flex.Col = 1;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		protected override bool BeforeSave()
		{
            DataTable dt = this._flex.GetChanges();

            if (dt.Select().AsEnumerable().Where(x => Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", x["E_MAIL"].ToString()))) == 0).Count() > 0)
            {
                this.ShowMessage("담당자 => 메일주소에 형식에 맞지 않은 이메일 주소가 입력 되어 있습니다.");
                return false;
            }

            if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
			{
                foreach (DataRow dr in dt.Select(string.Empty, string.Empty, DataViewRowState.Added))
                {
                    if (this.필수값확인(dr) == false)
                        return false;
                }
            }

            return base.BeforeSave();
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
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex.IsDataChanged == false) return false;

            DataTable dt = this._flex.GetChanges();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    dr["NO_REQ"] = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "05", Global.MainFrame.GetStringToday.Substring(2, 4));
                }
            }

            if (!this._biz.Save(dt)) return false;

            this._flex.AcceptChanges();

            return true;
        }

        private bool 계좌정보필수값확인(DataRow dr)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(dr["CD_DEPOSIT"])))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계좌구분.Text);
                    return false;
                }

                if (string.IsNullOrEmpty(D.GetString(dr["NO_DEPOSIT"])))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계좌번호.Text);
                    return false;
                }

                if (string.IsNullOrEmpty(D.GetString(dr["CD_BANK"])))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl은행.Text);
                    return false;
                }

                if (string.IsNullOrEmpty(D.GetString(dr["NM_DEPOSIT"])))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl예금주.Text);
                    return false;
                }

                if (D.GetString(dr["CD_DEPOSIT"]) == "002")
                {
                    if (string.IsNullOrEmpty(D.GetString(dr["NM_BANK"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl은행명.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(D.GetString(dr["NO_SORT"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl은행코드.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(D.GetString(dr["NO_SWIFT"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblSwift코드.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(D.GetString(dr["CD_BANK_NATION"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl은행소재국.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(D.GetString(dr["CD_DEPOSIT_NATION"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl예금주국적.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(D.GetString(dr["DC_DEPOSIT_TEL"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl예금주전화번호.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(D.GetString(dr["DC_DEPOSIT_ADDRESS"])))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl예금주주소.Text);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            string 결재상태;
            int index;
            DataRow[] dataRowArray;

            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex.HasNormalRow) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    index = 0;
                    this._flex.Redraw = false;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        결재상태 = D.GetString(dr["CD_GW_STATUS"]);

                        if (결재상태 == "0" || 결재상태 == "1")
                        {
                            this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flex["NM_GW_STATUS"]));
                            MsgControl.CloseMsg();
                            return;
                        }

                        dr.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flex.HasNormalRow) return;

                this._gw.미리보기(this._flex.GetDataRow(this._flex.Row), new object[] { this.cbo거래처구분.Text,
                                                                                        this.cbo거래처분류.Text,
                                                                                        this.cbo지역구분.Text,
                                                                                        this.cbo국가명.Text,
                                                                                        this.cbo거래처그룹.Text,
                                                                                        this.cbo거래처그룹2.Text,
                                                                                        this.cbo매입통화.Text,
                                                                                        this.cboINQ발신방법.Text,
                                                                                        this.cboPO발신방법.Text,
                                                                                        this.cbo매입결재조건.Text,
                                                                                        this.cbo매입가격조건.Text,
                                                                                        this.cbo매입부가세.Text,
                                                                                        this.cbo매출통화.Text,
                                                                                        this.cboINQ수신방법.Text,
                                                                                        this.cboQTN발신방법.Text,
                                                                                        this.cbo매출결재조건.Text,
                                                                                        this.cbo매출선적조건.Text,
                                                                                        this.cbo매출부가세.Text,
                                                                                        this.cbo담당유형.Text,
                                                                                        this.cbo계좌구분.Text,
                                                                                        this.cbo은행.Text,
                                                                                        this.cbo은행소재국.Text,
                                                                                        this.cbo예금주국적.Text,
                                                                                        this.cboINV발송방법.Text,
                                                                                        this.cbo계산서발행형태.Text,
                                                                                        this.txt원산지.Text,
                                                                                        this.txt원산지_출력물.Text });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드이벤트
        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this._flex["CD_PARTNER"])) && D.GetString(this._flex["CD_GW_STATUS"]) != "0" && D.GetString(this._flex["CD_GW_STATUS"]) != "1")
                {
                    #region 활성화
                    this.cbo거래처구분.Enabled = true;
                    this.cbo거래처분류.Enabled = true;
                    this.txt거래처명.ReadOnly = false;
                    this.meb사업자등록번호.ReadOnly = false;
                    this.txt대표자명.ReadOnly = false;
                    this.meb주민등록번호.ReadOnly = false;
                    this.cbo지역구분.Enabled = true;
                    this.cbo국가명.Enabled = true;
                    this.meb본사주소.ReadOnly = false;
                    this.txt본사주소.ReadOnly = false;
                    this.txt본사주소상세.ReadOnly = false;
                    this.cbo거래처그룹.Enabled = true;
                    this.cbo거래처그룹2.Enabled = true;
                    this.txt본사전화번호.ReadOnly = false;
                    this.txt본사FAX번호.ReadOnly = false;
                    this.txt업태.ReadOnly = false;
                    this.txt업종.ReadOnly = false;

                    this.cbo담당유형.Enabled = true;
                    this.txt담당자명.ReadOnly = false;
                    this.txt휴대폰번호.ReadOnly = false;
                    this.txt전화번호.ReadOnly = false;
                    this.txt메일주소.ReadOnly = false;
                    this.txtFAX번호.ReadOnly = false;

                    this.cbo계좌구분.Enabled = true;
                    this.txt계좌번호.ReadOnly = false;
                    this.cbo은행.Enabled = true;
                    this.txt은행코드.ReadOnly = false;
                    this.cbo은행소재국.Enabled = true;
                    this.txtSwift코드.ReadOnly = false;
                    this.cbo예금주국적.Enabled = true;
                    this.txt예금주전화번호.ReadOnly = false;
                    this.txt은행명.ReadOnly = false;
                    this.txt예금주.ReadOnly = false;
                    this.txt예금주주소.ReadOnly = false;
                    this.txt중계은행.ReadOnly = false;
                    this.txt계좌비고.ReadOnly = false;

                    this.ctx발주형태.ReadOnly = ReadOnly.None;
                    this.cbo매입통화.Enabled = true;
                    this.cboINQ발신방법.Enabled = true;
                    this.cboPO발신방법.Enabled = true;
                    this.cbo매입결재조건.Enabled = true;
                    this.cbo매입가격조건.Enabled = true;
                    this.cbo매입부가세.Enabled = true;
                    this.cur매입DC율.ReadOnly = false;
                    this.txt원산지.ReadOnly = false;
                    this.txt원산지_출력물.ReadOnly = false;

                    this.ctx수주형태.ReadOnly = ReadOnly.None;
                    this.cbo매출통화.Enabled = true;
                    this.cboINQ수신방법.Enabled = true;
                    this.cboQTN발신방법.Enabled = true;
                    this.cbo매출결재조건.Enabled = true;
                    this.cbo매출선적조건.Enabled = true;
                    this.cur매출DC율.ReadOnly = false;
                    this.cur매출마진율.ReadOnly = false;
                    this.txt매출커미션비고.ReadOnly = false;
                    this.cur매출커미션율.ReadOnly = false;
                    this.cbo매출부가세.Enabled = true;
                    this.cboINV발송방법.Enabled = true;
                    this.cbo계산서발행형태.Enabled = true;
                    #endregion
                }
                else
                {
                    #region 비활성화
                    this.cbo거래처구분.Enabled = false;
                    this.cbo거래처분류.Enabled = false;
                    this.txt거래처명.ReadOnly = true;
                    this.meb사업자등록번호.ReadOnly = true;
                    this.txt대표자명.ReadOnly = true;
                    this.meb주민등록번호.ReadOnly = true;
                    this.cbo지역구분.Enabled = false;
                    this.cbo국가명.Enabled = false;
                    this.meb본사주소.ReadOnly = true;
                    this.txt본사주소.ReadOnly = true;
                    this.txt본사주소상세.ReadOnly = true;
                    this.cbo거래처그룹.Enabled = false;
                    this.cbo거래처그룹2.Enabled = false;
                    this.txt본사전화번호.ReadOnly = true;
                    this.txt본사FAX번호.ReadOnly = true;
                    this.txt업태.ReadOnly = true;
                    this.txt업종.ReadOnly = true;

                    this.cbo담당유형.Enabled = false;
                    this.txt담당자명.ReadOnly = true;
                    this.txt휴대폰번호.ReadOnly = true;
                    this.txt전화번호.ReadOnly = true;
                    this.txt메일주소.ReadOnly = true;
                    this.txtFAX번호.ReadOnly = true;

                    this.cbo계좌구분.Enabled = false;
                    this.txt계좌번호.ReadOnly = true;
                    this.cbo은행.Enabled = false;
                    this.txt은행코드.ReadOnly = true;
                    this.cbo은행소재국.Enabled = false;
                    this.txtSwift코드.ReadOnly = true;
                    this.cbo예금주국적.Enabled = false;
                    this.txt예금주전화번호.ReadOnly = true;
                    this.txt은행명.ReadOnly = true;
                    this.txt예금주.ReadOnly = true;
                    this.txt예금주주소.ReadOnly = true;
                    this.txt중계은행.ReadOnly = true;
                    this.txt계좌비고.ReadOnly = true;

                    this.ctx발주형태.ReadOnly = ReadOnly.TotalReadOnly;
                    this.cbo매입통화.Enabled = false;
                    this.cboINQ발신방법.Enabled = false;
                    this.cboPO발신방법.Enabled = false;
                    this.cbo매입결재조건.Enabled = false;
                    this.cbo매입가격조건.Enabled = false;
                    this.cbo매입부가세.Enabled = false;
                    this.cur매입DC율.ReadOnly = true;
                    this.txt원산지.ReadOnly = true;
                    this.txt원산지_출력물.ReadOnly = true;

                    this.ctx수주형태.ReadOnly = ReadOnly.TotalReadOnly;
                    this.cbo매출통화.Enabled = false;
                    this.cboINQ수신방법.Enabled = false;
                    this.cboQTN발신방법.Enabled = false;
                    this.cbo매출결재조건.Enabled = false;
                    this.cbo매출선적조건.Enabled = false;
                    this.cur매출DC율.ReadOnly = true;
                    this.cur매출마진율.ReadOnly = true;
                    this.txt매출커미션비고.ReadOnly = true;
                    this.cur매출커미션율.ReadOnly = true;
                    this.cbo매출부가세.Enabled = false;
                    this.cboINV발송방법.Enabled = false;
                    this.cbo계산서발행형태.Enabled = false;
                    #endregion
                }

                if ((D.GetString(this._flex["CD_COMPANY"]) == "W100" || D.GetString(this._flex["CD_GW_STATUS"]) == "1") && D.GetString(this._flex["YN_CONFIRM"]) != "Y")
                {
                    this.btn승인.Enabled = true;
                    this.btn반려.Enabled = true;
                }
                else
                {
                    this.btn승인.Enabled = false;
                    this.btn반려.Enabled = false;
                }

                if (D.GetString(this._flex["YN_CONFIRM"]) == "Y")
                {
                    this.btn취소.Enabled = true;

                    if (D.GetString(this._flex["CD_PARTNER"]) != "REJECTED")
                        this.btn첨부파일.Enabled = true;
                    else
                        this.btn첨부파일.Enabled = false;
                }
                else
                {
                    this.btn첨부파일.Enabled = false;
                    this.btn취소.Enabled = false;
                }

                this.계좌정보필수값설정();
                this.거래처분류필수값설정();
                this.ControlEnable();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string pageId, pageName;

            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this._flex.MouseRow < this._flex.Rows.Fixed) return;
                if (this._flex.ColSel != this._flex.Cols["CD_PARTNER"].Index) return;
                if (string.IsNullOrEmpty(D.GetString(this._flex["CD_PARTNER"]))) return;
                
                pageId = "P_CZ_MA_PARTNER";
                pageName = this.DD("거래처정보관리");
                
                if (Global.MainFrame.IsExistPage(pageId, false))
                    Global.MainFrame.UnLoadPage(pageId, false);

                Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flex["CD_PARTNER"]) });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_거래처등록신청_" + Global.MainFrame.GetStringToday + ".xls";
                string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_PARTNER_REQ.xls";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                this.ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            int index;
            DataRow dataRow;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                DataTable dt엑셀 = new Excel().StartLoadExcel(openFileDialog.FileName, 0, 3);

                if (dt엑셀 == null || dt엑셀.Rows.Count == 0)
                    return;

                this._flex.Redraw = false;

                index = 0;
                foreach (DataRow dr in dt엑셀.Rows)
                {
                    MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dt엑셀.Rows.Count) });

                    dataRow = this._flex.DataTable.NewRow();
                    Data.DataCopy(dr, dataRow);

                    dataRow["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    dataRow["NM_COMPANY"] = Global.MainFrame.LoginInfo.CompanyName;

                    this._flex.DataTable.Rows.Add(dataRow);
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn엑셀업로드.Text });

                if (this._flex.HasNormalRow)
                    this.ToolBarSaveButtonEnabled = true;
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

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            if (!this._flex.HasNormalRow) return;
            if (string.IsNullOrEmpty(D.GetString(this._flex["CD_PARTNER"]))) return;

            P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(D.GetString(this._flex["CD_COMPANY"]), "MA", "P_CZ_MA_PARTNER", D.GetString(this._flex["CD_PARTNER"]), "P_CZ_MA_PARTNER");
            dlg.ShowDialog(this);
        }

        private void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (string.IsNullOrEmpty(D.GetString(this._flex["NO_REQ"])))
                {
                    this.ShowMessage("신청번호가 없습니다.");
                    return;
                }

                if (this._flex.GetDataRow(this._flex.Row).RowState != DataRowState.Unchanged)
                {
                    if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까) == DialogResult.No)
                        return;
                    else
                        this.OnToolBarSaveButtonClicked(null, null);
                }

                if (this.필수값확인(this._flex.GetDataRow(this._flex.Row)) == false) return;

                if (this._gw.전자결재(this._flex.GetDataRow(this._flex.Row), new object[] { this.cbo거래처구분.Text,
                                                                                            this.cbo거래처분류.Text,
                                                                                            this.cbo지역구분.Text,
                                                                                            this.cbo국가명.Text,
                                                                                            this.cbo거래처그룹.Text,
                                                                                            this.cbo거래처그룹2.Text,
                                                                                            this.cbo매입통화.Text,
                                                                                            this.cboINQ발신방법.Text,
                                                                                            this.cboPO발신방법.Text,
                                                                                            this.cbo매입결재조건.Text,
                                                                                            this.cbo매입가격조건.Text,
                                                                                            this.cbo매입부가세.Text,
                                                                                            this.cbo매출통화.Text,
                                                                                            this.cboINQ수신방법.Text,
                                                                                            this.cboQTN발신방법.Text,
                                                                                            this.cbo매출결재조건.Text,
                                                                                            this.cbo매출선적조건.Text,
                                                                                            this.cbo매출부가세.Text,
                                                                                            this.cbo담당유형.Text,
                                                                                            this.cbo계좌구분.Text,
                                                                                            this.cbo은행.Text,
                                                                                            this.cbo은행소재국.Text,
                                                                                            this.cbo예금주국적.Text,
                                                                                            this.cboINV발송방법.Text,
                                                                                            this.cbo계산서발행형태.Text,
                                                                                            this.txt원산지.Text,
                                                                                            this.txt원산지_출력물.Text }))
                {
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn중복확인_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dialog == null || this._dialog.IsDisposed == true)
                {
                    this._dialog = new P_CZ_MA_PARTNER_REQ_CHECK(D.GetString(this._flex["LN_PARTNER"]));
                    this._dialog.Show();
                }
                else
                {
                    this._dialog.재조회(D.GetString(this._flex["LN_PARTNER"]));
                    this._dialog.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        
        private void btn승인_Click(object sender, EventArgs e)
        {
            string 거래처코드, contents;
            int index;
            DataRow[] dataRowArray;
            
            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this.ShowMessage("승인 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        거래처코드 = this._biz.승인(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["NO_REQ"]));

                        contents = @"** 거래처등록 알림

신청번호 : {0}

신규거래처번호 : {1}
거래처명 : {2}

위와 같이 거래처 등록이 완료 되었습니다.

참고하시기 바랍니다.

※ 본 쪽지는 발신 전용 입니다.";

                        contents = string.Format(contents, D.GetString(dr["NO_REQ"]),
                                                           거래처코드,
                                                           D.GetString(dr["LN_PARTNER"]));

                        if (D.GetString(dr["CD_COMPANY"]) != "W100")
                            Messenger.SendMSG(new string[] { D.GetString(dr["ID_INSERT"]) }, contents);
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn승인.Text);
                this.OnToolBarSearchButtonClicked(null, null);
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

        private void btn반려_Click(object sender, EventArgs e)
        {
            string contents;
            int index;
            DataRow[] dataRowArray;

            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this.ShowMessage("반려 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        this._biz.반려(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["NO_REQ"]));

                        contents = @"** 거래처등록 거절 알림

신청번호 : {0}
거래처명 : {1}
사유 : {2}

위와 같이 거래처 등록이 거절 되었습니다.

참고하시기 바랍니다.

※ 본 쪽지는 발신 전용 입니다.";

                        contents = string.Format(contents, D.GetString(dr["NO_REQ"]),
                                                           D.GetString(dr["LN_PARTNER"]),
                                                           D.GetString(dr["NM_TEXT"]));

                        if (D.GetString(dr["CD_COMPANY"]) != "W100")
                            Messenger.SendMSG(new string[] { D.GetString(dr["ID_INSERT"]) }, contents);
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn반려.Text);
                this.OnToolBarSearchButtonClicked(null, null);
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

        private void btn취소_Click(object sender, EventArgs e)
        {
            int index;
            DataRow[] dataRowArray;

            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this.ShowMessage("취소 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });
                        this._biz.취소(D.GetString(dr["CD_COMPANY"]), D.GetString(dr["NO_REQ"]), D.GetString(dr["CD_PARTNER"]));
                    }
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn취소.Text);
                this.OnToolBarSearchButtonClicked(null, null);
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

        private void btn동기화_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            int index;
            string 대상회사;

            try
            {
                if (!this._flex.HasNormalRow) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y' AND ISNULL(CD_PARTNER, '') <> '' AND CD_PARTNER <> 'REJECTED'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        if (D.GetString(dr["CD_COMPANY"]) == "K100")
                            대상회사 = "TEST|K200|S100|W100";
                        else if (D.GetString(dr["CD_COMPANY"]) == "K200")
                            대상회사 = "TEST|K100|S100|W100";
                        else if (D.GetString(dr["CD_COMPANY"]) == "S100")
                            대상회사 = "TEST|K100|K200|W100";
                        else if (D.GetString(dr["CD_COMPANY"]) == "W100")
                            대상회사 = "TEST|K100|K200|S100";
                        else
                            대상회사 = "K100|K200|S100|W100";

                        this._biz.동기화(D.GetString(dr["CD_PARTNER"]), D.GetString(dr["CD_COMPANY"]), 대상회사, Global.MainFrame.LoginInfo.UserID);
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn동기화.Text);
                    this.OnToolBarSearchButtonClicked(null, null);
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

        private void btn본사주소_Click(object sender, EventArgs e)
        {
            try
            {
                object obj1 = this.LoadHelpWindow("P_MA_POST", new object[] { this.MainFrameInterface, 
                                                                               string.Empty });

                if (((Form)obj1).ShowDialog() == DialogResult.OK && obj1 is IHelpWindow)
                {
                    this.meb본사주소.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[0]) + D.GetString(((IHelpWindow)obj1).ReturnValues[1]);
                    this.txt본사주소.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[2]).Trim();
                    this.txt본사주소상세.Text = D.GetString(((IHelpWindow)obj1).ReturnValues[3]);
                    this.txt본사주소상세.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.계좌정보필수값설정();
                this.거래처분류필수값설정();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo계좌구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ControlEnable();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ControlEnable()
        {
            switch (D.GetString(this.cbo계좌구분.SelectedValue))
            {
                case "001":
                    this.txt은행명.Enabled = false;
                    this.cbo은행소재국.Enabled = false;
                    this.txt은행코드.Enabled = false;
                    this.txtSwift코드.Enabled = false;
                    this.txt예금주주소.Enabled = false;
                    this.txt중계은행.Enabled = false;
                    this.txt예금주전화번호.Enabled = false;
                    this.cbo예금주국적.Enabled = false;
                    this.cbo은행.Enabled = true;

                    this.txt계좌번호.BackColor = Color.FromArgb(255, 237, 242);
                    this.cbo은행.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt예금주.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt은행코드.BackColor = SystemColors.Window;
                    this.cbo은행소재국.BackColor = SystemColors.Window;
                    this.txtSwift코드.BackColor = SystemColors.Window;
                    this.cbo예금주국적.BackColor = SystemColors.Window;
                    this.txt예금주전화번호.BackColor = SystemColors.Window;
                    this.txt은행명.BackColor = SystemColors.Window;
                    this.txt예금주주소.BackColor = SystemColors.Window;
                    this.txt중계은행.BackColor = SystemColors.Window;
                    break;
                case "002":
                    this.txt은행명.Enabled = true;
                    this.cbo은행소재국.Enabled = true;
                    this.txt은행코드.Enabled = true;
                    this.txtSwift코드.Enabled = true;
                    this.txt예금주주소.Enabled = true;
                    this.txt중계은행.Enabled = true;
                    this.txt예금주전화번호.Enabled = true;
                    this.cbo예금주국적.Enabled = true;
                    this.cbo은행.SelectedValue = "300";
                    this.cbo은행.Enabled = false;

                    this.txt계좌번호.BackColor = Color.FromArgb(255, 237, 242);
                    this.cbo은행.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt예금주.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt은행코드.BackColor = Color.FromArgb(255, 237, 242);
                    this.cbo은행소재국.BackColor = Color.FromArgb(255, 237, 242);
                    this.txtSwift코드.BackColor = Color.FromArgb(255, 237, 242);
                    this.cbo예금주국적.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt예금주전화번호.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt은행명.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt예금주주소.BackColor = Color.FromArgb(255, 237, 242);
                    this.txt중계은행.BackColor = Color.FromArgb(255, 237, 242);
                    break;
                case "":
                    this.txt계좌번호.BackColor = SystemColors.Window;
                    this.cbo은행.BackColor = SystemColors.Window;
                    this.txt예금주.BackColor = SystemColors.Window;
                    this.txt은행코드.BackColor = SystemColors.Window;
                    this.cbo은행소재국.BackColor = SystemColors.Window;
                    this.txtSwift코드.BackColor = SystemColors.Window;
                    this.cbo예금주국적.BackColor = SystemColors.Window;
                    this.txt예금주전화번호.BackColor = SystemColors.Window;
                    this.txt은행명.BackColor = SystemColors.Window;
                    this.txt예금주주소.BackColor = SystemColors.Window;
                    this.txt중계은행.BackColor = SystemColors.Window;
                    break;
            }
        }

        private void cbo은행_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this.cbo계좌구분.SelectedValue) == "001")
                {
                    if (D.GetString(this.cbo은행.SelectedValue) == "300")
                    {
                        this.ShowMessage("국내계좌의 경우 해외은행을 선택할 수 없습니다.");
                        this.cbo은행.SelectedValue = string.Empty;
                        this.cbo은행.Focus();
                    }
                    else
                    {
                        this.txt은행명.Text = this.cbo은행.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void txt거래처명_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this._dialog == null || this._dialog.IsDisposed == true)
                {
                    this._dialog = new P_CZ_MA_PARTNER_REQ_CHECK(this.txt거래처명.Text);
                    this._dialog.Show();
                }
                else
                {
                    this._dialog.재조회(this.txt거래처명.Text);
                    this._dialog.Focus();
                }
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

        private void meb주민번호_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.CH)
                    return;

                if (this.meb주민등록번호.Text.Replace("-", "").Replace(" ", "").Replace("_", "") != "" && this.meb주민등록번호.ClipText.Length != 13)
                {
                    this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                    this.meb주민등록번호.Text = string.Empty;
                    this.meb주민등록번호.Focus();
                }
                else if (!new CommonFunction().CheckRegiNo(this.meb주민등록번호.ClipText) && this.meb주민등록번호.Text.Replace("-", "").Replace(" ", "").Replace("_", "") != "" && this.ShowMessage("주민등록번호가 맞지 않습니다. 허용 하시겠습니까?", "QY2") == DialogResult.No)
                {
                    this.meb주민등록번호.Text = string.Empty;
                    this.meb주민등록번호.Focus();
                }
                else
                {
                    string 등록유무 = string.Empty;
                    this._biz.주민번호체크(this.meb주민등록번호.ClipText, ref 등록유무);
                    if (등록유무 == "Y" && this.ShowMessage("이미 등록된 주민번호입니다.\n허용하시겠습니까?", "QY2") == DialogResult.No)
                    {
                        this.meb주민등록번호.Text = string.Empty;
                        this.meb주민등록번호.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Ckeck사업자등록번호(string 거래처코드, string 거래처명, CancelEventArgs e)
        {
            string noCompany중복메시지 = this.GetNoCompany중복메시지(거래처코드, 거래처명);

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

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.ControlName == this.ctx수주형태.Name)
                {
                    e.HelpParam.P61_CODE1 = "C";
                    e.HelpParam.P62_CODE2 = "Y";
                }
                else if (e.ControlName == this.ctx발주형태.Name)
                    e.HelpParam.P61_CODE1 = string.Empty;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타메소드
        private void 계좌정보필수값설정()
        {
            try
            {
                switch (D.GetString(cbo거래처구분.SelectedValue))
                {
                    case "110": // 대리점
                    case "200": // 메이커
                    case "400": // 공급사
                    case "500": // 포워더
                    case "600": // 트레이더
                        this.cbo계좌구분.BackColor = Color.FromArgb(255, 237, 242);
                        this.txt계좌번호.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo은행.BackColor = Color.FromArgb(255, 237, 242);
                        this.txt예금주.BackColor = Color.FromArgb(255, 237, 242);
                        this.txt은행코드.BackColor = SystemColors.Window;
                        this.cbo은행소재국.BackColor = SystemColors.Window;
                        this.txtSwift코드.BackColor = SystemColors.Window;
                        this.cbo예금주국적.BackColor = SystemColors.Window;
                        this.txt예금주전화번호.BackColor = SystemColors.Window;
                        this.txt은행명.BackColor = SystemColors.Window;
                        this.txt예금주주소.BackColor = SystemColors.Window;
                        this.txt중계은행.BackColor = SystemColors.Window;
                        break;
                    default:
                        this.cbo계좌구분.BackColor = SystemColors.Window;
                        this.txt계좌번호.BackColor = SystemColors.Window;
                        this.cbo은행.BackColor = SystemColors.Window;
                        this.txt예금주.BackColor = SystemColors.Window;
                        this.txt은행코드.BackColor = SystemColors.Window;
                        this.cbo은행소재국.BackColor = SystemColors.Window;
                        this.txtSwift코드.BackColor = SystemColors.Window;
                        this.cbo예금주국적.BackColor = SystemColors.Window;
                        this.txt예금주전화번호.BackColor = SystemColors.Window;
                        this.txt은행명.BackColor = SystemColors.Window;
                        this.txt예금주주소.BackColor = SystemColors.Window;
                        this.txt중계은행.BackColor = SystemColors.Window;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 거래처분류필수값설정()
        {
            try
            {
                switch (D.GetString(this.cbo거래처분류.SelectedValue))
                {
                    case "007": // 매입, 매출
                        this.cbo매출통화.BackColor = Color.FromArgb(255, 237, 242);
                        this.ctx수주형태.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매출결재조건.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매출부가세.BackColor = Color.FromArgb(255, 237, 242);

                        this.cbo매입통화.BackColor = Color.FromArgb(255, 237, 242);
                        this.ctx발주형태.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매입결재조건.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매입부가세.BackColor = Color.FromArgb(255, 237, 242);
                        break;
                    case "006": // 매출처
                        this.cbo매출통화.BackColor = Color.FromArgb(255, 237, 242);
                        this.ctx수주형태.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매출결재조건.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매출부가세.BackColor = Color.FromArgb(255, 237, 242);

                        this.cbo매입통화.BackColor = SystemColors.Window;
                        this.ctx발주형태.BackColor = SystemColors.Window;
                        this.cbo매입결재조건.BackColor = SystemColors.Window;
                        this.cbo매입부가세.BackColor = SystemColors.Window;
                        break;
                    case "005": // 매입처
                        this.cbo매출통화.BackColor = SystemColors.Window;
                        this.ctx수주형태.BackColor = SystemColors.Window;
                        this.cbo매출결재조건.BackColor = SystemColors.Window;
                        this.cbo매출부가세.BackColor = SystemColors.Window;

                        this.cbo매입통화.BackColor = Color.FromArgb(255, 237, 242);
                        this.ctx발주형태.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매입결재조건.BackColor = Color.FromArgb(255, 237, 242);
                        this.cbo매입부가세.BackColor = Color.FromArgb(255, 237, 242);
                        break;
                    default:
                        this.cbo매출통화.BackColor = SystemColors.Window;
                        this.ctx수주형태.BackColor = SystemColors.Window;
                        this.cbo매출결재조건.BackColor = SystemColors.Window;
                        this.cbo매출부가세.BackColor = SystemColors.Window;

                        this.cbo매입통화.BackColor = SystemColors.Window;
                        this.ctx발주형태.BackColor = SystemColors.Window;
                        this.cbo매입결재조건.BackColor = SystemColors.Window;
                        this.cbo매입부가세.BackColor = SystemColors.Window;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 필수값확인(DataRow dr)
        {
            try
            {
                #region 필수값 확인
                string 거래처구분, 거래처분류, 거래처그룹, 지역구분, 담당유형, 담당자명, 메일주소;

                거래처구분 = D.GetString(dr["FG_PARTNER"]);
                거래처분류 = D.GetString(dr["CLS_PARTNER"]);
                거래처그룹 = D.GetString(dr["CD_PARTNER_GRP"]);
                지역구분 = D.GetString(dr["CD_AREA"]);
                담당유형 = D.GetString(dr["TP_PTR"]);
                담당자명 = D.GetString(dr["CD_EMP_PARTNER"]);
                메일주소 = D.GetString(dr["E_MAIL"]);

                if (string.IsNullOrEmpty(거래처구분))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처구분.Text);
                    return false;
                }

                if (string.IsNullOrEmpty(거래처분류))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처분류.Text);
                    return false;
                }

                if ((dr["CD_COMPANY"].ToString() == "K100" || dr["CD_COMPANY"].ToString() == "K200") &&
                    (거래처분류 == "006" || 거래처분류 == "007"))
                {
                    if (string.IsNullOrEmpty(거래처그룹))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처그룹.Text);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(지역구분))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl지역구분.Text);
                    return false;
                }

                if (string.IsNullOrEmpty(담당유형))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당유형.Text);
                    return false;
                }

                if (string.IsNullOrEmpty(담당자명))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자명.Text);
                    return false;
                }

                if (!string.IsNullOrEmpty(담당자명) && string.IsNullOrEmpty(메일주소))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl메일주소.Text);
                    return false;
                }

                if (dr["CD_COMPANY"].ToString() == "K100" &&
                    (거래처분류 == "006" || 거래처분류 == "007") &&
                    (거래처그룹 != "53" && 거래처그룹 != "54"))
                {
                    if (string.IsNullOrEmpty(dr["YN_JEONJA"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계산서발행형태.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_INV"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblINV발송방법.Text);
                        return false;
                    }
                }

                if (dr["CD_COMPANY"].ToString() == "K200" &&
                    (거래처분류 == "006" || 거래처분류 == "007") &&
                    거래처그룹 != "82")
                {
                    if (string.IsNullOrEmpty(dr["YN_JEONJA"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계산서발행형태.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_INV"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblINV발송방법.Text);
                        return false;
                    }
                }

                if (거래처분류 == "007") //매입+매출
                {
                    #region 매출처
                    if (string.IsNullOrEmpty(dr["CD_EXCH1"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출통화.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_SO"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수주형태.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_TERMS_PAYMENT"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출결재조건.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_VAT"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출부가세.Text);
                        return false;
                    }
                    #endregion

                    #region 매입처
                    if (string.IsNullOrEmpty(dr["CD_EXCH2"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입통화.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["CD_TPPO"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발주형태.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["FG_PAYMENT"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입결재조건.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["FG_TAX"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입부가세.Text);
                        return false;
                    }
                    #endregion
                }
                else if (거래처분류 == "006")
                {
                    #region 매출처
                    if (string.IsNullOrEmpty(dr["CD_EXCH1"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출통화.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_SO"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수주형태.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_TERMS_PAYMENT"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출결재조건.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["TP_VAT"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출부가세.Text);
                        return false;
                    }
                    #endregion
                }
                else if (거래처분류 == "005")
                {
                    #region 매입처
                    if (string.IsNullOrEmpty(dr["CD_EXCH2"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입통화.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["CD_TPPO"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발주형태.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["FG_PAYMENT"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입결재조건.Text);
                        return false;
                    }

                    if (string.IsNullOrEmpty(dr["FG_TAX"].ToString()))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입부가세.Text);
                        return false;
                    }
                    #endregion
                }

                if (!string.IsNullOrEmpty(dr["NM_ORIGIN"].ToString()) && 
                    string.IsNullOrEmpty(dr["NM_ORIGIN_RPT"].ToString()))
				{
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl원산지_출력물.Text);
                    return false;
                }

                switch (거래처구분)
                {
                    case "110": // 대리점
                    case "200": // 메이커
                    case "500": // 포워더
                        if (!this.계좌정보필수값확인(dr))
                            return false;
                        break;
                    case "400": // 공급사
                    case "600": // 트레이더
                        if (거래처분류 != "006") // 매출처
                        {
                            if (!this.계좌정보필수값확인(dr))
                                return false;
                        }
                        break;

                }
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                return false;
            }

            return true;
        }
        #endregion
    }
}