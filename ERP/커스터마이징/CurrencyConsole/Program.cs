using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace CurrencyConsole
{
    class Program
    {
        public static SqlConnection _sqlConnetion;
        public static SqlDataAdapter _sqlDataAdapter;
        public static SqlCommand _sqlCommand;
        public static string _connectionString;
        public static DataTable _dtYear, _dtQuarter, _dtMonth, _dtDay, _dtForecast;

        static void Main(string[] args)
        {
            환율정보가져오기();
        }

        public static void 환율정보가져오기()
        {
            DateTime today;
            string 사용자등록통계표, 분기일자;

            try
            {
                DB접속();

                _dtYear = GetDataTable("SELECT * FROM CZ_LN_CURR_YEAR WITH(NOLOCK) WHERE 1 <> 1");
                _dtQuarter = GetDataTable("SELECT * FROM CZ_LN_CURR_QUARTER WITH(NOLOCK) WHERE 1 <> 1");
                _dtMonth = GetDataTable("SELECT * FROM CZ_LN_CURR_MONTH WITH(NOLOCK) WHERE 1 <> 1");
                _dtDay = GetDataTable("SELECT * FROM CZ_LN_CURR_DAY WITH(NOLOCK) WHERE 1 <> 1");
                _dtForecast = GetDataTable("SELECT * FROM CZ_LN_CURR_FORECAST WITH(NOLOCK) WHERE 1 <> 1");

                today = DateTime.Today;

                #region 년별

                #region 국내총생산(한국)
                WriteLog("ECOS, 년별 -> 18.1.16 국내총생산 -> 국내총생산(한국)");
                DownloadECOS("I05Y004", "YY", today.ToString("yyyy"), "KOR", "?", "?", "GDP_K");
                #endregion

                #region 국내총생산(미국)
                WriteLog("ECOS, 년별 -> 18.1.16 국내총생산 -> 국내총생산(미국)");
                DownloadECOS("I05Y004", "YY", today.ToString("yyyy"), "USA", "?", "?", "GDP_US");
                #endregion

                SaveDataYear();

                #endregion

                #region 분기별
                분기일자 = GetQuarterDate(today.ToString("yyyyMM"));
                WriteLog("분기일자 : " + 분기일자);

                #region 경제성장률(한국)
                WriteLog("ECOS, 분기별 -> 18.1.15 경제성장률 -> 경제성장률(한국)");
                DownloadECOS("I05Y003", "QQ", 분기일자, "KOR", "?", "?", "EC_R_K");
                #endregion

                #region 경제성장률(미국)
                WriteLog("ECOS, 분기별 -> 18.1.15 경제성장률 -> 경제성장률(미국)");
                DownloadECOS("I05Y003", "QQ", 분기일자, "USA", "?", "?", "EC_R_US");
                #endregion

                #region 환율예측 (Trading Economics)
                WriteLog("Trading Economics, 분기별 -> 환율예측");
                DownloadTradingEconomics();
                #endregion

                #region 환율예측 (DATA.go.kr)
                WriteLog("DATA.go.kr, 분기별 -> 환율예측");
                DownloadData();
                #endregion

                SaveDataQuarter();
                SaveDataForecast();
                #endregion

                #region 월별

                #region 경상수지
                WriteLog("ECOS, 월별 -> 8.1.1 국제수지 -> 경상수지");
                DownloadECOS("022Y013", "MM", today.ToString("yyyyMM"), "000000", "?", "?", "CURR_BAL");
                #endregion

                #region 자본수지
                WriteLog("ECOS, 월별 -> 8.1.1 국제수지 -> 자본수지");
                DownloadECOS("022Y013", "MM", today.ToString("yyyyMM"), "BOPC00000000", "?", "?", "CAPIT_BAL");
                #endregion

                #region KOSPI
                WriteLog("ECOS, 월별 -> 18.1.6 주가지수 -> KOSPI");
                DownloadECOS("I04Y006", "MM", today.ToString("yyyyMM"), "3010101", "?", "?", "KOSPI");
                #endregion

                #region Dow Jones
                WriteLog("ECOS, 월별 -> 18.1.6 주가지수 -> Dow Jones");
                DownloadECOS("I04Y006", "MM", today.ToString("yyyyMM"), "3020101", "?", "?", "DOW_JONES");
                #endregion

                #region NASDAQ
                WriteLog("ECOS, 월별 -> 18.1.6 주가지수 -> NASDAQ");
                DownloadECOS("I04Y006", "MM", today.ToString("yyyyMM"), "3020103", "?", "?", "NASDAQ");
                #endregion

                #region WTI(현물)
                WriteLog("ECOS, 월별 -> 18.1.9 국제상품가격 -> WTI(현물)");
                DownloadECOS("I04Y009", "MM", today.ToString("yyyyMM"), "4010101", "?", "?", "WTI");
                #endregion

                #region 소비자물가지수(한국)
                WriteLog("ECOS, 월별 -> 18.1.8 소비자물가지수 -> 소비자물가지수(한국)");
                DownloadECOS("I02Y002", "MM", today.ToString("yyyyMM"), "KOR", "?", "?", "CUS_PR_K");
                #endregion

                #region 소비자물가지수(미국)
                WriteLog("ECOS, 월별 -> 18.1.8 소비자물가지수 -> 소비자물가지수(미국)");
                DownloadECOS("I02Y002", "MM", today.ToString("yyyyMM"), "USA", "?", "?", "CUS_PR_US");
                #endregion

                #region 생산자물가지수(한국)
                WriteLog("ECOS, 월별 -> 18.1.7 생산자물가지수 -> 생산자물가지수(한국)");
                DownloadECOS("I02Y001", "MM", today.ToString("yyyyMM"), "KOR", "?", "?", "PROD_PR_K");
                #endregion

                #region 생산자물가지수(미국)
                WriteLog("ECOS, 월별 -> 18.1.7 생산자물가지수 -> 생산자물가지수(미국)");
                DownloadECOS("I02Y001", "MM", today.ToString("yyyyMM"), "USA", "?", "?", "PROD_PR_US");
                #endregion

                #region 산업생산지수(한국)
                WriteLog("ECOS, 월별 -> 18.1.13 산업생산지수(계절변동조정) -> 산업생산지수(한국)");
                DownloadECOS("I05Y001", "MM", today.ToString("yyyyMM"), "KOR", "?", "?", "INDUST_K");
                #endregion

                #region 산업생산지수(미국)
                WriteLog("ECOS, 월별 -> 18.1.13 산업생산지수(계절변동조정) -> 산업생산지수(미국)");
                DownloadECOS("I05Y001", "MM", today.ToString("yyyyMM"), "USA", "?", "?", "INDUST_US");
                #endregion

                #region 미국연방기금금리
                WriteLog("KOSIS, 월별 -> 미국연방기금금리");
                사용자등록통계표 = "dintec/101/DT_2FAKA/2/1/20190605184934_1";
                DownloadKOSIS(사용자등록통계표, today.ToString("yyyyMM"), "US_FED_R");
                #endregion

                SaveDataMonth();

                #endregion

                #region 일별

                #region 일본엔/달러
                WriteLog("ECOS, 일별 -> 8.8.1.2 주요국통화의 대미달러 환율 -> 일본엔/달러");
                DownloadECOS("036Y002", "DD", today.ToString("yyyyMMdd"), "0000002", "?", "?", "YEN_EXC_R");
                #endregion

                #region 달러/유로
                WriteLog("ECOS, 일별 -> 8.8.1.2 주요국통화의 대미달러 환율 -> 달러/유로");
                DownloadECOS("036Y002", "DD", today.ToString("yyyyMMdd"), "0000003", "?", "?", "EURO_EXC_R");
                #endregion

                #region 콜금리(1일, 전체거래)
                WriteLog("ECOS, 일별 -> 4.1.1 시장금리(일별) -> 콜금리(1일, 전체거래)");
                DownloadECOS("060Y001", "DD", today.ToString("yyyyMMdd"), "010101000", "?", "?", "CALL_R");
                #endregion

                #region 원/미국달러(매매기준율)
                WriteLog("ECOS, 일별 -> 8.8.1.1 주요국통화의 대원화 환율 -> 원/미국달러(매매기준율)");
                DownloadECOS("036Y001", "DD", today.ToString("yyyyMMdd"), "0000001", "?", "?", "EXC_R");
                #endregion

                #region KOSPI
                //WriteLog("ECOS, 일별 -> 6.1.1 주식시장(일별) -> KOSPI");
                //DownloadECOS("064Y001", "DD", today.ToString("yyyyMM"), "0001000", "?", "?", "KOSPI");

                WriteLog("Naver, 일별 -> KOSPI");
                DownloadNaver("KOSPI");
                #endregion

                #region NASDAQ
                WriteLog("FRED, 일별 -> NASDAQ");
                DownloadFRED("NASDAQCOM", "NASDAQ");

                //WriteLog("Naver, 일별 -> NASDAQ");
                //DownloadNaver("NASDAQ");
                #endregion

                #region Dow Jones
                WriteLog("FRED, 일별 -> Dow Jones");
                DownloadFRED("DJIA", "DOW_JONES");

                //WriteLog("Naver, 일별 -> Dow Jones");
                //DownloadNaver("DOW_JONES");
                #endregion

                #region WTI
                WriteLog("Naver, 일별 -> WTI");
                DownloadNaver("WTI");
                #endregion

                SaveDataDay();

                #endregion

                SaveDataSync();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                DB접속해제();
            }
        }

        public static void DownloadECOS(string 통계코드, string 주기, string 검색종료일자, string 항목코드1, string 항목코드2, string 항목코드3, string 컬럼명)
        {
            Uri address;
            WebClient webClient = null;
            XmlDocument xmlDoc;
            XmlElement root;
            DataTable dt;
            DataRow[] dataRowArray;
            DataRow dr;
            string key, xml, 주기컬럼, 검색시작일자;

            try
            {
                #region 검색시작일자
                switch (주기)
                {
                    case "YY":
                        주기컬럼 = "YEAR";
                        검색시작일자 = GetDataTable(@"SELECT ISNULL(MAX(YEAR), '2000') AS YEAR
                                                      FROM CZ_LN_CURR_YEAR WITH (NOLOCK)
                                                      WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                                     "AND " + 컬럼명 + " <> ''").Rows[0][주기컬럼].ToString();
                        break;
                    case "QQ":
                        주기컬럼 = "QUARTER";
                        검색시작일자 = GetDataTable(@"SELECT ISNULL(MAX(QUARTER), '20001') AS QUARTER
                                                      FROM CZ_LN_CURR_QUARTER WITH (NOLOCK)
                                                      WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                                     "AND " + 컬럼명 + " <> ''").Rows[0][주기컬럼].ToString();
                        break;
                    case "MM":
                        주기컬럼 = "MONTH";
                        검색시작일자 = GetDataTable(@"SELECT ISNULL(MAX(MONTH), '200001') AS MONTH
                                                      FROM CZ_LN_CURR_MONTH WITH (NOLOCK)
                                                      WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                                     "AND " + 컬럼명 + " <> ''").Rows[0][주기컬럼].ToString();
                        break;
                    case "DD":
                        주기컬럼 = "DAY";
                        검색시작일자 = GetDataTable(@"SELECT ISNULL(MAX(DAY), '20000101') AS DAY
                                                      FROM CZ_LN_CURR_DAY WITH (NOLOCK)
                                                      WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                                     "AND " + 컬럼명 + " <> ''").Rows[0][주기컬럼].ToString();
                        break;
                    default:
                        return;
                }
                #endregion

                key = "YZQUV7NO9KX5UUW77AQY";
                address = new Uri("http://ecos.bok.or.kr/api/StatisticSearch/" + key + "/xml/kr/1/100000/" + 통계코드 + "/" + 주기 + "/" + 검색시작일자 + "/" + 검색종료일자 + "/" + 항목코드1 + "/" + 항목코드2 + "/" + 항목코드3 + "/");

                WriteLog(address.OriginalString);

                webClient = new WebClient() { Encoding = Encoding.UTF8 };
                xml = webClient.DownloadString(address);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                root = xmlDoc.DocumentElement;

                switch (주기)
                {
                    case "YY":
                        dt = _dtYear;
                        break;
                    case "QQ":
                        dt = _dtQuarter;
                        break;
                    case "MM":
                        dt = _dtMonth;
                        break;
                    case "DD":
                        dt = _dtDay;
                        break;
                    default:
                        return;
                }

                foreach (XmlElement row in root.GetElementsByTagName("row"))
                {
                    dataRowArray = dt.Select(주기컬럼 + " = '" + row["TIME"].InnerText + "'");

                    if (dataRowArray.Length > 0)
                    {
                        dataRowArray[0][컬럼명] = row["DATA_VALUE"].InnerText;
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr[주기컬럼] = row["TIME"].InnerText;
                        dr[컬럼명] = row["DATA_VALUE"].InnerText;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DownloadKOSIS(string 사용자등록통계표, string 종료수록시점, string 컬럼명)
        {
            Uri address;
            WebClient webClient = null;
            XmlDocument xmlDoc;
            XmlElement root;
            DataRow[] dataRowArray;
            DataRow dr;
            string key, xml, 시작수록시점;

            try
            {
                시작수록시점 = GetDataTable(@"SELECT ISNULL(MAX(MONTH), '200001') AS MONTH
                                              FROM CZ_LN_CURR_MONTH WITH (NOLOCK)
                                              WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                             "AND " + 컬럼명 + " <> ''").Rows[0]["MONTH"].ToString();

                key = "NWI5ZmVlMzUyNzRlZTUxZGYxOTU4NjhmNTU5ZDgyYmY=";
                address = new Uri("http://kosis.kr/openapi/statisticsData.do?method=getList&apiKey=" + key + "&format=sdmx&jsonVD=Y&userStatsId=" + 사용자등록통계표 + "&type=StructureSpecific&prdSe=M&startPrdDe=" + 시작수록시점 + "&endPrdDe=" + 종료수록시점 + "&version=v2_1");

                WriteLog(address.OriginalString);

                webClient = new WebClient() { Encoding = Encoding.UTF8 };
                xml = webClient.DownloadString(address);

                if (!string.IsNullOrEmpty(xml))
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    root = xmlDoc.DocumentElement;

                    foreach (XmlElement row in root.GetElementsByTagName("Obs"))
                    {
                        dataRowArray = _dtMonth.Select("MONTH = '" + row.GetAttribute("TIME_PERIOD").Replace("-", "") + "'");

                        if (dataRowArray.Length > 0)
                        {
                            dataRowArray[0][컬럼명] = row.GetAttribute("OBS_VALUE");
                        }
                        else
                        {
                            dr = _dtMonth.NewRow();
                            dr["MONTH"] = row.GetAttribute("TIME_PERIOD").Replace("-", "");
                            dr[컬럼명] = row.GetAttribute("OBS_VALUE");
                            _dtMonth.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DownloadTradingEconomics()
        {
            Uri address;
            WebClient webClient = null;
            DataRow dr, dr1, dr2, dr3;
            string[] delimiter, tmpStr, tmpCol, tmpCol2;
            string source, tmpCol1;

            try
            {
                address = new Uri("https://tradingeconomics.com/forecast/currency");
                webClient = new WebClient() { Encoding = Encoding.UTF8 };
                source = webClient.DownloadString(address);
                delimiter = new string[] { "Price" };
                tmpStr = source.Split(delimiter, StringSplitOptions.None);

                foreach (string tmpStr1 in tmpStr)
                {
                    if (tmpStr1.IndexOf("USDKRW:CUR") > 0)
                    {
                        dr = _dtForecast.NewRow();
                        dr1 = _dtForecast.NewRow();
                        dr2 = _dtForecast.NewRow();
                        dr3 = _dtForecast.NewRow();

                        dr["TP_GUBUN"] = "Trading Economics";
                        dr1["TP_GUBUN"] = "Trading Economics";
                        dr2["TP_GUBUN"] = "Trading Economics";
                        dr3["TP_GUBUN"] = "Trading Economics";

                        dr["DT_MONTH"] = DateTime.Today.ToString("yyyyM");
                        dr1["DT_MONTH"] = DateTime.Today.ToString("yyyyM");
                        dr2["DT_MONTH"] = DateTime.Today.ToString("yyyyM");
                        dr3["DT_MONTH"] = DateTime.Today.ToString("yyyyM");
 
                        delimiter = new string[] { "</th>" };
                        tmpCol = tmpStr1.Split(delimiter, StringSplitOptions.None);

                        tmpCol1 = tmpCol[3].Split('>')[2].Split('<')[0];
                        tmpCol2 = tmpCol1.Split('/');
                        dr["DT_QUARTER"] = "20" + tmpCol2[1] + tmpCol2[0].Replace("Q", "");

                        tmpCol1 = tmpCol[4].Split('>')[2].Split('<')[0];
                        tmpCol2 = tmpCol1.Split('/');
                        dr1["DT_QUARTER"] = "20" + tmpCol2[1] + tmpCol2[0].Replace("Q", "");

                        tmpCol1 = tmpCol[5].Split('>')[2].Split('<')[0];
                        tmpCol2 = tmpCol1.Split('/');
                        dr2["DT_QUARTER"] = "20" + tmpCol2[1] + tmpCol2[0].Replace("Q", "");

                        tmpCol1 = tmpCol[6].Split('>')[2].Split('<')[0];
                        tmpCol2 = tmpCol1.Split('/');
                        dr3["DT_QUARTER"] = "20" + tmpCol2[1] + tmpCol2[0].Replace("Q", "");

                        delimiter = new string[] { "data-symbol" };
                        string[] tmpRow = tmpStr1.Split(delimiter, StringSplitOptions.None);

                        foreach (string tmpRow1 in tmpRow)
                        {
                            if (tmpRow1.IndexOf("USDKRW:CUR") > 0)
                            {
                                delimiter = new string[] { "</td>" };
                                string[] tmpRow2 = tmpRow1.Split(delimiter, StringSplitOptions.None);

                                dr["RT_USD"] = tmpRow2[5].Split('>')[1].Trim().Replace(",", "");
                                dr1["RT_USD"] = tmpRow2[6].Split('>')[1].Trim().Replace(",", "");
                                dr2["RT_USD"] = tmpRow2[7].Split('>')[1].Trim().Replace(",", "");
                                dr3["RT_USD"] = tmpRow2[8].Split('>')[1].Trim().Replace(",", "");

                                _dtForecast.Rows.Add(dr);
                                _dtForecast.Rows.Add(dr1);
                                _dtForecast.Rows.Add(dr2);
                                _dtForecast.Rows.Add(dr3);

                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DownloadFRED(string 코드, string 컬럼명)
        {
            Uri address;
            WebClient webClient = null;
            XmlDocument xmlDoc;
            XmlElement root;
            DataRow[] dataRowArray;
            DataRow dr;
            string key, xml, 검색시작일자;

            try
            {
                검색시작일자 = GetDataTable(@"SELECT ISNULL(CONVERT(CHAR(10), CONVERT(DATE, MAX(DAY))), '2000-01-01') AS DAY
                                              FROM CZ_LN_CURR_DAY WITH (NOLOCK)
                                              WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                             "AND " + 컬럼명 + " <> ''").Rows[0]["DAY"].ToString();

                key = "3dd0aaf1fcecf95eb47af777f20f3608";
                address = new Uri("https://api.stlouisfed.org/fred/series/observations?series_id=" + 코드 + "&observation_start=" + 검색시작일자 + "&observation_end=9999-12-31&api_key=" + key);

                WriteLog(address.OriginalString);

                webClient = new WebClient() { Encoding = Encoding.UTF8 };
                xml = webClient.DownloadString(address);

                if (!string.IsNullOrEmpty(xml))
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    root = xmlDoc.DocumentElement;

                    foreach (XmlElement row in root.GetElementsByTagName("observation"))
                    {
                        if (row.GetAttribute("value") != ".")
                        {
                            dataRowArray = _dtDay.Select("DAY = '" + row.GetAttribute("date").Replace("-", "") + "'");

                            if (dataRowArray.Length > 0)
                            {
                                dataRowArray[0][컬럼명] = row.GetAttribute("value");
                            }
                            else
                            {
                                dr = _dtDay.NewRow();
                                dr["DAY"] = row.GetAttribute("date").Replace("-", "");
                                dr[컬럼명] = row.GetAttribute("value");
                                _dtDay.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DownloadNaver(string 컬럼명)
        {
            Uri address;
            WebClient webClient = null;
            DataRow[] dataRowArray;
            DataRow dr;
            string[] delimiter, tmpStr, tmpStr2;
            string source, date, value, 검색시작일자;
            int page;

            try
            {
                검색시작일자 = GetDataTable(@"SELECT ISNULL(MAX(DAY), '20000101') AS DAY
                                              FROM CZ_LN_CURR_DAY WITH (NOLOCK)
                                              WHERE " + 컬럼명 + " IS NOT NULL" + Environment.NewLine +
                                             "AND " + 컬럼명 + " <> ''").Rows[0]["DAY"].ToString();

                page = 1;
                date = "99991231";

                while (Convert.ToInt32(date) >= Convert.ToInt32(검색시작일자))
                {
                    if (컬럼명 == "WTI")
                        address = new Uri("https://finance.naver.com/marketindex/worldDailyQuote.nhn?marketindexCd=OIL_CL&fdtc=2&page=" + page.ToString());
                    else if (컬럼명 == "KOSPI")
                        address = new Uri("https://finance.naver.com/sise/sise_index_day.nhn?code=KOSPI&page=" + page.ToString());
                    else if (컬럼명 == "DOW_JONES")
                        address = new Uri("https://finance.naver.com/world/worldDayListJson.nhn?symbol=DJI@DJI&fdtc=0&page=" + page.ToString());
                    else if (컬럼명 == "NASDAQ")
                        address = new Uri("https://finance.naver.com/world/worldDayListJson.nhn?symbol=NAS@IXIC&fdtc=0&page=" + page.ToString());
                    else
                        return;

                    WriteLog(address.OriginalString);

                    webClient = new WebClient() { Encoding = Encoding.UTF8 };
                    source = webClient.DownloadString(address);

                    switch (컬럼명)
                    {
                        case "WTI":
                        case "KOSPI":
                            #region Html
                            delimiter = new string[] { "<tr" };
                            tmpStr = source.Split(delimiter, StringSplitOptions.None);
                            
                            foreach (string tmpStr1 in tmpStr)
                            {
                                if (tmpStr1.IndexOf("date") > 0)
                                {
                                    delimiter = new string[] { "<td" };
                                    tmpStr2 = tmpStr1.Split(delimiter, StringSplitOptions.None);
                            
                                    date = tmpStr2[1].Split('>')[1].Split('<')[0].Trim().Replace(".", "");
                                    value = tmpStr2[2].Split('>')[1].Split('<')[0].Trim().Replace(",", "");
                            
                                    dataRowArray = _dtDay.Select("DAY = '" + date + "'");
                            
                                    if (dataRowArray.Length > 0)
                                    {
                                        dataRowArray[0][컬럼명] = value;
                                    }
                                    else
                                    {
                                        dr = _dtDay.NewRow();
                                        dr["DAY"] = date;
                                        dr[컬럼명] = value;
                                        _dtDay.Rows.Add(dr);
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "DOW_JONES":
                        case "NASDAQ":
                            #region Json
                            delimiter = new string[] { "{" };
                            tmpStr = source.Split(delimiter, StringSplitOptions.None);

                            foreach (string tmpStr1 in tmpStr)
                            {
                                if (tmpStr1.IndexOf("xymd") > 0)
                                {
                                    delimiter = new string[] { "," };
                                    tmpStr2 = tmpStr1.Split(delimiter, StringSplitOptions.None);

                                    date = tmpStr2[1].Split('"')[3];
                                    value = tmpStr2[5].Split(':')[1];

                                    dataRowArray = _dtDay.Select("DAY = '" + date + "'");

                                    if (dataRowArray.Length > 0)
                                    {
                                        dataRowArray[0][컬럼명] = value;
                                    }
                                    else
                                    {
                                        dr = _dtDay.NewRow();
                                        dr["DAY"] = date;
                                        dr[컬럼명] = value;
                                        _dtDay.Rows.Add(dr);
                                    }
                                }
                            }
                            #endregion
                            break;
                    }

                    page++;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DownloadData()
        {
            Uri address;
            WebClient webClient = null;
            XmlDocument xmlDoc;
            XmlElement root;
            DataRow dr;
            string[] delimiter, tmpStr;
            string source, xml, url, id, 기관명, 고시월, 분기, 환율;

            try
            {
                address = new Uri("https://www.data.go.kr/dataset/3078610/fileData.do");

                webClient = new WebClient() { Encoding = Encoding.UTF8 };
                source = webClient.DownloadString(address);

                delimiter = new string[] { "input" };
                tmpStr = source.Split(delimiter, StringSplitOptions.None);

                foreach (string tmpStr1 in tmpStr)
                {
                    if (tmpStr1.IndexOf("data-url=") > 0 && tmpStr1.IndexOf(".xml") > 0)
                    {
                        delimiter = new string[] { "data-url=" };
                        url = tmpStr1.Split(delimiter, StringSplitOptions.None)[1].Split('\"')[1];

                        delimiter = new string[] { "data-id=" };
                        id = tmpStr1.Split(delimiter, StringSplitOptions.None)[1].Split('\"')[1];

                        address = new Uri("https://www.data.go.kr" + url + "&publicDataDetailPk=" + id);

                        WriteLog(address.OriginalString);

                        webClient = new WebClient() { Encoding = Encoding.UTF8 };
                        xml = webClient.DownloadString(address);

                        xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xml);

                        root = xmlDoc.DocumentElement;

                        foreach (XmlElement row in root.GetElementsByTagName("Row"))
                        {
                            기관명 = string.Empty;
                            고시월 = string.Empty;
                            분기 = string.Empty;
                            환율 = string.Empty;

                            foreach (XmlElement row1 in row)
                            {
                                if (row1.Name == "기관명")
                                {
                                    기관명 = row1.InnerText;
                                }
                                else if (row1.Name.IndexOf("월") > 0)
                                {
                                    고시월 = row1.Name.Replace("_", "").Replace("년", "").Replace("월", "");

                                    if (고시월 != DateTime.Today.ToString("yyyyM"))
                                        break;
                                }
                                else
                                {
                                    분기 = row1.Name.Replace("_", "").Replace("년", "").Replace("분기", "");
                                    환율 = row1.InnerText;

                                    if (환율 != "-")
                                    {
                                        dr = _dtForecast.NewRow();

                                        dr["TP_GUBUN"] = 기관명;
                                        dr["DT_QUARTER"] = 분기;
                                        dr["DT_MONTH"] = 고시월;
                                        dr["RT_USD"] = 환율;
                                        
                                        _dtForecast.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DBConnection생성()
        {
            _connectionString = string.Format("Data Source={0},{1};Initial Catalog={2};User id={3};Password={4}",
                                                   "192.168.1.143",
                                                   "1433",
                                                   "NEOE",
                                                   "NEOE",
                                                   "NEOE");

            _sqlConnetion = new SqlConnection(_connectionString);
        }

        public static void DB접속()
        {
            try
            {
                if (_sqlConnetion != null)
                {
                    if (_sqlConnetion.State == ConnectionState.Closed)
                    {
                        _sqlConnetion.Open();
                        WriteLog("DB 접속");
                    }
                }
                else
                {
                    DBConnection생성();
                    _sqlConnetion.Open();
                    WriteLog("DB 접속");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DB접속해제()
        {
            try
            {
                if (_sqlConnetion != null)
                {
                    if (_sqlConnetion.State == ConnectionState.Open)
                    {
                        _sqlConnetion.Close();
                        WriteLog("DB 접속해제");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static DataTable GetDataTable(string sql)
        {
            DataTable dt;
            
            try
            {
                dt = new DataTable();

                Console.WriteLine(sql);

                _sqlCommand = new SqlCommand(sql, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlDataAdapter = new SqlDataAdapter();
                _sqlDataAdapter.SelectCommand = _sqlCommand;
                _sqlDataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return null;
        }

        public static void SaveDataYear()
        {
            string procedure;

            try
            {
                if (_dtYear == null || _dtYear.Rows.Count == 0) return;

                procedure = "SP_CZ_LN_CURR_YEAR";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtYear.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_YEAR", dataRow["YEAR"]);
                    _sqlCommand.Parameters.AddWithValue("@P_GDP_K", dataRow["GDP_K"]);
                    _sqlCommand.Parameters.AddWithValue("@P_GDP_US", dataRow["GDP_US"]);

                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("저장 : " + _dtYear.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void SaveDataQuarter()
        {
            string procedure;

            try
            {
                if (_dtQuarter == null || _dtQuarter.Rows.Count == 0) return;

                procedure = "SP_CZ_LN_CURR_QUARTER";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtQuarter.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_QUARTER", dataRow["QUARTER"]);
                    _sqlCommand.Parameters.AddWithValue("@P_EC_R_K", dataRow["EC_R_K"]);
                    _sqlCommand.Parameters.AddWithValue("@P_EC_R_US", dataRow["EC_R_US"]);

                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("저장 : " + _dtQuarter.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void SaveDataForecast()
        {
            string procedure;

            try
            {
                if (_dtForecast == null || _dtForecast.Rows.Count == 0) return;

                procedure = "SP_CZ_LN_CURR_FORECAST";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtForecast.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_TP_GUBUN", dataRow["TP_GUBUN"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DT_QUARTER", dataRow["DT_QUARTER"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DT_MONTH", dataRow["DT_MONTH"]);
                    _sqlCommand.Parameters.AddWithValue("@P_RT_USD", dataRow["RT_USD"]);

                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("저장 : " + _dtForecast.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void SaveDataMonth()
        {
            string procedure;

            try
            {
                if (_dtMonth == null || _dtMonth.Rows.Count == 0) return;

                procedure = "SP_CZ_LN_CURR_MONTH";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtMonth.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_MONTH", dataRow["MONTH"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CURR_BAL", dataRow["CURR_BAL"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CAPIT_BAL", dataRow["CAPIT_BAL"]);
                    _sqlCommand.Parameters.AddWithValue("@P_KOSPI", dataRow["KOSPI"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DOW_JONES", dataRow["DOW_JONES"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NASDAQ", dataRow["NASDAQ"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CUS_PR_K", dataRow["CUS_PR_K"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CUS_PR_US", dataRow["CUS_PR_US"]);
                    _sqlCommand.Parameters.AddWithValue("@P_PROD_PR_K", dataRow["PROD_PR_K"]);
                    _sqlCommand.Parameters.AddWithValue("@P_PROD_PR_US", dataRow["PROD_PR_US"]);
                    _sqlCommand.Parameters.AddWithValue("@P_INDUST_K", dataRow["INDUST_K"]);
                    _sqlCommand.Parameters.AddWithValue("@P_INDUST_US", dataRow["INDUST_US"]);
                    _sqlCommand.Parameters.AddWithValue("@P_WTI", dataRow["WTI"]);
                    _sqlCommand.Parameters.AddWithValue("@P_US_FED_R", dataRow["US_FED_R"]);

                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("저장 : " + _dtMonth.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void SaveDataDay()
        {
            string procedure;

            try
            {
                if (_dtDay == null || _dtDay.Rows.Count == 0) return;

                procedure = "SP_CZ_LN_CURR_DAY";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtDay.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_DAY", dataRow["DAY"]);
                    _sqlCommand.Parameters.AddWithValue("@P_YEN_EXC_R", dataRow["YEN_EXC_R"]);
                    _sqlCommand.Parameters.AddWithValue("@P_EURO_EXC_R", dataRow["EURO_EXC_R"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CALL_R", dataRow["CALL_R"]);
                    _sqlCommand.Parameters.AddWithValue("@P_EXC_R", dataRow["EXC_R"]);
                    _sqlCommand.Parameters.AddWithValue("@P_KOSPI", dataRow["KOSPI"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DOW_JONES", dataRow["DOW_JONES"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NASDAQ", dataRow["NASDAQ"]);
                    _sqlCommand.Parameters.AddWithValue("@P_WTI", dataRow["WTI"]);

                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("저장 : " + _dtDay.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void SaveDataSync()
        {
            string procedure;

            try
            {
                procedure = "SP_CZ_LN_CURR_SYNC";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                _sqlCommand.ExecuteNonQuery();

                WriteLog("저장 : SP_CZ_LN_CURR_SYNC");
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void WriteLog(string log)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + log + Environment.NewLine);
            파일쓰기(DateTime.Today.ToString("yyyyMMdd") + "_log", log);
        }

        private static void 파일쓰기(string fileName, string text)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            string filePath;

            try
            {
                if (string.IsNullOrEmpty(text.Trim())) return;

                filePath = "C:\\Currency\\" + fileName + ".txt";

                if (!Directory.Exists("C:\\Currency"))
                {
                    Directory.CreateDirectory("C:\\Currency");
                }

                fileStream = new FileStream(filePath, FileMode.Append);

                streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }

        public static string GetQuarterDate(string date)
        {
            string year, month, quarter;

            try
            {
                year = date.Substring(0, 4);
                month = date.Substring(4, 2);
                quarter = Math.Ceiling(Convert.ToDecimal(month) / 3).ToString();

                return year + quarter;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return string.Empty;
        }
    }
}
