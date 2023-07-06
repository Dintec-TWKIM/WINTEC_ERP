using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	public class MidEast
	{		
		string reference;
		string vessel;
		DataTable dtItem;

		string fileName;
		UnitConverter uc;

		#region ==================================================================================================== Property
		
		public string Reference
		{
			get
			{
				return reference;
			}
		}

		public string Vessel
		{
			get
			{
				return vessel;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public MidEast(string fileName)
		{
			vessel = "";
			reference = "";
			
			dtItem = new DataTable();
			dtItem.Columns.Add("NO");
			dtItem.Columns.Add("SUBJ");
			dtItem.Columns.Add("ITEM");
			dtItem.Columns.Add("DESC");
			dtItem.Columns.Add("UNIT");
			dtItem.Columns.Add("QT");
            dtItem.Columns.Add("UNIQ");         //선사코드

			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion

		#region ==================================================================================================== Logic		

		public void Parse()
		{
			// Pdf를 Xml로 변환해서 분석 (ByteScout로 하면 아이템헤더 Col과 아이템바디 Col이 다른 경우 발생해서 안됨)
			string xml = PdfReader.ToXml(fileName);
			DataSet ds = PdfReader.ToDataSet(xml);


            //DataSet Table 병합을 위한 Table
            DataTable dsAll = new DataTable();

            //DataSet Table의 Count Get
            int dsCount = ds.Tables.Count;

            for (int i = 0; i <= dsCount - 1; i++)
            {
                dsAll.Merge(ds.Tables[i]);
            }

            ds.Clear();
            ds.Tables.Add(dsAll);


			// 시작
			string subject = "";
			bool addible   = false;

			int noCol   = -1;
			int itemCol = -1;
			int descCol = -1;
			int unitCol = -1;
			int qtCol   = -1;

            string itemCode = string.Empty;
            string itemDesc = string.Empty;
			string subjStr = string.Empty;

            string dwgDesc = string.Empty;

		
			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColString = dt.Rows[i][0].ToString();

					if (firstColString == "No.")    // 아이템 추가 모드
					{
						addible = true;
					}
					else if (firstColString.StartsWith("Terms and Conditions:")) 
						addible = false;
					else if (firstColString.StartsWith("Special Instructions:"))
					{
						subjStr = firstColString.Trim();

						int _i = i + 1;

						while (!dt.Rows[_i][0].ToString().StartsWith("Date") && !dt.Rows[_i][0].ToString().StartsWith("Terms and"))
						{
							subjStr += dt.Rows[_i][0].ToString().Trim() + Environment.NewLine;

							_i += 1;

							if (_i > dt.Rows.Count)
								break;
						}
					}

					// ********** 문서 검색 모드
					if (!addible)
					{
						// ***** 문의번호
						if (firstColString == "Enquiry Ref.")
						{
							reference = dt.Rows[i][1].ToString();
						}
						// ***** 선명
						else if (firstColString == "Vessel")
						{
							vessel = dt.Rows[i][1].ToString();
						}
						// ***** 서브젝트 1
						else if (firstColString == "Order Title" || firstColString == "Equipment Name")
						{
							for (int j = 1; j < dt.Columns.Count; j++)
							{
								string text = dt.Rows[i][j].ToString().Trim();
								if (text != "" && text != ":" && !text.ToUpper().Trim().Equals(subject.Replace(",","").ToUpper().Trim())) subject += ", " + text;
								subject = subject.Trim();
							}
						}					
						// ***** 서브젝트 2
						else if (firstColString == "Model" || firstColString == "Drawing No." || firstColString == "Serial No." || firstColString == "Manufacturer")
						{
							string subjectExt = "";

							for (int j = 1; j < dt.Columns.Count; j++)
							{
								string text = dt.Rows[i][j].ToString();
								if (text != "" && text != ":" && !text.Equals("-") && !text.Equals(subjectExt)) subjectExt += " " + text;
								subjectExt = subjectExt.Trim();
							}

							if (subjectExt != "") subject += "\r\n" + firstColString + " : " + subjectExt;
						}
					}
					// ********** 아이템 추가 모드
					else
					{
						// ***** 첫번째 글자가 No.(아이템 헤더) → 컬럼 위치 설정 (전체 문서에서 딱 한번 출현)
						if (firstColString == "No.")
						{
							noCol = 0;

							for (int j = 0; j < dt.Columns.Count; j++)
							{
								if (dt.Rows[i][j].ToString() == "Item Description") descCol = j;
								else if (dt.Rows[i][j].ToString() == "Part No.") itemCol = j;
								else if (dt.Rows[i][j].ToString() == "Qty.") qtCol = j;
								else if (dt.Rows[i][j].ToString() == "Unit") unitCol = j;
							}
						}
						
						// ***** 첫번째 글자가 숫자 → 아이템
						else if (!firstColString.Contains(".") && GetTo.IsInt(firstColString))
						{
							if (dt.Rows[i][0].ToString().StartsWith("Terms and Conditions:")) break;

                            subject = subject.Trim();
                            if (subject.StartsWith(","))
                                subject = subject.Substring(1, subject.Length - 1).Trim();

                            if (subject.EndsWith(","))
                                subject = subject.Substring(0, subject.Length - 1).Trim();

                            if (!itemCol.Equals(-1))
                            {
                                itemCode = dt.Rows[i][itemCol].ToString().Replace("na", "").Replace("NA", "").Replace("N/A", "").Trim();
                            }

                            subject = subject.Replace("Manufacturer", "MAKER").Replace("Drawing No.","DWG.NO.").Replace("Serial No.","S/NO.").Trim();


                            if (descCol != -1)
                            {
                                itemDesc = dt.Rows[i][descCol].ToString().Replace("Maker", "\r\nMAKER").Replace("Drawing", "\r\nDRAWING").Replace("Details", "\r\nDetails");

                                if (dt.Rows.Count > i + 1 && !GetTo.IsInt(dt.Rows[i + 1][0].ToString()) && string.IsNullOrEmpty(dt.Rows[i + 1][0].ToString()))
                                    dwgDesc = dt.Rows[i + 1][descCol].ToString().Trim();
                            }



                            if (dwgDesc.Contains("Drawing"))
                            {
                                int idx_lts = dwgDesc.IndexOf("Drawing");

                                dwgDesc = dwgDesc.Substring(idx_lts, dwgDesc.Length - idx_lts).Trim();
                                dwgDesc = dwgDesc.Replace("Drawing No", "").Replace(":", "").Trim();
                            }

                            if (dwgDesc.Contains("-") && subject.Contains("MAN B&W"))
							{
								if(!itemCode.Contains(dwgDesc))
									itemCode = dwgDesc.Trim() + "-" + itemCode.Trim();
								//else 
								//	itemDesc = itemDesc.Trim() + Environment.NewLine + "Drawing NO:" + dwgDesc.Trim();
							}


							//if(!string.IsNullOrEmpty(subjStr))
							//{
							//	subject = subject + Environment.NewLine + subjStr.Trim();
							//}
                                

							// DataTable에 삽입
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][noCol];
							if (string.IsNullOrEmpty(subjStr))
							{
								if (!string.IsNullOrEmpty(subject))
									dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + subject;
							}
							else
							{
								if (!string.IsNullOrEmpty(subject)) 
									dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + subject + Environment.NewLine + subjStr.Trim();
							}

							dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCode;
                            if (!descCol.Equals(-1))
                                dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDesc;
                            if(!unitCol.Equals(-1))
    							dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(dt.Rows[i][unitCol].ToString());
                            if(!qtCol.Equals(-1))
							    dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = dt.Rows[i][qtCol];
						}
						// ***** 첫번째 글자가 빈칸 → Description 조합
						else if (firstColString == "")
                        {
                            string prev = dtItem.Rows[dtItem.Rows.Count - 1]["DESC"].ToString();
                            string desc = dt.Rows[i][descCol].ToString();

                            if (desc != "") dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = prev + "\r\n" + desc;
						}
					}
				}
			}
		}

		#endregion
	}
}
