using Duzon.Common.Forms;
using Duzon.Windows.Print;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Dintec
{
	public class ReportViewer
	{
		string CompanyCode;
		string ReportCode;
		DataTable dtH;
		DataTable dtL;
		ReportHelper report;

		public ReportViewer(string reportCode, DataTable dtH, DataTable dtL)
		{
			this.CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;

			if (CompanyCode == "TEST")
			{
				ReportCode = reportCode;
			}
			else
			{
				if (reportCode.IndexOf(CompanyCode) > 0)
					ReportCode = reportCode;
				else
					ReportCode = reportCode + "_" + CompanyCode;
			}

			this.dtH = dtH;
			this.dtL = dtL.Copy();

			Initialize();
			BindDocument();
			BindItems();


		}

		public ReportViewer(string companyCode, string reportCode, DataTable dtH, DataTable dtL)
		{
			this.CompanyCode = companyCode;
			this.ReportCode = reportCode + ((companyCode == "TEST") ? "" : "_" + companyCode);
			this.dtH = dtH;
			this.dtL = dtL.Copy();

			Initialize();
			BindDocument();
			BindItems();
		}



		private void Initialize()
		{
			// 리포트헬퍼 클래스 초기화 (더존 전용 클래스)
			report = new ReportHelper(ReportCode, "");
			report.PrintHelper.SetInit(PrintDialogOptionType.NONE, 0);

			// DB에서 리포트 가져오기
			string query = @"
SELECT
	  ID_OBJECT
	, NM_OBJECT
	, NM_OBJECT_E
	, TP_REPORT
FROM MA_REPORTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_SYSTEM = '" + ReportCode + @"'	
	AND YN_CHOOSE = 'Y'";

			// 리포트 팝업에서 "인쇄형태" 콤보 바인딩
			foreach (DataRow row in DBMgr.GetDataTable(query).Rows)
			{
				string contents = "";
				string fileName = "";

				if (Global.MainFrame.LoginInfo.Language == "KR")
					contents = row["NM_OBJECT"].ToString();
				else
					contents = row["NM_OBJECT_E"].ToString();

				if (row["TP_REPORT"].ToString() == "0")
					fileName = row["ID_OBJECT"] + ".RDF";
				else
					fileName = row["ID_OBJECT"] + ".DRF";

				report.PrintHelper.AddContentsString(contents, fileName);
			}
		}

		private void BindDocument()
		{
			// 지역코드 설정, 없는 경우 "한국"으로 취급
			string areaCode;

			if (!dtH.Columns.Contains("CD_AREA"))
				areaCode = "100";
			else
				areaCode = dtH.Rows[0]["CD_AREA"].ToString();

			// 기본 파라미터
			string[] parameter = {
							  "NM_COMPANY"          , "DC_ADS_COMPANY"  , "NM_CEO"								// 회사명, 주소, 대표자
							, "NO_TEL_COMPANY"      , "NO_FAX_COMPANY"  , "NM_EMAIL_COMPANY"					// 전화번호, 이메일
							, "DC_ADS_LOG"          , "NO_TEL_LOG"      , "NO_FAX_LOG"							// 물류센터 정보
							, "NM_EMP"              , "SIGNATURE"												// 담당자명, 사인
							, "NO_TEL_EMP"          , "NO_TEL_EMER_EMP" , "NO_FAX_EMP"      , "NO_EMAIL_EMP"	// 담당자 전화번호, 이메일
							, "NM_EMP_DEPT"         , "SIGNATURE_DEPT"											// 부서장명, 사인
							, "LN_PARTNER"          , "DC_ADS_H_PARTNER", "DC_ADS_D_PARTNER"					// 거래처, 주소
							, "CD_NATION"           , "NM_PIC"													// 거래처국가, 담당자
							, "NO_TEL_PARTNER"      , "NO_FAX_PARTNER"  , "E_MAIL_PARTNER"						// 거래처 전화번호, 이메일
							, "NO_COMPANY_PARTNER"  , "NM_CEO_PARTNER"  , "TP_JOB_PARTNER"  , "CLS_JOB_PARTNER"	// 거래처 사업자번호, 대표자, 업종, 업태
							, "NO_IMO"              , "NO_HULL"         , "NM_VESSEL"       , "NM_SHIP_YARD",   "DC_SHIPBUILDER"	// 호선, 조선소
							, "NO_FILE"             , "NO_SO"           , "NO_PO"								// 파일, 수주, 발주 번호
							, "NO_REF"              , "NO_PO_PARTNER"   , "NO_ORDER"							// REF 번호	
							, "NM_EXCH"             , "RT_EXCH"													// 통화명, 환율
							, "DT_INQ"              , "DT_QTN"          , "DT_SO"           , "DT_PO"			// 날짜 정보
							, "DC_RMK"              , "DC_RMK_INQ"      , "DC_RMK_QTN"      , "DC_RMK_TEXT" };  // 비고

			foreach (string s in parameter)
			{

				if (s == "DC_SHIPBUILDER")
				{

				}

				if (dtH.Columns.Contains(s))
				{
					// 날짜형식
					if (s.Substring(0, 3) == "DT_")
					{
						report.SetData(s, GetTo.DatePrint(dtH.Rows[0][s]));
					}
					// 지역구분에 따라 다른 필드를 사용 (100:한국)
					else if (s == "NM_EMP")
					{
						if (areaCode == "100")
							report.SetData(s, dtH.Rows[0][s].ToString());
						else
							report.SetData(s, dtH.Rows[0]["EN_EMP"].ToString());
					}
					else if (s == "NM_EMP_DEPT")
					{
						if (areaCode == "100")
							report.SetData(s, dtH.Rows[0][s].ToString());
						else
							report.SetData(s, dtH.Rows[0]["EN_EMP_DEPT"].ToString());
					}
					else if (s == "NO_TEL_EMP" || s == "NO_TEL_EMER_EMP")
					{
						if (areaCode == "100" || CompanyCode == "S100")     //	딘텍싱가폴의 경우 어떤 경우든 지역번호 안붙임
							report.SetData(s, dtH.Rows[0][s].ToString());
						else
							report.SetData(s, GetTo.InternationalCall(dtH.Rows[0][s]));
					}
					// 기타
					else
					{
						report.SetData(s, dtH.Rows[0][s].ToString());
					}
				}
			}
		}

		private void BindItems()
		{
			// 리포트의 Description 부분 넓이 가져오기
			string query = @"
SELECT TOP 1
	*
FROM MA_REPORTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_SYSTEM = '" + ReportCode + @"'
	AND ISNULL(CD_FLAG, '') != ''";

			DataTable dt = DBMgr.GetDataTable(query);
			int descWidth = dt.Rows.Count == 1 ? GetTo.Int(dt.Rows[0]["CD_FLAG"]) : 220;

			// 라인 쪼개기 시작
			int subjectWidth = 570;
			Font font = new Font("맑은 고딕", 9);
			int cnt = dtL.Rows.Count;
			string subject = "";
			List<string> keyColumns;

			// TP_ROW가 없는 경우 기본값 세팅
			if (!dtL.Columns.Contains("TP_ROW"))
			{
				dtL.Columns.Add("TP_ROW");
				for (int i = 0; i < dtL.Rows.Count; i++) dtL.Rows[i]["TP_ROW"] = "I";
			}

			keyColumns = new List<string>();

			if (dtL.Columns.Contains("NO_FILE")) keyColumns.Add("NO_FILE");
			if (dtL.Columns.Contains("NO_SO")) keyColumns.Add("NO_SO");
			if (dtL.Columns.Contains("NO_PO")) keyColumns.Add("NO_PO");
			if (dtL.Columns.Contains("NO_GIR")) keyColumns.Add("NO_GIR");
			if (dtL.Columns.Contains("NO_IV")) keyColumns.Add("NO_IV");
			if (dtL.Columns.Contains("NO_IV2")) keyColumns.Add("NO_IV2");
			if (dtL.Columns.Contains("NO_KEY")) keyColumns.Add("NO_KEY");

			// 라인별 리마크 표시 여부 => NM_ITEM_PARTNER과 DC_RMK 합친다
			if (dtL.Columns.Contains("YN_DSP_RMK"))
			{
				foreach (DataRow row in dtL.Rows)
				{
					if (row["YN_DSP_RMK"].ToString() == "Y" && row["DC_RMK"].ToString() != "")
					{
						row["NM_ITEM_PARTNER"] = row["NM_ITEM_PARTNER"] + "\n(*OFFER:" + row["DC_RMK"] + ")";
					}
				}
			}

			// 시작
			for (int i = 0; i < cnt; i++)
			{
				// ========== 서브젝트 행 체크
				if (dtL.Columns.Contains("NM_SUBJECT") && GetTo.String(dtL.Rows[i]["NM_SUBJECT"]) != "")
				{
					if (dtL.Columns.Contains("NM_SUBJECT") && (subject != GetTo.String(dtL.Rows[i]["NM_SUBJECT"]) || CheckKeyColumns(keyColumns, i, dtL)))
					{
						// 행추가
						DataRow row = dtL.NewRow();

						foreach (string column in keyColumns)
						{
							row[column] = GetTo.String(dtL.Rows[i][column]);
						}

						row["NM_SUBJECT"] = GetTo.String(dtL.Rows[i]["NM_SUBJECT"]);
						row["NM_ITEM_PARTNER"] = GetTo.String(dtL.Rows[i]["NM_SUBJECT"]);
						dtL.Rows.InsertAt(row, i);

						// 서브젝트 저장	및 루프카운트 +
						subject = GetTo.String(dtL.Rows[i]["NM_SUBJECT"]);
						cnt++;
					}
				}

				// ========== 길이 체크
				string s = dtL.Rows[i]["NM_ITEM_PARTNER"].ToString().Trim();
				int width = GetTo.String(dtL.Rows[i]["TP_ROW"]) == "" ? subjectWidth : descWidth;

				string[] flag = { " ", ":", "@", "," };
				int flag_idx = -1;
				int prev_idx = -1;

				do
				{
					bool rebuild = false;
					flag_idx = -1;                      // 플래그인덱스 초기화
					int enter_idx = s.IndexOf("\n");    // 엔터인덱스 선언
					int cut_idx = -1;                   // 커팅인덱스 선언

					// 플래그인덱스 저장
					foreach (string f in flag)
					{
						int idx = s.IndexOf(f, prev_idx + 1);

						if (idx > 0 && (flag_idx == -1 || idx < flag_idx))
							flag_idx = idx;
					}

					// 엔터인덱스가 존재하고 이전인덱스가 존재하는데 엔터인덱스까지 길이가 넘어가는 경우
					if (enter_idx > 0 && prev_idx > 0 && enter_idx < flag_idx && TextRenderer.MeasureText(s.Substring(0, enter_idx), font).Width > width)
					{
						rebuild = true;
						cut_idx = prev_idx + 1;
					}
					// 엔터인덱스가 플래그인덱스보다 가까운 경우
					else if (enter_idx > 0 && (flag_idx == -1 || enter_idx < flag_idx))
					{
						rebuild = true;
						cut_idx = enter_idx + 1;
					}
					// 플래그인덱스가 존재하고 이전인덱스가 존재하고 표시길이보다 큰 경우
					else if (flag_idx > 0 && prev_idx > 0 && TextRenderer.MeasureText(s.Substring(0, flag_idx), font).Width > width)
					{
						rebuild = true;
						cut_idx = prev_idx + 1;
					}

					// 리빌드
					if (rebuild)
					{
						// 현재 ROW
						dtL.Rows[i]["NM_ITEM_PARTNER"] = s.Substring(0, cut_idx).Replace("\n", ""); // 엔터 제거

						// 신규 ROW 추가 및 루프카운트 + 
						DataRow row = dtL.NewRow();
						if (dtL.Columns.Contains("NO_FILE")) row["NO_FILE"] = GetTo.String(dtL.Rows[i]["NO_FILE"]);
						if (dtL.Columns.Contains("NO_SO")) row["NO_SO"] = GetTo.String(dtL.Rows[i]["NO_SO"]);
						if (dtL.Columns.Contains("NO_PO")) row["NO_PO"] = GetTo.String(dtL.Rows[i]["NO_PO"]);
						if (dtL.Columns.Contains("NO_GIR")) row["NO_GIR"] = GetTo.String(dtL.Rows[i]["NO_GIR"]);
						if (dtL.Columns.Contains("NO_IV")) row["NO_IV"] = GetTo.String(dtL.Rows[i]["NO_IV"]);
						if (dtL.Columns.Contains("NO_IV2")) row["NO_IV2"] = GetTo.String(dtL.Rows[i]["NO_IV2"]);
						if (dtL.Columns.Contains("NO_KEY")) row["NO_KEY"] = GetTo.String(dtL.Rows[i]["NO_KEY"]);
						if (dtL.Columns.Contains("NM_SUBJECT")) row["NM_SUBJECT"] = dtL.Rows[i]["NM_SUBJECT"];
						row["NM_ITEM_PARTNER"] = s.Substring(cut_idx);
						row["TP_ROW"] = dtL.Rows[i]["TP_ROW"].ToString() == "" ? "" : "E";
						dtL.Rows.InsertAt(row, i + 1);
						cnt++;
						break;
					}

					prev_idx = flag_idx;

				} while (flag_idx > 0);
			}

			// 바인딩
			report.SetDataTable(dtL);
		}

		private bool CheckKeyColumns(List<string> columns, int index, DataTable dt)
		{
			foreach (string column in columns)
			{
				if (dt.Rows[index - 1][column].ToString() != dt.Rows[index][column].ToString())
					return true;
			}

			return false;
		}

		public void SetData(string name, string value)
		{
			report.SetData(name, value);
		}

		public void SetData(string name, object value)
		{
			report.SetData(name, value.ToString());
		}

		public void Print()
		{
			Language lang = Global.CurLanguage;

			Global.CurLanguage = Language.KR;
			report.PrintHelper.ShowPrintDialog();
			Global.CurLanguage = lang;
		}

		public string ConvertPdf(string fileName)
		{
			// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
			if (fileName.IndexOf(@"\") < 0)
				fileName = Application.StartupPath + @"\temp\" + fileName;

			// 디렉토리 생성
			Directory.CreateDirectory(fileName.Substring(0, fileName.LastIndexOf(@"\")));

			// PDF 저장

			report.PrintDirect(ReportCode + ".drf", false, true, fileName, new Dictionary<string, string>());

			return fileName;
		}
	}
}
