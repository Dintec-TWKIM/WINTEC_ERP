//********************************************************************
// ��   ��   �� : ���ȸ ## 
// ��   ��   �� : 
// ��   ��   �� : ������/������(2006-07-12)
// ��   ��   �� : ��������
// �� ��  �� �� : �������
// ����ý��۸� : ���⼱�����
// �� �� ��  �� : 
// ������Ʈ  �� : P_TR_EXBL
//********************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;

using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Util;

namespace trade
{
     public class P_TR_EXBL_BAK : Duzon.Common.Forms.PageBase
    {
        #region �� �ʱ�ȭ �ڵ� ���� �κ�

        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.Controls.PanelExt panel12;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.PanelExt panel8;
        private Duzon.Common.Controls.PanelExt panel9;
        private Duzon.Common.Controls.PanelExt panel11;
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
        private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.PanelExt panel10;
        private Duzon.Common.Controls.RoundedButton btn�ǸŰ����;
        private Duzon.Common.Controls.RoundedButton btn��������;
        private Duzon.Common.Controls.DzComboBox cbo��������;
        private Duzon.Common.Controls.CurrencyTextBox txtDAYS;
        private Duzon.Common.Controls.CurrencyTextBox cur��ȭ�ݾ�;
        private Duzon.Common.Controls.CurrencyTextBox cur��ǥ�ݾ�;
        private Duzon.Common.Controls.CurrencyTextBox cur��ǥȯ��;
        private Duzon.Common.Controls.TextBoxExt txt���1;
        private Duzon.Common.Controls.DzComboBox cboLC����;
        private Duzon.Common.Controls.TextBoxExt txt�����ȣ;
        private Duzon.Common.Controls.LabelExt lblDAYS;
        private Duzon.Common.Controls.DzComboBox cbo��������;
         private Duzon.Common.Controls.DzComboBox cbo��ȭ;
        private Duzon.Common.Controls.TextBoxExt txt������ȣ;
        private Duzon.Common.Controls.TextBoxExt txtVESSEL��;
        private Duzon.Common.Controls.TextBoxExt txt���3;
        private Duzon.Common.Controls.TextBoxExt txt���2;
        private Duzon.Common.Controls.TextBoxExt txt������;
        private Duzon.Common.Controls.TextBoxExt txt������;
        private Duzon.Common.Controls.LabelExt lbl�ŷ�ó;
        private Duzon.Common.Controls.LabelExt lbl��ȭ�ݾ�;
        private Duzon.Common.Controls.LabelExt lbl��ǥ�ݾ�;
        private Duzon.Common.Controls.LabelExt lbl��ǥȯ��;
        private Duzon.Common.Controls.LabelExt lbl����;
        private Duzon.Common.Controls.LabelExt lbl��������;
        private Duzon.Common.Controls.LabelExt lbl�����׷�;
        private Duzon.Common.Controls.LabelExt lbl�����Ű��ȣ;
        private Duzon.Common.Controls.LabelExt lbl�����;
        private Duzon.Common.Controls.LabelExt lbl������;
        private Duzon.Common.Controls.LabelExt lbl������;
        private Duzon.Common.Controls.LabelExt lbl��������;
        private Duzon.Common.Controls.LabelExt lbl���;
        private Duzon.Common.Controls.LabelExt lbl������;
        private Duzon.Common.Controls.LabelExt lbl������;
        private Duzon.Common.Controls.LabelExt lblVESSEL��;
        private Duzon.Common.Controls.LabelExt lbl�����ȣ;
        private Duzon.Common.Controls.LabelExt lbl��������;
        private Duzon.Common.Controls.LabelExt lbl��ȭ;
        private Duzon.Common.Controls.LabelExt lbl������������;
        private Duzon.Common.Controls.LabelExt lbl��ǥ����;
        private Duzon.Common.Controls.LabelExt lbl�����;
        private Duzon.Common.Controls.LabelExt lbl������ȣ;
        private Duzon.Common.Controls.LabelExt lbl��������;
        private Duzon.Common.Controls.LabelExt lbl����������;
        private Duzon.Common.Controls.LabelExt lblLC����;
         private Duzon.Common.Controls.RoundedButton btn������ǥó��;
        private Duzon.Common.Controls.DzComboBox cbo������;
        private Duzon.Common.Controls.DzComboBox cbo��������;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�����;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�����׷�;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�ŷ�ó;
        private Duzon.Common.BpControls.BpCodeTextBox bpc������;
        private Duzon.Common.BpControls.BpCodeTextBox bpc����;
        private Duzon.Common.BpControls.BpCodeTextBox bpc�����;
        private DatePicker dtp��ǥ����;
        private DatePicker dtp��������;
        private DatePicker dtp������������;
        private DatePicker dtp����������;
         private PanelExt panelExt1;
         private TableLayoutPanel tableLayoutPanel1;
         private TextButton tbtn�����Ű��ȣ;
        private System.ComponentModel.IContainer components;

        #endregion

        /// <summary> 
        /// ��� ���� ��� ���ҽ��� �����մϴ�.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// �����̳� ������ �ʿ��� �޼����Դϴ�. 
        /// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXBL_BAK));
            this.btn�ǸŰ���� = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn�������� = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.tbtn�����Ű��ȣ = new Duzon.Common.Controls.TextButton();
            this.dtp���������� = new Duzon.Common.Controls.DatePicker();
            this.dtp������������ = new Duzon.Common.Controls.DatePicker();
            this.dtp�������� = new Duzon.Common.Controls.DatePicker();
            this.dtp��ǥ���� = new Duzon.Common.Controls.DatePicker();
            this.bpc����� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc���� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc������ = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc�ŷ�ó = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc�����׷� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc����� = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cbo�������� = new Duzon.Common.Controls.DzComboBox();
            this.cbo������ = new Duzon.Common.Controls.DzComboBox();
            this.cbo�������� = new Duzon.Common.Controls.DzComboBox();
            this.txtDAYS = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur��ȭ�ݾ� = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur��ǥ�ݾ� = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur��ǥȯ�� = new Duzon.Common.Controls.CurrencyTextBox();
            this.txt���1 = new Duzon.Common.Controls.TextBoxExt();
            this.cboLC���� = new Duzon.Common.Controls.DzComboBox();
            this.txt�����ȣ = new Duzon.Common.Controls.TextBoxExt();
            this.lblDAYS = new Duzon.Common.Controls.LabelExt();
            this.cbo�������� = new Duzon.Common.Controls.DzComboBox();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.cbo��ȭ = new Duzon.Common.Controls.DzComboBox();
            this.txt������ȣ = new Duzon.Common.Controls.TextBoxExt();
            this.txtVESSEL�� = new Duzon.Common.Controls.TextBoxExt();
            this.txt���3 = new Duzon.Common.Controls.TextBoxExt();
            this.txt���2 = new Duzon.Common.Controls.TextBoxExt();
            this.txt������ = new Duzon.Common.Controls.TextBoxExt();
            this.txt������ = new Duzon.Common.Controls.TextBoxExt();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
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
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lbl�ŷ�ó = new Duzon.Common.Controls.LabelExt();
            this.lbl��ȭ�ݾ� = new Duzon.Common.Controls.LabelExt();
            this.lbl��ǥ�ݾ� = new Duzon.Common.Controls.LabelExt();
            this.lbl��ǥȯ�� = new Duzon.Common.Controls.LabelExt();
            this.lbl���� = new Duzon.Common.Controls.LabelExt();
            this.lbl�������� = new Duzon.Common.Controls.LabelExt();
            this.lbl�����׷� = new Duzon.Common.Controls.LabelExt();
            this.lbl�����Ű��ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl����� = new Duzon.Common.Controls.LabelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl�������� = new Duzon.Common.Controls.LabelExt();
            this.lbl��� = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lbl������ = new Duzon.Common.Controls.LabelExt();
            this.lblVESSEL�� = new Duzon.Common.Controls.LabelExt();
            this.lbl�����ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl�������� = new Duzon.Common.Controls.LabelExt();
            this.lbl��ȭ = new Duzon.Common.Controls.LabelExt();
            this.lbl������������ = new Duzon.Common.Controls.LabelExt();
            this.lbl��ǥ���� = new Duzon.Common.Controls.LabelExt();
            this.lbl����� = new Duzon.Common.Controls.LabelExt();
            this.lbl������ȣ = new Duzon.Common.Controls.LabelExt();
            this.lbl�������� = new Duzon.Common.Controls.LabelExt();
            this.lbl���������� = new Duzon.Common.Controls.LabelExt();
            this.lblLC���� = new Duzon.Common.Controls.LabelExt();
            this.btn������ǥó�� = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp����������)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp������������)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp��������)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp��ǥ����)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDAYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ȭ�ݾ�)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ǥ�ݾ�)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ǥȯ��)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn�ǸŰ����
            // 
            this.btn�ǸŰ����.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn�ǸŰ����.BackColor = System.Drawing.Color.White;
            this.btn�ǸŰ����.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn�ǸŰ����.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn�ǸŰ����.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn�ǸŰ����.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn�ǸŰ����.Location = new System.Drawing.Point(563, 3);
            this.btn�ǸŰ����.Name = "btn�ǸŰ����";
            this.btn�ǸŰ����.Size = new System.Drawing.Size(120, 24);
            this.btn�ǸŰ����.TabIndex = 133;
            this.btn�ǸŰ����.TabStop = false;
            this.btn�ǸŰ����.Text = "�ǸŰ����";
            this.btn�ǸŰ����.UseVisualStyleBackColor = false;
            this.btn�ǸŰ����.Click += new System.EventHandler(this.m_btnInputCost_Click);
            // 
            // btn��������
            // 
            this.btn��������.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn��������.BackColor = System.Drawing.Color.White;
            this.btn��������.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn��������.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn��������.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn��������.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn��������.Location = new System.Drawing.Point(685, 3);
            this.btn��������.Name = "btn��������";
            this.btn��������.Size = new System.Drawing.Size(100, 24);
            this.btn��������.TabIndex = 132;
            this.btn��������.TabStop = false;
            this.btn��������.Text = "��������";
            this.btn��������.UseVisualStyleBackColor = false;
            this.btn��������.Click += new System.EventHandler(this.m_btnBLText_Click);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tbtn�����Ű��ȣ);
            this.panel4.Controls.Add(this.dtp����������);
            this.panel4.Controls.Add(this.dtp������������);
            this.panel4.Controls.Add(this.dtp��������);
            this.panel4.Controls.Add(this.dtp��ǥ����);
            this.panel4.Controls.Add(this.bpc�����);
            this.panel4.Controls.Add(this.bpc����);
            this.panel4.Controls.Add(this.bpc������);
            this.panel4.Controls.Add(this.bpc�ŷ�ó);
            this.panel4.Controls.Add(this.bpc�����׷�);
            this.panel4.Controls.Add(this.bpc�����);
            this.panel4.Controls.Add(this.cbo��������);
            this.panel4.Controls.Add(this.cbo������);
            this.panel4.Controls.Add(this.cbo��������);
            this.panel4.Controls.Add(this.txtDAYS);
            this.panel4.Controls.Add(this.cur��ȭ�ݾ�);
            this.panel4.Controls.Add(this.cur��ǥ�ݾ�);
            this.panel4.Controls.Add(this.cur��ǥȯ��);
            this.panel4.Controls.Add(this.txt���1);
            this.panel4.Controls.Add(this.cboLC����);
            this.panel4.Controls.Add(this.txt�����ȣ);
            this.panel4.Controls.Add(this.lblDAYS);
            this.panel4.Controls.Add(this.cbo��������);
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.cbo��ȭ);
            this.panel4.Controls.Add(this.txt������ȣ);
            this.panel4.Controls.Add(this.txtVESSEL��);
            this.panel4.Controls.Add(this.txt���3);
            this.panel4.Controls.Add(this.txt���2);
            this.panel4.Controls.Add(this.txt������);
            this.panel4.Controls.Add(this.txt������);
            this.panel4.Controls.Add(this.panel12);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel11);
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
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 38);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 520);
            this.panel4.TabIndex = 130;
            // 
            // tbtn�����Ű��ȣ
            // 
            this.tbtn�����Ű��ȣ.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tbtn�����Ű��ȣ.ButtonImage")));
            this.tbtn�����Ű��ȣ.Location = new System.Drawing.Point(510, 3);
            this.tbtn�����Ű��ȣ.Name = "tbtn�����Ű��ȣ";
            this.tbtn�����Ű��ȣ.Size = new System.Drawing.Size(267, 21);
            this.tbtn�����Ű��ȣ.TabIndex = 204;
            this.tbtn�����Ű��ȣ.Tag = "NO_TO";
            this.tbtn�����Ű��ȣ.Text = "textButton1";
            // 
            // dtp����������
            // 
            this.dtp����������.BackColor = System.Drawing.Color.White;
            this.dtp����������.CalendarBackColor = System.Drawing.Color.White;
            this.dtp����������.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp����������.FriDayColor = System.Drawing.Color.Blue;
            this.dtp����������.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp����������.Location = new System.Drawing.Point(121, 253);
            this.dtp����������.Mask = "####/##/##";
            this.dtp����������.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
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
            this.dtp����������.TabIndex = 20;
            this.dtp����������.Tag = "DT_PAYABLE";
            this.dtp����������.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp����������.TitleForeColor = System.Drawing.Color.Black;
            this.dtp����������.ToDayColor = System.Drawing.Color.Red;
            this.dtp����������.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp����������.UseKeyF3 = false;
            this.dtp����������.Value = new System.DateTime(((long)(0)));
            // 
            // dtp������������
            // 
            this.dtp������������.BackColor = System.Drawing.Color.White;
            this.dtp������������.CalendarBackColor = System.Drawing.Color.White;
            this.dtp������������.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp������������.FriDayColor = System.Drawing.Color.Blue;
            this.dtp������������.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp������������.Location = new System.Drawing.Point(121, 103);
            this.dtp������������.Mask = "####/##/##";
            this.dtp������������.MaskBackColor = System.Drawing.Color.White;
            this.dtp������������.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp������������.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp������������.Modified = false;
            this.dtp������������.Name = "dtp������������";
            this.dtp������������.PaddingCharacter = '_';
            this.dtp������������.PassivePromptCharacter = '_';
            this.dtp������������.PromptCharacter = '_';
            this.dtp������������.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp������������.ShowToDay = true;
            this.dtp������������.ShowTodayCircle = true;
            this.dtp������������.ShowUpDown = false;
            this.dtp������������.Size = new System.Drawing.Size(87, 21);
            this.dtp������������.SunDayColor = System.Drawing.Color.Red;
            this.dtp������������.TabIndex = 8;
            this.dtp������������.Tag = "DT_ARRIVAL";
            this.dtp������������.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp������������.TitleForeColor = System.Drawing.Color.Black;
            this.dtp������������.ToDayColor = System.Drawing.Color.Red;
            this.dtp������������.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp������������.UseKeyF3 = false;
            this.dtp������������.Value = new System.DateTime(((long)(0)));
            // 
            // dtp��������
            // 
            this.dtp��������.BackColor = System.Drawing.Color.White;
            this.dtp��������.CalendarBackColor = System.Drawing.Color.White;
            this.dtp��������.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp��������.FriDayColor = System.Drawing.Color.Blue;
            this.dtp��������.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp��������.Location = new System.Drawing.Point(510, 78);
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
            this.dtp��������.TabIndex = 7;
            this.dtp��������.Tag = "DT_LOADING";
            this.dtp��������.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp��������.TitleForeColor = System.Drawing.Color.Black;
            this.dtp��������.ToDayColor = System.Drawing.Color.Red;
            this.dtp��������.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp��������.UseKeyF3 = false;
            this.dtp��������.Value = new System.DateTime(((long)(0)));
            // 
            // dtp��ǥ����
            // 
            this.dtp��ǥ����.BackColor = System.Drawing.Color.White;
            this.dtp��ǥ����.CalendarBackColor = System.Drawing.Color.White;
            this.dtp��ǥ����.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp��ǥ����.FriDayColor = System.Drawing.Color.Blue;
            this.dtp��ǥ����.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp��ǥ����.Location = new System.Drawing.Point(121, 78);
            this.dtp��ǥ����.Mask = "####/##/##";
            this.dtp��ǥ����.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp��ǥ����.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp��ǥ����.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp��ǥ����.Modified = false;
            this.dtp��ǥ����.Name = "dtp��ǥ����";
            this.dtp��ǥ����.PaddingCharacter = '_';
            this.dtp��ǥ����.PassivePromptCharacter = '_';
            this.dtp��ǥ����.PromptCharacter = '_';
            this.dtp��ǥ����.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp��ǥ����.ShowToDay = true;
            this.dtp��ǥ����.ShowTodayCircle = true;
            this.dtp��ǥ����.ShowUpDown = false;
            this.dtp��ǥ����.Size = new System.Drawing.Size(87, 21);
            this.dtp��ǥ����.SunDayColor = System.Drawing.Color.Red;
            this.dtp��ǥ����.TabIndex = 6;
            this.dtp��ǥ����.Tag = "DT_BALLOT";
            this.dtp��ǥ����.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp��ǥ����.TitleForeColor = System.Drawing.Color.Black;
            this.dtp��ǥ����.ToDayColor = System.Drawing.Color.Red;
            this.dtp��ǥ����.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp��ǥ����.UseKeyF3 = false;
            this.dtp��ǥ����.Value = new System.DateTime(((long)(0)));
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
            this.bpc�����.Location = new System.Drawing.Point(121, 53);
            this.bpc�����.Name = "bpc�����";
            this.bpc�����.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�����.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�����.SearchCode = true;
            this.bpc�����.SelectCount = 0;
            this.bpc�����.SetDefaultValue = true;
            this.bpc�����.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�����.Size = new System.Drawing.Size(267, 21);
            this.bpc�����.TabIndex = 4;
            this.bpc�����.Tag = "CD_BIZAREA";
            this.bpc�����.Text = "bpCodeTextBox1";
            this.bpc�����.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc�����.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc����.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BANK_SUB;
            this.bpc����.ItemBackColor = System.Drawing.Color.White;
            this.bpc����.Location = new System.Drawing.Point(510, 128);
            this.bpc����.Name = "bpc����";
            this.bpc����.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc����.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc����.SearchCode = true;
            this.bpc����.SelectCount = 0;
            this.bpc����.SetDefaultValue = false;
            this.bpc����.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc����.Size = new System.Drawing.Size(267, 21);
            this.bpc����.TabIndex = 11;
            this.bpc����.Tag = "SHIP_CORP";
            this.bpc����.Text = "bpCodeTextBox1";
            this.bpc����.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
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
            this.bpc������.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BANK_SUB;
            this.bpc������.ItemBackColor = System.Drawing.Color.White;
            this.bpc������.Location = new System.Drawing.Point(121, 128);
            this.bpc������.Name = "bpc������";
            this.bpc������.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc������.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc������.SearchCode = true;
            this.bpc������.SelectCount = 0;
            this.bpc������.SetDefaultValue = false;
            this.bpc������.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc������.Size = new System.Drawing.Size(267, 21);
            this.bpc������.TabIndex = 10;
            this.bpc������.Tag = "CD_EXPORT";
            this.bpc������.Text = "bpCodeTextBox1";
            this.bpc������.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc������.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc�ŷ�ó.ItemBackColor = System.Drawing.Color.White;
            this.bpc�ŷ�ó.Location = new System.Drawing.Point(510, 103);
            this.bpc�ŷ�ó.Name = "bpc�ŷ�ó";
            this.bpc�ŷ�ó.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�ŷ�ó.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�ŷ�ó.SearchCode = true;
            this.bpc�ŷ�ó.SelectCount = 0;
            this.bpc�ŷ�ó.SetDefaultValue = false;
            this.bpc�ŷ�ó.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�ŷ�ó.Size = new System.Drawing.Size(268, 21);
            this.bpc�ŷ�ó.TabIndex = 9;
            this.bpc�ŷ�ó.Tag = "CD_PARTNER";
            this.bpc�ŷ�ó.Text = "bpCodeTextBox1";
            this.bpc�ŷ�ó.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc�ŷ�ó.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc�����׷�
            // 
            this.bpc�����׷�.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc�����׷�.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc�����׷�.ButtonImage")));
            this.bpc�����׷�.ChildMode = "";
            this.bpc�����׷�.CodeName = "";
            this.bpc�����׷�.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc�����׷�.CodeValue = "";
            this.bpc�����׷�.ComboCheck = true;
            this.bpc�����׷�.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpc�����׷�.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc�����׷�.Location = new System.Drawing.Point(510, 28);
            this.bpc�����׷�.Name = "bpc�����׷�";
            this.bpc�����׷�.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�����׷�.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�����׷�.SearchCode = true;
            this.bpc�����׷�.SelectCount = 0;
            this.bpc�����׷�.SetDefaultValue = true;
            this.bpc�����׷�.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�����׷�.Size = new System.Drawing.Size(268, 21);
            this.bpc�����׷�.TabIndex = 3;
            this.bpc�����׷�.Tag = "CD_SALEGRP";
            this.bpc�����׷�.Text = "bpCodeTextBox1";
            this.bpc�����׷�.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc�����׷�.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc�����.Location = new System.Drawing.Point(510, 53);
            this.bpc�����.Name = "bpc�����";
            this.bpc�����.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc�����.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc�����.SearchCode = true;
            this.bpc�����.SelectCount = 0;
            this.bpc�����.SetDefaultValue = true;
            this.bpc�����.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc�����.Size = new System.Drawing.Size(268, 21);
            this.bpc�����.TabIndex = 5;
            this.bpc�����.Tag = "NO_EMP";
            this.bpc�����.Text = "bpCodeTextBox1";
            this.bpc�����.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc�����.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // cbo��������
            // 
            this.cbo��������.AutoDropDown = true;
            this.cbo��������.BackColor = System.Drawing.Color.White;
            this.cbo��������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��������.Location = new System.Drawing.Point(121, 204);
            this.cbo��������.Name = "cbo��������";
            this.cbo��������.Size = new System.Drawing.Size(267, 20);
            this.cbo��������.TabIndex = 16;
            this.cbo��������.Tag = "COND_SHIPMENT";
            this.cbo��������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo������
            // 
            this.cbo������.AutoDropDown = true;
            this.cbo������.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo������.Location = new System.Drawing.Point(121, 178);
            this.cbo������.Name = "cbo������";
            this.cbo������.Size = new System.Drawing.Size(267, 20);
            this.cbo������.TabIndex = 14;
            this.cbo������.Tag = "PORT_NATION";
            this.cbo������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo��������
            // 
            this.cbo��������.AutoDropDown = true;
            this.cbo��������.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo��������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��������.Location = new System.Drawing.Point(121, 29);
            this.cbo��������.Name = "cbo��������";
            this.cbo��������.Size = new System.Drawing.Size(267, 20);
            this.cbo��������.TabIndex = 2;
            this.cbo��������.Tag = "FG_BL";
            this.cbo��������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txtDAYS
            // 
            this.txtDAYS.BackColor = System.Drawing.Color.White;
            this.txtDAYS.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDAYS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDAYS.Location = new System.Drawing.Point(390, 228);
            this.txtDAYS.Mask = null;
            this.txtDAYS.MaxLength = 4;
            this.txtDAYS.Name = "txtDAYS";
            this.txtDAYS.NullString = "0";
            this.txtDAYS.PositiveColor = System.Drawing.Color.Black;
            this.txtDAYS.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDAYS.Size = new System.Drawing.Size(100, 21);
            this.txtDAYS.TabIndex = 19;
            this.txtDAYS.Tag = "COND_DAYS";
            this.txtDAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDAYS.UseKeyEnter = true;
            this.txtDAYS.UseKeyF3 = true;
            this.txtDAYS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
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
            this.cur��ȭ�ݾ�.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur��ȭ�ݾ�.Location = new System.Drawing.Point(510, 178);
            this.cur��ȭ�ݾ�.Mask = null;
            this.cur��ȭ�ݾ�.MaxLength = 22;
            this.cur��ȭ�ݾ�.Name = "cur��ȭ�ݾ�";
            this.cur��ȭ�ݾ�.NullString = "0";
            this.cur��ȭ�ݾ�.PositiveColor = System.Drawing.Color.Black;
            this.cur��ȭ�ݾ�.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur��ȭ�ݾ�.Size = new System.Drawing.Size(186, 21);
            this.cur��ȭ�ݾ�.TabIndex = 15;
            this.cur��ȭ�ݾ�.Tag = "AM_EX";
            this.cur��ȭ�ݾ�.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur��ȭ�ݾ�.UseKeyEnter = true;
            this.cur��ȭ�ݾ�.UseKeyF3 = true;
            this.cur��ȭ�ݾ�.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cur��ǥ�ݾ�
            // 
            this.cur��ǥ�ݾ�.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cur��ǥ�ݾ�.CurrencyDecimalDigits = 4;
            this.cur��ǥ�ݾ�.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur��ǥ�ݾ�.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur��ǥ�ݾ�.Location = new System.Drawing.Point(510, 203);
            this.cur��ǥ�ݾ�.Mask = null;
            this.cur��ǥ�ݾ�.MaxLength = 22;
            this.cur��ǥ�ݾ�.Name = "cur��ǥ�ݾ�";
            this.cur��ǥ�ݾ�.NullString = "0";
            this.cur��ǥ�ݾ�.PositiveColor = System.Drawing.Color.Black;
            this.cur��ǥ�ݾ�.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur��ǥ�ݾ�.Size = new System.Drawing.Size(186, 21);
            this.cur��ǥ�ݾ�.TabIndex = 17;
            this.cur��ǥ�ݾ�.Tag = "AM";
            this.cur��ǥ�ݾ�.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur��ǥ�ݾ�.UseKeyEnter = true;
            this.cur��ǥ�ݾ�.UseKeyF3 = true;
            this.cur��ǥ�ݾ�.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cur��ǥȯ��
            // 
            this.cur��ǥȯ��.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cur��ǥȯ��.CurrencyDecimalDigits = 4;
            this.cur��ǥȯ��.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur��ǥȯ��.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur��ǥȯ��.Location = new System.Drawing.Point(510, 153);
            this.cur��ǥȯ��.Mask = null;
            this.cur��ǥȯ��.MaxLength = 20;
            this.cur��ǥȯ��.Name = "cur��ǥȯ��";
            this.cur��ǥȯ��.NullString = "0";
            this.cur��ǥȯ��.PositiveColor = System.Drawing.Color.Black;
            this.cur��ǥȯ��.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur��ǥȯ��.Size = new System.Drawing.Size(186, 21);
            this.cur��ǥȯ��.TabIndex = 13;
            this.cur��ǥȯ��.Tag = "RT_EXCH";
            this.cur��ǥȯ��.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur��ǥȯ��.UseKeyEnter = true;
            this.cur��ǥȯ��.UseKeyF3 = true;
            this.cur��ǥȯ��.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt���1
            // 
            this.txt���1.BackColor = System.Drawing.Color.White;
            this.txt���1.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���1.Location = new System.Drawing.Point(120, 402);
            this.txt���1.MaxLength = 100;
            this.txt���1.Name = "txt���1";
            this.txt���1.SelectedAllEnabled = false;
            this.txt���1.Size = new System.Drawing.Size(660, 21);
            this.txt���1.TabIndex = 26;
            this.txt���1.Tag = "REMARK1";
            this.txt���1.UseKeyEnter = true;
            this.txt���1.UseKeyF3 = true;
            this.txt���1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cboLC����
            // 
            this.cboLC����.AutoDropDown = true;
            this.cboLC����.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cboLC����.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLC����.Location = new System.Drawing.Point(121, 303);
            this.cboLC����.Name = "cboLC����";
            this.cboLC����.Size = new System.Drawing.Size(267, 20);
            this.cboLC����.TabIndex = 22;
            this.cboLC����.Tag = "FG_LC";
            this.cboLC����.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt�����ȣ
            // 
            this.txt�����ȣ.BackColor = System.Drawing.Color.White;
            this.txt�����ȣ.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt�����ȣ.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt�����ȣ.Location = new System.Drawing.Point(121, 277);
            this.txt�����ȣ.MaxLength = 20;
            this.txt�����ȣ.Name = "txt�����ȣ";
            this.txt�����ȣ.SelectedAllEnabled = false;
            this.txt�����ȣ.Size = new System.Drawing.Size(180, 21);
            this.txt�����ȣ.TabIndex = 21;
            this.txt�����ȣ.Tag = "NO_INV";
            this.txt�����ȣ.UseKeyEnter = true;
            this.txt�����ȣ.UseKeyF3 = true;
            this.txt�����ȣ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // lblDAYS
            // 
            this.lblDAYS.Location = new System.Drawing.Point(492, 231);
            this.lblDAYS.Name = "lblDAYS";
            this.lblDAYS.Resizeble = true;
            this.lblDAYS.Size = new System.Drawing.Size(100, 18);
            this.lblDAYS.TabIndex = 203;
            this.lblDAYS.Tag = "DT_ILSU";
            this.lblDAYS.Text = "DAYS";
            this.lblDAYS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbo��������
            // 
            this.cbo��������.AutoDropDown = true;
            this.cbo��������.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo��������.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��������.Location = new System.Drawing.Point(121, 228);
            this.cbo��������.Name = "cbo��������";
            this.cbo��������.Size = new System.Drawing.Size(267, 20);
            this.cbo��������.TabIndex = 18;
            this.cbo��������.Tag = "COND_PAY";
            this.cbo��������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel10.BackgroundImage")));
            this.panel10.Location = new System.Drawing.Point(5, 325);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(775, 1);
            this.panel10.TabIndex = 187;
            // 
            // cbo��ȭ
            // 
            this.cbo��ȭ.AutoDropDown = true;
            this.cbo��ȭ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo��ȭ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo��ȭ.Location = new System.Drawing.Point(121, 153);
            this.cbo��ȭ.Name = "cbo��ȭ";
            this.cbo��ȭ.Size = new System.Drawing.Size(267, 20);
            this.cbo��ȭ.TabIndex = 12;
            this.cbo��ȭ.Tag = "CD_EXCH";
            this.cbo��ȭ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt������ȣ
            // 
            this.txt������ȣ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.txt������ȣ.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt������ȣ.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt������ȣ.Location = new System.Drawing.Point(121, 3);
            this.txt������ȣ.MaxLength = 20;
            this.txt������ȣ.Name = "txt������ȣ";
            this.txt������ȣ.SelectedAllEnabled = false;
            this.txt������ȣ.Size = new System.Drawing.Size(150, 21);
            this.txt������ȣ.TabIndex = 0;
            this.txt������ȣ.Tag = "NO_BL";
            this.txt������ȣ.UseKeyEnter = true;
            this.txt������ȣ.UseKeyF3 = true;
            this.txt������ȣ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txtVESSEL��
            // 
            this.txtVESSEL��.BackColor = System.Drawing.Color.White;
            this.txtVESSEL��.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtVESSEL��.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVESSEL��.Location = new System.Drawing.Point(120, 328);
            this.txtVESSEL��.MaxLength = 50;
            this.txtVESSEL��.Name = "txtVESSEL��";
            this.txtVESSEL��.SelectedAllEnabled = false;
            this.txtVESSEL��.Size = new System.Drawing.Size(660, 21);
            this.txtVESSEL��.TabIndex = 23;
            this.txtVESSEL��.Tag = "NM_VESSEL";
            this.txtVESSEL��.UseKeyEnter = true;
            this.txtVESSEL��.UseKeyF3 = true;
            this.txtVESSEL��.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
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
            this.txt���3.TabIndex = 28;
            this.txt���3.Tag = "REMARK3";
            this.txt���3.UseKeyEnter = true;
            this.txt���3.UseKeyF3 = true;
            this.txt���3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt���2
            // 
            this.txt���2.BackColor = System.Drawing.Color.White;
            this.txt���2.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt���2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt���2.Location = new System.Drawing.Point(120, 427);
            this.txt���2.MaxLength = 100;
            this.txt���2.Name = "txt���2";
            this.txt���2.SelectedAllEnabled = false;
            this.txt���2.Size = new System.Drawing.Size(660, 21);
            this.txt���2.TabIndex = 27;
            this.txt���2.Tag = "REMARK2";
            this.txt���2.UseKeyEnter = true;
            this.txt���2.UseKeyF3 = true;
            this.txt���2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt������
            // 
            this.txt������.BackColor = System.Drawing.Color.White;
            this.txt������.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt������.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt������.Location = new System.Drawing.Point(120, 352);
            this.txt������.MaxLength = 50;
            this.txt������.Name = "txt������";
            this.txt������.SelectedAllEnabled = false;
            this.txt������.Size = new System.Drawing.Size(660, 21);
            this.txt������.TabIndex = 24;
            this.txt������.Tag = "PORT_LOADING";
            this.txt������.UseKeyEnter = true;
            this.txt������.UseKeyF3 = true;
            this.txt������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt������
            // 
            this.txt������.BackColor = System.Drawing.Color.White;
            this.txt������.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt������.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt������.Location = new System.Drawing.Point(120, 377);
            this.txt������.MaxLength = 50;
            this.txt������.Name = "txt������";
            this.txt������.SelectedAllEnabled = false;
            this.txt������.Size = new System.Drawing.Size(660, 21);
            this.txt������.TabIndex = 25;
            this.txt������.Tag = "PORT_ARRIVER";
            this.txt������.UseKeyEnter = true;
            this.txt������.UseKeyF3 = true;
            this.txt������.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel12.Location = new System.Drawing.Point(116, 448);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(664, 1);
            this.panel12.TabIndex = 140;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel7.Location = new System.Drawing.Point(116, 424);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(664, 1);
            this.panel7.TabIndex = 139;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel8.Location = new System.Drawing.Point(5, 399);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(775, 1);
            this.panel8.TabIndex = 138;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel9.Location = new System.Drawing.Point(5, 374);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(775, 1);
            this.panel9.TabIndex = 137;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.Location = new System.Drawing.Point(5, 349);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(775, 1);
            this.panel11.TabIndex = 135;
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
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lbl�ŷ�ó);
            this.panel6.Controls.Add(this.lbl��ȭ�ݾ�);
            this.panel6.Controls.Add(this.lbl��ǥ�ݾ�);
            this.panel6.Controls.Add(this.lbl��ǥȯ��);
            this.panel6.Controls.Add(this.lbl����);
            this.panel6.Controls.Add(this.lbl��������);
            this.panel6.Controls.Add(this.lbl�����׷�);
            this.panel6.Controls.Add(this.lbl�����Ű��ȣ);
            this.panel6.Controls.Add(this.lbl�����);
            this.panel6.Location = new System.Drawing.Point(391, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(115, 225);
            this.panel6.TabIndex = 4;
            // 
            // lbl�ŷ�ó
            // 
            this.lbl�ŷ�ó.Location = new System.Drawing.Point(3, 104);
            this.lbl�ŷ�ó.Name = "lbl�ŷ�ó";
            this.lbl�ŷ�ó.Resizeble = true;
            this.lbl�ŷ�ó.Size = new System.Drawing.Size(110, 18);
            this.lbl�ŷ�ó.TabIndex = 13;
            this.lbl�ŷ�ó.Tag = "CD_TRANS";
            this.lbl�ŷ�ó.Text = "�ŷ�ó";
            this.lbl�ŷ�ó.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��ȭ�ݾ�
            // 
            this.lbl��ȭ�ݾ�.Location = new System.Drawing.Point(3, 180);
            this.lbl��ȭ�ݾ�.Name = "lbl��ȭ�ݾ�";
            this.lbl��ȭ�ݾ�.Resizeble = true;
            this.lbl��ȭ�ݾ�.Size = new System.Drawing.Size(110, 18);
            this.lbl��ȭ�ݾ�.TabIndex = 12;
            this.lbl��ȭ�ݾ�.Tag = "AMT_EX";
            this.lbl��ȭ�ݾ�.Text = "��ȭ�ݾ�";
            this.lbl��ȭ�ݾ�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��ǥ�ݾ�
            // 
            this.lbl��ǥ�ݾ�.Location = new System.Drawing.Point(3, 205);
            this.lbl��ǥ�ݾ�.Name = "lbl��ǥ�ݾ�";
            this.lbl��ǥ�ݾ�.Resizeble = true;
            this.lbl��ǥ�ݾ�.Size = new System.Drawing.Size(110, 18);
            this.lbl��ǥ�ݾ�.TabIndex = 6;
            this.lbl��ǥ�ݾ�.Tag = "AM_ISSUE";
            this.lbl��ǥ�ݾ�.Text = "��ǥ�ݾ�";
            this.lbl��ǥ�ݾ�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��ǥȯ��
            // 
            this.lbl��ǥȯ��.Location = new System.Drawing.Point(3, 154);
            this.lbl��ǥȯ��.Name = "lbl��ǥȯ��";
            this.lbl��ǥȯ��.Resizeble = true;
            this.lbl��ǥȯ��.Size = new System.Drawing.Size(110, 18);
            this.lbl��ǥȯ��.TabIndex = 5;
            this.lbl��ǥȯ��.Tag = "RATE_ISSUE";
            this.lbl��ǥȯ��.Text = "��ǥȯ��";
            this.lbl��ǥȯ��.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����
            // 
            this.lbl����.Location = new System.Drawing.Point(3, 129);
            this.lbl����.Name = "lbl����";
            this.lbl����.Resizeble = true;
            this.lbl����.Size = new System.Drawing.Size(110, 18);
            this.lbl����.TabIndex = 4;
            this.lbl����.Tag = "SHIP_CORP";
            this.lbl����.Text = "����";
            this.lbl����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������
            // 
            this.lbl��������.Location = new System.Drawing.Point(3, 80);
            this.lbl��������.Name = "lbl��������";
            this.lbl��������.Resizeble = true;
            this.lbl��������.Size = new System.Drawing.Size(110, 18);
            this.lbl��������.TabIndex = 3;
            this.lbl��������.Tag = "DT_BL";
            this.lbl��������.Text = "��������";
            this.lbl��������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����׷�
            // 
            this.lbl�����׷�.Location = new System.Drawing.Point(3, 30);
            this.lbl�����׷�.Name = "lbl�����׷�";
            this.lbl�����׷�.Resizeble = true;
            this.lbl�����׷�.Size = new System.Drawing.Size(110, 18);
            this.lbl�����׷�.TabIndex = 2;
            this.lbl�����׷�.Tag = "GROUP_ISUL";
            this.lbl�����׷�.Text = "�����׷�";
            this.lbl�����׷�.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����Ű��ȣ
            // 
            this.lbl�����Ű��ȣ.Location = new System.Drawing.Point(3, 4);
            this.lbl�����Ű��ȣ.Name = "lbl�����Ű��ȣ";
            this.lbl�����Ű��ȣ.Resizeble = true;
            this.lbl�����Ű��ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl�����Ű��ȣ.TabIndex = 1;
            this.lbl�����Ű��ȣ.Tag = "TO_REFER";
            this.lbl�����Ű��ȣ.Text = "�����Ű��ȣ";
            this.lbl�����Ű��ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����
            // 
            this.lbl�����.Location = new System.Drawing.Point(3, 55);
            this.lbl�����.Name = "lbl�����";
            this.lbl�����.Resizeble = true;
            this.lbl�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�����.TabIndex = 1;
            this.lbl�����.Tag = "NM_EMP";
            this.lbl�����.Text = "�����";
            this.lbl�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lbl��������);
            this.panel5.Controls.Add(this.lbl���);
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lbl������);
            this.panel5.Controls.Add(this.lblVESSEL��);
            this.panel5.Controls.Add(this.lbl�����ȣ);
            this.panel5.Controls.Add(this.lbl��������);
            this.panel5.Controls.Add(this.lbl��ȭ);
            this.panel5.Controls.Add(this.lbl������������);
            this.panel5.Controls.Add(this.lbl��ǥ����);
            this.panel5.Controls.Add(this.lbl�����);
            this.panel5.Controls.Add(this.lbl������ȣ);
            this.panel5.Controls.Add(this.lbl��������);
            this.panel5.Controls.Add(this.lbl����������);
            this.panel5.Controls.Add(this.lblLC����);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(115, 472);
            this.panel5.TabIndex = 0;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 129);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 22;
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 180);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 21;
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������
            // 
            this.lbl��������.Location = new System.Drawing.Point(3, 229);
            this.lbl��������.Name = "lbl��������";
            this.lbl��������.Resizeble = true;
            this.lbl��������.Size = new System.Drawing.Size(110, 18);
            this.lbl��������.TabIndex = 17;
            this.lbl��������.Tag = "COND_PAY";
            this.lbl��������.Text = "��������";
            this.lbl��������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl���
            // 
            this.lbl���.Location = new System.Drawing.Point(3, 402);
            this.lbl���.Name = "lbl���";
            this.lbl���.Resizeble = true;
            this.lbl���.Size = new System.Drawing.Size(110, 18);
            this.lbl���.TabIndex = 13;
            this.lbl���.Tag = "REMARK";
            this.lbl���.Text = "���";
            this.lbl���.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 379);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 11;
            this.lbl������.Tag = "ARRIVAL_PORT";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������
            // 
            this.lbl������.Location = new System.Drawing.Point(3, 353);
            this.lbl������.Name = "lbl������";
            this.lbl������.Resizeble = true;
            this.lbl������.Size = new System.Drawing.Size(110, 18);
            this.lbl������.TabIndex = 10;
            this.lbl������.Tag = "SHIP_PORT";
            this.lbl������.Text = "������";
            this.lbl������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVESSEL��
            // 
            this.lblVESSEL��.Location = new System.Drawing.Point(3, 329);
            this.lblVESSEL��.Name = "lblVESSEL��";
            this.lblVESSEL��.Resizeble = true;
            this.lblVESSEL��.Size = new System.Drawing.Size(110, 18);
            this.lblVESSEL��.TabIndex = 9;
            this.lblVESSEL��.Tag = "VESSEL";
            this.lblVESSEL��.Text = "VESSEL��";
            this.lblVESSEL��.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����ȣ
            // 
            this.lbl�����ȣ.Location = new System.Drawing.Point(3, 279);
            this.lbl�����ȣ.Name = "lbl�����ȣ";
            this.lbl�����ȣ.Resizeble = true;
            this.lbl�����ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl�����ȣ.TabIndex = 8;
            this.lbl�����ȣ.Tag = "NO_INV";
            this.lbl�����ȣ.Text = "�����ȣ";
            this.lbl�����ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������
            // 
            this.lbl��������.Location = new System.Drawing.Point(3, 204);
            this.lbl��������.Name = "lbl��������";
            this.lbl��������.Resizeble = true;
            this.lbl��������.Size = new System.Drawing.Size(110, 18);
            this.lbl��������.TabIndex = 6;
            this.lbl��������.Tag = "COND_SHIP";
            this.lbl��������.Text = "��������";
            this.lbl��������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��ȭ
            // 
            this.lbl��ȭ.Location = new System.Drawing.Point(3, 155);
            this.lbl��ȭ.Name = "lbl��ȭ";
            this.lbl��ȭ.Resizeble = true;
            this.lbl��ȭ.Size = new System.Drawing.Size(110, 18);
            this.lbl��ȭ.TabIndex = 5;
            this.lbl��ȭ.Tag = "";
            this.lbl��ȭ.Text = "��ȭ";
            this.lbl��ȭ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������������
            // 
            this.lbl������������.Location = new System.Drawing.Point(3, 104);
            this.lbl������������.Name = "lbl������������";
            this.lbl������������.Resizeble = true;
            this.lbl������������.Size = new System.Drawing.Size(110, 18);
            this.lbl������������.TabIndex = 4;
            this.lbl������������.Text = "������������";
            this.lbl������������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��ǥ����
            // 
            this.lbl��ǥ����.Location = new System.Drawing.Point(3, 79);
            this.lbl��ǥ����.Name = "lbl��ǥ����";
            this.lbl��ǥ����.Resizeble = true;
            this.lbl��ǥ����.Size = new System.Drawing.Size(110, 18);
            this.lbl��ǥ����.TabIndex = 3;
            this.lbl��ǥ����.Text = "��ǥ����";
            this.lbl��ǥ����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl�����
            // 
            this.lbl�����.Location = new System.Drawing.Point(3, 55);
            this.lbl�����.Name = "lbl�����";
            this.lbl�����.Resizeble = true;
            this.lbl�����.Size = new System.Drawing.Size(110, 18);
            this.lbl�����.TabIndex = 2;
            this.lbl�����.Tag = "BALLOT";
            this.lbl�����.Text = "�����";
            this.lbl�����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl������ȣ
            // 
            this.lbl������ȣ.Location = new System.Drawing.Point(3, 4);
            this.lbl������ȣ.Name = "lbl������ȣ";
            this.lbl������ȣ.Resizeble = true;
            this.lbl������ȣ.Size = new System.Drawing.Size(110, 18);
            this.lbl������ȣ.TabIndex = 0;
            this.lbl������ȣ.Tag = "NO_BL";
            this.lbl������ȣ.Text = "������ȣ";
            this.lbl������ȣ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl��������
            // 
            this.lbl��������.Location = new System.Drawing.Point(3, 30);
            this.lbl��������.Name = "lbl��������";
            this.lbl��������.Resizeble = true;
            this.lbl��������.Size = new System.Drawing.Size(110, 18);
            this.lbl��������.TabIndex = 0;
            this.lbl��������.Tag = "FG_BL";
            this.lbl��������.Text = "��������";
            this.lbl��������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl����������
            // 
            this.lbl����������.Location = new System.Drawing.Point(3, 254);
            this.lbl����������.Name = "lbl����������";
            this.lbl����������.Resizeble = true;
            this.lbl����������.Size = new System.Drawing.Size(110, 18);
            this.lbl����������.TabIndex = 7;
            this.lbl����������.Tag = "PAY_LIMITDAY";
            this.lbl����������.Text = "����������";
            this.lbl����������.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLC����
            // 
            this.lblLC����.Location = new System.Drawing.Point(3, 304);
            this.lblLC����.Name = "lblLC����";
            this.lblLC����.Resizeble = true;
            this.lblLC����.Size = new System.Drawing.Size(110, 18);
            this.lblLC����.TabIndex = 8;
            this.lblLC����.Tag = "FG_LC";
            this.lblLC����.Text = "L/C����";
            this.lblLC����.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn������ǥó��
            // 
            this.btn������ǥó��.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn������ǥó��.BackColor = System.Drawing.Color.White;
            this.btn������ǥó��.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn������ǥó��.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn������ǥó��.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn������ǥó��.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn������ǥó��.Location = new System.Drawing.Point(441, 3);
            this.btn������ǥó��.Name = "btn������ǥó��";
            this.btn������ǥó��.Size = new System.Drawing.Size(120, 24);
            this.btn������ǥó��.TabIndex = 134;
            this.btn������ǥó��.TabStop = false;
            this.btn������ǥó��.Text = "������ǥó��";
            this.btn������ǥó��.UseVisualStyleBackColor = false;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn������ǥó��);
            this.panelExt1.Controls.Add(this.btn��������);
            this.panelExt1.Controls.Add(this.btn�ǸŰ����);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 3);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(787, 29);
            this.panelExt1.TabIndex = 135;
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
            this.tableLayoutPanel1.TabIndex = 136;
            // 
            // P_TR_EXBL_BAK
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_TR_EXBL_BAK";
            this.TitleText = "�������";
            this.Load += new System.EventHandler(this.P_TR_EXBL_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_TR_EXBL_Paint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp����������)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp������������)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp��������)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp��ǥ����)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDAYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ȭ�ݾ�)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ǥ�ݾ�)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur��ǥȯ��)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region �� �ʱ�ȭ �κ� - ����� ����

        private System.Windows.Forms.BindingManagerBase m_Manager;

        private string m_Today;
        private DataTable m_HeadTable;
        private DataTable m_CopyTable;

        private bool m_IsPageActivated = false;

    
    #endregion
    
        #region �� �ʱ�ȭ �κ�


        /// <summary>
        /// ������
        /// </summary>
        public P_TR_EXBL_BAK()
        {
            try
            {
                InitializeComponent();

                this.txtDAYS.NumberFormatInfoObject.NumberDecimalDigits = 0;
                this.txtDAYS.DecimalValue = 0;

                this.cur��ǥȯ��.NumberFormatInfoObject.NumberDecimalDigits = 4;
                this.cur��ȭ�ݾ�.NumberFormatInfoObject.NumberDecimalDigits = 4;
                this.cur��ǥ�ݾ�.NumberFormatInfoObject.NumberDecimalDigits = 4;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// �� �ε��� DD������ �Ѵ�.
        /// �˻���� �˻��� �Ѵ�.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void P_TR_EXBL_Load(object sender, EventArgs e)
        {
            Show();

            try
            {
                // DD�� �ʱ�ȭ
                this.InitDD();


                // ��¥ �ʱ�ȭ
                this.InitDate();


                // �ʱ� ���̺� ����
                SelectInit();

                // Control �� Ȱ��ȭ
                SetControlDisable();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// Paint Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P_TR_EXBL_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                this.Paint -= new System.Windows.Forms.PaintEventHandler(this.P_TR_EXBL_Paint);
                Application.DoEvents();


                // �������� �޺� �ڽ� �ʱ�ȭ
                this.InitComboBox();

                // �ʱ� ���̺� �߰� �� ���ε�
                this.Append();

                // ��ư ����
                this.SetButton();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        /// <summary>
        /// ��ư �ʱ�ȭ
        /// </summary>
        private void SetButton()
        {
            if (this.CanSearch)
                this.ToolBarSearchButtonEnabled = true;
            else
                this.ToolBarSearchButtonEnabled = false;

            if (this.CanAdd)
                this.ToolBarAddButtonEnabled = true;
            else
                this.ToolBarAddButtonEnabled = false;

            if (this.CanDelete)
                this.ToolBarDeleteButtonEnabled = true;
            else
                this.ToolBarDeleteButtonEnabled = false;

            if (this.CanSave)
                this.ToolBarSaveButtonEnabled = true;
            else
                this.ToolBarSaveButtonEnabled = false;

            if (this.CanPrint)
                this.ToolBarPrintButtonEnabled = true;
            else
                this.ToolBarPrintButtonEnabled = false;
        }

        /// <summary>
        /// DD ����
        /// </summary>
        private void InitDD()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is PanelExt)
                {
                    foreach (Control ctrl in ((PanelExt)ctr).Controls)
                    {
                        if (ctrl is PanelExt)
                        {
                            foreach (Control ctrls in ((PanelExt)ctrl).Controls)
                            {
                                if (ctrls is LabelExt)
                                    ((LabelExt)ctrls).Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)((LabelExt)ctrls).Tag);
                            }
                        }
                    }
                }
            }

            btn������ǥó��.Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)btn������ǥó��.Tag);
            btn�ǸŰ����.Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)btn�ǸŰ����.Tag);
            btn��������.Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)btn��������.Tag);
        }

        /// <summary>
        /// �˻� ��¥ �ؽ�Ʈ �ڽ� �ʱ�ȭ
        /// </summary>
        private void InitDate()
        {
            string ls_day = this.MainFrameInterface.GetStringToday;

            this.dtp��ǥ����.Text = ls_day;
            this.dtp��������.Text = ls_day;
            this.dtp������������.Text = ls_day;
            this.dtp����������.Text = ls_day;

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

            string[] ls_args = new string[6];
            ls_args[0] = "N;TR_IM00016";// ��������
            ls_args[1] = "N;MA_B000005";// ��ȭ
            ls_args[2] = "N;MA_B000020";// ������
            ls_args[3] = "N;TR_IM00004";// ��������
            ls_args[4] = "N;TR_IM00005";// L/C ����
            ls_args[5] = "S;TR_IM00003";// ��������

            DataSet lds_Combo = (DataSet)GetComboData(ls_args);

            // ��������
            this.cbo��������.DataSource = lds_Combo.Tables[0];
            this.cbo��������.DisplayMember = "NAME";
            this.cbo��������.ValueMember = "CODE";

            // ��ȭ
            this.cbo��ȭ.DataSource = lds_Combo.Tables[1];
            this.cbo��ȭ.DisplayMember = "NAME";
            this.cbo��ȭ.ValueMember = "CODE";

            // ������
            this.cbo������.DataSource = lds_Combo.Tables[2];
            this.cbo������.DisplayMember = "NAME";
            this.cbo������.ValueMember = "CODE";

            // ��������
            this.cbo��������.DataSource = lds_Combo.Tables[3];
            this.cbo��������.DisplayMember = "NAME";
            this.cbo��������.ValueMember = "CODE";

            // L/C ����
            this.cboLC����.DataSource = lds_Combo.Tables[4];
            this.cboLC����.DisplayMember = "NAME";
            this.cboLC����.ValueMember = "CODE";

            // ���� ����
            this.cbo��������.DataSource = lds_Combo.Tables[5];
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
            foreach (Control ctr in this.Controls)
            {
                if (ctr is PanelExt)
                {
                    foreach (Control ctrl in ((PanelExt)ctr).Controls)
                    {
                        if (ctrl is TextBoxExt)
                        {
                            ((TextBoxExt)ctrl).ReadOnly = true;
                        }
                        if (ctrl is ButtonExt)
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

            //this.btn�����Ű��ȣ.Enabled = true;

        }

        /// <summary>
        /// Panel ���� ��Ʈ���� Ȱ������ �Ѵ�.
        /// </summary>
        /// <param name="ps_panel"></param>
        private void SetControlEnable()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is PanelExt)
                {
                    foreach (Control ctrl in ((PanelExt)ctr).Controls)
                    {
                        if (ctrl is TextBoxExt)
                        {
                            ((TextBoxExt)ctrl).ReadOnly = false;
                        }
                        if (ctrl is ButtonExt)
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
        }


        /// <summary>
        /// �޺��ڽ��� ó�� ���·� ������.
        /// </summary>
        private void ComboResetting()
        {
            this.cbo��������.SelectedIndex = 0;
            this.cbo��ȭ.SelectedIndex = 0;
            this.cbo������.SelectedIndex = 0;
            this.cbo��������.SelectedIndex = 0;
            this.cboLC����.SelectedIndex = 0;
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
                this.m_Manager.Position = 0;

                DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);

                if (ldt_temp != null)
                {
                    // ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ�?
                    // MA_M000073
                    string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
                    DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        // ���� �����͸� ������ �� ���ο� �����͸� �߰��� �� �ְ� �Ѵ�.
                        if (this.Save())
                        {
                            // ���� ����
                        }
                        else
                        {
                            // ����� ���� �߻�
                            return;
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                trade.P_TR_EXBLNO_SUB obj = new trade.P_TR_EXBLNO_SUB(this.MainFrameInterface);

                if (obj.ShowDialog() == DialogResult.OK)
                {
                    this.m_HeadTable = obj.GetResultTable;

                    this.SetBindingManager();

                    this.m_Manager.Position = 0;

                    // �ڵ�, �� ������ ���� ������ ����

                    // �ŷ�ó �ڵ� & ��Ī ����
                    this.bpc�ŷ�ó.SetCodeValue(this.m_HeadTable.Rows[0]["CD_PARTNER"].ToString());
                    this.bpc�ŷ�ó.SetCodeName(this.m_HeadTable.Rows[0]["NM_PARTNER"].ToString());

                    // �����׷� �ڵ� & ��Ī ����
                    this.bpc�����׷�.SetCodeValue(this.m_HeadTable.Rows[0]["CD_SALEGRP"].ToString());
                    this.bpc�����׷�.SetCodeName(this.m_HeadTable.Rows[0]["NM_SALEGRP"].ToString());

                    // ����� �ڵ� & ��Ī ����
                    this.bpc�����.SetCodeValue(this.m_HeadTable.Rows[0]["NO_EMP"].ToString());
                    this.bpc�����.SetCodeName(this.m_HeadTable.Rows[0]["NM_KOR"].ToString());

                    // ����� �ڵ� & ��Ī ����
                    this.bpc�����.SetCodeValue(this.m_HeadTable.Rows[0]["CD_BIZAREA"].ToString());
                    this.bpc�����.SetCodeName(m_HeadTable.Rows[0]["NM_BIZAREA"].ToString());

                    // ������ �ڵ� & ��Ī ����
                    this.bpc������.SetCodeValue(this.m_HeadTable.Rows[0]["CD_EXPORT"].ToString());
                    this.bpc������.SetCodeName(this.m_HeadTable.Rows[0]["NM_EXPORT"].ToString());

                    // ���� �ڵ� & ��Ī ����
                    this.bpc����.SetCodeValue(this.m_HeadTable.Rows[0]["SHIP_CORP"].ToString());
                    this.bpc����.SetCodeName(this.m_HeadTable.Rows[0]["NM_SHIP_CORP"].ToString());

                    this.m_IsPageActivated = true;
                    this.SetControlEnable();

                    this.m_HeadTable.AcceptChanges();

                    this.txt������ȣ.Enabled = false;


                }
                obj.Dispose();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
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
                if (!this.m_IsPageActivated)
                    return;

                this.m_Manager.Position = 0;

                DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);

                if (ldt_temp != null)
                {
                    // ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ�?
                    // MA_M000073
                    //string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
                    DialogResult result = ShowMessage("QY2_001");

                    if (result == DialogResult.Yes)
                    {
                        // ���� �����͸� ������ �� ���ο� �����͸� �߰��� �� �ְ� �Ѵ�.
                        if (this.Save())
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

                // �߰��Ҽ� �ֵ��� ���ο� Row�߰�
                this.Append();

                // Control �� Ȱ��ȭ
                this.SetControlDisable();

                this.txt������ȣ.Enabled = true;
                this.txt������ȣ.ReadOnly = false;
                this.txt������ȣ.Focus();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
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
                if (!this.m_IsPageActivated)
                    return;

                this.txt������ȣ.Focus();

                this.m_Manager.Position = 0;

                string msg = null;

                DataTable ldt_temp = this.m_HeadTable.GetChanges();

                if (ldt_temp == null)
                {
                    // MA_M000017 ("����� �����Ͱ� �����ϴ�)
                    ShowMessage("IK1_013");
                    //msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000017");
                    //Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                this.Save();

                this.txt������ȣ.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// ���� ��ư
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            string msg = null;

            if (!this.m_IsPageActivated)
                return;

            DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Added);

            // ���� �� �� ���� �κ�(�߰��� �����Ͱ� �ִٴ� ���� ���ο� �������̹Ƿ� DB���� ���ٴ� ��
            if (ldt_temp != null)
                return;

            try
            {

                // MA_M000016
                // ������ ���� �ϰڽ��ϱ�?
                msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000016");
                if (DialogResult.Yes == MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                 
                        m_HeadTable.Rows[0].Delete();
                        SpInfo si = new SpInfo();
                        si.DataValue = m_HeadTable;
                        si.CompanyID = LoginInfo.CompanyCode;
                        si.UserID = LoginInfo.EmployeeNo;
                        si.SpNameDelete = "UP_TR_EXBL_DELETE";
                        si.SpParamsDelete = new string[] { "NO_BL", "CD_COMPANY" };

                        ResultData result = (ResultData)this.Save(si);

                        if (result.Result)
                        {
                            // TR_M000033
                            // ���������� ���� �Ǿ����ϴ�.
                            ShowMessage("TR_M000033");
                            //msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000033");
                            //MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.m_HeadTable.AcceptChanges();
                            this.Append();

                            // Control �� Ȱ��ȭ
                            this.SetControlDisable();

                            this.txt������ȣ.Enabled = true;
                            this.txt������ȣ.ReadOnly = false;
                            this.txt������ȣ.Focus();
                        }
                
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// ��� ��ư
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {

        }

        ///// <summary>
        ///// ���� ��ư
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        //{
        //    if (this.m_HeadTable == null || this.m_HeadTable.Rows.Count < 1)
        //        return true;

        //    DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
        //    if (ldt_temp == null) return true;

        //    // �����Ͱ� ó�� �ε��� ������� ���� �߰��� ��� LC��ȣ�� ������ �Ǵ��Ͽ� ������ ������ �����.
        //    DataTable ldt_table = this.m_HeadTable.GetChanges(DataRowState.Added);

        //    if (this.m_txtNoBL.Text == "" || ldt_table == null) return true;

        //    try
        //    {

        //        DialogResult result = ShowMessage("MA_M000073","IY3");

        //        switch(result)
        //        {
        //            case DialogResult.Yes:

        //                if(this.Save())
        //                {
        //                    // �ڷḦ �����Ͽ����ϴ�.
        //                    this.ShowMessage("IK1_001");
        //                    return true;
        //                }
        //                else
        //                {
        //                    //�������
        //                    this.ShowMessage("EK1_002");
        //                    return false;
        //                }
					
        //            case DialogResult.Cancel:

        //                return false;
							

        //            case DialogResult.No:

        //                return true;
        //        }
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}

        #region -> �����ưŬ��

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {

            try
            {
                //������� ������� ����
                if (this.m_HeadTable == null || this.m_HeadTable.Rows.Count < 1)
                    return true;

                DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
                if (ldt_temp == null) return true;


                // ����� ������ �ֽ��ϴ�. �����Ͻðڽ��ϱ�?
                DialogResult result = ShowMessage("MA_M000073", "IY3");

                switch (result)
                {
                    case DialogResult.Yes:

                        if (this.Save())
                        {
                            // �ڷḦ �����Ͽ����ϴ�.
                            this.ShowMessage("IK1_001");
                            return true;
                        }
                        else
                        {
                            //�������
                            this.ShowMessage("EK1_002");
                            return false;
                        }

                    case DialogResult.Cancel:

                        return false;


                    case DialogResult.No:

                        return true;
                }

            }
            catch (System.Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
                return false;
            }

            return true;

        }


        #endregion

        #endregion

        #region �� �ʱ� ���̺� �ʱ�ȭ �κ�

        /// <summary>
        /// DataTable ����
        /// </summary>
        private void SelectInit()
        {
            //DataSet lds_Result = (DataSet)this.MainFrameInterface.InvokeRemoteMethod("TradeExport_NTX", "trade.CC_TR_EXBLIN_NTX", "CC_TR_EXBLIN_NTX.rem", "SelectBLText", null);
            //this.m_HeadTable = lds_Result.Tables[0];

            if (m_HeadTable == null)
                m_HeadTable = new DataTable();

            m_HeadTable.Columns.Add("NO_BL", typeof(string));
            m_HeadTable.Columns.Add("CD_COMPANY", typeof(string));
            m_HeadTable.Columns.Add("NO_TO", typeof(string));
            m_HeadTable.Columns.Add("NO_INV", typeof(string));
            m_HeadTable.Columns.Add("CD_BIZAREA", typeof(string));
            m_HeadTable.Columns.Add("CD_SALEGRP", typeof(string));
            m_HeadTable.Columns.Add("NO_EMP", typeof(string));
            m_HeadTable.Columns.Add("CD_PARTNER", typeof(string));
            m_HeadTable.Columns.Add("DT_BALLOT", typeof(string));
            m_HeadTable.Columns.Add("CD_EXCH", typeof(string));
            m_HeadTable.Columns.Add("RT_EXCH", typeof(double));
            m_HeadTable.Columns.Add("AM_EX", typeof(string));
            m_HeadTable.Columns.Add("AM", typeof(string));
            m_HeadTable.Columns.Add("AM_EXNEGO", typeof(string));
            m_HeadTable.Columns.Add("AM_NEGO", typeof(string));
            m_HeadTable.Columns.Add("YN_SLIP", typeof(string));
            m_HeadTable.Columns.Add("NO_SLIP", typeof(string));
            m_HeadTable.Columns.Add("CD_EXPORT", typeof(string));
            m_HeadTable.Columns.Add("DT_LOADING", typeof(string));
            m_HeadTable.Columns.Add("DT_ARRIVAL", typeof(string));
            m_HeadTable.Columns.Add("SHIP_CORP", typeof(string));
            m_HeadTable.Columns.Add("NM_VESSEL", typeof(string));
            m_HeadTable.Columns.Add("PORT_LOADING", typeof(string));
            m_HeadTable.Columns.Add("PORT_NATION", typeof(string));
            m_HeadTable.Columns.Add("PORT_ARRIVER", typeof(string));
            m_HeadTable.Columns.Add("COND_SHIPMENT", typeof(string));
            m_HeadTable.Columns.Add("FG_BL", typeof(string));
            m_HeadTable.Columns.Add("FG_LC", typeof(string));
            m_HeadTable.Columns.Add("COND_PAY", typeof(string));
            m_HeadTable.Columns.Add("COND_DAYS", typeof(string));
            m_HeadTable.Columns.Add("DT_PAYABLE", typeof(string));
            m_HeadTable.Columns.Add("REMARK1", typeof(string));
            m_HeadTable.Columns.Add("REMARK2", typeof(string));
            m_HeadTable.Columns.Add("REMARK3", typeof(string));
            m_HeadTable.Columns.Add("ID_INSERT", typeof(string));
            m_HeadTable.Columns.Add("DTS_INSERT", typeof(string));
            m_HeadTable.Columns.Add("ID_UPDATE", typeof(string));
            m_HeadTable.Columns.Add("DTS_UPDATE", typeof(string));
            m_HeadTable.Columns.Add("NM_BIZAREA", typeof(string));
            m_HeadTable.Columns.Add("NM_SALEGRP", typeof(string));
            m_HeadTable.Columns.Add("NM_KOR", typeof(string));
            m_HeadTable.Columns.Add("NM_PARTNER", typeof(string));
            m_HeadTable.Columns.Add("NM_EXPORT", typeof(string));
            m_HeadTable.Columns.Add("NM_SHIP_CORP", typeof(string));

            this.m_HeadTable.Columns["NO_BL"].DefaultValue = "";
            this.m_HeadTable.Columns["CD_COMPANY"].DefaultValue = LoginInfo.CompanyCode;
            this.m_HeadTable.Columns["NO_TO"].DefaultValue = "";
            this.m_HeadTable.Columns["NO_INV"].DefaultValue = "";
            this.m_HeadTable.Columns["CD_BIZAREA"].DefaultValue = "";
            this.m_HeadTable.Columns["CD_SALEGRP"].DefaultValue = "";
            this.m_HeadTable.Columns["NO_EMP"].DefaultValue = "";
            this.m_HeadTable.Columns["CD_PARTNER"].DefaultValue = "";
            this.m_HeadTable.Columns["DT_BALLOT"].DefaultValue = m_Today;
            this.m_HeadTable.Columns["CD_EXCH"].DefaultValue = "";

            this.m_HeadTable.Columns["RT_EXCH"].DefaultValue = 0;
            this.m_HeadTable.Columns["AM_EX"].DefaultValue = 0;
            this.m_HeadTable.Columns["AM"].DefaultValue = 0;
            this.m_HeadTable.Columns["AM_EXNEGO"].DefaultValue = 0;
            this.m_HeadTable.Columns["AM_NEGO"].DefaultValue = 0;
            this.m_HeadTable.Columns["YN_SLIP"].DefaultValue = "";
            this.m_HeadTable.Columns["NO_SLIP"].DefaultValue = "";
            this.m_HeadTable.Columns["CD_EXPORT"].DefaultValue = "";
            this.m_HeadTable.Columns["DT_LOADING"].DefaultValue = m_Today;
            this.m_HeadTable.Columns["DT_ARRIVAL"].DefaultValue = "";

            this.m_HeadTable.Columns["SHIP_CORP"].DefaultValue = "";
            this.m_HeadTable.Columns["NM_VESSEL"].DefaultValue = "";
            this.m_HeadTable.Columns["PORT_LOADING"].DefaultValue = "";
            this.m_HeadTable.Columns["PORT_NATION"].DefaultValue = "";
            this.m_HeadTable.Columns["PORT_ARRIVER"].DefaultValue = "";
            this.m_HeadTable.Columns["COND_SHIPMENT"].DefaultValue = "";
            this.m_HeadTable.Columns["FG_BL"].DefaultValue = "";
            this.m_HeadTable.Columns["FG_LC"].DefaultValue = "";
            this.m_HeadTable.Columns["COND_PAY"].DefaultValue = "";
            this.m_HeadTable.Columns["COND_DAYS"].DefaultValue = 0;
            this.m_HeadTable.Columns["DT_PAYABLE"].DefaultValue = "";

            this.m_HeadTable.Columns["REMARK1"].DefaultValue = "";
            this.m_HeadTable.Columns["REMARK2"].DefaultValue = "";
            this.m_HeadTable.Columns["REMARK3"].DefaultValue = "";
            this.m_HeadTable.Columns["ID_INSERT"].DefaultValue = LoginInfo.EmployeeNo;
            this.m_HeadTable.Columns["DTS_INSERT"].DefaultValue = "";
            this.m_HeadTable.Columns["ID_UPDATE"].DefaultValue = LoginInfo.EmployeeNo;
            this.m_HeadTable.Columns["DTS_UPDATE"].DefaultValue = "";
            this.m_HeadTable.Columns["NM_BIZAREA"].DefaultValue = "";
            this.m_HeadTable.Columns["NM_SALEGRP"].DefaultValue = "";
            this.m_HeadTable.Columns["NM_KOR"].DefaultValue = "";

            this.m_HeadTable.Columns["NM_PARTNER"].DefaultValue = "";
            this.m_HeadTable.Columns["NM_EXPORT"].DefaultValue = "";
            this.m_HeadTable.Columns["NM_SHIP_CORP"].DefaultValue = "";

            this.m_CopyTable = m_HeadTable.Copy();
        }


        /// <summary>
        /// �ʱ� ���ε� �޴��� ����
        /// </summary>
        private void SetBindingManager()
        {
            this.m_Manager = this.BindingContext[this.m_HeadTable];

            foreach (Control ctr in this.Controls)
            {
                if (ctr is PanelExt)
                {
                    foreach (Control ctrl in ((PanelExt)ctr).Controls)
                    {
                        ctrl.DataBindings.Clear();
                    }
                }
            }

            this.txt������ȣ.DataBindings.Add("Text", this.m_HeadTable, "NO_BL");
            this.cbo��������.DataBindings.Add("SelectedValue", this.m_HeadTable, "FG_BL");
            //this.txt�����Ű��ȣ.DataBindings.Add("Text", this.m_HeadTable, "NO_TO");

            this.dtp��ǥ����.DataBindings.Add("Text", this.m_HeadTable, "DT_BALLOT");
            this.dtp��������.DataBindings.Add("Text", this.m_HeadTable, "DT_LOADING");
            this.dtp������������.DataBindings.Add("Text", this.m_HeadTable, "DT_ARRIVAL");

            this.cbo��ȭ.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_EXCH");
            this.cur��ǥȯ��.DataBindings.Add("Text", this.m_HeadTable, "RT_EXCH");
            this.cur��ȭ�ݾ�.DataBindings.Add("Text", this.m_HeadTable, "AM_EX");

            this.cbo������.DataBindings.Add("SelectedValue", this.m_HeadTable, "PORT_NATION");
            this.cbo��������.DataBindings.Add("SelectedValue", this.m_HeadTable, "COND_SHIPMENT");
            this.cur��ǥ�ݾ�.DataBindings.Add("Text", this.m_HeadTable, "AM");
            this.cbo��������.DataBindings.Add("SelectedValue", this.m_HeadTable, "COND_PAY");
            this.txtDAYS.DataBindings.Add("Text", this.m_HeadTable, "COND_DAYS");
            this.dtp����������.DataBindings.Add("Text", this.m_HeadTable, "DT_PAYABLE");

            this.txt�����ȣ.DataBindings.Add("Text", this.m_HeadTable, "NO_INV");
            this.cboLC����.DataBindings.Add("SelectedValue", this.m_HeadTable, "FG_LC");
            this.txtVESSEL��.DataBindings.Add("Text", this.m_HeadTable, "NM_VESSEL");
            this.txt������.DataBindings.Add("Text", this.m_HeadTable, "PORT_LOADING");
            this.txt������.DataBindings.Add("Text", this.m_HeadTable, "PORT_ARRIVER");

            this.txt���1.DataBindings.Add("Text", this.m_HeadTable, "REMARK1");
            this.txt���2.DataBindings.Add("Text", this.m_HeadTable, "REMARK2");
            this.txt���3.DataBindings.Add("Text", this.m_HeadTable, "REMARK3");

            this.m_Manager.Position = 0;
        }

        #endregion

        #region ���߰� �κ�

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

            this.ComboResetting();

            // B/L ��ȣ ��Ŀ�� �̵� �� �Է��� �� �ְ� �Ѵ�.
            this.txt������ȣ.ReadOnly = false;
            this.txt������ȣ.Focus();


            // �ڵ�, �� ������ ���� ������ ����
            // �ŷ�ó �ڵ� & ��Ī ����
            this.bpc�ŷ�ó.SetCodeValue("");
            this.bpc�ŷ�ó.SetCodeName("");

            // �����׷� �ڵ� & ��Ī ����
            this.bpc�����׷�.SetCodeValue("");
            this.bpc�����׷�.SetCodeName("");

            // ������ �ڵ� & ��Ī ����
            this.bpc������.SetCodeValue("");
            this.bpc������.SetCodeName("");

            // ���� �ڵ� & ��Ī ����
            this.bpc����.SetCodeValue("");
            this.bpc����.SetCodeName("");

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

                 this.txt�����ȣ.Focus();

                 if (m_HeadTable == null) return true;
                 if (m_HeadTable.Rows.Count == 0) return true;

                 // �ʼ� �Է� �� üũ
                 if (!this.CheckRequiredValue())
                     return false;

                 //object[] args = new object[1] { this.m_HeadTable };

                 //DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Added);
                 //if (ldt_temp != null)
                 //{

                 //    this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_EXBLIN", "CC_TR_EXBLIN.rem", "SaveAppendEXH", args);
                 //}

                 //ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
                 //if (ldt_temp != null)
                 //{
                 //    this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_EXBLIN", "CC_TR_EXBLIN.rem", "SaveUpdateBL", args);
                 //}

                 DataTable dt = m_HeadTable.GetChanges();

                 SpInfo si = new SpInfo();
                 si.DataValue = dt;
                 si.CompanyID = LoginInfo.CompanyCode;
                 si.UserID = LoginInfo.EmployeeNo;
                 si.SpNameInsert = "UP_TR_EXBL_INSERT";
                 si.SpNameUpdate = "UP_TR_EXBL_UPDATE";
                 si.SpParamsInsert = new string[] { "NO_BL", "CD_COMPANY", "NO_TO", "NO_INV", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "CD_PARTNER", "DT_BALLOT", "CD_EXCH", "RT_EXCH", "AM_EX", "AM", "AM_EXNEGO", "AM_NEGO", "YN_SLIP", "NO_SLIP", "CD_EXPORT", "DT_LOADING", "DT_ARRIVAL", "SHIP_CORP", "NM_VESSEL", "PORT_LOADING", "PORT_NATION", "PORT_ARRIVER", "COND_SHIPMENT", "FG_BL", "FG_LC", "COND_PAY", "COND_DAYS", "DT_PAYABLE", "REMARK1", "REMARK2", "REMARK3", "ID_INSERT" };
                 si.SpParamsUpdate = new string[] { "NO_BL", "CD_COMPANY", "NO_TO", "NO_INV", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "CD_PARTNER", "DT_BALLOT", "CD_EXCH", "RT_EXCH", "AM_EX", "AM", "AM_EXNEGO", "AM_NEGO", "YN_SLIP", "NO_SLIP", "CD_EXPORT", "DT_LOADING", "DT_ARRIVAL", "SHIP_CORP", "NM_VESSEL", "PORT_LOADING", "PORT_NATION", "PORT_ARRIVER", "COND_SHIPMENT", "FG_BL", "FG_LC", "COND_PAY", "COND_DAYS", "DT_PAYABLE", "REMARK1", "REMARK2", "REMARK3", "ID_UPDATE" };

                 ResultData result = (ResultData)this.Save(si);

                 if (result.Result)
                 {
                     // CM_M000001
                     // �ڷḦ �����Ͽ����ϴ�.
                     this.ShowMessage("CM_M000001");
                     m_HeadTable.AcceptChanges();
                     // �ѹ� �����ϸ� BL��ȣ�� ������ �� ���� �Ѵ�.
                     this.txt������ȣ.ReadOnly = true;
                     return true;
                 }

                 return false;

             }
             catch (Exception ex)
             {
                 MsgEnd(ex);
             }
             return true;
         }

        #endregion

        #region �������� �۾�

        /// <summary>
        /// �ʼ� �Է»��� üũ
        /// </summary>
        /// <returns></returns>
        private bool CheckRequiredValue()
        {
            //������ȣ
            if (txt������ȣ.Text.Trim() == "")
            {
                //������ȣ + ��(��) �ʼ� �Է� �׸��Դϴ�.
                ShowMessage("WK1_004", this.lbl������ȣ.Text);
                txt������ȣ.Focus();
                return false;
            }
            ////�Ű��ȣ
            //if (txt�����Ű��ȣ.Text.Trim() == "")
            //{
            //    //�Ű��ȣ + ��(��) �ʼ� �Է� �׸��Դϴ�.
            //    ShowMessage("WK1_004", this.lbl�����Ű��ȣ.Text);
            //    txt�����Ű��ȣ.Focus();
            //    return false;
            //}
            //�����׷�
            if (bpc�����׷�.IsEmpty())
            {
                //�����׷� + ��(��) �ʼ� �Է� �׸��Դϴ�.
                ShowMessage("WK1_004", this.lbl�����׷�.Text);
                bpc�����׷�.Focus();
                return false;
            }
            //�����
            if (bpc�����.IsEmpty())
            {
                //����� + ��(��) �ʼ� �Է� �׸��Դϴ�.
                ShowMessage("WK1_004", this.lbl�����.Text);
                bpc�����.Focus();
                return false;
            }
            //�����
            if (bpc�����.IsEmpty())
            {
                //����� + ��(��) �ʼ� �Է� �׸��Դϴ�.
                ShowMessage("WK1_004", this.lbl�����.Text);
                bpc�����.Focus();
                return false;
            }
            //��ǥ����
            if (dtp��ǥ����.Text.Trim() == "")
            {
                //��ǥ���� + ��(��) �ʼ� �Է� �׸��Դϴ�.
                ShowMessage("WK1_004", this.lbl��ǥ����.Text);
                dtp��ǥ����.Focus();
                return false;
            }
            //��������
            if (dtp��������.Text.Trim() == "")
            {
                //�������� + ��(��) �ʼ� �Է� �׸��Դϴ�.
                ShowMessage("WK1_004", this.lbl��������.Text);
                dtp��������.Focus();
                return false;
            }
            //��ǥȯ��
            if (cur��ǥȯ��.DecimalValue == 0)
            {
                ShowMessage("WK1_004",lbl��ǥȯ��.Text);
                cur��ǥȯ��.Focus();
                return false;
            }
            //��ȭ�ݾ�
            if (cur��ȭ�ݾ�.DecimalValue == 0)
            {
                ShowMessage("WK1_004",lbl��ȭ�ݾ�.Text);
                cur��ȭ�ݾ�.Focus();
                return false;
            }
            //��ǥ�ݾ�
            if (cur��ǥ�ݾ�.DecimalValue == 0)
            {
                ShowMessage("WK1_004",lbl��ǥ�ݾ�.Text);
                cur��ǥ�ݾ�.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region �� ���� �Ű� ��ȣ �κ�

        /// <summary>
        /// ��� ����â���� ������ �����͸� HeadTable�� �־� �ش�.
        /// </summary>
        /// <param name="pdr_row"></param>
        private void ApplyToRefer(DataRow pdr_row)
        {
            this.m_HeadTable.Rows[0]["NO_TO"] = pdr_row["NO_TO"];
            this.m_HeadTable.Rows[0]["NO_INV"] = pdr_row["NO_INV"];
            this.m_HeadTable.Rows[0]["CD_BIZAREA"] = pdr_row["CD_BIZAREA"];

            this.m_HeadTable.Rows[0]["CD_SALEGRP"] = pdr_row["CD_SALEGRP"];
            this.m_HeadTable.Rows[0]["NO_EMP"] = pdr_row["NO_EMP"];
            this.m_HeadTable.Rows[0]["CD_PARTNER"] = pdr_row["CD_PARTNER"];
            //this.m_HeadTable.Rows[0]["DT_BALLOT"] = pdr_row["__________"];
            this.m_HeadTable.Rows[0]["CD_EXCH"] = pdr_row["CD_EXCH"];

            this.m_HeadTable.Rows[0]["RT_EXCH"] = pdr_row["RT_EXCH"];
            this.m_HeadTable.Rows[0]["AM_EX"] = pdr_row["AM_EX"];
            this.m_HeadTable.Rows[0]["AM"] = pdr_row["AM"];
            //this.m_HeadTable.Rows[0]["AM_EXNEGO"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["AM_NEGO"] = pdr_row["__________"];

            //this.m_HeadTable.Rows[0]["YN_SLIP"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["NO_SLIP"] = pdr_row["__________"];
            this.m_HeadTable.Rows[0]["CD_EXPORT"] = pdr_row["CD_EXPORT"];
            //this.m_HeadTable.Rows[0]["DT_LOADING"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["DT_ARRIVAL"] = pdr_row["__________"];

            //this.m_HeadTable.Rows[0]["SHIP_CORP"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["NM_VESSEL"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["PORT_LOADING"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["PORT_NATION"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["PORT_ARRIVER"] = pdr_row["__________"];

            //this.m_HeadTable.Rows[0]["COND_SHIPMENT"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["FG_BL"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["COND_PAY"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["COND_DAYS"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["DT_PAYABLE"] = pdr_row["__________"];

            //this.m_HeadTable.Rows[0]["REMARK1"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["REMARK2"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["REMARK3"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["ID_INSERT"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["DTS_INSERT"] = pdr_row["__________"];

            //this.m_HeadTable.Rows[0]["ID_UPDATE"] = pdr_row["__________"];
            //this.m_HeadTable.Rows[0]["DTS_UPDATE"] = pdr_row["__________"];
            this.m_HeadTable.Rows[0]["FG_LC"] = pdr_row["FG_LC"];
            this.m_HeadTable.Rows[0]["NM_BIZAREA"] = pdr_row["NM_BIZAREA"];
            this.m_HeadTable.Rows[0]["NM_SALEGRP"] = pdr_row["NM_SALEGRP"];

            this.m_HeadTable.Rows[0]["NM_KOR"] = pdr_row["NM_KOR"];
            this.m_HeadTable.Rows[0]["NM_PARTNER"] = pdr_row["LN_PARTNER"];
            this.m_HeadTable.Rows[0]["NM_EXPORT"] = pdr_row["NM_EXPORT"];
            //this.m_HeadTable.Rows[0]["NM_SHIP_CORP"] = pdr_row["__________"];

            // �ڵ�, �� ������ ���� ������ ����

            // �ŷ�ó �ڵ� & ��Ī ����
            this.bpc�ŷ�ó.SetCodeValue(pdr_row["CD_PARTNER"].ToString());
            this.bpc�ŷ�ó.SetCodeName(pdr_row["LN_PARTNER"].ToString());

            // �����׷� �ڵ� & ��Ī ����
            this.bpc�����׷�.SetCodeValue(pdr_row["CD_SALEGRP"].ToString());
            this.bpc�����׷�.SetCodeName(pdr_row["NM_SALEGRP"].ToString());

            // ����� �ڵ� & ��Ī ����
            this.bpc�����.SetCodeValue(pdr_row["NO_EMP"].ToString());
            this.bpc�����.SetCodeName(pdr_row["NM_KOR"].ToString());

            // ����� �ڵ� & ��Ī ����
            this.bpc�����.SetCodeValue(pdr_row["CD_BIZAREA"].ToString());
            this.bpc�����.SetCodeName(pdr_row["NM_BIZAREA"].ToString());

            // ������ �ڵ� & ��Ī ����
            this.bpc������.SetCodeValue(pdr_row["CD_EXPORT"].ToString());
            this.bpc������.SetCodeName(pdr_row["NM_EXPORT"].ToString());

            //this.txt�����Ű��ȣ.Text = pdr_row["NO_TO"].ToString();
        }



        #endregion

        #region �� ���� ���� ����â �κ�

        /// <summary>
        /// ���� ���� ����â 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnBLText_Click(object sender, System.EventArgs e)
        {
            try
            {

                //DataRow row = ((DataRowView)this.m_comFgBL.SelectedItem).Row;
                //string ls_fg = this.m_comFgBL.text;

                //row = ((DataRowView)this.m_comCdCurrency.SelectedItem).Row;
                //string ls_currency = row["NAME"].ToString();

                trade.P_TR_EXBL_SUB obj = new trade.P_TR_EXBL_SUB(this.MainFrameInterface, this.m_HeadTable, cbo��������.Text, cbo��ȭ.Text);

                if (obj.ShowDialog() == DialogResult.OK)
                {

                }
                obj.Dispose();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
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

                if (!this.m_IsPageActivated)
                    return;

                if (this.m_HeadTable == null)
                    return;

                this.m_Manager.Position = 0;

                DataTable ldt_table = this.m_HeadTable.GetChanges();
                if (ldt_table != null)
                {
                    // TR_M000036
                    // �۾��Ͻ� �ڷḦ ���� �����ϼž� �մϴ�. ����Ͻðڽ��ϱ�?"
                    msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000036");
                    DialogResult result = MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (!this.Save())
                            return;
                    }
                    else if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                // ���߻�����, ������ȣ, ��ǥ����, ��ǥ������ڵ�, ��ǥ������, 
                // �μ��ڵ�, �μ���, ������ڵ� ,����ڸ�, C/C �ڵ�, C/C��
                string[] ls_args = new string[11];
                ls_args[0] = "����";
                ls_args[1] = this.m_HeadTable.Rows[0]["NO_BL"].ToString();
                ls_args[2] = this.dtp��ǥ����.Text;	// ��ǥ����
                ls_args[3] = bpc�����.CodeValue.ToString();
                ls_args[4] = this.bpc�����.CodeName.ToString();
                ls_args[5] = "";
                ls_args[6] = "";
                ls_args[7] = this.bpc�����.CodeValue.ToString();;
                ls_args[8] = this.bpc�����.CodeName.ToString(); ;
                ls_args[9] = "";
                ls_args[10] = "";

                //public P_TR_EXCOST(string[] p_args, DataTable p_originTable)

                object[] args = new Object[3];
                args[0] = ls_args;
                args[1] = this.m_HeadTable;
                args[2] = 4;	// ���� : 1 , L/C : 1, 3 : ���, 4 : ����

                // Main �� ��� �ִ��� Ȯ������ ��� ������ ������ �����ϰ� �׾� ������ �׳� ���Ͻ��ѹ�����.
                if (this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
                {
                    //- Ư�� ������ �ݱ�
                    this.UnLoadPage("P_TR_EXCOST", false);
                }

                string ls_LinePageName = this.MainFrameInterface.GetDataDictionaryItem("TRE", "INPUT_COST");

                this.LoadPageFrom("P_TR_EXCOST", ls_LinePageName, this.Grant, args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region �ܵ���â �κ�


        /// <summary>
        /// ����â�� ����.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSubPage(object sender, System.EventArgs e)
        {
            try
            {
                if (sender is ButtonExt)
                {
                    Button box = (ButtonExt)sender;
                    if (box.Name == "m_btnToRefer")	// ���� �Ű� ��ȣ
                    {
                        trade.P_TR_EXTONO_SUB obj = new trade.P_TR_EXTONO_SUB(this.MainFrameInterface);
                        if (obj.ShowDialog() == DialogResult.OK)
                        {
                            this.ApplyToRefer(obj.GetExTOHeadTable.Rows[0]);
                            this.m_IsPageActivated = true;
                            this.SetControlEnable();
                        }
                        obj.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        #endregion

   

        #region �� Ű �̺�Ʈ ó�� �κ�


        /// <summary>
        /// �޺� �ڽ� ���� Ű �̺�Ʈ ó��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="backControl"></param>
        /// <param name="nextControl"></param>
        private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }

        /// <summary>
        /// ���� Ű �̺�Ʈ ó��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonTextBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox box = (TextBox)sender;
                    if (box.Name == "m_txtRemark3")
                    {
                        this.txt������ȣ.Focus();
                        return;
                    }
                }
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
            }
            else if (e.KeyData == Keys.Up)
                System.Windows.Forms.SendKeys.SendWait("+{TAB}");
            else if (e.KeyData == Keys.Down)
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }



        #endregion

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.ControlName)
            {
                case "m_bpMA_EMP":
                    this.m_HeadTable.Rows[0]["NO_EMP"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_KOR"] = e.HelpReturn.CodeName;
                    break;
                case "m_bpCD_SALEGRP":
                    this.m_HeadTable.Rows[0]["CD_SALEGRP"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_SALEGRP"] = e.HelpReturn.CodeName;
                    break;
                case "m_bpCD_PARTNER":
                    this.m_HeadTable.Rows[0]["CD_PARTNER"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_PARTNER"] = e.HelpReturn.CodeName;
                    break;
                case "m_bpCD_EXPORT":
                    this.m_HeadTable.Rows[0]["CD_EXPORT"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_EXPORT"] = e.HelpReturn.CodeName;
                    break;
                case "m_bpSHIP_CORPT":
                    this.m_HeadTable.Rows[0]["SHIP_CORP"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_SHIP_CORP"] = e.HelpReturn.CodeName;
                    break;
                case "m_bpCD_BIZAREA":
                    this.m_HeadTable.Rows[0]["CD_BIZAREA"] = e.HelpReturn.CodeValue;
                    this.m_HeadTable.Rows[0]["NM_BIZAREA"] = e.HelpReturn.CodeName;
                    break;
            }
        }

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.ControlName)
            {
                case "bpCD_ACCT":
                    e.HelpParam.P33_TP_ACSTATS = "2";
                    break;
            }
        }

    }
}