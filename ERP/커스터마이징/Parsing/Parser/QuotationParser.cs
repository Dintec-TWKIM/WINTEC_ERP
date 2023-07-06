using Aspose.Email.Outlook;

using Dintec;

using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Parsing
{
    public class QuotationParser
    {
        string fileName = string.Empty;

        // 첨부파일 갯수
        int fileCount = 0;
        int _fileCount = 0;

        DataTable dtIteml;

        string lt;
        string reference;
        string rmk;
        string currency;


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
        public string Lt
        {
            get
            {
                return lt;
            }
        }
        public string Rmk
        {
            get
            {
                return rmk;
            }
        }

        public string Currency
        {
            get
            {
                return currency;
            }
        }



        public QuotationParser(string fileName)
        {
            dtIteml = new DataTable();

            rmk = string.Empty;
            reference = string.Empty;
            lt = string.Empty;
            currency = string.Empty;

            // 첨부파일이름 (경로, 확장자 포함)
            this.fileName = fileName;
        }
        
        
        public bool Parse(bool isReal)
        {
            string extension = Path.GetExtension(fileName);

            fileCount = 0;
            _fileCount = 1;
            string[] fileNameValue;
            string[] _filename = new string[] { };

            fileNameValue = new string[] { fileName };

            extension = Path.GetExtension(fileName);

            if (extension.ToUpper().ToString().Equals(".MSG"))
            {
                MapiMessage msg = MapiMessage.FromFile(fileName);

                string msgText = msg.ConversationTopic.ToString();

                if (msg.Body.Contains("동화뉴텍"))
                {
                    if (!isReal) return true;

                    Donghwa_mail p = new Donghwa_mail(fileName);
                    p.Parse();

                    dtIteml = p.ItemL;

                    if (dtIteml.Rows.Count <= 0)
                        fileName = string.Empty;
                    
                }
                else if (msg.Body.Contains("하이텍오션") || msg.Body.Contains("hitechocean"))
                {
                    if (!isReal) return true;

                    HitechOcean p = new HitechOcean(fileName);
                    p.Parse();

                    dtIteml = p.ItemL;

                    if (dtIteml.Rows.Count <= 0)
                        fileName = string.Empty;
                }
                else if (msg.Body.Contains("Alfa Laval Korea") || msg.Body.Contains("alfalaval.com"))
                {
                    if (!isReal) return true;

                    AlfaLaval p = new AlfaLaval(fileName);
                    p.Parse();

                    dtIteml = p.ItemL;

                    if (dtIteml.Rows.Count <= 0)
                        fileName = string.Empty;
                }
                else if (msg.Body.Contains("hymax@chol.com"))
                {
                    if (!isReal) return true;


                }
                //else if (msg.Body.Contains("wooyang.moatt"))
                //{
                //    // 임시 우양선기 테스트
                //    Wooyang_html p = new Wooyang_html(fileName);
                //    p.Parse();

                //    dtIteml = p.ItemL;

                //    if (dtIteml.Rows.Count <= 0)
                //        fileName = string.Empty;
                //}
                else
                {
                    _filename = msgCheck(fileNameValue);
                }
            }

            for (int c = 0; c < _fileCount; c++)
            {
                if (_filename.Length > 0)
                {
                    if (_filename[c] == null)
                        break;

                    if (extension.ToUpper().Equals(".MSG") || _filename.Length >= 1 && _filename[c] != null)
                    {
                        extension = Path.GetExtension(_filename[c]);
                        fileName = _filename[c].ToString();
                    }
                }

                if (extension.ToUpper() == ".PDF")
                {
                    string text = PdfReader.ToText(fileName);
					//string test = PdfReader.GetText(fileName);


					if (text == null)
                    {
                        break;
                    }
                    // 하이에어코리아
                    if (text.Contains("hiairkorea"))
                    {
                        if (!isReal) return true;

                        HiAirKorea p = new HiAirKorea(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // 삼건엠에스
                    else if (text.Contains("samkunok.com") || text.Contains("413 Miyul-ro") || text.Contains("405 Miyul-ro"))
                    {
                        if (!isReal) return true;

                        Samkun p = new Samkun(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // 경성에스알엠
                    else if (text.Contains("경 성 에 스 알 엠"))
                    {
                        if (!isReal) return true;

                        KyungsungSRM p = new KyungsungSRM(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // 에스엔제이마린
                    else if (text.Contains("SNJ MARINE"))
                    {
                        if (!isReal) return true;

                        SNJ p = new SNJ(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // AC FLYNN REFRIGERATION
                    else if (text.Contains("Flynn Refrigeration"))
                    {
                        if (!isReal) return true;

                        Flynn p = new Flynn(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;

                        dtIteml = p.ItemL;
                    }
                    // 우양선기
                    else if ((text.Contains("Woo Yang") || text.Contains("\r\n\r\n\r\n\r\n\r\n\r\nQUOTATION\r\n\r\n\r\nMESSRS") || text.Contains("QUOTATION\r\n\r\n\r\nMESSRS")) && !text.Contains("A TOTAL SERVICE CO., LTD."))
                    {
                        if (!isReal) return true;

                        Wooyang p = new Wooyang(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 경남드라이어
                    else if (text.Contains("kndryer.co.kr"))
                    {
                        if (!isReal) return true;

                        KND p = new KND(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 위너스 마린
                    else if (text.Contains("Winners Marine"))
                    {
                        if (!isReal) return true;

                        Winners p = new Winners(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 신성밸브
                    else if (text.Contains("신") && text.Contains("성") && text.Contains("밸") && text.Contains("브"))
                    {
                        if (!isReal) return true;

                        Sinseong p = new Sinseong(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 극동일렉콤
                    else if (text.Contains("극동일렉콤"))
                    {
                        if (!isReal) return true;

                        Kukdong p = new Kukdong(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("케스콤"))
                    {
                        if (!isReal) return true;

                        Kukdong p = new Kukdong(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 신우이앤티
                    else if (text.Contains("SHINWOO E&T CO.,LTD") || text.Contains("@swnkcf.com"))
                    {
                        if (!isReal) return true;

                        Shinwoo p = new Shinwoo(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 컨실리움 마린 코리아
                    else if (text.Contains("Consilium Marine"))
                    {
                        if (!isReal) return true;

                        Consillium p = new Consillium(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 더센텀무역
                    else if (text.Contains("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nQuotation of Spare parts\r\n\r\n") || text.Contains("\r\n\r\n\r\n\r\n\r\n\r\n\r\nQuotation of Spare parts")
                        || text.Contains("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nQuotation of Spare Valve"))
                    {
                        if (!isReal) return true;

                        Thecentum p = new Thecentum(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 두산엔진
                    else if (text.Contains("Doosan Eng") || text.Contains("HSD Eng ine Co., Ltd .") || text.Contains("H SD Eng") || text.Contains("HSD Engine"))
                    {
                        if (!isReal) return true;
                        
                        DoosanEng p = new DoosanEng(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 삼주이엔지
                    else if (text.Contains("삼주이엔지"))
                    {
                        if (!isReal) return true;

                        Samju p = new Samju(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 동화뉴텍
                    else if (text.Contains("(주)동화뉴텍"))
                    {
                        if (!isReal) return true;

                        donghwa p = new donghwa(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 양산마린
                    else if (text.Contains("Yangshan Marine") || text.Contains("@yangshan.com.cn"))
                    {
                        if (!isReal) return true;

                        YangshanMarine p = new YangshanMarine(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 나미테크
                    else if (text.Contains("NAMI TECH"))
                    {
                        if (!isReal) return true;

                        Nami p = new Nami(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 테크플라워
                    else if (text.Contains("TECH FLOWER CO., LTD"))
                    {
                        if (!isReal) return true;

                        TechFlower p = new TechFlower(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 디이엑스
                    else if (text.Contains("㈜디이엑스"))
                    {
                        if (!isReal) return true;

                        DEX p = new DEX(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 세홍 부산지사
                    else if (text.Contains("SEHONG ERP SYSTEM"))
                    {
                        if (!isReal) return true;

                        Sehong p = new Sehong(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 알파라발 pdf
                    else if (text.Contains("LAVAL"))
                    {
                        if (!isReal) return true;

                        AlfaLavalPdf p = new AlfaLavalPdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("선진종합"))
                    {
                        if (!isReal) return true;

                        Sunjin p = new Sunjin(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("agmsystem"))
                    {
                        if (!isReal) return true;

                        AGM p = new AGM(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("DACOS"))
                    {
                        if (!isReal) return true;

                        Dacos p = new Dacos(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("위더스월드"))
                    {
                        if (!isReal) return true;

                        Withus p = new Withus(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 하나테크
                    else if (text.Contains("HANA TECH"))
                    {
                        if (!isReal) return true;

                        HANA p = new HANA(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                        // 서원텍
                    else if (text.Contains("서원텍"))
                    {
                        if (!isReal) return true;

                        Seowon p = new Seowon(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("한국피에치이"))
                    {
                        if (!isReal) return true;

                        KPHE p = new KPHE(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("MJ Corporation"))
                    {
                        if (!isReal) return true;

                        MJ p = new MJ(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("JETS KOREA LTD."))
                    {
                        if (!isReal) return true;

                        JETS p = new JETS(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // GEA KOREA
                    else if (text.Contains("GEA Korea"))
                    {
                        if (!isReal) return true;

                        GEAKOREA p = new GEAKOREA(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 파나시아
                    else if (text.Contains("파나시아"))
                    {
                        if (!isReal) return true;

                        PANASIA_pdf p = new PANASIA_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("@thesafety.com"))
                    {
                        if (!isReal) return true;

                        thesafety p = new thesafety(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("마린콤"))
                    {
                        if (!isReal) return true;

                        MRC_pdf p = new MRC_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("㈜오엠씨이스트"))
                    {
                        if (!isReal) return true;

                        OMCEast_pdf p = new OMCEast_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("Han Young Engineering"))
                    {
                        if (!isReal) return true;

                        Hanyoung_pdf p = new Hanyoung_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("Emerson Process Management") && !text.Contains("Standard Terms and Conditions of Sale"))
                    {
                        // if (!isReal) return true;
                        // 에머슨 일단 보류


                        Emerson_pdf p = new Emerson_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
					// SMS-SME
					else if (text.Contains("SMS-SME PTE. LTD"))
					{
						if (!isReal) return true;

						SMSSME_pdf p = new SMSSME_pdf(fileName);
						p.Parse();

						rmk = p.Rmk;
						lt = p.Lt;
						reference = p.Reference;
						currency = p.Currency;
						dtIteml = p.ItemL;
					}
					else if (text.Contains("MJ Corporation"))
					{
						if (!isReal) return true;

						MJ_pdf p = new MJ_pdf(fileName);
						p.Parse();

						rmk = p.Rmk;
						lt = p.Lt;
						reference = p.Reference;
						currency = p.Currency;
						dtIteml = p.ItemL;
					}
                    else if (text.Contains("(주)고 려 필 터"))
                    {
                        if (!isReal) return true;

                        GRFT p = new GRFT(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("PNS CO., LTD"))
                    {
                        if (!isReal) return true;

                        PNS_pdf p = new PNS_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 삼공사
                    else if (text.Contains("sam-gong.co.k"))
                    {
                        if (!isReal) return true;

                        samgong_pdf p = new samgong_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 동화엔텍
                    else if (text.Contains("@dh.co.kr"))
                    {
                        if (!isReal) return true;

                        DHET p = new DHET(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 대송유니텍
                    else if (text.Contains("대송유니텍"))
					{
                        if (!isReal) return true;

                        Daesong_pdf p = new Daesong_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    //플루맥스
                    else if (text.Contains("플루맥스") || text.Contains("Fluemax"))
					{
                        if (!isReal) return true;

                        Flumax_pdf p = new Flumax_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("topsafe"))
					{
                        if (!isReal) return true;

                        TopSafe1_pdf p = new TopSafe1_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // A TOTAL SERVICE CO., LTD.
                    else if (text.Contains("A TOTAL SERVICE CO., LTD."))
					{
                        if (!isReal) return true;

                        ATOTAL_pdf p = new ATOTAL_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("해동메탈"))
					{
                        // 개발 중단
                        //if (!isReal) return true;

                        //Parsing.Parser.Quotation.HaedongMetal_pdf p = new Parser.Quotation.HaedongMetal_pdf(fileName);
                        //p.Parse();

                        //rmk = p.Rmk;
                        //lt = p.Lt;
                        //reference = p.Reference;
                        //dtIteml = p.ItemL;
                    }
                    // 백드레인코리아
                    else if (text.Contains("백드레인코리아"))
                    {
                        if (!isReal) return true;

                        백드레인코리아_pdf p = new 백드레인코리아_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // 서주해양
                    else if (text.Contains("서주해양"))
                    {
                        if (!isReal) return true;

                        서주해양_pdf p = new 서주해양_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("desco6900@hanmail.net") && text.Contains("DESCO"))
                    {
                        if (!isReal) return true;

                        DESCO_pdf p = new DESCO_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // 씨월드
                    else if (text.Contains("SEA WORLD CO"))
                    {
                        if (!isReal) return true;

                        Seaworld_pdf p = new Seaworld_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    else if (text.Contains("엠에스엘콤프레서"))
					{
                        if (!isReal) return true;

                        MSLCOM_pdf p = new MSLCOM_pdf(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                }
                else if (extension.ToUpper().Equals(".XLS") || extension.ToUpper().Equals(".XLSX"))
                {
                    DataSet ds = ExcelReader.ToDataSet(fileName);

                    int rowCount = ds.Tables[0].Rows.Count;
                    int colCount = ds.Tables[0].Columns.Count;

                    // 정아마린
                    if (rowCount > 6 && colCount > 1 && ds.Tables[0].Rows[6][0].ToString().Contains("jung-a"))
                    {
                        if (!isReal) return true;

                        JungA p = new JungA(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        dtIteml = p.ItemL;
                    }
                    // 프라임코리아 주식회사
                    else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][2].ToString().StartsWith("견  적  서"))
                    {
                        if (!isReal) return true;

                        Prime p = new Prime(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                    // 신명테크
                    else if (ds.Tables[0].Rows[0][1].ToString().StartsWith("신명테크"))
                    {
                        //if (!isReal) return true;

                        Prime p = new Prime(fileName);
                        p.Parse();

                        rmk = p.Rmk;
                        lt = p.Lt;
                        reference = p.Reference;
                        currency = p.Currency;
                        dtIteml = p.ItemL;
                    }
                }
            }
                return true;
        }

        // 첨부파일 저장 후 파싱 전달
        private string[] msgCheck(string[] fileName)
        {
            FileInfo fileInfo;
            string[] localpath = { };

            string _filename = fileName[0].ToString();

            fileInfo = new FileInfo(_filename);

            if (fileInfo.Extension.ToUpper() == ".MSG")
            {
                MapiMessage msg = MapiMessage.FromFile(_filename);

                _fileCount = msg.Attachments.Count;

                localpath = new string[_fileCount];


                // temp 폴더 없을 시 생성
                string cPath = Path.Combine(Application.StartupPath, "temp");
                DirectoryInfo di = new DirectoryInfo(cPath);
                if (di.Exists == false)
                {
                    di.Create();
                }

                foreach (MapiAttachment item in msg.Attachments)
                {
                    _filename = FileMgr.GetUniqueFileName(Path.Combine(Application.StartupPath, "temp") + "\\" + item.FileName);
                    if (!_filename.StartsWith("image"))
                    {
                        localpath[fileCount] = Path.Combine(Application.StartupPath, "temp") + "\\" + _filename;
                        item.Save(localpath[fileCount]);

                        fileCount++;
                    }
                    //break;
                }

                return localpath;
            }
            else
            {
                return fileName;
            }
        }

    }
}
