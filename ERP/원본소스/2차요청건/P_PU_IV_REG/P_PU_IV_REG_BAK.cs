using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

using System.Diagnostics;


namespace pur
{
	/// <summary>
	//********************************************************************
	// 작   성   자 : 김대영, 김대영
	// 작   성   일 : 
	// 모   듈   명 : 구매/자재
	// 시 스  템 명 : 매입관리
	// 페 이 지  명 : 매입등록
	// 프로젝트  명 : P_PU_IV_REG
	// 수   정   일 : 2004-03-05 
    // 수   정   자 : 유지영 (2006-08-30)
	//********************************************************************
	/// </summary>
	public class P_PU_IV_REG_BAK : Duzon.Common.Forms.PageBase
	{
        #region ♣ 멤버필드
		
		#region -> 멤버필드(일반)
		private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.PanelExt panel3;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.LabelExt lb_NO_EMP;
		private Duzon.Common.Controls.LabelExt lb_DT_IV;
		private Duzon.Common.Controls.LabelExt lb_FG_PO_TR;
		private Duzon.Common.Controls.LabelExt lb_NM_BIZAREA;
		private Duzon.Common.Controls.RadioButtonExt rbtn_M_IV;
		private Duzon.Common.Controls.RadioButtonExt rbtn_T_IV;
		private Duzon.Common.Controls.RoundedButton btn_APPLY_GI;
		private System.ComponentModel.IContainer components;
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
		private System.Data.DataColumn dataColumn9;
		

		private System.Data.DataColumn dataColumn10; 

		private System.Data.DataColumn dataColumn11;
		private System.Data.DataTable dataTable2;
		private System.Data.DataColumn dataColumn12;
		private System.Data.DataColumn dataColumn13;
		private System.Data.DataColumn dataColumn14;
		private System.Data.DataColumn dataColumn15;
		private System.Data.DataColumn dataColumn16;
		private System.Data.DataColumn dataColumn17;
		private System.Data.DataColumn dataColumn18;
		private System.Data.DataColumn dataColumn19;
		private System.Data.DataColumn dataColumn20;
		private System.Data.DataColumn dataColumn21;
		private System.Data.DataColumn dataColumn22;
		private System.Data.DataColumn dataColumn23;
		private System.Data.DataColumn dataColumn24;
		private System.Data.DataColumn dataColumn25;
		private Duzon.Common.Controls.RoundedButton btn_TAXPAPER;
		private System.Data.DataColumn dataColumn26;
		private System.Data.DataColumn dataColumn27;
		private System.Data.DataColumn dataColumn28;
        private System.Data.DataColumn dataColumn29;
		private Duzon.Common.Controls.DropDownComboBox cbo_CD_BIZAREA;
		private Duzon.Common.Controls.DropDownComboBox cbo_FG_TRANS;
		private Duzon.Common.Controls.PanelExt m_pnlGrid;
		private Duzon.Common.Controls.DatePicker tb_DT_PO;


		#endregion

		#region -> 멤버필드(주요)	

	
		//그리드
		private Dass.FlexGrid.FlexGrid _flex;	
		// 페인트
		private bool _isPainted = false;
		// 콤보 박스
		DataSet _dsCombo = new DataSet();

		
		// 라인정보
		DataTable _dt_Line = new DataTable();

	
		// 부서코드 
		string _cddept="";
		private Duzon.Common.BpControls.BpCodeTextBox tb_NO_EMP;
        private PanelExt m_gridTmp;
        private TableLayoutPanel tableLayoutPanel1;
	
		// 텍스트 박스 포커스시 값 임시 저장 변수	
		private string gstb_noemp;
		
				
		#endregion
	
		#endregion

        #region ♣ 생성자/소멸자
	
		#region -> 생성자
        public P_PU_IV_REG_BAK()
		{
			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			// TODO: InitForm을 호출한 다음 초기화 작업을 추가합니다.
		
			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_IV_REG_BAK));
            this.m_pnlGrid = new Duzon.Common.Controls.PanelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.tb_NO_EMP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lb_DT_IV = new Duzon.Common.Controls.LabelExt();
            this.lb_NO_EMP = new Duzon.Common.Controls.LabelExt();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.lb_FG_PO_TR = new Duzon.Common.Controls.LabelExt();
            this.lb_NM_BIZAREA = new Duzon.Common.Controls.LabelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.rbtn_M_IV = new Duzon.Common.Controls.RadioButtonExt();
            this.rbtn_T_IV = new Duzon.Common.Controls.RadioButtonExt();
            this.cbo_CD_BIZAREA = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_FG_TRANS = new Duzon.Common.Controls.DropDownComboBox();
            this.tb_DT_PO = new Duzon.Common.Controls.DatePicker();
            this.btn_TAXPAPER = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_APPLY_GI = new Duzon.Common.Controls.RoundedButton(this.components);
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
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn29 = new System.Data.DataColumn();
            this.m_gridTmp = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_M_IV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_T_IV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_PO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            this.m_gridTmp.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlGrid
            // 
            this.m_pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid.Location = new System.Drawing.Point(3, 95);
            this.m_pnlGrid.Name = "m_pnlGrid";
            this.m_pnlGrid.Size = new System.Drawing.Size(787, 463);
            this.m_pnlGrid.TabIndex = 124;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tb_NO_EMP);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.cbo_CD_BIZAREA);
            this.panel1.Controls.Add(this.cbo_FG_TRANS);
            this.panel1.Controls.Add(this.tb_DT_PO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 55);
            this.panel1.TabIndex = 0;
            // 
            // tb_NO_EMP
            // 
            this.tb_NO_EMP.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_NO_EMP.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_NO_EMP.ButtonImage")));
            this.tb_NO_EMP.ChildMode = "";
            this.tb_NO_EMP.CodeName = "";
            this.tb_NO_EMP.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_NO_EMP.CodeValue = "";
            this.tb_NO_EMP.ComboCheck = true;
            this.tb_NO_EMP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.tb_NO_EMP.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NO_EMP.Location = new System.Drawing.Point(468, 3);
            this.tb_NO_EMP.Name = "tb_NO_EMP";
            this.tb_NO_EMP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NO_EMP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NO_EMP.SearchCode = true;
            this.tb_NO_EMP.SelectCount = 0;
            this.tb_NO_EMP.SetDefaultValue = false;
            this.tb_NO_EMP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NO_EMP.Size = new System.Drawing.Size(156, 21);
            this.tb_NO_EMP.TabIndex = 1;
            this.tb_NO_EMP.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Location = new System.Drawing.Point(5, 26);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(777, 1);
            this.panel8.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lb_DT_IV);
            this.panel6.Controls.Add(this.lb_NO_EMP);
            this.panel6.Location = new System.Drawing.Point(384, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(80, 51);
            this.panel6.TabIndex = 1;
            // 
            // lb_DT_IV
            // 
            this.lb_DT_IV.Location = new System.Drawing.Point(2, 32);
            this.lb_DT_IV.Name = "lb_DT_IV";
            this.lb_DT_IV.Resizeble = true;
            this.lb_DT_IV.Size = new System.Drawing.Size(75, 17);
            this.lb_DT_IV.TabIndex = 135;
            this.lb_DT_IV.Tag = "CD_CURRENCY";
            this.lb_DT_IV.Text = "처리일자";
            this.lb_DT_IV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NO_EMP
            // 
            this.lb_NO_EMP.Location = new System.Drawing.Point(2, 6);
            this.lb_NO_EMP.Name = "lb_NO_EMP";
            this.lb_NO_EMP.Resizeble = true;
            this.lb_NO_EMP.Size = new System.Drawing.Size(75, 17);
            this.lb_NO_EMP.TabIndex = 136;
            this.lb_NO_EMP.Tag = "GROUP_RCV";
            this.lb_NO_EMP.Text = "담당자";
            this.lb_NO_EMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.lb_FG_PO_TR);
            this.panel3.Controls.Add(this.lb_NM_BIZAREA);
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 51);
            this.panel3.TabIndex = 0;
            // 
            // lb_FG_PO_TR
            // 
            this.lb_FG_PO_TR.Location = new System.Drawing.Point(2, 32);
            this.lb_FG_PO_TR.Name = "lb_FG_PO_TR";
            this.lb_FG_PO_TR.Resizeble = true;
            this.lb_FG_PO_TR.Size = new System.Drawing.Size(75, 17);
            this.lb_FG_PO_TR.TabIndex = 133;
            this.lb_FG_PO_TR.Tag = "FG_OFFER";
            this.lb_FG_PO_TR.Text = "거래구분";
            this.lb_FG_PO_TR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NM_BIZAREA
            // 
            this.lb_NM_BIZAREA.Location = new System.Drawing.Point(3, 6);
            this.lb_NM_BIZAREA.Name = "lb_NM_BIZAREA";
            this.lb_NM_BIZAREA.Resizeble = true;
            this.lb_NM_BIZAREA.Size = new System.Drawing.Size(75, 17);
            this.lb_NM_BIZAREA.TabIndex = 132;
            this.lb_NM_BIZAREA.Tag = "";
            this.lb_NM_BIZAREA.Text = "사업장";
            this.lb_NM_BIZAREA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel9.Controls.Add(this.rbtn_M_IV);
            this.panel9.Controls.Add(this.rbtn_T_IV);
            this.panel9.Location = new System.Drawing.Point(696, 1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(88, 51);
            this.panel9.TabIndex = 139;
            // 
            // rbtn_M_IV
            // 
            this.rbtn_M_IV.Location = new System.Drawing.Point(8, 30);
            this.rbtn_M_IV.Name = "rbtn_M_IV";
            this.rbtn_M_IV.Size = new System.Drawing.Size(55, 18);
            this.rbtn_M_IV.TabIndex = 1;
            this.rbtn_M_IV.TabStop = true;
            this.rbtn_M_IV.Text = "건별";
            this.rbtn_M_IV.UseKeyEnter = true;
            // 
            // rbtn_T_IV
            // 
            this.rbtn_T_IV.Checked = true;
            this.rbtn_T_IV.Location = new System.Drawing.Point(8, 5);
            this.rbtn_T_IV.Name = "rbtn_T_IV";
            this.rbtn_T_IV.Size = new System.Drawing.Size(55, 18);
            this.rbtn_T_IV.TabIndex = 0;
            this.rbtn_T_IV.TabStop = true;
            this.rbtn_T_IV.Text = "일괄";
            this.rbtn_T_IV.UseKeyEnter = true;
            // 
            // cbo_CD_BIZAREA
            // 
            this.cbo_CD_BIZAREA.AutoDropDown = true;
            this.cbo_CD_BIZAREA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_CD_BIZAREA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_BIZAREA.Location = new System.Drawing.Point(85, 4);
            this.cbo_CD_BIZAREA.Name = "cbo_CD_BIZAREA";
            this.cbo_CD_BIZAREA.ShowCheckBox = false;
            this.cbo_CD_BIZAREA.Size = new System.Drawing.Size(192, 20);
            this.cbo_CD_BIZAREA.TabIndex = 0;
            this.cbo_CD_BIZAREA.Tag = "FG_TRANS";
            this.cbo_CD_BIZAREA.UseKeyEnter = false;
            this.cbo_CD_BIZAREA.UseKeyF3 = false;
            this.cbo_CD_BIZAREA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_COMBO_KeyDown);
            // 
            // cbo_FG_TRANS
            // 
            this.cbo_FG_TRANS.AutoDropDown = true;
            this.cbo_FG_TRANS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_FG_TRANS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FG_TRANS.Location = new System.Drawing.Point(85, 30);
            this.cbo_FG_TRANS.Name = "cbo_FG_TRANS";
            this.cbo_FG_TRANS.ShowCheckBox = false;
            this.cbo_FG_TRANS.Size = new System.Drawing.Size(192, 20);
            this.cbo_FG_TRANS.TabIndex = 2;
            this.cbo_FG_TRANS.Tag = "FG_TRANS";
            this.cbo_FG_TRANS.UseKeyEnter = false;
            this.cbo_FG_TRANS.UseKeyF3 = false;
            this.cbo_FG_TRANS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_COMBO_KeyDown);
            // 
            // tb_DT_PO
            // 
            this.tb_DT_PO.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_PO.DayColor = System.Drawing.Color.Black;
            this.tb_DT_PO.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_PO.Location = new System.Drawing.Point(468, 30);
            this.tb_DT_PO.Mask = "####/##/##";
            this.tb_DT_PO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_DT_PO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_PO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_PO.Modified = false;
            this.tb_DT_PO.Name = "tb_DT_PO";
            this.tb_DT_PO.PaddingCharacter = '_';
            this.tb_DT_PO.PassivePromptCharacter = '_';
            this.tb_DT_PO.PromptCharacter = '_';
            this.tb_DT_PO.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_PO.ShowToDay = true;
            this.tb_DT_PO.ShowTodayCircle = true;
            this.tb_DT_PO.ShowUpDown = false;
            this.tb_DT_PO.Size = new System.Drawing.Size(92, 21);
            this.tb_DT_PO.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_PO.TabIndex = 3;
            this.tb_DT_PO.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_PO.TitleForeColor = System.Drawing.Color.Black;
            this.tb_DT_PO.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_PO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_PO.UseKeyF3 = false;
            this.tb_DT_PO.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.tb_DT_PO.Validated += new System.EventHandler(this.DataPickerValidated);
            // 
            // btn_TAXPAPER
            // 
            this.btn_TAXPAPER.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_TAXPAPER.BackColor = System.Drawing.Color.White;
            this.btn_TAXPAPER.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_TAXPAPER.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_TAXPAPER.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TAXPAPER.Location = new System.Drawing.Point(50, 0);
            this.btn_TAXPAPER.Name = "btn_TAXPAPER";
            this.btn_TAXPAPER.Size = new System.Drawing.Size(100, 24);
            this.btn_TAXPAPER.TabIndex = 133;
            this.btn_TAXPAPER.TabStop = false;
            this.btn_TAXPAPER.Text = "세금계산서";
            this.btn_TAXPAPER.UseVisualStyleBackColor = false;
            this.btn_TAXPAPER.Click += new System.EventHandler(this.btn_TAXPAPER_Click);
            // 
            // btn_APPLY_GI
            // 
            this.btn_APPLY_GI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_APPLY_GI.BackColor = System.Drawing.Color.White;
            this.btn_APPLY_GI.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_APPLY_GI.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_APPLY_GI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_APPLY_GI.Location = new System.Drawing.Point(151, 0);
            this.btn_APPLY_GI.Name = "btn_APPLY_GI";
            this.btn_APPLY_GI.Size = new System.Drawing.Size(90, 24);
            this.btn_APPLY_GI.TabIndex = 132;
            this.btn_APPLY_GI.TabStop = false;
            this.btn_APPLY_GI.Text = "입고적용";
            this.btn_APPLY_GI.UseVisualStyleBackColor = false;
            this.btn_APPLY_GI.Click += new System.EventHandler(this.btn_APPLY_GI_Click);
            // 
            // ds_Ty1
            // 
            this.ds_Ty1.DataSetName = "NewDataSet";
            this.ds_Ty1.Locale = new System.Globalization.CultureInfo("ko-KR");
            this.ds_Ty1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2});
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
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24,
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn29});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "NO_IV";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CD_PARTNER";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "LN_PARTNER";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "NO_BIZAREA";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "FG_TAX";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "AM_K";
            this.dataColumn6.DataType = typeof(double);
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "VAT_TAX";
            this.dataColumn7.DataType = typeof(double);
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "AM_TOTAL";
            this.dataColumn8.DataType = typeof(double);
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "NO_TEMP";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "FG_TRANS";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "CD_COMPANY";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "CD_BIZAREA";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "YN_RETURN";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "AM_KR";
            this.dataColumn23.DataType = typeof(double);
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "VAT_TAXR";
            this.dataColumn24.DataType = typeof(double);
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "AM_TOTALR";
            this.dataColumn25.DataType = typeof(double);
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "NM_TAX";
            //// 
            // dataColumn29                                  CD_CC 데이터 칼럼 추가
            //// 
            this.dataColumn29.ColumnName = "CD_CC";
            this.dataColumn29.DataType = typeof(string);

            // dataTable2
            // 

            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn27,
            this.dataColumn28});
            this.dataTable2.TableName = "Table2";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "CD_COMPANY";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "CD_BIZAREA";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "NO_BIZAREA";
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "DT_PROCESS";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "TP_SUMTAX";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "FG_FGTAXP";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "CD_DEPT";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "NO_EMP";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "ID_USER";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "YN_PURSUB";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "MODULE";
            // 
            // m_gridTmp
            // 
            this.m_gridTmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridTmp.Controls.Add(this.btn_TAXPAPER);
            this.m_gridTmp.Controls.Add(this.btn_APPLY_GI);
            this.m_gridTmp.Location = new System.Drawing.Point(546, 3);
            this.m_gridTmp.Name = "m_gridTmp";
            this.m_gridTmp.Size = new System.Drawing.Size(244, 25);
            this.m_gridTmp.TabIndex = 135;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_gridTmp, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlGrid, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 134;
            // 
            // P_PU_IV_REG
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_PU_IV_REG";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_M_IV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_T_IV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_PO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            this.m_gridTmp.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
				//페이지를 로드 하는중입니다.
				this.ShowStatusBarMessage(1);
				this.SetProgressBarValue(100, 10);

				InitControl();
				this.SetProgressBarValue(100, 50);
				InitGrid();
								
				this.SetProgressBarValue(100, 70);			
				this.SetProgressBarValue(100, 100);		
				Application.DoEvents();

			}
			catch(coDbException ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.ToolBarSaveButtonEnabled = true;
				this.ToolBarAddButtonEnabled = true;
			
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

				
				lb_DT_IV.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","DT_IV");
			
				lb_FG_PO_TR.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","FG_PO_TR");
				lb_NM_BIZAREA.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_BIZAREA");
				lb_NO_EMP.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NO_EMP");
			

			
				rbtn_M_IV.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","M_IV");
				rbtn_T_IV.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","T_IV");

				tb_DT_PO.Mask = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				tb_DT_PO.ToDayDate = MainFrameInterface.GetDateTimeToday();


				//	btn_.Text = "세금계산서";//this.MainFrameInterface.GetDataDictionaryItem("PU","세금계산서");
				btn_APPLY_GI.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","APPLY_GI");	

				btn_TAXPAPER.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","TAX_DOCU");


				tb_NO_EMP.CodeName= this.LoginInfo.EmployeeName;	
				tb_NO_EMP.CodeValue= this.LoginInfo.EmployeeNo;
				_cddept = this.LoginInfo.DeptCode;
				tb_DT_PO.Text = this.MainFrameInterface.GetStringToday;
				btn_TAXPAPER.Enabled = false;
				rbtn_T_IV.Checked = true;
				SetControlEnabled(true);
			
			
			}
			catch(Exception ex)
			{
				throw ex;
			}
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
					
					case "NO_IV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NO_IV");
						break;
					case "CD_PARTNER":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","CD_PARTNER");
						break;
					case "LN_PARTNER":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PARTNER");
						break;
					case "NO_BIZAREA":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NO_COMPANY");
						break;
					case "NM_TAX":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","FG_TAX");
						break;
					case "AM_K":		
						temp = temp + " + " + this.GetDataDictionaryItem(DataDictionaryTypes.PU, "AM_IV");
						break;
					case "VAT_TAX":		
						temp = temp + " + " + this.GetDataDictionaryItem(DataDictionaryTypes.PU, "VAT");
						break;	
					case "AM_TOTAL":		
						temp = temp + " + " + this.GetDataDictionaryItem(DataDictionaryTypes.PU, "AM_TOTAL");
						break;
  
				}
			}
			
			if(temp == "")
				return "";
			else
				return temp.Substring(3,temp.Length-3);
		}


		#endregion

		#region -> InitGrid

		private void InitGrid()
		{
			Application.DoEvents();
			
			_flex = new Dass.FlexGrid.FlexGrid();

			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.BackColor = System.Drawing.SystemColors.Window;
			this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(785, 553);
			this._flex.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{Font:굴림체, 9pt;Trimming:EllipsisCharacter;}	Fixed{BackColor:Control;ForeColor:ControlText;TextAlign:CenterCenter;Trimming:EllipsisCharacter;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
			this._flex.TabIndex = 0;
			m_pnlGrid.Controls.Add(this._flex);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();


			_flex.Redraw = false;

			_flex.Rows.Count = 1;			// 총 Row 수
			_flex.Rows.Fixed = 1;			// FixedRow 수
			_flex.Cols.Count = 9;			// 총 Col 수
			_flex.Cols.Fixed = 1;			// FixedCol 수			
			_flex.Rows.DefaultSize = 20;	

			

			_flex.Cols[0].Width = 50;	
	
		
					
			_flex.Cols[1].Name = "NO_IV";
			_flex.Cols[1].DataType = typeof(string);
			_flex.Cols[1].Width = 120;		
			_flex.Cols[1].AllowEditing = false;
			_flex.SetColMaxLength("CD_ITEM",20);
			

			
			_flex.Cols[2].Name = "CD_PARTNER";
			_flex.Cols[2].DataType = typeof(string);
			_flex.Cols[2].AllowEditing = false;
			_flex.Cols[2].Width = 80;

			_flex.Cols[3].Name = "LN_PARTNER";
			_flex.Cols[3].DataType = typeof(string);
			_flex.Cols[3].AllowEditing = false;
			_flex.Cols[3].Width = 150;

			_flex.Cols[4].Name = "NO_BIZAREA";
			_flex.Cols[4].DataType = typeof(string);
			_flex.Cols[4].AllowEditing = false;
			_flex.Cols[4].Width = 100;
			_flex.Cols[4].EditMask = "999-99-99999";	
			_flex.Cols[4].Format = "###-##-#####";
			_flex.SetStringFormatCol("NO_BIZAREA");

				
			_flex.Cols[5].Name = "NM_TAX";
			_flex.Cols[5].DataType = typeof(string);
			_flex.Cols[5].AllowEditing = false;
			_flex.Cols[5].Width = 100;
            			
					
			_flex.Cols[6].Name = "AM_K";
			_flex.Cols[6].DataType = typeof(decimal);
			_flex.Cols[6].Width = 120;
			//	_flex.Cols[6].AllowEditing = false;
			_flex.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[6].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_K",17);
			
			
			_flex.Cols[7].Name = "VAT_TAX";
			_flex.Cols[7].DataType = typeof(decimal);
			_flex.Cols[7].Width = 120;	
			//_flex.Cols[7].AllowEditing = false;
			_flex.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[7].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("VAT_TAX",17);


			_flex.Cols[8].Name = "AM_TOTAL";
			_flex.Cols[8].DataType = typeof(decimal);
			_flex.Cols[8].Width = 120;	
			_flex.Cols[8].AllowEditing = false;
			_flex.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[8].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_TOTAL",17);

      							
			_flex.AllowSorting = AllowSortingEnum.None;		
		
			_flex.NewRowEditable = false;
			_flex.EnterKeyAddRow = false;

			_flex.GridStyle = GridStyleEnum.Green;
		
			this.SetUserGrid(this._flex);	
		
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flex.Cols.Count-1; i++)
				_flex[0, i] = GetDDItem(_flex.Cols[i].Name);

			_flex.Redraw = true;

		//	_flex.AddRow += new System.EventHandler(this.btn_Insert_Click);
		//	_flex.HelpClick += new System.EventHandler(this.OnShowHelp);
			_flex.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flex_ValidateEdit);
		//	_flex.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler( _flex_AfterRowColChange);
		
			
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
				
					//					this.m_lblTitle.Visible = true;
					//					this.m_lblTitle.Text = this.PageName;
					//					this.m_lblTitle.Show();		

				
					// 콤보박스 초기화
					InitCombo();

					_flex.Redraw = false;
					_flex.BindingStart();
					_flex.DataSource = new DataView(ds_Ty1.Tables[0]);
					_flex.BindingEnd();
					_flex.Redraw = true;					
				
					this.Enabled = true;									
					cbo_CD_BIZAREA.Focus();
					
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

//				string[] lsa_args = {"N;PU_C000016", "C;CC_MA_COMMON014","B_N;"};
//				object[] args = { this.LoginInfo.CompanyCode, lsa_args};
//				_dsCombo = (DataSet)MainFrameInterface.InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);
				
				_dsCombo = this.GetComboData("N;PU_C000016","S;MA_CODEDTL_003","NC;MA_BIZAREA");
			

				DataTable ldt_FG_TRANS = _dsCombo.Tables[0].Clone(); 
				DataRow[] ldr_FgTrans = _dsCombo.Tables[0].Select("CODE IN ('001', '002', '003')");

				if( ldr_FgTrans != null && ldr_FgTrans.Length >0)
				{
					for(int i = 0 ; i < ldr_FgTrans.Length ;i++)
					{
						ldt_FG_TRANS.ImportRow(ldr_FgTrans[i]);
					}
				}

				// 거래구분		
				cbo_FG_TRANS.DataSource = ldt_FG_TRANS;
				cbo_FG_TRANS.DisplayMember = "NAME";
				cbo_FG_TRANS.ValueMember = "CODE";

				// 사업장		
				cbo_CD_BIZAREA.DataSource = _dsCombo.Tables[2];
				cbo_CD_BIZAREA.DisplayMember = "NAME";
				cbo_CD_BIZAREA.ValueMember = "CODE";
			//	gdt_TaxCombo = _dsCombo.Tables[1].Copy();
			
				this.Enabled = true;
				
			
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
				this.Enabled = true;	
			
				this.ToolBarSearchButtonEnabled = true;				
				

				this.Cursor = Cursors.Default;
			}
		}


		#endregion

		#endregion

        #region ♣ 저장관련
	
		#region -> IsChanged

		private bool IsChanged(string gubun)
		{
			if( _dt_Line == null || _dt_Line.Rows.Count <= 0 )
			{
				return false;
			}
			return true;
		}
		#endregion

		#region -> MsgAndSave

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
				result = result = this.ShowMessage("QY3_002");// this.ShowMessageBox(1001, this.PageName);	
				if(result == DialogResult.No)
					return true;
				if(result == DialogResult.Cancel)
					return false;
			}
			else
			{
				
				result = this.ShowMessage("QY2_001"); //MessageBoxEx.Show(this.GetMessageDictionaryItem("MA_M000073"), this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);			
				if(result == DialogResult.No)
					return true;
			}

			Application.DoEvents();		// 대화상자 즉시 사라지게

			// "예"를 선택한 경우
			if(IsChanged(null)) isSaved = Save();

			return isSaved;
		}

		#endregion

		#region -> Check

		private bool Check()
		{
			return true;
		}

		#endregion

		#region -> Save
		
		/// <summary>
		/// 저장 함수
		/// </summary>
		/// <returns></returns>
		private bool Save()
		{			
			try
			{
				tb_DT_PO.Focus();
								

				if(!Check())		// 널값체크 및 중복값체크 등에서 문제가 발생한 경우
					return false;

			
				// 저장이 잘되면
				InDataHeadValue();

				
//				master.ICC_MA_SEQ m_Seq = new master.CC_MA_SEQ();
//
//				object[] ldbj_noiv  = m_Seq.CreateSeqnoArray(pdt_INFO.Rows[0]["CD_COMPANY"].ToString() ,"PU08","",_flex.DataTable.Rows.Count);									
			
				// take adventage of 이용하다.
				_flex.Redraw = false;

				DataRow[] rows = (System.Data.DataRow[])this.GetSeq(this.LoginInfo.CompanyCode,"PU","09",tb_DT_PO.Text.Substring(0,6), _flex.DataTable.Rows.Count);
				
		
				for( int i=0 ; i < _flex.DataTable.Rows.Count;i++)
				{						
					_flex.DataTable.Rows[i]["NO_IV"] = rows[i]["DOCU_NO"].ToString();
										
					DataRow[] ldr_temp = _dt_Line.Select("NO_TEMP='"+_flex.DataTable.Rows[i]["NO_TEMP"].ToString()+"'");				
					for(int j=0 ; j < ldr_temp.Length ; j++)
					{
						try
						{			
							ldr_temp[j].BeginEdit();
							ldr_temp[j]["NO_IV"] = rows[i]["DOCU_NO"].ToString();													
							ldr_temp[j]["NO_IVLINE"] = j+1;
							ldr_temp[j].EndEdit();
						}
						catch
						{
						}
					}								
				}

				SpInfoCollection sic = new SpInfoCollection();

				Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
				si.DataState = DataValueState.Added;
				si.DataValue =_flex.DataTable; 					//저장할 데이터 테이블
				si.SpNameInsert = "UP_PU_IVH_INSERT";			//Insert 프로시저명
								
				/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
				si.SpParamsInsert = new string[] { "NO_IV", "CD_COMPANY1", "CD_BIZAREA1", "NO_BIZAREA1", "FG_TPPURCHASE1", "CD_PARTNER", "FG_TRANS", "AM_K", "VAT_TAX", "DT_PROCESS1", 
													"FG_TAX", "TP_FD1", "TP_SUMTAX1", "FG_TAXP1", "CD_DEPT1", "NO_EMP1", "YN_PURSUB1", "YN_EXPIV1", "DTS_INSERT1", "ID_INSERT1" };
				
				/*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
				si.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", ds_Ty1.Tables[1].Rows[0]["CD_COMPANY"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA1", ds_Ty1.Tables[1].Rows[0]["CD_BIZAREA"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "NO_BIZAREA1", ds_Ty1.Tables[1].Rows[0]["NO_BIZAREA"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "DT_PROCESS1", ds_Ty1.Tables[1].Rows[0]["DT_PROCESS"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "FG_TPPURCHASE1", "");
				si.SpParamsValues.Add(ActionState.Insert, "TP_FD1", "D");				
				si.SpParamsValues.Add(ActionState.Insert, "TP_SUMTAX1", ds_Ty1.Tables[1].Rows[0]["TP_SUMTAX"].ToString());

				si.SpParamsValues.Add(ActionState.Insert, "FG_TAXP1",  ds_Ty1.Tables[1].Rows[0]["FG_FGTAXP"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "CD_DEPT1", ds_Ty1.Tables[1].Rows[0]["CD_DEPT"].ToString());				
				si.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", ds_Ty1.Tables[1].Rows[0]["NO_EMP"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "YN_PURSUB1", ds_Ty1.Tables[1].Rows[0]["YN_PURSUB"].ToString());				
				si.SpParamsValues.Add(ActionState.Insert, "YN_EXPIV1", "N");
				si.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
				si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID);
                			
				sic.Add(si);


				si = new Duzon.Common.Util.SpInfo();
				si.DataState = DataValueState.Added;
				si.DataValue = _dt_Line; 					//저장할 데이터 테이블
				si.SpNameInsert = "UP_PU_IVL_INSERT";			//Insert 프로시저명
										
				/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
				si.SpParamsInsert = new string[] { 	"NO_IV", "NO_IVLINE", "CD_COMPANY1", "CD_PLANT", "NO_IO", "NO_IOLINE", "CD_ITEM",
												"CD_GROUP", "CD_CC1", "DT_TAX1", "QT_IV", "UM", "AM_IV", "VAT_IV", "NO_EMP1", "CD_PJT",  
												"FG_TPPURCHASE", "NO_PO", "NO_POLINE", "CD_EXCH", "RT_EXCH", "YN_RETURN", "UM_EX", "AM_EX", 
												"QT_CLS", "NO_LC", "NO_LCLINE" , "CD_QTIOTP", "YN_PURSUB1","CD_PARTNER","CD_BIZAREA1","FG_TRANS1"};

							
				si.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", ds_Ty1.Tables[1].Rows[0]["CD_COMPANY"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "DT_TAX1", ds_Ty1.Tables[1].Rows[0]["DT_PROCESS"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", ds_Ty1.Tables[1].Rows[0]["NO_EMP"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "CD_CC1", ds_Ty1.Tables[0].Rows[0]["CD_CC"].ToString());            //  CD_CC 추가
				si.SpParamsValues.Add(ActionState.Insert, "YN_PURSUB1", ds_Ty1.Tables[1].Rows[0]["YN_PURSUB"].ToString());	
				si.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA1", ds_Ty1.Tables[1].Rows[0]["CD_BIZAREA"].ToString());
				si.SpParamsValues.Add(ActionState.Insert, "FG_TRANS1", cbo_FG_TRANS.SelectedValue.ToString());
				sic.Add(si);

				object obj = this.Save(sic); 
				ResultData[] result = (ResultData[])obj;
				if(result[0].Result && result[1].Result)
				{
					this.ToolBarSaveButtonEnabled = false;
					this.ToolBarAddButtonEnabled = true;
					btn_APPLY_GI.Enabled = false;
					btn_TAXPAPER.Enabled = true;									
														
					_flex.AllowEditing = false;	
					_flex.Redraw = true;
					return true;			
				}			
				

				SetSaveFalse();
				return false;

				


				/*
				object[] m_obj = {ds_Ty1.Tables[1],_flex.DataTable,_dt_Line};

				object[] m_result = (object[])(this.MainFrameInterface.InvokeRemoteMethod("PurPurchaseControl", "pur.CC_PU_IV","CC_PU_IV.rem", "SavePuIVREQ", m_obj));							
		
				if((int)m_result[0] >=0)
				{              	
					
					this.ToolBarSaveButtonEnabled = false;
					this.ToolBarAddButtonEnabled = true;
					btn_APPLY_GI.Enabled = false;
					btn_TAXPAPER.Enabled = true;					
				
					object[] lobj = (object[])m_result[1];
					for(int i=0 ; i < _flex.DataView.Count ; i++)
					{
						_flex.DataView[i].BeginEdit();
						_flex.DataView[i]["NO_IV"] = lobj[i];
						_flex.DataView[i].EndEdit();
					}

					_flex.AllowEditing = false;	

					return true;

				}	
				return false;
				*/
			}
			catch(coDbException ex)
			{
				SetSaveFalse();
				this.ShowErrorMessage(ex, this.PageName);
			}
			catch(Exception ex)
			{
				SetSaveFalse();
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				_flex.Redraw = true;
				Cursor.Current = Cursors.Default;
			}
			return false;
		}

		private void SetSaveFalse()
		{
			try
			{
				for( int i=0 ; i < _flex.DataTable.Rows.Count;i++)
				{
					_flex.DataTable.Rows[i].BeginEdit();
					_flex.DataTable.Rows[i]["NO_IV"] = "";	
					_flex.DataTable.Rows[i].EndEdit();	
				}
			}
			catch
			{
			}
		}

		#endregion

		#endregion

		#region ♣ 메인버튼 이벤트
		
		#region -> DoContinue

		private bool DoContinue()
		{
			if(_flex.Editor != null)
			{
				return _flex.FinishEditing(false);
			}
			
			return true;
		}

		#endregion

		#region -> 저장버튼클릭	
		/// <summary>
		/// 브라우저의 저장 버턴 클릭시 처리부
		/// </summary>
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			
			try
			{
				if(!DoContinue())
					return;
				Cursor.Current = Cursors.WaitCursor;
				
				if(MsgAndSave(false, false))
				{
					this.ShowMessage("IK1_001");
												
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
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion
	
		#region -> 추가버튼클릭
		/// <summary>
		/// 브라우저의 추가 버턴 클릭시 처리부
		/// </summary>
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{	
			
			try
			{
				FieldDataNULL();
				
			}			
			finally
			{
			}
		}	

		#endregion
	
		#region -> 종료버튼클릭
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
							
			try
			{

				if(_flex.DataTable != null && _flex.DataTable.Rows.Count >0)
				{
					DataRow[] m_rows = _flex.DataTable.Select( " NO_IV = '' OR NO_IV IS NULL ");

					if( m_rows != null && m_rows.Length >0)
					{
						if(!DoContinue())
							return false;

						if(!MsgAndSave(true,true))	// 저장이 실패하면
							return false;
					}					
				}

				return true;
							
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.ShowStatusBarMessage(0);
				this.SetProgressBarValue(100 ,0);
			}

			return true;
		}

		#endregion
	
		#endregion          
    
		#region ♣ 그리드 이벤트 / 메서드

		#region -> _flex_ValidateEdit
		private void _flex_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
		{
			try
			{
					
				if( _flex.AllowEditing )
				{			//	System.Diagnostics.Debugger.Break();
					if( _flex.GetData(e.Row,e.Col).ToString() != _flex.EditData)
					{
						
						switch (_flex.Cols[e.Col].Name)
						{
							case "AM_K":
								ChangeAM_K(System.Double.Parse(_flex.EditData));
								break;	
							case "VAT_TAX" :	
								ChangeVAT_K(System.Double.Parse(_flex.EditData));	
								break;
						}
					}
				}	
			}
			catch
			{
			}

		}
		#endregion

		#region -> 금액변경 ( ChangeAM_K )
		/// <summary>
		/// 금액변경
		/// </summary>
		/// <param name="pdb_amk"></param>
		private void ChangeAM_K(double pdb_amk)
		{
			try
			{

				double ldb_kwN=0; 

				
				string ls_NO_TEMP = _flex[_flex.Row,"NO_TEMP"].ToString();
				string ls_filter="";		
		
				//				ls_filterN = "NO_TEMP= '" +ls_NO_TEMP + "' AND YN_RETURN = 'N'";	
				//				ls_filterY = "NO_TEMP= '" +ls_NO_TEMP + "' AND YN_RETURN = 'Y'";		
				ls_filter = "NO_TEMP= '" +ls_NO_TEMP + "'";		
			
				
				try
				{
					try
					{
						ldb_kwN= (double)(System.Decimal)_dt_Line.Compute("SUM(AM_IV)",ls_filter);
					}
					catch
					{
					}

				}
				catch
				{
				}

				double gab = pdb_amk - ldb_kwN;//  ( ldb_kwN - ldb_kwY );

				DataRow[] ldr_rows = _dt_Line.Select(ls_filter);
				if(ldr_rows !=null && ldr_rows.Length >0)
				{
					ldr_rows[ldr_rows.Length-1].BeginEdit();
					
					ldr_rows[ldr_rows.Length-1]["AM_IV"] = System.Math.Floor( System.Double.Parse( ldr_rows[ldr_rows.Length-1]["AM_IV"].ToString()) +  gab);

					ldr_rows[ldr_rows.Length-1]["UM"] = System.Double.Parse( ldr_rows[ldr_rows.Length-1]["AM_IV"].ToString())  / System.Double.Parse( ldr_rows[ldr_rows.Length-1]["QT_IV"].ToString()) ;						
					ldr_rows[ldr_rows.Length-1]["AM_EX"] = (System.Double.Parse( ldr_rows[ldr_rows.Length-1]["AM_IV"].ToString()) ) / System.Double.Parse( ldr_rows[ldr_rows.Length-1]["RT_EXCH"].ToString());			
					ldr_rows[ldr_rows.Length-1]["UM_EX"] = (System.Double.Parse( ldr_rows[ldr_rows.Length-1]["UM"].ToString().Trim())) / System.Double.Parse( ldr_rows[ldr_rows.Length-1]["RT_EXCH"].ToString()) ;						
								
					ldr_rows[ldr_rows.Length-1].EndEdit();

				
					_flex[_flex.Row,"AM_TOTAL"] = (pdb_amk) + System.Double.Parse(_flex[_flex.Row,"VAT_TAX"].ToString().Trim());
				


				}			
	
					
                				
			}
			catch
			{				
			}
		}
		#endregion

		#region -> 부가세 변경 ( ChangeVAT_K )
		/// <summary>
		/// 부가세 변경
		/// </summary>
		/// <param name="pdb_vatk"></param>
		private void ChangeVAT_K(double pdb_vatk)
		{
			try
			{
				double ldb_kwN=0; 

			
				string ls_NO_TEMP = _flex[_flex.Row,"NO_TEMP"].ToString();
				string ls_filter="";		
		
				ls_filter = "NO_TEMP= '" +ls_NO_TEMP + "'";		
							
				try
				{
					try
					{
						ldb_kwN= (double)(System.Decimal)_dt_Line.Compute("SUM(VAT_IV)",ls_filter);
					}
					catch
					{
					}					
				}
				catch
				{
				}
				double gab = pdb_vatk - ldb_kwN;// ( ldb_kwN - ldb_kwY );

				DataRow[] ldr_rows = _dt_Line.Select(ls_filter);
				if(ldr_rows !=null && ldr_rows.Length >0)
				{
					ldr_rows[ldr_rows.Length-1].BeginEdit();
					ldr_rows[ldr_rows.Length-1]["VAT_IV"] = System.Math.Floor( System.Double.Parse( ldr_rows[ldr_rows.Length-1]["VAT_IV"].ToString()) +  gab);			
									ldr_rows[ldr_rows.Length-1].EndEdit();
					
					_flex[_flex.Row,"AM_TOTAL"] = (pdb_vatk) + System.Double.Parse(_flex[_flex.Row,"AM_K"].ToString().Trim());
				

				}				
			}
			catch
			{				
			}
		}

	
		#endregion		

		#endregion

		#region ♣ 기타 이벤트

		#region -> 입고적용 버턴(btn_APPLY_GI_Click)
		/// <summary>
		/// 입고적용 버턴 클리
		/// 도움창에서 마감할 항목 선택하여
		/// 그리드(계산서)에 뿌려줌
		/// 라인은 글로벌변수로 가주고 있음
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_APPLY_GI_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!FieldCheck())
				{
					return;
				}

				if(cbo_FG_TRANS.SelectedValue.ToString().Trim() == "001")
				{

					Cursor.Current = Cursors.WaitCursor;
					pur.P_PU_GIIV_SUB m_dlg = new pur.P_PU_GIIV_SUB(this.MainFrameInterface,_dt_Line);

					//	Cursor.Current = Cursors.Default;
					// 일괄이면
					if( rbtn_T_IV.Checked)
					{
						m_dlg.gs_FG_TAXP = "001";
					}
					else// 건별이면
					{
						m_dlg.gs_FG_TAXP = "002";
					}// 장두영 사원번호 : 02B01692 수정번호 : 1010813

					m_dlg.gs_FG_TRANS = cbo_FG_TRANS.SelectedValue.ToString();
					m_dlg.gdt_RateVAT = _dsCombo.Tables[1].Copy();					
					m_dlg.gs_FGTAX ="";
					m_dlg.gs_CD_BIZAREA = cbo_CD_BIZAREA.SelectedValue.ToString();
					Cursor.Current = Cursors.Default;

					if(m_dlg.ShowDialog(this) == DialogResult.OK)
					{			
						Cursor.Current = Cursors.WaitCursor;

						SettingDiviedData(m_dlg.gdt_return,rbtn_M_IV.Checked);

						PageControlEnalbed(false);
								
					}			
				}
				else if(cbo_FG_TRANS.SelectedValue.ToString().Trim() == "002")
				{

					Cursor.Current = Cursors.WaitCursor;
					pur.P_PU_GIIV_SUB1 m_dlg = new pur.P_PU_GIIV_SUB1(this.MainFrameInterface,_dt_Line);
				
					//	m_dlg.gs_FG_TAXP = "001', '002";

					// 일괄이면
					if( rbtn_T_IV.Checked)
					{
						m_dlg.gs_FG_TAXP = "001";
					}
					else// 건별이면
					{
						m_dlg.gs_FG_TAXP = "002";
					}


					m_dlg.gs_FG_TRANS = cbo_FG_TRANS.SelectedValue.ToString();
					m_dlg.gdt_RateVAT = _dsCombo.Tables[1].Copy();					
					m_dlg.gs_FGTAX ="";
					m_dlg.gs_CD_BIZAREA = cbo_CD_BIZAREA.SelectedValue.ToString();
					Cursor.Current = Cursors.Default;

					if(m_dlg.ShowDialog(this) == DialogResult.OK)
					{			
						Cursor.Current = Cursors.WaitCursor;
						SettingDiviedData(m_dlg.gdt_return,rbtn_M_IV.Checked);
						PageControlEnalbed(false);					
					}			
				}
				else if(cbo_FG_TRANS.SelectedValue.ToString().Trim() == "003")
				{
					Cursor.Current = Cursors.WaitCursor;

					pur.P_PU_GIIVLC_SUB m_dlg = new pur.P_PU_GIIVLC_SUB(this.MainFrameInterface,_dt_Line);
					m_dlg.gs_FG_TAXP = "001', '002";				
					m_dlg.gs_FG_TRANS = cbo_FG_TRANS.SelectedValue.ToString();
					m_dlg.gdt_RateVAT = _dsCombo.Tables[1].Copy();
					
					m_dlg.gs_FGTAX ="";
					m_dlg.gs_CD_BIZAREA = cbo_CD_BIZAREA.SelectedValue.ToString();

					Cursor.Current = Cursors.Default;

					if(m_dlg.ShowDialog(this) == DialogResult.OK)
					{			
						Cursor.Current = Cursors.WaitCursor;
						SettingDiviedData(m_dlg.gdt_return,false);
						PageControlEnalbed(false);			
		
					}			
				}
			}			
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}


		#endregion 

		#region -> btn_TAXPAPER_Click

		private void btn_TAXPAPER_Click(object sender, System.EventArgs e)
		{
			try
			{				
				object[] args = new object[5];	
				args[0] = cbo_CD_BIZAREA.SelectedValue.ToString();
				args[1] = tb_NO_EMP.CodeValue.ToString();
				args[2] = tb_NO_EMP.CodeName;
				args[3] = cbo_FG_TRANS.SelectedValue.ToString();
				args[4] = tb_DT_PO.MaskEditBox.ClipText;
				string ls_pagename = MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU, "P_PU_IV_MNG");
					
				if(MainFrameInterface.IsExistPage("P_PU_IV_MNG", false))
				{
					//MessageBox.Show("라인 페이지 죽이기"); merge
					//- 특정 페이지 닫기
					MainFrameInterface.UnLoadPage("P_PU_IV_MNG", false);
				}					

				MainFrameInterface.LoadPageFrom("P_PU_IV_MNG", ls_pagename, Grant, args);
			}
			catch
			{
			}
		}
		#endregion 

		#region -> tb_COMBO_KeyDown


		private void tb_COMBO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData.ToString() =="Enter")
			{	
				SendKeys.SendWait("{TAB}");	
			}		
		}

		
		#endregion

		
		#region -> 날짜 에 관련된 함수 및 이벤트

		private void DataPickerValidated(object sender, System.EventArgs e)
		{
			try
			{                
				if(!((DatePicker)sender).Modified)
					return;

				if(((DatePicker)sender).Text == string.Empty)
					return;

				// 유효성 검사
				if(!((DatePicker)sender).IsValidated)
				{
					this.ShowMessage("WK1_003");
					((DatePicker)sender).Text = string.Empty;
					((DatePicker)sender).Focus();
					return;
				}
                                                
				//				if(this.m_dtpFrom.Text != string.Empty && this.m_dtpTo.Text != string.Empty)
				//				{
				//					// From To 체크
				//					CommonFunction objComm = new CommonFunction();
				//					if(objComm.DiffDate(this.m_dtpFrom.Text, this.m_dtpTo.Text) > 0)
				//					{
				//						this.ShowMessage("WK1_007", GetDDItem("DT_DISCIP") );
				//						this.m_dtpTo.Focus();
				//						return;
				//					}
				//				}

			}
			catch
			{
			}		
		}

		
		
		#endregion
		
		#endregion

		#region ♣ 기타 함수

        #region -> SetControlEnabled
		/// <summary>
		/// 각 Control들의 상태 설정
		/// </summary>
		/// <param name="pb_enabled"></param>
		private void SetControlEnabled(bool pb_enabled)
		{
			cbo_CD_BIZAREA.Enabled = pb_enabled;		
			tb_NO_EMP.Enabled = pb_enabled;
			cbo_FG_TRANS.Enabled = pb_enabled;
		}

		#endregion

		#region -> InDataHeadValue
		private void InDataHeadValue()
		{	

			DataRow newrow;
			ds_Ty1.Tables[1].Clear();

			newrow = ds_Ty1.Tables[1].NewRow();
			newrow["CD_BIZAREA"] = cbo_CD_BIZAREA.SelectedValue.ToString();
			newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;			
			ds_Ty1.Tables[1].Rows.Add(newrow);
		
			ds_Ty1.Tables[1].BeginInit();
			DataRow ldr_row = ds_Ty1.Tables[1].Rows[0];

			string ls_nobizarea = "";
			try
			{
				DataRow[] ldrs_row =  _dsCombo.Tables[2].Select("CODE = '"+cbo_CD_BIZAREA.SelectedValue.ToString()+"'");
				if(ldrs_row != null && ldrs_row.Length >0)
				{
					ls_nobizarea = ldrs_row[0]["NO_BIZAREA"].ToString();
				}
			}
			catch
			{
			}

			
			ldr_row["NO_BIZAREA"] = ls_nobizarea;			
			ldr_row["DT_PROCESS"] =  tb_DT_PO.MaskEditBox.ClipText;
			ldr_row["TP_SUMTAX"] = "S";		
			ldr_row["CD_DEPT"] = _cddept;
			ldr_row["NO_EMP"] = tb_NO_EMP.CodeValue.ToString();		
			ldr_row["ID_USER"] = this.LoginInfo.UserID;	
			ldr_row["YN_PURSUB"] = "N";	
			ldr_row["MODULE"] = "PU";	

			ldr_row["FG_FGTAXP"] = "001";

			if( rbtn_M_IV.Checked)
			{
				ldr_row["FG_FGTAXP"] ="002";
			}						

			ds_Ty1.Tables[1].EndInit();

		}

		#endregion

		#region -> FieldDataNULL
		/// <summary>
		/// 필드값 초기화
		/// </summary>
		private void FieldDataNULL()
		{			
			try
			{
				lb_FG_PO_TR.Focus();

				tb_NO_EMP.Text= this.LoginInfo.EmployeeName;	
				tb_NO_EMP.Tag= this.LoginInfo.EmployeeNo;
				_cddept = this.LoginInfo.DeptCode;

				rbtn_M_IV.Enabled = true;
				rbtn_T_IV.Enabled = true;

				btn_APPLY_GI.Enabled = true;
				btn_TAXPAPER.Enabled = false;

				SetControlEnabled(true);
			
				tb_DT_PO.Text = this.MainFrameInterface.GetStringToday;
			
				this.ToolBarSaveButtonEnabled = true;
				
				
				_dt_Line.Clear();
				
				_flex.AllowEditing = true;

				_flex.DataTable.Clear();
			}
			catch
			{
			}
		}

		#endregion

		#region -> PageControlEnalbed
		
		private void PageControlEnalbed(bool pb_enabled)
		{
			try
			{
				cbo_CD_BIZAREA.Enabled = pb_enabled; 
				cbo_FG_TRANS.Enabled = pb_enabled; 
				rbtn_M_IV.Enabled = pb_enabled; 
				rbtn_T_IV.Enabled = pb_enabled; 
			}
			catch
			{
			}
		}


		#endregion

		#region -> 과세구분 변경과 과세비율 설정

		/// <summary>
		/// 과세구분 변경
		/// </summary>
		private void SetChageFG_TAX()
		{
			
			try
			{		
				Cursor.Current = Cursors.WaitCursor;
			
				DataRow[] ldr_temp;

				ldr_temp = _dt_Line.Select("NO_TEMP='"+_flex[_flex.Row,"NO_TEMP"].ToString()+"'");

													
				double ldb_vat = SettingTbTax(_flex[_flex.Row,"FG_TAX"].ToString());		
				double ldb_TatalVAT =0;
				for(int j=0 ; j < ldr_temp.Length ; j++)
				{
					try
					{						
						ldr_temp[j]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[j]["AM_IV"].ToString())*ldb_vat*0.01);														
						ldb_TatalVAT += System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString());
					}
					catch
					{
					}
				}	

				_flex[_flex.Row,"VAT_TAX"] = ldb_TatalVAT;
				_flex[_flex.Row,"AM_TOTAL"] = ldb_TatalVAT + System.Double.Parse(_flex[_flex.Row,"AM_K"].ToString());
			}
			catch
			{				
			}
			finally
			{				
				Cursor.Current = Cursors.Default;
			}
		}

		/// <summary>
		/// 과세구분에 따른 과세 비율을 가져옴
		/// </summary>
		/// <param name="ps_taxp"></param>
		/// <returns></returns>
		private double SettingTbTax(string ps_taxp)
		{
			double ldb_vaterate=0;
			try
			{				

				DataRow[] lr_row = _dsCombo.Tables[1].Select("CODE ='"+ps_taxp+"'");

				if( lr_row !=null && lr_row.Length >0)
				{
					ldb_vaterate = System.Double.Parse( lr_row[0]["VAT_RATE"].ToString());

				}		

				return ldb_vaterate; //tb_TAX.Text = ldb_vaterate.ToString();
			}
			catch
			{
				return 0;
			}
			return ldb_vaterate;
		}

		#endregion 

		#region -> 필드 체크
		/// <summary>
		/// 필드 체크 하는 함수 -- 입고기간, 매입형태(차후), 거래처 
		/// </summary>
		/// <returns></returns>
		private bool FieldCheck()
		{			
			if(tb_NO_EMP.CodeValue.ToString() == "")
			{
				this.ShowMessage("WK1_004",lb_NO_EMP.Text);
			//	Duzon.Common.Controls.MessageBoxEx.Show(lb_NO_EMP.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
				tb_NO_EMP.Focus();
				return false;
			}
		
			return true;
		}

		#endregion

		#region -> 도움창과 관련있는 것들
	
		/// <summary>
		/// 입고적용에서 받아온 테이블 정보로 헤더 생성  
		/// </summary>
		/// <param name="pdt_Line"></param>
		/// <param name="is_individual"></param>
		private void SettingDiviedData(DataTable pdt_Line,bool is_individual)
		{
			try
			{
				// 받아온것 없으면..
				if( pdt_Line == null || pdt_Line.Rows.Count <=0 )
					return ;

				// 기존에 선택된 것이 없으면 
				if( _dt_Line == null || _dt_Line.Rows.Count <=0 )
				{
					_dt_Line = pdt_Line.Copy();
				}			
				else 
				{
					for(int i=0 ; i < _dt_Line.Rows.Count ; i++)
					{
						try
						{
							_dt_Line.Rows[i]["NO_TEMP"] ="";
						}
						catch
						{
						}
					}                    				

					for(int i=0 ; i < pdt_Line.Rows.Count ; i++)
					{
						try
						{						
							_dt_Line.ImportRow(pdt_Line.Rows[i]);
						}
						catch(Exception ex)
						{
							this.ShowErrorMessage(ex, this.PageName);
							//MessageBox.Show(ex.ToString());
						}
					}
				}

				///////////////////////////////////////////////////////////////
				///	헤더 생성 
				/// 
			
				// 헤더 CLEAR
				_flex.DataTable.Clear();

				_flex.Redraw = false;

				//건별이면
				if( is_individual)
				{
					long ll_temp =1 ;
					DataRow[] ldr_temp;
					for( int i = 0 ; i < _dt_Line.Rows.Count ;i++)
					{
						// 기존에 생성되지 않았으면
						if( _dt_Line.Rows[i]["NO_TEMP"].ToString().Trim() =="")
						{
							ldr_temp = _dt_Line.Select("NO_IO='"+_dt_Line.Rows[i]["NO_IO"].ToString()+"'");
							
					
							_flex.Rows.Add();
							_flex.Row = _flex.Rows.Count - 1;

							_flex[_flex.Row,"NO_IV"] = "";
							_flex[_flex.Row,"CD_PARTNER"] = _dt_Line.Rows[i]["CD_PARTNER"].ToString();
							_flex[_flex.Row,"LN_PARTNER"] = _dt_Line.Rows[i]["LN_PARTNER"].ToString();
							_flex[_flex.Row,"NO_BIZAREA"] = _dt_Line.Rows[i]["NO_COMPANY"].ToString();
							_flex[_flex.Row,"FG_TAX"] = _dt_Line.Rows[i]["FG_TAX"].ToString();
							_flex[_flex.Row,"NM_TAX"] = _dt_Line.Rows[i]["NM_TAX"].ToString();
							_flex[_flex.Row,"NO_TEMP"] = ll_temp.ToString();	
							_flex[_flex.Row,"FG_TRANS"] = cbo_FG_TRANS.SelectedValue.ToString();//YN_RETURN
                            _flex[_flex.Row, "CD_CC"] = _dt_Line.Rows[i]["CD_CC"].ToString();   //CD_CC 값 추가
							//	_flex[_flex.Row,"YN_RETURN"] = _dt_Line.Rows[i]["YN_RETURN"].ToString();;
		
							double ldb_amk=0;
							double ldb_vat=0;					

							for(int j=0 ; j < ldr_temp.Length ; j++)
							{
								try
								{
									ldr_temp[j]["NO_TEMP"] = ll_temp.ToString();
									ldb_amk += System.Double.Parse(ldr_temp[j]["AM_IV"].ToString());
									ldb_vat += System.Math.Floor(System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString()));									
																
								}
								catch
								{
								}
							}

							// 과세 이고.. 거래처데이블의 TP_TAX ==001
							if( _dt_Line.Rows[i]["TP_TAX"].ToString().Trim() == "001" && 
								System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()) >0)								
							{
								double ldb_VatRate =  System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim());	
								//	double ldb_tempVat = System.Math.Floor(ldb_amk *ldb_VatRate * 0.01);

								double ldb_tempVat = ldb_amk *ldb_VatRate * 0.01;

								if( ldb_tempVat < 0)
								{
									ldb_tempVat = System.Math.Ceiling(ldb_tempVat);
								}
								ldb_tempVat = System.Math.Floor(ldb_tempVat);
								ldr_temp[ldr_temp.Length-1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[ldr_temp.Length-1]["VAT_IV"].ToString())) + ( ldb_tempVat - ldb_vat);
							
								ldb_vat = ldb_tempVat;
							}				

							// 실제 DB에 들어 가는 값
							_flex[_flex.Row,"AM_K"] = ldb_amk;
							_flex[_flex.Row,"VAT_TAX"] = ldb_vat;
							_flex[_flex.Row,"AM_TOTAL"] = ldb_amk+ldb_vat;
						
							_flex.AddFinished();				
							_flex.Col = _flex.Cols.Fixed;	
							ll_temp++;	

						}
					}
				}
				else
				{
					DataRow[] ldr_temp;
					long ll_temp =1 ; 
					for( int i = 0 ; i < _dt_Line.Rows.Count ;i++)
					{
						if( _dt_Line.Rows[i]["NO_TEMP"].ToString().Trim() =="")
						{
							// LOCAL LC 이면 ( 반품 구분 하지 않음 )
							if( cbo_FG_TRANS.SelectedValue.ToString().Trim() == "003")
							{
								ldr_temp = _dt_Line.Select("CD_PARTNER='"+_dt_Line.Rows[i]["CD_PARTNER"].ToString()+"' AND NO_LC ='"+_dt_Line.Rows[i]["NO_LC"].ToString()+"' AND FG_TAX ='"+_dt_Line.Rows[i]["FG_TAX"].ToString()+"'");						
							}
							else // 국내, 구매승인서 일경우 ( 반품 구분 하지 않음 )
							{
								ldr_temp = _dt_Line.Select("CD_PARTNER='"+_dt_Line.Rows[i]["CD_PARTNER"].ToString()+"' AND FG_TAX ='"+_dt_Line.Rows[i]["FG_TAX"].ToString()+"'");							
							//	ldr_temp = _dt_Line.Select("CD_PARTNER='"+_dt_Line.Rows[i]["CD_PARTNER"].ToString()+"' AND FG_TAX ='"+_dt_Line.Rows[i]["FG_TAX"].ToString()+"' AND YN_RETURN ='"+_dt_Line.Rows[i]["YN_RETURN"].ToString()+"'");							
							}


							_flex.Rows.Add();
							_flex.Row = _flex.Rows.Count - 1;
							_flex[_flex.Row,"NO_IV"] = "";
							_flex[_flex.Row,"CD_PARTNER"] = _dt_Line.Rows[i]["CD_PARTNER"].ToString();
							_flex[_flex.Row,"LN_PARTNER"] = _dt_Line.Rows[i]["LN_PARTNER"].ToString();
							_flex[_flex.Row,"NO_BIZAREA"] = _dt_Line.Rows[i]["NO_COMPANY"].ToString();
							_flex[_flex.Row,"FG_TAX"] = _dt_Line.Rows[i]["FG_TAX"].ToString();
							_flex[_flex.Row,"NM_TAX"] = _dt_Line.Rows[i]["NM_TAX"].ToString();							
							_flex[_flex.Row,"NO_TEMP"] = ll_temp.ToString();	
							_flex[_flex.Row,"FG_TRANS"] = cbo_FG_TRANS.SelectedValue.ToString();
                            _flex[_flex.Row, "CD_CC"] = _dt_Line.Rows[i]["CD_CC"].ToString();     //CD_CC값 추가
							//	_flex[_flex.Row,"YN_RETURN"] = _dt_Line.Rows[i]["YN_RETURN"].ToString();
			
							double ldb_amk=0;
							double ldb_vat=0;

							for(int j=0 ; j < ldr_temp.Length ; j++)
							{
								try
								{									
									ldr_temp[j]["NO_TEMP"] = ll_temp.ToString();	
									ldb_amk += System.Double.Parse(ldr_temp[j]["AM_IV"].ToString());
									ldb_vat += System.Math.Floor(System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString()));										
								
								}
								catch
								{
								}
							}


							if( _dt_Line.Rows[i]["TP_TAX"].ToString().Trim() == "001" && 
								System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()) > 0)
							{
								double ldb_VatRate =  System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim());	
								double ldb_tempVat = ldb_amk *ldb_VatRate * 0.01;

								if( ldb_tempVat < 0)
								{
									ldb_tempVat = System.Math.Ceiling(ldb_tempVat);
								}

								ldb_tempVat = System.Math.Floor(ldb_tempVat);

								ldr_temp[ldr_temp.Length-1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[ldr_temp.Length-1]["VAT_IV"].ToString())) + ( ldb_tempVat - ldb_vat);							
								ldb_vat = ldb_tempVat;
							}
                            
							// 실제 DB에 들어 가는 값
							_flex[_flex.Row,"AM_K"] = ldb_amk;
							_flex[_flex.Row,"VAT_TAX"] = ldb_vat;
							_flex[_flex.Row,"AM_TOTAL"] = ldb_amk+ldb_vat;

							_flex.AddFinished();				
							_flex.Col = _flex.Cols.Fixed;	
							ll_temp++;		
						}
					}		
				} 
                			
			}
			catch
			{
			}
			finally
			{
				_flex.Redraw = true;
			}
		}		
		
		#endregion


		#endregion

        #region -> BpControl Event

		private void OnBpCodeTextBox_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			
			if(e.DialogResult == DialogResult.OK)
			{
				System.Data.DataRow[] rows = e.HelpReturn.Rows;
				switch(e.HelpID)
				{					
					case Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB:
						_cddept =  rows[0]["CD_DEPT"].ToString();
						break;				
					default:
						break;
				}
			}
		}

		
		#endregion
    }
}
