using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Marmaras
    {
        string vessel;
        string reference;
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

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Marmaras(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호

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
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;
            string subjDetailStr = string.Empty;
            string manualStr = string.Empty;
            string drwingStr = string.Empty;

            string descStr = string.Empty;
            string[] descValue = { };

            string itemnoStr = string.Empty;
            string manufacStr = string.Empty;
            string specStr = string.Empty;
            string qtStr = string.Empty;
            string materialStr = string.Empty;
            string plateStr = string.Empty;

            bool makerCheck = false;



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
                    string firstcolString = dt.Rows[i][0].ToString();

                    if (firstcolString.Equals("Vessel"))
                    {
                        vessel = dt.Rows[i][1].ToString().Replace("M/V","").Replace("M/T", "").Trim();

                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Our Ref"))
                            {
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                    }

                    if (firstcolString.Equals("Equipment"))
                    {
                        //subjStr = dt.Rows[i][1].ToString().Trim();

                        //if (string.IsNullOrEmpty(subjStr))
                        //    subjStr = dt.Rows[i][2].ToString().Trim();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!makerCheck)
                                subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();

                            if (dt.Rows[i][c].ToString().Contains("Maker"))
                            {
                                makerStr = dt.Rows[i][c].ToString().Replace("Maker", "").Trim();
                                makerCheck = true;
                            }

                            if (string.IsNullOrEmpty(makerStr))
                            {
                                if(c + 1 < dt.Columns.Count)
                                    makerStr = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                    }

                    if (firstcolString.Equals("Model/Type"))
                    {
                        modelStr = dt.Rows[i][1].ToString().Trim();

                        if (string.IsNullOrEmpty(modelStr))
                            modelStr = dt.Rows[i][2].ToString().Trim();

                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Serial No"))
                                serialStr = dt.Rows[i][c].ToString().Replace("Serial No", "").Trim();

                            if (string.IsNullOrEmpty(serialStr) && c+1 < dt.Columns.Count)
                                serialStr = dt.Rows[i][c + 1].ToString().Trim();

                            
                        }
                    }

                    if (firstcolString.Equals("Equip.Details"))
                    {
                        subjDetailStr = dt.Rows[i][1].ToString().Trim();

                        if (string.IsNullOrEmpty(subjDetailStr))
                            subjDetailStr = dt.Rows[i][2].ToString().Trim();
                    }


                    if (firstcolString.Equals("Manual"))
                    {
                        manualStr = dt.Rows[i][1].ToString().Trim();

                        if (!string.IsNullOrEmpty(manualStr))
                            manualStr = dt.Rows[i][2].ToString().Trim();
                    }
                        


                    if (firstcolString.Equals("#"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Items")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity") || dt.Rows[i][c].ToString().Contains("Requested")) _itemQt = c;
                        }
                    }


                    iTemSUBJ = subjStr.Replace("Maker","").Replace("MAKER","").Replace("maker","").Trim();

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

                    if (!string.IsNullOrEmpty(modelStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL/TYPE: " + modelStr;

                    if (!string.IsNullOrEmpty(serialStr))
                    {
                        if (serialStr.Contains("DRAWING") || serialStr.Contains("Drawing"))
                        {
                            iTemSUBJ = iTemSUBJ.Trim() +Environment.NewLine + serialStr.Replace("Serial No", "");
                        }
                        else
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr;
                        }
                    }

                    if (!string.IsNullOrEmpty(subjDetailStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjDetailStr;
                    
                    if (!string.IsNullOrEmpty(manualStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MANUAL: " + manualStr;



                    if (GetTo.IsInt(firstcolString.Replace("(", "").Replace(")", "")))
                    {
                        iTemNo = firstcolString.Replace("(", "").Replace(")", "").Trim();

                        if(!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (_itemQt + 1 < dt.Columns.Count)
                            {
                                iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                            }

                            if(!GetTo.IsInt(iTemQt))
							{
                                iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQt].ToString().Trim();
							}

                        }
                        //descStr = dt.Rows[i][0].ToString().Trim();
                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][_itemQt].ToString()))
                        {
                            if (_i < dt.Rows.Count)
                            {
                                if (!dt.Rows[_i][0].ToString().Contains("MARMARAS NAVIGATION LTD"))
                                {
                                    descStr = descStr.Trim() + dt.Rows[_i][0].ToString().Trim();

                                    _i += 1;

                                    if (_i == dt.Rows.Count)
                                        break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }


                        string[] _descValue = descStr.Split(',');

                        for (int c = 0; c < _descValue.Length; c++)
                        {
                            if (_descValue[c].ToString().Contains("Item No"))
                            {
                                iTemCode = iTemCode.Trim() + _descValue[c].ToString().Replace("Item No", "").Replace(":","").Trim();
                                descStr = descStr.Replace(_descValue[c], "").Trim();

                                if (descStr.StartsWith(","))
                                {
                                    descStr = descStr.Substring(1, descStr.Length - 1).Trim();
                                }
                            }
                        }

                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + descStr;

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if(!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemNo = string.Empty;
                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                        iTemSUBJ = string.Empty;

                        descStr = string.Empty;
                        itemnoStr = string.Empty;
                        manufacStr = string.Empty;
                        specStr = string.Empty;
                        qtStr = string.Empty;
                        materialStr = string.Empty;
                        plateStr = string.Empty;

                        makerCheck = false;


                    }
                }
            }
        }
    }
}
