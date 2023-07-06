using Dintec;
using Dintec.Parser;
using Duzon.Common.Forms;
using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Parsing
{
    public class InquiryBase
    {
        protected string _partner;
        protected string _vessel;
        protected string _imoNumber;
        protected string _reference;
        protected DataTable _dtItem;

        private string _fileName;
        protected UnitConverter _uc;


        #region ==================================================================================================== Property
        public string Partner
        {
            get
            {
                return this._partner;
            }
        }

        public string Vessel
        {
            get
            {
                return this._vessel;
            }
        }

        public string IMONumber
        {
            get
            {
                return this._imoNumber;
            }
        }

        public string Reference
        {
            get
            {
                return this._reference;
            }
        }

        public DataTable Item
        {
            get
            {
                return this._dtItem;
            }
        }

        #endregion ==================================================================================================== Constructor

        public InquiryBase(string fileName)
        {
            this._partner = string.Empty; // 거래처
            this._vessel = string.Empty; // 호선
            this._imoNumber = string.Empty; // IMO 번호
            this._reference = string.Empty; // 문의번호

            this._dtItem = new DataTable();
            this._dtItem.Columns.Add("NO"); // 순번
            this._dtItem.Columns.Add("SUBJ"); // 주제
            this._dtItem.Columns.Add("ITEM"); // 품목코드
            this._dtItem.Columns.Add("DESC"); // 품목명
            this._dtItem.Columns.Add("UNIT"); // 단위
            this._dtItem.Columns.Add("QT"); // 수량
            this._dtItem.Columns.Add("UNIQ"); // 선사코드

            this._fileName = fileName;
            this._uc = new UnitConverter();
        }

        public void Parse()
        {
            DataSet ds = this.ReadPDF();

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.ParseItem(dt, i);
                }
            }
        }

        public void ParseXml()
		{
            string xml = string.Empty;
            xml = PdfReader.ToXml(this._fileName);

            this.ParseItemXml(xml);
        }

        public virtual void ParseItem(DataTable dt, int rowIndex)
        {

        }

        public virtual void ParseItemXml(string xml)
        {

        }

        public string RemoveHtmlTag(string html)
		{
            string tmpHtml = Regex.Replace(html, @"<(.|\n)*?>", string.Empty).Trim();
            
            return tmpHtml;
		}

        public DataSet ReadPDF()
        {
            try
            {
                // Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
                // 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
                string xmlTemp = PdfReader.ToXml(this._fileName);

                // 2. 도면을 제외한 Page 카운트 가져오기
                int pageCount = xmlTemp.Count("<page>");

                // 3. 앞서 나온 Page를 근거로 파싱 시작			
                string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
                xml = PdfReader.GetXml(_fileName);
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

                return ds;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return null;
        }
    }
}
