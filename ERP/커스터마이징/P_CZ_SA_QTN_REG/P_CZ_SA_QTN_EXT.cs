using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Linq;
using DX;

namespace cz
{
	public partial class P_CZ_SA_QTN_EXT : PageBase
	{
		#region ===================================================================================================== Property

		private string CompanyCode
		{
			get
			{
				if (Parent == null)
					return LoginInfo.CompanyCode;
				else
					return ((P_CZ_SA_QTN_REG)Parent.GetContainerControl()).회사코드;
			}
		}

		private string FileNumber
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).파일번호;
			}
		}

		private int HistoryNumber
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).차수;
			}
		}

		public string PartnerCode
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).매출처코드;
			}

		}

		public string ImoNumber
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).Imo번호;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_QTN_EXT()
		{
			//StartUp.Certify(this);
			InitializeComponent();
			//EXT.SetDefault(this);
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			// 재고대상
			ctx재고대상.CodeValue = LoginInfo.CompanyCode;
			ctx재고대상.CodeName = LoginInfo.CompanyName;

			// 콤보박스 관련
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			// 단가기준
			dt.Rows.Add("CR_EXT", "CONTRACT", "단가표");
			dt.Rows.Add("CR_EXT", "RECENT"	, "최근견적");
			dt.Rows.Add("CR_EXT", "PROFIT"	, "최고이윤율");
			dt.Rows.Add("CR_EXT", "PRICE"	, "최고단가");
			
			cbo단가기준1.DataBind(dt.Select("TYPE = 'CR_EXT'").CopyToDataTable(), false);
			cbo단가기준1.SelectedIndex = 0;

			dt.Rows.Add("CR_PEROID", 0 , "전체");
			dt.Rows.Add("CR_PEROID", 3 , "3개월");
			dt.Rows.Add("CR_PEROID", 6 , "6개월");
			dt.Rows.Add("CR_PEROID", 12, "12개월");
			dt.Rows.Add("CR_PEROID", 24, "24개월");

			cbo단가기준2.DataBind(dt.Select("TYPE = 'CR_PEROID'").CopyToDataTable(), false);
			cbo단가기준2.SelectedValue = 12;

			// 검색
			dt.Rows.Add("SEARCH", "C", "단가표");
			dt.Rows.Add("SEARCH", "R", "실적");

			cbo검색.DataSource = dt.Select("TYPE = 'SEARCH'").CopyToDataTable();
			cbo검색.SelectedIndex = 0;
		
			// 파일구분 동적 추가
			string query = @"
SELECT
	CD_SYSDEF
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND CD_FLAG1 = 'WF'
	AND ISNULL(CD_FLAG2, '') != 'CLAIM'
	AND CD_SYSDEF != 'PT'
	AND CD_SYSDEF != 'TE'

UNION ALL

SELECT 
	'ZB'

ORDER BY CD_SYSDEF";

			DataTable dtPrefix = SQL.GetDataTable(query);

			for (int i = 0; i < dtPrefix.Rows.Count; i++)
			{				
				CheckBoxExt chk = new CheckBoxExt()
				{
					Checked = dtPrefix.Rows[i]["CD_SYSDEF"].ToString().In("DB", "DS", "FB", "NB")
				,	Text = dtPrefix.Rows[i]["CD_SYSDEF"].ToString()
				,	Width = 41
				,	Left = 94 + (i * 51)
				,	Top = 0
				};

				pnl파일구분.Controls.Add(chk);
			}

			// 부모-자식 그리드
			grd헤드.DetailGrids = new FlexGrid[] { grd재고, grd실적 };

			//fgd장비.DetailGrids = new FlexGrid[] { grd재고검색 };

			if (!Certify.IsAdminEquip())
			{
				spc헤드.Panel2Collapsed = true;
				spc헤드.Panel2.Hide();
			}

			// 포커스용
			tbxFocus.Left = -500;

			InitGrid();
			InitEvent();

			//Btn확장_Click(null, null);
			spc헤드.Panel2Collapsed = true;
			spc헤드.Panel2.Hide();
		}

		protected override void InitPaint()
		{

		}

		public void Clear()
		{
			// 검색원그리드
			cbo단가기준1.Clear(true);
			cbo단가기준2.Clear(true);
			ctx재고대상.Clear(true);
			rdo단가대상1.Checked = true;
			cbo매입처.Clear(false);
			cbm분류.Clear(false);
			rdo복사옵션1.Checked = true;
			cbo가격조건.Clear(false);
			cbo검색.Clear(true);
			tbx검색.Text = "";
			rdo확정상태N.Checked = true;

			// 그리드
			grd헤드.Clear(false);
			grd재고.Clear(false);
			grd재고검색.Clear(false);
			grd실적.Clear(false);
			grd실적검색.Clear(false);
		}

		public void 사용(bool enabled)
		{
			pnl버튼.Editable(enabled);
			one검색.Editable(enabled);
			grd헤드.AllowEditing = enabled;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			DataTable dtExt = new DataTable();
			dtExt.Columns.Add("CODE");
			dtExt.Columns.Add("NAME");
			dtExt.Rows.Add("Y", DD("확정"));
			dtExt.Rows.Add("N", "");

			// ********** 헤드
			grd헤드.BeginSetting(2, 1, false);

			grd헤드.SetCol("CHK"					, "S"		, 30	, CheckTypeEnum.Y_N);
			grd헤드.SetCol("YN_EXT"				, "확정"		, 40	, TextAlignEnum.CenterCenter);
			grd헤드.SetCol("NO_FILE"				, "파일번호"	, false);
			grd헤드.SetCol("NO_LINE"				, "항번"		, false);
			grd헤드.SetCol("NO_DSP"				, "순번"		, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd헤드.SetCol("NM_SUBJECT"			, "주제"		, false);
			grd헤드.SetCol("CD_ITEM_PARTNER"		, "품목코드"	, 100);
			grd헤드.SetCol("NM_ITEM_PARTNER"		, "품목명"	, 230);
			grd헤드.SetCol("CD_ITEM_PARTNER_EXT", "실적코드"	, 100);
			grd헤드.SetCol("NM_ITEM_PARTNER_EXT", "실적명"	, 250);

			InitGrid_공통_재고정보(grd헤드);
			
			grd헤드.SetCol("UNIT"				, "단위(INQ)"	, false);
			grd헤드.SetCol("UNIT_EXT"			, "단위"			, 60	, TextAlignEnum.CenterCenter);
			grd헤드.SetCol("QT"					, "수량"			, 50	, typeof(decimal), FormatTpType.QUANTITY);
			
			InitGrid_공통_재고수량(grd헤드);
			InitGrid_공통_매입출(grd헤드);
			InitGrid_공통_단가표(grd헤드);

			// 비고 (참고용)
			grd헤드.SetCol("DC_RMK1"				, "비고1"		, false);
			grd헤드.SetCol("DC_RMK2"				, "비고2"		, false);
			grd헤드.SetCol("DC_RMK"				, "비고"			, false);
			// 기타
			grd헤드.SetCol("NO_FILE_EXT"			, "실적파일번호"	, 80);
			grd헤드.SetCol("NO_LINE_EXT"			, "실적항번"		, false);
			grd헤드.SetCol("YN_SO"				, "수주"			, 40	, TextAlignEnum.CenterCenter);

			grd헤드.SetDataMap("YN_EXT"		, dtExt					, "CODE", "NAME");
			grd헤드.SetDataMap("CLS_S"		, CODE.ClsS()			, "CODE", "NAME");
			grd헤드.SetDataMap("UNIT_EXT"	, CODE.Unit()			, "CODE", "NAME");
			grd헤드.SetDataMap("FG_UM"		, CODE.Key("SA_B000021"), "CODE", "NAME");

			grd헤드.SetDefault("21.02.19.01", SumPositionEnum.None);
			grd헤드.SetAlternateRow();
			grd헤드.SetMalgunGothic();
			grd헤드.SetEditColumn("CHK", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "DC_OFFER", "DC_STOCK", "UNIT_EXT", "LT");
			grd헤드.Styles.Add("ACTIVE").ForeColor = Color.Blue;
			grd헤드.LoadUserCache("P_CZ_SA_QTN_EXT");

			// 다양한 검색방법을 위해 바인딩 한번 기본으로 해준다
			DataTable dtCol = new DataTable();
			for (int i = 0; i < grd헤드.Cols.Count; i++)
			{
				string colName = grd헤드.Cols[i].Name;

				if (colName.Length > 2 && colName.Left(2).In("CD", "NM"))
					dtCol.Columns.Add(colName, typeof(string), "''");
				else
					dtCol.Columns.Add(colName);
			}
			grd헤드.Binding = dtCol;

			// ********** 라인
			InitGrid_재고(grd재고);
			InitGrid_재고(grd재고검색);
			InitGrid_실적(grd실적);
			InitGrid_실적(grd실적검색);

			// ********** 장비
			grd장비.BeginSetting(2, 1, false);

			grd장비.SetCol("CLS_S"			, "소분류"	, 250);
			grd장비.SetCol("DC_RMK1"			, "코드1"	, 100);
			grd장비.SetCol("DC_RMK2"			, "코드2"	, 100);
			grd장비.SetCol("DC_RMK3"			, "코드3"	, 100);
			grd장비.SetCol("DC_RMK4"			, "코드4"	, 100);
			grd장비.SetCol("DC_RMK5"			, "코드5"	, 100);
			grd장비.SetCol("DC_RMK6"			, "코드6"	, 100);
			grd장비.SetCol("DC_RMK7"			, "코드7"	, 100);
			grd장비.SetCol("DC_RMK8"			, "코드8"	, 100);
			grd장비.SetCol("DC_RMK9"			, "코드9"	, 100);
			grd장비.SetCol("DC_RMK10"		, "코드10"	, 100);
			grd장비.SetCol("DC_RMK11"		, "코드11"	, 100);
			grd장비.SetCol("DC_RMK12"		, "코드12"	, 100);
			grd장비.SetCol("DC_RMK13"		, "코드13"	, 100);
			grd장비.SetCol("DC_RMK14"		, "코드14"	, 100);
			grd장비.SetCol("DC_RMK15"		, "코드15"	, 100);
			grd장비.SetCol("DC_RMK"			, "비고"		, 200);

			for (int i = 1; i < grd장비.Cols.Count; i++)
				grd장비[0, i] = "매입처";

			grd장비.SetDataMap("CLS_S", GetDb.Code("MA_B000032"), "CODE", "NAME");

			grd장비.SetDefault("21.08.05.01", SumPositionEnum.None);
		}

		private void InitGrid_재고(FlexGrid flexGrid)
		{
			flexGrid.BeginSetting(2, 1, false);

			flexGrid.SetCol("NO_LINE"	, "필터"			, false);

			InitGrid_공통_재고정보(flexGrid);

			flexGrid.SetCol("UNIT_EXT"	, "단위"	, 50	, TextAlignEnum.CenterCenter);

			InitGrid_공통_재고수량(flexGrid);
			InitGrid_공통_매입출(flexGrid);
			InitGrid_공통_단가표(flexGrid);

			flexGrid.SetCol("DC_RMK1"	, "추가정보1"		, 60);
			flexGrid.SetCol("DC_RMK2"	, "추가정보2"		, 60);		
			flexGrid.SetCol("DC_RMK"	, "비고"			, 300);		// 공장품목등록의 비고 or 기실적의 견적라인 비고
			flexGrid.SetCol("DC_RMK3"	, "비고1"		, 300);     // 공장품목등록의 비고 or 기실적의 견적라인 비고

			flexGrid.SetDataMap("UNIT_EXT", CODE.Unit(), "CODE", "NAME");
			
			flexGrid.SetDefault("21.08.05.01", SumPositionEnum.None);
			flexGrid.SetAlternateRow();
			flexGrid.SetMalgunGothic();
			flexGrid.LoadUserCache("P_CZ_SA_QTN_EXT");
		}

		private void InitGrid_실적(FlexGrid flexGrid)
		{
			flexGrid.BeginSetting(2, 1, false);

			flexGrid.SetCol("NO_LINE"			 , "필터"	, false);
			flexGrid.SetCol("NO_FILE_EXT"		 , "파일번호"	, 90	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NO_LINE_EXT"		 , "항번"	, false);
			flexGrid.SetCol("LN_PARTNER"		 , "매출처"	, 230);
			flexGrid.SetCol("NO_DSP_EXT"		 , "순번"	, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NM_SUBJECT_EXT"	 , "주제"	, false);
			flexGrid.SetCol("CD_ITEM_PARTNER_EXT", "품목코드"	, 100);
			flexGrid.SetCol("NM_ITEM_PARTNER_EXT", "품목명"	, 200);
			flexGrid.SetCol("CD_ITEM"			 , "재고코드"	, 75);
			flexGrid.SetCol("NM_ITEM"			 , "재고명"	, 200);
			flexGrid.SetCol("UNIT_EXT"			 , "단위"	, 50	, TextAlignEnum.CenterCenter);

			InitGrid_공통_재고수량(flexGrid);
			InitGrid_공통_매입출(flexGrid);

			flexGrid.SetDataMap("UNIT_EXT", CODE.Unit(), "CODE", "NAME");
			
			flexGrid.SetDefault("21.02.19.03", SumPositionEnum.None);
			flexGrid.SetAlternateRow();
			flexGrid.SetMalgunGothic();
			flexGrid.LoadUserCache("P_CZ_SA_QTN_EXT");
		}

		private void InitGrid_공통_재고정보(FlexGrid flexGrid)
		{
			flexGrid.SetCol("CD_ITEM"		, "재고코드"		, 110	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NM_ITEM"		, "재고명"		, 230);
			flexGrid.SetCol("CLS_S"			, "분류"			, 80);
			flexGrid.SetCol("NO_PART"		, "파트넘버"		, 80);  // STND_ITEM
			flexGrid.SetCol("DC_MODEL"		, "적용모델"		, 120);	// DC_MODEL
			flexGrid.SetCol("NO_POS"		, "아이템넘버"	, 80);	// MAT_ITEM
			flexGrid.SetCol("DC_OFFER"		, "오퍼비고"		, 100);	// DC_OFFER
			flexGrid.SetCol("DC_STOCK"		, "재고비고"		, 100);	// DC_STOCK
		}

		private void InitGrid_공통_재고수량(FlexGrid flexGrid)
		{
			flexGrid.SetCol("QT_AVST"		, "재고수량"		, "가용"		, 45	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_AVPO"		, "재고수량"		, "미입고"	, 45	, typeof(decimal), FormatTpType.QUANTITY);
		}
		
		private void InitGrid_공통_매입출(FlexGrid flexGrid)
		{
			flexGrid.SetCol("CD_EXCH_P"		, "매입통화"		, false);
			flexGrid.SetCol("RT_EXCH_P"		, "매입환율"		, false);
			flexGrid.SetCol("UM_EX_E"		, "매입견적단가"	, "외화"		, 70	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("UM_KR_E"		, "매입견적단가"	, "원화"		, 70	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("RT_DC_P"		, "매입DC\n(%)"				, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("UM_EX_P"		, "매입단가"		, "외화"		, 70	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("UM_KR_P"		, "매입단가"		, "원화"		, 70	, typeof(decimal), FormatTpType.MONEY);			
			flexGrid.SetCol("RT_PROFIT"		, "이윤\n(%)"				, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("RT_MARGIN_DX"	, "민감율\n(%)"				, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("YN_MARGIN_EDIT", "수정\n여부"				, 45	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("CD_EXCH_S"		, "매출통화"		, false);
			flexGrid.SetCol("RT_EXCH_S"		, "매출환율"		, false);
			flexGrid.SetCol("UM_EX_Q"		, "매출견적단가"	, "외화"		, 70	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("UM_KR_Q"		, "매출견적단가"	, "원화"		, 70	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("RT_DC"			, "DC\n(%)"					, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("UM_EX_S"		, "매출단가"		, "외화"		, 70	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexGrid.SetCol("UM_KR_S"		, "매출단가"		, "원화"		, 70	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("RT_MARGIN"		, "최종\n(%)"				, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);			
			flexGrid.SetCol("LT"			, "납기"						, 40	, typeof(decimal), FormatTpType.MONEY);
		}

		private void InitGrid_공통_단가표(FlexGrid flexGrid)
		{
			flexGrid.SetCol("FG_UM"			, "단가정보"		, "단가유형"	, 70	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("UM_STD"		, "단가정보"		, "표준단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("UM_STK"		, "단가정보"		, "재고단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("RT_STK_DC"		, "단가정보"		, "할인율"	, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);			
		}
		
		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn테스트.Click += Btn테스트_Click;

			btn확정.Click += Btn확정_Click;
			btn확정취소.Click += Btn확정취소_Click;
			btn단가적용.Click += Btn단가적용_Click;
			btn확장.Click += Btn확장_Click;

			cbo매입처.SelectionChangeCommitted += Cbo매입처_SelectionChangeCommitted;
			cbm분류.QueryBefore += Bpc분류_QueryBefore;

			tbx검색.KeyDown += Tbx검색_KeyDown;
			btn검색.Click += Btn검색_Click;			

			grd헤드.DoubleClick += Grd헤드_DoubleClick;

			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;
			grd장비.AfterRowChange += Grd장비_AfterRowChange;
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			
		}

		private void Btn단가적용_Click(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)tab라인.SelectedTab.Controls[0];
			if (!flexGrid.HasNormalRow) return;
			
			// 단가 적용
			for (int i = 1; i < grd헤드.Cols.Count; i++)
			{
				string colName = grd헤드.Cols[i].Name;
				
				if (colName.In("NO_LINE") || !flexGrid.Cols.Contains(colName))		continue;   // 복사하면 안되는 컬럼				
				if (colName == "UNIT_EXT" && flexGrid[colName].ToString() == "")	continue;   // 단위의 경우 없으면 적용 안함

				// 적용
				grd헤드[colName] = flexGrid[colName].ToString() == "" ? DBNull.Value : flexGrid[colName];
			}

			// 선택 처리
			grd헤드["CHK"] = "Y";
		}

		private void Btn확장_Click(object sender, EventArgs e)
		{
			if (spc헤드.Panel2Collapsed)
			{
				// 접혀 있으면 펼치기
				spc헤드.Panel2Collapsed = false;
				spc헤드.Panel2.Show();
				btn확장.Text = ">";

				// 펼치면서 한번 조회
				ToggleColumn();
			}
			else
			{
				// 펼쳐져 있으면 접기
				spc헤드.Panel2Collapsed = true;
				spc헤드.Panel2.Hide();
				btn확장.Text = "<";
			}			
		}
		
		private void Cbo매입처_SelectionChangeCommitted(object sender, EventArgs e)
		{
			// 선택된 매입처의 가격조건 리스트
			string oVal = cbo가격조건.GetValue();	

			if (cbo매입처.GetValue() != "")
			{
				string query = @"
SELECT
	CODE	= DC_PRICE_TERMS
,	NAME	= DC_PRICE_TERMS
,	YN_SEL	= (SELECT 'Y' FROM CZ_MA_CODEDTL AS X WITH(NOLOCK) WHERE A.CD_COMPANY = X.CD_COMPANY AND X.CD_FIELD = 'CZ_DX00002' AND A.CD_PARTNER = X.CD_SYSDEF AND A.DC_PRICE_TERMS = X.CD_FLAG1)
FROM MA_ITEM_UMPARTNER AS A WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_PARTNER = '" + cbo매입처.GetValue() + @"'
	AND ISNULL(DC_PRICE_TERMS, '') != ''
	AND ISNULL(DC_PRICE_TERMS, '') != 'STOCK'
	AND LEFT(ISNULL(DC_PRICE_TERMS, ''), 4) != '사용불가'
GROUP BY CD_COMPANY, CD_PARTNER, DC_PRICE_TERMS";
				
				DataTable dt = SQL.GetDataTable(query);

				cbo가격조건.DataBind(dt, true);
				cbo가격조건.SetValue(oVal);

				// 선택된게 없는 경우
				if (cbo가격조건.GetValue() == "")
				{
					// 가격조건이 1개일 경우 자동 선택
					if (cbo가격조건.Items.Count == 2)
						cbo가격조건.SelectedIndex = 1;
					else
					{
						DataRow[] row = dt.Select("YN_SEL = 'Y'");
						if (row.Length > 0) cbo가격조건.SetValue(row[0]["CODE"]);
					}
				}

				// 선택된 벤더의 장비 리스트
				ToggleColumn();

				// ********* Notice!!
				// 테크로스
				if (cbo매입처.GetValue() == "17747")
				{
					query = @"
SELECT
	A.RT_COMMISSION AS RT_COMMISSION1
,	B.CD_FLAG1		AS RT_COMMISSION2
FROM      V_CZ_MA_PARTNER AS A WITH(NOLOCK)
LEFT JOIN V_CZ_MA_CODEDTL AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND B.CD_FIELD = 'CZ_DX00007' AND A.CD_PARTNER = B.CD_SYSDEF
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CompanyCode + @"'
	AND A.CD_PARTNER = '" + PartnerCode + "'";

					DataTable dtComms = SQL.GetDataTable(query);

					if (dtComms.Rows.Count == 1 && dtComms.Rows[0]["RT_COMMISSION1"].ToInt() > 0)
					{
						NOITCE notice = new NOITCE(web비고);
						//notice.Add("a");
						//notice.Add("b");

						//						web비고.DocumentText = @"
						//<html>
						//	<head>
						//		<style type='text/css'>
						//			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }
						//			body { background-color:#f6f7f8; }
						//		</style>
						//	</head>
						//	<body>
						//		<span style='color:blue; font-weight:bold;'>테크로스</span> 커미션 적용 선사 : {0}%
						//	</body>
						//</html>";

						//" + dtComms.Rows[0]["RT_COMMISSION2"] + @


					}
				}
			}
			else
			{
				cbo가격조건.ClearData();
			}
		}

		private void Bpc분류_QueryBefore(object sender, BpQueryArgs e)
		{
			// 호선 필수인 경우는 해당 호선의 장착 모델만 보여줌
			e.HelpParam.P61_CODE1 = @"
	CD_SYSDEF AS CODE
,	NM_SYSDEF AS NAME";

			e.HelpParam.P62_CODE2 = @"
V_CZ_MA_CODEDTL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = string.Format(@"
WHERE 1 = 1
	AND CD_COMPANY = '{0}'
	AND CD_FIELD = 'MA_B000032'
	AND CD_FLAG1 = (SELECT CD_SYSDEF FROM V_CZ_MA_CODEDTL WITH(NOLOCK) WHERE CD_COMPANY = '{0}' AND CD_FIELD = 'MA_B000031' AND CD_FLAG1 = 'EQ' AND CD_FLAG2 = '{1}')
	AND 
	(
			NOT EXISTS (SELECT 1 FROM V_CZ_MA_CODEDTL WITH(NOLOCK) WHERE CD_COMPANY = '{0}' AND CD_FIELD = 'CZ_DX00002' AND CD_SYSDEF = '{1}' AND CD_FLAG2 = 'Y')
		OR  CD_SYSDEF IN (SELECT CLS_S FROM CZ_MA_HULL_VENDOR_TYPE WITH(NOLOCK) WHERE NO_IMO = '{2}' AND CD_VENDOR = '{1}')
	)
ORDER BY CD_SYSDEF", CompanyCode, cbo매입처.GetValue(), ImoNumber);
		}

		private void Tbx검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				Btn검색_Click(null, null);
		}

		private void Grd헤드_DoubleClick(object sender, EventArgs e)
		{
			// 헤더클릭
			if (grd헤드.MouseRow < grd헤드.Rows.Fixed)
			{
				SetGridStyle();
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			((TabPage)Parent).ImageIndex = -1;
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.None;

			// 탭 이동없이 이 탭에서 바로 조회할 수 있으므로 조회시 한번 더 바인딩 함
			SetVendor();

			// 매입처 선택 여부 체크
			if (!cbo매입처.CheckValue("매입처를 선택하세요")) return;

			// 소분류 선택 여부
			if (cbo매입처.GetValue("YN_CLS") == "Y" && cbm분류.GetValue() == "")
			{
				ShowMessage("분류를 선택하세요");
				return;
			}

			// 가격조건 선택 여부 체크
			if (cbo가격조건.Items.Count > 1 && cbo가격조건.GetValue() == "")
			{
				ShowMessage("가격조건을 선택하세요");
				return;
			}

			try
			{
				UT.ShowPgb(DD("조회중입니다."));

				// 확정상태
				string ynExt = "";
				if (rdo확정상태N.Checked) ynExt = "N";
				if (rdo확정상태Y.Checked) ynExt = "Y";

				// DX테이블에 값 있는지 확인하고 없으면 넣기
				//string query = "SELECT 1 FROM CZ_DX_INQ WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_FILE = '" + FileNumber + "'";
				//if (SQL.GetDataTable(query).Rows.Count == 0)
				//SaveInq(CompanyCode, FileNumber);
				키워드.견적저장(CompanyCode, FileNumber);

				// ********** 조회
				SQL sql = new SQL("", SQLType.Procedure, sqlDebug);

				if (cbo단가기준1.GetValue() == "CONTRACT")
				{
					sql.Procedure = "PS_CZ_SA_QTN_EXT_1CALL";
					sql.Parameter.Add2("@CD_COMPANY"	, CompanyCode);
					sql.Parameter.Add2("@NO_FILE"		, FileNumber);
					sql.Parameter.Add2("@CD_VENDOR"		, cbo매입처.GetValue());
					sql.Parameter.Add2("@NO_IMO"		, cbo매입처.GetValue("YN_CLS") == "Y" ? ImoNumber : "");
					sql.Parameter.Add2("@CLS_S"			, cbo매입처.GetValue("YN_CLS") == "Y" ? cbm분류.GetValue() : "");	
					sql.Parameter.Add2("@PRICE_TERMS"	, cbo가격조건.GetValue());
					sql.Parameter.Add2("@YN_STD_UNIT"	, chk표준단위적용.Checked ? "Y" : "N");
					sql.Parameter.Add2("@YN_EXT"		, ynExt);
					sql.Parameter.Add2("@MODE"			, "H");
				}
				else
				{
					sql.Procedure = "PS_CZ_SA_QTN_EXT_H_RECORD";

					sql.Parameter.Add2("@CD_COMPANY"	, CompanyCode);
					sql.Parameter.Add2("@NO_FILE"		, FileNumber);
					sql.Parameter.Add2("@YN_EXT"		, ynExt);
					sql.Parameter.Add2("@CD_SUPPLIER"	, cbo매입처.GetValue());
					sql.Parameter.Add2("@CR_PERIOD"	, cbo단가기준2.GetValue());
					sql.Parameter.Add2("@CR_TARGET"	, rdo단가대상1.Checked ? 1 : 2);
					sql.Parameter.Add2("@CR_PREFIX"	, GetFilePrefix());
					sql.Parameter.Add2("@YN_BUYER"	, chkBuyer.Checked ? PartnerCode : "");
					sql.Parameter.Add2("@YN_HULL"		, chkHull.Checked ? ImoNumber : "");
					sql.Parameter.Add2("@YN_ORDER"	, chkOrder.Checked ? "Y" : "N");

					tab라인.SelectedTab = tab실적;
				}

				// 쿼리 실행 및 Linq 조인
				DataTable dtHead = sql.GetDataTable();
				Util.CalculateRate(dtHead, "YN_EXT = 'N'");

				// ********** 체크 컬럼 추가 및 값 할당
				dtHead.Columns.Add(new DataColumn("CHK", typeof(string)) { DefaultValue = "N" });

				foreach (DataRow dr in dtHead.Rows)
				{
					if (cbo단가기준1.GetValue() == "CONTRACT")
					{
						if (dr["YN_EXT"].ToString() == "N" && dr["CD_ITEM"].ToString() != "")
							dr["CHK"] = "Y";
					}
					else
					{
						if (dr["YN_EXT"].ToString() == "N" && dr["NO_FILE_EXT"].ToString() != "")
							dr["CHK"] = "Y";
					}
				}

				// 바인딩
				grd헤드.DataBind(dtHead);				
				SetGridStyle();
				ToggleCol(grd헤드);
				UT.ClosePgb();
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter = "NO_LINE = " + grd헤드["NO_LINE"];

			if (grd헤드.DetailQueryNeed)
			{
				try
				{
					UT.ShowPgb(DD("조회중입니다."));
					tbxFocus.Focus();

					if (cbo단가기준1.GetValue() == "CONTRACT")
					{
						// ********** 단가계약
						SQL sql = new SQL("PS_CZ_SA_QTN_EXT_1CALL", SQLType.Procedure, SQLDebug.Print);
						sql.Parameter.Add2("@CD_COMPANY"	, CompanyCode);
						sql.Parameter.Add2("@NO_FILE"		, FileNumber);
						sql.Parameter.Add2("@NO_LINE"		, grd헤드["NO_LINE"]);
						sql.Parameter.Add2("@CD_VENDOR"		, cbo매입처.GetValue());
						sql.Parameter.Add2("@NO_IMO"		, cbo매입처.GetValue("YN_CLS") == "Y" ? ImoNumber : "");
						sql.Parameter.Add2("@CLS_S"			, cbo매입처.GetValue("YN_CLS") == "Y" ? cbm분류.GetValue() : "");	
						sql.Parameter.Add2("@PRICE_TERMS"	, cbo가격조건.GetValue());
						sql.Parameter.Add2("@YN_STD_UNIT"	, chk표준단위적용.Checked ? "Y" : "N");
						sql.Parameter.Add2("@MODE"			, "L");

						DataTable dt = sql.GetDataTable();
						Util.CalculateRate(dt, "");
						grd재고.DataBindAdd(dt, filter);
					}
					else
					{ 
						//// ********** 실적
						//DBMgr dbRecord = new DBMgr();
						//dbRecord.Procedure = "PS_CZ_SA_QTN_EXT_RECORD_L";
						//dbRecord.AddParameter("@CD_COMPANY"	, CompanyCode);
						//dbRecord.AddParameter("@NO_FILE"	, FileNumber);
						//dbRecord.AddParameter("@NO_LINE"	, grd헤드["NO_LINE"]);
						//dbRecord.AddParameter("@CD_SUPPLIER", cbo매입처.GetValue());
						//dbRecord.AddParameter("@CR_PERIOD"	, cbo단가기준2.GetValue());
						//dbRecord.AddParameter("@CR_TARGET"	, rdo단가대상1.Checked ? 1 : 2);
						//dbRecord.AddParameter("@CR_PREFIX"	, GetFilePrefix());
						//dbRecord.AddParameter("@YN_BUYER"	, chkBuyer.Checked ? PartnerCode : "");
						//dbRecord.AddParameter("@YN_HULL"	, chkHull.Checked ? ImoNumber : "");
						//dbRecord.AddParameter("@YN_ORDER"	, chkOrder.Checked ? "Y" : "N");

						//DataView dvRecord = new DataView(dbRecord.GetDataTable());
						//DataTable dtRecord = GetDb.JoinStockQuantityR3(ctx재고대상.CodeValue, dvRecord.ToTable());
						//Util.CalculateRate(dtRecord, "");
						//grd실적.DataBindAdd(dtRecord, filter);
					}

					UT.ClosePgb();
				}
				catch (Exception ex)
				{
					UT.ShowMsg(ex);
				}
			}
			else
			{
				grd재고.DataBindAdd(null, filter);
				grd실적.DataBindAdd(null, filter);
			}

			// 재고그리드만 토글
			ToggleCol(grd재고);
		}

		private void Btn검색_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.None;

			if (grd헤드.HasNormalRow && tbx검색.Text.Trim() != "")
			{
				try
				{
					UT.ShowPgb(DD("조회중입니다."));
					tbxFocus.Focus();

					if (cbo검색.GetValue() == "C")
					{
						// 가격조건 선택 여부 체크
						if (cbo가격조건.Items.Count > 1 && cbo가격조건.GetValue() == "")
						{
							UT.ShowMsg("가격조건을 선택하세요");
							return;
						}

						// ********** 단가계약
						SQL sql = new SQL("PS_CZ_SA_QTN_EXT_1CALL", SQLType.Procedure, sqlDebug);
						sql.Parameter.Add2("@CD_COMPANY"	, CompanyCode);
						sql.Parameter.Add2("@NO_FILE"		, FileNumber);
						sql.Parameter.Add2("@NO_LINE"		, grd헤드["NO_LINE"]);
						sql.Parameter.Add2("@CD_VENDOR"		, cbo매입처.GetValue());
						sql.Parameter.Add2("@NO_IMO"		, cbo매입처.GetValue("YN_CLS") == "Y" ? ImoNumber : "");
						sql.Parameter.Add2("@CLS_S"			, cbo매입처.GetValue("YN_CLS") == "Y" ? cbm분류.QueryWhereIn_Pipe : "");	
						sql.Parameter.Add2("@PRICE_TERMS"	, cbo가격조건.GetValue());
						sql.Parameter.Add2("@YN_STD_UNIT"	, chk표준단위적용.Checked ? "Y" : "N");
						sql.Parameter.Add2("@SEARCH"		, tbx검색.Text);
						sql.Parameter.Add2("@MODE"			, "S");

						DataTable dt = sql.GetDataTable();
						Util.CalculateRate(dt, "");
						grd재고검색.Binding = dt;
						ToggleCol(grd재고검색);
						tab라인.SelectedTab = tab재고검색;
					}
					else
					{
						//DBMgr db = new DBMgr();
						//db.Procedure = "PS_CZ_SA_QTN_EXT_L_RECORD";
						//db.AddParameter("@CD_COMPANY"	, CompanyCode);
						//db.AddParameter("@NO_FILE"		, FileNumber);
						//db.AddParameter("@CD_SUPPLIER"	, cbo매입처.GetValue());
						//db.AddParameter("@CR_PERIOD"	, cbo단가기준2.GetValue());
						//db.AddParameter("@CR_TARGET"	, rdo단가대상1.Checked ? 1 : 2);
						//db.AddParameter("@CR_PREFIX"	, GetFilePrefix());
						//db.AddParameter("@YN_BUYER"		, chkBuyer.Checked ? PartnerCode : "");
						//db.AddParameter("@YN_HULL"		, chkHull.Checked ? ImoNumber : "");
						//db.AddParameter("@YN_ORDER"		, chkOrder.Checked ? "Y" : "N");
						//db.AddParameter("@SEARCH"		, tbx검색.Text);

						//DataTable dt = GetDb.JoinStockQuantityR3(ctx재고대상.CodeValue, db.GetDataTable());
						//Util.CalculateRate(dt, "");

						//grd실적검색.Binding = dt;
						//grd실적검색.AutoSizeRows();
						//grd실적검색.Rows[0].Height = 28;
						//grd실적검색.Rows[1].Height = 28;

						//tab라인.SelectedTab = tab실적검색;
					}

					UT.ClosePgb();
				}
				catch (Exception ex)
				{
					UT.ShowMsg(ex);
				}				
			}
		}
		
		private void Grd장비_AfterRowChange(object sender, RangeEventArgs e)
		{
			MsgControl.ShowMsg("조회중입니다.");
			tbxFocus.Focus();

			
			// 가격조건 선택 여부 체크
			if (cbo가격조건.Items.Count > 1 && cbo가격조건.GetValue() == "")
			{
				ShowMessage("가격조건을 선택하세요");
				return;
			}


			DBMgr dbm = new DBMgr
			{
				//DebugMode = DebugMode.Popup
			};

			if (cbo매입처.GetValue().In("17650", "08507", "00133"))
			{ 
				dbm.Procedure = "PS_CZ_SA_QTN_EXT_DX";
				dbm.AddParameter("@CD_COMPANY"	, CompanyCode);
				dbm.AddParameter("@NO_FILE"		, FileNumber);
				dbm.AddParameter("@CD_VENDOR"	, cbo매입처.GetValue());
				dbm.AddParameter("@NO_IMO"		, ImoNumber);
				dbm.AddParameter("@CLS_S"		, grd장비["CLS_S"]);
				dbm.AddParameter("@PRICE_TERMS"	, cbo가격조건.GetValue());
				dbm.AddParameter("@YN_STD_UNIT"	, chk표준단위적용.Checked ? "Y" : "N");
				dbm.AddParameter("@YN_EXT"		, "N");
				dbm.AddParameter("@SEARCH"		, "%");
				dbm.AddParameter("@MODE"		, "S");
			}				

			DataTable dt = GetDb.JoinStockQuantityR3(ctx재고대상.CodeValue, dbm.GetDataTable());
			Util.CalculateRate(dt, "");
					
			grd재고검색.DataBind(dt);	
			tab라인.SelectedTab = tab재고검색;
			MsgControl.CloseMsg();
		}

		#endregion

		#region ==================================================================================================== Save

		private void Btn확정_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.None;
			DataTable dt = grd헤드.DataTable.Select("CHK = 'Y'").ToDataTable();

			if (dt != null)
			{
				try
				{
					SQL sql = new SQL("PX_CZ_SA_QTN_EXT", SQLType.Procedure, sqlDebug);
					sql.Parameter.Add2("@MODE"	, rdo복사옵션1.Checked ? "P" : "PS");
					sql.Parameter.Add2("@XML"	, GetTo.Xml(dt));
					sql.Parameter.Add2("@NO_REF", (cbo단가기준1.GetValue() == "CONTRACT" ? "단가표-" : "기실적-") + UT.Today());
					sql.ExecuteNonQuery();

					// DX테이블 저장
					키워드.견적저장(dt);

					UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
					Search();
				}
				catch (Exception ex)
				{
					UT.ShowMsg(ex);
				}
			}
			else
			{
				UT.ShowMsg(공통메세지.선택된자료가없습니다);
			}
		}

		private void Btn확정취소_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.None;
			DataTable dt = grd헤드.DataTable.Select("CHK = 'Y'").ToDataTable();

			if (dt != null)
			{
				try
				{
					SQL sql = new SQL("PX_CZ_SA_QTN_EXT", SQLType.Procedure, sqlDebug);
					sql.Parameter.Add2("@MODE"	, "C");
					sql.Parameter.Add2("@XML"	, GetTo.Xml(dt));
					sql.ExecuteNonQuery();

					UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
					Search();
				}
				catch (Exception ex)
				{
					UT.ShowMsg(ex);
				}
			}
			else
			{
				UT.ShowMsg(공통메세지.선택된자료가없습니다);
			}
		}

		#endregion

		#region ==================================================================================================== 기타

		// 탭 이동할때 이부분만 바인딩함
		public void SetVendor()
		{

//			string query = @"
//SELECT
//	CODE	= DC_PRICE_TERMS
//,	NAME	= DC_PRICE_TERMS
//,	YN_SEL	= (SELECT 'Y' FROM CZ_MA_CODEDTL AS X WITH(NOLOCK) WHERE A.CD_COMPANY = X.CD_COMPANY AND X.CD_FIELD = 'CZ_DX00002' AND A.CD_PARTNER = X.CD_SYSDEF AND A.DC_PRICE_TERMS = X.CD_FLAG1)
//FROM MA_ITEM_UMPARTNER AS A WITH(NOLOCK)
//WHERE 1 = 1
//	AND CD_COMPANY = '" + CompanyCode + @"'
//	AND CD_PARTNER = '" + cbo매입처.GetValue() + @"'
//	AND ISNULL(DC_PRICE_TERMS, '') != ''
//	AND ISNULL(DC_PRICE_TERMS, '') != 'STOCK'
//	AND LEFT(ISNULL(DC_PRICE_TERMS, ''), 4) != '사용불가'
//GROUP BY CD_COMPANY, CD_PARTNER, DC_PRICE_TERMS";


			// 매입처 콤보 바인딩
			string oVal = cbo매입처.GetValue();
			string query = string.Format(@"
SELECT
	CD_PARTNER	AS CODE
,	LN_PARTNER	AS NAME
,	ISNULL((SELECT 'Y' FROM CZ_MA_CODEDTL AS X WITH(NOLOCK) WHERE A.CD_COMPANY = X.CD_COMPANY AND X.CD_FIELD = 'CZ_DX00002' AND X.CD_SYSDEF LIKE A.CD_PARTNER + '%' AND X.CD_FLAG2 = 'Y'), '') AS YN_CLS
FROM V_CZ_PU_QTNH AS A WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '{0}'
	AND NO_FILE = '{1}'", CompanyCode, FileNumber);

			DataTable dt = DBMgr.GetDataTable(query);
			cbo매입처.DataBind(dt, true);
			cbo매입처.SetValue(oVal);

			// 매입처가 1개일 경우 자동 선택
			if (cbo매입처.GetValue() == "" && cbo매입처.Items.Count > 1)
				cbo매입처.SelectedIndex = 1;
			
			// 매입처가 선택이 되어 있으면 체인지커밋 실행
			if (cbo매입처.GetValue() != "")			
				Cbo매입처_SelectionChangeCommitted(null, null);
		}

		private void SetGridStyle()
		{
			grd헤드.Redraw = false;

			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				// 가용재고나 미입고가 있는 경우 색상 표시
				if (grd헤드[i, "QT_AVSUM"].ToInt() > 0)
					grd헤드.Rows[i].Style = grd헤드.Styles["ACTIVE"];
				else
					grd헤드.Rows[i].Style = null;

				// INQ 단위와 다른 항목 색상 표시
				if (grd헤드[i, "UNIT"].ToString() != grd헤드[i, "UNIT_EXT"].ToString())
					grd헤드.SetCellColor(i, "UNIT_EXT", Color.Red);
				else
					grd헤드.SetCellColor(i, "UNIT_EXT", "");

				// 기획실 견적건
				if (grd헤드[i, "YN_SPO"].ToString() == "Y")
					grd헤드.SetCellColor(i, "CD_ITEM", Color.Red);
				else
					grd헤드.SetCellColor(i, "CD_ITEM", "");
			}

			grd헤드.Redraw = true;
		}

		private void ToggleColumn()
		{
			if (!grd장비.Visible)
				return;

			// ********** 벤더별 필드명 가져오기
			string query = @"
SELECT
	CD_FLAG2	AS CODE
,	NM_SYSDEF	AS NAME
FROM V_CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00033'
	AND CD_FLAG1 = '" + cbo매입처.GetValue() + "'";

			DataTable dtCols = DBMgr.GetDataTable(query);

			// 코드관련 필드 숨김
			for (int i = 1; i <= 15; i++)
			{
				grd장비.Cols["DC_RMK" + i].Caption = "코드" + i;
				grd장비.Cols["DC_RMK" + i].Visible = false;
			}

			// 해당 벤더 필드만 보여줌
			foreach (DataRow row in dtCols.Rows)
			{
				grd장비.Cols[(string)row["CODE"]].Caption = (string)row["NAME"];
				grd장비.Cols[(string)row["CODE"]].Visible = true;
			}

			// 위에 라인은 매입처명 보여주기
			for (int i = 1; i < grd장비.Cols.Count; i++)
				grd장비[0, i] = "매입처 - " + cbo매입처.GetText();

			// ********** 장비 바인딩
			query = @"
SELECT
	*
FROM CZ_MA_HULL_VENDOR_TYPE WITH(NOLOCK)
WHERE 1 = 1
	AND NO_IMO = '" + ImoNumber + @"'
	AND CD_VENDOR = '" + cbo매입처.GetValue() + "'";

			DataTable dtEquip = DBMgr.GetDataTable(query);
			grd장비.DataBind(dtEquip);			
		}

		private string GetFilePrefix()
		{
			string prefix = "";

			foreach (Control con in pnl파일구분.Controls)
			{
				if (con is CheckBoxExt)
				{
					CheckBoxExt chk = (CheckBoxExt)con;
					if (chk.Checked) prefix += ",'" + chk.Text + "'";
				}
			}

			if (prefix != "") prefix = prefix.Substring(1);

			return prefix;
		}

		private void ToggleCol(FlexGrid flexGrid)
		{
			flexGrid.Redraw = false;

			bool 재고;
			bool 실적;
			string[] col재고 = { "NM_ITEM", "CLS_S", "NO_PART", "DC_MODEL", "NO_POS", "DC_OFFER", "DC_STOCK", "DC_RMK1", "DC_RMK2", "DC_RMK" };
			string[] col실적 = { "CD_ITEM_PARTNER_EXT", "NM_ITEM_PARTNER_EXT", "NO_FILE_EXT" };

			// 재고인지 실적인지 판단
			if (flexGrid == grd헤드)
			{
				재고 = cbo단가기준1.GetValue() == "CONTRACT";
				실적 = cbo단가기준1.GetValue() != "CONTRACT";

				// 헤드 그리드 일때만 보이기 가리기
				foreach (string s in col재고) flexGrid.Cols[s].Visible = 재고;
				foreach (string s in col실적) flexGrid.Cols[s].Visible = 실적;
			}
			else
			{
				재고 = flexGrid.Name.Contains("재고");
			}

			// 재고 컬럼 보이기, 값 없는 필드는 숨기기
			if (재고)
			{
				foreach (string s in col재고)
					if (flexGrid.DataTable.Select(s + " <> ''").Length == 0) flexGrid.Cols[s].Visible = false;
			}

			flexGrid.Redraw = true;			
		}

		#endregion

	}
}


