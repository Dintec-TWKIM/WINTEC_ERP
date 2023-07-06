using Aspose.Email.Outlook;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Parsing
{
    public class AutoInquiry
    {
		string 임시폴더;

		public AutoInquiry()
		{
			임시폴더 = "temp";

			// 라이선스 인증
			Aspose.Email.License license = new Aspose.Email.License();
			license.SetLicense("Aspose.Email.lic");
		}

		/// <summary>
		/// 호출 예시
		/// 고객문의서 : SaveEmail("K100", string.Empty, "FB", "01", "S-391", string.Empty, "파일경로입력")
		/// 매입견적 : SaveEmail("K100", "파일번호입력", string.Empty, "04", string.Empty, "매입처코드입력", "파일경로입력")
		/// 고객발주서 : SaveEmail("K100", "파일번호입력", string.Empty, "08", string.Empty, string.Empty, "파일경로입력")
		/// </summary>
		/// <param name="회사코드"></param>
		/// <param name="파일번호"></param>
		/// <param name="영업파일구분"></param>
		/// <param name="단계"></param>
		/// <param name="영업담당자"></param>
		/// <param name="매입처코드"></param>
		/// <param name="파일경로"></param>
		/// <returns></returns>
		public string SaveEmail(string 회사코드, string 파일번호, string 영업파일구분, string 단계, string 영업담당자, string 매입처코드, string 파일경로)
		{
			FileInfo 파일정보;
			List<string> imageList;
			string[] separator, temp;
			string image1, fileName, localpath;
			string 입력담당자, 구매담당자, 영업물류담당자;

			try
			{
				if (단계 != "01" && 단계 != "04" && 단계 != "08" && 단계 != "50" && 단계 != "51")
				{
					//Util.ShowMessage("해당 단계는 지원하지 않습니다.");
					return string.Empty;
				}
				else if (단계 == "04" && string.IsNullOrEmpty(매입처코드)) // 매입견적 
				{
					//Util.ShowMessage("매입처코드가 지정되지 않았습니다.");
					return string.Empty;
				}
				
				#region 선사 웹에서 파일 다운로드
				Dintec.AutoMail.MailSorter ms = new Dintec.AutoMail.MailSorter(파일경로);

				if (ms.Sort())
				{
					Dintec.AutoWeb.AutoWeb autoWeb = new Dintec.AutoWeb.AutoWeb();
					autoWeb.OutlookFile = 파일경로;
					파일경로 = autoWeb.DownloadInquiryWithMessage(false);

					if (string.IsNullOrEmpty(파일경로))
					{
						return string.Empty;
					}
				}
				#endregion

				파일정보 = new FileInfo(파일경로);

				#region Header
				구매담당자 = string.Empty;
				영업물류담당자 = string.Empty;

				if (단계 == "01") // 고객문의서
				{
					if(string.IsNullOrEmpty(파일번호))
						파일번호 = (string)Global.MainFrame.GetSeq(회사코드, "CZ", 영업파일구분, Global.MainFrame.GetStringToday.Substring(2, 2));
					
					입력담당자 = this.입력담당자자동지정(회사코드, 영업담당자);
				}
				else
				{
					if (string.IsNullOrEmpty(파일번호))
					{
						//Util.ShowMessage("파일번호가 지정되지 않았습니다.");
						return string.Empty;
					}

					DataTable dt = DBHelper.GetDataTable(@"SELECT TP_SALES,
																  ID_SALES,
																  ID_TYPIST
														   FROM CZ_MA_WORKFLOWH WH WITH(NOLOCK)
														   WHERE WH.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
														 @"AND WH.TP_STEP = '01'
														   AND WH.NO_KEY = '" + 파일번호 + "'");
					
					if (dt != null && dt.Rows.Count == 1)
					{
						영업파일구분 = dt.Rows[0]["TP_SALES"].ToString();
						영업담당자 = dt.Rows[0]["ID_SALES"].ToString();
						입력담당자 = dt.Rows[0]["ID_TYPIST"].ToString();
					}
					else
					{
						//Util.ShowMessage("고객문의서가 등록되지 않았습니다.");
						return string.Empty;
					}

					if (단계 == "08" || 단계 == "50" || 단계 == "51") // 고객발주서
					{
						dt = DBHelper.GetDataTable(@"SELECT ID_PUR_DEF,
							   								ID_LOG_DEF
													 FROM CZ_SA_USER
													 WHERE CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
													"AND ID_USER = '" + 영업담당자 + "'");

						if (dt != null && dt.Rows.Count == 1)
						{
							구매담당자 = dt.Rows[0]["ID_PUR_DEF"].ToString();
							영업물류담당자 = dt.Rows[0]["ID_LOG_DEF"].ToString();
						}
						else
						{
							//Util.ShowMessage("등록되지 않은 영업담당자 입니다.");
							return string.Empty;
						}
					}
				}

				DBHelper.ExecuteNonQuery("SP_CZ_SA_INQ_REGH_I", new object[] { 회사코드,
																			   단계,
																			   파일번호,
																			   영업파일구분,
																			   영업담당자,
																			   입력담당자,
																			   구매담당자,
																			   영업물류담당자,
																			   "자동생성건",
																			   "SYSTEM" });
				#endregion

				#region Line
				this.AddDataRow(회사코드, 단계, 파일번호, 매입처코드, 파일경로, 파일정보.Name, 파일정보, false);

				if (파일정보.Extension == ".msg")
				{
					#region MSG
					MapiMessage msg = MapiMessage.FromFile(파일경로);

					#region 본문 이미지 제거
					separator = new string[] { "cid:" };
					temp = msg.BodyHtml.Split(separator, StringSplitOptions.None);

					temp[0] = string.Empty;
					imageList = new List<string>();

					foreach (string image in temp)
					{
						if (string.IsNullOrEmpty(image))
							continue;

						image1 = image.Split('.')[0];

						if (!imageList.Contains(image1))
							imageList.Add(image1);
					}
					#endregion

					string filePath = Path.Combine(Application.StartupPath, this.임시폴더) + "\\";
					FileMgr.CreateDirectory(filePath);

					foreach (MapiAttachment item in msg.Attachments)
					{
						fileName = Regex.Replace(item.LongFileName, @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ .]", string.Empty);
						FileInfo fileInfo = new FileInfo(fileName);

						if (imageList.Contains(fileInfo.Name.Replace(fileInfo.Extension, string.Empty))) continue;

						fileName = FileMgr.GetUniqueFileName(Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + fileName);
						localpath = Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + fileName;
						item.Save(localpath);

						this.AddDataRow(회사코드, 단계, 파일번호, 매입처코드, localpath, item.LongFileName, new FileInfo(localpath), true);
					}
					#endregion
				}
				#endregion

				return 파일번호;
			}
			catch (Exception ex)
			{
				return null;
			}
			finally
			{
				this.임시파일제거();
			}
		}

		private void AddDataRow(string 회사코드, string 단계, string 파일번호, string 매입처코드, string 파일경로, string 원본파일이름, FileInfo 파일정보, bool 첨부여부)
		{
			string 파싱가능여부 = "N";

			try
			{
				#region 파싱가능여부
				if (단계 == "01" && 첨부여부 == false)
				{
					try
					{
						InquiryParser parser = new InquiryParser(파일경로);

						if (parser.Parse(false) == true)
							파싱가능여부 = "Y";
						else
							파싱가능여부 = "N";
					}
					catch
					{
						파싱가능여부 = "N";
					}
				}
				else if (단계 == "04" && 첨부여부 == false)
				{
					try
					{
						if (QuotationFinder.IsPossible(회사코드, 매입처코드, 파일정보.FullName) == true)
							파싱가능여부 = "Y";
						else
							파싱가능여부 = "N";
					}
					catch
					{
						파싱가능여부 = "N";
					}
				}
				#endregion

				DBHelper.ExecuteNonQuery("SP_CZ_SA_INQ_REGL_I", new object[] { 회사코드,
																			   단계,
																			   파일번호,
																			   원본파일이름,
																			   FileMgr.Upload_WF(회사코드, 파일번호, 파일정보.FullName, false),
																			   매입처코드,
																			   파싱가능여부,
																			   (첨부여부 == true ? "Y" : "N"), 
																			   "SYSTEM" });
			}
			catch (Exception ex)
			{
				return;
			}
		}

		public string 입력담당자자동지정(string 회사코드, string 영업담당자)
		{
			DataTable dt, dt1;
			DataRow[] dataRowArray;

			try
			{
				dt = this.입력지원자동할당(new object[] { 회사코드,
														  영업담당자 });


				dt1 = dt.DefaultView.ToTable(true, "ID_USER", "QT_PROGRESS", "YN_AUTO");

				dataRowArray = dt1.Select("YN_AUTO" + " = 'Y'", "QT_PROGRESS ASC");

				if (dataRowArray.Length > 0)
					return D.GetString(dataRowArray[0]["ID_USER"]);
				else
					return string.Empty;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		public DataTable 입력지원자동할당(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_REGA_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		private void 임시파일제거()
		{
			DirectoryInfo dirInfo;
			bool isExistFile;

			try
			{
				dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, this.임시폴더));
				isExistFile = false;

				if (dirInfo.Exists == true)
				{
					foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
					{
						try
						{
							file.Delete();
						}
						catch
						{
							isExistFile = true;
							continue;
						}
					}

					if (isExistFile == false)
						dirInfo.Delete(true);
				}
			}
			catch (Exception ex)
			{
				return;
			}
		}
	}
}
