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
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using System.Drawing.Drawing2D;
using DX;
using System.Linq;

namespace cz
{
	public partial class P_CZ_DX_WARNING_REG : PageBase
	{
		#region ==================================================================================================== Constructor

		public P_CZ_DX_WARNING_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();			
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			this.페이지초기화();
			dtp접수일자.StartDateToString = UTIL.오늘(-365);
			dtp접수일자.EndDateToString = UTIL.오늘();

			// ********** 콤보박스
			DataTable dt = CODE.코드관리("CZ_MA00013");
			cbo구분.바인딩(dt, true);
			cbo구분S.바인딩(dt, true);

			// ********** 파일구분 동적 추가
			string query = @"
SELECT
	CODE = CD_SYSDEF
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND CD_FLAG1 = 'WF'
	AND CD_SYSDEF NOT IN ('CL', 'PT', 'TE')";

			DataTable dtPrefix = SQL.GetDataTable(query);

			for (int i = 0; i < dtPrefix.Rows.Count; i++)
			{
				CheckBoxExt chk = new CheckBoxExt
				{
					Checked = true
				,	Text = dtPrefix.Rows[i]["CODE"].ToString()
				,	Width = 41
				,	Top = 8
				,	Left = 8 + (i * 51)				
				};
				chk.Click += Chk파일구분_Click;
				pnl적용파일.Controls.Add(chk);
			}

			// ********** 키워드 버튼
			btn포함키추가.Text = "";
			btn제외키추가.Text = "";
			btn제외파일추가.Text = "";

			btn포함키삭제.Text = "";
			btn제외키삭제.Text = "";
			btn제외파일삭제.Text = "";

			btn포함키추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));
			btn제외키추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));
			btn제외파일추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));
			
			btn포함키삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));
			btn제외키삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));
			btn제외파일삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));

			// ********** 기타
			MainGrids = new FlexGrid[] { grd라인, grd포함키, grd제외키, grd제외파일 };
			grd라인.DetailGrids = new FlexGrid[] { grd포함키, grd제외키, grd제외파일 };

			InitGrid();
			InitEvent();

			// ********** 권한
			if (!LoginInfo.GroupID.포함("SPO", "ADMIN"))
			{
				btn제외파일추가.Enabled = false;
				btn제외파일삭제.Enabled = false;
				pnl재고관리여부.사용(false);
			}

			// 팀장들은 제외파일 권한줌
			if (코드.사원(상수.사원번호).첫행("CD_DUTY_RESP").문자().포함("200", "400"))	// 200:부서장,400:팀장
			{
				btn제외파일추가.Enabled = true;
				btn제외파일삭제.Enabled = true;
			}
		}

		protected override void InitPaint()
		{
			spc메인.SplitterDistance = spc메인.Width - 1320;
			spc키워드.SplitterDistance = spc키워드.Width - 520;
			spc키워드2.SplitterDistance = spc키워드2.Width / 2;

			pnl포함키1.BackColor = Color.Blue;
			pnl포함키2.BackColor = Color.White;

			pnl제외키1.BackColor = Color.Red;
			pnl제외키2.BackColor = Color.White;

			pnl제외파일1.BackColor = Color.Red;
			pnl제외파일2.BackColor = Color.White;

			grd포함키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd제외키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd제외파일.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			DataTable dtCode = new DataTable();
			dtCode.Columns.Add("TYPE");
			dtCode.Columns.Add("CODE");
			dtCode.Columns.Add("NAME");

			dtCode.Rows.Add("YN_SAVE", "Y", DD("가능"));
			dtCode.Rows.Add("YN_SAVE", "N", DD("불가능"));

			dtCode.Rows.Add("YN_CD_ITEM", "Y", DD("재고"));
			dtCode.Rows.Add("YN_CD_ITEM", "N", DD("상시"));

			// ********** 경고마스터
			grd라인.BeginSetting(1, 1, false);

			grd라인.SetCol("NO_WARNING"		, "경고번호"		, 80	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("CD_WARNING"		, "구분"			, 90	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_WARNING"		, "제목"			, 300);
			grd라인.SetCol("YN_SAVE"			, "저장"			, 60	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("YN_CD_ITEM"		, "팝업"			, 60	, TextAlignEnum.CenterCenter);
			
			grd라인.SetCol("CD_BUYER"		, "매출처코드"	, false);
			grd라인.SetCol("NM_BUYER"		, "매출처명"		, 170);			
			grd라인.SetCol("CD_VENDOR"		, "매입처코드"	, false);
			grd라인.SetCol("NM_VENDOR"		, "매입처명"		, 170);			
			grd라인.SetCol("NO_IMO"			, "IMO번호"		, false);
			grd라인.SetCol("NO_HULL"			, "호선"			, 170);

			grd라인.SetCol("DC_MSG_KO"		, "경고문(국문)"	, false);
			grd라인.SetCol("DC_MSG_EN"		, "경고문(영문)"	, false);
			grd라인.SetCol("DC_RMK"			, "비고"			, false);
			grd라인.SetCol("YN_USE"			, "사용여부"		, false);
			grd라인.SetCol("ID_INSERT"		, "등록자"		, 70	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("DTS_INSERT"		, "등록일"		, 140	, typeof(string), "####/##/## ##:##:##");
			grd라인.SetCol("ID_UPDATE"		, "수정자"		, 70	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("DTS_UPDATE"		, "수정일"		, 140	, typeof(string), "####/##/## ##:##:##");

			grd라인.SetCol("ID_INSERT2"		, "등록자"		, false);
			grd라인.SetCol("ID_UPDATE2"		, "수정자"		, false);

			grd라인.SetDataMap("CD_WARNING", CODE.코드관리("CZ_MA00013"));
			grd라인.SetDataMap("YN_SAVE", dtCode.Select("TYPE = 'YN_SAVE'"));
			grd라인.SetDataMap("YN_CD_ITEM", dtCode.Select("TYPE = 'YN_CD_ITEM'"));
			
			grd라인.SetBinding(lay상세);			
			grd라인.SetBindningRadioButton(new RadioButtonExt[] { rdo저장여부Y, rdo저장여부N }, new string[] { "Y", "N" });
			grd라인.SetBindningRadioButton(new RadioButtonExt[] { rdo재고코드Y, rdo재고코드N }, new string[] { "Y", "N" });
			grd라인.SetBindningRadioButton(new RadioButtonExt[] { rdo관리여부Y, rdo관리여부N }, new string[] { "Y", "N" });
			grd라인.SetBindningRadioButton(new RadioButtonExt[] { rdo사용여부Y, rdo사용여부N }, new string[] { "Y", "N" });
			grd라인.VerifyNotNull = new string[] { "CD_WARNING", "NM_WARNING" };

			grd라인.SetDefault("21.11.09.01", SumPositionEnum.None);
			grd라인.SetAlternateRow();
			grd라인.SetMalgunGothic();

			// ********** 트레이닝
			grd트레이닝.BeginSetting(1, 1, false);

			grd트레이닝.SetCol("CHK"				, "S"			, 30	, CheckTypeEnum.Y_N);
			grd트레이닝.SetCol("NO_FILE"			, "파일번호"		, 90	, TextAlignEnum.CenterCenter);
			grd트레이닝.SetCol("NO_LINE"			, "항번"			, false);
			grd트레이닝.SetCol("NO_DSP"			, "순번"			, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd트레이닝.SetCol("NM_SUBJECT"		, "주제"			, 300);
			grd트레이닝.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 150);
			grd트레이닝.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 300);
			grd트레이닝.SetCol("NM_VENDOR"		, "매입처"		, 100);
			grd트레이닝.SetCol("CD_ITEM"			, "재고코드"		, 110	, TextAlignEnum.CenterCenter);
			grd트레이닝.SetCol("CD_ITEM_NEW"		, "재고코드(후)"	, 110	, TextAlignEnum.CenterCenter);
			grd트레이닝.SetCol("UM_KR_P"			, "원화단가"		, 75	, typeof(decimal), FormatTpType.MONEY);

			grd트레이닝.SetDefault("21.10.21.02", SumPositionEnum.None);
			grd트레이닝.SetEditColumn("CHK", "CD_ITEM_NEW");
			grd트레이닝.SetAlternateRow();
			grd트레이닝.SetMalgunGothic();

			// ********** 포함키
			grd포함키.BeginSetting(1, 1, false);

			grd포함키.SetCol("CD_COMPANY", "회사코드"	, false);
			grd포함키.SetCol("NO_WARNING", "재고코드"	, false);
			grd포함키.SetCol("SEQ"		, "순번"		, false);
			grd포함키.SetCol("KEY1"		, "포함"		, 180	, true);
			grd포함키.SetCol("KEY2"		, "포함"		, 180	, true);
			grd포함키.SetCol("KEY3"		, "포함"		, 180	, true);
			grd포함키.SetCol("KEY4"		, "포함"		, 180	, true);

			grd포함키.VerifyNotNull = new string[] { "KEY1" };
			grd포함키.SetDefault("21.10.26.01", SumPositionEnum.None);
			grd포함키.SetAlternateRow();
			grd포함키.SetMalgunGothic();

			// ********** 제외키
			grd제외키.BeginSetting(1, 1, false);

			grd제외키.SetCol("CD_COMPANY", "회사코드"	, false);
			grd제외키.SetCol("NO_WARNING", "재고코드"	, false);
			grd제외키.SetCol("SEQ"		, "순번"		, false);
			grd제외키.SetCol("KEY1"		, "제외"		, 180	, true);

			grd제외키.VerifyNotNull = new string[] { "KEY1" };
			grd제외키.SetDefault("21.10.26.01", SumPositionEnum.None);
			grd제외키.SetAlternateRow();
			grd제외키.SetMalgunGothic();

			// ********** 제외키
			grd제외파일.BeginSetting(1, 1, false);

			grd제외파일.SetCol("CD_COMPANY"	, "회사코드"	, false);
			grd제외파일.SetCol("NO_WARNING"	, "재고코드"	, false);
			grd제외파일.SetCol("SEQ"			, "순번"		, false);
			grd제외파일.SetCol("KEY1"			, "파일번호"	, 180	, true);

			grd제외파일.VerifyNotNull = new string[] { "KEY1" };
			grd제외파일.SetDefault("21.10.26.01", SumPositionEnum.None);
			grd제외파일.SetAlternateRow();
			grd제외파일.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			grd라인.AfterRowChange += Grd라인_AfterRowChange;

			cbo매출처.QueryAfter += Cbo매출처_QueryAfter;
			cbo매입처.QueryAfter += Cbo매입처_QueryAfter;
			cbo호선.QueryAfter += Cbo호선_QueryAfter;

			btn포함키추가.Click += Btn키워드추가_Click;
			btn제외키추가.Click += Btn키워드추가_Click;
			btn제외파일추가.Click += Btn키워드추가_Click;

			btn포함키삭제.Click += Btn키워드삭제_Click;
			btn제외키삭제.Click += Btn키워드삭제_Click;
			btn제외파일삭제.Click += Btn키워드삭제_Click;

			btn트레이닝.Click += Btn트레이닝_Click;
			grd트레이닝.DoubleClick += Grd트레이닝_DoubleClick;
		}

		private void Chk파일구분_Click(object sender, EventArgs e)
		{
			string value = "";

			foreach (Control con in pnl적용파일.Controls)
			{
				if (con is CheckBoxExt chk && chk.Checked)
					value += chk.Text + ",";
			}

			grd라인["CD_FILE"] = value;
		}

		private void Cbo매출처_QueryAfter(object sender, BpQueryArgs e)
		{
			grd라인["CD_BUYER"] = cbo매출처.QueryWhereIn_Pipe;
			grd라인["NM_BUYER"] = cbo매출처.QueryWhereIn_PipeDisplayMember;
		}

		private void Cbo매입처_QueryAfter(object sender, BpQueryArgs e)
		{
			grd라인["CD_VENDOR"] = cbo매입처.QueryWhereIn_Pipe;
			grd라인["NM_VENDOR"] = cbo매입처.QueryWhereIn_PipeDisplayMember;
		}

		private void Cbo호선_QueryAfter(object sender, BpQueryArgs e)
		{
			grd라인["NO_IMO"] = cbo호선.QueryWhereIn_Pipe;
			grd라인["NO_HULL"] = cbo호선.QueryWhereIn_PipeDisplayMember;
		}

		private void Btn키워드추가_Click(object sender, EventArgs e)
		{
			string name = "grd" + ((Button)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			flexGrid.Rows.Add();
			flexGrid.Row = flexGrid.Rows.Count - 1;
			flexGrid["CD_COMPANY"]	= LoginInfo.CompanyCode;
			flexGrid["NO_WARNING"]	= grd라인["NO_WARNING"];
			flexGrid["SEQ"]			= (int)flexGrid.Aggregate(AggregateEnum.Max, "SEQ") + 1;
			flexGrid["CD_TYPE"]		= flexGrid.태그();
			flexGrid.행추가완료();			
		}

		private void Btn키워드삭제_Click(object sender, EventArgs e)
		{
			string name = "grd" + ((Button)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			if (flexGrid.HasNormalRow)
				flexGrid.Rows.Remove(flexGrid.Row);
		}

		private void Btn트레이닝_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (!grd포함키.HasNormalRow)
			{
				UTIL.메세지("키워드가 없으면 트레이닝을 실행할 수 없습니다.", "WK1");
				return;
			}

			//if (cbm매입처.QueryWhereIn_Pipe == "")
			//{
			//	if (ShowMessage("매입처를 지정하지 않을 경우 많은 시간이 소요됩니다.\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
			//		return;
			//}

			UTIL.작업중("조회중입니다.");

			SQL sqlTrn = new SQL("PS_CZ_DX_WARNING_REG_TRANING", SQLType.Procedure, sqlDebug);
			sqlTrn.Parameter.Add2("@CD_COMPANY"	, LoginInfo.CompanyCode);
			sqlTrn.Parameter.Add2("@NO_WARNING"	, grd라인["NO_WARNING"]);
			sqlTrn.Parameter.Add2("@DT_F"		, dtp접수일자.StartDateToString);
			sqlTrn.Parameter.Add2("@DT_T"		, dtp접수일자.EndDateToString);
			sqlTrn.Parameter.Add2("@CD_BUYER"	, chk매출처.Checked ? cbo매출처.QueryWhereIn_Pipe : "");
			sqlTrn.Parameter.Add2("@CD_VENDOR"	, chk매입처.Checked ? cbo매입처.QueryWhereIn_Pipe : "");
			sqlTrn.Parameter.Add2("@NO_IMO"		, chk호선.Checked ? cbo호선.QueryWhereIn_Pipe : "");
			//sqlTrn.Parameter.Add2("@YN_MK"		, chk메인키검색.GetValue());
			//sqlTrn.Parameter.Add2("@SEQ_MK"		, rdo메인키전체.GetCheckedControl().GetTag() == "Y" ? 0 : grd메인키["SEQ"].ToInt());
			//sqlTrn.Parameter.Add2("@YN_SK"		, chk서브키검색.GetValue());
			//sqlTrn.Parameter.Add2("@SEQ_SK1"	, rdo서브키전체.GetCheckedControl().GetTag() == "Y" ? 0 : grd서브키1["SEQ"].ToInt());
			//sqlTrn.Parameter.Add2("@SEQ_SK2"	, rdo서브키전체.GetCheckedControl().GetTag() == "Y" ? 0 : grd서브키2["SEQ"].ToInt());

			DataTable dtTrn = sqlTrn.GetDataTable();
			grd트레이닝.Binding = dtTrn;
			tab메인.SelectedTab = tab트레이닝;

			UTIL.작업중();
		}

		private void Grd트레이닝_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;
			int row = flexGrid.MouseRow;

			// 헤더클릭
			if (row < flexGrid.Rows.Fixed)
			{
				return;
			}

			// 키워드 헬프팝업
			string[] 키워드 = grd트레이닝["KEYS"].ToString().Split(' ').Select(x => x.Trim()).Where(x => x != "").ToArray();
			DataRow 아이템 = grd트레이닝.GetDataRow(grd트레이닝.Row);
			KEY_CHECK kc = new KEY_CHECK(키워드, 아이템);
			kc.ShowDialog();
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			try
			{ 
				SQL sql = new SQL("PS_CZ_DX_WARNING_REG", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_WARNING", tbx경고번호.Text);
				sql.Parameter.Add2("@CD_WARNING", cbo구분S.SelectedValue);
				sql.Parameter.Add2("@NM_WARNING", tbx제목S.Text);
				sql.Parameter.Add2("@YN_USE"	, rdo사용여부A.선택().태그());
				sql.Parameter.Add2("@CD_BUYER"	, ctx매출처.CodeValue);
				sql.Parameter.Add2("@CD_VENDOR"	, ctx매입처.CodeValue);
				sql.Parameter.Add2("@NO_IMO"	, ctx호선.CodeValue);
				sql.Parameter.Add2("@KEYWORD"	, tbx키워드.Text);
			
				DataTable dt = sql.GetDataTable();
				grd라인.Binding = dt;
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			cbo매출처.Clear();
			cbo매입처.Clear();
			cbo호선.Clear();

			cbo매출처.AddItem2(grd라인["CD_BUYER"].ToString(), grd라인["NM_BUYER"].ToString());
			cbo매입처.AddItem2(grd라인["CD_VENDOR"].ToString(), grd라인["NM_VENDOR"].ToString());
			cbo호선.AddItem2(grd라인["NO_IMO"].ToString(), grd라인["NO_HULL"].ToString());

			// ********** 파일구분은 수동바인딩
			string 파일코드 = grd라인["CD_FILE"].ToString();

			foreach (Control con in pnl적용파일.Controls)
			{
				if (con is CheckBoxExt chk)
					chk.Checked = 파일코드.Contains(chk.Text);
			}

			// ********** 키워드 바인딩
			string 경고번호 = grd라인["NO_WARNING"].ToString();
			string 필터 = "NO_WARNING = '" + 경고번호 + "'";

			if (grd라인.DetailQueryNeed)
			{
				DataSet ds = SQL.GetDataSet("PS_CZ_DX_WARNING_KEY", LoginInfo.CompanyCode, 경고번호);
				grd포함키.BindingAdd(ds.Tables[0], 필터);
				grd제외키.BindingAdd(ds.Tables[1], 필터);
				grd제외파일.BindingAdd(ds.Tables[2], 필터);
			}
			else
			{
				grd포함키.BindingAdd(null, 필터);
				grd제외키.BindingAdd(null, 필터);
				grd제외파일.BindingAdd(null, 필터);
			}
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			// 이미 추가된 라인이 있는지 확인
			if (grd라인.DataTable.Select("NO_WARNING = '추가'").Length > 0)
			{
				UTIL.메세지("이미 추가 중인 항목이 있습니다.", "WK1");
				return;
			}

			// 파일코드 기본값 : 전체선택
			string 파일코드 = "";
			
			foreach (Control con in pnl적용파일.Controls)
			{
				if (con is CheckBoxExt chk)
					파일코드 += chk.Text + ",";
			}

			// 추가
			grd라인.Rows.Add();
			grd라인.Row = grd라인.Rows.Count - 1;
			grd라인["NO_WARNING"] = "추가";
			grd라인["CD_FILE"] = 파일코드;
			grd라인["YN_SAVE"] = "Y";
			grd라인["YN_CD_ITEM"] = "N";
			grd라인["YN_MGMT"] = "N";
			grd라인["YN_USE"] = "Y";
			grd라인.AddFinished();
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			// 그리드 검사
			if (!base.Verify())
				return;

			try
			{
				// ********** 경고마스터 저장
				SQL sql = new SQL("PX_CZ_DX_WARNING_REG", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@XML"		, grd라인.GetChanges().ToXml());
				sql.Parameter.Add2("@XML_MK"	, grd포함키.GetChanges().ToXml());
				sql.Parameter.Add2("@XML_EX"	, grd제외키.GetChanges().ToXml());
				sql.Parameter.Add2("@XML_EXFI"	, grd제외파일.GetChanges().ToXml());
				DataTable dtResult = sql.GetDataTable();

				// 추가 Row만 경고번호 업데이트
				if (dtResult.Rows.Count > 0)
				{
					grd라인.DataTable.Select("NO_WARNING = '추가'")[0]["NO_WARNING"] = dtResult.Rows[0]["NO_WARNING"];
					if (grd포함키.DataTable.Select("NO_WARNING = '추가'").Length > 0) grd포함키.DataTable.Select("NO_WARNING = '추가'")[0]["NO_WARNING"] = dtResult.Rows[0]["NO_WARNING"];
					if (grd제외키.DataTable.Select("NO_WARNING = '추가'").Length > 0) grd제외키.DataTable.Select("NO_WARNING = '추가'")[0]["NO_WARNING"] = dtResult.Rows[0]["NO_WARNING"];
					if (grd제외파일.DataTable.Select("NO_WARNING = '추가'").Length > 0) grd제외파일.DataTable.Select("NO_WARNING = '추가'")[0]["NO_WARNING"] = dtResult.Rows[0]["NO_WARNING"];
				}

				grd라인.AcceptChanges();
				grd포함키.AcceptChanges();
				grd제외키.AcceptChanges();
				grd제외파일.AcceptChanges();

				UTIL.메세지(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				UTIL.메세지(ex);
			}
		}
		
		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (!grd라인.HasNormalRow) return;
			grd라인.Rows.Remove(grd라인.Row);
		}

		#endregion
	}
}
