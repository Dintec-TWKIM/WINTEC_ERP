using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;


namespace DX
{
	public partial class WARNING : Duzon.Common.Forms.CommonDialog
	{
		DataTable dtItem;
		ArrayList buttonList = new ArrayList();

		public WARNING_TARGET 적용대상 { get; set; }
		public string 경고구분 { get; set; }
		public string 파일구분 { get; set; }
		public string 매출처코드 { get; set; }
		public string 매입처코드 { get; set; }
		public string IMO번호 { get; set; }
		public DataTable 아이템
		{
			get
			{
				return dtItem;
			}
			set
			{
				if (value != null)
					dtItem = value.Copy();
			}
		}

		public string 메세지 { get; set; }

		public SQLDebug SQLDebug { get; set; } = SQLDebug.Print;


		public bool 경고여부
		{
			get
			{
				return grd경고.HasNormalRow;
			}
		}

		public bool 확인여부
		{
			get
			{
				return grd경고.DataTable.Select("CHK = 'N'").Length == 0;
			}
		}

		public bool 저장불가
		{
			get
			{
				return grd경고.DataTable.Select("YN_SAVE = 'N'").Length > 0;
			}
		}

		public WARNING(WARNING_TARGET 적용대상)
		{
			InitializeComponent();
			InitGrid();
			InitEvent();
			this.적용대상 = 적용대상;
		}

		#region ==================================================================================================== Initialize

		protected override void InitPaint()
		{
			grd견적.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			DataTable dtSaveYn = new DataTable();
			dtSaveYn.Columns.Add("CODE");
			dtSaveYn.Columns.Add("NAME");
			dtSaveYn.Rows.Add("Y", Global.MainFrame.DD("가능"));
			dtSaveYn.Rows.Add("N", Global.MainFrame.DD("불가능"));

			// ********** 경고
			grd경고.BeginSetting(1, 1, false);
			
			grd경고.SetCol("CHK"			, "확인"		, 50);
			grd경고.SetCol("NO_WARNING"	, "경고번호"	, 90	, TextAlignEnum.CenterCenter);
			grd경고.SetCol("NO_DSP"		, "대상순번"	, 90);
			grd경고.SetCol("CD_WARNING"	, "구분"		, 110	, TextAlignEnum.CenterCenter);
			grd경고.SetCol("NM_WARNING"	, "제목"		, 400);
			grd경고.SetCol("YN_SAVE"		, "저장"		, 50	, TextAlignEnum.CenterCenter);
			grd경고.SetCol("NM_BUYER"	, "매출처"	, 210);
			grd경고.SetCol("NM_VENDOR"	, "매입처"	, 210);
			grd경고.SetCol("NO_HULL"		, "호선"		, 210);
			
			grd경고.SetDataMap("CD_WARNING", CODE.코드관리("CZ_MA00013"));
			grd경고.SetDataMap("YN_SAVE", dtSaveYn);

			grd경고.SetDefault("21.10.14.06", SumPositionEnum.None);
			grd경고.SetAlternateRow();
			grd경고.SetMalgunGothic();

			// ********** 견적
			grd견적.BeginSetting(1, 1, false);
			
			grd견적.SetCol("NO_DSP"			, "순번"		, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd견적.SetCol("NM_SUBJECT"		, "주제"		, 200);
			grd견적.SetCol("CD_ITEM_PARTNER"	, "품목코드"	, 130);
			grd견적.SetCol("NM_ITEM_PARTNER"	, "품목명"	, 340);
			grd견적.SetCol("CD_ITEM"			, "재고코드"	, 110);
			grd견적.SetCol("NM_VENDOR"		, "매입처"	, 180);

			grd견적.SetDefault("21.10.12.01", SumPositionEnum.None);
			grd견적.SetAlternateRow();
			grd견적.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn확인.Click += Btn확인_Click;
			btn닫기.Click += Btn닫기_Click;

			grd경고.Paint += new PaintEventHandler(Grd경고_Paint);
			grd경고.AfterRowChange += Grd경고_AfterRowChange;
			grd견적.AfterRowChange += Grd견적_AfterRowChange;			
		}

		private void Btn확인_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (확인여부)
			{
				// SEQ 데이터테이블
				DataTable dtSeq = new DataTable();
				dtSeq.Columns.Add("SEQ", typeof(int));

				foreach (string s in grd경고.GetChanges().Rows[0]["SEQ"].ToString().Split(','))
					dtSeq.Rows.Add(s.ToInt());

				// 저장
				SQL sql = new SQL("PX_CZ_DX_WARNING_CHK", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@XML"		, dtSeq.ToXml());
				sql.ExecuteNonQuery();
				DialogResult = DialogResult.OK;
			}
			else
			{
				UTIL.메세지("모든 경고 항목이 확인되지 않았습니다.", "WK1");
			}			
		}

		private void Btn닫기_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		private void Grd경고_Paint(object sender, PaintEventArgs e)
		{
			foreach (HostedControl hosted in buttonList)
				hosted.UpdatePosition();
		}

		private void Grd경고_AfterRowChange(object sender, RangeEventArgs e)
		{
			// ********** HTML
			// 스타일
			string style = @"
.inq { position:fixed; bottom:0; }";

			// 바디
			string body = @"
<div>
	<table class='dx-viewbox2'>
		<tr>
			<th class='first'>경고내용</th>
		</tr>
		<tr>
			<td>" + grd경고["DC_MSG_KO"].ToString().Trim().Replace("\n", "<br />") + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>포함키워드</th>
		</tr>
		<tr>
			<td class='last'>" + string.Join("<br />", grd경고["KEYS"].ToString().Split(',').Select(x => x.Trim()).Distinct().ToArray()) + @"<br style='visibility:hidden'></td>
		</tr>
	</table>
</div>

<div class='inq'>
	<table class='dx-viewbox2'>
		<tr>
			<th>경고대상</th>
		</tr>
	</table>
</div>";

			web경고.SetDefault();
			web경고.AddStyle(style);
			web경고.AddBody(body);

			// ********** 그리드 바인딩
			grd견적.Binding = 아이템.Select("NO_LINE IN (" + grd경고["NO_LINE"] + ")").ToDataTable();
		}


		private void Grd견적_AfterRowChange(object sender, RangeEventArgs e)
		{
			// ********** 해당 키워드 가져오기
			string[] 라인배열 = grd경고["NO_LINE"].ToString().Split(',').Select(x => x.Trim()).ToArray();
			string[] 키워드배열 = grd경고["KEYS"].ToString().Split(',').Select(x => x.Trim()).ToArray();
			int 인덱스 = 라인배열.IndexOf(grd견적["NO_LINE"].ToString());
			string[] 키워드s = 키워드배열[인덱스].Split(' ').Select(x => x.Trim()).Where(x => x != "").ToArray();

			// ********** 키워드 강조			
			string 주제		= grd견적["NM_SUBJECT"].ToString();
			string 품목코드	= grd견적["CD_ITEM_PARTNER"].ToString();
			string 품목명	= grd견적["NM_ITEM_PARTNER"].ToString();
			string 재고코드	= grd견적["CD_ITEM"].ToString();

			//UTIL.키워드강조(키워드, ref 주제, ref 품목코드, ref 품목명, ref 재고코드);
			bool 키워드찾음 = 키워드.강조(키워드s, ref 주제, ref 품목코드, ref 품목명, ref 재고코드);

			// ********** HTML
			string body = @"
<div>
	<table class='dx-viewbox2'>
		<tr>
			<th class='first'>주제</th>
		</tr>
		<tr>
			<td>" + 주제.Trim().Replace("\n", "<br />") + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>품목코드</th>
		</tr>
		<tr>
			<td>" + 품목코드 + @"<br style='visibility:hidden'></td>
		 </tr>
		<tr>
			<th>품목명</th>
		</tr>
		<tr>
			<td>" + 품목명.Trim().Replace("\n", "<br />") + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>재고코드</th>
		</tr>
		<tr>
			<td class='last'>" + 재고코드 + @"<br style='visibility:hidden'></td>
		</tr>
	</table>
</div>";

			web견적.SetDefault();
			web견적.AddBody(body);
		}

		#endregion


		public void 조회(bool 로그저장)
		{
			if (아이템 == null || 아이템.Rows.Count == 0)
				return;

			try
			{ 
				// ********** 조회
				//DataTable item = 아이템.Copy();
			
				// 매입처 컬럼 이름 변경 및 아이템에 없는 경우 추가
				if (아이템.Columns.Contains("CD_SUPPLIER"))
					아이템.Columns["CD_SUPPLIER"].ColumnName = "CD_VENDOR";
				if (!아이템.Columns.Contains("CD_VENDOR"))
					아이템.Columns.Add("CD_VENDOR", typeof(string), "'" + 매입처코드 + "'");

				if (아이템.Columns.Contains("UM_KR_P"))
					아이템.Columns["UM_KR_P"].ColumnName = "UM_PU";
				if (아이템.Columns.Contains("UM_KR_S"))
					아이템.Columns["UM_KR_S"].ColumnName = "UM_SA";


				if (!아이템.Columns.Contains("UM_PU"))
					아이템.Columns.Add("UM_PU", typeof(decimal));
				if (!아이템.Columns.Contains("UM_SA"))
					아이템.Columns.Add("UM_SA", typeof(decimal));

				// 파일번호가 없을 경우
				if (!아이템.Columns.Contains("NO_FILE"))
				{
					if (아이템.Columns.Contains("NO_PO"))
						아이템.Columns["NO_PO"].ColumnName = "NO_FILE";
				}

				// DSP넘버가 없는 경우
				if (!아이템.Columns.Contains("NO_DSP"))
				{
					아이템.Columns.Add("NO_DSP", typeof(decimal), "NO_LINE");
					//foreach (DataRow row in item.Rows)

				}

				// 재고발주
				if (적용대상 == WARNING_TARGET.재고발주)
				{
					아이템.Columns.Add("NM_SUBJECT", typeof(string), "''");
					아이템.Columns["NO_PART"].ColumnName = "CD_ITEM_PARTNER";
					아이템.Columns["NM_ITEM"].ColumnName = "NM_ITEM_PARTNER";
				}


				// 경고마스터 확인
				SQL sql = new SQL("PS_CZ_DX_WARNING_CHK", SQLType.Procedure, SQLDebug);	
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@CD_WARNING", 경고구분);
				sql.Parameter.Add2("@CD_TARGET"	, 적용대상.ToString());
				sql.Parameter.Add2("@CD_FILE"	, 파일구분 == null ? "" : 파일구분.Left(2));
				sql.Parameter.Add2("@CD_BUYER"	, 매출처코드);
				sql.Parameter.Add2("@NO_IMO"	, IMO번호);
				sql.Parameter.Add2("@YN_LOG"	, 로그저장 ? "Y" : "N");
				sql.Parameter.Add2("@XML"		, 아이템.ToXml("NO_FILE", "NO_LINE", "NO_DSP", "NM_SUBJECT", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_ITEM", "CD_VENDOR", "UM_PU", "UM_SA"));
				DataTable dtWarning = sql.GetDataTable();

				grd경고.Binding = dtWarning;
				grd경고.Col = 2;	// 포커스 변경해줌 (버튼 컬럼에 있으면 테두리 생겨서 보기 시름)

				// ********** 그리드에 버튼 삽입
				for (int i = grd경고.Rows.Fixed; i < grd경고.Rows.Count; i++)
				{
					Button button = new Button
					{
						BackColor = SystemColors.Control
					,	Font = new Font("맑은 고딕", 9F)
					,	TabStop = false
					,	Tag = i
					,	Text = "확인"
					};

					button.Click += Button_Click;
					buttonList.Add(new HostedControl(grd경고, button, i, 1));
				}
			}
			catch (Exception ex)
			{
				UTIL.메세지(ex);
			}
		}

		private void Button_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			int row = button.Tag.ToInt();
			
			grd경고.Row = row;
			grd경고["CHK"] = "Y";
			button.Enabled = false;
		}
	}

	public enum WARNING_TARGET
	{
		견적
	,	수주
	,	발주
	,	재고발주
	,	입고
	,	출고의뢰	
	,	출고
	}

	internal class HostedControl
	{
		internal FlexGrid _flex;
		internal Control _ctl;
		internal Row _row;
		internal Column _col;

		internal HostedControl(FlexGrid flex, Control hosted, int row, int col)
		{
			// save info
			_flex = flex;
			_ctl = hosted;
			_row = flex.Rows[row];
			_col = flex.Cols[col];

			// insert hosted control into grid
			_flex.Controls.Add(_ctl);
		}

		internal bool UpdatePosition()
		{
			// get row/col indexes
			int r = _row.Index;
			int c = _col.Index;

			if (r < 0 || c < 0) return false;

			// get cell rect
			Rectangle rc = _flex.GetCellRect(r, c, false);

			// hide control if out of range
			if (rc.Width <= 0 || rc.Height <= 0 || !rc.IntersectsWith(_flex.ClientRectangle))
			{
				_ctl.Visible = false;
				return true;
			}

			// move the control and show it
			rc.Width -= 1;
			rc.Height -= 1;

			_ctl.Bounds = rc;
			_ctl.Visible = true;

			// done
			return true;
		}
	}
}



