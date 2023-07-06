using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using DX;
using System;
using System.Data;
using System.Drawing;
using System.Linq;

namespace cz
{
	public partial class P_CZ_PR_MATCHING_MNG : PageBase
	{
		P_CZ_PR_MATCHING_MNG_BIZ _biz = new P_CZ_PR_MATCHING_MNG_BIZ();

		public P_CZ_PR_MATCHING_MNG()
		{
			if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
				StartUp.Certify(this);

			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo품목위치.DataSource = MA.GetCodeUser(new string[] { "001", "002", "003", "004", "005" }, new string[] { "1번품목", "2번품목", "3번품목", "4번품목", "5번품목" });
			this.cbo품목위치.ValueMember = "CODE";
			this.cbo품목위치.DisplayMember = "NAME";

			this.cbo등급내경.DataSource = MA.GetCodeUser(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" },
														 new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" }, true);
			this.cbo등급내경.ValueMember = "CODE";
			this.cbo등급내경.DisplayMember = "NAME";

			this.cbo등급외경.DataSource = MA.GetCodeUser(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" },
														 new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" }, true);
			this.cbo등급외경.ValueMember = "CODE";
			this.cbo등급외경.DisplayMember = "NAME";

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn현합품등록);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn현합등급);
		}

		private void InitGrid()
		{
			this._flex작업지시.DetailGrids = new FlexGrid[] { this._flex수주번호 };

			#region 현합

			#region 작업지시
			this._flex작업지시.BeginSetting(2, 1, false);

			this._flex작업지시.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex작업지시.SetCol("NO_WO", "작업지시번호", 100);
			this._flex작업지시.SetCol("NM_OP", "공정명", 100);
			this._flex작업지시.SetCol("CD_ITEM", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM", "품목명", 100);
			this._flex작업지시.SetCol("QT_ITEM", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_WIP", "대기수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_MATCHING", "현합수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("CD_ITEM_001", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM_001", "품목명", 100);
			this._flex작업지시.SetCol("NO_DESIGN_001", "도면번호", 100);
			this._flex작업지시.SetCol("CD_ITEM_002", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM_002", "품목명", 100);
			this._flex작업지시.SetCol("NO_DESIGN_002", "도면번호", 100);
			this._flex작업지시.SetCol("CD_ITEM_003", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM_003", "품목명", 100);
			this._flex작업지시.SetCol("NO_DESIGN_003", "도면번호", 100);
			this._flex작업지시.SetCol("CD_ITEM_004", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM_004", "품목명", 100);
			this._flex작업지시.SetCol("NO_DESIGN_004", "도면번호", 100);
			this._flex작업지시.SetCol("CD_ITEM_005", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM_005", "품목명", 100);
			this._flex작업지시.SetCol("NO_DESIGN_005", "도면번호", 100);

			this._flex작업지시[0, this._flex작업지시.Cols["CD_ITEM_001"].Index] = this.DD("1번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NM_ITEM_001"].Index] = this.DD("1번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NO_DESIGN_001"].Index] = this.DD("1번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["CD_ITEM_002"].Index] = this.DD("2번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NM_ITEM_002"].Index] = this.DD("2번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NO_DESIGN_002"].Index] = this.DD("2번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["CD_ITEM_003"].Index] = this.DD("3번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NM_ITEM_003"].Index] = this.DD("3번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NO_DESIGN_003"].Index] = this.DD("3번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["CD_ITEM_004"].Index] = this.DD("4번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NM_ITEM_004"].Index] = this.DD("4번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NO_DESIGN_004"].Index] = this.DD("4번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["CD_ITEM_005"].Index] = this.DD("5번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NM_ITEM_005"].Index] = this.DD("5번품목");
			this._flex작업지시[0, this._flex작업지시.Cols["NO_DESIGN_005"].Index] = this.DD("5번품목");

			this._flex작업지시.AddDummyColumn("S");

			this._flex작업지시.SettingVersion = "0.0.0.1";
			this._flex작업지시.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 수주번호
			this._flex수주번호.BeginSetting(1, 1, false);

			this._flex수주번호.SetCol("NO_SO", "수주번호", 100);
			this._flex수주번호.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호.SetCol("QT_INSP", "현합수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex수주번호.SettingVersion = "0.0.0.1";
			this._flex수주번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 현합등록
			this._flex현합.BeginSetting(2, 1, false);

			this._flex현합.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex현합.SetCol("NO_WO", "작업지시번호", 100);
			this._flex현합.SetCol("SEQ_WO", "순번", 100);
			this._flex현합.SetCol("NO_ID", "가공ID", 100);
			this._flex현합.SetCol("001", "ID", 100, true);
			this._flex현합.SetCol("001_IN", "등급(내경)", 100);
			this._flex현합.SetCol("001_OUT", "등급(외경)", 100);
			this._flex현합.SetCol("002", "ID", 100, true);
			this._flex현합.SetCol("002_IN", "등급(내경)", 100);
			this._flex현합.SetCol("002_OUT", "등급(외경)", 100);
			this._flex현합.SetCol("003", "ID", 100, true);
			this._flex현합.SetCol("003_IN", "등급(내경)", 100);
			this._flex현합.SetCol("003_OUT", "등급(외경)", 100);
			this._flex현합.SetCol("004", "ID", 100, true);
			this._flex현합.SetCol("004_IN", "등급(내경)", 100);
			this._flex현합.SetCol("004_OUT", "등급(외경)", 100);
			this._flex현합.SetCol("005", "ID", 100, true);
			this._flex현합.SetCol("005_IN", "등급(내경)", 100);
			this._flex현합.SetCol("005_OUT", "등급(외경)", 100);
			this._flex현합.SetCol("NO_SO", "수주번호", 100, true);
			this._flex현합.SetCol("DC_RMK", "비고", 100, true);

			this._flex현합[0, this._flex현합.Cols["001"].Index] = this.DD("1번품목");
			this._flex현합[0, this._flex현합.Cols["001_IN"].Index] = this.DD("1번품목");
			this._flex현합[0, this._flex현합.Cols["001_OUT"].Index] = this.DD("1번품목");
			this._flex현합[0, this._flex현합.Cols["002"].Index] = this.DD("2번품목");
			this._flex현합[0, this._flex현합.Cols["002_IN"].Index] = this.DD("2번품목");
			this._flex현합[0, this._flex현합.Cols["002_OUT"].Index] = this.DD("2번품목");
			this._flex현합[0, this._flex현합.Cols["003"].Index] = this.DD("3번품목");
			this._flex현합[0, this._flex현합.Cols["003_IN"].Index] = this.DD("3번품목");
			this._flex현합[0, this._flex현합.Cols["003_OUT"].Index] = this.DD("3번품목");
			this._flex현합[0, this._flex현합.Cols["004"].Index] = this.DD("4번품목");
			this._flex현합[0, this._flex현합.Cols["004_IN"].Index] = this.DD("4번품목");
			this._flex현합[0, this._flex현합.Cols["004_OUT"].Index] = this.DD("4번품목");
			this._flex현합[0, this._flex현합.Cols["005"].Index] = this.DD("5번품목");
			this._flex현합[0, this._flex현합.Cols["005_IN"].Index] = this.DD("5번품목");
			this._flex현합[0, this._flex현합.Cols["005_OUT"].Index] = this.DD("5번품목");

			this._flex현합.AddDummyColumn("S", "001", "002", "003", "004", "005");
			this._flex현합.KeyActionEnter = KeyActionEnum.MoveDown;

			this._flex현합.SettingVersion = "0.0.0.1";
			this._flex현합.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flex현합.Styles.Add("A").BackColor = Color.Purple;
			this._flex현합.Styles.Add("A").ForeColor = Color.Black;
			this._flex현합.Styles.Add("B").BackColor = Color.LightGray;
			this._flex현합.Styles.Add("B").ForeColor = Color.Black;
			this._flex현합.Styles.Add("C").BackColor = Color.FromArgb(255, 230, 153);
			this._flex현합.Styles.Add("C").ForeColor = Color.Black;
			this._flex현합.Styles.Add("D").BackColor = Color.Khaki;
			this._flex현합.Styles.Add("D").ForeColor = Color.Black;
			this._flex현합.Styles.Add("E").BackColor = Color.Yellow;
			this._flex현합.Styles.Add("E").ForeColor = Color.Black;
			this._flex현합.Styles.Add("F").BackColor = Color.Red;
			this._flex현합.Styles.Add("F").ForeColor = Color.Black;
			this._flex현합.Styles.Add("G").BackColor = Color.Green;
			this._flex현합.Styles.Add("G").ForeColor = Color.Black;
			this._flex현합.Styles.Add("H").BackColor = Color.DeepSkyBlue;
			this._flex현합.Styles.Add("H").ForeColor = Color.Black;
			this._flex현합.Styles.Add("I").BackColor = Color.DarkGray;
			this._flex현합.Styles.Add("I").ForeColor = Color.Black;
			this._flex현합.Styles.Add("J").BackColor = Color.LightCyan;
			this._flex현합.Styles.Add("J").ForeColor = Color.Black;
			this._flex현합.Styles.Add("K").BackColor = Color.DarkCyan;
			this._flex현합.Styles.Add("K").ForeColor = Color.Black;
			this._flex현합.Styles.Add("L").BackColor = Color.SkyBlue;
			this._flex현합.Styles.Add("L").ForeColor = Color.Black;
			this._flex현합.Styles.Add("M").BackColor = Color.Pink;
			this._flex현합.Styles.Add("M").ForeColor = Color.Black;
			this._flex현합.Styles.Add("없음").BackColor = Color.White;
			this._flex현합.Styles.Add("없음").ForeColor = Color.Black;
			#endregion

			#region 대상품목
			this._flex대상품목.BeginSetting(1, 1, false);

			this._flex대상품목.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex대상품목.SetCol("NO_ID", "가공ID", 100);
			this._flex대상품목.SetCol("NO_DATA_IN", "측정치(내경)", 100);
			this._flex대상품목.SetCol("NO_DATA_OUT", "측정치(외경)", 100);
			this._flex대상품목.SetCol("CD_GRADE_IN", "등급(내경)", 100);
			this._flex대상품목.SetCol("CD_GRADE_OUT", "등급(외경)", 100);
			this._flex대상품목.SetCol("QT_SPEC_IN", "내경", 100);
			this._flex대상품목.SetCol("QT_SPEC_OUT", "외경", 100);
			this._flex대상품목.SetCol("NM_CLEAR_GRP_IN", "클리어런스그룹(내경)", 100);
			this._flex대상품목.SetCol("NM_CLEAR_GRP_OUT", "클리어런스그룹(외경)", 100);
			this._flex대상품목.SetCol("NO_HEAT", "소재HEAT번호", 100);
			this._flex대상품목.SetCol("NO_LOT", "열처리LOT번호", 100);

			this._flex대상품목.AddDummyColumn("S");

			this._flex대상품목.SettingVersion = "0.0.0.1";
			this._flex대상품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flex대상품목.Styles.Add("A").BackColor = Color.Purple;
			this._flex대상품목.Styles.Add("A").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("B").BackColor = Color.LightGray;
			this._flex대상품목.Styles.Add("B").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("C").BackColor = Color.FromArgb(255, 230, 153);
			this._flex대상품목.Styles.Add("C").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("D").BackColor = Color.Khaki;
			this._flex대상품목.Styles.Add("D").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("E").BackColor = Color.Yellow;
			this._flex대상품목.Styles.Add("E").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("F").BackColor = Color.Red;
			this._flex대상품목.Styles.Add("F").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("G").BackColor = Color.Green;
			this._flex대상품목.Styles.Add("G").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("H").BackColor = Color.DeepSkyBlue;
			this._flex대상품목.Styles.Add("H").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("I").BackColor = Color.DarkGray;
			this._flex대상품목.Styles.Add("I").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("J").BackColor = Color.LightCyan;
			this._flex대상품목.Styles.Add("J").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("K").BackColor = Color.DarkCyan;
			this._flex대상품목.Styles.Add("K").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("L").BackColor = Color.SkyBlue;
			this._flex대상품목.Styles.Add("L").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("M").BackColor = Color.Pink;
			this._flex대상품목.Styles.Add("M").ForeColor = Color.Black;
			this._flex대상품목.Styles.Add("없음").BackColor = Color.White;
			this._flex대상품목.Styles.Add("없음").ForeColor = Color.Black;
			#endregion

			#endregion

			#region 현황
		    this._flex현황.BeginSetting(2, 1, false);

			this._flex현황.SetCol("NO_WO", "작업지시번호", false);
			this._flex현황.SetCol("NO_ID", "ID번호", 100);
			this._flex현황.SetCol("CD_GRADE_IN1", "등급(내경)", 100);
			this._flex현황.SetCol("NO_HEAT1", "소재HEAT번호", 100);
			this._flex현황.SetCol("NO_ID_C1", "ID번호", 100);
			this._flex현황.SetCol("NO_LOT1", "열처리LOT번호", 100);
			this._flex현황.SetCol("QT_SPEC_IN1", "내경", 100);
			this._flex현황.SetCol("QT_CLEARANCE1", "클리어런스1", 100);
			this._flex현황.SetCol("CD_GRADE_OUT2", "등급(외경)", 100);
			this._flex현황.SetCol("NO_HEAT2", "소재HEAT번호", 100);
			this._flex현황.SetCol("NO_ID_C2", "ID번호", 100);
			this._flex현황.SetCol("NO_LOT2", "열처리LOT번호", 100);
			this._flex현황.SetCol("QT_SPEC_OUT2", "외경", 100);
			this._flex현황.SetCol("QT_SPEC_IN2", "내경", 100);
			this._flex현황.SetCol("CD_GRADE_IN2", "등급(내경)", 100);
			this._flex현황.SetCol("QT_CLEARANCE2", "클리어런스2", 100);
			this._flex현황.SetCol("CD_GRADE_OUT3", "등급(외경)", 100);
			this._flex현황.SetCol("NO_HEAT3", "소재HEAT번호", 100);
			this._flex현황.SetCol("NO_ID_C3", "ID번호", 100);
			this._flex현황.SetCol("NO_LOT3", "열처리LOT번호", 100);
			this._flex현황.SetCol("QT_SPEC_OUT3", "외경", 100);
			this._flex현황.SetCol("QT_SPEC_IN3", "내경", 100);
			this._flex현황.SetCol("CD_GRADE_IN3", "등급(내경)", 100);
			this._flex현황.SetCol("QT_CLEARANCE3", "클리어런스3", 100);
			this._flex현황.SetCol("CD_GRADE_OUT4", "등급(외경)", 100);
			this._flex현황.SetCol("NO_HEAT4", "소재HEAT번호", 100);
			this._flex현황.SetCol("NO_ID_C4", "ID번호", 100);
			this._flex현황.SetCol("NO_LOT4", "열처리LOT번호", 100);
			this._flex현황.SetCol("QT_SPEC_OUT4", "외경", 100);

			this._flex현황.SetCol("QT_SPEC_IN4", "내경", 100);
			this._flex현황.SetCol("CD_GRADE_IN4", "등급(내경)", 100);
			this._flex현황.SetCol("QT_CLEARANCE4", "클리어런스4", 100);

			this._flex현황.SetCol("CD_GRADE_OUT5", "등급(외경)", 100);
			this._flex현황.SetCol("NO_HEAT5", "소재HEAT번호", 100);
			this._flex현황.SetCol("NO_ID_C5", "ID번호", 100);
			this._flex현황.SetCol("NO_LOT5", "열처리LOT번호", 100);
			this._flex현황.SetCol("QT_SPEC_OUT5", "외경", 100);

			this._flex현황.SetCol("NO_SO", "수주번호", 100);
			this._flex현황.SetCol("DC_RMK", "비고", 100);

			this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN1"].Index] = this.DD("1번품목");
			this._flex현황[0, this._flex현황.Cols["NO_HEAT1"].Index] = this.DD("1번품목");
			this._flex현황[0, this._flex현황.Cols["NO_ID_C1"].Index] = this.DD("1번품목");
			this._flex현황[0, this._flex현황.Cols["NO_LOT1"].Index] = this.DD("1번품목");
			this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE1"].Index] = this.DD("클리어런스1");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN1"].Index] = this.DD("1번품목");
			this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["NO_HEAT2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["NO_ID_C2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["NO_LOT2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN2"].Index] = this.DD("2번품목");
			this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE2"].Index] = this.DD("클리어런스2");
			this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["NO_HEAT3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["NO_ID_C3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["NO_LOT3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN3"].Index] = this.DD("3번품목");
			this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE3"].Index] = this.DD("클리어런스3");
			this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT4"].Index] = this.DD("4번품목");
			this._flex현황[0, this._flex현황.Cols["NO_HEAT4"].Index] = this.DD("4번품목");
			this._flex현황[0, this._flex현황.Cols["NO_ID_C4"].Index] = this.DD("4번품목");
			this._flex현황[0, this._flex현황.Cols["NO_LOT4"].Index] = this.DD("4번품목");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT4"].Index] = this.DD("4번품목");

			this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN4"].Index] = this.DD("4번품목");
			this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN4"].Index] = this.DD("4번품목");
			this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE4"].Index] = this.DD("클리어런스4");

			this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT5"].Index] = this.DD("5번품목");
			this._flex현황[0, this._flex현황.Cols["NO_HEAT5"].Index] = this.DD("5번품목");
			this._flex현황[0, this._flex현황.Cols["NO_ID_C5"].Index] = this.DD("5번품목");
			this._flex현황[0, this._flex현황.Cols["NO_LOT5"].Index] = this.DD("5번품목");
			this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT5"].Index] = this.DD("5번품목");

			this._flex현황.SettingVersion = "0.0.0.1";
			this._flex현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flex현황.Styles.Add("A").BackColor = Color.Purple;
			this._flex현황.Styles.Add("A").ForeColor = Color.Black;
			this._flex현황.Styles.Add("B").BackColor = Color.LightGray;
			this._flex현황.Styles.Add("B").ForeColor = Color.Black;
			this._flex현황.Styles.Add("C").BackColor = Color.FromArgb(255, 230, 153);
			this._flex현황.Styles.Add("C").ForeColor = Color.Black;
			this._flex현황.Styles.Add("D").BackColor = Color.Khaki;
			this._flex현황.Styles.Add("D").ForeColor = Color.Black;
			this._flex현황.Styles.Add("E").BackColor = Color.Yellow;
			this._flex현황.Styles.Add("E").ForeColor = Color.Black;
			this._flex현황.Styles.Add("F").BackColor = Color.Red;
			this._flex현황.Styles.Add("F").ForeColor = Color.Black;
			this._flex현황.Styles.Add("G").BackColor = Color.Green;
			this._flex현황.Styles.Add("G").ForeColor = Color.Black;
			this._flex현황.Styles.Add("H").BackColor = Color.DeepSkyBlue;
			this._flex현황.Styles.Add("H").ForeColor = Color.Black;
			this._flex현황.Styles.Add("I").BackColor = Color.DarkGray;
			this._flex현황.Styles.Add("I").ForeColor = Color.Black;
			this._flex현황.Styles.Add("J").BackColor = Color.LightCyan;
			this._flex현황.Styles.Add("J").ForeColor = Color.Black;
			this._flex현황.Styles.Add("K").BackColor = Color.DarkCyan;
			this._flex현황.Styles.Add("K").ForeColor = Color.Black;
			this._flex현황.Styles.Add("L").BackColor = Color.SkyBlue;
			this._flex현황.Styles.Add("L").ForeColor = Color.Black;
			this._flex현황.Styles.Add("M").BackColor = Color.Pink;
			this._flex현황.Styles.Add("M").ForeColor = Color.Black;
			this._flex현황.Styles.Add("없음").BackColor = Color.White;
			this._flex현황.Styles.Add("없음").ForeColor = Color.Black;
			#endregion
		}

		private void InitEvent()
		{
			this.btn현합등급.Click += Btn현합등급_Click;
			this.btn현합품등록.Click += Btn현합품등록_Click;
			this.btn대기품목관리.Click += Btn대기품목관리_Click;
			this.btn대상품목등록.Click += Btn대상품목등록_Click;

			this.btn지시적용.Click += Btn지시적용_Click;
			this.btn조회.Click += Btn조회_Click;
			this.btn교체.Click += Btn교체_Click;
			this.btn적용.Click += Btn적용_Click;
			this.btn대기.Click += Btn대기_Click;
			this.btn수주적용.Click += Btn수주적용_Click;
			this.btn실적등록.Click += Btn실적등록_Click;

			this.btn번호갱신.Click += Btn번호갱신_Click;

			this.ctx현합품목.QueryBefore += Ctx품목_QueryBefore;
			this.ctx모품목.QueryBefore += Ctx품목_QueryBefore;

			this._flex현합.ValidateEdit += _flex현합_ValidateEdit;
			this._flex대상품목.OwnerDrawCell += _flex_OwnerDrawCell;
			this._flex현합.OwnerDrawCell += _flex_OwnerDrawCell;
			this._flex작업지시.AfterRowChange += _flex작업지시_AfterRowChange;
		}

		private void Btn번호갱신_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"UPDATE MD
SET MD.NO_HEAT = ID.NO_HEAT 
FROM CZ_PR_MATCHING_DATA MD
LEFT JOIN (SELECT WD.CD_COMPANY, WD.NO_ID,
                  ISNULL(NULLIF(WD.NO_HEAT, ''), WO.TXT_USERDEF1) AS NO_HEAT
           FROM PR_WO WO
           JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO 
           UNION ALL
           SELECT CD_COMPANY, NO_ID, NO_HEAT 
           FROM CZ_PR_MATCHING_ID_OLD) ID
ON ID.CD_COMPANY = MD.CD_COMPANY AND ID.NO_ID = MD.NO_ID_C
WHERE MD.CD_COMPANY = '{0}' 
AND ISNULL(MD.NO_HEAT, '') <> ISNULL(ID.NO_HEAT, '')";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

				query = @"UPDATE MD
SET MD.NO_LOT = ID.NO_LOT 
FROM CZ_PR_MATCHING_DATA MD
LEFT JOIN (SELECT WD.CD_COMPANY, WD.NO_ID,
                  (SELECT MAX(WI2.NO_HEAT) AS NO_HEAT 
                   FROM CZ_PR_WO_INSP WI2 
                   WHERE WI2.CD_COMPANY = WO.CD_COMPANY 
                   AND WI2.NO_WO = WO.NO_WO  
                   AND WI2.NO_INSP IN (0, 999, 995)
                   AND WI2.SEQ_WO = WD.SEQ_WO) AS NO_LOT
           FROM PR_WO WO
           JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO 
           UNION ALL
           SELECT CD_COMPANY, NO_ID, NO_LOT 
           FROM CZ_PR_MATCHING_ID_OLD) ID
ON ID.CD_COMPANY = MD.CD_COMPANY AND ID.NO_ID = MD.NO_ID_C
WHERE MD.CD_COMPANY = '{0}' 
AND ISNULL(MD.NO_LOT, '') <> ISNULL(ID.NO_LOT, '')";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn번호갱신.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대상품목등록_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_MATCHING_ID_OLD_SUB dialog = new P_CZ_PR_MATCHING_ID_OLD_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대기품목관리_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_MATCHING_DEACTIVATE_SUB dialog = new P_CZ_PR_MATCHING_DEACTIVATE_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대기_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow drTemp;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex대상품목.HasNormalRow) return;

				dataRowArray = this._flex대상품목.DataTable.Select("S = 'Y'");

				dt = new DataTable();

				dt.Columns.Add("CD_COMPANY");
				dt.Columns.Add("CD_PLANT");
				dt.Columns.Add("NO_ID");
				dt.Columns.Add("STA_DEACTIVATE");
				dt.Columns.Add("DC_RMK");

				if (dataRowArray == null && dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_ID"] = dr["NO_ID"].ToString();
						drTemp["STA_DEACTIVATE"] = "001"; // 정상
						drTemp["DC_RMK"] = string.Empty;

						dt.Rows.Add(drTemp);

						dr.Delete();
					}
				}

				if (dt.Rows.Count > 0)
				{
					DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_DEACTIVATE_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn실적등록_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.cur실적수량.DecimalValue > (D.GetDecimal(this._flex작업지시["QT_MATCHING"]) - D.GetDecimal(this._flex작업지시["QT_WORK"])))
				{
					this.ShowMessage(공통메세지._은_보다작거나같아야합니다, "실적수량", "현합수량-작업수량");
					return;
				}

				bool isSuccess = DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_WO_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									   this._flex작업지시["NO_WO"].ToString(),
																									   this._flex작업지시["NO_LINE"].ToString(),
																									   Global.MainFrame.LoginInfo.UserID,
																									   this.cur실적수량.DecimalValue });

				if (isSuccess == true)
				{
					this.cur실적수량.DecimalValue = 0;
					this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex작업지시_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string key, filter;

			try
			{
				if (this._flex작업지시.HasNormalRow == false) return;

				key = this._flex작업지시["NO_WO"].ToString();
				filter = "NO_WO = '" + key + "'";

				if (this._flex작업지시.DetailQueryNeed == true)
					dt = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, key });

				this._flex수주번호.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn수주적용_Click(object sender, EventArgs e)
		{
			DataTable dt, dtWO;
			DataRow drTemp, drWO;
			DataRow[] dataRowArray;
			string query, 수주번호;
			int 작업지시수량;

			try
			{
				수주번호 = this._flex수주번호["NO_SO"].ToString();

				if (string.IsNullOrEmpty(수주번호))
				{
					this.ShowMessage("수주번호가 지정되어 있지 않습니다.");
					return;
				}

				dataRowArray = this._flex현합.DataTable.Select("S = 'Y' AND ISNULL(NO_SO, '') = ''", "NO_WO, NO_LINE, SEQ_WO");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage("수주 미적용 항목이 없습니다.");
					return;
				}
				else
				{
					dtWO = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_WO" }, true);
					dtWO.Columns.Add("QT_APPLY", typeof(int));
					dtWO.Columns.Add("QT_INSP", typeof(int));

					foreach (DataRow dr in dtWO.Rows)
					{
						query = @"SELECT SW.NO_WO,
										 SW.QT_APPLY,
										 ISNULL(WI.QT_INSP, 0) AS QT_INSP
								  FROM CZ_PR_SA_SOL_PR_WO_MAPPING SW WITH(NOLOCK)
								  LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE, WI.NO_SO,
								                    COUNT(1) AS QT_INSP
								             FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
								             WHERE WI.NO_INSP = 997
								             GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE, WI.NO_SO) WI
								  ON WI.CD_COMPANY = SW.CD_COMPANY AND WI.NO_WO = SW.NO_WO AND WI.NO_SO = SW.NO_SO
								  WHERE SW.CD_COMPANY = '{0}'
								  AND SW.NO_WO = '{1}'
								  AND SW.NO_SO = '{2}'";

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   dr["NO_WO"].ToString(),
																					   수주번호 }));

						if (dt == null || dt.Rows.Count == 0)
						{
							this.ShowMessage("적용할 수주번호가 할당되지 않은 작업지시가 선택되어 있습니다.");
							return;
						}
						else
						{
							dr["QT_APPLY"] = dt.Rows[0]["QT_APPLY"];
							dr["QT_INSP"] = dt.Rows[0]["QT_INSP"];
						}
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("DC_RMK");

					foreach (DataRow dr in dataRowArray)
					{
						drWO = dtWO.Select(string.Format("NO_WO = '{0}'", dr["NO_WO"].ToString()))[0];

						작업지시수량 = D.GetInt(drWO["QT_APPLY"]);
						drWO["QT_INSP"] = D.GetInt(drWO["QT_INSP"]) + 1;

						if (작업지시수량 < D.GetInt(drWO["QT_INSP"]))
							break;

						dr["NO_SO"] = 수주번호;

						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["NO_WO"] = dr["NO_WO"].ToString();
						drTemp["NO_LINE"] = dr["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = dr["SEQ_WO"].ToString();
						drTemp["NO_SO"] = 수주번호;
						drTemp["DC_RMK"] = string.Empty;
						
						dt.Rows.Add(drTemp);
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_JSON1", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			FlexGrid grid;

			try
			{
				grid = ((FlexGrid)sender);

				if (!grid.HasNormalRow) return;
				if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed) return;
				if (grid[e.Row, e.Col] == null) return;

				CellStyle cellStyle = grid.GetCellStyle(e.Row, e.Col);

				if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "A"))
				{
					if (cellStyle == null || cellStyle.Name != "A")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["A"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "B"))
				{
					if (cellStyle == null || cellStyle.Name != "B")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["B"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "C"))
				{
					if (cellStyle == null || cellStyle.Name != "C")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["C"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "D"))
				{
					if (cellStyle == null || cellStyle.Name != "D")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["D"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "E"))
				{
					if (cellStyle == null || cellStyle.Name != "E")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["E"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "F"))
				{
					if (cellStyle == null || cellStyle.Name != "F")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["F"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "G"))
				{
					if (cellStyle == null || cellStyle.Name != "G")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["G"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "H"))
				{
					if (cellStyle == null || cellStyle.Name != "H")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["H"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "I"))
				{
					if (cellStyle == null || cellStyle.Name != "I")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["I"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "J"))
				{
					if (cellStyle == null || cellStyle.Name != "J")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["J"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "K"))
				{
					if (cellStyle == null || cellStyle.Name != "K")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["K"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "L"))
				{
					if (cellStyle == null || cellStyle.Name != "L")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["L"]);
				}
				else if ((grid[e.Row, e.Col] != null && grid[e.Row, e.Col].ToString() == "M"))
				{
					if (cellStyle == null || cellStyle.Name != "M")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["M"]);
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "없음")
						grid.SetCellStyle(e.Row, e.Col, grid.Styles["없음"]);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex현합_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			DataTable dt, dt1;
			DataRow drTemp;
			string oldValue, newValue, columnName, 품목코드, query;

			try
			{
				columnName = this._flex현합.Cols[e.Col].Name;

				if (columnName != "001" && 
					columnName != "002" && 
					columnName != "003" && 
					columnName != "004" &&
					columnName != "005" &&
					columnName != "NO_SO" &&
					columnName != "DC_RMK") return;

				oldValue = this._flex현합[e.Row, e.Col].ToString();
				newValue = this._flex현합.EditData;
				
				if (oldValue == newValue) return;

				if (columnName == "NO_SO")
				{
					#region 수주번호
					if (!string.IsNullOrEmpty(newValue))
					{
						query = @"SELECT SW.QT_APPLY,
								  	     ISNULL(WI.QT_INSP, 0) AS QT_INSP 
								  FROM CZ_PR_SA_SOL_PR_WO_MAPPING SW WITH(NOLOCK)
								  LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE, WI.NO_SO,
								                    COUNT(1) AS QT_INSP
								             FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
								             WHERE WI.NO_INSP = 997
								             GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE, WI.NO_SO) WI
								  ON WI.CD_COMPANY = SW.CD_COMPANY AND WI.NO_WO = SW.NO_WO AND WI.NO_SO = SW.NO_SO
								  WHERE SW.CD_COMPANY = '{0}'
								  AND SW.NO_WO = '{1}'
								  AND SW.NO_SO = '{2}'";

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this._flex현합["NO_WO"].ToString(),
																					   newValue }));

						if (dt == null || dt.Rows.Count == 0)
						{
							this.ShowMessage("할당되어 있지 않은 수주번호 입니다.");
							e.Cancel = true;
							return;
						}
						else if (D.GetInt(dt.Rows[0]["QT_APPLY"]) < D.GetInt(dt.Rows[0]["QT_INSP"]) + 1)
						{
							this.ShowMessage("작업지시에 할당 된 수주 수량보다 현합수량이 많습니다.");
							e.Cancel = true;
							return;
						}
					}
					else
					{
						query = @"SELECT ISNULL(WR.QT_WORK, 0) AS QT_WORK,
								         ISNULL(WI.QT_MATCHING, 0) AS QT_MATCHING
								  FROM PR_WO WO WITH(NOLOCK)
								  JOIN PR_WO_ROUT WR WITH(NOLOCK) ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
								  LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE,
								                    COUNT(1) AS QT_MATCHING
								             FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
								             WHERE WI.NO_INSP = 997
								             GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE) WI
								  ON WI.CD_COMPANY = WO.CD_COMPANY AND WI.NO_WO = WO.NO_WO AND WI.NO_LINE = WR.NO_LINE
								  WHERE WO.CD_COMPANY = '{0}'
								  AND WO.NO_WO = '{1}'
								  AND WR.NO_LINE = '{2}'";

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this._flex현합["NO_WO"].ToString(),
																					   this._flex현합["NO_LINE"].ToString() }));

						int 작업수량 = D.GetInt(dt.Rows[0]["QT_WORK"]);
						int 현합수량 = (D.GetInt(dt.Rows[0]["QT_MATCHING"]) - 1);

						if (작업수량 > 현합수량)
						{
							this.ShowMessage("현합수량은 작업수량보다 크거나 같아야 합니다.");
							e.Cancel = true;
							return;
						}
					}

					query = @"SELECT *
FROM CZ_PR_ASSEMBLING_DATA WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_PLANT = '{1}'
AND NO_ID_C = '{2}'";

					dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	Global.MainFrame.LoginInfo.CdPlant,
																	this._flex현합["001"].ToString()));

					if (dt != null && dt.Rows.Count > 0)
					{
						this.ShowMessage("조립품 등록되어 있는 수주번호는 수정 할 수 없습니다. (수주번호 :" + oldValue + ")");
						e.Cancel = true;
						return;
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("DC_RMK");

					if (string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
						drTemp["NO_SO"] = newValue;
						drTemp["DC_RMK"] = string.Empty;

						dt.Rows.Add(drTemp);
					}
					else if (!string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(newValue))
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
						drTemp["NO_SO"] = oldValue;
						drTemp["DC_RMK"] = string.Empty;

						dt.Rows.Add(drTemp);
						drTemp.AcceptChanges();
						drTemp.Delete();
					}
					else if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
						drTemp["NO_SO"] = newValue;
						drTemp["DC_RMK"] = string.Empty;

						dt.Rows.Add(drTemp);
						drTemp.AcceptChanges();
						drTemp.SetModified();
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_JSON1", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
				else if (columnName == "DC_RMK")
				{
					#region 비고
					if (string.IsNullOrEmpty(this._flex현합["NO_SO"].ToString()))
					{
						this.ShowMessage("수주번호가 할당되어 있지 않은 건 입니다.");
						e.Cancel = true;
						return;
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("DC_RMK");

					drTemp = dt.NewRow();

					drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
					drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
					drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
					drTemp["NO_SO"] = this._flex현합["NO_SO"].ToString();
					drTemp["DC_RMK"] = newValue;

					dt.Rows.Add(drTemp);
					drTemp.AcceptChanges();
					drTemp.SetModified();

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_JSON1", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
				else
				{
					#region ID
					if (!string.IsNullOrEmpty(this._flex현합["NO_SO"].ToString()))
					{
						this.ShowMessage("수주번호가 할당 되어 있는 항목은 수정할 수 없습니다.");
						e.Cancel = true;
						return;
					}

					query = @"SELECT NO_ID_C 
FROM CZ_PR_ASSEMBLING_DATA WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_PLANT = '{1}'
AND NO_ID_C = '{2}'";

					dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	Global.MainFrame.LoginInfo.CdPlant,
																	oldValue));

					if (dt != null && dt.Rows.Count > 0)
					{
						this.ShowMessage("조립품 등록되어 있는 ID번호는 수정 할 수 없습니다. (ID번호 :" + oldValue + ")");
						e.Cancel = true;
						return;
					}

					switch (columnName)
					{
						case "001":
							if (string.IsNullOrEmpty(this.ctx1번품목.CodeValue))
							{
								this.ShowMessage("1번품목이 설정되어 있지 않습니다.");
								e.Cancel = true;
								return;
							}

							품목코드 = this.ctx1번품목.CodeValue;
							break;
						case "002":
							if (string.IsNullOrEmpty(this.ctx2번품목.CodeValue))
							{
								this.ShowMessage("2번품목이 설정되어 있지 않습니다.");
								e.Cancel = true;
								return;
							}

							품목코드 = this.ctx2번품목.CodeValue;
							break;
						case "003":
							if (string.IsNullOrEmpty(this.ctx3번품목.CodeValue))
							{
								this.ShowMessage("3번품목이 설정되어 있지 않습니다.");
								e.Cancel = true;
								return;
							}

							품목코드 = this.ctx3번품목.CodeValue;
							break;
						case "004":
							if (string.IsNullOrEmpty(this.ctx4번품목.CodeValue))
							{
								this.ShowMessage("4번품목이 설정되어 있지 않습니다.");
								e.Cancel = true;
								return;
							}

							품목코드 = this.ctx4번품목.CodeValue;
							break;
						case "005":
							if (string.IsNullOrEmpty(this.ctx5번품목.CodeValue))
							{
								this.ShowMessage("5번품목이 설정되어 있지 않습니다.");
								e.Cancel = true;
								return;
							}

							품목코드 = this.ctx5번품목.CodeValue;
							break;
						default:
							return;
					}

					dt = null;

					if (!string.IsNullOrEmpty(newValue))
					{
						dt = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														       Global.MainFrame.LoginInfo.CdPlant,
														       품목코드,
														       newValue,
														       newValue,
														       string.Empty,
														       string.Empty });

						if (dt == null || dt.Rows.Count == 0)
						{
							this.ShowMessage("ID에 해당하는 항목이 없습니다.");

							this._flex현합[e.Row, columnName + "_IN"] = string.Empty;
							this._flex현합[e.Row, columnName + "_OUT"] = string.Empty;

							e.Cancel = true;
							return;
						}
						else
						{
							this._flex현합[e.Row, columnName + "_IN"] = dt.Rows[0]["CD_GRADE_IN"].ToString();
							this._flex현합[e.Row, columnName + "_OUT"] = dt.Rows[0]["CD_GRADE_OUT"].ToString();

							dt1 = dt.Clone();
						}
					}
					else
					{
						dt1 = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															    Global.MainFrame.LoginInfo.CdPlant,
															    품목코드,
															    "99999",
															    "99999",
															    string.Empty,
															    string.Empty });

						this._flex현합[e.Row, columnName + "_IN"] = string.Empty;
						this._flex현합[e.Row, columnName + "_OUT"] = string.Empty;
					}

					dt1.Columns.Add("CD_COMPANY");
					dt1.Columns.Add("CD_PLANT");
					dt1.Columns.Add("NO_WO");
					dt1.Columns.Add("NO_LINE");
					dt1.Columns.Add("SEQ_WO");
					dt1.Columns.Add("NO_ID_C");
					dt1.Columns.Add("TP_POS");

					if (string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
					{
						drTemp = dt1.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
						drTemp["TP_POS"] = columnName;
						drTemp.ItemArray = dt.Rows[0].ItemArray;
						drTemp["NO_ID_C"] = drTemp["NO_ID"];
						drTemp["NO_ID"] = this._flex현합["NO_ID"].ToString();

						dt1.Rows.Add(drTemp);
					}
					else if (!string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(newValue))
					{
						drTemp = dt1.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
						drTemp["NO_ID"] = this._flex현합["NO_ID"].ToString();
						drTemp["TP_POS"] = columnName;
						drTemp["NO_ID_C"] = oldValue;
						drTemp["CD_PITEM"] = 품목코드;

						dt1.Rows.Add(drTemp);
						drTemp.AcceptChanges();
						drTemp.Delete();
					}
					else if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
					{
						drTemp = dt1.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex현합["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"].ToString();
						drTemp["TP_POS"] = columnName;
						drTemp.ItemArray = dt.Rows[0].ItemArray;
						drTemp["NO_ID_C"] = drTemp["NO_ID"];
						drTemp["NO_ID"] = this._flex현합["NO_ID"].ToString();

						dt1.Rows.Add(drTemp);
						drTemp.AcceptChanges();
						drTemp.SetModified();
					}

					if (dt1.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_JSON", new object[] { dt1.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn교체_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			DataRow drFrom, drTemp;
			string 위치코드, 품목코드, ID번호, 기존ID번호;
			try
			{
				if (!this._flex현합.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.cbo품목위치.SelectedValue.ToString()))
				{
					this.ShowMessage("품목위치를 지정해야 합니다.");
					return;
				}

				dataRowArray = this._flex대상품목.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (dataRowArray.Length != 1)
				{
					this.ShowMessage("대상품목 중 1개의 ID번호만 선택 가능합니다.");
				}
				else
				{
					위치코드 = this.cbo품목위치.SelectedValue.ToString();

					switch (위치코드)
					{
						case "001":
							if (string.IsNullOrEmpty(this.ctx1번품목.CodeValue))
							{
								this.ShowMessage("1번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx1번품목.CodeValue;
							break;
						case "002":
							if (string.IsNullOrEmpty(this.ctx2번품목.CodeValue))
							{
								this.ShowMessage("2번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx2번품목.CodeValue;
							break;
						case "003":
							if (string.IsNullOrEmpty(this.ctx3번품목.CodeValue))
							{
								this.ShowMessage("3번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx3번품목.CodeValue;
							break;
						case "004":
							if (string.IsNullOrEmpty(this.ctx4번품목.CodeValue))
							{
								this.ShowMessage("4번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx4번품목.CodeValue;
							break;
						case "005":
							if (string.IsNullOrEmpty(this.ctx5번품목.CodeValue))
							{
								this.ShowMessage("5번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx5번품목.CodeValue;
							break;
						default:
							return;
					}

					기존ID번호 = this._flex현합[위치코드].ToString();

					dt = this._flex대상품목.DataTable.Clone();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_ID_C");
					dt.Columns.Add("TP_POS");
					dt.Columns.Add(위치코드);
					dt.Columns.Add(위치코드 + "_IN");
					dt.Columns.Add(위치코드 + "_OUT");
					dt.Columns.Add("NO_ID_BEFORE");

					drFrom = dataRowArray[0];

					ID번호 = drFrom["NO_ID"].ToString();

					if (drFrom["CD_PITEM"].ToString() != 품목코드)
					{
						this.ShowMessage("선택한 대상품목과 현합품목이 다릅니다.");
						return;
					}
					else if (this._flex현합.DataTable.Select("ISNULL([" + 위치코드 + "], '') = '" + ID번호 + "'").Length > 0)
					{
						this.ShowMessage("동일한 가공ID가 등록되어 있습니다. (" + ID번호 + ")");
						return;
					}
					else
					{
						this._flex현합[위치코드] = ID번호;
						this._flex현합[위치코드 + "_IN"] = drFrom["CD_GRADE_IN"].ToString();
						this._flex현합[위치코드 + "_OUT"] = drFrom["CD_GRADE_OUT"].ToString();

						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex현합["NO_WO"];
						drTemp["NO_LINE"] = this._flex현합["NO_LINE"];
						drTemp["SEQ_WO"] = this._flex현합["SEQ_WO"];
						drTemp["TP_POS"] = 위치코드;
						drTemp[위치코드] = this._flex현합[위치코드];
						drTemp[위치코드 + "_IN"] = this._flex현합[위치코드 + "_IN"];
						drTemp[위치코드 + "_OUT"] = this._flex현합[위치코드 + "_OUT"];
						drTemp.ItemArray = drFrom.ItemArray;
						drTemp["NO_ID_C"] = drTemp["NO_ID"];
						drTemp["NO_ID"] = this._flex현합["NO_ID"];
						drTemp["NO_ID_BEFORE"] = 기존ID번호;

						dt.Rows.Add(drTemp);

						drFrom.Delete();
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_JSON2", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}

					DataTable dt2 = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 Global.MainFrame.LoginInfo.CdPlant,
																 품목코드,
																 this.txt범위조회From.Text,
																 this.txt범위조회To.Text,
																 this.cbo등급내경.SelectedValue,
																 this.cbo등급외경.SelectedValue });
					this._flex대상품목.Binding = dt2;

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn교체.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn적용_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			DataRow drFrom, drTemp;
			string 위치코드, ID번호, 품목코드;
			int index;

			try
			{
				if (!this._flex현합.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.cbo품목위치.SelectedValue.ToString()))
				{
					this.ShowMessage("품목위치를 지정해야 합니다.");
					return;
				}

				dataRowArray = this._flex대상품목.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					위치코드 = this.cbo품목위치.SelectedValue.ToString();

					switch (위치코드)
					{
						case "001":
							if (string.IsNullOrEmpty(this.ctx1번품목.CodeValue))
							{
								this.ShowMessage("1번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx1번품목.CodeValue;
							break;
						case "002":
							if (string.IsNullOrEmpty(this.ctx2번품목.CodeValue))
							{
								this.ShowMessage("2번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx2번품목.CodeValue;
							break;
						case "003":
							if (string.IsNullOrEmpty(this.ctx3번품목.CodeValue))
							{
								this.ShowMessage("3번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx3번품목.CodeValue;
							break;
						case "004":
							if (string.IsNullOrEmpty(this.ctx4번품목.CodeValue))
							{
								this.ShowMessage("4번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx4번품목.CodeValue;
							break;
						case "005":
							if (string.IsNullOrEmpty(this.ctx5번품목.CodeValue))
							{
								this.ShowMessage("5번품목이 설정되어 있지 않습니다.");
								return;
							}

							품목코드 = this.ctx5번품목.CodeValue;
							break;
						default:
							return;
					}

					index = 0;

					dt = this._flex대상품목.DataTable.Clone();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_ID_C");
					dt.Columns.Add("TP_POS");

					foreach (DataRow dr in this._flex현합.DataTable.Select("ISNULL([" + 위치코드 + "], '') = '' AND ISNULL(NO_SO, '') = ''"))
					{
						if (dataRowArray.Length - 1 < index)
							break;

						drFrom = dataRowArray[index];

						ID번호 = drFrom["NO_ID"].ToString();

						if (drFrom["CD_PITEM"].ToString() != 품목코드)
						{
							this.ShowMessage("선택한 대상품목과 현합품목이 다릅니다.");
							break;
						}
						else if (this._flex현합.DataTable.Select("ISNULL([" + 위치코드 + "], '') = '" + ID번호 + "'").Length > 0)
						{
							this.ShowMessage("동일한 가공ID가 등록되어 있습니다. (" + ID번호 + ")");
							break;
						}
						else
						{
							dr[위치코드] = ID번호;
							dr[위치코드 + "_IN"] = drFrom["CD_GRADE_IN"].ToString();
							dr[위치코드 + "_OUT"] = drFrom["CD_GRADE_OUT"].ToString();

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							drTemp["NO_WO"] = dr["NO_WO"];
							drTemp["NO_LINE"] = dr["NO_LINE"];
							drTemp["SEQ_WO"] = dr["SEQ_WO"];
							drTemp["TP_POS"] = 위치코드;
							drTemp.ItemArray = drFrom.ItemArray;
							drTemp["NO_ID_C"] = drTemp["NO_ID"];
							drTemp["NO_ID"] = dr["NO_ID"];

							dt.Rows.Add(drTemp);

							drFrom.Delete();
						}

						index++;
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_MNG_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn적용.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			string 품목코드, 위치코드;

			try
			{
				if (!this._flex현합.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.cbo품목위치.SelectedValue.ToString()))
				{
					this.ShowMessage("품목위치를 지정해야 합니다.");
					return;
				}

				위치코드 = this.cbo품목위치.SelectedValue.ToString();

				switch (위치코드)
				{
					case "001":
						if (string.IsNullOrEmpty(this.ctx1번품목.CodeValue))
						{
							this.ShowMessage("1번품목이 설정되어 있지 않습니다.");
							return;
						}

						품목코드 = this.ctx1번품목.CodeValue;
						break;
					case "002":
						if (string.IsNullOrEmpty(this.ctx2번품목.CodeValue))
						{
							this.ShowMessage("2번품목이 설정되어 있지 않습니다.");
							return;
						}

						품목코드 = this.ctx2번품목.CodeValue;
						break;
					case "003":
						if (string.IsNullOrEmpty(this.ctx3번품목.CodeValue))
						{
							this.ShowMessage("3번품목이 설정되어 있지 않습니다.");
							return;
						}

						품목코드 = this.ctx3번품목.CodeValue;
						break;
					case "004":
						if (string.IsNullOrEmpty(this.ctx4번품목.CodeValue))
						{
							this.ShowMessage("4번품목이 설정되어 있지 않습니다.");
							return;
						}

						품목코드 = this.ctx4번품목.CodeValue;
						break;
					case "005":
						if (string.IsNullOrEmpty(this.ctx5번품목.CodeValue))
						{
							this.ShowMessage("5번품목이 설정되어 있지 않습니다.");
							return;
						}

						품목코드 = this.ctx5번품목.CodeValue;
						break;
					default:
						return;
				}

				DataTable dt = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
															     Global.MainFrame.LoginInfo.CdPlant,
																 품목코드,
																 this.txt범위조회From.Text,
																 this.txt범위조회To.Text,
																 this.cbo등급내경.SelectedValue,
																 this.cbo등급외경.SelectedValue });

				this._flex대상품목.Binding = dt;

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn조회.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn현합품등록_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_MATCHING_ITEM_SUB dialog = new P_CZ_PR_MATCHING_ITEM_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn현합등급_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_MATCHING_GRADE_SUB dialog = new P_CZ_PR_MATCHING_GRADE_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn지시적용_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex작업지시.HasNormalRow) return;

				if (this._flex작업지시.DataTable.Select("S = 'Y'").Select(x => x["CD_ITEM"]).Distinct().Count() > 1)
				{
					this.ShowMessage("동일하지 않은 품목이 선택되어 있습니다.");
					return;
				}

				dataRowArray = this._flex작업지시.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this.ctx대상품목.CodeValue = dataRowArray[0]["CD_ITEM"].ToString();
					this.ctx대상품목.CodeName = dataRowArray[0]["NM_ITEM"].ToString();

					this.ctx1번품목.CodeValue = dataRowArray[0]["CD_ITEM_001"].ToString();
					this.ctx1번품목.CodeName = dataRowArray[0]["NM_ITEM_001"].ToString();

					this.ctx2번품목.CodeValue = dataRowArray[0]["CD_ITEM_002"].ToString();
					this.ctx2번품목.CodeName = dataRowArray[0]["NM_ITEM_002"].ToString();

					this.ctx3번품목.CodeValue = dataRowArray[0]["CD_ITEM_003"].ToString();
					this.ctx3번품목.CodeName = dataRowArray[0]["NM_ITEM_003"].ToString();

					this.ctx4번품목.CodeValue = dataRowArray[0]["CD_ITEM_004"].ToString();
					this.ctx4번품목.CodeName = dataRowArray[0]["NM_ITEM_004"].ToString();

					this.ctx5번품목.CodeValue = dataRowArray[0]["CD_ITEM_005"].ToString();
					this.ctx5번품목.CodeName = dataRowArray[0]["NM_ITEM_005"].ToString();

					this._flex현합[0, this._flex현합.Cols["001"].Index] = dataRowArray[0]["NO_DESIGN_001"].ToString();
					this._flex현합[0, this._flex현합.Cols["001_IN"].Index] = dataRowArray[0]["NO_DESIGN_001"].ToString();
					this._flex현합[0, this._flex현합.Cols["001_OUT"].Index] = dataRowArray[0]["NO_DESIGN_001"].ToString();
					this._flex현합[0, this._flex현합.Cols["002"].Index] = dataRowArray[0]["NO_DESIGN_002"].ToString();
					this._flex현합[0, this._flex현합.Cols["002_IN"].Index] = dataRowArray[0]["NO_DESIGN_002"].ToString();
					this._flex현합[0, this._flex현합.Cols["002_OUT"].Index] = dataRowArray[0]["NO_DESIGN_002"].ToString();
					this._flex현합[0, this._flex현합.Cols["003"].Index] = dataRowArray[0]["NO_DESIGN_003"].ToString();
					this._flex현합[0, this._flex현합.Cols["003_IN"].Index] = dataRowArray[0]["NO_DESIGN_003"].ToString();
					this._flex현합[0, this._flex현합.Cols["003_OUT"].Index] = dataRowArray[0]["NO_DESIGN_003"].ToString();
					this._flex현합[0, this._flex현합.Cols["004"].Index] = dataRowArray[0]["NO_DESIGN_004"].ToString();
					this._flex현합[0, this._flex현합.Cols["004_IN"].Index] = dataRowArray[0]["NO_DESIGN_004"].ToString();
					this._flex현합[0, this._flex현합.Cols["004_OUT"].Index] = dataRowArray[0]["NO_DESIGN_004"].ToString();
					this._flex현합[0, this._flex현합.Cols["005"].Index] = dataRowArray[0]["NO_DESIGN_005"].ToString();
					this._flex현합[0, this._flex현합.Cols["005_IN"].Index] = dataRowArray[0]["NO_DESIGN_005"].ToString();
					this._flex현합[0, this._flex현합.Cols["005_OUT"].Index] = dataRowArray[0]["NO_DESIGN_005"].ToString();

					this._flex대상품목.ClearData();

					DataTable dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty, 0 });

					foreach (DataRow dr in dataRowArray)
					{
						dt.Merge(this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode, dr["NO_WO"].ToString(), D.GetDecimal(dr["NO_LINE"]) }));
					}

					this._flex현합.Binding = dt;
					((DataTable)this._flex현합.DataSource).DefaultView.Sort = "NO_WO, NO_LINE, SEQ_WO";
				}	
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Ctx품목_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt;
			string query, query1, 품목명;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (this.tabControl2.SelectedTab == this.tpg현합)
				{
					this._flex작업지시.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			     Global.MainFrame.LoginInfo.CdPlant,
																			     this.ctx현합품목.CodeValue,
																			     this.txt작업지시번호.Text,
																				 this.txt수주번호.Text,
																				 this.chk완료제외.Checked == true ? "Y" : "N" });

					if (!this._flex작업지시.HasNormalRow)
					{
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					}
				}
				else
				{
					if (string.IsNullOrEmpty(this.ctx모품목.CodeValue) &&
						string.IsNullOrEmpty(this.txtID번호.Text) &&
						string.IsNullOrEmpty(this.txt수주번호현황.Text))
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl모품목.Text);
						return;
					}

					else if (!string.IsNullOrEmpty(this.txtID번호.Text))
					{
						query = @"SELECT WO.CD_ITEM, MI.NM_ITEM
FROM CZ_PR_MATCHING_DATA AD WITH(NOLOCK)
LEFT JOIN PR_WO WO ON WO.CD_COMPANY = AD.CD_COMPANY AND WO.NO_WO = AD.NO_WO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_ITEM = WO.CD_ITEM
WHERE AD.CD_COMPANY = '{0}'
AND AD.CD_PLANT = '{1}'
AND AD.NO_ID_C = '{2}'";

						dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																		Global.MainFrame.LoginInfo.CdPlant,
																		this.txtID번호.Text));

						if (dt != null && dt.Rows.Count > 0)
						{
							this.ctx모품목.CodeValue = dt.Rows[0]["CD_ITEM"].ToString();
							this.ctx모품목.CodeName = dt.Rows[0]["NM_ITEM"].ToString();
						}
					}
					else
					{
						query = @"SELECT MI1.CD_ITEM,
										 MI1.NM_ITEM 
FROM CZ_PR_MATCHING_ITEM MI WITH(NOLOCK)
LEFT JOIN MA_PITEM MI1 WITH(NOLOCK) ON MI1.CD_COMPANY = MI.CD_COMPANY AND MI1.CD_PLANT = MI.CD_PLANT AND MI1.CD_ITEM = MI.CD_PITEM
WHERE MI.CD_COMPANY = '{0}' 
AND MI.CD_ITEM = '{1}' 
AND TP_POS = '{2}'";

						query1 = @"SELECT MC.CD_FLAG2 AS NM_CLEAR_GRP 
FROM CZ_PR_ROUT_INSP RI WITH(NOLOCK)
JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = RI.CD_COMPANY AND MC.CD_FIELD = 'CZ_WIN0013' AND MC.CD_SYSDEF = RI.CD_CLEAR_GRP
WHERE RI.CD_COMPANY = '{0}'
AND RI.YN_ASSY = 'Y'
AND RI.CD_ITEM = '{1}'
AND MC.CD_FLAG1 = '{2}'";

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this.ctx모품목.CodeValue,
																					   "001" }));

						if (dt != null && dt.Rows.Count == 1)
						{
							품목명 = dt.Rows[0]["NM_ITEM"].ToString();
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN1"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_HEAT1"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_ID_C1"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_LOT1"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN1"].Index] = 품목명;

							object temp = DBHelper.ExecuteScalar(string.Format(query1, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									  dt.Rows[0]["CD_ITEM"].ToString(),
																									  "IN" }));

							if (temp == null)
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE1"].Index] = "클리어런스1";
							else
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE1"].Index] = temp.ToString();
						}

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this.ctx모품목.CodeValue,
																					   "002" }));

						if (dt != null && dt.Rows.Count == 1)
						{
							품목명 = dt.Rows[0]["NM_ITEM"].ToString();
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT2"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_HEAT2"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_ID_C2"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_LOT2"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT2"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN2"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN2"].Index] = 품목명;

							object temp = DBHelper.ExecuteScalar(string.Format(query1, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									  dt.Rows[0]["CD_ITEM"].ToString(),
																									  "IN" }));

							if (temp == null)
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE2"].Index] = "클리어런스2";
							else
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE2"].Index] = temp.ToString();
						}

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this.ctx모품목.CodeValue,
																					   "003" }));

						if (dt != null && dt.Rows.Count == 1)
						{
							품목명 = dt.Rows[0]["NM_ITEM"].ToString();
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT3"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_HEAT3"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_ID_C3"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_LOT3"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT3"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN3"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN3"].Index] = 품목명;

							object temp = DBHelper.ExecuteScalar(string.Format(query1, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									  dt.Rows[0]["CD_ITEM"].ToString(),
																									  "IN" }));

							if (temp == null)
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE3"].Index] = "클리어런스3";
							else
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE3"].Index] = temp.ToString();
						}

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this.ctx모품목.CodeValue,
																					   "004" }));

						if (dt != null && dt.Rows.Count == 1)
						{
							품목명 = dt.Rows[0]["NM_ITEM"].ToString();
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT4"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_HEAT4"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_ID_C4"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_LOT4"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT4"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_IN4"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_IN4"].Index] = 품목명;

							object temp = DBHelper.ExecuteScalar(string.Format(query1, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									  dt.Rows[0]["CD_ITEM"].ToString(),
																									  "IN" }));

							if (temp == null)
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE4"].Index] = "클리어런스4";
							else
								this._flex현황[0, this._flex현황.Cols["QT_CLEARANCE4"].Index] = temp.ToString();
						}

						dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this.ctx모품목.CodeValue,
																					   "005" }));

						if (dt != null && dt.Rows.Count == 1)
						{
							품목명 = dt.Rows[0]["NM_ITEM"].ToString();
							this._flex현황[0, this._flex현황.Cols["CD_GRADE_OUT5"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_HEAT5"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_ID_C5"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["NO_LOT5"].Index] = 품목명;
							this._flex현황[0, this._flex현황.Cols["QT_SPEC_OUT5"].Index] = 품목명;
						}
					}

					this._flex현황.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			  Global.MainFrame.LoginInfo.CdPlant,
																		      this.ctx모품목.CodeValue,
																			  this.txt수주번호현황.Text });

					if (!this._flex현황.HasNormalRow)
					{
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	}
}
