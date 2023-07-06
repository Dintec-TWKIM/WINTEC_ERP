using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Dintec;
using DX;


namespace cz
{
	public partial class P_CZ_SA_QTN_REG : PageBase
	{
		string 링크파일번호 = "";
		bool 관리자조회 = false;
		헤더 헤더 = new 헤더();
		
		P_CZ_SA_INQ 영업문의 = new P_CZ_SA_INQ();
		P_CZ_PU_INQ 구매문의 = new P_CZ_PU_INQ();
		P_CZ_PU_QTN 구매견적 = new P_CZ_PU_QTN();
		P_CZ_SA_QTN 영업견적 = new P_CZ_SA_QTN();
		P_CZ_SA_STOCK 재고시뮬 = new P_CZ_SA_STOCK();
		P_CZ_SA_INQ_EXT 실적선용 = new P_CZ_SA_INQ_EXT();
		P_CZ_SA_QTN_EXT 실적부품 = new P_CZ_SA_QTN_EXT();
		P_CZ_SA_INQ_2 선용문의 = new P_CZ_SA_INQ_2();

		#region ==================================================================================================== Property

		public string 회사코드 => 상수.회사코드;
		
		public string 파일번호
		{
			get => tbx파일번호.Text.트림().대문자();
			set => tbx파일번호.Text = value;
		}

		public int 차수 => 1;

		public string 담당자 => ctx담당자.CodeValue;

		public string 매출처코드
		{
			get => ctx매출처.CodeValue;
			set
			{
				string 이전값 = ctx매출처.CodeValue;
				string 신규값 = value;

				if (신규값 == "") ctx매출처.글("");
				if (신규값 != 이전값)
				{
					ctx매출처.CodeValue = 신규값;
					Ctx매출처_CodeChanged(null, null);
				}
			}
		}

		public string 매출처이름 => ctx매출처.CodeName;

		public string Imo번호
		{
			get => ctx호선.CodeValue;
			set
			{
				string 이전값 = ctx호선.CodeValue;
				string 신규값 = value;

				if (신규값 == "") ctx호선.글("");
				if (신규값 != 이전값)
				{
					ctx호선.CodeValue = value;
					Ctx호선_CodeChanged(null, null);
				}
			}
		}

		public string 선명 => ctx호선.CodeName;

		public string 문의번호
		{
			get => tbx문의번호.Text;
			set => tbx문의번호.Text = value;
		}

		public string 숨김여부 { get; set; }

		public bool 메시지여부 { get; set; } = true;

		public bool 부품영업 => PageID == "P_CZ_SA_QTN_REG";

		public bool 선용영업 => !부품영업;

		#endregion

		#region ==================================================================================================== 생성자 == CONS == 채ㅜㄴ

		public P_CZ_SA_QTN_REG()
		{
			InitializeComponent();

			StartUp.Certify(this);
			숨김여부 = GetDb.HiddenYn();
		}

		public P_CZ_SA_QTN_REG(string 파일번호)
		{
			InitializeComponent();

			StartUp.Certify(this);
			숨김여부 = GetDb.HiddenYn();
			링크파일번호 = 파일번호;
		}

		#endregion

		#region ==================================================================================================== 초기화 == INIT == ㅑㅜㅑㅅ

		protected override void InitLoad()
		{
			

			this.페이지초기화();

			tbx파일번호.엔터검색();			
			ctx매출처.검색어필수();
			ctx호선.검색어필수();
			cbo매출처그룹.바인딩(코드.코드관리("MA_B000065"), true);

			// 헤더
			헤더.컨테이너(lay헤드);
			헤더.기본키(tbx파일번호);
			헤더.필수값(tbx파일번호, dtp작성일자, ctx담당자, ctx매출처, cbo매출처그룹, ctx영업그룹, ctx호선, tbx문의번호);

			// 자식폼
			영업문의.Parent = tab영업문의;
			선용문의.Parent = tab선용문의;
			구매문의.Parent = tab구매문의;
			구매견적.Parent = tab구매견적;
			영업견적.Parent = tab영업견적;
			재고시뮬.Parent = tab재고시뮬;
			실적부품.Parent = tab실적부품;
			실적선용.Parent = tab실적선용;
			
			foreach (TabPage tab in tab메인.TabPages)			
				tab.Controls[0].Dock = DockStyle.Fill;

			// 그리드
			MainGrids = new[]
			{
				  영업문의.그리드라인
				, 구매문의.그리드헤드, 구매문의.그리드라인
				, 구매견적.그리드헤드, 구매견적.그리드라인
				, 영업견적.그리드라인, 영업견적.그리드가로, 영업견적.그리드세로
				, 재고시뮬.그리드헤드
				, 선용문의.라인
			};

			이벤트();
			매출처정보();
			엔진정보();
			중복정보();

			// 모드에 따라 탭 재배치
			if (부품영업)
			{
				tab메인.TabPages.Remove(tab선용문의);

				if (회사코드 == "K100")
					tab메인.TabPages.Remove(tab실적선용);

				//tab메인.TabPages.Remove(tab실적부품);
				//tab메인.TabPages.Add(tab실적부품);
			}
			else
			{
				
			}

			// 마무리
			클리어();
		}

		protected override void InitPaint()
		{		
			// 헤더 레이아웃
			lay헤드컨테이너.ColumnStyles[1].Width = 300;
			lay헤드컨테이너.ColumnStyles[2].Width = 270;
			lay헤드컨테이너.ColumnStyles[3].Width = 290;

			// 관리자검색버튼
			if (!담당자.In("S-343", "S-391", "D-004A", "S-458"))
				btn관리자조회.Visible = false;

			// 링크모드
			if (링크파일번호 != "")
			{
				파일번호 = 링크파일번호;
				OnToolBarSearchButtonClicked(null, null);
			}

			if (상수.사원번호.포함("S-343"))
			{
				//파일번호 = "TY22000005";
				//파일번호 = "ZZ23000713";
				//파일번호 = "TY23000004";	// 동화뉴텍 자동견적
			}
			else
			{
				btn테스트.Visible = false;
			}

			tbx파일번호.Focus();
		}


		protected override bool IsChanged() => base.IsChanged() || ToolBarSaveButtonEnabled;

		private void 클리어()
		{
			관리자조회 = false;
			pnl파일번호.사용(true);
			사용(true);

			// ********** 상단 알림
			lbl견적마감.ForeColor = Color.LightGray;
			lblBOM구성.ForeColor = Color.LightGray;
			lbl매입부대비용.ForeColor = Color.LightGray;
			lbl매출부대비용.ForeColor = Color.LightGray;

			// ********** 헤더
			헤더.초기화();
			헤더.변경이벤트중지();

			// 기본값
			dtp작성일자.기본값();
			ctx담당자.기본값();
			ctx영업그룹.기본값();

			헤더.변경이벤트시작();

			// ********** 자식폼
			영업문의.Clear();
			선용문의.클리어();
			구매문의.Clear();
			구매견적.Clear();
			영업견적.Clear();
			재고시뮬.Clear();
			실적부품.Clear();
			실적선용.Clear();

			// 탭 이미지 없애기
			foreach (TabPage tab in tab메인.TabPages)
				tab.ImageIndex = -1;

			// ********** 기타
			// 별도 관리 버튼
			btn수주등록.Enabled = true;
			btn발주등록.Enabled = true;

			매출처정보();
			엔진정보();
			중복정보();

			tbx파일번호.Focus();
		}

		public void 사용(bool 사용)
		{
			// 버튼
			btn견적현황표.Enabled = 사용;
			btn파일복사.Enabled = 사용;
			btn견적마감.Enabled = 사용;

			// 헤더
			헤더.사용(사용);

			// 자식폼
			영업문의.사용(사용);
			선용문의.사용(사용);
			구매문의.사용(사용);
			구매견적.사용(사용);
			영업견적.사용(사용);
			재고시뮬.사용(사용);			
			실적부품.사용(사용);
			실적선용.사용(사용);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVEN == ㄷㅍ두

		private void 이벤트()
		{
			btn테스트.Click += Btn테스트_Click;

			btn견적현황표.Click += Btn견적현황표_Click;
			btn파일복사.Click += Btn파일복사_Click;
			btn수주등록.Click += Btn수주등록_Click;
			btn발주등록.Click += Btn발주등록_Click;
			btn견적마감.Click += Btn견적마감_Click;
			btn관리자조회.Click += Btn관리자조회_Click;

			ctx담당자.CodeChanged += Ctx담당자_CodeChanged;
			ctx매출처.CodeChanged += Ctx매출처_CodeChanged;
			ctx호선.CodeChanged += Ctx호선_CodeChanged;			
			tbx문의번호.Validated += Tbx문의번호_Validated;

			lay헤드컨테이너.Resize += Lay헤드컨테이너_Resize;

			tab메인.Deselecting += Tab메인_Deselecting;
			tab메인.SelectedIndexChanged += Tab메인_SelectedIndexChanged;			
		}

		
		private void Btn테스트_Click(object sender, EventArgs e)
        {
	
		}

        private void Btn견적현황표_Click(object sender, EventArgs e)
		{
			H_CZ_STATUS_TABLE f = new H_CZ_STATUS_TABLE(파일번호);
			
			if (f.ShowDialog() != DialogResult.OK)
				return;
		}

		private void Btn파일복사_Click(object sender, EventArgs e)
		{
			// 복사 팝업
			H_CZ_COPY_FILE f = new H_CZ_COPY_FILE();
			f.부모 = this;
			f.NO_FILE_T = 파일번호;
			f.NO_DSP_MAX = 영업문의.그리드라인.GetMaxValue("NO_DSP");
			if (f.ShowDialog() != DialogResult.OK) return;

			// 조회
			파일번호 = f.NO_FILE_T;
			OnToolBarSearchButtonClicked(null, null);
		}

		private void Btn수주등록_Click(object sender, EventArgs e)
		{
			if (ToolBarSaveButtonEnabled)
			{
				ShowMessage("현재 상태가 저장되지 않았습니다.");
				return;
			}

			// 필수값 및 선사 발주서 체크
			string query = @"
SELECT
	  TP_SO
	, COND_PAY
	, TP_VAT
FROM CZ_SA_QTNH WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_FILE = @NO_FILE

SELECT
	1
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_KEY = @NO_FILE
	AND TP_STEP = '08'
	AND ISNULL(NM_FILE, '') != ''

SELECT
	CD_SYSDEF
FROM MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND CD_FIELD = 'CZ_SA00023'
	AND CD_FLAG2 = 'CLAIM'";

			DBMgr dbm = new DBMgr();
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", 회사코드);
			dbm.AddParameter("@NO_FILE", 파일번호);
			DataSet ds = dbm.GetDataSet();

			DataTable dtValue = ds.Tables[0];
			DataTable dtAttach = ds.Tables[1];
			DataTable dtClaim = ds.Tables[2];

			// 주요 필수값 체크
			if (dtValue.Rows[0]["TP_SO"].ToString() == "") { ShowMessage("CZ_@ 은(는) 필수입력 항목입니다.", "수주형태"); return; }
			if (dtValue.Rows[0]["COND_PAY"].ToString() == "") { ShowMessage("CZ_@ 은(는) 필수입력 항목입니다.", "지불조건"); return; }
			if (dtValue.Rows[0]["TP_VAT"].ToString() == "") { ShowMessage("CZ_@ 은(는) 필수입력 항목입니다.", "과세구분"); return; }

			// 선사 발주서 체크
			if (파일번호.Substring(0, 2) != dtClaim.Rows[0][0].ToString() && dtAttach.Rows.Count == 0 && 담당자 != "S-343" && LoginInfo.UserID != "S-343")
			{
				ShowMessage("발주서 첨부파일이 누락되었습니다.");
				return;
			}
			
			if (IsExistPage("P_CZ_SA_SO_REG", false)) UnLoadPage("P_CZ_SA_SO_REG", false);

			this.Grant.CanSave = true;
			this.Grant.CanSearch = true;
			this.Grant.CanPrint = true;
			this.Grant.CanDelete = true;

			LoadPageFrom("P_CZ_SA_SO_REG", DD("수주등록"), this.Grant, new object[] { 파일번호, true });
			//CallOtherPageMethod("P_CZ_SA_SO_REG", DD("수주등록"), this.Grant, new object[] { NO_FILE, true });
		}

		private void Btn발주등록_Click(object sender, EventArgs e)
		{
			if (ToolBarSaveButtonEnabled) { ShowMessage("현재 상태가 저장되지 않았습니다."); return; }

			if (IsExistPage("P_CZ_PU_PO_REG", false)) UnLoadPage("P_CZ_PU_PO_REG", false);

			this.Grant.CanSave = true;
			this.Grant.CanSearch = true;
			this.Grant.CanPrint = true;
			this.Grant.CanDelete = true;

			LoadPageFrom("P_CZ_PU_PO_REG", DD("발주등록"), this.Grant, new object[] { 파일번호, true });
		}

		private void Btn견적마감_Click(object sender, EventArgs e)
		{
			H_CZ_QTN_CLOSE form = new H_CZ_QTN_CLOSE(파일번호);
			if (form.ShowDialog() != DialogResult.OK) return;

			OnToolBarSearchButtonClicked(null, null);
		}

		private void Btn관리자조회_Click(object sender, EventArgs e)
		{
			tab영업문의.ImageIndex = -1;
			tab구매문의.ImageIndex = -1;
			tab구매견적.ImageIndex = -1;
			tab영업견적.ImageIndex = -1;
			tab실적선용.ImageIndex = -1;
			tab실적부품.ImageIndex = -1;
			tab재고시뮬.ImageIndex = -1;

			//AdminSearch = true;
			//OnToolBarSearchButtonClicked(null, null);
		}
		
		private void Ctx담당자_CodeChanged(object sender, EventArgs e)
		{
			DataTable dt = 코드.영업구매그룹(ctx담당자.값());
			ctx영업그룹.값(dt.존재() ? dt.첫행("CD_SALEGRP") : "");
			ctx영업그룹.글(dt.존재() ? dt.첫행("NM_SALEGRP") : "");
			
			tbx문의번호.Focus();
		}

		private void Ctx매출처_CodeChanged(object sender, EventArgs e)
		{
			cbo매출처그룹.값("");

			if (ctx매출처.값() != "")
			{
				DataTable dt = 코드.거래처(ctx매출처.값());

				ctx매출처.글(dt.첫행("LN_PARTNER"));
				cbo매출처그룹.값(dt.첫행("CD_PARTNER_GRP"));
			}

			매출처정보();
			영업문의.SetPartnerPic(ctx매출처.CodeValue, "");
			tbx문의번호.Focus();
		}

		private void Ctx호선_CodeChanged(object sender, EventArgs e)
		{
			매출처코드 = "";
			//매출처이름 = "";
			tbx호선유형.Text = "";
			tbx선박납기.Text = "";

			if (ctx호선.값() != "")
			{
				DataTable dt = 코드.호선(ctx호선.값());
				
				ctx호선.글(dt.첫행("NM_VESSEL") + " (" + dt.첫행("NO_HULL") + ")");
				tbx호선유형.Text = 회사코드 == "K100" ? dt.첫행("NM_TYPE2").문자() : dt.첫행("NM_TYPE").문자();
				tbx선박납기.Text = dt.첫행("DT_SHIP_DLV").문자();

				if (dt.첫행("YN_PARTNER").문자() == "Y")
				{
					매출처코드 = dt.첫행("CD_PARTNER").문자();
					//매출처이름 = dt.첫행("LN_PARTNER").문자();
				}
			}			

			엔진정보();
			영업문의.SetEngineType(Imo번호);    // 엔진유형 바인딩
			tbx문의번호.Focus();
		}

		private void Tbx문의번호_Validated(object sender, EventArgs e)
		{
			if (tbx문의번호.Text != "")
				중복정보();
		}

		private void Lay헤드컨테이너_Resize(object sender, EventArgs e)
		{
			//tbx비고.Text += "a";
			// 화면이 좁아지면 헤더가 짤리므로 우측 web을 없애줌
			if (lay헤드컨테이너.GetColumnWidths()[0] < 900)
				lay헤드컨테이너.ColumnStyles[3].Width = 0;
			else
				lay헤드컨테이너.ColumnStyles[3].Width = 290;
		}

		#endregion

		#region ==================================================================================================== 조회 == SEAR == ㄴㄷㅁㄱ초

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				파일번호 = 파일번호.한글을영어();

				// DY파일인 경우 최근 파일을 불러옴
				if (파일번호.포함("DY", "SH", "TY"))
				{
					string query = "SELECT TOP 1 NO_FILE FROM CZ_SA_QTNH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE LIKE '" + 파일번호 + @"[0-9]%' ORDER BY NO_FILE DESC";
					파일번호 = 디비.결과(query).첫행(0).문자();
				}

				// ********** 조회
				DataTable dtHead = 디비.결과("PS_CZ_SA_INQ_H_4", 회사코드, 파일번호);

				if (!dtHead.존재() && 링크파일번호 == "")
				{
					클리어();
					메시지.경고발생(메시지코드.선택된자료가없습니다);
				}

				// ********** 바인딩
				if (!dtHead.존재())
				{					
					// ********** 워크플로우에서 첫 작성
					// 워크플로우 영업담당 및 비고 세팅
					string query = "SELECT ID_SALES, DC_RMK FROM CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_KEY = '" + 파일번호 + "' AND TP_STEP = '01'";
					DataTable dtWork = 디비.결과(query);

					ctx담당자.값(dtWork.첫행("ID_SALES"));
					ctx담당자.글(코드.사원(ctx담당자.값()).첫행("NM_EMP"));
					Ctx담당자_CodeChanged(null, null);
					tbx비고.Text = dtWork.첫행("DC_RMK").문자();

					if (tbx비고.Text != "")
						영업문의.SetSupplier(tbx비고.Text);
				}
				else
				{
					// ********** 헤더 바인딩
					헤더.바인딩(dtHead);
					pnl파일번호.사용(false);
					ToolBarDeleteButtonEnabled = true;

					// 폼 상태 변경 (수,발주 버튼은 별도 관리)
					사용(관리자조회 || dtHead.첫행("NO_SO") + "" + dtHead.첫행("NO_PO") == "");
					btn수주등록.사용(dtHead.첫행("NO_SO").문자() == "");
					btn발주등록.사용(dtHead.첫행("NO_SO").문자() != "" && dtHead.첫행("NO_PO").문자() == "");

					// ********** 상단 NOTICE
					string query = @"
SELECT TOP 1 1 FROM CZ_SA_QTNH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + @"' AND YN_CLOSE = 'Y'
SELECT TOP 1 1 FROM CZ_SA_QTNL WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + @"' AND TP_BOM = 'C'
SELECT TOP 1 1 FROM CZ_PU_QTNL WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + @"' AND NO_LINE > 90000
SELECT TOP 1 1 FROM CZ_SA_QTNL WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + @"' AND NO_LINE > 90000";

					DataSet ds = 디비.결과s(query);
					lbl견적마감.ForeColor		= ds.Tables[0].존재() ? Color.Red : Color.LightGray;
					lblBOM구성.ForeColor		= ds.Tables[0].존재() ? Color.Red : Color.LightGray;
					lbl매입부대비용.ForeColor	= ds.Tables[0].존재() ? Color.Red : Color.LightGray;
					lbl매출부대비용.ForeColor	= ds.Tables[0].존재() ? Color.Red : Color.LightGray;

					// ********** 자식 탭 바인딩
					Tab메인_SelectedIndexChanged(null, null);
					if (tab메인.SelectedTab == tab영업문의) 영업문의.Search();
					if (tab메인.SelectedTab == tab선용문의) 선용문의.조회();
					if (tab메인.SelectedTab == tab구매문의) 구매문의.Search();
					if (tab메인.SelectedTab == tab구매견적) 구매견적.Search();
					if (tab메인.SelectedTab == tab영업견적) 영업견적.Search();
					if (tab메인.SelectedTab == tab재고시뮬) 재고시뮬.Search();
					if (tab메인.SelectedTab == tab실적부품) 실적부품.Search();
					if (tab메인.SelectedTab == tab실적선용) 실적선용.Search();

					// 삭제 버튼 강제 활성화 (그리드에 row가 없는 데이터테이블이 바인딩 되면 삭제버튼 비활성화 되버림)
					ToolBarDeleteButtonEnabled = true;

					// ********** 기타
					매출처정보();
					엔진정보();
					중복정보();					
				}
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		private void Tab메인_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			// 이미 조회가(바인딩) 된 경우에만 동작함
			if (!lbl파일번호.Enabled)
			{
				if (디비.결과("PS_CZ_SA_INQ_H_4", 회사코드, 파일번호).존재())
				{
					if (tab메인.SelectedTab == tab영업문의) 영업문의.Search();
					if (tab메인.SelectedTab == tab구매문의) 구매문의.Search();
					if (tab메인.SelectedTab == tab구매견적) 구매견적.Search();
					if (tab메인.SelectedTab == tab영업견적) 영업견적.Search();
					if (tab메인.SelectedTab == tab재고시뮬) 재고시뮬.Search();
					if (tab메인.SelectedTab == tab실적부품) 실적부품.SetVendor();

					// 테스트
					if (tab메인.SelectedTab == tab선용문의) 선용문의.조회();

					// 삭제 버튼 강제 활성화 (그리드에 row가 없는 데이터테이블이 바인딩 되면 삭제버튼 비활성화 되버림)
					ToolBarDeleteButtonEnabled = true;
				}
				else
				{
					메시지.경고알람(메시지코드.선택된자료가없습니다);
					클리어();
				}
			}
		}

		#endregion

		#region ==================================================================================================== 추가 == ADD == ㅁㅇㅇ

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (MsgAndSave(PageActionMode.Search))				
				클리어();
		}

		#endregion

		#region ==================================================================================================== 삭제 == DELE == ㅇ딛

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (!메시지.일반선택(공통메세지.자료를삭제하시겠습니까)) return;
			if (!Util.CheckPW()) return;

			try
			{
				// 삭제 XMl 만들어서 삭제
				DataTable dt = 헤더.데이터테이블();
				dt.컬럼추가("IP_ADDRESS", 상수.IP);

				string headXml = GetTo.Xml(dt, RowState.Deleted);
				DBMgr.ExecuteNonQuery("SP_CZ_SA_INQ_REG_XML", headXml, DBNull.Value);

				클리어();
				메시지.일반알람(공통메세지.자료가정상적으로삭제되었습니다);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE == ㄴㅁㅍㄷ

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);

			if (저장())
			{
				// 저장 이후 액션
				if (tab메인.SelectedTab == tab영업문의)
					영업문의.Search();	// SINQ일 경우는 NO_LINE을 위해 조회한번 해줌

				ShowMessage(PageResultMode.SaveGood);
			}			
		}
		
		public bool 저장()
		{
			try
			{
				파일번호 = 파일번호.한글을영어();

				#region ********** 유효성 검사

				헤더.유효성검사();

				// 제제 선박 체크
				string query = "SELECT 1 FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND CD_FIELD = 'CZ_SA00062' AND CD_SYSDEF = '" + Imo번호 + "'";
				if (디비.결과(query).존재())
					메시지.경고발생("이란 제제 선박입니다.");

				// 신조선 체크
				if (회사코드 == "K100")
				{
					query = "SELECT NEOE.NEW_SHIP('" + Imo번호 + "')";
					DataTable dtNew = 디비.결과(query);

					if (dtNew.첫행(0).문자() == "Y" && 파일번호.왼쪽(2).포함("FB", "SB", "DY"))
						메시지.경고발생("신조선입니다");
				}

				// 조기경보시스템 → 미수금 체크
				WarningLevel 경고레벨 = WarningLevel.정상;
				string 경고메세지 = "";
				string 제외여부 = "";
				string 지불조건제외여부 = "";

				EalryWarningSystem ews = new EalryWarningSystem();
				ews.미수금확인(매출처코드, ref 경고레벨, ref 경고메세지, ref 제외여부, ref 지불조건제외여부);

				if (경고메세지 != "")
					메시지.경고알람(경고메세지);	// 경고발생으로 하면 안됨, 경고 발생하고 아래코드로 진행해야함

				// 경고레벨 사용불가라도 저장할수 있도록 함
				if (회사코드 == "K200" && 경고레벨 == WarningLevel.사용불가 && 제외여부 != "Y")
					return false;

				#endregion

				// ********** 저장
				// 파일번호가 2자리 일때 자동채번
				if (파일번호.Length == 2)
				{
					string year = DateTime.Now.Year.문자().오른쪽(2);
					query = "SELECT TOP 1 NO_FILE FROM CZ_SA_QTNH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND LEFT(NO_FILE, 4) = '" + 파일번호 + year + "' ORDER BY NO_FILE DESC";

					DataTable dtMax = 디비.결과(query);
					int seq = dtMax.존재() ? dtMax.첫행(0).문자().Substring(4).정수() : 0;
					파일번호 = 파일번호 + year + string.Format("{0:000000}", seq + 1);
				}

				// 헤더 저장
				DataTable dtHead = 헤더.데이터테이블_수정();
				디비.실행("SP_CZ_SA_INQ_REG_XML", GetTo.Xml(dtHead), DBNull.Value, 관리자조회 ? "Y" : "N");	// 언젠가 XML에서 JSON으로 바꾸자

				// 자식탭 저장
				if (tab메인.SelectedTab == tab영업문의) tab영업문의.ImageIndex = 영업문의.저장() ? -1 : 0;
				if (tab메인.SelectedTab == tab선용문의) tab선용문의.ImageIndex = 선용문의.저장() ? -1 : 0;
				if (tab메인.SelectedTab == tab구매문의) tab구매문의.ImageIndex = 구매문의.저장() ? -1 : 0;
				if (tab메인.SelectedTab == tab구매견적) tab구매견적.ImageIndex = 구매견적.저장() ? -1 : 0;
				if (tab메인.SelectedTab == tab영업견적) tab영업견적.ImageIndex = 영업견적.저장() ? -1 : 0;
				if (tab메인.SelectedTab == tab재고시뮬) tab재고시뮬.ImageIndex = 재고시뮬.저장() ? -1 : 0;
				if (tab메인.SelectedTab.ImageIndex == 0) return false;

				// 마무리
				헤더.커밋();
				tbx포커스.Focus();
				pnl파일번호.사용(false);
				
				return true;
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
				return false;
			}
		}

		private void Tab메인_Deselecting(object sender, TabControlCancelEventArgs e)
		{
			if (ToolBarSaveButtonEnabled)
			{
				메시지여부 = false;
				저장();
				메시지여부 = true;
			}
		}

		#endregion
		
		#region ==================================================================================================== 인쇄 == PRIN == ㅔ갸ㅜ

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab영업견적)
			{
				H_CZ_PRT_OPTION f = new H_CZ_PRT_OPTION("SQTN");

				if (Control.ModifierKeys == Keys.Control)
				{
					if (f.ShowDialog() != DialogResult.OK)
						return;
				}

				영업견적.Print(f);
			}
			else if (tab메인.SelectedTab == tab구매문의)
			{
				H_CZ_PRT_OPTION f = new H_CZ_PRT_OPTION("PINQ");

				if (Control.ModifierKeys == Keys.Control)
				{
					if (f.ShowDialog() != DialogResult.OK)
						return;
				}

				구매문의.Print(f);
			}
		}

		#endregion

		#region ==================================================================================================== 닫기

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			영업문의.Exit();
			구매견적.Exit();
			재고시뮬.Exit();
			선용문의.Exit();

			MsgControl.CloseMsg();

			return base.OnToolBarExitButtonClicked(sender, e);
		}

		#endregion

		#region ==================================================================================================== 기타

		private void 매출처정보()
		{
			// 쿼리
			string query = @"
SELECT
	NM_GEN_TYPE		= NEOE.CODE_NAME(CD_COMPANY, 'MA_B000002', TP_PARTNER)
,	NM_PRICE_SENS	= NEOE.CODE_NAME(CD_COMPANY, 'CZ_MA00028', TP_PRICE_SENS)
,	RT_PROFIT_S
,	RT_DC_S
,	RT_COMMISSION
,	DC_COMMISSION
,	NM_EMP_QTN		= NEOE.EMP((SELECT NO_EMP_QTN FROM CZ_SA_QTNH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + @"'))
FROM V_CZ_MA_PARTNER WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 회사코드 + @"'
	AND CD_PARTNER = '" + 매출처코드 + "'";

			DataTable dt = SQL.GetDataTable(query);

			string 선호		= dt.Rows.Count == 0 ? "" : dt.Rows[0]["NM_GEN_TYPE"].문자();
			string 견적담당자	= dt.Rows.Count == 0 ? "" : dt.Rows[0]["NM_EMP_QTN"].문자();
			string 마진율	= dt.Rows.Count == 0 ? "" : dt.Rows[0]["RT_PROFIT_S"].문자("#,###.##");
			string DC율		= dt.Rows.Count == 0 ? "" : dt.Rows[0]["RT_DC_S"].문자("#,###.##");
			string 커미션율	= dt.Rows.Count == 0 ? "" : dt.Rows[0]["RT_COMMISSION"].문자("#,###.##");
			string 가격민감도	= dt.Rows.Count == 0 ? "" : dt.Rows[0]["NM_PRICE_SENS"].문자();
			string 커미션비고	= dt.Rows.Count == 0 ? "" : dt.Rows[0]["DC_COMMISSION"].문자();

			if (마진율 != "")	마진율 += " %";
			if (DC율 != "")		DC율 += " %";
			if (커미션율 != "")	커미션율 += " %";

			// 스타일
			string head = @"
.dx-viewbox th		   { height:28px; }
.dx-viewbox .last-row  { height:50px; }
.dx-viewbox .last-cell { white-space:normal; }";

			// 바디
			string body = @"
<div>
	<table class='dx-viewbox'>
		<colgroup>
			<col style='width:24%;' />
			<col style='width:28%;' />
			<col style='width:24%;' />
			<col style='width:24%;' />
		</colgroup>
		<tr>
			<th>선호</th>
			<td>" + 선호 + @"</td>
			<th>견적담당</th>
			<td>" + 견적담당자 + @"</td>
		</tr>			
		<tr>
			<th>마진</th>
			<td>" + 마진율 + @"</td>
			<th>D/C</th>
			<td>" + DC율 + @"</td>
		</tr>
		<tr>
			<th>커미션</th>
			<td>" + 커미션율 + @"</td>
			<th>민감도</th>
			<td>" + 가격민감도 + @"</td>
		</tr>
		<tr>
			<th>비고</th>
			<td colspan='3'>" + 커미션비고 + @"</td>
		</tr>
	</table>
</div>";
			web매출처.바인딩(head, body, false);
		}

		private void 엔진정보()
		{			
			int 엔진설정라인 = 3;
			int 입항설정라인 = 3;

			// ********** 엔진정보
			string query = @"
SELECT TOP " + (엔진설정라인 - 1) + @"
	ENGINE_TYPE  = LEFT(NEOE.CODE_NAME('K100', 'CZ_MA00009', CD_ENGINE), 2)
,	ENGINE_MAKER = NEOE.CODE_NAME('K100', 'MA_B000031', CLS_M)
,	ENGINE_MODEL = NM_MODEL
FROM
(
	  SELECT TOP 1 * FROM CZ_MA_HULL_ENGINE WHERE NO_IMO = '" + Imo번호 + @"' AND CD_ENGINE = 'ME'
UNION SELECT TOP 1 * FROM CZ_MA_HULL_ENGINE WHERE NO_IMO = '" + Imo번호 + @"' AND CD_ENGINE = 'GE'
) AS A";

			DataTable dtEngine = SQL.GetDataTable(query);

			// ********** 입출항 스케줄
			query = @"
SELECT TOP " + (입항설정라인 - 1) + @"
    PORT            = B.NM_PRTAG
,	DT_ARRIVAL      = ISNULL(CONVERT(NVARCHAR(10), CONVERT(DATETIME, LEFT(B.DTS_ETRYPT, 8)), 111), '')
,	DT_DEPARTURE	= ISNULL(CONVERT(NVARCHAR(10), CONVERT(DATETIME, LEFT(B.DTS_TKOFF, 8)), 111), '')
FROM CZ_MA_HULL			AS A
JOIN CZ_SA_VSSL_ETRYNDH	AS B ON A.CALL_SIGN = B.CALL_SIGN
WHERE 1 = 1
	AND A.NO_IMO = '" + Imo번호 + @"'
	AND (LEFT(B.DTS_ETRYPT, 8) >= NEOE.TODAY(0) OR LEFT(B.DTS_TKOFF, 8) >= NEOE.TODAY(0))
ORDER BY B.DTS_ETRYPT";

			DataTable dtPort = SQL.GetDataTable(query);

			// ********** Html 만들기
			// 스타일
			string style = @"
html, body	{ height:100%; }
.dx-viewbox { height:100%; }
.dx-viewbox th { text-align:center; }";

			// 바디
			string body = @"
<div>
	<table class='dx-viewbox'>
		<colgroup>
			<col style='width:28%; text-align:center;' />
			<col style='width:36%; text-align:center;' />
			<col style='width:36%; text-align:center;' />
		</colgroup>";

			// ********** 엔진정보
			body += @"
		<tr>
			<th>엔진</th>
			<th>제조사</th>
			<th>모델</th>
		</tr>";

			for (int i = 0; i < 엔진설정라인 - 1; i++)
			{
				string 엔진타입	= dtEngine.Rows.Count > i ? dtEngine.Rows[i]["ENGINE_TYPE"].문자()	: "";
				string 엔진메이커	= dtEngine.Rows.Count > i ? dtEngine.Rows[i]["ENGINE_MAKER"].문자()	: "";
				string 엔진모델	= dtEngine.Rows.Count > i ? dtEngine.Rows[i]["ENGINE_MODEL"].문자()	: "";

				body += @"
		<tr>
			<td>" + 엔진타입.웹() + @"</td>
			<td>" + 엔진메이커.웹() + @"</td>
			<td>" + 엔진모델.웹() + @"</td>
		</tr>";
			}

			// ********** 입출항 정보
			body += @"
		<tr>
			<th>도착항</th>
			<th>입항일</th>
			<th>출항일</th>
		</tr>";

			for (int i = 0; i < 입항설정라인 - 1; i++)
			{
				string 도착항 = dtPort.Rows.Count > i ? dtPort.Rows[i]["PORT"].문자() : "";
				string 입항일 = dtPort.Rows.Count > i ? dtPort.Rows[i]["DT_ARRIVAL"].문자() : "";
				string 출항일 = dtPort.Rows.Count > i ? dtPort.Rows[i]["DT_DEPARTURE"].문자() : "";

				body += @"
		<tr>
			<td>" + 도착항.웹() + @"</td>
			<td>" + 입항일.웹() + @"</td>
			<td>" + 출항일.웹() + @"</td>
		</tr>";
			}

			// 마무리
		
			body += @"
	</table>
</div>";
			web엔진.바인딩(style, body, false);
		}

		private void 중복정보()
		{			
			int 설정라인 = 5;

			// 쿼리
			string query = @"
SELECT TOP " + (설정라인 - 1) + @"
	*
FROM
(
	SELECT
		NO_FILE
	,	NO_REF
	FROM CZ_SA_QTNH WITH(NOLOCK)
	WHERE 1 = 1
		AND NO_FILE != @NO_FILE
		AND NO_REF != ''
		AND NO_REF = @NO_REF";

			if (문의번호.Length >= 5)
			{
				query += @"

	UNION

	SELECT
		NO_FILE
	,	NO_REF
	FROM CZ_SA_QTNH WITH(NOLOCK)
	WHERE 1 = 1
		AND NO_FILE != @NO_FILE
		AND DT_INQ BETWEEN @DT_INQ_F AND @DT_INQ_T
		AND (CD_PARTNER = @CD_PARTNER OR NO_IMO = @NO_IMO)
		AND NO_REF != ''
		AND NO_REF LIKE '%' + @NO_REF + '%'";
			}

			query += @"
) AS A";

			SQL sql = new SQL(query, SQLType.Text);						
			sql.Parameter.추가("@NO_FILE"	, 파일번호	, true);
			sql.Parameter.추가("@CD_PARTNER"	, 매출처코드	, true);
			sql.Parameter.추가("@NO_IMO"		, Imo번호	, true);
			sql.Parameter.추가("@NO_REF"		, 문의번호	, true);
			sql.Parameter.추가("@DT_INQ_F"	, dtp작성일자.더하기(-90).문자("yyyyMMdd"));   // 유사 검색은 해당 작성일자의 ±90일치만 검색
			sql.Parameter.추가("@DT_INQ_T"	, dtp작성일자.더하기(90).문자("yyyyMMdd"));
			DataTable dt = sql.GetDataTable();

			// 스타일
			string style = @"
html, body	{ height:100%; }
.dx-viewbox	{ height:100%; }
.dx-viewbox th { text-align:center; }";

			// 바디
			string body = @"
<div>
	<table class='dx-viewbox'>
		<colgroup>
			<col style='width:30%; text-align:center;' />
			<col style='width:50%;' />
			<col style='width:20%; text-align:center;' />
		</colgroup>
		<tr>
			<th>파일번호</th>
			<th>문의번호</th>
			<th>구분</th>
		</tr>";

			for (int i = 0; i < 설정라인 - 1; i++)
			{
				string 파일번호	= dt.Rows.Count > i ? dt.Rows[i]["NO_FILE"].문자()	: "";
				string 문의번호	= dt.Rows.Count > i ? dt.Rows[i]["NO_REF"].문자()	: "";
				string 구분		= dt.Rows.Count > i ? (dt.Rows[i]["NO_REF"].문자() == 문의번호 ? "일치" : "유사") : "";

				body += @"
		<tr>
			<td>" + 파일번호.웹() + @"</td>
			<td>" + 문의번호.웹() + @"</td>
			<td>" + 구분.웹() + @"</td>
		</tr>";
			}
		
			body += @"
	</table>
</div>";
			web중복.바인딩(style, body, false);
		}

		#endregion
	}
}


