using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.Windows.Print;
using DzHelpFormLib;
using System.Collections.Generic;
using Duzon.ERPU.MF;
using System.Drawing;
using DX;
using System.IO;
using System.Linq;

namespace cz
{
    public partial class P_CZ_SA_GIR_PACK : PageBase
    {
        #region ♣ 생성자 & 변수 선언
        private P_CZ_SA_GIR_PACK_BIZ _biz;
        private FreeBinding _기본정보 = new FreeBinding(); //FreeBinding 생성
        private FreeBinding _송장정보 = new FreeBinding();

        //영업환경설정  : 수주수량 초과허용
        private bool is수주수량초과허용 = false;   //영업환경설정 : 수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
        private string 파일번호;
        private string 협조전번호;
        private string 회사코드;
        private decimal 차수;

        public P_CZ_SA_GIR_PACK()
        {
            StartUp.Certify(this);
            InitializeComponent();
            
            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
            this.회사코드 = this.LoginInfo.CompanyCode;
            this._biz = new P_CZ_SA_GIR_PACK_BIZ(this.회사코드);
        }

        public P_CZ_SA_GIR_PACK(string 파일번호)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;
            this.파일번호 = 파일번호;
            this.회사코드 = this.LoginInfo.CompanyCode;
            this._biz = new P_CZ_SA_GIR_PACK_BIZ(this.회사코드);
        }

        public P_CZ_SA_GIR_PACK(string 페이지명, string 협조전번호, string 회사코드)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
            this.PageName = 페이지명;
            this.협조전번호 = 협조전번호;
            this.회사코드 = 회사코드;
            this._biz = new P_CZ_SA_GIR_PACK_BIZ(this.회사코드);
        }

        public P_CZ_SA_GIR_PACK(string 페이지명, string 회사코드, string 협조전번호, decimal 차수)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
            this.PageName = 페이지명;
            this.회사코드 = 회사코드;
            this.협조전번호 = 협조전번호;
            this.차수 = 차수;
            this._biz = new P_CZ_SA_GIR_PACK_BIZ(this.회사코드);
        }
        #endregion

        #region ♣ 초기화 이벤트 / 메소드
        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._품목정보 };

            this._품목정보.BeginSetting(1, 1, false);

            this._품목정보.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._품목정보.SetCol("NO_SO", "수주번호", 100);
            this._품목정보.SetCol("SEQ_SO", "수주항번", 40);
            this._품목정보.SetCol("NO_DSP", "순번", 40);
            this._품목정보.SetCol("NM_SUBJECT", "주제", false);
            this._품목정보.SetCol("CD_ITEM_PARTNER", "매출처품번", 100); // ITEM CODE 필드 추가 !!
            this._품목정보.SetCol("NM_ITEM_PARTNER", "매출처품명", 120); // DESCRIPTION 필드 추가 !!
            this._품목정보.SetCol("CD_ITEM", "품목코드", 100);
            this._품목정보.SetCol("NM_ITEM", "품목명", 120);
            this._품목정보.SetCol("STND_ITEM", "규격", false);
            this._품목정보.SetCol("DT_DUEDATE", "납기요청일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._품목정보.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY); // 입고일자 필드 추가 !!
            this._품목정보.SetCol("DT_REQGI", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._품목정보.SetCol("YN_ADD_STOCK", "추가반출", 60, true, CheckTypeEnum.Y_N);

            this._품목정보.SetCol("NO_LOCATION", "로케이션", 80);
            this._품목정보.SetCol("NO_LOCATION_STOCK", "재고로케이션", 80);
            this._품목정보.SetCol("NM_GI", "출고형태", false);
            this._품목정보.SetCol("CD_EXCH", "통화명", false);
            this._품목정보.SetCol("QT_GIR_IM", "재고수량", false);
            this._품목정보.SetCol("QT_INV", "현재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_BL", "납부대상수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_TAX", "납부수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("CUSTOMS", "납부금액", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._품목정보.SetCol("QT_SO", "수주수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_GIR", "의뢰수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_GIR_STOCK", "재고의뢰수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._품목정보.SetCol("QT_GI", "출고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            this._품목정보.SetCol("UM", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._품목정보.SetCol("AM_GIR", "금액", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._품목정보.SetCol("RT_EXCH", "환율", false);
            this._품목정보.SetCol("AM_GIRAMT", "원화금액", 60, false, typeof(decimal), FormatTpType.MONEY);
            this._품목정보.SetCol("AM_VAT", "부가세", false);
            this._품목정보.SetCol("AMT", "합계", 60, false, typeof(decimal), FormatTpType.MONEY);
            this._품목정보.SetCol("UNIT", "재고단위", false);
            this._품목정보.SetCol("CD_SUPPLIER", "매입처코드", false);
            this._품목정보.SetCol("NM_SUPPLIER", "매입처", 120);
            this._품목정보.SetCol("GI_PARTNER", "납품처코드", false);
            this._품목정보.SetCol("LN_PARTNER", "납품처명", 120);
            this._품목정보.SetCol("NM_PROJECT", "프로젝트", false);
            this._품목정보.SetCol("CD_SALEGRP", "영업그룹", false);
            this._품목정보.SetCol("NM_SALEGRP", "영업그룹명", false);
            this._품목정보.SetCol("DC_RMK", "비고", 150);

            this._품목정보.SetCol("NO_PO_PARTNER", "매출처발주번호", 140);
            this._품목정보.SetCol("NO_POLINE_PARTNER", "매출처발주항번", 140, false, typeof(decimal), FormatTpType.QUANTITY);

            this._품목정보.SetDummyColumn("S");
            this._품목정보.SettingVersion = "1.0.0.1";
            this._품목정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._품목정보.SetExceptSumCol("UM", "RT_EXCH", "SEQ_PROJECT");

            this._품목정보.SetCodeHelpCol("NM_SL", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CODE", "NAME" }, ResultMode.FastMode);
            this._품목정보.SetCodeHelpCol("GI_PARTNER", Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });

            this._품목정보.AddMyMenu = true;
            this._품목정보.AddMenuSeperator();
            ToolStripMenuItem parent = this._품목정보.AddPopup("관련 현황");
            this._품목정보.AddMenuItem(parent, "현재고조회", SubMenuClick);

            this._품목정보.Styles.Add("미납부").ForeColor = Color.Black;
            this._품목정보.Styles.Add("미납부").BackColor = Color.White;
            this._품목정보.Styles.Add("납부").ForeColor = Color.Red;
            this._품목정보.Styles.Add("납부").BackColor = Color.Yellow;

            this._품목정보.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.PageDataChanged);

            this._품목정보.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._품목정보.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);

            this._기본정보.ControlValueChanged += new FreeBindingEventHandler(this.ControlValueChanged);
            this._송장정보.ControlValueChanged += new FreeBindingEventHandler(this.ControlValueChanged);
            
            this.btn제출.Click += new EventHandler(this.btn제출_Click);
			this.btn제출취소.Click += btn제출취소_Click;
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn삭제요청.Click += new EventHandler(this.btn삭제요청_Click);
            this.btn삭제요청취소.Click += new EventHandler(this.btn삭제요청취소_Click);
            this.btn협조전적용.Click += new EventHandler(this.btn협조전적용_Click);
            this.btn수주적용.Click += new EventHandler(this.btn수주적용_Click);
			this.btn송품서류등록.Click += Btn송품서류등록_Click;
			this.btn자동메일발송설정.Click += Btn자동메일발송설정_Click;
            
            this.cboMainCategory.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
            this.cboSubCategory.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
			this.cboHSCode.SelectionChangeCommitted += CboHSCode_SelectionChangeCommitted;

            this.ctx원산지.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx도착국가.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.ctx수주번호.QueryBefore += new BpQueryHandler(this.ctx수주번호검색_QueryBefore);
            this.ctx수주번호.QueryAfter += new BpQueryHandler(this.ctx수주번호검색_QueryAfter);

			this.ctx납품처.QueryBefore += Ctx납품처_QueryBefore;
            this.ctx납품처.QueryAfter += new BpQueryHandler(this.ctxDeliveryTo_QueryAfter);

            this.ctx매출처.QueryAfter += new BpQueryHandler(this.ctx매출처_QueryAfter);
            this.ctx수출자.QueryAfter += new BpQueryHandler(this.ctx수출자_QueryAfter);
            this.ctx수하인.QueryAfter += new BpQueryHandler(this.ctx수하인_QueryAfter);
            this.ctx착하통지처.QueryAfter += new BpQueryHandler(this.ctx착하통지처_QueryAfter);
            this.ctx호선번호.QueryAfter += new BpQueryHandler(this.ctx호선번호_QueryAfter);
            this.ctx호선번호.CodeChanged += new EventHandler(this.ctx호선번호_CodeChanged);

            this.txt수하인.TextChanged += new EventHandler(this.txt수하인_TextChanged);
            this.txt착하통지처.TextChanged += new EventHandler(this.txt착하통지처_TextChanged);

            this.txt매출처주소1.KeyDown += Control_KeyDown;
            this.txt매출처주소2.KeyDown += Control_KeyDown;
            this.txt수출자주소1.KeyDown += Control_KeyDown;
            this.txt수출자주소2.KeyDown += Control_KeyDown;
            this.txt수하인주소1.KeyDown += Control_KeyDown;
            this.txt수하인주소2.KeyDown += Control_KeyDown;
            this.txt착하통지처주소1.KeyDown += Control_KeyDown;
            this.txt착하통지처주소2.KeyDown += Control_KeyDown;

            foreach (var c in GetAll(this, typeof(DropDownComboBox)))
            {
                DropDownComboBox dropDownComboBox = (DropDownComboBox)c;
                dropDownComboBox.MouseWheel += DropDownComboBox_MouseWheel;
            }
        }

		private void Btn송품서류등록_Click(object sender, EventArgs e)
		{
            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호는 필수 입력 항목입니다." + Environment.NewLine;

                    return;
                }

                if (string.IsNullOrEmpty(this.dtp의뢰일자.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰일자.Text);
                    else
                        this.txt경고메시지.Text += "의뢰일자는 필수 입력 항목입니다." + Environment.NewLine;
                    
                    return;
                }

                P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "SA", "P_CZ_SA_GIR", this.txt의뢰번호.Text, "P_CZ_SA_GIR" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this.dtp의뢰일자.Text.Substring(0, 4));
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void CboHSCode_SelectionChangeCommitted(object sender, EventArgs e)
		{
            try
            {
                this._송장정보.CurrentRow["CD_PRODUCT"] = D.GetString(this.cboHSCode.SelectedValue);

                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxExt textBox;

            try
            {
                textBox = (TextBoxExt)sender;

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V && 
                    Clipboard.GetData(DataFormats.Text) != null)
				{
                    textBox.Text += Clipboard.GetData(DataFormats.Text).ToString().Replace(Environment.NewLine, " ");
                    Clipboard.Clear();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn자동메일발송설정_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호가 존재하지 않습니다." + Environment.NewLine;

                    return;
				}

                P_CZ_SA_GIR_AUTO_MAIL dialog = new P_CZ_SA_GIR_AUTO_MAIL(this.회사코드, this.txt의뢰번호.Text);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Ctx납품처_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P00_CHILD_MODE = "납품처";
                e.HelpParam.P61_CODE1 = @"CD_PARTNER AS CODE, LN_PARTNER AS NAME,
                                          (ISNULL(DC_ADDRESS, '') + ' ' + ISNULL(DC_ADDRESS1, '') + ' ' + ISNULL(NM_PIC, '') + ' ' + ISNULL(NO_TEL, '')) AS DC_DELIVERY_ADDR";
                e.HelpParam.P62_CODE2 = "CZ_MA_DELIVERY";
                e.HelpParam.P63_CODE3 = string.Format("WHERE CD_COMPANY = '{0}' AND ISNULL(YN_USE, 'N') = 'Y' AND CD_PARTNER LIKE 'DLV%'", 회사코드);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                this.SetToolBarButtonState(true, true, true, true, true);

                ds = this.GetComboData("S;CZ_SA00020",
                                       "S;CZ_SA00012",
                                       "S;TR_IM00003",
                                       "S;CZ_SA00005",
                                       "S;CZ_SA00013",
                                       "S;PU_C000016",
                                       "N;MA_B000005",
                                       "S;CZ_SA00018");

                this.cboMainCategory.DataSource = ds.Tables[0];
                this.cboMainCategory.DisplayMember = "NAME";
                this.cboMainCategory.ValueMember = "CODE";

                this.cbo선적조건.DataSource = ds.Tables[2];
                this.cbo선적조건.DisplayMember = "NAME";
                this.cbo선적조건.ValueMember = "CODE";

                this.cboINCOTERMS지역.DataSource = ds.Tables[3];
                this.cboINCOTERMS지역.DisplayMember = "NAME";
                this.cboINCOTERMS지역.ValueMember = "CODE";

                this.cbo지불조건.DataSource = ds.Tables[4];
                this.cbo지불조건.DisplayMember = "NAME";
                this.cbo지불조건.ValueMember = "CODE";

                this.cbo거래구분.DataSource = ds.Tables[5];
                this.cbo거래구분.DisplayMember = "NAME";
                this.cbo거래구분.ValueMember = "CODE";

                this.cbo통화.DataSource = ds.Tables[6];
                this.cbo통화.DisplayMember = "NAME";
                this.cbo통화.ValueMember = "CODE";

                this.cboHSCode.DataSource = new DataView(ds.Tables[7], string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable();
                this.cboHSCode.DisplayMember = "NAME";
                this.cboHSCode.ValueMember = "CODE";

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("CODE", typeof(string));
                dt1.Columns.Add("NAME", typeof(string));
                DataRow row1 = null;
                row1 = dt1.NewRow(); row1["CODE"] = "N"; row1["NAME"] = DD("출고"); dt1.Rows.Add(row1);
                //row1 = dt1.NewRow(); row1["CODE"] = "Y"; row1["NAME"] = DD("반품"); dt1.Rows.Add(row1);

                this.cbo출고구분.DataSource = dt1;
                this.cbo출고구분.DisplayMember = "NAME";
                this.cbo출고구분.ValueMember = "CODE";

                this.cur외화금액.Mask = this.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.FOREIGN_MONEY, FormatFgType.INSERT);

                if (회사코드 == "S100")
                    this.btn자동메일발송설정.Visible = false;
                else
                    this.btn자동메일발송설정.Visible = true;

                if (!string.IsNullOrEmpty(this.협조전번호))
                    this.협조전적용(this.회사코드, this.협조전번호, this.차수);
                else
                    this.InitData();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private static IEnumerable<Control> GetAll(Control control, Type type = null)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        private static void DropDownComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void InitData()
        {
            try
            {
                #region 기본정보
                DataSet ds = this._biz.기본정보검색(new object[] { this.회사코드,
                                                                  Global.MainFrame.LoginInfo.Language,
                                                                  string.Empty,
                                                                  0 });

                this._기본정보.SetBinding(ds.Tables[0], this.tpg기본정보);
                this._기본정보.ClearAndNewRow();        // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해
                this._기본정보.AcceptChanges();

                this._품목정보.Binding = ds.Tables[1];
                this._품목정보.AcceptChanges();

                this.cboSubCategory.DataSource = null;

                if (!string.IsNullOrEmpty(this.파일번호))
                {
                    this.ctx수주번호.CodeValue = this.파일번호;
                    this.ctx수주번호.CodeName = this.파일번호;

                    this.수주번호검색(this.파일번호);
                    this.파일번호 = string.Empty;
                }
                else
                {
                    this.ctx수주번호.CodeValue = string.Empty;
                    this.ctx수주번호.CodeName = string.Empty;
                }
                #endregion

                #region 송장정보
                DataTable dt = this._biz.송장정보검색(new object[] { this.회사코드,
                                                                     string.Empty, 
                                                                     0 });

                this._송장정보.SetBinding(dt, this.tpg송장정보);
                this._송장정보.ClearAndNewRow();
                this._송장정보.AcceptChanges();

                this.cboHSCode.SelectedValue = string.Empty;
                #endregion

                this.수정여부설정(true);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool IsChanged()
        {
            return (base.IsChanged() || _기본정보.GetChanges() != null ? true : false || _송장정보.GetChanges() != null ? true : false);
        } 
        #endregion

        #region ♣ 메인버튼 클릭
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string 반품여부;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                //TODO KKH : 추후 커스터마이징 예정
                반품여부 = "N";

                P_CZ_SA_GIR_SCH_SUB dlg = new P_CZ_SA_GIR_SCH_SUB(반품여부, true);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.임시변수초기화();
                    this.협조전적용(Global.MainFrame.LoginInfo.CompanyCode, D.GetString(dlg.returnParams[0]), 0);
                }   
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 협조전적용(string 회사코드, string 의뢰번호, decimal 차수)
        {
            DataTable dt;
            DataRow dr;

            try
            {
                #region 기본정보
                DataSet ds = this._biz.기본정보검색(new object[] { 회사코드,
                                                                  Global.MainFrame.LoginInfo.Language,
                                                                  의뢰번호,
                                                                  차수 });

                dt = ds.Tables[0];

                this._기본정보.SetBinding(ds.Tables[0].Copy(), this.tpg기본정보);
                this._기본정보.ClearAndNewRow();
                this._기본정보.AcceptChanges();

                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];

                    this.SetSubCategory(D.GetString(dr["CD_PACK_CATEGORY"]));

                    this._기본정보.SetDataTable(dt);
                    this._기본정보.AcceptChanges();
                }
                #endregion

                #region 품목정보
                this._품목정보.Binding = ds.Tables[1];
                this._품목정보.AcceptChanges();
                #endregion

                #region 송장정보
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    dt = this._biz.송장정보검색(new object[] { 회사코드, 
                                                               D.GetString(ds.Tables[0].Rows[0]["NO_INV"]),
                                                               차수 });

                    this._송장정보.SetBinding(dt, this.tpg송장정보);

                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        this.cboHSCode.SelectedValue = dr["CD_PRODUCT"].ToString();

                        this._송장정보.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨
                        this._송장정보.AcceptChanges();
                    }
                    else
                    {
                        this._송장정보.ClearAndNewRow();
                        this._송장정보.AcceptChanges();
                    }
                }
                #endregion

                this.수정여부설정(false);
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

                //초기화 : 조회조건, 헤더그리드, 라인그리드 모두 초기화 된다.
                this.임시변수초기화();
                this.InitData();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;
            
            string 협조전상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

            if (협조전상태 == "C")
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", this.DD("종결"));
                else
                    this.txt경고메시지.Text += "종결상태는 삭제할 수 없습니다." + Environment.NewLine;

                return false;
            }
            else if (협조전상태 == "R")
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_확정상태는 삭제요청으로 삭제해야 합니다.");
                else
                    this.txt경고메시지.Text += "확정상태는 삭제요청으로 삭제해야 합니다." + Environment.NewLine;

                return false;
            }
            else if (협조전상태 == "D")
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", this.DD("삭제요청"));
                else
                    this.txt경고메시지.Text += "삭제요청 상태는 삭제할 수 없습니다." + Environment.NewLine;

                return false;
            }
            else if (협조전상태 == "O")
			{
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", this.DD("제출"));
                else
                    this.txt경고메시지.Text += "제출 상태는 삭제할 수 없습니다." + Environment.NewLine;

                return false;
            }

            if (!this._biz.의뢰번호중복체크(this.txt의뢰번호.Text))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt의뢰번호.Text);
                else
                    this.txt경고메시지.Text += "의뢰번호로 등록된 자료가 없습니다." + Environment.NewLine;

                return false;
            }

            if (!this._biz.송장번호중복체크(this.txt송장번호.Text))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt송장번호.Text);
                else
                    this.txt경고메시지.Text += "송장번호로 등록된 자료가 없습니다." + Environment.NewLine;

                return false;
            }
            
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                return true;
            else
                return false;            
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            bool result;
            
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!BeforeDelete()) return;

                result = false;

                result = this._biz.기본정보제거(this.txt의뢰번호.Text);
                result = this._biz.송장정보제거(this.txt송장번호.Text);

                if (result == true)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    else
                        this.txt경고메시지.Text += "자료가 정상적으로 삭제되었습니다." + Environment.NewLine;

                    this.OnToolBarAddButtonClicked(sender, e);        //삭제 후 바로 초기화 시켜준다.
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            #region 의뢰상태확인
            if (Global.MainFrame.LoginInfo.UserID != "S-180" &&
                Global.MainFrame.LoginInfo.UserID != "S-304" &&
                Global.MainFrame.LoginInfo.UserID != "S-587")
			{
                string 의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 == "C")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 수정할 수 없습니다.", this.DD("종결"));
                    else
                        this.txt경고메시지.Text += "종결상태는 수정할 수 없습니다." + Environment.NewLine;

                    return false;
                }

                if (의뢰상태 == "D")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 수정할 수 없습니다.", this.DD("삭제요청"));
                    else
                        this.txt경고메시지.Text += "삭제요청 상태는 수정할 수 없습니다." + Environment.NewLine;

                    return false;
                }

                if (의뢰상태 == "R")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 수정할 수 없습니다.", this.DD("확정"));
                    else
                        this.txt경고메시지.Text += "확정상태는 수정할 수 없습니다." + Environment.NewLine;

                    return false;
                }
            }
            #endregion

            #region 기본정보
            if (this.dtp포장예정일.Text == "" || this.dtp포장예정일.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl포장예정일.Text);
                else
                    this.txt경고메시지.Text += "포장예정일은 필수 입력 항목입니다." + Environment.NewLine;

                this.dtp포장예정일.Focus();
                return false;
            }

            if (D.GetDecimal(this.dtp포장예정일.Text) < D.GetDecimal(this.dtp의뢰일자.Text))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은_보다크거나같아야합니다, new string[] { this.lbl포장예정일.Text, this.lbl의뢰일자.Text });
                else
                    this.txt경고메시지.Text += "포장예정일은 의뢰일자보다 크거나 같아야 합니다." + Environment.NewLine;

                this.dtp포장예정일.Text = this.dtp의뢰일자.Text;
                this._기본정보.CurrentRow["DT_START"] = this.dtp의뢰일자.Text;
                this.dtp포장예정일.Focus();
                return false;
            }

            if (this.ctx호선번호.IsEmpty() || this.ctx호선번호.CodeValue == "" || this.ctx호선번호.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl호선.Text);
                else
                    this.txt경고메시지.Text += "호선은 필수 입력 항목입니다." + Environment.NewLine;

                this.ctx호선번호.Focus();
                return false;
            }

            if (this.ctx매출처S.IsEmpty() || this.ctx매출처S.CodeValue == "" || this.ctx매출처S.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출처S.Text);
                else
                    this.txt경고메시지.Text += "매출처는 필수 입력 항목입니다." + Environment.NewLine;

                this.ctx매출처S.Focus();
                return false;
            }
            
            if (this.ctx포장의뢰자.IsEmpty() || this.ctx포장의뢰자.CodeValue == "" || this.ctx포장의뢰자.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl포장의뢰자.Text);
                else
                    this.txt경고메시지.Text += "포장의뢰자는 필수 입력 항목입니다." + Environment.NewLine;

                this.ctx포장의뢰자.Focus();
                return false;
            }
            
            if (this.dtp의뢰일자.Text == "" || this.dtp의뢰일자.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰일자.Text);
                else
                    this.txt경고메시지.Text += "의뢰일자는 필수 입력 항목 입니다." + Environment.NewLine;

                this.dtp의뢰일자.Focus();
                return false;
            }
            
            if (string.IsNullOrEmpty(D.GetString(this.cboMainCategory.SelectedValue)))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblMainCategory.Text);
                else
                    this.txt경고메시지.Text += "MainCategory는 필수 입력항목 입니다." + Environment.NewLine;

                this.cboMainCategory.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(D.GetString(this.cboSubCategory.SelectedValue)))
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblSubCategory.Text);
                else
                    this.txt경고메시지.Text += "SubCategory는 필수 입력 항목 입니다." + Environment.NewLine;

                this.cboSubCategory.Focus();
                return false;
            }

            if (회사코드 != "S100")
            {
                if (string.IsNullOrEmpty(this.ctx납품처.CodeValue))
                {
                    if (this.cboMainCategory.SelectedValue.ToString() == "002")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl납품처.Text);
                        else
                            this.txt경고메시지.Text += "납품처는 필수 입력항목 입니다." + Environment.NewLine;

                        this.ctx납품처.Focus();
                        return false;
                    }
                }
            }
            #endregion

            #region 송장정보
            if (this.cbo거래구분.SelectedValue == DBNull.Value || this.cbo거래구분.SelectedValue == null || this.cbo거래구분.SelectedValue.ToString() == "" || this.cbo거래구분.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래구분.Text);
                else
                    this.txt경고메시지.Text += "거래구분은 필수 입력항목 입니다." + Environment.NewLine;

                this.cbo거래구분.Focus();
                return false;
            }
            
            if (this.dtp발행일자.Text == "" || this.dtp발행일자.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발행일자.Text);
                else
                    this.txt경고메시지.Text += "발행일자는 필수 입력항목 입니다." + Environment.NewLine;

                this.dtp발행일자.Focus();
                return false;
            }
            
            if (this.cbo출고구분.SelectedValue == DBNull.Value || this.cbo출고구분.SelectedValue == null || this.cbo출고구분.SelectedValue.ToString() == "" || this.cbo출고구분.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl출고구분.Text);
                else
                    this.txt경고메시지.Text += "출고구분은 필수 입력항목 입니다." + Environment.NewLine;

                this.cbo출고구분.Focus();
                return false;
            }
            
            if (this.ctx사업장.IsEmpty() || this.ctx사업장.CodeValue == "" || this.ctx사업장.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl사업장.Text);
                else
                    this.txt경고메시지.Text += "사업장은 필수 입력항목 입니다." + Environment.NewLine;

                this.ctx사업장.Focus();
                return false;
            }
            
            if (this.ctx영업그룹.IsEmpty() || this.ctx영업그룹.CodeValue == "" || this.ctx영업그룹.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl영업그룹.Text);
                else
                    this.txt경고메시지.Text += "영업그룹은 필수 입력항목 입니다." + Environment.NewLine;

                this.ctx영업그룹.Focus();
                return false;
            }
            
            if (this.ctx매출처.CodeValue == "" || this.ctx매출처.CodeValue == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매출처.Text);
                else
                    this.txt경고메시지.Text += "매출처는 필수 입력항목 입니다." + Environment.NewLine;

                this.ctx매출처.Focus();
                return false;
            }
            
            if (this.ctx담당자.IsEmpty() || this.ctx담당자.CodeValue == "" || this.ctx담당자.CodeValue == null)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자.Text);
                else
                    this.txt경고메시지.Text += "담당자는 필수 입력항목 입니다." + Environment.NewLine;

                this.ctx담당자.Focus();
                return false;
            }
            
            if (this.cur외화금액.Text == "" || this.cur외화금액.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl외화금액.Text);
                else
                    this.txt경고메시지.Text += "외화금액은 필수 입력항목 입니다." + Environment.NewLine;

                this.cur외화금액.Focus();
                return false;
            }
            
            if (this.cbo통화.SelectedValue == DBNull.Value || this.cbo통화.SelectedValue == null || this.cbo통화.SelectedValue.ToString() == "" || this.cbo통화.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl통화.Text);
                else
                    this.txt경고메시지.Text += "통화는 필수 입력항목 입니다." + Environment.NewLine;

                this.cbo통화.Focus();
                return false;
            }
            
            if (this.dtp통관예정일.Text == "" || this.dtp통관예정일.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl통관예정일.Text);
                else
                    this.txt경고메시지.Text += "통관예정일은 필수 입력항목 입니다." + Environment.NewLine;

                this.dtp통관예정일.Focus();
                return false;
            }
            
            if (this.dtp선적예정일.Text == "" || this.dtp선적예정일.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선적예정일.Text);
                else
                    this.txt경고메시지.Text += "선적예정일은 필수 입력항목 입니다." + Environment.NewLine;

                this.dtp선적예정일.Focus();
                return false;
            }
            
            if (this.cbo선적조건.SelectedValue == DBNull.Value || this.cbo선적조건.SelectedValue == null || this.cbo선적조건.SelectedValue.ToString() == "" || this.cbo선적조건.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선적조건.Text);
                else
                    this.txt경고메시지.Text += "선적조건은 필수 입력항목 입니다." + Environment.NewLine;

                this.cbo선적조건.Focus();
                return false;
            }
            
            if (this.cbo지불조건.SelectedValue == DBNull.Value || this.cbo지불조건.SelectedValue == null || this.cbo지불조건.SelectedValue.ToString() == "" || this.cbo지불조건.SelectedValue.ToString() == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl지불조건.Text);
                else
                    this.txt경고메시지.Text += "지불조건은 필수 입력항목 입니다." + Environment.NewLine;

                this.cbo지불조건.Focus();
                return false;
            }
            
            if (this.txt선적지.Text == "" || this.txt선적지.Text == string.Empty)
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선적지.Text);
                else
                    this.txt경고메시지.Text += "선적지는 필수 입력항목 입니다." + Environment.NewLine;

                this.txt선적지.Focus();
                return false;
            }
            #endregion

            #region 협조전진행수량 확인
            if (this.cboMainCategory.SelectedValue.ToString() == "001")
            {
                if (this._biz.협조전진행수량체크(this.txt의뢰번호.Text, this._품목정보.DataTable) == false)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("진행중인 협조전이 있습니다.");
                    else
                        this.txt경고메시지.Text += "진행중인 협조전이 있습니다." + Environment.NewLine;

                    return false;
                }
            }
            #endregion

            this.ToolBarSaveButtonEnabled = false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!_품목정보.HasNormalRow) return;

                if (MsgAndSave(PageActionMode.Save))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    else
                        this.txt경고메시지.Text += "자료가 정상적으로 저장되었습니다." + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            DataTable dt기본정보, dt송장정보, dt품목정보;

            if (string.IsNullOrEmpty(this.txt의뢰번호.Text) && string.IsNullOrEmpty(this.txt송장번호.Text))
            {
                #region 추가
                this.txt의뢰번호.Text = (string)this.GetSeq(this.회사코드, "CZ", "03", this.dtp의뢰일자.Text.Substring(2, 4));
                this.txt송장번호.Text = (string)this.GetSeq(this.회사코드, "TRE", "05", this.dtp발행일자.Text.Substring(0, 6));

                if (this._biz.의뢰번호중복체크(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 이(가) 중복되었습니다.", this.txt의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호가 중복되었습니다." + Environment.NewLine;

                    return false;
                }

                if (this._biz.송장번호중복체크(this.txt송장번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 이(가) 중복되었습니다.", this.txt송장번호.Text);
                    else
                        this.txt경고메시지.Text += "송장번호가 중복되었습니다." + Environment.NewLine;

                    return false;
                }

                #region 기본정보
                this._기본정보.CurrentRow["NO_GIR"] = this.txt의뢰번호.Text;
                this._기본정보.CurrentRow["NO_IMO"] = this.ctx호선번호.CodeValue;
                
                this._기본정보.CurrentRow["CD_PARTNER"] = this.ctx매출처S.CodeValue;
                this._기본정보.CurrentRow["TP_BUSI"] = D.GetString(this.cbo거래구분.SelectedValue);
                this._기본정보.CurrentRow["YN_RETURN"] = D.GetString(this.cbo출고구분.SelectedValue);

                this._기본정보.CurrentRow["CD_PACK_CATEGORY"] = D.GetString(this.cboMainCategory.SelectedValue);
                this._기본정보.CurrentRow["CD_SUB_CATEGORY"] = D.GetString(this.cboSubCategory.SelectedValue);

                this._기본정보.CurrentRow["DC_RMK"] = this.txt상세요청.Text;
                this._기본정보.CurrentRow["DC_RMK1"] = this.txt수정취소.Text;
                this._기본정보.CurrentRow["DC_RMK2"] = this.txt매출비고.Text;
                this._기본정보.CurrentRow["DC_RMK3"] = this.txt기포장정보.Text;
                this._기본정보.CurrentRow["DC_RMK4"] = this.txt포장비고.Text;
                this._기본정보.CurrentRow["DC_RMK5"] = this.txtPicking비고.Text;

                this._기본정보.CurrentRow.AcceptChanges();
                this._기본정보.CurrentRow.SetAdded();

                dt기본정보 = this._기본정보.GetChanges();

                if (dt기본정보 != null)
                {
                    if (this._biz.기본정보저장(dt기본정보))
                    {
                        this._기본정보.AcceptChanges();
                    }
                }
                #endregion

                #region 송장정보
                this._송장정보.CurrentRow["NO_INV"] = this.txt송장번호.Text;
                this._송장정보.CurrentRow["TP_TRANS"] = this.cbo선적조건.SelectedValue;
                this._송장정보.CurrentRow["TP_TRANSPORT"] = this.cboINCOTERMS지역.SelectedValue;
                this._송장정보.CurrentRow["COND_PAY"] = this.cbo지불조건.SelectedValue;
                this._송장정보.CurrentRow["CD_EXCH"] = this.cbo통화.SelectedValue;
                this._송장정보.CurrentRow["CD_SALEGRP"] = this.ctx영업그룹.CodeValue;
                this._송장정보.CurrentRow["FG_LC"] = D.GetString(this.cbo거래구분.SelectedValue);
                this._송장정보.CurrentRow["DESCRIPTION"] = this.txt매출처발주번호.Text;
                this._송장정보.CurrentRow["REMARK"] = this.txt송장비고.Text;

                this._송장정보.CurrentRow.AcceptChanges();
                this._송장정보.CurrentRow.SetAdded();

                dt송장정보 = this._송장정보.GetChanges();

                if (dt송장정보 != null)
                {
                    if (this._biz.송장정보저장(dt송장정보))
                    {
                        this._송장정보.AcceptChanges();
                    }
                }
                #endregion

                #region 품목정보
                dt품목정보 = this._품목정보.DataTable.GetChanges();

                if (dt품목정보 != null)
                {
                    if (this._biz.품목정보저장(dt품목정보, this.txt의뢰번호.Text, this.txt송장번호.Text))
                    {
                        this._품목정보.AcceptChanges();
                    }
                }
                #endregion
                #endregion
            }
            else
            {
                #region 수정
                if (!this._biz.의뢰번호중복체크(this.txt의뢰번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt의뢰번호.Text);
                    else
                        this.txt경고메시지.Text += "의뢰번호로 등록된 자료가 없습니다." + Environment.NewLine;

                    return false;
                }

                if (!this._biz.송장번호중복체크(this.txt송장번호.Text))
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ (으)로 등록된 자료가 없습니다.", this.txt송장번호.Text);
                    else
                        this.txt경고메시지.Text += "송장번호로 등록된 자료가 없습니다." + Environment.NewLine;

                    return false;
                }

                #region 기본정보
                dt기본정보 = this._기본정보.GetChanges();

                if (dt기본정보 != null)
                {
                    if (this._biz.기본정보저장(dt기본정보))
                    {
                        this._기본정보.AcceptChanges();
                    }
                }
                #endregion

                #region 송장정보
                dt송장정보 = this._송장정보.GetChanges();

                if (dt송장정보 != null)
                {
                    if (this._biz.송장정보저장(dt송장정보))
                    {
                        this._송장정보.AcceptChanges();
                    }
                }
                #endregion

                #region 품목정보
                dt품목정보 = this._품목정보.DataTable.GetChanges();

                if (dt품목정보 != null)
                {
                    if (this._biz.품목정보저장(dt품목정보, this.txt의뢰번호.Text, this.txt송장번호.Text))
                    {
                        this._품목정보.AcceptChanges();
                    }
                }
                #endregion
                #endregion
            }

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt1, dt2, dt3;
            string 매출처발주번호, url;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                #region 협조전헤더
                dt1 = _biz.협조전헤더(new object[] { Global.MainFrame.ServerKey,
                                                     this.회사코드,
                                                     Global.MainFrame.LoginInfo.Language,
                                                     this.txt의뢰번호.Text });
                #endregion

                #region 협조전라인
                dt2 = _biz.협조전라인(new object[] { this.회사코드,
                                                     this.txt의뢰번호.Text,
                                                     'Y' });
                #endregion

                매출처발주번호 = string.Empty;

                dt1.Columns.Add("CD_GIR_QR");
                dt1.Columns.Add("CD_QR");

                foreach (DataRow dr in dt1.Rows)
                {
                    dr["CD_QR"] = dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString();

                    url = URL.GetShortner("log/pack/write", this.회사코드 + "/" + dr["NO_GIR"].ToString());

                    dr["CD_GIR_QR"] = "V01/D08" + dr["NO_GIR"].ToString() + "/D10 " + url;
                }

                foreach (DataRow dr in ComFunc.getGridGroupBy(dt2.Select(), new string[] { "NO_PO_PARTNER" }, true).Rows)
                {
                    if (!string.IsNullOrEmpty(D.GetString(dr["NO_PO_PARTNER"])))
                        매출처발주번호 += D.GetString(dr["NO_PO_PARTNER"]) + ",";
                }

                dt1.Rows[0]["NO_PO_PARTNER_ALL"] = (string.IsNullOrEmpty(매출처발주번호) == true ? string.Empty : 매출처발주번호.Remove(매출처발주번호.Length - 1));
                dt1.AcceptChanges();

                dt2.Columns.Remove("NM_SUBJECT");

                #region 컷오프타임
                dt3 = DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", new object[] { this.회사코드, string.Empty, "Y" });
                #endregion

                reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH", "물류업무협조전", this.회사코드, dt1, dt2);

                reportHelper.SetDataTable(dt1, 1);
                reportHelper.SetDataTable(dt2, 2);
                reportHelper.SetDataTable(dt3, 3);

                Util.RPT_Print(reportHelper);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
            this.임시파일제거();
            return base.OnToolBarExitButtonClicked(sender, e);
		}
		#endregion

		#region ♣ 화면내버튼 클릭
		private void btn삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._품목정보.HasNormalRow) return;
                
                dataRowArray = this._품목정보.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    else
                        this.txt경고메시지.Text += "선택된 자료가 없습니다." + Environment.NewLine;

                    return;
                }
                else
                {
                    this._품목정보.Redraw = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }

                    this._품목정보.Redraw = true;
                    this._품목정보.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._품목정보.Redraw = true;
            }
        }

        private void btn제출_Click(object sender, EventArgs e)
        {
            SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

            string 의뢰상태, query;
            DataTable dt;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                #region 자동메일발송설정
                if (this.회사코드 == "K100" &&
                    this.cboMainCategory.SelectedValue.ToString() == "001" &&
                    this.cboSubCategory.SelectedValue.ToString() != "007")
                {
                    query = @"SELECT (CASE WHEN ISNULL(ST.TP_SEND, '001') = '001' AND ISNULL(PR.QT_PTR, 0) = 0 AND ISNULL(MV.QT_PACK, 0) = 0 THEN 'Y' ELSE 'N' END) AS YN_PTR,
       (CASE WHEN ISNULL(ST.TP_SEND, '001') = '002' AND ISNULL(ST.TP_TRANS, '') = '' THEN 'Y' ELSE 'N' END) AS YN_TRANS,
       (CASE WHEN ISNULL(ST.TP_SEND, '001') = '002' AND ISNULL(ST.DC_CONSIGNEE, '') = '' THEN 'Y' ELSE 'N' END) AS YN_CONSIGNEE,
       (CASE WHEN ST.YN_DHL = 'Y' AND ISNULL(ST.CD_COUNTRY_DHL, '') = '' THEN 'Y' ELSE 'N' END) AS YN_DHL,
       (CASE WHEN ST.YN_FEDEX = 'Y' AND ISNULL(ST.CD_COUNTRY_FEDEX, '') = '' THEN 'Y' ELSE 'N' END) AS YN_FEDEX,
       (CASE WHEN ST.YN_TPO = 'Y' AND ISNULL(ST.DC_COUNTRY_ETC, '') = '' THEN 'Y' ELSE 'N' END) AS YN_TPO,
       (CASE WHEN ST.YN_SK = 'Y' AND ISNULL(ST.DC_COUNTRY_ETC, '') = '' THEN 'Y' ELSE 'N' END) AS YN_SK,
       (CASE WHEN ST.YN_SR = 'Y' AND ISNULL(ST.DC_COUNTRY_ETC, '') = '' THEN 'Y' ELSE 'N' END) AS YN_SR,
       (CASE WHEN ST.YN_ETC = 'Y' AND (ISNULL(ST.CD_PARTNER_ETC, '') = '' OR ISNULL(ST.DC_EMAIL_ETC, '') = '' OR ISNULL(ST.DC_COUNTRY_ETC, '') = '') THEN 'Y' ELSE 'N' END) AS YN_ETC
FROM CZ_SA_GIRH_PACK GH WITH(NOLOCK)
LEFT JOIN CZ_SA_GIRH_PACK_DETAIL PD WITH(NOLOCK) ON PD.CD_COMPANY = GH.CD_COMPANY AND PD.NO_GIR = GH.NO_GIR
LEFT JOIN CZ_SA_GIR_AUTO_MAIL_SETTING ST WITH(NOLOCK) ON ST.CD_COMPANY = GH.CD_COMPANY AND ST.NO_GIR = GH.NO_GIR
LEFT JOIN (SELECT PR.CD_COMPANY, PR.NO_GIR,
                  COUNT(1) AS QT_PTR
           FROM CZ_SA_GIR_AUTO_MAIL_PTR PR WITH(NOLOCK)
           GROUP BY PR.CD_COMPANY, PR.NO_GIR) PR
ON PR.CD_COMPANY = GH.CD_COMPANY AND PR.NO_GIR = GH.NO_GIR
LEFT JOIN (SELECT MV.CD_COMPANY, MV.CD_PARTNER, MV.NO_IMO,
                  COUNT(1) AS QT_PACK
           FROM CZ_SA_AUTO_MAIL_VESSEL MV WITH(NOLOCK)
           WHERE ISNULL(MV.YN_PACK, 'N') = 'Y'
           GROUP BY MV.CD_COMPANY, MV.CD_PARTNER, MV.NO_IMO) MV
ON MV.CD_COMPANY = GH.CD_COMPANY AND MV.CD_PARTNER = GH.CD_PARTNER AND MV.NO_IMO = PD.NO_IMO
WHERE GH.CD_COMPANY = '{0}'
AND GH.NO_GIR = '{1}'";

                    dt = DBHelper.GetDataTable(string.Format(query, new object[] { this.회사코드, this.txt의뢰번호.Text }));

                    if (dt.Rows[0]["YN_PTR"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("매출처 담당자가 지정되어 있지 않습니다.\n자동메일발송설정 -> 매출처 담당자 지정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "매출처 담당자가 지정되어 있지 않습니다.\n자동메일발송설정 -> 매출처 담당자 지정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_TRANS"].ToString() == "Y")
					{
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("운송구분이 지정되어 있지 않습니다.\n자동메일발송설정 -> 운송구분을 지정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "운송구분이 지정되어 있지 않습니다.\n자동메일발송설정 -> 운송구분을 지정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_CONSIGNEE"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("수하인이 지정되어 있지 않습니다.\n자동메일발송설정 -> 수하인을 지정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "수하인이 지정되어 있지 않습니다.\n자동메일발송설정 -> 수하인을 지정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_DHL"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("DHL 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> DHL 발송 정보를 설정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "DHL 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> DHL 발송 정보를 설정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_FEDEX"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("FEDEX 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> FEDEX 발송 정보를 설정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "FEDEX 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> FEDEX 발송 정보를 설정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_TPO"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("TPO 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> TPO 발송 정보를 설정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "TPO 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> TPO 발송 정보를 설정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_SK"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("쉥커 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> 쉥커 발송 정보를 설정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "쉥커 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> 쉥커 발송 정보를 설정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_SR"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("SR 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> SR 발송 정보를 설정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "SR 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> SR 발송 정보를 설정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                    else if (dt.Rows[0]["YN_ETC"].ToString() == "Y")
                    {
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("기타 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> 기타 발송 정보를 설정하시기 바랍니다.");
                        else
                            this.txt경고메시지.Text += "기타 발송 정보가 지정되어 있지 않습니다.\n자동메일발송설정 -> 기타 발송 정보를 설정하시기 바랍니다." + Environment.NewLine;

                        this.btn자동메일발송설정.Focus();
                        return;
                    }
                }
                #endregion

                #region 경고마스터
                dt = new DataTable();
                dt.Columns.Add("NO_FILE");
                dt.Columns.Add("NO_LINE");
                dt.Columns.Add("CD_SUPPLIER");
                dt.Columns.Add("NO_DSP");
                dt.Columns.Add("NM_SUBJECT");
                dt.Columns.Add("CD_ITEM_PARTNER");
                dt.Columns.Add("NM_ITEM_PARTNER");
                dt.Columns.Add("CD_ITEM");
                dt.Columns.Add("UM_PU");
                dt.Columns.Add("UM_SA");

                DataRow newRow;

                foreach (DataRow dr in this._품목정보.DataTable.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows))
                {
                    string[] supplierArray = dr["CD_SUPPLIER"].ToString().Split(',');

                    if (supplierArray.Length == 1)
                    {
                        newRow = dt.NewRow();
                        newRow["NO_FILE"] = this.txt의뢰번호.Text;
                        newRow["NO_LINE"] = dr["SEQ_GIR"];
                        newRow["CD_SUPPLIER"] = supplierArray[0];
                        newRow["NO_DSP"] = dr["NO_DSP"];
                        newRow["NM_SUBJECT"] = dr["NM_SUBJECT"];
                        newRow["CD_ITEM_PARTNER"] = dr["CD_ITEM_PARTNER"];
                        newRow["NM_ITEM_PARTNER"] = dr["NM_ITEM_PARTNER"];
                        newRow["CD_ITEM"] = dr["CD_ITEM"];
                        dt.Rows.Add(newRow);
                    }
                    else
                    {
                        foreach (string supplier in supplierArray)
                        {
                            newRow = dt.NewRow();
                            newRow["NO_FILE"] = this.txt의뢰번호.Text;
                            newRow["NO_LINE"] = dr["SEQ_GIR"];
                            newRow["CD_SUPPLIER"] = supplier;
                            newRow["NO_DSP"] = dr["NO_DSP"];
                            newRow["NM_SUBJECT"] = dr["NM_SUBJECT"];
                            newRow["CD_ITEM_PARTNER"] = dr["CD_ITEM_PARTNER"];
                            newRow["NM_ITEM_PARTNER"] = dr["NM_ITEM_PARTNER"];
                            newRow["CD_ITEM"] = dr["CD_ITEM"];
                            dt.Rows.Add(newRow);
                        }
                    }
                }

                WARNING warning = new WARNING(WARNING_TARGET.출고의뢰)
                {
                    매출처코드 = this.ctx매출처S.CodeValue,
                    IMO번호 = this.ctx호선번호.CodeValue,
                    아이템 = dt,
                    SQLDebug = sqlDebug
                };

                warning.조회(true);

                if (warning.경고여부)
                {
                    DialogResult 경고결과 = warning.ShowDialog();

                    if (warning.저장불가 || 경고결과 == DialogResult.Cancel)
                    {
                        UTIL.메세지("작업이 취소되었습니다.", "WK1");
                        return;
                    }
                }
                #endregion

                #region 협조전건수 확인
                dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_PACK_COUNT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

                if (dt.Select("IDX = 2 AND QT_TOTAL >= 180").Length > 0)
                {
                    P_CZ_SA_GIR_COUNT dialog = new P_CZ_SA_GIR_COUNT(true);
                    dialog.ShowDialog();

                    if (dialog.DialogResult == DialogResult.Cancel) return;
                }
				#endregion

				#region 미제출 여부 확인
				의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (!string.IsNullOrEmpty(의뢰상태))
				{
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_미제출 건만 제출 할 수 있습니다.");
                    else
                        this.txt경고메시지.Text += "미제출 건만 제출 할 수 있습니다." + Environment.NewLine;

                    return;
                }
                #endregion

                #region 의뢰일자 확인 (제출일자와 동일해야 됨)
                if (this.dtp의뢰일자.Text != this.MainFrameInterface.GetStringToday)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._와_은같아야합니다, new string[] { this.lbl의뢰일자.Text, this.DD("제출일자") });
                    else
                        this.txt경고메시지.Text += "제출일자와 의뢰일자는 같아야 합니다." + Environment.NewLine;

                    this.dtp의뢰일자.Text = this.MainFrameInterface.GetStringToday;
                    _기본정보.CurrentRow["DT_GIR"] = this.dtp의뢰일자.Text;
                    this.dtp의뢰일자.Focus();
                    return;
                }
                #endregion

                // 납품처명 입력
                #region 포장명세서비고 입력
                query = @"UPDATE GD
SET GD.DC_RMK_PL = ISNULL(MD.LN_PARTNER, '')
FROM CZ_SA_GIRH_WORK_DETAIL GD
LEFT JOIN CZ_MA_DELIVERY MD ON MD.CD_COMPANY = GD.CD_COMPANY AND MD.CD_PARTNER = GD.CD_DELIVERY_TO
WHERE GD.CD_COMPANY = '{0}'
AND GD.NO_GIR = '{1}'";

                DBHelper.ExecuteScalar(string.Format(query, this.회사코드, this.txt의뢰번호.Text));
                #endregion

                if (this.SaveData() == true)
                {
                    #region 부대비용 경고 (LIV 관련, 운송비, 포장비 자동청구)
                    if ((this.회사코드 == "K100" || this.회사코드 == "K200") &&
                        this._품목정보.DataTable.Select("CD_ITEM LIKE 'SD%'").Length > 0)
                    {
                        if (this.ShowMessage("부대비용이 포함 되어 있는 건 입니다.\n비용청구 방법 확인 후 진행 하시기 바랍니다.(이중청구 발생가능)\n진행하시겠습니까 ?", "QY2") != DialogResult.Yes)
                            return;
                    }
                    #endregion

                    의뢰상태 = "O";

                    this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                         this.txt의뢰번호.Text,
                                                         "Y",
                                                         의뢰상태,
                                                         "Y",
                                                         this.LoginInfo.UserID });

                    this.수정여부설정(false);
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn제출.Text);
                    else
                        this.txt경고메시지.Text += "제출작업을 완료하였습니다." + Environment.NewLine;

                    #region 지불조건에 따른 경고 팝업
                    if (this.cbo지불조건.SelectedValue.ToString() == "101")
					{
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("CASH IN ADVANCE 조건\n물품대 입금 확인, 송품비 발생여부 확인 철저");
                        else
                            this.txt경고메시지.Text += "CASH IN ADVANCE 조건\n물품대 입금 확인, 송품비 발생여부 확인 철저" + Environment.NewLine;
                    }
                    else if (this.cbo지불조건.SelectedValue.ToString() == "102")
					{
                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("CASH ON DELIVERY 조건\n협조전에 캐쉬수급 리마크 기재");
                        else
                            this.txt경고메시지.Text += "CASH ON DELIVERY 조건\n협조전에 캐쉬수급 리마크 기재" + Environment.NewLine;
                    }
					#endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn제출취소_Click(object sender, EventArgs e)
        {
            string 의뢰상태;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 == "O")
                {
                    this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                         this.txt의뢰번호.Text,
                                                         "Y",
                                                         string.Empty,
                                                         "N",
                                                         this.LoginInfo.UserID });

                    this.수정여부설정(false);

                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn제출취소.Text);
                    else
                        this.txt경고메시지.Text += "제출취소 작업을 완료하였습니다." + Environment.NewLine;
                }
                else
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_제출 건만 제출취소 할 수 있습니다.");
                    else
                        this.txt경고메시지.Text += "제출 건만 제출취소 할 수 있습니다." + Environment.NewLine;

                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제요청_Click(object sender, EventArgs e)
        {
            string 의뢰상태;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 == "C")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태는 삭제요청할 수 없습니다.", this.DD("종결"));
                    else
                        this.txt경고메시지.Text += "종결상태는 삭제요청할 수 없습니다." + Environment.NewLine;

                    return;
                }
                else if (의뢰상태 == "D")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_이미 @ 되었습니다.", this.DD("삭제요청"));
                    else
                        this.txt경고메시지.Text += "이미 삭제요청 되었습니다." + Environment.NewLine;

                    return;
                }
                else if (의뢰상태 == "R")
                {
                    this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                          this.txt의뢰번호.Text,
                                                          "Y",
                                                          "D",
                                                          "N",
                                                          this.LoginInfo.UserID });

                    this.수정여부설정(false);
                    
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn삭제요청.Text);
                    else
                        this.txt경고메시지.Text += "삭제요청 작업을 완료 하였습니다." + Environment.NewLine;
                }
                else
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_확정상태만 수정할 수 있습니다.");
                    else
                        this.txt경고메시지.Text += "확정상태만 수정할 수 있습니다." + Environment.NewLine;

                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제요청취소_Click(object sender, EventArgs e)
        {
            string 의뢰상태;

            try
            {
                if (string.IsNullOrEmpty(this.txt의뢰번호.Text)) return;

                의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                if (의뢰상태 != "D")
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("CZ_@ 상태가 아닙니다.", this.DD("삭제요청"));
                    else
                        this.txt경고메시지.Text += "삭제요청 상태가 아닙니다." + Environment.NewLine;

                    return;
                }

                this._biz.의뢰상태갱신(new object[] { this.회사코드,
                                                      this.txt의뢰번호.Text,
                                                      "Y",
                                                      "R",
                                                      "N",
                                                      this.LoginInfo.UserID });

                this.수정여부설정(false);

                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn삭제요청취소.Text);
                else
                    this.txt경고메시지.Text += "삭제요청취소 작업을 완료하였습니다." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn협조전적용_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_SA_GIR_FORMAT dlg = new P_CZ_SA_GIR_FORMAT(this.ctx매출처S.CodeValue,
                                                                this.ctx매출처S.CodeName,
                                                                this.ctx호선번호.CodeValue,
                                                                this.ctx호선번호.CodeName,
                                                                this.txt호선명.Text,
                                                                true);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    #region 기본정보
                    if (string.IsNullOrEmpty(this.ctx매출처.CodeValue))
                    {
                        this.ctx매출처S.CodeValue = D.GetString(dlg.적용데이터["CD_PARTNER"]);
                        this._기본정보.CurrentRow["CD_PARTNER"] = this.ctx매출처S.CodeValue;

                        this.ctx매출처S.CodeName = D.GetString(dlg.적용데이터["LN_PARTNER"]);
                        this._기본정보.CurrentRow["LN_PARTNER"] = this.ctx매출처S.CodeName;
                    }

                    if (string.IsNullOrEmpty(this.ctx호선번호.CodeValue))
                    {
                        this.ctx호선번호.CodeValue = D.GetString(dlg.적용데이터["NO_IMO"]);
                        this._기본정보.CurrentRow["NO_IMO"] = this.ctx호선번호.CodeValue;

                        this.ctx호선번호.CodeName = D.GetString(dlg.적용데이터["NO_HULL"]);
                        this._기본정보.CurrentRow["NO_HULL"] = this.ctx호선번호.CodeName;

                        this.txt호선명.Text = D.GetString(dlg.적용데이터["NM_VESSEL"]);
                    }

                    this.cboMainCategory.SelectedValue = D.GetString(dlg.적용데이터["CD_MAIN_CATEGORY"]);
                    this._기본정보.CurrentRow["CD_PACK_CATEGORY"] = this.cboMainCategory.SelectedValue;
                    SelectionChangeCommitted(this.cboMainCategory, null);

                    this.cboSubCategory.SelectedValue = D.GetString(dlg.적용데이터["CD_SUB_CATEGORY"]);
                    this._기본정보.CurrentRow["CD_SUB_CATEGORY"] = this.cboSubCategory.SelectedValue;
                    SelectionChangeCommitted(this.cboSubCategory, null);

                    this.납품처적용(D.GetString(dlg.적용데이터["CD_DELIVERY_TO"]), D.GetString(dlg.적용데이터["NM_DELIVERY_TO"]), D.GetString(dlg.적용데이터["DC_DELIVERY_ADDR"]));
                    #endregion
                    
                    #region 송장정보
                    if (dlg.송장정보포함 == true)
                    {
                        if (this._송장정보.CurrentRow.RowState != DataRowState.Unchanged)
                        {
                            if (this.ShowMessage("송장데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                                return;
                        }

                        #region 수하인
                        this.ctx수하인.CodeValue = D.GetString(dlg.적용데이터["CD_CONSIGNEE"]);
                        this._송장정보.CurrentRow["CD_CONSIGNEE"] = this.ctx수하인.CodeValue;
                        this.ctx수하인.CodeName = D.GetString(dlg.적용데이터["NM_CONSIGNEE"]);
                        this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.ctx수하인.CodeName;
                        this.txt수하인주소1.Text = D.GetString(dlg.적용데이터["ADDR1_CONSIGNEE"]);
                        this._송장정보.CurrentRow["ADDR1_CONSIGNEE"] = this.txt수하인주소1.Text;
                        this.txt수하인주소2.Text = D.GetString(dlg.적용데이터["ADDR2_CONSIGNEE"]);
                        this._송장정보.CurrentRow["ADDR2_CONSIGNEE"] = this.txt수하인주소2.Text;
                        #endregion

                        this.cboHSCode.SelectedValue = D.GetString(dlg.적용데이터["CD_PRODUCT"]);
                        this._송장정보.CurrentRow["CD_PRODUCT"] = this.cboHSCode.SelectedValue;

                        this.txt도착도시.Text = D.GetString(dlg.적용데이터["PORT_ARRIVER"]);
                        this._송장정보.CurrentRow["PORT_ARRIVER"] = this.txt도착도시.Text;

                        this.ctx도착국가.CodeValue = D.GetString(dlg.적용데이터["ARRIVER_COUNTRY"]);
                        this._송장정보.CurrentRow["ARRIVER_COUNTRY"] = this.ctx도착국가.CodeValue;
                        this.ctx도착국가.CodeName = D.GetString(dlg.적용데이터["NM_ARRIVER_COUNTRY"]);
                        this._송장정보.CurrentRow["NM_ARRIVER_COUNTRY"] = this.ctx도착국가.CodeName;

                        this.txt전화번호.Text = D.GetString(dlg.적용데이터["REMARK1"]);
                        this._송장정보.CurrentRow["REMARK1"] = this.txt전화번호.Text;
                        this.txt이메일.Text = D.GetString(dlg.적용데이터["REMARK2"]);
                        this._송장정보.CurrentRow["REMARK2"] = this.txt이메일.Text;
                        this.txt받는사람.Text = D.GetString(dlg.적용데이터["REMARK3"]);
                        this._송장정보.CurrentRow["REMARK3"] = this.txt받는사람.Text;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn수주적용_Click(object sender, EventArgs e)
        {
            string 매출처발주번호, filter, noLocation, noStockLocation, dtIn, 포장비고, query, message;
            decimal i, qtReq, qtReqStock, qtReqIm, amGir, amGirAmt, amVat, amt;
            int index;
            DataTable dt, dt수주아이템, dt기포장;
            DataRow newRow, tmpRow;
            DataRow[] dataRowArray, 수주RowArray;

            try
            {
                if (!Chk호선번호 || !Chk매출처) return;

                #region 제재호선 확인
                query = string.Format(@"SELECT * 
                                        FROM CZ_MA_CODEDTL MC
				                        WHERE MC.CD_COMPANY = '{0}'
				                        AND MC.CD_FIELD = 'CZ_SA00062'
				                        AND MC.CD_SYSDEF = '{1}'
                                        AND MC.YN_USE = 'Y'", Global.MainFrame.LoginInfo.CompanyCode, this.ctx호선번호.CodeValue);

                dt = DBHelper.GetDataTable(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                        this.ShowMessage("협조전작성 불가한 호선 입니다.");
                    else
                        this.txt경고메시지.Text += "협조전작성 불가한 호선 입니다." + Environment.NewLine;

                    return;
                }
                #endregion

                #region 동일 호선으로 진행중인 건 확인
                query = string.Format(@"SELECT (GH.NO_GIR + ' / ' + GH.DT_GIR + ' / ' + MC.NM_SYSDEF + ' / ' + MC1.NM_SYSDEF) AS MESSAGE
                                        FROM SA_GIRH GH WITH(NOLOCK)
                                        JOIN CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
                                        LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00006' AND MC.CD_SYSDEF = WD.CD_MAIN_CATEGORY
                                        LEFT JOIN MA_CODEDTL MC1 WITH(NOLOCK) ON MC1.CD_COMPANY = WD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = WD.CD_SUB_CATEGORY
                                        WHERE GH.CD_COMPANY = '{0}'
                                        AND GH.STA_GIR = 'R'
                                        AND WD.NO_IMO = '{1}'
                                        UNION ALL
                                        SELECT (GH.NO_GIR + ' / ' + GH.DT_GIR + ' / ' + MC.NM_SYSDEF + ' / ' + MC1.NM_SYSDEF) AS MESSAGE
                                        FROM CZ_SA_GIRH_PACK GH WITH(NOLOCK)
                                        JOIN CZ_SA_GIRH_PACK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
                                        LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = WD.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00020' AND MC.CD_SYSDEF = WD.CD_PACK_CATEGORY
                                        LEFT JOIN MA_CODEDTL MC1 WITH(NOLOCK) ON MC1.CD_COMPANY = WD.CD_COMPANY AND MC1.CD_FIELD = MC.CD_FLAG1 AND MC1.CD_SYSDEF = WD.CD_SUB_CATEGORY
                                        WHERE GH.CD_COMPANY = '{0}'
                                        AND GH.STA_GIR = 'R'
                                        AND WD.NO_IMO = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, this.ctx호선번호.CodeValue);

                dt = DBHelper.GetDataTable(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    message = string.Empty;

                    foreach (DataRow dr in dt.Rows)
                    {
                        message += dr["MESSAGE"] + Environment.NewLine;
                    }

                    if (this.ShowMessage("동일한 호선으로 진행 중인 건이 " + dt.Rows.Count + "건 있습니다." + Environment.NewLine + Environment.NewLine +
                                         message + Environment.NewLine +
                                         "진행하시겠습니까 ?", "QY2") != DialogResult.Yes)
                        return;
                }
                #endregion

                P_CZ_SA_GIR_SUB dlg = new P_CZ_SA_GIR_SUB(this.ctx매출처S.CodeValue,
                                                          this.ctx매출처S.CodeName,
                                                          this.ctx호선번호.CodeValue,
                                                          this.ctx호선번호.CodeName,
                                                          this.txt호선명.Text,
                                                          true,
                                                          this._품목정보.DataTable);

                if (dlg.ShowDialog() != DialogResult.OK) return;

                수주RowArray = dlg.수주데이터;
                dt수주아이템 = dlg.수주아이템데이터;

                if (this.ctx매출처.ReadOnly == ReadOnly.None)
                {
                    #region 기본정보
                    this.ctx매출처S.CodeValue = D.GetString(수주RowArray[0]["CD_PARTNER"]);
                    this.ctx매출처S.CodeName = D.GetString(수주RowArray[0]["LN_PARTNER"]);

                    this.ctx호선번호.CodeValue = D.GetString(수주RowArray[0]["NO_IMO"]);
                    this.ctx호선번호.CodeName = D.GetString(수주RowArray[0]["NO_HULL"]);

                    this.txt호선명.Text = D.GetString(수주RowArray[0]["NM_VESSEL"]);

                    this._기본정보.CurrentRow["CD_PLANT"] = D.GetString(dt수주아이템.Rows[0]["CD_PLANT"]);

                    매출처발주번호 = string.Empty;

                    foreach (DataRow dr in 수주RowArray)
                    {
                        if (!string.IsNullOrEmpty(D.GetString(dr["NO_PO_PARTNER"])))
                            매출처발주번호 += D.GetString(dr["NO_PO_PARTNER"]) + ",";
                    }
                    #endregion

                    #region 송장정보
                    this.cbo거래구분.SelectedValue = D.GetString(수주RowArray[0]["TP_BUSI"]);

                    this.ctx영업그룹.CodeValue = D.GetString(수주RowArray[0]["CD_SALEGRP"]);
                    this.ctx영업그룹.CodeName = D.GetString(수주RowArray[0]["NM_SALEGRP"]);

                    this.매출처설정(D.GetString(수주RowArray[0]["CD_PARTNER"]));

                    if (this.회사코드 == "K200")
                        this.수출자설정("09989");
                    else if (this.회사코드 == "S100")
                        this.수출자설정("10286");
                    else if (this.회사코드 == "K300")
                        this.수출자설정("99999");
                    else
                        this.수출자설정("00000");

                    this.txt매출처발주번호.Text = (string.IsNullOrEmpty(매출처발주번호) == true ? string.Empty : 매출처발주번호.Remove(매출처발주번호.Length - 1));

                    this.cbo선적조건.SelectedValue = D.GetString(수주RowArray[0]["TP_TRANS"]);
                    this.cboINCOTERMS지역.SelectedValue = D.GetString(수주RowArray[0]["TP_TRANSPORT"]);
                    this.cbo지불조건.SelectedValue = D.GetString(수주RowArray[0]["COND_PAY"]);

                    this.cbo통화.SelectedValue = D.GetString(수주RowArray[0]["CD_EXCH"]);
                    this.cur외화금액.DecimalValue = (Decimal)dt수주아이템.Compute("SUM(AM_GIR)", string.Empty);
                    #endregion
                }

                #region 품목정보
                i = D.GetDecimal(this._품목정보.DataTable.Compute("MAX(SEQ_GIR)", string.Empty));

                this._품목정보.Redraw = false;
                dt기포장 = new DataTable();
                dt기포장.Columns.Add("NO_GIR");
                dt기포장.Columns.Add("NO_PACK");
                dt기포장.Columns.Add("NO_FILE");
                dt기포장.Columns.Add("DC_PACK");

                dt = dt수주아이템.DefaultView.ToTable(true, "NO_SO", "SEQ_SO");
                index = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dt.Rows.Count) });

                    filter = "NO_SO = '" + D.GetString(dr["NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(dr["SEQ_SO"]) + "'";

                    dataRowArray = dt수주아이템.Select(filter);

                    if (dataRowArray.Length == 0) continue;

                    tmpRow = dataRowArray[0];

                    qtReq = 0;
                    qtReqStock = 0;
                    qtReqIm = 0;
                    amGir = 0;
                    amGirAmt = 0;
                    amVat = 0;
                    amt = 0;

                    dtIn = string.Empty;
                    noLocation = string.Empty;
                    noStockLocation = string.Empty;

                    foreach (DataRow dr1 in dataRowArray)
                    {
                        qtReq += D.GetDecimal(dr1["QT_REQ"]);
                        qtReqIm += D.GetDecimal(dr1["QT_REQ_IM"]);
                        amGir += D.GetDecimal(dr1["AM_GIR"]);
                        amGirAmt += D.GetDecimal(dr1["AM_GIRAMT"]);
                        amVat += D.GetDecimal(dr1["AM_VAT"]);
                        amt += D.GetDecimal(dr1["AMT"]);

                        if (D.GetDecimal(dtIn) < D.GetDecimal(dr1["DT_IN"]))
                            dtIn = D.GetString(dr1["DT_IN"]);

                        if (D.GetString(dr1["DT_IN"]) == "STOCK")
                        {
                            noStockLocation = D.GetString(dr1["NO_LOCATION"]);
                            qtReqStock += D.GetDecimal(dr1["QT_REQ"]);
                        }
                        else
                        {
                            noLocation = D.GetString(dr1["NO_LOCATION"]);
                        }
                    }

                    DataTable dt1 = this._biz.기포장정보(new object[] { this.회사코드, dr["NO_SO"].ToString(), dr["SEQ_SO"].ToString(), qtReq });

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (dt기포장.Select(string.Format("NO_GIR = '{0}' AND NO_PACK = '{1}'", dr1["NO_GIR"].ToString(),
                                                                                                dr1["NO_PACK"].ToString())).Length == 0)
                            dt기포장.ImportRow(dr1);
                    }

                    #region 품목추가
                    newRow = this._품목정보.DataTable.NewRow();

                    newRow["S"] = "N";
                    newRow["SEQ_GIR"] = ++i;
                    newRow["NO_LINE"] = i;
                    newRow["CD_ITEM"] = tmpRow["CD_ITEM"];
                    newRow["NM_ITEM"] = tmpRow["NM_ITEM"];
                    newRow["NO_DSP"] = tmpRow["NO_DSP"];
                    newRow["NM_SUBJECT"] = tmpRow["NM_SUBJECT"];
                    newRow["CD_ITEM_PARTNER"] = tmpRow["CD_ITEM_PARTNER"];
                    newRow["NM_ITEM_PARTNER"] = tmpRow["NM_ITEM_PARTNER"];
                    newRow["CD_PLANT"] = tmpRow["CD_PLANT"];
                    newRow["TP_ITEM"] = tmpRow["TP_ITEM"];
                    newRow["STND_ITEM"] = tmpRow["STND_ITEM"];
                    newRow["DT_DUEDATE"] = tmpRow["DT_DUEDATE"];
                    newRow["DT_IO"] = dtIn;
                    newRow["DT_REQGI"] = tmpRow["DT_REQGI"];
                    newRow["YN_INSPECT"] = tmpRow["YN_INSPECT"];
                    newRow["CD_SL"] = tmpRow["CD_SL"];
                    newRow["NM_SL"] = tmpRow["NM_SL"];
                    newRow["TP_GI"] = tmpRow["TP_GI"];
                    newRow["NM_GI"] = tmpRow["NM_GI"];
                    newRow["QT_INV"] = tmpRow["QT_INV"];
                    newRow["QT_TAX"] = tmpRow["QT_TAX"];
                    newRow["QT_BL"] = tmpRow["QT_BL"];
                    newRow["CUSTOMS"] = tmpRow["CUSTOMS"];
                    newRow["QT_GIR"] = qtReq;
                    newRow["QT_GIR_STOCK"] = qtReqStock;
                    newRow["QT_GI"] = tmpRow["QT_GI"];
                    newRow["CD_EXCH"] = tmpRow["CD_EXCH"];
                    newRow["UM"] = tmpRow["UM"];
                    newRow["AM_GIR"] = amGir;
                    newRow["AM_GIRAMT"] = amGirAmt;
                    newRow["AM_VAT"] = amVat;
                    newRow["AMT"] = amt;
                    newRow["UNIT"] = tmpRow["UNIT"];
                    newRow["QT_GIR_IM"] = qtReqIm;
                    newRow["GI_PARTNER"] = tmpRow["GI_PARTNER"];
                    newRow["LN_PARTNER"] = tmpRow["NM_GI_PARTNER"];
                    newRow["NO_PROJECT"] = tmpRow["NO_PROJECT"];
                    newRow["NM_PROJECT"] = tmpRow["NM_PROJECT"];
                    newRow["RT_EXCH"] = tmpRow["RT_EXCH"];
                    newRow["RT_VAT"] = tmpRow["RT_VAT"];
                    newRow["NO_SO"] = tmpRow["NO_SO"];
                    newRow["SEQ_SO"] = tmpRow["SEQ_SO"];
                    newRow["CD_SALEGRP"] = tmpRow["CD_SALEGRP"];
                    newRow["NM_SALEGRP"] = tmpRow["NM_SALEGRP"];
                    newRow["TP_VAT"] = tmpRow["TP_VAT"];
                    newRow["NO_EMP"] = tmpRow["NO_EMP"];
                    newRow["TP_IV"] = tmpRow["TP_IV"];
                    newRow["FG_TAXP"] = tmpRow["FG_TAXP"];
                    newRow["TP_BUSI"] = tmpRow["TP_BUSI"];
                    newRow["NO_LC"] = string.Empty;
                    newRow["SEQ_LC"] = 0.0;
                    newRow["FG_LC_OPEN"] = string.Empty;
                    newRow["DC_RMK"] = tmpRow["DC_RMK"];
                    newRow["NO_PO_PARTNER"] = tmpRow["NO_PO_PARTNER"];
                    newRow["NO_POLINE_PARTNER"] = tmpRow["NO_POLINE_PARTNER"];
                    newRow["GIR"] = tmpRow["GIR"];
                    newRow["IV"] = tmpRow["IV"];
                    newRow["CD_WH"] = tmpRow["CD_WH"];
                    newRow["NO_LOCATION"] = noLocation;
                    newRow["NO_LOCATION_STOCK"] = noStockLocation;
                    newRow["SEQ_PROJECT"] = tmpRow["SEQ_PROJECT"];
                    newRow["YN_PICKING"] = tmpRow["YN_PICKING"];
                    newRow["L_CD_USERDEF1"] = string.Empty;
                    newRow["TP_UM_TAX"] = tmpRow["TP_UM_TAX"];
                    newRow["UMVAT_GIR"] = tmpRow["UMVAT_GIR"];
                    newRow["CD_SUPPLIER"] = tmpRow["CD_SUPPLIER"];
                    newRow["NM_SUPPLIER"] = tmpRow["NM_SUPPLIER"];

                    this._품목정보.DataTable.Rows.Add(newRow);
                    #endregion
                }

                if (dt기포장.Rows.Count > 0)
                {
                    포장비고 = string.Empty;

                    foreach(DataRow dr in dt기포장.Rows)
                        포장비고 += "[" + dr["NO_GIR"].ToString() + "-" + dr["NO_PACK"].ToString() + "/" + dr["NO_FILE"].ToString() + "] " + dr["DC_PACK"].ToString() + " ";

                    포장비고 = 포장비고.Trim();

                    this.txt기포장정보.Text = 포장비고;
                    this._기본정보.CurrentRow["DC_RMK3"] = 포장비고;
                }

                this.OnDataChanged(null, null);

                if (this._품목정보.Rows.Count > 0) this.수정여부설정(false);
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._품목정보.SumRefresh();
                this._품목정보.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P11_ID_MENU = this.Name;
                e.HelpParam.P41_CD_FIELD1 = "MA_B000020";
                e.HelpParam.P42_CD_FIELD2 = "US";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx수주번호검색_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P11_ID_MENU = "CZ_SA_GIR_PACK_SUB";
                e.HelpParam.P21_FG_MODULE = "N";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx수주번호검색_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                this.ctx호선번호.CodeValue = D.GetString(e.HelpReturn.Rows[0]["NO_IMO"]);
                this.ctx호선번호.CodeName = D.GetString(e.HelpReturn.Rows[0]["NO_HULL"]);
                this.txt호선명.Text = D.GetString(e.HelpReturn.Rows[0]["NM_VESSEL"]);

                this.ctx매출처S.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                this.ctx매출처S.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 수주번호검색(string 수주번호)
        {
            try
            {
                DataTable dt = Global.MainFrame.FillDataTable(@"SELECT MH.NO_IMO, MH.NO_HULL, MH.NM_VESSEL, MP.CD_PARTNER, MP.LN_PARTNER 
                                                                FROM SA_SOH SH WITH(NOLOCK)
                                                                LEFT JOIN CZ_SA_QTNH QH WITH(NOLOCK) ON QH.CD_COMPANY = SH.CD_COMPANY AND QH.NO_FILE = SH.NO_SO
                                                                LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = QH.NO_IMO
                                                                LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER "
                                                             + "WHERE SH.CD_COMPANY = '" + this.회사코드 + "' AND SH.NO_SO = '" + 수주번호 + "'");

                if (dt.Rows.Count == 1)
                {
                    this.ctx호선번호.CodeValue = D.GetString(dt.Rows[0]["NO_IMO"]);
                    this.ctx호선번호.CodeName = D.GetString(dt.Rows[0]["NO_HULL"]);
                    this.txt호선명.Text = D.GetString(dt.Rows[0]["NM_VESSEL"]);

                    this.ctx매출처S.CodeValue = D.GetString(dt.Rows[0]["CD_PARTNER"]);
                    this.ctx매출처S.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                }
                else
                {
                    this.ctx호선번호.CodeValue = string.Empty;
                    this.ctx호선번호.CodeName = string.Empty;
                    this.txt호선명.Text = string.Empty;

                    this.ctx매출처S.CodeValue = string.Empty;
                    this.ctx매출처S.CodeName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctxDeliveryTo_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    this.납품처적용(helpReturn.CodeValue, helpReturn.CodeName, helpReturn.Rows[0]["DC_DELIVERY_ADDR"].ToString());
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void 납품처적용(string 납품처코드, string 납품처이름, string 납품처주소)
        {
            try
            {
                this.ctx납품처.CodeValue = 납품처코드;
                this.ctx납품처.CodeName = 납품처이름;
                this.txt납품처주소.Text = 납품처주소;

                this._기본정보.CurrentRow["CD_COLLECT_FROM"] = 납품처코드;
                this._기본정보.CurrentRow["LN_COLLECT_FROM"] = 납품처이름;
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx매출처_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    this.매출처설정(e.HelpReturn.CodeValue);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void 매출처설정(string codeValue)
        {
            DataTable dt;

            try
            {
                this.ctx매출처.CodeValue = codeValue;
                this._송장정보.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                
                dt = Global.MainFrame.FillDataTable(@"SELECT LN_PARTNER, 
                                                             DC_ADS1_H,
                                                             DC_ADS1_D 
                                                      FROM MA_PARTNER WITH(NOLOCK) 
                                                      WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                    @"AND CD_PARTNER = '" + this.ctx매출처.CodeValue + "'");

                this.ctx매출처.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this.txt매출처주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this.txt매출처주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                this._송장정보.CurrentRow["NM_PARTNER"] = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this._송장정보.CurrentRow["ADDR1_PARTNER"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this._송장정보.CurrentRow["ADDR2_PARTNER"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx수출자_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    this.수출자설정(e.HelpReturn.CodeValue);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void 수출자설정(string codeValue)
        {
            DataTable dt;

            try
            {
                this.ctx수출자.CodeValue = codeValue;
                this._송장정보.CurrentRow["CD_EXPORT"] = this.ctx수출자.CodeValue;
                
                dt = Global.MainFrame.FillDataTable(@"SELECT LN_PARTNER, 
                                                             DC_ADS1_H,
                                                             DC_ADS1_D 
                                                      FROM MA_PARTNER WITH(NOLOCK) 
                                                      WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                    @"AND CD_PARTNER = '" + this.ctx수출자.CodeValue + "'");

                this.ctx수출자.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this.txt수출자주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this.txt수출자주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                this._송장정보.CurrentRow["NM_EXPORT"] = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                this._송장정보.CurrentRow["ADDR1_EXPORT"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                this._송장정보.CurrentRow["ADDR2_EXPORT"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx수하인_QueryAfter(object sender, BpQueryArgs e)
        {
            DataTable dt;

            try
            {
                if (e.HelpReturn != null)
                {
                    this.ctx수하인.CodeValue = e.HelpReturn.CodeValue;
                    this.ctx수하인.CodeName = e.HelpReturn.CodeName;
                    this.txt수하인.Text = this.ctx수하인.CodeName;
                    this._송장정보.CurrentRow["CD_CONSIGNEE"] = this.ctx수하인.CodeValue;
                    this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.ctx수하인.CodeName;

                    dt = Global.MainFrame.FillDataTable(@"SELECT DC_ADS1_H, 
                                                                 DC_ADS1_D 
                                                          FROM MA_PARTNER WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                        @"AND CD_PARTNER = '" + this.ctx수하인.CodeValue + "' ");

                    this.txt수하인주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this.txt수하인주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                    this._송장정보.CurrentRow["ADDR1_CONSIGNEE"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this._송장정보.CurrentRow["ADDR2_CONSIGNEE"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx착하통지처_QueryAfter(object sender, BpQueryArgs e)
        {
            DataTable dt;
            try
            {
                if (e.HelpReturn != null)
                {
                    this.ctx착하통지처.CodeValue = e.HelpReturn.CodeValue;
                    this.ctx착하통지처.CodeName = e.HelpReturn.CodeName;
                    this.txt착하통지처.Text = this.ctx착하통지처.CodeName;
                    this._송장정보.CurrentRow["CD_NOTIFY"] = this.ctx착하통지처.CodeValue;
                    this._송장정보.CurrentRow["NM_NOTIFY"] = this.ctx착하통지처.CodeName;

                    dt = Global.MainFrame.FillDataTable(@"SELECT DC_ADS1_H, 
                                                                 DC_ADS1_D 
                                                          FROM MA_PARTNER WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + this.회사코드 + "'" +
                                                        @"AND CD_PARTNER = '" + this.ctx착하통지처.CodeValue + "' ");
                    
                    this.txt착하통지처주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this.txt착하통지처주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                    this._송장정보.CurrentRow["ADDR1_NOTIFY"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    this._송장정보.CurrentRow["ADDR2_NOTIFY"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx호선번호_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.txt호선명.Text = D.GetString(e.HelpReturn.Rows[0]["NM_VESSEL"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx호선번호_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctx호선번호.CodeValue))
                {
                    this.txt호선명.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (this.cboMainCategory.Name == name)
                {
                    this.SetSubCategory(D.GetString(this.cboMainCategory.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt수하인_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txt수하인.Text != this.ctx수하인.CodeName)
                {
                    if (!string.IsNullOrEmpty(this.ctx수하인.CodeValue))
                    {
                        this.ctx수하인.CodeValue = string.Empty;
                        this.ctx수하인.CodeName = string.Empty;
                        this._송장정보.CurrentRow["CD_CONSIGNEE"] = this.ctx수하인.CodeValue;
                        this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.ctx수하인.CodeName;
                    }
                    
                    this._송장정보.CurrentRow["NM_CONSIGNEE"] = this.txt수하인.Text;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt착하통지처_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txt착하통지처.Text != this.ctx착하통지처.CodeName)
                {
                    if (!string.IsNullOrEmpty(this.ctx착하통지처.CodeValue))
                    {
                        this.ctx착하통지처.CodeValue = string.Empty;
                        this.ctx착하통지처.CodeName = string.Empty;
                        this._송장정보.CurrentRow["CD_NOTIFY"] = this.ctx착하통지처.CodeValue;
                        this._송장정보.CurrentRow["NM_NOTIFY"] = this.ctx착하통지처.CodeName;
                    }

                    this._송장정보.CurrentRow["NM_NOTIFY"] = this.txt착하통지처.Text;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _header_ControlValueChanged
        private void PageDataChanged(object sender, EventArgs e)
        {
            try
            {
                bool bChange = _품목정보.GetChanges() != null ? true : false;

                if (bChange)
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                bool bChange = (_기본정보.GetChanges() != null ? true : false) || (_송장정보.GetChanges() != null ? true : false);

                if (bChange)
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 이벤트
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string colname = _품목정보.Cols[e.Col].Name;

                if (colname == "QT_GIR")
                {
                    string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                    string newValue = ((FlexGrid)sender).EditData;

                    if (D.GetDecimal(newValue) <= 0)
                    {
                        _품목정보[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);

                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("CZ_@ 은(는) 0보다 커야합니다.", this.DD("의뢰수량"));
                        else
                            this.txt경고메시지.Text += "의뢰수량은 0보다 커야합니다." + Environment.NewLine;
                        
                        e.Cancel = true;
                        return;
                    }

                    decimal 출고수량 = D.GetDecimal(_품목정보["QT_GI"]);
                    if (출고수량 > 0 && D.GetDecimal(newValue) < 출고수량)
                    {
                        _품목정보[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);

                        if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                            this.ShowMessage("의뢰수량이 출고수량보다 작을 수 없습니다.");
                        else
                            this.txt경고메시지.Text += "의뢰수량이 출고수량보다 작을 수 없습니다." + Environment.NewLine;

                        e.Cancel = true;
                        return;
                    }

                    //의뢰수량체크 프로스져를 호출한다.
                    object[] obj = new object[8];
                    obj[0] = this.회사코드;
                    obj[1] = txt의뢰번호.Text;
                    obj[2] = D.GetDecimal(_품목정보[e.Row, "SEQ_GIR"]);
                    obj[3] = _품목정보[e.Row, "NO_SO"].ToString();
                    obj[4] = D.GetDecimal(_품목정보[e.Row, "SEQ_SO"]);
                    obj[5] = _품목정보[e.Row, "NO_LC"].ToString();
                    obj[6] = D.GetDecimal(_품목정보[e.Row, "SEQ_LC"]);
                    obj[7] = D.GetDecimal(newValue);

                    // is수주수량초과허용 = false 일 경우 수주(의뢰) 수량이 초과하려는 경우 수주(의뢰) 수량으로 맞춘다.
                    // is수주수량초과허용 = true  일 경우 수주(의뢰) 수량이 초과허용 가능하다.
                    //decimal qtso_AddAllow = D.GetDecimal(_flex[e.Row, "QT_MINUS"]) + (D.GetDecimal(_flex[e.Row, "QT_MINUS"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));
                    decimal 의뢰한도수량 = D.GetDecimal(_품목정보["QT_SO"]) * D.GetDecimal(_품목정보["RT_PLUS"]) * 0.01M;
                    의뢰한도수량 += D.GetDecimal(_품목정보["QT_SO"]);
                    //D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) + (D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));

                    decimal 분할의뢰수량;
                    bool is입력 = _biz.Check(obj, out 분할의뢰수량);

                    if (!is입력)
                    {
                        bool is허용 = false;
                        if (is수주수량초과허용)
                        {
                            // 범위 허용
                            if (의뢰한도수량 >= D.GetDecimal(newValue) + 분할의뢰수량)
                            {
                                is허용 = true;
                            }
                        }

                        if (!is허용)
                        {
                            _품목정보[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);
                            e.Cancel = true;

                            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                                this.ShowMessage("의뢰수량이 한도수량을 초과하였습니다.");
                            else
                                this.txt경고메시지.Text += "의뢰수량이 한도수량을 초과하였습니다." + Environment.NewLine;
                        }
                    }

                    this.금액계산();
                }
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

                if (grid["DT_IO"].ToString() == "STOCK" || grid["DT_IO"].ToString() == "CHARGE")
                {
                    e.Cancel = true;
                    return;
                }

                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P00_CHILD_MODE = "출고창고";

                        e.Parameter.P61_CODE1 = @"MS.CD_SL AS CODE,
	                                              MS.NM_SL AS NAME";
                        e.Parameter.P62_CODE2 = @"MA_SL MS WITH(NOLOCK)";
                        e.Parameter.P63_CODE3 = "WHERE MS.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                "AND MS.CD_SL IN ('SL01', 'VL01', 'VL02')";
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid flex;

            try
            {
                flex = ((FlexGrid)sender);

                if (e.Row < flex.Rows.Fixed || e.Col < flex.Cols.Fixed) return;
                if (flex.Cols[e.Col].Name != "QT_TAX" && 
                    flex.Cols[e.Col].Name != "QT_BL" &&
                    flex.Cols[e.Col].Name != "CUSTOMS") return;

                CellStyle style = flex.GetCellStyle(e.Row, e.Col);

                if (style == null)
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_BL"]) > 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
                    else
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
                }
                else if (style.Name == "미납부")
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_BL"]) > 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
                }
                else if (style.Name == "납부")
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_BL"]) == 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public void SubMenuClick(object sender, EventArgs e)
        {
            string 아이템코드 = string.Empty;
            string 공장코드 = string.Empty;

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "현재고조회":
                    for (int i = _품목정보.Row - 2; i < _품목정보.Row + 40; i++)
                    {
                        if (i < _품목정보.Rows.Count - 2)
                        {
                            공장코드 = _품목정보.DataTable.Rows[i]["CD_PLANT"].ToString();
                            아이템코드 += _품목정보.DataTable.Rows[i]["CD_ITEM"].ToString() + "|";
                        }
                        else
                            break;
                    }

                    pur.P_PU_STOCK_SUB m_dlg = new pur.P_PU_STOCK_SUB(공장코드, 아이템코드);
                    m_dlg.ShowDialog(this);
                    break;
            }
        }
        #endregion

        #region ♣ 기타
        private void 금액계산()
        {
            decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["QT_GIR"]));
            decimal 면적2M = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["QT_SQUARE"]));
            decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["UM"]));
            decimal 단가2M = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["UM_SQUARE"]));
            decimal 환율 = Unit.환율(DataDictionaryTypes.SA, D.GetDecimal(_품목정보["RT_EXCH"]));

            decimal 총면적 = Unit.수량(DataDictionaryTypes.SA, 의뢰수량 * 면적2M);

            decimal 부가세율 = D.GetDecimal(_품목정보["RT_VAT"]) * 0.01M;
            decimal 외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);
            decimal 원화금액 = Unit.원화금액(DataDictionaryTypes.SA, 외화금액 * 환율);
            decimal 부가세 = Unit.원화금액(DataDictionaryTypes.SA, 원화금액 * 부가세율);
            decimal 합계 = 원화금액 + 부가세;

            _품목정보["SUM_SQUARE"] = 총면적;
            _품목정보["AM_GIR"] = 외화금액;
            _품목정보["AM_GIRAMT"] = 원화금액;
            _품목정보["AM_VAT"] = 부가세;
            _품목정보["AMT"] = 합계;

            decimal 재고단위수량 = D.GetDecimal(_품목정보["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(_품목정보["UNIT_SO_FACT"]);
            _품목정보["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, 재고단위수량 * 의뢰수량);
            this.cur외화금액.DecimalValue = (Decimal)this._품목정보.DataTable.Compute("SUM(AM_GIR)", string.Empty);
        }

        private void SetSubCategory(string mainCategory)
        {
            if (string.IsNullOrEmpty(mainCategory) == false)
            {
                this.cboSubCategory.DataSource = this.GetComboDataCombine("S;" + MA.GetCode("CZ_SA00020").Select("CODE = '" + mainCategory + "'")[0].ItemArray[3].ToString());
            }
            else
                this.cboSubCategory.DataSource = null;

            this.cboSubCategory.DisplayMember = "NAME";
            this.cboSubCategory.ValueMember = "CODE";

            this._기본정보.CurrentRow["CD_SUB_CATEGORY"] = null;
        }

        private void 수정여부설정(bool isEnabled)
        {
            string 의뢰상태;
            
            try
            {
                if ((Global.MainFrame.LoginInfo.UserID != "S-304" && 
                     Global.MainFrame.LoginInfo.UserID != "S-180" &&
                     Global.MainFrame.LoginInfo.UserID != "S-587" &&
                     this.회사코드 != Global.MainFrame.LoginInfo.CompanyCode) || this.차수 > 0)
                {
                    this.SetToolBarButtonState(false, false, false, false, false);

                    this.btn제출.Enabled = false;
                    this.btn삭제요청.Enabled = false;
                    this.btn삭제요청취소.Enabled = false;

                    this.컨트롤활성화(false);
                }
                else
                {
                    의뢰상태 = this._biz.의뢰상태확인(this.txt의뢰번호.Text);

                    if (string.IsNullOrEmpty(의뢰상태))
                        this.btn제출.Enabled = true;
                    else
                        this.btn제출.Enabled = false;

                    if (의뢰상태 == "R")
                        this.btn삭제요청.Enabled = true;
                    else
                        this.btn삭제요청.Enabled = false;

                    if (의뢰상태 == "D")
                        this.btn삭제요청취소.Enabled = true;
                    else
                        this.btn삭제요청취소.Enabled = false;

                    if (Global.MainFrame.LoginInfo.UserID != "S-180" &&
                        Global.MainFrame.LoginInfo.UserID != "S-304" &&
                        Global.MainFrame.LoginInfo.UserID != "S-587" &&
                        (의뢰상태 == "O" || 의뢰상태 == "C" || 의뢰상태 == "D" || 의뢰상태 == "R"))
                        this.컨트롤활성화(false);
                    else
                        this.컨트롤활성화(true);

                    if (isEnabled == true)
                    {
                        this.ctx수주번호.ReadOnly = ReadOnly.None;
                        this.ctx호선번호.ReadOnly = ReadOnly.None;
                        this.ctx매출처S.ReadOnly = ReadOnly.None;

                        this.cbo거래구분.Enabled = true;
                        this.ctx영업그룹.ReadOnly = ReadOnly.None;

                        this.ctx매출처.ReadOnly = ReadOnly.None;
                        this.txt매출처주소1.ReadOnly = false;
                        this.txt매출처주소2.ReadOnly = false;
                    }
                    else
                    {
                        this.ctx수주번호.ReadOnly = ReadOnly.TotalReadOnly;
                        this.ctx호선번호.ReadOnly = ReadOnly.TotalReadOnly;
                        this.ctx매출처S.ReadOnly = ReadOnly.TotalReadOnly;

                        this.cbo거래구분.Enabled = false;
                        this.ctx영업그룹.ReadOnly = ReadOnly.TotalReadOnly;

                        this.ctx매출처.ReadOnly = ReadOnly.TotalReadOnly;
                        this.txt매출처주소1.ReadOnly = true;
                        this.txt매출처주소2.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 컨트롤활성화(bool 활성여부)
        {
            try
            {
                #region 기본정보
                this.ctx포장의뢰자.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx납품처.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);

                this.cboMainCategory.Enabled = 활성여부;
                this.cboSubCategory.Enabled = 활성여부;

                this.txt상세요청.ReadOnly = !활성여부;
                this.txt포장비고.ReadOnly = !활성여부;
                this.txt수정취소.ReadOnly = !활성여부;
                this.txt매출비고.ReadOnly = !활성여부;
                this.txtPicking비고.ReadOnly = !활성여부;
                #endregion

                #region 송장정보
                this.ctx사업장.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx수출자.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx수하인.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx착하통지처.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx담당자.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx원산지.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);
                this.ctx도착국가.ReadOnly = (활성여부 == true ? ReadOnly.None : ReadOnly.TotalReadOnly);

                this.cbo선적조건.Enabled = 활성여부;
                this.cbo지불조건.Enabled = 활성여부;
                this.cbo통화.Enabled = 활성여부;
                this.cboHSCode.Enabled = 활성여부;
                this.dtp출발예정일.Enabled = 활성여부;
                this.dtp선적예정일.Enabled = 활성여부;
                this.dtp통관예정일.Enabled = 활성여부;

                this.txt수출자주소1.ReadOnly = !활성여부;
                this.txt수출자주소2.ReadOnly = !활성여부;
                this.txt수하인.ReadOnly = !활성여부;
                this.txt수하인주소1.ReadOnly = !활성여부;
                this.txt수하인주소2.ReadOnly = !활성여부;
                this.txt착하통지처.ReadOnly = !활성여부;
                this.txt착하통지처주소1.ReadOnly = !활성여부;
                this.txt착하통지처주소2.ReadOnly = !활성여부;
                this.txt선적지.ReadOnly = !활성여부;
                this.txt도착도시.ReadOnly = !활성여부;
                this.txt전화번호.ReadOnly = !활성여부;
                this.txt이메일.ReadOnly = !활성여부;
                this.txt받는사람.ReadOnly = !활성여부;
                this.txt송장비고.ReadOnly = !활성여부;
                this.chk보험유무.Enabled = 활성여부;
                #endregion

                #region 기타
                this.btn수주적용.Enabled = 활성여부;
                this.btn협조전적용.Enabled = 활성여부;
                this._품목정보.Cols["S"].AllowEditing = 활성여부;
                this._품목정보.Cols["YN_ADD_STOCK"].AllowEditing = 활성여부;
                this.btn삭제.Enabled = 활성여부;
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 임시변수초기화()
        {
            this.회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
            this.협조전번호 = string.Empty;
            this.차수 = 0;
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
        #endregion

        #region ♣ 기타 Property
        bool Chk매출처 { get { return !Checker.IsEmpty(this.ctx매출처S, this.DD("매출처")); } }

        bool Chk호선번호 { get { return !Checker.IsEmpty(this.ctx호선번호, this.DD("호선번호")); } }
		#endregion
	}
}