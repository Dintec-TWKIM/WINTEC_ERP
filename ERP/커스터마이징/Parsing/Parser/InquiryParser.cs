using Aspose.Email.Outlook;
using Dintec;
using Parsing.Parser.Inquiry;

using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Parsing.Parser.UNIPASS;

namespace Parsing
{
    public class InquiryParser
    {
        string fileName = "";
        string mailFileName = string.Empty;
        string mailSubject = string.Empty;

        string imoNumber;
        string vessel;
        string reference;
        string contact;
        string partner;
        string remark;
        string tnid;
        string partnercode = string.Empty;
        string inqfilename;
        string buyer;
        string shipservatt;


        // public으로 다 때림.
        public string orderfileno = string.Empty;
        public string ordertotal = string.Empty;
        public string ordercurrency = string.Empty;
        public string orderreference = string.Empty;
        public string orderimo = string.Empty;
        public string ordervessel = string.Empty;


        // 첨부파일 갯수
        int fileCount = 0;
        int _fileCount = 0;

        DataTable dtItem;

        #region ==================================================================================================== Property

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        public string Vessel
        {
            get
            {
                return vessel;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        public string Partner
        {
            get
            {
                return partner;
            }
        }


        public string Tnid
        {
            get
            {
                return tnid;
            }
        }

        public string Buyer
        {
            get
            {
                return buyer;
            }
        }

        public string Remark
        {
            get
            {
                return remark;
            }
        }

        public string PartnerCode
        {
            get
            {
                return partnercode;
            }
        }

        public string InqFileName
        {
            get
            {
                return inqfilename;
            }
        }

        public string ShipServAtt
        {
            get
            {
                return shipservatt;
            }
        }


        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion

        #region ==================================================================================================== Constructor
        public InquiryParser(string fileName)
        {
            imoNumber = string.Empty;
            vessel = string.Empty;
            reference = string.Empty;
            contact = string.Empty;
            partner = string.Empty;
            remark = string.Empty;
            partnercode = string.Empty;
            buyer = string.Empty;
            shipservatt = string.Empty;
            dtItem = new DataTable();

            // 첨부파일이름 (경로, 확장자 포함)
            this.fileName = fileName;
        }
        #endregion


        #region ==================================================================================================== Parsing

        public bool Parse(bool isReal)
        {
            string extension = Path.GetExtension(fileName);

            fileCount = 0;
            _fileCount = 1;
            string[] fileNameValue;   // 보내는 배열
            string[] _filename = new string[] { };  // 받는 배열

            fileNameValue = new string[] { fileName };


            extension = Path.GetExtension(fileName);

            // if 메일 then GO
            if (extension.ToUpper().ToString().Equals(".MSG"))
            {
                mailFileName = fileName;
                MapiMessage msg = MapiMessage.FromFile(fileName);

                mailSubject = msg.Subject.ToString();
                // 메일 제목
                string msgText = msg.ConversationTopic.ToString();
                // 메일 본문
                //                string msgBody = string.Empty;
                string msgBody = msg.BodyHtml;

                // shipserv 첨부파일 확인용
                if (msgBody.Contains("https://trade.shipserv.com/download/"))
                    shipservatt = msgBody;
                else
                    shipservatt = string.Empty;


                _filename = msgCheck(fileNameValue);
                if (!string.IsNullOrEmpty(msg.Body))
                {
                    msgBody = msg.Body;

                    // MIDEAST 예외 처리
                    if (fileNameValue[0].ToString().Contains("FW Request for Quotation  ") || fileNameValue[0].ToString().Contains("Request for Quotation  ") || msgText.Contains("FW Request for Quotation / ") || msgText.Contains("Request for Quotation / "))
                    {
                        if (!fileNameValue[0].ToString().Contains("RPSC"))
                        {
                            if (!isReal) return true;
                        }

                    }
                    // 테크로스 20230419 데이터운용팀 요청
                    else if (msgBody.Contains("* Information of the inquired materials"))
                    {
                        if (!isReal) return true;

                        mailTechcross p = new mailTechcross(fileName);
                        p.Parse();

                        vessel = p.Vessel;
                        reference = p.Reference.ToUpper();
                        dtItem = p.Item;
                    }
                    // 메일 - FLEET MANAGEMENT LIMITED 
                    else if ((msgText.Contains("Fleet Management Limited") || msgText.Contains("Fleet Management Korea") || msgText.Contains("Fleet Ship Management") ||
                        msgText.Contains("Fleet Management India") || msgText.Contains("Naess Ship")) || msgText.Contains("Fleet Management Middle East DMCC") &&
                        !msg.Subject.ToUpper().Contains("ORDER") || msgText.Contains("Celsius Tech Limited")
                        || (msg.Subject.ToUpper().Contains("||") && msg.Subject.ToUpper().Contains("[") && msg.Subject.ToUpper().Contains("]")))
                    {
                        if (!isReal) return true;

                        Fleet p = new Fleet(fileName);
                        p.Parse();

                        vessel = p.Vessel;
                        reference = p.Reference.ToUpper();
                        dtItem = p.Item;
                        contact = p.Contact;
                        imoNumber = p.ImoNumber;
                        if (isReal)
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }
                    }
                    else if (msgText.Contains("FML Ship Management Ltd"))
                    {
                        if (!isReal) return true;

                        FML p = new FML(fileName);
                        p.Parse();

                        vessel = p.Vessel;
                        reference = p.Reference.ToUpper();
                        dtItem = p.Item;
                        contact = p.Contact;
                        if (isReal)
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }

                    }
                    else if (msgBody.Contains("DANAOS SHIPPING CO.,LTD"))
                    {
                        //if (!isReal) return true;

                        DanaosMail p = new DanaosMail(fileName);
                        p.Parse();

                        vessel = p.Vessel;
                        reference = p.Reference.ToUpper();
                        dtItem = p.Item;
                        contact = p.Contact;
                        if (isReal)
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }

                    }
                    else if (msgBody.Contains("Spare Parts Pdf File") && msgBody.Contains("Wisdom Spare Parts"))
                    {
                        if (!isReal) return true;

                        Wisdom p = new Wisdom(fileName);
                        p.Parse();

                        vessel = p.Vessel;
                        reference = p.Reference.ToUpper();
                        dtItem = p.Item;
                        if (isReal)
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }
                    }
                    else if (msg.Body.Contains("shipmanagement5@sealeaders.com"))
                    {
                        //if (!isReal) return true;
                    }
                    // ShipServ 메일 파싱
                    else if (msgBody.Contains("THIS IS AN AUTOMATED E-MAIL ALERT FROM SHIPSERV") || msgBody.Contains("ShipServ © 2012"))
                    {
                        if (!isReal) return true;

                        mailShipServ p = new mailShipServ(fileName);
                        p.Parse();

                        vessel = p.Vessel;
                        reference = p.Reference.ToUpper();
                        imoNumber = p.ImoNumber;
                        partner = p.Partner;
                        tnid = p.Tnid;
                        dtItem = p.Item;
                        contact = p.Contact;
                        buyer = p.Buyer;

                        if (isReal)
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }

                    }
                    
                }
            }


            if (string.IsNullOrEmpty(vessel))
            {
                for (int c = 0; c < _fileCount; c++)
                {
                    if (_filename.Length != 0)
                    {
                        if (_filename[c] != null && _filename.Length > 0 && !_filename[c].ToString().ToUpper().EndsWith(".PNG") && !_filename[c].ToString().ToUpper().EndsWith(".JPG"))
                        {
                            if (_filename[c] == null)
                                break;

                            if (extension.ToUpper().Equals(".MSG") || _filename.Length >= 1 && _filename[c] != null)
                            {
                                extension = Path.GetExtension(_filename[c]);
                                fileName = _filename[c].ToString();
                            }
                        }
                    }


                    if (extension.ToUpper() == ".PDF")
                    {
                        // Pdf 텍스트를 읽어서 어떤 유형인지 판단
                        string text = PdfReader.ToText(fileName);


                        if (text == null)
                        {
                            //break;
                        }
                        // 자이브 파싱
                        else if (fileName.Contains("_JiBe_"))
                        {
                            if (!isReal) return true;

                            JiBe_pdf p = new JiBe_pdf(fileName);
                            p.Parse();

                            imoNumber = p.ImoNumber;
                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            contact = p.Contact;
                            dtItem = p.Item;
                        }
                        else if (text.Replace(" ", "").Contains("수입신고필증"))
                        {
                            if (!isReal) return true;

                            Parsing.Parser.Order.STXOrder_csv2 p = new Parser.Order.STXOrder_csv2(fileName);
                            p.Parse();

                            //UNIPASS_2 p = new UNIPASS_2(fileName);
                            //p.Parse();
                        }
                        else if (text.Replace(" ", "").Contains("수입신고필증"))
                        {


                        }


                        #region 제일메카
                        // 제일 용 (TEST 용, 작업 완료후 OutParsing 으로 옮겨야 함)
                        else if (text.Contains("www.hanil-fuji.com") && text.Contains("@hanilss.com"))
                        {
                            if (!isReal) return true;

                            한일후지코리아_pdf p = new 한일후지코리아_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // 20230323 제일 NEW
                        else if (text.Contains("BOSUNG ENGINEERING CO"))
						{
                            if (!isReal) return true;

                            보성_pdf p = new 보성_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("케이프라인 주식회사"))
                        {
                            if (!isReal) return true;

                            케이프_pdf p = new 케이프_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("Sea-one International"))
                        {
                            if (!isReal) return true;

                            Seaone_pdf p = new Seaone_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("komarine@kmarine.co.kr"))
                        {
                            if (!isReal) return true;

                            코리아마린_pdf p = new 코리아마린_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }

                        else if (text.Contains("LDC-KOREA"))
                        {
                            if (!isReal) return true;

                            ldc_pdf p = new ldc_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("G&P TECH CO."))
                        {
                            if (!isReal) return true;

                            gnp_pdf p = new gnp_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("DH MARINE"))
						{
                            if (!isReal) return true;

                            DHMARINE_pdf p = new DHMARINE_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if(text.Contains("BK OCEAN"))
						{
                            if (!isReal) return true;

                            BKOCEAN_pdf p = new BKOCEAN_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        #endregion 제일메카


                        // 쉽서브 New Order
                        else if (text.Contains("(58055)") && text.Contains("Purchase Order") && text.Contains("TNID") && !text.Contains("Request For Quote"))
                        {
                            Shipserv_order_pdf p = new Shipserv_order_pdf(fileName);
                            p.Parse();

                            ordervessel = p.Vessel;
                            orderreference = p.Reference;
                            orderimo = p.ImoNumber;
                            orderfileno = p.FileNo;
                            ordercurrency = p.Currency;
                            ordertotal = p.Total;
                            
                            //dtItem = p.Item;
                        }


                        // ********** ShipServ
                        else if (text.IndexOf("Generated by ShipServ") > 0)
                        {
                            if (text.Contains("Maersk Line A/S"))
                            {
                                if (!isReal) return true;

                                ShipServMaerskLine p = new ShipServMaerskLine(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            // 2.4 버전 쉽서브 scorpio zenith optimum
                            else if (text.Contains("TradeNet ID: 10553)") || text.Contains("TradeNet ID: 11578") || text.Contains("TradeNet ID: 11390") || text.Contains("TradeNet ID: 11539"))
                            {
                                if (!isReal) return true;

                                //ShipServScorpio p = new ShipServScorpio(fileName);
                                //p.Parse();

                                ShipServScorpioNew p = new ShipServScorpioNew(fileName);
                                p.Parse();


                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.Contains("TradeNet ID: 10585") && text.Contains("TradeNet ID: 10319"))
                            {
                                if (!isReal) return true;

                                ShipServAnglo p = new ShipServAnglo(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            else if (text.Contains("TradeNet ID: 11061"))
                            {
                                if (!isReal) return true;

                                ShipServNorbulk p = new ShipServNorbulk(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            else if (text.Contains("TradeNet ID: 11538"))
                            {
                                if (!isReal) return true;

                                GasLog p = new GasLog(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            else if (text.Contains("TradeNet ID: 11389"))
                            {
                                if (!isReal) return true;

                                UASCs p = new UASCs(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            // Columbia Shipmanagement
                            else if (text.Contains("TradeNet ID: 11934"))
                            {
                                if (!isReal) return true;

                                ColumbiaShip p = new ColumbiaShip(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            else if (text.Contains("TradeNet ID: 11938"))
                            {
                                if (!isReal) return true;

                                ShipServShell p = new ShipServShell(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                            else
                            {
                                if (!isReal) return true;

                                ShipServ p = new ShipServ(fileName);
                                p.Parse();

                                imoNumber = p.ImoNumber;
                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                contact = p.Contact;
                                dtItem = p.Item;
                            }
                        }
                        // 쉽서브 신형 플랫폼 pdf
                        else if (text.Contains("Request For Quo") && text.Contains("ShipServ") && text.Contains("Buyer"))
                        {
                            if (!isReal) return true;

                            ShipServNew_pdf p = new ShipServNew_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.Imonumber;
                        }
                        else if (text.Contains("ROGAMOS NOS ENVIEN OFERTA DE LOS MATERIALES SIGUIENTES"))
                        {
                            if (!isReal) return true;

                            Empresa_pdf p = new Empresa_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        // MARAN TANKERS
                        else if (text.Contains("MARAN TANKERS") || text.Contains("MARAN GAS") || text.Contains("MARAN") || text.Contains("ALMI MARINE MANAGEMENT"))
                        {

                            if (!text.Contains("Zodiac"))
                            {
                                if (!isReal) return true;

                                Maran p = new Maran(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;

                            }
                        }
                        // ********** Kotc
                        else if (text.IndexOf("REQUISITION FORM") == 0)
                        {
                            if (!isReal) return true;

                            if (text.Contains("APPROVED"))
                            {
                                Kotc p = new Kotc(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else
                            {
                                Enesel p = new Enesel(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                        }
                        else if (text.Contains("OCEANGOLD TANKERS INC"))
                        {
                            if (!isReal) return true;

                            AlmiPdf p = new AlmiPdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;

                        }
                        // GOODWOOD SHIP && PG Shipmanagement Pte Ltd
                        else if (text.Contains("Goodwood Ship") || text.Contains("PG Shipmanagement Pte. Ltd"))
                        {
                            if (!isReal) return true;

                            GoodWood p = new GoodWood(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        else if (text.Contains("TB Marine Shipmanagement GmbH & Co. KG"))
                        {
                            if (!isReal) return true;

                            TBmarine_pdf p = new TBmarine_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            imoNumber = p.ImoNumber;
                            dtItem = p.Item;
                        }
                        // ********** MidEast Ship Management
                        else if (text.IndexOf("REQUEST FOR QUOTATION") == 0 || text.Contains("Mideast Ship Management"))
                        {
                            //ALMI TANKERS  .PDF
                            if (text.Contains("Almi Tankers"))
                            {
                                if (!isReal) return true;
                                AlmiPdf p = new AlmiPdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                                imoNumber = p.ImoNumber;
                            }
                            else if (text.Contains("Suntech Ship Management"))
                            {
                                if (!isReal) return true;
                                Suntech_pdf p = new Suntech_pdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            // KYKLADES
                            else if (text.Contains("KYKLADES MARITIME CORPORATION"))
                            {
                                if (!isReal) return true;

                                Kyklades p = new Kyklades(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            // MINERVA MARINE
                            else if (text.Contains("minervamarine.com"))
                            {
                                if (!isReal) return true;

                                Minerva p = new Minerva(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                                imoNumber = p.ImoNumber;
                            }
                            // COSTAMARE SHIPPING COMPANY   진입 두군데
                            else if (text.IndexOf("REQUEST FOR QUOTATION No.:") == 0 && !text.Contains("MINERVA"))
                            {
                                if (!isReal) return true;

                                if (text.Contains("asmhq.com"))
                                {
                                    Patriot p = new Patriot(fileName);
                                    p.Parse();

                                    vessel = p.Vessel;
                                    reference = p.Reference.ToUpper();
                                    dtItem = p.Item;
                                }
                                else
                                {
                                    Costamare p = new Costamare(fileName);
                                    p.Parse();

                                    vessel = p.Vessel;
                                    reference = p.Reference.ToUpper();
                                    dtItem = p.Item;
                                    imoNumber = p.ImoNumber;
                                }
                            }
                            // MIDEAST
                            else if (text.Contains("Mideast"))
                            {
                                //MessageBox.Show("test");

                                if (!isReal) return true;

                                MidEast p = new MidEast(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.ToUpper().Contains("SPS CYPRUS") || text.Contains("Samos Steamship"))
                            {
                                if (!isReal) return true;

                                SPS_pdf p = new SPS_pdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                                imoNumber = p.ImoNumber;
                            }
                            else if (text.Contains("SPS S.A.") || text.Contains("Seanergy Maritime"))
                            {
                                if (!isReal) return true;

                                Procurement p = new Procurement(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                                imoNumber = p.ImoNumber;
                            }
                            // EASTERN MEDITERRANEAN MARITIME
                            else if (text.Contains("EASTERN MEDITERRANEAN MARITIME LIMITED"))
                            {
                                if (!isReal) return true;

                                EasternMediterranean p = new EasternMediterranean(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.Contains("Navig8 Ship Management"))
                            {
                                if (!isReal) return true;

                                Navig8 p = new Navig8(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.Contains("premuda.net"))
                            {
                                if (!isReal) return true;

                                Premuda p = new Premuda(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.Contains("ZEABORN Ship Management"))
                            {
                                if (!isReal) return true;

                                ZEABORN p = new ZEABORN(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.Contains("PHAETHON"))
                            {
                                if (!isReal) return true;

                                PantheonTankers_GS_pdf p = new PantheonTankers_GS_pdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else if (text.Contains("UNISEA SHIPPING LTD."))
                            {
                                if (!isReal) return true;

                                UniseaShipping_pdf p = new UniseaShipping_pdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                                imoNumber = p.Imonumber;
                            }
                            else
                            {
                                if (!isReal) return true;

                                AlmiPdf p = new AlmiPdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                                imoNumber = p.ImoNumber;
                            }
                        }
                        // ********** 에이치라인
                        else if (text.IndexOf("Quotation Detail") == 0)
                        {
                            if (!isReal) return true;

                            // 현대글로비스
                            if (text.Contains("GLOVIS") || text.Contains("글로비스") || text.Contains("Quotation No	50000"))
                            {
                                Glovis p = new Glovis(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else
                            {
                                HLine p = new HLine(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }


                        }

                        // ********** BERNHARD SCHULTE SHIPMANAGEMENT(CYPRUS) CO., LTD
                        else if (text.IndexOf("Enquiry") == 0)
                        {
                            //SHANGHAI HIGHWAY MANAGEMENT
                            if (text.IndexOf("Enquiry Items") == 0)
                            {
                                if (!isReal) return true;

                                ShanghaiHighway p = new ShanghaiHighway(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            // ATHENIAN SEA CARRIERS LTD
                            else if (text.Contains("ATHENIAN SEA"))
                            {
                                if (!isReal) return true;

                                Athenian p = new Athenian(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            // SELANDIA
                            else if (text.StartsWith("Enquiry / Order"))
                            {
                                if (!isReal) return true;


                                Selandia p = new Selandia(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;
                            }
                            else
                            {
                                if (!isReal) return true;

                                Bernhard_UK_pdf p = new Bernhard_UK_pdf(fileName);
                                p.Parse();

                                vessel = p.Vessel;
                                reference = p.Reference.ToUpper();
                                dtItem = p.Item;


                                // BERNHARD SCHULTE SHIPMANAGEMENT(CYPRUS) CO., LTD
                                //if (!isReal) return true;

                                //Bernhard p = new Bernhard(fileName);
                                //p.Parse();

                                //vessel = p.Vessel;
                                //reference = p.Reference.ToUpper();
                                //dtItem = p.Item;
                            }
                        }
                        //CAPITAL SHIP MANAGEMENT
                        else if (text.IndexOf("CAPITAL SHIP") == 0 || text.Contains("CAPITAL-EXECUTIVE SHIPMANAGEMENT") || text.Contains("CAPITAL GAS SHIP"))
                        {
                            if (!isReal) return true;

                            //Util.ShowMessage("작업중입니다.");

                            CapitalShip p = new CapitalShip(fileName);

                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;

                        }
                        else if (text.Contains("Bernhard Schulte Shipmanagement (UK) Limited"))
                        {
                            if (!isReal) return true;

                            Bernhard_UK_pdf p = new Bernhard_UK_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // CAPITAL-EXECUTIVE SHIPMANAGEMENT
                        else if (text.Contains("CAPITAL-EXECUTIVE SHIPMANAGEMENT"))
                        {
                            if (!isReal) return true;

                            CapitalExecutive p = new CapitalExecutive(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        // NAVIOS TANKERS MANAGEMENT INC. ,  NAVIOS SHIP MANAGEMENT
                        else if (text.ToLower().Contains("navios"))
                        {
                            if (!isReal) return true;

                            NaviosTankers p = new NaviosTankers(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 에스케이해운 주식회사  // https://partner.skshipping.com/
                        else if (text.IndexOf("SPARE QUOTATION SHEET") == 0 || text.Contains("구매관리 프로그램 [SMP]"))
                        {
                            if (!isReal) return true;

                            SKShipping p = new SKShipping(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            partner = p.Partner;
                            dtItem = p.Item;
                        }
                        else if (text.Contains("https://partner.skshipping.com/") || text.Trim().StartsWith("SPARE QUOTATION SHEET"))
                        {
                            if (!isReal) return true;

                            SKShipping_NEW p = new SKShipping_NEW(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            partner = p.Partner;
                            dtItem = p.Item;
                        }
                        // KNUTSEN OAS SHIPPING
                        else if (text.Contains("Knutsen OAS Shipping"))
                        {
                            if (!isReal) return true;

                            KnutsenOasShipping p = new KnutsenOasShipping(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                            imoNumber = p.ImoNumber;
                        }
                        // WALLEM SHIP MANAGEMENT
                        else if (text.IndexOf("RFQ Content") == 0)
                        {
                            if (!isReal) return true;

                            Wallem p = new Wallem(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        // DYNACOM TANKERS MANAGEMENT
                        else if (text.IndexOf("Dynacom Tankers Management") == 0)
                        {
                            if (!isReal) return true;

                            Dynacom p = new Dynacom(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DYNACOM TANKERS, 업체명이 다르게 올 경우
                        else if (text.Contains("dynacom"))
                        {
                            if (!isReal) return true;

                            Dynacom p = new Dynacom(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MISUZU MACHINERY RORO
                        else if (text.Contains("MISUZU MACHINERY") && text.Contains("RoRo"))
                        {
                            if (!isReal) return true;

                            MisuzuRoRo p = new MisuzuRoRo(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MISUZU MACHINERY
                        else if (text.Contains("MISUZU MACHINERY"))
                        {
                            //if (!isReal) return true;

                            Misuzu p = new Misuzu(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ALMI NEW
                        else if (text.Contains("ALMI TANKERS") && text.Contains("Req. Code/Date"))
                        {
                            //*** ALMI TANKERS : 진입 3군데, 엑셀 형식 1, PDF형식 2
                            if (!isReal) return true;

                            AlmiNew_pdf p = new AlmiNew_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // ALMI
                        else if (text.Contains("ALMI TANKERS"))
                        {
                            //*** ALMI TANKERS : 진입 3군데, 엑셀 형식 1, PDF형식 2
                            if (!isReal) return true;

                            AlmiPdf p = new AlmiPdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // Transocean
                        else if (text.IndexOf("Event Details") == 0)
                        {
                            if (!isReal) return true;

                            Transocean p = new Transocean(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        // MARMARAS NAVIGATION
                        else if (text.Contains("MARMARAS NAVIGATION LTD"))
                        {
                            if (!isReal) return true;

                            Marmaras p = new Marmaras(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // RICKMERS PDF
                        else if (text.Contains("Rickmers Shipmanagement"))
                        {
                            if (!isReal) return true;

                            Rickmers p = new Rickmers(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //COLUMBUS
                        else if (text.Contains("COLUMBUS"))
                        {
                            if (!isReal) return true;

                            Columbus p = new Columbus(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ELETSON 
                        // EMC GAS
                        else if (text.Contains("ELETSON CORPORATION") || text.Contains("EMC GAS CORPORATION"))
                        {
                            if (!isReal) return true;

                            Eletson p = new Eletson(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // COSTAMARE SHIPPING           진입 두군데
                        else if (text.Contains("COSTAMARE SHIPPING COMPANY"))
                        {
                            if (!isReal) return true;

                            Costamare p = new Costamare(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ADNATCO RFx
                        else if ((text.Contains("ADNATCO-NGSCO") || text.Contains("adnatcongsco.ae") || text.Contains("RFx")) && !text.Contains("Noble Drilling"))
                        {
                            if (!isReal) return true;

                            Adnatco p = new Adnatco(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // NORDDEUTSCHE
                        else if (text.Contains("norddeutsche"))
                        {
                            if (!isReal) return true;

                            Norddeutsche p = new Norddeutsche(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DIAMOND OFFSHORE COMPANY
                        else if (text.ToUpper().Contains("DIAMOND OFFSHORE"))
                        {
                            if (!isReal) return true;

                            Diamond p = new Diamond(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // QATAR
                        else if (text.Contains("QATAR SHIPPING") || text.Contains("QATAR NAVIGATION QPSC"))
                        {
                            if (!isReal) return true;

                            Qatar p = new Qatar(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ACTIVE CENIZCILIK
                        else if (text.Contains("ACTIVE DENIZCILIK"))
                        {
                            if (!isReal) return true;

                            ActiveShipping p = new ActiveShipping(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // YUAN
                        else if (text.Contains("機械部") || text.Contains("xingyuan"))
                        {
                            if (!isReal) return true;

                            Yuan p = new Yuan(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("QATAR NAVIGATION QPSC"))
                        {
                            if (!isReal) return true;

                            QatarNavigation_pdf p = new QatarNavigation_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // seaproc 양식 다미코
                        else if (text.Contains("Amico So"))
                        {
                            if (!isReal) return true;

                            Damico2 p = new Damico2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // DAMICO                   SHIPNET 양식임
                        else if (text.ToUpper().Contains("DAMICOSHIP") || text.ToUpper().Contains("GULF ENERGY MARITIME") || text.Contains("@Milaha.com") || text.Contains("@milaha.com") ||
                            text.Contains("@gemships.com") || text.Contains("@uk.kline.com"))
                        {
                            // 권혜성씨 요청 20190129
                            if (text.ToUpper().Contains("DAMICOSHIP"))
                                if (!isReal) return true;

                            Damico p = new Damico(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // POLARIS
                        else if (text.Contains("POLARIS SHIPPING") || text.Contains("HYUNDAI GLOVIS") || text.Contains("SM LINE") || text.Contains("G-Marine Service") || text.Contains("KOREA SHIPPING CORPORATION")
                    || text.Contains("Spare  Part  Quotation\r\n\r\nQuote No.\r\n\r\nRFQ No.") || text.Contains("KLCSM CO.,LTD."))
                        {
                            if (!isReal) return true;

                            Polaris p = new Polaris(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            partner = p.Partner;
                            dtItem = p.Item;
                        }
                        // INTERORIENT MARINE
                        else if (text.Contains("INTERORIENT"))
                        {
                            if (!isReal) return true;

                            Interorient p = new Interorient(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GOLAR
                        else if (text.Contains("Golar Management"))
                        {
                            if (!isReal) return true;

                            Golar p = new Golar(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DELTA TANKERS
                        else if (text.Contains("DELTA TANKERS"))
                        {
                            if (!isReal) return true;

                            DeltaTankers p = new DeltaTankers(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // HANWA
                        else if (text.Contains("HANWA ENGINEERING"))
                        {
                            if (!isReal) return true;

                            Hanwa p = new Hanwa(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Wilhelmsen Ship Management
                        else if (text.Contains("Wilhelmsen Ship Management"))
                        {
                            if (!isReal) return true;

                            Wilhelmsen p = new Wilhelmsen(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // V.Ship
                        else if (text.Contains("V.Ships LIMASSOL") || text.Contains("V.Ships") || text.Contains("International Tanker Management Limited")
                            || text.Contains("Dania Ship Management") || text.Contains("CSL AUSTRALIA"))
                        {
                            if (!isReal) return true;

                            Vships p = new Vships(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.Imonumber;
                        }
                        // OSG SHIP
                        else if (text.Contains("OSG Ship"))
                        {
                            if (!isReal) return true;

                            OSGShip p = new OSGShip(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        // ITM DUBAI
                        else if (text.Contains("ITM Dubai"))
                        {
                            if (!isReal) return true;

                            ITM p = new ITM(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }

                        // NOBLE DRILLING SERVICES INC
                        else if (text.Contains("Noble Drilling"))
                        {
                            if (!isReal) return true;

                            Noble p = new Noble(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GRIMALDI
                        else if (text.Contains("GRIMALDI") || text.Contains("Grimaldi Group Company"))
                        {
                            if (!isReal) return true;

                            Grimaldi p = new Grimaldi(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }

                        // NORTHERN
                        else if (text.Contains("NORTHERN MARINE MANAGEMENT"))
                        {
                            if (!isReal) return true;

                            Northern p = new Northern(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MOTIA
                        else if (text.Contains("Motia Compagnia"))
                        {
                            if (!isReal) return true;

                            Motia p = new Motia(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // BERG & LARSEN
                        else if (text.Contains("Berg & Larsen"))
                        {
                            if (!isReal) return true;

                            BergLarsen p = new BergLarsen(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // INTERGIS
                        else if (text.Contains("REQUISITION OF SPARE PARTS"))
                        {
                            if (!isReal) return true;

                            Intergis p = new Intergis(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 고려해운
                        else if (text.Contains("고려해운"))
                        {
                            if (!isReal) return true;

                            Korea p = new Korea(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            partner = p.Partner;
                            dtItem = p.Item;
                        }
                        // M.T.M SHIP MANAGEMENT
                        else if (text.Contains("M.T.M SHIP"))
                        {
                            //if (!isReal) return true;

                            MTM p = new MTM(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //DIANA SHIPPING
                        else if (text.Contains("DIANA SHIPPING"))
                        {
                            if (!isReal) return true;

                            Diana p = new Diana(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SHIN CHUNG LIN
                        else if (text.StartsWith("ENQUIRY DATE:"))
                        {
                            if (!isReal) return true;

                            ShinChungLin p = new ShinChungLin(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // SEARIVER
                        else if (text.Contains("SeaRiver"))
                        {
                            if (!isReal) return true;

                            SeaRiver p = new SeaRiver(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GESTIONI
                        else if (text.StartsWith("Number"))
                        {
                            if (!isReal) return true;

                            Gestioni p = new Gestioni(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GESTIONI
                        else if (text.StartsWith("QUOTATION REQUEST") && text.Contains("Req. No."))
                        {
                            if (!isReal) return true;

                            Gestioni2 p = new Gestioni2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SEA WORLD MANAGEMENT
                        else if (text.Contains("SEA WORLD MNGT"))
                        {
                            if (!isReal) return true;

                            SeaWorld p = new SeaWorld(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DYNAGAS
                        else if (text.Contains("Dynagas Ltd."))
                        {
                            if (!isReal) return true;

                            Dynagas p = new Dynagas(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ISS MACHINERY
                        else if (text.Contains("ISS Machinery"))
                        {
                            //if (!isReal) return true;

                            ISS p = new ISS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // CYPRUS, UNIVERSAL, VENDOR 새로형
                        else if (text.Contains("CYPRUS SEALINES") || text.Contains("UNIVERSAL SHIPPINGALLIANCE"))
                        {
                            if (!isReal) return true;

                            CyprusSea p = new CyprusSea(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ZENITH GEMI
                        else if (text.Contains("Zenith Gemi"))
                        {
                            if (!isReal) return true;

                            ZenithGemi p = new ZenithGemi(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Peter Döhle Schiffahrts"))
                        {
                            if (!isReal) return true;

                            PeterDohle_pdf p = new PeterDohle_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        //                  else if (text.Contains(""))
                        //{
                        //                      if (!isReal) return true;

                        //                      Hartmann_pdf p = new Hartmann_pdf(fileName);
                        //                      p.Parse();

                        //                      vessel = p.Vessel;
                        //                      reference = p.Reference.ToUpper();
                        //                      dtItem = p.Item;
                        //                      contact = p.Contact;
                        //                  }
                        // ER SCHIFFAHRT
                        else if ((text.Contains("Schiffahrt") && !text.Contains("HARTMANN GAS CARRIERS")) && (text.Contains("Schiffahrt") && !text.Contains("SLOMAN NEPTUN")) && !text.ToUpper().Contains("OSKAR WEHR")
                            && !text.Contains("HARTMANN DRY CARGO GERMANY GMBH"))
                        {
                            if (!isReal) return true;

                            ER p = new ER(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // SEA SPAN NEW
                        else if (text.StartsWith("Requisition Form") || text.Contains("EMPRESA"))
                        {
                            if (!isReal) return true;

                            SeaspanNew p = new SeaspanNew(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ********* CONSOLIDATED MARINE
                        else if (text.IndexOf("CONSOLIDATED MARINE") == 0)
                        {
                            if (!isReal) return true;

                            ConsolidatedMarine p = new ConsolidatedMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EVERLAST
                        else if (text.Contains("Order Details") && text.Contains("Purchasing Contact") && text.Contains("Vessel Name / IMO No.") && text.Contains("Order Number / Status"))
                        {
                            if (!isReal) return true;

                            Everlast p = new Everlast(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ROWAN
                        else if (text.Contains("ROWAN COMPANIES"))
                        {
                            if (!isReal) return true;

                            Rowan p = new Rowan(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("IONIA MANAGEMENT"))
                        {
                            if (!isReal) return true;

                            IONIAMAN p = new IONIAMAN(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EMARAT
                        else if ((text.Contains("Hull No. :") || text.Contains("Shipyard :") || text.Contains("Req ID")) && !text.Contains("IMC Co., Ltd."))
                        {
                            if (!isReal) return true;

                            Emarat p = new Emarat(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DOLE REEFERSHIP MARINE SERVICES
                        else if (text.Contains("Dole Reefership Marine Services"))
                        {
                            if (!isReal) return true;

                            Dole p = new Dole(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //ASP PROCUREMENT SERVICES
                        else if (text.Contains("Material Requisition FOR INTERNAL USE ONLY"))
                        {
                            if (!isReal) return true;

                            Asp p = new Asp(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("@ocyan-sa.com"))
                        {
                            if (!isReal) return true;

                            OCYAN p = new OCYAN(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ODEBRECHT
                        else if (text.Contains("odebrecht.com") || text.Contains("Solicitação de Cotação Número") || text.Contains("Odebrecht"))
                        {
                            if (!isReal) return true;

                            Odebrecht p = new Odebrecht(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // UNISEA
                        else if (text.Contains("Requisition issued by:"))
                        {
                            if (!isReal) return true;

                            Unisea p = new Unisea(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DANAOS
                        else if (text.Contains("DANAOS SHIPPING"))
                        {
                            if (!isReal) return true;

                            Danaos p = new Danaos(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // LOUIS DREYFUS ARMATEURS S.A.S
                        else if (text.Contains("LOUIS  DREYFUS"))
                        {
                            if (!isReal) return true;

                            Louis p = new Louis(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // PANTHEON TANKERS MANAGEMENT
                        else if (text.StartsWith("SPARES INQUIRY FOR QUOTATION"))
                        {
                            if (!isReal) return true;

                            Pantheon p = new Pantheon(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ENSCO
                        else if (text.ToUpper().Contains("ENSCO"))
                        {
                            if (!isReal) return true;

                            Ensco p = new Ensco(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // FRATELLI D'AMICO ARMATORI
                        else if (text.Contains("FRATELLI d'AMICO ARMATORI"))
                        {
                            if (!isReal) return true;

                            FratelliAmico p = new FratelliAmico(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 케이에스에스해운
                        else if (text.StartsWith("(SMP)") || text.StartsWith("구매관리 프로그램"))
                        {
                            if (!isReal) return true;

                            KSS p = new KSS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // NAKILAT
                        else if (text.Contains("Transmarine (London) Ltd") || text.Contains("Transmarine  (London)") || text.Contains("Your vendor number with "))
                        {
                            // 변수가 너무 많음.
                            if (!isReal) return true;

                            Nakilat p = new Nakilat(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ZODIAC
                        else if (text.Contains("Zodiac"))
                        {
                            if (!isReal) return true;

                            Zodiac p = new Zodiac(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MOL TECHNO-TRADE
                        else if (text.Contains("MOL Techno-Trade"))
                        {
                            //if (!isReal) return true;

                            MolTech p = new MolTech(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SOLVANG
                        else if (text.Contains("solvang") || text.Contains("Solvang") || text.Contains("SOLVANG"))
                        {
                            if (!isReal) return true;

                            Solvang p = new Solvang(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SBM
                        else if (text.Contains("Guará Norte Operações"))
                        {
                            if (!isReal) return true;

                            SBM p = new SBM(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MEARSK DRILLING
                        else if (text.Contains("maerskdrilling"))
                        {
                            if (!isReal) return true;

                            Maerskdrilling p = new Maerskdrilling(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MINERVA MARINE
                        else if (text.Contains("minervamarine.com"))
                        {
                            if (!isReal) return true;
                            Minerva p = new Minerva(fileName);

                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // TSAKOS
                        else if (text.StartsWith("Vessel:"))
                        {
                            //if (!isReal) return true;

                            TSAKOS p = new TSAKOS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // CHANDRIS
                        else if (text.Contains("CHANDRIS") || text.Contains("AMARTHEA"))
                        {
                            if (!isReal) return true;

                            Chandris p = new Chandris(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.Imonumber;
                        }
                        // 동진선사
                        else if (text.Contains("DONGJIN SHIPPING") && !text.Contains("DONGJIN MARINE SERVICE"))
                        {
                            if (!isReal) return true;

                            Dongjin p = new Dongjin(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // NIPPIN DIESEL SERVICE GMBH
                        else if (text.Contains("@nds-marine.com"))
                        {
                            if (!isReal) return true;

                            NDS p = new NDS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SAIPEM
                        else if (text.Contains("Saipem") && !text.Contains("LOW COMPLEXITY") && !text.Contains("LOW CRITICALITY") && !text.Contains("PACKING AND MARKING SPECIFICATION"))
                        {
                            if (!isReal) return true;

                            Saipem p = new Saipem(fileName, mailFileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Rahbaran Omid Darya
                        else if (text.Contains("Rahbaran Omid Darya") || text.Contains("HTS코리아"))
                        {
                            if (!isReal) return true;

                            ROD p = new ROD(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GENERAL MARINE SPARES
                        else if (text.Contains("General Marine Spares"))
                        {
                            if (!isReal) return true;

                            GMS p = new GMS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DOLPHIN DRILLING
                        else if (text.Contains("DOLPHIN DRILLING"))
                        {
                            if (!isReal) return true;

                            Dolphin p = new Dolphin(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Kinnetik"))
                        {
                            if (!isReal) return true;

                            Kinnetik p = new Kinnetik(fileName);
                            p.ParseXml();

                            partner = p.Partner;
                            vessel = p.Vessel;
                            imoNumber = p.IMONumber;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // BW
                        else if (!text.Contains("Kinnetik") && text.Contains("BW Fleet Management"))
                        {
                            //if (!isReal) return true;

                            //Util.ShowMessage("작업중입니다.");

                            BW p = new BW(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 엔피에스 
                        else if (text.Contains("N P S CO., LTD."))
                        {
                            //if (!isReal) return true;

                            NPS p = new NPS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // KLEIMAR N.V
                        else if (text.StartsWith("KLEIMAR N.V"))
                        {
                            if (!isReal) return true;

                            Kleimar p = new Kleimar(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 에스케이해운 다른 버전!
                        else if (text.Contains("STORE QUOTATION SHEET"))
                        {
                            if (!isReal) return true;

                            SK2 p = new SK2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SYNERGY MARITIME PVT LTD
                        else if (text.Contains("SYNERGY GROUP") || text.Contains("Synergy Group"))
                        {
                            if (!isReal) return true;

                            Synergy p = new Synergy(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // PB TANKERS
                        else if (text.Contains("pbtankers.com"))
                        {
                            //if (!isReal) return true;

                            PB p = new PB(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // KLAVENESS
                        else if (text.Contains("Klaveness Ship Management"))
                        {
                            if (!isReal) return true;

                            Klaveness p = new Klaveness(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SBM 2
                        else if (text.Contains("TUPI NORDESTE"))
                        {
                            if (!isReal) return true;

                            SBM2 p = new SBM2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // TORM
                        // Gearbulk Norway AS
                        else if (text.Contains("c/o TORM A/S") || text.Contains("Gearbulk Norway AS") || text.Contains("Torm"))
                        {
                            if (!isReal) return true;

                            Torm p = new Torm(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 한국마에스터
                        else if (text.Contains("\r\n\r\nINQUIRY\r\n\r\n\r\nINQ-NO."))
                        {
                            if (!isReal) return true;

                            Meister p = new Meister(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // WORLD TANKERS
                        else if (text.Contains("World Tankers") || text.Contains("MSI Ship Management") || text.Contains("Anglo-Eastern (Labuan) Limited - As Agent only") || text.Contains("Anglo Ardmore Tanker Management Limited - As Agents Only"))
                        {
                            if (!isReal) return true;

                            WorldTanker p = new WorldTanker(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // TMS BULKERS
                        else if (text.Contains("TMS BULKERS LTD.") || text.Contains("TMS CARDIFF GAS LTD") || text.Contains("TMS DRY LTD"))
                        {
                            if (!isReal) return true;

                            tms p = new tms(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 장금상선
                        else if (text.Contains("Inquiry Sheet"))
                        {
                            if (!isReal) return true;

                            JGSS p = new JGSS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // VENTURI
                        else if (text.Contains("VENTURI FLEET MANAGEMENT"))
                        {
                            if (!isReal) return true;

                            Venturi p = new Venturi(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SEADRILL
                        else if (text.Contains("Seadrill"))
                        {
                            if (!isReal) return true;

                            Seadrill p = new Seadrill(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MARAN GAS
                        else if (text.Contains("Enquiry for Spare Parts") && text.Contains("You are kindly requested to quote availability, best prices and delivery time for the supply of spare parts are listed below."))
                        {
                            if (!isReal) return true;

                            Maran2 p = new Maran2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Kyla
                        else if (text.Contains("Kyla Shipping"))
                        {
                            if (!isReal) return true;

                            Kyla p = new Kyla(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Arcadia
                        else if (text.Contains("Arcadia Shipmanagement Co. LTD"))
                        {
                            if (!isReal) return true;

                            Arcadia_pdf p = new Arcadia_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // UNIVERSAL SHIPPING ALLIANCE LTD.
                        else if (text.Contains("UNIVERSAL SHIPPING ALLIANCE LTD."))
                        {
                            if (!isReal) return true;

                            Universal p = new Universal(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Requisition\r\n\r\nSTORES"))
                        {
                            // 안씀 파싱 하기 난해함

                            //if (!isReal) return true;

                            //Vship2 p = new Vship2(fileName);
                            //p.Parse();

                            //vessel = p.Vessel;
                            //reference = p.Reference.ToUpper();
                            //dtItem = p.Item;
                        }
                        // CJ MARINE
                        else if (text.Contains("CJ MARINE & ENG"))
                        {
                            if (!isReal) return true;

                            CJMarine p = new CJMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DONGJIN MARINE
                        else if (text.Contains("DONGJIN MARINE SERVICE"))
                        {
                            if (!isReal) return true;

                            DongjinMarine p = new DongjinMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ULTRA SHIP
                        else if (text.Contains("UltraShip"))
                        {
                            if (!isReal) return true;

                            Ultraship p = new Ultraship(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // ATHENIAN SEA
                        else if (text.Contains("ATHENIAN SEA"))
                        {
                            if (!isReal) return true;

                            AthenianSea p = new AthenianSea(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // K-MARINE
                        else if (text.Contains("K-MARINE Tech. & Pipe Co.,Ltd"))
                        {
                            if (!isReal) return true;

                            KMarine p = new KMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // KOMASCO 한국선용품협회?
                        else if (text.Contains("KOMASCO"))
                        {
                            if (!isReal) return true;

                            KOMASCO p = new KOMASCO(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("allseas.gr"))
                        {
                            if (!isReal) return true;

                            Allseas p = new Allseas(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Delivery Information") && text.Contains("Quotes Details"))
                        {
                            if (!isReal) return true;

                            UMAR p = new UMAR(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // AWILCO
                        else if (text.Contains("AWILCO TECHNICAL"))
                        {
                            if (!isReal) return true;

                            Awilco p = new Awilco(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("HARTMANN GAS CARRIERS GERMANY GMBH"))
                        {
                            if (!isReal) return true;

                            HartmannGas p = new HartmannGas(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("ORIENT OVERSEAS CONTAINER LINE LTD"))
                        {
                            if (!isReal) return true;

                            OOCL p = new OOCL(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("ENQUIRY\r\nEnq. Date :"))
                        {
                            if (!isReal) return true;

                            NGL p = new NGL(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("MATERIALS E-BIDDING SYSTEM"))
                        {
                            if (!isReal) return true;

                            SSSS p = new SSSS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EASTERN PACIFIC
                        else if (text.Contains("epshipping.com.sg"))
                        {
                            if (!isReal) return true;

                            EsternPacific p = new EsternPacific(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // PAVIMAR
                        else if (text.Contains("\r\n\r\n\r\n\r\n\r\n\r\n\r\nTo\tDINTEC CO LTD"))
                        {
                            if (!isReal) return true;

                            PAVIMAR p = new PAVIMAR(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MAR-DYN
                        else if (text.Contains("mardyn"))
                        {
                            if (!isReal) return true;

                            Mar_dyn p = new Mar_dyn(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 강한해양
                        else if (text.Contains("KANGHAN Marine"))
                        {
                            if (!isReal) return true;

                            Kanghan p = new Kanghan(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 히텍
                        else if (text.Contains("Heatech"))
                        {
                            if (!isReal) return true;

                            HeaTech p = new HeaTech(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GREEN PEARL TRADING
                        else if (text.Contains("GREEN PEARL TRADING"))
                        {
                            if (!isReal) return true;

                            GreenPearl p = new GreenPearl(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("A COMPANHIA DE NAVEGAÇÃO NORSUL"))
                        {
                            if (!isReal) return true;

                            COMPANHIA p = new COMPANHIA(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("General Stores Indent"))
                        {
                            if (!isReal) return true;

                            ShanghaiHighway p = new ShanghaiHighway(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Ahrenkiel Steamship"))
                        {
                            if (!isReal) return true;

                            REEDEREI p = new REEDEREI(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                            imoNumber = p.ImoNumber;
                        }
                        // Northern Marine
                        else if (text.StartsWith("QUOTATION REQUEST"))
                        {
                            if (!isReal) return true;

                            NorthernMarine p = new NorthernMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EURONAV SHIP
                        else if (text.StartsWith("Euronav Ship"))
                        {
                            if (!isReal) return true;

                            EuronavShipPDF p = new EuronavShipPDF(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // AEGEAN
                        else if (text.Contains("purchasing@ampni.com"))
                        {
                            //if (!isReal) return true;

                            AEGEAN p = new AEGEAN(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // UNISEA SHIPPING 
                        else if (text.Contains("UNISEA SHIPPING LTD"))
                        {
                            if (!isReal) return true;

                            Unisea2 p = new Unisea2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // WISDOM MARINE
                        else if (text.Contains("■\r\n\r\n\r\n\r\n"))
                        {
                            if (!isReal) return true;

                            WisdomMarine p = new WisdomMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("B TRADER"))
                        {
                            if (!isReal) return true;

                            BTrader p = new BTrader(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // CRYSTAL POOL SRL
                        else if (text.Contains("crystalpool"))
                        {
                            if (!isReal) return true;

                            CrystalPool p = new CrystalPool(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MILAHA SHIP MANAGEMENT
                        else if (text.StartsWith("MILAHA SHIP MANAGEMENT"))
                        {
                            if (!isReal) return true;

                            MILAHA_pdf p = new MILAHA_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        // NSC 품목명 파싱 어려워서 일단 보류
                        //else if (text.Contains("NSC") || text.Contains("GmbH"))
                        //{
                        //    if (!isReal) return true;
                        //
                        //
                        //    NSC_xml p = new NSC_xml(fileName);
                        //    p.Parse();
                        //
                        //
                        //    //NSC p = new NSC(fileName);
                        //    //p.Parse();
                        //
                        //    //vessel = p.Vessel;
                        //    //reference = p.Reference.ToUpper();
                        //    //dtItem = p.Item;
                        //    //imoNumber = p.ImoNumber;
                        //}
                        // SEA GLOBE
                        else if (text.Contains("SEA GLOBE"))
                        {
                            if (!isReal) return true;

                            SeaGlobe_pdf p = new SeaGlobe_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SOCATRA
                        else if (text.Contains("SOCATRA OUTRE"))
                        {
                            if (!isReal) return true;

                            SOCATRA_pdf p = new SOCATRA_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // HALUL OFFSHORE
                        else if (text.Contains("HALUL OFFSHORE"))
                        {
                            if (!isReal) return true;

                            Halul_pdf p = new Halul_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // NAVIGAZIONE
                        else if (text.Contains("Navigazione Montanari"))
                        {
                            if (!isReal) return true;

                            NAVIGAZIONE_pdf p = new NAVIGAZIONE_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // OSKAR WEHR KG
                        else if (text.Contains("Oskar Wehr KG"))
                        {
                            if (!isReal) return true;

                            Oskar_pdf p = new Oskar_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // SHIPS SURVEYS AND SERVICE S.R.L
                        else if (text.Contains("Ships Surveys and Service"))
                        {
                            if (!isReal) return true;

                            shipsSurveys_pdf p = new shipsSurveys_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("PREMUDA S.P.A"))
                        {
                            //if (!isReal) return true;

                            PREMUDA_pdf p = new PREMUDA_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // OSM SHIP
                        else if (text.Contains("@osm.no") && !text.Contains("Purchase Order"))
                        {
                            if (!isReal) return true;

                            OSMShip p = new OSMShip(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MOTOR DIAGNOZE
                        else if (text.Contains("MOTOR-DIAGNOZE"))
                        {
                            if (!isReal) return true;

                            MotorDiagnoze_pdf p = new MotorDiagnoze_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SEA TEAM
                        else if (text.Contains("@sea-team.com"))
                        {
                            if (!isReal) return true;

                            Seateam_pdf p = new Seateam_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // MARFLET
                        else if (text.Contains("MARFLET"))
                        {
                            if (!isReal) return true;

                            Marflet_pdf p = new Marflet_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // KYODO CORPORATION
                        else if (text.Contains("KYODO CORPORATION"))
                        {
                            if (!isReal) return true;

                            kYODO_pdf p = new kYODO_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // KSS LINE
                        else if (text.Contains("SHIPSERV SCORPIO"))
                        {
                            if (!isReal) return true;

                            ShipServScorpio p = new ShipServScorpio(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        else if (text.Contains("purchase.kssline.com"))
                        {
                            if (!isReal) return true;

                            KSS_pdf p = new KSS_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // EAGLESTAR SHIPPING
                        else if (text.Contains("Eaglestar Ship"))
                        {
                            if (!isReal) return true;

                            Eaglestart_pdf p = new Eaglestart_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        else if (text.Contains("Tamar Ship Management Ltd."))
                        {
                            if (!isReal) return true;

                            Tamar_pdf p = new Tamar_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        else if (text.Contains("GasLog LNG Services Ltd."))
                        {
                            if (!isReal) return true;

                            GasLogLNG_pdf p = new GasLogLNG_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // 수정해야함
                        else if (text.Contains("HARTMANN DRY CARGO GERMANY GMBH"))
                        {
                            if (!isReal) return true;

                            Hartman_pdf p = new Hartman_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        else if (text.Contains("STOLT TANKERS"))
                        {
                            if (!isReal) return true;

                            STOLT_pdf p = new STOLT_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // GENERAL NATIONAL MARITIME TRANSPORT
                        else if (text.Contains("General National Maritime Transport"))
                        {
                            if (!isReal) return true;

                            GeneralNational_pdf p = new GeneralNational_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // EMPRESA NAVIERA ELCANO
                        else if (text.Contains("ROGAMOS NOS ENVIEN OFERTA DE LOS MATERIALES"))
                        {
                            if (!isReal) return true;

                            EmpresaNavieraElcano_pdf p = new EmpresaNavieraElcano_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        else if (text.Contains("Sydney Trader GmbH"))
                        {
                            if (!isReal) return true;

                            Lomar_pdf p = new Lomar_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }

                        // SAFE TECHNICAL SUPPLY
                        else if (text.Contains("Safe Technical Supply"))
                        {
                            if (!isReal) return true;

                            SafeTechnical_pdf p = new SafeTechnical_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Scorpio Commercial Management") || text.Contains("Zenith Gemi Isletmeciligi A.S.") || text.Contains("Optimum Management") || text.Contains("Scorpio Ship Management") ||
                            text.Contains("Campbell Shipping Company") || text.Contains("Garrets International A/S"))
                        {
                            if (!isReal) return true;

                            Scorpio_pdf p = new Scorpio_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GERAT EASTERN SHIPPING COMPANY LIMITED SHIPPING
                        else if (text.Contains("THE GERAT EASTERN SHIPPING COMPANY LIMITED SHIPPING"))
                        {
                            if (!isReal) return true;

                            GeratEastern_pdf p = new GeratEastern_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("RCL Shipmanagement Pte Ltd"))
                        {
                            if (!isReal) return true;

                            RCL_pdf p = new RCL_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Marwave Shipmanagement"))
                        {
                            if (!isReal) return true;

                            Marwave_pdf p = new Marwave_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Reefership Marine"))
                        {
                            if (!isReal) return true;

                            Dolereefership_pdf p = new Dolereefership_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("SAUDI ARABIAN SAIPEM LTD."))
                        {
                            if (!isReal) return true;

                            Dolereefership_pdf p = new Dolereefership_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        else if (text.Contains("Kristian Gerhard Jebsen Skipsrederi A/S"))
                        {
                            if (!isReal) return true;

                            KGJS p = new KGJS(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("THOMAS SCHULTE"))
                        {
                            if (!isReal) return true;

                            THOMASSCHULTE_pdf p = new THOMASSCHULTE_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            imoNumber = p.Imonumber;
                            dtItem = p.Item;

                        }
                        else if (text.Contains("GOE Petrol Sanayi ve Ticaret Limited Sirketi") || text.Contains("Aroona Drilling Ltd") || text.Contains("TURKISH PETROLEUM-OFFSHORE TECHNOLOGY CENTER A.S."))
                        {
                            if (!isReal) return true;

                            GOE p = new GOE(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (text.Contains("Anglo Eastern Ship Management (NL) BV"))
                        {
                            if (!isReal) return true;

                            ANGLO_NL p = new ANGLO_NL(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.Imonumber;
                        }
                        else if (text.Contains("Ahrenkiel Tankers GmbH & Co. KG"))
                        {
                            if (!isReal) return true;

                            Ahrenkiel_pdf p = new Ahrenkiel_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }

                        else if (text.Contains("WINWIN INTEC CO.,") && text.Contains("MAN Energy Solutions") && !text.Contains("order acknowledgement :") &&
                            !text.Contains("Order acknowledgement :"))
                        {
                            if (!isReal) return true;

                            Parser.Order.MANOrder_pdf p = new Parser.Order.MANOrder_pdf(fileName);
                            p.Parse();

                        }
                        else if (text.ToLower().Contains("norden synergy ship management"))
                        {
                            if (!isReal) return true;

                            NordenSynergy_pdf p = new NordenSynergy_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.Imonumber;
                        }
                        // Navigator
                        else if (text.Contains("Navigator"))
                        {
                            if (!isReal) return true;

                            NavigatorPDF p = new NavigatorPDF(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        // SEVEN ISLANDS SHIPPING LTD
                        else if (text.Contains("RFQ #") && text.Contains("IMO #") && text.Contains("HULL #"))
                        {
                            if (!isReal) return true;

                            SevenIslands_pdf p = new SevenIslands_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("PRONAV Ship Management"))
						{
                            if (!isReal) return true;

                            Pronav_pdf p = new Pronav_pdf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference;
                            dtItem = p.Item;
                            imoNumber = p.ImoNumber;
                        }
                        else if (text.Contains("Schenker Korea"))
						{
                            if (!isReal) return true;

                            Schenker_sea p = new Schenker_sea(fileName);
                            p.Parse();

                            dtItem = p.Item;
                        }
                        else if (text.Contains("Shell International Trading"))
						{
                            if (!isReal) return true;

                            Shell_pdf p = new Shell_pdf(fileName);
                            p.Parse();

                            dtItem = p.Item;
                            vessel = p.Vessel;
                            reference = p.Reference;
                        }
                        

                        if (isReal)
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }
                    }
                    else if (extension.ToUpper() == ".XLS" || extension.ToUpper() == ".XLSX")
                    {

                        DataSet ds = ExcelReader.ToDataSet(fileName);
                        DataTable dt = ds.Tables[0];

                        // 첫번째 페이지 행, 열 카운트
                        int rowCount = ds.Tables[0].Rows.Count;
                        int colCount = ds.Tables[0].Columns.Count;


                        // Yang Ming ALL OCEANS TRANSPORTATION
                        if (rowCount > 3 && colCount > 4 && (ds.Tables[0].Rows[2][3].ToString().IndexOf("YANG MING") == 0 || ds.Tables[0].Rows[2][3].ToString().Contains("ALL OCEANS TRANSPORTATION")))
                        {
                            if (!isReal) return true;

                            YangMing p = new YangMing(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }

                        // MOL SHIP
                        else if (rowCount > 3 && colCount > 4 && ds.Tables[0].Rows[0][0].ToString().IndexOf("MOL Ship") == 0)
                        {
                            if (!isReal) return true;

                            MolShip p = new MolShip(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SCF
                        else if (rowCount > 3 && colCount > 4 && ds.Tables[0].Rows[1][4].ToString().Contains("SCF") || ds.Tables[0].Rows[0][0].ToString().StartsWith("ATTENTION!")) 
                        {
                            if (!isReal) return true;

                            SCF p = new SCF(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Garrets International A/S
                        else if (rowCount > 3 && colCount > 4 && ds.Tables[0].Rows[0][0].ToString().Contains("Reply to Request for Quotation"))
                        {
                            if (!isReal) return true;

                            Garrets_excel p = new Garrets_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // POSSM
                        else if (rowCount > 3 && colCount > 4 && ds.Tables[0].Rows[0][0].ToString().Contains("Request for Quotation"))
                        {
                            if (!isReal) return true;

                            POSSM p = new POSSM(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                            partner = p.Partner;
                        }
                        // EURONAV
                        else if (rowCount > 4 && colCount > 4 && ds.Tables[0].Rows[3][0].ToString().Contains("Euronav Ship"))
                        {
                            if (!isReal) return true;

                            Euronav p = new Euronav(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SYNCRO
                        else if (rowCount > 5 && colCount > 1 && ds.Tables[0].Rows[5][0].ToString().Contains("Q U O T A T I O N"))
                        {
                            if (!isReal) return true;

                            Syncro p = new Syncro(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // THE GREAT EASTERN
                        else if (rowCount > 9 && colCount > 3 && ds.Tables[0].Rows[8][2].ToString().Contains("THE GREAT EASTERN"))
                        {
                            if (!isReal) return true;

                            GreatEstern p = new GreatEstern(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //ALMI TANKERS
                        else if (rowCount > 3 && colCount > 13 && ds.Tables[0].Rows[0][12].ToString().Equals("VERSION4"))
                        {
                            if (!isReal) return true;

                            Almi p = new Almi(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // RICKMERS EXCEL
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[1][0].ToString().Contains("Rickmers"))
                        {
                            if (!isReal) return true;

                            RickmersExcel p = new RickmersExcel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EXMAR
                        else if (rowCount > 4 && colCount > 1 && ds.Tables[0].Rows[3][0].ToString().Contains("Exmar Shipmanagement"))
                        {
                            if (!isReal) return true;

                            Exmar p = new Exmar(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // OCEANIC
                        else if (ds.Tables[0].Rows[0][0].ToString().Contains("OCL Oceanic Catering Ltd."))
                        {
                            if (!isReal) return true;

                            Oceanic p = new Oceanic(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // THENAMARIS
                        else if (rowCount > 23 && colCount > 11 && ds.Tables[0].Rows[22][10].ToString().Contains("thenamaris.com"))
                        {
                            if (!isReal) return true;

                            Thenamaris p = new Thenamaris(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SEATEAM
                        else if (rowCount > 1 && colCount > 3 && ds.Tables[0].Rows[0][2].ToString().Contains("SeaTeam"))
                        {
                            if (!isReal) return true;

                            SeaTeam p = new SeaTeam(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DALEX SHIPPING Co. S.A.
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("DALEX SHIPPING Co. S.A."))
                        {
                            if (!isReal) return true;

                            DALEX p = new DALEX(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // HLINE EXCEL
                        else if (rowCount > 3 && colCount > 8 && ((ds.Tables[0].Rows[1][7].ToString().Contains("Quotation Detail") || ds.Tables[0].Rows[1][6].ToString().Contains("Quotation Detail"))))
                        {
                            if (!isReal) return true;

                            HLineExcel p = new HLineExcel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // DORIAN
                        else if (ds.Tables[0].Rows[0][0].ToString().Contains("DORIAN"))
                        {
                            if (!isReal) return true;

                            Dorian p = new Dorian(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // LAUREL
                        else if (rowCount > 3 && colCount > 2 && ds.Tables[0].Rows[2][1].ToString().Contains("INQUIRY"))
                        {
                            if (!isReal) return true;

                            Laurel p = new Laurel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EVERGREEN MARINE TAIWAN
                        else if (rowCount > 1 && colCount > 1 && ((ds.Tables[0].Rows[0][1].ToString().Contains("PURCHASE QUOTATION") || ds.Tables[0].Rows[0][1].ToString().Contains("REPAIR SERVICE DETAIL ITEMS"))))
                        {
                            if (!isReal) return true;

                            EverGreenMarineTAI p = new EverGreenMarineTAI(fileName);
                            p.Parse();


                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Marlow
                        //else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains(""))
                        //{
                        //    if (!isReal) return true;

                        //    Marlow p = new Marlow(fileName);
                        //    p.Parse();

                        //    vessel = p.Vessel;
                        //    reference = p.Reference.ToUpper();
                        //    dtItem = p.Item;
                        //}
                        // POLALIS EXCEL
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("RFQ No :"))
                        {
                            if (!isReal) return true;

                            PolalisExcel p = new PolalisExcel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        // GIMSCO
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[1][0].ToString().Contains("GIMSCO"))
                        {
                            if (!isReal) return true;

                            Gimsco p = new Gimsco(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SOUTHERN
                        else if (rowCount > 10 && colCount > 1 && ds.Tables[0].Rows[9][0].ToString().Contains("UNITED KINGDOM"))
                        {
                            if (!isReal) return true;

                            Southern p = new Southern(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 5 && colCount > 2 && ds.Tables[0].Rows[3][1].ToString().Contains("ASSOCIATED MARITIME COMPANY (HONGKONG) LTD"))
                        {
                            if (!isReal) return true;

                            Associated p = new Associated(fileName);
                            p.Parse();

                            vessel = p.Vessel;

                            if (!string.IsNullOrEmpty(mailSubject) && mailSubject.Contains("("))
                            {
                                int idx_s = mailSubject.IndexOf("(");
                                int idx_e = mailSubject.IndexOf(")");

                                if (idx_s != -1 & idx_e != -1)
                                {
                                    reference = mailSubject.Substring(idx_s, idx_e - idx_s).Trim();
                                    reference = reference.Replace("(", "").Trim();
                                }
                            }

                            if (string.IsNullOrEmpty(reference))
                                reference = p.Reference.ToUpper();

                            dtItem = p.Item;
                        }
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("MARLOW NAVIGATION"))
                        {
                            if (!isReal) return true;

                            Marlow2 p = new Marlow2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 2 && colCount > 1 && ds.Tables[0].Rows[1][0].ToString().Contains("PACC Ship"))
                        {
                            if (!isReal) return true;

                            Pacc p = new Pacc(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("Mt Advantage"))
                        {
                            if (!isReal) return true;

                            Advantage p = new Advantage(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Winners Marine
                        else if (rowCount > 1 && colCount > 3 && ds.Tables[0].Rows[0][2].ToString().Contains("Winners Marine Co.,Ltd."))
                        {
                            if (!isReal) return true;

                            WinnersMarine p = new WinnersMarine(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // GULF
                        else if (rowCount > 1 && colCount > 5 && ds.Tables[0].Rows[0][4].ToString().Contains("Gulf Ship"))
                        {
                            if (!isReal) return true;

                            Gulf p = new Gulf(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ARCADIA
                        else if (rowCount > 4 && colCount > 1 && ds.Tables[0].Rows[3][0].ToString().Contains("Arcadia Shipmanagement"))
                        {
                            if (!isReal) return true;

                            Arcadia p = new Arcadia(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // HALKIDON
                        else if (rowCount > 4 && colCount > 1 && ds.Tables[0].Rows[3][0].ToString().Contains("HALKIDON SHIPPING CORPORATION"))
                        {
                            if (!isReal) return true;

                            Halkidon p = new Halkidon(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("GEMİ ADI"))
                        {
                            if (!isReal) return true;

                            Marinsa p = new Marinsa(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 천경해운
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[1][0].ToString().Contains("I N Q U I R Y"))
                        {
                            if (!isReal) return true;

                            CHEON p = new CHEON(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 3 && colCount > 1 && ds.Tables[0].Rows[2][0].ToString().Contains("HONG KONG MING WAH SHIPPING CO., LTD"))
                        {
                            if (!isReal) return true;

                            HongkongMing p = new HongkongMing(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 1 && colCount > 3 && ds.Tables[0].Rows[0][2].ToString().Contains("Raffles"))
                        {
                            if (!isReal) return true;

                            Raffles p = new Raffles(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // IONIA
                        else if (rowCount > 3 && colCount > 5 && ds.Tables[0].Rows[2][4].ToString().Contains("IONIA"))
                        {
                            if (!isReal) return true;

                            Ionia p = new Ionia(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // POLEMBROS   // CAPE SHIPPING S.A.    // NEREUS SHIPPING
                        else if (rowCount > 5 && colCount > 4 && ((ds.Tables[0].Rows[4][3].ToString().Contains("POLEMBROS") || ds.Tables[0].Rows[4][3].ToString().Contains("CAPE MARITIME") ||
                            ds.Tables[0].Rows[4][3].ToString().Contains("NEREUS SHIPPING"))))
                        {
                            if (!isReal) return true;

                            Polembros p = new Polembros(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 3 && colCount > 5 && ds.Tables[0].Rows[2][4].ToString().StartsWith("GRACE MANAGEMENT"))
                        {
                            if (!isReal) return true;

                            Grace p = new Grace(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 대한해운
                        else if (rowCount > 3 && colCount > 4 && ds.Tables[0].Rows[2][0].ToString().StartsWith("( DOCK )"))
                        {
                            if (!isReal) return true;

                            Daehan p = new Daehan(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // ACTIVE SHIPPING
                        else if (rowCount > 2 && colCount > 5 && ds.Tables[0].Rows[0][4].ToString().StartsWith("ACTIVE DENIZCILIK"))
                        {
                            if (!isReal) return true;

                            ACTIVEDENIZCILIK p = new ACTIVEDENIZCILIK(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //BSM GROUP EXCEL
                        else if (rowCount > 6 && colCount > 4 && ds.Tables[0].Rows[5][3].ToString().StartsWith("Bernhard Schulte Shipmanagement"))
                        {
                            if (!isReal) return true;

                            BSM_EXCEL p = new BSM_EXCEL(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 5 && colCount > 12 && ds.Tables[0].Rows[0][0].ToString().StartsWith("Red Sea Marine"))
                        {
                            if (!isReal) return true;

                            RedSea_excel p = new RedSea_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 5 && colCount > 5 && ds.Tables[0].Rows[0][0].ToString().StartsWith("DİTAŞ DENİZ İŞLETMECİLİĞİ"))
                        {
                            if (!isReal) return true;

                            DitasDeniz p = new DitasDeniz(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        else if (rowCount > 10 && colCount > 7 && ds.Tables[0].Rows[0][0].ToString().StartsWith("SPARE PARTS & EQUIPMENT REQUISITION"))
                        {
                            if (!isReal) return true;

                            AegeanBulk p = new AegeanBulk(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 10 && colCount > 7 && ds.Tables[0].Rows[1][2].ToString().StartsWith("TOLANI SHIPPING CO. LTD"))
                        {
                            if (!isReal) return true;

                            Tolani p = new Tolani(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 10 && colCount > 9 && ds.Tables[0].Rows[1][8].ToString().StartsWith("RB"))
                        {
                            if (!isReal) return true;

                            RB_EXCEL p = new RB_EXCEL(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //danaos flatform
                        else if (rowCount > 10 && colCount > 18 && ds.Tables[0].Rows[0][17].ToString().StartsWith("Excel Form Cre"))
                        {
                            if (!isReal) return true;

                            DanaosExcel p = new DanaosExcel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // CAPE SHIPPING
                        else if (rowCount > 5 && colCount > 4 && ds.Tables[0].Rows[4][3].ToString().Contains("CAPE SHIPPING"))
                        {
                            if (!isReal) return true;

                            CAFESHIPPING_excel p = new CAFESHIPPING_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;

                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 장금상선 엑셀
                        else if (rowCount > 4 && colCount > 3 && ds.Tables[0].Rows[0][0].ToString().Contains("No") && ds.Tables[0].Rows[0][1].ToString().Contains("Mfg Part No") && ds.Tables[0].Rows[0][2].ToString().ToString().Contains("Description"))
                        {
                            if (!isReal) return true;

                            Janggum_excel p = new Janggum_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // EASTERN PACIFIC SHIPPING UK
                        else if (rowCount > 10 && colCount > 10 && ds.Tables[0].Rows[0][0].ToString().StartsWith("Vessel Name :"))
                        {
                            if (!isReal) return true;

                            EasternPacificShipping_excel p = new EasternPacificShipping_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //else if (rowCount > 11 && colCount > 9 && ds.Tables[1].Rows[0][1].ToString().Equals(""))
						//{
                        //
						//}
                        else if (rowCount > 10 && colCount > 7 && ds.Tables[0].Rows[0][0].ToString().StartsWith("REQUISTION FORM"))
                        {
                            if (!isReal) return true;

                            Goodwood_excel p = new Goodwood_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;

                        }
                        else if (rowCount > 1 && colCount > 10 && ds.Tables[0].Rows[0][0].ToString().Contains("No") && ds.Tables[0].Rows[0][1].ToString().Contains("Mfg Part No"))
                        {
                            if (!isReal) return true;

                            jg_excel p = new jg_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 폴라리스 엑셀 NEW
                        else if (rowCount > 1 && colCount > 12 && ds.Tables[0].Rows[0][0].ToString().Contains("Group") && ds.Tables[0].Rows[0][1].ToString().Contains("RFQ_ID"))
                        {
                            if (!isReal) return true;

                            Polaris_excel p = new Polaris_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                            contact = p.Contact;
                        }
                        else if (rowCount > 3 && colCount > 3 && ds.Tables[0].Rows[2][2].ToString().Contains("EMPRESA NAVIERA ELCANO, S.A."))
                        {
                            if (!isReal) return true;

                            EmpresaNavieraElcano_excel p = new EmpresaNavieraElcano_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("Adani Shipping (India) Pvt. Ltd."))
                        {
                            if (!isReal) return true;

                            Adani_excel p = new Adani_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        //else if (rowCount > 1 && colCount > 2 && ds.Tables[0].Rows[3][1].ToString().Contains("Rosy Star Ocean"))
                        //{
                        //    //if (!isReal) return true;


                        //}
                        else if (rowCount > 2 && colCount > 2 && ds.Tables[0].Rows[1][1].ToString().Contains("Vessel Name:"))
                        {
                            if (!isReal) return true;

                            Garret_Excel p = new Garret_Excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        
                        else if (rowCount > 2 && colCount > 1 && ds.Tables[0].Rows[1][0].ToString().Contains("Garrets International Limited"))
                        {
                            if (!isReal) return true;

                            Garrets_excel2 p = new Garrets_excel2(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Anglo 선용 버전 엑셀
                        else if (rowCount > 10 && colCount > 4 && ds.Tables[0].Rows[0][0].ToString().StartsWith("Vessel Name"))
                        {
                            if (!isReal) return true;

                            Anglo_gs_excel p = new Anglo_gs_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 현대상선 (에이치엠엠)
                        else if (colCount > 14 && (ds.Tables[0].Rows[0][6].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][7].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][8].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][9].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][10].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][11].ToString().StartsWith("QUOTATION DETAIL")
                            || ds.Tables[0].Rows[0][12].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][13].ToString().StartsWith("QUOTATION DETAIL") || ds.Tables[0].Rows[0][14].ToString().StartsWith("QUOTATION DETAIL")))
                        {
                            if (!isReal) return true;

                            HMM_excel p = new HMM_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            imoNumber = p.ImoNumber;
                            dtItem = p.Item;
                        }
                        // STOLT TANKERS
                        else if (ds.Tables[0].Rows[0][0].ToString().StartsWith("Line No") && ds.Tables[0].Rows[0][1].ToString().StartsWith("Description") && ds.Tables[0].Rows[0][2].ToString().StartsWith("UOM"))
                        {
                            if (!isReal) return true;

                            STOLT_excel p = new STOLT_excel(fileName);
                            p.Parse();

                            dtItem = p.Item;
                        }
                        // 장금상선
                        else if (ds.Tables[0].Rows[0][0].ToString().Contains("Purchase request"))
                        {
                            if (!isReal) return true;

                            sinokor_excel p = new sinokor_excel(fileName);
                            p.Parse();


                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 2 && ds.Tables[0].Rows[2][3].ToString().StartsWith("TRANSMED SHIPPING LIMITED"))
                        {
                            if (!isReal) return true;


                            transmed_excel p = new transmed_excel(fileName);
                            p.Parse();


                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 3 && ds.Tables[0].Rows[3][0].ToString().StartsWith("Kind") && ds.Tables[0].Rows[4][0].ToString().StartsWith("Vessel Name"))
                        {

                        }
                        else if (rowCount > 7 && ds.Tables[0].Rows[7][2].ToString().StartsWith("㈜제일메카트로닉스") && ds.Tables[0].Rows[2][3].ToString().StartsWith("DUBHE"))
                        {
                            if (!isReal) return true;


                            jeil_dintec_excel p = new jeil_dintec_excel(fileName);
                            p.Parse();


                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 7 && ds.Tables[0].Rows[7][2].ToString().StartsWith("㈜제일메카트로닉스"))
                        {
                            if (!isReal) return true;


                            jeil_dintec_excel p = new jeil_dintec_excel(fileName);
                            p.Parse();


                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 1 && colCount > 1 && (ds.Tables[0].Rows[1][0].ToString().StartsWith("d'Amico") || ds.Tables[0].Rows[1][0].ToString().StartsWith("GLOBAL Maritime Supplies")))
                        {
                            if (!isReal) return true;

                            seaproc_excel p = new seaproc_excel(fileName);
                            p.Parse();


                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // 윈텍 테스트용
                        //else if (ds.Tables[0].Rows[0][0].ToString().Contains(""))
						//{
                        //    //Parsing.Parser.Order.STXOrder_csv2 p = new Parser.Order.STXOrder_csv2(fileName);
                        //    //p.Parse();
                        //
                        //    Parsing.Parser.Order.STXOrder_csv p = new Parser.Order.STXOrder_csv(fileName);
                        //    p.Parse();
                        //}
                        else if (rowCount > 3 && colCount > 1 && ds.Tables[0].Rows[2][0].ToString().Contains("ORDER DETAIL REPORT"))
						{
                            if (!isReal) return true;

                            LYRAS_excel p = new LYRAS_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 2 && colCount > 1 && ds.Tables[0].Rows[0][0].ToString().Contains("Column A~F"))
						{
                            if (!isReal) return true;

                            KSS_new p = new KSS_new(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 2 && colCount > 2 && ds.Tables[0].Rows[0][0].ToString().Contains("Vessel's Name"))
						{
                            if (!isReal) return true;

                            Lyric_excel p = new Lyric_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 2 && colCount > 2 && ds.Tables[0].Rows[1][0].ToString().Contains("REQUISITION INFO"))
						{
                            if (!isReal) return true;

                            Synergy_excel p = new Synergy_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 3 && colCount > 3 && ds.Tables[0].Rows[1][4].ToString().Contains("HYUNDAI LNG SHIP"))
						{
                            if (!isReal) return true;

                            hyundailng_excel p = new hyundailng_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // SEADRILL 20220818
                        else if (rowCount > 2 && colCount > 3 && ds.Tables[0].Rows[0][1].ToString().Contains("REQUESTOR NAME") && ds.Tables[0].Rows[0][2].ToString().Contains("ICN"))
						{
                            if (!isReal) return true;

                            SEADRILL_excel p = new SEADRILL_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // Executive Ship Management 20221115
                        else if (rowCount > 0 && colCount > 1 && ds.Tables[0].Rows[0][1].ToString().Contains("Quotation Items"))
                        {
                            if (!isReal) return true;

                            excutive_excel p = new excutive_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        else if (rowCount > 0 && colCount > 1 && ds.Tables[0].Rows[1][0].ToString().Contains("BFT01@"))
                        {
                            if (!isReal) return true;

                            benelux_excel p = new benelux_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // PROCURESHIP NEW
                        else if (rowCount > 1 && colCount > 1 && ds.Tables[0].Rows[1][1].ToString().Contains("REQUEST FOR QUOTATION"))
                        {
                            if (!isReal) return true;

                            Procureship_excel p = new Procureship_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        // PROCURESHIP SPS S.A.
                        else if (rowCount > 5 && colCount > 5 && ds.Tables[0].Rows[0][6].ToString().Contains("REQUEST FOR QUOTATION") && ds.Tables[0].Rows[2][0].ToString().Contains("RFQ N"))
                        {
                            if (!isReal) return true;

                            Latsco_excel p = new Latsco_excel(fileName);
                            p.Parse();

                            vessel = p.Vessel;
                            reference = p.Reference.ToUpper();
                            dtItem = p.Item;
                        }
                        if (isReal) 
                        {
                            inqfilename = fileName;
                            if (!string.IsNullOrEmpty(inqfilename))
                            {
                                string[] filenameSpl = inqfilename.Split('\\');
                                inqfilename = filenameSpl[filenameSpl.Length - 1].ToString();
                            }
                        }

                    }
                }
            }


            


            return false;

            // 공통 - 스페이스 N개 스페이스를 하나의 공백으로 치환
            //foreach (

            // 공통 - 정규화 한다

            // 컬럼 추가
            dtItem.Columns.Add("FLAG");

            foreach (DataRow row in dtItem.Rows)
            {
                string desc = row["DESC"].ToString();

                row["SUBJ"] = Normalize(row["SUBJ"], true);
                row["ITEM"] = Normalize(row["ITEM"], true);
                row["DESC"] = Normalize(desc, true);
                row["FLAG"] = Normalize(desc, false);
            }





            // 공통 - 아이템에 반복되는 문자열은 서브젝트로 올린다
            //string[] text = dt.Rows[7][3].ToString().Split(' ');

            //if (text.Length > 2)
            //    vessel = text[0] + " " + text[1];
            //else
            //    vessel = string.Join("", text);

            // 앞에서 부터(최소 3개 단어 이상 반복)
            string[] arr = dtItem.Rows[0]["FLAG"].ToString().Split(' ');
            //int i = 2;
            //string repeatStr = "";

            //while (i < arr.Length)
            //{
            //    string countStr = string.Join(" ", arr, 0, i);
            //    int count = dtItem.Select("DESC LIKE '" + countStr + "%'").Length;

            //    if (count == dtItem.Rows.Count)
            //    {
            //        repeatStr = countStr;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            //if (repeatStr != "")
            //{
            //}


            // 뒤에서 부터
            int flag = 3;
            string countStr = "";
            string repeatStr = "";

            for (int i = arr.Length - 1; i > 0; i--)
            {
                countStr = (arr[i] + " " + countStr).Trim();

                if (arr.Length - i >= flag)
                {
                    if (dtItem.Select("FLAG LIKE '%" + countStr + "'").Length == dtItem.Rows.Count)
                        repeatStr = countStr;
                    else
                        break;
                }
            }

            // 반복 문자열이 있는 경우
            if (repeatStr != "")
            {
                foreach (DataRow row in dtItem.Rows)
                {
                    // 서브젝트에 반복 문자열 추가
                    row["SUBJ"] = row["SUBJ"] + "\r\n" + repeatStr;

                    string desc = row["DESC"].ToString();
                    desc = @"
A
B 
C";

                    while (true)
                    {
                        if (desc.IndexOf(repeatStr) > 0)
                        {
                            row["DESC"] = Normalize(desc.Replace(repeatStr, ""), true);
                            break;
                        }
                        else
                        {
                            int i = desc.LastIndexOf("\r\n");
                            desc = desc.Remove(i, 2).Insert(i, " ");
                            desc = Regex.Replace(desc, @"\s+", " ");
                        }
                    }

                    //row["DESC"] = Normalize(row["DESC"].ToString().Replace(repeatStr, ""));
                }
            }

            return false;
        }

        private string Normalize(object text, bool keepCRLF)
        {
            string s = text.ToString();
            s = s.Replace(",", ", ");
            s = s.Replace(":", ": ");
            s = s.Replace(" ,", ",");
            s = s.Replace(" :", ":");

            s = s.Trim();

            // 줄바꿈 여부
            if (keepCRLF)
                s = Regex.Replace(s, @"[^\S\r\n]+", " ");
            else
                s = Regex.Replace(s, @"\s+", " ");

            // 양쪽 끝 특수문자 여부
            if (s.Right(1) == ",") s = s.Substring(0, s.Length - 1);

            return s;
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
                    _filename = Regex.Replace(item.LongFileName, @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ .]", string.Empty);
                    _filename = FileMgr.GetUniqueFileName(Path.Combine(Application.StartupPath, "temp") + "\\" + _filename);

                    if (!_filename.StartsWith("image")) 
                    {
                        localpath[fileCount] = Path.Combine(Application.StartupPath, "temp") + "\\" + _filename;
                        item.Save(localpath[fileCount]);

                        fileCount++;
                    }
                }

                return localpath;
            }
            else
            {
                return fileName;
            }
        }

        #endregion
    }
}
