using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class GOE : InquiryBase
    {
        string subject = "FOR";

        public GOE(string fileName)
            : base(fileName)
        {

        }

        public override void ParseItem(DataTable dt, int rowIndex)
        {
            if (dt.Rows[rowIndex][0].ToString().StartsWith("REQUEST FOR QUOTATION:"))
            {
                

                if (string.IsNullOrEmpty(this._reference))
                {
                    this._reference = dt.Rows[rowIndex][6].ToString();

                    if(string.IsNullOrEmpty(_reference))
                        _reference = dt.Rows[rowIndex][5].ToString();
                }

                for (int index = rowIndex + 1; index < dt.Rows.Count; index ++)
                {
                    if (dt.Rows[index][0].ToString().StartsWith("REQUEST FOR QUOTATION:"))
                        break;

                    if (dt.Rows[index][0].ToString().StartsWith("Rig Name :"))
                        this._vessel = dt.Rows[index][0].ToString().Split(':')[1].Trim();

                    if (dt.Rows[index][0].ToString().StartsWith("Maker:") || dt.Rows[index][0].ToString().StartsWith("MANUFACTURER:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if(!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Remarks:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Tech."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Maker's"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Model:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Serial"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Location"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }

                    if (dt.Rows[index][0].ToString().StartsWith("Comment for"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[index][c].ToString().Trim()))
                                subject = subject + Environment.NewLine + dt.Rows[index][c].ToString().Trim();
                        }
                    }



                    decimal number = 0;

                    if (decimal.TryParse(dt.Rows[index][0].ToString(), out number) &&
                        base._dtItem.Select("NO = '" + number.ToString() + "'").Length == 0)
                    {
                        
                        string no = number.ToString();
                        string item = string.Empty;
                        string desc = string.Empty;
                        string qty = string.Empty;
                        string unit = string.Empty;

                        for (int index1 = 1; index1 < dt.Columns.Count; index1 ++)
                        {
                            if (decimal.TryParse(dt.Rows[index][index1].ToString(), out number))
                            {
                                qty = dt.Rows[index][index1].ToString();
                                unit = dt.Rows[index][index1 + 1].ToString().Replace("-","").Trim();
                                break;
                            }
                        }

                        for (int index1 = index; index1 < dt.Rows.Count; index1 ++)
                        {
                            if (decimal.TryParse(dt.Rows[index1][0].ToString(), out number) && 
                                number.ToString() != no)
                                break;

                            


                            if (dt.Rows[index1][1].ToString().StartsWith("Material item number :"))
                            {
                                item = dt.Rows[index1][2].ToString();

                                for (int index2 = index; index2 < index1; index2 ++)
                                {
                                    if (dt.Rows[index2][0].ToString().StartsWith("Address:") ||
                                        dt.Rows[index2][0].ToString().StartsWith("Web : ") ||
                                        dt.Rows[index2][0].ToString().StartsWith("ITEM") ||
                                        dt.Rows[index2][0].ToString().StartsWith("REQUEST FOR QUOTATION:"))
                                        continue;

                                    if (!string.IsNullOrEmpty(dt.Rows[index2][0].ToString()) &&
                                        !decimal.TryParse(dt.Rows[index2][0].ToString(), out number))
                                        desc += dt.Rows[index2][0].ToString() + " ";

                                    if (!string.IsNullOrEmpty(dt.Rows[index2][1].ToString()))
                                        desc += dt.Rows[index2][1].ToString() + " ";

                                    if (!string.IsNullOrEmpty(dt.Rows[index2][2].ToString()) &&
                                        !decimal.TryParse(dt.Rows[index2][2].ToString(), out number))
                                        desc += dt.Rows[index2][2].ToString() + " ";
                                }

                                for (int index2 = index1 + 1; index2 < dt.Rows.Count; index2 ++)
                                {
                                    if (decimal.TryParse(dt.Rows[index2][0].ToString(), out number) ||
                                        dt.Rows[index2][0].ToString().StartsWith("SUBTOTAL :"))
                                        break;

                                    if (dt.Rows[index1 + 1][1].ToString().StartsWith("Line Notes :"))
                                    {
                                        if (decimal.TryParse(dt.Rows[index2][0].ToString(), out number) ||
                                            dt.Rows[index2][0].ToString().StartsWith("SUBTOTAL :"))
                                            break;

                                        if (dt.Rows[index2][1].ToString().StartsWith("Line Notes :") ||
                                            dt.Rows[index2][0].ToString().StartsWith("Address:") ||
                                            dt.Rows[index2][0].ToString().StartsWith("Web : ") ||
                                            dt.Rows[index2][0].ToString().StartsWith("ITEM") ||
                                            dt.Rows[index2][0].ToString().StartsWith("REQUEST FOR QUOTATION:"))
                                            continue;

                                        subject += " " + dt.Rows[index2][1].ToString();
                                    }
                                    else if (dt.Rows[index2][0].ToString().EndsWith("Line Notes :"))
                                    {
                                        for (int index3 = index2 + 1; index3 < dt.Rows.Count; index3++)
                                        {
                                            if (decimal.TryParse(dt.Rows[index3][0].ToString(), out number) ||
                                                dt.Rows[index3][0].ToString().StartsWith("SUBTOTAL :"))
                                                break;

                                            subject += " " + dt.Rows[index3][0].ToString();
                                        }
                                    }
                                }
                            }
                            else if (dt.Rows[index1][0].ToString().StartsWith("Material item number :"))
                            {
                                item = dt.Rows[index1][1].ToString();

                                for (int index2 = index; index2 < index1; index2++)
                                {
                                    if (dt.Rows[index2][0].ToString().StartsWith("Address:") ||
                                        dt.Rows[index2][0].ToString().StartsWith("Web : ") ||
                                        dt.Rows[index2][0].ToString().StartsWith("ITEM") ||
                                        dt.Rows[index2][0].ToString().StartsWith("REQUEST FOR QUOTATION:"))
                                        continue;

                                    if (!string.IsNullOrEmpty(dt.Rows[index2][0].ToString()) &&
                                        !decimal.TryParse(dt.Rows[index2][0].ToString(), out number))
                                        desc += dt.Rows[index2][0].ToString() + " ";

                                    if (!string.IsNullOrEmpty(dt.Rows[index2][1].ToString()))
                                        desc += dt.Rows[index2][1].ToString() + " ";

                                    if (!string.IsNullOrEmpty(dt.Rows[index2][2].ToString()) &&
                                        !decimal.TryParse(dt.Rows[index2][2].ToString(), out number))
                                        desc += dt.Rows[index2][2].ToString() + " ";
                                }

                                for (int index2 = index1 + 1; index2 < dt.Rows.Count; index2++)
                                {
                                    if (decimal.TryParse(dt.Rows[index2][0].ToString(), out number) ||
                                        dt.Rows[index2][0].ToString().StartsWith("SUBTOTAL :"))
                                        break;

                                    if (dt.Rows[index1 + 1][1].ToString().StartsWith("Line Notes :"))
                                    {
                                        if (decimal.TryParse(dt.Rows[index2][0].ToString(), out number) ||
                                            dt.Rows[index2][0].ToString().StartsWith("SUBTOTAL :"))
                                            break;

                                        if (dt.Rows[index2][1].ToString().StartsWith("Line Notes :") ||
                                            dt.Rows[index2][0].ToString().StartsWith("Address:") ||
                                            dt.Rows[index2][0].ToString().StartsWith("Web : ") ||
                                            dt.Rows[index2][0].ToString().StartsWith("ITEM") ||
                                            dt.Rows[index2][0].ToString().StartsWith("REQUEST FOR QUOTATION:"))
                                            continue;

                                        subject += " " + dt.Rows[index2][1].ToString();
                                    }
                                    else if (dt.Rows[index2][0].ToString().EndsWith("Line Notes :"))
                                    {
                                        for (int index3 = index2 + 1; index3 < dt.Rows.Count; index3++)
                                        {
                                            if (decimal.TryParse(dt.Rows[index3][0].ToString(), out number) ||
                                                dt.Rows[index3][0].ToString().StartsWith("SUBTOTAL :"))
                                                break;

                                            subject += " " + dt.Rows[index3][0].ToString();
                                        }
                                    }
                                }
                            }
                        }

                        base._dtItem.Rows.Add();
                        base._dtItem.Rows[_dtItem.Rows.Count - 1]["NO"] = no;
                        if (subject != "FOR")
                            base._dtItem.Rows[base._dtItem.Rows.Count - 1]["SUBJ"] = subject;
                        base._dtItem.Rows[_dtItem.Rows.Count - 1]["DESC"] = desc.Trim();
                        base._dtItem.Rows[base._dtItem.Rows.Count - 1]["ITEM"] = item;
                        base._dtItem.Rows[base._dtItem.Rows.Count - 1]["UNIT"] = base._uc.Convert(unit);
                        base._dtItem.Rows[base._dtItem.Rows.Count - 1]["QT"] = qty;
                    }
                }
            }
        }
    }
}
