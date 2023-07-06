using System;
using System.Data;
using System.Windows.Forms;
using DX;

namespace cz
{
	public partial class H_CZ_DLV_PARTNER_SEL : Duzon.Common.Forms.CommonDialog
	{		
		public string 납품처_코드 { get; set; }
		public string 납품처_이름 { get; set; }
		public string 납품처_구분코드 { get; set; }
		public string 납품처_구분이름 { get; set; }
		public string 납품처_주소 { get; set; }
		public string 납품처_담당자 { get; set; }
		public string 납품처_연락처 { get; set; }
		
		public H_CZ_DLV_PARTNER_SEL()
		{
			InitializeComponent();
		}

		#region ==================================================================================================== 초기화 == INIT == ㅑㅜㅑㅅ

		protected override void InitLoad()
		{
			this.페이지초기화();

			tbx검색.엔터검색();

			InitGrid();
			InitEvent();
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID == ㅎ걍

		private void InitGrid()
		{
			grd검색.세팅시작(1);

			grd검색.컬럼세팅("CD_DELIVERY"	, "납품처코드"	, false);
			grd검색.컬럼세팅("NM_DELIVERY"	, "납품처"		, 250);
			grd검색.컬럼세팅("TP_ADDRESS"		, "구분코드"		, false);
			grd검색.컬럼세팅("NM_ADDRESS"		, "구분이름"		, false);
			grd검색.컬럼세팅("DC_ADDRESS"		, "주소"			, false);
			grd검색.컬럼세팅("DC_ADDRESS2"	, "주소"			, 400);
			grd검색.컬럼세팅("NM_PIC"			, "담당자"		, 150);
			grd검색.컬럼세팅("NO_TEL"			, "연락처"		, 180);

			grd검색.세팅종료("23.03.20.01", false, false);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{			
			btn조회.Click += Btn조회_Click;
			btn취소.Click += Btn취소_Click;
			grd검색.DoubleClick += Grd검색_DoubleClick;
		}

		private void Grd검색_DoubleClick(object sender, EventArgs e)
		{
			if (grd검색.아이템클릭())
			{
				납품처_코드 = grd검색["CD_DELIVERY"].문자();
				납품처_이름 = grd검색["NM_DELIVERY"].문자();
				납품처_구분코드 = grd검색["TP_ADDRESS"].문자();
				납품처_구분이름 = grd검색["NM_ADDRESS"].문자();
				납품처_주소 = grd검색["DC_ADDRESS"].문자();
				납품처_담당자 = grd검색["NM_PIC"].문자();
				납품처_연락처 = grd검색["NO_TEL"].문자();
				DialogResult = DialogResult.OK;
			}
		}

		private void Btn취소_Click(object sender, EventArgs e) => Close();

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ초

		private void Btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt = 디비.결과("PS_CZ_MA_DELIVERY", 상수.회사코드, tbx검색.Text);
			grd검색.바인딩(dt);
		}

		#endregion
	}
}
