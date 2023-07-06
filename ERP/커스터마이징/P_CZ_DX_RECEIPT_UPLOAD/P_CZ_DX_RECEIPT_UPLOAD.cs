using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.IO;
using Duzon.Common.Controls;
using DX;
using System.Linq;
using System.Diagnostics;

namespace cz
{
	public partial class P_CZ_DX_RECEIPT_UPLOAD : PageBase
	{
		FlexGrid flexGrid;

		public P_CZ_DX_RECEIPT_UPLOAD()
		{
			InitializeComponent();
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{			
			MainGrids = new FlexGrid[] { grd라인, grd상세 };

			InitGrid(grd라인);
			InitGrid(grd상세);
			InitEvent();

			DataTable dt = new DataTable();
			dt.Columns.Add("CD_COMPANY"	, typeof(string));
			dt.Columns.Add("NO_GIR"		, typeof(string));
			dt.Columns.Add("STA_GIR"	, typeof(string));
			dt.Columns.Add("DT_IO"		, typeof(string));
			dt.Columns.Add("CNT_RECEIPT", typeof(int));			
			dt.Columns.Add("PAGE_ST"	, typeof(int));
			dt.Columns.Add("PAGE_ED"	, typeof(int));
			dt.Columns.Add("PAGE_CHK_PB", typeof(int));
			dt.Columns.Add("PAGE_CHK_RC", typeof(int));
			dt.Columns.Add("FILE_CHK_PB", typeof(int));
			dt.Columns.Add("FILE_CHK_RC", typeof(int));
			dt.Columns.Add("YN_SEQ"		, typeof(string));
			dt.Columns.Add("ERROR"		, typeof(string));
			dt.Columns.Add("DC_FILE"	, typeof(string));
			dt.Columns.Add("NM_FILE"	, typeof(string));
			dt.Columns.Add("CD_FILE"	, typeof(string));

			grd라인.Binding = dt.Clone();
			grd상세.Binding = dt.Clone();
		}

		protected override void InitPaint()
		{
			
		}

		private void Clear()
		{
			tbx로그.Text = "";
			grd라인.Clear2();
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid(FlexGrid flexGrid)
		{			
			flexGrid.BeginSetting(1, 1, false);
			
			flexGrid.SetCol("CD_COMPANY"	, "회사"			, 130	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NO_GIR"		, "협조전번호"	, 130	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("STA_GIR"		, "진행상태"		, 70	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("DT_IO"			, "출고일자"		, 80	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexGrid.SetCol("DC_FILE"		, "파일명"		, 200);
			flexGrid.SetCol("CNT_RECEIPT"	, "인수증 수"		, 85	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("PAGE_ST"		, "페이지(시작)"	, 85	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("PAGE_ED"		, "페이지(종료)"	, 85	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("PAGE_CHK_PB"	, "페이지(발행)"	, 85	, TextAlignEnum.CenterCenter);  // PUBLISH
			flexGrid.SetCol("PAGE_CHK_RC"	, "페이지(접수)"	, 85	, TextAlignEnum.CenterCenter);	// RECEIVE
			flexGrid.SetCol("FILE_CHK_PB"	, "파일(발행)"	, 85	, TextAlignEnum.CenterCenter);  // PUBLISH
			flexGrid.SetCol("FILE_CHK_RC"	, "파일(접수)"	, 85	, TextAlignEnum.CenterCenter);	// RECEIVE
			
			flexGrid.SetCol("SEQ"			, "순번"			, 80	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("YN_SEQ"		, "등록여부"		, 80	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("ERROR"			, "오류"			, 400);			
			flexGrid.SetCol("NM_FILE"		, "파일명"		, false);
			flexGrid.SetCol("CD_FILE"		, "파일코드"		, false);
			
			flexGrid.SetDataMap("STA_GIR", BASE.Code("CZ_SA00030"), "CODE", "NAME");
			
			flexGrid.SetDefault("21.08.21.01", SumPositionEnum.None);
			flexGrid.SetAlternateRow();
			flexGrid.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			//btn테스트.Click += Btn테스트_Click;
			//btn테스트2.Click += Btn테스트2_Click;

			btn업로드_혼합문서.Click += Btn업로드_혼합문서_Click;
			btn업로드_인수증만.Click += Btn업로드_인수증만_Click;
			btn삭제.Click += Btn삭제_Click;

			grd라인.GotFocus += Grd라인_GotFocus;
			grd상세.GotFocus += Grd라인_GotFocus;

			grd라인.DoubleClick += Grd라인_DoubleClick;
			grd상세.DoubleClick += Grd라인_DoubleClick;
		}

		private void Btn테스트2_Click(object sender, EventArgs e)
		{
			
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			PDF.추출(@"d:\인수증\WO21081508.pdf", @"d:\인수증\test.pdf", 4, 5);
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{			
			if (flexGrid.HasNormalRow)
				flexGrid.Rows.Remove(flexGrid.Row);			
		}

		private void Grd라인_GotFocus(object sender, EventArgs e)
		{
			flexGrid = (FlexGrid)sender;
		}

		private void Grd라인_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			// 첨부파일 열기
			string colName = flexGrid.Cols[flexGrid.Col].Name;

			if (colName == "DC_FILE")
			{
				Process.Start(flexGrid["NM_FILE"].ToString());
			}
		}

		#endregion

		#region ==================================================================================================== Search

		private void Btn업로드_혼합문서_Click(object sender, EventArgs e)
		{
			OpenFileDialog f = new OpenFileDialog() { Filter = DD("Pdf 파일") + "|*.pdf;", Multiselect = true };

			if (f.ShowDialog() != DialogResult.OK)
				return;

			UTIL.작업중(DD("분석 중 입니다."));
			tbx로그.Text = "";
			grd라인.Clear2();
			grd상세.Clear2();

			try
			{
				foreach (string 파일이름 in f.FileNames)
				{
					// QR 읽기
					QR qr = new QR();
					qr.파일이름 = 파일이름;
					qr.읽기유형.QR코드 = chkQR.Checked;
					qr.읽기유형.데이터매트릭스 = chk데이터매트릭스.Checked;
					qr.읽기유형.바코드 = chk바코드.Checked;
					qr.읽기();

					// ********** 페이지 당 한개의 쇼트너만 가져오기
					List<QRDATA> 쇼트너리스트 = new List<QRDATA>();
			
					foreach (QRDATA q in qr.Items)
					{
						string value = q.값;
						value = value.Replace("dint.ec/", "");
						value = value.Replace("http://dintec.kr/", "");

						if (쇼트너리스트.Count == 0 || 쇼트너리스트.Last().페이지 != q.페이지)
						{
							쇼트너리스트.Add(new QRDATA(q.페이지, value));
						}
						else
						{
							// 페이지가 같을 경우 같은 페이지의 이미 있는 쇼트너랑 비교해서 다르면 에러
							if (쇼트너리스트.Last().값 != value)
							{
								throw new Exception("QR data 오류");
							}
						}
					}

					tbx로그.Text += "\r\n" + "********** 시작" + "\r\n";
					tbx로그.Text += "[업로드]	" + qr.파일이름 + "\r\n";
					tbx로그.Text += "[페이지 수]	" + PDF.페이지수(qr.파일이름) + "\r\n";
					tbx로그.Text += "[읽은 QR 수]	" + qr.Count + "\r\n";

					// ********** 쇼트너 정보 바인딩
					DataTable dt = new DataTable();
					dt.Columns.Add("QR"			, typeof(string));
					dt.Columns.Add("그룹"		, typeof(string));
					dt.Columns.Add("회사코드"		, typeof(string));
					dt.Columns.Add("협조전번호"	, typeof(string));
					dt.Columns.Add("진행상태"		, typeof(string));
					dt.Columns.Add("출고일자"		, typeof(string));
					dt.Columns.Add("파일번호"		, typeof(string));
					dt.Columns.Add("페이지시작"	, typeof(int));
					dt.Columns.Add("페이지종료"	, typeof(int));
					dt.Columns.Add("페이지발행"	, typeof(int));
					dt.Columns.Add("페이지접수"	, typeof(int));	
					dt.Columns.Add("파일발행"		, typeof(int));
					dt.Columns.Add("파일접수"		, typeof(int));
					dt.Columns.Add("등록여부"		, typeof(string));

					// ********** QR에서 코드에서 필요한 정보 추출 (파일단위로 쪼개짐)
					foreach (QRDATA 쇼트너 in 쇼트너리스트)
					{
						// QR에서 인수증 데이터만 가져오기
						DataTable dtBase62 = SQL.GetDataTable("PS_CZ_DX_BASE62", 쇼트너.값);

						// 딘텍 쇼트너인지 확인
						if (dtBase62.Rows.Count == 0)
						{
							tbx로그.Text += " =====> 스킵 : 인식 불가" + "\r\n";
							continue;
						}

						// 인수증인지 확인
						if (dtBase62.Select("TYPE = '인수증'").Length == 0)
						{
							tbx로그.Text +=  " =====> 스킵 : " + dtBase62.Rows[0]["TYPE"] + "\r\n";
							continue;
						}

						// ********** 쇼트너에서 데이터 가져오기
						string data = dtBase62.Rows[0]["DATA"].ToString();
						DataRow newRow = dt.NewRow();
						newRow["QR"]		= 쇼트너.값;
						newRow["그룹"]		= data.Split('/')[0] + "/" + data.Split('/')[1];
						newRow["회사코드"]	= data.Split('/')[0];
						newRow["협조전번호"]	= data.Split('/')[1];
						newRow["파일번호"]	= data.Split('/')[2];
						newRow["페이지시작"]	= 쇼트너.페이지;
						dt.Rows.Add(newRow);
					}

					// ********** 그룹별 정보 바인딩
					foreach (string s in dt.AsEnumerable().Select(r => r["그룹"]).Distinct())
					{
						DataRow[] 그룹 = dt.Select("그룹 = '" + s + "'");
						DataRow 협조전 = SQL.GetDataTable("PS_CZ_DX_RECEIPT_UPLOAD_CHK", 그룹[0]["회사코드"], 그룹[0]["협조전번호"]).Rows[0];

						for (int i = 0; i < 그룹.Length; i++)
						{
							그룹[i]["진행상태"]	= 협조전["진행상태"];
							그룹[i]["출고일자"]	= 협조전["출고일자"];
							그룹[i]["페이지발행"]	= 협조전["페이지발행"];
							그룹[i]["페이지종료"] = i < 그룹.Length - 1 ? 그룹[i + 1]["페이지시작"].ToInt() - 1 : 그룹[0]["페이지시작"].ToInt() + 그룹[0]["페이지발행"].ToInt() - 1;
							그룹[i]["파일발행"]	= 협조전["파일발행"];
							그룹[i]["등록여부"]	= 협조전["등록여부"];
						}

						for (int i = 0; i < 그룹.Length; i++)
						{
							그룹[i]["페이지접수"] = 그룹.Sum(r => r.Field<int>("페이지종료") - r.Field<int>("페이지시작") + 1);
							그룹[i]["파일접수"] = 그룹.Length;
						}
					}

					// ********** 오류 확인
					if (dt.Select("페이지발행 <> 페이지접수 OR 파일발행 <> 파일접수").Length > 0)
					{
						throw new Exception("오류");
					}

					// ********** PDF 추출
					foreach (DataRow row in dt.Rows)
					{
						string path = PATH.GetPath(qr.파일이름) + @"\";
						string pdf = path + row["협조전번호"] + "_" + row["파일번호"] + "_" + row["회사코드"] + ".pdf";
						PDF.추출(qr.파일이름, pdf, (int)row["페이지시작"], (int)row["페이지종료"]);

						tbx로그.Text += "인수증 분할 : " + Dintec.FILE.GetFileName(pdf) + "\r\n";

						// 그리드 추가
						grd상세.Rows.Add();
						grd상세.Row = grd상세.Rows.Count - 1;
						grd상세["CD_COMPANY"]	= row["회사코드"];
						grd상세["NO_GIR"]		= row["협조전번호"];
						grd상세["STA_GIR"]		= row["진행상태"];
						grd상세["DT_IO"]			= row["출고일자"];
						grd상세["PAGE_ST"]		= row["페이지시작"];
						grd상세["PAGE_ED"]		= row["페이지종료"];
						grd상세["PAGE_CHK_PB"]	= row["페이지발행"];
						grd상세["PAGE_CHK_RC"]	= row["페이지접수"];
						grd상세["FILE_CHK_PB"]	= row["파일발행"];
						grd상세["FILE_CHK_RC"]	= row["파일접수"];
						grd상세["YN_SEQ"]		= row["등록여부"];
						grd상세["DC_FILE"]		= Dintec.FILE.GetFileName(pdf);
						grd상세["NM_FILE"]		= pdf;
						grd상세["CD_FILE"]		= row["협조전번호"] + "_" + row["회사코드"];
						grd상세.AddFinished();
					} 
				}

				// 오류체크
				오류체크(grd상세);

				// ********** RPA일 경우 자동 저장
				if (LoginInfo.UserID == "SYSADMIN")
				{
					if (grd상세.DataTable.Select("ERROR <> ''").Length > 0)
						throw new Exception(grd상세.DataTable.Select("ERROR <> ''")[0]["ERROR"].ToString());

					OnToolBarSaveButtonClicked(null, null);
				}

				UTIL.작업중();
			}
			catch (Exception ex)
			{
				UTIL.메세지(ex);
			}
		}

		private void Btn업로드_인수증만_Click(object sender, EventArgs e)
		{
			// 바코드 읽기
			BARCODE barcode = new BARCODE() { QRCode = true };
			barcode.OpenFileDialog();

			tbx로그.Text += "\r\n" + "********** 시작" + "\r\n";
			tbx로그.Text += "업로드 : " + barcode.FileName + "\r\n";
			tbx로그.Text += "총 페이지 수 : " + PDF.GetPageCount(barcode.FileName) + "\r\n";

			UT.ShowPgb(DD("분석 중 입니다."));
			grd라인.Clear2();
			grd상세.Clear2();
			barcode.Read();

			tbx로그.Text += "읽은 QR 수 : " + barcode.Items.Length + "\r\n";

			// QR 데이터를 저장할 데이터테이블
			//DataTable dtOld = new DataTable();
			//dtOld.Columns.Add("qr"		, typeof(string));
			//dtOld.Columns.Add("comCd"	, typeof(string));
			//dtOld.Columns.Add("woNo"	, typeof(string));
			//dtOld.Columns.Add("WoSt"	, typeof(string));
			//dtOld.Columns.Add("fileNo"	, typeof(string));
			//dtOld.Columns.Add("pageCnt"	, typeof(int));

			//dtOld.Columns.Add("pageSt"	, typeof(int));
			//dtOld.Columns.Add("pageEd"	, typeof(int));
			//DataTable dtNew = dtOld.Clone();

			DataTable dt = new DataTable();
			dt.Columns.Add("qr"			, typeof(string));
			dt.Columns.Add("comCd"		, typeof(string));
			dt.Columns.Add("woNo"		, typeof(string));
			dt.Columns.Add("WoSt"		, typeof(string));
			dt.Columns.Add("출고일자"		, typeof(string));
			dt.Columns.Add("fileNo"		, typeof(string));
			dt.Columns.Add("pageSt"		, typeof(int));
			dt.Columns.Add("pageEd"		, typeof(int));
			dt.Columns.Add("pageCnt"	, typeof(int));
			dt.Columns.Add("pageChkPb"	, typeof(int));
			dt.Columns.Add("pageChkRc"	, typeof(int));
			dt.Columns.Add("fileChkPb"	, typeof(int));
			dt.Columns.Add("fileChkRc"	, typeof(int));
			

			// 현재 파일 경로			
			string path = PATH.GetPath(barcode.FileName) + @"\";

			// ********** QR에서 코드에서 필요한 정보 추출 (파일단위로 쪼개짐)
			for (int i = 0; i < barcode.Items.Length; i++)
			{				
				string qr = barcode.Items[i].Value;
				tbx로그.Text += string.Format("{0:00}", i + 1) + "번 QR : " + qr + "\r\n";
				qr = qr.Replace("http://", "").Replace("dintec.kr/", "").Replace("dint.ec/", "");	// 주소를 지워서 기존 형태로 만듬

				if (qr == "데이터 없음")
				{					
					grd라인.Rows.Add();
					grd라인.Row = grd라인.Rows.Count - 1;
					grd라인["PAGE_ST"] = barcode.Items[i].Page;
					grd라인["ERROR"] = qr;
					continue;
				}

				string comCd;
				string woNo;
				string fileNo;

				if (qr.Length > 7)
				{
					// 옛날 QR코드
					comCd = qr.Substring(0, 2) + "00";
					woNo = (comCd == "K100" ? "WO" : "DO") + qr.Substring(2, 8);
					fileNo = "";
				}
				else
				{
					// 신규 QR코드
					DataTable dtShot = SQL.GetDataTable("PS_CZ_DX_URL_BASE62", SQLDebug.Print, qr);

					if (!dtShot.존재())
					{
						유틸.메세지("잘못된 qr코드입니다. " + barcode.Items[i].Page);
						return;
					}

					string data = dtShot.Rows[0]["DATA"].ToString();					
					comCd = data.Split('/')[0];
					woNo = data.Split('/')[1];
					fileNo = data.Split('/')[2];
				}

				// 데이터테이블 추가
				string query = @"
SELECT
	A.STA_GIR
,	PAGE_CHK = A.CD_USERDEF2
--,	FILE_CHK = (SELECT COUNT(DISTINCT NULLIF(TXT_USERDEF1, '')) FROM SA_GIRL AS X WHERE A.CD_COMPANY = X.CD_COMPANY AND A.NO_GIR = X.NO_GIR)
,	FILE_CHK = (SELECT COUNT(DISTINCT NO_SO) FROM SA_GIRL AS X WHERE A.CD_COMPANY = X.CD_COMPANY AND A.NO_GIR = X.NO_GIR)
,	출고일자	= (SELECT MAX(DT_IO) FROM MM_QTIO AS X WHERE A.CD_COMPANY = X.CD_COMPANY AND A.NO_GIR = X.NO_ISURCV)
FROM SA_GIRH				AS A
JOIN CZ_SA_GIRH_WORK_DETAIL	AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_GIR = B.NO_GIR
WHERE 1 = 1
	AND A.CD_COMPANY = '" + comCd + @"'
	AND A.NO_GIR = '" + woNo + "'";

				DataTable dtWo = SQL.GetDataTable(query);

				if (!dtWo.존재())
				{
					유틸.메세지("협조전을 찾을수없습니다. " + barcode.Items[i].Page);
					return;
				}


				string woSt = dtWo.Rows[0]["STA_GIR"].ToString();				
				int pageSt = barcode.Items[i].Page;
				int pageEd = i < barcode.Items.Length - 1 ? barcode.Items[i + 1].Page - 1 : PDF.GetPageCount(barcode.FileName);
				int pageCnt = pageEd - pageSt + 1;
				int pageChk = dtWo.Rows[0]["PAGE_CHK"].ToInt();
				int fileChk = dtWo.Rows[0]["FILE_CHK"].ToInt();

				// 행 추가
				dt.Rows.Add(qr, comCd, woNo, woSt, dtWo.첫행("출고일자"), fileNo, pageSt, pageEd, pageCnt, pageChk);
				if (fileChk > 0) dt.Rows[dt.Rows.Count - 1]["fileChkPb"] = fileChk;
			}

			// 페이지, 파일 갯수 체크 행
			foreach (DataRow row in dt.Rows)
			{
				DataRow[] rows = dt.Select("comCd = '" + row["comCd"] + "' AND woNo = '" + row["woNo"] + "'");				
				row["pageChkRc"] = rows.Sum(r => r.Field<int>("pageCnt"));
				if (row["fileChkPb"].ToInt() > 0) row["fileChkRc"] = rows.Length;
			}

			// ********** 인수증 pdf 만들기 - NEW
			foreach (DataRow row in dt.Select("fileNo <> ''"))
			{
				// 인수증 한건 추출 (파일단위)
				string pdf = path + row["woNo"] + "_" + row["fileNo"] + "_" + row["comCd"] + ".pdf";				
				PDF.ExtractPageRange(barcode.FileName, pdf, (int)row["pageSt"], (int)row["pageEd"]);
				tbx로그.Text += "인수증 단위 분할 : " + Dintec.FILE.GetFileName(pdf) + "\r\n";

				// 그리드 추가
				grd상세.Rows.Add();
				grd상세.Row = grd상세.Rows.Count - 1;
				grd상세["CD_COMPANY"]	= row["comCd"];
				grd상세["NO_GIR"]		= row["woNo"];
				grd상세["STA_GIR"]		= row["woSt"];
				grd상세["DT_IO"]			= row["출고일자"];
				grd상세["PAGE_ST"]		= row["pageSt"];
				grd상세["PAGE_ED"]		= row["pageEd"];
				grd상세["PAGE_CHK_PB"]	= row["pageChkPb"];
				grd상세["PAGE_CHK_RC"]	= row["pageChkRc"];
				grd상세["FILE_CHK_PB"]	= row["fileChkPb"];
				grd상세["FILE_CHK_RC"]	= row["fileChkRc"];
				grd상세["YN_SEQ"]		= 등록여부(row["comCd"], row["woNo"]) ? "Y" : "";
				grd상세["DC_FILE"]		= Dintec.FILE.GetFileName(pdf);
				grd상세["NM_FILE"]		= pdf;
				grd상세["CD_FILE"]		= row["woNo"] + "_" + row["comCd"];
				grd상세.AddFinished();
			}

			// ********** 인수증 pdf 만들기 - OLD
			foreach (string comCd in dt.Select("fileNo = ''").AsEnumerable().Select(r => r["comCd"]).Distinct().ToArray())
			{
				foreach (string woNo in dt.Select("fileNo = ''").AsEnumerable().Where(r => r["comCd"].ToString() == comCd).Select(r => r["woNo"]).Distinct().ToArray())
				{
					DataRow[] rows = dt.Select("comCd = '" + comCd + "' AND woNo = '" + woNo + "'");
					string[] subPdfs = new string[rows.Length];

					for (int i = 0; i < rows.Length; i++)
					{
						// 인수증 한건 추출 (파일단위)
						subPdfs[i] = path + woNo + "_" + comCd + "_" + i + ".pdf";
						tbx로그.Text += "인수증 단위 분할 : " + Dintec.FILE.GetFileName(subPdfs[i]) + "\r\n";						
						PDF.ExtractPageRange(barcode.FileName, subPdfs[i], (int)rows[i]["pageSt"], (int)rows[i]["pageEd"]);						
					}

					// 각각 나온 인수증을 협조전 하나로 합침
					string finalPdf = path + woNo + "_" + comCd + ".pdf";
					tbx로그.Text += "협조전 단위 병합 : " + finalPdf + "\r\n";
					PDF.Merge(finalPdf, subPdfs);
					
					// 그리드 추가
					grd라인.Rows.Add();
					grd라인.Row = grd라인.Rows.Count - 1;
					grd라인["CD_COMPANY"]	= comCd;
					grd라인["NO_GIR"]		= woNo;
					grd라인["STA_GIR"]		= rows[0]["woSt"];
					grd라인["PAGE_ST"]		= rows[0]["pageSt"];
					grd라인["PAGE_ED"]		= rows[0]["pageEd"];
					grd라인["PAGE_CHK_PB"]	= rows[0]["pageChkPb"];
					grd라인["PAGE_CHK_RC"]	= rows[0]["pageChkRc"];
					grd라인["FILE_CHK_PB"]	= rows[0]["fileChkPb"];
					grd라인["FILE_CHK_RC"]	= rows[0]["fileChkRc"];
					grd라인["YN_SEQ"]		= 등록여부(comCd, woNo) ? "Y" : "";
					grd라인["DC_FILE"]		= Dintec.FILE.GetFileName(finalPdf);
					grd라인["NM_FILE"]		= finalPdf;
					grd라인["CD_FILE"]		= woNo + "_" + comCd;
					grd라인.AddFinished();
				}
			}

			오류체크(grd라인);
			오류체크(grd상세);

			UT.ClosePgb();
		}

		private bool 등록여부(object comCd, object woNo)
		{
			string query = @"
SELECT
	1
FROM MA_FILEINFO WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + comCd + @"'
	AND CD_MODULE = 'SA'
	AND ID_MENU = 'P_CZ_SA_GIM_REG'
	AND CD_FILE = '" + woNo + "_" + comCd + @"'";

			return SQL.GetDataTable(query).Rows.Count > 0;
		}

		private void 오류체크(FlexGrid flexGrid)
		{
			for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
			{
				if (flexGrid[i, "STA_GIR"].ToString() != "C")
					flexGrid[i, "ERROR"] += "종결된 인수증이 아닙니다.";

				if (flexGrid[i, "PAGE_CHK_PB"].ToInt() != flexGrid[i, "PAGE_CHK_RC"].ToInt())
					flexGrid[i, "ERROR"] += "발행 페이지와 접수 페이지가 다릅니다.";

				if (flexGrid[i, "FILE_CHK_PB"].ToInt() != flexGrid[i, "FILE_CHK_RC"].ToInt())
					flexGrid[i, "ERROR"] += "발행 파일과 접수 파일이 다릅니다.";

				if (flexGrid[i, "YN_SEQ"].ToString() != "")
					flexGrid[i, "ERROR"] += "이미 접수된 인수증입니다.";
			}
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{			
			base.OnToolBarSaveButtonClicked(sender, e);
			
			// ********** 유효성 검사
			if (dtp선적일자.Text == "")
			{
				if (LoginInfo.UserID.In("S-343", "SYSADMIN"))
				{
					// RPA의 경우 선적일자가 없는 경우 출고일자를 인식하게 해줌
				}
				else
				{
					UT.ShowMsg(공통메세지._은는필수입력항목입니다, DD("선적일자"));
					return;
				}
			}

			if (dtp선적일자.Value > DateTime.Now)
			{
				UT.ShowMsg("선적일자가 잘못되었습니다.");
				return;
			}

			try
			{
				Save(grd라인);
				Save(grd상세);

				UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
				Clear();
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Save(FlexGrid grid)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			for (int i = grid.Rows.Fixed; i < grid.Rows.Count; i++)
			{
				// ********** DB 인서트
				string query = @"
SELECT
	ISNULL(MAX(NO_SEQ), 0) + 1
FROM MA_FILEINFO WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + grid[i, "CD_COMPANY"] + @"'
	AND CD_MODULE = 'SA'
	AND ID_MENU = 'P_CZ_SA_GIM_REG'
	AND CD_FILE = '" + grid[i, "CD_FILE"] + @"'

SELECT
	DT_GIR
FROM SA_GIRH WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + grid[i, "CD_COMPANY"] + @"'
	AND NO_GIR = '" + grid[i, "NO_GIR"] + @"'
";

				DataSet ds = SQL.GetDataSet(query);
				int seq = ds.Tables[0].Rows[0][0].ToInt();
				string year = ds.Tables[1].Rows[0][0].ToString().Substring(0, 4);
				string filePath = "Upload/P_CZ_SA_GIM_REG" + "/" + grid[i, "CD_COMPANY"] + "/" + year + "/" + grid[i, "CD_FILE"];
				FileInfo fileInfo = new FileInfo(grid[i, "NM_FILE"].ToString());

				// ********** 파일 업로드
				Dintec.FILE.Upload(grid[i, "NM_FILE"].ToString(), filePath);

				// ********** DB 인수증 인서트
				SQL sql = new SQL("UP_MA_FILEINFO_INSERT", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@P_CD_COMPANY"	, grid[i, "CD_COMPANY"]);
				sql.Parameter.Add2("@P_CD_MODULE"	, "SA");
				sql.Parameter.Add2("@P_ID_MENU"		, "P_CZ_SA_GIM_REG");
				sql.Parameter.Add2("@P_CD_FILE"		, grid[i, "CD_FILE"]);
				sql.Parameter.Add2("@P_NO_SEQ"		, seq);
				sql.Parameter.Add2("@P_FILE_NAME"	, fileInfo.Name);
				sql.Parameter.Add2("@P_FILE_PATH"	, filePath);
				sql.Parameter.Add2("@P_FILE_EXT"	, fileInfo.Extension.Replace(".", ""));
				sql.Parameter.Add2("@P_FILE_DATE"	, fileInfo.LastWriteTime.ToString("yyyyMMdd"));
				sql.Parameter.Add2("@P_FILE_TIME"	, fileInfo.LastWriteTime.ToString("HHmm"));
				sql.Parameter.Add2("@P_FILE_SIZE"	, string.Format("{0:0.00}M", fileInfo.Length / 1048576.0));
				sql.Parameter.Add2("@P_ID_INSERT"	, LoginInfo.UserID);
				sql.ExecuteNonQuery();

				// ********** 리마크 업데이트
				query = @"
UPDATE MA_FILEINFO SET
	DC_RMK = '인수증 QR업로드'
WHERE 1 = 1
	AND CD_COMPANY = '" + grid[i, "CD_COMPANY"] + @"'
	AND CD_MODULE = 'SA'
	AND ID_MENU = 'P_CZ_SA_GIM_REG'
	AND CD_FILE = '" + grid[i, "CD_FILE"] + @"'
	AND NO_SEQ = " + seq;

				SQL.ExecuteNonQuery(query);

				// ********** 선적일자 업데이트
				string 선적일자 = dtp선적일자.Text != "" ? dtp선적일자.Text : grid[i, "DT_IO"].ToString();
				query = @"
UPDATE CZ_SA_GIRH_WORK_DETAIL SET
	DT_LOADING = '" + 선적일자 + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + grid[i, "CD_COMPANY"] + @"'
	AND NO_GIR = '" + grid[i, "NO_GIR"] + @"'
	AND ISNULL(DT_LOADING, '') = ''";

				SQL.ExecuteNonQuery(query);
			}

			grid.Clear2();
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			if (!MsgAndSave(PageActionMode.Search))
				return;

			Clear();
		}

		#endregion
	}
}
