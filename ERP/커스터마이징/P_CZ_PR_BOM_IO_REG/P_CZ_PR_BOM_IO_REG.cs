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


namespace cz
{
	public partial class P_CZ_PR_BOM_IO_REG : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PR_BOM_IO_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
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
			txt포커스.Left = -500;

			MainGrids = new FlexGrid[] { flexH };
			flexH.DetailGrids = new FlexGrid[] { flexL };
		}

		private void InitGrid()
		{
			// ========== PARENT
			flexH.BeginSetting(1, 1, false);

			flexH.SetCol("CHK"				, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexH.SetCol("NO_FILE"			, "파일번호"		, 90	, true);
			flexH.SetCol("NO_LINE"			, "고유번호"		, false);
			flexH.SetCol("NO_DSP"			, "순번"			, 45	, true);
			flexH.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			flexH.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 230);			
			flexH.SetCol("UNIT"				, "단위"			, 50);
			flexH.SetCol("QT_SO"			, "수주수량"		, 60	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexH.SetCol("QT_PR"			, "생산수량"		, 60	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexH.SetCol("CD_LOCATION"		, "로케이션"		, 70);
			flexH.SetCol("CD_ITEM"			, "재고코드"		, 90);

			flexH.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["NO_DSP"].Format = "####.##";			
			flexH.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["CD_LOCATION"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.SetDataMap("UNIT", MA.GetCode("MA_B000004"), "CODE", "NAME");

			flexH.SettingVersion = "15.09.18.01";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ========== CHILDREN
			flexL.BeginSetting(1, 1, false);

			flexL.SetCol("NO_FILE"			, "파일번호"		, false);
			flexL.SetCol("NO_LINE"			, "고유번호"		, false);
			flexL.SetCol("NO_LINE_PARENT"	, "부모번호"		, false);
			flexL.SetCol("NO_DSP"			, "순번"			, 45);
			flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 230);
			flexL.SetCol("UNIT"				, "단위"			, 50);
			flexL.SetCol("QT"				, "수량"			, 60	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("CD_ITEM"			, "재고코드"		, 90);

			flexL.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NO_DSP"].Format = "####.##";
			flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDataMap("UNIT", MA.GetCode("MA_B000004"), "CODE", "NAME");

			flexL.SettingVersion = "15.09.18.01";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
		}

		protected override void InitPaint()
		{
			txt포커스.Focus();
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter = "NO_FILE = '" + flexH["NO_FILE"] + "' AND NO_LINE_PARENT = " + flexH["NO_LINE"];
			DataTable dt = null;
			flexL.Redraw = false;

			if (flexH.DetailQueryNeed) dt = DBHelper.GetDataTable("SP_CZ_PR_BOM_IOL_REG_SELECT", new object[] { CD_COMPANY, flexH["NO_FILE"], flexH["NO_LINE"] });

			flexL.BindingAdd(dt, filter);
			flexL.Redraw = true;
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt = SearchH();
			flexH.Binding = dt;
			if (!flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		private DataTable SearchH()
		{
			return DBHelper.GetDataTable("SP_CZ_PR_BOM_IOH_REG_SELECT", new object[] { CD_COMPANY });
		} 

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			ShowMessage(PageResultMode.SaveGood);
		}

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			return true;
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			DataTable dtH = flexH.GetCheckedRows("CHK");
			SpInfo si = Util.SetSpInfo(dtH, "SP_CZ_PR_BOM_IOH_REG_XML", "I");
			DBHelper.Save(si);
			
			// 재바인딩
			DataTable dt = SearchH();
			flexH.Binding = dt;

			return true;
		}

		#endregion
	}
}
