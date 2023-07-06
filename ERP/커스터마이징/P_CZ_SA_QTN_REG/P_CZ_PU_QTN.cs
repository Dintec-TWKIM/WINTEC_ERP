using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.Controls;
using System.Net;
using System.IO;
using System.Diagnostics;
using Parsing;
using DX;

namespace cz
{
	public partial class P_CZ_PU_QTN : PageBase
	{
		Dictionary<int, int> lineToRowIndex = new Dictionary<int, int>();
		int WonRound = 0;

		#region ===================================================================================================== Property

		private P_CZ_SA_QTN_REG Quotation
		{
			get
			{
				return (P_CZ_SA_QTN_REG)this.Parent.GetContainerControl();
			}
		}
		
		public string PartnerCode 
		{
			get
			{ 
				return grd헤드["CD_PARTNER"].ToString();
			}
		}

		public string ExchangeCode
		{
			get
			{
				return cbo통화.GetValue();
			}
		}

		public decimal ExchangeRate
		{
			get
			{
				return cur환율.DecimalValue;
			}
		}

		private int RoundCodePrice
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
					if (ExchangeCode == "000")
						roundCode = 0;
					else
						roundCode = 2;
				}

				return roundCode;
			}
		}

		private int RoundCodeAmount
		{
			get
			{
				// 금액 표시형식은 무조건 표기 표시형식을 따름 (단가, 금액 라디오 무의미, 단가 * 수량해서 나온거 이므로 단가 체크되있어도 단가 표시형식을 따름)
				return GetTo.Int(GetDb.CodeFlag1(cbo표시형식));
			}
		}

		public FlexGrid 그리드헤드
		{
			get
			{
				return grd헤드;
			}
		}

		public FlexGrid 그리드라인
		{
			get
			{
				return grd라인;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PU_QTN()
		{
			//StartUp.Certify(this);
			InitializeComponent();
			if (LoginInfo.CompanyCode != "S100")
				WonRound = 0;
			else
				WonRound = 2;
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
			// 편집불가 패널
			pnl지급조건.Enabled(false);	// 반드시 죽여야함, 사고침, 유령업체에 선지급함

			// 달력은 타이핑 못하게 함, 타이핑 될때마다 환율 불러오는 문제도 있고 조회될때도 이벤트 실행되므로 클릭으로만 변경되게 함
			dtp견적일자.Init();
			dtp견적일자.AllowTyping(false);
			dtp유효일자.Init();
			dtp유효일자.AllowTyping(false);

			// 콤보박스
			DataSet ds = GetDb.Code("MA_B000005", "PU_C000014", "CZ_SA00014");
			cbo통화.DataBind(ds.Tables[0], false);
			cbo지급조건.DataBind(ds.Tables[1], true);
			cbo표시형식.DataBind(ds.Tables[2], false);
			cbo표시형식.SetValue("6");
			cbo부대비용.DataBind(GetDb.ExtraCost(), true);

			// 숫자박스
			cur할인율.DecimalValue = 0;

			// 기타
			grd헤드.DetailGrids = new FlexGrid[] { grd라인 };
			spl메인.SplitterDistance = 900;
		}

		private void InitGrid()
		{
			DataTable dtSrm = new DataTable();
			dtSrm.Columns.Add("CODE");
			dtSrm.Columns.Add("NAME");
			dtSrm.Rows.Add("O", "");
			dtSrm.Rows.Add("S", Global.MainFrame.DD("가능"));
			dtSrm.Rows.Add("C", Global.MainFrame.DD("완료"));

			DataTable dtParsing = new DataTable();
			dtParsing.Columns.Add("CODE");
			dtParsing.Columns.Add("NAME");
			dtParsing.Rows.Add("Y", Global.MainFrame.DD("가능"));
			dtParsing.Rows.Add("N", "");

			DataTable dtContract = new DataTable();
			dtContract.Columns.Add("CODE");
			dtContract.Columns.Add("NAME");
			dtContract.Rows.Add("Y", Global.MainFrame.DD("계약"));
			dtContract.Rows.Add("N", "");

			// ********** 헤드
			grd헤드.BeginSetting(1, 1, false);

			grd헤드.SetCol("SRM_STATUS"	, "SRM"			, 40);
			grd헤드.SetCol("YN_PARSING"	, "파싱"			, 40);
			grd헤드.SetCol("NO_FILE"		, "파일번호"		, false);
			grd헤드.SetCol("CD_PARTNER"	, "매입처코드"	, false);
			grd헤드.SetCol("LN_PARTNER"	, "매입처명"		, 200);
			grd헤드.SetCol("ATTN"		, "담당자"		, false);
			grd헤드.SetCol("DT_QTN"		, "견적일자"		, false);
			grd헤드.SetCol("DT_VALID"	, "유효일자"		, false);
			grd헤드.SetCol("CD_EXCH"		, "통화명"		, 50);
			grd헤드.SetCol("RT_EXCH"		, "환율"			, false);
			grd헤드.SetCol("COND_PAY"	, "지불조건"		, false);
			grd헤드.SetCol("AM_EX_E"		, "매입견적금액"	, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd헤드.SetCol("RT_DC_P"		, "DC(%)"		, 50	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd헤드.SetCol("AM_DC_P"		, "DC금액"		, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd헤드.SetCol("AM_EX_P"		, "매입금액"		, 100	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd헤드.SetCol("LT"			, "납기"			, 50	, false, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("FILE_ICON"	, "첨부"			, 40);			
			grd헤드.SetCol("YN_EXT"		, "계약"			, 50);
			grd헤드.SetCol("DC_RMK_QTN"	, "비고"			, false);
			grd헤드.SetCol("EDITED"		, "수정여부"		, false);

			grd헤드.Cols["SRM_STATUS"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["YN_PARSING"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["CD_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["FILE_ICON"].ImageAlign = ImageAlignEnum.CenterCenter;
			grd헤드.Cols["YN_EXT"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.SetDataMap("SRM_STATUS", dtSrm, "CODE", "NAME");
			grd헤드.SetDataMap("YN_PARSING", dtParsing, "CODE", "NAME");
			grd헤드.SetDataMap("CD_EXCH", GetDb.Code("MA_B000005"), "CODE", "NAME");
			grd헤드.SetDataMap("YN_EXT", dtContract, "CODE", "NAME");
			grd헤드.SetDummyColumn("AM_EX_E", "RT_DC_P", "AM_DC_P", "AM_EX_P", "LT");			

			grd헤드.SetOneGridBinding(new object[] { }, one헤드);
			grd헤드.SetBindningRadioButton(new RadioButtonExt[] { rdo표시형식단가, rdo표시형식금액 }, new string[] { "1", "2" });

			grd헤드.SetDefault("20.08.31.01", SumPositionEnum.None);
			grd헤드.LoadUserCache("P_CZ_PU_QTN");

			// ********** 라인
			grd라인.BeginSetting(2, 1, false);
			
			grd라인.SetCol("NO_FILE"			, "파일번호"		, false);
			grd라인.SetCol("CD_PARTNER"		, "거래처코드"	, false);
			grd라인.SetCol("NO_LINE"			, "항번"			, false);
			grd라인.SetCol("NO_LINE_PARENT"	, "부모항번"		, false);
			grd라인.SetCol("NO_DSP"			, "순번"			, 40);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 140);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 220);
			grd라인.SetCol("NO_PLATE"		, "품목코드(HGS)"	, 100);
			grd라인.SetCol("NM_PLATE"		, "품목명(HGS)"	, 200);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 80	, false);	// EDIT(POPUP)
			grd라인.SetCol("NM_ITEM"			, "재고명"		, 200);
			grd라인.SetCol("UCODE"			, "U코드"		, 100);
			grd라인.SetCol("UCODE_OLD"		, "U코드OLD"		, false);
			grd라인.SetCol("UNIT"			, "단위"			, 45);
			grd라인.SetCol("QT"				, "수량"			, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grd라인.SetCol("UM_EX_E"			, "외화단가"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);	// EDIT
			grd라인.SetCol("AM_EX_E"			, "외화금액"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_E"			, "원화단가"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("AM_KR_E"			, "원화금액"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd라인.SetCol("RT_DC_P"			, "DC\n(%)"		, 45	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);	// EDIT
			grd라인.SetCol("UM_EX_P"			, "외화단가"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);	// EDIT
			grd라인.SetCol("AM_EX_P"			, "외화금액"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_P"			, "원화단가"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_P"			, "원화금액"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);

			grd라인.SetCol("LT"				, "납기"			, 40	, false	, typeof(decimal), FormatTpType.MONEY);			// EDIT
			grd라인.SetCol("DC_RMK"			, "비고"			, 150	, false);	// EDIT
			
			grd라인.SetCol("NO_ENGINE"		, "엔진번호"		, false);
			grd라인.SetCol("TP_BOM"			, "BOM구분"		, false);
			grd라인.SetCol("SORT"			, "SORT"		, false);

			// 헤더 병합
			grd라인[0, grd라인.Cols["UM_EX_E"].Index] = "매입견적단가";
			grd라인[0, grd라인.Cols["AM_EX_E"].Index] = "매입견적단가";
			grd라인[0, grd라인.Cols["UM_KR_E"].Index] = "매입견적단가";
			grd라인[0, grd라인.Cols["AM_KR_E"].Index] = "매입견적단가";

			grd라인[0, grd라인.Cols["UM_EX_P"].Index] = "매입단가";
			grd라인[0, grd라인.Cols["AM_EX_P"].Index] = "매입단가";
			grd라인[0, grd라인.Cols["UM_KR_P"].Index] = "매입단가";
			grd라인[0, grd라인.Cols["AM_KR_P"].Index] = "매입단가";

			grd라인.Cols["NO_DSP"].Format = "####.##";
			grd라인.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NO_PLATE"].Visible = false;	// 기본은 안보이게 하고 엑셀 버튼 눌렀을때만 보이게 하자
			grd라인.Cols["NM_PLATE"].Visible = false;
			//grd라인.Cols["UM_KR_E"].Visible = false;	// 언젠가는 꼭 구현하자.....
			//grd라인.Cols["AM_KR_E"].Visible = false;

			grd라인.SetDefault("19.04.23.03", SumPositionEnum.Top);
			grd라인.SetExceptSumCol("UM_EX_E", "UM_KR_E", "RT_DC_P", "UM_EX_P", "UM_KR_P", "LT");			
			grd라인.SetSumColumnStyle("AM_EX_E", "AM_KR_E", "AM_EX_P", "AM_KR_P");
			grd라인.SetEditColumn("NO_PLATE", "NM_PLATE", "CD_ITEM", "UM_EX_E", "RT_DC_P", "UM_EX_P", "LT", "DC_RMK");	// 차후 ENABLE 상태에 따라 변경되도록 수정하자
			grd라인.LoadUserCache("P_CZ_PU_QTN");
		}
		
		protected override void InitPaint()
		{
			
		}

		public void Clear()
		{
			if (TitleText != "Loaded")
				return;

			// 그리드
			grd헤드.ClearData();
			grd라인.ClearData();

			// 원그리드
			tbx견적번호.Text = "";
			dtp견적일자.ClearData();
			dtp유효일자.ClearData();
			cbo통화.SetDefaultValue();
			cur환율.DecimalValue = 1;
			cbo지급조건.SetDefaultValue();
			ctx대행업체.ClearData();
			cur할인율.DecimalValue = 0;
			cur납기.DecimalValue = 0;
			cbo표시형식.SetValue("6");
			rdo표시형식단가.Checked = true;
			cbo부대비용.SetDefaultValue();
			tbx비고.Text = "";

			// 합계라벨
			lbl매입견적금액KR.Text = "0";
			lblDC금액KR.Text = "0";
			lbl매입금액KR.Text = "0";
			lblDC율KR.Text = "0";

			lbl매입견적금액EX.Text = "0";
			lblDC금액EX.Text = "0";
			lbl매입금액EX.Text = "0";
			lbl부대비용EX.Text = "0";
			lbl합계EX.Text = "0";
		}

		public void 사용(bool editable)
		{
			pnl버튼.Editable(editable);
			one헤드.Editable(editable);
			grd라인.Editable(editable);

			pnl지급조건.Editable(false);
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			dtp견적일자.CalendarClosed += new EventHandler(dtp견적일자_CalendarClosed);
			cbo통화.SelectionChangeCommitted += new EventHandler(cbo통화_SelectionChangeCommitted);

			btn파싱.Click += new EventHandler(btn파싱_Click);
			btnSRM연동.Click += new EventHandler(btnSRM연동_Click);
			btn현대엑셀.Click += new EventHandler(btn현대엑셀_Click);
			btn견적불가.Click += Btn견적불가_Click;

			btn환율.Click += new EventHandler(btn환율_Click);
			btn할인율.Click += new EventHandler(btn할인율_Click);
			btn납기.Click += new EventHandler(btn납기_Click);
			btn부대비용추가.Click += new EventHandler(btn부대비용추가_Click);
			btn부대비용삭제.Click += new EventHandler(btn부대비용삭제_Click);
			btn표시형식.Click += new EventHandler(btn표시형식_Click);

			grd헤드.AfterRowChange += new RangeEventHandler(grd헤드_AfterRowChange);
			grd헤드.DoubleClick += new EventHandler(Grd헤드_DoubleClick);
			grd라인.DoubleClick += new EventHandler(grd라인_DoubleClick);
			grd라인.KeyDown += new KeyEventHandler(grd라인_KeyDown);
			grd라인.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);
			grd라인.AfterEdit += new RowColEventHandler(grd라인_AfterEdit);
		}

		

		private void dtp견적일자_CalendarClosed(object sender, EventArgs e)
		{
			cur환율.DecimalValue = GetDb.ExchangeRate(dtp견적일자.Text, cbo통화.GetValue(), "P");
		}
		
		private void cbo통화_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cur환율.DecimalValue = GetDb.ExchangeRate(dtp견적일자.Text, cbo통화.GetValue(), "P");
		}

		private void btn파싱_Click(object sender, EventArgs e)
		{
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			// 워크플로우에서 파일 찾아오기
			string fileName = QuotationFinder.GetFileFromWorkFlow(Quotation.회사코드, Quotation.파일번호, PartnerCode);

			// 워크플로우에 파일이 없으면 로컬에서 수동으로 업로드
			if (fileName == "")
			{
				OpenFileDialog fileDlg = new OpenFileDialog();
				fileDlg.Filter = "Quotation|*.msg;*.pdf;*.xls;*.xlsx";

				if (fileDlg.ShowDialog() != DialogResult.OK) return;
				fileName = fileDlg.FileName;
			}
		
			// 분석 시작
			try
			{
				MsgControl.ShowMsg(DD("파싱 중 입니다."));

				// Quotation 파싱 시작
				QuotationParser parser = new QuotationParser(fileName);
				parser.Parse(true);

				DataTable dtParsing = parser.ItemL.Copy();

				// 단가 가져오기 및 재고코드가 필요한 경우 DB에서 조인하여 재고코드를 물고옴
				if (dtParsing.Columns.Contains("NO"))
					dtParsing.Columns["NO"].ColumnName = "NO_DSP";

				if (dtParsing.Columns.Contains("ITEM"))
					dtParsing.Columns["ITEM"].ColumnName = "CD_ITEM";

				if (dtParsing.Columns.Contains("DESC"))
					dtParsing.Columns["DESC"].ColumnName = "NM_ITEM";

				if (dtParsing.Columns.Contains("UNIQ"))
					dtParsing.Columns["UNIQ"].ColumnName = "UCODE";

				if (dtParsing.Columns.Contains("RMK"))
					dtParsing.Columns["RMK"].ColumnName = "DC_RMK";

				if (!dtParsing.Columns.Contains("CHARGE"))
					dtParsing.Columns.Add(new DataColumn("CHARGE", typeof(string), "'N'"));

				string xml = GetTo.Xml(GetTo.DataTable(dtParsing, "NO_DSP", "NM_ITEM", "UNIT", "QT", "UCODE", "UM", "AM", "LT", "DC_RMK", "CHARGE"), "CHARGE <> 'Y'");
				DataTable dtItem = DBMgr.GetDataTable("PS_CZ_PU_QTN_L_PARSING", debugMode, Quotation.회사코드, Quotation.파일번호, PartnerCode, xml);

				// Head 바인딩
				tbx견적번호.Text = parser.Reference;
				tbx비고.Text = parser.Rmk;

				// Line 바인딩
				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					// 부대비용 라인이면 PASS 함
					string itemCode = grd라인.Rows[i]["CD_ITEM"].ToString();

					if (itemCode.Length == 6 && itemCode.Left(2) == "SD")
                        continue;

					// DSP로 해당 아이템 찾아오기
					DataRow[] row = dtItem.Select("NO_DSP = " + grd라인.Rows[i]["NO_DSP"]);
					int leadTime = 0;

					// ***** 매입처별 세팅해야 할 것
					if (PartnerCode == "00740")	// 하이에어코리아
					{
						if (GetTo.Int(parser.Lt) > 0)
							leadTime = GetTo.Int(GetTo.Decimal(parser.Lt) / 5 * 7);
					}
					else if (PartnerCode == "11415")	// 삼건엠에스
					{
						if (GetTo.Int(parser.Lt) > 0)
							leadTime = GetTo.Int(parser.Lt);
					}
                    else if (PartnerCode == "00432")    // 삼건세기
                    {
						grd라인[i, "LT"] = GetTo.Int(row[0]["LT"]);

						if (((string)row[0]["NM_ITEM"]).IndexOf("특별단가 적용") > 0)
                            grd라인[i, "RT_DC_P"] = 0;
                    }
                    else
					{
						leadTime = GetTo.Int(row[0]["LT"]);
					}

					// 바인딩
					//grd라인[i, "CD_ITEM"] = row[0]["CD_ITEM"];
					grd라인[i, "UCODE"] = row[0]["UCODE"];
					grd라인[i, "UM_EX_E"] = row[0]["UM"];
					grd라인[i, "DC_RMK"] = row[0]["DC_OFFER"];

					// 재고코드가 바뀐 경우 만 덮어쓰기 (못찾은 경우는 빈칸으로 덮어쓰지 않음, 20211014)
					if (row[0]["CD_ITEM"].ToString() != "")
						grd라인[i, "CD_ITEM"] = row[0]["CD_ITEM"];

					if (leadTime > 0)
						grd라인[i, "LT"] = leadTime;

					CalcRow(i, "");
				}

				// 부대비용 추가
				DataRow[] drCharge = dtParsing.Select("CHARGE = 'Y'");

				foreach (DataRow row in drCharge)
				{
					//FREIGHT HANDLING PACKING CERTIFICATE
					string chargeCode = row["NM_ITEM"].ToString();
					string itemCode = "";

					// 부대비용코드에 따라 재고코드 결정
					if (chargeCode == "FREIGHT")
						itemCode = "SD0001";
					else if (chargeCode == "HANDLING")
						itemCode = "SD0002";
					else if (chargeCode == "PACKING")
						itemCode = "SD0003";
					else if (chargeCode == "CERTIFICATE")
						itemCode = "SD0004";

					// 재고코드가 없는 경우에만 추가
					if (itemCode != "" && grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND CD_ITEM = '" + itemCode + "'").Length == 0)
					{
						int lineNew = 90001;
						int lineMax = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");
						if (lineMax > 90000) lineNew = lineMax + 1;

						grd라인.Rows.Add();
						grd라인.Row = grd라인.Rows.Count - 1;
						grd라인["NO_FILE"] = Quotation.파일번호;
						grd라인["CD_PARTNER"] = PartnerCode;
						grd라인["NO_LINE"] = lineNew;
						grd라인["NM_ITEM_PARTNER"] = chargeCode + " CHARGE";
						grd라인["CD_ITEM"] = itemCode;
						grd라인["NM_ITEM"] = chargeCode + " CHARGE";
						grd라인["QT"] = 1;
						grd라인["UM_EX_E"] = row["UM"];
						grd라인["RT_DC_P"] = 0;
						grd라인["LT"] = 0;
						grd라인["TP_BOM"] = "S";
						grd라인["SORT"] = lineNew;
						grd라인.AddFinished();

						CalcRow(grd라인.Row, "");
					}
				}

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}			
		}

		private void btnSRM연동_Click(object sender, EventArgs e)
		{
			string query = @"
SELECT
	  A.DT_QTN
	, A.DT_VALID
	, A.NO_REF
	, A.CD_EXCH
	, A.DC_RMK		AS DC_RMK_H
	, B.NO_LINE
	, B.UM_EX_STD	AS UM_EX_E
	, B.AM_EX_STD	AS AM_EX_E
	, B.RT_DC		AS RT_DC_P
	, B.UM_EX		AS UM_EX_P
	, B.AM_EX		AS AM_EX_P
	, B.LT
	, B.DC_RMK		AS DC_RMK_L
	, A.CD_STATUS
-- 신규추가
	, B.UCODE
	, C.CD_ITEM
	, D.DC_OFFER
FROM	  CZ_SRM_QTNH		AS A WITH(NOLOCK)
JOIN	  CZ_SRM_QTNL		AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_FILE = B.NO_FILE AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN CZ_DX_PITEM_KEY	AS C ON B.CD_COMPANY = C.CD_COMPANY AND C.KEYU != '' AND B.UCODE = C.KEYU
LEFT JOIN MA_PITEM			AS D ON C.CD_COMPANY = D.CD_COMPANY AND C.CD_ITEM = D.CD_ITEM
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND A.NO_FILE = '" + Quotation.파일번호 + @"'
	AND A.CD_PARTNER = '" + PartnerCode + @"'
	AND A.CD_STATUS IN ('S', 'C')

UPDATE CZ_SRM_QTNH SET
	CD_STATUS = 'C'
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND NO_FILE = '" + Quotation.파일번호 + @"'
	AND CD_PARTNER = '" + PartnerCode + @"'
	AND CD_STATUS = 'S'";

			DataTable dt = DBMgr.GetDataTable(query);

			if (dt.Rows.Count == 0)
			{
				ShowMessage("전송 내역이 없습니다.");
				return;
			}
			else if (dt.Rows[0]["CD_STATUS"].ToString() == "C")
			{
				if (ShowMessage("재전송 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;
			}

			// Head 정보 변경
			tbx견적번호.Text = dt.Rows[0]["NO_REF"].ToString() == "" ? DateTime.Now.ToShortDateString().Replace("-", ".") + "(SRM)" : dt.Rows[0]["NO_REF"].ToString();
			dtp견적일자.Text = dt.Rows[0]["DT_QTN"].ToString();
			dtp유효일자.Text = dt.Rows[0]["DT_VALID"].ToString();
			cbo통화.SelectedValue = dt.Rows[0]["CD_EXCH"].ToString();
			cbo통화_SelectionChangeCommitted(null, null);
			tbx비고.Text = dt.Rows[0]["DC_RMK_H"].ToString();

			// 라인변경하기전 헤드와 라인의 거래처 비교 (이상하게 다른 거래처의 라인에 바인딩 될때가 있음)
			if (PartnerCode != grd라인[grd라인.Rows.Fixed, "CD_PARTNER"].ToString())
			{
				ShowMessage("거래처가 잘못되었습니다. 기획실로 연락바랍니다.");
				return;
			}

			// DC 기본값
			decimal dcRate = GetTo.Decimal(grd헤드["RT_DC_P_DEFAULT"]);

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				DataRow[] dr = dt.Select("NO_LINE = " + grd라인[i, "NO_LINE"]);
				if (dr.Length == 0) continue;

				grd라인[i, "CD_ITEM"] = dr[0]["CD_ITEM"];
				grd라인[i, "UCODE"] = dr[0]["UCODE"];
				grd라인[i, "DC_RMK"] = dr[0]["DC_OFFER"];

				grd라인[i, "UM_EX_E"] = dr[0]["UM_EX_E"];
				grd라인[i, "AM_EX_E"] = dr[0]["AM_EX_E"];
				grd라인[i, "RT_DC_P"] = dr[0]["RT_DC_P"];
				grd라인[i, "UM_EX_P"] = dr[0]["UM_EX_P"];
				grd라인[i, "AM_EX_P"] = dr[0]["AM_EX_P"];
				grd라인[i, "LT"] = dr[0]["LT"];
				grd라인[i, "DC_RMK"] = dr[0]["DC_RMK_L"];

				// DC 적용
				if (GetTo.Decimal(grd라인[i, "RT_DC_P"]) == 0) grd라인[i, "RT_DC_P"] = dcRate;

				CalcRow(i, "RT_DC_P");
			}

			저장();
			SetGridStyle(grd라인);
			ShowMessage(PageResultMode.SaveGood);
		}

		private void btn현대엑셀_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			// 현대 체크
			if (PartnerCode != "11823")
			{
				UT.ShowMsg("올바른 매입처가 아닙니다.");
				return;
			}

			// HGS 관련 컬럼 보이도록 설정
			grd라인.Cols["NO_PLATE"].Visible = true;
			grd라인.Cols["NM_PLATE"].Visible = true;

			// 워크플로우에서 파일 자동 검색
			string query = @"
SELECT
	NM_FILE_REAL
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND NO_KEY = '" + Quotation.파일번호 + @"'
	AND TP_STEP = '04'
	AND CD_SUPPLIER = '" + PartnerCode + @"'
	AND NM_FILE_REAL IS NOT NULL
ORDER BY NO_LINE DESC";

			DataTable dtFile = SQL.GetDataTable(query);
			EXCEL excel = new EXCEL();

			if (dtFile.Rows.Count > 0)
			{
				excel.FileName = Dintec.FILE.Download(Quotation.파일번호, dtFile.Rows[0][0].ToString(), false);
			}
			else
			{
				// 워크플로우에 파일이 없으면 로컬에서 수동으로 업로드
				if (excel.OpenDialog() != DialogResult.OK)
					return;
			}
			
			try
			{
				UT.ShowPgb(DD("분석 중 입니다."));

				// ********** Head 파싱
				excel.HeaderRowIndex = 0;
				excel.StartDataIndex = 1;
				DataTable dtHead = excel.Read();

				string refNumber = dtHead.Rows[0][0].ToString();
				string remark = dtHead.Rows[3][0].ToString();

				if (refNumber.IndexOf("QTN NO : ") >= 0)
					tbx견적번호.Text = refNumber.Replace("QTN NO : ", "");

				if (remark != "출력순서")
					tbx비고.Text = remark;

				// ********** Line 파싱
				excel.HeaderRowIndex = 11;
				excel.StartDataIndex = 12;
				DataTable dtLine = excel.Read();

				// 컬럼이름 변경 : 현대 MAPS → 딘텍 ERP
				dtLine.Columns["출력 순서"].ColumnName = "NO_DSP";
				dtLine.Columns["PLATE NO"].ColumnName = "NO_PLATE";
				dtLine.Columns["DESCRIPTION"].ColumnName = "NM_PLATE";
				dtLine.Columns["U CODE NO"].ColumnName = "UCODE";
				dtLine.Columns["DEL."].ColumnName = "LT";
				dtLine.Columns["UNIT PRICE"].ColumnName = "UM";

				// 납기 중 STOCK은 10으로 변환
				foreach (DataRow row in dtLine.Select("LT = 'STOCK'"))
					row["LT"] = "10";

				// Plate Number에 해당하는 엑셀 아이템 찾아오기 (재고코드도 같이)
				SQL sql = new SQL("PS_CZ_PU_QTN_PARSING_HGS", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@CD_COMPANY", Quotation.회사코드);
				sql.Parameter.Add2("@NO_FILE"	, Quotation.파일번호);
				sql.Parameter.Add2("@XML"		, GetTo.Xml(dtLine, "NO_PLATE <> '' AND NO_PLATE <> '비고'"));
				DataSet dsDb = sql.GetDataSet();

				// 엑셀가격으로 단가 입력
				foreach (DataRow item in dsDb.Tables[0].Rows)
				{
					DataRow row = grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND NO_LINE = " + item["NO_LINE"])[0];
					row["NO_PLATE"] = item["NO_PLATE"];
					row["NM_PLATE"] = item["NM_PLATE"];
					//row["CD_ITEM"] = item["CD_ITEM"].ToString();    // 빈값을 확실하게 입력해야 업데이트가 됨 (null은 머지에서 업뎃 안함)
					row["UCODE"] = item["UCODE"].ToString();
					row["UM_EX_E"] = item["UM"];
					row["LT"] = item["LT"];

					// 재고코드가 바뀐 경우 만 덮어쓰기 (못찾은 경우는 빈칸으로 덮어쓰지 않음, 20211014)
					if (item["CD_ITEM"].ToString() != "")
						row["CD_ITEM"] = item["CD_ITEM"];

					CalcRow(row, "");
				}

				// 안찾아진 아이템만 팝업 띄우기				
				if (dsDb.Tables[2].Rows.Count > 0)
				{
					P_CZ_PU_QTN_HGS f = new P_CZ_PU_QTN_HGS(dsDb.Tables[1], dsDb.Tables[2]);

					if (f.ShowDialog() == DialogResult.OK)
					{
						foreach (DataRow item in f.Result.Rows)
						{
							DataRow row = grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND NO_LINE = " + item["NO_LINE"])[0];
							row["NO_PLATE"] = item["NO_PLATE"];
							row["NM_PLATE"] = item["NM_PLATE"];
							row["CD_ITEM"] = item["CD_ITEM"].ToString();
							row["UCODE"] = item["UCODE"].ToString();
							row["UM_EX_E"] = item["UM"];
							row["LT"] = item["LT"];
							
							CalcRow(row, "");							
						}
					}
				}

				grd라인.AutoRowSize();
				SetGridStyle(grd라인);
				UT.ClosePgb();
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Btn견적불가_Click(object sender, EventArgs e)
		{
			tbx견적번호.Text = string.Format("{0:MM.dd}", DateTime.Now);
			
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{				
				grd라인[i, "RT_DC_P"] = 0;
				grd라인[i, "LT"] = 998;
			}
		}
		private void btn환율_Click(object sender, EventArgs e)
		{
			foreach (DataRow row in grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "'"))
				CalcRow(row, "");
		}

		private void btn할인율_Click(object sender, EventArgs e)
		{
			foreach (DataRow row in grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND NO_LINE < 90000"))
			{
				row["RT_DC_P"] = cur할인율.DecimalValue;
				CalcRow(row, "RT_DC_P");
			}
		}

		private void btn납기_Click(object sender, EventArgs e)
		{
			foreach (DataRow row in grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND NO_LINE < 90000"))
				row["LT"] = cur납기.DecimalValue;
		}

		private void btn표시형식_Click(object sender, EventArgs e)
		{
			foreach (DataRow row in grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "'"))
				CalcRow(row, "");
		}

		private void btn부대비용추가_Click(object sender, EventArgs e)
		{
			if (cbo부대비용.GetValue() == "")
			{
				ShowMessage("부대비용을 선택하세요!");
				return;
			}

			if (grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND CD_ITEM = '" + cbo부대비용.GetValue() + "'").Length > 0) 
			{
				ShowMessage("이미 추가되었습니다.");
				return;
			}

			int NO_LINE = 90001;
			int NO_LINE_MAX = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");
			if (NO_LINE_MAX > 90000) NO_LINE = NO_LINE_MAX + 1;

			grd라인.Rows.Add();
			grd라인.Row = grd라인.Rows.Count - 1;
			grd라인["NO_FILE"] = Quotation.파일번호;
			grd라인["CD_PARTNER"] = PartnerCode;
			grd라인["NO_LINE"] = NO_LINE;
			grd라인["NM_ITEM_PARTNER"] = cbo부대비용.GetText();
			grd라인["CD_ITEM"] = cbo부대비용.GetValue();
			grd라인["NM_ITEM"] = cbo부대비용.GetText();
			grd라인["QT"] = 1;
			grd라인["RT_DC_P"] = 0;
			grd라인["LT"] = 0;
			grd라인["TP_BOM"] = "S";
			grd라인["SORT"] = NO_LINE;
			grd라인.AddFinished();

			grd라인.Col = grd라인.Cols["UM_EX_E"].Index;
			grd라인.Focus();
		}

		private void btn부대비용삭제_Click(object sender, EventArgs e)
		{
			if (GetCon.Value(cbo부대비용) == "") { ShowMessage("부대비용을 선택하세요!"); return; }

			DataRow[] row = grd라인.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND CD_ITEM = '" + GetCon.Value(cbo부대비용) + "'");
			if (row.Length > 0) row[0].Delete();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트
		
		private void Grd헤드_DoubleClick(object sender, EventArgs e)
		{
			// 헤더 클릭
			if (grd헤드.MouseRow < grd헤드.Rows.Fixed)
			{
				SetGridStyle(grd헤드);
				return;
			}

			// 첨부파일 열기
			string colName = grd헤드.Cols[grd헤드.Col].Name;

			if (colName == "FILE_ICON" && grd헤드["FILE_LIST"].ToString() == "Y")
			{
				Util.SaveReferenceFile(Quotation.파일번호, new string[] { grd헤드["CD_PARTNER"].ToString() });
			}
		}

		private void grd라인_DoubleClick(object sender, EventArgs e)
		{
			// 헤더 클릭
			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				SetGridStyle(grd라인);
				return;
			}
		}

		private void grd라인_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					grd라인[grd라인.Row + i, grd라인.Col] = clipboard[i, 0];

					if (grd라인.Cols[grd라인.Col].Name == "UM_EX_E" || grd라인.Cols[grd라인.Col].Name == "RT_DC_P") CalcRow(grd라인.Row + i, "UM_EX_E");
					if (grd라인.Row + i == grd라인.Rows.Count - 1) break;
				}
			}
		}

		private void grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;

			if (colName == "CD_ITEM")
			{
				string itemCode = grd라인.EditData;

				if (itemCode == "")
				{
					grd라인["NM_ITEM"] = "";
				}
				else
				{
					string query = @"
SELECT
	*
FROM MA_PITEM
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND CD_ITEM = '" + itemCode + @"'
	AND YN_USE = 'Y'";

					
					DataTable dt = DBMgr.GetDataTable(query);
					decimal price = 0;

					if (dt.Rows.Count == 1)
					{
						grd라인["CD_ITEM"] = dt.Rows[0]["CD_ITEM"];
						grd라인["NM_ITEM"] = dt.Rows[0]["NM_ITEM"];
						price = GetTo.Decimal(dt.Rows[0]["STAND_PRC"]);
					}
					else
					{
						H_CZ_MA_PITEM f = new H_CZ_MA_PITEM(itemCode);

						if (f.ShowDialog() == DialogResult.OK)
						{
							grd라인["CD_ITEM"] = f.ITEM["CD_ITEM"];
							grd라인["NM_ITEM"] = f.ITEM["NM_ITEM"];
							price = GetTo.Decimal(f.ITEM["STAND_PRC"]);
						}
						else
						{
							e.Cancel = true;
							SendKeys.Send("{ESC}");
						}
					}

					// 매입처가 STOCK일 경우만 단가 계산
					if (PartnerCode == "STOCK")	
					{
						grd라인["UM_EX_E"] = Util.Round(price, WonRound);
						CalcRow(e.Row, "UM_EX_E");
					}
				}
			}
			else if (colName == "UM_EX_E" || colName == "RT_DC_P" || colName == "UM_EX_P")
			{
				//CalcRow(e.Row, colName);
			}
			else if (colName == "LT")
			{
				// BOM인 경우 부모 LT 변경
				if (grd라인["TP_BOM"].ToString() == "C")
				{
					int parentLine = GetTo.Int(grd라인["NO_LINE_PARENT"]);
					int maxLeadTime = GetTo.Int(grd라인.GetTableFromGrid().Compute("MAX(LT)", "NO_LINE_PARENT = '" + parentLine + "'"));
					int newLeadTime = GetTo.Int(grd라인.EditData);
					grd라인[lineToRowIndex[parentLine], "LT"] = maxLeadTime >= newLeadTime ? maxLeadTime : newLeadTime;
				}

				// Head LT 변경
				grd헤드["LT"] = grd라인.GetTableFromGrid().Compute("MAX(LT)", "");
			}
		}

		private void grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;

			if (colName.In("UM_EX_E", "RT_DC_P", "UM_EX_P"))
				CalcRow(e.Row, colName);
		}


		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			// HGS 관련 컬럼 보이지 않도록 설정
			grd라인.Cols["NO_PLATE"].Visible = false;
			grd라인.Cols["NM_PLATE"].Visible = false;
			
			// 조회
			DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_QTN_H", Quotation.회사코드, Quotation.파일번호);
			DataTable dtExch = BASE.ExchangeRates(BASE.Today());

			// 기본값으로 바뀌는 매입처를 체크하기 위한 필드
			dtHead.Columns.Add("EDITED", typeof(string));
			
			// ********** 거래처 기본값 설정
			foreach (DataRow row in dtHead.Rows)
			{
				// 기본 N 때리고 Unchanged로 함
				row["EDITED"] = "N";
				row.AcceptChanges();

				// 시작
				if (row["DT_QTN"].ToString() == "")
				{
					row["DT_QTN"] = BASE.Today();
				}

				if (row["DT_VALID"].ToString() == "")
				{
					row["DT_VALID"] = BASE.Today(30);
				}

				if (row["CD_EXCH"].ToString() == "")
				{
					string exchangeCode = (string)row["CD_EXCH_DEFAULT"];
					DataRow dr = dtExch.Select("CURR_SOUR = '" + exchangeCode + "'").GetFirstRow();
					string 환율컬럼 = row["CD_PARTNER"].문자() == "17747" ? "RT_EXCH_B" : "RT_EXCH_P";	// 테크로스는 기준환율 씀

					row["CD_EXCH"] = exchangeCode;
					row["RT_EXCH"] = dr != null ? CT.Decimal(dr[환율컬럼]) : 1;
				}

				if (row["FG_PAYMENT"].ToString() == "")
				{
					row["FG_PAYMENT"] = row["FG_PAYMENT_DEFAULT"];
				}

				if (row["RT_DC_P"].ToString() == "")
				{
					row["RT_DC_P"] = row["RT_DC_P_DEFAULT"];
				}

				if (row["TP_DIGIT"].ToString() == "")
				{
					row["TP_DIGIT"] = (row["CD_EXCH"].ToString() == "000") ? "6" : "3";
					row["TP_ROUND"] = (row["CD_EXCH"].ToString() == "000") ? "1" : "2";
				}
			}

			// 변경된 얘들 표식 남기기
			for (int i = 0; i < dtHead.Rows.Count; i++)
			{
				if (dtHead.Rows[i].RowState == DataRowState.Modified)
					dtHead.Rows[i]["EDITED"] = "P";
			}

			// 바인딩 및 행 상태 변경
			grd헤드.Binding = dtHead;

			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				if (grd헤드[i, "EDITED"].ToString() == "P")
					grd헤드[i, "EDITED"] = "Y";
			}

			SetGridStyle(grd헤드);
		}

		private void grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			grd라인.Redraw = false;
			string filter = "CD_PARTNER = '" + PartnerCode + "'";

			if (grd헤드.DetailQueryNeed)
			{
				DataTable dt = DBMgr.GetDataTable("PS_CZ_PU_QTN_L", Quotation.회사코드, Quotation.파일번호, PartnerCode);
				dt.Columns.Add("NO_PLATE", typeof(string));	// 현대엑셀 때문에 추가
				dt.Columns.Add("NM_PLATE", typeof(string));
				grd라인.DataBindAdd(dt, filter);

				// L 그리드 DC율 표시
				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					if (grd라인[i, "RT_DC_P"].ToString() == "")
						grd라인[i, "RT_DC_P"] = grd헤드["RT_DC_P"];
				}
			}
			else
			{
				grd라인.BindingAdd(null, filter);
			}

			합계계산();
			SetGridStyle(grd라인);
			grd라인.Redraw = true;
		}

		#endregion

		#region ==================================================================================================== Save

		public bool 저장()
		{
			//DebugMode debugMode = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.Print;
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			// 변경사항
			DataTable dtHead = grd헤드.GetChanges();
			DataTable dtLine = grd라인.GetChanges();

			// 저장
			//SQL.ExecuteNonQuery("PX_CZ_PU_QTN_2", sqlDebug, dtHead.ToXml(), dtLine.ToXml());
			SQL.ExecuteNonQuery("PX_CZ_PU_QTN_2", SQLDebug.Popup, dtHead.ToXml(), dtLine.ToXml());

			grd헤드.AcceptChanges();
			grd라인.AcceptChanges();
		
			// 워크플로우 처리
			WorkFlow workflow = new WorkFlow(Quotation.파일번호, "04", Quotation.차수);

			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				string partnerCode = grd헤드[i, "CD_PARTNER"].ToString();
				decimal amount = GetTo.Decimal(grd헤드[i, "AM_EX_E"]);
				int leadTime = GetTo.Int(grd헤드[i, "LT"]);
				string remark = grd헤드[i, "DC_RMK_QTN"].ToString();

				if (amount > 0 || (amount == 0 && leadTime >= 998) || remark != "")
					workflow.AddItem("", partnerCode, "", "");
			}

			workflow.Save();
			
			return true;
		}

		#endregion

		#region ==================================================================================================== 닫기

		public void Exit()
		{
			grd헤드.SaveUserCache("P_CZ_PU_QTN");
			grd라인.SaveUserCache("P_CZ_PU_QTN");
		}

		#endregion

		#region ==================================================================================================== 계산식

		private void CalcRow(int rowIndex, string colName)
		{
			DataRow row = grd라인.GetDataRow(rowIndex);
			CalcRow(row, colName);
			//if (colName == "")				// 외부
			//{
			//    매입견적금액계산(row);
			//    매입단가계산(row);
			//    매입금액계산(row);
			//}
			//else if (colName == "UM_EX_E")	// 매입견적단가
			//{
			//    매입견적금액계산(row);
			//    매입단가계산(row);
			//    매입금액계산(row);
			//}
			//else if (colName == "RT_DC_P")	// DC(%)
			//{
			//    매입단가계산(row);
			//    매입금액계산(row);
			//}
			//else if (colName == "UM_EX_P")	// 매입단가
			//{
			//    할인율계산(row);
			//    매입금액계산(row);
			//}

			//CalcBomTotal(row);
			//합계계산();
		}

		private void CalcRow(DataRow row, string colName)
		{
			if (colName == "")				// 외부
			{
				매입견적금액계산(row);
				매입단가계산(row);
				매입금액계산(row);
			}
			else if (colName == "UM_EX_E")	// 매입견적단가
			{
				매입견적금액계산(row);
				매입단가계산(row);
				매입금액계산(row);
			}
			else if (colName == "RT_DC_P")	// DC(%)
			{
				매입단가계산(row);
				매입금액계산(row);
			}
			else if (colName == "UM_EX_P")	// 매입단가
			{
				할인율계산(row);
				매입금액계산(row);
			}

			CalcBomTotal(row);
			합계계산();
		}

		// 매입견적금액 (※ 매입견적단가가 외부의 요소에 의해 바뀔일이 없으므로 금액만 계산)
		private void 매입견적금액계산(DataRow row)
		{
			decimal 수량 = GetTo.Decimal(row["QT"]);
			decimal 매입견적단가외화 = GetTo.Decimal(row["UM_EX_E"]);
			decimal 매입견적금액외화 = Util.Round(매입견적단가외화 * 수량, RoundCodeAmount);
			decimal 매입견적단가원화 = Util.Round(매입견적단가외화 * ExchangeRate, WonRound);
			decimal 매입견적금액원화 = Util.Round(매입견적금액외화 * ExchangeRate, WonRound);

			row["AM_EX_E"] = 매입견적금액외화;
			row["UM_KR_E"] = 매입견적단가원화;
			row["AM_KR_E"] = 매입견적금액원화;
		}

		// 할인율 계산
		private void 할인율계산(DataRow row)
		{
			decimal 매입견적단가외화 = GetTo.Decimal(row["UM_EX_E"]);
			decimal 매입단가외화 = GetTo.Decimal(row["UM_EX_P"]);

			if (GetTo.Int(row["NO_LINE"]) < 90000)
				row["RT_DC_P"] = Util.CalcDiscountRate(매입견적단가외화, 매입단가외화);
			else
				row["RT_DC_P"] = 0;
		}

		// 매입단가 → 외부의 요소에 의해 변하는 경우(매입견적단가를 수정했다거나 DC를 수정했다거나)
		private void 매입단가계산(DataRow row)
		{
			decimal 매입견적단가외화 = GetTo.Decimal(row["UM_EX_E"]);
			decimal 할인율 = GetTo.Decimal(row["RT_DC_P"]);
			decimal 매입단가외화 = Util.Round(매입견적단가외화 * (1 - 할인율 / 100), RoundCodePrice);

			row["UM_EX_P"] = 매입단가외화;
		}

		// 매입금액 
		private void 매입금액계산(DataRow row)
		{
			decimal 수량 = GetTo.Decimal(row["QT"]);
			decimal 매입단가외화 = GetTo.Decimal(row["UM_EX_P"]);
			decimal 매입금액외화 = Util.Round(매입단가외화 * 수량, RoundCodeAmount);
			decimal 매입단가원화 = Util.Round(매입단가외화 * ExchangeRate, WonRound);
			decimal 매입금액원화 = Util.Round(매입금액외화 * ExchangeRate, WonRound);

			row["AM_EX_P"] = 매입금액외화;
			row["UM_KR_P"] = 매입단가원화;
			row["AM_KR_P"] = 매입금액원화;
		}

		private void CalcBomTotal(DataRow row)
		{
			if (GetTo.String(row["TP_BOM"]).In("S", "P"))
				return;

			int parentLine = GetTo.Int(row["NO_LINE_PARENT"]);
			int parentRow = lineToRowIndex[parentLine];
			DataTable dtChild = grd라인.GetTableFromGrid("NO_LINE_PARENT = " + parentLine);

			// 정미수량에 의한 수량 필드 추가
			decimal parentQt = GetTo.Decimal(grd라인[parentRow, "QT"]);
			dtChild.Columns.Add("UM_EX_E_BOM", typeof(decimal), "UM_EX_E * (QT / " + parentQt + ")");
			dtChild.Columns.Add("UM_EX_P_BOM", typeof(decimal), "UM_EX_P * (QT / " + parentQt + ")");

			// 계산
			decimal beforePriceEx = GetTo.Decimal(dtChild.Compute("SUM(UM_EX_E_BOM)", ""));	// 1. 정미수량에 의한 매입기준단가 계산
			decimal beforeAmountEx = beforePriceEx * parentQt;								// 2. 매입기준단가 * 수량
			decimal afterPriceEx = GetTo.Decimal(dtChild.Compute("SUM(UM_EX_P_BOM)", ""));	// 3. 정미수량에 의한 매입단가 계산
			decimal afterAmountEx = afterPriceEx * parentQt;								// 4. 매입단가 * 수량
			decimal discountRate = Util.CalcDiscountRate(beforePriceEx, afterPriceEx);	// 5. 최종적으로 DC 계산 (라인별로 DC가 틀릴 경우 때문)

			grd라인[parentRow, "UM_EX_E"] = beforePriceEx;
			grd라인[parentRow, "AM_EX_E"] = beforeAmountEx;
			grd라인[parentRow, "UM_KR_E"] = beforePriceEx * cur환율.DecimalValue;
			grd라인[parentRow, "AM_KR_E"] = beforeAmountEx * cur환율.DecimalValue;
			grd라인[parentRow, "RT_DC_P"] = discountRate;
			grd라인[parentRow, "UM_EX_P"] = afterPriceEx;
			grd라인[parentRow, "AM_EX_P"] = afterAmountEx;
			grd라인[parentRow, "UM_KR_P"] = afterPriceEx * cur환율.DecimalValue;
			grd라인[parentRow, "AM_KR_P"] = afterAmountEx * cur환율.DecimalValue;
		}

		//// 매입견적금액 → 매입견적단가가 외부의 요소에 의해 바뀔일이 없으므로 금액만 계산
		//private void 매입견적금액계산(int row)
		//{
		//    decimal 수량 = GetTo.Decimal(grd라인[row, "QT"]);
		//    decimal 매입견적단가외화 = GetTo.Decimal(grd라인[row, "UM_EX_E"]);
		//    decimal 매입견적금액외화 = Util.Round(매입견적단가외화 * 수량, RoundCodeAmount);
		//    decimal 매입견적단가원화 = Util.Round(매입견적단가외화 * ExchangeRate, WonRound);
		//    decimal 매입견적금액원화 = Util.Round(매입견적금액외화 * ExchangeRate, WonRound);

		//    grd라인[row, "AM_EX_E"] = 매입견적금액외화;
		//    grd라인[row, "UM_KR_E"] = 매입견적단가원화;
		//    grd라인[row, "AM_KR_E"] = 매입견적금액원화;
		//}

		//// 할인율 계산
		//private void 할인율계산(int row)
		//{
		//    decimal 매입견적단가외화 = GetTo.Decimal(grd라인[row, "UM_EX_E"]);
		//    decimal 매입단가외화 = GetTo.Decimal(grd라인[row, "UM_EX_P"]);

		//    if (GetTo.Int(grd라인[row, "NO_LINE"]) < 90000)
		//        grd라인[row, "RT_DC_P"] = Util.CalcDiscountRate(매입견적단가외화, 매입단가외화);
		//    else
		//        grd라인[row, "RT_DC_P"] = 0;
		//}

		//// 매입단가 → 외부의 요소에 의해 변하는 경우(매입견적단가를 수정했다거나 DC를 수정했다거나)
		//private void 매입단가계산(int row)
		//{			
		//    decimal 매입견적단가외화 = GetTo.Decimal(grd라인[row, "UM_EX_E"]);
		//    decimal 할인율 = GetTo.Decimal(grd라인[row, "RT_DC_P"]);
		//    decimal 매입단가외화 = Util.Round(매입견적단가외화 * (1 - 할인율 / 100), RoundCodePrice);
			
		//    grd라인[row, "UM_EX_P"] = 매입단가외화;
		//}

		//// 매입금액 
		//private void 매입금액계산(int row)
		//{
		//    decimal 수량 = GetTo.Decimal(grd라인[row, "QT"]);
		//    decimal 매입단가외화 = GetTo.Decimal(grd라인[row, "UM_EX_P"]);
		//    decimal 매입금액외화 = Util.Round(매입단가외화 * 수량, RoundCodeAmount);
		//    decimal 매입단가원화 = Util.Round(매입단가외화 * ExchangeRate, WonRound);
		//    decimal 매입금액원화 = Util.Round(매입금액외화 * ExchangeRate, WonRound);

		//    grd라인[row, "AM_EX_P"] = 매입금액외화;
		//    grd라인[row, "UM_KR_P"] = 매입단가원화;
		//    grd라인[row, "AM_KR_P"] = 매입금액원화;
		//}

		//private void CalcBomTotal(int row)
		//{
		//    if (GetTo.String(grd라인[row, "TP_BOM"]).In("S", "P"))
		//        return;

		//    int parentLine = GetTo.Int(grd라인[row, "NO_LINE_PARENT"]);
		//    int parentRow = lineToRowIndex[parentLine];
		//    DataTable dtChild = grd라인.GetTableFromGrid("NO_LINE_PARENT = " + parentLine);

		//    // 정미수량에 의한 수량 필드 추가
		//    decimal parentQt = GetTo.Decimal(grd라인[parentRow, "QT"]);
		//    dtChild.Columns.Add("UM_EX_E_BOM", typeof(decimal), "UM_EX_E * (QT / " + parentQt + ")");
		//    dtChild.Columns.Add("UM_EX_P_BOM", typeof(decimal), "UM_EX_P * (QT / " + parentQt + ")");

		//    // 계산
		//    decimal beforePriceEx = GetTo.Decimal(dtChild.Compute("SUM(UM_EX_E_BOM)", ""));	// 1. 정미수량에 의한 매입기준단가 계산
		//    decimal beforeAmountEx = beforePriceEx * parentQt;								// 2. 매입기준단가 * 수량
		//    decimal afterPriceEx = GetTo.Decimal(dtChild.Compute("SUM(UM_EX_P_BOM)", ""));	// 3. 정미수량에 의한 매입단가 계산
		//    decimal afterAmountEx = afterPriceEx * parentQt;								// 4. 매입단가 * 수량
		//    decimal discountRate = Util.CalcDiscountRate(beforePriceEx, afterPriceEx);	// 5. 최종적으로 DC 계산 (라인별로 DC가 틀릴 경우 때문)

		//    grd라인[parentRow, "UM_EX_E"] = beforePriceEx;
		//    grd라인[parentRow, "AM_EX_E"] = beforeAmountEx;
		//    grd라인[parentRow, "UM_KR_E"] = beforePriceEx * cur환율.DecimalValue;
		//    grd라인[parentRow, "AM_KR_E"] = beforeAmountEx * cur환율.DecimalValue;
		//    grd라인[parentRow, "RT_DC_P"] = discountRate;
		//    grd라인[parentRow, "UM_EX_P"] = afterPriceEx;
		//    grd라인[parentRow, "AM_EX_P"] = afterAmountEx;
		//    grd라인[parentRow, "UM_KR_P"] = afterPriceEx * cur환율.DecimalValue;
		//    grd라인[parentRow, "AM_KR_P"] = afterAmountEx * cur환율.DecimalValue;
		//}

		private void 합계계산()
		{
			DataTable dt = grd라인.DataTable;

			// 원화 합계 금액
			decimal amountKrE = GetTo.Decimal(dt.Compute("SUM(AM_EX_E)", "CD_PARTNER = '" + PartnerCode + "' AND NO_LINE < 90000 AND TP_BOM <> 'P'")) * ExchangeRate;	// 매입견적금액
			decimal amountKrP = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", "CD_PARTNER = '" + PartnerCode + "' AND NO_LINE < 90000 AND TP_BOM <> 'P'"));					// 매입금액
			decimal dcAmountKr  = amountKrE - amountKrP;
			decimal dcRate = (amountKrE == 0) ? 0 : 100 * (1 - amountKrP / amountKrE);

			lbl매입견적금액KR.Text = string.Format("{0:#,##0.##}", amountKrE);
			lblDC금액KR.Text = string.Format("{0:#,##0.##}", dcAmountKr);
			lbl매입금액KR.Text = string.Format("{0:#,##0.##}", amountKrP);
			lblDC율KR.Text = string.Format("{0:#,##0.##}", dcRate);

			// 외화 합계 금액
			decimal amountExE = GetTo.Decimal(dt.Compute("SUM(AM_EX_E)", "CD_PARTNER = '" + PartnerCode + "' AND NO_LINE < 90000 AND TP_BOM <> 'P'"));	// 매입견적금액(외화)
			decimal amountExP = GetTo.Decimal(dt.Compute("SUM(AM_EX_P)", "CD_PARTNER = '" + PartnerCode + "' AND NO_LINE < 90000 AND TP_BOM <> 'P'"));	// 매입금액(외화)
			decimal dcAmountEx = amountExE - amountExP;
			decimal amountExtra = GetTo.Decimal(dt.Compute("SUM(AM_EX_P)", "CD_PARTNER = '" + PartnerCode + "' AND NO_LINE > 90000"));
			decimal amountTotal = amountExP + amountExtra;

			lbl통화EX.Text = GetCon.Text(cbo통화);
			lbl매입견적금액EX.Text = string.Format("{0:#,##0.##}", amountExE);
			lblDC금액EX.Text = string.Format("{0:#,##0.##}", dcAmountEx);
			lbl매입금액EX.Text = string.Format("{0:#,##0.##}", amountExP);
			lbl부대비용EX.Text = string.Format("{0:#,##0.##}", amountExtra);
			lbl합계EX.Text = string.Format("{0:#,##0.##}", amountTotal);

			// Head 정보 변경
			grd헤드["AM_EX_E"] = amountExE;
			grd헤드["RT_DC_P"] = dcRate;
			grd헤드["AM_DC_P"] = dcAmountEx;
			grd헤드["AM_EX_P"] = amountExP;
		}

		#endregion

		#region ==================================================================================================== 기타

		private void SetGridStyle(FlexGrid flexGrid)
		{
			// ********** 헤더 그리드
			if (flexGrid == grd헤드)
			{
				for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
				{
					// SRM 상태값에 따라 색상 변경
					if (grd헤드[i, "SRM_STATUS"].ToString() == "S")		grd헤드.SetCellColor(i, "SRM_STATUS", Color.Red);
					else if (grd헤드[i, "SRM_STATUS"].ToString() == "C")	grd헤드.SetCellColor(i, "SRM_STATUS", Color.Blue);

					// 첨부파일 유무 표시
					if (grd헤드[i, "FILE_LIST"].ToString() == "Y")	grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, Icons.Popup);
					else											grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, null);
				}
			}

			// ********** 라인 그리드
			if (flexGrid == grd라인)
			{
				lineToRowIndex.Clear();

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					grd라인.Rows[i].AllowEditing = true;

					// BOM에 따른 아이콘 추가
					if (grd라인[i, "TP_BOM"].ToString() == "P")
					{
						grd라인.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.FolderExpand);
						grd라인.Rows[i].AllowEditing = false;
						lineToRowIndex.Add(GetTo.Int(grd라인[i, "NO_LINE"]), i);	// 빠른 Sub Total 계산을 위해 사전 추가
					}
					else if (grd라인[i, "TP_BOM"].ToString() == "S")
					{
						grd라인.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
					}
					else if (grd라인[i, "TP_BOM"].ToString() == "C")
					{
						grd라인.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_20x6);
					}					

					// U코드가 바뀐 경우 경고 표시
					if (GetTo.String(grd라인[i, "UCODE"]) != grd라인[i, "UCODE_OLD"].ToString())
					{
						SetGrid.CellRed(grd라인, i, grd라인.Cols["CD_ITEM_PARTNER"].Index);
						SetGrid.CellRed(grd라인, i, grd라인.Cols["NM_ITEM_PARTNER"].Index);
					}
					else
					{
						SetGrid.CellRed(grd라인, i, grd라인.Cols["CD_ITEM_PARTNER"].Index, "");
						SetGrid.CellRed(grd라인, i, grd라인.Cols["NM_ITEM_PARTNER"].Index, "");
					}

					// PLATE NO가 바뀐 경우 경고 표시					
					SetGrid.CellRed(grd라인, i, grd라인.Cols["NO_PLATE"].Index, "");
					SetGrid.CellRed(grd라인, i, grd라인.Cols["NM_PLATE"].Index, "");

					if (GetTo.String(grd라인[i, "NO_PLATE"]) != "")
					{
						if (GetTo.String(grd라인[i, "CD_ITEM_PARTNER"]) != GetTo.String(grd라인[i, "NO_PLATE"]))
						{
							SetGrid.CellRed(grd라인, i, grd라인.Cols["NO_PLATE"].Index);
							SetGrid.CellRed(grd라인, i, grd라인.Cols["NM_PLATE"].Index);
						}
					}
				}
			}
		}

		#endregion
	}
}

