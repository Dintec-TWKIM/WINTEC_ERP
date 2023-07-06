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
using System.Text.RegularExpressions;

// 20190917
namespace cz
{
	public partial class P_CZ_SA_STOCK : PageBase
	{
		#region ===================================================================================================== Property

		private P_CZ_SA_QTN_REG Quotation
		{
			get
			{
				return (P_CZ_SA_QTN_REG)this.Parent.GetContainerControl();
			}
		}

		public FlexGrid 그리드헤드
		{
			get
			{
				return grd라인;
			}
		}		

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_STOCK()
		{
			//StartUp.Certify(this);
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
			grd라인.DetailGrids = new FlexGrid[] { grd출고예약, grd입고예약, grd미입고 };
		}

		private void InitGrid()
		{
			grd라인.BeginSetting(2, 1, false);

			grd라인.SetCol("NO_FILE"			, "파일번호"		, false);
			grd라인.SetCol("NO_LINE"			, "항번"			, false);
			grd라인.SetCol("YN_STOCK"		, "S"			, 30	, true	, CheckTypeEnum.Y_N);
			grd라인.SetCol("NO_DSP"			, "순번"			, 40);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 140);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 190);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 80);
			grd라인.SetCol("NM_ITEM"			, "재고명"		, 180);
			grd라인.SetCol("NO_PART"			, "파트넘버"		, false);
			grd라인.SetCol("UCODE"			, "U코드"		, false);
			grd라인.SetCol("KCODE"			, "K코드"		, false);

			grd라인.SetCol("QT_INV"			, "현재고"		, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_BOOK"			, "예약"			, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_AVST"			, "가용"			, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_NGR"			, "미입고"		, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_HOLD"			, "예약"			, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_AVGR"			, "가용"			, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grd라인.SetCol("QT_QTN"			, "수량"			, 47	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grd라인.SetCol("UM_KR_P"			, "원화단가"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_P"			, "원화금액"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("UM_KR_S"			, "원화단가"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_S"			, "원화금액"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);

			grd라인.SetCol("RT_MARGIN"		, "마진\n(%)"	, 40	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("LT"				, "납기"			, 40	, true	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("LT_ORG"			, "납기(*)"		, false);
			grd라인.SetCol("DC_STOCK"		, "재고비고"		, 165	, true);
			grd라인.SetCol("DC_RMK"			, "일반비고"		, 165	, true);
			
			grd라인.SetCol("STAND_PRC"		, "평균단가"		, 75	, false	, typeof(decimal), FormatTpType.MONEY);

			grd라인.SetCol("FILE_ICON_DOC"	, "서류"			, 35);			
			grd라인.SetCol("FILE_NAME_DOC"	, "첨부파일_DOC"	, false);
			grd라인.SetCol("FILE_COUNT_DOC"	, "첨부개수_DOC"	, false);

			grd라인.SetCol("FILE_ICON_IMG"	, "사진"			, 35);
			grd라인.SetCol("FILE_NAME_IMG"	, "첨부파일_IMG"	, false);
			grd라인.SetCol("FILE_COUNT_IMG"	, "첨부개수_IMG"	, false);

			grd라인[0, grd라인.Cols["UM_KR_P"].Index] = "매입단가";
			grd라인[0, grd라인.Cols["AM_KR_P"].Index] = "매입단가";

			grd라인[0, grd라인.Cols["UM_KR_S"].Index] = "매출단가";
			grd라인[0, grd라인.Cols["AM_KR_S"].Index] = "매출단가";

			grd라인[0, grd라인.Cols["QT_INV"].Index] = "재고수량";
			grd라인[0, grd라인.Cols["QT_BOOK"].Index] = "재고수량";
			grd라인[0, grd라인.Cols["QT_AVST"].Index] = "재고수량";
			grd라인[0, grd라인.Cols["QT_NGR"].Index] = "미입고수량";
			grd라인[0, grd라인.Cols["QT_HOLD"].Index] = "미입고수량";
			grd라인[0, grd라인.Cols["QT_AVGR"].Index] = "미입고수량";

			grd라인.Cols["NO_DSP"].Format = "####.##";
			grd라인.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["FILE_ICON_DOC"].ImageAlign = ImageAlignEnum.CenterCenter;
			grd라인.Cols["FILE_ICON_IMG"].ImageAlign = ImageAlignEnum.CenterCenter;
			
			grd라인.SetDefault("19.06.20.05", SumPositionEnum.Top);
			grd라인.SetEditColumn("LT", "DC_STOCK", "DC_RMK");
			grd라인.SetExceptSumCol("UM_KR_P", "UM_KR_S", "RT_MARGIN", "LT", "STAND_PRC");
			grd라인.SetSumColumnStyle("QT_AVST", "QT_AVGR", "QT_QTN");
			grd라인.Styles.Add("ACTIVE").ForeColor = Color.Blue;
			grd라인.Styles.Add("INACTIVE").ForeColor = Color.LightGray;
			grd라인.LoadUserCache("P_CZ_SA_STOCK");

			// 수량컬럼 색상
			grd라인.Cols["QT_QTN"].Style.Font = new Font(grd라인.Font, FontStyle.Bold);
			grd라인.Cols["QT_QTN"].Style.ForeColor = Color.Green;

			CellStyle blueStyle = grd라인.Styles.Add("BLUE");
			blueStyle.Font = new Font(grd라인.Font, FontStyle.Bold);
			blueStyle.ForeColor = Color.Blue;

			CellStyle redStyle = grd라인.Styles.Add("RED");
			redStyle.Font = new Font(grd라인.Font, FontStyle.Bold);
			redStyle.ForeColor = Color.Red;

			// 현재고, 미입고 수량 숨겨보자. 왠지 안볼거 같다
			grd라인.Cols["QT_INV"].Visible = false;
			grd라인.Cols["QT_NGR"].Visible = false;

			// ========== Booking 현황
			grd출고예약.BeginSetting(1, 1, false);

			grd출고예약.SetCol("NO_SO"			, "수주번호"		, 100);
			grd출고예약.SetCol("NO_DSP"			, "순번"			, 40);
			grd출고예약.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd출고예약.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd출고예약.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd출고예약.SetCol("NO_LINE"			, "항번"			, false);
			grd출고예약.SetCol("LN_PARTNER"		, "매출처"		, 180);
			grd출고예약.SetCol("NM_VESSEL"		, "선명"			, 180);
			grd출고예약.SetCol("NO_PO_PARTNER"	, "주문번호"		, 100);			
			grd출고예약.SetCol("NM_EMP"			, "담당자"		, 80);
			grd출고예약.SetCol("DT_DUEDATE"		, "납품예정일"	, 90	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd출고예약.SetCol("QT_SO"			, "수주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);			
			grd출고예약.SetCol("QT_BOOK"			, "예약수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd출고예약.SetCol("NO_GIR"			, "송품협조전"	, 90);
			grd출고예약.SetCol("NO_PACK"			, "포장협조전"	, 90);

			grd출고예약.Cols["NO_DSP"].Format = "####.##";
			grd출고예약.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd출고예약.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;

			grd출고예약.SetDefault("18.07.31.01", SumPositionEnum.None);
			grd출고예약.LoadUserCache("P_CZ_SA_QTN_REG_STOCK");

			// ========== Holding 현황
			grd입고예약.BeginSetting(1, 1, false);
				
			grd입고예약.SetCol("NO_SO"			, "수주번호"		, 100);
			grd입고예약.SetCol("NO_DSP"			, "순번"			, 40);
			grd입고예약.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd입고예약.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd입고예약.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd입고예약.SetCol("NO_LINE"			, "항번"			, false);
			grd입고예약.SetCol("LN_PARTNER"		, "매출처"		, 180);
			grd입고예약.SetCol("NM_VESSEL"		, "선명"			, 180);
			grd입고예약.SetCol("NO_PO_PARTNER"	, "주문번호"		, 100);			
			grd입고예약.SetCol("NM_EMP"			, "담당자"		, 80);
			grd입고예약.SetCol("DT_DUEDATE"		, "납품예정일"	, 90	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd입고예약.SetCol("QT_SO"			, "수주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd입고예약.SetCol("QT_HOLD"			, "예약수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
				
			grd입고예약.Cols["NO_DSP"].Format = "####.##";
			grd입고예약.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd입고예약.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;

			grd입고예약.SetDefault("18.07.31.01", SumPositionEnum.None);
			grd입고예약.LoadUserCache("P_CZ_SA_QTN_REG_STOCK");

			// ========== 미입고
			grd미입고.BeginSetting(1, 1, false);
			
			grd미입고.SetCol("NO_PO"				, "발주번호"		, 100);
			grd미입고.SetCol("NO_LINE"			, "순번"			, 40);
			grd미입고.SetCol("NO_PART"			, "품목코드"		, 120);
			grd미입고.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd미입고.SetCol("LN_PARTNER"		, "매입처"		, 180);
			grd미입고.SetCol("NO_ORDER"			, "공사번호"		, 100);
			grd미입고.SetCol("NM_VESSEL2"		, "호선"			, 150);

			grd미입고.SetCol("NM_EMP"			, "담당자"		, 80);
			grd미입고.SetCol("DT_LIMIT"			, "예정납기일"	, 90	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);					
			grd미입고.SetCol("DT_AVERAGE"		, "기대납기일"	, 90	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd미입고.SetCol("WEIGHTED_MEAN"		, "평균LT"		, 58);
			grd미입고.SetCol("STD_DEVIATION"		, "표준편차"		, 58);
			grd미입고.SetCol("DT_EXPECT"			, "확약일"		, 90	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd미입고.SetCol("DT_EXDATE"			, "반출일"		, 90	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd미입고.SetCol("QT_PO"				, "발주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd미입고.SetCol("QT_NONGR"			, "미입고"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd미입고.SetCol("UM_EX"				, "매입단가"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd미입고.SetCol("DC4"				, "비고"			, 200	, true);
			grd미입고.SetCol("DC_RMK_TEXT"		, "발주비고"		, 200	, true);

			grd미입고.Cols["NO_LINE"].Format = "####.##";
			grd미입고.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
			grd미입고.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd미입고.Cols["WEIGHTED_MEAN"].TextAlign = TextAlignEnum.RightCenter;
			grd미입고.Cols["STD_DEVIATION"].TextAlign = TextAlignEnum.RightCenter;

			grd미입고.SetDefault("20.10.14.01", SumPositionEnum.None);
			grd미입고.LoadUserCache("P_CZ_SA_QTN_REG_STOCK");
		}

		private void InitEvent()
		{
			btn시뮬레이션.Click += new EventHandler(btn시뮬레이션_Click);

			rdo파일번호.CheckedChanged += new EventHandler(rdoType_CheckedChanged);
			rdo재고코드.CheckedChanged += new EventHandler(rdoType_CheckedChanged);

			grd라인.AfterRowChange += new RangeEventHandler(grd라인_AfterRowChange);
			grd라인.DoubleClick += new EventHandler(grd라인_DoubleClick);
			grd라인.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);
			grd미입고.ValidateEdit += new ValidateEditEventHandler(grd미입고_ValidateEdit);
		}
		
		protected override void InitPaint()
		{
			if (!Certify.IsLive())
			{
				grd라인.Cols.Remove("STND_DETAIL_ITEM");
				grd라인.Cols.Remove("NO_STND");
			}
		}

		public void Clear()
		{
			// 검색원그리드
			rdo파일번호.Checked = true;
			tbx재고코드.Text = "";
			rdo시뮬레이션1.Checked = true;
			rdo재고수량1.Checked = true;

			// 그리드
			grd라인.Clear(false);
			grd출고예약.Clear(false);
			grd입고예약.Clear(false);
			grd미입고.Clear(false);

			// 라벨			
			lblPuAmountKr.Text = "0";
			lblSaAmountKr.Text = "0";

			lblMaAmount.Text = "0";
			lblMaRate.Text = "0";
		}

		public void 사용(bool enabled)
		{
			pnl버튼.Editable(enabled);
			one검색.Editable(enabled);
			grd라인.AllowEditing = enabled;			
		}

		#endregion

		#region ==================================================================================================== Event

		private void rdoType_CheckedChanged(object sender, EventArgs e)
		{
			if (rdo파일번호.Checked)
			{
				tbx재고코드.Enabled = false;
				pnlSimulation.Enabled = true;

			}
			else
			{
				tbx재고코드.Enabled = true;
				pnlSimulation.Enabled = false;
			}
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn시뮬레이션_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (!grd라인.Rows[i].AllowEditing)
					continue;

				decimal 견적수량 = GetTo.Decimal(grd라인[i, "QT_QTN"]);
				decimal 가용재고 = GetTo.Decimal(grd라인[i, "QT_AVST"]);
				decimal 가용입고 = GetTo.Decimal(grd라인[i, "QT_AVGR"]);
				decimal 단가 = GetTo.Decimal(grd라인[i, "STAND_PRC"]);

				if (가용재고 < 0) 가용재고 = 0;
				if (가용입고 < 0) 가용입고 = 0;

				// 수동이 아닌경우는 일단 체크 품
				if (!rdo시뮬레이션3.Checked)
					grd라인[i, "YN_STOCK"] = "N";

				// 비교수량 결정
				decimal 재고수량 = 0;

				if (rdo시뮬레이션1.Checked)		재고수량 = 가용재고;
				else if (rdo시뮬레이션2.Checked)	재고수량 = 가용재고 + 가용입고;

				// 시뮬레이션
				if (rdo재고수량1.Checked)
				{
					// 견적수량 50% 이상이면 체크
					if (재고수량 >= 견적수량 / 2)
					{
						grd라인[i, "YN_STOCK"] = "Y";
						grd라인[i, "LT"] = 0;
					}
				}
				else if (rdo재고수량2.Checked)
				{
					// 재고수량이 1개라도 있으면 체크
					if (재고수량 > 0)
					{
						grd라인[i, "YN_STOCK"] = "Y";
						grd라인[i, "LT"] = 0;
					}
				}

				// ********** 자동비고
				if (재고수량 > 0 && 재고수량 < 견적수량)
				{
					// 찍혀야할 비고
					string stock = string.Format("\r\n{0:#,##0}PCS:STOCK, {1:#,##0}PCS:{2}DAYS", 재고수량, (견적수량 - 재고수량), grd라인[i, "LT_ORG"]);

					// 기존 납기부분이 있으면 삭제 후 저장					
					string remark = grd라인[i, "DC_STOCK"].ToString();

					Regex regF = new Regex(@"\d+PCS:STOCK", RegexOptions.Compiled | RegexOptions.CultureInvariant);
					Regex regT = new Regex(@"\d+DAYS", RegexOptions.Compiled | RegexOptions.CultureInvariant);

					Match mchF = regF.Match(remark);
					Match mchT = regT.Match(remark);

					int idxF = remark.IndexOf(mchF.Value);
					int idxT = remark.IndexOf(mchT.Value) + mchT.Length;

					grd라인[i, "DC_STOCK"] = remark.Remove(idxF, idxT - idxF).Trim();

					// 기존 납기비고 삭제
					remark = grd라인[i, "DC_RMK"].ToString();

					mchF = regF.Match(remark);
					mchT = regT.Match(remark);

					idxF = remark.IndexOf(mchF.Value);					
					idxT = remark.IndexOf(mchT.Value) + mchT.Length;

					grd라인[i, "DC_RMK"] = remark.Remove(idxF, idxT - idxF).Trim();

					// 비고 바인딩
					if (grd라인[i, "YN_STOCK"].ToString() == "Y")
						grd라인[i, "DC_STOCK"] = (grd라인[i, "DC_STOCK"] + stock).Trim();
					else
						grd라인[i, "DC_RMK"] = (grd라인[i, "DC_RMK"] + stock).Trim();
				}
				
				// 재고 체크되어 있으면 마진 계산 (수동, 자동 둘다 함)
				if (grd라인[i, "YN_STOCK"].ToString() == "Y")
				{				
					// 재고단가 계산
					grd라인[i, "UM_KR_P"] = 단가;
					grd라인[i, "AM_KR_P"] = 단가 * 견적수량;
					grd라인[i, "RT_MARGIN"] = Calculator.이윤율계산(GetTo.Decimal(grd라인[i, "AM_KR_P"]), GetTo.Decimal(grd라인[i, "AM_KR_S"]));					
				}

				// 스타일 적용
				if (GetTo.String(grd라인[i, "YN_STOCK"]) == "Y")
					grd라인.Rows[i].Style = grd라인.Styles["ACTIVE"];
				else
					grd라인.Rows[i].Style = null;
			}

			합계계산();
			grd라인.AutoRowSize();
		}

		#endregion


		#region ==================================================================================================== 그리드 이벤트

		private void grd라인_DoubleClick(object sender, EventArgs e)
		{			
			// 헤더클릭
			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 첨부파일 아이콘 클릭
			string colName = grd라인.Cols[grd라인.Col].Name;

			if (colName.In("FILE_ICON_DOC", "FILE_ICON_IMG"))
			{
				string companyCode = Quotation.회사코드;
				string itemCode = grd라인["CD_ITEM"].ToString();
				int fileCount = GetTo.Int(grd라인[colName.Replace("ICON", "COUNT")]);
				
				if (fileCount == 1)
				{
					string file = @"Upload\P_CZ_MA_PITEM\" + companyCode + @"\" + itemCode + @"\" + grd라인[colName.Replace("ICON", "NAME")];
					FileMgr.Download(file, true);
				}
				else if (fileCount > 1)
				{
					string extCode = (colName == "FILE_ICON_DOC") ? "'.PDF', 'XLSX'" : "'.JPG', 'JPEG', '.PNG'";
					string query = @"
	  SELECT IMAGE1 AS FILE_NAME FROM MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND RIGHT(IMAGE1, 4) IN (" + extCode + @")
UNION SELECT IMAGE2 AS FILE_NAME FROM MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND RIGHT(IMAGE2, 4) IN (" + extCode + @")
UNION SELECT IMAGE3 AS FILE_NAME FROM MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND RIGHT(IMAGE3, 4) IN (" + extCode + @")
UNION SELECT IMAGE4 AS FILE_NAME FROM MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND RIGHT(IMAGE4, 4) IN (" + extCode + @")
UNION SELECT IMAGE5 AS FILE_NAME FROM MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND RIGHT(IMAGE5, 4) IN (" + extCode + @")
UNION SELECT IMAGE6 AS FILE_NAME FROM MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM AND RIGHT(IMAGE6, 4) IN (" + extCode + @")";

					DBMgr dbm = new DBMgr();
					dbm.Query = query;
					dbm.AddParameter("@CD_COMPANY"	, companyCode);
					dbm.AddParameter("@CD_ITEM"		, itemCode);

					DataTable dt = dbm.GetDataTable();
					dt.Columns.Add("FILE_TYPE", typeof(string), "'U'");
					dt.Columns.Add("FILE_PATH", typeof(string), @"'Upload\P_CZ_MA_PITEM\" + companyCode + @"\" + itemCode + "'");

					H_CZ_ATTACHMENT_LIST f = new H_CZ_ATTACHMENT_LIST(dt);
					f.ShowDialog();
				}
			}
		}

		private void grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;
			
			// 비활성화 Row인 경우 이벤트 취소
			//if (grd라인[e.Row, "CD_ITEM"].ToString() == "")
			//{
			//    e.Cancel = true;
			//    //SendKeys.Send("{ESC}");
			//    return;
			//}

			// 재고 체크시 색상 변경
			if (colName == "YN_STOCK")
			{
				if (grd라인.EditData == "Y")
					grd라인.Rows[e.Row].Style = grd라인.Styles["ACTIVE"];
				else
					grd라인.Rows[e.Row].Style = null;
			}
		}

		private void grd미입고_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;
			string colName = flexGrid.Cols[e.Col].Name;			

			// 비고 수정 및 DB 업데이트
			if (colName == "DC4")
			{
				//string query = 
				DBMgr dbm = new DBMgr();
				dbm.Query = "UPDATE PU_POL SET DC4 = @DC4 WHERE CD_COMPANY = @CD_COMPANY AND NO_PO = @NO_PO AND NO_LINE = @NO_LINE";
				dbm.AddParameter("@CD_COMPANY", Quotation.회사코드);
				dbm.AddParameter("@NO_PO"	  , flexGrid["NO_PO"]);
				dbm.AddParameter("@NO_LINE"	  , flexGrid["NO_LINE"]);
				dbm.AddParameter("@DC4"		  , flexGrid["DC4"]);
				dbm.ExecuteNonQuery();
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			// 조회
			DBMgr dbm = new DBMgr
			{
				DebugMode = DebugMode.Print
			,	Procedure = "PS_CZ_SA_QTN_STOCK_R2"
			};
			dbm.AddParameter("@CD_COMPANY", Quotation.회사코드);
			if (rdo파일번호.Checked)
				dbm.AddParameter("@NO_FILE", Quotation.파일번호);
			else if (rdo재고코드.Checked)
				dbm.AddParameter("@CD_ITEM", tbx재고코드.Text);
			else if (rdo공사번호.Checked)
				dbm.AddParameter("@NO_ORDER", tbx공사번호.Text);

			// 쿼리 실행 및 Linq 조인
			DataTable dt = GetDb.JoinStockQuantityR3(Quotation.회사코드, dbm.GetDataTable());

			foreach (DataRow row in dt.Rows)
			{
				if (row["YN_STOCK"].ToString() == "Y")
				{
					// 재고단가, 재고금액, 재고납기, 마진, 재고비고 바인딩
					row["UM_KR_P"] = row["UM_STOCK"];
					row["AM_KR_P"] = GetTo.Decimal(row["UM_STOCK"]) * GetTo.Decimal(row["QT_QTN"]);
					row["LT"] = row["LT_STOCK"];
					row["RT_MARGIN"] = Calculator.이윤율계산(GetTo.Decimal(row["AM_KR_P"]), GetTo.Decimal(row["AM_KR_S"]));
				}

				// 재고단가 세팅포맷에 따라 반올림
				row["STAND_PRC"] = string.Format("{0:" + grd라인.Cols["STAND_PRC"].Format + "}", GetTo.Decimal(row["STAND_PRC"]));
			}
			
			grd라인.DataBind(dt);			
			SetGridStyle();
			합계계산();
		}

		private void grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			string itemCode = GetTo.String(grd라인["CD_ITEM"]);
			DataTable dtBook = null;
			DataTable dtHold = null;
			DataTable dtNongr = null;
			
			if (grd라인.DetailQueryNeed)
			{
				dtBook = DBMgr.GetDataTable("PS_CZ_SA_STOCK_BOOK_BOOK", Quotation.회사코드, itemCode);
				dtHold = DBMgr.GetDataTable("SP_CZ_SA_STOCK_BOOK_SELECT_HOLD", Quotation.회사코드, itemCode);
				dtNongr = DBMgr.GetDataTable("SP_CZ_SA_STOCK_BOOK_SELECT_NONGR", Quotation.회사코드, itemCode);

				// 수주라인 추가 => 요고해야 동일 재고코드일때 더블데이터가 안생김
				//dtBook.Columns.Add("SEQ_SO
			}

			grd출고예약.BindingAdd(dtBook, "CD_ITEM = '" + itemCode + "'");
			grd입고예약.BindingAdd(dtHold, "CD_ITEM = '" + itemCode + "'");
			grd미입고.BindingAdd(dtNongr, "CD_ITEM = '" + itemCode + "'");
		}

		#endregion

		#region ==================================================================================================== Save

		public bool 저장()
		{
			DataTable dt = grd라인.GetChanges();
			string xml = GetTo.Xml(dt);
			DBMgr.ExecuteNonQuery("PX_CZ_SA_QTN_STOCK", xml);
			grd라인.AcceptChanges();

			return true;
		}
		
		#endregion

		#region ==================================================================================================== 종료

		public void Exit()
		{
			grd라인.SaveUserCache("P_CZ_SA_QTN_REG_STOCK");
			grd출고예약.SaveUserCache("P_CZ_SA_QTN_REG_STOCK");
			grd입고예약.SaveUserCache("P_CZ_SA_QTN_REG_STOCK");
			grd미입고.SaveUserCache("P_CZ_SA_QTN_REG_STOCK");
		}

		#endregion

		private void SetGridStyle()
		{
			grd라인.Redraw = false;

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				// 재고체크 및 재고여부에 따라 색상 표시
				if (grd라인[i, "YN_STOCK"].ToString() == "Y")
				{
					grd라인.Rows[i].AllowEditing = true;
					grd라인.Rows[i].Style = grd라인.Styles["ACTIVE"];					
				}
				else if (grd라인[i, "CD_ITEM"].ToString() == "")
				{
					grd라인.Rows[i].AllowEditing = false;
					grd라인.Rows[i].Style = grd라인.Styles["INACTIVE"];					
				}
				else
				{
					grd라인.Rows[i].AllowEditing = true;
					grd라인.Rows[i].Style = null;
				}

				// 수량이 충분히 있으면 파란색, 모지라면 빨간색
				decimal 견적수량 = GetTo.Decimal(grd라인[i, "QT_QTN"]);
				decimal 가용재고 = GetTo.Decimal(grd라인[i, "QT_AVST"]);
				decimal 가용입고 = GetTo.Decimal(grd라인[i, "QT_AVGR"]);

				if (가용재고 > 0)
				{
					if (가용재고 >= 견적수량 / 2)
						grd라인.SetCellStyle(i, grd라인.Cols["QT_AVST"].Index, "BLUE");
					else
						grd라인.SetCellStyle(i, grd라인.Cols["QT_AVST"].Index, "RED");
				}

				if (가용입고 > 0)
				{
					if (가용입고 >= 견적수량 / 2)
						grd라인.SetCellStyle(i, grd라인.Cols["QT_AVGR"].Index, "BLUE");
					else
						grd라인.SetCellStyle(i, grd라인.Cols["QT_AVGR"].Index, "RED");
				}

				// 서류 아이콘 표시
				int fileCountCb = GetTo.Int(grd라인[i, "FILE_COUNT_DOC"]);
				string fileName = grd라인[i, "FILE_NAME_DOC"].ToString();

				if (fileCountCb == 1)
					grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON_DOC"].Index, Icons.GetExtension(fileName.Substring(fileName.LastIndexOf(".") + 1)));
				else if (fileCountCb > 1)
					grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON_DOC"].Index, Icons.Popup);
				else
					grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON_DOC"].Index, null);

				// 이미지 아이콘 표시
				int fileCountImg = GetTo.Int(grd라인[i, "FILE_COUNT_IMG"]);

				if (fileCountImg == 1)
					grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON_IMG"].Index, Icons.GetExtension("jpg"));
				else if (fileCountImg > 1)
					grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON_IMG"].Index, Icons.Popup);
				else
					grd라인.SetCellImage(i, grd라인.Cols["FILE_ICON_IMG"].Index, null);
			}

			grd라인.Redraw = true;
		}

		// 합계
		private void 합계계산()
		{
			DataTable dt = grd라인.DataTable;

			decimal puAmount = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", ""));
			decimal saAmount = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", ""));
			decimal maAmount = saAmount - puAmount;
			decimal maRate = (saAmount > 0) ? 100 * (1 - puAmount / saAmount) : 0;

			// Label 정보 변경
			lblPuAmountKr.Text = string.Format("{0:#,##0.##}", puAmount);
			lblSaAmountKr.Text = string.Format("{0:#,##0.##}", saAmount);
			lblMaAmount.Text = string.Format("{0:#,##0.##}", maAmount);
			lblMaRate.Text = string.Format("{0:#,##0.##}", maRate);
		}
	}
}
