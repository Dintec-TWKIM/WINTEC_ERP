using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    public class HLine
    {
        string contact;
        string reference;
        string vessel;
        string imoNumber;
        DataTable dtItem;

        string fileName;
        UnitConverter uc;

        #region ==================================================================================================== Property

        public string Contact
        {
            get
            {
                return contact;
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

        public HLine(string fileName)
        {
            contact = "";
            reference = "";
            vessel = "";
            imoNumber = "";

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
            // Pdf를 엑셀로 변환해서 분석 (엑셀이 편함)
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

            string iTemDesc = string.Empty;
            string iTemDwg = string.Empty;
            string iTemCode = string.Empty;
            string iTemQt = string.Empty;
            string iTemSubj = string.Empty;

            string subjStr = string.Empty;
            string modelStr = string.Empty;
            string makerStr = string.Empty;

            string referBody = string.Empty;

            int _itemDesc = -1;
            int _itemDwg = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemSubj = -1;
            int _itemVessel = -1;

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstColString = dt.Rows[i][0].ToString();

                    if (firstColString.Equals("Quotation No"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();

                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][2].ToString().Trim(); ;
                    }
                    else if (firstColString == "Seq")
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Vessel Name")) _itemVessel = c;
                            else if (dt.Rows[i][c].ToString().Equals("DRAW NO.")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("ITEM NO.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Mach. Desc.")) _itemSubj = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Equip. Desc.")) _itemDesc = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColString))
                    {
                        

                        if (!_itemVessel.Equals(-1))
                        {
                            vessel = dt.Rows[i][_itemVessel].ToString().Trim();
                            referBody = dt.Rows[i + 2][_itemVessel].ToString().Trim();
                        }

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemSubj.Equals(-1))
                        {
                            subjStr = dt.Rows[i][_itemSubj].ToString().Trim();
                            modelStr = dt.Rows[i+1][_itemSubj].ToString().Trim();
                            makerStr = dt.Rows[i + 2][_itemSubj].ToString().Trim();
                        }

                        if (!_itemDwg.Equals(-1))
                        {
                            iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();
                            iTemDesc = dt.Rows[i + 1][_itemDwg].ToString().Trim();
                            iTemCode = dt.Rows[i + 2][_itemDwg].ToString().Trim();  // part no : 20190807 양현정 수정 요청
                        }

                        if (!_itemCode.Equals(-1))
                        {
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                            iTemDesc = iTemDesc + "/" + dt.Rows[i + 1][_itemCode].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSubj = subjStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSubj = iTemSubj.Trim() + Environment.NewLine + "TYPE: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSubj = iTemSubj.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();


                        // 아이템
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                        if(!string.IsNullOrEmpty(iTemSubj))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSubj;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc.Trim();
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = "PCS";
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;

                        string itemNo = dt.Rows[i][3].ToString();
                        string partNo = dt.Rows[i + 2][2].ToString();
                        string maker = dt.Rows[i + 2][3].ToString();

                        if (maker.IndexOf("HYUNDAI") == 0 || (string.IsNullOrEmpty(itemNo) || itemNo.Equals("00") || itemNo.Equals("0") || maker.StartsWith("BOLL+KIRCH FILTERBAU GMBH")))
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = partNo;
                        else if (!string.IsNullOrEmpty(iTemDwg))
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemDwg + "-" + itemNo;
                        else
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemNo;
                    }
                }
            }

            if (!string.IsNullOrEmpty(referBody))
                reference = reference + "/" + referBody;
        }

        #endregion
    }
}
