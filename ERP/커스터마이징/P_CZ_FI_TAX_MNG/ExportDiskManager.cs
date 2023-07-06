using Duzon.BizOn.Tax.Common.Account;
using System;
using System.Collections.Generic;
using System.Data;
using Duzon.BizOn.Tax;
using System.IO;
using System.Windows.Forms;
using System.Text;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public class ExportDiskManager : DisketUtil
    {
        public List<string> m_ErrorMsg = new List<string>();
        private Decimal 총외화건수 = 0;
        private Decimal 총외화금액 = 0;
        private Decimal 총원화금액 = 0;
        private Decimal 헤더외화금액 = 0;
        private Decimal 헤더원화금액 = 0;
        private Decimal 바디외화금액합 = 0;
        private Decimal 바디원화금액합 = 0;
        private Decimal 재화외화건수합 = 0;
        private Decimal 재화외화금액합 = 0;
        private Decimal 재화원화금액합 = 0;
        private Decimal 기타외화건수합 = 0;
        private Decimal 기타외화금액합 = 0;
        private Decimal 기타원화금액합 = 0;

        public void SetHeader(string arg_FrDt, string arg_ToDt, string arg_RptTm, DataRow arg_Row)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            this.SetHeaderData("A", 1);
            this.SetHeaderData(arg_ToDt, 6);
            int num = (int.Parse(arg_ToDt.Substring(0, 4)) - int.Parse(arg_FrDt.Substring(0, 4))) * 12 - int.Parse(arg_FrDt.Substring(4, 2)) + int.Parse(arg_ToDt.Substring(4, 2)) + 1;
            this.SetHeaderData(num <= 3 ? num.ToString() : "4", 1);
            this.SetHeaderData(arg_Row["REG_NB"].ToString(), 10);
            this.SetHeaderData(arg_Row["DIV_NM"].ToString(), 30);
            this.SetHeaderData(arg_Row["CEO_NM"].ToString(), 15);
            this.SetHeaderData(arg_Row["ADDR"].ToString(), 45);
            this.SetHeaderData(arg_Row["BUSINESS"].ToString(), 17);
            this.SetHeaderData(arg_Row["JONGMOK"].ToString(), 25);
            this.SetHeaderData(arg_FrDt + arg_ToDt, 16);
            this.SetHeaderData(arg_ToDt, 8);
            this.SetHeaderData("", 6);
            this.ValidationHeader(arg_Row);
        }

        public void SetBody(string arg_FrDt, string arg_ToDt, string arg_RptTm, string arg_regNb, string arg_divCd, string arg_divNM, DataRow arg_Row, int arg_Seq)
        {
            DisketData arg_Data = new DisketData();
            string str1 = string.Empty;
            string str2 = string.Empty;
            arg_Data.SetData("C", 1);
            arg_Data.SetData(arg_ToDt, 6);
            int num = (int.Parse(arg_ToDt.Substring(0, 4)) - int.Parse(arg_FrDt.Substring(0, 4))) * 12 - int.Parse(arg_FrDt.Substring(4, 2)) + int.Parse(arg_ToDt.Substring(4, 2)) + 1;
            string arg_Str = num <= 3 ? num.ToString() : "4";
            arg_Data.SetData(arg_Str, 1);
            arg_Data.SetData(arg_regNb.Replace("-", ""), 10);
            arg_Data.SetData(arg_Seq.ToString(), 7, true);
            arg_Data.SetData(arg_Row["NO_TO"].ToString(), 15);
            arg_Data.SetData(arg_Row["DT_SHIPPING"].ToString(), 8);
            arg_Data.SetData(arg_Row["NM_EXCH"].ToString(), 3);
            arg_Data.SetData(string.Format("{0:f4}", arg_Row["RT_DELIVERY"]).Replace(".", ""), 9, true, true);
            arg_Data.SetData(string.Format("{0:f2}", arg_Row["AM_EX"]).Replace(".", ""), 15, true, true);
            arg_Data.SetData(string.Format("{0:f0}", arg_Row["AM"]).Replace(".", ""), 15, true, true);
            arg_Data.SetData("", 90);

            this.총외화건수++;
            this.총외화금액 += D.GetDecimal(arg_Row["AM_EX"]);
            this.총원화금액 += D.GetDecimal(arg_Row["AM"]);

            if (D.GetString(arg_Row["TP_EXPORT"]) == "001")
            {
                this.재화외화건수합++;
                this.재화외화금액합 += D.GetDecimal(arg_Row["AM_EX"]);
                this.재화원화금액합 += D.GetDecimal(arg_Row["AM"]);
            }
            else
            {
                this.기타외화건수합++;
                this.기타외화금액합 += D.GetDecimal(arg_Row["AM_EX"]);
                this.기타원화금액합 += D.GetDecimal(arg_Row["AM"]);
            }
            
            this.SetMultiBodyData(arg_Data);
            this.ValidationBody(arg_Row, arg_divCd, arg_divNM);
        }

        public void SetTail(string arg_FrDt, string arg_ToDt, string arg_RegNb)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            this.SetTailData("B", 1);
            this.SetTailData(arg_ToDt, 6);
            int num = (int.Parse(arg_ToDt.Substring(0, 4)) - int.Parse(arg_FrDt.Substring(0, 4))) * 12 - int.Parse(arg_FrDt.Substring(4, 2)) + int.Parse(arg_ToDt.Substring(4, 2)) + 1;
            this.SetTailData(num <= 3 ? num.ToString() : "4", 1);
            this.SetTailData(arg_RegNb.Replace("-", ""), 10);
            this.SetTailData(this.총외화건수.ToString(), 7, true);
            this.SetTailData(string.Format("{0:f2}", this.총외화금액).Replace(".", ""), 15, true, true);
            this.SetTailData(string.Format("{0:f0}", this.총원화금액).Replace(".", ""), 15, true, true);
            this.SetTailData(this.재화외화건수합.ToString(), 7, true);
            this.SetTailData(string.Format("{0:f2}", this.재화외화금액합).Replace(".", ""), 15, true, true);
            this.SetTailData(string.Format("{0:f0}", this.재화원화금액합).Replace(".", ""), 15, true, true);
            this.SetTailData(this.기타외화건수합.ToString(), 7, true);
            this.SetTailData(string.Format("{0:f2}", this.기타외화금액합).Replace(".", ""), 15, true, true);
            this.SetTailData(string.Format("{0:f0}", this.기타원화금액합).Replace(".", ""), 15, true, true);

            this.SetTailData("", 51);
        }

        public DataTable GetReadHeaderData()
        {
            return this.GetReadData(new List<FileItem>()
            {
              new FileItem("", 1),
              new FileItem("", 6),
              new FileItem("", 1),
              new FileItem("", 10),
              new FileItem("", 30),
              new FileItem("", 15),
              new FileItem("", 45),
              new FileItem("", 17),
              new FileItem("", 25),
              new FileItem("", 16),
              new FileItem("", 8),
              new FileItem("", 6)
            }, 0, 1, "A");
        }

        public DataTable GetReadBodyData()
        {
            return this.GetReadData(new List<FileItem>()
            {
              new FileItem("", 1),
              new FileItem("", 6),
              new FileItem("", 1),
              new FileItem("", 10),
              new FileItem("", 7),
              new FileItem("", 15),
              new FileItem("", 8),
              new FileItem("", 3),
              new FileItem("", 9),
              new FileItem("", 15),
              new FileItem("", 15),
              new FileItem("", 90)
            }, 0, 1, "C");
        }

        public DataTable GetReadTailData()
        {
            return this.GetReadData(new List<FileItem>()
            {
              new FileItem("", 1),
              new FileItem("", 6),
              new FileItem("", 1),
              new FileItem("", 10),
              new FileItem("", 7),
              new FileItem("", 15),
              new FileItem("", 15),
              new FileItem("", 7),
              new FileItem("", 15),
              new FileItem("", 15),
              new FileItem("", 7),
              new FileItem("", 15),
              new FileItem("", 15),
              new FileItem("", 51)
            }, 0, 1, "B");
        }

        public DataTable GetAllData()
        {
            DataTable readHeaderData = this.GetReadHeaderData();
            DataTable readBodyData = this.GetReadBodyData();
            DataTable readTailData = this.GetReadTailData();
            DataTable dataTable = readHeaderData.Columns.Count <= readBodyData.Columns.Count ? (readBodyData.Columns.Count <= readTailData.Columns.Count ? readTailData.Clone() : readBodyData.Clone()) : readHeaderData.Clone();
            for (int index = 0; index < readHeaderData.Rows.Count; ++index)
                dataTable.Rows.Add(readHeaderData.Rows[index].ItemArray);
            for (int index = 0; index < readBodyData.Rows.Count; ++index)
                dataTable.Rows.Add(readBodyData.Rows[index].ItemArray);
            for (int index = 0; index < readTailData.Rows.Count; ++index)
                dataTable.Rows.Add(readTailData.Rows[index].ItemArray);
            return dataTable;
        }

        public DataTable GetAllDataOrderHeaderTailBody()
        {
            DataTable readHeaderData = this.GetReadHeaderData();
            DataTable readBodyData = this.GetReadBodyData();
            DataTable readTailData = this.GetReadTailData();
            DataTable dataTable = readHeaderData.Columns.Count <= readBodyData.Columns.Count ? (readBodyData.Columns.Count <= readTailData.Columns.Count ? readTailData.Clone() : readBodyData.Clone()) : readHeaderData.Clone();
            for (int index = 0; index < readHeaderData.Rows.Count; ++index)
                dataTable.Rows.Add(readHeaderData.Rows[index].ItemArray);
            for (int index = 0; index < readTailData.Rows.Count; ++index)
                dataTable.Rows.Add(readTailData.Rows[index].ItemArray);
            for (int index = 0; index < readBodyData.Rows.Count; ++index)
                dataTable.Rows.Add(readBodyData.Rows[index].ItemArray);
            return dataTable;
        }

        public bool SetEnd()
        {
            this.ValidationTailer();
            return this.m_ErrorMsg.Count == 0;
        }

        private void ValidationHeader(DataRow arg_Row)
        {
            List<string> list = new List<string>();
            if (arg_Row["REG_NB"].ToString().Equals(string.Empty) || arg_Row["REG_NB"].ToString().Replace("-", "").Length != 10)
                this.m_ErrorMsg.Add("-사업자등록번호 오류. 회사등록에서 확인하십시요.");
            if (arg_Row["DIV_NM"].ToString().Equals(string.Empty))
                list.Add(string.Format("[{0}]{2}", arg_Row["DIV_CD"] == null ? "" : arg_Row["DIV_CD"].ToString(), "-상호가 없습니다. 회사등록에서 입력하세요."));
            if (arg_Row["CEO_NM"].ToString().Equals(string.Empty))
                list.Add("-성명이 없습니다. 회사등록에서 입력하세요.");
            if (arg_Row["ADDR"].ToString().Equals(string.Empty))
                list.Add(string.Format("[{0}]{1}{2}", arg_Row["DIV_CD"] == null ? "" : arg_Row["DIV_CD"].ToString(), arg_Row["DIV_NM"].ToString(), "-사업장 소재지가 없습니다. 회사등록에서 입력하세요."));
            if (arg_Row["BUSINESS"].ToString().Equals(string.Empty))
                list.Add(string.Format("[{0}]{1}{2}", arg_Row["DIV_CD"] == null ? "" : arg_Row["DIV_CD"].ToString(), arg_Row["DIV_NM"].ToString(), "-업태가 없습니다. 회사등록에서 입력하세요."));
            if (!arg_Row["JONGMOK"].ToString().Equals(string.Empty))
                return;
            list.Add(string.Format("[{0}]{1}{2}", arg_Row["DIV_CD"] == null ? "" : arg_Row["DIV_CD"].ToString(), arg_Row["DIV_NM"].ToString(), "-종목이 없습니다. 회사등록에서 입력하세요."));
        }

        private void ValidationBody(DataRow arg_Row, string arg_divCd, string arg_divNm)
        {
            if (!this.ValidationDate(arg_Row["DT_SHIPPING"].ToString()))
                this.m_ErrorMsg.Add(string.Format("[{0}]{1}{2}", arg_divCd == null ? "" : arg_divCd, arg_divNm, "-수출한 재화 선적일자 오류"));
            Decimal num1 = Decimal.Parse(arg_Row["RT_DELIVERY"].ToString().Equals(string.Empty) ? "0" : arg_Row["RT_DELIVERY"].ToString());
            this.헤더외화금액 = Decimal.Parse(arg_Row["AM_EX"].ToString().Equals(string.Empty) ? "0" : arg_Row["AM_EX"].ToString());
            this.바디외화금액합 += this.헤더외화금액;
            this.헤더원화금액 = Decimal.Parse(arg_Row["AM"].ToString().Equals(string.Empty) ? "0" : arg_Row["AM"].ToString());
            this.바디원화금액합 += this.헤더원화금액;
            Decimal num2 = num1 * this.헤더외화금액;
            if (this.헤더원화금액 + new Decimal(10) > num2 && this.헤더원화금액 - new Decimal(10) < num2)
                return;
            this.m_ErrorMsg.Add(string.Format("[{0}]{1}{2}", arg_divCd == null ? "" : arg_divCd, arg_divNm, "-수출한 재화 원화금액 불일치. (원화금액 = 환율 X 외화금액)"));
        }

        private void ValidationTailer()
        {
            if (this.총원화금액 != this.기타원화금액합 + this.바디원화금액합 || this.총외화금액 != this.기타외화금액합 + this.바디외화금액합 || this.총외화건수 != this.기타외화건수합 + this.재화외화건수합)
                this.m_ErrorMsg.Add("-전체 합계 불일치.");
            if (!(this.재화원화금액합 != this.바디원화금액합) && !(this.재화외화건수합 != (Decimal)this.m_Body.Count))
                return;
            this.m_ErrorMsg.Add("-수출한 재화 합계 불일치.");
        }

        private bool ValidationDate(string arg_Date)
        {
            try
            {
                if (string.IsNullOrEmpty(arg_Date))
                    return false;

                DateTime dateTime = new DateTime(int.Parse(arg_Date.Substring(0, 4)), int.Parse(arg_Date.Substring(4, 2)), int.Parse(arg_Date.Substring(6, 2)));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CreateFileOrderHeaderTailBody2(string arg_Path)
        {
            if (File.Exists(Path.Combine(arg_Path, this.FileName)))
                File.Delete(Path.Combine(arg_Path, this.FileName));

            DirectoryInfo directoryInfo = new DirectoryInfo(arg_Path);

            if (!directoryInfo.Exists)
                directoryInfo.Create();

            StreamWriter streamWriter = new StreamWriter(Path.Combine(arg_Path, this.FileName), true, Encoding.GetEncoding("ks_c_5601-1987"));
            streamWriter.WriteLine(this.m_Header.GetData());

            if (!this.m_Tail.GetData().Equals(string.Empty))
                streamWriter.WriteLine(this.m_Tail.GetData());

            foreach (DisketData disketData in this.m_Body)
                streamWriter.WriteLine(disketData.GetData());

            streamWriter.Close();

            return true;
        }
    }
}