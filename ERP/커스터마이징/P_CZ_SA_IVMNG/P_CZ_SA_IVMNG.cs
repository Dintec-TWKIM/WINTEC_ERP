using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.Utils;
using Duzon.Windows.Print;
using DX;
using NeoportDtiX;

namespace cz
{
    public partial class P_CZ_SA_IVMNG : PageBase
    {
        #region 생성자 & 전역변수
        private P_CZ_SA_IVMNG_BIZ _biz;
		private P_CZ_SA_IVMNG_GW _gw = new P_CZ_SA_IVMNG_GW();
		private bool _isPainted;
        private string 사용자ID;
        private string 회사번호;
        private string 지불회사;
        private string 구분;
        private string 위치;
        private string 이용사이트;
        private string 사이트구분;
        
        private string 전표승인여부
        {
            get
            {
                if (this.rbo전표전체.Checked == true)
                    return "A";
                else if (this.rbo전표승인.Checked == true)
                    return "2";
                else
                    return "1";
            }
        }

        public P_CZ_SA_IVMNG()
        {
            StartUp.Certify(this);
            this.InitializeComponent();
        }

        public P_CZ_SA_IVMNG(string 계산서번호)
        {
            StartUp.Certify(this);
            this.InitializeComponent();

            this.txt계산서번호.Text = 계산서번호;
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_IVMNG_BIZ();

            this.InitGrid();
            this.InitControl();
            this.InitStyle();
            this.InitEvent();

            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.dtp처리일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
                this.dtp처리일자.EndDateToString = Global.MainFrame.GetStringToday;

                if (!Certify.IsLive())
                {
					this.btn전자결재.Visible = false;
					this.btn전자세금계산서.Visible = false;
                    this.btn출력옵션.Visible = false;
                    this.btn거래명세표.Visible = false;
                    this.btn전표출력.Visible = false;
                    this.btn회계전표처리.Visible = false;
                    this.btn회계전표취소.Visible = false;
                    this.btn선수금정리.Visible = false;
                    this.bpPanelControl12.Visible = false;
                }

				this.btn전자결재.Enabled = false;
                this.btnINVOICE.Enabled = false;
                this.btn거래명세표.Enabled = false;
                this.btn전표출력.Enabled = false;
                this.btn회계전표처리.Enabled = false;
                this.btn회계전표취소.Enabled = false;
                this.btn선수금정리.Enabled = false;

                if (!string.IsNullOrEmpty(this.txt계산서번호.Text))
                    this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitControl()
        {
            DataSet comboData;

            try
            {
                this.oneGrid1.UseCustomLayout = true;
                this.bpPanelControl1.IsNecessaryCondition = true;
                this.oneGrid1.InitCustomLayout();

                comboData = this.GetComboData(new string[] { "S;MA_BIZAREA;",
                                                             "S;SA_B000028",
                                                             "S;PU_C000016",
                                                             "S;SA_B000039",
                                                             "S;MA_BIZAREA;",
                                                             "N;SA_B000040",
                                                             "S;MA_B000073",
                                                             "S;MA_B000080",
                                                             "S;MA_PLANT",
                                                             "S;MA_B000040",
                                                             "S;MA_B000065",
                                                             "S;MA_B000067",
                                                             "N;MA_B000010",
                                                             "S;SA_B000063" });

                this.cbo공장.DataSource = comboData.Tables[8];
                this.cbo공장.ValueMember = "CODE";
                this.cbo공장.DisplayMember = "NAME";

                this.cbo처리상태.DataSource = comboData.Tables[1];
                this.cbo처리상태.ValueMember = "CODE";
                this.cbo처리상태.DisplayMember = "NAME";

                DataTable dataTable1 = comboData.Tables[2].Copy();
                while (dataTable1.Rows.Count > 4)
                    dataTable1.Rows.RemoveAt(4);

                this.cbo거래구분.DataSource = dataTable1;
                this.cbo거래구분.ValueMember = "CODE";
                this.cbo거래구분.DisplayMember = "NAME";

                if (Certify.IsLive())
                {
                    this._flexH.SetDataMap("CD_BIZAREA", comboData.Tables[0].Copy(), "CODE", "NAME");
                    this._flexH.SetDataMap("CD_BIZAREA_TAX", comboData.Tables[0].Copy(), "CODE", "NAME");
                    this._flexH.SetDataMap("TP_RECEIPT", comboData.Tables[5], "CODE", "NAME");
                }
                
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("CODE", typeof(string));
                dataTable2.Columns.Add("NAME", typeof(string));
                DataRow row1 = dataTable2.NewRow();
                row1["CODE"] = "0";
                row1["NAME"] = this.DD("발송요청");
                dataTable2.Rows.Add(row1);
                DataRow row2 = dataTable2.NewRow();
                row2["CODE"] = "1";
                row2["NAME"] = this.DD("발송처리");
                dataTable2.Rows.Add(row2);
                DataRow row3 = dataTable2.NewRow();
                row3["CODE"] = "2";
                row3["NAME"] = this.DD("승인처리");
                dataTable2.Rows.Add(row3);
                DataRow row4 = dataTable2.NewRow();
                row4["CODE"] = "3";
                row4["NAME"] = this.DD("반려처리");
                dataTable2.Rows.Add(row4);
                DataRow row5 = dataTable2.NewRow();
                row5["CODE"] = "4";
                row5["NAME"] = this.DD("승인취소(요청)");
                dataTable2.Rows.Add(row5);
                DataRow row6 = dataTable2.NewRow();
                row6["CODE"] = "5";
                row6["NAME"] = this.DD("승인취소(승인)");
                dataTable2.Rows.Add(row6);
                DataRow row7 = dataTable2.NewRow();
                row7["CODE"] = "6";
                row7["NAME"] = this.DD("승인취소거절");
                dataTable2.Rows.Add(row7);
                DataRow row8 = dataTable2.NewRow();
                row8["CODE"] = "8";
                row8["NAME"] = this.DD("삭제처리");
                dataTable2.Rows.Add(row8);
                DataRow row9 = dataTable2.NewRow();
                row9["CODE"] = "9";
                row9["NAME"] = this.DD("취소");
                dataTable2.Rows.Add(row9);
                DataRow row10 = dataTable2.NewRow();
                row10["CODE"] = "00";
                row10["NAME"] = this.DD("미발행");
                dataTable2.Rows.Add(row10);
                DataRow row11 = dataTable2.NewRow();
                row11["CODE"] = "01";
                row11["NAME"] = this.DD("발행");
                dataTable2.Rows.Add(row11);
                DataRow row12 = dataTable2.NewRow();
                row12["CODE"] = "02";
                row12["NAME"] = this.DD("승인");
                dataTable2.Rows.Add(row12);
                DataRow row13 = dataTable2.NewRow();
                row13["CODE"] = "03";
                row13["NAME"] = this.DD("반려");
                dataTable2.Rows.Add(row13);
                DataRow row14 = dataTable2.NewRow();
                row14["CODE"] = "04";
                row14["NAME"] = this.DD("발행취소");
                dataTable2.Rows.Add(row14);
                DataRow row15 = dataTable2.NewRow();
                row15["CODE"] = "21";
                row15["NAME"] = this.DD("승인/발행취소요청");
                dataTable2.Rows.Add(row15);
                DataRow row16 = dataTable2.NewRow();
                row16["CODE"] = "22";
                row16["NAME"] = this.DD("승인취소");
                dataTable2.Rows.Add(row16);
                DataRow row17 = dataTable2.NewRow();
                row17["CODE"] = "23";
                row17["NAME"] = this.DD("반려승인");
                dataTable2.Rows.Add(row17);
                DataRow row18 = dataTable2.NewRow();
                row18["CODE"] = "99";
                row18["NAME"] = this.DD("삭제");
                dataTable2.Rows.Add(row18);
                DataRow row19 = dataTable2.NewRow();
                row19["CODE"] = "xx";
                row19["NAME"] = this.DD("서면");
                dataTable2.Rows.Add(row19);
                DataRow row20 = dataTable2.NewRow();
                row20["CODE"] = "xy";
                row20["NAME"] = this.DD("역발행");
                dataTable2.Rows.Add(row20);

                if (Certify.IsLive()) this._flexH.SetDataMap("ETAX_TOT_ST", dataTable2.Copy(), "CODE", "NAME");

                DataTable dataTable3 = new DataTable();
                dataTable3.Columns.Add("CODE", typeof(string));
                dataTable3.Columns.Add("NAME", typeof(string));
                DataRow row21 = dataTable3.NewRow();
                row21["CODE"] = "";
                row21["NAME"] = "";
                dataTable3.Rows.Add(row21);
                DataRow row22 = dataTable3.NewRow();
                row22["CODE"] = "0";
                row22["NAME"] = this.DD("미발행");
                dataTable3.Rows.Add(row22);
                DataRow row23 = dataTable3.NewRow();
                row23["CODE"] = "A";
                row23["NAME"] = this.DD("발송요청");
                dataTable3.Rows.Add(row23);
                DataRow row24 = dataTable3.NewRow();
                row24["CODE"] = "B";
                row24["NAME"] = this.DD("발송처리");
                dataTable3.Rows.Add(row24);
                DataRow row25 = dataTable3.NewRow();
                row25["CODE"] = "C";
                row25["NAME"] = this.DD("승인처리");
                dataTable3.Rows.Add(row25);
                DataRow row26 = dataTable3.NewRow();
                row26["CODE"] = "D";
                row26["NAME"] = this.DD("반려처리");
                dataTable3.Rows.Add(row26);
                DataRow row27 = dataTable3.NewRow();
                row27["CODE"] = "E";
                row27["NAME"] = this.DD("취소");
                dataTable3.Rows.Add(row27);
                DataRow row28 = dataTable3.NewRow();
                row28["CODE"] = "F";
                row28["NAME"] = this.DD("서면");
                dataTable3.Rows.Add(row28);
                DataRow row29 = dataTable3.NewRow();
                row29["CODE"] = "G";
                row29["NAME"] = this.DD("역발행");
                dataTable3.Rows.Add(row29);
                DataRow row30 = dataTable3.NewRow();
                row30["CODE"] = "H";
                row30["NAME"] = this.DD("삭제");
                dataTable3.Rows.Add(row30);

                this.cbo전자세금계산서.DataSource = dataTable3;
                this.cbo전자세금계산서.DisplayMember = "NAME";
                this.cbo전자세금계산서.ValueMember = "CODE";

                this.cbo휴폐업구분.DataSource = comboData.Tables[6];
                this.cbo휴폐업구분.DisplayMember = "NAME";
                this.cbo휴폐업구분.ValueMember = "CODE";

                this.ctx사업장.CodeValue = this.LoginInfo.BizAreaCode;
                this.ctx사업장.CodeName = this.LoginInfo.BizAreaName;

                this.ctx부가세사업장.CodeValue = this.LoginInfo.BizAreaCode;
                this.ctx부가세사업장.CodeName = this.LoginInfo.BizAreaName;

                DataTable dataTable4 = new DataTable();
                dataTable4.Columns.Add("CODE", typeof(string));
                dataTable4.Columns.Add("NAME", typeof(string));
                DataRow row31 = dataTable4.NewRow();
                row31["CODE"] = "1000";
                row31["NAME"] = "[E-Mail+SMS]";
                dataTable4.Rows.Add(row31);
                DataRow row32 = dataTable4.NewRow();
                row32["CODE"] = "1100";
                row32["NAME"] = "[E-Mail+MOBILE TAX]";
                dataTable4.Rows.Add(row32);
                
                if (Certify.IsLive()) this._flexH.SetDataMap("ETAX_SEND_TYPE", dataTable4, "CODE", "NAME");

                SetControl setControl = new SetControl();
                setControl.SetCombobox(this.cbo부가세구분, comboData.Tables[9]);
                setControl.SetCombobox(this.cbo매출처그룹, comboData.Tables[10]);
                setControl.SetCombobox(this.cbo매출처그룹2, comboData.Tables[11]);

                this._flexL.SetDataMap("CLS_ITEM", comboData.Tables[12], "CODE", "NAME");
                this._flexL.SetDataMap("FG_USE2", comboData.Tables[13], "CODE", "NAME");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
            this._flexH.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flexH.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flexH_OwnerDrawCell);
            this._flexH.CellContentChanged += new CellContentEventHandler(this._flexH_CellContentChanged);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);
            this._flexH.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
            this._flexL.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

			this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
			this.btn출력옵션.Click += new EventHandler(this.btn출력옵션_Click);
            this.btnINVOICE.Click += new EventHandler(this.btnINVOICE_Click);
            this.btn전자세금계산서.Click += new EventHandler(this.btn전자세금계산서_Click);
            this.btn거래명세표.Click += new EventHandler(this.btn거래명세표_Click);
            this.btn전표출력.Click += new EventHandler(this.btn전표출력_Click);
            this.btn회계전표처리.Click += new EventHandler(this.btn회계전표처리_Click);
            this.btn회계전표취소.Click += new EventHandler(this.btn회계전표취소_Click);
            this.btn선수금정리.Click += new EventHandler(this.btn선수금정리_Click);
			this.btn발송용저장.Click += Btn발송용저장_Click;
			this.btn외화획득명세서.Click += Btn외화획득명세서_Click;
			this.btn일괄출력.Click += Btn일괄출력_Click;

            this.ctx매출처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
        }

        private void Btn일괄출력_Click(object sender, EventArgs e)
        {
            DataTable dt, dt1, dt2, dtH, dtL, dtH1, dtL1, dtH2, dtL2;
            DataRow[] dataRowArray, dataRowArray1;
            ReportHelper reportHelper;
            List<string> fileList;
            Dictionary<string, string> _dic첨부파일, _dic첨부파일1;
            Dictionary<string, List<string>> _dic인수증;
            Dictionary<string, string> _dic고객발주서;
            string 대표자명, filePath, filePath1, query, 매출처코드, fileName;
            int index;

            try
            {
                if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    FolderBrowserDialog dlg = new FolderBrowserDialog();
                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    filePath = Path.Combine(Application.StartupPath, "temp");

                    this.임시파일제거();

                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    index = 0;
                    foreach (DataRow dr in dataRowArray)
					{
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        filePath1 = filePath + "\\" + dr["NO_IV"].ToString();

                        if (!Directory.Exists(filePath1))
                            Directory.CreateDirectory(filePath1);

                        _dic첨부파일 = new Dictionary<string, string>();
                        _dic첨부파일1 = new Dictionary<string, string>();
                        _dic인수증 = new Dictionary<string, List<string>>();
                        _dic고객발주서 = new Dictionary<string, string>();

                        매출처코드 = dr["CD_PARTNER"].ToString();

                        dtH = this._flexH.DataTable.Clone();
                        dtL = this._flexL.DataTable.Clone();

                        dtL.Columns.Add("IDX");
                        dtL.Columns.Add("TO_NOCOMPANY");
                        dtL.Columns.Add("TO_COMPANY");
                        dtL.Columns.Add("TO_NAME");
                        dtL.Columns.Add("TO_ADRESS");
                        dtL.Columns.Add("TO_TPJOB");
                        dtL.Columns.Add("TO_CLSJOB");

                        #region 데이터 생성
                        dtH.ImportRow(dr);

                        dataRowArray1 = this._flexL.DataTable.Select("NO_IV = '" + dr["NO_IV"].ToString() + "'");

                        if (dataRowArray1 == null || dataRowArray1.Length == 0)
                        {
                            dataRowArray1 = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  dr["NO_IV"].ToString(),
                                                                                  this.txt프로젝트.Text,
                                                                                  this.cbo공장.SelectedValue,
                                                                                  this.txt주문번호.Text,
                                                                                  this.ctx호선번호.CodeValue,
                                                                                  this.bpc영업조직.QueryWhereIn_Pipe,
                                                                                  this.bpc영업그룹.QueryWhereIn_Pipe }).Select();
                        }

                        foreach (DataRow dr1 in dataRowArray1)
                        {
                            dtL.ImportRow(dr1);

                            dtL.Rows[dtL.Rows.Count - 1]["IDX"] = index.ToString();
                            dtL.Rows[dtL.Rows.Count - 1]["TO_NOCOMPANY"] = dr["TO_NOCOMPANY"];
                            dtL.Rows[dtL.Rows.Count - 1]["TO_COMPANY"] = dr["TO_COMPANY"];
                            dtL.Rows[dtL.Rows.Count - 1]["TO_NAME"] = dr["TO_NAME"];
                            dtL.Rows[dtL.Rows.Count - 1]["TO_ADRESS"] = dr["TO_ADRESS"];
                            dtL.Rows[dtL.Rows.Count - 1]["TO_TPJOB"] = dr["TO_TPJOB"];
                            dtL.Rows[dtL.Rows.Count - 1]["TO_CLSJOB"] = dr["TO_CLSJOB"];
                        }

						dtL.DefaultView.Sort = "NM_VESSEL ASC, NO_PO_PARTNER ASC";
						dtL = dtL.DefaultView.ToTable();
						#endregion

						dt = ComFunc.getGridGroupBy(dataRowArray1.ToDataTable(), new string[] { "CD_COMPANY", "NO_IO" }, true);

                        foreach (DataRow dr1 in dt.Rows)
                        {
                            #region 인수증 확인
                            query = @"SELECT DISTINCT MF.FILE_NAME,
	            ('/Upload/P_CZ_SA_GIM_REG/' + OH.CD_COMPANY + '/' + LEFT(GH.DT_GIR, 4)) AS FILE_PATH,
                (OL.NO_GIR + '_' + OH.CD_COMPANY) AS CD_FILE,
                ISNULL(MF.NO_REF, '') AS NO_REF
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
		       MAX(OL.NO_ISURCV) AS NO_GIR
	    FROM MM_QTIO OL WITH(NOLOCK)
	    GROUP BY OL.CD_COMPANY, OL.NO_IO) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
LEFT JOIN SA_GIRH GH WITH(NOLOCK) ON GH.CD_COMPANY = OL.CD_COMPANY AND GH.NO_GIR = OL.NO_GIR
LEFT JOIN MA_FILEINFO MF WITH(NOLOCK) ON MF.CD_COMPANY = OL.CD_COMPANY AND MF.CD_MODULE = 'SA' AND MF.ID_MENU = 'P_CZ_SA_GIM_REG' AND MF.CD_FILE = OL.NO_GIR + '_' + OL.CD_COMPANY
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'
AND UPPER(MF.FILE_EXT) IN ('JPG', 'PNG', 'JPEG', 'PDF', 'TIF')";

                            dt1 = DBHelper.GetDataTable(string.Format(query, dr1["CD_COMPANY"].ToString(), dr1["NO_IO"].ToString()));

                            if (!Directory.Exists(filePath1 + "\\" + dr1["NO_IO"].ToString()))
                                Directory.CreateDirectory(filePath1 + "\\" + dr1["NO_IO"].ToString());

                            foreach (DataRow dr2 in dt1.Rows)
                            {
                                FileUploader.DownloadFile(dr2["FILE_NAME"].ToString(), filePath1 + "\\" + dr1["NO_IO"].ToString(), dr2["FILE_PATH"].ToString(), dr2["CD_FILE"].ToString());
                                FileInfo fileInfo = new FileInfo(filePath1 + "\\" + dr1["NO_IO"].ToString() + "\\" + dr2["FILE_NAME"].ToString());

                                if (fileInfo.Extension.ToUpper() != ".PDF")
                                {
                                    PDF.ImageToPdf(fileInfo.FullName);
                                    fileName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty) + ".pdf";
                                }
                                else
                                    fileName = dr2["FILE_NAME"].ToString();

                                if (!_dic첨부파일.ContainsKey(fileName))
                                    _dic첨부파일.Add(fileName, filePath1 + "\\" + dr1["NO_IO"].ToString() + "\\" + fileName);

                                if (!string.IsNullOrEmpty(dr2["NO_REF"].ToString()) &&
                                    !_dic첨부파일1.ContainsKey(dr2["NO_REF"].ToString()))
                                    _dic첨부파일1.Add(dr2["NO_REF"].ToString(), filePath1 + "\\" + dr1["NO_IO"].ToString() + "\\" + fileName);
                            }

                            query = @"SELECT OL.NO_SO,
	   (OL.NO_GIR + '_' + OL.NO_SO + '_' + OH.CD_COMPANY) AS NM_FILE
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
			 OL.NO_ISURCV AS NO_GIR,
			 OL.NO_PSO_MGMT AS NO_SO
	  FROM MM_QTIO OL WITH(NOLOCK)
	  GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.NO_ISURCV, OL.NO_PSO_MGMT) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'";

                            dt2 = DBHelper.GetDataTable(string.Format(query, dr1["CD_COMPANY"].ToString(), dr1["NO_IO"].ToString()));
                            string errorMsg = string.Empty;

                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                if (_dic첨부파일1.ContainsKey(dr2["NO_SO"].ToString()))
								{
                                    if (!_dic인수증.ContainsKey(dr1["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString()))
                                    {
                                        List<string> 파일리스트 = new List<string>();
                                        파일리스트.Add(_dic첨부파일1[dr2["NO_SO"].ToString()]);

                                        _dic인수증.Add(dr1["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString(), 파일리스트);
                                    }

                                    _dic첨부파일1.Remove(dr2["NO_SO"].ToString());
                                }
                                else if (_dic첨부파일.ContainsKey(dr2["NM_FILE"].ToString() + ".pdf"))
                                {
                                    if (!_dic인수증.ContainsKey(dr1["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString()))
                                    {
                                        List<string> 파일리스트 = new List<string>();
                                        파일리스트.Add(_dic첨부파일[dr2["NM_FILE"].ToString() + ".pdf"]);

                                        _dic인수증.Add(dr1["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString(), 파일리스트);
                                    }

                                    _dic첨부파일.Remove(dr2["NM_FILE"].ToString() + ".pdf");
                                }
                                else if (_dic첨부파일.ContainsKey(dr2["NM_FILE"].ToString() + ".PDF"))
                                {
                                    if (!_dic인수증.ContainsKey(dr1["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString()))
                                    {
                                        List<string> 파일리스트 = new List<string>();
                                        파일리스트.Add(_dic첨부파일[dr2["NM_FILE"].ToString() + ".PDF"]);

                                        _dic인수증.Add(dr1["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString(), 파일리스트);
                                    }

                                    _dic첨부파일.Remove(dr2["NM_FILE"].ToString() + ".PDF");
                                }
                                else
                                {
                                    errorMsg = string.Format("인수증이 누락된 수주가 있습니다. [출고번호 : {0}, 수주번호 : {1}]", dr1["NO_IO"].ToString(), dr2["NO_SO"].ToString());
                                    break;
                                }
                            }

                            if (!string.IsNullOrEmpty(errorMsg))
                            {
                                Global.MainFrame.ShowMessage(errorMsg);
                                return;
                            }
                            #endregion

                            #region 고객발주서
                            //현대글로비스(11845)
                            //에스티엑스마린서비스(07742)
                            //시너지코리아(15501)
                            if (매출처코드 == "11845" || 매출처코드 == "07742" || 매출처코드 == "15501")
                            {
                                query = @"SELECT OH.CD_COMPANY,
	   OL.NO_SO,
	   WL.NM_FILE_REAL
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN (SELECT OL.CD_COMPANY, OL.NO_IO,
			 OL.NO_ISURCV AS NO_GIR,
			 OL.NO_PSO_MGMT AS NO_SO
	  FROM MM_QTIO OL WITH(NOLOCK)
	  GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.NO_ISURCV, OL.NO_PSO_MGMT) OL
ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
JOIN CZ_MA_WORKFLOWL WL ON WL.CD_COMPANY = OH.CD_COMPANY AND WL.NO_KEY = OL.NO_SO
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'
AND WL.TP_STEP = '08'
AND WL.NM_FILE_REAL LIKE '%.PDF'";

                                dt1 = DBHelper.GetDataTable(string.Format(query, dr1["CD_COMPANY"].ToString(), dr1["NO_IO"].ToString()));
                                dt2 = ComFunc.getGridGroupBy(dt1, new string[] { "NO_SO" }, true);

                                foreach (DataRow dr2 in dt2.Rows)
                                {
                                    List<string> tmpList = new List<string>();

                                    index = 0;
                                    foreach (DataRow dr3 in dt1.Select(string.Format("NO_SO = '{0}'", dr2["NO_SO"].ToString())))
                                    {
                                        string 파일명 = FileMgr.Download_WF(dr3["CD_COMPANY"].ToString(), dr3["NO_SO"].ToString(), dr3["NM_FILE_REAL"].ToString(), filePath1 + "\\" + dr2["NO_SO"].ToString() + "_PO_" + index.ToString() + ".pdf", false);

                                        tmpList.Add(filePath1 + "\\" + 파일명);

                                        index++;
                                    }

                                    PDF.Merge(filePath1 + "\\" + dr2["NO_SO"].ToString() + ".pdf", tmpList.ToArray());

                                    if (!_dic고객발주서.ContainsKey(dr2["NO_SO"].ToString()))
                                        _dic고객발주서.Add(dr2["NO_SO"].ToString(), filePath1 + "\\" + dr2["NO_SO"].ToString() + ".pdf");
                                }
                            }
                            #endregion
                        }

                        대표자명 = string.Empty;
                        if (this.중복확인(dtH, "FROM_NAME", "대표자명", ref 대표자명) == false) return;

                        dtH1 = dtH.Copy();
                        dtL1 = dtL.Copy();

                        reportHelper = Util.SetRPT("R_CZ_SA_IVMNG_3", "매출관리-INVOICE", Global.MainFrame.LoginInfo.CompanyCode, dtH, dtL);

                        fileList = new List<string>();

                        #region Invoice Cover 출력
                        reportHelper.SetDataTable(dtH, 1);
                        reportHelper.SetDataTable(dtL, 2);

                        reportHelper.SetData("대표자명", 대표자명);
                        reportHelper.SetData("직인표시", Settings.Default.직인표시);

                        reportHelper.PrintHelper.UseUserFontStyle();

                        if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                            reportHelper.PrintDirect("R_CZ_SA_IVMNG_3_K100_SM.DRF", false, true, filePath1 + "\\" + "Cover.pdf", new Dictionary<string, string>());
                        else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                            reportHelper.PrintDirect("R_CZ_SA_IVMNG_3_K200_SM.DRF", false, true, filePath1 + "\\" + "Cover.pdf", new Dictionary<string, string>());

                        fileList.Add(filePath1 + "\\" + "Cover.pdf");
                        #endregion

                        dt = ComFunc.getGridGroupBy(dtL1.Select("ISNULL(CD_COMPANY, '') <> ''"), new string[] { "CD_COMPANY", "NO_IV", "NO_SO" }, true);

                        foreach (DataRow dr1 in dt.Rows)
                        {
                            dtH2 = dtH1.Select(string.Format("NO_IV = '{0}'", dr1["NO_IV"].ToString())).ToDataTable();
                            dtL2 = dtL1.Select(string.Format("NO_IV = '{0}' AND NO_SO = '{1}'", dr1["NO_IV"].ToString(), dr1["NO_SO"].ToString())).ToDataTable();

                            #region 거래명세서
                            reportHelper = Util.SetRPT("R_CZ_SA_IVMNG_3", "매출관리-INVOICE", Global.MainFrame.LoginInfo.CompanyCode, dtH2, dtL2);

                            reportHelper.SetDataTable(dtH2, 1);
                            reportHelper.SetDataTable(dtL2, 2);

                            reportHelper.SetData("대표자명", 대표자명);
                            reportHelper.SetData("직인표시", Settings.Default.직인표시);

                            reportHelper.PrintHelper.UseUserFontStyle();

                            if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_3_K100_TRSTM2.DRF", false, true, filePath1 + "\\" + dr1["NO_IV"].ToString() + "_" + dr1["NO_SO"].ToString() + "_TRSTM.pdf", new Dictionary<string, string>());
                            else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                                reportHelper.PrintDirect("R_CZ_SA_IVMNG_3_K200_TRSTM2.DRF", false, true, filePath1 + "\\" + dr1["NO_IV"].ToString() + "_" + dr1["NO_SO"].ToString() + "_TRSTM.pdf", new Dictionary<string, string>());

                            fileList.Add(filePath1 + "\\" + dr1["NO_IV"].ToString() + "_" + dr1["NO_SO"].ToString() + "_TRSTM.pdf");
                            #endregion

                            dt1 = ComFunc.getGridGroupBy(dtL1.Select(string.Format("ISNULL(CD_COMPANY, '') <> '' AND NO_IV = '{0}' AND NO_SO = '{1}'", dr1["NO_IV"].ToString(), dr1["NO_SO"].ToString())), new string[] { "NO_IO", "NO_SO" }, true);

                            foreach (DataRow dr2 in dt1.Rows)
							{
                                if (_dic인수증.ContainsKey(dr2["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString()))
                                    fileList.AddRange(_dic인수증[dr2["NO_IO"].ToString() + "_" + dr2["NO_SO"].ToString()]);
                            }

                            //현대글로비스(11845)
                            //에스티엑스마린서비스(07742)
                            //시너지코리아(15501)
                            #region 발주서
                            if (매출처코드 == "11845" || 매출처코드 == "07742" || 매출처코드 == "15501")
                            {
                                if (_dic고객발주서.ContainsKey(dr1["NO_SO"].ToString()))
                                    fileList.Add(_dic고객발주서[dr1["NO_SO"].ToString()]);
                            }
                            #endregion
                        }

                        PDF.Merge(dlg.SelectedPath + "//" + dr["NO_IV"].ToString() + ".pdf", fileList.ToArray());
                    }
				}
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
			{
                MsgControl.CloseMsg();
            }
        }

        private void 임시파일제거()
        {
            DirectoryInfo dirInfo;
            bool isExistFile;

            try
            {
                dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "temp"));
                isExistFile = false;

                if (dirInfo.Exists == true)
                {
                    foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            isExistFile = true;
                            continue;
                        }
                    }

                    if (isExistFile == false)
                        dirInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn외화획득명세서_Click(object sender, EventArgs e)
        {
            DataTable dt1, dt2;
            DataRow[] dataRowArray, dataRowArray1;
            ReportHelper reportHelper;

            string 매출번호, 거래처코드, 매출처, 매출처주소, 매출처국가, 호선번호, 호선명, 영업조직명, 영업조직장, 서명, 창봉투매출처, 창봉투주소, 창봉투전화번호, 창봉투이메일, 창봉투비고, 국가코드, IMO번호, 대표자명, query;

            try
            {
                if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow)
                    return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

                    int index = 0;

                    foreach (DataRow dr in dataRowArray)
					{
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        dt1 = this._flexH.DataTable.Clone();
                        dt2 = this._flexL.DataTable.Clone();

                        dataRowArray1 = this._flexL.DataTable.Select("NO_IV = '" + dr["NO_IV"].ToString() + "'");

                        foreach (DataRow dr1 in dataRowArray1)
                        {
                            dt2.ImportRow(dr1);

                            if (!string.IsNullOrEmpty(dr1["NM_ITEM_SOL"].ToString()))
                                dt2.Rows[dt2.Rows.Count - 1]["NM_ITEM_PARTNER"] = dr1["NM_ITEM_SOL"].ToString();

                            if (!string.IsNullOrEmpty(dr1["DT_IV"].ToString()))
                                dt2.Rows[dt2.Rows.Count - 1]["DT_LOADING"] = dr1["DT_IV"].ToString();

                            dt2.Rows[dt2.Rows.Count - 1]["DT_TAX"] = Util.GetToDatePrint(dr1["DT_TAX"]);
                            dt2.Rows[dt2.Rows.Count - 1]["DT_SO"] = Util.GetToDatePrint(dr1["DT_SO"]);
                            dt2.Rows[dt2.Rows.Count - 1]["DT_LOADING"] = Util.GetToDatePrint(dr1["DT_LOADING"]);
                        }

                        dt1.ImportRow(dr);

                        reportHelper = Util.SetRPT("R_CZ_SA_IVMNG_1", "매출관리-INVOICE", Global.MainFrame.LoginInfo.CompanyCode, dt1, dt2);

                        reportHelper.SetDataTable(dt1, 1);
                        reportHelper.SetDataTable(dt2, 2);

                        매출번호 = dr["NO_IV"].ToString();
                        거래처코드 = string.Empty;
                        매출처 = string.Empty;
                        매출처주소 = string.Empty;
                        매출처국가 = string.Empty;
                        호선번호 = string.Empty;
                        호선명 = string.Empty;
                        영업조직명 = string.Empty;
                        영업조직장 = string.Empty;
                        서명 = string.Empty;
                        창봉투매출처 = string.Empty;
                        창봉투주소 = string.Empty;
                        창봉투전화번호 = string.Empty;
                        창봉투이메일 = string.Empty;
                        창봉투비고 = string.Empty;
                        국가코드 = string.Empty;
                        IMO번호 = string.Empty;
                        대표자명 = string.Empty;

                        if (this.중복확인(dt1, "CD_PARTNER", "매출처", ref 거래처코드) == false) return;

                        if (Settings.Default.주소표시 == "자동")
                        {
                            #region 자동
                            query = @"SELECT COUNT(1) 
                                      FROM MA_PARTNER WITH(NOLOCK)" + Environment.NewLine +
                                     "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                     "AND CD_PARTNER = '" + 거래처코드 + "'" + Environment.NewLine +
                                     "AND FG_PARTNER IN ('100', '300')"; //선주사, 선박관리사

                            if (D.GetDecimal(Global.MainFrame.ExecuteScalar(query)) > 0)
                            {
                                if (this.중복확인(dt2, "INVOICE_COMPANY", "매출처", ref 매출처) == false) return;

                                if (string.IsNullOrEmpty(매출처))
                                {
                                    if (this.중복확인(dt1, "TO_COMPANY", "매출처", ref 매출처) == false) return;
                                    if (this.중복확인(dt1, "TO_ADRESS", "매출처주소", ref 매출처주소) == false) return;

                                    if (this.중복확인(dt1, "TO_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                                    if (this.중복확인(dt1, "TO_ADRESS", "창봉투주소", ref 창봉투주소) == false) return;
                                    if (this.중복확인(dt1, "TO_TEL1", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                                    if (this.중복확인(dt1, "TO_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                                    if (this.중복확인(dt1, "TO_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                                    if (this.중복확인(dt1, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                                }
                                else
                                {
                                    if (this.중복확인(dt2, "INVOICE_ADDRESS", "매출처주소", ref 매출처주소) == false) return;

                                    if (this.중복확인(dt2, "INVOICE_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                                    if (this.중복확인(dt2, "INVOICE_ADDRESS", "창봉투주소", ref 창봉투주소) == false) return;
                                    if (this.중복확인(dt2, "INVOICE_TEL", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                                    if (this.중복확인(dt2, "INVOICE_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                                    if (this.중복확인(dt2, "INVOICE_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                                    if (this.중복확인(dt2, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                                }
                            }
                            else
                            {
                                if (this.중복확인(dt1, "TO_COMPANY", "매출처", ref 매출처) == false) return;
                                if (this.중복확인(dt1, "TO_ADRESS", "매출처주소", ref 매출처주소) == false) return;

                                if (this.중복확인(dt1, "TO_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                                if (this.중복확인(dt1, "TO_ADRESS", "창봉투주소", ref 창봉투주소) == false) return;
                                if (this.중복확인(dt1, "TO_TEL1", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                                if (this.중복확인(dt1, "TO_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                                if (this.중복확인(dt1, "TO_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                                if (this.중복확인(dt1, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                            }
                            #endregion
                        }
                        else if (Settings.Default.주소표시 == "매출처주소")
                        {
                            #region 매출처주소
                            if (this.중복확인(dt1, "TO_COMPANY", "매출처", ref 매출처) == false) return;
                            if (this.중복확인(dt1, "TO_ADRESS", "매출처주소", ref 매출처주소) == false) return;

                            if (this.중복확인(dt1, "TO_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                            if (this.중복확인(dt1, "TO_ADRESS", "창봉투주소", ref 창봉투주소) == false) return;
                            if (this.중복확인(dt1, "TO_TEL1", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                            if (this.중복확인(dt1, "TO_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                            if (this.중복확인(dt1, "TO_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                            if (this.중복확인(dt1, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                            #endregion
                        }
                        else if (Settings.Default.주소표시 == "계산서주소")
                        {
                            #region 계산서주소
                            if (this.중복확인(dt2, "INVOICE_COMPANY", "매출처", ref 매출처) == false) return;
                            if (this.중복확인(dt2, "INVOICE_ADDRESS", "매출처주소", ref 매출처주소) == false) return;

                            if (this.중복확인(dt2, "INVOICE_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                            if (this.중복확인(dt2, "INVOICE_ADDRESS", "창봉투주소", ref 창봉투주소) == false) return;
                            if (this.중복확인(dt2, "INVOICE_TEL", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                            if (this.중복확인(dt2, "INVOICE_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                            if (this.중복확인(dt2, "INVOICE_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                            if (this.중복확인(dt2, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                            #endregion
                        }

                        if (this.중복확인(dt1, "NM_NATION", "매출처국가", ref 매출처국가) == false) return;
                        if (this.중복확인(dt1, "FROM_NAME", "대표자명", ref 대표자명) == false) return;

                        dataRowArray1 = dt2.Select("ISNULL(NO_HULL, '') <> ''");
                        if (dataRowArray1.Length > 0)
                            호선번호 = D.GetString(dataRowArray1[0]["NO_HULL"]);

                        dataRowArray1 = dt2.Select("ISNULL(NM_VESSEL, '') <> ''");
                        if (dataRowArray1.Length > 0)
                            호선명 = D.GetString(dataRowArray1[0]["NM_VESSEL"]);

                        dataRowArray1 = dt2.Select("ISNULL(NO_IMO, '') <> ''");
                        if (dataRowArray1.Length > 0)
                            IMO번호 = D.GetString(dataRowArray1[0]["NO_IMO"]);

                        dataRowArray1 = dt2.Select("ISNULL(EN_SALEORG, '') <> ''");
                        if (dataRowArray1.Length > 0)
                            영업조직명 = D.GetString(dataRowArray1[0]["EN_SALEORG"]);

                        dataRowArray1 = dt2.Select("ISNULL(NM_ENG, '') <> ''");
                        if (dataRowArray1.Length > 0)
                            영업조직장 = D.GetString(dataRowArray1[0]["NM_ENG"]);

                        dataRowArray1 = dt2.Select("ISNULL(DC_SIGN, '') <> ''");
                        if (dataRowArray1.Length > 0)
                            서명 = D.GetString(dataRowArray1[0]["DC_SIGN"]);

                        서명 = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 서명;
                        
                        reportHelper.SetData("인보이스번호", 매출번호);
                        reportHelper.SetData("매출처명", 매출처);
                        reportHelper.SetData("매출처주소", 매출처주소);
                        reportHelper.SetData("매출처국가", 매출처국가);
                        reportHelper.SetData("호선번호", 호선번호);
                        reportHelper.SetData("호선명", 호선명);
                        reportHelper.SetData("영업조직명", 영업조직명);
                        reportHelper.SetData("영업조직장", 영업조직장);
                        reportHelper.SetData("서명", 서명);
                        reportHelper.SetData("창봉투매출처", 창봉투매출처);
                        reportHelper.SetData("창봉투주소", 창봉투주소);
                        reportHelper.SetData("창봉투전화번호", 창봉투전화번호);
                        reportHelper.SetData("창봉투이메일", 창봉투이메일);
                        reportHelper.SetData("창봉투비고", 창봉투비고);
                        reportHelper.SetData("스탬프표시", Settings.Default.스탬프표시);
                        reportHelper.SetData("로고표시", Settings.Default.로고표시);
                        reportHelper.SetData("서명표시", Settings.Default.서명표시);
                        reportHelper.SetData("대표자명", 대표자명);

                        reportHelper.유저폰트사용();

                        if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                            reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K100_A_INV_ADM.DRF", false, true, folderBrowserDialog.SelectedPath + "\\" + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());
                        else
                            reportHelper.PrintDirect("R_CZ_SA_IVMNG_1_K200_A_INV_ADM.DRF", false, true, folderBrowserDialog.SelectedPath + "\\" + 매출번호 + "_TMP.pdf", new Dictionary<string, string>());

                        PDF.ExtractPageRange(folderBrowserDialog.SelectedPath + "\\" + 매출번호 + "_TMP.pdf", folderBrowserDialog.SelectedPath + "\\" + 매출번호 + ".pdf", 1, 2);

                        File.Delete(folderBrowserDialog.SelectedPath + "\\" + 매출번호 + "_TMP.pdf");
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn외화획득명세서.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void Btn발송용저장_Click(object sender, EventArgs e)
        {
            DataTable dt;
            string query, 매출번호, filePath;

            try
            {
                if (string.IsNullOrEmpty(this.txt계산서번호.Text))
				{
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계산서번호.Text);
                    return;
				}

                query = @"SELECT MF.FILE_NAME,
	   MF.FILE_PATH
FROM MA_FILEINFO MF WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_MODULE = 'CZ'
AND ID_MENU = 'P_CZ_SA_IV_MNG'
AND CD_FILE = '{1}'";

                매출번호 = this.txt계산서번호.Text;

                filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + 매출번호 + "견적발송용" + "\\";
                if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath);

                    foreach (string file in files)
                        File.Delete(file);
                }
                else
                    Directory.CreateDirectory(filePath);

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 매출번호));

                foreach (DataRow dataRow in dt.Rows)
                {
                    FileUploader.DownloadFile(dataRow["FILE_NAME"].ToString(), filePath, dataRow["FILE_PATH"].ToString(), 매출번호);
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn발송용저장.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.Redraw = false;

            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_ST_STAT", "결재상태", 80, false);
			this._flexH.SetCol("NO_IV", "계산서번호", 100, false);
            this._flexH.SetCol("DT_PROCESS", "발생일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("CD_PARTNER", "매출처코드", false);
            this._flexH.SetCol("LN_PARTNER", "매출처명", 120, false);
            this._flexH.SetCol("SN_PARTNER", "매출처약칭", false);
            this._flexH.SetCol("NO_BIZAREA", "사업자번호", false);
            this._flexH.SetCol("NM_CON", "휴폐업구분", false);
            this._flexH.SetCol("NM_TRANS", "거래구분", 80, false);
            this._flexH.SetCol("NM_TAX", "VAT구분", 100, false);
            this._flexH.SetCol("CD_EXCH_NAME", "통화명", 60, false);
            this._flexH.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM_K", "공급가액", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("VAT_TAX", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SUM", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_RCP_A", "선수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("ST_RPA", "RPA 실행상태", 100);
            this._flexH.SetCol("DC_RPA", "RPA 로그", 100);

            if (Certify.IsLive())
            {
                this._flexH.SetCol("AM_BAN", "반제금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_JAN", "잔액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            }

            this._flexH.SetCol("AM_EX", "마감금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("VAT_TAX_EX", "부가세(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SUM_EX", "합계(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_RCP_A_EX", "선수금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            if (Certify.IsLive())
            {
                this._flexH.SetCol("AM_BAN_EX", "반제금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_JAN_EX", "잔액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            }
            
            this._flexH.SetCol("NM_KOR", "담당자", 60, true);
            this._flexH.SetCol("NM_DEPT", "마감부서", 100, false);
            this._flexH.SetCol("TP_AIS", "처리상태", 60, false);
            this._flexH.SetCol("CD_DOCU", "전표유형", 80, true);

            if (Certify.IsLive())
            {
                this._flexH.SetCol("NO_DOCU", "전표번호", 120, false);
                this._flexH.SetCol("YN_AUTO", "자동발행", 60);
                this._flexH.SetCol("NO_DOCU_GRP", "통합전표번호", false);
                this._flexH.SetCol("NM_BILL", "결제구분", 60, false);
                this._flexH.SetCol("DTS_INSERT", "입력일시", 120, false);
                this._flexH.SetCol("DC_REMARK", "비고", 150, false);
                this._flexH.SetCol("CD_PC", "회계단위", 60, false);
                this._flexH.SetCol("CD_BIZAREA", "사업장", 120, false);
                this._flexH.SetCol("CD_BIZAREA_TAX", "부가세사업장", 120, false);
                this._flexH.SetCol("BILL_PARTNER", "수금처코드", false);
                this._flexH.SetCol("BILL_LN_PARTNER", "수금처명", 120, false);
                this._flexH.SetCol("DT_RCP_RSV", "수금예정일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("DT_RCP_PRETOLERANCE", "수금예정허용일(매출처)", 110, false, typeof(decimal), FormatTpType.MONEY);
                this._flexH.SetCol("DT_RCP_RSV1", "최종수금예정허용일", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("NO_VOLUME", "권", 40, true);
                this._flexH.SetCol("NO_HO", "호", 40, true);
                this._flexH.SetCol("NO_SEQ", "일련번호", 120, true);
                this._flexH.SetCol("ETAX_TOT_TYPE", "전송종류", 100, false);
                this._flexH.SetCol("ETAX_TOT_KEY", "전자계산서번호", 100, false);
                this._flexH.SetCol("ETAX_TOT_ST", "전송상태", 100, false);
                this._flexH.SetCol("ERR_MSG", "전송MSG", 100, false);
                this._flexH.SetCol("TP_RECEIPT", "청구/영수", 80, true);
                this._flexH.SetCol("ETAX_SEND_TYPE", "발행구분", 80, true);
                this._flexH.SetCol("ETAX_SELL_DAM_NM", "공급자", 80, true);
                this._flexH.SetCol("ETAX_SELL_DAM_MOBIL", "공급자핸드폰", 100, true);
                this._flexH.SetCol("ETAX_SELL_DAM_EMAIL", "공급자이메일", 100, true);
                this._flexH.SetCol("NM_PTR", "매출처담당자", 80, true);
                this._flexH.SetCol("EX_EMIL", "매출처담당자이메일", 100, true);
                this._flexH.SetCol("EX_HP", "매출처담당자핸드폰", 100, true);
                this._flexH.SetCol("NM_BUSINESS", "사업자유형", 60, false);
                this._flexH.SetCol("DT_CLOSE", "폐업일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("FG_AR_EXC", "채권에외대상", 100, false, CheckTypeEnum.Y_N);
                this._flexH.SetCol("NO_ISS", "국세청승인번호", 180, false);
                this._flexH.SetCol("ETAX_KEY_ORIGINAL", "원인세금계산서번호", 120, false);
                this._flexH.SetCol("NO_ISS_SRC", "원인국세청승인번호", 180, false);

                this._flexH.Cols["CD_PC"].Visible = false;
            }

            this._flexH.SetDataMap("CD_DOCU", this.GetComboDataCombine("N;FI_J000002"), "CODE", "NAME");

            this._flexH.Cols["NO_BIZAREA"].Format = "###-##-#####";
            this._flexH.SetStringFormatCol("NO_BIZAREA");
            
            this._flexH.SetCodeHelpCol("NM_KOR", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[3] { "CD_DEPT", "NO_EMP", "NM_KOR" }, new string[3] { "CD_DEPT", "NO_EMP", "NM_KOR" });
            this._flexH.NewRowEditable = false;
            this._flexH.EnterKeyAddRow = false;
            this._flexH.SetDummyColumn(new string[] { "S" });
            
            this._flexH.SettingVersion = "1.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            
            this._flexH.SetExceptSumCol(new string[2] { "RT_EXCH", "DT_RCP_PRETOLERANCE" });
            
            this._flexH.Redraw = true;
            this._flexH.CellNoteInfo.EnabledCellNote = true;
            this._flexH.CellNoteInfo.CategoryID = this.Name;
            this._flexH.CellNoteInfo.DisplayColumnForDefaultNote = "NO_IV";
            this._flexH.CheckPenInfo.EnabledCheckPen = true;
            this._flexH.UseMultySorting = true;
            #endregion

            #region Line
            this._flexL.Redraw = false;

            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_IO", "출고번호", 100);
            this._flexL.SetCol("NM_TP_IV", "매출형태", 100);
            this._flexL.SetCol("NM_PLANT", "출고공장", false);
            this._flexL.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_BL", "선적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("CD_ITEM", "품목코드", 80);
            this._flexL.SetCol("NM_ITEM", "품목명", 110);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 110);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 110);
            this._flexL.SetCol("UNIT_IM", "단위", 50);
            this._flexL.SetCol("STND_ITEM", "규격", false);            
            this._flexL.SetCol("QT_GI_CLS", "매출수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_ITEM_CLS", "매출단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_CLS", "공급가액", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_SUM", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("UM_EX", "매출단가(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_EX", "매출금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("VAT_EX", "부가세(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_SUM_EX", "합계(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NO_SO", "수주번호", 90);
            this._flexL.SetCol("NM_PROJECT", "프로젝트", 110);
            this._flexL.SetCol("NO_PO_PARTNER", "주문번호", 110);
            this._flexL.SetCol("NM_VESSEL", "호선명", 110);
            this._flexL.SetCol("NM_SALEORG", "영업조직명", 110);
            this._flexL.SetCol("NM_SALEGRP", "영업그룹명", 110);
            this._flexL.SetCol("CD_CC", "C/C코드", false);
            this._flexL.SetCol("NM_CC", "C/C명", 120);
            this._flexL.SetCol("DC_RMK_IV", "비고", 120);
            this._flexL.SetCol("NM_MNGD1", "관리내역1", false);
            this._flexL.SetCol("NM_MNGD2", "관리내역2", false);
            this._flexL.SetCol("NM_MNGD3", "관리내역3", false);
            this._flexL.SetCol("UD_NM_04", "관리내역4", false);
            this._flexL.SetCol("WEIGHT", "품목중량", false);
            this._flexL.SetCol("QT_WEIGHT", "출고중량", false);
            this._flexL.SetCol("QTIO_DC_RMK", "출고비고", 120);

            if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
            {
                this._flexL.SetCol("CD_ITEM_REF", "SET품목", false);
                this._flexL.SetCol("NM_ITEM_REF", "SET품명", false);
                this._flexL.SetCol("STND_ITEM_REF", "SET규격", false);
            }

            if (BASIC.GetMAEXC("배차사용유무") == "Y")
                this._flexL.SetCol("YN_PICKING", "배송여부", false);

            this._flexL.SetCol("CLS_ITEM", "품목계정", false);
            this._flexL.SetCol("FG_USE2", "수주용도2", false);

            this._flexL.AllowSorting = AllowSortingEnum.MultiColumn;
            this._flexL.NewRowEditable = false;
            this._flexL.EnterKeyAddRow = false;
            
            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            
            this._flexL.SetExceptSumCol(new string[2] { "UM_ITEM_CLS", "UM_EX" });
            this._flexL.AllowMerging = AllowMergingEnum.FixedOnly;
            this.SetUserGrid(this._flexL);
            this._flexL.Redraw = true;
            this._flexL.Rows[1].AllowMerging = true;
            this._flexL.UseMultySorting = true;
            #endregion
        }

        private void InitStyle()
        {
            this._flexH.Styles.Add("처리").BackColor = Color.Yellow;
            this._flexH.Styles.Add("일반").BackColor = Color.White;
        }
        #endregion

        #region 메인버튼
        private void CheckedRowDelete(FlexGrid _flex)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                int row = _flex.FindRow("Y", _flex.Rows.Fixed, _flex.Cols["S"].Index, false, true, true);
                object obj;

                if (row < 1)
                {
                    if (_flex.Name.ToString() == "_flexH" && this._flexL.HasNormalRow)
                    {
                        this._flexL.Redraw = false;
                        this._flexL.RemoveViewAll();
                        this._flexL.Redraw = true;
                        Application.DoEvents();
                    }

                    SpInfo spInfo = new SpInfo();
                    spInfo.SpNameSelect = "SP_SA_IV_CHECK_SELECT";
                    spInfo.SpParamsSelect = new object[] { this.LoginInfo.CompanyCode, 
                                                            _flex[_flex.Row, "NO_IV"].ToString().Trim() };
                    obj = this.FillDataTable(spInfo);
                    _flex.Rows.Remove(_flex.Row);
                }
                else
                {
                    int num = row;
                    while (num > 0)
                    {
                        if (_flex[num, "S"].ToString() == "Y")
                        {
                            _flex.Select(num, 1);
                            if (_flex.Name.ToString() == "_flexH" && this._flexL.HasNormalRow)
                            {
                                this._flexL.Redraw = false;
                                this._flexL.RemoveViewAll();
                                this._flexL.Redraw = true;
                                Application.DoEvents();
                            }

                            SpInfo spInfo = new SpInfo();
                            spInfo.SpNameSelect = "SP_SA_IV_CHECK_SELECT";
                            spInfo.SpParamsSelect = new object[] { this.LoginInfo.CompanyCode, 
                                                                 _flex[num, "NO_IV"].ToString().Trim() };
                            obj = this.FillDataTable(spInfo);
                            _flex.Rows.Remove(num);
                            num = _flex.FindRow("Y", num, _flex.Cols["S"].Index, false, true, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                DataTable dt = null;

                int 기간 = Math.Abs((this.dtp처리일자.StartDate - this.dtp처리일자.EndDate).Days);

                string startDate, endDate;
                DataTable tmpDataTable;

                for (int i = 0; i <= 기간 / 365; i++)
                {
                    startDate = this.dtp처리일자.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

                    if (기간 >= (i + 1) * 365)
                        endDate = this.dtp처리일자.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
                    else
                        endDate = this.dtp처리일자.EndDateToString;

                    MsgControl.ShowMsg("조회 중 입니다. 잠시만 기다려 주세요.\n조회기간 (@ ~ @)", new string[] { Util.GetTo_DateStringS(startDate), Util.GetTo_DateStringS(endDate) });

                    tmpDataTable = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   startDate,
                                                                   endDate,
                                                                   this.ctx사업장.CodeValue,
                                                                   this.cbo처리상태.SelectedValue,
                                                                   this.ctx매출처.CodeValue,
                                                                   this.cbo거래구분.SelectedValue,
                                                                   this.ctx마감부서.CodeValue,
                                                                   this.ctx마감담당.CodeValue,
                                                                   this.ctx부가세사업장.CodeValue,
                                                                   this.cbo전자세금계산서.SelectedValue,
                                                                   this.cbo휴폐업구분.SelectedValue,
                                                                   this.bpc영업조직.QueryWhereIn_Pipe,
                                                                   this.bpc영업그룹.QueryWhereIn_Pipe,
                                                                   this.cbo부가세구분.SelectedValue,
                                                                   this.cbo매출처그룹.SelectedValue,
                                                                   this.cbo매출처그룹2.SelectedValue,
                                                                   this.txt프로젝트.Text,
                                                                   this.cbo공장.SelectedValue,
                                                                   this.txt계산서번호.Text,
                                                                   this.txt주문번호.Text,
                                                                   this.ctx호선번호.CodeValue,
                                                                   this.전표승인여부 });

                    if (i == 0)
                        dt = tmpDataTable;
                    else
                        dt.Merge(tmpDataTable);
                }

                dt.DefaultView.Sort = "DT_PROCESS, CD_PARTNER, NO_IV DESC";

                this._flexH.Binding = dt;

                if (!_flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                else
                {
					this.btn전자결재.Enabled = false;
                    this.btnINVOICE.Enabled = true;
                    this.btn거래명세표.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            bool flag = false;
            DialogResult dialogResult = DialogResult.No;
            
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S ='Y' AND ETAX_TOT_ST <> '' AND (ETAX_TOT_ST = '0' OR ETAX_TOT_ST = '1' OR ETAX_TOT_ST = '2' OR ETAX_TOT_ST = '01' OR ETAX_TOT_ST = '02' OR ETAX_TOT_ST = 'xx' OR ETAX_TOT_ST = 'xy') ");
                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("전자세금계산서발행건중 요청/발행/승인/서면 처리된건은 삭제할수없습니다!");
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S ='Y' AND TP_AIS = 'N'");
                    if (dataRowArray2 == null || dataRowArray2.Length <= 0)
                    {
                        this.ShowMessage("전표처리 되었거나 체크박스가 선택되지 않았습니다.");
                    }
                    else
                    {
                        if (!flag) dialogResult = this.ShowMessage("MA_M000016", "QY2");

                        if (dialogResult == DialogResult.Yes)
                        {
                            this.CheckedRowDelete(this._flexH);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                this._flexH.Redraw = true;
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            if (this._flexH.IsDataChanged == false &&
                this._flexL.IsDataChanged == false) return false;

            for (int @fixed = this._flexH.Rows.Fixed; @fixed <= this._flexH.Rows.Count - 1; ++@fixed)
            {
                if (this._flexH.RowState(@fixed) != DataRowState.Unchanged)
                    this._flexH[@fixed, "ID_UPDATE"] = this.LoginInfo.UserID;
            }

            for (int @fixed = this._flexL.Rows.Fixed; @fixed <= this._flexL.Rows.Count - 1; ++@fixed)
            {
                if (this._flexL.RowState(@fixed) != DataRowState.Unchanged)
                    this._flexL[@fixed, "ID_UPDATE"] = this.LoginInfo.UserID;
            }

            DataTable changes1 = this._flexH.GetChanges();
            DataTable changes2 = this._flexL.GetChanges();

            if (changes1 != null && changes1.Rows != null && changes1.Rows.Count > 0)
            {
                for (int index = 0; index < changes1.Rows.Count; ++index)
                {
                    SpInfo spinfo = new SpInfo();
                    spinfo.SpNameSelect = "SP_SA_IV_CHECK_SELECT";
                    object obj;

                    if (changes1.Rows[index].RowState == DataRowState.Deleted)
                    {
                        changes1.Rows[index].RejectChanges();
                        spinfo.SpParamsSelect = new object[] { this.LoginInfo.CompanyCode,
                                                               changes1.Rows[index]["NO_IV"].ToString().Trim() };
                        obj = this.FillDataTable(spinfo);
                        changes1.Rows[index].AcceptChanges();
                        changes1.Rows[index].Delete();
                    }
                    else
                    {
                        spinfo.SpParamsSelect = new object[] { this.LoginInfo.CompanyCode,
                                                               changes1.Rows[index]["NO_IV"].ToString().Trim() };
                        obj = this.FillDataTable(spinfo);
                    }
                }
            }

            if (!_biz.Save(changes1, changes2)) return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
			DataSet ds;
			DataTable dt1, dt2, dt3, dt4, dt5;
			DataRow[] dataRowArray;
			DataRow header;
			string 멀티출고번호;

			try
			{
                base.OnToolBarPrintButtonClicked(sender, e);

                header = this._flexH.GetDataRow(this._flexH.Row);

				if (string.IsNullOrEmpty(header["NO_DOCU"].ToString()))
				{
					this.ShowMessage("CZ_전표 처리되지 않은 건이 포함되어 있습니다.");
					return;
				}

				ds = this._biz.전표출력(header["CD_PC"].ToString(), header["NO_DOCU"].ToString());

				dt1 = ds.Tables[0];
				dt2 = ds.Tables[1];

				dt1.Columns.Add("NM_VESSEL");
				dt1.Columns.Add("DT_LOADING");
				dt1.Columns.Add("NM_LOG_EMP");

				dataRowArray = this._flexL.DataTable.Select("NO_IV = '" + D.GetString(header["NO_IV"]) + "'");

				멀티출고번호 = string.Empty;

				dt3 = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_IO" }, true);

				if (dt3.Rows.Count == 1)
					멀티출고번호 = D.GetString(dt3.Rows[0]["NO_IO"]);
				else
					멀티출고번호 = D.GetString(dt3.Rows[0]["NO_IO"]) + " 외 " + (dt3.Rows.Count - 1) + "건";

				dt1.Rows[0]["NO_IO"] = 멀티출고번호;

				dt4 = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NM_VESSEL", "DT_LOADING", "NM_LOG_EMP" }, true);

				dt1.Rows[dt1.Rows.Count - 1]["NM_VESSEL"] = D.GetString(dt4.Rows[0]["NM_VESSEL"]);
				dt1.Rows[dt1.Rows.Count - 1]["DT_LOADING"] = D.GetString(dt4.Rows[0]["DT_LOADING"]);
				dt1.Rows[dt1.Rows.Count - 1]["NM_LOG_EMP"] = D.GetString(dt4.Rows[0]["NM_LOG_EMP"]);

				dt5 = ComFunc.getGridGroupBy(dt2, new string[] { "NO_DOCU", "CD_ACCT", "NM_ACCT" }, true);
				dt5.Columns.Add("AM_DR");
				dt5.Columns.Add("AM_CR");

				foreach (DataRow dr1 in dt5.Rows)
				{
					string filter = "NO_DOCU = '" + D.GetString(dr1["NO_DOCU"]) + "' AND CD_ACCT = '" + D.GetString(dr1["CD_ACCT"]) + "'";
					dr1["AM_DR"] = dt2.Compute("SUM(AM_DR)", filter);
					dr1["AM_CR"] = dt2.Compute("SUM(AM_CR)", filter);
				}

				this._gw.미리보기(header, dt1, dt2, dt5);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.임시파일제거();
                return base.OnToolBarExitButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region 버튼 이벤트
        private void btn전자결재_Click(object sender, EventArgs e)
		{
			DataSet ds;
			DataTable dt1, dt2, dt3, dt4, dt5;
			DataRow[] dataRowArray;
			DataRow header;
			string 멀티출고번호;

			try
			{
			    header = this._flexH.GetDataRow(this._flexH.Row);

				if (Global.MainFrame.ShowMessage(header["NO_IV"].ToString() + "건 전자결재 진행 하시겠습니까?", "QY2") != DialogResult.Yes) return;

				if (string.IsNullOrEmpty(header["NO_DOCU"].ToString()))
				{
					this.ShowMessage("CZ_전표 처리되지 않은 건이 포함되어 있습니다.");
					return;
				}

				ds = this._biz.전표출력(header["CD_PC"].ToString(), header["NO_DOCU"].ToString());

				dt1 = ds.Tables[0];
				dt2 = ds.Tables[1];

				dt1.Columns.Add("NM_VESSEL");
				dt1.Columns.Add("DT_LOADING");
				dt1.Columns.Add("NO_LOG_EMP");
				dt1.Columns.Add("NM_LOG_EMP");

				dataRowArray = this._flexL.DataTable.Select("NO_IV = '" + D.GetString(header["NO_IV"]) + "'");

				멀티출고번호 = string.Empty;

				dt3 = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_IO" }, true);

				if (dt3.Rows.Count == 1)
					멀티출고번호 = D.GetString(dt3.Rows[0]["NO_IO"]);
				else
					멀티출고번호 = D.GetString(dt3.Rows[0]["NO_IO"]) + " 외 " + (dt3.Rows.Count - 1) + "건";

				dt1.Rows[0]["NO_IO"] = 멀티출고번호;

				dt4 = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NM_VESSEL", "DT_LOADING", "NO_EMP", "NM_LOG_EMP" }, true);

				dt1.Rows[dt1.Rows.Count - 1]["NM_VESSEL"] = D.GetString(dt4.Rows[0]["NM_VESSEL"]);
				dt1.Rows[dt1.Rows.Count - 1]["DT_LOADING"] = D.GetString(dt4.Rows[0]["DT_LOADING"]);
				dt1.Rows[dt1.Rows.Count - 1]["NO_LOG_EMP"] = D.GetString(dt4.Rows[0]["NO_EMP"]);
				dt1.Rows[dt1.Rows.Count - 1]["NM_LOG_EMP"] = D.GetString(dt4.Rows[0]["NM_LOG_EMP"]);

				dt5 = ComFunc.getGridGroupBy(dt2, new string[] { "NO_DOCU", "CD_ACCT", "NM_ACCT" }, true);
				dt5.Columns.Add("AM_DR");
				dt5.Columns.Add("AM_CR");

				foreach (DataRow dr1 in dt5.Rows)
				{
					string filter = "NO_DOCU = '" + D.GetString(dr1["NO_DOCU"]) + "' AND CD_ACCT = '" + D.GetString(dr1["CD_ACCT"]) + "'";
					dr1["AM_DR"] = dt2.Compute("SUM(AM_DR)", filter);
					dr1["AM_CR"] = dt2.Compute("SUM(AM_CR)", filter);
				}

				if (this._gw.전자결재(header, dt1, dt2, dt5))
				{
					ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
				}
			}
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
		}

		private void btn출력옵션_Click(object sender, EventArgs e)
        {
            P_CZ_SA_IVMNG_OPTION_SUB 출력옵션화면 = new P_CZ_SA_IVMNG_OPTION_SUB();
            출력옵션화면.ShowDialog();
        }

        private void btnINVOICE_Click(object sender, EventArgs e)
        {
            string query, 거래처코드, 매출처, 매출처주소, 매출처국가, 호선번호, 호선명, 영업조직명, 영업조직장, 서명, 창봉투매출처, 창봉투주소, 창봉투전화번호, 창봉투이메일, 창봉투비고, 인보이스번호, 국가코드, IMO번호, 대표자명;
            DataTable dt1, dt2;
            DataRow[] dataRowArray2;
            ReportHelper reportHelper;

            try
            {
                if (this._flexH.DataView == null) return;

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow)
                        return;

                    dt1 = this._flexH.DataTable.Clone();
                    dt2 = this._flexL.DataTable.Clone();

                    foreach (DataRow dr in dataRowArray1)
                    {
                        dataRowArray2 = this._flexL.DataTable.Select("NO_IV = '" + dr["NO_IV"].ToString() + "'");

                        foreach (DataRow dr1 in dataRowArray2)
                        {
                            dt2.ImportRow(dr1);

                            if (!string.IsNullOrEmpty(dr1["NM_ITEM_SOL"].ToString()))
                                dt2.Rows[dt2.Rows.Count - 1]["NM_ITEM_PARTNER"] = dr1["NM_ITEM_SOL"].ToString();

                            if (!string.IsNullOrEmpty(dr1["DT_IV"].ToString()))
                                dt2.Rows[dt2.Rows.Count - 1]["DT_LOADING"] = dr1["DT_IV"].ToString();

                            dt2.Rows[dt2.Rows.Count - 1]["DT_TAX"] = Util.GetToDatePrint(dr1["DT_TAX"]);
                            dt2.Rows[dt2.Rows.Count - 1]["DT_SO"] = Util.GetToDatePrint(dr1["DT_SO"]);
                            dt2.Rows[dt2.Rows.Count - 1]["DT_LOADING"] = Util.GetToDatePrint(dr1["DT_LOADING"]);
                        }

                        dt1.ImportRow(dr);
                    }

                    reportHelper = Util.SetRPT("R_CZ_SA_IVMNG_1", "매출관리-INVOICE", Global.MainFrame.LoginInfo.CompanyCode, dt1, dt2);

                    reportHelper.SetDataTable(dt1, 1);
                    reportHelper.SetDataTable(dt2, 2);

                    거래처코드 = string.Empty;
                    매출처 = string.Empty;
                    매출처주소 = string.Empty;
                    매출처국가 = string.Empty;
                    호선번호 = string.Empty;
                    호선명 = string.Empty;
                    영업조직명 = string.Empty;
                    영업조직장 = string.Empty;
                    서명 = string.Empty;
                    창봉투매출처 = string.Empty;
                    창봉투주소 = string.Empty;
                    창봉투전화번호 = string.Empty;
                    창봉투이메일 = string.Empty;
                    창봉투비고 = string.Empty;
                    국가코드 = string.Empty;
                    IMO번호 = string.Empty;
                    대표자명 = string.Empty;

                    if (this.중복확인(dt1, "CD_PARTNER", "매출처", ref 거래처코드) == false) return;

                    if (Settings.Default.주소표시 == "자동")
                    {
                        #region 자동
                        query = @"SELECT COUNT(1) 
                                  FROM MA_PARTNER WITH(NOLOCK)" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_PARTNER = '" + 거래처코드 + "'" + Environment.NewLine +
                                 "AND FG_PARTNER IN ('100', '300')"; //선주사, 선박관리사

                        if (D.GetDecimal(Global.MainFrame.ExecuteScalar(query)) > 0)
                        {
                            if (this.중복확인(dt2, "INVOICE_COMPANY", "매출처", ref 매출처) == false) return;

                            if (string.IsNullOrEmpty(매출처))
                            {
                                if (this.중복확인(dt1, "TO_COMPANY", "매출처", ref 매출처) == false) return;
                                if (this.중복확인(dt1, "TO_ADRESS", "매출처주소", ref 매출처주소) == false) return;

                                if (this.중복확인(dt1, "TO_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                                if (this.중복확인(dt1, "TO_ADRESS", "창봉투주소", ref 창봉투주소) == false) return;
                                if (this.중복확인(dt1, "TO_TEL1", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                                if (this.중복확인(dt1, "TO_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                                if (this.중복확인(dt1, "TO_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                                if (this.중복확인(dt1, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                            }
                            else
                            {
                                if (this.중복확인(dt2, "INVOICE_ADDRESS", "매출처주소", ref 매출처주소) == false) return;

                                if (this.중복확인(dt2, "INVOICE_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                                if (this.중복확인(dt2, "INVOICE_ADDRESS", "창봉투주소", ref 창봉투주소) == false) return;
                                if (this.중복확인(dt2, "INVOICE_TEL", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                                if (this.중복확인(dt2, "INVOICE_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                                if (this.중복확인(dt2, "INVOICE_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                                if (this.중복확인(dt2, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                            }
                        }
                        else
                        {
                            if (this.중복확인(dt1, "TO_COMPANY", "매출처", ref 매출처) == false) return;
                            if (this.중복확인(dt1, "TO_ADRESS", "매출처주소", ref 매출처주소) == false) return;

                            if (this.중복확인(dt1, "TO_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                            if (this.중복확인(dt1, "TO_ADRESS", "창봉투주소", ref 창봉투주소) == false) return;
                            if (this.중복확인(dt1, "TO_TEL1", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                            if (this.중복확인(dt1, "TO_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                            if (this.중복확인(dt1, "TO_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                            if (this.중복확인(dt1, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                        }
                        #endregion
                    }
                    else if (Settings.Default.주소표시 == "매출처주소")
                    {
                        #region 매출처주소
                        if (this.중복확인(dt1, "TO_COMPANY", "매출처", ref 매출처) == false) return;
                        if (this.중복확인(dt1, "TO_ADRESS", "매출처주소", ref 매출처주소) == false) return;

                        if (this.중복확인(dt1, "TO_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                        if (this.중복확인(dt1, "TO_ADRESS", "창봉투주소", ref 창봉투주소) == false) return;
                        if (this.중복확인(dt1, "TO_TEL1", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                        if (this.중복확인(dt1, "TO_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                        if (this.중복확인(dt1, "TO_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                        if (this.중복확인(dt1, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                        #endregion
                    }
                    else if (Settings.Default.주소표시 == "계산서주소")
                    {
                        #region 계산서주소
                        if (this.중복확인(dt2, "INVOICE_COMPANY", "매출처", ref 매출처) == false) return;
                        if (this.중복확인(dt2, "INVOICE_ADDRESS", "매출처주소", ref 매출처주소) == false) return;

                        if (this.중복확인(dt2, "INVOICE_COMPANY", "창봉투매출처", ref 창봉투매출처) == false) return;
                        if (this.중복확인(dt2, "INVOICE_ADDRESS", "창봉투주소", ref 창봉투주소) == false) return;
                        if (this.중복확인(dt2, "INVOICE_TEL", "창봉투전화번호", ref 창봉투전화번호) == false) return;
                        if (this.중복확인(dt2, "INVOICE_EMAIL", "창봉투이메일", ref 창봉투이메일) == false) return;
                        if (this.중복확인(dt2, "INVOICE_RMK", "창봉투비고", ref 창봉투비고) == false) return;

                        if (this.중복확인(dt2, "CD_NATION_INVOICE", "국가코드", ref 국가코드) == false) return;
                        #endregion
                    }

                    if (this.중복확인(dt1, "NM_NATION", "매출처국가", ref 매출처국가) == false) return;
                    if (this.중복확인(dt1, "FROM_NAME", "대표자명", ref 대표자명) == false) return;
                    
                    //if (this.중복확인(dt2, "NO_HULL", "호선번호", ref 호선번호) == false) return;
                    //if (this.중복확인(dt2, "NM_VESSEL", "호선명", ref 호선명) == false) return;

                    dataRowArray1 = dt2.Select("ISNULL(NO_HULL, '') <> ''");
                    if (dataRowArray1.Length > 0)
                        호선번호 = D.GetString(dataRowArray1[0]["NO_HULL"]);

                    dataRowArray1 = dt2.Select("ISNULL(NM_VESSEL, '') <> ''");
                    if (dataRowArray1.Length > 0)
                        호선명 = D.GetString(dataRowArray1[0]["NM_VESSEL"]);

                    dataRowArray1 = dt2.Select("ISNULL(NO_IMO, '') <> ''");
                    if (dataRowArray1.Length > 0)
                        IMO번호 = D.GetString(dataRowArray1[0]["NO_IMO"]);

                    //if (this.중복확인(dt2, "EN_SALEORG", "영업조직명", ref 영업조직명) == false) return;
                    //if (this.중복확인(dt2, "NM_ENG", "영업조직장", ref 영업조직장) == false) return;
                    //if (this.중복확인(dt2, "DC_SIGN", "서명", ref 서명) == false) return;

                    dataRowArray1 = dt2.Select("ISNULL(EN_SALEORG, '') <> ''");
                    if (dataRowArray1.Length > 0)
                        영업조직명 = D.GetString(dataRowArray1[0]["EN_SALEORG"]);

                    dataRowArray1 = dt2.Select("ISNULL(NM_ENG, '') <> ''");
                    if (dataRowArray1.Length > 0)
                        영업조직장 = D.GetString(dataRowArray1[0]["NM_ENG"]);

                    dataRowArray1 = dt2.Select("ISNULL(DC_SIGN, '') <> ''");
                    if (dataRowArray1.Length > 0)
                        서명 = D.GetString(dataRowArray1[0]["DC_SIGN"]);

                    서명 = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + 서명;
                    인보이스번호 = D.GetString(this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "IV"));

                    #region 인보이스 정보
                    this._biz.인보이스정보등록(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                              인보이스번호,
                                                              dataRowArray1[0]["NO_IV"].ToString(),
                                                              거래처코드,
                                                              IMO번호,
                                                              창봉투매출처,
                                                              창봉투주소,
                                                              창봉투전화번호,
                                                              국가코드,
                                                              Global.MainFrame.LoginInfo.UserID });
                    #endregion

                    reportHelper.SetData("인보이스번호", 인보이스번호);
                    reportHelper.SetData("매출처명", 매출처);
                    reportHelper.SetData("매출처주소", 매출처주소);
                    reportHelper.SetData("매출처국가", 매출처국가);
                    reportHelper.SetData("호선번호", 호선번호);
                    reportHelper.SetData("호선명", 호선명);
                    reportHelper.SetData("영업조직명", 영업조직명);
                    reportHelper.SetData("영업조직장", 영업조직장);
                    reportHelper.SetData("서명", 서명);
                    reportHelper.SetData("창봉투매출처", 창봉투매출처);
                    reportHelper.SetData("창봉투주소", 창봉투주소);
                    reportHelper.SetData("창봉투전화번호", 창봉투전화번호);
                    reportHelper.SetData("창봉투이메일", 창봉투이메일);
                    reportHelper.SetData("창봉투비고", 창봉투비고);
                    reportHelper.SetData("스탬프표시", Settings.Default.스탬프표시);
                    reportHelper.SetData("로고표시", Settings.Default.로고표시);
                    reportHelper.SetData("서명표시", Settings.Default.서명표시);
                    reportHelper.SetData("대표자명", 대표자명);

                    Util.RPT_Print(reportHelper);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 중복확인(DataTable sourceDt, string column, string columnName, ref string value)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = ComFunc.getGridGroupBy(sourceDt, new string[1] { column }, true).Select("ISNULL(" + column + ", '') <> ''");

                if (dataRowArray.Length == 0)
                {
                    value = string.Empty;
                    return true;
                }
                else if (dataRowArray.Length > 1)
                {
                    this.ShowMessage(공통메세지._의값이중복되었습니다, columnName);
                    return true;
                }
                else
                {
                    value = D.GetString(dataRowArray[0][0]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

        private void btn전자세금계산서_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsExistPage("P_SA_ETAX36524D_REG", false))
                    this.UnLoadPage("P_SA_ETAX36524D_REG", false);

                this.CallOtherPageMethod("P_SA_ETAX36524D_REG", "전자세금계산서발행(36524D)", "P_SA_ETAX36524D_REG", this.Grant, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn거래명세표_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray1, dataRowArray2;
            DataTable dt1, dt2;
            ReportHelper reportHelper;
            string 대표자명;
            
            try
            {
                if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow)
                    return;

                dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    dt1 = this._flexH.DataTable.Clone();
                    dt2 = this._flexL.DataTable.Clone();

                    dt2.Columns.Add("IDX");
                    dt2.Columns.Add("TO_NOCOMPANY");
                    dt2.Columns.Add("TO_COMPANY");
                    dt2.Columns.Add("TO_NAME");
                    dt2.Columns.Add("TO_ADRESS");
                    dt2.Columns.Add("TO_TPJOB");
                    dt2.Columns.Add("TO_CLSJOB");

                    int index = 0;

                    foreach (DataRow dr in dataRowArray1)
                    {
                        dt1.ImportRow(dr);

                        dataRowArray2 = this._flexL.DataTable.Select("NO_IV = '" + dr["NO_IV"].ToString() + "'");
                        index++;

                        foreach (DataRow dr1 in dataRowArray2)
                        {
                            dt2.ImportRow(dr1);

                            dt2.Rows[dt2.Rows.Count - 1]["IDX"] = index.ToString();
                            dt2.Rows[dt2.Rows.Count - 1]["TO_NOCOMPANY"] = dr["TO_NOCOMPANY"];
                            dt2.Rows[dt2.Rows.Count - 1]["TO_COMPANY"] = dr["TO_COMPANY"];
                            dt2.Rows[dt2.Rows.Count - 1]["TO_NAME"] = dr["TO_NAME"];
                            dt2.Rows[dt2.Rows.Count - 1]["TO_ADRESS"] = dr["TO_ADRESS"];
                            dt2.Rows[dt2.Rows.Count - 1]["TO_TPJOB"] = dr["TO_TPJOB"];
                            dt2.Rows[dt2.Rows.Count - 1]["TO_CLSJOB"] = dr["TO_CLSJOB"];
                        }
                    }

                    dt2.DefaultView.Sort = "NM_VESSEL ASC, NO_PO_PARTNER ASC";
                    dt2 = dt2.DefaultView.ToTable();

                    대표자명 = string.Empty;
                    if (this.중복확인(dt1, "FROM_NAME", "대표자명", ref 대표자명) == false) return;

                    reportHelper = Util.SetRPT("R_CZ_SA_IVMNG_3", "매출관리-INVOICE", Global.MainFrame.LoginInfo.CompanyCode, dt1, dt2);
                    
                    reportHelper.SetDataTable(dt1, 1);
                    reportHelper.SetDataTable(dt2, 2);

                    reportHelper.SetData("대표자명", 대표자명);
                    reportHelper.SetData("직인표시", Settings.Default.직인표시);

                    Util.RPT_Print(reportHelper);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표출력_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray1, dataRowArray2, dataRowArray3;
            DataTable dt1, dt2, dt3, dt4, dt5, dtTemp;
            ReportHelper reportHelper;
            DataSet ds;
            string filter, 멀티출고번호, 멀티수주번호, 청구운임;

            try
            {
                if (this._flexH.DataView == null) return;

                dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    dataRowArray2 = this._flexH.DataTable.Select("ISNULL(NO_DOCU, '') = '' AND S ='Y'");
                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this.ShowMessage("CZ_전표 처리되지 않은 건이 포함되어 있습니다.");
                    }
                    else
                    {
                        if (!this._flexH.HasNormalRow) return;

                        ds = this._biz.전표출력(string.Empty, string.Empty);

                        dt1 = ds.Tables[0];
                        dt2 = ds.Tables[1];

                        dt1.Columns.Add("NO_HULL");
                        dt1.Columns.Add("NM_VESSEL");
                        dt1.Columns.Add("DT_LOADING");
                        dt1.Columns.Add("NM_MAIN_CATEGORY");
                        dt1.Columns.Add("NM_SUB_CATEGORY");
                        dt1.Columns.Add("AM_CLS");
                        dt1.Columns.Add("NM_EMP");
                        dt1.Columns.Add("NM_LOG_EMP");

						dt2.Columns.Add("DC_SIGN1");
						dt2.Columns.Add("DC_SIGN2");

						if (Global.MainFrame.LoginInfo.CompanyCode == "K100" || 
							Global.MainFrame.LoginInfo.CompanyCode == "K200")
							dt2.Columns.Add("DC_SIGN3");

						foreach (DataRow dr in dataRowArray1)
                        {
                            dataRowArray3 = this._flexL.DataTable.Select("NO_IV = '" + D.GetString(dr["NO_IV"]) + "'");

                            멀티출고번호 = string.Empty;
                            멀티수주번호 = string.Empty;

                            dt3 = ComFunc.getGridGroupBy(dataRowArray3, new string[] { "NO_IO" }, true);
                            dt4 = ComFunc.getGridGroupBy(dataRowArray3, new string[] { "NO_SO" }, true);

                            if (dt3.Rows.Count == 1)
                                멀티출고번호 = D.GetString(dt3.Rows[0]["NO_IO"]);
                            else
                                멀티출고번호 = D.GetString(dt3.Rows[0]["NO_IO"]) + " 외 " + (dt3.Rows.Count - 1) + "건";

                            if (dt4.Rows.Count == 1)
                                멀티수주번호 = D.GetString(dt4.Rows[0]["NO_SO"]);
                            else
                                멀티수주번호 = D.GetString(dt4.Rows[0]["NO_SO"]) + " 외 " + (dt4.Rows.Count - 1) + "건";

                            ds = this._biz.전표출력(D.GetString(dr["CD_PC"]), D.GetString(dr["NO_DOCU"]));

                            ds.Tables[0].Rows[0]["NO_IO"] = 멀티출고번호;
                            ds.Tables[0].Rows[0]["NO_SO"] = 멀티수주번호;

                            foreach (DataRow dr2 in ds.Tables[1].Rows)
                            {
                                dt2.ImportRow(dr2);

								if (dr["ST_STAT"].ToString() == "1")
								{
									string query;
									DataTable 결재정보;

									query = @"SELECT APP_SIGN1,
											  	     APP_SIGN3
											  FROM FI_GWDOCU WITH(NOLOCK)
											  WHERE CD_COMPANY = 'K100'
											  AND NO_DOCU = '" + dr["CD_COMPANY"].ToString() + "-" + dr["NO_IV"].ToString() + "'";

									결재정보 = DBHelper.GetDataTable(query);

									query = @"SELECT DC_SIGN 
											  FROM HR_PHOTO WITH(NOLOCK)
											  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
											  AND NO_EMP = '{0}'";

									dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN1"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + DBHelper.ExecuteScalar(string.Format(query, dr["NO_EMP"].ToString())).ToString();

									if (결재정보.Rows[0]["APP_SIGN1"].ToString() == "http://gw.dintec.co.kr:80/Images/common/EA_confirm_bg.gif")
										dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN2"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + DBHelper.ExecuteScalar(string.Format(query, dataRowArray3[0]["NO_EMP"].ToString())).ToString();
									else
										dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN2"] = 결재정보.Rows[0]["APP_SIGN1"].ToString();

									if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
									{
										if (결재정보.Rows[0]["APP_SIGN3"].ToString() == "http://gw.dintec.co.kr:80/Images/common/EA_confirm_bg.gif")
											dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN3"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + DBHelper.ExecuteScalar(string.Format(query, "S-279")).ToString();
										else
											dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN3"] = 결재정보.Rows[0]["APP_SIGN3"].ToString();
									}
									else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
									{
										if (결재정보.Rows[0]["APP_SIGN3"].ToString() == "http://gw.dintec.co.kr:80/Images/common/EA_confirm_bg.gif")
											dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN3"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + DBHelper.ExecuteScalar(string.Format(query, "D-038")).ToString();
										else
											dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN3"] = 결재정보.Rows[0]["APP_SIGN3"].ToString();
									}
								}
								else
								{
									dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN1"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + "blank.jpg";
									dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN2"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + "blank.jpg";

									if (Global.MainFrame.LoginInfo.CompanyCode == "K100" || 
										Global.MainFrame.LoginInfo.CompanyCode == "K200")
										dt2.Rows[dt2.Rows.Count - 1]["DC_SIGN3"] = Global.MainFrame.HostURL + "/shared/image/human/sign/" + "blank.jpg";
								}
							}

                            dt1.ImportRow(ds.Tables[0].Rows[0]);

                            dtTemp = ComFunc.getGridGroupBy(dataRowArray3, new string[] { "NO_HULL", "NM_VESSEL", "DT_LOADING", "NM_MAIN_CATEGORY", "NM_SUB_CATEGORY", "NM_LOG_EMP" }, true);
                            청구운임 = D.GetString(this._flexL.DataTable.Compute("SUM(AM_EX) + SUM(VAT_TOT)", "NO_IV = '" + D.GetString(dr["NO_IV"]) + "' AND CD_ITEM LIKE 'SD%'"));

                            dt1.Rows[dt1.Rows.Count - 1]["NO_HULL"] = D.GetString(dtTemp.Rows[0]["NO_HULL"]);
                            dt1.Rows[dt1.Rows.Count - 1]["NM_VESSEL"] = D.GetString(dtTemp.Rows[0]["NM_VESSEL"]);
                            dt1.Rows[dt1.Rows.Count - 1]["DT_LOADING"] = D.GetString(dtTemp.Rows[0]["DT_LOADING"]);
                            dt1.Rows[dt1.Rows.Count - 1]["NM_MAIN_CATEGORY"] = D.GetString(dtTemp.Rows[0]["NM_MAIN_CATEGORY"]);
                            dt1.Rows[dt1.Rows.Count - 1]["NM_SUB_CATEGORY"] = D.GetString(dtTemp.Rows[0]["NM_SUB_CATEGORY"]);
                            dt1.Rows[dt1.Rows.Count - 1]["AM_CLS"] = 청구운임;
                            dt1.Rows[dt1.Rows.Count - 1]["NM_EMP"] = Global.MainFrame.LoginInfo.EmployeeName;
                            dt1.Rows[dt1.Rows.Count - 1]["NM_LOG_EMP"] = D.GetString(dtTemp.Rows[0]["NM_LOG_EMP"]);
						}

                        reportHelper = Util.SetReportHelper(Util.GetReportFileName("R_CZ_SA_IVMNG_2", this.LoginInfo.CompanyCode), "매출관리-전표출력", this.LoginInfo.CompanyCode);

                        reportHelper.SetDataTable(dt1, 1);
                        reportHelper.SetDataTable(dt2, 2);

                        dt5 = ComFunc.getGridGroupBy(dt2, new string[] { "NO_DOCU", "CD_ACCT", "NM_ACCT" }, true);
                        dt5.Columns.Add("AM_DR");
                        dt5.Columns.Add("AM_CR");
						
						foreach (DataRow dr3 in dt5.Rows)
                        {
                            filter = "NO_DOCU = '" + D.GetString(dr3["NO_DOCU"]) + "' AND CD_ACCT = '" + D.GetString(dr3["CD_ACCT"]) + "'";
                            dr3["AM_DR"] = dt2.Compute("SUM(AM_DR)", filter);
                            dr3["AM_CR"] = dt2.Compute("SUM(AM_CR)", filter);
						}

                        reportHelper.SetDataTable(dt5, 3);

                        reportHelper.SetData("작성자", Global.MainFrame.LoginInfo.EmployeeName);

						Util.RPT_Print(reportHelper);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn회계전표처리_Click(object sender, EventArgs e)
        {
            if (this._flexH.DataView == null) return;

            DataRow[] dataRowArray1 = this._flexH.DataTable.Select("TP_AIS = 'N' AND S ='Y'");

            if (dataRowArray1.Length.Equals(0))
            {
                this.ShowMessage("선택된 자료가 없습니다");
            }
            else
            {
                DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S ='Y' AND TP_AIS = 'N' AND  AM_K = 0 AND VAT_TAX = 0 ");
                if (dataRowArray2 != null && dataRowArray2.Length > 0
                    && this.ShowMessage("공급금액/부가세가 0인 항목이 있습니다. 당 항목은 전표처리표시를 하되 회계전표를 만들지않습니다. 계속하시겠습니까?", "IK2") == DialogResult.Cancel)
                    return;

                try
                {
                    bool flag = false;

                    foreach (DataRow dataRow in dataRowArray1)
                    {
                        if (!this._biz.미결전표처리(new object[] { dataRow["CD_COMPANY"].ToString(),
                                                                   Global.MainFrame.LoginInfo.Language,
                                                                   dataRow["NO_IV"].ToString(),
                                                                   dataRow["ETAX_SEND_TYPE"].ToString(), 
                                                                   Settings.Default.Tx_내역표시구분,
                                                                   Settings.Default.Tx_내역표시_Text,
                                                                   Settings.Default.Tx_품목표시구분,
                                                                   Settings.Default.Tx_공급자연락처구분,
                                                                   this.LoginInfo.UserName,
                                                                   Settings.Default.Tx_공급자E_Mail,
                                                                   Settings.Default.Tx_공급자Hp,
                                                                   this.LoginInfo.UserID }))
                        {
                            this.ShowMessage("@(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", new string [] { this.DD("계산서번호"), dataRow["NO_IV"].ToString() }, "IK1");
                            return;
                        }
                        else if (D.GetDecimal(dataRow["AM_RCP_A_EX"]) > 0)
                            flag = true;

                        #region 전표승인
                        if (Global.MainFrame.LoginInfo.StDocuApp == "2" || Global.MainFrame.LoginInfo.StDocuApp == "3")
						{
                            string query = string.Format(@"SELECT FD.NO_DOCU,
       FD.CD_PC
FROM SA_IVH IH WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				  FD.NO_DOCU,
				  FD.CD_PC
           FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, 
				    FD.NO_DOCU,
					FD.CD_PC) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
WHERE IH.CD_COMPANY = '{0}'
AND IH.NO_IV = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_IV"].ToString());

                            DataTable dt = DBHelper.GetDataTable(query);

                            string 전표번호 = dt.Rows[0]["NO_DOCU"].ToString();
                            string 회계단위 = dt.Rows[0]["CD_PC"].ToString();

                            object[] obj = new object[1];
                            DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                              회계단위,
                                                                                              "FI04",
                                                                                              dataRow["DT_PROCESS"].ToString() }, out obj);

                            decimal 회계번호 = D.GetDecimal(obj[0]);

                            DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { 전표번호,
                                                                                            회계단위,
                                                                                            Global.MainFrame.LoginInfo.CompanyCode,
                                                                                            dataRow["DT_PROCESS"].ToString(),
                                                                                            회계번호,
                                                                                            "2",
                                                                                            Global.MainFrame.LoginInfo.UserID,
                                                                                            Global.MainFrame.LoginInfo.UserID });
                        }
                        #endregion
                    }

                    if (flag)
                    {
                        this.ShowMessage("전표가 처리되었습니다.\n선수금 정리 대상 건이 포함되어 있기 때문에 정리 화면으로 자동 링크 됩니다.");
                        this.btn선수금정리_Click(null, null);
                    }
                    else
                        this.ShowMessage("전표가 처리되었습니다");
                    
                    this.OnToolBarSearchButtonClicked(sender, e);
                }
                catch (Exception ex)
                {
                    this.MsgEnd(ex);
                }
            }
        }

        private void btn회계전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.DataView == null) return;

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S ='Y'");
                if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                {
                    this.ShowMessage("선택된 자료가 없습니다.");
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("TP_AIS = 'N' AND S ='Y'");
					DataRow[] dataRowArray3 = this._flexH.DataTable.Select("S ='Y' AND NO_DOCU_GRP IS NOT NULL ");
					DataRow[] dataRowArray4 = this._flexH.DataTable.Select("S ='Y' AND ST_STAT = '1'");

					if (dataRowArray2 != null && dataRowArray2.Length > 0)
					{
						this.ShowMessage("이미 전표취소된 건이 포함되어 있습니다");
					}
					else if (dataRowArray3 != null && dataRowArray3.Length > 0)
					{
						this.ShowMessage("선택건중 통합전표처리건이 포함되어 있습니다 (일반전표건만 선택바랍니다)");
					}
					else if (dataRowArray4 != null && dataRowArray4.Length > 0)
					{
						this.ShowMessage("선택건중 전자결재 완료된 건이 포함 되어 있습니다.");
					}
					else
					{
						DataTable dataTable = this._flexH.DataTable.Clone();
						for (int index = 0; index < dataRowArray1.Length; ++index)
							dataTable.ImportRow(dataRowArray1[index]);

						bool flag = false;

						foreach (DataRow dataRow in dataTable.Rows)
						{
							string 전표유형 = "110";
							string 전표번호 = dataRow["NO_IV"].ToString();

							flag = this._biz.미결전표취소(전표유형, 전표번호);

							for (int index = 0; index < dataRowArray1.Length; ++index)
							{
								if (dataRowArray1[index]["NO_IV"].ToString() == 전표번호)
								{
									dataRowArray1[index].BeginEdit();
									dataRowArray1[index]["TP_AIS"] = "N";
									dataRowArray1[index].EndEdit();
									this.btn전표출력.Enabled = false;
									this.btn회계전표취소.Enabled = false;
									this.btn회계전표처리.Enabled = true;
								}
							}
						}

						if (flag)
						{
							this.ShowMessage("전표가 취소되었습니다");
						}

						this.OnToolBarSearchButtonClicked(null, null);
					}
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn선수금정리_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.IsExistPage("P_CZ_SA_RCPBILL", false))
                    this.UnLoadPage("P_CZ_SA_RCPBILL", false);

                this.LoadPageFrom("P_CZ_SA_RCPBILL", this.DD("선수금정리"), Grant, new object[] { new object[] { D.GetString(this._flexH["CD_PARTNER"]),
                                                                                                                 D.GetString(this._flexH["LN_PARTNER"]),
                                                                                                                 D.GetString(this._flexH["FG_TRANS"]),
                                                                                                                 D.GetString(this._flexH["DT_PROCESS"]) } });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region Grid 이벤트
        private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed) return;

            CellStyle style = this._flexH.Rows[e.Row].Style;

            if (style == null)
            {
                if (D.GetString(this._flexH[e.Row, "TP_AIS"]) == "Y")
                    this._flexH.Rows[e.Row].Style = this._flexH.Styles["처리"];
                else
                    this._flexH.Rows[e.Row].Style = this._flexH.Styles["일반"];
            }
            else if (style.Name == "처리")
            {
                if (!(D.GetString(this._flexH[e.Row, "TP_AIS"]) != "Y"))
                    return;

                this._flexH.Rows[e.Row].Style = this._flexH.Styles["일반"];
            }
            else
            {
                if (!(style.Name == "일반") || !(D.GetString(this._flexH[e.Row, "TP_AIS"]) == "Y"))
                    return;

                this._flexH.Rows[e.Row].Style = this._flexH.Styles["처리"];
            }
        }

        private void _flexH_DoubleClick(object sender, EventArgs e)
        {
            if (!this._flexH.HasNormalRow) return;
            if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;

            DataTable dtL = this._biz.TB_TAX();

            if (Certify.IsLive() && this._flexH.Cols[this._flexH.Col].Name == "NO_DOCU" && D.GetString(this._flexH["NO_DOCU"]) != "")
            {
                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[4] { D.GetString(this._flexH["NO_DOCU"]), 
                                                                                                                                  "1",
                                                                                                                                  D.GetString(this._flexH["CD_PC"]),
                                                                                                                                  Global.MainFrame.LoginInfo.CompanyCode });
            }

            if (!(this._flexH.Cols[this._flexH.Col].Name == "ETAX_TOT_KEY"))
                return;

            string str = this._flexH[this._flexH.Row, "ETAX_TOT_KEY"].ToString();

            if (str == "" || !this.전자세금계산서환경(this._flexH[this._flexH.Row, "CD_BIZAREA_TAX"].ToString()))
                return;

            if (this._flexH[this._flexH.Row, "ETAX_TOT_TYPE"].ToString() == "NEOPORT")
            {
                int num1 = 0;
                foreach (DataRow row in this._flexH.DataTable.Select("ETAX_KEY = '" + str + "' AND ETAX_SYS <> '00' AND ETAX_SYS <> 'xx'", "", DataViewRowState.CurrentRows))
                {
                    this.상태확인(row);
                    ++num1;
                }
                if (num1 > 0)
                {
                    this.ShowMessage("neoport:상태확인 되었습니다");
                }
                else
                {
                    this.ShowMessage("NEOPORT:변경된 상태가 없습니다");
                }
            }
            else if (this._flexH[this._flexH.Row, "ETAX_TOT_TYPE"].ToString() == "E36524")
            {
                DataRow row = dtL.NewRow();
                row["NO"] = this._flexH[this._flexH.Row, "NO"].ToString();
                row["CMP_TAX_ST"] = "2";
                dtL.Rows.Add(row);

                if (dtL == null || !this._biz.change_status(dtL))
                    return;

                this.ShowMessage("e36524:상태확인을 요청했습니다.");
            }
        }

        private bool 전자세금계산서환경(string 사업장코드)
        {
            DataTable dataTable = this._biz.etax_LoginSelect(사업장코드, "SA");

            if (dataTable.Rows.Count == 0)
            {
                this.ShowMessage("환경설정이 되어있지 않습니다. 설정후 다시 시도하세요");
                return false;
            }

            this.사용자ID = dataTable.Rows[0]["ID_USER"].ToString();
            this.회사번호 = dataTable.Rows[0]["NO_COMPANY"].ToString();
            this.지불회사 = dataTable.Rows[0]["NO_PAYCOMPANY"].ToString();
            this.구분 = dataTable.Rows[0]["TP_RECEIPT"].ToString();
            this.위치 = dataTable.Rows[0]["DC_LOCATION"].ToString();
            this.이용사이트 = dataTable.Rows[0]["SITE_ADD"].ToString();
            this.사이트구분 = dataTable.Rows[0]["SITE_GUBUN"].ToString();

            return true;
        }

        private void 상태확인(DataRow row)
        {
            NeoportDti neoportDti = new NeoportDti();
            neoportDti.InitConfig(null);
            neoportDti.ClearAll();
            neoportDti.InitObject(this.사이트구분);

            string str = this.MakeToken("Api_no", "3") + this.MakeToken("Dt_no", row["ETAX_KEY"].ToString()) + this.MakeToken("Biz_no", row["TO_NOCOMPANY"].ToString());
            string resultsStatus;

            if (!((resultsStatus = this.GetResultsStatus(neoportDti.CallAPI("3", str, ""))) != ""))
                return;

            if (resultsStatus == "0")
            {
                row["ETAX_SYS"] = (object)"00";
                row["ETAX_KEY"] = (object)"";
            }
            else
                row["ETAX_SYS"] = (object)resultsStatus;

            this._biz.etax_Update(row["NO_IV"].ToString(), row["ETAX_KEY"].ToString(), row["ETAX_SYS"].ToString(), row["ETAX_EMAIL"].ToString());
        }

        private string MakeToken(string key, string data)
        {
            data = data.Replace("&", "#amp;");
            data = data.Replace("=", "#equ;");
            return key + "=" + data + "&";
        }

        private string GetResultsStatus(string ret)
        {
            string str1 = "";
            int length = ret.IndexOf("|");
            if (length >= 0)
            {
                string str2 = ret.Substring(0, length);
                if ((str2.ToString() == "" ? 0 : Decimal.Parse(str2.ToString())) < 0)
                {
                    if (ret.Substring(length + 1).IndexOf("존재하지") >= 0)
                        str1 = "0";
                }
                else
                    str1 = ret.Substring(length + 1, 2);
            }
            return str1;
        }

        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            if (this.IsChanged(null))
                this.SetToolBarButtonState(true, false, true, true, false);
            else
                this.SetToolBarButtonState(true, false, true, false, false);

            if (this._flexH.HasNormalRow)
                return;

            this.ToolBarDeleteButtonEnabled = false;
        }

        private bool IsChanged(string gubun)
        {
            try
            {
                if (gubun == null)
                    return this._flexH.IsDataChanged;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid = (FlexGrid)sender;

            if (flexGrid.Name.ToString() == "_flexH")
            {
                if (flexGrid.Cols[e.Col].Name != "S" && flexGrid.Cols[e.Col].Name != "NO_VOLUME" && (flexGrid.Cols[e.Col].Name != "NO_HO" && flexGrid.Cols[e.Col].Name != "NO_SEQ") && (flexGrid.Cols[e.Col].Name != "DC_REMARK" && flexGrid.Cols[e.Col].Name != "DT_RCP_RSV" && (flexGrid.Cols[e.Col].Name != "ETAX_SEND_TYPE" && flexGrid.Cols[e.Col].Name != "ETAX_SELL_DAM_NM")) && (flexGrid.Cols[e.Col].Name != "ETAX_SELL_DAM_MOBIL" && flexGrid.Cols[e.Col].Name != "ETAX_SELL_DAM_EMAIL" && (flexGrid.Cols[e.Col].Name != "NM_PTR" && flexGrid.Cols[e.Col].Name != "EX_EMIL") && flexGrid.Cols[e.Col].Name != "EX_HP") && flexGrid.Cols[e.Col].Name != "TP_RECEIPT")
                    e.Cancel = true;
                if (!(flexGrid.Cols[e.Col].Name == "TP_RECEIPT") || !(D.GetString(this._flexH["TP_AIS"]).ToUpper() == "Y") && !(D.GetString(this._flexH["TP_AIS"]) == "01") && !(D.GetString(this._flexH["TP_AIS"]) == "02"))
                    return;
                e.Cancel = true;
            }
            else if (this._flexH["TP_AIS"].ToString().ToUpper() == "Y")
                e.Cancel = true;
            else if (flexGrid.Cols[e.Col].Name != "S" && flexGrid.Cols[e.Col].Name != "UM_ITEM_CLS" && flexGrid.Cols[e.Col].Name != "AM_CLS" && flexGrid.Cols[e.Col].Name != "VAT")
                e.Cancel = true;
        }

        private void _flexH_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                this._biz.SaveContent(e.ContentType,
                                      e.CommandType,
                                      D.GetString(this._flexH[e.Row, "NO_IV"]),
                                      e.SettingValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "S":
                        string @string = D.GetString(this._flexH["ETAX_KEY_ORIGINAL"]);

                        if (!(@string != string.Empty) || this._flexH.DataTable.Select("ETAX_KEY_ORIGINAL = '" + @string + "'").Length <= 1)
                            break;

                        for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                        {
                            if (!(D.GetString(this._flexH[@fixed, "ETAX_KEY_ORIGINAL"]) != @string))
                                this._flexH[@fixed, "S"] = (e.Checkbox == CheckEnum.Checked ? "Y" : "N");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                dt = null;
                key = this._flexH["NO_IV"].ToString();
                filter = "NO_IV = '" + key + "'";

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key,
                                                               this.txt프로젝트.Text, 
                                                               this.cbo공장.SelectedValue,
                                                               this.txt주문번호.Text,
                                                               this.ctx호선번호.CodeValue,
                                                               this.bpc영업조직.QueryWhereIn_Pipe,
                                                               this.bpc영업그룹.QueryWhereIn_Pipe });
                }

                this._flexL.BindingAdd(dt, filter);

                this.콘트롤활성화();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (this._flexL.Cols[e.Col].Name)
                {
                    case "UM_ITEM_CLS":
                        double num1 = 0.0;
                        double num2 = 0.0;

                        if (this._flexL["QT_GI_CLS"].ToString() != "")
                            num1 = Convert.ToDouble(this._flexL["QT_GI_CLS"].ToString());
                        if (this._flexL.EditData.ToString() != "")
                            num2 = Convert.ToDouble(this._flexL.EditData.ToString());

                        this._flexL["AM_CLS"] = (num1 * num2);
                        this._flexL["VAT"] = (num1 * num2 / Convert.ToInt32(this._flexL["CD_FLAG1"].ToString().Trim()));
                        int num3 = 2;
                        double num4 = 0.0;
                        double num5 = 0.0;

                        for (; num3 < this._flexL.Rows.Count; ++num3)
                        {
                            if (this._flexL[num3, "AM_CLS"].ToString() != "")
                                num4 += Convert.ToDouble(this._flexL[num3, "AM_CLS"].ToString());
                            if (this._flexL[num3, "VAT"].ToString() != "")
                                num5 += Convert.ToDouble(this._flexL[num3, "VAT"].ToString());
                        }

                        if (Convert.ToDouble(this._flexH["AM_K"].ToString()) != num4)
                            this._flexH["AM_K"] = num4;
                        if (Convert.ToDouble(this._flexH["VAT_TAX"]) == num5)
                            break;

                        this._flexH["VAT_TAX"] = num5;
                        break;
                    case "AM_CLS":
                        double num6 = 0.0;

                        if (this._flexL.EditData.ToString() != "")
                            num6 = Convert.ToDouble(this._flexL.EditData.ToString());

                        this._flexL["AM_CLS"] = num6;
                        this._flexL["VAT"] = (num6 / 100.0);
                        int num7 = 2;
                        double num8 = 0.0;
                        double num9 = 0.0;

                        for (; num7 < this._flexL.Rows.Count; ++num7)
                        {
                            if (this._flexL[num7, "AM_CLS"].ToString() != "")
                                num8 += Convert.ToDouble(this._flexL[num7, "AM_CLS"].ToString());
                            if (this._flexL[num7, "VAT"].ToString() != "")
                                num9 += Convert.ToDouble(this._flexL[num7, "VAT"].ToString());
                        }

                        if (Convert.ToDouble(this._flexH["AM_K"].ToString()) != num8)
                            this._flexH["AM_K"] = num8;
                        if (Convert.ToDouble(this._flexH["VAT_TAX"]) == num9)
                            break;

                        this._flexH["VAT_TAX"] = num9;
                        break;
                    case "VAT":
                        double num10 = 0.0;

                        if (this._flexL.EditData.ToString() != "")
                            num10 = Convert.ToDouble(this._flexL.EditData.ToString());

                        this._flexL["VAT"] = num10;
                        int num11 = 2;
                        double num12 = 0.0;

                        for (; num11 < this._flexL.Rows.Count; ++num11)
                        {
                            if (this._flexL[num11, "VAT"].ToString() != "")
                                num12 += Convert.ToDouble(this._flexL[num11, "VAT"].ToString());
                        }

                        if (Convert.ToDouble(this._flexH["VAT_TAX"]) == num12)
                            break;

                        this._flexH["VAT_TAX"] = num12;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
        }

        private void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this._flexH.Redraw = false;

                //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h + 1), D.GetString(this._flexH.Rows.Count - 1) });

                    this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexH.Redraw = true;
            }
        }

        private void 콘트롤활성화()
        {
            if (this._flexH[this._flexH.Row, "TP_AIS"].ToString() == "Y")
            {
                this.btn전표출력.Enabled = true;
                this.btn회계전표처리.Enabled = false;
                this.btn회계전표취소.Enabled = true;
            }
            else if (this._flexH[this._flexH.Row, "TP_AIS"].ToString() == "N")
            {
                this.btn전표출력.Enabled = false;
                this.btn회계전표처리.Enabled = true;
                this.btn회계전표취소.Enabled = false;
            }

            if (D.GetDecimal(this._flexH[this._flexH.Row, "AM_RCP_A"]) > 0)
                this.btn선수금정리.Enabled = true;
            else if (D.GetDecimal(this._flexH[this._flexH.Row, "AM_RCP_A"]) <= 0)
                this.btn선수금정리.Enabled = false;
        }
        #endregion

        #region 기타 이벤트
        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult == DialogResult.Cancel) return;

                if (e.ControlName == this.ctx매출처.Name)
                {
                    this.ctx매출처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
                    this.ctx매출처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion
    }
}