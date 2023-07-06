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

namespace pur
{
	/// <summary>
	/// UserControl1에 대한 요약 설명입니다.
	/// </summary>
	public class P_PU_STS_REG_OLD : Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

        private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel2;
		private Duzon.Common.Controls.PanelExt panel3;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.LabelExt lb_no_io;
		private Duzon.Common.Controls.TextBoxExt tb_no_io;
		private Duzon.Common.Controls.LabelExt lb_dt_trans;
		private Duzon.Common.Controls.LabelExt lb_cd_qtio;
		private Duzon.Common.Controls.LabelExt lb_nm_plant;
		private Duzon.Common.Controls.LabelExt lb_gr_sl;
		private Duzon.Common.Controls.LabelExt lb_gl_sl;
		private Duzon.Common.Controls.LabelExt lb_no_emp;
		private Duzon.Common.Controls.TextBoxExt tb_dc;
		private Duzon.Common.Controls.RoundedButton b_Delete;
		private Duzon.Common.Controls.RoundedButton b_Append;
        private System.ComponentModel.IContainer components;
		private Duzon.Common.Controls.LabelExt label1;
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
		private System.Data.DataColumn dataColumn12;
		private System.Data.DataColumn dataColumn13;
		private System.Data.DataColumn dataColumn14;
		private System.Data.DataColumn dataColumn15;
		private System.Data.DataColumn dataColumn16;
		private Duzon.Common.Controls.RoundedButton btn_INV_CON;
		private System.Data.DataColumn dataColumn17;
		private System.Data.DataColumn dataColumn18;
		private System.Data.DataTable dataTable2;
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
			
		//삭제된 정보를 가지고 있는 테이블
		DataTable gdt_Delete = new DataTable();

		// 페이지 상태
		private string m_page_state;

		// 텍스트 박스 값 변경 변수들 정의
		private string gs_cddept;
		//저장시 CC단에 들어갈 값들 
		private string gs_tpqtio,gfg_trans,gyn_purchase,gfg_tppurchase, gyn_am, sCD_QTIOTP;// Head 부분의 텍스트 박스에서 값을 가져옴
									
		
		DataSet gds_INV = new DataSet();	// 구매관련 집계테이블의 정보를 가지고 있는 DataSet
		private DataSet gds_Combo = null;

		/// <summary>
		/// 그리드 선언
		/// </summary>
		//		private GridDataBoundGrid m_grdSTS = null;

	

		/// <summary>
		/// 데이터셋 선언
		/// </summary>
		private DataSet ds_StsH = null;
		private DataSet ds_StsL = null;

		
		/// <summary>
		/// 사용중인 데이터뷰
		/// </summary>
		//private DataView dv_StsH = null;
		private DataView dv_StsL = null;
	
		
		
		private Duzon.Common.Controls.DropDownComboBox cb_cd_plant;
		
		#endregion

		#region -> 멤버필드(주요)	
	
		private bool _isPainted = false;
		private Duzon.Common.Controls.DatePicker tb_DT_IO;
		private System.Data.DataColumn dataColumn34;
		private Duzon.Common.BpControls.BpCodeTextBox tb_cd_qtio;
		private Duzon.Common.BpControls.BpCodeTextBox tb_no_emp;
		private Duzon.Common.BpControls.BpCodeTextBox tb_gl_sl;
		private Duzon.Common.BpControls.BpCodeTextBox tb_gr_sl;
		private Duzon.Common.Controls.RoundedButton btn_REQ;
		private System.Data.DataColumn dataColumn35;
		private System.Data.DataColumn dataColumn36;
		private System.Data.DataColumn dataColumn37;
        private TableLayoutPanel tableLayoutPanel1;
        private PanelExt m_gridTmp;
		private Dass.FlexGrid.FlexGrid _flex;
		
		#endregion

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자
        public P_PU_STS_REG_OLD()
		{
			ds_StsH = new DataSet();
			ds_StsL = new DataSet();

			Cursor.Current = Cursors.WaitCursor;
			InitializeComponent();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_STS_REG));
            this.lb_no_io = new Duzon.Common.Controls.LabelExt();
            this.tb_no_io = new Duzon.Common.Controls.TextBoxExt();
            this.lb_dt_trans = new Duzon.Common.Controls.LabelExt();
            this.lb_cd_qtio = new Duzon.Common.Controls.LabelExt();
            this.lb_nm_plant = new Duzon.Common.Controls.LabelExt();
            this.lb_gr_sl = new Duzon.Common.Controls.LabelExt();
            this.lb_gl_sl = new Duzon.Common.Controls.LabelExt();
            this.lb_no_emp = new Duzon.Common.Controls.LabelExt();
            this.tb_dc = new Duzon.Common.Controls.TextBoxExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.tb_DT_IO = new Duzon.Common.Controls.DatePicker();
            this.cb_cd_plant = new Duzon.Common.Controls.DropDownComboBox();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.tb_cd_qtio = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_no_emp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_gl_sl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_gr_sl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.b_Delete = new Duzon.Common.Controls.RoundedButton(this.components);
            this.b_Append = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
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
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn33 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
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
            this.dataColumn34 = new System.Data.DataColumn();
            this.dataColumn35 = new System.Data.DataColumn();
            this.dataColumn36 = new System.Data.DataColumn();
            this.dataColumn37 = new System.Data.DataColumn();
            this.btn_INV_CON = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_REQ = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_gridTmp = new Duzon.Common.Controls.PanelExt();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_IO)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_gridTmp.SuspendLayout();
            this.SuspendLayout();
            // 
            // mTop_PageBase_Title
            // 
            this.mTop_PageBase_Title.Size = new System.Drawing.Size(128, 18);
            this.mTop_PageBase_Title.Text = "S/L간 이동 처리";
            // 
            // lb_no_io
            // 
            this.lb_no_io.Location = new System.Drawing.Point(2, 5);
            this.lb_no_io.Name = "lb_no_io";
            this.lb_no_io.Resizeble = true;
            this.lb_no_io.Size = new System.Drawing.Size(85, 18);
            this.lb_no_io.TabIndex = 0;
            this.lb_no_io.Text = "수불번호";
            this.lb_no_io.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_no_io
            // 
            this.tb_no_io.Location = new System.Drawing.Point(97, 4);
            this.tb_no_io.MaxLength = 20;
            this.tb_no_io.Name = "tb_no_io";
            this.tb_no_io.ReadOnly = true;
            this.tb_no_io.SelectedAllEnabled = false;
            this.tb_no_io.Size = new System.Drawing.Size(100, 21);
            this.tb_no_io.TabIndex = 0;
            this.tb_no_io.TabStop = false;
            this.tb_no_io.UseKeyEnter = false;
            this.tb_no_io.UseKeyF3 = false;
            // 
            // lb_dt_trans
            // 
            this.lb_dt_trans.Location = new System.Drawing.Point(2, 5);
            this.lb_dt_trans.Name = "lb_dt_trans";
            this.lb_dt_trans.Resizeble = true;
            this.lb_dt_trans.Size = new System.Drawing.Size(85, 18);
            this.lb_dt_trans.TabIndex = 2;
            this.lb_dt_trans.Text = "이동일";
            this.lb_dt_trans.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_cd_qtio
            // 
            this.lb_cd_qtio.Location = new System.Drawing.Point(2, 5);
            this.lb_cd_qtio.Name = "lb_cd_qtio";
            this.lb_cd_qtio.Resizeble = true;
            this.lb_cd_qtio.Size = new System.Drawing.Size(85, 18);
            this.lb_cd_qtio.TabIndex = 5;
            this.lb_cd_qtio.Text = "수불형태";
            this.lb_cd_qtio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_nm_plant
            // 
            this.lb_nm_plant.Location = new System.Drawing.Point(2, 31);
            this.lb_nm_plant.Name = "lb_nm_plant";
            this.lb_nm_plant.Resizeble = true;
            this.lb_nm_plant.Size = new System.Drawing.Size(85, 18);
            this.lb_nm_plant.TabIndex = 8;
            this.lb_nm_plant.Text = "공장";
            this.lb_nm_plant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_gr_sl
            // 
            this.lb_gr_sl.Location = new System.Drawing.Point(2, 31);
            this.lb_gr_sl.Name = "lb_gr_sl";
            this.lb_gr_sl.Resizeble = true;
            this.lb_gr_sl.Size = new System.Drawing.Size(85, 18);
            this.lb_gr_sl.TabIndex = 14;
            this.lb_gr_sl.Text = "입고창고";
            this.lb_gr_sl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_gl_sl
            // 
            this.lb_gl_sl.Location = new System.Drawing.Point(2, 31);
            this.lb_gl_sl.Name = "lb_gl_sl";
            this.lb_gl_sl.Resizeble = true;
            this.lb_gl_sl.Size = new System.Drawing.Size(85, 18);
            this.lb_gl_sl.TabIndex = 11;
            this.lb_gl_sl.Text = "출고창고";
            this.lb_gl_sl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_no_emp
            // 
            this.lb_no_emp.Location = new System.Drawing.Point(1, 57);
            this.lb_no_emp.Name = "lb_no_emp";
            this.lb_no_emp.Resizeble = true;
            this.lb_no_emp.Size = new System.Drawing.Size(85, 18);
            this.lb_no_emp.TabIndex = 20;
            this.lb_no_emp.Text = "담당자";
            this.lb_no_emp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_dc
            // 
            this.tb_dc.Location = new System.Drawing.Point(356, 56);
            this.tb_dc.Name = "tb_dc";
            this.tb_dc.SelectedAllEnabled = false;
            this.tb_dc.Size = new System.Drawing.Size(420, 21);
            this.tb_dc.TabIndex = 7;
            this.tb_dc.UseKeyEnter = false;
            this.tb_dc.UseKeyF3 = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tb_DT_IO);
            this.panel1.Controls.Add(this.cb_cd_plant);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.tb_no_io);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tb_dc);
            this.panel1.Controls.Add(this.tb_cd_qtio);
            this.panel1.Controls.Add(this.tb_no_emp);
            this.panel1.Controls.Add(this.tb_gl_sl);
            this.panel1.Controls.Add(this.tb_gr_sl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 84);
            this.panel1.TabIndex = 0;
            // 
            // tb_DT_IO
            // 
            this.tb_DT_IO.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_IO.DayColor = System.Drawing.SystemColors.ControlText;
            this.tb_DT_IO.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_IO.Location = new System.Drawing.Point(356, 3);
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
            // cb_cd_plant
            // 
            this.cb_cd_plant.AutoDropDown = true;
            this.cb_cd_plant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cb_cd_plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cd_plant.Location = new System.Drawing.Point(97, 31);
            this.cb_cd_plant.Name = "cb_cd_plant";
            this.cb_cd_plant.ShowCheckBox = false;
            this.cb_cd_plant.Size = new System.Drawing.Size(159, 20);
            this.cb_cd_plant.TabIndex = 3;
            this.cb_cd_plant.UseKeyEnter = false;
            this.cb_cd_plant.UseKeyF3 = false;
            this.cb_cd_plant.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Location = new System.Drawing.Point(5, 78);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(777, 1);
            this.panel7.TabIndex = 31;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.Location = new System.Drawing.Point(5, 52);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(777, 1);
            this.panel6.TabIndex = 30;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Location = new System.Drawing.Point(5, 26);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(777, 1);
            this.panel4.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lb_gr_sl);
            this.panel5.Controls.Add(this.lb_cd_qtio);
            this.panel5.Location = new System.Drawing.Point(522, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(90, 52);
            this.panel5.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.lb_dt_trans);
            this.panel3.Controls.Add(this.lb_gl_sl);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(261, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(90, 78);
            this.panel3.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label1.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(14, 58);
            this.label1.Name = "label1";
            this.label1.Resizeble = true;
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 33;
            this.label1.Text = "비고";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.lb_nm_plant);
            this.panel2.Controls.Add(this.lb_no_io);
            this.panel2.Controls.Add(this.lb_no_emp);
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(91, 105);
            this.panel2.TabIndex = 20;
            // 
            // tb_cd_qtio
            // 
            this.tb_cd_qtio.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_cd_qtio.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_cd_qtio.ButtonImage")));
            this.tb_cd_qtio.ChildMode = "";
            this.tb_cd_qtio.CodeName = "";
            this.tb_cd_qtio.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_cd_qtio.CodeValue = "";
            this.tb_cd_qtio.ComboCheck = true;
            this.tb_cd_qtio.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB;
            this.tb_cd_qtio.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_cd_qtio.Location = new System.Drawing.Point(617, 2);
            this.tb_cd_qtio.Name = "tb_cd_qtio";
            this.tb_cd_qtio.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_cd_qtio.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.tb_cd_qtio.SearchCode = true;
            this.tb_cd_qtio.SelectCount = 0;
            this.tb_cd_qtio.SetDefaultValue = false;
            this.tb_cd_qtio.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_cd_qtio.Size = new System.Drawing.Size(159, 21);
            this.tb_cd_qtio.TabIndex = 2;
            this.tb_cd_qtio.TabStop = false;
            this.tb_cd_qtio.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            this.tb_cd_qtio.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
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
            this.tb_no_emp.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_no_emp.Location = new System.Drawing.Point(96, 56);
            this.tb_no_emp.Name = "tb_no_emp";
            this.tb_no_emp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_no_emp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_no_emp.SearchCode = true;
            this.tb_no_emp.SelectCount = 0;
            this.tb_no_emp.SetDefaultValue = false;
            this.tb_no_emp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_no_emp.Size = new System.Drawing.Size(152, 21);
            this.tb_no_emp.TabIndex = 6;
            this.tb_no_emp.TabStop = false;
            this.tb_no_emp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            // 
            // tb_gl_sl
            // 
            this.tb_gl_sl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_gl_sl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_gl_sl.ButtonImage")));
            this.tb_gl_sl.ChildMode = "";
            this.tb_gl_sl.CodeName = "";
            this.tb_gl_sl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_gl_sl.CodeValue = "";
            this.tb_gl_sl.ComboCheck = true;
            this.tb_gl_sl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.tb_gl_sl.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_gl_sl.Location = new System.Drawing.Point(356, 29);
            this.tb_gl_sl.Name = "tb_gl_sl";
            this.tb_gl_sl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_gl_sl.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_gl_sl.SearchCode = true;
            this.tb_gl_sl.SelectCount = 0;
            this.tb_gl_sl.SetDefaultValue = false;
            this.tb_gl_sl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_gl_sl.Size = new System.Drawing.Size(156, 21);
            this.tb_gl_sl.TabIndex = 4;
            this.tb_gl_sl.TabStop = false;
            this.tb_gl_sl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // tb_gr_sl
            // 
            this.tb_gr_sl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_gr_sl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_gr_sl.ButtonImage")));
            this.tb_gr_sl.ChildMode = "";
            this.tb_gr_sl.CodeName = "";
            this.tb_gr_sl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_gr_sl.CodeValue = "";
            this.tb_gr_sl.ComboCheck = true;
            this.tb_gr_sl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.tb_gr_sl.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_gr_sl.Location = new System.Drawing.Point(617, 29);
            this.tb_gr_sl.Name = "tb_gr_sl";
            this.tb_gr_sl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_gr_sl.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_gr_sl.SearchCode = true;
            this.tb_gr_sl.SelectCount = 0;
            this.tb_gr_sl.SetDefaultValue = false;
            this.tb_gr_sl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_gr_sl.Size = new System.Drawing.Size(159, 21);
            this.tb_gr_sl.TabIndex = 5;
            this.tb_gr_sl.TabStop = false;
            this.tb_gr_sl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // b_Delete
            // 
            this.b_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Delete.BackColor = System.Drawing.Color.White;
            this.b_Delete.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.b_Delete.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.b_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_Delete.Location = new System.Drawing.Point(123, 0);
            this.b_Delete.Name = "b_Delete";
            this.b_Delete.Size = new System.Drawing.Size(60, 24);
            this.b_Delete.TabIndex = 82;
            this.b_Delete.TabStop = false;
            this.b_Delete.Text = "삭제";
            this.b_Delete.UseVisualStyleBackColor = false;
            this.b_Delete.Click += new System.EventHandler(this.b_Delete_Click);
            // 
            // b_Append
            // 
            this.b_Append.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Append.BackColor = System.Drawing.Color.White;
            this.b_Append.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.b_Append.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.b_Append.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_Append.Location = new System.Drawing.Point(62, 0);
            this.b_Append.Name = "b_Append";
            this.b_Append.Size = new System.Drawing.Size(60, 24);
            this.b_Append.TabIndex = 83;
            this.b_Append.TabStop = false;
            this.b_Append.Text = "추가";
            this.b_Append.UseVisualStyleBackColor = false;
            this.b_Append.Click += new System.EventHandler(this.b_Append_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this._flex);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 123);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(787, 435);
            this.panel8.TabIndex = 0;
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
            this._flex.IsDataChanged = false;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(0, 0);
            this._flex.Name = "_flex";
            this._flex.RowFilter = "";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(787, 435);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
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
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33});
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
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "CD_SL_IN";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "CD_SL_OUT";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "CD_QTIOTP";
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "FG_PS";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "YN_AM";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "YN_PURCHASE";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
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
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37});
            this.dataTable2.TableName = "MM_QTIO";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "S";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "NO_IO";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "NO_IOLINE";
            this.dataColumn21.DataType = typeof(decimal);
            this.dataColumn21.DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "CD_COMPANY";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "CD_ITEM";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "NM_ITEM";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "STND_ITEM";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "UNIT_IM";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "NO_LOT";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "QT_GOOD_INV";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "QT_REJECT_INV";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "AM";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "NO_ISURCV";
            // 
            // dataColumn36
            // 
            this.dataColumn36.ColumnName = "NO_ISURCVLINE";
            this.dataColumn36.DataType = typeof(decimal);
            this.dataColumn36.DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "FLAG";
            // 
            // btn_INV_CON
            // 
            this.btn_INV_CON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_INV_CON.BackColor = System.Drawing.Color.White;
            this.btn_INV_CON.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_INV_CON.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_INV_CON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_INV_CON.Location = new System.Drawing.Point(184, 0);
            this.btn_INV_CON.Name = "btn_INV_CON";
            this.btn_INV_CON.Size = new System.Drawing.Size(72, 24);
            this.btn_INV_CON.TabIndex = 84;
            this.btn_INV_CON.TabStop = false;
            this.btn_INV_CON.Text = "재고확인";
            this.btn_INV_CON.UseVisualStyleBackColor = false;
            this.btn_INV_CON.Click += new System.EventHandler(this.btn_INV_CON_Click);
            // 
            // btn_REQ
            // 
            this.btn_REQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_REQ.BackColor = System.Drawing.Color.White;
            this.btn_REQ.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_REQ.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_REQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_REQ.Location = new System.Drawing.Point(257, 0);
            this.btn_REQ.Name = "btn_REQ";
            this.btn_REQ.Size = new System.Drawing.Size(72, 24);
            this.btn_REQ.TabIndex = 85;
            this.btn_REQ.TabStop = false;
            this.btn_REQ.Text = "요청적용";
            this.btn_REQ.UseVisualStyleBackColor = false;
            this.btn_REQ.Click += new System.EventHandler(this.btn_REQ_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_gridTmp, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 86;
            // 
            // m_gridTmp
            // 
            this.m_gridTmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridTmp.Controls.Add(this.btn_INV_CON);
            this.m_gridTmp.Controls.Add(this.b_Delete);
            this.m_gridTmp.Controls.Add(this.b_Append);
            this.m_gridTmp.Controls.Add(this.btn_REQ);
            this.m_gridTmp.Location = new System.Drawing.Point(458, 93);
            this.m_gridTmp.Name = "m_gridTmp";
            this.m_gridTmp.Size = new System.Drawing.Size(332, 24);
            this.m_gridTmp.TabIndex = 87;
            // 
            // P_PU_STS_REG
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_PU_STS_REG";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_IO)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_gridTmp.ResumeLayout(false);
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

		/// <summary>
		/// 그리드에 바인딩할 기본 데이터 테이블을 초기화 한다.
		/// </summary>
//		private void InitializeDefaultDataTable()
//		{
//			// 관리항목 테이블
//			dt_Sts = new DataTable();
//			// 체크박스
//			dt_Sts.Columns.Add(new DataColumn("S"));
//			// 항목코드
//			dt_Sts.Columns.Add(new DataColumn("CD_MNG"));
//			// 관리항목명
//			dt_Sts.Columns.Add(new DataColumn("NM_MNG"));
//			// 성격구분
//			dt_Sts.Columns.Add(new DataColumn("TP_MNGNM"));
//			// 코드여부
//			dt_Sts.Columns.Add(new DataColumn("YN_CDMNGNM"));
//			// 형태
//			dt_Sts.Columns.Add(new DataColumn("TP_MNGFORMNM"));
//			// default값
//			dt_Sts.Columns.Add(new DataColumn("CD_MNGDDEF"));
//			// 사용여부
//			dt_Sts.Columns.Add(new DataColumn("YN_USENM"));
//		}
		
		#region -> Page_Load

		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				this.Enabled = false;	

				// 페이지를 로드하는 중입니다.
				this.ShowStatusBarMessage(1);				
				this.SetProgressBarValue(100, 10);				
			
				InitControl();				
				InitGrid();
	
				// 페이지 상태 정의
				m_page_state = "Added";

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

		#region -> InitCombo
		/// <summary>
		/// 콤보박스
		/// </summary>
		private void InitCombo()
		{
			try
			{
				if(gds_Combo == null)
					gds_Combo = new DataSet();
				else
					gds_Combo.Clear();
//		
//				// 콤보셋팅(거래구분, 종료여부)
//				string[] lsa_args = {"P_N;000000"};
//				object[] args1 = { this.MainFrameInterface.LoginInfo.CompanyCode, lsa_args};
//				gds_Combo = (DataSet)InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args1);
										
				gds_Combo = this.GetComboData("NC;MA_PLANT");


				// 공장 콤보
				cb_cd_plant.DataSource = gds_Combo.Tables[0].DefaultView;
				cb_cd_plant.DisplayMember = "NAME";
				cb_cd_plant.ValueMember = "CODE";
			}
			catch
			{

			}
			finally
			{
				ToolBarAddButtonEnabled = true;
				ToolBarSearchButtonEnabled = true;
				ToolBarSaveButtonEnabled = true;
				ToolBarDeleteButtonEnabled = true;
				//ToolBarAddButtonEnabled = true;

			}
		}

		
		#endregion
		
		#region -> InitControl
		/// <summary>
		/// 언어에 따른 라벨 설정
		/// </summary>
		private void InitControl()
		{
			try
			{
				//m_lblTitle.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","P_PU_STS_REG");
				lb_no_io.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NO_IO");
				//				lb_alo_dept.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","ALO_DEPT");
				lb_dt_trans.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","DT_TRANS");
				lb_cd_qtio.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","CD_QTIO");
			
				lb_nm_plant.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","GI_PLANT");
				lb_no_emp.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_EMP");
				lb_gr_sl.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","GR_SL");
				lb_gl_sl.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","GI_SL");

				m_page_state="Added";

				tb_no_io.Text="";		
			
				

				cb_cd_plant.SelectedValue = "";

			
			
				tb_dc.Text ="";
			
				gs_tpqtio = "";

			
				gs_cddept= "";
				gs_tpqtio= "";
				gfg_trans= "";
				gyn_purchase= "";
				gfg_tppurchase= "";				
				gyn_am= "";
                sCD_QTIOTP = string.Empty;
			
				ds_StsL.Clear();
				gdt_Delete.Clear();	
		
				/**************************************************/

				gs_cddept = this.LoginInfo.DeptCode;
				tb_no_emp.CodeName= this.LoginInfo.EmployeeName;
				tb_no_emp.CodeValue= this.LoginInfo.EmployeeNo;

				this.tb_DT_IO.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				this.tb_DT_IO.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

	

				//시스템 날짜					
				string ls_day = MainFrameInterface.GetStringToday;				
				this.tb_DT_IO.Text = ls_day.Substring(0,8);				
				tb_DT_IO.Focus();		
						
				this.ds_StsL = ds_Ty1.Copy();
				dv_StsL= new DataView(this.ds_StsL.Tables[1]);
				gdt_Delete= ds_StsL.Tables[1].Clone();

			}
			catch
			{
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
					case "UNIT_IM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","UNIT_IM");
						break;
					case "NO_LOT":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","YN_LOT");
						break;
					case "QT_GOOD_INV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_GOOD");
						break;
					case "QT_REJECT_INV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_INFERIOR");
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

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGrid()
		{	
			Application.DoEvents();
			
			_flex.Redraw = false;

			_flex.Rows.Count = 1;
			_flex.Rows.Fixed = 1;
			_flex.Cols.Count = 9;
			_flex.Cols.Fixed = 1;
			_flex.Rows.DefaultSize = 20;	

			_flex.Cols[0].Width = 50;

			_flex.Cols[1].Name = "S";
			_flex.Cols[1].DataType = typeof(string);
			_flex.Cols[1].Format = "1;0";
			_flex.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flex.Cols[1].Width = 30;
			
			_flex.Cols[2].Name = "CD_ITEM";
			_flex.Cols[2].DataType = typeof(string);
			_flex.Cols[2].Width = 90;
			_flex.Cols[2].AllowEditing = true;
			
			_flex.Cols[3].Name = "NM_ITEM";
			_flex.Cols[3].DataType = typeof(string);
			_flex.Cols[3].Width = 90;
			_flex.Cols[3].AllowEditing = false;
			
			_flex.Cols[4].Name = "STND_ITEM";
			_flex.Cols[4].DataType = typeof(string);
			_flex.Cols[4].Width = 90;
			_flex.Cols[4].AllowEditing = false;
			
			_flex.Cols[5].Name = "UNIT_IM";
			_flex.Cols[5].DataType = typeof(string);
			_flex.Cols[5].Width = 90;
			_flex.Cols[5].AllowEditing = false;

			_flex.Cols[6].Name = "NO_LOT";
			_flex.Cols[6].DataType = typeof(string);
			_flex.Cols[6].Width = 90;
			_flex.Cols[6].AllowEditing = false;
			
			_flex.Cols[7].Name = "QT_GOOD_INV";	
			_flex.Cols[7].DataType = typeof(decimal);
			_flex.Cols[7].Width = 120;
			_flex.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[7].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_GOOD_INV",17);	
			
			_flex.Cols[8].Name = "QT_REJECT_INV";	
			_flex.Cols[8].DataType = typeof(decimal);
			_flex.Cols[8].Width = 120;
			_flex.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[8].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_REJECT_INV",17);	
		
			_flex.AllowSorting = AllowSortingEnum.None;			
		//	_flex.SumPosition = SumPositionEnum.Top;

			_flex.NewRowEditable = false;
			_flex.EnterKeyAddRow = true;

			_flex.GridStyle = GridStyleEnum.Green;					
			_flex.SetCodeHelpCol("CD_ITEM","NM_SL");

			SetUserGrid(_flex);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flex.Cols.Count-1; i++)
				_flex[0, i] = GetDDItem(_flex.Cols[i].Name);

			_flex.Redraw = true;
			_flex.AddRow += new System.EventHandler(b_Append_Click);
		//	_flex.AfterDataRefresh += new System.ComponentModel.ListChangedEventHandler(_flex_AfterDataRefresh);
			_flex.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler( _flex_AfterRowColChange);
			_flex.CodeHelp += new Dass.FlexGrid.CodeHelpEventHandler(_flex_CodeHelp);
			
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
				//m_lblTitle.Text = GetDataDictionaryItem(DataDictionaryTypes.PU, "P_PU_STS_REG");
				//m_lblTitle.Show();
				Application.DoEvents();
				//					
			
				// 콤보박스 첨가
				InitCombo();	
		
				_flex.Redraw = false;
				_flex.BindingStart();
				_flex.DataSource = ds_Ty1.Tables[1];				
				_flex.BindingEnd();
				_flex.Redraw = true;

				ToolBarSaveButtonEnabled = true;
				ToolBarSearchButtonEnabled = true;								
			
				this.Enabled = true;	
				
				tb_DT_IO.Focus();
				tb_DT_IO.Select();
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

		#region >> 브라우져 버튼 이벤트

		// 브라우저의 조회 버턴이 클릭될때 처리 부분
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			
			this.Search_Click();
		}

		
		// 브라우저의 삭제 버턴이 클릭될때 처리 부분
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			
			this.Del_Click();
			
		}
		
		
		// 브라우저의 저장 버턴이 클릭될때 처리 부분
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if(Save_Click() !=false)
				{
					this.ToolBarAddButtonEnabled = true;
					this.ToolBarDeleteButtonEnabled = true;					
					m_page_state = "Modified";
					this.ShowMessage("IK1_001");

					gdt_Delete.Clear();
					ds_StsL.AcceptChanges();
					SetButtonState(false);
                    
				}
				//		this.ShowMessageBox(1);
				//	this.ToolBarSaveButtonEnabled = false;
			}
			catch(Exception ex)
			{
				ShowMessageBox(109, "[OnToolBarSaveButtonClicked] " + ex.Message);
			}		
			finally
			{							
				Cursor.Current = Cursors.Default;
			}
		}
		
		
		// 브라우저의 추가 버턴이 클릭될때 처리 부분
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{		
			this.Add_Click();
		}	
		
		
		// 브라우저의 닫기 버턴이 클릭될때 처리 부분
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if(!DoContinue())
					return false;

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

		#region >> 조회 함수
		/// <summary>
		/// 조회버튼 클릭시 처리 함수
		/// </summary>
		public void Search_Click()
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				

				if(!CheckFieldHeadDetail())
				{
					return;
				}

				tb_DT_IO.Focus();

				pur.P_PU_STS_SUB m_dlg = new pur.P_PU_STS_SUB(this.MainFrameInterface);
			
				// 값 이전 시킴
				m_dlg.SetDataFill(cb_cd_plant.SelectedValue.ToString(),
								cb_cd_plant.Text, 
								tb_cd_qtio.CodeValue.ToString(),tb_cd_qtio.CodeName,
								tb_gl_sl.CodeValue.ToString(),tb_gl_sl.CodeName,
								tb_no_emp.CodeValue.ToString(),tb_no_emp.CodeName);

				Cursor.Current = Cursors.Default;

				if( m_dlg.ShowDialog(this) == DialogResult.OK)
				{
					Cursor.Current = Cursors.WaitCursor;

					//SP_PU_STS_SELECT
					object[] m_obj = new object[2];
					m_obj[0] = m_dlg.m_NO_IO;
					m_obj[1] = this.LoginInfo.CompanyCode;

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();			
					ResultData result = (ResultData)this.FillDataSet("UP_PU_STS_SELECT", m_obj);
					DataSet ldt_ptp = (DataSet)result.DataValue;

//					if(ds_StsH ==null)
//						ds_StsH = new DataSet();
//					object[] args = {this.MainFrameInterface.LoginInfo.CompanyCode,m_dlg.m_NO_IO, m_dlg.m_CD_PLANT};
//					ds_StsH = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurStockControl_NTX", "pur.CC_PU_STS_NTX", "CC_PU_STS_NTX.rem", "SelectMMQTIO", args));
//			
//					dv_StsH = new DataView(ds_StsH.Tables[0]);
//
//					if(ds_StsL == null)
//						ds_StsL = new DataSet();
//					object[] args1 = {this.MainFrameInterface.LoginInfo.CompanyCode,m_dlg.m_NO_IO, m_dlg.m_CD_PLANT};
//					ds_StsL = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurStockControl_NTX", "pur.CC_PU_STS_NTX", "CC_PU_STS_NTX.rem", "SelectPtpsub2", args1));
//					dv_StsL = new DataView(ds_StsL.Tables[0]);


					tb_no_io.Text = ldt_ptp.Tables[0].Rows[0]["NO_IO"].ToString();

					tb_DT_IO.Text = ldt_ptp.Tables[0].Rows[0]["DT_IO"].ToString();

					tb_cd_qtio.CodeName = ldt_ptp.Tables[0].Rows[0]["NM_QTIOTP"].ToString();
					tb_cd_qtio.CodeValue =  ldt_ptp.Tables[0].Rows[0]["CD_QTIOTP"].ToString();	

					tb_gr_sl.CodeName = ldt_ptp.Tables[0].Rows[0]["IN_SL"].ToString();
					tb_gr_sl.CodeValue = ldt_ptp.Tables[0].Rows[0]["CD_SL"].ToString();
				
					tb_gl_sl.CodeName = ldt_ptp.Tables[0].Rows[0]["OUT_SL"].ToString();
					tb_gl_sl.CodeValue = ldt_ptp.Tables[0].Rows[0]["CD_SL_REF"].ToString();

					tb_no_emp.CodeName = ldt_ptp.Tables[0].Rows[0]["NM_KOR"].ToString();
					tb_no_emp.CodeValue = ldt_ptp.Tables[0].Rows[0]["NO_EMP"].ToString();	

					tb_dc.Text = ldt_ptp.Tables[0].Rows[0]["DC_RMK"].ToString();	


					// Detail 바인딩
					_flex.Redraw=false;
					_flex.BindingStart();
					_flex.DataSource = ldt_ptp.Tables[1].DefaultView;
					_flex.EmptyRowFilter();								// 처음에 아무것도 안 보이게
					_flex.BindingEnd();
					_flex.Redraw=true; 

				
				
					m_page_state = "Modified";

					// 버턴 상태
					this.ToolBarDeleteButtonEnabled = true;
					this.ToolBarAddButtonEnabled = true;
					
					b_Append.Enabled = false;
					b_Delete.Enabled = false;
					btn_INV_CON.Enabled = false;
					btn_REQ.Enabled = false;

					_flex.AllowEditing = true;

					if( ldt_ptp !=null)
					{
						if( ldt_ptp.Tables.Count > 1)
						{
								
							_flex.AllowEditing = true;
						}
					}

					gdt_Delete.Clear();		
					gdt_Delete = ldt_ptp.Tables[1].Clone();

					SetButtonState(false);

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

		
		/// <summary>
		/// 선택된 그리드에 해당하는 내용 Text Box Binding
		/// </summary>
		/// <param name="row"></param>
		private void SetTextBoxBinding()
		{
			try
			{				
				tb_no_io.Text = ds_StsH.Tables[0].Rows[0]["NO_IO"].ToString();

				tb_DT_IO.Text = ds_StsH.Tables[0].Rows[0]["DT_IO"].ToString();

				tb_cd_qtio.CodeName = ds_StsH.Tables[0].Rows[0]["NM_QTIOTP"].ToString();
				tb_cd_qtio.CodeValue =  ds_StsH.Tables[0].Rows[0]["CD_QTIOTP"].ToString();	

				tb_gr_sl.CodeName = ds_StsH.Tables[0].Rows[0]["IN_SL"].ToString();
				tb_gr_sl.CodeValue = ds_StsH.Tables[0].Rows[0]["CD_SL"].ToString();
				
				tb_gl_sl.CodeName = ds_StsH.Tables[0].Rows[0]["OUT_SL"].ToString();
				tb_gl_sl.CodeValue = ds_StsH.Tables[0].Rows[0]["CD_SL_REF"].ToString();

				tb_no_emp.CodeName = ds_StsH.Tables[0].Rows[0]["NM_KOR"].ToString();
				tb_no_emp.CodeValue = ds_StsH.Tables[0].Rows[0]["NO_EMP"].ToString();	

				tb_dc.Text = ds_StsH.Tables[0].Rows[0]["DC_RMK"].ToString();	
			
			}
			catch
			{
			}
		}


		
		#endregion

		#region -> 저장관련 함수
		
		#region	-> IsChanged
		
		private bool IsChanged(string gubun)
		{
			if(gubun == null)
				return _flex.IsDataChanged;
			if(gubun == "_flex")
				return _flex.IsDataChanged;
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
				if(IsChanged(null)) isSaved = Save_Click();
				
				return isSaved;
			}

			DialogResult result;

			if(isExit)
			{
				result = result = this.ShowMessage("QY3_002");
				if(result == DialogResult.No)
					return true;
				if(result == DialogResult.Cancel)
					return false;
			}
			else
			{				
				result = this.ShowMessage("QY2_001"); 
				if(result == DialogResult.No)
					return true;
			}

			Application.DoEvents();		// 대화상자 즉시 사라지게

			// "예"를 선택한 경우
			if(IsChanged(null)) isSaved = Save_Click();

			return isSaved;
		}

		
		#endregion

		#region >> InDataHeadValue(), Save_Click()
		/// <summary>
		/// 헤더 테이블 생성
		/// </summary>
		private void InDataHeadValue()
		{
			DataRow newrow;

			ds_Ty1.Tables[0].Clear();

			newrow = ds_Ty1.Tables[0].NewRow();
			newrow["NO_IO"] = tb_no_io.Text;
			newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;			
			ds_Ty1.Tables[0].Rows.Add(newrow);
						
			if(m_page_state == "Modified" )
			{		
				ds_Ty1.Tables[0].AcceptChanges();				
			}		
		
			ds_Ty1.Tables[0].BeginInit();
			DataRow ldr_row = ds_Ty1.Tables[0].Rows[0];
			
			ldr_row["CD_PLANT"] = cb_cd_plant.SelectedValue.ToString();					//공장
			ldr_row["CD_PARTNER"] = "";													//거래처

			ldr_row["CD_SL_OUT"] = 	tb_gl_sl.CodeValue.ToString();							//출고창고
			ldr_row["CD_SL_IN"] = tb_gr_sl.CodeValue.ToString();								//입고창고

			ldr_row["FG_TRANS"] = gfg_trans;											//거래구분
            ldr_row["CD_QTIOTP"] = sCD_QTIOTP;
			ldr_row["YN_RETURN"] ="N";													//반품유무		
			ldr_row["DT_IO"] =  tb_DT_IO.Text;							//이동일
			ldr_row["GI_PARTNER"] = "";													//납품처
			ldr_row["CD_DEPT"] = gs_cddept;												//부서				
			ldr_row["NO_EMP"] = tb_no_emp.CodeValue.ToString();								//담당자			
			ldr_row["DC_RMK"] = tb_dc.Text;												//비고

			ldr_row["CD_QTIOTP"] = tb_cd_qtio.CodeValue;		
			ldr_row["FG_PS"] = gs_tpqtio;		
			ldr_row["YN_AM"] = gyn_am;		
			ldr_row["YN_PURCHASE"] = gyn_purchase;				           

			ds_Ty1.Tables[0].EndInit();			
		}

		#region -> Check

		private bool Check()
		{
			//			int row;
			//			string colName;
			// 필요없는 행 삭제 : 리턴값이 False 이면 삭제후에 변경된 내용이 없다는 뜻
			if(!_flex.CheckView_DeleteIfNull(new string[] {"CD_ITEM"},"OR"))
			{
				// 변경된 내용이 없습니다.
				this.ShowMessage("IK1_013");
				return false;
			}

			//			// 필수입력항목 체크
			//			if(_flex.CheckView_HasNull(new string[] {"CD_ITEM", "DT_LIMIT"}, out row, out colName, "OR"))
			//			{
			//				this.ShowMessage("WK1_004", GetDDItem(colName));
			//				_flex.Select(row, colName);
			//				_flex.Focus();
			//				return false;
			//			}			
			return true;
		}

		#endregion


		/// <summary>
		/// 저장 함수
		/// </summary>
		private bool Save_Click()
		{
			try
			{
				if( !CheckSL())
				{
					return false;
				}
				if(!Check())		// 널값체크 및 중복값체크 등에서 문제가 발생한 경우
					return false;

				
				// 발주등록 필드 체크
				if( !CheckFieldHead())//CheckSL
				{
					return false;
				}
				//발주내역등록 필드체크
				if( !CheckFieldLine())
				{
					return false;
				}
				
				tb_no_io.Focus();
					
				object[] ls_result = new object[2];
				ls_result[0] = 0;
				ls_result[1] ="";

				DataTable ldt_chg = _flex.DataTable.GetChanges(DataRowState.Added);		


				if(ldt_chg == null || ldt_chg.Rows.Count <= 0)
				{
					this.ShowMessage("MA_M000017");
					return false;
					
				}

			
				InDataHeadValue();

				
				if(ldt_chg !=null && ldt_chg.Rows.Count >0)
				{
					string	no_seq = "";
					
					no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode,"PU","12",ds_Ty1.Tables[0].Rows[0]["DT_IO"].ToString().Substring(0,6));
									
					SpInfoCollection sic = new SpInfoCollection();

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
				
					
					si.DataValue = ds_Ty1.Tables[0]; 					//저장할 데이터 테이블
					si.SpNameInsert = "SP_PU_MM_QTIOH_INSERT";			//Insert 프로시저명
				
					
					/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
					si.SpParamsInsert = new string[] { "NO_IO1","CD_COMPANY","CD_PLANT","CD_PARTNER", "FG_TRANS","YN_RETURN","DT_IO","GI_PARTNER","CD_DEPT","NO_EMP","DC_RMK","DTS_INSERT1","ID_INSERT1", "CD_QTIOTP"};
					
					/*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
					si.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_seq);
					si.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
					si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID);
				
					sic.Add(si);

					si = new Duzon.Common.Util.SpInfo();
					//	si.DataState = DataValueState.Added; 
					si.DataValue = ldt_chg; 					//저장할 데이터 테이블
					si.SpNameInsert = "UP_PU_STS_INSERT";			//Insert 프로시저명
					//si.spType = "Added";
                    										
					/*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/		
					si.SpParamsInsert = new string[] {  "YN_RETURN1","NO_IO1","NO_IOLINE","CD_COMPANY1","CD_PLANT1","CD_SL1","DT_IO1",
														"CD_QTIOTP1", "CD_ITEM",
														"QT_GOOD_INV","QT_REJECT_INV","NO_EMP1","GR_SL1","YN_AM1","NO_ISURCV","NO_ISURCVLINE","FLAG"};
					/*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
				
					si.SpParamsValues.Add(ActionState.Insert, "YN_RETURN1", ds_Ty1.Tables[0].Rows[0]["YN_RETURN"].ToString());
					si.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_seq);
					si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT1", ds_Ty1.Tables[0].Rows[0]["CD_PLANT"].ToString());
					si.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", ds_Ty1.Tables[0].Rows[0]["CD_COMPANY"].ToString());
					si.SpParamsValues.Add(ActionState.Insert, "CD_QTIOTP1", ds_Ty1.Tables[0].Rows[0]["CD_QTIOTP"].ToString());
					si.SpParamsValues.Add(ActionState.Insert, "DT_IO1", ds_Ty1.Tables[0].Rows[0]["DT_IO"].ToString());			
					si.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", tb_no_emp.CodeValue.ToString());				
					si.SpParamsValues.Add(ActionState.Insert, "CD_SL1", ds_Ty1.Tables[0].Rows[0]["CD_SL_OUT"].ToString());		
					si.SpParamsValues.Add(ActionState.Insert, "GR_SL1", ds_Ty1.Tables[0].Rows[0]["CD_SL_IN"].ToString());			
					si.SpParamsValues.Add(ActionState.Insert, "YN_AM1", ds_Ty1.Tables[0].Rows[0]["YN_AM"].ToString());			
					
					sic.Add(si);
						
					object obj = this.Save(sic);
					ResultData[] result = (ResultData[])obj;
					if(result[0].Result && result[1].Result)
					{
						tb_no_io.Text = no_seq ;										
						tb_no_io.Enabled = false;
						
						b_Append.Enabled = false;
						b_Delete.Enabled = false;
						btn_INV_CON.Enabled = false;
						btn_REQ.Enabled = false;
						_flex.DataTable.AcceptChanges();	
	
						
						this.m_page_state = "Modified";	
	
						this.ToolBarDeleteButtonEnabled = true;
						return true;						

					}
				}


//				object[] m_obj = new object[3];
//
//				m_obj[0] = ds_Ty1.Tables[0];
//				m_obj[1] = ldt_chg;				
//				m_obj[2] = this.MainFrameInterface.LoginInfo.UserID;
//
//				if(ldt_chg !=null && ldt_chg.Rows.Count >0)
//				{
//					ls_result = (object[])(this.MainFrameInterface.InvokeRemoteMethod("PurStockControl", "pur.CC_PU_STS", "CC_PU_STS.rem", "SaveSTS", m_obj));
//
//					if((int)ls_result[0] >=0)
//					{  
//						_flex.DataTable.AcceptChanges();
//							
//						tb_no_io.Text = ls_result[1].ToString();
//						tb_no_io.Enabled = false;
//
//						b_Append.Enabled = false;
//						b_Delete.Enabled = false;
//						btn_INV_CON.Enabled = false;
//
//					//	this.ShowMessageBox(1);		
//						this.m_page_state = "Modified";		
//						return true;
//				
//					}
//					return false;
//					
//				}
				else
				{
					this.ShowMessage("MA_M000017");					
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

		#region >>  추가 함수

		/// <summary>
		/// 추가 함수
		/// </summary>
		public void Add_Click()
		{	
			try
			{	
				dv_StsL.Table.Clear();
				//m_lblTitle.Focus();

				InitializeControlDataValue();
				
				Cursor.Current = Cursors.WaitCursor;
				
				// 변경된 내용 조사 후 변경된 내용 있을 경우 저장
//				if(!CheckSearchOption(0))
//				{
//					return;
//				}

				tb_DT_IO.Text = MainFrameInterface.GetStringToday;

				m_page_state="Added";
				
//				b_Append.Enabled = true;
//				b_Delete.Enabled = false;

				InitializeControlDataValue();
//				this.ToolBarDeleteButtonEnabled = false;
//				this.ToolBarSaveButtonEnabled = false;

				tb_DT_IO.Select();
			
			}
			catch(Exception ex)
			{
				ShowMessageBox(109, "[OnToolBarAddButtonClicked] " + ex.Message);
			}	
			finally
			{							
				Cursor.Current = Cursors.Default;
			}

			
		}


		#endregion

		#region >>  삭제 함수

		/// <summary>
		/// 삭제 함수
		/// </summary>
		public void Del_Click()
		{
			try
			{		
				//int li_result = 0;
			
				DialogResult result = this.ShowMessage("QY2_003");					
				if(result == DialogResult.Yes)
				{							
					tb_DT_IO.Focus();

					if( m_page_state == "Modified" && tb_no_io.Text !="")
					{
					
						object[] m_obj = new object[2];
						m_obj[0] = tb_no_io.Text; 
						m_obj[1] = MainFrameInterface.LoginInfo.CompanyCode;
				
					
						ResultData ret = (ResultData)this.ExecSp("SP_PU_GRM_DELETE", m_obj);
						if(ret.Result)
						{													
							this.ShowMessage("IK1_002");
						}
						else
						{
							this.ShowMessage("EK1_002");							
							return; 
						}



//						li_result = (int)(this.MainFrameInterface.InvokeRemoteMethod("PurQtioControl", "pur.CC_MM_QTIO", "CC_MM_QTIO.rem", "DeleteMMQTIOH", m_obj));
//						if( li_result >=0)
//						{
//							this.ShowMessage("IK1_002");
//						}
//						else
//						{
//							this.ShowMessage("EK1_002");							
//							return; 
//						}
					}
				}
				else
					return ;

				m_page_state="Added";
					
				InitializeControlDataValue();						
					
				this.ToolBarSearchButtonEnabled = true;
				this.ToolBarAddButtonEnabled = true;
				this.ToolBarDeleteButtonEnabled = true;
				this.ToolBarSaveButtonEnabled = true; 
					
//				b_Append.Enabled = true;
//				b_Delete.Enabled = false;

			}
			catch(coDbException ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			catch//(Exception ex)
			{
				//this.ShowErrorMessage(ex, this.PageName);										
			}
		}

		#endregion

		#endregion

		#region >> 데이터 체크 함수
		
		
		/// <summary>
		/// 헤드부분의 필드중 세부적인 내용체크 -- 도움창 호출시 체크 하는 내역들
		/// </summary>
		/// <returns></returns>
		private bool CheckFieldHeadDetail()
		{
			try
			{
				if(cb_cd_plant.SelectedIndex.ToString() =="-1")
				{					
					ShowMessage("WK1_004", lb_nm_plant.Text);
					cb_cd_plant.Focus();
					return false;
				}				
			}
			catch//(Exception ex)
			{
				//ShowMessage(ex.Message);
			}
			return true;
		}



		/// <summary>
		/// 헤드 부분의 필드 체크
		/// </summary>
		/// <returns></returns>
		private bool CheckSL()
		{	
			if(tb_gl_sl.CodeValue.ToString() !=string.Empty && tb_gr_sl.CodeValue.ToString()!=string.Empty)
			{
				if(tb_gl_sl.CodeValue.ToString() == tb_gr_sl.CodeValue.ToString())
				{					
					tb_gr_sl.Focus();				
					ShowMessage("PU_M000058");				
					return false;
				}
			}
			return true;			
		}
		
	
		/// <summary>
		/// 헤드 부분의 필드 체크
		/// </summary>
		/// <returns></returns>
		private bool CheckFieldHead()
		{
			try
			{				
				if(this.tb_DT_IO.Text =="" )
				{
					MainFrameInterface.ShowMessage("WK1_004", lb_dt_trans.Text);
					tb_DT_IO.Focus();
					return false;
				}

				if(cb_cd_plant.SelectedIndex.ToString() =="-1" )
				{	
					MainFrameInterface.ShowMessage("WK1_004", lb_nm_plant.Text);
					cb_cd_plant.Focus();
					return false;
				}
				if(tb_cd_qtio.CodeValue.ToString() =="" )
				{					
					MainFrameInterface.ShowMessage("WK1_004", lb_cd_qtio.Text);
					tb_cd_qtio.Focus();
					return false;
				}
				if(tb_gl_sl.CodeValue.ToString() =="" )
				{					
					MainFrameInterface.ShowMessage("WK1_004", lb_gl_sl.Text);
					tb_gl_sl.Focus();
					return false;
				}
				if(tb_gr_sl.CodeValue.ToString() =="" )
				{					
					MainFrameInterface.ShowMessage("WK1_004", lb_gr_sl.Text);
					tb_gr_sl.Focus();
					return false;
				}

				
				if(tb_no_emp.CodeValue.ToString() =="")
				{					
					MainFrameInterface.ShowMessage("WK1_004", lb_no_emp.Text);	
					tb_no_emp.Focus();
					return false;
				}
				
			}
			catch(Exception ex)
			{
				Duzon.Common.Controls.MessageBoxEx.Show(ex.Message);
			}
			return true;
		
		}		

		
		/// <summary>
		/// 라인 부분 체크 함수-- 그리드 부분의 값들을 체크
		/// </summary>
		/// <returns></returns>
		private bool CheckFieldLine()
		{
			try
			{
				if(_flex[_flex.Row, "CD_ITEM"].ToString() =="" || _flex[_flex.Row, "CD_ITEM"] ==null)
				{									
					MainFrameInterface.ShowMessage("WK1_004", GetDDItem("CD_ITEM"));
					_flex.Select(_flex.Row,2);	
					return false;
				}
				
				if( _flex[_flex.Row,"QT_GOOD_INV"].ToString() =="" || _flex[_flex.Row,"QT_GOOD_INV"] ==null)
				{									
					MainFrameInterface.ShowMessage("WK1_004", GetDDItem("QT_GOOD"));
					_flex.Select(_flex.Row,7);	
					return false;
				}		
	
				if( _flex[_flex.Row,"QT_REJECT_INV"].ToString() =="" || _flex[_flex.Row,"QT_REJECT_INV"] ==null)
				{								
					MainFrameInterface.ShowMessage("WK1_004", GetDDItem("QT_INFERIOR"));
					_flex.Select(_flex.Row,8);	
					return false;
				}		
			}
			catch
			{
			}
			return true;

			
		}
		
		#endregion

		#region ♣ 기타 이벤트

		#region >> 라인 추가, 삭제
		private void b_Append_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!b_Append.Enabled)
					return;

				if(!CheckSL())
				{
					return;
				}
				// 헤드 필드 체크
				if( !CheckFieldHead())
				{
					return;
				}
				
				_flex.Rows.Add();
				_flex.Row = _flex.Rows.Count - 1;				
						
				_flex[_flex.Row,"S"] = "0";	
				_flex[_flex.Row,"CD_ITEM"] = "";
				_flex[_flex.Row,"NO_IOLINE"] = GetLineNum();	
				_flex[_flex.Row,"QT_GOOD_INV"] = 0;
				_flex[_flex.Row,"QT_REJECT_INV"] = 0;
										
				_flex.AddFinished();				
				_flex.Col = _flex.Cols.Fixed;
				
				this.ToolBarSaveButtonEnabled = true;
				
				_flex.Focus();
				this.b_Delete.Enabled = true;
				this.btn_INV_CON.Enabled = true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		/// <summary>
		/// 최대항번 구하는 함수
		/// </summary>
		private int GetLineNum()
		{					
			int li_line =1;
			try
			{			
				if(System.Convert.ToInt32(_flex.DataTable.Compute("MAX(NO_IOLINE)","").ToString()) == 0)
				{
					return 1;
				}
				else
				{
					li_line = System.Convert.ToInt32(_flex.DataTable.Compute("MAX(NO_IOLINE)","").ToString());
					li_line += 2;
				}
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
				return -1;
			}
			return li_line;
		}
		

		private void b_Delete_Click(object sender, System.EventArgs e)
		{
			try
			{	
				bool m_okstate =false;
				for(int r = _flex.Rows.Count-1;r >= _flex.Rows.Fixed; r--)
				{					
					if(_flex.DataView.Count > 0)                      //데이타행 삭제.
					{	
						if(_flex[r,"S"].ToString() == "1")
						{							
//							DataRow row = gdt_Delete.NewRow();							
//							row["NO_IOLINE"]= _flex[r,"NO_IOLINE"].ToString();
//							gdt_Delete.Rows.Add(row);

							_flex.Rows.Remove(r);	
						}					
						m_okstate = true;
					}				
					else
					{					
						ShowMessage("PU_M000112");
						
					}					
				}
				
				if( !m_okstate )
				{
					ShowMessage("PU_M000018");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}	

		}
		#endregion 

		#region >> 재고확인
		
		private void btn_INV_CON_Click(object sender, System.EventArgs e)
		{
			try
			{
				//m_lblTitle.Focus();
				
				pur.P_PU_OHSLINV_SUB m_dlg = new pur.P_PU_OHSLINV_SUB(this.MainFrameInterface,cb_cd_plant.SelectedValue.ToString(),  _flex.DataTable) ;
				if( m_dlg.ShowDialog(this) == DialogResult.OK)
				{
					//출고창고
					tb_gl_sl.CodeName = m_dlg.m_SelecedRow["NM_SL"].ToString();
					tb_gl_sl.CodeValue = m_dlg.m_SelecedRow["CD_SL"].ToString();
					tb_gl_sl.Focus();

					//입고창고

				}
			}
			catch
			{
			}
		}
		#endregion
			
		#region >> 날짜 에 관련된 함수 및 이벤트

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

		#endregion			
	
		#region ♣ 그리드 이벤트		
		
		#region -> _flex_AfterDataRefresh

		private void _flex_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			if(IsChanged(null))
				SetToolBarButtonState(true,true,true,true,false);
			else
				SetToolBarButtonState(true,true,true,false, false);

			if(!_flex.HasNormalRow)
				ToolBarDeleteButtonEnabled = false;	

		}

		#endregion

		#region -> _flex_AfterRowColChange
	
		private void _flex_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{					
				
				if( _flex.DataView[ _flex.DataIndex( _flex.Row)].Row.RowState.ToString() != "Added" )
				{
					_flex.AllowEditing = false;
				}	
				else
				{
					_flex.AllowEditing = true;
				}
			}
			catch
			{
			}
		}
		#endregion

		#region -> _flex_CodeHelp
	
		private void _flex_CodeHelp(object sender, Dass.FlexGrid.CodeHelpEventArgs e)
		{						
			try
			{
				if( _flex.AllowEditing )				
				{				

					HelpReturn helpReturn = null;
					HelpParam param = null;
					
					switch(_flex.Cols[e.Col].Name)
					{
						case "CD_ITEM":
							if( e.Source == CodeHelpEnum.CodeSearch && e.EditValue =="" )
							{
								_flex[_flex.Row, "CD_ITEM"]= "";
								_flex[_flex.Row, "NM_ITEM"]= "";
								_flex[_flex.Row, "STND_ITEM"]= "";
								_flex[_flex.Row, "UNIT_IM"]= "";		
								_flex[_flex.Row, "NO_LOT"] = "";	
									
							}
							else
							{
								param = new Duzon.Common.Forms.Help.HelpParam(
									Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB, this.MainFrameInterface);
								param.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
								param.P09_CD_PLANT   = cb_cd_plant.SelectedValue.ToString();	
								param.ResultMode = ResultMode.SlowMode;
								if(e.Source == CodeHelpEnum.CodeSearch)
									param.P92_DETAIL_SEARCH_CODE = e.EditValue;

								if(e.Source == CodeHelpEnum.CodeSearch)		helpReturn = (HelpReturn)this.CodeSearch(param);
								else										helpReturn = (HelpReturn)this.ShowHelp(param);						

								if(helpReturn.DialogResult == DialogResult.OK)
								{
									_flex[_flex.Row, "CD_ITEM"] = helpReturn.Rows[0]["CD_ITEM"].ToString();
									_flex[_flex.Row, "NM_ITEM"]= helpReturn.Rows[0]["NM_ITEM"].ToString();		
									_flex[_flex.Row, "STND_ITEM"]= helpReturn.Rows[0]["STND_ITEM"].ToString();		
									_flex[_flex.Row, "UNIT_IM"]= helpReturn.Rows[0]["UNIT_IM"].ToString();	
									if( helpReturn.Rows[0]["FG_LOTNO"].ToString() == "002") // LOT 여부 								
										_flex[_flex.Row, "NO_LOT"] = "YES";								
									else
										_flex[_flex.Row, "NO_LOT"] = "NO";
									
								}
							
							}
							break;

						case "NM_SL":							
							if( e.Source == CodeHelpEnum.CodeSearch && e.EditValue =="" )
							{
								_flex[_flex.Row, "CD_SL"]		= "";
								_flex[_flex.Row, "NM_SL"]		= "";
							}
							else
							{
								param = new Duzon.Common.Forms.Help.HelpParam(
									Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, this.MainFrameInterface);
								param.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
								param.P09_CD_PLANT   = cb_cd_plant.SelectedValue.ToString();
								param.ResultMode = ResultMode.FastMode;
								if(e.Source == CodeHelpEnum.CodeSearch)
									param.P92_DETAIL_SEARCH_CODE = e.EditValue;

								if(e.Source == CodeHelpEnum.CodeSearch)		helpReturn = (HelpReturn)this.CodeSearch(param);
								else										helpReturn = (HelpReturn)this.ShowHelp(param);						

								if(helpReturn.DialogResult == System.Windows.Forms.DialogResult.OK)
								{
									_flex[_flex.Row, "CD_SL"]		= helpReturn.CodeValue;
									_flex[_flex.Row, "NM_SL"]		= helpReturn.CodeName;

								}	
							}
							break;						
					}
				}
			}
			catch(Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	

		#endregion

		#endregion		
		
		#region -> InitializeControlDataValue
		/// <summary>
		/// 모든 컨트롤들의 값을 초기화 시킴
		/// </summary>
		private void InitializeControlDataValue()
		{				
			tb_DT_IO.Text=this.MainFrameInterface.GetStringToday;
			
			tb_no_io.Text ="";

			tb_cd_qtio.CodeName ="";
			tb_cd_qtio.CodeValue ="";

			tb_gl_sl.CodeName="";
			tb_gl_sl.CodeValue="";

			tb_gr_sl.CodeName="";
			tb_gr_sl.CodeValue="";

				
			//_cddept = this.LoginInfo.DeptCode; 
			tb_no_emp.CodeName= this.LoginInfo.EmployeeName;
			tb_no_emp.CodeValue=this.LoginInfo.EmployeeNo;
			
			tb_dc.Text="";		

		
			SetButtonState(true);

			gs_tpqtio = "";

		
			gs_cddept= "";
			gs_tpqtio= "";
			gfg_trans= "";
			gyn_purchase= "";
			gfg_tppurchase= "";		
			gyn_am= "";
            sCD_QTIOTP = string.Empty;
			

			ds_StsL.Clear();
			gdt_Delete.Clear();	

			//----명수
			//_flex 데이타 다시 바인딩
			_flex.Redraw = false;
			_flex.BindingStart();
			_flex.DataSource = ds_Ty1.Tables[1];				
			_flex.BindingEnd();
			_flex.Redraw = true;

			try
			{
				_flex.DataTable.Clear();
			}
			catch
			{
			}			
		}


		#endregion

		#region -> SetButtonState
		private void SetButtonState(bool pb_state)
		{

			tb_DT_IO.Enabled = pb_state;;
			
			tb_no_io.Enabled = pb_state;

			tb_cd_qtio.Enabled = pb_state;
			tb_cd_qtio.Enabled = pb_state;
			cb_cd_plant.Enabled = pb_state;

			tb_gl_sl.Enabled = pb_state;
			tb_gl_sl.Enabled = pb_state;
			
			tb_gr_sl.Enabled = pb_state;
			tb_gr_sl.Enabled = pb_state;
			
				
			//_cddept = this.LoginInfo.DeptCode; 
			tb_no_emp.Enabled = pb_state;
			tb_no_emp.Enabled = pb_state;
			
			b_Append.Enabled = pb_state;
			b_Delete.Enabled = pb_state;
			btn_INV_CON.Enabled = pb_state;
			btn_REQ.Enabled = pb_state;
			
		}
		#endregion

		#region -> BpControl Event

		private void OnBpCodeTextBox_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			try
			{			
				if(e.DialogResult == DialogResult.OK)
				{
					System.Data.DataRow[] rows = e.HelpReturn.Rows;
					switch(e.HelpID)
					{									
						case Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB:
							gs_cddept =  rows[0]["CD_DEPT"].ToString();							
							break;				
						case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:	
							gfg_trans = e.HelpReturn.Rows[0]["FG_TRANS"].ToString();				//거래구분코드
							gyn_purchase = e.HelpReturn.Rows[0]["YN_PURCHASE"].ToString();		//매입유무					
							gfg_tppurchase = e.HelpReturn.Rows[0]["FG_TPPURCHASE"].ToString();	//매입형태
							gs_tpqtio = e.HelpReturn.Rows[0]["TP_QTIO"].ToString();				//입출고구분
							gyn_am = e.HelpReturn.Rows[0]["YN_AM"].ToString();
                            sCD_QTIOTP = e.HelpReturn.Rows[0]["CD_QTIOTP"].ToString();
							break;
						default:
							break;
					}
				}
			}
			catch
			{
			}
		}
	
	
		private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			Control m_ctr = (Control)sender;
			switch(e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:		// 발주유형 도움창
					if( cb_cd_plant.SelectedValue.ToString() == "")
					{
						this.ShowMessage("PU_M000070");
						cb_cd_plant.Focus();
						e.QueryCancel = true;
						return ;
					}
					e.HelpParam.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();					
					break;					
				case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:		// 발주유형 도움창	
					e.HelpParam.P61_CODE1 = "022|";									
					break;	
			}
		}

	
		
		#endregion

		#region -> 요청적용 도움창

		private void btn_REQ_Click(object sender, System.EventArgs e)
		{
			try
			{
				object dlg = this.MainFrameInterface.LoadHelpWindow("P_QU_AS_FG_REQ_SUB02", new object[] {this.MainFrameInterface, this.cb_cd_plant.SelectedValue.ToString()});
				if(((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
				{
					_flex.DataTable.Clear();
					if(dlg is IHelpWindow)
					{

						object[] _sub = ((IHelpWindow)dlg).ReturnValues;

						DataRow[] _drHeader = (DataRow[])_sub[0];
						DataRow[] _drLine = (DataRow[])_sub[1];

						if(_drHeader.Length > 0)
						{
							tb_gl_sl.CodeValue = _drHeader[0]["CD_SL_FROM"].ToString();
							tb_gl_sl.CodeName = _drHeader[0]["NM_SL_FROM"].ToString();

							tb_gr_sl.CodeValue = _drHeader[0]["CD_SL_TO"].ToString();
							tb_gr_sl.CodeName = _drHeader[0]["NM_SL_TO"].ToString();
				
							tb_no_emp.CodeValue = _drHeader[0]["NO_EMP"].ToString();
							tb_no_emp.CodeName = _drHeader[0]["NM_EMP"].ToString();	

							tb_dc.Text = _drHeader[0]["DC_REMARK"].ToString();	
						}

						if(_drLine.Length > 0)
						{
							_flex.Redraw = false;
							for(int i = 0; i < _drLine.Length; i++)
							{
								_flex.Rows.Add();
								_flex.Row = _flex.Rows.Count - 1;				
						
								_flex[_flex.Row,"S"] = "0";	
								_flex[_flex.Row,"CD_ITEM"] = _drLine[i]["CD_ITEM"].ToString();
								_flex[_flex.Row,"NM_ITEM"] = _drLine[i]["NM_ITEM"].ToString();
								_flex[_flex.Row,"STND_ITEM"] = _drLine[i]["STND_ITEM"].ToString();
								_flex[_flex.Row,"UNIT_IM"] = _drLine[i]["UNIT_IM"].ToString();
								
								if( _drLine[i]["FG_LOTNO"].ToString() == "002") // LOT 여부 								
									_flex[_flex.Row, "NO_LOT"] = "YES";								
								else
									_flex[_flex.Row, "NO_LOT"] = "NO";

								_flex[_flex.Row,"NO_IOLINE"] = GetLineNum();	
								_flex[_flex.Row,"QT_GOOD_INV"] = System.Convert.ToDecimal(_drLine[i]["QT"].ToString());
								_flex[_flex.Row,"QT_REJECT_INV"] = 0;
								_flex[_flex.Row,"NO_ISURCV"] = _drLine[i]["NO_REQ"].ToString();
								_flex[_flex.Row,"NO_ISURCVLINE"] = _drLine[i]["NO_LINE"].ToString();
								_flex[_flex.Row,"FLAG"] = _drLine[i]["FLAG"].ToString();
										
								_flex.AddFinished();		
							}

							_flex.Redraw = true;

							this._flex.Focus();
						}
					}
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
	}
}
