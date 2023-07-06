
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_MA_PITEM_WINTEC
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_PITEM_WINTEC));
			this.img = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.tab = new Duzon.Common.Controls.TabControlExt();
			this.tpg기본 = new System.Windows.Forms.TabPage();
			this.pnl기본 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl14 = new Duzon.Common.BpControls.BpPanelControl();
			this._btnAutoCdItem = new Duzon.Common.Controls.RoundedButton(this.components);
			this.m_lblCdItem = new Duzon.Common.Controls.LabelExt();
			this.txt품목코드 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl15 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt품명 = new Duzon.Common.Controls.GlobalzationSearchBox();
			this.m_lblNmItem = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem7 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl16 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblEnItem = new Duzon.Common.Controls.LabelExt();
			this.txt영문품명 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem8 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl17 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblStndItem = new Duzon.Common.Controls.LabelExt();
			this.txt규격 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem9 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl18 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt6 = new Duzon.Common.Controls.LabelExt();
			this.txt세부규격 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem10 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl19 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblMatItem = new Duzon.Common.Controls.LabelExt();
			this.txt재질 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem11 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl20 = new Duzon.Common.BpControls.BpPanelControl();
			this._btnBarcode = new Duzon.Common.Controls.RoundedButton(this.components);
			this.m_lblBarCode = new Duzon.Common.Controls.LabelExt();
			this.txt바코드 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem12 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl21 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.cbo계정구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl23 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblTpProc = new Duzon.Common.Controls.LabelExt();
			this.cbo조달구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem13 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl22 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblClsItem = new Duzon.Common.Controls.LabelExt();
			this.cbo출하단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.cur출하단위수량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl25 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUnitIm = new Duzon.Common.Controls.LabelExt();
			this.cbo재고단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem14 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl24 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUnitSo = new Duzon.Common.Controls.LabelExt();
			this.cbo수주단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.cur수주단위수량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl27 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUnitPo = new Duzon.Common.Controls.LabelExt();
			this.cbo구매단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.cur구매단위수량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem99 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl26 = new Duzon.Common.BpControls.BpPanelControl();
			this._curUnitMoFact = new Duzon.Common.Controls.CurrencyTextBox();
			this.m_lblUnitMo = new Duzon.Common.Controls.LabelExt();
			this.cbo생산단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl173 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUnitSu = new Duzon.Common.Controls.LabelExt();
			this.cbo외주단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.cur외주단위수량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem15 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl28 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblTpPart = new Duzon.Common.Controls.LabelExt();
			this.cbo내외자구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl29 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblTpItem = new Duzon.Common.Controls.LabelExt();
			this.cbo품목타입 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem16 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl30 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblGrpMfg = new Duzon.Common.Controls.LabelExt();
			this.ctx제품군 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl31 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.ctx품목군 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem17 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl32 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUnitHs = new Duzon.Common.Controls.LabelExt();
			this.cboHS단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl33 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblNoHs = new Duzon.Common.Controls.LabelExt();
			this.txtHS코드 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem18 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl34 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUnitWeight = new Duzon.Common.Controls.LabelExt();
			this.textBoxExt7 = new Duzon.Common.Controls.TextBoxExt();
			this.cbo중량단위 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl35 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblWeight = new Duzon.Common.Controls.LabelExt();
			this.cur중량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem19 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl48 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblNoDesign = new Duzon.Common.Controls.LabelExt();
			this.cbo도면유무 = new Duzon.Common.Controls.DropDownComboBox();
			this.txt도면번호 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem20 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl49 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUrlItem = new Duzon.Common.Controls.LabelExt();
			this.txt품목url = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem21 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl36 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblYnUse = new Duzon.Common.Controls.LabelExt();
			this.cbo사용유무 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl37 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblDtValid = new Duzon.Common.Controls.LabelExt();
			this.dtp유효일 = new Duzon.Common.Controls.DatePicker();
			this.oneGridItem22 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl38 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl등록자 = new Duzon.Common.Controls.LabelExt();
			this.ctx등록자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl39 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl등록일 = new Duzon.Common.Controls.LabelExt();
			this.dtp등록일 = new Duzon.Common.Controls.DatePicker();
			this.oneGridItem23 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl40 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl수정자 = new Duzon.Common.Controls.LabelExt();
			this.ctx수정자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl41 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl수정일 = new Duzon.Common.Controls.LabelExt();
			this.dtp수정일 = new Duzon.Common.Controls.DatePicker();
			this.oneGridItem24 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl42 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl길이 = new Duzon.Common.Controls.LabelExt();
			this.cur길이 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl43 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl폭 = new Duzon.Common.Controls.LabelExt();
			this.cur폭 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem25 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl44 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl매입형태 = new Duzon.Common.Controls.LabelExt();
			this.cbo매입형태 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl45 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl코어코드 = new Duzon.Common.Controls.LabelExt();
			this.bp코어코드 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem26 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl46 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblcc코드 = new Duzon.Common.Controls.LabelExt();
			this.ctxcc = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl47 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl시작일 = new Duzon.Common.Controls.LabelExt();
			this.dtp시작일 = new Duzon.Common.Controls.DatePicker();
			this.oneGridItem101 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl174 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt_기본_비고 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl기본텝비고 = new Duzon.Common.Controls.LabelExt();
			this.tpg자재 = new System.Windows.Forms.TabPage();
			this.pnl자재 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem27 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this._bpCdItemR = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl50 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblCdPurGrp = new Duzon.Common.Controls.LabelExt();
			this.ctx구매그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem28 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl52 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl53 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblQtSstock = new Duzon.Common.Controls.LabelExt();
			this.cur안전재고 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem29 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl54 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.cboATP적용여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt8 = new Duzon.Common.Controls.LabelExt();
			this.cur허용일 = new Duzon.Common.Controls.CurrencyTextBox();
			this.labelExt9 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl55 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblSageLt = new Duzon.Common.Controls.LabelExt();
			this.cur안전LT = new Duzon.Common.Controls.CurrencyTextBox();
			this.m_lblDy1 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem30 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl56 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl57 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgAbc = new Duzon.Common.Controls.LabelExt();
			this.cboABC구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem31 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl58 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl최종재고실사일 = new Duzon.Common.Controls.LabelExt();
			this.dtp최종재고실사일 = new Duzon.Common.Controls.DatePicker();
			this.bpPanelControl59 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblCdSl = new Duzon.Common.Controls.LabelExt();
			this.ctx입고SL = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem32 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl60 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl로케이션 = new Duzon.Common.Controls.LabelExt();
			this.txt로케이션 = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl61 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblCdGiSl = new Duzon.Common.Controls.LabelExt();
			this.ctx출고SL = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem33 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl62 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl표준단위원가 = new Duzon.Common.Controls.LabelExt();
			this.cur표준단위원가 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl63 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblDyImcly = new Duzon.Common.Controls.LabelExt();
			this.cur순환실사주기 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem34 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl64 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt10 = new Duzon.Common.Controls.LabelExt();
			this.dtp표준원가적용일 = new Duzon.Common.Controls.DatePicker();
			this.bpPanelControl65 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgGri = new Duzon.Common.Controls.LabelExt();
			this.cbo불출관리 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem35 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl66 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl과세구분매입 = new Duzon.Common.Controls.LabelExt();
			this.cbo과세구분매입 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl67 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblDyValid = new Duzon.Common.Controls.LabelExt();
			this.cur유효기간 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem36 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl68 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl과세구분매출 = new Duzon.Common.Controls.LabelExt();
			this.cbo과세구분매출 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl69 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblNoModel = new Duzon.Common.Controls.LabelExt();
			this.txt모델코드 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem37 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl71 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl대분류 = new Duzon.Common.Controls.LabelExt();
			this.ctx대분류 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.oneGridItem38 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl77 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl중분류 = new Duzon.Common.Controls.LabelExt();
			this.ctx중분류 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.oneGridItem39 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl75 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx소분류 = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.lbl소분류 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem40 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl72 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_관리자2 = new Duzon.Common.Controls.LabelExt();
			this.ctx관리자2 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl73 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl_관리자1 = new Duzon.Common.Controls.LabelExt();
			this.ctx관리자1 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem103 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl178 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt12 = new Duzon.Common.Controls.LabelExt();
			this.ctxCC코드 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.tpg오더 = new System.Windows.Forms.TabPage();
			this.pnl오더 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem41 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl74 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgSerNo = new Duzon.Common.Controls.LabelExt();
			this.cboLOT관리 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl70 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblTpPo = new Duzon.Common.Controls.LabelExt();
			this.cbo발주방침 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem42 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl76 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblLotSize = new Duzon.Common.Controls.LabelExt();
			this.cur로트사이즈 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl78 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgTracking = new Duzon.Common.Controls.LabelExt();
			this.cbo트래킹 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem43 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl79 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbllotsn동시사용 = new Duzon.Common.Controls.LabelExt();
			this.cbolotsn동시사용 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl80 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblPartner = new Duzon.Common.Controls.LabelExt();
			this.ctx주거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem44 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl82 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblMaker = new Duzon.Common.Controls.LabelExt();
			this.txtMaker = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem45 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl83 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblTpBom = new Duzon.Common.Controls.LabelExt();
			this.cboBOM형태 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl84 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblLtItem = new Duzon.Common.Controls.LabelExt();
			this.cur품목LT = new Duzon.Common.Controls.CurrencyTextBox();
			this.m_lblDay = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem46 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl85 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgLong = new Duzon.Common.Controls.LabelExt();
			this.cbo장단납기구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl86 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblTpManu = new Duzon.Common.Controls.LabelExt();
			this.cbo제조전략 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem47 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl87 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblDyPoq = new Duzon.Common.Controls.LabelExt();
			this.curPOQ기간 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl88 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblClsPo = new Duzon.Common.Controls.LabelExt();
			this.cbo발주정책 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem48 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl89 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblQtMax = new Duzon.Common.Controls.LabelExt();
			this.cur최대수배수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl90 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblQtMin = new Duzon.Common.Controls.LabelExt();
			this.cur최소수배수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem49 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl91 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgFoq = new Duzon.Common.Controls.LabelExt();
			this.cboFOQ오더정리 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl92 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblQtFoq = new Duzon.Common.Controls.LabelExt();
			this.curFAQ수량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem50 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl93 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl표준ST = new Duzon.Common.Controls.LabelExt();
			this.cur표준ST = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl94 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblYnPhanTom = new Duzon.Common.Controls.LabelExt();
			this.cbo팬텀구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem51 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl95 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_cboQtRop = new Duzon.Common.Controls.LabelExt();
			this.cboROP = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl96 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgBf = new Duzon.Common.Controls.LabelExt();
			this.cbo백플러쉬 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem52 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl97 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUpd = new Duzon.Common.Controls.LabelExt();
			this.cur생산능력 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl98 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblUph = new Duzon.Common.Controls.LabelExt();
			this.curUPH = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem53 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl99 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblRtMinus = new Duzon.Common.Controls.LabelExt();
			this.cur과입고허용율음수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl81 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblRtPlus = new Duzon.Common.Controls.LabelExt();
			this.cur과입고허용율양수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem54 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl100 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl101 = new Duzon.Common.BpControls.BpPanelControl();
			this.curNEGO금액 = new Duzon.Common.Controls.CurrencyTextBox();
			this.labelExt11 = new Duzon.Common.Controls.LabelExt();
			this.tpg품질 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.pnl첨부파일 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.btn파일삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txt첨부파일명 = new Duzon.Common.Controls.TextBoxExt();
			this.btn다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.pnl품질 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem55 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl103 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblRtQm = new Duzon.Common.Controls.LabelExt();
			this.cur품목불량율 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl102 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblLtQc = new Duzon.Common.Controls.LabelExt();
			this.cur검사LT = new Duzon.Common.Controls.CurrencyTextBox();
			this.m_lblDay2 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem56 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl104 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgPqc = new Duzon.Common.Controls.LabelExt();
			this.cbo수입검사여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl공정검사 = new Duzon.Common.Controls.LabelExt();
			this.cbo공정검사 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem57 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl105 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblFgMqc = new Duzon.Common.Controls.LabelExt();
			this.cbo출하검사여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.cbo이동검사 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl이동검사 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem58 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl106 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.cbo생산입고검사불량 = new Duzon.Common.Controls.DropDownComboBox();
			this.cbo생산입고검사 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem59 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl107 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo수입검사레벨 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl수입검사레벨 = new Duzon.Common.Controls.LabelExt();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.cbo외주검사 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem60 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl108 = new Duzon.Common.BpControls.BpPanelControl();
			this.m_lblNoStnd = new Duzon.Common.Controls.LabelExt();
			this.txt규격번호 = new Duzon.Common.Controls.TextBoxExt();
			this.tpg기타 = new System.Windows.Forms.TabPage();
			this.pnl기타정보 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem61 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl109 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의1 = new Duzon.Common.Controls.LabelExt();
			this.txt사용자정의1 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem62 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl110 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의2 = new Duzon.Common.Controls.LabelExt();
			this.txt사용자정의2 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem63 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl111 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의3 = new Duzon.Common.Controls.LabelExt();
			this.txt사용자정의3 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem64 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl112 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의4 = new Duzon.Common.Controls.LabelExt();
			this.txt사용자정의4 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem65 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl113 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의5 = new Duzon.Common.Controls.LabelExt();
			this.txt사용자정의5 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem66 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl115 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의7 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의7 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl114 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의6 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의6 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem67 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl116 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의9 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의9 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl117 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의8 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의8 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem68 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl118 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의11 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의11 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl119 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의10 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의10 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem69 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl120 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의13 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의13 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl121 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의12 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의12 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem70 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl122 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의15 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의15 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl123 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의14 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의14 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem71 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl124 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의17 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의17 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl125 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의16 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의16 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem72 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl126 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의19 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의19 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl127 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의18 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의18 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem73 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl128 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의21 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의21 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl129 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의20 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의20 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem74 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl130 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의23 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의23 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl131 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의22 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의22 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem75 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl132 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의25 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의25 = new Duzon.Common.Controls.CurrencyTextBox();
			this.bpPanelControl133 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의24 = new Duzon.Common.Controls.LabelExt();
			this.cur사용자정의24 = new Duzon.Common.Controls.CurrencyTextBox();
			this.oneGridItem76 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl134 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의27 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의27 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl135 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의26 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의26 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem77 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl136 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의29 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의29 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl137 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의28 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의28 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem78 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl138 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의31 = new Duzon.Common.Controls.LabelExt();
			this.bp_사용자정의31 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl139 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의30 = new Duzon.Common.Controls.LabelExt();
			this.bp_사용자정의30 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem79 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl140 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의33 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의33 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl141 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의32 = new Duzon.Common.Controls.LabelExt();
			this.bp_사용자정의32 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem80 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl142 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo사용자정의35 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl사용자정의35 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl143 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용자정의34 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용자정의34 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem98 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl172 = new Duzon.Common.BpControls.BpPanelControl();
			this._datePicker37 = new Duzon.Common.Controls.DatePicker();
			this.lbl사용자정의37 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl171 = new Duzon.Common.BpControls.BpPanelControl();
			this._datePicker36 = new Duzon.Common.Controls.DatePicker();
			this.lbl사용자정의36 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem102 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl177 = new Duzon.Common.BpControls.BpPanelControl();
			this._datePicker39 = new Duzon.Common.Controls.DatePicker();
			this.lbl사용자정의39 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl176 = new Duzon.Common.BpControls.BpPanelControl();
			this._datePicker38 = new Duzon.Common.Controls.DatePicker();
			this.lbl사용자정의38 = new Duzon.Common.Controls.LabelExt();
			this.tpgIMAGE = new System.Windows.Forms.TabPage();
			this.panelExt43 = new Duzon.Common.Controls.PanelExt();
			this.panelExt44 = new Duzon.Common.Controls.PanelExt();
			this.tblImage = new System.Windows.Forms.TableLayoutPanel();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.pixFile = new System.Windows.Forms.PictureBox();
			this.pnlFileList = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.textBoxExt4 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt5 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt6 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt3 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt2 = new Duzon.Common.Controls.TextBoxExt();
			this.textBoxExt1 = new Duzon.Common.Controls.TextBoxExt();
			this.rduFile6View = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFile5View = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFile4View = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileIns6 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileIns4 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileIns5 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileDel6 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileDel4 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileDel5 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txtFile6 = new Duzon.Common.Controls.TextBoxExt();
			this.txtFile5 = new Duzon.Common.Controls.TextBoxExt();
			this.txtFile4 = new Duzon.Common.Controls.TextBoxExt();
			this.rduFile3View = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFile2View = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFile1View = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileIns3 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileIns1 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileIns2 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileDel3 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileDel1 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.rduFileDel2 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txtFile3 = new Duzon.Common.Controls.TextBoxExt();
			this.txtFile2 = new Duzon.Common.Controls.TextBoxExt();
			this.txtFile1 = new Duzon.Common.Controls.TextBoxExt();
			this.tag버전관리 = new System.Windows.Forms.TabPage();
			this.panelExt70 = new Duzon.Common.Controls.PanelExt();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panelExt71 = new Duzon.Common.Controls.PanelExt();
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.panelExt72 = new Duzon.Common.Controls.PanelExt();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.panelExt73 = new Duzon.Common.Controls.PanelExt();
			this._flex버전관리 = new Dass.FlexGrid.FlexGrid(this.components);
			this.pnl버전관리 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.lbl배포일자 = new Duzon.Common.Controls.LabelExt();
			this.lbl_버전관리_비고 = new Duzon.Common.Controls.LabelExt();
			this.lbl최종소스여부 = new Duzon.Common.Controls.LabelExt();
			this.dtp배포일자 = new Duzon.Common.Controls.DatePicker();
			this.lbl난이도 = new Duzon.Common.Controls.LabelExt();
			this.lbl제품사항 = new Duzon.Common.Controls.LabelExt();
			this.lbl담당자부 = new Duzon.Common.Controls.LabelExt();
			this.cbo최종소스여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl담당자정 = new Duzon.Common.Controls.LabelExt();
			this.lbl지원여부 = new Duzon.Common.Controls.LabelExt();
			this.lbl개발자 = new Duzon.Common.Controls.LabelExt();
			this.cbo난이도 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl지원OS = new Duzon.Common.Controls.LabelExt();
			this.bp담당자부 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl타입 = new Duzon.Common.Controls.LabelExt();
			this.bp담당자정 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl언어 = new Duzon.Common.Controls.LabelExt();
			this.bp개발자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl버전 = new Duzon.Common.Controls.LabelExt();
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl순번 = new Duzon.Common.Controls.LabelExt();
			this.txt제품사항 = new Duzon.Common.Controls.TextBoxExt();
			this.cbo지원여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.txt지원OS = new Duzon.Common.Controls.TextBoxExt();
			this.txt타입 = new Duzon.Common.Controls.TextBoxExt();
			this.txt언어 = new Duzon.Common.Controls.TextBoxExt();
			this.txt버전 = new Duzon.Common.Controls.TextBoxExt();
			this.txt순번 = new Duzon.Common.Controls.TextBoxExt();
			this.tpgStndItem = new System.Windows.Forms.TabPage();
			this.pnlStndItem = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem85 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl51 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblStndItem1 = new Duzon.Common.Controls.LabelExt();
			this._txtStndItem01 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem86 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl159 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblStndItem2 = new Duzon.Common.Controls.LabelExt();
			this._txtStndItem02 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem87 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl160 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblStndItem3 = new Duzon.Common.Controls.LabelExt();
			this._txtStndItem03 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem88 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl161 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblStndItem4 = new Duzon.Common.Controls.LabelExt();
			this._txtStndItem04 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem89 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl162 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblStndItem5 = new Duzon.Common.Controls.LabelExt();
			this._txtStndItem05 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem90 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl163 = new Duzon.Common.BpControls.BpPanelControl();
			this._txtNumStndItem01 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lblStndItem6 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem91 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl164 = new Duzon.Common.BpControls.BpPanelControl();
			this._txtNumStndItem02 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lblStndItem7 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem92 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl165 = new Duzon.Common.BpControls.BpPanelControl();
			this._txtNumStndItem03 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lblStndItem8 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem93 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl166 = new Duzon.Common.BpControls.BpPanelControl();
			this._txtNumStndItem04 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lblStndItem9 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem94 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl167 = new Duzon.Common.BpControls.BpPanelControl();
			this._txtNumStndItem05 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lblStndItem10 = new Duzon.Common.Controls.LabelExt();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid2 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem81 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl147 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef09_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef09_S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl146 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef08_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef08_S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl145 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef07_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef07_S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl144 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef06_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef06_S = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem82 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl151 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef04_S = new Duzon.Common.Controls.LabelExt();
			this._txtUserDef04_S = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl150 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef03_S = new Duzon.Common.Controls.LabelExt();
			this._txtUserDef03_S = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl149 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef02_S = new Duzon.Common.Controls.LabelExt();
			this._txtUserDef02_S = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl148 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef01_S = new Duzon.Common.Controls.LabelExt();
			this._txtUserDef01_S = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem83 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl155 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl154 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef32_S = new Duzon.Common.Controls.LabelExt();
			this._ctxUserDef32_S = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl153 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef31_S = new Duzon.Common.Controls.LabelExt();
			this._ctxUserDef31_S = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl152 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef30_S = new Duzon.Common.Controls.LabelExt();
			this._ctxUserDef30_S = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem84 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl158 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef35_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef35_S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl157 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef34_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef34_S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl156 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblUserDef33_S = new Duzon.Common.Controls.LabelExt();
			this._cboUserDef33_S = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem95 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGridItem96 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl조달구분S = new Duzon.Common.Controls.LabelExt();
			this.cbo조달구분S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl계정구분S = new Duzon.Common.Controls.LabelExt();
			this.cbo계정구분S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공장코드S = new Duzon.Common.Controls.LabelExt();
			this.cbo공장코드S = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl품목타입 = new Duzon.Common.Controls.LabelExt();
			this.bpc품목타입S = new Duzon.Common.BpControls.BpComboBox();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl제품군S = new Duzon.Common.Controls.LabelExt();
			this.cbo제품군S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc품목군S = new Duzon.Common.BpControls.BpComboBox();
			this.lbl품목군S = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpcClssS = new Duzon.Common.BpControls.BpComboBox();
			this.lbl소분류S = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpcClsmS = new Duzon.Common.BpControls.BpComboBox();
			this.lbl중분류S = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpcClslS = new Duzon.Common.BpControls.BpComboBox();
			this.lbl대분류S = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl내외자구분S = new Duzon.Common.Controls.LabelExt();
			this.cbo내외자구분S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용유무S = new Duzon.Common.Controls.LabelExt();
			this.cbo사용유무S = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx주거래처S = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl주거래처 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem97 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl175 = new Duzon.Common.BpControls.BpPanelControl();
			this._chkTop = new Duzon.Common.Controls.CheckBoxExt();
			this._chkUpper = new Duzon.Common.Controls.CheckBoxExt();
			this._chkSString = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl검색S = new Duzon.Common.Controls.LabelExt();
			this.txt검색S = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem100 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl168 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt검색2 = new System.Windows.Forms.TextBox();
			this.cboSearch2 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl170 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt검색3 = new System.Windows.Forms.TextBox();
			this.cboSearch3 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl169 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt검색1 = new System.Windows.Forms.TextBox();
			this.cboSearch1 = new Duzon.Common.Controls.DropDownComboBox();
			this.opendlg = new System.Windows.Forms.OpenFileDialog();
			this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn붙여넣기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn품목복사 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn첨부파일 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnPDM적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCopyToCompany = new Duzon.Common.Controls.RoundedButton(this.components);
			this._btnBulkSave = new Duzon.Common.Controls.RoundedButton(this.components);
			this._btnZSATREC_PASS = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.tab.SuspendLayout();
			this.tpg기본.SuspendLayout();
			this.oneGridItem5.SuspendLayout();
			this.bpPanelControl14.SuspendLayout();
			this.oneGridItem6.SuspendLayout();
			this.bpPanelControl15.SuspendLayout();
			this.oneGridItem7.SuspendLayout();
			this.bpPanelControl16.SuspendLayout();
			this.oneGridItem8.SuspendLayout();
			this.bpPanelControl17.SuspendLayout();
			this.oneGridItem9.SuspendLayout();
			this.bpPanelControl18.SuspendLayout();
			this.oneGridItem10.SuspendLayout();
			this.bpPanelControl19.SuspendLayout();
			this.oneGridItem11.SuspendLayout();
			this.bpPanelControl20.SuspendLayout();
			this.oneGridItem12.SuspendLayout();
			this.bpPanelControl21.SuspendLayout();
			this.bpPanelControl23.SuspendLayout();
			this.oneGridItem13.SuspendLayout();
			this.bpPanelControl22.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur출하단위수량)).BeginInit();
			this.bpPanelControl25.SuspendLayout();
			this.oneGridItem14.SuspendLayout();
			this.bpPanelControl24.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur수주단위수량)).BeginInit();
			this.bpPanelControl27.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur구매단위수량)).BeginInit();
			this.oneGridItem99.SuspendLayout();
			this.bpPanelControl26.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._curUnitMoFact)).BeginInit();
			this.bpPanelControl173.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur외주단위수량)).BeginInit();
			this.oneGridItem15.SuspendLayout();
			this.bpPanelControl28.SuspendLayout();
			this.bpPanelControl29.SuspendLayout();
			this.oneGridItem16.SuspendLayout();
			this.bpPanelControl30.SuspendLayout();
			this.bpPanelControl31.SuspendLayout();
			this.oneGridItem17.SuspendLayout();
			this.bpPanelControl32.SuspendLayout();
			this.bpPanelControl33.SuspendLayout();
			this.oneGridItem18.SuspendLayout();
			this.bpPanelControl34.SuspendLayout();
			this.bpPanelControl35.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur중량)).BeginInit();
			this.oneGridItem19.SuspendLayout();
			this.bpPanelControl48.SuspendLayout();
			this.oneGridItem20.SuspendLayout();
			this.bpPanelControl49.SuspendLayout();
			this.oneGridItem21.SuspendLayout();
			this.bpPanelControl36.SuspendLayout();
			this.bpPanelControl37.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp유효일)).BeginInit();
			this.oneGridItem22.SuspendLayout();
			this.bpPanelControl38.SuspendLayout();
			this.bpPanelControl39.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp등록일)).BeginInit();
			this.oneGridItem23.SuspendLayout();
			this.bpPanelControl40.SuspendLayout();
			this.bpPanelControl41.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp수정일)).BeginInit();
			this.oneGridItem24.SuspendLayout();
			this.bpPanelControl42.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur길이)).BeginInit();
			this.bpPanelControl43.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur폭)).BeginInit();
			this.oneGridItem25.SuspendLayout();
			this.bpPanelControl44.SuspendLayout();
			this.bpPanelControl45.SuspendLayout();
			this.oneGridItem26.SuspendLayout();
			this.bpPanelControl46.SuspendLayout();
			this.bpPanelControl47.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp시작일)).BeginInit();
			this.oneGridItem101.SuspendLayout();
			this.bpPanelControl174.SuspendLayout();
			this.tpg자재.SuspendLayout();
			this.oneGridItem27.SuspendLayout();
			this.bpPanelControl50.SuspendLayout();
			this.oneGridItem28.SuspendLayout();
			this.bpPanelControl53.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur안전재고)).BeginInit();
			this.oneGridItem29.SuspendLayout();
			this.bpPanelControl54.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur허용일)).BeginInit();
			this.bpPanelControl55.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur안전LT)).BeginInit();
			this.oneGridItem30.SuspendLayout();
			this.bpPanelControl57.SuspendLayout();
			this.oneGridItem31.SuspendLayout();
			this.bpPanelControl58.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp최종재고실사일)).BeginInit();
			this.bpPanelControl59.SuspendLayout();
			this.oneGridItem32.SuspendLayout();
			this.bpPanelControl60.SuspendLayout();
			this.bpPanelControl61.SuspendLayout();
			this.oneGridItem33.SuspendLayout();
			this.bpPanelControl62.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur표준단위원가)).BeginInit();
			this.bpPanelControl63.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur순환실사주기)).BeginInit();
			this.oneGridItem34.SuspendLayout();
			this.bpPanelControl64.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp표준원가적용일)).BeginInit();
			this.bpPanelControl65.SuspendLayout();
			this.oneGridItem35.SuspendLayout();
			this.bpPanelControl66.SuspendLayout();
			this.bpPanelControl67.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur유효기간)).BeginInit();
			this.oneGridItem36.SuspendLayout();
			this.bpPanelControl68.SuspendLayout();
			this.bpPanelControl69.SuspendLayout();
			this.oneGridItem37.SuspendLayout();
			this.bpPanelControl71.SuspendLayout();
			this.oneGridItem38.SuspendLayout();
			this.bpPanelControl77.SuspendLayout();
			this.oneGridItem39.SuspendLayout();
			this.bpPanelControl75.SuspendLayout();
			this.oneGridItem40.SuspendLayout();
			this.bpPanelControl72.SuspendLayout();
			this.bpPanelControl73.SuspendLayout();
			this.oneGridItem103.SuspendLayout();
			this.bpPanelControl178.SuspendLayout();
			this.tpg오더.SuspendLayout();
			this.oneGridItem41.SuspendLayout();
			this.bpPanelControl74.SuspendLayout();
			this.bpPanelControl70.SuspendLayout();
			this.oneGridItem42.SuspendLayout();
			this.bpPanelControl76.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur로트사이즈)).BeginInit();
			this.bpPanelControl78.SuspendLayout();
			this.oneGridItem43.SuspendLayout();
			this.bpPanelControl79.SuspendLayout();
			this.bpPanelControl80.SuspendLayout();
			this.oneGridItem44.SuspendLayout();
			this.bpPanelControl82.SuspendLayout();
			this.oneGridItem45.SuspendLayout();
			this.bpPanelControl83.SuspendLayout();
			this.bpPanelControl84.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur품목LT)).BeginInit();
			this.oneGridItem46.SuspendLayout();
			this.bpPanelControl85.SuspendLayout();
			this.bpPanelControl86.SuspendLayout();
			this.oneGridItem47.SuspendLayout();
			this.bpPanelControl87.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.curPOQ기간)).BeginInit();
			this.bpPanelControl88.SuspendLayout();
			this.oneGridItem48.SuspendLayout();
			this.bpPanelControl89.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur최대수배수)).BeginInit();
			this.bpPanelControl90.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur최소수배수)).BeginInit();
			this.oneGridItem49.SuspendLayout();
			this.bpPanelControl91.SuspendLayout();
			this.bpPanelControl92.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.curFAQ수량)).BeginInit();
			this.oneGridItem50.SuspendLayout();
			this.bpPanelControl93.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur표준ST)).BeginInit();
			this.bpPanelControl94.SuspendLayout();
			this.oneGridItem51.SuspendLayout();
			this.bpPanelControl95.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboROP)).BeginInit();
			this.bpPanelControl96.SuspendLayout();
			this.oneGridItem52.SuspendLayout();
			this.bpPanelControl97.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur생산능력)).BeginInit();
			this.bpPanelControl98.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.curUPH)).BeginInit();
			this.oneGridItem53.SuspendLayout();
			this.bpPanelControl99.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur과입고허용율음수)).BeginInit();
			this.bpPanelControl81.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur과입고허용율양수)).BeginInit();
			this.oneGridItem54.SuspendLayout();
			this.bpPanelControl101.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.curNEGO금액)).BeginInit();
			this.tpg품질.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.pnl첨부파일.SuspendLayout();
			this.oneGridItem55.SuspendLayout();
			this.bpPanelControl103.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur품목불량율)).BeginInit();
			this.bpPanelControl102.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur검사LT)).BeginInit();
			this.oneGridItem56.SuspendLayout();
			this.bpPanelControl104.SuspendLayout();
			this.oneGridItem57.SuspendLayout();
			this.bpPanelControl105.SuspendLayout();
			this.oneGridItem58.SuspendLayout();
			this.bpPanelControl106.SuspendLayout();
			this.oneGridItem59.SuspendLayout();
			this.bpPanelControl107.SuspendLayout();
			this.oneGridItem60.SuspendLayout();
			this.bpPanelControl108.SuspendLayout();
			this.tpg기타.SuspendLayout();
			this.oneGridItem61.SuspendLayout();
			this.bpPanelControl109.SuspendLayout();
			this.oneGridItem62.SuspendLayout();
			this.bpPanelControl110.SuspendLayout();
			this.oneGridItem63.SuspendLayout();
			this.bpPanelControl111.SuspendLayout();
			this.oneGridItem64.SuspendLayout();
			this.bpPanelControl112.SuspendLayout();
			this.oneGridItem65.SuspendLayout();
			this.bpPanelControl113.SuspendLayout();
			this.oneGridItem66.SuspendLayout();
			this.bpPanelControl115.SuspendLayout();
			this.bpPanelControl114.SuspendLayout();
			this.oneGridItem67.SuspendLayout();
			this.bpPanelControl116.SuspendLayout();
			this.bpPanelControl117.SuspendLayout();
			this.oneGridItem68.SuspendLayout();
			this.bpPanelControl118.SuspendLayout();
			this.bpPanelControl119.SuspendLayout();
			this.oneGridItem69.SuspendLayout();
			this.bpPanelControl120.SuspendLayout();
			this.bpPanelControl121.SuspendLayout();
			this.oneGridItem70.SuspendLayout();
			this.bpPanelControl122.SuspendLayout();
			this.bpPanelControl123.SuspendLayout();
			this.oneGridItem71.SuspendLayout();
			this.bpPanelControl124.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의17)).BeginInit();
			this.bpPanelControl125.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의16)).BeginInit();
			this.oneGridItem72.SuspendLayout();
			this.bpPanelControl126.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의19)).BeginInit();
			this.bpPanelControl127.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의18)).BeginInit();
			this.oneGridItem73.SuspendLayout();
			this.bpPanelControl128.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의21)).BeginInit();
			this.bpPanelControl129.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의20)).BeginInit();
			this.oneGridItem74.SuspendLayout();
			this.bpPanelControl130.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의23)).BeginInit();
			this.bpPanelControl131.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의22)).BeginInit();
			this.oneGridItem75.SuspendLayout();
			this.bpPanelControl132.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의25)).BeginInit();
			this.bpPanelControl133.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의24)).BeginInit();
			this.oneGridItem76.SuspendLayout();
			this.bpPanelControl134.SuspendLayout();
			this.bpPanelControl135.SuspendLayout();
			this.oneGridItem77.SuspendLayout();
			this.bpPanelControl136.SuspendLayout();
			this.bpPanelControl137.SuspendLayout();
			this.oneGridItem78.SuspendLayout();
			this.bpPanelControl138.SuspendLayout();
			this.bpPanelControl139.SuspendLayout();
			this.oneGridItem79.SuspendLayout();
			this.bpPanelControl140.SuspendLayout();
			this.bpPanelControl141.SuspendLayout();
			this.oneGridItem80.SuspendLayout();
			this.bpPanelControl142.SuspendLayout();
			this.bpPanelControl143.SuspendLayout();
			this.oneGridItem98.SuspendLayout();
			this.bpPanelControl172.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._datePicker37)).BeginInit();
			this.bpPanelControl171.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._datePicker36)).BeginInit();
			this.oneGridItem102.SuspendLayout();
			this.bpPanelControl177.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._datePicker39)).BeginInit();
			this.bpPanelControl176.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._datePicker38)).BeginInit();
			this.tpgIMAGE.SuspendLayout();
			this.panelExt43.SuspendLayout();
			this.panelExt44.SuspendLayout();
			this.tblImage.SuspendLayout();
			this.imagePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pixFile)).BeginInit();
			this.pnlFileList.SuspendLayout();
			this.tag버전관리.SuspendLayout();
			this.panelExt70.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panelExt71.SuspendLayout();
			this.panelExt72.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panelExt73.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex버전관리)).BeginInit();
			this.pnl버전관리.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp배포일자)).BeginInit();
			this.tpgStndItem.SuspendLayout();
			this.oneGridItem85.SuspendLayout();
			this.bpPanelControl51.SuspendLayout();
			this.oneGridItem86.SuspendLayout();
			this.bpPanelControl159.SuspendLayout();
			this.oneGridItem87.SuspendLayout();
			this.bpPanelControl160.SuspendLayout();
			this.oneGridItem88.SuspendLayout();
			this.bpPanelControl161.SuspendLayout();
			this.oneGridItem89.SuspendLayout();
			this.bpPanelControl162.SuspendLayout();
			this.oneGridItem90.SuspendLayout();
			this.bpPanelControl163.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem01)).BeginInit();
			this.oneGridItem91.SuspendLayout();
			this.bpPanelControl164.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem02)).BeginInit();
			this.oneGridItem92.SuspendLayout();
			this.bpPanelControl165.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem03)).BeginInit();
			this.oneGridItem93.SuspendLayout();
			this.bpPanelControl166.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem04)).BeginInit();
			this.oneGridItem94.SuspendLayout();
			this.bpPanelControl167.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem05)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem81.SuspendLayout();
			this.bpPanelControl147.SuspendLayout();
			this.bpPanelControl146.SuspendLayout();
			this.bpPanelControl145.SuspendLayout();
			this.bpPanelControl144.SuspendLayout();
			this.oneGridItem82.SuspendLayout();
			this.bpPanelControl151.SuspendLayout();
			this.bpPanelControl150.SuspendLayout();
			this.bpPanelControl149.SuspendLayout();
			this.bpPanelControl148.SuspendLayout();
			this.oneGridItem83.SuspendLayout();
			this.bpPanelControl154.SuspendLayout();
			this.bpPanelControl153.SuspendLayout();
			this.bpPanelControl152.SuspendLayout();
			this.oneGridItem84.SuspendLayout();
			this.bpPanelControl158.SuspendLayout();
			this.bpPanelControl157.SuspendLayout();
			this.bpPanelControl156.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl9.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl11.SuspendLayout();
			this.bpPanelControl10.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl13.SuspendLayout();
			this.oneGridItem97.SuspendLayout();
			this.bpPanelControl175.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.oneGridItem100.SuspendLayout();
			this.bpPanelControl168.SuspendLayout();
			this.bpPanelControl170.SuspendLayout();
			this.bpPanelControl169.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1647, 820);
			// 
			// img
			// 
			this.img.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.img.ImageSize = new System.Drawing.Size(16, 16);
			this.img.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(3, 163);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flex);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tab);
			this.splitContainer1.Panel2MinSize = 760;
			this.splitContainer1.Size = new System.Drawing.Size(1641, 654);
			this.splitContainer1.SplitterDistance = 857;
			this.splitContainer1.TabIndex = 5;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.Font = new System.Drawing.Font("굴림", 9F);
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(0, 0);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(857, 654);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 4;
			this._flex.UseGridCalculator = true;
			// 
			// tab
			// 
			this.tab.Controls.Add(this.tpg기본);
			this.tab.Controls.Add(this.tpg자재);
			this.tab.Controls.Add(this.tpg오더);
			this.tab.Controls.Add(this.tpg품질);
			this.tab.Controls.Add(this.tpg기타);
			this.tab.Controls.Add(this.tpgIMAGE);
			this.tab.Controls.Add(this.tag버전관리);
			this.tab.Controls.Add(this.tpgStndItem);
			this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tab.ItemSize = new System.Drawing.Size(120, 20);
			this.tab.Location = new System.Drawing.Point(0, 0);
			this.tab.Name = "tab";
			this.tab.SelectedIndex = 0;
			this.tab.Size = new System.Drawing.Size(780, 654);
			this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tab.TabIndex = 3;
			// 
			// tpg기본
			// 
			this.tpg기본.BackColor = System.Drawing.Color.White;
			this.tpg기본.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg기본.Controls.Add(this.pnl기본);
			this.tpg기본.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tpg기본.ImageIndex = 0;
			this.tpg기본.Location = new System.Drawing.Point(4, 24);
			this.tpg기본.Name = "tpg기본";
			this.tpg기본.Padding = new System.Windows.Forms.Padding(4);
			this.tpg기본.Size = new System.Drawing.Size(772, 626);
			this.tpg기본.TabIndex = 0;
			this.tpg기본.Tag = "BASE";
			this.tpg기본.Text = "기본";
			this.tpg기본.UseVisualStyleBackColor = true;
			// 
			// pnl기본
			// 
			this.pnl기본.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl기본.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem5,
            this.oneGridItem6,
            this.oneGridItem7,
            this.oneGridItem8,
            this.oneGridItem9,
            this.oneGridItem10,
            this.oneGridItem11,
            this.oneGridItem12,
            this.oneGridItem13,
            this.oneGridItem14,
            this.oneGridItem99,
            this.oneGridItem15,
            this.oneGridItem16,
            this.oneGridItem17,
            this.oneGridItem18,
            this.oneGridItem19,
            this.oneGridItem20,
            this.oneGridItem21,
            this.oneGridItem22,
            this.oneGridItem23,
            this.oneGridItem24,
            this.oneGridItem25,
            this.oneGridItem26,
            this.oneGridItem101});
			this.pnl기본.Location = new System.Drawing.Point(4, 4);
			this.pnl기본.Name = "pnl기본";
			this.pnl기본.Size = new System.Drawing.Size(760, 614);
			this.pnl기본.TabIndex = 1;
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bpPanelControl14);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 0;
			// 
			// bpPanelControl14
			// 
			this.bpPanelControl14.Controls.Add(this._btnAutoCdItem);
			this.bpPanelControl14.Controls.Add(this.m_lblCdItem);
			this.bpPanelControl14.Controls.Add(this.txt품목코드);
			this.bpPanelControl14.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl14.Name = "bpPanelControl14";
			this.bpPanelControl14.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl14.TabIndex = 0;
			this.bpPanelControl14.Text = "bpPanelControl14";
			// 
			// _btnAutoCdItem
			// 
			this._btnAutoCdItem.BackColor = System.Drawing.Color.White;
			this._btnAutoCdItem.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnAutoCdItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnAutoCdItem.Location = new System.Drawing.Point(348, 1);
			this._btnAutoCdItem.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnAutoCdItem.Name = "_btnAutoCdItem";
			this._btnAutoCdItem.Size = new System.Drawing.Size(64, 19);
			this._btnAutoCdItem.TabIndex = 237;
			this._btnAutoCdItem.TabStop = false;
			this._btnAutoCdItem.Text = "자동채번";
			this._btnAutoCdItem.UseVisualStyleBackColor = false;
			this._btnAutoCdItem.Visible = false;
			// 
			// m_lblCdItem
			// 
			this.m_lblCdItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblCdItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblCdItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblCdItem.Name = "m_lblCdItem";
			this.m_lblCdItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblCdItem.TabIndex = 1;
			this.m_lblCdItem.Tag = "CD_ITEM";
			this.m_lblCdItem.Text = "품목코드";
			this.m_lblCdItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt품목코드
			// 
			this.txt품목코드.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.txt품목코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt품목코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt품목코드.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txt품목코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt품목코드.Location = new System.Drawing.Point(157, 1);
			this.txt품목코드.MaxLength = 20;
			this.txt품목코드.Name = "txt품목코드";
			this.txt품목코드.Size = new System.Drawing.Size(185, 21);
			this.txt품목코드.TabIndex = 0;
			this.txt품목코드.Tag = "CD_ITEM";
			// 
			// oneGridItem6
			// 
			this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem6.Controls.Add(this.bpPanelControl15);
			this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem6.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem6.Name = "oneGridItem6";
			this.oneGridItem6.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem6.TabIndex = 1;
			// 
			// bpPanelControl15
			// 
			this.bpPanelControl15.Controls.Add(this.txt품명);
			this.bpPanelControl15.Controls.Add(this.m_lblNmItem);
			this.bpPanelControl15.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl15.Name = "bpPanelControl15";
			this.bpPanelControl15.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl15.TabIndex = 1;
			this.bpPanelControl15.Text = "bpPanelControl15";
			// 
			// txt품명
			// 
			this.txt품명.Location = new System.Drawing.Point(157, 0);
			this.txt품명.Modified = false;
			this.txt품명.Name = "txt품명";
			this.txt품명.Size = new System.Drawing.Size(529, 21);
			this.txt품명.TabIndex = 68;
			this.txt품명.TabStop = false;
			this.txt품명.Tag = "NM_ITEM";
			this.txt품명.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			// 
			// m_lblNmItem
			// 
			this.m_lblNmItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblNmItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblNmItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblNmItem.Name = "m_lblNmItem";
			this.m_lblNmItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblNmItem.TabIndex = 67;
			this.m_lblNmItem.Tag = "NM_ITEM";
			this.m_lblNmItem.Text = "품명";
			this.m_lblNmItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem7
			// 
			this.oneGridItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem7.Controls.Add(this.bpPanelControl16);
			this.oneGridItem7.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem7.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem7.Name = "oneGridItem7";
			this.oneGridItem7.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem7.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem7.TabIndex = 2;
			// 
			// bpPanelControl16
			// 
			this.bpPanelControl16.Controls.Add(this.m_lblEnItem);
			this.bpPanelControl16.Controls.Add(this.txt영문품명);
			this.bpPanelControl16.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl16.Name = "bpPanelControl16";
			this.bpPanelControl16.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl16.TabIndex = 1;
			this.bpPanelControl16.Text = "bpPanelControl16";
			// 
			// m_lblEnItem
			// 
			this.m_lblEnItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblEnItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblEnItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblEnItem.Name = "m_lblEnItem";
			this.m_lblEnItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblEnItem.TabIndex = 68;
			this.m_lblEnItem.Tag = "EN_ITEM";
			this.m_lblEnItem.Text = "품명(영)";
			this.m_lblEnItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt영문품명
			// 
			this.txt영문품명.BackColor = System.Drawing.Color.White;
			this.txt영문품명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt영문품명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt영문품명.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt영문품명.Location = new System.Drawing.Point(157, 1);
			this.txt영문품명.MaxLength = 200;
			this.txt영문품명.Name = "txt영문품명";
			this.txt영문품명.Size = new System.Drawing.Size(529, 21);
			this.txt영문품명.TabIndex = 2;
			this.txt영문품명.Tag = "EN_ITEM";
			// 
			// oneGridItem8
			// 
			this.oneGridItem8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem8.Controls.Add(this.bpPanelControl17);
			this.oneGridItem8.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem8.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem8.Name = "oneGridItem8";
			this.oneGridItem8.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem8.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem8.TabIndex = 3;
			// 
			// bpPanelControl17
			// 
			this.bpPanelControl17.Controls.Add(this.m_lblStndItem);
			this.bpPanelControl17.Controls.Add(this.txt규격);
			this.bpPanelControl17.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl17.Name = "bpPanelControl17";
			this.bpPanelControl17.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl17.TabIndex = 1;
			this.bpPanelControl17.Text = "bpPanelControl17";
			// 
			// m_lblStndItem
			// 
			this.m_lblStndItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblStndItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblStndItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblStndItem.Name = "m_lblStndItem";
			this.m_lblStndItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblStndItem.TabIndex = 69;
			this.m_lblStndItem.Tag = "STND_ITEM";
			this.m_lblStndItem.Text = "규격";
			this.m_lblStndItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt규격
			// 
			this.txt규격.BackColor = System.Drawing.Color.White;
			this.txt규격.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt규격.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt규격.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt규격.Location = new System.Drawing.Point(157, 1);
			this.txt규격.MaxLength = 200;
			this.txt규격.Name = "txt규격";
			this.txt규격.Size = new System.Drawing.Size(529, 21);
			this.txt규격.TabIndex = 3;
			this.txt규격.Tag = "STND_ITEM";
			// 
			// oneGridItem9
			// 
			this.oneGridItem9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem9.Controls.Add(this.bpPanelControl18);
			this.oneGridItem9.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem9.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem9.Name = "oneGridItem9";
			this.oneGridItem9.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem9.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem9.TabIndex = 4;
			// 
			// bpPanelControl18
			// 
			this.bpPanelControl18.Controls.Add(this.labelExt6);
			this.bpPanelControl18.Controls.Add(this.txt세부규격);
			this.bpPanelControl18.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl18.Name = "bpPanelControl18";
			this.bpPanelControl18.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl18.TabIndex = 1;
			this.bpPanelControl18.Text = "bpPanelControl18";
			// 
			// labelExt6
			// 
			this.labelExt6.BackColor = System.Drawing.Color.Transparent;
			this.labelExt6.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt6.Location = new System.Drawing.Point(0, 3);
			this.labelExt6.Name = "labelExt6";
			this.labelExt6.Size = new System.Drawing.Size(156, 16);
			this.labelExt6.TabIndex = 154;
			this.labelExt6.Text = "세부규격";
			this.labelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt세부규격
			// 
			this.txt세부규격.BackColor = System.Drawing.Color.White;
			this.txt세부규격.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt세부규격.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt세부규격.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt세부규격.Location = new System.Drawing.Point(157, 1);
			this.txt세부규격.MaxLength = 200;
			this.txt세부규격.Name = "txt세부규격";
			this.txt세부규격.Size = new System.Drawing.Size(529, 21);
			this.txt세부규격.TabIndex = 4;
			this.txt세부규격.Tag = "STND_DETAIL_ITEM";
			// 
			// oneGridItem10
			// 
			this.oneGridItem10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem10.Controls.Add(this.bpPanelControl19);
			this.oneGridItem10.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem10.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem10.Name = "oneGridItem10";
			this.oneGridItem10.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem10.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem10.TabIndex = 5;
			// 
			// bpPanelControl19
			// 
			this.bpPanelControl19.Controls.Add(this.m_lblMatItem);
			this.bpPanelControl19.Controls.Add(this.txt재질);
			this.bpPanelControl19.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl19.Name = "bpPanelControl19";
			this.bpPanelControl19.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl19.TabIndex = 1;
			this.bpPanelControl19.Text = "bpPanelControl19";
			// 
			// m_lblMatItem
			// 
			this.m_lblMatItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblMatItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblMatItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblMatItem.Name = "m_lblMatItem";
			this.m_lblMatItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblMatItem.TabIndex = 77;
			this.m_lblMatItem.Tag = "MAT_ITEM";
			this.m_lblMatItem.Text = "재질";
			this.m_lblMatItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt재질
			// 
			this.txt재질.BackColor = System.Drawing.Color.White;
			this.txt재질.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt재질.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt재질.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt재질.Location = new System.Drawing.Point(157, 1);
			this.txt재질.MaxLength = 50;
			this.txt재질.Name = "txt재질";
			this.txt재질.Size = new System.Drawing.Size(529, 21);
			this.txt재질.TabIndex = 5;
			this.txt재질.Tag = "MAT_ITEM";
			// 
			// oneGridItem11
			// 
			this.oneGridItem11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem11.Controls.Add(this.bpPanelControl20);
			this.oneGridItem11.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem11.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem11.Name = "oneGridItem11";
			this.oneGridItem11.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem11.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem11.TabIndex = 6;
			// 
			// bpPanelControl20
			// 
			this.bpPanelControl20.Controls.Add(this._btnBarcode);
			this.bpPanelControl20.Controls.Add(this.m_lblBarCode);
			this.bpPanelControl20.Controls.Add(this.txt바코드);
			this.bpPanelControl20.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl20.Name = "bpPanelControl20";
			this.bpPanelControl20.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl20.TabIndex = 0;
			this.bpPanelControl20.Text = "bpPanelControl20";
			// 
			// _btnBarcode
			// 
			this._btnBarcode.BackColor = System.Drawing.Color.White;
			this._btnBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnBarcode.Location = new System.Drawing.Point(347, 1);
			this._btnBarcode.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnBarcode.Name = "_btnBarcode";
			this._btnBarcode.Size = new System.Drawing.Size(62, 19);
			this._btnBarcode.TabIndex = 238;
			this._btnBarcode.TabStop = false;
			this._btnBarcode.Text = "검색";
			this._btnBarcode.UseVisualStyleBackColor = false;
			this._btnBarcode.Visible = false;
			// 
			// m_lblBarCode
			// 
			this.m_lblBarCode.BackColor = System.Drawing.Color.Transparent;
			this.m_lblBarCode.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblBarCode.Location = new System.Drawing.Point(0, 3);
			this.m_lblBarCode.Name = "m_lblBarCode";
			this.m_lblBarCode.Size = new System.Drawing.Size(156, 16);
			this.m_lblBarCode.TabIndex = 70;
			this.m_lblBarCode.Tag = "BARCODE";
			this.m_lblBarCode.Text = "Bar code";
			this.m_lblBarCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt바코드
			// 
			this.txt바코드.BackColor = System.Drawing.Color.White;
			this.txt바코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt바코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt바코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt바코드.Location = new System.Drawing.Point(157, 1);
			this.txt바코드.MaxLength = 20;
			this.txt바코드.Name = "txt바코드";
			this.txt바코드.Size = new System.Drawing.Size(185, 21);
			this.txt바코드.TabIndex = 6;
			this.txt바코드.Tag = "BARCODE";
			// 
			// oneGridItem12
			// 
			this.oneGridItem12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem12.Controls.Add(this.bpPanelControl21);
			this.oneGridItem12.Controls.Add(this.bpPanelControl23);
			this.oneGridItem12.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem12.Location = new System.Drawing.Point(0, 161);
			this.oneGridItem12.Name = "oneGridItem12";
			this.oneGridItem12.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem12.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem12.TabIndex = 7;
			// 
			// bpPanelControl21
			// 
			this.bpPanelControl21.Controls.Add(this.labelExt2);
			this.bpPanelControl21.Controls.Add(this.cbo계정구분);
			this.bpPanelControl21.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl21.Name = "bpPanelControl21";
			this.bpPanelControl21.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl21.TabIndex = 1;
			this.bpPanelControl21.Text = "bpPanelControl21";
			// 
			// labelExt2
			// 
			this.labelExt2.BackColor = System.Drawing.Color.Transparent;
			this.labelExt2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt2.Location = new System.Drawing.Point(0, 3);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(156, 16);
			this.labelExt2.TabIndex = 148;
			this.labelExt2.Tag = "CLS_ITEM";
			this.labelExt2.Text = "계정구분";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo계정구분
			// 
			this.cbo계정구분.AutoDropDown = true;
			this.cbo계정구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo계정구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo계정구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo계정구분.ItemHeight = 12;
			this.cbo계정구분.Location = new System.Drawing.Point(157, 1);
			this.cbo계정구분.Name = "cbo계정구분";
			this.cbo계정구분.Size = new System.Drawing.Size(70, 20);
			this.cbo계정구분.TabIndex = 7;
			this.cbo계정구분.Tag = "CLS_ITEM";
			// 
			// bpPanelControl23
			// 
			this.bpPanelControl23.Controls.Add(this.m_lblTpProc);
			this.bpPanelControl23.Controls.Add(this.cbo조달구분);
			this.bpPanelControl23.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl23.Name = "bpPanelControl23";
			this.bpPanelControl23.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl23.TabIndex = 2;
			this.bpPanelControl23.Text = "bpPanelControl23";
			// 
			// m_lblTpProc
			// 
			this.m_lblTpProc.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTpProc.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblTpProc.Location = new System.Drawing.Point(0, 3);
			this.m_lblTpProc.Name = "m_lblTpProc";
			this.m_lblTpProc.Size = new System.Drawing.Size(156, 16);
			this.m_lblTpProc.TabIndex = 78;
			this.m_lblTpProc.Tag = "TP_PROC";
			this.m_lblTpProc.Text = "조달구분";
			this.m_lblTpProc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo조달구분
			// 
			this.cbo조달구분.AutoDropDown = true;
			this.cbo조달구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo조달구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo조달구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo조달구분.ItemHeight = 12;
			this.cbo조달구분.Location = new System.Drawing.Point(157, 1);
			this.cbo조달구분.MaxLength = 3;
			this.cbo조달구분.Name = "cbo조달구분";
			this.cbo조달구분.Size = new System.Drawing.Size(70, 20);
			this.cbo조달구분.TabIndex = 8;
			this.cbo조달구분.Tag = "TP_PROC";
			// 
			// oneGridItem13
			// 
			this.oneGridItem13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem13.Controls.Add(this.bpPanelControl22);
			this.oneGridItem13.Controls.Add(this.bpPanelControl25);
			this.oneGridItem13.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem13.Location = new System.Drawing.Point(0, 184);
			this.oneGridItem13.Name = "oneGridItem13";
			this.oneGridItem13.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem13.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem13.TabIndex = 8;
			// 
			// bpPanelControl22
			// 
			this.bpPanelControl22.Controls.Add(this.m_lblClsItem);
			this.bpPanelControl22.Controls.Add(this.cbo출하단위);
			this.bpPanelControl22.Controls.Add(this.cur출하단위수량);
			this.bpPanelControl22.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl22.Name = "bpPanelControl22";
			this.bpPanelControl22.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl22.TabIndex = 3;
			this.bpPanelControl22.Text = "bpPanelControl22";
			// 
			// m_lblClsItem
			// 
			this.m_lblClsItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblClsItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblClsItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblClsItem.Name = "m_lblClsItem";
			this.m_lblClsItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblClsItem.TabIndex = 137;
			this.m_lblClsItem.Tag = "CLS_ITEM";
			this.m_lblClsItem.Text = "출하단위";
			this.m_lblClsItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo출하단위
			// 
			this.cbo출하단위.AutoDropDown = true;
			this.cbo출하단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo출하단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo출하단위.ItemHeight = 12;
			this.cbo출하단위.Location = new System.Drawing.Point(157, 1);
			this.cbo출하단위.MaxLength = 3;
			this.cbo출하단위.Name = "cbo출하단위";
			this.cbo출하단위.Size = new System.Drawing.Size(70, 20);
			this.cbo출하단위.TabIndex = 9;
			this.cbo출하단위.Tag = "UNIT_GI";
			// 
			// cur출하단위수량
			// 
			this.cur출하단위수량.BackColor = System.Drawing.Color.White;
			this.cur출하단위수량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur출하단위수량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur출하단위수량.CurrencyDecimalDigits = 4;
			this.cur출하단위수량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur출하단위수량.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur출하단위수량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur출하단위수량.Location = new System.Drawing.Point(227, 1);
			this.cur출하단위수량.MaxLength = 15;
			this.cur출하단위수량.MaxValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            262144});
			this.cur출하단위수량.MinValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147221504});
			this.cur출하단위수량.Name = "cur출하단위수량";
			this.cur출하단위수량.NullString = "0";
			this.cur출하단위수량.PositiveColor = System.Drawing.Color.Black;
			this.cur출하단위수량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur출하단위수량.Size = new System.Drawing.Size(115, 21);
			this.cur출하단위수량.TabIndex = 10;
			this.cur출하단위수량.Tag = "UNIT_GI_FACT";
			this.cur출하단위수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl25
			// 
			this.bpPanelControl25.Controls.Add(this.m_lblUnitIm);
			this.bpPanelControl25.Controls.Add(this.cbo재고단위);
			this.bpPanelControl25.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl25.Name = "bpPanelControl25";
			this.bpPanelControl25.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl25.TabIndex = 2;
			this.bpPanelControl25.Text = "bpPanelControl25";
			// 
			// m_lblUnitIm
			// 
			this.m_lblUnitIm.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitIm.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitIm.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitIm.Name = "m_lblUnitIm";
			this.m_lblUnitIm.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitIm.TabIndex = 79;
			this.m_lblUnitIm.Tag = "UNIT_IM";
			this.m_lblUnitIm.Text = "재고단위";
			this.m_lblUnitIm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo재고단위
			// 
			this.cbo재고단위.AutoDropDown = true;
			this.cbo재고단위.BackColor = System.Drawing.Color.White;
			this.cbo재고단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo재고단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo재고단위.ItemHeight = 12;
			this.cbo재고단위.Location = new System.Drawing.Point(157, 1);
			this.cbo재고단위.MaxLength = 3;
			this.cbo재고단위.Name = "cbo재고단위";
			this.cbo재고단위.Size = new System.Drawing.Size(70, 20);
			this.cbo재고단위.TabIndex = 11;
			this.cbo재고단위.Tag = "UNIT_IM";
			// 
			// oneGridItem14
			// 
			this.oneGridItem14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem14.Controls.Add(this.bpPanelControl24);
			this.oneGridItem14.Controls.Add(this.bpPanelControl27);
			this.oneGridItem14.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem14.Location = new System.Drawing.Point(0, 207);
			this.oneGridItem14.Name = "oneGridItem14";
			this.oneGridItem14.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem14.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem14.TabIndex = 9;
			// 
			// bpPanelControl24
			// 
			this.bpPanelControl24.Controls.Add(this.m_lblUnitSo);
			this.bpPanelControl24.Controls.Add(this.cbo수주단위);
			this.bpPanelControl24.Controls.Add(this.cur수주단위수량);
			this.bpPanelControl24.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl24.Name = "bpPanelControl24";
			this.bpPanelControl24.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl24.TabIndex = 3;
			this.bpPanelControl24.Text = "bpPanelControl24";
			// 
			// m_lblUnitSo
			// 
			this.m_lblUnitSo.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitSo.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitSo.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitSo.Name = "m_lblUnitSo";
			this.m_lblUnitSo.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitSo.TabIndex = 138;
			this.m_lblUnitSo.Tag = "UNIT_SO";
			this.m_lblUnitSo.Text = "수주단위";
			this.m_lblUnitSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo수주단위
			// 
			this.cbo수주단위.AutoDropDown = true;
			this.cbo수주단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo수주단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo수주단위.ItemHeight = 12;
			this.cbo수주단위.Location = new System.Drawing.Point(157, 1);
			this.cbo수주단위.MaxLength = 3;
			this.cbo수주단위.Name = "cbo수주단위";
			this.cbo수주단위.Size = new System.Drawing.Size(70, 20);
			this.cbo수주단위.TabIndex = 12;
			this.cbo수주단위.Tag = "UNIT_SO";
			// 
			// cur수주단위수량
			// 
			this.cur수주단위수량.BackColor = System.Drawing.Color.White;
			this.cur수주단위수량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur수주단위수량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur수주단위수량.CurrencyDecimalDigits = 4;
			this.cur수주단위수량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur수주단위수량.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur수주단위수량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur수주단위수량.Location = new System.Drawing.Point(227, 1);
			this.cur수주단위수량.MaxLength = 15;
			this.cur수주단위수량.MaxValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            262144});
			this.cur수주단위수량.MinValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147221504});
			this.cur수주단위수량.Name = "cur수주단위수량";
			this.cur수주단위수량.NullString = "0";
			this.cur수주단위수량.PositiveColor = System.Drawing.Color.Black;
			this.cur수주단위수량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur수주단위수량.Size = new System.Drawing.Size(115, 21);
			this.cur수주단위수량.TabIndex = 13;
			this.cur수주단위수량.Tag = "UNIT_SO_FACT";
			this.cur수주단위수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl27
			// 
			this.bpPanelControl27.Controls.Add(this.m_lblUnitPo);
			this.bpPanelControl27.Controls.Add(this.cbo구매단위);
			this.bpPanelControl27.Controls.Add(this.cur구매단위수량);
			this.bpPanelControl27.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl27.Name = "bpPanelControl27";
			this.bpPanelControl27.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl27.TabIndex = 2;
			this.bpPanelControl27.Text = "bpPanelControl27";
			// 
			// m_lblUnitPo
			// 
			this.m_lblUnitPo.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitPo.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitPo.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitPo.Name = "m_lblUnitPo";
			this.m_lblUnitPo.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitPo.TabIndex = 85;
			this.m_lblUnitPo.Tag = "UNIT_PO";
			this.m_lblUnitPo.Text = "발주단위";
			this.m_lblUnitPo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo구매단위
			// 
			this.cbo구매단위.AutoDropDown = true;
			this.cbo구매단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo구매단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo구매단위.ItemHeight = 12;
			this.cbo구매단위.Location = new System.Drawing.Point(157, 1);
			this.cbo구매단위.MaxLength = 3;
			this.cbo구매단위.Name = "cbo구매단위";
			this.cbo구매단위.Size = new System.Drawing.Size(70, 20);
			this.cbo구매단위.TabIndex = 14;
			this.cbo구매단위.Tag = "UNIT_PO";
			// 
			// cur구매단위수량
			// 
			this.cur구매단위수량.BackColor = System.Drawing.Color.White;
			this.cur구매단위수량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur구매단위수량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur구매단위수량.CurrencyDecimalDigits = 4;
			this.cur구매단위수량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur구매단위수량.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur구매단위수량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur구매단위수량.Location = new System.Drawing.Point(227, 1);
			this.cur구매단위수량.MaxLength = 15;
			this.cur구매단위수량.MaxValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            262144});
			this.cur구매단위수량.MinValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147221504});
			this.cur구매단위수량.Name = "cur구매단위수량";
			this.cur구매단위수량.NullString = "0";
			this.cur구매단위수량.PositiveColor = System.Drawing.Color.Black;
			this.cur구매단위수량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur구매단위수량.Size = new System.Drawing.Size(115, 21);
			this.cur구매단위수량.TabIndex = 15;
			this.cur구매단위수량.Tag = "UNIT_PO_FACT";
			this.cur구매단위수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem99
			// 
			this.oneGridItem99.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem99.Controls.Add(this.bpPanelControl26);
			this.oneGridItem99.Controls.Add(this.bpPanelControl173);
			this.oneGridItem99.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem99.Location = new System.Drawing.Point(0, 230);
			this.oneGridItem99.Name = "oneGridItem99";
			this.oneGridItem99.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem99.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem99.TabIndex = 10;
			// 
			// bpPanelControl26
			// 
			this.bpPanelControl26.Controls.Add(this._curUnitMoFact);
			this.bpPanelControl26.Controls.Add(this.m_lblUnitMo);
			this.bpPanelControl26.Controls.Add(this.cbo생산단위);
			this.bpPanelControl26.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl26.Name = "bpPanelControl26";
			this.bpPanelControl26.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl26.TabIndex = 3;
			this.bpPanelControl26.Text = "bpPanelControl26";
			// 
			// _curUnitMoFact
			// 
			this._curUnitMoFact.BackColor = System.Drawing.Color.White;
			this._curUnitMoFact.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._curUnitMoFact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._curUnitMoFact.CurrencyDecimalDigits = 4;
			this._curUnitMoFact.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._curUnitMoFact.Font = new System.Drawing.Font("굴림체", 9F);
			this._curUnitMoFact.ForeColor = System.Drawing.SystemColors.ControlText;
			this._curUnitMoFact.Location = new System.Drawing.Point(227, 1);
			this._curUnitMoFact.MaxLength = 15;
			this._curUnitMoFact.MaxValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            262144});
			this._curUnitMoFact.MinValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147221504});
			this._curUnitMoFact.Name = "_curUnitMoFact";
			this._curUnitMoFact.NullString = "0";
			this._curUnitMoFact.PositiveColor = System.Drawing.Color.Black;
			this._curUnitMoFact.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._curUnitMoFact.Size = new System.Drawing.Size(115, 21);
			this._curUnitMoFact.TabIndex = 140;
			this._curUnitMoFact.Tag = "UNIT_MO_FACT";
			this._curUnitMoFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_lblUnitMo
			// 
			this.m_lblUnitMo.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitMo.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitMo.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitMo.Name = "m_lblUnitMo";
			this.m_lblUnitMo.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitMo.TabIndex = 139;
			this.m_lblUnitMo.Tag = "UNIT_MO";
			this.m_lblUnitMo.Text = "생산단위";
			this.m_lblUnitMo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo생산단위
			// 
			this.cbo생산단위.AutoDropDown = true;
			this.cbo생산단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo생산단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo생산단위.ItemHeight = 12;
			this.cbo생산단위.Location = new System.Drawing.Point(157, 1);
			this.cbo생산단위.MaxLength = 3;
			this.cbo생산단위.Name = "cbo생산단위";
			this.cbo생산단위.Size = new System.Drawing.Size(70, 20);
			this.cbo생산단위.TabIndex = 16;
			this.cbo생산단위.Tag = "UNIT_MO";
			// 
			// bpPanelControl173
			// 
			this.bpPanelControl173.Controls.Add(this.m_lblUnitSu);
			this.bpPanelControl173.Controls.Add(this.cbo외주단위);
			this.bpPanelControl173.Controls.Add(this.cur외주단위수량);
			this.bpPanelControl173.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl173.Name = "bpPanelControl173";
			this.bpPanelControl173.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl173.TabIndex = 3;
			this.bpPanelControl173.Text = "bpPanelControl173";
			// 
			// m_lblUnitSu
			// 
			this.m_lblUnitSu.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitSu.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitSu.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitSu.Name = "m_lblUnitSu";
			this.m_lblUnitSu.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitSu.TabIndex = 85;
			this.m_lblUnitSu.Tag = "UNIT_PO";
			this.m_lblUnitSu.Text = "외주단위";
			this.m_lblUnitSu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo외주단위
			// 
			this.cbo외주단위.AutoDropDown = true;
			this.cbo외주단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo외주단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo외주단위.ItemHeight = 12;
			this.cbo외주단위.Location = new System.Drawing.Point(157, 1);
			this.cbo외주단위.MaxLength = 3;
			this.cbo외주단위.Name = "cbo외주단위";
			this.cbo외주단위.Size = new System.Drawing.Size(70, 20);
			this.cbo외주단위.TabIndex = 14;
			this.cbo외주단위.Tag = "UNIT_SU";
			// 
			// cur외주단위수량
			// 
			this.cur외주단위수량.BackColor = System.Drawing.Color.White;
			this.cur외주단위수량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur외주단위수량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur외주단위수량.CurrencyDecimalDigits = 4;
			this.cur외주단위수량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur외주단위수량.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur외주단위수량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur외주단위수량.Location = new System.Drawing.Point(227, 1);
			this.cur외주단위수량.MaxLength = 15;
			this.cur외주단위수량.MaxValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            262144});
			this.cur외주단위수량.MinValue = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147221504});
			this.cur외주단위수량.Name = "cur외주단위수량";
			this.cur외주단위수량.NullString = "0";
			this.cur외주단위수량.PositiveColor = System.Drawing.Color.Black;
			this.cur외주단위수량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur외주단위수량.Size = new System.Drawing.Size(115, 21);
			this.cur외주단위수량.TabIndex = 15;
			this.cur외주단위수량.Tag = "UNIT_SU_FACT";
			this.cur외주단위수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem15
			// 
			this.oneGridItem15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem15.Controls.Add(this.bpPanelControl28);
			this.oneGridItem15.Controls.Add(this.bpPanelControl29);
			this.oneGridItem15.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem15.Location = new System.Drawing.Point(0, 253);
			this.oneGridItem15.Name = "oneGridItem15";
			this.oneGridItem15.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem15.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem15.TabIndex = 11;
			// 
			// bpPanelControl28
			// 
			this.bpPanelControl28.Controls.Add(this.m_lblTpPart);
			this.bpPanelControl28.Controls.Add(this.cbo내외자구분);
			this.bpPanelControl28.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl28.Name = "bpPanelControl28";
			this.bpPanelControl28.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl28.TabIndex = 3;
			this.bpPanelControl28.Text = "bpPanelControl28";
			// 
			// m_lblTpPart
			// 
			this.m_lblTpPart.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTpPart.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblTpPart.Location = new System.Drawing.Point(0, 3);
			this.m_lblTpPart.Name = "m_lblTpPart";
			this.m_lblTpPart.Size = new System.Drawing.Size(156, 16);
			this.m_lblTpPart.TabIndex = 136;
			this.m_lblTpPart.Tag = "TP_PART";
			this.m_lblTpPart.Text = "내외자구분";
			this.m_lblTpPart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo내외자구분
			// 
			this.cbo내외자구분.AutoDropDown = true;
			this.cbo내외자구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo내외자구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo내외자구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo내외자구분.ItemHeight = 12;
			this.cbo내외자구분.Location = new System.Drawing.Point(157, 1);
			this.cbo내외자구분.MaxLength = 3;
			this.cbo내외자구분.Name = "cbo내외자구분";
			this.cbo내외자구분.Size = new System.Drawing.Size(70, 20);
			this.cbo내외자구분.TabIndex = 18;
			this.cbo내외자구분.Tag = "TP_PART";
			// 
			// bpPanelControl29
			// 
			this.bpPanelControl29.Controls.Add(this.m_lblTpItem);
			this.bpPanelControl29.Controls.Add(this.cbo품목타입);
			this.bpPanelControl29.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl29.Name = "bpPanelControl29";
			this.bpPanelControl29.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl29.TabIndex = 2;
			this.bpPanelControl29.Text = "bpPanelControl29";
			// 
			// m_lblTpItem
			// 
			this.m_lblTpItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTpItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblTpItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblTpItem.Name = "m_lblTpItem";
			this.m_lblTpItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblTpItem.TabIndex = 80;
			this.m_lblTpItem.Tag = "TP_ITEM";
			this.m_lblTpItem.Text = "품목타입";
			this.m_lblTpItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo품목타입
			// 
			this.cbo품목타입.AutoDropDown = true;
			this.cbo품목타입.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo품목타입.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo품목타입.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo품목타입.ItemHeight = 12;
			this.cbo품목타입.Location = new System.Drawing.Point(157, 1);
			this.cbo품목타입.MaxLength = 3;
			this.cbo품목타입.Name = "cbo품목타입";
			this.cbo품목타입.Size = new System.Drawing.Size(70, 20);
			this.cbo품목타입.TabIndex = 17;
			this.cbo품목타입.Tag = "TP_ITEM";
			// 
			// oneGridItem16
			// 
			this.oneGridItem16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem16.Controls.Add(this.bpPanelControl30);
			this.oneGridItem16.Controls.Add(this.bpPanelControl31);
			this.oneGridItem16.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem16.Location = new System.Drawing.Point(0, 276);
			this.oneGridItem16.Name = "oneGridItem16";
			this.oneGridItem16.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem16.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem16.TabIndex = 12;
			// 
			// bpPanelControl30
			// 
			this.bpPanelControl30.Controls.Add(this.m_lblGrpMfg);
			this.bpPanelControl30.Controls.Add(this.ctx제품군);
			this.bpPanelControl30.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl30.Name = "bpPanelControl30";
			this.bpPanelControl30.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl30.TabIndex = 3;
			this.bpPanelControl30.Text = "bpPanelControl30";
			// 
			// m_lblGrpMfg
			// 
			this.m_lblGrpMfg.BackColor = System.Drawing.Color.Transparent;
			this.m_lblGrpMfg.Location = new System.Drawing.Point(0, 3);
			this.m_lblGrpMfg.Name = "m_lblGrpMfg";
			this.m_lblGrpMfg.Size = new System.Drawing.Size(156, 16);
			this.m_lblGrpMfg.TabIndex = 151;
			this.m_lblGrpMfg.Tag = "GRP_MFG";
			this.m_lblGrpMfg.Text = "제품군";
			this.m_lblGrpMfg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx제품군
			// 
			this.ctx제품군.CodeName = null;
			this.ctx제품군.CodeValue = null;
			this.ctx제품군.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx제품군.Location = new System.Drawing.Point(157, 1);
			this.ctx제품군.Name = "ctx제품군";
			this.ctx제품군.Size = new System.Drawing.Size(185, 21);
			this.ctx제품군.TabIndex = 166;
			this.ctx제품군.TabStop = false;
			this.ctx제품군.Tag = "GRP_MFG,NM_GRP_MFG";
			// 
			// bpPanelControl31
			// 
			this.bpPanelControl31.Controls.Add(this.labelExt1);
			this.bpPanelControl31.Controls.Add(this.ctx품목군);
			this.bpPanelControl31.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl31.Name = "bpPanelControl31";
			this.bpPanelControl31.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl31.TabIndex = 2;
			this.bpPanelControl31.Text = "bpPanelControl31";
			// 
			// labelExt1
			// 
			this.labelExt1.BackColor = System.Drawing.Color.Transparent;
			this.labelExt1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt1.Location = new System.Drawing.Point(0, 3);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(156, 16);
			this.labelExt1.TabIndex = 145;
			this.labelExt1.Tag = "GRP_ITEM";
			this.labelExt1.Text = "품목군";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx품목군
			// 
			this.ctx품목군.CodeName = null;
			this.ctx품목군.CodeValue = null;
			this.ctx품목군.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEMGP_SUB;
			this.ctx품목군.Location = new System.Drawing.Point(157, 1);
			this.ctx품목군.Name = "ctx품목군";
			this.ctx품목군.Size = new System.Drawing.Size(185, 21);
			this.ctx품목군.TabIndex = 19;
			this.ctx품목군.TabStop = false;
			this.ctx품목군.Tag = "GRP_ITEM,NM_GRP_ITEM";
			// 
			// oneGridItem17
			// 
			this.oneGridItem17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem17.Controls.Add(this.bpPanelControl32);
			this.oneGridItem17.Controls.Add(this.bpPanelControl33);
			this.oneGridItem17.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem17.Location = new System.Drawing.Point(0, 299);
			this.oneGridItem17.Name = "oneGridItem17";
			this.oneGridItem17.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem17.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem17.TabIndex = 13;
			// 
			// bpPanelControl32
			// 
			this.bpPanelControl32.Controls.Add(this.m_lblUnitHs);
			this.bpPanelControl32.Controls.Add(this.cboHS단위);
			this.bpPanelControl32.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl32.Name = "bpPanelControl32";
			this.bpPanelControl32.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl32.TabIndex = 3;
			this.bpPanelControl32.Text = "bpPanelControl32";
			// 
			// m_lblUnitHs
			// 
			this.m_lblUnitHs.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitHs.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitHs.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitHs.Name = "m_lblUnitHs";
			this.m_lblUnitHs.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitHs.TabIndex = 141;
			this.m_lblUnitHs.Tag = "UNIT_HS";
			this.m_lblUnitHs.Text = "HS Code 단위";
			this.m_lblUnitHs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboHS단위
			// 
			this.cboHS단위.AutoDropDown = true;
			this.cboHS단위.BackColor = System.Drawing.Color.White;
			this.cboHS단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboHS단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cboHS단위.ItemHeight = 12;
			this.cboHS단위.Location = new System.Drawing.Point(157, 1);
			this.cboHS단위.MaxLength = 3;
			this.cboHS단위.Name = "cboHS단위";
			this.cboHS단위.Size = new System.Drawing.Size(70, 20);
			this.cboHS단위.TabIndex = 22;
			this.cboHS단위.Tag = "UNIT_HS";
			// 
			// bpPanelControl33
			// 
			this.bpPanelControl33.Controls.Add(this.m_lblNoHs);
			this.bpPanelControl33.Controls.Add(this.txtHS코드);
			this.bpPanelControl33.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl33.Name = "bpPanelControl33";
			this.bpPanelControl33.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl33.TabIndex = 2;
			this.bpPanelControl33.Text = "bpPanelControl33";
			// 
			// m_lblNoHs
			// 
			this.m_lblNoHs.BackColor = System.Drawing.Color.Transparent;
			this.m_lblNoHs.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblNoHs.Location = new System.Drawing.Point(0, 3);
			this.m_lblNoHs.Name = "m_lblNoHs";
			this.m_lblNoHs.Size = new System.Drawing.Size(156, 16);
			this.m_lblNoHs.TabIndex = 88;
			this.m_lblNoHs.Tag = "NO_HS";
			this.m_lblNoHs.Text = "HS Code";
			this.m_lblNoHs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtHS코드
			// 
			this.txtHS코드.BackColor = System.Drawing.Color.White;
			this.txtHS코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtHS코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHS코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txtHS코드.Location = new System.Drawing.Point(157, 1);
			this.txtHS코드.MaxLength = 40;
			this.txtHS코드.Name = "txtHS코드";
			this.txtHS코드.Size = new System.Drawing.Size(185, 21);
			this.txtHS코드.TabIndex = 21;
			this.txtHS코드.Tag = "NO_HS";
			// 
			// oneGridItem18
			// 
			this.oneGridItem18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem18.Controls.Add(this.bpPanelControl34);
			this.oneGridItem18.Controls.Add(this.bpPanelControl35);
			this.oneGridItem18.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem18.Location = new System.Drawing.Point(0, 322);
			this.oneGridItem18.Name = "oneGridItem18";
			this.oneGridItem18.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem18.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem18.TabIndex = 14;
			// 
			// bpPanelControl34
			// 
			this.bpPanelControl34.Controls.Add(this.m_lblUnitWeight);
			this.bpPanelControl34.Controls.Add(this.textBoxExt7);
			this.bpPanelControl34.Controls.Add(this.cbo중량단위);
			this.bpPanelControl34.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl34.Name = "bpPanelControl34";
			this.bpPanelControl34.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl34.TabIndex = 3;
			this.bpPanelControl34.Text = "bpPanelControl34";
			// 
			// m_lblUnitWeight
			// 
			this.m_lblUnitWeight.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUnitWeight.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUnitWeight.Location = new System.Drawing.Point(0, 3);
			this.m_lblUnitWeight.Name = "m_lblUnitWeight";
			this.m_lblUnitWeight.Size = new System.Drawing.Size(156, 16);
			this.m_lblUnitWeight.TabIndex = 140;
			this.m_lblUnitWeight.Tag = "UNIT_WEIGHT";
			this.m_lblUnitWeight.Text = "중량단위";
			this.m_lblUnitWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxExt7
			// 
			this.textBoxExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.textBoxExt7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.textBoxExt7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textBoxExt7.Location = new System.Drawing.Point(-137, -304);
			this.textBoxExt7.MaxLength = 20;
			this.textBoxExt7.Name = "textBoxExt7";
			this.textBoxExt7.Size = new System.Drawing.Size(200, 21);
			this.textBoxExt7.TabIndex = 0;
			this.textBoxExt7.Tag = "CD_ITEM";
			// 
			// cbo중량단위
			// 
			this.cbo중량단위.AutoDropDown = true;
			this.cbo중량단위.BackColor = System.Drawing.Color.White;
			this.cbo중량단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo중량단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo중량단위.ItemHeight = 12;
			this.cbo중량단위.Location = new System.Drawing.Point(157, 1);
			this.cbo중량단위.MaxLength = 3;
			this.cbo중량단위.Name = "cbo중량단위";
			this.cbo중량단위.Size = new System.Drawing.Size(70, 20);
			this.cbo중량단위.TabIndex = 24;
			this.cbo중량단위.Tag = "UNIT_WEIGHT";
			// 
			// bpPanelControl35
			// 
			this.bpPanelControl35.Controls.Add(this.m_lblWeight);
			this.bpPanelControl35.Controls.Add(this.cur중량);
			this.bpPanelControl35.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl35.Name = "bpPanelControl35";
			this.bpPanelControl35.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl35.TabIndex = 2;
			this.bpPanelControl35.Text = "bpPanelControl35";
			// 
			// m_lblWeight
			// 
			this.m_lblWeight.BackColor = System.Drawing.Color.Transparent;
			this.m_lblWeight.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblWeight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.m_lblWeight.Location = new System.Drawing.Point(0, 3);
			this.m_lblWeight.Name = "m_lblWeight";
			this.m_lblWeight.Size = new System.Drawing.Size(156, 16);
			this.m_lblWeight.TabIndex = 90;
			this.m_lblWeight.Tag = "WEIGHT";
			this.m_lblWeight.Text = "중량";
			this.m_lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur중량
			// 
			this.cur중량.BackColor = System.Drawing.Color.White;
			this.cur중량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur중량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur중량.CurrencyDecimalDigits = 4;
			this.cur중량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur중량.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur중량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur중량.Location = new System.Drawing.Point(157, 1);
			this.cur중량.MaxLength = 16;
			this.cur중량.Name = "cur중량";
			this.cur중량.NullString = "0";
			this.cur중량.PositiveColor = System.Drawing.Color.Black;
			this.cur중량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur중량.Size = new System.Drawing.Size(185, 21);
			this.cur중량.TabIndex = 23;
			this.cur중량.Tag = "WEIGHT";
			this.cur중량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem19
			// 
			this.oneGridItem19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem19.Controls.Add(this.bpPanelControl48);
			this.oneGridItem19.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem19.Location = new System.Drawing.Point(0, 345);
			this.oneGridItem19.Name = "oneGridItem19";
			this.oneGridItem19.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem19.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem19.TabIndex = 15;
			// 
			// bpPanelControl48
			// 
			this.bpPanelControl48.Controls.Add(this.m_lblNoDesign);
			this.bpPanelControl48.Controls.Add(this.cbo도면유무);
			this.bpPanelControl48.Controls.Add(this.txt도면번호);
			this.bpPanelControl48.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl48.Name = "bpPanelControl48";
			this.bpPanelControl48.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl48.TabIndex = 0;
			this.bpPanelControl48.Text = "bpPanelControl48";
			// 
			// m_lblNoDesign
			// 
			this.m_lblNoDesign.BackColor = System.Drawing.Color.Transparent;
			this.m_lblNoDesign.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblNoDesign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.m_lblNoDesign.Location = new System.Drawing.Point(0, 3);
			this.m_lblNoDesign.Name = "m_lblNoDesign";
			this.m_lblNoDesign.Size = new System.Drawing.Size(156, 16);
			this.m_lblNoDesign.TabIndex = 154;
			this.m_lblNoDesign.Tag = "NO_DESIGN";
			this.m_lblNoDesign.Text = "도면번호";
			this.m_lblNoDesign.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo도면유무
			// 
			this.cbo도면유무.AutoDropDown = true;
			this.cbo도면유무.BackColor = System.Drawing.Color.White;
			this.cbo도면유무.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo도면유무.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo도면유무.ItemHeight = 12;
			this.cbo도면유무.Location = new System.Drawing.Point(157, 1);
			this.cbo도면유무.MaxLength = 3;
			this.cbo도면유무.Name = "cbo도면유무";
			this.cbo도면유무.Size = new System.Drawing.Size(185, 20);
			this.cbo도면유무.TabIndex = 25;
			this.cbo도면유무.Tag = "FG_MODEL";
			// 
			// txt도면번호
			// 
			this.txt도면번호.BackColor = System.Drawing.Color.White;
			this.txt도면번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt도면번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt도면번호.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt도면번호.Location = new System.Drawing.Point(344, 1);
			this.txt도면번호.MaxLength = 20;
			this.txt도면번호.Name = "txt도면번호";
			this.txt도면번호.Size = new System.Drawing.Size(342, 21);
			this.txt도면번호.TabIndex = 26;
			this.txt도면번호.Tag = "NO_DESIGN";
			// 
			// oneGridItem20
			// 
			this.oneGridItem20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem20.Controls.Add(this.bpPanelControl49);
			this.oneGridItem20.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem20.Location = new System.Drawing.Point(0, 368);
			this.oneGridItem20.Name = "oneGridItem20";
			this.oneGridItem20.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem20.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem20.TabIndex = 16;
			// 
			// bpPanelControl49
			// 
			this.bpPanelControl49.Controls.Add(this.m_lblUrlItem);
			this.bpPanelControl49.Controls.Add(this.txt품목url);
			this.bpPanelControl49.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl49.Name = "bpPanelControl49";
			this.bpPanelControl49.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl49.TabIndex = 0;
			this.bpPanelControl49.Text = "bpPanelControl49";
			// 
			// m_lblUrlItem
			// 
			this.m_lblUrlItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUrlItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUrlItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblUrlItem.Name = "m_lblUrlItem";
			this.m_lblUrlItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblUrlItem.TabIndex = 95;
			this.m_lblUrlItem.Tag = "URL_ITEM";
			this.m_lblUrlItem.Text = "품목URL";
			this.m_lblUrlItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt품목url
			// 
			this.txt품목url.BackColor = System.Drawing.Color.White;
			this.txt품목url.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt품목url.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt품목url.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt품목url.Location = new System.Drawing.Point(157, 1);
			this.txt품목url.MaxLength = 40;
			this.txt품목url.Name = "txt품목url";
			this.txt품목url.Size = new System.Drawing.Size(529, 21);
			this.txt품목url.TabIndex = 27;
			this.txt품목url.Tag = "URL_ITEM";
			// 
			// oneGridItem21
			// 
			this.oneGridItem21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem21.Controls.Add(this.bpPanelControl36);
			this.oneGridItem21.Controls.Add(this.bpPanelControl37);
			this.oneGridItem21.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem21.Location = new System.Drawing.Point(0, 391);
			this.oneGridItem21.Name = "oneGridItem21";
			this.oneGridItem21.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem21.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem21.TabIndex = 17;
			// 
			// bpPanelControl36
			// 
			this.bpPanelControl36.Controls.Add(this.m_lblYnUse);
			this.bpPanelControl36.Controls.Add(this.cbo사용유무);
			this.bpPanelControl36.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl36.Name = "bpPanelControl36";
			this.bpPanelControl36.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl36.TabIndex = 3;
			this.bpPanelControl36.Text = "bpPanelControl36";
			// 
			// m_lblYnUse
			// 
			this.m_lblYnUse.BackColor = System.Drawing.Color.Transparent;
			this.m_lblYnUse.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblYnUse.Location = new System.Drawing.Point(0, 3);
			this.m_lblYnUse.Name = "m_lblYnUse";
			this.m_lblYnUse.Size = new System.Drawing.Size(156, 16);
			this.m_lblYnUse.TabIndex = 142;
			this.m_lblYnUse.Tag = "YN_USE";
			this.m_lblYnUse.Text = "사용유무";
			this.m_lblYnUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo사용유무
			// 
			this.cbo사용유무.AutoDropDown = true;
			this.cbo사용유무.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo사용유무.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용유무.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo사용유무.ItemHeight = 12;
			this.cbo사용유무.Location = new System.Drawing.Point(157, 1);
			this.cbo사용유무.MaxLength = 3;
			this.cbo사용유무.Name = "cbo사용유무";
			this.cbo사용유무.Size = new System.Drawing.Size(71, 20);
			this.cbo사용유무.TabIndex = 29;
			this.cbo사용유무.Tag = "YN_USE";
			// 
			// bpPanelControl37
			// 
			this.bpPanelControl37.Controls.Add(this.m_lblDtValid);
			this.bpPanelControl37.Controls.Add(this.dtp유효일);
			this.bpPanelControl37.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl37.Name = "bpPanelControl37";
			this.bpPanelControl37.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl37.TabIndex = 2;
			this.bpPanelControl37.Text = "bpPanelControl37";
			// 
			// m_lblDtValid
			// 
			this.m_lblDtValid.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDtValid.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDtValid.Location = new System.Drawing.Point(0, 3);
			this.m_lblDtValid.Name = "m_lblDtValid";
			this.m_lblDtValid.Size = new System.Drawing.Size(156, 16);
			this.m_lblDtValid.TabIndex = 97;
			this.m_lblDtValid.Tag = "DT_VALID";
			this.m_lblDtValid.Text = "유효일";
			this.m_lblDtValid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp유효일
			// 
			this.dtp유효일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp유효일.Location = new System.Drawing.Point(157, 1);
			this.dtp유효일.Mask = "####/##/##";
			this.dtp유효일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp유효일.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp유효일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp유효일.Name = "dtp유효일";
			this.dtp유효일.Size = new System.Drawing.Size(91, 21);
			this.dtp유효일.TabIndex = 28;
			this.dtp유효일.Tag = "DT_VALID";
			this.dtp유효일.Value = new System.DateTime(((long)(0)));
			// 
			// oneGridItem22
			// 
			this.oneGridItem22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem22.Controls.Add(this.bpPanelControl38);
			this.oneGridItem22.Controls.Add(this.bpPanelControl39);
			this.oneGridItem22.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem22.Location = new System.Drawing.Point(0, 414);
			this.oneGridItem22.Name = "oneGridItem22";
			this.oneGridItem22.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem22.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem22.TabIndex = 18;
			// 
			// bpPanelControl38
			// 
			this.bpPanelControl38.Controls.Add(this.lbl등록자);
			this.bpPanelControl38.Controls.Add(this.ctx등록자);
			this.bpPanelControl38.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl38.Name = "bpPanelControl38";
			this.bpPanelControl38.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl38.TabIndex = 3;
			this.bpPanelControl38.Text = "bpPanelControl38";
			// 
			// lbl등록자
			// 
			this.lbl등록자.BackColor = System.Drawing.Color.Transparent;
			this.lbl등록자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl등록자.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl등록자.Location = new System.Drawing.Point(0, 3);
			this.lbl등록자.Name = "lbl등록자";
			this.lbl등록자.Size = new System.Drawing.Size(156, 16);
			this.lbl등록자.TabIndex = 171;
			this.lbl등록자.Text = "등록자";
			this.lbl등록자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx등록자
			// 
			this.ctx등록자.CodeName = null;
			this.ctx등록자.CodeValue = null;
			this.ctx등록자.Enabled = false;
			this.ctx등록자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx등록자.Location = new System.Drawing.Point(157, 1);
			this.ctx등록자.Name = "ctx등록자";
			this.ctx등록자.Size = new System.Drawing.Size(185, 21);
			this.ctx등록자.TabIndex = 194;
			this.ctx등록자.TabStop = false;
			this.ctx등록자.Tag = "INSERT_ID,NM_INSERT_ID";
			// 
			// bpPanelControl39
			// 
			this.bpPanelControl39.Controls.Add(this.lbl등록일);
			this.bpPanelControl39.Controls.Add(this.dtp등록일);
			this.bpPanelControl39.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl39.Name = "bpPanelControl39";
			this.bpPanelControl39.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl39.TabIndex = 2;
			this.bpPanelControl39.Text = "bpPanelControl39";
			// 
			// lbl등록일
			// 
			this.lbl등록일.BackColor = System.Drawing.Color.Transparent;
			this.lbl등록일.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl등록일.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl등록일.Location = new System.Drawing.Point(0, 3);
			this.lbl등록일.Name = "lbl등록일";
			this.lbl등록일.Size = new System.Drawing.Size(156, 16);
			this.lbl등록일.TabIndex = 171;
			this.lbl등록일.Text = "등록일";
			this.lbl등록일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp등록일
			// 
			this.dtp등록일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp등록일.Enabled = false;
			this.dtp등록일.Location = new System.Drawing.Point(157, 1);
			this.dtp등록일.Mask = "####/##/##";
			this.dtp등록일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp등록일.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp등록일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp등록일.Name = "dtp등록일";
			this.dtp등록일.Size = new System.Drawing.Size(91, 21);
			this.dtp등록일.TabIndex = 193;
			this.dtp등록일.Tag = "DTS_INSERT";
			this.dtp등록일.Value = new System.DateTime(((long)(0)));
			// 
			// oneGridItem23
			// 
			this.oneGridItem23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem23.Controls.Add(this.bpPanelControl40);
			this.oneGridItem23.Controls.Add(this.bpPanelControl41);
			this.oneGridItem23.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem23.Location = new System.Drawing.Point(0, 437);
			this.oneGridItem23.Name = "oneGridItem23";
			this.oneGridItem23.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem23.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem23.TabIndex = 19;
			// 
			// bpPanelControl40
			// 
			this.bpPanelControl40.Controls.Add(this.lbl수정자);
			this.bpPanelControl40.Controls.Add(this.ctx수정자);
			this.bpPanelControl40.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl40.Name = "bpPanelControl40";
			this.bpPanelControl40.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl40.TabIndex = 3;
			this.bpPanelControl40.Text = "bpPanelControl40";
			// 
			// lbl수정자
			// 
			this.lbl수정자.BackColor = System.Drawing.Color.Transparent;
			this.lbl수정자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl수정자.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl수정자.Location = new System.Drawing.Point(0, 3);
			this.lbl수정자.Name = "lbl수정자";
			this.lbl수정자.Size = new System.Drawing.Size(156, 16);
			this.lbl수정자.TabIndex = 172;
			this.lbl수정자.Text = "수정자";
			this.lbl수정자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx수정자
			// 
			this.ctx수정자.CodeName = null;
			this.ctx수정자.CodeValue = null;
			this.ctx수정자.Enabled = false;
			this.ctx수정자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEM_SUB;
			this.ctx수정자.Location = new System.Drawing.Point(157, 1);
			this.ctx수정자.Name = "ctx수정자";
			this.ctx수정자.Size = new System.Drawing.Size(185, 21);
			this.ctx수정자.TabIndex = 195;
			this.ctx수정자.TabStop = false;
			this.ctx수정자.Tag = "UPDATE_ID,NM_UPDATE_ID";
			// 
			// bpPanelControl41
			// 
			this.bpPanelControl41.Controls.Add(this.lbl수정일);
			this.bpPanelControl41.Controls.Add(this.dtp수정일);
			this.bpPanelControl41.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl41.Name = "bpPanelControl41";
			this.bpPanelControl41.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl41.TabIndex = 2;
			this.bpPanelControl41.Text = "bpPanelControl41";
			// 
			// lbl수정일
			// 
			this.lbl수정일.BackColor = System.Drawing.Color.Transparent;
			this.lbl수정일.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl수정일.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl수정일.Location = new System.Drawing.Point(0, 3);
			this.lbl수정일.Name = "lbl수정일";
			this.lbl수정일.Size = new System.Drawing.Size(156, 16);
			this.lbl수정일.TabIndex = 172;
			this.lbl수정일.Text = "수정일";
			this.lbl수정일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp수정일
			// 
			this.dtp수정일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp수정일.Enabled = false;
			this.dtp수정일.Location = new System.Drawing.Point(157, 1);
			this.dtp수정일.Mask = "####/##/##";
			this.dtp수정일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp수정일.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp수정일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp수정일.Name = "dtp수정일";
			this.dtp수정일.Size = new System.Drawing.Size(91, 21);
			this.dtp수정일.TabIndex = 196;
			this.dtp수정일.Tag = "DTS_UPDATE";
			this.dtp수정일.Value = new System.DateTime(((long)(0)));
			// 
			// oneGridItem24
			// 
			this.oneGridItem24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem24.Controls.Add(this.bpPanelControl42);
			this.oneGridItem24.Controls.Add(this.bpPanelControl43);
			this.oneGridItem24.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem24.Location = new System.Drawing.Point(0, 460);
			this.oneGridItem24.Name = "oneGridItem24";
			this.oneGridItem24.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem24.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem24.TabIndex = 20;
			// 
			// bpPanelControl42
			// 
			this.bpPanelControl42.Controls.Add(this.lbl길이);
			this.bpPanelControl42.Controls.Add(this.cur길이);
			this.bpPanelControl42.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl42.Name = "bpPanelControl42";
			this.bpPanelControl42.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl42.TabIndex = 3;
			this.bpPanelControl42.Text = "bpPanelControl42";
			// 
			// lbl길이
			// 
			this.lbl길이.BackColor = System.Drawing.Color.Transparent;
			this.lbl길이.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl길이.Location = new System.Drawing.Point(0, 3);
			this.lbl길이.Name = "lbl길이";
			this.lbl길이.Size = new System.Drawing.Size(156, 16);
			this.lbl길이.TabIndex = 164;
			this.lbl길이.Text = "길이";
			this.lbl길이.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl길이.Visible = false;
			// 
			// cur길이
			// 
			this.cur길이.BackColor = System.Drawing.Color.White;
			this.cur길이.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur길이.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur길이.CurrencyDecimalDigits = 4;
			this.cur길이.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur길이.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur길이.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur길이.Location = new System.Drawing.Point(157, 1);
			this.cur길이.MaxLength = 16;
			this.cur길이.Name = "cur길이";
			this.cur길이.NullString = "0";
			this.cur길이.PositiveColor = System.Drawing.Color.Black;
			this.cur길이.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur길이.Size = new System.Drawing.Size(185, 21);
			this.cur길이.TabIndex = 161;
			this.cur길이.Tag = "QT_LENGTH";
			this.cur길이.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur길이.Visible = false;
			// 
			// bpPanelControl43
			// 
			this.bpPanelControl43.Controls.Add(this.lbl폭);
			this.bpPanelControl43.Controls.Add(this.cur폭);
			this.bpPanelControl43.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl43.Name = "bpPanelControl43";
			this.bpPanelControl43.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl43.TabIndex = 2;
			this.bpPanelControl43.Text = "bpPanelControl43";
			// 
			// lbl폭
			// 
			this.lbl폭.BackColor = System.Drawing.Color.Transparent;
			this.lbl폭.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl폭.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl폭.Location = new System.Drawing.Point(0, 3);
			this.lbl폭.Name = "lbl폭";
			this.lbl폭.Size = new System.Drawing.Size(156, 16);
			this.lbl폭.TabIndex = 162;
			this.lbl폭.Text = "폭";
			this.lbl폭.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl폭.Visible = false;
			// 
			// cur폭
			// 
			this.cur폭.BackColor = System.Drawing.Color.White;
			this.cur폭.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur폭.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur폭.CurrencyDecimalDigits = 4;
			this.cur폭.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur폭.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur폭.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur폭.Location = new System.Drawing.Point(157, 1);
			this.cur폭.MaxLength = 16;
			this.cur폭.Name = "cur폭";
			this.cur폭.NullString = "0";
			this.cur폭.PositiveColor = System.Drawing.Color.Black;
			this.cur폭.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur폭.Size = new System.Drawing.Size(185, 21);
			this.cur폭.TabIndex = 160;
			this.cur폭.Tag = "QT_WIDTH";
			this.cur폭.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur폭.Visible = false;
			// 
			// oneGridItem25
			// 
			this.oneGridItem25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem25.Controls.Add(this.bpPanelControl44);
			this.oneGridItem25.Controls.Add(this.bpPanelControl45);
			this.oneGridItem25.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem25.Location = new System.Drawing.Point(0, 483);
			this.oneGridItem25.Name = "oneGridItem25";
			this.oneGridItem25.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem25.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem25.TabIndex = 21;
			// 
			// bpPanelControl44
			// 
			this.bpPanelControl44.Controls.Add(this.lbl매입형태);
			this.bpPanelControl44.Controls.Add(this.cbo매입형태);
			this.bpPanelControl44.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl44.Name = "bpPanelControl44";
			this.bpPanelControl44.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl44.TabIndex = 3;
			this.bpPanelControl44.Text = "bpPanelControl44";
			// 
			// lbl매입형태
			// 
			this.lbl매입형태.BackColor = System.Drawing.Color.Transparent;
			this.lbl매입형태.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl매입형태.Location = new System.Drawing.Point(0, 3);
			this.lbl매입형태.Name = "lbl매입형태";
			this.lbl매입형태.Size = new System.Drawing.Size(156, 16);
			this.lbl매입형태.TabIndex = 165;
			this.lbl매입형태.Text = "매입형태";
			this.lbl매입형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl매입형태.Visible = false;
			// 
			// cbo매입형태
			// 
			this.cbo매입형태.AutoDropDown = true;
			this.cbo매입형태.BackColor = System.Drawing.Color.White;
			this.cbo매입형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo매입형태.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo매입형태.ItemHeight = 12;
			this.cbo매입형태.Location = new System.Drawing.Point(157, 1);
			this.cbo매입형태.MaxLength = 3;
			this.cbo매입형태.Name = "cbo매입형태";
			this.cbo매입형태.Size = new System.Drawing.Size(185, 20);
			this.cbo매입형태.TabIndex = 163;
			this.cbo매입형태.Tag = "CD_TP";
			this.cbo매입형태.Visible = false;
			// 
			// bpPanelControl45
			// 
			this.bpPanelControl45.Controls.Add(this.lbl코어코드);
			this.bpPanelControl45.Controls.Add(this.bp코어코드);
			this.bpPanelControl45.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl45.Name = "bpPanelControl45";
			this.bpPanelControl45.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl45.TabIndex = 2;
			this.bpPanelControl45.Text = "bpPanelControl45";
			// 
			// lbl코어코드
			// 
			this.lbl코어코드.BackColor = System.Drawing.Color.Transparent;
			this.lbl코어코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl코어코드.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl코어코드.Location = new System.Drawing.Point(0, 3);
			this.lbl코어코드.Name = "lbl코어코드";
			this.lbl코어코드.Size = new System.Drawing.Size(156, 16);
			this.lbl코어코드.TabIndex = 163;
			this.lbl코어코드.Text = "코어코드";
			this.lbl코어코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl코어코드.Visible = false;
			// 
			// bp코어코드
			// 
			this.bp코어코드.CodeName = null;
			this.bp코어코드.CodeValue = null;
			this.bp코어코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.bp코어코드.Location = new System.Drawing.Point(157, 1);
			this.bp코어코드.Name = "bp코어코드";
			this.bp코어코드.Size = new System.Drawing.Size(185, 21);
			this.bp코어코드.TabIndex = 162;
			this.bp코어코드.TabStop = false;
			this.bp코어코드.Tag = "CD_CORE,NM_CORE";
			this.bp코어코드.Visible = false;
			// 
			// oneGridItem26
			// 
			this.oneGridItem26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem26.Controls.Add(this.bpPanelControl46);
			this.oneGridItem26.Controls.Add(this.bpPanelControl47);
			this.oneGridItem26.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem26.Location = new System.Drawing.Point(0, 506);
			this.oneGridItem26.Name = "oneGridItem26";
			this.oneGridItem26.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem26.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem26.TabIndex = 22;
			// 
			// bpPanelControl46
			// 
			this.bpPanelControl46.Controls.Add(this.lblcc코드);
			this.bpPanelControl46.Controls.Add(this.ctxcc);
			this.bpPanelControl46.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl46.Name = "bpPanelControl46";
			this.bpPanelControl46.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl46.TabIndex = 3;
			this.bpPanelControl46.Text = "bpPanelControl46";
			// 
			// lblcc코드
			// 
			this.lblcc코드.BackColor = System.Drawing.Color.Transparent;
			this.lblcc코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblcc코드.Location = new System.Drawing.Point(0, 3);
			this.lblcc코드.Name = "lblcc코드";
			this.lblcc코드.Size = new System.Drawing.Size(156, 16);
			this.lblcc코드.TabIndex = 142;
			this.lblcc코드.Text = "C/C";
			this.lblcc코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblcc코드.Visible = false;
			// 
			// ctxcc
			// 
			this.ctxcc.CodeName = null;
			this.ctxcc.CodeValue = null;
			this.ctxcc.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB;
			this.ctxcc.Location = new System.Drawing.Point(157, 1);
			this.ctxcc.Name = "ctxcc";
			this.ctxcc.Size = new System.Drawing.Size(185, 21);
			this.ctxcc.TabIndex = 176;
			this.ctxcc.TabStop = false;
			this.ctxcc.Tag = "CD_CC,NM_CC";
			this.ctxcc.Visible = false;
			// 
			// bpPanelControl47
			// 
			this.bpPanelControl47.Controls.Add(this.lbl시작일);
			this.bpPanelControl47.Controls.Add(this.dtp시작일);
			this.bpPanelControl47.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl47.Name = "bpPanelControl47";
			this.bpPanelControl47.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl47.TabIndex = 2;
			this.bpPanelControl47.Text = "bpPanelControl47";
			// 
			// lbl시작일
			// 
			this.lbl시작일.BackColor = System.Drawing.Color.Transparent;
			this.lbl시작일.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl시작일.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl시작일.Location = new System.Drawing.Point(0, 3);
			this.lbl시작일.Name = "lbl시작일";
			this.lbl시작일.Size = new System.Drawing.Size(156, 16);
			this.lbl시작일.TabIndex = 171;
			this.lbl시작일.Text = "시작일";
			this.lbl시작일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl시작일.Visible = false;
			// 
			// dtp시작일
			// 
			this.dtp시작일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp시작일.Location = new System.Drawing.Point(157, 1);
			this.dtp시작일.Mask = "####/##/##";
			this.dtp시작일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp시작일.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp시작일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp시작일.Name = "dtp시작일";
			this.dtp시작일.Size = new System.Drawing.Size(91, 21);
			this.dtp시작일.TabIndex = 172;
			this.dtp시작일.Tag = "DT_ST";
			this.dtp시작일.Value = new System.DateTime(((long)(0)));
			this.dtp시작일.Visible = false;
			// 
			// oneGridItem101
			// 
			this.oneGridItem101.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem101.Controls.Add(this.bpPanelControl174);
			this.oneGridItem101.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem101.Location = new System.Drawing.Point(0, 529);
			this.oneGridItem101.Name = "oneGridItem101";
			this.oneGridItem101.Size = new System.Drawing.Size(750, 60);
			this.oneGridItem101.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem101.TabIndex = 23;
			// 
			// bpPanelControl174
			// 
			this.bpPanelControl174.Controls.Add(this.txt_기본_비고);
			this.bpPanelControl174.Controls.Add(this.lbl기본텝비고);
			this.bpPanelControl174.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl174.Name = "bpPanelControl174";
			this.bpPanelControl174.Size = new System.Drawing.Size(726, 56);
			this.bpPanelControl174.TabIndex = 3;
			this.bpPanelControl174.Text = "bpPanelControl174";
			// 
			// txt_기본_비고
			// 
			this.txt_기본_비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt_기본_비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt_기본_비고.Location = new System.Drawing.Point(156, 2);
			this.txt_기본_비고.Multiline = true;
			this.txt_기본_비고.Name = "txt_기본_비고";
			this.txt_기본_비고.Size = new System.Drawing.Size(531, 50);
			this.txt_기본_비고.TabIndex = 190;
			this.txt_기본_비고.Tag = "DC_RMK";
			// 
			// lbl기본텝비고
			// 
			this.lbl기본텝비고.BackColor = System.Drawing.Color.Transparent;
			this.lbl기본텝비고.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl기본텝비고.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl기본텝비고.Location = new System.Drawing.Point(76, 3);
			this.lbl기본텝비고.Name = "lbl기본텝비고";
			this.lbl기본텝비고.Size = new System.Drawing.Size(80, 16);
			this.lbl기본텝비고.TabIndex = 162;
			this.lbl기본텝비고.Text = "비고";
			this.lbl기본텝비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpg자재
			// 
			this.tpg자재.BackColor = System.Drawing.Color.White;
			this.tpg자재.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg자재.Controls.Add(this.pnl자재);
			this.tpg자재.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tpg자재.ImageIndex = 3;
			this.tpg자재.Location = new System.Drawing.Point(4, 24);
			this.tpg자재.Name = "tpg자재";
			this.tpg자재.Padding = new System.Windows.Forms.Padding(4);
			this.tpg자재.Size = new System.Drawing.Size(772, 626);
			this.tpg자재.TabIndex = 1;
			this.tpg자재.Tag = "MATERIAL";
			this.tpg자재.Text = "자재";
			this.tpg자재.UseVisualStyleBackColor = true;
			// 
			// pnl자재
			// 
			this.pnl자재.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnl자재.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem27,
            this.oneGridItem28,
            this.oneGridItem29,
            this.oneGridItem30,
            this.oneGridItem31,
            this.oneGridItem32,
            this.oneGridItem33,
            this.oneGridItem34,
            this.oneGridItem35,
            this.oneGridItem36,
            this.oneGridItem37,
            this.oneGridItem38,
            this.oneGridItem39,
            this.oneGridItem40,
            this.oneGridItem103});
			this.pnl자재.Location = new System.Drawing.Point(4, 4);
			this.pnl자재.Name = "pnl자재";
			this.pnl자재.Size = new System.Drawing.Size(760, 590);
			this.pnl자재.TabIndex = 1;
			// 
			// oneGridItem27
			// 
			this.oneGridItem27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem27.Controls.Add(this._bpCdItemR);
			this.oneGridItem27.Controls.Add(this.bpPanelControl50);
			this.oneGridItem27.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem27.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem27.Name = "oneGridItem27";
			this.oneGridItem27.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem27.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem27.TabIndex = 0;
			// 
			// _bpCdItemR
			// 
			this._bpCdItemR.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this._bpCdItemR.LabelVisibled = true;
			this._bpCdItemR.LabelWidth = 156;
			this._bpCdItemR.Location = new System.Drawing.Point(346, 1);
			this._bpCdItemR.Name = "_bpCdItemR";
			this._bpCdItemR.Size = new System.Drawing.Size(342, 21);
			this._bpCdItemR.TabIndex = 227;
			this._bpCdItemR.TabStop = false;
			this._bpCdItemR.Tag = "CD_ITEM_RELATION,NM_ITEM_RELATION";
			this._bpCdItemR.Text = "관련품목";
			// 
			// bpPanelControl50
			// 
			this.bpPanelControl50.Controls.Add(this.m_lblCdPurGrp);
			this.bpPanelControl50.Controls.Add(this.ctx구매그룹);
			this.bpPanelControl50.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl50.Name = "bpPanelControl50";
			this.bpPanelControl50.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl50.TabIndex = 0;
			this.bpPanelControl50.Text = "bpPanelControl50";
			// 
			// m_lblCdPurGrp
			// 
			this.m_lblCdPurGrp.BackColor = System.Drawing.Color.Transparent;
			this.m_lblCdPurGrp.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblCdPurGrp.Location = new System.Drawing.Point(0, 3);
			this.m_lblCdPurGrp.Name = "m_lblCdPurGrp";
			this.m_lblCdPurGrp.Size = new System.Drawing.Size(156, 16);
			this.m_lblCdPurGrp.TabIndex = 38;
			this.m_lblCdPurGrp.Tag = "CD_PURGRP";
			this.m_lblCdPurGrp.Text = "구매그룹";
			this.m_lblCdPurGrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx구매그룹
			// 
			this.ctx구매그룹.CodeName = null;
			this.ctx구매그룹.CodeValue = null;
			this.ctx구매그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
			this.ctx구매그룹.Location = new System.Drawing.Point(157, 1);
			this.ctx구매그룹.Name = "ctx구매그룹";
			this.ctx구매그룹.Size = new System.Drawing.Size(185, 21);
			this.ctx구매그룹.TabIndex = 0;
			this.ctx구매그룹.TabStop = false;
			this.ctx구매그룹.Tag = "CD_PURGRP,NM_PURGRP";
			// 
			// oneGridItem28
			// 
			this.oneGridItem28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem28.Controls.Add(this.bpPanelControl52);
			this.oneGridItem28.Controls.Add(this.bpPanelControl53);
			this.oneGridItem28.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem28.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem28.Name = "oneGridItem28";
			this.oneGridItem28.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem28.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem28.TabIndex = 1;
			// 
			// bpPanelControl52
			// 
			this.bpPanelControl52.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl52.Name = "bpPanelControl52";
			this.bpPanelControl52.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl52.TabIndex = 3;
			this.bpPanelControl52.Text = "bpPanelControl52";
			// 
			// bpPanelControl53
			// 
			this.bpPanelControl53.Controls.Add(this.m_lblQtSstock);
			this.bpPanelControl53.Controls.Add(this.cur안전재고);
			this.bpPanelControl53.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl53.Name = "bpPanelControl53";
			this.bpPanelControl53.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl53.TabIndex = 2;
			this.bpPanelControl53.Text = "bpPanelControl53";
			// 
			// m_lblQtSstock
			// 
			this.m_lblQtSstock.BackColor = System.Drawing.Color.Transparent;
			this.m_lblQtSstock.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblQtSstock.Location = new System.Drawing.Point(0, 3);
			this.m_lblQtSstock.Name = "m_lblQtSstock";
			this.m_lblQtSstock.Size = new System.Drawing.Size(156, 16);
			this.m_lblQtSstock.TabIndex = 39;
			this.m_lblQtSstock.Tag = "QT_SSTOCK";
			this.m_lblQtSstock.Text = "안전재고";
			this.m_lblQtSstock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur안전재고
			// 
			this.cur안전재고.BackColor = System.Drawing.Color.White;
			this.cur안전재고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur안전재고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur안전재고.CurrencyDecimalDigits = 4;
			this.cur안전재고.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur안전재고.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur안전재고.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur안전재고.Location = new System.Drawing.Point(157, 1);
			this.cur안전재고.MaxLength = 16;
			this.cur안전재고.Name = "cur안전재고";
			this.cur안전재고.NullString = "0";
			this.cur안전재고.PositiveColor = System.Drawing.Color.Black;
			this.cur안전재고.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur안전재고.Size = new System.Drawing.Size(185, 21);
			this.cur안전재고.TabIndex = 1;
			this.cur안전재고.Tag = "QT_SSTOCK";
			this.cur안전재고.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem29
			// 
			this.oneGridItem29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem29.Controls.Add(this.bpPanelControl54);
			this.oneGridItem29.Controls.Add(this.bpPanelControl55);
			this.oneGridItem29.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem29.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem29.Name = "oneGridItem29";
			this.oneGridItem29.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem29.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem29.TabIndex = 2;
			// 
			// bpPanelControl54
			// 
			this.bpPanelControl54.Controls.Add(this.labelExt7);
			this.bpPanelControl54.Controls.Add(this.cboATP적용여부);
			this.bpPanelControl54.Controls.Add(this.labelExt8);
			this.bpPanelControl54.Controls.Add(this.cur허용일);
			this.bpPanelControl54.Controls.Add(this.labelExt9);
			this.bpPanelControl54.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl54.Name = "bpPanelControl54";
			this.bpPanelControl54.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl54.TabIndex = 3;
			this.bpPanelControl54.Text = "bpPanelControl54";
			// 
			// labelExt7
			// 
			this.labelExt7.BackColor = System.Drawing.Color.Transparent;
			this.labelExt7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt7.ForeColor = System.Drawing.Color.Black;
			this.labelExt7.Location = new System.Drawing.Point(0, 3);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(156, 16);
			this.labelExt7.TabIndex = 3;
			this.labelExt7.Tag = "CD_ZONE";
			this.labelExt7.Text = "ATP적용여부";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboATP적용여부
			// 
			this.cboATP적용여부.AutoDropDown = true;
			this.cboATP적용여부.BackColor = System.Drawing.Color.White;
			this.cboATP적용여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboATP적용여부.ItemHeight = 12;
			this.cboATP적용여부.Location = new System.Drawing.Point(157, 1);
			this.cboATP적용여부.Name = "cboATP적용여부";
			this.cboATP적용여부.Size = new System.Drawing.Size(46, 20);
			this.cboATP적용여부.TabIndex = 175;
			this.cboATP적용여부.Tag = "YN_ATP";
			// 
			// labelExt8
			// 
			this.labelExt8.BackColor = System.Drawing.Color.Transparent;
			this.labelExt8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt8.ForeColor = System.Drawing.Color.Black;
			this.labelExt8.Location = new System.Drawing.Point(204, 3);
			this.labelExt8.Name = "labelExt8";
			this.labelExt8.Size = new System.Drawing.Size(87, 16);
			this.labelExt8.TabIndex = 176;
			this.labelExt8.Tag = "CD_ZONE";
			this.labelExt8.Text = "허용일";
			this.labelExt8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur허용일
			// 
			this.cur허용일.BackColor = System.Drawing.Color.White;
			this.cur허용일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur허용일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur허용일.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur허용일.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur허용일.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur허용일.Location = new System.Drawing.Point(291, 1);
			this.cur허용일.MaxLength = 3;
			this.cur허용일.Name = "cur허용일";
			this.cur허용일.NullString = "0";
			this.cur허용일.PositiveColor = System.Drawing.Color.Black;
			this.cur허용일.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur허용일.Size = new System.Drawing.Size(32, 21);
			this.cur허용일.TabIndex = 4;
			this.cur허용일.Tag = "CUR_ATP_DAY";
			this.cur허용일.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelExt9
			// 
			this.labelExt9.BackColor = System.Drawing.Color.Transparent;
			this.labelExt9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt9.Location = new System.Drawing.Point(326, 3);
			this.labelExt9.Name = "labelExt9";
			this.labelExt9.Size = new System.Drawing.Size(14, 18);
			this.labelExt9.TabIndex = 178;
			this.labelExt9.Tag = "DAY";
			this.labelExt9.Text = "일";
			this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// bpPanelControl55
			// 
			this.bpPanelControl55.Controls.Add(this.m_lblSageLt);
			this.bpPanelControl55.Controls.Add(this.cur안전LT);
			this.bpPanelControl55.Controls.Add(this.m_lblDy1);
			this.bpPanelControl55.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl55.Name = "bpPanelControl55";
			this.bpPanelControl55.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl55.TabIndex = 2;
			this.bpPanelControl55.Text = "bpPanelControl55";
			// 
			// m_lblSageLt
			// 
			this.m_lblSageLt.BackColor = System.Drawing.Color.Transparent;
			this.m_lblSageLt.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblSageLt.Location = new System.Drawing.Point(0, 3);
			this.m_lblSageLt.Name = "m_lblSageLt";
			this.m_lblSageLt.Size = new System.Drawing.Size(156, 16);
			this.m_lblSageLt.TabIndex = 56;
			this.m_lblSageLt.Tag = "SAFELT";
			this.m_lblSageLt.Text = "안전L/T";
			this.m_lblSageLt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur안전LT
			// 
			this.cur안전LT.BackColor = System.Drawing.Color.White;
			this.cur안전LT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur안전LT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur안전LT.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur안전LT.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur안전LT.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur안전LT.Location = new System.Drawing.Point(157, 1);
			this.cur안전LT.MaxLength = 3;
			this.cur안전LT.Name = "cur안전LT";
			this.cur안전LT.NullString = "0";
			this.cur안전LT.PositiveColor = System.Drawing.Color.Black;
			this.cur안전LT.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur안전LT.Size = new System.Drawing.Size(161, 21);
			this.cur안전LT.TabIndex = 2;
			this.cur안전LT.Tag = "LT_SAFE";
			this.cur안전LT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_lblDy1
			// 
			this.m_lblDy1.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDy1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDy1.Location = new System.Drawing.Point(324, 3);
			this.m_lblDy1.Name = "m_lblDy1";
			this.m_lblDy1.Size = new System.Drawing.Size(16, 18);
			this.m_lblDy1.TabIndex = 59;
			this.m_lblDy1.Tag = "DAY";
			this.m_lblDy1.Text = "일";
			this.m_lblDy1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// oneGridItem30
			// 
			this.oneGridItem30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem30.Controls.Add(this.bpPanelControl56);
			this.oneGridItem30.Controls.Add(this.bpPanelControl57);
			this.oneGridItem30.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem30.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem30.Name = "oneGridItem30";
			this.oneGridItem30.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem30.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem30.TabIndex = 3;
			// 
			// bpPanelControl56
			// 
			this.bpPanelControl56.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl56.Name = "bpPanelControl56";
			this.bpPanelControl56.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl56.TabIndex = 3;
			this.bpPanelControl56.Text = "bpPanelControl56";
			// 
			// bpPanelControl57
			// 
			this.bpPanelControl57.Controls.Add(this.m_lblFgAbc);
			this.bpPanelControl57.Controls.Add(this.cboABC구분);
			this.bpPanelControl57.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl57.Name = "bpPanelControl57";
			this.bpPanelControl57.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl57.TabIndex = 2;
			this.bpPanelControl57.Text = "bpPanelControl57";
			// 
			// m_lblFgAbc
			// 
			this.m_lblFgAbc.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgAbc.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgAbc.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgAbc.Name = "m_lblFgAbc";
			this.m_lblFgAbc.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgAbc.TabIndex = 40;
			this.m_lblFgAbc.Tag = "FG_ABC";
			this.m_lblFgAbc.Text = "ABC구분";
			this.m_lblFgAbc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboABC구분
			// 
			this.cboABC구분.AutoDropDown = true;
			this.cboABC구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cboABC구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboABC구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cboABC구분.ItemHeight = 12;
			this.cboABC구분.Location = new System.Drawing.Point(157, 1);
			this.cboABC구분.Name = "cboABC구분";
			this.cboABC구분.Size = new System.Drawing.Size(185, 20);
			this.cboABC구분.TabIndex = 5;
			this.cboABC구분.Tag = "FG_ABC";
			// 
			// oneGridItem31
			// 
			this.oneGridItem31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem31.Controls.Add(this.bpPanelControl58);
			this.oneGridItem31.Controls.Add(this.bpPanelControl59);
			this.oneGridItem31.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem31.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem31.Name = "oneGridItem31";
			this.oneGridItem31.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem31.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem31.TabIndex = 4;
			// 
			// bpPanelControl58
			// 
			this.bpPanelControl58.Controls.Add(this.lbl최종재고실사일);
			this.bpPanelControl58.Controls.Add(this.dtp최종재고실사일);
			this.bpPanelControl58.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl58.Name = "bpPanelControl58";
			this.bpPanelControl58.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl58.TabIndex = 3;
			this.bpPanelControl58.Text = "bpPanelControl58";
			// 
			// lbl최종재고실사일
			// 
			this.lbl최종재고실사일.BackColor = System.Drawing.Color.Transparent;
			this.lbl최종재고실사일.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl최종재고실사일.ForeColor = System.Drawing.Color.Black;
			this.lbl최종재고실사일.Location = new System.Drawing.Point(0, 3);
			this.lbl최종재고실사일.Name = "lbl최종재고실사일";
			this.lbl최종재고실사일.Size = new System.Drawing.Size(156, 16);
			this.lbl최종재고실사일.TabIndex = 158;
			this.lbl최종재고실사일.Tag = "DT_IMMNG";
			this.lbl최종재고실사일.Text = "최종재고실사일";
			this.lbl최종재고실사일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp최종재고실사일
			// 
			this.dtp최종재고실사일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp최종재고실사일.Location = new System.Drawing.Point(157, 1);
			this.dtp최종재고실사일.Mask = "####/##/##";
			this.dtp최종재고실사일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp최종재고실사일.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp최종재고실사일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp최종재고실사일.Name = "dtp최종재고실사일";
			this.dtp최종재고실사일.Size = new System.Drawing.Size(89, 21);
			this.dtp최종재고실사일.TabIndex = 9;
			this.dtp최종재고실사일.Tag = "DT_IMMNG";
			this.dtp최종재고실사일.Value = new System.DateTime(((long)(0)));
			// 
			// bpPanelControl59
			// 
			this.bpPanelControl59.Controls.Add(this.m_lblCdSl);
			this.bpPanelControl59.Controls.Add(this.ctx입고SL);
			this.bpPanelControl59.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl59.Name = "bpPanelControl59";
			this.bpPanelControl59.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl59.TabIndex = 2;
			this.bpPanelControl59.Text = "bpPanelControl59";
			// 
			// m_lblCdSl
			// 
			this.m_lblCdSl.BackColor = System.Drawing.Color.Transparent;
			this.m_lblCdSl.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblCdSl.Location = new System.Drawing.Point(0, 3);
			this.m_lblCdSl.Name = "m_lblCdSl";
			this.m_lblCdSl.Size = new System.Drawing.Size(156, 16);
			this.m_lblCdSl.TabIndex = 63;
			this.m_lblCdSl.Tag = "CD_GRSL";
			this.m_lblCdSl.Text = "입고S/L";
			this.m_lblCdSl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx입고SL
			// 
			this.ctx입고SL.CodeName = null;
			this.ctx입고SL.CodeValue = null;
			this.ctx입고SL.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
			this.ctx입고SL.Location = new System.Drawing.Point(157, 1);
			this.ctx입고SL.Name = "ctx입고SL";
			this.ctx입고SL.Size = new System.Drawing.Size(185, 21);
			this.ctx입고SL.TabIndex = 6;
			this.ctx입고SL.TabStop = false;
			this.ctx입고SL.Tag = "CD_SL,NM_SL";
			// 
			// oneGridItem32
			// 
			this.oneGridItem32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem32.Controls.Add(this.bpPanelControl60);
			this.oneGridItem32.Controls.Add(this.bpPanelControl61);
			this.oneGridItem32.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem32.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem32.Name = "oneGridItem32";
			this.oneGridItem32.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem32.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem32.TabIndex = 5;
			// 
			// bpPanelControl60
			// 
			this.bpPanelControl60.Controls.Add(this.lbl로케이션);
			this.bpPanelControl60.Controls.Add(this.txt로케이션);
			this.bpPanelControl60.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl60.Name = "bpPanelControl60";
			this.bpPanelControl60.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl60.TabIndex = 3;
			this.bpPanelControl60.Text = "bpPanelControl60";
			// 
			// lbl로케이션
			// 
			this.lbl로케이션.BackColor = System.Drawing.Color.Transparent;
			this.lbl로케이션.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl로케이션.ForeColor = System.Drawing.Color.Black;
			this.lbl로케이션.Location = new System.Drawing.Point(0, 3);
			this.lbl로케이션.Name = "lbl로케이션";
			this.lbl로케이션.Size = new System.Drawing.Size(156, 16);
			this.lbl로케이션.TabIndex = 160;
			this.lbl로케이션.Tag = "CD_ZONE";
			this.lbl로케이션.Text = "Location";
			this.lbl로케이션.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt로케이션
			// 
			this.txt로케이션.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt로케이션.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt로케이션.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt로케이션.Location = new System.Drawing.Point(157, 1);
			this.txt로케이션.Name = "txt로케이션";
			this.txt로케이션.Size = new System.Drawing.Size(185, 21);
			this.txt로케이션.TabIndex = 11;
			this.txt로케이션.Tag = "CD_ZONE";
			// 
			// bpPanelControl61
			// 
			this.bpPanelControl61.Controls.Add(this.m_lblCdGiSl);
			this.bpPanelControl61.Controls.Add(this.ctx출고SL);
			this.bpPanelControl61.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl61.Name = "bpPanelControl61";
			this.bpPanelControl61.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl61.TabIndex = 2;
			this.bpPanelControl61.Text = "bpPanelControl61";
			// 
			// m_lblCdGiSl
			// 
			this.m_lblCdGiSl.BackColor = System.Drawing.Color.Transparent;
			this.m_lblCdGiSl.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblCdGiSl.Location = new System.Drawing.Point(0, 3);
			this.m_lblCdGiSl.Name = "m_lblCdGiSl";
			this.m_lblCdGiSl.Size = new System.Drawing.Size(156, 16);
			this.m_lblCdGiSl.TabIndex = 62;
			this.m_lblCdGiSl.Text = "출고S/L";
			this.m_lblCdGiSl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx출고SL
			// 
			this.ctx출고SL.CodeName = null;
			this.ctx출고SL.CodeValue = null;
			this.ctx출고SL.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
			this.ctx출고SL.Location = new System.Drawing.Point(157, 1);
			this.ctx출고SL.Name = "ctx출고SL";
			this.ctx출고SL.Size = new System.Drawing.Size(185, 21);
			this.ctx출고SL.TabIndex = 7;
			this.ctx출고SL.TabStop = false;
			this.ctx출고SL.Tag = "CD_GISL,NM_GISL";
			// 
			// oneGridItem33
			// 
			this.oneGridItem33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem33.Controls.Add(this.bpPanelControl62);
			this.oneGridItem33.Controls.Add(this.bpPanelControl63);
			this.oneGridItem33.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem33.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem33.Name = "oneGridItem33";
			this.oneGridItem33.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem33.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem33.TabIndex = 6;
			// 
			// bpPanelControl62
			// 
			this.bpPanelControl62.Controls.Add(this.lbl표준단위원가);
			this.bpPanelControl62.Controls.Add(this.cur표준단위원가);
			this.bpPanelControl62.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl62.Name = "bpPanelControl62";
			this.bpPanelControl62.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl62.TabIndex = 3;
			this.bpPanelControl62.Text = "bpPanelControl62";
			// 
			// lbl표준단위원가
			// 
			this.lbl표준단위원가.BackColor = System.Drawing.Color.Transparent;
			this.lbl표준단위원가.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl표준단위원가.Location = new System.Drawing.Point(0, 3);
			this.lbl표준단위원가.Name = "lbl표준단위원가";
			this.lbl표준단위원가.Size = new System.Drawing.Size(156, 16);
			this.lbl표준단위원가.TabIndex = 159;
			this.lbl표준단위원가.Tag = "STAND_PRC";
			this.lbl표준단위원가.Text = "표준단위원가";
			this.lbl표준단위원가.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur표준단위원가
			// 
			this.cur표준단위원가.BackColor = System.Drawing.Color.White;
			this.cur표준단위원가.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur표준단위원가.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur표준단위원가.CurrencyDecimalDigits = 4;
			this.cur표준단위원가.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur표준단위원가.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur표준단위원가.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur표준단위원가.Location = new System.Drawing.Point(157, 1);
			this.cur표준단위원가.MaxLength = 16;
			this.cur표준단위원가.Name = "cur표준단위원가";
			this.cur표준단위원가.NullString = "0";
			this.cur표준단위원가.PositiveColor = System.Drawing.Color.Black;
			this.cur표준단위원가.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur표준단위원가.Size = new System.Drawing.Size(185, 21);
			this.cur표준단위원가.TabIndex = 13;
			this.cur표준단위원가.Tag = "STAND_PRC";
			this.cur표준단위원가.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl63
			// 
			this.bpPanelControl63.Controls.Add(this.m_lblDyImcly);
			this.bpPanelControl63.Controls.Add(this.cur순환실사주기);
			this.bpPanelControl63.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl63.Name = "bpPanelControl63";
			this.bpPanelControl63.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl63.TabIndex = 2;
			this.bpPanelControl63.Text = "bpPanelControl63";
			// 
			// m_lblDyImcly
			// 
			this.m_lblDyImcly.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDyImcly.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDyImcly.ForeColor = System.Drawing.Color.Black;
			this.m_lblDyImcly.Location = new System.Drawing.Point(0, 3);
			this.m_lblDyImcly.Name = "m_lblDyImcly";
			this.m_lblDyImcly.Size = new System.Drawing.Size(156, 16);
			this.m_lblDyImcly.TabIndex = 67;
			this.m_lblDyImcly.Tag = "CD_CIM";
			this.m_lblDyImcly.Text = "순환실사주기";
			this.m_lblDyImcly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur순환실사주기
			// 
			this.cur순환실사주기.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur순환실사주기.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur순환실사주기.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur순환실사주기.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur순환실사주기.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur순환실사주기.Location = new System.Drawing.Point(157, 1);
			this.cur순환실사주기.MaxLength = 3;
			this.cur순환실사주기.Name = "cur순환실사주기";
			this.cur순환실사주기.NullString = "0";
			this.cur순환실사주기.PositiveColor = System.Drawing.Color.Black;
			this.cur순환실사주기.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur순환실사주기.Size = new System.Drawing.Size(185, 21);
			this.cur순환실사주기.TabIndex = 8;
			this.cur순환실사주기.Tag = "DY_IMCLY";
			this.cur순환실사주기.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem34
			// 
			this.oneGridItem34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem34.Controls.Add(this.bpPanelControl64);
			this.oneGridItem34.Controls.Add(this.bpPanelControl65);
			this.oneGridItem34.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem34.Location = new System.Drawing.Point(0, 161);
			this.oneGridItem34.Name = "oneGridItem34";
			this.oneGridItem34.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem34.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem34.TabIndex = 7;
			// 
			// bpPanelControl64
			// 
			this.bpPanelControl64.Controls.Add(this.labelExt10);
			this.bpPanelControl64.Controls.Add(this.dtp표준원가적용일);
			this.bpPanelControl64.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl64.Name = "bpPanelControl64";
			this.bpPanelControl64.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl64.TabIndex = 3;
			this.bpPanelControl64.Text = "bpPanelControl64";
			// 
			// labelExt10
			// 
			this.labelExt10.BackColor = System.Drawing.Color.Transparent;
			this.labelExt10.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt10.Location = new System.Drawing.Point(0, 3);
			this.labelExt10.Name = "labelExt10";
			this.labelExt10.Size = new System.Drawing.Size(156, 16);
			this.labelExt10.TabIndex = 161;
			this.labelExt10.Text = "표준원가적용일";
			this.labelExt10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp표준원가적용일
			// 
			this.dtp표준원가적용일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp표준원가적용일.Location = new System.Drawing.Point(157, 1);
			this.dtp표준원가적용일.Mask = "####/##/##";
			this.dtp표준원가적용일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp표준원가적용일.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp표준원가적용일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp표준원가적용일.Name = "dtp표준원가적용일";
			this.dtp표준원가적용일.Size = new System.Drawing.Size(89, 21);
			this.dtp표준원가적용일.TabIndex = 187;
			this.dtp표준원가적용일.Tag = "DT_STAND_CO";
			this.dtp표준원가적용일.Value = new System.DateTime(((long)(0)));
			// 
			// bpPanelControl65
			// 
			this.bpPanelControl65.Controls.Add(this.m_lblFgGri);
			this.bpPanelControl65.Controls.Add(this.cbo불출관리);
			this.bpPanelControl65.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl65.Name = "bpPanelControl65";
			this.bpPanelControl65.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl65.TabIndex = 2;
			this.bpPanelControl65.Text = "bpPanelControl65";
			// 
			// m_lblFgGri
			// 
			this.m_lblFgGri.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgGri.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgGri.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgGri.Name = "m_lblFgGri";
			this.m_lblFgGri.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgGri.TabIndex = 43;
			this.m_lblFgGri.Tag = "FG_GIR";
			this.m_lblFgGri.Text = "불출관리";
			this.m_lblFgGri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo불출관리
			// 
			this.cbo불출관리.AutoDropDown = true;
			this.cbo불출관리.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo불출관리.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo불출관리.ItemHeight = 12;
			this.cbo불출관리.Location = new System.Drawing.Point(157, 1);
			this.cbo불출관리.Name = "cbo불출관리";
			this.cbo불출관리.Size = new System.Drawing.Size(185, 20);
			this.cbo불출관리.TabIndex = 10;
			this.cbo불출관리.Tag = "FG_GIR";
			// 
			// oneGridItem35
			// 
			this.oneGridItem35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem35.Controls.Add(this.bpPanelControl66);
			this.oneGridItem35.Controls.Add(this.bpPanelControl67);
			this.oneGridItem35.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem35.Location = new System.Drawing.Point(0, 184);
			this.oneGridItem35.Name = "oneGridItem35";
			this.oneGridItem35.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem35.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem35.TabIndex = 8;
			// 
			// bpPanelControl66
			// 
			this.bpPanelControl66.Controls.Add(this.lbl과세구분매입);
			this.bpPanelControl66.Controls.Add(this.cbo과세구분매입);
			this.bpPanelControl66.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl66.Name = "bpPanelControl66";
			this.bpPanelControl66.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl66.TabIndex = 3;
			this.bpPanelControl66.Text = "bpPanelControl66";
			// 
			// lbl과세구분매입
			// 
			this.lbl과세구분매입.BackColor = System.Drawing.Color.Transparent;
			this.lbl과세구분매입.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl과세구분매입.ForeColor = System.Drawing.Color.Black;
			this.lbl과세구분매입.Location = new System.Drawing.Point(0, 3);
			this.lbl과세구분매입.Name = "lbl과세구분매입";
			this.lbl과세구분매입.Size = new System.Drawing.Size(156, 16);
			this.lbl과세구분매입.TabIndex = 173;
			this.lbl과세구분매입.Tag = "CD_ZONE";
			this.lbl과세구분매입.Text = "과세구분(매입)";
			this.lbl과세구분매입.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo과세구분매입
			// 
			this.cbo과세구분매입.AutoDropDown = true;
			this.cbo과세구분매입.BackColor = System.Drawing.Color.White;
			this.cbo과세구분매입.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo과세구분매입.ItemHeight = 12;
			this.cbo과세구분매입.Location = new System.Drawing.Point(157, 1);
			this.cbo과세구분매입.Name = "cbo과세구분매입";
			this.cbo과세구분매입.Size = new System.Drawing.Size(185, 20);
			this.cbo과세구분매입.TabIndex = 16;
			this.cbo과세구분매입.Tag = "FG_TAX_PU";
			// 
			// bpPanelControl67
			// 
			this.bpPanelControl67.Controls.Add(this.m_lblDyValid);
			this.bpPanelControl67.Controls.Add(this.cur유효기간);
			this.bpPanelControl67.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl67.Name = "bpPanelControl67";
			this.bpPanelControl67.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl67.TabIndex = 2;
			this.bpPanelControl67.Text = "bpPanelControl67";
			// 
			// m_lblDyValid
			// 
			this.m_lblDyValid.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDyValid.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDyValid.Location = new System.Drawing.Point(0, 3);
			this.m_lblDyValid.Name = "m_lblDyValid";
			this.m_lblDyValid.Size = new System.Drawing.Size(156, 16);
			this.m_lblDyValid.TabIndex = 44;
			this.m_lblDyValid.Tag = "DY_VALID";
			this.m_lblDyValid.Text = "유효기간";
			this.m_lblDyValid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur유효기간
			// 
			this.cur유효기간.BackColor = System.Drawing.Color.White;
			this.cur유효기간.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur유효기간.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur유효기간.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur유효기간.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur유효기간.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur유효기간.Location = new System.Drawing.Point(157, 1);
			this.cur유효기간.MaxLength = 3;
			this.cur유효기간.Name = "cur유효기간";
			this.cur유효기간.NullString = "0";
			this.cur유효기간.PositiveColor = System.Drawing.Color.Black;
			this.cur유효기간.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur유효기간.Size = new System.Drawing.Size(185, 21);
			this.cur유효기간.TabIndex = 12;
			this.cur유효기간.Tag = "DY_VALID";
			this.cur유효기간.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem36
			// 
			this.oneGridItem36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem36.Controls.Add(this.bpPanelControl68);
			this.oneGridItem36.Controls.Add(this.bpPanelControl69);
			this.oneGridItem36.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem36.Location = new System.Drawing.Point(0, 207);
			this.oneGridItem36.Name = "oneGridItem36";
			this.oneGridItem36.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem36.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem36.TabIndex = 9;
			// 
			// bpPanelControl68
			// 
			this.bpPanelControl68.Controls.Add(this.lbl과세구분매출);
			this.bpPanelControl68.Controls.Add(this.cbo과세구분매출);
			this.bpPanelControl68.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl68.Name = "bpPanelControl68";
			this.bpPanelControl68.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl68.TabIndex = 3;
			this.bpPanelControl68.Text = "bpPanelControl68";
			// 
			// lbl과세구분매출
			// 
			this.lbl과세구분매출.BackColor = System.Drawing.Color.Transparent;
			this.lbl과세구분매출.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl과세구분매출.ForeColor = System.Drawing.Color.Black;
			this.lbl과세구분매출.Location = new System.Drawing.Point(0, 3);
			this.lbl과세구분매출.Name = "lbl과세구분매출";
			this.lbl과세구분매출.Size = new System.Drawing.Size(156, 16);
			this.lbl과세구분매출.TabIndex = 171;
			this.lbl과세구분매출.Tag = "CD_ZONE";
			this.lbl과세구분매출.Text = "과세구분(매출)";
			this.lbl과세구분매출.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo과세구분매출
			// 
			this.cbo과세구분매출.AutoDropDown = true;
			this.cbo과세구분매출.BackColor = System.Drawing.Color.White;
			this.cbo과세구분매출.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo과세구분매출.ItemHeight = 12;
			this.cbo과세구분매출.Location = new System.Drawing.Point(157, 1);
			this.cbo과세구분매출.Name = "cbo과세구분매출";
			this.cbo과세구분매출.Size = new System.Drawing.Size(185, 20);
			this.cbo과세구분매출.TabIndex = 18;
			this.cbo과세구분매출.Tag = "FG_TAX_SA";
			// 
			// bpPanelControl69
			// 
			this.bpPanelControl69.Controls.Add(this.m_lblNoModel);
			this.bpPanelControl69.Controls.Add(this.txt모델코드);
			this.bpPanelControl69.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl69.Name = "bpPanelControl69";
			this.bpPanelControl69.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl69.TabIndex = 2;
			this.bpPanelControl69.Text = "bpPanelControl69";
			// 
			// m_lblNoModel
			// 
			this.m_lblNoModel.BackColor = System.Drawing.Color.Transparent;
			this.m_lblNoModel.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblNoModel.Location = new System.Drawing.Point(0, 3);
			this.m_lblNoModel.Name = "m_lblNoModel";
			this.m_lblNoModel.Size = new System.Drawing.Size(156, 16);
			this.m_lblNoModel.TabIndex = 45;
			this.m_lblNoModel.Tag = "NO_MODEL";
			this.m_lblNoModel.Text = "모델코드";
			this.m_lblNoModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt모델코드
			// 
			this.txt모델코드.BackColor = System.Drawing.Color.White;
			this.txt모델코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt모델코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt모델코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt모델코드.Location = new System.Drawing.Point(157, 1);
			this.txt모델코드.MaxLength = 20;
			this.txt모델코드.Name = "txt모델코드";
			this.txt모델코드.Size = new System.Drawing.Size(185, 21);
			this.txt모델코드.TabIndex = 14;
			this.txt모델코드.Tag = "NO_MODEL";
			// 
			// oneGridItem37
			// 
			this.oneGridItem37.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem37.Controls.Add(this.bpPanelControl71);
			this.oneGridItem37.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem37.Location = new System.Drawing.Point(0, 230);
			this.oneGridItem37.Name = "oneGridItem37";
			this.oneGridItem37.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem37.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem37.TabIndex = 10;
			// 
			// bpPanelControl71
			// 
			this.bpPanelControl71.Controls.Add(this.lbl대분류);
			this.bpPanelControl71.Controls.Add(this.ctx대분류);
			this.bpPanelControl71.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl71.Name = "bpPanelControl71";
			this.bpPanelControl71.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl71.TabIndex = 2;
			this.bpPanelControl71.Text = "bpPanelControl71";
			// 
			// lbl대분류
			// 
			this.lbl대분류.BackColor = System.Drawing.Color.Transparent;
			this.lbl대분류.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl대분류.ForeColor = System.Drawing.Color.Black;
			this.lbl대분류.Location = new System.Drawing.Point(0, 3);
			this.lbl대분류.Name = "lbl대분류";
			this.lbl대분류.Size = new System.Drawing.Size(156, 16);
			this.lbl대분류.TabIndex = 71;
			this.lbl대분류.Tag = "CLS_L";
			this.lbl대분류.Text = "대분류";
			this.lbl대분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx대분류
			// 
			this.ctx대분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx대분류.Location = new System.Drawing.Point(157, 1);
			this.ctx대분류.Name = "ctx대분류";
			this.ctx대분류.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx대분류.Size = new System.Drawing.Size(326, 21);
			this.ctx대분류.TabIndex = 179;
			this.ctx대분류.TabStop = false;
			this.ctx대분류.Tag = "CLS_L,NM_CLS_L";
			// 
			// oneGridItem38
			// 
			this.oneGridItem38.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem38.Controls.Add(this.bpPanelControl77);
			this.oneGridItem38.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem38.Location = new System.Drawing.Point(0, 253);
			this.oneGridItem38.Name = "oneGridItem38";
			this.oneGridItem38.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem38.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem38.TabIndex = 11;
			// 
			// bpPanelControl77
			// 
			this.bpPanelControl77.Controls.Add(this.lbl중분류);
			this.bpPanelControl77.Controls.Add(this.ctx중분류);
			this.bpPanelControl77.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl77.Name = "bpPanelControl77";
			this.bpPanelControl77.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl77.TabIndex = 4;
			this.bpPanelControl77.Text = "bpPanelControl77";
			// 
			// lbl중분류
			// 
			this.lbl중분류.BackColor = System.Drawing.Color.Transparent;
			this.lbl중분류.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl중분류.ForeColor = System.Drawing.Color.Black;
			this.lbl중분류.Location = new System.Drawing.Point(0, 3);
			this.lbl중분류.Name = "lbl중분류";
			this.lbl중분류.Size = new System.Drawing.Size(156, 16);
			this.lbl중분류.TabIndex = 73;
			this.lbl중분류.Tag = "CLS_M";
			this.lbl중분류.Text = "중분류";
			this.lbl중분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx중분류
			// 
			this.ctx중분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx중분류.Location = new System.Drawing.Point(157, 1);
			this.ctx중분류.Name = "ctx중분류";
			this.ctx중분류.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx중분류.Size = new System.Drawing.Size(326, 21);
			this.ctx중분류.TabIndex = 189;
			this.ctx중분류.TabStop = false;
			this.ctx중분류.Tag = "CLS_M,NM_CLS_M";
			// 
			// oneGridItem39
			// 
			this.oneGridItem39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem39.Controls.Add(this.bpPanelControl75);
			this.oneGridItem39.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem39.Location = new System.Drawing.Point(0, 276);
			this.oneGridItem39.Name = "oneGridItem39";
			this.oneGridItem39.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem39.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem39.TabIndex = 12;
			// 
			// bpPanelControl75
			// 
			this.bpPanelControl75.Controls.Add(this.ctx소분류);
			this.bpPanelControl75.Controls.Add(this.lbl소분류);
			this.bpPanelControl75.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl75.Name = "bpPanelControl75";
			this.bpPanelControl75.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl75.TabIndex = 4;
			this.bpPanelControl75.Text = "bpPanelControl75";
			// 
			// ctx소분류
			// 
			this.ctx소분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx소분류.Location = new System.Drawing.Point(157, 1);
			this.ctx소분류.Name = "ctx소분류";
			this.ctx소분류.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx소분류.Size = new System.Drawing.Size(326, 21);
			this.ctx소분류.TabIndex = 190;
			this.ctx소분류.TabStop = false;
			this.ctx소분류.Tag = "CLS_S,NM_CLS_S";
			// 
			// lbl소분류
			// 
			this.lbl소분류.BackColor = System.Drawing.Color.Transparent;
			this.lbl소분류.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl소분류.ForeColor = System.Drawing.Color.Black;
			this.lbl소분류.Location = new System.Drawing.Point(0, 3);
			this.lbl소분류.Name = "lbl소분류";
			this.lbl소분류.Size = new System.Drawing.Size(156, 16);
			this.lbl소분류.TabIndex = 75;
			this.lbl소분류.Tag = "CLS_S";
			this.lbl소분류.Text = "소분류";
			this.lbl소분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem40
			// 
			this.oneGridItem40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem40.Controls.Add(this.bpPanelControl72);
			this.oneGridItem40.Controls.Add(this.bpPanelControl73);
			this.oneGridItem40.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem40.Location = new System.Drawing.Point(0, 299);
			this.oneGridItem40.Name = "oneGridItem40";
			this.oneGridItem40.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem40.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem40.TabIndex = 13;
			// 
			// bpPanelControl72
			// 
			this.bpPanelControl72.Controls.Add(this.lbl_관리자2);
			this.bpPanelControl72.Controls.Add(this.ctx관리자2);
			this.bpPanelControl72.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl72.Name = "bpPanelControl72";
			this.bpPanelControl72.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl72.TabIndex = 5;
			this.bpPanelControl72.Text = "bpPanelControl72";
			// 
			// lbl_관리자2
			// 
			this.lbl_관리자2.BackColor = System.Drawing.Color.Transparent;
			this.lbl_관리자2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl_관리자2.ForeColor = System.Drawing.Color.Black;
			this.lbl_관리자2.Location = new System.Drawing.Point(0, 3);
			this.lbl_관리자2.Name = "lbl_관리자2";
			this.lbl_관리자2.Size = new System.Drawing.Size(156, 16);
			this.lbl_관리자2.TabIndex = 167;
			this.lbl_관리자2.Tag = "CD_ZONE";
			this.lbl_관리자2.Text = "관리자2";
			this.lbl_관리자2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx관리자2
			// 
			this.ctx관리자2.CodeName = null;
			this.ctx관리자2.CodeValue = null;
			this.ctx관리자2.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx관리자2.Location = new System.Drawing.Point(157, 2);
			this.ctx관리자2.Name = "ctx관리자2";
			this.ctx관리자2.Size = new System.Drawing.Size(185, 21);
			this.ctx관리자2.TabIndex = 21;
			this.ctx관리자2.TabStop = false;
			this.ctx관리자2.Tag = "NO_MANAGER2,NM_MANAGER2";
			// 
			// bpPanelControl73
			// 
			this.bpPanelControl73.Controls.Add(this.lbl_관리자1);
			this.bpPanelControl73.Controls.Add(this.ctx관리자1);
			this.bpPanelControl73.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl73.Name = "bpPanelControl73";
			this.bpPanelControl73.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl73.TabIndex = 4;
			this.bpPanelControl73.Text = "bpPanelControl73";
			// 
			// lbl_관리자1
			// 
			this.lbl_관리자1.BackColor = System.Drawing.Color.Transparent;
			this.lbl_관리자1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl_관리자1.ForeColor = System.Drawing.Color.Black;
			this.lbl_관리자1.Location = new System.Drawing.Point(0, 3);
			this.lbl_관리자1.Name = "lbl_관리자1";
			this.lbl_관리자1.Size = new System.Drawing.Size(156, 16);
			this.lbl_관리자1.TabIndex = 172;
			this.lbl_관리자1.Tag = "CD_ZONE";
			this.lbl_관리자1.Text = "관리자1";
			this.lbl_관리자1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx관리자1
			// 
			this.ctx관리자1.CodeName = null;
			this.ctx관리자1.CodeValue = null;
			this.ctx관리자1.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx관리자1.Location = new System.Drawing.Point(157, 1);
			this.ctx관리자1.Name = "ctx관리자1";
			this.ctx관리자1.Size = new System.Drawing.Size(185, 21);
			this.ctx관리자1.TabIndex = 20;
			this.ctx관리자1.TabStop = false;
			this.ctx관리자1.Tag = "NO_MANAGER1,NM_MANAGER1";
			// 
			// oneGridItem103
			// 
			this.oneGridItem103.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem103.Controls.Add(this.bpPanelControl178);
			this.oneGridItem103.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem103.Location = new System.Drawing.Point(0, 322);
			this.oneGridItem103.Name = "oneGridItem103";
			this.oneGridItem103.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem103.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem103.TabIndex = 14;
			// 
			// bpPanelControl178
			// 
			this.bpPanelControl178.Controls.Add(this.labelExt12);
			this.bpPanelControl178.Controls.Add(this.ctxCC코드);
			this.bpPanelControl178.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl178.Name = "bpPanelControl178";
			this.bpPanelControl178.Size = new System.Drawing.Size(744, 23);
			this.bpPanelControl178.TabIndex = 6;
			this.bpPanelControl178.Text = "bpPanelControl178";
			// 
			// labelExt12
			// 
			this.labelExt12.BackColor = System.Drawing.Color.Transparent;
			this.labelExt12.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt12.ForeColor = System.Drawing.Color.Black;
			this.labelExt12.Location = new System.Drawing.Point(0, 3);
			this.labelExt12.Name = "labelExt12";
			this.labelExt12.Size = new System.Drawing.Size(156, 16);
			this.labelExt12.TabIndex = 167;
			this.labelExt12.Text = "C/C코드";
			this.labelExt12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctxCC코드
			// 
			this.ctxCC코드.CodeName = null;
			this.ctxCC코드.CodeValue = null;
			this.ctxCC코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB;
			this.ctxCC코드.Location = new System.Drawing.Point(157, 2);
			this.ctxCC코드.Name = "ctxCC코드";
			this.ctxCC코드.Size = new System.Drawing.Size(185, 21);
			this.ctxCC코드.TabIndex = 21;
			this.ctxCC코드.TabStop = false;
			this.ctxCC코드.Tag = "CD_CC,NM_CC";
			// 
			// tpg오더
			// 
			this.tpg오더.BackColor = System.Drawing.Color.White;
			this.tpg오더.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg오더.Controls.Add(this.pnl오더);
			this.tpg오더.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tpg오더.ImageIndex = 2;
			this.tpg오더.Location = new System.Drawing.Point(4, 24);
			this.tpg오더.Name = "tpg오더";
			this.tpg오더.Padding = new System.Windows.Forms.Padding(6);
			this.tpg오더.Size = new System.Drawing.Size(772, 626);
			this.tpg오더.TabIndex = 2;
			this.tpg오더.Tag = "ORDER";
			this.tpg오더.Text = "오더";
			this.tpg오더.UseVisualStyleBackColor = true;
			// 
			// pnl오더
			// 
			this.pnl오더.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl오더.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem41,
            this.oneGridItem42,
            this.oneGridItem43,
            this.oneGridItem44,
            this.oneGridItem45,
            this.oneGridItem46,
            this.oneGridItem47,
            this.oneGridItem48,
            this.oneGridItem49,
            this.oneGridItem50,
            this.oneGridItem51,
            this.oneGridItem52,
            this.oneGridItem53,
            this.oneGridItem54});
			this.pnl오더.Location = new System.Drawing.Point(6, 6);
			this.pnl오더.Name = "pnl오더";
			this.pnl오더.Size = new System.Drawing.Size(756, 610);
			this.pnl오더.TabIndex = 1;
			// 
			// oneGridItem41
			// 
			this.oneGridItem41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem41.Controls.Add(this.bpPanelControl74);
			this.oneGridItem41.Controls.Add(this.bpPanelControl70);
			this.oneGridItem41.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem41.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem41.Name = "oneGridItem41";
			this.oneGridItem41.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem41.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem41.TabIndex = 0;
			// 
			// bpPanelControl74
			// 
			this.bpPanelControl74.Controls.Add(this.m_lblFgSerNo);
			this.bpPanelControl74.Controls.Add(this.cboLOT관리);
			this.bpPanelControl74.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl74.Name = "bpPanelControl74";
			this.bpPanelControl74.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl74.TabIndex = 1;
			this.bpPanelControl74.Text = "bpPanelControl74";
			// 
			// m_lblFgSerNo
			// 
			this.m_lblFgSerNo.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgSerNo.Font = new System.Drawing.Font("굴림체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgSerNo.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgSerNo.Name = "m_lblFgSerNo";
			this.m_lblFgSerNo.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgSerNo.TabIndex = 172;
			this.m_lblFgSerNo.Tag = "FG_SERNO";
			this.m_lblFgSerNo.Text = "LOT,S/N관리";
			this.m_lblFgSerNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboLOT관리
			// 
			this.cboLOT관리.AutoDropDown = true;
			this.cboLOT관리.BackColor = System.Drawing.Color.White;
			this.cboLOT관리.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLOT관리.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cboLOT관리.ItemHeight = 12;
			this.cboLOT관리.Location = new System.Drawing.Point(157, 1);
			this.cboLOT관리.Name = "cboLOT관리";
			this.cboLOT관리.Size = new System.Drawing.Size(185, 20);
			this.cboLOT관리.TabIndex = 1;
			this.cboLOT관리.Tag = "FG_SERNO";
			// 
			// bpPanelControl70
			// 
			this.bpPanelControl70.Controls.Add(this.m_lblTpPo);
			this.bpPanelControl70.Controls.Add(this.cbo발주방침);
			this.bpPanelControl70.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl70.Name = "bpPanelControl70";
			this.bpPanelControl70.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl70.TabIndex = 0;
			this.bpPanelControl70.Text = "bpPanelControl70";
			// 
			// m_lblTpPo
			// 
			this.m_lblTpPo.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTpPo.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblTpPo.Location = new System.Drawing.Point(0, 3);
			this.m_lblTpPo.Name = "m_lblTpPo";
			this.m_lblTpPo.Size = new System.Drawing.Size(156, 16);
			this.m_lblTpPo.TabIndex = 165;
			this.m_lblTpPo.Tag = "TP_PO";
			this.m_lblTpPo.Text = "발주방침";
			this.m_lblTpPo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo발주방침
			// 
			this.cbo발주방침.AutoDropDown = true;
			this.cbo발주방침.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo발주방침.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo발주방침.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo발주방침.ItemHeight = 12;
			this.cbo발주방침.Location = new System.Drawing.Point(157, 1);
			this.cbo발주방침.Name = "cbo발주방침";
			this.cbo발주방침.Size = new System.Drawing.Size(185, 20);
			this.cbo발주방침.TabIndex = 0;
			this.cbo발주방침.Tag = "TP_PO";
			// 
			// oneGridItem42
			// 
			this.oneGridItem42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem42.Controls.Add(this.bpPanelControl76);
			this.oneGridItem42.Controls.Add(this.bpPanelControl78);
			this.oneGridItem42.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem42.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem42.Name = "oneGridItem42";
			this.oneGridItem42.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem42.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem42.TabIndex = 1;
			// 
			// bpPanelControl76
			// 
			this.bpPanelControl76.Controls.Add(this.m_lblLotSize);
			this.bpPanelControl76.Controls.Add(this.cur로트사이즈);
			this.bpPanelControl76.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl76.Name = "bpPanelControl76";
			this.bpPanelControl76.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl76.TabIndex = 3;
			this.bpPanelControl76.Text = "bpPanelControl76";
			// 
			// m_lblLotSize
			// 
			this.m_lblLotSize.BackColor = System.Drawing.Color.Transparent;
			this.m_lblLotSize.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblLotSize.Location = new System.Drawing.Point(0, 3);
			this.m_lblLotSize.Name = "m_lblLotSize";
			this.m_lblLotSize.Size = new System.Drawing.Size(156, 16);
			this.m_lblLotSize.TabIndex = 171;
			this.m_lblLotSize.Tag = "LOTSIZE";
			this.m_lblLotSize.Text = "Lot Size";
			this.m_lblLotSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur로트사이즈
			// 
			this.cur로트사이즈.BackColor = System.Drawing.Color.White;
			this.cur로트사이즈.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur로트사이즈.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur로트사이즈.CurrencyDecimalDigits = 4;
			this.cur로트사이즈.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur로트사이즈.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur로트사이즈.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur로트사이즈.Location = new System.Drawing.Point(157, 1);
			this.cur로트사이즈.MaxLength = 16;
			this.cur로트사이즈.Name = "cur로트사이즈";
			this.cur로트사이즈.NullString = "0";
			this.cur로트사이즈.PositiveColor = System.Drawing.Color.Black;
			this.cur로트사이즈.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur로트사이즈.Size = new System.Drawing.Size(185, 21);
			this.cur로트사이즈.TabIndex = 3;
			this.cur로트사이즈.Tag = "LOTSIZE";
			this.cur로트사이즈.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl78
			// 
			this.bpPanelControl78.Controls.Add(this.m_lblFgTracking);
			this.bpPanelControl78.Controls.Add(this.cbo트래킹);
			this.bpPanelControl78.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl78.Name = "bpPanelControl78";
			this.bpPanelControl78.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl78.TabIndex = 2;
			this.bpPanelControl78.Text = "bpPanelControl78";
			// 
			// m_lblFgTracking
			// 
			this.m_lblFgTracking.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgTracking.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgTracking.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgTracking.Name = "m_lblFgTracking";
			this.m_lblFgTracking.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgTracking.TabIndex = 163;
			this.m_lblFgTracking.Tag = "FG_TRACKING";
			this.m_lblFgTracking.Text = "Tracking";
			this.m_lblFgTracking.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo트래킹
			// 
			this.cbo트래킹.AutoDropDown = true;
			this.cbo트래킹.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo트래킹.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo트래킹.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo트래킹.ItemHeight = 12;
			this.cbo트래킹.Location = new System.Drawing.Point(157, 1);
			this.cbo트래킹.Name = "cbo트래킹";
			this.cbo트래킹.Size = new System.Drawing.Size(185, 20);
			this.cbo트래킹.TabIndex = 2;
			this.cbo트래킹.Tag = "FG_TRACKING";
			// 
			// oneGridItem43
			// 
			this.oneGridItem43.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem43.Controls.Add(this.bpPanelControl79);
			this.oneGridItem43.Controls.Add(this.bpPanelControl80);
			this.oneGridItem43.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem43.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem43.Name = "oneGridItem43";
			this.oneGridItem43.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem43.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem43.TabIndex = 2;
			// 
			// bpPanelControl79
			// 
			this.bpPanelControl79.Controls.Add(this.lbllotsn동시사용);
			this.bpPanelControl79.Controls.Add(this.cbolotsn동시사용);
			this.bpPanelControl79.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl79.Name = "bpPanelControl79";
			this.bpPanelControl79.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl79.TabIndex = 3;
			this.bpPanelControl79.Text = "bpPanelControl79";
			// 
			// lbllotsn동시사용
			// 
			this.lbllotsn동시사용.BackColor = System.Drawing.Color.Transparent;
			this.lbllotsn동시사용.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbllotsn동시사용.Location = new System.Drawing.Point(0, 3);
			this.lbllotsn동시사용.Name = "lbllotsn동시사용";
			this.lbllotsn동시사용.Size = new System.Drawing.Size(156, 16);
			this.lbllotsn동시사용.TabIndex = 173;
			this.lbllotsn동시사용.Tag = "LOTSIZE";
			this.lbllotsn동시사용.Text = "LOT,S/N동시사용";
			this.lbllotsn동시사용.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbllotsn동시사용.Visible = false;
			// 
			// cbolotsn동시사용
			// 
			this.cbolotsn동시사용.AutoDropDown = true;
			this.cbolotsn동시사용.BackColor = System.Drawing.Color.White;
			this.cbolotsn동시사용.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbolotsn동시사용.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbolotsn동시사용.ItemHeight = 12;
			this.cbolotsn동시사용.Location = new System.Drawing.Point(157, 1);
			this.cbolotsn동시사용.Name = "cbolotsn동시사용";
			this.cbolotsn동시사용.Size = new System.Drawing.Size(185, 20);
			this.cbolotsn동시사용.TabIndex = 188;
			this.cbolotsn동시사용.Tag = "BOTH_SERLOT";
			this.cbolotsn동시사용.Visible = false;
			// 
			// bpPanelControl80
			// 
			this.bpPanelControl80.Controls.Add(this.m_lblPartner);
			this.bpPanelControl80.Controls.Add(this.ctx주거래처);
			this.bpPanelControl80.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl80.Name = "bpPanelControl80";
			this.bpPanelControl80.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl80.TabIndex = 2;
			this.bpPanelControl80.Text = "bpPanelControl80";
			// 
			// m_lblPartner
			// 
			this.m_lblPartner.BackColor = System.Drawing.Color.Transparent;
			this.m_lblPartner.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblPartner.Location = new System.Drawing.Point(0, 3);
			this.m_lblPartner.Name = "m_lblPartner";
			this.m_lblPartner.Size = new System.Drawing.Size(156, 16);
			this.m_lblPartner.TabIndex = 161;
			this.m_lblPartner.Tag = "PARTNER";
			this.m_lblPartner.Text = "주거래처";
			this.m_lblPartner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx주거래처
			// 
			this.ctx주거래처.CodeName = null;
			this.ctx주거래처.CodeValue = null;
			this.ctx주거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx주거래처.Location = new System.Drawing.Point(157, 1);
			this.ctx주거래처.Name = "ctx주거래처";
			this.ctx주거래처.Size = new System.Drawing.Size(185, 21);
			this.ctx주거래처.TabIndex = 4;
			this.ctx주거래처.TabStop = false;
			this.ctx주거래처.Tag = "PARTNER,LN_PARTNER";
			// 
			// oneGridItem44
			// 
			this.oneGridItem44.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem44.Controls.Add(this.bpPanelControl82);
			this.oneGridItem44.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem44.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem44.Name = "oneGridItem44";
			this.oneGridItem44.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem44.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem44.TabIndex = 3;
			// 
			// bpPanelControl82
			// 
			this.bpPanelControl82.Controls.Add(this.lblMaker);
			this.bpPanelControl82.Controls.Add(this.txtMaker);
			this.bpPanelControl82.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl82.Name = "bpPanelControl82";
			this.bpPanelControl82.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl82.TabIndex = 2;
			this.bpPanelControl82.Text = "bpPanelControl82";
			// 
			// lblMaker
			// 
			this.lblMaker.BackColor = System.Drawing.Color.Transparent;
			this.lblMaker.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblMaker.Location = new System.Drawing.Point(0, 3);
			this.lblMaker.Name = "lblMaker";
			this.lblMaker.Size = new System.Drawing.Size(156, 16);
			this.lblMaker.TabIndex = 182;
			this.lblMaker.Tag = "LT_ITEM";
			this.lblMaker.Text = "Maker";
			this.lblMaker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtMaker
			// 
			this.txtMaker.BackColor = System.Drawing.Color.White;
			this.txtMaker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtMaker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMaker.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txtMaker.Location = new System.Drawing.Point(157, 1);
			this.txtMaker.MaxLength = 100;
			this.txtMaker.Name = "txtMaker";
			this.txtMaker.Size = new System.Drawing.Size(185, 21);
			this.txtMaker.TabIndex = 5;
			this.txtMaker.Tag = "NM_MAKER";
			// 
			// oneGridItem45
			// 
			this.oneGridItem45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem45.Controls.Add(this.bpPanelControl83);
			this.oneGridItem45.Controls.Add(this.bpPanelControl84);
			this.oneGridItem45.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem45.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem45.Name = "oneGridItem45";
			this.oneGridItem45.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem45.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem45.TabIndex = 4;
			// 
			// bpPanelControl83
			// 
			this.bpPanelControl83.Controls.Add(this.m_lblTpBom);
			this.bpPanelControl83.Controls.Add(this.cboBOM형태);
			this.bpPanelControl83.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl83.Name = "bpPanelControl83";
			this.bpPanelControl83.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl83.TabIndex = 3;
			this.bpPanelControl83.Text = "bpPanelControl83";
			// 
			// m_lblTpBom
			// 
			this.m_lblTpBom.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTpBom.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblTpBom.Location = new System.Drawing.Point(0, 3);
			this.m_lblTpBom.Name = "m_lblTpBom";
			this.m_lblTpBom.Size = new System.Drawing.Size(156, 16);
			this.m_lblTpBom.TabIndex = 174;
			this.m_lblTpBom.Tag = "TP_BOM";
			this.m_lblTpBom.Text = "BOM형태";
			this.m_lblTpBom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboBOM형태
			// 
			this.cboBOM형태.AutoDropDown = true;
			this.cboBOM형태.BackColor = System.Drawing.Color.White;
			this.cboBOM형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBOM형태.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cboBOM형태.ItemHeight = 12;
			this.cboBOM형태.Location = new System.Drawing.Point(157, 1);
			this.cboBOM형태.Name = "cboBOM형태";
			this.cboBOM형태.Size = new System.Drawing.Size(185, 20);
			this.cboBOM형태.TabIndex = 7;
			this.cboBOM형태.Tag = "TP_BOM";
			// 
			// bpPanelControl84
			// 
			this.bpPanelControl84.Controls.Add(this.m_lblLtItem);
			this.bpPanelControl84.Controls.Add(this.cur품목LT);
			this.bpPanelControl84.Controls.Add(this.m_lblDay);
			this.bpPanelControl84.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl84.Name = "bpPanelControl84";
			this.bpPanelControl84.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl84.TabIndex = 2;
			this.bpPanelControl84.Text = "bpPanelControl84";
			// 
			// m_lblLtItem
			// 
			this.m_lblLtItem.BackColor = System.Drawing.Color.Transparent;
			this.m_lblLtItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblLtItem.Location = new System.Drawing.Point(0, 3);
			this.m_lblLtItem.Name = "m_lblLtItem";
			this.m_lblLtItem.Size = new System.Drawing.Size(156, 16);
			this.m_lblLtItem.TabIndex = 164;
			this.m_lblLtItem.Tag = "LT_ITEM";
			this.m_lblLtItem.Text = "품목 L/T";
			this.m_lblLtItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur품목LT
			// 
			this.cur품목LT.BackColor = System.Drawing.Color.White;
			this.cur품목LT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur품목LT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur품목LT.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur품목LT.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur품목LT.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur품목LT.Location = new System.Drawing.Point(157, 1);
			this.cur품목LT.MaxLength = 3;
			this.cur품목LT.Name = "cur품목LT";
			this.cur품목LT.NullString = "0";
			this.cur품목LT.PositiveColor = System.Drawing.Color.Black;
			this.cur품목LT.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur품목LT.Size = new System.Drawing.Size(87, 21);
			this.cur품목LT.TabIndex = 6;
			this.cur품목LT.Tag = "LT_ITEM";
			this.cur품목LT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_lblDay
			// 
			this.m_lblDay.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDay.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDay.Location = new System.Drawing.Point(244, 3);
			this.m_lblDay.Name = "m_lblDay";
			this.m_lblDay.Size = new System.Drawing.Size(22, 18);
			this.m_lblDay.TabIndex = 59;
			this.m_lblDay.Tag = "DAY";
			this.m_lblDay.Text = "일";
			this.m_lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// oneGridItem46
			// 
			this.oneGridItem46.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem46.Controls.Add(this.bpPanelControl85);
			this.oneGridItem46.Controls.Add(this.bpPanelControl86);
			this.oneGridItem46.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem46.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem46.Name = "oneGridItem46";
			this.oneGridItem46.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem46.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem46.TabIndex = 5;
			// 
			// bpPanelControl85
			// 
			this.bpPanelControl85.Controls.Add(this.m_lblFgLong);
			this.bpPanelControl85.Controls.Add(this.cbo장단납기구분);
			this.bpPanelControl85.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl85.Name = "bpPanelControl85";
			this.bpPanelControl85.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl85.TabIndex = 3;
			this.bpPanelControl85.Text = "bpPanelControl85";
			// 
			// m_lblFgLong
			// 
			this.m_lblFgLong.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgLong.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgLong.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgLong.Name = "m_lblFgLong";
			this.m_lblFgLong.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgLong.TabIndex = 175;
			this.m_lblFgLong.Tag = "FG_LONG";
			this.m_lblFgLong.Text = "장단납기구분";
			this.m_lblFgLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo장단납기구분
			// 
			this.cbo장단납기구분.AutoDropDown = true;
			this.cbo장단납기구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo장단납기구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo장단납기구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo장단납기구분.ItemHeight = 12;
			this.cbo장단납기구분.Location = new System.Drawing.Point(157, 1);
			this.cbo장단납기구분.Name = "cbo장단납기구분";
			this.cbo장단납기구분.Size = new System.Drawing.Size(185, 20);
			this.cbo장단납기구분.TabIndex = 9;
			this.cbo장단납기구분.Tag = "FG_LONG";
			// 
			// bpPanelControl86
			// 
			this.bpPanelControl86.Controls.Add(this.m_lblTpManu);
			this.bpPanelControl86.Controls.Add(this.cbo제조전략);
			this.bpPanelControl86.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl86.Name = "bpPanelControl86";
			this.bpPanelControl86.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl86.TabIndex = 2;
			this.bpPanelControl86.Text = "bpPanelControl86";
			// 
			// m_lblTpManu
			// 
			this.m_lblTpManu.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTpManu.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblTpManu.Location = new System.Drawing.Point(0, 3);
			this.m_lblTpManu.Name = "m_lblTpManu";
			this.m_lblTpManu.Size = new System.Drawing.Size(156, 16);
			this.m_lblTpManu.TabIndex = 159;
			this.m_lblTpManu.Tag = "TP_MANU";
			this.m_lblTpManu.Text = "제조전략";
			this.m_lblTpManu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo제조전략
			// 
			this.cbo제조전략.AutoDropDown = true;
			this.cbo제조전략.BackColor = System.Drawing.Color.White;
			this.cbo제조전략.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo제조전략.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo제조전략.ItemHeight = 12;
			this.cbo제조전략.Location = new System.Drawing.Point(157, 1);
			this.cbo제조전략.Name = "cbo제조전략";
			this.cbo제조전략.Size = new System.Drawing.Size(185, 20);
			this.cbo제조전략.TabIndex = 8;
			this.cbo제조전략.Tag = "TP_MANU";
			// 
			// oneGridItem47
			// 
			this.oneGridItem47.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem47.Controls.Add(this.bpPanelControl87);
			this.oneGridItem47.Controls.Add(this.bpPanelControl88);
			this.oneGridItem47.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem47.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem47.Name = "oneGridItem47";
			this.oneGridItem47.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem47.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem47.TabIndex = 6;
			// 
			// bpPanelControl87
			// 
			this.bpPanelControl87.Controls.Add(this.m_lblDyPoq);
			this.bpPanelControl87.Controls.Add(this.curPOQ기간);
			this.bpPanelControl87.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl87.Name = "bpPanelControl87";
			this.bpPanelControl87.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl87.TabIndex = 3;
			this.bpPanelControl87.Text = "bpPanelControl87";
			// 
			// m_lblDyPoq
			// 
			this.m_lblDyPoq.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDyPoq.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDyPoq.Location = new System.Drawing.Point(0, 3);
			this.m_lblDyPoq.Name = "m_lblDyPoq";
			this.m_lblDyPoq.Size = new System.Drawing.Size(156, 16);
			this.m_lblDyPoq.TabIndex = 177;
			this.m_lblDyPoq.Tag = "PERIOD_POQ";
			this.m_lblDyPoq.Text = "POQ기간";
			this.m_lblDyPoq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// curPOQ기간
			// 
			this.curPOQ기간.BackColor = System.Drawing.Color.White;
			this.curPOQ기간.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.curPOQ기간.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.curPOQ기간.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.curPOQ기간.Font = new System.Drawing.Font("굴림체", 9F);
			this.curPOQ기간.ForeColor = System.Drawing.SystemColors.ControlText;
			this.curPOQ기간.Location = new System.Drawing.Point(157, 1);
			this.curPOQ기간.MaxLength = 3;
			this.curPOQ기간.Name = "curPOQ기간";
			this.curPOQ기간.NullString = "0";
			this.curPOQ기간.PositiveColor = System.Drawing.Color.Black;
			this.curPOQ기간.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.curPOQ기간.Size = new System.Drawing.Size(185, 21);
			this.curPOQ기간.TabIndex = 11;
			this.curPOQ기간.Tag = "DY_POQ";
			this.curPOQ기간.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl88
			// 
			this.bpPanelControl88.Controls.Add(this.m_lblClsPo);
			this.bpPanelControl88.Controls.Add(this.cbo발주정책);
			this.bpPanelControl88.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl88.Name = "bpPanelControl88";
			this.bpPanelControl88.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl88.TabIndex = 2;
			this.bpPanelControl88.Text = "bpPanelControl88";
			// 
			// m_lblClsPo
			// 
			this.m_lblClsPo.BackColor = System.Drawing.Color.Transparent;
			this.m_lblClsPo.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblClsPo.Location = new System.Drawing.Point(0, 3);
			this.m_lblClsPo.Name = "m_lblClsPo";
			this.m_lblClsPo.Size = new System.Drawing.Size(156, 16);
			this.m_lblClsPo.TabIndex = 167;
			this.m_lblClsPo.Tag = "CLS_PO";
			this.m_lblClsPo.Text = "발주정책";
			this.m_lblClsPo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo발주정책
			// 
			this.cbo발주정책.AutoDropDown = true;
			this.cbo발주정책.BackColor = System.Drawing.Color.White;
			this.cbo발주정책.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo발주정책.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo발주정책.ItemHeight = 12;
			this.cbo발주정책.Location = new System.Drawing.Point(157, 1);
			this.cbo발주정책.Name = "cbo발주정책";
			this.cbo발주정책.Size = new System.Drawing.Size(185, 20);
			this.cbo발주정책.TabIndex = 10;
			this.cbo발주정책.Tag = "CLS_PO";
			// 
			// oneGridItem48
			// 
			this.oneGridItem48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem48.Controls.Add(this.bpPanelControl89);
			this.oneGridItem48.Controls.Add(this.bpPanelControl90);
			this.oneGridItem48.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem48.Location = new System.Drawing.Point(0, 161);
			this.oneGridItem48.Name = "oneGridItem48";
			this.oneGridItem48.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem48.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem48.TabIndex = 7;
			// 
			// bpPanelControl89
			// 
			this.bpPanelControl89.Controls.Add(this.m_lblQtMax);
			this.bpPanelControl89.Controls.Add(this.cur최대수배수);
			this.bpPanelControl89.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl89.Name = "bpPanelControl89";
			this.bpPanelControl89.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl89.TabIndex = 3;
			this.bpPanelControl89.Text = "bpPanelControl89";
			// 
			// m_lblQtMax
			// 
			this.m_lblQtMax.BackColor = System.Drawing.Color.Transparent;
			this.m_lblQtMax.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblQtMax.Location = new System.Drawing.Point(0, 3);
			this.m_lblQtMax.Name = "m_lblQtMax";
			this.m_lblQtMax.Size = new System.Drawing.Size(156, 16);
			this.m_lblQtMax.TabIndex = 173;
			this.m_lblQtMax.Tag = "QT_MAX";
			this.m_lblQtMax.Text = "최대수배수";
			this.m_lblQtMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur최대수배수
			// 
			this.cur최대수배수.BackColor = System.Drawing.Color.White;
			this.cur최대수배수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur최대수배수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur최대수배수.CurrencyDecimalDigits = 4;
			this.cur최대수배수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur최대수배수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur최대수배수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur최대수배수.Location = new System.Drawing.Point(157, 1);
			this.cur최대수배수.MaxLength = 16;
			this.cur최대수배수.Name = "cur최대수배수";
			this.cur최대수배수.NullString = "0";
			this.cur최대수배수.PositiveColor = System.Drawing.Color.Black;
			this.cur최대수배수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur최대수배수.Size = new System.Drawing.Size(185, 21);
			this.cur최대수배수.TabIndex = 13;
			this.cur최대수배수.Tag = "QT_MAX";
			this.cur최대수배수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl90
			// 
			this.bpPanelControl90.Controls.Add(this.m_lblQtMin);
			this.bpPanelControl90.Controls.Add(this.cur최소수배수);
			this.bpPanelControl90.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl90.Name = "bpPanelControl90";
			this.bpPanelControl90.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl90.TabIndex = 2;
			this.bpPanelControl90.Text = "bpPanelControl90";
			// 
			// m_lblQtMin
			// 
			this.m_lblQtMin.BackColor = System.Drawing.Color.Transparent;
			this.m_lblQtMin.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblQtMin.Location = new System.Drawing.Point(0, 3);
			this.m_lblQtMin.Name = "m_lblQtMin";
			this.m_lblQtMin.Size = new System.Drawing.Size(156, 16);
			this.m_lblQtMin.TabIndex = 160;
			this.m_lblQtMin.Tag = "QT_MIN";
			this.m_lblQtMin.Text = "최소수배수";
			this.m_lblQtMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur최소수배수
			// 
			this.cur최소수배수.BackColor = System.Drawing.Color.White;
			this.cur최소수배수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur최소수배수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur최소수배수.CurrencyDecimalDigits = 4;
			this.cur최소수배수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur최소수배수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur최소수배수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur최소수배수.Location = new System.Drawing.Point(157, 1);
			this.cur최소수배수.MaxLength = 16;
			this.cur최소수배수.Name = "cur최소수배수";
			this.cur최소수배수.NullString = "0";
			this.cur최소수배수.PositiveColor = System.Drawing.Color.Black;
			this.cur최소수배수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur최소수배수.Size = new System.Drawing.Size(185, 21);
			this.cur최소수배수.TabIndex = 12;
			this.cur최소수배수.Tag = "QT_MIN";
			this.cur최소수배수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem49
			// 
			this.oneGridItem49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem49.Controls.Add(this.bpPanelControl91);
			this.oneGridItem49.Controls.Add(this.bpPanelControl92);
			this.oneGridItem49.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem49.Location = new System.Drawing.Point(0, 184);
			this.oneGridItem49.Name = "oneGridItem49";
			this.oneGridItem49.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem49.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem49.TabIndex = 8;
			// 
			// bpPanelControl91
			// 
			this.bpPanelControl91.Controls.Add(this.m_lblFgFoq);
			this.bpPanelControl91.Controls.Add(this.cboFOQ오더정리);
			this.bpPanelControl91.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl91.Name = "bpPanelControl91";
			this.bpPanelControl91.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl91.TabIndex = 3;
			this.bpPanelControl91.Text = "bpPanelControl91";
			// 
			// m_lblFgFoq
			// 
			this.m_lblFgFoq.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgFoq.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgFoq.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgFoq.Name = "m_lblFgFoq";
			this.m_lblFgFoq.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgFoq.TabIndex = 176;
			this.m_lblFgFoq.Tag = "FG_FOQ";
			this.m_lblFgFoq.Text = "FOQ오더정리";
			this.m_lblFgFoq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboFOQ오더정리
			// 
			this.cboFOQ오더정리.AutoDropDown = true;
			this.cboFOQ오더정리.BackColor = System.Drawing.Color.White;
			this.cboFOQ오더정리.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFOQ오더정리.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cboFOQ오더정리.ItemHeight = 12;
			this.cboFOQ오더정리.Location = new System.Drawing.Point(157, 1);
			this.cboFOQ오더정리.Name = "cboFOQ오더정리";
			this.cboFOQ오더정리.Size = new System.Drawing.Size(185, 20);
			this.cboFOQ오더정리.TabIndex = 15;
			this.cboFOQ오더정리.Tag = "FG_FOQ";
			// 
			// bpPanelControl92
			// 
			this.bpPanelControl92.Controls.Add(this.m_lblQtFoq);
			this.bpPanelControl92.Controls.Add(this.curFAQ수량);
			this.bpPanelControl92.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl92.Name = "bpPanelControl92";
			this.bpPanelControl92.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl92.TabIndex = 2;
			this.bpPanelControl92.Text = "bpPanelControl92";
			// 
			// m_lblQtFoq
			// 
			this.m_lblQtFoq.BackColor = System.Drawing.Color.Transparent;
			this.m_lblQtFoq.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblQtFoq.Location = new System.Drawing.Point(0, 3);
			this.m_lblQtFoq.Name = "m_lblQtFoq";
			this.m_lblQtFoq.Size = new System.Drawing.Size(156, 16);
			this.m_lblQtFoq.TabIndex = 168;
			this.m_lblQtFoq.Tag = "QT_FOQ";
			this.m_lblQtFoq.Text = "FAQ수량";
			this.m_lblQtFoq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// curFAQ수량
			// 
			this.curFAQ수량.BackColor = System.Drawing.Color.White;
			this.curFAQ수량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.curFAQ수량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.curFAQ수량.CurrencyDecimalDigits = 4;
			this.curFAQ수량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.curFAQ수량.Font = new System.Drawing.Font("굴림체", 9F);
			this.curFAQ수량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.curFAQ수량.Location = new System.Drawing.Point(157, 1);
			this.curFAQ수량.MaxLength = 16;
			this.curFAQ수량.Name = "curFAQ수량";
			this.curFAQ수량.NullString = "0";
			this.curFAQ수량.PositiveColor = System.Drawing.Color.Black;
			this.curFAQ수량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.curFAQ수량.Size = new System.Drawing.Size(185, 21);
			this.curFAQ수량.TabIndex = 14;
			this.curFAQ수량.Tag = "QT_FOQ";
			this.curFAQ수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem50
			// 
			this.oneGridItem50.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem50.Controls.Add(this.bpPanelControl93);
			this.oneGridItem50.Controls.Add(this.bpPanelControl94);
			this.oneGridItem50.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem50.Location = new System.Drawing.Point(0, 207);
			this.oneGridItem50.Name = "oneGridItem50";
			this.oneGridItem50.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem50.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem50.TabIndex = 9;
			// 
			// bpPanelControl93
			// 
			this.bpPanelControl93.Controls.Add(this.lbl표준ST);
			this.bpPanelControl93.Controls.Add(this.cur표준ST);
			this.bpPanelControl93.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl93.Name = "bpPanelControl93";
			this.bpPanelControl93.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl93.TabIndex = 3;
			this.bpPanelControl93.Text = "bpPanelControl93";
			// 
			// lbl표준ST
			// 
			this.lbl표준ST.BackColor = System.Drawing.Color.Transparent;
			this.lbl표준ST.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl표준ST.Location = new System.Drawing.Point(0, 3);
			this.lbl표준ST.Name = "lbl표준ST";
			this.lbl표준ST.Size = new System.Drawing.Size(156, 16);
			this.lbl표준ST.TabIndex = 178;
			this.lbl표준ST.Tag = "FG_FOQ";
			this.lbl표준ST.Text = "표준ST";
			this.lbl표준ST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur표준ST
			// 
			this.cur표준ST.BackColor = System.Drawing.Color.White;
			this.cur표준ST.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur표준ST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur표준ST.CurrencyDecimalDigits = 4;
			this.cur표준ST.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur표준ST.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur표준ST.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur표준ST.Location = new System.Drawing.Point(157, 1);
			this.cur표준ST.MaxLength = 16;
			this.cur표준ST.Name = "cur표준ST";
			this.cur표준ST.NullString = "0";
			this.cur표준ST.PositiveColor = System.Drawing.Color.Black;
			this.cur표준ST.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur표준ST.Size = new System.Drawing.Size(185, 21);
			this.cur표준ST.TabIndex = 17;
			this.cur표준ST.Tag = "STND_ST";
			this.cur표준ST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl94
			// 
			this.bpPanelControl94.Controls.Add(this.m_lblYnPhanTom);
			this.bpPanelControl94.Controls.Add(this.cbo팬텀구분);
			this.bpPanelControl94.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl94.Name = "bpPanelControl94";
			this.bpPanelControl94.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl94.TabIndex = 2;
			this.bpPanelControl94.Text = "bpPanelControl94";
			// 
			// m_lblYnPhanTom
			// 
			this.m_lblYnPhanTom.BackColor = System.Drawing.Color.Transparent;
			this.m_lblYnPhanTom.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblYnPhanTom.Location = new System.Drawing.Point(0, 3);
			this.m_lblYnPhanTom.Name = "m_lblYnPhanTom";
			this.m_lblYnPhanTom.Size = new System.Drawing.Size(156, 16);
			this.m_lblYnPhanTom.TabIndex = 169;
			this.m_lblYnPhanTom.Tag = "YN_PHANTOM";
			this.m_lblYnPhanTom.Text = "Phantom 구분";
			this.m_lblYnPhanTom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo팬텀구분
			// 
			this.cbo팬텀구분.AutoDropDown = true;
			this.cbo팬텀구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo팬텀구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo팬텀구분.ItemHeight = 12;
			this.cbo팬텀구분.Location = new System.Drawing.Point(157, 1);
			this.cbo팬텀구분.Name = "cbo팬텀구분";
			this.cbo팬텀구분.Size = new System.Drawing.Size(185, 20);
			this.cbo팬텀구분.TabIndex = 16;
			this.cbo팬텀구분.Tag = "YN_PHANTOM";
			// 
			// oneGridItem51
			// 
			this.oneGridItem51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem51.Controls.Add(this.bpPanelControl95);
			this.oneGridItem51.Controls.Add(this.bpPanelControl96);
			this.oneGridItem51.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem51.Location = new System.Drawing.Point(0, 230);
			this.oneGridItem51.Name = "oneGridItem51";
			this.oneGridItem51.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem51.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem51.TabIndex = 10;
			// 
			// bpPanelControl95
			// 
			this.bpPanelControl95.Controls.Add(this.m_cboQtRop);
			this.bpPanelControl95.Controls.Add(this.cboROP);
			this.bpPanelControl95.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl95.Name = "bpPanelControl95";
			this.bpPanelControl95.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl95.TabIndex = 3;
			this.bpPanelControl95.Text = "bpPanelControl95";
			// 
			// m_cboQtRop
			// 
			this.m_cboQtRop.BackColor = System.Drawing.Color.Transparent;
			this.m_cboQtRop.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_cboQtRop.Location = new System.Drawing.Point(0, 3);
			this.m_cboQtRop.Name = "m_cboQtRop";
			this.m_cboQtRop.Size = new System.Drawing.Size(156, 16);
			this.m_cboQtRop.TabIndex = 178;
			this.m_cboQtRop.Tag = "QT_ROP";
			this.m_cboQtRop.Text = "ROP";
			this.m_cboQtRop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboROP
			// 
			this.cboROP.BackColor = System.Drawing.Color.White;
			this.cboROP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cboROP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cboROP.CurrencyDecimalDigits = 4;
			this.cboROP.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cboROP.Font = new System.Drawing.Font("굴림체", 9F);
			this.cboROP.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboROP.Location = new System.Drawing.Point(157, 1);
			this.cboROP.MaxLength = 16;
			this.cboROP.Name = "cboROP";
			this.cboROP.NullString = "0";
			this.cboROP.PositiveColor = System.Drawing.Color.Black;
			this.cboROP.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cboROP.Size = new System.Drawing.Size(185, 21);
			this.cboROP.TabIndex = 19;
			this.cboROP.Tag = "QT_ROP";
			this.cboROP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl96
			// 
			this.bpPanelControl96.Controls.Add(this.m_lblFgBf);
			this.bpPanelControl96.Controls.Add(this.cbo백플러쉬);
			this.bpPanelControl96.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl96.Name = "bpPanelControl96";
			this.bpPanelControl96.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl96.TabIndex = 2;
			this.bpPanelControl96.Text = "bpPanelControl96";
			// 
			// m_lblFgBf
			// 
			this.m_lblFgBf.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgBf.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgBf.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgBf.Name = "m_lblFgBf";
			this.m_lblFgBf.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgBf.TabIndex = 162;
			this.m_lblFgBf.Tag = "FG_BF";
			this.m_lblFgBf.Text = "Back Flush";
			this.m_lblFgBf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo백플러쉬
			// 
			this.cbo백플러쉬.AutoDropDown = true;
			this.cbo백플러쉬.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo백플러쉬.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo백플러쉬.ItemHeight = 12;
			this.cbo백플러쉬.Location = new System.Drawing.Point(157, 1);
			this.cbo백플러쉬.Name = "cbo백플러쉬";
			this.cbo백플러쉬.Size = new System.Drawing.Size(185, 20);
			this.cbo백플러쉬.TabIndex = 18;
			this.cbo백플러쉬.Tag = "FG_BF";
			// 
			// oneGridItem52
			// 
			this.oneGridItem52.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem52.Controls.Add(this.bpPanelControl97);
			this.oneGridItem52.Controls.Add(this.bpPanelControl98);
			this.oneGridItem52.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem52.Location = new System.Drawing.Point(0, 253);
			this.oneGridItem52.Name = "oneGridItem52";
			this.oneGridItem52.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem52.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem52.TabIndex = 11;
			// 
			// bpPanelControl97
			// 
			this.bpPanelControl97.Controls.Add(this.m_lblUpd);
			this.bpPanelControl97.Controls.Add(this.cur생산능력);
			this.bpPanelControl97.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl97.Name = "bpPanelControl97";
			this.bpPanelControl97.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl97.TabIndex = 3;
			this.bpPanelControl97.Text = "bpPanelControl97";
			// 
			// m_lblUpd
			// 
			this.m_lblUpd.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUpd.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUpd.ForeColor = System.Drawing.Color.Black;
			this.m_lblUpd.Location = new System.Drawing.Point(0, 3);
			this.m_lblUpd.Name = "m_lblUpd";
			this.m_lblUpd.Size = new System.Drawing.Size(156, 16);
			this.m_lblUpd.TabIndex = 180;
			this.m_lblUpd.Tag = "UPD";
			this.m_lblUpd.Text = "생산능력(UPD)";
			this.m_lblUpd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur생산능력
			// 
			this.cur생산능력.BackColor = System.Drawing.Color.White;
			this.cur생산능력.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur생산능력.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur생산능력.CurrencyDecimalDigits = 4;
			this.cur생산능력.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur생산능력.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur생산능력.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur생산능력.Location = new System.Drawing.Point(157, 1);
			this.cur생산능력.MaxLength = 14;
			this.cur생산능력.Name = "cur생산능력";
			this.cur생산능력.NullString = "0";
			this.cur생산능력.PositiveColor = System.Drawing.Color.Black;
			this.cur생산능력.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur생산능력.Size = new System.Drawing.Size(185, 21);
			this.cur생산능력.TabIndex = 21;
			this.cur생산능력.Tag = "UPD";
			this.cur생산능력.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl98
			// 
			this.bpPanelControl98.Controls.Add(this.m_lblUph);
			this.bpPanelControl98.Controls.Add(this.curUPH);
			this.bpPanelControl98.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl98.Name = "bpPanelControl98";
			this.bpPanelControl98.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl98.TabIndex = 2;
			this.bpPanelControl98.Text = "bpPanelControl98";
			// 
			// m_lblUph
			// 
			this.m_lblUph.BackColor = System.Drawing.Color.Transparent;
			this.m_lblUph.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblUph.ForeColor = System.Drawing.Color.Black;
			this.m_lblUph.Location = new System.Drawing.Point(0, 3);
			this.m_lblUph.Name = "m_lblUph";
			this.m_lblUph.Size = new System.Drawing.Size(156, 16);
			this.m_lblUph.TabIndex = 170;
			this.m_lblUph.Tag = "UPH";
			this.m_lblUph.Text = "UPH";
			this.m_lblUph.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// curUPH
			// 
			this.curUPH.BackColor = System.Drawing.Color.White;
			this.curUPH.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.curUPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.curUPH.CurrencyDecimalDigits = 4;
			this.curUPH.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.curUPH.Font = new System.Drawing.Font("굴림체", 9F);
			this.curUPH.ForeColor = System.Drawing.SystemColors.ControlText;
			this.curUPH.Location = new System.Drawing.Point(157, 1);
			this.curUPH.MaxLength = 14;
			this.curUPH.Name = "curUPH";
			this.curUPH.NullString = "0";
			this.curUPH.PositiveColor = System.Drawing.Color.Black;
			this.curUPH.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.curUPH.Size = new System.Drawing.Size(185, 21);
			this.curUPH.TabIndex = 20;
			this.curUPH.Tag = "UPH";
			this.curUPH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem53
			// 
			this.oneGridItem53.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem53.Controls.Add(this.bpPanelControl99);
			this.oneGridItem53.Controls.Add(this.bpPanelControl81);
			this.oneGridItem53.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem53.Location = new System.Drawing.Point(0, 276);
			this.oneGridItem53.Name = "oneGridItem53";
			this.oneGridItem53.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem53.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem53.TabIndex = 12;
			// 
			// bpPanelControl99
			// 
			this.bpPanelControl99.Controls.Add(this.m_lblRtMinus);
			this.bpPanelControl99.Controls.Add(this.cur과입고허용율음수);
			this.bpPanelControl99.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl99.Name = "bpPanelControl99";
			this.bpPanelControl99.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl99.TabIndex = 1;
			this.bpPanelControl99.Text = "bpPanelControl99";
			// 
			// m_lblRtMinus
			// 
			this.m_lblRtMinus.BackColor = System.Drawing.Color.Transparent;
			this.m_lblRtMinus.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblRtMinus.Location = new System.Drawing.Point(0, 3);
			this.m_lblRtMinus.Name = "m_lblRtMinus";
			this.m_lblRtMinus.Size = new System.Drawing.Size(156, 16);
			this.m_lblRtMinus.TabIndex = 179;
			this.m_lblRtMinus.Tag = "RT_MINUS";
			this.m_lblRtMinus.Text = "과입고허용율(-)";
			this.m_lblRtMinus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur과입고허용율음수
			// 
			this.cur과입고허용율음수.BackColor = System.Drawing.Color.White;
			this.cur과입고허용율음수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur과입고허용율음수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur과입고허용율음수.CurrencyDecimalDigits = 2;
			this.cur과입고허용율음수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur과입고허용율음수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur과입고허용율음수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur과입고허용율음수.Location = new System.Drawing.Point(157, 1);
			this.cur과입고허용율음수.MaxLength = 4;
			this.cur과입고허용율음수.MaxValue = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
			this.cur과입고허용율음수.MinValue = new decimal(new int[] {
            99999,
            0,
            0,
            -2147352576});
			this.cur과입고허용율음수.Name = "cur과입고허용율음수";
			this.cur과입고허용율음수.NullString = "0";
			this.cur과입고허용율음수.PositiveColor = System.Drawing.Color.Black;
			this.cur과입고허용율음수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur과입고허용율음수.Size = new System.Drawing.Size(185, 21);
			this.cur과입고허용율음수.TabIndex = 23;
			this.cur과입고허용율음수.Tag = "RT_MINUS";
			this.cur과입고허용율음수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl81
			// 
			this.bpPanelControl81.Controls.Add(this.m_lblRtPlus);
			this.bpPanelControl81.Controls.Add(this.cur과입고허용율양수);
			this.bpPanelControl81.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl81.Name = "bpPanelControl81";
			this.bpPanelControl81.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl81.TabIndex = 0;
			this.bpPanelControl81.Text = "bpPanelControl81";
			// 
			// m_lblRtPlus
			// 
			this.m_lblRtPlus.BackColor = System.Drawing.Color.Transparent;
			this.m_lblRtPlus.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblRtPlus.Location = new System.Drawing.Point(0, 3);
			this.m_lblRtPlus.Name = "m_lblRtPlus";
			this.m_lblRtPlus.Size = new System.Drawing.Size(156, 16);
			this.m_lblRtPlus.TabIndex = 166;
			this.m_lblRtPlus.Tag = "RT_PLUS";
			this.m_lblRtPlus.Text = "과입고허용율(+)";
			this.m_lblRtPlus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur과입고허용율양수
			// 
			this.cur과입고허용율양수.BackColor = System.Drawing.Color.White;
			this.cur과입고허용율양수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur과입고허용율양수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur과입고허용율양수.CurrencyDecimalDigits = 2;
			this.cur과입고허용율양수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur과입고허용율양수.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur과입고허용율양수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur과입고허용율양수.Location = new System.Drawing.Point(157, 1);
			this.cur과입고허용율양수.MaxLength = 4;
			this.cur과입고허용율양수.MaxValue = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
			this.cur과입고허용율양수.MinValue = new decimal(new int[] {
            99999,
            0,
            0,
            -2147352576});
			this.cur과입고허용율양수.Name = "cur과입고허용율양수";
			this.cur과입고허용율양수.NullString = "0";
			this.cur과입고허용율양수.PositiveColor = System.Drawing.Color.Black;
			this.cur과입고허용율양수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur과입고허용율양수.Size = new System.Drawing.Size(185, 21);
			this.cur과입고허용율양수.TabIndex = 22;
			this.cur과입고허용율양수.Tag = "RT_PLUS";
			this.cur과입고허용율양수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// oneGridItem54
			// 
			this.oneGridItem54.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem54.Controls.Add(this.bpPanelControl100);
			this.oneGridItem54.Controls.Add(this.bpPanelControl101);
			this.oneGridItem54.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem54.Location = new System.Drawing.Point(0, 299);
			this.oneGridItem54.Name = "oneGridItem54";
			this.oneGridItem54.Size = new System.Drawing.Size(746, 23);
			this.oneGridItem54.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem54.TabIndex = 13;
			// 
			// bpPanelControl100
			// 
			this.bpPanelControl100.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl100.Name = "bpPanelControl100";
			this.bpPanelControl100.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl100.TabIndex = 3;
			this.bpPanelControl100.Text = "bpPanelControl100";
			// 
			// bpPanelControl101
			// 
			this.bpPanelControl101.Controls.Add(this.curNEGO금액);
			this.bpPanelControl101.Controls.Add(this.labelExt11);
			this.bpPanelControl101.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl101.Name = "bpPanelControl101";
			this.bpPanelControl101.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl101.TabIndex = 2;
			this.bpPanelControl101.Text = "bpPanelControl101";
			// 
			// curNEGO금액
			// 
			this.curNEGO금액.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.curNEGO금액.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.curNEGO금액.CurrencyDecimalDigits = 4;
			this.curNEGO금액.CurrencyNegativePattern = 2;
			this.curNEGO금액.CurrencyPositivePattern = 2;
			this.curNEGO금액.DecimalValue = new decimal(new int[] {
            10000,
            0,
            0,
            262144});
			this.curNEGO금액.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.curNEGO금액.ForeColor = System.Drawing.Color.Black;
			this.curNEGO금액.Location = new System.Drawing.Point(157, 1);
			this.curNEGO금액.Name = "curNEGO금액";
			this.curNEGO금액.NullString = "0";
			this.curNEGO금액.PositiveColor = System.Drawing.Color.Black;
			this.curNEGO금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.curNEGO금액.Size = new System.Drawing.Size(185, 21);
			this.curNEGO금액.TabIndex = 226;
			this.curNEGO금액.Tag = "UM_ROYALTY";
			this.curNEGO금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.curNEGO금액.UseKeyEnter = false;
			this.curNEGO금액.UseKeyF3 = false;
			// 
			// labelExt11
			// 
			this.labelExt11.BackColor = System.Drawing.Color.Transparent;
			this.labelExt11.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt11.Location = new System.Drawing.Point(0, 3);
			this.labelExt11.Name = "labelExt11";
			this.labelExt11.Size = new System.Drawing.Size(156, 16);
			this.labelExt11.TabIndex = 183;
			this.labelExt11.Text = "ROYALTY";
			this.labelExt11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpg품질
			// 
			this.tpg품질.BackColor = System.Drawing.Color.White;
			this.tpg품질.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tpg품질.Controls.Add(this.tableLayoutPanel4);
			this.tpg품질.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tpg품질.ImageIndex = 1;
			this.tpg품질.Location = new System.Drawing.Point(4, 24);
			this.tpg품질.Name = "tpg품질";
			this.tpg품질.Padding = new System.Windows.Forms.Padding(6);
			this.tpg품질.Size = new System.Drawing.Size(772, 626);
			this.tpg품질.TabIndex = 3;
			this.tpg품질.Tag = "QU";
			this.tpg품질.Text = "품질";
			this.tpg품질.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.imagePanel2, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.pnl첨부파일, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.pnl품질, 0, 0);
			this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 6);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 4;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(739, 586);
			this.tableLayoutPanel4.TabIndex = 243;
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(3, 193);
			this.imagePanel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(733, 25);
			this.imagePanel2.TabIndex = 242;
			this.imagePanel2.TitleText = "첨부파일";
			// 
			// pnl첨부파일
			// 
			this.pnl첨부파일.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.pnl첨부파일.Controls.Add(this.btn파일삭제);
			this.pnl첨부파일.Controls.Add(this.txt첨부파일명);
			this.pnl첨부파일.Controls.Add(this.btn다운로드);
			this.pnl첨부파일.Controls.Add(this.btn업로드);
			this.pnl첨부파일.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl첨부파일.Location = new System.Drawing.Point(3, 219);
			this.pnl첨부파일.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.pnl첨부파일.Name = "pnl첨부파일";
			this.pnl첨부파일.Size = new System.Drawing.Size(733, 35);
			this.pnl첨부파일.TabIndex = 243;
			// 
			// btn파일삭제
			// 
			this.btn파일삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn파일삭제.BackColor = System.Drawing.Color.White;
			this.btn파일삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn파일삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn파일삭제.Location = new System.Drawing.Point(662, 7);
			this.btn파일삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn파일삭제.Name = "btn파일삭제";
			this.btn파일삭제.Size = new System.Drawing.Size(64, 19);
			this.btn파일삭제.TabIndex = 3;
			this.btn파일삭제.TabStop = false;
			this.btn파일삭제.Tag = "ITEM_COPY";
			this.btn파일삭제.Text = "파일삭제";
			this.btn파일삭제.UseVisualStyleBackColor = false;
			// 
			// txt첨부파일명
			// 
			this.txt첨부파일명.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txt첨부파일명.BackColor = System.Drawing.Color.White;
			this.txt첨부파일명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt첨부파일명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt첨부파일명.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt첨부파일명.Location = new System.Drawing.Point(6, 6);
			this.txt첨부파일명.MaxLength = 20;
			this.txt첨부파일명.Name = "txt첨부파일명";
			this.txt첨부파일명.ReadOnly = true;
			this.txt첨부파일명.Size = new System.Drawing.Size(445, 21);
			this.txt첨부파일명.TabIndex = 0;
			this.txt첨부파일명.TabStop = false;
			this.txt첨부파일명.Tag = "FILE_PATH_MNG";
			// 
			// btn다운로드
			// 
			this.btn다운로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn다운로드.BackColor = System.Drawing.Color.White;
			this.btn다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn다운로드.Location = new System.Drawing.Point(548, 7);
			this.btn다운로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn다운로드.Name = "btn다운로드";
			this.btn다운로드.Size = new System.Drawing.Size(112, 19);
			this.btn다운로드.TabIndex = 2;
			this.btn다운로드.TabStop = false;
			this.btn다운로드.Tag = "ITEM_COPY";
			this.btn다운로드.Text = "DOWNLOAD";
			this.btn다운로드.UseVisualStyleBackColor = false;
			// 
			// btn업로드
			// 
			this.btn업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn업로드.BackColor = System.Drawing.Color.White;
			this.btn업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn업로드.Location = new System.Drawing.Point(457, 7);
			this.btn업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn업로드.Name = "btn업로드";
			this.btn업로드.Size = new System.Drawing.Size(89, 19);
			this.btn업로드.TabIndex = 1;
			this.btn업로드.TabStop = false;
			this.btn업로드.Tag = "ITEM_COPY";
			this.btn업로드.Text = "UPLOAD";
			this.btn업로드.UseVisualStyleBackColor = false;
			// 
			// pnl품질
			// 
			this.pnl품질.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl품질.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem55,
            this.oneGridItem56,
            this.oneGridItem57,
            this.oneGridItem58,
            this.oneGridItem59,
            this.oneGridItem60});
			this.pnl품질.Location = new System.Drawing.Point(3, 3);
			this.pnl품질.Name = "pnl품질";
			this.pnl품질.Size = new System.Drawing.Size(733, 184);
			this.pnl품질.TabIndex = 244;
			// 
			// oneGridItem55
			// 
			this.oneGridItem55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem55.Controls.Add(this.bpPanelControl103);
			this.oneGridItem55.Controls.Add(this.bpPanelControl102);
			this.oneGridItem55.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem55.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem55.Name = "oneGridItem55";
			this.oneGridItem55.Size = new System.Drawing.Size(723, 23);
			this.oneGridItem55.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem55.TabIndex = 0;
			// 
			// bpPanelControl103
			// 
			this.bpPanelControl103.Controls.Add(this.m_lblRtQm);
			this.bpPanelControl103.Controls.Add(this.cur품목불량율);
			this.bpPanelControl103.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl103.Name = "bpPanelControl103";
			this.bpPanelControl103.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl103.TabIndex = 1;
			this.bpPanelControl103.Text = "bpPanelControl103";
			// 
			// m_lblRtQm
			// 
			this.m_lblRtQm.BackColor = System.Drawing.Color.Transparent;
			this.m_lblRtQm.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblRtQm.Location = new System.Drawing.Point(0, 3);
			this.m_lblRtQm.Name = "m_lblRtQm";
			this.m_lblRtQm.Size = new System.Drawing.Size(156, 16);
			this.m_lblRtQm.TabIndex = 139;
			this.m_lblRtQm.Tag = "RT_QM";
			this.m_lblRtQm.Text = "품목불량율";
			this.m_lblRtQm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur품목불량율
			// 
			this.cur품목불량율.BackColor = System.Drawing.Color.White;
			this.cur품목불량율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur품목불량율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur품목불량율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur품목불량율.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur품목불량율.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur품목불량율.Location = new System.Drawing.Point(157, 1);
			this.cur품목불량율.MaxLength = 3;
			this.cur품목불량율.Name = "cur품목불량율";
			this.cur품목불량율.NullString = "0";
			this.cur품목불량율.PositiveColor = System.Drawing.Color.Black;
			this.cur품목불량율.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur품목불량율.Size = new System.Drawing.Size(185, 21);
			this.cur품목불량율.TabIndex = 1;
			this.cur품목불량율.Tag = "RT_QM";
			this.cur품목불량율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// bpPanelControl102
			// 
			this.bpPanelControl102.Controls.Add(this.m_lblLtQc);
			this.bpPanelControl102.Controls.Add(this.cur검사LT);
			this.bpPanelControl102.Controls.Add(this.m_lblDay2);
			this.bpPanelControl102.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl102.Name = "bpPanelControl102";
			this.bpPanelControl102.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl102.TabIndex = 0;
			this.bpPanelControl102.Text = "bpPanelControl102";
			// 
			// m_lblLtQc
			// 
			this.m_lblLtQc.BackColor = System.Drawing.Color.Transparent;
			this.m_lblLtQc.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblLtQc.Location = new System.Drawing.Point(0, 3);
			this.m_lblLtQc.Name = "m_lblLtQc";
			this.m_lblLtQc.Size = new System.Drawing.Size(156, 16);
			this.m_lblLtQc.TabIndex = 47;
			this.m_lblLtQc.Tag = "LT_QC";
			this.m_lblLtQc.Text = "검사 L/T";
			this.m_lblLtQc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cur검사LT
			// 
			this.cur검사LT.BackColor = System.Drawing.Color.White;
			this.cur검사LT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur검사LT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur검사LT.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur검사LT.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur검사LT.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur검사LT.Location = new System.Drawing.Point(157, 1);
			this.cur검사LT.MaxLength = 3;
			this.cur검사LT.Name = "cur검사LT";
			this.cur검사LT.NullString = "0";
			this.cur검사LT.PositiveColor = System.Drawing.Color.Black;
			this.cur검사LT.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur검사LT.Size = new System.Drawing.Size(70, 21);
			this.cur검사LT.TabIndex = 0;
			this.cur검사LT.Tag = "LT_QC";
			this.cur검사LT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_lblDay2
			// 
			this.m_lblDay2.BackColor = System.Drawing.Color.Transparent;
			this.m_lblDay2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblDay2.Location = new System.Drawing.Point(227, 3);
			this.m_lblDay2.Name = "m_lblDay2";
			this.m_lblDay2.Size = new System.Drawing.Size(24, 18);
			this.m_lblDay2.TabIndex = 48;
			this.m_lblDay2.Tag = "DAY";
			this.m_lblDay2.Text = "일";
			this.m_lblDay2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// oneGridItem56
			// 
			this.oneGridItem56.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem56.Controls.Add(this.bpPanelControl104);
			this.oneGridItem56.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem56.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem56.Name = "oneGridItem56";
			this.oneGridItem56.Size = new System.Drawing.Size(723, 23);
			this.oneGridItem56.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem56.TabIndex = 1;
			// 
			// bpPanelControl104
			// 
			this.bpPanelControl104.Controls.Add(this.m_lblFgPqc);
			this.bpPanelControl104.Controls.Add(this.cbo수입검사여부);
			this.bpPanelControl104.Controls.Add(this.lbl공정검사);
			this.bpPanelControl104.Controls.Add(this.cbo공정검사);
			this.bpPanelControl104.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl104.Name = "bpPanelControl104";
			this.bpPanelControl104.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl104.TabIndex = 0;
			this.bpPanelControl104.Text = "bpPanelControl104";
			// 
			// m_lblFgPqc
			// 
			this.m_lblFgPqc.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgPqc.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgPqc.ForeColor = System.Drawing.Color.Black;
			this.m_lblFgPqc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.m_lblFgPqc.Location = new System.Drawing.Point(3, 3);
			this.m_lblFgPqc.Name = "m_lblFgPqc";
			this.m_lblFgPqc.Size = new System.Drawing.Size(153, 18);
			this.m_lblFgPqc.TabIndex = 2;
			this.m_lblFgPqc.Tag = "FG_PQC";
			this.m_lblFgPqc.Text = "수 입 검 사";
			this.m_lblFgPqc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo수입검사여부
			// 
			this.cbo수입검사여부.AutoDropDown = true;
			this.cbo수입검사여부.BackColor = System.Drawing.Color.White;
			this.cbo수입검사여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo수입검사여부.ItemHeight = 12;
			this.cbo수입검사여부.Location = new System.Drawing.Point(157, 2);
			this.cbo수입검사여부.Name = "cbo수입검사여부";
			this.cbo수입검사여부.Size = new System.Drawing.Size(185, 20);
			this.cbo수입검사여부.TabIndex = 2;
			this.cbo수입검사여부.Tag = "FG_IQC";
			// 
			// lbl공정검사
			// 
			this.lbl공정검사.BackColor = System.Drawing.Color.Transparent;
			this.lbl공정검사.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl공정검사.ForeColor = System.Drawing.Color.Black;
			this.lbl공정검사.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl공정검사.Location = new System.Drawing.Point(344, 3);
			this.lbl공정검사.Name = "lbl공정검사";
			this.lbl공정검사.Size = new System.Drawing.Size(156, 16);
			this.lbl공정검사.TabIndex = 237;
			this.lbl공정검사.Tag = "FG_MQC";
			this.lbl공정검사.Text = "공 정 검 사";
			this.lbl공정검사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공정검사
			// 
			this.cbo공정검사.AutoDropDown = true;
			this.cbo공정검사.BackColor = System.Drawing.Color.White;
			this.cbo공정검사.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공정검사.ItemHeight = 12;
			this.cbo공정검사.Location = new System.Drawing.Point(501, 1);
			this.cbo공정검사.Name = "cbo공정검사";
			this.cbo공정검사.Size = new System.Drawing.Size(185, 20);
			this.cbo공정검사.TabIndex = 236;
			this.cbo공정검사.Tag = "FG_OPQC";
			// 
			// oneGridItem57
			// 
			this.oneGridItem57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem57.Controls.Add(this.bpPanelControl105);
			this.oneGridItem57.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem57.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem57.Name = "oneGridItem57";
			this.oneGridItem57.Size = new System.Drawing.Size(723, 23);
			this.oneGridItem57.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem57.TabIndex = 2;
			// 
			// bpPanelControl105
			// 
			this.bpPanelControl105.Controls.Add(this.m_lblFgMqc);
			this.bpPanelControl105.Controls.Add(this.cbo출하검사여부);
			this.bpPanelControl105.Controls.Add(this.cbo이동검사);
			this.bpPanelControl105.Controls.Add(this.lbl이동검사);
			this.bpPanelControl105.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl105.Name = "bpPanelControl105";
			this.bpPanelControl105.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl105.TabIndex = 1;
			this.bpPanelControl105.Text = "bpPanelControl105";
			// 
			// m_lblFgMqc
			// 
			this.m_lblFgMqc.BackColor = System.Drawing.Color.Transparent;
			this.m_lblFgMqc.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblFgMqc.ForeColor = System.Drawing.Color.Black;
			this.m_lblFgMqc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.m_lblFgMqc.Location = new System.Drawing.Point(0, 3);
			this.m_lblFgMqc.Name = "m_lblFgMqc";
			this.m_lblFgMqc.Size = new System.Drawing.Size(156, 16);
			this.m_lblFgMqc.TabIndex = 56;
			this.m_lblFgMqc.Tag = "FG_MQC";
			this.m_lblFgMqc.Text = "출 하 검 사";
			this.m_lblFgMqc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo출하검사여부
			// 
			this.cbo출하검사여부.AutoDropDown = true;
			this.cbo출하검사여부.BackColor = System.Drawing.Color.White;
			this.cbo출하검사여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo출하검사여부.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo출하검사여부.ItemHeight = 12;
			this.cbo출하검사여부.Location = new System.Drawing.Point(157, 1);
			this.cbo출하검사여부.Name = "cbo출하검사여부";
			this.cbo출하검사여부.Size = new System.Drawing.Size(185, 20);
			this.cbo출하검사여부.TabIndex = 3;
			this.cbo출하검사여부.Tag = "FG_SQC";
			// 
			// cbo이동검사
			// 
			this.cbo이동검사.AutoDropDown = true;
			this.cbo이동검사.BackColor = System.Drawing.Color.White;
			this.cbo이동검사.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo이동검사.ItemHeight = 12;
			this.cbo이동검사.Location = new System.Drawing.Point(501, 1);
			this.cbo이동검사.Name = "cbo이동검사";
			this.cbo이동검사.Size = new System.Drawing.Size(185, 20);
			this.cbo이동검사.TabIndex = 239;
			this.cbo이동검사.Tag = "FG_SLQC";
			// 
			// lbl이동검사
			// 
			this.lbl이동검사.BackColor = System.Drawing.Color.Transparent;
			this.lbl이동검사.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl이동검사.ForeColor = System.Drawing.Color.Black;
			this.lbl이동검사.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl이동검사.Location = new System.Drawing.Point(344, 3);
			this.lbl이동검사.Name = "lbl이동검사";
			this.lbl이동검사.Size = new System.Drawing.Size(156, 16);
			this.lbl이동검사.TabIndex = 238;
			this.lbl이동검사.Tag = "FG_MQC";
			this.lbl이동검사.Text = "이 동 검 사";
			this.lbl이동검사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem58
			// 
			this.oneGridItem58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem58.Controls.Add(this.bpPanelControl106);
			this.oneGridItem58.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem58.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem58.Name = "oneGridItem58";
			this.oneGridItem58.Size = new System.Drawing.Size(723, 23);
			this.oneGridItem58.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem58.TabIndex = 3;
			// 
			// bpPanelControl106
			// 
			this.bpPanelControl106.Controls.Add(this.labelExt4);
			this.bpPanelControl106.Controls.Add(this.cbo생산입고검사불량);
			this.bpPanelControl106.Controls.Add(this.cbo생산입고검사);
			this.bpPanelControl106.Controls.Add(this.labelExt3);
			this.bpPanelControl106.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl106.Name = "bpPanelControl106";
			this.bpPanelControl106.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl106.TabIndex = 1;
			this.bpPanelControl106.Text = "bpPanelControl106";
			// 
			// labelExt4
			// 
			this.labelExt4.BackColor = System.Drawing.Color.Transparent;
			this.labelExt4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt4.ForeColor = System.Drawing.Color.Black;
			this.labelExt4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelExt4.Location = new System.Drawing.Point(0, 3);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(156, 16);
			this.labelExt4.TabIndex = 141;
			this.labelExt4.Tag = "FG_MQC";
			this.labelExt4.Text = "생산입고검사";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo생산입고검사불량
			// 
			this.cbo생산입고검사불량.AutoDropDown = true;
			this.cbo생산입고검사불량.BackColor = System.Drawing.Color.White;
			this.cbo생산입고검사불량.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo생산입고검사불량.ItemHeight = 12;
			this.cbo생산입고검사불량.Location = new System.Drawing.Point(501, 1);
			this.cbo생산입고검사불량.Name = "cbo생산입고검사불량";
			this.cbo생산입고검사불량.Size = new System.Drawing.Size(185, 20);
			this.cbo생산입고검사불량.TabIndex = 243;
			this.cbo생산입고검사불량.Tag = "FG_PQCB";
			// 
			// cbo생산입고검사
			// 
			this.cbo생산입고검사.AutoDropDown = true;
			this.cbo생산입고검사.BackColor = System.Drawing.Color.White;
			this.cbo생산입고검사.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo생산입고검사.ItemHeight = 12;
			this.cbo생산입고검사.Location = new System.Drawing.Point(157, 1);
			this.cbo생산입고검사.Name = "cbo생산입고검사";
			this.cbo생산입고검사.Size = new System.Drawing.Size(185, 20);
			this.cbo생산입고검사.TabIndex = 4;
			this.cbo생산입고검사.Tag = "FG_PQC";
			// 
			// labelExt3
			// 
			this.labelExt3.BackColor = System.Drawing.Color.Transparent;
			this.labelExt3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt3.ForeColor = System.Drawing.Color.Black;
			this.labelExt3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelExt3.Location = new System.Drawing.Point(344, 3);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(156, 16);
			this.labelExt3.TabIndex = 242;
			this.labelExt3.Text = "생산입고검사(불량)";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem59
			// 
			this.oneGridItem59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem59.Controls.Add(this.bpPanelControl107);
			this.oneGridItem59.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem59.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem59.Name = "oneGridItem59";
			this.oneGridItem59.Size = new System.Drawing.Size(723, 23);
			this.oneGridItem59.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem59.TabIndex = 4;
			// 
			// bpPanelControl107
			// 
			this.bpPanelControl107.Controls.Add(this.cbo수입검사레벨);
			this.bpPanelControl107.Controls.Add(this.lbl수입검사레벨);
			this.bpPanelControl107.Controls.Add(this.labelExt5);
			this.bpPanelControl107.Controls.Add(this.cbo외주검사);
			this.bpPanelControl107.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl107.Name = "bpPanelControl107";
			this.bpPanelControl107.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl107.TabIndex = 1;
			this.bpPanelControl107.Text = "bpPanelControl107";
			// 
			// cbo수입검사레벨
			// 
			this.cbo수입검사레벨.AutoDropDown = true;
			this.cbo수입검사레벨.BackColor = System.Drawing.Color.White;
			this.cbo수입검사레벨.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo수입검사레벨.ItemHeight = 12;
			this.cbo수입검사레벨.Location = new System.Drawing.Point(501, 1);
			this.cbo수입검사레벨.Name = "cbo수입검사레벨";
			this.cbo수입검사레벨.Size = new System.Drawing.Size(185, 20);
			this.cbo수입검사레벨.TabIndex = 241;
			this.cbo수입검사레벨.Tag = "FG_IQCL";
			// 
			// lbl수입검사레벨
			// 
			this.lbl수입검사레벨.BackColor = System.Drawing.Color.Transparent;
			this.lbl수입검사레벨.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl수입검사레벨.ForeColor = System.Drawing.Color.Black;
			this.lbl수입검사레벨.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl수입검사레벨.Location = new System.Drawing.Point(344, 4);
			this.lbl수입검사레벨.Name = "lbl수입검사레벨";
			this.lbl수입검사레벨.Size = new System.Drawing.Size(156, 16);
			this.lbl수입검사레벨.TabIndex = 240;
			this.lbl수입검사레벨.Text = "수입검사레벨";
			this.lbl수입검사레벨.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelExt5
			// 
			this.labelExt5.BackColor = System.Drawing.Color.Transparent;
			this.labelExt5.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt5.ForeColor = System.Drawing.Color.Black;
			this.labelExt5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelExt5.Location = new System.Drawing.Point(0, 3);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(156, 16);
			this.labelExt5.TabIndex = 142;
			this.labelExt5.Tag = "FG_MQC";
			this.labelExt5.Text = "외 주 검 사";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo외주검사
			// 
			this.cbo외주검사.AutoDropDown = true;
			this.cbo외주검사.BackColor = System.Drawing.Color.White;
			this.cbo외주검사.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo외주검사.ItemHeight = 12;
			this.cbo외주검사.Location = new System.Drawing.Point(157, 1);
			this.cbo외주검사.Name = "cbo외주검사";
			this.cbo외주검사.Size = new System.Drawing.Size(185, 20);
			this.cbo외주검사.TabIndex = 5;
			this.cbo외주검사.Tag = "FG_OQC";
			// 
			// oneGridItem60
			// 
			this.oneGridItem60.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem60.Controls.Add(this.bpPanelControl108);
			this.oneGridItem60.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem60.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem60.Name = "oneGridItem60";
			this.oneGridItem60.Size = new System.Drawing.Size(723, 23);
			this.oneGridItem60.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem60.TabIndex = 5;
			// 
			// bpPanelControl108
			// 
			this.bpPanelControl108.Controls.Add(this.m_lblNoStnd);
			this.bpPanelControl108.Controls.Add(this.txt규격번호);
			this.bpPanelControl108.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl108.Name = "bpPanelControl108";
			this.bpPanelControl108.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl108.TabIndex = 1;
			this.bpPanelControl108.Text = "bpPanelControl108";
			// 
			// m_lblNoStnd
			// 
			this.m_lblNoStnd.BackColor = System.Drawing.Color.Transparent;
			this.m_lblNoStnd.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.m_lblNoStnd.Location = new System.Drawing.Point(0, 3);
			this.m_lblNoStnd.Name = "m_lblNoStnd";
			this.m_lblNoStnd.Size = new System.Drawing.Size(156, 16);
			this.m_lblNoStnd.TabIndex = 46;
			this.m_lblNoStnd.Tag = "NO_STND";
			this.m_lblNoStnd.Text = "규격번호";
			this.m_lblNoStnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt규격번호
			// 
			this.txt규격번호.BackColor = System.Drawing.Color.White;
			this.txt규격번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt규격번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt규격번호.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt규격번호.Location = new System.Drawing.Point(157, 1);
			this.txt규격번호.MaxLength = 20;
			this.txt규격번호.Name = "txt규격번호";
			this.txt규격번호.Size = new System.Drawing.Size(185, 21);
			this.txt규격번호.TabIndex = 6;
			this.txt규격번호.Tag = "NO_STND";
			// 
			// tpg기타
			// 
			this.tpg기타.AutoScroll = true;
			this.tpg기타.BackColor = System.Drawing.Color.White;
			this.tpg기타.Controls.Add(this.pnl기타정보);
			this.tpg기타.ImageIndex = 1;
			this.tpg기타.Location = new System.Drawing.Point(4, 24);
			this.tpg기타.Name = "tpg기타";
			this.tpg기타.Padding = new System.Windows.Forms.Padding(6);
			this.tpg기타.Size = new System.Drawing.Size(772, 626);
			this.tpg기타.TabIndex = 4;
			this.tpg기타.Text = "기타정보";
			this.tpg기타.UseVisualStyleBackColor = true;
			// 
			// pnl기타정보
			// 
			this.pnl기타정보.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl기타정보.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem61,
            this.oneGridItem62,
            this.oneGridItem63,
            this.oneGridItem64,
            this.oneGridItem65,
            this.oneGridItem66,
            this.oneGridItem67,
            this.oneGridItem68,
            this.oneGridItem69,
            this.oneGridItem70,
            this.oneGridItem71,
            this.oneGridItem72,
            this.oneGridItem73,
            this.oneGridItem74,
            this.oneGridItem75,
            this.oneGridItem76,
            this.oneGridItem77,
            this.oneGridItem78,
            this.oneGridItem79,
            this.oneGridItem80,
            this.oneGridItem98,
            this.oneGridItem102});
			this.pnl기타정보.Location = new System.Drawing.Point(6, 6);
			this.pnl기타정보.Name = "pnl기타정보";
			this.pnl기타정보.Size = new System.Drawing.Size(760, 597);
			this.pnl기타정보.TabIndex = 1;
			// 
			// oneGridItem61
			// 
			this.oneGridItem61.Controls.Add(this.bpPanelControl109);
			this.oneGridItem61.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGridItem61.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem61.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem61.Name = "oneGridItem61";
			this.oneGridItem61.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem61.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem61.TabIndex = 0;
			// 
			// bpPanelControl109
			// 
			this.bpPanelControl109.Controls.Add(this.lbl사용자정의1);
			this.bpPanelControl109.Controls.Add(this.txt사용자정의1);
			this.bpPanelControl109.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl109.Name = "bpPanelControl109";
			this.bpPanelControl109.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl109.TabIndex = 0;
			this.bpPanelControl109.Text = "bpPanelControl109";
			// 
			// lbl사용자정의1
			// 
			this.lbl사용자정의1.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의1.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의1.Name = "lbl사용자정의1";
			this.lbl사용자정의1.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의1.TabIndex = 38;
			this.lbl사용자정의1.Tag = "CD_PURGRP";
			this.lbl사용자정의1.Text = "사용자정의1";
			this.lbl사용자정의1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의1.Visible = false;
			// 
			// txt사용자정의1
			// 
			this.txt사용자정의1.BackColor = System.Drawing.Color.White;
			this.txt사용자정의1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt사용자정의1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt사용자정의1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt사용자정의1.Location = new System.Drawing.Point(157, 1);
			this.txt사용자정의1.MaxLength = 100;
			this.txt사용자정의1.Name = "txt사용자정의1";
			this.txt사용자정의1.Size = new System.Drawing.Size(185, 21);
			this.txt사용자정의1.TabIndex = 0;
			this.txt사용자정의1.Tag = "NM_USERDEF1";
			this.txt사용자정의1.Visible = false;
			// 
			// oneGridItem62
			// 
			this.oneGridItem62.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem62.Controls.Add(this.bpPanelControl110);
			this.oneGridItem62.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem62.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem62.Name = "oneGridItem62";
			this.oneGridItem62.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem62.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem62.TabIndex = 1;
			// 
			// bpPanelControl110
			// 
			this.bpPanelControl110.Controls.Add(this.lbl사용자정의2);
			this.bpPanelControl110.Controls.Add(this.txt사용자정의2);
			this.bpPanelControl110.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl110.Name = "bpPanelControl110";
			this.bpPanelControl110.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl110.TabIndex = 1;
			this.bpPanelControl110.Text = "bpPanelControl110";
			// 
			// lbl사용자정의2
			// 
			this.lbl사용자정의2.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의2.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의2.Name = "lbl사용자정의2";
			this.lbl사용자정의2.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의2.TabIndex = 39;
			this.lbl사용자정의2.Tag = "QT_SSTOCK";
			this.lbl사용자정의2.Text = "사용자정의2";
			this.lbl사용자정의2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의2.Visible = false;
			// 
			// txt사용자정의2
			// 
			this.txt사용자정의2.BackColor = System.Drawing.Color.White;
			this.txt사용자정의2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt사용자정의2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt사용자정의2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt사용자정의2.Location = new System.Drawing.Point(157, 1);
			this.txt사용자정의2.MaxLength = 100;
			this.txt사용자정의2.Name = "txt사용자정의2";
			this.txt사용자정의2.Size = new System.Drawing.Size(185, 21);
			this.txt사용자정의2.TabIndex = 1;
			this.txt사용자정의2.Tag = "NM_USERDEF2";
			this.txt사용자정의2.Visible = false;
			// 
			// oneGridItem63
			// 
			this.oneGridItem63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem63.Controls.Add(this.bpPanelControl111);
			this.oneGridItem63.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem63.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem63.Name = "oneGridItem63";
			this.oneGridItem63.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem63.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem63.TabIndex = 2;
			// 
			// bpPanelControl111
			// 
			this.bpPanelControl111.Controls.Add(this.lbl사용자정의3);
			this.bpPanelControl111.Controls.Add(this.txt사용자정의3);
			this.bpPanelControl111.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl111.Name = "bpPanelControl111";
			this.bpPanelControl111.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl111.TabIndex = 1;
			this.bpPanelControl111.Text = "bpPanelControl111";
			// 
			// lbl사용자정의3
			// 
			this.lbl사용자정의3.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의3.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의3.Name = "lbl사용자정의3";
			this.lbl사용자정의3.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의3.TabIndex = 56;
			this.lbl사용자정의3.Tag = "SAFELT";
			this.lbl사용자정의3.Text = "사용자정의3";
			this.lbl사용자정의3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의3.Visible = false;
			// 
			// txt사용자정의3
			// 
			this.txt사용자정의3.BackColor = System.Drawing.Color.White;
			this.txt사용자정의3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt사용자정의3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt사용자정의3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt사용자정의3.Location = new System.Drawing.Point(157, 1);
			this.txt사용자정의3.MaxLength = 100;
			this.txt사용자정의3.Name = "txt사용자정의3";
			this.txt사용자정의3.Size = new System.Drawing.Size(185, 21);
			this.txt사용자정의3.TabIndex = 2;
			this.txt사용자정의3.Tag = "NM_USERDEF3";
			this.txt사용자정의3.Visible = false;
			// 
			// oneGridItem64
			// 
			this.oneGridItem64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem64.Controls.Add(this.bpPanelControl112);
			this.oneGridItem64.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem64.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem64.Name = "oneGridItem64";
			this.oneGridItem64.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem64.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem64.TabIndex = 3;
			// 
			// bpPanelControl112
			// 
			this.bpPanelControl112.Controls.Add(this.lbl사용자정의4);
			this.bpPanelControl112.Controls.Add(this.txt사용자정의4);
			this.bpPanelControl112.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl112.Name = "bpPanelControl112";
			this.bpPanelControl112.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl112.TabIndex = 1;
			this.bpPanelControl112.Text = "bpPanelControl112";
			// 
			// lbl사용자정의4
			// 
			this.lbl사용자정의4.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의4.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의4.Name = "lbl사용자정의4";
			this.lbl사용자정의4.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의4.TabIndex = 40;
			this.lbl사용자정의4.Tag = "FG_ABC";
			this.lbl사용자정의4.Text = "사용자정의4";
			this.lbl사용자정의4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의4.Visible = false;
			// 
			// txt사용자정의4
			// 
			this.txt사용자정의4.BackColor = System.Drawing.Color.White;
			this.txt사용자정의4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt사용자정의4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt사용자정의4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt사용자정의4.Location = new System.Drawing.Point(157, 1);
			this.txt사용자정의4.MaxLength = 100;
			this.txt사용자정의4.Name = "txt사용자정의4";
			this.txt사용자정의4.Size = new System.Drawing.Size(185, 21);
			this.txt사용자정의4.TabIndex = 3;
			this.txt사용자정의4.Tag = "NM_USERDEF4";
			this.txt사용자정의4.Visible = false;
			// 
			// oneGridItem65
			// 
			this.oneGridItem65.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem65.Controls.Add(this.bpPanelControl113);
			this.oneGridItem65.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem65.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem65.Name = "oneGridItem65";
			this.oneGridItem65.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem65.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem65.TabIndex = 4;
			// 
			// bpPanelControl113
			// 
			this.bpPanelControl113.Controls.Add(this.lbl사용자정의5);
			this.bpPanelControl113.Controls.Add(this.txt사용자정의5);
			this.bpPanelControl113.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl113.Name = "bpPanelControl113";
			this.bpPanelControl113.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl113.TabIndex = 1;
			this.bpPanelControl113.Text = "bpPanelControl113";
			// 
			// lbl사용자정의5
			// 
			this.lbl사용자정의5.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의5.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의5.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의5.Name = "lbl사용자정의5";
			this.lbl사용자정의5.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의5.TabIndex = 63;
			this.lbl사용자정의5.Tag = "CD_GRSL";
			this.lbl사용자정의5.Text = "사용자정의5";
			this.lbl사용자정의5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의5.Visible = false;
			// 
			// txt사용자정의5
			// 
			this.txt사용자정의5.BackColor = System.Drawing.Color.White;
			this.txt사용자정의5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt사용자정의5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt사용자정의5.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt사용자정의5.Location = new System.Drawing.Point(157, 1);
			this.txt사용자정의5.MaxLength = 100;
			this.txt사용자정의5.Name = "txt사용자정의5";
			this.txt사용자정의5.Size = new System.Drawing.Size(185, 21);
			this.txt사용자정의5.TabIndex = 4;
			this.txt사용자정의5.Tag = "NM_USERDEF5";
			this.txt사용자정의5.Visible = false;
			// 
			// oneGridItem66
			// 
			this.oneGridItem66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem66.Controls.Add(this.bpPanelControl115);
			this.oneGridItem66.Controls.Add(this.bpPanelControl114);
			this.oneGridItem66.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem66.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem66.Name = "oneGridItem66";
			this.oneGridItem66.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem66.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem66.TabIndex = 5;
			// 
			// bpPanelControl115
			// 
			this.bpPanelControl115.Controls.Add(this.lbl사용자정의7);
			this.bpPanelControl115.Controls.Add(this.cbo사용자정의7);
			this.bpPanelControl115.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl115.Name = "bpPanelControl115";
			this.bpPanelControl115.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl115.TabIndex = 2;
			this.bpPanelControl115.Text = "bpPanelControl115";
			// 
			// lbl사용자정의7
			// 
			this.lbl사용자정의7.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의7.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의7.Name = "lbl사용자정의7";
			this.lbl사용자정의7.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의7.TabIndex = 63;
			this.lbl사용자정의7.Text = "사용자정의7";
			this.lbl사용자정의7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의7.Visible = false;
			// 
			// cbo사용자정의7
			// 
			this.cbo사용자정의7.AutoDropDown = true;
			this.cbo사용자정의7.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의7.ItemHeight = 12;
			this.cbo사용자정의7.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의7.Name = "cbo사용자정의7";
			this.cbo사용자정의7.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의7.TabIndex = 6;
			this.cbo사용자정의7.Tag = "CD_USERDEF2";
			this.cbo사용자정의7.Visible = false;
			// 
			// bpPanelControl114
			// 
			this.bpPanelControl114.Controls.Add(this.lbl사용자정의6);
			this.bpPanelControl114.Controls.Add(this.cbo사용자정의6);
			this.bpPanelControl114.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl114.Name = "bpPanelControl114";
			this.bpPanelControl114.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl114.TabIndex = 1;
			this.bpPanelControl114.Text = "bpPanelControl114";
			// 
			// lbl사용자정의6
			// 
			this.lbl사용자정의6.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의6.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의6.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의6.Name = "lbl사용자정의6";
			this.lbl사용자정의6.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의6.TabIndex = 62;
			this.lbl사용자정의6.Text = "사용자정의6";
			this.lbl사용자정의6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의6.Visible = false;
			// 
			// cbo사용자정의6
			// 
			this.cbo사용자정의6.AutoDropDown = true;
			this.cbo사용자정의6.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의6.ItemHeight = 12;
			this.cbo사용자정의6.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의6.Name = "cbo사용자정의6";
			this.cbo사용자정의6.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의6.TabIndex = 5;
			this.cbo사용자정의6.Tag = "CD_USERDEF1";
			this.cbo사용자정의6.Visible = false;
			// 
			// oneGridItem67
			// 
			this.oneGridItem67.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem67.Controls.Add(this.bpPanelControl116);
			this.oneGridItem67.Controls.Add(this.bpPanelControl117);
			this.oneGridItem67.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem67.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem67.Name = "oneGridItem67";
			this.oneGridItem67.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem67.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem67.TabIndex = 6;
			// 
			// bpPanelControl116
			// 
			this.bpPanelControl116.Controls.Add(this.lbl사용자정의9);
			this.bpPanelControl116.Controls.Add(this.cbo사용자정의9);
			this.bpPanelControl116.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl116.Name = "bpPanelControl116";
			this.bpPanelControl116.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl116.TabIndex = 4;
			this.bpPanelControl116.Text = "bpPanelControl116";
			// 
			// lbl사용자정의9
			// 
			this.lbl사용자정의9.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의9.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의9.Name = "lbl사용자정의9";
			this.lbl사용자정의9.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의9.TabIndex = 198;
			this.lbl사용자정의9.Text = "사용자정의9";
			this.lbl사용자정의9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의9.Visible = false;
			// 
			// cbo사용자정의9
			// 
			this.cbo사용자정의9.AutoDropDown = true;
			this.cbo사용자정의9.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의9.ItemHeight = 12;
			this.cbo사용자정의9.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의9.Name = "cbo사용자정의9";
			this.cbo사용자정의9.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의9.TabIndex = 8;
			this.cbo사용자정의9.Tag = "CD_USERDEF4";
			this.cbo사용자정의9.Visible = false;
			// 
			// bpPanelControl117
			// 
			this.bpPanelControl117.Controls.Add(this.lbl사용자정의8);
			this.bpPanelControl117.Controls.Add(this.cbo사용자정의8);
			this.bpPanelControl117.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl117.Name = "bpPanelControl117";
			this.bpPanelControl117.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl117.TabIndex = 3;
			this.bpPanelControl117.Text = "bpPanelControl117";
			// 
			// lbl사용자정의8
			// 
			this.lbl사용자정의8.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의8.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의8.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의8.Name = "lbl사용자정의8";
			this.lbl사용자정의8.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의8.TabIndex = 67;
			this.lbl사용자정의8.Tag = "CD_CIM";
			this.lbl사용자정의8.Text = "사용자정의8";
			this.lbl사용자정의8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의8.Visible = false;
			// 
			// cbo사용자정의8
			// 
			this.cbo사용자정의8.AutoDropDown = true;
			this.cbo사용자정의8.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의8.ItemHeight = 12;
			this.cbo사용자정의8.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의8.Name = "cbo사용자정의8";
			this.cbo사용자정의8.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의8.TabIndex = 7;
			this.cbo사용자정의8.Tag = "CD_USERDEF3";
			this.cbo사용자정의8.Visible = false;
			// 
			// oneGridItem68
			// 
			this.oneGridItem68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem68.Controls.Add(this.bpPanelControl118);
			this.oneGridItem68.Controls.Add(this.bpPanelControl119);
			this.oneGridItem68.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem68.Location = new System.Drawing.Point(0, 161);
			this.oneGridItem68.Name = "oneGridItem68";
			this.oneGridItem68.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem68.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem68.TabIndex = 7;
			// 
			// bpPanelControl118
			// 
			this.bpPanelControl118.Controls.Add(this.lbl사용자정의11);
			this.bpPanelControl118.Controls.Add(this.cbo사용자정의11);
			this.bpPanelControl118.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl118.Name = "bpPanelControl118";
			this.bpPanelControl118.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl118.TabIndex = 4;
			this.bpPanelControl118.Text = "bpPanelControl118";
			// 
			// lbl사용자정의11
			// 
			this.lbl사용자정의11.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의11.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의11.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의11.Name = "lbl사용자정의11";
			this.lbl사용자정의11.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의11.TabIndex = 198;
			this.lbl사용자정의11.Text = "사용자정의11";
			this.lbl사용자정의11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의11.Visible = false;
			// 
			// cbo사용자정의11
			// 
			this.cbo사용자정의11.AutoDropDown = true;
			this.cbo사용자정의11.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의11.ItemHeight = 12;
			this.cbo사용자정의11.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의11.Name = "cbo사용자정의11";
			this.cbo사용자정의11.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의11.TabIndex = 10;
			this.cbo사용자정의11.Tag = "CD_USERDEF6";
			this.cbo사용자정의11.Visible = false;
			// 
			// bpPanelControl119
			// 
			this.bpPanelControl119.Controls.Add(this.lbl사용자정의10);
			this.bpPanelControl119.Controls.Add(this.cbo사용자정의10);
			this.bpPanelControl119.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl119.Name = "bpPanelControl119";
			this.bpPanelControl119.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl119.TabIndex = 3;
			this.bpPanelControl119.Text = "bpPanelControl119";
			// 
			// lbl사용자정의10
			// 
			this.lbl사용자정의10.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의10.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의10.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의10.Name = "lbl사용자정의10";
			this.lbl사용자정의10.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의10.TabIndex = 43;
			this.lbl사용자정의10.Tag = "FG_GIR";
			this.lbl사용자정의10.Text = "사용자정의10";
			this.lbl사용자정의10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의10.Visible = false;
			// 
			// cbo사용자정의10
			// 
			this.cbo사용자정의10.AutoDropDown = true;
			this.cbo사용자정의10.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의10.ItemHeight = 12;
			this.cbo사용자정의10.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의10.Name = "cbo사용자정의10";
			this.cbo사용자정의10.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의10.TabIndex = 9;
			this.cbo사용자정의10.Tag = "CD_USERDEF5";
			this.cbo사용자정의10.Visible = false;
			// 
			// oneGridItem69
			// 
			this.oneGridItem69.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem69.Controls.Add(this.bpPanelControl120);
			this.oneGridItem69.Controls.Add(this.bpPanelControl121);
			this.oneGridItem69.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem69.Location = new System.Drawing.Point(0, 184);
			this.oneGridItem69.Name = "oneGridItem69";
			this.oneGridItem69.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem69.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem69.TabIndex = 8;
			// 
			// bpPanelControl120
			// 
			this.bpPanelControl120.Controls.Add(this.lbl사용자정의13);
			this.bpPanelControl120.Controls.Add(this.cbo사용자정의13);
			this.bpPanelControl120.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl120.Name = "bpPanelControl120";
			this.bpPanelControl120.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl120.TabIndex = 4;
			this.bpPanelControl120.Text = "bpPanelControl120";
			// 
			// lbl사용자정의13
			// 
			this.lbl사용자정의13.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의13.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의13.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의13.Name = "lbl사용자정의13";
			this.lbl사용자정의13.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의13.TabIndex = 198;
			this.lbl사용자정의13.Text = "사용자정의13";
			this.lbl사용자정의13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의13.Visible = false;
			// 
			// cbo사용자정의13
			// 
			this.cbo사용자정의13.AutoDropDown = true;
			this.cbo사용자정의13.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의13.ItemHeight = 12;
			this.cbo사용자정의13.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의13.Name = "cbo사용자정의13";
			this.cbo사용자정의13.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의13.TabIndex = 12;
			this.cbo사용자정의13.Tag = "CD_USERDEF8";
			this.cbo사용자정의13.Visible = false;
			// 
			// bpPanelControl121
			// 
			this.bpPanelControl121.Controls.Add(this.lbl사용자정의12);
			this.bpPanelControl121.Controls.Add(this.cbo사용자정의12);
			this.bpPanelControl121.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl121.Name = "bpPanelControl121";
			this.bpPanelControl121.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl121.TabIndex = 3;
			this.bpPanelControl121.Text = "bpPanelControl121";
			// 
			// lbl사용자정의12
			// 
			this.lbl사용자정의12.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의12.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의12.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의12.Name = "lbl사용자정의12";
			this.lbl사용자정의12.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의12.TabIndex = 44;
			this.lbl사용자정의12.Tag = "DY_VALID";
			this.lbl사용자정의12.Text = "사용자정의12";
			this.lbl사용자정의12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의12.Visible = false;
			// 
			// cbo사용자정의12
			// 
			this.cbo사용자정의12.AutoDropDown = true;
			this.cbo사용자정의12.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의12.ItemHeight = 12;
			this.cbo사용자정의12.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의12.Name = "cbo사용자정의12";
			this.cbo사용자정의12.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의12.TabIndex = 11;
			this.cbo사용자정의12.Tag = "CD_USERDEF7";
			this.cbo사용자정의12.Visible = false;
			// 
			// oneGridItem70
			// 
			this.oneGridItem70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem70.Controls.Add(this.bpPanelControl122);
			this.oneGridItem70.Controls.Add(this.bpPanelControl123);
			this.oneGridItem70.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem70.Location = new System.Drawing.Point(0, 207);
			this.oneGridItem70.Name = "oneGridItem70";
			this.oneGridItem70.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem70.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem70.TabIndex = 9;
			// 
			// bpPanelControl122
			// 
			this.bpPanelControl122.Controls.Add(this.lbl사용자정의15);
			this.bpPanelControl122.Controls.Add(this.cbo사용자정의15);
			this.bpPanelControl122.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl122.Name = "bpPanelControl122";
			this.bpPanelControl122.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl122.TabIndex = 4;
			this.bpPanelControl122.Text = "bpPanelControl122";
			// 
			// lbl사용자정의15
			// 
			this.lbl사용자정의15.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의15.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의15.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의15.Name = "lbl사용자정의15";
			this.lbl사용자정의15.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의15.TabIndex = 199;
			this.lbl사용자정의15.Text = "사용자정의15";
			this.lbl사용자정의15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의15.Visible = false;
			// 
			// cbo사용자정의15
			// 
			this.cbo사용자정의15.AutoDropDown = true;
			this.cbo사용자정의15.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의15.ItemHeight = 12;
			this.cbo사용자정의15.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의15.Name = "cbo사용자정의15";
			this.cbo사용자정의15.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의15.TabIndex = 14;
			this.cbo사용자정의15.Tag = "CD_USERDEF10";
			this.cbo사용자정의15.Visible = false;
			// 
			// bpPanelControl123
			// 
			this.bpPanelControl123.Controls.Add(this.lbl사용자정의14);
			this.bpPanelControl123.Controls.Add(this.cbo사용자정의14);
			this.bpPanelControl123.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl123.Name = "bpPanelControl123";
			this.bpPanelControl123.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl123.TabIndex = 3;
			this.bpPanelControl123.Text = "bpPanelControl123";
			// 
			// lbl사용자정의14
			// 
			this.lbl사용자정의14.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의14.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의14.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의14.Name = "lbl사용자정의14";
			this.lbl사용자정의14.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의14.TabIndex = 45;
			this.lbl사용자정의14.Tag = "NO_MODEL";
			this.lbl사용자정의14.Text = "사용자정의14";
			this.lbl사용자정의14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의14.Visible = false;
			// 
			// cbo사용자정의14
			// 
			this.cbo사용자정의14.AutoDropDown = true;
			this.cbo사용자정의14.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의14.ItemHeight = 12;
			this.cbo사용자정의14.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의14.Name = "cbo사용자정의14";
			this.cbo사용자정의14.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의14.TabIndex = 13;
			this.cbo사용자정의14.Tag = "CD_USERDEF9";
			this.cbo사용자정의14.Visible = false;
			// 
			// oneGridItem71
			// 
			this.oneGridItem71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem71.Controls.Add(this.bpPanelControl124);
			this.oneGridItem71.Controls.Add(this.bpPanelControl125);
			this.oneGridItem71.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem71.Location = new System.Drawing.Point(0, 230);
			this.oneGridItem71.Name = "oneGridItem71";
			this.oneGridItem71.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem71.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem71.TabIndex = 10;
			// 
			// bpPanelControl124
			// 
			this.bpPanelControl124.Controls.Add(this.lbl사용자정의17);
			this.bpPanelControl124.Controls.Add(this.cur사용자정의17);
			this.bpPanelControl124.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl124.Name = "bpPanelControl124";
			this.bpPanelControl124.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl124.TabIndex = 4;
			this.bpPanelControl124.Text = "bpPanelControl124";
			// 
			// lbl사용자정의17
			// 
			this.lbl사용자정의17.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의17.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의17.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의17.Name = "lbl사용자정의17";
			this.lbl사용자정의17.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의17.TabIndex = 198;
			this.lbl사용자정의17.Text = "사용자정의17";
			this.lbl사용자정의17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의17.Visible = false;
			// 
			// cur사용자정의17
			// 
			this.cur사용자정의17.BackColor = System.Drawing.Color.White;
			this.cur사용자정의17.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의17.CurrencyDecimalDigits = 4;
			this.cur사용자정의17.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의17.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의17.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의17.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의17.MaxLength = 3;
			this.cur사용자정의17.Name = "cur사용자정의17";
			this.cur사용자정의17.NullString = "0";
			this.cur사용자정의17.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의17.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의17.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의17.TabIndex = 16;
			this.cur사용자정의17.Tag = "NUM_USERDEF2";
			this.cur사용자정의17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의17.Visible = false;
			// 
			// bpPanelControl125
			// 
			this.bpPanelControl125.Controls.Add(this.lbl사용자정의16);
			this.bpPanelControl125.Controls.Add(this.cur사용자정의16);
			this.bpPanelControl125.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl125.Name = "bpPanelControl125";
			this.bpPanelControl125.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl125.TabIndex = 3;
			this.bpPanelControl125.Text = "bpPanelControl125";
			// 
			// lbl사용자정의16
			// 
			this.lbl사용자정의16.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의16.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의16.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의16.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의16.Name = "lbl사용자정의16";
			this.lbl사용자정의16.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의16.TabIndex = 71;
			this.lbl사용자정의16.Tag = "CLS_L";
			this.lbl사용자정의16.Text = "사용자정의16";
			this.lbl사용자정의16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의16.Visible = false;
			// 
			// cur사용자정의16
			// 
			this.cur사용자정의16.BackColor = System.Drawing.Color.White;
			this.cur사용자정의16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의16.CurrencyDecimalDigits = 4;
			this.cur사용자정의16.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의16.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의16.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의16.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의16.MaxLength = 3;
			this.cur사용자정의16.Name = "cur사용자정의16";
			this.cur사용자정의16.NullString = "0";
			this.cur사용자정의16.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의16.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의16.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의16.TabIndex = 15;
			this.cur사용자정의16.Tag = "NUM_USERDEF1";
			this.cur사용자정의16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의16.Visible = false;
			// 
			// oneGridItem72
			// 
			this.oneGridItem72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem72.Controls.Add(this.bpPanelControl126);
			this.oneGridItem72.Controls.Add(this.bpPanelControl127);
			this.oneGridItem72.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem72.Location = new System.Drawing.Point(0, 253);
			this.oneGridItem72.Name = "oneGridItem72";
			this.oneGridItem72.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem72.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem72.TabIndex = 11;
			// 
			// bpPanelControl126
			// 
			this.bpPanelControl126.Controls.Add(this.lbl사용자정의19);
			this.bpPanelControl126.Controls.Add(this.cur사용자정의19);
			this.bpPanelControl126.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl126.Name = "bpPanelControl126";
			this.bpPanelControl126.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl126.TabIndex = 4;
			this.bpPanelControl126.Text = "bpPanelControl126";
			// 
			// lbl사용자정의19
			// 
			this.lbl사용자정의19.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의19.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의19.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의19.Name = "lbl사용자정의19";
			this.lbl사용자정의19.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의19.TabIndex = 200;
			this.lbl사용자정의19.Text = "사용자정의19";
			this.lbl사용자정의19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의19.Visible = false;
			// 
			// cur사용자정의19
			// 
			this.cur사용자정의19.BackColor = System.Drawing.Color.White;
			this.cur사용자정의19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의19.CurrencyDecimalDigits = 4;
			this.cur사용자정의19.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의19.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의19.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의19.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의19.MaxLength = 3;
			this.cur사용자정의19.Name = "cur사용자정의19";
			this.cur사용자정의19.NullString = "0";
			this.cur사용자정의19.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의19.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의19.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의19.TabIndex = 18;
			this.cur사용자정의19.Tag = "NUM_USERDEF4";
			this.cur사용자정의19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의19.Visible = false;
			// 
			// bpPanelControl127
			// 
			this.bpPanelControl127.Controls.Add(this.lbl사용자정의18);
			this.bpPanelControl127.Controls.Add(this.cur사용자정의18);
			this.bpPanelControl127.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl127.Name = "bpPanelControl127";
			this.bpPanelControl127.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl127.TabIndex = 3;
			this.bpPanelControl127.Text = "bpPanelControl127";
			// 
			// lbl사용자정의18
			// 
			this.lbl사용자정의18.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의18.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의18.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의18.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의18.Name = "lbl사용자정의18";
			this.lbl사용자정의18.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의18.TabIndex = 73;
			this.lbl사용자정의18.Tag = "CLS_M";
			this.lbl사용자정의18.Text = "사용자정의18";
			this.lbl사용자정의18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의18.Visible = false;
			// 
			// cur사용자정의18
			// 
			this.cur사용자정의18.BackColor = System.Drawing.Color.White;
			this.cur사용자정의18.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의18.CurrencyDecimalDigits = 4;
			this.cur사용자정의18.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의18.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의18.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의18.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의18.MaxLength = 3;
			this.cur사용자정의18.Name = "cur사용자정의18";
			this.cur사용자정의18.NullString = "0";
			this.cur사용자정의18.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의18.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의18.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의18.TabIndex = 17;
			this.cur사용자정의18.Tag = "NUM_USERDEF3";
			this.cur사용자정의18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의18.Visible = false;
			// 
			// oneGridItem73
			// 
			this.oneGridItem73.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem73.Controls.Add(this.bpPanelControl128);
			this.oneGridItem73.Controls.Add(this.bpPanelControl129);
			this.oneGridItem73.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem73.Location = new System.Drawing.Point(0, 276);
			this.oneGridItem73.Name = "oneGridItem73";
			this.oneGridItem73.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem73.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem73.TabIndex = 12;
			// 
			// bpPanelControl128
			// 
			this.bpPanelControl128.Controls.Add(this.lbl사용자정의21);
			this.bpPanelControl128.Controls.Add(this.cur사용자정의21);
			this.bpPanelControl128.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl128.Name = "bpPanelControl128";
			this.bpPanelControl128.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl128.TabIndex = 4;
			this.bpPanelControl128.Text = "bpPanelControl128";
			// 
			// lbl사용자정의21
			// 
			this.lbl사용자정의21.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의21.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의21.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의21.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의21.Name = "lbl사용자정의21";
			this.lbl사용자정의21.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의21.TabIndex = 201;
			this.lbl사용자정의21.Tag = "CLS_S";
			this.lbl사용자정의21.Text = "사용자정의21";
			this.lbl사용자정의21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의21.Visible = false;
			// 
			// cur사용자정의21
			// 
			this.cur사용자정의21.BackColor = System.Drawing.Color.White;
			this.cur사용자정의21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의21.CurrencyDecimalDigits = 4;
			this.cur사용자정의21.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의21.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의21.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의21.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의21.MaxLength = 3;
			this.cur사용자정의21.Name = "cur사용자정의21";
			this.cur사용자정의21.NullString = "0";
			this.cur사용자정의21.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의21.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의21.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의21.TabIndex = 202;
			this.cur사용자정의21.Tag = "NUM_USERDEF6";
			this.cur사용자정의21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의21.Visible = false;
			// 
			// bpPanelControl129
			// 
			this.bpPanelControl129.Controls.Add(this.lbl사용자정의20);
			this.bpPanelControl129.Controls.Add(this.cur사용자정의20);
			this.bpPanelControl129.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl129.Name = "bpPanelControl129";
			this.bpPanelControl129.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl129.TabIndex = 3;
			this.bpPanelControl129.Text = "bpPanelControl129";
			// 
			// lbl사용자정의20
			// 
			this.lbl사용자정의20.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의20.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의20.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의20.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의20.Name = "lbl사용자정의20";
			this.lbl사용자정의20.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의20.TabIndex = 75;
			this.lbl사용자정의20.Tag = "CLS_S";
			this.lbl사용자정의20.Text = "사용자정의20";
			this.lbl사용자정의20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의20.Visible = false;
			// 
			// cur사용자정의20
			// 
			this.cur사용자정의20.BackColor = System.Drawing.Color.White;
			this.cur사용자정의20.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의20.CurrencyDecimalDigits = 4;
			this.cur사용자정의20.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의20.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의20.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의20.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의20.MaxLength = 3;
			this.cur사용자정의20.Name = "cur사용자정의20";
			this.cur사용자정의20.NullString = "0";
			this.cur사용자정의20.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의20.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의20.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의20.TabIndex = 19;
			this.cur사용자정의20.Tag = "NUM_USERDEF5";
			this.cur사용자정의20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의20.Visible = false;
			// 
			// oneGridItem74
			// 
			this.oneGridItem74.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem74.Controls.Add(this.bpPanelControl130);
			this.oneGridItem74.Controls.Add(this.bpPanelControl131);
			this.oneGridItem74.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem74.Location = new System.Drawing.Point(0, 299);
			this.oneGridItem74.Name = "oneGridItem74";
			this.oneGridItem74.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem74.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem74.TabIndex = 13;
			// 
			// bpPanelControl130
			// 
			this.bpPanelControl130.Controls.Add(this.lbl사용자정의23);
			this.bpPanelControl130.Controls.Add(this.cur사용자정의23);
			this.bpPanelControl130.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl130.Name = "bpPanelControl130";
			this.bpPanelControl130.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl130.TabIndex = 4;
			this.bpPanelControl130.Text = "bpPanelControl130";
			// 
			// lbl사용자정의23
			// 
			this.lbl사용자정의23.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의23.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의23.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의23.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의23.Name = "lbl사용자정의23";
			this.lbl사용자정의23.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의23.TabIndex = 200;
			this.lbl사용자정의23.Tag = "CLS_S";
			this.lbl사용자정의23.Text = "사용자정의23";
			this.lbl사용자정의23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의23.Visible = false;
			// 
			// cur사용자정의23
			// 
			this.cur사용자정의23.BackColor = System.Drawing.Color.White;
			this.cur사용자정의23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의23.CurrencyDecimalDigits = 4;
			this.cur사용자정의23.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의23.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의23.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의23.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의23.MaxLength = 3;
			this.cur사용자정의23.Name = "cur사용자정의23";
			this.cur사용자정의23.NullString = "0";
			this.cur사용자정의23.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의23.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의23.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의23.TabIndex = 203;
			this.cur사용자정의23.Tag = "NUM_USERDEF8";
			this.cur사용자정의23.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의23.Visible = false;
			// 
			// bpPanelControl131
			// 
			this.bpPanelControl131.Controls.Add(this.lbl사용자정의22);
			this.bpPanelControl131.Controls.Add(this.cur사용자정의22);
			this.bpPanelControl131.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl131.Name = "bpPanelControl131";
			this.bpPanelControl131.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl131.TabIndex = 3;
			this.bpPanelControl131.Text = "bpPanelControl131";
			// 
			// lbl사용자정의22
			// 
			this.lbl사용자정의22.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의22.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의22.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의22.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의22.Name = "lbl사용자정의22";
			this.lbl사용자정의22.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의22.TabIndex = 76;
			this.lbl사용자정의22.Tag = "CLS_S";
			this.lbl사용자정의22.Text = "사용자정의22";
			this.lbl사용자정의22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의22.Visible = false;
			// 
			// cur사용자정의22
			// 
			this.cur사용자정의22.BackColor = System.Drawing.Color.White;
			this.cur사용자정의22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의22.CurrencyDecimalDigits = 4;
			this.cur사용자정의22.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의22.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의22.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의22.MaxLength = 3;
			this.cur사용자정의22.Name = "cur사용자정의22";
			this.cur사용자정의22.NullString = "0";
			this.cur사용자정의22.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의22.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의22.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의22.TabIndex = 200;
			this.cur사용자정의22.Tag = "NUM_USERDEF7";
			this.cur사용자정의22.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의22.Visible = false;
			// 
			// oneGridItem75
			// 
			this.oneGridItem75.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem75.Controls.Add(this.bpPanelControl132);
			this.oneGridItem75.Controls.Add(this.bpPanelControl133);
			this.oneGridItem75.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem75.Location = new System.Drawing.Point(0, 322);
			this.oneGridItem75.Name = "oneGridItem75";
			this.oneGridItem75.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem75.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem75.TabIndex = 14;
			// 
			// bpPanelControl132
			// 
			this.bpPanelControl132.Controls.Add(this.lbl사용자정의25);
			this.bpPanelControl132.Controls.Add(this.cur사용자정의25);
			this.bpPanelControl132.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl132.Name = "bpPanelControl132";
			this.bpPanelControl132.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl132.TabIndex = 4;
			this.bpPanelControl132.Text = "bpPanelControl132";
			// 
			// lbl사용자정의25
			// 
			this.lbl사용자정의25.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의25.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의25.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의25.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의25.Name = "lbl사용자정의25";
			this.lbl사용자정의25.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의25.TabIndex = 200;
			this.lbl사용자정의25.Tag = "CLS_S";
			this.lbl사용자정의25.Text = "사용자정의25";
			this.lbl사용자정의25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의25.Visible = false;
			// 
			// cur사용자정의25
			// 
			this.cur사용자정의25.BackColor = System.Drawing.Color.White;
			this.cur사용자정의25.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의25.CurrencyDecimalDigits = 4;
			this.cur사용자정의25.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의25.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의25.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의25.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의25.MaxLength = 3;
			this.cur사용자정의25.Name = "cur사용자정의25";
			this.cur사용자정의25.NullString = "0";
			this.cur사용자정의25.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의25.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의25.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의25.TabIndex = 204;
			this.cur사용자정의25.Tag = "NUM_USERDEF10";
			this.cur사용자정의25.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의25.Visible = false;
			// 
			// bpPanelControl133
			// 
			this.bpPanelControl133.Controls.Add(this.lbl사용자정의24);
			this.bpPanelControl133.Controls.Add(this.cur사용자정의24);
			this.bpPanelControl133.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl133.Name = "bpPanelControl133";
			this.bpPanelControl133.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl133.TabIndex = 3;
			this.bpPanelControl133.Text = "bpPanelControl133";
			// 
			// lbl사용자정의24
			// 
			this.lbl사용자정의24.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의24.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의24.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의24.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의24.Name = "lbl사용자정의24";
			this.lbl사용자정의24.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의24.TabIndex = 77;
			this.lbl사용자정의24.Tag = "CLS_S";
			this.lbl사용자정의24.Text = "사용자정의24";
			this.lbl사용자정의24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의24.Visible = false;
			// 
			// cur사용자정의24
			// 
			this.cur사용자정의24.BackColor = System.Drawing.Color.White;
			this.cur사용자정의24.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur사용자정의24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur사용자정의24.CurrencyDecimalDigits = 4;
			this.cur사용자정의24.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur사용자정의24.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur사용자정의24.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur사용자정의24.Location = new System.Drawing.Point(157, 1);
			this.cur사용자정의24.MaxLength = 3;
			this.cur사용자정의24.Name = "cur사용자정의24";
			this.cur사용자정의24.NullString = "0";
			this.cur사용자정의24.PositiveColor = System.Drawing.Color.Black;
			this.cur사용자정의24.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur사용자정의24.Size = new System.Drawing.Size(185, 21);
			this.cur사용자정의24.TabIndex = 201;
			this.cur사용자정의24.Tag = "NUM_USERDEF9";
			this.cur사용자정의24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur사용자정의24.Visible = false;
			// 
			// oneGridItem76
			// 
			this.oneGridItem76.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem76.Controls.Add(this.bpPanelControl134);
			this.oneGridItem76.Controls.Add(this.bpPanelControl135);
			this.oneGridItem76.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem76.Location = new System.Drawing.Point(0, 345);
			this.oneGridItem76.Name = "oneGridItem76";
			this.oneGridItem76.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem76.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem76.TabIndex = 15;
			// 
			// bpPanelControl134
			// 
			this.bpPanelControl134.Controls.Add(this.lbl사용자정의27);
			this.bpPanelControl134.Controls.Add(this.cbo사용자정의27);
			this.bpPanelControl134.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl134.Name = "bpPanelControl134";
			this.bpPanelControl134.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl134.TabIndex = 4;
			this.bpPanelControl134.Text = "bpPanelControl134";
			// 
			// lbl사용자정의27
			// 
			this.lbl사용자정의27.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의27.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의27.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의27.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의27.Name = "lbl사용자정의27";
			this.lbl사용자정의27.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의27.TabIndex = 202;
			this.lbl사용자정의27.Tag = "CLS_S";
			this.lbl사용자정의27.Text = "사용자정의27";
			this.lbl사용자정의27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의27.Visible = false;
			// 
			// cbo사용자정의27
			// 
			this.cbo사용자정의27.AutoDropDown = true;
			this.cbo사용자정의27.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의27.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의27.ItemHeight = 12;
			this.cbo사용자정의27.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의27.Name = "cbo사용자정의27";
			this.cbo사용자정의27.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의27.TabIndex = 208;
			this.cbo사용자정의27.Tag = "CD_USERDEF12";
			this.cbo사용자정의27.Visible = false;
			// 
			// bpPanelControl135
			// 
			this.bpPanelControl135.Controls.Add(this.lbl사용자정의26);
			this.bpPanelControl135.Controls.Add(this.cbo사용자정의26);
			this.bpPanelControl135.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl135.Name = "bpPanelControl135";
			this.bpPanelControl135.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl135.TabIndex = 3;
			this.bpPanelControl135.Text = "bpPanelControl135";
			// 
			// lbl사용자정의26
			// 
			this.lbl사용자정의26.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의26.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의26.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의26.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의26.Name = "lbl사용자정의26";
			this.lbl사용자정의26.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의26.TabIndex = 78;
			this.lbl사용자정의26.Tag = "CLS_S";
			this.lbl사용자정의26.Text = "사용자정의26";
			this.lbl사용자정의26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의26.Visible = false;
			// 
			// cbo사용자정의26
			// 
			this.cbo사용자정의26.AutoDropDown = true;
			this.cbo사용자정의26.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의26.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의26.ItemHeight = 12;
			this.cbo사용자정의26.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의26.Name = "cbo사용자정의26";
			this.cbo사용자정의26.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의26.TabIndex = 207;
			this.cbo사용자정의26.Tag = "CD_USERDEF11";
			this.cbo사용자정의26.Visible = false;
			// 
			// oneGridItem77
			// 
			this.oneGridItem77.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem77.Controls.Add(this.bpPanelControl136);
			this.oneGridItem77.Controls.Add(this.bpPanelControl137);
			this.oneGridItem77.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem77.Location = new System.Drawing.Point(0, 368);
			this.oneGridItem77.Name = "oneGridItem77";
			this.oneGridItem77.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem77.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem77.TabIndex = 16;
			// 
			// bpPanelControl136
			// 
			this.bpPanelControl136.Controls.Add(this.lbl사용자정의29);
			this.bpPanelControl136.Controls.Add(this.cbo사용자정의29);
			this.bpPanelControl136.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl136.Name = "bpPanelControl136";
			this.bpPanelControl136.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl136.TabIndex = 4;
			this.bpPanelControl136.Text = "bpPanelControl136";
			// 
			// lbl사용자정의29
			// 
			this.lbl사용자정의29.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의29.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의29.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의29.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의29.Name = "lbl사용자정의29";
			this.lbl사용자정의29.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의29.TabIndex = 210;
			this.lbl사용자정의29.Tag = "CLS_S";
			this.lbl사용자정의29.Text = "사용자정의29";
			this.lbl사용자정의29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의29.Visible = false;
			// 
			// cbo사용자정의29
			// 
			this.cbo사용자정의29.AutoDropDown = true;
			this.cbo사용자정의29.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의29.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의29.ItemHeight = 12;
			this.cbo사용자정의29.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의29.Name = "cbo사용자정의29";
			this.cbo사용자정의29.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의29.TabIndex = 211;
			this.cbo사용자정의29.Tag = "CD_USERDEF14";
			this.cbo사용자정의29.Visible = false;
			// 
			// bpPanelControl137
			// 
			this.bpPanelControl137.Controls.Add(this.lbl사용자정의28);
			this.bpPanelControl137.Controls.Add(this.cbo사용자정의28);
			this.bpPanelControl137.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl137.Name = "bpPanelControl137";
			this.bpPanelControl137.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl137.TabIndex = 3;
			this.bpPanelControl137.Text = "bpPanelControl137";
			// 
			// lbl사용자정의28
			// 
			this.lbl사용자정의28.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의28.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의28.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의28.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의28.Name = "lbl사용자정의28";
			this.lbl사용자정의28.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의28.TabIndex = 79;
			this.lbl사용자정의28.Tag = "CLS_S";
			this.lbl사용자정의28.Text = "사용자정의28";
			this.lbl사용자정의28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의28.Visible = false;
			// 
			// cbo사용자정의28
			// 
			this.cbo사용자정의28.AutoDropDown = true;
			this.cbo사용자정의28.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의28.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의28.ItemHeight = 12;
			this.cbo사용자정의28.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의28.Name = "cbo사용자정의28";
			this.cbo사용자정의28.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의28.TabIndex = 210;
			this.cbo사용자정의28.Tag = "CD_USERDEF13";
			this.cbo사용자정의28.Visible = false;
			// 
			// oneGridItem78
			// 
			this.oneGridItem78.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem78.Controls.Add(this.bpPanelControl138);
			this.oneGridItem78.Controls.Add(this.bpPanelControl139);
			this.oneGridItem78.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem78.Location = new System.Drawing.Point(0, 391);
			this.oneGridItem78.Name = "oneGridItem78";
			this.oneGridItem78.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem78.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem78.TabIndex = 17;
			// 
			// bpPanelControl138
			// 
			this.bpPanelControl138.Controls.Add(this.lbl사용자정의31);
			this.bpPanelControl138.Controls.Add(this.bp_사용자정의31);
			this.bpPanelControl138.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl138.Name = "bpPanelControl138";
			this.bpPanelControl138.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl138.TabIndex = 4;
			this.bpPanelControl138.Text = "bpPanelControl138";
			// 
			// lbl사용자정의31
			// 
			this.lbl사용자정의31.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의31.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의31.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의31.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의31.Name = "lbl사용자정의31";
			this.lbl사용자정의31.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의31.TabIndex = 211;
			this.lbl사용자정의31.Tag = "CLS_S";
			this.lbl사용자정의31.Text = "사용자정의31";
			this.lbl사용자정의31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의31.Visible = false;
			// 
			// bp_사용자정의31
			// 
			this.bp_사용자정의31.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.bp_사용자정의31.Location = new System.Drawing.Point(157, 1);
			this.bp_사용자정의31.Name = "bp_사용자정의31";
			this.bp_사용자정의31.Size = new System.Drawing.Size(185, 21);
			this.bp_사용자정의31.TabIndex = 226;
			this.bp_사용자정의31.TabStop = false;
			this.bp_사용자정의31.Tag = "CD_USERDEF16,NM_USERDEF16";
			this.bp_사용자정의31.Visible = false;
			// 
			// bpPanelControl139
			// 
			this.bpPanelControl139.Controls.Add(this.lbl사용자정의30);
			this.bpPanelControl139.Controls.Add(this.bp_사용자정의30);
			this.bpPanelControl139.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl139.Name = "bpPanelControl139";
			this.bpPanelControl139.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl139.TabIndex = 3;
			this.bpPanelControl139.Text = "bpPanelControl139";
			// 
			// lbl사용자정의30
			// 
			this.lbl사용자정의30.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의30.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의30.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의30.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의30.Name = "lbl사용자정의30";
			this.lbl사용자정의30.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의30.TabIndex = 80;
			this.lbl사용자정의30.Tag = "CLS_S";
			this.lbl사용자정의30.Text = "사용자정의30";
			this.lbl사용자정의30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의30.Visible = false;
			// 
			// bp_사용자정의30
			// 
			this.bp_사용자정의30.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.bp_사용자정의30.Location = new System.Drawing.Point(157, 1);
			this.bp_사용자정의30.Name = "bp_사용자정의30";
			this.bp_사용자정의30.Size = new System.Drawing.Size(185, 21);
			this.bp_사용자정의30.TabIndex = 227;
			this.bp_사용자정의30.TabStop = false;
			this.bp_사용자정의30.Tag = "CD_USERDEF15,NM_USERDEF15";
			this.bp_사용자정의30.Visible = false;
			// 
			// oneGridItem79
			// 
			this.oneGridItem79.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem79.Controls.Add(this.bpPanelControl140);
			this.oneGridItem79.Controls.Add(this.bpPanelControl141);
			this.oneGridItem79.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem79.Location = new System.Drawing.Point(0, 414);
			this.oneGridItem79.Name = "oneGridItem79";
			this.oneGridItem79.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem79.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem79.TabIndex = 18;
			// 
			// bpPanelControl140
			// 
			this.bpPanelControl140.Controls.Add(this.lbl사용자정의33);
			this.bpPanelControl140.Controls.Add(this.cbo사용자정의33);
			this.bpPanelControl140.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl140.Name = "bpPanelControl140";
			this.bpPanelControl140.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl140.TabIndex = 4;
			this.bpPanelControl140.Text = "bpPanelControl140";
			// 
			// lbl사용자정의33
			// 
			this.lbl사용자정의33.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의33.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의33.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의33.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의33.Name = "lbl사용자정의33";
			this.lbl사용자정의33.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의33.TabIndex = 212;
			this.lbl사용자정의33.Tag = "CLS_S";
			this.lbl사용자정의33.Text = "사용자정의33";
			this.lbl사용자정의33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의33.Visible = false;
			// 
			// cbo사용자정의33
			// 
			this.cbo사용자정의33.AutoDropDown = true;
			this.cbo사용자정의33.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의33.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의33.ItemHeight = 12;
			this.cbo사용자정의33.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의33.Name = "cbo사용자정의33";
			this.cbo사용자정의33.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의33.TabIndex = 231;
			this.cbo사용자정의33.Tag = "CD_USERDEF18";
			this.cbo사용자정의33.Visible = false;
			// 
			// bpPanelControl141
			// 
			this.bpPanelControl141.Controls.Add(this.lbl사용자정의32);
			this.bpPanelControl141.Controls.Add(this.bp_사용자정의32);
			this.bpPanelControl141.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl141.Name = "bpPanelControl141";
			this.bpPanelControl141.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl141.TabIndex = 3;
			this.bpPanelControl141.Text = "bpPanelControl141";
			// 
			// lbl사용자정의32
			// 
			this.lbl사용자정의32.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의32.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의32.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의32.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의32.Name = "lbl사용자정의32";
			this.lbl사용자정의32.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의32.TabIndex = 81;
			this.lbl사용자정의32.Tag = "CLS_S";
			this.lbl사용자정의32.Text = "사용자정의32";
			this.lbl사용자정의32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의32.Visible = false;
			// 
			// bp_사용자정의32
			// 
			this.bp_사용자정의32.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.bp_사용자정의32.Location = new System.Drawing.Point(157, 1);
			this.bp_사용자정의32.Name = "bp_사용자정의32";
			this.bp_사용자정의32.Size = new System.Drawing.Size(185, 21);
			this.bp_사용자정의32.TabIndex = 228;
			this.bp_사용자정의32.TabStop = false;
			this.bp_사용자정의32.Tag = "CD_USERDEF17,NM_USERDEF17";
			this.bp_사용자정의32.Visible = false;
			// 
			// oneGridItem80
			// 
			this.oneGridItem80.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem80.Controls.Add(this.bpPanelControl142);
			this.oneGridItem80.Controls.Add(this.bpPanelControl143);
			this.oneGridItem80.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem80.Location = new System.Drawing.Point(0, 437);
			this.oneGridItem80.Name = "oneGridItem80";
			this.oneGridItem80.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem80.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem80.TabIndex = 19;
			// 
			// bpPanelControl142
			// 
			this.bpPanelControl142.Controls.Add(this.cbo사용자정의35);
			this.bpPanelControl142.Controls.Add(this.lbl사용자정의35);
			this.bpPanelControl142.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl142.Name = "bpPanelControl142";
			this.bpPanelControl142.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl142.TabIndex = 4;
			this.bpPanelControl142.Text = "bpPanelControl142";
			// 
			// cbo사용자정의35
			// 
			this.cbo사용자정의35.AutoDropDown = true;
			this.cbo사용자정의35.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의35.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의35.ItemHeight = 12;
			this.cbo사용자정의35.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의35.Name = "cbo사용자정의35";
			this.cbo사용자정의35.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의35.TabIndex = 233;
			this.cbo사용자정의35.Tag = "CD_USERDEF20";
			this.cbo사용자정의35.Visible = false;
			// 
			// lbl사용자정의35
			// 
			this.lbl사용자정의35.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의35.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의35.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의35.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의35.Name = "lbl사용자정의35";
			this.lbl사용자정의35.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의35.TabIndex = 214;
			this.lbl사용자정의35.Tag = "CLS_S";
			this.lbl사용자정의35.Text = "사용자정의35";
			this.lbl사용자정의35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의35.Visible = false;
			// 
			// bpPanelControl143
			// 
			this.bpPanelControl143.Controls.Add(this.lbl사용자정의34);
			this.bpPanelControl143.Controls.Add(this.cbo사용자정의34);
			this.bpPanelControl143.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl143.Name = "bpPanelControl143";
			this.bpPanelControl143.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl143.TabIndex = 3;
			this.bpPanelControl143.Text = "bpPanelControl143";
			// 
			// lbl사용자정의34
			// 
			this.lbl사용자정의34.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의34.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의34.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의34.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의34.Name = "lbl사용자정의34";
			this.lbl사용자정의34.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의34.TabIndex = 213;
			this.lbl사용자정의34.Tag = "CLS_S";
			this.lbl사용자정의34.Text = "사용자정의34";
			this.lbl사용자정의34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의34.Visible = false;
			// 
			// cbo사용자정의34
			// 
			this.cbo사용자정의34.AutoDropDown = true;
			this.cbo사용자정의34.BackColor = System.Drawing.Color.White;
			this.cbo사용자정의34.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용자정의34.ItemHeight = 12;
			this.cbo사용자정의34.Location = new System.Drawing.Point(157, 1);
			this.cbo사용자정의34.Name = "cbo사용자정의34";
			this.cbo사용자정의34.Size = new System.Drawing.Size(185, 20);
			this.cbo사용자정의34.TabIndex = 232;
			this.cbo사용자정의34.Tag = "CD_USERDEF19";
			this.cbo사용자정의34.Visible = false;
			// 
			// oneGridItem98
			// 
			this.oneGridItem98.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem98.Controls.Add(this.bpPanelControl172);
			this.oneGridItem98.Controls.Add(this.bpPanelControl171);
			this.oneGridItem98.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem98.Location = new System.Drawing.Point(0, 460);
			this.oneGridItem98.Name = "oneGridItem98";
			this.oneGridItem98.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem98.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem98.TabIndex = 20;
			// 
			// bpPanelControl172
			// 
			this.bpPanelControl172.Controls.Add(this._datePicker37);
			this.bpPanelControl172.Controls.Add(this.lbl사용자정의37);
			this.bpPanelControl172.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl172.Name = "bpPanelControl172";
			this.bpPanelControl172.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl172.TabIndex = 5;
			this.bpPanelControl172.Text = "bpPanelControl172";
			// 
			// _datePicker37
			// 
			this._datePicker37.Cursor = System.Windows.Forms.Cursors.Hand;
			this._datePicker37.Location = new System.Drawing.Point(157, 1);
			this._datePicker37.Mask = "####/##/##";
			this._datePicker37.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this._datePicker37.MaximumSize = new System.Drawing.Size(0, 21);
			this._datePicker37.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this._datePicker37.Name = "_datePicker37";
			this._datePicker37.Size = new System.Drawing.Size(91, 21);
			this._datePicker37.TabIndex = 214;
			this._datePicker37.Tag = "CD_USERDEF22";
			this._datePicker37.Value = new System.DateTime(((long)(0)));
			this._datePicker37.Visible = false;
			// 
			// lbl사용자정의37
			// 
			this.lbl사용자정의37.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의37.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의37.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의37.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의37.Name = "lbl사용자정의37";
			this.lbl사용자정의37.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의37.TabIndex = 213;
			this.lbl사용자정의37.Tag = "CLS_S";
			this.lbl사용자정의37.Text = "사용자정의37";
			this.lbl사용자정의37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의37.Visible = false;
			// 
			// bpPanelControl171
			// 
			this.bpPanelControl171.Controls.Add(this._datePicker36);
			this.bpPanelControl171.Controls.Add(this.lbl사용자정의36);
			this.bpPanelControl171.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl171.Name = "bpPanelControl171";
			this.bpPanelControl171.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl171.TabIndex = 4;
			this.bpPanelControl171.Text = "bpPanelControl171";
			// 
			// _datePicker36
			// 
			this._datePicker36.Cursor = System.Windows.Forms.Cursors.Hand;
			this._datePicker36.Location = new System.Drawing.Point(157, 1);
			this._datePicker36.Mask = "####/##/##";
			this._datePicker36.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this._datePicker36.MaximumSize = new System.Drawing.Size(0, 21);
			this._datePicker36.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this._datePicker36.Name = "_datePicker36";
			this._datePicker36.Size = new System.Drawing.Size(91, 21);
			this._datePicker36.TabIndex = 214;
			this._datePicker36.Tag = "CD_USERDEF21";
			this._datePicker36.Value = new System.DateTime(((long)(0)));
			this._datePicker36.Visible = false;
			// 
			// lbl사용자정의36
			// 
			this.lbl사용자정의36.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의36.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의36.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의36.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의36.Name = "lbl사용자정의36";
			this.lbl사용자정의36.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의36.TabIndex = 213;
			this.lbl사용자정의36.Tag = "CLS_S";
			this.lbl사용자정의36.Text = "사용자정의36";
			this.lbl사용자정의36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의36.Visible = false;
			// 
			// oneGridItem102
			// 
			this.oneGridItem102.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem102.Controls.Add(this.bpPanelControl177);
			this.oneGridItem102.Controls.Add(this.bpPanelControl176);
			this.oneGridItem102.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem102.Location = new System.Drawing.Point(0, 483);
			this.oneGridItem102.Name = "oneGridItem102";
			this.oneGridItem102.Size = new System.Drawing.Size(750, 23);
			this.oneGridItem102.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem102.TabIndex = 21;
			// 
			// bpPanelControl177
			// 
			this.bpPanelControl177.Controls.Add(this._datePicker39);
			this.bpPanelControl177.Controls.Add(this.lbl사용자정의39);
			this.bpPanelControl177.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl177.Name = "bpPanelControl177";
			this.bpPanelControl177.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl177.TabIndex = 6;
			this.bpPanelControl177.Text = "bpPanelControl177";
			// 
			// _datePicker39
			// 
			this._datePicker39.Cursor = System.Windows.Forms.Cursors.Hand;
			this._datePicker39.Location = new System.Drawing.Point(157, 1);
			this._datePicker39.Mask = "####/##/##";
			this._datePicker39.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this._datePicker39.MaximumSize = new System.Drawing.Size(0, 21);
			this._datePicker39.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this._datePicker39.Name = "_datePicker39";
			this._datePicker39.Size = new System.Drawing.Size(91, 21);
			this._datePicker39.TabIndex = 214;
			this._datePicker39.Tag = "CD_USERDEF24";
			this._datePicker39.Value = new System.DateTime(((long)(0)));
			this._datePicker39.Visible = false;
			// 
			// lbl사용자정의39
			// 
			this.lbl사용자정의39.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의39.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의39.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의39.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의39.Name = "lbl사용자정의39";
			this.lbl사용자정의39.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의39.TabIndex = 213;
			this.lbl사용자정의39.Tag = "CLS_S";
			this.lbl사용자정의39.Text = "사용자정의39";
			this.lbl사용자정의39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의39.Visible = false;
			// 
			// bpPanelControl176
			// 
			this.bpPanelControl176.Controls.Add(this._datePicker38);
			this.bpPanelControl176.Controls.Add(this.lbl사용자정의38);
			this.bpPanelControl176.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl176.Name = "bpPanelControl176";
			this.bpPanelControl176.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl176.TabIndex = 5;
			this.bpPanelControl176.Text = "bpPanelControl176";
			// 
			// _datePicker38
			// 
			this._datePicker38.Cursor = System.Windows.Forms.Cursors.Hand;
			this._datePicker38.Location = new System.Drawing.Point(157, 1);
			this._datePicker38.Mask = "####/##/##";
			this._datePicker38.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this._datePicker38.MaximumSize = new System.Drawing.Size(0, 21);
			this._datePicker38.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this._datePicker38.Name = "_datePicker38";
			this._datePicker38.Size = new System.Drawing.Size(91, 21);
			this._datePicker38.TabIndex = 214;
			this._datePicker38.Tag = "CD_USERDEF23";
			this._datePicker38.Value = new System.DateTime(((long)(0)));
			this._datePicker38.Visible = false;
			// 
			// lbl사용자정의38
			// 
			this.lbl사용자정의38.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용자정의38.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용자정의38.ForeColor = System.Drawing.Color.Black;
			this.lbl사용자정의38.Location = new System.Drawing.Point(0, 3);
			this.lbl사용자정의38.Name = "lbl사용자정의38";
			this.lbl사용자정의38.Size = new System.Drawing.Size(156, 16);
			this.lbl사용자정의38.TabIndex = 213;
			this.lbl사용자정의38.Tag = "CLS_S";
			this.lbl사용자정의38.Text = "사용자정의38";
			this.lbl사용자정의38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lbl사용자정의38.Visible = false;
			// 
			// tpgIMAGE
			// 
			this.tpgIMAGE.Controls.Add(this.panelExt43);
			this.tpgIMAGE.Location = new System.Drawing.Point(4, 24);
			this.tpgIMAGE.Name = "tpgIMAGE";
			this.tpgIMAGE.Size = new System.Drawing.Size(772, 626);
			this.tpgIMAGE.TabIndex = 5;
			this.tpgIMAGE.Text = "IMAGE관리";
			this.tpgIMAGE.UseVisualStyleBackColor = true;
			// 
			// panelExt43
			// 
			this.panelExt43.AutoScroll = true;
			this.panelExt43.BackColor = System.Drawing.Color.White;
			this.panelExt43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelExt43.Controls.Add(this.panelExt44);
			this.panelExt43.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt43.Location = new System.Drawing.Point(0, 0);
			this.panelExt43.Name = "panelExt43";
			this.panelExt43.Size = new System.Drawing.Size(772, 626);
			this.panelExt43.TabIndex = 1;
			// 
			// panelExt44
			// 
			this.panelExt44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelExt44.Controls.Add(this.tblImage);
			this.panelExt44.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt44.Location = new System.Drawing.Point(0, 0);
			this.panelExt44.Name = "panelExt44";
			this.panelExt44.Size = new System.Drawing.Size(770, 624);
			this.panelExt44.TabIndex = 21;
			// 
			// tblImage
			// 
			this.tblImage.ColumnCount = 1;
			this.tblImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblImage.Controls.Add(this.imagePanel1, 0, 1);
			this.tblImage.Controls.Add(this.pnlFileList, 0, 0);
			this.tblImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblImage.Location = new System.Drawing.Point(0, 0);
			this.tblImage.Name = "tblImage";
			this.tblImage.RowCount = 2;
			this.tblImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblImage.Size = new System.Drawing.Size(768, 622);
			this.tblImage.TabIndex = 0;
			// 
			// imagePanel1
			// 
			this.imagePanel1.AutoScroll = true;
			this.imagePanel1.BackColor = System.Drawing.Color.White;
			this.imagePanel1.Controls.Add(this.pixFile);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 159);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(762, 460);
			this.imagePanel1.TabIndex = 10;
			this.imagePanel1.TitleText = "";
			// 
			// pixFile
			// 
			this.pixFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pixFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pixFile.Location = new System.Drawing.Point(0, 0);
			this.pixFile.Name = "pixFile";
			this.pixFile.Size = new System.Drawing.Size(762, 460);
			this.pixFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pixFile.TabIndex = 9;
			this.pixFile.TabStop = false;
			// 
			// pnlFileList
			// 
			this.pnlFileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFileList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.pnlFileList.Controls.Add(this.textBoxExt4);
			this.pnlFileList.Controls.Add(this.textBoxExt5);
			this.pnlFileList.Controls.Add(this.textBoxExt6);
			this.pnlFileList.Controls.Add(this.textBoxExt3);
			this.pnlFileList.Controls.Add(this.textBoxExt2);
			this.pnlFileList.Controls.Add(this.textBoxExt1);
			this.pnlFileList.Controls.Add(this.rduFile6View);
			this.pnlFileList.Controls.Add(this.rduFile5View);
			this.pnlFileList.Controls.Add(this.rduFile4View);
			this.pnlFileList.Controls.Add(this.rduFileIns6);
			this.pnlFileList.Controls.Add(this.rduFileIns4);
			this.pnlFileList.Controls.Add(this.rduFileIns5);
			this.pnlFileList.Controls.Add(this.rduFileDel6);
			this.pnlFileList.Controls.Add(this.rduFileDel4);
			this.pnlFileList.Controls.Add(this.rduFileDel5);
			this.pnlFileList.Controls.Add(this.txtFile6);
			this.pnlFileList.Controls.Add(this.txtFile5);
			this.pnlFileList.Controls.Add(this.txtFile4);
			this.pnlFileList.Controls.Add(this.rduFile3View);
			this.pnlFileList.Controls.Add(this.rduFile2View);
			this.pnlFileList.Controls.Add(this.rduFile1View);
			this.pnlFileList.Controls.Add(this.rduFileIns3);
			this.pnlFileList.Controls.Add(this.rduFileIns1);
			this.pnlFileList.Controls.Add(this.rduFileIns2);
			this.pnlFileList.Controls.Add(this.rduFileDel3);
			this.pnlFileList.Controls.Add(this.rduFileDel1);
			this.pnlFileList.Controls.Add(this.rduFileDel2);
			this.pnlFileList.Controls.Add(this.txtFile3);
			this.pnlFileList.Controls.Add(this.txtFile2);
			this.pnlFileList.Controls.Add(this.txtFile1);
			this.pnlFileList.Location = new System.Drawing.Point(3, 3);
			this.pnlFileList.Name = "pnlFileList";
			this.pnlFileList.Size = new System.Drawing.Size(762, 150);
			this.pnlFileList.TabIndex = 9;
			// 
			// textBoxExt4
			// 
			this.textBoxExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExt4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt4.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt4.Location = new System.Drawing.Point(409, 124);
			this.textBoxExt4.MaxLength = 100;
			this.textBoxExt4.Name = "textBoxExt4";
			this.textBoxExt4.Size = new System.Drawing.Size(343, 21);
			this.textBoxExt4.TabIndex = 255;
			this.textBoxExt4.Tag = "DC_IMAGE6";
			// 
			// textBoxExt5
			// 
			this.textBoxExt5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExt5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt5.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt5.Location = new System.Drawing.Point(409, 100);
			this.textBoxExt5.MaxLength = 100;
			this.textBoxExt5.Name = "textBoxExt5";
			this.textBoxExt5.Size = new System.Drawing.Size(343, 21);
			this.textBoxExt5.TabIndex = 254;
			this.textBoxExt5.Tag = "DC_IMAGE5";
			// 
			// textBoxExt6
			// 
			this.textBoxExt6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExt6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt6.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt6.Location = new System.Drawing.Point(409, 76);
			this.textBoxExt6.MaxLength = 100;
			this.textBoxExt6.Name = "textBoxExt6";
			this.textBoxExt6.Size = new System.Drawing.Size(343, 21);
			this.textBoxExt6.TabIndex = 253;
			this.textBoxExt6.Tag = "DC_IMAGE4";
			// 
			// textBoxExt3
			// 
			this.textBoxExt3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExt3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt3.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt3.Location = new System.Drawing.Point(409, 51);
			this.textBoxExt3.MaxLength = 100;
			this.textBoxExt3.Name = "textBoxExt3";
			this.textBoxExt3.Size = new System.Drawing.Size(343, 21);
			this.textBoxExt3.TabIndex = 252;
			this.textBoxExt3.Tag = "DC_IMAGE3";
			// 
			// textBoxExt2
			// 
			this.textBoxExt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExt2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt2.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt2.Location = new System.Drawing.Point(409, 27);
			this.textBoxExt2.MaxLength = 100;
			this.textBoxExt2.Name = "textBoxExt2";
			this.textBoxExt2.Size = new System.Drawing.Size(343, 21);
			this.textBoxExt2.TabIndex = 251;
			this.textBoxExt2.Tag = "DC_IMAGE2";
			// 
			// textBoxExt1
			// 
			this.textBoxExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt1.Font = new System.Drawing.Font("굴림체", 9F);
			this.textBoxExt1.Location = new System.Drawing.Point(409, 3);
			this.textBoxExt1.MaxLength = 100;
			this.textBoxExt1.Name = "textBoxExt1";
			this.textBoxExt1.Size = new System.Drawing.Size(343, 21);
			this.textBoxExt1.TabIndex = 250;
			this.textBoxExt1.Tag = "DC_IMAGE1";
			// 
			// rduFile6View
			// 
			this.rduFile6View.BackColor = System.Drawing.Color.White;
			this.rduFile6View.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFile6View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFile6View.Location = new System.Drawing.Point(3, 124);
			this.rduFile6View.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFile6View.Name = "rduFile6View";
			this.rduFile6View.Size = new System.Drawing.Size(112, 19);
			this.rduFile6View.TabIndex = 248;
			this.rduFile6View.TabStop = false;
			this.rduFile6View.Text = "Image보기";
			this.rduFile6View.UseVisualStyleBackColor = true;
			// 
			// rduFile5View
			// 
			this.rduFile5View.BackColor = System.Drawing.Color.White;
			this.rduFile5View.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFile5View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFile5View.Location = new System.Drawing.Point(3, 100);
			this.rduFile5View.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFile5View.Name = "rduFile5View";
			this.rduFile5View.Size = new System.Drawing.Size(112, 19);
			this.rduFile5View.TabIndex = 247;
			this.rduFile5View.TabStop = false;
			this.rduFile5View.Text = "Image보기";
			this.rduFile5View.UseVisualStyleBackColor = true;
			// 
			// rduFile4View
			// 
			this.rduFile4View.BackColor = System.Drawing.Color.White;
			this.rduFile4View.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFile4View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFile4View.Location = new System.Drawing.Point(3, 75);
			this.rduFile4View.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFile4View.Name = "rduFile4View";
			this.rduFile4View.Size = new System.Drawing.Size(112, 19);
			this.rduFile4View.TabIndex = 246;
			this.rduFile4View.TabStop = false;
			this.rduFile4View.Text = "Image보기";
			this.rduFile4View.UseVisualStyleBackColor = true;
			// 
			// rduFileIns6
			// 
			this.rduFileIns6.BackColor = System.Drawing.Color.White;
			this.rduFileIns6.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileIns6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileIns6.Location = new System.Drawing.Point(275, 124);
			this.rduFileIns6.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileIns6.Name = "rduFileIns6";
			this.rduFileIns6.Size = new System.Drawing.Size(62, 19);
			this.rduFileIns6.TabIndex = 245;
			this.rduFileIns6.TabStop = false;
			this.rduFileIns6.Text = "...";
			this.rduFileIns6.UseVisualStyleBackColor = true;
			// 
			// rduFileIns4
			// 
			this.rduFileIns4.BackColor = System.Drawing.Color.White;
			this.rduFileIns4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileIns4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileIns4.Location = new System.Drawing.Point(275, 76);
			this.rduFileIns4.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileIns4.Name = "rduFileIns4";
			this.rduFileIns4.Size = new System.Drawing.Size(62, 19);
			this.rduFileIns4.TabIndex = 243;
			this.rduFileIns4.TabStop = false;
			this.rduFileIns4.Text = "...";
			this.rduFileIns4.UseVisualStyleBackColor = true;
			// 
			// rduFileIns5
			// 
			this.rduFileIns5.BackColor = System.Drawing.Color.White;
			this.rduFileIns5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileIns5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileIns5.Location = new System.Drawing.Point(275, 100);
			this.rduFileIns5.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileIns5.Name = "rduFileIns5";
			this.rduFileIns5.Size = new System.Drawing.Size(62, 19);
			this.rduFileIns5.TabIndex = 244;
			this.rduFileIns5.TabStop = false;
			this.rduFileIns5.Text = "...";
			this.rduFileIns5.UseVisualStyleBackColor = true;
			// 
			// rduFileDel6
			// 
			this.rduFileDel6.BackColor = System.Drawing.Color.White;
			this.rduFileDel6.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileDel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileDel6.Location = new System.Drawing.Point(341, 124);
			this.rduFileDel6.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileDel6.Name = "rduFileDel6";
			this.rduFileDel6.Size = new System.Drawing.Size(62, 19);
			this.rduFileDel6.TabIndex = 242;
			this.rduFileDel6.TabStop = false;
			this.rduFileDel6.Text = "Del";
			this.rduFileDel6.UseVisualStyleBackColor = true;
			// 
			// rduFileDel4
			// 
			this.rduFileDel4.BackColor = System.Drawing.Color.White;
			this.rduFileDel4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileDel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileDel4.Location = new System.Drawing.Point(341, 76);
			this.rduFileDel4.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileDel4.Name = "rduFileDel4";
			this.rduFileDel4.Size = new System.Drawing.Size(62, 19);
			this.rduFileDel4.TabIndex = 238;
			this.rduFileDel4.TabStop = false;
			this.rduFileDel4.Text = "Del";
			this.rduFileDel4.UseVisualStyleBackColor = true;
			// 
			// rduFileDel5
			// 
			this.rduFileDel5.BackColor = System.Drawing.Color.White;
			this.rduFileDel5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileDel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileDel5.Location = new System.Drawing.Point(341, 100);
			this.rduFileDel5.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileDel5.Name = "rduFileDel5";
			this.rduFileDel5.Size = new System.Drawing.Size(62, 19);
			this.rduFileDel5.TabIndex = 240;
			this.rduFileDel5.TabStop = false;
			this.rduFileDel5.Text = "Del";
			this.rduFileDel5.UseVisualStyleBackColor = true;
			// 
			// txtFile6
			// 
			this.txtFile6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFile6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile6.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFile6.Location = new System.Drawing.Point(120, 123);
			this.txtFile6.Name = "txtFile6";
			this.txtFile6.ReadOnly = true;
			this.txtFile6.Size = new System.Drawing.Size(150, 21);
			this.txtFile6.TabIndex = 241;
			this.txtFile6.TabStop = false;
			this.txtFile6.Tag = "IMAGE6";
			// 
			// txtFile5
			// 
			this.txtFile5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFile5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile5.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFile5.Location = new System.Drawing.Point(120, 99);
			this.txtFile5.Name = "txtFile5";
			this.txtFile5.ReadOnly = true;
			this.txtFile5.Size = new System.Drawing.Size(150, 21);
			this.txtFile5.TabIndex = 239;
			this.txtFile5.TabStop = false;
			this.txtFile5.Tag = "IMAGE5";
			// 
			// txtFile4
			// 
			this.txtFile4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFile4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile4.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFile4.Location = new System.Drawing.Point(120, 75);
			this.txtFile4.Name = "txtFile4";
			this.txtFile4.ReadOnly = true;
			this.txtFile4.Size = new System.Drawing.Size(150, 21);
			this.txtFile4.TabIndex = 237;
			this.txtFile4.TabStop = false;
			this.txtFile4.Tag = "IMAGE4";
			// 
			// rduFile3View
			// 
			this.rduFile3View.BackColor = System.Drawing.Color.White;
			this.rduFile3View.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFile3View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFile3View.Location = new System.Drawing.Point(3, 51);
			this.rduFile3View.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFile3View.Name = "rduFile3View";
			this.rduFile3View.Size = new System.Drawing.Size(112, 19);
			this.rduFile3View.TabIndex = 233;
			this.rduFile3View.TabStop = false;
			this.rduFile3View.Text = "Image보기";
			this.rduFile3View.UseVisualStyleBackColor = true;
			// 
			// rduFile2View
			// 
			this.rduFile2View.BackColor = System.Drawing.Color.White;
			this.rduFile2View.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFile2View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFile2View.Location = new System.Drawing.Point(3, 27);
			this.rduFile2View.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFile2View.Name = "rduFile2View";
			this.rduFile2View.Size = new System.Drawing.Size(112, 19);
			this.rduFile2View.TabIndex = 232;
			this.rduFile2View.TabStop = false;
			this.rduFile2View.Text = "Image보기";
			this.rduFile2View.UseVisualStyleBackColor = true;
			// 
			// rduFile1View
			// 
			this.rduFile1View.BackColor = System.Drawing.Color.White;
			this.rduFile1View.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFile1View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFile1View.Location = new System.Drawing.Point(3, 2);
			this.rduFile1View.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFile1View.Name = "rduFile1View";
			this.rduFile1View.Size = new System.Drawing.Size(112, 19);
			this.rduFile1View.TabIndex = 231;
			this.rduFile1View.TabStop = false;
			this.rduFile1View.Text = "Image보기";
			this.rduFile1View.UseVisualStyleBackColor = true;
			// 
			// rduFileIns3
			// 
			this.rduFileIns3.BackColor = System.Drawing.Color.White;
			this.rduFileIns3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileIns3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileIns3.Location = new System.Drawing.Point(275, 51);
			this.rduFileIns3.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileIns3.Name = "rduFileIns3";
			this.rduFileIns3.Size = new System.Drawing.Size(62, 19);
			this.rduFileIns3.TabIndex = 230;
			this.rduFileIns3.TabStop = false;
			this.rduFileIns3.Text = "...";
			this.rduFileIns3.UseVisualStyleBackColor = true;
			// 
			// rduFileIns1
			// 
			this.rduFileIns1.BackColor = System.Drawing.Color.White;
			this.rduFileIns1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileIns1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileIns1.Location = new System.Drawing.Point(275, 3);
			this.rduFileIns1.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileIns1.Name = "rduFileIns1";
			this.rduFileIns1.Size = new System.Drawing.Size(62, 19);
			this.rduFileIns1.TabIndex = 228;
			this.rduFileIns1.TabStop = false;
			this.rduFileIns1.Text = "...";
			this.rduFileIns1.UseVisualStyleBackColor = true;
			// 
			// rduFileIns2
			// 
			this.rduFileIns2.BackColor = System.Drawing.Color.White;
			this.rduFileIns2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileIns2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileIns2.Location = new System.Drawing.Point(275, 27);
			this.rduFileIns2.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileIns2.Name = "rduFileIns2";
			this.rduFileIns2.Size = new System.Drawing.Size(62, 19);
			this.rduFileIns2.TabIndex = 229;
			this.rduFileIns2.TabStop = false;
			this.rduFileIns2.Text = "...";
			this.rduFileIns2.UseVisualStyleBackColor = true;
			// 
			// rduFileDel3
			// 
			this.rduFileDel3.BackColor = System.Drawing.Color.White;
			this.rduFileDel3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileDel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileDel3.Location = new System.Drawing.Point(341, 51);
			this.rduFileDel3.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileDel3.Name = "rduFileDel3";
			this.rduFileDel3.Size = new System.Drawing.Size(62, 19);
			this.rduFileDel3.TabIndex = 227;
			this.rduFileDel3.TabStop = false;
			this.rduFileDel3.Text = "Del";
			this.rduFileDel3.UseVisualStyleBackColor = true;
			// 
			// rduFileDel1
			// 
			this.rduFileDel1.BackColor = System.Drawing.Color.White;
			this.rduFileDel1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileDel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileDel1.Location = new System.Drawing.Point(341, 3);
			this.rduFileDel1.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileDel1.Name = "rduFileDel1";
			this.rduFileDel1.Size = new System.Drawing.Size(62, 19);
			this.rduFileDel1.TabIndex = 221;
			this.rduFileDel1.TabStop = false;
			this.rduFileDel1.Text = "Del";
			this.rduFileDel1.UseVisualStyleBackColor = true;
			// 
			// rduFileDel2
			// 
			this.rduFileDel2.BackColor = System.Drawing.Color.White;
			this.rduFileDel2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rduFileDel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rduFileDel2.Location = new System.Drawing.Point(341, 27);
			this.rduFileDel2.MaximumSize = new System.Drawing.Size(0, 19);
			this.rduFileDel2.Name = "rduFileDel2";
			this.rduFileDel2.Size = new System.Drawing.Size(62, 19);
			this.rduFileDel2.TabIndex = 224;
			this.rduFileDel2.TabStop = false;
			this.rduFileDel2.Text = "Del";
			this.rduFileDel2.UseVisualStyleBackColor = true;
			// 
			// txtFile3
			// 
			this.txtFile3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFile3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile3.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFile3.Location = new System.Drawing.Point(120, 51);
			this.txtFile3.Name = "txtFile3";
			this.txtFile3.ReadOnly = true;
			this.txtFile3.Size = new System.Drawing.Size(150, 21);
			this.txtFile3.TabIndex = 226;
			this.txtFile3.TabStop = false;
			this.txtFile3.Tag = "IMAGE3";
			// 
			// txtFile2
			// 
			this.txtFile2.BackColor = System.Drawing.SystemColors.Control;
			this.txtFile2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFile2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile2.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFile2.Location = new System.Drawing.Point(120, 27);
			this.txtFile2.Name = "txtFile2";
			this.txtFile2.ReadOnly = true;
			this.txtFile2.Size = new System.Drawing.Size(150, 21);
			this.txtFile2.TabIndex = 223;
			this.txtFile2.TabStop = false;
			this.txtFile2.Tag = "IMAGE2";
			// 
			// txtFile1
			// 
			this.txtFile1.BackColor = System.Drawing.SystemColors.Control;
			this.txtFile1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtFile1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile1.Font = new System.Drawing.Font("굴림체", 9F);
			this.txtFile1.Location = new System.Drawing.Point(120, 3);
			this.txtFile1.Name = "txtFile1";
			this.txtFile1.ReadOnly = true;
			this.txtFile1.Size = new System.Drawing.Size(150, 21);
			this.txtFile1.TabIndex = 220;
			this.txtFile1.TabStop = false;
			this.txtFile1.Tag = "IMAGE1";
			// 
			// tag버전관리
			// 
			this.tag버전관리.Controls.Add(this.panelExt70);
			this.tag버전관리.Location = new System.Drawing.Point(4, 24);
			this.tag버전관리.Name = "tag버전관리";
			this.tag버전관리.Size = new System.Drawing.Size(772, 626);
			this.tag버전관리.TabIndex = 6;
			this.tag버전관리.Text = "버전관리";
			this.tag버전관리.UseVisualStyleBackColor = true;
			// 
			// panelExt70
			// 
			this.panelExt70.Controls.Add(this.tableLayoutPanel2);
			this.panelExt70.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt70.Location = new System.Drawing.Point(0, 0);
			this.panelExt70.Name = "panelExt70";
			this.panelExt70.Size = new System.Drawing.Size(772, 626);
			this.panelExt70.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.panelExt71, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.panelExt72, 0, 1);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(772, 602);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// panelExt71
			// 
			this.panelExt71.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelExt71.Controls.Add(this.btn삭제);
			this.panelExt71.Controls.Add(this.btn추가);
			this.panelExt71.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt71.Location = new System.Drawing.Point(3, 3);
			this.panelExt71.Name = "panelExt71";
			this.panelExt71.Size = new System.Drawing.Size(766, 25);
			this.panelExt71.TabIndex = 0;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(697, 2);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(62, 19);
			this.btn삭제.TabIndex = 1;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = true;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(632, 2);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(62, 19);
			this.btn추가.TabIndex = 0;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = true;
			// 
			// panelExt72
			// 
			this.panelExt72.Controls.Add(this.splitContainer2);
			this.panelExt72.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt72.Location = new System.Drawing.Point(3, 34);
			this.panelExt72.Name = "panelExt72";
			this.panelExt72.Size = new System.Drawing.Size(766, 565);
			this.panelExt72.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.panelExt73);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.pnl버전관리);
			this.splitContainer2.Size = new System.Drawing.Size(766, 565);
			this.splitContainer2.SplitterDistance = 235;
			this.splitContainer2.TabIndex = 0;
			// 
			// panelExt73
			// 
			this.panelExt73.Controls.Add(this._flex버전관리);
			this.panelExt73.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt73.Location = new System.Drawing.Point(0, 0);
			this.panelExt73.Name = "panelExt73";
			this.panelExt73.Size = new System.Drawing.Size(235, 565);
			this.panelExt73.TabIndex = 0;
			// 
			// _flex버전관리
			// 
			this._flex버전관리.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex버전관리.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex버전관리.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex버전관리.AutoResize = false;
			this._flex버전관리.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex버전관리.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex버전관리.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex버전관리.EnabledHeaderCheck = true;
			this._flex버전관리.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex버전관리.Location = new System.Drawing.Point(0, 0);
			this._flex버전관리.Name = "_flex버전관리";
			this._flex버전관리.Rows.Count = 1;
			this._flex버전관리.Rows.DefaultSize = 18;
			this._flex버전관리.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex버전관리.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex버전관리.ShowSort = false;
			this._flex버전관리.Size = new System.Drawing.Size(235, 565);
			this._flex버전관리.StyleInfo = resources.GetString("_flex버전관리.StyleInfo");
			this._flex버전관리.TabIndex = 0;
			this._flex버전관리.UseGridCalculator = true;
			// 
			// pnl버전관리
			// 
			this.pnl버전관리.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.pnl버전관리.Controls.Add(this.lbl배포일자);
			this.pnl버전관리.Controls.Add(this.lbl_버전관리_비고);
			this.pnl버전관리.Controls.Add(this.lbl최종소스여부);
			this.pnl버전관리.Controls.Add(this.dtp배포일자);
			this.pnl버전관리.Controls.Add(this.lbl난이도);
			this.pnl버전관리.Controls.Add(this.lbl제품사항);
			this.pnl버전관리.Controls.Add(this.lbl담당자부);
			this.pnl버전관리.Controls.Add(this.cbo최종소스여부);
			this.pnl버전관리.Controls.Add(this.lbl담당자정);
			this.pnl버전관리.Controls.Add(this.lbl지원여부);
			this.pnl버전관리.Controls.Add(this.lbl개발자);
			this.pnl버전관리.Controls.Add(this.cbo난이도);
			this.pnl버전관리.Controls.Add(this.lbl지원OS);
			this.pnl버전관리.Controls.Add(this.bp담당자부);
			this.pnl버전관리.Controls.Add(this.lbl타입);
			this.pnl버전관리.Controls.Add(this.bp담당자정);
			this.pnl버전관리.Controls.Add(this.lbl언어);
			this.pnl버전관리.Controls.Add(this.bp개발자);
			this.pnl버전관리.Controls.Add(this.lbl버전);
			this.pnl버전관리.Controls.Add(this.txt비고);
			this.pnl버전관리.Controls.Add(this.lbl순번);
			this.pnl버전관리.Controls.Add(this.txt제품사항);
			this.pnl버전관리.Controls.Add(this.cbo지원여부);
			this.pnl버전관리.Controls.Add(this.txt지원OS);
			this.pnl버전관리.Controls.Add(this.txt타입);
			this.pnl버전관리.Controls.Add(this.txt언어);
			this.pnl버전관리.Controls.Add(this.txt버전);
			this.pnl버전관리.Controls.Add(this.txt순번);
			this.pnl버전관리.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl버전관리.Location = new System.Drawing.Point(0, 0);
			this.pnl버전관리.Name = "pnl버전관리";
			this.pnl버전관리.Size = new System.Drawing.Size(527, 565);
			this.pnl버전관리.TabIndex = 0;
			// 
			// lbl배포일자
			// 
			this.lbl배포일자.BackColor = System.Drawing.Color.Transparent;
			this.lbl배포일자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl배포일자.Location = new System.Drawing.Point(6, 262);
			this.lbl배포일자.Name = "lbl배포일자";
			this.lbl배포일자.Size = new System.Drawing.Size(156, 16);
			this.lbl배포일자.TabIndex = 52;
			this.lbl배포일자.Text = "배포일자";
			this.lbl배포일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_버전관리_비고
			// 
			this.lbl_버전관리_비고.BackColor = System.Drawing.Color.Transparent;
			this.lbl_버전관리_비고.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl_버전관리_비고.Location = new System.Drawing.Point(6, 395);
			this.lbl_버전관리_비고.Name = "lbl_버전관리_비고";
			this.lbl_버전관리_비고.Size = new System.Drawing.Size(156, 16);
			this.lbl_버전관리_비고.TabIndex = 54;
			this.lbl_버전관리_비고.Text = "비고";
			this.lbl_버전관리_비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl최종소스여부
			// 
			this.lbl최종소스여부.BackColor = System.Drawing.Color.Transparent;
			this.lbl최종소스여부.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl최종소스여부.Location = new System.Drawing.Point(6, 238);
			this.lbl최종소스여부.Name = "lbl최종소스여부";
			this.lbl최종소스여부.Size = new System.Drawing.Size(156, 16);
			this.lbl최종소스여부.TabIndex = 51;
			this.lbl최종소스여부.Text = "최종소스여부";
			this.lbl최종소스여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp배포일자
			// 
			this.dtp배포일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp배포일자.Location = new System.Drawing.Point(167, 259);
			this.dtp배포일자.Mask = "####/##/##";
			this.dtp배포일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp배포일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp배포일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp배포일자.Modified = true;
			this.dtp배포일자.Name = "dtp배포일자";
			this.dtp배포일자.Size = new System.Drawing.Size(89, 21);
			this.dtp배포일자.TabIndex = 237;
			this.dtp배포일자.Tag = "DTS_DSTB";
			this.dtp배포일자.Value = new System.DateTime(((long)(0)));
			// 
			// lbl난이도
			// 
			this.lbl난이도.BackColor = System.Drawing.Color.Transparent;
			this.lbl난이도.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl난이도.Location = new System.Drawing.Point(6, 215);
			this.lbl난이도.Name = "lbl난이도";
			this.lbl난이도.Size = new System.Drawing.Size(156, 16);
			this.lbl난이도.TabIndex = 50;
			this.lbl난이도.Text = "난이도";
			this.lbl난이도.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl제품사항
			// 
			this.lbl제품사항.BackColor = System.Drawing.Color.Transparent;
			this.lbl제품사항.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl제품사항.Location = new System.Drawing.Point(6, 291);
			this.lbl제품사항.Name = "lbl제품사항";
			this.lbl제품사항.Size = new System.Drawing.Size(156, 16);
			this.lbl제품사항.TabIndex = 53;
			this.lbl제품사항.Text = "제품사항";
			this.lbl제품사항.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl담당자부
			// 
			this.lbl담당자부.BackColor = System.Drawing.Color.Transparent;
			this.lbl담당자부.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl담당자부.Location = new System.Drawing.Point(6, 191);
			this.lbl담당자부.Name = "lbl담당자부";
			this.lbl담당자부.Size = new System.Drawing.Size(156, 16);
			this.lbl담당자부.TabIndex = 49;
			this.lbl담당자부.Text = "담당자(부)";
			this.lbl담당자부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo최종소스여부
			// 
			this.cbo최종소스여부.AutoDropDown = true;
			this.cbo최종소스여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo최종소스여부.FormattingEnabled = true;
			this.cbo최종소스여부.ItemHeight = 12;
			this.cbo최종소스여부.Location = new System.Drawing.Point(167, 235);
			this.cbo최종소스여부.Name = "cbo최종소스여부";
			this.cbo최종소스여부.Size = new System.Drawing.Size(185, 20);
			this.cbo최종소스여부.TabIndex = 236;
			this.cbo최종소스여부.Tag = "YN_FINL_SOURCE";
			// 
			// lbl담당자정
			// 
			this.lbl담당자정.BackColor = System.Drawing.Color.Transparent;
			this.lbl담당자정.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl담당자정.Location = new System.Drawing.Point(6, 168);
			this.lbl담당자정.Name = "lbl담당자정";
			this.lbl담당자정.Size = new System.Drawing.Size(156, 16);
			this.lbl담당자정.TabIndex = 48;
			this.lbl담당자정.Text = "담당자(정)";
			this.lbl담당자정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl지원여부
			// 
			this.lbl지원여부.BackColor = System.Drawing.Color.Transparent;
			this.lbl지원여부.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl지원여부.Location = new System.Drawing.Point(6, 123);
			this.lbl지원여부.Name = "lbl지원여부";
			this.lbl지원여부.Size = new System.Drawing.Size(156, 16);
			this.lbl지원여부.TabIndex = 52;
			this.lbl지원여부.Text = "지원여부";
			this.lbl지원여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl개발자
			// 
			this.lbl개발자.BackColor = System.Drawing.Color.Transparent;
			this.lbl개발자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl개발자.Location = new System.Drawing.Point(6, 146);
			this.lbl개발자.Name = "lbl개발자";
			this.lbl개발자.Size = new System.Drawing.Size(156, 16);
			this.lbl개발자.TabIndex = 47;
			this.lbl개발자.Text = "개발자";
			this.lbl개발자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo난이도
			// 
			this.cbo난이도.AutoDropDown = true;
			this.cbo난이도.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo난이도.FormattingEnabled = true;
			this.cbo난이도.ItemHeight = 12;
			this.cbo난이도.Location = new System.Drawing.Point(167, 212);
			this.cbo난이도.Name = "cbo난이도";
			this.cbo난이도.Size = new System.Drawing.Size(185, 20);
			this.cbo난이도.TabIndex = 235;
			this.cbo난이도.Tag = "FG_LVL";
			// 
			// lbl지원OS
			// 
			this.lbl지원OS.BackColor = System.Drawing.Color.Transparent;
			this.lbl지원OS.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl지원OS.Location = new System.Drawing.Point(6, 99);
			this.lbl지원OS.Name = "lbl지원OS";
			this.lbl지원OS.Size = new System.Drawing.Size(156, 16);
			this.lbl지원OS.TabIndex = 51;
			this.lbl지원OS.Text = "지원OS";
			this.lbl지원OS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bp담당자부
			// 
			this.bp담당자부.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.bp담당자부.Location = new System.Drawing.Point(167, 189);
			this.bp담당자부.Name = "bp담당자부";
			this.bp담당자부.Size = new System.Drawing.Size(185, 21);
			this.bp담당자부.TabIndex = 234;
			this.bp담당자부.TabStop = false;
			this.bp담당자부.Tag = "ID_SMGM_EMP,NM_SMGM_EMP";
			// 
			// lbl타입
			// 
			this.lbl타입.BackColor = System.Drawing.Color.Transparent;
			this.lbl타입.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl타입.Location = new System.Drawing.Point(6, 76);
			this.lbl타입.Name = "lbl타입";
			this.lbl타입.Size = new System.Drawing.Size(156, 16);
			this.lbl타입.TabIndex = 50;
			this.lbl타입.Text = "타입";
			this.lbl타입.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bp담당자정
			// 
			this.bp담당자정.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.bp담당자정.Location = new System.Drawing.Point(167, 166);
			this.bp담당자정.Name = "bp담당자정";
			this.bp담당자정.Size = new System.Drawing.Size(185, 21);
			this.bp담당자정.TabIndex = 233;
			this.bp담당자정.TabStop = false;
			this.bp담당자정.Tag = "ID_PMGM_EMP,NM_PMGM_EMP";
			// 
			// lbl언어
			// 
			this.lbl언어.BackColor = System.Drawing.Color.Transparent;
			this.lbl언어.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl언어.Location = new System.Drawing.Point(6, 52);
			this.lbl언어.Name = "lbl언어";
			this.lbl언어.Size = new System.Drawing.Size(156, 16);
			this.lbl언어.TabIndex = 49;
			this.lbl언어.Text = "언어";
			this.lbl언어.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bp개발자
			// 
			this.bp개발자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.bp개발자.Location = new System.Drawing.Point(167, 143);
			this.bp개발자.Name = "bp개발자";
			this.bp개발자.Size = new System.Drawing.Size(185, 21);
			this.bp개발자.TabIndex = 232;
			this.bp개발자.TabStop = false;
			this.bp개발자.Tag = "ID_DEV_EMP,NM_DEV_EMP";
			// 
			// lbl버전
			// 
			this.lbl버전.BackColor = System.Drawing.Color.Transparent;
			this.lbl버전.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl버전.Location = new System.Drawing.Point(6, 29);
			this.lbl버전.Name = "lbl버전";
			this.lbl버전.Size = new System.Drawing.Size(156, 16);
			this.lbl버전.TabIndex = 48;
			this.lbl버전.Text = "버전";
			this.lbl버전.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt비고
			// 
			this.txt비고.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txt비고.BackColor = System.Drawing.Color.White;
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt비고.Location = new System.Drawing.Point(167, 391);
			this.txt비고.MaxLength = 4000;
			this.txt비고.Multiline = true;
			this.txt비고.Name = "txt비고";
			this.txt비고.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txt비고.Size = new System.Drawing.Size(350, 99);
			this.txt비고.TabIndex = 231;
			this.txt비고.Tag = "DC_REMARK";
			this.txt비고.UseKeyEnter = false;
			// 
			// lbl순번
			// 
			this.lbl순번.BackColor = System.Drawing.Color.Transparent;
			this.lbl순번.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl순번.Location = new System.Drawing.Point(6, 7);
			this.lbl순번.Name = "lbl순번";
			this.lbl순번.Size = new System.Drawing.Size(156, 16);
			this.lbl순번.TabIndex = 47;
			this.lbl순번.Text = "순번";
			this.lbl순번.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt제품사항
			// 
			this.txt제품사항.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txt제품사항.BackColor = System.Drawing.Color.White;
			this.txt제품사항.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt제품사항.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt제품사항.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt제품사항.Location = new System.Drawing.Point(167, 286);
			this.txt제품사항.MaxLength = 4000;
			this.txt제품사항.Multiline = true;
			this.txt제품사항.Name = "txt제품사항";
			this.txt제품사항.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txt제품사항.Size = new System.Drawing.Size(350, 99);
			this.txt제품사항.TabIndex = 230;
			this.txt제품사항.Tag = "DC_ITEM_REMARK";
			this.txt제품사항.UseKeyEnter = false;
			// 
			// cbo지원여부
			// 
			this.cbo지원여부.AutoDropDown = true;
			this.cbo지원여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo지원여부.FormattingEnabled = true;
			this.cbo지원여부.ItemHeight = 12;
			this.cbo지원여부.Location = new System.Drawing.Point(167, 120);
			this.cbo지원여부.Name = "cbo지원여부";
			this.cbo지원여부.Size = new System.Drawing.Size(185, 20);
			this.cbo지원여부.TabIndex = 229;
			this.cbo지원여부.Tag = "YN_USE";
			// 
			// txt지원OS
			// 
			this.txt지원OS.BackColor = System.Drawing.Color.White;
			this.txt지원OS.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt지원OS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt지원OS.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt지원OS.Location = new System.Drawing.Point(167, 96);
			this.txt지원OS.MaxLength = 20;
			this.txt지원OS.Name = "txt지원OS";
			this.txt지원OS.Size = new System.Drawing.Size(185, 21);
			this.txt지원OS.TabIndex = 228;
			this.txt지원OS.Tag = "NM_OS";
			// 
			// txt타입
			// 
			this.txt타입.BackColor = System.Drawing.Color.White;
			this.txt타입.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt타입.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt타입.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt타입.Location = new System.Drawing.Point(167, 73);
			this.txt타입.MaxLength = 20;
			this.txt타입.Name = "txt타입";
			this.txt타입.Size = new System.Drawing.Size(185, 21);
			this.txt타입.TabIndex = 227;
			this.txt타입.Tag = "NM_ITEM_TYPE";
			// 
			// txt언어
			// 
			this.txt언어.BackColor = System.Drawing.Color.White;
			this.txt언어.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt언어.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt언어.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt언어.Location = new System.Drawing.Point(167, 50);
			this.txt언어.MaxLength = 20;
			this.txt언어.Name = "txt언어";
			this.txt언어.Size = new System.Drawing.Size(185, 21);
			this.txt언어.TabIndex = 226;
			this.txt언어.Tag = "NM_LANGUAGE";
			// 
			// txt버전
			// 
			this.txt버전.BackColor = System.Drawing.Color.White;
			this.txt버전.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt버전.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt버전.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt버전.Location = new System.Drawing.Point(167, 27);
			this.txt버전.MaxLength = 20;
			this.txt버전.Name = "txt버전";
			this.txt버전.Size = new System.Drawing.Size(185, 21);
			this.txt버전.TabIndex = 225;
			this.txt버전.Tag = "NM_VERSION";
			// 
			// txt순번
			// 
			this.txt순번.BackColor = System.Drawing.Color.White;
			this.txt순번.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt순번.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt순번.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txt순번.Location = new System.Drawing.Point(167, 4);
			this.txt순번.MaxLength = 20;
			this.txt순번.Name = "txt순번";
			this.txt순번.Size = new System.Drawing.Size(185, 21);
			this.txt순번.TabIndex = 224;
			this.txt순번.Tag = "SEQ";
			// 
			// tpgStndItem
			// 
			this.tpgStndItem.Controls.Add(this.pnlStndItem);
			this.tpgStndItem.Location = new System.Drawing.Point(4, 24);
			this.tpgStndItem.Name = "tpgStndItem";
			this.tpgStndItem.Padding = new System.Windows.Forms.Padding(3);
			this.tpgStndItem.Size = new System.Drawing.Size(772, 626);
			this.tpgStndItem.TabIndex = 7;
			this.tpgStndItem.Text = "규격정보";
			this.tpgStndItem.UseVisualStyleBackColor = true;
			// 
			// pnlStndItem
			// 
			this.pnlStndItem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlStndItem.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem85,
            this.oneGridItem86,
            this.oneGridItem87,
            this.oneGridItem88,
            this.oneGridItem89,
            this.oneGridItem90,
            this.oneGridItem91,
            this.oneGridItem92,
            this.oneGridItem93,
            this.oneGridItem94});
			this.pnlStndItem.Location = new System.Drawing.Point(3, 3);
			this.pnlStndItem.Name = "pnlStndItem";
			this.pnlStndItem.Size = new System.Drawing.Size(766, 620);
			this.pnlStndItem.TabIndex = 0;
			// 
			// oneGridItem85
			// 
			this.oneGridItem85.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem85.Controls.Add(this.bpPanelControl51);
			this.oneGridItem85.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem85.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem85.Name = "oneGridItem85";
			this.oneGridItem85.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem85.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem85.TabIndex = 0;
			// 
			// bpPanelControl51
			// 
			this.bpPanelControl51.Controls.Add(this.lblStndItem1);
			this.bpPanelControl51.Controls.Add(this._txtStndItem01);
			this.bpPanelControl51.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl51.Name = "bpPanelControl51";
			this.bpPanelControl51.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl51.TabIndex = 1;
			this.bpPanelControl51.Text = "bpPanelControl51";
			// 
			// lblStndItem1
			// 
			this.lblStndItem1.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem1.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem1.Name = "lblStndItem1";
			this.lblStndItem1.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem1.TabIndex = 38;
			this.lblStndItem1.Tag = "CD_PURGRP";
			this.lblStndItem1.Text = "규격1";
			this.lblStndItem1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem1.Visible = false;
			// 
			// _txtStndItem01
			// 
			this._txtStndItem01.BackColor = System.Drawing.Color.White;
			this._txtStndItem01.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtStndItem01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtStndItem01.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._txtStndItem01.Location = new System.Drawing.Point(157, 1);
			this._txtStndItem01.MaxLength = 100;
			this._txtStndItem01.Name = "_txtStndItem01";
			this._txtStndItem01.Size = new System.Drawing.Size(185, 21);
			this._txtStndItem01.TabIndex = 0;
			this._txtStndItem01.Tag = "CD_STND_ITEM_1";
			this._txtStndItem01.Visible = false;
			// 
			// oneGridItem86
			// 
			this.oneGridItem86.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem86.Controls.Add(this.bpPanelControl159);
			this.oneGridItem86.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem86.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem86.Name = "oneGridItem86";
			this.oneGridItem86.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem86.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem86.TabIndex = 1;
			// 
			// bpPanelControl159
			// 
			this.bpPanelControl159.Controls.Add(this.lblStndItem2);
			this.bpPanelControl159.Controls.Add(this._txtStndItem02);
			this.bpPanelControl159.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl159.Name = "bpPanelControl159";
			this.bpPanelControl159.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl159.TabIndex = 2;
			this.bpPanelControl159.Text = "bpPanelControl159";
			// 
			// lblStndItem2
			// 
			this.lblStndItem2.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem2.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem2.Name = "lblStndItem2";
			this.lblStndItem2.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem2.TabIndex = 38;
			this.lblStndItem2.Tag = "CD_PURGRP";
			this.lblStndItem2.Text = "규격2";
			this.lblStndItem2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem2.Visible = false;
			// 
			// _txtStndItem02
			// 
			this._txtStndItem02.BackColor = System.Drawing.Color.White;
			this._txtStndItem02.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtStndItem02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtStndItem02.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._txtStndItem02.Location = new System.Drawing.Point(157, 1);
			this._txtStndItem02.MaxLength = 100;
			this._txtStndItem02.Name = "_txtStndItem02";
			this._txtStndItem02.Size = new System.Drawing.Size(185, 21);
			this._txtStndItem02.TabIndex = 0;
			this._txtStndItem02.Tag = "CD_STND_ITEM_2";
			this._txtStndItem02.Visible = false;
			// 
			// oneGridItem87
			// 
			this.oneGridItem87.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem87.Controls.Add(this.bpPanelControl160);
			this.oneGridItem87.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem87.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem87.Name = "oneGridItem87";
			this.oneGridItem87.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem87.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem87.TabIndex = 2;
			// 
			// bpPanelControl160
			// 
			this.bpPanelControl160.Controls.Add(this.lblStndItem3);
			this.bpPanelControl160.Controls.Add(this._txtStndItem03);
			this.bpPanelControl160.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl160.Name = "bpPanelControl160";
			this.bpPanelControl160.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl160.TabIndex = 2;
			this.bpPanelControl160.Text = "bpPanelControl160";
			// 
			// lblStndItem3
			// 
			this.lblStndItem3.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem3.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem3.Name = "lblStndItem3";
			this.lblStndItem3.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem3.TabIndex = 38;
			this.lblStndItem3.Tag = "CD_PURGRP";
			this.lblStndItem3.Text = "규격3";
			this.lblStndItem3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem3.Visible = false;
			// 
			// _txtStndItem03
			// 
			this._txtStndItem03.BackColor = System.Drawing.Color.White;
			this._txtStndItem03.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtStndItem03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtStndItem03.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._txtStndItem03.Location = new System.Drawing.Point(157, 1);
			this._txtStndItem03.MaxLength = 100;
			this._txtStndItem03.Name = "_txtStndItem03";
			this._txtStndItem03.Size = new System.Drawing.Size(185, 21);
			this._txtStndItem03.TabIndex = 0;
			this._txtStndItem03.Tag = "CD_STND_ITEM_3";
			this._txtStndItem03.Visible = false;
			// 
			// oneGridItem88
			// 
			this.oneGridItem88.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem88.Controls.Add(this.bpPanelControl161);
			this.oneGridItem88.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem88.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem88.Name = "oneGridItem88";
			this.oneGridItem88.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem88.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem88.TabIndex = 3;
			// 
			// bpPanelControl161
			// 
			this.bpPanelControl161.Controls.Add(this.lblStndItem4);
			this.bpPanelControl161.Controls.Add(this._txtStndItem04);
			this.bpPanelControl161.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl161.Name = "bpPanelControl161";
			this.bpPanelControl161.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl161.TabIndex = 2;
			this.bpPanelControl161.Text = "bpPanelControl161";
			// 
			// lblStndItem4
			// 
			this.lblStndItem4.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem4.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem4.Name = "lblStndItem4";
			this.lblStndItem4.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem4.TabIndex = 38;
			this.lblStndItem4.Tag = "CD_PURGRP";
			this.lblStndItem4.Text = "규격4";
			this.lblStndItem4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem4.Visible = false;
			// 
			// _txtStndItem04
			// 
			this._txtStndItem04.BackColor = System.Drawing.Color.White;
			this._txtStndItem04.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtStndItem04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtStndItem04.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._txtStndItem04.Location = new System.Drawing.Point(157, 1);
			this._txtStndItem04.MaxLength = 100;
			this._txtStndItem04.Name = "_txtStndItem04";
			this._txtStndItem04.Size = new System.Drawing.Size(185, 21);
			this._txtStndItem04.TabIndex = 0;
			this._txtStndItem04.Tag = "CD_STND_ITEM_4";
			this._txtStndItem04.Visible = false;
			// 
			// oneGridItem89
			// 
			this.oneGridItem89.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem89.Controls.Add(this.bpPanelControl162);
			this.oneGridItem89.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem89.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem89.Name = "oneGridItem89";
			this.oneGridItem89.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem89.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem89.TabIndex = 4;
			// 
			// bpPanelControl162
			// 
			this.bpPanelControl162.Controls.Add(this.lblStndItem5);
			this.bpPanelControl162.Controls.Add(this._txtStndItem05);
			this.bpPanelControl162.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl162.Name = "bpPanelControl162";
			this.bpPanelControl162.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl162.TabIndex = 2;
			this.bpPanelControl162.Text = "bpPanelControl162";
			// 
			// lblStndItem5
			// 
			this.lblStndItem5.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem5.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem5.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem5.Name = "lblStndItem5";
			this.lblStndItem5.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem5.TabIndex = 38;
			this.lblStndItem5.Tag = "CD_PURGRP";
			this.lblStndItem5.Text = "규격5";
			this.lblStndItem5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem5.Visible = false;
			// 
			// _txtStndItem05
			// 
			this._txtStndItem05.BackColor = System.Drawing.Color.White;
			this._txtStndItem05.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtStndItem05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtStndItem05.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._txtStndItem05.Location = new System.Drawing.Point(157, 1);
			this._txtStndItem05.MaxLength = 100;
			this._txtStndItem05.Name = "_txtStndItem05";
			this._txtStndItem05.Size = new System.Drawing.Size(185, 21);
			this._txtStndItem05.TabIndex = 0;
			this._txtStndItem05.Tag = "CD_STND_ITEM_5";
			this._txtStndItem05.Visible = false;
			// 
			// oneGridItem90
			// 
			this.oneGridItem90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem90.Controls.Add(this.bpPanelControl163);
			this.oneGridItem90.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem90.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem90.Name = "oneGridItem90";
			this.oneGridItem90.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem90.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem90.TabIndex = 5;
			// 
			// bpPanelControl163
			// 
			this.bpPanelControl163.Controls.Add(this._txtNumStndItem01);
			this.bpPanelControl163.Controls.Add(this.lblStndItem6);
			this.bpPanelControl163.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl163.Name = "bpPanelControl163";
			this.bpPanelControl163.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl163.TabIndex = 2;
			this.bpPanelControl163.Text = "bpPanelControl163";
			// 
			// _txtNumStndItem01
			// 
			this._txtNumStndItem01.BackColor = System.Drawing.Color.White;
			this._txtNumStndItem01.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtNumStndItem01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtNumStndItem01.CurrencyDecimalDigits = 4;
			this._txtNumStndItem01.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._txtNumStndItem01.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtNumStndItem01.ForeColor = System.Drawing.SystemColors.ControlText;
			this._txtNumStndItem01.Location = new System.Drawing.Point(157, 1);
			this._txtNumStndItem01.MaxLength = 3;
			this._txtNumStndItem01.Name = "_txtNumStndItem01";
			this._txtNumStndItem01.NullString = "0";
			this._txtNumStndItem01.PositiveColor = System.Drawing.Color.Black;
			this._txtNumStndItem01.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtNumStndItem01.Size = new System.Drawing.Size(185, 21);
			this._txtNumStndItem01.TabIndex = 39;
			this._txtNumStndItem01.Tag = "NUM_STND_ITEM_1";
			this._txtNumStndItem01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtNumStndItem01.Visible = false;
			// 
			// lblStndItem6
			// 
			this.lblStndItem6.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem6.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem6.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem6.Name = "lblStndItem6";
			this.lblStndItem6.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem6.TabIndex = 38;
			this.lblStndItem6.Tag = "CD_PURGRP";
			this.lblStndItem6.Text = "규격6";
			this.lblStndItem6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem6.Visible = false;
			// 
			// oneGridItem91
			// 
			this.oneGridItem91.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem91.Controls.Add(this.bpPanelControl164);
			this.oneGridItem91.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem91.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem91.Name = "oneGridItem91";
			this.oneGridItem91.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem91.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem91.TabIndex = 6;
			// 
			// bpPanelControl164
			// 
			this.bpPanelControl164.Controls.Add(this._txtNumStndItem02);
			this.bpPanelControl164.Controls.Add(this.lblStndItem7);
			this.bpPanelControl164.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl164.Name = "bpPanelControl164";
			this.bpPanelControl164.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl164.TabIndex = 2;
			this.bpPanelControl164.Text = "bpPanelControl164";
			// 
			// _txtNumStndItem02
			// 
			this._txtNumStndItem02.BackColor = System.Drawing.Color.White;
			this._txtNumStndItem02.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtNumStndItem02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtNumStndItem02.CurrencyDecimalDigits = 4;
			this._txtNumStndItem02.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._txtNumStndItem02.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtNumStndItem02.ForeColor = System.Drawing.SystemColors.ControlText;
			this._txtNumStndItem02.Location = new System.Drawing.Point(157, 1);
			this._txtNumStndItem02.MaxLength = 3;
			this._txtNumStndItem02.Name = "_txtNumStndItem02";
			this._txtNumStndItem02.NullString = "0";
			this._txtNumStndItem02.PositiveColor = System.Drawing.Color.Black;
			this._txtNumStndItem02.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtNumStndItem02.Size = new System.Drawing.Size(185, 21);
			this._txtNumStndItem02.TabIndex = 40;
			this._txtNumStndItem02.Tag = "NUM_STND_ITEM_2";
			this._txtNumStndItem02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtNumStndItem02.Visible = false;
			// 
			// lblStndItem7
			// 
			this.lblStndItem7.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem7.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem7.Name = "lblStndItem7";
			this.lblStndItem7.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem7.TabIndex = 38;
			this.lblStndItem7.Tag = "CD_PURGRP";
			this.lblStndItem7.Text = "규격7";
			this.lblStndItem7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem7.Visible = false;
			// 
			// oneGridItem92
			// 
			this.oneGridItem92.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem92.Controls.Add(this.bpPanelControl165);
			this.oneGridItem92.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem92.Location = new System.Drawing.Point(0, 161);
			this.oneGridItem92.Name = "oneGridItem92";
			this.oneGridItem92.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem92.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem92.TabIndex = 7;
			// 
			// bpPanelControl165
			// 
			this.bpPanelControl165.Controls.Add(this._txtNumStndItem03);
			this.bpPanelControl165.Controls.Add(this.lblStndItem8);
			this.bpPanelControl165.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl165.Name = "bpPanelControl165";
			this.bpPanelControl165.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl165.TabIndex = 2;
			this.bpPanelControl165.Text = "bpPanelControl165";
			// 
			// _txtNumStndItem03
			// 
			this._txtNumStndItem03.BackColor = System.Drawing.Color.White;
			this._txtNumStndItem03.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtNumStndItem03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtNumStndItem03.CurrencyDecimalDigits = 4;
			this._txtNumStndItem03.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._txtNumStndItem03.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtNumStndItem03.ForeColor = System.Drawing.SystemColors.ControlText;
			this._txtNumStndItem03.Location = new System.Drawing.Point(157, 1);
			this._txtNumStndItem03.MaxLength = 3;
			this._txtNumStndItem03.Name = "_txtNumStndItem03";
			this._txtNumStndItem03.NullString = "0";
			this._txtNumStndItem03.PositiveColor = System.Drawing.Color.Black;
			this._txtNumStndItem03.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtNumStndItem03.Size = new System.Drawing.Size(185, 21);
			this._txtNumStndItem03.TabIndex = 40;
			this._txtNumStndItem03.Tag = "NUM_STND_ITEM_3";
			this._txtNumStndItem03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtNumStndItem03.Visible = false;
			// 
			// lblStndItem8
			// 
			this.lblStndItem8.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem8.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem8.Name = "lblStndItem8";
			this.lblStndItem8.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem8.TabIndex = 38;
			this.lblStndItem8.Tag = "CD_PURGRP";
			this.lblStndItem8.Text = "규격8";
			this.lblStndItem8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem8.Visible = false;
			// 
			// oneGridItem93
			// 
			this.oneGridItem93.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem93.Controls.Add(this.bpPanelControl166);
			this.oneGridItem93.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem93.Location = new System.Drawing.Point(0, 184);
			this.oneGridItem93.Name = "oneGridItem93";
			this.oneGridItem93.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem93.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem93.TabIndex = 8;
			// 
			// bpPanelControl166
			// 
			this.bpPanelControl166.Controls.Add(this._txtNumStndItem04);
			this.bpPanelControl166.Controls.Add(this.lblStndItem9);
			this.bpPanelControl166.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl166.Name = "bpPanelControl166";
			this.bpPanelControl166.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl166.TabIndex = 2;
			this.bpPanelControl166.Text = "bpPanelControl166";
			// 
			// _txtNumStndItem04
			// 
			this._txtNumStndItem04.BackColor = System.Drawing.Color.White;
			this._txtNumStndItem04.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtNumStndItem04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtNumStndItem04.CurrencyDecimalDigits = 4;
			this._txtNumStndItem04.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._txtNumStndItem04.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtNumStndItem04.ForeColor = System.Drawing.SystemColors.ControlText;
			this._txtNumStndItem04.Location = new System.Drawing.Point(157, 1);
			this._txtNumStndItem04.MaxLength = 3;
			this._txtNumStndItem04.Name = "_txtNumStndItem04";
			this._txtNumStndItem04.NullString = "0";
			this._txtNumStndItem04.PositiveColor = System.Drawing.Color.Black;
			this._txtNumStndItem04.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtNumStndItem04.Size = new System.Drawing.Size(185, 21);
			this._txtNumStndItem04.TabIndex = 40;
			this._txtNumStndItem04.Tag = "NUM_STND_ITEM_4";
			this._txtNumStndItem04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtNumStndItem04.Visible = false;
			// 
			// lblStndItem9
			// 
			this.lblStndItem9.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem9.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem9.Name = "lblStndItem9";
			this.lblStndItem9.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem9.TabIndex = 38;
			this.lblStndItem9.Tag = "CD_PURGRP";
			this.lblStndItem9.Text = "규격9";
			this.lblStndItem9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem9.Visible = false;
			// 
			// oneGridItem94
			// 
			this.oneGridItem94.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem94.Controls.Add(this.bpPanelControl167);
			this.oneGridItem94.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem94.Location = new System.Drawing.Point(0, 207);
			this.oneGridItem94.Name = "oneGridItem94";
			this.oneGridItem94.Size = new System.Drawing.Size(756, 23);
			this.oneGridItem94.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem94.TabIndex = 9;
			// 
			// bpPanelControl167
			// 
			this.bpPanelControl167.Controls.Add(this._txtNumStndItem05);
			this.bpPanelControl167.Controls.Add(this.lblStndItem10);
			this.bpPanelControl167.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl167.Name = "bpPanelControl167";
			this.bpPanelControl167.Size = new System.Drawing.Size(750, 23);
			this.bpPanelControl167.TabIndex = 2;
			this.bpPanelControl167.Text = "bpPanelControl167";
			// 
			// _txtNumStndItem05
			// 
			this._txtNumStndItem05.BackColor = System.Drawing.Color.White;
			this._txtNumStndItem05.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtNumStndItem05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtNumStndItem05.CurrencyDecimalDigits = 4;
			this._txtNumStndItem05.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this._txtNumStndItem05.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtNumStndItem05.ForeColor = System.Drawing.SystemColors.ControlText;
			this._txtNumStndItem05.Location = new System.Drawing.Point(157, 1);
			this._txtNumStndItem05.MaxLength = 3;
			this._txtNumStndItem05.Name = "_txtNumStndItem05";
			this._txtNumStndItem05.NullString = "0";
			this._txtNumStndItem05.PositiveColor = System.Drawing.Color.Black;
			this._txtNumStndItem05.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._txtNumStndItem05.Size = new System.Drawing.Size(185, 21);
			this._txtNumStndItem05.TabIndex = 40;
			this._txtNumStndItem05.Tag = "NUM_STND_ITEM_5";
			this._txtNumStndItem05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._txtNumStndItem05.Visible = false;
			// 
			// lblStndItem10
			// 
			this.lblStndItem10.BackColor = System.Drawing.Color.Transparent;
			this.lblStndItem10.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStndItem10.Location = new System.Drawing.Point(0, 3);
			this.lblStndItem10.Name = "lblStndItem10";
			this.lblStndItem10.Size = new System.Drawing.Size(156, 16);
			this.lblStndItem10.TabIndex = 38;
			this.lblStndItem10.Tag = "CD_PURGRP";
			this.lblStndItem10.Text = "규격10";
			this.lblStndItem10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblStndItem10.Visible = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1647, 820);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// oneGrid2
			// 
			this.oneGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid2.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem81,
            this.oneGridItem82,
            this.oneGridItem83,
            this.oneGridItem84,
            this.oneGridItem95,
            this.oneGridItem96});
			this.oneGrid2.Location = new System.Drawing.Point(3, 823);
			this.oneGrid2.Name = "oneGrid2";
			this.oneGrid2.Size = new System.Drawing.Size(1641, 1);
			this.oneGrid2.TabIndex = 191;
			// 
			// oneGridItem81
			// 
			this.oneGridItem81.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem81.Controls.Add(this.bpPanelControl147);
			this.oneGridItem81.Controls.Add(this.bpPanelControl146);
			this.oneGridItem81.Controls.Add(this.bpPanelControl145);
			this.oneGridItem81.Controls.Add(this.bpPanelControl144);
			this.oneGridItem81.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem81.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem81.Name = "oneGridItem81";
			this.oneGridItem81.Size = new System.Drawing.Size(1614, 23);
			this.oneGridItem81.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem81.TabIndex = 0;
			// 
			// bpPanelControl147
			// 
			this.bpPanelControl147.Controls.Add(this._lblUserDef09_S);
			this.bpPanelControl147.Controls.Add(this._cboUserDef09_S);
			this.bpPanelControl147.Location = new System.Drawing.Point(1034, 1);
			this.bpPanelControl147.Name = "bpPanelControl147";
			this.bpPanelControl147.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl147.TabIndex = 3;
			this.bpPanelControl147.Text = "bpPanelControl147";
			// 
			// _lblUserDef09_S
			// 
			this._lblUserDef09_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef09_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef09_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef09_S.Name = "_lblUserDef09_S";
			this._lblUserDef09_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef09_S.TabIndex = 6;
			this._lblUserDef09_S.Tag = "CLS_ITEM";
			this._lblUserDef09_S.Text = "사용자정의09";
			this._lblUserDef09_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef09_S
			// 
			this._cboUserDef09_S.AutoDropDown = true;
			this._cboUserDef09_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef09_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef09_S.ItemHeight = 12;
			this._cboUserDef09_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef09_S.MaxLength = 3;
			this._cboUserDef09_S.Name = "_cboUserDef09_S";
			this._cboUserDef09_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef09_S.TabIndex = 7;
			this._cboUserDef09_S.Tag = "CLS_ITEM";
			// 
			// bpPanelControl146
			// 
			this.bpPanelControl146.Controls.Add(this._lblUserDef08_S);
			this.bpPanelControl146.Controls.Add(this._cboUserDef08_S);
			this.bpPanelControl146.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl146.Name = "bpPanelControl146";
			this.bpPanelControl146.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl146.TabIndex = 2;
			this.bpPanelControl146.Text = "bpPanelControl146";
			// 
			// _lblUserDef08_S
			// 
			this._lblUserDef08_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef08_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef08_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef08_S.Name = "_lblUserDef08_S";
			this._lblUserDef08_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef08_S.TabIndex = 6;
			this._lblUserDef08_S.Tag = "CLS_ITEM";
			this._lblUserDef08_S.Text = "사용자정의08";
			this._lblUserDef08_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef08_S
			// 
			this._cboUserDef08_S.AutoDropDown = true;
			this._cboUserDef08_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef08_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef08_S.ItemHeight = 12;
			this._cboUserDef08_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef08_S.MaxLength = 3;
			this._cboUserDef08_S.Name = "_cboUserDef08_S";
			this._cboUserDef08_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef08_S.TabIndex = 7;
			this._cboUserDef08_S.Tag = "CLS_ITEM";
			// 
			// bpPanelControl145
			// 
			this.bpPanelControl145.Controls.Add(this._lblUserDef07_S);
			this.bpPanelControl145.Controls.Add(this._cboUserDef07_S);
			this.bpPanelControl145.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl145.Name = "bpPanelControl145";
			this.bpPanelControl145.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl145.TabIndex = 1;
			this.bpPanelControl145.Text = "bpPanelControl145";
			// 
			// _lblUserDef07_S
			// 
			this._lblUserDef07_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef07_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef07_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef07_S.Name = "_lblUserDef07_S";
			this._lblUserDef07_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef07_S.TabIndex = 6;
			this._lblUserDef07_S.Tag = "CLS_ITEM";
			this._lblUserDef07_S.Text = "사용자정의07";
			this._lblUserDef07_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef07_S
			// 
			this._cboUserDef07_S.AutoDropDown = true;
			this._cboUserDef07_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef07_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef07_S.ItemHeight = 12;
			this._cboUserDef07_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef07_S.MaxLength = 3;
			this._cboUserDef07_S.Name = "_cboUserDef07_S";
			this._cboUserDef07_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef07_S.TabIndex = 7;
			this._cboUserDef07_S.Tag = "CLS_ITEM";
			// 
			// bpPanelControl144
			// 
			this.bpPanelControl144.Controls.Add(this._lblUserDef06_S);
			this.bpPanelControl144.Controls.Add(this._cboUserDef06_S);
			this.bpPanelControl144.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl144.Name = "bpPanelControl144";
			this.bpPanelControl144.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl144.TabIndex = 0;
			this.bpPanelControl144.Text = "bpPanelControl144";
			// 
			// _lblUserDef06_S
			// 
			this._lblUserDef06_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef06_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef06_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef06_S.Name = "_lblUserDef06_S";
			this._lblUserDef06_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef06_S.TabIndex = 6;
			this._lblUserDef06_S.Tag = "CLS_ITEM";
			this._lblUserDef06_S.Text = "사용자정의06";
			this._lblUserDef06_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef06_S
			// 
			this._cboUserDef06_S.AutoDropDown = true;
			this._cboUserDef06_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef06_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef06_S.ItemHeight = 12;
			this._cboUserDef06_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef06_S.MaxLength = 3;
			this._cboUserDef06_S.Name = "_cboUserDef06_S";
			this._cboUserDef06_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef06_S.TabIndex = 7;
			this._cboUserDef06_S.Tag = "CLS_ITEM";
			// 
			// oneGridItem82
			// 
			this.oneGridItem82.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem82.Controls.Add(this.bpPanelControl151);
			this.oneGridItem82.Controls.Add(this.bpPanelControl150);
			this.oneGridItem82.Controls.Add(this.bpPanelControl149);
			this.oneGridItem82.Controls.Add(this.bpPanelControl148);
			this.oneGridItem82.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem82.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem82.Name = "oneGridItem82";
			this.oneGridItem82.Size = new System.Drawing.Size(1614, 23);
			this.oneGridItem82.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem82.TabIndex = 1;
			// 
			// bpPanelControl151
			// 
			this.bpPanelControl151.Controls.Add(this._lblUserDef04_S);
			this.bpPanelControl151.Controls.Add(this._txtUserDef04_S);
			this.bpPanelControl151.Location = new System.Drawing.Point(1034, 1);
			this.bpPanelControl151.Name = "bpPanelControl151";
			this.bpPanelControl151.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl151.TabIndex = 4;
			this.bpPanelControl151.Text = "bpPanelControl151";
			// 
			// _lblUserDef04_S
			// 
			this._lblUserDef04_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef04_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef04_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef04_S.Name = "_lblUserDef04_S";
			this._lblUserDef04_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef04_S.TabIndex = 7;
			this._lblUserDef04_S.Tag = "SEARCH";
			this._lblUserDef04_S.Text = "사용자정의04";
			this._lblUserDef04_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _txtUserDef04_S
			// 
			this._txtUserDef04_S.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtUserDef04_S.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtUserDef04_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtUserDef04_S.Location = new System.Drawing.Point(157, 1);
			this._txtUserDef04_S.MaxLength = 20;
			this._txtUserDef04_S.Name = "_txtUserDef04_S";
			this._txtUserDef04_S.Size = new System.Drawing.Size(185, 21);
			this._txtUserDef04_S.TabIndex = 8;
			this._txtUserDef04_S.Tag = "CD_ITEM";
			// 
			// bpPanelControl150
			// 
			this.bpPanelControl150.Controls.Add(this._lblUserDef03_S);
			this.bpPanelControl150.Controls.Add(this._txtUserDef03_S);
			this.bpPanelControl150.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl150.Name = "bpPanelControl150";
			this.bpPanelControl150.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl150.TabIndex = 3;
			this.bpPanelControl150.Text = "bpPanelControl150";
			// 
			// _lblUserDef03_S
			// 
			this._lblUserDef03_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef03_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef03_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef03_S.Name = "_lblUserDef03_S";
			this._lblUserDef03_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef03_S.TabIndex = 7;
			this._lblUserDef03_S.Tag = "SEARCH";
			this._lblUserDef03_S.Text = "사용자정의03";
			this._lblUserDef03_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _txtUserDef03_S
			// 
			this._txtUserDef03_S.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtUserDef03_S.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtUserDef03_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtUserDef03_S.Location = new System.Drawing.Point(157, 1);
			this._txtUserDef03_S.MaxLength = 20;
			this._txtUserDef03_S.Name = "_txtUserDef03_S";
			this._txtUserDef03_S.Size = new System.Drawing.Size(185, 21);
			this._txtUserDef03_S.TabIndex = 8;
			this._txtUserDef03_S.Tag = "CD_ITEM";
			// 
			// bpPanelControl149
			// 
			this.bpPanelControl149.Controls.Add(this._lblUserDef02_S);
			this.bpPanelControl149.Controls.Add(this._txtUserDef02_S);
			this.bpPanelControl149.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl149.Name = "bpPanelControl149";
			this.bpPanelControl149.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl149.TabIndex = 2;
			this.bpPanelControl149.Text = "bpPanelControl149";
			// 
			// _lblUserDef02_S
			// 
			this._lblUserDef02_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef02_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef02_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef02_S.Name = "_lblUserDef02_S";
			this._lblUserDef02_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef02_S.TabIndex = 5;
			this._lblUserDef02_S.Tag = "SEARCH";
			this._lblUserDef02_S.Text = "사용자정의02";
			this._lblUserDef02_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _txtUserDef02_S
			// 
			this._txtUserDef02_S.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtUserDef02_S.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtUserDef02_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtUserDef02_S.Location = new System.Drawing.Point(157, 1);
			this._txtUserDef02_S.MaxLength = 20;
			this._txtUserDef02_S.Name = "_txtUserDef02_S";
			this._txtUserDef02_S.Size = new System.Drawing.Size(185, 21);
			this._txtUserDef02_S.TabIndex = 6;
			this._txtUserDef02_S.Tag = "CD_ITEM";
			// 
			// bpPanelControl148
			// 
			this.bpPanelControl148.Controls.Add(this._lblUserDef01_S);
			this.bpPanelControl148.Controls.Add(this._txtUserDef01_S);
			this.bpPanelControl148.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl148.Name = "bpPanelControl148";
			this.bpPanelControl148.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl148.TabIndex = 1;
			this.bpPanelControl148.Text = "bpPanelControl148";
			// 
			// _lblUserDef01_S
			// 
			this._lblUserDef01_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef01_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef01_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef01_S.Name = "_lblUserDef01_S";
			this._lblUserDef01_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef01_S.TabIndex = 5;
			this._lblUserDef01_S.Tag = "SEARCH";
			this._lblUserDef01_S.Text = "사용자정의01";
			this._lblUserDef01_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _txtUserDef01_S
			// 
			this._txtUserDef01_S.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtUserDef01_S.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtUserDef01_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._txtUserDef01_S.Location = new System.Drawing.Point(157, 1);
			this._txtUserDef01_S.MaxLength = 20;
			this._txtUserDef01_S.Name = "_txtUserDef01_S";
			this._txtUserDef01_S.Size = new System.Drawing.Size(185, 21);
			this._txtUserDef01_S.TabIndex = 6;
			this._txtUserDef01_S.Tag = "CD_ITEM";
			// 
			// oneGridItem83
			// 
			this.oneGridItem83.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem83.Controls.Add(this.bpPanelControl155);
			this.oneGridItem83.Controls.Add(this.bpPanelControl154);
			this.oneGridItem83.Controls.Add(this.bpPanelControl153);
			this.oneGridItem83.Controls.Add(this.bpPanelControl152);
			this.oneGridItem83.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem83.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem83.Name = "oneGridItem83";
			this.oneGridItem83.Size = new System.Drawing.Size(1614, 23);
			this.oneGridItem83.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem83.TabIndex = 2;
			// 
			// bpPanelControl155
			// 
			this.bpPanelControl155.Location = new System.Drawing.Point(1034, 1);
			this.bpPanelControl155.Name = "bpPanelControl155";
			this.bpPanelControl155.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl155.TabIndex = 3;
			this.bpPanelControl155.Text = "bpPanelControl155";
			// 
			// bpPanelControl154
			// 
			this.bpPanelControl154.Controls.Add(this._lblUserDef32_S);
			this.bpPanelControl154.Controls.Add(this._ctxUserDef32_S);
			this.bpPanelControl154.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl154.Name = "bpPanelControl154";
			this.bpPanelControl154.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl154.TabIndex = 2;
			this.bpPanelControl154.Text = "bpPanelControl154";
			// 
			// _lblUserDef32_S
			// 
			this._lblUserDef32_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef32_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef32_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef32_S.Name = "_lblUserDef32_S";
			this._lblUserDef32_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef32_S.TabIndex = 17;
			this._lblUserDef32_S.Text = "사용자정의32";
			this._lblUserDef32_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _ctxUserDef32_S
			// 
			this._ctxUserDef32_S.CodeName = null;
			this._ctxUserDef32_S.CodeValue = null;
			this._ctxUserDef32_S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this._ctxUserDef32_S.Location = new System.Drawing.Point(157, 1);
			this._ctxUserDef32_S.Name = "_ctxUserDef32_S";
			this._ctxUserDef32_S.Size = new System.Drawing.Size(185, 21);
			this._ctxUserDef32_S.TabIndex = 16;
			this._ctxUserDef32_S.TabStop = false;
			// 
			// bpPanelControl153
			// 
			this.bpPanelControl153.Controls.Add(this._lblUserDef31_S);
			this.bpPanelControl153.Controls.Add(this._ctxUserDef31_S);
			this.bpPanelControl153.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl153.Name = "bpPanelControl153";
			this.bpPanelControl153.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl153.TabIndex = 1;
			this.bpPanelControl153.Text = "bpPanelControl153";
			// 
			// _lblUserDef31_S
			// 
			this._lblUserDef31_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef31_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef31_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef31_S.Name = "_lblUserDef31_S";
			this._lblUserDef31_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef31_S.TabIndex = 17;
			this._lblUserDef31_S.Text = "사용자정의31";
			this._lblUserDef31_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _ctxUserDef31_S
			// 
			this._ctxUserDef31_S.CodeName = null;
			this._ctxUserDef31_S.CodeValue = null;
			this._ctxUserDef31_S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this._ctxUserDef31_S.Location = new System.Drawing.Point(157, 1);
			this._ctxUserDef31_S.Name = "_ctxUserDef31_S";
			this._ctxUserDef31_S.Size = new System.Drawing.Size(185, 21);
			this._ctxUserDef31_S.TabIndex = 16;
			this._ctxUserDef31_S.TabStop = false;
			// 
			// bpPanelControl152
			// 
			this.bpPanelControl152.Controls.Add(this._lblUserDef30_S);
			this.bpPanelControl152.Controls.Add(this._ctxUserDef30_S);
			this.bpPanelControl152.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl152.Name = "bpPanelControl152";
			this.bpPanelControl152.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl152.TabIndex = 0;
			this.bpPanelControl152.Text = "bpPanelControl152";
			// 
			// _lblUserDef30_S
			// 
			this._lblUserDef30_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef30_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef30_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef30_S.Name = "_lblUserDef30_S";
			this._lblUserDef30_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef30_S.TabIndex = 17;
			this._lblUserDef30_S.Text = "사용자정의30";
			this._lblUserDef30_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _ctxUserDef30_S
			// 
			this._ctxUserDef30_S.CodeName = null;
			this._ctxUserDef30_S.CodeValue = null;
			this._ctxUserDef30_S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this._ctxUserDef30_S.Location = new System.Drawing.Point(157, 1);
			this._ctxUserDef30_S.Name = "_ctxUserDef30_S";
			this._ctxUserDef30_S.Size = new System.Drawing.Size(185, 21);
			this._ctxUserDef30_S.TabIndex = 16;
			this._ctxUserDef30_S.TabStop = false;
			// 
			// oneGridItem84
			// 
			this.oneGridItem84.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem84.Controls.Add(this.bpPanelControl158);
			this.oneGridItem84.Controls.Add(this.bpPanelControl157);
			this.oneGridItem84.Controls.Add(this.bpPanelControl156);
			this.oneGridItem84.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem84.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem84.Name = "oneGridItem84";
			this.oneGridItem84.Size = new System.Drawing.Size(1614, 23);
			this.oneGridItem84.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem84.TabIndex = 3;
			// 
			// bpPanelControl158
			// 
			this.bpPanelControl158.Controls.Add(this._lblUserDef35_S);
			this.bpPanelControl158.Controls.Add(this._cboUserDef35_S);
			this.bpPanelControl158.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl158.Name = "bpPanelControl158";
			this.bpPanelControl158.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl158.TabIndex = 3;
			this.bpPanelControl158.Text = "bpPanelControl158";
			// 
			// _lblUserDef35_S
			// 
			this._lblUserDef35_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef35_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef35_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef35_S.Name = "_lblUserDef35_S";
			this._lblUserDef35_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef35_S.TabIndex = 6;
			this._lblUserDef35_S.Tag = "CLS_ITEM";
			this._lblUserDef35_S.Text = "사용자정의35";
			this._lblUserDef35_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef35_S
			// 
			this._cboUserDef35_S.AutoDropDown = true;
			this._cboUserDef35_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef35_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef35_S.ItemHeight = 12;
			this._cboUserDef35_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef35_S.MaxLength = 3;
			this._cboUserDef35_S.Name = "_cboUserDef35_S";
			this._cboUserDef35_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef35_S.TabIndex = 7;
			this._cboUserDef35_S.Tag = "CLS_ITEM";
			// 
			// bpPanelControl157
			// 
			this.bpPanelControl157.Controls.Add(this._lblUserDef34_S);
			this.bpPanelControl157.Controls.Add(this._cboUserDef34_S);
			this.bpPanelControl157.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl157.Name = "bpPanelControl157";
			this.bpPanelControl157.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl157.TabIndex = 2;
			this.bpPanelControl157.Text = "bpPanelControl157";
			// 
			// _lblUserDef34_S
			// 
			this._lblUserDef34_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef34_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef34_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef34_S.Name = "_lblUserDef34_S";
			this._lblUserDef34_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef34_S.TabIndex = 6;
			this._lblUserDef34_S.Tag = "CLS_ITEM";
			this._lblUserDef34_S.Text = "사용자정의34";
			this._lblUserDef34_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef34_S
			// 
			this._cboUserDef34_S.AutoDropDown = true;
			this._cboUserDef34_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef34_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef34_S.ItemHeight = 12;
			this._cboUserDef34_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef34_S.MaxLength = 3;
			this._cboUserDef34_S.Name = "_cboUserDef34_S";
			this._cboUserDef34_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef34_S.TabIndex = 7;
			this._cboUserDef34_S.Tag = "CLS_ITEM";
			// 
			// bpPanelControl156
			// 
			this.bpPanelControl156.Controls.Add(this._lblUserDef33_S);
			this.bpPanelControl156.Controls.Add(this._cboUserDef33_S);
			this.bpPanelControl156.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl156.Name = "bpPanelControl156";
			this.bpPanelControl156.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl156.TabIndex = 1;
			this.bpPanelControl156.Text = "bpPanelControl156";
			// 
			// _lblUserDef33_S
			// 
			this._lblUserDef33_S.BackColor = System.Drawing.Color.Transparent;
			this._lblUserDef33_S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblUserDef33_S.Location = new System.Drawing.Point(0, 3);
			this._lblUserDef33_S.Name = "_lblUserDef33_S";
			this._lblUserDef33_S.Size = new System.Drawing.Size(156, 16);
			this._lblUserDef33_S.TabIndex = 6;
			this._lblUserDef33_S.Tag = "CLS_ITEM";
			this._lblUserDef33_S.Text = "사용자정의33";
			this._lblUserDef33_S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _cboUserDef33_S
			// 
			this._cboUserDef33_S.AutoDropDown = true;
			this._cboUserDef33_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboUserDef33_S.Font = new System.Drawing.Font("굴림체", 9F);
			this._cboUserDef33_S.ItemHeight = 12;
			this._cboUserDef33_S.Location = new System.Drawing.Point(157, 1);
			this._cboUserDef33_S.MaxLength = 3;
			this._cboUserDef33_S.Name = "_cboUserDef33_S";
			this._cboUserDef33_S.Size = new System.Drawing.Size(185, 20);
			this._cboUserDef33_S.TabIndex = 7;
			this._cboUserDef33_S.Tag = "CLS_ITEM";
			// 
			// oneGridItem95
			// 
			this.oneGridItem95.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem95.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem95.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem95.Name = "oneGridItem95";
			this.oneGridItem95.Size = new System.Drawing.Size(1614, 23);
			this.oneGridItem95.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem95.TabIndex = 4;
			// 
			// oneGridItem96
			// 
			this.oneGridItem96.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem96.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem96.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem96.Name = "oneGridItem96";
			this.oneGridItem96.Size = new System.Drawing.Size(1614, 23);
			this.oneGridItem96.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem96.TabIndex = 5;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem97,
            this.oneGridItem100});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1641, 154);
			this.oneGrid1.TabIndex = 6;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl7);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1631, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl조달구분S);
			this.bpPanelControl2.Controls.Add(this.cbo조달구분S);
			this.bpPanelControl2.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl조달구분S
			// 
			this.lbl조달구분S.BackColor = System.Drawing.Color.Transparent;
			this.lbl조달구분S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl조달구분S.Location = new System.Drawing.Point(0, 3);
			this.lbl조달구분S.Name = "lbl조달구분S";
			this.lbl조달구분S.Size = new System.Drawing.Size(156, 16);
			this.lbl조달구분S.TabIndex = 0;
			this.lbl조달구분S.Tag = "TP_PROC";
			this.lbl조달구분S.Text = "조달구분";
			this.lbl조달구분S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo조달구분S
			// 
			this.cbo조달구분S.AutoDropDown = true;
			this.cbo조달구분S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo조달구분S.ItemHeight = 12;
			this.cbo조달구분S.Location = new System.Drawing.Point(157, 1);
			this.cbo조달구분S.Name = "cbo조달구분S";
			this.cbo조달구분S.Size = new System.Drawing.Size(185, 20);
			this.cbo조달구분S.TabIndex = 1;
			this.cbo조달구분S.Tag = "TP_PROC";
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.lbl계정구분S);
			this.bpPanelControl7.Controls.Add(this.cbo계정구분S);
			this.bpPanelControl7.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl7.TabIndex = 5;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// lbl계정구분S
			// 
			this.lbl계정구분S.BackColor = System.Drawing.Color.Transparent;
			this.lbl계정구분S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl계정구분S.Location = new System.Drawing.Point(0, 3);
			this.lbl계정구분S.Name = "lbl계정구분S";
			this.lbl계정구분S.Size = new System.Drawing.Size(156, 16);
			this.lbl계정구분S.TabIndex = 1;
			this.lbl계정구분S.Tag = "CLS_ITEM";
			this.lbl계정구분S.Text = "계정구분";
			this.lbl계정구분S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo계정구분S
			// 
			this.cbo계정구분S.AutoDropDown = true;
			this.cbo계정구분S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo계정구분S.Font = new System.Drawing.Font("굴림체", 9F);
			this.cbo계정구분S.ItemHeight = 12;
			this.cbo계정구분S.Location = new System.Drawing.Point(157, 1);
			this.cbo계정구분S.MaxLength = 3;
			this.cbo계정구분S.Name = "cbo계정구분S";
			this.cbo계정구분S.Size = new System.Drawing.Size(185, 20);
			this.cbo계정구분S.TabIndex = 5;
			this.cbo계정구분S.Tag = "CLS_ITEM";
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl공장코드S);
			this.bpPanelControl1.Controls.Add(this.cbo공장코드S);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lbl공장코드S
			// 
			this.lbl공장코드S.BackColor = System.Drawing.Color.Transparent;
			this.lbl공장코드S.Location = new System.Drawing.Point(0, 3);
			this.lbl공장코드S.Name = "lbl공장코드S";
			this.lbl공장코드S.Size = new System.Drawing.Size(156, 16);
			this.lbl공장코드S.TabIndex = 14;
			this.lbl공장코드S.Tag = "CD_PLANT";
			this.lbl공장코드S.Text = "공장코드";
			this.lbl공장코드S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공장코드S
			// 
			this.cbo공장코드S.AutoDropDown = true;
			this.cbo공장코드S.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo공장코드S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장코드S.ItemHeight = 12;
			this.cbo공장코드S.Location = new System.Drawing.Point(157, 1);
			this.cbo공장코드S.Name = "cbo공장코드S";
			this.cbo공장코드S.Size = new System.Drawing.Size(185, 20);
			this.cbo공장코드S.TabIndex = 0;
			this.cbo공장코드S.Tag = "CD_PLANT";
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl9);
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1631, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl9
			// 
			this.bpPanelControl9.Controls.Add(this.lbl품목타입);
			this.bpPanelControl9.Controls.Add(this.bpc품목타입S);
			this.bpPanelControl9.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl9.Name = "bpPanelControl9";
			this.bpPanelControl9.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl9.TabIndex = 7;
			this.bpPanelControl9.Text = "bpPanelControl9";
			// 
			// lbl품목타입
			// 
			this.lbl품목타입.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목타입.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목타입.Location = new System.Drawing.Point(0, 3);
			this.lbl품목타입.Name = "lbl품목타입";
			this.lbl품목타입.Size = new System.Drawing.Size(156, 16);
			this.lbl품목타입.TabIndex = 155;
			this.lbl품목타입.Tag = "TP_PART";
			this.lbl품목타입.Text = "품목타입";
			this.lbl품목타입.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc품목타입S
			// 
			this.bpc품목타입S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpc품목타입S.Location = new System.Drawing.Point(157, 1);
			this.bpc품목타입S.Name = "bpc품목타입S";
			this.bpc품목타입S.Size = new System.Drawing.Size(185, 21);
			this.bpc품목타입S.TabIndex = 11;
			this.bpc품목타입S.TabStop = false;
			this.bpc품목타입S.Text = "bpComboBox1";
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.lbl제품군S);
			this.bpPanelControl6.Controls.Add(this.cbo제품군S);
			this.bpPanelControl6.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl6.TabIndex = 6;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// lbl제품군S
			// 
			this.lbl제품군S.BackColor = System.Drawing.Color.Transparent;
			this.lbl제품군S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl제품군S.Location = new System.Drawing.Point(0, 3);
			this.lbl제품군S.Name = "lbl제품군S";
			this.lbl제품군S.Size = new System.Drawing.Size(156, 16);
			this.lbl제품군S.TabIndex = 1;
			this.lbl제품군S.Tag = "GRP_MFG";
			this.lbl제품군S.Text = "제품군";
			this.lbl제품군S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo제품군S
			// 
			this.cbo제품군S.AutoDropDown = true;
			this.cbo제품군S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo제품군S.FormattingEnabled = true;
			this.cbo제품군S.ItemHeight = 12;
			this.cbo제품군S.Location = new System.Drawing.Point(157, 1);
			this.cbo제품군S.Name = "cbo제품군S";
			this.cbo제품군S.Size = new System.Drawing.Size(185, 20);
			this.cbo제품군S.TabIndex = 6;
			this.cbo제품군S.Tag = "GRP_MFG";
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.bpc품목군S);
			this.bpPanelControl3.Controls.Add(this.lbl품목군S);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// bpc품목군S
			// 
			this.bpc품목군S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEMGP_SUB1;
			this.bpc품목군S.Location = new System.Drawing.Point(157, 1);
			this.bpc품목군S.Name = "bpc품목군S";
			this.bpc품목군S.Size = new System.Drawing.Size(185, 21);
			this.bpc품목군S.TabIndex = 12;
			this.bpc품목군S.TabStop = false;
			this.bpc품목군S.Text = "bpComboBox1";
			// 
			// lbl품목군S
			// 
			this.lbl품목군S.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목군S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목군S.Location = new System.Drawing.Point(0, 3);
			this.lbl품목군S.Name = "lbl품목군S";
			this.lbl품목군S.Size = new System.Drawing.Size(156, 16);
			this.lbl품목군S.TabIndex = 0;
			this.lbl품목군S.Tag = "GRP_ITEM";
			this.lbl품목군S.Text = "품목군";
			this.lbl품목군S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl11);
			this.oneGridItem3.Controls.Add(this.bpPanelControl10);
			this.oneGridItem3.Controls.Add(this.bpPanelControl12);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1631, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.bpcClssS);
			this.bpPanelControl11.Controls.Add(this.lbl소분류S);
			this.bpPanelControl11.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl11.TabIndex = 6;
			this.bpPanelControl11.Text = "bpPanelControl11";
			// 
			// bpcClssS
			// 
			this.bpcClssS.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpcClssS.Location = new System.Drawing.Point(157, 1);
			this.bpcClssS.Name = "bpcClssS";
			this.bpcClssS.Size = new System.Drawing.Size(185, 21);
			this.bpcClssS.TabIndex = 18;
			this.bpcClssS.TabStop = false;
			this.bpcClssS.Text = "bpComboBox1";
			// 
			// lbl소분류S
			// 
			this.lbl소분류S.BackColor = System.Drawing.Color.Transparent;
			this.lbl소분류S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl소분류S.Location = new System.Drawing.Point(0, 3);
			this.lbl소분류S.Name = "lbl소분류S";
			this.lbl소분류S.Size = new System.Drawing.Size(156, 16);
			this.lbl소분류S.TabIndex = 16;
			this.lbl소분류S.Text = "소분류";
			this.lbl소분류S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl10
			// 
			this.bpPanelControl10.Controls.Add(this.bpcClsmS);
			this.bpPanelControl10.Controls.Add(this.lbl중분류S);
			this.bpPanelControl10.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl10.Name = "bpPanelControl10";
			this.bpPanelControl10.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl10.TabIndex = 5;
			this.bpPanelControl10.Text = "bpPanelControl10";
			// 
			// bpcClsmS
			// 
			this.bpcClsmS.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpcClsmS.Location = new System.Drawing.Point(157, 1);
			this.bpcClsmS.Name = "bpcClsmS";
			this.bpcClsmS.Size = new System.Drawing.Size(185, 21);
			this.bpcClsmS.TabIndex = 17;
			this.bpcClsmS.TabStop = false;
			this.bpcClsmS.Text = "bpComboBox1";
			// 
			// lbl중분류S
			// 
			this.lbl중분류S.BackColor = System.Drawing.Color.Transparent;
			this.lbl중분류S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl중분류S.Location = new System.Drawing.Point(0, 3);
			this.lbl중분류S.Name = "lbl중분류S";
			this.lbl중분류S.Size = new System.Drawing.Size(156, 16);
			this.lbl중분류S.TabIndex = 16;
			this.lbl중분류S.Text = "중분류";
			this.lbl중분류S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.bpcClslS);
			this.bpPanelControl12.Controls.Add(this.lbl대분류S);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl12.TabIndex = 4;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// bpcClslS
			// 
			this.bpcClslS.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpcClslS.Location = new System.Drawing.Point(157, 1);
			this.bpcClslS.Name = "bpcClslS";
			this.bpcClslS.Size = new System.Drawing.Size(185, 21);
			this.bpcClslS.TabIndex = 16;
			this.bpcClslS.TabStop = false;
			this.bpcClslS.Text = "bpComboBox1";
			// 
			// lbl대분류S
			// 
			this.lbl대분류S.BackColor = System.Drawing.Color.Transparent;
			this.lbl대분류S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl대분류S.Location = new System.Drawing.Point(0, 3);
			this.lbl대분류S.Name = "lbl대분류S";
			this.lbl대분류S.Size = new System.Drawing.Size(156, 16);
			this.lbl대분류S.TabIndex = 15;
			this.lbl대분류S.Text = "대분류";
			this.lbl대분류S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.bpPanelControl5);
			this.oneGridItem4.Controls.Add(this.bpPanelControl4);
			this.oneGridItem4.Controls.Add(this.bpPanelControl13);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1631, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 3;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.lbl내외자구분S);
			this.bpPanelControl5.Controls.Add(this.cbo내외자구분S);
			this.bpPanelControl5.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl5.TabIndex = 7;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// lbl내외자구분S
			// 
			this.lbl내외자구분S.BackColor = System.Drawing.Color.Transparent;
			this.lbl내외자구분S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl내외자구분S.Location = new System.Drawing.Point(0, 3);
			this.lbl내외자구분S.Name = "lbl내외자구분S";
			this.lbl내외자구분S.Size = new System.Drawing.Size(156, 16);
			this.lbl내외자구분S.TabIndex = 154;
			this.lbl내외자구분S.Tag = "TP_PART";
			this.lbl내외자구분S.Text = "내외자구분";
			this.lbl내외자구분S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo내외자구분S
			// 
			this.cbo내외자구분S.AutoDropDown = true;
			this.cbo내외자구분S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo내외자구분S.FormattingEnabled = true;
			this.cbo내외자구분S.ItemHeight = 12;
			this.cbo내외자구분S.Location = new System.Drawing.Point(157, 1);
			this.cbo내외자구분S.Name = "cbo내외자구분S";
			this.cbo내외자구분S.Size = new System.Drawing.Size(185, 20);
			this.cbo내외자구분S.TabIndex = 7;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.lbl사용유무S);
			this.bpPanelControl4.Controls.Add(this.cbo사용유무S);
			this.bpPanelControl4.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// lbl사용유무S
			// 
			this.lbl사용유무S.BackColor = System.Drawing.Color.Transparent;
			this.lbl사용유무S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl사용유무S.Location = new System.Drawing.Point(0, 3);
			this.lbl사용유무S.Name = "lbl사용유무S";
			this.lbl사용유무S.Size = new System.Drawing.Size(156, 16);
			this.lbl사용유무S.TabIndex = 0;
			this.lbl사용유무S.Tag = "YN_USE";
			this.lbl사용유무S.Text = "사용유무";
			this.lbl사용유무S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo사용유무S
			// 
			this.cbo사용유무S.AutoDropDown = true;
			this.cbo사용유무S.BackColor = System.Drawing.Color.White;
			this.cbo사용유무S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용유무S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cbo사용유무S.ItemHeight = 12;
			this.cbo사용유무S.Location = new System.Drawing.Point(157, 1);
			this.cbo사용유무S.MaxLength = 3;
			this.cbo사용유무S.Name = "cbo사용유무S";
			this.cbo사용유무S.Size = new System.Drawing.Size(185, 20);
			this.cbo사용유무S.TabIndex = 3;
			this.cbo사용유무S.Tag = "YN_USE";
			// 
			// bpPanelControl13
			// 
			this.bpPanelControl13.Controls.Add(this.ctx주거래처S);
			this.bpPanelControl13.Controls.Add(this.lbl주거래처);
			this.bpPanelControl13.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl13.Name = "bpPanelControl13";
			this.bpPanelControl13.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl13.TabIndex = 0;
			this.bpPanelControl13.Text = "bpPanelControl13";
			// 
			// ctx주거래처S
			// 
			this.ctx주거래처S.CodeName = null;
			this.ctx주거래처S.CodeValue = null;
			this.ctx주거래처S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx주거래처S.Location = new System.Drawing.Point(157, 1);
			this.ctx주거래처S.Name = "ctx주거래처S";
			this.ctx주거래처S.Size = new System.Drawing.Size(185, 21);
			this.ctx주거래처S.TabIndex = 12;
			this.ctx주거래처S.TabStop = false;
			this.ctx주거래처S.Tag = "PARTNER,LN_PARTNER";
			// 
			// lbl주거래처
			// 
			this.lbl주거래처.BackColor = System.Drawing.Color.Transparent;
			this.lbl주거래처.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl주거래처.Location = new System.Drawing.Point(0, 3);
			this.lbl주거래처.Name = "lbl주거래처";
			this.lbl주거래처.Size = new System.Drawing.Size(156, 16);
			this.lbl주거래처.TabIndex = 16;
			this.lbl주거래처.Text = "주거래처";
			this.lbl주거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem97
			// 
			this.oneGridItem97.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem97.Controls.Add(this.bpPanelControl175);
			this.oneGridItem97.Controls.Add(this.bpPanelControl8);
			this.oneGridItem97.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem97.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem97.Name = "oneGridItem97";
			this.oneGridItem97.Size = new System.Drawing.Size(1631, 23);
			this.oneGridItem97.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem97.TabIndex = 4;
			// 
			// bpPanelControl175
			// 
			this.bpPanelControl175.Controls.Add(this._chkTop);
			this.bpPanelControl175.Controls.Add(this._chkUpper);
			this.bpPanelControl175.Controls.Add(this._chkSString);
			this.bpPanelControl175.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl175.Name = "bpPanelControl175";
			this.bpPanelControl175.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl175.TabIndex = 8;
			this.bpPanelControl175.Text = "bpPanelControl175";
			// 
			// _chkTop
			// 
			this._chkTop.Location = new System.Drawing.Point(119, -1);
			this._chkTop.Name = "_chkTop";
			this._chkTop.Size = new System.Drawing.Size(104, 24);
			this._chkTop.TabIndex = 7;
			this._chkTop.Text = "상위검색";
			this._chkTop.TextDD = null;
			this._chkTop.UseVisualStyleBackColor = true;
			this._chkTop.Visible = false;
			// 
			// _chkUpper
			// 
			this._chkUpper.Location = new System.Drawing.Point(2, 0);
			this._chkUpper.Name = "_chkUpper";
			this._chkUpper.Size = new System.Drawing.Size(104, 24);
			this._chkUpper.TabIndex = 6;
			this._chkUpper.Text = "대/소문자구분";
			this._chkUpper.TextDD = null;
			this._chkUpper.UseVisualStyleBackColor = true;
			this._chkUpper.Visible = false;
			// 
			// _chkSString
			// 
			this._chkSString.Location = new System.Drawing.Point(225, 0);
			this._chkSString.Name = "_chkSString";
			this._chkSString.Size = new System.Drawing.Size(115, 24);
			this._chkSString.TabIndex = 5;
			this._chkSString.Text = "시작문자열검색";
			this._chkSString.TextDD = null;
			this._chkSString.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.lbl검색S);
			this.bpPanelControl8.Controls.Add(this.txt검색S);
			this.bpPanelControl8.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(686, 23);
			this.bpPanelControl8.TabIndex = 4;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// lbl검색S
			// 
			this.lbl검색S.BackColor = System.Drawing.Color.Transparent;
			this.lbl검색S.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl검색S.Location = new System.Drawing.Point(0, 3);
			this.lbl검색S.Name = "lbl검색S";
			this.lbl검색S.Size = new System.Drawing.Size(156, 16);
			this.lbl검색S.TabIndex = 2;
			this.lbl검색S.Tag = "SEARCH";
			this.lbl검색S.Text = "검색";
			this.lbl검색S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt검색S
			// 
			this.txt검색S.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt검색S.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt검색S.Font = new System.Drawing.Font("굴림체", 9F);
			this.txt검색S.Location = new System.Drawing.Point(157, 1);
			this.txt검색S.MaxLength = 100;
			this.txt검색S.Name = "txt검색S";
			this.txt검색S.Size = new System.Drawing.Size(529, 21);
			this.txt검색S.TabIndex = 4;
			this.txt검색S.Tag = "CD_ITEM";
			// 
			// oneGridItem100
			// 
			this.oneGridItem100.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem100.Controls.Add(this.bpPanelControl168);
			this.oneGridItem100.Controls.Add(this.bpPanelControl170);
			this.oneGridItem100.Controls.Add(this.bpPanelControl169);
			this.oneGridItem100.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem100.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem100.Name = "oneGridItem100";
			this.oneGridItem100.Size = new System.Drawing.Size(1631, 23);
			this.oneGridItem100.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem100.TabIndex = 5;
			// 
			// bpPanelControl168
			// 
			this.bpPanelControl168.Controls.Add(this.txt검색2);
			this.bpPanelControl168.Controls.Add(this.cboSearch2);
			this.bpPanelControl168.Location = new System.Drawing.Point(690, 1);
			this.bpPanelControl168.Name = "bpPanelControl168";
			this.bpPanelControl168.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl168.TabIndex = 4;
			this.bpPanelControl168.Text = "bpPanelControl168";
			// 
			// txt검색2
			// 
			this.txt검색2.Location = new System.Drawing.Point(157, 1);
			this.txt검색2.MaxLength = 100;
			this.txt검색2.Name = "txt검색2";
			this.txt검색2.Size = new System.Drawing.Size(185, 21);
			this.txt검색2.TabIndex = 33;
			// 
			// cboSearch2
			// 
			this.cboSearch2.AutoDropDown = true;
			this.cboSearch2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSearch2.ImeMode = System.Windows.Forms.ImeMode.Alpha;
			this.cboSearch2.ItemHeight = 12;
			this.cboSearch2.Location = new System.Drawing.Point(0, 1);
			this.cboSearch2.MaxLength = 15;
			this.cboSearch2.Name = "cboSearch2";
			this.cboSearch2.Size = new System.Drawing.Size(156, 20);
			this.cboSearch2.TabIndex = 35;
			this.cboSearch2.Tag = "GRP_MFG";
			// 
			// bpPanelControl170
			// 
			this.bpPanelControl170.Controls.Add(this.txt검색3);
			this.bpPanelControl170.Controls.Add(this.cboSearch3);
			this.bpPanelControl170.Location = new System.Drawing.Point(346, 1);
			this.bpPanelControl170.Name = "bpPanelControl170";
			this.bpPanelControl170.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl170.TabIndex = 5;
			this.bpPanelControl170.Text = "bpPanelControl170";
			// 
			// txt검색3
			// 
			this.txt검색3.Location = new System.Drawing.Point(157, 1);
			this.txt검색3.MaxLength = 100;
			this.txt검색3.Name = "txt검색3";
			this.txt검색3.Size = new System.Drawing.Size(185, 21);
			this.txt검색3.TabIndex = 33;
			// 
			// cboSearch3
			// 
			this.cboSearch3.AutoDropDown = true;
			this.cboSearch3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSearch3.ImeMode = System.Windows.Forms.ImeMode.Alpha;
			this.cboSearch3.ItemHeight = 12;
			this.cboSearch3.Location = new System.Drawing.Point(0, 1);
			this.cboSearch3.MaxLength = 15;
			this.cboSearch3.Name = "cboSearch3";
			this.cboSearch3.Size = new System.Drawing.Size(156, 20);
			this.cboSearch3.TabIndex = 35;
			this.cboSearch3.Tag = "GRP_MFG";
			// 
			// bpPanelControl169
			// 
			this.bpPanelControl169.Controls.Add(this.txt검색1);
			this.bpPanelControl169.Controls.Add(this.cboSearch1);
			this.bpPanelControl169.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl169.Name = "bpPanelControl169";
			this.bpPanelControl169.Size = new System.Drawing.Size(342, 23);
			this.bpPanelControl169.TabIndex = 3;
			this.bpPanelControl169.Text = "bpPanelControl169";
			// 
			// txt검색1
			// 
			this.txt검색1.Location = new System.Drawing.Point(157, 1);
			this.txt검색1.MaxLength = 100;
			this.txt검색1.Name = "txt검색1";
			this.txt검색1.Size = new System.Drawing.Size(185, 21);
			this.txt검색1.TabIndex = 9;
			// 
			// cboSearch1
			// 
			this.cboSearch1.AutoDropDown = true;
			this.cboSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSearch1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
			this.cboSearch1.ItemHeight = 12;
			this.cboSearch1.Location = new System.Drawing.Point(0, 1);
			this.cboSearch1.MaxLength = 15;
			this.cboSearch1.Name = "cboSearch1";
			this.cboSearch1.Size = new System.Drawing.Size(156, 20);
			this.cboSearch1.TabIndex = 32;
			this.cboSearch1.Tag = "GRP_MFG";
			// 
			// btn엑셀업로드
			// 
			this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn엑셀업로드.BackColor = System.Drawing.Color.White;
			this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드.Location = new System.Drawing.Point(478, 3);
			this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드.Name = "btn엑셀업로드";
			this.btn엑셀업로드.Size = new System.Drawing.Size(100, 19);
			this.btn엑셀업로드.TabIndex = 235;
			this.btn엑셀업로드.TabStop = false;
			this.btn엑셀업로드.Tag = "ITEM_COPY";
			this.btn엑셀업로드.Text = "Excel Upload";
			this.btn엑셀업로드.UseVisualStyleBackColor = false;
			// 
			// btn붙여넣기
			// 
			this.btn붙여넣기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn붙여넣기.BackColor = System.Drawing.Color.White;
			this.btn붙여넣기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn붙여넣기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn붙여넣기.Location = new System.Drawing.Point(654, 3);
			this.btn붙여넣기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn붙여넣기.Name = "btn붙여넣기";
			this.btn붙여넣기.Size = new System.Drawing.Size(64, 19);
			this.btn붙여넣기.TabIndex = 234;
			this.btn붙여넣기.TabStop = false;
			this.btn붙여넣기.Tag = "ITEM_PASTE";
			this.btn붙여넣기.Text = "붙여넣기";
			this.btn붙여넣기.UseVisualStyleBackColor = false;
			// 
			// btn품목복사
			// 
			this.btn품목복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn품목복사.BackColor = System.Drawing.Color.White;
			this.btn품목복사.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn품목복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn품목복사.Location = new System.Drawing.Point(584, 3);
			this.btn품목복사.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn품목복사.Name = "btn품목복사";
			this.btn품목복사.Size = new System.Drawing.Size(64, 19);
			this.btn품목복사.TabIndex = 233;
			this.btn품목복사.TabStop = false;
			this.btn품목복사.Tag = "ITEM_COPY";
			this.btn품목복사.Text = "품목복사";
			this.btn품목복사.UseVisualStyleBackColor = false;
			// 
			// btn첨부파일
			// 
			this.btn첨부파일.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn첨부파일.BackColor = System.Drawing.Color.White;
			this.btn첨부파일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn첨부파일.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn첨부파일.Location = new System.Drawing.Point(408, 3);
			this.btn첨부파일.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn첨부파일.Name = "btn첨부파일";
			this.btn첨부파일.Size = new System.Drawing.Size(64, 19);
			this.btn첨부파일.TabIndex = 236;
			this.btn첨부파일.TabStop = false;
			this.btn첨부파일.Text = "첨부파일";
			this.btn첨부파일.UseVisualStyleBackColor = false;
			// 
			// btnPDM적용
			// 
			this.btnPDM적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPDM적용.BackColor = System.Drawing.Color.White;
			this.btnPDM적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnPDM적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPDM적용.Location = new System.Drawing.Point(324, 3);
			this.btnPDM적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnPDM적용.Name = "btnPDM적용";
			this.btnPDM적용.Size = new System.Drawing.Size(78, 19);
			this.btnPDM적용.TabIndex = 237;
			this.btnPDM적용.TabStop = false;
			this.btnPDM적용.Text = "PDM적용";
			this.btnPDM적용.UseVisualStyleBackColor = false;
			this.btnPDM적용.Visible = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn붙여넣기);
			this.flowLayoutPanel1.Controls.Add(this.btn품목복사);
			this.flowLayoutPanel1.Controls.Add(this.btn엑셀업로드);
			this.flowLayoutPanel1.Controls.Add(this.btn첨부파일);
			this.flowLayoutPanel1.Controls.Add(this.btnPDM적용);
			this.flowLayoutPanel1.Controls.Add(this.btnCopyToCompany);
			this.flowLayoutPanel1.Controls.Add(this._btnBulkSave);
			this.flowLayoutPanel1.Controls.Add(this._btnZSATREC_PASS);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(923, 10);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(721, 24);
			this.flowLayoutPanel1.TabIndex = 238;
			// 
			// btnCopyToCompany
			// 
			this.btnCopyToCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopyToCompany.BackColor = System.Drawing.Color.White;
			this.btnCopyToCompany.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCopyToCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCopyToCompany.Location = new System.Drawing.Point(206, 3);
			this.btnCopyToCompany.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnCopyToCompany.Name = "btnCopyToCompany";
			this.btnCopyToCompany.Size = new System.Drawing.Size(112, 19);
			this.btnCopyToCompany.TabIndex = 238;
			this.btnCopyToCompany.TabStop = false;
			this.btnCopyToCompany.Text = "회사간품목전송";
			this.btnCopyToCompany.UseVisualStyleBackColor = false;
			this.btnCopyToCompany.Visible = false;
			// 
			// _btnBulkSave
			// 
			this._btnBulkSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnBulkSave.BackColor = System.Drawing.Color.White;
			this._btnBulkSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnBulkSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnBulkSave.Location = new System.Drawing.Point(111, 3);
			this._btnBulkSave.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnBulkSave.Name = "_btnBulkSave";
			this._btnBulkSave.Size = new System.Drawing.Size(89, 19);
			this._btnBulkSave.TabIndex = 239;
			this._btnBulkSave.TabStop = false;
			this._btnBulkSave.Text = "대용량 저장";
			this._btnBulkSave.UseVisualStyleBackColor = true;
			this._btnBulkSave.Visible = false;
			// 
			// _btnZSATREC_PASS
			// 
			this._btnZSATREC_PASS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnZSATREC_PASS.BackColor = System.Drawing.Color.White;
			this._btnZSATREC_PASS.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnZSATREC_PASS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnZSATREC_PASS.Location = new System.Drawing.Point(16, 3);
			this._btnZSATREC_PASS.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnZSATREC_PASS.Name = "_btnZSATREC_PASS";
			this._btnZSATREC_PASS.Size = new System.Drawing.Size(89, 19);
			this._btnZSATREC_PASS.TabIndex = 240;
			this._btnZSATREC_PASS.TabStop = false;
			this._btnZSATREC_PASS.Text = "품목등록요청";
			this._btnZSATREC_PASS.UseVisualStyleBackColor = true;
			this._btnZSATREC_PASS.Visible = false;
			// 
			// P_CZ_MA_PITEM_WINTEC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "P_CZ_MA_PITEM_WINTEC";
			this.Size = new System.Drawing.Size(1647, 860);
			this.TitleText = "공장품목등록";
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.tab.ResumeLayout(false);
			this.tpg기본.ResumeLayout(false);
			this.oneGridItem5.ResumeLayout(false);
			this.bpPanelControl14.ResumeLayout(false);
			this.bpPanelControl14.PerformLayout();
			this.oneGridItem6.ResumeLayout(false);
			this.bpPanelControl15.ResumeLayout(false);
			this.oneGridItem7.ResumeLayout(false);
			this.bpPanelControl16.ResumeLayout(false);
			this.bpPanelControl16.PerformLayout();
			this.oneGridItem8.ResumeLayout(false);
			this.bpPanelControl17.ResumeLayout(false);
			this.bpPanelControl17.PerformLayout();
			this.oneGridItem9.ResumeLayout(false);
			this.bpPanelControl18.ResumeLayout(false);
			this.bpPanelControl18.PerformLayout();
			this.oneGridItem10.ResumeLayout(false);
			this.bpPanelControl19.ResumeLayout(false);
			this.bpPanelControl19.PerformLayout();
			this.oneGridItem11.ResumeLayout(false);
			this.bpPanelControl20.ResumeLayout(false);
			this.bpPanelControl20.PerformLayout();
			this.oneGridItem12.ResumeLayout(false);
			this.bpPanelControl21.ResumeLayout(false);
			this.bpPanelControl23.ResumeLayout(false);
			this.oneGridItem13.ResumeLayout(false);
			this.bpPanelControl22.ResumeLayout(false);
			this.bpPanelControl22.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur출하단위수량)).EndInit();
			this.bpPanelControl25.ResumeLayout(false);
			this.oneGridItem14.ResumeLayout(false);
			this.bpPanelControl24.ResumeLayout(false);
			this.bpPanelControl24.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur수주단위수량)).EndInit();
			this.bpPanelControl27.ResumeLayout(false);
			this.bpPanelControl27.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur구매단위수량)).EndInit();
			this.oneGridItem99.ResumeLayout(false);
			this.bpPanelControl26.ResumeLayout(false);
			this.bpPanelControl26.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._curUnitMoFact)).EndInit();
			this.bpPanelControl173.ResumeLayout(false);
			this.bpPanelControl173.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur외주단위수량)).EndInit();
			this.oneGridItem15.ResumeLayout(false);
			this.bpPanelControl28.ResumeLayout(false);
			this.bpPanelControl29.ResumeLayout(false);
			this.oneGridItem16.ResumeLayout(false);
			this.bpPanelControl30.ResumeLayout(false);
			this.bpPanelControl31.ResumeLayout(false);
			this.oneGridItem17.ResumeLayout(false);
			this.bpPanelControl32.ResumeLayout(false);
			this.bpPanelControl33.ResumeLayout(false);
			this.bpPanelControl33.PerformLayout();
			this.oneGridItem18.ResumeLayout(false);
			this.bpPanelControl34.ResumeLayout(false);
			this.bpPanelControl34.PerformLayout();
			this.bpPanelControl35.ResumeLayout(false);
			this.bpPanelControl35.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur중량)).EndInit();
			this.oneGridItem19.ResumeLayout(false);
			this.bpPanelControl48.ResumeLayout(false);
			this.bpPanelControl48.PerformLayout();
			this.oneGridItem20.ResumeLayout(false);
			this.bpPanelControl49.ResumeLayout(false);
			this.bpPanelControl49.PerformLayout();
			this.oneGridItem21.ResumeLayout(false);
			this.bpPanelControl36.ResumeLayout(false);
			this.bpPanelControl37.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp유효일)).EndInit();
			this.oneGridItem22.ResumeLayout(false);
			this.bpPanelControl38.ResumeLayout(false);
			this.bpPanelControl39.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp등록일)).EndInit();
			this.oneGridItem23.ResumeLayout(false);
			this.bpPanelControl40.ResumeLayout(false);
			this.bpPanelControl41.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp수정일)).EndInit();
			this.oneGridItem24.ResumeLayout(false);
			this.bpPanelControl42.ResumeLayout(false);
			this.bpPanelControl42.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur길이)).EndInit();
			this.bpPanelControl43.ResumeLayout(false);
			this.bpPanelControl43.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur폭)).EndInit();
			this.oneGridItem25.ResumeLayout(false);
			this.bpPanelControl44.ResumeLayout(false);
			this.bpPanelControl45.ResumeLayout(false);
			this.oneGridItem26.ResumeLayout(false);
			this.bpPanelControl46.ResumeLayout(false);
			this.bpPanelControl47.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp시작일)).EndInit();
			this.oneGridItem101.ResumeLayout(false);
			this.bpPanelControl174.ResumeLayout(false);
			this.bpPanelControl174.PerformLayout();
			this.tpg자재.ResumeLayout(false);
			this.oneGridItem27.ResumeLayout(false);
			this.bpPanelControl50.ResumeLayout(false);
			this.oneGridItem28.ResumeLayout(false);
			this.bpPanelControl53.ResumeLayout(false);
			this.bpPanelControl53.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur안전재고)).EndInit();
			this.oneGridItem29.ResumeLayout(false);
			this.bpPanelControl54.ResumeLayout(false);
			this.bpPanelControl54.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur허용일)).EndInit();
			this.bpPanelControl55.ResumeLayout(false);
			this.bpPanelControl55.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur안전LT)).EndInit();
			this.oneGridItem30.ResumeLayout(false);
			this.bpPanelControl57.ResumeLayout(false);
			this.oneGridItem31.ResumeLayout(false);
			this.bpPanelControl58.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp최종재고실사일)).EndInit();
			this.bpPanelControl59.ResumeLayout(false);
			this.oneGridItem32.ResumeLayout(false);
			this.bpPanelControl60.ResumeLayout(false);
			this.bpPanelControl60.PerformLayout();
			this.bpPanelControl61.ResumeLayout(false);
			this.oneGridItem33.ResumeLayout(false);
			this.bpPanelControl62.ResumeLayout(false);
			this.bpPanelControl62.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur표준단위원가)).EndInit();
			this.bpPanelControl63.ResumeLayout(false);
			this.bpPanelControl63.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur순환실사주기)).EndInit();
			this.oneGridItem34.ResumeLayout(false);
			this.bpPanelControl64.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp표준원가적용일)).EndInit();
			this.bpPanelControl65.ResumeLayout(false);
			this.oneGridItem35.ResumeLayout(false);
			this.bpPanelControl66.ResumeLayout(false);
			this.bpPanelControl67.ResumeLayout(false);
			this.bpPanelControl67.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur유효기간)).EndInit();
			this.oneGridItem36.ResumeLayout(false);
			this.bpPanelControl68.ResumeLayout(false);
			this.bpPanelControl69.ResumeLayout(false);
			this.bpPanelControl69.PerformLayout();
			this.oneGridItem37.ResumeLayout(false);
			this.bpPanelControl71.ResumeLayout(false);
			this.oneGridItem38.ResumeLayout(false);
			this.bpPanelControl77.ResumeLayout(false);
			this.oneGridItem39.ResumeLayout(false);
			this.bpPanelControl75.ResumeLayout(false);
			this.oneGridItem40.ResumeLayout(false);
			this.bpPanelControl72.ResumeLayout(false);
			this.bpPanelControl73.ResumeLayout(false);
			this.oneGridItem103.ResumeLayout(false);
			this.bpPanelControl178.ResumeLayout(false);
			this.tpg오더.ResumeLayout(false);
			this.oneGridItem41.ResumeLayout(false);
			this.bpPanelControl74.ResumeLayout(false);
			this.bpPanelControl70.ResumeLayout(false);
			this.oneGridItem42.ResumeLayout(false);
			this.bpPanelControl76.ResumeLayout(false);
			this.bpPanelControl76.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur로트사이즈)).EndInit();
			this.bpPanelControl78.ResumeLayout(false);
			this.oneGridItem43.ResumeLayout(false);
			this.bpPanelControl79.ResumeLayout(false);
			this.bpPanelControl80.ResumeLayout(false);
			this.oneGridItem44.ResumeLayout(false);
			this.bpPanelControl82.ResumeLayout(false);
			this.bpPanelControl82.PerformLayout();
			this.oneGridItem45.ResumeLayout(false);
			this.bpPanelControl83.ResumeLayout(false);
			this.bpPanelControl84.ResumeLayout(false);
			this.bpPanelControl84.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur품목LT)).EndInit();
			this.oneGridItem46.ResumeLayout(false);
			this.bpPanelControl85.ResumeLayout(false);
			this.bpPanelControl86.ResumeLayout(false);
			this.oneGridItem47.ResumeLayout(false);
			this.bpPanelControl87.ResumeLayout(false);
			this.bpPanelControl87.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.curPOQ기간)).EndInit();
			this.bpPanelControl88.ResumeLayout(false);
			this.oneGridItem48.ResumeLayout(false);
			this.bpPanelControl89.ResumeLayout(false);
			this.bpPanelControl89.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur최대수배수)).EndInit();
			this.bpPanelControl90.ResumeLayout(false);
			this.bpPanelControl90.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur최소수배수)).EndInit();
			this.oneGridItem49.ResumeLayout(false);
			this.bpPanelControl91.ResumeLayout(false);
			this.bpPanelControl92.ResumeLayout(false);
			this.bpPanelControl92.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.curFAQ수량)).EndInit();
			this.oneGridItem50.ResumeLayout(false);
			this.bpPanelControl93.ResumeLayout(false);
			this.bpPanelControl93.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur표준ST)).EndInit();
			this.bpPanelControl94.ResumeLayout(false);
			this.oneGridItem51.ResumeLayout(false);
			this.bpPanelControl95.ResumeLayout(false);
			this.bpPanelControl95.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboROP)).EndInit();
			this.bpPanelControl96.ResumeLayout(false);
			this.oneGridItem52.ResumeLayout(false);
			this.bpPanelControl97.ResumeLayout(false);
			this.bpPanelControl97.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur생산능력)).EndInit();
			this.bpPanelControl98.ResumeLayout(false);
			this.bpPanelControl98.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.curUPH)).EndInit();
			this.oneGridItem53.ResumeLayout(false);
			this.bpPanelControl99.ResumeLayout(false);
			this.bpPanelControl99.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur과입고허용율음수)).EndInit();
			this.bpPanelControl81.ResumeLayout(false);
			this.bpPanelControl81.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur과입고허용율양수)).EndInit();
			this.oneGridItem54.ResumeLayout(false);
			this.bpPanelControl101.ResumeLayout(false);
			this.bpPanelControl101.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.curNEGO금액)).EndInit();
			this.tpg품질.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.pnl첨부파일.ResumeLayout(false);
			this.pnl첨부파일.PerformLayout();
			this.oneGridItem55.ResumeLayout(false);
			this.bpPanelControl103.ResumeLayout(false);
			this.bpPanelControl103.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur품목불량율)).EndInit();
			this.bpPanelControl102.ResumeLayout(false);
			this.bpPanelControl102.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur검사LT)).EndInit();
			this.oneGridItem56.ResumeLayout(false);
			this.bpPanelControl104.ResumeLayout(false);
			this.oneGridItem57.ResumeLayout(false);
			this.bpPanelControl105.ResumeLayout(false);
			this.oneGridItem58.ResumeLayout(false);
			this.bpPanelControl106.ResumeLayout(false);
			this.oneGridItem59.ResumeLayout(false);
			this.bpPanelControl107.ResumeLayout(false);
			this.oneGridItem60.ResumeLayout(false);
			this.bpPanelControl108.ResumeLayout(false);
			this.bpPanelControl108.PerformLayout();
			this.tpg기타.ResumeLayout(false);
			this.oneGridItem61.ResumeLayout(false);
			this.bpPanelControl109.ResumeLayout(false);
			this.bpPanelControl109.PerformLayout();
			this.oneGridItem62.ResumeLayout(false);
			this.bpPanelControl110.ResumeLayout(false);
			this.bpPanelControl110.PerformLayout();
			this.oneGridItem63.ResumeLayout(false);
			this.bpPanelControl111.ResumeLayout(false);
			this.bpPanelControl111.PerformLayout();
			this.oneGridItem64.ResumeLayout(false);
			this.bpPanelControl112.ResumeLayout(false);
			this.bpPanelControl112.PerformLayout();
			this.oneGridItem65.ResumeLayout(false);
			this.bpPanelControl113.ResumeLayout(false);
			this.bpPanelControl113.PerformLayout();
			this.oneGridItem66.ResumeLayout(false);
			this.bpPanelControl115.ResumeLayout(false);
			this.bpPanelControl114.ResumeLayout(false);
			this.oneGridItem67.ResumeLayout(false);
			this.bpPanelControl116.ResumeLayout(false);
			this.bpPanelControl117.ResumeLayout(false);
			this.oneGridItem68.ResumeLayout(false);
			this.bpPanelControl118.ResumeLayout(false);
			this.bpPanelControl119.ResumeLayout(false);
			this.oneGridItem69.ResumeLayout(false);
			this.bpPanelControl120.ResumeLayout(false);
			this.bpPanelControl121.ResumeLayout(false);
			this.oneGridItem70.ResumeLayout(false);
			this.bpPanelControl122.ResumeLayout(false);
			this.bpPanelControl123.ResumeLayout(false);
			this.oneGridItem71.ResumeLayout(false);
			this.bpPanelControl124.ResumeLayout(false);
			this.bpPanelControl124.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의17)).EndInit();
			this.bpPanelControl125.ResumeLayout(false);
			this.bpPanelControl125.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의16)).EndInit();
			this.oneGridItem72.ResumeLayout(false);
			this.bpPanelControl126.ResumeLayout(false);
			this.bpPanelControl126.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의19)).EndInit();
			this.bpPanelControl127.ResumeLayout(false);
			this.bpPanelControl127.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의18)).EndInit();
			this.oneGridItem73.ResumeLayout(false);
			this.bpPanelControl128.ResumeLayout(false);
			this.bpPanelControl128.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의21)).EndInit();
			this.bpPanelControl129.ResumeLayout(false);
			this.bpPanelControl129.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의20)).EndInit();
			this.oneGridItem74.ResumeLayout(false);
			this.bpPanelControl130.ResumeLayout(false);
			this.bpPanelControl130.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의23)).EndInit();
			this.bpPanelControl131.ResumeLayout(false);
			this.bpPanelControl131.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의22)).EndInit();
			this.oneGridItem75.ResumeLayout(false);
			this.bpPanelControl132.ResumeLayout(false);
			this.bpPanelControl132.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의25)).EndInit();
			this.bpPanelControl133.ResumeLayout(false);
			this.bpPanelControl133.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur사용자정의24)).EndInit();
			this.oneGridItem76.ResumeLayout(false);
			this.bpPanelControl134.ResumeLayout(false);
			this.bpPanelControl135.ResumeLayout(false);
			this.oneGridItem77.ResumeLayout(false);
			this.bpPanelControl136.ResumeLayout(false);
			this.bpPanelControl137.ResumeLayout(false);
			this.oneGridItem78.ResumeLayout(false);
			this.bpPanelControl138.ResumeLayout(false);
			this.bpPanelControl139.ResumeLayout(false);
			this.oneGridItem79.ResumeLayout(false);
			this.bpPanelControl140.ResumeLayout(false);
			this.bpPanelControl141.ResumeLayout(false);
			this.oneGridItem80.ResumeLayout(false);
			this.bpPanelControl142.ResumeLayout(false);
			this.bpPanelControl143.ResumeLayout(false);
			this.oneGridItem98.ResumeLayout(false);
			this.bpPanelControl172.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._datePicker37)).EndInit();
			this.bpPanelControl171.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._datePicker36)).EndInit();
			this.oneGridItem102.ResumeLayout(false);
			this.bpPanelControl177.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._datePicker39)).EndInit();
			this.bpPanelControl176.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._datePicker38)).EndInit();
			this.tpgIMAGE.ResumeLayout(false);
			this.panelExt43.ResumeLayout(false);
			this.panelExt44.ResumeLayout(false);
			this.tblImage.ResumeLayout(false);
			this.imagePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pixFile)).EndInit();
			this.pnlFileList.ResumeLayout(false);
			this.pnlFileList.PerformLayout();
			this.tag버전관리.ResumeLayout(false);
			this.panelExt70.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panelExt71.ResumeLayout(false);
			this.panelExt72.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panelExt73.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex버전관리)).EndInit();
			this.pnl버전관리.ResumeLayout(false);
			this.pnl버전관리.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp배포일자)).EndInit();
			this.tpgStndItem.ResumeLayout(false);
			this.oneGridItem85.ResumeLayout(false);
			this.bpPanelControl51.ResumeLayout(false);
			this.bpPanelControl51.PerformLayout();
			this.oneGridItem86.ResumeLayout(false);
			this.bpPanelControl159.ResumeLayout(false);
			this.bpPanelControl159.PerformLayout();
			this.oneGridItem87.ResumeLayout(false);
			this.bpPanelControl160.ResumeLayout(false);
			this.bpPanelControl160.PerformLayout();
			this.oneGridItem88.ResumeLayout(false);
			this.bpPanelControl161.ResumeLayout(false);
			this.bpPanelControl161.PerformLayout();
			this.oneGridItem89.ResumeLayout(false);
			this.bpPanelControl162.ResumeLayout(false);
			this.bpPanelControl162.PerformLayout();
			this.oneGridItem90.ResumeLayout(false);
			this.bpPanelControl163.ResumeLayout(false);
			this.bpPanelControl163.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem01)).EndInit();
			this.oneGridItem91.ResumeLayout(false);
			this.bpPanelControl164.ResumeLayout(false);
			this.bpPanelControl164.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem02)).EndInit();
			this.oneGridItem92.ResumeLayout(false);
			this.bpPanelControl165.ResumeLayout(false);
			this.bpPanelControl165.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem03)).EndInit();
			this.oneGridItem93.ResumeLayout(false);
			this.bpPanelControl166.ResumeLayout(false);
			this.bpPanelControl166.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem04)).EndInit();
			this.oneGridItem94.ResumeLayout(false);
			this.bpPanelControl167.ResumeLayout(false);
			this.bpPanelControl167.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._txtNumStndItem05)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem81.ResumeLayout(false);
			this.bpPanelControl147.ResumeLayout(false);
			this.bpPanelControl146.ResumeLayout(false);
			this.bpPanelControl145.ResumeLayout(false);
			this.bpPanelControl144.ResumeLayout(false);
			this.oneGridItem82.ResumeLayout(false);
			this.bpPanelControl151.ResumeLayout(false);
			this.bpPanelControl151.PerformLayout();
			this.bpPanelControl150.ResumeLayout(false);
			this.bpPanelControl150.PerformLayout();
			this.bpPanelControl149.ResumeLayout(false);
			this.bpPanelControl149.PerformLayout();
			this.bpPanelControl148.ResumeLayout(false);
			this.bpPanelControl148.PerformLayout();
			this.oneGridItem83.ResumeLayout(false);
			this.bpPanelControl154.ResumeLayout(false);
			this.bpPanelControl153.ResumeLayout(false);
			this.bpPanelControl152.ResumeLayout(false);
			this.oneGridItem84.ResumeLayout(false);
			this.bpPanelControl158.ResumeLayout(false);
			this.bpPanelControl157.ResumeLayout(false);
			this.bpPanelControl156.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl9.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl11.ResumeLayout(false);
			this.bpPanelControl10.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.oneGridItem4.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl13.ResumeLayout(false);
			this.oneGridItem97.ResumeLayout(false);
			this.bpPanelControl175.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl8.PerformLayout();
			this.oneGridItem100.ResumeLayout(false);
			this.bpPanelControl168.ResumeLayout(false);
			this.bpPanelControl168.PerformLayout();
			this.bpPanelControl170.ResumeLayout(false);
			this.bpPanelControl170.PerformLayout();
			this.bpPanelControl169.ResumeLayout(false);
			this.bpPanelControl169.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DropDownComboBox cbo내외자구분S;
        private LabelExt lbl내외자구분S;
        private LabelExt lbl사용유무S;
        private DropDownComboBox cbo사용유무S;
        private DropDownComboBox cbo제품군S;
        private LabelExt lbl소분류S;
        private LabelExt lbl제품군S;
        private LabelExt lbl품목군S;
        private DropDownComboBox cbo계정구분S;
        private TextBoxExt txt검색S;
        private LabelExt lbl중분류S;
        private LabelExt lbl계정구분S;
        private LabelExt lbl조달구분S;
        private LabelExt lbl대분류S;
        private LabelExt lbl공장코드S;
        private LabelExt lbl검색S;
        private DropDownComboBox cbo조달구분S;
        private DropDownComboBox cbo공장코드S;
        private TabControlExt tab;
        private TabPage tpg기본;
        private DropDownComboBox cbo도면유무;
        private LabelExt m_lblGrpMfg;
        private CurrencyTextBox cur출하단위수량;
        private DropDownComboBox cbo출하단위;
        private CurrencyTextBox cur수주단위수량;
        private CurrencyTextBox cur구매단위수량;
        private LabelExt labelExt1;
        private LabelExt m_lblYnUse;
        private BpCodeTextBox ctx품목군;
        private DatePicker dtp유효일;
        private CurrencyTextBox cur중량;
        private LabelExt m_lblBarCode;
        private LabelExt m_lblMatItem;
        private TextBoxExt txtHS코드;
        private DropDownComboBox cbo계정구분;
        private DropDownComboBox cbo재고단위;
        private LabelExt m_lblUnitIm;
        private LabelExt m_lblTpItem;
        private LabelExt m_lblDtValid;
        private DropDownComboBox cbo생산단위;
        private TextBoxExt txt품목url;
        private DropDownComboBox cbo품목타입;
        private LabelExt m_lblUrlItem;
        private DropDownComboBox cbo조달구분;
        private LabelExt m_lblTpProc;
        private TextBoxExt txt도면번호;
        private DropDownComboBox cbo내외자구분;
        private LabelExt m_lblWeight;
        private DropDownComboBox cbo중량단위;
        private DropDownComboBox cboHS단위;
        private DropDownComboBox cbo사용유무;
        private LabelExt m_lblUnitPo;
        private LabelExt m_lblNoHs;
        private DropDownComboBox cbo수주단위;
        private DropDownComboBox cbo구매단위;
        private TextBoxExt txt바코드;
        private LabelExt m_lblStndItem;
        private TextBoxExt txt품목코드;
        private LabelExt m_lblNmItem;
        private LabelExt m_lblEnItem;
        private TextBoxExt txt영문품명;
        private TextBoxExt txt재질;
        private TextBoxExt txt규격;
        private LabelExt m_lblCdItem;
        private LabelExt m_lblNoDesign;
        private LabelExt m_lblTpPart;
        private LabelExt m_lblClsItem;
        private LabelExt m_lblUnitMo;
        private LabelExt m_lblUnitSo;
        private LabelExt m_lblUnitHs;
        private LabelExt m_lblUnitWeight;
        private LabelExt labelExt2;
        private TabPage tpg자재;
        private LabelExt lbl과세구분매입;
        private DropDownComboBox cbo과세구분매출;
        private LabelExt lbl과세구분매출;
        private BpCodeTextBox ctx관리자2;
        private BpCodeTextBox ctx관리자1;
        private LabelExt lbl_관리자2;
        private DropDownComboBox cbo과세구분매입;
        private TextBoxExt txt로케이션;
        private LabelExt lbl로케이션;
        private BpCodeTextBox ctx출고SL;
        private BpCodeTextBox ctx입고SL;
        private BpCodeTextBox ctx구매그룹;
        private DatePicker dtp최종재고실사일;
        private CurrencyTextBox cur표준단위원가;
        private CurrencyTextBox cur유효기간;
        private CurrencyTextBox cur순환실사주기;
        private CurrencyTextBox cur안전LT;
        private LabelExt m_lblCdPurGrp;
        private CurrencyTextBox cur안전재고;
        private TextBoxExt txt모델코드;
        private LabelExt m_lblDyValid;
        private LabelExt m_lblQtSstock;
        private LabelExt m_lblDy1;
        private DropDownComboBox cboABC구분;
        private LabelExt lbl대분류;
        private LabelExt m_lblFgAbc;
        private LabelExt m_lblNoModel;
        private LabelExt m_lblCdSl;
        private LabelExt m_lblCdGiSl;
        private LabelExt m_lblFgGri;
        private DropDownComboBox cbo불출관리;
        private LabelExt m_lblSageLt;
        private LabelExt lbl_관리자1;
        private LabelExt lbl소분류;
        private LabelExt lbl중분류;
        private LabelExt m_lblDyImcly;
        private LabelExt lbl최종재고실사일;
        private LabelExt lbl표준단위원가;
        private TabPage tpg오더;
        private BpCodeTextBox ctx주거래처;
        private CurrencyTextBox cur과입고허용율음수;
        private CurrencyTextBox cur과입고허용율양수;
        private CurrencyTextBox cur생산능력;
        private CurrencyTextBox curUPH;
        private CurrencyTextBox cboROP;
        private CurrencyTextBox curFAQ수량;
        private CurrencyTextBox cur최대수배수;
        private CurrencyTextBox cur최소수배수;
        private CurrencyTextBox curPOQ기간;
        private CurrencyTextBox cur품목LT;
        private CurrencyTextBox cur로트사이즈;
        private DropDownComboBox cbo발주방침;
        private DropDownComboBox cbo트래킹;
        private LabelExt m_lblDay;
        private DropDownComboBox cboLOT관리;
        private DropDownComboBox cboBOM형태;
        private DropDownComboBox cbo제조전략;
        private DropDownComboBox cbo발주정책;
        private DropDownComboBox cbo장단납기구분;
        private DropDownComboBox cboFOQ오더정리;
        private DropDownComboBox cbo팬텀구분;
        private DropDownComboBox cbo백플러쉬;
        private LabelExt m_lblTpPo;
        private LabelExt m_lblFgTracking;
        private LabelExt m_lblPartner;
        private LabelExt m_lblLtItem;
        private LabelExt m_lblTpManu;
        private LabelExt m_lblClsPo;
        private LabelExt m_lblQtMin;
        private LabelExt m_lblQtFoq;
        private LabelExt m_lblYnPhanTom;
        private LabelExt m_lblFgBf;
        private LabelExt m_lblUph;
        private LabelExt m_lblRtPlus;
        private LabelExt m_lblFgSerNo;
        private LabelExt m_lblLotSize;
        private LabelExt m_lblDyPoq;
        private LabelExt m_lblTpBom;
        private LabelExt m_lblFgLong;
        private LabelExt m_lblQtMax;
        private LabelExt m_lblFgFoq;
        private LabelExt m_cboQtRop;
        private LabelExt m_lblUpd;
        private LabelExt m_lblRtMinus;
        private TabPage tpg품질;
        private RoundedButton btn파일삭제;
        private TextBoxExt txt첨부파일명;
        private RoundedButton btn다운로드;
        private RoundedButton btn업로드;
        private DropDownComboBox cbo수입검사여부;
        private DropDownComboBox cbo외주검사;
        private LabelExt labelExt5;
        private LabelExt labelExt4;
        private CurrencyTextBox cur품목불량율;
        private CurrencyTextBox cur검사LT;
        private TextBoxExt txt규격번호;
        private LabelExt m_lblFgMqc;
        private DropDownComboBox cbo출하검사여부;
        private LabelExt m_lblDay2;
        private DropDownComboBox cbo생산입고검사;
        private LabelExt m_lblFgPqc;
        private LabelExt m_lblNoStnd;
        private LabelExt m_lblLtQc;
        private LabelExt m_lblRtQm;
        private SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flex;
        private TableLayoutPanel tableLayoutPanel1;
        private ImageList img;
        private RoundedButton btn엑셀업로드;
        private RoundedButton btn붙여넣기;
        private RoundedButton btn품목복사;
        private TextBoxExt txt세부규격;
        private LabelExt labelExt6;
        private LabelExt labelExt7;
        private CurrencyTextBox cur허용일;
        private LabelExt labelExt9;
        private LabelExt labelExt8;
        private DropDownComboBox cboATP적용여부;
        private LabelExt lbl품목타입;
        private BpComboBox bpc품목타입S;
        private TextBoxExt txtMaker;
        private LabelExt lblMaker;
        private TabPage tpg기타;
        private DropDownComboBox cbo사용자정의6;
        private LabelExt lbl사용자정의1;
        private LabelExt lbl사용자정의12;
        private LabelExt lbl사용자정의2;
        private LabelExt lbl사용자정의16;
        private LabelExt lbl사용자정의4;
        private LabelExt lbl사용자정의14;
        private LabelExt lbl사용자정의5;
        private LabelExt lbl사용자정의6;
        private LabelExt lbl사용자정의10;
        private LabelExt lbl사용자정의3;
        private LabelExt lbl사용자정의20;
        private LabelExt lbl사용자정의18;
        private LabelExt lbl사용자정의8;
        private DropDownComboBox cbo사용자정의7;
        private TextBoxExt txt사용자정의5;
        private TextBoxExt txt사용자정의4;
        private TextBoxExt txt사용자정의3;
        private TextBoxExt txt사용자정의2;
        private TextBoxExt txt사용자정의1;
        private DropDownComboBox cbo사용자정의14;
        private DropDownComboBox cbo사용자정의12;
        private DropDownComboBox cbo사용자정의10;
        private DropDownComboBox cbo사용자정의8;
        private CurrencyTextBox cur사용자정의19;
        private CurrencyTextBox cur사용자정의17;
        private DropDownComboBox cbo사용자정의15;
        private DropDownComboBox cbo사용자정의13;
        private DropDownComboBox cbo사용자정의11;
        private DropDownComboBox cbo사용자정의9;
        private CurrencyTextBox cur사용자정의20;
        private CurrencyTextBox cur사용자정의18;
        private CurrencyTextBox cur사용자정의16;
        private LabelExt lbl사용자정의19;
        private LabelExt lbl사용자정의17;
        private LabelExt lbl사용자정의15;
        private LabelExt lbl사용자정의13;
        private LabelExt lbl사용자정의11;
        private LabelExt lbl사용자정의9;
        private LabelExt lbl사용자정의7;
        private LabelExt lbl표준ST;
        private CurrencyTextBox cur표준ST;
        private BpCodeTextBox ctx주거래처S;
        private LabelExt lbl주거래처;
        private DatePicker dtp표준원가적용일;
        private LabelExt labelExt10;
        private BpCodeNTextBox ctx대분류;
        private BpCodeNTextBox ctx소분류;
        private BpCodeNTextBox ctx중분류;
        private TabPage tpgIMAGE;
        private PanelExt panelExt43;
        private OpenFileDialog opendlg;
        private PanelExt panelExt44;
        private TableLayoutPanel tblImage;
        private FlexibleRoundedCornerBox pnlFileList;
        private RoundedButton rduFile6View;
        private RoundedButton rduFile5View;
        private RoundedButton rduFile4View;
        private RoundedButton rduFileIns6;
        private RoundedButton rduFileIns4;
        private RoundedButton rduFileIns5;
        private RoundedButton rduFileDel6;
        private RoundedButton rduFileDel4;
        private RoundedButton rduFileDel5;
        private TextBoxExt txtFile6;
        private TextBoxExt txtFile5;
        private TextBoxExt txtFile4;
        private RoundedButton rduFile3View;
        private RoundedButton rduFile2View;
        private RoundedButton rduFile1View;
        private RoundedButton rduFileIns3;
        private RoundedButton rduFileIns1;
        private RoundedButton rduFileIns2;
        private RoundedButton rduFileDel3;
        private RoundedButton rduFileDel1;
        private RoundedButton rduFileDel2;
        private TextBoxExt txtFile3;
        private TextBoxExt txtFile2;
        private TextBoxExt txtFile1;
        private TextBoxExt textBoxExt4;
        private TextBoxExt textBoxExt5;
        private TextBoxExt textBoxExt6;
        private TextBoxExt textBoxExt3;
        private TextBoxExt textBoxExt2;
        private TextBoxExt textBoxExt1;
        private DropDownComboBox cbo공정검사;
        private LabelExt lbl공정검사;
        private RoundedButton btn첨부파일;
        private DropDownComboBox cbo매입형태;
        private BpCodeTextBox bp코어코드;
        private CurrencyTextBox cur길이;
        private CurrencyTextBox cur폭;
        private LabelExt lbl매입형태;
        private LabelExt lbl길이;
        private LabelExt lbl폭;
        private LabelExt lbl코어코드;
        private BpCodeTextBox ctx제품군;
        private LabelExt lbl사용자정의24;
        private LabelExt lbl사용자정의22;
        private LabelExt lbl사용자정의25;
        private LabelExt lbl사용자정의23;
        private LabelExt lbl사용자정의21;
        private CurrencyTextBox cur사용자정의25;
        private CurrencyTextBox cur사용자정의23;
        private CurrencyTextBox cur사용자정의21;
        private CurrencyTextBox cur사용자정의24;
        private CurrencyTextBox cur사용자정의22;
        private LabelExt lbl사용자정의26;
        private DropDownComboBox cbo사용자정의27;
        private DropDownComboBox cbo사용자정의26;
        private LabelExt lbl사용자정의27;
        private DropDownComboBox cbo사용자정의29;
        private DropDownComboBox cbo사용자정의28;
        private LabelExt lbl사용자정의28;
        private LabelExt lbl사용자정의29;
        private BpCodeTextBox bp_사용자정의30;
        private BpCodeTextBox bp_사용자정의31;
        private LabelExt lbl사용자정의30;
        private LabelExt lbl사용자정의31;
        private LabelExt lbl사용자정의32;
        private BpCodeTextBox bp_사용자정의32;
        private LabelExt lbl이동검사;
        private DropDownComboBox cbo이동검사;
        private ImagePanel imagePanel1;
        private PictureBox pixFile;
        private TabPage tag버전관리;
        private PanelExt panelExt70;
        private TableLayoutPanel tableLayoutPanel2;
        private PanelExt panelExt71;
        private PanelExt panelExt72;
        private SplitContainer splitContainer2;
        private RoundedButton btn삭제;
        private RoundedButton btn추가;
        private PanelExt panelExt73;
        private Dass.FlexGrid.FlexGrid _flex버전관리;
        private FlexibleRoundedCornerBox pnl버전관리;
        private LabelExt lbl순번;
        private LabelExt lbl언어;
        private LabelExt lbl버전;
        private LabelExt lbl_버전관리_비고;
        private LabelExt lbl제품사항;
        private LabelExt lbl지원여부;
        private LabelExt lbl지원OS;
        private LabelExt lbl타입;
        private LabelExt lbl배포일자;
        private LabelExt lbl최종소스여부;
        private LabelExt lbl난이도;
        private LabelExt lbl담당자부;
        private LabelExt lbl담당자정;
        private LabelExt lbl개발자;
        private TextBoxExt txt순번;
        private TextBoxExt txt언어;
        private TextBoxExt txt버전;
        private TextBoxExt txt비고;
        private TextBoxExt txt제품사항;
        private DropDownComboBox cbo지원여부;
        private TextBoxExt txt지원OS;
        private TextBoxExt txt타입;
        private DatePicker dtp배포일자;
        private DropDownComboBox cbo최종소스여부;
        private DropDownComboBox cbo난이도;
        private BpCodeTextBox bp담당자부;
        private BpCodeTextBox bp담당자정;
        private BpCodeTextBox bp개발자;
        private DropDownComboBox cbo사용자정의35;
        private DropDownComboBox cbo사용자정의34;
        private DropDownComboBox cbo사용자정의33;
        private LabelExt lbl사용자정의33;
        private LabelExt lbl사용자정의34;
        private LabelExt lbl사용자정의35;
        private LabelExt lbl시작일;
        private DatePicker dtp시작일;
        private BpCodeTextBox ctxcc;
        private LabelExt lblcc코드;
        private DropDownComboBox cbo수입검사레벨;
        private LabelExt lbl수입검사레벨;
        private DropDownComboBox cbolotsn동시사용;
        private LabelExt lbllotsn동시사용;
        private RoundedButton btnPDM적용;
        private FlowLayoutPanel flowLayoutPanel1;
        private DatePicker dtp수정일;
        private BpCodeTextBox ctx수정자;
        private BpCodeTextBox ctx등록자;
        private DatePicker dtp등록일;
        private LabelExt lbl수정자;
        private LabelExt lbl등록자;
        private LabelExt lbl수정일;
        private LabelExt lbl등록일;
        private LabelExt labelExt11;
        private CurrencyTextBox curNEGO금액;
        private OneGrid oneGrid1;
        private OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl4;
        private BpPanelControl bpPanelControl3;
        private BpPanelControl bpPanelControl2;
        private BpPanelControl bpPanelControl1;
        private OneGridItem oneGridItem2;
        private BpPanelControl bpPanelControl5;
        private BpPanelControl bpPanelControl6;
        private BpPanelControl bpPanelControl7;
        private BpPanelControl bpPanelControl8;
        private OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl9;
        private BpPanelControl bpPanelControl10;
        private BpPanelControl bpPanelControl11;
        private BpPanelControl bpPanelControl12;
        private OneGridItem oneGridItem4;
        private BpPanelControl bpPanelControl13;
        private OneGrid pnl기본;
        private OneGridItem oneGridItem5;
        private OneGridItem oneGridItem6;
        private OneGridItem oneGridItem7;
        private OneGridItem oneGridItem8;
        private OneGridItem oneGridItem9;
        private OneGridItem oneGridItem10;
        private OneGridItem oneGridItem11;
        private OneGridItem oneGridItem12;
        private OneGridItem oneGridItem13;
        private OneGridItem oneGridItem14;
        private OneGridItem oneGridItem15;
        private OneGridItem oneGridItem16;
        private OneGridItem oneGridItem17;
        private OneGridItem oneGridItem18;
        private OneGridItem oneGridItem19;
        private OneGridItem oneGridItem20;
        private OneGridItem oneGridItem21;
        private OneGridItem oneGridItem22;
        private OneGridItem oneGridItem23;
        private OneGridItem oneGridItem24;
        private OneGridItem oneGridItem25;
        private OneGridItem oneGridItem26;
        private BpPanelControl bpPanelControl14;
        private BpPanelControl bpPanelControl15;
        private BpPanelControl bpPanelControl16;
        private BpPanelControl bpPanelControl17;
        private BpPanelControl bpPanelControl18;
        private BpPanelControl bpPanelControl19;
        private BpPanelControl bpPanelControl21;
        private BpPanelControl bpPanelControl20;
        private BpPanelControl bpPanelControl22;
        private BpPanelControl bpPanelControl23;
        private BpPanelControl bpPanelControl24;
        private BpPanelControl bpPanelControl25;
        private BpPanelControl bpPanelControl26;
        private BpPanelControl bpPanelControl27;
        private BpPanelControl bpPanelControl28;
        private BpPanelControl bpPanelControl29;
        private BpPanelControl bpPanelControl30;
        private BpPanelControl bpPanelControl31;
        private BpPanelControl bpPanelControl32;
        private BpPanelControl bpPanelControl33;
        private BpPanelControl bpPanelControl34;
        private BpPanelControl bpPanelControl35;
        private BpPanelControl bpPanelControl36;
        private BpPanelControl bpPanelControl37;
        private BpPanelControl bpPanelControl38;
        private BpPanelControl bpPanelControl39;
        private BpPanelControl bpPanelControl40;
        private BpPanelControl bpPanelControl41;
        private BpPanelControl bpPanelControl42;
        private BpPanelControl bpPanelControl43;
        private BpPanelControl bpPanelControl44;
        private BpPanelControl bpPanelControl45;
        private BpPanelControl bpPanelControl46;
        private BpPanelControl bpPanelControl47;
        private BpPanelControl bpPanelControl48;
        private BpPanelControl bpPanelControl49;
        private TextBoxExt textBoxExt7;
        private OneGrid pnl자재;
        private OneGridItem oneGridItem27;
        private OneGridItem oneGridItem28;
        private OneGridItem oneGridItem29;
        private OneGridItem oneGridItem30;
        private OneGridItem oneGridItem31;
        private OneGridItem oneGridItem32;
        private OneGridItem oneGridItem33;
        private OneGridItem oneGridItem34;
        private OneGridItem oneGridItem35;
        private OneGridItem oneGridItem36;
        private OneGridItem oneGridItem37;
        private OneGridItem oneGridItem38;
        private OneGridItem oneGridItem39;
        private OneGridItem oneGridItem40;
        private BpPanelControl bpPanelControl50;
        private BpPanelControl bpPanelControl52;
        private BpPanelControl bpPanelControl53;
        private BpPanelControl bpPanelControl54;
        private BpPanelControl bpPanelControl55;
        private BpPanelControl bpPanelControl56;
        private BpPanelControl bpPanelControl57;
        private BpPanelControl bpPanelControl58;
        private BpPanelControl bpPanelControl59;
        private BpPanelControl bpPanelControl60;
        private BpPanelControl bpPanelControl61;
        private BpPanelControl bpPanelControl62;
        private BpPanelControl bpPanelControl63;
        private BpPanelControl bpPanelControl64;
        private BpPanelControl bpPanelControl65;
        private BpPanelControl bpPanelControl66;
        private BpPanelControl bpPanelControl67;
        private BpPanelControl bpPanelControl68;
        private BpPanelControl bpPanelControl69;
        private BpPanelControl bpPanelControl71;
        private BpPanelControl bpPanelControl77;
        private BpPanelControl bpPanelControl75;
        private BpPanelControl bpPanelControl72;
        private BpPanelControl bpPanelControl73;
        private OneGrid pnl오더;
        private OneGridItem oneGridItem41;
        private BpPanelControl bpPanelControl74;
        private BpPanelControl bpPanelControl70;
        private OneGridItem oneGridItem42;
        private BpPanelControl bpPanelControl76;
        private BpPanelControl bpPanelControl78;
        private OneGridItem oneGridItem43;
        private BpPanelControl bpPanelControl79;
        private BpPanelControl bpPanelControl80;
        private OneGridItem oneGridItem44;
        private BpPanelControl bpPanelControl82;
        private OneGridItem oneGridItem45;
        private BpPanelControl bpPanelControl83;
        private BpPanelControl bpPanelControl84;
        private OneGridItem oneGridItem46;
        private BpPanelControl bpPanelControl85;
        private BpPanelControl bpPanelControl86;
        private OneGridItem oneGridItem47;
        private BpPanelControl bpPanelControl87;
        private BpPanelControl bpPanelControl88;
        private OneGridItem oneGridItem48;
        private BpPanelControl bpPanelControl89;
        private BpPanelControl bpPanelControl90;
        private OneGridItem oneGridItem49;
        private BpPanelControl bpPanelControl91;
        private BpPanelControl bpPanelControl92;
        private OneGridItem oneGridItem50;
        private BpPanelControl bpPanelControl93;
        private BpPanelControl bpPanelControl94;
        private OneGridItem oneGridItem51;
        private BpPanelControl bpPanelControl95;
        private BpPanelControl bpPanelControl96;
        private OneGridItem oneGridItem52;
        private BpPanelControl bpPanelControl97;
        private BpPanelControl bpPanelControl98;
        private OneGridItem oneGridItem53;
        private BpPanelControl bpPanelControl99;
        private BpPanelControl bpPanelControl81;
        private OneGridItem oneGridItem54;
        private BpPanelControl bpPanelControl100;
        private BpPanelControl bpPanelControl101;
        private ImagePanel imagePanel2;
        private TableLayoutPanel tableLayoutPanel4;
        private FlexibleRoundedCornerBox pnl첨부파일;
        private OneGrid pnl품질;
        private OneGridItem oneGridItem55;
        private OneGridItem oneGridItem56;
        private OneGridItem oneGridItem57;
        private OneGridItem oneGridItem58;
        private OneGridItem oneGridItem59;
        private OneGridItem oneGridItem60;
        private BpPanelControl bpPanelControl103;
        private BpPanelControl bpPanelControl102;
        private BpPanelControl bpPanelControl104;
        private BpPanelControl bpPanelControl105;
        private BpPanelControl bpPanelControl106;
        private BpPanelControl bpPanelControl107;
        private BpPanelControl bpPanelControl108;
        private OneGrid pnl기타정보;
        private OneGridItem oneGridItem61;
        private OneGridItem oneGridItem62;
        private OneGridItem oneGridItem63;
        private OneGridItem oneGridItem64;
        private OneGridItem oneGridItem65;
        private OneGridItem oneGridItem66;
        private OneGridItem oneGridItem67;
        private OneGridItem oneGridItem68;
        private OneGridItem oneGridItem69;
        private OneGridItem oneGridItem70;
        private OneGridItem oneGridItem71;
        private OneGridItem oneGridItem72;
        private OneGridItem oneGridItem73;
        private OneGridItem oneGridItem74;
        private OneGridItem oneGridItem75;
        private OneGridItem oneGridItem76;
        private OneGridItem oneGridItem77;
        private OneGridItem oneGridItem78;
        private OneGridItem oneGridItem79;
        private OneGridItem oneGridItem80;
        private BpPanelControl bpPanelControl109;
        private BpPanelControl bpPanelControl110;
        private BpPanelControl bpPanelControl111;
        private BpPanelControl bpPanelControl112;
        private BpPanelControl bpPanelControl113;
        private BpPanelControl bpPanelControl115;
        private BpPanelControl bpPanelControl114;
        private BpPanelControl bpPanelControl116;
        private BpPanelControl bpPanelControl117;
        private BpPanelControl bpPanelControl118;
        private BpPanelControl bpPanelControl119;
        private BpPanelControl bpPanelControl120;
        private BpPanelControl bpPanelControl121;
        private BpPanelControl bpPanelControl122;
        private BpPanelControl bpPanelControl123;
        private BpPanelControl bpPanelControl124;
        private BpPanelControl bpPanelControl125;
        private BpPanelControl bpPanelControl126;
        private BpPanelControl bpPanelControl127;
        private BpPanelControl bpPanelControl128;
        private BpPanelControl bpPanelControl129;
        private BpPanelControl bpPanelControl130;
        private BpPanelControl bpPanelControl131;
        private BpPanelControl bpPanelControl132;
        private BpPanelControl bpPanelControl133;
        private BpPanelControl bpPanelControl134;
        private BpPanelControl bpPanelControl135;
        private BpPanelControl bpPanelControl136;
        private BpPanelControl bpPanelControl137;
        private BpPanelControl bpPanelControl138;
        private BpPanelControl bpPanelControl139;
        private BpPanelControl bpPanelControl140;
        private BpPanelControl bpPanelControl141;
        private BpPanelControl bpPanelControl142;
        private BpPanelControl bpPanelControl143;
        private RoundedButton _btnAutoCdItem;
        private OneGrid oneGrid2;
        private OneGridItem oneGridItem81;
        private OneGridItem oneGridItem82;
        private BpPanelControl bpPanelControl147;
        private BpPanelControl bpPanelControl146;
        private BpPanelControl bpPanelControl145;
        private LabelExt _lblUserDef07_S;
        private DropDownComboBox _cboUserDef07_S;
        private BpPanelControl bpPanelControl144;
        private LabelExt _lblUserDef06_S;
        private DropDownComboBox _cboUserDef06_S;
        private BpPanelControl bpPanelControl151;
        private BpPanelControl bpPanelControl150;
        private BpPanelControl bpPanelControl149;
        private BpPanelControl bpPanelControl148;
        private LabelExt _lblUserDef09_S;
        private DropDownComboBox _cboUserDef09_S;
        private LabelExt _lblUserDef08_S;
        private DropDownComboBox _cboUserDef08_S;
        private LabelExt _lblUserDef02_S;
        private TextBoxExt _txtUserDef02_S;
        private LabelExt _lblUserDef01_S;
        private TextBoxExt _txtUserDef01_S;
        private LabelExt _lblUserDef04_S;
        private TextBoxExt _txtUserDef04_S;
        private LabelExt _lblUserDef03_S;
        private TextBoxExt _txtUserDef03_S;
        private OneGridItem oneGridItem83;
        private BpPanelControl bpPanelControl155;
        private BpPanelControl bpPanelControl154;
        private BpPanelControl bpPanelControl153;
        private LabelExt _lblUserDef31_S;
        private BpCodeTextBox _ctxUserDef31_S;
        private BpPanelControl bpPanelControl152;
        private LabelExt _lblUserDef30_S;
        private BpCodeTextBox _ctxUserDef30_S;
        private LabelExt _lblUserDef32_S;
        private BpCodeTextBox _ctxUserDef32_S;
        private OneGridItem oneGridItem84;
        private BpPanelControl bpPanelControl157;
        private LabelExt _lblUserDef34_S;
        private DropDownComboBox _cboUserDef34_S;
        private BpPanelControl bpPanelControl156;
        private LabelExt _lblUserDef33_S;
        private DropDownComboBox _cboUserDef33_S;
        private BpPanelControl bpPanelControl158;
        private LabelExt _lblUserDef35_S;
        private DropDownComboBox _cboUserDef35_S;
        private BpCodeTextBox _bpCdItemR;
        private TabPage tpgStndItem;
        private OneGrid pnlStndItem;
        private OneGridItem oneGridItem85;
        private OneGridItem oneGridItem86;
        private OneGridItem oneGridItem87;
        private OneGridItem oneGridItem88;
        private OneGridItem oneGridItem89;
        private OneGridItem oneGridItem90;
        private OneGridItem oneGridItem91;
        private OneGridItem oneGridItem92;
        private OneGridItem oneGridItem93;
        private OneGridItem oneGridItem94;
        private BpPanelControl bpPanelControl51;
        private LabelExt lblStndItem1;
        private TextBoxExt _txtStndItem01;
        private BpPanelControl bpPanelControl159;
        private LabelExt lblStndItem2;
        private TextBoxExt _txtStndItem02;
        private BpPanelControl bpPanelControl160;
        private LabelExt lblStndItem3;
        private TextBoxExt _txtStndItem03;
        private BpPanelControl bpPanelControl161;
        private LabelExt lblStndItem4;
        private TextBoxExt _txtStndItem04;
        private BpPanelControl bpPanelControl162;
        private LabelExt lblStndItem5;
        private TextBoxExt _txtStndItem05;
        private BpPanelControl bpPanelControl163;
        private LabelExt lblStndItem6;
        private BpPanelControl bpPanelControl164;
        private LabelExt lblStndItem7;
        private BpPanelControl bpPanelControl165;
        private LabelExt lblStndItem8;
        private BpPanelControl bpPanelControl166;
        private LabelExt lblStndItem9;
        private LabelExt lblStndItem10;
        private CurrencyTextBox _txtNumStndItem01;
        private CurrencyTextBox _txtNumStndItem02;
        private CurrencyTextBox _txtNumStndItem03;
        private CurrencyTextBox _txtNumStndItem04;
        private BpPanelControl bpPanelControl167;
        private CurrencyTextBox _txtNumStndItem05;
        private BpComboBox bpc품목군S;
        private OneGridItem oneGridItem95;
        private OneGridItem oneGridItem96;
        private OneGridItem oneGridItem97;
        private BpPanelControl bpPanelControl168;
        private TextBox txt검색2;
        private DropDownComboBox cboSearch2;
        private BpPanelControl bpPanelControl169;
        private TextBox txt검색1;
        private DropDownComboBox cboSearch1;
        private BpPanelControl bpPanelControl170;
        private TextBox txt검색3;
        private DropDownComboBox cboSearch3;
        private OneGridItem oneGridItem98;
        private BpPanelControl bpPanelControl171;
        private DatePicker _datePicker36;
        private LabelExt lbl사용자정의36;
        private BpPanelControl bpPanelControl172;
        private DatePicker _datePicker37;
        private LabelExt lbl사용자정의37;
        private RoundedButton btnCopyToCompany;
        private RoundedButton _btnBulkSave;
        private OneGridItem oneGridItem99;
        private BpPanelControl bpPanelControl173;
        private LabelExt m_lblUnitSu;
        private DropDownComboBox cbo외주단위;
        private CurrencyTextBox cur외주단위수량;
        private LabelExt lbl기본텝비고;
        private TextBoxExt txt_기본_비고;
        private DropDownComboBox cbo생산입고검사불량;
        private LabelExt labelExt3;
        private BpComboBox bpcClsmS;
        private BpComboBox bpcClslS;
        private BpComboBox bpcClssS;
        private RoundedButton _btnBarcode;
        private OneGridItem oneGridItem100;
        private GlobalzationSearchBox txt품명;
        private OneGridItem oneGridItem101;
        private BpPanelControl bpPanelControl174;
        private BpPanelControl bpPanelControl175;
        private CheckBoxExt _chkSString;
        private OneGridItem oneGridItem102;
        private BpPanelControl bpPanelControl177;
        private DatePicker _datePicker39;
        private LabelExt lbl사용자정의39;
        private BpPanelControl bpPanelControl176;
        private DatePicker _datePicker38;
        private LabelExt lbl사용자정의38;
        private CurrencyTextBox _curUnitMoFact;
        private RoundedButton _btnZSATREC_PASS;
        private CheckBoxExt _chkUpper;
        private OneGridItem oneGridItem103;
        private BpPanelControl bpPanelControl178;
        private LabelExt labelExt12;
        private BpCodeTextBox ctxCC코드;
        private CheckBoxExt _chkTop;
    }
}