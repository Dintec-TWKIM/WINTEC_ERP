using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dass.FlexGrid;
using DX;

namespace cz
{
	public partial class H_CZ_DLV_CODE_SEL : Duzon.Common.Forms.CommonDialog
	{
		string 매출처코드;
		string IMO번호;

		public string 물류의뢰_코드 { get; set; }
		public string 물류의뢰_이름 { get; set; }
		public string 물류상세_코드 { get; set; }
		public string 물류상세_이름 { get; set; }
		public string 납품처_코드 { get; set; }
		public string 납품처_이름 { get; set; }
		public string 납품처_구분코드 { get; set; }
		public string 납품처_구분이름 { get; set; }
		public string 납품처_주소 { get; set; }
		public string 납품처_담당자 { get; set; }
		public string 납품처_연락처 { get; set; }
		
		public H_CZ_DLV_CODE_SEL(string 매출처코드, string IMO번호)
		{
			InitializeComponent();

			this.페이지초기화();
			this.매출처코드 = 매출처코드;
			this.IMO번호 = IMO번호;
		}

		#region ==================================================================================================== 초기화 == INIT == ㅑㅜㅑㅅ

		protected override void InitLoad()
		{
			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			Btn조회_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID == ㅎ걍

		private void InitGrid()
		{
			// 기본 설정
			grd기본설정.세팅시작(1);

			grd기본설정.컬럼세팅("CD_MAIN_NEW"		, "물류의뢰"		, false);
			grd기본설정.컬럼세팅("NM_MAIN_NEW"		, "물류의뢰"		, 100	, 정렬.가운데);
			grd기본설정.컬럼세팅("CD_SUB_NEW"		, "물류상세"		, false);
			grd기본설정.컬럼세팅("NM_SUB_NEW"		, "물류상세"		, 100	, 정렬.가운데);
			grd기본설정.컬럼세팅("CD_DELIVERY"		, "납품처"		, false);
			grd기본설정.컬럼세팅("NM_DELIVERY"		, "납품처"		, 250);
			grd기본설정.컬럼세팅("TP_ADDRESS"		, "구분코드"		, false);
			grd기본설정.컬럼세팅("NM_ADDRESS"		, "구분이름"		, false);
			grd기본설정.컬럼세팅("DC_ADDRESS"		, "주소"			, false);
			grd기본설정.컬럼세팅("DC_ADDRESS2"		, "주소"			, 400);
			grd기본설정.컬럼세팅("NM_PIC"			, "담당자"		, 150);
			grd기본설정.컬럼세팅("NO_TEL"			, "연락처"		, 180);
			grd기본설정.컬럼세팅("CD_DELIVERY_NEW"	, "납품처(NEW)"	, false);
			grd기본설정.컬럼세팅("NM_DELIVERY_NEW"	, "납품처(NEW)"	, false);
			
			grd기본설정.세팅종료("23.02.16.03", false, false);

			// 주요 사용
			grd주요사용.세팅시작(1);

			grd주요사용.컬럼복사(grd기본설정);
			grd주요사용.컬럼세팅("RATE"		, "비율"		, 60	, 포맷.비율);
			grd주요사용.컬럼세팅("CNT"			, "건수"		, 60	, 포맷.수량);
			
			grd주요사용.세팅종료(grd기본설정.세팅버전(), false, true);	// 데이터테이블 쓸데가 있어서 바인딩 초기화 해줌

			// 최근 작성
			grd최근작성.세팅시작(1);

			grd최근작성.컬럼복사(grd기본설정);
			grd최근작성.컬럼세팅("DT_GIR"		, "의뢰일자"	, 120	, 포맷.날짜);

			grd최근작성.세팅종료(grd기본설정.세팅버전(), false, false);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{			
			btn조회.Click += Btn조회_Click;
			btn취소.Click += Btn취소_Click;

			grd기본설정.DoubleClick += Grd물류업무_DoubleClick;
			grd주요사용.DoubleClick += Grd물류업무_DoubleClick;
			grd최근작성.DoubleClick += Grd물류업무_DoubleClick;
		}

		private void Grd물류업무_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid 그리드 = (FlexGrid)sender;

			if (그리드.헤더클릭())
			{
				그리드스타일(그리드);
			}
			else if (그리드.아이템클릭())
			{
				// 납품처 매핑 테이블에 데이터 있는지 확인
				if (그리드["CD_DELIVERY"].문자() != "" && !그리드["CD_DELIVERY"].문자().발생("DLV") && 그리드["CD_DELIVERY_NEW"].문자() == "")
				{
					메시지.경고알람("신규 납품처가 맵핑되지 않았습니다.");
					return;
				}

				// 바인딩
				물류의뢰_코드 = 그리드["CD_MAIN_NEW"].문자();
				물류의뢰_이름 = 그리드["NM_MAIN_NEW"].문자();
				물류상세_코드 = 그리드["CD_SUB_NEW"].문자();
				물류상세_이름 = 그리드["NM_SUB_NEW"].문자();
				납품처_코드 = 그리드["CD_DELIVERY_NEW"].문자();
				납품처_이름 = 그리드["NM_DELIVERY_NEW"].문자();
				납품처_구분코드 = 그리드["TP_ADDRESS"].문자();
				납품처_구분이름 = 그리드["NM_ADDRESS"].문자();
				납품처_주소 = 그리드["DC_ADDRESS"].문자();
				납품처_담당자 = 그리드["NM_PIC"].문자();
				납품처_연락처 = 그리드["NO_TEL"].문자();
				DialogResult = DialogResult.OK;
			}
		}

		private void Btn취소_Click(object sender, EventArgs e) => Close();

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ

		private void Btn조회_Click(object sender, EventArgs e)
		{
			// ********** 물류업무
			DataTable dt물류업무 = 디비.결과("PS_CZ_DX_DLV_CODE_SET", 상수.회사코드, 매출처코드);
			grd기본설정.바인딩(dt물류업무);	

			// ********** 주요사용 & 전체
			// 전체를 먼저 조회
			디비 db = new 디비("PS_CZ_DX_DLV_CODE_HIS");
			db.변수.추가("@CD_COMPANY"	, 상수.회사코드);
			db.변수.추가("@CD_PARTNER"	, 매출처코드);
			db.변수.추가("@NO_IMO"		, rdo호선.Checked ? IMO번호 : "");
			db.변수.추가("@DAY_LAST"		, rdo3개월.선택().태그());
			DataTable dt전체 = db.결과();

			// 전체를 그룹해서 주요사용 조회
			DataTable dt주요사용 = grd주요사용.데이터테이블().Clone();

			if (dt전체.존재())
			{
				dt주요사용 = dt전체.AsEnumerable().GroupBy(r => new
				{
					CD_MAIN_NEW = r["CD_MAIN_NEW"]
				,	NM_MAIN_NEW = r["NM_MAIN_NEW"]
				,	CD_SUB_NEW = r["CD_SUB_NEW"]
				,	NM_SUB_NEW = r["NM_SUB_NEW"]
				,	CD_DELIVERY = r["CD_DELIVERY"]
				,	NM_DELIVERY = r["NM_DELIVERY"]
				,	TP_ADDRESS = r["TP_ADDRESS"]
				,	NM_ADDRESS = r["NM_ADDRESS"]
				,	DC_ADDRESS = r["DC_ADDRESS"]
				,	DC_ADDRESS2 = r["DC_ADDRESS2"]
				,	NM_PIC = r["NM_PIC"]
				,	NO_TEL = r["NO_TEL"]
				,	CD_DELIVERY_NEW = r["CD_DELIVERY_NEW"]
				,	NM_DELIVERY_NEW = r["NM_DELIVERY_NEW"]
				}).Select(g =>
				{
					var row = dt주요사용.NewRow();
					row["CD_MAIN_NEW"] = g.Key.CD_MAIN_NEW;
					row["NM_MAIN_NEW"] = g.Key.NM_MAIN_NEW;
					row["CD_SUB_NEW"] = g.Key.CD_SUB_NEW;
					row["NM_SUB_NEW"] = g.Key.NM_SUB_NEW;
					row["CD_DELIVERY"] = g.Key.CD_DELIVERY;
					row["NM_DELIVERY"] = g.Key.NM_DELIVERY;
					row["TP_ADDRESS"] = g.Key.TP_ADDRESS;
					row["NM_ADDRESS"] = g.Key.NM_ADDRESS;
					row["DC_ADDRESS"] = g.Key.DC_ADDRESS;
					row["DC_ADDRESS2"] = g.Key.DC_ADDRESS2;
					row["NM_PIC"] = g.Key.NM_PIC;
					row["NO_TEL"] = g.Key.NO_TEL;
					row["CD_DELIVERY_NEW"] = g.Key.CD_DELIVERY_NEW;
					row["NM_DELIVERY_NEW"] = g.Key.NM_DELIVERY_NEW;
					row["CNT"] = g.Count();
					row["RATE"] = g.Count() * 100.0 / dt전체.Rows.Count;
					return row;
				}).OrderByDescending(g => g["RATE"]).CopyToDataTable();
			}

			grd주요사용.바인딩(dt주요사용);
			grd최근작성.바인딩(dt전체);

			// 스타일
			그리드스타일(grd기본설정);
			그리드스타일(grd주요사용);
			그리드스타일(grd최근작성);			
		}

		#endregion


		public void 그리드스타일(FlexGrid 그리드)
		{
			그리드.그리기중지();

			for (int i = 그리드.Rows.Fixed; i < 그리드.Rows.Count; i++)
			{
				그리드.셀글자색_빨강(i, "NM_DELIVERY", 그리드[i, "CD_DELIVERY_NEW"].문자() == "");
			}

			그리드.그리기시작();
		}
	}
}
