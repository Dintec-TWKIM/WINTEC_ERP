using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Windows.Print;
using DX;

namespace cz
{
	public partial class P_CZ_PU_RCV_REG : PageBase
	{
		string CompanyCode;
		bool IsStock = true;

		#region ===================================================================================================== Property

		public string FileNumber
		{
			get
			{
				return tbx파일번호.Text;
			}
			set
			{
				tbx파일번호.Text = value;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PU_RCV_REG()
		{
			StartUp.Certify(this);
			CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();			
		}

		public P_CZ_PU_RCV_REG(string NO_FILE)
		{
			StartUp.Certify(this);
			CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			pnl매출처.Enabled(false);
			pnl선명.Enabled(false);

			cbo입고일자.ValueMember = "NO_IO";
			cbo입고일자.DisplayMember = "DT_GR";

			cbo매입처.ValueMember = "CD_PARTNER";
			cbo매입처.DisplayMember = "LN_PARTNER";
			
			ctx로케이션.Enabled = false;

			dtp조정일자.Text = Util.GetToday();
			MainGrids = new FlexGrid[] { grd라인 };

			Grant.CanPrint = false;
			Grant.CanDelete = false;
		}

		private void InitGrid()
		{
			grd라인.BeginSetting(1, 1, false);

			grd라인.SetCol("CHK"				, "S"			, 30	, true	, CheckTypeEnum.Y_N);			
			grd라인.SetCol("NO_DSP"			, "순번"			, 40);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 140);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 230);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 80);
			grd라인.SetCol("CD_PARTNER"		, "매입처코드"	, false);
			grd라인.SetCol("SN_PARTNER"		, "매입처"		, 150);

			grd라인.SetCol("NM_EMP_SLOG"		, "영업물류"		, 70);

			grd라인.SetCol("CD_UNIT_MM"		, "단위"			, 45);
			grd라인.SetCol("QT_PO"			, "발주"			, 57	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("QT_GR"			, "입고"			, 57	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("QT_NGR"			, "미입고"		, 57	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("QT_WORK"			, "입고적용"		, 57	, true	, typeof(decimal), FormatTpType.FOREIGN_MONEY);		// EDIT
			grd라인.SetCol("QT_IO"			, "입고상세"		, 57	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("DT_IO"			, "입고일자"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd라인.SetCol("DT_INSERT"		, "실입고일자"	, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd라인.SetCol("CD_SL"			, "창고"			, 130);
			grd라인.SetCol("CD_LOCATION"		, "로케이션"		, 70);
			grd라인.SetCol("NM_EMP"			, "담당자"		, 70);
			grd라인.SetCol("NO_IO"			, "입고번호"		, 100);
			grd라인.SetCol("NO_IOLINE"		, "입고항번"		, false);
			grd라인.SetCol("NO_DRAWING"		, "도면번호"		, false);
			grd라인.SetCol("WEIGHT"			, "무게"			, 60	, true	, typeof(decimal), FormatTpType.FOREIGN_MONEY);		// EDIT
			grd라인.SetCol("NO_PO"			, "발주번호"		, false);
			grd라인.SetCol("NO_LINE"			, "발주라인"		, false);
			grd라인.SetCol("QT_WORK2"		, "양품적용"		, 57	, true	, typeof(decimal), FormatTpType.FOREIGN_MONEY);		// EDIT
			grd라인.SetCol("QT_MSL"			, "양품확인"		, 57	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd라인.Cols["NO_DSP"].Format = "####.##";
			grd라인.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CD_UNIT_MM"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NM_EMP_SLOG"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CD_LOCATION"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NO_IO"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.SetDataMap("CD_UNIT_MM", GetDb.Code("MA_B000004"), "CODE", "NAME");
			grd라인.SetDataMap("CD_SL", GetDb.Storage(), "CD_SL", "NM_SL");
			grd라인.SetDummyColumn("CHK");
		
			grd라인.SetDefault("19.12.19.01", SumPositionEnum.Top);
			grd라인.SetSumColumnStyle("QT_WORK", "QT_WORK2");
			grd라인.SetEditColumn("QT_WORK", "WEIGHT", "QT_WORK2");	// 차후 ENABLE 상태에 따라 변경되도록 수정하자

			CellStyle style = grd라인.Styles.Add("NGR");
			style.Font = new Font(grd라인.Font, FontStyle.Bold);
			style.ForeColor = Color.Red;
		}
				
		protected override void InitPaint()
		{
			tbx파일번호.Focus();
			//FileNumber = "test01";
		}

		protected override bool IsChanged()
		{
			// 그리드 변경
			if (base.IsChanged())
				return true;
			else
				return false;
		}

		private void Clear()
		{
			IsStock = true;
			tbx파일번호.Clear();
			cbo입고일자.Clear();
			tbx매출처.Clear();
			tbx선명.Clear();
			cbo매입처.Clear();
			ctx입고창고.Clear();
			ctx로케이션.Clear();
			ctx로케이션.Enabled = false;
			dtp조정일자.Clear(true);
			chk조정일자.Checked = false;
			cur무게.Clear();
			tbx비고.Clear();
			grd라인.Clear(false);
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			tbx파일번호.KeyDown += new KeyEventHandler(tbx파일번호_KeyDown);
			cbo입고일자.SelectionChangeCommitted += new EventHandler(cbo입고일자_SelectionChangeCommitted);
			cbo매입처.SelectionChangeCommitted += new EventHandler(cbo매입처_SelectionChangeCommitted);
			ctx입고창고.QueryBefore += new BpQueryHandler(ctx입고창고_QueryBefore);
			ctx입고창고.QueryAfter += new BpQueryHandler(ctx입고창고_QueryAfter);
			ctx로케이션.EnabledChanged += new EventHandler(ctx로케이션_EnabledChanged);

			btn쪽지재전송.Click += new EventHandler(btn쪽지재전송_Click);
			btn수량등록.Click += new EventHandler(btn수량등록_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			grd라인.DoubleClick += new EventHandler(grd라인_DoubleClick);
			grd라인.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);
		}		

		private void tbx파일번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				OnToolBarSearchButtonClicked(null, null);
		}

		private void cbo입고일자_SelectionChangeCommitted(object sender, EventArgs e)
		{
			Search();
		}

		private void cbo매입처_SelectionChangeCommitted(object sender, EventArgs e)
		{
			Search();
		}

		private void ctx입고창고_QueryBefore(object sender, BpQueryArgs e)
		{
			string sl = "''";

			if (IsStock)
				sl = "'SL02', 'SL998', 'SL999', 'VL01', 'HO01'";
			else
				sl = "'SL01', 'SL998', 'SL999', 'VL01', 'VL02'";
			
			e.HelpParam.P00_CHILD_MODE = "입고창고";

			e.HelpParam.P61_CODE1 = @"
	  CD_SL AS CODE
	, NM_SL AS NAME";

			e.HelpParam.P62_CODE2 = @"
MA_SL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_SL IN (" + sl + @")
ORDER BY CD_SL";
		}

		private void ctx입고창고_QueryAfter(object sender, BpQueryArgs e)
		{
			string query = @"
SELECT
	YN_LOCATION
FROM MA_SL
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_SL = '" + ctx입고창고.CodeValue + "'";

			string locUsable = DBMgr.GetDataTable(query).Rows[0][0].ToString();

			if (locUsable == "Y")
				ctx로케이션.Enabled = true;
			else
				ctx로케이션.Enabled = false;

			ctx로케이션.Clear();
		}

		private void ctx로케이션_EnabledChanged(object sender, EventArgs e)
		{
			if (ctx로케이션.Enabled)
				ctx로케이션.ItemBackColor = Color.FromArgb(255, 237, 242);
			else
				ctx로케이션.ItemBackColor = Color.White;
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn쪽지재전송_Click(object sender, EventArgs e)
		{
			DataTable dtChecked = grd라인.GetCheckedRows("CHK");
			//Messenger.SendStockMsg(GetTo.Xml(dtChecked, "", "CD_ITEM"), "ERP_RESEND");

			DataTable dt = 디비.결과("PI_CZ_DX_AUTO_STK_BOOK", dtChecked.Json("CD_ITEM"), "ERP_RESEND");
			메신져.재고예약쪽지(dt);

		}

		private void btn수량등록_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "CHK"].ToString() == "Y")
					grd라인[i, "QT_WORK"] = grd라인[i, "QT_NGR"];
			}
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			// 양품확인된 항목이 있는지 체크
			//if (grd라인.DataTable.Select("CHK = 'Y' AND QT_MSL > 0").Length > 0)
			//{
			//    ShowMessage("양품확인된 항목은 삭제할 수 없습니다.");
			//    return;
			//}

			// 삭제
			grd라인.Redraw = false;

			DataRow[] rows = grd라인.DataTable.Select("CHK = 'Y'");

			for (int i = 0; i < rows.Length; i++)
			{
				MsgControl.ShowMsg("삭제중입니다. (" + (i + 1) + "/" + rows.Length + ")");
				rows[i].Delete();
			}
			
			grd라인.Redraw = true;
			MsgControl.CloseMsg();
		}
	
		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grd라인_DoubleClick(object sender, EventArgs e)
		{
			if (!grd라인.HasNormalRow || grd라인.MouseCol <= 0)
				return;

			// 헤더클릭
			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 입고일자 콤보 바로가기 (편의)
			if (grd라인.Cols[grd라인.Col].Name == "NO_IO")
			{
				string ioNumber = grd라인["NO_IO"].ToString();

				if (ioNumber != "")
				{
					cbo입고일자.SetValue(ioNumber);
					cbo입고일자_SelectionChangeCommitted(null, null);
				}				
			}
		}

		private void grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;
			string oldValue = GetTo.String(grd라인[e.Row, e.Col]);
			
			if (colName == "QT_WORK")
			{
				if (GetTo.Int(grd라인.EditData) > 0)
					grd라인["CHK"] = "Y";
				else
					grd라인["CHK"] = "N";
			}
			else if (colName == "QT_WORK2")
			{
				if (GetTo.Int(grd라인.EditData) > 0)
				{
					if (grd라인["CD_SL"].ToString().In("SL998", "SL999"))
					{
						grd라인["CHK"] = "Y";
					}
					else
					{
						ShowMessage("사양확인 로케이션 또는 미입고 로케이션만 양품적용 가능합니다.");
						grd라인["QT_WORK2"] = oldValue;
						e.Cancel = true;
						tbx파일번호.Focus();	// 제자리로 돌아오기 위해 포커스 왔다갔다 한번 하기
						grd라인.Focus();
					}
				}
				else
				{
					grd라인["CHK"] = "N";
				}
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			string query = "";
			tbx파일번호.Text = EngHanConverter.KorToEng(tbx파일번호.Text);			

			// ********** 매출처 정보
			query = @"
SELECT
	  LN_PARTNER
	, NM_VESSEL
	, NO_HULL
FROM V_CZ_SA_SOH 
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND NO_SO = '" + FileNumber + "'";

			DataTable dtSo = DBMgr.GetDataTable(query);

			if (dtSo.Rows.Count == 1)
			{
				tbx매출처.Text = dtSo.Rows[0]["LN_PARTNER"].ToString();
				tbx선명.Text = dtSo.Rows[0]["NM_VESSEL"] + " (" + dtSo.Rows[0]["NO_HULL"] + ")";
			}

			// 조회
			if (FileNumber.Left(2).In("ST", "DT", "GT") || FileNumber.Right(2).In("ST") || FileNumber == "TEST01")
			{
				// ********** 재고품
				IsStock = true;

				// 입고이력 => 발주번호로 조회
				query = @"
SELECT
	  A.NO_RCV			AS NO_IO
	, SUBSTRING(B.DT_REQ, 1, 4) + '/'
	+ SUBSTRING(B.DT_REQ, 5, 2) + '/'
	+ SUBSTRING(B.DT_REQ, 7, 2) + ' '
	+ SUBSTRING(A.DTS_INSERT, 9, 2) + ':'
	+ SUBSTRING(A.DTS_INSERT, 11, 2) + ':'
	+ SUBSTRING(A.DTS_INSERT, 13, 2) + ' ('
	+ C.NM_KOR  + ')'	AS DT_GR
FROM PU_RCVL	AS A WITH(NOLOCK)
JOIN PU_RCVH	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_RCV = B.NO_RCV
JOIN MA_EMP		AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.NO_EMP = C.NO_EMP
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CompanyCode + @"'
	AND A.NO_PO = '" + FileNumber + @"'
GROUP BY A.NO_RCV, B.DT_REQ, A.DTS_INSERT, C.NM_KOR
ORDER BY A.DTS_INSERT DESC";
				
				DataTable dtRecord = DBMgr.GetDataTable(query);
				cbo입고일자.DataBind(dtRecord, true);

				// 매입처 => 발주번호로 조회, 1개 자동선택
				query = "SELECT CD_PARTNER, LN_PARTNER FROM V_CZ_PU_POH WITH(NOLOCK) WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_PO = '" + FileNumber + "'";
				DataTable dtSupplier = DBMgr.GetDataTable(query);

				if (dtSupplier.Rows.Count == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}

				cbo매입처.DataBind(dtSupplier, false);
			}
			else
			{
				// ********** 일반품
				IsStock = false;

				// 입고이력 => 프로젝트로 조회
				query = @"
SELECT
	  A.NO_RCV			AS NO_IO
	, SUBSTRING(B.DT_REQ, 1, 4) + '/'
	+ SUBSTRING(B.DT_REQ, 5, 2) + '/'
	+ SUBSTRING(B.DT_REQ, 7, 2) + ' '
	+ SUBSTRING(A.DTS_INSERT, 9, 2) + ':'
	+ SUBSTRING(A.DTS_INSERT, 11, 2) + ':'
	+ SUBSTRING(A.DTS_INSERT, 13, 2) + ' ('
	+ C.NM_KOR  + ')'	AS DT_GR
FROM PU_RCVL	AS A WITH(NOLOCK)
JOIN PU_RCVH	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_RCV = B.NO_RCV
JOIN MA_EMP		AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.NO_EMP = C.NO_EMP
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CompanyCode + @"'
	AND A.CD_PJT = '" + FileNumber + @"'
GROUP BY A.NO_RCV, B.DT_REQ, A.DTS_INSERT, C.NM_KOR
ORDER BY A.DTS_INSERT DESC";

				DataTable dtRecord = DBMgr.GetDataTable(query);
				cbo입고일자.DataBind(dtRecord, true);

				// 매입처 => 수주번호로 조회, 선택 안된채로 나둠
				query = "SELECT CD_PARTNER, LN_PARTNER FROM V_CZ_PU_POH WITH(NOLOCK) WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_SO = '" + FileNumber + "'";
				DataTable dtSupplier = DBMgr.GetDataTable(query);
				
				if (dtSupplier.Rows.Count == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}

				cbo매입처.DataBind(dtSupplier, true);
			}

			// 라인 조회
			Search();
		}

		private void Search()
		{
			// 일반품과 재고품일때 프로시져가 다름
			string proc = "PS_CZ_PU_RCV_REG_L";

			if (IsStock)
				proc = proc + "_ST";

			// 조회
			grd라인.Binding = DBMgr.GetDataTable(proc, true, false, CompanyCode, FileNumber, cbo입고일자.GetValue(), cbo매입처.GetValue());
			SetGridStyle();
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (!MsgAndSave(PageActionMode.Search))
				return;

			Clear();
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			// ********** 유효성 검사
			DataTable dtChanged = grd라인.GetChanges();

			// 체크 안된 얘들은 행 삭제함, 커밋까지 해줘야 XML에 표시 안됨
			foreach (DataRow row in dtChanged.Select("CHK = 'N'"))
			{
				row.Delete();
				row.AcceptChanges();
			}
	
			// 검사 시작
			if (dtChanged == null || dtChanged.Rows.Count == 0)
			{
				ShowMessage(공통메세지.선택된자료가없습니다);
				return;
			}

			// 체크가 되어 있는데 입고, 양품 둘다 0인 항목이 있는지 체크
			if (dtChanged.Select("CHK = 'Y' AND QT_WORK + QT_WORK2 = 0").Length > 0)
			{
				ShowMessage("입고수량은 0보다 커야합니다!");
				return;
			}

			// 입고수량이 있는데 창고 선택되어 있는지 체크
			if (dtChanged.Select("CHK = 'Y' AND QT_WORK > 0").Length > 0)
			{
				if (ctx입고창고.CodeValue == "")
				{
					ShowMessage("창고가 선택되지 않았습니다!");
					return;
				}

				if (!IsStock && ctx로케이션.Enabled && ctx로케이션.CodeValue == "")
				{
					ShowMessage("로케이션이 선택되지 않았습니다!");
					return;
				}
			}

			// 미입고 수량 체크
			if (dtChanged.Select("CHK = 'Y' AND QT_WORK > QT_NGR").Length > 0)
			{
				ShowMessage("입고수량이 미입고수량을 초과하였습니다!");
				return;
			}

			// 양품 수량 체크
			if (dtChanged.Select("CHK = 'Y' AND QT_WORK2 > ISNULL(QT_IO, 0) - ISNULL(QT_MSL, 0)").Length > 0)
			{
				ShowMessage("양품수량이 사양상이수량을 초과하였습니다!");
				return;
			}

			// 무게 빠진 항목이 있는지 검사
			if (CompanyCode == "K100" && dtChanged.Select("NO_DRAWING <> '' AND WEIGHT = 0").Length > 0)
			{
				if (ShowMessage("무게 정보가 없는 항목이 있습니다. 진행 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;
			}

			// 그리드 검사
			if (!base.Verify())
				return;

			// ********** 저장
			MsgControl.ShowMsg("잠시만 기다려주세요.");

			try
			{
				// ********** 입고 OR 삭제
				DBMgr dbmGr = new DBMgr();
				dbmGr.DebugMode = DebugMode.Print;
				dbmGr.Procedure = "PX_CZ_PU_RCV_REG_R2";
				dbmGr.AddParameter("@CD_SL"		 , ctx입고창고.CodeValue);
				dbmGr.AddParameter("@CD_LOCATION", ctx로케이션.CodeValue);
				dbmGr.AddParameter("@WEIGHT"	 , cur무게.DecimalValue);
				dbmGr.AddParameter("@DC_RMK"	 , tbx비고.Text);
				dbmGr.AddParameter("@DT_IO"		 , chk조정일자.Checked ? dtp조정일자.Text : "");
				dbmGr.AddParameter("@XML_L"		 , GetTo.Xml(dtChanged, "", "CHK", "NO_PO", "NO_LINE", "CD_ITEM", "QT_WORK", "NO_IO", "NO_IOLINE"));
				DataTable dtGr = dbmGr.GetDataTable();

				// ***** 쪽지보내기
				if (dtGr.Rows.Count > 0)
				{
					if (!IsStock)
					{
						#region 일반품

						DataSet ds = DBMgr.GetDataSet("SP_CZ_PU_RCV_REG_ALRAM", new object[] { CompanyCode, FileNumber });
						DataTable dtP = ds.Tables[0];
						DataTable dtC = ds.Tables[1];

						if (dtP.Rows.Count > 0)
						{
							string item = @"
-. {0} / {1:####.##} / {2} / {3:#,##0}({4:#,##0})";

							string item_p = "";
							string item_c = "";

							foreach (DataRow row in dtP.Rows)
							{
								item_p += string.Format(item, row["NO_FILE"], row["NO_DSP_P"], row["CD_ITEM_PARTNER_P"], row["QT_PR"], row["QT_P"]);
							}

							foreach (DataRow row in dtC.Rows)
							{
								item_c += string.Format(item, row["NO_FILE"], row["NO_DSP"], row["CD_ITEM_PARTNER"], row["QT_GR"], row["QT"]);
							}

							string body = "";

							if (Global.MainFrame.LoginInfo.Language == "KR")
							{
								body = @"** BOM 입고 알람

자품목 입고 리스트{0}

모품목 생산가능 리스트{1}


※ 본 쪽지는 발신 전용 쪽지입니다.
";
							}
							else
							{
								body = @"** Notice for BOM Assembly

List of received components{0}

List of parent item that can be assembled{1}


※ You can't reply back to this message.
";
							}

							string contents = string.Format(body, item_c, item_p);

							// 워크플로우 담당자 가져오기
							string[] users = Util.GetDB_WORKFLOW_EMP(FileNumber);

							//Messenger.SendMSG(new string[] { "S-359", "S-343", "S-391", "S-347" }, contents);
							Messenger.SendMSG(users, contents);
						}

						#endregion
					}
					else if (IsStock)
					{
						//Messenger.SendStockMsg(GetTo.Xml(dtGr, "", "CD_ITEM"), "ERP_GR");

						DataTable dt = 디비.결과("PI_CZ_DX_AUTO_STK_BOOK", dtGr.Json("CD_ITEM"), "ERP_GR");
						메신져.재고예약쪽지(dt);
					}
				}

				// ********** 창고이동
				string slCode = IsStock ? "SL02" : "SL01";

				// 저장
				DBMgr dbmMsl = new DBMgr();
				dbmMsl.DebugMode = DebugMode.Print;
				dbmMsl.Procedure = "PX_CZ_PU_RCV_REG_MSL";
				dbmMsl.AddParameter("@CD_SL"	  , slCode);
				//dbmMsl.AddParameter("@CD_LOCATION", cbo로케이션.SelectedValue);
				dbmMsl.AddParameter("@DC_RMK"	  , tbx비고.Text);
				dbmMsl.AddParameter("@DT_IO"	  , chk조정일자.Checked ? dtp조정일자.Text : "");
				dbmMsl.AddParameter("@XML_L"	  , GetTo.Xml(dtChanged, "", "NO_PO", "NO_LINE", "CD_ITEM", "QT_WORK2", "NO_IO", "NO_IOLINE", "CD_SL"));
				DataTable dtMsl = dbmMsl.GetDataTable();

				// ***** 쪽지보내기
				if (dtMsl.Rows.Count > 0)
				{
					if (IsStock)
					{
						//Messenger.SendStockMsg(GetTo.Xml(dtMsl, "", "CD_ITEM"), "ERP_MSL");

						DataTable dt = 디비.결과("PI_CZ_DX_AUTO_STK_BOOK", dtMsl.Json("CD_ITEM"), "ERP_MSL");
						메신져.재고예약쪽지(dt);
					}
				}
				
				// ********** 자동협조전 체크 (일단 일반창고일때만 하자, 창고이동 하는건 나중에 하자)
				if (!IsStock && ctx입고창고.값() == "SL01")
				{
					TSQL.실행("PI_CZ_DX_AUTO_GIR", 상수.회사코드, dtChanged.첫행("NO_FILE"));
				}

				// 마무리
				grd라인.AcceptChanges();
				OnToolBarSearchButtonClicked(null, null);
				MsgControl.CloseMsg();
				ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}
		}

		#endregion
	
		#region ==================================================================================================== 기타

		private void SetGridStyle()
		{
			// 미입고 눈에띄게 표시
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (GetTo.Decimal(grd라인[i, "QT_NGR"]) > 0)
					grd라인.SetCellStyle(i, grd라인.Cols["QT_NGR"].Index, "NGR");
				else
					grd라인.SetCellStyle(i, grd라인.Cols["QT_NGR"].Index, "");
			}

			// 셀병합
			string[] mergeCols = { "NO_DSP", "NM_ITEM_PARTNER", "CD_ITEM", "SN_PARTNER", "CD_UNIT_MM", "QT_PO" };

			if (grd라인.HasNormalRow)
			{
				grd라인.Clear(ClearFlags.UserData, grd라인.Rows.Fixed, 1, grd라인.Rows.Count - 1, grd라인.Cols.Count - 1);
				grd라인.Merge("BASED", "NO_DSP", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_ITEM", "SN_PARTNER", "CD_UNIT_MM", "QT_PO", "QT_GR", "QT_NGR", "QT_WORK");
			}
		}

		#endregion
	}
}
