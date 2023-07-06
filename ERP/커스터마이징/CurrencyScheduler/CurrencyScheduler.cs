using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dintec;

namespace cs
{
    public partial class CurrencyScheduler : Form
    {
        #region 생성자 & 전역변수
        SqlConnection _sqlConnetion;
        SqlDataAdapter _sqlDataAdapter;
        SqlCommand _sqlCommand;
        string _connectionString, 고시일자, 고시회차;
        DataTable _dt;

        public CurrencyScheduler()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.WriteLog("프로그램 시작");

            this.InitControl();
            this.InitEvent();
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                base.OnClosed(e);
                this.WriteLog("프로그램 종료");
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                timer.Stop();
                this.DB접속해제();

                if (this.chk파일저장.Checked == true) 
                    this.파일쓰기(DateTime.Today.ToString("yyyyMMdd") + "_log", this.txt로그.Text);
            }
        }

        private void InitControl()
        {
            this.txt회사코드.Text = "TEST";
            this.txtDB.Text = "NEOE";
            this.txt서버IP.Text = "192.168.1.143";
            this.txt포트번호.Text = "1433";
            this.txt사용자.Text = "NEOE";
            this.txt패스워드.Text = "NEOE";
            this.txt회차.Text = "1";

            this.btn환율정보가져오기_Click(null, null);
            
            this.timer.Interval = 1000; // 1 초
            this.timer.Start();
        }

        private void InitEvent()
        {
            this.chk타이머.CheckedChanged += new EventHandler(this.chk타이머_CheckedChanged);
            this.timer.Tick += new EventHandler(this.btn환율정보가져오기_Click);
            this.btn환율정보가져오기.Click += new EventHandler(this.btn환율정보가져오기_Click);
            this.btnFileOpen.Click += new EventHandler(this.btnFileOpen_Click);
        }
        #endregion

        #region 이벤트
        private void chk타이머_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk타이머.Checked == true)
                this.timer.Start();
            else
                this.timer.Stop();
        }

        private void btn환율정보가져오기_Click(object sender, EventArgs e)
        {
            Uri address = null;
            WebClient webClient = null;
            StreamReader streamReader = null;
            StreamReader streamReader1 = null;
            Stream stream = null;
            Stream stream1 = null;
            bool 파일저장여부 = false;

            try
            {
                switch(DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        this.WriteLog("휴일에는 환율 정보를 제공하지 않습니다.");
                        this.Close();
                        return;
                }

                this.DB접속();
                
                if (this.chk자동종료.Checked == false)
                {
                    if (this.환율정보존재여부(DateTime.Now.ToString("yyyyMMdd"), this.txt회차.Text) == true)
                    {
                        this.WriteLog(this.txt회차.Text + "회차 환율이 이미 존재합니다.");
                        return;
                    }
                }
   
                address = new Uri("http://203.234.132.2/ip/exrate.txt");
                webClient = new WebClient();
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                stream = webClient.OpenRead(address);
                
                streamReader = new StreamReader(stream, Encoding.Default);

                if (this.환율정보파싱(streamReader, true) == true)
                {
                    파일저장여부 = true;

                    this.환율정보저장();

                    #region 환율정보 확인
                    if (this.환율정보존재여부(this.고시일자, this.txt회차.Text) == true)
                    {
                        if (this.chk자동종료.Checked == true)
                        {
                            this.Close();
                        }
                        else
                        {
                            this.환율정보(this.고시일자, this.txt회차.Text);
                            this.grd환율정보.DataSource = this._dt;
                        }
                    }
                    #endregion
                }
                else
                {
                    if (string.IsNullOrEmpty(this.고시회차)) 파일저장여부 = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                if (파일저장여부 == true && this.chk파일저장.Checked == true)
                {
                    stream1 = webClient.OpenRead(address);
                    streamReader1 = new StreamReader(stream1, Encoding.Default);
                    this.파일쓰기(DateTime.Today.ToString("yyyyMMdd"), streamReader1.ReadToEnd());
                }

                if (stream != null) stream.Close();
                if (stream1 != null) stream1.Close();
                if (streamReader != null) streamReader.Close();
                if (streamReader1 != null) streamReader1.Close();

                this.DB접속해제();
            }
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            StreamReader streamReader = null;

            try
            {
                openFileDialog.InitialDirectory = Application.StartupPath;

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    streamReader = new StreamReader(openFileDialog.FileName);
                    
                    if (this.환율정보파싱(streamReader, false) == true)
                    {
                        this.grd환율정보.DataSource = this._dt;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
        }
        #endregion

        #region 메소드
        private void DBConnection생성()
        {
            this._connectionString = string.Format("Data Source={0},{1};Initial Catalog={2};User id={3};Password={4}",
                                                   this.txt서버IP.Text,
                                                   this.txt포트번호.Text,
                                                   this.txtDB.Text,
                                                   this.txt사용자.Text,
                                                   this.txt패스워드.Text);

            this._sqlConnetion = new SqlConnection(this._connectionString);
        }

        private void DB접속()
        {
            try
            {
                if (this._sqlConnetion != null)
                {
                    if (this._sqlConnetion.State == ConnectionState.Closed)
                    {
                        this._sqlConnetion.Open();
                        this.WriteLog("DB 접속");
                    }
                }
                else
                {
                    this.DBConnection생성();
                    this._sqlConnetion.Open();
                    this.WriteLog("DB 접속");
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private void DB접속해제()
        {
            try
            {
                if (this._sqlConnetion != null)
                {
                    if (this._sqlConnetion.State == ConnectionState.Open)
                    {
                        this._sqlConnetion.Close();
                        this.WriteLog("DB 접속해제");
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private DataTable CreateDataTable()
        {
            DataTable dt;

            dt = new DataTable();

            dt.Columns.Add("YYMMDD", typeof(string));
            dt.Columns.Add("NO_SEQ", typeof(int));
            dt.Columns.Add("QUOTATION_TIME", typeof(string));
            dt.Columns.Add("CURR_SOUR", typeof(string));
            dt.Columns.Add("CURR_DEST", typeof(string));
            dt.Columns.Add("CD_COMPANY", typeof(string));
            dt.Columns.Add("RATE_BASE", typeof(decimal));
            dt.Columns.Add("RATE_SALE", typeof(decimal));
            dt.Columns.Add("RATE_BUY", typeof(decimal));
            dt.Columns.Add("ID_INSERT", typeof(string));
            dt.Columns.Add("DTS_INSERT", typeof(string));
            
            return dt;
        }

        private void WriteLog(string log)
        {
            this.txt로그.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + log + Environment.NewLine;
        }

        private void 파일쓰기(string fileName, string text)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            string filePath;

            try
            {
                if (string.IsNullOrEmpty(text.Trim())) return;

                filePath = Application.StartupPath + "\\CurrencyData\\" + fileName + ".txt";
                
                if (!Directory.Exists(Application.StartupPath + "\\CurrencyData"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\CurrencyData");
                }

                fileStream = new FileStream(filePath, FileMode.Append);

                streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write("Log Time : " + DateTime.Now.ToString("yyyy-MM-dd tt hh:mm:ss") + Environment.NewLine + text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }

        private bool 환율정보파싱(StreamReader streamReader, bool 고시일자확인)
        {
            string lineStr, 고시시간;
            int lineNum;
            DataRow row;

            try
            {
                lineNum = 0;
                lineStr = string.Empty;

                this.고시일자 = string.Empty;
                this.고시회차 = string.Empty;
                고시시간 = string.Empty;

                while ((lineStr = streamReader.ReadLine()) != null && !(lineStr.Substring(0, 1).ToUpper() == "E"))
                {
                    if (lineNum == 0)
                    {
                        if (lineStr.Length <= 32)
                        {
                            this.WriteLog("환율정보를 읽을 수 없습니다.");
                            return false;
                        }

                        this.고시일자 = lineStr.Substring(10, 8);
                        this.고시회차 = Convert.ToInt32(lineStr.Substring(32, 2)).ToString();
                        고시시간 = lineStr.Substring(34, 6);

                        if (고시일자확인 == true && this.고시회차 != this.txt회차.Text)
                        {
                            this.WriteLog(this.txt회차.Text + " 회차 환율이 아닙니다. [" + this.고시회차 + " 회차]");
                            return false;
                        }

                        this.WriteLog("파싱시작" + Environment.NewLine +
                                      "고시일자 : " + this.고시일자 + Environment.NewLine +
                                      "고시회차 : " + this.고시회차);

                        this._dt = this.CreateDataTable();

                        lineStr = streamReader.ReadLine();
                        ++lineNum;
                    }

                    row = this._dt.NewRow();

                    if (lineNum % 2 > 0)
                    {
                        row["YYMMDD"] = this.고시일자;
                        row["NO_SEQ"] = this.고시회차;
                        row["QUOTATION_TIME"] = 고시시간;
                        row["CURR_SOUR"] = lineStr.Substring(3, 3);
                        row["CURR_DEST"] = "KRW";
                        lineStr = streamReader.ReadLine();
                        ++lineNum;
                    }

                    if (lineNum % 2 == 0)
                    {
                        if (!lineStr.Contains("#DIV/0!"))
                        {
                            row["RATE_SALE"] = Convert.ToDecimal(string.Format("{0:###.#0}", ((double)Convert.ToInt64(lineStr.Substring(10, 7)) * 0.01)));
                            row["RATE_BUY"] = Convert.ToDecimal(string.Format("{0:###.#0}", ((double)Convert.ToInt64(lineStr.Substring(31, 7)) * 0.01)));
                            row["RATE_BASE"] = Convert.ToDecimal(string.Format("{0:###.#0}", ((double)Convert.ToInt64(lineStr.Substring(38, 7)) * 0.01)));
                        }
                        else
                        {
                            row["RATE_SALE"] = 0;
                            row["RATE_BUY"] = 0;
                            row["RATE_BASE"] = 0;
                        }
                    }

                    ++lineNum;
                    this._dt.Rows.Add(row);
                }

                this.WriteLog("파싱완료");
                return true;
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
                return false;
            }
        }

        private void 환율정보저장()
        {
            string procedure;

            try
            {
                if (this._dt == null || this._dt.Rows.Count == 0) return;

                procedure = "SP_CZ_MA_EXCHANGE_RATE_INFO";

                this._sqlCommand = new SqlCommand(procedure, this._sqlConnetion);
                this._sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in this._dt.Rows)
                {
                    this._sqlCommand.Parameters.Clear();

                    this._sqlCommand.Parameters.AddWithValue("@P_YYMMDD", dataRow["YYMMDD"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NO_SEQ", dataRow["NO_SEQ"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_QUOTATION_TIME", dataRow["QUOTATION_TIME"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_SOURCE", dataRow["CURR_SOUR"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_DESTINATION", dataRow["CURR_DEST"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_COMPANY", this.txt회사코드.Text);
                    this._sqlCommand.Parameters.AddWithValue("@P_RATE_BASE", dataRow["RATE_BASE"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_RATE_SALE", dataRow["RATE_SALE"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_RATE_BUY", dataRow["RATE_BUY"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_ID_INSERT", "SYSTEM");

                    this._sqlCommand.ExecuteNonQuery();
                }

                this.WriteLog(this.txt회차.Text + " 회차 환율 저장 완료");
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private void 환율정보(string 고시일자, string 고시회차)
        {
            string sql;

            try
            {
                this._dt = new DataTable();

                sql = "SELECT YYMMDD, NO_SEQ, QUOTATION_TIME, CURR_SOUR, CURR_DEST, CD_COMPANY, RATE_BASE, RATE_SALE, RATE_BUY, ID_INSERT, DTS_INSERT " +
                      "FROM MA_EXCHANGE " +
                      "WHERE YYMMDD = '" + 고시일자 + "' AND NO_SEQ = '" + 고시회차 + "' AND ID_INSERT = 'SYSTEM'";

                Console.WriteLine(sql);

                this._sqlCommand = new SqlCommand(sql, this._sqlConnetion);
                this._sqlCommand.CommandType = CommandType.Text;
                this._sqlDataAdapter = new SqlDataAdapter();
                this._sqlDataAdapter.SelectCommand = this._sqlCommand;
                this._sqlDataAdapter.Fill(this._dt);
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private bool 환율정보존재여부(string 고시일자, string 고시회차)
        {
            string sql;
            int count;

            try
            {
                sql = "SELECT COUNT(*) AS COUNT " +
                      "FROM MA_EXCHANGE " +
                      "WHERE YYMMDD = '" + 고시일자 + "' AND NO_SEQ = '" + 고시회차 + "' AND ID_INSERT = 'SYSTEM'";

                Console.WriteLine(sql);

                this._sqlCommand = new SqlCommand(sql, this._sqlConnetion);
                this._sqlCommand.CommandType = CommandType.Text;
                count = Convert.ToInt32(this._sqlCommand.ExecuteScalar().ToString());

                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }

            return false;
        }
        #endregion
    }
}
