using System;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Windows.Print;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.ERPU.MF.Common;

namespace pur
{
	/// <summary>
	/// P_PU_INVMON_RPT에 대한 요약 설명입니다.
	/// </summary>
	public class P_PU_INVMON_RPT : Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드
		
		#region -> 멤버필드(일반)

        private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ImageList imageList1;
		private Duzon.Common.Controls.LabelExt m_lblFg_Acct;
		private Duzon.Common.Controls.LabelExt m_lblNo_Item;
		private Duzon.Common.Controls.LabelExt m_lblDy_Cumul;
		private Duzon.Common.Controls.LabelExt m_lblNm_Sl;
		private Duzon.Common.Controls.LabelExt m_lblCd_Plant;
		private Duzon.Common.Controls.LabelExt label8;
		private System.ComponentModel.IContainer components;
        private Duzon.Common.Controls.DropDownComboBox m_cbCd_Plant;
		private System.Data.DataSet ds_Ty1;
		private System.Data.DataTable dataTable1;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Data.DataColumn dataColumn7;
		private System.Data.DataColumn dataColumn8;
        private System.Windows.Forms.DateTimePicker m_mskDy_Cumul;
	

		#endregion

		#region -> 멤버필드(주요)	
        P_PU_INVMON_RPT_BIZ _biz = new P_PU_INVMON_RPT_BIZ();
		private bool _isPainted = false;
        // 그리드
		private Duzon.Common.BpControls.BpCodeTextBox m_txtNo_Item_s;
		private Duzon.Common.BpControls.BpCodeTextBox m_txtNo_Item_e;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private FlexGrid _flexD1;
        private FlexGrid _flexM2;
        private FlexGrid _flexD2;
        private FlexGrid _flexM1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private TextBoxExt txt출고창고;
        private TextButton bp출고창고;
        private Duzon.Common.BpControls.BpCodeTextBox bpGI_SL;
        private TabPage tabPage3;
        private SplitContainer splitContainer3;
        private FlexGrid _flexM3;
        private FlexGrid _flexD3;
        private LabelExt TXT프로젝트;
        private BpCodeTextBox ctx프로젝트;
        private BpComboBox cbo_계정구분;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl3;
        private BpPanelControl bpPanelControl2;
        private BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private BpPanelControl bpPanelControl4;
        private BpPanelControl bpPanelControl5;
        private BpPanelControl bpPanelControl6;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl9;
        private BpPanelControl bpPanelControl8;
        private BpPanelControl bpPanelControl7;
        private LabelExt lbl_ITEMGRP;
        private BpCodeTextBox ctx_ITEMGRP;
        private Dass.FlexGrid.FlexGrid _flexL;	
		
		#endregion
	
		#endregion

		#region ♣ 생성자/소멸자
	
		#region -> 생성자
		public P_PU_INVMON_RPT()
		{
			
			InitializeComponent();
		
			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Page_Paint); 	
		}
		
		#endregion

		#region -> 소멸자
		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_INVMON_RPT));
            this.cbo_계정구분 = new Duzon.Common.BpControls.BpComboBox();
            this.TXT프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.ctx프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpGI_SL = new Duzon.Common.BpControls.BpCodeTextBox();
            this.txt출고창고 = new Duzon.Common.Controls.TextBoxExt();
            this.bp출고창고 = new Duzon.Common.Controls.TextButton();
            this.m_cbCd_Plant = new Duzon.Common.Controls.DropDownComboBox();
            this.label8 = new Duzon.Common.Controls.LabelExt();
            this.m_lblFg_Acct = new Duzon.Common.Controls.LabelExt();
            this.m_lblNo_Item = new Duzon.Common.Controls.LabelExt();
            this.m_lblDy_Cumul = new Duzon.Common.Controls.LabelExt();
            this.m_lblNm_Sl = new Duzon.Common.Controls.LabelExt();
            this.m_lblCd_Plant = new Duzon.Common.Controls.LabelExt();
            this.m_mskDy_Cumul = new System.Windows.Forms.DateTimePicker();
            this.m_txtNo_Item_s = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_txtNo_Item_e = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexM1 = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexD1 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._flexM2 = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexD2 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._flexM3 = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexD3 = new Dass.FlexGrid.FlexGrid(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ds_Ty1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl_ITEMGRP = new Duzon.Common.Controls.LabelExt();
            this.ctx_ITEMGRP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.mDataArea.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // cbo_계정구분
            // 
            this.cbo_계정구분.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
            this.cbo_계정구분.Location = new System.Drawing.Point(81, 1);
            this.cbo_계정구분.Name = "cbo_계정구분";
            this.cbo_계정구분.Size = new System.Drawing.Size(186, 21);
            this.cbo_계정구분.TabIndex = 234;
            this.cbo_계정구분.TabStop = false;
            this.cbo_계정구분.Tag = "";
            this.cbo_계정구분.UserCodeName = "NAME";
            this.cbo_계정구분.UserCodeValue = "CODE";
            this.cbo_계정구분.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // TXT프로젝트
            // 
            this.TXT프로젝트.Location = new System.Drawing.Point(0, 3);
            this.TXT프로젝트.Name = "TXT프로젝트";
            this.TXT프로젝트.Size = new System.Drawing.Size(80, 16);
            this.TXT프로젝트.TabIndex = 71;
            this.TXT프로젝트.Text = "프로젝트";
            this.TXT프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TXT프로젝트.Visible = false;
            // 
            // ctx프로젝트
            // 
            this.ctx프로젝트.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx프로젝트.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx프로젝트.Location = new System.Drawing.Point(81, 1);
            this.ctx프로젝트.Name = "ctx프로젝트";
            this.ctx프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx프로젝트.Size = new System.Drawing.Size(186, 21);
            this.ctx프로젝트.TabIndex = 232;
            this.ctx프로젝트.TabStop = false;
            this.ctx프로젝트.Tag = "CD_PJT;NM_PJT";
            this.ctx프로젝트.UserCodeName = "NM_PROJECT";
            this.ctx프로젝트.UserCodeValue = "NO_PROJECT";
            this.ctx프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            this.ctx프로젝트.Visible = false;
            // 
            // bpGI_SL
            // 
            this.bpGI_SL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.bpGI_SL.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bpGI_SL.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.bpGI_SL.Location = new System.Drawing.Point(81, 1);
            this.bpGI_SL.Name = "bpGI_SL";
            this.bpGI_SL.Size = new System.Drawing.Size(186, 21);
            this.bpGI_SL.TabIndex = 219;
            this.bpGI_SL.TabStop = false;
            this.bpGI_SL.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // txt출고창고
            // 
            this.txt출고창고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt출고창고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt출고창고.Enabled = false;
            this.txt출고창고.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt출고창고.Location = new System.Drawing.Point(238, 1);
            this.txt출고창고.MaxLength = 100;
            this.txt출고창고.Name = "txt출고창고";
            this.txt출고창고.Size = new System.Drawing.Size(7, 21);
            this.txt출고창고.TabIndex = 218;
            this.txt출고창고.Tag = "";
            this.txt출고창고.Visible = false;
            // 
            // bp출고창고
            // 
            this.bp출고창고.BackColor = System.Drawing.Color.White;
            this.bp출고창고.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp출고창고.ButtonImage")));
            this.bp출고창고.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bp출고창고.Location = new System.Drawing.Point(175, 2);
            this.bp출고창고.Modified = false;
            this.bp출고창고.Name = "bp출고창고";
            this.bp출고창고.Size = new System.Drawing.Size(41, 21);
            this.bp출고창고.TabIndex = 217;
            this.bp출고창고.Tag = "";
            this.bp출고창고.Visible = false;
            this.bp출고창고.Search += new Duzon.Common.Controls.SearchEventHandler(this.bp출고창고_Search);
            // 
            // m_cbCd_Plant
            // 
            this.m_cbCd_Plant.AutoDropDown = true;
            this.m_cbCd_Plant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.m_cbCd_Plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbCd_Plant.ItemHeight = 12;
            this.m_cbCd_Plant.Location = new System.Drawing.Point(81, 1);
            this.m_cbCd_Plant.Name = "m_cbCd_Plant";
            this.m_cbCd_Plant.Size = new System.Drawing.Size(186, 20);
            this.m_cbCd_Plant.TabIndex = 0;
            this.m_cbCd_Plant.UseKeyEnter = false;
            this.m_cbCd_Plant.UseKeyF3 = false;
            this.m_cbCd_Plant.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(167, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 18);
            this.label8.TabIndex = 110;
            this.label8.Text = "~";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblFg_Acct
            // 
            this.m_lblFg_Acct.Location = new System.Drawing.Point(0, 3);
            this.m_lblFg_Acct.Name = "m_lblFg_Acct";
            this.m_lblFg_Acct.Size = new System.Drawing.Size(80, 16);
            this.m_lblFg_Acct.TabIndex = 71;
            this.m_lblFg_Acct.Text = "계정구분";
            this.m_lblFg_Acct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNo_Item
            // 
            this.m_lblNo_Item.Location = new System.Drawing.Point(0, 3);
            this.m_lblNo_Item.Name = "m_lblNo_Item";
            this.m_lblNo_Item.Size = new System.Drawing.Size(80, 16);
            this.m_lblNo_Item.TabIndex = 72;
            this.m_lblNo_Item.Text = "품번";
            this.m_lblNo_Item.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDy_Cumul
            // 
            this.m_lblDy_Cumul.Location = new System.Drawing.Point(0, 3);
            this.m_lblDy_Cumul.Name = "m_lblDy_Cumul";
            this.m_lblDy_Cumul.Size = new System.Drawing.Size(80, 16);
            this.m_lblDy_Cumul.TabIndex = 71;
            this.m_lblDy_Cumul.Text = "기준년도";
            this.m_lblDy_Cumul.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNm_Sl
            // 
            this.m_lblNm_Sl.Location = new System.Drawing.Point(0, 3);
            this.m_lblNm_Sl.Name = "m_lblNm_Sl";
            this.m_lblNm_Sl.Size = new System.Drawing.Size(80, 16);
            this.m_lblNm_Sl.TabIndex = 72;
            this.m_lblNm_Sl.Text = "창고";
            this.m_lblNm_Sl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCd_Plant
            // 
            this.m_lblCd_Plant.Location = new System.Drawing.Point(0, 3);
            this.m_lblCd_Plant.Name = "m_lblCd_Plant";
            this.m_lblCd_Plant.Size = new System.Drawing.Size(80, 16);
            this.m_lblCd_Plant.TabIndex = 71;
            this.m_lblCd_Plant.Text = "공장";
            this.m_lblCd_Plant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_mskDy_Cumul
            // 
            this.m_mskDy_Cumul.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskDy_Cumul.CustomFormat = "yyyy";
            this.m_mskDy_Cumul.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_mskDy_Cumul.Location = new System.Drawing.Point(81, 1);
            this.m_mskDy_Cumul.Name = "m_mskDy_Cumul";
            this.m_mskDy_Cumul.ShowUpDown = true;
            this.m_mskDy_Cumul.Size = new System.Drawing.Size(82, 21);
            this.m_mskDy_Cumul.TabIndex = 1;
            this.m_mskDy_Cumul.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // m_txtNo_Item_s
            // 
            this.m_txtNo_Item_s.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.m_txtNo_Item_s.ItemBackColor = System.Drawing.Color.Empty;
            this.m_txtNo_Item_s.Location = new System.Drawing.Point(81, 1);
            this.m_txtNo_Item_s.Name = "m_txtNo_Item_s";
            this.m_txtNo_Item_s.Size = new System.Drawing.Size(82, 21);
            this.m_txtNo_Item_s.TabIndex = 4;
            this.m_txtNo_Item_s.TabStop = false;
            this.m_txtNo_Item_s.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // m_txtNo_Item_e
            // 
            this.m_txtNo_Item_e.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.m_txtNo_Item_e.ItemBackColor = System.Drawing.Color.Empty;
            this.m_txtNo_Item_e.Location = new System.Drawing.Point(183, 1);
            this.m_txtNo_Item_e.Name = "m_txtNo_Item_e";
            this.m_txtNo_Item_e.Size = new System.Drawing.Size(82, 21);
            this.m_txtNo_Item_e.TabIndex = 5;
            this.m_txtNo_Item_e.TabStop = false;
            this.m_txtNo_Item_e.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 20);
            this.tabControl1.Location = new System.Drawing.Point(3, 94);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(821, 482);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 100;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(813, 454);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "총계";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._flexM1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flexD1);
            this.splitContainer1.Size = new System.Drawing.Size(809, 450);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 0;
            // 
            // _flexM1
            // 
            this._flexM1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM1.AutoResize = false;
            this._flexM1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM1.Cursor = System.Windows.Forms.Cursors.Default;
            this._flexM1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM1.EnabledHeaderCheck = true;
            this._flexM1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM1.Location = new System.Drawing.Point(0, 0);
            this._flexM1.Name = "_flexM1";
            this._flexM1.Rows.Count = 1;
            this._flexM1.Rows.DefaultSize = 20;
            this._flexM1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM1.ShowSort = false;
            this._flexM1.Size = new System.Drawing.Size(809, 208);
            this._flexM1.StyleInfo = resources.GetString("_flexM1.StyleInfo");
            this._flexM1.TabIndex = 3;
            this._flexM1.UseGridCalculator = true;
            // 
            // _flexD1
            // 
            this._flexD1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD1.AutoResize = false;
            this._flexD1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD1.EnabledHeaderCheck = true;
            this._flexD1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD1.Location = new System.Drawing.Point(0, 0);
            this._flexD1.Name = "_flexD1";
            this._flexD1.Rows.Count = 1;
            this._flexD1.Rows.DefaultSize = 20;
            this._flexD1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD1.ShowSort = false;
            this._flexD1.Size = new System.Drawing.Size(809, 238);
            this._flexD1.StyleInfo = resources.GetString("_flexD1.StyleInfo");
            this._flexD1.TabIndex = 3;
            this._flexD1.UseGridCalculator = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(813, 454);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "상세";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._flexM2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._flexD2);
            this.splitContainer2.Size = new System.Drawing.Size(809, 450);
            this.splitContainer2.SplitterDistance = 202;
            this.splitContainer2.TabIndex = 0;
            // 
            // _flexM2
            // 
            this._flexM2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM2.AutoResize = false;
            this._flexM2.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM2.Cursor = System.Windows.Forms.Cursors.Default;
            this._flexM2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM2.EnabledHeaderCheck = true;
            this._flexM2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM2.Location = new System.Drawing.Point(0, 0);
            this._flexM2.Name = "_flexM2";
            this._flexM2.Rows.Count = 1;
            this._flexM2.Rows.DefaultSize = 20;
            this._flexM2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM2.ShowSort = false;
            this._flexM2.Size = new System.Drawing.Size(809, 202);
            this._flexM2.StyleInfo = resources.GetString("_flexM2.StyleInfo");
            this._flexM2.TabIndex = 3;
            this._flexM2.UseGridCalculator = true;
            // 
            // _flexD2
            // 
            this._flexD2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD2.AutoResize = false;
            this._flexD2.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD2.EnabledHeaderCheck = true;
            this._flexD2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD2.Location = new System.Drawing.Point(0, 0);
            this._flexD2.Name = "_flexD2";
            this._flexD2.Rows.Count = 1;
            this._flexD2.Rows.DefaultSize = 20;
            this._flexD2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD2.ShowSort = false;
            this._flexD2.Size = new System.Drawing.Size(809, 244);
            this._flexD2.StyleInfo = resources.GetString("_flexD2.StyleInfo");
            this._flexD2.TabIndex = 3;
            this._flexD2.UseGridCalculator = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer3);
            this.tabPage3.ImageIndex = 1;
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(813, 476);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "상세(프로젝트)";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Visible = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._flexM3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._flexD3);
            this.splitContainer3.Size = new System.Drawing.Size(807, 470);
            this.splitContainer3.SplitterDistance = 211;
            this.splitContainer3.TabIndex = 1;
            // 
            // _flexM3
            // 
            this._flexM3.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM3.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM3.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM3.AutoResize = false;
            this._flexM3.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM3.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM3.EnabledHeaderCheck = true;
            this._flexM3.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM3.Location = new System.Drawing.Point(0, 0);
            this._flexM3.Name = "_flexM3";
            this._flexM3.Rows.Count = 1;
            this._flexM3.Rows.DefaultSize = 20;
            this._flexM3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM3.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM3.ShowSort = false;
            this._flexM3.Size = new System.Drawing.Size(807, 211);
            this._flexM3.StyleInfo = resources.GetString("_flexM3.StyleInfo");
            this._flexM3.TabIndex = 4;
            this._flexM3.UseGridCalculator = true;
            // 
            // _flexD3
            // 
            this._flexD3.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD3.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD3.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD3.AutoResize = false;
            this._flexD3.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD3.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD3.EnabledHeaderCheck = true;
            this._flexD3.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD3.Location = new System.Drawing.Point(0, 0);
            this._flexD3.Name = "_flexD3";
            this._flexD3.Rows.Count = 1;
            this._flexD3.Rows.DefaultSize = 20;
            this._flexD3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD3.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD3.ShowSort = false;
            this._flexD3.Size = new System.Drawing.Size(807, 255);
            this._flexD3.StyleInfo = resources.GetString("_flexD3.StyleInfo");
            this._flexD3.TabIndex = 4;
            this._flexD3.UseGridCalculator = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // ds_Ty1
            // 
            this.ds_Ty1.DataSetName = "NewDataSet";
            this.ds_Ty1.Locale = new System.Globalization.CultureInfo("ko-KR");
            this.ds_Ty1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "NM_PLANT";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "YEAR";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "FG_ACCT";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "NM_SL";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "CD_ITEM";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "NM_ITEM";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "STND_ITEM";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "UNIT_IM";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 102;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 85);
            this.oneGrid1.TabIndex = 101;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbo_계정구분);
            this.bpPanelControl3.Controls.Add(this.m_lblFg_Acct);
            this.bpPanelControl3.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.m_lblDy_Cumul);
            this.bpPanelControl2.Controls.Add(this.m_mskDy_Cumul);
            this.bpPanelControl2.Controls.Add(this.bp출고창고);
            this.bpPanelControl2.Controls.Add(this.txt출고창고);
            this.bpPanelControl2.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.m_lblCd_Plant);
            this.bpPanelControl1.Controls.Add(this.m_cbCd_Plant);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.TXT프로젝트);
            this.bpPanelControl4.Controls.Add(this.ctx프로젝트);
            this.bpPanelControl4.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl4.TabIndex = 5;
            this.bpPanelControl4.Text = "bpPanelControl4";
            this.bpPanelControl4.Visible = false;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.m_lblNo_Item);
            this.bpPanelControl5.Controls.Add(this.label8);
            this.bpPanelControl5.Controls.Add(this.m_txtNo_Item_e);
            this.bpPanelControl5.Controls.Add(this.m_txtNo_Item_s);
            this.bpPanelControl5.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl5.TabIndex = 4;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.m_lblNm_Sl);
            this.bpPanelControl6.Controls.Add(this.bpGI_SL);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl6.TabIndex = 3;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl9);
            this.oneGridItem3.Controls.Add(this.bpPanelControl8);
            this.oneGridItem3.Controls.Add(this.bpPanelControl7);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.ctx_ITEMGRP);
            this.bpPanelControl7.Controls.Add(this.lbl_ITEMGRP);
            this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl7.TabIndex = 0;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl8.TabIndex = 1;
            this.bpPanelControl8.Text = "bpPanelControl8";
            // 
            // bpPanelControl9
            // 
            this.bpPanelControl9.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl9.Name = "bpPanelControl9";
            this.bpPanelControl9.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl9.TabIndex = 2;
            this.bpPanelControl9.Text = "bpPanelControl9";
            // 
            // lbl_ITEMGRP
            // 
            this.lbl_ITEMGRP.Location = new System.Drawing.Point(0, 3);
            this.lbl_ITEMGRP.Name = "lbl_ITEMGRP";
            this.lbl_ITEMGRP.Size = new System.Drawing.Size(80, 16);
            this.lbl_ITEMGRP.TabIndex = 0;
            this.lbl_ITEMGRP.Text = "품목군";
            this.lbl_ITEMGRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx_ITEMGRP
            // 
            this.ctx_ITEMGRP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEMGP_SUB;
            this.ctx_ITEMGRP.Location = new System.Drawing.Point(81, 1);
            this.ctx_ITEMGRP.Name = "ctx_ITEMGRP";
            this.ctx_ITEMGRP.Size = new System.Drawing.Size(186, 21);
            this.ctx_ITEMGRP.TabIndex = 1;
            this.ctx_ITEMGRP.TabStop = false;
            this.ctx_ITEMGRP.Text = "bpCodeTextBox1";
            // 
            // P_PU_INVMON_RPT
            // 
            this.Name = "P_PU_INVMON_RPT";
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region ♣ 초기화

		#region -> Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				this.Enabled   = false;

              
          	//페이지를 로드 하는중입니다.
				this.ShowStatusBarMessage(1);
				this.SetProgressBarValue(100, 10);
				
				InitControl();
				InitGridM1();
				InitGridD1();
				InitGridM2();
				InitGridD2();
                InitGridM3();
                InitGridD3();
                InitEvent();
							
				
				this.SetProgressBarValue(100, 100);		
				Application.DoEvents();

			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.ToolBarSearchButtonEnabled = true;		
				this.ShowStatusBarMessage(0);
				this.SetProgressBarValue(100, 0);
				Cursor.Current = Cursors.Default;
			}		
		}

		#endregion

		#region -> InitControl

		private void InitControl()
		{
			try
			{
				m_mskDy_Cumul.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0,4)),1,1);

				
				DataTable table = ds_Ty1.Tables[0].Copy();
				table.TableName = "TABLE2";
                
				ds_Ty1.Tables.Add(table);

                if (Config.MA_ENV.PJT형여부 == "Y")
                {
                    ctx프로젝트.Visible = true;
                    TXT프로젝트.Visible = true;
                    bpPanelControl4.Visible = true;
                }
                else
                {
                    tabControl1.Controls.RemoveAt(2);
                }


			}
			catch(Exception ex)
			{
				throw ex;
			}

		}
		
		#endregion

        #region ->InitEvent

        private void InitEvent()
        {
            m_txtNo_Item_s.CodeChanged += new EventHandler(Control_CodeChanged);
            ctx프로젝트.QueryBefore += new BpQueryHandler(OnBpCodeTextBox_QueryBefore);
        }

        #endregion

        #region -> GetDDItem

        private string GetDDItem(params string[] colName)
		{
			string temp = "";
			
			for(int i = 0; i < colName.Length; i++)
			{
				switch(colName[i])		// DataView 의 컬럼이름
				{
					case "CHK":		
						temp = temp + " + " +  "S";
						break;
					case "CD_ITEM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","CD_ITEM");
						break;
					case "NM_ITEM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NM_ITEM");;
						break;
					case "STND_ITEM":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","STND_ITEM");
						break;
					case "UNIT_IM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","UNIT_IM");
						break;					
					case "YM_IO":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","AT_YM");
						break;
					case "QT_OPEN":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_OPEN");
						break;
					case "QT_IN":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","RCV");
						break;
					case "QT_OUT":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","ISU");
						break;
					case "QT_GOOD_INV":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","INV_GOOD");
						break;
					case "QT_REJECT_INV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","INV_INFERIOR");
						break;
					case "QT_INSP_INV":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","INV_INSP");
						break;					
					case "NM_SL":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NM_SL");;
						break;											
					case "QT_TRANS_INV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","INV_TRANS");
						break;
					case "NM_QTIOTP":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","CD_QTIO");
						break;
							
				}
			}
			
			if(temp == "")
				return "";
			else
				return temp.Substring(3,temp.Length-3);
		}

		#endregion

		#region -> InitGridM1
		/// <summary>
		/// 그리드 형태 정의 함수
		/// </summary>
		private void InitGridM1()
		{	
            _flexM1.BeginSetting(1, 1, true);
            _flexM1.SetCol("CD_ITEM", "품목코드", 120, false);
            _flexM1.SetCol("NM_ITEM", "품목명", 150, false);
            _flexM1.SetCol("STND_ITEM", "규격", 120, false);
            _flexM1.SetCol("UNIT_IM", "재고단위", 80, false);
            _flexM1.SetCol("STND_DETAIL_ITEM", "세부규격", 70);
            _flexM1.SetCol("MAT_ITEM", "재질", 70);
            _flexM1.SetCol("GRP_MFG", "제품군", 140);
            _flexM1.SetCol("NM_ITEMGRP", "품목군", 140);
            //2011/03/23 조형우 관리자 컬럼 추가
            _flexM1.SetCol("NM_MANAGER1", "관리자1", 80, false);
            _flexM1.SetCol("NM_MANAGER2", "관리자2", 80, false);
            _flexM1.SetCol("NO_DESIGN", "도면번호", 100);

            _flexM1.SettingVersion = "1.0.0.0";
            _flexM1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			_flexM1.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);	
			_flexM1.DoubleClick += new System.EventHandler(_flex_DoubleClick);
		}

		
		#endregion

		#region -> InitGridD1
		/// <summary>
		/// 그리드 형태 정의 함수
		/// </summary>
		private void InitGridD1()
		{	
            _flexD1.BeginSetting(1, 1, true);
            _flexD1.SetCol("YM_IO", "해당년월", 80, false, typeof(string), FormatTpType.YEAR_MONTH);
            _flexD1.SetCol("QT_OPEN", "기초재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD1.SetCol("QT_IN", "입고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD1.SetCol("QT_OUT", "출고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD1.SetCol("QT_GOOD_INV", "기말재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            
            _flexD1.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);					
		}

	
		#endregion

		#region -> InitGridM2
		/// <summary>
		/// 그리드 형태 정의 함수
		/// </summary>
		private void InitGridM2()
		{
            _flexM2.BeginSetting(1, 1, true);
            _flexM2.SetCol("CD_ITEM", "품목코드", 120, false);
            _flexM2.SetCol("NM_ITEM", "품목명", 150, false);
            _flexM2.SetCol("STND_ITEM", "규격", 120, false);
            _flexM2.SetCol("UNIT_IM", "재고단위", 80, false);
            _flexM2.SetCol("STND_DETAIL_ITEM", "세부규격", 70);
            _flexM2.SetCol("MAT_ITEM", "재질", 70);
            _flexM2.SetCol("GRP_MFG", "제품군", 140);
            _flexM2.SetCol("NM_ITEMGRP", "품목군", 140);
            //2011/03/23 조형우 관리자 컬럼 추가
            _flexM2.SetCol("NM_MANAGER1", "관리자1", 80, false);
            _flexM2.SetCol("NM_MANAGER2", "관리자2", 80, false);
            _flexM2.SetCol("NO_DESIGN", "도면번호", 100);

            _flexM2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexM2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
            _flexM2.DoubleClick += new System.EventHandler(_flex_DoubleClick);

			//_flexM2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);	
			//_flexM2.DoubleClick += new System.EventHandler(_flex_DoubleClick);
		}

		
		#endregion

		#region -> InitGridD2
		/// <summary>
		/// 그리드 형태 정의 함수
		/// </summary>
		private void InitGridD2()
		{	 
            _flexD2.BeginSetting(1, 1, true);
            _flexD2.SetCol("YM_IO", "해당년월", 80, false, typeof(string), FormatTpType.YEAR_MONTH);
            _flexD2.SetCol("NM_QTIOTP", "수불형태", 150, false);
            _flexD2.SetCol("QT_OPEN", "기초재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD2.SetCol("QT_IN", "입고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD2.SetCol("QT_OUT", "출고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD2.SetCol("QT_GOOD_INV", "기말재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);

            _flexD2.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			_flexD2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);	
			_flexD2.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(_flex_AfterSort);
		}

	
		#endregion		

        #region -> InitGridM3
        /// <summary>
        /// 그리드 형태 정의 함수
        /// </summary>
        private void InitGridM3()
        {
            _flexM3.BeginSetting(1, 1, true);
            _flexM3.SetCol("CD_ITEM", "품목코드", 120, false);
            _flexM3.SetCol("NM_ITEM", "품목명", 150, false);
            _flexM3.SetCol("STND_ITEM", "규격", 120, false);
            _flexM3.SetCol("UNIT_IM", "재고단위", 80, false);
            _flexM3.SetCol("STND_DETAIL_ITEM", "세부규격", 70);
            _flexM3.SetCol("MAT_ITEM", "재질", 70);
            _flexM3.SetCol("GRP_MFG", "제품군", 140);
            _flexM3.SetCol("NM_ITEMGRP", "품목군", 140);
            _flexM3.SetCol("NM_MANAGER1", "관리자1", 80, false);
            _flexM3.SetCol("NM_MANAGER2", "관리자2", 80, false);
            _flexM3.SetCol("NO_DESIGN", "도면번호", 100);

            _flexM3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexM3.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
            _flexM3.DoubleClick += new System.EventHandler(_flex_DoubleClick);

            _flexM3.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
            _flexM3.DoubleClick += new System.EventHandler(_flex_DoubleClick);
        }


        #endregion

        #region -> InitGridD3
        /// <summary>
        /// 그리드 형태 정의 함수
        /// </summary>
        private void InitGridD3()
        {
            _flexD3.BeginSetting(1, 1, true);
            _flexD3.SetCol("YM_IO", "해당년월", 80, false, typeof(string), FormatTpType.YEAR_MONTH);
            _flexD3.SetCol("CD_PJT", "프로젝트코드", 100, false);
            _flexD3.SetCol("NM_PJT", "프로젝트명", 120, false);
            _flexD3.SetCol("NM_QTIOTP", "수불형태", 150, false);
            _flexD3.SetCol("QT_OPEN", "기초재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD3.SetCol("QT_IN", "입고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD3.SetCol("QT_OUT", "출고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD3.SetCol("QT_GOOD_INV", "기말재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD3.SettingVersion = "1.0.0.0";
            _flexD3.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flexD3.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
            _flexD3.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(_flex_AfterSort);
        }


        #endregion		
	
		#region -> Page_Paint

		private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try	//반드시 예외처리 할것!
			{			
				if(_isPainted == false)	// 페인트 이벤트를 총 한번만 호출하도록 함
				{						
					this._isPainted = true; //로드 된적이 있다.
				
					
					
					// 콤보박스 초기화
					InitCombo();

					//					_flex.Redraw = false;
					//					_flex.BindingStart();
					//					_flex.DataSource = new DataView(ds_Ty1.Tables[0]);
					//					_flex.BindingEnd();
					//					_flex.Redraw = true;					
					//				
				
					this.Enabled = true; //페이지 전체 활성
					m_cbCd_Plant.Focus();

                    oneGrid1.UseCustomLayout = true;
                    bpPanelControl1.IsNecessaryCondition = true;
                    bpPanelControl6.IsNecessaryCondition = true;
                    oneGrid1.InitCustomLayout();

				}
					
				//	base.OnPaint(e);
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}		
			finally
			{
				this.Enabled = true;
			}
		}	
		#endregion

		#region -> InitCombo
		/// <summary>
		/// 콤보 박스 셋팅
		/// </summary>
		private void InitCombo()
		{
			try
			{
						
				// 공장, 계정구분 
//				string[] lsa_args = {"P_N;000000","S;MA_B000010"};
//				object[] args = { this.LoginInfo.CompanyCode, lsa_args};
//				DataSet g_dsCombo = (DataSet)MainFrameInterface.InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);
	
				DataSet g_dsCombo = this.GetComboData("NC;MA_PLANT","S;MA_B000010");


				//공장				
				m_cbCd_Plant.DataSource = g_dsCombo.Tables[0];
				m_cbCd_Plant.DisplayMember = "NAME";
				m_cbCd_Plant.ValueMember = "CODE";
                m_cbCd_Plant.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;


				DataTable ldt_temp = g_dsCombo.Tables[1].Clone();

				DataRow[] ldr_arg = g_dsCombo.Tables[1].Select( "CODE IN ('','001','002','003','004','005','009')");

				if( ldr_arg != null && ldr_arg.Length >0)
				{
					for(int i=0 ; i < ldr_arg.Length; i++)
					{
						ldt_temp.ImportRow(ldr_arg[i]);
					}
				}

				//계정구분
                //m_cbFg_Acct.DataSource = ldt_temp;
                //m_cbFg_Acct.DisplayMember = "NAME";
                //m_cbFg_Acct.ValueMember = "CODE";
				//m_cbFg_Acct.SelectedIndex = 0;				
								
			
			}
			catch(coDbException ex)
			{
				throw ex;
				//this.ShowErrorMessage(ex, this.PageName);
			}
			catch(Exception ex)
			{
				throw ex;
				//this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.ToolBarSearchButtonEnabled = true;		
				this.Enabled = true;
				
				this.Cursor = Cursors.Default;
			}
		}


		#endregion

		#endregion

		#region ♣ 그리드 이벤트

		private void SearchDetail(string ps_cditem)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				//SP_PU_INVMON_RPT_SELECT_L
				object[] m_obj = new object[7];
				m_obj[0] = D.GetString(m_cbCd_Plant.SelectedValue);	
				m_obj[1] = this.MainFrameInterface.LoginInfo.CompanyCode;
				m_obj[2] = ps_cditem;
                m_obj[3] = bpGI_SL.GetCodeValue();//txt출고창고.Text.ToString();	
				m_obj[4] = m_mskDy_Cumul.Value.Year.ToString("0000");
				m_obj[5] = tabControl1.SelectedIndex;
                m_obj[6] = ctx프로젝트.CodeValue;
                //Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                //si.SpNameSelect = "UP_PU_INVMON_RPT_SELECT_L";			
                //si.SpParamsSelect = m_obj;
                //ResultData result = (ResultData)MainFrameInterface.FillDataTable(si);
                //DataTable dt = (DataTable)result.DataValue;

                DataTable dt =  DBHelper.GetDataTable("UP_PU_INVMON_RPT_SELECT_L", m_obj, "YM_IO ASC");

//				string[] m_str = new string[13];
//				m_str[0] = m_mskDy_Cumul.Value.Year.ToString("0000")+"00";
//				m_str[1] = 	ps_cditem;
//				m_str[2] = 	ps_cditem;				
//				m_str[3] = 	m_txtNm_Sl.Tag.ToString().Trim();
//				m_str[4] = D.GetString(m_cbCd_Plant.SelectedValue);
//				m_str[5] = this.LoginInfo.CompanyCode;
//				m_str[6] = m_mskDy_Cumul.Value.Year.ToString("0000")+"00";
//
//				m_str[7] = 	ps_cditem;
//				m_str[8] = 	m_txtNm_Sl.Tag.ToString().Trim();
//				m_str[9] = D.GetString(m_cbCd_Plant.SelectedValue);
//				m_str[10] = this.LoginInfo.CompanyCode;
//				m_str[11] = m_mskDy_Cumul.Value.Year.ToString("0000")+"01";
//				m_str[12] = m_mskDy_Cumul.Value.Year.ToString("0000")+"12";
//			
//															
//				object[] m_obj = new object[3];
//				m_obj[0] = m_str;
//				m_obj[1] = "CC_PU_INVMON_RPT";
//				m_obj[2] = 5;
//
//				if( tabControl1.SelectedIndex == 1)
//				{
//					m_obj[2] = 6;
//				}
//
//				DataTable dt = (DataTable)(this.MainFrameInterface.InvokeRemoteMethod("PurReport_NTX", "pur.CC_PU_RPT_NTX","CC_PU_RPT_NTX.rem", "SelectDataTable", m_obj));

				//DataSet ds = SelectDataSet(m_str, m_obj[1].ToString(), lia_num );
				Cursor.Current = Cursors.Default;

				if( dt !=null && dt.Rows.Count >0 )
				{
					
					//	dzdwGrid1.DataSource = new DataView(ds.Tables[1]);	
					for(int i=1 ; i < dt.Rows.Count ;i++)
					{
						//	dt.Rows[i].BeginEdit();
						dt.Rows[i]["QT_GOOD_INV"] = System.Double.Parse(dt.Rows[i-1]["QT_GOOD_INV"].ToString().Trim())+
							System.Double.Parse(dt.Rows[i]["QT_GOOD_INV"].ToString().Trim());
						dt.Rows[i]["QT_REJECT_INV"] = System.Double.Parse(dt.Rows[i-1]["QT_REJECT_INV"].ToString().Trim())+
							System.Double.Parse(dt.Rows[i]["QT_REJECT_INV"].ToString().Trim());
						dt.Rows[i]["QT_INSP_INV"] = System.Double.Parse(dt.Rows[i-1]["QT_INSP_INV"].ToString().Trim())+
							System.Double.Parse(dt.Rows[i]["QT_INSP_INV"].ToString().Trim());
						dt.Rows[i]["QT_TRANS_INV"] = System.Double.Parse(dt.Rows[i-1]["QT_TRANS_INV"].ToString().Trim())+
							System.Double.Parse(dt.Rows[i]["QT_TRANS_INV"].ToString().Trim());
						//	dt.Rows[i].EndEdit();
					}
					dt.AcceptChanges();
			
					if(tabControl1.SelectedIndex == 0)
					{
						_flexD1.Redraw=false;
						_flexD1.BindingStart();
						_flexD1.DataSource = new DataView(dt); 
						_flexD1.BindingEnd();						
						_flexD1.Redraw=true;

                        //SubTotalDisplay( _flexD1);
					}
					else if(tabControl1.SelectedIndex == 1)
					{
						_flexD2.Redraw=false;
						_flexD2.BindingStart();
						_flexD2.DataSource = new DataView(dt); 
						_flexD2.BindingEnd();					
						_flexD2.Redraw=true;

                        //SubTotalDisplay( _flexD2);
					}

                    else if (tabControl1.SelectedIndex == 2)
                    {
                        _flexD3.Redraw = false;
                        _flexD3.BindingStart();
                        _flexD3.DataSource =new DataView(dt);
                        _flexD3.BindingEnd();	
                        _flexD3.Redraw = true;
                    }
					ToolBarPrintButtonEnabled = true;
				}
				else
				{
					ToolBarPrintButtonEnabled = false;
					this.ShowMessage("IK1_003");
					return ;
				}
			}
			catch(coDbException ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
		}
		
		private void InDataHeadValue(DataRow ps_Row)
		{
			DataRow newrow;

			if( tabControl1.SelectedIndex == 0)
			{
				ds_Ty1.Tables[0].Clear();

				newrow = ds_Ty1.Tables[0].NewRow();							
				newrow["NM_PLANT"] = m_cbCd_Plant.Text;
				newrow["YEAR"] = m_mskDy_Cumul.Text;
                newrow["FG_ACCT"] = cbo_계정구분.QueryWhereIn_PipeDisplayMember;
                newrow["NM_SL"] = bpGI_SL.GetCodeName();//bp출고창고.Text;
				newrow["CD_ITEM"] = ps_Row["CD_ITEM"].ToString();
				newrow["NM_ITEM"] = ps_Row["NM_ITEM"].ToString();		
				newrow["STND_ITEM"] = ps_Row["STND_ITEM"].ToString();	
				newrow["UNIT_IM"] = ps_Row["UNIT_IM"].ToString();
				ds_Ty1.Tables[0].Rows.Add(newrow);	
			}
			//else
            else if(tabControl1.SelectedIndex ==1)

			{
				ds_Ty1.Tables[1].Clear();

				newrow = ds_Ty1.Tables[1].NewRow();							
				newrow["NM_PLANT"] = m_cbCd_Plant.Text;
				newrow["YEAR"] = m_mskDy_Cumul.Text;
                newrow["FG_ACCT"] = cbo_계정구분.QueryWhereIn_PipeDisplayMember;
                newrow["NM_SL"] = bpGI_SL.GetCodeName(); //bp출고창고.Text;
				newrow["CD_ITEM"] = ps_Row["CD_ITEM"].ToString();
				newrow["NM_ITEM"] = ps_Row["NM_ITEM"].ToString();		
				newrow["STND_ITEM"] = ps_Row["STND_ITEM"].ToString();	
				newrow["UNIT_IM"] = ps_Row["UNIT_IM"].ToString();
				ds_Ty1.Tables[1].Rows.Add(newrow);	
			}

            else if (tabControl1.SelectedIndex == 2)
            {
                ds_Ty1.Tables[1].Clear();

                newrow = ds_Ty1.Tables[1].NewRow();
                newrow ["NM_PLANT"] = m_cbCd_Plant.Text;
                newrow ["YEAR"]= m_mskDy_Cumul.Text;
                newrow ["FG_ACCT"] = m_mskDy_Cumul.Text;
                newrow ["NM_SL"] = bpGI_SL.GetCodeName();
                newrow ["CD_ITEM"] = ps_Row["CD_ITEM"].ToString();
                newrow ["NM_ITEM"] =ps_Row["NM_ITEM"].ToString();
                newrow ["STND_ITEM"] =ps_Row["STND_ITEM"].ToString();
                newrow ["UNIT_IM"] =ps_Row["UNIT_IM"].ToString();
                ds_Ty1.Tables[1].Rows.Add(newrow);
            }

		}
				
		private void _flex_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				Dass.FlexGrid.FlexGrid flex = (Dass.FlexGrid.FlexGrid)sender;
				if(flex.Row > 0 && flex.Col >0)
				{											
					SearchDetail(flex[flex.Row,"CD_ITEM"].ToString());
					InDataHeadValue(flex.DataView[flex.Row-1].Row);
				}
			}
			catch(Exception ex)
			{
                MsgEnd(ex);
			}
		}

		#region -> SubTotalDisplay
		private void SubTotalDisplay( Dass.FlexGrid.FlexGrid flex)
		{
			try
			{
				flex.SubtotalPosition = SubtotalPositionEnum.BelowData;
				flex.SelectionMode=SelectionModeEnum.Cell;
				
				
				CellStyle s = flex.Styles[CellStyleEnum.Subtotal0];
				s.BackColor = Color.FromArgb(234, 234, 234);		
				s.ForeColor = Color.Black;
				s.Font = new Font(flex.Font, FontStyle.Bold);
									
				flex.Subtotal(AggregateEnum.Clear);//MA, GRAND
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["QT_OPEN"].Index);	
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["QT_IN"].Index);	
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["QT_OUT"].Index);		
				//	flex[flex.Rows.Count -1 ,1] = 	this.GetDataDictionaryItem("MA","GRAND");	

			}
			catch
			{
			}
		}

		#endregion

		#region -> _flexM_AfterRowColChange

		private void _flexM_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{				
			}
			catch
			{
			}
		}

		
		#endregion

		#region -> _flex_StartEdit
		
		private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			try
			{

				Dass.FlexGrid.FlexGrid flex = (Dass.FlexGrid.FlexGrid)sender;
				if( flex.Cols[e.Col].Name != "CHK")
				{
					e.Cancel = true;	// 셀 입력상태로 못 들어가게

				}				
			}
			finally
			{
			}			
		}
		
		#endregion
	
		#region -> _flex_AfterSort	
		
		private void _flex_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
		{
			try
			{
				//SubTotalDisplay((Dass.FlexGrid.FlexGrid)sender);
			}
			catch//(Exception ex)
			{
				//MessageBox.Show(ex.ToString());
			}
		}

		
		#endregion

		#endregion		
		
		#region ♣ 브라우저 버턴이벤트

		#region -> 조회 함수
		/// <summary>
		/// 브라우저 버턴에서 조회가 눌러졌을때 처리 부분
		/// </summary>
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{			
				Cursor.Current = Cursors.WaitCursor;
                if (!FieldCheck() || !Chk품목)
				{
					return;
				}

				//m_lblTitle.Focus();

				try
				{
					_flexM1.DataTable.Rows.Clear();
					_flexD1.DataTable.Rows.Clear();	
					_flexM2.DataTable.Rows.Clear();
					_flexD2.DataTable.Rows.Clear();
                    _flexM3.DataTable.Rows.Clear();
                    _flexD3.DataTable.Rows.Clear();

					Thread.Sleep(50);								
				}
				catch{}

				// SP_PU_INVMON_RPT_SELECT_H

				object[] m_obj = new object[9];
				m_obj[0] = D.GetString(m_cbCd_Plant.SelectedValue);	
				m_obj[1] = this.MainFrameInterface.LoginInfo.CompanyCode;
				m_obj[2] = m_txtNo_Item_s.CodeValue.ToString();
				m_obj[3] = m_txtNo_Item_e.CodeValue.ToString();
                m_obj[4] = bpGI_SL.GetCodeValue();//txt출고창고.Text.ToString();
				m_obj[5] = m_mskDy_Cumul.Value.Year.ToString("0000");
                m_obj[6] = D.GetString(cbo_계정구분.QueryWhereIn_Pipe);
                m_obj[7] = ctx프로젝트.CodeValue;
                m_obj[8] = ctx_ITEMGRP.CodeValue;


				Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
				si.SpNameSelect = "UP_PU_INVMON_RPT_SELECT_H";			
				si.SpParamsSelect = m_obj;
				ResultData result = (ResultData)MainFrameInterface.FillDataTable(si);

				DataTable dt = (DataTable)result.DataValue;
				if( dt !=null && dt.Rows.Count >0 )
				{

					// Master 바인딩
					_flexM1.Redraw=false;
					_flexM1.BindingStart();
					_flexM1.DataSource = new DataView(dt); 	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
					_flexM1.BindingEnd();
					_flexM1.Redraw=true;
						
					// Master 바인딩
					_flexM2.Redraw=false;
					_flexM2.BindingStart();
					_flexM2.DataSource = new DataView( dt); 	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
					_flexM2.BindingEnd();
					_flexM2.Redraw=true;

                    _flexM3.Redraw = false;
                    _flexM3.BindingStart();
                    _flexM3.DataSource = new DataView(dt);
                    _flexM3.BindingEnd();
                    _flexM3.Redraw = true;


					ToolBarPrintButtonEnabled = true;
				}				
				else
				{		
					ToolBarPrintButtonEnabled = false;
					this.ShowMessage("IK1_003");
					return ;

				}		
			}
			catch(coDbException ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
		}

		#endregion
		
		#region -> 출력 함수
		
		// 브라우저의 출력 버턴이 클릭될때 처리 부분
		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
            try
            {
                string 메뉴ID = string.Empty;
                string 메뉴명 = string.Empty;

                if (tabControl1.SelectedIndex == 0)
                {
                    메뉴ID = "R_PU_INVMON_RPT_0";
                    메뉴명 = "월별재고현황-총계";
                    _flexH = _flexM1;
                    _flexL = _flexD1;
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    메뉴ID = "R_PU_INVMON_RPT_1";
                    메뉴명 = "월별재고현황-상세";

                    _flexH = _flexM2;
                    _flexL = _flexD2;

                }

                else if (tabControl1.SelectedIndex == 2)
                {
                    메뉴ID = "R_PU_INVMON_RPT_2";
                    메뉴명 = "월별재고현황-프로젝트";

                    _flexH = _flexM3;
                    _flexL = _flexD3;

                }
                 

                string No_PK_Multi = "";

                for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                {
                    No_PK_Multi += _flexH[i, "CD_ITEM"].ToString() + "|";
                }

                if (!_flexH.HasNormalRow && _flexL.HasNormalRow)
                    return;

                ReportHelper rptHelper = new ReportHelper(메뉴ID, 메뉴명);

                rptHelper.가로출력();

                DataTable dt = _biz.SearchPrint(
                               m_mskDy_Cumul.Text,
                               m_cbCd_Plant.SelectedValue == null ? string.Empty : D.GetString(m_cbCd_Plant.SelectedValue), // 사업장
                               cbo_계정구분.QueryWhereIn_Pipe == null ? string.Empty : D.GetString(cbo_계정구분.QueryWhereIn_Pipe), // 사업장
                               txt출고창고.Text.ToString(), m_txtNo_Item_s.CodeValue, m_txtNo_Item_e.CodeValue,
                               tabControl1.SelectedIndex.ToString(),           //  상태 
                               No_PK_Multi);

                rptHelper.SetDataTable(dt);
                rptHelper.SetData("기준년도", m_mskDy_Cumul.Text);
                rptHelper.SetData("공장", m_cbCd_Plant.SelectedValue == null ? string.Empty : m_cbCd_Plant.Text);
                rptHelper.SetData("계정구분", cbo_계정구분.QueryWhereIn_Pipe == null ? string.Empty : cbo_계정구분.QueryWhereIn_PipeDisplayMember);
                rptHelper.SetData("창고", bpGI_SL.GetCodeName()); //bp출고창고.Text);
                rptHelper.SetData("품번FROM", m_txtNo_Item_s.CodeName);
                rptHelper.SetData("품번TO", m_txtNo_Item_e.CodeName);
                rptHelper.Print();

            }

            catch (Exception ex)
            {
              //  Duzon.Common.Controls.MessageBoxEx.Show(ex.ToString(), this.PageName);
                ShowMessage(ex.ToString(), this.PageName);
            }  
			finally
			{

				Cursor.Current = Cursors.Default;
			}	
		}

	
		#endregion
			
		#endregion

		#region ♣ 기타 함수 / 이벤트

		#region >> 필드 체크 함수
		/// <summary>
		/// 필드 체크 함수 
		/// </summary>
		/// <returns></returns>
		private bool FieldCheck()
		{
			if(m_cbCd_Plant.SelectedIndex.ToString() =="-1")
			{
				ShowMessage("WK1_004",m_lblCd_Plant.Text);
				
			//	Duzon.Common.Controls.MessageBoxEx.Show(m_cbCd_Plant.Text +  this.GetMessageDictionaryItem("PU_M000004"),this.PageName);

				m_cbCd_Plant.Focus();
				return false;

			}
			if(m_mskDy_Cumul.Text =="" || m_mskDy_Cumul.Text==null)
			{
				ShowMessage("WK1_004", m_lblDy_Cumul.Text);
				
			//	Duzon.Common.Controls.MessageBoxEx.Show(m_lblDy_Cumul.Text+ this.MainFrameInterface.GetMessageDictionaryItem("PU_M000004"),this.PageName);
				m_mskDy_Cumul.Focus();
				return false;
			}

            //if (txt출고창고.Text == "" || txt출고창고.Text == null)
            if(bpGI_SL.GetCodeValue() == string.Empty)
			{
				ShowMessage("WK1_004", m_lblNm_Sl.Text);
				
			//	Duzon.Common.Controls.MessageBoxEx.Show( m_txtNm_Sl.Text+MainFrameInterface.GetMessageDictionaryItem("PU_M000004"),this.PageName);
				return false;
			}			
			return true;

		}		
		#endregion
	
		#region >> 콤보박스 키이벤트
		private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
		}
		#endregion

		#region >> TextBox Enter 이벤트
		/// <summary>
		/// TextBox Enter 이벤트
		/// </summary>
		private void TextBoxEnterEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData.ToString() == "Enter" || e.KeyData.ToString() == "Down")
			{			
				SendKeys.SendWait("{TAB}");
			}	
			else if(e.KeyData.ToString() == "Up")
				SendKeys.SendWait("+{TAB}");	
		}
		#endregion

        #region -> Bp_Control CodeChanged

        void Control_CodeChanged(object sender, EventArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            try
            {
                switch (bp_Control.Name)
                {
                    case "m_txtNo_Item_s":
                        m_txtNo_Item_e.SetCode(m_txtNo_Item_s.CodeValue, m_txtNo_Item_s.CodeName);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:		// 창고 도움창					
                    if (D.GetString(m_cbCd_Plant.SelectedValue) == "")
                    {
                        this.ShowMessage("PU_M000070");
                        m_cbCd_Plant.Focus();
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P09_CD_PLANT = D.GetString(m_cbCd_Plant.SelectedValue);
                    e.HelpParam.P07_NO_EMP = LoginInfo.EmployeeNo;
                    break;
                case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:		// 발주유형 도움창					
                    if (D.GetString(m_cbCd_Plant.SelectedValue) == "")
                    {
                        this.ShowMessage("PU_M000070");
                        m_cbCd_Plant.Focus();
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P09_CD_PLANT = D.GetString(m_cbCd_Plant.SelectedValue);
                    break;

                case Duzon.Common.Forms.Help.HelpID.P_USER:
                    if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                    {
                        e.HelpParam.P41_CD_FIELD1 = "프로젝트";
                        return;
                    }
                    break;
                case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
                    switch (D.GetString(e.ControlName))
                    {
                        case "cbo_계정구분":
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000010";
                            break;
                    }
                    break;
            }
        }

        private void bp출고창고_Search(object sender, SearchEventArgs e)
        {
            master.P_MA_SL_AUTH_SUB dlg = null;

            try
            {
                dlg = new master.P_MA_SL_AUTH_SUB(D.GetString(m_cbCd_Plant.SelectedValue), Global.MainFrame.LoginInfo.EmployeeNo);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txt출고창고.Text = dlg.returnParams[0];   //창고번호
                    bp출고창고.Text = dlg.returnParams[1];      //창고이름
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		#endregion

        #region ♥ 속성

        bool Chk품목 { get { return Checker.IsValid(m_txtNo_Item_s, m_txtNo_Item_e, false, DD("품목From"), DD("품목To")); } }

        #endregion
	}
}
