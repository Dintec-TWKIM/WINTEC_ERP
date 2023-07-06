using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Donghwa_mail
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



        public Donghwa_mail(string fileName)
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
            string _count = "01";

            int _splCount = -1;
            int _splQt = -1;
            int _splUnit = -1;
            int _splUm = -1;
            int _splCode = -1;

            bool noState = false;


            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body;

            int idx_s = mailBodyStr.IndexOf("발주후");

            if (idx_s > 1)
            {

                int idx_e = mailBodyStr.IndexOf("감사합니다", idx_s);
                string mailSpl = mailBodyStr.Substring(idx_s, idx_e - idx_s).Trim();

                if (mailSpl.Length > 20)
                {
                    mailSpl = mailSpl.Replace("\r\n", " ").Trim();


                    idx_s = mailSpl.IndexOf("일");
                    idx_e = mailSpl.Length;





                    lt = mailSpl.Substring(idx_s - 1, 1).Trim();

                    string mailSpl2 = mailSpl.Substring(idx_s, idx_e - idx_s).Replace("일", "").Replace("  ", " ").Replace("   ", " ").Replace("    ", " ").Replace("     ", " ").Replace("      ", " ").Replace("       ", " ").Replace("        ", " ").Trim();
                    mailSpl2 = mailSpl2.Replace("  ", " ").Trim();
                    mailSpl2 = mailSpl2.Replace("  ", " ").Trim();

                    mailSpl2 = mailSpl2.Replace("품번", "").Trim();

                    if (mailSpl2.Contains("1EA") || mailSpl2.Contains("1SET"))
                        mailSpl2 = mailSpl2.Replace("1EA", "1 EA").Replace("1SET", "1 SET").Trim();


                    if (mailSpl2.StartsWith("A") || mailSpl2.StartsWith("B") || !GetTo.IsInt(mailSpl2.Substring(0, 1).ToString()))
                    {

                        mailSpl2 = _count + " " + mailSpl2.Trim();

                        int __countR = Convert.ToInt16(_count) + 1;

                        _count = "0" + Convert.ToString(__countR);
                    }

                    if (mailSpl2.Length < 55)
                        goto Exit;

                    // 단위랑 수량이 있나 없나 구분 - 단가로 구분
                    if (!mailSpl2.Contains("EA") || !mailSpl2.Contains("SET"))
                    {
                        string[] mailRealSpl = mailSpl2.Split(' ');

                        for (int c = 0; c < mailRealSpl.Length; c++)
                        {
                            for (int r = c; r < mailRealSpl.Length; r++)
                            {
                                if (mailRealSpl[r].ToString().Length.Equals(2) && GetTo.IsInt(mailRealSpl[r].ToString()))
                                {
                                    _splCount = r;
                                    _splCode = r + 1;
                                }
                                else if (mailRealSpl[r].ToString().Contains(",") && mailRealSpl[r].ToString().Contains(".") && !mailRealSpl[r].ToString().Contains("*") && !mailRealSpl[r].ToString().Contains("-"))
                                {
                                    if (GetTo.IsInt(mailRealSpl[r].ToString().Substring(0, 1)) || mailRealSpl[r].ToString().StartsWith("\\"))
                                    {
                                        _splUm = r;

                                        if (_splCount.Equals(-1))
                                        {
                                            noState = true;
                                            _splCount = c;
                                            _splCode = c;
                                        }
                                    }
                                }

                                if (!_splCount.Equals(-1) && !_splUm.Equals(-1))
                                    break;
                            }


                            if (noState)
                            {
                                if (_count.Length == 1)
                                {
                                    iTemNo = "0" + Convert.ToString(_count);
                                }
                                else
                                {
                                    iTemNo = Convert.ToString(_count);
                                }

                                int __countR = Convert.ToInt16(_count) + 1;

                                _count = Convert.ToString(__countR);

                            }
                            else
                            {
                                iTemNo = mailRealSpl[_splCount].ToString().Trim();
                            }

                            iTemCode = mailRealSpl[_splCode].ToString().Trim();

                            for (int _desc = _splCode + 1; _desc < _splUm; _desc++)
                            {
                                iTemDesc = iTemDesc.Trim() + " " + mailRealSpl[_desc].ToString().Trim();
                            }

                            iTemQt = "1";
                            iTemUnit = "EA";
                            iTemUm = mailRealSpl[_splUm].ToString().Trim();
                            iTemAm = "0";

                            c = _splUm;

                            iTemUm = iTemUm.Replace("\\", "").Trim();

                            iTemDesc = iTemDesc.Replace("1 EA", "").Trim();

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

                            _splCount = -1;
                            _splQt = -1;
                            _splUm = -1;
                            _splUnit = -1;
                            _splCode = -1;

                            noState = false;


                        }
                    }
                    else
                    {
                        string[] mailRealSpl = mailSpl2.Split(' ');

                        for (int c = 0; c < mailRealSpl.Length; c++)
                        {
                            for (int r = c; r < mailRealSpl.Length; r++)
                            {
                                if (mailRealSpl[r].ToString().Length.Equals(2) && GetTo.IsInt(mailRealSpl[r].ToString()))
                                {
                                    _splCount = r;
                                    _splCode = r + 1;
                                }
                                else if (mailRealSpl[r].ToString().StartsWith("1") && !mailRealSpl[r].ToString().Contains("*") && !mailRealSpl[r].ToString().Contains("-"))
                                {
                                    _splQt = r;
                                    _splUnit = r + 1;
                                    _splUm = r + 2;

                                    if (_splCount.Equals(-1))
                                    {
                                        noState = true;
                                        _splCount = c;
                                        _splCode = c;
                                    }
                                }

                                if (!_splCount.Equals(-1) && !_splQt.Equals(-1))
                                    break;
                            }


                            if (noState)
                            {
                                if (_count.Length == 1)
                                {
                                    iTemNo = "0" + Convert.ToString(_count);
                                }
                                else
                                {
                                    iTemNo = Convert.ToString(_count);
                                }

                                int __countR = Convert.ToInt16(_count) + 1;

                                _count = Convert.ToString(__countR);

                            }
                            else
                            {
                                iTemNo = mailRealSpl[_splCount].ToString().Trim();
                            }

                            iTemCode = mailRealSpl[_splCode].ToString().Trim();

                            for (int _desc = _splCode + 1; _desc < _splQt; _desc++)
                            {
                                iTemDesc = iTemDesc.Trim() + " " + mailRealSpl[_desc].ToString().Trim();
                            }

                            iTemQt = mailRealSpl[_splQt].ToString().Trim();
                            iTemUnit = mailRealSpl[_splUnit].ToString().Trim();
                            iTemUm = mailRealSpl[_splUm].ToString().Trim();
                            iTemAm = "0";

                            c = _splUm;

                            iTemUm = iTemUm.Replace("\\", "").Trim();

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

                            _splCount = -1;
                            _splQt = -1;
                            _splUm = -1;
                            _splUnit = -1;
                            _splCode = -1;

                            noState = false;
                        }
                    }

                Exit:
                    string test = string.Empty;
                }
            }
        }
    }
}
