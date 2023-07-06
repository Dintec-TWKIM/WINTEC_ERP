using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;

using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

using Dintec;


using PopupControl;

using System.Net;
using System.IO;
using System.Linq;
using DX;

namespace cz
{
	public partial class P_CZ_SA_QTN : PageBase
	{
		FreeBinding Header = new FreeBinding();
		string[] VendorCodes;
		int KR_ROUND = 0;
		ToolTip toolTip = new ToolTip();

		decimal DcDefault;
		decimal DcTO;

		List<string> EditCols;

		#region ===================================================================================================== Property

		public P_CZ_SA_QTN_REG Quotation
		{
			get
			{
				return (P_CZ_SA_QTN_REG)this.Parent.GetContainerControl();
			}
		}

		public decimal 환율
		{
			get
			{
				return cur환율.DecimalValue;
			}
		}

		public int TP_DIGIT
		{
			get
			{
				return GetTo.Int(Util.GetDB_CD_FLAG1(cbo표시형식));
			}
		}

		public FlexGrid 그리드라인
		{
			get
			{
				return grd라인;
			}
		}

		public FlexGrid 그리드가로
		{
			get
			{
				return grd가로;
			}
		}

		public FlexGrid 그리드세로
		{
			get
			{
				return grd세로;
			}
		}

		public bool IsEquipment
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).부품영업;
			}
		}

		private int 표시형식단가용
		{
			get
			{
				int roundCode;

				if (rdo표시형식단가.Checked)
				{
					roundCode = GetTo.Int(GetDb.CodeFlag1(cbo표시형식));
				}
				else
				{
					if (cbo통화.GetValue() == "000")
						roundCode = 0;
					else
						roundCode = 2;
				}

				return roundCode;
			}
		}

		private int 표시형식금액용
		{
			get
			{
				// 금액 표시형식은 무조건 표기 표시형식을 따름 (단가, 금액 라디오 무의미, 단가 * 수량해서 나온거 이므로 단가 체크되있어도 단가 표시형식을 따름)
				return GetTo.Int(GetDb.CodeFlag1(cbo표시형식));
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_QTN()
		{
			//StartUp.Certify(this);
			if (LoginInfo.CompanyCode != "S100")
				KR_ROUND = 0;
			else 
				KR_ROUND = 2;
			InitializeComponent();
			this.SetConDefault();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			TitleText = "Loaded";
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{		
			// 기타
			tbx포커스.Left = -500;

			// 달력은 타이핑 못하게 함, 타이핑 될때마다 환율 불러오는 문제도 있고 조회될때도 이벤트 실행되므로 클릭으로만 변경되게 함
			dtp견적일자.Init();
			dtp견적일자.AllowTyping(false);
			dtp유효일자.Init();
			dtp유효일자.AllowTyping(false);

			// 콤보박스
			DataSet ds = GetDb.Code("MA_B000005", "TR_IM00003", "CZ_SA00005", "TR_IM00011", "CZ_SA00013", "MA_B000040", "CZ_SA00014");
			Util.SetDB_CODE(cbo통화	, ds.Tables[0], false);
			Util.SetDB_CODE(cbo선적조건1	, ds.Tables[1], true);
			Util.SetDB_CODE(cbo포장형태	, ds.Tables[3], true);
			Util.SetDB_CODE(cbo지불조건	, ds.Tables[4], true);
			Util.SetDB_CODE(cbo과세구분	, ds.Tables[5], true);
			Util.SetDB_CODE(cbo표시형식	, ds.Tables[6], true);
			Util.SetDB_CODE(cbo부대비용	, Util.GetDB_EXTRA_COST(), true);

			cbo인도기간.Items.Add("");
			cbo인도기간.Items.Add("기본값");
			cbo인도기간.Items.Add("기간(일)");
			cbo인도기간.Items.Add("기간(날짜)");
			cbo인도기간.Items.Add("납기(일)");
			cbo인도기간.Items.Add("납기(날짜)");

			// 바인딩 초기화
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_SA_QTN_H_R2", "", "");
			Header.SetBinding(dtHead, one헤드);
			Header.ClearAndNewRow();
		}

		

		protected override void InitPaint()
		{
			if (IsEquipment)
				tab메인.SelectedIndex = 1;
			else
				tab메인.SelectedIndex = 2;

			// 갈매기 엑셀 업로드 버튼			
			if (LoginInfo.EmployeeNo != "S-343")
				btn갈매기.Visible = false;

			// 갈매기 필드 제거
			//if (!Certify.IsLive() || Quotation.HiddenYn == "Y")
			//    dtLineSt.Columns.Remove("YN_GULL");
		}

		public void Clear()
		{
			if (TitleText != "Loaded")
				return;

			// 헤더 초기화
			Header.ClearAndNewRow();

			cbo원산지.ClearData();
			cbo인도기간.SetDefaultValue();
			cbo부대비용.SetDefaultValue();
			rdo표시형식금액.Checked = true;
			
			// 그리드
			grd라인.ClearData();
			grd가로.ClearData();
			grd세로.ClearData();
			grd매입처.ClearData();

			// 라벨
			lbl통화외화.Text = "USD";

			lbl매출견적금액원화.Text = "0";
			lbl매출견적금액외화.Text = "0";

			lbl매출금액원화.Text = "0";
			lbl매출금액외화.Text = "0";

			lblDC금액원화.Text = "0";
			lblDC금액외화.Text = "0";

			lbl이윤금액일반.Text = "0";
			lbl이윤율일반.Text = "0";

			lbl이윤금액재고.Text = "0";
			lbl이윤율재고.Text = "0";

			lbl부대비용원화.Text = "0";
			lbl최종합계원화.Text = "0";

			lbl계약할인합계원화.Text = "0";
			lbl계약할인할인율.Text = "0";
			lbl계약할인이윤원화.Text = "0";
			lbl계약할인이윤율.Text = "0";
		}

		public void 사용(bool enabled)
		{
			pnl버튼.Editable(enabled);
			one헤드.Editable(enabled);

			//grd라인.Editable(enabled);

			if (grd라인.Cols.Count > 1)
			{
				string[] editCols = { "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "UNIT_QTN", "QT_QTN"
								, "RT_PROFIT", "UM_EX_Q", "RT_DC", "UM_EX_S"
								, "LT", "DC_RMK", "YN_DSP_RMK" };

				grd라인.SetEditColumn(enabled, editCols);
			}

			grd가로.AllowEditing = enabled;
			grd세로.AllowEditing = enabled;
			grd매입처.AllowEditing = enabled;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			// ********** 라인
			grd라인.BeginSetting(2, 1, false);

			grd라인.SetCol("NO_FILE"			, "파일번호"		, false);
			grd라인.SetCol("NO_LINE"			, "항번"			, false);
			grd라인.SetCol("NO_DSP"			, "순번"			, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd라인.SetCol("GRP_ITEM"		, "유형"			, false);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 130	, false);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 250	, false);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 90	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_ITEM"			, "재고명"		, false);
			grd라인.SetCol("CD_VENDOR"		, "매입처코드"	, false);
			grd라인.SetCol("NM_VENDOR"		, "매입처"		, 80	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("UNIT"			, "단위(INQ)"	, false);
			grd라인.SetCol("UNIT_QTN"		, "단위"			, 60	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("QT"				, "수량(INQ)"	, false);
			grd라인.SetCol("QT_QTN"			, "수량"			, 50	, typeof(decimal), FormatTpType.QUANTITY);

			grd라인.SetCol("UM_STD"			, "기준단가"		, "표준단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("UM_STK"			, "기준단가"		, "재고단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("UM_MIN"			, "기준단가"		, "최저단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("QT_AVST"			, "가용재고"		, "재고"		, 45	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_AVPO"			, "가용재고"		, "입고"		, 45	, typeof(decimal), FormatTpType.QUANTITY);

			grd라인.SetCol("UM_EX_STD_P"		, "매입견적단가"	, false);
			grd라인.SetCol("AM_EX_STD_P"		, "매입견적금액"	, false);
			grd라인.SetCol("RT_DC_P"			, "DC(%)"		, false);

			grd라인.SetCol("UM_EX_P"			, "매입단가"		, "외화단가"	, 75	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("AM_EX_P"			, "매입단가"		, "외화금액"	, 75	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_P"			, "매입단가"		, "원화단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_P"			, "매입단가"		, "원화금액"	, 75	, typeof(decimal), FormatTpType.MONEY);
			
			grd라인.SetCol("RT_PROFIT"		, "이윤\n(%)"				, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_EX_Q"			, "매출견적단가"	, "외화단가"	, 75	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("AM_EX_Q"			, "매출견적단가"	, "외화금액"	, 75	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_Q"			, "매출견적단가"	, "원화단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_Q"			, "매출견적단가"	, "원화금액"	, 75	, typeof(decimal), FormatTpType.MONEY);
			
			grd라인.SetCol("RT_DC"			, "DC\n(%)"					, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_EX_S"			, "매출단가"		, "외화단가"	, 75	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("AM_EX_S"			, "매출단가"		, "외화금액"	, 75	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_S"			, "매출단가"		, "원화단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_S"			, "매출단가"		, "원화금액"	, 75	, typeof(decimal), FormatTpType.MONEY);

			grd라인.SetCol("RT_MARGIN"		, "최종마진(%)"	, "최종"		, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("RT_MARGIN_ST"	, "최종마진(%)"	, "재고"		, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("RT_MARGIN_MIN"	, "최종마진(%)"	, "최저"		, 45	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			
			grd라인.SetCol("LT"				, "납기"			, 40	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("DC_RMK"			, "비고"			, 200);
			grd라인.SetCol("WEIGHT"			, "무게(Kg)"		, "단품"		, 60	, typeof(decimal), "#,##0.####", TextAlignEnum.RightCenter);
			grd라인.SetCol("WEIGHT_TOT"		, "무게(Kg)"		, "합계"		, 60	, typeof(decimal), "#,##0.####", TextAlignEnum.RightCenter);
			grd라인.SetCol("FILE_LIST"		, "파일여부"		, false);
			grd라인.SetCol("FILE_ICON"		, "첨부"			, 35	, ImageAlignEnum.CenterCenter);
			grd라인.SetCol("REFERENCE"		, "첨부"			, false);
			grd라인.SetCol("YN_DSP_RMK"		, "D"			, 30	, CheckTypeEnum.Y_N);
			grd라인.SetCol("YN_GULL"			, "H"			, 30	, CheckTypeEnum.Y_N);
			
			grd라인.SetCol("YN_BASE"			, "표준단가적용"	, false);
			grd라인.SetCol("TP_ENGINE"		, "사양"			, false);
			grd라인.SetCol("TP_BOM"			, "BOM구분"		, false);
			grd라인.SetCol("CHK_ITEM"		, "아이템체크"	, false);	////////////////////////////////////// 어디서 쓰는거 같은데 현재는 값이 없음, 연구해봐야 할듯
			grd라인.SetCol("YN_QLINK"		, "퀵링크"		, false);

			grd라인.SetDataMap("UNIT_QTN", CODE.Unit(), "CODE", "NAME");
			grd라인.Cols["UM_EX_P"].Visible = false;
			grd라인.Cols["AM_EX_P"].Visible = false;

			// 편집 컬럼
			EditCols = new List<string>() { "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "UNIT_QTN", "QT_QTN", "RT_PROFIT", "UM_EX_Q", "RT_DC", "UM_EX_S", "LT", "DC_RMK", "YN_DSP_RMK", "YN_GULL" };

			if (Quotation.회사코드 == "K100" || Quotation.숨김여부 == "Y" || !Certify.IsLive())
			{
				grd라인.Cols.Remove("YN_GULL");
				EditCols.Remove("YN_GULL");
			}

			grd라인.SetDefault("22.12.08.01", SumPositionEnum.Top);
			grd라인.SetEditColumn(EditCols.ToArray());
			grd라인.SetExceptSumCol(true, "WEIGHT");
			grd라인.SetSumColumnStyle("AM_EX_P", "AM_KR_P", "AM_EX_Q", "AM_KR_Q", "AM_EX_S", "AM_KR_S");
			grd라인.SetAlternateRow();
			grd라인.SetMalgunGothic();

			grd라인.Cols["QT_AVST"].SetColor(Color.Blue);
			grd라인.Cols["QT_AVPO"].SetColor(Color.Blue);

			grd라인.Styles.Add("BAD_STOCK").ForeColor = Color.Red;
			grd라인.Styles.Add("TOOLTIP").Font = new Font(grd라인.Cols[0].Style.Font, FontStyle.Underline);
			grd라인.LoadUserCache("P_CZ_SA_QTN");

			// 기자재 OR 선용
			if (Quotation.부품영업)
			{
				grd라인.컬럼숨기기("UM_MIN", "RT_MARGIN_MIN");
			}
			else
			{
				grd라인.컬럼숨기기("UM_STD");
			}

			// *********** 가로
			grd가로.BeginSetting(1, 1, false);

			grd가로.SetCol("NO_FILE"			, "파일번호"	, false);
			grd가로.SetCol("NO_LINE"			, "항번"		, false);
			grd가로.SetCol("NO_DSP"			, "순번"		, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd가로.SetCol("NM_SUBJECT"		, "주제"		, false);
			grd가로.SetCol("CD_ITEM_PARTNER"	, "품목코드"	, 130);
			grd가로.SetCol("NM_ITEM_PARTNER"	, "품목명"	, 250);
			grd가로.SetCol("CD_ITEM"			, "재고코드"	, 90);
			grd가로.SetCol("NM_ITEM"			, "재고명"	, 250);
			grd가로.SetCol("UNIT"			, "단위"		, 45	, TextAlignEnum.CenterCenter);
			grd가로.SetCol("QT"				, "수량"		, 50	, typeof(decimal), FormatTpType.QUANTITY);

			grd가로.SetDataMap("UNIT", CODE.Unit(), "CODE", "NAME");

			grd가로.SetDefault("21.02.01.03", SumPositionEnum.Top);
			grd가로.SetAlternateRow();
			grd가로.SetMalgunGothic();
			SetGrid.Selected(grd가로);			

			// ---------------------------------------------------------------------------------------------------- 세로
			grd세로.BeginSetting(1, 1, false);
			
			grd세로.SetCol("NO_FILE"			, "파일번호"		, false);
			grd세로.SetCol("NO_LINE"			, "고유번호"		, false);
			grd세로.SetCol("NO_DSP"			, "순번"			, 40	, false);
			grd세로.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd세로.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 110	, false);
			grd세로.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200	, false);
			grd세로.SetCol("NM_UNIT"			, "단위"			, 45	, false);
			grd세로.SetCol("QT"				, "수량"			, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd세로.SetCol("CD_PARTNER"		, "매입처코드"	, false);
			grd세로.SetCol("LN_PARTNER"		, "매입처명"		, 170);
			grd세로.SetCol("UM_KR"			, "매입단가(￦)"	, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd세로.SetCol("AM_KR"			, "매입금액(￦)"	, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd세로.SetCol("LT"				, "납기"			, 50	, false	, typeof(decimal), FormatTpType.MONEY);
			grd세로.SetCol("DC_RMK"			, "비고"			, 100);
			grd세로.SetCol("YN_CHOICE"		, "선택"			, false);
			
			grd세로.Cols["NO_DSP"].Format = "####.##";
			grd세로.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd세로.Cols["NM_UNIT"].TextAlign = TextAlignEnum.CenterCenter;

			grd세로.SetDefault("19.05.14.01", SumPositionEnum.None);
			//grd세로.SettingVersion = "17.08.25.01";
			//grd세로.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			//grd세로.Rows.DefaultSize = 22;
			//grd세로.Rows[0].Height = 28;
			SetGrid.Selected(grd세로);			

			// ==================================================================================================== 매입처선택(세로) - 매입처
			grd매입처.BeginSetting(1, 1, false);
			
			grd매입처.SetCol("NO_FILE"	, "파일번호"		, false);
			grd매입처.SetCol("CD_PARTNER"	, "매입처코드"	, false);
			grd매입처.SetCol("LN_PARTNER"	, "매입처명"		, 170);
			grd매입처.SetCol("CD_EXCH"	, "통화"			, 40);
			grd매입처.SetCol("RT_EXCH"	, "환율"			, false);
			grd매입처.SetCol("AM_EX_STD"	, "매입견적금액"	, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd매입처.SetCol("RT_DC"		, "DC(%)"		, 50	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd매입처.SetCol("AM_DC"		, "DC금액"		, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd매입처.SetCol("AM_EX"		, "매입금액"		, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd매입처.SetCol("AM_KR"		, "매입금액(￦)"	, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd매입처.SetCol("LT"			, "납기"			, 50	, false, typeof(decimal), FormatTpType.MONEY);
			grd매입처.SetCol("DC_RMK_QTN"	, "비고"			, 200);
				
			grd매입처.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			grd매입처.Cols["CD_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grd매입처.SetDataMap("CD_EXCH", GetDb.Code("MA_B000005"), "CODE", "NAME");
								
			grd매입처.SettingVersion = "17.08.25.02";
			grd매입처.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			grd매입처.Rows.DefaultSize = 22;
			grd매입처.Rows[0].Height = 28;

			grd매입처.Cols["CD_EXCH"].Visible = false;
			grd매입처.Cols["AM_EX_STD"].Visible = false;
			grd매입처.Cols["RT_DC"].Visible = false;
			grd매입처.Cols["AM_DC"].Visible = false;
			grd매입처.Cols["AM_EX"].Visible = false;
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			Header.ControlValueChanged += new FreeBindingEventHandler(Header_ControlValueChanged);

			btn갈매기.Click += new EventHandler(btn갈매기_Click);

			btn첨부선택.Click += Btn첨부선택_Click;
			btn메일발송.Click += new EventHandler(Btn메일발송_Click);
			btn워크전달.Click += Btn워크전달_Click;
			btn견적제출.Click += Btn견적제출_Click;

			btn표준단가.Click += new EventHandler(btn표준단가_Click);

			btn이윤계산.Click += new EventHandler(btn이윤계산_Click);
			btn기부속연동.Click += new EventHandler(btn기부속연동_Click);
			btn가격우선.Click += new EventHandler(btn우선_Click);
			btn납기우선.Click += new EventHandler(btn우선_Click);
			btn선택완료.Click += new EventHandler(btn선택완료_Click);

			dtp견적일자.CalendarClosed += new EventHandler(dtp견적일자_CalendarClosed);
			cbo통화.SelectionChangeCommitted += new EventHandler(cbo통화명_SelectionChangeCommitted);
			ctx수주형태.QueryBefore += new BpQueryHandler(ctx수주형태_QueryBefore);
			cbo원산지.SelectionChangeCommitted += new EventHandler(cbo원산지_SelectionChangeCommitted);
			cbo인도기간.SelectionChangeCommitted += new EventHandler(cbo인도기간_SelectionChangeCommitted);

			btn환율.Click += new EventHandler(btn환율_Click);
			btn표시형식.Click += new EventHandler(btn표시형식_Click);
			btn이윤율.Click += new EventHandler(btn이윤율_Click);
			btnDC율.Click += new EventHandler(btnDC율_Click);
			btn납기.Click += new EventHandler(btn납기_Click);
			btn부대비용추가.Click += new EventHandler(btn부대비용추가_Click);
			btn부대비용삭제.Click += new EventHandler(btn부대비용삭제_Click);

			grd라인.DoubleClick += new EventHandler(flexGrid_DoubleClick);
			grd가로.DoubleClick += new EventHandler(flexGrid_DoubleClick);
			grd세로.DoubleClick += new EventHandler(flexGrid_DoubleClick);
			grd매입처.DoubleClick += new EventHandler(flexGrid_DoubleClick);

			grd라인.AfterRowChange += new RangeEventHandler(grd라인_AfterRowChange);
			grd라인.KeyDown += new KeyEventHandler(grd라인_KeyDown);
			grd라인.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);
			grd라인.MouseHoverCell += new EventHandler(grd라인_MouseHoverCell);
			grd라인.MouseLeave += new EventHandler(grd라인_MouseLeave);
			grd라인.DoubleClick += Grd라인_DoubleClick;

			grd가로.BeforeDoubleClick += new BeforeMouseDownEventHandler(grd가로_BeforeDoubleClick);
			
			offer = new H_CZ_VIEW_OFFER_RATE();
			popOffer = new Popup(offer);
			popOffer.AutoClose = false;

			pnl오퍼이윤율.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnCountAText.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnCountAValue.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnMarginAText.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnMarginAValue.Click += new EventHandler(pnlOfferRate_Click);

			lblQtnCountCText.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnCountCValue.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnMarginCText.Click += new EventHandler(pnlOfferRate_Click);
			lblQtnMarginCValue.Click += new EventHandler(pnlOfferRate_Click);


			cbo포장형태.SelectionChangeCommitted += Cbo포장형태_SelectionChangeCommitted;
			tab메인.SelectedIndexChanged += new EventHandler(tab메인_SelectedIndexChanged);

			grd라인.StartEdit += Grd라인_StartEdit;
		}

		private void Grd라인_StartEdit(object sender, RowColEventArgs e)
		{
			string colName = grd라인.Cols[grd라인.Col].Name;

			// 편집 불가일 경우 막음
			if (grd라인[e.Row, "YN_MARGIN_EDIT"].ToString() == "N")
			{
				if (colName.In("RT_PROFIT", "UM_EX_Q", "RT_DC", "UM_EX_S"))
				{
					UT.ShowMsg("이윤 고정된 항목입니다. 편집할 수 없습니다.");
					e.Cancel = true;
				}
			}
		}
		


		private void Cbo포장형태_SelectionChangeCommitted(object sender, EventArgs e)
		{
			string str = "";

			if (LoginInfo.CompanyCode.In("K100", "S100"))
				str = "* IN CASE WOODEN BOX OR PALLET IS NEEDED TO AVOID GOODS DAMAGE FOR EXPORT DELIVERY, EXTRA ACTUAL COST CAN BE CHARGED";
			else if (LoginInfo.CompanyCode.포함("K200", "K300"))
				str = "EXTRA ACTUAL COST CAN BE CHARGED LATER IN CASE A WOODEN OR PALLET IS REQUIRED FOR AN EXPORT PACKING.";

			if (cbo포장형태.GetValue("CD_FLAG1") == "Y")
				tbx비고.Text = (tbx비고.Text.Replace(str, "").Trim() + "\r\n" + str).Trim();
			else
				tbx비고.Text = tbx비고.Text.Replace(str, "").Trim();

			Header.CurrentRow["TP_PACKING"] = cbo포장형태.GetValue();
			Header.CurrentRow["DC_RMK_QTN"] = tbx비고.Text;
			Header_ControlValueChanged(tbx포커스, null);
		}

		private void tab메인_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab세로)
				SetGridStyle(grd세로);
		}

		private void Header_ControlValueChanged(object sender, FreeBindingArgs e)
		{
			(Parent.GetContainerControl() as PageBase).ToolBarSaveButtonEnabled = true;
			//Quotation.Header_ControlValueChanged(sender, e);
		}

		private void btn갈매기_Click(object sender, EventArgs e)
		{
			bool popupDebug = false;

			if (Control.ModifierKeys == Keys.Control)
				popupDebug = true;

			// 엑셀파일 선택
			OpenFileDialog f = new OpenFileDialog();
			f.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

			if (f.ShowDialog() != DialogResult.OK)
				return;

			// 엑셀업로드
			try
			{
				MsgControl.ShowMsg(DD("엑셀 업로드 중입니다."));

				// 엑셀 → DataTable 변환
				string fileName = f.FileName;
				ExcelReader excelReader = new ExcelReader();
				DataTable dtExcel = excelReader.Read(fileName, 1, 2);

				// 컬럼이름 변경
				dtExcel.Columns["파일번호"].ColumnName = "NO_FILE";
				dtExcel.Columns["파일항번"].ColumnName = "NO_LINE";
				dtExcel.Columns["갈매기"].ColumnName = "YN_GULL";

				DataTable dtGull = dtExcel.DefaultView.ToTable(false, "NO_FILE", "NO_LINE", "YN_GULL");
				DBMgr.GetDataTable("PX_CZ_SA_QTN_GULL_UPLOAD", false, popupDebug, GetTo.Xml(dtGull));

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}     

		private void Btn첨부선택_Click(object sender, EventArgs e)
		{
			string[] vendorCodes = grd라인.DataTable.AsEnumerable().Select(row => row.Field<string>("CD_VENDOR")).Distinct().ToArray();
			Util.SaveReferenceFile(Quotation.파일번호, vendorCodes);
		}

		private void Btn메일발송_Click(object sender, EventArgs e)
		{
			//string companyCode = Quotation.회사코드;
			//string bcc = "";

			//// ********** 인터컴퍼니 체크
			//string query = @"
			//SELECT
			//	C.CD_COMPANY
			//,	C.NO_EMP
			//,	D.NO_EMAIL	-- 현재 담당자 이메일 (인터컴퍼니 된 담당자 말고 실제 담당자)
			//FROM CZ_SA_QTNH			AS A
			//JOIN CZ_SA_INTERCOMPANY	AS B ON A.CD_COMPANY = B.CD_COMPANY AND LEFT(A.NO_FILE, 2) = B.CD_PREFIX
			//JOIN CZ_SA_QTNH			AS C ON B.CD_COMPANY_SO = C.CD_COMPANY AND A.NO_FILE = C.NO_FILE
			//JOIN MA_EMP				AS D ON A.CD_COMPANY = D.CD_COMPANY AND A.NO_EMP = D.NO_EMP
			//WHERE 1 = 1
			//	AND A.CD_COMPANY = '" + Quotation.회사코드 + @"'
			//	AND A.NO_FILE = '" + Quotation.FileNumber + "'";

			//DataTable dtIC = SQL.GetDataTable(query);

			//if (dtIC.Rows.Count > 0)
			//{
			//	// 인터컴퍼니 회사, 담당자 변경
			//	companyCode = dtIC.Rows[0]["CD_COMPANY"].ToString();
			//	bcc = dtIC.Rows[0]["NO_EMAIL"].ToString();  // 인터컴퍼니 경우 현재 담당자가 숨은참조가 됨
			//}

			// ********** 팝업 
			//H_CZ_MAIL_OPTION G = new H_CZ_MAIL_OPTION(companyCode, MailType.Quotation, Quotation.FileNumber, Quotation.EmpNumber, bcc, Quotation.회사코드);
			//G.ShowDialog();




			//MAIL_OPTION f = new MAIL_OPTION(MAIL_TYPE.Quotation)
			//{
			//	회사코드		= companyCode
			//,	원래회사코드	= Quotation.회사코드
			//,	파일번호		= Quotation.FileNumber
			//,	숨은참조		= bcc
			//};




			//if (f.ShowDialog() == DialogResult.OK)
			//{
			//	DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "04", Quotation.FileNumber, LoginInfo.EmployeeNo);	// 매입가등록
			//	DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "05", Quotation.FileNumber, LoginInfo.EmployeeNo);	// 견적작성
			//	DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "21", Quotation.FileNumber, LoginInfo.EmployeeNo);	// 견적요청
			//}




			if (new MAIL_OPTION(Quotation.회사코드, Quotation.파일번호, MAIL_TYPE.Quotation).ShowDialog() == DialogResult.OK)
			{
				DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "04", Quotation.파일번호, LoginInfo.EmployeeNo);   // 매입가등록
				DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "05", Quotation.파일번호, LoginInfo.EmployeeNo);   // 견적작성
				DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "21", Quotation.파일번호, LoginInfo.EmployeeNo);   // 견적요청
			}
		}

		private void Btn워크전달_Click(object sender, EventArgs e)
		{
			string workRemark = "";			

			if (DBMgr.GetDataTable("SELECT * FROM FT_CZ_MA_WORKFLOWL('" + Quotation.회사코드 + "', '" + Quotation.파일번호 + "') WHERE TP_STEP = '58'").Rows.Count > 0)
				workRemark = "첨부파일 유첨";

			DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "04", Quotation.파일번호, LoginInfo.EmployeeNo);					// 매입가등록
			DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "05", Quotation.파일번호, LoginInfo.EmployeeNo);					// 견적작성
			DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_DONE", Quotation.회사코드, "21", Quotation.파일번호, LoginInfo.EmployeeNo);					// 견적요청
			DBMgr.ExecuteNonQuery("SP_CZ_MA_WORKFLOW_NEXT_STEP", Quotation.회사코드, "06", Quotation.파일번호, LoginInfo.EmployeeNo, workRemark);   // 견적제출
			ShowMessage(PageResultMode.SaveGood);
		}

		private void Btn견적제출_Click(object sender, EventArgs e)
		{
			btn메일발송.Enabled = true;
			btn워크전달.Enabled = true;
		}


		private void btn표준단가_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (GetTo.Decimal(grd라인[i, "UM_EX_P"]) == 0)
				{
					decimal price = GetTo.Decimal(grd라인.GetDataDisplay(i, "UM_STD"));

					grd라인[i, "UM_EX_P"] = price;
					grd라인[i, "AM_EX_P"] = price * GetTo.Decimal(grd라인[i, "QT_QTN"]);
					grd라인[i, "UM_KR_P"] = price;
					grd라인[i, "AM_KR_P"] = price * GetTo.Decimal(grd라인[i, "QT_QTN"]);
					grd라인[i, "YN_BASE"] = "Y";

					행계산(i, "");
				}
			}
		}

		private void btn이윤계산_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				행계산(i, "UM_EX_S");
			}
		}

		private void btn기부속연동_Click(object sender, EventArgs e)
		{
			DataTable dtL = grd라인.DataTable.Select("ISNULL(CD_ITEM, '') <> ''").CopyToDataTable();
			dtL.Columns.Add("NO_IMO", typeof(string), "'" + Quotation.Imo번호 + "'");

			string xmlL = Util.GetTO_Xml(dtL, RowState.Modified);
			DBMgr.ExecuteNonQuery("SP_CZ_MA_ENGINE_PARTS_REG_XML", new object[] { xmlL });
			ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void btn우선_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab가로)
			{
				string tag = "";
				if (((RoundedButton)sender).Name.IndexOf("가격") > 0) tag = "_AM";
				if (((RoundedButton)sender).Name.IndexOf("납기") > 0) tag = "_LT";

				for (int i = grd가로.Rows.Fixed; i < grd가로.Rows.Count; i++)
				{
					int COL_YN_MIN = -1;
					int COL_AM_MIN = -1;
					int COL_VALUE_MIN = -1;
					decimal VALUE_MIN = 100000000000;

					// 거래처별 최소값 비교
					foreach (string CD_PARTNER in VendorCodes)
					{
						int COL_YN = grd가로.Cols[CD_PARTNER + "_YN"].Index;
						int COL_AM = grd가로.Cols[CD_PARTNER + "_AM"].Index;
						int COL_VALUE = grd가로.Cols[CD_PARTNER + tag].Index;
						decimal VALUE = GetTo.Decimal(grd가로[i, COL_VALUE]);

						if (VALUE != 0 && VALUE < VALUE_MIN && CD_PARTNER != "STOCK")	// 재고거래처는 선택안되도록 함
						{
							COL_YN_MIN = COL_YN;
							COL_AM_MIN = COL_AM;
							COL_VALUE_MIN = COL_VALUE;
							VALUE_MIN = VALUE;
						}

						// 모든 컬럼 체크 False
						grd가로[i, COL_YN] = "N";
						grd가로.SetCellStyle(i, COL_AM, "");
					}

					// 선택된 컬럼 체크 True
					if (COL_YN_MIN > 0)
					{
						grd가로[i, COL_YN_MIN] = "Y";
						grd가로.SetCellStyle(i, COL_AM_MIN, "SELECTED");
					}
				}
			}
			else if (tab메인.SelectedTab == tab세로)
			{
				string colname = "";
				if (((RoundedButton)sender).Name.IndexOf("가격") > 0) colname = "AM_KR";
				if (((RoundedButton)sender).Name.IndexOf("납기") > 0) colname = "LT";

				int i = grd세로.Rows.Fixed;

				while (i < grd세로.Rows.Count)
				{
					int NO_LINE = GetTo.Int(grd세로[i, "NO_LINE"]);
					int row_min = -1;
					decimal value_min = (colname == "AM_KR") ? 100000000000 : 990;

					// 단독 업체이고 단가가 있을 경우
					if (grd세로.DataTable.Select("NO_LINE = " + NO_LINE).Length == 1 && GetTo.Decimal(grd세로[i, colname]) > 0 && GetTo.Decimal(grd세로[i, colname]) < value_min)
					{
						SetSelectedStatus(i, "Y");
						i++;
						continue;
					}
									
					// 복수 업체인 경우
					while (NO_LINE == GetTo.Int(grd세로[i, "NO_LINE"]))
					{
						// 모든 업체 선택안함 체크
						SetSelectedStatus(i, "N");

						// 값 비교
						decimal value_cur = GetTo.Decimal(grd세로[i, colname]);

						if (value_cur > 0 && value_cur < value_min)
						{
							row_min = i;
							value_min = value_cur;
						}

						i++;
					}

					// row 값이 있는 경우 선택
					if (row_min > 0) SetSelectedStatus(row_min, "Y");
				}
			}
		}

		private void btn선택완료_Click(object sender, EventArgs e)
		{
			SaveSupplier();	// 저장			
			Search();		// 조회

			// 매입관련 컬럼들을 NEW 컬럼으로 값 변경
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (GetTo.Int(grd라인[i, "NO_LINE"]) < 90000)
				{
					//grd라인[i, "CD_ITEM"]	  = grd라인[i, "CD_ITEM_P_SEL"];
					grd라인[i, "CD_VENDOR"]	  = grd라인[i, "CD_VENDOR_SEL"];
					grd라인[i, "NM_VENDOR"]   = grd라인[i, "NM_VENDOR_SEL"];
					grd라인[i, "UM_EX_STD_P"] = grd라인[i, "UM_EX_STD_P_SEL"];
					grd라인[i, "AM_EX_STD_P"] = grd라인[i, "AM_EX_STD_P_SEL"];
					grd라인[i, "RT_DC_P"]	  = grd라인[i, "RT_DC_P_SEL"];		
					grd라인[i, "UM_EX_P"]	  = grd라인[i, "UM_EX_P_SEL"];
					grd라인[i, "AM_EX_P"]	  = grd라인[i, "AM_EX_P_SEL"];
					grd라인[i, "UM_KR_P"]	  = grd라인[i, "UM_KR_P_SEL"];
					grd라인[i, "AM_KR_P"]	  = grd라인[i, "AM_KR_P_SEL"];
					grd라인[i, "LT"]		  = grd라인[i, "LT_P_SEL"];
					grd라인[i, "DC_RMK"]	  = grd라인[i, "DC_RMK_P_SEL"];
				}
			}

			// ********** 자동 마진
			// DX마진이 있는 경우 계산함
			



			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{				
				// 고정마진
				if (grd라인[i, "RT_MARGIN_FIX"].문자() != "")
				{
					grd라인[i, "RT_PROFIT"] = grd라인[i, "RT_MARGIN_FIX"].정수() + DcDefault + DcTO;
					행계산(i, "RT_PROFIT");
				}
			}





			tab메인.SelectedTab = tab라인;	// 탭이동
			합계계산();	

			// 오퍼이윤율
			pnlOfferRate_Click(null, null);
		}

		private void dtp견적일자_CalendarClosed(object sender, EventArgs e)
		{
			cur환율.DecimalValue = GetDb.ExchangeRate(dtp견적일자.Text, cbo통화.GetValue(), "S");
		}

		private void cbo통화명_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cur환율.DecimalValue = Util.GetDB_EXCHANGE(dtp견적일자.Text, cbo통화.SelectedValue, "S");
			if (cbo통화.SelectedValue.ToString() == "000")
			{
				cbo표시형식.SelectedValue = "7";
				rdo표시형식단가.Checked = true;
			}
			else
			{
				cbo표시형식.SelectedValue = "3";
				rdo표시형식금액.Checked = true;
			}
			Header.CurrentRow["RT_EXCH"] = cur환율.DecimalValue;
			Header.CurrentRow["TP_DIGIT"] = cbo표시형식.SelectedValue;
		}

		private void ctx수주형태_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P61_CODE1 = "N";
			e.HelpParam.P62_CODE2 = "Y";
		}

		private void cbo원산지_SelectionChangeCommitted(object sender, EventArgs e)
		{
			Header.CurrentRow["ORIGIN"] = cbo원산지.GetText();
			Header_ControlValueChanged(tbx포커스, null);
		}

		private void cbo인도기간_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (!grd라인.HasNormalRow) return;

			int min = GetTo.Int(grd라인.DataTable.Compute("MIN(LT)", ""));
			int max = GetTo.Int(grd라인.DataTable.Compute("MAX(LT)", ""));
			string minDay = (min == 0) ? "STOCK" : string.Format("{0} DAYS", min);
			string maxDay = (max == 0) ? "STOCK" : string.Format("{0} DAYS", max);
			string minDate = (min == 0) ? "STOCK" : Util.GetToDatePrint(dtp견적일자.ToDayDate.AddDays(min));
			string maxDate = (max == 0) ? "STOCK" : Util.GetToDatePrint(dtp견적일자.ToDayDate.AddDays(max));

			if (cbo인도기간.SelectedIndex == 1) tbx인도기간.Text = "AS BELOW";
			else if (cbo인도기간.SelectedIndex == 2) tbx인도기간.Text = minDay + " ~ " + maxDay;
			else if (cbo인도기간.SelectedIndex == 3) tbx인도기간.Text = minDate + " ~ " + maxDate;
			else if (cbo인도기간.SelectedIndex == 4) tbx인도기간.Text = maxDay;
			else if (cbo인도기간.SelectedIndex == 5) tbx인도기간.Text = maxDate;
		}

		private void grd라인_MouseLeave(object sender, EventArgs e)
		{
			toolTip.RemoveAll();
		}

		private void grd라인_MouseHoverCell(object sender, EventArgs e)
		{
			int row = grd라인.MouseRow;
			int col = grd라인.MouseCol;
			
			if (row >= grd라인.Rows.Fixed && col == grd라인.Cols["UM_STD"].Index)
			{
				string tip;
				tip = GetTo.String(grd라인[row, "UM_STD_MULTI"]);

				toolTip.AutoPopDelay = 30000;
				toolTip.ShowAlways = true;
				toolTip.SetToolTip(grd라인, tip);
			}
		}

		private void Grd라인_DoubleClick(object sender, EventArgs e)
		{
			// 헤더 클릭
			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				SetGridStyle((FlexGrid)sender);
				return;
			}

			// 첨부파일 아이콘 클릭
			string colName = grd라인.Cols[grd라인.Col].Name;

			if (colName.In("FILE_ICON") && grd라인["FILE_LIST"].ToString() == "Y")
			{
				if (CT.String(grd라인["FILE_LIST"]) ==  "Y")
				{
					string companyCode = Quotation.회사코드;
					string itemCode = grd라인["CD_ITEM"].ToString();
					string query = @"
	  SELECT 'N' AS CHK, IMAGE1 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE1, '') != ''
UNION SELECT 'N' AS CHK, IMAGE2 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE2, '') != ''
UNION SELECT 'N' AS CHK, IMAGE3 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE3, '') != ''
UNION SELECT 'N' AS CHK, IMAGE4 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE4, '') != ''
UNION SELECT 'N' AS CHK, IMAGE5 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE5, '') != ''
UNION SELECT 'N' AS CHK, IMAGE6 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE6, '') != ''
UNION SELECT 'N' AS CHK, IMAGE7 AS FILE_NAME FROM CZ_MA_PITEM_FILE WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND ISNULL(IMAGE7, '') != ''
";

					SQL sql = new SQL(query, SQLType.Text);
					sql.Parameter.Add2("@CD_COMPANY", companyCode);
					sql.Parameter.Add2("@CD_ITEM"	, itemCode);

					DataTable dt = sql.GetDataTable();
					dt.Columns.Add("FILE_TYPE", typeof(string), "'P'");
					dt.Columns.Add("FILE_PATH", typeof(string), @"'Upload\P_CZ_MA_PITEM\" + companyCode + @"\" + itemCode + "'");

					H_CZ_FILE_LIST f = new H_CZ_FILE_LIST(dt) { Checkable = true };

					if (f.ShowDialog() == DialogResult.OK)
					{
						if (f.SelectedItem == null)
							grd라인["REFERENCE"] = "";
						else
							grd라인["REFERENCE"] = string.Join("|", f.SelectedItem.AsEnumerable().Select(x => x["FILE_PATH"].ToString() + @"\" + x["FILE_NAME"].ToString()).ToArray());
					}
				}
			}
		}


		H_CZ_VIEW_OFFER_RATE offer;
		Popup popOffer;

		private void pnlOfferRate_Click(object sender, EventArgs e)
		{
			offer.CompanyCode = Quotation.회사코드;
			offer.FileNumber = Quotation.파일번호;//SB17004023,DB17006997
			offer.PartnerCode = Quotation.매출처코드;
			offer.PartnerName = Quotation.매출처이름;
			offer.Bind();

			popOffer.Show((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl(), 10, 10);
		}

		

		

		

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		

		private void btn환율_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				행계산(i, "RT_EXCH");
		}

		private void btn표시형식_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				행계산(i, "CD_DSP");
		}

		private void btn이윤율_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++) 
			{
				if (GetTo.Int(grd라인[i, "NO_LINE"]) < 90000 && GetTo.Decimal(grd라인[i, "EZPROFIT"]) == 0 && grd라인[i, "YN_MARGIN_EDIT"].ToString() != "N")
				{
					grd라인[i, "RT_PROFIT"] = cur이윤율.DecimalValue;
					행계산(i, "RT_PROFIT");
				}				
				else if (GetTo.Decimal(grd라인[i, "EZPROFIT"]) != 0)
				{
					if (GetTo.Decimal(grd라인[i, "UM_EX_S"]) == 0)
					{
						행계산(i, "RT_PROFIT");
					}
				}
			}
		}

		private void btnDC율_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++) 
			{
				if (GetTo.Int(grd라인[i, "NO_LINE"]) < 90000)
				{
					grd라인[i, "RT_DC"] = curDC율.DecimalValue;
					행계산(i, "RT_DC");
				}
			}
		}

		private void btn납기_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (GetTo.Int(grd라인[i, "NO_LINE"]) < 90000)
				{
					grd라인[i, "LT"] = cur납기.DecimalValue;
				}
			}
		}

		private void btn부대비용추가_Click(object sender, EventArgs e)
		{
			if (GetCon.Value(cbo부대비용) == "") { ShowMessage("부대비용을 선택하세요!"); return; }

			DataRow[] row = 그리드라인.DataTable.Select("CD_ITEM = '" + GetCon.Value(cbo부대비용) + "'");
			if (row.Length > 0) { ShowMessage("이미 추가되었습니다."); return; }

			int NO_LINE = 90001;
			int NO_LINE_MAX = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");
			if (NO_LINE_MAX > 90000) NO_LINE = NO_LINE_MAX + 1;

			grd라인.Rows.Add();
			grd라인.Row = grd라인.Rows.Count - 1;
			grd라인["CD_COMPANY"] = Quotation.회사코드;
			grd라인["NO_FILE"] = Quotation.파일번호;
			grd라인["NO_LINE"] = NO_LINE;
			grd라인["NM_ITEM_PARTNER"] = GetCon.Text(cbo부대비용);
			grd라인["CD_ITEM"] = GetCon.Value(cbo부대비용);
			grd라인["NM_ITEM"] = GetCon.Text(cbo부대비용);
			grd라인["QT"] = 1;
			grd라인["QT_QTN"] = 1;
			grd라인["RT_DC"] = 0;
			grd라인["LT"] = 0;
			grd라인["TP_BOM"] = "S";
			grd라인["YN_DSP_RMK"] = "N";
			grd라인["YN_GULL"] = "N";
			grd라인.AddFinished();

			grd라인.Col = grd라인.Cols["UM_EX_Q"].Index;
			grd라인.Focus();
		}

		private void btn부대비용삭제_Click(object sender, EventArgs e)
		{
			if (GetCon.Value(cbo부대비용) == "") { ShowMessage("부대비용을 선택하세요!"); return; }

			DataRow[] row = grd라인.DataTable.Select("CD_ITEM = '" + GetCon.Value(cbo부대비용) + "'");
			if (row.Length > 0) row[0].Delete();
		}

		

		

		
		
		

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grd가로_BeforeDoubleClick(object sender, BeforeMouseDownEventArgs e)
		{
			int row = grd가로.HitTest(new Point(e.X, e.Y)).Row;

			// 헤더의 합계행 클릭
			if (row == 1)
			{
				e.Cancel = true;	// 일단 이벤트 취소 (Sort 이벤트 취소해야함)

				// 금액 필드일 경우 해당 매입처 전체 선택
				if (grd가로.AllowEditing && grd가로.Cols[grd가로.Col].Name.Right(3) == "_AM")
				{
					// 매입처 체크 → STOCK 인 경우는 선택 안되도록
					string supplierCode = grd가로.Cols[grd가로.Col].Name.Replace("_AM", "");

					if (supplierCode == "STOCK")
					{
						ShowMessage("STOCK은 선택할 수 없습니다.");
						return;
					}

					for (int i = grd가로.Rows.Fixed; i < grd가로.Rows.Count; i++)
					{
						// 금액이 0인 경우는 Pass
						if (GetTo.Decimal(grd가로[i, supplierCode + "_AM"]) == 0)
							continue;

						// 해당 매입처 선택, 나머지는 해제
						foreach (string code in VendorCodes)
						{
							int colYn = grd가로.Cols[code + "_YN"].Index;
							int colAm = grd가로.Cols[code + "_AM"].Index;

							if (code == supplierCode)
							{
								grd가로[i, colYn] = "";
								grd가로[i, colYn] = "Y";
								grd가로.SetCellStyle(i, colAm, "SELECTED");
							}
							else
							{
								grd가로[i, colYn] = "N";
								grd가로.SetCellStyle(i, colAm, "");
							}
						}
					}
				}
			}
		}

		private void flexGrid_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;
			int row = flexGrid.MouseRow;
			int col = flexGrid.MouseCol;
			
			// 헤더클릭
			if (row <= 0)
			{
				if (flexGrid == grd라인 || flexGrid == grd가로)
					SetGridStyle(flexGrid);
				return;
			}

			if (flexGrid == grd가로)
			{
				if (flexGrid.AllowEditing && flexGrid.Cols[col].Name.Right(3) == "_AM")
				{
					// 매입처 체크 → STOCK 인 경우는 선택 안되도록
					string supplierCode = flexGrid.Cols[col].Name.Replace("_AM", "");

					if (supplierCode == "STOCK")
					{
						ShowMessage("STOCK은 선택할 수 없습니다.");
						return;
					}

					foreach (string code in VendorCodes)
					{
						int j = grd가로.Cols[code + "_AM"].Index;

						if (code == supplierCode)
						{
							grd가로[code + "_YN"] = "Y";
							grd가로.SetCellStyle(row, j, "SELECTED");
						}
						else
						{
							grd가로[code + "_YN"] = "N";
							grd가로.SetCellStyle(row, j, "");
						}
					}
				}
			}
			else if (flexGrid == grd세로)
			{
				if (flexGrid.Cols[col].Name.In("CD_PARTNER", "LN_PARTNER", "UM_KR", "AM_KR", "LT", "DC_RMK"))
				{
					// 현재 선택 ROW 셀 변경
					SetSelectedStatus(row, "Y");

					// 앞뒤로 탐색해서 선택 취소
					int lineNumber = GetTo.Int(flexGrid["NO_LINE"]);

					for (int i = row - 1; i >= flexGrid.Rows.Fixed; i--)
					{
						if (lineNumber == GetTo.Int(flexGrid[i, "NO_LINE"]))
							SetSelectedStatus(i, "N");
						else
							break;
					}

					for (int i = row + 1; i < flexGrid.Rows.Count; i++)
					{
						if (lineNumber == GetTo.Int(flexGrid[i, "NO_LINE"]))
							SetSelectedStatus(i, "N");
						else
							break;
					}
				}
			}
			else if (flexGrid == grd매입처)
			{
				string partnerCode = GetTo.String(flexGrid["CD_PARTNER"]);

				// 매입처 체크 → STOCK 인 경우는 선택 안되도록
				if (partnerCode == "STOCK")
				{
					ShowMessage("STOCK은 선택할 수 없습니다.");
					return;
				}

				int i = grd세로.Rows.Fixed;

				while (i < grd세로.Rows.Count)
				{
					int lineNumber = GetTo.Int(grd세로[i, "NO_LINE"]);

					// 단독 업체이고 다른 업체면 Pass
					if (grd세로.DataTable.Select("NO_LINE = " + lineNumber).Length == 1 && partnerCode != GetTo.String(grd세로[i, "CD_PARTNER"]))
					{
						i++;
						continue;
					}

					// 복수 업체인 경우
					while (lineNumber == GetTo.Int(grd세로[i, "NO_LINE"]))
					{
						if (partnerCode == GetTo.String(grd세로[i, "CD_PARTNER"]))
							SetSelectedStatus(i, "Y");
						else
							SetSelectedStatus(i, "N");

						i++;
					}
				}
			}
		}
		
		private void grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			// 매출처 Offer 이윤율
			// 이윤율 표시
			//DataSet ds = DBMgr.GetDataSet("PS_CZ_SA_QTN_REG_OFFER_RATE", false, false, Quotation.회사코드, Quotation.PartnerCode, grd라인["CD_SUPPLIER"].ToString(), 180);
			////DataTable dt = ds.Tables[0];
			//DataRow[] rowsA = ds.Tables[0].Select("CD_ITEMGRP = '" + grd라인["GRP_ITEM"] + "'");
			//DataRow[] rowsC = ds.Tables[2].Select("CD_ITEMGRP = '" + grd라인["GRP_ITEM"] + "'");

			//if (rowsA.Length > 0)
			//{
			//	lblQtnCountAValue.Text = string.Format("{0:#,##0}건", rowsA[0]["CNT_QTN"]);
			//	lblQtnMarginAValue.Text = string.Format("{0:#,##0.00}%", rowsA[0]["RT_MARGIN_QTN"]);
			//}

			//if (rowsC.Length > 0)
			//{
			//	lblQtnCountCValue.Text = string.Format("{0:#,##0}건", rowsC[0]["CNT_QTN"]);
			//	lblQtnMarginCValue.Text = string.Format("{0:#,##0.00}%", rowsC[0]["RT_MARGIN_QTN"]);
			//}

			//offer.ChangeSupplier(grd라인["CD_SUPPLIER"].ToString());
		}

		private void grd라인_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				// 에디트 가능 컬럼만
				if (!grd라인.Cols[grd라인.Col].AllowEditing)
					return;

				// 붙여넣기 시작
				string[,] clipboard = Util.GetClipboardValues();

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					grd라인[grd라인.Row + i, grd라인.Col] = clipboard[i, 0];

					if (grd라인.Cols[grd라인.Col].Name.In("UM_EX_Q", "UM_EX_S"))
						행계산(grd라인.Row + i, grd라인.Cols[grd라인.Col].Name);

					if (grd라인.Row + i == grd라인.Rows.Count - 1)
						break;
				}
			}
		}

		private void grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			if (grd라인[e.Row, "YN_MARGIN_EDIT"].ToString() == "N")
			{
				//grd라인[e.Row, "RT_PROFIT"]
				//grd라인.EditData
			}
			

			if (grd라인.Cols[e.Col].Name != "YN_GULL") 행계산(e.Row, grd라인.Cols[e.Col].Name);

		}

		

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			((TabPage)Parent).ImageIndex = -1;
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			UT.ShowPgb("조회중입니다.");

			DataTable dtHead = SQL.GetDataTable("PS_CZ_SA_QTN_H_R3", sqlDebug, Quotation.회사코드, Quotation.파일번호);
			DataTable dtLine = SQL.GetDataTable("PS_CZ_SA_QTN_L_6", sqlDebug, Quotation.회사코드, Quotation.파일번호);

			// 견적서 발송 방법에 따른 버튼 권한
			btn메일발송.Enabled = false;
			btn워크전달.Enabled = false;
			string cdQtn = (string)dtHead.Rows[0]["CD_QTN"];

			if (cdQtn.In("EML"))
				btn메일발송.Enabled = true;
			else if (cdQtn.In("SRV", "PTL"))
				btn워크전달.Enabled = true;
			//else if (cdQtn == "EML")
			//	btn메일발송.Enabled = true;

			string query = "";

			// ---------------------------------------------------------------------------------------------------- 헤드
			#region ********** 매출처 기본값 설정 (수주등록이 안되어 있을 경우만)

			if (dtHead.Rows[0]["NO_SO"].ToString() == "")
			{
				// 견적일자, 유효일자
				if (dtHead.Rows[0]["DT_QTN"].ToString() == "")
				{
					dtHead.Rows[0]["DT_QTN"] = Util.GetToday();
					dtHead.Rows[0]["DT_VALID"] = Util.GetToday(30);
				}

				// 환율정보
				if (dtHead.Rows[0]["CD_EXCH"].ToString() == "")
				{					
					// 매입처 리스트중에 기준환율 쓰는 매입처가 있으면 매출 환율도 기준환율 사용함
					query = @"
SELECT
	1
FROM CZ_PU_QTNH		AS A
JOIN CZ_MA_CODEDTL	AS B ON A.CD_COMPANY = B.CD_COMPANY AND B.CD_FIELD = 'CZ_SA00001' AND A.CD_PARTNER = B.CD_SYSDEF
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND A.NO_FILE = '" + Quotation.파일번호 + "'";

					string mode = (DBMgr.GetDataTable(query).Rows.Count > 0) ? "B" : "S";

					dtHead.Rows[0]["CD_EXCH"] = dtHead.Rows[0]["CD_EXCH_DEFAULT"];
					dtHead.Rows[0]["RT_EXCH"] = GetDb.ExchangeRate(dtHead.Rows[0]["DT_QTN"], dtHead.Rows[0]["CD_EXCH"], mode);
				}

				// 수주형태
				if (dtHead.Rows[0]["TP_SO"].ToString() == "")
				{
					dtHead.Rows[0]["TP_SO"] = dtHead.Rows[0]["TP_SO_DEFAULT"];
					dtHead.Rows[0]["NM_SO"] = dtHead.Rows[0]["NM_SO_DEFAULT"];
				}
				
				// 인도기간
				if (dtHead.Rows[0]["COND_TRANS"].ToString() == "")
				{
					dtHead.Rows[0]["COND_TRANS"] = "AS BELOW";
				}

				// ***** 지불조건
				// 경고마스터 사용불가 레벨이면 CASH 고정
				WarningLevel 경고레벨 = WarningLevel.정상;
				string 경고메세지 = "";
				string 제외여부 = "";
				string 지불조건제외여부 = "";
				EalryWarningSystem ews = new EalryWarningSystem();
				ews.미수금확인(Quotation.매출처코드, ref 경고레벨, ref 경고메세지, ref 제외여부, ref 지불조건제외여부);

				if (Quotation.회사코드 == "K100" && 경고레벨 == WarningLevel.사용불가 && 지불조건제외여부 == "N")
				{
					string str = @"* IN CASE OF CREDIT BLOCK DUE TO OUTSTANDING PAYMENTS, ALL SERVICES CAN BE PROVIDED BASED ON ""CASH-IN-ADVANCE"" TERMS.";

					if (!dtHead.Rows[0]["DC_RMK_QTN"].문자().발생(str))
						dtHead.Rows[0]["DC_RMK_QTN"] += ("\r\n" + str).트림();

					dtHead.Rows[0]["COND_PAY"] = "101";
					cbo지불조건.사용(false);
				}
				else if (dtHead.Rows[0]["COND_PAY"].ToString() == "")
				{
					dtHead.Rows[0]["COND_PAY"] = dtHead.Rows[0]["COND_PAY_DEFAULT"];
				}

				// 선적조건
				if (dtHead.Rows[0]["TP_TRANS"].ToString() == "")
				{
					if (IsEquipment)
					{
						dtHead.Rows[0]["TP_TRANS"] = dtHead.Rows[0]["TP_TRANS_DEFAULT"];
					}
					else
					{
						if (Quotation.파일번호.Left(2) == "NS")
							dtHead.Rows[0]["TP_TRANS"] = "FOB SHIPYARD";
						else
							dtHead.Rows[0]["TP_TRANS"] = "FOB";
					}

					dtHead.Rows[0]["PORT_LOADING"] = "BUSAN, KOREA"; 
				}

				// 포장형태
				if (dtHead.Rows[0]["TP_PACKING"].ToString() == "")
				{
					if (IsEquipment)
					{
						// 테크로스
						if (dtLine.Select("CD_VENDOR = '17747'").Length > 0)
							dtHead.Rows[0]["TP_PACKING"] = "007";						
					}
					else
					{
						dtHead.Rows[0]["TP_PACKING"] = "005";
					}					
				}

				// 과세구분
				if (dtHead.Rows[0]["TP_VAT"].ToString() == "")
				{
					dtHead.Rows[0]["TP_VAT"] = dtHead.Rows[0]["TP_VAT_DEFAULT"];
				}

				// 이윤율
				if (dtHead.Rows[0]["RT_PROFIT"].ToString() == "")
				{
					dtHead.Rows[0]["RT_PROFIT"] = dtHead.Rows[0]["RT_PROFIT_DEFAULT"];
				}

				// DC율
				if (dtHead.Rows[0]["RT_DC"].ToString() == "")
				{
					dtHead.Rows[0]["RT_DC"] = dtHead.Rows[0]["RT_DC_DEFAULT"];
				}

				// 표시형식
				if (dtHead.Rows[0]["TP_DIGIT"].ToString() == "")
				{

					dtHead.Rows[0]["TP_DIGIT"] = (GetTo.String(dtHead.Rows[0]["CD_EXCH"]) == "000") ? "7" : "3";
					dtHead.Rows[0]["TP_ROUND"] = (GetTo.String(dtHead.Rows[0]["CD_EXCH"]) == "000") ? "1" : "2";
				}
			}

			// 기본DC, 커미션DC 저장 (다른데서 씀)
			DcDefault = dtHead.Rows[0]["RT_DC_DEFAULT"].ToDecimal();
			DcTO = dtHead.Rows[0]["RT_TURNOVER_DC"].ToDecimal();

			#endregion

			#region ********** 원산지 콤보 바인딩
			query = @"
;WITH #ORIGION AS
(
	SELECT
		  CASE WHEN C.CD_ORIGIN IS NOT NULL 
			THEN 'A' 
			ELSE 'B'
		  END			AS CODE
		, B.NM_ORIGIN	AS NAME
	FROM
	(
		SELECT
			  CD_COMPANY
			, CD_PARTNER
		FROM CZ_PU_QTNL WITH(NOLOCK)
		WHERE 1 = 1
			AND CD_COMPANY = '" + Quotation.회사코드 + @"'
			AND NO_FILE = '" + Quotation.파일번호 + @"'
			AND YN_CHOICE = 'Y'
		GROUP BY CD_COMPANY, CD_PARTNER
	)								AS A
	JOIN	  CZ_MA_PARTNER_ORIGIN	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
	LEFT JOIN CZ_MA_PARTNER			AS C WITH(NOLOCK) ON B.CD_COMPANY = C.CD_COMPANY AND B.CD_PARTNER = C.CD_PARTNER AND B.CD_ORIGIN = C.CD_ORIGIN
	GROUP BY B.NM_ORIGIN, C.CD_ORIGIN
)

SELECT * 
FROM #ORIGION

UNION ALL

SELECT
	  'C'
	, ORIGIN + ' (" + DD("사용불가") + @")'
FROM CZ_SA_QTNH WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND NO_FILE = '" + Quotation.파일번호 + @"'
	AND ISNULL(ORIGIN, '') != ''
	AND ORIGIN NOT IN (SELECT NAME FROM #ORIGION)

ORDER BY NAME";

				DataTable dtOrigon = DBMgr.GetDataTable(query);
				cbo원산지.DataBind(dtOrigon, true);

				if (dtHead.Rows[0]["ORIGIN"].ToString() == "")
				{
					cbo원산지.SelectedValue = "A";
				}
				else
				{
					// 우선 코드값 있는 것으로 한바퀴 돔
					for (int i = 0; i < cbo원산지.Items.Count; i++)
					{
						if (cbo원산지.GetItemText(cbo원산지.Items[i]) == dtHead.Rows[0]["ORIGIN"].ToString())
							cbo원산지.SelectedIndex = i;
					}

					// 위에서 안찾아지면 사용불가 검색
					for (int i = 0; i < cbo원산지.Items.Count; i++)
					{
						if (cbo원산지.GetItemText(cbo원산지.Items[i]) == dtHead.Rows[0]["ORIGIN"].ToString() + " (" + DD("사용불가") + ")")
							cbo원산지.SelectedIndex = i;
					}
				}
				#endregion
			
			// 턴오버 DC 또는 커미션 세팅
			if (GetTo.Decimal(dtHead.Rows[0]["RT_TURNOVER_DC"]) > 0)
				lbl계약할인할인율.Text = string.Format("{0:#,##0.##}", GetTo.Decimal(dtHead.Rows[0]["RT_TURNOVER_DC"]));
			else if (GetTo.Decimal(dtHead.Rows[0]["DC_COMMISSION"]) > 0)
				lbl계약할인할인율.Text = string.Format("{0:#,##0.##}", GetTo.Decimal(dtHead.Rows[0]["DC_COMMISSION"]));

			// 바인딩
			Header.SetDataTable(dtHead);

			// 저장버튼 활성화
			if (dtHead.Rows[0].RowState == DataRowState.Modified)
				Header_ControlValueChanged(tbx포커스, null);

			// ******************** 라인 그리드
			// 재고 수량 가져오기
			DataTable dtLineSt = CODE.JoinStockQuantity4(Quotation.회사코드, dtLine);
			dtLineSt.Columns.Add("RT_MARGIN_ST", typeof(decimal));

			// 재고단가 세팅 및 재고 사용했을때 이윤율 계산
			foreach (DataRow row in dtLineSt.Rows)
			{
				// 단가를 포맷세팅에 따라 반올림 (이걸 해야 눈에보이는 이윤율이랑 실제 계산이랑 맞아떨어짐, 해볼사람 없겠지만..)
				decimal stdPrice = string.Format("{0:" + grd라인.Cols["UM_STD"].Format + "}", row["UM_STD"]).ToDecimal();
				decimal stkPrice = string.Format("{0:" + grd라인.Cols["UM_STK"].Format + "}", row["UM_STK"]).ToDecimal();

				if (stdPrice > 0) row["UM_STD"] = stdPrice;
				if (stkPrice > 0) row["UM_STK"] = stkPrice;

				// 재고이윤율
				decimal stQty = row["QT_AVST"].ToDecimal();
				decimal poQty = row["QT_AVPO"].ToDecimal();

				if (stQty + poQty > 0)
					row["RT_MARGIN_ST"] = CALC.이윤율계산(row["UM_STK"].ToDecimal(), row["UM_KR_S"].ToDecimal());
			}

			// 저장 버튼 활성화 차단
			dtLineSt.AcceptChanges();

			// 바인딩
			grd라인.DataBind(dtLineSt);
			SetGridStyle(grd라인);
						
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				// 이윤율, DC율, RMK표시여부 기본값 설장
				if (grd라인[i, "RT_PROFIT"].ToString() == "")
					grd라인[i, "RT_PROFIT"] = cur이윤율.DecimalValue;

				if (grd라인[i, "RT_DC"].ToString() == "")
					grd라인[i, "RT_DC"] = curDC율.DecimalValue;

				if (grd라인[i, "YN_DSP_RMK"].ToString() == "") 
					grd라인[i, "YN_DSP_RMK"] = IsEquipment ? "N" : "Y";		// 리마크 표시 여부 (기본값 설정)

				// ********** 가격민감도가 있는 경우 민갑도 세팅
				if (GetTo.Decimal(grd라인[i, "EZPROFIT"]) != 0)
				{
					if (GetTo.Decimal(grd라인[i, "UM_EX_S"]) == 0)
					{
						grd라인[i, "RT_PROFIT"] = GetTo.Decimal(grd라인[i, "EZPROFIT"]) + GetTo.Decimal(dtHead.Rows[0]["RT_DC_DEFAULT"]) + GetTo.Decimal(dtHead.Rows[0]["RT_TURNOVER_DC"]);
						행계산(i, "RT_PROFIT");
					}
				}

				// ********** 가격민감도가 있는 경우 민갑도 세팅
				if (GetTo.Decimal(grd라인[i, "RT_MARGIN_FIX"]) != 0)
				{
					if (GetTo.Decimal(grd라인[i, "UM_EX_S"]) == 0)
					{
						grd라인[i, "RT_PROFIT"] = GetTo.Decimal(grd라인[i, "RT_MARGIN_FIX"]) + GetTo.Decimal(dtHead.Rows[0]["RT_DC_DEFAULT"]) + GetTo.Decimal(dtHead.Rows[0]["RT_TURNOVER_DC"]);
						행계산(i, "RT_PROFIT");
					}
				}
			}

			// 기준단가 컬럼 Visible 설정
			if (Quotation.부품영업)
			{
				if (dtLineSt.Select("UM_STD IS NOT NULL OR UM_STK IS NOT NULL").Length > 0)
				{
					grd라인.Cols["UM_STD"].Visible = true;
					grd라인.Cols["UM_STK"].Visible = true;
				}
				else
				{
					grd라인.Cols["UM_STD"].Visible = false;
					grd라인.Cols["UM_STK"].Visible = false;
				}
			}

			// 재고수량 컬럼 Visible 설정
			if (dtLineSt.Select("QT_AVST IS NOT NULL OR QT_AVPO IS NOT NULL").Length > 0)
			{
				grd라인.Cols["QT_AVST"].Visible = true;
				grd라인.Cols["QT_AVPO"].Visible = true;
			}
			else
			{
				grd라인.Cols["QT_AVST"].Visible = false;
				grd라인.Cols["QT_AVPO"].Visible = false;
			}

			// ********** 가로 그리드
			grd가로.BeginSetting(1, 1, false);

			// 매입처 컬럼 삭제
			for (int j = grd가로.Cols.Count - 1; j > grd가로.Cols["QT"].Index; j--)
				grd가로.Cols.Remove(j);

			// 매입처 컬럼 추가
			DataTable dtVendor = DBMgr.GetDataTable("PS_CZ_PU_QTN_REG_H", Quotation.회사코드, Quotation.파일번호);
			VendorCodes = new string[dtVendor.Rows.Count];
			List<string> exceptCols = new List<string>();

			for (int i = 0; i < dtVendor.Rows.Count; i++)
			{
				string partnerCode = dtVendor.Rows[i]["CD_PARTNER"].ToString();
				string partnerName = dtVendor.Rows[i]["LN_PARTNER"].ToString();
				VendorCodes[i] = partnerCode;
				exceptCols.Add(partnerCode + "_LT");

				grd가로.SetCol(partnerCode + "_CD", "코드"		, false);
				grd가로.SetCol(partnerCode + "_YN", "S"			, false);
				grd가로.SetCol(partnerCode + "_AM", partnerName	, 120	, typeof(decimal), FormatTpType.MONEY);
				grd가로.SetCol(partnerCode + "_LT", "납기"		, 40	, typeof(decimal), FormatTpType.QUANTITY);
			}

			grd가로.SetDefault(grd가로.SettingVersion, SumPositionEnum.Top);
			grd가로.SetExceptSumCol(true, exceptCols.ToArray());
			grd가로.SetAlternateRow();
			grd가로.SetMalgunGothic();

			// 바인딩
			DataTable dtSelHor = SQL.GetDataTable("PS_CZ_SA_QTN_L_SEL_HOR", SQLDebug.Print, Quotation.회사코드, Quotation.파일번호, Quotation.차수);
			grd가로.DataBind(dtSelHor);
			SetGridStyle(grd가로);

			// ---------------------------------------------------------------------------------------------------- 세로 그리드
			DataTable dtLineV = DBMgr.GetDataTable("PS_CZ_SA_QTN_REG_L_VERTICAL", Quotation.회사코드, Quotation.파일번호, Quotation.차수);
			grd세로.Binding = dtLineV;
			SetGridStyle(grd세로);

			// ---------------------------------------------------------------------------------------------------- 매입처 그리드
			grd매입처.DataBind(dtVendor);

			// ---------------------------------------------------------------------------------------------------- 기타
			합계계산();

			// 조회할때 아이템체크 한놈이라도 있으면 메세지 한번 띄운다
			if (IsEquipment)
			{
				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					if (GetTo.String(grd라인[i, "CHK_ITEM"]) == "Y")
					{
						ShowMessage("U코드재고와 기부속재고가 다른 아이템이 존재합니다. 확인바랍니다.");
						break;
					}
				}
			}

			// 견적을 이미 만든 경우라면 자동 탭 이동
			if (dtLineSt.Select("AM_KR_S IS NOT NULL").Length > 0)
				tab메인.SelectedTab = tab라인;

			MsgControl.CloseMsg();
		}

		#endregion

		#region ==================================================================================================== Save

		public bool 저장()
		{
			if (!SaveQuotation())
				return false;

			if (!SaveSupplier())
				return false;
			
			return true;
		}

		// ================================================== 매출가입력 탭
		private bool SaveQuotation()
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			DataTable dtHead = Header.GetChanges();
			DataTable dtLine = grd라인.GetChanges();

			try
			{
				#region ********** 유효성 검사

				// 헤드 필수값 체크
				if (ctx수주형태.CodeValue == "" || ctx수주형태.CodeName == "")
				{
					if (Quotation.메시지여부)
						ShowMessage(공통메세지._은는필수입력항목입니다, DD("수주형태"));
					return false;					
				}

				if (cbo지불조건.GetValue() == "")
				{
					if (Quotation.메시지여부)
						ShowMessage(공통메세지._은는필수입력항목입니다, DD("지불조건"));
					return false;
				}

				if (cbo포장형태.GetValue() == "")
				{
					if (Quotation.메시지여부)
						ShowMessage(공통메세지._은는필수입력항목입니다, DD("포장형태"));
					return false;
				}

				if (cbo과세구분.GetValue() == "")
				{
					if (Quotation.메시지여부)
						ShowMessage(공통메세지._은는필수입력항목입니다, DD("과세구분"));
					return false;
				}

				if (cbo원산지.GetValue() == "" || cbo원산지.GetValue() == "C")
				{
					if (Quotation.메시지여부)
						ShowMessage(공통메세지._은는필수입력항목입니다, DD("원산지"));
					return false;
				}

				// 라인 필수값 체크
				if (grd라인.DataTable.Select("NM_ITEM_PARTNER = ''").Length > 0)
				{
					if (Quotation.메시지여부)
						ShowMessage(공통메세지._은는필수입력항목입니다, DD("품목명"));
					return false;
				}

				#endregion

				// 경고메시지 팝업
				if (dtLine != null)
				{
					// 퀵링크 확인
					if (dtLine.Select("YN_QLINK = 'P'").Length > 0)
					{
						if (!Quotation.메시지여부)
						{
							return false;	// 탭넘김 자동 저장이면 저장안함
						}
						else
						{
							if (ShowMessage("퀵링크 [선택] 항목이 존재합니다. 계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
								return false;
						}
					}

					// 악성재고 확인
					DataTable dtBadStock = DBMgr.GetDataTable("PS_CZ_MM_BAD_STOCK_CHK", Quotation.회사코드, Quotation.파일번호);

					if (dtBadStock.Rows.Count > 0)
					{
						if (!Quotation.메시지여부)
						{
							return false;	// 탭넘김 자동 저장이면 저장안함
						}
						else
						{
							string message = "[악성재고관리항목]" + "\r\n\r\n";

							foreach (DataRow dr in dtBadStock.Rows)
							{
								message += dr["DC_MSG_KO"] + "\r\n";

								for (int i = 1; i <= 3; i++)
								{
									if (dr["NM_COND" + i].ToString() != "")
										message += dr["NM_COND" + i] + "\r\n";
								}

								message += "\r\n\r\n";
							}

							message += "★★ 이윤율 3% 이하 오퍼바랍니다!! ★★";

							ShowMessage(message);
							//MessageBox.Show(message);
						}
					}

					// ********** 경고마스터 확인
					WARNING warning = new WARNING(WARNING_TARGET.견적)
					{
						파일구분		= Quotation.파일번호.Left(2)
					,	매출처코드	= Quotation.매출처코드
					,	IMO번호		= Quotation.Imo번호
					,	아이템		= dtLine
					,	SQLDebug	= sqlDebug
					};

					// 조회
					warning.조회(Quotation.메시지여부);

					// 경고할 꺼리가 있는 경우만.
					if (warning.경고여부)
					{
						if (!Quotation.메시지여부)
						{
							return false;   // 탭넘김 자동 저장이면 저장안함
						}
						else
						{
							DialogResult 경고결과 = warning.ShowDialog();

							if (warning.저장불가 || 경고결과 == DialogResult.Cancel)
							{
								UTIL.메세지("작업이 취소되었습니다.", "WK1");
								return false;
							}
						}
					}

					//Warning warning = new Warning(WarningFlag.QTN);
					//warning.FileCode = Quotation.FileNumber.Left(2);
					//warning.BuyerCode = Quotation.PartnerCode;
					//warning.ImoNumber = Quotation.ImoNumber;
					//warning.Item = dtLine;
					//warning.Check();

					//if (warning.Count > 0)
					//{
					//	if (!Quotation.EnabledMessage)
					//	{
					//		return false;   // 탭넘김 자동 저장이면 저장안함
					//	}
					//	else
					//	{
					//		ShowMessage(warning.Message);
					//		if (!warning.AllowSave)
					//			return false;
					//	}
					//}
				}

				// 저장
				string headXml = Util.GetTO_Xml(dtHead);
				string lineXml = Util.GetTO_Xml(dtLine);
				DBMgr.ExecuteNonQuery("PX_CZ_SA_QTN_REG", DebugMode.Print, headXml, lineXml);
				키워드.견적저장(dtLine);

				Header.AcceptChanges();
				grd라인.AcceptChanges();
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
				return false;
			}
			
			return true;
		}

		// ================================================== 거래처선택 탭
		private bool SaveSupplier()
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (tab메인.SelectedTab == tab가로)
			{
				DataTable dtL = grd가로.GetChanges();
				DataTable dtXml = new DataTable();

				if (dtL != null)
				{
					foreach (string CD_PARTNER in VendorCodes)
					{
						// 필수 컬럼 + CD_PARTNER + YN_CHOICE 만 추출
						DataTable dt = dtL.DefaultView.ToTable(false, "NO_FILE", "NO_LINE", CD_PARTNER + "_CD", CD_PARTNER + "_YN");
						dt.Columns[CD_PARTNER + "_CD"].ColumnName = "CD_PARTNER";	// 컬럼명 변경
						dt.Columns[CD_PARTNER + "_YN"].ColumnName = "YN_CHOICE";    // 컬럼명 변경

						// 테이블 하나로 합치기
						DataTable dtSel = dt.Select("ISNULL(CD_PARTNER, '') <> ''").ToDataTable();
						if (dtSel != null) dtXml.Merge(dtSel);
					}
					
					string xmlL = GetTo.Xml(dtXml, RowState.Modified);
					//DBMgr.ExecuteNonQuery("SP_CZ_PU_QTN_REG_XML", DebugMode.Popup, DBNull.Value, xmlL);
					//SQL.ExecuteNonQuery()
					//"PX_CZ_PU_QTN_SEL"
					SQL sql = new SQL("PX_CZ_PU_QTN_SEL_2", SQLType.Procedure, sqlDebug);			
					sql.Parameter.Add2("@CD_COMPANY", Quotation.회사코드);
					sql.Parameter.Add2("@NO_FILE"	, Quotation.파일번호);
					sql.Parameter.Add2("@XML_L"		, dtXml.ToXml(DataRowState.Modified));
					sql.ExecuteNonQuery();


					grd가로.AcceptChanges();
				}
			}
			else if (tab메인.SelectedTab == tab세로)
			{
				DataTable dtL = grd세로.GetChanges();

				if (dtL != null)
				{
					// 필수 컬럼 + CD_PARTNER + YN_CHOICE 만 추출
					DataTable dtXml = dtL.DefaultView.ToTable(false, "NO_FILE", "NO_LINE", "CD_PARTNER", "YN_CHOICE");

					string xmlL = GetTo.Xml(dtXml, RowState.Modified);
					DBMgr.ExecuteNonQuery("SP_CZ_PU_QTN_REG_XML", new object[] { DBNull.Value, xmlL });
					dtL.AcceptChanges();
				}
			}

			return true;
		}

		#endregion

		#region ==================================================================================================== Print

		public void Print(H_CZ_PRT_OPTION f)
		{
			// =============================== 시작
			string printCompanyCode = Quotation.회사코드;

			// ********** 인터컴퍼니 체크
			string query = @"
SELECT
	CD_COMPANY_SO
FROM CZ_SA_INTERCOMPANY WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + printCompanyCode + @"'
	AND CD_PREFIX = '" + Quotation.파일번호.Substring(0, 2) + "'";

			DataTable dtIC = DBMgr.GetDataTable(query);

			if (dtIC.Rows.Count == 1)
				printCompanyCode = dtIC.Rows[0]["CD_COMPANY_SO"].ToString();

			// ********** 인쇄
			string printType = (string)Header.CurrentRow["CD_PRINT"];

			if (f.PrintType != "")
				printType = f.PrintType;

			try
			{
				// Json 요청
				string url = "http://erp.dintec.co.kr/WebService/ViewerConverter.asmx/QuotationTo" + printType;
				//string url = "http://localhost/erp/WebService/ViewerConverter.asmx/QuotationTo" + printType;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";

				using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
				{
					// 옵션 Dict for Json
					Dictionary<string, string> optionDict = new Dictionary<string, string>
					{
						{ "agy" , f.AgentLogo ? "Y" : "N" }
					,   { "rev" , f.Revised ? "Y" : "N" }
					,   { "ren" , f.PartnerName }
					,   { "coic", Quotation.회사코드 }
					,   { "lang", Header.CurrentRow["CD_AREA"].ToString() == "100" ? "ko" : "en" }	// CD_AREA Null 값도 있음
					};

					// 전체 Dict for Json
					Dictionary<string, string> paramDict = new Dictionary<string, string>
					{
						{ "co"      , printCompanyCode }
					,   { "fn"      , Quotation.파일번호 }
					,   { "oJson"   , Json.Serialize(optionDict) }
					};

					stream.Write(Json.Serialize(paramDict));
					stream.Flush();
					stream.Close();
				}

				// Json 응답
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Dictionary<string, string> resultDict;

				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					string resultJson = reader.ReadToEnd();
					resultDict = Json.Deserialize(resultJson);
				}

				// 파일 저장
				if (resultDict["result"] == "success")
				{
					string download = resultDict["download"];
					string extension = Path.GetExtension(download).Replace(".", "");
					string fileName = string.Format("{0}_{1:00}_SQTN_{2}.{3}", Quotation.파일번호, Quotation.차수, dtp견적일자.Text, extension);

					// 웹서버에서 파일 다운로드
					WebClient client = new WebClient();
					client.DownloadFile(download, FileMgr.GetTempPath() + fileName);

					// 워크플로우에 업로드
					string uploadName = FileMgr.Upload_WF(Quotation.회사코드, Quotation.파일번호, fileName, false);

					// 완료처리
					WorkFlow wf = new WorkFlow(Quotation.파일번호, "05", Quotation.차수);
					wf.AddItem("", "", fileName, uploadName);
					wf.Save();

					// 실행
					System.Diagnostics.Process.Start(FileMgr.GetTempPath() + fileName);

					// 바탕화면 저장
					if (f.SaveDesktop)
					{
						string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\";
						File.Copy(FileMgr.GetTempPath() + fileName, desktopPath + fileName);
					}
				}
				else
				{
					throw new Exception(resultDict["error"]);
				}
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}
		}

		#endregion

		#region ==================================================================================================== 계산식

		private void 행계산(int i, string colName)
		{
			// 외부 => 환율 표시형식 표준단가
			if (colName == "")
			{
				매출견적단가계산(i);
				매출견적금액계산(i);
				이윤율계산(i);
				매출단가계산(i);
				매출금액계산(i);
			}
			else if (colName == "RT_EXCH")		// 환율
			{
				매출견적금액계산(i);
				매출금액계산(i);

				최종이윤율계산(i);
			}
			else if (colName == "CD_DSP")		// 표시형식 *****
			{
				//매출견적금액계산(i);
				//매출금액계산(i);

				//최종이윤율계산(i);
			}
			else if (colName == "QT_QTN")		// 수량
			{
				매입금액계산(i);

				매출견적금액계산(i);
				매출금액계산(i);
			}
			else if (colName == "RT_PROFIT")	// 이윤율
			{
				매출견적단가계산(i);
				매출견적금액계산(i);

				매출단가계산(i);
				매출금액계산(i);

				최종이윤율계산(i);
			}
			else if (colName == "UM_EX_Q")		// 매출견적단가 입력
			{
				매출견적금액계산(i);
				이윤율계산(i);
				
				매출단가계산(i);
				매출금액계산(i);

				최종이윤율계산(i);
			}
			else if (colName == "RT_DC")		// 할인율
			{
				매출단가계산(i);
				매출금액계산(i);

				최종이윤율계산(i);
			}
			else if (colName == "UM_EX_S")		// 매출단가 입력
			{
				매출금액계산(i);

				최종이윤율계산(i);

				// 매출견적단가 => 별도 로직 적용 (뒤에서 부터 와야함)
				decimal 할인율			= GetTo.Decimal(grd라인[i, "RT_DC"]);
				decimal 매출단가외화		= GetTo.Decimal(grd라인[i, "UM_EX_S"]);
				decimal 매출견적단가외화	= Calculator.이윤율적용(매출단가외화, 할인율);
				grd라인[i, "UM_EX_Q"] = 매출견적단가외화;

				매출견적금액계산(i);
				이윤율계산(i);								
			}

			합계계산();
		}

		// 매입금액 => 각 외화, 원화단가에 수량만 곱해짐
		private void 매입금액계산(int i)
		{
			decimal 수량				= GetTo.Decimal(grd라인[i, "QT_QTN"]);
			decimal 매입견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_STD_P"]);
			decimal 매입단가외화		= GetTo.Decimal(grd라인[i, "UM_EX_P"]);
			decimal 매입단가원화		= GetTo.Decimal(grd라인[i, "UM_KR_P"]);

			decimal 매입견적금액외화	= 수량 * 매입견적단가외화;
			decimal 매입금액외화		= 수량 * 매입단가외화;
			decimal 매입금액원화		= 수량 * 매입단가원화;

			grd라인[i, "AM_EX_STD_P"] = 매입견적금액외화;
			grd라인[i, "AM_EX_P"]	  = 매입금액외화;
			grd라인[i, "AM_KR_P"]	  = 매입금액원화;
		}

		// 이윤율 => 매입, 매출견적 둘다 원화단가 베이스 계산
		private void 이윤율계산(int i)
		{
			decimal 매입단가원화		= GetTo.Decimal(grd라인[i, "UM_KR_P"]);
			decimal 매출견적단가원화	= GetTo.Decimal(grd라인[i, "UM_KR_Q"]);
			decimal 이윤율			= Calculator.이윤율계산(매입단가원화, 매출견적단가원화);

			grd라인[i, "RT_PROFIT"] = 이윤율;
		}

		// 매출견적단가 => 이윤이 바뀌는 경우 원화단가 베이스로 해서 환율을 나누어 계산
		private void 매출견적단가계산(int i)
		{
			decimal 매입단가원화	= GetTo.Decimal(grd라인[i, "UM_KR_P"]);
			decimal 이윤율		= GetTo.Decimal(grd라인[i, "RT_PROFIT"]);
			decimal 매출견적단가외화;
		
			매출견적단가외화 = Calculator.이윤율적용(매입단가원화, 이윤율) / 환율;		// 원화를 외화로 변경
			매출견적단가외화 = Calculator.반올림(매출견적단가외화, 표시형식단가용);

			grd라인[i, "UM_EX_Q"] = 매출견적단가외화;		
		}

		// 매출견적금액 => 그냥 구하면 됨
		private void 매출견적금액계산(int i)
		{
			decimal 수량				= GetTo.Decimal(grd라인[i, "QT_QTN"]);						
			decimal 매출견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_Q"]);
			decimal 매출견적금액외화	= Calculator.반올림(매출견적단가외화 * 수량, 표시형식금액용);
			decimal 매출견적단가원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["UM_KR_Q"].Format + "}", 매출견적단가외화 * 환율));
			decimal 매출견적금액원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["AM_KR_Q"].Format + "}", 매출견적금액외화 * 환율));

			grd라인[i, "AM_EX_Q"] = 매출견적금액외화;
			grd라인[i, "UM_KR_Q"] = 매출견적단가원화;
			grd라인[i, "AM_KR_Q"] = 매출견적금액원화;
		}

		// 매출단가 => 할인이 바뀌는 경우 외화단가 베이스로 계산 (매출견적과 매출은 같은 통화이므로 외화 베이스임)
		private void 매출단가계산(int i)
		{
			decimal 매출견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_Q"]);
			decimal 할인율			= GetTo.Decimal(grd라인[i, "RT_DC"]);
			decimal 표시형식			= (rdo표시형식단가.Checked) ? 표시형식단가용 : 4;	// 매출은 소수 4번째 자리까지 가지고 있는다 (쉽섭 방식)
			decimal 매출단가외화;

			매출단가외화 = Calculator.할인율적용(매출견적단가외화, 할인율);
			매출단가외화 = Calculator.반올림(매출단가외화, 표시형식);

			grd라인[i, "UM_EX_S"] = 매출단가외화;
		}

		// 매출금액 => 그냥 구하면 됨
		private void 매출금액계산(int i)
		{
			decimal 수량			= GetTo.Decimal(grd라인[i, "QT_QTN"]);
			decimal 매출단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_S"]);						
			decimal 매출금액외화	= Calculator.반올림(매출단가외화 * 수량	, 표시형식금액용);
			decimal 매출단가원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["UM_KR_S"].Format + "}", 매출단가외화 * 환율));
			decimal 매출금액원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["AM_KR_S"].Format + "}", 매출금액외화 * 환율));

			grd라인[i, "AM_EX_S"] = 매출금액외화;
			grd라인[i, "UM_KR_S"] = 매출단가원화;
			grd라인[i, "AM_KR_S"] = 매출금액원화;
		}

		// 최종이윤율 => 매입, 매출 둘다 원화금액 베이스 계산
		private void 최종이윤율계산(int i)
		{			
			decimal 매입금액원화	= GetTo.Decimal(grd라인[i, "AM_KR_P"]);
			decimal 매출금액원화	= GetTo.Decimal(grd라인[i, "AM_KR_S"]);
			decimal 최종이윤율	= Calculator.이윤율계산(매입금액원화, 매출금액원화);

			grd라인[i, "RT_MARGIN"] = 최종이윤율;

			// 재고 사용시 마진율
			decimal 가용수량 = GetTo.Decimal(grd라인[i, "QT_AVST"]);
			decimal 입고수량 = GetTo.Decimal(grd라인[i, "QT_AVPO"]);

			if (가용수량 + 입고수량 > 0)
			{
				decimal 매입단가 = GetTo.Decimal(grd라인[i, "UM_STK"]);
				decimal 매출단가 = GetTo.Decimal(grd라인[i, "UM_KR_S"]);
				grd라인[i, "RT_MARGIN_ST"] = (매출단가 > 0) ? 100 * (1 - 매입단가 / 매출단가) : 0;
			}

			// 최저단가 대비 마진율
			decimal 최저단가 = grd라인[i, "UM_MIN"].실수();

			if (최저단가 > 0)
				grd라인[i, "RT_MARGIN_MIN"] = CALC.이윤율계산(최저단가, 매출금액원화);
		}

		// 합계
		private void 합계계산()
		{
            DataTable dt = grd라인.GetTableFromGrid();

            decimal 매입금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", "NO_LINE < 90000"));

			decimal 매출견적금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_Q)", "NO_LINE < 90000"));
			decimal 매출견적금액외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX_Q)", "NO_LINE < 90000"));
			
			decimal 매출금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", "NO_LINE < 90000"));
			decimal 매출금액외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX_S)", "NO_LINE < 90000"));

			decimal 할인금액원화 = 매출견적금액원화 - 매출금액원화;
			decimal 할인금액외화 = 매출견적금액외화 - 매출금액외화;

			decimal 부대비용원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", "NO_LINE > 90000"));
			decimal 최종합계원화 = 매출금액원화 + 부대비용원화;

			decimal 이윤금액일반 = 매출금액원화 - 매입금액원화;
			decimal 이윤율일반 = (매출금액원화 == 0) ? 0 : 100 * (1 - 매입금액원화 / 매출금액원화);

			// 재고 이윤율
			dt.Columns.Add("AM_STOCK", typeof(decimal), "QT_QTN * UM_STK");
			decimal 매입금액일반 = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)" , "ISNULL(QT_AVST, 0) + ISNULL(QT_AVPO, 0) = 0 AND NO_LINE < 90000"));
			decimal 매입금액재고 = GetTo.Decimal(dt.Compute("SUM(AM_STOCK)", "ISNULL(QT_AVST, 0) + ISNULL(QT_AVPO, 0) > 0 AND NO_LINE < 90000"));
			decimal 이윤금액재고 = 매출금액원화 - (매입금액일반 + 매입금액재고);	// 매입금액일반 : 재고를 뺀 일반매입
			decimal 재고이윤율 = Util.CalcProfitRate(매입금액일반 + 매입금액재고, 매출금액원화);

			// 계약할인
			decimal 계약할인할인율	= GetTo.Decimal(lbl계약할인할인율.Text);
			decimal 계약할인합계원화	= 매출금액원화 * ( 1 - 계약할인할인율 / 100);
			decimal 계약할인이윤원화	= 계약할인합계원화 - (매입금액일반 + 매입금액재고);
			decimal 계약할인이윤율	= Util.CalcProfitRate(매입금액일반 + 매입금액재고, 계약할인합계원화);

			// Label 정보 변경
			lbl통화외화.Text = GetCon.Text(cbo통화);

			lbl매출견적금액원화.Text = string.Format("{0:#,##0.##}", 매출견적금액원화);
			lbl매출견적금액외화.Text = string.Format("{0:#,##0.##}", 매출견적금액외화);

			lbl매출금액원화.Text = string.Format("{0:#,##0.##}", 매출금액원화);
			lbl매출금액외화.Text = string.Format("{0:#,##0.##}", 매출금액외화);

			lblDC금액원화.Text = string.Format("{0:#,##0.##}", 할인금액원화);
			lblDC금액외화.Text = string.Format("{0:#,##0.##}", 할인금액외화);

			lbl이윤금액일반.Text = string.Format("{0:#,##0.##}", 이윤금액일반);
			lbl이윤율일반.Text = string.Format("{0:#,##0.##}", 이윤율일반);

			lbl이윤금액재고.Text = string.Format("{0:#,##0.##}", 이윤금액재고);
			lbl이윤율재고.Text = string.Format("{0:#,##0.##}", 재고이윤율);

			lbl부대비용원화.Text = string.Format("{0:#,##0.##}", 부대비용원화);
			lbl최종합계원화.Text = string.Format("{0:#,##0.##}", 최종합계원화);

			lbl계약할인합계원화.Text = string.Format("{0:#,##0}", 계약할인합계원화);
			lbl계약할인이윤원화.Text = string.Format("{0:#,##0}", 계약할인이윤원화);
			lbl계약할인이윤율.Text = string.Format("{0:#,##0.##}", 계약할인이윤율);
		}

		#endregion

		#region ==================================================================================================== 기타

		private void SetGridStyle(FlexGrid flexGrid)
		{
			flexGrid.Redraw = false;

			// 매출가입력 그리드
			if (flexGrid == grd라인)
			{
				for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
				{
					// 표준단가 복수개일때 단가
					string priceToolTip = GetTo.String(flexGrid[i, "UM_STD_MULTI"]);

					if (priceToolTip.IndexOf("\n") > 0)
						flexGrid.SetCellStyle(i, flexGrid.Cols["UM_STD"].Index, "TOOLTIP");
					else
						flexGrid.SetCellStyle(i, flexGrid.Cols["UM_STD"].Index, "");

					// 매입단가와 재고기준금액과 20% 이상 차이나는 경우 경고 표시
					decimal UM_KR_P = GetTo.Decimal(flexGrid[i, "UM_KR_P"]);
					decimal 표준단가 = GetTo.Decimal(flexGrid[i, "UM_STD"]);
					decimal diffRate = 표준단가 == 0 ? 0 : (표준단가 - UM_KR_P) / 표준단가 * 100;

					if (diffRate > 20 || diffRate < -20)
						SetGrid.CellRed(flexGrid, i, flexGrid.Cols["UM_KR_P"].Index);
					else
						SetGrid.CellRed(flexGrid, i, flexGrid.Cols["UM_KR_P"].Index, "");

					// 기부속재고코드와 U코드재고코드가 다른경우
					if (GetTo.String(flexGrid[i, "CHK_ITEM"]) == "Y")
						SetGrid.CellRed(flexGrid, i, flexGrid.Cols["CD_ITEM"].Index);
					else
						SetGrid.CellRed(flexGrid, i, flexGrid.Cols["CD_ITEM"].Index, "");

					// 악성재고
					if (flexGrid[i, "YN_BAD"].ToString() == "Y")
						flexGrid.SetCellStyle(i, flexGrid.Cols["RT_MARGIN"].Index, "BAD_STOCK");
					else
						flexGrid.SetCellStyle(i, flexGrid.Cols["RT_MARGIN"].Index, "");

					// 첨부파일 유무 표시
					//int fileCountImg = GetTo.Int(grd라인[i, "FILE_COUNT_IMG"]);

					if ((string)grd라인[i, "FILE_LIST"] == "Y")
						grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON"].Index, Icons.Popup);
				}
			}
			else if (flexGrid == grd가로)
			{
				for (int j = flexGrid.Cols["QT"].Index + 1; j < flexGrid.Cols.Count; j++)
				{
					if (flexGrid.Cols[j].Name.Right(3) == "_YN")
					{
						for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
						{
							if (flexGrid[i, j].ToString() == "Y")
								flexGrid.SetCellStyle(i, j + 1, "SELECTED");
							else
								flexGrid.SetCellStyle(i, j + 1, "");
						}

						j = j + 2;
					}
				}
			}
			else if (flexGrid == grd세로)
			{
				if (flexGrid.HasNormalRow)
				{
					// 병합
					flexGrid.Clear(ClearFlags.UserData, flexGrid.Rows.Fixed, 1, flexGrid.Rows.Count - 1, flexGrid.Cols.Count - 1);
					flexGrid.Merge("NO_LINE", "NO_DSP", "NM_SUBJECT", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "NM_UNIT", "QT");

					for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
					{
						// 홀수행 배경
						if (GetTo.Int(flexGrid[i, "DR"]) % 2 == 1)
							Util.SetGridOddRow(flexGrid, i);

						// 선택행 색칠
						if (flexGrid[i, "YN_CHOICE"].ToString() == "Y")
							SetSelectedStatus(i, "Y");
					}
				}
			}

			flexGrid.Redraw = true;
		}

		private void SetSelectedStatus(int row, string selected)
		{
			string style = (selected == "Y") ? "SELECTED" : "";

			grd세로[row, "YN_CHOICE"] = selected;
			grd세로.SetCellStyle(row, grd세로.Cols["CD_PARTNER"].Index, style);
			grd세로.SetCellStyle(row, grd세로.Cols["LN_PARTNER"].Index, style);
			grd세로.SetCellStyle(row, grd세로.Cols["UM_KR"].Index, style);
			grd세로.SetCellStyle(row, grd세로.Cols["AM_KR"].Index, style);
			grd세로.SetCellStyle(row, grd세로.Cols["LT"].Index, style);
			grd세로.SetCellStyle(row, grd세로.Cols["DC_RMK"].Index, style);
		}

		//private void SetEnabled()
		//{
		//    bool enabled;

		//    if (Quotation.STA_QTN == "O")
		//        enabled = true;
		//    else
		//        enabled = false;

		//    if (!Certify.IsLive())
		//        enabled = true;

		//    one헤드.Enabled = enabled;
		//    grd가로.AllowEditing = enabled;
		//    grd매입.AllowEditing = enabled;
		//    grd세로.AllowEditing = enabled;

		//    grd라인.SetEditColumn("CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "UNIT_QTN", "QT_QTN", "RT_PROFIT", "UM_EX_Q", "RT_DC", "UM_EX_S", "LT", "DC_RMK", "YN_DSP_RMK", "YN_GULL");
		//}

		private void SetOrigin()
		{
			string query = @"
SELECT
	  CASE WHEN C.CD_ORIGIN IS NOT NULL THEN 'A' ELSE 'B' END	AS CODE
	, B.NM_ORIGIN	AS NAME
FROM
(
	SELECT
		  CD_COMPANY
		, CD_PARTNER
	FROM CZ_PU_QTNL
	WHERE 1 = 1
		AND CD_COMPANY = '" + Quotation.회사코드 + @"'
		AND NO_FILE = '" + Quotation.파일번호 + @"'
		AND YN_CHOICE = 'Y'
	GROUP BY CD_COMPANY, CD_PARTNER
)								AS A
JOIN	  CZ_MA_PARTNER_ORIGIN	AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN CZ_MA_PARTNER			AS C ON B.CD_COMPANY = C.CD_COMPANY AND B.CD_PARTNER = C.CD_PARTNER AND B.CD_ORIGIN = C.CD_ORIGIN
GROUP BY B.NM_ORIGIN, C.CD_ORIGIN

UNION ALL

SELECT
	  'C'
	, ORIGIN + ' (" + DD("사용불가") + @")'
FROM CZ_SA_QTNH
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND NO_FILE = '" + Quotation.파일번호 + @"'
	AND ISNULL(ORIGIN, '') != ''

ORDER BY NM_ORIGIN";

			DataTable dt = DBMgr.GetDataTable(query);
			SetCon.DataBind(cbo원산지, dt, true);
		}

		#endregion
	}
}

