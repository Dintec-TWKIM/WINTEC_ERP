using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.Utils;
using System.IO;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Xml;

namespace cz
{
    /// <summary>
    /// ================================================
    /// AUTHOR      : 김기현
    /// CREATE DATE : 2015-03-18 오후 8:17
    /// 
    /// MODULE      : MASTER
    /// SYSTEM      : 
    /// SUBSYSTEM   : 
    /// PAGE        : 
    /// PROJECT     : 
    /// DESCRIPTION : 호선 등록 화면
    /// ================================================ 
    /// CHANGE HISTORY
    /// v1.0 : 2015-03-18 오후 8:17 신규 생성
    /// ================================================
    /// </summary>

    public partial class P_CZ_MA_HULL : PageBase
    {
        #region 생성자 & 전역변수
        P_CZ_MA_HULL_BIZ _biz;
        OpenFileDialog _fileDlg;
        Excel _excel;
        private bool 내부접속여부, isAdmin, isEzcode, isExcel, isLeader, isEng;
		Image fileIcon;

		public P_CZ_MA_HULL()
        {
            if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
                StartUp.Certify(this);

            this.내부접속여부 = Util.CertifyIP();
            InitializeComponent();

			UGrant ugrant = new UGrant();
			this.isAdmin = ugrant.GrantButton("P_CZ_MA_HULL", "ADMIN");
            this.isExcel = ugrant.GrantButton("P_CZ_MA_HULL", "EXCEL");
            this.isLeader = ugrant.GrantButton("P_CZ_MA_HULL", "LEADER");
            this.isEng = ugrant.GrantButton("P_CZ_MA_HULL", "ENG");

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

			this.InitControl();
		}

        public P_CZ_MA_HULL(string IMO번호, string 호선번호)
            : this()
        {
            this.bpcIMO번호.AddItem(IMO번호, 호선번호);
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_MA_HULL_BIZ();
            this._fileDlg = new OpenFileDialog();
            this._excel = new Excel();

            this.InitGrid();
            this.InitEvent();

			this.fileIcon = imageList1.Images[0];
		}

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex호선정보, this._flex엔진정보, this._flex기부속정보, this._flex매입처정보, this._flex유형정보, this._flex기자재정보 };
            this._flex호선정보.DetailGrids = new FlexGrid[] { this._flex엔진정보, this._flex기부속정보, this._flex매입처정보, this._flex유형정보, this._flex기자재정보 };
            this._flex엔진정보.DetailGrids = new FlexGrid[] { this._flex기부속정보 };
            this._flex매입처정보.DetailGrids = new FlexGrid[] { this._flex유형정보, this._flex기자재정보 };
            this._flex유형정보.DetailGrids = new FlexGrid[] { this._flex기자재정보 };

			#region 호선정보
			this._flex호선정보.BeginSetting(1, 1, false);

            this._flex호선정보.SetCol("NO_IMO", "IMO번호", 60);
            this._flex호선정보.SetCol("NO_HULL", "호선번호", 60);
            this._flex호선정보.SetCol("NM_VESSEL", "호선명", 80);
            this._flex호선정보.SetCol("CALL_SIGN", "호출부호", 80);
            this._flex호선정보.SetCol("TP_SHIP", "호선유형", false);
            this._flex호선정보.SetCol("NM_TP_SHIP", "호선유형", 80);
            this._flex호선정보.SetCol("CD_PARTNER", "운항선사", false);
            this._flex호선정보.SetCol("LN_PARTNER", "운항선사", 100);
            this._flex호선정보.SetCol("NM_SHIP_OWNER", "선주", 80);
            this._flex호선정보.SetCol("DC_SHIPBUILDER", "조선소", 100);
            this._flex호선정보.SetCol("NM_SHIP_YARD", "조선소(구)", false);
            this._flex호선정보.SetCol("DT_SHIP_DLV", "선박납기일자", 60, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex호선정보.SetCol("DTS_ETRYPT", "최근국내입항일자", 100);
            this._flex호선정보.SetCol("YN_FILE", "파일첨부여부", 100);
            
            this._flex호선정보.SetCol("ME1_MODEL", "대형엔진모델", false);
            this._flex호선정보.SetCol("ME1_MAKER", "대형엔진제조사", false);
            this._flex호선정보.SetCol("ME1_CLS_M", "대형엔진중분류", false);
            this._flex호선정보.SetCol("ME1_SERIAL", "대형엔진일련번호", false);
            this._flex호선정보.SetCol("GE1_MODEL", "중형엔진1모델", false);
            this._flex호선정보.SetCol("GE1_MAKER", "중형엔진1제조사", false);
            this._flex호선정보.SetCol("GE1_CLS_M", "중형엔진1중분류", false);
            this._flex호선정보.SetCol("GE1_SERIAL", "중형엔진1일련번호", false);
            this._flex호선정보.SetCol("GE2_MODEL", "중형엔진2모델", false);
            this._flex호선정보.SetCol("GE2_MAKER", "중형엔진2제조사", false);
            this._flex호선정보.SetCol("GE2_CLS_M", "중형엔진2중분류", false);
            this._flex호선정보.SetCol("GE2_SERIAL", "중형엔진2일련번호", false);
            this._flex호선정보.SetCol("TC1_MODEL", "터보차저모델", false);
            this._flex호선정보.SetCol("TC1_MAKER", "터보차저제조사", false);
            this._flex호선정보.SetCol("TC1_CLS_M", "터보차저중분류", false);
            this._flex호선정보.SetCol("TC1_SERIAL", "터보차저일련번호", false);
            this._flex호선정보.SetCol("TC2_MODEL", "터보차저1모델", false);
            this._flex호선정보.SetCol("TC2_MAKER", "터보차저1제조사", false);
            this._flex호선정보.SetCol("TC2_CLS_M", "터보차저1중분류", false);
            this._flex호선정보.SetCol("TC2_SERIAL", "터보차저1일련번호", false);
            this._flex호선정보.SetCol("TC3_MODEL", "터보차저2모델", false);
            this._flex호선정보.SetCol("TC3_MAKER", "터보차저2제조사", false);
            this._flex호선정보.SetCol("TC3_CLS_M", "터보차저2중분류", false);
            this._flex호선정보.SetCol("TC3_SERIAL", "터보차저2일련번호", false);

            this._flex호선정보.SetCol("INVOICE_COMPANY", "계산서회사", false);
            this._flex호선정보.SetCol("INVOICE_TEL", "계산서전화", false);
            this._flex호선정보.SetCol("INVOICE_ADDRESS", "계산서주소", false);
            this._flex호선정보.SetCol("INVOICE_EMAIL", "계산서메일", false);
            this._flex호선정보.SetCol("INVOICE_RMK", "계산서비고", false);

			this._flex호선정보.SetCol("NO_SISTER", "자매그룹번호", false);
			this._flex호선정보.SetCol("CD_SISTER", "자매유형", false);

            this._flex호선정보.SetOneGridBinding(new object[] { this.txtIMO번호 }, new IUParentControl[] { this.pnl일반정보, this.pnl계산서발행정보 });
            this._flex호선정보.SetBindningCheckBox(this.chk특수선여부, "Y", "N");
            this._flex호선정보.SetBindningCheckBox(this.chk국내기항여부, "Y", "N");
            this._flex호선정보.SetBindningCheckBox(this.chk스크러버유무, "Y", "N");

            this._flex호선정보.VerifyPrimaryKey = new string[] { "NO_IMO" };

            if (Global.MainFrame.LoginInfo.CompanyCode.ToUpper() == "K100" && Global.MainFrame.LoginInfo.GroupID.ToUpper() == "ADMIN")
                this._flex호선정보.VerifyNotNull = new string[] { "NO_HULL", "NM_VESSEL" };
            else
                this._flex호선정보.VerifyNotNull = new string[] { "NO_HULL", "NM_VESSEL", "TP_SHIP", "CD_PARTNER" };
            
            this._flex호선정보.ExtendLastCol = false;

            this._flex호선정보.SettingVersion = "0.0.0.3";
            this._flex호선정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flex호선정보.Styles.Add("부모호선").ForeColor = Color.Black;
			this._flex호선정보.Styles.Add("부모호선").BackColor = Color.Yellow;
			this._flex호선정보.Styles.Add("자식호선").ForeColor = Color.Black;
			this._flex호선정보.Styles.Add("자식호선").BackColor = Color.White;
			#endregion

			#region 엔진정보
			this._flex엔진정보.BeginSetting(1, 1, true);

            this._flex엔진정보.SetCol("CD_ENGINE", "유형", 100);
            this._flex엔진정보.SetCol("NM_MODEL", "모델명", 100);
            this._flex엔진정보.SetCol("NM_MAKER", "제조사", 100);
            this._flex엔진정보.SetCol("NM_CLS_L", "대분류", 100);
            this._flex엔진정보.SetCol("NM_CLS_M", "중분류", 100);
            this._flex엔진정보.SetCol("NM_CLS_S", "소분류", 100);
            this._flex엔진정보.SetCol("DC_VERSION", "버전", 100);
            this._flex엔진정보.SetCol("CD_CUSTOMER", "고객코드", false);
            this._flex엔진정보.SetCol("QT_ALL", "전체종수", false);
            this._flex엔진정보.SetCol("QT_HGS_COUNT", "현대종수", false);
            this._flex엔진정보.SetCol("QT_UPLOAD", "업로드종수", false);
            
            if (Certify.IsLive())
            {
                this._flex엔진정보.SetCol("CAPACITY", "용량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
                this._flex엔진정보.SetCol("SERIAL", "일련번호", 100);
                this._flex엔진정보.SetCol("YN_SYNC", "동기화", 80, this.isAdmin, CheckTypeEnum.Y_N);
                this._flex엔진정보.SetCol("DC_RMK", "비고", 100);
            }
            
            this._flex엔진정보.VerifyPrimaryKey = new string[] { "NO_IMO", "NO_ENGINE" };
            this._flex엔진정보.VerifyNotNull = new string[] { "CD_ENGINE", "NM_CLS_L", "NM_CLS_M", "NM_CLS_S" };

            this._flex엔진정보.SetDataMap("CD_ENGINE", Global.MainFrame.GetComboDataCombine("S;CZ_MA00009"), "CODE", "NAME");
            this._flex엔진정보.SetCodeHelpCol("NM_MAKER", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_MAKER", "NM_MAKER" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flex엔진정보.SetCodeHelpCol("NM_CLS_L", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_L", "NM_CLS_L" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flex엔진정보.SetCodeHelpCol("NM_CLS_M", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_M", "NM_CLS_M" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flex엔진정보.SetCodeHelpCol("NM_CLS_S", "H_CZ_MA_CODE_SUB", ShowHelpEnum.Always, new string[] { "CLS_S", "NM_CLS_S" }, new string[] { "CODE", "NAME" });

            if (Certify.IsLive())
                this._flex엔진정보.ExtendLastCol = true;

            this._flex엔진정보.SettingVersion = "0.0.0.1";
            this._flex엔진정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 기부속정보
            this._flex기부속정보.BeginSetting(1, 1, true);

            this._flex기부속정보.SetCol("NO_PLATE", "부품번호", 120);
            this._flex기부속정보.SetCol("NM_PLATE", "부품명", 120);
            this._flex기부속정보.SetCol("NM_PLATE_BAK", "부품명(백업)", false);
            this._flex기부속정보.SetCol("CD_ITEM", "재고코드", 80);
            this._flex기부속정보.SetCol("NM_ITEM", "재고명", 120);
            this._flex기부속정보.SetCol("UCODE", "U코드", 80);
            this._flex기부속정보.SetCol("WEIGHT", "무게", 80, true, typeof(decimal));
            this._flex기부속정보.SetCol("NM_UNIT", "단위", 80);
            this._flex기부속정보.SetCol("NO_POSITION", "위치", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기부속정보.SetCol("QT_PLATE", "수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);

			if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
			{
                this._flex기부속정보.SetCol("NO_PLATE_ORG", "부품번호(원본)", false);
                this._flex기부속정보.SetCol("YN_UPLOAD", "업로드", 80, false, CheckTypeEnum.Y_N);
                this._flex기부속정보.SetCol("EZCODE", "EZ코드", 80);
                this._flex기부속정보.SetCol("EZCODE2", "EZ코드2", 80);
            }

			this._flex기부속정보.SetCol("DC_RMK", "비고", 150);

            this._flex기부속정보.Cols["WEIGHT"].Format = "###,##0.###";

            this._flex기부속정보.VerifyPrimaryKey = new string[] { "NO_IMO", "NO_ENGINE", "NO_PLATE" };

            this._flex기부속정보.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flex기부속정보.SetCodeHelpCol("NM_UNIT", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "UNIT", "NM_UNIT" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });

            this._flex기부속정보.ExtendLastCol = true;

            this._flex기부속정보.SettingVersion = "0.0.0.2";
            this._flex기부속정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 매입처정보
            this._flex매입처정보.BeginSetting(1, 1, false);

            if (this.isAdmin)
                this._flex매입처정보.SetCol("CD_VENDOR", "매입처코드", 100, true);
            else
                this._flex매입처정보.SetCol("CD_VENDOR", "매입처코드", 100, false);

            this._flex매입처정보.SetCol("NM_VENDOR", "매입처명", 100);

            this._flex매입처정보.SetCodeHelpCol("CD_VENDOR", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "CD_VENDOR", "NM_VENDOR");

			this._flex매입처정보.ExtendLastCol = true;

            this._flex매입처정보.SettingVersion = "0.0.0.2";
            this._flex매입처정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 유형정보
			this._flex유형정보.BeginSetting(1, 1, false);

            if (this.isAdmin)
            {
                this._flex유형정보.SetCol("NO_TYPE", "유형번호", 100, true);
                this._flex유형정보.SetCol("NM_CLS_L", "대분류", 100, true);
                this._flex유형정보.SetCol("NM_CLS_M", "중분류", 100, true);
                this._flex유형정보.SetCol("NM_CLS_S", "소분류", 100, true);
            }
            else
            {
                this._flex유형정보.SetCol("NO_TYPE", "유형번호", 100, false);
                this._flex유형정보.SetCol("NM_CLS_L", "대분류", 100, false);
                this._flex유형정보.SetCol("NM_CLS_M", "중분류", 100, false);
                this._flex유형정보.SetCol("NM_CLS_S", "소분류", 100, false);
            }
			
            this._flex유형정보.SetCol("CLS_S", "소분류", false);

			this._flex유형정보.SetCodeHelpCol("NM_CLS_L", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_L", "NM_CLS_L" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
			this._flex유형정보.SetCodeHelpCol("NM_CLS_M", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_M", "NM_CLS_M" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
			this._flex유형정보.SetCodeHelpCol("NM_CLS_S", "H_CZ_MA_CODE_SUB", ShowHelpEnum.Always, new string[] { "CLS_S", "NM_CLS_S" }, new string[] { "CODE", "NAME" });

			this._flex유형정보.ExtendLastCol = true;

			this._flex유형정보.SettingVersion = "0.0.0.2";
			this._flex유형정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 기자재정보
			this._flex기자재정보.BeginSetting(1, 1, false);

            this._flex기자재정보.SetCol("NO_ITEM", "기자재번호", 100, true);
            this._flex기자재정보.SetCol("CD_ITEM", "재고코드", 100, true);
            this._flex기자재정보.SetCol("NO_PART", "파트번호", 100, true);
            this._flex기자재정보.SetCol("NM_PART", "품목명", 100, true);
			this._flex기자재정보.SetCol("DC_MODEL", "적용모델", 100, true);
            this._flex기자재정보.SetCol("NO_POS", "아이템번호", 100, true);
            this._flex기자재정보.SetCol("UCODE", "U코드", 100, true);
            this._flex기자재정보.SetCol("UCODE2", "U코드2", 100, true);
            this._flex기자재정보.SetCol("NO_DRAWING", "도면번호", 100, true);
            this._flex기자재정보.SetCol("NO_DRAWING2", "도면번호2", 100, true);
            this._flex기자재정보.SetCol("UNIT", "단위", 100, true);
            this._flex기자재정보.SetCol("QT_ITEM", "수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재정보.SetCol("DC_RMK1_ITEM", "추가정보1", 100);
            this._flex기자재정보.SetCol("DC_RMK_ITEM", "품목비고", 100);
            
            this._flex기자재정보.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flex기자재정보.SetDataMap("UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");

            this._flex기자재정보.ExtendLastCol = true;

            this._flex기자재정보.SettingVersion = "0.0.0.1";
            this._flex기자재정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

				if (this.DisplayRectangle.Width >= 1024)
				{
					this.splitContainer1.SplitterDistance = 1024;
					this.splitContainer2.SplitterDistance = 419;
					this.splitContainer3.SplitterDistance = 173;
					this.splitContainer5.SplitterDistance = 200;
				}

                ds = this.GetComboData(new string[] { "S;CZ_MA00002",
                                                      "S;CZ_MA00003",
                                                      "S;MA_B000040",
                                                      "S;MA_B000020" });

                ds.Tables[3].DefaultView.Sort = "NAME ASC";

                this.cbo호선유형.ValueMember = "CODE";
                this.cbo호선유형.DisplayMember = "NAME";
                this.cbo호선유형.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT NULL AS CODE, NULL AS NAME, NULL AS NM_SYSDEF
                                                                                    UNION ALL
                                                                                    SELECT MC.CD_SYSDEF AS CODE,
                                                                                           (CASE WHEN ISNULL(MC.CD_FLAG1, '') = '' THEN '' ELSE '(' + ISNULL(MC.CD_FLAG1, '') + ') ' END) + MC.NM_SYSDEF AS NAME,
                                                                                           MC.NM_SYSDEF
                                                                                    FROM MA_CODEDTL MC WITH(NOLOCK)
                                                                                    WHERE MC.CD_COMPANY = '{0}'
                                                                                    AND MC.CD_FIELD = 'CZ_MA00002'
                                                                                    ORDER BY NM_SYSDEF ASC", Global.MainFrame.LoginInfo.CompanyCode));

                this.cbo호선유형S.ValueMember = "CODE";
                this.cbo호선유형S.DisplayMember = "NAME";
                this.cbo호선유형S.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT NULL AS CODE, NULL AS NAME, NULL AS NM_SYSDEF
                                                                                    UNION ALL
                                                                                    SELECT MC.CD_SYSDEF AS CODE,
                                                                                           (CASE WHEN ISNULL(MC.CD_FLAG1, '') = '' THEN '' ELSE '(' + ISNULL(MC.CD_FLAG1, '') + ') ' END) + MC.NM_SYSDEF AS NAME,
                                                                                           MC.NM_SYSDEF
                                                                                    FROM MA_CODEDTL MC WITH(NOLOCK)
                                                                                    WHERE MC.CD_COMPANY = '{0}'
                                                                                    AND MC.CD_FIELD = 'CZ_MA00002'
                                                                                    ORDER BY NM_SYSDEF ASC", Global.MainFrame.LoginInfo.CompanyCode));

                this.cbo엔진제조사S.ValueMember = "CODE";
                this.cbo엔진제조사S.DisplayMember = "NAME";
                this.cbo엔진제조사S.DataSource = ds.Tables[1].Copy();

                this.cbo과세구분.ValueMember = "CODE";
                this.cbo과세구분.DisplayMember = "NAME";
                this.cbo과세구분.DataSource = ds.Tables[2];

                this.cbo국가.ValueMember = "CODE";
                this.cbo국가.DisplayMember = "NAME";
                this.cbo국가.DataSource = ds.Tables[3];

				this.btn첨부파일.Enabled = false;

                this.btn엑셀업로드.Enabled = false;
				this.btn엑셀업로드엔진.Enabled = false;
				this.btn엑셀업로드매입처.Enabled = false;
				this.btn엑셀업로드유형.Enabled = false;
				this.btn엑셀업로드기부속.Enabled = false;
                this.btn엑셀업로드기자재.Enabled = false;

                this.btn엑셀양식다운로드.Enabled = false;
				this.btn엑셀양식다운로드엔진.Enabled = false;
				this.btn엑셀양식다운로드매입처.Enabled = false;
				this.btn엑셀양식다운로드유형.Enabled = false;
				this.btn엑셀양식다운로드기부속.Enabled = false;
                this.btn엑셀양식다운로드기자재.Enabled = false;

				this.btn일괄업로드유형.Enabled = false;

				this.btn엔진추가.Enabled = false;
				this.btn기부속추가.Enabled = false;
				this.btn매입처추가.Enabled = false;
				this.btn유형추가.Enabled = false;
				this.btn기자재추가.Enabled = false;

				this.btn엔진제거.Enabled = false;
                this.btn기부속제거.Enabled = false;
                this.btn매입처제거.Enabled = false;
				this.btn유형제거.Enabled = false;
				this.btn기자재제거.Enabled = false;

				if (!string.IsNullOrEmpty(this.bpcIMO번호.QueryWhereIn_Pipe))
                    this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
			this.DataChanged += new EventHandler(this.PageDataChanged);

			this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);

			this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
			this.btn엑셀양식다운로드엔진.Click += new EventHandler(this.btn엑셀양식다운로드엔진_Click);
			this.btn엑셀양식다운로드매입처.Click += new EventHandler(this.btn엑셀양식다운로드매입처_Click);
			this.btn엑셀양식다운로드유형.Click += new EventHandler(this.btn엑셀양식다운로드유형_Click);
			this.btn엑셀양식다운로드기부속.Click += new EventHandler(this.btn엑셀양식다운로드기부속_Click);
            this.btn엑셀양식다운로드기자재.Click += new EventHandler(this.btn엑셀양식다운로드기자재_Click);

			this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
			this.btn엑셀업로드엔진.Click += new EventHandler(this.btn엑셀업로드엔진_Click);
			this.btn엑셀업로드매입처.Click += new EventHandler(this.btn엑셀업로드매입처_Click);
			this.btn엑셀업로드유형.Click += new EventHandler(this.btn엑셀업로드유형_Click);
			this.btn엑셀업로드기부속.Click += new EventHandler(this.btn엑셀업로드기부속_Click);
            this.btn엑셀업로드기자재.Click += new EventHandler(this.btn엑셀업로드기자재_Click);

			this.btn일괄업로드유형.Click += new EventHandler(this.btn일괄업로드매입처_Click);
			
            this.txt호선번호S.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txt호선명S.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txt선주S.KeyDown += new KeyEventHandler(this.Control_KeyDown);

            this.txtIMO번호.Leave += new EventHandler(this.txtIMO번호_Leave);

            this.btn엔진추가.Click += new EventHandler(this.btn엔진추가_Click);
            this.btn엔진제거.Click += new EventHandler(this.btn엔진제거_Click);
            this.btn기부속추가.Click += new EventHandler(this.btn기부속추가_Click);
            this.btn기부속제거.Click += new EventHandler(this.btn기부속제거_Click);

            this.btn매입처추가.Click += new EventHandler(this.btn매입처추가_Click);
            this.btn매입처제거.Click += new EventHandler(this.btn매입처제거_Click);
			this.btn유형추가.Click += new EventHandler(this.btn유형추가_Click);
			this.btn유형제거.Click += new EventHandler(this.btn유형제거_Click);
			this.btn기자재추가.Click += new EventHandler(this.btn기자재추가_Click);
            this.btn기자재제거.Click += new EventHandler(this.btn기자재제거_Click);

			this.btn위치확인.Click += Btn위치확인_Click;
			this.btn위치확인2.Click += Btn위치확인2_Click;

			this.ctx대분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx중분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx소분류.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx조선소구.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this._flex호선정보.AfterRowChange += new RangeEventHandler(this._flex호선정보_AfterRowChange);
            this._flex엔진정보.AfterRowChange += new RangeEventHandler(this._flex엔진정보_AfterRowChange);
            this._flex매입처정보.AfterRowChange += new RangeEventHandler(this._flex매입처정보_AfterRowChange);
			this._flex유형정보.AfterRowChange += new RangeEventHandler(this._flex유형정보_AfterRowChange);

			this._flex엔진정보.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex엔진정보.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex엔진정보.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);

            this._flex유형정보.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex유형정보.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex유형정보.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
			this._flex유형정보.DoubleClick += new EventHandler(this._flex유형정보_DoubleClick);

			this._flex엔진정보.ValidateEdit += new ValidateEditEventHandler(this._flex엔진정보_ValidateEdit);

            this._flex기부속정보.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex기자재정보.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);

			//this._flex엔진정보.BeforeMouseDown += new BeforeMouseDownEventHandler(this._flex_BeforeMouseDown);
			this._flex매입처정보.BeforeMouseDown += new BeforeMouseDownEventHandler(this._flex_BeforeMouseDown);
			this._flex유형정보.BeforeMouseDown += new BeforeMouseDownEventHandler(this._flex_BeforeMouseDown);
			//this._flex기부속정보.BeforeMouseDown += new BeforeMouseDownEventHandler(this._flex_BeforeMouseDown);
			this._flex기자재정보.BeforeMouseDown += new BeforeMouseDownEventHandler(this._flex_BeforeMouseDown);

			//this._flex엔진정보.KeyDown += new KeyEventHandler(this._flex_KeyDown);
			this._flex매입처정보.KeyDown += new KeyEventHandler(this._flex_KeyDown);
			this._flex유형정보.KeyDown += new KeyEventHandler(this._flex_KeyDown);
			//this._flex기부속정보.KeyDown += new KeyEventHandler(this._flex_KeyDown);
			this._flex기자재정보.KeyDown += new KeyEventHandler(this._flex_KeyDown);

			this._flex호선정보.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex호선정보_OwnerDrawCell);
		}

		private void Btn위치확인2_Click(object sender, EventArgs e)
		{
            try
            {
                string url = "http://192.168.1.140:1123/Datalastic.asmx";
                string soapAction = "http://192.168.1.140:1123/GetVesselInfo";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", soapAction);
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                XmlDocument soapEnvelope = new XmlDocument();
                soapEnvelope.LoadXml(string.Format(@"<?xml version='1.0' encoding='utf-8'?>
<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
  <soap:Body>
    <GetVesselInfo xmlns='http://192.168.1.140:1123/'>
      <imo>{0}</imo>
    </GetVesselInfo>
  </soap:Body>
</soap:Envelope>", this._flex호선정보["NO_IMO"].ToString()));

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelope.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(soapResult);

                        string source = doc.DocumentElement.GetElementsByTagName("GetVesselInfoResult")[0].InnerText;

                        if (!source.Contains('{'))
                        {
                            this.ShowMessage(source);
                            return;
                        }

                        JObject obj = (JObject)JsonConvert.DeserializeObject(source);

                        object obj1 = ((JValue)obj["data"]["dep_port"]).Value;
                        string dep_port = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["dep_port_unlocode"]).Value;
                        string dep_port_unlocode = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["destination"]).Value;
                        string destination = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["dest_port"]).Value;
                        string dest_port = (obj1 == null ? string.Empty : obj1.ToString());

                        obj1 = ((JValue)obj["data"]["dest_port_unlocode"]).Value;
                        string dest_port_unlocode = (obj1 == null ? string.Empty : obj1.ToString());

                        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time"); // 한국 시간대

                        obj1 = ((JValue)obj["data"]["last_position_UTC"]).Value;
                        string 업데이트시간_STR = string.Empty;
                        if (obj1 != null)
						{
                            DateTime 업데이트시간_UTC = (DateTime)obj1;
                            DateTime 업데이트시간 = TimeZoneInfo.ConvertTimeFromUtc(업데이트시간_UTC, timeZone);
                            업데이트시간_STR = 업데이트시간.ToString("yyyy.MM.dd HH:mm:ss");
                        }

                        obj1 = ((JValue)obj["data"]["atd_UTC"]).Value;
                        string ATD_STR = string.Empty;
                        if (obj1 != null)
						{
                            DateTime ATD_UTC = (DateTime)obj1;
                            DateTime ATD = TimeZoneInfo.ConvertTimeFromUtc(ATD_UTC, timeZone);
                            ATD_STR = ATD.ToString("yyyy.MM.dd HH:mm:ss");
                        }

                        obj1 = ((JValue)obj["data"]["eta_UTC"]).Value;
                        string ETA_STR = string.Empty;
                        if (obj1 != null)
						{
                            DateTime ETA_UTC = (DateTime)obj1;
                            DateTime ETA = TimeZoneInfo.ConvertTimeFromUtc(ETA_UTC, timeZone);
                            ETA_STR = ETA.ToString("yyyy.MM.dd HH:mm:ss");
                        }

                        string strURL = "http://maps.google.co.kr/maps?q=" + HttpUtility.UrlEncode(obj["data"]["lat"].ToString(), Encoding.UTF8) + "," + HttpUtility.UrlEncode(obj["data"]["lon"].ToString(), Encoding.UTF8);
                        Process.Start("msedge.exe", strURL);

                        this.ShowMessage("업데이트 : " + 업데이트시간_STR + Environment.NewLine +
                                         dep_port + " -> " + dest_port + "(" + destination + ")" + Environment.NewLine +
                                         "출발 : " + ATD_STR + " (" + dep_port_unlocode + ")" + Environment.NewLine +
                                         "도착 : " + ETA_STR + " (" + dest_port_unlocode + ")");
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Btn위치확인_Click(object sender, EventArgs e)
		{
            Process.Start("msedge.exe", "https://www.vesselfinder.com/aismap?zoom=undefined&lat=undefined&lon=undefined&width=100%25&height=400&names=true&imo=" + this._flex호선정보["NO_IMO"].ToString() + "&track=true&fleet=false&fleet_name=false&fleet_hide_old_positions=false&clicktoact=false&store_pos=true");
        }

		private void InitControl()
		{
			try
			{
				if (!Global.MainFrame.LoginInfo.CompanyCode.In("K100", "K200", "S100"))
					this.splitContainer2.Panel2Collapsed = true;

				//실사대비
				if (!Certify.IsLive())
					this.splitContainer3.Panel2Collapsed = true;

				//this.btn엑셀양식다운로드.Visible = this.isExcel;
				this.btn엑셀양식다운로드엔진.Visible = this.isExcel;
				this.btn엑셀양식다운로드기부속.Visible = this.isExcel;
				this.btn엑셀양식다운로드매입처.Visible = this.isExcel;
				this.btn엑셀양식다운로드유형.Visible = this.isExcel;
				this.btn엑셀양식다운로드기자재.Visible = this.isExcel;

				//this.btn엑셀업로드.Visible = this.isExcel;
				this.btn엑셀업로드엔진.Visible = this.isExcel;
				this.btn엑셀업로드기부속.Visible = this.isExcel;
				this.btn엑셀업로드매입처.Visible = this.isExcel;
				this.btn엑셀업로드유형.Visible = this.isExcel;
				this.btn엑셀업로드기자재.Visible = this.isExcel;

				this.btn일괄업로드유형.Visible = this.isAdmin;

				//this.btn엔진추가.Visible = this.isAdmin;
				this.btn기부속추가.Visible = this.isExcel;
				this.btn매입처추가.Visible = this.isAdmin;
				this.btn유형추가.Visible = this.isAdmin;
				this.btn기자재추가.Visible = this.isAdmin;

				//this.btn엔진제거.Visible = this.isAdmin;
				this.btn기부속제거.Visible = this.isExcel;
				this.btn매입처제거.Visible = this.isAdmin;
				this.btn유형제거.Visible = this.isAdmin;
				this.btn기자재제거.Visible = this.isAdmin;

				if (this.isAdmin == true)
                {

                }
                else if (this.isLeader == true)
                {
                    this.imagePanel2.Visible = false;
                    this.chk첨부파일여부.Visible = false;
                    this.chk업로드.Visible = false;
                }
                else if (this.isEng == true)
                {
                    this.imagePanel2.Visible = false;
                    this.chk첨부파일여부.Visible = false;
                    this.chk업로드.Visible = false;
                }
                else
                {
                    this.bpPanelControl36.Visible = false;
                    this.chk첨부파일여부.Visible = false;
                    this.chk업로드.Visible = false;
                    this.tabControl1.TabPages.Remove(this.tpg기자재);
                }
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region 메인 버튼
		protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex호선정보.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			 this.txtIMO번호S.Text,
																			 this.bpcIMO번호.QueryWhereIn_Pipe,
																			 this.txt호선번호S.Text,
																			 this.txt호선명S.Text,
																			 this.txt선주S.Text,
																			 this.ctx운항선사S.CodeValue,
																			 this.cbo엔진제조사S.SelectedValue,
																			 this.txt모델명.Text,
																			 this.txt일련번호.Text,
																			 this.ctx대분류.CodeValue,
																			 this.ctx중분류.CodeValue,
																			 this.ctx소분류.CodeValue,
																			 (this.chk특수선.Checked == true ? "Y" : "N"),
																	         this.ctx매입처.CodeValue,
																			 (this.chk자매호선.Checked == true ? "Y" : "N"),
																		     this.txt조선소S.Text,
                                                                             (this.isLeader == true ? "Y" : "N"),
                                                                             (this.isEng == true ? "Y" : "N"),
                                                                             (this.chk첨부파일여부.Checked == true ? "Y" : "N"),
                                                                             (this.chk업로드.Checked == true ? "Y" : "N"),
                                                                             this.cbo호선유형S.SelectedValue }, this.chk입항일자.Checked);

				if (this.chk자매호선.Checked)
					this._flex호선정보.DataTable.DefaultView.Sort = "NO_SISTER ASC, CD_SISTER DESC";

                if (!this._flex호선정보.HasNormalRow)
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

                if (!BeforeAdd()) return;

                this._flex호선정보.Rows.Add();
                this._flex호선정보.Row = _flex호선정보.Rows.Count - 1;

                this._flex호선정보.AddFinished();
                this._flex호선정보.Col = _flex호선정보.Cols["NO_IMO"].Index;
                this._flex호선정보.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            if (this.IMO번호형식확인(this.txtIMO번호.Text) == false)
            {
                this.ShowMessage("@ 입력 형식이 올바르지 않습니다.", this.lblIMO번호.Text);
                this.txtIMO번호.Text = string.Empty;
                return false;
            }

            if (this._flex호선정보.GetChanges() != null)
            {
                DataTable dt = this._flex호선정보.GetChanges();
                Regex regex = new Regex(@"\d{7}");

                if (Global.MainFrame.LoginInfo.DeptCode != "010900" &&
                    dt.Select().AsEnumerable().Where(x => x.RowState == DataRowState.Added &&
                                                          regex.IsMatch(x["NO_IMO"].ToString())).Count() > 0)
                {
                    this.ShowMessage("호선등록 화면에서는 Plant, Generator만 등록 가능 합니다.\n호선은 SEA-WEB 관리에서 등록 하시기 바랍니다.");
                    return false;
                }

                if (dt.Select("ISNULL(CALL_SIGN, '') = ''").Length > 0)
                {
                    this.ShowMessage("Call Sign은 필수 입력항목 입니다.\n없을 경우 None 으로 입력");
                    return false;
                }

                if (dt.Select("ISNULL(NM_VESSEL, '') = ''").Length > 0)
                {
                    this.ShowMessage("호선명은 필수 입력항목 입니다.\n알 수 없는 경우 UNKNOWN 으로 입력");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", x["INVOICE_EMAIL"].ToString()))) == 0).Count() > 0)
				{
                    this.ShowMessage("계산서발행정보 => 이메일에 형식에 맞지 않은 이메일 주소가 입력 되어 있습니다.");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => x["INVOICE_COMPANY"].ToString().Split('\n').Length > 3).Count() > 0)
                {
                    this.ShowMessage("계산서발행정보 => 회사명은 3줄 초과 입력 할 수 없습니다.");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => x["INVOICE_ADDRESS"].ToString().Split('\n').Length > 4).Count() > 0)
                {
                    this.ShowMessage("계산서발행정보 => 주소는 4줄 초과 입력 할 수 없습니다.");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => x["INVOICE_COMPANY"].ToString().Split('\n').Length > 3).Count() > 0)
                {
                    this.ShowMessage("계산서발행정보 => 회사명은 3줄 초과 입력 할 수 없습니다.");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => x["INVOICE_COMPANY"].ToString().Split('\n').Where(y => y.Length >= 70).Count() > 0).Count() > 0)
                {
                    this.ShowMessage("계산서발행정보 => 회사명은 한줄에 70자 이상 입력 할 수 없습니다.");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => x["INVOICE_ADDRESS"].ToString().Split('\n').Length > 4).Count() > 0)
                {
                    this.ShowMessage("계산서발행정보 => 주소는 4줄 초과 입력 할 수 없습니다.");
                    return false;
                }

                if (dt.Select().AsEnumerable().Where(x => x["INVOICE_ADDRESS"].ToString().Split('\n').Where(y => y.Length >= 70).Count() > 0).Count() > 0)
                {
                    this.ShowMessage("계산서발행정보 => 주소는 한줄에 70자 이상 입력 할 수 없습니다.");
                    return false;
                }
            }

            if (this._flex기자재정보.GetChanges() != null && this._flex기자재정보.GetChanges().Select("ISNULL(QT_ITEM, 0) = 0").Length > 0)
			{
				this.ShowMessage("수량은 필수 입력항목 입니다.\n 0보다 큰 값 입력");
				return false;
			}

			return true;
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

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
            if (this._flex호선정보.IsDataChanged == false &&
                this._flex엔진정보.IsDataChanged == false &&
                this._flex기부속정보.IsDataChanged == false &&
                this._flex매입처정보.IsDataChanged == false &&
				this._flex유형정보.IsDataChanged == false &&
				this._flex기자재정보.IsDataChanged == false) return false;

            if (!_biz.Save(this._flex호선정보.GetChanges(),
                           this._flex엔진정보.GetChanges(),
                           this._flex기부속정보.GetChanges(),
                           this._flex매입처정보.GetChanges(),
						   this._flex유형정보.GetChanges(),
						   this._flex기자재정보.GetChanges())) return false;

            this._flex호선정보.AcceptChanges();
            this._flex엔진정보.AcceptChanges();
            this._flex기부속정보.AcceptChanges();
            this._flex매입처정보.AcceptChanges();
			this._flex유형정보.AcceptChanges();
			this._flex기자재정보.AcceptChanges();

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex호선정보.HasNormalRow) return;
                if (this._biz.사용여부체크(D.GetString(this._flex호선정보["NO_IMO"])))
                {
                    this.ShowMessage("사용중인 코드이므로 삭제하실 수 없습니다.");
                    return;
                }
                if (this._biz.엔진여부체크(D.GetString(this._flex호선정보["NO_IMO"])))
                {
                    this.ShowMessage("엔진 정보가 존재합니다.");
                    return;
                }
                if (!Util.CheckPW()) return;

                this._flex호선정보.Rows.Remove(this._flex호선정보.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 화면 내 버튼
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void txtIMO번호_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtIMO번호.Text)) return;

                if (this.IMO번호형식확인(this.txtIMO번호.Text) == false)
                {
                    this.ShowMessage("@ 입력 형식이 올바르지 않습니다.", this.lblIMO번호.Text);
                    this.txtIMO번호.Text = string.Empty;
                    return;
                }

                if (this._biz.IMO번호중복체크(this.txtIMO번호.Text))
                {
                    this.ShowMessage("CZ_@ 이(가) 중복되었습니다.", this.txtIMO번호.Text);
                    this.txtIMO번호.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
			StringBuilder Values;
            string serverPath;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_호선등록_" + Global.MainFrame.GetStringToday + ".xlsx";
                
                if (this.isExcel)
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL.xlsx";
                else
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL1.xlsx";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (this._flex호선정보.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
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
                foreach (DataRow dr in this._flex호선정보.DataTable.Rows)
                {
                    Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!this._flex호선정보.DataTable.Columns.Contains(Column.ColumnName)) continue;

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

		private void btn엑셀양식다운로드엔진_Click(object sender, EventArgs e)
		{
			OleDbConnection conn = null;

			try
			{
				bool bState = true;
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_엔진정보_" + Global.MainFrame.GetStringToday + ".xlsx";
				string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL_ENGINE.xlsx";

				System.Net.WebClient client = new System.Net.WebClient();
				client.DownloadFile(serverPath, localPath);

				if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
					bState = false;

				ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

				if (bState == false) return;

				// 확장명 XLS (Excel 97~2003 용)
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
				DataTable dt = DBHelper.GetDataTable(@"SELECT NO_IMO,
														      NO_ENGINE,
															  CD_ENGINE,
													          NM_MODEL,
															  CD_MAKER,
                                        	                  CLS_L,
                                        	                  CLS_M,
                                        	                  CLS_S,
															  DC_VERSION,
															  CAPACITY,
															  SERIAL,
                                        	                  DC_RMK,
															  'N' AS YN_DELETE
                                                       FROM CZ_MA_HULL_ENGINE WITH(NOLOCK)");

				foreach (DataRow dr in dt.Rows)
				{
					StringBuilder Values = new StringBuilder();

					foreach (DataColumn Column in ds.Tables[0].Columns)
					{
						if (!dt.Columns.Contains(Column.ColumnName)) continue;

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

		private void btn엑셀양식다운로드매입처_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_매입처정보_" + Global.MainFrame.GetStringToday + ".xlsx";
                string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL_VENDOR.xlsx";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

				if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
					bState = false;

				ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
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
                DataTable dt = DBHelper.GetDataTable(@"SELECT NO_IMO,
                                        	                  CD_VENDOR,
															  'N' AS YN_DELETE
                                                       FROM CZ_MA_HULL_VENDOR WITH(NOLOCK)");

                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!dt.Columns.Contains(Column.ColumnName)) continue;

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

		private void btn엑셀양식다운로드유형_Click(object sender, EventArgs e)
		{
			OleDbConnection conn = null;

			try
			{
				//bool bState = true;
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_유형정보_" + Global.MainFrame.GetStringToday + ".xlsx";
				string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL_VENDOR_TYPE.xlsx";

				System.Net.WebClient client = new System.Net.WebClient();
				client.DownloadFile(serverPath, localPath);

				//if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
				//	bState = false;

				ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

				//if (bState == false) return;

				//// 확장명 XLS (Excel 97~2003 용)
				//string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 12.0;";

				//conn = new OleDbConnection(strConn);
				//conn.Open();

				//OleDbCommand Cmd = null;
				//OleDbDataAdapter OleDBAdap = null;

				//string sTableName = string.Empty;

				//DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
				//DataSet ds = new DataSet();

				//// 엑셀의 1번째 시트만 데이터 만들어 주면 되므로 한 루프후 끝내면 됩니다.
				//foreach (DataRow dr in dtSchema.Rows)
				//{
				//	OleDBAdap = new OleDbDataAdapter(dr["TABLE_NAME"].ToString(), conn);

				//	OleDBAdap.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
				//	OleDBAdap.AcceptChangesDuringFill = false;

				//	sTableName = dr["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

				//	if (dr["TABLE_NAME"].ToString().Contains("$"))
				//		OleDBAdap.Fill(ds, sTableName);
				//	break;
				//}

				//StringBuilder FldsInfo = new StringBuilder();
				//StringBuilder Flds = new StringBuilder();

				//// Create Field(s) String : 현재 테이블의 Field 명 생성
				//foreach (DataColumn Column in ds.Tables[0].Columns)
				//{
				//	if (FldsInfo.Length > 0)
				//	{
				//		FldsInfo.Append(",");
				//		Flds.Append(",");
				//	}

				//	FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
				//	Flds.Append(Column.ColumnName.Replace("'", "''"));
				//}

				//// Insert Data
				//DataTable dt = DBHelper.GetDataTable(@"SELECT NO_IMO,
				//											  CD_VENDOR,
    //                                    	                  NO_TYPE,
    //                                    	                  CLS_L,
    //                                    	                  CLS_M,
    //                                    	                  CLS_S,
				//											  DC_RMK1,
				//											  DC_RMK2,
				//											  DC_RMK3,
				//											  DC_RMK4,
				//											  DC_RMK5,
				//											  DC_RMK6,
				//											  DC_RMK7,
				//											  DC_RMK8,
				//											  DC_RMK9,
				//											  DC_RMK10,
				//											  DC_RMK11,
				//											  DC_RMK12,
				//											  DC_RMK13,
				//											  DC_RMK14,
				//											  DC_RMK15,
    //                                    	                  DC_RMK,
				//											  'N' AS YN_DELETE
    //                                                   FROM CZ_MA_HULL_VENDOR_TYPE WITH(NOLOCK)");

				//foreach (DataRow dr in dt.Rows)
				//{
				//	StringBuilder Values = new StringBuilder();

				//	foreach (DataColumn Column in ds.Tables[0].Columns)
				//	{
				//		if (!dt.Columns.Contains(Column.ColumnName)) continue;

				//		if (Values.Length > 0) Values.Append(",");
				//		Values.Append("'" + dr[Column.ColumnName].ToString().Replace("'", "''") + "'");
				//	}

				//	Cmd = new OleDbCommand(
				//		"INSERT INTO [" + sTableName + "$]" +
				//		"(" + Flds.ToString() + ") " +
				//		"VALUES (" + Values.ToString() + ")",
				//		conn);
				//	Cmd.ExecuteNonQuery();
				//}

				//bState = true;
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

		private void btn엑셀양식다운로드기부속_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_기부속정보_" + Global.MainFrame.GetStringToday + ".xlsx";

                string serverPath = string.Empty;

                if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL_ENGINE_ITEM1.xlsx";
                else
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL_ENGINE_ITEM.xlsx";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (this._flex기부속정보.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
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
                DataTable dt;

                if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
                    dt = DBHelper.GetDataTable(@"SELECT EI.NO_IMO,
                                                        EI.NO_ENGINE,
                                                        EI.NO_PLATE,
                                                        EI.NM_PLATE,
                                                        EI.CD_ITEM,
                                                        EI.KCODE AS EZCODE,
                                                        EI.KCODE2 AS EZCODE2,
                                                        EI.UCODE,
                                                        EI.WEIGHT,
                                                        EI.UNIT,
                                                        EI.NO_POSITION,
                                                        EI.QT_PLATE,
                                                        EI.DC_RMK,
                                                        'N' AS YN_DELETE
                                                 FROM CZ_MA_HULL_ENGINE_ITEM EI WITH(NOLOCK)
											     WHERE EI.NO_IMO = '" + this._flex호선정보["NO_IMO"].ToString() + "'" + Environment.NewLine +
                                                "AND EI.NO_ENGINE = '" + this._flex엔진정보["NO_ENGINE"].ToString() + "'");
                else
                    dt = DBHelper.GetDataTable(@"SELECT NO_IMO,
	                                                    NO_ENGINE,
	                                                    NO_PLATE,
	                                                    NM_PLATE,
	                                                    CD_ITEM,
	                                                    UCODE,
	                                                    WEIGHT,
	                                                    UNIT,
	                                                    NO_POSITION,
	                                                    QT_PLATE,
	                                                    DC_RMK,
												        'N' AS YN_DELETE
                                               FROM CZ_MA_HULL_ENGINE_ITEM WITH(NOLOCK)
											   WHERE NO_IMO = '" + this._flex호선정보["NO_IMO"].ToString() + "'" + Environment.NewLine +
                                              "AND NO_ENGINE = '" + this._flex엔진정보["NO_ENGINE"].ToString() + "'");

                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!dt.Columns.Contains(Column.ColumnName)) continue;

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

        private void btn엑셀양식다운로드기자재_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_기자재정보_" + Global.MainFrame.GetStringToday + ".xlsx";
                string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_MA_HULL_VENDOR_ITEM.xlsx";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (this._flex기자재정보.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
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
				DataTable dt = DBHelper.GetDataTable(@"SELECT NO_IMO,
	                                                          CD_VENDOR,
	                                                          NO_TYPE,
	                                                          NO_ITEM,
	                                                          QT_ITEM,
	                                                          CD_ITEM,
	                                                          NO_PART,
	                                                          NM_PART,
	                                                          DC_MODEL,
	                                                          NO_POS,
	                                                          UCODE,
	                                                          UCODE2,
	                                                          NO_DRAWING,
	                                                          NO_DRAWING2,
                                                              UNIT,
	                                                          DC_RMK,
                                                              DC_RMK1,
	                                                          DC_RMK2,
	                                                          DC_RMK3,
	                                                          DC_RMK4,
	                                                          DC_RMK5,
                                                              DC_RMK6,
                                                              DC_RMK7,
                                                              DC_RMK8,
                                                              DC_RMK9,
                                                              DC_RMK10,
														      'N' AS YN_DELETE
                                                       FROM CZ_MA_HULL_VENDOR_ITEM WITH(NOLOCK)
													   WHERE NO_IMO = '" + this._flex호선정보["NO_IMO"].ToString() + "'" + Environment.NewLine +
                                                      "AND CD_VENDOR = '" + this._flex매입처정보["CD_VENDOR"].ToString() + "'" + Environment.NewLine +
													  "AND NO_TYPE = '" + this._flex유형정보["NO_TYPE"].ToString() + "'");

				foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!dt.Columns.Contains(Column.ColumnName)) continue;

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
            DataRow[] dataRowArray;
            DataRow dr1;
            int index;

            try
            {
                #region btn엑셀업로드
                _fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (_fileDlg.ShowDialog() != DialogResult.OK) return;
                if (this.IsFileLocked(_fileDlg.FileName))
                    return;
                else
                {
                    string 경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_HULL/Excel/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday;
                    string fileName = FileMgr.GetUniqueFileName(경로, _fileDlg.FileName);

                    FileUploader.UploadFile(fileName, _fileDlg.FileName, "Upload/P_CZ_MA_HULL", "Excel/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday);
                }

                this._flex호선정보.Redraw = false;

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + _fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IMO" };
                string[] argsPkNm = new string[] { DD("IMO번호") };

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                // 데이터 읽으면서 해당하는 값 셋팅
                index = 0;
                foreach (DataRow dr in dtExcel.Rows)
                {
                    MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[]{ D.GetString(++index), D.GetString(dtExcel.Rows.Count) });

                    if (this.IMO번호형식확인(D.GetString(dr["NO_IMO"])) == false)
                    {
                        this.ShowMessage("@ 입력 형식이 올바르지 않습니다.", this.lblIMO번호.Text);
                        return;
                    }

                    dataRowArray = this._flex호선정보.DataTable.Select("NO_IMO = '" + D.GetString(dr["NO_IMO"]) + "'");

                    if (dataRowArray.Length > 0)
                        dr1 = dataRowArray[0];
                    else
                        dr1 = this._flex호선정보.DataTable.NewRow();

                    if (this.isExcel)
					{
                        if (!string.IsNullOrEmpty(D.GetString(dr["NO_IMO"])))
                            dr1["NO_IMO"] = dr["NO_IMO"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["NO_HULL"])))
                            dr1["NO_HULL"] = dr["NO_HULL"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["CALL_SIGN"])))
                            dr1["CALL_SIGN"] = dr["CALL_SIGN"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["NM_VESSEL"])))
                            dr1["NM_VESSEL"] = dr["NM_VESSEL"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["NM_CERT"])))
                            dr1["NM_CERT"] = dr["NM_CERT"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["NM_SHIP_OWNER"])))
                            dr1["NM_SHIP_OWNER"] = dr["NM_SHIP_OWNER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["DC_SHIPBUILDER"])))
                            dr1["DC_SHIPBUILDER"] = dr["DC_SHIPBUILDER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["TP_SHIP"])))
                            dr1["TP_SHIP"] = dr["TP_SHIP"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["YN_NEWSHIP"])))
                            dr1["YN_NEWSHIP"] = dr["YN_NEWSHIP"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["CD_PARTNER"])))
                            dr1["CD_PARTNER"] = dr["CD_PARTNER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["LN_PARTNER"])))
                            dr1["LN_PARTNER"] = dr["LN_PARTNER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["PURIFIER"])))
                            dr1["PURIFIER"] = dr["PURIFIER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["BOILER"])))
                            dr1["BOILER"] = dr["BOILER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["INCINERATOR"])))
                            dr1["INCINERATOR"] = dr["INCINERATOR"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["PUMP"])))
                            dr1["PUMP"] = dr["PUMP"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["REF_SYS"])))
                            dr1["REF_SYS"] = dr["REF_SYS"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["ER_MACH"])))
                            dr1["ER_MACH"] = dr["ER_MACH"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["DECK_MACH"])))
                            dr1["DECK_MACH"] = dr["DECK_MACH"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["PRE_MACH"])))
                            dr1["PRE_MACH"] = dr["PRE_MACH"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["DC_RMK"])))
                            dr1["DC_RMK"] = D.GetString(dr["DC_RMK"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["PROPELLER_SERIAL"])))
                            dr1["PROPELLER_SERIAL"] = dr["PROPELLER_SERIAL"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["STERNPOST_SERIAL"])))
                            dr1["STERNPOST_SERIAL"] = dr["STERNPOST_SERIAL"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["TP_VAT"])))
                            dr1["TP_VAT"] = dr["TP_VAT"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["CD_NATION"])))
                            dr1["CD_NATION"] = dr["CD_NATION"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["QT_DEAD_WEIGHT"])))
                            dr1["QT_DEAD_WEIGHT"] = dr["QT_DEAD_WEIGHT"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["YN_DOM"])))
                            dr1["YN_DOM"] = dr["YN_DOM"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["YN_SCRUBBER"])))
                            dr1["YN_SCRUBBER"] = dr["YN_SCRUBBER"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["DC_BWTS"])))
                            dr1["DC_BWTS"] = dr["DC_BWTS"];
                        if (!string.IsNullOrEmpty(D.GetString(dr["DT_SHIP_DLV"])))
                            dr1["DT_SHIP_DLV"] = dr["DT_SHIP_DLV"];

                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_COMPANY"])))
                            dr1["INVOICE_COMPANY"] = D.GetString(dr["INVOICE_COMPANY"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_ADDRESS"])))
                            dr1["INVOICE_ADDRESS"] = D.GetString(dr["INVOICE_ADDRESS"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_TEL"])))
                            dr1["INVOICE_TEL"] = D.GetString(dr["INVOICE_TEL"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_EMAIL"])))
                            dr1["INVOICE_EMAIL"] = D.GetString(dr["INVOICE_EMAIL"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_RMK"])))
                            dr1["INVOICE_RMK"] = D.GetString(dr["INVOICE_RMK"]).Replace("\n", Environment.NewLine);
                    }
                    else
					{
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_COMPANY"])))
                            dr1["INVOICE_COMPANY"] = D.GetString(dr["INVOICE_COMPANY"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_ADDRESS"])))
                            dr1["INVOICE_ADDRESS"] = D.GetString(dr["INVOICE_ADDRESS"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_TEL"])))
                            dr1["INVOICE_TEL"] = D.GetString(dr["INVOICE_TEL"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_EMAIL"])))
                            dr1["INVOICE_EMAIL"] = D.GetString(dr["INVOICE_EMAIL"]).Replace("\n", Environment.NewLine);
                        if (!string.IsNullOrEmpty(D.GetString(dr["INVOICE_RMK"])))
                            dr1["INVOICE_RMK"] = D.GetString(dr["INVOICE_RMK"]).Replace("\n", Environment.NewLine);
                    }

                    if (dataRowArray.Length == 0)
                        this._flex호선정보.DataTable.Rows.Add(dr1);
                }
                MsgControl.CloseMsg();

                if (this._flex호선정보.HasNormalRow)
                {
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                    this.ToolBarSaveButtonEnabled = true;
                }
                else
                {
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                }

                this._flex호선정보.Redraw = true;
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex호선정보.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

		private void btn엑셀업로드엔진_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ShowMessage("※ 경고 ※\n\n엔진 삭제 업로드시 엔진에 해당하는 기부속이 모두 삭제되고 해당 데이터는 복구할 수 없습니다.\n\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				#region btn엑셀업로드
				_fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

				if (_fileDlg.ShowDialog() != DialogResult.OK) return;
                if (this.IsFileLocked(_fileDlg.FileName))
                    return;
                else
                {
                    string 경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_HULL/Excel1/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday;
                    string fileName = FileMgr.GetUniqueFileName(경로, _fileDlg.FileName);

                    FileUploader.UploadFile(fileName, _fileDlg.FileName, "Upload/P_CZ_MA_HULL", "Excel1/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday);
                }

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + _fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IMO", "NO_ENGINE" };
				string[] argsPkNm = new string[] { DD("호선번호"), DD("엔진번호") };

				for (int i = 0; i < argsPk.Length; i++)
				{
					if (!dtExcel.Columns.Contains(argsPk[i]))
					{
						this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
						return;
					}
				}

				if (this._biz.SaveExcelEngine(dtExcel))
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                #endregion
            }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn엑셀업로드매입처_Click(object sender, EventArgs e)
        {
            try
            {
				if (this.ShowMessage("※ 경고 ※\n\n매입처 삭제 업로드시 매입처에 해당하는 유형 및 기자재가 모두 삭제되고 해당 데이터는 복구할 수 없습니다.\n\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				#region btn엑셀업로드
				_fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (_fileDlg.ShowDialog() != DialogResult.OK) return;
                if (this.IsFileLocked(_fileDlg.FileName))
                    return;
                else
                {
                    string 경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_HULL/Excel2/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday;
                    string fileName = FileMgr.GetUniqueFileName(경로, _fileDlg.FileName);

                    FileUploader.UploadFile(fileName, _fileDlg.FileName, "Upload/P_CZ_MA_HULL", "Excel2/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday);
                }

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + _fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IMO", "CD_VENDOR" };
                string[] argsPkNm = new string[] { DD("호선번호"), DD("매입처코드") };

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                if (this._biz.SaveExcelVendor(dtExcel))
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void btn엑셀업로드유형_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ShowMessage("※ 경고 ※\n\n유형 삭제 업로드시 유형에 해당하는 기자재가 모두 삭제되고 해당 데이터는 복구할 수 없습니다.\n\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				#region btn엑셀업로드
				_fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

				if (_fileDlg.ShowDialog() != DialogResult.OK) return;
                if (this.IsFileLocked(_fileDlg.FileName))
                    return;
                else
                {
                    string 경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_HULL/Excel3/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday;
                    string fileName = FileMgr.GetUniqueFileName(경로, _fileDlg.FileName);

                    FileUploader.UploadFile(fileName, _fileDlg.FileName, "Upload/P_CZ_MA_HULL", "Excel3/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday);
                }

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + _fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IMO", "CD_VENDOR", "NO_TYPE" };
				string[] argsPkNm = new string[] { DD("호선번호"), DD("매입처번호"), DD("유형번호") };

				for (int i = 0; i < argsPk.Length; i++)
				{
					if (!dtExcel.Columns.Contains(argsPk[i]))
					{
						this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
						return;
					}
				}

                if (this._biz.SaveExcelVendorType(dtExcel))
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                #endregion
            }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn엑셀업로드기부속_Click(object sender, EventArgs e)
        {
            try
            {
                #region btn엑셀업로드
                _fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (_fileDlg.ShowDialog() != DialogResult.OK) return;
                if (this.IsFileLocked(_fileDlg.FileName))
                    return;
                else
                {
                    string 경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_HULL/Excel4/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday;
                    string fileName = FileMgr.GetUniqueFileName(경로, _fileDlg.FileName);

                    FileUploader.UploadFile(fileName, _fileDlg.FileName, "Upload/P_CZ_MA_HULL", "Excel4/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday);
                }

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + _fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_PLATE" };
                string[] argsPkNm = new string[] { DD("부품번호") };

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                if (!dtExcel.Columns.Contains("EZCODE"))
                    dtExcel.Columns.Add("EZCODE");

                if (!dtExcel.Columns.Contains("EZCODE2"))
                    dtExcel.Columns.Add("EZCODE2");

                foreach (DataRow dr in dtExcel.Rows)
                {
                    dr["NO_PLATE"] = this.XML오류문자변경(dr["NO_PLATE"].ToString());
                    dr["NM_PLATE"] = this.XML오류문자변경(dr["NM_PLATE"].ToString());
                    dr["WEIGHT"] = D.GetDecimal(dr["WEIGHT"]);
                    dr["NO_POSITION"] = D.GetDecimal(dr["NO_POSITION"]);
                    dr["QT_PLATE"] = D.GetDecimal(dr["QT_PLATE"]);
                    dr["DC_RMK"] = this.XML오류문자변경(D.GetString(dr["DC_RMK"]).Replace("\n", Environment.NewLine));
                }

                bool 존재여부확인 = false;

                if (this.ShowMessage("동일 기부속 데이터 존재여부 확인을 하시겠습니까 ?\n동일한 기부속 데이터가 있는 경우 업로드 작업이 진행 되지 않습니다.", "QY2") == DialogResult.Yes)
                    존재여부확인 = true;

                if (this._biz.SaveExcelEngineItem(dtExcel, 존재여부확인))
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void btn엑셀업로드기자재_Click(object sender, EventArgs e)
		{
			try
			{
				#region btn엑셀업로드
				_fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

				if (_fileDlg.ShowDialog() != DialogResult.OK) return;
                if (this.IsFileLocked(_fileDlg.FileName))
                    return;
                else
                {
                    string 경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_HULL/Excel5/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday;
                    string fileName = FileMgr.GetUniqueFileName(경로, _fileDlg.FileName);

                    FileUploader.UploadFile(fileName, _fileDlg.FileName, "Upload/P_CZ_MA_HULL", "Excel5/" + Global.MainFrame.LoginInfo.UserID + "_" + Global.MainFrame.GetStringDetailToday);
                }

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + _fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                OleDbCommand oconn = new OleDbCommand(@"Select * From [다운로드양식$]", con);
                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IMO", "CD_VENDOR", "NO_TYPE", "NO_ITEM", "QT_ITEM" };
				string[] argsPkNm = new string[] { DD("호선번호"), DD("매입처코드"), DD("유형번호"), DD("품목번호"), DD("수량") };

				for (int i = 0; i < argsPk.Length; i++)
				{
					if (!dtExcel.Columns.Contains(argsPk[i]))
					{
						this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
						return;
					}
				}

                foreach(DataRow dr in dtExcel.Rows)
                {
                    dr["NO_PART"] = this.XML오류문자변경(dr["NO_PART"].ToString());
                    dr["NM_PART"] = this.XML오류문자변경(dr["NM_PART"].ToString());
                    dr["DC_MODEL"] = this.XML오류문자변경(dr["DC_MODEL"].ToString());
                    dr["NO_POS"] = this.XML오류문자변경(dr["NO_POS"].ToString());
                    dr["UCODE"] = this.XML오류문자변경(dr["UCODE"].ToString());
                    dr["UCODE2"] = this.XML오류문자변경(dr["UCODE2"].ToString());
                    dr["NO_DRAWING"] = this.XML오류문자변경(dr["NO_DRAWING"].ToString());
                    dr["NO_DRAWING2"] = this.XML오류문자변경(dr["NO_DRAWING2"].ToString());
                    dr["DC_RMK"] = this.XML오류문자변경(dr["DC_RMK"].ToString());
                    dr["DC_RMK1"] = this.XML오류문자변경(dr["DC_RMK1"].ToString());
                    dr["DC_RMK2"] = this.XML오류문자변경(dr["DC_RMK2"].ToString());
                    dr["DC_RMK3"] = this.XML오류문자변경(dr["DC_RMK3"].ToString());
                    dr["DC_RMK4"] = this.XML오류문자변경(dr["DC_RMK4"].ToString());
                    dr["DC_RMK5"] = this.XML오류문자변경(dr["DC_RMK5"].ToString());
                    dr["DC_RMK6"] = this.XML오류문자변경(dr["DC_RMK6"].ToString());
                    dr["DC_RMK7"] = this.XML오류문자변경(dr["DC_RMK7"].ToString());
                    dr["DC_RMK8"] = this.XML오류문자변경(dr["DC_RMK8"].ToString());
                    dr["DC_RMK9"] = this.XML오류문자변경(dr["DC_RMK9"].ToString());
                    dr["DC_RMK10"] = this.XML오류문자변경(dr["DC_RMK10"].ToString());
                }

				if (this._biz.SaveExcelVendorItem(dtExcel))
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                #endregion
            }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn엔진추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex호선정보.HasNormalRow) return;
                if (string.IsNullOrEmpty(this.txtIMO번호.Text))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblIMO번호.Text);
                    return;
                }

                this._flex엔진정보.Rows.Add();
                this._flex엔진정보.Row = this._flex엔진정보.Rows.Count - 1;

                this._flex엔진정보["NO_IMO"] = this.txtIMO번호.Text;
                this._flex엔진정보["NO_ENGINE"] = (D.GetDecimal(this._flex엔진정보.DataTable.Compute("MAX(NO_ENGINE)", "NO_IMO = '" + this.txtIMO번호.Text + "'")) + 1);

                this._flex엔진정보.AddFinished();
                this._flex엔진정보.Col = this._flex엔진정보.Cols.Fixed;
                this._flex엔진정보.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn엔진제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex엔진정보.HasNormalRow) return;
                if (this._biz.엔진사용여부체크(D.GetString(this._flex엔진정보["NO_IMO"]),
                                               D.GetString(this._flex엔진정보["NO_ENGINE"])))
                {
                    this.ShowMessage("사용중인 엔진은 제거할 수 없습니다.");
                    return;
                }

                if (this._biz.기부속여부체크(D.GetString(this._flex엔진정보["NO_IMO"]),
                                             D.GetString(this._flex엔진정보["NO_ENGINE"])))
                {
                    this.ShowMessage("기부속 정보가 존재 합니다.");
                    return;
                }

                this._flex엔진정보.GetDataRow(this._flex엔진정보.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn기부속추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex엔진정보.HasNormalRow) return;
                
                this._flex기부속정보.Rows.Add();
                this._flex기부속정보.Row = this._flex기부속정보.Rows.Count - 1;

                this._flex기부속정보["NO_IMO"] = D.GetString(this._flex엔진정보["NO_IMO"]);
                this._flex기부속정보["NO_ENGINE"] = D.GetString(this._flex엔진정보["NO_ENGINE"]);

                this._flex기부속정보.AddFinished();
                this._flex기부속정보.Col = this._flex기부속정보.Cols.Fixed;
                this._flex기부속정보.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn기부속제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex기부속정보.HasNormalRow) return;

                this._flex기부속정보.GetDataRow(this._flex기부속정보.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void btn매입처추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex호선정보.HasNormalRow) return;
				if (string.IsNullOrEmpty(this._flex호선정보["NO_IMO"].ToString()))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, "IMO 번호");
					return;
				}

				this._flex매입처정보.Rows.Add();
				this._flex매입처정보.Row = this._flex매입처정보.Rows.Count - 1;

				this._flex매입처정보["NO_IMO"] = this._flex호선정보["NO_IMO"].ToString();

				this._flex매입처정보.AddFinished();
				this._flex매입처정보.Col = this._flex매입처정보.Cols.Fixed;
				this._flex매입처정보.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        private void btn매입처제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex매입처정보.HasNormalRow) return;

                if (this._biz.유형여부체크(D.GetString(this._flex매입처정보["NO_IMO"]),
                                          D.GetString(this._flex매입처정보["CD_VENDOR"])))
                {
                    this.ShowMessage("유형 정보가 존재 합니다.");
                    return;
                }

                this._flex매입처정보.GetDataRow(this._flex매입처정보.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void btn유형추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex매입처정보.HasNormalRow) return;
				if (string.IsNullOrEmpty(this._flex매입처정보["CD_VENDOR"].ToString()))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, "매입처코드");
					return;
				}

				this._flex유형정보.Rows.Add();
				this._flex유형정보.Row = this._flex유형정보.Rows.Count - 1;

				this._flex유형정보["NO_IMO"] = this._flex매입처정보["NO_IMO"].ToString();
				this._flex유형정보["CD_VENDOR"] = this._flex매입처정보["CD_VENDOR"].ToString();
				this._flex유형정보["NO_TYPE"] = (D.GetDecimal(this._flex유형정보.DataTable.Compute("MAX(NO_TYPE)", "NO_IMO = '" + this._flex매입처정보["NO_IMO"].ToString() + "' AND CD_VENDOR = '" + this._flex매입처정보["CD_VENDOR"].ToString() + "'")) + 1);

				this._flex유형정보.AddFinished();
				this._flex유형정보.Col = this._flex유형정보.Cols.Fixed;
				this._flex유형정보.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn유형제거_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex유형정보.HasNormalRow) return;

				if (this._biz.기자재여부체크(D.GetString(this._flex유형정보["NO_IMO"]),
										    D.GetString(this._flex유형정보["CD_VENDOR"]),
										    D.GetString(this._flex유형정보["NO_TYPE"])))
				{
					this.ShowMessage("기자재 정보가 존재 합니다.");
					return;
				}

				this._flex유형정보.GetDataRow(this._flex유형정보.Row).Delete();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn기자재추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex유형정보.HasNormalRow) return;
				if (string.IsNullOrEmpty(this._flex유형정보["NO_TYPE"].ToString()))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, "유형번호");
					return;
				}

				this._flex기자재정보.Rows.Add();
                this._flex기자재정보.Row = this._flex기자재정보.Rows.Count - 1;

                this._flex기자재정보["NO_IMO"] = D.GetString(this._flex유형정보["NO_IMO"]);
                this._flex기자재정보["CD_VENDOR"] = D.GetString(this._flex유형정보["CD_VENDOR"]);
                this._flex기자재정보["NO_TYPE"] = D.GetString(this._flex유형정보["NO_TYPE"]);
                this._flex기자재정보["NO_ITEM"] = (D.GetDecimal(this._flex기자재정보.DataTable.Compute("MAX(NO_ITEM)", "NO_IMO = '" + this._flex유형정보["NO_IMO"].ToString() + "' AND CD_VENDOR = '" + this._flex유형정보["CD_VENDOR"].ToString() + "' AND NO_TYPE = '" + this._flex유형정보["NO_TYPE"].ToString() + "'")) + 1);

                this._flex기자재정보.AddFinished();
                this._flex기자재정보.Col = this._flex기자재정보.Cols.Fixed;
                this._flex기자재정보.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn기자재제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex기자재정보.HasNormalRow) return;

                this._flex기자재정보.GetDataRow(this._flex기자재정보.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_CODE_SUB)
                {
                    if (e.ControlName == this.ctx대분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                    }
                    else if (e.ControlName == this.ctx중분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx대분류.CodeValue);
                    }
                    else if (e.ControlName == this.ctx소분류.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                        e.HelpParam.P42_CD_FIELD2 = D.GetString(this.ctx중분류.CodeValue);
                    }
                    else if (e.ControlName == this.ctx조선소구.Name)
                    {
                        e.HelpParam.P41_CD_FIELD1 = "CZ_MA00001";
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void btn일괄업로드매입처_Click(object sender, EventArgs e)
		{
			P_CZ_MA_HULL_SUB dialog;

			try
			{
				dialog = new P_CZ_MA_HULL_SUB();
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
				P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB("K100", "MA", "P_CZ_MA_HULL", D.GetString(this._flex호선정보["NO_IMO"]), "P_CZ_MA_HULL" + "/" + D.GetString(this._flex호선정보["NO_IMO"]));
				dlg.ShowDialog(this);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 그리드 이벤트
		private void PageDataChanged(object sender, EventArgs e)
        {
            try
            {
				this.btn첨부파일.Enabled = true;

                this.btn엑셀양식다운로드.Enabled = true;
				this.btn엑셀양식다운로드엔진.Enabled = true;
				this.btn엑셀양식다운로드매입처.Enabled = true;
				this.btn엑셀양식다운로드유형.Enabled = true;
				this.btn엑셀양식다운로드기부속.Enabled = true;
                this.btn엑셀양식다운로드기자재.Enabled = true;

                this.btn엑셀업로드.Enabled = true;
				this.btn엑셀업로드엔진.Enabled = true;
				this.btn엑셀업로드매입처.Enabled = true;
				this.btn엑셀업로드유형.Enabled = true;
				this.btn엑셀업로드기부속.Enabled = true;
                this.btn엑셀업로드기자재.Enabled = true;

				this.btn일괄업로드유형.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex호선정보_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt, dt1;
            string key, filter;

            try
            {
                if (this._flex호선정보.RowState() == DataRowState.Added)
                {
                    this.btn엔진추가.Enabled = false;
                    this.btn엔진제거.Enabled = false;
                    this.btn기부속추가.Enabled = false;
                    this.btn기부속제거.Enabled = false;

                    this.btn매입처추가.Enabled = false;
                    this.btn매입처제거.Enabled = false;
					this.btn유형추가.Enabled = false;
					this.btn유형제거.Enabled = false;
					this.btn기자재추가.Enabled = false;
                    this.btn기자재제거.Enabled = false;
                }
                else
                {
                    this.btn엔진추가.Enabled = true;
                    this.btn엔진제거.Enabled = true;
                    this.btn기부속추가.Enabled = true;
                    this.btn기부속제거.Enabled = true;

                    this.btn매입처추가.Enabled = true;
                    this.btn매입처제거.Enabled = true;
					this.btn유형추가.Enabled = true;
					this.btn유형제거.Enabled = true;
					this.btn기자재추가.Enabled = true;
                    this.btn기자재제거.Enabled = true;
                }

                dt = null;
                dt1 = null;
                key = D.GetString(this._flex호선정보["NO_IMO"]);
                filter = "NO_IMO = '" + key + "'";

                if (this._flex호선정보.DetailQueryNeed == true)
                {
                    dt = this._biz.Search엔진정보(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 key,
                                                                 this.cbo엔진제조사S.SelectedValue,
                                                                 this.txt모델명.Text,
                                                                 this.txt일련번호.Text,
                                                                 (this.chk업로드.Checked == true ? "Y" : "N") });

					if (this.isAdmin || this.isLeader || this.isEng)
					{
						dt1 = this._biz.Search매입처정보(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	    key,
																	    this.ctx매입처.CodeValue,
                                                                        (this.isLeader == true ? "Y" : "N"),
                                                                        (this.isEng == true ? "Y" : "N") });
					}
                }

                this._flex엔진정보.Redraw = false;
                this._flex엔진정보.BindingAdd(dt, filter);

				if (this.isAdmin || this.isLeader || this.isEng)
				{
                    this._flex매입처정보.Redraw = false;
                    this._flex매입처정보.BindingAdd(dt1, filter);
                }
                
                //실사대비
                if (Certify.IsLive()) this.Set기부속정보();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
			{
                this._flex엔진정보.Redraw = true;
                this._flex매입처정보.Redraw = true;
            }
        }

        private void _flex엔진정보_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                //실사대비
                if (Certify.IsLive())
                    this.Set기부속정보();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex매입처정보_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.Set유형정보();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void _flex유형정보_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				this.Set기자재정보();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_MAKER":
                        e.Parameter.P41_CD_FIELD1 = "CZ_MA00003";
                        break;
                    case "NM_CLS_L":
                        e.Parameter.P41_CD_FIELD1 = "MA_B000030";
                        break;
                    case "NM_CLS_M":
                        if (string.IsNullOrEmpty(D.GetString(grid["CLS_L"])))
                            e.Cancel = true;
                        else
                        {
                            e.Parameter.P41_CD_FIELD1 = "MA_B000031";
                            e.Parameter.P42_CD_FIELD2 = D.GetString(grid["CLS_L"]);
                        }
                        break;
                    case "NM_CLS_S":
                        if (string.IsNullOrEmpty(D.GetString(grid["CLS_M"])))
                            e.Cancel = true;
                        else
                        {
                            e.Parameter.UserParams = "소분류;H_CZ_MA_CODE_SUB";
                            e.Parameter.P41_CD_FIELD1 = "MA_B000032";
                            e.Parameter.P42_CD_FIELD2 = Global.MainFrame.LoginInfo.Language;
                            e.Parameter.P11_ID_MENU = this.Name;
                            e.Parameter.P61_CODE1 = D.GetString(grid["CLS_M"]);
                        }
                        break;
                    case "CD_ITEM":
                        if (((Control)sender).Name == this._flex기자재정보.Name)
                            e.Parameter.P14_CD_PARTNER = D.GetString(this._flex유형정보["CD_VENDOR"]);

                        e.Parameter.P09_CD_PLANT = "001";
                        e.Parameter.P42_CD_FIELD2 = "009";
                        break;
                    case "NM_UNIT":
                        e.Parameter.P41_CD_FIELD1 = "MA_B000004";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);
                if (!grid.HasNormalRow) return;

                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_CLS_L":
                        if (e.OriginValue != e.Result.CodeName)
                        {
                            grid["CLS_M"] = string.Empty;
                            grid["NM_CLS_M"] = string.Empty;
                            grid["CLS_S"] = string.Empty;
                            grid["NM_CLS_S"] = string.Empty;
                        }
                        break;
                    case "NM_CLS_M":
                        if (e.OriginValue != e.Result.CodeName)
                        {
                            grid["CLS_S"] = string.Empty;
                            grid["NM_CLS_S"] = string.Empty;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);
                if (!grid.HasNormalRow) return;

                if (string.IsNullOrEmpty(D.GetString(grid["NM_CLS_L"])))
                {
                    grid["CLS_M"] = string.Empty;
                    grid["NM_CLS_M"] = string.Empty;
                    grid["CLS_S"] = string.Empty;
                    grid["NM_CLS_S"] = string.Empty;
                }

                if (string.IsNullOrEmpty(D.GetString(grid["NM_CLS_M"])))
                {
                    grid["CLS_S"] = string.Empty;
                    grid["NM_CLS_S"] = string.Empty;
                }

				if ((this.isAdmin || this.isEng) && grid.Name == this._flex유형정보.Name)
				{
					int rowIndex = 1;
					int columnIndex = this._flex유형정보.Cols["FILE_PATH_MNG"].Index;

					for (rowIndex = this._flex유형정보.Rows.Fixed; rowIndex < this._flex유형정보.Rows.Count; rowIndex++)
					{
						if (!string.IsNullOrEmpty(D.GetString(this._flex유형정보.Rows[rowIndex]["FILE_PATH_MNG"])))
							this._flex유형정보.SetCellImage(rowIndex, columnIndex, fileIcon);
						else
							this._flex유형정보.SetCellImage(rowIndex, columnIndex, null);
					}
				}
			}
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex엔진정보_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = (FlexGrid)sender;

                string colname = _flex엔진정보.Cols[e.Col].Name;
                string oldValue = D.GetString(grid.GetData(e.Row, e.Col));
                string newValue = grid.EditData;

                switch (colname)
                {
                    case "SERIAL":
                        if (grid["NO_IMO"].ToString() != "HGS0001" && this._biz.일련번호중복체크(newValue))
                        {
                            this.ShowMessage(공통메세지._의값이중복되었습니다, this.DD("일련번호"));
                            this._flex엔진정보["SERIAL"] = oldValue;
                            e.Cancel = true;
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		public void _flex유형정보_DoubleClick(object sender, EventArgs e)
		{
			string fileCode, fileName, query;
            P_CZ_MA_FILE_SUB dlg;

            try
			{
				if (!this._flex유형정보.HasNormalRow) return;
				if (this._flex유형정보.MouseRow < this._flex유형정보.Rows.Fixed) return;
				if (this._flex유형정보.RowState() != DataRowState.Unchanged) return;

                query = @"SELECT 1 
                          FROM CZ_MA_CODEDTL
                          WHERE CD_COMPANY = 'K100'
                          AND CD_FIELD = 'CZ_MA00038'
                          AND YN_USE = 'Y'
                          AND CD_FLAG1 = '{0}'
                          AND CD_FLAG2 = '{1}'
                          AND CD_FLAG3 = '{2}'";

				if (this._flex유형정보.Cols[_flex유형정보.Col].Name == "FILE_PATH_MNG")
				{
                    DataTable dt = DBHelper.GetDataTable(string.Format(query, new string[] { this._flex유형정보["CLS_L"].ToString(),
                                                                                             this._flex유형정보["CLS_M"].ToString(),
                                                                                             this._flex유형정보["CLS_S"].ToString() }));

                    if (!this.isAdmin && Global.MainFrame.LoginInfo.UserID != "S-223" && dt.Rows.Count > 0)
                    {
                        this.ShowMessage("해당 파일 열람 권한이 없습니다.");
                        return;
                    }

                    fileCode = D.GetString(this._flex유형정보["NO_IMO"]) + "_" + D.GetString(this._flex유형정보["CD_VENDOR"]) + "_" + D.GetString(this._flex유형정보["NO_TYPE"]);
                    
                    dlg = new P_CZ_MA_FILE_SUB("K100", "MA", "P_CZ_MA_HULL", fileCode, "P_CZ_MA_HULL" + "/" + D.GetString(this._flex유형정보["NO_IMO"]));
                    
                    if (!this.isAdmin)
                        dlg.UseGrant = false;
                    
                    dlg.ShowDialog(this);

					fileName = this._biz.SearchFileInfo(fileCode);

					if (!string.IsNullOrEmpty(fileName))
					{
						this._flex유형정보["FILE_PATH_MNG"] = fileName;
						this._flex유형정보.SetCellImage(this._flex유형정보.Row, this._flex유형정보.Cols["FILE_PATH_MNG"].Index, this.fileIcon);
					}
					else
					{
						this._flex유형정보["FILE_PATH_MNG"] = string.Empty;
						this._flex유형정보.SetCellImage(this._flex유형정보.Row, this._flex유형정보.Cols["FILE_PATH_MNG"].Index, null);
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex_BeforeMouseDown(object sender, BeforeMouseDownEventArgs e)
		{
			try
			{
				if (!this.isAdmin)
				{
					if (e.Button == MouseButtons.Right)
					{
						e.Cancel = true;
						this.ShowMessage("[경고] 해당 리스트는 마우스 오른쪽 클릭을 할 수 없습니다.");
					}
					else if (Control.ModifierKeys == Keys.Control)
					{
						e.Cancel = true;
						this.ShowMessage("[경고] 해당 리스트는 Control + 클릭을 할 수 없습니다.");
					}
					else if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
					{
						e.Cancel = true;
						this.ShowMessage("[경고] 해당 리스트는 Control + Shift + 클릭을 할 수 없습니다.");
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (!this.isAdmin)
				{
					if (e.Control && e.KeyCode == Keys.C)
					{
						Clipboard.SetText("[경고] 해당 리스트 데이터는 복사가 금지 되어 있습니다.");
						this.ShowMessage("[경고] 해당 리스트 데이터는 복사가 금지 되어 있습니다.");
					}
				}

                if (e.KeyData == (Keys.Control | Keys.V))
                {
                    FlexGrid grid = (FlexGrid)sender;

                    string[,] clipboard = Util.GetClipboardValues();
                    int index = grid.Row;  // 시작인덱스 저장 (행이 클립보다 많은 경우는 .Row가 안바뀌지만 클립보드가 더 많은 경우에는 .Row가 바뀌므로 미리 저장함)

                    for (int i = 0; i < clipboard.GetLength(0); i++)
                    {
                        int row = index + i;
                        int j = 0;

                        for (int col = grid.Col; col < grid.Cols.Count; col++)
                        {
                            // 클립보드 넘어가는 순간 제외
                            if (j == clipboard.GetLength(1))
                                break;

                            // 비허용 컬럼
                            if (!grid.Cols[col].Visible || !grid.Cols[col].AllowEditing)
                                continue;

                            grid[row, col] = clipboard[i, j];
                            j++;
                        }

                        // 마지막 행이면 종료
                        if (i == clipboard.GetLength(0) - 1)
                            break;

                        // 클립보드는 아직 남았는데 그리드의 마지막 행인 경우 행 추가
                        if (row == grid.Rows.Count - 1)
                        {
                            // 행 추가
                            grid.Rows.Add();
                            //flexL.Row = flexL.Rows.Count - 1;

                            if (grid.Name == this._flex매입처정보.Name)
                            {
                                grid[row + 1, "NO_IMO"] = this._flex호선정보["NO_IMO"].ToString();
                            }
                            else if (grid.Name == this._flex유형정보.Name)
                            {
                                grid[row + 1, "NO_IMO"] = this._flex매입처정보["NO_IMO"].ToString();
                                grid[row + 1, "CD_VENDOR"] = this._flex매입처정보["CD_VENDOR"].ToString();
                                grid[row + 1, "NO_TYPE"] = (D.GetDecimal(this._flex유형정보.DataTable.Compute("MAX(NO_TYPE)", "NO_IMO = '" + this._flex매입처정보["NO_IMO"].ToString() + "' AND CD_VENDOR = '" + this._flex매입처정보["CD_VENDOR"].ToString() + "'")) + 1);
                            }
                            else if (grid.Name == this._flex기자재정보.Name)
                            {
                                grid[row + 1, "NO_IMO"] = D.GetString(this._flex유형정보["NO_IMO"]);
                                grid[row + 1, "CD_VENDOR"] = D.GetString(this._flex유형정보["CD_VENDOR"]);
                                grid[row + 1, "NO_TYPE"] = D.GetString(this._flex유형정보["NO_TYPE"]);
                            }
                        }
                    }

                    grid.AddFinished();
                }
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex호선정보_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flex호선정보.HasNormalRow || !this.chk자매호선.Checked) return;

				CellStyle cellStyle = this._flex호선정보.Rows[e.Row].Style;

				switch (D.GetString(this._flex호선정보[e.Row, "CD_SISTER"]))
				{
					case "P":
						if (cellStyle == null || cellStyle.Name != "부모호선")
							this._flex호선정보.Rows[e.Row].Style = this._flex호선정보.Styles["부모호선"];
						break;
					case "C":
						if (cellStyle == null || cellStyle.Name != "자식호선")
							this._flex호선정보.Rows[e.Row].Style = this._flex호선정보.Styles["자식호선"];
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region 기타 메소드
		private void Set기부속정보()
        {
            DataTable dt;
            string key, key1, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex엔진정보["NO_IMO"]);
                key1 = D.GetString(this._flex엔진정보["NO_ENGINE"]);
                filter = "NO_IMO = '" + key + "' AND NO_ENGINE = '" + key1 + "'";

                if (this._flex엔진정보.DetailQueryNeed == true)
                {
                    dt = this._biz.Search기부속정보(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   key,
                                                                   key1,
                                                                   (this.isEzcode && this.내부접속여부 && Certify.IsLive() ? "Y" : "N"),
                                                                   (this.chk업로드.Checked == true ? "Y" : "N")});

                    if (!this.isEzcode || !this.내부접속여부 || !Certify.IsLive())
                    {
                        dt.Columns.Remove("EZCODE");
                        dt.Columns.Remove("EZCODE2");
                    }
                }

                this._flex기부속정보.Redraw = false;
                this._flex기부속정보.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
			{
                this._flex기부속정보.Redraw = true;
			}
        }

		private void Set유형정보()
		{
			try
			{
				DataTable dt;
				string key, key1, filter, query;

				try
				{
                    if (!this._flex매입처정보.HasNormalRow) return;

                    this._flex유형정보.BeginSetting(1, 1, false);
					this._flex유형정보.Cols.Count = 6;

                    if (this.isAdmin || this.isEng)
                    {
                        this._flex유형정보.SetCol("FILE_PATH_MNG", "첨부파일", 100);
                        this._flex유형정보.SetDummyColumn("FILE_PATH_MNG");
                    }

					query = @"SELECT CD_FLAG2 AS CD_COLUMN,
							  	     NM_SYSDEF AS NM_COLUMN,
                                     ISNULL(CD_FLAG5, 0) AS SIZE
							  FROM CZ_MA_CODEDTL WITH(NOLOCK)
							  WHERE CD_COMPANY = 'K100'
							  AND CD_FIELD = 'CZ_MA00033'
                              AND CD_FLAG3 = 'TYPE'
							  AND CD_FLAG1 = '" + this._flex매입처정보["CD_VENDOR"].ToString() + "'" + Environment.NewLine +
							 "ORDER BY CD_FLAG4";

					dt = DBHelper.GetDataTable(query);

					foreach (DataRow dr in dt.Rows)
					{
                        if (this.isAdmin)
                            this._flex유형정보.SetCol(dr["CD_COLUMN"].ToString(), dr["NM_COLUMN"].ToString(), 100, true);
                        else if (this.isEng || (this.isLeader && D.GetDecimal(dr["SIZE"]) > 0))
                            this._flex유형정보.SetCol(dr["CD_COLUMN"].ToString(), dr["NM_COLUMN"].ToString(), 100, false);
					}

                    if (this.isAdmin)
                        this._flex유형정보.SetCol("DC_RMK", "비고", 100, true);
                    else
                        this._flex유형정보.SetCol("DC_RMK", "비고", 100, false);
                    
                    this._flex유형정보.AllowCache = false;
					this._flex유형정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

					dt = null;
					key = D.GetString(this._flex매입처정보["NO_IMO"]);
					key1 = D.GetString(this._flex매입처정보["CD_VENDOR"]);
					filter = "NO_IMO = '" + key + "' AND CD_VENDOR = '" + key1 + "'";

					if (this._flex매입처정보.DetailQueryNeed == true)
					{
						dt = this._biz.Search유형정보(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	 key,
																     key1 });
					}

                    this._flex유형정보.Redraw = false;
                    this._flex유형정보.BindingAdd(dt, filter);
				}
				catch (Exception ex)
				{
					MsgEnd(ex);
				}
                finally
				{
                    this._flex유형정보.Redraw = true;
                }
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Set기자재정보()
        {
            DataTable dt;
            string query, key, key1, key2, filter;

            try
            {
                if (!this._flex유형정보.HasNormalRow) return;

                this._flex기자재정보.BeginSetting(1, 1, false);
                this._flex기자재정보.Cols.Count = 15;

                query = @"SELECT CD_FLAG2 AS CD_COLUMN,
							  	 NM_SYSDEF AS NM_COLUMN 
						  FROM CZ_MA_CODEDTL WITH(NOLOCK)
					      WHERE CD_COMPANY = 'K100'
						  AND CD_FIELD = 'CZ_MA00033'
                          AND CD_FLAG3 = 'ITEM'
						  AND CD_FLAG1 = '" + this._flex유형정보["CD_VENDOR"].ToString() + "'" + Environment.NewLine +
                         "ORDER BY CD_FLAG4";

                dt = DBHelper.GetDataTable(query);

                foreach (DataRow dr in dt.Rows)
                {
                    this._flex기자재정보.SetCol(dr["CD_COLUMN"].ToString(), dr["NM_COLUMN"].ToString(), 100, true);
                }

                this._flex기자재정보.SetCol("DC_RMK", "비고", 100, true);

                this._flex기자재정보.AllowCache = false;
                this._flex기자재정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

                dt = null;
                key = D.GetString(this._flex유형정보["NO_IMO"]);
                key1 = D.GetString(this._flex유형정보["CD_VENDOR"]);
				key2 = D.GetString(this._flex유형정보["NO_TYPE"]);
				filter = "NO_IMO = '" + key + "' AND CD_VENDOR = '" + key1 + "' AND NO_TYPE = '" + key2 + "'";

                if (this._flex유형정보.DetailQueryNeed == true)
                {
                    dt = this._biz.Search기자재정보(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   key,
                                                                   key1,
																   key2 });
                }

                this._flex기자재정보.Redraw = false;
                this._flex기자재정보.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
			{
                this._flex기자재정보.Redraw = true;
            }
        }

        /// <summary>
        /// HGS**** : 현대재고문의호선
        /// PLT**** : Plant No.
        /// BRG**** : Barge Ship
        /// G****** : Generator No.
        /// ******* : IMO No.
        /// </summary>
        /// <param name="IMO번호"></param>
        /// <returns></returns>
        private bool IMO번호형식확인(string IMO번호)
        {
            Regex regex;

            try
            {
                if (IMO번호.Length > 7) return false;

                regex = new Regex(@"(G|P|B|H|\d){1}(L|R|G|\d){1}(T|G|S|\d){1}\d{4}");
                return regex.IsMatch(IMO번호);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        private string XML오류문자변경(string text)
        {
            string result;

            result = Regex.Replace(text, "[]", ""); //아스키 제어문자 제거

            return result;
        }

        private bool IsFileLocked(string fileName)
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                Global.MainFrame.MsgEnd(ex);
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }
        #endregion
    }
}