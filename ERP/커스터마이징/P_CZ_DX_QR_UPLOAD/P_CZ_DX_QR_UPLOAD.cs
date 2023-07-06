using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

using Dass.FlexGrid;
using Duzon.Common.Forms;
using DX;

namespace cz
{
	public partial class P_CZ_DX_QR_UPLOAD : PageBase
	{
		private string 문서종류 => cbo문서종류.값();

		#region ==================================================================================================== 생성자

		public P_CZ_DX_QR_UPLOAD()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== 초기화

		protected override void InitLoad()
		{
			this.페이지초기화();

			// 콤보박스
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("문서종류", "적재허가서", DD("적재허가서"));
			dt.Rows.Add("문서종류", "인수증"	 , DD("인수증"));
			cbo문서종류.바인딩(dt.Select("TYPE = '문서종류'"), true);

			// 메서드
			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			//spc헤드.SplitterDistance = 850;
		}

		private void 클리어()
		{
			tbx로그.초기화();
			grd라인.초기화();
			grd최종.초기화();
		}

		#endregion

		#region ==================================================================================================== 그리드

		private void InitGrid()
		{
			MainGrids = new FlexGrid[] { grd최종 };

			// ********** 라인
			grd라인.세팅시작(2);

			grd라인.컬럼세팅("NM_FILE"	, "파일이름"		, false);
			grd라인.컬럼세팅("DOC_TYPE"	, "문서종류"		, 100	, 정렬.가운데);
			grd라인.컬럼세팅("QR"			, "QR"			, 70	, 정렬.가운데);
			grd라인.컬럼세팅("DATA"		, "DATA"		, 300);
			grd라인.컬럼세팅("CD_COMPANY"	, "회사"			, 90	, 정렬.가운데);
			grd라인.컬럼세팅("NO_GIR"		, "협조전번호"	, 300);
			grd라인.컬럼세팅("NO_FILE"	, "파일번호"		, 300);
			
			grd라인.컬럼세팅("PAGE_CHK_PB", "페이지"		, "발행"	, 85	, 정렬.가운데);	// PUBLISH
			grd라인.컬럼세팅("PAGE_CHK_RC", "페이지"		, "접수"	, 85	, 정렬.가운데);	// RECEIVE
			//grd라인.컬럼세팅("FILE_CHK_PB", "파일(발행)"	, 85	, 정렬.가운데);  // PUBLISH
			//grd라인.컬럼세팅("FILE_CHK_RC", "파일(접수)"	, 85	, 정렬.가운데);	// RECEIVE
			
			grd라인.컬럼세팅("PAGE_ST"	, "페이지"		, "시작"	, 85	, 정렬.가운데);
			grd라인.컬럼세팅("PAGE_ED"	, "페이지"		, "종료"	, 85	, 정렬.가운데);

			

			grd라인.데이터맵("CD_COMPANY", 코드.회사());
			grd라인.세팅종료("22.05.26.02", false, true);

			// ********** 최종
			grd최종.세팅시작(1);
			   
			grd최종.컬럼세팅("DOC_TYPE"	, "문서종류"		, false);
			grd최종.컬럼세팅("CD_COMPANY"	, "회사"			, 80	, 정렬.가운데);
			grd최종.컬럼세팅("NO_GIR"		, "협조전번호"	, 130	, 정렬.가운데);
			grd최종.컬럼세팅("STA_GIR"	, "진행상태"		, 70	, 정렬.가운데);
			grd최종.컬럼세팅("DT_IO"		, "출고일자"		, 80	, 포맷.날짜);
			grd최종.컬럼세팅("DC_FILE"	, "파일명"		, 200);
			grd최종.컬럼세팅("CNT_RECEIPT", "인수증 수"		, 85	, 정렬.가운데);
			grd최종.컬럼세팅("PAGE_ST"	, "페이지(시작)"	, 85	, 정렬.가운데);
			grd최종.컬럼세팅("PAGE_ED"	, "페이지(종료)"	, 85	, 정렬.가운데);
			grd최종.컬럼세팅("PAGE_CHK_PB", "페이지(발행)"	, 85	, 정렬.가운데);	// PUBLISH
			grd최종.컬럼세팅("PAGE_CHK_RC", "페이지(접수)"	, 85	, 정렬.가운데);	// RECEIVE
			grd최종.컬럼세팅("FILE_CHK_PB", "파일(발행)"	, 85	, 정렬.가운데);	// PUBLISH
			grd최종.컬럼세팅("FILE_CHK_RC", "파일(접수)"	, 85	, 정렬.가운데);	// RECEIVE			   
			grd최종.컬럼세팅("SEQ"		, "순번"			, 80	, 정렬.가운데);
			grd최종.컬럼세팅("YN_SEQ"		, "등록여부"		, 80	, 정렬.가운데);
			grd최종.컬럼세팅("ERROR"		, "오류"			, 400);
			grd최종.컬럼세팅("NM_FILE"	, "파일명"		, false);
			grd최종.컬럼세팅("CD_FILE"	, "파일코드"		, false);

			grd최종.데이터맵("CD_COMPANY", 코드.회사());
			grd최종.데이터맵("STA_GIR", 코드.코드관리("CZ_SA00030"));
			grd최종.세팅종료("21.12.24.01", false, true);
		}

		#endregion

		#region ==================================================================================================== 이벤트

		private void InitEvent()
		{			
			btn자동업로드.Click += Btn자동업로드_Click;
			btn업로드_단일문서.Click += Btn업로드_단일문서_Click;

			cbo문서종류.SelectionChangeCommitted += Cbo문서종류_SelectionChangeCommitted;
		}

		private void Btn자동업로드_Click(object sender, EventArgs e)
		{
			cbo문서종류.값("인수증");

			// ********** 인수증 메일
			파일선택 fs = new 파일선택();
			fs.다중선택 = true;
			fs.파일유형 = 파일유형.메일;
			fs.열기();

			foreach (string s in fs.파일이름s)
			{
				아웃룩 msg = new 아웃룩(s);

				// 발신자 체크
				if (msg.발신자.발생("receipt@dintec.co.kr"))
					continue;

				// 수신자 체크




				// 도움 정보 가져오기
				DataTable dt = 디비.결과("SELECT * FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = 'K100' AND CD_FIELD = 'CZ_DX00021' AND CD_FLAG1 = '" + 문서종류 + "' AND CD_FLAG2 = '" + msg.발신자.분할("@")[1] + "'");
				string 단일혼합 = dt.첫행("CD_FLAG3").문자();
				string 단수복수 = dt.첫행("CD_FLAG4").문자();

				//return;

				// 첨부파일 체크
				foreach (아웃룩첨부파일 file in msg.첨부파일)
				{

					if (file.확장자.소문자() == "pdf")
					{
						string 파일이름 = 상수.저장경로 + file.이름;
						file.저장(파일이름);
						
						tbx로그.Text = "";
						grd라인.초기화();
						grd최종.초기화();

						인수증(파일이름, 단일혼합, 단수복수, true);
					}
				}

				
			}



		}



		

		private void Btn업로드_단일문서_Click(object sender, EventArgs e)
		{
			// 파일선택
			파일선택 fs = new 파일선택();
			fs.다중선택 = true;
			fs.파일유형 = 파일유형.Pdf;
			if (!fs.열기()) return;

			유틸.작업중("분석 중입니다.");
			tbx로그.Text = "";
			grd라인.클리어();
			grd최종.클리어();

			try
			{
				grd라인.그리기중지();
				grd최종.그리기중지();

				foreach (string 파일이름 in fs.파일이름s)
				{
					QR qr = new QR();
					qr.읽기유형.QR코드 = chkQR코드.Checked;
					qr.읽기유형.데이터매트릭스 = chk데이터매트릭스.Checked;
					qr.읽기유형.바코드 = chk바코드.Checked;
					qr.파일이름 = 파일이름;
					qr.읽기();

					// ********** 페이지 당 한개의 QR만 가져오기
					List<QRDATA> qrdata = new List<QRDATA>();
					
					for (int i = 0; i < qr.Count; i++)
					{
						if (qrdata.Count == 0 || qrdata.Last().페이지 != qr[i].페이지)
							qrdata.Add(qr[i]);
						else if (qrdata.Last().인코딩 != qr[i].인코딩)
							throw new Exception("QR data 오류");  // 페이지가 같을 경우 같은 페이지의 이미 있는 인코딩 값이랑 비교해서 다르면 에러
					}

					// 로그 찍어주고..
					tbx로그.Text += "\r\n" + "********** 시작" + "\r\n";
					tbx로그.Text += "[업로드]	" + 파일.파일이름(파일이름) + "\r\n";
					tbx로그.Text += "[페이지 수]	" + PDF.페이지수(파일이름) + "\r\n";
					tbx로그.Text += "[읽은 QR 수]	" + qr.Count + "\r\n";

					// ********** QR 바인딩
					for (int i = 0; i < qrdata.Count; i++)
					{
						DataTable dtCode = TSQL.실행<DataTable>("PS_CZ_DX_BASE62", qrdata[i].인코딩);

						// 딘텍 쇼트너인지 확인
						if (dtCode.Rows.Count == 0)
						{
							tbx로그.Text += " =====> 스킵 : 인식 불가" + "\r\n";
							continue;
						}

						// 해당 문서종류인지 확인
						if (dtCode.Select("TYPE = '" + 문서종류 + "'").Length == 0)
						{
							tbx로그.Text +=  " =====> 스킵 : " + dtCode.Rows[0]["TYPE"] + "\r\n";
							continue;
						}

						// ********** 쇼트너에서 데이터 가져오기
						string data = dtCode.Rows[0]["DATA"].문자();
						string[] code = data.분할("/");

						grd라인.행추가();
						grd라인["NM_FILE"]		= 파일이름;
						grd라인["DOC_TYPE"]		= 문서종류;
						grd라인["QR"]			= qrdata[i].인코딩;
						grd라인["DATA"]			= data;
						//grd라인["CD_COMPANY"]	= code[0];
						grd라인["CD_COMPANY"]	= code[1].왼쪽(2) == "DO" ? "K200" : code[0];
						grd라인["NO_GIR"]		= code[1];
						grd라인["NO_FILE"]		= code.Length > 2 ? code[2] : "";
						grd라인["PAGE_ST"]		= qrdata[i].페이지;				
						grd라인["PAGE_ED"]		= i < qrdata.Count - 1 ? qrdata[i + 1].페이지 - 1 : PDF.페이지수(파일이름);
						grd라인.행추가완료();						
					}
				}

				적재허가서();
				오류체크();
				grd라인.그리기시작();
				grd최종.그리기시작();
				유틸.작업중();
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Cbo문서종류_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (문서종류 == "적재허가서")
			{
				chkQR코드.Checked = false;
				chk데이터매트릭스.Checked = true;
				chk바코드.Checked = false;
			}
		}

		private void 인수증(string 파일이름, string 단일혼합, string 단수복수, bool 오류중지)
		{
			// ********** QR코드 읽기
			QR qr = new QR();
			qr.파일이름 = 파일이름;
			qr.쇼트너 = true;
			qr.읽기유형.QR코드 = true;
			qr.읽기유형.데이터매트릭스 = true;
			qr.읽기유형.바코드 = true;
			qr.읽기();

			// ********** 로그 찍어주고..
			tbx로그.Text += "\r\n" + "********** 시작" + "\r\n";
			tbx로그.Text += "[업로드]	" + 파일.파일이름(파일이름) + "\r\n";
			tbx로그.Text += "[페이지 수]	" + PDF.페이지수(파일이름) + "\r\n";
			tbx로그.Text += "[읽은 QR 수]	" + qr.Count + "\r\n";
			tbx로그.Text += "[단일 or 혼합]	" + 단일혼합 + "\r\n";
			tbx로그.Text += "[단수 or 복수]	" + 단수복수 + "\r\n";

			// ********** QR 바인딩
			for (int i = 0; i < qr.Count; i++)
			{
				tbx로그.Text += string.Format("{0:00}", i + 1) + "번 QR : " + qr + "\r\n";
				DataTable dtCode = 디비.결과("PS_CZ_DX_BASE62", qr[i].값);

				// 딘텍 쇼트너인지 확인
				if (!dtCode.존재())
				{
					//if (오류중지)
					//{
					//	tbx로그.Text += " =====> 실패 : 인식 불가" + "\r\n";
					//	throw new Exception("QR 인식 불가");
					//}
					//else
					//{
						tbx로그.Text += " =====> 스킵 : 인식 불가" + "\r\n";
						continue;
					//}
				}

				// 해당 문서종류인지 확인
				if (dtCode.선택("TYPE = '" + 문서종류 + "'").존재())
				{
					// ********** 쇼트너에서 데이터 가져오기
					string data = dtCode.첫행("DATA").문자();
					string[] code = data.분할("/");
					DataRow row = 디비.결과("PS_CZ_DX_QR_UPLOAD_CHK_GIR", code[0], code[1], 문서종류).첫행();

					grd라인.행추가();
					grd라인["NM_FILE"]		= 파일이름;
					grd라인["DOC_TYPE"]		= 문서종류;
					grd라인["QR"]			= qr[i].값;
					grd라인["DATA"]			= data;
					grd라인["CD_COMPANY"]	= code[0];
					grd라인["NO_GIR"]		= code[1];
					grd라인["NO_FILE"]		= code[2];
					grd라인["PAGE_ST"]		= qr[i].페이지;
					grd라인["PAGE_ED"]		= i < qr.Count - 1 ? qr[i + 1].페이지 - 1 : PDF.페이지수(파일이름);
					grd라인.행추가완료();


					//grd라인["PAGE_CHK_PB"]	= row[""]
					//grd라인["PAGE_CHK_RC"]

					//


					//grd최종.행추가();
					//grd최종["DOC_TYPE"] = 문서종류;
					//grd최종["CD_COMPANY"] = 회사코드;
					//grd최종["NO_GIR"] = 협조전;
					//grd최종["STA_GIR"] = row["진행상태"];
					//grd최종["DT_IO"] = row["출고일자"];
					//grd최종["PAGE_ST"] = grd라인[i, "PAGE_ST"];
					//grd최종["PAGE_ED"] = grd라인[i, "PAGE_ED"];
					//grd최종["YN_SEQ"] = row["등록여부"];
					//grd최종["DC_FILE"] = 파일.파일이름(새것);
					//grd최종["NM_FILE"] = 새것;
					//grd최종["CD_FILE"] = 협조전 + "_" + 회사코드 + "_ETC";
					//grd최종.행추가완료();
				}

				

			}

			// ********** QR 바인딩 완료 됐으면 적합성 체크
			
			// 단일문서 & 
			
			// 혼합문서 & 단일건수
			int a = grd라인.데이터테이블().AsEnumerable().Select(x => x["CD_COMPANY"].문자() + x["NO_GIR"].문자()).Distinct().Count();



		}

		private void 적재허가서()
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				// pdf 파일 추출
				string 회사코드 = grd라인[i, "CD_COMPANY"].문자();
				string[] 협조전s = grd라인[i, "NO_GIR"].문자().분할(",");

				string 원본 = grd라인[i, "NM_FILE"].문자();
				string 새것 = 파일.경로만(원본) + @"\" + 협조전s[0] + "_" + 회사코드 + ".pdf";								
				PDF.추출(원본, 새것, grd라인[i, "PAGE_ST"].정수(), grd라인[i, "PAGE_ED"].정수());

				// 협조전 수만큼 추가
				foreach (string 협조전 in 협조전s)
				{
					DataRow row = TSQL.실행<DataTable>("PS_CZ_DX_QR_UPLOAD_CHK_GIR", 회사코드, 협조전, 문서종류).Rows[0];

					grd최종.행추가();
					grd최종["DOC_TYPE"]		= 문서종류;
					grd최종["CD_COMPANY"]	= 회사코드;
					grd최종["NO_GIR"]		= 협조전;
					grd최종["STA_GIR"]		= row["진행상태"];
					grd최종["DT_IO"]			= row["출고일자"];
					grd최종["PAGE_ST"]		= grd라인[i, "PAGE_ST"];
					grd최종["PAGE_ED"]		= grd라인[i, "PAGE_ED"];
					grd최종["YN_SEQ"]		= row["등록여부"];
					grd최종["DC_FILE"]		= 파일.파일이름(새것);
					grd최종["NM_FILE"]		= 새것;
					grd최종["CD_FILE"]		= 협조전 + "_" + 회사코드 + "_ETC";
					grd최종.행추가완료();
				}
			}
		}

		private void 오류체크()
		{
			for (int i = grd최종.Rows.Fixed; i < grd최종.Rows.Count; i++)
			{
				if (grd최종[i, "STA_GIR"].ToString() != "C")
					grd최종[i, "ERROR"] += "종결된 협조전이 아닙니다.";

				if (grd최종[i, "PAGE_CHK_PB"].정수() != grd최종[i, "PAGE_CHK_RC"].정수())
					grd최종[i, "ERROR"] += "발행 페이지와 접수 페이지가 다릅니다.";

				if (grd최종[i, "FILE_CHK_PB"].정수() != grd최종[i, "FILE_CHK_RC"].정수())
					grd최종[i, "ERROR"] += "발행 파일과 접수 파일이 다릅니다.";

				if (grd최종[i, "YN_SEQ"].ToString() != "")
					grd최종[i, "ERROR"] += "이미 접수된 " + grd최종[i, "DOC_TYPE"] + "입니다.";
			}
		}

		#endregion

		#region ==================================================================================================== 저장

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{			
			base.OnToolBarSaveButtonClicked(sender, e);

			try
			{
				// ********** 유효성 검사
				if (문서종류 == "인수증")
				{
					if (dtp선적일자.Text == "")
					{
						if (상수.사원번호.포함("S-343", "SYSADMIN"))
						{
							// RPA의 경우 선적일자가 없는 경우 출고일자를 인식하게 해줌
						}
						else
							유틸.경고발생("선적일자를 입력하세요");
					}

					if (dtp선적일자.Value > DateTime.Now)	
						유틸.경고발생("선적일자가 잘못되었습니다.");
				}

				if (grd최종.데이터테이블("ERROR != ''").Rows.Count > 0)
					유틸.경고발생("오류 항목이 있습니다.");
				
				// ********** 저장
				for (int i = grd최종.Rows.Fixed; i < grd최종.Rows.Count; i++)
				{
					// 순번 가져오기
					string 회사코드 = grd최종[i, "CD_COMPANY"].문자();
					string 파일코드 = grd최종[i, "CD_FILE"].문자();
					string 협조전번호 = grd최종[i, "NO_GIR"].문자();
					string query = @"
	SELECT
		ISNULL(MAX(NO_SEQ), 0) + 1
	FROM MA_FILEINFO WITH(NOLOCK)
	WHERE 1 = 1
		AND CD_COMPANY = '" + 회사코드 + @"'
		AND CD_MODULE = 'SA'
		AND ID_MENU = 'P_CZ_SA_GIM_REG'
		AND CD_FILE = '" + 파일코드 + @"'

	SELECT
		DT_GIR
	FROM SA_GIRH WITH(NOLOCK)
	WHERE 1 = 1
		AND CD_COMPANY = '" + 회사코드 + @"'
		AND NO_GIR = '" + 협조전번호 + @"'";

					DataSet ds = TSQL.실행<DataSet>(query);
					int 순번 = ds.Tables[0].Rows[0][0].정수();
					string 년 = ds.Tables[1].Rows[0][0].ToString().Substring(0, 4);
					string 경로 = "UPLOAD/P_CZ_SA_GIM_REG" + "/" + 회사코드 + "/" + 년 + "/" + 파일코드;
					파일 파일 = new 파일(grd최종[i, "NM_FILE"].문자());

					// 파일 업로드
					파일.업로드(경로);

					// DB 인서트
					TSQL sql = new TSQL("UP_MA_FILEINFO_INSERT_NEW");
					sql.변수.추가("@P_CD_COMPANY"	, 회사코드);
					sql.변수.추가("@P_CD_MODULE"	, "SA");
					sql.변수.추가("@P_ID_MENU"	, "P_CZ_SA_GIM_REG");
					sql.변수.추가("@P_CD_FILE"	, 파일코드);
					sql.변수.추가("@P_NO_SEQ"		, 순번);
					sql.변수.추가("@P_FILE_NAME"	, 파일.이름);
					sql.변수.추가("@P_FILE_PATH"	, 경로);
					sql.변수.추가("@P_FILE_EXT"	, 파일.확장자_점제외());
					sql.변수.추가("@P_FILE_DATE"	, 파일.수정한날짜().문자("yyyyMMdd"));
					sql.변수.추가("@P_FILE_TIME"	, 파일.수정한날짜().문자("HHmm"));
					sql.변수.추가("@P_FILE_SIZE"	, string.Format("{0:0.00}M", 파일.크기() / 1048576.0));
					sql.변수.추가("@P_FILE_BYTE"	, 파일.크기());
					sql.변수.추가("@P_DC_RMK"		, "QR업로드-" + grd최종[i, "DOC_TYPE"]);
					sql.변수.추가("@P_FG_FILE"	, DBNull.Value);
					sql.변수.추가("@P_ID_INSERT"	, 상수.사원번호);
					sql.실행();

					// 선적일자 업데이트
					if (문서종류 == "인수증")
					{
						string 선적일자 = dtp선적일자.Text != "" ? dtp선적일자.Text : grd최종[i, "DT_IO"].문자();
						query = @"
UPDATE CZ_SA_GIRH_WORK_DETAIL SET
	DT_LOADING = '" + 선적일자 + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + 회사코드 + @"'
	AND NO_GIR = '" + 협조전번호 + @"'";

						TSQL.실행(query);
					}
				}

				클리어();
				유틸.메세지(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		#endregion
	}
}

