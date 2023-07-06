using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Utils;
using System.Data.OleDb;
using System.Text;
using Duzon.Common.Util;
using Duzon.Windows.Print;
using Duzon.ERPU.MF;
using Duzon.ERPU.Grant;
using Duzon.Common.BpControls;

namespace cz
{
    public partial class P_CZ_MA_STOCK_INFO_REG : PageBase
    {
        P_CZ_MA_STOCK_INFO_REG_BIZ _biz = new P_CZ_MA_STOCK_INFO_REG_BIZ();
        private bool 내부접속여부, isDrawing;
        private string _재고코드엑셀조건;

        public P_CZ_MA_STOCK_INFO_REG()
        {
            StartUp.Certify(this);
            this.내부접속여부 = Util.CertifyIP();

            this.InitializeComponent();

            DataTable dt = DBHelper.GetDataTable(@"SELECT * 
                                                   FROM CZ_MA_CODEDTL WITH(NOLOCK)
                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                 @"AND CD_FIELD = 'CZ_MA00030'
                                                   AND YN_USE = 'Y'
                                                   AND CD_SYSDEF = '" + Global.MainFrame.LoginInfo.EmployeeNo + "'");

            if (dt != null && dt.Rows.Count > 0)
                this.isDrawing = true;
            else
                this.isDrawing = false;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            if (!this.isDrawing || !this.내부접속여부 || !Certify.IsLive())
            {
                this.oneGridItem25.ControlEdit.Remove(this.bpPanelControl16);
                this.tabControl1.TabPages.Remove(this.tpg도면번호);
            }

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex도면번호, this._flex유코드, this._flex재고코드, this._flex매입처 };
            this._flex매입처.DetailGrids = new FlexGrid[] { this._flex품목 };

            #region 도면번호
            this._flex도면번호.BeginSetting(1, 1, false);

            this._flex도면번호.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex도면번호.SetCol("KCODE", "도면번호", 100);
            this._flex도면번호.SetCol("WEIGHT_HGS", "무게(현대)", 100);
            this._flex도면번호.SetCol("WEIGHT_LOG", "무게(물류부)", 100);
            this._flex도면번호.SetCol("WEIGHT_ITEM", "무게(재고코드)", 100);
            this._flex도면번호.SetCol("NM_PARTNER1", "소싱업체1", 100);
            this._flex도면번호.SetCol("NM_PARTNER2", "소싱업체2", 100);
            this._flex도면번호.SetCol("YN_ITEM_IMAGE", "이미지(재코코드)", 100);
            this._flex도면번호.SetCol("QT_ITEM", "견적빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex도면번호.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex도면번호.SetCol("CD_LOCATION", "로케이션", 100);
            this._flex도면번호.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex도면번호.SetCol("NO_PO", "발주번호", 100);
            this._flex도면번호.SetCol("NO_LINE", "발주항번", false);
            this._flex도면번호.SetCol("CD_ITEM", "재고코드", 100);
            this._flex도면번호.SetCol("NM_ITEM", "재고명", 100);
            this._flex도면번호.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
            this._flex도면번호.SetCol("NM_ITEM_PARTNER", "품목명", 100);
            this._flex도면번호.SetCol("YN_PACK", "포장여부", 60, false, CheckTypeEnum.Y_N);
			this._flex도면번호.SetCol("YN_GIR", "확정여부", 60, false, CheckTypeEnum.Y_N);
			this._flex도면번호.SetCol("DC_RMK", "비고", 100);
            this._flex도면번호.SetCol("NM_INSERT", "등록자", false);
            this._flex도면번호.SetCol("DTS_INSERT", "등록일", false);
            this._flex도면번호.SetCol("NM_UPDATE", "수정자", false);
            this._flex도면번호.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex도면번호.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flex도면번호.SetOneGridBinding(new object[] { }, oneGrid3);
            this._flex도면번호.VerifyNotNull = new string[] { "KCODE" };
            this._flex도면번호.SetDummyColumn("S", "DC_IMAGE1", "DC_IMAGE2", "DC_IMAGE3", "DC_IMAGE4", "DC_IMAGE5");
            this._flex도면번호.SetBindningCheckBox(this.chk재고코드이미지존재여부1, "Y", "N");

            this._flex도면번호.SettingVersion = "0.0.0.2";
            this._flex도면번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 유코드
            this._flex유코드.BeginSetting(1, 1, false);

            this._flex유코드.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex유코드.SetCol("UCODE", "유코드", 100);
            this._flex유코드.SetCol("WEIGHT_LOG", "무게(유코드)", 100);
            this._flex유코드.SetCol("WEIGHT_ITEM", "무게(재고코드)", 100);
            this._flex유코드.SetCol("NM_PARTNER1", "소싱업체1", 100);
            this._flex유코드.SetCol("NM_PARTNER2", "소싱업체2", 100);
            this._flex유코드.SetCol("YN_ITEM_IMAGE", "이미지(재코코드)", 100);
            this._flex유코드.SetCol("QT_ITEM", "견적빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex유코드.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex유코드.SetCol("CD_LOCATION", "로케이션", 100);
            this._flex유코드.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex유코드.SetCol("NO_PO", "발주번호", 100);
            this._flex유코드.SetCol("NO_LINE", "발주항번", false);
            this._flex유코드.SetCol("CD_ITEM", "재고코드", 100);
            this._flex유코드.SetCol("NM_ITEM", "재고명", 100);
            this._flex유코드.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
            this._flex유코드.SetCol("NM_ITEM_PARTNER", "품목명", 100);
            this._flex유코드.SetCol("YN_PACK", "포장여부", 60, false, CheckTypeEnum.Y_N);
			this._flex유코드.SetCol("YN_GIR", "확정여부", 60, false, CheckTypeEnum.Y_N);
			this._flex유코드.SetCol("DC_RMK", "비고", 100);
            this._flex유코드.SetCol("NM_INSERT", "등록자", false);
            this._flex유코드.SetCol("DTS_INSERT", "등록일", false);
            this._flex유코드.SetCol("NM_UPDATE", "수정자", false);
            this._flex유코드.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex유코드.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flex유코드.SetOneGridBinding(new object[] { }, oneGrid2);
            this._flex유코드.VerifyNotNull = new string[] { "UCODE" };
            this._flex유코드.SetDummyColumn("S", "DC_IMAGE1", "DC_IMAGE2", "DC_IMAGE3", "DC_IMAGE4", "DC_IMAGE5");
            this._flex유코드.SetBindningCheckBox(this.chk재고코드이미지존재여부, "Y", "N");

            this._flex유코드.SettingVersion = "0.0.0.3";
            this._flex유코드.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 재고코드
            this._flex재고코드.BeginSetting(1, 1, false);

            this._flex재고코드.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex재고코드.SetCol("CD_ITEM", "재고코드", 100);
            this._flex재고코드.SetCol("NM_ITEM", "재고명", 100);

            if (this.isDrawing && this.내부접속여부 && Certify.IsLive())
                this._flex재고코드.SetCol("NO_STND", "도면번호", 100);

            this._flex재고코드.SetCol("STND_DETAIL_ITEM", "U코드", 100);
            this._flex재고코드.SetCol("WEIGHT", "무게", 100);
            this._flex재고코드.SetCol("NM_PARTNER1", "소싱업체1", 100);
            this._flex재고코드.SetCol("NM_PARTNER2", "소싱업체2", 100);
            this._flex재고코드.SetCol("QT_ITEM", "견적빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고코드.SetCol("CD_LOCATION", "로케이션", 100);
            this._flex재고코드.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고코드.SetCol("NO_PO", "발주번호", 100);
            this._flex재고코드.SetCol("NO_LINE", "발주항번", false);
            this._flex재고코드.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
            this._flex재고코드.SetCol("NM_ITEM_PARTNER", "품목명", 100);
            this._flex재고코드.SetCol("YN_PACK", "포장여부", 60, false, CheckTypeEnum.Y_N);
            this._flex재고코드.SetCol("DC_RMK", "비고", 100);
            this._flex재고코드.SetCol("DT_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고코드.SetCol("NM_INSERT", "등록자", false);
            this._flex재고코드.SetCol("DTS_INSERT", "등록일", false);
            this._flex재고코드.SetCol("NM_UPDATE", "수정자", false);
            this._flex재고코드.SetCol("DTS_UPDATE", "수정일자", false);
            this._flex재고코드.SetCol("QT_QR", "출력갯수", 100, true, typeof(decimal), FormatTpType.QUANTITY);

            this._flex재고코드.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flex재고코드.SetOneGridBinding(new object[] { }, oneGrid4);
            this._flex재고코드.SetDummyColumn("S", "IMAGE1", "IMAGE2", "IMAGE3", "IMAGE4", "IMAGE5", "IMAGE6", "IMAGE7", "QT_QR");

            this._flex재고코드.SettingVersion = "0.0.0.5";
            this._flex재고코드.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
            
            #region 매입처
            this._flex매입처.BeginSetting(1, 1, false);

            this._flex매입처.SetCol("CD_PARTNER", "매입처코드", 100);
            this._flex매입처.SetCol("LN_PARTNER", "매입처", 100);
            this._flex매입처.SetCol("QT_ITEM", "건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex매입처.SettingVersion = "0.0.0.1";
            this._flex매입처.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
            
            #region 품목
            this._flex품목.BeginSetting(1, 1, false);

            if (this.isDrawing && this.내부접속여부 && Certify.IsLive())
                this._flex품목.SetCol("KCODE", "도면번호", 100);

            this._flex품목.SetCol("UCODE", "유코드", 100);
            this._flex품목.SetCol("WEIGHT_HGS", "무게(현대)", 100);
            this._flex품목.SetCol("WEIGHT_LOG", "무게(물류부)", 100);
            this._flex품목.SetCol("QT_ITEM", "견적빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex품목.SetCol("NO_PO", "발주번호", 100);
            this._flex품목.SetCol("NO_LINE", "발주항번", false);
            this._flex품목.SetCol("CD_ITEM", "재고코드", 100);
            this._flex품목.SetCol("NM_ITEM", "재고명", 100);
            this._flex품목.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
            this._flex품목.SetCol("NM_ITEM_PARTNER", "품목명", 100);
            this._flex품목.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex품목.SetCol("DC_RMK1", "추가정보1", 100);
            this._flex품목.SetCol("DC_RMK2", "추가정보2", 100);
            this._flex품목.SetCol("YN_PACK", "포장여부", 60, false, CheckTypeEnum.Y_N);
            this._flex품목.SetCol("DC_RMK", "비고", 100);
            this._flex품목.SetCol("NM_INSERT", "등록자", false);
            this._flex품목.SetCol("DTS_INSERT", "등록일", false);
            this._flex품목.SetCol("NM_UPDATE", "수정자", false);
            this._flex품목.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex품목.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flex품목.SettingVersion = "0.0.0.2";
            this._flex품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.btn재고출고라벨출력.Click += new EventHandler(this.btn재고출고라벨출력_Click);
            this.btn사진업로드.Click += new EventHandler(this.btn사진업로드_Click);
            this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);	
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            
            this.btn첨부파일1추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일2추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일3추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일4추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일5추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일1삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일2삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일3삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일4삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일5삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일1열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일2열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일3열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일4열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn첨부파일5열기.Click += new EventHandler(this.btn첨부파일_Click);

            this.btn유코드첨부파일1추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일2추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일3추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일4추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일5추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일1삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일2삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일3삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일4삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일5삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일1열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일2열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일3열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일4열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn유코드첨부파일5열기.Click += new EventHandler(this.btn첨부파일_Click);

            this.btn재고코드첨부파일1추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일2추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일3추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일4추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일5추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일6추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일7추가.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일1삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일2삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일3삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일4삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일5삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일6삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일7삭제.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일1열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일2열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일3열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일4열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일5열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일6열기.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn재고코드첨부파일7열기.Click += new EventHandler(this.btn첨부파일_Click);

            this.btn재고코드엑셀.Click += new EventHandler(this.btn재고코드엑셀_Click);

            this._flex매입처.AfterRowChange += new RangeEventHandler(this._flex매입처_AfterRowChange);
        }

        private void btn재고코드엑셀_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

                if (fileDlg.ShowDialog() != DialogResult.OK) return;

                ExcelReader excel = new ExcelReader();
                DataTable dtExcel = excel.Read(fileDlg.FileName);

                if (dtExcel.Rows.Count == 0)
                {
                    this.ShowMessage("엑셀파일을 읽을 수 없습니다.");
                    return;
                }

                this._재고코드엑셀조건 = string.Empty;

                foreach (DataRow row in dtExcel.Rows)
                {
                    this._재고코드엑셀조건 += "|" + row[0].ToString();
                }
                
                this._재고코드엑셀조건 = this._재고코드엑셀조건.Substring(1);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo일자구분.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "입고일자", "수정일자" }, true);
            this.cbo일자구분.DisplayMember = "NAME";
            this.cbo일자구분.ValueMember = "CODE";
            this.cbo일자구분.SelectedValue = "001";

            this.dtp조회일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp조회일자.EndDateToString = Global.MainFrame.GetStringToday;

            this.cbo발주유형.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "재고발주", "일반발주" }, true);
            this.cbo발주유형.DisplayMember = "NAME";
            this.cbo발주유형.ValueMember = "CODE";

            this.cbo재고구분.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { (Global.MainFrame.LoginInfo.CompanyCode == "K100" ? "현대" : "두산"), "기자재" }, true);
            this.cbo재고구분.DisplayMember = "NAME";
            this.cbo재고구분.ValueMember = "CODE";
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    dt = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          this.txt도면번호S.Text,
                                                          this.txt유코드S.Text,
                                                          this.ctx소싱업체.CodeValue,
                                                          this.txt재고코드S.Text,
                                                          this.txt재고명.Text,
                                                          this.txt발주번호.Text,
                                                          this.cbo일자구분.SelectedValue,
                                                          this.dtp조회일자.StartDateToString,
                                                          this.dtp조회일자.EndDateToString,
                                                          this.cur견적빈도수From.DecimalValue,
                                                          this.cur견적빈도수To.DecimalValue,
                                                          this.cbo발주유형.SelectedValue,
                                                          (this.chk현재고0제외.Checked == true ? "Y" : "N"),
                                                          (this.chk포장건제외.Checked == true ? "Y" : "N"),
                                                          (this.chk무게미등록.Checked == true ? "Y" : "N"),
                                                          (this.chk소싱업체미등록.Checked == true ? "Y" : "N"),
                                                          (this.chk이미지미등록.Checked == true ? "Y" : "N"),
                                                          this.cbo재고구분.SelectedValue,
                                                          this._재고코드엑셀조건 });

                    this._재고코드엑셀조건 = string.Empty;

                    if (!this.isDrawing || !this.내부접속여부 || !Certify.IsLive())
                        dt.Columns.Remove("NO_STND");

                    if (this.cbo발주유형.SelectedValue.ToString() == "002")
                        dt.DefaultView.Sort = "CD_LOCATION ASC, NO_PO ASC, CD_ITEM_PARTNER ASC";
                    else
                        dt.DefaultView.Sort = "CD_ITEM ASC";

                    this._flex재고코드.Binding = dt.DefaultView.ToTable();
                    this._flex재고코드.AcceptChanges();

                    if (!this._flex재고코드.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);    
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    dt = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          this.txt유코드S.Text,
                                                          this.ctx소싱업체.CodeValue,
                                                          this.txt재고코드S.Text,
                                                          this.txt재고명.Text,
                                                          this.txt발주번호.Text,
                                                          this.cbo일자구분.SelectedValue,
                                                          this.dtp조회일자.StartDateToString,
                                                          this.dtp조회일자.EndDateToString,
                                                          this.cur견적빈도수From.DecimalValue,
                                                          this.cur견적빈도수To.DecimalValue,
                                                          this.cbo발주유형.SelectedValue,
                                                          (this.chk재고관리항목제외.Checked == true ? "Y" : "N"),
                                                          (this.chk현재고0제외.Checked == true ? "Y" : "N"),
                                                          (this.chk포장건제외.Checked == true ? "Y" : "N"),
                                                          (this.chk무게미등록.Checked == true ? "Y" : "N"),
                                                          (this.chk소싱업체미등록.Checked == true ? "Y" : "N"),
                                                          (this.chk이미지미등록.Checked == true ? "Y" : "N"),
														  (this.chk협조전확정건제외.Checked == true ? "Y" : "N"),
                                                          this._재고코드엑셀조건 });

                    this._재고코드엑셀조건 = string.Empty;

                    this._flex유코드.Binding = dt;

                    if (!this._flex유코드.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                         this.txt도면번호S.Text,
                                                         this.ctx소싱업체.CodeValue,
                                                         this.txt재고코드S.Text,
                                                         this.txt재고명.Text,
                                                         this.txt발주번호.Text,
                                                         this.cbo일자구분.SelectedValue,
                                                         this.dtp조회일자.StartDateToString,
                                                         this.dtp조회일자.EndDateToString,
                                                         this.cur견적빈도수From.DecimalValue,
                                                         this.cur견적빈도수To.DecimalValue,
                                                         this.cbo발주유형.SelectedValue,
                                                         (this.chk재고관리항목제외.Checked == true ? "Y" : "N"),
                                                         (this.chk현재고0제외.Checked == true ? "Y" : "N"),
                                                         (this.chk포장건제외.Checked == true ? "Y" : "N"),
                                                         (this.chk무게미등록.Checked == true ? "Y" : "N"),
                                                         (this.chk소싱업체미등록.Checked == true ? "Y" : "N"),
                                                         (this.chk이미지미등록.Checked == true ? "Y" : "N"),
														 (this.chk협조전확정건제외.Checked == true ? "Y" : "N"),
                                                         this._재고코드엑셀조건 });

                    this._재고코드엑셀조건 = string.Empty;

                    this._flex도면번호.Binding = dt;

                    if (!this._flex도면번호.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg매입처))
                {
                    dt = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          this.txt도면번호S.Text,
                                                          this.txt유코드S.Text,
                                                          this.ctx소싱업체.CodeValue,
                                                          this.txt재고코드S.Text,
                                                          this.txt재고명.Text,
                                                          this.txt발주번호.Text,
                                                          this.cbo일자구분.SelectedValue,
                                                          this.dtp조회일자.StartDateToString,
                                                          this.dtp조회일자.EndDateToString,
                                                          this.cur견적빈도수From.DecimalValue,
                                                          this.cur견적빈도수To.DecimalValue,
                                                          this.cbo발주유형.SelectedValue,
                                                          (this.chk재고관리항목제외.Checked == true ? "Y" : "N"),
                                                          (this.chk현재고0제외.Checked == true ? "Y" : "N"),
                                                          (this.chk포장건제외.Checked == true ? "Y" : "N"),
                                                          this._재고코드엑셀조건 });

                    this._재고코드엑셀조건 = string.Empty;

                    if (!this.isDrawing || !this.내부접속여부 || !Certify.IsLive())
                        dt.Columns.Remove("KCODE");

                    var temp = dt.AsEnumerable()
                                 .GroupBy(x => x["CD_PARTNER"].ToString(), y => y, (x, y) => new
                                 {
                                     key = x,
                                     name = y.Max(z => z["NM_PARTNER"].ToString()),
                                     count = y.Count()
                                 });

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("CD_PARTNER", typeof(string));
                    dt1.Columns.Add("LN_PARTNER", typeof(string));
                    dt1.Columns.Add("QT_ITEM", typeof(decimal));

                    foreach (var temp1 in temp)
                    {
                        DataRow dr = dt1.NewRow();
                        dr["CD_PARTNER"] = temp1.key;
                        dr["LN_PARTNER"] = temp1.name;
                        dr["QT_ITEM"] = temp1.count;
                        dt1.Rows.Add(dr);
                    }

                    this._flex매입처.Binding = dt1;
                    this._flex품목.Binding = dt;

                    if (!this._flex매입처.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    this._flex유코드.Rows.Add();
                    this._flex유코드.Row = this._flex유코드.Rows.Count - 1;
                    this._flex유코드.AddFinished();
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    this._flex도면번호.Rows.Add();
                    this._flex도면번호.Row = this._flex도면번호.Rows.Count - 1;
                    this._flex도면번호.AddFinished();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
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
                MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt;
            DataRow[] dataRowArray;
            DataRow newRow;
            FlexGrid grid;
            string columnName;
            int index;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    grid = this._flex재고코드;
                    columnName = "CD_ITEM";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    grid = this._flex유코드;
                    columnName = "UCODE";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    grid = this._flex도면번호;
                    columnName = "KCODE";
                }
                else
                    return;

                if (!grid.HasNormalRow) return;

                dataRowArray = grid.DataTable.Select("S = 'Y'");
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
                            newRow["NO_KEY"] = dr[columnName].ToString() + Environment.NewLine + dr["CD_ITEM"].ToString();
                            newRow["CD_QR"] = dr["CD_QR"].ToString();
                        }
                        else
                        {
                            newRow["NO_KEY1"] = dr[columnName].ToString() + Environment.NewLine + dr["CD_ITEM"].ToString();
                            newRow["CD_QR1"] = dr["CD_QR"].ToString();
                        }

                        if (index % 2 != 0 || index == dataRowArray.Length - 1)
                            dt.Rows.Add(newRow);

                        index++;
                    }

                    reportHelper = new ReportHelper("R_CZ_MA_STOCK_INFO_REG", "재고정보등록");
                    reportHelper.SetDataTable(dt, 1);
                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
                if (this._flex도면번호.IsDataChanged == false && 
                    this._flex유코드.IsDataChanged == false &&
                    this._flex재고코드.IsDataChanged == false) 
                    return false;

                if (!this._biz.Save(this._flex도면번호.GetChanges())) 
                    return false;

                this._flex도면번호.AcceptChanges();

                if (!this._biz.Save1(this._flex유코드.GetChanges()))
                    return false;

                this._flex유코드.AcceptChanges();

                if (!this._biz.Save2(this._flex재고코드.GetChanges()))
                    return false;

                this._flex재고코드.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    if (!this._flex유코드.HasNormalRow)
                        return;

                    this._flex유코드.Rows.Remove(this._flex유코드.Row);
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    if (!this._flex도면번호.HasNormalRow)
                        return;

                    this._flex도면번호.Rows.Remove(this._flex도면번호.Row);
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn사진업로드_Click(object sender, EventArgs e)
        {
            P_CZ_MA_STOCK_INFO_REG_SUB dialog;

            try
            {
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    dialog = new P_CZ_MA_STOCK_INFO_REG_SUB("CD_ITEM");
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    dialog = new P_CZ_MA_STOCK_INFO_REG_SUB("UCODE");
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    dialog = new P_CZ_MA_STOCK_INFO_REG_SUB("KCODE");
                }
                else
                    return;
                
                dialog.Show();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            FlexGrid grid;
            string localPath, serverPath;

            try
            {
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                    grid = this._flex재고코드;
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                    grid = this._flex유코드;
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                    grid = this._flex도면번호;
                else
                    return;

                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_재고정보등록_재고코드_" + Global.MainFrame.GetStringToday + ".xls";
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_STOCK_INFO_REG_ITEM.xls";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_재고정보등록_유코드_" + Global.MainFrame.GetStringToday + ".xls";
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_STOCK_INFO_REG_UCODE.xls";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_재고정보등록_도면번호_" + Global.MainFrame.GetStringToday + ".xls";
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_STOCK_INFO_REG_KCODE.xls";
                }
                else
                    return;
                
                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (grid.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }
                else
                    bState = false;

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 8.0;";

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

                    FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] TEXT");
                    Flds.Append(Column.ColumnName.Replace("'", "''"));
                }

                // Insert Data
                foreach (DataRow dr in grid.DataTable.Rows)
                {
                    if (dr["S"].ToString() != "Y")
                        continue;

                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!grid.DataTable.Columns.Contains(Column.ColumnName)) continue;

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
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            string[] argsPk, argsPkNm;
            
            try
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (fileDlg.ShowDialog() != DialogResult.OK) return;

                string FileName = fileDlg.FileName;

                Excel excel = new Excel();
                DataTable dtExcel = null;
                dtExcel = excel.StartLoadExcel(FileName, 0, 3); // 3번째 라인부터 저장

                // 필요한 컬럼 존재 유무 파악
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    argsPk = new string[] { "CD_ITEM" };
                    argsPkNm = new string[] { DD("재고코드") };
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    argsPk = new string[] { "UCODE" };
                    argsPkNm = new string[] { DD("유코드") };
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    argsPk = new string[] { "KCODE" };
                    argsPkNm = new string[] { DD("도면번호") };
                }
                else
                    return;

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                DataTable tmpDt = new DataTable();
                tmpDt.Columns.Add("LN_PARTNER");
                tmpDt.Columns.Add("DC_RMK");

                string 미지정목록 = string.Empty;

                foreach (DataRow dr in ComFunc.getGridGroupBy(dtExcel, new string[] { "CD_PARTNER1" }, true).Rows)
                {
                    if (!string.IsNullOrEmpty(dr["CD_PARTNER1"].ToString()) &&
                        DBHelper.GetDataTable(@"SELECT 1 
                                                FROM MA_PARTNER WITH(NOLOCK)
                                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                               "AND CD_PARTNER = '" + dr["CD_PARTNER1"].ToString() + "'").Rows.Count == 0)
                    {
                        DataTable tmpDt1 = DBHelper.GetDataTable(@"SELECT CD_FLAG1 
                                                                   FROM MA_CODEDTL MC
                                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                 @"AND CD_FIELD = 'CZ_MA00027'
                                                                   AND NM_SYSDEF = '" + dr["CD_PARTNER1"].ToString().Replace("㈜", "").Replace("(주)", "") + "'");

                        if (tmpDt1 != null && tmpDt1.Rows.Count == 1)
                        {
                            DataRow[] dataRowArray = dtExcel.Select("CD_PARTNER1 = '" + dr["CD_PARTNER1"].ToString() + "'");

                            foreach (DataRow dr1 in dataRowArray)
                            {
                                dr1["CD_PARTNER1"] = tmpDt1.Rows[0]["CD_FLAG1"].ToString();
                            }
                        }
                        else
                        {
                            DataRow newRow = tmpDt.NewRow();
                            newRow["LN_PARTNER"] = dr["CD_PARTNER1"].ToString();
                            tmpDt.Rows.Add(newRow);
                        }
                    }
                }

                foreach(DataRow dr in ComFunc.getGridGroupBy(dtExcel, new string[] { "CD_PARTNER2" }, true).Rows)
                {
                    if (!string.IsNullOrEmpty(dr["CD_PARTNER2"].ToString()) &&
                        tmpDt.Select("LN_PARTNER = '" + dr["CD_PARTNER2"].ToString() + "'").Length == 0 &&
                        DBHelper.GetDataTable(@"SELECT 1 
                                                FROM MA_PARTNER WITH(NOLOCK)
                                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                               "AND CD_PARTNER = '" + dr["CD_PARTNER2"].ToString() + "'").Rows.Count == 0)
                    {
                        DataTable tmpDt1 = DBHelper.GetDataTable(@"SELECT CD_FLAG1 
                                                                   FROM MA_CODEDTL MC
                                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                 @"AND CD_FIELD = 'CZ_MA00027'
                                                                   AND NM_SYSDEF = '" + dr["CD_PARTNER2"].ToString().Replace("㈜", "").Replace("(주)", "") + "'");

                        if (tmpDt1 != null && tmpDt1.Rows.Count == 1)
                        {
                            DataRow[] dataRowArray = dtExcel.Select("CD_PARTNER2 = '" + dr["CD_PARTNER2"].ToString() + "'");

                            foreach (DataRow dr1 in dataRowArray)
                            {
                                dr1["CD_PARTNER2"] = tmpDt1.Rows[0]["CD_FLAG1"].ToString();
                            }
                        }
                        else
                        {
                            DataRow newRow = tmpDt.NewRow();
                            newRow["LN_PARTNER"] = dr["CD_PARTNER2"].ToString();
                            tmpDt.Rows.Add(newRow);
                        }   
                    }
                }

                if (tmpDt.Rows.Count > 0)
                {
                    foreach (DataRow dr in tmpDt.Rows)
                    {
                        string 거래처명 = dr["LN_PARTNER"].ToString().Replace("㈜", "").Replace("(주)", "");

                        DataTable tmpDt1 = DBHelper.GetDataTable(@"SELECT CD_PARTNER, 
                                                                          LN_PARTNER 
                                                                   FROM MA_PARTNER WITH(NOLOCK)
                                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                  "AND LN_PARTNER LIKE '%" + 거래처명 + "%'");

                        if (tmpDt1 != null && tmpDt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in tmpDt1.Rows)
                            {
                                dr["DC_RMK"] += dr1["LN_PARTNER"].ToString() + "(" + dr1["CD_PARTNER"].ToString() + "), ";
                            }
                        }

                        미지정목록 += dr["LN_PARTNER"].ToString() + " : " + dr["DC_RMK"].ToString() + Environment.NewLine;
                    }

                    this.ShowDetailMessage("거래처코드가 지정되지 않은 소싱업체목록 입니다." + Environment.NewLine + 
                                           "[더보기] 버튼을 눌러 목록을 확인하고 거래처 코드 지정 후 재업로드 하시기 바랍니다.", 미지정목록);
                    return;
                }

                dtExcel.AcceptChanges();

                foreach (DataRow dr in dtExcel.Rows)
                {
                    if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                    {
                        dr["WEIGHT"] = 0;
                    }
                    else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                    {
                        dr["WEIGHT_LOG"] = 0;
                    }
                    else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                    {
                        dr["WEIGHT_LOG"] = 0;
                    }
                    else
                        return;
                }
                
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    this._biz.Save2(dtExcel);
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    this._biz.Save1(dtExcel);
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    this._biz.Save(dtExcel);
                }

                this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                #region 재고코드
                if (name == this.btn재고코드첨부파일1추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일1, "IMAGE1");
                }
                else if (name == this.btn재고코드첨부파일2추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일2, "IMAGE2");
                }
                else if (name == this.btn재고코드첨부파일3추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일3, "IMAGE3");
                }
                else if (name == this.btn재고코드첨부파일4추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일4, "IMAGE4");
                }
                else if (name == this.btn재고코드첨부파일5추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일5, "IMAGE5");
                }
                else if (name == this.btn재고코드첨부파일6추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일6, "IMAGE6");
                }
                else if (name == this.btn재고코드첨부파일7추가.Name)
                {
                    this.파일추가(this.txt재고코드첨부파일7, "IMAGE7");
                }
                else if (name == this.btn재고코드첨부파일1삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일1, "IMAGE1");
                }
                else if (name == this.btn재고코드첨부파일2삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일2, "IMAGE2");
                }
                else if (name == this.btn재고코드첨부파일3삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일3, "IMAGE3");
                }
                else if (name == this.btn재고코드첨부파일4삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일4, "IMAGE4");
                }
                else if (name == this.btn재고코드첨부파일5삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일5, "IMAGE5");
                }
                else if (name == this.btn재고코드첨부파일6삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일6, "IMAGE6");
                }
                else if (name == this.btn재고코드첨부파일7삭제.Name)
                {
                    this.파일삭제(this.txt재고코드첨부파일7, "IMAGE7");
                }
                else if (name == this.btn재고코드첨부파일1열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일1);
                }
                else if (name == this.btn재고코드첨부파일2열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일2);
                }
                else if (name == this.btn재고코드첨부파일3열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일3);
                }
                else if (name == this.btn재고코드첨부파일4열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일4);
                }
                else if (name == this.btn재고코드첨부파일5열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일5);
                }
                else if (name == this.btn재고코드첨부파일6열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일6);
                }
                else if (name == this.btn재고코드첨부파일7열기.Name)
                {
                    this.파일열기(this.txt재고코드첨부파일7);
                }
                #endregion   

                #region 도면번호
                else if (name == this.btn첨부파일1추가.Name)
                {
                    this.파일추가(this.txt첨부파일1, "DC_IMAGE1");
                }
                else if (name == this.btn첨부파일2추가.Name)
                {
                    this.파일추가(this.txt첨부파일2, "DC_IMAGE2");
                }
                else if (name == this.btn첨부파일3추가.Name)
                {
                    this.파일추가(this.txt첨부파일3, "DC_IMAGE3");
                }
                else if (name == this.btn첨부파일4추가.Name)
                {
                    this.파일추가(this.txt첨부파일4, "DC_IMAGE4");
                }
                else if (name == this.btn첨부파일5추가.Name)
                {
                    this.파일추가(this.txt첨부파일5, "DC_IMAGE5");
                }
                else if (name == this.btn첨부파일1삭제.Name)
                {
                    this.파일삭제(this.txt첨부파일1, "DC_IMAGE1");
                }
                else if (name == this.btn첨부파일2삭제.Name)
                {
                    this.파일삭제(this.txt첨부파일2, "DC_IMAGE2");
                }
                else if (name == this.btn첨부파일3삭제.Name)
                {
                    this.파일삭제(this.txt첨부파일3, "DC_IMAGE3");
                }
                else if (name == this.btn첨부파일4삭제.Name)
                {
                    this.파일삭제(this.txt첨부파일4, "DC_IMAGE4");
                }
                else if (name == this.btn첨부파일5삭제.Name)
                {
                    this.파일삭제(this.txt첨부파일5, "DC_IMAGE5");
                }
                else if (name == this.btn첨부파일1열기.Name)
                {
                    this.파일열기(this.txt첨부파일1);
                }
                else if (name == this.btn첨부파일2열기.Name)
                {
                    this.파일열기(this.txt첨부파일2);
                }
                else if (name == this.btn첨부파일3열기.Name)
                {
                    this.파일열기(this.txt첨부파일3);
                }
                else if (name == this.btn첨부파일4열기.Name)
                {
                    this.파일열기(this.txt첨부파일4);
                }
                else if (name == this.btn첨부파일5열기.Name)
                {
                    this.파일열기(this.txt첨부파일5);
                }
                #endregion

                #region 유코드
                else if (name == this.btn유코드첨부파일1추가.Name)
                {
                    this.파일추가(this.txt유코드첨부파일1, "DC_IMAGE1");
                }
                else if (name == this.btn유코드첨부파일2추가.Name)
                {
                    this.파일추가(this.txt유코드첨부파일2, "DC_IMAGE2");
                }
                else if (name == this.btn유코드첨부파일3추가.Name)
                {
                    this.파일추가(this.txt유코드첨부파일3, "DC_IMAGE3");
                }
                else if (name == this.btn유코드첨부파일4추가.Name)
                {
                    this.파일추가(this.txt유코드첨부파일4, "DC_IMAGE4");
                }
                else if (name == this.btn유코드첨부파일5추가.Name)
                {
                    this.파일추가(this.txt유코드첨부파일5, "DC_IMAGE5");
                }
                else if (name == this.btn유코드첨부파일1삭제.Name)
                {
                    this.파일삭제(this.txt유코드첨부파일1, "DC_IMAGE1");
                }
                else if (name == this.btn유코드첨부파일2삭제.Name)
                {
                    this.파일삭제(this.txt유코드첨부파일2, "DC_IMAGE2");
                }
                else if (name == this.btn유코드첨부파일3삭제.Name)
                {
                    this.파일삭제(this.txt유코드첨부파일3, "DC_IMAGE3");
                }
                else if (name == this.btn유코드첨부파일4삭제.Name)
                {
                    this.파일삭제(this.txt유코드첨부파일4, "DC_IMAGE4");
                }
                else if (name == this.btn유코드첨부파일5삭제.Name)
                {
                    this.파일삭제(this.txt유코드첨부파일5, "DC_IMAGE5");
                }
                else if (name == this.btn유코드첨부파일1열기.Name)
                {
                    this.파일열기(this.txt유코드첨부파일1);
                }
                else if (name == this.btn유코드첨부파일2열기.Name)
                {
                    this.파일열기(this.txt유코드첨부파일2);
                }
                else if (name == this.btn유코드첨부파일3열기.Name)
                {
                    this.파일열기(this.txt유코드첨부파일3);
                }
                else if (name == this.btn유코드첨부파일4열기.Name)
                {
                    this.파일열기(this.txt유코드첨부파일4);
                }
                else if (name == this.btn유코드첨부파일5열기.Name)
                {
                    this.파일열기(this.txt유코드첨부파일5);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn재고출고라벨출력_Click(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataRow[] dataRowArray;
            DataTable dt;
            DataRow newRow;
            int index;

            try
            {
                if (!this._flex재고코드.HasNormalRow) return;

                dataRowArray = this._flex재고코드.DataTable.Select("S = 'Y'");
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

                    int totalCount = D.GetInt(this._flex재고코드.DataTable.Compute("SUM(QT_QR)", "S = 'Y'"));

                    foreach (DataRow dr in dataRowArray)
                    {
                        for (int index1 = 0; index1 < D.GetInt(dr["QT_QR"]); index1++)
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

                            if (index % 2 != 0 || index == totalCount - 1)
                                dt.Rows.Add(newRow);

                            index++;
                        }
                    }

                    reportHelper = new ReportHelper("R_CZ_MA_STOCK_INFO_REG", "재고출고라벨");
                    reportHelper.SetDataTable(dt, 1);
                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 파일추가(TextBoxExt control, string columnName)
        {
            string serverPath, fileName, query, key;

            try
            {
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    if (!this._flex재고코드.HasNormalRow) return;

                    key = this._flex재고코드["CD_ITEM"].ToString();
                    serverPath = "/Upload/P_CZ_MA_PITEM/";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    if (!this._flex유코드.HasNormalRow) return;

                    key = this._flex유코드["UCODE"].ToString();
                    serverPath = "/Upload/P_CZ_MA_STOCK_INFO_REG/";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    if (!this._flex도면번호.HasNormalRow) return;

                    key = this._flex도면번호["KCODE"].ToString();
                    serverPath = "/Upload/P_CZ_MA_STOCK_INFO_REG/";
                }
                else
                    return;

                if (!string.IsNullOrEmpty(control.Text))
                {
                    this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                    return;
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                fileName = FileMgr.GetUniqueFileName(Global.MainFrame.HostURL + serverPath + Global.MainFrame.LoginInfo.CompanyCode + "/" + key, openFileDialog.FileName);

                if (FileUploader.UploadFile(fileName, openFileDialog.FileName, serverPath, Global.MainFrame.LoginInfo.CompanyCode + "/" + key) == "Success")
                {
                    control.Text = fileName;

                    if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                    {
                        this._flex재고코드[columnName] = fileName;

                        #region CZ_MA_PITEM_FILE
                        query = @"SELECT 1 
                                  FROM CZ_MA_PITEM_FILE WITH(NOLOCK)
                                  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_ITEM = '" + key + "'";

                        DataTable dt = DBHelper.GetDataTable(query);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            query = @"UPDATE CZ_MA_PITEM_FILE
                                      SET " + columnName + " = '" + fileName + "'" + Environment.NewLine +
                                     "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                     "AND CD_ITEM = '" + key + "'";
                        }
                        else
                        {
                            query = string.Format(@"INSERT INTO CZ_MA_PITEM_FILE
                                                    (CD_COMPANY, CD_PLANT, CD_ITEM, {0}, ID_INSERT, DTS_INSERT)
                                                    VALUES
                                                    ('{1}', '{2}', '{3}', '{4}', '{5}', NEOE.SF_SYSDATE(GETDATE()))", columnName,
                                                                                                                      Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                      Global.MainFrame.LoginInfo.CdPlant,
                                                                                                                      key,
                                                                                                                      fileName,
                                                                                                                      Global.MainFrame.LoginInfo.UserID);
                        }

                        DBHelper.ExecuteScalar(query);
                        #endregion

                        #region MA_PITEM
                        if (columnName != "IMAGE7")
                        {
                            query = @"UPDATE MA_PITEM
                                      SET " + columnName + " = '" + fileName + "'" + Environment.NewLine +
                                     "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                     "AND CD_ITEM = '" + key + "'";

                            DBHelper.ExecuteScalar(query);
                        }
                        #endregion
                    }
                    else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                    {
                        this._flex유코드[columnName] = fileName;

                        query = @"UPDATE CZ_MA_UCODE_HGS
                                  SET " + columnName + " = '" + fileName + "'" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND UCODE = '" + key + "'";

                        DBHelper.ExecuteScalar(query);
                    }
                    else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                    {
                        this._flex도면번호[columnName] = fileName;

                        query = @"UPDATE CZ_MA_KCODE_HGS
                                  SET " + columnName + " = '" + fileName + "'" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND KCODE = '" + key + "'";

                        DBHelper.ExecuteScalar(query);
                    }
                    else
                        return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 파일삭제(TextBoxExt control, string columnName)
        {
            string query, key, serverPath;

            try
            {
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    if (!this._flex재고코드.HasNormalRow) return;

                    key = this._flex재고코드["CD_ITEM"].ToString();
                    serverPath = "/Upload/P_CZ_MA_PITEM";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    if (!this._flex유코드.HasNormalRow) return;

                    key = this._flex유코드["UCODE"].ToString();
                    serverPath = "Upload/P_CZ_MA_STOCK_INFO_REG";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    if (!this._flex도면번호.HasNormalRow) return;

                    key = this._flex도면번호["KCODE"].ToString();
                    serverPath = "Upload/P_CZ_MA_STOCK_INFO_REG";
                }
                else
                    return;

                if (this.ShowMessage("파일을 삭제 하시겠습니까 ?", "QY2") != DialogResult.Yes) return;

                if (FileUploader.DeleteFile(serverPath, Global.MainFrame.LoginInfo.CompanyCode + "/" + key, control.Text))
                {
                    control.Text = string.Empty;
                    
                    if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                    {
                        this._flex재고코드[columnName] = string.Empty;

                        #region CZ_MA_PITEM_FILE
                        query = @"UPDATE CZ_MA_PITEM_FILE
                                  SET " + columnName + " = NULL" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_ITEM = '" + key + "'";
                        
                        DBHelper.ExecuteScalar(query);
                        #endregion

                        #region MA_PITEM
                        if (columnName != "IMAGE7")
                        {
                            query = @"UPDATE MA_PITEM
                                      SET " + columnName + " = NULL" + Environment.NewLine +
                                     "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                     "AND CD_ITEM = '" + key + "'";

                            DBHelper.ExecuteScalar(query);
                        }
                        #endregion
                    }
                    else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                    {
                        this._flex유코드[columnName] = string.Empty;

                        query = @"UPDATE CZ_MA_UCODE_HGS
                                  SET " + columnName + " = NULL" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND UCODE = '" + key + "'";
                        
                        DBHelper.ExecuteScalar(query);
                    }
                    else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                    {
                        this._flex도면번호[columnName] = string.Empty;

                        query = @"UPDATE CZ_MA_KCODE_HGS
                                  SET " + columnName + " = NULL" + Environment.NewLine +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND KCODE = '" + key + "'";

                        DBHelper.ExecuteScalar(query);
                    }
                    else
                        return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 파일열기(TextBoxExt control)
        {
            string localPath, serverPath, key;

            try
            {
                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    if (!this._flex재고코드.HasNormalRow) return;

                    key = this._flex재고코드["CD_ITEM"].ToString();
                    serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "/";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg유코드))
                {
                    if (!this._flex유코드.HasNormalRow) return;

                    key = this._flex유코드["UCODE"].ToString();
                    serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_STOCK_INFO_REG/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "/";
                }
                else if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg도면번호))
                {
                    if (!this._flex도면번호.HasNormalRow) return;

                    key = this._flex도면번호["KCODE"].ToString();
                    serverPath = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_STOCK_INFO_REG/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + key + "/";
                }
                else
                    return;
                
                WebClient wc = new WebClient();

                localPath = Application.StartupPath + "/temp/";

                if (string.IsNullOrEmpty(control.Text))
                    return;
                else
                {
                    Directory.CreateDirectory(localPath);
                    wc.DownloadFile(serverPath + control.Text, localPath + control.Text);
                    Process.Start(localPath + control.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex매입처_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this._flex품목.RowFilter = "CD_PARTNER = '" + this._flex매입처["CD_PARTNER"].ToString() + "'";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }	
        }
    }
}
