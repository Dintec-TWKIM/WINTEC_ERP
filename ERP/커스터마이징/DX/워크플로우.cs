using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU.Utils;


namespace DX
{
	public class 워크플로우
	{
		DataTable dtHead;
		DataTable dtLine;

		public string 파일번호 { get; set; }

		public string 단계 { get; set; }

		public bool 현재단계완료여부 { get; set; } = false;

		public 워크플로우(string 파일번호, string 단계)
		{
			this.파일번호 = 파일번호;
			this.단계 = 단계;
			데이터테이블초기화();
		}

		public 워크플로우(string 파일번호, string 단계, bool 현재단계완료여부)
		{			
			this.파일번호 = 파일번호;
			this.단계 = 단계;
			this.현재단계완료여부 = 현재단계완료여부;
			데이터테이블초기화();			
		}

		private void 데이터테이블초기화()
		{
			// 헤드
			dtHead = new DataTable();
			dtHead.컬럼추가("NO_KEY");
			dtHead.컬럼추가("TP_STEP");
			dtHead.컬럼추가("YN_DONE");
			dtHead.컬럼추가("PAGE_ID");
			dtHead.Rows.Add(파일번호, 단계, 현재단계완료여부 ? "Y" : "N", 상수.페이지ID);	// 어짜피 한줄이니 여기서 행추가

			// 라인
			dtLine = new DataTable();
			dtLine.컬럼추가("NO_KEY");
			dtLine.컬럼추가("TP_STEP");
			dtLine.컬럼추가("NO_LINE", typeof(int));
			dtLine.컬럼추가("NO_KEY_REF");
			dtLine.컬럼추가("CD_SUPPLIER");
			dtLine.컬럼추가("NO_HST");
			dtLine.컬럼추가("NM_FILE");
			dtLine.컬럼추가("NM_FILE_REAL");
			dtLine.컬럼추가("YN_INCLUDED");
		}

		public void 파일추가(string 파일번호_참고, string 매입처, string 파일이름)
		{
			// ********** 서버에 파일 업로드
			// 임시 경로로 복사 (파일이 열려있을때 업로드 안되는것을 해결하기 위해 복사 한번 해줌)
			string 임시파일이름 = 경로.임시() + @"\" + 파일.파일이름(파일이름);
			File.Copy(파일이름, 임시파일이름, true);

			// 업로드 폴더, 파일
			string 업로드경로 = 경로.워크플로우(파일번호);
			string 신규파일이름 = 파일.파일이름_고유(업로드경로, 임시파일이름);

			// 업로드
			string 결과 = FileUploader.UploadFile(신규파일이름, 임시파일이름, "", 업로드경로);

			if (결과.IndexOf("Fail") >= 0)
				throw new Exception(결과);

			// ********** 데이터테이블에 추가
			라인데이터테이블추가(파일번호_참고, 매입처, 파일.파일이름(파일이름), 파일.파일이름(신규파일이름));			
		}

		/// <summary>
		/// 물리적 파일은 고대로 있고 디비에 라인만 추가함 (어짜피 한폴더에 다 있으니 용량 아깝게 물리적 파일은 복사 안함)
		/// </summary>
		public void 파일복사(string 파일번호_참고, string 매입처, string 파일이름_표시, string 파일이름_실제) => 라인데이터테이블추가(파일번호_참고, 매입처, 파일이름_표시, 파일이름_실제);

		private void 라인데이터테이블추가(string 파일번호_참고, string 매입처, string 파일이름_표시, string 파일이름_실제)
		{
			DataRow 신규행			= dtLine.NewRow();
			신규행["NO_KEY"]			= 파일번호;
			신규행["TP_STEP"]		= 단계;
			신규행["NO_LINE"]		= 1;
			신규행["NO_KEY_REF"]		= 파일번호_참고;
			신규행["CD_SUPPLIER"]	= 매입처;
			신규행["NM_FILE"]		= 파일이름_표시;
			신규행["NM_FILE_REAL"]	= 파일이름_실제;
			신규행["YN_INCLUDED"]	= "N";
			dtLine.Rows.Add(신규행);
		}


		public void 저장()
		{
			디비.실행("PX_CZ_MA_WORKFLOW", dtHead.Json(), dtLine.Json());
		}




		
		/// <summary>
		/// 경로까지 포함한 파일 리턴
		/// </summary>
		public static string 다운로드(string 파일번호, string 단계, bool 실행여부)
		{
			// 첨부파일 가져오기
			string query = "SELECT TOP 1 * FROM CZ_MA_WORKFLOWL WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_KEY = '" + 파일번호 + "' AND TP_STEP = '" + 단계 + "' AND YN_INCLUDED = 'N' ORDER BY NO_LINE DESC";
			DataTable dt = 디비.결과(query);
			if (!dt.존재()) return "";

			// 다운로드 파일 이름 결정
			string 파일이름_신규 = 파일.파일이름_고유(경로.임시() + @"\" + dt.첫행("NM_FILE"));
			string 로컬파일 = 경로.임시() + @"\" + 파일이름_신규;

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(상수.호스트URL + "/" + 경로.워크플로우(파일번호) + "/" + Uri.EscapeDataString(dt.첫행("NM_FILE_REAL").문자()), 로컬파일);

			// 파일 실행
			if (실행여부) 파일.실행(로컬파일);
			return 로컬파일;
		}
	}
}
