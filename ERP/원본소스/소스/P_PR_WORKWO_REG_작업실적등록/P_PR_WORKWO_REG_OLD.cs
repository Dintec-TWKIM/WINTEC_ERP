using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

namespace prd
{
	/// <summary>
	/// P_PR_WORKWO_REG에 대한 요약 설명입니다.
	/// 작업지시 정보 jhahn 2003.07.30 인수인계
	/// </summary>
	public class P_PR_WORKWO_REG_OLD : Duzon.Common.Forms.PageBase
	{
		#region ♣ 변수 선언  
		
		#region -> 멤버필드(일반)
        //페널
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel18;
		private Duzon.Common.Controls.PanelExt panel19;
		private Duzon.Common.Controls.PanelExt panel20;
		private Duzon.Common.Controls.PanelExt panel17;
		private Duzon.Common.Controls.PanelExt panel21;
		private Duzon.Common.Controls.PanelExt panel22;
		private Duzon.Common.Controls.PanelExt panel16;
		private Duzon.Common.Controls.PanelExt panel15;
		private Duzon.Common.Controls.PanelExt panel14;
		private Duzon.Common.Controls.PanelExt panel13;
		private Duzon.Common.Controls.PanelExt panel12;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.PanelExt panel11;
		private Duzon.Common.Controls.PanelExt panel10;
		private Duzon.Common.Controls.PanelExt panel23;
		private Duzon.Common.Controls.PanelExt m_pnlWOInfo;
		private Duzon.Common.Controls.PanelExt m_pnlWorkInfo;
		private Duzon.Common.Controls.PanelExt m_pnlTitle1;
		
		//라벨
		private Duzon.Common.Controls.LabelExt label4;
        private Duzon.Common.Controls.LabelExt label5;
		private Duzon.Common.Controls.LabelExt m_lblplant;
		private Duzon.Common.Controls.LabelExt m_lblworkitem;
		private Duzon.Common.Controls.LabelExt m_lblnowo;
		private Duzon.Common.Controls.LabelExt m_lblperiodwork;
		private Duzon.Common.Controls.LabelExt m_lbldtwork;
		private Duzon.Common.Controls.LabelExt m_lblqtwork;
		private Duzon.Common.Controls.LabelExt m_lblqtreject;
		private Duzon.Common.Controls.LabelExt m_lblfgmove;
		private Duzon.Common.Controls.LabelExt m_lblcdrsrcmach;
		private Duzon.Common.Controls.LabelExt m_lblcdrsrclabor;
		private Duzon.Common.Controls.LabelExt m_lbltmmach;
		private Duzon.Common.Controls.LabelExt m_lbltmlabor;
		private Duzon.Common.Controls.LabelExt m_lblfgisu;
		private Duzon.Common.Controls.LabelExt m_lblfgclose;
		private Duzon.Common.Controls.LabelExt m_lblEmp;
		private Duzon.Common.Controls.LabelExt m_lblStWo;
		private Duzon.Common.Controls.LabelExt m_lblTitle1;
		
		//텍스트
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem;
		private Duzon.Common.Controls.TextBoxExt m_txtUnit;
		private Duzon.Common.Controls.TextBoxExt m_txtsrclabor;
		private Duzon.Common.Controls.TextBoxExt m_txtsrcmach;
		private Duzon.Common.Controls.MaskedEditBox m_txtWoFrom;
		private Duzon.Common.Controls.MaskedEditBox m_txtWoTo;

		//버튼
		private Duzon.Common.Controls.ButtonExt m_btnnowofrom;
		private Duzon.Common.Controls.ButtonExt m_btnnowoto;
		private Duzon.Common.Controls.RoundedButton m_btndtl_isu;
		private Duzon.Common.Controls.RoundedButton m_btnhst_work;
		private Duzon.Common.Controls.RoundedButton m_btnproc_rework;
		private Duzon.Common.Controls.RoundedButton m_btnRejectDtl;
		
		//콤보
		private Duzon.Common.Controls.DropDownComboBox m_cboCdPlant;
		
		//커런시
		private Duzon.Common.Controls.CurrencyTextBox m_currqtreject;
		private Duzon.Common.Controls.CurrencyTextBox m_currqtwork;
		private Duzon.Common.Controls.CurrencyTextBox m_currtmmach;
		private Duzon.Common.Controls.CurrencyTextBox m_currtmlabor;

		//체크박스
		private Duzon.Common.Controls.CheckBoxExt m_chkfgclose;
		private Duzon.Common.Controls.CheckBoxExt m_chkfgisu;
		private Duzon.Common.Controls.CheckBoxExt m_chkfgmove;
		private Duzon.Common.Controls.CheckBoxExt m_chkNonClose;
		private Duzon.Common.Controls.CheckBoxExt m_chkClose;

		//날짜
		private Duzon.Common.Controls.DatePicker m_dtFrom;
		private Duzon.Common.Controls.DatePicker m_dtTo;
		private Duzon.Common.Controls.DatePicker m_dt;
				
		// 그리드
		private Dass.FlexGrid.FlexGrid _flexM;	//작업지시 그리드(상단)	
		private Dass.FlexGrid.FlexGrid _flexD;	//작업실적 그리드

		//텍스트
		private Duzon.Common.BpControls.BpCodeNTextBox m_txtCdItem;
		private Duzon.Common.BpControls.BpCodeTextBox m_txtNoEmp;
		
		#endregion

		#region -> 멤버필드(주요)
		
		// 데이터 테이블
		private DataTable _dtWork;
		private DataTable _dtReject = new DataTable();	//불량수량 저장을 위한 테이블

		//변수
		private bool _isPainted = false;
		private int _curRow = 0;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private PanelExt m_gridTmp;
		private System.ComponentModel.IContainer components;
		#endregion

		#endregion

		#region ♣ 생성자/소멸자
		
		#region -> 생성자
		/// <summary>
		/// 생성자
		/// </summary>
        public P_PR_WORKWO_REG_OLD()
		{
			InitializeComponent();
			this.Load += new System.EventHandler(OnPageLoad);
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
				if( components != null )
					components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PR_WORKWO_REG_OLD));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.m_txtCdItem = new Duzon.Common.BpControls.BpCodeNTextBox();
            this.m_dtTo = new Duzon.Common.Controls.DatePicker();
            this.m_dtFrom = new Duzon.Common.Controls.DatePicker();
            this.m_chkClose = new Duzon.Common.Controls.CheckBoxExt();
            this.m_chkNonClose = new Duzon.Common.Controls.CheckBoxExt();
            this.panel23 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStWo = new Duzon.Common.Controls.LabelExt();
            this.m_cboCdPlant = new Duzon.Common.Controls.DropDownComboBox();
            this.m_btnnowofrom = new Duzon.Common.Controls.ButtonExt();
            this.m_btnnowoto = new Duzon.Common.Controls.ButtonExt();
            this.m_txtWoTo = new Duzon.Common.Controls.MaskedEditBox();
            this.m_txtWoFrom = new Duzon.Common.Controls.MaskedEditBox();
            this.label5 = new Duzon.Common.Controls.LabelExt();
            this.m_txtUnit = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtStndItem = new Duzon.Common.Controls.TextBoxExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblperiodwork = new Duzon.Common.Controls.LabelExt();
            this.m_lblworkitem = new Duzon.Common.Controls.LabelExt();
            this.m_lblplant = new Duzon.Common.Controls.LabelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_lblnowo = new Duzon.Common.Controls.LabelExt();
            this.label4 = new Duzon.Common.Controls.LabelExt();
            this.m_btnproc_rework = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnhst_work = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btndtl_isu = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlWOInfo = new Duzon.Common.Controls.PanelExt();
            this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlWorkInfo = new Duzon.Common.Controls.PanelExt();
            this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_currqtreject = new Duzon.Common.Controls.CurrencyTextBox();
            this.m_currqtwork = new Duzon.Common.Controls.CurrencyTextBox();
            this.panel15 = new Duzon.Common.Controls.PanelExt();
            this.panel14 = new Duzon.Common.Controls.PanelExt();
            this.panel13 = new Duzon.Common.Controls.PanelExt();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.m_lbldtwork = new Duzon.Common.Controls.LabelExt();
            this.m_lblqtwork = new Duzon.Common.Controls.LabelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.m_lblqtreject = new Duzon.Common.Controls.LabelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.m_txtNoEmp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_dt = new Duzon.Common.Controls.DatePicker();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.m_lblEmp = new Duzon.Common.Controls.LabelExt();
            this.m_chkfgclose = new Duzon.Common.Controls.CheckBoxExt();
            this.m_chkfgisu = new Duzon.Common.Controls.CheckBoxExt();
            this.m_chkfgmove = new Duzon.Common.Controls.CheckBoxExt();
            this.m_currtmmach = new Duzon.Common.Controls.CurrencyTextBox();
            this.m_currtmlabor = new Duzon.Common.Controls.CurrencyTextBox();
            this.panel18 = new Duzon.Common.Controls.PanelExt();
            this.panel19 = new Duzon.Common.Controls.PanelExt();
            this.panel20 = new Duzon.Common.Controls.PanelExt();
            this.m_lblfgmove = new Duzon.Common.Controls.LabelExt();
            this.m_lblcdrsrcmach = new Duzon.Common.Controls.LabelExt();
            this.m_lblcdrsrclabor = new Duzon.Common.Controls.LabelExt();
            this.panel17 = new Duzon.Common.Controls.PanelExt();
            this.m_lbltmmach = new Duzon.Common.Controls.LabelExt();
            this.m_lbltmlabor = new Duzon.Common.Controls.LabelExt();
            this.panel21 = new Duzon.Common.Controls.PanelExt();
            this.m_lblfgisu = new Duzon.Common.Controls.LabelExt();
            this.panel22 = new Duzon.Common.Controls.PanelExt();
            this.m_lblfgclose = new Duzon.Common.Controls.LabelExt();
            this.panel16 = new Duzon.Common.Controls.PanelExt();
            this.m_txtsrcmach = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtsrclabor = new Duzon.Common.Controls.TextBoxExt();
            this.m_btnRejectDtl = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlTitle1 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTitle1 = new Duzon.Common.Controls.LabelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_gridTmp = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtFrom)).BeginInit();
            this.panel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWoTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWoFrom)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.m_pnlWOInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
            this.m_pnlWorkInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_currqtreject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_currqtwork)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dt)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_currtmmach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_currtmlabor)).BeginInit();
            this.panel20.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel16.SuspendLayout();
            this.m_pnlTitle1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_gridTmp.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.m_txtCdItem);
            this.panel4.Controls.Add(this.m_dtTo);
            this.panel4.Controls.Add(this.m_dtFrom);
            this.panel4.Controls.Add(this.m_chkClose);
            this.panel4.Controls.Add(this.m_chkNonClose);
            this.panel4.Controls.Add(this.panel23);
            this.panel4.Controls.Add(this.m_cboCdPlant);
            this.panel4.Controls.Add(this.m_btnnowofrom);
            this.panel4.Controls.Add(this.m_btnnowoto);
            this.panel4.Controls.Add(this.m_txtWoTo);
            this.panel4.Controls.Add(this.m_txtWoFrom);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.m_txtUnit);
            this.panel4.Controls.Add(this.m_txtStndItem);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 79);
            this.panel4.TabIndex = 128;
            // 
            // m_txtCdItem
            // 
            this.m_txtCdItem.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtCdItem.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtCdItem.ButtonImage")));
            this.m_txtCdItem.ChildMode = "";
            this.m_txtCdItem.CodeName = "";
            this.m_txtCdItem.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtCdItem.CodeValue = "";
            this.m_txtCdItem.ComboCheck = true;
            this.m_txtCdItem.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.m_txtCdItem.ItemBackColor = System.Drawing.Color.White;
            this.m_txtCdItem.Location = new System.Drawing.Point(108, 28);
            this.m_txtCdItem.Name = "m_txtCdItem";
            this.m_txtCdItem.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtCdItem.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.m_txtCdItem.SearchCode = true;
            this.m_txtCdItem.SelectCount = 0;
            this.m_txtCdItem.SetDefaultValue = false;
            this.m_txtCdItem.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtCdItem.Size = new System.Drawing.Size(328, 21);
            this.m_txtCdItem.TabIndex = 2;
            this.m_txtCdItem.TabStop = false;
            this.m_txtCdItem.CodeChanged += new System.EventHandler(this.OnBpControl_CodeChanged);
            this.m_txtCdItem.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            this.m_txtCdItem.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
            // 
            // m_dtTo
            // 
            this.m_dtTo.CalendarBackColor = System.Drawing.Color.White;
            this.m_dtTo.DayColor = System.Drawing.Color.Black;
            this.m_dtTo.FriDayColor = System.Drawing.Color.Blue;
            this.m_dtTo.Location = new System.Drawing.Point(223, 54);
            this.m_dtTo.Mask = "####/##/##";
            this.m_dtTo.MaskBackColor = System.Drawing.Color.White;
            this.m_dtTo.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_dtTo.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_dtTo.Modified = false;
            this.m_dtTo.Name = "m_dtTo";
            this.m_dtTo.PaddingCharacter = '_';
            this.m_dtTo.PassivePromptCharacter = '_';
            this.m_dtTo.PromptCharacter = '_';
            this.m_dtTo.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_dtTo.ShowToDay = true;
            this.m_dtTo.ShowTodayCircle = true;
            this.m_dtTo.ShowUpDown = false;
            this.m_dtTo.Size = new System.Drawing.Size(92, 21);
            this.m_dtTo.SunDayColor = System.Drawing.Color.Red;
            this.m_dtTo.TabIndex = 4;
            this.m_dtTo.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_dtTo.TitleForeColor = System.Drawing.Color.White;
            this.m_dtTo.ToDayColor = System.Drawing.Color.Red;
            this.m_dtTo.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_dtTo.UseKeyF3 = false;
            this.m_dtTo.Value = new System.DateTime(((long)(0)));
            this.m_dtTo.Validated += new System.EventHandler(this.ControlDtValidated);
            // 
            // m_dtFrom
            // 
            this.m_dtFrom.CalendarBackColor = System.Drawing.Color.White;
            this.m_dtFrom.DayColor = System.Drawing.Color.Black;
            this.m_dtFrom.FriDayColor = System.Drawing.Color.Blue;
            this.m_dtFrom.Location = new System.Drawing.Point(109, 54);
            this.m_dtFrom.Mask = "####/##/##";
            this.m_dtFrom.MaskBackColor = System.Drawing.Color.White;
            this.m_dtFrom.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_dtFrom.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_dtFrom.Modified = false;
            this.m_dtFrom.Name = "m_dtFrom";
            this.m_dtFrom.PaddingCharacter = '_';
            this.m_dtFrom.PassivePromptCharacter = '_';
            this.m_dtFrom.PromptCharacter = '_';
            this.m_dtFrom.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_dtFrom.ShowToDay = true;
            this.m_dtFrom.ShowTodayCircle = true;
            this.m_dtFrom.ShowUpDown = false;
            this.m_dtFrom.Size = new System.Drawing.Size(92, 21);
            this.m_dtFrom.SunDayColor = System.Drawing.Color.Red;
            this.m_dtFrom.TabIndex = 3;
            this.m_dtFrom.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_dtFrom.TitleForeColor = System.Drawing.Color.White;
            this.m_dtFrom.ToDayColor = System.Drawing.Color.Red;
            this.m_dtFrom.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_dtFrom.UseKeyF3 = false;
            this.m_dtFrom.Value = new System.DateTime(((long)(0)));
            this.m_dtFrom.Validated += new System.EventHandler(this.ControlDtValidated);
            // 
            // m_chkClose
            // 
            this.m_chkClose.Location = new System.Drawing.Point(569, 3);
            this.m_chkClose.Name = "m_chkClose";
            this.m_chkClose.Size = new System.Drawing.Size(71, 22);
            this.m_chkClose.TabIndex = 143;
            this.m_chkClose.Tag = "CLOSE";
            this.m_chkClose.Text = "마감";
            this.m_chkClose.UseKeyEnter = true;
            // 
            // m_chkNonClose
            // 
            this.m_chkNonClose.Checked = true;
            this.m_chkNonClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkNonClose.Location = new System.Drawing.Point(499, 3);
            this.m_chkNonClose.Name = "m_chkNonClose";
            this.m_chkNonClose.Size = new System.Drawing.Size(71, 22);
            this.m_chkNonClose.TabIndex = 142;
            this.m_chkNonClose.Tag = "NON_CLOSE";
            this.m_chkNonClose.Text = "미마감";
            this.m_chkNonClose.UseKeyEnter = true;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel23.Controls.Add(this.m_lblStWo);
            this.panel23.Location = new System.Drawing.Point(391, 1);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(103, 24);
            this.panel23.TabIndex = 141;
            // 
            // m_lblStWo
            // 
            this.m_lblStWo.Location = new System.Drawing.Point(3, 5);
            this.m_lblStWo.Name = "m_lblStWo";
            this.m_lblStWo.Resizeble = true;
            this.m_lblStWo.Size = new System.Drawing.Size(98, 18);
            this.m_lblStWo.TabIndex = 1;
            this.m_lblStWo.Tag = "ST_WO";
            this.m_lblStWo.Text = "Status";
            this.m_lblStWo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cboCdPlant
            // 
            this.m_cboCdPlant.AutoDropDown = false;
            this.m_cboCdPlant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboCdPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCdPlant.Location = new System.Drawing.Point(109, 4);
            this.m_cboCdPlant.Name = "m_cboCdPlant";
            this.m_cboCdPlant.ShowCheckBox = false;
            this.m_cboCdPlant.Size = new System.Drawing.Size(231, 20);
            this.m_cboCdPlant.TabIndex = 1;
            this.m_cboCdPlant.UseKeyEnter = true;
            this.m_cboCdPlant.UseKeyF3 = false;
            // 
            // m_btnnowofrom
            // 
            this.m_btnnowofrom.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnnowofrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnnowofrom.Image = ((System.Drawing.Image)(resources.GetObject("m_btnnowofrom.Image")));
            this.m_btnnowofrom.Location = new System.Drawing.Point(598, 54);
            this.m_btnnowofrom.Name = "m_btnnowofrom";
            this.m_btnnowofrom.Size = new System.Drawing.Size(30, 21);
            this.m_btnnowofrom.TabIndex = 0;
            this.m_btnnowofrom.TabStop = false;
            this.m_btnnowofrom.UseVisualStyleBackColor = false;
            this.m_btnnowofrom.Click += new System.EventHandler(this.OnControlClick);
            // 
            // m_btnnowoto
            // 
            this.m_btnnowoto.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnnowoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnnowoto.Image = ((System.Drawing.Image)(resources.GetObject("m_btnnowoto.Image")));
            this.m_btnnowoto.Location = new System.Drawing.Point(749, 54);
            this.m_btnnowoto.Name = "m_btnnowoto";
            this.m_btnnowoto.Size = new System.Drawing.Size(30, 21);
            this.m_btnnowoto.TabIndex = 0;
            this.m_btnnowoto.TabStop = false;
            this.m_btnnowoto.UseVisualStyleBackColor = false;
            this.m_btnnowoto.Click += new System.EventHandler(this.OnControlClick);
            // 
            // m_txtWoTo
            // 
            this.m_txtWoTo.AccessibleDescription = "MaskedEdit TextBox";
            this.m_txtWoTo.AccessibleName = "MaskedEditBox";
            this.m_txtWoTo.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.m_txtWoTo.AllowPrompt = false;
            this.m_txtWoTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtWoTo.ClipMode = Duzon.Common.Controls.ClipModes.IncludeLiterals;
            this.m_txtWoTo.Culture = new System.Globalization.CultureInfo("ko-KR");
            this.m_txtWoTo.DateSeparator = '/';
            this.m_txtWoTo.Location = new System.Drawing.Point(649, 54);
            this.m_txtWoTo.MaxLength = 0;
            this.m_txtWoTo.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_txtWoTo.Name = "m_txtWoTo";
            this.m_txtWoTo.PaddingCharacterInt = 95;
            this.m_txtWoTo.PassivePromptCharacterInt = 95;
            this.m_txtWoTo.PromptCharacterInt = 95;
            this.m_txtWoTo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtWoTo.Size = new System.Drawing.Size(100, 21);
            this.m_txtWoTo.SpecialCultureValue = Duzon.Common.Controls.SpecialCultureValues.None;
            this.m_txtWoTo.TabIndex = 6;
            this.m_txtWoTo.UseKeyEnter = true;
            this.m_txtWoTo.UseKeyF3 = false;
            this.m_txtWoTo.UseLocaleDefault = false;
            this.m_txtWoTo.UseUserOverride = true;
            this.m_txtWoTo.Validating += new System.ComponentModel.CancelEventHandler(this.OnControlValidating);
            this.m_txtWoTo.DoubleClick += new System.EventHandler(this.OnControlDoubleClick);
            this.m_txtWoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // m_txtWoFrom
            // 
            this.m_txtWoFrom.AccessibleDescription = "MaskedEdit TextBox";
            this.m_txtWoFrom.AccessibleName = "MaskedEditBox";
            this.m_txtWoFrom.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.m_txtWoFrom.AllowPrompt = false;
            this.m_txtWoFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtWoFrom.ClipMode = Duzon.Common.Controls.ClipModes.IncludeLiterals;
            this.m_txtWoFrom.Culture = new System.Globalization.CultureInfo("ko-KR");
            this.m_txtWoFrom.DateSeparator = '/';
            this.m_txtWoFrom.Location = new System.Drawing.Point(498, 54);
            this.m_txtWoFrom.MaxLength = 0;
            this.m_txtWoFrom.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_txtWoFrom.Name = "m_txtWoFrom";
            this.m_txtWoFrom.PaddingCharacterInt = 95;
            this.m_txtWoFrom.PassivePromptCharacterInt = 95;
            this.m_txtWoFrom.PromptCharacterInt = 95;
            this.m_txtWoFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtWoFrom.Size = new System.Drawing.Size(100, 21);
            this.m_txtWoFrom.SpecialCultureValue = Duzon.Common.Controls.SpecialCultureValues.None;
            this.m_txtWoFrom.TabIndex = 5;
            this.m_txtWoFrom.UseKeyEnter = true;
            this.m_txtWoFrom.UseKeyF3 = false;
            this.m_txtWoFrom.UseLocaleDefault = false;
            this.m_txtWoFrom.UseUserOverride = true;
            this.m_txtWoFrom.Validating += new System.ComponentModel.CancelEventHandler(this.OnControlValidating);
            this.m_txtWoFrom.DoubleClick += new System.EventHandler(this.OnControlDoubleClick);
            this.m_txtWoFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(629, 55);
            this.label5.Name = "label5";
            this.label5.Resizeble = true;
            this.label5.Size = new System.Drawing.Size(18, 18);
            this.label5.TabIndex = 140;
            this.label5.Text = "~";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtUnit
            // 
            this.m_txtUnit.Location = new System.Drawing.Point(638, 28);
            this.m_txtUnit.Name = "m_txtUnit";
            this.m_txtUnit.ReadOnly = true;
            this.m_txtUnit.SelectedAllEnabled = false;
            this.m_txtUnit.Size = new System.Drawing.Size(142, 21);
            this.m_txtUnit.TabIndex = 0;
            this.m_txtUnit.TabStop = false;
            this.m_txtUnit.UseKeyEnter = true;
            this.m_txtUnit.UseKeyF3 = true;
            // 
            // m_txtStndItem
            // 
            this.m_txtStndItem.Location = new System.Drawing.Point(438, 28);
            this.m_txtStndItem.Name = "m_txtStndItem";
            this.m_txtStndItem.ReadOnly = true;
            this.m_txtStndItem.SelectedAllEnabled = false;
            this.m_txtStndItem.Size = new System.Drawing.Size(200, 21);
            this.m_txtStndItem.TabIndex = 0;
            this.m_txtStndItem.TabStop = false;
            this.m_txtStndItem.UseKeyEnter = true;
            this.m_txtStndItem.UseKeyF3 = true;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Location = new System.Drawing.Point(5, 50);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(777, 1);
            this.panel8.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Location = new System.Drawing.Point(5, 25);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(777, 1);
            this.panel7.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.m_lblperiodwork);
            this.panel5.Controls.Add(this.m_lblworkitem);
            this.panel5.Controls.Add(this.m_lblplant);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(103, 75);
            this.panel5.TabIndex = 0;
            // 
            // m_lblperiodwork
            // 
            this.m_lblperiodwork.Location = new System.Drawing.Point(3, 55);
            this.m_lblperiodwork.Name = "m_lblperiodwork";
            this.m_lblperiodwork.Resizeble = true;
            this.m_lblperiodwork.Size = new System.Drawing.Size(98, 18);
            this.m_lblperiodwork.TabIndex = 3;
            this.m_lblperiodwork.Text = "작업기간";
            this.m_lblperiodwork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblworkitem
            // 
            this.m_lblworkitem.Location = new System.Drawing.Point(3, 30);
            this.m_lblworkitem.Name = "m_lblworkitem";
            this.m_lblworkitem.Resizeble = true;
            this.m_lblworkitem.Size = new System.Drawing.Size(98, 18);
            this.m_lblworkitem.TabIndex = 2;
            this.m_lblworkitem.Text = "작업품목";
            this.m_lblworkitem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblplant
            // 
            this.m_lblplant.Location = new System.Drawing.Point(3, 5);
            this.m_lblplant.Name = "m_lblplant";
            this.m_lblplant.Resizeble = true;
            this.m_lblplant.Size = new System.Drawing.Size(98, 18);
            this.m_lblplant.TabIndex = 1;
            this.m_lblplant.Text = "공장";
            this.m_lblplant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.m_lblnowo);
            this.panel6.Location = new System.Drawing.Point(391, 50);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(103, 26);
            this.panel6.TabIndex = 1;
            // 
            // m_lblnowo
            // 
            this.m_lblnowo.Location = new System.Drawing.Point(3, 5);
            this.m_lblnowo.Name = "m_lblnowo";
            this.m_lblnowo.Resizeble = true;
            this.m_lblnowo.Size = new System.Drawing.Size(98, 18);
            this.m_lblnowo.TabIndex = 1;
            this.m_lblnowo.Text = "작업지시번호";
            this.m_lblnowo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(201, 55);
            this.label4.Name = "label4";
            this.label4.Resizeble = true;
            this.label4.Size = new System.Drawing.Size(18, 18);
            this.label4.TabIndex = 129;
            this.label4.Text = "~";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnproc_rework
            // 
            this.m_btnproc_rework.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnproc_rework.BackColor = System.Drawing.Color.White;
            this.m_btnproc_rework.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnproc_rework.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnproc_rework.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnproc_rework.Location = new System.Drawing.Point(579, 0);
            this.m_btnproc_rework.Name = "m_btnproc_rework";
            this.m_btnproc_rework.Size = new System.Drawing.Size(120, 24);
            this.m_btnproc_rework.TabIndex = 0;
            this.m_btnproc_rework.TabStop = false;
            this.m_btnproc_rework.Text = "공정재작업처리";
            this.m_btnproc_rework.UseVisualStyleBackColor = false;
            this.m_btnproc_rework.Click += new System.EventHandler(this.m_btnproc_rework_Click);
            // 
            // m_btnhst_work
            // 
            this.m_btnhst_work.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnhst_work.BackColor = System.Drawing.Color.White;
            this.m_btnhst_work.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnhst_work.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnhst_work.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnhst_work.Location = new System.Drawing.Point(499, 0);
            this.m_btnhst_work.Name = "m_btnhst_work";
            this.m_btnhst_work.Size = new System.Drawing.Size(80, 24);
            this.m_btnhst_work.TabIndex = 0;
            this.m_btnhst_work.TabStop = false;
            this.m_btnhst_work.Text = "실적이력";
            this.m_btnhst_work.UseVisualStyleBackColor = false;
            this.m_btnhst_work.Click += new System.EventHandler(this.m_btnhst_work_Click);
            // 
            // m_btndtl_isu
            // 
            this.m_btndtl_isu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btndtl_isu.BackColor = System.Drawing.Color.White;
            this.m_btndtl_isu.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btndtl_isu.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btndtl_isu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btndtl_isu.Location = new System.Drawing.Point(419, 0);
            this.m_btndtl_isu.Name = "m_btndtl_isu";
            this.m_btndtl_isu.Size = new System.Drawing.Size(80, 24);
            this.m_btndtl_isu.TabIndex = 0;
            this.m_btndtl_isu.TabStop = false;
            this.m_btndtl_isu.Text = "투입상세";
            this.m_btndtl_isu.UseVisualStyleBackColor = false;
            this.m_btndtl_isu.Click += new System.EventHandler(this.m_btndtl_isu_Click);
            // 
            // m_pnlWOInfo
            // 
            this.m_pnlWOInfo.Controls.Add(this._flexM);
            this.m_pnlWOInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlWOInfo.Location = new System.Drawing.Point(3, 118);
            this.m_pnlWOInfo.Name = "m_pnlWOInfo";
            this.m_pnlWOInfo.Size = new System.Drawing.Size(787, 261);
            this.m_pnlWOInfo.TabIndex = 132;
            // 
            // _flexM
            // 
            this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM.AutoResize = false;
            this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM.EnabledHeaderCheck = true;
            this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM.Location = new System.Drawing.Point(0, 0);
            this._flexM.Name = "_flexM";
            this._flexM.Rows.Count = 1;
            this._flexM.Rows.DefaultSize = 20;
            this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM.ShowSort = false;
            this._flexM.Size = new System.Drawing.Size(787, 261);
            this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
            this._flexM.TabIndex = 7;
            // 
            // m_pnlWorkInfo
            // 
            this.m_pnlWorkInfo.BackColor = System.Drawing.Color.White;
            this.m_pnlWorkInfo.Controls.Add(this._flexD);
            this.m_pnlWorkInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlWorkInfo.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_pnlWorkInfo.Location = new System.Drawing.Point(3, 3);
            this.m_pnlWorkInfo.Name = "m_pnlWorkInfo";
            this.tableLayoutPanel2.SetRowSpan(this.m_pnlWorkInfo, 3);
            this.m_pnlWorkInfo.Size = new System.Drawing.Size(415, 167);
            this.m_pnlWorkInfo.TabIndex = 133;
            // 
            // _flexD
            // 
            this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD.AutoResize = false;
            this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD.EnabledHeaderCheck = true;
            this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD.Location = new System.Drawing.Point(0, 0);
            this._flexD.Name = "_flexD";
            this._flexD.Rows.Count = 1;
            this._flexD.Rows.DefaultSize = 18;
            this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD.ShowSort = false;
            this._flexD.Size = new System.Drawing.Size(415, 167);
            this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
            this._flexD.TabIndex = 7;
            // 
            // m_currqtreject
            // 
            this.m_currqtreject.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_currqtreject.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_currqtreject.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_currqtreject.Location = new System.Drawing.Point(255, 29);
            this.m_currqtreject.Mask = null;
            this.m_currqtreject.MaxLength = 9;
            this.m_currqtreject.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.m_currqtreject.MinValue = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.m_currqtreject.Name = "m_currqtreject";
            this.m_currqtreject.NullString = "0";
            this.m_currqtreject.PositiveColor = System.Drawing.Color.Black;
            this.m_currqtreject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_currqtreject.Size = new System.Drawing.Size(99, 21);
            this.m_currqtreject.TabIndex = 10;
            this.m_currqtreject.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_currqtreject.UseKeyEnter = true;
            this.m_currqtreject.UseKeyF3 = false;
            this.m_currqtreject.WordWrap = false;
            this.m_currqtreject.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            this.m_currqtreject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // m_currqtwork
            // 
            this.m_currqtwork.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_currqtwork.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_currqtwork.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_currqtwork.Location = new System.Drawing.Point(86, 29);
            this.m_currqtwork.Mask = null;
            this.m_currqtwork.MaxLength = 9;
            this.m_currqtwork.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.m_currqtwork.MinValue = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.m_currqtwork.Name = "m_currqtwork";
            this.m_currqtwork.NullString = "0";
            this.m_currqtwork.PositiveColor = System.Drawing.Color.Black;
            this.m_currqtwork.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_currqtwork.Size = new System.Drawing.Size(91, 21);
            this.m_currqtwork.TabIndex = 9;
            this.m_currqtwork.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_currqtwork.UseKeyEnter = true;
            this.m_currqtwork.UseKeyF3 = false;
            this.m_currqtwork.WordWrap = false;
            this.m_currqtwork.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            this.m_currqtwork.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel15.BackgroundImage")));
            this.panel15.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel15.Location = new System.Drawing.Point(5, 76);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(350, 1);
            this.panel15.TabIndex = 5;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel14.BackgroundImage")));
            this.panel14.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel14.Location = new System.Drawing.Point(5, 51);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(350, 1);
            this.panel14.TabIndex = 4;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel13.BackgroundImage")));
            this.panel13.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel13.Location = new System.Drawing.Point(5, 26);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(350, 1);
            this.panel13.TabIndex = 3;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel12.Controls.Add(this.m_lbldtwork);
            this.panel12.Controls.Add(this.m_lblqtwork);
            this.panel12.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel12.Location = new System.Drawing.Point(1, 1);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(80, 52);
            this.panel12.TabIndex = 1;
            // 
            // m_lbldtwork
            // 
            this.m_lbldtwork.Location = new System.Drawing.Point(3, 5);
            this.m_lbldtwork.Name = "m_lbldtwork";
            this.m_lbldtwork.Resizeble = true;
            this.m_lbldtwork.Size = new System.Drawing.Size(75, 18);
            this.m_lbldtwork.TabIndex = 5;
            this.m_lbldtwork.Tag = "DT_WORK";
            this.m_lbldtwork.Text = "작업일자";
            this.m_lbldtwork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblqtwork
            // 
            this.m_lblqtwork.Location = new System.Drawing.Point(3, 31);
            this.m_lblqtwork.Name = "m_lblqtwork";
            this.m_lblqtwork.Resizeble = true;
            this.m_lblqtwork.Size = new System.Drawing.Size(75, 18);
            this.m_lblqtwork.TabIndex = 1;
            this.m_lblqtwork.Text = "실적수량";
            this.m_lblqtwork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel9.Controls.Add(this.m_lblqtreject);
            this.panel9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel9.Location = new System.Drawing.Point(183, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(68, 26);
            this.panel9.TabIndex = 148;
            // 
            // m_lblqtreject
            // 
            this.m_lblqtreject.Location = new System.Drawing.Point(3, 5);
            this.m_lblqtreject.Name = "m_lblqtreject";
            this.m_lblqtreject.Resizeble = true;
            this.m_lblqtreject.Size = new System.Drawing.Size(64, 18);
            this.m_lblqtreject.TabIndex = 2;
            this.m_lblqtreject.Text = "불량수량";
            this.m_lblqtreject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.m_txtNoEmp);
            this.panel11.Controls.Add(this.m_dt);
            this.panel11.Controls.Add(this.panel10);
            this.panel11.Controls.Add(this.m_currqtreject);
            this.panel11.Controls.Add(this.m_currqtwork);
            this.panel11.Controls.Add(this.panel15);
            this.panel11.Controls.Add(this.panel14);
            this.panel11.Controls.Add(this.panel13);
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Controls.Add(this.panel9);
            this.panel11.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel11.Location = new System.Drawing.Point(424, 32);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(360, 54);
            this.panel11.TabIndex = 134;
            // 
            // m_txtNoEmp
            // 
            this.m_txtNoEmp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtNoEmp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtNoEmp.ButtonImage")));
            this.m_txtNoEmp.ChildMode = "";
            this.m_txtNoEmp.CodeName = "";
            this.m_txtNoEmp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtNoEmp.CodeValue = "";
            this.m_txtNoEmp.ComboCheck = true;
            this.m_txtNoEmp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.m_txtNoEmp.ItemBackColor = System.Drawing.Color.White;
            this.m_txtNoEmp.Location = new System.Drawing.Point(255, 4);
            this.m_txtNoEmp.Name = "m_txtNoEmp";
            this.m_txtNoEmp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtNoEmp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.m_txtNoEmp.SearchCode = true;
            this.m_txtNoEmp.SelectCount = 0;
            this.m_txtNoEmp.SetDefaultValue = false;
            this.m_txtNoEmp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtNoEmp.Size = new System.Drawing.Size(100, 21);
            this.m_txtNoEmp.TabIndex = 8;
            this.m_txtNoEmp.TabStop = false;
            // 
            // m_dt
            // 
            this.m_dt.CalendarBackColor = System.Drawing.Color.White;
            this.m_dt.DayColor = System.Drawing.Color.Black;
            this.m_dt.FriDayColor = System.Drawing.Color.Blue;
            this.m_dt.Location = new System.Drawing.Point(87, 4);
            this.m_dt.Mask = "####/##/##";
            this.m_dt.MaskBackColor = System.Drawing.Color.White;
            this.m_dt.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_dt.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_dt.Modified = false;
            this.m_dt.Name = "m_dt";
            this.m_dt.PaddingCharacter = '_';
            this.m_dt.PassivePromptCharacter = '_';
            this.m_dt.PromptCharacter = '_';
            this.m_dt.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_dt.ShowToDay = true;
            this.m_dt.ShowTodayCircle = true;
            this.m_dt.ShowUpDown = false;
            this.m_dt.Size = new System.Drawing.Size(92, 21);
            this.m_dt.SunDayColor = System.Drawing.Color.Red;
            this.m_dt.TabIndex = 7;
            this.m_dt.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_dt.TitleForeColor = System.Drawing.Color.White;
            this.m_dt.ToDayColor = System.Drawing.Color.Red;
            this.m_dt.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_dt.UseKeyF3 = false;
            this.m_dt.Value = new System.DateTime(((long)(0)));
            this.m_dt.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel10.Controls.Add(this.m_lblEmp);
            this.panel10.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel10.Location = new System.Drawing.Point(183, 1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(69, 26);
            this.panel10.TabIndex = 149;
            // 
            // m_lblEmp
            // 
            this.m_lblEmp.Location = new System.Drawing.Point(10, 5);
            this.m_lblEmp.Name = "m_lblEmp";
            this.m_lblEmp.Resizeble = true;
            this.m_lblEmp.Size = new System.Drawing.Size(56, 18);
            this.m_lblEmp.TabIndex = 2;
            this.m_lblEmp.Text = "담당자";
            this.m_lblEmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_chkfgclose
            // 
            this.m_chkfgclose.BackColor = System.Drawing.Color.White;
            this.m_chkfgclose.Enabled = false;
            this.m_chkfgclose.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_chkfgclose.Location = new System.Drawing.Point(319, 58);
            this.m_chkfgclose.Name = "m_chkfgclose";
            this.m_chkfgclose.Size = new System.Drawing.Size(22, 16);
            this.m_chkfgclose.TabIndex = 17;
            this.m_chkfgclose.UseKeyEnter = true;
            this.m_chkfgclose.UseVisualStyleBackColor = false;
            this.m_chkfgclose.CheckedChanged += new System.EventHandler(this.ControlCheckdChange);
            // 
            // m_chkfgisu
            // 
            this.m_chkfgisu.BackColor = System.Drawing.Color.White;
            this.m_chkfgisu.Checked = true;
            this.m_chkfgisu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkfgisu.Enabled = false;
            this.m_chkfgisu.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_chkfgisu.Location = new System.Drawing.Point(203, 58);
            this.m_chkfgisu.Name = "m_chkfgisu";
            this.m_chkfgisu.Size = new System.Drawing.Size(22, 16);
            this.m_chkfgisu.TabIndex = 16;
            this.m_chkfgisu.UseKeyEnter = true;
            this.m_chkfgisu.UseVisualStyleBackColor = false;
            this.m_chkfgisu.CheckedChanged += new System.EventHandler(this.ControlCheckdChange);
            // 
            // m_chkfgmove
            // 
            this.m_chkfgmove.BackColor = System.Drawing.Color.White;
            this.m_chkfgmove.Checked = true;
            this.m_chkfgmove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkfgmove.Enabled = false;
            this.m_chkfgmove.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_chkfgmove.Location = new System.Drawing.Point(86, 58);
            this.m_chkfgmove.Name = "m_chkfgmove";
            this.m_chkfgmove.Size = new System.Drawing.Size(22, 16);
            this.m_chkfgmove.TabIndex = 15;
            this.m_chkfgmove.UseKeyEnter = true;
            this.m_chkfgmove.UseVisualStyleBackColor = false;
            this.m_chkfgmove.CheckedChanged += new System.EventHandler(this.ControlCheckdChange);
            // 
            // m_currtmmach
            // 
            this.m_currtmmach.CurrencyGroupSeparator = "";
            this.m_currtmmach.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_currtmmach.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_currtmmach.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_currtmmach.Location = new System.Drawing.Point(263, 29);
            this.m_currtmmach.Mask = null;
            this.m_currtmmach.MaxLength = 6;
            this.m_currtmmach.MaxValue = new decimal(new int[] {
            236060,
            0,
            0,
            0});
            this.m_currtmmach.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_currtmmach.Name = "m_currtmmach";
            this.m_currtmmach.NullString = "0";
            this.m_currtmmach.PositiveColor = System.Drawing.Color.Black;
            this.m_currtmmach.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_currtmmach.Size = new System.Drawing.Size(91, 21);
            this.m_currtmmach.TabIndex = 14;
            this.m_currtmmach.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_currtmmach.UseKeyEnter = true;
            this.m_currtmmach.UseKeyF3 = false;
            this.m_currtmmach.WordWrap = false;
            this.m_currtmmach.Validating += new System.ComponentModel.CancelEventHandler(this.OnControlValidating);
            this.m_currtmmach.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            this.m_currtmmach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDownEnd);
            // 
            // m_currtmlabor
            // 
            this.m_currtmlabor.CurrencyGroupSeparator = "";
            this.m_currtmlabor.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_currtmlabor.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_currtmlabor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_currtmlabor.Location = new System.Drawing.Point(263, 3);
            this.m_currtmlabor.Mask = null;
            this.m_currtmlabor.MaxLength = 6;
            this.m_currtmlabor.MaxValue = new decimal(new int[] {
            236060,
            0,
            0,
            0});
            this.m_currtmlabor.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_currtmlabor.Name = "m_currtmlabor";
            this.m_currtmlabor.NullString = "0";
            this.m_currtmlabor.PositiveColor = System.Drawing.Color.Black;
            this.m_currtmlabor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_currtmlabor.Size = new System.Drawing.Size(91, 21);
            this.m_currtmlabor.TabIndex = 12;
            this.m_currtmlabor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_currtmlabor.UseKeyEnter = true;
            this.m_currtmlabor.UseKeyF3 = false;
            this.m_currtmlabor.WordWrap = false;
            this.m_currtmlabor.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            this.m_currtmlabor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel18.BackgroundImage")));
            this.panel18.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel18.Location = new System.Drawing.Point(5, 51);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(380, 1);
            this.panel18.TabIndex = 4;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel19.BackgroundImage")));
            this.panel19.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel19.Location = new System.Drawing.Point(5, 26);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(380, 1);
            this.panel19.TabIndex = 3;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel20.Controls.Add(this.m_lblfgmove);
            this.panel20.Controls.Add(this.m_lblcdrsrcmach);
            this.panel20.Controls.Add(this.m_lblcdrsrclabor);
            this.panel20.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel20.Location = new System.Drawing.Point(1, 1);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(80, 75);
            this.panel20.TabIndex = 1;
            // 
            // m_lblfgmove
            // 
            this.m_lblfgmove.Location = new System.Drawing.Point(3, 55);
            this.m_lblfgmove.Name = "m_lblfgmove";
            this.m_lblfgmove.Resizeble = true;
            this.m_lblfgmove.Size = new System.Drawing.Size(75, 18);
            this.m_lblfgmove.TabIndex = 3;
            this.m_lblfgmove.Text = "공정이동";
            this.m_lblfgmove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblcdrsrcmach
            // 
            this.m_lblcdrsrcmach.Location = new System.Drawing.Point(3, 30);
            this.m_lblcdrsrcmach.Name = "m_lblcdrsrcmach";
            this.m_lblcdrsrcmach.Resizeble = true;
            this.m_lblcdrsrcmach.Size = new System.Drawing.Size(75, 18);
            this.m_lblcdrsrcmach.TabIndex = 2;
            this.m_lblcdrsrcmach.Text = "기계자원";
            this.m_lblcdrsrcmach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblcdrsrclabor
            // 
            this.m_lblcdrsrclabor.Location = new System.Drawing.Point(3, 5);
            this.m_lblcdrsrclabor.Name = "m_lblcdrsrclabor";
            this.m_lblcdrsrclabor.Resizeble = true;
            this.m_lblcdrsrclabor.Size = new System.Drawing.Size(75, 18);
            this.m_lblcdrsrclabor.TabIndex = 1;
            this.m_lblcdrsrclabor.Text = "인원자원";
            this.m_lblcdrsrclabor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel17.Controls.Add(this.m_lbltmmach);
            this.panel17.Controls.Add(this.m_lbltmlabor);
            this.panel17.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel17.Location = new System.Drawing.Point(164, 1);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(95, 51);
            this.panel17.TabIndex = 8;
            // 
            // m_lbltmmach
            // 
            this.m_lbltmmach.Location = new System.Drawing.Point(3, 30);
            this.m_lbltmmach.Name = "m_lbltmmach";
            this.m_lbltmmach.Resizeble = true;
            this.m_lbltmmach.Size = new System.Drawing.Size(90, 18);
            this.m_lbltmmach.TabIndex = 2;
            this.m_lbltmmach.Text = "기계시간";
            this.m_lbltmmach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lbltmlabor
            // 
            this.m_lbltmlabor.Location = new System.Drawing.Point(3, 5);
            this.m_lbltmlabor.Name = "m_lbltmlabor";
            this.m_lbltmlabor.Resizeble = true;
            this.m_lbltmlabor.Size = new System.Drawing.Size(89, 18);
            this.m_lbltmlabor.TabIndex = 1;
            this.m_lbltmlabor.Text = "인원시간";
            this.m_lbltmlabor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel21.Controls.Add(this.m_lblfgisu);
            this.panel21.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel21.Location = new System.Drawing.Point(128, 51);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(70, 25);
            this.panel21.TabIndex = 11;
            // 
            // m_lblfgisu
            // 
            this.m_lblfgisu.Location = new System.Drawing.Point(3, 5);
            this.m_lblfgisu.Name = "m_lblfgisu";
            this.m_lblfgisu.Resizeble = true;
            this.m_lblfgisu.Size = new System.Drawing.Size(65, 18);
            this.m_lblfgisu.TabIndex = 1;
            this.m_lblfgisu.Text = "자재투입";
            this.m_lblfgisu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel22.Controls.Add(this.m_lblfgclose);
            this.panel22.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel22.Location = new System.Drawing.Point(244, 51);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(70, 25);
            this.panel22.TabIndex = 12;
            // 
            // m_lblfgclose
            // 
            this.m_lblfgclose.Location = new System.Drawing.Point(3, 5);
            this.m_lblfgclose.Name = "m_lblfgclose";
            this.m_lblfgclose.Resizeble = true;
            this.m_lblfgclose.Size = new System.Drawing.Size(65, 18);
            this.m_lblfgclose.TabIndex = 1;
            this.m_lblfgclose.Text = "공정마감";
            this.m_lblfgclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel16
            // 
            this.panel16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.m_txtsrcmach);
            this.panel16.Controls.Add(this.m_txtsrclabor);
            this.panel16.Controls.Add(this.m_chkfgclose);
            this.panel16.Controls.Add(this.m_chkfgisu);
            this.panel16.Controls.Add(this.m_chkfgmove);
            this.panel16.Controls.Add(this.m_currtmmach);
            this.panel16.Controls.Add(this.m_currtmlabor);
            this.panel16.Controls.Add(this.panel18);
            this.panel16.Controls.Add(this.panel19);
            this.panel16.Controls.Add(this.panel20);
            this.panel16.Controls.Add(this.panel17);
            this.panel16.Controls.Add(this.panel21);
            this.panel16.Controls.Add(this.panel22);
            this.panel16.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel16.Location = new System.Drawing.Point(424, 92);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(360, 78);
            this.panel16.TabIndex = 135;
            // 
            // m_txtsrcmach
            // 
            this.m_txtsrcmach.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtsrcmach.Location = new System.Drawing.Point(86, 29);
            this.m_txtsrcmach.MaxLength = 10;
            this.m_txtsrcmach.Name = "m_txtsrcmach";
            this.m_txtsrcmach.SelectedAllEnabled = false;
            this.m_txtsrcmach.Size = new System.Drawing.Size(77, 21);
            this.m_txtsrcmach.TabIndex = 13;
            this.m_txtsrcmach.UseKeyEnter = true;
            this.m_txtsrcmach.UseKeyF3 = false;
            this.m_txtsrcmach.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            this.m_txtsrcmach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // m_txtsrclabor
            // 
            this.m_txtsrclabor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtsrclabor.Location = new System.Drawing.Point(86, 3);
            this.m_txtsrclabor.MaxLength = 10;
            this.m_txtsrclabor.Name = "m_txtsrclabor";
            this.m_txtsrclabor.SelectedAllEnabled = false;
            this.m_txtsrclabor.Size = new System.Drawing.Size(75, 21);
            this.m_txtsrclabor.TabIndex = 11;
            this.m_txtsrclabor.UseKeyEnter = true;
            this.m_txtsrclabor.UseKeyF3 = false;
            this.m_txtsrclabor.Validated += new System.EventHandler(this.OnWorkControlUpDate);
            this.m_txtsrclabor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // m_btnRejectDtl
            // 
            this.m_btnRejectDtl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRejectDtl.BackColor = System.Drawing.Color.White;
            this.m_btnRejectDtl.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnRejectDtl.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnRejectDtl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRejectDtl.Location = new System.Drawing.Point(699, 0);
            this.m_btnRejectDtl.Name = "m_btnRejectDtl";
            this.m_btnRejectDtl.Size = new System.Drawing.Size(88, 24);
            this.m_btnRejectDtl.TabIndex = 136;
            this.m_btnRejectDtl.TabStop = false;
            this.m_btnRejectDtl.Tag = "REJECT_DTL";
            this.m_btnRejectDtl.Text = "불량내역등록";
            this.m_btnRejectDtl.UseVisualStyleBackColor = false;
            this.m_btnRejectDtl.Click += new System.EventHandler(this.m_btnRejectDtl_Click);
            // 
            // m_pnlTitle1
            // 
            this.m_pnlTitle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlTitle1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_pnlTitle1.BackgroundImage")));
            this.m_pnlTitle1.Controls.Add(this.m_lblTitle1);
            this.m_pnlTitle1.Location = new System.Drawing.Point(424, 3);
            this.m_pnlTitle1.Name = "m_pnlTitle1";
            this.m_pnlTitle1.Size = new System.Drawing.Size(360, 23);
            this.m_pnlTitle1.TabIndex = 137;
            // 
            // m_lblTitle1
            // 
            this.m_lblTitle1.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTitle1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblTitle1.ForeColor = System.Drawing.Color.Black;
            this.m_lblTitle1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_lblTitle1.Location = new System.Drawing.Point(20, 4);
            this.m_lblTitle1.Name = "m_lblTitle1";
            this.m_lblTitle1.Resizeble = true;
            this.m_lblTitle1.Size = new System.Drawing.Size(148, 18);
            this.m_lblTitle1.TabIndex = 7;
            this.m_lblTitle1.Tag = "DTL_INFO";
            this.m_lblTitle1.Text = "세부내역";
            this.m_lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_gridTmp, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlWOInfo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 138;
            // 
            // m_gridTmp
            // 
            this.m_gridTmp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridTmp.Controls.Add(this.m_btnRejectDtl);
            this.m_gridTmp.Controls.Add(this.m_btnproc_rework);
            this.m_gridTmp.Controls.Add(this.m_btnhst_work);
            this.m_gridTmp.Controls.Add(this.m_btndtl_isu);
            this.m_gridTmp.Location = new System.Drawing.Point(3, 88);
            this.m_gridTmp.Name = "m_gridTmp";
            this.m_gridTmp.Size = new System.Drawing.Size(787, 24);
            this.m_gridTmp.TabIndex = 139;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.m_pnlWorkInfo, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_pnlTitle1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel16, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel11, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 385);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(787, 173);
            this.tableLayoutPanel2.TabIndex = 140;
            // 
            // P_PR_WORKWO_REG_OLD
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_PR_WORKWO_REG";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPagePaint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtFrom)).EndInit();
            this.panel23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWoTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWoFrom)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.m_pnlWOInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
            this.m_pnlWorkInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_currqtreject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_currqtwork)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dt)).EndInit();
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_currtmmach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_currtmlabor)).EndInit();
            this.panel20.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.m_pnlTitle1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_gridTmp.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region ♣ 초기화 이벤트 / 메소드
		
		#region -> Page_Load 
		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void OnPageLoad(object sender, EventArgs e)
		{
			this.Enabled = false;
			this.Show();
			_flexM.GridStyle = GridStyleEnum.Green;
			_flexD.GridStyle = GridStyleEnum.Blue;
			Application.DoEvents();
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
					case "NO_WO":		// 작업지시번호
						temp = temp + " + " + this.GetDataDictionaryItem("PR","NO_WO");
						break;
					case "FG_WO":		// 지시구분
						temp = temp + " + " + this.GetDataDictionaryItem("PR","FG_WO");
						break;
					case "ST_WO":		// 지시상태
						temp = temp + " + " + this.GetDataDictionaryItem("PR","ST_WO");
						break;
					case "CD_ITEM":		// 품목코드
						temp = temp + " + " + this.GetDataDictionaryItem("PR","ITEM");
						break;
					case "NM_ITEM":		// 품목명
						temp = temp + " + " + this.GetDataDictionaryItem("PR","NM_ITEM");
						break;
					case "STND_ITEM":	// 규격
						temp = temp + " + " + this.GetDataDictionaryItem("PR","STND_ITEM");
						break;
					case "QT_ITEM":		// 지시수량
						temp = temp + " + " + this.GetDataDictionaryItem("PR","QT_WO");
						break;					
					case "DT_REL":		// 시작일
						temp = temp + " + " + this.GetDataDictionaryItem("PR","DT_START");
						break;
					case "DT_DUE":		// 종료일
						temp = temp + " + " + this.GetDataDictionaryItem("PR","DT_END");
						break;					
					case "CD_OP":		// 공정코드
						temp = temp + " + " + this.GetDataDictionaryItem("PR","CD_OP");
						break;
					case "NM_OP":		// 공정명
						temp = temp + " + " + this.GetDataDictionaryItem("PR","NM_OP");
						break;
					case "CD_WC":		// 작업장코드
						temp = temp + " + " + this.GetDataDictionaryItem("PR","CD_WC");
						break;
					case "NM_WC":		// 작업장명
						temp = temp + " + " + this.GetDataDictionaryItem("PR","NM_WC");
						break;
					case "ST_OP":		// 공정상태
						temp = temp + " + " + this.GetDataDictionaryItem("PR","ST_OP");
						break;
					case "QT_WO":		// 지시수량
						temp = temp + " + " + this.GetDataDictionaryItem("PR","QT_WO");
						break;							
					case "QT_WO_WORK":	// 완료수량
						temp = temp + " + " + this.GetDataDictionaryItem("PR","QT_WORK");
						break;
					case "QT_START":	// 입고수량
						temp = temp + " + " + this.GetDataDictionaryItem("PR","QT_RCV");
						break;
					case "QT_REMAIN":	// 작업잔량
						temp = temp + " + " + this.GetDataDictionaryItem("PR","QT_REMAIN");
						break;
					case "QT_REWORKREMAIN":	// 재작업잔량
						temp = temp + " + " + this.GetDataDictionaryItem("PR","QT_REWORK_REMAIN");
						break;
					default:			// 기타
						temp = temp + " + " + this.GetDataDictionaryItem("PR",colName[i]);
						break;
				}
			}
			
			if(temp == "")
				return "";
			else
				return temp.Substring(3,temp.Length-3);
		}

		#endregion
		
		#region -> OnPagePaint 
		/// <summary>
		/// 페인트
		/// </summary>
		private void OnPagePaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{			
				if(_isPainted == false)
				{	
					this._isPainted = true; 
					InitGrid();		
					Application.DoEvents();
					
					InitControl();	
					Application.DoEvents();
			
					//시작일과 종료일 셋팅
					string ls_date = this.MainFrameInterface.GetStringToday;			
					this.m_dtFrom.Text = ls_date.ToString().Substring(0,6) + "01" ; //작업일
					this.m_dt.Text = this.m_dtTo.Text = ls_date.ToString().Substring(0,8); //작업일
								
					this.Enabled = true;
					this.Paint -= new System.Windows.Forms.PaintEventHandler(this.OnPagePaint);
				}
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
			}
			finally
			{
				this.ToolBarSearchButtonEnabled = true;
				this.m_cboCdPlant.Focus();	
			}
		}
		#endregion
		
		#region -> InitGrid 
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitGrid()
		{
			#region -> 작업장 그리드
			Application.DoEvents();

			_flexM.Redraw = false;

			_flexM.Rows.Count = 1;
			_flexM.Rows.Fixed = 1;
			_flexM.Cols.Count = 10;
			_flexM.Cols.Fixed = 1;
			_flexM.Rows.DefaultSize = 20;	

			_flexM[0,0] = " ";
			_flexM.Cols[0].Width = 35;

			// 작업지시번호
			_flexM.Cols[1].Name = "NO_WO";
			_flexM.Cols[1].DataType = typeof(string);
			_flexM.Cols[1].Width = 110;
			
			// 지시구분
			_flexM.Cols[2].Name = "FG_WO";
			_flexM.Cols[2].DataType = typeof(string);
			_flexM.Cols[2].Width = 90;
			
			// 지시상태
			_flexM.Cols[3].Name = "ST_WO";
			_flexM.Cols[3].DataType = typeof(string);
			_flexM.Cols[3].Width = 60;
			
			// 품목코드
			_flexM.Cols[4].Name = "CD_ITEM";
			_flexM.Cols[4].DataType = typeof(string);
			_flexM.Cols[4].Width = 120;
			
			// 품목명
			_flexM.Cols[5].Name = "NM_ITEM";
			_flexM.Cols[5].DataType = typeof(string);
			_flexM.Cols[5].Width = 200;
			
			// 품목규격
			_flexM.Cols[6].Name = "STND_ITEM";
			_flexM.Cols[6].DataType = typeof(string);
			_flexM.Cols[6].Width = 150;
			
			// 지시수량
			_flexM.Cols[7].Name = "QT_ITEM";
			_flexM.Cols[7].DataType = typeof(string);
			_flexM.Cols[7].Width = 60;
			_flexM.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexM.Cols[7].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.MONEY, FormatFgType.SELECT);
			
			// 시작일
			_flexM.Cols[8].Name = "DT_REL";
			_flexM.Cols[8].DataType = typeof(string);
			_flexM.Cols[8].Width = 80;
			_flexM.Cols[8].TextAlign = TextAlignEnum.CenterCenter;
			_flexM.Cols[8].EditMask = this.GetFormatDescription(DataDictionaryTypes.PR,FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexM.Cols[8].Format = _flexM.Cols[8].EditMask;
			_flexM.SetStringFormatCol("DT_REL");

			// 종료일
			_flexM.Cols[9].Name = "DT_DUE";
			_flexM.Cols[9].DataType = typeof(string);
			_flexM.Cols[9].Width = 80;
			_flexM.Cols[9].TextAlign = TextAlignEnum.CenterCenter;
			_flexM.Cols[9].EditMask = this.GetFormatDescription(DataDictionaryTypes.PR,FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexM.Cols[9].Format = _flexM.Cols[9].EditMask;
			_flexM.SetStringFormatCol("DT_DUE");
			
			_flexM.AllowSorting = AllowSortingEnum.None;
			_flexM.NewRowEditable = false;	
			_flexM.EnterKeyAddRow = false;
			
			_flexM.SumPosition = SumPositionEnum.None;
			
			// 그리드 헤더캡션 표시하기
			for(int i = 1; i <= _flexM.Cols.Count-1; i++)
				_flexM[0, i] = GetDDItem(_flexM.Cols[i].Name);
			
			_flexM.Redraw = true;

			_flexM.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexM.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flex_AfterRowColChange);
			#endregion

			#region -> 작업시간 그리드
			Application.DoEvents();

			_flexD.Redraw = false;

			_flexD.Rows.Count = 1;
			_flexD.Rows.Fixed = 1;
			_flexD.Cols.Count = 12;
			_flexD.Cols.Fixed = 1;
			_flexD.Rows.DefaultSize = 20;	

			_flexD[0,0] = " ";
			_flexD.Cols[0].Width = 35;

			// 공정코드
			_flexD.Cols[1].Name = "CD_OP";
			_flexD.Cols[1].DataType = typeof(string);
			_flexD.Cols[1].Width = 30;

			// 공정명
			_flexD.Cols[2].Name = "NM_OP";
			_flexD.Cols[2].DataType = typeof(string);
			_flexD.Cols[2].Width = 100;

			// 작업장코드
			_flexD.Cols[3].Name = "CD_WC";
			_flexD.Cols[3].DataType = typeof(string);
			_flexD.Cols[3].Width = 60;

			// 작업장명
			_flexD.Cols[4].Name = "NM_WC";
			_flexD.Cols[4].DataType = typeof(string);
			_flexD.Cols[4].Width = 100;

			// 공정상태
			_flexD.Cols[5].Name = "ST_OP";
			_flexD.Cols[5].DataType = typeof(string);
			_flexD.Cols[5].Width = 40;

			// 지시수량
			_flexD.Cols[6].Name = "QT_WO";
			_flexD.Cols[6].DataType = typeof(string);
			_flexD.Cols[6].Width = 60;
			_flexD.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[6].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 입고수량
			_flexD.Cols[7].Name = "QT_START";
			_flexD.Cols[7].DataType = typeof(int);
			_flexD.Cols[7].Width = 60;
			_flexD.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[7].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 실적수량
			_flexD.Cols[8].Name = "QT_WO_WORK";
			_flexD.Cols[8].DataType = typeof(int);
			_flexD.Cols[8].Width = 60;
			_flexD.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[8].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.QUANTITY, FormatFgType.SELECT);

			//작업잔량
			_flexD.Cols[9].Name = "QT_REMAIN";
			_flexD.Cols[9].DataType = typeof(int);
			_flexD.Cols[9].Width = 60;
			_flexD.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[9].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.QUANTITY, FormatFgType.SELECT);

			//출고수량
			_flexD.Cols[10].Name = "QT_MOVE";
			_flexD.Cols[10].DataType = typeof(int);
			_flexD.Cols[10].Width = 60;
			_flexD.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[10].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.QUANTITY, FormatFgType.SELECT);

			//재작업잔량
			_flexD.Cols[11].Name = "QT_REWORKREMAIN";
			_flexD.Cols[11].DataType = typeof(int);
			_flexD.Cols[11].Width = 80;
			_flexD.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[11].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.QUANTITY, FormatFgType.SELECT);

			_flexD.AllowSorting = AllowSortingEnum.None;
			_flexD.NewRowEditable = false;	
			_flexD.EnterKeyAddRow = false;

			_flexD.SumPosition = SumPositionEnum.None;
			
			// 그리드 헤더캡션 표시하기
			for(int i = 1; i <= _flexD.Cols.Count-1; i++)
				_flexD[0, i] = GetDDItem(_flexD.Cols[i].Name);
			
			_flexD.Redraw = true;

			_flexD.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexD.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flex_AfterRowColChange);
			#endregion
		}
		#endregion

		#region -> InitControl 
		/// <summary>
		/// 헤더 콤보박스 셋팅
		/// </summary>
		private void InitControl()
		{
			try
			{
				this.m_lblplant.Text = this.GetDDItem("CD_PLANT");	//공장
				this.m_lblperiodwork.Text = this.GetDDItem("PERIOD_WORK");	//작업기간
				this.m_lblworkitem.Text = this.GetDDItem("ITEM_WORK");		//작업품목
				this.m_lblnowo.Text = this.GetDDItem("NO_WO");				//작업지시번호
				this.m_btndtl_isu.Text = this.GetDDItem("DTL_ISU");			//투입상세
				this.m_btnhst_work.Text = this.GetDDItem("HST_WORK");		//실적이력
				this.m_btnproc_rework.Text = this.GetDDItem("PROC_REWORK");	//공정재작업처리
				this.m_btnRejectDtl.Text = this.GetDDItem("REJECT_DTL");	//불량내역등록
		
				this.m_lblqtwork.Text = this.GetDDItem("QT_WORK");			//실적수량
				this.m_lblqtreject.Text = this.GetDDItem("QT_REJECT");		//불량수량
				this.m_lblcdrsrclabor.Text = this.GetDDItem("LABOR_RSRC");	//인원자원수
				this.m_lbltmlabor.Text = this.GetDDItem("LABOR_TIME");		//인원시간
				this.m_lblcdrsrcmach.Text = this.GetDDItem("MACHINE_RSRC");	//기계자원수
				this.m_lbltmmach.Text = this.GetDDItem("MACHINE_TIME");		//기계시간
				this.m_lbldtwork.Text = this.GetDDItem("DT_WORK");			//작업일자

				this.m_lblEmp.Text = this.GetDDItem("EMP");				//담당자
				this.m_lblfgmove.Text = this.GetDDItem("FG_MOVE");		//공정이동
				this.m_lblfgisu.Text = this.GetDDItem("FG_ISU");		//자재투입
				this.m_lblfgclose.Text = this.GetDDItem("FG_COMPLETE");	//공정마감
			
				this.m_lblStWo.Text = this.GetDDItem(this.m_lblStWo.Tag.ToString());			//상태
				this.m_lblTitle1.Text = this.GetDDItem(this.m_lblTitle1.Tag.ToString());		//세부내역
				this.m_chkNonClose.Text = this.GetDDItem(this.m_chkNonClose.Tag.ToString());	//미마감
				this.m_chkClose.Text = this.GetDDItem(this.m_chkClose.Tag.ToString());			//마감
				Application.DoEvents();
			
				string[] args1 = {"CD_PLANT", "U", "U", "U", "PR"};
				string[] args2 = {"CD_PLANT", "PR_0000009", "PR_0000007", "PR_0000006"};
				makeCommon mk = new makeCommon(args1, args2);
				DataSet _ds = mk.reDataSet(this.MainFrameInterface, this.LoginInfo.CompanyCode);
				if(_ds != null)
				{
					// 공장
					m_cboCdPlant.DataSource = _ds.Tables[0];
					m_cboCdPlant.DisplayMember = "NAME";
					m_cboCdPlant.ValueMember = "CODE";
					if(this.LoginInfo.CdPlant.ToString() != "")	m_cboCdPlant.SelectedValue = this.LoginInfo.CdPlant.ToString();
			
					_flexD.SetDataMap("ST_OP", _ds.Tables[1].Copy(), "CODE", "NAME");
					_flexD.ShowButtons = ShowButtonsEnum.WhenEditing;

					_flexM.SetDataMap("FG_WO", _ds.Tables[2].Copy(), "CODE", "NAME");
					_flexM.ShowButtons = ShowButtonsEnum.WhenEditing;

					_flexM.SetDataMap("ST_WO", _ds.Tables[3].Copy(), "CODE", "NAME");
					_flexM.ShowButtons = ShowButtonsEnum.WhenEditing;
				}
				Application.DoEvents();
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return;
			}
		}
		#endregion

		#endregion
		
		#region ♣ 메인버튼 이벤트/메소드 
		
		#region -> 조회조건체크 
		/// <summary>
		/// 테이블 체크
		/// </summary>
		private bool SearchCondition()
		{
			try
			{
				if(this.m_cboCdPlant.SelectedValue.ToString() == "")
				{
					//공장은 필수 입력입니다.
					this.ShowMessage("WK1_004", this.GetDDItem("PLANT"));
					this.m_cboCdPlant.Focus();
					return false;
				}
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
			}

			return true;
			
		}
		#endregion		

		#region -> 조회 
	
		/// <summary>
		/// 조회버튼 클릭시
		/// </summary>
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;

            //this.m_lblTitle.Focus();

			try
			{
				if(!SearchCondition())
					return;

				if (sender == null)
				{
					// 변경된 내용 조사 후 변경된 내용 있을 경우 저장
					if(!MsgAndSave(true,false))
						return;
				}
				
				if(_flexM.DataTable != null)
				{
					_flexM.DataTable.Rows.Clear();
					_flexD.DataTable.Rows.Clear();
				}
				
				// 자료조회중입니다. 잠시만 기다려주세요.
				this.ShowStatusBarMessage(2);

				Duzon.Common.Util.SpInfoCollection sc = new Duzon.Common.Util.SpInfoCollection();
				for(int i = 0; i < 2; i++)
				{
					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
					if(i == 0)
					{
						object[] obj = new Object[25];
						si.SpNameSelect = "UP_PR_WO";
						obj[0] = "S3";
						obj[1] = this.LoginInfo.CompanyCode;
						obj[3] = this.m_cboCdPlant.SelectedValue.ToString();
						obj[14] = this.m_dtFrom.Text.Replace("/","").Replace("_","");	//작업일(From)
						obj[15] = this.m_dtTo.Text.Replace("/","").Replace("_","");		//작업일(To)
						obj[5] = this.m_txtCdItem.CodeValue;							//작업품목
						obj[2] = this.m_txtWoFrom.Text.ToString();						//작지번호(From)
						obj[12] = this.m_txtWoTo.Text.ToString();						//작지번호(To)
						if (m_chkNonClose.Checked == true && m_chkClose.Checked == true)
							obj[8] = "RC";												//진행상태
						else if (m_chkNonClose.Checked == true && m_chkClose.Checked == false)
							obj[8] = "R";												//진행상태
						else if (m_chkNonClose.Checked == false && m_chkClose.Checked == true)
							obj[8] = "C";												//진행상태
						else if (m_chkNonClose.Checked == false && m_chkClose.Checked == false)
							obj[8] = "NC";												//진행상태
						si.SpParamsSelect = obj;
					}
					else
					{
						object[] obj = new Object[35];
						si.SpNameSelect = "UP_PR_WORK_REG";
						if (m_chkNonClose.Checked == true && m_chkClose.Checked == true)
							obj[0] = "SRC";
						if (m_chkNonClose.Checked == true && m_chkClose.Checked == false)
							obj[0] = "SR";
						if (m_chkNonClose.Checked == false && m_chkClose.Checked == true)
							obj[0] = "SC";
						if (m_chkNonClose.Checked == false && m_chkClose.Checked == false)
							obj[0] = "S";
						obj[1] = this.LoginInfo.CompanyCode;
						obj[4] = this.m_cboCdPlant.SelectedValue.ToString();
						obj[8] = this.m_dtFrom.Text.Replace("/","").Replace("_","");	//작업일(From)
						obj[22] = this.m_dtTo.Text.Replace("/","").Replace("_","");		//작업일(To)
						obj[6] = this.m_txtCdItem.CodeValue;							//작업품목
						obj[3] = this.m_txtWoFrom.Text.ToString();						//작지번호(From)
						obj[2] = this.m_txtWoTo.Text.ToString();						//작지번호(To)
						obj[34] = m_chkfgmove.Checked == true ? "Y" : "N";
						si.SpParamsSelect = obj;
					}

					sc.Add(si);
				}

				DataSet _dsWo = (DataSet)this.FillDataSet(sc);
				
				//사원을 넣는다.
				foreach(DataRow _drRow in _dsWo.Tables[1].Rows)
				{
					_drRow["NO_EMP"] = this.MainFrameInterface.LoginInfo.EmployeeNo;
					_drRow["NM_KOR"] = this.MainFrameInterface.LoginInfo.EmployeeName;
				}
				_dsWo.Tables[1].AcceptChanges();

				_flexD.Redraw=false;
				_flexD.BindingStart();
				_flexD.DataSource = _dsWo.Tables[1].DefaultView;	//실적 정보(하단 그리드)	
				_flexD.EmptyRowFilter();	
				_flexD.BindingEnd();
				_flexD.Redraw=true;
				
				_flexM.Redraw=false;
				_flexM.BindingStart();
				_flexM.DataSource = _dsWo.Tables[0].DefaultView;	//작업장 정보(상단 그리드)	
				_flexM.BindingEnd();
				_flexM.Redraw=true;

				if(!_flexM.HasNormalRow)	// 검색된 내용이 존재하지 않습니다..
					this.ShowMessage("IK1_003");
				
				this.ShowStatusBarMessage(3, _flexM.DataTable.Rows.Count.ToString());
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				this.ShowStatusBarMessage(0);
			}
			finally
			{				
				this.m_dt.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_currqtwork.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_currqtreject.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_txtsrclabor.TextChanged += new System.EventHandler(this.onControlChang);
			
				this.m_currtmlabor.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_txtsrcmach.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_currqtwork.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_currtmmach.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_chkfgmove.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_chkfgisu.TextChanged += new System.EventHandler(this.onControlChang);
				this.m_chkfgclose.TextChanged += new System.EventHandler(this.onControlChang);
			}
		}
		#endregion

		#region -> 저장 

		#region ->-> DoContinue

		private bool DoContinue()
		{
			if(_flexD.Editor != null)
			{
				return _flexD.FinishEditing(false);
			}
			
			return true;
		}

		#endregion
		
		#region ->-> IsChanged
		/// <summary>
		/// 변경유무
		/// </summary>
		/// <param name="gubun"></param>
		/// <returns></returns>
		private bool IsChanged(string gubun)
		{
			if(gubun == null)
				return _flexD.IsDataChanged;
			if(gubun == "_flexD")
				return _flexD.IsDataChanged;

			return false;
		}
		#endregion

		#region ->-> MsgAndSave

		private bool MsgAndSave(bool displayDialog, bool isExit)
		{
			if(!IsChanged(null)) return true;
			
			bool isSaved = false;

			if(!displayDialog)								// 저장 버튼을 클릭한 경우이므로 다이알로그는 필요없음
			{
				if(IsChanged(null)) isSaved = Save();

				return isSaved;
			}

			DialogResult result;

			if(isExit)
			{
				// 변경된 내용이 있습니다. 저장하시겠습니까?
				result = this.ShowMessage("QY3_002");
				if(result == DialogResult.No)
					return true;
				if(result == DialogResult.Cancel)
					return false;
			}
			else
			{
				// 변경된 내용이 있습니다. 저장하시겠습니까?
				result = this.ShowMessage("QY2_001");
				if(result == DialogResult.No)
					return true;
			}

			Application.DoEvents();		// 대화상자 즉시 사라지게

			// "예"를 선택한 경우
			if(IsChanged(null)) isSaved = Save();

			return isSaved;
		}

		#endregion

		#region ->-> Check

		private bool Check()
		{
			if(!_flexD.HasNormalRow)
				return false;

			string reject = "0";
			if(m_currqtreject.Text.ToString().Replace(" ","") != "")
				reject = m_currqtreject.Text.ToString().Replace(" ","");

			if(System.Convert.ToDouble(reject) > 0)
			{
				if(_dtReject == null || _dtReject.Rows.Count < 1)
				{
					//불량수량이 존재하지만 불량내역이 존재하지 않을경우 도움창 Open
					MessageBoxEx.Show(MainFrameInterface.GetMessageDictionaryItem("PR_M100049"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
						
					ShowRejectHelp();
					this._flexD.Focus();
					return false;
				}
			}

			for(int i = 0; i < this._flexD.DataTable.Rows.Count; i++)
			{
				if(this._flexD.DataTable.Rows[i].RowState.ToString() == "Added" || this._flexD.DataTable.Rows[i].RowState.ToString() == "Modified")
				{
					if(this._flexD.DataTable.Rows[i]["DT_WORK"].ToString() == "" )
					{
						// 작업일자 필수입력값입니다.
						MessageBoxEx.Show(this.GetDDItem("DT_WORK") + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.m_dt.Focus();
						return false;
					}
				}
			}
			
			//변경된 DataView 정보를 새로운 DataTable 을 생성 해서 추가한 정보로 바꿈
			//Select 할때 실적정보에 대해서 빈 Row를 가져오는데 빈 Row에 데이터를 입력하면
			//DataState는 Modify 로 됨..
			//하지만 실제로는 Modify가 아니라 추가된 데이터임
			//따라서 빈 테이블을 생성해서 변경된 정보를 추가해줌
			
			if(!_flexD.IsDataChanged)
			{
				//변경된내용이 없습니다.
				MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("MA_M000017"),"", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.ToolBarSaveButtonEnabled = false;
				return false;
			}

			DataTable _dtUpdate = _flexD.DataTable.GetChanges();
			_dtWork = _flexD.DataTable.Clone();

			for(int li_i = 0; li_i < _dtUpdate.Rows.Count; li_i++)
			{
				if(_dtUpdate.Rows[li_i].RowState.ToString() == "Modified")
				{
					_dtUpdate.Rows[li_i].BeginEdit();
					_dtUpdate.Rows[li_i]["YN_REWORK"] = "N" ;
					_dtUpdate.Rows[li_i]["ID_INSERT"] = this.LoginInfo.UserID.ToString() ;
					_dtUpdate.Rows[li_i]["DTS_INSERT"] = this.MainFrameInterface.GetStringDetailToday;
					_dtUpdate.Rows[li_i].EndEdit();
					
					DataRow _drTemp = _dtWork.NewRow();

					for(int icnt = 0;icnt < _flexD.DataTable.Columns.Count; icnt++)
					{
						_drTemp[icnt] = _dtUpdate.Rows[li_i][icnt];
					}
					_dtWork.Rows.Add(_drTemp);
				}
			}

			return true;
		}

		#endregion

		/// <summary>
		/// 저장 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			int _Row = 0 ;
			
			try
			{
                //this.m_lblTitle.Focus();

				//저장 후 다시 조회 할 때 기존 그리드 행을 선택하기 위해 행 값을 담음
				_Row = _flexM.Row;
				int iCount = _flexM.DataTable.Rows.Count;
				if(Save())
				{
					this.ShowMessageBox(1);
					this.ToolBarSaveButtonEnabled = false;

					OnToolBarSearchButtonClicked(sender, e);
					if (iCount == _flexM.DataTable.Rows.Count)
					{
						_flexM.Select(_Row, 1);	//저장 후 조회(저장전 위치로 이동)
					}
					ClearDataControl();
				}
//				else
//				{
//					//작업을 정상적으로 처리하지 못했습니다.
//					this.ShowMessageBox(109);
//				}				
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
			}
		}
	
		/// <summary>
		/// 저장루틴
		/// </summary>
		private bool Save()
		{	
			try
			{
				// 테이블제약조건 검사(향후 추가)
				if(!Check())
				{
					this._flexD.Focus();
					return false;
				}

				DataTable _dtWorkUp = _dtWork.GetChanges();
				
				if(_dtWorkUp == null)
					return false;

				/*********** 채번해온다 *************/
				try
				{
					if(this._flexD.HasNormalRow && _flexD["NO_WORK"].ToString() == "")
					{
						string no = "";
						string day = "";
						day = m_dt.Text.ToString().Replace(" ","").Replace("/","").Replace("_","");

						if(day == "")
						{
							day = this.MainFrameInterface.GetStringToday.Substring(0,6);
						}

						no = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "05", day.Substring(0,6));
						if(no == "")
						{
							//채번하는 중 에러가 발생했습니다.
							this.ShowMessage("PR_M100040");
							return false;
						}
						else
						{
							for(int i = 0; i < _dtWorkUp.Rows.Count; i++)
							{
								_dtWorkUp.Rows[i]["DT_WORK"] = day;
								_dtWorkUp.Rows[i]["NO_WORK"] = no;
							}

							for(int i = 0; i < _dtReject.Rows.Count; i++)
							{
								_dtReject.Rows[i]["QT_WORK"] = _flexD["QT_WORK"];
								_dtReject.Rows[i]["NO_WORK"] = no;
							}
						}
					}
	
				}
				catch(Exception ex)
				{	
					// 작업을 정상적으로 처리하지 못했습니다.
					this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "", "", "MA_M000011");
					return false;
				}
				/************************************/

				// 작업중입니다.
				this.MainFrameInterface.SetStatusBarMessage(this.MainFrameInterface.GetMessageDictionaryItem("HR_M500019")+"....");
				Application.DoEvents();
				
				#region /** 변수 선언 **/
				Duzon.Common.Util.SpInfoCollection sc = new Duzon.Common.Util.SpInfoCollection();
				string[] _sH = new string[] { "STATE","CD_COMPANY",
											   "NO_WORK",
											   "NO_WO",
											   "CD_PLANT",
											   "CD_OP",
											   "CD_ITEM",
											   "NO_EMP",
											   "DT_WORK",
											   "QT_WORK",
											   "QT_REJECT",
											   "CD_REJECT",
											   "CD_SOURCE",
											   "QT_MOVE",
											   "YN_REWORK",
											   "TM_LABOR",
											   "CD_RSRC_LABOR",
											   "TM_MACH",
											   "CD_RSRC_MACH",
											   "NO_OP",
											   "CD_WC",
											   "DC_REJECT",
											   "DTS_INSERT",
											   "ID_INSERT",
											   "DTS_UPDATE",
											   "ID_UPDATE",
											   "NO_WORK_SRC",
											   "QT_CLS",
											   "CD_OP_TO",
											   "DC_REMARK",
											   "UM_WORK",
											   "AM_WORK",
											   "AM_VAT_WORK",
											   "AM_HAP_WORK", 
											   "FG_MOVE" 
											   //"YN_RECEIPT"
										   };

				string[] _sL = new string[] {   "STATE","CD_COMPANY", "NO_WO", 
												"NO_WORK",
												"NO_LINE",
												"CD_REJECT",
												"CD_RESOURCE",
												"TM_WORK",
												"QT_WORK",
												"QT_REJECT",
												"DC_RMK", 
												"NO_WOLINE"
											 };
				#endregion
				
				DataTable dtWorkUpTemp = _dtWorkUp.Copy();
				dtWorkUpTemp.Rows[0]["QT_REJECT"] = m_currqtreject.DecimalValue;

				for(int i = 0; i < 2; i++)
				{
					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
						

					if(i == 0)
					{
						si.DataValue = dtWorkUpTemp;
						si.SpNameInsert = si.SpNameUpdate = si.SpNameDelete = "UP_PR_WORK_REG";			// 프로시저명
						si.SpParamsDelete = si.SpParamsInsert = si.SpParamsUpdate = _sH;
					}
					else if(i == 1)
					{
						si.DataValue = _dtReject;
						si.SpNameInsert = si.SpNameUpdate = si.SpNameDelete = "UP_PR_WORKL";			// 프로시저명
						si.SpParamsDelete = si.SpParamsInsert = si.SpParamsUpdate = _sL;
					}
							
					si.SpParamsValues.Add(ActionState.Insert, "STATE", "I");
					si.SpParamsValues.Add(ActionState.Update, "STATE", "I");
					if(i == 1)
					{
						si.SpParamsValues.Add(ActionState.Insert, "NO_WOLINE", _flexD[_flexD.Row, "NO_LINE"].ToString());
						si.SpParamsValues.Add(ActionState.Update, "NO_WOLINE", _flexD[_flexD.Row, "NO_LINE"].ToString());
					}
					sc.Add(si);
				}

				ResultData[] result = (ResultData[])this.Save(sc);
				
				if(result[0].Result || result[1].Result)
				{
					_flexD.DataTable.AcceptChanges();
					_dtReject.Clear();
					_dtReject.AcceptChanges();
					return true;
				}
			}
			catch(coDbException ex)
			{	
				this.ShowErrorMessage(ex, this.PageName, string.Empty);
				return false;
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return false;
			}
			finally
			{
				this.ShowStatusBarMessage(0);
			}

			return false;
		}
		#endregion

		#region -> 인쇄 

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;
		}

		#endregion

		#region -> 종료 
		
		/// <summary>
		/// 종료
		/// </summary>
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return false;
			
			try
			{
				if(!MsgAndSave(true,true))	// 저장이 실패하면
					return false;					
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
			}
			return true;
		}

		#endregion

		#endregion
		
		#region ♣ 그리드 이벤트/메소드 

		/// <summary>
		/// Edit flase
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			e.Cancel = true;	// 셀 입력상태로 못 들어가게
		}

		/// <summary>
		/// 변경유무확인
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _flex_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			if(!_flexM.IsBindingEnd || !_flexM.HasNormalRow)
			{
				_flexD.EmptyRowFilter();
				return;
			}

			int _mRow = _flexM.Row ;
			int _dRow = _flexD.Row ;
			
			if(_flexD.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
			{								
				// 변경된 내용 조사 후 변경된 내용 있을 경우 저장
				if(IsChanged(null))
				{
					this._flexD.DataTable.RejectChanges();
					this._dtReject.Clear();
				}

				string ls_filter = "";

				ls_filter = "NO_WO = '" + _flexM[_flexM.Row, "NO_WO"].ToString() + "'";

				if(this.m_chkNonClose.Checked == false && this.m_chkClose.Checked == false)
				{
					//ls_filter += " AND ST_OP <> 'C' AND ST_OP = 'C'";
					ls_filter += " ";
				}
				else if(this.m_chkNonClose.Checked == true && this.m_chkClose.Checked == false)
				{
					//ls_filter += " AND ST_OP <> 'C'";
					ls_filter += " ";
				}
				else if(this.m_chkNonClose.Checked == false && this.m_chkClose.Checked == true)	
				{
					//ls_filter += " AND ST_OP = 'C'";
					ls_filter += " ";
				}

				_flexD.DataView.RowFilter  = ls_filter;
				Application.DoEvents();

				if(_flexD.HasNormalRow)
				{
					
					m_dt.Text = this._flexD[_flexD.Row, "DT_WORK"].ToString();
					//m_currqtwork.Text = this._flexD[_flexD.Row, "QT_WORK"].ToString();
					//m_currqtreject.Text = this._flexD[_flexD.Row, "QT_REJECT"].ToString();
					m_currqtwork.DecimalValue = 0;
					m_currqtreject.DecimalValue = 0;
					m_currtmlabor.Text = this._flexD[_flexD.Row, "TM_LABOR"].ToString();
					m_currtmmach.Text = this._flexD[_flexD.Row, "TM_MACH"].ToString();
					m_txtsrclabor.Text = this._flexD[_flexD.Row, "CD_RSRC_LABOR"].ToString();
					m_txtsrcmach.Text = this._flexD[_flexD.Row, "CD_RSRC_MACH"].ToString();
					m_txtNoEmp.CodeValue = this._flexD[_flexD.Row, "NO_EMP"].ToString();
					m_txtNoEmp.CodeName = this._flexD[_flexD.Row, "NM_KOR"].ToString();

					if(this._flexD[_flexD.Row, "FG_MOVE"].ToString() == "Y")
						this.m_chkfgmove.Checked = true;
					else 
						this.m_chkfgmove.Checked = false;

					if(this._flexD[_flexD.Row, "FG_ISU"].ToString() == "Y")
						this.m_chkfgisu.Checked = true;
					else 
						this.m_chkfgisu.Checked = false;

					if(this._flexD[_flexD.Row, "FG_CLOSE"].ToString() == "Y")
						this.m_chkfgclose.Checked = true;
					else 
						this.m_chkfgclose.Checked = false;

					this._flexD.DataTable.AcceptChanges();
					this._dtReject.Clear();
				}
			}
		}
		#endregion		
	
		#region ♣ 도움창 이벤트/메소드
		private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			switch(e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB: // 사업자등록번호
					e.HelpParam.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
					e.HelpParam.P65_CODE5 = m_cboCdPlant.Text.Replace(" ","").Remove(0, m_cboCdPlant.Text.Replace(" ","").IndexOf(")", 0) + 1);
					break;
			}
		}

		private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			DataRow[] _dr = e.HelpReturn.Rows;	
			m_txtStndItem.Text = _dr[0]["STND_ITEM"].ToString();
			m_txtUnit.Text = _dr[0]["UNIT_IMNM"].ToString();
		}

		private void OnBpControl_CodeChanged(object sender, System.EventArgs e)
		{
			m_txtStndItem.Text = m_txtUnit.Text = "";
		}

		/// <summary>
		/// DoubleClick
		/// </summary>
		private void OnControlDoubleClick(object sender, System.EventArgs e)
		{
			OnShowHelp(sender,e);
		}
		
		/// <summary>
		/// Click
		/// </summary>
		private void OnControlClick(object sender, System.EventArgs e)
		{
			OnShowHelp(sender,e);
		}

		/// <summary>
		/// 도움창
		/// </summary>
		private void OnShowHelp(object sender, System.EventArgs e)
		{
			if(Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
				return;

			object dlg = null;
			string control_name = ((Control)sender).Name.ToString();

			switch(control_name)
			{	
				#region *** 작업지시번호 ***
					// 작업지시번호From
				case "m_txtWoFrom" :
				case "m_btnnowofrom" :
					try
					{
						dlg = this.LoadHelpWindow("P_PR_WO_REG_SUB", new object[] { 
																					  this.MainFrameInterface,
																					  this.m_cboCdPlant.SelectedValue.ToString()});
					}
					catch(Exception ex)
					{
						// 작업을 정상적으로 처리하지 못했습니다.
						this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
						return;
					}
					
					if (((Duzon.Common.Forms.CommonDialog) dlg).ShowDialog() == DialogResult.OK)
					{
						if (dlg is IHelpWindow)
						{
							this.m_txtWoFrom.Text = ((IHelpWindow) dlg).ReturnValues[0].ToString();
						}
					}
					break;

					// 작업지시번호To
				case "m_txtWoTo" :
				case "m_btnnowoto" :
					try
					{
						dlg = this.LoadHelpWindow("P_PR_WO_REG_SUB", new object[] { 
																					  this.MainFrameInterface,
																					  this.m_cboCdPlant.SelectedValue.ToString()});
					}
					catch(Exception ex)
					{
						// 작업을 정상적으로 처리하지 못했습니다.
						this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
						return;
					}
					
					if (((Duzon.Common.Forms.CommonDialog) dlg).ShowDialog() == DialogResult.OK)
					{
						if (dlg is IHelpWindow)
						{
							this.m_txtWoTo.Text = ((IHelpWindow) dlg).ReturnValues[0].ToString();
						}
					}
					break;
				#endregion

				default :
					break;
			}
		}
		
		/// <summary>
		/// KeyDown
		/// </summary>
		private void OnControlKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.F3 )
			{
				OnShowHelp(sender,e); 
			}
		}

		/// <summary>
		/// KeyDownEnd
		/// </summary>
		private void OnControlKeyDownEnd(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				m_cboCdPlant.Focus();
			}
		}

		/// <summary>
		/// Validating
		/// </summary>
		private void OnControlValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string control_name = ((Control)sender).Name.ToString();
			DataTable _dthelp = new DataTable();

			switch(control_name)
			{	
				#region *** 작업지시번호 ***
				case "m_txtWoFrom" :	//작업지시번호From
					
					try
					{
						if(this.m_txtWoFrom.Text.ToString() == "")
							return;

						if(this.m_cboCdPlant.SelectedValue.ToString() == "")
						{
							// 공장코드는 필수입력값입니다.
							MessageBoxEx.Show(this.GetDataDictionaryItem(DataDictionaryTypes.PU, "CD_PLANT") + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.m_cboCdPlant.Focus();
							return;
						}
                    
						try
						{
							Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
							object[] obj = new Object[6];
							si.SpNameSelect = "UP_MAKE_COMMON";
							obj[0] = this.LoginInfo.CompanyCode;
							obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
							obj[2] = "NO_WO_PERIOD";
							obj[3] = this.m_txtWoFrom.Text.ToString();
							obj[4] = this.m_dtFrom.Text.Replace("/","").Replace(" ","").Replace("_","").ToString();
							obj[5] = this.m_dtTo.Text.Replace("/","").Replace(" ","").Replace("_","").ToString();
							si.SpParamsSelect = obj;
				
							object ret = this.FillDataTable(si);
							_dthelp = (DataTable)((ResultData)ret).DataValue;
						}
						catch(Exception ex)
						{
							MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}

						if (_dthelp == null && _dthelp.Rows.Count < 1)
						{
							// 작업지시번호이(가) 존재하지 않습니다..
							MessageBoxEx.Show(this.m_lblnowo.Text.ToString() + this.MainFrameInterface.GetMessageDictionaryItem("FI_M100001"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
							m_txtWoFrom.Text = "";
							m_txtWoFrom.Focus();
						}
						else
						{
							this.m_txtWoTo.Focus();
						}
					}
					catch(Exception ex)
					{
						MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					break;

				case "m_txtWoTo" :	//작업지시번호To

					try
					{
						if(this.m_txtWoTo.Text.ToString() == "")
						{   
							this.m_dt.Focus();
							return;
						}
						
						if(this.m_cboCdPlant.SelectedValue.ToString() == "")
						{
							// 공장코드는 필수입력값입니다.
							MessageBoxEx.Show(this.GetDataDictionaryItem(DataDictionaryTypes.PU, "CD_PLANT") + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.m_cboCdPlant.Focus();
							return;
						}

						try
						{
							Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
							object[] obj = new Object[6];
							si.SpNameSelect = "UP_MAKE_COMMON";
							obj[0] = this.LoginInfo.CompanyCode;
							obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
							obj[2] = "NO_WO_PERIOD";
							obj[3] = this.m_txtWoTo.Text.ToString();
							obj[4] = this.m_dtFrom.Text.Replace("/","").Replace(" ","").Replace("_","").ToString();
							obj[5] = this.m_dtTo.Text.Replace("/","").Replace(" ","").Replace("_","").ToString();
							si.SpParamsSelect = obj;
				
							object ret = this.FillDataTable(si);
							_dthelp = (DataTable)((ResultData)ret).DataValue;
						}
						catch(Exception ex)
						{
							MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}

						if (_dthelp == null && _dthelp.Rows.Count < 1)
						{
							// 작업지시번호이(가) 존재하지 않습니다..
							MessageBoxEx.Show(this.m_lblnowo.Text.ToString() + this.MainFrameInterface.GetMessageDictionaryItem("FI_M100001"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
							m_txtWoTo.Text = "";
							m_txtWoTo.Focus();
						}
						else
						{
							this.m_dt.Focus();
						}
					}
					catch(Exception ex)
					{
						MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					break;
				#endregion

				#region *** 마지막 커런시 ***
				case "m_currtmmach" :	//마지막 커런시
					this.m_cboCdPlant.Focus();
					break;
				#endregion

				default :
					break;
			}
		}

		#endregion  

		#region ♣ 버튼 이벤트/메소드 
		
		#region -> 불량내역등록 
		/// <summary>
		/// 불량내역등록
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnRejectDtl_Click(object sender, System.EventArgs e)
		{
			ShowRejectHelp();
		}

		/// <summary>
		/// 불량내역등록
		/// </summary>
		private void ShowRejectHelp()
		{
			try
			{
				if(Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
					return;

				if(!_flexM.HasNormalRow)
				{
					// 선택된 자료가 없습니다.
					MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("CM_M100007"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				
				DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '" +_flexM[_flexM.Row, "NO_WO"].ToString()+ "'", "", DataViewRowState.CurrentRows);
				
				int qt_reject = 0;
				if(this.m_currqtreject.Text.ToString().Replace(" ","") != "" && this.m_currqtreject.Text.ToString().Replace(" ","") != "0")
				{
					qt_reject = System.Convert.ToInt32(this.m_currqtreject.Text.ToString().Replace(" ",""));
				}
				else if(this.m_currqtreject.Text.ToString().Replace(" ","") == "" || this.m_currqtreject.Text.ToString().Replace(" ","") == "0")
				{
					//불량수량은 0보다 큽니다.
					MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("PR_M000018"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.m_currqtreject.Focus();
					return;
				}

				object dlg = this.LoadHelpWindow("P_PR_WORK_SUB02", new object[] {	 cur_row,
																					 this.MainFrameInterface, 
																					 this.m_cboCdPlant.Text.ToString(),
																					 qt_reject});
				if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
				{
					if(dlg is IHelpWindow)
					{
						object[] wo_sub = ((IHelpWindow)dlg).ReturnValues;
						_dtReject = (DataTable)wo_sub[0];
//						string mes = "";
//						for(int i = 0; i < _dtReject.Rows.Count; i++)
//						{
//							mes += _dtReject.Rows[i].RowState.ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["CD_COMPANY"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["NO_WORK"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["NO_LINE"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["CD_REJECT"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["CD_RESOURCE"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["TM_WORK"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["QT_WORK"].ToString();
//							mes += "==";
//							mes += _dtReject.Rows[i]["QT_REJECT"].ToString();
//							mes += "\n";
//
//						}
//						MessageBox.Show(mes);

						this.m_currqtreject.Focus();
					}
				}
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return;
			}
		}
		#endregion

		#region -> 공정재작업처리 
		
		/// <summary>
		/// 재작업처리 도움창 호출
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnproc_rework_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
					return;

				if(!_flexM.HasNormalRow || !_flexD.HasNormalRow)
				{
					// 선택된 자료가 없습니다.
					MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("CM_M100007"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				string[] loa_param = new string[15];
				int lrow_WO = _flexM.Row;
				int lcol_WO = _flexM.Col;

				int lrow = _flexD.Row;
				int lcol = _flexD.Col;
		
				loa_param[0] = this._flexD[lrow, "CD_PLANT"].ToString();	//공장
				loa_param[1] = this._flexD[lrow, "NM_PLANT"].ToString();	//공장명
				loa_param[2] = this._flexD[lrow, "NO_WO"].ToString();		//작지번호
				loa_param[3] = this._flexD[lrow, "CD_WC"].ToString();		//작업장
				loa_param[4] = this._flexD[lrow, "NM_WC"].ToString();		//작업장명
				loa_param[5] = this._flexD[lrow, "CD_OP"].ToString();		//공정
				loa_param[6] = this._flexD[lrow, "NM_OP"].ToString();		//공정명
				loa_param[7] = this._flexD[lrow, "CD_ITEM"].ToString();		//품목
				loa_param[8] = this._flexD[lrow, "NM_ITEM"].ToString();		//품명
				loa_param[9] = this._flexD[lrow, "STND_ITEM"].ToString();	//규격
				loa_param[10] = this._flexD[lrow, "UNIT_IM"].ToString();	//단위
				loa_param[11] = this._flexD[lrow, "QT_WO_REJECT"].ToString();//불량수량
				loa_param[12] = this._flexD[lrow, "QT_REWORK"].ToString();//재작업수량
				loa_param[13] = this._flexD[lrow, "QT_REWORKREMAIN"].ToString();//재작업잔량
                loa_param[14] = "WO";

				object dlg = this.LoadHelpWindow("P_PR_WORK_SUB", new object[] {this.MainFrameInterface, loa_param, this._flexD.DataView, lrow});
				if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
				{
					int iCount = _flexM.DataTable.Rows.Count;

					OnToolBarSearchButtonClicked(sender, e);

					if (iCount == _flexM.DataTable.Rows.Count)
					{
						this._flexM.Select(lrow_WO,lcol_WO);
					}
					this._flexM.Focus();
					this._flexD.Select(lrow,lcol);
					this._flexD.Focus();
				}
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return;
			}	
		}

		#endregion	

		#region -> 투입상세 
		/// <summary>
		/// 투입상세
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btndtl_isu_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
					return;

				if(!SearchCondition())
					return;
				
				if(!_flexM.HasNormalRow)
				{
					// 선택된 자료가 없습니다.
					MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("CM_M100007"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				else
				{

					string[] args = new string[6]{	 this.m_cboCdPlant.SelectedValue.ToString(),
													 _flexM[_flexM.Row, "DT_REL"].ToString(),		//시작일
													 _flexM[_flexM.Row, "DT_DUE"].ToString(),		//종료일
													 _flexD[_flexD.Row, "CD_WC"].ToString(),		//작업장
													 _flexM[_flexM.Row, "CD_ITEM"].ToString(),		//품목코드
													 _flexM[_flexM.Row, "NO_WO"].ToString()			//작업지시번호
												 };

					object[] obj_args = new object[2]{args, MainFrameInterface};

					if(this._curRow != _flexM.Row)
					{
						if(this.IsExistPage("P_PR_II_SCH01", true) == true)
						{
							//페이지를 먼저 닫는다.
							this.UnLoadPage("P_PR_II_SCH01", false);
						}

					}

					this._curRow = _flexM.Row;
					
					this.CallOtherPageMethod("P_PR_II_SCH01", "자재투입List", Grant, obj_args);
				}
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return;
			}
		}
		#endregion		

		#region -> 실적이력 
		/// <summary>
		/// 실적이력
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnhst_work_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
					return;

				if(!SearchCondition())
					return;
				
				if(!_flexM.HasNormalRow)
				{
					// 선택된 자료가 없습니다.
					MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("CM_M100007"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				else
				{
					DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '"+_flexM[_flexM.Row, "NO_WO"].ToString()+"' AND CD_OP = '" +_flexD[_flexD.Row, "CD_OP"].ToString()+ "'", "", DataViewRowState.CurrentRows);
					if(cur_row == null || cur_row.Length < 1)
					{
						// 실적이력이 존재하지 않습니다.
						MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("PR_M000066"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					int iQT_START = -1;
					int iQT_WORK = -1;
					if (_flexD.Row < _flexD.DataView.Count)//맨 마지막 행을 제외한 행일 때
					{
                        //iQT_START = Convert.ToInt32(_flexD[_flexD.Row+1, "QT_START"].ToString().Replace(",", "").Trim());
                        //iQT_WORK = Convert.ToInt32(_flexD[_flexD.Row+1, "QT_WO_WORK"].ToString().Replace(",", "").Trim());

                        iQT_START = Convert.ToInt32(_flexD[_flexD.Row + 1, "QT_START"]);
                        iQT_WORK = Convert.ToInt32(_flexD[_flexD.Row + 1, "QT_WO_WORK"]);
					}

					object dlg = this.LoadHelpWindow("P_PR_WORK_HST_SUB01", new object[] {cur_row, this.MainFrameInterface, iQT_START, iQT_WORK});
					if (DialogResult.OK == ((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog())
						OnToolBarSearchButtonClicked(sender, e);

//					string[] args = new string[5]{
//													 this.m_cboCdPlant.SelectedValue.ToString(),
//													 _flexM[_flexM.Row, _flexM.Binder.NameToColIndex("DT_REL")].Text.ToString(),		//시작일
//													 _flexM[_flexM.Row, _flexM.Binder.NameToColIndex("DT_DUE")].Text.ToString(),		//종료일
//													 _flexM[_flexM.Row, _flexM.Binder.NameToColIndex("CD_WC")].Text.ToString(),		//작업장
//													 _flexM[_flexM.Row, _flexM.Binder.NameToColIndex("CD_ITEM")].Text.ToString()	//품목코드
//												 };
//
//					object[] obj_args = new object[2]{args, MainFrameInterface};
//
//					if(this.CurrentRow2 != _flexM.Row)
//					{
//						if(this.IsExistPage("P_PR_WORK_SCH02", true) == true)
//						{
//							//페이지를 먼저 닫는다.
//							this.UnLoadPage("P_PR_WORK_SCH02", false);
//						}
//
//					}
//
//					this.CurrentRow2 = _flexM.Row;
//					
//					this.LoadPageFrom("P_PR_WORK_SCH02", "작업실적현황", Grant, obj_args);
				}
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return;
			}
		}
		#endregion		
		
		#endregion

		#region ♣ 기타 이벤트/메소드 

		#region -> 날짜 체크 
		
		private void ControlDtValidated(object sender, System.EventArgs e)
		{
			try
			{
				Duzon.Common.Controls.DatePicker dt = (Duzon.Common.Controls.DatePicker)sender;
		
				if(dt.Text.ToString().Replace("/","").Replace("_","").Replace(" ","") == "")
				{
					if(dt.Name.ToString() == "m_dtTo")
						this.m_txtWoFrom.Focus();
					
					return;
				}
						
				if(!dt.IsValidated)
				{
					// 날짜 입력형식이 잘못되었습니다.
					this.ShowMessage("WK1_003");
					dt.Text = "";
					dt.Focus();
					return;
				}
					
				int from = 0;
				int to = 0;

				string f_day = m_dtFrom.Text.ToString().Replace(" ","").Replace("/","").Replace("_","");
				string t_day = m_dtTo.Text.ToString().Replace(" ","").Replace("/","").Replace("_","");
					
				if(t_day.Trim().ToString() == "")
					return;

				if(f_day.Trim().ToString() != "")
					from = System.Int32.Parse(f_day);
						
				if(t_day.Trim().ToString() != "")
					to = System.Int32.Parse(t_day);

				if(from > to)
				{
					// 시작일이 종료일보다 늦을 수 없습니다.
					MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("SA_M000010"), this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					m_dtTo.Text = "";
					m_dtTo.Focus();
					return;
				}

				if(dt.Name.ToString() == "m_dtTo")
					this.m_txtWoFrom.Focus();
				
			}
			catch
			{
				// 날짜가 잘 못 입력되었습니다. 예)2002/01/01
				MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("TR_M000019"), this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				m_dtTo.Focus();
				return;
			}
		}

		#endregion
		
		#region -> 텍스트 변경 Event 모음 
		
		/// <summary>
		/// 공장환경설정 변경시 DataTable에 반영하기
		/// TEXTbOX, COMBO, OPTION 에서 변경시 DataTable에 반영
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWorkControlUpDate(object sender, System.EventArgs e)
		{
			if(!_flexD.HasNormalRow)
				return;
			
			int lrow = _flexD.Row;

			switch(sender.GetType().Name)
			{
				case "CurrencyTextBox" :
				
				switch(((Duzon.Common.Controls.CurrencyTextBox)sender).Name)
				{
						/****************/
						/* 실적정보 등록시*/
						/****************/
						// 실적수량
					case "m_currqtwork" :
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Modified == false)
							return;
						
						string qt_work = "0";
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","") != "")
							qt_work = ((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","");

						if(System.Convert.ToDouble(qt_work) > System.Convert.ToDouble(_flexD[_flexD.Row, "QT_START"].ToString()) - System.Convert.ToDouble(_flexD[_flexD.Row, "QT_WO_WORK"].ToString()))
						{
							// 실적수량이 입고수량을 초과합니다.
							//MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("PR_M100011"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
							System.Windows.Forms.DialogResult dialog = MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("PR_M200005"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
							if (DialogResult.Cancel == dialog)
							{
								m_currqtwork.DecimalValue = 0;
								return;
							}
						}
						_flexD[lrow, "QT_WORK"] = qt_work;
						break;

						// 불량수량
					case "m_currqtreject" :
						
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Modified == false)
							return;
						
						string qt_reject = "0";
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","") != "")
							qt_reject = ((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","");

						if(System.Convert.ToDouble(qt_reject) > System.Convert.ToDouble(m_currqtwork.DecimalValue))
						{
							//불량수량이 작업수량을 초과합니다.
							System.Windows.Forms.DialogResult dialog = MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("PR_M200003"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
//							if (DialogResult.Cancel == dialog)
//							{
							m_currqtreject.DecimalValue = 0;
							return;
//							}
						}
						_flexD[lrow, "QT_REJECT"] = qt_reject;
						break;

						// 인원시간
					case "m_currtmlabor" :
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Modified == false)
							return;
						
						string tm_labor = "0";
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","") != "")
							tm_labor = ((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","");

						_flexD[lrow, "TM_LABOR"] = tm_labor;
						break;

						// 기계시간
					case "m_currtmmach" :

						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Modified == false)
							return;
						
						string tm_mach = "0";
						if(((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","") != "")
							tm_mach = ((Duzon.Common.Controls.CurrencyTextBox)sender).Text.ToString().Replace(" ","");
						
						_flexD[lrow, "TM_MACH"] = tm_mach;
						break;
				}
					break;

				case "ComboBox" :
				switch(((Duzon.Common.Controls.ComboBoxExt)sender).Name)
				{
						// 불량코드
					case "m_cboreject" :
						
						_flexD[lrow, "CD_REJECT"] = ((Duzon.Common.Controls.ComboBoxExt)sender).SelectedValue;
						break;

						// 원인코드
					case "m_cboreason" :
						
						_flexD[lrow, "CD_SOURCE"] = ((Duzon.Common.Controls.ComboBoxExt)sender).SelectedValue;
						break;
				}
					break;
				case "DatePicker" :
				switch(((Duzon.Common.Controls.DatePicker)sender).Name)
				{
						// 작업일
					case "m_dt" :

						if(m_dt.Text.ToString().Replace("/","").Replace("_","").Replace(" ","") == "")
						{	
							return;
						}

						if(!m_dt.IsValidated)
						{
							// 날짜 입력형식이 잘못되었습니다.
							this.ShowMessage("WK1_003");
							m_dt.Text = "";
							m_dt.Focus();
						}

						_flexD[lrow, "DT_WORK"] = ((Duzon.Common.Controls.DatePicker)sender).Text.Replace("/","");
						break;
				}
					break;
				case "TextBox" :
				switch(((Duzon.Common.Controls.TextBoxExt)sender).Name)
				{
						// 인원자원
					case "m_txtsrclabor" :
						if(((Duzon.Common.Controls.TextBoxExt)sender).Modified == false)
							return;
						
						_flexD[lrow, "CD_RSRC_LABOR"] = ((Duzon.Common.Controls.TextBoxExt)sender).Text;
						break;

						// 기계자원
					case "m_txtsrcmach" :
						if(((Duzon.Common.Controls.TextBoxExt)sender).Modified == false)
							return;
						
						_flexD[lrow, "CD_RSRC_MACH"] = ((Duzon.Common.Controls.TextBoxExt)sender).Text;
						break;
				}
					break;
				default :
					break;
			}
			
			if(!ToolBarSaveButtonEnabled)
				this.ToolBarSaveButtonEnabled = true;
		}

		#endregion

		#region -> 저장버튼 활성화 
		
		/// <summary>
		/// onControlChang
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void onControlChang(object sender, System.EventArgs e)
		{
			if(_flexD.HasNormalRow && this.ToolBarSaveButtonEnabled == false)
				this.ToolBarSaveButtonEnabled = true;
		}
		#endregion

		#region -> 컨트롤에 값 체움 
		/// <summary>
		/// 컨트롤에 값 체움
		/// </summary>
		/// <returns></returns>
		private void fillDataControl(int ls_row)
		{
			try
			{
				if(!_flexD.HasNormalRow)
					return;
			
				m_dt.Text = this._flexD[ls_row, "DT_WORK"].ToString();
				m_currqtwork.Text = this._flexD[ls_row, "QT_WORK"].ToString();
				m_currqtreject.Text = this._flexD[ls_row, "QT_REJECT"].ToString();
				m_currtmlabor.Text = this._flexD[ls_row, "TM_LABOR"].ToString();
				m_currtmmach.Text = this._flexD[ls_row, "TM_MACH"].ToString();
				m_txtsrclabor.Text = this._flexD[ls_row, "CD_RSRC_LABOR"].ToString();
				m_txtsrcmach.Text = this._flexD[ls_row, "CD_RSRC_MACH"].ToString();
				m_txtNoEmp.CodeValue = this._flexD[ls_row, "NO_EMP"].ToString();
				m_txtNoEmp.CodeName = this._flexD[ls_row, "NM_KOR"].ToString();
				
				if(_flexD[ls_row, "FG_MOVE"].ToString() == "Y")
					this.m_chkfgmove.Checked = true;
				else 
					this.m_chkfgmove.Checked = false;

				if(_flexD[ls_row, "FG_ISU"].ToString() == "Y")
					this.m_chkfgisu.Checked = true;
				else 
					this.m_chkfgisu.Checked = false;

				if(_flexD[ls_row, "FG_CLOSE"].ToString() == "Y")
					this.m_chkfgclose.Checked = true;
				else 
					this.m_chkfgclose.Checked = false;
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
			}
		}
		#endregion

		#region -> 실적 내용 Clear 
		/// <summary>
		/// 실적 내용 Clear
		/// </summary>
		/// <returns></returns>
		private void ClearDataControl()
		{
			if(!_flexD.HasNormalRow)
				return;

			m_currqtwork.DecimalValue = m_currqtreject.DecimalValue = m_currtmlabor.DecimalValue = m_currtmmach.DecimalValue = 0;
			m_txtsrcmach.Text = m_txtsrclabor.Text = "";
		}
		#endregion

		#region -> 체크상태변경 
		/// <summary>
		/// 체크상태변경
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ControlCheckdChange(object sender, System.EventArgs e)
		{
			string control_name = ((Control)sender).Name.ToString();
			int lrow = _flexD.Row;

			switch(control_name)
			{
					// 공정이동
				case "m_chkfgmove" :

					if(this.m_chkfgmove.Checked == true)
						_flexD[lrow, "FG_MOVE"] = "Y";
					else
						_flexD[lrow, "FG_MOVE"] = "N";
					
					break;
					// 자재투입
				case "m_chkfgisu" :
					
					if(this.m_chkfgisu.Checked == true)
						_flexD[lrow, "FG_ISU"] = "Y";
					else
						_flexD[lrow, "FG_ISU"] = "N";
					
					break;
					// 공정마감
				case "m_chkfgclose" :
					
					if(this.m_chkfgclose.Checked == true)
						_flexD[lrow, "FG_CLOSE"] = "Y";
					else
						_flexD[lrow, "FG_CLOSE"] = "N";

					break;
				default :
					break;
			}
		}
		#endregion

		#endregion
	}
}

