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
using Duzon.Common.Util;
using Dintec;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;

namespace cz
{
	public partial class P_CZ_MA_HGS_ORDER_UTIL : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string EngineModel { get; set; }

		public DataTable UCodes { get; set; }

		public string SelectedImoNumber
		{
			get
			{
				return flexHull["NO_IMO"].ToString();
			}
		}

		public int SelectedEngineNumber
		{
			get
			{
				return flexHull["NO_ENGINE"].ToInt();
			}
		}

		public DataTable SelectedUCodes
		{
			get
			{
				return flexItem.GetTableFromGrid();
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_MA_HGS_ORDER_UTIL()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
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
			flexHull.DetailGrids = new FlexGrid[] { flexItem };
		}

		private void InitGrid()
		{
		    // ================================================== U코드
		    flexUCode.BeginSetting(1, 1, false);
						
		    flexUCode.SetCol("U_CODE"		, "U코드"		, 120);

			flexUCode.KeyActionEnter = KeyActionEnum.MoveDown;
			flexUCode.SettingVersion = "17.01.05.01";
			flexUCode.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flexUCode.Rows[0].Height = 28;

		    // ================================================== 도면번호검색
			flexSearchDrawing.BeginSetting(1, 1, false);

		    flexSearchDrawing.SetCol("CD_SPEC"		, "U코드"		, 120);
		    flexSearchDrawing.SetCol("NO_DRAWING"	, "도면번호"		, 200);
		    flexSearchDrawing.SetCol("CNT"			, "실적건수"		, 150);
		    flexSearchDrawing.SetCol("CHK"			, "중복"			, 100);

			flexSearchDrawing.KeyActionEnter = KeyActionEnum.MoveDown;
			flexSearchDrawing.SettingVersion = "17.01.05.01";
			flexSearchDrawing.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flexSearchDrawing.Rows[0].Height = 28;

			// ================================================== 확정도면번호
			flexConfirmDrawing.BeginSetting(1, 1, false);

		    flexConfirmDrawing.SetCol("NO_DRAWING"	, "도면번호"		, 200);

			flexConfirmDrawing.KeyActionEnter = KeyActionEnum.MoveDown;
			flexConfirmDrawing.SettingVersion = "17.01.05.01";
			flexConfirmDrawing.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flexConfirmDrawing.Rows[0].Height = 28;

			// ================================================== 호선순위
			flexHull.BeginSetting(1, 1, false);

			flexHull.SetCol("NO_IMO"	,	"IMO번호"	, 200);            
			flexHull.SetCol("NO_ENGINE"	,	"엔진번호"	, 60);
            flexHull.SetCol("NM_MODEL"	,	"모델"    	, 80);
			flexHull.SetCol("CNT"		,	"아이템수"	, 60);

			flexHull.KeyActionEnter = KeyActionEnum.MoveDown;
			flexHull.SettingVersion = "17.03.07.01";
			flexHull.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flexHull.Rows[0].Height = 28;

			// ================================================== 확정도면번호
			flexItem.BeginSetting(1, 1, false);

			flexItem.SetCol("NO_IMO"		, "IMO번호"	, false);
			flexItem.SetCol("NO_ENGINE"		, "엔진번호"	, false);
			flexItem.SetCol("NO_PLATE"		, "부품코드"	, 120);
			flexItem.SetCol("NM_PLATE"		, "부품명"	, 200);
			flexItem.SetCol("UNIT"			, "단위"		, 50);
			flexItem.SetCol("UCODE"			, "U코드"	, 200);
			flexItem.SetCol("NO_DRAWING"	, "도면번호"	, 200);

			flexItem.KeyActionEnter = KeyActionEnum.MoveDown;
			flexItem.SettingVersion = "17.02.09.01";
			flexItem.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flexItem.Rows[0].Height = 28;
		}

		private void InitEvent()
		{
			btnUCode.Click += new EventHandler(btnUCode_Click);
			btnNoDrawing.Click += new EventHandler(btnNoDrawing_Click);
			flexHull.AfterRowChange += new RangeEventHandler(flexHull_AfterRowChange);			
		}

		protected override void InitPaint()
		{

		}

		#endregion

		#region ==================================================================================================== Event

		

		private void btnNoDrawing_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDlg = new OpenFileDialog();
			fileDlg.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

			if (fileDlg.ShowDialog() != DialogResult.OK) return;

			ExcelReader excel = new ExcelReader();
			DataTable dtExcel = excel.Read(fileDlg.FileName, 1, 2);

			if (dtExcel.Rows.Count == 0)
			{
				ShowMessage("엑셀파일을 읽을 수 없습니다.");
				return;
			}

			dtExcel.Columns[0].ColumnName = "NO_DRAWING";

			// 조회
			DataSet ds = DBMgr.GetDataSet("PS_CZ_MA_HGS_ORDER_UTIL_H", new object[] { DBNull.Value, GetTo.Xml(dtExcel) });
		
			flexConfirmDrawing.Binding = ds.Tables[2];
			flexHull.Binding = ds.Tables[3];
		}


		#endregion

		


		#region ==================================================================================================== Search

		public void Search()
		{
			//// 외부에서의 검색은 u코드 기반 검색을 함
			//dtExcel.Columns[0].ColumnName = "U_CODE";

			//// 조회
			//DataSet ds = DBMgr.GetDataSet("PS_CZ_MA_HGS_ORDER_UTIL_H", new object[] { GetTo.Xml(dtExcel), DBNull.Value });

			//flexUCode.Binding = ds.Tables[0];
			//flexSearchDrawing.Binding = ds.Tables[1];
			//flexConfirmDrawing.Binding = ds.Tables[2];
			//flexHull.Binding = ds.Tables[3];

			SQL sql = new SQL("PS_CZ_MA_HULL_CODE_COUNTER_H", SQLType.Procedure);
			sql.Parameter.Add2("@XML_U"		, UCodes.ToXml("UCODE"));
			sql.Parameter.Add2("@NM_MODEL"	, EngineModel);
			DataTable dt = sql.GetDataTable();

			flexHull.Binding = dt;
		}

		private void btnUCode_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDlg = new OpenFileDialog();
			fileDlg.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

			if (fileDlg.ShowDialog() != DialogResult.OK) return;

			ExcelReader excel = new ExcelReader();
			DataTable dtExcel = excel.Read(fileDlg.FileName);

			if (dtExcel.Rows.Count == 0)
			{
				ShowMessage("엑셀파일을 읽을 수 없습니다.");
				return;
			}

			dtExcel.Columns[0].ColumnName = "U_CODE";

			// 조회
			DataSet ds = DBMgr.GetDataSet("PS_CZ_MA_HGS_ORDER_UTIL_H", new object[] { GetTo.Xml(dtExcel), DBNull.Value });

			flexUCode.Binding = ds.Tables[0];
			flexSearchDrawing.Binding = ds.Tables[1];
			flexConfirmDrawing.Binding = ds.Tables[2];
			flexHull.Binding = ds.Tables[3];
		}

		private void flexHull_AfterRowChange(object sender, RangeEventArgs e)
		{
			string NO_IMO = GetTo.String(flexHull["NO_IMO"]);
			int NO_ENGINE = GetTo.Int(flexHull["NO_ENGINE"]);

			DataTable dtL = null;

			if (flexHull.DetailQueryNeed)
			{
				if (UCodes != null && UCodes.Rows.Count > 0)
				{
					dtL = DBMgr.GetDataTable("PS_CZ_MA_HULL_CODE_COUNTER_L", NO_IMO, NO_ENGINE, UCodes.ToXml("UCODE"));
				}
				else
					dtL = DBMgr.GetDataTable("PS_CZ_MA_HGS_ORDER_UTIL_L", new object[] { NO_IMO, NO_ENGINE, GetTo.Xml(flexConfirmDrawing.DataTable) });
		}

		flexItem.BindingAdd(dtL, "NO_IMO = '" + NO_IMO + "' AND NO_ENGINE = " + NO_ENGINE);
		}

	#endregion

}
}
