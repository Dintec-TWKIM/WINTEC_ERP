using Dintec;
using Dintec.Parser;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace Parsing
{
    class Kinnetik : InquiryBase
    {
        public Kinnetik(string fileName)
            : base(fileName)
        {

        }

        /// URL : https://web.kinnetiksolutions.com/management/
        /// ID : haeri.kim@dintec.co.kr
        /// PW : Dintec0849
        /// <summary>
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowIndex"></param>
        public override void ParseItem(DataTable dt, int rowIndex)
        {
            if (dt.Rows[rowIndex][0].ToString() == "From:")
                this._partner = dt.Rows[rowIndex + 1][0].ToString();

            if (dt.Rows[rowIndex][1].ToString() == "From:")
                this._partner = dt.Rows[rowIndex + 1][1].ToString();

            if (dt.Rows[rowIndex][0].ToString() == "Buyer Ref.:")
            {
                if (dt.Rows[rowIndex + 1][0].ToString().Split('/').Length == 2)
                    this._reference = dt.Rows[rowIndex + 1][0].ToString().Split('/')[1];
                else
                    this._reference = dt.Rows[rowIndex + 1][0].ToString();
            }

            if (dt.Rows[rowIndex][1].ToString() == "Buyer Ref.:")
            {
                if (dt.Rows[rowIndex + 1][1].ToString().Split('/').Length == 2)
                    this._reference = dt.Rows[rowIndex + 1][1].ToString().Split('/')[1];
                else
                    this._reference = dt.Rows[rowIndex + 1][1].ToString();
            }

            if (dt.Rows[rowIndex][2].ToString().StartsWith("Vessel:"))
            {
                if (dt.Rows[rowIndex + 1][2].ToString().Split('(').Length == 2)
                {
                    this._vessel = dt.Rows[rowIndex + 1][2].ToString().Split('(')[0].Trim();
                    this._imoNumber = dt.Rows[rowIndex + 1][2].ToString().Split('(')[1].Split(')')[0];
                }
                else if (dt.Rows[rowIndex + 2][2].ToString() != "Port of delivery:" &&
                         Regex.Replace(dt.Rows[rowIndex + 2][2].ToString(), @"\D", string.Empty).Length == 7)
                {
                    this._vessel = dt.Rows[rowIndex + 1][2].ToString();
                    this._imoNumber = Regex.Replace(dt.Rows[rowIndex + 2][2].ToString(), @"\D", string.Empty);
                }
                else
                {
                    this._vessel = dt.Rows[rowIndex + 1][2].ToString();
                }
            }

            if (dt.Rows[rowIndex][1].ToString() == "# ?" && this._dtItem.Rows.Count == 0)
            {
                int colNumber = 0,
                    colEquipment = 0,
                    colPartID = 0,
                    colDescription = 0,
                    colQty = 0,
                    colUnit = 0,
                    rowEquipment = 0,
                    newRowIndex = 0,
                    rowCurrent = 0,
                    rowCurrentClearance = 0,
                    rowNext = 0,
                    rowNextClearance = 0,
                    number;

                bool isNo;

                string no = string.Empty;

                for (int index = rowIndex; index < dt.Rows.Count; index++)
                {
                    if (dt.Rows[index][1].ToString() == "# ?")
                    {
                        #region 컬럼위치파악
                        for (int colIndex = 0; colIndex < dt.Rows[index].ItemArray.Length; colIndex++)
                        {
                            if (dt.Rows[index][colIndex].ToString() == "# ?")
                                colNumber = colIndex;

                            if (colNumber != 0)
                            {
                                if ((dt.Rows[index][colIndex].ToString().Length >= 2 && dt.Rows[index][colIndex].ToString().Right(2) == "ID") ||
                                    (dt.Rows[index + 1][colIndex].ToString().Length >= 2 && dt.Rows[index + 1][colIndex].ToString().Right(2) == "ID"))
                                    colPartID = colIndex;

                                if (dt.Rows[index][colIndex].ToString() == "Description")
                                    colDescription = colIndex;

                                if (dt.Rows[index][colIndex].ToString() == "Qty")
                                    colQty = colIndex;

                                if (dt.Rows[index][colIndex].ToString() == "UoM")
                                    colUnit = colIndex;
                            }
                        }
                        #endregion
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[index][colNumber].ToString()) &&
                        int.TryParse(dt.Rows[index][colNumber].ToString(), out number))
                    {
                        if (dt.Rows[index][colDescription].ToString() == "Packing/Handing/Freight" ||
                            dt.Rows[index][colDescription].ToString() == "Packing/Handling/Freight")
                            break;

                        no = number.ToString();

                        #region Equipment 컬럼 위치 찾기 및 순번이 아닌 경우 통과
                        isNo = false;

                        for (int e = index + 1; e < dt.Rows.Count; e++)
                        {
                            for (int c = 0; c < dt.Rows[e].ItemArray.Length; c++)
                            {
                                if (dt.Rows[e][c].ToString().StartsWith("Equipment information:"))
                                {
                                    rowEquipment = e;
                                    colEquipment = c;
                                    isNo = true;
                                    break;
                                }
                            }

                            if (isNo == true)
                                break;
                            else if (!string.IsNullOrEmpty(dt.Rows[e][colNumber].ToString()) &&
                                      int.TryParse(dt.Rows[e][colNumber].ToString(), out number))
                            {
                                isNo = false;
                                break;
                            }
                        }

                        if (!isNo) continue;
                        #endregion

                        rowCurrent = index;

                        for (int index1 = rowCurrent + 1; index1 < dt.Rows.Count; index1++)
                        {
                            if (dt.Rows[index1][0].ToString().StartsWith("https://") ||
                                dt.Rows[index1][colEquipment].ToString().StartsWith("Equipment information:"))
                            {
                                rowCurrentClearance = (index1 - 1) - rowCurrent;
                                break;
                            }
                        }

                        rowNext = 0;
                        rowNextClearance = 0;

                        for (int index1 = rowEquipment + 1; index1 < dt.Rows.Count; index1++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index1][colNumber].ToString()) && 
                                int.TryParse(dt.Rows[index1][colNumber].ToString(), out number))
                                rowNext = index1;
                            else if  (dt.Rows[index1][colNumber].ToString() == "Terms and conditions")
                            {
                                rowNext = index1;
                                break;
                            }
                            else if (rowNext > 0 &&
                                     (dt.Rows[index1][0].ToString().StartsWith("https://") ||
                                      dt.Rows[index1][colEquipment].ToString().StartsWith("Equipment information:")))
                            {
                                rowNextClearance = (index1 - 1) - rowNext;
                                break;
                            }
                        }

                        base._dtItem.Rows.Add();
                        newRowIndex = base._dtItem.Rows.Count - 1;

                        base._dtItem.Rows[newRowIndex]["NO"] = no;
                        base._dtItem.Rows[newRowIndex]["QT"] = dt.Rows[index][colQty].ToString();
                        base._dtItem.Rows[newRowIndex]["UNIT"] = dt.Rows[index][colUnit].ToString();

                        int startRowIndex = rowEquipment + 1;
                        int endRowIndex = rowNext - rowNextClearance;
                        string comment, strNumber, strEquipment, strComment;

                        comment = string.Empty;

                        for (int index1 = startRowIndex; index1 < endRowIndex; index1++)
                        {
                            strNumber = dt.Rows[index1][colNumber].ToString().Trim();
                            strEquipment = dt.Rows[index1][colEquipment].ToString().Trim();
                            strComment = dt.Rows[index1][colEquipment + 1].ToString().Trim();

                            if (string.IsNullOrEmpty(strNumber) && string.IsNullOrEmpty(strEquipment) && string.IsNullOrEmpty(strComment))
                                break;

                            if (!string.IsNullOrEmpty(strNumber))
                                base._dtItem.Rows[newRowIndex]["SUBJ"] += " " + strNumber;

                            if (!string.IsNullOrEmpty(strEquipment))
                                base._dtItem.Rows[newRowIndex]["SUBJ"] += " " + strEquipment;

                            if (!string.IsNullOrEmpty(strComment) && strComment != "Buyer's comment:" && strComment != "Supplier's comment:")
                                comment += " " + dt.Rows[index1][colEquipment + 1].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(base._dtItem.Rows[newRowIndex]["SUBJ"].ToString()))
                            base._dtItem.Rows[newRowIndex]["SUBJ"] = "FOR " + base._dtItem.Rows[newRowIndex]["SUBJ"].ToString().Trim() + comment;

                        startRowIndex = rowCurrent - rowCurrentClearance;
                        endRowIndex = rowCurrent + rowCurrentClearance;

                        for (int index1 = startRowIndex; index1 <= endRowIndex; index1++)
                        {
                            if (colPartID == colDescription)
                            {
                                if (rowCurrentClearance > 0)
                                {
                                    if (endRowIndex - startRowIndex == 2)
									{
                                        if (string.IsNullOrEmpty(dt.Rows[rowCurrent][colPartID].ToString()))
										{
                                            if (index1 - rowCurrent != 0)
                                            {
                                                string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                                base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();

                                                base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colQty].ToString().Trim();
                                            }
                                        }
                                        else
										{
                                            if (index1 - rowCurrent == 0)
                                            {
                                                string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                                base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();
                                            }
                                            else
											{
                                                base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                            }
                                        }
                                    }
                                    else if (endRowIndex - startRowIndex == 4)
									{
                                        if (index1 - rowCurrent != 0)
										{
                                            string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                            base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();
                                        }
                                        else
										{
                                            base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                        }
									}
                                    else if (dt.Rows[rowCurrent][colPartID].ToString().Length <= 11)
                                    {
                                        if (dt.Rows[rowCurrent - 1][colPartID].ToString().Length > 11 ||
                                            dt.Rows[rowCurrent + 1][colPartID].ToString().Length > 11)
                                        {
                                            if ((index1 - rowCurrent) % 2 == 0 &&
                                                !string.IsNullOrEmpty(dt.Rows[index1][colPartID].ToString().Trim()))
                                            {
                                                string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                                base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();
                                            }
                                            
                                            if ((index1 - rowCurrent) % 2 != 0 &&
                                                !string.IsNullOrEmpty(dt.Rows[index1][colDescription].ToString().Trim()))
                                                base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                        }
                                        else
                                        {
                                            if ((index1 - rowCurrent) % 2 != 0 &&
                                                !string.IsNullOrEmpty(dt.Rows[index1][colPartID].ToString().Trim()))
											{
                                                string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                                base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();
                                            }
                                            
                                            if ((index1 - rowCurrent) % 2 == 0 &&
                                                !string.IsNullOrEmpty(dt.Rows[index1][colDescription].ToString().Trim()))
                                                base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        if ((dt.Rows[rowCurrent - 1][colPartID].ToString().Length > 11 ||
                                             dt.Rows[rowCurrent + 1][colPartID].ToString().Length > 11) &&
                                             !string.IsNullOrEmpty(dt.Rows[index1][colDescription].ToString().Trim()))
                                        {
                                            base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                        }
                                        else if (dt.Rows[rowCurrent - 1][colPartID].ToString().Length <= 11 &&
                                                 dt.Rows[rowCurrent + 1][colPartID].ToString().Length <= 11)
                                        {
                                            if ((index1 - rowCurrent) % 2 != 0 &&
                                                !string.IsNullOrEmpty(dt.Rows[index1][colPartID].ToString().Trim()))
											{
                                                string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                                base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();
                                            }
                                            
                                            if ((index1 - rowCurrent) % 2 == 0 &&
                                                !string.IsNullOrEmpty(dt.Rows[index1][colDescription].ToString().Trim()))
                                                base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                        }
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[index1][colDescription].ToString().Trim()))
                                        base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[index1][colPartID].ToString().Trim()))
								{
                                    string oldValue = base._dtItem.Rows[newRowIndex]["ITEM"].ToString();
                                    base._dtItem.Rows[newRowIndex]["ITEM"] += (oldValue.Length > 0 && oldValue.Right(1) == "-" ? "" : " ") + dt.Rows[index1][colPartID].ToString().Trim();
                                }
                                
                                if (!string.IsNullOrEmpty(dt.Rows[index1][colDescription].ToString().Trim()))
                                    base._dtItem.Rows[newRowIndex]["DESC"] += " " + dt.Rows[index1][colDescription].ToString().Trim();
                            }
                        }

                        base._dtItem.Rows[newRowIndex]["ITEM"] = base._dtItem.Rows[newRowIndex]["ITEM"].ToString().Trim();
                        base._dtItem.Rows[newRowIndex]["DESC"] = base._dtItem.Rows[newRowIndex]["DESC"].ToString().Trim();
                    }
                }
            }
        }

		public override void ParseItemXml(string xml)
		{
            this._partner = xml.Split(new string[] { "From:" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "To:" }, StringSplitOptions.RemoveEmptyEntries)[0];
            this._partner = this.RemoveHtmlTag(this._partner);

            this._reference = xml.Split(new string[] { "Buyer Ref.:" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "Supplier Ref.:" }, StringSplitOptions.RemoveEmptyEntries)[0];
            this._reference = this.RemoveHtmlTag(this._reference).Replace("RFQ-", string.Empty);

            if (this._reference.Split('/').Length == 2)
                this._reference = this._reference.Split('/')[1];

            this._vessel = xml.Split(new string[] { "Phone:" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "Email:" }, StringSplitOptions.RemoveEmptyEntries)[0];
            this._vessel = this.RemoveHtmlTag(this._vessel);

            if (this._vessel.Split('(').Length > 1)
                this._imoNumber = this._vessel.Split('(')[1].Split(')')[0];

            this._vessel = this._vessel.Split('(')[0].Replace("Fax:", string.Empty).Replace(Environment.NewLine, string.Empty).Trim();

            string line = xml.Split(new string[] { "Line items" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "TOTAL PRICE" }, StringSplitOptions.RemoveEmptyEntries)[0];
            string[] tempArray = line.Split(new string[] { "<row>" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> tempList = new List<string>();

            int colNo = 0, colItem = 0, colSubject = 0, colDesc = 0, colQty = 0, colUnit = 0;

            for (int r = 1; r < tempArray.Length; r++)
            {
                string[] cells = tempArray[r].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (tempArray[r].Contains("#"))
                {
                    for (int c = 0; c < cells.Length; c++)
                    {
                        if (cells[c].Contains("#"))
                            colNo = c;
                        else if (cells[c].Contains("Supp"))
						{
                            colItem = c;
                            colSubject = c - 1;
                        }
                        else if (cells[c].Contains("Desc"))
                            colDesc = c;
                        else if (cells[c].Contains("Qty"))
                            colQty = c - 1;
                        else if (cells[c].Contains("UoM"))
                            colUnit = c - 1;
                    }
                }

                if (tempArray[r].Contains("https://") || 
                    tempArray[r].Contains("Kinnetik Web") ||
                    tempArray[r].Contains("#") ||
                    tempArray[r - 1].Contains("#"))
                    continue;

                tempList.Add(tempArray[r]);
			}

            string[] rows = tempList.ToArray();

            for (int r = 0; r < rows.Length; r++)
			{
                string[] cells = rows[r].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                int number, number1;

                if (int.TryParse(this.RemoveHtmlTag(cells[colNo]), out number))
				{
                    if (rows[r].Contains("Packing/Handing/Freight") ||
                        rows[r].Contains("Packing/Handling/Freight"))
                        continue;
                    
                    string strItem = string.Empty, strDesc = string.Empty, strSubject = string.Empty, temp;

                    int startRowIndex = 0;

                    for (int r1 = r; r1 >= 0; r1--)
                    {
                        string[] cells1 = rows[r1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        if (cells1[0].StartsWith("        <cell />") || 
                            (int.TryParse(this.RemoveHtmlTag(cells1[colNo]), out number1) && number != number1))
						{
                            startRowIndex = r1 + 1;
                            break;
                        }       
                    }

                    for (int r1 = startRowIndex; r1 < rows.Length; r1++)
					{
                        string[] cells1 = rows[r1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        if (int.TryParse(this.RemoveHtmlTag(cells1[colNo]), out number1) && number != number1)
                            break;
                        
                        if (rows[r1].Contains("Equipment information:"))
						{
                            startRowIndex = r1 + 1;
                            break;
                        }

                        string[] cells2 = new string[20];
                        int current = 0;

                        for (int c = 0; c < cells1.Length; c++)
						{
                            if (cells1[c].Contains("colspan=\""))
							{
                                int colSpan = Convert.ToInt32(cells1[c].Split(new string[] { "colspan=\"" }, StringSplitOptions.RemoveEmptyEntries)[1].Split('\"')[0]);

                                for (int s = 0; s < colSpan; s++)
                                {
                                    cells2[current++] = this.RemoveHtmlTag(cells1[c]);
                                }
                            }
                            else
							{
                                cells2[current++] = this.RemoveHtmlTag(cells1[c]);
                            }
                        }

                        temp = cells2[colItem];
                        strItem += (temp == string.Empty ? string.Empty : temp + " ");

                        temp = cells2[colDesc];
                        strDesc += (temp == string.Empty ? string.Empty : temp + " ");
                    }

                    int endRowIndex = 0;

                    for (int r1 = startRowIndex; r1 < rows.Length; r1++)
                    {
                        string[] cells1 = rows[r1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        if (!cells1[0].StartsWith("        <cell />"))
                        {
                            endRowIndex = r1;
                            break;
                        }
                    }

                    for (int r1 = startRowIndex; r1 < endRowIndex; r1++)
                    {
                        string[] cells1 = rows[r1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        temp = this.RemoveHtmlTag(cells1[colSubject]);
                        strSubject += (temp == string.Empty ? string.Empty : temp + " ");
                    }

                    for (int r1 = startRowIndex; r1 < endRowIndex; r1++)
                    {
                        if (rows[r1].Contains("Buyer's") || rows[r1].Contains("Supplier's"))
                            continue;

                        string[] cells1 = rows[r1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        for (int c = colSubject + 1; c < cells1.Length; c++)
						{
                            temp = this.RemoveHtmlTag(cells1[c]);
                            strSubject += (temp == string.Empty ? string.Empty : temp + " ");
                        }
                    }

                    base._dtItem.Rows.Add();
                    int newRowIndex = base._dtItem.Rows.Count - 1;
                    
                    base._dtItem.Rows[newRowIndex]["NO"] = number.ToString();
                    base._dtItem.Rows[newRowIndex]["SUBJ"] = (string.IsNullOrEmpty(strSubject.Trim()) ? string.Empty : "FOR " + HttpUtility.HtmlDecode(strSubject.Trim()));
                    base._dtItem.Rows[newRowIndex]["ITEM"] = HttpUtility.HtmlDecode(strItem.Trim());
                    base._dtItem.Rows[newRowIndex]["DESC"] = HttpUtility.HtmlDecode(strDesc.Trim());
					base._dtItem.Rows[newRowIndex]["QT"] = this.RemoveHtmlTag(cells[colQty]);
					base._dtItem.Rows[newRowIndex]["UNIT"] = this.RemoveHtmlTag(cells[colUnit]);
				}
            }
        }
	}
}
