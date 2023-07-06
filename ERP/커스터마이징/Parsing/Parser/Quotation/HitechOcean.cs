using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class HitechOcean
    {
        DataTable dtIteml;

        string lt;
        string rmk;
        string reference;
        string currency;

        string fileName;
        UnitConverter uc;


        #region ==================================================================================================== Property

        public DataTable ItemL
        {
            get
            {
                return dtIteml;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public string Rmk
        {
            get
            {
                return rmk;
            }
        }

        public string Lt
        {
            get
            {
                return lt;
            }
        }

        public string Currency
        {
            get
            {
                return currency;
            }
        }

        #endregion ==================================================================================================== Constructor



        public HitechOcean(string fileName)
        {
            lt = string.Empty;
            rmk = string.Empty;
            reference = string.Empty;
            currency = string.Empty;

            dtIteml = new DataTable();
            dtIteml.Columns.Add("NO");          // 순번
            dtIteml.Columns.Add("DESC");        // 품목명
            dtIteml.Columns.Add("ITEM");        // 품목코드
            dtIteml.Columns.Add("UNIT");        // 단위
            dtIteml.Columns.Add("QT");          // 수량
            dtIteml.Columns.Add("UNIQ");          // 고유코드
            dtIteml.Columns.Add("UM");          // 단가
            dtIteml.Columns.Add("AM");          // 금액
            dtIteml.Columns.Add("LT");          // 납기
            dtIteml.Columns.Add("RMK");         // 비고
            dtIteml.Columns.Add("CHARGE");      // 부대비용

            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemUm = string.Empty;
            string iTemDC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemRMK = string.Empty;
            string iTemAm = string.Empty;
            string iTemDesc = string.Empty;
            string iTemTotal = string.Empty;
            string iTemCode = string.Empty;
            string iTemType = string.Empty;
            string iTemUniq = string.Empty;
            string iTemLt = string.Empty;
            string iTemCharge = string.Empty;

            string iTemRef = string.Empty;
            string itemRMKH = string.Empty;
            string leadTimeStr = string.Empty;



            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body;

            int idx_lts = mailBodyStr.IndexOf("발주 후");
            int idx_lte = mailBodyStr.IndexOf("(배송비");

            if (!idx_lts.Equals(-1))
            {

                string ltStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("발주 후", "").Replace(":", "").Trim();

                if (ltStr.ToUpper().Contains("STOCK"))
                {
                    lt = "3";
                }
                else if (ltStr.Contains("주"))
                {
                    int _lt = Convert.ToInt16(ltStr.Replace("주", "")) * 7;

                    lt = Convert.ToString(_lt).Trim();
                }
                else if (ltStr.Contains("일"))
                {

                }
            }
            


            int idx_s = mailBodyStr.IndexOf("연락");

            if (idx_s > 1)
            {

                int idx_e = mailBodyStr.IndexOf("견적", idx_s);
                string mailSpl = mailBodyStr.Substring(idx_s, idx_e - idx_s).Trim();

                mailSpl = mailSpl.Replace("연락 주십시오.", "").Replace("\r\n\r\n", "\r\n").Trim();

                if (mailSpl.EndsWith("*"))
                    mailSpl = mailSpl.Substring(0, mailSpl.Length - 1).Trim();

                if (mailSpl.Length > 20)
                {
                    if (mailSpl.Contains("1)"))
                        idx_s = mailSpl.IndexOf("1)");
                    else if (mailSpl.Contains("1."))
                        idx_s = mailSpl.IndexOf("1.");

                    idx_e = mailSpl.Length;


                    string mailSpl2 = mailSpl.Substring(idx_s, idx_e - idx_s).Replace("/ \r\n", "/ ").Replace("=\r\n", "= ").Replace("/\r\n", "/ ").Replace("PN:\r\n", "PN:").Replace("PN: \r\n", "PN:").Replace("\r\n=", " =").Replace("\r\n/", "/").Trim();

                    string[] mailEndSpl = mailSpl2.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    if (mailSpl2.Length < 45)
                        goto Exit;

                    for (int r = 0; r < mailEndSpl.Length; r++)
                    {
                        // 순번 나누기
                        string[] noSpl = mailEndSpl[r].Split(new string[] { "\t" }, StringSplitOptions.None);

                        if (noSpl.Length == 1)
                        {
                            noSpl = mailEndSpl[r].Split(new string[] { "     " }, StringSplitOptions.None);

                            if (noSpl.Length == 1)
                            {
                                noSpl = mailEndSpl[r].Split(new string[] { "    " }, StringSplitOptions.None);

                                if(noSpl.Length == 1)
                                    noSpl = mailEndSpl[r].Split(new string[] { "   " }, StringSplitOptions.None);
                            }
                        }

                        if (noSpl.Length > 1)
                        {
                            iTemNo = noSpl[0].ToString().Replace(".", "").Replace(")", "").Trim();

                            if (!GetTo.IsInt(iTemNo))
                                break;

                            if (noSpl[1].Contains("/"))
                            {
                                // 품목명 나누기
                                string[] descSpl = noSpl[1].Split(new string[] { "/ " }, StringSplitOptions.None);

                                if (descSpl.Length > 1)
                                {
                                    iTemDesc = descSpl[0].ToString().Trim();

                                    if (descSpl[1].Contains("="))
                                    {
                                        string[] umSpl = descSpl[1].Split('=');

                                        if (umSpl.Length > 1)
                                        {
                                            umSpl[0] = umSpl[0].Trim();

                                            iTemQt = umSpl[0].Substring(0, 1).Trim();

                                            iTemUnit = umSpl[0].Replace(iTemQt, "").Trim();

                                            iTemUm = umSpl[1].ToString().Replace("->", "").Trim();
                                        }
                                    }
                                    else if (descSpl[1].Contains("PN"))
                                    {
                                        iTemCode = descSpl[1].ToString().Replace("PN", "").Replace(":", "").Trim();
                                        //iTemUm = descSpl[2].ToString().Replace("=", "").Trim();

                                        if (descSpl[2].Contains("="))
                                        {
                                            string[] umSpl = descSpl[2].Split('=');

                                            if (umSpl.Length > 1)
                                            {
                                                umSpl[0] = umSpl[0].Trim();

                                                iTemQt = umSpl[0].Substring(0, 1).Trim();

                                                iTemUnit = umSpl[0].Replace(iTemQt, "").Trim();

                                                iTemUm = umSpl[1].ToString().Trim();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        iTemQt = "0";
                                        iTemUm = "0";
                                    }
                                }
                            }


                            if (iTemUm.Contains("->"))
                            {
                                idx_e = iTemUm.IndexOf("->");
                                iTemUm = iTemUm.Substring(0, idx_e).Trim();
                            }

                            iTemAm = "0";

                            //ITEM ADD START
                            dtIteml.Rows.Add();

                            dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = iTemNo;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = "";
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = iTemLt;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["RMK"] = iTemRMK;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["CHARGE"] = iTemCharge;

                            iTemDC = string.Empty;
                            iTemUnit = string.Empty;
                            iTemRMK = string.Empty;
                            iTemQt = string.Empty;
                            iTemUm = string.Empty;
                            iTemAm = string.Empty;
                            iTemCode = string.Empty;
                            iTemDesc = string.Empty;
                        }
                    }

                Exit:
                    string test = string.Empty;
                }
            }
        }
    }
}
