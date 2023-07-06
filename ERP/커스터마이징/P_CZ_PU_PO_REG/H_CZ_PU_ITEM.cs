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

namespace cz
{
	public partial class H_CZ_PU_ITEM : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_FILE { get; set; }

		public string XML { get; set; }

		public DataTable Items
		{
			get
			{
				return flexL.GetCheckedRows("CHK");
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_PU_ITEM(string NO_FILE, string XML)
		{
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();

			this.NO_FILE = NO_FILE;
			this.XML = XML;
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

		}

		private void InitGrid()
		{
			flexL.BeginSetting(1, 1, false);

			flexL.SetCol("CHK"				, "S"			, 30	, true	, CheckTypeEnum.Y_N);
			flexL.SetCol("NO_PO"			, "파일번호"		, false);
			flexL.SetCol("NO_LINE"			, "고유번호"		, false);
			flexL.SetCol("NO_DSP"			, "순번"			, 40);
			flexL.SetCol("NM_SUBJECT"		, "주제"			, false);
			flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			flexL.SetCol("CD_ITEM"			, "재고코드"		, 80);
			flexL.SetCol("CD_UNIT_MM"		, "단위"			, 45);
			flexL.SetCol("QT_PO"			, "수량"			, 50	, 6		, true	, typeof(decimal), FormatTpType.QUANTITY);		// EDIT
			flexL.SetCol("QT_RAW"			, "수량RAW"		, false);
			flexL.SetCol("UM_EX_STD"		, "매입견적단가"	, 100	, 11	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_STD"		, "매입견적금액"	, 100	, 11	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("RT_DC"			, "DC(%)"		, 55	, 4		, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX"			, "매입단가"		, 100	, 11	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX"			, "매입금액"		, 100	, 11	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM"				, "매입단가(￦)"	, 100	, 11	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM"				, "매입금액(￦)"	, 100	, 11	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("VAT"				, "부가세"		, 100	, 11	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("LT"				, "납기"			, 50	, 3		, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("NO_SO"			, "수주번호"		, false);
			flexL.SetCol("NO_SOLINE"		, "수주항번"		, false);
			flexL.SetCol("TP_ROW"			, "행구분"		, false);
			flexL.SetCol("SORT"				, "SORT"		, false);

			flexL.Cols["NO_DSP"].Format = "####.##";
			flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["CD_UNIT_MM"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDataMap("CD_UNIT_MM", Util.GetDB_CODE("MA_B000004"), "CODE", "NAME");

			flexL.KeyActionEnter = KeyActionEnum.MoveDown;
			flexL.SettingVersion = "15.12.24.01";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexL.SetExceptSumCol("NO_DSP", "UM_EX_STD", "RT_DC", "UM_EX", "UM", "LT");

			FlexUtil.AddEditStyle(flexL, "QT_PO");
		}

		private void InitEvent()
		{
			btn조회.Click += new EventHandler(btn조회_Click);
			btn확인.Click += new EventHandler(btn확인_Click);

			flexL.ValidateEdit += new ValidateEditEventHandler(flexL_ValidateEdit);
		}

		protected override void InitPaint()
		{
			btn조회_Click(null, null);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape) return false;

			return base.ProcessCmdKey(ref msg, keyData);
		}

		#endregion

		#region ==================================================================================================== Event

	

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt = DBMgr.GetDataTable("SP_CZ_PU_POL_REG_SELECT_SO", new object[] { CD_COMPANY, NO_FILE, DBNull.Value, DBNull.Value, XML });
			flexL.Binding = dt;
			if (!flexL.HasNormalRow) Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		private void btn확인_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			if (flexL.Row >= flexL.Rows.Fixed) flexL.Rows.Remove(flexL.Row);
		}

		private void btn저장_Click(object sender, EventArgs e)
		{
			if (!Global.MainFrame.VerifyGrid(flexL)) return;

			// H 저장
			DataTable dtC = flexL.GetChanges();
			if (dtC != null)
			{
				string xmlC = Util.GetTO_Xml(dtC);
				DBMgr.ExecuteNonQuery("SP_CZ_HR_CAR_OIL_CODE_XML", new object[] { xmlC });
			}

			flexL.AcceptChanges();
			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			this.DialogResult = DialogResult.OK;
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			//string COLNAME = flexL.Cols[e.Col].Name;

			//if (COLNAME == "QT_PO" || COLNAME == "UM_EX_STD" || COLNAME == "RT_DC")
			//{
			//    CalcRow(e.Row, COLNAME);
			//}

			decimal QT_PO  = Util.GetTO_Decimal(flexL[e.Row, "QT_PO"]);
			decimal QT_RAW = Util.GetTO_Decimal(flexL[e.Row, "QT_RAW"]);

			if (QT_PO > QT_RAW)
			{
				Global.MainFrame.ShowMessage("CZ_@ 은(는) @ 보다 작거나 같아야 합니다.", new object[] { Global.MainFrame.DD("발주수량"), Global.MainFrame.DD("발주잔량") });
				e.Cancel = true;
				SendKeys.Send("{ESC}");
				return;
			}
		}

		#endregion
	}
}
