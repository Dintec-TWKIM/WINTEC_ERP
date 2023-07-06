using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Duzon.Common.Forms;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Dintec;
using System.IO;

namespace cz
{
	public partial class P_CZ_MA_HGS_ITEM : PageBase
	{
        P_CZ_MA_HGS_ITEM_biz _biz = new P_CZ_MA_HGS_ITEM_biz();

		#region ==================================================================================================== 속성

		public string CD_COMPANY { get; set; }
        public string NO_EMP { get; set; }

        DataTable dTItem = null;

        public int imoCount = 1;

		#endregion

		#region ==================================================================================================== 생성자

		public P_CZ_MA_HGS_ITEM()
		{
			StartUp.Certify(this);
            CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
            NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo.ToString();
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== 초기화

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			dtp일자.StartDateToString = Util.GetToday(-365);
			dtp일자.EndDateToString = Util.GetToday();

		}

		private void InitGrid()
		{
            this.MainGrids = new FlexGrid[] { this.flexHULL };

			flexHULL.BeginSetting(1, 1, true);
				
			flexHULL.SetCol("NO_IMO"		, "IMO"	      , 120);
            flexHULL.SetCol("NO_ENGINE"       , "엔진번호"          , 120);
            flexHULL.SetCol("CNT", "아이템수", 120);
            flexHULL.SetCol("NM_MODEL", "모델명", 120);

            flexHULL.Cols["NO_IMO"].TextAlign = TextAlignEnum.CenterCenter;
            flexHULL.Cols["NO_ENGINE"].TextAlign = TextAlignEnum.CenterCenter;

			flexHULL.KeyActionEnter = KeyActionEnum.MoveDown;
			flexHULL.SettingVersion = "18.11.11.29";
			flexHULL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            flexHULL.AllowEditing = true;

            CellStyle style = flexHULL.Styles.Add("SELECT");
            style.ForeColor = Color.Red;

            style = flexHULL.Styles.Add("BASIC");
            style.ForeColor = Color.Black;





            flexHITEM.BeginSetting(1, 1, false);

            flexHITEM.SetCol("CD_ITEM", "재고코드", 80);
            flexHITEM.SetCol("UCODE", "U코드", 120);
            flexHITEM.SetCol("NO_DRAWING", "도면번호", 120);
            flexHITEM.SetCol("PL_DRAWING", "도면번호2", 120);
            flexHITEM.SetCol("NO_PLATE", "품목코드", 120);
            flexHITEM.SetCol("NM_PLATE", "품목명", 200);
            flexHITEM.SetCol("NO_IMO", "IMO번호", 100);
            flexHITEM.SetCol("NO_ENGINE", "엔진번호", false);


            flexHITEM.KeyActionEnter = KeyActionEnum.MoveDown;
            flexHITEM.SettingVersion = "19.02.20.03";
            flexHITEM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            flexHITEM.Rows[0].Height = 24;



            flexMaps.BeginSetting(1, 1, false);

            flexMaps.SetCol("NO_IMO", "NO_IMO", true);
            flexMaps.SetCol("NO_ENGINE", "NO_ENGINE", true);
            flexMaps.SetCol("SERIAL", "SERIAL", true);
            flexMaps.SetCol("NO_DRAWING", "NO_DRAWING", true);
            flexMaps.SetCol("PL_DRAWING", "PL_DRAWING", true);
            flexMaps.SetCol("WEIGHT", "WEIGHT", true);
            flexMaps.SetCol("NM_PLATE", "NM_PLATE", true);
            flexMaps.SetCol("NO_PLATE", "NO_PLATE", true);
            flexMaps.SetCol("UNIT", "UNIT", true);

            flexMaps.KeyActionEnter = KeyActionEnum.MoveDown;
            flexMaps.SettingVersion = "17.01.23.02";
            flexMaps.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);





            flexL.BeginSetting(1, 1, false);

            flexL.SetCol("CD_ITEM", "재고코드", 80);
            flexL.SetCol("CD_SPEC", "U코드", 120);
            flexL.SetCol("NO_DRAWING", "도면번호", 120);
            flexL.SetCol("NO_PLATE", "품목코드", 120);
            flexL.SetCol("NM_PLATE", "품목명", 200);
            flexL.SetCol("NO_IMO", "IMO번호", 100);
            flexL.SetCol("NO_ENGINE", "엔진번호", false);


            flexL.SetCol("CNT_QTN", "견적건수", 55, false, typeof(decimal), FormatTpType.QUANTITY);
            flexL.SetCol("CNT_SO", "수주건수", 55, false, typeof(decimal), FormatTpType.QUANTITY);

            flexL.SetCol("SUM_QTN", "견적수량", 55, false, typeof(decimal), FormatTpType.QUANTITY);
            flexL.SetCol("SUM_SO", "수주수량", 55, false, typeof(decimal), FormatTpType.QUANTITY);

            flexL.SetCol("AVG_UM_KR_P", "평균매입단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flexL.SetCol("AVG_UM_KR_S", "평균매출단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flexL.SetCol("AVG_LT", "평균납기", 55, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            flexL.KeyActionEnter = KeyActionEnum.MoveDown;
            flexL.SettingVersion = "18.02.20.22";
            flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            flexL.SetExceptSumCol("CNT_SPEC", "CNT_RT_SO", "SUM_RT_SO", "AVG_UM_KR_P", "AVG_UM_KR_S", "AVG_LT");

            flexL.Rows[0].Height = 24;



            flexITEM.BeginSetting(1, 1, false);

            flexITEM.SetCol("CD_ITEM", "재고코드", 80);
            flexITEM.SetCol("CD_SPEC", "U코드", 120);
            flexITEM.SetCol("NO_DRAWING", "도면번호", 120);
            flexITEM.SetCol("NO_PLATE", "품목코드", 120);
            flexITEM.SetCol("NM_PLATE", "품목명", 200);
            flexITEM.SetCol("NO_IMO", "IMO번호", 100);
            flexITEM.SetCol("NO_ENGINE", "엔진번호", false);


            flexITEM.SetCol("CNT_QTN", "견적건수", 55, false, typeof(decimal), FormatTpType.QUANTITY);
            flexITEM.SetCol("CNT_SO", "수주건수", 55, false, typeof(decimal), FormatTpType.QUANTITY);

            flexITEM.SetCol("SUM_QTN", "견적수량", 55, false, typeof(decimal), FormatTpType.QUANTITY);
            flexITEM.SetCol("SUM_SO", "수주수량", 55, false, typeof(decimal), FormatTpType.QUANTITY);

            flexITEM.SetCol("AVG_UM_KR_P", "평균매입단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flexITEM.SetCol("AVG_UM_KR_S", "평균매출단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flexITEM.SetCol("AVG_LT", "평균납기", 55, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            flexITEM.SetCol("MIX2", "1M", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            flexITEM.SetCol("MIX3", "4M", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            flexITEM.SetCol("QT_AVAILABLE", "가용재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexITEM.SetCol("MIX", "가용+발주", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexITEM.SetCol("NEED_COUNT", "필요수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            flexITEM.KeyActionEnter = KeyActionEnum.MoveDown;
            flexITEM.SettingVersion = "18.02.20.55";
            flexITEM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            flexITEM.Rows[0].Height = 24;




            flex3.BeginSetting(1, 1, false);

            flex3.SetCol("CD_ITEM", "재고코드", 80);
            flex3.SetCol("CD_SPEC", "U코드", 120);
            flex3.SetCol("NO_DRAWING", "도면번호", 120);
            flex3.SetCol("NO_PLATE", "품목코드", 120);
            flex3.SetCol("NM_PLATE", "품목명", 200);
            flex3.SetCol("NO_IMO", "IMO번호", 100);
            flex3.SetCol("NO_ENGINE", "엔진번호", false);


            flex3.SetCol("CNT_QTN", "견적건수", 55, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("CNT_SO", "수주건수", 55, false, typeof(decimal), FormatTpType.QUANTITY);

            flex3.SetCol("SUM_QTN", "견적수량", 55, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("SUM_SO", "수주수량", 55, false, typeof(decimal), FormatTpType.QUANTITY);

            flex3.SetCol("AVG_UM_KR_P", "평균매입단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flex3.SetCol("AVG_UM_KR_S", "평균매출단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flex3.SetCol("AVG_LT", "평균납기", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);


            flex3.SetCol("MIN_UM_KR_P", "최저매입단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flex3.SetCol("MIN_UM_KR_S", "최저매출단가", 90, false, typeof(decimal), FormatTpType.MONEY);

            flex3.SetCol("MAX_UM_KR_P", "최고매입단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flex3.SetCol("MAX_UM_KR_S", "최고매출단가", 90, false, typeof(decimal), FormatTpType.MONEY);

            flex3.SetCol("MIX2", "1M", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("MIX3", "4M", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("QT_AVAILABLE", "가용재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("MIX", "가용+발주", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("NEED_COUNT", "필요수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flex3.SetCol("EX_AMOUNT", "예상금액", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            flex3.KeyActionEnter = KeyActionEnum.MoveDown;
            flex3.SettingVersion = "18.03.20.16";
            flex3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            flex3.Rows[0].Height = 24;






            flexResult.BeginSetting(1, 1, false);

            flexResult.SetCol("CD_ITEM", "재고코드", 80);
            flexResult.SetCol("CD_SPEC", "U코드", 100);
            flexResult.SetCol("NO_DRAWING", "도면번호", 100);
            flexResult.SetCol("NO_PLATE", "품목코드", 120);
            flexResult.SetCol("NM_PLATE", "품목명", 200);
            flexResult.SetCol("NO_IMO", "IMO번호", 70);
            flexResult.SetCol("NO_ENGINE", "엔진번호", false);


            flexResult.SetCol("CNT_QTN", "견적건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("CNT_SO", "수주건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("CNT_RT_SO", "수주율", 55, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            flexResult.SetCol("SUM_QTN", "견적수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("SUM_SO", "수주수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("SUM_RT_SO", "수주율", 55, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            flexResult.SetCol("AVG_UM_KR_P", "평균매입단가", 90, false, typeof(decimal), FormatTpType.MONEY);
            flexResult.SetCol("AVG_LT", "평균납기", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);


            flexResult.SetCol("MIX2", "1M", 30, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("MIX3", "4M", 30, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("QT_AVAILABLE", "가용재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("MIX", "가용+발주", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("NEED_COUNT", "필요수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("EX_AMOUNT", "예상금액", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            flexResult.SetCol("CNT_ITEM", "재고중복", 70);
            flexResult.SetCol("CNT_DRAWING", "도면중복", 70);
            flexResult.SetCol("CNT_SPEC", "U코드중복", 70);
            flexResult.SetCol("CNT_ALL", "중복", 70);


            //flexResult.Cols["QT_INV2"].Visible = false;
            //flexResult.Cols["QT_BOOK_TOT"].Visible = false;
            //flexResult.Cols["QT_NONGR"].Visible = false;
            //flexResult.Cols["QT_HOLD_TOT"].Visible = false;
            //flexResult.Cols["STAND_PRC2"].Visible = false;
            //flexResult.Cols["BASE_PRC2"].Visible = false;



            flexResult.KeyActionEnter = KeyActionEnum.MoveDown;
            flexResult.SettingVersion = "18.03.20.19";
            flexResult.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            flexResult.Rows[0].Height = 24;





            //================================ 중복 U코드 ================================//
            flexUCODE.BeginSetting(1, 1, true);

            flexUCODE.SetCol("CD_ITEM", "재고코드", 150);
            flexUCODE.SetCol("NO_DRAWING", "도면번호", 150);
            flexUCODE.SetCol("UCODE", "U코드", 150);
            flexUCODE.SetCol("CNT", "발주진행수량", 150);

            flexUCODE.Cols["UCODE"].TextAlign = TextAlignEnum.CenterCenter;

            flexUCODE.KeyActionEnter = KeyActionEnum.MoveDown;
            flexUCODE.SettingVersion = "18.11.11.98";
            flexUCODE.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);





            //================================ IMO 확인 ================================//
            flexIMO_S.BeginSetting(1, 1, true);

            flexIMO_S.SetCol("NO_IMO", "IMO", 120);
            flexIMO_S.SetCol("NO_ENGINE", "엔진번호", 120);
            flexIMO_S.SetCol("NM_MODEL", "모델명", 120);

            flexIMO_S.Cols["NO_IMO"].TextAlign = TextAlignEnum.CenterCenter;
            flexIMO_S.Cols["NO_ENGINE"].TextAlign = TextAlignEnum.CenterCenter;

            flexIMO_S.KeyActionEnter = KeyActionEnum.MoveDown;
            flexIMO_S.SettingVersion = "18.33.12.00";
            flexIMO_S.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);



            //================================ IMO NEW 선택 ================================//
            flexIMOnew.BeginSetting(1, 1, true);

            flexIMOnew.SetCol("NO_IMO", "IMO", 120);
            flexIMOnew.SetCol("NO_ENGINE", "엔진번호", 120);
            flexIMOnew.SetCol("CNT", "아이템수", false);
            flexIMOnew.SetCol("NM_MODEL", "모델명", 120);
            flexIMOnew.SetCol("NO_EMP", "사번", false);
            flexIMOnew.SetCol("ROWNUM", "ROW", false);

            flexIMOnew.Cols["NO_IMO"].TextAlign = TextAlignEnum.CenterCenter;
            flexIMOnew.Cols["NO_ENGINE"].TextAlign = TextAlignEnum.CenterCenter;

            flexIMOnew.KeyActionEnter = KeyActionEnum.MoveDown;
            flexIMOnew.SettingVersion = "0.0.0.05";
            flexIMOnew.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            flexIMOnew.AllowEditing = true;
		}

		private void InitEvent()
		{
            btn완료.Click += new EventHandler(this.btn완료_Click);
            btn중복삭제.Click += new EventHandler(btn중복삭제_Click);
            btnIMOS.Click += new EventHandler(btnIMOS_Click);
            btn1.Click += new EventHandler(this.btn1_Click);
            btn2.Click += new EventHandler(this.btn2_Click);
            btn3.Click += new EventHandler(this.btn3_Click);
            btn4.Click += new EventHandler(this.btn4_Click);
            btn5.Click += new EventHandler(this.btn5_Click);
            btn6.Click += new EventHandler(this.btn6_Click);
            btnIMOnew.Click += new EventHandler(this.btnIMOChoice_Click);
            btnMaps.Click += new EventHandler(this.btnMaps_Click);
            btn초기화.Click += new EventHandler(this.btn초기화_Click);
            btnBtnClear.Click +=new EventHandler(this.btnBtnClear_Click);
            btnIMOadd.Click += new EventHandler(this.btnIMOadd_Click);
            btn시작.Click += new EventHandler(btn시작_Click);
            btn삭제.Click += new EventHandler(btn삭제_Click);


            btnAllinOne.Click += new EventHandler(this.btnAllinOne_Click);
		}

		protected override void InitPaint()
		{
			if (!Util.CertifyIP())
			{
				flexHULL.Cols.Remove("NO_DRAWING");
			}
		}

		#endregion

		#region ==================================================================================================== 이벤트

        private void btn중복삭제_Click(object sender, EventArgs e)
        {
            bool popupDebug = false;

            if (Control.ModifierKeys == Keys.Control)
                popupDebug = true;

            tabL.SelectedIndex = 7;

            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

            if (fileDlg.ShowDialog() != DialogResult.OK) return;

            ExcelReader excel = new ExcelReader();
            DataTable dtExcel = excel.Read(fileDlg.FileName, 1, 2);

            if (dtExcel.Rows.Count == 0)
            {
                ShowMessage("엑셀파일을 읽을 수 없습니다.");
                return;
            }

            flexUCODE.DataTable = dtExcel;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_UCODE_DELETE", true, popupDebug, GetTo.Xml(dtExcel), NO_EMP);

            flexUCODE.Binding = dTItem;
            flexUCODE.Redraw = true;

            Util.ShowMessage("진행중인 아이템 업로드 완료");
            btn중복삭제.ForeColor = Color.Red;
        }
	


		#endregion
	
		#region ==================================================================================================== 조회

        #region 1.호선선정
        
        private void btn1_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 0;


            if (string.IsNullOrEmpty(tbx호선수.Text))
            {
                ShowMessage("선택할 호선의 수를 입력해주세요."); return;
            }
            else
            {
                btn1.Enabled = false;

                dTItem = null;

                if (cbx통합.Checked)
                {
                    dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_IMO_S2", new object[]{ CD_COMPANY, NO_EMP, dtp일자.StartDateToString, dtp일자.EndDateToString,
                                                                                tbxCnt.Text, tbx엔진모델.Text, tbx제외키워드.Text, tbx호선수.Text });
                }
                else
                {
                    dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_IMO_S", new object[]{ CD_COMPANY, NO_EMP, dtp일자.StartDateToString, dtp일자.EndDateToString,
                                                                                tbxCnt.Text, tbx엔진모델.Text, tbx제외키워드.Text, tbx호선수.Text });
                }

                
                flexHULL.Binding = dTItem;
                flexHULL.Redraw = true;

                //flexIMOnew.Binding = dTItem;
                //flexIMOnew.Redraw = true;

                AddGridEffect();

                Util.ShowMessage("호선 검색 완료");
                lbl1.ForeColor = Color.Red;
            }
        }

        #endregion 호선선정

        #region 2.호선선정완료
        
        private void btn6_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 0;
            btn6.Enabled = false;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_SELECT", new object[]{ GetTo.Xml(flexHULL.DataTable), NO_EMP });

            flexHULL.Binding = dTItem;
            flexHULL.Redraw = true;

            AddGridEffect();

            Util.ShowMessage("호선 선택 완료");
            lbl2.ForeColor = Color.Red;

            
        }

        #endregion 호선선정완료

        #region 호선선정 _ NEW
        private void btnIMOChoice_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 9;

            dTItem = null;
            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_SELECT", new object[] { GetTo.Xml(flexIMOnew.DataTable), NO_EMP });

            flexIMOnew.Binding = dTItem;
            flexIMOnew.Redraw = true;

            Util.ShowMessage("호선 선택 완료");
            lbl2.ForeColor = Color.Red;
        }
        #endregion 호선선정 _ NEW

        #region 호선 아이템 동기화
        private void btnMaps_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 1;
            btnMaps.Enabled = false;

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                textBox2.Text = "Start...";

                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Extension.ToUpper() == "XLS" || file.Extension.ToUpper() == ".XLSX")
                    {
                        if (file.Name.IndexOf("~$") >= 0) continue;

                        textBox2.Text = "\r\n" + file.Name;

                        ExcelReader excel = new ExcelReader();
                        DataTable dt = excel.Read(file.FullName, 1, 2);

                        dt.Columns["PLATE NO"].ColumnName = "NO_PLATE";
                        dt.Columns["DESCRIPTION"].ColumnName = "NM_PLATE";
                        dt.Columns["U-CODE"].ColumnName = "UCODE";
                        dt.Columns["UNIT"].ColumnName = "UNIT";
                        dt.Columns["STOCK"].ColumnName = "STOCK";
                        dt.Columns["DEL"].ColumnName = "DEL";
                        dt.Columns["WEIGHT(KG)"].ColumnName = "WEIGHT";
                        dt.Columns["DWG_NO"].ColumnName = "NO_DRAWING";
                        dt.Columns["PL_DWG"].ColumnName = "PL_DRAWING";


                        try
                        {
                            // IMO 번호 가져오기
                            string fileName = file.Name.Replace(file.Extension, "");
                            string imoNumber = fileName.Substring(0, fileName.IndexOf("_"));

                            // 시리얼 번호 가져오기
                            string serial = fileName.Substring(fileName.IndexOf("_") + 1);

                            if (serial.Length == 10)
                            {
                                serial = "K" + serial.Substring(0, 2) + "00" + serial.Substring(2);
                            }
                            else if (serial.Length == 13)
                            {

                            }
                            else
                            {
                                throw new Exception("잘못된 파일");
                            }

                            string xml = GetTo.Xml(dt);

                            xml = xml.Replace("&#x5;", "");
                            xml = xml.Replace("&#x6;", "");

                            DataTable dtResult = DBMgr.GetDataTable("SP_CZ_HULL_ENGINE_ITEM_HSD", new object[] { xml, imoNumber, serial, file.Name });
                            flexMaps.Binding = dtResult;

                            FileMgr.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\Completed");
                            File.Move(file.FullName, folderBrowserDialog1.SelectedPath + "\\Completed\\" + file.Name);

                            textBox2.Text = "==> Success";
                            textBox2.SelectionStart = textBox2.TextLength;
                            textBox2.ScrollToCaret();
                        }
                        catch (Exception ex)
                        {
                            textBox2.Text = "==> Fail";

                            if (ex.Message.IndexOf("StartIndex는") >= 0)
                            {
                                textBox2.Text = " (잘못된 파일)";
                            }
                            else if (ex.Message.IndexOf("IMO번호 없음") >= 0)
                            {
                                textBox2.Text = " (" + ex.Message + ")";
                            }
                            else if (ex.Message.IndexOf("엔진시리얼 없음") >= 0)
                            {
                                textBox2.Text = " (" + ex.Message + ")";
                            }
                            else if (ex.Message.IndexOf("조회되는 DATA 없음") >= 0)
                            {
                                textBox2.Text = " (조회되는 DATA 없음)";
                            }
                            else if (ex.Message.IndexOf("길이제한 초과") >= 0)
                            {
                                textBox2.Text = " (" + ex.Message + ")";
                            }
                            else if (ex.Message.IndexOf("문자열이나 이진 데이터는 잘립니다.") >= 0)
                            {
                                textBox2.Text = " (자리수 초과)";
                            }
                            else if (ex.Message.IndexOf("파일이 이미 있으므로") >= 0)
                            {
                                textBox2.Text = " (파일 이동 오류:이미 있음)";
                            }
                            else
                            {
                                textBox2.Text = " (알수없음:" + ex.Message + ")";
                            }
                        }
                    }
                }

                textBox2.Text = "\r\nEnd...";
            }

            Util.ShowMessage("아이템 동기화 완료");
            lbl동기화.ForeColor = Color.Red;
        }
        #endregion 호선 아이템 동기화

        #region 3.호선 아이템 가져오기
        private void btn2_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 2;
            btn2.Enabled = false;

            // 아이템 선정
            if (string.IsNullOrEmpty(tbx호선수.Text))
            {
                ShowMessage("선택할 호선의 수를 입력해주세요."); return;
            }
            else
            {
                dTItem = null;

                dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_ORDER", new object[]{ CD_COMPANY, NO_EMP, dtp일자.StartDateToString, dtp일자.EndDateToString, 
                                                                                tbxCnt.Text, tbx엔진모델.Text, tbx제외키워드.Text, tbx호선수.Text });

                flexHITEM.Binding = dTItem;
                flexHITEM.Redraw = true;

                Util.ShowMessage("호선아이템 검색 완료");
                lbl3.ForeColor = Color.Red;
            }
        }
        #endregion 호선 아이템 가져오기

        #region 4.빈도분석 1
        
        private void btn3_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 3;
            btn3.Enabled = false;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_S", new object[] { CD_COMPANY, NO_EMP, dtp일자.StartDateToString, dtp일자.EndDateToString, });


            flexL.Redraw = false;
            flexL.Binding = dTItem;
            flexL.Redraw = true;

            Util.ShowMessage("빈도분석 완료");
            lbl4.ForeColor = Color.Red;
        }
        #endregion

        #region 5.중복제거
        
        private void btn4_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 4;
            btn4.Enabled = false;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_GET", new object[] { CD_COMPANY, NO_EMP});

            flexITEM.Redraw = false;
            flexITEM.Binding = dTItem;
            flexITEM.Redraw = true;

            Util.ShowMessage("중복제거 완료");
            lbl5.ForeColor = Color.Red;
        }

        #endregion 중복제거

        #region 6.재고입출고현황
        private void btn5_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 5;
            btn5.Enabled = false;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_GET_2", new object[] { CD_COMPANY, NO_EMP });

        
            flex3.Redraw = false;
            flex3.Binding = dTItem;
            flex3.Redraw = true;

            Util.ShowMessage("재고수량 검색 완료");
            lbl6.ForeColor = Color.Red;

        }
        #endregion

        #region 7.최종
        private void btn완료_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 6;
            btn완료.Enabled = false;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_RESULT", new object[] { CD_COMPANY, NO_EMP });

            flexResult.Redraw = false;
            flexResult.Binding = dTItem;
            flexResult.Redraw = true;

            Util.ShowMessage("재고발주 완료");
            lbl7.ForeColor = Color.Red;
            lbl8.ForeColor = Color.Red;
        }
        #endregion

        #region 초기화
        private void btn초기화_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 0;

            dTItem = null;

            DBMgr dbm = new DBMgr(DBConn.iU);
            dbm.Procedure = "SP_CZ_SA_HGS_ITEM_CLEAR";
            dbm.AddParameter("P_CD_COMPANY", CD_COMPANY);
            dbm.AddParameter("NO_EMP", NO_EMP);
            dbm.ExecuteNonQuery();

            lbl1.ForeColor = Color.Black;
            lbl2.ForeColor = Color.Black;
            lbl3.ForeColor = Color.Black;
            lbl4.ForeColor = Color.Black;
            lbl5.ForeColor = Color.Black;
            lbl6.ForeColor = Color.Black;
            lbl7.ForeColor = Color.Black;
            lbl8.ForeColor = Color.Black;
            lbl동기화.ForeColor = Color.Black;

            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btnMaps.Enabled = true;

            btn완료.Enabled = true;

            flex3.Binding = dTItem;
            flexIMO_S.Binding = dTItem;
            flexHITEM.Binding = dTItem;
            flexHULL.Binding = dTItem;
            flexUCODE.Binding = dTItem;
            flexL.Binding = dTItem;
            flexResult.Binding = dTItem;
            flexITEM.Binding = dTItem;
            flexMaps.Binding = dTItem;
            flexIMOnew.Binding = dTItem;

            tbxIMO.Text = "";
            textBox2.Text = "";

            imoCount = Convert.ToInt16(lbCount.Text);
            lbCount.Text = Convert.ToString(imoCount);


            Util.ShowMessage("초기화를 완료하였습니다.");
        }
        #endregion 초기화

        #region 버튼초기화
        private void btnBtnClear_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 0;

            lbl1.ForeColor = Color.Black;
            lbl2.ForeColor = Color.Black;
            lbl3.ForeColor = Color.Black;
            lbl4.ForeColor = Color.Black;
            lbl5.ForeColor = Color.Black;
            lbl6.ForeColor = Color.Black;
            lbl7.ForeColor = Color.Black;
            lbl8.ForeColor = Color.Black;
            lbl동기화.ForeColor = Color.Black;


            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btnMaps.Enabled = true;

            btn완료.Enabled = true;

            tbxIMO.Text = "";
            textBox2.Text = "";

            imoCount = Convert.ToInt16(lbCount.Text);
            lbCount.Text = Convert.ToString(imoCount);

            Util.ShowMessage("초기화를 완료하였습니다.");
        }
        #endregion 버튼초기화

        #region All In One
        
        private void btnAllinOne_Click(object sender, EventArgs e)
        {

            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_ORDER", new object[]{ CD_COMPANY, NO_EMP, dtp일자.StartDateToString, dtp일자.EndDateToString, 
                                                                                tbxCnt.Text, tbx엔진모델.Text, tbx제외키워드.Text, tbx호선수.Text });
            flexHITEM.Redraw = false;
            flexHITEM.Binding = dTItem;
            flexHITEM.Redraw = true;


            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_S", new object[] { CD_COMPANY, NO_EMP, dtp일자.StartDateToString, dtp일자.EndDateToString, });


            flexL.Redraw = false;
            flexL.Binding = dTItem;
            flexL.Redraw = true;


            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_GET", new object[] { CD_COMPANY, NO_EMP });

            flexITEM.Redraw = false;
            flexITEM.Binding = dTItem;
            flexITEM.Redraw = true;



            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_GET_2", new object[] { CD_COMPANY, NO_EMP });

            flex3.Redraw = false;
            flex3.Binding = dTItem;
            flex3.Redraw = true;



            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_RESULT", new object[] { CD_COMPANY, NO_EMP });

            flexResult.Redraw = false;
            flexResult.Binding = dTItem;
            flexResult.Redraw = true;
        }
        #endregion

        #region IMO 검색
        private void btnIMOS_Click(object sender, EventArgs e)
        {
            tabL.SelectedIndex = 8;
            dTItem = null;

            string imoSstr = tbxIMO.Text;

            if (string.IsNullOrEmpty(imoSstr))
            {
                Util.ShowMessage("검색할 IMO를 입력하세요.");
            }
            else
            {
                dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_IMO_S", new object[] { imoSstr });

                flexIMO_S.Redraw = false;
                flexIMO_S.Binding = dTItem;
                flexIMO_S.Redraw = true;
            }
        }
        #endregion IMO 검색

        #region IMO 컬럼 추가
        private void btnIMOadd_Click(object sender, EventArgs e)
        {
            //DataTable DT = new DataTable("flexIMOnew2");
            //DataRow dtr = DT.NewRow();
            //DataColumn col = null;

            //col = DT.Columns.Add("NO_IMO");
            //col.DataType = typeof(string);

            //col = DT.Columns.Add("NO_ENGINE");
            //col.DataType = typeof(string);

            //col = DT.Columns.Add("NM_MODEL");
            //col.DataType = typeof(string);

            //dtr["NO_IMO"] = tbxIMOadd.Text;
            //dtr["NO_ENGINE"] = tbxENGadd.Text;
            //dtr["NM_MODEL"] = tbx엔진모델.Text.Replace("%","");

            //DT.Rows.Add(dtr);

            //flexIMOnew2.SetDataBinding(DT, null, true);

            imoCount = Convert.ToInt16(lbCount.Text);

            if (imoCount >= flexIMOnew.Rows.Count)
                flexIMOnew.Rows.Add();

            flexIMOnew[imoCount, "NO_IMO"] = tbxIMOadd.Text;
            flexIMOnew[imoCount, "NO_ENGINE"] = tbxENGadd.Text;


            flexIMOnew[imoCount, "NM_MODEL"] = tbx엔진모델.Text;
            flexIMOnew[imoCount, "NO_EMP"] = NO_EMP;
            flexIMOnew[imoCount, "CNT"] = 0;
            flexIMOnew[imoCount, "ROWNUM"] = imoCount;

            tbxIMOadd.Text = "";
            tbxENGadd.Text = "";

            imoCount += 1;

            lbCount.Text = Convert.ToString(imoCount);
        }
        #endregion IMO 컬럼 추가

        #region imo 시작 & 삭제
        private void btn시작_Click(object sender, EventArgs e)
        {
            dTItem = null;

            dTItem = DBMgr.GetDataTable("SP_CZ_SA_HGS_ITEM_SELECT", new object[] { GetTo.Xml(flexIMOnew.DataTable), NO_EMP });

            flexIMOnew.Binding = dTItem;
            flexIMOnew.Redraw = true;

            Util.ShowMessage("IMO 입력 준비 완료");
        }


        private void btn삭제_Click(object sender, EventArgs e)
        {
            CellRange range = flexIMOnew.Selection;

            for (int i = range.r2; i >= range.r1; i--)
            {
                string lineNumber = flexIMOnew[i, "NO_IMO"].ToString();

                // 해당 행 삭제
                flexIMOnew.DataTable.Select("NO_IMO = " + lineNumber)[0].Delete();

            }
        }
        #endregion imo 시작 & 삭제


        private void AddGridEffect()
        {
                // 호선 선택 한 수 만큼 grid row color 변화
                for (int i = 1; i < flexHULL.Rows.Count; i++)
                {
                    flexHULL.SetCellStyle(i, flexHULL.Cols["NO_IMO"].Index, "BASIC");
                    flexHULL.SetCellStyle(i, flexHULL.Cols["NO_ENGINE"].Index, "BASIC");
                }


                if (flexHULL.Rows.Count > Convert.ToInt16(tbx호선수.Text))
                {
                    for (int i = 1; i <= Convert.ToInt16(tbx호선수.Text); i++)
                    {
                        flexHULL.SetCellStyle(i, flexHULL.Cols["NO_IMO"].Index, "SELECT");
                        flexHULL.SetCellStyle(i, flexHULL.Cols["NO_ENGINE"].Index, "SELECT");
                    }
                }
        }
        #endregion
	}
}
