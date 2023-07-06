using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

using System.Diagnostics;

namespace pur
{
	//********************************************************************
	// 작   성   자 : 이금주		--> 김대영
	// 작   성   일 : 2002-11-14	--> 2004-04-01	
	// 모   듈   명 : 구매/자재
	// 시 스  템 명 : 입고관리
	// 페 이 지  명 : 구매입고등록Page
	// 프로젝트  명 : P_PU_GR_REG
	//********************************************************************

	public class P_PU_GR_REG_OLD : Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

		private Duzon.Common.Controls.LabelExt label2;
        private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.DatePicker tb_DT_FROM;
		private Duzon.Common.Controls.DatePicker tb_DT_TO;
		private Duzon.Common.Controls.DatePicker tb_DT_IO;
		private Duzon.Common.Controls.LabelExt lb_nm_sl;
		private Duzon.Common.Controls.RadioButtonExt rb_do_pc;
		private Duzon.Common.Controls.RadioButtonExt rb_not_pc;
		private Duzon.Common.Controls.LabelExt lb_sta_iv;
		private Duzon.Common.Controls.LabelExt lb_dt_req;
		private Duzon.Common.Controls.LabelExt lb_nm_sl2;
		private Duzon.Common.Controls.LabelExt lb_dt_rcv;
		private Duzon.Common.Controls.TextBoxExt tb_no_gr;
		private Duzon.Common.Controls.LabelExt lb_no_gr;
		private System.ComponentModel.IContainer components;
		
		private Duzon.Common.Controls.LabelExt lb_dc_rmk;
		private Duzon.Common.Controls.TextBoxExt tb_dc_rmk;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.PanelExt panel10;
		private Duzon.Common.Controls.PanelExt panel11;
		private Duzon.Common.Controls.PanelExt panel12;
		private Duzon.Common.Controls.PanelExt panel13;
		private Duzon.Common.Controls.LabelExt lb_nm_partner;
		private Duzon.Common.Controls.RoundedButton btn_apply_good;
		private Duzon.Common.Controls.LabelExt lb_gl_plant2;
		private Duzon.Common.Controls.LabelExt lb_no_emp2;
		private Duzon.Common.Controls.LabelExt lb_no_emp;
		private System.Data.DataSet dataSet1;
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
		private System.Data.DataColumn dataColumn12;
		private System.Data.DataColumn dataColumn13;
		private System.Data.DataColumn dataColumn14;
		private System.Data.DataColumn dataColumn15;
		private System.Data.DataColumn dataColumn16;
		private System.Data.DataTable dataTable2;
		private System.Data.DataColumn dataColumn17;
		private System.Data.DataColumn dataColumn18;
		private System.Data.DataColumn dataColumn19;
		private System.Data.DataColumn dataColumn20;
		private System.Data.DataColumn dataColumn21;
		private System.Data.DataColumn dataColumn22;
		private System.Data.DataColumn dataColumn23;
		private System.Data.DataColumn dataColumn24;
		private System.Data.DataColumn dataColumn25;
		private System.Data.DataColumn dataColumn26;
		private System.Data.DataColumn dataColumn27;
		private System.Data.DataColumn dataColumn28;
		private System.Data.DataColumn dataColumn29;
		private System.Data.DataColumn dataColumn30;
		private System.Data.DataColumn dataColumn31;
		private System.Data.DataColumn dataColumn32;
		private System.Data.DataColumn dataColumn33;
		private System.Data.DataColumn dataColumn34;
		private System.Data.DataColumn dataColumn35;
		private System.Data.DataColumn dataColumn36;
		private System.Data.DataColumn dataColumn37;
		private System.Data.DataColumn dataColumn38;
		private System.Data.DataColumn dataColumn39;
		private System.Data.DataColumn dataColumn40;
		private System.Data.DataColumn dataColumn41;
		private System.Data.DataColumn dataColumn42;
		private System.Data.DataColumn dataColumn43;
		private System.Data.DataColumn dataColumn44;
		private System.Data.DataColumn dataColumn45;
		private System.Data.DataColumn dataColumn46;
		private System.Data.DataColumn dataColumn47;
		private System.Data.DataColumn dataColumn48;
		private System.Data.DataColumn dataColumn49;
		private System.Data.DataColumn dataColumn50;
		private System.Data.DataColumn dataColumn51;
		private System.Data.DataColumn dataColumn52;
		private System.Data.DataColumn dataColumn53;
		private System.Data.DataColumn dataColumn54;
		private System.Data.DataColumn dataColumn55;
		private System.Data.DataColumn dataColumn56;
		private System.Data.DataColumn dataColumn57;
		private System.Data.DataColumn dataColumn58;
		private System.Data.DataColumn dataColumn59;
		private System.Data.DataColumn dataColumn60;
		private System.Data.DataColumn dataColumn61;
		private System.Data.DataColumn dataColumn62;
		private System.Data.DataColumn dataColumn63;
		private System.Data.DataColumn dataColumn64;
		private System.Data.DataColumn dataColumn65;
		private System.Data.DataColumn dataColumn66;
		private System.Data.DataColumn dataColumn67;
		private System.Data.DataColumn dataColumn68;
		private System.Data.DataColumn dataColumn69;
		private System.Data.DataColumn dataColumn70;
		private System.Data.DataColumn dataColumn71;
		private System.Data.DataColumn dataColumn72;
		private System.Data.DataColumn dataColumn73;
		private System.Data.DataColumn dataColumn74;
		private System.Data.DataColumn dataColumn75;
		private System.Data.DataColumn dataColumn76;
		private System.Data.DataColumn dataColumn77;
		private System.Data.DataColumn dataColumn78;
		private System.Data.DataColumn dataColumn79;
		private System.Data.DataColumn dataColumn80;
		private System.Data.DataColumn dataColumn81;
		private System.Data.DataColumn dataColumn82;
		private System.Data.DataColumn dataColumn83;
		private System.Data.DataColumn dataColumn84;
		private System.Data.DataColumn dataColumn85;
		private System.Data.DataColumn dataColumn86;
		private System.Data.DataColumn dataColumn87;
		private System.Data.DataColumn dataColumn88;
		private System.Data.DataColumn dataColumn89;
		private System.Data.DataColumn dataColumn90;
		private Duzon.Common.Controls.DropDownComboBox cb_cd_plant;

	
		/// <summary>
		/// 데이터 셋
		/// </summary>
	

		private DataSet g_dsCombo = null;		
		
			
				
		/// <summary>
		/// Load여부 변수(Paint Event에서 사용)
		/// </summary>
	//	private string m_page_state ;           // 현재 페이지 상태 정의 	
	//	private bool YN_SAVE = false;
		private Duzon.Common.Controls.RoundedButton btn_apply;

		#endregion

		#region -> 멤버필드(주요)	

		private bool _isPainted = false;
		private Duzon.Common.Controls.PanelExt m_pnlGrid2;
		private Duzon.Common.Controls.PanelExt m_pnlGrid1;
		private Dass.FlexGrid.FlexGrid _flexM;
		private Duzon.Common.BpControls.BpCodeTextBox tb_nm_partner;
        private Duzon.Common.BpControls.BpCodeTextBox tb_no_emp;
		private Duzon.Common.BpControls.BpCodeTextBox tb_nm_sl2;
		private Duzon.Common.BpControls.BpCodeTextBox tb_no_emp2;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private Duzon.Common.BpControls.BpCodeTextBox tb_FG_TPRCV;
        private DataColumn dataColumn91;		
		private Dass.FlexGrid.FlexGrid ㅑ_flexD;           // 현재 페이지 상태 정의

		#endregion 

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자

		public P_PU_GR_REG_OLD()
		{
		
			
			//			base.AddAutoAnchorControl(this, panel15,ControlPositionType.StaticHeightVerticalTop);
			//			base.AddAutoAnchorControl(this,panel14,ControlPositionType.Single);

			Cursor.Current = Cursors.WaitCursor;
			InitializeComponent();
			

			this.AddAutoAnchorControl(this, m_pnlGrid1, ControlPositionType.SingleVerticalTop);
			this.AddAutoAnchorControl(this, m_pnlGrid2, ControlPositionType.SingleVerticalButtom);


			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Page_Paint); 
		
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_GR_REG_OLD));
            this.lb_nm_sl2 = new Duzon.Common.Controls.LabelExt();
            this.lb_no_emp2 = new Duzon.Common.Controls.LabelExt();
            this.lb_dt_rcv = new Duzon.Common.Controls.LabelExt();
            this.tb_no_gr = new Duzon.Common.Controls.TextBoxExt();
            this.lb_no_gr = new Duzon.Common.Controls.LabelExt();
            this.lb_nm_sl = new Duzon.Common.Controls.LabelExt();
            this.lb_no_emp = new Duzon.Common.Controls.LabelExt();
            this.lb_nm_partner = new Duzon.Common.Controls.LabelExt();
            this.rb_do_pc = new Duzon.Common.Controls.RadioButtonExt();
            this.rb_not_pc = new Duzon.Common.Controls.RadioButtonExt();
            this.lb_sta_iv = new Duzon.Common.Controls.LabelExt();
            this.lb_gl_plant2 = new Duzon.Common.Controls.LabelExt();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.lb_dt_req = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.tb_FG_TPRCV = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_DT_TO = new Duzon.Common.Controls.DatePicker();
            this.tb_DT_FROM = new Duzon.Common.Controls.DatePicker();
            this.cb_cd_plant = new Duzon.Common.Controls.DropDownComboBox();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.tb_nm_partner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_no_emp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.tb_DT_IO = new Duzon.Common.Controls.DatePicker();
            this.btn_apply = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel13 = new Duzon.Common.Controls.PanelExt();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.lb_dc_rmk = new Duzon.Common.Controls.LabelExt();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.tb_dc_rmk = new Duzon.Common.Controls.TextBoxExt();
            this.tb_nm_sl2 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_no_emp2 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.btn_apply_good = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlGrid2 = new Duzon.Common.Controls.PanelExt();
            this.m_pnlGrid1 = new Duzon.Common.Controls.PanelExt();
            this.dataSet1 = new System.Data.DataSet();
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
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn90 = new System.Data.DataColumn();
            this.dataColumn91 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn29 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn33 = new System.Data.DataColumn();
            this.dataColumn34 = new System.Data.DataColumn();
            this.dataColumn35 = new System.Data.DataColumn();
            this.dataColumn36 = new System.Data.DataColumn();
            this.dataColumn37 = new System.Data.DataColumn();
            this.dataColumn38 = new System.Data.DataColumn();
            this.dataColumn39 = new System.Data.DataColumn();
            this.dataColumn40 = new System.Data.DataColumn();
            this.dataColumn41 = new System.Data.DataColumn();
            this.dataColumn42 = new System.Data.DataColumn();
            this.dataColumn43 = new System.Data.DataColumn();
            this.dataColumn44 = new System.Data.DataColumn();
            this.dataColumn45 = new System.Data.DataColumn();
            this.dataColumn46 = new System.Data.DataColumn();
            this.dataColumn47 = new System.Data.DataColumn();
            this.dataColumn48 = new System.Data.DataColumn();
            this.dataColumn49 = new System.Data.DataColumn();
            this.dataColumn50 = new System.Data.DataColumn();
            this.dataColumn51 = new System.Data.DataColumn();
            this.dataColumn52 = new System.Data.DataColumn();
            this.dataColumn53 = new System.Data.DataColumn();
            this.dataColumn54 = new System.Data.DataColumn();
            this.dataColumn55 = new System.Data.DataColumn();
            this.dataColumn56 = new System.Data.DataColumn();
            this.dataColumn57 = new System.Data.DataColumn();
            this.dataColumn58 = new System.Data.DataColumn();
            this.dataColumn59 = new System.Data.DataColumn();
            this.dataColumn60 = new System.Data.DataColumn();
            this.dataColumn61 = new System.Data.DataColumn();
            this.dataColumn62 = new System.Data.DataColumn();
            this.dataColumn63 = new System.Data.DataColumn();
            this.dataColumn64 = new System.Data.DataColumn();
            this.dataColumn65 = new System.Data.DataColumn();
            this.dataColumn66 = new System.Data.DataColumn();
            this.dataColumn67 = new System.Data.DataColumn();
            this.dataColumn68 = new System.Data.DataColumn();
            this.dataColumn69 = new System.Data.DataColumn();
            this.dataColumn70 = new System.Data.DataColumn();
            this.dataColumn71 = new System.Data.DataColumn();
            this.dataColumn72 = new System.Data.DataColumn();
            this.dataColumn73 = new System.Data.DataColumn();
            this.dataColumn74 = new System.Data.DataColumn();
            this.dataColumn75 = new System.Data.DataColumn();
            this.dataColumn76 = new System.Data.DataColumn();
            this.dataColumn77 = new System.Data.DataColumn();
            this.dataColumn78 = new System.Data.DataColumn();
            this.dataColumn79 = new System.Data.DataColumn();
            this.dataColumn80 = new System.Data.DataColumn();
            this.dataColumn81 = new System.Data.DataColumn();
            this.dataColumn82 = new System.Data.DataColumn();
            this.dataColumn83 = new System.Data.DataColumn();
            this.dataColumn84 = new System.Data.DataColumn();
            this.dataColumn85 = new System.Data.DataColumn();
            this.dataColumn86 = new System.Data.DataColumn();
            this.dataColumn87 = new System.Data.DataColumn();
            this.dataColumn88 = new System.Data.DataColumn();
            this.dataColumn89 = new System.Data.DataColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.rb_do_pc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rb_not_pc)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_TO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_FROM)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_IO)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mTop_PageBase_Title
            // 
            this.mTop_PageBase_Title.Size = new System.Drawing.Size(103, 18);
            this.mTop_PageBase_Title.Text = "구매입고등록";
            // 
            // lb_nm_sl2
            // 
            this.lb_nm_sl2.BackColor = System.Drawing.Color.Transparent;
            this.lb_nm_sl2.Location = new System.Drawing.Point(2, 30);
            this.lb_nm_sl2.Name = "lb_nm_sl2";
            this.lb_nm_sl2.Resizeble = true;
            this.lb_nm_sl2.Size = new System.Drawing.Size(75, 18);
            this.lb_nm_sl2.TabIndex = 77;
            this.lb_nm_sl2.Text = "보관장소";
            this.lb_nm_sl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_no_emp2
            // 
            this.lb_no_emp2.BackColor = System.Drawing.Color.Transparent;
            this.lb_no_emp2.Location = new System.Drawing.Point(2, 5);
            this.lb_no_emp2.Name = "lb_no_emp2";
            this.lb_no_emp2.Resizeble = true;
            this.lb_no_emp2.Size = new System.Drawing.Size(75, 18);
            this.lb_no_emp2.TabIndex = 71;
            this.lb_no_emp2.Text = "담당자";
            this.lb_no_emp2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_dt_rcv
            // 
            this.lb_dt_rcv.BackColor = System.Drawing.Color.Transparent;
            this.lb_dt_rcv.Location = new System.Drawing.Point(2, 5);
            this.lb_dt_rcv.Name = "lb_dt_rcv";
            this.lb_dt_rcv.Resizeble = true;
            this.lb_dt_rcv.Size = new System.Drawing.Size(75, 18);
            this.lb_dt_rcv.TabIndex = 65;
            this.lb_dt_rcv.Text = "입고일자";
            this.lb_dt_rcv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_no_gr
            // 
            this.tb_no_gr.Location = new System.Drawing.Point(85, 3);
            this.tb_no_gr.MaxLength = 20;
            this.tb_no_gr.Name = "tb_no_gr";
            this.tb_no_gr.ReadOnly = true;
            this.tb_no_gr.SelectedAllEnabled = false;
            this.tb_no_gr.Size = new System.Drawing.Size(188, 21);
            this.tb_no_gr.TabIndex = 0;
            this.tb_no_gr.TabStop = false;
            this.tb_no_gr.UseKeyEnter = false;
            this.tb_no_gr.UseKeyF3 = false;
            // 
            // lb_no_gr
            // 
            this.lb_no_gr.BackColor = System.Drawing.Color.Transparent;
            this.lb_no_gr.Location = new System.Drawing.Point(2, 5);
            this.lb_no_gr.Name = "lb_no_gr";
            this.lb_no_gr.Resizeble = true;
            this.lb_no_gr.Size = new System.Drawing.Size(75, 18);
            this.lb_no_gr.TabIndex = 63;
            this.lb_no_gr.Text = "입고번호";
            this.lb_no_gr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_nm_sl
            // 
            this.lb_nm_sl.BackColor = System.Drawing.Color.Transparent;
            this.lb_nm_sl.Location = new System.Drawing.Point(2, 30);
            this.lb_nm_sl.Name = "lb_nm_sl";
            this.lb_nm_sl.Resizeble = true;
            this.lb_nm_sl.Size = new System.Drawing.Size(75, 18);
            this.lb_nm_sl.TabIndex = 60;
            this.lb_nm_sl.Text = "입고형태";
            this.lb_nm_sl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_no_emp
            // 
            this.lb_no_emp.BackColor = System.Drawing.Color.Transparent;
            this.lb_no_emp.Location = new System.Drawing.Point(18, 30);
            this.lb_no_emp.Name = "lb_no_emp";
            this.lb_no_emp.Resizeble = true;
            this.lb_no_emp.Size = new System.Drawing.Size(56, 18);
            this.lb_no_emp.TabIndex = 54;
            this.lb_no_emp.Text = "담당자";
            this.lb_no_emp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_nm_partner
            // 
            this.lb_nm_partner.BackColor = System.Drawing.Color.Transparent;
            this.lb_nm_partner.Location = new System.Drawing.Point(2, 30);
            this.lb_nm_partner.Name = "lb_nm_partner";
            this.lb_nm_partner.Resizeble = true;
            this.lb_nm_partner.Size = new System.Drawing.Size(75, 18);
            this.lb_nm_partner.TabIndex = 51;
            this.lb_nm_partner.Text = "거래처";
            this.lb_nm_partner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rb_do_pc
            // 
            this.rb_do_pc.Location = new System.Drawing.Point(688, 5);
            this.rb_do_pc.Name = "rb_do_pc";
            this.rb_do_pc.Size = new System.Drawing.Size(70, 18);
            this.rb_do_pc.TabIndex = 11;
            this.rb_do_pc.TabStop = true;
            this.rb_do_pc.Text = "처리";
            this.rb_do_pc.TextDD = null;
            this.rb_do_pc.UseKeyEnter = false;
            this.rb_do_pc.CheckedChanged += new System.EventHandler(this.rb_do_pc_CheckedChanged);
            // 
            // rb_not_pc
            // 
            this.rb_not_pc.Location = new System.Drawing.Point(618, 5);
            this.rb_not_pc.Name = "rb_not_pc";
            this.rb_not_pc.Size = new System.Drawing.Size(70, 18);
            this.rb_not_pc.TabIndex = 7;
            this.rb_not_pc.TabStop = true;
            this.rb_not_pc.Text = "미처리";
            this.rb_not_pc.TextDD = null;
            this.rb_not_pc.UseKeyEnter = false;
            this.rb_not_pc.CheckedChanged += new System.EventHandler(this.rb_not_pc_CheckedChanged);
            // 
            // lb_sta_iv
            // 
            this.lb_sta_iv.BackColor = System.Drawing.Color.Transparent;
            this.lb_sta_iv.Location = new System.Drawing.Point(2, 5);
            this.lb_sta_iv.Name = "lb_sta_iv";
            this.lb_sta_iv.Resizeble = true;
            this.lb_sta_iv.Size = new System.Drawing.Size(75, 18);
            this.lb_sta_iv.TabIndex = 48;
            this.lb_sta_iv.Text = "처리상태";
            this.lb_sta_iv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_gl_plant2
            // 
            this.lb_gl_plant2.BackColor = System.Drawing.Color.Transparent;
            this.lb_gl_plant2.Location = new System.Drawing.Point(2, 5);
            this.lb_gl_plant2.Name = "lb_gl_plant2";
            this.lb_gl_plant2.Resizeble = true;
            this.lb_gl_plant2.Size = new System.Drawing.Size(75, 18);
            this.lb_gl_plant2.TabIndex = 45;
            this.lb_gl_plant2.Text = "입고공장";
            this.lb_gl_plant2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(172, 6);
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size(11, 15);
            this.label2.TabIndex = 44;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_dt_req
            // 
            this.lb_dt_req.BackColor = System.Drawing.Color.Transparent;
            this.lb_dt_req.Location = new System.Drawing.Point(2, 5);
            this.lb_dt_req.Name = "lb_dt_req";
            this.lb_dt_req.Resizeble = true;
            this.lb_dt_req.Size = new System.Drawing.Size(75, 18);
            this.lb_dt_req.TabIndex = 41;
            this.lb_dt_req.Text = "의뢰일자";
            this.lb_dt_req.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tb_FG_TPRCV);
            this.panel1.Controls.Add(this.tb_DT_TO);
            this.panel1.Controls.Add(this.tb_DT_FROM);
            this.panel1.Controls.Add(this.cb_cd_plant);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rb_not_pc);
            this.panel1.Controls.Add(this.rb_do_pc);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.tb_nm_partner);
            this.panel1.Controls.Add(this.tb_no_emp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 55);
            this.panel1.TabIndex = 0;
            // 
            // tb_FG_TPRCV
            // 
            this.tb_FG_TPRCV.BackColor = System.Drawing.Color.White;
            this.tb_FG_TPRCV.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_FG_TPRCV.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_FG_TPRCV.ButtonImage")));
            this.tb_FG_TPRCV.ChildMode = "";
            this.tb_FG_TPRCV.CodeName = "";
            this.tb_FG_TPRCV.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_FG_TPRCV.CodeValue = "";
            this.tb_FG_TPRCV.ComboCheck = true;
            this.tb_FG_TPRCV.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB;
            this.tb_FG_TPRCV.ItemBackColor = System.Drawing.Color.White;
            this.tb_FG_TPRCV.Location = new System.Drawing.Point(618, 30);
            this.tb_FG_TPRCV.Name = "tb_FG_TPRCV";
            this.tb_FG_TPRCV.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_FG_TPRCV.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_FG_TPRCV.SearchCode = true;
            this.tb_FG_TPRCV.SelectCount = 0;
            this.tb_FG_TPRCV.SetDefaultValue = false;
            this.tb_FG_TPRCV.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_FG_TPRCV.Size = new System.Drawing.Size(126, 21);
            this.tb_FG_TPRCV.TabIndex = 65;
            this.tb_FG_TPRCV.TabStop = false;
            this.tb_FG_TPRCV.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            this.tb_FG_TPRCV.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            // 
            // tb_DT_TO
            // 
            this.tb_DT_TO.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_TO.DayColor = System.Drawing.SystemColors.ControlText;
            this.tb_DT_TO.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_TO.Location = new System.Drawing.Point(184, 3);
            this.tb_DT_TO.Mask = "####/##/##";
            this.tb_DT_TO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_DT_TO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_TO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_TO.Modified = false;
            this.tb_DT_TO.Name = "tb_DT_TO";
            this.tb_DT_TO.PaddingCharacter = '_';
            this.tb_DT_TO.PassivePromptCharacter = '_';
            this.tb_DT_TO.PromptCharacter = '_';
            this.tb_DT_TO.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_TO.ShowToDay = true;
            this.tb_DT_TO.ShowTodayCircle = true;
            this.tb_DT_TO.ShowUpDown = false;
            this.tb_DT_TO.Size = new System.Drawing.Size(83, 21);
            this.tb_DT_TO.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_TO.TabIndex = 1;
            this.tb_DT_TO.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_TO.TitleForeColor = System.Drawing.Color.Black;
            this.tb_DT_TO.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_TO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_TO.UseKeyF3 = false;
            this.tb_DT_TO.Value = new System.DateTime(((long)(0)));
            this.tb_DT_TO.Validated += new System.EventHandler(this.DataPickerValidated);
            // 
            // tb_DT_FROM
            // 
            this.tb_DT_FROM.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_FROM.DayColor = System.Drawing.SystemColors.ControlText;
            this.tb_DT_FROM.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_FROM.Location = new System.Drawing.Point(85, 3);
            this.tb_DT_FROM.Mask = "####/##/##";
            this.tb_DT_FROM.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_DT_FROM.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_FROM.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_FROM.Modified = false;
            this.tb_DT_FROM.Name = "tb_DT_FROM";
            this.tb_DT_FROM.PaddingCharacter = '_';
            this.tb_DT_FROM.PassivePromptCharacter = '_';
            this.tb_DT_FROM.PromptCharacter = '_';
            this.tb_DT_FROM.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_FROM.ShowToDay = true;
            this.tb_DT_FROM.ShowTodayCircle = true;
            this.tb_DT_FROM.ShowUpDown = false;
            this.tb_DT_FROM.Size = new System.Drawing.Size(83, 21);
            this.tb_DT_FROM.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_FROM.TabIndex = 0;
            this.tb_DT_FROM.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_FROM.TitleForeColor = System.Drawing.Color.Black;
            this.tb_DT_FROM.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_FROM.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_FROM.UseKeyF3 = false;
            this.tb_DT_FROM.Value = new System.DateTime(((long)(0)));
            this.tb_DT_FROM.Validated += new System.EventHandler(this.DataPickerValidated);
            // 
            // cb_cd_plant
            // 
            this.cb_cd_plant.AutoDropDown = true;
            this.cb_cd_plant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cb_cd_plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cd_plant.Location = new System.Drawing.Point(360, 5);
            this.cb_cd_plant.Name = "cb_cd_plant";
            this.cb_cd_plant.ShowCheckBox = false;
            this.cb_cd_plant.Size = new System.Drawing.Size(168, 20);
            this.cb_cd_plant.TabIndex = 2;
            this.cb_cd_plant.UseKeyEnter = false;
            this.cb_cd_plant.UseKeyF3 = false;
            this.cb_cd_plant.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Location = new System.Drawing.Point(5, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(777, 1);
            this.panel9.TabIndex = 64;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.lb_gl_plant2);
            this.panel7.Controls.Add(this.lb_no_emp);
            this.panel7.Location = new System.Drawing.Point(278, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(80, 51);
            this.panel7.TabIndex = 62;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lb_dt_req);
            this.panel6.Controls.Add(this.lb_nm_partner);
            this.panel6.Location = new System.Drawing.Point(1, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(80, 51);
            this.panel6.TabIndex = 61;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel8.Controls.Add(this.lb_sta_iv);
            this.panel8.Controls.Add(this.lb_nm_sl);
            this.panel8.Location = new System.Drawing.Point(531, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(80, 51);
            this.panel8.TabIndex = 63;
            // 
            // tb_nm_partner
            // 
            this.tb_nm_partner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_nm_partner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_nm_partner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_nm_partner.ButtonImage")));
            this.tb_nm_partner.ChildMode = "";
            this.tb_nm_partner.CodeName = "";
            this.tb_nm_partner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_nm_partner.CodeValue = "";
            this.tb_nm_partner.ComboCheck = true;
            this.tb_nm_partner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.tb_nm_partner.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_nm_partner.Location = new System.Drawing.Point(84, 30);
            this.tb_nm_partner.Name = "tb_nm_partner";
            this.tb_nm_partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_nm_partner.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_nm_partner.SearchCode = true;
            this.tb_nm_partner.SelectCount = 0;
            this.tb_nm_partner.SetDefaultValue = false;
            this.tb_nm_partner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_nm_partner.Size = new System.Drawing.Size(180, 21);
            this.tb_nm_partner.TabIndex = 3;
            this.tb_nm_partner.TabStop = false;
            // 
            // tb_no_emp
            // 
            this.tb_no_emp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_no_emp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_no_emp.ButtonImage")));
            this.tb_no_emp.ChildMode = "";
            this.tb_no_emp.CodeName = "";
            this.tb_no_emp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_no_emp.CodeValue = "";
            this.tb_no_emp.ComboCheck = true;
            this.tb_no_emp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.tb_no_emp.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_no_emp.Location = new System.Drawing.Point(361, 29);
            this.tb_no_emp.Name = "tb_no_emp";
            this.tb_no_emp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_no_emp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_no_emp.SearchCode = true;
            this.tb_no_emp.SelectCount = 0;
            this.tb_no_emp.SetDefaultValue = false;
            this.tb_no_emp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_no_emp.Size = new System.Drawing.Size(159, 21);
            this.tb_no_emp.TabIndex = 4;
            this.tb_no_emp.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tb_DT_IO);
            this.panel5.Controls.Add(this.btn_apply);
            this.panel5.Controls.Add(this.panel13);
            this.panel5.Controls.Add(this.panel12);
            this.panel5.Controls.Add(this.panel11);
            this.panel5.Controls.Add(this.panel10);
            this.panel5.Controls.Add(this.tb_dc_rmk);
            this.panel5.Controls.Add(this.tb_no_gr);
            this.panel5.Controls.Add(this.tb_nm_sl2);
            this.panel5.Controls.Add(this.tb_no_emp2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 64);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 55);
            this.panel5.TabIndex = 1;
            // 
            // tb_DT_IO
            // 
            this.tb_DT_IO.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_IO.DayColor = System.Drawing.SystemColors.ControlText;
            this.tb_DT_IO.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_IO.Location = new System.Drawing.Point(362, 3);
            this.tb_DT_IO.Mask = "####/##/##";
            this.tb_DT_IO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_DT_IO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_IO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_IO.Modified = false;
            this.tb_DT_IO.Name = "tb_DT_IO";
            this.tb_DT_IO.PaddingCharacter = '_';
            this.tb_DT_IO.PassivePromptCharacter = '_';
            this.tb_DT_IO.PromptCharacter = '_';
            this.tb_DT_IO.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_IO.ShowToDay = true;
            this.tb_DT_IO.ShowTodayCircle = true;
            this.tb_DT_IO.ShowUpDown = false;
            this.tb_DT_IO.Size = new System.Drawing.Size(90, 21);
            this.tb_DT_IO.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_IO.TabIndex = 1;
            this.tb_DT_IO.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_IO.TitleForeColor = System.Drawing.Color.Black;
            this.tb_DT_IO.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_IO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_IO.UseKeyF3 = false;
            this.tb_DT_IO.Value = new System.DateTime(((long)(0)));
            this.tb_DT_IO.Validated += new System.EventHandler(this.DataPickerValidated);
            // 
            // btn_apply
            // 
            this.btn_apply.BackColor = System.Drawing.Color.White;
            this.btn_apply.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_apply.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_apply.Location = new System.Drawing.Point(218, 28);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(56, 24);
            this.btn_apply.TabIndex = 95;
            this.btn_apply.TabStop = false;
            this.btn_apply.Text = "적용";
            this.btn_apply.UseVisualStyleBackColor = false;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // panel13
            // 
            this.panel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel13.BackgroundImage")));
            this.panel13.Location = new System.Drawing.Point(5, 26);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(777, 1);
            this.panel13.TabIndex = 84;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel12.Controls.Add(this.lb_no_emp2);
            this.panel12.Location = new System.Drawing.Point(531, 1);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(80, 26);
            this.panel12.TabIndex = 83;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel11.Controls.Add(this.lb_dt_rcv);
            this.panel11.Controls.Add(this.lb_dc_rmk);
            this.panel11.Location = new System.Drawing.Point(278, 1);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(80, 51);
            this.panel11.TabIndex = 82;
            // 
            // lb_dc_rmk
            // 
            this.lb_dc_rmk.BackColor = System.Drawing.Color.Transparent;
            this.lb_dc_rmk.Location = new System.Drawing.Point(2, 30);
            this.lb_dc_rmk.Name = "lb_dc_rmk";
            this.lb_dc_rmk.Resizeble = true;
            this.lb_dc_rmk.Size = new System.Drawing.Size(75, 18);
            this.lb_dc_rmk.TabIndex = 80;
            this.lb_dc_rmk.Text = "비고";
            this.lb_dc_rmk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel10.Controls.Add(this.lb_no_gr);
            this.panel10.Controls.Add(this.lb_nm_sl2);
            this.panel10.Location = new System.Drawing.Point(1, 1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(80, 51);
            this.panel10.TabIndex = 2;
            // 
            // tb_dc_rmk
            // 
            this.tb_dc_rmk.Location = new System.Drawing.Point(362, 30);
            this.tb_dc_rmk.MaxLength = 50;
            this.tb_dc_rmk.Name = "tb_dc_rmk";
            this.tb_dc_rmk.SelectedAllEnabled = false;
            this.tb_dc_rmk.Size = new System.Drawing.Size(419, 21);
            this.tb_dc_rmk.TabIndex = 4;
            this.tb_dc_rmk.UseKeyEnter = false;
            this.tb_dc_rmk.UseKeyF3 = false;
            // 
            // tb_nm_sl2
            // 
            this.tb_nm_sl2.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_nm_sl2.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_nm_sl2.ButtonImage")));
            this.tb_nm_sl2.ChildMode = "";
            this.tb_nm_sl2.CodeName = "";
            this.tb_nm_sl2.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_nm_sl2.CodeValue = "";
            this.tb_nm_sl2.ComboCheck = true;
            this.tb_nm_sl2.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.tb_nm_sl2.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_nm_sl2.Location = new System.Drawing.Point(84, 30);
            this.tb_nm_sl2.Name = "tb_nm_sl2";
            this.tb_nm_sl2.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_nm_sl2.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_nm_sl2.SearchCode = true;
            this.tb_nm_sl2.SelectCount = 0;
            this.tb_nm_sl2.SetDefaultValue = false;
            this.tb_nm_sl2.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_nm_sl2.Size = new System.Drawing.Size(132, 21);
            this.tb_nm_sl2.TabIndex = 3;
            this.tb_nm_sl2.TabStop = false;
            this.tb_nm_sl2.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // tb_no_emp2
            // 
            this.tb_no_emp2.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_no_emp2.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_no_emp2.ButtonImage")));
            this.tb_no_emp2.ChildMode = "";
            this.tb_no_emp2.CodeName = "";
            this.tb_no_emp2.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_no_emp2.CodeValue = "";
            this.tb_no_emp2.ComboCheck = true;
            this.tb_no_emp2.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.tb_no_emp2.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_no_emp2.Location = new System.Drawing.Point(615, 4);
            this.tb_no_emp2.Name = "tb_no_emp2";
            this.tb_no_emp2.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_no_emp2.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_no_emp2.SearchCode = true;
            this.tb_no_emp2.SelectCount = 0;
            this.tb_no_emp2.SetDefaultValue = false;
            this.tb_no_emp2.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_no_emp2.Size = new System.Drawing.Size(153, 21);
            this.tb_no_emp2.TabIndex = 2;
            this.tb_no_emp2.TabStop = false;
            // 
            // btn_apply_good
            // 
            this.btn_apply_good.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_apply_good.BackColor = System.Drawing.Color.White;
            this.btn_apply_good.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_apply_good.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_apply_good.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_apply_good.Location = new System.Drawing.Point(710, 125);
            this.btn_apply_good.Name = "btn_apply_good";
            this.btn_apply_good.Size = new System.Drawing.Size(80, 24);
            this.btn_apply_good.TabIndex = 87;
            this.btn_apply_good.TabStop = false;
            this.btn_apply_good.Text = "양품적용";
            this.btn_apply_good.UseVisualStyleBackColor = false;
            this.btn_apply_good.Click += new System.EventHandler(this.btn_apply_good_Click);
            // 
            // m_pnlGrid2
            // 
            this.m_pnlGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid2.Location = new System.Drawing.Point(0, 0);
            this.m_pnlGrid2.Name = "m_pnlGrid2";
            this.m_pnlGrid2.Size = new System.Drawing.Size(787, 239);
            this.m_pnlGrid2.TabIndex = 88;
            // 
            // m_pnlGrid1
            // 
            this.m_pnlGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid1.Location = new System.Drawing.Point(0, 0);
            this.m_pnlGrid1.Name = "m_pnlGrid1";
            this.m_pnlGrid1.Size = new System.Drawing.Size(787, 160);
            this.m_pnlGrid1.TabIndex = 89;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("ko-KR");
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
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
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn90,
            this.dataColumn91});
            this.dataTable1.TableName = "MM_QTIOH";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "NO_IO";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CD_COMPANY";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "CD_PLANT";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "CD_PARTNER";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "CD_SL";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "FG_TRANS";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "YN_RETURN";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "DT_IO";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "GI_PARTNER";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "CD_DEPT";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "NO_EMP";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "DC_RMK";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "DTS_INSERT";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "ID_INSERT";
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "DTS_UPDATE";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "ID_UPDATE";
            // 
            // dataColumn90
            // 
            this.dataColumn90.ColumnName = "YN_AM";
            // 
            // dataColumn91
            // 
            this.dataColumn91.ColumnName = "CD_QTIOTP";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24,
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn28,
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33,
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn39,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42,
            this.dataColumn43,
            this.dataColumn44,
            this.dataColumn45,
            this.dataColumn46,
            this.dataColumn47,
            this.dataColumn48,
            this.dataColumn49,
            this.dataColumn50,
            this.dataColumn51,
            this.dataColumn52,
            this.dataColumn53,
            this.dataColumn54,
            this.dataColumn55,
            this.dataColumn56,
            this.dataColumn57,
            this.dataColumn58,
            this.dataColumn59,
            this.dataColumn60,
            this.dataColumn61,
            this.dataColumn62,
            this.dataColumn63,
            this.dataColumn64,
            this.dataColumn65,
            this.dataColumn66,
            this.dataColumn67,
            this.dataColumn68,
            this.dataColumn69,
            this.dataColumn70,
            this.dataColumn71,
            this.dataColumn72,
            this.dataColumn73,
            this.dataColumn74,
            this.dataColumn75,
            this.dataColumn76,
            this.dataColumn77,
            this.dataColumn78,
            this.dataColumn79,
            this.dataColumn80,
            this.dataColumn81,
            this.dataColumn82,
            this.dataColumn83,
            this.dataColumn84,
            this.dataColumn85,
            this.dataColumn86,
            this.dataColumn87,
            this.dataColumn88,
            this.dataColumn89});
            this.dataTable2.TableName = "MM_QTIO";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "NO_IO";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "NO_IOLINE";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "CD_COMPANY";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "CD_PLANT";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "CD_BIZAREA";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "CD_SL";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "CD_SECTION";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "CD_BIN";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "DT_IO";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "NO_ISURCV";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "NO_ISURCVLINE";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "NO_PSO_MGMT";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "NO_PSOLINE_MGMT";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "FG_PS";
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "YN_PURSALE";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "FG_TPIO";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "FG_IO";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "CD_QTIOTP";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "FG_TRANS";
            // 
            // dataColumn36
            // 
            this.dataColumn36.ColumnName = "FG_TAX";
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "CD_PARTNER";
            // 
            // dataColumn38
            // 
            this.dataColumn38.ColumnName = "CD_ITEM";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "QT_IO";
            // 
            // dataColumn40
            // 
            this.dataColumn40.ColumnName = "QT_RETURN";
            // 
            // dataColumn41
            // 
            this.dataColumn41.ColumnName = "QT_TRANS_INV";
            // 
            // dataColumn42
            // 
            this.dataColumn42.ColumnName = "QT_INSP_INV";
            // 
            // dataColumn43
            // 
            this.dataColumn43.ColumnName = "QT_REJECT_INV";
            // 
            // dataColumn44
            // 
            this.dataColumn44.ColumnName = "QT_GOOD_INV";
            // 
            // dataColumn45
            // 
            this.dataColumn45.ColumnName = "CD_EXCH";
            // 
            // dataColumn46
            // 
            this.dataColumn46.ColumnName = "RT_EXCH";
            // 
            // dataColumn47
            // 
            this.dataColumn47.ColumnName = "UM_EX";
            // 
            // dataColumn48
            // 
            this.dataColumn48.ColumnName = "AM_EX";
            // 
            // dataColumn49
            // 
            this.dataColumn49.ColumnName = "UM";
            // 
            // dataColumn50
            // 
            this.dataColumn50.ColumnName = "AM";
            // 
            // dataColumn51
            // 
            this.dataColumn51.ColumnName = "QT_CLS";
            this.dataColumn51.DataType = typeof(decimal);
            // 
            // dataColumn52
            // 
            this.dataColumn52.ColumnName = "AM_CLS";
            this.dataColumn52.DataType = typeof(decimal);
            // 
            // dataColumn53
            // 
            this.dataColumn53.ColumnName = "VAT";
            this.dataColumn53.DataType = typeof(decimal);
            // 
            // dataColumn54
            // 
            this.dataColumn54.ColumnName = "VAT_CLS";
            this.dataColumn54.DataType = typeof(decimal);
            // 
            // dataColumn55
            // 
            this.dataColumn55.ColumnName = "FG_TAXP";
            // 
            // dataColumn56
            // 
            this.dataColumn56.ColumnName = "UM_STOCK";
            this.dataColumn56.DataType = typeof(decimal);
            // 
            // dataColumn57
            // 
            this.dataColumn57.ColumnName = "UM_EVAL";
            this.dataColumn57.DataType = typeof(decimal);
            // 
            // dataColumn58
            // 
            this.dataColumn58.ColumnName = "YN_AM";
            // 
            // dataColumn59
            // 
            this.dataColumn59.ColumnName = "CD_PJT";
            // 
            // dataColumn60
            // 
            this.dataColumn60.ColumnName = "AM_DISTRIBU";
            // 
            // dataColumn61
            // 
            this.dataColumn61.ColumnName = "RT_CUSTOMS";
            // 
            // dataColumn62
            // 
            this.dataColumn62.ColumnName = "AM_CUSTOMS";
            // 
            // dataColumn63
            // 
            this.dataColumn63.ColumnName = "NO_LC";
            // 
            // dataColumn64
            // 
            this.dataColumn64.ColumnName = "NO_LCLINE";
            this.dataColumn64.DataType = typeof(decimal);
            // 
            // dataColumn65
            // 
            this.dataColumn65.ColumnName = "QT_IMSEAL";
            this.dataColumn65.DataType = typeof(decimal);
            // 
            // dataColumn66
            // 
            this.dataColumn66.ColumnName = "QT_EXLC";
            this.dataColumn66.DataType = typeof(decimal);
            // 
            // dataColumn67
            // 
            this.dataColumn67.ColumnName = "GI_PARTNER";
            // 
            // dataColumn68
            // 
            this.dataColumn68.ColumnName = "NO_EMP";
            // 
            // dataColumn69
            // 
            this.dataColumn69.ColumnName = "CD_GROUP";
            // 
            // dataColumn70
            // 
            this.dataColumn70.ColumnName = "NO_IO_MGMT";
            // 
            // dataColumn71
            // 
            this.dataColumn71.ColumnName = "NO_IOLINE_MGMT";
            this.dataColumn71.DataType = typeof(decimal);
            // 
            // dataColumn72
            // 
            this.dataColumn72.ColumnName = "CD_BIZAREA_RCV";
            // 
            // dataColumn73
            // 
            this.dataColumn73.ColumnName = "CD_PLANT_RCV";
            // 
            // dataColumn74
            // 
            this.dataColumn74.ColumnName = "CD_SL_REF";
            // 
            // dataColumn75
            // 
            this.dataColumn75.ColumnName = "CD_SECTION_REF";
            // 
            // dataColumn76
            // 
            this.dataColumn76.ColumnName = "CD_BIN_REF";
            // 
            // dataColumn77
            // 
            this.dataColumn77.ColumnName = "BILL_PARTNER";
            // 
            // dataColumn78
            // 
            this.dataColumn78.ColumnName = "CD_UNIT_MM";
            // 
            // dataColumn79
            // 
            this.dataColumn79.ColumnName = "QT_UNIT_MM";
            this.dataColumn79.DataType = typeof(decimal);
            // 
            // dataColumn80
            // 
            this.dataColumn80.ColumnName = "UM_EX_PSO";
            this.dataColumn80.DataType = typeof(decimal);
            // 
            // dataColumn81
            // 
            this.dataColumn81.ColumnName = "UNIT_IM";
            // 
            // dataColumn82
            // 
            this.dataColumn82.ColumnName = "NO_LOT";
            // 
            // dataColumn83
            // 
            this.dataColumn83.ColumnName = "YN_INSP";
            // 
            // dataColumn84
            // 
            this.dataColumn84.ColumnName = "QT_MM";
            this.dataColumn84.DataType = typeof(decimal);
            // 
            // dataColumn85
            // 
            this.dataColumn85.ColumnName = "QT_REQ_RCV";
            this.dataColumn85.DataType = typeof(decimal);
            // 
            // dataColumn86
            // 
            this.dataColumn86.ColumnName = "QT_GOOD_REQ";
            this.dataColumn86.DataType = typeof(decimal);
            // 
            // dataColumn87
            // 
            this.dataColumn87.ColumnName = "UM_EX_PO";
            this.dataColumn87.DataType = typeof(decimal);
            // 
            // dataColumn88
            // 
            this.dataColumn88.ColumnName = "RATE_ITEM";
            // 
            // dataColumn89
            // 
            this.dataColumn89.ColumnName = "RATE_EXCHG";
            this.dataColumn89.DataType = typeof(decimal);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_apply_good, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 90;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 155);
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
            this.splitContainer1.Size = new System.Drawing.Size(787, 403);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 88;
            // 
            // P_PU_GR_REG_OLD
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_PU_GR_REG_OLD";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.rb_do_pc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rb_not_pc)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_TO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_FROM)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_IO)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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

		#endregion

		#region ♣ 초기화

		#region -> Page_Load
		
		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				this.Enabled = false;	

				// 페이지를 로드하는 중입니다.
				this.ShowStatusBarMessage(1);				
				this.SetProgressBarValue(100, 10);				
			
				InitControl();
				InitGridM();
				InitGridD();
				
				this.SetProgressBarValue(100, 100);
			
			}
			catch(Exception ex)
			{
				this.ShowStatusBarMessage(0);
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.ShowStatusBarMessage(0);
				this.SetProgressBarValue(100, 0);
				this.ToolBarSearchButtonEnabled = true;				
				Cursor.Current = Cursors.Default;
			}
		}

		
		#endregion

		#region -> InitControl

		/// <summary>
		/// 언어에 따른 라벨 설정
		/// </summary>
		private void InitControl()
		{
			//페이지 네임
			this.//m_lblTitle.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU, "P_PU_GR_REG");//this.PageName;

			//label
			lb_dt_req.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU, "DT_REQ");
			lb_nm_partner.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"NM_PARTNER");
			lb_sta_iv.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"STA_IV");//"처리상태";//
			rb_not_pc.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"NOT_PC");
			rb_do_pc.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"DO_PC");

            lb_nm_sl.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU, "RCV_PT");
			lb_no_gr.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"NO_GR");//"입고번호";
			lb_dt_rcv.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"DT_RCV");
			
			lb_no_emp.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"NO_EMP");
			lb_gl_plant2.Text =this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"GR_PLANT");// "입고공장";
			lb_nm_sl2.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"NM_SL");
			lb_dc_rmk.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"DC");//"비고";
			lb_no_emp2.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"NO_EMP");
			//			btn_AllCancel.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.CM,"SELECT_CANCEL");

			btn_apply_good.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU,"APPLY_GOOD");
			btn_apply.Text = this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.CM,"APPLY");

			this.tb_DT_FROM.Text = "";
			this.tb_DT_TO.Text = "";

			cb_cd_plant.SelectedValue = "";

					
			tb_no_gr.Text = "";
		
			tb_DT_IO.Text = "";
								
			tb_no_emp2.CodeName = this.LoginInfo.EmployeeName;
			tb_no_emp2.CodeValue=this.LoginInfo.EmployeeNo;
		
			tb_dc_rmk.Text = "";			
					
			
			tb_DT_FROM.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
			tb_DT_FROM.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
			
			tb_DT_TO.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
			tb_DT_TO.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
			
			tb_DT_IO.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
			tb_DT_IO.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
			

			//시스템 날짜					
			tb_DT_FROM.Text = MainFrameInterface.GetStringToday.Substring(0,6)+"01";
			tb_DT_TO.Text = MainFrameInterface.GetStringToday;
			tb_DT_IO.Text = MainFrameInterface.GetStringToday;
				
				
		}

		
		#endregion
		
		#region -> InitCombo

		/// <summary>
		/// 콤보박스
		/// </summary>
		private void InitCombo()
		{
			try
			{
				// 메인폼 콤보셋팅(공장)
				if(g_dsCombo != null)
					return;

				g_dsCombo = new DataSet();

//				// 진행상태(PR_0000009)
//				string[] lsa_args = {"P_N;000000"};
//				object[] args = { this.MainFrameInterface.LoginInfo.CompanyCode, lsa_args};
//
				g_dsCombo = this.GetComboData("NC;MA_PLANT");
//			

//				g_dsCombo = (DataSet)this.MainFrameInterface.InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);

				// 공장 콤보
				cb_cd_plant.DataSource = g_dsCombo.Tables[0];
				cb_cd_plant.DisplayMember = "NAME";
				cb_cd_plant.ValueMember = "CODE";	
			}
			catch(Exception ex)
			{				
				this.ShowErrorMessage(ex, this.PageName);
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
					case "NO_RCV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NO_REQ");
						break;
					case "CD_PLANT":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","CD_PLANT");;
						break;
					case "NM_PLANT":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PLANT");;
						break;
					case "CD_PARTNER":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","CD_PARTNER");;
						break;
					case "LN_PARTNER":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PARTNER");;
						break;
					case "DT_REQ":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","DT_REQ");;
						break;
					case "NM_FG_TRANS":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","FG_PO_TR");;
						break;
					case "YN_AM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","YN_AM");;
						break;
					case "DC_RMK":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","DC");;
						break;


					case "S":		
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
					case "CD_UNIT_MM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","UNIT_MM");
						break;
					case "NM_SL":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","NM_SL");
						break;
					case "NO_LOT":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","YN_LOT");
						break;
					case "YN_INSP":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","INSPECTION");
						break;
					case "QT_MM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_REQ");
						break;
					case "QT_REQ_U":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_UNIT");
						break;
					case "QT_REQ_RCV":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_GOOD_GR");
						break;
					case "UNIT_IM":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","UNIT_MNG");
						break;
					case "QT_GOOD_REQ":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_GOOD");
						break;
					case "NM_PROJECT":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","PROJECT ");;
						break;
					case "QT_REJECT_REQ":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_INFERIOR");
						break;
					case "QT_REJECT_REQ_MM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_INFERIOR_RCV ");;
						break;
				}
			}
			
			if(temp == "")
				return "";
			else
				return temp.Substring(3,temp.Length-3);
		}


		#endregion
		
		#region -> InitGridM

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM()
		{	
			Application.DoEvents();
			_flexM = new Dass.FlexGrid.FlexGrid();

			((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
			
			this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
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
			_flexM.Redraw = false;

			_flexM.Rows.Count = 1;
			_flexM.Rows.Fixed = 1;
			_flexM.Cols.Count = 11;
			_flexM.Cols.Fixed = 1;
			_flexM.Rows.DefaultSize = 20;	

			// 헤더텍스트
			//			_flexM[0,1] = "S";
			//			_flexM[0,2] = this.MainFrameInterface.GetDataDictionaryItem("PU","NO_REQ");
			//			_flexM[0,3] = this.MainFrameInterface.GetDataDictionaryItem("PU","CD_PLANT");
			//			_flexM[0,4] = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PLANT");
			//			_flexM[0,5] = this.MainFrameInterface.GetDataDictionaryItem("PU","CD_PARTNER");
			//			_flexM[0,6] = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PARTNER");
			//			_flexM[0,7] = this.MainFrameInterface.GetDataDictionaryItem("PU","DT_REQ");
			//			_flexM[0,8] = this.MainFrameInterface.GetDataDictionaryItem("PU","FG_PO_TR");
			//			_flexM[0,9] = this.MainFrameInterface.GetDataDictionaryItem("PU","YN_AM");
			//			_flexM[0,10] = this.MainFrameInterface.GetDataDictionaryItem("PU","DC");

			_flexM.Cols[0].Width = 50;

			_flexM.Cols[1].Name = "S";
			_flexM.Cols[1].DataType = typeof(string);
			_flexM.Cols[1].Format = "1;0";
			_flexM.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM.Cols[1].Width = 30;
			
			_flexM.Cols[2].Name = "NO_RCV";
			_flexM.Cols[2].DataType = typeof(string);
			_flexM.Cols[2].Width = 120;
		//	_flexM.Cols[2].AllowEditing = false;
			
			_flexM.Cols[3].Name = "CD_PLANT";
			_flexM.Cols[3].DataType = typeof(string);
			_flexM.Cols[3].Width = 80;
			
			_flexM.Cols[4].Name = "NM_PLANT";
			_flexM.Cols[4].DataType = typeof(string);
			_flexM.Cols[4].Width = 120;

			_flexM.Cols[5].Name = "CD_PARTNER";
			_flexM.Cols[5].DataType = typeof(string);
			_flexM.Cols[5].Width = 80;
			
			_flexM.Cols[6].Name = "LN_PARTNER";
			_flexM.Cols[6].DataType = typeof(string);
			_flexM.Cols[6].Width = 120;

			_flexM.Cols[7].Name = "DT_REQ";
			_flexM.Cols[7].DataType = typeof(string);
			_flexM.Cols[7].Width = 100;
			_flexM.Cols[7].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT).Replace("#","9");
			_flexM.Cols[7].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
			_flexM.Cols[7].TextAlign = TextAlignEnum.CenterCenter;
			_flexM.SetStringFormatCol("DT_REQ");			
		
			_flexM.Cols[8].Name = "NM_FG_TRANS";
			_flexM.Cols[8].DataType = typeof(string);
			_flexM.Cols[8].Width = 80;			
		
			_flexM.Cols[9].Name = "YN_AM";
			_flexM.Cols[9].DataType = typeof(string);
			_flexM.Cols[9].Width = 80;
			
			_flexM.Cols[10].Name = "DC_RMK";
			_flexM.Cols[10].DataType = typeof(string);
			_flexM.Cols[10].Width = 150;
			
			_flexM.AllowSorting = AllowSortingEnum.None;
			_flexM.NewRowEditable = false;
			_flexM.EnterKeyAddRow = false;

			_flexM.SumPosition = SumPositionEnum.None;
			_flexM.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM.Cols.Count-1; i++)
				_flexM[0, i] = GetDDItem(_flexM.Cols[i].Name);

			_flexM.Redraw = true;
			
			_flexM.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flexM_StartEdit);
			_flexM.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);				
		}

		#endregion

		#region -> InitGridD

		/// <summary>
		/// Grid Initialization
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
			this._flexD.Size = new System.Drawing.Size(785, 198);
			this._flexD.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{Font:굴림체, 9pt;Trimming:EllipsisCharacter;WordWrap:True;}	Fixed{BackColor:Control;ForeColor:ControlText;TextAlign:CenterCenter;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
			this._flexD.TabIndex = 1;
			m_pnlGrid2.Controls.Add(this._flexD);
			((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();

			_flexD.Redraw = false;

			_flexD.Rows.Count = 1;
			_flexD.Rows.Fixed = 1;
			_flexD.Cols.Count =18;
			_flexD.Cols.Fixed = 1;
			_flexD.Rows.DefaultSize = 20;	

			// 헤더텍스트
			//			_flexD[0,1] = "S";
			//			_flexD[0,2] = this.MainFrameInterface.GetDataDictionaryItem("PU","CD_ITEM");
			//			_flexD[0,3] = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_ITEM");
			//			_flexD[0,4] = this.MainFrameInterface.GetDataDictionaryItem("PU","STND_ITEM");
			//			_flexD[0,5] = this.MainFrameInterface.GetDataDictionaryItem("PU","UNIT_MM");
			//			_flexD[0,6] = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_SL");
			//			_flexD[0,7] = this.MainFrameInterface.GetDataDictionaryItem("PU","YN_LOT");	
			//			_flexD[0,8] = this.MainFrameInterface.GetDataDictionaryItem("PU","INSPECTION");
			//			_flexD[0,9] = this.MainFrameInterface.GetDataDictionaryItem("PU","QT_REQ");
			//			_flexD[0,10] = this.MainFrameInterface.GetDataDictionaryItem("PU","QT_UNIT");
			//			_flexD[0,11] = this.MainFrameInterface.GetDataDictionaryItem("PU","QT_RCV");
			//			_flexD[0,12] = this.MainFrameInterface.GetDataDictionaryItem("PU","UNIT_MNG");
			//			_flexD[0,13] = this.MainFrameInterface.GetDataDictionaryItem("PU","QT_GOOD_GR");
			//			_flexD[0,14] = this.MainFrameInterface.GetDataDictionaryItem("PU","PROJECT");			

			_flexD.Cols[0].Width = 50;

			_flexD.Cols[1].Name = "S";
			_flexD.Cols[1].DataType = typeof(string);
			_flexD.Cols[1].Format = "1;0";
			_flexD.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexD.Cols[1].Width = 30;
			
			_flexD.Cols[2].Name = "CD_ITEM";
			_flexD.Cols[2].DataType = typeof(string);
			_flexD.Cols[2].Width = 120;
			//_flexD.Cols[2].AllowEditing = false;
			
			_flexD.Cols[3].Name = "NM_ITEM";
			_flexD.Cols[3].DataType = typeof(string);
			_flexD.Cols[3].Width = 150;
			_flexD.Cols[3].AllowEditing = false;

			_flexD.Cols[4].Name = "STND_ITEM";
			_flexD.Cols[4].ImageAlign = ImageAlignEnum.RightCenter;
			_flexD.Cols[4].DataType = typeof(string);
			_flexD.Cols[4].Width = 90;
			_flexD.Cols[4].AllowEditing = false;
			//_flexD.Cols[4].Visible = false;

			_flexD.Cols[5].Name = "CD_UNIT_MM";
			_flexD.Cols[5].DataType = typeof(string);
			_flexD.Cols[5].Width = 80;
			_flexD.Cols[5].AllowEditing = false;

			
			_flexD.Cols[6].Name = "NO_LOT";
			_flexD.Cols[6].DataType = typeof(string);
			_flexD.Cols[6].Width = 80;		
			_flexD.Cols[6].AllowEditing = false;	
			_flexD.Cols[6].Visible = false;

			_flexD.Cols[7].Name = "YN_INSP";
			_flexD.Cols[7].DataType = typeof(string);
			_flexD.Cols[7].Format = "Y;N";
			_flexD.Cols[7].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexD.Cols[7].Width = 30;		
			_flexD.Cols[7].AllowEditing = false;
						
			_flexD.Cols[8].Name = "QT_MM";
			_flexD.Cols[8].DataType = typeof(decimal);
			_flexD.Cols[8].Width = 120;
			_flexD.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[8].AllowEditing = false;
			_flexD.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flexD.SetColMaxLength("QT_MM",17);

						
			_flexD.Cols[9].Name = "QT_REQ_RCV";
			_flexD.Cols[9].DataType = typeof(decimal);
			_flexD.Cols[9].Width = 120;			
			_flexD.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flexD.SetColMaxLength("QT_REQ_RCV",17);	
	
			_flexD.Cols[10].Name = "QT_REJECT_REQ_MM";
			_flexD.Cols[10].DataType = typeof(decimal);
			_flexD.Cols[10].Width = 120;			
			_flexD.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flexD.SetColMaxLength("QT_REJECT_REQ_MM",17);	
	
			_flexD.Cols[11].Name = "NM_SL";
			_flexD.Cols[11].DataType = typeof(string);
			_flexD.Cols[11].Width = 120;				
			_flexD.Cols[11].AllowEditing = false;
			
			
			_flexD.Cols[12].Name = "UNIT_IM";
			_flexD.Cols[12].DataType = typeof(string);
			_flexD.Cols[12].Width = 80;		
			_flexD.Cols[12].AllowEditing = false;

			_flexD.Cols[13].Name = "QT_REQ_U";
			_flexD.Cols[13].DataType = typeof(decimal);
			_flexD.Cols[13].Width = 120;		
			_flexD.Cols[13].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[13].AllowEditing = false;
			_flexD.Cols[13].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flexD.SetColMaxLength("QT_REQ_U",17);

			
			_flexD.Cols[14].Name = "QT_GOOD_REQ";
			_flexD.Cols[14].DataType = typeof(decimal);
			_flexD.Cols[14].Width = 120;			
			_flexD.Cols[14].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[14].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flexD.SetColMaxLength("QT_GOOD_REQ",17);

			_flexD.Cols[15].Name = "QT_REJECT_REQ";
			_flexD.Cols[15].DataType = typeof(decimal);
			_flexD.Cols[15].Width = 120;			
			_flexD.Cols[15].TextAlign = TextAlignEnum.RightCenter;
			_flexD.Cols[15].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flexD.SetColMaxLength("QT_REJECT_REQ",17);


			_flexD.Cols[16].Name = "NM_PROJECT";
			_flexD.Cols[16].DataType = typeof(string);
			_flexD.Cols[16].Width = 120;		
			_flexD.Cols[16].AllowEditing = false;

			_flexD.Cols[17].Name = "DC_RMK";
			_flexD.Cols[17].DataType = typeof(string);
			_flexD.Cols[17].Width = 150;
			_flexD.Cols[17].AllowEditing = false;
			_flexD.SetColMaxLength("DC_RMK",40);
		
		
			_flexD.AllowSorting = AllowSortingEnum.None;
			_flexD.NewRowEditable = false;
			_flexD.EnterKeyAddRow = false;

			_flexD.SumPosition = SumPositionEnum.None;
			_flexD.GridStyle = GridStyleEnum.Blue;


			_flexD.SetDummyColumn("S");
			_flexD.SetCodeHelpCol("NM_SL");


			MainFrameInterface.SetUserGrid(_flexD);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD.Cols.Count-1; i++)
				_flexD[0, i] = GetDDItem(_flexD.Cols[i].Name);

			_flexD.Redraw = true;

			_flexD.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flexD_StartEdit);
			_flexD.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flexD_ValidateEdit);
		//	_flexD.HelpClick += new System.EventHandler(OnShowHelp);
			_flexD.CodeHelp += new Dass.FlexGrid.CodeHelpEventHandler(_flex_CodeHelp);
			
		}

		#endregion

		#region -> Page_Paint

		/// <summary>
		/// Page_Paint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this._isPainted)
				return;
			try
			{				
				this._isPainted = true;
				Application.DoEvents();

				// 라벨 번쩍거리는것 막기위해
				//m_lblTitle.Visible = true;
				//m_lblTitle.Text = GetDataDictionaryItem(DataDictionaryTypes.PU, "P_PU_GR_REG");
				//m_lblTitle.Show();
				Application.DoEvents();
				
				InitCombo();

				ToolBarSearchButtonEnabled = true;		
				ToolBarSaveButtonEnabled = true;
				ToolBarAddButtonEnabled= true;

				this.Enabled = true;

				rb_not_pc.Checked = true;
				
				tb_DT_FROM.Focus();
				
			}
			catch//(Exception ex)
			{
				//				ShowStatusBarMessage(0);
				//				SetProgressBarValue(100, 0);
				//				ShowErrorMessage(ex, PageName);
				//				Cursor.Current = Cursors.Default;
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
			
			int row;
			string colName; 
			// 필수입력항목 체크
			if(_flexD.CheckView_HasNull(new string[] {"NM_SL"}, out row, out colName, "OR"))
			{
				this.ShowMessage("WK1_004", GetDDItem(colName));
				_flexD.Select(row, colName);
				_flexD.Focus();
				return false;
			}			

			// mitigate  give off 방출하다. emits 방출하다. 
				
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
				//m_lblTitle.Focus();				
			
				if(!FieldCheckgrreg())
				{
					return false;
				}        
			
				DataTable ldt_woure = _flexD.DataTable.Clone();

				for(int i=0 ; i < _flexD.DataView.Count ;i++)
				{
					if( (System.Double.Parse(_flexD.DataView[i]["QT_REQ_RCV"].ToString().Trim()) + System.Double.Parse(_flexD.DataView[i]["QT_REJECT_REQ_MM"].ToString().Trim())) != 0 )
					{
						ldt_woure.ImportRow(_flexD.DataView[i].Row);
					}
				}
						
				if( ldt_woure == null || ldt_woure.Rows.Count<= 0)
				{
					this.ShowMessage("PU_M000114");					
					return false;
				}
				//m_lblTitle.Focus();
							
				// 
				InDataHeadValue();
			
				object[] m_obj = new object[3];

				m_obj[0] = dataSet1.Tables[0];
				m_obj[1] = ldt_woure;		
				m_obj[2] = this.MainFrameInterface.LoginInfo.UserID;
				
				if(ldt_woure !=null && ldt_woure.Rows.Count >0)
				{
					string	no_seq = "";
					
					no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode,"PU","06",tb_DT_IO.MaskEditBox.ClipText.Substring(0,6));
					dataSet1.Tables[0].Rows[0].BeginEdit();
					dataSet1.Tables[0].Rows[0]["NO_IO"] = no_seq;
					dataSet1.Tables[0].Rows[0].EndEdit();
					
					DataTable ldt_Save = ldt_woure.Clone();
					for(int i =0  ; i < ldt_woure.Rows.Count ;i++)
					{
//						DataRow newrow = ldt_Save.NewRow();
//						for(int j=0 ; j < ldt_woure.Columns.Count;j++)
//						{
//                            newrow[j] = ldt_woure.Rows[i][j]; 
//						}  
 

						ldt_woure.Rows[i].BeginEdit();
			
						ldt_woure.Rows[i]["NO_IO"] = no_seq;
						ldt_woure.Rows[i]["NO_IOLINE"] = i+1;
						ldt_woure.Rows[i]["YN_RETURN"] = "N";
						ldt_woure.Rows[i].EndEdit();
						//ldt_Save.Rows.Add(newrow);						
					}

					// 
					SpInfoCollection sic = new SpInfoCollection();

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();

					si.DataValue = dataSet1.Tables[0]; 					//저장할 데이터 테이블
					si.SpNameInsert = "SP_PU_MM_QTIOH_INSERT";			//Insert 프로시저명				
					
					/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                    si.SpParamsInsert = new string[] { "NO_IO", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO", "GI_PARTNER", "CD_DEPT", "NO_EMP", "DC_RMK", "DTS_INSERT1", "ID_INSERT1", "CD_QTIOTP" };
					// 
					/*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
					si.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
					si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID);
				
					sic.Add(si);

					si = new Duzon.Common.Util.SpInfo();
					si.DataState = DataValueState.Added; 
					si.DataValue = ldt_woure; 					//저장할 데이터 테이블
					si.SpNameInsert = "SP_PU_GR_INSERT";			//Insert 프로시저명
					//	si.spType = "Added";
                    //  
					/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
					si.SpParamsInsert = new string[] { "YN_RETURN", "NO_IO","NO_IOLINE", "CD_COMPANY", "CD_PLANT", "CD_SL", "DT_IO1", "NO_RCV", "NO_LINE", "NO_PO", "NO_POLINE", "FG_PS1", 
														 "FG_TPPURCHASE", "FG_IO", "CD_QTIOTP", "FG_TRANS", "FG_TAX", "CD_PARTNER","CD_ITEM", "QT_GOOD_REQ","QT_REJECT_REQ", "CD_EXCH", "RT_EXCH", "UM_EX", "UM", "VAT", "FG_TAXP",
														 "YN_AM", "CD_PJT", "NO_LC", "NO_LCLINE", "NO_EMP1", "CD_PURGRP","CD_UNIT_MM", "QT_REQ_RCV","QT_REJECT_REQ_MM", "UM_EX_PO","YN_INSP"};
					/* 데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
					si.SpParamsValues.Add(ActionState.Insert, "DT_IO1", tb_DT_IO.MaskEditBox.ClipText);
					si.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "1");
					si.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", tb_no_emp2.CodeValue.ToString());					
					sic.Add(si);
						
					object obj = this.Save(sic);
					ResultData[] result = (ResultData[])obj;
					if(result[0].Result && result[1].Result)
					{
						_flexD.DataTable.AcceptChanges();						
						tb_no_gr.Text =no_seq;
						tb_no_gr.Enabled = false;	
						return true;
					}

					return false;
				}
				
				return false;			

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

		#region -> 조회버튼클릭
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{	
				Cursor.Current = Cursors.WaitCursor;
				
				//필수항목 체크
				if(!Field_Check())
					return;

				this.ShowStatusBarMessage(2);
				this.SetProgressBarValue(100, 10);	
					
				if(_flexM.DataTable != null)
				{
					_flexM.DataTable.Rows.Clear();
					_flexD.DataTable.Rows.Clear();	
					
					tb_no_gr.Text ="";
					tb_nm_sl2.CodeValue ="";
					tb_nm_sl2.CodeName ="";


					Thread.Sleep(50);
				}				

				this.ShowStatusBarMessage(2);

				object[] m_objstr = new object[8];
				m_objstr[0] = this.MainFrameInterface.LoginInfo.CompanyCode;
				m_objstr[1] = tb_DT_FROM.MaskEditBox.ClipText;
				m_objstr[2] = tb_DT_TO.MaskEditBox.ClipText;
				m_objstr[3] = cb_cd_plant.SelectedValue.ToString();
				m_objstr[4] = tb_nm_partner.CodeValue.ToString();
				m_objstr[5] = tb_no_emp.CodeValue.ToString();
                m_objstr[6] = "";// tb_nm_sl.CodeValue.ToString();
                m_objstr[7] = tb_FG_TPRCV.CodeValue;

				SpInfo si = new SpInfo();
				si.SpNameSelect = "UP_PU_GR_SELECT_H";
				si.SpParamsSelect = m_objstr;
				ResultData result = (ResultData)FillDataTable(si);
				DataTable dt = (DataTable)result.DataValue;
				dt.Columns.Add("AFTER_OK");
				
				SpInfo si2 = new SpInfo();
				si2.SpNameSelect = "UP_PU_GR_SELECT_L";
				si2.SpParamsSelect = new Object[] { "xxx","xxx" };
				ResultData result2 = (ResultData)this.FillDataTable(si2);
				DataTable dt2 = (DataTable)result2.DataValue;

				if(dt != null && dt.Rows.Count > 0)
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

					this.ToolBarSaveButtonEnabled = true;

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

		#region -> 저장버튼클릭
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			//m_lblTitle.Focus();
			
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				if(Save())//MsgAndSave(false, false))
				{
					
					this.ToolBarSearchButtonEnabled = true;
					this.ToolBarDeleteButtonEnabled = false;	
					this.ToolBarSaveButtonEnabled = false;
					this.ToolBarAddButtonEnabled = false;

                    OnToolBarSearchButtonClicked(sender, e);

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

		#region -> _flexM_StartEdit
		private void _flexM_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			try
			{		
				if(  _flexM.Cols[e.Col].Name != "S")
				{
					e.Cancel = true;
				}	
			}
			finally
			{
			}			
		}
		#endregion

		#region -> _flexD_StartEdit
		private void _flexD_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			try
			{		
				if( _flexD[e.Row , "YN_INSP"].ToString() == "Y")
				{
					e.Cancel = true;
				}
				else if( _flexD.Cols[e.Col].Name != "S" && _flexD.Cols[e.Col].Name != "QT_GOOD_REQ" && _flexD.Cols[e.Col].Name != "QT_REQ_RCV"
					&& _flexD.Cols[e.Col].Name != "QT_REJECT_REQ" && _flexD.Cols[e.Col].Name != "QT_REJECT_REQ_MM")
				{
					e.Cancel = true;
				}	
			}
			finally
			{
			}			
		}
		#endregion

		#region	-> _flexM_AfterDataRefresh
		
		private void _flexM_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			if(IsChanged(null))
				SetToolBarButtonState(true,true,true,true,false);
			else
				SetToolBarButtonState(true,true,true,false,false);

			if(!_flexM.HasNormalRow)
				ToolBarDeleteButtonEnabled = false;							
							
		}
	
		#endregion			

		#region -> _flexM_AfterRowColChange

		private void _flexM_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{
				if(!_flexM.IsBindingEnd || !_flexM.HasNormalRow)
				{
					_flexD.EmptyRowFilter();
					return;
				}	
				if(e.OldRange.r1 != e.NewRange.r1)
				{
				//	_flexD.Redraw = false;
					ShowDetail(e.NewRange.r1);
				//	_flexD.Redraw = true;
				}

//				if(_flexD.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
//				{			
//					string filter = "NO_RCV = '" + _flexM[_flexM.Row,"NO_RCV"].ToString() + "'";
//
//					_flexD.DataView.RowFilter  = filter;
//
//				}
//				
			}
			catch
			{
			}
		
		}
				
		private void ShowDetail(int row)
		{
			string str_NO_RCV = _flexM[row, "NO_RCV"].ToString();
			string filter = "NO_RCV = '" + str_NO_RCV + "'";

			
			// 왜냐하면 디테일이 아무것도 없는경우 추가 버튼을 눌렀을 때 잘 되게 하기 위해
			if(!_flexM[row, "AFTER_OK"].Equals("Y"))
			{				
				SpInfo si2 = new SpInfo();
				si2.SpNameSelect = "UP_PU_GR_SELECT_L";
				si2.SpParamsSelect = new Object[] { this.LoginInfo.CompanyCode, str_NO_RCV  };
				ResultData result2 = (ResultData)this.FillDataTable(si2);
				DataTable dt = (DataTable)result2.DataValue;
				
				if(dt != null && dt.Rows.Count > 0)
				{
				//	_flexD.SetDummyColumnAll();	// 모든 컬럼을 더미컬럼으로 설정하고 -> 인서트시 빨리 처리되게 하기 위해
				
				//	DataTable srcTable = _flexD.DataTable;

					for(int r = 0; r < dt.Rows.Count; r++)
					{
						_flexD.DataTable.ImportRow(dt.Rows[r]);//LoadDataRow(dt.Rows[r].ItemArray, true);						
					}

				//	_flexD.RemoveDummyColumnAll();	// 다시 원상태로 돌린다.
				}

				_flexM[row, "AFTER_OK"] = "Y";					
				
			}

			_flexD.RowFilter = filter;			// 이 문장이 반드시 처음으로 와야 한다.

			
		}

		#endregion	

		#region -> _flexD_ValidateEdit
		/// <summary>
		/// ValidateEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _flexD_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
		{			
			try
			{
				if( _flexD.AllowEditing )
				{
					// 체크박스 일 경우
					if(e.Checkbox != CheckEnum.None)
					{

					}
					else // 체크박스가 아닐 경우
					{
						if( _flexD.GetData(e.Row,e.Col).ToString() != _flexD.EditData)
						{
							switch(_flexD.Cols[e.Col].Name)
							{
								
								case "QT_REQ_RCV" :
								{
									double ll_qt_rcv = 0;				// 입고량				
									double rt_exch =0;					// 환율	
									double qt_reject_mm =0;
									double qt_reject = 0 ;

									try
									{
										ll_qt_rcv = System.Convert.ToDouble(_flexD.Editor.Text.Trim());//(e.Text);														// 입고량
										rt_exch = System.Double.Parse(_flexD[_flexD.Row, "RATE_EXCHG"].ToString());	// 			
										double aa = System.Convert.ToDouble(rt_exch * ll_qt_rcv);												
										_flexD[_flexD.Row,"QT_GOOD_REQ"] =  aa;		
										// 양품입고량
										qt_reject_mm = System.Convert.ToDouble(_flexD[_flexD.Row,"QT_REJECT_REQ_MM"].ToString());
										qt_reject = System.Convert.ToDouble(_flexD[_flexD.Row,"QT_REJECT_REQ"].ToString());	
										if( System.Convert.ToDouble(_flexD.Editor.Text.Trim())+qt_reject_mm == System.Convert.ToDouble(_flexD[_flexD.Row, "QT_MM"].ToString()) )
										{
											_flexD[_flexD.Row,"VAT"] = System.Convert.ToDecimal(_flexD[_flexD.Row,"OLD_VAT"].ToString());
										}
										else
										{
											_flexD[_flexD.Row,"VAT"] = System.Math.Floor((qt_reject+aa) *System.Convert.ToDouble(_flexD[_flexD.Row,"UM"].ToString())* System.Convert.ToDouble(_flexD[_flexD.Row,"CD_FLAG1"].ToString()));																	
										}
									}
									catch
									{
									}									
									break;	
								}	
								case "QT_REJECT_REQ_MM" :
								{
									double ll_qt_rcv = 0;				// 입고량				
									double rt_exch =0;					// 환율	
									double qt_good_mm =0;
									double qt_good = 0 ;

									try
									{
										ll_qt_rcv = System.Convert.ToDouble(_flexD.Editor.Text.Trim());//(e.Text);														// 입고량
										rt_exch = System.Double.Parse(_flexD[_flexD.Row, "RATE_EXCHG"].ToString());	// 			
										double aa = System.Convert.ToDouble(rt_exch * ll_qt_rcv);												
										_flexD[_flexD.Row,"QT_REJECT_REQ"] =  aa;		
										// 양품입고량
										qt_good_mm = System.Convert.ToDouble(_flexD[_flexD.Row,"QT_REQ_RCV"].ToString());
										qt_good = System.Convert.ToDouble(_flexD[_flexD.Row,"QT_GOOD_REQ"].ToString());	
										if( System.Convert.ToDouble(_flexD.Editor.Text.Trim())+qt_good_mm == System.Convert.ToDouble(_flexD[_flexD.Row, "QT_MM"].ToString()) )
										{
											_flexD[_flexD.Row,"VAT"] = System.Convert.ToDecimal(_flexD[_flexD.Row,"OLD_VAT"].ToString());
										}
										else
										{
											_flexD[_flexD.Row,"VAT"] = System.Math.Floor((qt_good+aa) *System.Convert.ToDouble(_flexD[_flexD.Row,"UM"].ToString())* System.Convert.ToDouble(_flexD[_flexD.Row,"CD_FLAG1"].ToString()));																	
										}
									}
									catch
									{
									}									
									break;	
								}	
								case "QT_GOOD_REQ" :
								{
									try
									{
										double qt_reject = System.Convert.ToDouble(_flexD[_flexD.Row,"QT_REJECT_REQ"].ToString());
										_flexD[_flexD.Row,"VAT"] = System.Math.Floor((System.Convert.ToDouble(_flexD.Editor.Text.Trim())+qt_reject) *System.Convert.ToDouble(_flexD[_flexD.Row,"UM"].ToString())* System.Convert.ToDouble(_flexD[_flexD.Row,"CD_FLAG1"].ToString()));																										
									}
									catch
									{
									}
									break;
								}
								case "QT_REJECT_REQ" :
								{
									try
									{
										double qt_good = System.Convert.ToDouble(_flexD[_flexD.Row,"QT_GOOD_REQ"].ToString());										
										_flexD[_flexD.Row,"VAT"] = System.Math.Floor((System.Convert.ToDouble(_flexD.Editor.Text.Trim())+qt_good) *System.Convert.ToDouble(_flexD[_flexD.Row,"UM"].ToString())* System.Convert.ToDouble(_flexD[_flexD.Row,"CD_FLAG1"].ToString()));																										
									}
									catch
									{
									}
									break;
								}
							}
						}
					}
				}
				// hold back
				if(!ToolBarSaveButtonEnabled)
					this.ToolBarSaveButtonEnabled = true;
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.ShowErrorMessage(ex, PageName);
				return;
			}
		}

	
		#endregion

		#endregion		
		
		#region ♣ 도움창 이벤트 / 메소드

		#region -> 도움창 분기

	
		private void _flex_CodeHelp(object sender, Dass.FlexGrid.CodeHelpEventArgs e)
		{						
			try
			{				
				HelpReturn helpReturn = null;
				HelpParam param = null;

				switch(_flexD.Cols[e.Col].Name)
				{					

					case "NM_SL":
						if( e.Source == CodeHelpEnum.CodeSearch && e.EditValue =="" )
						{
							_flexD[_flexD.Row, "CD_SL"]		= "";
							_flexD[_flexD.Row, "NM_SL"]		= "";
						}
						else
						{
							param = new Duzon.Common.Forms.Help.HelpParam(
								Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, this.MainFrameInterface);
							param.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
							param.P09_CD_PLANT   = _flexD[_flexD.Row, "CD_PLANT"].ToString();
							param.ResultMode = ResultMode.FastMode;
							if(e.Source == CodeHelpEnum.CodeSearch)
								param.P92_DETAIL_SEARCH_CODE = e.EditValue;

							if(e.Source == CodeHelpEnum.CodeSearch)		helpReturn = (HelpReturn)this.CodeSearch(param);
							else										helpReturn = (HelpReturn)this.ShowHelp(param);						

							if(helpReturn.DialogResult == System.Windows.Forms.DialogResult.OK)
							{
								_flexD[_flexD.Row, "CD_SL"]		= helpReturn.CodeValue;
								_flexD[_flexD.Row, "NM_SL"]		= helpReturn.CodeName;

							}	
						}
						break;		
				}
				
			}
			catch(Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	


		#endregion

		#region -> 그리드에서 호출 도움창들
					
		/// <summary>
		/// 보관장소 도움창 
		/// </summary>
		/// <param name="ps_search"></param>
		private void ShowDlgSL(string ps_search)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				object obj = this.LoadHelpWindow("P_MA_SL_SUB", new object[3] {this.MainFrameInterface, ps_search, _flexD[_flexD.Row,"CD_PLANT"].ToString() });
				Cursor.Current = Cursors.Default;
				if(((Duzon.Common.Forms.BaseSearchHelp)obj).ShowDialog() == DialogResult.OK)
				{
					object[] row = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;	
													
					_flexD[_flexD.Row,"CD_SL"] = row[0].ToString();
					_flexD[_flexD.Row,"NM_SL"] = row[1].ToString();	
					
					_flexD.Focus();				
				}					
			}
			catch
			{
			}
		}		

		
		
		#endregion		
	
		#endregion

		#region ♣ 기타 이벤트

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

		#region >> 보관장소 적용 버튼
		private void btn_apply_Click(object sender, System.EventArgs e)
		{
			try
			{
				panel1.Focus();
				
				if(_flexD.DataView.Count < 0)
					return;			
				
				
				for(int i = 0 ; i < _flexD.DataView.Count ;i++)
				{
					if( _flexD.DataView[i]["S"].ToString() == "1")
					{
						_flexD.DataView[i].BeginEdit();
						_flexD.DataView[i]["CD_SL"] = tb_nm_sl2.CodeValue.ToString();
						_flexD.DataView[i]["NM_SL"] = tb_nm_sl2.CodeName.ToString();
						_flexD.DataView[i].EndEdit();
					}
					
				}

//
//				for(int li_i = _flexD.Rows.Count-1;li_i >= _flexD.Rows.Fixed; li_i--)
//				{
//					if(_flexD[li_i,"S"].ToString() == "1")
//					{				
//						string ui_nm_sl = tb_nm_sl2.Text.ToString();
//						string ui_cd_sl = tb_nm_sl2.Tag.ToString();
//					
//						// 창고
//						_flexD[li_i,"NM_SL"] =  ui_nm_sl;	
//						_flexD[li_i,"CD_SL"] =  ui_cd_sl;
//						
//						continue;
//					}
//				}
//				
				_flexD.Focus();				
				
			}
			catch(Exception ex)
			{				
				//this.ShowErrorMessage(ex, this.PageName);
			}
		}
		#endregion

		
		#endregion
		
		#region ♣ 기타 함수

		#region -> InDataHeadValue
		private void InDataHeadValue()
		{

			DataRow newrow;
			
			//m_lblTitle.Focus();
			_flexM.Focus();
			
			int row = _flexM.Row;
			if(row == -1)
			{
				row = 1;
			}
			
			dataSet1.Tables[0].Clear();

			newrow = dataSet1.Tables[0].NewRow();
			newrow["NO_IO"] = tb_no_gr.Text;
			newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;			
			dataSet1.Tables[0].Rows.Add(newrow);
						
			//			if(m_page_state == "Modified" )
			//			{		
			//				dataSet1.Tables[0].AcceptChanges();				
			//			}		
		
			dataSet1.Tables[0].BeginInit();
			DataRow ldr_row = dataSet1.Tables[0].Rows[0];
			
			ldr_row["CD_PLANT"] = cb_cd_plant.SelectedValue.ToString();
			ldr_row["CD_PARTNER"] = _flexM[_flexM.Row, "CD_PARTNER"].ToString();

			//			if(dv_qtioL[row -1]["CD_SL"].ToString() == string.Empty ||
			//				dv_qtioL[row -1]["CD_SL"].ToString() =="")
			//			{
			//				ldr_row["CD_SL"] = acd_sl;
			//			}
			//			else
			//				ldr_row["CD_SL"] = dv_qtioL[row -1]["CD_SL"].ToString();

			ldr_row["FG_TRANS"] = _flexM[_flexM.Row,"FG_TRANS"].ToString();//거래구분
			ldr_row["YN_RETURN"] ="N";												//반품유무		
			ldr_row["DT_IO"] =  tb_DT_IO.MaskEditBox.ClipText;//tb_DT_IO.MaskEditBox.ClipText
			ldr_row["GI_PARTNER"] = "";												//납품처
			ldr_row["CD_DEPT"] ="";													//부서				
			ldr_row["NO_EMP"] = tb_no_emp2.CodeValue.ToString();			
			ldr_row["DC_RMK"] = tb_dc_rmk.Text;
            ldr_row["CD_QTIOTP"] = _flexM[_flexM.Row, "CD_QTIOTP"].ToString();
			//			ldr_row["YN_AM"] ="";	

			dataSet1.Tables[0].EndInit();
		}


		#endregion

		#region >> 계산
		
		/// <summary>
		/// QT_MM(수배수량), QT_R_MM(의뢰수량) 값을 계산합니다.
		/// </summary>
		private void CalReq()
		{		
//			for(int li_i=0 ; li_i < ds_qtioL.Tables[0].Rows.Count ; li_i++)
//			{
//				Double qt_req_mm = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_REQ_MM"].ToString());
//				Double qt_gr_mm = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_GR_MM"].ToString());
//				Double qt_req = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_REQ"].ToString());
//				Double qt_gr = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_GR"].ToString());
//
//				//rate_exchg = System.Convert.ToDouble(gds_dzdwGrid2.Tables["PU_RCVL"].Rows[gridDataBoundGrid1.CurrentCell.RowIndex -1]["RATE_EXCHG"].ToString());
//
//				Double qt_mm = Math.Abs(qt_req_mm-qt_gr_mm);
//				Double qt_r_mm = Math.Abs(qt_req-qt_gr);				
//
//				//수배수량
//				ds_qtioL.Tables[0].Rows[li_i]["QT_MM"] = System.Convert.ToString(qt_mm);
//				//의뢰수량
//				ds_qtioL.Tables[0].Rows[li_i]["QT_R_MM"] = System.Convert.ToString(qt_r_mm);
//			}

		}



		/// <summary>
		/// QT_GR_MM = QT_GR/RATE_EXCHG(수배입고수량)
		/// </summary>
		private void CalQT_GR_MM()
		{
		
//			for(int li_i=0 ; li_i < ds_qtioL.Tables[0].Rows.Count ; li_i++)
//			{
//				Double qt_gr = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_GR"].ToString());
//				Double rate_exchg = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["RATE_EXCHG"].ToString());
//
//				Double qt_gr_mm = qt_gr/rate_exchg;
//
//				//수배입고수량
//				ds_qtioL.Tables[0].Rows[li_i]["QT_GR_MM"] = System.Convert.ToString(qt_gr_mm);				
//			}
		}
		

		/// <summary>
		/// 양품재고 계산 GOOD_INV
		/// </summary>
		private void CalGOOD_INV()
		{		
//			for(int li_i=0 ; li_i < ds_qtioL.Tables[0].Rows.Count ; li_i++)
//			{
//				if(ds_qtioL.Tables[0].Rows[li_i]["TP_QTIO"].ToString() == "1" && ds_qtioL.Tables[0].Rows[li_i]["TP_INV_G"].ToString() == "Y"
//					&& ds_qtioL.Tables[0].Rows[li_i]["TP_VARIATION"].ToString() == "1")
//				{
//					Double qt_gr1 = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_GR"].ToString());
//
//					Double good_inv1 = -qt_gr1;
//
//					ds_qtioL.Tables[0].Rows[li_i]["GOOD_INV"] = System.Convert.ToString(good_inv1);
//
//				}
//				else if(ds_qtioL.Tables[0].Rows[li_i]["TP_QTIO"].ToString() == "1" && ds_qtioL.Tables[0].Rows[li_i]["TP_INV_G"].ToString() == "Y"
//					&& ds_qtioL.Tables[0].Rows[li_i]["TP_VARIATION"].ToString() == "2")
//				{
//					Double qt_gr2 = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_GR"].ToString());
//
//					Double good_inv2 = qt_gr2;
//
//					ds_qtioL.Tables[0].Rows[li_i]["GOOD_INV"] = System.Convert.ToString(good_inv2);
//				}
//				
//				else if(ds_qtioL.Tables[0].Rows[li_i]["TP_QTIO"].ToString() == "2")
//				{
//				
//					Duzon.Common.Controls.MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("PU_M000024"));
//
//				}
//			}

		}
		/// <summary>
		/// 
		/// </summary>
		private void Error_QT_GR()
		{
		
//			for(int li_i=0 ; li_i < ds_qtioL.Tables[0].Rows.Count ; li_i++)
//			{
//				Double qt_gr = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_GR"].ToString());
//				Double qt_req = System.Convert.ToDouble(ds_qtioL.Tables[0].Rows[li_i]["QT_REQ"].ToString());
//
//				if(qt_gr > qt_req)
//				{				
//					ShowErrorMessage("PU_M000020",//m_lblTitle.Text);
//				}				
//				else
//				{
//				}
//				
//			}

		}

		#endregion		

		#region >> 데이터 체크 함수

		/// <summary>
		/// 필수항목체크
		/// </summary>
		private bool Field_Check()
		{
			//의뢰일자 시작일
			if(tb_DT_FROM.Text.Trim() == "")
			{
				MainFrameInterface.ShowMessage("WK1_004", lb_dt_req.Text);
				tb_DT_FROM.Focus();
				return false;
			}

			//의뢰일자 종료일
			if(tb_DT_TO.Text.Trim() == "")
			{
				MainFrameInterface.ShowMessage("WK1_004", lb_dt_req.Text);
				tb_DT_TO.Focus();
				return false;
			}			

			//입고공장
			if(cb_cd_plant.SelectedValue.ToString() == "")
			{
				MainFrameInterface.ShowMessage("WK1_004", lb_gl_plant2.Text);
				cb_cd_plant.Focus();
				return false;
			}	
			return true;			
		}


		/// <summary>
		/// 필수항목체크
		/// </summary>
		private bool FieldCheckgrreg()
		{
			//입고일자 
			if(tb_DT_IO.MaskEditBox.ClipText.Trim() == "")
			{
				MainFrameInterface.ShowMessage("WK1_004", lb_dt_rcv.Text);
				tb_DT_IO.Focus();
				return false;
			}
		
			//담당자
			if(tb_no_emp2.CodeValue.Trim() == "")
			{
				MainFrameInterface.ShowMessage("WK1_004", lb_no_emp2.Text);
				tb_no_emp2.Focus();
				return false;
			}
			
			return true;
			
		}

		
		
		#endregion

		#region >> 양품적용, S/L적용 버튼 이벤트
	
		/// <summary>
		/// 양품적용
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_apply_good_Click(object sender, System.EventArgs e)
		{
			try
			{
				panel1.Focus();

								
				if(_flexD.DataView.Count < 0)
					return;

				for(int i = 0 ; i < _flexD.DataView.Count ;i++)
				{
					if( _flexD.DataView[i]["YN_INSP"].ToString() != "Y")
					{
						_flexD.DataView[i].BeginEdit();
						_flexD.DataView[i]["QT_REQ_RCV"] = System.Convert.ToDecimal(_flexD.DataView[i]["QT_MM"].ToString());								// 입고량					
						_flexD.DataView[i]["QT_GOOD_REQ"] =  System.Convert.ToDecimal(_flexD.DataView[i]["QT_REQ_U"].ToString());
						_flexD.DataView[i]["QT_REJECT_REQ_MM"] = 0;								// 입고량					
						_flexD.DataView[i]["QT_REJECT_REQ"] =  0;																												
						_flexD.DataView[i]["VAT"] = System.Convert.ToDecimal(_flexD.DataView[i]["OLD_VAT"].ToString());
						_flexD.DataView[i].EndEdit();
					}

				}
                
				// 
//				// 양품적용-->> 입고량에는 의뢰량(발주단위겠죠..물론)이 들어가고요..					
//				for(int li_i = _flexD.Rows.Count-1;li_i >= _flexD.Rows.Fixed; li_i--)
//				{	// torn away 
//				
//					_flexD[li_i,"QT_REQ_RCV"] = System.Convert.ToDecimal(_flexD[li_i,"QT_MM"].ToString());								// 입고량
//					
//					_flexD[li_i,"QT_GOOD_REQ"] =  System.Convert.ToDecimal(_flexD[li_i,"QT_REQ_U"].ToString());							
//							
//					_flexD[li_i,"VAT"] = System.Convert.ToDecimal(_flexD[li_i,"OLD_VAT"].ToString());
//
//				}								
				_flexD.Focus();				
			}
			catch(Exception ex)
			{				
				this.ShowErrorMessage(ex, PageName);
			}			
		}

	
		private void rb_not_pc_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
//				if(rb_not_pc.Checked == true)
//				{
//					object[] args = {this.MainFrameInterface.LoginInfo.CompanyCode, tb_DT_FROM.Text,tb_DT_TO.Text, cb_cd_plant.SelectedValue.ToString(), tb_nm_partner.Tag.ToString(), tb_no_emp.Tag.ToString(),tb_nm_sl.Tag.ToString()};
//					ds_qtioL= (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurStorageControl_NTX", "pur.CC_PU_GR_NTX", "CC_PU_GR_NTX.rem", "SelectGRM", args));
//	
//					CalReq();//QT_MM(수배수량), QT_R_MM(의뢰수량) 값을 계산합니다.
//					CalQT_GR_MM();//QT_GR_MM = QT_GR/RATE_EXCHG(수배입고수량)
//					CalGOOD_INV();//양품재고 계산 GOOD_INV				
//				}
//				dv_qtioL = new DataView(ds_qtioL.Tables[0]);
//				//				dzdwGrid.DataSource = dv_qtioL;
//				//
//				//				dzdwGrid.GridBoundColumns[13].StyleInfo.ReadOnly = false;
//				//				dzdwGrid.GridBoundColumns[13].StyleInfo.BackColor= System.Drawing.Color.White;
//				//				dzdwGrid.GridBoundColumns[13].StyleInfo.Enabled = true;
//
//				this.ToolBarSaveButtonEnabled = true;
				// get even with 

			}
			catch//(Exception ex)
			{				
				//this.ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		/// 처리-rb_do_pc_CheckedChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void rb_do_pc_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(rb_do_pc.Checked == true)
				{
					object[] args = new Object[10];
				
					args[0] = tb_DT_FROM.Text;
					args[1] = tb_DT_TO.Text;

					args[2] = tb_nm_partner.CodeValue.ToString();	
					args[3] = tb_nm_partner.CodeName;
				
					args[4] = cb_cd_plant.SelectedValue.ToString();	
					args[5] = g_dsCombo.Tables[0].Rows[0]["NAME"].ToString();

                    args[6] = "";// tb_nm_sl.CodeValue.ToString();
                    args[7] = "";// tb_nm_sl.CodeName;
				
					args[8] = tb_no_emp.CodeValue.ToString();
					args[9] = tb_no_emp.CodeName;
			
					Cursor.Current = Cursors.Default;
			
					// Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
					if(this.MainFrameInterface.IsExistPage("P_PU_GRM_REG", false))
					{
						//- 특정 페이지 닫기
						this.UnLoadPage("P_PU_GRM_REG", false);
					}
					string ls_LinePageName = this.MainFrameInterface.GetDataDictionaryItem("PU", "P_PU_GRM_REG");
					bool isComplete = this.LoadPageFrom("P_PU_GRM_REG", ls_LinePageName, this.Grant, args);

					if ( !isComplete )
					{
						ShowMessage("EK1_002");
					}
				}

				Cursor.Current = Cursors.WaitCursor;

			}
			catch//(Exception ex)
			{				
				//this.ShowErrorMessage(ex, this.PageName);
			}
		}
		#endregion


		private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			switch(e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:		// 창고 도움창
					e.HelpParam.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();
					break;
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:		// 발주유형 도움창					
                    e.HelpParam.P61_CODE1 = "001|005|";
                    break;
			}
		}

        private void OnBpCodeTextBox_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult == DialogResult.OK)
                {
                    System.Data.DataRow[] rows = e.HelpReturn.Rows;
                    switch (e.HelpID)
                    {
                        case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		#endregion
	}
}
