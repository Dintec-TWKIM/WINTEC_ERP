using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Chandris
    {
        string vessel;
        string reference;
        DataTable dtItem;
        string imoNumber;

        string fileName;
        UnitConverter uc;


        #region ==================================================================================================== Property

        public string Vessel
        {
            get
            {
                return vessel;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public string Imonumber
        {
            get
            {
                return imoNumber;
            }
        }

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Chandris(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            imoNumber = string.Empty;

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         // 선사
            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemCode = -1;



            string equipStr = string.Empty;
            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;
            string bookStr = string.Empty;
            string particuStr = string.Empty;
            string assStr = string.Empty;
            string drwStr = string.Empty;
            string model2Str = string.Empty;




            #region ########### READ ###########
            // Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
            // 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
            string xmlTemp = PdfReader.ToXml(fileName);

            // 2. 도면을 제외한 Page 카운트 가져오기
            int pageCount = xmlTemp.Count("<page>");

            // 3. 앞서 나온 Page를 근거로 파싱 시작			
            string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
            xml = PdfReader.GetXml(fileName);
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
            #endregion ########### READ ###########

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstColStr = dt.Rows[i][0].ToString();

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Requisition No"))
                            {
                                for(int cc = c; cc < dt.Columns.Count; cc++)
								{
                                    if (string.IsNullOrEmpty(reference))
                                        reference = dt.Rows[i][cc].ToString().Replace("Requisition No.", "").Trim();
                                    else
                                        break;
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().Contains("M/T"))
                                vessel = dt.Rows[i][c].ToString().Trim() +  dt.Rows[i][c + 1].ToString().Trim();

                            vessel = vessel.Replace("M/T", "").Trim();
                        }
                    }

                    if(string.IsNullOrEmpty(imoNumber))
					{
                        for(int c = 0; c < dt.Columns.Count; c++)
						{
                            if(dt.Rows[i][c].ToString().Contains("IMO No."))
							{
                                for (int cc = c+1; cc < dt.Columns.Count; cc++)
                                {
                                    if (string.IsNullOrEmpty(imoNumber))
                                        imoNumber = dt.Rows[i][cc].ToString().Trim();
                                    else
                                        break;
                                }
                            }

                            if(!string.IsNullOrEmpty(imoNumber))
							{
                                string[] imoSpl = imoNumber.Split('/');

                                if (imoSpl.Length == 3)
                                    imoNumber = imoSpl[2].ToStr().Trim();
							}
						}
					}


                    if (firstColStr.StartsWith("Equipment"))
                    {
                        subjStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Maker"))
                    {
                        makerStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            makerStr = makerStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Model"))
                    {
                        modelStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            modelStr = modelStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Book"))
                    {
                        bookStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            bookStr = bookStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        serialStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            serialStr = serialStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("System Particulars"))
                    {
                        particuStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            particuStr = particuStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Assembly"))
                    {
                        assStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            assStr = assStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Drawing No"))
                    {
                        drwStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            drwStr = drwStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Line No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        //// row 값 가져와서 배열에 넣은후 값 추가하기
                        //string[] rowValueSpl = new string[20];
                        //int columnCount = 0;
                        //for (int c = 0; c < dt.Columns.Count; c++)
                        //{
                        //    if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                        //    {
                        //        rowValueSpl[columnCount] = c.ToString();
                        //        columnCount++;
                        //    }
                        //}


                        //if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        //{
                        //    _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                        //    _itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
                        //    _itemCode = Convert.ToInt16(rowValueSpl[3].ToString());
                        //    _itemDesc = Convert.ToInt16(rowValueSpl[4].ToString());
                        //    //_itemCode = Convert.ToInt16(rowValueSpl[3].ToString());
                        //    //_itemDwg = Convert.ToInt16(rowValueSpl[4].ToString());
                        //}

                        iTemNo = firstColStr;

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(bookStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "BOOK NO: " + bookStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(particuStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Assembly Particulars: " + particuStr.Trim();

                        if (!string.IsNullOrEmpty(drwStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Drawing No: " + drwStr.Trim();

                        if (!string.IsNullOrEmpty(assStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + assStr.Trim();



                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                    }
                }
            }
        }
    }
}
