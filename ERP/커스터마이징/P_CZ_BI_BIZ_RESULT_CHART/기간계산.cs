using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU.OLD;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace cz
{
    internal class 기간계산
    {
        private CommonFunction cf = new CommonFunction();
        private Dictionary<int, string[]> _년월Dic = new Dictionary<int, string[]>();
        private 공통.기간구분 _E기간구분;
        private int _기준년월F;
        private int _기준년월T;
        private int _기준월;

        internal Dictionary<int, string[]> 년월Dic
        {
            get
            {
                return this._년월Dic;
            }
        }

        internal 기간계산(int 기간TYPE, string 기준년월F, string 기준년월T, bool Is기간을회계기수기준)
        {
            this._기준년월F = D.GetInt(기준년월F);
            this._기준년월T = D.GetInt(기준년월T);

            if (Is기간을회계기수기준)
            {
                string DtFrom;
                string DtTo;
                Duzon.ERPU.FI.FI.Basic.Get기수(기준년월F + "01", out DtFrom, out DtTo);
                this._기준월 = D.GetInt(DtFrom.Substring(4, 2)) - 1;
            }

            int 증가차수 = 0;
            
            switch (기간TYPE)
            {
                case 1:
                    증가차수 = 1;
                    this._E기간구분 = 공통.기간구분.월;
                    break;
                case 2:
                    증가차수 = 3;
                    this._E기간구분 = 공통.기간구분.분기;
                    break;
                case 3:
                    증가차수 = 6;
                    this._E기간구분 = 공통.기간구분.반기;
                    break;
            }

            this.계산(this._E기간구분, 증가차수);
        }

        private void 계산(공통.기간구분 기간TYPE, int 증가차수)
        {
            int num = 0;
            int next년월;

            for (int 년월F = this._기준년월F; 년월F <= this._기준년월T; 년월F = next년월)
            {
                string 년월캡션;
                int 년월;
                
                this.GetMonthAdd(년월F, this._기준년월T, 증가차수, out 년월캡션, out 년월, out next년월);
                
                string[] strArray = new string[] { 년월캡션,
                                                   D.GetString(년월F),
                                                   D.GetString(년월) };

                this._년월Dic.Add(++num, strArray);
            }
        }

        private void GetMonthAdd(int 년월F, int 년월T, int val, out string 년월캡션, out int 년월, out int next년월)
        {
            PageBase pageBase = new PageBase();
            
            if (this._E기간구분 == 공통.기간구분.월)
            {
                년월캡션 = D.GetString(년월F);
                년월 = 년월F;
                next년월 = D.GetInt(this.cf.DateAdd(년월F.ToString() + "01", "M", 1).Substring(0, 6));
            }
            else if (this._E기간구분 == 공통.기간구분.분기)
            {
                string str = D.GetString(년월F).Substring(0, 4);
                int num1 = D.GetInt(D.GetString(년월F).Substring(4, 2));
                
                if (num1 >= 1 + this._기준월 && num1 <= 3 + this._기준월)
                {
                    년월캡션 = str + "년 " + "1분기";
                    int num2 = D.GetInt(this.cf.DateAdd(str + "0301", "M", this._기준월).Substring(0, 6));
                    년월 = num2 < 년월T ? num2 : 년월T;
                }
                else if (num1 >= 4 + this._기준월 && num1 <= 6 + this._기준월)
                {
                    년월캡션 = str + "년 " + "2분기";
                    int num2 = D.GetInt(this.cf.DateAdd(str + "0601", "M", this._기준월).Substring(0, 6));
                    년월 = num2 < 년월T ? num2 : 년월T;
                }
                else if (num1 >= 7 + this._기준월 && num1 <= 9 + this._기준월)
                {
                    년월캡션 = str + "년 " + "3분기";
                    int num2 = D.GetInt(this.cf.DateAdd(str + "0901", "M", this._기준월).Substring(0, 6));
                    년월 = num2 < 년월T ? num2 : 년월T;
                }
                else
                {
                    년월캡션 = str + "년 " + "4분기";
                    int num2 = D.GetInt(this.cf.DateAdd(str + "1201", "M", this._기준월).Substring(0, 6));
                    년월 = num2 < 년월T ? num2 : 년월T;
                }
                next년월 = D.GetInt(this.cf.DateAdd(년월.ToString() + "01", "M", 1).Substring(0, 6));
            }
            else if (this._E기간구분 == 공통.기간구분.반기)
            {
                string str = D.GetString(년월F).Substring(0, 4);
                int num1 = D.GetInt(D.GetString(년월F).Substring(4, 2));
                
                if (num1 >= 1 + this._기준월 && num1 <= 6 + this._기준월)
                {
                    년월캡션 = pageBase.DD("상") + pageBase.DD("반기");
                    int num2 = D.GetInt(this.cf.DateAdd(str + "0601", "M", this._기준월).Substring(0, 6));
                    년월 = num2 < 년월T ? num2 : 년월T;
                }
                else
                {
                    년월캡션 = pageBase.DD("하") + pageBase.DD("반기");
                    int num2 = D.GetInt(this.cf.DateAdd(str + "1201", "M", this._기준월).Substring(0, 6));
                    년월 = num2 < 년월T ? num2 : 년월T;
                }
                next년월 = D.GetInt(this.cf.DateAdd(년월.ToString() + "01", "M", 1).Substring(0, 6));
            }
            else
            {
                년월캡션 = pageBase.DD("에러");
                년월 = next년월 = 999999;
            }
        }
    }
}
