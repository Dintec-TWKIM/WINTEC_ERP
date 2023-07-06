using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class EsternPacific
    {
        string vessel;
        string reference;
        string partner;
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

        public string Partner
        {
            get
            {
                return partner;
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



        public EsternPacific(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자

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

            string subjStr = string.Empty;
            string particulars = string.Empty;
            string makerStr = string.Empty;
            string dwgStr = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemDwg = -1;

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

                    if(firstColStr.Contains("Machine Details"))
                    {
                        string[] vesselSpl = firstColStr.Replace("Machine Details", "").Split('-'); 

                        if(vesselSpl.Length == 2)
                        {
                            vessel = vesselSpl[0].ToString().Trim();
                            reference = vesselSpl[1].ToString().Trim();

                            if(vessel.Contains(":"))
                            {
                                int idx_e = vessel.IndexOf(":");

                                vessel = vessel.Substring(0, idx_e).Trim();
                            }
                        }

                        string vesselStr = string.Empty;

                        if(string.IsNullOrEmpty(vessel))
                        {
                            for(int c = 0; c < dt.Columns.Count; c++)
                            {
                                vesselStr = vesselStr + " " +  dt.Rows[i-1][c].ToString();
                            }

                            int idx_s = vesselStr.IndexOf("(");
                            int idx_e = vesselStr.IndexOf("/");

                            if (idx_s != -1)
                                vessel = vesselStr.Substring(0, idx_s).Trim();

                            if(idx_e != -1)
                                idx_e = vesselStr.IndexOf(")");

                            if (idx_e != -1)
                                reference = vesselStr.Substring(idx_e, vesselStr.Length - idx_e).Replace(")", "").Replace("/", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Description"))
                    {
                        subjStr = firstColStr.Replace("Description", "").Trim() + dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Particula"))
					{
                        particulars = firstColStr.Replace("Particulars:", "").Trim();
					}
                    else if (firstColStr.StartsWith("Maker:"))
                    {
                        makerStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("SNo."))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Ref")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr.Replace("-","").Trim()))
                    {
                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || dt.Rows[_i][0].ToString().StartsWith("https://"))
                            {
                                iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }
                            

                        if(_itemDwg != -1)
                        {
                            iTemCode = dt.Rows[i][_itemDwg].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || dt.Rows[_i][0].ToString().StartsWith("https://"))
                            {
                                dwgStr = dwgStr.Trim() + dt.Rows[_i][_itemDwg].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if(_itemQt != -1)
                        {
                            string[] qtSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if(qtSpl.Length == 2)
                            {
                                iTemQt = qtSpl[0].ToString().Trim();
                                iTemUnit = qtSpl[1].ToString().Trim();
                            }
                            else if (qtSpl.Length == 1)
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();

                                if(!GetTo.IsInt(iTemQt))
                                {
                                    iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();
                                    iTemUnit = dt.Rows[i][_itemQt].ToString().Trim();
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(particulars))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "PARTICULARS: " + particulars.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + dwgStr.Trim();


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
                        dwgStr = string.Empty;
                    }
                }
            }
        }
    }
}
