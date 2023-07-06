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
	// 작   성   자 : 김대영
	// 작   성   일 : 
	// 모   듈   명 : 구매/자재
	// 시 스  템 명 : 매입관리
	// 페 이 지  명 : 매입등록
	// 프로젝트  명 : P_PU_IV_MNG
	//********************************************************************
	/// </summary>
	public class P_PU_IV_MNG_BAK : Duzon.Common.Forms.PageBase
	{

		#region ♣ 멤버필드
		
		#region -> 멤버필드(일반)

        private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel3;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.LabelExt lb_ALO_DEPT;
		private Duzon.Common.Controls.LabelExt lb_NO_EMP;
		private Duzon.Common.Controls.LabelExt lb_FG_PO_TR;
		private Duzon.Common.Controls.LabelExt lb_DT_IV;
		private Duzon.Common.Controls.LabelExt lb_NM_PARTNER;
		private Duzon.Common.Controls.LabelExt lb_NM_BIZAREA;
		private Duzon.Common.Controls.LabelExt lb_PT_PURCHASE;
		private Duzon.Common.Controls.RoundedButton btn_IVPROCESS;
		private Duzon.Common.Controls.LabelExt label2;
		private System.ComponentModel.IContainer components;
		private Duzon.Common.Controls.DropDownComboBox cbo_CD_BIZAREA;
		private Duzon.Common.Controls.DropDownComboBox cbo_TRANS;
		private Duzon.Common.Controls.DropDownComboBox cbo_PT_PURCHASE;
		private Duzon.Common.Controls.PanelExt m_pnlGrid2;
		private Duzon.Common.Controls.PanelExt m_pnlGrid1;
		private Duzon.Common.Controls.DatePicker mtb_DT_PO1;
		private Duzon.Common.Controls.DatePicker mtb_DT_PO2;
	
		private System.Data.DataSet ds_Ty1;
		private System.Data.DataTable dataTable1;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Windows.Forms.ContextMenu PupUpMenu;	

		#endregion

		#region -> 멤버필드(주요)	
		// 그리드 헤더
		private Dass.FlexGrid.FlexGrid _flexM;		
		// 그리드 라인
		private Dass.FlexGrid.FlexGrid _flexD;

		//페이팅
		private bool _isPainted = false;

		// 매입등록 화면에서 받는 정보
		string _cdbizarea ="", _noemp ="", _nmemp="",_fg_trans ="",_dtpo="";
		bool _isCallIV = false;

				
		#endregion
		private Duzon.Common.BpControls.BpCodeTextBox tb_NM_PARTNER;
		private Duzon.Common.BpControls.BpCodeTextBox tb_NO_EMP;
		private Duzon.Common.BpControls.BpCodeTextBox tb_NM_DEPT;
        private TableLayoutPanel tableLayoutPanel1;
        private PanelExt m_gridTmp;
        private SplitContainer splitContainer1;
		private Duzon.Common.Controls.RoundedButton btn_FI_CANCEL;
			
		#endregion

		#region ♣ 생성자/소멸자
	
		#region -> 생성자
		public P_PU_IV_MNG_BAK()
		{
			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			// TODO: InitForm을 호출한 다음 초기화 작업을 추가합니다.

			//		PupUpMenu = new System.Windows.Forms.ContextMenu();		
			//	PupUpMenu.Popup += new System.EventHandler(PopupEventHandler);

			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);

		}
	

		/// <summary>
		/// 매입등록에서 직접 호출 
		/// </summary>
		/// <param name="ps_cdbizarea"></param>
		/// <param name="ps_no_emp"></param>
		/// <param name="ps_nm_emp"></param>
		/// <param name="ps_fgtrans"></param>
		/// <param name="ps_dtpo"></param>
		public P_PU_IV_MNG_BAK(string ps_cdbizarea, string ps_no_emp, string ps_nm_emp,string ps_fgtrans,string ps_dtpo)
		{
			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			// TODO: InitForm을 호출한 다음 초기화 작업을 추가합니다.
		
			_cdbizarea = ps_cdbizarea;
			_noemp = ps_no_emp;
			_nmemp = ps_nm_emp;
			_fg_trans = ps_fgtrans;
			_dtpo = ps_dtpo;


			_isCallIV = true;
		
			PupUpMenu = new System.Windows.Forms.ContextMenu();
			PupUpMenu.Popup += new System.EventHandler(PopupEventHandler);

			//			cbo_CD_BIZAREA.SelectedValue = ps_cdbizarea;
			//			tb_NO_EMP.Tag = 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_IV_MNG));
            this.m_pnlGrid2 = new Duzon.Common.Controls.PanelExt();
            this.m_pnlGrid1 = new Duzon.Common.Controls.PanelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.tb_NM_PARTNER = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.lb_ALO_DEPT = new Duzon.Common.Controls.LabelExt();
            this.lb_NO_EMP = new Duzon.Common.Controls.LabelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lb_FG_PO_TR = new Duzon.Common.Controls.LabelExt();
            this.lb_DT_IV = new Duzon.Common.Controls.LabelExt();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.lb_NM_PARTNER = new Duzon.Common.Controls.LabelExt();
            this.lb_NM_BIZAREA = new Duzon.Common.Controls.LabelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.lb_PT_PURCHASE = new Duzon.Common.Controls.LabelExt();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.cbo_CD_BIZAREA = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_TRANS = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_PT_PURCHASE = new Duzon.Common.Controls.DropDownComboBox();
            this.mtb_DT_PO1 = new Duzon.Common.Controls.DatePicker();
            this.mtb_DT_PO2 = new Duzon.Common.Controls.DatePicker();
            this.tb_NO_EMP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_NM_DEPT = new Duzon.Common.BpControls.BpCodeTextBox();
            this.btn_IVPROCESS = new Duzon.Common.Controls.RoundedButton(this.components);
            this.ds_Ty1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.btn_FI_CANCEL = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_gridTmp = new Duzon.Common.Controls.PanelExt();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtb_DT_PO1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtb_DT_PO2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_gridTmp.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlGrid2
            // 
            this.m_pnlGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid2.Location = new System.Drawing.Point(0, 0);
            this.m_pnlGrid2.Name = "m_pnlGrid2";
            this.m_pnlGrid2.Size = new System.Drawing.Size(787, 274);
            this.m_pnlGrid2.TabIndex = 127;
            // 
            // m_pnlGrid1
            // 
            this.m_pnlGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid1.Location = new System.Drawing.Point(0, 0);
            this.m_pnlGrid1.Name = "m_pnlGrid1";
            this.m_pnlGrid1.Size = new System.Drawing.Size(787, 183);
            this.m_pnlGrid1.TabIndex = 126;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tb_NM_PARTNER);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbo_CD_BIZAREA);
            this.panel1.Controls.Add(this.cbo_TRANS);
            this.panel1.Controls.Add(this.cbo_PT_PURCHASE);
            this.panel1.Controls.Add(this.mtb_DT_PO1);
            this.panel1.Controls.Add(this.mtb_DT_PO2);
            this.panel1.Controls.Add(this.tb_NO_EMP);
            this.panel1.Controls.Add(this.tb_NM_DEPT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 55);
            this.panel1.TabIndex = 0;
            // 
            // tb_NM_PARTNER
            // 
            this.tb_NM_PARTNER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NM_PARTNER.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_NM_PARTNER.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_NM_PARTNER.ButtonImage")));
            this.tb_NM_PARTNER.ChildMode = "";
            this.tb_NM_PARTNER.CodeName = "";
            this.tb_NM_PARTNER.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_NM_PARTNER.CodeValue = "";
            this.tb_NM_PARTNER.ComboCheck = true;
            this.tb_NM_PARTNER.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.tb_NM_PARTNER.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_NM_PARTNER.Location = new System.Drawing.Point(83, 30);
            this.tb_NM_PARTNER.Name = "tb_NM_PARTNER";
            this.tb_NM_PARTNER.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NM_PARTNER.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NM_PARTNER.SearchCode = true;
            this.tb_NM_PARTNER.SelectCount = 0;
            this.tb_NM_PARTNER.SetDefaultValue = false;
            this.tb_NM_PARTNER.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NM_PARTNER.Size = new System.Drawing.Size(149, 21);
            this.tb_NM_PARTNER.TabIndex = 4;
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
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.lb_ALO_DEPT);
            this.panel7.Controls.Add(this.lb_NO_EMP);
            this.panel7.Location = new System.Drawing.Point(547, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(80, 51);
            this.panel7.TabIndex = 2;
            // 
            // lb_ALO_DEPT
            // 
            this.lb_ALO_DEPT.Location = new System.Drawing.Point(3, 6);
            this.lb_ALO_DEPT.Name = "lb_ALO_DEPT";
            this.lb_ALO_DEPT.Resizeble = true;
            this.lb_ALO_DEPT.Size = new System.Drawing.Size(75, 17);
            this.lb_ALO_DEPT.TabIndex = 136;
            this.lb_ALO_DEPT.Tag = "GROUP_RCV";
            this.lb_ALO_DEPT.Text = "담당부서";
            this.lb_ALO_DEPT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NO_EMP
            // 
            this.lb_NO_EMP.Location = new System.Drawing.Point(3, 32);
            this.lb_NO_EMP.Name = "lb_NO_EMP";
            this.lb_NO_EMP.Resizeble = true;
            this.lb_NO_EMP.Size = new System.Drawing.Size(75, 17);
            this.lb_NO_EMP.TabIndex = 136;
            this.lb_NO_EMP.Tag = "GROUP_RCV";
            this.lb_NO_EMP.Text = "담당자";
            this.lb_NO_EMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lb_FG_PO_TR);
            this.panel6.Controls.Add(this.lb_DT_IV);
            this.panel6.Location = new System.Drawing.Point(239, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(80, 51);
            this.panel6.TabIndex = 1;
            // 
            // lb_FG_PO_TR
            // 
            this.lb_FG_PO_TR.Location = new System.Drawing.Point(3, 32);
            this.lb_FG_PO_TR.Name = "lb_FG_PO_TR";
            this.lb_FG_PO_TR.Resizeble = true;
            this.lb_FG_PO_TR.Size = new System.Drawing.Size(75, 17);
            this.lb_FG_PO_TR.TabIndex = 135;
            this.lb_FG_PO_TR.Tag = "CD_CURRENCY";
            this.lb_FG_PO_TR.Text = "거래구분";
            this.lb_FG_PO_TR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_DT_IV
            // 
            this.lb_DT_IV.Location = new System.Drawing.Point(3, 6);
            this.lb_DT_IV.Name = "lb_DT_IV";
            this.lb_DT_IV.Resizeble = true;
            this.lb_DT_IV.Size = new System.Drawing.Size(75, 17);
            this.lb_DT_IV.TabIndex = 134;
            this.lb_DT_IV.Tag = "TERM_DT";
            this.lb_DT_IV.Text = "처리일자";
            this.lb_DT_IV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.lb_NM_PARTNER);
            this.panel3.Controls.Add(this.lb_NM_BIZAREA);
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 51);
            this.panel3.TabIndex = 0;
            // 
            // lb_NM_PARTNER
            // 
            this.lb_NM_PARTNER.Location = new System.Drawing.Point(3, 32);
            this.lb_NM_PARTNER.Name = "lb_NM_PARTNER";
            this.lb_NM_PARTNER.Resizeble = true;
            this.lb_NM_PARTNER.Size = new System.Drawing.Size(75, 17);
            this.lb_NM_PARTNER.TabIndex = 133;
            this.lb_NM_PARTNER.Tag = "FG_OFFER";
            this.lb_NM_PARTNER.Text = "거래처";
            this.lb_NM_PARTNER.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NM_BIZAREA
            // 
            this.lb_NM_BIZAREA.Location = new System.Drawing.Point(3, 6);
            this.lb_NM_BIZAREA.Name = "lb_NM_BIZAREA";
            this.lb_NM_BIZAREA.Resizeble = true;
            this.lb_NM_BIZAREA.Size = new System.Drawing.Size(75, 17);
            this.lb_NM_BIZAREA.TabIndex = 132;
            this.lb_NM_BIZAREA.Tag = "CD_TRANS";
            this.lb_NM_BIZAREA.Text = "사업장";
            this.lb_NM_BIZAREA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel9.Controls.Add(this.lb_PT_PURCHASE);
            this.panel9.Location = new System.Drawing.Point(395, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(75, 26);
            this.panel9.TabIndex = 136;
            // 
            // lb_PT_PURCHASE
            // 
            this.lb_PT_PURCHASE.Location = new System.Drawing.Point(3, 6);
            this.lb_PT_PURCHASE.Name = "lb_PT_PURCHASE";
            this.lb_PT_PURCHASE.Resizeble = true;
            this.lb_PT_PURCHASE.Size = new System.Drawing.Size(70, 17);
            this.lb_PT_PURCHASE.TabIndex = 136;
            this.lb_PT_PURCHASE.Tag = "GROUP_RCV";
            this.lb_PT_PURCHASE.Text = "전표처리";
            this.lb_PT_PURCHASE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(424, 4);
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size(18, 18);
            this.label2.TabIndex = 147;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbo_CD_BIZAREA
            // 
            this.cbo_CD_BIZAREA.AutoDropDown = true;
            this.cbo_CD_BIZAREA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_CD_BIZAREA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_BIZAREA.Location = new System.Drawing.Point(85, 4);
            this.cbo_CD_BIZAREA.Name = "cbo_CD_BIZAREA";
            this.cbo_CD_BIZAREA.ShowCheckBox = false;
            this.cbo_CD_BIZAREA.Size = new System.Drawing.Size(150, 20);
            this.cbo_CD_BIZAREA.TabIndex = 0;
            this.cbo_CD_BIZAREA.UseKeyEnter = false;
            this.cbo_CD_BIZAREA.UseKeyF3 = false;
            this.cbo_CD_BIZAREA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_COMBO_KeyDown);
            // 
            // cbo_TRANS
            // 
            this.cbo_TRANS.AutoDropDown = true;
            this.cbo_TRANS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_TRANS.Location = new System.Drawing.Point(321, 30);
            this.cbo_TRANS.Name = "cbo_TRANS";
            this.cbo_TRANS.ShowCheckBox = false;
            this.cbo_TRANS.Size = new System.Drawing.Size(72, 20);
            this.cbo_TRANS.TabIndex = 5;
            this.cbo_TRANS.UseKeyEnter = false;
            this.cbo_TRANS.UseKeyF3 = false;
            this.cbo_TRANS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_COMBO_KeyDown);
            // 
            // cbo_PT_PURCHASE
            // 
            this.cbo_PT_PURCHASE.AutoDropDown = true;
            this.cbo_PT_PURCHASE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_PT_PURCHASE.Location = new System.Drawing.Point(473, 30);
            this.cbo_PT_PURCHASE.Name = "cbo_PT_PURCHASE";
            this.cbo_PT_PURCHASE.ShowCheckBox = false;
            this.cbo_PT_PURCHASE.Size = new System.Drawing.Size(72, 20);
            this.cbo_PT_PURCHASE.TabIndex = 6;
            this.cbo_PT_PURCHASE.UseKeyEnter = false;
            this.cbo_PT_PURCHASE.UseKeyF3 = false;
            this.cbo_PT_PURCHASE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_COMBO_KeyDown);
            // 
            // mtb_DT_PO1
            // 
            this.mtb_DT_PO1.CalendarBackColor = System.Drawing.Color.White;
            this.mtb_DT_PO1.DayColor = System.Drawing.Color.Black;
            this.mtb_DT_PO1.FriDayColor = System.Drawing.Color.Blue;
            this.mtb_DT_PO1.Location = new System.Drawing.Point(322, 3);
            this.mtb_DT_PO1.Mask = "####/##/##";
            this.mtb_DT_PO1.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.mtb_DT_PO1.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.mtb_DT_PO1.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.mtb_DT_PO1.Modified = false;
            this.mtb_DT_PO1.Name = "mtb_DT_PO1";
            this.mtb_DT_PO1.PaddingCharacter = '_';
            this.mtb_DT_PO1.PassivePromptCharacter = '_';
            this.mtb_DT_PO1.PromptCharacter = '_';
            this.mtb_DT_PO1.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.mtb_DT_PO1.ShowToDay = true;
            this.mtb_DT_PO1.ShowTodayCircle = true;
            this.mtb_DT_PO1.ShowUpDown = false;
            this.mtb_DT_PO1.Size = new System.Drawing.Size(92, 21);
            this.mtb_DT_PO1.SunDayColor = System.Drawing.Color.Red;
            this.mtb_DT_PO1.TabIndex = 1;
            this.mtb_DT_PO1.TitleBackColor = System.Drawing.SystemColors.Control;
            this.mtb_DT_PO1.TitleForeColor = System.Drawing.Color.Black;
            this.mtb_DT_PO1.ToDayColor = System.Drawing.Color.Red;
            this.mtb_DT_PO1.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.mtb_DT_PO1.UseKeyF3 = false;
            this.mtb_DT_PO1.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.mtb_DT_PO1.Validated += new System.EventHandler(this.DataPickerValidated);
            // 
            // mtb_DT_PO2
            // 
            this.mtb_DT_PO2.CalendarBackColor = System.Drawing.Color.White;
            this.mtb_DT_PO2.DayColor = System.Drawing.Color.Black;
            this.mtb_DT_PO2.FriDayColor = System.Drawing.Color.Blue;
            this.mtb_DT_PO2.Location = new System.Drawing.Point(447, 3);
            this.mtb_DT_PO2.Mask = "####/##/##";
            this.mtb_DT_PO2.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.mtb_DT_PO2.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.mtb_DT_PO2.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.mtb_DT_PO2.Modified = false;
            this.mtb_DT_PO2.Name = "mtb_DT_PO2";
            this.mtb_DT_PO2.PaddingCharacter = '_';
            this.mtb_DT_PO2.PassivePromptCharacter = '_';
            this.mtb_DT_PO2.PromptCharacter = '_';
            this.mtb_DT_PO2.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.mtb_DT_PO2.ShowToDay = true;
            this.mtb_DT_PO2.ShowTodayCircle = true;
            this.mtb_DT_PO2.ShowUpDown = false;
            this.mtb_DT_PO2.Size = new System.Drawing.Size(92, 21);
            this.mtb_DT_PO2.SunDayColor = System.Drawing.Color.Red;
            this.mtb_DT_PO2.TabIndex = 2;
            this.mtb_DT_PO2.TitleBackColor = System.Drawing.SystemColors.Control;
            this.mtb_DT_PO2.TitleForeColor = System.Drawing.Color.Black;
            this.mtb_DT_PO2.ToDayColor = System.Drawing.Color.Red;
            this.mtb_DT_PO2.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.mtb_DT_PO2.UseKeyF3 = false;
            this.mtb_DT_PO2.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.mtb_DT_PO2.Validated += new System.EventHandler(this.DataPickerValidated);
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
            this.tb_NO_EMP.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_NO_EMP.Location = new System.Drawing.Point(630, 30);
            this.tb_NO_EMP.Name = "tb_NO_EMP";
            this.tb_NO_EMP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NO_EMP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NO_EMP.SearchCode = true;
            this.tb_NO_EMP.SelectCount = 0;
            this.tb_NO_EMP.SetDefaultValue = false;
            this.tb_NO_EMP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NO_EMP.Size = new System.Drawing.Size(146, 21);
            this.tb_NO_EMP.TabIndex = 7;
            // 
            // tb_NM_DEPT
            // 
            this.tb_NM_DEPT.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_NM_DEPT.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_NM_DEPT.ButtonImage")));
            this.tb_NM_DEPT.ChildMode = "";
            this.tb_NM_DEPT.CodeName = "";
            this.tb_NM_DEPT.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_NM_DEPT.CodeValue = "";
            this.tb_NM_DEPT.ComboCheck = true;
            this.tb_NM_DEPT.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB;
            this.tb_NM_DEPT.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_NM_DEPT.Location = new System.Drawing.Point(630, 3);
            this.tb_NM_DEPT.Name = "tb_NM_DEPT";
            this.tb_NM_DEPT.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NM_DEPT.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NM_DEPT.SearchCode = true;
            this.tb_NM_DEPT.SelectCount = 0;
            this.tb_NM_DEPT.SetDefaultValue = false;
            this.tb_NM_DEPT.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NM_DEPT.Size = new System.Drawing.Size(146, 21);
            this.tb_NM_DEPT.TabIndex = 3;
            // 
            // btn_IVPROCESS
            // 
            this.btn_IVPROCESS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_IVPROCESS.BackColor = System.Drawing.Color.White;
            this.btn_IVPROCESS.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_IVPROCESS.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_IVPROCESS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IVPROCESS.Location = new System.Drawing.Point(18, 0);
            this.btn_IVPROCESS.Name = "btn_IVPROCESS";
            this.btn_IVPROCESS.Size = new System.Drawing.Size(110, 24);
            this.btn_IVPROCESS.TabIndex = 132;
            this.btn_IVPROCESS.TabStop = false;
            this.btn_IVPROCESS.Text = "미결전표처리";
            this.btn_IVPROCESS.UseVisualStyleBackColor = false;
            this.btn_IVPROCESS.Click += new System.EventHandler(this.btn_IVPROCESS_Click);
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
            this.dataColumn3});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "CD_COMPANY";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "ID_INSERT";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "MODULE";
            // 
            // btn_FI_CANCEL
            // 
            this.btn_FI_CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_FI_CANCEL.BackColor = System.Drawing.Color.White;
            this.btn_FI_CANCEL.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_FI_CANCEL.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_FI_CANCEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FI_CANCEL.Location = new System.Drawing.Point(130, 0);
            this.btn_FI_CANCEL.Name = "btn_FI_CANCEL";
            this.btn_FI_CANCEL.Size = new System.Drawing.Size(110, 24);
            this.btn_FI_CANCEL.TabIndex = 132;
            this.btn_FI_CANCEL.TabStop = false;
            this.btn_FI_CANCEL.Text = "전표처리취소";
            this.btn_FI_CANCEL.UseVisualStyleBackColor = false;
            this.btn_FI_CANCEL.Click += new System.EventHandler(this.btn_FI_CANCEL_Click);
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
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 133;
            // 
            // m_gridTmp
            // 
            this.m_gridTmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridTmp.Controls.Add(this.btn_IVPROCESS);
            this.m_gridTmp.Controls.Add(this.btn_FI_CANCEL);
            this.m_gridTmp.Location = new System.Drawing.Point(546, 3);
            this.m_gridTmp.Name = "m_gridTmp";
            this.m_gridTmp.Size = new System.Drawing.Size(244, 27);
            this.m_gridTmp.TabIndex = 134;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 97);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_pnlGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_pnlGrid2);
            this.splitContainer1.Size = new System.Drawing.Size(787, 461);
            this.splitContainer1.SplitterDistance = 183;
            this.splitContainer1.TabIndex = 135;
            // 
            // P_PU_IV_MNG
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_PU_IV_MNG";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtb_DT_PO1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtb_DT_PO2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_gridTmp.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
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
				this.Enabled   = false;

				//페이지를 로드 하는중입니다.
				this.ShowStatusBarMessage(1);
				this.SetProgressBarValue(100, 10);
				
				InitControl();
				this.SetProgressBarValue(100, 40);
				InitGridM();
				this.SetProgressBarValue(100, 70);
				InitGridD();
                
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
				lb_ALO_DEPT.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","ALO_DEPT");
				lb_DT_IV.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","DT_IV");		
				lb_FG_PO_TR.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","FG_PO_TR");
				lb_NM_BIZAREA.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_BIZAREA");
				lb_NO_EMP.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NO_EMP");
				lb_NM_PARTNER.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PARTNER");
				lb_PT_PURCHASE.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","ST_SLIP");		
//				btn_SELECT_ALL.Text = this.MainFrameInterface.GetDataDictionaryItem("CM","SELECT_ALL");
//				btn_SELECT_CANCEL.Text = this.MainFrameInterface.GetDataDictionaryItem("CM","SELECT_CANCEL");	

			
				mtb_DT_PO2.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				mtb_DT_PO2.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

				mtb_DT_PO1.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				mtb_DT_PO1.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
				
				mtb_DT_PO2.Text = this.MainFrameInterface.GetStringToday;
				mtb_DT_PO1.Text = this.MainFrameInterface.GetStringToday.Substring(0,6)+"01";

				btn_FI_CANCEL.Enabled = false;
			//	btn_IVPROCESS.Enabled = false;

			}
			catch(Exception ex)
			{
				throw ex;
			}

		}
	
		#endregion

		#region -> InitGridM
		/// <summary>
		/// 그리드 형태 정의 함수
		/// </summary>
		private void InitGridM()
		{	
			Application.DoEvents();
			
			_flexM = new Dass.FlexGrid.FlexGrid();

			((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
			
			this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexM.AutoResize = false;
			this._flexM.BackColor = System.Drawing.SystemColors.Window;
			this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexM.Name = "_flexM";
			this._flexM.Rows.Count = 1;
			this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexM.ShowSort = false;
			this._flexM.Size = new System.Drawing.Size(785, 553);
			this._flexM.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{Font:굴림체, 9pt;Trimming:EllipsisCharacter;}	Fixed{BackColor:Control;ForeColor:ControlText;TextAlign:CenterCenter;Trimming:EllipsisCharacter;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
			this._flexM.TabIndex = 0;
			m_pnlGrid1.Controls.Add(this._flexM);
			((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();


			_flexM.Redraw = false;

			_flexM.Rows.Count = 1;			// 총 Row 수
			_flexM.Rows.Fixed = 1;			// FixedRow 수
			_flexM.Cols.Count = 13;			// 총 Col 수
			_flexM.Cols.Fixed = 1;			// FixedCol 수			
			_flexM.Rows.DefaultSize = 20;	

			_flexM[0,1] = "S";
			_flexM[0,2] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NO_IV");
			_flexM[0,3] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "DT_IV");
			_flexM[0,4] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "CD_PARTNER");
			_flexM[0,5] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NM_PARTNER");
			_flexM[0,6] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NO_COMPANY");
			_flexM[0,7] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "FG_PO_TR");
			_flexM[0,8] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "FG_TAX");
			_flexM[0,9] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "AM_IV");
			_flexM[0,10] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "VAT");
			_flexM[0,11] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "STA_IV");
			_flexM[0,12] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NO_LC");
			
			_flexM.Cols[0].Width = 50;
		
			_flexM.Cols[1].Name = "CHK";
			_flexM.Cols[1].DataType = typeof(string);
			_flexM.Cols[1].Format = "T;F";
			_flexM.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM.Cols[1].Width = 30;
			
			_flexM.Cols[2].Name = "NO_IV";
			_flexM.Cols[2].DataType = typeof(string);
			_flexM.Cols[2].Width = 120;

			_flexM.Cols[3].Name = "DT_PROCESS";
			_flexM.Cols[3].DataType = typeof(string);
			_flexM.Cols[3].Width = 100;
			_flexM.Cols[3].EditMask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT).Replace("#","9");
			_flexM.Cols[3].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
			_flexM.Cols[3].TextAlign = TextAlignEnum.CenterCenter;
			_flexM.SetStringFormatCol("DT_PROCESS");
			
			_flexM.Cols[4].Name = "CD_PARTNER";
			_flexM.Cols[4].DataType = typeof(string);
			_flexM.Cols[4].Width = 80;

			_flexM.Cols[5].Name = "LN_PARTNER";
			_flexM.Cols[5].DataType = typeof(string);			
			_flexM.Cols[5].Width = 120;

			_flexM.Cols[6].Name = "NO_BIZAREA";
			_flexM.Cols[6].DataType = typeof(string);
			_flexM.Cols[6].EditMask = "999-99-99999";	
			_flexM.Cols[6].Format = "###-##-#####";
			_flexM.Cols[6].Width = 120;
			_flexM.SetStringFormatCol("NO_BIZAREA");

			_flexM.Cols[7].Name = "NM_TRANS";
			_flexM.Cols[7].DataType = typeof(string);			
			_flexM.Cols[7].Width = 80;
		
			_flexM.Cols[8].Name = "NM_TAX";
			_flexM.Cols[8].DataType = typeof(string);			
			_flexM.Cols[8].Width = 80;

			_flexM.Cols[9].Name = "AM_K";
			_flexM.Cols[9].DataType = typeof(decimal);
			_flexM.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexM.Cols[9].Format = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);			
			_flexM.Cols[9].Width = 120;

			_flexM.Cols[10].Name = "VAT_TAX";
			_flexM.Cols[10].DataType = typeof(decimal);
			_flexM.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexM.Cols[10].Format = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);			
			_flexM.Cols[10].Width = 100;


			_flexM.Cols[11].Name = "TP_AIS";
			_flexM.Cols[11].DataType = typeof(string);			
			_flexM.Cols[11].Width = 80;

			_flexM.Cols[12].Name = "NO_LC";
			_flexM.Cols[12].DataType = typeof(string);			
			_flexM.Cols[12].Width = 80;

		
			_flexM.AllowSorting = AllowSortingEnum.MultiColumn;
			_flexM.AllowEditing = false;
			_flexM.NewRowEditable = false;
			_flexM.EnterKeyAddRow = false;
		
			_flexM.GridStyle = GridStyleEnum.Green;
			
			this.SetUserGrid(this._flexM);			
			_flexM.Redraw = true;

			//			_flexM.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flexM_ValidateEdit);

			_flexM.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler( _flexM_AfterRowColChange);

			//			_flexM.DoubleClick += new System.EventHandler(_flexM_DoubleClick);
			//			_flexM.KeyDown += new System.Windows.Forms.KeyEventHandler(_flexM_KeyDown);
			//
		}

		
		#endregion

		#region -> InitGridD
		/// <summary>
		/// 그리드 형태 정의 함수
		/// </summary>
		private void InitGridD()
		{	         
			
			Application.DoEvents();
			
			_flexD = new Dass.FlexGrid.FlexGrid();

			((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
			this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexD.AutoResize = false;
			this._flexD.BackColor = System.Drawing.SystemColors.Window;
			this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexD.Name = "_flexD";
			this._flexD.Rows.Count = 1;
			this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexD.ShowSort = false;
			this._flexD.Size = new System.Drawing.Size(434, 490);
			this._flexD.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{Font:굴림체, 9pt;Trimming:EllipsisCharacter;}	Fixed{BackColor:Control;ForeColor:ControlText;TextAlign:CenterCenter;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
			this._flexD.TabIndex = 0;
			m_pnlGrid2.Controls.Add(this._flexD);
			((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();


			_flexD.Redraw = false;

			_flexD.Rows.Count = 1;			// 총 Row 수
			_flexD.Rows.Fixed = 1;			// FixedRow 수
			_flexD.Cols.Count = 15;			// 총 Col 수
			_flexD.Cols.Fixed = 1;			// FixedCol 수			
			_flexD.Rows.DefaultSize = 20;	

			_flexD[0,1] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NO_GR");
			_flexD[0,2] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "DT_GR");
			_flexD[0,3] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "CD_ITEM");
			_flexD[0,4] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NM_ITEM");
			_flexD[0,5] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "UNIT");
			_flexD[0,6] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "QT");
			_flexD[0,7] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "UM");
			_flexD[0,8] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "AM");
			_flexD[0,9] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "VAT");
			_flexD[0,10] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NO_PO");
			_flexD[0,11] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "PT_PURCHASE");
			_flexD[0,12] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NM_PURGRP");
			_flexD[0,13] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "NO_EMP");
			_flexD[0,14] = this.GetDataDictionaryItem(DataDictionaryTypes.PU, "PROJECT");
		
			_flexD.Cols[0].Width = 50;
		
			_flexD.Cols[1].Name = "NO_IO";
			_flexD.Cols[1].DataType = typeof(string);
			_flexD.Cols[1].Width = 120;

			_flexD.Cols[2].Name = "DT_IO";
			_flexD.Cols[2].DataType = typeof(string);
			_flexD.Cols[2].Width = 100;
			_flexD.Cols[2].EditMask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT).Replace("#","9");
			_flexD.Cols[2].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
			_flexD.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexD.SetStringFormatCol("DT_IO");

	
			_flexD.Cols[3].Name = "CD_ITEM";
			_flexD.Cols[3].DataType = typeof(string);
			_flexD.Cols[3].Width = 120;
			
			_flexD.Cols[4].Name = "NM_ITEM";
			_flexD.Cols[4].DataType = typeof(string);
			_flexD.Cols[4].Width = 150;
	
			_flexD.Cols[5].Name = "CD_UNIT_MM";
			_flexD.Cols[5].DataType = typeof(string);
			_flexD.Cols[5].Width = 80;
			
			_flexD.Cols[6].Name = "QT_RCV_CLS";
			_flexD.Cols[6].DataType = typeof(decimal);
			_flexD.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[6].Format = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);			
			_flexD.Cols[6].Width = 100;

			_flexD.Cols[7].Name = "UM_ITEM_CLS";
			_flexD.Cols[7].DataType = typeof(decimal);
			_flexD.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[7].Format = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.FOREIGN_UNIT_COST,FormatFgType.INSERT);			
			_flexD.Cols[7].Width = 100;

			_flexD.Cols[8].Name = "AM_CLS";
			_flexD.Cols[8].DataType = typeof(decimal);
			_flexD.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[8].Format = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);			
			_flexD.Cols[8].Width = 120;

			_flexD.Cols[9].Name = "VAT";
			_flexD.Cols[9].DataType = typeof(decimal);
			_flexD.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[9].Format = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);			
			_flexD.Cols[9].Width = 100;


			_flexD.Cols[10].Name = "NO_PO";
			_flexD.Cols[10].DataType = typeof(string);
			_flexD.Cols[10].Width = 120;

			_flexD.Cols[11].Name = "NM_TPPURCHASE";
			_flexD.Cols[11].DataType = typeof(string);
			_flexD.Cols[11].Width = 80;

		
			_flexD.Cols[12].Name = "NM_PURGRP";
			_flexD.Cols[12].DataType = typeof(string);
			_flexD.Cols[12].Width = 120;

			_flexD.Cols[13].Name = "NM_KOR";
			_flexD.Cols[13].DataType = typeof(string);
			_flexD.Cols[13].Width = 120;

		
			_flexD.Cols[14].Name = "NM_PROJECT";
			_flexD.Cols[14].DataType = typeof(string);
			_flexD.Cols[14].Width = 120;


			_flexD.AllowSorting = AllowSortingEnum.MultiColumn;
			_flexD.NewRowEditable = false;
			_flexD.EnterKeyAddRow = false;
			_flexD.AllowEditing = false;
		
			_flexD.GridStyle = GridStyleEnum.Green;
					
			this.SetUserGrid(this._flexD);			
			_flexD.Redraw = true;			
			
					
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
				
					Application.DoEvents(); //<-- 반드시 처리할것
				
					
					// 콤보박스 초기화
					InitCombo();
				
				
					this.Enabled = true; //페이지 전체 활성
					cbo_CD_BIZAREA.Focus();

					if( _isCallIV )
					{
						cbo_CD_BIZAREA.SelectedValue = _cdbizarea;
						tb_NO_EMP.CodeValue = _noemp;
						tb_NO_EMP.CodeName = _nmemp;

						cbo_TRANS.SelectedValue = _fg_trans;
						mtb_DT_PO1.Text = _dtpo;
						mtb_DT_PO2.Text = _dtpo;

						OnToolBarSearchButtonClicked(null, null);
					}		
					
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
//				string[] lsa_args = {"S;PU_C000016", "C;CC_MA_COMMON008","C;CC_MA_COMMON014","B_N;"};
//				object[] args = { this.LoginInfo.CompanyCode, lsa_args};
//				DataSet g_dsCombo = (DataSet)MainFrameInterface.InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);
//					
				DataSet g_dsCombo  = this.GetComboData("S;PU_C000016", "S;YESNO","S;MA_CODEDTL_003","NC;MA_BIZAREA");
			

				// 거래구분		
				cbo_TRANS.DataSource = g_dsCombo.Tables[0];
				cbo_TRANS.DisplayMember = "NAME";
				cbo_TRANS.ValueMember = "CODE";

				// 전표처리	
				cbo_PT_PURCHASE.DataSource = g_dsCombo.Tables[1];
				cbo_PT_PURCHASE.DisplayMember = "NAME";
				cbo_PT_PURCHASE.ValueMember = "CODE";	
				
				//사업장처리	
				cbo_CD_BIZAREA.DataSource = g_dsCombo.Tables[3];
				cbo_CD_BIZAREA.DisplayMember = "NAME";
				cbo_CD_BIZAREA.ValueMember = "CODE";	

				this.ToolBarSearchButtonEnabled = true;		
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
				this.ToolBarSearchButtonEnabled = true;		
				this.Enabled = true;
				
				this.Cursor = Cursors.Default;
			}
		}


		#endregion


		#endregion


		#region ♣ 저장관련
		

		#region	-> IsChanged
		
		private bool IsChanged(string gubun)
		{	
			try
			{
				DataTable ldt_HeadDel = _flexM.DataTable.GetChanges(DataRowState.Deleted);
				if( ldt_HeadDel != null && ldt_HeadDel.Rows.Count >0)
				{
					return true;
				}		
	
				if(gubun == null)
					return _flexD.IsDataChanged;
			}
			catch
			{
			}
			return false;
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
				result = this.ShowMessageBox(1001, this.PageName);	
				if(result == DialogResult.No)
					return true;
				if(result == DialogResult.Cancel)
					return false;
			}
			else
			{
				result = this.ShowMessageBox(1001, this.PageName);	
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
				// 깜한밤에.
				//m_lblTitle.Focus();				
		

				DataTable ldt_HeadDel = _flexM.DataTable.GetChanges(DataRowState.Deleted);

				Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
				si.DataValue = ldt_HeadDel; 					//저장할 데이터 테이블				
				si.SpNameDelete = "UP_PU_IVH_DELETE";			//Delete 프로시저명

		
				/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
				si.SpParamsDelete = new string[] { "NO_IV", "CD_COMPANY" };
				
				/*브라우저의 저장메소드(Save)호출
					ResultData 타입으로 리턴된다.	
				*/	
				Duzon.Common.Util.ResultData result = (Duzon.Common.Util.ResultData)this.Save(si);
				/*저장에 성공한경우*/
				if(result.Result)
				{						
					_flexM.DataTable.AcceptChanges();	
					_flexD.DataTable.AcceptChanges();
					return true;
				}
				return false;	



//				ldt_HeadDel.RejectChanges();
//				object[] m_obj = { this.MainFrameInterface.LoginInfo.CompanyCode,this.MainFrameInterface.LoginInfo.UserID,
//									 null,ldt_HeadDel,null};
//
//				int rtn = (int)(this.MainFrameInterface.InvokeRemoteMethod("PurPurchaseControl", "pur.CC_PU_IV","CC_PU_IV.rem", "SavePuivmng", m_obj));	
//		
//				if( rtn >= 0)
//				{
//					_flexM.DataTable.AcceptChanges();	
//					_flexD.DataTable.AcceptChanges();
//					return true;
//				}
//				
//				return false;			

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
			return false;
		}


		#endregion


		#endregion


		#region ♣ 메인버튼 이벤트


		#region -> DoContinue

		private bool DoContinue()
		{
			if(_flexM.Editor != null)
			{
				return _flexM.FinishEditing(false);
			}
			
			return true;
		}

		#endregion

		#region -> 조회버튼클릭
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{	
				if(!DoContinue())
					return;

				Cursor.Current = Cursors.WaitCursor;
				if(!FieldCheck())
				{
					return;
				}
			
				if(!MsgAndSave(true,false))
					return;

				this.ShowStatusBarMessage(2);
				this.SetProgressBarValue(100, 10);	
					
				if(_flexM.DataTable != null)
				{
					_flexM.DataTable.Rows.Clear();
					_flexD.DataTable.Rows.Clear();						
					Thread.Sleep(50);
				}

				object[] m_obj = new object[10];
				m_obj[0] = cbo_CD_BIZAREA.SelectedValue.ToString();	
				m_obj[1] = mtb_DT_PO1.MaskEditBox.ClipText;
				m_obj[2] = mtb_DT_PO2.MaskEditBox.ClipText;
				m_obj[3] = this.MainFrameInterface.LoginInfo.CompanyCode;			
				m_obj[4] = "N";			
				m_obj[5] = cbo_TRANS.SelectedValue.ToString();
				m_obj[6] = cbo_PT_PURCHASE.SelectedValue.ToString();
				m_obj[7] = tb_NM_PARTNER.CodeValue ;
				m_obj[8] = tb_NM_DEPT.CodeValue;
				m_obj[9] = tb_NO_EMP.CodeValue;


				
				SpInfo si = new SpInfo();
				si.SpNameSelect = "UP_PU_IV_MNG_SELECT_H";
				si.SpParamsSelect = m_obj;
				ResultData result = (ResultData)FillDataTable(si);
				DataTable dt = (DataTable)result.DataValue;

				dt.Columns.Add("AFTER_OK");

				
				SpInfo si2 = new SpInfo();
				si2.SpNameSelect = "UP_PU_IV_MNG_SELECT_L";
				si2.SpParamsSelect = new Object[] { "xxx","xxx" };
				ResultData result2 = (ResultData)this.FillDataTable(si2);
				DataTable dt2 = (DataTable)result2.DataValue;


//
//				string[] m_agrs  = new string[]{cbo_CD_BIZAREA.SelectedValue.ToString(),mtb_DT_PO1.MaskEditBox.ClipText,mtb_DT_PO2.MaskEditBox.ClipText,tb_NM_DEPT.Tag.ToString(),
//												   tb_NM_PARTNER.Tag.ToString(),cbo_TRANS.SelectedValue.ToString(),cbo_PT_PURCHASE.SelectedValue.ToString(),tb_NO_EMP.Tag.ToString(),
//												   this.MainFrameInterface.LoginInfo.CompanyCode, "N"};
//
//				object[] m_obj = new object[1];
//				m_obj[0] = m_agrs;
//				
//				DataSet lds_search = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurPurchaseControl_NTX", "pur.CC_PU_IV_NTX","CC_PU_IV_NTX.rem", "Selectiv", m_obj));	

				if( dt !=null && dt.Rows.Count >0 )
				{	
					// Detail 바인딩
					_flexD.Redraw=false;
					_flexD.BindingStart();
					_flexD.DataSource = new DataView(dt2); 
					_flexD.BindingEnd();
					_flexD.EmptyRowFilter();	// 처음에 아무것도 안 보이게
					_flexD.Redraw=true;

					// Master 바인딩
					_flexM.Redraw=false;
					_flexM.BindingStart();
					_flexM.DataSource = new DataView( dt); 	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
					_flexM.BindingEnd();
					_flexM.Redraw=true;
						
					if(_flexM.HasNormalRow)		// 처음 조회시 강제로 AfterRowColChange 메소드 호출
					{
					//	this.ToolBarSearchButtonEnabled = true;
						this.ToolBarDeleteButtonEnabled = true;
					}					
					else
					{
					//	this.ToolBarSearchButtonEnabled = false;
						this.ToolBarDeleteButtonEnabled = false;
					}
				}				
				else
				{
					this.ShowMessage("IK1_003");
					return ;
				}		

				this.SetProgressBarValue(100, 90);				
				
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
				_flexM.Redraw=true;

				this.SetProgressBarValue(100, 0);	
				this.ShowStatusBarMessage(0);	
				Cursor.Current = Cursors.Default;
			}		
		}		
	

		#endregion

		#region -> 삭제버튼클릭
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{		
			try
			{	
				// 하얗게 널 비쳐줄께
				//m_lblTitle.Focus();											
				DataRow[] row =  _flexM.DataTable.Select("CHK ='T'");					
				if( row ==null || row.Length <1 )
				{
					this.ShowMessage("IK1_007");
					return ;
				}

				// 전표처리가 완료된것이 선택되었는지 검사 
				DataRow[] ldr_Args1 =  _flexM.DataTable.Select("CHK ='T' AND TP_AIS ='Y'");
				if( ldr_Args1 != null && ldr_Args1.Length > 0 )
				{
					this.ShowMessage("PU_M000094");
					return ;
				}

				DialogResult result = this.ShowMessage("MA_M000016","QY3");
			
				if(result == DialogResult.Yes)
				{	
				
					_flexM.Redraw = false;
					for(int r = _flexM.Rows.Count-1;r >= _flexM.Rows.Fixed; r--)
					{
						if(_flexM[r,"CHK"].ToString() == "T")
						{							
							_flexM.Rows.Remove(r);
						}							
					}	
					
					if(_flexM.HasNormalRow)		// 처음 조회시 강제로 AfterRowColChange 메소드 호출
					{
						int rowi = _flexM.Row;
						_flexM.Row = -1;
						_flexM.Row = rowi;

					}
					else
					{
						_flexD.Redraw=false;
						_flexD.EmptyRowFilter();	// 처음에 아무것도 안 보이게
						_flexD.Redraw=true;
					}
                    
					if(MsgAndSave(false, false))
					{
						this.ShowMessage("IK1_001");
					}					
				}		
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{							
				_flexM.Redraw = true;	
				Cursor.Current = Cursors.Default;
			}	
		}

	
		#endregion
        
		#region -> 저장버튼클릭
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			//m_lblTitle.Focus();
			
			try
			{
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

		#region -> 종료버튼클릭

		/// <summary>
		/// 브라우저의 종료 버턴 클릭시 처리부
		/// </summary>
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			//m_lblTitle.Focus();
					
			try
			{
				if(!MsgAndSave(true,true))	// 저장이 실패하면
					return false;			
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
	

		#region ♣ 그리드 이벤트

		#region -> _flexM_AfterRowColChange

		private void _flexM_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{
				if(!_flexM.IsBindingEnd || !_flexM.HasNormalRow)
				{
					//btn_IVPROCESS.Enabled = false;
					btn_FI_CANCEL.Enabled = false;
					_flexD.EmptyRowFilter();
					return;
				}

				if(  _flexM.Cols[e.NewRange.c1].Name == "CHK")
				{
					_flexM.AllowEditing = true;
				}				
				else
				{
					_flexM.AllowEditing = false;
				}	

				if( _flexM[_flexM.Row,"TP_AIS"].ToString().Trim() == "Y")
				{
				//	btn_IVPROCESS.Enabled = false;
					btn_FI_CANCEL.Enabled = true;
				}
				else
				{
				//	btn_IVPROCESS.Enabled = true;
					btn_FI_CANCEL.Enabled = false;
				}

				if(e.OldRange.r1 != e.NewRange.r1)
				{
					_flexD.Redraw = false;
					ShowDetail(e.NewRange.r1);
					_flexD.Redraw = true;
				}

//				// 필터
//				if(_flexM.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
//				{
//					if(_flexD.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
//					{			
//						string filter = "NO_IV = '" + _flexM[_flexM.Row,"NO_IV"].ToString() + "'";
//
//						_flexD.DataView.RowFilter  = filter;									
//					}	
//				}						
			}
			finally
			{				
			}            
		}


		private void ShowDetail(int row)
		{
			string filter = "NO_IV = '" + _flexM[_flexM.Row,"NO_IV"].ToString() + "'";
			
			_flexD.RowFilter = filter;			// 이 문장이 반드시 처음으로 와야 한다.

			// 왜냐하면 디테일이 아무것도 없는경우 추가 버튼을 눌렀을 때 잘 되게 하기 위해
			if(!_flexM[row, "AFTER_OK"].Equals("Y"))
			{				
				SpInfo si2 = new SpInfo();
				si2.SpNameSelect = "UP_PU_IV_MNG_SELECT_L";
				si2.SpParamsSelect = new Object[] { this.LoginInfo.CompanyCode,_flexM[_flexM.Row,"NO_IV"].ToString() };
				ResultData result2 = (ResultData)this.FillDataTable(si2);
				DataTable dt = (DataTable)result2.DataValue;
				
				if(dt != null && dt.Rows.Count > 0)
				{
					_flexD.SetDummyColumnAll();	// 모든 컬럼을 더미컬럼으로 설정하고 -> 인서트시 빨리 처리되게 하기 위해
				
					DataTable srcTable = _flexD.DataTable;

					for(int r = 0; r < dt.Rows.Count; r++)
					{
						_flexD.DataTable.LoadDataRow(dt.Rows[r].ItemArray, true);						
					}

					_flexD.RemoveDummyColumnAll();	// 다시 원상태로 돌린다.
				}

				_flexM[row, "AFTER_OK"] = "Y";					
				
			}
			
		}

	


		#endregion
		
		#endregion


		#region ♣ 기타 이벤트

	
		#region -> 미결전표처리 버턴 처리부분
				
		/// <summary>
		/// 미결전표 버턴 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_IVPROCESS_Click(object sender, System.EventArgs e)
		{
			try
			{
				btn_IVPROCESS.Focus();
				Cursor.Current = Cursors.WaitCursor;
			
				// 선택된것 검사 , 선택이 안되면 전표처리가 불가 함으로 ㅋㅋㅋ 
				DataRow[] ldr_Args = _flexM.DataTable.Select("CHK ='T'");				
				if( ldr_Args == null || ldr_Args.Length <=0)
				{
					this.ShowMessage("IK1_007");
				//	Duzon.Common.Controls.MessageBoxEx.Show(MainFrameInterface.GetMessageDictionaryItem("MA_M000009"),this.PageName);	
					return ;					
				}                
			    
				// 이미 전표처리 된 것 검사
				DataRow[] ldr_Args1 = _flexM.DataTable.Select("CHK ='T' AND TP_AIS ='Y'");
				if( ldr_Args1  != null && ldr_Args1.Length > 0)
				{
					this.ShowMessage("PU_M000092");
					//Duzon.Common.Controls.MessageBoxEx.Show(MainFrameInterface.GetMessageDictionaryItem("PU_M000092"),this.PageName);	
					return ;					
				}

				// 선택된것 담기..
				DataTable ldt_Save = _flexM.DataTable.Clone();
				for( int i=0 ; i < ldr_Args.Length ; i++)
				{
					ldt_Save.ImportRow(ldr_Args[i]);
				}
                                
				// 전표 처리
				Cursor.Current = Cursors.WaitCursor;				
				
				// 전표처리를 위한 기타 정보담기 
				InDataInfoValue();

				object[] m_argobj = new object[2];
				m_argobj[0] = ldt_Save;
				m_argobj[1] = ds_Ty1.Tables[0];
                								
				DataTable ldt_result = (DataTable)(this.MainFrameInterface.InvokeRemoteMethod("PurPurchaseControl", "pur.CC_PU_IV", "CC_PU_IV.rem","SaveAisPostPU", m_argobj));				
				
				bool lbo_result = false;
				
				// 저장이 잘 되었으면 PU_IVH의 TP_AIS(전표처리여부)를 Y(처리)로 변경
				for( int i=0 ; i < ldr_Args.Length ; i++)
				{
					string ls_filter = "NO_IV = '"+  ldr_Args[i]["NO_IV"].ToString()+"'";
					DataRow[] ldr_temp = ldt_result.Select(ls_filter);						
					if( ldr_temp !=null && ldr_temp.Length >0)
					{		

						// 저장이 잘되었으면  전표발생여부에 "Y" 변경
						if( ldr_temp[0]["TP_AIS"].ToString().Trim() == "Y")
						{
							ldr_Args[i].BeginEdit();
							ldr_Args[i]["TP_AIS"] = "Y";
							ldr_Args[i].EndEdit();
							lbo_result = true;
						}
					}
				}
				if( lbo_result )
				{
					// 저장이 잘 되었습니다. ㅋㅋㅋ
					this.ShowMessageBox(1);	 
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
				_flexM.Redraw = true;
				Cursor.Current = Cursors.Default;
				// 과연 나는 누군가.. 우리를 두번 죽이는 것이라고 하셨습니다.
			}
		}
	
		// 전표 정보 담기
		private void InDataInfoValue()
		{
			DataRow newrow;
			ds_Ty1.Tables[0].Clear();
			newrow = ds_Ty1.Tables[0].NewRow();	
			newrow["ID_INSERT"] = this.LoginInfo.UserID;							// 사용자
			newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;	// 회사					
			newrow["MODULE"] = "PU";												// 모듈
			ds_Ty1.Tables[0].Rows.Add(newrow);			        
		}
				
			
		#endregion
	
		#region -> 팝업 바로가기 이벤트 처리 함수들
	
		/// <summary>
		///  팝업 이벤트 핸들러...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopupEventHandler(object sender, System.EventArgs e)
		{
			try
			{                
//				Duzon.Windows.Forms.Grid.GridDataBoundGrid Grdid = (GridDataBoundGrid)((System.Windows.Forms.ContextMenu)sender).SourceControl;
//				DataView ddv = (DataView)Grdid.DataSource;					
//				string ls_contract =  MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.FI, "CB_DOCURPT")+"(&S) ";		
//				MenuItem menuItem1 = new MenuItem(ls_contract,new EventHandler(PopupItemSelectdEventHandler));	
				
				// Clear all previously added MenuItems.
				//				PupUpMenu.MenuItems.Clear();
				//
				//				// 전표처리된 row만 팝업창 생성 
				//				if( ddv[Grdid.CurrentCell.RowIndex-1]["TP_AIS"].ToString().Trim() == "Y")
				//				{
				//					// 회계전표( 항목 추가 )
				//			//		PupUpMenu.MenuItems.Add(menuItem1);					
				//				}             
				//   Clear all previously added MenuItems.    Don't Worry . Be Happy! 
				//	Why be normal?.   Because , I am not confident of my future to this office.      
				//	
				// I don't know this issue !. 


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

					
		private void PopupItemSelectdEventHandler(object sender, EventArgs e)
		{
			try
			{					
				Cursor.Current = Cursors.WaitCursor;				
			
				string[] args = new string[11];
				args[0] = _flexM[_flexM.Row,"CD_PC"].ToString();		//회계단위
				args[1] = _flexM[_flexM.Row,"NM_PC"].ToString();		//회계단위명
				args[2] = _flexM[_flexM.Row,"CD_DEPT"].ToString();		//작성부서코드
				args[3] = _flexM[_flexM.Row,"NM_DEPT"].ToString();		//작성부서명
				args[4] = _flexM[_flexM.Row,"DT_PROCESS"].ToString();	//회계일자FROM
				args[5] = _flexM[_flexM.Row,"DT_PROCESS"].ToString();	//회계일자TO
				args[6] = "3";																//전표구분	
				args[7] = "11";																//전표유형	
				args[8] = _flexM[_flexM.Row,"NO_EMP"].ToString();		//작성사원명
				args[9] = _flexM[_flexM.Row,"NM_KOR"].ToString();		//작성자명
				args[10] = _flexM[_flexM.Row,"CD_COMPANY"].ToString();	//회사코드
			
				string ls_pagename = MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.FI, "P_FI_DOCU_RPT");
				this.CallOtherPageMethod("P_FI_DOCU_RPT",ls_pagename,this.Grant, args );   
			  			  
				//   System.Net.WebClient hWeb = new System.Net.WebClient();   
				//   System.Collections.Hashtable hdd = new System.Collections.Hashtable();
							
			}			
			finally
			{
				Cursor.Current = Cursors.Default;
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
			finally
			{
			}		
		}
		
		
		#endregion
		
		#endregion


		#region ♣ 기타 함수

		#region -> 체크 필드
		/// <summary>
		/// 필드 체크
		/// </summary>
		/// <returns></returns>
		private bool FieldCheck()
		{			
			if(mtb_DT_PO1.MaskEditBox.ClipText =="")
			{
				this.ShowMessage("WK1_004", lb_DT_IV.Text);
			//	Duzon.Common.Controls.MessageBoxEx.Show(lb_DT_IV.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
				mtb_DT_PO1.Focus();
				return false;
			}
			if(mtb_DT_PO2.MaskEditBox.ClipText =="")
			{
				this.ShowMessage("WK1_004", lb_DT_IV.Text);
				//Duzon.Common.Controls.MessageBoxEx.Show(lb_DT_IV.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
				mtb_DT_PO2.Focus();
				return false;
			}			
			return true;
		}

		#endregion

		#region -> 기타 키이벤트
	
		private void tb_COMBO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData.ToString() =="Enter")
			{	
				SendKeys.SendWait("{TAB}");	
			}		
		}
	
		#endregion

		private void btn_FI_CANCEL_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(_flexM.DataView == null || _flexM.DataView.Count == 0)
					return ;

				if( _flexM.Row <= 0)
					return;

				if( _flexM[_flexM.Row, "TP_AIS"].ToString() != "Y")
					return;

				object[] m_obj = new object[3];
				m_obj[0] = this.MainFrameInterface.LoginInfo.CompanyCode;
				m_obj[1] = "210";
				m_obj[2] = _flexM[_flexM.Row, "NO_IV"].ToString(); 
				
				//int li_result = (int)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl", "pur.CC_PU_APP","CC_PU_APP.rem", "DeleteApp", m_obj));
						
				ResultData ret = (ResultData)this.ExecSp("SP_FI_DOCU_AUTODEL", m_obj);
				if(ret.Result)
				{					
					_flexM[_flexM.Row, "TP_AIS"] = "N";		
					btn_FI_CANCEL.Enabled = false;
					this.ShowMessage("IK1_002");
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

				
	
	}
}
