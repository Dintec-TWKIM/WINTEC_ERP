using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using Dintec;
using Duzon.Common.Forms;
using System.Windows.Forms;
using System.IO;
using GemBox.Spreadsheet;

namespace DX
{
	public class LABEL
	{
		public static bool 선사라벨데이터생성(string 파일번호)
		{
			// 파일번호에 해당하는 선사
			string query = "SELECT CD_PARTNER, NO_PO_PARTNER FROM SA_SOH WITH(NOLOCK) WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND NO_SO = '" + 파일번호 + "'";
			DataTable dtSo = SQL.GetDataTable(query);

			if (dtSo.Rows.Count == 0)
				throw new Exception("수주 정보가 없습니다.");

			string 선사코드 = dtSo.Rows[0]["CD_PARTNER"].ToStr();
			string 오더번호 = dtSo.Rows[0]["NO_PO_PARTNER"].ToStr();
			DataTable 코드관리 = CODE.코드관리("CZ_DX00019");

			if (코드관리.Select("CODE = '" + 선사코드 + "'").Length == 0)
				throw new Exception("선사라벨을 발행하는 업체가 아닙니다.");

			//***************************************************************
			string url = "https://api.klcsm.co.kr/v1/QR";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Headers.Add("OrderNo", 오더번호);

			//HttpWebResponse 객체 받아옴
			HttpWebResponse wRes = (HttpWebResponse)request.GetResponse();
			Stream respGetStream = wRes.GetResponseStream();
			StreamReader readerGet = new StreamReader(respGetStream, Encoding.UTF8);

			// 생성한 스트림으로부터 string으로 변환합니다.
			string jsonStr = readerGet.ReadToEnd();

			// ********** DB 저장
			DataTable dt = JSON.Deserialize<DataTable>(jsonStr);
			dt.Columns.Add("NO_SO").SetOrdinal(0);
			dt.Columns.Add("SEQ").SetOrdinal(1);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dt.Rows[i]["NO_SO"] = 파일번호;
				dt.Rows[i]["SEQ"] = i + 1;
			}

			// 컬럼 이름을 대문자로 통일
			for (int j = 0; j < dt.Columns.Count; j++)
			{
				dt.Columns[j].ColumnName = dt.Columns[j].ColumnName.ToUpper();
			}

			if (dt.Columns.Contains("PARTDESC"))
				dt.Columns["PARTDESC"].ColumnName = "PART_DESC";

			// DB 저장
			SQL sql = new SQL("PX_CZ_SA_SOL_LABEL", SQLType.Procedure, SQLDebug.Print);
			sql.Parameter.Add2("@XML", dt.ToXml());
			sql.ExecuteNonQuery();

			return true;
		}

		public static string 매입처라벨(string 발주번호)
		{
			DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_PO_LABEL_H", Global.MainFrame.LoginInfo.CompanyCode, 발주번호);
			DataTable dtLine = SQL.GetDataTable("PS_CZ_PU_PO_LABEL_L", Global.MainFrame.LoginInfo.CompanyCode, 발주번호);
			DataTable 코드관리 = CODE.코드관리("CZ_DX00019");
			string 파일이름;

			if (코드관리.Select("CODE = '" + dtHead.Rows[0]["CD_BUYER"] + "'").Length > 0)
				파일이름 = 매입처라벨_SM그룹(dtHead, dtLine);
			else
				파일이름 = 매입처라벨(dtHead, dtLine);

			return 파일이름;
		}

		public static string 매입처라벨(DataTable dtHead, DataTable dtLine)
		{
			string 저장경로 = Application.StartupPath + @"\temp\";

			// ********** 스타일시트
			string 헤드 = @"
  <style type='text/css'>
    table   { table-layout:fixed; }
    td, div { font-family:맑은 고딕; font-size:11pt; letter-spacing:-.5px; }
      
    .page       { margin:0 auto; margin-top:25px; }
    .cell       { width:480px; height:347px; padding:30px; box-sizing:border-box; }
    .label-wrap { width:100%; height:100%; padding:5px; box-sizing:border-box; border:1px solid #000000; }
    
    .label-qr       { width:100%; }
    .label-qr table { width:100%; }
    .label-qr .col1 { width:30%; }
    .label-qr .col2 { width:46%; }
    .label-qr .col3 { width:24%; }

    .label-qr .logo-dintec img  { height:45px; }
    .label-qr .logo-dubheco img { height:30px; width:115px; }
    .label-qr .logo-dubheco     { margin-bottom:10px; }

    .label-qr .company     { font-size:17pt; font-weight:bold; }
    .label-qr .disp-num    { font-size:18pt; font-weight:bold; }
    .label-qr .qr-code     { text-align:right; }
    .label-qr .qr-code img { width:75px; height:75px; }

    .label-ct       { width:100%; margin-top:3px; }
    .label-ct table { width:100%; }
    .label-ct tr    { height:20px; }
    .label-ct .col1 { width:30%; }
    .label-ct .col2 { width:2%; }
    .label-ct .col3 { width:48%; }
    .label-ct .col4 { width:20%; }

    .label-ct td    { white-space:nowrap; overflow:hidden; }
    .label-ct .desc { height:55px; line-height:18px; white-space:normal; overflow:hidden; }

    .page-break { page-break-after:always; }

    /*
    .page        { border:solid 1px green; }
    .cell        { border:solid 1px red; }
    .label-qr    { border:solid 1px red; }
    .label-qr td { border:solid 1px red; }
    .label-ct    { border:solid 1px red; }
    .label-ct td { border:solid 1px blue; }
    */
  </style>";

			// Html 바디
			string 바디 = "";
			int pageNum = 1;	// 시작 페이지 넘버
			int pageSize = 8;   // 한페이지당 라벨 갯수

			while (true)
			{
				if ((pageNum - 1) * pageSize >= dtLine.Rows.Count)
					break;

				// 라벨 갯수만큼 신규 테이블 만들기
				DataTable dtPage = dtLine.Rows.Cast<DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
				pageNum++;

				// Html GoGo!!
				바디 += @"
  <div>
    <table class='page'>";

				for (int i = 0; i < dtPage.Rows.Count; i++)
				{
					// ********** QR코드 이미지 생성 (대신 pda에서 사용하기 위해 @대신 \n을 씀)
					string QR코드 = ""
+ "QTY:" + string.Format("{0:###0.##}", dtPage.Rows[i]["QT_PO"]) + "\n"
+ "C/CODE:" + dtHead.Rows[0]["CD_COMPANY"] + "\n"
+ "D/CODE:" + dtHead.Rows[0]["NO_PO"] + "/" + dtPage.Rows[i]["NO_LINE"] + "/S";	// S:Supplier

					string QR파일 = dtHead.Rows[0]["NO_PO"] + "_" + dtPage.Rows[i]["NO_LINE"] + ".png";
					QR.이미지_데이터매트릭스(QR코드.Trim(), 저장경로 + QR파일);

					// ********** 라벨
					string companyName = "";
					string logoHtml = "";

					if (dtHead.Rows[0]["CD_COMPANY"].ToStr() == "K100")
					{
						companyName = "DINTEC CO., LTD";
						logoHtml = "<div class='logo-dintec'><img src='http://www.dintec.co.kr/common/img/common/logo.png' /></div>";
					}
					else if (dtHead.Rows[0]["CD_COMPANY"].ToStr() == "K200")
					{
						companyName = "DUBHE CO., LTD";
						logoHtml = "<div class='logo-dubheco'><img src='http://www.dintec.co.kr/common/img/common/logo_dubheco3.png' /></div>";
					}

					// 기자재인 경우 Subject 추가, 선용은 Offer 추가
					string 주제 = "";
					string 오퍼 = "";
					bool 기자재여부 = CODE.코드관리("CZ_SA00023").Select("CODE = '" + dtHead.Rows[0]["NO_FILE"].Left(2) + "'")[0]["CD_FLAG2"].ToStr() != "GS";

					if (기자재여부)
					{
						주제 = @"
                <tr>
                  <td>SUBJECT NAME</td>
                  <td>:</td>
                  <td colspan='2'>" + HTML.인코딩(dtPage.Rows[i]["NM_SUBJECT"]) + @"</td>
                </tr>";
					}
					else if (!기자재여부 && dtPage.Rows[i]["YN_DSP_RMK"].ToString() == "Y")
					{
						오퍼 = @"
                <tr>
                  <td>OFFER</td>
                  <td>:</td>
                  <td colspan='2'>" + HTML.인코딩(dtPage.Rows[i]["DC_RMK"]) + @"</td>
                </tr>";
					}

					// 라벨 본문
					string 라벨 = @"
          <div class='label-wrap'>
            <div class='label-qr'>
              <table>
                <colgroup>
                  <col class='col1' />
                  <col class='col2' />
                  <col class='col3' />
                </colgroup>
                <tr>
                  <td class='logo'>" + logoHtml + @"</td>
                  <td class='company'>" + companyName + @"</td>
                  <td class='qr-code' rowspan='2'><img src='file:///" + 저장경로.Replace(@"\", "/") + QR파일 + @"' /></td>
                </tr>
                <tr>
                  <td class='disp-num' colspan='2'>No. " + string.Format("{0:###0.##}", dtPage.Rows[i]["NO_DSP"]) + @"</td>
                </tr>
              </table>
            </div>

            <div class='label-ct'>
              <table>
                <colgroup>
                  <col class='col1' />
                  <col class='col2' />
                  <col class='col3' />
                  <col class='col4' />
                </colgroup>
                <tr>
                  <td>YOUR ORDER NO.</td>
                  <td>:</td>
                  <td colspan='2'>" + HTML.인코딩(dtHead.Rows[0]["NO_PO_BUYER"]) + @"</td>
                </tr>
                <tr>
                  <td>OUR REF NO.</td>
                  <td>:</td>
                  <td colspan='2'>" + HTML.인코딩(dtHead.Rows[0]["NO_FILE"]) + @"</td>
                </tr>
                <tr>
                  <td>BUYER</td>
                  <td>:</td>
                  <td colspan='2'>" + HTML.인코딩(dtHead.Rows[0]["NM_BUYER"]) + @"</td>
                </tr>
                <tr>
                  <td>VESSEL</td>
                  <td>:</td>
                  <td colspan='2'>" + HTML.인코딩(dtHead.Rows[0]["NM_VESSEL"]) + @"</td>
                </tr>" + 주제 + @"
                <tr>
                  <td>ITEM CODE</td>
                  <td>:</td>
                  <td>" + HTML.인코딩(dtPage.Rows[i]["CD_ITEM_PARTNER"]) + @"</td>
                  <td>QTY : " + string.Format("{0:#,##0}", dtPage.Rows[i]["QT_PO"]) + @"</td>
                </tr>
                <tr>
                  <td class='desc'>ITEM NAME<br />&nbsp;<br />&nbsp;</td>
                  <td class='desc'>:<br />&nbsp;<br />&nbsp;</td>
                  <td colspan='2'><div class='desc'>" + HTML.인코딩(dtPage.Rows[i]["NM_ITEM_PARTNER"]) + @"</div></td>
                </tr>" + 오퍼 + @"
              </table>
            </div>
          </div>";
					
					// ********** 셀 (라벨 부모)
					if (i % 2 == 0)
					{
						// 짝수행(0,2,4)이면 여는 태그
						바디 += @"
      <tr>
        <td class='cell'>" + 라벨 + @"
        </td>";

						if (i == dtPage.Rows.Count - 1)
						{
							// 짝수행이 마지막으로 끝나면 빈값 홀수행 생성
							바디 += @"
        <td class='cell'>
        </td>
      </tr>";
						}
					}
					else
					{
						// 홀수행(1,3,5)이면 닫는 태그
						바디 += @"
        <td class='cell'>" + 라벨 + @"
        </td>
      </tr>";
					}
				}

				바디 += @"
    </table>
  </div>
  <div class='page-break'></div>
";
			}

			// ********** 파일 만들기
			UTIL.작업중("라벨 저장중입니다. \r\n잠시만 기다려주세요!");
			
			// Html, Pdf 파일 만들기
			string 파일이름 = string.Format("라벨_{0}_{1}", dtHead.Rows[0]["NO_PO"], dtHead.Rows[0]["CD_PARTNER"]);
			string html = HTML.만들기(헤드, 바디);
			HTML.저장(html, 저장경로 + 파일이름 + ".html");
			HTML.저장(html, 저장경로 + 파일이름 + ".pdf");


			//HTML.저장(html, 저장경로 + 파일이름 + ".xlsx");
			//SpreadsheetInfo.SetLicense("EAAN-UCCU-1F8C-X668");
			//ExcelFile.Load(저장경로 + 파일이름 + ".html").Save(파일이름 + ".xlsx");


			UTIL.작업중();

			return 저장경로 + 파일이름 + ".pdf";
		}

		public static string 매입처라벨_SM그룹(DataTable dtHead, DataTable dtLine)
		{
			string 저장경로 = Application.StartupPath + @"\temp\";

			// ********** 스타일시트
			string 헤드 = @"
  <style type='text/css'>
    table   { table-layout:fixed; }
    td, div { font-family:맑은 고딕; font-size:12pt; letter-spacing:-.5px; }
  
    .page  { margin:0 auto; margin-top:25px; }
    .cell  { width:480px; height:347px; padding:30px; box-sizing:border-box; }
  
    .label-wrap { position:relative; width:100%; height:100%; border:1px solid #000000; }
    .label-sm   { position:absolute; top:5px; left:5px; }
    .label-di   { position:absolute; bottom:5px; left:5px; }

    .label       { width:calc(100% - 10px); box-sizing:border-box; }
    .label table { width:100%; }
    .label tr    { height:25px; }		/* 행높이랑 line-heigt랑 비슷하게 */
    .label td    { line-height:20px; }
    .label .col1 { width:20%; }
    .label .col2 { width:80%; }

    .label .qr-code img    { width:70px; height:70px; }
    .label .qr-text        { padding-left:10px; max-width:10px; white-space:nowrap; overflow:hidden; }	/* max-width를 넣어줘야 overflow가 %길이일때 동작함 */
    .label-sm .qr-code img { border:1px solid #000000; padding:3px; }
    .label-di .qr-code     { text-align:right; }

    .page-break { page-break-after:always; }

    /*
    .cell     { border:solid 1px red; }
    .label    { border:solid 1px red; }
    .label td { border:solid 1px blue; }
    */
  </style>";

			// ********** 바디
			string 바디 = "";
			int pageNum = 1;	// 시작 페이지 넘버
			int pageSize = 8;	// 한페이지당 라벨 갯수

			while (true)
			{
				if ((pageNum - 1) * pageSize >= dtLine.Rows.Count)
					break;

				// 라벨 갯수만큼 신규 테이블 만들기
				DataTable pageTable = dtLine.Rows.Cast<DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
				pageNum++;

				// Html GoGo!!
				바디 += @"
  <div>
    <table class='page'>";

				for (int i = 0; i < pageTable.Rows.Count; i++)
				{
					// ********** QR코드 이미지 생성 (SM그룹용)
					string QR코드SM = pageTable.Rows[i]["QR"].ToStr();
					string QR파일SM = dtHead.Rows[0]["NO_PO"] + "_" + pageTable.Rows[i]["NO_LINE"] + "_SM.png";
					QR.이미지_QR코드(QR코드SM.Trim(), 저장경로 + QR파일SM);

					// ********** QR코드 이미지 생성 (우리꺼, 대신 pda에서 사용하기 위해 @대신 \n을 씀)
					string QR코드DI = ""
+ "QTY:" + string.Format("{0:###0.##}", pageTable.Rows[i]["QT_PO"]) + "\n"
+ "C/CODE:" + dtHead.Rows[0]["CD_COMPANY"] + "\n"
+ "D/CODE:" + dtHead.Rows[0]["NO_PO"] + "/" + pageTable.Rows[i]["NO_LINE"] + "/S";	// S:Supplier

					string QR파일DI = dtHead.Rows[0]["NO_PO"] + "_" + pageTable.Rows[i]["NO_LINE"] + "_DI.png";
					QR.이미지_데이터매트릭스(QR코드DI.Trim(), 저장경로 + QR파일DI);

					// ********** 라벨 본문
					string 라벨 = @"
          <div class='label-wrap'>
            <div class='label label-sm'>
              <table>
                <colgroup>
                  <col class='col1' />
                  <col class='col2' />
                </colgroup>
                <tr>
                  <td class='qr-code' rowspan='3'><img src='file:///" + 저장경로.Replace(@"\", "/") + QR파일SM + @"' /></td>
                  <td class='qr-text'>Eq: " + HTML.인코딩(pageTable.Rows[i]["EQ"]) + @"</td>
                </tr>
                <tr>
                  <td class='qr-text'>Part No: " + HTML.인코딩(pageTable.Rows[i]["PART"]) + @"</td>
                </tr>
                <tr>
                  <td class='qr-text'>Location: " + HTML.인코딩(pageTable.Rows[i]["LOCATION"]) + @"</td>
                </tr>
                <tr>
                  <td colspan='2'></td>
                </tr>
                <tr>
                  <td colspan='2'>" + HTML.인코딩(pageTable.Rows[i]["PART_DESC"]) + @"</td>
                </tr>
              </table>
            </div>
		    
            <div class='label label-di'>
              <table>
                <colgroup>
                  <col class='col2' />
                  <col class='col1' />
                </colgroup>
                <tr>
                  <td></td>
                  <td class='qr-code' rowspan='3'><img src='file:///" + 저장경로.Replace(@"\", "/") + QR파일DI + @"' /></td>
                </tr>
                <tr>
                  <td>" + "No." + string.Format("{0:###0.##}/", pageTable.Rows[i]["NO_DSP"]) + HTML.인코딩(dtHead.Rows[0]["NO_FILE"]) + "/QTY:" + string.Format("{0:#,##0}", pageTable.Rows[i]["QT_PO"])  + @"</td>
                </tr>
                <tr>
                  <td>" + HTML.인코딩(dtHead.Rows[0]["NM_VESSEL"]) + @"</td>
                </tr>
              </table>
            </div>
          </div>";

					// ********** 셀 (라벨 부모)
					if (i % 2 == 0)
					{
						// 짝수행(0,2,4)이면 여는 태그
						바디 += @"
      <tr>
        <td class='cell'>" + 라벨 + @"
        </td>";

						if (i == pageTable.Rows.Count - 1)
						{
							// 짝수행이 마지막으로 끝나면 빈값 홀수행 생성
							바디 += @"
        <td class='cell'>
        </td>
      </tr>";
						}
					}
					else
					{
						// 홀수행(1,3,5)이면 닫는 태그
						바디 += @"
        <td class='cell'>" + 라벨 + @"
        </td>
      </tr>";
					}
				}

				바디 += @"
    </table>
  </div>
  <div class='page-break'></div>";
			}

			// ********** 파일 만들기
			UTIL.작업중("라벨 저장중입니다. \r\n잠시만 기다려주세요!");

			// Html, Pdf 파일 만들기
			string 파일이름 = string.Format("라벨_{0}_{1}", dtHead.Rows[0]["NO_PO"], dtHead.Rows[0]["CD_PARTNER"]);
			string html = HTML.만들기(헤드, 바디);
			HTML.저장(html, 저장경로 + 파일이름 + ".htm");
			HTML.저장(html, 저장경로 + 파일이름 + ".pdf");
			UTIL.작업중();

			return 저장경로 + 파일이름 + ".pdf";
		}
	}
}
