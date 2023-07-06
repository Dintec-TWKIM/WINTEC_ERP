using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using DX;


namespace cz
{
	public partial class H_CZ_MAIL_SEL : Duzon.Common.Forms.CommonDialog
	{
		string 매출처코드;
		string IMO번호;
		string 파일번호;

		// 메일 파싱 관련
		string[] 파싱제외도메인 = { "dintec.co.kr", "dubheco.com", "shipserv.com" };
		List<string> 파싱메일 = new List<string>();
		string 발신자 = "";

		public DataTable 결과 => grd선택.데이터테이블();

		public H_CZ_MAIL_SEL(string 매출처코드, string IMO번호, string 파일번호)
		{
			InitializeComponent();

			this.페이지초기화();
			this.매출처코드 = 매출처코드;
			this.IMO번호 = IMO번호;
			this.파일번호 = 파일번호;
		}

		#region ==================================================================================================== 초기화 == INIT

		protected override void InitLoad()
		{
			InitGrid();
			InitEvent();

			// 납품지시서 발신자, 파싱은 어짜피 한번만 조회하면 되니 로드할때 준비함
			string 납품지시서 = 워크플로우.다운로드(파일번호, "51", false);

			if (파일.확장자(납품지시서).소문자() == "msg")
			{
				메일 mail = new 메일(납품지시서);
				발신자 = mail.발신자;

				// 납품지시서 발신자
				발신자 = mail.발신자;

				// 납품지시서 메일본문 파싱
				foreach (Match m in Regex.Matches(mail.내용, 상수.메일패턴, RegexOptions.IgnoreCase | RegexOptions.Singleline))									
					파싱메일.Add(m.Value);
					
				// 납품지시서 첨부파일 파싱
				foreach (메일첨부파일 f in mail.첨부파일)
				{
					string file = f.저장();

					if (파일.확장자(file).소문자() == "pdf")
					{
						foreach (Match m in Regex.Matches(PDF.텍스트(file), 상수.메일패턴, RegexOptions.IgnoreCase | RegexOptions.Singleline))						
							파싱메일.Add(m.Value);

					}
				}
			}

			// 미리 조회 한번 해줌
			Btn조회_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID == ㅎ걍

		private void InitGrid()
		{
			// 기본 설정
			grd기본설정.세팅시작(1);

			grd기본설정.컬럼세팅("SEQ"		, "순번"		, false);
			grd기본설정.컬럼세팅("TYPE"	, "구분"		, 120	, 정렬.가운데);
			grd기본설정.컬럼세팅("NM_PTR"	, "담당자"	, 250);
			grd기본설정.컬럼세팅("NM_MAIL"	, "이메일"	, 350);

			grd기본설정.세팅종료("23.02.09.06", false, false);

			// 발신 이력
			grd발신이력.세팅시작(1);

			grd발신이력.컬럼세팅("SEQ"		, "순번"		, false);
			grd발신이력.컬럼세팅("DT_GIR"	, "구분"		, 120	, 포맷.날짜);
			grd발신이력.컬럼세팅("NM_PTR"	, "담당자"	, 250);
			grd발신이력.컬럼세팅("NM_MAIL"	, "이메일"	, 350);

			grd발신이력.세팅종료(grd기본설정.세팅버전(), false, false);

			// 선택
			grd선택.세팅시작(1);

			grd선택.컬럼세팅("SEQ"	, "순번"		, false);
			grd선택.컬럼세팅("TYPE"	, "구분"		, 120	, 정렬.가운데);
			grd선택.컬럼세팅("NM_PTR"	, "담당자"	, 250);
			grd선택.컬럼세팅("NM_MAIL", "이메일"	, 350);

			grd선택.에디트컬럼("NM_MAIL");
			grd선택.세팅종료(grd기본설정.세팅버전(), false, true);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{
			btn조회.Click += Btn조회_Click;
			btn확정.Click += Btn확정_Click;
			btn취소.Click += Btn취소_Click;

			btn기본설정추가.Click += Btn기본설정추가_Click;
			btn발신이력추가.Click += Btn발신이력추가_Click;
			btn선택추가.Click += Btn선택추가_Click;
			btn선택삭제.Click += Btn선택삭제_Click;

			grd기본설정.DoubleClick += Grd기본설정_DoubleClick;
			grd발신이력.DoubleClick += Grd발신이력_DoubleClick;
			grd선택.BeforeEdit += Grd선택_BeforeEdit;
			grd선택.ValidateEdit += Grd선택_ValidateEdit;
		}

		private void Btn확정_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

		private void Btn취소_Click(object sender, EventArgs e) => Close();

		private void Btn기본설정추가_Click(object sender, EventArgs e)
		{			
			if (!grd기본설정.HasNormalRow)
				return;

			// 중복 체크
			if (grd선택.데이터테이블("NM_MAIL = '" + grd기본설정["NM_MAIL"] + "'").존재())
			{
				메시지.경고알람("중복");
				return;
			}

			// 추가
			grd선택.행추가();
			grd선택["SEQ"]		= grd기본설정["SEQ"];
			grd선택["TYPE"]		= grd기본설정["TYPE"];
			grd선택["NM_PTR"]	= grd기본설정["NM_PTR"];
			grd선택["NM_MAIL"]	= grd기본설정["NM_MAIL"];
			grd선택.행추가완료();
		}

		private void Btn발신이력추가_Click(object sender, EventArgs e)
		{
			if (!grd발신이력.HasNormalRow)
				return;

			// 중복 체크
			if (grd선택.데이터테이블("NM_MAIL = '" + grd발신이력["NM_MAIL"] + "'").존재())
			{
				메시지.경고알람("중복");
				return;
			}

			// 추가
			grd선택.행추가();
			grd선택["SEQ"]		= grd발신이력["SEQ"];
			grd선택["TYPE"]		= "발신 이력";
			grd선택["NM_PTR"]	= grd발신이력["NM_PTR"];
			grd선택["NM_MAIL"]	= grd발신이력["NM_MAIL"];
			grd선택.행추가완료();
		}

		private void Btn선택추가_Click(object sender, EventArgs e)
		{
			// 요기는 수기로 담당자 추가할때
			grd선택.행추가();
			grd선택["SEQ"] = "-2";	// 일단 -2로 해놓고 (그래야 에딧할때 구분되니) 나중에 처리
			grd선택["TYPE"] = "직접 입력";
			grd선택.행추가완료();

			// 포커스
			grd선택.Col = grd선택.컬럼인덱스("NM_MAIL");
			grd선택.Focus();
		}

		private void Btn선택삭제_Click(object sender, EventArgs e)
		{
			grd선택.행삭제();
		}

		private void Grd기본설정_DoubleClick(object sender, EventArgs e)
		{
			if (grd기본설정.아이템클릭())
				Btn기본설정추가_Click(null, null);
		}

		private void Grd발신이력_DoubleClick(object sender, EventArgs e)
		{
			if (grd발신이력.아이템클릭())
				Btn발신이력추가_Click(null, null);
		}

		private void Grd선택_BeforeEdit(object sender, RowColEventArgs e)
		{
			// 직접 입력 아니면 수정 금지
			if (grd선택["SEQ"].정수() != -2)
				e.Cancel = true;
		}

		private void Grd선택_ValidateEdit(object sender, ValidateEditEventArgs e)
		{			
			// 복붙은 에딧 이벤트 안타서 요고 있어야함
			if (grd선택["SEQ"].정수() != -2)
			{
				grd선택["NM_MAIL"] = grd선택.GetData("NM_MAIL");
				return;
			}

			// 중복 체크
			if (grd선택.데이터테이블("NM_MAIL = '" + grd선택["NM_MAIL"] + "'").존재())
			{
				grd선택["NM_MAIL"] = "";
				메시지.경고알람("중복");
				return;
			}

			// 주소 검사
			if (!메일.주소검사(grd선택["NM_MAIL"].문자()))
			{
				string mail = grd선택["NM_MAIL"].문자();
				grd선택["NM_MAIL"] = "";
				메시지.경고알람(mail + "\n\r올바른 메일 주소 형식이 아닙니다.");
			}
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ초

		private void Btn조회_Click(object sender, EventArgs e)
		{
			// ********** 기본 설정
			DataTable dt기본설정 = 디비.결과("PS_CZ_DX_DLV_MAIL_SET", 상수.회사코드, 매출처코드, rdo호선.Checked ? IMO번호 : "");
			dt기본설정.컬럼추가("TYPE", "기본 설정");

			// 납품지시서 발신자
			if (발신자 != "")
			{
				dt기본설정.Rows.Add();
				dt기본설정.Rows[dt기본설정.Rows.Count - 1]["SEQ"] = -1;
				dt기본설정.Rows[dt기본설정.Rows.Count - 1]["TYPE"] = "발신자";
				dt기본설정.Rows[dt기본설정.Rows.Count - 1]["NM_MAIL"] = 발신자;
			}

			// 파싱
			foreach (string s in 파싱메일)
			{
				if (!메일.주소검사(s)) continue;
				if (s.발생(파싱제외도메인)) continue;
				if (dt기본설정.선택("NM_MAIL = '" + s + "'").존재()) continue;

				dt기본설정.Rows.Add();
				dt기본설정.Rows[dt기본설정.Rows.Count - 1]["SEQ"] = -1;
				dt기본설정.Rows[dt기본설정.Rows.Count - 1]["TYPE"] = "파싱";
				dt기본설정.Rows[dt기본설정.Rows.Count - 1]["NM_MAIL"] = s;
			}

			grd기본설정.바인딩(dt기본설정);

			// ********** 최근 발신 이력
			DataTable dt발신이력 = 디비.결과("PS_CZ_DX_DLV_MAIL_HIS", 상수.회사코드, 매출처코드, rdo호선.Checked ? IMO번호 : "", rdo3개월.선택().태그());
			grd발신이력.바인딩(dt발신이력);
		}

		#endregion
	}
}
