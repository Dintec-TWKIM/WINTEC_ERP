using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Settng;
using Duzon.ERPU.Utils;
using Duzon.Windows.Print;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_MA_PITEM : PageBase
    {
        private P_CZ_MA_PITEM_BIZ _biz = new P_CZ_MA_PITEM_BIZ();
        private List<object> deleteRowList;
        private DataRow row복사;
        private bool 내부접속여부, isEzcode;
        private string _검색방법1;
        private string _검색방법2;
        private string _검색방법3;

        private bool Chk공장코드
        {
            get
            {
                return !Checker.IsEmpty(this.cbo공장코드S, this.DD("공장코드"));
            }
        }

        #region 초기화
        public P_CZ_MA_PITEM()
        {
            StartUp.Certify(this);
            this.내부접속여부 = Util.CertifyIP();

            InitializeComponent();

            DataTable dt = DBHelper.GetDataTable(@"SELECT * 
                                                   FROM CZ_MA_CODEDTL WITH(NOLOCK)
                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                 @"AND CD_FIELD = 'CZ_MA00030'
                                                   AND YN_USE = 'Y'
                                                   AND CD_SYSDEF = '" + Global.MainFrame.LoginInfo.EmployeeNo + "'");

            if (dt != null && dt.Rows.Count > 0)
                this.isEzcode = true;
            else
                this.isEzcode = false;

            this.MainGrids = new FlexGrid[] { this._flex };
        }
        
        protected override void InitLoad()
        {
            base.InitLoad();

            if (!this.isEzcode || !this.내부접속여부 || !Certify.IsLive())
            {
                this.oneGridItem48.ControlEdit.Remove(this.bpPanelControl51);
                this.pnl기본.ItemCollection.Remove(this.oneGridItem48);
                this.pnl기본.Controls.Remove(this.oneGridItem48);
            }

            if (Global.MainFrame.LoginInfo.CompanyCode != "K200" && 
                Global.MainFrame.LoginInfo.CompanyCode != "S100")
			{
                this.pnl기본.ItemCollection.Remove(this.oneGridItem32);
                this.pnl기본.Controls.Remove(this.oneGridItem32);
                this.pnl기본.ItemCollection.Remove(this.oneGridItem43);
                this.pnl기본.Controls.Remove(this.oneGridItem43);
                this.pnl기본.ItemCollection.Remove(this.oneGridItem41);
                this.pnl기본.Controls.Remove(this.oneGridItem41);
                this.pnl기본.ItemCollection.Remove(this.oneGridItem42);
                this.pnl기본.Controls.Remove(this.oneGridItem42);

                this.txtU코드1.ReadOnly = true;
                this.txtU코드2.ReadOnly = true;
            }

            this.bpc계정구분.AddItem("009", "재고품");
            this.bpc계정구분.AddItem("010", "비용");

            this.InitGrid();
            this.InitEvent();

            this.txt재고코드.MaxLength = this._biz.GetDigitPitem();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("CD_PLANT", "공장코드", false);
            this._flex.SetCol("CD_ITEM", "재고코드", 100);
            this._flex.SetCol("NM_ITEM", "재고명", 100);
            this._flex.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
            this._flex.SetCol("STND_DETAIL_ITEM", "U코드1", 100);
            this._flex.SetCol("UCODE2", "U코드2", 100);
            this._flex.SetCol("STND_ITEM", "파트번호", 100);
            this._flex.SetCol("MAT_ITEM", "아이템번호", 100);
            this._flex.SetCol("DC_OFFER", "오퍼/호선", 100);
            this._flex.SetCol("NO_STND", "도면번호", 100);

            if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
            {
                this._flex.SetCol("EZCODE", "EZ코드", 100);
            }
            
            this._flex.SetCol("CLS_ITEM", "계정구분", 60);
            this._flex.SetCol("GRP_ITEM", "품목군", 60);
            this._flex.SetCol("GRP_MFG", "제품군", 60);
            this._flex.SetCol("WEIGHT", "중량", 60, false, typeof(decimal));
            this._flex.SetCol("FG_ABC", "ABC구분", 60);
            this._flex.SetCol("CD_ZONE", "LOCATION", false);
            this._flex.SetCol("STAND_PRC", "표준단위원가", 60, true, typeof(decimal));
            this._flex.SetCol("CLS_L", "대분류", 60);
            this._flex.SetCol("CLS_M", "중분류", 60);
            this._flex.SetCol("CLS_S", "소분류", 60);
            this._flex.SetCol("CD_SL", "입고 S/L", false);
            this._flex.SetCol("CD_GISL", "출고 S/L", false);
            this._flex.SetCol("DC_RMK1", "추가정보1", 100);
            this._flex.SetCol("DC_RMK2", "추가정보2", 100);
            this._flex.SetCol("TP_PART", "내외자구분", 100);
            this._flex.SetCol("YN_USE", "사용유무", false);
            this._flex.SetCol("YN_MGMT", "관리여부", false);
            this._flex.SetCol("PARTNER", "주거래처", false);
            this._flex.SetCol("DC_KEYWORD", "키워드", 100);
            this._flex.SetCol("DC_MODEL", "적용모델", 100);
            this._flex.SetCol("UNIT_IM", "단위", 100);
            this._flex.SetCol("FG_GIR", "재고부적합", 100);
            this._flex.SetCol("DC_RMK4", "재고부적합사유", 100);
            this._flex.SetCol("NM_TP_ITEM", "품목타입", 100);
            this._flex.SetCol("NM_INSERT_ID", "등록자", 80, false);
            this._flex.SetCol("DTS_INSERT", "등록일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_UPDATE_ID", "수정자", 80, false);
            this._flex.SetCol("DTS_UPDATE", "수정일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DC_RMK", "비고", 100);
            this._flex.SetCol("DC_RMK3", "비고1", 100);

            this._flex.SetCol("LT_ITEM", "LTD(DAY)", false);
            this._flex.SetCol("LOTSIZE", "LOT(MON)", false);
            this._flex.SetCol("QT_SSTOCK", "S/S(MON)", false);
            this._flex.SetCol("QT_MIN", "MOQ(PCS)", false);
            this._flex.SetCol("LT_STDEV", "납기표준편차", false);
            this._flex.SetCol("RT_STDEV", "납기변동계수", 100);

            this._flex.SetOneGridBinding(new object[] { this.txt재고코드 }, new IUParentControl [] { this.pnl기본, this.pnl파일 });
            this._flex.SetBindningRadioButton(new RadioButtonExt[] { this.rdo관리여부_예, this.rdo관리여부_아니오 }, new string[] { "Y", "N" });
            this._flex.SetBindningRadioButton(new RadioButtonExt[] { this.rdo사용여부_사용, this.rdo사용여부_미사용 }, new string[] { "Y", "N" });
            this._flex.SetBindningRadioButton(new RadioButtonExt[] { this.rdoINQ재고매칭_예, this.rdoINQ재고매칭_아니오 }, new string[] { "Y", "N" });

            ControlBinding controlBinding1 = null;
            controlBinding1 = new ControlBinding(this._flex, new Control[] { this.pnl파일 }, new object[] { this.txt재고코드 });
            
            this._flex.SetDummyColumn("IMAGE1", "IMAGE2", "IMAGE3", "IMAGE4", "IMAGE5", "IMAGE6", "IMAGE7");

            this._flex.SetDataMap("CLS_ITEM", MF.GetCode(MF.코드.MASTER.품목.품목계정), "CODE", "NAME");
            this._flex.SetDataMap("GRP_ITEM", MF.GetCode(MF.코드.MASTER.품목.품목군), "CODE", "NAME");
            this._flex.SetDataMap("GRP_MFG", MF.GetCode(MF.코드.MASTER.품목.제품군), "CODE", "NAME");
            this._flex.SetDataMap("FG_ABC", MF.GetCode(MF.코드.MASTER.품목.ABC구분), "CODE", "NAME");
            this._flex.SetDataMap("CLS_L", MF.GetCode(MF.코드.MASTER.품목.대분류), "CODE", "NAME");
            this._flex.SetDataMap("CLS_M", MF.GetCode(MF.코드.MASTER.품목.중분류), "CODE", "NAME");
            this._flex.SetDataMap("CLS_S", MF.GetCode(MF.코드.MASTER.품목.소분류), "CODE", "NAME");
            this._flex.SetDataMap("TP_PART", MF.GetCode(MF.코드.MASTER.품목.내외자구분), "CODE", "NAME");

            this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.SetAlternateRow();
            this._flex.SetMalgunGothic();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.splitContainer1.SplitterDistance = 1824;

            this.Page_DataChanged(null, null);

            UGrant ugrant = new UGrant();

            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn품목동기화);
            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn재고평균단가계산);
            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn일괄업로드);
            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ADMIN", this.btn일괄다운로드);
            ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "WEIGHT", this.btn재고무게동기화);

            this.btn엑셀양식다운로드.Enabled = false;
            this.btn엑셀업로드.Enabled = false;
            this.btn첨부파일.Enabled = false;

            this.btn품목동기화.Enabled = false;
            this.btn재고평균단가계산.Enabled = false;
            this.btn일괄업로드.Enabled = false;
            this.btn호환단위등록.Enabled = false;
            this.btn재고무게동기화.Enabled = false;
            this.btn일괄다운로드.Enabled = false;
            this.btn납기상세.Enabled = false;
            this.btn키워드갱신.Enabled = false;

            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo공장코드S, this.GetComboData("N;MA_PLANT").Tables[0]);
            setControl.SetCombobox(this.cbo조달구분S, MF.GetCode(MF.코드.MASTER.품목.조달구분, true));
            setControl.SetCombobox(this.cbo내외자구분S, MF.GetCode(MF.코드.MASTER.품목.내외자구분, true));
            setControl.SetCombobox(this.cbo조달구분, MF.GetCode(MF.코드.MASTER.품목.조달구분, true));
            setControl.SetCombobox(this.cbo내외자구분, MF.GetCode(MF.코드.MASTER.품목.내외자구분, true));
            setControl.SetCombobox(this.cbo품목타입, MF.GetCode(MF.코드.MASTER.품목.품목타입));
            setControl.SetCombobox(this.cbo사용유무S, MF.GetCode(MF.코드.MASTER.사용유무, true));
            setControl.SetCombobox(this.cbo제품군S, MF.GetCode(MF.코드.MASTER.품목.제품군, true));
            setControl.SetCombobox(this.cboABC구분, MF.GetCode(MF.코드.MASTER.품목.ABC구분));
            setControl.SetCombobox(this.cbo재고부적합, MF.GetCode(MF.코드.MASTER.사용유무));

            bool flag = false;

            switch (BASIC.GetMAEXC("공장품목등록-첨부파일"))
            {
                case "000":
                    flag = false;
                    break;
                case "001":
                    flag = true;
                    break;
            }

            this.btn첨부파일.Visible = flag;

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();

            DataTable dt;

            dt = MA.GetCodeUser(new string[] { "000",
                                               "001",
                                               "002",
                                               "003",
                                               "004",
                                               "005",
                                               "006",
                                               "007",
                                               "008",
                                               "009",
                                               "010" }, new string[] { "재고코드",
                                                                       "재고명",
                                                                       "파트번호",
                                                                       "U코드2",
                                                                       "U코드",
                                                                       "MAKER",
                                                                       "BARCODE",
                                                                       "적용모델",
                                                                       "키워드",
                                                                       "아이템번호",
                                                                       "도면번호" }, false);

            this.cbo검색1.DataSource = dt;
            this.cbo검색1.DisplayMember = "NAME";
            this.cbo검색1.ValueMember = "CODE";

            this.cbo검색2.DataSource = dt.Copy();
            this.cbo검색2.DisplayMember = "NAME";
            this.cbo검색2.ValueMember = "CODE";

            this.cbo검색3.DataSource = dt.Copy();
            this.cbo검색3.DisplayMember = "NAME";
            this.cbo검색3.ValueMember = "CODE";

            DataTable dt1 = MA.GetCodeUser(new string[] { "000", "001", "002", "003" }, new string[] { "일치", "앞글자검색", "포함검색", "멀티검색" }, false);

            this.cbo검색방법1.DataSource = dt1;
            this.cbo검색방법1.DisplayMember = "NAME";
            this.cbo검색방법1.ValueMember = "CODE";

            this.cbo검색방법2.DataSource = dt1.Copy();
            this.cbo검색방법2.DisplayMember = "NAME";
            this.cbo검색방법2.ValueMember = "CODE";

            this.cbo검색방법3.DataSource = dt1.Copy();
            this.cbo검색방법3.DisplayMember = "NAME";
            this.cbo검색방법3.ValueMember = "CODE";
            this.cbo검색방법3.SelectedValue = "002";

            this.cbo검색방법1.SelectedValueChanged += Cbo검색방법_SelectedValueChanged;
            this.cbo검색방법2.SelectedValueChanged += Cbo검색방법_SelectedValueChanged;
            this.cbo검색방법3.SelectedValueChanged += Cbo검색방법_SelectedValueChanged;

            dt = MA.GetCodeUser(new string[] { "1",
                                               "2",
                                               "3",
                                               "4",
                                               "5",
                                               "6",
                                               "7" }, new string[] { "관련파일1",
                                                                     "관련파일2",
                                                                     "관련파일3",
                                                                     "관련파일4",
                                                                     "관련파일5",
                                                                     "관련파일6",
                                                                     "관련파일7" }, true);

            this.cbo대표파일.DataSource = dt;
            this.cbo대표파일.DisplayMember = "NAME";
            this.cbo대표파일.ValueMember = "CODE";
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn품목동기화.Click += new EventHandler(this.btn품목동기화_Click);
            this.btn재고평균단가계산.Click += new EventHandler(this.btn재고평균단가계산_Click);
            this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn품목복사.Click += new EventHandler(this.btn품목복사_Click);
            this.btn붙여넣기.Click += new EventHandler(this.btn붙여넣기_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn일괄업로드.Click += new EventHandler(this.btn일괄업로드_Click);
            this.btn호환단위등록.Click += new EventHandler(this.btn호환단위등록_Click);
            this.btn재고무게동기화.Click += new EventHandler(this.btn재고무게동기화_Click);
			this.btn키워드갱신.Click += Btn키워드갱신_Click;
			this.btn납기상세.Click += Btn납기상세_Click;

            this.btn파일열기1.Click += new EventHandler(this.btn파일열기_Click);
            this.btn파일열기2.Click += new EventHandler(this.btn파일열기_Click);
            this.btn파일열기3.Click += new EventHandler(this.btn파일열기_Click);
            this.btn파일열기4.Click += new EventHandler(this.btn파일열기_Click);
            this.btn파일열기5.Click += new EventHandler(this.btn파일열기_Click);
            this.btn파일열기6.Click += new EventHandler(this.btn파일열기_Click);
            this.btn파일열기7.Click += new EventHandler(this.btn파일열기_Click);

            this.btn파일보기1.Click += new EventHandler(this.btn파일보기_Click);
            this.btn파일보기2.Click += new EventHandler(this.btn파일보기_Click);
            this.btn파일보기3.Click += new EventHandler(this.btn파일보기_Click);
            this.btn파일보기4.Click += new EventHandler(this.btn파일보기_Click);
            this.btn파일보기5.Click += new EventHandler(this.btn파일보기_Click);
            this.btn파일보기6.Click += new EventHandler(this.btn파일보기_Click);
            this.btn파일보기7.Click += new EventHandler(this.btn파일보기_Click);

            this.btn파일추가1.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일추가2.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일추가3.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일추가4.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일추가5.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일추가6.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일추가7.Click += new EventHandler(this.btn파일추가_Click);

            this.btn파일삭제1.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn파일삭제2.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn파일삭제3.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn파일삭제4.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn파일삭제5.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn파일삭제6.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn파일삭제7.Click += new EventHandler(this.btn파일삭제_Click);

            this.bpc계정구분.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx대분류S.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx대분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx중분류S.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx중분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx소분류S.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx소분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx제품군.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx입고SL.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx출고SL.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx계정구분.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx단위.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

			this.btn일괄다운로드.Click += Btn일괄다운로드_Click;

            this._flex.AfterRowChange += _flex_AfterRowChange;
        }

		private void Btn일괄다운로드_Click(object sender, EventArgs e)
		{
            try
            {
                if (this.ShowMessage("선택된 품목의 관련파일을 일괄 다운로드 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

                string 로컬경로, 서버경로;
                WebClient wc = new WebClient();

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                        {
                            string prefix2 = dr["CD_ITEM"].ToString().Left(2);
                            string prefix3 = dr["CD_ITEM"].ToString().Left(3);

                            if (dr["CLS_L"].ToString() == "EQ" && dr["CLS_ITEM"].ToString() == "009" && dr["CD_ITEM"].ToString().Length == 9)
                                서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(dr["CD_ITEM"]) + "/";
                            else if ((prefix2 == "BF" || prefix2 == "BR" || prefix2 == "BX" || prefix2 == "SA") && prefix3 != "SAM" && prefix3 != "SAR")
                                서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(dr["CD_ITEM"]) + "/";
                            else
                                서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(dr["CD_ITEM"]) + "/";
                        }
                        else
                            서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(dr["CD_ITEM"]) + "/";

                        로컬경로 = folderBrowserDialog.SelectedPath + "/P_CZ_MA_PITEM/" + dr["CD_ITEM"].ToString() + "/";
                        Directory.CreateDirectory(로컬경로);

                        if (!string.IsNullOrEmpty(dr["IMAGE1"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE1"].ToString(), 로컬경로 + dr["IMAGE1"].ToString());

                        if (!string.IsNullOrEmpty(dr["IMAGE2"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE2"].ToString(), 로컬경로 + dr["IMAGE2"].ToString());

                        if (!string.IsNullOrEmpty(dr["IMAGE3"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE3"].ToString(), 로컬경로 + dr["IMAGE3"].ToString());

                        if (!string.IsNullOrEmpty(dr["IMAGE4"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE4"].ToString(), 로컬경로 + dr["IMAGE4"].ToString());

                        if (!string.IsNullOrEmpty(dr["IMAGE5"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE5"].ToString(), 로컬경로 + dr["IMAGE5"].ToString());

                        if (!string.IsNullOrEmpty(dr["IMAGE6"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE6"].ToString(), 로컬경로 + dr["IMAGE6"].ToString());

                        if (!string.IsNullOrEmpty(dr["IMAGE7"].ToString()))
                            wc.DownloadFile(서버경로 + dr["IMAGE7"].ToString(), 로컬경로 + dr["IMAGE7"].ToString());
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn일괄다운로드.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Cbo검색방법_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DropDownComboBox)sender).SelectedValue.ToString() != "003")
                    return;

                string name = ((Control)sender).Name;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + openFileDialog.FileName + ";Extended Properties='Excel 12.0;HDR=No'";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [sheet1$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);

                foreach (DataRow dr in dtExcel.Rows)
				{
                    if (name == this.cbo검색방법1.Name)
                        this._검색방법1 += dr[0].ToString() + "|";
                    else if (name == this.cbo검색방법2.Name)
                        this._검색방법2 += dr[0].ToString() + "|";
                    else if (name == this.cbo검색방법3.Name)
                        this._검색방법3 += dr[0].ToString() + "|";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (D.GetDecimal(this._flex["RT_STDEV"]) >= 30)
                    this.cur납기변동계수.ForeColor = Color.Red;
                else
                    this.cur납기변동계수.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn납기상세_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_MA_PITEM_LT_SUB dialog = new P_CZ_MA_PITEM_LT_SUB(this._flex["CD_ITEM"].ToString());
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn키워드갱신_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                {
                    if (this.ShowMessage("두베코 품목의 키워드를 갱신 하시겠습니까 ?", "QY2") == DialogResult.Yes)
                    {
                        DBHelper.ExecuteNonQuery("PX_CZ_MA_QLINK_ITEM_HSD", null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 메인버튼이벤트
        protected override bool IsChanged()
        {
            if (!base.IsChanged())
                return false;
            return true;
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch() || !this.Chk공장코드)
                return false;

            this.row복사 = null;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (this.deleteRowList == null)
                    this.deleteRowList = new List<object>();
                else
                    this.deleteRowList.Clear();

                if (!this.BeforeSearch()) return;

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     D.GetString(this.cbo공장코드S.SelectedValue),
                                                     this.bpc계정구분.QueryWhereIn_Pipe,
                                                     D.GetString(this.cbo사용유무S.SelectedValue),
                                                     D.GetString(this.ctx대분류S.CodeValue),
                                                     D.GetString(this.ctx중분류S.CodeValue),
                                                     D.GetString(this.ctx소분류S.CodeValue),
                                                     this.bpc품목군S.QueryWhereIn_Pipe,
                                                     D.GetString(this.cbo제품군S.SelectedValue),
                                                     D.GetString(this.ctx주거래처S.CodeValue),
                                                     D.GetString(this.cbo내외자구분S.SelectedValue),
                                                     D.GetString(this.cbo조달구분S.SelectedValue),
                                                     this.cbo검색1.SelectedValue.ToString(),
                                                     this.cbo검색2.SelectedValue.ToString(),
                                                     this.cbo검색3.SelectedValue.ToString(),
                                                     this.cbo검색방법1.SelectedValue.ToString(),
                                                     this.cbo검색방법2.SelectedValue.ToString(),
                                                     this.cbo검색방법3.SelectedValue.ToString(),
                                                     (this.cbo검색방법1.SelectedValue.ToString() == "003" ? this._검색방법1 : this.txt검색1.Text),
                                                     (this.cbo검색방법2.SelectedValue.ToString() == "003" ? this._검색방법2 : this.txt검색2.Text),
                                                     (this.cbo검색방법3.SelectedValue.ToString() == "003" ? this._검색방법3 : this.txt검색3.Text),
                                                     (this.isEzcode && this.내부접속여부 && Certify.IsLive() ? "Y" : "N") });

                if (!this.isEzcode || !this.내부접속여부 || !Certify.IsLive())
                {
                    dt.Columns.Remove("EZCODE");
                }
                
                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd() || !this.Chk공장코드) return;

                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex["CD_PLANT"] = this.cbo공장코드S.SelectedValue;
                this._flex["FG_GIR"] = "N";
                this._flex["YN_MATCH"] = "N";

                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.AddFinished();

                this.txt재고코드.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

            if (!string.IsNullOrEmpty(D.GetString(this._flex["IMAGE1"])) || !string.IsNullOrEmpty(D.GetString(this._flex["IMAGE2"])))
            {
                this.ShowMessage("등록된 이미지 삭제 후 다시 진행하시기 바랍니다.");
                return false;
            }

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;
                
                this.deleteRowList.Add(this._flex.Rows[this._flex.Row]["CD_ITEM"]);
                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            DataRow[] dataRowArray = this._flex.DataTable.AsEnumerable()
                                                         .Where(x => x.RowState == DataRowState.Modified || x.RowState == DataRowState.Added)
                                                         .ToArray();

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["CD_ITEM"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl재고코드.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["NM_ITEM"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl재고명.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["CLS_ITEM"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계정구분.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["PARTNER"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl주거래처.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["GRP_ITEM"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl품목군.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["CD_SL"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl입고SL.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["CD_GISL"].ToString() == string.Empty)
                            .Count() > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl출고SL.Text);
                return false;
            }

            if (dataRowArray.AsEnumerable()
                            .Where(x => x["STND_DETAIL_ITEM"].ToString().Length > 20)
                            .Count() > 0)
            {
                this.ShowMessage("U코드1에 20자를 초과해서 입력한 데이터가 있습니다."); // 20자 초과시 견적등록 화면에서 오류 남
                return false;
            }

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.OK;
            bool bMsgShow = false;
            string strDeleteData = null;
            
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (this.deleteRowList.Count > 0)
                    this.DeleteListShow(ref bMsgShow, ref strDeleteData);

                if (bMsgShow) dialogResult = this.ShowDetailMessage("▽아래 품목내역이 삭제됩니다.\n정말 변경사항을 저장하시겠습니까?", strDeleteData);
                if (dialogResult != DialogResult.OK || !this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다, new string[0]);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this._flex.IsDataChanged = false;
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
                if (this._flex.IsDataChanged == false) return false;

                if (this._biz.Save(this._flex.GetChanges()))
                {
                    this._flex.AcceptChanges();
                    this.deleteRowList.Clear();
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataRow[] dataRowArray;
			DataTable dt;
			DataRow newRow;
			int index;

			try
			{
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!_flex.HasNormalRow) return;

				dataRowArray = _flex.DataTable.Select("S = 'Y'");
				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					dt = new DataTable();
					dt.Columns.Add("NO_KEY");
					dt.Columns.Add("CD_QR");
					dt.Columns.Add("NO_KEY1");
					dt.Columns.Add("CD_QR1");

					newRow = dt.NewRow();
					index = 0;
					foreach (DataRow dr in dataRowArray)
					{
						if (index != 0 && index % 2 == 0)
							newRow = dt.NewRow();

						if (index % 2 == 0)
						{
							newRow["NO_KEY"] = dr["CD_ITEM"].ToString();
							newRow["CD_QR"] = "V01/D09" + dr["CD_ITEM"].ToString();
						}
						else
						{
							newRow["NO_KEY1"] = dr["CD_ITEM"].ToString();
							newRow["CD_QR1"] = "V01/D09" + dr["CD_ITEM"].ToString();
						}

						if (index % 2 != 0 || index == dataRowArray.Length - 1)
							dt.Rows.Add(newRow);

						index++;
					}

					reportHelper = new ReportHelper("R_CZ_MA_STOCK_INFO_REG", "공장품목등록");
					reportHelper.SetDataTable(dt, 1);
					reportHelper.Print();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 버튼이벤트
		private void btn품목동기화_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx대분류S.CodeValue)) return;
                if (this.ctx대분류S.CodeValue != "EQ" && this.ctx대분류S.CodeValue != "GS") return;

                if (this.ShowMessage("선택된 분류의 품목을 두베코로 동기화 하시겠습니까 ?", "QY2") == DialogResult.Yes)
                {
                    DBHelper.ExecuteNonQuery("SP_CZ_MA_PITEM_SYNC", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   Global.MainFrame.LoginInfo.CdPlant,
                                                                                   this.ctx대분류S.CodeValue,
                                                                                   this.ctx중분류S.CodeValue,
                                                                                   this.ctx소분류S.CodeValue,
                                                                                   "K200",
                                                                                   "001" });

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn품목동기화.Text);
                }
                else if (this.ShowMessage("선택된 분류의 품목을 싱가폴로 동기화 하시겠습니까 ?", "QY2") == DialogResult.Yes)
                {
                    DBHelper.ExecuteNonQuery("SP_CZ_MA_PITEM_SYNC", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   Global.MainFrame.LoginInfo.CdPlant,
                                                                                   this.ctx대분류S.CodeValue,
                                                                                   this.ctx중분류S.CodeValue,
                                                                                   this.ctx소분류S.CodeValue,
                                                                                   "S100",
                                                                                   "S01" });

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn품목동기화.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn재고평균단가계산_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (!this._flex.HasNormalRow) return;

                query = @"SELECT NEOE.FN_CZ_GET_STOCK_UM_AVG('" + Global.MainFrame.LoginInfo.CompanyCode + "', '" + D.GetString(this._flex["CD_ITEM"]) + "')";
                
                this._flex["STAND_PRC"] = D.GetDecimal(Global.MainFrame.ExecuteScalar(query));
                this.cur재고평균단가.DecimalValue = D.GetDecimal(this._flex["STAND_PRC"]);

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn재고평균단가계산.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_공장품목등록_" + Global.MainFrame.GetStringToday + ".xlsx";

                string serverPath = string.Empty;

                if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_PITEM1.xlsx";
                else
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_PITEM.xlsx";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (this._flex.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

                if (bState == false) return;

                // 확장명 XLS
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 12.0;";

                conn = new OleDbConnection(strConn);
                conn.Open();

                OleDbCommand Cmd = null;
                OleDbDataAdapter OleDBAdap = null;

                string sTableName = string.Empty;

                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                DataSet ds = new DataSet();

                // 엑셀의 1번째 시트만 데이터 만들어 주면 되므로 한 루프후 끝내면 됩니다.
                foreach (DataRow dr in dtSchema.Rows)
                {
                    OleDBAdap = new OleDbDataAdapter(dr["TABLE_NAME"].ToString(), conn);

                    OleDBAdap.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
                    OleDBAdap.AcceptChangesDuringFill = false;

                    sTableName = dr["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

                    if (dr["TABLE_NAME"].ToString().Contains("$"))
                        OleDBAdap.Fill(ds, sTableName);
                    break;
                }

                StringBuilder FldsInfo = new StringBuilder();
                StringBuilder Flds = new StringBuilder();

                // Create Field(s) String : 현재 테이블의 Field 명 생성
                foreach (DataColumn Column in ds.Tables[0].Columns)
                {
                    if (FldsInfo.Length > 0)
                    {
                        FldsInfo.Append(",");
                        Flds.Append(",");
                    }

                    FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
                    Flds.Append(Column.ColumnName.Replace("'", "''"));
                }

                // Insert Data
                MsgControl.ShowMsg("기본데이터 UPDATE 중 입니다.\n잠시만 기다려 주세요");
                
                foreach (DataRow dr in this._flex.DataTable.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (Column.ColumnName == "YN_DELETE")
                        {
                            if (Values.Length > 0) Values.Append(",");
                            Values.Append("'N'");
                            continue;
                        }
                        else if (!this._flex.DataTable.Columns.Contains(Column.ColumnName))
                            continue;

                        if (Values.Length > 0) Values.Append(",");
                        Values.Append("'" + dr[Column.ColumnName].ToString().Replace("'", "''") + "'");
                    }

                    Cmd = new OleDbCommand(
                        "INSERT INTO [" + sTableName + "$]" +
                        "(" + Flds.ToString() + ") " +
                        "VALUES (" + Values.ToString() + ")",
                        conn);
                    Cmd.ExecuteNonQuery();
                }

                bState = true;
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn엑셀양식다운로드.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
                MsgControl.CloseMsg();
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            OleDbConnection con = null;
            OpenFileDialog openFileDialog;
            DataTable dtSchema, dtExcel;
            OleDbCommand oconn;
            OleDbDataAdapter sda;
            bool isUmUpdate, isWeightUpdate;

            try
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + openFileDialog.FileName + ";Extended Properties=Excel 12.0;";

                con = new OleDbConnection(constr);
                con.Open();

                dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                sda = new OleDbDataAdapter(oconn);
                dtExcel = new DataTable();

                sda.Fill(dtExcel);

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                if (!dtExcel.Columns.Contains("CD_ITEM"))
                {
                    this.ShowMessage("품목코드에 대한 스키마가 존재하지 않습니다.");
                    return;
                }

                foreach (DataRow dataRow in dtExcel.Select("CD_ITEM IS NULL"))
                {
                    dataRow.Delete();
                }

                dtExcel.AcceptChanges();

                isUmUpdate = false;
                if (ShowMessage("표준단가를 UPDATE 하시겠습니까 ?", "QY2") == DialogResult.Yes)
                    isUmUpdate = true;

                isWeightUpdate = false;
                if (ShowMessage("무게를 UPDATE 하시겠습니까 ?", "QY2") == DialogResult.Yes)
                    isWeightUpdate = true;

                dtExcel.Columns.Add("CD_COMPANY");
                dtExcel.Columns.Add("CD_BIZAREA");
                dtExcel.Columns.Add("CD_PLANT");

                dtExcel.Columns.Add("LT_SAFE");
                dtExcel.Columns.Add("YN_PHANTOM");
                dtExcel.Columns.Add("FG_LONG");

                dtExcel.Columns.Add("DT_VALID");
                dtExcel.Columns.Add("CLS_PO");

                if (!dtExcel.Columns.Contains("EZCODE"))
                    dtExcel.Columns.Add("EZCODE");

                foreach (DataRow dr in dtExcel.Rows)
                {
                    dr["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    dr["CD_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaCode;
                    dr["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

                    dr["NM_ITEM"] = this.XML오류문자변경(dr["NM_ITEM"].ToString());
                    dr["TP_PROC"] = (dr["TP_PROC"] == DBNull.Value ? string.Empty : dr["TP_PROC"]);
                    dr["DC_RMK"] = this.XML오류문자변경(dr["DC_RMK"].ToString());

                    if (isWeightUpdate == true)
                        dr["WEIGHT"] = D.GetDecimal(dr["WEIGHT"]);
                    else
                        dr["WEIGHT"] = 0;

                    if (isUmUpdate == true)
                        dr["STAND_PRC"] = D.GetDecimal(dr["STAND_PRC"]);
                    else
                        dr["STAND_PRC"] = 0;

                    dr["LT_SAFE"] = (dr["LT_SAFE"] == DBNull.Value ? 0 : dr["LT_SAFE"]);
                    dr["YN_PHANTOM"] = (dr["YN_PHANTOM"] == DBNull.Value ? string.Empty : dr["YN_PHANTOM"]);
                    dr["FG_LONG"] = (dr["FG_LONG"] == DBNull.Value ? string.Empty : dr["FG_LONG"]);

                    dr["DT_VALID"] = "29991231";
                    dr["CLS_PO"] = (dr["CLS_PO"] == DBNull.Value ? string.Empty : dr["CLS_PO"]);

                    dr["LT_ITEM"] = (dr["LT_ITEM"] == DBNull.Value ? 0 : dr["LT_ITEM"]);
                    dr["QT_SSTOCK"] = (dr["QT_SSTOCK"] == DBNull.Value ? 0 : dr["QT_SSTOCK"]);

                    if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
                        dr["EZCODE"] = D.GetString(dr["EZCODE"]);
                    else
                        dr["EZCODE"] = string.Empty;
                }

                if (this._biz.SaveExcel(dtExcel))
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn엑셀업로드.Text });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        private void btn품목복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;

                DataRow dataRow = this._flex.GetDataRow(this._flex.Row);

                dataRow["IMAGE1"] = string.Empty;
                dataRow["DC_IMAGE1"] = string.Empty;
                dataRow["IMAGE2"] = string.Empty;
                dataRow["DC_IMAGE2"] = string.Empty;

                DataTable dataTable = this._flex.DataTable.Clone();
                dataTable.ImportRow(dataRow);

                this.row복사 = dataTable.Rows[0];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn붙여넣기_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (this.row복사 == null)
                {
                    this.ShowMessage("복사할 내용이 존재하지 않습니다.");
                }
                else
                {
                    string str = BASIC.GetMAEXC("공장품목등록-품목코드붙여넣기여부");

                    if (string.IsNullOrEmpty(str)) 
                        str = "000";

                    DataRow dataRow = this._flex.GetDataRow(this._flex.Row);

					foreach (DataColumn dataColumn in this._flex.DataTable.Columns)
					{
						//if (!(dataColumn.ColumnName == "DTS_INSERT") &&
						//	!(dataColumn.ColumnName == "DTS_UPDATE") &&
						//	(!(dataColumn.ColumnName == "INSERT_ID") &&
						//	 !(dataColumn.ColumnName == "UPDATE_ID")) &&
						//	(!(dataColumn.ColumnName == "NM_INSERT_ID") &&
						//	 !(dataColumn.ColumnName == "NM_UPDATE_ID") &&
						//	 !(dataColumn.ColumnName == "STND_ITEM")) &&
						//	(!(dataColumn.ColumnName == "CD_ITEM") &&
						//	 !(dataColumn.ColumnName == "NM_ITEM") ||
						//	 !(str == "000")))
						//	dataRow[dataColumn.ColumnName] = this.row복사[dataColumn.ColumnName];

                        if (dataColumn.ColumnName == "CLS_ITEM" ||
                            dataColumn.ColumnName == "NM_CLS_ITEM" ||
                            dataColumn.ColumnName == "UNIT_IM" ||
                            dataColumn.ColumnName == "NM_UNIT_IM" ||
                            dataColumn.ColumnName == "GRP_ITEM" ||
                            dataColumn.ColumnName == "NM_GRP_ITEM" ||
                            dataColumn.ColumnName == "CLS_L" ||
                            dataColumn.ColumnName == "NM_CLS_L" ||
                            dataColumn.ColumnName == "CLS_M" ||
                            dataColumn.ColumnName == "NM_CLS_M" ||
                            dataColumn.ColumnName == "FG_ABC" ||
                            dataColumn.ColumnName == "TP_ITEM" ||
                            dataColumn.ColumnName == "FG_GIR" ||
                            dataColumn.ColumnName == "TP_PART" ||
                            dataColumn.ColumnName == "PARTNER" ||
                            dataColumn.ColumnName == "LN_PARTNER" ||
                            dataColumn.ColumnName == "DT_VALID" ||
                            dataColumn.ColumnName == "CD_SL" ||
                            dataColumn.ColumnName == "NM_SL" ||
                            dataColumn.ColumnName == "CD_GISL" ||
                            dataColumn.ColumnName == "NM_GISL")
                        {
                            dataRow[dataColumn.ColumnName] = this.row복사[dataColumn.ColumnName];
                        }
                        else if(dataColumn.ColumnName == "YN_MGMT")
                        {
                            dataRow[dataColumn.ColumnName] = "N";
                        }
					}

					this._flex.RefreshBindng(this._flex.Row);

                    this.txt재고코드.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn일괄업로드_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_MA_PITEM_FILE_SUB dialog = new P_CZ_MA_PITEM_FILE_SUB();
                dialog.Show();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn호환단위등록_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_MA_PITEM_UNIT_SUB dialog = new P_CZ_MA_PITEM_UNIT_SUB();
                dialog.Show();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    string fileCode = D.GetString(this._flex["CD_PLANT"]) + "_" + D.GetString(this._flex["CD_ITEM"]);
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "MA", "P_CZ_MA_PITEM", fileCode, "P_CZ_MA_PITEM");
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn파일추가_Click(object sender, EventArgs e)
        {
            string buttonName, 경로, fileName, column, query;

            try
            {
                if (!this._flex.HasNormalRow) return;
                if (string.IsNullOrEmpty(this._flex["CD_ITEM"].ToString())) return;
                if (this._flex.GetDataRow(this._flex.Row).RowState == DataRowState.Added) return;
                if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
                    string prefix2 = this._flex["CD_ITEM"].ToString().Left(2);
                    string prefix3 = this._flex["CD_ITEM"].ToString().Left(3);

                    if (this._flex["CLS_L"].ToString() == "EQ" && this._flex["CLS_ITEM"].ToString() == "009" && this._flex["CD_ITEM"].ToString().Length == 9) return;
                    else if ((prefix2 == "BF" || prefix2 == "BR" || prefix2 == "BX" || prefix2 == "SA") && prefix3 != "SAM" && prefix3 != "SAR") return;
				}

                buttonName = ((Control)sender).Name;

                if (buttonName == this.btn파일추가1.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름1.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else if (buttonName == this.btn파일추가2.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름2.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else if (buttonName == this.btn파일추가3.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름3.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else if (buttonName == this.btn파일추가4.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름4.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else if (buttonName == this.btn파일추가5.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름5.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else if (buttonName == this.btn파일추가6.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름6.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else if (buttonName == this.btn파일추가7.Name)
                {
                    if (!string.IsNullOrEmpty(this.txt파일이름7.Text))
                    {
                        this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"]);
                fileName = FileMgr.GetUniqueFileName(경로, openFileDialog.FileName);
                
                if (FileUploader.UploadFile(fileName, openFileDialog.FileName, "Upload/P_CZ_MA_PITEM", Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"])) == "Success")
                {
                    column = string.Empty;

                    if (buttonName == this.btn파일추가1.Name)
                    {
                        column = "IMAGE1";
                        this.txt파일이름1.Text = fileName;
                        this._flex["IMAGE1"] = fileName;
                    }
                    else if (buttonName == this.btn파일추가2.Name)
                    {
                        column = "IMAGE2";
                        this.txt파일이름2.Text = fileName;
                        this._flex["IMAGE2"] = fileName;
                    }
                    else if (buttonName == this.btn파일추가3.Name)
                    {
                        column = "IMAGE3";
                        this.txt파일이름3.Text = fileName;
                        this._flex["IMAGE3"] = fileName;
                    }
                    else if (buttonName == this.btn파일추가4.Name)
                    {
                        column = "IMAGE4";
                        this.txt파일이름4.Text = fileName;
                        this._flex["IMAGE4"] = fileName;
                    }
                    else if (buttonName == this.btn파일추가5.Name)
                    {
                        column = "IMAGE5";
                        this.txt파일이름5.Text = fileName;
                        this._flex["IMAGE5"] = fileName;
                    }
                    else if (buttonName == this.btn파일추가6.Name)
                    {
                        column = "IMAGE6";
                        this.txt파일이름6.Text = fileName;
                        this._flex["IMAGE6"] = fileName;
                    }
                    else if (buttonName == this.btn파일추가7.Name)
                    {
                        column = "IMAGE7";
                        this.txt파일이름7.Text = fileName;
                        this._flex["IMAGE7"] = fileName;
                    }

                    #region CZ_MA_PITEM_FILE
                    DataTable dt = DBHelper.GetDataTable(@"SELECT 1 
                                                           FROM CZ_MA_PITEM_FILE WITH(NOLOCK)
                                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                          "AND CD_ITEM = '" + D.GetString(this._flex["CD_ITEM"]) + "'");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        query = @"UPDATE CZ_MA_PITEM_FILE
                                  SET " + column + " = '" + fileName + "'" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_ITEM = '" + D.GetString(this._flex["CD_ITEM"]) + "'";
                    }
                    else
                    {
                        query = string.Format(@"INSERT INTO CZ_MA_PITEM_FILE
                                                (CD_COMPANY, CD_PLANT, CD_ITEM, {0}, ID_INSERT, DTS_INSERT)
                                                VALUES
                                                ('{1}', '{2}', '{3}', '{4}', '{5}', NEOE.SF_SYSDATE(GETDATE()))", column,
                                                                                                                  Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                  Global.MainFrame.LoginInfo.CdPlant,
                                                                                                                  D.GetString(this._flex["CD_ITEM"]),
                                                                                                                  fileName,
                                                                                                                  Global.MainFrame.LoginInfo.UserID);
                    }

                    DBHelper.ExecuteScalar(query);
                    #endregion

                    #region MA_PITEM
                    if (buttonName != this.btn파일추가7.Name)
                    {
                        query = @"UPDATE MA_PITEM
                                  SET " + column + " = '" + fileName + "'" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_ITEM = '" + D.GetString(this._flex["CD_ITEM"]) + "'";

                        DBHelper.ExecuteScalar(query);
                    }
                    #endregion

                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
                else
                    this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn파일열기_Click(object sender, EventArgs e)
        {
            string buttonName, 로컬경로, 서버경로;

            try
            {
                if (!this._flex.HasNormalRow) return;

                WebClient wc = new WebClient();
                buttonName = ((Control)sender).Name;

                로컬경로 = Application.StartupPath + "/temp/";

                if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
                    string prefix2 = this._flex["CD_ITEM"].ToString().Left(2);
                    string prefix3 = this._flex["CD_ITEM"].ToString().Left(3);

                    if (this._flex["CLS_L"].ToString() == "EQ" && this._flex["CLS_ITEM"].ToString() == "009" && this._flex["CD_ITEM"].ToString().Length == 9)
                        서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";
                    else if ((prefix2 == "BF" || prefix2 == "BR" || prefix2 == "BX" || prefix2 == "SA") && prefix3 != "SAM" && prefix3 != "SAR")
                        서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";
                    else
                        서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";
                }
                else
                    서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";

                if (buttonName == this.btn파일열기1.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름1.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름1.Text, 로컬경로 + this.txt파일이름1.Text);
                        Process.Start(로컬경로 + this.txt파일이름1.Text);
                    }
                }
                else if (buttonName == this.btn파일열기2.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름2.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름2.Text, 로컬경로 + this.txt파일이름2.Text);
                        Process.Start(로컬경로 + this.txt파일이름2.Text);
                    }
                }
                else if (buttonName == this.btn파일열기3.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름3.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름3.Text, 로컬경로 + this.txt파일이름3.Text);
                        Process.Start(로컬경로 + this.txt파일이름3.Text);
                    }
                }
                else if (buttonName == this.btn파일열기4.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름4.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름4.Text, 로컬경로 + this.txt파일이름4.Text);
                        Process.Start(로컬경로 + this.txt파일이름4.Text);
                    }
                }
                else if (buttonName == this.btn파일열기5.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름5.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름5.Text, 로컬경로 + this.txt파일이름5.Text);
                        Process.Start(로컬경로 + this.txt파일이름5.Text);
                    }
                }
                else if (buttonName == this.btn파일열기6.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름6.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름6.Text, 로컬경로 + this.txt파일이름6.Text);
                        Process.Start(로컬경로 + this.txt파일이름6.Text);
                    }
                }
                else if (buttonName == this.btn파일열기7.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름7.Text))
                        return;
                    else
                    {
                        Directory.CreateDirectory(로컬경로);
                        wc.DownloadFile(서버경로 + this.txt파일이름7.Text, 로컬경로 + this.txt파일이름7.Text);
                        Process.Start(로컬경로 + this.txt파일이름7.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn파일보기_Click(object sender, EventArgs e)
        {
            string buttonName, 경로;

            try
            {
                if (!this._flex.HasNormalRow) return;

                buttonName = ((Control)sender).Name;

                if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                {
                    string prefix2 = this._flex["CD_ITEM"].ToString().Left(2);
                    string prefix3 = this._flex["CD_ITEM"].ToString().Left(3);

                    if (this._flex["CLS_L"].ToString() == "EQ" && this._flex["CLS_ITEM"].ToString() == "009" && this._flex["CD_ITEM"].ToString().Length == 9)
                        경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";
                    else if ((prefix2 == "BF" || prefix2 == "BR" || prefix2 == "BX" || prefix2 == "SA") && prefix3 != "SAM" && prefix3 != "SAR")
                        경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";
                    else
                        경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";
                }
                else
                    경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"]) + "/";

                if (buttonName == this.btn파일보기1.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름1.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }
                    
                    this.web미리보기.Navigate(경로 + this.txt파일이름1.Text);
                }
                else if (buttonName == this.btn파일보기2.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름2.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }
                    
                    this.web미리보기.Navigate(경로 + this.txt파일이름2.Text);
                }
                else if (buttonName == this.btn파일보기3.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름3.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }

                    this.web미리보기.Navigate(경로 + this.txt파일이름3.Text);
                }
                else if (buttonName == this.btn파일보기4.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름4.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }

                    this.web미리보기.Navigate(경로 + this.txt파일이름4.Text);
                }
                else if (buttonName == this.btn파일보기5.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름5.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }

                    this.web미리보기.Navigate(경로 + this.txt파일이름5.Text);
                }
                else if (buttonName == this.btn파일보기6.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름6.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }

                    this.web미리보기.Navigate(경로 + this.txt파일이름6.Text);
                }
                else if (buttonName == this.btn파일보기7.Name)
                {
                    if (string.IsNullOrEmpty(this.txt파일이름7.Text))
                    {
                        this.web미리보기.Navigate(string.Empty);
                        return;
                    }

                    this.web미리보기.Navigate(경로 + this.txt파일이름7.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn파일삭제_Click(object sender, EventArgs e)
        {
            string buttonName, 파일명, column, query;

            try
            {
                if (!this._flex.HasNormalRow) return;

                if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                {
                    string prefix2 = this._flex["CD_ITEM"].ToString().Left(2);
                    string prefix3 = this._flex["CD_ITEM"].ToString().Left(3);

                    if (this._flex["CLS_L"].ToString() == "EQ" && this._flex["CLS_ITEM"].ToString() == "009" && this._flex["CD_ITEM"].ToString().Length == 9) return;
                    else if ((prefix2 == "BF" || prefix2 == "BR" || prefix2 == "BX" || prefix2 == "SA") && prefix3 != "SAM" && prefix3 != "SAR") return;
                }

                if (this.ShowMessage("파일삭제 하시겠습니까 ?\n삭제된 파일은 서버에서도 삭제되어 복구 할 수 없습니다.", "QY2") != DialogResult.Yes) return;

                buttonName = ((Control)sender).Name;
                파일명 = string.Empty;

                if (buttonName == this.btn파일삭제1.Name)
                    파일명 = this.txt파일이름1.Text;
                else if (buttonName == this.btn파일삭제2.Name)
                    파일명 = this.txt파일이름2.Text;
                else if (buttonName == this.btn파일삭제3.Name)
                    파일명 = this.txt파일이름3.Text;
                else if (buttonName == this.btn파일삭제4.Name)
                    파일명 = this.txt파일이름4.Text;
                else if (buttonName == this.btn파일삭제5.Name)
                    파일명 = this.txt파일이름5.Text;
                else if (buttonName == this.btn파일삭제6.Name)
                    파일명 = this.txt파일이름6.Text;
                else if (buttonName == this.btn파일삭제7.Name)
                    파일명 = this.txt파일이름7.Text;

                if (FileUploader.DeleteFile("Upload/P_CZ_MA_PITEM", Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["CD_ITEM"]), 파일명))
                {
                    column = string.Empty;

                    if (buttonName == this.btn파일삭제1.Name)
                    {
                        column = "IMAGE1";
                        this.txt파일이름1.Text = string.Empty;
                        this._flex["IMAGE1"] = string.Empty;
                    }
                    else if (buttonName == this.btn파일삭제2.Name)
                    {
                        column = "IMAGE2";
                        this.txt파일이름2.Text = string.Empty;
                        this._flex["IMAGE2"] = string.Empty;
                    }
                    else if (buttonName == this.btn파일삭제3.Name)
                    {
                        column = "IMAGE3";
                        this.txt파일이름3.Text = string.Empty;
                        this._flex["IMAGE3"] = string.Empty;
                    }
                    else if (buttonName == this.btn파일삭제4.Name)
                    {
                        column = "IMAGE4";
                        this.txt파일이름4.Text = string.Empty;
                        this._flex["IMAGE4"] = string.Empty;
                    }
                    else if (buttonName == this.btn파일삭제5.Name)
                    {
                        column = "IMAGE5";
                        this.txt파일이름5.Text = string.Empty;
                        this._flex["IMAGE5"] = string.Empty;
                    }
                    else if (buttonName == this.btn파일삭제6.Name)
                    {
                        column = "IMAGE6";
                        this.txt파일이름6.Text = string.Empty;
                        this._flex["IMAGE6"] = string.Empty;
                    }
                    else if (buttonName == this.btn파일삭제7.Name)
                    {
                        column = "IMAGE7";
                        this.txt파일이름7.Text = string.Empty;
                        this._flex["IMAGE7"] = string.Empty;
                    }

                    #region CZ_MA_PITEM_FILE
                    query = @"UPDATE CZ_MA_PITEM_FILE
                              SET " + column + " = NULL" + Environment.NewLine +
                             "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                             "AND CD_ITEM = '" + D.GetString(this._flex["CD_ITEM"]) + "'";

                    DBHelper.ExecuteScalar(query);
                    #endregion

                    #region MA_PITEM
                    if (buttonName != this.btn파일삭제7.Name)
                    {
                        query = @"UPDATE MA_PITEM
                                  SET " + column + " = NULL" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_ITEM = '" + D.GetString(this._flex["CD_ITEM"]) + "'";

                        DBHelper.ExecuteScalar(query);
                    }
                    #endregion

                    this.web미리보기.Navigate(string.Empty);
                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                }
                else
                {
                    this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn재고무게동기화_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

MERGE INTO CZ_MA_KCODE_HGS AS T 
      USING (SELECT KCODE,
					WEIGHT_LOG,
					WEIGHT_HGS
             FROM CZ_MA_KCODE_HGS
			 WHERE CD_COMPANY = 'K100') S
	  ON T.CD_COMPANY = 'S100'
	  AND T.KCODE = S.KCODE
      WHEN NOT MATCHED THEN 
        INSERT (CD_COMPANY,
			    KCODE,
			    WEIGHT_LOG,
			    WEIGHT_HGS,
			    ID_INSERT,
			    DTS_INSERT) 
		VALUES ('S100',
			    S.KCODE,
			    S.WEIGHT_LOG,
			    S.WEIGHT_HGS,
			    'SYSTEM',
			    NEOE.SF_SYSDATE(GETDATE()));

UPDATE MI
SET MI.WEIGHT = MI1.WEIGHT,
	MI.ID_UPDATE = 'SYSTEM',
	MI.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM MA_PITEM MI
JOIN MA_PITEM MI1 ON MI1.CD_COMPANY = 'K100' AND MI1.CD_ITEM = MI.CD_ITEM
WHERE MI.CD_COMPANY = 'S100'
AND ISNULL(MI.WEIGHT, 0) <= 0
AND ISNULL(MI1.WEIGHT, 0) > 0";

                DBHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤이벤트
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_CODE_SUB)
                {
                    if (e.ControlName == this.ctx대분류S.Name || e.ControlName == this.ctx대분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                        return;
                    }
                    else if (e.ControlName == this.ctx중분류S.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx대분류S.CodeValue);
                        return;
                    }
                    else if (e.ControlName == this.ctx중분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx대분류.CodeValue);
                        return;
                    }
                    else if (e.ControlName == this.ctx소분류S.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx중분류S.CodeValue);
                        return;
                    }
                    else if (e.ControlName == this.ctx소분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx중분류.CodeValue);
                        return;
                    }
                    else if (e.ControlName == this.ctx제품군.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                        return;
                    }
                    else if (e.ControlName == this.ctx계정구분.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000010";
                        return;
                    }
                    else if (e.ControlName == this.ctx단위.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000004";
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (e.HelpID == HelpID.P_MA_CODE_SUB1)
                {
                    if (e.ControlName == this.bpc계정구분.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000010";
                        return;
                    }
                }
                else if (e.HelpID == HelpID.P_MA_PITEM_SUB || e.HelpID == HelpID.P_MA_SL_SUB)
                {
                    if (!this.Chk공장코드)
                    {
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P09_CD_PLANT = D.GetString(this._flex["CD_PLANT"]);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타메소드
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarDeleteButtonEnabled = this.btn품목복사.Enabled = this.btn붙여넣기.Enabled = this._flex.HasNormalRow;

                this.btn첨부파일.Enabled = true;
                this.btn엑셀양식다운로드.Enabled = true;
                this.btn엑셀업로드.Enabled = true;

                this.btn품목동기화.Enabled = true;
                this.btn재고평균단가계산.Enabled = true;
                this.btn일괄업로드.Enabled = true;
                this.btn호환단위등록.Enabled = true;
                this.btn재고무게동기화.Enabled = true;
                this.btn일괄다운로드.Enabled = true;
                this.btn납기상세.Enabled = true;
                this.btn키워드갱신.Enabled = true;

                this.ToolBarSaveButtonEnabled = this.IsChanged();
                this.pnl기본.Enabled = this._flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void DeleteListShow(ref bool bMsgShow, ref string strDeleteData)
        {
            strDeleteData = "  품목코드\n------------------------------------\n";
            for (int index = 0; index < this.deleteRowList.Count; ++index)
            {
                if (this.deleteRowList[index] != null)
                    strDeleteData += string.Format("{0}) {1}\n", index, this.deleteRowList[index].ToString());
            }
            bMsgShow = true;
        }

        private string XML오류문자변경(string text)
        {
            string result;

            result = Regex.Replace(text, "[]", ""); //아스키 제어문자 제거

            return result;
        }
        #endregion
    }
}