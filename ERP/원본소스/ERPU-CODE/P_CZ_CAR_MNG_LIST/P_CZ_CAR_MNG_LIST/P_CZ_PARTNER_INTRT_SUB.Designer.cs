namespace cz
{
    partial class P_CZ_PARTNER_INTRT_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PARTNER_INTRT_SUB));
            this.m_titlePanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_타이틀 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMaing = new Duzon.Common.Controls.PanelExt();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.dtp대상월 = new Duzon.Common.Controls.DatePicker();
            this.dtp조회월 = new Duzon.Common.Controls.DatePicker();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.lbl요청일 = new Duzon.Common.Controls.LabelExt();
            this.panelExt9 = new Duzon.Common.Controls.PanelExt();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.m_titlePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlMaing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp대상월)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp조회월)).BeginInit();
            this.panelExt2.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.panelExt5.SuspendLayout();
            this.panelExt9.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_titlePanel
            // 
            this.m_titlePanel.BackColor = System.Drawing.Color.White;
            this.m_titlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_titlePanel.BackgroundImage")));
            this.m_titlePanel.Controls.Add(this.panel3);
            this.m_titlePanel.Controls.Add(this.panel1);
            this.m_titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_titlePanel.ForeColor = System.Drawing.Color.Black;
            this.m_titlePanel.Location = new System.Drawing.Point(0, 0);
            this.m_titlePanel.Name = "m_titlePanel";
            this.m_titlePanel.Size = new System.Drawing.Size(748, 47);
            this.m_titlePanel.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(648, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(100, 47);
            this.panel3.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.lbl_타이틀);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 47);
            this.panel1.TabIndex = 2;
            // 
            // lbl_타이틀
            // 
            this.lbl_타이틀.AutoSize = true;
            this.lbl_타이틀.BackColor = System.Drawing.Color.Transparent;
            this.lbl_타이틀.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_타이틀.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(44)))), ((int)(((byte)(80)))));
            this.lbl_타이틀.Location = new System.Drawing.Point(35, 19);
            this.lbl_타이틀.Name = "lbl_타이틀";
            this.lbl_타이틀.Size = new System.Drawing.Size(131, 15);
            this.lbl_타이틀.TabIndex = 0;
            this.lbl_타이틀.Text = "월별 이자율 복사";
            this.lbl_타이틀.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlMaing, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelExt9, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 87);
            this.tableLayoutPanel1.TabIndex = 130;
            // 
            // pnlMaing
            // 
            this.pnlMaing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMaing.Controls.Add(this.cbo공장);
            this.pnlMaing.Controls.Add(this.dtp대상월);
            this.pnlMaing.Controls.Add(this.dtp조회월);
            this.pnlMaing.Controls.Add(this.panelExt2);
            this.pnlMaing.Controls.Add(this.panelExt1);
            this.pnlMaing.Controls.Add(this.panelExt5);
            this.pnlMaing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMaing.Location = new System.Drawing.Point(9, 9);
            this.pnlMaing.Name = "pnlMaing";
            this.pnlMaing.Size = new System.Drawing.Size(730, 30);
            this.pnlMaing.TabIndex = 0;
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = true;
            this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(97, 4);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.ShowCheckBox = false;
            this.cbo공장.Size = new System.Drawing.Size(142, 20);
            this.cbo공장.TabIndex = 88;
            this.cbo공장.UseKeyEnter = true;
            this.cbo공장.UseKeyF3 = false;
            // 
            // dtp대상월
            // 
            this.dtp대상월.CalendarBackColor = System.Drawing.Color.White;
            this.dtp대상월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp대상월.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp대상월.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp대상월.Location = new System.Drawing.Point(548, 3);
            this.dtp대상월.Mask = "####/##";
            this.dtp대상월.MaskBackColor = System.Drawing.Color.White;
            this.dtp대상월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp대상월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp대상월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp대상월.Modified = false;
            this.dtp대상월.Name = "dtp대상월";
            this.dtp대상월.NullCheck = false;
            this.dtp대상월.PaddingCharacter = '_';
            this.dtp대상월.PassivePromptCharacter = '_';
            this.dtp대상월.PromptCharacter = '_';
            this.dtp대상월.SelectedDayColor = System.Drawing.Color.White;
            this.dtp대상월.ShowToDay = true;
            this.dtp대상월.ShowTodayCircle = true;
            this.dtp대상월.ShowUpDown = true;
            this.dtp대상월.Size = new System.Drawing.Size(74, 21);
            this.dtp대상월.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp대상월.TabIndex = 87;
            this.dtp대상월.TitleBackColor = System.Drawing.Color.White;
            this.dtp대상월.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp대상월.ToDayColor = System.Drawing.Color.Red;
            this.dtp대상월.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp대상월.UseKeyF3 = false;
            this.dtp대상월.Value = new System.DateTime(((long)(0)));
            // 
            // dtp조회월
            // 
            this.dtp조회월.CalendarBackColor = System.Drawing.Color.White;
            this.dtp조회월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp조회월.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp조회월.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp조회월.Location = new System.Drawing.Point(343, 3);
            this.dtp조회월.Mask = "####/##";
            this.dtp조회월.MaskBackColor = System.Drawing.Color.White;
            this.dtp조회월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp조회월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp조회월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp조회월.Modified = false;
            this.dtp조회월.Name = "dtp조회월";
            this.dtp조회월.NullCheck = false;
            this.dtp조회월.PaddingCharacter = '_';
            this.dtp조회월.PassivePromptCharacter = '_';
            this.dtp조회월.PromptCharacter = '_';
            this.dtp조회월.SelectedDayColor = System.Drawing.Color.White;
            this.dtp조회월.ShowToDay = true;
            this.dtp조회월.ShowTodayCircle = true;
            this.dtp조회월.ShowUpDown = true;
            this.dtp조회월.Size = new System.Drawing.Size(73, 21);
            this.dtp조회월.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp조회월.TabIndex = 86;
            this.dtp조회월.TitleBackColor = System.Drawing.Color.White;
            this.dtp조회월.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp조회월.ToDayColor = System.Drawing.Color.Red;
            this.dtp조회월.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp조회월.UseKeyF3 = false;
            this.dtp조회월.Value = new System.DateTime(((long)(0)));
            // 
            // panelExt2
            // 
            this.panelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt2.Controls.Add(this.labelExt2);
            this.panelExt2.Location = new System.Drawing.Point(-1, 0);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(92, 29);
            this.panelExt2.TabIndex = 85;
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.Transparent;
            this.labelExt2.Location = new System.Drawing.Point(5, 5);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(85, 18);
            this.labelExt2.TabIndex = 12;
            this.labelExt2.Tag = "";
            this.labelExt2.Text = "공장";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt1
            // 
            this.panelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt1.Controls.Add(this.labelExt1);
            this.panelExt1.Location = new System.Drawing.Point(452, -1);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(92, 29);
            this.panelExt1.TabIndex = 80;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.Transparent;
            this.labelExt1.Location = new System.Drawing.Point(5, 5);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(85, 18);
            this.labelExt1.TabIndex = 12;
            this.labelExt1.Tag = "CD_BIZAREA";
            this.labelExt1.Text = "대상월";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt5
            // 
            this.panelExt5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt5.Controls.Add(this.lbl요청일);
            this.panelExt5.Location = new System.Drawing.Point(245, -1);
            this.panelExt5.Name = "panelExt5";
            this.panelExt5.Size = new System.Drawing.Size(92, 29);
            this.panelExt5.TabIndex = 79;
            // 
            // lbl요청일
            // 
            this.lbl요청일.BackColor = System.Drawing.Color.Transparent;
            this.lbl요청일.Location = new System.Drawing.Point(5, 5);
            this.lbl요청일.Name = "lbl요청일";
            this.lbl요청일.Resizeble = true;
            this.lbl요청일.Size = new System.Drawing.Size(85, 18);
            this.lbl요청일.TabIndex = 12;
            this.lbl요청일.Tag = "CD_BIZAREA";
            this.lbl요청일.Text = "조회월";
            this.lbl요청일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt9
            // 
            this.panelExt9.Controls.Add(this.btn취소);
            this.panelExt9.Controls.Add(this.btn확인);
            this.panelExt9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt9.Location = new System.Drawing.Point(9, 45);
            this.panelExt9.Name = "panelExt9";
            this.panelExt9.Size = new System.Drawing.Size(730, 25);
            this.panelExt9.TabIndex = 123;
            // 
            // btn취소
            // 
            this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(666, 2);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(62, 19);
            this.btn취소.TabIndex = 125;
            this.btn취소.TabStop = false;
            this.btn취소.Tag = "";
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // btn확인
            // 
            this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(601, 2);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(62, 19);
            this.btn확인.TabIndex = 124;
            this.btn확인.TabStop = false;
            this.btn확인.Tag = "";
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = false;
            // 
            // P_CZ_PARTNER_INTRT_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(748, 134);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_titlePanel);
            this.Name = "P_CZ_PARTNER_INTRT_SUB";
            this.Text = "::  ERP iU  ::";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.m_titlePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlMaing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp대상월)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp조회월)).EndInit();
            this.panelExt2.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.panelExt5.ResumeLayout(false);
            this.panelExt9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_titlePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_타이틀;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt pnlMaing;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.LabelExt lbl요청일;
        private Duzon.Common.Controls.PanelExt panelExt9;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.DatePicker dtp조회월;
        private Duzon.Common.Controls.DatePicker dtp대상월;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
    }
}