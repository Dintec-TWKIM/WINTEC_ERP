using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class KGJS : InquiryBase
    {
        public KGJS(string fileName) 
            : base(fileName)
        {

        }

        public override void ParseItem(DataTable dt, int rowIndex)
        {
            if (dt.Rows[rowIndex][2].ToString().StartsWith("QUERY NO:"))
                this._reference = dt.Rows[rowIndex][2].ToString().Split(':')[1].Trim();

            if (dt.Rows[rowIndex][2].ToString().StartsWith("Delivery Adr: Vessel:"))
                this._vessel = dt.Rows[rowIndex][2].ToString().Split(':')[2].Split('/')[0].Trim();

            if (dt.Rows[rowIndex][2].ToString().StartsWith("Name:"))
            {
                string subject = "FOR";

                if (!string.IsNullOrEmpty(dt.Rows[rowIndex + 3][3].ToString()))
                    subject += " Maker: " + dt.Rows[rowIndex + 3][3].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex + 4][3].ToString()))
                    subject += " Comp Maker: " + dt.Rows[rowIndex + 4][3].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex + 5][3].ToString()))
                    subject += " Comp Name: " + dt.Rows[rowIndex + 5][3].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex + 6][3].ToString()))
                    subject += " Type: " + dt.Rows[rowIndex + 6][3].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex + 7][3].ToString()))
                    subject += " Serial: " + dt.Rows[rowIndex + 7][3].ToString();

                base._dtItem.Rows.Add();
                base._dtItem.Rows[_dtItem.Rows.Count - 1]["NO"] = dt.Rows[rowIndex - 1][0].ToString();
                if (subject != "FOR")
                    base._dtItem.Rows[base._dtItem.Rows.Count - 1]["SUBJ"] = subject;
                base._dtItem.Rows[base._dtItem.Rows.Count - 1]["ITEM"] = dt.Rows[rowIndex + 1][3].ToString();
                base._dtItem.Rows[_dtItem.Rows.Count - 1]["DESC"] = dt.Rows[rowIndex][3].ToString();
                base._dtItem.Rows[base._dtItem.Rows.Count - 1]["UNIT"] = base._uc.Convert(dt.Rows[rowIndex - 1][2].ToString());
                base._dtItem.Rows[base._dtItem.Rows.Count - 1]["QT"] = dt.Rows[rowIndex - 1][1].ToString();
            }
        }
    }
}
