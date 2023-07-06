using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using System.Drawing;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using System.Security.Cryptography;
using C1.Win.C1FlexGrid;
using DzHelpFormLib;

using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using Microsoft.Win32;
using System.Xml;
using System.Linq;

namespace Dintec
{
   
        
    


    public class Util
    {
        //static string companyCode;
        //static string bizCode;
        //static string language;
        //static string deptCode;
        //static string empNumber;
        public static void SaveReferenceFile(string fileNumber, string[] vendorCodes)
        {
            for (int i = 0; i < vendorCodes.Length; i++)
                vendorCodes[i] = "'" + vendorCodes[i] + "'";

            string query = @"
SELECT 
	CHK			= ISNULL(YN_REFERENCE, 'N')
,	FILE_TYPE	= 'W'
,	FILE_PATH	= 'WorkFlow/' + A.CD_COMPANY + '/' + B.YYYY + '/' + A.NO_FILE
,	FILE_NAME	= A.NM_FILE
,	FILE_DATA	= NULL
,	NO_FILE		= A.NO_FILE
,	NO_LINE		= A.NO_LINE
,	CD_PARTNER	= A.CD_VENDOR
,	DTS_INSERT	= A.DTS_INSERT
FROM V_CZ_MA_WORKFLOWL	AS A
JOIN
(
	SELECT
		CD_COMPANY
	,	NO_FILE
	,	YYYY
	,	ROW_NUMBER() OVER (PARTITION BY CD_COMPANY, NO_FILE ORDER BY TP_STEP)	AS RN
	FROM V_CZ_MA_WORKFLOWH
)						AS B  ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_FILE = B.NO_FILE AND B.RN = 1
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND A.NO_FILE = '" + fileNumber + @"'
	AND A.TP_STEP = '04'
	AND A.CD_VENDOR IN (" + string.Join(",", vendorCodes) + @")
	AND ISNULL(A.NM_FILE, '') != ''

UNION ALL

SELECT
	CHK			= ISNULL(YN_REFERENCE, 'N')
,	FILE_TYPE	= 'S'
,	FILE_PATH	= NULL
,	FILE_NAME	= FILE_NAME
,	FILE_DATA	= FILE_DATA
,	NO_FILE
,	NO_LINE		= SEQ
,	CD_PARTNER
,	DTS_INSERT
FROM CZ_SRM_QTNH_ATTACHMENT
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND NO_FILE = '" + fileNumber + @"'
	AND CD_PARTNER IN (" + string.Join(",", vendorCodes) + @")";

            H_CZ_FILE_LIST f = new H_CZ_FILE_LIST(SQL.GetDataTable(query)) { Checkable = true };

            // 견적발송용 저장
            if (f.ShowDialog() == DialogResult.OK)
            {
                SQL sql = new SQL("PX_CZ_PU_QTN_REFERENCE_R2", SQLType.Procedure);
                sql.Parameter.Add2("@XML", f.ChangedItem.ToXml("CHK", "FILE_TYPE", "NO_FILE", "NO_LINE", "CD_PARTNER"));
                sql.ExecuteNonQuery();
            }
        }

        public static void Certify(PageBase form)
        {
            //// 견적등록
            //string path = Path.Combine(Application.StartupPath, "P_CZ_SA_QTN_REG.dll");
            //System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //fi.Delete();


            // 견적등록
            string path = Path.Combine(@"C:\ERPU\Browser", "P_CZ_SA_QTN_REG.dll");
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            fi.Delete();


            // ********** 폰트 체크
            string fontName = "Agency FB";
            Font font = new Font(fontName, 12);

            if (font.Name != fontName)
            {
                bool added = false;

                // 파일 복사
                string fontFile1 = "AGENCYR.TTF";
                string fontFile2 = "AGENCYB.TTF";

                string pathFile1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", fontFile1);
                string pathFile2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", fontFile2);

                if (!File.Exists(pathFile1))
                {
                    added = true;
                    File.Copy(Application.StartupPath + @"\" + fontFile1, pathFile1);
                }

                if (!File.Exists(pathFile2))
                {
                    added = true;
                    File.Copy(Application.StartupPath + @"\" + fontFile2, pathFile2);
                }

                // 레지스트리 등록
                string regName1 = "Agency FB (TrueType)";
                string regName2 = "Agency FB Bold (TrueType)";

                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");

                if (key.GetValue(regName1) == null)
                {
                    added = true;
                    key.SetValue(regName1, fontFile1);
                }

                if (key.GetValue(regName2) == null)
                {
                    added = true;
                    key.SetValue(regName2, fontFile2);
                }

                key.Close();

                if (added)
                    ShowMessage("폰트설치가 완료되었습니다.\nERP 재시작 바랍니다!!");
            }

            string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
            string bizCode = Global.MainFrame.LoginInfo.BizAreaCode;
            string language = Global.MainFrame.LoginInfo.Language;
            string deptCode = Global.MainFrame.LoginInfo.DeptCode;
            string empNumber = Global.MainFrame.LoginInfo.UserID;


            // 클라우독 체크
            if (empNumber.Left(1).In("S", "D") && Process.GetProcessesByName("PlusDrive").Length < 1 && empNumber != "S-293" &&
                empNumber != "S-347" && empNumber != "S-343" && empNumber != "S-250" && empNumber != "S-305" &&
                empNumber != "S-231D" && empNumber != "S-267" && empNumber != "S-223" && empNumber != "S-391" && empNumber != "S-458" && empNumber != "S-373"
                && empNumber != "D-004A" && empNumber != "D-038" && empNumber != "S-332" && empNumber != "SYSADMIN" && empNumber != "S-576" && empNumber != "G-030")
                form.Dispose();

            // MAC Address 검증
            bool boMacOK = false;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;

                // esvpn인 경우 업링크 죽어버림..
                //if (nic.OperationalStatus == OperationalStatus.Up)
                //{
                string mac = nic.GetPhysicalAddress().ToString();

                // MAC 검증
                string query = @"
SELECT
	1
FROM MA_CODEDTL
WHERE 1 = 1
	--AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00014'
	--AND CD_SYSDEF = '" + Global.MainFrame.LoginInfo.UserID.Right(3) + @"'
	AND (  REPLACE(CD_FLAG1, '-', '') = '" + mac + @"'
		OR REPLACE(CD_FLAG2, '-', '') = '" + mac + @"'
		OR REPLACE(CD_FLAG3, '-', '') = '" + mac + "')";

                DataTable dt = DBMgr.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    boMacOK = true;
                    break;
                }
                //}
            }

            if (!boMacOK) form.Dispose();

            // 서버키 인증
            if (Global.MainFrame.ServerKey != "DINTEC" && Global.MainFrame.ServerKey != "DINTEC2")
            {
                form.Dispose();
            }
        }

        public static bool CertifyIP()
        {
            // 허용된 IP 불러오기 (코드관리에 있음)
            string query = @"
SELECT
	  SUBSTRING(IP, 1, CHARINDEX('/', IP) - 1)	AS IP
	, SUBSTRING(IP, CHARINDEX('/', IP) + 1, 2)	AS BIT
FROM
(
	SELECT
		SPLIT.A.value('.', 'VARCHAR(100)') AS IP
	FROM
	(
		SELECT
			CONVERT(XML, '<M>' + REPLACE(ISNULL(CD_FLAG1, ''), CHAR(13) + CHAR(10), '</M><M>') + '</M>') AS CD_FLAG_SPLIT
		FROM MA_CODEDTL
		where 1= 1
			--and CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
			AND CD_FIELD = 'CZ_MA00012'
			AND CD_SYSDEF = '001'
	) AS A CROSS APPLY CD_FLAG_SPLIT.nodes ('/M') AS SPLIT(A)
) AS A";

            DataTable dt = DBMgr.GetDataTable(query);

            // 로컬컴퓨터 IP
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    string ip = addr.ToString();

                    string bClass = ip.Substring(0, ip.IndexOf(".", ip.IndexOf(".") + 1));
                    string cClass = ip.Substring(0, ip.LastIndexOf("."));
                    string dClass = ip;

                    string filter = "(BIT = '24' AND IP LIKE '" + cClass + "%') OR (BIT = '32' AND IP = '" + dClass + "')";
                    DataRow[] row = dt.Select(filter);

                    if (row.Length > 0) return true;
                }
            }

            return false;
        }

        public static string[,] GetClipboardValues()
        {
            string[,] result;

            if (Clipboard.ContainsData("XML Spreadsheet"))
            {
                object clipData = Clipboard.GetData("XML Spreadsheet");
                MemoryStream ms = clipData as MemoryStream;
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                // Row 태그만 가져옴 (쓸데없는 것도 들어옴)
                XmlNode table = xml.GetElementsByTagName("Table")[0];
                int rowCount = GetTo.Int(table.Attributes["ss:ExpandedRowCount"].Value);
                int colCount = GetTo.Int(table.Attributes["ss:ExpandedColumnCount"].Value);
                result = new string[rowCount, colCount];

                // 부어넣기 시작
                int i = 0;

                foreach (XmlNode row in xml.GetElementsByTagName("Row"))
                {
                    //속성 인덱스 체크
                    if (row.Attributes["ss:Index"] != null)
                        i = GetTo.Int(row.Attributes["ss:Index"].Value) - 1;

                    int j = 0;

                    foreach (XmlNode cell in row.ChildNodes)
                    {
                        // 속성에 인덱스가 있는지 체크하여 있으면 j값을 치환함 (셀이 빈칸인 경우는 그 다음칸에 index가 있음)
                        if (cell.Attributes["ss:Index"] != null)
                            j = GetTo.Int(cell.Attributes["ss:Index"].Value) - 1;

                        //result[i, j] = cell.InnerText.Replace("\n", "\r\n");

                        // cell안에 또 루프 돌아야함, Data 태그 값만 가져와야함, cell에 메모가 있을 경우 같이 가져오는 불상사가 생겨버림
                        foreach (XmlNode node in cell.ChildNodes)
						{
                            if (node.Name == "Data")
                            {
                                result[i, j] = node.InnerText.Replace("\n", "\r\n");
                                break;
                            }
                        }

                        j++;
                    }

                    i++;
                }
            }
            else
            {
                string[] rows = Regex.Split(Clipboard.GetText().TrimEnd(), "\r\n");
                string[] columns = Regex.Split(rows[0], "\t");
                result = new string[rows.Length, columns.Length];

                // 부어넣기 시작
                for (int i = 0; i < rows.Length; i++)
                {
                    columns = Regex.Split(rows[i], "\t");

                    for (int j = 0; j < columns.Length; j++)
                        result[i, j] = columns[j];
                }
            }

            return result;
        }

        public static string GetTO_Xml(DataTable dataTable)
        {
            return GetTO_Xml(dataTable, null);
        }

        public static string GetTO_Xml(DataTable dataTable, string filter)
        {
            if (dataTable == null) return "<XML></XML>";

            DataTable dt = dataTable.Copy();
            DataTable dtD = new DataView(dt, filter, null, DataViewRowState.Deleted).ToTable();
            DataTable dtI = new DataView(dt, filter, null, DataViewRowState.Added).ToTable();
            DataTable dtU = new DataView(dt, filter, null, DataViewRowState.ModifiedCurrent).ToTable();
            DataTable dtO = new DataView(dt, filter, null, DataViewRowState.Unchanged).ToTable();

            dt.Columns.Add("XML_FLAG", typeof(string));
            dtD.Columns.Add("XML_FLAG", typeof(string), "'D'");
            dtI.Columns.Add("XML_FLAG", typeof(string), "'I'");
            dtU.Columns.Add("XML_FLAG", typeof(string), "'U'");
            dtO.Columns.Add("XML_FLAG", typeof(string), "'O'");

            dt.Clear();				// 초기화
            dt.TableName = "ROW";	// 테이블 이름 변경

            dt.Merge(dtD);
            dt.Merge(dtI);
            dt.Merge(dtU);
            dt.Merge(dtO);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return GetTO_Xml(ds);
        }

        public static string GetTO_Xml(DataTable dataTable, RowState rowState)
        {
            if (dataTable == null) return "<XML></XML>";

            DataTable dt = dataTable.Copy();

            string XML_FLAG = "";
            if (rowState == RowState.Deleted) XML_FLAG = "D";
            if (rowState == RowState.Added) XML_FLAG = "I";
            if (rowState == RowState.Modified) XML_FLAG = "U";
            if (rowState == RowState.Unchanged) XML_FLAG = "O";

            dt.TableName = "ROW";	// 테이블 이름 변경
            dt.Columns.Add("XML_FLAG", typeof(string), "'" + XML_FLAG + "'");

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return GetTO_Xml(ds);
        }

        public static string GetTO_Xml(DataSet ds)
        {
            string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
            string bizCode = Global.MainFrame.LoginInfo.BizAreaCode;
            string language = Global.MainFrame.LoginInfo.Language;
            string deptCode = Global.MainFrame.LoginInfo.DeptCode;
            string empNumber = Global.MainFrame.LoginInfo.UserID;

            // 컬럼 자동 추가
            foreach (DataTable dtRow in ds.Tables)
            {
                if (dtRow.Rows.Count > 0)
                {
                    if (!dtRow.Columns.Contains("CD_COMPANY")) dtRow.Columns.Add("CD_COMPANY", typeof(string), "'" + companyCode + "'");
                    if (!dtRow.Columns.Contains("CD_BIZAREA")) dtRow.Columns.Add("CD_BIZAREA", typeof(string), "'" + bizCode + "'");
                    if (!dtRow.Columns.Contains("CD_DEPT")) dtRow.Columns.Add("CD_DEPT", typeof(string), "'" + deptCode + "'");
                    if (!dtRow.Columns.Contains("NO_EMP")) dtRow.Columns.Add("NO_EMP", typeof(string), "'" + empNumber + "'");
                    if (!dtRow.Columns.Contains("ID_USER")) dtRow.Columns.Add("ID_USER", typeof(string), "'" + empNumber + "'");
                }
            }

            // XML 변환
            StringWriter sw = new StringWriter();
            ds.DataSetName = "XML";
            ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);
            return sw.ToString();
        }

        public static DataTable GetXmlTable(DataTable dt)
        {
            return GetXmlTable(dt, "");
        }

        public static DataTable GetXmlTable(DataTable dt, string tableName)
        {
            DataSet ds = new DataSet();

            // DataSet에 추가
            if (tableName == "")
            {
                ds.Tables.Add(new DataView(dt, null, null, DataViewRowState.Deleted).ToTable("D"));
                ds.Tables.Add(new DataView(dt, null, null, DataViewRowState.Added).ToTable("I"));
                ds.Tables.Add(new DataView(dt, null, null, DataViewRowState.ModifiedCurrent).ToTable("U"));
            }
            else
            {
                dt.TableName = tableName;
                ds.Tables.Add(dt.Copy());
            }

            DataTable dtXml = new DataTable();
            dtXml.Columns.Add("XML");
            dtXml.Rows.Add(GetTO_Xml(ds));

            return dtXml;
        }






        public static SpInfo SetSpInfo(DataTable dt, string procedure)
        {
            SpInfo si = new SpInfo();
            si.DataValue = GetXmlTable(dt);
            si.SpNameInsert = procedure;
            si.SpParamsInsert = new string[] { "XML" };

            return si;
        }

        public static SpInfo SetSpInfo(DataTable dt, string procedure, string tableName)
        {
            SpInfo si = new SpInfo();
            si.DataValue = GetXmlTable(dt, tableName);
            si.SpNameInsert = procedure;
            si.SpParamsInsert = new string[] { "XML" };

            return si;
        }


        public static string GetToday()
        {
            return GetToday(0);
        }

        public static string GetToday(int addedDay)
        {
            string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
            DateTime dt = Global.MainFrame.GetDateTimeToday().AddDays(addedDay);

            if (CD_COMPANY == "TEST") dt = dt.AddHours(0);

            return string.Format("{0:yyyyMMdd}", dt);
        }

        #region ==================================================================================================== 코드

        public static DataTable GetDB_CODE(string keyCode)
        {
            return GetDB_CODE(keyCode, false, Global.MainFrame.LoginInfo.CompanyCode);
        }

        public static DataTable GetDB_CODE(string keyCode, bool addEmptyLine)
        {
            return GetDB_CODE(keyCode, addEmptyLine, Global.MainFrame.LoginInfo.CompanyCode);
        }

        public static DataTable GetDB_CODE(string keyCode, bool addEmptyLine, string CD_COMPANY)
        {
            string language = Global.MainFrame.LoginInfo.Language;
            string query = "";

            if (keyCode == "GRP_ITEM")
            {
                string col = (language == "KR") ? "NM_ITEMGRP" : "EN_ITEMGRP";
                query += @"
SELECT
	  CD_ITEMGRP AS CODE
	, " + col + @" AS NAME
FROM MA_ITEMGRP
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND USE_YN = 'Y'";
            }
            else if (keyCode == "CD_WCODE")
            {
                query += @"
SELECT
	  CD_WCODE AS CODE
	, NM_WCODE AS NAME
	, DY_WOCCUR
FROM HR_WCODE
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_WTYPE = '001'
	AND YN_PROPOSAL = 'Y' 
	AND YN_USE = 'Y'";
            }
            else if (keyCode == "TP_FILE")
            {
                query += @"
SELECT
	  CD_FLAG3 AS CODE
	, CD_FLAG3 AS NAME
FROM MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND ISNULL(CD_FLAG3, '') != ''
	AND USE_YN = 'Y'";
            }
            else
            {
                string col = (language == "KR") ? "NM_SYSDEF" : "ISNULL(NM_SYSDEF_E, NM_SYSDEF)";
                query += @"
SELECT
	  CD_SYSDEF AS CODE
	, " + col + @" AS NAME
	, CD_FLAG1
	, CD_FLAG2
	, CD_FLAG3
FROM MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_FIELD = '" + keyCode + @"'
	AND USE_YN = 'Y'";
            }

            DataTable dt = DBHelper.GetDataTable(query);
            if (addEmptyLine)
            {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["CODE"] = "";
            }

            return dt;
        }

        public static DataSet GetDB_CODE(string[] keyCode)
        {
            return GetDB_CODE(keyCode, Global.MainFrame.LoginInfo.CompanyCode);
        }

        public static DataSet GetDB_CODE(string[] keyCode, string CD_COMPANY)
        {
            string language = Global.MainFrame.LoginInfo.Language;
            string query = "";

            foreach (string s in keyCode)
            {
                if (s == "GRP_ITEM")
                {
                    string col = (language == "KR") ? "NM_ITEMGRP" : "EN_ITEMGRP";
                    query += @"
SELECT
	  CD_ITEMGRP AS CODE
	, " + col + @" AS NAME
FROM MA_ITEMGRP
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND USE_YN = 'Y'";
                }
                else if (s == "CD_WCODE")
                {
                    query += @"
SELECT
	  CD_WCODE AS CODE
	, NM_WCODE AS NAME
	, DY_WOCCUR
FROM HR_WCODE
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_WTYPE = '001'
	AND YN_PROPOSAL = 'Y' 
	AND YN_USE = 'Y'";
                }
                else if (s == "SALES_CLASS")
                {
                    query += @"
SELECT
	  CD_FLAG3 AS CODE
	, CD_FLAG3 AS NAME
FROM MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND ISNULL(CD_FLAG3, '') != ''
	AND USE_YN = 'Y'";
                }
                else
                {
                    string col = (language == "KR") ? "NM_SYSDEF" : "ISNULL(NM_SYSDEF_E, NM_SYSDEF)";
                    query += @"
SELECT
	  CD_SYSDEF AS CODE
	, " + col + @" AS NAME
	, CD_FLAG1
	, CD_FLAG2
	, CD_FLAG3
FROM MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_FIELD = '" + s + @"'
	AND USE_YN = 'Y'";
                }
            }

            DataSet ds = DBHelper.GetDataSet(query);

            return ds;
        }

        public static void SetDB_CODE(DropDownComboBox comboBox, string keyCode, bool addEmptyLine)
        {
            DataTable dt = GetDB_CODE(keyCode, addEmptyLine, Global.MainFrame.LoginInfo.CompanyCode);

            comboBox.ValueMember = "CODE";
            comboBox.DisplayMember = "NAME";
            comboBox.DataSource = dt;
        }

        public static void SetDB_CODE(DropDownComboBox comboBox, DataTable dataTable, bool addEmptyLine)
        {
            if (addEmptyLine)
            {
                dataTable.Rows.InsertAt(dataTable.NewRow(), 0);
                dataTable.Rows[0]["CODE"] = DBNull.Value;
                dataTable.Rows[0]["NAME"] = DBNull.Value;
            }

            comboBox.ValueMember = "CODE";
            comboBox.DisplayMember = "NAME";
            comboBox.DataSource = dataTable;
        }

        public static DataTable GetDB_EXTRA_COST()
        {
            string query = @"
SELECT
	  CD_ITEM AS CODE
	, NM_ITEM AS NAME
	, UNIT_IM AS UNIT
FROM MA_PITEM
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_ITEM LIKE 'SD%'";

            return DBMgr.GetDataTable(query);
        }

        public static DataTable GetDB_SALEGRP(string CD_COMPANY, string NO_EMP)
        {
            string query = ""
                + "\n" + "SELECT"
                + "\n" + "	*"
                + "\n" + "FROM MA_USER		AS A"
                + "\n" + "JOIN MA_SALEGRP	AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_SALEGRP = B.CD_SALEGRP"
                + "\n" + "WHERE 1 = 1"
                + "\n" + "	AND A.CD_COMPANY = '" + CD_COMPANY + "'"
                + "\n" + "	AND A.NO_EMP = '" + NO_EMP + "'";

            return DBHelper.GetDataTable(query);
        }

        public static DataTable GetDB_SALEPURGRP(string NO_EMP)
        {
            string query = @"
SELECT
	  B.CD_SALEGRP
	, B.NM_SALEGRP
	, C.CD_PURGRP
	, C.NM_PURGRP
FROM MA_USER			AS A WITH(NOLOCK)
LEFT JOIN MA_SALEGRP	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_SALEGRP = B.CD_SALEGRP
LEFT JOIN MA_PURGRP		AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_PURGRP = C.CD_PURGRP	
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND A.NO_EMP = '" + NO_EMP + "'";

            return DBHelper.GetDataTable(query);
        }

        public static string GetDB_EMP_PW(string ID_USER)
        {
            string query = @"
SELECT
	*
FROM MA_USER
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND ID_USER = '" + ID_USER + "'";

            DataTable dt = DBMgr.GetDataTable(query);
            string pw = dt.Rows[0]["PASS_WORD"].ToString();
            string key = Global.MainFrame.LoginInfo.CompanyCode + ID_USER;

            return DecryptString(pw, key);
        }

        public static string DecryptString(string data, string key)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            byte[] buffer = Convert.FromBase64String(data);
            byte[] bytes = Encoding.ASCII.GetBytes(key.Length.ToString());
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(key, bytes);
            ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(buffer);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] numArray = new byte[buffer.Length];
            int count = cryptoStream.Read(numArray, 0, numArray.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.Unicode.GetString(numArray, 0, count);
        }

        public static DataTable GetDB_PARTNER(string CD_COMPANY, string CD_PARTNER)
        {
            string query = @"
SELECT
	  A.*
	, B.CD_EXCH1
	, B.CD_EXCH2
	, S.TP_SO
	, S.NM_SO
	, B.TP_VAT
	, B.FG_BILL1
	, B.FG_BILL2
	, P.CD_TPPO
	, P.NM_TPPO	
	, B.FG_PAYMENT
	, B.FG_TAX
FROM	  MA_PARTNER			AS A
LEFT JOIN CZ_MA_PARTNER	        AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN SA_TPSO				AS S ON B.CD_COMPANY = S.CD_COMPANY AND B.TP_SO = S.TP_SO
LEFT JOIN PU_TPPO				AS P ON B.CD_COMPANY = P.CD_COMPANY AND B.CD_TPPO = P.CD_TPPO
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + "'";

            if (CD_PARTNER.IndexOf(")") < 0) query += "\n" + "	AND A.CD_PARTNER = '" + CD_PARTNER + "'";
            if (CD_PARTNER.IndexOf(")") > 0) query += "\n" + "	AND A.CD_PARTNER IN " + CD_PARTNER;

            return DBHelper.GetDataTable(query);
        }


        public static string[] GetDB_WORKFLOW_EMP(string NO_FILE)
        {
            string query = @"
SELECT
	*
FROM
(
	SELECT ID_SALES	AS ID_USER FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = 'K100' AND NO_KEY = '" + NO_FILE + @"'
	UNION
	SELECT ID_PUR	AS ID_USER FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = 'K100' AND NO_KEY = '" + NO_FILE + @"'
	UNION
	SELECT ID_LOG	AS ID_USER FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = 'K100' AND NO_KEY = '" + NO_FILE + @"'
)	AS A
WHERE ID_USER IS NOT NULL";

            DataTable dt = DBMgr.GetDataTable(query);
            string[] users = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++) users[i] = dt.Rows[i]["ID_USER"].ToString();

            return users;
        }

        public static object GetDB_CD_FLAG1(DropDownComboBox cbo)
        {
            return ((DataTable)cbo.DataSource).Rows[cbo.SelectedIndex]["CD_FLAG1"];
        }

        // ========== 환율
        public static decimal GetDB_EXCHANGE(object YYMMDD, object CURR_SOUR, string MODE)
        {
            return GetDB_EXCHANGE(YYMMDD, CURR_SOUR, MODE, 2, Global.MainFrame.LoginInfo.CompanyCode);
        }

        public static decimal GetDB_EXCHANGE(object YYMMDD, object CURR_SOUR, string MODE, int NO_SEQ, string CD_COMPANY)
        {
            DataTable dt = DBMgr.GetDataTable("PS_CZ_MA_EXCHANGE", new object[] { CD_COMPANY, YYMMDD, NO_SEQ, CURR_SOUR });

            if (MODE == "P") MODE = "RATE_PURCHASE";
            if (MODE == "S") MODE = "RATE_SALES";

            if (dt.Rows.Count == 1) return Util.GetTO_Decimal(dt.Rows[0][MODE]);
            return 1;
        }

        #endregion

        #region ==================================================================================================== 수식

        public static int GetTO_Int(object value)
        {
            if (value == null || value.ToString() == "") return 0;
            return Convert.ToInt32(value);
        }

        public static DateTime GetTO_Date(object value)
        {
            // 8글자를 변환
            string s = value.ToString();
            if (s.Length == 8) s = s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);
            return Convert.ToDateTime(s);
        }

        public static string GetTo_DateString(object value)
        {
            if (value.ToString() == "") return "";
            return Convert.ToDateTime(value).ToShortDateString().Replace("-", "");
        }

        public static string GetTo_DateStringS(object value)
        {
            if (value == null || value.ToString() == "" || value.ToString().Length != 8) return "";
            return value.ToString().Substring(0, 4) + "/" + value.ToString().Substring(4, 2) + "/" + value.ToString().Substring(6, 2);
        }

        public static string GetToDatePrint(DateTime value)
        {
            string s = string.Format("{0:yyyyMMdd}", value);
            return GetToDatePrint(s);
        }

        public static string GetToDatePrint(object value)
        {
            if (value.ToString() == "") return "";
            string s = value.ToString();

            string yyyy = s.Substring(0, 4);
            string mm = s.Substring(4, 2);
            string dd = s.Substring(6, 2);

            if (mm == "01") mm = "JAN";
            if (mm == "02") mm = "FEB";
            if (mm == "03") mm = "MAR";
            if (mm == "04") mm = "APR";
            if (mm == "05") mm = "MAY";
            if (mm == "06") mm = "JUN";
            if (mm == "07") mm = "JUL";
            if (mm == "08") mm = "AUG";
            if (mm == "09") mm = "SEP";
            if (mm == "10") mm = "OCT";
            if (mm == "11") mm = "NOV";
            if (mm == "12") mm = "DEC";

            return dd + "/" + mm + "/" + yyyy;
        }

        public static decimal GetTO_Decimal(object value)
        {
            if (value.ToString() == "") return 0;
            return Convert.ToDecimal(value);
        }

        public static string GetTO_String(object value)
        {
            if (value == null) return "";
            return value.ToString();
        }

        public static string GetTO_Money(object value)
        {
            if (value == null) return "";
            return string.Format("{0:#,##0.##}", value);
        }

        public static DataTable GetTO_DataTable(DataRow[] rows)
        {
            if (rows.Length == 0) return null;

            return rows.CopyToDataTable();
        }




        public static decimal Round(decimal value)
        {
            return Math.Round(value, MidpointRounding.AwayFromZero);
        }

        public static decimal Round(decimal value, int pos)
        {
            value = value * (decimal)Math.Pow(10, pos);
            value = Math.Round(value, MidpointRounding.AwayFromZero);
            value = value * (decimal)Math.Pow(10, pos * -1);

            return value;
        }

        public static decimal Ceiling(decimal value)
        {
            return Math.Ceiling(value);
        }

        public static decimal Ceiling(decimal value, int pos)
        {
            value = value * (decimal)Math.Pow(10, pos);
            value = Math.Ceiling(value);
            value = value * (decimal)Math.Pow(10, pos * -1);

            return value;
        }

        public static decimal Truncate(decimal value)
        {
            return Math.Truncate(value);
        }

        public static decimal Truncate(decimal value, int pos)
        {
            value = value * (decimal)Math.Pow(10, pos);
            value = Math.Truncate(value);
            value = value * (decimal)Math.Pow(10, pos * -1);

            return value;
        }

        #endregion





        public static void Clear(Control control)
        {
            Clear(control, false);
        }

        public static void Clear(Control control, bool bDefault)
        {
            if (control is TextBoxExt)
            {
                ((TextBoxExt)control).Text = "";
            }
            else if (control is BpCodeTextBox)
            {
                if (bDefault)
                {
                    if (((BpCodeTextBox)control).Tag.ToString() == "NO_EMP;NM_EMP")			// 담당자
                    {
                        ((BpCodeTextBox)control).CodeValue = Global.MainFrame.LoginInfo.UserID;
                        ((BpCodeTextBox)control).CodeName = Global.MainFrame.LoginInfo.UserName;
                    }
                    if (((BpCodeTextBox)control).Tag.ToString() == "CD_SALEGRP;NM_SALEGRP")	// 영업그룹
                    {
                        ((BpCodeTextBox)control).CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
                        ((BpCodeTextBox)control).CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
                    }
                    if (((BpCodeTextBox)control).Tag.ToString() == "CD_PURGRP;NM_PURGRP")	// 구매그룹
                    {
                        ((BpCodeTextBox)control).CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                        ((BpCodeTextBox)control).CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
                    }
                }
                else
                {
                    ((BpCodeTextBox)control).CodeValue = "";
                    ((BpCodeTextBox)control).CodeName = "";
                }
            }
            else if (control is DatePicker)
            {
                if (bDefault) ((DatePicker)control).Text = GetToday();
                else ((DatePicker)control).Text = "";
            }
            else if (control is DropDownComboBox)
            {
                if ((DataTable)((DropDownComboBox)control).DataSource != null) ((DataTable)((DropDownComboBox)control).DataSource).Clear();
            }
            else if (control is FlexGrid)
            {
                if (((FlexGrid)control).DataSource != null)
                {
                    ((FlexGrid)control).DataTable.Rows.Clear();
                    ((FlexGrid)control).AcceptChanges();
                }
            }
        }

        public static void SetCON_ReadOnly(BpPanelControl pnl, bool value)
        {
            foreach (Control c in pnl.Controls)
            {
                if (c is TextBoxExt)
                {
                    ((TextBoxExt)c).ReadOnly = value;
                }
                else if (c is BpCodeTextBox)
                {
                    ((BpCodeTextBox)c).ReadOnly = value ? ReadOnly.TotalReadOnly : ReadOnly.None;
                }
                else if (c is CurrencyTextBox)
                {
                    ((CurrencyTextBox)c).ReadOnly = value;
                }
                else c.Enabled = !value;
            }
        }

        public static bool CheckPW()
        {
            if (Global.MainFrame.LoginInfo.UserID == "S-343")
                return true;

            H_CZ_CHECK_PW help = new H_CZ_CHECK_PW();
            if (help.ShowDialog() != DialogResult.OK) return false;

            return true;
        }

        public static string GetErrorMessage(string msg)
        {
            if (msg.IndexOf("DINTEC ERROR : ") >= 0)
            {
                int start = msg.IndexOf("DINTEC ERROR : ");
                int end;

                if (msg.IndexOf("ERROR_MSG_END") > 0) end = msg.IndexOf("ERROR_MSG_END");
                else if (msg.IndexOf("\n") > 0) end = msg.IndexOf("\n");
                else end = 0;

                int length = (end > 0) ? end - start : msg.Length - start;

                return msg.Substring(start, length);
            }

            return msg;
        }

        public static bool ShowMessage(string msg)
        {
            Global.MainFrame.ShowMessage(msg);
            return false;
        }


        public static void SetGridOddRow(FlexGrid flex, int rowIndex)
        {
            if (!flex.Styles.Contains("ODD_ROW"))
            {
                CellStyle style = flex.Styles.Add("ODD_ROW");
                style.BackColor = Color.FromArgb(241, 241, 241);
            }

            flex.Rows[rowIndex].Style = flex.Styles["ODD_ROW"];
        }

        public static void SetGRID_Edit(FlexGrid flex, string columnName, bool editable)
        {
            if (!flex.Styles.Contains("EDIT_HEADER"))
            {
                CellStyle style = flex.Styles.Add("EDIT_HEADER");
                style.Font = new Font(flex.Font, FontStyle.Bold);
                style.ForeColor = Color.Blue;
            }

            flex.Cols[columnName].AllowEditing = editable;
            flex.SetCellStyle(0, flex.Cols[columnName].Index, (editable) ? "EDIT_HEADER" : "");
        }


        public static void SetGRID_Highlight(FlexGrid flex, int rowIndex)
        {
            if (!flex.Styles.Contains("ROW_HIGHLIGHT"))
            {
                CellStyle style = flex.Styles.Add("ROW_HIGHLIGHT");
                style.BackColor = Color.Yellow;
            }

            flex.Rows[rowIndex].Style = flex.Styles["ROW_HIGHLIGHT"];
        }

        public static void CalculateRate(DataTable dataTable, string filter)
        {
            string[] cols = new string[] { "RT_DC_P", "RT_PROFIT", "RT_DC", "RT_MARGIN" };

            foreach (string s in cols)
            {
                if (!dataTable.Columns.Contains(s))
                    dataTable.Columns.Add(s, typeof(decimal));
            }

            foreach (DataRow dr in dataTable.Select(filter))
            {
                if (dr["UM_KR_E"] != DBNull.Value && dr["UM_KR_P"] != DBNull.Value)
                    dr["RT_DC_P"] = CalcDiscountRate(GetTo.Decimal(dr["UM_KR_E"]), GetTo.Decimal(dr["UM_KR_P"]));

                if (dr["UM_KR_P"] != DBNull.Value && dr["UM_KR_Q"] != DBNull.Value)
                    dr["RT_PROFIT"] = CalcProfitRate(GetTo.Decimal(dr["UM_KR_P"]), GetTo.Decimal(dr["UM_KR_Q"]));

                if (dr["UM_KR_Q"] != DBNull.Value && dr["UM_KR_S"] != DBNull.Value)
                    dr["RT_DC"] = CalcDiscountRate(GetTo.Decimal(dr["UM_KR_Q"]), GetTo.Decimal(dr["UM_KR_S"]));

                if (dr["UM_KR_P"] != DBNull.Value && dr["UM_KR_S"] != DBNull.Value)
                    dr["RT_MARGIN"] = CalcProfitRate(GetTo.Decimal(dr["UM_KR_P"]), GetTo.Decimal(dr["UM_KR_S"]));
            }
        }

        ////////////////////////////////////////
        public static decimal CalcProfitRate(decimal purchase, decimal sale)
        {
            return Calculator.이윤율계산(purchase, sale);
        }

        public static decimal CalcDiscountRate(decimal before, decimal after)
        {
            return Calculator.할인율계산(before, after);
        }


        ////////////////////////////////////////
        public static void SaveInquiryDx(string companyCode, string fileNumber)
        {
            string query = "SELECT * FROM CZ_SA_QTNL WHERE CD_COMPANY = '" + companyCode + "' AND NO_FILE = '" + fileNumber + "'";
            DataTable dt = SQL.GetDataTable(query);
            SaveInquiryDx(dt);
        }

        public static void SaveInquiryDx(DataTable itemTable)
        {
            if (itemTable == null)
                return;

            // 컬럼 및 데이터 생성
            if (!itemTable.Columns.Contains("DXDESC"))
                itemTable.Columns.Add("DXDESC");

            if (!itemTable.Columns.Contains("DXCODE_SUBJ"))
                itemTable.Columns.Add("DXCODE_SUBJ");

            if (!itemTable.Columns.Contains("DXCODE_ITEM"))
                itemTable.Columns.Add("DXCODE_ITEM");

            if (!itemTable.Columns.Contains("DXDICT_TRAIN"))
                itemTable.Columns.Add("DXDICT_TRAIN");

            foreach (DataRow row in itemTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    row["DXDESC"] = Util.GetDxDesc(row["NM_SUBJECT"] + " " + row["CD_ITEM_PARTNER"] + " " + row["NM_ITEM_PARTNER"]);
                    row["DXCODE_SUBJ"] = Util.GetDxCode(row["NM_SUBJECT"].ToString());
                    row["DXCODE_ITEM"] = Util.GetDxCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
                    row["DXDICT_TRAIN"] = Util.GetDxDictByTrain(row["NM_SUBJECT"] + " ‡ " + row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
                }
            }

            string lineXml = itemTable.ToXml("CD_COMPANY", "NO_FILE", "NO_LINE", "DXDESC", "DXCODE_SUBJ", "DXCODE_ITEM", "DXDICT_TRAIN");
            //string lineXml = itemTable.ToXml();
            DBMgr.GetDataTable("PX_CZ_SA_QTNL_DX", DebugMode.Popup, lineXml);
        }

        public static string GetDxDescriptionBySimple(string inString)
        {
            inString = Regex.Replace(inString, @"\s+", " ");
            inString = inString.Replace(" ", "   ");
            return "   " + inString + "   ";
        }

        public static string GetDxDesc(string str)
        {
            return " " + Regex.Replace(str, @"\s+", " ").Trim().ToUpper() + " ";
        }

        public static string GetDxCode(string str)
        {
            // 확실히 단어 사이에 구분져야 할 것.. 슬래쉬 요곤 좀 애매함.. 일단 고민해보자.. (구분져야 하는 CASE : DB18017984/12번)
            foreach (string f in new string[] { "\"", "|", "§", "'", ",", ".", ":", ";", "(", ")", "[", "]", "<", ">", "&", "/", "\r", "\n" })
                str = str.Replace(f, " ‡ ");

            // 주의단어
            str = str.Replace("NO-", "NO ");

            // 한 단어로 붙일 것
            str = str.Replace("-", "");
            str = str.Replace("_", "");

            // 여러 공백을 단일 공백으로 변경 및 루프 돌리기 위해 앞뒤 ‡ 추가
            str = Regex.Replace(str, @"\s+", " ");
            str = "‡ " + str + " ‡";

            // ********** 각 단어를 분리시켜 분석 시작			
            Regex numeric = new Regex("[0-9]");
            Regex alphabet = new Regex("[A-Z]");

            //independent
            List<string> independentWord = new List<string>();
            string[] word = str.Split(' ');

            for (int i = 0; i < word.Length; i++)
            {
                // 전단어, 이후단어 세팅
                string prevStr = "";
                string nextStr = "";

                if (i > 0)
                    prevStr = word[i - 1];

                if (i < word.Length - 1)
                    nextStr = word[i + 1];

                // 숫자가 하나도 없는 단어가 5글자 이상이면 무조건 탈락
                if (!numeric.IsMatch(word[i]) && word[i].Length >= 5)
                {
                    word[i] = "‡";
                }
                // 영숫자 결합에 5글자 이상이면 PICK도 하고 단독 단어로 인정도 하고
                else if (numeric.IsMatch(word[i]) && alphabet.IsMatch(word[i]) && word[i].Length >= 5)
                {
                    independentWord.Add(word[i]);
                }
                // 자신 또는 앞뒤단어에 숫자 포함되어 있으면 Pick
                else if (numeric.IsMatch(prevStr) || numeric.IsMatch(word[i]) || numeric.IsMatch(nextStr))
                {
                    // CODE 060-3117 이런 케이스는 CODE0603117 되버리므로 끝에다가 별도 단독단어를 덛붙임
                    if (numeric.IsMatch(word[i]) && word[i].Length >= 5)
                        independentWord.Add(word[i]);
                }
                // 나머지는 탈락
                else
                {
                    word[i] = "‡";
                }
            }

            // 줄어든 문장 완성
            string workedString = string.Join("", word);
            workedString = workedString.Replace("‡", " ");
            workedString = Regex.Replace(workedString, @"\s+", " ");

            // 마지막 과정 : 5글자 이상 되는 단어만 PICK
            string outString = "";

            foreach (string s in workedString.Split(' '))
            {
                if (s.Length >= 5)
                {
                    outString += " " + s;
                    independentWord.Remove(s);  // 독립단어 중 중복단어 제거
                }
            }

            outString += " " + string.Join(" ", independentWord);
            outString = " " + outString.Trim().ToUpper() + " ";

            return outString.ToUpper();
        }

        public static string GetDxDescriptionByCoreCode(string inString)
        {
            // 확실히 단어 사이에 구분져야 할 것.. 슬래쉬 요곤 좀 애매함.. 일단 고민해보자.. (구분져야 하는 CASE : DB18017984/12번)
            foreach (string f in new string[] { "\"", "|", "§", "'", ",", ".", ":", ";", "(", ")", "[", "]", "<", ">", "&", "/", "\r", "\n" })
                inString = inString.Replace(f, " ‡ ");

            // 주의단어
            inString = inString.Replace("NO-", "NO ");

            // 한 단어로 붙일 것
            inString = inString.Replace("-", "");
            inString = inString.Replace("_", "");

            // 여러 공백을 단일 공백으로 변경 및 루프 돌리기 위해 앞뒤 ‡ 추가
            inString = Regex.Replace(inString, @"\s+", " ");
            inString = "‡ " + inString + " ‡";

            // ********** 각 단어를 분리시켜 분석 시작			
            Regex numeric = new Regex("[0-9]");
            Regex alphabet = new Regex("[A-Z]");

            //independent
            List<string> independentWord = new List<string>();
            string[] word = inString.Split(' ');

            for (int i = 0; i < word.Length; i++)
            {
                // 전단어, 이후단어 세팅
                string prevStr = "";
                string nextStr = "";

                if (i > 0)
                    prevStr = word[i - 1];

                if (i < word.Length - 1)
                    nextStr = word[i + 1];

                // 숫자가 하나도 없는 단어가 5글자 이상이면 무조건 탈락
                if (!numeric.IsMatch(word[i]) && word[i].Length >= 5)
                {
                    word[i] = "‡";
                }
                // 영숫자 결합에 5글자 이상이면 PICK도 하고 단독 단어로 인정도 하고
                else if (numeric.IsMatch(word[i]) && alphabet.IsMatch(word[i]) && word[i].Length >= 5)
                {
                    independentWord.Add(word[i]);
                }
                // 자신 또는 앞뒤단어에 숫자 포함되어 있으면 Pick
                else if (numeric.IsMatch(prevStr) || numeric.IsMatch(word[i]) || numeric.IsMatch(nextStr))
                {
                    // CODE 060-3117 이런 케이스는 CODE0603117 되버리므로 끝에다가 별도 단독단어를 덛붙임
                    if (numeric.IsMatch(word[i]) && word[i].Length >= 5)
                        independentWord.Add(word[i]);
                }
                // 나머지는 탈락
                else
                {
                    word[i] = "‡";
                }
            }

            // 줄어든 문장 완성
            string workedString = string.Join("", word);
            workedString = workedString.Replace("‡", " ");
            workedString = Regex.Replace(workedString, @"\s+", " ");

            // 마지막 과정 : 5글자 이상 되는 단어만 PICK
            string outString = "";

            foreach (string s in workedString.Split(' '))
            {
                if (s.Length >= 5)
                {
                    outString += " " + s;
                    independentWord.Remove(s);  // 독립단어 중 중복단어 제거
                }
            }

            outString += " " + string.Join(" ", independentWord);
            outString = " " + outString.Trim().ToUpper() + " ";

            return outString;
        }

        public static string GetDxDictByTrain(string inString)
        {
            // 선두 단어 가져오기
            string query = @"
SELECT
	NM_SYSDEF					AS HEAD
,	ISNULL(CD_FLAG1, '')		AS HEAD2
,	'‡' + ISNULL(CD_FLAG2, '')
+	'‡' + ISNULL(CD_FLAG3, '')
+	'‡' + ISNULL(CD_FLAG4, '')
+	'‡' + ISNULL(CD_FLAG5, '')	AS IGNORED
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = 'K100'
	AND CD_FIELD = 'CZ_DX00005'";

            DataTable dtDict = DBMgr.GetDataTable(query);

            string outString = "";
            string replaceStr = Regex.Replace(inString, "[^a-zA-Z0-9-/‡]+", " ");   // 삭제하지 말아야 할 단어들
            string[] words = replaceStr.Split(' ');

            for (int i = 0; i < words.Length - 1; i++)
            {
                // 찾았으면 다음 인덱스로 옮겨서 append 시작
                if (FindRowIndex(dtDict, words[i]) is int dictIdx && dictIdx >= 0)
                {
                    // 2번 헤드가 있는지 검색, 있다면 그 다음 단어와 일치할때만 진행
                    if ((string)dtDict.Rows[dictIdx]["HEAD2"] is string head2Str && head2Str != "")
                    {
                        if (head2Str.IndexOf(words[i + 1]) >= 0)
                            i++;
                        else
                            continue;
                    }

                    // i로부터 3칸까지 검색
                    int endIdx = i + 3;
                    string dropStr = (string)dtDict.Rows[dictIdx]["IGNORED"];

                    // 다음 인덱스로 옮긴다음 지정된 수만큼(endIdx) 단어 모으기 시작
                    for (i++; i <= endIdx && i < words.Length; i++)
                    {
                        // 사전에 있는 단어를 만난다면 다시 상위 루프부터 시작
                        if (FindRowIndex(dtDict, words[i]) >= 0)
                            break;
                        else if (words[i] == "‡")                   // ‡ 는 서브젝트 아이템 구분자 이므로 탐색 중단함
                            i = endIdx;
                        else if (dropStr.IndexOf(words[i]) >= 0)    // 제외 키워드에 있는 단어인지 검색하여 있으면 현재꺼는 제외 하고 한칸 더 +함
                            endIdx++;
                        else
                            outString += " " + words[i];
                    }

                    // 바로 윗 루프에서 이미 i++ 되어 있으므로 이중증가를 막기위해 한번 줄여줌
                    i--;
                }
            }

            return outString.Trim().ToUpper();
        }

        private static int FindRowIndex(DataTable dtDict, string findStr)
        {
            DataRow[] rows = dtDict.Select("HEAD = '" + findStr + "'");

            if (rows.Length > 0)
            {
                return dtDict.Rows.IndexOf(rows[0]);
            }
            else
                return -1;
        }


        public static void ShowProgress(string msg)
        {
            MsgControl.ShowMsg(msg);
            DBHelper.ExecuteNonQuery("", new object[] { });	// 여기에 즉시 띄우는 뭐가 있나봄
        }

        public static void CloseProgress()
        {
            MsgControl.CloseMsg();
        }

        public static string GetReportFileName(string name, string companyCode)
        {
            if (companyCode == "TEST")
                return name;
            else
                return name + "_" + companyCode;
        }

        public static ReportHelper SetReportHelper(string reportCode, string reportName, string companyCode)
        {
            ReportHelper report = new ReportHelper(reportCode, reportName);

            report.PrintHelper.SetInit(PrintDialogOptionType.NONE, 0);

            string query = "SELECT ID_OBJECT," + Environment.NewLine +
                        " 	    NM_OBJECT," + Environment.NewLine +
                        " 	    NM_OBJECT_E," + Environment.NewLine +
                        " 	    TP_REPORT" + Environment.NewLine +
                        "FROM MA_REPORTL" + Environment.NewLine +
                        "WHERE CD_COMPANY = '" + companyCode + "'" + Environment.NewLine +
                        "AND YN_CHOOSE = 'Y'" + Environment.NewLine +
                        "AND CD_SYSTEM = '" + reportCode + "'";

            string fileName = "";

            foreach (DataRow dr in DBHelper.GetDataTable(query).Rows)
            {
                if (D.GetDecimal(dr["TP_REPORT"]) == 0)
                    fileName = D.GetString(dr["ID_OBJECT"]) + ".RDF";
                else
                    fileName = D.GetString(dr["ID_OBJECT"]) + ".DRF";

                if (Global.MainFrame.LoginInfo.Language == "KR")
                    report.PrintHelper.AddContentsString(D.GetString(dr["NM_OBJECT"]), fileName);
                else
                    report.PrintHelper.AddContentsString(D.GetString(dr["NM_OBJECT_E"]), fileName);
            }

            return report;
        }

        public static ReportHelper SetRPT(string reportCode, string reportName, string companyCode, DataTable dtH, DataTable dtL)
        {
            reportCode = GetReportFileName(reportCode, companyCode);

            string query;
            ReportHelper report = SetReportHelper(reportCode, reportName, companyCode);

            // H 기본 파라미터 추가
            string[] parameter = {
                  "NM_COMPANY"          , "DC_ADS_COMPANY"  , "NO_TEL_COMPANY"  , "NO_FAX_COMPANY"  , "NM_EMAIL_COMPANY", "NM_CEO"	// 회사 정보
				, "DC_ADS_LOG"          , "NO_TEL_LOG"      , "NO_FAX_LOG"															// 물류센터 정보
				, "NM_EMP"              , "NO_TEL_EMP"      , "NO_TEL_EMER_EMP" , "NO_FAX_EMP"      , "NO_EMAIL_EMP"    , "SIGNATURE"		// 담당자 정보
				, "NM_EMP_DEPT"         , "SIGNATURE_DEPT"																					// 부서장 정보
				, "LN_PARTNER"          , "DC_ADS_H_PARTNER", "DC_ADS_D_PARTNER", "NO_TEL_PARTNER"  , "NO_FAX_PARTNER"  , "E_MAIL_PARTNER", "NM_PIC"	// 거래처 정보1
				, "NO_COMPANY_PARTNER"  , "NM_CEO_PARTNER"  , "TP_JOB_PARTNER"  , "CLS_JOB_PARTNER" , "CD_NATION"					// 거래처 정보2
				, "NO_IMO"              , "NO_HULL"         , "NM_VESSEL"       , "NM_SHIP_YARD"	// 호선 정보
				, "NO_FILE"             , "NO_SO"           , "NO_PO"           , "NO_PO_PARTNER"	// 파일 정보
				, "NO_REF"              , "NO_ORDER"							// REF 정보		
				, "NM_EXCH"             , "RT_EXCH"								// 환율 정보
				, "DT_INQ"              , "DT_QTN"          , "DT_SO"   , "DT_PO"	// 날짜 정보
				, "DC_RMK"              , "DC_RMK_INQ"      , "DC_RMK_QTN"      , "DC_RMK_TEXT" };      // 비고

            foreach (string s in parameter)
            {
                if (dtH.Columns.Contains(s))
                {
                    if (s.Substring(0, 3) == "DT_")
                    {
                        report.SetData(s, GetToDatePrint(dtH.Rows[0][s]));
                    }
                    else if (s == "NM_EMP")
                    {
                        // 한국 or 지역코드 없음
                        if (!dtH.Columns.Contains("CD_AREA") || dtH.Rows[0]["CD_AREA"].ToString() == "100")
                        {
                            report.SetData(s, dtH.Rows[0][s].ToString());
                        }
                        // 해외
                        else
                        {
                            report.SetData(s, dtH.Rows[0]["EN_EMP"].ToString());
                        }
                    }
                    else if (s == "NM_EMP_DEPT")
                    {
                        // 한국 or 지역코드 없음
                        if (!dtH.Columns.Contains("CD_AREA") || dtH.Rows[0]["CD_AREA"].ToString() == "100")
                        {
                            report.SetData(s, dtH.Rows[0][s].ToString());
                        }
                        // 해외
                        else
                        {
                            report.SetData(s, dtH.Rows[0]["EN_EMP_DEPT"].ToString());
                        }

                    }
                    else if (s == "NO_TEL_EMP" || s == "NO_TEL_EMER_EMP")
                    {
                        // 한국 or 지역코드 없음 (싱가폴도 지역코드 안붙임)
                        if (!dtH.Columns.Contains("CD_AREA") || dtH.Rows[0]["CD_AREA"].ToString() == "100" || companyCode == "S100")
                        {
                            report.SetData(s, dtH.Rows[0][s].ToString());
                        }
                        // 해외
                        else
                        {
                            string tel = dtH.Rows[0][s].ToString();
                            if (tel.Length > 5 && tel.Substring(0, 3).IndexOf("82") < 0) tel = "+82-" + tel.Substring(1);
                            report.SetData(s, tel);
                        }
                    }
                    else
                    {
                        report.SetData(s, dtH.Rows[0][s].ToString());
                    }
                }
            }

            // 리포트의 Description 넓이 가져오기
            query = @"
SELECT TOP 1
	*
FROM MA_REPORTL
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_SYSTEM = '" + reportCode + @"'
	AND ISNULL(CD_FLAG, '') != ''";

            DataTable dt = DBHelper.GetDataTable(query);
            int width = dt.Rows.Count == 1 ? Util.GetTO_Int(dt.Rows[0]["CD_FLAG"]) : 220;

            // L
            Util.SetRPT_DataTable(dtL, width);
            report.SetDataTable(dtL);

            return report;
        }

        public static ReportHelper SetRPT_SPO(string reportCode, string reportName, string companyCode, DataTable dtH, DataTable dtL)
        {
            // NO_DSP가 중복이 있을 수 있으므로 고유넘버 부여 (일반품 + 재고품 섞인 경우)
            dtL.Columns.Add("RN", typeof(decimal));
            for (int i = 0; i < dtL.Rows.Count; i++) dtL.Rows[i]["RN"] = i;

            DataTable dt1 = dtL.Copy();
            DataTable dt2 = dtL.Copy();

            // ***** 1번 테이블
            dt1.Columns.Remove("NM_ITEM_PARTNER");
            dt1.Columns.Remove("AM_PO");
            dt1.Columns.Remove("AM_SO_BOM");
            dt1.Columns.Remove("PROFIT");

            dt1.Columns["CD_ITEM_PARTNER"].ColumnName = "NM_ITEM_PARTNER";
            dt1.Columns["UM_PO"].ColumnName = "UM_PO";
            dt1.Columns["UM_SO"].ColumnName = "UM_SO";
            dt1.Columns["RT_PROFIT"].ColumnName = "RT_PROFIT";

            foreach (DataRow row in dt1.Rows) row["DC_RMK"] = "";   // 품목코드 집합 테이블에는 비고를 없앤다 (중복해서 나옴)

            // ***** 2번 테이블
            // 수주금액 재계산 (발주수량 또는 재고수량 Base)
            //for (int i = 0; i < dt2.Rows.Count; i++) dt2.Rows[i]["AM_SO"] = GetToDecimal(dt2.Rows[i]["QT_PO"]) * GetToDecimal(dt2.Rows[i]["UM_SO"]);

            dt2.Columns.Remove("CD_ITEM_PARTNER");
            dt2.Columns.Remove("UM_PO");
            dt2.Columns.Remove("UM_SO");
            dt2.Columns.Remove("RT_PROFIT");

            dt2.Columns["NM_ITEM_PARTNER"].ColumnName = "NM_ITEM_PARTNER";
            dt2.Columns["AM_PO"].ColumnName = "UM_PO";
            dt2.Columns["AM_SO_BOM"].ColumnName = "UM_SO";
            dt2.Columns["PROFIT"].ColumnName = "RT_PROFIT";

            // ORDER BY를 위해 구분값 추가
            dt1.Columns.Add("SEQ");
            dt2.Columns.Add("SEQ");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                // ORDER BY를 위해 순서값 설정
                dt1.Rows[i]["SEQ"] = string.Format("{0:00000}", dt1.Rows[i]["RN"]) + "A";
                dt2.Rows[i]["SEQ"] = string.Format("{0:00000}", dt2.Rows[i]["RN"]) + "B";

                // 2번 테이블 값 삭제
                dt2.Rows[i]["NO_DSP"] = DBNull.Value;
                dt2.Rows[i]["QT_SO"] = DBNull.Value;
                dt2.Rows[i]["QT_PO"] = DBNull.Value;
                dt2.Rows[i]["UNIT_SO"] = DBNull.Value;
                dt2.Rows[i]["LN_SUPPLIER"] = DBNull.Value;
            }

            dt1.Merge(dt2);
            DataTable dt = dt1.Select("", "NO_SO, SEQ").CopyToDataTable();

            return SetRPT(reportCode, reportName, companyCode, dtH, dt);
        }

        public static void SetRPT_DataTable(DataTable dtL, int desc_width)
        {
            int subject_width = 570;
            Font font = new Font("맑은 고딕", 9);
            int cnt = dtL.Rows.Count;
            string subject = "";
            List<string> keyColumns;

            // TP_ROW가 없는 경우 기본값 세팅
            if (!dtL.Columns.Contains("TP_ROW"))
            {
                dtL.Columns.Add("TP_ROW");
                for (int i = 0; i < dtL.Rows.Count; i++) dtL.Rows[i]["TP_ROW"] = "I";
            }

            keyColumns = new List<string>();

            if (dtL.Columns.Contains("NO_FILE")) keyColumns.Add("NO_FILE");
            if (dtL.Columns.Contains("NO_SO")) keyColumns.Add("NO_SO");
            if (dtL.Columns.Contains("NO_PO")) keyColumns.Add("NO_PO");
            if (dtL.Columns.Contains("NO_GIR")) keyColumns.Add("NO_GIR");
            if (dtL.Columns.Contains("NO_IV")) keyColumns.Add("NO_IV");
            if (dtL.Columns.Contains("NO_IV2")) keyColumns.Add("NO_IV2");
            if (dtL.Columns.Contains("NO_KEY")) keyColumns.Add("NO_KEY");

            // 라인별 리마크 표시 여부 => NM_ITEM_PARTNER과 DC_RMK 합친다
            if (dtL.Columns.Contains("YN_DSP_RMK"))
            {
                foreach (DataRow row in dtL.Rows)
                {
                    if (row["YN_DSP_RMK"].ToString() == "Y" && row["DC_RMK"].ToString() != "")
                    {
                        row["NM_ITEM_PARTNER"] = row["NM_ITEM_PARTNER"] + "\n(*OFFER:" + row["DC_RMK"] + ")";
                    }
                }
            }

            // 시작
            for (int i = 0; i < cnt; i++)
            {
                // ========== 서브젝트 행 체크
                if (dtL.Columns.Contains("NM_SUBJECT") && GetTO_String(dtL.Rows[i]["NM_SUBJECT"]) != "")
                {
                    if (dtL.Columns.Contains("NM_SUBJECT") && (subject != GetTO_String(dtL.Rows[i]["NM_SUBJECT"]) || CheckKeyColumns(keyColumns, i, dtL)))
                    {
                        // 행추가
                        DataRow row = dtL.NewRow();

                        foreach (string column in keyColumns)
                        {
                            row[column] = GetTO_String(dtL.Rows[i][column]);
                        }

                        row["NM_SUBJECT"] = GetTO_String(dtL.Rows[i]["NM_SUBJECT"]);
                        row["NM_ITEM_PARTNER"] = GetTO_String(dtL.Rows[i]["NM_SUBJECT"]);
                        dtL.Rows.InsertAt(row, i);

                        // 서브젝트 저장	및 루프카운트 +
                        subject = GetTO_String(dtL.Rows[i]["NM_SUBJECT"]);
                        cnt++;
                    }
                }

                // ========== 길이 체크
                string s = dtL.Rows[i]["NM_ITEM_PARTNER"].ToString().Trim();
                int width = GetTO_String(dtL.Rows[i]["TP_ROW"]) == "" ? subject_width : desc_width;

                string[] flag = { " ", ":", "@", "," };
                int flag_idx = -1;
                int prev_idx = -1;

                do
                {
                    bool rebuild = false;
                    flag_idx = -1;                      // 플래그인덱스 초기화
                    int enter_idx = s.IndexOf("\n");    // 엔터인덱스 선언
                    int cut_idx = -1;                   // 커팅인덱스 선언

                    // 플래그인덱스 저장
                    foreach (string f in flag)
                    {
                        int idx = s.IndexOf(f, prev_idx + 1);
                        if (idx > 0 && (flag_idx == -1 || idx < flag_idx)) flag_idx = idx;
                    }

                    // 엔터인덱스가 플래그인덱스보다 가까운 경우
                    if (enter_idx > 0 && (flag_idx == -1 || enter_idx < flag_idx))
                    {
                        rebuild = true;
                        cut_idx = enter_idx + 1;
                    }
                    // 플래그인덱스가 존재하고 이전인덱스가 존재하고 표시길이보다 큰 경우
                    else if (flag_idx > 0 && prev_idx > 0 && TextRenderer.MeasureText(s.Substring(0, flag_idx), font).Width > width)
                    {
                        rebuild = true;
                        cut_idx = prev_idx + 1;
                    }

                    // 리빌드
                    if (rebuild)
                    {
                        // 현재 ROW
                        dtL.Rows[i]["NM_ITEM_PARTNER"] = s.Substring(0, cut_idx).Replace("\n", ""); // 엔터 제거

                        // 신규 ROW 추가 및 루프카운트 + 
                        DataRow row = dtL.NewRow();
                        if (dtL.Columns.Contains("NO_FILE")) row["NO_FILE"] = GetTO_String(dtL.Rows[i]["NO_FILE"]);
                        if (dtL.Columns.Contains("NO_SO")) row["NO_SO"] = GetTO_String(dtL.Rows[i]["NO_SO"]);
                        if (dtL.Columns.Contains("NO_PO")) row["NO_PO"] = GetTO_String(dtL.Rows[i]["NO_PO"]);
                        if (dtL.Columns.Contains("NO_GIR")) row["NO_GIR"] = GetTO_String(dtL.Rows[i]["NO_GIR"]);
                        if (dtL.Columns.Contains("NO_IV")) row["NO_IV"] = GetTO_String(dtL.Rows[i]["NO_IV"]);
                        if (dtL.Columns.Contains("NO_IV2")) row["NO_IV2"] = GetTO_String(dtL.Rows[i]["NO_IV2"]);
                        if (dtL.Columns.Contains("NO_KEY")) row["NO_KEY"] = GetTO_String(dtL.Rows[i]["NO_KEY"]);
                        if (dtL.Columns.Contains("NM_SUBJECT")) row["NM_SUBJECT"] = dtL.Rows[i]["NM_SUBJECT"];
                        row["NM_ITEM_PARTNER"] = s.Substring(cut_idx);
                        row["TP_ROW"] = dtL.Rows[i]["TP_ROW"].ToString() == "" ? "" : "E";
                        dtL.Rows.InsertAt(row, i + 1);
                        cnt++;
                        break;
                    }

                    prev_idx = flag_idx;

                } while (flag_idx > 0);
            }
        }

        public static void RPT_Print(ReportHelper report)
        {
            Language lang = Global.CurLanguage;

            Global.CurLanguage = Language.KR;
            report.PrintHelper.ShowPrintDialog();
            Global.CurLanguage = lang;
        }

        public static bool CheckKeyColumns(List<string> columns, int index, DataTable dt)
        {
            foreach (string column in columns)
            {
                if (GetTO_String(dt.Rows[index - 1][column]) != GetTO_String(dt.Rows[index][column]))
                    return true;
            }

            return false;
        }



        public static string GetLocalIpAddress()
        {
            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "";
        }
    }

    public enum HighlightMode
    {
        On
      ,
        Off
            , Neutral
    }

    public enum ButtonStatus
    {
        Added
      ,
        Searched
            ,
        Saved
            ,
        Delete
            ,
        Printed
            , Changed
    }

    public enum RowState
    {
        Deleted
      ,
        Added
            ,
        Modified
            , Unchanged
    }

    
}
