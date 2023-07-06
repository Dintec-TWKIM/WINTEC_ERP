using System;

namespace cz
{
    internal class P_CZ_FI_EPDOCU_CHECK
    {
        internal bool BL<T>(T a, T b, T c) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 && a.CompareTo(c) <= 0;
        }

        internal int HfxGetLastDay(int nYear, int nMonth)
        {
            return DateTime.DaysInMonth(nYear, nMonth);
        }

        internal bool IsNumeric(string value)
        {
            if (value == null || value == string.Empty)
                return false;
            value = value.Replace(".", "");
            value = value.Replace("-", "");
            value = value.Replace("/", "");
            value = value.Replace(",", "");
            foreach (char c in value)
            {
                if (!char.IsNumber(c))
                    return false;
            }
            return true;
        }

        internal bool HfxIsDate(int y, int m, int d)
        {
            return this.BL<int>(y, 1900, 2100) && this.BL<int>(m, 1, 12) && this.BL<int>(d, 1, this.HfxGetLastDay(y, m));
        }

        internal bool HfxIsDate(string sDate)
        {
            sDate = sDate.Replace("-", "");
            sDate = sDate.Replace("/", "");
            if (sDate.Length != 8 || !this.IsNumeric(sDate))
                return false;
            int[] numArray = new int[3]
      {
        int.Parse(sDate.Substring(0, 4)),
        int.Parse(sDate.Substring(4, 2)),
        int.Parse(sDate.Substring(6, 2))
      };
            return this.HfxIsDate(numArray[0], numArray[1], numArray[2]);
        }

        internal bool IsEmpty(string str)
        {
            return str == null || str.Length == 0;
        }

        internal string MarkComma(string str)
        {
            if (this.IsEmpty(str))
                return "";
            bool flag = str.Substring(0, 1) == "-";
            if (flag)
                str = str.Substring(1, str.Length - 1);
            string str1 = "";
            string[] strArray = str.Split('.');
            for (int index = 0; index < strArray[0].Length; ++index)
            {
                if (index % 3 == 0 && index != 0 && index != strArray[0].Length)
                    str1 = str1.Insert(0, ",");
                str1 = str1.Insert(0, strArray[0].Substring(strArray[0].Length - (index + 1), 1));
            }
            if (strArray.Length > 1)
                str1 = str1 + "." + strArray[1];
            if (flag)
                str1 = "-" + str1;
            return str1;
        }

        internal string MarkComma(string str, int num, bool state)
        {
            if (this.IsEmpty(str))
                return "";
            bool flag = str.Substring(0, 1) == "-";
            if (flag)
                str = str.Substring(1, str.Length - 1);
            string str1 = "";
            string[] strArray = str.Split('.');
            for (int index = 0; index < strArray[0].Length; ++index)
            {
                if (index % 3 == 0 && index != 0 && index != strArray[0].Length)
                    str1 = str1.Insert(0, ",");
                str1 = str1.Insert(0, strArray[0].Substring(strArray[0].Length - (index + 1), 1));
            }
            if (strArray.Length > 1)
                str1 = str1 + "." + strArray[1];
            if (state)
            {
                if (strArray.Length < 2)
                {
                    str1 += ".";
                    for (int index = 0; index < num; ++index)
                        str1 += "0";
                }
                else
                {
                    for (int length = strArray[1].Length; length < num; ++length)
                        str1 += "0";
                }
            }
            if (flag)
                str1 = "-" + str1;
            return str1;
        }
    }
}