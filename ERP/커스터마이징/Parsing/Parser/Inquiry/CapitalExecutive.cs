using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class CapitalExecutive
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



        public CapitalExecutive(string fileName)
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
            int _itemUnit = 0;
            int _itemQt = 0;
            int _itemDescription = 0;
            int _itemCode = 0;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string makerString = string.Empty;
            string equipNameString = string.Empty;
            string modelString = string.Empty;
            string serialString = string.Empty;



            string assemblyString = string.Empty;
            string makerDescString = string.Empty;
            string modelDescString = string.Empty;
            string serialDescString = string.Empty;


            string _vessel = string.Empty;

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

            string[] itemName = { };

            bool itemStart = false;

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();
                    string dataValueSecond = dt.Rows[i][1].ToString();

                    if (dataValue.Contains("Line")) itemStart = true;

                    if (!itemStart)
                    {
                        //################# 문의번호
                        string enqNo = string.Empty;


                        if (string.IsNullOrEmpty(reference))
                        {
                            for (int j = 2; j < dt.Columns.Count; j++)
                            {
                                enqNo = dt.Rows[i][j].ToString();

                                if (enqNo.Contains("Our Ref"))
                                {
                                    reference = dt.Rows[i][j + 1].ToString().Trim();

                                    if (string.IsNullOrEmpty(reference))
                                        reference = dt.Rows[i][j + 2].ToString().Trim();
                                }
                            }
                        }


                        //################# 선명
                        if (string.IsNullOrEmpty(vessel))
                        {
                            if (dataValue.Contains("Vessel Name:"))
                            {
                                vessel = dt.Rows[i][1].ToString();
                                if (string.IsNullOrEmpty(vessel))
                                {
                                    vessel = dt.Rows[i][2].ToString();
                                }
                                // 앞에 붙는 호선번호(XXXXX-) 제거
                                int vesselIndex = vessel.IndexOf('-') + 1;

                                if (vesselIndex >= 0)
                                    _vessel = vessel.Substring(vesselIndex, vessel.Length - vesselIndex);

                                vessel = _vessel.Trim();
                            }
                        }


                        //################# SUBJECT EQUIPMENT
                        if (dataValue.Contains("Equipment:"))
                        {
                            equipNameString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();

                            int k = equipNameString.IndexOf('/');

                            if (k >= 0)
                                equipNameString = equipNameString.Substring(0, k - 1);

                            if (string.IsNullOrEmpty(equipNameString))
                                equipNameString = dataValue.Replace("Equipment:", "").Trim();

                        }
                        else if (dataValueSecond.Contains("Equipment:"))
                        {
                            equipNameString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();

                            int k = equipNameString.IndexOf('/');

                            if (k >= 0)
                                equipNameString = equipNameString.Substring(0, k - 1);

                            equipNameString = equipNameString.Trim();

                            if (string.IsNullOrEmpty(equipNameString))
                                equipNameString = dataValueSecond.Replace("Equipment:", "").Trim();
                        }


                        //################# SUBJECT MAKER
                        if (dataValue.Contains("Maker:"))
                        {
                            makerString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();
                        }
                        else if (dataValueSecond.Contains("Maker:"))
                        {
                            makerString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();
                        }


                        //################# SUBJECT MODEL
                        if (dataValue.Contains("Model:"))
                        {
                            modelString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();
                        }
                        else if (dataValueSecond.Contains("Model:"))
                        {
                            modelString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();
                        }


                        //################# SUBJECT SERIAL NO
                        if (dataValue.Contains("Serial No.:"))
                        {
                            serialString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();
                        }
                        else if (dataValueSecond.Contains("Serial No.:"))
                        {
                            serialString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();
                        }

                        serialString = serialString.Trim();
                        modelString = modelString.Trim();
                        makerString = makerString.Trim();
                        equipNameString = equipNameString.Trim();

                    }
                    else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량
                        string firstColString = dt.Rows[i][0].ToString();
                        string secondColString = dt.Rows[i][1].ToString();

                        // 수량, 단위
                        if (firstColString.Equals("Line No."))
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i][j].ToString().Contains("Unit")) _itemUnit = j;                            //단위
                                else if (dt.Rows[i][j].ToString().Contains("Quantity")) _itemQt = j;                     //수량
                                else if (dt.Rows[i][j].ToString().Contains("Description")) _itemDescription = j;         //제목
                                else if (dt.Rows[i][j].ToString().Contains("Catalogue No.")) _itemCode = j;             //코드(선용ver)
                                else if (dt.Rows[i][j].ToString().Contains("Part No.")) _itemCode = j;                  //코드
                            }
                        }


                        //################# DESCRIPTION ASSEMBLY
                        if (firstColString.Contains("Assembly") || firstColString.Contains("Section:"))
                        {
                            assemblyString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();

                            int k = iTemSUBJ.IndexOf('/');

                            if (k >= 0)
                                assemblyString = assemblyString.Substring(0, k - 1);
                        }
                        else if (secondColString.Contains("Assembly") || secondColString.Contains("Section:"))
                        {
                            assemblyString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();

                            int k = assemblyString.IndexOf('/');

                            if (k >= 0)
                                assemblyString = assemblyString.Substring(0, k - 1);
                        }



                        //################# DESCRIPTION MAKER
                        if (firstColString.Contains("Maker:"))
                        {
                            makerDescString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();
                        }
                        else if (secondColString.Contains("Maker:"))
                        {
                            makerDescString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();
                        }


                        //################# DESCRIPTION MODEL
                        if (firstColString.Contains("Model:"))
                        {
                            modelDescString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();
                        }
                        else if (secondColString.Contains("Model:"))
                        {
                            modelDescString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();
                        }


                        //################# DESCRIPTION SERIAL NO
                        if (firstColString.Contains("Serial No"))
                        {
                            serialDescString = dt.Rows[i][1].ToString().Trim() + " " + dt.Rows[i][2].ToString().Trim();
                        }
                        else if (secondColString.Contains("Serial No"))
                        {
                            serialDescString = dt.Rows[i][2].ToString().Trim() + " " + dt.Rows[i][3].ToString().Trim();
                        }

                        serialDescString = serialDescString.Replace(",", "").Trim();

                        serialDescString = serialDescString.Trim();



                        // 항목 갯수 만큼 ROW 추가
                        if (GetTo.IsInt(firstColString))
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

                            if (rowValueSpl[8] != null && rowValueSpl[9] == null)
                            {
                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                            }
                            else if (rowValueSpl[7] != null && rowValueSpl[8] == null)
                            {
                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                            }
                            else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                            {

                            }


                            double NumDetec;
                            bool isNumDetec = double.TryParse(dt.Rows[i][0].ToString(), out NumDetec);

                            iTemSUBJ = equipNameString.Trim();

                            if (!string.IsNullOrEmpty(makerString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerString;

                            if (!string.IsNullOrEmpty(modelString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelString;

                            if (!string.IsNullOrEmpty(serialString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialString;



                            // 새로운 아이템 항목이 시작할 때, 추가
                            if (isNumDetec)
                            {
                                iTemNo = dt.Rows[i][0].ToString();

                                if(!_itemCode.Equals(-1))
                                    iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                                if(!_itemUnit.Equals(-1))
                                    iTemUnit = dt.Rows[i][_itemUnit].ToString();

                                if(!_itemQt.Equals(-1))
                                    iTemQt = dt.Rows[i][_itemQt].ToString();

                                if(!_itemDescription.Equals(-1))
                                    iTemDESC = dt.Rows[i][_itemDescription].ToString();

                                string[] iTemCodeValue = iTemCode.Split('/');

                                if (iTemCodeValue.Length == 2)
                                {
                                    if (!GetTo.IsInt(iTemCodeValue[1].ToString().Substring(0, 1)) && iTemCodeValue[1].Length > 2)
                                    {
                                        iTemCode = iTemCodeValue[0].ToString().Trim();
                                        iTemDESC = iTemCodeValue[1].ToString().Trim() + " " + iTemDESC.Trim();
                                    }

                                }


                                int _i = i + 1;
                                while (dt.Rows[_i][0].ToString().Equals("Part Notes:"))
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDescription].ToString().Trim();

                                    _i += 1;
                                }

                                assemblyString = assemblyString.Trim();
                                makerDescString = makerDescString.Trim();
                                modelDescString = modelDescString.Trim();
                                serialDescString = serialDescString.Trim();

                                if (!string.IsNullOrEmpty(assemblyString))
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine + assemblyString.Trim();


                                //if (!string.IsNullOrEmpty(makerDescString))
                                //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + "MAKER: " + makerDescString.Trim();

                                //if (!string.IsNullOrEmpty(modelDescString))
                                //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + "MODEL: " + modelDescString.Trim();

                                //if (!string.IsNullOrEmpty(serialDescString))
                                //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + "SERIAL NO.: " + serialDescString.Trim();



                                // CODE 부분 DESC가 섞여 있음, '/' 나눌라고 햇으나, '/'뒤에도 CODE가 존재하는 경우가 있으므로 안됨...

                                if (!string.IsNullOrEmpty(iTemQt))
                                {
                                    string qtTest = iTemQt.Substring(0, 1);

                                    if (!GetTo.IsInt(qtTest))
                                        iTemQt = string.Empty;
                                }


                                //ITEM ADD START
                                dtItem.Rows.Add();
                                dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                                dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                                dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                                if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                                if (!string.IsNullOrEmpty(iTemSUBJ))
                                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                                dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                                iTemSUBJ = string.Empty;
                                iTemDESC = string.Empty;
                                iTemUnit = string.Empty;
                                iTemQt = string.Empty;
                                iTemCode = string.Empty;
                            }
                        }
                    }
                }
            }
        }
    }
}
