using Duzon.Common.Controls;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace cz
{
    partial class P_CZ_MA_EXCHANGE_SUB
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
            this.m_lblBaseDate = new Duzon.Common.Controls.LabelExt();
            this.m_lblCreateDate = new Duzon.Common.Controls.LabelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.dtpTO = new Duzon.Common.Controls.DatePicker();
            this.dtpBasic = new Duzon.Common.Controls.DatePicker();
            this.txtNoSeq = new Duzon.Common.Controls.TextBoxExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.dtpFROM = new Duzon.Common.Controls.DatePicker();
            this.btnCopy = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_titlePanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBasic)).BeginInit();
            this.panel10.SuspendLayout();
            this.panelExt1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFROM)).BeginInit();
            this.m_titlePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.closeButton.Location = new System.Drawing.Point(368, 3);
            // 
            // m_lblBaseDate
            // 
            this.m_lblBaseDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblBaseDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblBaseDate.Location = new System.Drawing.Point(3, 4);
            this.m_lblBaseDate.Name = "m_lblBaseDate";
            this.m_lblBaseDate.Size = new System.Drawing.Size(75, 18);
            this.m_lblBaseDate.TabIndex = 1;
            this.m_lblBaseDate.Tag = "YYMM_BASE";
            this.m_lblBaseDate.Text = "기준년월";
            this.m_lblBaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCreateDate
            // 
            this.m_lblCreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCreateDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblCreateDate.Location = new System.Drawing.Point(3, 30);
            this.m_lblCreateDate.Name = "m_lblCreateDate";
            this.m_lblCreateDate.Size = new System.Drawing.Size(75, 18);
            this.m_lblCreateDate.TabIndex = 2;
            this.m_lblCreateDate.Tag = "YYMM_CRT";
            this.m_lblCreateDate.Text = "생성년월";
            this.m_lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(186, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = "∼";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dtpTO);
            this.panel2.Controls.Add(this.dtpBasic);
            this.panel2.Controls.Add(this.txtNoSeq);
            this.panel2.Controls.Add(this.panel11);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_lblCreateDate);
            this.panel2.Controls.Add(this.panel10);
            this.panel2.Controls.Add(this.panelExt1);
            this.panel2.Controls.Add(this.dtpFROM);
            this.panel2.Location = new System.Drawing.Point(9, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(375, 55);
            this.panel2.TabIndex = 13;
            // 
            // dtpTO
            // 
            this.dtpTO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTO.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtpTO.FriDayColor = System.Drawing.Color.Blue;
            this.dtpTO.Location = new System.Drawing.Point(208, 29);
            this.dtpTO.Mask = "####/##/##";
            this.dtpTO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpTO.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpTO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpTO.Name = "dtpTO";
            this.dtpTO.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtpTO.Size = new System.Drawing.Size(96, 21);
            this.dtpTO.SunDayColor = System.Drawing.Color.Red;
            this.dtpTO.TabIndex = 82;
            this.dtpTO.Tag = "";
            this.dtpTO.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtpTO.TitleForeColor = System.Drawing.Color.Black;
            this.dtpTO.Value = new System.DateTime(((long)(0)));
            // 
            // dtpBasic
            // 
            this.dtpBasic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpBasic.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtpBasic.FriDayColor = System.Drawing.Color.Blue;
            this.dtpBasic.Location = new System.Drawing.Point(84, 3);
            this.dtpBasic.Mask = "####/##/##";
            this.dtpBasic.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpBasic.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpBasic.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpBasic.Name = "dtpBasic";
            this.dtpBasic.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtpBasic.Size = new System.Drawing.Size(96, 21);
            this.dtpBasic.SunDayColor = System.Drawing.Color.Red;
            this.dtpBasic.TabIndex = 80;
            this.dtpBasic.Tag = "";
            this.dtpBasic.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtpBasic.TitleForeColor = System.Drawing.Color.Black;
            this.dtpBasic.Value = new System.DateTime(((long)(0)));
            // 
            // txtNoSeq
            // 
            this.txtNoSeq.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txtNoSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSeq.Location = new System.Drawing.Point(274, 3);
            this.txtNoSeq.Name = "txtNoSeq";
            this.txtNoSeq.Size = new System.Drawing.Size(30, 21);
            this.txtNoSeq.TabIndex = 79;
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = global::cz.Properties.Resources.BackgroundImage3;
            this.panel11.Location = new System.Drawing.Point(5, 26);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(365, 1);
            this.panel11.TabIndex = 77;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel10.Controls.Add(this.m_lblBaseDate);
            this.panel10.Location = new System.Drawing.Point(1, 1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(80, 51);
            this.panel10.TabIndex = 20;
            // 
            // panelExt1
            // 
            this.panelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt1.Controls.Add(this.labelExt1);
            this.panelExt1.Location = new System.Drawing.Point(190, 1);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(81, 26);
            this.panelExt1.TabIndex = 78;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelExt1.Location = new System.Drawing.Point(3, 4);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(75, 18);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Tag = "YYMM_BASE";
            this.labelExt1.Text = "고시회차";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFROM
            // 
            this.dtpFROM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFROM.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtpFROM.FriDayColor = System.Drawing.Color.Blue;
            this.dtpFROM.Location = new System.Drawing.Point(84, 29);
            this.dtpFROM.Mask = "####/##/##";
            this.dtpFROM.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpFROM.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpFROM.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpFROM.Name = "dtpFROM";
            this.dtpFROM.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtpFROM.Size = new System.Drawing.Size(96, 21);
            this.dtpFROM.SunDayColor = System.Drawing.Color.Red;
            this.dtpFROM.TabIndex = 81;
            this.dtpFROM.Tag = "";
            this.dtpFROM.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtpFROM.TitleForeColor = System.Drawing.Color.Black;
            this.dtpFROM.Value = new System.DateTime(((long)(0)));
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.White;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(244, 53);
            this.btnCopy.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(70, 19);
            this.btnCopy.TabIndex = 14;
            this.btnCopy.TabStop = false;
            this.btnCopy.Text = "복사";
            this.btnCopy.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(315, 53);
            this.btnCancel.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 19);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // m_titlePanel
            // 
            this.m_titlePanel.BackColor = System.Drawing.Color.White;
            this.m_titlePanel.BackgroundImage = global::cz.Properties.Resources.BackgroundImage1;
            this.m_titlePanel.Controls.Add(this.panel3);
            this.m_titlePanel.Controls.Add(this.panel1);
            this.m_titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_titlePanel.ForeColor = System.Drawing.Color.Black;
            this.m_titlePanel.Location = new System.Drawing.Point(0, 0);
            this.m_titlePanel.Name = "m_titlePanel";
            this.m_titlePanel.Size = new System.Drawing.Size(394, 47);
            this.m_titlePanel.TabIndex = 155;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::cz.Properties.Resources.BackgroundImage4;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(295, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(99, 47);
            this.panel3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::cz.Properties.Resources.BackgroundImage2;
            this.panel1.Controls.Add(this._lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 47);
            this.panel1.TabIndex = 2;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.BackColor = System.Drawing.Color.Transparent;
            this._lblTitle.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(44)))), ((int)(((byte)(80)))));
            this._lblTitle.Location = new System.Drawing.Point(30, 19);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(133, 14);
            this._lblTitle.TabIndex = 1;
            this._lblTitle.Text = "환율관리정보 복사";
            this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // P_CZ_MA_EXCHANGE_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 152);
            this.Controls.Add(this.m_titlePanel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "P_CZ_MA_EXCHANGE_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBasic)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpFROM)).EndInit();
            this.m_titlePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private LabelExt m_lblBaseDate;
        private LabelExt m_lblCreateDate;
        private LabelExt label1;
        private PanelExt panel2;
        private RoundedButton btnCopy;
        private RoundedButton btnCancel;
        private PanelExt panel10;
        private PanelExt panel11;
        private Panel m_titlePanel;
        private Panel panel3;
        private Panel panel1;
        private Label _lblTitle;
        private PanelExt panelExt1;
        private LabelExt labelExt1;
        private TextBoxExt txtNoSeq;
        private DatePicker dtpTO;
        private DatePicker dtpBasic;
        private DatePicker dtpFROM;

        #endregion
    }
}