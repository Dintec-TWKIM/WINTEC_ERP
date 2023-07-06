using System;
using System.Data;
using Dintec;
using Dintec.Parser;
using Aspose.Email.Outlook;

namespace Parsing
{
    class DanaosMail
    {
        string imoNumber;
        string vessel;
        string reference;
        string contact;
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

        public string Contact
        {
            get
            {
                return contact;
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


        public DanaosMail(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            imoNumber = "";                     // IMO
            contact = "";

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
            string iTemDesc = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string nameStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;

            string subjStr = string.Empty;
            string subjStr1 = string.Empty;


            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body;


            // IMO
            int idx_lts = mailBodyStr.IndexOf("IMO No");
            int idx_lte = mailBodyStr.IndexOf("System");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                imoNumber = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("System", "").Replace("IMO No", "").Replace(":","").Trim();
            }

            idx_lts = -1;
            idx_lte = -1;


            // Reference
            idx_lts = mailBodyStr.IndexOf("Request For Quotation");
            idx_lte = mailBodyStr.IndexOf("Vessel");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                reference = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Request For Quotation", "").Replace("Vessel", "").Replace(":", "").Trim();
            }

            idx_lts = -1;
            idx_lte = -1;


            // Vessel
            idx_lts = mailBodyStr.IndexOf("Vessel");
            idx_lte = mailBodyStr.IndexOf("Department");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                vessel = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel", "").Replace("Department", "").Replace(":", "").Trim();
            }


            idx_lts = -1;
            idx_lte = -1;

            // SUBJECT
            idx_lts = mailBodyStr.IndexOf("System");
            idx_lte = mailBodyStr.IndexOf("No.");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                subjStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("System", "").Replace(":", "").Trim();

                if (subjStr.Contains("Subsystem"))
                {
                    idx_lts = subjStr.IndexOf("Subsystem");

                    if (!idx_lts.Equals(-1))
                    {
                        subjStr1 = subjStr.Substring(idx_lts, subjStr.Length - idx_lts).Trim();
                    }
                }
            }


            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                string vesselRef = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("발주 후", "").Replace(":", "").Trim();

                idx_lte = vesselRef.IndexOf("Department");

                string _vesselRef = vesselRef.Substring(0, idx_lte).Trim();

                string[] vesselSpl = _vesselRef.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                if (vesselSpl.Length == 4)
                {
                    vessel = vesselSpl[1].ToString().Trim();
                    reference = vesselSpl[3].ToString().Trim();
                }
            }
            

            // Subject
            idx_lts = mailBodyStr.IndexOf("Description");
            idx_lte = mailBodyStr.IndexOf("Spare Type");
            int idx_lte_gs = mailBodyStr.IndexOf("Date of Delivery");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                string subjStr12 = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Trim();

                string[] subjStrSpl = subjStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                if (subjStrSpl.Length == 8)
                {
                    if (subjStrSpl[0].ToString().Equals("Description"))
                        nameStr = subjStrSpl[1].ToString().Trim();

                    if (subjStrSpl[2].ToString().Equals("Maker"))
                        makerStr = subjStrSpl[3].ToString().Trim();

                    if (subjStrSpl[4].ToString().Equals("Model"))
                        modelStr = subjStrSpl[5].ToString().Trim();

                    if (subjStrSpl[6].ToString().Equals("Serial No."))
                        serialStr = subjStrSpl[7].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(nameStr))
                    iTemSUBJ = nameStr.Trim();

                if (!string.IsNullOrEmpty(makerStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                if (!string.IsNullOrEmpty(modelStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                if (!string.IsNullOrEmpty(serialStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();
            }
            else if (!idx_lts.Equals(-1) && !idx_lte_gs.Equals(-1))
            {
                string subjStr2 = mailBodyStr.Substring(idx_lts, idx_lte_gs - idx_lts).Trim();

                string[] subjStrSpl = subjStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                if (subjStrSpl.Length == 2)
                {
                    if (subjStrSpl[0].ToString().Equals("Description"))
                        nameStr = subjStrSpl[1].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(nameStr))
                    iTemSUBJ = nameStr.Trim();
            }



            //Item
            idx_lts = mailBodyStr.IndexOf("Unit Price");
            idx_lte = mailBodyStr.IndexOf("Your Ref.");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                string itemStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Unit Price", "").Trim();

                string[] descSpl = itemStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                int cycleCount = descSpl.Length / 5;
                int resultInt = descSpl.Length % 5;

                if (resultInt.Equals(0))
                {
                    for (int c = 0; c < cycleCount; c++)
                    {
                        iTemNo = descSpl[c * 5].Trim();
                        iTemDesc = descSpl[(c*5) + 1].Trim();

                        string[] qtSpl = descSpl[(c*5) + 2].Split(' ');

                        if (qtSpl.Length == 2)
                        {
                            iTemQt = qtSpl[0].Trim();
                            iTemUnit = qtSpl[1].Trim();
                        }


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemNo = string.Empty;
                        iTemDesc = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                    }
                }
            }
        }
    }
}
