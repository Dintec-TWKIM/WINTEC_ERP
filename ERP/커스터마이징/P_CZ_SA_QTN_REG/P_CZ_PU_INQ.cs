using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.Controls;
using Duzon.ERPU.Utils;
using System.Net;


using System.IO;

using System.Web.Script.Serialization;
using DX;

// 20190917
namespace cz
{ 
	public partial class P_CZ_PU_INQ : PageBase
	{
		Exception Ex;

		#region ===================================================================================================== Property

		private P_CZ_SA_QTN_REG Quotation
		{
			get
			{
				return (P_CZ_SA_QTN_REG)this.Parent.GetContainerControl();
			}
		}

		private string PartnerCode
		{
			get
			{
				return grd헤드["CD_PARTNER"].ToString();
			}
		}

		public FlexGrid 그리드헤드
		{
			get
			{
				return grd헤드;
			}
		}

		public FlexGrid 그리드라인
		{
			get
			{
				return grd라인;
			}
		}		
		
		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PU_INQ()
		{
			//StartUp.Certify(this);
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			TitleText = "Loaded";
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			cbo매입처담당자.ValueMember = "SEQ";
			cbo매입처담당자.DisplayMember = "NM_PTR";
			
			// 모자 세팅
			grd헤드.DetailGrids = new FlexGrid[] { grd라인, grd카트 };
		}

		private void InitGrid()
		{
			DataTable dtYN = new DataTable();
			dtYN.Columns.Add("CODE");
			dtYN.Columns.Add("NAME");
			dtYN.Rows.Add("Y", DD("계약"));
			dtYN.Rows.Add("N", "");

			// ********** 헤드
			grd헤드.BeginSetting(1, 1, false);

			grd헤드.SetCol("NO_FILE"			, "파일번호"		, false);
			grd헤드.SetCol("CD_PARTNER"		, "코드"			, 60	, TextAlignEnum.CenterCenter);
			grd헤드.SetCol("LN_PARTNER"		, "매입처명"		, 230);
			grd헤드.SetCol("DT_INQ"			, "작성일자"		, false);
			grd헤드.SetCol("CNT_ITEM"		, "아이템"		, 50	, typeof(decimal)	, FormatTpType.QUANTITY);
			grd헤드.SetCol("FILE_ICON"		, "첨부"			, 50	, ImageAlignEnum.CenterCenter);		// 첨부파일용(최근파일 아이콘 하나-바로열리기용, 다수일경우 POP-UP아이콘 추가 하나 해서 총 2개 뿌리자)
			grd헤드.SetCol("YN_EXT"			, "계약"			, 50	, TextAlignEnum.CenterCenter);
			grd헤드.SetCol("CD_PINQ"			, "발신방법"		, 80	, TextAlignEnum.CenterCenter);			
			grd헤드.SetCol("DT_SEND_MAIL"	, "발송일(메일)"	, 140	, typeof(string)	, "####/##/## ##:##:##");
			grd헤드.SetCol("DT_SEND_WORK"	, "발송일(워크)"	, 140	, typeof(string)	, "####/##/## ##:##:##");
			grd헤드.SetCol("NM_FILE"			, "첨부파일명"	, false);
			grd헤드.SetCol("SEQ_ATTN"		, "담당자코드"	, false);
			grd헤드.SetCol("NO_REF"			, "문의번호"		, false);
			grd헤드.SetCol("YN_QUICK"		, "자동견적"		, false);
			grd헤드.SetCol("DC_RMK_INQ"		, "비고"			, false);
			grd헤드.SetCol("CD_AREA"			, "지역구분"		, false);
			grd헤드.SetCol("CD_PRINT"		, "인쇄구분"		, false);
			grd헤드.SetCol("YN_SPO"			, "기획실"		, false);
			
			grd헤드.SetDataMap("YN_EXT", dtYN, "CODE", "NAME");
			grd헤드.SetDataMap("CD_PINQ", CODE.Key("CZ_PU00001"), "CODE", "NAME");

			grd헤드.SetOneGridBinding(new object[] { }, one헤드);
			grd헤드.SetBindningCheckBox(chk자동, "Y", "N");
			grd헤드.VerifyPrimaryKey = new string[] { "CD_PARTNER" };

			grd헤드.SetDefault("21.03.08.02", SumPositionEnum.None);

			// ********** 라인
			grd라인.BeginSetting(1, 1, false);

			grd라인.SetCol("CHK"				, "S"			, 30	, true, CheckTypeEnum.Y_N);
			grd라인.SetCol("NO_FILE"			, "파일번호"		, false);
			grd라인.SetCol("CD_PARTNER"		, "매입처코드"	, false);
			grd라인.SetCol("NO_LINE"			, "고유번호"		, false);
			grd라인.SetCol("NO_LINE_PARENT"	, "부모번호"		, false);
			grd라인.SetCol("NO_DSP"			, "순번"			, 45);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 250);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 80);
			grd라인.SetCol("NM_UNIT"			, "단위"			, 50);
			grd라인.SetCol("QT"				, "수량"			, 60	, false, typeof(decimal), FormatTpType.QUANTITY);			
			grd라인.SetCol("TP_BOM"			, "BOM구분"		, false);
			grd라인.SetCol("SORT"			, "SORT"		, false);

			grd라인.Cols["NO_DSP"].Format = "####.##";
			grd라인.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NM_UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.SetDummyColumn("CHK");

			grd라인.SetDefault("19.06.20.01", SumPositionEnum.None);

			// ********** 카트
			grd카트.BeginSetting(1, 1, false);
				
			grd카트.SetCol("CHK"				, "S"			, 30	, true, CheckTypeEnum.Y_N);
			grd카트.SetCol("NO_FILE"			, "파일번호"		, false);
			grd카트.SetCol("CD_PARTNER"		, "매입처코드"	, false);
			grd카트.SetCol("NO_LINE"			, "고유번호"		, false);
			grd카트.SetCol("NO_LINE_PARENT"	, "부모번호"		, false);
			grd카트.SetCol("NO_DSP"			, "순번"			, 45);
			grd카트.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd카트.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd카트.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 250);
			grd카트.SetCol("CD_ITEM"			, "재고코드"		, 80);
			grd카트.SetCol("NM_UNIT"			, "단위"			, 50);			
			grd카트.SetCol("QT"				, "수량"			, 60	, false, typeof(decimal), FormatTpType.QUANTITY);			
			grd카트.SetCol("TP_BOM"			, "BOM구분"		, false);
			grd카트.SetCol("SORT"			, "SORT"		, false);

			grd카트.Cols["NO_DSP"].Format = "####.##";
			grd카트.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd카트.Cols["NM_UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grd카트.SetDummyColumn("CHK");

			grd카트.SetDefault("19.02.27.01", SumPositionEnum.None);

			// 바인딩 초기화
			grd헤드.Binding = DBMgr.GetDataTable("PS_CZ_PU_INQ_REG_H", "", "", 0);
			grd라인.Binding = DBMgr.GetDataTable("PS_CZ_PU_INQ_REG_L", "", "", 0, "");
		}
		
		

		protected override void InitPaint()
		{
			if (!Certify.IsLive())
			{
				//grd헤드.Cols.Remove("FILE_ICON");
				grd헤드.Cols.Remove("YN_EXT");
				grd헤드.Cols.Remove("NM_FILE");
			}
		}

		public void Clear()
		{
			if (TitleText != "Loaded")
				return;

			dtp작성일자.ClearData();
			cbo매입처담당자.ClearData();
			tbx견적번호.Text = "";
			chk자동.Checked = false;
			tbx비고.Text = "";

			grd헤드.ClearData();
			grd라인.ClearData();
			grd카트.ClearData();
		}

		public void 사용(bool editable)
		{
			pnl버튼.Editable(editable);
			one헤드.Editable(editable);
			btnTo아이템1.Enabled = editable;
			btnTo아이템2.Enabled = editable;

			grd헤드.AllowEditing = editable;
			grd라인.AllowEditing = editable;
			grd카트.AllowEditing = editable;
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn일괄변환.Click += Btn일괄변환_Click;
			btn메일발송.Click += Btn메일발송_Click;
			btn워크전달.Click += Btn워크전달_Click;

			btn추가.Click += new EventHandler(Btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			cbo매입처담당자.SelectionChangeCommitted += new EventHandler(Cbo거래처담당자_SelectionChangeCommitted);
			btn비고적용.Click += new EventHandler(btn비고적용_Click);

			btnTo아이템1.Click += new EventHandler(btn이동_Click);
			btnTo아이템2.Click += new EventHandler(btn이동_Click);

			grd헤드.AfterRowChange += new RangeEventHandler(grd헤드_AfterRowChange);
			grd헤드.DoubleClick += new EventHandler(grd헤드_DoubleClick);

			grd라인.AfterDataRefresh += new ListChangedEventHandler(grd라인_AfterDataRefresh);
			grd카트.AfterDataRefresh += new ListChangedEventHandler(grd라인_AfterDataRefresh);
			grd라인.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);
			grd카트.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);
		}

		
		private void Btn메일발송_Click(object sender, EventArgs e)
		{
			저장();

			// 테스트용
			CheckWorkSend(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"]);

			if (grd헤드["DT_SEND_WORK"].문자() == "" && CheckWorkSend(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"]))
			{
				// 워크전달
				워크전달(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"]);

				// 해당 매입처가 워크&메일 동시 적용 업체인지 확인
				string query = "SELECT CD_FLAG8 FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = 'K100' AND CD_FIELD = 'CZ_DX00009' AND CD_SYSDEF = '" + grd헤드["CD_PARTNER"] + "'";
				DataTable dt = 디비.결과(query);

				// 메일발송도 함
				if (dt.첫행(0).문자() == "Y")
					SendMail(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"], false);
			}
			else
				SendMail(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"], false);
		}

		private void Btn워크전달_Click(object sender, EventArgs e)
		{
			저장();

			if (워크전달(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"]))
				UT.ShowMsg("해당 담당자의 워크플로우에 전달되었습니다.");
			else
				UT.ShowMsg(Ex);
		}

		private void Btn일괄변환_Click(object sender, EventArgs e)
		{			
			bool boSend = false;
			int success = 0, fail = 0, skip = 0;

			if (UT.ShowMsg("일괄변환 시 많은 시간이 소요됩니다. 진행 하시겠습니까?", "QY2") != DialogResult.Yes)
				return;

			if (UT.ShowMsg("견적문의서 발송을 함께 진행하시겠습니까?", "QY2") == DialogResult.Yes)
				boSend = true;

			UT.ShowPgb("작업 진행 중입니다. \r\n잠시만 기다려주세요!");

			// 저장 후 진행
			저장();

			// 루프 돌면서 발송 실행
			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				string partnerCode = (string)grd헤드[i, "CD_PARTNER"];

				try
				{
					// 파일 저장
					Print(Quotation.회사코드, Quotation.파일번호, partnerCode, null, false);
					grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension(Path.GetExtension((string)grd헤드[i, "NM_FILE"]).Replace(".", "")));

					// 견적문의서 발송
					if (boSend)
					{
						if (partnerCode == "10493")		// 10493 : DINTEC CO., LTD.
						{
							skip++;
							continue;
						}

						// EXT값이 모두 Y인 경우는 스킵
						string query = @"
SELECT 1
FROM CZ_PU_QTNL 
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND NO_FILE = '" + Quotation.파일번호 + @"'
	AND CD_PARTNER = '" + partnerCode + @"'
	AND ISNULL(YN_EXT, 'N') = 'N'";

						if (SQL.GetDataTable(query).Rows.Count == 0)
						{
							skip++;
						}
						else
						{
							string inqCode = (string)grd헤드[i, "CD_PINQ"];

							// 워크전달건인지 확인
							if (CheckWorkSend(Quotation.회사코드, Quotation.파일번호, partnerCode))
							{
								if (워크전달(Quotation.회사코드, Quotation.파일번호, partnerCode))
								{
									// 해당 매입처가 워크&메일 동시 적용 업체인지 확인
									query = "SELECT CD_FLAG8 FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = 'K100' AND CD_FIELD = 'CZ_DX00009' AND CD_SYSDEF = '" + partnerCode + "'";
									DataTable dt = 디비.결과(query);

									// 메일발송도 함
									if (dt.첫행(0).문자() == "Y")
										SendMail(Quotation.회사코드, Quotation.파일번호, partnerCode, true);

									success++;
								}
								else
									fail++;
							}
							else if (inqCode == "EML")
							{
								if (SendMail(Quotation.회사코드, Quotation.파일번호, partnerCode, true))
									success++;
								else
									fail++;
							}
							else
							{
								skip++;
							}
						}
					}
				}
				catch
				{
					fail++;
				}
			}

			UT.ClosePgb();

			// 완료 메세지
			string msg = @"
작업이 완료되었습니다.

발송성공 : " + success + @"건
발송예외 : " + skip + @"건
발송실패 : " + fail + "건";

			UT.ShowMsg(msg);

			if (fail > 0) UT.ShowMsg("발송실패 건수가 있습니다. 확인바랍니다.");
		}

		public bool CheckWorkSend(string companyCode, string fileNumber, string partnerCode)
		{
			// 코드관리에서 워크로 강제 할당시키는지 여부 판단
			DataTable dtChkWf = SQL.GetDataTable("PS_CZ_PU_INQ_CHK_WF", SQLDebug.Print, companyCode, fileNumber, partnerCode);
		
			if (dtChkWf.Rows.Count > 0)
				return true;
			else
				return false;
		}

		public bool SendMail(string companyCode, string fileNumber, string partnerCode, bool boAuto)
		{
			string query;

			// 시작
			DataRow headRow = SQL.GetDataTable("PS_CZ_PU_INQ_RPT_H", companyCode, fileNumber, partnerCode).Rows[0];
			DataTable dtLine = SQL.GetDataTable("PS_CZ_PU_INQ_RPT_L", companyCode, fileNumber, partnerCode);

			// ********** 경고마스터 확인
			// 업데이트 모드로 변경 (그래야 경고마스터에서 인식함)
			foreach (DataRow row in dtLine.Rows)
				row.SetModified();

			//Warning warning = new Warning(WarningFlag.QTN);
			//warning.WarningCode = "007";
			//warning.FileCode = fileNumber.Left(2);
			//warning.BuyerCode = Quotation.PartnerCode;
			//warning.ImoNumber = Quotation.ImoNumber;
			//warning.SupplierCode = partnerCode;
			//warning.Item = dtLine;
			//warning.Check();

			//if (warning.Count > 0)
			//{
			//	if (!Quotation.EnabledMessage)
			//	{
			//		return false;   // 탭넘김 자동 저장이면 저장안함
			//	}
			//	else
			//	{
			//		ShowMessage(warning.Message);
			//		//if (!warning.AllowSave)
			//		//	return false;
			//		return false;
			//	}
			//}

			// ********** 경고마스터 확인
			WARNING warning = new WARNING(WARNING_TARGET.견적)
			{
				경고구분		= "007"
			,	파일구분		= Quotation.파일번호.Left(2)
			,	매출처코드	= Quotation.매출처코드
			,	IMO번호		= Quotation.Imo번호
			,	매입처코드	= partnerCode
			,	아이템		= dtLine
			,	SQLDebug	= SQLDebug.Popup
			};

			// 조회
			warning.조회(Quotation.메시지여부);

			// 경고할 꺼리가 있는 경우만.
			if (warning.경고여부)
			{
				if (!Quotation.메시지여부)
				{
					return false;   // 탭넘김 자동 저장이면 저장안함
				}
				else
				{
					warning.ShowDialog();
					UTIL.메세지("작업이 취소되었습니다.", "WK1");
					return false;
					//if (warning.저장불가 || 경고결과 == DialogResult.Cancel)
					//{
					//	UTIL.메세지("작업이 취소되었습니다.", "WK1");
					//	return false;
					//}
				}
			}



			// ********** 메일 발송
			// 필수값 체크
			if (headRow["EN_EMP"].ToString() == "")
			{
				ShowMessage("영문 이름이 설정되어 있지 않습니다.");
				return false;
			}

			// ********** 서광산전 제목/내용 아무것도 안넣기 (해킹업체)
			bool isHack = false;

            if (partnerCode.In("08012"))
                isHack = true;

			// 딘텍 담당자 정보
			string empNameKor = (string)headRow["NM_EMP"];
			string empNameEng = (string)headRow["EN_EMP"];
			string empTelNumber = (string)headRow["NO_TEL_EMP"];			
			string empMail = (string)headRow["NO_EMAIL_EMP"];

			// 메일 주소
			string from = fileNumber.Left(2).In("FB", "DS", "NB", "TB") ? "service@dintec.co.kr" : empMail;
			string to = headRow["E_MAIL_PARTNER"].ToString();
			string cc = empMail;
			
			// 주제에 따라 발송메일 주소 변경
			DataTable dtMailBySubj = DBMgr.GetDataTable("PS_CZ_PU_INQ_SUBJMAIL", companyCode, fileNumber, partnerCode);

			if (dtMailBySubj.Rows.Count > 0)
			{
				to = (string)dtMailBySubj.Rows[0]["MAIL"];
				ShowMessage("메일 주소가 변경됩니다.");
			}

			// 제목
			string 매출처태그 = "";

			// 특정 매입처는 매출처 정보 넣어줌
			if (headRow["CD_PARTNER"].문자().포함("00523", "07110", "08075"))
			{
				query = "SELECT LN_PARTNER, NEOE.CODE_NAME(CD_COMPANY, 'MA_B000020', CD_NATION)  FROM MA_PARTNER WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND CD_PARTNER = '" + headRow["CD_BUYER"] + "'";
				DataTable dtBuyer = 디비.결과(query);
				매출처태그 = "/" + dtBuyer.첫행(0) + "/" + dtBuyer.첫행(1);
			}

			string title = headRow["NM_VESSEL"] + " / " + headRow["NM_SHIP_YARD"] + " " + headRow["NO_HULL"] + " (IMO:" + headRow["NO_IMO"] + ")" + 매출처태그 + " _" + partnerCode;

            if (!isHack)
            {
                if (companyCode == "TEST") title = "DINTEC - INQUIRY(" + fileNumber + ") " + title;
                else if (companyCode == "K100") title = "DINTEC - INQUIRY(" + fileNumber + ") " + title;
                else if (companyCode == "K200") title = "DUBHECO - INQUIRY(" + fileNumber + ") " + title;
                else if (companyCode == "S100") title = "DINTEC - INQUIRY(" + fileNumber + ") " + title;
            }
            else
            {
                if (companyCode == "TEST") title = "DINTEC";
                else if (companyCode == "K100") title = "DINTEC";
                else if (companyCode == "K200") title = "DUBHECO";
                else if (companyCode == "S100") title = "DINTEC";
            }

			// 기본파일
			string[] files1 = { headRow["NM_FILE"].ToString() };

			// 추가파일
			query = @"
SELECT
	NM_FILE_REAL
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_KEY = '" + fileNumber + @"'
	AND TP_STEP IN ('01', '54')";	// 01:INQUIRY,54:관련도면

			DataTable dtFiles = DBMgr.GetDataTable(query);
			string[] files2 = new string[dtFiles.Rows.Count];

			for (int i = 0; i < dtFiles.Rows.Count; i++)
				files2[i] = dtFiles.Rows[i][0].ToString();

			// ********** SRM 링크 생성
			query = @"
SELECT NEOE.SF_SYSDATE(GETDATE())
SELECT TOP 1 CD_AUTH FROM CZ_SC_AUTH_CODE WHERE CD_COMPANY = '" + companyCode + @"' AND CD_STEP = '1' ORDER BY DT_UPDATE DESC
SELECT TOP 1 CD_AUTH FROM CZ_SC_AUTH_CODE WHERE CD_COMPANY = '" + companyCode + @"' AND CD_STEP = '2' ORDER BY DT_UPDATE DESC";

			DataSet dtCrypt = DBMgr.GetDataSet(query);

			string today = dtCrypt.Tables[0].Rows[0][0].ToString().Substring(0, 8);
			string dateCode = today.Substring(2);
			string encrypted1 = Crypt.Encrypt(dateCode, dtCrypt.Tables[1].Rows[0][0].ToString()).Substring(0, 22);
			string encrypted2 = Crypt.Encrypt(partnerCode + "/" + fileNumber, dtCrypt.Tables[2].Rows[0][0].ToString());
						
			// html 형식으로 변환
			string dateFormat = "{0:yyyy년 MM월 dd일}";
			string srmLink = "";

			if (companyCode == "K100")
			{
				srmLink = @"
<a href='" + "http://srm.dintec.co.kr/RFQ/View.aspx?p=" + Uri.EscapeDataString(encrypted1 + encrypted2) + @"' style='color:#0000ff; text-decoration:underline;' target='_blank'>딘텍 SRM 단가 입력 바로가기</a>
(위 링크는 " + string.Format(dateFormat, DateTime.ParseExact(today, "yyyyMMdd", null).AddDays(6)) + " 까지 유효합니다.)";
			}
			else if (companyCode == "K200")
			{
				srmLink = @"
<a href='" + "http://srm.dubheco.com/RFQ/View.aspx?p=" + Uri.EscapeDataString(encrypted1 + encrypted2) + @"' style='color:#0000ff; text-decoration:underline;' target='_blank'>두베코 SRM 단가 입력 바로가기</a>
(위 링크는 " + string.Format(dateFormat, DateTime.ParseExact(today, "yyyyMMdd", null).AddDays(6)) + " 까지 유효합니다.)";
			}

			// 서명
			string sign = "";

            if (!isHack)
            {
                if (fileNumber.Left(2) == "FB" && (string)headRow["CD_AREA"] == "100") sign = MailSign.GetInquiryKR(srmLink);
                else if (fileNumber.Left(2) == "FB" && (string)headRow["CD_AREA"] != "100") sign = MailSign.GetInquiryEN(srmLink);
                else if (fileNumber.Left(2) == "DS" && (string)headRow["CD_AREA"] == "100") sign = MailSign.GetInquiryKR(srmLink);
                else if (fileNumber.Left(2) == "DS" && (string)headRow["CD_AREA"] != "100") sign = MailSign.GetInquiryEN(srmLink);
                else if (fileNumber.Left(2) != "FB" && (string)headRow["CD_AREA"] == "100") sign = MailSign.GetInquiryKR(empNameKor, empTelNumber, empMail, srmLink);
                else if (fileNumber.Left(2) != "FB" && (string)headRow["CD_AREA"] != "100") sign = MailSign.GetInquiryEN(empNameEng, empTelNumber, empMail, srmLink);
            }

			// ********** 메일발송
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(from, to, cc, "", title, files1, files2, sign, fileNumber, partnerCode, boAuto);

			// 메일 발송에 성공한 경우 수신자 업데이트
			if (f.ShowDialog() == DialogResult.OK)
			{
				// 발송에 성공한 경우 수신자, 발송일 업데이트 =====================> 발송타입도 컬럼 만들어서 저장하자
				query = @"
DECLARE @DT_SEND	NVARCHAR(14) = NEOE.NOW()

UPDATE CZ_PU_QTNH SET
	YN_SEND		 = 'Y'
,	DT_SEND_MAIL = @DT_SEND
,	MAIL_SEND	 = '" + to + @"'
,	YN_SRM		 = 'Y'
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_FILE = '" + fileNumber + @"'
	AND CD_PARTNER = '" + partnerCode + @"'

SELECT @DT_SEND";

				DataTable dtSend = SQL.GetDataTable(query);

				if (grd헤드.DataTable != null)
					grd헤드.DataTable.Select("CD_PARTNER = '" + partnerCode + "'")[0]["DT_SEND_MAIL"] = dtSend.Rows[0][0];

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool 워크전달(string companyCode, string fileNumber, string partnerCode)
		{
			try
			{								
				// 워크발송
				SQL sql = new SQL("SP_CZ_MA_WORKFLOW_SUPPLIER_AUTO", SQLType.Procedure, SQLDebug.Print);
				sql.Parameter.Add2("@P_CD_COMPANY"	, companyCode);
				sql.Parameter.Add2("@P_NO_FILE"		, fileNumber);
				sql.Parameter.Add2("@P_CD_PARTNER"	, partnerCode);
				sql.ExecuteNonQuery();

				// 발송에 성공한 경우 수신자, 발송일 업데이트
				string query = @"
DECLARE @DT_SEND	NVARCHAR(14) = NEOE.NOW()

UPDATE CZ_PU_QTNH SET
	YN_SEND		 = 'Y'
,	DT_SEND_WORK = @DT_SEND
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_FILE = '" + fileNumber + @"'
	AND CD_PARTNER = '" + partnerCode + @"'

SELECT @DT_SEND";

				DataTable dtSend = SQL.GetDataTable(query);

				if (grd헤드.DataTable != null)
					grd헤드.DataTable.Select("CD_PARTNER = '" + partnerCode + "'")[0]["DT_SEND_WORK"] = dtSend.Rows[0][0];

				return true;
			}
			catch (Exception ex)
			{
				Ex = ex;
				return false;
			}
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{			
			H_CZ_SUPPLIER f = new H_CZ_SUPPLIER("INQ");

			if (f.ShowDialog() != DialogResult.OK)
				return;

			// 중복거래처 확인
			foreach (DataRow row in f.Result.Rows)
			{
				if (grd헤드.DataTable.Select("CD_PARTNER = '" + row["CD_PARTNER"] + "'").Length > 0)
				{
					ShowMessage("중복된 거래처가 있습니다.");
					return;
				}
			}

			// 거래처 추가
			grd헤드.Row = grd헤드.Rows.Count - 1;       // AfterRowChange 이벤트로의 점프를 방지하기 위해 미리 행이동 한번 실행

			// 현대하일렉, 제일메카는 항상 같이 오도록 설정 (현대하일렉:05648, 제일메카:12758, 플루맥스:15424, 우양선기:07249)
			DataTable dt동시 = 코드.코드관리("CZ_PU00015");
			int cnt = f.Result.Rows.Count;	// 실시간으로 추가되므로 최초 카운트만큼만 루프 돌림

			for (int i = 0; i < cnt; i++)
			{ 
				foreach (DataRow 코드관리 in dt동시.Rows)
				{
					string[] 동시매입처s = 코드관리["CD_FLAG1"].문자().분할(",");

					// 동시 매입처 중 하나라고 한다면
					 if (f.Result.Rows[i]["CD_PARTNER"].문자().포함(동시매입처s))
					{
						foreach (string s in 동시매입처s)
						{
							if (!f.Result.선택("CD_PARTNER = '" + s + "'").존재())
							{
								DataTable dt = 디비.결과("PS_CZ_MA_PARTNER", 상수.회사코드, s);

								if (dt.Rows.Count == 1)
								{
									f.Result.Rows.Add();
									f.Result.Rows[f.Result.Rows.Count - 1]["CD_PARTNER"] = dt.첫행("CD_PARTNER");
									f.Result.Rows[f.Result.Rows.Count - 1]["LN_PARTNER"] = dt.첫행("LN_PARTNER");
									f.Result.Rows[f.Result.Rows.Count - 1]["CD_PINQ"] = dt.첫행("CD_PINQ");
									f.Result.Rows[f.Result.Rows.Count - 1]["CD_AREA"] = dt.첫행("CD_AREA");
									f.Result.Rows[f.Result.Rows.Count - 1]["CD_PRINT"] = dt.첫행("CD_PRINT");
								}
							}
						}
					}
				}
			}


			foreach (DataRow row in f.Result.Rows)
			{
				// 매입처 추가
				grd헤드.Rows.Add();
				grd헤드.Row = grd헤드.Rows.Count - 1;
				grd헤드["NO_FILE"]		= Quotation.파일번호;
				grd헤드["CD_PARTNER"]	= row["CD_PARTNER"];
				grd헤드["LN_PARTNER"]	= row["LN_PARTNER"];
				grd헤드["DT_INQ"]		= Util.GetToday();
				grd헤드["CD_PINQ"]		= row["CD_PINQ"];
				grd헤드["CD_AREA"]		= row["CD_AREA"];
				grd헤드["CD_PRINT"]		= row["CD_PRINT"];
				grd헤드["_IS_DETAILGET"]	= false;
				
				// 조회
				DBMgr dbm = new DBMgr(DBConn.iU);
				dbm.Procedure = "PS_CZ_PU_INQ_REG_L";
				dbm.AddParameter("CD_COMPANY"	, Quotation.회사코드);
				dbm.AddParameter("NO_FILE"		, Quotation.파일번호);
				dbm.AddParameter("NO_HST"		, Quotation.차수);
				dbm.AddParameter("CD_PARTNER"	, PartnerCode);				
				dbm.AddParameter("ONLY_MANUAL"	, chk계약단가제외.Checked ? "Y" : "N");
				DataTable dt = dbm.GetDataTable();

				// L, R 그리드 바인딩
				string[] range = GetTo.String(row["RANGE"]).Split(',');
				string filter = "";

				foreach (string r in range)
				{
					if (r.Trim() == "") continue;

					// 단일
					if (r.IndexOf("-") < 0)
					{
						filter += " OR ISNULL(NO_DSP, 0) = " + GetTo.Decimal(r);
					}
					// 범위
					else
					{
						decimal from = GetTo.Decimal(r.Split('-')[0]);
						decimal to = GetTo.Decimal(r.Split('-')[1]);
						if (to == 0) to = 100000000;
						filter += " OR (ISNULL(NO_DSP, 0) >= " + from + " AND ISNULL(NO_DSP, 0) <= " + to + ")";
					}
				}

				if (filter == "")
					filter = "ISNULL(NO_DSP, 0) >= 0 AND ISNULL(NO_DSP, 0) <= 100000000";
				else
					filter = filter.Substring(4);	// 첫 " OR" 부분은 삭제

				DataRow[] rowL = dt.Select("IS_NEW ='Y' AND (" + filter + ")", "SORT");
				DataRow[] rowR = dt.Select("IS_NEW ='Y' AND NOT (" + filter + ")", "SORT");				

				DataTable dtL = rowL.Length > 0 ? rowL.CopyToDataTable() : null;
				DataTable dtR = rowR.Length > 0 ? rowR.CopyToDataTable() : null;

				grd라인.BindingAdd(dtL, "CD_PARTNER = '" + PartnerCode + "'", false);
				grd카트.BindingAdd(dtR, "CD_PARTNER = '" + PartnerCode + "'", false);
				grd헤드["CNT_ITEM"] = (dtL == null) ? 0 : dtL.Rows.Count;
			}

			grd헤드.AddFinished();
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			grd헤드.Rows.Remove(grd헤드.Row);
			if (!grd헤드.HasNormalRow)
			{
				Util.Clear(grd라인);
				Util.Clear(grd카트);
			}
		}


		private void btn비고적용_Click(object sender, EventArgs e)
		{
			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
				grd헤드[i, "DC_RMK_INQ"] = tbx비고.Text;
		}

		private void btn이동_Click(object sender, EventArgs e)
		{
			RoundedButton btn = (RoundedButton)sender;
			FlexGrid flexS;
			FlexGrid flexT;

			if (btn.Name == "btnTo아이템1")
			{
				flexS = grd카트;
				flexT = grd라인;
			}
			else
			{
				flexS = grd라인;
				flexT = grd카트;
			}

			// 원본 그리드 선택개수 체크
			DataTable dt = flexS.GetCheckedRows("CHK");
			if (dt == null) return;

			MsgControl.ShowMsg("처리중입니다.");

			string filter = "CD_PARTNER = '" + PartnerCode + "'";
			flexT.Redraw = false;
			flexS.Redraw = false;

			// TARGET 그리드 아이템 추가 및 정렬
			flexT.BindingAdd(dt, filter, false);
			flexT.Sort(SortFlags.Ascending, flexT.Cols["SORT"].Index);

			// SOURCE 그리드 아이템 삭제
			DataRow[] rows = flexS.DataTable.Select("CD_PARTNER = '" + PartnerCode + "' AND CHK = 'Y'");

			for (int i = 0; i < rows.Length; i++)
			{
				MsgControl.ShowMsg("처리중입니다. (" + (i + 1) + "/" + rows.Length + ")");
				rows[i].Delete();
			}

			//DataTable dtSource = flexS.DataTable.Copy();
			//DataRow[] drSource = dtSource.Select("CD_PARTNER <> '" + CD_PARTNER + "' OR ISNULL(CHK, '') <> 'Y'");
			//flexS.DataTable.Rows.Clear();

			//if (drSource.Length > 0)
			//{
			//    flexS.Binding = drSource.CopyToDataTable();
			//    flexS.BindingAdd(null, filter);
			//}

			flexT.Redraw = true;
			flexS.Redraw = true;
			MsgControl.CloseMsg();

			// H 아이템 수량 변경
			grd헤드["CNT_ITEM"] = grd라인.Rows.Count - 1;
		}

		private void Cbo거래처담당자_SelectionChangeCommitted(object sender, EventArgs e)
		{
			grd헤드["SEQ_ATTN"] = cbo매입처담당자.SelectedValue;
		}


		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			// 거래처담당자 바인딩
			DataTable dtPic = GetDb.PartnerPic(PartnerCode, PicType.PurchaseQuotation);
			dtPic.Rows.InsertAt(dtPic.NewRow(), 0);
			cbo매입처담당자.DataSource = dtPic;

			// 기본 담당자 설정
			if (grd헤드["SEQ_ATTN"].ToString() == "")
			{
				DataRow[] rows = dtPic.Select("YN_TYPE = 'Y'", "TP_PTR DESC");

				if (rows.Length > 0)
				{
					grd헤드["SEQ_ATTN"] = rows[0]["SEQ"];
					cbo매입처담당자.SelectedValue = rows[0]["SEQ"];
				}
			}
			else
			{
				cbo매입처담당자.SelectedValue = grd헤드["SEQ_ATTN"];
			}			

			// 라인그리드 바인딩
			string filter = "CD_PARTNER = '" + PartnerCode + "'";
			grd라인.Redraw = false;
			grd카트.Redraw = false;

			if (grd헤드.DetailQueryNeed)
			{
				DBMgr dbm = new DBMgr(DBConn.iU);
				dbm.Procedure = "PS_CZ_PU_INQ_REG_L";
				dbm.AddParameter("@CD_COMPANY"	, Quotation.회사코드);
				dbm.AddParameter("@NO_FILE"		, Quotation.파일번호);
				dbm.AddParameter("@NO_HST"		, Quotation.차수);
				dbm.AddParameter("@CD_PARTNER"	, PartnerCode);
				//dbm.AddParameter("@ONLY_MANUAL"	, chk계약단가제외.Checked ? "Y" : "N");
				dbm.AddParameter("@ONLY_MANUAL"	, "N");
				DataTable dt = dbm.GetDataTable();

				DataRow[] rowCart = dt.Select("IS_NEW = 'N'");
				DataRow[] rowItem = dt.Select("IS_NEW = 'Y'");
				DataTable dtCart = rowCart.Length > 0 ? rowCart.CopyToDataTable() : null;
				DataTable dtItem = rowItem.Length > 0 ? rowItem.CopyToDataTable() : null;

				grd라인.BindingAdd(dtCart, filter);
				grd카트.BindingAdd(dtItem, filter);
			}
			else
			{
				grd라인.BindingAdd(null, filter);
				grd카트.BindingAdd(null, filter);
			}

			grd라인.Redraw = true;
			grd카트.Redraw = true;
		}

		private void grd헤드_DoubleClick(object sender, EventArgs e)
		{
			// 헤더클릭
			if (grd헤드.MouseRow < grd헤드.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 첨부파일 보기
			if (grd헤드.Cols[grd헤드.Col].Name == "FILE_ICON")
			{
				if (grd헤드["NM_FILE"].ToString() == "")
					return;

				FileMgr.Download_WF(Quotation.회사코드, Quotation.파일번호, grd헤드["NM_FILE"].ToString(), true);
			}
		}

		private void SetGridStyle()
		{
			grd헤드.Redraw = false;

			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				string fileName = grd헤드[i, "NM_FILE"].ToString();

				if (fileName == "")
				{
					grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, null);
				}
				else
				{
					Image img = Icons.GetExtension(fileName.Substring(fileName.LastIndexOf(".") + 1));
					grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, img);
				}
			}

			grd헤드.Redraw = true;
		}

		private void grd라인_AfterDataRefresh(object sender, ListChangedEventArgs e)
		{
			//FlexGrid flex = (FlexGrid)sender;

			//for (int i = flex.Rows.Fixed; i < flex.Rows.Count; i++)
			//{
			//    if (flex[i, "TP_BOM"].ToString() == "P") flex.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.FolderExpand);
			//    else if (flex[i, "TP_BOM"].ToString() == "S") flex.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
			//    else if (flex[i, "TP_BOM"].ToString() == "C") flex.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_20x6);
			//}
		}

		private void grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			//FlexGrid flexGrid = (FlexGrid)sender;
			//string CHK = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
			//string KEY = flexGrid["TP_BOM"].ToString() != "C" ? flexGrid["NO_LINE"].ToString() : flexGrid["NO_LINE_PARENT"].ToString();

			//// 이전 행 & 현재 행
			//for (int i = e.Row - 1; i > 0; i--)
			//{
			//    if (flexGrid[i, "NO_LINE"].ToString() != KEY && flexGrid[i, "NO_LINE_PARENT"].ToString() != KEY) break;
			//    flexGrid[i, "CHK"] = CHK;
			//}

			//// 다음 행
			//for (int i = e.Row + 1; i < flexGrid.Rows.Count; i++)
			//{
			//    if (flexGrid[i, "NO_LINE"].ToString() != KEY && flexGrid[i, "NO_LINE_PARENT"].ToString() != KEY) break;
			//    flexGrid[i, "CHK"] = CHK;
			//}
		}
		
		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_PU_INQ_H", Quotation.회사코드, Quotation.파일번호, Quotation.차수);

			// 바인딩
			dtHead.Columns.Add("YN_SPO", typeof(string));
			grd헤드.Binding = dtHead;

			// 마무리
			SetGridStyle();
		}

		#endregion

		#region ==================================================================================================== Save

		public bool 저장()
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			DataTable dtHead = grd헤드.GetChanges();
			DataTable dtLine = grd라인.GetChanges();

			//string headXml = GetTo.Xml(dtHead);
			//string lineXml = GetTo.Xml(dtLine);
			//DBMgr.ExecuteNonQuery("SP_CZ_PU_INQ_REG_XML", headXml, lineXml);

			SQL sql = new SQL("PX_CZ_PU_INQ", SQLType.Procedure, sqlDebug);
			sql.Parameter.Add2("@CD_COMPANY", Quotation.회사코드);
			sql.Parameter.Add2("@NO_FILE"	, Quotation.파일번호);
			sql.Parameter.Add2("@XML_H"		, dtHead.ToXml());
			sql.Parameter.Add2("@XML_L"		, dtLine.ToXml());
			sql.ExecuteNonQuery();

			grd헤드.AcceptChanges();
			grd라인.AcceptChanges();

			return true;
		}

		#endregion

		#region ==================================================================================================== Print

		public void Print(H_CZ_PRT_OPTION option)
		{
			MsgControl.ShowMsg("저장중입니다.\r\n잠시만 기다려주세요!");

			Print(Quotation.회사코드, Quotation.파일번호, (string)grd헤드["CD_PARTNER"], option, true);
			grd헤드.SetCellImage(grd헤드.Row, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension(Path.GetExtension((string)grd헤드["NM_FILE"]).Replace(".", "")));

			// RPA
			if ((string)grd헤드["CD_PARTNER"] == "11823")
			{
				// 딜레이 (분단위)
				string delay = "";
				string urgent = "";

				if (grd헤드["YN_QUICK"].ToString() == "Y")
				{
					delay = "2";  // 2분 내 견적나옴
					urgent = "1";
				}

				//RPA.AddWork("QTN_MAPS", Quotation.FileNumber, (string)grd헤드["CD_PARTNER"], delay, urgent);


				RPA rpa = new RPA() { RpaCode = "QTN_MAPS", FileNumber = Quotation.파일번호, PartnerCode = "11823", DelayMin = delay, Urgent = urgent };
				rpa.AddQueue();
			}

			MsgControl.CloseMsg();
		}

		public string Print(string companyCode, string fileNumber, string partnerCode, H_CZ_PRT_OPTION option, bool run)
		{
			DataRow headRow = DBMgr.GetDataTable("PS_CZ_PU_INQ_RPT_H", companyCode, fileNumber, partnerCode).Rows[0];

			// ********** 옵션
			string sel = "N";
			string ext = "";

			if (option != null && option.SelItem)
			{
				// 요건 웹으로 전달할 방법이 없으므로 저장함
				sel = "Y";
				
				// 추가모드로 불러와짐
				DataTable dtLine = grd라인.GetCheckedRows("CHK");
				dtLine.AcceptChanges();

				// 수정모드로 변경
				foreach (DataRow row in dtLine.Rows)
					row.SetModified();

				string lineXml = GetTo.Xml(dtLine, "", "NO_FILE", "CD_PARTNER", "NO_LINE", "CHK");
				DBMgr.ExecuteNonQuery("PX_CZ_PU_INQ_PRT_OPTION", DebugMode.Print, lineXml);
			}

			if (chk계약단가제외.Checked)
			{
				// 빈값이면 All, 체크되어 있으면 EXT 값이 N인 것만
				ext = "N";
			}

			// ********** 인쇄
			try
			{
				bool boLive = false;

				if (LoginInfo.EmployeeNo != "S-343")
					boLive = true;

				string url = (boLive ? "http://erp.dintec.co.kr/" : "http://localhost/erp/") + "WebService/ViewerConverter.asmx/InquiryTo" + headRow["CD_PRINT"];



				//if (ClientRepository.DatabaseCallType == "Direct")
				//	return false;
				//else
				//	return true;




				// Json 요청
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";

				using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
				{
					// 옵션 Json
					Dictionary<string, string> optionDict = new Dictionary<string, string>
					{
						{ "ext" , ext }
					,   { "sel" , sel }
					,   { "ren" , option == null ? "" : option.PartnerName }
					,   { "lang", (string)headRow["CD_AREA"] == "100" ? "ko" : "en" }
					};

					// 전체 Json
					Dictionary<string, string> paramDict = new Dictionary<string, string>
					{
						{ "co"      , companyCode }
					,   { "fn"      , fileNumber }
					,   { "su"      , partnerCode }
					,   { "oJson"   , Json.Serialize(optionDict) }
					};

					stream.Write(Json.Serialize(paramDict));
					stream.Flush();
					stream.Close();
				}

				// Json 응답
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Dictionary<string, string> resultDict;

				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					string resultJson = reader.ReadToEnd();
					resultDict = Json.Deserialize(resultJson);
				}

				// 파일 저장
				if (resultDict["result"] == "success")
				{
					string download = resultDict["download"];
					string extension = Path.GetExtension(download).Replace(".", "");
					string fileName = string.Format("{0}_{1}_PINQ_{2}.{3}", fileNumber, partnerCode, headRow["DT_INQ"], extension);

					// 웹서버에서 파일 다운로드
					WebClient client = new WebClient();
					client.DownloadFile(download, FileMgr.GetTempPath() + fileName);

					// 워크플로우에 업로드
					string uploadName = FileMgr.Upload_WF(companyCode, fileNumber, fileName, false);

					// 그리드 저장
					if (grd헤드.DataTable != null)
						grd헤드.DataTable.Select("CD_PARTNER = '" + partnerCode + "'")[0]["NM_FILE"] = uploadName;
					
					// ********** 워크플로우 완료처리
					WorkFlow wf = new WorkFlow(fileNumber, "02", GetTo.Int(headRow["NO_HST"]));
					wf.AddItem("", partnerCode, fileName, uploadName);
					wf.Save();

					// ********** 기획실 견적 처리
					SQL sqlDx = new SQL("PS_CZ_PU_INQ_REG_L", SQLType.Procedure, SQLDebug.Print);
					sqlDx.Parameter.Add2("@CD_COMPANY"	, companyCode);
					sqlDx.Parameter.Add2("@NO_FILE"		, fileNumber);
					sqlDx.Parameter.Add2("@CD_PARTNER"	, partnerCode);

					DataTable dtDx = sqlDx.GetDataTable();
					UT.ExtractCode(dtDx);

					SQL sql = new SQL("PX_CZ_PU_INQ_DX", SQLType.Procedure, SQLDebug.Print);
					sql.Parameter.Add2("@CD_COMPANY", companyCode);
					sql.Parameter.Add2("@NO_FILE"	, fileNumber);
					sql.Parameter.Add2("@CD_PARTNER", partnerCode);
					sql.Parameter.Add2("@XML"		, dtDx.ToXml("NO_LINE", "ITEM_CODE", "SUBJ_CODE"));
					sql.ExecuteNonQuery();

					// 실행
					if (run)
						System.Diagnostics.Process.Start(FileMgr.GetTempPath() + fileName);
				}
				else
				{
					throw new Exception(resultDict["error"]);
				}
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}

			return "";
		}

		

		#endregion
	}
}


