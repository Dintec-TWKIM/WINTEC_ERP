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
	/// 개발자 : 김대영
	/// 페이지명 : 재고평가 ( 총평균법 )
	/// 특징 : 프로시저에서 처리함
	/// 작성자 : 김대영, 김대영
	/// 작성일 : 
	/// 수정일 : 2004-03-12
	/// </summary>
	public class P_PU_EVAL_REG_OLD : Duzon.Common.Forms.PageBase
	{

		#region ♣ 멤버필드
		
		#region -> 멤버필드(일반)

        private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.LabelExt label8;
		private System.ComponentModel.IContainer components;
		private Duzon.Common.Controls.RoundedButton btn_INV_EVAL;

		private Duzon.Common.Controls.LabelExt lb_DY_EVAL;
		private Duzon.Common.Controls.LabelExt lb_DT_EVAL;
		private Duzon.Common.Controls.LabelExt lb_NM_PLANT;
        private Duzon.Common.Controls.LabelExt lb_NO_EMP;
		private Duzon.Common.Controls.DropDownComboBox cbo_CD_PLANT;
		private Duzon.Common.Controls.PanelExt m_pnlGrid;
		private Duzon.Common.Controls.DatePicker tb_DT_TODAY;
	

		#endregion

		#region -> 멤버필드(주요)	
		//페이팅관련
		private bool _isPainted = false;
				
		//그리드
		private Dass.FlexGrid.FlexGrid _flex;	

		private string _cddept;
		private Duzon.Common.Controls.DateTimePickerExt dTP_From;
		private Duzon.Common.Controls.DateTimePickerExt sss;
		private Duzon.Common.Controls.DateTimePickerExt dTP_To;
		private Duzon.Common.Controls.DateTimePickerExt dTP_To1;
		private Duzon.Common.BpControls.BpCodeTextBox tb_NO_EMP;
        private TableLayoutPanel tableLayoutPanel1;

		private string gstb_noemp;
		#endregion
		
		#endregion

		#region ♣ 생성자/소멸자
	
		#region -> 생성자
		public P_PU_EVAL_REG_OLD()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_EVAL_REG));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.tb_NO_EMP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lb_DY_EVAL = new Duzon.Common.Controls.LabelExt();
            this.lb_DT_EVAL = new Duzon.Common.Controls.LabelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.lb_NM_PLANT = new Duzon.Common.Controls.LabelExt();
            this.lb_NO_EMP = new Duzon.Common.Controls.LabelExt();
            this.label8 = new Duzon.Common.Controls.LabelExt();
            this.dTP_From = new Duzon.Common.Controls.DateTimePickerExt();
            this.dTP_To = new Duzon.Common.Controls.DateTimePickerExt();
            this.cbo_CD_PLANT = new Duzon.Common.Controls.DropDownComboBox();
            this.tb_DT_TODAY = new Duzon.Common.Controls.DatePicker();
            this.btn_INV_EVAL = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlGrid = new Duzon.Common.Controls.PanelExt();
            this.sss = new Duzon.Common.Controls.DateTimePickerExt();
            this.dTP_To1 = new Duzon.Common.Controls.DateTimePickerExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_TODAY)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tb_NO_EMP);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.dTP_From);
            this.panel4.Controls.Add(this.dTP_To);
            this.panel4.Controls.Add(this.cbo_CD_PLANT);
            this.panel4.Controls.Add(this.tb_DT_TODAY);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 53);
            this.panel4.TabIndex = 0;
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
            this.tb_NO_EMP.Location = new System.Drawing.Point(89, 29);
            this.tb_NO_EMP.Name = "tb_NO_EMP";
            this.tb_NO_EMP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NO_EMP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NO_EMP.SearchCode = true;
            this.tb_NO_EMP.SelectCount = 0;
            this.tb_NO_EMP.SetDefaultValue = false;
            this.tb_NO_EMP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NO_EMP.Size = new System.Drawing.Size(167, 21);
            this.tb_NO_EMP.TabIndex = 3;
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
            this.panel8.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lb_DY_EVAL);
            this.panel6.Controls.Add(this.lb_DT_EVAL);
            this.panel6.Location = new System.Drawing.Point(390, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(90, 49);
            this.panel6.TabIndex = 1;
            // 
            // lb_DY_EVAL
            // 
            this.lb_DY_EVAL.Location = new System.Drawing.Point(3, 5);
            this.lb_DY_EVAL.Name = "lb_DY_EVAL";
            this.lb_DY_EVAL.Resizeble = true;
            this.lb_DY_EVAL.Size = new System.Drawing.Size(85, 18);
            this.lb_DY_EVAL.TabIndex = 2;
            this.lb_DY_EVAL.Text = "재고평가기간";
            this.lb_DY_EVAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_DT_EVAL
            // 
            this.lb_DT_EVAL.Location = new System.Drawing.Point(8, 28);
            this.lb_DT_EVAL.Name = "lb_DT_EVAL";
            this.lb_DT_EVAL.Resizeble = true;
            this.lb_DT_EVAL.Size = new System.Drawing.Size(80, 18);
            this.lb_DT_EVAL.TabIndex = 2;
            this.lb_DT_EVAL.Text = "평가일";
            this.lb_DT_EVAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lb_NM_PLANT);
            this.panel5.Controls.Add(this.lb_NO_EMP);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(85, 49);
            this.panel5.TabIndex = 0;
            // 
            // lb_NM_PLANT
            // 
            this.lb_NM_PLANT.Location = new System.Drawing.Point(3, 5);
            this.lb_NM_PLANT.Name = "lb_NM_PLANT";
            this.lb_NM_PLANT.Resizeble = true;
            this.lb_NM_PLANT.Size = new System.Drawing.Size(80, 18);
            this.lb_NM_PLANT.TabIndex = 2;
            this.lb_NM_PLANT.Text = "공장명";
            this.lb_NM_PLANT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NO_EMP
            // 
            this.lb_NO_EMP.Location = new System.Drawing.Point(8, 30);
            this.lb_NO_EMP.Name = "lb_NO_EMP";
            this.lb_NO_EMP.Resizeble = true;
            this.lb_NO_EMP.Size = new System.Drawing.Size(72, 18);
            this.lb_NO_EMP.TabIndex = 3;
            this.lb_NO_EMP.Text = "담당자";
            this.lb_NO_EMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(568, 6);
            this.label8.Name = "label8";
            this.label8.Resizeble = true;
            this.label8.Size = new System.Drawing.Size(14, 15);
            this.label8.TabIndex = 123;
            this.label8.Text = "~";
            // 
            // dTP_From
            // 
            this.dTP_From.CustomFormat = "yyyy/MM";
            this.dTP_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP_From.Location = new System.Drawing.Point(484, 3);
            this.dTP_From.Name = "dTP_From";
            this.dTP_From.ShowUpDown = true;
            this.dTP_From.Size = new System.Drawing.Size(80, 21);
            this.dTP_From.TabIndex = 1;
            this.dTP_From.UseKeyEnter = true;
            this.dTP_From.UseKeyF3 = true;
            this.dTP_From.Value = new System.DateTime(2004, 8, 31, 18, 39, 0, 859);
            this.dTP_From.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_CONTROL_KeyDown);
            // 
            // dTP_To
            // 
            this.dTP_To.CustomFormat = "yyyy/MM";
            this.dTP_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP_To.Location = new System.Drawing.Point(584, 3);
            this.dTP_To.Name = "dTP_To";
            this.dTP_To.ShowUpDown = true;
            this.dTP_To.Size = new System.Drawing.Size(80, 21);
            this.dTP_To.TabIndex = 2;
            this.dTP_To.UseKeyEnter = true;
            this.dTP_To.UseKeyF3 = true;
            this.dTP_To.Value = new System.DateTime(2004, 8, 31, 18, 39, 20, 890);
            this.dTP_To.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_CONTROL_KeyDown);
            // 
            // cbo_CD_PLANT
            // 
            this.cbo_CD_PLANT.AutoDropDown = true;
            this.cbo_CD_PLANT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_CD_PLANT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_PLANT.Location = new System.Drawing.Point(90, 3);
            this.cbo_CD_PLANT.Name = "cbo_CD_PLANT";
            this.cbo_CD_PLANT.ShowCheckBox = false;
            this.cbo_CD_PLANT.Size = new System.Drawing.Size(174, 20);
            this.cbo_CD_PLANT.TabIndex = 0;
            this.cbo_CD_PLANT.Tag = "FG_TRANS";
            this.cbo_CD_PLANT.UseKeyEnter = false;
            this.cbo_CD_PLANT.UseKeyF3 = false;
            this.cbo_CD_PLANT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_CONTROL_KeyDown);
            // 
            // tb_DT_TODAY
            // 
            this.tb_DT_TODAY.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_TODAY.DayColor = System.Drawing.Color.Black;
            this.tb_DT_TODAY.Enabled = false;
            this.tb_DT_TODAY.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_TODAY.Location = new System.Drawing.Point(484, 29);
            this.tb_DT_TODAY.Mask = "####/##/##";
            this.tb_DT_TODAY.MaskBackColor = System.Drawing.SystemColors.Window;
            this.tb_DT_TODAY.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_TODAY.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_TODAY.Modified = false;
            this.tb_DT_TODAY.Name = "tb_DT_TODAY";
            this.tb_DT_TODAY.PaddingCharacter = '_';
            this.tb_DT_TODAY.PassivePromptCharacter = '_';
            this.tb_DT_TODAY.PromptCharacter = '_';
            this.tb_DT_TODAY.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_TODAY.ShowToDay = false;
            this.tb_DT_TODAY.ShowTodayCircle = false;
            this.tb_DT_TODAY.ShowUpDown = false;
            this.tb_DT_TODAY.Size = new System.Drawing.Size(92, 21);
            this.tb_DT_TODAY.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_TODAY.TabIndex = 4;
            this.tb_DT_TODAY.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_TODAY.TitleForeColor = System.Drawing.Color.White;
            this.tb_DT_TODAY.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_TODAY.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_TODAY.UseKeyF3 = false;
            this.tb_DT_TODAY.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            // 
            // btn_INV_EVAL
            // 
            this.btn_INV_EVAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_INV_EVAL.BackColor = System.Drawing.Color.White;
            this.btn_INV_EVAL.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_INV_EVAL.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_INV_EVAL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_INV_EVAL.Location = new System.Drawing.Point(700, 62);
            this.btn_INV_EVAL.Name = "btn_INV_EVAL";
            this.btn_INV_EVAL.Size = new System.Drawing.Size(90, 24);
            this.btn_INV_EVAL.TabIndex = 3;
            this.btn_INV_EVAL.TabStop = false;
            this.btn_INV_EVAL.Text = "재고평가";
            this.btn_INV_EVAL.UseVisualStyleBackColor = false;
            this.btn_INV_EVAL.Click += new System.EventHandler(this.btn_INV_EVAL_Click);
            // 
            // m_pnlGrid
            // 
            this.m_pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid.Location = new System.Drawing.Point(3, 92);
            this.m_pnlGrid.Name = "m_pnlGrid";
            this.m_pnlGrid.Size = new System.Drawing.Size(787, 466);
            this.m_pnlGrid.TabIndex = 4;
            // 
            // sss
            // 
            this.sss.CustomFormat = "yyyy/MM";
            this.sss.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sss.Location = new System.Drawing.Point(348, 222);
            this.sss.Name = "sss";
            this.sss.ShowUpDown = true;
            this.sss.Size = new System.Drawing.Size(88, 21);
            this.sss.TabIndex = 126;
            this.sss.UseKeyEnter = true;
            this.sss.UseKeyF3 = true;
            // 
            // dTP_To1
            // 
            this.dTP_To1.Location = new System.Drawing.Point(348, 222);
            this.dTP_To1.Name = "dTP_To1";
            this.dTP_To1.ShowUpDown = true;
            this.dTP_To1.Size = new System.Drawing.Size(88, 21);
            this.dTP_To1.TabIndex = 0;
            this.dTP_To1.UseKeyEnter = true;
            this.dTP_To1.UseKeyF3 = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_INV_EVAL, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlGrid, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // P_PU_EVAL_REG
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_PU_EVAL_REG";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_TODAY)).EndInit();
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
				Application.DoEvents();
				this.SetProgressBarValue(100, 100);		

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
				lb_DT_EVAL.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","DT_EVAL");
				lb_DY_EVAL.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","DY_EVAL");
				lb_NM_PLANT.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","NM_PLANT");
				lb_NO_EMP.Text  = this.MainFrameInterface.GetDataDictionaryItem("PU","NO_EMP");
	
				//m_lblTitle.Text = this.PageName;
		
				btn_INV_EVAL.Text = this.MainFrameInterface.GetDataDictionaryItem("PU","INV_EVAL");
			

			
				this.tb_DT_TODAY.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				this.tb_DT_TODAY.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

				tb_DT_TODAY.Text = this.MainFrameInterface.GetStringToday;

				
				SetInitialControlData();
				
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
					case "QT_BAS":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_OPEN");
						break;
					case "UM_BAS":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","UM_OPEN");
						break;
					case "AM_BAS":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","AM_OPEN");
						break;	
					case "QT_RCV":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_GR");
						break;
					case "AM_RCV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","AM_GR");
						break;
					case "QT_ISU_SUB":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_SUBSTITUTE");
						break;
					case "AM_ISU_SUB":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","AM_SUBSTITUTE");
						break;		
					case "QT_ISU":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_GI");
						break;	
					case "AM_ISU":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","AM_GI");
						break;
					case "QT_INV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","QT_INV_EVAL");
						break;
					case "UM_INV":	
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","UM_INV");
						break;
					case "AM_INV":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","AM_INV");
						break;	
					case "NM_CLSITEM":		
						temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU","FG_ACCT");
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
			_flex.Cols.Count = 18;			// 총 Col 수
			_flex.Cols.Fixed = 1;			// FixedCol 수			
			_flex.Rows.DefaultSize = 20;	

			

			_flex.Cols[0].Width = 50;	
	
								
			_flex.Cols[1].Name = "CD_ITEM";
			_flex.Cols[1].DataType = typeof(string);			
			_flex.Cols[1].Width = 120;
			_flex.SetColMaxLength("CD_ITEM",20);

			
			_flex.Cols[2].Name = "NM_ITEM";
			_flex.Cols[2].DataType = typeof(string);
			_flex.Cols[2].AllowEditing = false;
			_flex.Cols[2].Width = 150;

			_flex.Cols[3].Name = "STND_ITEM";
			_flex.Cols[3].DataType = typeof(string);
			_flex.Cols[3].AllowEditing = false;
			_flex.Cols[3].Width = 80;

			_flex.Cols[4].Name = "UNIT_IM";
			_flex.Cols[4].DataType = typeof(string);
			_flex.Cols[4].AllowEditing = false;
			_flex.Cols[4].Width = 100;

			_flex.Cols[5].Name = "NM_CLSITEM";
			_flex.Cols[5].DataType = typeof(string);
			_flex.Cols[5].AllowEditing = false;
			_flex.Cols[5].Width = 100;
				
					
			_flex.Cols[6].Name = "QT_BAS";
			_flex.Cols[6].DataType = typeof(decimal);			
			_flex.Cols[6].Width = 120;
			_flex.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[6].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_BAS",17);			
			

			_flex.Cols[7].Name = "UM_BAS";
			_flex.Cols[7].DataType = typeof(decimal);			
			_flex.Cols[7].Width = 120;	
			_flex.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[7].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.INSERT);
			_flex.SetColMaxLength("UM_BAS",17);


			_flex.Cols[8].Name = "AM_BAS";
			_flex.Cols[8].DataType = typeof(decimal);			
			_flex.Cols[8].Width = 120;
			_flex.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[8].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_BAS",17);
			
			

			_flex.Cols[9].Name = "QT_RCV";
			_flex.Cols[9].DataType = typeof(decimal);
			_flex.Cols[9].Width = 120;	
			_flex.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[9].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_RCV",17);


            _flex.Cols[10].Name = "AM_RCV";
			_flex.Cols[10].DataType = typeof(decimal);
			_flex.Cols[10].Width = 120;	
			_flex.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[10].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_RCV",17);


			_flex.Cols[11].Name = "QT_ISU_SUB";
			_flex.Cols[11].DataType = typeof(decimal);			
			_flex.Cols[11].Width = 120;
			_flex.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[11].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_ISU_SUB",17);
			
			
			_flex.Cols[12].Name = "AM_ISU_SUB";
			_flex.Cols[12].DataType = typeof(decimal);			
			_flex.Cols[12].Width = 120;	
			_flex.Cols[12].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[12].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_ISU_SUB",17);


			_flex.Cols[13].Name = "QT_ISU";
			_flex.Cols[13].DataType = typeof(decimal);			
			_flex.Cols[13].Width = 120;
			_flex.Cols[13].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[13].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_ISU",17);
			
			
			_flex.Cols[14].Name = "AM_ISU";
			_flex.Cols[14].DataType = typeof(decimal);
			_flex.Cols[14].Width = 120;	
			_flex.Cols[14].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[14].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_ISU",17);

		
			_flex.Cols[15].Name = "QT_INV";
			_flex.Cols[15].DataType = typeof(decimal);
			_flex.Cols[15].Width = 120;	
			_flex.Cols[15].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[15].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.INSERT);
			_flex.SetColMaxLength("QT_INV",17);


			_flex.Cols[16].Name = "UM_INV";
			_flex.Cols[16].DataType = typeof(decimal);
			_flex.Cols[16].Width = 120;	
			_flex.Cols[16].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[16].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.INSERT);
			_flex.SetColMaxLength("UM_INV",17);

		
			_flex.Cols[17].Name = "AM_INV";
			_flex.Cols[17].DataType = typeof(decimal);
			_flex.Cols[17].Width = 120;	
			_flex.Cols[17].TextAlign = TextAlignEnum.RightCenter;
			_flex.Cols[17].Format =this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY,FormatFgType.INSERT);
			_flex.SetColMaxLength("AM_INV",17);

		
			_flex.AllowSorting = AllowSortingEnum.None;
		
			_flex.NewRowEditable = false;
			_flex.EnterKeyAddRow = false;
			_flex.AllowEditing = false;
		
			
			_flex.GridStyle = GridStyleEnum.Green;
		
			this.SetUserGrid(this._flex);	

			_flex.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			
			//_flex.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(_flex_AfterSort);	
			_flex.AfterDataRefresh += new System.ComponentModel.ListChangedEventHandler(_flex_AfterDataRefresh);						
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flex.Cols.Count-1; i++)
				_flex[0, i] = GetDDItem(_flex.Cols[i].Name);

			_flex.Redraw = true;	
					
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

					//					_flex.Redraw = false;
					//					_flex.BindingStart();
					//					_flex.DataSource = new DataView(ds_Ty1.Tables[0]);
					//					_flex.BindingEnd();
					//					_flex.Redraw = true;					
				
									
					this.Enabled = true; //페이지 전체 활성
					cbo_CD_PLANT.Focus();
					
				}
					
				//	base.OnPaint(e);
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}		
			finally
			{
				_flex.Redraw = true;
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

//				string[] lsa_args = {"P_N;"};
//				object[] args = { this.LoginInfo.CompanyCode, lsa_args};
//				DataSet g_dsCombo = (DataSet)MainFrameInterface.InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);
	
				DataSet g_dsCombo = MainFrameInterface.GetComboData("NC;MA_PLANT");
			
				
				// 출고공장	
				cbo_CD_PLANT.DataSource = g_dsCombo.Tables[0].Copy();
				cbo_CD_PLANT.DisplayMember = "NAME";
				cbo_CD_PLANT.ValueMember = "CODE";

			
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


		#region ♣ 메인버튼 이벤트

		#region -> DoContinue

		private bool DoContinue()
		{
			//m_lblTitle.Focus();
			if(_flex.Editor != null)
			{
				return _flex.FinishEditing(false);
			}
			
			return true;
		}

		#endregion

		#region -> 조회버튼클릭
		/// <summary>
		/// 브라우저의 조회 버턴 클릭시 처리부
		/// </summary>
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{					
				Cursor.Current = Cursors.WaitCursor;
				pur.P_PU_EVAL_SUB m_dlg = new pur.P_PU_EVAL_SUB(this.MainFrameInterface);
				if( m_dlg.ShowDialog(this) == DialogResult.OK)
				{
					//m_lblTitle.Focus();


					object[] args = new object[3];
					args[0] = m_dlg.m_SelecedRow["YM_STANDARD"].ToString();
					args[1] = m_dlg.m_SelecedRow["CD_PLANT"].ToString();
					args[2] = MainFrameInterface.LoginInfo.CompanyCode;
				

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
					si.SpNameSelect = "SP_PU_EVAL_SELECT";			
					si.SpParamsSelect = args;
					ResultData result = (ResultData)this.FillDataTable(si);

					DataTable ldt_data = (DataTable)result.DataValue;



//					string[] ls_args = new string[3];
//					ls_args[0] = m_dlg.m_SelecedRow["YM_STANDARD"].ToString();
//					ls_args[1] = m_dlg.m_SelecedRow["CD_PLANT"].ToString();
//					ls_args[2] = MainFrameInterface.LoginInfo.CompanyCode;
//				
//					object[] lobj_args = new object[3];
//					lobj_args[0] = ls_args;
//					lobj_args[1] = "CC_PU_EVAL";
//					lobj_args[2] = 2;
//					DataTable ldt_data = (DataTable)(this.MainFrameInterface.InvokeRemoteMethod("PurMediationControl_NTX", "pur.CC_PU_EVAL_NTX", "CC_PU_EVAL_NTX.rem", "SelectDataTable", lobj_args));				
						
					if( ldt_data == null || ldt_data.Rows.Count <=0)
					{
						this.ShowMessage("IK1_003");
						//Duzon.Common.Controls.MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("MA_M000010"));						
						return ;
					}			
					
					_flex.Redraw = false;
					_flex.BindingStart();
					_flex.DataSource = new DataView(ldt_data);
					_flex.BindingEnd();
					_flex.Redraw = true;

					SubTotalDisplay( _flex);

					cbo_CD_PLANT.SelectedValue = m_dlg.m_SelecedRow["CD_PLANT"].ToString();
					tb_NO_EMP.CodeValue = m_dlg.m_SelecedRow["NO_EMP"].ToString();
					tb_NO_EMP.CodeName = m_dlg.m_SelecedRow["NM_KOR"].ToString();
					
					dTP_From.Value = new System.DateTime(System.Int32.Parse(m_dlg.m_SelecedRow["YM_FSTANDARD"].ToString().Substring(0,4)),
						System.Int32.Parse(m_dlg.m_SelecedRow["YM_FSTANDARD"].ToString().Substring(4,2)),1);
					dTP_To.Value = new System.DateTime(System.Int32.Parse(m_dlg.m_SelecedRow["YM_STANDARD"].ToString().Substring(0,4)),
						System.Int32.Parse(m_dlg.m_SelecedRow["YM_STANDARD"].ToString().Substring(4,2)),1);

					tb_DT_TODAY.Text = m_dlg.m_SelecedRow["DT_INPUT"].ToString();

					this.ToolBarDeleteButtonEnabled = true;
					EnabledControl(false);					
                    
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
				this.SetProgressBarValue(100, 0);					
				this.ShowStatusBarMessage(0);
				Cursor.Current = Cursors.Default;
			}	
		}				
	
		#endregion
	
		#region -> 삭제버튼클릭
		/// <summary>
		/// 브라우저의 삭제 버턴 클릭시 처리부
		/// </summary>
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{	
			try
			{	
                
				Cursor.Current = Cursors.WaitCursor;					
				DialogResult result = this.ShowMessage("QY2_003");					
				if(result == DialogResult.Yes)
				{							
					Cursor.Current = Cursors.WaitCursor;
					//m_lblTitle.Focus();

					//SP_PU_EVAL_DELETE

					object[] m_obj = new object[6];
					m_obj[0] = this.LoginInfo.CompanyCode;
					m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
					m_obj[2] = dTP_To.Value.Year.ToString("0000")+dTP_To.Value.Month.ToString("00");						
					m_obj[3] = dTP_From.Value.Year.ToString("0000")+dTP_From.Value.Month.ToString("00");
					m_obj[4] = this.LoginInfo.UserID;
					m_obj[5] = this.MainFrameInterface.GetStringDetailToday;

//					object[] m_obj = new object[2];
//					m_obj[0] = tb_no_io.Text;
//					m_obj[1] = this.MainFrameInterface.LoginInfo.CompanyCode;

					ResultData ret = (ResultData)this.ExecSp("SP_PU_EVAL_DELETE", m_obj);
					if(ret.Result)
					{
						_flex.DataTable.Clear();
						SetInitialControlData(); 	
						this.ShowMessage("IK1_002");
					}
					else
					{
						this.ShowMessage("EK1_002");							
						return; 
					}


//
//					string[] ls_args = new string[5];
//					ls_args[0] = dTP_To.Value.Year.ToString("0000")+dTP_To.Value.Month.ToString("00");
//					ls_args[1] = cbo_CD_PLANT.SelectedValue.ToString();
//					ls_args[2] = this.LoginInfo.CompanyCode;
//					ls_args[3] = this.LoginInfo.UserID;
//					ls_args[4] = dTP_From.Value.Year.ToString("0000")+dTP_From.Value.Month.ToString("00");
//		
//
//
//					object[] lobj_args =new object[1];
//					lobj_args[0] = ls_args;
//
//					int li_result  = (int)(this.MainFrameInterface.InvokeRemoteMethod("PurMediationControl", "pur.CC_PU_EVAL", "CC_PU_EVAL.rem","DeleteEval", 
//						lobj_args));
//
//					if( li_result >=0)
//					{
//						_flex.DataTable.Clear();
//						SetInitialControlData(); 	
//						this.ShowMessage("IK1_002");
//					}
//					else
//					{
//						this.ShowMessage("EK1_002");							
//						return; 
//					}
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
				Cursor.Current = Cursors.WaitCursor;
				SetInitialControlData(); 
				cbo_CD_PLANT.Focus();
				_flex.DataTable.Clear();					

			}			
			finally
			{				
				this.ShowStatusBarMessage(4);
			}
		}	

		#endregion
	
		#endregion

		#region ♣ 그리드 이벤트

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
	
		#region -> _flex_AfterDataRefresh
	
		private void _flex_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			try
			{
				//	SubTotalDisplay((Dass.FlexGrid.FlexGrid)sender);
				SubTotalDisplay(_flex);
			}
			catch
			{
			}
		}

	
		#endregion


		#endregion


		#region ♣ 기타 이벤트	

		#region -> 재고평가 버턴 클릭
		private void btn_INV_EVAL_Click(object sender, System.EventArgs e)
		{
			try
			{				
				if( !FieldCheck_Head())
				{
					return ;
				}

				DialogResult resultOK = this.ShowMessage("PU_M000066","QY2");				
				if(resultOK == DialogResult.Yes)
				{					
					Cursor.Current = Cursors.WaitCursor;

					object[] m_obj = new object[12];
					m_obj[0] = this.LoginInfo.CompanyCode;
					m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
					m_obj[2] = dTP_From.Value.Year.ToString("0000")+ dTP_From.Value.Month.ToString("00");
					m_obj[3] = dTP_To.Value.Year.ToString("0000")+ dTP_To.Value.Month.ToString("00");	
					m_obj[4] = dTP_From.Value.Year.ToString("0000");	
					m_obj[5] = _cddept;
					m_obj[6] = tb_NO_EMP.CodeValue.ToString();
					m_obj[7] = tb_DT_TODAY.MaskEditBox.ClipText;
					m_obj[8] = this.LoginInfo.UserID;
					m_obj[9] = this.MainFrameInterface.GetStringDetailToday;
					m_obj[10] = this.LoginInfo.UserID;
					m_obj[11] = this.MainFrameInterface.GetStringDetailToday;
					ResultData ret = (ResultData)this.ExecSp("SP_PU_AMINV_PROCESS_TOTAL", m_obj);

//					if(ret.Result)
//					{
//						_flex.DataTable.Clear();
//						SetInitialControlData(); 	
//						this.ShowMessage("PU_M000068");
//					}
//					else
//					{
//						return;
//					}
					


					object[] args = new object[3];
					args[0] = dTP_To.Value.Year.ToString("0000")+ dTP_To.Value.Month.ToString("00");
					args[1] = cbo_CD_PLANT.SelectedValue.ToString();
					args[2] = this.LoginInfo.CompanyCode;
				

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
					si.SpNameSelect = "SP_PU_EVAL_SELECT";			
					si.SpParamsSelect = args;
					ResultData result = (ResultData)this.FillDataTable(si);

					DataTable ldt_result = (DataTable)result.DataValue;



//					string[] ls_args = new string[8];
//					ls_args[0] = this.LoginInfo.CompanyCode;
//					ls_args[1] = cbo_CD_PLANT.SelectedValue.ToString();
//					ls_args[2] = dTP_From.Value.Year.ToString("0000")+ dTP_From.Value.Month.ToString("00");
//					ls_args[3] = dTP_To.Value.Year.ToString("0000")+ dTP_To.Value.Month.ToString("00");				
//					ls_args[4] = _cddept;
//					ls_args[5] = tb_NO_EMP.Tag.ToString();
//					ls_args[6] = tb_DT_TODAY.MaskEditBox.ClipText;
//					ls_args[7] = this.LoginInfo.UserID;
//
//					object[] lobj_args =new object[1];
//					lobj_args[0] = ls_args;
//
//
//					DataTable ldt_result  = (DataTable)(this.MainFrameInterface.InvokeRemoteMethod("PurMediationControl", "pur.CC_PU_EVAL", "CC_PU_EVAL.rem","RuningEval", 
//						lobj_args));

					// 어떻게 할까.? 그냥 바인딩 할까?
				
					_flex.Redraw = false;
					_flex.BindingStart();
					_flex.DataSource = new DataView(ldt_result);
					_flex.BindingEnd();
					_flex.Redraw = true;	
		
					SubTotalDisplay( _flex);

				
					this.ShowMessage("PU_M000068");					
					EnabledControl(false);
					this.ToolBarDeleteButtonEnabled  = true;

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

		
		#region -> tb_CONTROL_KeyDown
		
		private void tb_CONTROL_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData.ToString() =="Enter")
			{			
				SendKeys.SendWait("{TAB}");	
			}		
		}

		#endregion

		#endregion
	

		#region ♣ 기타 함수
			

		#region -> SubTotalDisplay
		private void SubTotalDisplay( Dass.FlexGrid.FlexGrid flex)
		{
			try
			{
				flex.SubtotalPosition = SubtotalPositionEnum.BelowData;
			//	flex.SelectionMode=SelectionModeEnum.Cell;
						

				CellStyle s = flex.Styles[CellStyleEnum.Subtotal0];
				s.BackColor = Color.FromArgb(234, 234, 234);		
				s.ForeColor = Color.Black;
				s.Font = new Font(flex.Font, FontStyle.Bold);
									
				flex.Subtotal(AggregateEnum.Clear);//MA, GRAND
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["AM_BAS"].Index);
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["AM_RCV"].Index);
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["AM_ISU_SUB"].Index);
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["AM_ISU"].Index);
				flex.Subtotal(AggregateEnum.Sum,0,-1,flex.Cols["AM_INV"].Index);
				//	flex[flex.Rows.Count -1 ,1] = 	this.GetDataDictionaryItem("MA","GRAND");	
				//  

			}
			catch
			{
			}
		}
		#endregion	


		#region -> 체크 함수
		private bool FieldCheck_Head()
		{
			try
			{
				//  발주등록 부분의 필드 검사
				if(cbo_CD_PLANT.SelectedValue.ToString() =="" )
				{					
					cbo_CD_PLANT.Focus();		
					this.ShowMessage("WK1_004",lb_NM_PLANT.Text);
					//Duzon.Common.Controls.MessageBoxEx.Show(lb_NM_PLANT.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
					return false;
				}
				
				//  발주등록 부분의 필드 검사
				if(tb_NO_EMP.CodeValue.ToString() =="" )
				{					
					tb_NO_EMP.Focus();
					this.ShowMessage("WK1_004",lb_NO_EMP.Text);
					//Duzon.Common.Controls.MessageBoxEx.Show(lb_NO_EMP.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
					return false;
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
			return true;		
		}
	
		#endregion

		#region -> SetInitialControlData
		private void SetInitialControlData()
		{
			try
			{
				EnabledControl(true);				
				dTP_From.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0,4)),
					System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(4,2)),1);
				dTP_To.Value = new System.DateTime(System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(0,4)),
					System.Int32.Parse(MainFrameInterface.GetStringToday.Substring(4,2)),1);
				tb_DT_TODAY.Text = this.MainFrameInterface.GetStringToday; 
				tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;
				tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
				_cddept = this.LoginInfo.DeptCode;

				this.ToolBarAddButtonEnabled = true;
				this.ToolBarDeleteButtonEnabled = false;
				this.ToolBarSaveButtonEnabled = false;
				this.ToolBarSearchButtonEnabled = true;
				                
			}
			catch
			{
			}
		}

		#endregion

		#region -> EnabledControl
		private void EnabledControl(bool isEnabled)
		{
			try
			{
				cbo_CD_PLANT.Enabled = isEnabled;
				tb_NO_EMP.Enabled = isEnabled;
				dTP_From.Enabled = isEnabled;
				dTP_To.Enabled = isEnabled;
				btn_INV_EVAL.Enabled = isEnabled;						
			}
			catch
			{
			}
		}


		#endregion

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
		}// 



		#endregion	

	
	}
}
