using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Windows.Print;

using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.BpControls;
using DX;

namespace cz
{
	public partial class H_CZ_COPY_FILE : Duzon.Common.Forms.CommonDialog
	{

		string NO_REF = "";
		string DT_INQ = "";
		string DT_QTN = "";
		string DT_VALID = "";

		#region ===================================================================================================== Property

		public string CD_COMPANY_S
		{
			get
			{
				return ctxNewS원본회사.CodeValue;
			}
		}

		public string CD_COMPANY_T
		{
			get
			{
				if (tabM.SelectedTab == tabNew) return ctxNewT대상회사.CodeValue;
				else return ctxOldT대상회사.CodeValue;
			}
		}

		public string NO_FILE_S
		{
			get
			{
				if (tabM.SelectedTab == tabNew) return txtNewS파일번호.Text;
				else return txtOldS파일번호.Text;
			}
			set
			{
				if (tabM.SelectedTab == tabNew) txtNewS파일번호.Text = value;
				else txtOldS파일번호.Text = value;
			}
		}

		public string NO_FILE_T
		{
			get
			{
				if (tabM.SelectedTab == tabNew) return txtNewT파일번호.Text;
				else return txtOldT파일번호.Text;
			}
			set
			{
				if (tabM.SelectedTab == tabNew) txtNewT파일번호.Text = value;
				else txtOldT파일번호.Text = value;
			}
		}

		public int NO_HST
		{
			get
			{
				return cbo차수.SelectedValue == null ? -1 : Util.GetTO_Int(cbo차수.SelectedValue);
			}
		}

		public decimal NO_DSP_MAX { get; set; }

		public Control 부모 { get; set; }
	
		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_COPY_FILE()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid(flexNewPH, flexNewSL);
			InitGrid(flexOldPH, flexOldSL);
			InitEvent();			
		}

		private void InitControl()
		{
			cbo차수.ValueMember = "NO_HST";
			cbo차수.DisplayMember = "NO_HST";
			
			ctxNewS원본회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			ctxNewS원본회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			ctxNewT대상회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			ctxNewT대상회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			ctxOldT대상회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			ctxOldT대상회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			Util.SetDB_CODE(cboNewT거래처그룹, "MA_B000065", true);
			Util.SetDB_CODE(cboOldT거래처그룹, "MA_B000065", true);

			Util.SetDB_CODE(cboOldS접속주소, "CZ_SA00029", false);
			if (Global.MainFrame.LoginInfo.CompanyCode == "TEST") cboOldS접속주소.SelectedIndex = 0;
			if (Global.MainFrame.LoginInfo.CompanyCode == "K100") cboOldS접속주소.SelectedIndex = 0;
			if (Global.MainFrame.LoginInfo.CompanyCode == "K200") cboOldS접속주소.SelectedIndex = 1;

			Util.SetDB_CODE(cboOldT통화명S, "MA_B000005", true);
			Util.SetDB_CODE(cboOldT통화명P, "MA_B000005", true);
			
			rdoNew복사대상3.Checked = true;
			rdoNew복사방식1.Checked = true;
			rdoN순번1.Checked = true;
			rdoOld복사대상2.Checked = true;
		}

		private void InitGrid(FlexGrid flexH, FlexGrid flexL)
		{		
			// 거래처
			flexH.BeginSetting(1, 1, false);
				
			flexH.SetCol("CHK"			, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexH.SetCol("NO_FILE"		, "파일번호"		, false);
			flexH.SetCol("CD_PARTNER"	, "거래처코드"	, 80);
			flexH.SetCol("LN_PARTNER"	, "거래처명"		, 230);
			flexH.SetCol("DT_INQ"		, "INQ일"		, false);
			flexH.SetCol("NO_REF"		, "견적번호"		, false);
			flexH.SetCol("DT_QTN"		, "QTN일"		, false);
			flexH.SetCol("DT_VALID"		, "유효일"		, false);
			flexH.SetCol("CD_EXCH"		, "통화명"		, 50);
			flexH.SetCol("RT_EXCH"		, "환율"			, 70	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexH.SetCol("CNT_ITEM"		, "아이템"		, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexH.SetCol("DC_RMK"		, "비고"			, 500);
				
			flexH.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["CD_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005"), "CODE", "NAME");
			//flexH.SetDummyColumn("CHK");

			flexH.SetOneGridBinding(new object[] { }, oneOldH);
				
			flexH.SettingVersion = "15.10.15.01";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
				
			// 아이템
			flexL.BeginSetting(1, 1, false);
				
			flexL.SetCol("CHK"				, "S"				, 30	, true, CheckTypeEnum.Y_N);			
			flexL.SetCol("NO_FILE"			, "파일번호"			, false);
			flexL.SetCol("NO_LINE"			, "고유번호"			, false);
			flexL.SetCol("NO_DSP"			, "순번"				, 40	, true);
			flexL.SetCol("NO_DSP_ORG"		, "순번ORG"			, false);
			flexL.SetCol("GRP_ITEM"			, "유형"				, false);
			flexL.SetCol("NM_SUBJECT"		, "주제"				, false);			
			flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"			, 90);
			flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"			, 180);
			flexL.SetCol("CD_ITEM"			, "재고코드"			, 80);
			flexL.SetCol("UNIT"				, "단위코드"			, false);
			flexL.SetCol("NM_UNIT"			, "단위"				, 45);			
			flexL.SetCol("QT"				, "수량"				, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("CD_SUPPLIER"		, "매입처코드"		, false);
			flexL.SetCol("LN_SUPPLIER"		, "매입처"			, 180);
			flexL.SetCol("UM_EX_STD_P"		, "매입견적단가"		, false);
			flexL.SetCol("AM_EX_STD_P"		, "매입견적금액"		, false);
			flexL.SetCol("RT_DC_P"			, "매입DC(%)"		, false);
			flexL.SetCol("UM_EX_P"			, "매입단가"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_P"			, "매입금액"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_P"			, "매입단가(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_P"			, "매입금액(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("RT_PROFIT"		, "이윤(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX_Q"			, "매출견적단가"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_Q"			, "매출견적금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_Q"			, "매출견적단가(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_Q"			, "매출견적금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("RT_DC"			, "DC(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX_S"			, "매출단가"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_S"			, "매출금액"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_S"			, "매출단가(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_S"			, "매출금액(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);			
			flexL.SetCol("RT_MARGIN"		, "최종(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("LT_P"				, "납기P"			, false);
			flexL.SetCol("LT"				, "납기"				, 50	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("YN_CHOICE"		, "선택"				, false);
			flexL.SetCol("TP_BOM"			, "BOM구분"			, false);
			flexL.SetCol("YN_GULL"			, "H"				, false);			
			flexL.SetCol("O_SUBJECT_CODE"	, "O_SUBJECT_CODE"	, false);	// 구전산정보
			flexL.SetCol("O_ITEM_CODE"		, "O_ITEM_CODE"		, false);	// 구전산정보
			flexL.SetCol("O_QTY"			, "O_QTY"			, false);	// 구전산정보

			flexL.Cols["NO_DSP"].Format = "####.##";
			flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDummyColumn("CHK");

			flexL.KeyActionEnter = KeyActionEnum.MoveDown;
			flexL.SettingVersion = "15.12.26.01";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			//flexL.SetExceptSumCol("UM_EX_STD_P", "RT_DC_P", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT_P", "LT");
			flexL.SetExceptSumCol("UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT_P", "LT");

			FlexUtil.AddEditStyle(flexL, "NO_DSP");
		}

		private void InitEvent()
		{
			txtNewS파일번호.KeyDown += new KeyEventHandler(txtNewS파일번호_KeyDown);
			txtOldS파일번호.KeyDown += new KeyEventHandler(txtOldS파일번호_KeyDown);
			cbo차수.SelectionChangeCommitted += new EventHandler(cbo차수_SelectionChangeCommitted);

			ctxOldT담당자.QueryAfter += new BpQueryHandler(ctx담당자_QueryAfter);
			ctxOldT매출처.QueryAfter += new BpQueryHandler(ctx매출처_QueryAfter);
			ctxOldT호선번호.QueryAfter += new BpQueryHandler(ctx호선번호_QueryAfter);

			rdoNew복사대상1.Click += new EventHandler(rdoNew복사대상_Click);
			rdoNew복사대상2.Click += new EventHandler(rdoNew복사대상_Click);
			rdoNew복사대상3.Click += new EventHandler(rdoNew복사대상_Click);
			   										
			rdoNew복사방식1.Click += new EventHandler(rdoNew복사방식_Click);
			rdoNew복사방식2.Click += new EventHandler(rdoNew복사방식_Click);
			rdoNew복사방식3.Click += new EventHandler(rdoNew복사방식_Click);

			rdoN순번1.Click += new EventHandler(rdoN순번_Click);
			rdoN순번2.Click += new EventHandler(rdoN순번_Click);

			tabM.SelectedIndexChanged += new EventHandler(tabM_SelectedIndexChanged);

			btnNew조회.Click += new EventHandler(btnNew조회_Click);
			btnNew복사.Click += new EventHandler(btnNew복사_Click);
			btnNew취소.Click += new EventHandler(btnNew취소_Click);

			btnOld조회.Click += new EventHandler(btnOld조회_Click);
			btnOld복사.Click += new EventHandler(btnOld복사_Click);
			btnOld취소.Click += new EventHandler(btnOld취소_Click);

			flexNewSL.DoubleClick += new EventHandler(flexNewSL_DoubleClick);
		}

		protected override void InitPaint()
		{
			txtNewS파일번호.Focus();
			rdoNew복사대상_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Event

		private void txtNewS파일번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btnNew조회_Click(null, null);
			}
		}

		private void txtOldS파일번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btnOld조회_Click(null, null);
			}
		}

		private void cbo차수_SelectionChangeCommitted(object sender, EventArgs e)
		{			
			DataTable dtSH = DBHelper.GetDataTable("SP_CZ_SA_QTNH_REG_SELECT", new object[] { CD_COMPANY_S, NO_FILE_S, NO_HST });
			DataTable dtSL = DBHelper.GetDataTable("SP_CZ_SA_QTNL_REG_SELECT_COPY", new object[] { CD_COMPANY_S, NO_FILE_S, NO_HST, 상수.사원번호, 상수.IP });

			if (dtSH.Rows.Count != 1) { Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다); return; }

			// 원본 정보
			txtNewS담당자.Text = dtSH.Rows[0]["NM_EMP"].ToString();
			txtNewS담당자.Tag = dtSH.Rows[0]["NO_EMP"].ToString();
			txtNewS영업그룹.Text = dtSH.Rows[0]["NM_SALEGRP"].ToString();
			txtNewS영업그룹.Tag = dtSH.Rows[0]["CD_SALEGRP"].ToString();
			txtNewS매출처.Text = dtSH.Rows[0]["LN_PARTNER"].ToString();
			txtNewS매출처.Tag = dtSH.Rows[0]["CD_PARTNER"].ToString();
			txtNewS거래처그룹.Text = dtSH.Rows[0]["NM_PARTNER_GRP"].ToString();
			txtNewS거래처그룹.Tag = dtSH.Rows[0]["CD_PARTNER_GRP"].ToString();
			txtNewS호선번호.Text = dtSH.Rows[0]["NO_HULL"].ToString();
			txtNewS호선번호.Tag = dtSH.Rows[0]["NO_IMO"].ToString();
			txtNewS선명.Text = dtSH.Rows[0]["NM_VESSEL"].ToString();

			// 대상 담당정보 초기화 (설정하도록 강제함)
			ctxNewT담당자.CodeValue = "";
			ctxNewT담당자.CodeName = "";
			ctxNewT영업그룹.CodeValue = "";
			ctxNewT영업그룹.CodeName = "";
			
			// 대상 호선정보 (기본적으로 변경 안함, 옵션 등에 의해서도 변경 안됨)
			ctxNewT호선번호.CodeValue = txtNewS호선번호.Tag.ToString();
			ctxNewT호선번호.CodeName = txtNewS호선번호.Text;
			txtNewT선명.Text = txtNewS선명.Text;

			// 대상 매출처 정보 & 매입처 정보 (복사방식에 의해 결정)
			rdoNew복사방식_Click(null, null);

			// 아이템
			if (!dtSL.Columns.Contains("CHK")) dtSL.Columns.Add("CHK").SetOrdinal(0);
			dtSL.Columns.Add("NO_DSP_ORG");
			foreach (DataRow row in dtSL.Rows)
			{
				row["CHK"] = "Y";
				row["NO_DSP_ORG"] = row["NO_DSP"];
			}
			flexNewSL.Binding = dtSL;
			ReDrawGrid(flexNewSL);
		}

		private void ctx담당자_QueryAfter(object sender, BpQueryArgs e)
		{
			BpCodeTextBox ctx담당자 = (BpCodeTextBox)sender;
			BpCodeTextBox ctx영업그룹;

			if (ctx담당자.Name.IndexOf("New") > 0)	ctx영업그룹 = null;
			else									ctx영업그룹 = ctxOldT영업그룹;

			DataTable dt = Util.GetDB_SALEGRP(CD_COMPANY_T, ctx담당자.CodeValue);
			if (dt.Rows.Count > 0)
			{
				ctx영업그룹.CodeValue = dt.Rows[0]["CD_SALEGRP"].ToString();
				ctx영업그룹.CodeName = dt.Rows[0]["NM_SALEGRP"].ToString();
			}
		}

		private void ctx매출처_QueryAfter(object sender, BpQueryArgs e)
		{
			//DataTable dt = Util.GetDB_PARTNER(CD_COMPANY, ctx거래처.CodeValue);
			//if (dt.Rows.Count > 0) cbo거래처그룹.SelectedValue = dt.Rows[0]["CD_PARTNER_GRP"].ToString();
			//sinq.SetPartnerAttn(ctx거래처.CodeValue);
			//txt문의번호.Focus();
		}

		private void ctx호선번호_QueryAfter(object sender, BpQueryArgs e)
		{
			//txt선명.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
			//string CD_PARTNER = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
			//if (e.HelpReturn.Rows[0]["CD_PARTNER"].ToString() != "")
			//{
			//    ctx거래처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
			//    ctx거래처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
			//    //ctx거래처.CodeValue = e.HelpReturn.Rows[0]["INVOICE_COMPANY"].ToString();
			//    ctx거래처_QueryAfter(null, null);
			//}
		}

		private void rdoNew복사대상_Click(object sender, EventArgs e)
		{
			if (rdoNew복사대상1.Checked)
			{
				rdoNew복사방식1.Checked = true;
				rdoNew복사방식2.Enabled = false;
				rdoNew복사방식3.Enabled = false;
			}
			else
			{
				rdoNew복사방식2.Enabled = true;
				rdoNew복사방식3.Enabled = true;
			}
		}

		private void rdoNew복사방식_Click(object sender, EventArgs e)
		{
			if (rdoNew복사방식1.Checked)
			{
				// 매출처 → 매출처
				ctxNewT매출처.CodeValue = txtNewS매출처.Tag.ToString();
				ctxNewT매출처.CodeName = txtNewS매출처.Text;
				cboNewT거래처그룹.SelectedValue = txtNewS거래처그룹.Tag.ToString();

				// 매입처
				DataTable dtPH = DBHelper.GetDataTable("SP_CZ_PU_QTNH_RPT_SELECT", new object[] { ctxNewS원본회사.CodeValue, txtNewS파일번호.Text });
				dtPH.Columns.Add("CHK").SetOrdinal(0);
				foreach (DataRow row in dtPH.Rows) row["CHK"] = "Y";
				flexNewPH.Binding = dtPH;
			}
			else if (rdoNew복사방식2.Checked)
			{
				// 매출처 → 삭제
				ctxNewT매출처.CodeValue = "";
				ctxNewT매출처.CodeName = "";

				// 매입처 → 딘텍
				DataTable dtPH = DBHelper.GetDataTable("SP_CZ_PU_QTNH_RPT_SELECT", new object[] { "!@#", "!@#" });
				dtPH.Columns.Add("CHK").SetOrdinal(0);
				dtPH.Rows.Add();
				dtPH.Rows[0]["CD_PARTNER"] = "00000";
				dtPH.Rows[0]["LN_PARTNER"] = "(주)딘텍";

				flexNewPH.Binding = dtPH;
			}
			else if (rdoNew복사방식3.Checked)
			{
				// 매출처 → 딘텍
				ctxNewT매출처.CodeValue = "00000";
				ctxNewT매출처.CodeName = "(주)딘텍";

				// 매입처 → 삭제
				if (flexNewPH.DataTable != null) flexNewPH.DataTable.Rows.Clear();
			}
		}

		private void rdoN순번_Click(object sender, EventArgs e)
		{
			if (rdoN순번1.Checked)
			{
				for (int i = flexNewSL.Rows.Fixed; i < flexNewSL.Rows.Count; i++)
				{
					flexNewSL[i, "NO_DSP"] = flexNewSL[i, "NO_DSP_ORG"];
				}
			}
			else
			{
				int seq = 1;

				for (int i = flexNewSL.Rows.Fixed; i < flexNewSL.Rows.Count; i++)
				{
					flexNewSL[i, "NO_DSP"] = NO_DSP_MAX + seq;
					seq++;
				}
			}
		}
		
		private void tabM_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabM.SelectedTab == tabNew)	txtNewS파일번호.Focus();
			else							txtOldS파일번호.Focus();
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btnNew조회_Click(object sender, EventArgs e)
		{
			txtNewS파일번호.Text = txtNewS파일번호.Text.ToUpper();
			BindHistory();	// 차수 바인딩 - 조회하기 전에 바인딩하여 차수를 파라미터로 넘김
			cbo차수_SelectionChangeCommitted(null, null);

			// 스크린샷
			//Rectangle rect = new Rectangle
			//	(

			//	);

			//Rectangle rect = 부모.Bounds;
			//Rectangle rect = Bounds;
			Rectangle rect = SystemInformation.VirtualScreen;

			Bitmap img = new Bitmap(rect.Width, rect.Height);
			//Bitmap img = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);

			using (Graphics g = Graphics.FromImage(img))
			{
				g.CopyFromScreen(rect.X, rect.Y, 0, 0, img.Size, CopyPixelOperation.SourceCopy);
				img.Save(파일.경로_임시() + @"\" + txtNewS파일번호.Text + ".jpg");
				파일.업로드(파일.경로_임시() + @"\" + txtNewS파일번호.Text + ".jpg", "COPY", true);
			}

		}

		private void btnOld조회_Click(object sender, EventArgs e)
		{			
			txtOldS파일번호.Text = txtOldS파일번호.Text.ToUpper();
			string flag = rdoOld복사대상1.Checked ? "QUOTATION" : "ORDER";

			// 접속주소
			DBConn conn = DBConn.Old_DINTEC;
			if (cboOldS접속주소.SelectedIndex == 0) conn = DBConn.Old_DINTEC;
			else if (cboOldS접속주소.SelectedIndex == 1) conn = DBConn.Old_DUBHECO;
			else if (cboOldS접속주소.SelectedIndex == 2) conn = DBConn.Old_GMI;

			DBMgr dbm = new DBMgr(conn);
			dbm.Procedure = "SP_IU_GET_" + flag;
			dbm.AddParameter("IN_FILE_NO", txtOldS파일번호.Text);	
			DataTable dtH = dbm.GetDataTable();

			if (dtH.Rows.Count == 0) { Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다); return; }

			// ========== 원본 정보
			txtOldS담당자.Text = dtH.Rows[0]["NM_EMP"].ToString();
			txtOldS담당자.Tag = dtH.Rows[0]["NO_EMP"].ToString();
			txtOldS매출처.Text = dtH.Rows[0]["LN_PARTNER"].ToString();
			txtOldS매출처.Tag = dtH.Rows[0]["CD_PARTNER"].ToString();
			txtOldS지역구분.Text = dtH.Rows[0]["NM_GRP"].ToString();
			txtOldS호선번호.Text = dtH.Rows[0]["NO_HULL"].ToString();
			txtOldS선명.Text = dtH.Rows[0]["NM_VESSEL"].ToString();

			// ========== 대상 정보
			string query = "";

			query += ""
				+ "\n" + "SELECT"
				+ "\n" + "	A.NO_EMP, A.NM_KOR AS NM_EMP, B.CD_SALEGRP, C.NM_SALEGRP"
				+ "\n" + "FROM		MA_EMP		AS A"
				+ "\n" + "LEFT JOIN MA_USER		AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_EMP = B.NO_EMP"
				+ "\n" + "LEFT JOIN MA_SALEGRP	AS C ON B.CD_COMPANY = C.CD_COMPANY AND B.CD_SALEGRP = C.CD_SALEGRP"
				+ "\n" + "WHERE 1 = 1"
				+ "\n" + "	AND A.CD_COMPANY = '" + CD_COMPANY_T + "'"
				+ "\n" + "	AND A.NO_EMP = '" + dtH.Rows[0]["NO_EMP"] + "'";

			query += ""
				+ "\n" + "SELECT"
				+ "\n" + "	*"
				+ "\n" + "FROM MA_PARTNER	AS A"
				+ "\n" + "WHERE 1 = 1"
				+ "\n" + "	AND CD_COMPANY = '" + CD_COMPANY_T + "'"
				+ "\n" + "	AND CD_PARTNER = '" + dtH.Rows[0]["CD_PARTNER"] + "'";

			query += ""
				+ "\n" + "SELECT"
				+ "\n" + "	*"
				+ "\n" + "FROM CZ_MA_HULL	AS A"
				+ "\n" + "WHERE 1 = 1"
				+ "\n" + "	AND NO_HULL = '" + dtH.Rows[0]["NO_HULL"] + "'";

			DataSet dsT = DBHelper.GetDataSet(query);
			DataRow drEMP	  = dsT.Tables[0].Rows.Count == 1 ? dsT.Tables[0].Rows[0] : null;
			DataRow drPARTNER = dsT.Tables[1].Rows.Count == 1 ? dsT.Tables[1].Rows[0] : null;
			DataRow drHULL	  = dsT.Tables[2].Rows.Count == 1 ? dsT.Tables[2].Rows[0] : null;
				
			txtOldT파일번호.Text = txtOldS파일번호.Text;
			NO_REF = dtH.Rows[0]["NO_REF"].ToString();
			DT_INQ = dtH.Rows[0]["DT_INQ"].ToString();
			DT_QTN = dtH.Rows[0]["DT_QTN"].ToString();
			DT_VALID = dtH.Rows[0]["DT_VALID"].ToString();
				
			if (drEMP != null)
			{
				ctxOldT담당자.CodeValue = drEMP["NO_EMP"].ToString();
				ctxOldT담당자.CodeName = drEMP["NM_EMP"].ToString();
				ctxOldT영업그룹.CodeValue = drEMP["CD_SALEGRP"].ToString();
				ctxOldT영업그룹.CodeName = drEMP["NM_SALEGRP"].ToString();
			}
			if (drPARTNER != null)
			{ 
				ctxOldT매출처.CodeValue = drPARTNER["CD_PARTNER"].ToString();
				ctxOldT매출처.CodeName = drPARTNER["LN_PARTNER"].ToString();
				cboOldT거래처그룹.SelectedValue = drPARTNER["CD_PARTNER_GRP"].ToString();
			}
			if (drHULL != null)
			{ 
				ctxOldT호선번호.CodeValue = drHULL["NO_IMO"].ToString();
				ctxOldT호선번호.CodeName = drHULL["NO_HULL"].ToString();
				txtOldT선명.Text = drHULL["NM_VESSEL"].ToString();
			}

			if (drEMP == null)		ctxOldT담당자.CodeName = "검색 결과 없음";
			if (drPARTNER == null)	ctxOldT매출처.CodeName = "검색 결과 없음";
			if (drHULL == null)		ctxOldT호선번호.CodeName = "검색 결과 없음";

			// 환율코드 변환
			DataTable dtEXCH = (DataTable)cboOldT통화명S.DataSource;

			if (dtH.Rows[0]["NM_EXCH"].ToString() == "US$")
			{
				cboOldT통화명S.SelectedValue = "001";
			}
			else
			{
				foreach (DataRow row in dtEXCH.Rows)
				{
					if (dtH.Rows[0]["NM_EXCH"].ToString() == row["NAME"].ToString())
					{
						cboOldT통화명S.SelectedValue = row["CODE"];
						break;
					}
				}
			}
				
			curOldT환율S.DecimalValue = Util.GetTO_Decimal(dtH.Rows[0]["RT_EXCH"]);

			// ========== 매입처 리스트
			dbm.ClearParameter();
			dbm.Procedure = "SP_IU_GET_" + flag + "_SUPPLIER";
			dbm.AddParameter("IN_FILE_NO", txtOldS파일번호.Text);
			DataTable dtPH = dbm.GetDataTable();
				
			// 환율코드 변환
			for (int i = 0; i < dtPH.Rows.Count; i++)
			{
				if (dtPH.Rows[i]["NM_EXCH"].ToString() == "US$")
				{
					dtPH.Rows[i]["CD_EXCH"] = "001";
				}
				else
				{
					foreach (DataRow row in dtEXCH.Rows)
					{
						if (dtPH.Rows[i]["NM_EXCH"].ToString() == row["NAME"].ToString())
						{
							dtPH.Rows[i]["CD_EXCH"] = row["CODE"];
							break;
						}
					}
				}
			}

			flexOldPH.Binding = dtPH;

			// ========== 아이템 리스트
			dbm.ClearParameter();
			dbm.Procedure = "SP_IU_GET_" + flag + "_ITEM";
			dbm.AddParameter("IN_FILE_NO", txtOldS파일번호.Text);
			flexOldSL.Binding = dbm.GetDataTable();
				
			// 계산
			for (int i = 2; i < flexOldSL.Rows.Count; i++) CalcRow(i);			
		}

		private void btnNew복사_Click(object sender, EventArgs e)
		{
			if (txtNewT파일번호.Text == "") { Global.MainFrame.ShowMessage("파일번호를 입력하십시오."); return; }
			if (ctxNewT담당자.CodeValue == "") { Global.MainFrame.ShowMessage("담당자를 선택하십시오."); return; }
			if (ctxNewT영업그룹.CodeValue == "") { Global.MainFrame.ShowMessage("영업그룹을 선택하십시오."); return; }
			if (ctxNewT매출처.CodeValue == "") { Global.MainFrame.ShowMessage("매출처를 선택하십시오."); return; }
			if (cboNewT거래처그룹.SelectedIndex == 0) { Global.MainFrame.ShowMessage("거래처그룹을 선택하십시오."); return; }
			if (ctxNewT호선번호.CodeValue == "") { Global.MainFrame.ShowMessage("호선번호를 선택하십시오."); return; }

			string COPY_TABLE = "";
			string COPY_MODE = "";

			if (rdoNew복사대상1.Checked) COPY_TABLE = "ITEM";
			if (rdoNew복사대상2.Checked) COPY_TABLE = "INQ";
			if (rdoNew복사대상3.Checked) COPY_TABLE = "QTN";

			if (rdoNew복사방식1.Checked) COPY_MODE = "USUAL";
			if (rdoNew복사방식2.Checked) COPY_MODE = "TO_SUPPLIER";
			if (rdoNew복사방식3.Checked) COPY_MODE = "TO_BUYER";

			// 대상파일 XML 만들기
			DataTable dtSH = new DataTable();
			dtSH.Columns.Add("NO_EMP");
			dtSH.Columns.Add("CD_SALEGRP");
			dtSH.Columns.Add("CD_PARTNER");
			dtSH.Columns.Add("CD_PARTNER_GRP");
			dtSH.Columns.Add("NO_IMO");

			dtSH.Rows.Add();
			dtSH.Rows[0]["NO_EMP"] = ctxNewT담당자.CodeValue;
			dtSH.Rows[0]["CD_SALEGRP"] = ctxNewT영업그룹.CodeValue;
			dtSH.Rows[0]["CD_PARTNER"] = ctxNewT매출처.CodeValue;
			dtSH.Rows[0]["CD_PARTNER_GRP"] = cboNewT거래처그룹.SelectedValue;
			dtSH.Rows[0]["NO_IMO"] = ctxNewT호선번호.CodeValue;

			DataTable dtPH = flexNewPH.GetCheckedRows("CHK");
			DataTable dtSL = flexNewSL.GetCheckedRows("CHK");

			string xmlSH = Util.GetTO_Xml(dtSH);
			string xmlPH = Util.GetTO_Xml(dtPH);
			string xmlSL = Util.GetTO_Xml(dtSL);

			object[] parameter =
				{ COPY_TABLE, COPY_MODE
				, Global.MainFrame.LoginInfo.UserID
			    , CD_COMPANY_S, NO_FILE_S
				, CD_COMPANY_T, NO_FILE_T
			    , xmlSH, xmlSL, xmlPH };

			DBMgr.ExecuteNonQuery("SP_CZ_SA_QTN_REG_XML_COPY_NEW", DebugMode.Print, parameter);

			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			this.DialogResult = DialogResult.OK;
		}

		private void btnOld복사_Click(object sender, EventArgs e)
		{
			if (txtOldT파일번호.Text == "")				{ Global.MainFrame.ShowMessage("파일번호를 입력하십시오."); return; }
			if (ctxOldT담당자.CodeValue == "")			{ Global.MainFrame.ShowMessage("담당자를 선택하십시오."); return; }
			if (ctxOldT영업그룹.CodeValue == "")			{ Global.MainFrame.ShowMessage("영업그룹을 선택하십시오."); return; }
			if (ctxOldT매출처.CodeValue == "")			{ Global.MainFrame.ShowMessage("매출처를 선택하십시오."); return; }
			if (cboOldT거래처그룹.SelectedIndex == 0)		{ Global.MainFrame.ShowMessage("거래처그룹을 선택하십시오."); return; }
			if (ctxOldT호선번호.CodeValue == "")			{ Global.MainFrame.ShowMessage("호선번호를 선택하십시오."); return; }

			// SH
			DataTable dtSH = new DataTable();
			dtSH.Rows.Add();
			dtSH.Columns.Add("CD_COMPANY");		dtSH.Rows[0]["CD_COMPANY"] = CD_COMPANY_T;
			dtSH.Columns.Add("NO_FILE");		dtSH.Rows[0]["NO_FILE"] = txtOldT파일번호.Text;
			dtSH.Columns.Add("DT_INQ");			dtSH.Rows[0]["DT_INQ"] = DT_INQ;
			dtSH.Columns.Add("NO_EMP");			dtSH.Rows[0]["NO_EMP"] = ctxOldT담당자.CodeValue;
			dtSH.Columns.Add("CD_SALEGRP");		dtSH.Rows[0]["CD_SALEGRP"] = ctxOldT영업그룹.CodeValue;
			dtSH.Columns.Add("CD_PARTNER");		dtSH.Rows[0]["CD_PARTNER"] = ctxOldT매출처.CodeValue;
			dtSH.Columns.Add("CD_PARTNER_GRP");	dtSH.Rows[0]["CD_PARTNER_GRP"] = cboOldT거래처그룹.SelectedValue;
			dtSH.Columns.Add("NO_IMO");			dtSH.Rows[0]["NO_IMO"] = ctxOldT호선번호.CodeValue;
			dtSH.Columns.Add("NO_REF");			dtSH.Rows[0]["NO_REF"] = NO_REF;
			dtSH.Columns.Add("DT_QTN");			dtSH.Rows[0]["DT_QTN"] = DT_QTN;
			dtSH.Columns.Add("DT_VALID");		dtSH.Rows[0]["DT_VALID"] = DT_VALID;
			dtSH.Columns.Add("CD_EXCH");		dtSH.Rows[0]["CD_EXCH"] = cboOldT통화명S.SelectedValue;
			dtSH.Columns.Add("RT_EXCH");		dtSH.Rows[0]["RT_EXCH"] = curOldT환율S.DecimalValue;

			// SL
			DataTable dtSL = flexOldSL.GetTableFromGrid();
			dtSL.Columns.Add("CD_COMPANY", typeof(string), "'" + CD_COMPANY_T +"'");

			// PH
			DataTable dtPH = flexOldPH.GetTableFromGrid();
			dtPH.Columns.Add("CD_COMPANY", typeof(string), "'" + CD_COMPANY_T + "'");

			// PL
			DataTable dtPL = flexOldSL.GetTableFromGrid();
			dtPL.Columns.Add("CD_COMPANY", typeof(string), "'" + CD_COMPANY_T + "'");
			dtPL.Columns.Remove("RT_DC");
			dtPL.Columns.Remove("LT");
			dtPL.Columns["CD_SUPPLIER"].ColumnName = "CD_PARTNER";
			dtPL.Columns["UM_EX_STD_P"].ColumnName = "UM_EX_STD";
			dtPL.Columns["AM_EX_STD_P"].ColumnName = "AM_EX_STD";
			dtPL.Columns["RT_DC_P"].ColumnName = "RT_DC";
			dtPL.Columns["UM_EX_P"].ColumnName = "UM_EX";
			dtPL.Columns["AM_EX_P"].ColumnName = "AM_EX";
			dtPL.Columns["UM_KR_P"].ColumnName = "UM_KR";
			dtPL.Columns["AM_KR_P"].ColumnName = "AM_KR";
			dtPL.Columns["LT_P"].ColumnName = "LT";

			// 파일번호 변경
			foreach (DataRow row in dtSL.Rows) row["NO_FILE"] = txtOldT파일번호.Text;
			foreach (DataRow row in dtPH.Rows) row["NO_FILE"] = txtOldT파일번호.Text;
			foreach (DataRow row in dtPL.Rows) row["NO_FILE"] = txtOldT파일번호.Text;

			// DB 저장
			string xmlSH = Util.GetTO_Xml(dtSH);
			string xmlSL = Util.GetTO_Xml(dtSL);
			string xmlPH = Util.GetTO_Xml(dtPH);
			string xmlPL = Util.GetTO_Xml(dtPL);

			DBMgr.ExecuteNonQuery("SP_CZ_SA_QTNH_REG_COPY_OLD", new object[] { xmlSH, xmlSL, xmlPH, xmlPL });

			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			this.DialogResult = DialogResult.OK;
		}

		private void btnNew취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOld취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexNewSL_DoubleClick(object sender, EventArgs e)
		{
			ReDrawGrid((FlexGrid)sender);
		}

		private void ReDrawGrid(FlexGrid flex)
		{
			flex.Redraw = false;

			for (int i = flex.Rows.Fixed; i < flex.Rows.Count; i++)
			{
				if (flex[i, "TP_BOM"].ToString() == "P")	  flex.SetCellImage(i, flex.Cols["CD_ITEM_PARTNER"].Index, Icons.FolderExpand);
				else if (flex[i, "TP_BOM"].ToString() == "S") flex.SetCellImage(i, flex.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
				else if (flex[i, "TP_BOM"].ToString() == "C") flex.SetCellImage(i, flex.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_20x6);
			}

			flex.Redraw = true;
		}

		#endregion

		#region ==================================================================================================== 계산식

		private void CalcRow(int row)
		{			
			decimal UM_KR_P = Util.GetTO_Decimal(flexOldSL[row, "UM_KR_P"]);
			decimal AM_KR_P = Util.GetTO_Decimal(flexOldSL[row, "AM_KR_P"]);
			decimal UM_KR_S = Util.GetTO_Decimal(flexOldSL[row, "UM_KR_S"]);
			decimal AM_KR_S = Util.GetTO_Decimal(flexOldSL[row, "AM_KR_S"]);

			decimal RT_PROFIT = UM_KR_S == 0 ? 0 : 100 * (1 - UM_KR_P / UM_KR_S);	// 이윤(%) 계산			
			decimal RT_MARGIN = AM_KR_S == 0 ? 0 : 100 * (1 - AM_KR_P / AM_KR_S);	// 마진(%) 계산

			flexOldSL[row, "RT_PROFIT"] = RT_PROFIT;
			flexOldSL[row, "RT_MARGIN"] = RT_MARGIN;
		}

		#endregion

		private void BindHistory()
		{
			// 차수 조회 및 바인딩
			string query = ""
				+ "\n" + "SELECT"
				+ "\n" + "	*"
				+ "\n" + "FROM"
				+ "\n" + "("
				+ "\n" + "	SELECT"
				+ "\n" + "		NO_HST"
				+ "\n" + "	FROM CZ_SA_QTNH"
				+ "\n" + "	WHERE 1 = 1"
				+ "\n" + "		AND CD_COMPANY = '" + CD_COMPANY_S + "'"
				+ "\n" + "		AND NO_FILE = '" + NO_FILE_S + "'"
				+ "\n" + ""
				+ "\n" + "	UNION ALL"
				+ "\n" + ""
				+ "\n" + "	SELECT"
				+ "\n" + "		NO_HST"
				+ "\n" + "	FROM CZ_SA_QTNH_HST"
				+ "\n" + "	WHERE 1 = 1"
				+ "\n" + "		AND CD_COMPANY = '" + CD_COMPANY_S + "'"
				+ "\n" + "		AND NO_FILE = '" + NO_FILE_S + "'"
				+ "\n" + ")	AS A"
				+ "\n" + "ORDER BY NO_HST";

			DataTable dt = DBHelper.GetDataTable(query);
			cbo차수.DataSource = dt;
			cbo차수.SelectedIndex = cbo차수.Items.Count - 1;
		}
	}
}
