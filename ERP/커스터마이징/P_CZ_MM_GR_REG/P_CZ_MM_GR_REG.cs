using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using DX;
using System.Linq;
using DzHelpFormLib;
using Dintec;

namespace cz
{
	public partial class P_CZ_MM_GR_REG : PageBase
	{
		FreeBinding Header = new FreeBinding();
		

		string 파일번호 => tbx파일번호.Text;

		string 파일구분 => tbx파일번호.Text.왼쪽(2).포함("SB", "NS") ? "선용품" : "기자재";

		string 발주번호 => cbo매입처.값();

		string 입고구분 => rdo일반품.선택().글();

		string 스캔모드 => rdo품목체크.선택().글();

		

		#region ==================================================================================================== 생성자

		public P_CZ_MM_GR_REG()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== 초기화

		protected override void InitLoad()
		{
			this.페이지초기화();
			//tbx폐기번호검색.엔터검색();

			//// ********** 편집불가 패널
			//pnl폐기번호.사용(false);
			//pnl등록일자.사용(false);
			//pnl담당자.사용(false);
			//pnl결재상태.사용(false);
			//pnl수불번호.사용(false);
			//pnl전표번호.사용(false);

			////// ********** 콤보박스
			////DataSet ds = CODE.코드관리("PU_C000021");

			//cbo구분.바인딩(CODE.코드관리("PU_C000021").선택("CD_FLAG1 = 'SCRAP'", "NAME"), true);
			////cbo지급조건.바인딩(ds.Tables[1], true);
			////cbo선적조건.바인딩(ds.Tables[2], true);
			///

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			tbx파일번호.Focus();

		}

		#endregion

		#region ==================================================================================================== 그리드 = GRID

		private void InitGrid()
		{
			MainGrids = this.컨트롤<FlexGrid>();

			// ********** 목록
			

			grd라인.세팅시작(2);
			
			grd라인.컬럼세팅("CHK"				, "S"			, 30	, 포맷.체크);
			grd라인.컬럼세팅("NO_PO"				, "발주번호"		, 110	, 정렬.가운데);
			grd라인.컬럼세팅("NO_DSP"				, "순번"			, 45	, "####.##", 정렬.가운데);
			grd라인.컬럼세팅("NM_SUBJECT"			, "주제"			, 200	, false);
			grd라인.컬럼세팅("CD_ITEM_PARTNER"	, "품목코드"		, 140);
			grd라인.컬럼세팅("NM_ITEM_PARTNER"	, "품목명"		, 230);
			grd라인.컬럼세팅("CD_ITEM"			, "재고코드"		, 100	, 정렬.가운데);
			grd라인.컬럼세팅("CD_PARTNER"			, "매입처코드"	, 80	, false);
			grd라인.컬럼세팅("SN_PARTNER"			, "매입처"		, 150);
			grd라인.컬럼세팅("CD_UNIT"			, "단위"			, 45	, 정렬.가운데);
			grd라인.컬럼세팅("QT_PO"				, "수량"			, "발주"			, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_GR"				, "수량"			, "입고"			, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_NGR"				, "수량"			, "미입고"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_WORK"			, "수량"			, "입력"			, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_IO"				, "수량"			, "상세"			, 50	, 포맷.수량);
			grd라인.컬럼세팅("DT_IO"				, "입고일자"		, 80	, 포맷.날짜);
			grd라인.컬럼세팅("DT_INSERT"			, "등록일자"		, 80	, 포맷.날짜);
			grd라인.컬럼세팅("CD_SL"				, "창고"			, 130);
			grd라인.컬럼세팅("CD_LOCATION"		, "로케이션"		, 70);
			grd라인.컬럼세팅("NM_EMP"				, "담당자"		, 70	, 정렬.가운데);
			grd라인.컬럼세팅("NO_IO"				, "입고번호"		, 100	, 정렬.가운데);
			grd라인.컬럼세팅("NO_IOLINE"			, "입고항번"		, false);
			grd라인.컬럼세팅("WEIGHT"				, "무게"			, 60	, 포맷.수량);		// EDIT
			grd라인.컬럼세팅("NO_LINE"			, "발주라인"		, false);

			grd라인.데이터맵("CD_UNIT", 코드.단위());
			grd라인.데이터맵("CD_SL", 코드.창고());
			//grd라인.기본키("CD_COMPANY", "NO_SCRAP", "SEQ");
			grd라인.세팅종료("22.06.30.02", true);

			grd라인.에디트컬럼("CHK", "QT_WORK", "WEIGHT");
			grd라인.합계제외컬럼("NO_DSP");

			//grd라인.SetDummyColumn("CHK");


			//CellStyle style = grd라인.Styles.Add("NGR");
			//style.Font = new Font(grd라인.Font, FontStyle.Bold);
			//style.ForeColor = Color.Red;
		}

		

		#endregion


		#region ==================================================================================================== 이벤트 = EVENT

		private void InitEvent()
		{
			cbo매입처.SelectionChangeCommitted += Cbo매입처_SelectionChangeCommitted;


			tbx파일번호.KeyDown += Tbx파일번호_KeyDown;
			tbx스캔.KeyDown += Tbx스캔_KeyDown;
			rdo품목체크.CheckedChanged += Rdo품목체크_CheckedChanged;
			rdo품목해제.CheckedChanged += Rdo품목체크_CheckedChanged;

			grd라인.AfterEdit += Grd라인_AfterEdit;

			ctx입고창고.QueryBefore += Ctx입고창고_QueryBefore;
			
		}

		private void Ctx입고창고_QueryBefore(object sender, BpQueryArgs e)
		{
			string sl;

			if (입고구분 == "일반품")
				sl = "'SL01', 'SL998', 'SL999', 'VL01', 'VL02'";			
			else
				sl = "'SL02', 'SL998', 'SL999', 'VL01', 'HO01'";

			e.HelpParam.P00_CHILD_MODE = "입고창고";

			e.HelpParam.P61_CODE1 = @"
	  CD_SL AS CODE
	, NM_SL AS NAME";

			e.HelpParam.P62_CODE2 = @"
MA_SL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_SL IN (" + sl + @")
ORDER BY CD_SL";
		}

		private void Rdo품목체크_CheckedChanged(object sender, EventArgs e)
		{			
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (스캔모드 == "품목체크")
					grd라인[i, "CHK"] = "N";
				else
					grd라인[i, "CHK"] = "Y";

				Grd라인_AfterEdit(grd라인, grd라인.이벤트<RowColEventArgs>(i, "CHK"));
			}
		}

		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string 컬럼이름 = grd라인.컬럼이름(e);

			if (컬럼이름 == "CHK")
			{			
				if (grd라인[e.Row, 컬럼이름].문자() == "N")
					grd라인[e.Row, "QT_WORK"] = 0;
				else if (파일구분 == "선용품")
					grd라인[e.Row, "QT_WORK"] = grd라인[e.Row, "QT_NGR"];
			}


			그리드스타일(e.Row);
		}

		private void Tbx파일번호_KeyDown(object sender, KeyEventArgs e)
		{
			//tbx비고.Text += e.KeyCode + ",";

			if (tbx비고.Text.Length == 10)
			{
				tbx비고.Text = "";
			}


			if (e.KeyData == Keys.Enter)
			{
				OnToolBarSearchButtonClicked(null, null);
				tbx스캔.Focus();
				tbx비고.Text = "";
			}
		}

		private void Tbx스캔_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				// 찾기 시작
				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					if (grd라인[i, "ENCODED"].문자() == tbx스캔.Text)
					{
						grd라인.Row = i;
						grd라인[i, "CHK"] = "N";
						Grd라인_AfterEdit(grd라인, grd라인.이벤트<RowColEventArgs>(i, "CHK"));
						tbx스캔.Text = "";
						tbx스캔.Focus();
						return;
					}
				}

				// 찾기 실패
				유틸.경고("아이템이 없습니다.");
				tbx스캔.Text = "";
				tbx스캔.Focus();
			}
		}

		#endregion

		#region ==================================================================================================== 조회 = SEARCH

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				// 발주번호로 들어오는 경우 별도 저장해놓고 파일번호로 바꿔주고 해당 발주(매입처) 바인딩함 (주로 QR코드 스캔하는 얘들)
				string 발주번호 = "";
				
				if (tbx파일번호.Text.Length == 13 && tbx파일번호.Text.발생("-"))
				{
					발주번호 = tbx파일번호.Text;
					tbx파일번호.Text = tbx파일번호.Text.분할("-")[0];
				}

				// ********** 헤드 바인딩
				DataTable dtH = 디비.결과("PS_CZ_MM_GR_REG_H", 상수.회사코드, 파일번호);
				Header.바인딩(dtH, lay헤드);

				// 선용은 품목해제 모드로 변경
				if (파일번호.왼쪽(2).포함("SB", "NS"))
					rdo품목해제.Checked = true;

				// ********** 매입처 바인딩
				DataTable dtV = dtH.데이터테이블(false, "NO_PO", "NM_VENDOR");
				dtV.Columns["NO_PO"].ColumnName = "CODE";
				dtV.Columns["NM_VENDOR"].ColumnName = "NAME";
				cbo매입처.바인딩(dtV, true);

				if (발주번호 != "")			  cbo매입처.값(발주번호);
				else if (dtH.Rows.Count == 1) cbo매입처.값(dtH.첫행("NO_PO"));

				// ********** 창고 바인딩
				if (파일구분 == "선용품")
				{
					ctx입고창고.값("SL01");
					ctx입고창고.글("일반 로케이션");
					ctx로케이션.값("A1-1-1");
					ctx로케이션.글("A1-1-1");
				}
				//SL01

				// ********** 입고이력 바인딩
				//				query = @"
				//SELECT
				//	A.NO_RCV			AS NO_IO
				//	, SUBSTRING(B.DT_REQ, 1, 4) + '/'
				//	+ SUBSTRING(B.DT_REQ, 5, 2) + '/'
				//	+ SUBSTRING(B.DT_REQ, 7, 2) + ' '
				//	+ SUBSTRING(A.DTS_INSERT, 9, 2) + ':'
				//	+ SUBSTRING(A.DTS_INSERT, 11, 2) + ':'
				//	+ SUBSTRING(A.DTS_INSERT, 13, 2) + ' ('
				//	+ C.NM_KOR  + ')'	AS DT_GR
				//FROM PU_RCVL	AS A WITH(NOLOCK)
				//JOIN PU_RCVH	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_RCV = B.NO_RCV
				//JOIN MA_EMP		AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.NO_EMP = C.NO_EMP
				//WHERE 1 = 1
				//	AND A.CD_COMPANY = '" + CompanyCode + @"'
				//	AND A.NO_PO = '" + FileNumber + @"'
				//GROUP BY A.NO_RCV, B.DT_REQ, A.DTS_INSERT, C.NM_KOR
				//ORDER BY A.DTS_INSERT DESC";

				// ********** 라인 바인딩
				Cbo매입처_SelectionChangeCommitted(null, null);
				
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}

			this.ActiveControl = tbx비고;
			//tbx비고.Select();
		}

		private void Cbo매입처_SelectionChangeCommitted(object sender, EventArgs e)
		{			
			DataTable dt = 디비.결과("PS_CZ_MM_GR_REG_L", 상수.회사코드, 파일번호, 발주번호);
			grd라인.바인딩(dt);
			Rdo품목체크_CheckedChanged(null, null);
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE

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
			if (dtChanged.Select("CHK = 'Y' AND QT_WORK = 0").Length > 0)
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

				if (입고구분 == "일반품" && ctx로케이션.Enabled && ctx로케이션.CodeValue == "")
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
			//if (dtChanged.Select("CHK = 'Y' AND QT_WORK2 > ISNULL(QT_IO, 0) - ISNULL(QT_MSL, 0)").Length > 0)
			//{
			//	ShowMessage("양품수량이 사양상이수량을 초과하였습니다!");
			//	return;
			//}

			// 무게 빠진 항목이 있는지 검사
			//if (상수.회사코드 == "K100" && dtChanged.Select("NO_DRAWING <> '' AND WEIGHT = 0").Length > 0)
			//{
			//	if (ShowMessage("무게 정보가 없는 항목이 있습니다. 진행 하시겠습니까?", "QY2") != DialogResult.Yes)
			//		return;
			//}

			// 그리드 검사
			if (!base.Verify())
				return;

			// ********** 저장
			MsgControl.ShowMsg("잠시만 기다려주세요.");

			try
			{
				// ********** 입고 OR 삭제				
				디비 db = new 디비("PX_CZ_PU_RCV_REG_R2");
				db.변수.추가("@CD_SL"			, ctx입고창고.CodeValue);
				db.변수.추가("@CD_LOCATION"	, ctx로케이션.CodeValue);
				db.변수.추가("@WEIGHT"		, cur무게.DecimalValue);
				db.변수.추가("@DC_RMK"		, tbx비고.Text);
				db.변수.추가("@DT_IO"			, chk조정일자.Checked ? dtp조정일자.Text : "");
				db.변수.추가("@XML_L"			, GetTo.Xml(dtChanged, "", "CHK", "NO_PO", "NO_LINE", "CD_ITEM", "QT_WORK", "NO_IO", "NO_IOLINE"));
				DataTable dtGr = db.결과();

				// ***** 쪽지보내기
				if (dtGr.Rows.Count > 0)
				{
					if (입고구분 == "일반품")
					{
						#region 일반품

						DataSet ds = DBMgr.GetDataSet("SP_CZ_PU_RCV_REG_ALRAM", new object[] { 상수.회사코드, 파일번호 });
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
							string[] users = Util.GetDB_WORKFLOW_EMP(파일번호);

							//Messenger.SendMSG(new string[] { "S-359", "S-343", "S-391", "S-347" }, contents);
							Messenger.SendMSG(users, contents);
						}

						#endregion
					}
					else if (입고구분 == "재고품")
					{
						//Messenger.SendStockMsg(GetTo.Xml(dtGr, "", "CD_ITEM"), "ERP_GR");

						DataTable dt = 디비.결과("PI_CZ_DX_AUTO_STK_BOOK", dtGr.Json("CD_ITEM"), "ERP_GR");
						메신져.재고예약쪽지(dt);
					}
				}

				// ********** 창고이동
				string slCode = 입고구분 == "재고품" ? "SL02" : "SL01";

				// 저장
				DBMgr dbmMsl = new DBMgr();
				dbmMsl.DebugMode = DebugMode.Print;
				dbmMsl.Procedure = "PX_CZ_PU_RCV_REG_MSL";
				dbmMsl.AddParameter("@CD_SL"	  , slCode);
				//dbmMsl.AddParameter("@CD_LOCATION", cbo로케이션.SelectedValue);
				dbmMsl.AddParameter("@DC_RMK"	  , tbx비고.Text);
				dbmMsl.AddParameter("@DT_IO"	  , chk조정일자.Checked ? dtp조정일자.Text : "");
				dbmMsl.AddParameter("@XML_L"	  , GetTo.Xml(dtChanged, "", "NO_PO", "NO_LINE", "CD_ITEM", "NO_IO", "NO_IOLINE", "CD_SL"));
				DataTable dtMsl = dbmMsl.GetDataTable();

				// ***** 쪽지보내기
				if (dtMsl.Rows.Count > 0)
				{
					if (입고구분 == "재고품")
					{
						//Messenger.SendStockMsg(GetTo.Xml(dtMsl, "", "CD_ITEM"), "ERP_MSL");

						DataTable dt = 디비.결과("PI_CZ_DX_AUTO_STK_BOOK", dtMsl.Json("CD_ITEM"), "ERP_MSL");
						메신져.재고예약쪽지(dt);
					}
				}
				
				// ********** 자동협조전 체크 (일단 일반창고일때만 하자, 창고이동 하는건 나중에 하자)
				if (입고구분 == "일반품" && ctx입고창고.값() == "SL01")
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

		private void 그리드스타일()
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{

			}
		}

		private void 그리드스타일(int 행)
		{
			// ********** 스캔모드
			string 색상 = "";
			bool 굵게 = false;

			if (스캔모드 == "품목체크")
			{
				if (grd라인[행, "CHK"].문자() == "Y")
				{
					색상 = "BLUE";
					굵게 = true;
				}
			}
			else
			{
				if (grd라인[행, "CHK"].문자() == "N")
				{
					색상 = "RED";
					굵게 = true;
				}
			}

			grd라인.셀글자색(행, "NO_PO", 색상, 굵게);
			grd라인.셀글자색(행, "NO_DSP", 색상, 굵게);
			grd라인.셀글자색(행, "CD_ITEM_PARTNER", 색상, 굵게);
			grd라인.셀글자색(행, "NM_ITEM_PARTNER", 색상, 굵게);

			// ********** 미입고 눈에띄게 표시
			if (grd라인[행, "QT_NGR"].정수() > 0)
				grd라인.셀글자색(행, "QT_NGR", "RED", true);
			else
				grd라인.셀글자색(행, "QT_NGR", "", false);

			//for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			//{
			//	if (GetTo.Decimal(grd라인[i, "QT_NGR"]) > 0)
			//		grd라인.SetCellStyle(i, grd라인.Cols["QT_NGR"].Index, "NGR");
			//	else
			//		grd라인.SetCellStyle(i, grd라인.Cols["QT_NGR"].Index, "");
			//}

			//// 셀병합
			//string[] mergeCols = { "NO_DSP", "NM_ITEM_PARTNER", "CD_ITEM", "SN_PARTNER", "CD_UNIT_MM", "QT_PO" };

			//if (grd라인.HasNormalRow)
			//{
			//	grd라인.Clear(ClearFlags.UserData, grd라인.Rows.Fixed, 1, grd라인.Rows.Count - 1, grd라인.Cols.Count - 1);
			//	grd라인.Merge("MERGED", "NO_DSP", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_ITEM", "SN_PARTNER", "CD_UNIT_MM", "QT_PO", "QT_GR", "QT_NGR", "QT_WORK");
			//}
		}
	
	}
}
