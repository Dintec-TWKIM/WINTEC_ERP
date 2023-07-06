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
	public partial class H_CZ_ITEM_ADD : Duzon.Common.Forms.CommonDialog
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

		public H_CZ_ITEM_ADD(string NO_FILE, string XML)
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
			btn더미.Left = -1000;
		}

		private void InitGrid()
		{
			flexL.BeginSetting(2, 1, false);

			flexL.SetCol("CHK"				, "S"			, 30	, true	, CheckTypeEnum.Y_N);
			flexL.SetCol("NO_SO"			, "파일번호"		, false);
			flexL.SetCol("SEQ_SO"			, "항번"			, false);
			flexL.SetCol("NO_DSP"			, "순번"			, 40);
			flexL.SetCol("GRP_ITEM"			, "유형"			, false);
			flexL.SetCol("NM_SUBJECT"		, "주제"			, false);
			flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 110);
			flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 230);
			flexL.SetCol("CD_ITEM"			, "재고코드"		, 80);
			flexL.SetCol("NM_ITEM"			, "재고명"		, false);
			flexL.SetCol("CD_SUPPLIER"		, "매입처코드"	, false);
			flexL.SetCol("LN_SUPPLIER"		, "매입처"		, 200);
			flexL.SetCol("UNIT_SO"			, "단위"			, 45);
			flexL.SetCol("QT_SO"			, "수량"			, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);						
			flexL.SetCol("UM_EX_P"			, "외화단가"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_P"			, "외화금액"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_P"			, "원화단가"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_P"			, "원화금액"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("RT_PROFIT"		, "이윤(%)"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX_Q"			, "외화단가"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_Q"			, "외화금액"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_Q"			, "원화단가"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_Q"			, "원화금액"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("RT_DC"			, "DC(%)"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX_S"			, "외화단가"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_S"			, "외화금액"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_KR_S"			, "원화단가"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_KR_S"			, "원화금액"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);	
			flexL.SetCol("RT_MARGIN"		, "최종(%)"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_VAT"			, "부가세"		, 100	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("RT_VAT"			, "부가세율(%)"	, false);
			flexL.SetCol("LT"				, "납기"			, 50	, false	, typeof(decimal), FormatTpType.MONEY);			
			flexL.SetCol("NO_PO_PARTNER"	, "주문번호"		, false);

			flexL[0, flexL.Cols["UM_EX_P"].Index] = "매입금액";
			flexL[0, flexL.Cols["AM_EX_P"].Index] = "매입금액";
			flexL[0, flexL.Cols["UM_KR_P"].Index] = "매입금액";
			flexL[0, flexL.Cols["AM_KR_P"].Index] = "매입금액";

			flexL[0, flexL.Cols["UM_EX_Q"].Index] = "매출견적금액";
			flexL[0, flexL.Cols["AM_EX_Q"].Index] = "매출견적금액";
			flexL[0, flexL.Cols["UM_KR_Q"].Index] = "매출견적금액";
			flexL[0, flexL.Cols["AM_KR_Q"].Index] = "매출견적금액";

			flexL[0, flexL.Cols["UM_EX_S"].Index] = "매출금액";
			flexL[0, flexL.Cols["AM_EX_S"].Index] = "매출금액";
			flexL[0, flexL.Cols["UM_KR_S"].Index] = "매출금액";
			flexL[0, flexL.Cols["AM_KR_S"].Index] = "매출금액";

			flexL.Cols["NO_DSP"].Format = "####.##";
			flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["UNIT_SO"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDataMap("UNIT_SO", Util.GetDB_CODE("MA_B000004"), "CODE", "NAME");

			flexL.KeyActionEnter = KeyActionEnum.MoveDown;
			flexL.SettingVersion = "16.10.19.01";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexL.SetExceptSumCol("NO_DSP", "QT_SO", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT");

			flexL.Rows[0].Height = 28;
			flexL.Rows[1].Height = 28;
		}

		private void InitEvent()
		{
			btn조회.Click += new EventHandler(btn조회_Click);
			btn저장.Click += new EventHandler(btn저장_Click);
			btn취소.Click += new EventHandler(btn취소_Click);
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

		#region ==================================================================================================== Search

		private void btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt = DBMgr.GetDataTable("PS_CZ_SA_SO_REG_L_QTN", new object[] { CD_COMPANY, NO_FILE, XML });
			dt.Columns.Add("CHK");
			flexL.Binding = dt;
			btn더미.Focus();

			if (!flexL.HasNormalRow) Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== Save

		private void btn저장_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		#endregion

		#region ==================================================================================================== 취소
			
		private void btn취소_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		#endregion		
	}
}
