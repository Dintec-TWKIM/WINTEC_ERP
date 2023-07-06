using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Rickmers
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



        public Rickmers(string fileName)
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

            bool iTemStart = false;

            int _itemDesc = -1;
            int _itemQt = -1;

            string deleteQt = string.Empty;
            string descValue = string.Empty;

            string makerString = string.Empty;
            string dwgNoString = string.Empty;
            string typeString = string.Empty;
            string sizeString = string.Empty;
            string modelString = string.Empty;
            string internoString = string.Empty;

            string subjStr = string.Empty;

            bool subjCheck = false;

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
                    string firstColString = dt.Rows[i][0].ToString();

                    if (firstColString.Equals("Item"))
                    {
                        iTemStart = true;

                        subjStr = string.Empty;

                        string _firstcolstring = firstColString.Substring(0, 1);
                        int _i = i + 1;
                        while (!GetTo.IsInt(_firstcolstring))
                        {
                            if (!subjCheck)
                            {
                                if (GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1).ToString()))
                                    break;


                            }

                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr + dt.Rows[_i][c].ToString().Trim();

                                
                            }

                            if (subjStr.Contains("PLEASE NOTE"))
                                break;

                            subjStr = subjStr + Environment.NewLine;

                            if (_i < dt.Rows.Count - 1)
                                _i += 1;
                            else
                                break;

                            if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                _firstcolstring = dt.Rows[_i][0].ToString().Substring(0, 1);
                            else
                                subjCheck = true;


                            //else
                            //{
                            //    subjCheck = true;
                            //}
                        }
                    }

                    // 선명 ex) 6108-2017-C0043-B(01) - SPIRIT OF HAMBURG
                    if (string.IsNullOrEmpty(vessel))
                    {
                        if (firstColString.Equals("Inquiry"))
                        {
                            string[] vesselValue = { };
                            if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                            {
                                vesselValue = dt.Rows[i][1].ToString().Split('-');
                                vessel = vesselValue[vesselValue.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                vesselValue = dt.Rows[i][2].ToString().Split('-');
                                vessel = vesselValue[vesselValue.Length - 1].ToString().Trim();
                            }
                        }
                    }
                    
                    // 문의번호
                    if (string.IsNullOrEmpty(reference))
                    {
                        if (firstColString.Equals("Inquiry No.:"))
                        {
                            reference = dt.Rows[i][1].ToString().Trim();
                        }
                    }


                    if (iTemStart)
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        }

                        if (!string.IsNullOrEmpty(firstColString))
                        {
                            string _firstcolstring = firstColString.Substring(0, 1);

                            if (GetTo.IsInt(_firstcolstring))
                            {
                                _firstcolstring = string.Empty;
                                string[] QtValue = {};


                                // 수량과 단위가 붙어서 나옴
                                // 수량 컬럼 위치에 없을때는 -1 하여 찾음
                                // 수량만 있고, 단위가 없는 경우가 있음
                                if (!string.IsNullOrEmpty(dt.Rows[i][_itemQt].ToString()))
                                    QtValue = dt.Rows[i][_itemQt].ToString().Split(' ');
                                else
                                    QtValue = dt.Rows[i][_itemQt - 1].ToString().Split(' ');

                                if (QtValue.Length == 2)
                                {
                                    iTemQt = QtValue[0].ToString().Trim();
                                    iTemUnit = QtValue[1].ToString().Trim();

                                    deleteQt = QtValue[0].ToString() + " " + QtValue[1].ToString();
                                }
                                else if (QtValue.Length == 1)
                                {
                                    // 수량만 있을경우 아니면 단위만 있을경우
                                    if (GetTo.IsInt(QtValue[0].ToString()))
                                        iTemQt = QtValue[0].ToString().Trim();
                                    else
                                        iTemUnit = QtValue[0].ToString().Trim();
                                }


                                // DESCRIPTION 첫 ROW
                                for (int c = _itemQt; c < dt.Columns.Count; c++)
                                {
                                    descValue = descValue.Trim() + " " + dt.Rows[i][c].ToString();
                                }

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    for (int c = _itemQt; c < dt.Columns.Count; c++)
                                    {
                                        if (dt.Rows[_i][c].ToString().IndexOf("Part No") == 0)
                                        {
                                            iTemCode = dt.Rows[_i][c].ToString().Trim().Replace("Part No.", "").Replace(":", "").Replace("......", "").Trim();
                                            
                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                iTemCode = iTemCode + dt.Rows[_i][cc].ToString().Replace("......", "").Trim();
                                            }

                                                // 같은 ROW if에 들어왔을때 break
                                                break;
                                        }
                                        else if (dt.Rows[_i][c].ToString().IndexOf("MAKER") == 0 || dt.Rows[_i][c].ToString().IndexOf("Maker") == 0)
                                        {
                                            makerString = dt.Rows[_i][c].ToString().Trim().Replace("MAKER", "").Replace(":", "").Replace("......", "").Replace(";","").Trim();

                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                makerString = makerString + dt.Rows[_i][cc].ToString().Replace("......", "").Replace(";","").Trim();
                                            }

                                            break;
                                        }
                                        else if (dt.Rows[_i][c].ToString().IndexOf("TYPE") == 0 || dt.Rows[_i][c].ToString().IndexOf("Type") == 0)
                                        {
                                            typeString = dt.Rows[_i][c].ToString().Trim().Replace("TYPE", "").Replace(":", "").Replace("......", "").Trim();

                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                typeString = typeString + dt.Rows[_i][cc].ToString().Replace("......", "").Trim();
                                            }
                                            break;
                                        }
                                        else if (dt.Rows[_i][c].ToString().IndexOf("DWG") == 0 || dt.Rows[_i][c].ToString().IndexOf("Dwg") == 0 || dt.Rows[_i][c].ToString().IndexOf("Drawing") == 0)
                                        {
                                            dwgNoString = dt.Rows[_i][c].ToString().Trim().Replace("DWG NO", "").Replace(":", "").Replace("Drawing No.", "").Replace("......", "").Trim();

                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                dwgNoString = dwgNoString + dt.Rows[_i][cc].ToString().Replace("......", "").Trim();
                                            }
                                            break;
                                        }
                                        else if (dt.Rows[_i][c].ToString().IndexOf("Size") == 0 || dt.Rows[_i][c].ToString().IndexOf("SIZE") == 0)
                                        {
                                            sizeString = dt.Rows[_i][c].ToString().Trim().Replace("Size", "").Replace(":", "").Replace("SIZE", "").Replace("......", "").Trim();

                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                sizeString = sizeString + dt.Rows[_i][cc].ToString().Replace("......", "").Trim();
                                            }
                                            break;
                                        }
                                        else if (dt.Rows[_i][c].ToString().IndexOf("Model") == 0 || dt.Rows[_i][c].ToString().IndexOf("MODEL") == 0)
                                        {
                                            modelString = dt.Rows[_i][c].ToString().Trim().Replace("MODEL", "").Replace(":", "").Replace("Model", "").Replace("......", "").Trim();

                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                modelString = modelString + dt.Rows[_i][cc].ToString().Replace("......", "").Trim();
                                            }
                                            break;
                                        }
                                        else if (dt.Rows[_i][c].ToString().IndexOf("Internal") == 0)
                                        {
                                            internoString = dt.Rows[_i][c].ToString().Trim().Replace("Internal No.", "").Replace(":", "").Replace("......", "").Trim();

                                            for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                            {
                                                internoString = internoString + dt.Rows[_i][cc].ToString().Replace("......", "").Trim();
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            descValue = descValue.Trim() + " " + dt.Rows[_i][c].ToString();
                                        }
                                    }

                                    descValue = descValue.Trim();

                                    // 같은줄이 아닐땐  ','  구분
                                    if(!descValue.EndsWith(","))
                                        descValue = descValue + ",";

                                    _i += 1;
                                }


                                // 마지막에 ',' 이면 삭제
                                if (descValue.EndsWith(","))
                                    descValue = descValue.Substring(0, descValue.Length - 1);


                                // 수량, 단위와 함께 읽히기 때문에 삭제
                                if (!string.IsNullOrEmpty(deleteQt))
                                    iTemDESC = descValue.Replace(deleteQt, "");
                                else
                                    iTemDESC = descValue;


                                // 각 description 항목 앞에 ',' 구분자 추가
                                if (!string.IsNullOrEmpty(makerString))
                                    iTemDESC = iTemDESC.Trim() + ", MAKER: " + makerString;

                                if (!string.IsNullOrEmpty(modelString))
                                    iTemDESC = iTemDESC.Trim() + ", MODEL: " + modelString;

                                if (!string.IsNullOrEmpty(typeString))
                                    iTemDESC = iTemDESC.Trim() + ", TYPE: " + typeString;

                                if (!string.IsNullOrEmpty(dwgNoString))
                                    iTemDESC = iTemDESC.Trim() + ", DWG NO:" + dwgNoString;

                                if (!string.IsNullOrEmpty(sizeString))
                                    iTemDESC = iTemDESC.Trim() + ", SIZE: " + sizeString;

                                if (!string.IsNullOrEmpty(internoString))
                                    iTemDESC = iTemDESC.Trim() + ", INTERNAL NO: " + internoString;


                                iTemSUBJ = subjStr.Trim();

                                //ITEM ADD START
                                dtItem.Rows.Add();
                                //dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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
                                iTemSUBJ = string.Empty;
                                iTemDESC = string.Empty;
                                iTemCode = string.Empty;

                                descValue = string.Empty;
                                typeString = string.Empty;
                                dwgNoString = string.Empty;
                                makerString = string.Empty;
                                sizeString = string.Empty;
                                internoString = string.Empty;

                                subjCheck = false;
                                iTemSUBJ = string.Empty;
                            }
                        }
                    }

                }
            }
        }
    }
}
