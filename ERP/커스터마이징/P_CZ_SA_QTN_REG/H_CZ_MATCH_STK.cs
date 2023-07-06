using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;

using DX;

namespace cz
{
	public partial class H_CZ_MATCH_STK : Duzon.Common.Forms.CommonDialog
	{
		string 회사코드;
		string 파일번호;
		DataRow 견적아이템;
		bool 부품영업;

		public DataRow 재고아이템 => grd재고.선택데이터행();
		
		#region ==================================================================================================== Constructor

		public H_CZ_MATCH_STK(string 회사코드, string 파일번호, DataRow 견적아이템, bool 부품영업)
		{
			InitializeComponent();
			
			this.페이지초기화();
			this.회사코드 = 회사코드;
			this.파일번호 = 파일번호;
			this.견적아이템 = 견적아이템;
			this.부품영업 = 부품영업;

			spc메인.SplitterDistance = 500;
			spc재고.SplitterDistance = 200;

			btn이전.Image = 아이콘.이전_18x18;
			btn다음.Image = 아이콘.다음_18x18;

			btn회전.Image = 아이콘.회전_18x18;
			btn좌우반전.Image = 아이콘.가로뒤집기_18x18;
			btn상하반전.Image = 아이콘.세로뒤집기_18x18;

			InitGrid();
			InitEvent();
			조회();	// 생성자에서 조회해야 팝업할때 자연스럽게 보임 (paint에서 하면 팝업뜰때 조회되서 보기 싫음)
		}

		protected override void InitPaint()
		{
			
			//grd재고.자동행높이();
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID

		private void InitGrid()
		{
			grd재고.세팅시작(2);

			grd재고.컬럼세팅("CD_ITEM"	, "재고코드"		, 110	, 정렬.가운데);
			grd재고.컬럼세팅("NM_ITEM"	, "재고명"		, 300);
			grd재고.컬럼세팅("DC_MODEL"	, "적용모델"		, 110);
			grd재고.컬럼세팅("UCODE"		, "U코드"		, 110);
			grd재고.컬럼세팅("NO_PART"	, "파트넘버"		, 130);
			grd재고.컬럼세팅("UNIT_STK"	, "단위"			, 60	, 정렬.가운데);
			grd재고.컬럼세팅("DC_OFFER_STK"	, "오퍼"			, 120);

			grd재고.컬럼세팅("QT_AVST"	, "가용재고"		, "재고"			, 50	, 포맷.수량);
			grd재고.컬럼세팅("QT_AVPO"	, "가용재고"		, "발주"			, 50	, 포맷.수량);

			grd재고.컬럼세팅("DC_RMK1"	, "비고"			, "추가정보1"		, 230);
			grd재고.컬럼세팅("DC_RMK2"	, "비고"			, "추가정보2"		, 230);

			grd재고.데이터맵("UNIT_STK", 코드.단위());
			grd재고.패널바인딩(lay재고);

			grd재고.세팅종료("23.05.11.01", false);

			// 모드에 따라 컬럼 숨기기
			if (부품영업)
			{

			}
			else
			{				
				grd재고.컬럼숨기기("DC_MODEL", "UCODE");
			}
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{
			btn확정.Click += Btn확정_Click;
			btn취소.Click += Btn취소_Click;

			grd재고.DoubleClick += Grd재고_DoubleClick;
			grd재고.AfterRowChange += Grd재고_AfterRowChange;			

			btn이전.Click += Btn이전_Click;
			btn다음.Click += Btn다음_Click;
			
			btn회전.Click += Btn회전_Click;
			btn좌우반전.Click += Btn좌우반전_Click;
			btn상하반전.Click += Btn상하반전_Click;

			pic사진.MouseWheel += Pic사진_MouseWheel;
			pic사진.MouseDown += Pic사진_MouseDown;
			pic사진.MouseMove += Pic사진_MouseMove;
			pic사진.Paint += Pic사진_Paint;
		}

		private void Btn확정_Click(object sender, EventArgs e)
		{
			if (grd재고.Row > 0)
				DialogResult = DialogResult.OK;
			else
				메시지.경고발생(공통메세지.선택된자료가없습니다);
		}

		private void Btn취소_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Grd재고_DoubleClick(object sender, EventArgs e)
		{
			int row = grd재고.MouseRow;

			// 아이템 클릭
			if (row >= grd재고.Rows.Fixed)
				Btn확정_Click(null, null);
		}

		DataTable 이미지;
		int 이미지인덱스;

		private void Grd재고_AfterRowChange(object sender, RangeEventArgs e)
		{
			// ********** 키워드 강조
			string[] 키워드s	= grd재고["KEYS"].문자().분할(" ").Select(x => x.Trim()).Where(x => x != "").ToArray();	
			string 주제		= 견적아이템["NM_SUBJECT"].문자();
			string 품목코드	= 견적아이템["CD_ITEM_PARTNER"].문자();
			string 품목명	= 견적아이템["NM_ITEM_PARTNER"].문자();
			string 재고코드	= "";

			bool 키워드찾음 = 키워드.강조(키워드s, ref 주제, ref 품목코드, ref 품목명, ref 재고코드);

			// 키워드를 못찾았으면 화이트스페이에 의한 것일수 잇으므로 다시 검색
			if (키워드s.Length > 0 && !키워드찾음)
			{
				string 키워드_옛 = 키워드s[0];
				string 키워드_신 = "";

				// U코드에서만 일어나는 현상이므로 첫번째 키워드만 변경
				for (int i = 0; i < 키워드s[0].Length; i++)
					키워드_신 += 키워드s[0][i] + @"\s*";

				키워드s[0] = 키워드_신;
				키워드.강조(키워드s, ref 주제, ref 품목코드, ref 품목명, ref 재고코드);
				키워드s[0] = 키워드_옛;	// 화면상에 표시해줘야해서 다시 돌림
			}


			// ********** HTML
			string body = @"
<div>
	<table class='dx-viewbox2'>
		<tr>
			<th>키워드</th>
		</tr>
		<tr>
			<td>" + string.Join(" ", 키워드s) + @"<br style='visibility:hidden'></td>
		</tr>
		<tr>
			<th>주제</th>
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
	</table>
</div>";
			
			web견적.바인딩("", body, false);

			// ********** 재고 정보
			헤더 헤더 = new 헤더();
			헤더.컨테이너(lay재고);
			헤더.바인딩(grd재고.선택데이터행());

			// ********** 사진 표시
			이미지 = 디비.결과("PS_CZ_MA_PITEM_FILE", 회사코드, grd재고["CD_ITEM"]);
			이미지인덱스 = 0;
			lbl사진.Text = "0 / 0";
			pic사진.Image = null;

			if (이미지.Rows.Count == 0)
			{
				pic사진.SizeMode = PictureBoxSizeMode.CenterImage;
				pic사진.Image = 아이콘.이미지없음;				
			}
			else
			{
				pic사진.SizeMode = PictureBoxSizeMode.StretchImage;
				이미지보기(이미지.Rows[이미지인덱스]["NM_FILE"].문자());
			}			
		}

		private void 이미지보기(string 파일이름)
		{
			
			lbl사진.Text = (이미지인덱스 + 1) + " / " + 이미지.Rows.Count;
			string url = 상수.호스트URL + "/Upload/P_CZ_MA_PITEM/" + 회사코드 + "/" + grd재고["CD_ITEM"] + "/" + 파일이름;

			using (var ms = new MemoryStream(new WebClient().DownloadData(url)))
			{
				var img = Image.FromStream(ms);
				img.방향표준화();

				// 전면 후면 구분하여 전면이면 좌우반전함
				if (img.제조사().소문자().발생("samsung") && !(img.Width >= 4032 || img.Height >= 4032))
					img.뒤집기_가로();

				pic사진.Image = img;
				imgRect = new Rectangle(0, 0, pic사진.Width, pic사진.Height);
				ratio = 1.0;
			}
		}

		private void Btn이전_Click(object sender, EventArgs e)
		{
			if (이미지.Rows.Count > 1)
			{
				이미지인덱스 = (이미지인덱스 - 1 + 이미지.Rows.Count) % 이미지.Rows.Count;
				이미지보기(이미지.Rows[이미지인덱스]["NM_FILE"].문자());
			}
		}

		private void Btn다음_Click(object sender, EventArgs e)
		{
			if (이미지.Rows.Count > 1)
			{
				이미지인덱스 = (이미지인덱스 + 1) % 이미지.Rows.Count;
				이미지보기(이미지.Rows[이미지인덱스]["NM_FILE"].문자());
			}
		}

		private void Btn회전_Click(object sender, EventArgs e)
		{
			pic사진.Image.회전_90도();
			pic사진.Invalidate();
		}

		private void Btn좌우반전_Click(object sender, EventArgs e)
		{
			pic사진.Image.뒤집기_가로();
			pic사진.Invalidate();
		}

		private void Btn상하반전_Click(object sender, EventArgs e)
		{
			pic사진.Image.뒤집기_세로();
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
				imgRect.X += (int)Math.Round((double)(e.X - clickPoint.X) / 5);

				if (imgRect.X >= 0) imgRect.X = 0;
				if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - pic사진.Width)) imgRect.X = -(imgRect.Width - pic사진.Width);

				imgRect.Y += (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);

				if (imgRect.Y >= 0) imgRect.Y = 0;
				if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - pic사진.Height)) imgRect.Y = -(imgRect.Height - pic사진.Height);
			}
			
			pic사진.Invalidate();
		}

		private void Pic사진_Paint(object sender, PaintEventArgs e)
		{
			if (이미지.존재() && pic사진.Image != null)
			{
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				e.Graphics.DrawImage(pic사진.Image, imgRect);
				pic사진.Focus();
			}
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ초

		private void 조회()
		{
			DataTable dt = 디비.결과("PI_CZ_SA_INQ_STMCH_GS_SNGL", 회사코드, 파일번호, 견적아이템["NO_LINE"]);
			grd재고.바인딩(dt);
		}

		#endregion
	}
}
