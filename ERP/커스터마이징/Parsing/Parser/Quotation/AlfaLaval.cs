using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class AlfaLaval
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



        public AlfaLaval(string fileName)
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
            int idx_s = 0;
            int idx_e = 0;

            if (mailBodyStr.Contains("Shipment/Delivery"))
            {
                idx_s = mailBodyStr.IndexOf("Shipment/Delivery");
                idx_e = mailBodyStr.IndexOf("after receipt");

                string ltStr = mailBodyStr.Substring(idx_s, idx_e - idx_s).Trim();

                ltStr = ltStr.Replace("Shipment/Delivery:", "").Replace("Within", "").Replace(":", "").Replace("weeks", "").Trim();

                if (ltStr.Contains("~"))
                {
                    ltStr = ltStr.Substring(2, 1).Trim();

                    if (ltStr.Length == 1)
                        ltStr = Convert.ToString(Convert.ToInt16(ltStr) * 7);
                }
                lt = ltStr.Trim();
            }


            if (mailBodyStr.Contains("Packing:"))
            {
                idx_s = mailBodyStr.IndexOf("KRW");
                idx_e = mailBodyStr.IndexOf("Packing:");

                if (idx_s == -1)
                {
                    idx_s = mailBodyStr.IndexOf("Old");
                }
            }
            else if (mailBodyStr.Contains("Delivery"))
            {
                idx_s = mailBodyStr.IndexOf("KRW");
                idx_e = mailBodyStr.IndexOf("Delivery");
            }

            else if (mailBodyStr.Contains("New\r\n\r\n1"))
            {
                idx_s = mailBodyStr.IndexOf("New");
                idx_e = mailBodyStr.IndexOf("From: service@dintec.co.kr");
            }
            else if (mailBodyStr.Contains("\r\n\r\nNew\r\n\r\nOld\r\n\r\n　\r\n\r\n1") || mailBodyStr.Contains("\r\n\r\nNew\r\n\r\nOld\r\n\r\n"))
            {
                idx_s = mailBodyStr.IndexOf("Old");
                idx_e = mailBodyStr.IndexOf("Best regards");

                if (idx_e == -1)
                    idx_e = mailBodyStr.IndexOf("Best Regards");

                if (idx_e == -1)
                    idx_e = mailBodyStr.IndexOf("From: service@dintec.co.kr");

            }
            else if (mailBodyStr.Contains("\r\n\r\nNew\r\n\r\nOld\r\n\r\n1"))
            {
                idx_s = mailBodyStr.IndexOf("Part No");
                idx_e = mailBodyStr.IndexOf("Best Regards");

                if (idx_e == -1)
                    idx_e = mailBodyStr.IndexOf("Best regards");

                if (idx_e == -1)
                    idx_e = mailBodyStr.IndexOf("best");

                if (idx_e == -1)
                    idx_e = mailBodyStr.IndexOf("From");
            }
            else if (mailBodyStr.Contains("\r\n\r\nT/Total\r\n\r\n"))
            {
                idx_s = mailBodyStr.IndexOf("KRW");
                idx_e = mailBodyStr.IndexOf("From: service@dintec.co.kr");

                if (mailBodyStr.Contains("HANDLING"))
                    idx_e = mailBodyStr.IndexOf("HANDLING FEE");
            }
            else if (mailBodyStr.Contains("견적이 어렵습니다."))
            {
                return;
            }
            else if (mailBodyStr.Contains("Remark"))
            {
                idx_s = mailBodyStr.IndexOf("Remark");
                idx_e = mailBodyStr.IndexOf("From: service@dintec.co.kr");
            }



            if (idx_s > 1)
            {
                string mailSpl = mailBodyStr.Substring(idx_s, idx_e - idx_s).Trim();


                // 모든게 다 있는 옵션 1
                if (mailSpl.StartsWith("KRW"))
                {
                    mailSpl = mailSpl.Replace("KRW", "").Replace(" ", "").Replace("　", "").Replace("\r\n\r\n", "\r\n").Trim();
                    mailSpl = mailSpl.Replace("\r\n\r\n", "\r\n").Replace("●", "").Replace("-", "").Replace("*", "").Trim();

                    string[] mailEndSpl = mailSpl.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    for (int r = 0; r < mailEndSpl.Length; r++)
                    {
                        if (r + 2 >= mailEndSpl.Length - 1)
                            break;


                        iTemNo = mailEndSpl[r];
                        iTemDesc = mailEndSpl[r + 1];
                        iTemCode = mailEndSpl[r + 2];

                        if (string.IsNullOrEmpty(iTemCode))
                        {
                            iTemQt = "0";
                            iTemUm = "0";
                            iTemAm = "0";

                            r += 3;
                        }
                        else if (mailEndSpl[r + 3].ToString().Length < 4)
                        {
                            iTemQt = mailEndSpl[r + 3];
                            iTemUm = mailEndSpl[r + 4];
                            iTemAm = mailEndSpl[r + 5];

                            r += 5;
                        }
                        else
                        {
                            iTemQt = mailEndSpl[r + 4];
                            iTemUm = mailEndSpl[r + 5];
                            iTemAm = mailEndSpl[r + 6];

                            r += 6;
                        }




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
                // DESCRIPTION + CODE
                else if (mailSpl.StartsWith("New") || mailSpl.StartsWith("Part No"))
                {
                    mailSpl = mailSpl.Replace("Part No", "").Replace("Old", "").Replace(" ", "").Replace("　", "").Replace("\r\n\r\n", "\r\n").Replace("New", "").Trim();
                    mailSpl = mailSpl.Replace("\r\n\r\n", "\r\n").Replace("●", "").Replace("-", "").Replace("*", "").Trim();

                    string[] mailEndSpl = mailSpl.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    for (int r = 0; r < mailEndSpl.Length; r++)
                    {
                        if (r + 2 >= mailEndSpl.Length - 1)
                            break;

                        iTemNo = mailEndSpl[r];

                        if (iTemNo.Length > 4)
                        {
                            iTemNo = mailEndSpl[r + 1];

                            iTemDesc = mailEndSpl[r + 2];

                            if (GetTo.IsInt(iTemDesc) && iTemDesc.Length > 6)
                            {
                                iTemDesc = "";
                                iTemCode = mailEndSpl[r + 2];
                                iTemQt = "0";
                                iTemUm = "0";
                                iTemAm = "0";

                                r += 2;
                            }
                            else
                            {

                                iTemCode = mailEndSpl[r + 3];
                                iTemQt = "0";
                                iTemUm = "0";
                                iTemAm = "0";

                                r += 3;
                            }

                        }
                        else
                        {
                            iTemDesc = mailEndSpl[r + 1];

                            if (GetTo.IsInt(iTemDesc) && iTemDesc.Length > 6)
                            {
                                iTemDesc = "";
                                iTemCode = mailEndSpl[r + 1];
                                iTemQt = "0";
                                iTemUm = "0";
                                iTemAm = "0";

                                r += 1;
                            }
                            else
                            {

                                iTemCode = mailEndSpl[r + 2];
                                iTemQt = "0";
                                iTemUm = "0";
                                iTemAm = "0";

                                r += 2;
                            }
                        }


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
                // DESCRIPTION + CODE + QT
                else if (mailSpl.StartsWith("Old"))
                {
                    mailSpl = mailSpl.Replace("Part No", "").Replace("Old", "").Replace(" ", "").Replace("　", "").Replace("\r\n\r\n", "\r\n").Replace("New", "").Trim();
                    mailSpl = mailSpl.Replace("\r\n\r\n", "\r\n").Replace("●", "").Replace("-", "").Replace("*", "").Trim();

                    string[] mailEndSpl = mailSpl.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    for (int r = 0; r < mailEndSpl.Length; r++)
                    {
                        if (r + 2 >= mailEndSpl.Length - 1)
                            break;


                        iTemNo = mailEndSpl[r].ToString().Replace(".", "");
                        iTemDesc = mailEndSpl[r + 1];

                        if (iTemDesc.Contains("도면") || iTemDesc.Equals("N/A"))
                        {
                            r += 2;
                            iTemQt = "0";
                        }
                        else
                        {

                            if (GetTo.IsInt(iTemDesc) && iTemDesc.Length > 6)
                            {
                                iTemDesc = "";
                                iTemCode = mailEndSpl[r + 1];
                                iTemQt = mailEndSpl[r + 2];

                                if (iTemQt.Length > 5)
                                {
                                    iTemQt = mailEndSpl[r + 3];
                                    r += 3;
                                }
                                else
                                {
                                    r += 2;
                                }
                            }
                            else
                            {
                                iTemCode = mailEndSpl[r + 2];
                                iTemQt = mailEndSpl[r + 3];

                                if (iTemQt.Length > 5)
                                {
                                    iTemQt = mailEndSpl[r + 4];
                                    r += 4;
                                }
                                else
                                {
                                    r += 3;
                                }
                            }
                        }

                        iTemUm = "0";
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
                // DESCRIPTION + NEW + OLD
                else if (mailSpl.StartsWith("Part No"))
                {
                    mailSpl = mailSpl.Replace("Old", "").Replace(" ", "").Replace("　", "").Replace("\r\n\r\n", "\r\n").Replace("New", "").Trim();
                    mailSpl = mailSpl.Replace("\r\n\r\n", "\r\n").Replace("●", "").Replace("-", "").Replace("*", "").Trim();

                    string[] mailEndSpl = mailSpl.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    for (int r = 0; r < mailEndSpl.Length; r++)
                    {
                        if (r + 2 >= mailEndSpl.Length - 1)
                            break;


                        iTemNo = mailEndSpl[r];
                        iTemDesc = mailEndSpl[r + 1];

                        if (GetTo.IsInt(iTemDesc) && iTemDesc.Length > 6)
                        {
                            iTemDesc = "";
                            iTemCode = mailEndSpl[r + 1];
                            iTemQt = mailEndSpl[r + 2];
                            iTemUm = "0";
                            iTemAm = "0";

                            r += 2;
                        }
                        else
                        {
                            iTemCode = mailEndSpl[r + 2];
                            iTemQt = mailEndSpl[r + 3];
                            iTemUm = "0";
                            iTemAm = "0";

                            r += 3;
                        }


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
                    // DESCRIPTION  +  CODE + REMARK
                else if (mailSpl.StartsWith("Remark"))
                {
                    mailSpl = mailSpl.Replace("Old", "").Replace(" ", "").Replace("　", "").Replace("\r\n\r\n", "\r\n").Replace("New", "").Replace("Remark", "").Trim();
                    mailSpl = mailSpl.Replace("\r\n\r\n", "\r\n").Replace("●", "").Replace("-", "").Replace("*", "").Trim();

                    string[] mailEndSpl = mailSpl.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    for (int r = 0; r < mailEndSpl.Length; r++)
                    {
                        if (r + 3 > mailEndSpl.Length - 1)
                            break;


                        iTemNo = mailEndSpl[r];
                        iTemDesc = mailEndSpl[r + 1];
                        iTemCode = mailEndSpl[r + 2];
                        iTemRMK = mailEndSpl[r + 3];

                        iTemQt = "0";
                        iTemUm = "0";
                        iTemAm = "0";

                        r += 3;

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
            }
        }
    }
}
