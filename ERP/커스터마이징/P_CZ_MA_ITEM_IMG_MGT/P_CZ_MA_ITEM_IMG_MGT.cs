using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Linq;

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
using System.Net;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GemBox.Spreadsheet;
using System.Drawing.Drawing2D;
using DX;

namespace cz
{
	public partial class P_CZ_MA_ITEM_IMG_MGT : PageBase
	{
		public P_CZ_MA_ITEM_IMG_MGT()
		{
			InitializeComponent();
			this.SetConDefault();
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{			
			dtp조회기간.StartDateToString = UT.Today(-30);
			dtp조회기간.EndDateToString = UT.Today();

			grd라인.DetailGrids = new FlexGrid[] { grd사진 };

			btn회전.Image = 아이콘.회전_18x18;
			btn좌우반전.Image = 아이콘.가로뒤집기_18x18;
			btn상하반전.Image = 아이콘.세로뒤집기_18x18;

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{			
			spc메인.SplitterDistance = spc메인.Width - 1000;
			spc사진.SplitterDistance = 160;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{			
			// ********** 라인
			grd라인.BeginSetting(1, 1, false);
						
			grd라인.SetCol("NO_FILE"			, "파일번호"		, 100	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("LN_PARTNER"		, "매입처"		, 260);
			grd라인.SetCol("NO_LINE"			, "항번"			, false);
			grd라인.SetCol("NO_DSP"			, "순번"			, 45	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 450);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 100	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_UNIT"			, "단위"			, 50	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("UM_EX"			, "매입단가"		, 85	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("LT"				, "납기"			, 45	, typeof(decimal), FormatTpType.MONEY);

			grd라인.SetCol("DT_IMG"			, "촬영일자"		, 90	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd라인.SetCol("CNT_IMG"			, "사진"			, 45	, typeof(decimal), FormatTpType.MONEY);

			grd라인.SetDefault("20.12.04.03", SumPositionEnum.None);
			grd라인.SetAlternateRow();
			grd라인.SetMalgunGothic();
			grd라인.Rows.DefaultSize = 41;

			// ********** 사진
			grd사진.BeginSetting(1, 1, false);
			   
			grd사진.SetCol("NM_FILE"		, "사진"		, 300);
			grd사진.SetCol("DTS_INSERT"	, "등록일"	, 140	, typeof(string), "####/##/## ##:##:##");
			grd사진.SetCol("DC_RMK"		, "비고"		, 200);

			grd사진.SetDefault("20.12.03.02", SumPositionEnum.None);
			grd사진.SetAlternateRow();
			grd사진.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			grd라인.AfterRowChange += Grd라인_AfterRowChange;
			grd사진.AfterRowChange += Grd사진_AfterRowChange;

			chk자동회전사용.CheckedChanged += Chk자동회전사용_CheckedChanged;

			btn회전.Click += Btn회전_Click;
			btn좌우반전.Click += Btn좌우반전_Click;
			btn상하반전.Click += Btn상하반전_Click;

			pic사진.MouseWheel += Pic사진_MouseWheel;
			pic사진.MouseDown += Pic사진_MouseDown;
			pic사진.MouseMove += Pic사진_MouseMove;
			pic사진.Paint += Pic사진_Paint;

			btn테스트.Click += Btn테스트_Click;
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			string query = @"
SELECT
*, NEOE.SPLIT(NO_PO, '-', 1) AS NO_FILE
FROM CZ_PU_POL_FILE
WHERE 1 = 1
	AND LEFT(NO_PO, 8) IN 
('A-210152'
,'CL210266'
,'DS210017'
,'DS210017'
,'DS210017'
,'FB201783'
,'FB210045'
,'FB210224'
,'FB210363'
,'FB210394'
,'NB210029'
)
	AND NEOE.SPLIT(NO_PO, '-', 1)  + '/' + CONVERT(NVARCHAR(5), NO_LINE) IN
('A-21015237/1 '
,'CL210266/1'
,'DS21001784/1'
,'DS21001784/2'
,'DS21001784/3'
,'FB20178351/1'
,'FB21004559/1'
,'FB21022471/1'
,'FB21036353/1'
,'FB21039450/1'
,'NB21002954/1')";

			DataTable dt = SQL.GetDataTable(query);

			foreach (DataRow row in dt.Rows)
			{
				string url = Global.MainFrame.HostURL + "/Upload/CZ_PU_POL_FILE/" + row["CD_COMPANY"] + "/" + row["NO_FILE"] + "/" + row["NM_FILE"];

				Dintec.FILE.Download(url, @"d:/임시/" + row["NO_FILE"] + "-" + row["NO_LINE"] + "-" + row["SEQ"] + ".jpg");
			}
		}

		private void Chk자동회전사용_CheckedChanged(object sender, EventArgs e)
		{
			Grd사진_AfterRowChange(null, null);
		}

		private void Btn회전_Click(object sender, EventArgs e)
		{			
			pic사진.Image.Rotate90();
			pic사진.Invalidate();
		}

		private void Btn좌우반전_Click(object sender, EventArgs e)
		{
			pic사진.Image.FlipX();
			pic사진.Invalidate();
		}

		private void Btn상하반전_Click(object sender, EventArgs e)
		{
			pic사진.Image.FlipY();
			pic사진.Invalidate();
		}

		private double ratio = 1.0F;
		private Rectangle imgRect;
		private Point imgPoint;
		private Point clickPoint;
		
		private void Pic사진_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!(ModifierKeys == Keys.Control))
				return;

			int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

			if (lines > 0)
			{
				ratio *= 1.1F;

				if (ratio > 100.0)
				{
					ratio = 100.0;
					return;
				}

				// 확대할때 사용했던 마우스 포인트를 기록한다 (축소할때는 마우스위치가 어디던 그위치에서 축소함)
				imgPoint = new Point(e.X, e.Y);
			}
			else if (lines < 0)
			{
				ratio *= 0.9F;

				if (ratio < 1)
				{
					ratio = 1;
					return;
				}
			}

			imgRect.Width = (int)Math.Round(pic사진.Width * ratio);
			imgRect.Height = (int)Math.Round(pic사진.Height * ratio);
			imgRect.X = (int)Math.Round(pic사진.Width / 2 - imgPoint.X * ratio);
			imgRect.Y = (int)Math.Round(pic사진.Height / 2 - imgPoint.Y * ratio);

			if (imgRect.X > 0) imgRect.X = 0;
			if (imgRect.Y > 0) imgRect.Y = 0;
			if (imgRect.X + imgRect.Width < pic사진.Width) imgRect.X = pic사진.Width - imgRect.Width;
			if (imgRect.Y + imgRect.Height < pic사진.Height) imgRect.Y = pic사진.Height - imgRect.Height;

			pic사진.Invalidate();
		}		

		private void Pic사진_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				clickPoint = new Point(e.X, e.Y);
		}

		private void Pic사진_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				//int x = imgPoint.X;
				//int y = imgPoint.Y;

				imgRect.X += (int)Math.Round((double)(e.X - clickPoint.X) / 5);

				if (imgRect.X >= 0) imgRect.X = 0;
				if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - pic사진.Width)) imgRect.X = -(imgRect.Width - pic사진.Width);

				imgRect.Y += (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);

				if (imgRect.Y >= 0) imgRect.Y = 0;
				if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - pic사진.Height)) imgRect.Y = -(imgRect.Height - pic사진.Height);

				//x = (int)Math.Round((imgPoint.X - x) * ratio);
				//y = (int)Math.Round((imgPoint.Y - y) * ratio);
				//imgPoint = new Point(x, y);
			}
			
			pic사진.Invalidate();
		}

		private void Pic사진_Paint(object sender, PaintEventArgs e)
		{
			if (pic사진.Image != null)
			{
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				e.Graphics.DrawImage(pic사진.Image, imgRect);
				pic사진.Focus();
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{			
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			UT.ShowPgb("조회중입니다.");

			try
			{
				SQL sql = new SQL("PS_CZ_MA_ITEM_IMG_MGT_H", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@CD_COMPANY", ctx회사코드.CodeValue);
				sql.Parameter.Add2("@NO_FILE"	, tbx파일번호.Text);
				sql.Parameter.Add2("@DT_F"		, dtp조회기간.StartDateToString);
				sql.Parameter.Add2("@DT_T"		, dtp조회기간.EndDateToString);
				sql.Parameter.Add2("@CD_VENDOR"	, ctx매입처.CodeValue);
				sql.Parameter.Add2("@DXKEY"		, tbx키워드.Text);
				DataTable dtLine = sql.GetDataTable();
				grd라인.Binding = dtLine;

				UT.ClosePgb();
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			grd사진.Redraw = false;
			string companyCode = grd라인["CD_COMPANY"].ToString2();
			string purchaseNumber = grd라인["NO_PO"].ToString2();
			string lineNumber = grd라인["NO_LINE"].ToString2();
			DataTable dt = null;

			if (grd라인.DetailQueryNeed)
				dt = SQL.GetDataTable("PS_CZ_MA_ITEM_IMG_MGT_L", companyCode, purchaseNumber, lineNumber);

			grd사진.BindingAdd(dt, "CD_COMPANY = '" + companyCode + "' AND NO_PO = '" + purchaseNumber + "' AND NO_LINE = " + lineNumber);
			grd사진.Redraw = true;
		}

		private void Grd사진_AfterRowChange(object sender, RangeEventArgs e)
		{
			string url = Global.MainFrame.HostURL + "/Upload/CZ_PU_POL_FILE/" + grd사진["CD_COMPANY"] + "/" + grd사진["NO_FILE"] + "/" + grd사진["NM_FILE"];

			using (var ms = new MemoryStream(new WebClient().DownloadData(url)))
			{
				var img = Image.FromStream(ms);

				if (chk자동회전사용.Checked)
				{
					img.NormalizeOrientation();

					// 전면 후면 구분하여 전면이면 좌우반전함
					if (img.GetEquipMaker().ToLower().Contains("samsung") && !(img.Width >= 4032 || img.Height >= 4032))
						img.FlipX();
				}

				if (chk좌우반전.Checked)	img.FlipX();
				if (chk상하반전.Checked) img.FlipY();
				if (chk90.Checked)		img.Rotate90();
				if (chk180.Checked)		img.Rotate180();
				if (chk270.Checked)		img.Rotate270();

				pic사진.Image = img;
				imgRect = new Rectangle(0, 0, pic사진.Width, pic사진.Height);
				ratio = 1.0;
			}
		}

		#endregion
	}
}
