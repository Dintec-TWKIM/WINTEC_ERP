using Duzon.Common.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    partial class P_CZ_HR_WTMCALC_MON_SUB
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
            this.components = (IContainer)new Container();
            this.panel1 = new FlexibleRoundedCornerBox();
            this.labelExt2 = new LabelExt();
            this.labelExt1 = new LabelExt();
            this._dp기간TO = new DatePicker();
            this._dp기간FROM = new DatePicker();
            this._lbl귀속일자 = new LabelExt();
            this._btn확인 = new RoundedButton(this.components);
            this._btn취소 = new RoundedButton(this.components);
            ((ISupportInitialize)this.closeButton).BeginInit();
            ((Control)this.panel1).SuspendLayout();
            ((ISupportInitialize)this._dp기간TO).BeginInit();
            ((ISupportInitialize)this._dp기간FROM).BeginInit();
            ((Control)this).SuspendLayout();
            ((Control)this.panel1).BackColor = Color.FromArgb(246, 247, 248);
            ((Control)this.panel1).Controls.Add((Control)this._lbl귀속일자);
            ((Control)this.panel1).Controls.Add((Control)this.labelExt2);
            ((Control)this.panel1).Controls.Add((Control)this.labelExt1);
            ((Control)this.panel1).Controls.Add((Control)this._dp기간TO);
            ((Control)this.panel1).Controls.Add((Control)this._dp기간FROM);
            ((Control)this.panel1).Location = new Point(8, 53);
            ((Control)this.panel1).Name = "panel1";
            ((Control)this.panel1).Size = new Size(379, 36);
            ((Control)this.panel1).TabIndex = 145;
            ((Control)this.labelExt2).BackColor = Color.Transparent;
            ((Control)this.labelExt2).Location = new Point(313, 9);
            ((Control)this.labelExt2).Name = "labelExt2";
            this.labelExt2.Resizeble = (true);
            ((Control)this.labelExt2).Size = new Size(55, 18);
            ((Control)this.labelExt2).TabIndex = 12;
            ((Control)this.labelExt2).Text = "일 까지";
            ((Label)this.labelExt2).TextAlign = ContentAlignment.MiddleCenter;
            ((Control)this.labelExt1).BackColor = Color.Transparent;
            ((Control)this.labelExt1).Location = new Point(171, 9);
            ((Control)this.labelExt1).Name = "labelExt1";
            this.labelExt1.Resizeble = (true);
            ((Control)this.labelExt1).Size = new Size(48, 18);
            ((Control)this.labelExt1).TabIndex = 11;
            ((Control)this.labelExt1).Text = "일 부터";
            ((Label)this.labelExt1).TextAlign = ContentAlignment.MiddleCenter;
            this._dp기간TO.CalendarBackColor = (Color.White);
            ((Control)this._dp기간TO).Cursor = Cursors.Hand;
            this._dp기간TO.DayColor = (Color.FromArgb(51, 51, 51));
            this._dp기간TO.FriDayColor = (Color.FromArgb(43, 72, 125));
            ((Control)this._dp기간TO).Location = new Point(225, 7);
            this._dp기간TO.Mask = ("####/##/##");
            this._dp기간TO.MaskBackColor = (Color.FromArgb((int)byte.MaxValue, 237, 242));
            this._dp기간TO.MaxDate = (new DateTime(9999, 12, 31, 23, 59, 59, 999));
            ((Control)this._dp기간TO).MaximumSize = new Size(0, 21);
            this._dp기간TO.MinDate = (new DateTime(1800, 1, 1, 0, 0, 0, 0));
            this._dp기간TO.Modified = (false);
            ((Control)this._dp기간TO).Name = "_dp기간TO";
            this._dp기간TO.PaddingCharacter = ('_');
            this._dp기간TO.PassivePromptCharacter = ('_');
            this._dp기간TO.PromptCharacter = ('_');
            this._dp기간TO.SelectedDayColor = (Color.White);
            this._dp기간TO.ShowToDay = (true);
            this._dp기간TO.ShowTodayCircle = (true);
            this._dp기간TO.ShowUpDown = (false);
            ((Control)this._dp기간TO).Size = new Size(82, 21);
            this._dp기간TO.SunDayColor = (Color.FromArgb(244, 104, 90));
            ((Control)this._dp기간TO).TabIndex = 10;
            ((Control)this._dp기간TO).Tag = (object)"";
            this._dp기간TO.TitleBackColor = (Color.White);
            this._dp기간TO.TitleForeColor = (Color.FromArgb(51, 51, 51));
            this._dp기간TO.ToDayColor = (Color.Red);
            this._dp기간TO.TrailingForeColor = (SystemColors.ControlDark);
            this._dp기간TO.UseKeyF3 = (false);
            this._dp기간TO.Value = (new DateTime(0L));
            this._dp기간FROM.CalendarBackColor = (Color.White);
            ((Control)this._dp기간FROM).Cursor = Cursors.Hand;
            this._dp기간FROM.DayColor = (Color.FromArgb(51, 51, 51));
            this._dp기간FROM.FriDayColor = (Color.FromArgb(43, 72, 125));
            ((Control)this._dp기간FROM).Location = new Point(86, 7);
            this._dp기간FROM.Mask = ("####/##/##");
            this._dp기간FROM.MaskBackColor = (Color.FromArgb((int)byte.MaxValue, 237, 242));
            this._dp기간FROM.MaxDate = (new DateTime(9999, 12, 31, 23, 59, 59, 999));
            ((Control)this._dp기간FROM).MaximumSize = new Size(0, 21);
            this._dp기간FROM.MinDate = (new DateTime(1800, 1, 1, 0, 0, 0, 0));
            this._dp기간FROM.Modified = (false);
            ((Control)this._dp기간FROM).Name = "_dp기간FROM";
            this._dp기간FROM.PaddingCharacter = ('_');
            this._dp기간FROM.PassivePromptCharacter = ('_');
            this._dp기간FROM.PromptCharacter = ('_');
            this._dp기간FROM.SelectedDayColor = (Color.White);
            this._dp기간FROM.ShowToDay = (true);
            this._dp기간FROM.ShowTodayCircle = (true);
            this._dp기간FROM.ShowUpDown = (false);
            ((Control)this._dp기간FROM).Size = new Size(82, 21);
            this._dp기간FROM.SunDayColor = (Color.FromArgb(244, 104, 90));
            ((Control)this._dp기간FROM).TabIndex = 9;
            ((Control)this._dp기간FROM).Tag = (object)"";
            this._dp기간FROM.TitleBackColor = (Color.White);
            this._dp기간FROM.TitleForeColor = (Color.FromArgb(51, 51, 51));
            this._dp기간FROM.ToDayColor = (Color.Red);
            this._dp기간FROM.TrailingForeColor = (SystemColors.ControlDark);
            this._dp기간FROM.UseKeyF3 = (false);
            this._dp기간FROM.Value = (new DateTime(0L));
            ((Control)this._lbl귀속일자).Location = new Point(9, 9);
            ((Control)this._lbl귀속일자).Name = "_lbl귀속일자";
            this._lbl귀속일자.Resizeble = (true);
            ((Control)this._lbl귀속일자).Size = new Size(74, 18);
            ((Control)this._lbl귀속일자).TabIndex = 0;
            ((Control)this._lbl귀속일자).Tag = (object)"";
            ((Control)this._lbl귀속일자).Text = "적용기간";
            ((Label)this._lbl귀속일자).TextAlign = ContentAlignment.MiddleRight;
            ((Control)this._btn확인).BackColor = Color.White;
            ((Control)this._btn확인).Cursor = Cursors.Hand;
            (this._btn확인).FlatStyle = FlatStyle.Flat;
            ((Control)this._btn확인).Location = new Point(137, 94);
            ((Control)this._btn확인).MaximumSize = new Size(0, 19);
            ((Control)this._btn확인).Name = "_btn확인";
            ((Control)this._btn확인).Size = new Size(62, 19);
            ((Control)this._btn확인).TabIndex = 148;
            ((Control)this._btn확인).TabStop = false;
            ((Control)this._btn확인).Text = "확인";
            (this._btn확인).UseVisualStyleBackColor = false;
            ((Control)this._btn취소).BackColor = Color.White;
            ((Control)this._btn취소).Cursor = Cursors.Hand;
            ((Button)this._btn취소).DialogResult = DialogResult.Cancel;
            (this._btn취소).FlatStyle = FlatStyle.Flat;
            ((Control)this._btn취소).Location = new Point(202, 94);
            ((Control)this._btn취소).MaximumSize = new Size(0, 19);
            ((Control)this._btn취소).Name = "_btn취소";
            ((Control)this._btn취소).Size = new Size(62, 19);
            ((Control)this._btn취소).TabIndex = 147;
            ((Control)this._btn취소).TabStop = false;
            ((Control)this._btn취소).Text = "취소";
            (this._btn취소).UseVisualStyleBackColor = false;
            this.AutoScaleMode = (AutoScaleMode.Inherit);
            this.CaptionPaint = (true);
            ((Form)this).ClientSize = new Size(396, 118);
            ((Control)this).Controls.Add((Control)this._btn확인);
            ((Control)this).Controls.Add((Control)this._btn취소);
            ((Control)this).Controls.Add((Control)this.panel1);
            ((Control)this).Name = "P_CZ_HR_WTMCALC_MON_SUB";
            ((Form)this).StartPosition = FormStartPosition.CenterParent;
            this.TitleText = ("적용기간 도움창");
            ((ISupportInitialize)this.closeButton).EndInit();
            ((Control)this.panel1).ResumeLayout(false);
            ((ISupportInitialize)this._dp기간TO).EndInit();
            ((ISupportInitialize)this._dp기간FROM).EndInit();
            ((Control)this).ResumeLayout(false);
        }

        #endregion

        private FlexibleRoundedCornerBox panel1;
        private LabelExt _lbl귀속일자;
        private RoundedButton _btn확인;
        private RoundedButton _btn취소;
        private LabelExt labelExt1;
        private DatePicker _dp기간TO;
        private DatePicker _dp기간FROM;
        private LabelExt labelExt2;
    }
}