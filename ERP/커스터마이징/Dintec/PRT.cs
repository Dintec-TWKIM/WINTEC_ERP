using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Dass.FlexGrid;
using Duzon.Common.Forms;

namespace Dintec
{
	public static class PRT
	{
		public static string PInq(string companyCode, string fileNumber, string partnerCode, FlexGrid flex, /*H_CZ_PRT_OPTION option,*/ bool run)
		{
			DataRow headRow = DBMgr.GetDataTable("PS_CZ_PU_INQ_RPT_H", companyCode, fileNumber, partnerCode).Rows[0];

			// ********** 옵션
			string sel = "N";
			string ext = "";

			//if (option != null && option.SelItem)
			//{
			//	// 요건 웹으로 전달할 방법이 없으므로 저장함
			//	sel = "Y";

			//	// 추가모드로 불러와짐
			//	DataTable dtLine = grd라인.GetCheckedRows("CHK");
			//	dtLine.AcceptChanges();

			//	// 수정모드로 변경
			//	foreach (DataRow row in dtLine.Rows)
			//		row.SetModified();

			//	string lineXml = GetTo.Xml(dtLine, "", "NO_FILE", "CD_PARTNER", "NO_LINE", "CHK");
			//	DBMgr.ExecuteNonQuery("PX_CZ_PU_INQ_PRT_OPTION", DebugMode.Print, lineXml);
			//}

			//if (chk계약단가제외.Checked)
			//{
			//	// 빈값이면 All, 체크되어 있으면 EXT 값이 N인 것만
			//	ext = "N";
			//}

			// ********** 인쇄
			try
			{
				bool boLive = true;

				if (Global.MainFrame.LoginInfo.EmployeeNo != "S-343")
					boLive = true;

				string url = (boLive ? "http://erp.dintec.co.kr/" : "http://localhost/erp/") + "WebService/ViewerConverter.asmx/InquiryTo" + headRow["CD_PRINT"];

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
					//,   { "ren" , option == null ? "" : option.PartnerName }
					,   { "ren" , "" }
					,   { "lang", (string)headRow["CD_AREA"] == "100" ? "ko" : "en" }
					};

					// 전체 Json
					Dictionary<string, string> paramDict = new Dictionary<string, string>
					{
						{ "co"      , companyCode }
					,   { "fn"      , fileNumber }
					,   { "su"      , partnerCode }
					//,   { "oJson"   , Json.Serialize(optionDict) }
					,   { "oJson"   , "" }
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
					//if (grd헤드.DataTable != null)
					//	grd헤드.DataTable.Select("CD_PARTNER = '" + partnerCode + "'")[0]["NM_FILE"] = uploadName;
					
					// 완료처리
					WorkFlow wf = new WorkFlow(fileNumber, "02", GetTo.Int(headRow["NO_HST"]));
					wf.CD_COMPANY = companyCode;
					wf.AddItem("", partnerCode, fileName, uploadName);
					wf.Save();

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
				//ShowMessage(ex.Message);
			}

			return "";
		}
	}
}
