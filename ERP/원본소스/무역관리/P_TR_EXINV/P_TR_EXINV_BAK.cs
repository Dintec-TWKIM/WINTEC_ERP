//********************************************************************
// ��   ��   �� : 
// ��   ��   �� : 2006-06-12
// ��   ��   �� : ����
// �� ��  �� �� : ��������
// ����ý��۸� : �������
// �� �� ��  �� : ������
// ������Ʈ  �� : P_TR_EXINV
//********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Util;
using Duzon.Common.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

namespace trade
{
	public class P_TR_EXINV_BAK : Duzon.Common.Forms.PageBase
	{
		#region �� �ڵ� ���� ����

        private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel28;
		private Duzon.Common.Controls.PanelExt panel29;
		private Duzon.Common.Controls.PanelExt panel25;
		private Duzon.Common.Controls.PanelExt panel26;
		private Duzon.Common.Controls.PanelExt panel27;
		private Duzon.Common.Controls.PanelExt panel22;
		private Duzon.Common.Controls.PanelExt panel23;
		private Duzon.Common.Controls.PanelExt panel24;
		private Duzon.Common.Controls.PanelExt panel19;
		private Duzon.Common.Controls.PanelExt panel20;
		private Duzon.Common.Controls.PanelExt panel21;
		private Duzon.Common.Controls.PanelExt panel17;
		private Duzon.Common.Controls.PanelExt panel18;
		private Duzon.Common.Controls.PanelExt panel16;
        private Duzon.Common.Controls.PanelExt panel15;
        private Duzon.Common.Controls.DropDownComboBox cbo�ŷ�����;
		private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel11;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel12;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.TextBoxExt txt�ε�����;
		private Duzon.Common.Controls.TextBoxExt txt����������;
		private Duzon.Common.Controls.TextBoxExt txt������;
		private Duzon.Common.Controls.TextBoxExt txt����CT��ȣ;
        private Duzon.Common.Controls.DropDownComboBox cbo�߷�����;
        private Duzon.Common.Controls.DropDownComboBox cbo��۹��;
        private Duzon.Common.Controls.DropDownComboBox cbo��ȭ;
        private Duzon.Common.Controls.TextBoxExt txt���1;
		private Duzon.Common.Controls.TextBoxExt txt���5;
		private Duzon.Common.Controls.TextBoxExt txt���4;
		private Duzon.Common.Controls.TextBoxExt txt���2;
        private Duzon.Common.Controls.TextBoxExt txt���3;
        private Duzon.Common.Controls.TextBoxExt txt������;
		private Duzon.Common.Controls.TextBoxExt txtVESSEL��;
        private Duzon.Common.Controls.TextBoxExt txt����CT��ȣ;
        private Duzon.Common.Controls.DropDownComboBox cbo��������;
        private Duzon.Common.Controls.DropDownComboBox cbo�������;
		private Duzon.Common.Controls.LabelExt lbl�ε�����;
		private Duzon.Common.Controls.LabelExt lbl����������;
		private Duzon.Common.Controls.LabelExt lbl������;
		private Duzon.Common.Controls.LabelExt lbl��ȭ�ݾ�;
		private Duzon.Common.Controls.LabelExt lbl����CT��ȣ;
		private Duzon.Common.Controls.LabelExt lbl���߷�;
		private Duzon.Common.Controls.LabelExt lbl�߷�����;
		private Duzon.Common.Controls.LabelExt lbl��۹��;
		private Duzon.Common.Controls.LabelExt lbl���������;
		private Duzon.Common.Controls.LabelExt lbl����������;
		private Duzon.Common.Controls.LabelExt lbl������;
		private Duzon.Common.Controls.LabelExt lbl������;
		private Duzon.Common.Controls.LabelExt lbl�����;
		private Duzon.Common.Controls.LabelExt lbl����SO��ȣ;
		private Duzon.Common.Controls.LabelExt lbl�ŷ�����;
		private Duzon.Common.Controls.LabelExt lbl��������;
		private Duzon.Common.Controls.TextBoxExt txt�����ȣ;
		private Duzon.Common.Controls.LabelExt lbl��ȭ;
		private Duzon.Common.Controls.LabelExt lbl���;
		private Duzon.Common.Controls.LabelExt lbl����;
		private Duzon.Common.Controls.LabelExt lblVESSEL��;
		private Duzon.Common.Controls.LabelExt lbl������;
		private Duzon.Common.Controls.LabelExt lbl��������ó;
		private Duzon.Common.Controls.LabelExt lbl����CT��ȣ;
		private Duzon.Common.Controls.LabelExt lbl���߷�;
		private Duzon.Common.Controls.LabelExt lbl��������;
		private Duzon.Common.Controls.LabelExt lbl�������;
		private Duzon.Common.Controls.LabelExt lbl������;
		private Duzon.Common.Controls.LabelExt lbl������;
		private Duzon.Common.Controls.LabelExt lbl�����;
		private Duzon.Common.Controls.LabelExt lbl�����׷�;
		private Duzon.Common.Controls.LabelExt lbl�ŷ�ó;
		private Duzon.Common.Controls.LabelExt lbl�����ſ����ȣ;
        private Duzon.Common.Controls.LabelExt lbl�����ȣ;
        private Duzon.Common.Controls.DropDownComboBox cbo������;
		private Duzon.Common.Controls.CurrencyTextBox cur��ȭ�ݾ�;
		private Duzon.Common.Controls.RoundedButton btn�������;
		private Duzon.Common.Controls.RoundedButton btn�ǸŰ����;
		private Duzon.Common.Controls.CurrencyTextBox cur���߷�;
        private Duzon.Common.Controls.CurrencyTextBox cur���߷�;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�����;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�����;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�ŷ�ó;
        private Duzon.Common.BpControls.BpCodeTextBox bbpc�����׷�;
        private Duzon.Common.BpControls.BpCodeTextBox bpc������;
        private Duzon.Common.BpControls.BpCodeTextBox bpc������;
        private Duzon.Common.BpControls.BpCodeTextBox bpc������;
        private Duzon.Common.BpControls.BpCodeTextBox bpc����;
        private Duzon.Common.BpControls.BpCodeTextBox bpc��������ó;
        private DatePicker dtp��������;
        private DatePicker dtp���������;
        private DatePicker dtp����������;
        private PanelExt panelExt1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextButton tbtn����SO��ȣ;
        private TextButton tbtn�����ſ����ȣ;
		private System.ComponentModel.IContainer components;

		#endregion

		#region �� �Ҹ� �κ�

        /// <summary> 
		/// ��� ���� ��� ���ҽ��� �����մϴ�.
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
		/// �����̳� ������ �ʿ��� �޼����Դϴ�. 
		/// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXINV_BAK));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.tbtn����SO��ȣ = new Duzon.Common.Controls.TextButton();
            this.tbtn�����ſ����ȣ = new Duzon.Common.Controls.TextButton();
            this.dtp���������� = new Duzon.Common.Controls.DatePicker();
            this.dtp��������� = new Duzon.Common.Controls.DatePicker();
            this.dtp�������� = new Duzon.Common.Controls.DatePicker();
            this.bpc��������ó = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc���� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc������ = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc������ = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc������ = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bbpc�����׷� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc�ŷ�ó = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc����� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc����� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cur���߷� = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur���߷� = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur��ȭ�ݾ� = new Duzon.Common.Controls.CurrencyTextBox();
            this.cbo������ = new Duzon.Common.Controls.DropDownComboBox();
            this.txt�ε����� = new Duzon.Common.Controls.TextBoxExt();
            this.txt���������� = new Duzon.Common.Controls.TextBoxExt();
            this.txt������ = new Duzon.Common.Controls.TextBoxExt();
            this.txt����CT��ȣ = new Duzon.Common.Controls.TextBoxExt();
            this.cbo�߷����� = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo��۹�� = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo��ȭ = new Duzon.Common.Controls.DropDownComboBox();
            this.txt���1 = new Duzon.Common.Controls.TextBoxExt();
            this.txt���5 = new Duzon.Common.Controls.TextBoxExt();
            this.txt���4 = new Duzon.Common.Controls.TextBoxExt();
            this.txt���2 = new Duzon.Common.Controls.TextBoxExt();
            this.txt���3 = new Duzon.Common.Controls.TextBoxExt();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.txt������ = new Duzon.Common.Controls.TextBoxExt();
            this.panel28 = new Duzon.Common.Controls.PanelExt();
            this.panel29 = new Duzon.Common.Controls.PanelExt();
            this.panel25 = new Duzon.Common.Controls.PanelExt();
            this.panel26 = new Duzon.Common.Controls.PanelExt();
            this.panel27 = new Duzon.Common.Controls.PanelExt();
            this.panel22 = new Duzon.Common.Controls.PanelExt();
            this.panel23 = new Duzon.Common.Controls.PanelExt();
            this.panel24 = new Duzon.Common.Controls.PanelExt();
            this.panel19 = new Duzon.Common.Controls.PanelExt();
            this.panel20 = new Duzon.Common.Controls.PanelExt();
            this.panel21 = new Duzon.Common.Controls.PanelExt();
            this.panel17 = new Duzon.Common.Controls.PanelExt();
            this.panel18 = new Duzon.Common.Controls.PanelExt();
            this.panel16 = new Duzon.Common.Controls.PanelExt();
            this.panel15 = new Duzon.Common.Controls.PanelExt();
            this.txtVESSEL�� = new Duzon.Common.Controls.TextBoxExt();
            this.txt����CT��ȣ = new Duzon.Common.Controls.TextBoxExt();
            this.cbo�������� = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo������� = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo�ŷ����� = new Duzon.Common.Controls.DropDownComboBox();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lbl�ε����� = new Duzon.Common.Controls.LabelExt();
            this.lbl���������� = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl��ȭ�ݾ� = new Duzon.Common.Controls.LabelExt();
            this.lbl����CT��ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl���߷� = new Duzon.Common.Controls.LabelExt();
            this.lbl�߷����� = new Duzon.Common.Controls.LabelExt();
            this.lbl��۹�� = new Duzon.Common.Controls.LabelExt();
            this.lbl��������� = new Duzon.Common.Controls.LabelExt();
            this.lbl���������� = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl����� = new Duzon.Common.Controls.LabelExt();
            this.lbl����SO��ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl�����ſ����ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl�ŷ����� = new Duzon.Common.Controls.LabelExt();
            this.txt�����ȣ = new Duzon.Common.Controls.TextBoxExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.lbl��ȭ = new Duzon.Common.Controls.LabelExt();
            this.lbl��� = new Duzon.Common.Controls.LabelExt();
            this.lbl���� = new Duzon.Common.Controls.LabelExt();
            this.lblVESSEL�� = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl��������ó = new Duzon.Common.Controls.LabelExt();
            this.lbl����CT��ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl���߷� = new Duzon.Common.Controls.LabelExt();
            this.lbl�������� = new Duzon.Common.Controls.LabelExt();
            this.lbl������� = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl����� = new Duzon.Common.Controls.LabelExt();
            this.lbl�����׷� = new Duzon.Common.Controls.LabelExt();
            this.lbl�ŷ�ó = new Duzon.Common.Controls.LabelExt();
            this.lbl�����ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl�������� = new Duzon.Common.Controls.LabelExt();
            this.btn������� = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn�ǸŰ���� = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp����������)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp���������)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp��������)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur���߷�)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur���߷�)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ȭ�ݾ�)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tbtn����SO��ȣ);
            this.panel4.Controls.Add(this.tbtn�����ſ����ȣ);
            this.panel4.Controls.Add(this.dtp����������);
            this.panel4.Controls.Add(this.dtp���������);
            this.panel4.Controls.Add(this.dtp��������);
            this.panel4.Controls.Add(this.bpc��������ó);
            this.panel4.Controls.Add(this.bpc����);
            this.panel4.Controls.Add(this.bpc������);
            this.panel4.Controls.Add(this.bpc������);
            this.panel4.Controls.Add(this.bpc������);
            this.panel4.Controls.Add(this.bbpc�����׷�);
            this.panel4.Controls.Add(this.bpc�ŷ�ó);
            this.panel4.Controls.Add(this.bpc�����);
            this.panel4.Controls.Add(this.bpc�����);
            this.panel4.Controls.Add(this.cur���߷�);
            this.panel4.Controls.Add(this.cur���߷�);
            this.panel4.Controls.Add(this.cur��ȭ�ݾ�);
            this.panel4.Controls.Add(this.cbo������);
            this.panel4.Controls.Add(this.txt�ε�����);
            this.panel4.Controls.Add(this.txt����������);
            this.panel4.Controls.Add(this.txt������);
            this.panel4.Controls.Add(this.txt����CT��ȣ);
            this.panel4.Controls.Add(this.cbo�߷�����);
            this.panel4.Controls.Add(this.cbo��۹��);
            this.panel4.Controls.Add(this.cbo��ȭ);
            this.panel4.Controls.Add(this.txt���1);
            this.panel4.Controls.Add(this.txt���5);
            this.panel4.Controls.Add(this.txt���4);
            this.panel4.Controls.Add(this.txt���2);
            this.panel4.Controls.Add(this.txt���3);
            this.panel4.Controls.Add(this.panel12);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel11);
            this.panel4.Controls.Add(this.txt������);
            this.panel4.Controls.Add(this.panel28);
            this.panel4.Controls.Add(this.panel29);
            this.panel4.Controls.Add(this.panel25);
            this.panel4.Controls.Add(this.panel26);
            this.panel4.Controls.Add(this.panel27);
            this.panel4.Controls.Add(this.panel22);
            this.panel4.Controls.Add(this.panel23);
            this.panel4.Controls.Add(this.panel24);
            this.panel4.Controls.Add(this.panel19);
            this.panel4.Controls.Add(this.panel20);
            this.panel4.Controls.Add(this.panel21);
            this.panel4.Controls.Add(this.panel17);
            this.panel4.Controls.Add(this.panel18);
            this.panel4.Controls.Add(this.panel16);
            this.panel4.Controls.Add(this.panel15);
            this.panel4.Controls.Add(this.txtVESSEL��);
            this.panel4.Controls.Add(this.txt����CT��ȣ);
            this.panel4.Controls.Add(this.cbo��������);
            this.panel4.Controls.Add(this.cbo�������);
            this.panel4.Controls.Add(this.cbo�ŷ�����);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.txt�����ȣ);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 525);
            this.panel4.TabIndex = 124;
            // 
            // tbtn����SO��ȣ
            // 
            this.tbtn����SO��ȣ.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tbtn����SO��ȣ.ButtonImage")));
            this.tbtn����SO��ȣ.Location = new System.Drawing.Point(510, 29);
            this.tbtn����SO��ȣ.Name = "tbtn����SO��ȣ";
            this.tbtn����SO��ȣ.Size = new System.Drawing.Size(250, 21);
            this.tbtn����SO��ȣ.TabIndex = 142;
            this.tbtn����SO��ȣ.Tag = "NO_SO";
            this.tbtn����SO��ȣ.Text = "textButton2";
            // 
            // tbtn�����ſ����ȣ
            // 
            this.tbtn�����ſ����ȣ.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tbtn�����ſ����ȣ.ButtonImage")));
            this.tbtn�����ſ����ȣ.Location = new System.Drawing.Point(510, 2);
            this.tbtn�����ſ����ȣ.Name = "tbtn�����ſ����ȣ";
            this.tbtn�����ſ����ȣ.Size = new System.Drawing.Size(250, 21);
            this.tbtn�����ſ����ȣ.TabIndex = 141;
            this.tbtn�����ſ����ȣ.Tag = "NO_LC";
            this.tbtn�����ſ����ȣ.Text = "textButton1";
            // 
            // dtp����������
            // 
            this.dtp����������.BackColor = System.Drawing.Color.White;
            this.dtp����������.CalendarBackColor = System.Drawing.Color.White;
            this.dtp����������.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp����������.FriDayColor = System.Drawing.Color.Blue;
            this.dtp����������.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp����������.Location = new System.Drawing.Point(510, 178);
            this.dtp����������.Mask = "####/##/##";
            this.dtp����������.MaskBackColor = System.Drawing.Color.White;
            this.dtp����������.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp����������.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp����������.Modified = false;
            this.dtp����������.Name = "dtp����������";
            this.dtp����������.PaddingCharacter = '_';
            this.dtp����������.PassivePromptCharacter = '_';
            this.dtp����������.PromptCharacter = '_';
            this.dtp����������.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp����������.ShowToDay = true;
            this.dtp����������.ShowTodayCircle = true;
            this.dtp����������.ShowUpDown = false;
            this.dtp����������.Size = new System.Drawing.Size(87, 21);
            this.dtp����������.SunDayColor = System.Drawing.Color.Red;
            this.dtp����������.TabIndex = 15;
            this.dtp����������.Tag = "DT_LOADING";
            this.dtp����������.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp����������.TitleForeColor = System.Drawing.Color.Black;
            this.dtp����������.ToDayColor = System.Drawing.Color.Red;
            this.dtp����������.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp����������.UseKeyF3 = true;
            this.dtp����������.Value = new System.DateTime(((long)(0)));
            this.dtp����������.Click += new System.EventHandler(this.dtp����������_Click);
            // 
            // dtp���������
            // 
            this.dtp���������.BackColor = System.Drawing.Color.White;
            this.dtp���������.CalendarBackColor = System.Drawing.Color.White;
            this.dtp���������.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp���������.FriDayColor = System.Drawing.Color.Blue;
            this.dtp���������.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp���������.Location = new System.Drawing.Point(510, 203);
            this.dtp���������.Mask = "####/##/##";
            this.dtp���������.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp���������.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp���������.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp���������.Modified = false;
            this.dtp���������.Name = "dtp���������";
            this.dtp���������.PaddingCharacter = '_';
            this.dtp���������.PassivePromptCharacter = '_';
            this.dtp���������.PromptCharacter = '_';
            this.dtp���������.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp���������.ShowToDay = true;
            this.dtp���������.ShowTodayCircle = true;
            this.dtp���������.ShowUpDown = false;
            this.dtp���������.Size = new System.Drawing.Size(87, 21);
            this.dtp���������.SunDayColor = System.Drawing.Color.Red;
            this.dtp���������.TabIndex = 17;
            this.dtp���������.Tag = "DT_TO";
            this.dtp���������.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp���������.TitleForeColor = System.Drawing.Color.Black;
            this.dtp���������.ToDayColor = System.Drawing.Color.Red;
            this.dtp���������.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp���������.UseKeyF3 = true;
            this.dtp���������.Value = new System.DateTime(((long)(0)));
            // 
            // dtp��������
            // 
            this.dtp��������.BackColor = System.Drawing.Color.White;
            this.dtp��������.CalendarBackColor = System.Drawing.Color.White;
            this.dtp��������.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp��������.FriDayColor = System.Drawing.Color.Blue;
            this.dtp��������.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp��������.Location = new System.Drawing.Point(120, 28);
            this.dtp��������.Mask = "####/##/##";
            this.dtp��������.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp��������.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp��������.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp��������.Modified = false;
            this.dtp��������.Name = "dtp��������";
            this.dtp��������.PaddingCharacter = '_';
            this.dtp��������.PassivePromptCharacter = '_';
            this.dtp��������.PromptCharacter = '_';
            this.dtp��������.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp��������.ShowToDay = true;
            this.dtp��������.ShowTodayCircle = true;
            this.dtp��������.ShowUpDown = false;
            this.dtp��������.Size = new System.Drawing.Size(87, 21);
            this.dtp��������.SunDayColor = System.Drawing.Color.Red;
            this.dtp��������.TabIndex = 2;
            this.dtp��������.Tag = "DT_BALLOT";
            this.dtp��������.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp��������.TitleForeColor = System.Drawing.Color.Black;
            this.dtp��������.ToDayColor = System.Drawing.Color.Red;
            this.dtp��������.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp��������.UseKeyF3 = false;
            this.dtp��������.Value = new System.DateTime(((long)(0)));
            // 
            // bpc��������ó
            // 
            this.bpc��������ó.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc��������ó.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc��������ó.ButtonImage")));
            this.bpc��������ó.ChildMode = "";
            this.bpc��������ó.CodeName = "";
            this.bpc��������ó.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc��������ó.CodeValue = "";
            this.bpc��������ó.ComboCheck = true;
            this.bpc��������ó.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc��������ó.ItemBackColor = System.Drawing.Color.White;
            this.bpc��������ó.Location = new System.Drawing.Point(120, 327);
            this.bpc��������ó.Name = "bpc��������ó";
            this.bpc��������ó.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc��������ó.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc��������ó.SearchCode = true;
            this.bpc��������ó.SelectCount = 0;
            this.bpc��������ó.SetDefaultValue = false;
            this.bpc��������ó.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc��������ó.Size = new System.Drawing.Size(268, 21);
            this.bpc��������ó.TabIndex = 26;
            this.bpc��������ó.Tag = "CD_CUST_IN";
            this.bpc��������ó.Text = "bpCodeTextBox1";
            this.bpc��������ó.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc����
            // 
            this.bpc����.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc����.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc����.ButtonImage")));
            this.bpc����.ChildMode = "";
            this.bpc����.CodeName = "";
            this.bpc����.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc����.CodeValue = "";
            this.bpc����.ComboCheck = true;
            this.bpc����.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc����.ItemBackColor = System.Drawing.Color.White;
            this.bpc����.Location = new System.Drawing.Point(120, 203);
            this.bpc����.Name = "bpc����";
            this.bpc����.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc����.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc����.SearchCode = true;
            this.bpc����.SelectCount = 0;
            this.bpc����.SetDefaultValue = false;
            this.bpc����.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc����.Size = new System.Drawing.Size(268, 21);
            this.bpc����.TabIndex = 16;
            this.bpc����.Tag = "SHIP_CORP";
            this.bpc����.Text = "bpCodeTextBox1";
            this.bpc����.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc������
            // 
            this.bpc������.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc������.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc������.ButtonImage")));
            this.bpc������.ChildMode = "";
            this.bpc������.CodeName = "";
            this.bpc������.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc������.CodeValue = "";
            this.bpc������.ComboCheck = true;
            this.bpc������.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc������.ItemBackColor = System.Drawing.Color.White;
            this.bpc������.Location = new System.Drawing.Point(510, 128);
            this.bpc������.Name = "bpc������";
            this.bpc������.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc������.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc������.SearchCode = true;
            this.bpc������.SelectCount = 0;
            this.bpc������.SetDefaultValue = false;
            this.bpc������.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc������.Size = new System.Drawing.Size(268, 21);
            this.bpc������.TabIndex = 11;
            this.bpc������.Tag = "CD_AGENT";
            this.bpc������.Text = "bpCodeTextBox1";
            this.bpc������.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc������
            // 
            this.bpc������.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc������.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc������.ButtonImage")));
            this.bpc������.ChildMode = "";
            this.bpc������.CodeName = "";
            this.bpc������.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc������.CodeValue = "";
            this.bpc������.ComboCheck = true;
            this.bpc������.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc������.ItemBackColor = System.Drawing.Color.White;
            this.bpc������.Location = new System.Drawing.Point(120, 128);
            this.bpc������.Name = "bpc������";
            this.bpc������.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc������.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc������.SearchCode = true;
            this.bpc������.SelectCount = 0;
            this.bpc������.SetDefaultValue = false;
            this.bpc������.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc������.Size = new System.Drawing.Size(268, 21);
            this.bpc������.TabIndex = 10;
            this.bpc������.Tag = "CD_PRODUCT;NM_PRODUCT";
            this.bpc������.Text = "bpCodeTextBox1";
            this.bpc������.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc������
            // 
            this.bpc������.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc������.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc������.ButtonImage")));
            this.bpc������.ChildMode = "";
            this.bpc������.CodeName = "";
            this.bpc������.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc������.CodeValue = "";
            this.bpc������.ComboCheck = true;
            this.bpc������.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc������.ItemBackColor = System.Drawing.Color.White;
            this.bpc������.Location = new System.Drawing.Point(510, 103);
            this.bpc������.Name = "bpc������";
            this.bpc������.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc������.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc������.SearchCode = true;
            this.bpc������.SelectCount = 0;
            this.bpc������.SetDefaultValue = false;
            this.bpc������.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc������.Size = new System.Drawing.Size(268, 21);
            this.bpc������.TabIndex = 9;
            this.bpc������.Tag = "CD_EXPORT";
            this.bpc������.Text = "bpCodeTextBox1";
            this.bpc������.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bbpc�����׷�
            // 
            this.bbpc�����׷�.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bbpc�����׷�.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bbpc�����׷�.ButtonImage")));
            this.bbpc�����׷�.ChildMode = "";
            this.bbpc�����׷�.CodeName = "";
            this.bbpc�����׷�.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bbpc�����׷�.CodeValue = "";
            this.bbpc�����׷�.ComboCheck = true;
            this.bbpc�����׷�.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bbpc�����׷�.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bbpc�����׷�.Location = new System.Drawing.Point(120, 78);
            this.bbpc�����׷�.Name = "bbpc�����׷�";
            this.bbpc�����׷�.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bbpc�����׷�.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bbpc�����׷�.SearchCode = true;
            this.bbpc�����׷�.SelectCount = 0;
            this.bbpc�����׷�.SetDefaultValue = true;
            this.bbpc�����׷�.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bbpc�����׷�.Size = new System.Drawing.Size(268, 21);
            this.bbpc�����׷�.TabIndex = 6;
            this.bbpc�����׷�.Tag = "CD_SALEGRP;NM_SALEGRP";
            this.bbpc�����׷�.Text = "bpCodeTextBox1";
            this.bbpc�����׷�.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc�ŷ�ó
            // 
            this.bpc�ŷ�ó.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc�ŷ�ó.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc�ŷ�ó.ButtonImage")));
            this.bpc�ŷ�ó.ChildMode = "";
            this.bpc�ŷ�ó.CodeName = "";
            this.bpc�ŷ�ó.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc�ŷ�ó.CodeValue = "";
            this.bpc�ŷ�ó.ComboCheck = true;
            this.bpc�ŷ�ó.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc�ŷ�ó.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc�ŷ�ó.Location = new System.Drawing.Point(120, 53);
            this.bpc�ŷ�ó.Name = "bpc�ŷ�ó";
            this.bpc�ŷ�ó.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�ŷ�ó.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�ŷ�ó.SearchCode = true;
            this.bpc�ŷ�ó.SelectCount = 0;
            this.bpc�ŷ�ó.SetDefaultValue = true;
            this.bpc�ŷ�ó.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�ŷ�ó.Size = new System.Drawing.Size(268, 21);
            this.bpc�ŷ�ó.TabIndex = 4;
            this.bpc�ŷ�ó.Tag = "CD_PARTNER;NM_PARTNER";
            this.bpc�ŷ�ó.Text = "bpCodeTextBox1";
            this.bpc�ŷ�ó.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc�����
            // 
            this.bpc�����.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc�����.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc�����.ButtonImage")));
            this.bpc�����.ChildMode = "";
            this.bpc�����.CodeName = "";
            this.bpc�����.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc�����.CodeValue = "";
            this.bpc�����.ComboCheck = true;
            this.bpc�����.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpc�����.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc�����.Location = new System.Drawing.Point(120, 103);
            this.bpc�����.Name = "bpc�����";
            this.bpc�����.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�����.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�����.SearchCode = true;
            this.bpc�����.SelectCount = 0;
            this.bpc�����.SetDefaultValue = true;
            this.bpc�����.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�����.Size = new System.Drawing.Size(268, 21);
            this.bpc�����.TabIndex = 8;
            this.bpc�����.Tag = "NO_EMP;NM_KOR";
            this.bpc�����.Text = "bpCodeTextBox1";
            this.bpc�����.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc�����
            // 
            this.bpc�����.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc�����.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc�����.ButtonImage")));
            this.bpc�����.ChildMode = "";
            this.bpc�����.CodeName = "";
            this.bpc�����.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc�����.CodeValue = "";
            this.bpc�����.ComboCheck = true;
            this.bpc�����.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
            this.bpc�����.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc�����.Location = new System.Drawing.Point(511, 78);
            this.bpc�����.Name = "bpc�����";
            this.bpc�����.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�����.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�����.SearchCode = true;
            this.bpc�����.SelectCount = 0;
            this.bpc�����.SetDefaultValue = true;
            this.bpc�����.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�����.Size = new System.Drawing.Size(268, 21);
            this.bpc�����.TabIndex = 7;
            this.bpc�����.Tag = "CD_BIZAREA";
            this.bpc�����.Text = "bpCodeTextBox1";
            this.bpc�����.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // cur���߷�
            // 
            this.cur���߷�.BackColor = System.Drawing.Color.White;
            this.cur���߷�.CurrencyDecimalDigits = 4;
            this.cur���߷�.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur���߷�.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur���߷�.Location = new System.Drawing.Point(510, 277);
            this.cur���߷�.Mask = null;
            this.cur���߷�.MaxLength = 22;
            this.cur���߷�.Name = "cur���߷�";
            this.cur���߷�.NullString = "0";
            this.cur���߷�.PositiveColor = System.Drawing.Color.Black;
            this.cur���߷�.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur���߷�.Size = new System.Drawing.Size(100, 21);
            this.cur���߷�.TabIndex = 23;
            this.cur���߷�.Tag = "NET_WEIGHT";
            this.cur���߷�.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur���߷�.UseKeyEnter = true;
            this.cur���߷�.UseKeyF3 = true;
            this.cur���߷�.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cur���߷�
            // 
            this.cur���߷�.BackColor = System.Drawing.Color.White;
            this.cur���߷�.CurrencyDecimalDigits = 4;
            this.cur���߷�.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur���߷�.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur���߷�.Location = new System.Drawing.Point(120, 277);
            this.cur���߷�.Mask = null;
            this.cur���߷�.MaxLength = 22;
            this.cur���߷�.Name = "cur���߷�";
            this.cur���߷�.NullString = "0";
            this.cur���߷�.PositiveColor = System.Drawing.Color.Black;
            this.cur���߷�.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur���߷�.Size = new System.Drawing.Size(100, 21);
            this.cur���߷�.TabIndex = 22;
            this.cur���߷�.Tag = "GROSS_WEIGHT";
            this.cur���߷�.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur���߷�.UseKeyEnter = true;
            this.cur���߷�.UseKeyF3 = true;
            this.cur���߷�.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cur��ȭ�ݾ�
            // 
            this.cur��ȭ�ݾ�.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cur��ȭ�ݾ�.CurrencyDecimalDigits = 4;
            this.cur��ȭ�ݾ�.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur��ȭ�ݾ�.Enabled = false;
            this.cur��ȭ�ݾ�.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur��ȭ�ݾ�.Location = new System.Drawing.Point(510, 153);
            this.cur��ȭ�ݾ�.Mask = null;
            this.cur��ȭ�ݾ�.MaxLength = 22;
            this.cur��ȭ�ݾ�.Name = "cur��ȭ�ݾ�";
            this.cur��ȭ�ݾ�.NullString = "0";
            this.cur��ȭ�ݾ�.PositiveColor = System.Drawing.Color.Black;
            this.cur��ȭ�ݾ�.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur��ȭ�ݾ�.Size = new System.Drawing.Size(150, 21);
            this.cur��ȭ�ݾ�.TabIndex = 17;
            this.cur��ȭ�ݾ�.Tag = "AM_EX";
            this.cur��ȭ�ݾ�.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur��ȭ�ݾ�.UseKeyEnter = true;
            this.cur��ȭ�ݾ�.UseKeyF3 = true;
            this.cur��ȭ�ݾ�.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cbo������
            // 
            this.cbo������.AutoDropDown = true;
            this.cbo������.BackColor = System.Drawing.Color.White;
            this.cbo������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo������.Location = new System.Drawing.Point(120, 178);
            this.cbo������.Name = "cbo������";
            this.cbo������.ShowCheckBox = false;
            this.cbo������.Size = new System.Drawing.Size(268, 20);
            this.cbo������.TabIndex = 14;
            this.cbo������.Tag = "CD_ORIGIN";
            this.cbo������.UseKeyEnter = true;
            this.cbo������.UseKeyF3 = true;
            this.cbo������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt�ε�����
            // 
            this.txt�ε�����.BackColor = System.Drawing.Color.White;
            this.txt�ε�����.Location = new System.Drawing.Point(510, 377);
            this.txt�ε�����.MaxLength = 50;
            this.txt�ε�����.Name = "txt�ε�����";
            this.txt�ε�����.SelectedAllEnabled = false;
            this.txt�ε�����.Size = new System.Drawing.Size(268, 21);
            this.txt�ε�����.TabIndex = 31;
            this.txt�ε�����.Tag = "COND_TRANS";
            this.txt�ε�����.UseKeyEnter = true;
            this.txt�ε�����.UseKeyF3 = true;
            this.txt�ε�����.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt����������
            // 
            this.txt����������.BackColor = System.Drawing.Color.White;
            this.txt����������.Location = new System.Drawing.Point(510, 352);
            this.txt����������.MaxLength = 50;
            this.txt����������.Name = "txt����������";
            this.txt����������.SelectedAllEnabled = false;
            this.txt����������.Size = new System.Drawing.Size(268, 21);
            this.txt����������.TabIndex = 29;
            this.txt����������.Tag = "DESTINATION";
            this.txt����������.UseKeyEnter = true;
            this.txt����������.UseKeyF3 = true;
            this.txt����������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt������
            // 
            this.txt������.BackColor = System.Drawing.Color.White;
            this.txt������.Location = new System.Drawing.Point(510, 327);
            this.txt������.MaxLength = 50;
            this.txt������.Name = "txt������";
            this.txt������.SelectedAllEnabled = false;
            this.txt������.Size = new System.Drawing.Size(268, 21);
            this.txt������.TabIndex = 27;
            this.txt������.Tag = "PORT_LOADING";
            this.txt������.UseKeyEnter = true;
            this.txt������.UseKeyF3 = true;
            this.txt������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt����CT��ȣ
            // 
            this.txt����CT��ȣ.BackColor = System.Drawing.Color.White;
            this.txt����CT��ȣ.Location = new System.Drawing.Point(510, 302);
            this.txt����CT��ȣ.MaxLength = 10;
            this.txt����CT��ȣ.Name = "txt����CT��ȣ";
            this.txt����CT��ȣ.SelectedAllEnabled = false;
            this.txt����CT��ȣ.Size = new System.Drawing.Size(150, 21);
            this.txt����CT��ȣ.TabIndex = 25;
            this.txt����CT��ȣ.Tag = "NO_ECT";
            this.txt����CT��ȣ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt����CT��ȣ.UseKeyEnter = true;
            this.txt����CT��ȣ.UseKeyF3 = true;
            this.txt����CT��ȣ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cbo�߷�����
            // 
            this.cbo�߷�����.AutoDropDown = true;
            this.cbo�߷�����.BackColor = System.Drawing.Color.White;
            this.cbo�߷�����.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo�߷�����.Location = new System.Drawing.Point(510, 253);
            this.cbo�߷�����.Name = "cbo�߷�����";
            this.cbo�߷�����.ShowCheckBox = false;
            this.cbo�߷�����.Size = new System.Drawing.Size(268, 20);
            this.cbo�߷�����.TabIndex = 21;
            this.cbo�߷�����.Tag = "CD_WEIGHT";
            this.cbo�߷�����.UseKeyEnter = true;
            this.cbo�߷�����.UseKeyF3 = true;
            this.cbo�߷�����.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo��۹��
            // 
            this.cbo��۹��.AutoDropDown = true;
            this.cbo��۹��.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo��۹��.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��۹��.Location = new System.Drawing.Point(510, 229);
            this.cbo��۹��.Name = "cbo��۹��";
            this.cbo��۹��.ShowCheckBox = false;
            this.cbo��۹��.Size = new System.Drawing.Size(268, 20);
            this.cbo��۹��.TabIndex = 19;
            this.cbo��۹��.Tag = "TP_TRANS";
            this.cbo��۹��.UseKeyEnter = true;
            this.cbo��۹��.UseKeyF3 = true;
            this.cbo��۹��.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo��ȭ
            // 
            this.cbo��ȭ.AutoDropDown = true;
            this.cbo��ȭ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo��ȭ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��ȭ.Location = new System.Drawing.Point(120, 153);
            this.cbo��ȭ.Name = "cbo��ȭ";
            this.cbo��ȭ.ShowCheckBox = false;
            this.cbo��ȭ.Size = new System.Drawing.Size(268, 20);
            this.cbo��ȭ.TabIndex = 12;
            this.cbo��ȭ.Tag = "CD_EXCH";
            this.cbo��ȭ.UseKeyEnter = true;
            this.cbo��ȭ.UseKeyF3 = true;
            this.cbo��ȭ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt���1
            // 
            this.txt���1.BackColor = System.Drawing.Color.White;
            this.txt���1.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���1.Location = new System.Drawing.Point(120, 401);
            this.txt���1.MaxLength = 100;
            this.txt���1.Name = "txt���1";
            this.txt���1.SelectedAllEnabled = false;
            this.txt���1.Size = new System.Drawing.Size(660, 21);
            this.txt���1.TabIndex = 32;
            this.txt���1.Tag = "REMARK1";
            this.txt���1.UseKeyEnter = true;
            this.txt���1.UseKeyF3 = true;
            this.txt���1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt���5
            // 
            this.txt���5.BackColor = System.Drawing.Color.White;
            this.txt���5.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���5.Location = new System.Drawing.Point(120, 500);
            this.txt���5.MaxLength = 100;
            this.txt���5.Name = "txt���5";
            this.txt���5.SelectedAllEnabled = false;
            this.txt���5.Size = new System.Drawing.Size(660, 21);
            this.txt���5.TabIndex = 36;
            this.txt���5.Tag = "REMARK5";
            this.txt���5.UseKeyEnter = true;
            this.txt���5.UseKeyF3 = true;
            this.txt���5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt���4
            // 
            this.txt���4.BackColor = System.Drawing.Color.White;
            this.txt���4.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���4.Location = new System.Drawing.Point(120, 476);
            this.txt���4.MaxLength = 100;
            this.txt���4.Name = "txt���4";
            this.txt���4.SelectedAllEnabled = false;
            this.txt���4.Size = new System.Drawing.Size(660, 21);
            this.txt���4.TabIndex = 35;
            this.txt���4.Tag = "REMARK4";
            this.txt���4.UseKeyEnter = true;
            this.txt���4.UseKeyF3 = true;
            this.txt���4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt���2
            // 
            this.txt���2.BackColor = System.Drawing.Color.White;
            this.txt���2.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���2.Location = new System.Drawing.Point(120, 426);
            this.txt���2.MaxLength = 100;
            this.txt���2.Name = "txt���2";
            this.txt���2.SelectedAllEnabled = false;
            this.txt���2.Size = new System.Drawing.Size(660, 21);
            this.txt���2.TabIndex = 33;
            this.txt���2.Tag = "REMARK2";
            this.txt���2.UseKeyEnter = true;
            this.txt���2.UseKeyF3 = true;
            this.txt���2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt���3
            // 
            this.txt���3.BackColor = System.Drawing.Color.White;
            this.txt���3.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���3.Location = new System.Drawing.Point(120, 451);
            this.txt���3.MaxLength = 100;
            this.txt���3.Name = "txt���3";
            this.txt���3.SelectedAllEnabled = false;
            this.txt���3.Size = new System.Drawing.Size(660, 21);
            this.txt���3.TabIndex = 34;
            this.txt���3.Tag = "REMARK3";
            this.txt���3.UseKeyEnter = true;
            this.txt���3.UseKeyF3 = true;
            this.txt���3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel12.Location = new System.Drawing.Point(116, 497);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(664, 1);
            this.panel12.TabIndex = 140;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel7.Location = new System.Drawing.Point(116, 473);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(664, 1);
            this.panel7.TabIndex = 139;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel8.Location = new System.Drawing.Point(116, 448);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(664, 1);
            this.panel8.TabIndex = 138;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel9.Location = new System.Drawing.Point(116, 423);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(664, 1);
            this.panel9.TabIndex = 137;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.Location = new System.Drawing.Point(5, 399);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(775, 1);
            this.panel11.TabIndex = 135;
            // 
            // txt������
            // 
            this.txt������.BackColor = System.Drawing.Color.White;
            this.txt������.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt������.Location = new System.Drawing.Point(120, 352);
            this.txt������.MaxLength = 50;
            this.txt������.Name = "txt������";
            this.txt������.SelectedAllEnabled = false;
            this.txt������.Size = new System.Drawing.Size(268, 21);
            this.txt������.TabIndex = 28;
            this.txt������.Tag = "PORT_ARRIVER";
            this.txt������.UseKeyEnter = true;
            this.txt������.UseKeyF3 = true;
            this.txt������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel28
            // 
            this.panel28.BackColor = System.Drawing.Color.Transparent;
            this.panel28.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel28.BackgroundImage")));
            this.panel28.Location = new System.Drawing.Point(5, 374);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(775, 1);
            this.panel28.TabIndex = 63;
            // 
            // panel29
            // 
            this.panel29.BackColor = System.Drawing.Color.Transparent;
            this.panel29.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel29.BackgroundImage")));
            this.panel29.Location = new System.Drawing.Point(5, 349);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(775, 1);
            this.panel29.TabIndex = 62;
            // 
            // panel25
            // 
            this.panel25.BackColor = System.Drawing.Color.Transparent;
            this.panel25.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel25.BackgroundImage")));
            this.panel25.Location = new System.Drawing.Point(5, 324);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(775, 1);
            this.panel25.TabIndex = 61;
            // 
            // panel26
            // 
            this.panel26.BackColor = System.Drawing.Color.Transparent;
            this.panel26.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel26.BackgroundImage")));
            this.panel26.Location = new System.Drawing.Point(5, 299);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(775, 1);
            this.panel26.TabIndex = 60;
            // 
            // panel27
            // 
            this.panel27.BackColor = System.Drawing.Color.Transparent;
            this.panel27.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel27.BackgroundImage")));
            this.panel27.Location = new System.Drawing.Point(5, 274);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(775, 1);
            this.panel27.TabIndex = 59;
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.Transparent;
            this.panel22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel22.BackgroundImage")));
            this.panel22.Location = new System.Drawing.Point(5, 250);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(775, 1);
            this.panel22.TabIndex = 58;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.Transparent;
            this.panel23.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel23.BackgroundImage")));
            this.panel23.Location = new System.Drawing.Point(5, 225);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(775, 1);
            this.panel23.TabIndex = 57;
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.Transparent;
            this.panel24.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel24.BackgroundImage")));
            this.panel24.Location = new System.Drawing.Point(5, 200);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(775, 1);
            this.panel24.TabIndex = 56;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel19.BackgroundImage")));
            this.panel19.Location = new System.Drawing.Point(5, 175);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(775, 1);
            this.panel19.TabIndex = 55;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Transparent;
            this.panel20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel20.BackgroundImage")));
            this.panel20.Location = new System.Drawing.Point(5, 150);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(775, 1);
            this.panel20.TabIndex = 54;
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.Transparent;
            this.panel21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel21.BackgroundImage")));
            this.panel21.Location = new System.Drawing.Point(5, 125);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(775, 1);
            this.panel21.TabIndex = 53;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Transparent;
            this.panel17.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel17.BackgroundImage")));
            this.panel17.Location = new System.Drawing.Point(5, 100);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(775, 1);
            this.panel17.TabIndex = 52;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel18.BackgroundImage")));
            this.panel18.Location = new System.Drawing.Point(5, 75);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(775, 1);
            this.panel18.TabIndex = 51;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Transparent;
            this.panel16.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel16.BackgroundImage")));
            this.panel16.Location = new System.Drawing.Point(5, 50);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(775, 1);
            this.panel16.TabIndex = 50;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel15.BackgroundImage")));
            this.panel15.Location = new System.Drawing.Point(5, 25);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(775, 1);
            this.panel15.TabIndex = 49;
            // 
            // txtVESSEL��
            // 
            this.txtVESSEL��.BackColor = System.Drawing.Color.White;
            this.txtVESSEL��.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVESSEL��.Location = new System.Drawing.Point(120, 377);
            this.txtVESSEL��.MaxLength = 50;
            this.txtVESSEL��.Name = "txtVESSEL��";
            this.txtVESSEL��.SelectedAllEnabled = false;
            this.txtVESSEL��.Size = new System.Drawing.Size(268, 21);
            this.txtVESSEL��.TabIndex = 30;
            this.txtVESSEL��.Tag = "NM_VESSEL";
            this.txtVESSEL��.UseKeyEnter = true;
            this.txtVESSEL��.UseKeyF3 = true;
            this.txtVESSEL��.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt����CT��ȣ
            // 
            this.txt����CT��ȣ.BackColor = System.Drawing.Color.White;
            this.txt����CT��ȣ.Location = new System.Drawing.Point(120, 302);
            this.txt����CT��ȣ.MaxLength = 10;
            this.txt����CT��ȣ.Name = "txt����CT��ȣ";
            this.txt����CT��ȣ.SelectedAllEnabled = false;
            this.txt����CT��ȣ.Size = new System.Drawing.Size(150, 21);
            this.txt����CT��ȣ.TabIndex = 24;
            this.txt����CT��ȣ.Tag = "NO_SCT";
            this.txt����CT��ȣ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt����CT��ȣ.UseKeyEnter = true;
            this.txt����CT��ȣ.UseKeyF3 = true;
            this.txt����CT��ȣ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cbo��������
            // 
            this.cbo��������.AutoDropDown = true;
            this.cbo��������.BackColor = System.Drawing.Color.White;
            this.cbo��������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��������.Location = new System.Drawing.Point(120, 253);
            this.cbo��������.Name = "cbo��������";
            this.cbo��������.ShowCheckBox = false;
            this.cbo��������.Size = new System.Drawing.Size(268, 20);
            this.cbo��������.TabIndex = 20;
            this.cbo��������.Tag = "TP_PACKING";
            this.cbo��������.UseKeyEnter = true;
            this.cbo��������.UseKeyF3 = true;
            this.cbo��������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo�������
            // 
            this.cbo�������.AutoDropDown = true;
            this.cbo�������.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo�������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo�������.Location = new System.Drawing.Point(120, 228);
            this.cbo�������.Name = "cbo�������";
            this.cbo�������.ShowCheckBox = false;
            this.cbo�������.Size = new System.Drawing.Size(268, 20);
            this.cbo�������.TabIndex = 18;
            this.cbo�������.Tag = "TP_TRANSPORT";
            this.cbo�������.UseKeyEnter = true;
            this.cbo�������.UseKeyF3 = true;
            this.cbo�������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo�ŷ�����
            // 
            this.cbo�ŷ�����.AutoDropDown = true;
            this.cbo�ŷ�����.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo�ŷ�����.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo�ŷ�����.Location = new System.Drawing.Point(510, 54);
            this.cbo�ŷ�����.Name = "cbo�ŷ�����";
            this.cbo�ŷ�����.ShowCheckBox = false;
            this.cbo�ŷ�����.Size = new System.Drawing.Size(268, 20);
            this.cbo�ŷ�����.TabIndex = 5;
            this.cbo�ŷ�����.Tag = "FG_LC";
            this.cbo�ŷ�����.UseKeyEnter = true;
            this.cbo�ŷ�����.UseKeyF3 = true;
            this.cbo�ŷ�����.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lbl�ε�����);
            this.panel6.Controls.Add(this.lbl����������);
            this.panel6.Controls.Add(this.lbl������);
            this.panel6.Controls.Add(this.lbl��ȭ�ݾ�);
            this.panel6.Controls.Add(this.lbl����CT��ȣ);
            this.panel6.Controls.Add(this.lbl���߷�);
            this.panel6.Controls.Add(this.lbl�߷�����);
            this.panel6.Controls.Add(this.lbl��۹��);
            this.panel6.Controls.Add(this.lbl���������);
            this.panel6.Controls.Add(this.lbl����������);
            this.panel6.Controls.Add(this.lbl������);
            this.panel6.Controls.Add(this.lbl������);
            this.panel6.Controls.Add(this.lbl�����);
            this.panel6.Controls.Add(this.lbl����SO��ȣ);
            this.panel6.Controls.Add(this.lbl�����ſ����ȣ);
            this.panel6.Controls.Add(this.lbl�ŷ�����);
            this.panel6.Location = new System.Drawing.Point(391, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(115, 399);
            this.panel6.TabIndex = 4;
            // 
            // lbl�ε�����
            // 
            this.lbl�ε�����.Location = new System.Drawing.Point(3, 379);
            this.lbl�ε�����.Name = "lbl�ε�����";
            this.lbl�ε�����.Resizeble = true;
            this.lbl�ε�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�ε�����.TabIndex = 21;
            this.lbl�ε�����.Tag = "COND_TRANS";
            this.lbl�ε�����.Text = "�ε�����";
            this.lbl�ε�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����������
            // 
            this.lbl����������.Location = new System.Drawing.Point(3, 355);
            this.lbl����������.Name = "lbl����������";
            this.lbl����������.Resizeble = true;
            this.lbl����������.Size = new System.Drawing.Size(110, 18);
            this.lbl����������.TabIndex = 20;
            this.lbl����������.Tag = "DESTINATION";
            this.lbl����������.Text = "����������";
            this.lbl����������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 329);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 19;
            this.lbl������.Tag = "SHIP_PORT";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��ȭ�ݾ�
            // 
            this.lbl��ȭ�ݾ�.Location = new System.Drawing.Point(3, 155);
            this.lbl��ȭ�ݾ�.Name = "lbl��ȭ�ݾ�";
            this.lbl��ȭ�ݾ�.Resizeble = true;
            this.lbl��ȭ�ݾ�.Size = new System.Drawing.Size(110, 18);
            this.lbl��ȭ�ݾ�.TabIndex = 12;
            this.lbl��ȭ�ݾ�.Tag = "AMT_EX";
            this.lbl��ȭ�ݾ�.Text = "��ȭ�ݾ�";
            this.lbl��ȭ�ݾ�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����CT��ȣ
            // 
            this.lbl����CT��ȣ.Location = new System.Drawing.Point(3, 304);
            this.lbl����CT��ȣ.Name = "lbl����CT��ȣ";
            this.lbl����CT��ȣ.Resizeble = true;
            this.lbl����CT��ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl����CT��ȣ.TabIndex = 11;
            this.lbl����CT��ȣ.Tag = "NO_ECT";
            this.lbl����CT��ȣ.Text = "����C/T��ȣ";
            this.lbl����CT��ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl���߷�
            // 
            this.lbl���߷�.Location = new System.Drawing.Point(3, 279);
            this.lbl���߷�.Name = "lbl���߷�";
            this.lbl���߷�.Resizeble = true;
            this.lbl���߷�.Size = new System.Drawing.Size(110, 18);
            this.lbl���߷�.TabIndex = 10;
            this.lbl���߷�.Tag = "NET_WEIGHT";
            this.lbl���߷�.Text = "���߷�";
            this.lbl���߷�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�߷�����
            // 
            this.lbl�߷�����.Location = new System.Drawing.Point(3, 254);
            this.lbl�߷�����.Name = "lbl�߷�����";
            this.lbl�߷�����.Resizeble = true;
            this.lbl�߷�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�߷�����.TabIndex = 9;
            this.lbl�߷�����.Tag = "UNIT_WEIGHT";
            this.lbl�߷�����.Text = "�߷�����";
            this.lbl�߷�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��۹��
            // 
            this.lbl��۹��.Location = new System.Drawing.Point(3, 230);
            this.lbl��۹��.Name = "lbl��۹��";
            this.lbl��۹��.Resizeble = true;
            this.lbl��۹��.Size = new System.Drawing.Size(110, 18);
            this.lbl��۹��.TabIndex = 8;
            this.lbl��۹��.Tag = "CARRY";
            this.lbl��۹��.Text = "��۹��";
            this.lbl��۹��.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl���������
            // 
            this.lbl���������.Location = new System.Drawing.Point(3, 205);
            this.lbl���������.Name = "lbl���������";
            this.lbl���������.Resizeble = true;
            this.lbl���������.Size = new System.Drawing.Size(110, 18);
            this.lbl���������.TabIndex = 7;
            this.lbl���������.Tag = "DT_ONTO";
            this.lbl���������.Text = "���������";
            this.lbl���������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����������
            // 
            this.lbl����������.Location = new System.Drawing.Point(3, 180);
            this.lbl����������.Name = "lbl����������";
            this.lbl����������.Resizeble = true;
            this.lbl����������.Size = new System.Drawing.Size(110, 18);
            this.lbl����������.TabIndex = 6;
            this.lbl����������.Tag = "DT_ONSHIP";
            this.lbl����������.Text = "����������";
            this.lbl����������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 129);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 5;
            this.lbl������.Tag = "AGENT_TRAN";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 104);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 4;
            this.lbl������.Tag = "EXPORT_TRAN";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����
            // 
            this.lbl�����.Location = new System.Drawing.Point(3, 80);
            this.lbl�����.Name = "lbl�����";
            this.lbl�����.Resizeble = true;
            this.lbl�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�����.TabIndex = 3;
            this.lbl�����.Tag = "BALLOT";
            this.lbl�����.Text = "�����";
            this.lbl�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����SO��ȣ
            // 
            this.lbl����SO��ȣ.Location = new System.Drawing.Point(3, 30);
            this.lbl����SO��ȣ.Name = "lbl����SO��ȣ";
            this.lbl����SO��ȣ.Resizeble = true;
            this.lbl����SO��ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl����SO��ȣ.TabIndex = 2;
            this.lbl����SO��ȣ.Tag = "SO_REFER";
            this.lbl����SO��ȣ.Text = "����S/O��ȣ";
            this.lbl����SO��ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����ſ����ȣ
            // 
            this.lbl�����ſ����ȣ.Location = new System.Drawing.Point(3, 4);
            this.lbl�����ſ����ȣ.Name = "lbl�����ſ����ȣ";
            this.lbl�����ſ����ȣ.Resizeble = true;
            this.lbl�����ſ����ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl�����ſ����ȣ.TabIndex = 1;
            this.lbl�����ſ����ȣ.Tag = "LC_REFER";
            this.lbl�����ſ����ȣ.Text = "�����ſ��� ��ȣ";
            this.lbl�����ſ����ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�ŷ�����
            // 
            this.lbl�ŷ�����.Location = new System.Drawing.Point(3, 55);
            this.lbl�ŷ�����.Name = "lbl�ŷ�����";
            this.lbl�ŷ�����.Resizeble = true;
            this.lbl�ŷ�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�ŷ�����.TabIndex = 1;
            this.lbl�ŷ�����.Tag = "FG_TRANS";
            this.lbl�ŷ�����.Text = "�ŷ�����";
            this.lbl�ŷ�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt�����ȣ
            // 
            this.txt�����ȣ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.txt�����ȣ.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt�����ȣ.Location = new System.Drawing.Point(120, 2);
            this.txt�����ȣ.MaxLength = 20;
            this.txt�����ȣ.Name = "txt�����ȣ";
            this.txt�����ȣ.SelectedAllEnabled = false;
            this.txt�����ȣ.Size = new System.Drawing.Size(150, 21);
            this.txt�����ȣ.TabIndex = 0;
            this.txt�����ȣ.Tag = "NO_INV";
            this.txt�����ȣ.UseKeyEnter = true;
            this.txt�����ȣ.UseKeyF3 = true;
            this.txt�����ȣ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lbl��ȭ);
            this.panel5.Controls.Add(this.lbl���);
            this.panel5.Controls.Add(this.lbl����);
            this.panel5.Controls.Add(this.lblVESSEL��);
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lbl��������ó);
            this.panel5.Controls.Add(this.lbl����CT��ȣ);
            this.panel5.Controls.Add(this.lbl���߷�);
            this.panel5.Controls.Add(this.lbl��������);
            this.panel5.Controls.Add(this.lbl�������);
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lbl�����);
            this.panel5.Controls.Add(this.lbl�����׷�);
            this.panel5.Controls.Add(this.lbl�ŷ�ó);
            this.panel5.Controls.Add(this.lbl�����ȣ);
            this.panel5.Controls.Add(this.lbl��������);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(115, 520);
            this.panel5.TabIndex = 0;
            // 
            // lbl��ȭ
            // 
            this.lbl��ȭ.Location = new System.Drawing.Point(3, 155);
            this.lbl��ȭ.Name = "lbl��ȭ";
            this.lbl��ȭ.Resizeble = true;
            this.lbl��ȭ.Size = new System.Drawing.Size(110, 18);
            this.lbl��ȭ.TabIndex = 21;
            this.lbl��ȭ.Tag = "CD_CURRENCY";
            this.lbl��ȭ.Text = "��ȭ";
            this.lbl��ȭ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl���
            // 
            this.lbl���.Location = new System.Drawing.Point(3, 403);
            this.lbl���.Name = "lbl���";
            this.lbl���.Resizeble = true;
            this.lbl���.Size = new System.Drawing.Size(110, 18);
            this.lbl���.TabIndex = 20;
            this.lbl���.Tag = "REMARK";
            this.lbl���.Text = "���";
            this.lbl���.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����
            // 
            this.lbl����.Location = new System.Drawing.Point(3, 204);
            this.lbl����.Name = "lbl����";
            this.lbl����.Resizeble = true;
            this.lbl����.Size = new System.Drawing.Size(110, 18);
            this.lbl����.TabIndex = 17;
            this.lbl����.Tag = "SHIP_CORP";
            this.lbl����.Text = "����";
            this.lbl����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVESSEL��
            // 
            this.lblVESSEL��.Location = new System.Drawing.Point(3, 378);
            this.lblVESSEL��.Name = "lblVESSEL��";
            this.lblVESSEL��.Resizeble = true;
            this.lblVESSEL��.Size = new System.Drawing.Size(110, 18);
            this.lblVESSEL��.TabIndex = 16;
            this.lblVESSEL��.Tag = "VESSEL";
            this.lblVESSEL��.Text = "VESSEL��";
            this.lblVESSEL��.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 354);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 14;
            this.lbl������.Tag = "ARRIVAL_PORT";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������ó
            // 
            this.lbl��������ó.Location = new System.Drawing.Point(3, 329);
            this.lbl��������ó.Name = "lbl��������ó";
            this.lbl��������ó.Resizeble = true;
            this.lbl��������ó.Size = new System.Drawing.Size(110, 18);
            this.lbl��������ó.TabIndex = 13;
            this.lbl��������ó.Tag = "CD_ARTRAN";
            this.lbl��������ó.Text = "��������ó";
            this.lbl��������ó.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����CT��ȣ
            // 
            this.lbl����CT��ȣ.Location = new System.Drawing.Point(3, 304);
            this.lbl����CT��ȣ.Name = "lbl����CT��ȣ";
            this.lbl����CT��ȣ.Resizeble = true;
            this.lbl����CT��ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl����CT��ȣ.TabIndex = 11;
            this.lbl����CT��ȣ.Tag = "NO_SCT";
            this.lbl����CT��ȣ.Text = "����C/T��ȣ";
            this.lbl����CT��ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl���߷�
            // 
            this.lbl���߷�.Location = new System.Drawing.Point(3, 278);
            this.lbl���߷�.Name = "lbl���߷�";
            this.lbl���߷�.Resizeble = true;
            this.lbl���߷�.Size = new System.Drawing.Size(110, 18);
            this.lbl���߷�.TabIndex = 10;
            this.lbl���߷�.Tag = "GROSS_WEIGHT";
            this.lbl���߷�.Text = "���߷�";
            this.lbl���߷�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������
            // 
            this.lbl��������.Location = new System.Drawing.Point(3, 253);
            this.lbl��������.Name = "lbl��������";
            this.lbl��������.Resizeble = true;
            this.lbl��������.Size = new System.Drawing.Size(110, 18);
            this.lbl��������.TabIndex = 9;
            this.lbl��������.Tag = "NM_PACKING";
            this.lbl��������.Text = "��������";
            this.lbl��������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�������
            // 
            this.lbl�������.Location = new System.Drawing.Point(3, 229);
            this.lbl�������.Name = "lbl�������";
            this.lbl�������.Resizeble = true;
            this.lbl�������.Size = new System.Drawing.Size(110, 18);
            this.lbl�������.TabIndex = 8;
            this.lbl�������.Tag = "TRANS_TYPE";
            this.lbl�������.Text = "�������";
            this.lbl�������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 179);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 6;
            this.lbl������.Tag = "CO_NATION";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 130);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 5;
            this.lbl������.Tag = "PRODUCT_TRAN";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����
            // 
            this.lbl�����.Location = new System.Drawing.Point(3, 104);
            this.lbl�����.Name = "lbl�����";
            this.lbl�����.Resizeble = true;
            this.lbl�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�����.TabIndex = 4;
            this.lbl�����.Tag = "NM_EMP";
            this.lbl�����.Text = "�����";
            this.lbl�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����׷�
            // 
            this.lbl�����׷�.Location = new System.Drawing.Point(3, 79);
            this.lbl�����׷�.Name = "lbl�����׷�";
            this.lbl�����׷�.Resizeble = true;
            this.lbl�����׷�.Size = new System.Drawing.Size(110, 18);
            this.lbl�����׷�.TabIndex = 3;
            this.lbl�����׷�.Tag = "GROUP_ISUL";
            this.lbl�����׷�.Text = "�����׷�";
            this.lbl�����׷�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�ŷ�ó
            // 
            this.lbl�ŷ�ó.Location = new System.Drawing.Point(3, 55);
            this.lbl�ŷ�ó.Name = "lbl�ŷ�ó";
            this.lbl�ŷ�ó.Resizeble = true;
            this.lbl�ŷ�ó.Size = new System.Drawing.Size(110, 18);
            this.lbl�ŷ�ó.TabIndex = 2;
            this.lbl�ŷ�ó.Tag = "CD_TRANS";
            this.lbl�ŷ�ó.Text = "�ŷ�ó";
            this.lbl�ŷ�ó.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����ȣ
            // 
            this.lbl�����ȣ.Location = new System.Drawing.Point(3, 4);
            this.lbl�����ȣ.Name = "lbl�����ȣ";
            this.lbl�����ȣ.Resizeble = true;
            this.lbl�����ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl�����ȣ.TabIndex = 0;
            this.lbl�����ȣ.Tag = "NO_INV";
            this.lbl�����ȣ.Text = "�����ȣ";
            this.lbl�����ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������
            // 
            this.lbl��������.Location = new System.Drawing.Point(3, 30);
            this.lbl��������.Name = "lbl��������";
            this.lbl��������.Resizeble = true;
            this.lbl��������.Size = new System.Drawing.Size(110, 18);
            this.lbl��������.TabIndex = 0;
            this.lbl��������.Tag = "DT_ISSUE";
            this.lbl��������.Text = "��������";
            this.lbl��������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn�������
            // 
            this.btn�������.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn�������.BackColor = System.Drawing.Color.White;
            this.btn�������.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn�������.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn�������.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn�������.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn�������.Location = new System.Drawing.Point(685, 0);
            this.btn�������.Name = "btn�������";
            this.btn�������.Size = new System.Drawing.Size(100, 24);
            this.btn�������.TabIndex = 126;
            this.btn�������.TabStop = false;
            this.btn�������.Tag = "MOD_TEXT";
            this.btn�������.Text = "�������";
            this.btn�������.UseVisualStyleBackColor = false;
            this.btn�������.Click += new System.EventHandler(this.m_btnModText_Click);
            // 
            // btn�ǸŰ����
            // 
            this.btn�ǸŰ����.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn�ǸŰ����.BackColor = System.Drawing.Color.White;
            this.btn�ǸŰ����.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn�ǸŰ����.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn�ǸŰ����.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn�ǸŰ����.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn�ǸŰ����.Location = new System.Drawing.Point(563, 0);
            this.btn�ǸŰ����.Name = "btn�ǸŰ����";
            this.btn�ǸŰ����.Size = new System.Drawing.Size(120, 24);
            this.btn�ǸŰ����.TabIndex = 127;
            this.btn�ǸŰ����.TabStop = false;
            this.btn�ǸŰ����.Tag = "INPUT_COST";
            this.btn�ǸŰ����.Text = "�ǸŰ����";
            this.btn�ǸŰ����.UseVisualStyleBackColor = false;
            this.btn�ǸŰ����.Click += new System.EventHandler(this.m_btnInputCost_Click);
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn�ǸŰ����);
            this.panelExt1.Controls.Add(this.btn�������);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 3);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(787, 24);
            this.panelExt1.TabIndex = 128;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 129;
            // 
            // P_TR_EXINV
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_TR_EXINV";
            this.TitleText = "������";
            this.Load += new System.EventHandler(this.P_TR_EXINV_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_TR_EXINV_Paint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp����������)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp���������)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp��������)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur���߷�)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur���߷�)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ȭ�ݾ�)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.BindingManagerBase m_Manager;

		private string m_Today;
		private DataTable m_HeadTable;
		private DataTable m_CopyTable;

		// �ŷ�ó �ڵ� & ��Ī ����
		private string m_CdPartner = "";
		private string m_NmPartner = "";

		// �����׷� �ڵ� & ��Ī ����
		private string m_CdGroup = "";
		private string m_NmGroup = "";

		// ����� �ڵ� & ��Ī ����
		private string m_CdEmp = "";
		private string m_NmEmp = "";

		// ����� �ڵ� ����
		private string m_CdBizarea;

		// ������ �ڵ� & ��Ī ����
		private string m_CdExport = "";
		private string m_NmExport = "";

		// ������ �ڵ� & ��Ī ����
		private string m_CdPRODUCT = "";
		private string m_NmPRODUCT = "";

		// ������ �ڵ� & ��Ī ����
		private string m_CdAgent = "";
		private string m_NmAgent = "";

		// ���� �ڵ� & ��Ī ����
		private string m_CdShipCorp = "";
		private string m_NmShipCorp = "";

		// ���� ����ó
		private string m_CdCustIn = "";
		private string m_NmCustIn = "";

		private bool m_IsPageActivated = false;
		private bool m_IsSave = true;

		#region �� �ʱ�ȭ �κ�

		/// <summary>
		/// ������
		/// </summary>
		public P_TR_EXINV_BAK()
		{
			try
			{

				InitializeComponent();

				// Currency Box ����
				this.cur��ȭ�ݾ�.NumberFormatInfoObject.NumberDecimalDigits = 4;
				this.cur���߷�.NumberFormatInfoObject.NumberDecimalDigits = 4;
				this.cur���߷�.NumberFormatInfoObject.NumberDecimalDigits = 4;

				this.cur��ȭ�ݾ�.Text = "0";
				this.cur���߷�.Text = "0";
				this.cur���߷�.Text = "0";

			}
			catch(Exception ex)
			{

				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		/// �� �ε��� DD������ �Ѵ�.
		/// �˻���� �˻��� �Ѵ�.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void P_TR_EXINV_Load(object sender, EventArgs e)
		{
			Show();

			try
			{
				// DD�� �ʱ�ȭ
				this.InitDD();

				// ��¥ �ʱ�ȭ
				this.InitDate();

				// Control �� Ȱ��ȭ
				SetControlDisable();
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}


		/// <summary>
		/// Paint  �̺�Ʈ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void P_TR_EXINV_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				this.Paint -= new System.Windows.Forms.PaintEventHandler(this.P_TR_EXINV_Paint);

				Application.DoEvents();
				// �޺� �ڽ� �ʱ�ȭ
				this.InitComboBox();			

				// �ʱ� ���̺� Setting.
				this.SelectInit();
				
				// �ʱ� ���̺� �߰� �� ���ε�
				this.Append();

				// ��ư ����
				this.SetButton();

			}
			catch(System.Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}



		/// <summary>
		/// ��ư �ʱ�ȭ
		/// </summary>
		private void SetButton()
		{
			if(this.CanSearch)
				this.ToolBarSearchButtonEnabled = true;
			else
				this.ToolBarSearchButtonEnabled = false;

			if(this.CanAdd)
				this.ToolBarAddButtonEnabled = true;
			else
				this.ToolBarAddButtonEnabled = false;

			if(this.CanDelete)
				this.ToolBarDeleteButtonEnabled = true;
			else
				this.ToolBarDeleteButtonEnabled = false;

			if(this.CanSave)
				this.ToolBarSaveButtonEnabled = true;
			else
				this.ToolBarSaveButtonEnabled = false;

			if(this.CanPrint)
				this.ToolBarPrintButtonEnabled = true;
			else
				this.ToolBarPrintButtonEnabled = false;           
		}

		/// <summary>
		/// DD ����
		/// </summary>
		private void InitDD()
		{
			foreach(Control ctr in this.Controls)
			{
				if(ctr is PanelExt)
				{
					foreach(Control ctrl in ((PanelExt)ctr).Controls)
					{
						if(ctrl is PanelExt)
						{
							foreach(Control ctrls in ((PanelExt)ctrl).Controls)
							{
								if(ctrls is LabelExt)
                                    ((LabelExt)ctrls).Text = this.MainFrameInterface.GetDataDictionaryItem("TR", (string)((LabelExt)ctrls).Tag);
							}
						}
					}
				}
			}

            btn�ǸŰ����.Text = this.MainFrameInterface.GetDataDictionaryItem("TR", (string)btn�ǸŰ����.Tag);
			btn�������.Text = this.MainFrameInterface.GetDataDictionaryItem("TR", (string)btn�������.Tag);
		}
		
		/// <summary>
		/// �˻� ��¥ �ؽ�Ʈ �ڽ� �ʱ�ȭ
		/// </summary>
		private void InitDate()
		{
			string ls_day = this.MainFrameInterface.GetStringToday;


            this.dtp��������.Text = ls_day;
            this.dtp����������.Text = ls_day;
            this.dtp���������.Text = ls_day;

			this.m_Today = ls_day;

		}

		/// <summary>
		/// �� ComboBox�� �����Ѵ�.
		/// </summary>
		private void InitComboBox()
		{
			//	* ������ Type ����
			//	1. N : ������� ����
			//	2. S : �����ִ� ����
			//	3. U : ����� ����

			string[] ls_args = new string[7];
			ls_args[0] = "N;TR_IM00005";// �ŷ�����
			ls_args[1] = "N;MA_B000005";// ��ȭ
			ls_args[2] = "N;TR_IM00009";// �������
			ls_args[3] = "S;MA_B000004";// �߷�����?????
			ls_args[4] = "N;TR_IM00008";// ��۹��
			ls_args[5] = "S;MA_B000020";// ������
			ls_args[6] = "S;TR_IM00011";// ��������

            DataSet lds_Combo = GetComboData(ls_args);

            // �ŷ�����
            this.cbo�ŷ�����.DataSource = lds_Combo.Tables[0];
            this.cbo�ŷ�����.DisplayMember = "NAME";
            this.cbo�ŷ�����.ValueMember = "CODE";

            // ��ȭ
            this.cbo��ȭ.DataSource = lds_Combo.Tables[1];
            this.cbo��ȭ.DisplayMember = "NAME";
            this.cbo��ȭ.ValueMember = "CODE";

            // �������
            this.cbo�������.DataSource = lds_Combo.Tables[2];
            this.cbo�������.DisplayMember = "NAME";
            this.cbo�������.ValueMember = "CODE";

            // �߷�����
            this.cbo�߷�����.DataSource = lds_Combo.Tables[3];
            this.cbo�߷�����.DisplayMember = "NAME";
            this.cbo�߷�����.ValueMember = "CODE";

            // ��۹��
            this.cbo��۹��.DataSource = lds_Combo.Tables[4];
            this.cbo��۹��.DisplayMember = "NAME";
            this.cbo��۹��.ValueMember = "CODE";

            // ������
            this.cbo������.DataSource = lds_Combo.Tables[5];
            this.cbo������.DisplayMember = "NAME";
            this.cbo������.ValueMember = "CODE";

            // ��������
            this.cbo��������.DataSource = lds_Combo.Tables[6];
            this.cbo��������.DisplayMember = "NAME";
            this.cbo��������.ValueMember = "CODE";

		}

		#endregion

		#region �� �Ϲ� �޼ҵ�

		/// <summary>
		/// �޾ƿ� Panel ���� Control���� ���¸� ������ �ش�.(��Ȱ������ �����Ѵ�.)
		/// </summary>
		/// <param name="ps_panel"></param>
		private void SetControlDisable()
		{
			foreach(Control ctr in this.Controls)
			{
                if (ctr is PanelExt)
				{
                    foreach (Control ctrl in ((PanelExt)ctr).Controls)
					{
                        if (ctrl is TextBoxExt)
						{
                            ((TextBoxExt)ctrl).ReadOnly = true;
						}
						if(ctrl is ButtonExt)
						{
                            ((ButtonExt)ctrl).Enabled = false;
						}
                        if (ctrl is DropDownComboBox)
						{
                            ((DropDownComboBox)ctrl).Enabled = false;
						}				
					}
				}
			}

			txt�����ȣ.ReadOnly = false;
			//this.m_btnSoRefer.Enabled = true;
			//this.m_btnLcRefer.Enabled = true;
		}

		/// <summary>
		/// Panel ���� ��Ʈ���� Ȱ������ �Ѵ�.
		/// </summary>
		/// <param name="ps_panel"></param>
		private void SetControlEnable()
		{
			foreach(Control ctr in this.Controls)
			{
                if (ctr is PanelExt)
				{
                    foreach (Control ctrl in ((PanelExt)ctr).Controls)
					{
						if(ctrl is TextBoxExt)
						{
                            ((TextBoxExt)ctrl).ReadOnly = false;
						}
						if(ctrl is ButtonExt)
						{
                            ((ButtonExt)ctrl).Enabled = true;
						}
                        if (ctrl is DropDownComboBox)
						{
                            ((DropDownComboBox)ctrl).Enabled = true;
						}		
					}
				}
			}

            //m_txtSoRefer.ReadOnly = true;
            //m_txtLcRefer.ReadOnly = true;
		}


		/// <summary>
		/// �޺��ڽ��� ó�� ���·� ������.
		/// </summary>
		private void ComboResetting()
		{
            this.cbo�ŷ�����.SelectedIndex = 0;
            this.cbo��ȭ.SelectedIndex = 0;
            this.cbo�������.SelectedIndex = 0;
            this.cbo�߷�����.SelectedIndex = 0;
            this.cbo��۹��.SelectedIndex = 0;
            this.cbo������.SelectedIndex = 0;
            this.cbo��������.SelectedIndex = 0;
		}

		#endregion

		#region �� ���̽� �� override Method

		/// <summary>
		/// ��ȸ ��ư
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);

				if(ldt_temp != null)
				{		
					// ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ�?
					// MA_M000073
					string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
					DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if(result == DialogResult.Yes)
					{
						// ���� �����͸� ������ �� ���ο� �����͸� �߰��� �� �ְ� �Ѵ�.
						if(this.Save())
						{
							// ���� ����
						}
						else
						{
							// ����� ���� �߻�
							return;
						}
					}
					else if(result == DialogResult.Cancel)
					{
						return;
					}
				}

                trade.P_TR_EXINVNO_SUB obj = new trade.P_TR_EXINVNO_SUB(this.MainFrameInterface);

                if (obj.ShowDialog() == DialogResult.OK)
                {
                    this.m_HeadTable = obj.GetResultTable;

                    Application.DoEvents();

                    this.SetBindingManager();

                    this.m_Manager.Position = 0;

                    // �ڵ�, �� ������ ���� ������ ����

                    // �ŷ�ó �ڵ� & ��Ī ����
                    this.m_CdPartner = this.m_HeadTable.Rows[0]["CD_PARTNER"].ToString();
                    this.m_NmPartner = this.m_HeadTable.Rows[0]["NM_PARTNER"].ToString();
                    this.bpc�ŷ�ó.CodeName = this.m_NmPartner;
                    this.bpc�ŷ�ó.CodeValue = this.m_CdPartner;


                    // �����׷� �ڵ� & ��Ī ����
                    this.m_CdGroup = this.m_HeadTable.Rows[0]["CD_SALEGRP"].ToString();
                    this.m_NmGroup = this.m_HeadTable.Rows[0]["NM_SALEGRP"].ToString();
                    this.bbpc�����׷�.CodeName = this.m_NmGroup;
                    this.bbpc�����׷�.CodeValue = this.m_CdGroup;

                    // ����� �ڵ� & ��Ī ����
                    this.m_CdEmp = this.m_HeadTable.Rows[0]["NO_EMP"].ToString();
                    this.m_NmEmp = this.m_HeadTable.Rows[0]["NM_KOR"].ToString();
                    this.bpc�����.CodeName = this.m_NmEmp;
                    this.bpc�����.CodeValue = this.m_CdEmp;

                    // ����� �ڵ� ����
                    this.m_CdBizarea = this.m_HeadTable.Rows[0]["CD_BIZAREA"].ToString();
                    this.bpc�����.CodeName = this.m_CdBizarea;

                    // ������ �ڵ� & ��Ī ����
                    this.m_CdExport = this.m_HeadTable.Rows[0]["CD_EXPORT"].ToString();
                    this.m_NmExport = this.m_HeadTable.Rows[0]["NM_EXPORT"].ToString();
                    this.bpc������.CodeName = this.m_NmExport;
                    this.bpc������.CodeValue = this.m_CdExport;

                    // ������ �ڵ� & ��Ī ����
                    this.m_CdPRODUCT = this.m_HeadTable.Rows[0]["CD_PRODUCT"].ToString();
                    this.m_NmPRODUCT = this.m_HeadTable.Rows[0]["NM_PRODUCT"].ToString();
                    this.bpc������.CodeName = this.m_NmPRODUCT;
                    this.bpc������.CodeValue = this.m_CdPRODUCT;

                    // ������ �ڵ� & ��Ī ����
                    this.m_CdAgent = this.m_HeadTable.Rows[0]["CD_AGENT"].ToString();
                    this.m_NmAgent = this.m_HeadTable.Rows[0]["NM_AGENT"].ToString();
                    this.bpc������.CodeName = this.m_NmAgent;
                    this.bpc������.CodeValue = this.m_CdAgent;

                    // ���� �ڵ� & ��Ī ����
                    this.m_CdShipCorp = this.m_HeadTable.Rows[0]["SHIP_CORP"].ToString();
                    this.m_NmShipCorp = this.m_HeadTable.Rows[0]["NM_SHIP_CORP"].ToString();
                    this.bpc����.CodeName = this.m_NmShipCorp;
                    this.bpc����.CodeValue = this.m_CdShipCorp;

                    // ���� ����ó
                    this.m_CdCustIn = this.m_HeadTable.Rows[0]["CD_CUST_IN"].ToString();
                    this.m_NmCustIn = this.m_HeadTable.Rows[0]["NM_CUST_IN"].ToString();
                    this.bpc��������ó.CodeName = this.m_NmCustIn;
                    this.bpc��������ó.CodeValue = this.m_CdCustIn;

                    this.m_IsPageActivated = true;
                    this.SetControlEnable();

                    this.txt�����ȣ.Enabled = false;

                    ////���� ���ο� ���� ���´�.(2003-08-14)
                    //if (m_txtSoRefer.Text == "")
                    //{
                    //    m_txtSoRefer.Enabled = false;
                    //    m_txtSoRefer.BackColor = System.Drawing.SystemColors.Control;
                    //    m_btnSoRefer.Enabled = false;
                    //    m_bpCD_EXPORT.Focus();
                    //}

                    //// (2003-08-14)
                    //if (m_txtLcRefer.Text == "")
                    //{
                    //    m_txtLcRefer.Enabled = false;
                    //    m_txtLcRefer.BackColor = System.Drawing.SystemColors.Control;
                    //    m_btnLcRefer.Enabled = false;
                    //    m_bpCD_EXPORT.Focus();
                    //}

                    this.m_HeadTable.AcceptChanges();
                }
                obj.Dispose();
            }
            catch (Exception ex)
            {
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
            }
		}

		/// <summary>
		/// �߰� ��ư
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
    			if(!m_IsPageActivated)
					return;

				this.m_Manager.Position = 0;
	
				DataTable ldt_temp = m_HeadTable.GetChanges(DataRowState.Modified);
								
				if(ldt_temp != null)
				{		
					// ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ�?
					// MA_M000073
					string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
					DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

					if(result == DialogResult.Yes)
					{
						// ���� �����͸� ������ �� ���ο� �����͸� �߰��� �� �ְ� �Ѵ�.
						if(this.Save())
						{
							// ���� ����
						}
						else
						{
							// ����� ���� �߻�
							return;
						}
					}
				}

				// Control �� Ȱ��ȭ
				SetControlDisable();

				this.Append();

				this.txt�����ȣ.Enabled = true;
				this.txt�����ȣ.Focus();

                //m_txtSoRefer.Enabled = true;
                //m_txtSoRefer.BackColor = System.Drawing.Color.White;
                //m_btnSoRefer.Enabled = true;
                //m_txtLcRefer.Enabled = true;
                //m_txtLcRefer.BackColor = System.Drawing.Color.White;
                //m_btnLcRefer.Enabled = true;
			}
			catch(Exception ex)
			{
				ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		/// ���� ��ư
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if(!this.m_IsPageActivated)
					return;

				this.txt�����ȣ.Focus();

				this.m_Manager.Position = 0;

				string msg = null;

				DataTable ldt_temp = this.m_HeadTable.GetChanges();

				if(ldt_temp == null)
				{
					// MA_M000017 ("����� �����Ͱ� �����ϴ�)
					msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000017");
					Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);

					return;
				}

				this.Save();

				this.txt�����ȣ.Enabled = false;
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		/// ���� ��ư
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				string msg = null;

				if(!this.m_IsPageActivated)
					return;

				DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Added);

				// ���� �� �� ���� �κ�(�߰��� �����Ͱ� �ִٴ� ���� ���ο� �������̹Ƿ� DB���� ���ٴ� ��
				if(ldt_temp != null)
					return;
			
				// MA_M000016
				// ������ ���� �ϰڽ��ϱ�?
				msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000016");

				if(DialogResult.Yes == MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
				{
					Application.DoEvents();

					//object[] args = new object[1]{this.m_HeadTable};
					//this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_INVIN", "CC_TR_INVIN.rem", "SaveDelete", args);
                    SaveDelete(this.m_HeadTable);

					// TR_M000033
					// ���������� ���� �Ǿ����ϴ�.
					msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000033");
					MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.Append();

					this.txt�����ȣ.Enabled = true;
					this.txt�����ȣ.Focus();
				}
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

///////////////////////
		/// <summary>
		/// �ۼ��� : ���ȸ
		/// ȣ���� : ����(����)
		/// ȣ�� UI : P_TR_EXINV(���� ���)
		/// ���� : TR_INVH�� �����͸� �����Ѵ�.
		/// </summary>
		/// <param name="pdt_table"></param>
		/// <returns></returns>
		public void SaveDelete(DataTable pdt_table)
		{
			try
			{

				if(this.DeleteINVH(pdt_table.Rows[0]["CD_COMPANY"].ToString(), pdt_table.Rows[0]["NO_INV"].ToString()) < 0)
				{
					// ������ �����Ͱ� �������� �ʽ��ϴ�.
					// TR_M000058
					System.ApplicationException app = new System.ApplicationException("TR_M000058");
					app.Source = "100000";
					app.HelpLink = "������ �����Ͱ� �������� �ʽ��ϴ�.";
					throw app;

				}

				if(this.DeleteINVL(pdt_table.Rows[0]["CD_COMPANY"].ToString(), pdt_table.Rows[0]["NO_INV"].ToString()) < 0)
				{
					// ������ �����Ͱ� �������� �ʽ��ϴ�.
					// TR_M000058
					System.ApplicationException app = new System.ApplicationException("TR_M000058");
					app.Source = "100000";
					app.HelpLink = "������ �����Ͱ� �������� �ʽ��ϴ�.";
					throw app;

				}

				if(this.DeletePacking(pdt_table.Rows[0]["CD_COMPANY"].ToString(), pdt_table.Rows[0]["NO_INV"].ToString()) < 0)
				{
					// ������ �����Ͱ� �������� �ʽ��ϴ�.
					// TR_M000058
					System.ApplicationException app = new System.ApplicationException("TR_M000058");
					app.Source = "100000";
					app.HelpLink = "������ �����Ͱ� �������� �ʽ��ϴ�.";
					throw app;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		#endregion

        /// <summary>
		/// �ۼ��� : ���ȸ
		/// ȣ���� : ����(����)
		/// ȣ�� CC : CC_TR_INVIN(���� ���)
		/// ���� : TR_INVH�� �����͸� �����Ѵ�.
		/// </summary>
		/// <param name="pdt_table"></param>
		/// <returns></returns>
		private int DeleteINVH(string ps_company, string ps_inv)
		{
			try
			{
				//string ls_query = null;
				string[] args = new string[2];

				//args[0] = ps_inv;
				//args[1] = ps_company;

                args[0] = ps_company;
                args[1] = ps_inv;

                //ls_query = TR_DBScript.GetScript("CC_TR_INVIN006", args);
								
				//return this.ExecuteNonQuery(ls_query);

                return (int)this.ExecSp("UP_TR_INVIN006", args);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		
		/// <summary>
		/// �ۼ��� : ���ȸ
		/// ȣ���� : ����(����)
		/// ȣ�� CC : CC_TR_INVIN(���� ���)
		/// ���� : TR_INVL�� �����͸� �����Ѵ�.
		/// </summary>
		/// <param name="pdt_table"></param>
		/// <returns></returns>
		private int DeleteINVL(string ps_company, string ps_inv)
		{
			try
			{
				//string ls_query = null;
				string[] args = new string[2];
				args[0] = ps_company;
				args[1] = ps_inv;

				//ls_query = TR_DBScript.GetScript("CC_TR_INVIN007", args);
								
				//return this.ExecuteNonQuery(ls_query);
                return (int)this.ExecSp("UP_TR_INVIN007", args);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		
		/// <summary>
		/// �ۼ��� : ���ȸ
		/// ȣ���� : ����(����)
		/// ȣ�� CC : CC_TR_INVIN(���� ���)
		/// ���� : TR_PACKING�� �����͸� �����Ѵ�.
		/// </summary>
		/// <param name="pdt_table"></param>
		/// <returns></returns>
		private int DeletePacking(string ps_company, string ps_inv)
		{
			try
			{
				string[] args = new string[2];

				args[0] = ps_company;
				args[1] = ps_inv;

				//string ls_query = TR_DBScript.GetScript("CC_TR_INVIN025", args);
				
				//return this.ExecuteNonQuery(ls_query);
                return (int)this.ExecSp("UP_TR_INVIN025", args);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
			
///////////////////////

		/// <summary>
		/// ��� ��ư
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ���� ��ư
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if(this.m_HeadTable == null)
					return true;

				string msg = null;

				this.m_Manager.Position = 0;


				bool lb_isSave = false;

				DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
				if(ldt_temp != null)
				{			
					lb_isSave = true;
				}

				// �����Ͱ� ó�� �ε��� ������� ���� �߰��� ��� LC��ȣ�� ������ �Ǵ��Ͽ� ������ ������ �����.
				DataTable ldt_table = this.m_HeadTable.GetChanges(DataRowState.Added);
			
				if(this.txt�����ȣ.Text != "" && ldt_table != null)
				{
					lb_isSave = true;
				}


				if(lb_isSave)
				{
					// ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ�?
					// MA_M000073
					msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
					DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if(result == DialogResult.Yes)
					{
						// ���� �����͸� ������ �� ���ο� �����͸� �߰��� �� �ְ� �Ѵ�.
						if(this.Save())
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					if(result == DialogResult.Cancel)
						return false;

				}
			}
			catch
			{
			}

			return true;

		}

		#region �� �ʱ� ���̺� �ʱ�ȭ �κ�

		/// <summary>
		/// DB���� ���ο��� ����� �� �����͸� �����´�.
		/// InvokeRemoteMethod ��� �� Table�� ������Ŵ (2003-08-13)
		/// </summary>
		private void SelectInit()
		{
			//DataSet lds_Result = (DataSet)this.MainFrameInterface.InvokeRemoteMethod("TradeExport_NTX", "trade.CC_TR_INVIN_NTX", "CC_TR_INVIN_NTX.rem", "SelectInvH", null);
			//this.m_HeadTable = lds_Result.Tables[0];

			//Head Table���� �� ����Ʈ �� �ο�
			m_HeadTable = new DataTable();
			m_HeadTable.Columns.Add("NO_INV", typeof(string));
			m_HeadTable.Columns.Add("CD_COMPANY", typeof(string));
			m_HeadTable.Columns.Add("DT_BALLOT", typeof(string));
			m_HeadTable.Columns.Add("CD_BIZAREA", typeof(string));
			m_HeadTable.Columns.Add("CD_SALEGRP", typeof(string));
			m_HeadTable.Columns.Add("NO_EMP", typeof(string));
			m_HeadTable.Columns.Add("FG_LC", typeof(string));
			m_HeadTable.Columns.Add("CD_PARTNER", typeof(string));
			m_HeadTable.Columns.Add("CD_EXCH", typeof(string));
			m_HeadTable.Columns.Add("AM_EX", typeof(double));
			m_HeadTable.Columns.Add("DT_LOADING", typeof(string));
			m_HeadTable.Columns.Add("CD_ORIGIN", typeof(string));
			m_HeadTable.Columns.Add("CD_AGENT", typeof(string));
			m_HeadTable.Columns.Add("CD_EXPORT", typeof(string));
			m_HeadTable.Columns.Add("CD_PRODUCT", typeof(string));
			m_HeadTable.Columns.Add("SHIP_CORP", typeof(string));
			m_HeadTable.Columns.Add("NM_VESSEL", typeof(string));
			m_HeadTable.Columns.Add("COND_TRANS", typeof(string));
			m_HeadTable.Columns.Add("TP_TRANSPORT", typeof(string));
			m_HeadTable.Columns.Add("TP_TRANS", typeof(string));
			m_HeadTable.Columns.Add("TP_PACKING", typeof(string));
			m_HeadTable.Columns.Add("CD_WEIGHT", typeof(string));
			m_HeadTable.Columns.Add("GROSS_WEIGHT", typeof(double));
			m_HeadTable.Columns.Add("NET_WEIGHT", typeof(double));
			m_HeadTable.Columns.Add("PORT_LOADING", typeof(string));
			m_HeadTable.Columns.Add("PORT_ARRIVER", typeof(string));
			m_HeadTable.Columns.Add("DESTINATION", typeof(string));
			m_HeadTable.Columns.Add("NO_SCT", typeof(int));
			m_HeadTable.Columns.Add("NO_ECT", typeof(int));
			m_HeadTable.Columns.Add("CD_CUST_IN", typeof(string));
			m_HeadTable.Columns.Add("DT_TO", typeof(string));
			m_HeadTable.Columns.Add("NO_LC", typeof(string));
			m_HeadTable.Columns.Add("NO_SO", typeof(string));
			m_HeadTable.Columns.Add("REMARK1", typeof(string));
			m_HeadTable.Columns.Add("REMARK2", typeof(string));
			m_HeadTable.Columns.Add("REMARK3", typeof(string));
			m_HeadTable.Columns.Add("REMARK4", typeof(string));
			m_HeadTable.Columns.Add("REMARK5", typeof(string));
			m_HeadTable.Columns.Add("DTS_INSERT", typeof(string));
			m_HeadTable.Columns.Add("ID_INSERT", typeof(string));
			m_HeadTable.Columns.Add("DTS_UPDATE", typeof(string));
			m_HeadTable.Columns.Add("ID_UPDATE", typeof(string));
			m_HeadTable.Columns.Add("OLD_NO_INV", typeof(string));
			//m_HeadTable.Columns.Add("NM_BIZAREA", typeof(string));
			m_HeadTable.Columns.Add("NM_SALEGRP", typeof(string));
			m_HeadTable.Columns.Add("NM_KOR", typeof(string));
			m_HeadTable.Columns.Add("NM_PARTNER", typeof(string));
			m_HeadTable.Columns.Add("NM_AGENT", typeof(string));
			m_HeadTable.Columns.Add("NM_EXPORT", typeof(string));
			m_HeadTable.Columns.Add("NM_PRODUCT", typeof(string));
			m_HeadTable.Columns.Add("NM_SHIP_CORP", typeof(string));
			m_HeadTable.Columns.Add("NM_CUST_IN", typeof(string));

			// DefaultValue ����
			this.m_HeadTable.Columns["NO_INV"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_COMPANY"].DefaultValue = this.MainFrameInterface.LoginInfo.CompanyCode;
			this.m_HeadTable.Columns["DT_BALLOT"].DefaultValue = this.m_Today;
            this.m_HeadTable.Columns["CD_BIZAREA"].DefaultValue = this.bpc�����.CodeValue.ToString();
			this.m_HeadTable.Columns["CD_SALEGRP"].DefaultValue = "";
			this.m_HeadTable.Columns["NO_EMP"].DefaultValue = "";
			this.m_HeadTable.Columns["FG_LC"].DefaultValue = this.cbo�ŷ�����.SelectedValue;
			this.m_HeadTable.Columns["CD_PARTNER"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_EXCH"].DefaultValue = this.cbo��ȭ.SelectedValue;
			this.m_HeadTable.Columns["AM_EX"].DefaultValue = 0;

			this.m_HeadTable.Columns["DT_LOADING"].DefaultValue = this.m_Today;
			this.m_HeadTable.Columns["CD_ORIGIN"].DefaultValue = this.cbo������.SelectedValue;
			this.m_HeadTable.Columns["CD_AGENT"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_EXPORT"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_PRODUCT"].DefaultValue = "";
			this.m_HeadTable.Columns["SHIP_CORP"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_VESSEL"].DefaultValue = "";
			this.m_HeadTable.Columns["COND_TRANS"].DefaultValue = "";
			this.m_HeadTable.Columns["TP_TRANSPORT"].DefaultValue = this.cbo��۹��.SelectedValue;
			this.m_HeadTable.Columns["TP_TRANS"].DefaultValue = this.cbo�������.SelectedValue;

			this.m_HeadTable.Columns["TP_PACKING"].DefaultValue = this.cbo��������.SelectedValue;
			this.m_HeadTable.Columns["CD_WEIGHT"].DefaultValue = this.cbo�߷�����.SelectedValue;
			this.m_HeadTable.Columns["GROSS_WEIGHT"].DefaultValue = 0;
			this.m_HeadTable.Columns["NET_WEIGHT"].DefaultValue = 0;
			this.m_HeadTable.Columns["PORT_LOADING"].DefaultValue = "";
			this.m_HeadTable.Columns["PORT_ARRIVER"].DefaultValue = "";
			this.m_HeadTable.Columns["DESTINATION"].DefaultValue = "";
			this.m_HeadTable.Columns["NO_SCT"].DefaultValue = 0;
			this.m_HeadTable.Columns["NO_ECT"].DefaultValue = 0;
			this.m_HeadTable.Columns["CD_CUST_IN"].DefaultValue = "";

			this.m_HeadTable.Columns["DT_TO"].DefaultValue = this.m_Today;
			this.m_HeadTable.Columns["NO_LC"].DefaultValue = "";
			this.m_HeadTable.Columns["NO_SO"].DefaultValue = "";
			this.m_HeadTable.Columns["REMARK1"].DefaultValue = "";
			this.m_HeadTable.Columns["REMARK2"].DefaultValue = "";
			this.m_HeadTable.Columns["REMARK3"].DefaultValue = "";
			this.m_HeadTable.Columns["REMARK4"].DefaultValue = "";
			this.m_HeadTable.Columns["REMARK5"].DefaultValue = "";
			this.m_HeadTable.Columns["DTS_INSERT"].DefaultValue = "";
			this.m_HeadTable.Columns["ID_INSERT"].DefaultValue = this.MainFrameInterface.LoginInfo.EmployeeNo;

			this.m_HeadTable.Columns["DTS_UPDATE"].DefaultValue = "";
			this.m_HeadTable.Columns["ID_UPDATE"].DefaultValue = this.MainFrameInterface.LoginInfo.EmployeeNo;
			this.m_HeadTable.Columns["OLD_NO_INV"].DefaultValue = "";
			//this.m_HeadTable.Columns["NM_BIZAREA"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_SALEGRP"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_KOR"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_PARTNER"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_AGENT"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_EXPORT"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_PRODUCT"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_SHIP_CORP"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_CUST_IN"].DefaultValue = "";

			this.m_CopyTable = this.m_HeadTable.Copy();
		}

		
		/// <summary>
		/// �ʱ� ���ε� �޴��� ����
		/// </summary>
		private void SetBindingManager()
		{
			this.m_Manager = this.BindingContext[this.m_HeadTable];
			
			foreach(Control ctr in this.Controls)
			{
				if(ctr is Panel)
				{
					foreach(Control ctrl in ((Panel)ctr).Controls)
					{
						ctrl.DataBindings.Clear();
					}
				}
			}
		
			
			this.txt�����ȣ.DataBindings.Add("Text", this.m_HeadTable, "NO_INV");
			this.cbo�ŷ�����.DataBindings.Add("SelectedValue", this.m_HeadTable, "FG_LC");
			this.cbo��ȭ.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_EXCH");	// Mask
            this.dtp����������.DataBindings.Add("Text", this.m_HeadTable, "DT_LOADING");	// Mask
			this.cbo�������.DataBindings.Add("SelectedValue", this.m_HeadTable, "TP_TRANS");
			this.cbo�߷�����.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_WEIGHT"); //2003-08-14�߰�
			
			this.txt����CT��ȣ.DataBindings.Add("Text", this.m_HeadTable, "NO_SCT");
			this.txt������.DataBindings.Add("Text", this.m_HeadTable, "PORT_LOADING");
			this.txt������.DataBindings.Add("Text", this.m_HeadTable, "PORT_ARRIVER");
			this.txt����������.DataBindings.Add("Text", this.m_HeadTable, "DESTINATION");
			this.txtVESSEL��.DataBindings.Add("Text", this.m_HeadTable, "NM_VESSEL");
			
			this.txt�ε�����.DataBindings.Add("Text", this.m_HeadTable, "COND_TRANS");
			this.txt���1.DataBindings.Add("Text", this.m_HeadTable, "REMARK1");
			this.txt���2.DataBindings.Add("Text", this.m_HeadTable, "REMARK2");
			this.txt���3.DataBindings.Add("Text", this.m_HeadTable, "REMARK3");
			this.txt���4.DataBindings.Add("Text", this.m_HeadTable, "REMARK4");
			this.txt���5.DataBindings.Add("Text", this.m_HeadTable, "REMARK5");

            this.dtp��������.DataBindings.Add("Text", this.m_HeadTable, "DT_BALLOT");
			this.cur��ȭ�ݾ�.DataBindings.Add("Text", this.m_HeadTable, "AM_EX");	// Mask
			this.cbo��۹��.DataBindings.Add("SelectedValue", this.m_HeadTable, "TP_TRANSPORT");
			this.cur���߷�.DataBindings.Add("Text", this.m_HeadTable, "GROSS_WEIGHT");
			this.txt����CT��ȣ.DataBindings.Add("Text", this.m_HeadTable, "NO_ECT");	
			
			//this.m_txtLcRefer.DataBindings.Add("Text", this.m_HeadTable, "NO_LC");	// Mask
			//this.m_txtSoRefer.DataBindings.Add("Text", this.m_HeadTable, "NO_SO");
			this.cbo������.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_ORIGIN");
            this.dtp���������.DataBindings.Add("Text", this.m_HeadTable, "DT_TO");
			this.cbo��������.DataBindings.Add("SelectedValue", this.m_HeadTable, "TP_PACKING");
			this.cur���߷�.DataBindings.Add("Text", this.m_HeadTable, "NET_WEIGHT");// ����ȯ��
			//this.m_txtArtran.DataBindings.Add("Text", this.m_HeadTable, "CD_CUST_IN");
			
			this.m_Manager.Position = 0;
		}

		#endregion

		#region �� S/O ���� ���� �κ� 

//		/// <summary>
//		/// S/O ���� ����â�� ����.
//		/// </summary>
//		/// <param name="sender"></param>
//		/// <param name="e"></param>
//		private void m_btnSoRefer_Click(object sender, System.EventArgs e)
//		{
//			try
//			{
//				trade.P_TR_SASO_SUB obj = new trade.P_TR_SASO_SUB(this.MainFrameInterface);
//
//				if(obj.ShowDialog() == DialogResult.OK)
//				{
//					DataRow ldr_Result = obj.GetResultRow;
//
//					this.SelectSOH(ldr_Result);
//
//					this.m_IsPageActivated = true;
//				}
//				obj.Dispose();
//			}
//			catch(Exception ex)
//			{
//				// �۾��� ���������� ó������ ���߽��ϴ�.(����â)
//				this.MainFrameInterface.ShowMessageBox(109, ex.Message);
//			}
//		}
//
//
//		/// <summary>
//		/// P_TR_SASO_SUB���� ������ �������� ���ֹ�ȣ�� �ش絥���͸� DB���� �����´�.
//		/// </summary>
//		/// <param name="pdr_saso"></param>
//		private void SelectSOH(DataRow pdr_saso)
//		{
//			try
//			{
//				this.SetControlEnable();
//
//				DataSet lds_Result = null;
//
//				// null�� �ƴϸ� TR_EXSOH���� �����͸� ��������, NULL�̸� ����ROW���� �����͸� �־� �ش�.
//				if(pdr_saso["CNT_SO"].ToString() != "")
//				{
//					object[] args = new object[2]{this.MainFrameInterface.LoginInfo.CompanyCode, pdr_saso["NO_SO"].ToString()};
//					lds_Result = (DataSet)this.MainFrameInterface.InvokeRemoteMethod("TradeExLCReg", "trade.CC_TR_LCIN", "CC_TR_LCIN.rem", "SelectSOH", args);
//
//					this.InsertToMainTable(lds_Result.Tables[0]);
//				}
//				else
//				{
//					this.InsertToMainTable(pdr_saso);
//				}
//			}
//			catch(Exception ex)
//			{
//				// �۾��� ���������� ó������ ���߽��ϴ�.(��ȸ)
//				this.MainFrameInterface.ShowMessageBox(109, "[SelectSOH] " + ex.Message);
//			}
//		}
//
//		/// <summary>
//		/// SA_SOH���� ������ �����͸� �ִ´�.
//		/// </summary>
//		/// <param name="pdr_row"></param>
//		private void InsertToMainTable(DataRow pdr_row)
//		{
//			this.m_HeadTable.Rows[0]["CD_COMPANY"] = pdr_row["CD_COMPANY"];
//			this.m_HeadTable.Rows[0]["CD_BIZAREA"] = pdr_row["CD_BIZAREA"];
//			this.m_HeadTable.Rows[0]["CD_PARTNER"] = pdr_row["CD_PARTNER"];
//			this.m_HeadTable.Rows[0]["CD_EXCH"] = pdr_row["CD_EXCH"];
//			this.m_HeadTable.Rows[0]["RT_EXCH"] = pdr_row["RT_EXCH"];
//			this.m_HeadTable.Rows[0]["AM_EX"] = pdr_row["AM_SO"];
//			this.m_HeadTable.Rows[0]["CD_SALEGRP"] = pdr_row["CD_SALEGRP"];
//			this.m_HeadTable.Rows[0]["NO_EMP"] = pdr_row["NO_EMP"];
//			this.m_HeadTable.Rows[0]["REMARK1"] = pdr_row["DC_RMK"];
////			this.m_HeadTable.Rows[0]["___"] = pdr_row["___"];
//
//
//			this.m_HeadTable.Rows[0]["NM_PARTNER"] = pdr_row["NM_PARTNER"];
//			this.m_HeadTable.Rows[0]["NM_SALEGRP"] = pdr_row["NM_SALEGRP"];
//			this.m_HeadTable.Rows[0]["NM_KOR"] = pdr_row["NM_KOR"];
//			this.m_HeadTable.Rows[0]["NM_BIZAREA"] = pdr_row["NM_BIZAREA"];
//			
//
//			// �ڵ�, �� ������ ���� ������ ����
//					
//			// �ŷ�ó �ڵ� & ��Ī ����
//			this.m_CdPartner = pdr_row["CD_PARTNER"].ToString();
//			this.m_NmPartner = pdr_row["NM_PARTNER"].ToString();
//			this.m_txtCdTrans.Text = this.m_NmPartner;
//
//			// �����׷� �ڵ� & ��Ī ����
//			this.m_CdGroup = pdr_row["CD_SALEGRP"].ToString();
//			this.m_NmGroup = pdr_row["NM_SALEGRP"].ToString();
//			this.m_txtGroupIsul.Text = this.m_NmGroup;
//
//			// ����� �ڵ� & ��Ī ����
//			this.m_CdEmp = pdr_row["NO_EMP"].ToString();
//			this.m_NmEmp = pdr_row["NM_KOR"].ToString();
//			this.m_txtNmEmp.Text = this.m_NmEmp;
//
//			// ����� �ڵ� & ��Ī ����
//			this.m_CdBizarea = pdr_row["CD_BIZAREA"].ToString();
//			this.m_NmBizarea = pdr_row["NM_BIZAREA"].ToString();
//			this.m_txtBallot.Text = this.m_NmBizarea;
//
//
//
//		}
//
//		/// <summary>
//		/// TR_EXSOH���� ������ �����͸� �ִ´�.
//		/// </summary>
//		/// <param name="pdt_table"></param>
//		private void InsertToMainTable(DataTable pdt_table)
//		{
//			this.m_HeadTable.Rows[0]["CD_COMPANY"] = pdt_table.Rows[0]["CD_COMPANY"];
//			this.m_HeadTable.Rows[0]["CD_BIZAREA"] = pdt_table.Rows[0]["CD_BIZAREA"];
//			this.m_HeadTable.Rows[0]["CD_SALEGRP"] = pdt_table.Rows[0]["CD_SALEGRP"];
//			this.m_HeadTable.Rows[0]["NO_EMP"] = pdt_table.Rows[0]["NO_EMP"];
//			this.m_HeadTable.Rows[0]["FG_LC"] = pdt_table.Rows[0]["FG_LC"];
//			this.m_HeadTable.Rows[0]["CD_BANK_NOTICE"] = pdt_table.Rows[0]["CD_BANK"];
//            this.m_HeadTable.Rows[0]["CD_PARTNER"] = pdt_table.Rows[0]["CD_PARTNER"];
//			this.m_HeadTable.Rows[0]["CD_EXCH"] = pdt_table.Rows[0]["CD_EXCH"];
//			this.m_HeadTable.Rows[0]["RT_EXCH"] = pdt_table.Rows[0]["RT_EXCH"];
//			this.m_HeadTable.Rows[0]["AM_EX"] = pdt_table.Rows[0]["AM_EX"];
//			
//			this.m_HeadTable.Rows[0]["COND_PRICE"] = pdt_table.Rows[0]["COND_PRICE"];
//			this.m_HeadTable.Rows[0]["CD_NATION"] = pdt_table.Rows[0]["CD_NATION"];
//			this.m_HeadTable.Rows[0]["COND_SHIPMENT"] = pdt_table.Rows[0]["COND_SHIPMENT"];
//			this.m_HeadTable.Rows[0]["COND_PAY"] = pdt_table.Rows[0]["COND_PAY"];
//			this.m_HeadTable.Rows[0]["COND_DAYS"] = pdt_table.Rows[0]["COND_DAYS"];
//			this.m_HeadTable.Rows[0]["COND_DESC"] = pdt_table.Rows[0]["COND_TEXT"];
//			this.m_HeadTable.Rows[0]["TP_PACKING"] = pdt_table.Rows[0]["TP_PACKING"];
//			this.m_HeadTable.Rows[0]["NO_OFFER"] = pdt_table.Rows[0]["NO_SO"];
//			this.m_HeadTable.Rows[0]["NM_INSPECT"] = pdt_table.Rows[0]["NM_INSPECT"];
//			this.m_HeadTable.Rows[0]["INSPECTION"] = pdt_table.Rows[0]["INSPECTION"];	// ���� �κ�
//			
//			this.m_HeadTable.Rows[0]["DESTINATION"] = pdt_table.Rows[0]["DESTINATION"];
//			this.m_HeadTable.Rows[0]["PORT_ARRIVER"] = pdt_table.Rows[0]["PORT_ARRIVER"];
//			this.m_HeadTable.Rows[0]["PORT_LOADING"] = pdt_table.Rows[0]["PORT_LOADING"];
//			this.m_HeadTable.Rows[0]["TP_TRANSPORT"] = pdt_table.Rows[0]["TP_TRANSPORT"];
//			this.m_HeadTable.Rows[0]["TP_TRANS"] = pdt_table.Rows[0]["TP_TRANS"];
//			this.m_HeadTable.Rows[0]["COND_INSUR"] = pdt_table.Rows[0]["COND_INSUR"];
//			this.m_HeadTable.Rows[0]["CD_ORIGIN"] = pdt_table.Rows[0]["CD_ORIGIN"];
//			this.m_HeadTable.Rows[0]["REMARK1"] = pdt_table.Rows[0]["REMARK1"];
//			this.m_HeadTable.Rows[0]["REMARK2"] = pdt_table.Rows[0]["REMARK2"];
//
//			// �ŷ�ó �κ�
//			this.m_HeadTable.Rows[0]["NM_PARTNER"] = pdt_table.Rows[0]["NM_PARTNER"];
//			// ��������
//			this.m_HeadTable.Rows[0]["NM_BANK_NOTICE"] = pdt_table.Rows[0]["NM_BANK"];
//			// �����
//			this.m_HeadTable.Rows[0]["NM_KOR"] = pdt_table.Rows[0]["NM_KOR"];
//			this.m_HeadTable.Rows[0]["NM_SALEGRP"] = pdt_table.Rows[0]["NM_SALEGRP"];
//			this.m_HeadTable.Rows[0]["NM_BIZAREA"] = pdt_table.Rows[0]["NM_BIZAREA"];
//
//
//			// ��������
//
//			// �ڵ�, �� ������ ���� ������ ����
//					
//			// �ŷ�ó �ڵ� & ��Ī ����
//			this.m_CdPartner = pdt_table.Rows[0]["CD_PARTNER"].ToString();
//			this.m_NmPartner = pdt_table.Rows[0]["NM_PARTNER"].ToString();		
//			this.m_txtCdTrans.Text = this.m_NmPartner;
//
//			// �����׷� �ڵ� & ��Ī ����
//			this.m_CdGroup = pdt_table.Rows[0]["CD_SALEGRP"].ToString();
//			this.m_NmGroup = pdt_table.Rows[0]["NM_SALEGRP"].ToString();
//			this.m_txtGroupIsul.Text = this.m_NmGroup;
//
////			// ���� ���� �ڵ� & ��Ī ����
////			this.m_CdOpenBank = pdt_table.Rows[0["CD_BANK_LC"].ToString();
////			this.m_NmOpenBank = pdt_table.Rows[0["NM_BANK_LC"].ToString();
////			this.m_txtOpenBank.Text = this.m_NmOpenBank;
//
//			// ���� ���� �ڵ� & ��Ī ����
//			this.m_CdNoticeBank = pdt_table.Rows[0]["CD_BANK"].ToString();
//			this.m_NmNoticeBank = pdt_table.Rows[0]["NM_BANK"].ToString();
//			this.m_txtNoticeBank.Text = this.m_NmNoticeBank;
//
//			// ����� �ڵ� & ��Ī ����
//			this.m_CdEmp = pdt_table.Rows[0]["NO_EMP"].ToString();
//			this.m_NmEmp = pdt_table.Rows[0]["NM_KOR"].ToString();
//			this.m_txtNmEmp.Text = this.m_NmEmp;
//
//			// ����� �ڵ� & ��Ī ����
//			this.m_CdBizarea = pdt_table.Rows[0]["CD_BIZAREA"].ToString();
//			this.m_NmBizarea = pdt_table.Rows[0]["NM_BIZAREA"].ToString();
//			this.m_txtBallot.Text = this.m_NmBizarea;
//
//
//			
//
//		}


		#endregion

		#region �� �߰� �κ�

		/// <summary>
		/// 
		/// </summary>
		private void Append()
		{
			this.m_HeadTable = null;

			this.m_HeadTable = this.m_CopyTable.Copy();
			this.m_HeadTable.Clear();

			this.SetBindingManager();

			DataRow newrow = this.m_HeadTable.NewRow();
			this.m_HeadTable.Rows.Add(newrow);

            //this.ComboResetting(); --- ���߿�

			// �ڵ�, �� ������ ���� ������ ����
			// �ŷ�ó �ڵ� & ��Ī ����
			this.m_CdPartner = "";
			this.m_NmPartner = "";
            this.bpc�ŷ�ó.CodeName = m_NmPartner;
            this.bpc�ŷ�ó.CodeValue = m_CdPartner;

			// �����׷� �ڵ� & ��Ī ����
			this.m_CdGroup = "";
			this.m_NmGroup = "";
            this.bbpc�����׷�.CodeName = m_NmGroup;
            this.bbpc�����׷�.CodeName = m_CdGroup;

            // ����� �ڵ� & ��Ī ����
			this.m_CdEmp = "";
			this.m_NmEmp = "";

			// ����� �ڵ� & ��Ī ����
            m_CdBizarea = bpc�����.CodeValue.ToString();

			// ������ �ڵ� & ��Ī ����
			this.m_CdExport = "";
			this.m_NmExport = "";
            this.bpc������.CodeName = "";
            this.bpc������.CodeValue = "";

			// ������ �ڵ� & ��Ī ����
			this.m_CdPRODUCT = "";
			this.m_NmPRODUCT = "";
            this.bpc������.CodeName = m_NmPRODUCT;
            this.bpc������.CodeValue = m_CdPRODUCT;

			// ������ �ڵ� & ��Ī ����
			this.m_CdAgent = "";
			this.m_NmAgent = "";
            this.bpc������.CodeName = "";
            this.bpc������.CodeValue = "";

			// ���� �ڵ� & ��Ī ����
			this.m_CdShipCorp = "";
			this.m_NmShipCorp = "";
            this.bpc����.CodeName = "";
            this.bpc����.CodeValue = "";

            // ��������ó
			m_CdCustIn = "";
			m_NmCustIn = "";
            this.bpc��������ó.CodeName = "";
            this.bpc��������ó.CodeValue = "";

		}

		#endregion

		#region �� ���� �κ�

		/// <summary>
		/// ����
		/// </summary>
		/// <returns></returns>
		private bool Save()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				Cursor.Current = Cursors.WaitCursor;

				string msg = null;

				this.txt�����ȣ.Focus();

				// Validating �˻��Ͽ� ������ �߻��ϸ� �����ϱ� ����
				if(this.m_IsSave == false)
				{
					this.m_IsSave = true;
					return false;
				}


				// �ʼ� �Է� �� üũ
				if(!this.CheckRequiredValue())
					return false;

				object[] args = new object[1]{this.m_HeadTable};

				//DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Added);
				//if(ldt_temp != null)
                if (this.m_HeadTable != null)
				{
					
					// �Է°����� ��¥�� �����Ѵ�.
					this.ReWriteDate();

					//this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_INVIN", "CC_TR_INVIN.rem", "SaveAppendH", args);
                    SaveAppendH(this.m_HeadTable);
				}

//���� ���� ����. �������� ���ս�Ŵ... ���� 2006/07/05                
                //ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
                //if(ldt_temp != null)
                //{
				
                //    // �Է°����� ��¥�� �����Ѵ�.
                //    this.ReWriteDate();

                //    //this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_INVIN", "CC_TR_INVIN.rem", "SaveUpdateH", args);
                //}
		
				// CM_M000001
				// �ڷḦ �����Ͽ����ϴ�.
				msg = this.MainFrameInterface.GetMessageDictionaryItem("CM_M000001");
				Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.MainFrameInterface.ShowStatusBarMessage(6, "");


				this.m_HeadTable.Rows[0]["OLD_NO_INV"] = this.m_HeadTable.Rows[0]["NO_INV"];

				this.m_HeadTable.AcceptChanges();

				return true;
			}
			catch(System.Exception ex)
			{
				this.Cursor = Cursors.Default;
				Cursor.Current = Cursors.Default;

				throw ex;
			}
			finally
			{
				this.Cursor = Cursors.Default;
				Cursor.Current = Cursors.Default;
			}
		}

        /// <summary>
        /// �ۼ��� : ���ȸ
        /// ȣ���� : ����(����)
        /// ȣ�� UI : P_TR_EXINV(���� ���)
        /// ���� : TR_INVH�� �����͸� �߰��Ѵ�.
        /// </summary>
        /// <param name="pdt_table"></param>
        /// <returns></returns>
        public void SaveAppendH(DataTable pdt_table)
        {
            try
            {
                //string ls_date = this.GetSysDateTimeString(Duzon.Common.Util.DateType.LongDateString);


                DataTable dt = pdt_table.GetChanges();

                if (dt == null) return;
                if (dt.Rows.Count == 0) return;

                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.CompanyID = LoginInfo.CompanyCode;
                si.UserID = LoginInfo.EmployeeNo;
                si.SpNameInsert = "UP_TR_INVH_INSERT";
                si.SpNameUpdate = "UP_TR_INVH_UPDATE";

                si.SpParamsInsert = new string[] { "NO_INV", "CD_COMPANY", "DT_BALLOT", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "FG_LC", "CD_PARTNER", "CD_EXCH", "AM_EX", "DT_LOADING", "CD_ORIGIN", "CD_AGENT", "CD_EXPORT", "CD_PRODUCT", "SHIP_CORP", "NM_VESSEL", "COND_TRANS", "TP_TRANSPORT", "TP_TRANS", "TP_PACKING", "CD_WEIGHT", "GROSS_WEIGHT", "NET_WEIGHT", "PORT_LOADING", "PORT_ARRIVER", "DESTINATION", "NO_SCT", "NO_ECT", "CD_CUST_IN", "DT_TO", "NO_LC", "NO_SO", "REMARK1", "REMARK2", "REMARK3", "REMARK4", "REMARK5", "ID_INSERT" };
                si.SpParamsUpdate = new string[] { "NO_INV", "CD_COMPANY", "DT_BALLOT", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "FG_LC", "CD_PARTNER", "CD_EXCH", "AM_EX", "DT_LOADING", "CD_ORIGIN", "CD_AGENT", "CD_EXPORT", "CD_PRODUCT", "SHIP_CORP", "NM_VESSEL", "COND_TRANS", "TP_TRANSPORT", "TP_TRANS", "TP_PACKING", "CD_WEIGHT", "GROSS_WEIGHT", "NET_WEIGHT", "PORT_LOADING", "PORT_ARRIVER", "DESTINATION", "NO_SCT", "NO_ECT", "CD_CUST_IN", "DT_TO", "NO_LC", "NO_SO", "REMARK1", "REMARK2", "REMARK3", "REMARK4", "REMARK5", "ID_UPDATE" };

                ResultData result = (ResultData)this.Save(si);

                if (result.Result)
                {
                    pdt_table.AcceptChanges();
                    return;
                }

                return;
                //if (this.AppendINVH(pdt_table, ls_date) < 0)
                //{
                //    // ������� �ʾҽ��ϴ�. �ٽ� �õ��� ���ñ� �ٶ��ϴ�.
                //    // TR_M000058
                //    System.ApplicationException app = new System.ApplicationException("TR_M000058");
                //    app.Source = "100000";
                //    app.HelpLink = "������� �ʾҽ��ϴ�. �ٽ� �õ��� ���ñ� �ٶ��ϴ�.";
                //    throw app;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



		#endregion

		#region �� ������ �۾�


		/// <summary>
		/// ��¥�� /�� ������ �ش�.
		/// </summary>
		private void ReWriteDate()
		{
			foreach(DataRow row in this.m_HeadTable.Rows)
			{
				row.BeginEdit();
				row["DT_BALLOT"] = row["DT_BALLOT"].ToString().Replace("/", "");
				row["DT_BALLOT"] = row["DT_BALLOT"].ToString().Replace("_", "");
				row["DT_BALLOT"] = row["DT_BALLOT"].ToString().Replace(" ", "");

				row["DT_LOADING"] = row["DT_LOADING"].ToString().Replace("/", "");
				row["DT_LOADING"] = row["DT_LOADING"].ToString().Replace("_", "");
				row["DT_LOADING"] = row["DT_LOADING"].ToString().Replace(" ", "");

				row["DT_TO"] = row["DT_TO"].ToString().Replace("/", "");
				row["DT_TO"] = row["DT_TO"].ToString().Replace("_", "");
				row["DT_TO"] = row["DT_TO"].ToString().Replace(" ", "");
				row.EndEdit();
			}
		}



		/// <summary>
		/// �ʼ� �Է»��� üũ
		/// </summary>
		/// <returns></returns>
		private bool CheckRequiredValue()
		{
			bool isok = true;

			if(!this.DetailCheckRequiredValue(this.txt�����ȣ))
				isok = false;
            else if (dtp��������.Text == string.Empty)
                isok = false;
            else if (bpc�ŷ�ó.IsEmpty())
				isok = false;
			else if(bbpc�����׷�.IsEmpty())
				isok = false;
            else if (bpc�����.IsEmpty())
				isok = false;

			if(isok == false)
			{
				// �ʼ� �Է»����� �������ϴ�.
				this.MainFrameInterface.ShowMessageBox(102, "");
				return false;
			}

            ////�ſ����ȣ�� S/O��ȣ�� ���� �ϳ��� �����Ǿ���Ѵ�.(2003-08-14)
            //if((m_txtSoRefer.Text == "") && (m_txtLcRefer.Text == ""))
            //{
            //    //������ȣ�� �������� �ʾҽ��ϴ�.(�ſ����ȣ or S/O��ȣ)
            //    MessageBoxEx.Show(GetMessageDictionaryItem("TR_M000080"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    m_txtLcRefer.Focus();
            //    return false;
            //}

			return isok;	
		}


		/// <summary>
		/// �� ��Ʈ���� �ʼ� �Է°��� üũ�Ѵ�.
		/// </summary>
		/// <param name="ps_control"></param>
		/// <returns></returns>
		private bool DetailCheckRequiredValue(object ps_control)
		{
			if(ps_control is Duzon.Common.Controls.MaskedEditBox)
			{
				Duzon.Common.Controls.MaskedEditBox ctrl = (Duzon.Common.Controls.MaskedEditBox)ps_control;
				// ��¥ ��ȿ�� üũ
				try
				{
					Convert.ToDateTime(ctrl.Text);
				}
				catch
				{
					ctrl.Focus();
					return false;
				}
			}
			else if(ps_control is Duzon.Common.Controls.CurrencyTextBox)
			{
				Duzon.Common.Controls.CurrencyTextBox ctrl = (Duzon.Common.Controls.CurrencyTextBox)ps_control;

				if(ctrl.DecimalValue.ToString() == "")
				{
					ctrl.Focus();
					return false;
				}
			}
			else if(ps_control is TextBox)
			{
				TextBox ctrl = (TextBox)ps_control;
				if(ctrl.Text == "")
				{
					ctrl.Focus();
					return false;
				}
			}


			return true;

		}

		#endregion

		#region �� ���� �ſ��� ��ȣ �κ�
			
		/// <summary>
		/// L/C ��ȣ ����â���� �����͸� �����´�.
		/// </summary>
		/// <param name="pdr_row"></param>
		private void ApplyLCRefer(DataRow pdr_row)
		{
			m_HeadTable.Rows[0].BeginEdit();
			this.m_HeadTable.Rows[0]["CD_BIZAREA"] = pdr_row["CD_BIZAREA"];
			this.m_HeadTable.Rows[0]["CD_SALEGRP"] = pdr_row["CD_SALEGRP"];
			this.m_HeadTable.Rows[0]["NO_EMP"] = pdr_row["NO_EMP"];
            this.m_HeadTable.Rows[0]["FG_LC"] = pdr_row["FG_LC"];
			this.m_HeadTable.Rows[0]["CD_PARTNER"] = pdr_row["CD_PARTNER"];
			this.m_HeadTable.Rows[0]["CD_EXCH"] = pdr_row["CD_EXCH"];
			this.m_HeadTable.Rows[0]["AM_EX"] = pdr_row["AM_EX"];
	
			this.m_HeadTable.Rows[0]["DT_LOADING"] = pdr_row["DT_LOADING"];
			this.m_HeadTable.Rows[0]["CD_ORIGIN"] = pdr_row["CD_ORIGIN"];
			this.m_HeadTable.Rows[0]["TP_TRANSPORT"] = pdr_row["TP_TRANSPORT"];
			this.m_HeadTable.Rows[0]["TP_TRANS"] = pdr_row["TP_TRANS"];	// ���� �κ�
	
			this.m_HeadTable.Rows[0]["TP_PACKING"] = pdr_row["TP_PACKING"];
			this.m_HeadTable.Rows[0]["PORT_LOADING"] = pdr_row["PORT_LOADING"];
			this.m_HeadTable.Rows[0]["PORT_ARRIVER"] = pdr_row["PORT_ARRIVER"];
			this.m_HeadTable.Rows[0]["DESTINATION"] = pdr_row["DESTINATION"];
 
			this.m_HeadTable.Rows[0]["NO_LC"] = pdr_row["NO_LC"];
			this.m_HeadTable.Rows[0]["REMARK1"] = pdr_row["REMARK1"];
			this.m_HeadTable.Rows[0]["REMARK2"] = pdr_row["REMARK2"];
			
			this.m_HeadTable.Rows[0]["NM_PARTNER"] = pdr_row["NM_PARTNER"];
			this.m_HeadTable.Rows[0]["NM_KOR"] = pdr_row["NM_EMP"];
            this.m_HeadTable.Rows[0]["NM_SALEGRP"] = pdr_row["NM_SALEGRP"];
			//this.m_HeadTable.Rows[0]["NM_BIZAREA"] = pdr_row["NM_BIZAREA"];

			// �ڵ�, �� ������ ���� ������ ����
					
			// �ŷ�ó �ڵ� & ��Ī ����
			this.m_CdPartner = pdr_row["CD_PARTNER"].ToString();
			this.m_NmPartner = pdr_row["NM_PARTNER"].ToString();
            this.bpc�ŷ�ó.CodeName = this.m_NmPartner;
            this.bpc�ŷ�ó.CodeValue = this.m_CdPartner;

			// �����׷� �ڵ� & ��Ī ����
			this.m_CdGroup = pdr_row["CD_SALEGRP"].ToString();
			this.m_NmGroup = pdr_row["NM_SALEGRP"].ToString();
	//		this.m_txtGroupIsul.Text = this.m_NmGroup;

			// ����� �ڵ� & ��Ī ����
			this.m_CdEmp = pdr_row["NO_EMP"].ToString();
			this.m_NmEmp = pdr_row["NM_EMP"].ToString();
            this.bpc�����.CodeName = this.m_NmEmp;
            this.bpc�����.CodeValue = this.m_CdEmp;

			// ����� �ڵ� & ��Ī ����
			this.m_CdBizarea = pdr_row["CD_BIZAREA"].ToString();
            this.bpc�����.CodeValue = this.m_CdBizarea;
			m_HeadTable.Rows[0].EndEdit();
		}



		#endregion

		#region �� ���� S/O ��ȣ �κ�

		/// <summary>
		/// S/O ����â���� �����͸� ���´�.
		/// </summary>
		/// <param name="pdt_table"></param>
		private void ApplySORefer(DataTable pdt_table)
		{
			m_HeadTable.Rows[0].BeginEdit();
			this.m_HeadTable.Rows[0]["CD_BIZAREA"] = pdt_table.Rows[0]["CD_BIZAREA"];
			this.m_HeadTable.Rows[0]["CD_SALEGRP"] = pdt_table.Rows[0]["CD_SALEGRP"];
			this.m_HeadTable.Rows[0]["NO_EMP"] = pdt_table.Rows[0]["NO_EMP"];
			this.m_HeadTable.Rows[0]["FG_LC"] = pdt_table.Rows[0]["FG_LC"];
			this.m_HeadTable.Rows[0]["CD_PARTNER"] = pdt_table.Rows[0]["CD_PARTNER"];
			this.m_HeadTable.Rows[0]["CD_EXCH"] = pdt_table.Rows[0]["CD_EXCH"];
			this.m_HeadTable.Rows[0]["AM_EX"] = pdt_table.Rows[0]["AM_EX"];
	
			this.m_HeadTable.Rows[0]["CD_ORIGIN"] = pdt_table.Rows[0]["CD_ORIGIN"];
			this.m_HeadTable.Rows[0]["TP_TRANSPORT"] = pdt_table.Rows[0]["TP_TRANSPORT"];
			this.m_HeadTable.Rows[0]["TP_TRANS"] = pdt_table.Rows[0]["TP_TRANS"];	// ���� �κ�
	
			this.m_HeadTable.Rows[0]["TP_PACKING"] = pdt_table.Rows[0]["TP_PACKING"];
			this.m_HeadTable.Rows[0]["PORT_LOADING"] = pdt_table.Rows[0]["PORT_LOADING"];
			this.m_HeadTable.Rows[0]["PORT_ARRIVER"] = pdt_table.Rows[0]["PORT_ARRIVER"];
			this.m_HeadTable.Rows[0]["DESTINATION"] = pdt_table.Rows[0]["DESTINATION"];
 
			this.m_HeadTable.Rows[0]["NO_LC"] = "";
			this.m_HeadTable.Rows[0]["NO_SO"] = pdt_table.Rows[0]["NO_SO"];
			this.m_HeadTable.Rows[0]["REMARK1"] = pdt_table.Rows[0]["REMARK1"];
			this.m_HeadTable.Rows[0]["REMARK2"] = pdt_table.Rows[0]["REMARK2"];
			
			this.m_HeadTable.Rows[0]["NM_PARTNER"] = pdt_table.Rows[0]["LN_PARTNER"];
			this.m_HeadTable.Rows[0]["NM_KOR"] = pdt_table.Rows[0]["NM_KOR"];
			this.m_HeadTable.Rows[0]["NM_SALEGRP"] = pdt_table.Rows[0]["NM_SALEGRP"];
			//this.m_HeadTable.Rows[0]["NM_BIZAREA"] = pdt_table.Rows[0]["NM_BIZAREA"];

			// �ڵ�, �� ������ ���� ������ ����
					
			// �ŷ�ó �ڵ� & ��Ī ����
			this.m_CdPartner = pdt_table.Rows[0]["CD_PARTNER"].ToString();
			this.m_NmPartner = pdt_table.Rows[0]["LN_PARTNER"].ToString();
            this.bpc�ŷ�ó.CodeName = this.m_NmPartner;
            this.bpc�ŷ�ó.CodeValue = this.m_CdPartner;

			// �����׷� �ڵ� & ��Ī ����
			this.m_CdGroup = pdt_table.Rows[0]["CD_SALEGRP"].ToString();
			this.m_NmGroup = pdt_table.Rows[0]["NM_SALEGRP"].ToString();
            this.bbpc�����׷�.CodeName = this.m_NmGroup;
            this.bbpc�����׷�.CodeValue = this.m_CdGroup;

            // ����� �ڵ� & ��Ī ����
			this.m_CdEmp = pdt_table.Rows[0]["NO_EMP"].ToString();
			this.m_NmEmp = pdt_table.Rows[0]["NM_KOR"].ToString();
			this.bpc�����.CodeName = this.m_NmEmp;
            this.bpc�����.CodeValue = this.m_CdEmp;

			// ����� �ڵ� & ��Ī ����
			this.m_CdBizarea = pdt_table.Rows[0]["CD_BIZAREA"].ToString();
            this.bpc�����.CodeValue = this.m_CdBizarea;
			m_HeadTable.Rows[0].EndEdit();
		}

		#endregion

		#region �� ����â �κ�

		/// <summary>
		/// ����â�� ����.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenSubPage(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				Cursor.Current = Cursors.WaitCursor;

				if(sender is Button)
				{
					Button box = (Button)sender;

					// ���� �ſ��� ��ȣ
					if(box.Name == "m_btnLcRefer")
					{

                        trade.P_TR_EXLCNO_SUB obj = new trade.P_TR_EXLCNO_SUB(this.MainFrameInterface);

                        if (obj.ShowDialog() == DialogResult.OK)
                        {
                            this.ApplyLCRefer(obj.GetResultRow);
                            this.m_IsPageActivated = true;
                            this.SetControlEnable();
                            ////�ſ��� ��ȣ�� �����߱� ������ S/O��ȣ�� ���´�.(2003-08-13)
                            //m_txtSoRefer.Enabled = false;
                            //m_txtSoRefer.BackColor = System.Drawing.SystemColors.Control;
                            //m_btnSoRefer.Enabled = false;
                            //m_bpCD_EXPORT.Focus();
                        }
                        obj.Dispose();

					}
					else if(box.Name == "m_btnSoRefer")	// ���� S/O ��ȣ
					{
                        // ��Ŵ� �˸°� �����ؾ� ��

                        ////trade.P_TR_EXSONO_SUB obj = new trade.P_TR_EXSONO_SUB(this.MainFrameInterface, "P_TR_EXINV");
                        //trade.P_TR_EXSONO_SUB obj = new trade.P_TR_EXSONO_SUB(this.MainFrameInterface, "P_TR_EXINV");

                        //if (obj.ShowDialog() == DialogResult.OK)
                        //{
                        //    this.ApplySORefer(obj.GetSOHeadTable);
                        //    this.m_IsPageActivated = true;
                        //    this.SetControlEnable();
                        //    //S/O��ȣ�� �����߱� ������ �ſ��� ��ȣ�� ���´�.(2003-08-13)
                        //    m_txtLcRefer.Enabled = false;
                        //    m_txtLcRefer.BackColor = System.Drawing.SystemColors.Control;
                        //    m_btnLcRefer.Enabled = false;
                        //    m_bpCD_EXPORT.Focus();
                        //}
                        //obj.Dispose();

					}
					
				}
			}
			catch(System.Exception ex)
			{
				this.Cursor = Cursors.Default;
				Cursor.Current = Cursors.Default;

				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.Cursor = Cursors.Default;
				Cursor.Current = Cursors.Default;
			}
		}


		#endregion

		#region �� TaxtBox KeyDown �̺�Ʈ

		/// <summary>
		/// TextBox KeyDown �̺�Ʈ ó��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTextBoxKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				Cursor.Current = Cursors.WaitCursor;

				if(Keys.Enter == e.KeyData)
				{
					System.Windows.Forms.SendKeys.SendWait("{TAB}");
				}
				else if(Keys.Up == e.KeyData)
				{
					System.Windows.Forms.SendKeys.SendWait("+{TAB}");
				}
				else if(Keys.Down == e.KeyData)
				{
					System.Windows.Forms.SendKeys.SendWait("{TAB}");
				}
                else if (Keys.F3 == e.KeyData)
                {
                    if (sender is TextBox)
                    {
                        TextBox box = (TextBox)sender;

                        switch (box.Name)
                        {
                            ////�����ſ����ȣ
                            //case "m_txtLcRefer":
                            //    OpenSubPage(m_btnLcRefer, e);
                            //    break;

                            ////����S/O��ȣ
                            //case "m_txtSoRefer":
                            //    OpenSubPage(m_btnSoRefer, e);
                            //    break;
                        }

                    }
                }
			}
			catch(System.Exception ex)
			{
				this.Cursor = Cursors.Default;
				Cursor.Current = Cursors.Default;

				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{
				this.Cursor = Cursors.Default;
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion

        #region �� ����â �̺�Ʈ / �޼ҵ�

 
      /// <summary>
      /// ������ ��ȿ�� üũ
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void OnDateValidating(object sender, System.ComponentModel.CancelEventArgs e)
      {
          Duzon.Common.Controls.MaskedEditBox maskBox = (Duzon.Common.Controls.MaskedEditBox)sender;

          if (maskBox.ClipText.Trim().Length == 0)
              return;

          // ��¥ ��ȿ�� üũ
          try
          {
              Convert.ToDateTime(maskBox.Text);
          }
          catch
          {
              // ������ �� Validating üũ�� �� ���� �� ��� �������� �ʱ� ����.
              this.m_IsSave = false;

              // MA_M000084
              // ��¥ ������ �߸��Ǿ����ϴ�.
              string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000084");
              DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);

              e.Cancel = true;
              maskBox.Text = "";
              return;
          }
      }



        #endregion

		#region �� ������� �κ�

		/// <summary>
		/// ������� �κ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnModText_Click(object sender, System.EventArgs e)
		{
			try
			{
				string msg = null;

				if(!this.m_IsPageActivated)
					return;

				if(this.m_HeadTable == null)
					return;

				this.m_Manager.Position = 0;

				DataTable ldt_table = this.m_HeadTable.GetChanges();
				if(ldt_table != null)
				{
					// TR_M000036
					// �۾��Ͻ� �ڷḦ ���� �����ϼž� �մϴ�. ����Ͻðڽ��ϱ�?"
					msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000036");
					DialogResult result = MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if(result == DialogResult.Yes)
					{
						if(!this.Save())
							return;
					}
					else if(result == DialogResult.No)
					{
						return;
					}

				}

				object[] args = new object[5];
				args[0] = this.m_HeadTable;
				args[1] = new EventHandler(this.SetAmEx);

				DataRow row = ((DataRowView)this.cbo�ŷ�����.SelectedItem).Row;
				args[2] = row["NAME"].ToString();
				
				row = ((DataRowView)this.cbo��ȭ.SelectedItem).Row;
				args[3] = row["NAME"].ToString();

				row = ((DataRowView)this.cbo�߷�����.SelectedItem).Row;
				args[4] = row["NAME"].ToString();

	
			
				// Main �� ��� �ִ��� Ȯ������ ��� ������ ������ �����ϰ� �׾� ������ �׳� ���Ͻ��ѹ�����.
				if(this.MainFrameInterface.IsExistPage("P_TR_EXINVL", false))
				{
					//- Ư�� ������ �ݱ�
					this.UnLoadPage("P_TR_EXINVL", false);
				}


				string ls_LinePageName = this.MainFrameInterface.GetDataDictionaryItem("TRE", "MOD_TEXT");

				
				this.LoadPageFrom("P_TR_EXINVL", ls_LinePageName, this.Grant, args);
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		///  ������Ͽ��� ��ȭ�ݾװ� ��ȭ�ݾ��� �ٽ� ����� �� ȣ���Ѵ�.
		/// </summary>
		/// <param name="values"></param>
		/// <param name="e"></param>
		public void SetAmEx(object values, EventArgs e)
		{
			try
			{
				this.m_HeadTable.Rows[0].BeginEdit();
				this.m_HeadTable.Rows[0]["AM_EX"] = values;
				this.m_HeadTable.Rows[0].EndEdit();

				this.m_HeadTable.AcceptChanges();

			}
			catch
			{
			}
		}

		#endregion
	
		#region �� �Ǹ� ��� �κ�

		/// <summary>
		/// �Ǹ� ��� ����â
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnInputCost_Click(object sender, System.EventArgs e)
		{
			try
			{
    			string msg = null;

				if(!this.m_IsPageActivated)
					return;

				if(this.m_HeadTable == null)
					return;

				this.m_Manager.Position = 0;

				DataTable ldt_table = this.m_HeadTable.GetChanges();
				if(ldt_table != null)
				{
					// TR_M000036
					// �۾��Ͻ� �ڷḦ ���� �����ϼž� �մϴ�. ����Ͻðڽ��ϱ�?"
					msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000036");
					DialogResult result = MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if(result == DialogResult.Yes)
					{
						if(!this.Save())
							return;
					}
					else if(result == DialogResult.No)
					{
						return;
					}
				}

				// ���߻�����, ������ȣ, ��ǥ����, ��ǥ������ڵ�,
				// �μ��ڵ�, �μ���, ������ڵ� ,����ڸ�, C/C �ڵ�, C/C��
				string[] ls_args = new string[11];
				ls_args[0] = "����";
				ls_args[1] = this.m_HeadTable.Rows[0]["NO_INV"].ToString();
                ls_args[2] = this.dtp��������.Text;	// ��������
				ls_args[3] = this.m_CdBizarea;
				ls_args[4] = "";
				ls_args[5] = "";
				ls_args[6] = this.m_CdEmp;
				ls_args[7] = this.m_NmEmp;
				ls_args[8] = "";
				ls_args[9] = "";

				//public P_TR_EXCOST(string[] p_args, DataTable p_originTable)

				object[] args = new Object[3];
				args[0] = ls_args;
				args[1] = this.m_HeadTable;
				args[2] = 2;	// ���� : 1 , L/C : 1
			
				// Main �� ��� �ִ��� Ȯ������ ��� ������ ������ �����ϰ� �׾� ������ �׳� ���Ͻ��ѹ�����.
				if(this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
				{
					//- Ư�� ������ �ݱ�
					this.UnLoadPage("P_TR_EXCOST", false);
				}

				string ls_LinePageName = this.MainFrameInterface.GetDataDictionaryItem("TRE", "INPUT_COST");
				
				this.LoadPageFrom("P_TR_EXCOST", ls_LinePageName, this.Grant, args);
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		#endregion

		#region �� Ű �̺�Ʈ ó�� �κ�


		//++++++++++++++++++++++++++++++++++++++++++++++++++++<< �޺��ڽ� Ű �̺�Ʈ ó�� >>
		
		/// <summary>
		/// �޺� �ڽ� ���� Ű �̺�Ʈ ó��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="backControl"></param>
		/// <param name="nextControl"></param>
		private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
		}
		
		
		
		//+++++++++++++++++++++++++++++++++++++++++++++++++++++<< �Ϲ� �ؽ�Ʈ �ڽ� Ű �̺�Ʈ >>

		/// <summary>
		/// ���� Ű �̺�Ʈ ó��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommonTextBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
			else if(e.KeyData == Keys.Up)
				System.Windows.Forms.SendKeys.SendWait("+{TAB}");
			else if(e.KeyData == Keys.Down)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
		}



		#endregion


        #region �� control Ű �̺�Ʈ �� ���� ����

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.ControlName)
            {
                case "m_bpCD_PARTNER":
                    this.m_HeadTable.Rows[0]["CD_PARTNER"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_PARTNER"] = e.HelpReturn.CodeName;
                    this.m_NmPartner = e.HelpReturn.CodeName;
                    this.m_CdPartner = e.HelpReturn.CodeValue;
                    break;
                case "m_bpCD_SALEGRP":
                    this.m_HeadTable.Rows[0]["CD_SALEGRP"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_SALEGRP"] = e.HelpReturn.CodeName;
                    m_NmGroup = e.HelpReturn.CodeName;
                    m_CdGroup = e.HelpReturn.CodeValue;
                    break;
                case "m_bpCD_EXPORT":
                    m_CdExport = e.HelpReturn.CodeValue;
                    m_NmExport = e.HelpReturn.CodeName;
                    this.m_HeadTable.Rows[0]["CD_EXPORT"] = this.m_CdExport;
                    this.m_HeadTable.Rows[0]["NM_EXPORT"] = this.m_NmExport;
                    break;
                case "m_bpCD_BIZAREA":
                    this.m_HeadTable.Rows[0]["CD_BIZAREA"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_BIZAREA"] = e.HelpReturn.CodeName;
                    break;
                case "m_bpNO_EMP":
                    this.m_HeadTable.Rows[0]["NO_EMP"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_KOR"] = e.HelpReturn.CodeName;
                    this.m_CdEmp = e.HelpReturn.CodeValue;
                    this.m_NmEmp = e.HelpReturn.CodeName;
                    break;
                case "m_bpCD_PRODUCT":
                    this.m_HeadTable.Rows[0]["CD_PRODUCT"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_PRODUCT"] = e.HelpReturn.CodeName;
                    this.m_NmPRODUCT = e.HelpReturn.CodeName;
                    this.m_CdPRODUCT = e.HelpReturn.CodeValue;
                    break;
                case "m_bpCD_AGENT":
                    m_CdAgent = e.HelpReturn.CodeValue;
                    m_NmAgent = e.HelpReturn.CodeName;
                    this.m_HeadTable.Rows[0]["CD_AGENT"] = this.m_CdAgent;
                    this.m_HeadTable.Rows[0]["NM_AGENT"] = this.m_NmAgent;
                    break;
                case "m_bpSHIP_CORP":
                    m_CdShipCorp = e.HelpReturn.CodeValue;
                    m_NmShipCorp = e.HelpReturn.CodeName;
                    this.m_HeadTable.Rows[0]["SHIP_CORP"] = this.m_CdShipCorp;
                    this.m_HeadTable.Rows[0]["NM_SHIP_CORP"] = this.m_NmShipCorp;
                    break;
                case "m_bpCD_CUST_IN":
                    m_CdCustIn = e.HelpReturn.CodeValue;
                    m_NmCustIn = e.HelpReturn.CodeName;
                    this.m_HeadTable.Rows[0]["CD_CUST_IN"] = this.m_CdCustIn;
                    this.m_HeadTable.Rows[0]["NM_CUST_IN"] = this.m_NmCustIn;
                    break;

            }

        }

        #endregion

        private void dtp����������_Click(object sender, EventArgs e)
        {

        }

	}
}
