using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class AlmiNew_pdf
	{
        string vessel;
        string reference;
        string imoNumber;
        DataTable dtItem;

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

        public string ImoNumber
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



        public AlmiNew_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            imoNumber = "";

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         //선사코드
            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string subjStr1 = string.Empty;

            int _referenceCheckInt = -1;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;
            int _itemUnit = -1;

            bool itemStart = false;


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
                    string firstColString = dt.Rows[i][0].ToString().ToLower();


                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("vessel:"))
                            {
                                for (int c2 = c; c2 < dt.Columns.Count; c2++)
                                {
                                    if (string.IsNullOrEmpty(vessel))
                                        vessel = vessel.Trim() + dt.Rows[i][c2].ToString().ToLower().Replace("vessel:", "").Trim();
                                    else
                                        break;
                                }
                            }
                        }
                    }


                    if (_referenceCheckInt.Equals(-1))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("req. code/date:"))
                            {
                                _referenceCheckInt = c;
                                reference = dt.Rows[i][c].ToString().ToLower().Replace("req. code/date:", "").Trim();

                                string[] refSpl = reference.Split('/');

                                if (refSpl.Length == 2)
                                {
                                    reference = refSpl[0].ToString().Trim();
                                }

                                itemStart = true;
                            }
                        }
                    }
                    else if (itemStart)
                    {
                        if (firstColString.StartsWith("no."))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().StartsWith("Req.")) _itemQt = c;
                                if (dt.Rows[i][c].ToString().StartsWith("Packing")) _itemUnit = c;
                                if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                            }

                            if (!GetTo.IsInt(dt.Rows[i + 1][0].ToString().Replace(".", "")))
                            {
                                subjStr1 = string.Empty;

                                int _i = i + 1;

                                while (!GetTo.IsInt(dt.Rows[_i][0].ToString().Replace(".", "")))
                                {
                                    for (int c = 0; c < dt.Columns.Count; c++)
                                    {
                                        subjStr1 = subjStr1.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                    }

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }

                        }
                        else if (GetTo.IsInt(firstColString.Replace(".", "")))
                        {

                            // row 값 가져와서 배열에 넣은후 값 추가하기
                            string[] rowValueSpl = new string[20];
                            int columnCount = 0;
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                                {
                                    rowValueSpl[columnCount] = c.ToString();
                                    columnCount++;
                                }
                            }


                            if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                            {
                                _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            }
                            else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                            {
                                _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            }
                            else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                            {
                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                                _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            }

                            if (_itemQt != -1)
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (_itemUnit != -1)
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (_itemCode != -1)
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();


                            if (!string.IsNullOrEmpty(subjStr1))
                                iTemSUBJ = subjStr1.Trim();


                            if (_itemDesc != -1)
                            {
                                iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                                int _i = i + 1;

                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    for (int c = _itemDesc; c <= _itemDesc; c++)
                                    {
                                        string checkLineStr = dt.Rows[i][c].ToString().ToLower();

                                        if (checkLineStr.StartsWith("produced") || checkLineStr.StartsWith("no.item") || checkLineStr.StartsWith("system:") || checkLineStr.StartsWith("particulars") || checkLineStr.StartsWith("maker") ||
                                            checkLineStr.StartsWith("subsystem"))
                                            break;
                                        else
                                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                    }

                                    if (iTemDESC.Contains("produced") || iTemDESC.Contains("no.item") || iTemDESC.Contains("system:") || iTemDESC.Contains("particulars") || iTemDESC.Contains("maker :") ||
                                            iTemDESC.Contains("subsystem"))
                                        break;

                                        _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }



                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemQt = string.Empty;
                            iTemUnit = string.Empty;
                            iTemDESC = string.Empty;
                            iTemSUBJ = string.Empty;
                        }
                    }
                }
            }
        }
    }
}
