//********************************************************************
// 작   성   자 : 김봉회 ## 
// 작   성   일 : 
// 수   성   자 : 심현주/김정근(2006-07-12)
// 모   듈   명 : 무역관리
// 시 스  템 명 : 수출관리
// 서브시스템명 : 수출선적등록
// 페 이 지  명 : 
// 프로젝트  명 : P_TR_EXBL
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
        #region ● 초기화 자동 선언 부분

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
        private Duzon.Common.Controls.RoundedButton btn판매경비등록;
        private Duzon.Common.Controls.RoundedButton btn선적내역;
        private Duzon.Common.Controls.DzComboBox cbo선적구분;
        private Duzon.Common.Controls.CurrencyTextBox txtDAYS;
        private Duzon.Common.Controls.CurrencyTextBox cur외화금액;
        private Duzon.Common.Controls.CurrencyTextBox cur기표금액;
        private Duzon.Common.Controls.CurrencyTextBox cur기표환율;
        private Duzon.Common.Controls.TextBoxExt txt비고1;
        private Duzon.Common.Controls.DzComboBox cboLC구분;
        private Duzon.Common.Controls.TextBoxExt txt송장번호;
        private Duzon.Common.Controls.LabelExt lblDAYS;
        private Duzon.Common.Controls.DzComboBox cbo결제형태;
         private Duzon.Common.Controls.DzComboBox cbo통화;
        private Duzon.Common.Controls.TextBoxExt txt선적번호;
        private Duzon.Common.Controls.TextBoxExt txtVESSEL명;
        private Duzon.Common.Controls.TextBoxExt txt비고3;
        private Duzon.Common.Controls.TextBoxExt txt비고2;
        private Duzon.Common.Controls.TextBoxExt txt선적지;
        private Duzon.Common.Controls.TextBoxExt txt도착지;
        private Duzon.Common.Controls.LabelExt lbl거래처;
        private Duzon.Common.Controls.LabelExt lbl외화금액;
        private Duzon.Common.Controls.LabelExt lbl기표금액;
        private Duzon.Common.Controls.LabelExt lbl기표환율;
        private Duzon.Common.Controls.LabelExt lbl선사;
        private Duzon.Common.Controls.LabelExt lbl선적일자;
        private Duzon.Common.Controls.LabelExt lbl영업그룹;
        private Duzon.Common.Controls.LabelExt lbl참조신고번호;
        private Duzon.Common.Controls.LabelExt lbl담당자;
        private Duzon.Common.Controls.LabelExt lbl수출자;
        private Duzon.Common.Controls.LabelExt lbl도착국;
        private Duzon.Common.Controls.LabelExt lbl결제형태;
        private Duzon.Common.Controls.LabelExt lbl비고;
        private Duzon.Common.Controls.LabelExt lbl도착지;
        private Duzon.Common.Controls.LabelExt lbl선적지;
        private Duzon.Common.Controls.LabelExt lblVESSEL명;
        private Duzon.Common.Controls.LabelExt lbl송장번호;
        private Duzon.Common.Controls.LabelExt lbl선적조건;
        private Duzon.Common.Controls.LabelExt lbl통화;
        private Duzon.Common.Controls.LabelExt lbl도착예정일자;
        private Duzon.Common.Controls.LabelExt lbl기표일자;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.Controls.LabelExt lbl선적번호;
        private Duzon.Common.Controls.LabelExt lbl선적구분;
        private Duzon.Common.Controls.LabelExt lbl결제만기일;
        private Duzon.Common.Controls.LabelExt lblLC구분;
         private Duzon.Common.Controls.RoundedButton btn매출전표처리;
        private Duzon.Common.Controls.DzComboBox cbo도착국;
        private Duzon.Common.Controls.DzComboBox cbo선적조건;
        private Duzon.Common.BpControls.BpCodeTextBox bpc담당자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc영업그룹;
        private Duzon.Common.BpControls.BpCodeTextBox bpc거래처;
        private Duzon.Common.BpControls.BpCodeTextBox bpc수출자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc선사;
        private Duzon.Common.BpControls.BpCodeTextBox bpc사업장;
        private DatePicker dtp기표일자;
        private DatePicker dtp선적일자;
        private DatePicker dtp도착예정일자;
        private DatePicker dtp결제만기일;
         private PanelExt panelExt1;
         private TableLayoutPanel tableLayoutPanel1;
         private TextButton tbtn참조신고번호;
        private System.ComponentModel.IContainer components;

        #endregion

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
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
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXBL_BAK));
            this.btn판매경비등록 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn선적내역 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.tbtn참조신고번호 = new Duzon.Common.Controls.TextButton();
            this.dtp결제만기일 = new Duzon.Common.Controls.DatePicker();
            this.dtp도착예정일자 = new Duzon.Common.Controls.DatePicker();
            this.dtp선적일자 = new Duzon.Common.Controls.DatePicker();
            this.dtp기표일자 = new Duzon.Common.Controls.DatePicker();
            this.bpc사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc선사 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc수출자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cbo선적조건 = new Duzon.Common.Controls.DzComboBox();
            this.cbo도착국 = new Duzon.Common.Controls.DzComboBox();
            this.cbo선적구분 = new Duzon.Common.Controls.DzComboBox();
            this.txtDAYS = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur외화금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur기표금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur기표환율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.txt비고1 = new Duzon.Common.Controls.TextBoxExt();
            this.cboLC구분 = new Duzon.Common.Controls.DzComboBox();
            this.txt송장번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lblDAYS = new Duzon.Common.Controls.LabelExt();
            this.cbo결제형태 = new Duzon.Common.Controls.DzComboBox();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.cbo통화 = new Duzon.Common.Controls.DzComboBox();
            this.txt선적번호 = new Duzon.Common.Controls.TextBoxExt();
            this.txtVESSEL명 = new Duzon.Common.Controls.TextBoxExt();
            this.txt비고3 = new Duzon.Common.Controls.TextBoxExt();
            this.txt비고2 = new Duzon.Common.Controls.TextBoxExt();
            this.txt선적지 = new Duzon.Common.Controls.TextBoxExt();
            this.txt도착지 = new Duzon.Common.Controls.TextBoxExt();
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
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.lbl외화금액 = new Duzon.Common.Controls.LabelExt();
            this.lbl기표금액 = new Duzon.Common.Controls.LabelExt();
            this.lbl기표환율 = new Duzon.Common.Controls.LabelExt();
            this.lbl선사 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적일자 = new Duzon.Common.Controls.LabelExt();
            this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
            this.lbl참조신고번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.lbl수출자 = new Duzon.Common.Controls.LabelExt();
            this.lbl도착국 = new Duzon.Common.Controls.LabelExt();
            this.lbl결제형태 = new Duzon.Common.Controls.LabelExt();
            this.lbl비고 = new Duzon.Common.Controls.LabelExt();
            this.lbl도착지 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적지 = new Duzon.Common.Controls.LabelExt();
            this.lblVESSEL명 = new Duzon.Common.Controls.LabelExt();
            this.lbl송장번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적조건 = new Duzon.Common.Controls.LabelExt();
            this.lbl통화 = new Duzon.Common.Controls.LabelExt();
            this.lbl도착예정일자 = new Duzon.Common.Controls.LabelExt();
            this.lbl기표일자 = new Duzon.Common.Controls.LabelExt();
            this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적구분 = new Duzon.Common.Controls.LabelExt();
            this.lbl결제만기일 = new Duzon.Common.Controls.LabelExt();
            this.lblLC구분 = new Duzon.Common.Controls.LabelExt();
            this.btn매출전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp결제만기일)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp도착예정일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp선적일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기표일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDAYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur외화금액)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur기표금액)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur기표환율)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn판매경비등록
            // 
            this.btn판매경비등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn판매경비등록.BackColor = System.Drawing.Color.White;
            this.btn판매경비등록.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn판매경비등록.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn판매경비등록.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn판매경비등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn판매경비등록.Location = new System.Drawing.Point(563, 3);
            this.btn판매경비등록.Name = "btn판매경비등록";
            this.btn판매경비등록.Size = new System.Drawing.Size(120, 24);
            this.btn판매경비등록.TabIndex = 133;
            this.btn판매경비등록.TabStop = false;
            this.btn판매경비등록.Text = "판매경비등록";
            this.btn판매경비등록.UseVisualStyleBackColor = false;
            this.btn판매경비등록.Click += new System.EventHandler(this.m_btnInputCost_Click);
            // 
            // btn선적내역
            // 
            this.btn선적내역.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn선적내역.BackColor = System.Drawing.Color.White;
            this.btn선적내역.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn선적내역.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn선적내역.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn선적내역.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn선적내역.Location = new System.Drawing.Point(685, 3);
            this.btn선적내역.Name = "btn선적내역";
            this.btn선적내역.Size = new System.Drawing.Size(100, 24);
            this.btn선적내역.TabIndex = 132;
            this.btn선적내역.TabStop = false;
            this.btn선적내역.Text = "선적내역";
            this.btn선적내역.UseVisualStyleBackColor = false;
            this.btn선적내역.Click += new System.EventHandler(this.m_btnBLText_Click);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tbtn참조신고번호);
            this.panel4.Controls.Add(this.dtp결제만기일);
            this.panel4.Controls.Add(this.dtp도착예정일자);
            this.panel4.Controls.Add(this.dtp선적일자);
            this.panel4.Controls.Add(this.dtp기표일자);
            this.panel4.Controls.Add(this.bpc사업장);
            this.panel4.Controls.Add(this.bpc선사);
            this.panel4.Controls.Add(this.bpc수출자);
            this.panel4.Controls.Add(this.bpc거래처);
            this.panel4.Controls.Add(this.bpc영업그룹);
            this.panel4.Controls.Add(this.bpc담당자);
            this.panel4.Controls.Add(this.cbo선적조건);
            this.panel4.Controls.Add(this.cbo도착국);
            this.panel4.Controls.Add(this.cbo선적구분);
            this.panel4.Controls.Add(this.txtDAYS);
            this.panel4.Controls.Add(this.cur외화금액);
            this.panel4.Controls.Add(this.cur기표금액);
            this.panel4.Controls.Add(this.cur기표환율);
            this.panel4.Controls.Add(this.txt비고1);
            this.panel4.Controls.Add(this.cboLC구분);
            this.panel4.Controls.Add(this.txt송장번호);
            this.panel4.Controls.Add(this.lblDAYS);
            this.panel4.Controls.Add(this.cbo결제형태);
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.cbo통화);
            this.panel4.Controls.Add(this.txt선적번호);
            this.panel4.Controls.Add(this.txtVESSEL명);
            this.panel4.Controls.Add(this.txt비고3);
            this.panel4.Controls.Add(this.txt비고2);
            this.panel4.Controls.Add(this.txt선적지);
            this.panel4.Controls.Add(this.txt도착지);
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
            // tbtn참조신고번호
            // 
            this.tbtn참조신고번호.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tbtn참조신고번호.ButtonImage")));
            this.tbtn참조신고번호.Location = new System.Drawing.Point(510, 3);
            this.tbtn참조신고번호.Name = "tbtn참조신고번호";
            this.tbtn참조신고번호.Size = new System.Drawing.Size(267, 21);
            this.tbtn참조신고번호.TabIndex = 204;
            this.tbtn참조신고번호.Tag = "NO_TO";
            this.tbtn참조신고번호.Text = "textButton1";
            // 
            // dtp결제만기일
            // 
            this.dtp결제만기일.BackColor = System.Drawing.Color.White;
            this.dtp결제만기일.CalendarBackColor = System.Drawing.Color.White;
            this.dtp결제만기일.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp결제만기일.FriDayColor = System.Drawing.Color.Blue;
            this.dtp결제만기일.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp결제만기일.Location = new System.Drawing.Point(121, 253);
            this.dtp결제만기일.Mask = "####/##/##";
            this.dtp결제만기일.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp결제만기일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp결제만기일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp결제만기일.Modified = false;
            this.dtp결제만기일.Name = "dtp결제만기일";
            this.dtp결제만기일.PaddingCharacter = '_';
            this.dtp결제만기일.PassivePromptCharacter = '_';
            this.dtp결제만기일.PromptCharacter = '_';
            this.dtp결제만기일.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp결제만기일.ShowToDay = true;
            this.dtp결제만기일.ShowTodayCircle = true;
            this.dtp결제만기일.ShowUpDown = false;
            this.dtp결제만기일.Size = new System.Drawing.Size(87, 21);
            this.dtp결제만기일.SunDayColor = System.Drawing.Color.Red;
            this.dtp결제만기일.TabIndex = 20;
            this.dtp결제만기일.Tag = "DT_PAYABLE";
            this.dtp결제만기일.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp결제만기일.TitleForeColor = System.Drawing.Color.Black;
            this.dtp결제만기일.ToDayColor = System.Drawing.Color.Red;
            this.dtp결제만기일.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp결제만기일.UseKeyF3 = false;
            this.dtp결제만기일.Value = new System.DateTime(((long)(0)));
            // 
            // dtp도착예정일자
            // 
            this.dtp도착예정일자.BackColor = System.Drawing.Color.White;
            this.dtp도착예정일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp도착예정일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp도착예정일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp도착예정일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp도착예정일자.Location = new System.Drawing.Point(121, 103);
            this.dtp도착예정일자.Mask = "####/##/##";
            this.dtp도착예정일자.MaskBackColor = System.Drawing.Color.White;
            this.dtp도착예정일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp도착예정일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp도착예정일자.Modified = false;
            this.dtp도착예정일자.Name = "dtp도착예정일자";
            this.dtp도착예정일자.PaddingCharacter = '_';
            this.dtp도착예정일자.PassivePromptCharacter = '_';
            this.dtp도착예정일자.PromptCharacter = '_';
            this.dtp도착예정일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp도착예정일자.ShowToDay = true;
            this.dtp도착예정일자.ShowTodayCircle = true;
            this.dtp도착예정일자.ShowUpDown = false;
            this.dtp도착예정일자.Size = new System.Drawing.Size(87, 21);
            this.dtp도착예정일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp도착예정일자.TabIndex = 8;
            this.dtp도착예정일자.Tag = "DT_ARRIVAL";
            this.dtp도착예정일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp도착예정일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp도착예정일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp도착예정일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp도착예정일자.UseKeyF3 = false;
            this.dtp도착예정일자.Value = new System.DateTime(((long)(0)));
            // 
            // dtp선적일자
            // 
            this.dtp선적일자.BackColor = System.Drawing.Color.White;
            this.dtp선적일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp선적일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp선적일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp선적일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp선적일자.Location = new System.Drawing.Point(510, 78);
            this.dtp선적일자.Mask = "####/##/##";
            this.dtp선적일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp선적일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp선적일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp선적일자.Modified = false;
            this.dtp선적일자.Name = "dtp선적일자";
            this.dtp선적일자.PaddingCharacter = '_';
            this.dtp선적일자.PassivePromptCharacter = '_';
            this.dtp선적일자.PromptCharacter = '_';
            this.dtp선적일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp선적일자.ShowToDay = true;
            this.dtp선적일자.ShowTodayCircle = true;
            this.dtp선적일자.ShowUpDown = false;
            this.dtp선적일자.Size = new System.Drawing.Size(87, 21);
            this.dtp선적일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp선적일자.TabIndex = 7;
            this.dtp선적일자.Tag = "DT_LOADING";
            this.dtp선적일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp선적일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp선적일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp선적일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp선적일자.UseKeyF3 = false;
            this.dtp선적일자.Value = new System.DateTime(((long)(0)));
            // 
            // dtp기표일자
            // 
            this.dtp기표일자.BackColor = System.Drawing.Color.White;
            this.dtp기표일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp기표일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp기표일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp기표일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp기표일자.Location = new System.Drawing.Point(121, 78);
            this.dtp기표일자.Mask = "####/##/##";
            this.dtp기표일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp기표일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp기표일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp기표일자.Modified = false;
            this.dtp기표일자.Name = "dtp기표일자";
            this.dtp기표일자.PaddingCharacter = '_';
            this.dtp기표일자.PassivePromptCharacter = '_';
            this.dtp기표일자.PromptCharacter = '_';
            this.dtp기표일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp기표일자.ShowToDay = true;
            this.dtp기표일자.ShowTodayCircle = true;
            this.dtp기표일자.ShowUpDown = false;
            this.dtp기표일자.Size = new System.Drawing.Size(87, 21);
            this.dtp기표일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp기표일자.TabIndex = 6;
            this.dtp기표일자.Tag = "DT_BALLOT";
            this.dtp기표일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp기표일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp기표일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp기표일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp기표일자.UseKeyF3 = false;
            this.dtp기표일자.Value = new System.DateTime(((long)(0)));
            // 
            // bpc사업장
            // 
            this.bpc사업장.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc사업장.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc사업장.ButtonImage")));
            this.bpc사업장.ChildMode = "";
            this.bpc사업장.CodeName = "";
            this.bpc사업장.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc사업장.CodeValue = "";
            this.bpc사업장.ComboCheck = true;
            this.bpc사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
            this.bpc사업장.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc사업장.Location = new System.Drawing.Point(121, 53);
            this.bpc사업장.Name = "bpc사업장";
            this.bpc사업장.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc사업장.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc사업장.SearchCode = true;
            this.bpc사업장.SelectCount = 0;
            this.bpc사업장.SetDefaultValue = true;
            this.bpc사업장.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc사업장.Size = new System.Drawing.Size(267, 21);
            this.bpc사업장.TabIndex = 4;
            this.bpc사업장.Tag = "CD_BIZAREA";
            this.bpc사업장.Text = "bpCodeTextBox1";
            this.bpc사업장.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc사업장.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc선사
            // 
            this.bpc선사.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc선사.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc선사.ButtonImage")));
            this.bpc선사.ChildMode = "";
            this.bpc선사.CodeName = "";
            this.bpc선사.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc선사.CodeValue = "";
            this.bpc선사.ComboCheck = true;
            this.bpc선사.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BANK_SUB;
            this.bpc선사.ItemBackColor = System.Drawing.Color.White;
            this.bpc선사.Location = new System.Drawing.Point(510, 128);
            this.bpc선사.Name = "bpc선사";
            this.bpc선사.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc선사.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc선사.SearchCode = true;
            this.bpc선사.SelectCount = 0;
            this.bpc선사.SetDefaultValue = false;
            this.bpc선사.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc선사.Size = new System.Drawing.Size(267, 21);
            this.bpc선사.TabIndex = 11;
            this.bpc선사.Tag = "SHIP_CORP";
            this.bpc선사.Text = "bpCodeTextBox1";
            this.bpc선사.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc선사.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc수출자
            // 
            this.bpc수출자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc수출자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc수출자.ButtonImage")));
            this.bpc수출자.ChildMode = "";
            this.bpc수출자.CodeName = "";
            this.bpc수출자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc수출자.CodeValue = "";
            this.bpc수출자.ComboCheck = true;
            this.bpc수출자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BANK_SUB;
            this.bpc수출자.ItemBackColor = System.Drawing.Color.White;
            this.bpc수출자.Location = new System.Drawing.Point(121, 128);
            this.bpc수출자.Name = "bpc수출자";
            this.bpc수출자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc수출자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc수출자.SearchCode = true;
            this.bpc수출자.SelectCount = 0;
            this.bpc수출자.SetDefaultValue = false;
            this.bpc수출자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc수출자.Size = new System.Drawing.Size(267, 21);
            this.bpc수출자.TabIndex = 10;
            this.bpc수출자.Tag = "CD_EXPORT";
            this.bpc수출자.Text = "bpCodeTextBox1";
            this.bpc수출자.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc수출자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc거래처
            // 
            this.bpc거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc거래처.ButtonImage")));
            this.bpc거래처.ChildMode = "";
            this.bpc거래처.CodeName = "";
            this.bpc거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc거래처.CodeValue = "";
            this.bpc거래처.ComboCheck = true;
            this.bpc거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc거래처.ItemBackColor = System.Drawing.Color.White;
            this.bpc거래처.Location = new System.Drawing.Point(510, 103);
            this.bpc거래처.Name = "bpc거래처";
            this.bpc거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc거래처.SearchCode = true;
            this.bpc거래처.SelectCount = 0;
            this.bpc거래처.SetDefaultValue = false;
            this.bpc거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc거래처.Size = new System.Drawing.Size(268, 21);
            this.bpc거래처.TabIndex = 9;
            this.bpc거래처.Tag = "CD_PARTNER";
            this.bpc거래처.Text = "bpCodeTextBox1";
            this.bpc거래처.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc거래처.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc영업그룹
            // 
            this.bpc영업그룹.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc영업그룹.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc영업그룹.ButtonImage")));
            this.bpc영업그룹.ChildMode = "";
            this.bpc영업그룹.CodeName = "";
            this.bpc영업그룹.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc영업그룹.CodeValue = "";
            this.bpc영업그룹.ComboCheck = true;
            this.bpc영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpc영업그룹.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc영업그룹.Location = new System.Drawing.Point(510, 28);
            this.bpc영업그룹.Name = "bpc영업그룹";
            this.bpc영업그룹.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc영업그룹.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc영업그룹.SearchCode = true;
            this.bpc영업그룹.SelectCount = 0;
            this.bpc영업그룹.SetDefaultValue = true;
            this.bpc영업그룹.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc영업그룹.Size = new System.Drawing.Size(268, 21);
            this.bpc영업그룹.TabIndex = 3;
            this.bpc영업그룹.Tag = "CD_SALEGRP";
            this.bpc영업그룹.Text = "bpCodeTextBox1";
            this.bpc영업그룹.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc영업그룹.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc담당자
            // 
            this.bpc담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc담당자.ButtonImage")));
            this.bpc담당자.ChildMode = "";
            this.bpc담당자.CodeName = "";
            this.bpc담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc담당자.CodeValue = "";
            this.bpc담당자.ComboCheck = true;
            this.bpc담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpc담당자.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc담당자.Location = new System.Drawing.Point(510, 53);
            this.bpc담당자.Name = "bpc담당자";
            this.bpc담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc담당자.SearchCode = true;
            this.bpc담당자.SelectCount = 0;
            this.bpc담당자.SetDefaultValue = true;
            this.bpc담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc담당자.Size = new System.Drawing.Size(268, 21);
            this.bpc담당자.TabIndex = 5;
            this.bpc담당자.Tag = "NO_EMP";
            this.bpc담당자.Text = "bpCodeTextBox1";
            this.bpc담당자.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpc담당자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // cbo선적조건
            // 
            this.cbo선적조건.AutoDropDown = true;
            this.cbo선적조건.BackColor = System.Drawing.Color.White;
            this.cbo선적조건.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo선적조건.Location = new System.Drawing.Point(121, 204);
            this.cbo선적조건.Name = "cbo선적조건";
            this.cbo선적조건.Size = new System.Drawing.Size(267, 20);
            this.cbo선적조건.TabIndex = 16;
            this.cbo선적조건.Tag = "COND_SHIPMENT";
            this.cbo선적조건.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo도착국
            // 
            this.cbo도착국.AutoDropDown = true;
            this.cbo도착국.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo도착국.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo도착국.Location = new System.Drawing.Point(121, 178);
            this.cbo도착국.Name = "cbo도착국";
            this.cbo도착국.Size = new System.Drawing.Size(267, 20);
            this.cbo도착국.TabIndex = 14;
            this.cbo도착국.Tag = "PORT_NATION";
            this.cbo도착국.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo선적구분
            // 
            this.cbo선적구분.AutoDropDown = true;
            this.cbo선적구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo선적구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo선적구분.Location = new System.Drawing.Point(121, 29);
            this.cbo선적구분.Name = "cbo선적구분";
            this.cbo선적구분.Size = new System.Drawing.Size(267, 20);
            this.cbo선적구분.TabIndex = 2;
            this.cbo선적구분.Tag = "FG_BL";
            this.cbo선적구분.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
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
            // cur외화금액
            // 
            this.cur외화금액.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cur외화금액.CurrencyDecimalDigits = 4;
            this.cur외화금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur외화금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur외화금액.Location = new System.Drawing.Point(510, 178);
            this.cur외화금액.Mask = null;
            this.cur외화금액.MaxLength = 22;
            this.cur외화금액.Name = "cur외화금액";
            this.cur외화금액.NullString = "0";
            this.cur외화금액.PositiveColor = System.Drawing.Color.Black;
            this.cur외화금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur외화금액.Size = new System.Drawing.Size(186, 21);
            this.cur외화금액.TabIndex = 15;
            this.cur외화금액.Tag = "AM_EX";
            this.cur외화금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur외화금액.UseKeyEnter = true;
            this.cur외화금액.UseKeyF3 = true;
            this.cur외화금액.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cur기표금액
            // 
            this.cur기표금액.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cur기표금액.CurrencyDecimalDigits = 4;
            this.cur기표금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur기표금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur기표금액.Location = new System.Drawing.Point(510, 203);
            this.cur기표금액.Mask = null;
            this.cur기표금액.MaxLength = 22;
            this.cur기표금액.Name = "cur기표금액";
            this.cur기표금액.NullString = "0";
            this.cur기표금액.PositiveColor = System.Drawing.Color.Black;
            this.cur기표금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur기표금액.Size = new System.Drawing.Size(186, 21);
            this.cur기표금액.TabIndex = 17;
            this.cur기표금액.Tag = "AM";
            this.cur기표금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur기표금액.UseKeyEnter = true;
            this.cur기표금액.UseKeyF3 = true;
            this.cur기표금액.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cur기표환율
            // 
            this.cur기표환율.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cur기표환율.CurrencyDecimalDigits = 4;
            this.cur기표환율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur기표환율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur기표환율.Location = new System.Drawing.Point(510, 153);
            this.cur기표환율.Mask = null;
            this.cur기표환율.MaxLength = 20;
            this.cur기표환율.Name = "cur기표환율";
            this.cur기표환율.NullString = "0";
            this.cur기표환율.PositiveColor = System.Drawing.Color.Black;
            this.cur기표환율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur기표환율.Size = new System.Drawing.Size(186, 21);
            this.cur기표환율.TabIndex = 13;
            this.cur기표환율.Tag = "RT_EXCH";
            this.cur기표환율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur기표환율.UseKeyEnter = true;
            this.cur기표환율.UseKeyF3 = true;
            this.cur기표환율.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt비고1
            // 
            this.txt비고1.BackColor = System.Drawing.Color.White;
            this.txt비고1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고1.Location = new System.Drawing.Point(120, 402);
            this.txt비고1.MaxLength = 100;
            this.txt비고1.Name = "txt비고1";
            this.txt비고1.SelectedAllEnabled = false;
            this.txt비고1.Size = new System.Drawing.Size(660, 21);
            this.txt비고1.TabIndex = 26;
            this.txt비고1.Tag = "REMARK1";
            this.txt비고1.UseKeyEnter = true;
            this.txt비고1.UseKeyF3 = true;
            this.txt비고1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cboLC구분
            // 
            this.cboLC구분.AutoDropDown = true;
            this.cboLC구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cboLC구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLC구분.Location = new System.Drawing.Point(121, 303);
            this.cboLC구분.Name = "cboLC구분";
            this.cboLC구분.Size = new System.Drawing.Size(267, 20);
            this.cboLC구분.TabIndex = 22;
            this.cboLC구분.Tag = "FG_LC";
            this.cboLC구분.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt송장번호
            // 
            this.txt송장번호.BackColor = System.Drawing.Color.White;
            this.txt송장번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt송장번호.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt송장번호.Location = new System.Drawing.Point(121, 277);
            this.txt송장번호.MaxLength = 20;
            this.txt송장번호.Name = "txt송장번호";
            this.txt송장번호.SelectedAllEnabled = false;
            this.txt송장번호.Size = new System.Drawing.Size(180, 21);
            this.txt송장번호.TabIndex = 21;
            this.txt송장번호.Tag = "NO_INV";
            this.txt송장번호.UseKeyEnter = true;
            this.txt송장번호.UseKeyF3 = true;
            this.txt송장번호.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
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
            // cbo결제형태
            // 
            this.cbo결제형태.AutoDropDown = true;
            this.cbo결제형태.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo결제형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo결제형태.Location = new System.Drawing.Point(121, 228);
            this.cbo결제형태.Name = "cbo결제형태";
            this.cbo결제형태.Size = new System.Drawing.Size(267, 20);
            this.cbo결제형태.TabIndex = 18;
            this.cbo결제형태.Tag = "COND_PAY";
            this.cbo결제형태.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
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
            // cbo통화
            // 
            this.cbo통화.AutoDropDown = true;
            this.cbo통화.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo통화.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo통화.Location = new System.Drawing.Point(121, 153);
            this.cbo통화.Name = "cbo통화";
            this.cbo통화.Size = new System.Drawing.Size(267, 20);
            this.cbo통화.TabIndex = 12;
            this.cbo통화.Tag = "CD_EXCH";
            this.cbo통화.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt선적번호
            // 
            this.txt선적번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.txt선적번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt선적번호.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt선적번호.Location = new System.Drawing.Point(121, 3);
            this.txt선적번호.MaxLength = 20;
            this.txt선적번호.Name = "txt선적번호";
            this.txt선적번호.SelectedAllEnabled = false;
            this.txt선적번호.Size = new System.Drawing.Size(150, 21);
            this.txt선적번호.TabIndex = 0;
            this.txt선적번호.Tag = "NO_BL";
            this.txt선적번호.UseKeyEnter = true;
            this.txt선적번호.UseKeyF3 = true;
            this.txt선적번호.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txtVESSEL명
            // 
            this.txtVESSEL명.BackColor = System.Drawing.Color.White;
            this.txtVESSEL명.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtVESSEL명.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVESSEL명.Location = new System.Drawing.Point(120, 328);
            this.txtVESSEL명.MaxLength = 50;
            this.txtVESSEL명.Name = "txtVESSEL명";
            this.txtVESSEL명.SelectedAllEnabled = false;
            this.txtVESSEL명.Size = new System.Drawing.Size(660, 21);
            this.txtVESSEL명.TabIndex = 23;
            this.txtVESSEL명.Tag = "NM_VESSEL";
            this.txtVESSEL명.UseKeyEnter = true;
            this.txtVESSEL명.UseKeyF3 = true;
            this.txtVESSEL명.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt비고3
            // 
            this.txt비고3.BackColor = System.Drawing.Color.White;
            this.txt비고3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고3.Location = new System.Drawing.Point(120, 451);
            this.txt비고3.MaxLength = 100;
            this.txt비고3.Name = "txt비고3";
            this.txt비고3.SelectedAllEnabled = false;
            this.txt비고3.Size = new System.Drawing.Size(660, 21);
            this.txt비고3.TabIndex = 28;
            this.txt비고3.Tag = "REMARK3";
            this.txt비고3.UseKeyEnter = true;
            this.txt비고3.UseKeyF3 = true;
            this.txt비고3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt비고2
            // 
            this.txt비고2.BackColor = System.Drawing.Color.White;
            this.txt비고2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고2.Location = new System.Drawing.Point(120, 427);
            this.txt비고2.MaxLength = 100;
            this.txt비고2.Name = "txt비고2";
            this.txt비고2.SelectedAllEnabled = false;
            this.txt비고2.Size = new System.Drawing.Size(660, 21);
            this.txt비고2.TabIndex = 27;
            this.txt비고2.Tag = "REMARK2";
            this.txt비고2.UseKeyEnter = true;
            this.txt비고2.UseKeyF3 = true;
            this.txt비고2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt선적지
            // 
            this.txt선적지.BackColor = System.Drawing.Color.White;
            this.txt선적지.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt선적지.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt선적지.Location = new System.Drawing.Point(120, 352);
            this.txt선적지.MaxLength = 50;
            this.txt선적지.Name = "txt선적지";
            this.txt선적지.SelectedAllEnabled = false;
            this.txt선적지.Size = new System.Drawing.Size(660, 21);
            this.txt선적지.TabIndex = 24;
            this.txt선적지.Tag = "PORT_LOADING";
            this.txt선적지.UseKeyEnter = true;
            this.txt선적지.UseKeyF3 = true;
            this.txt선적지.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt도착지
            // 
            this.txt도착지.BackColor = System.Drawing.Color.White;
            this.txt도착지.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt도착지.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt도착지.Location = new System.Drawing.Point(120, 377);
            this.txt도착지.MaxLength = 50;
            this.txt도착지.Name = "txt도착지";
            this.txt도착지.SelectedAllEnabled = false;
            this.txt도착지.Size = new System.Drawing.Size(660, 21);
            this.txt도착지.TabIndex = 25;
            this.txt도착지.Tag = "PORT_ARRIVER";
            this.txt도착지.UseKeyEnter = true;
            this.txt도착지.UseKeyF3 = true;
            this.txt도착지.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel12.Location = new System.Drawing.Point(116, 448);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(664, 1);
            this.panel12.TabIndex = 140;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel7.Location = new System.Drawing.Point(116, 424);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(664, 1);
            this.panel7.TabIndex = 139;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel8.Location = new System.Drawing.Point(5, 399);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(775, 1);
            this.panel8.TabIndex = 138;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
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
            this.panel6.Controls.Add(this.lbl거래처);
            this.panel6.Controls.Add(this.lbl외화금액);
            this.panel6.Controls.Add(this.lbl기표금액);
            this.panel6.Controls.Add(this.lbl기표환율);
            this.panel6.Controls.Add(this.lbl선사);
            this.panel6.Controls.Add(this.lbl선적일자);
            this.panel6.Controls.Add(this.lbl영업그룹);
            this.panel6.Controls.Add(this.lbl참조신고번호);
            this.panel6.Controls.Add(this.lbl담당자);
            this.panel6.Location = new System.Drawing.Point(391, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(115, 225);
            this.panel6.TabIndex = 4;
            // 
            // lbl거래처
            // 
            this.lbl거래처.Location = new System.Drawing.Point(3, 104);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Resizeble = true;
            this.lbl거래처.Size = new System.Drawing.Size(110, 18);
            this.lbl거래처.TabIndex = 13;
            this.lbl거래처.Tag = "CD_TRANS";
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl외화금액
            // 
            this.lbl외화금액.Location = new System.Drawing.Point(3, 180);
            this.lbl외화금액.Name = "lbl외화금액";
            this.lbl외화금액.Resizeble = true;
            this.lbl외화금액.Size = new System.Drawing.Size(110, 18);
            this.lbl외화금액.TabIndex = 12;
            this.lbl외화금액.Tag = "AMT_EX";
            this.lbl외화금액.Text = "외화금액";
            this.lbl외화금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl기표금액
            // 
            this.lbl기표금액.Location = new System.Drawing.Point(3, 205);
            this.lbl기표금액.Name = "lbl기표금액";
            this.lbl기표금액.Resizeble = true;
            this.lbl기표금액.Size = new System.Drawing.Size(110, 18);
            this.lbl기표금액.TabIndex = 6;
            this.lbl기표금액.Tag = "AM_ISSUE";
            this.lbl기표금액.Text = "기표금액";
            this.lbl기표금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl기표환율
            // 
            this.lbl기표환율.Location = new System.Drawing.Point(3, 154);
            this.lbl기표환율.Name = "lbl기표환율";
            this.lbl기표환율.Resizeble = true;
            this.lbl기표환율.Size = new System.Drawing.Size(110, 18);
            this.lbl기표환율.TabIndex = 5;
            this.lbl기표환율.Tag = "RATE_ISSUE";
            this.lbl기표환율.Text = "기표환율";
            this.lbl기표환율.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선사
            // 
            this.lbl선사.Location = new System.Drawing.Point(3, 129);
            this.lbl선사.Name = "lbl선사";
            this.lbl선사.Resizeble = true;
            this.lbl선사.Size = new System.Drawing.Size(110, 18);
            this.lbl선사.TabIndex = 4;
            this.lbl선사.Tag = "SHIP_CORP";
            this.lbl선사.Text = "선사";
            this.lbl선사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적일자
            // 
            this.lbl선적일자.Location = new System.Drawing.Point(3, 80);
            this.lbl선적일자.Name = "lbl선적일자";
            this.lbl선적일자.Resizeble = true;
            this.lbl선적일자.Size = new System.Drawing.Size(110, 18);
            this.lbl선적일자.TabIndex = 3;
            this.lbl선적일자.Tag = "DT_BL";
            this.lbl선적일자.Text = "선적일자";
            this.lbl선적일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl영업그룹
            // 
            this.lbl영업그룹.Location = new System.Drawing.Point(3, 30);
            this.lbl영업그룹.Name = "lbl영업그룹";
            this.lbl영업그룹.Resizeble = true;
            this.lbl영업그룹.Size = new System.Drawing.Size(110, 18);
            this.lbl영업그룹.TabIndex = 2;
            this.lbl영업그룹.Tag = "GROUP_ISUL";
            this.lbl영업그룹.Text = "영업그룹";
            this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl참조신고번호
            // 
            this.lbl참조신고번호.Location = new System.Drawing.Point(3, 4);
            this.lbl참조신고번호.Name = "lbl참조신고번호";
            this.lbl참조신고번호.Resizeble = true;
            this.lbl참조신고번호.Size = new System.Drawing.Size(110, 18);
            this.lbl참조신고번호.TabIndex = 1;
            this.lbl참조신고번호.Tag = "TO_REFER";
            this.lbl참조신고번호.Text = "참조신고번호";
            this.lbl참조신고번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl담당자
            // 
            this.lbl담당자.Location = new System.Drawing.Point(3, 55);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Resizeble = true;
            this.lbl담당자.Size = new System.Drawing.Size(110, 18);
            this.lbl담당자.TabIndex = 1;
            this.lbl담당자.Tag = "NM_EMP";
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lbl수출자);
            this.panel5.Controls.Add(this.lbl도착국);
            this.panel5.Controls.Add(this.lbl결제형태);
            this.panel5.Controls.Add(this.lbl비고);
            this.panel5.Controls.Add(this.lbl도착지);
            this.panel5.Controls.Add(this.lbl선적지);
            this.panel5.Controls.Add(this.lblVESSEL명);
            this.panel5.Controls.Add(this.lbl송장번호);
            this.panel5.Controls.Add(this.lbl선적조건);
            this.panel5.Controls.Add(this.lbl통화);
            this.panel5.Controls.Add(this.lbl도착예정일자);
            this.panel5.Controls.Add(this.lbl기표일자);
            this.panel5.Controls.Add(this.lbl사업장);
            this.panel5.Controls.Add(this.lbl선적번호);
            this.panel5.Controls.Add(this.lbl선적구분);
            this.panel5.Controls.Add(this.lbl결제만기일);
            this.panel5.Controls.Add(this.lblLC구분);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(115, 472);
            this.panel5.TabIndex = 0;
            // 
            // lbl수출자
            // 
            this.lbl수출자.Location = new System.Drawing.Point(3, 129);
            this.lbl수출자.Name = "lbl수출자";
            this.lbl수출자.Resizeble = true;
            this.lbl수출자.Size = new System.Drawing.Size(110, 18);
            this.lbl수출자.TabIndex = 22;
            this.lbl수출자.Text = "수출자";
            this.lbl수출자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl도착국
            // 
            this.lbl도착국.Location = new System.Drawing.Point(3, 180);
            this.lbl도착국.Name = "lbl도착국";
            this.lbl도착국.Resizeble = true;
            this.lbl도착국.Size = new System.Drawing.Size(110, 18);
            this.lbl도착국.TabIndex = 21;
            this.lbl도착국.Text = "도착국";
            this.lbl도착국.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl결제형태
            // 
            this.lbl결제형태.Location = new System.Drawing.Point(3, 229);
            this.lbl결제형태.Name = "lbl결제형태";
            this.lbl결제형태.Resizeble = true;
            this.lbl결제형태.Size = new System.Drawing.Size(110, 18);
            this.lbl결제형태.TabIndex = 17;
            this.lbl결제형태.Tag = "COND_PAY";
            this.lbl결제형태.Text = "결제형태";
            this.lbl결제형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl비고
            // 
            this.lbl비고.Location = new System.Drawing.Point(3, 402);
            this.lbl비고.Name = "lbl비고";
            this.lbl비고.Resizeble = true;
            this.lbl비고.Size = new System.Drawing.Size(110, 18);
            this.lbl비고.TabIndex = 13;
            this.lbl비고.Tag = "REMARK";
            this.lbl비고.Text = "비고";
            this.lbl비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl도착지
            // 
            this.lbl도착지.Location = new System.Drawing.Point(3, 379);
            this.lbl도착지.Name = "lbl도착지";
            this.lbl도착지.Resizeble = true;
            this.lbl도착지.Size = new System.Drawing.Size(110, 18);
            this.lbl도착지.TabIndex = 11;
            this.lbl도착지.Tag = "ARRIVAL_PORT";
            this.lbl도착지.Text = "도착지";
            this.lbl도착지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적지
            // 
            this.lbl선적지.Location = new System.Drawing.Point(3, 353);
            this.lbl선적지.Name = "lbl선적지";
            this.lbl선적지.Resizeble = true;
            this.lbl선적지.Size = new System.Drawing.Size(110, 18);
            this.lbl선적지.TabIndex = 10;
            this.lbl선적지.Tag = "SHIP_PORT";
            this.lbl선적지.Text = "선적지";
            this.lbl선적지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVESSEL명
            // 
            this.lblVESSEL명.Location = new System.Drawing.Point(3, 329);
            this.lblVESSEL명.Name = "lblVESSEL명";
            this.lblVESSEL명.Resizeble = true;
            this.lblVESSEL명.Size = new System.Drawing.Size(110, 18);
            this.lblVESSEL명.TabIndex = 9;
            this.lblVESSEL명.Tag = "VESSEL";
            this.lblVESSEL명.Text = "VESSEL명";
            this.lblVESSEL명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl송장번호
            // 
            this.lbl송장번호.Location = new System.Drawing.Point(3, 279);
            this.lbl송장번호.Name = "lbl송장번호";
            this.lbl송장번호.Resizeble = true;
            this.lbl송장번호.Size = new System.Drawing.Size(110, 18);
            this.lbl송장번호.TabIndex = 8;
            this.lbl송장번호.Tag = "NO_INV";
            this.lbl송장번호.Text = "송장번호";
            this.lbl송장번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적조건
            // 
            this.lbl선적조건.Location = new System.Drawing.Point(3, 204);
            this.lbl선적조건.Name = "lbl선적조건";
            this.lbl선적조건.Resizeble = true;
            this.lbl선적조건.Size = new System.Drawing.Size(110, 18);
            this.lbl선적조건.TabIndex = 6;
            this.lbl선적조건.Tag = "COND_SHIP";
            this.lbl선적조건.Text = "선적조건";
            this.lbl선적조건.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl통화
            // 
            this.lbl통화.Location = new System.Drawing.Point(3, 155);
            this.lbl통화.Name = "lbl통화";
            this.lbl통화.Resizeble = true;
            this.lbl통화.Size = new System.Drawing.Size(110, 18);
            this.lbl통화.TabIndex = 5;
            this.lbl통화.Tag = "";
            this.lbl통화.Text = "통화";
            this.lbl통화.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl도착예정일자
            // 
            this.lbl도착예정일자.Location = new System.Drawing.Point(3, 104);
            this.lbl도착예정일자.Name = "lbl도착예정일자";
            this.lbl도착예정일자.Resizeble = true;
            this.lbl도착예정일자.Size = new System.Drawing.Size(110, 18);
            this.lbl도착예정일자.TabIndex = 4;
            this.lbl도착예정일자.Text = "도착예정일자";
            this.lbl도착예정일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl기표일자
            // 
            this.lbl기표일자.Location = new System.Drawing.Point(3, 79);
            this.lbl기표일자.Name = "lbl기표일자";
            this.lbl기표일자.Resizeble = true;
            this.lbl기표일자.Size = new System.Drawing.Size(110, 18);
            this.lbl기표일자.TabIndex = 3;
            this.lbl기표일자.Text = "기표일자";
            this.lbl기표일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl사업장
            // 
            this.lbl사업장.Location = new System.Drawing.Point(3, 55);
            this.lbl사업장.Name = "lbl사업장";
            this.lbl사업장.Resizeble = true;
            this.lbl사업장.Size = new System.Drawing.Size(110, 18);
            this.lbl사업장.TabIndex = 2;
            this.lbl사업장.Tag = "BALLOT";
            this.lbl사업장.Text = "사업장";
            this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적번호
            // 
            this.lbl선적번호.Location = new System.Drawing.Point(3, 4);
            this.lbl선적번호.Name = "lbl선적번호";
            this.lbl선적번호.Resizeble = true;
            this.lbl선적번호.Size = new System.Drawing.Size(110, 18);
            this.lbl선적번호.TabIndex = 0;
            this.lbl선적번호.Tag = "NO_BL";
            this.lbl선적번호.Text = "선적번호";
            this.lbl선적번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적구분
            // 
            this.lbl선적구분.Location = new System.Drawing.Point(3, 30);
            this.lbl선적구분.Name = "lbl선적구분";
            this.lbl선적구분.Resizeble = true;
            this.lbl선적구분.Size = new System.Drawing.Size(110, 18);
            this.lbl선적구분.TabIndex = 0;
            this.lbl선적구분.Tag = "FG_BL";
            this.lbl선적구분.Text = "선적구분";
            this.lbl선적구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl결제만기일
            // 
            this.lbl결제만기일.Location = new System.Drawing.Point(3, 254);
            this.lbl결제만기일.Name = "lbl결제만기일";
            this.lbl결제만기일.Resizeble = true;
            this.lbl결제만기일.Size = new System.Drawing.Size(110, 18);
            this.lbl결제만기일.TabIndex = 7;
            this.lbl결제만기일.Tag = "PAY_LIMITDAY";
            this.lbl결제만기일.Text = "결제만기일";
            this.lbl결제만기일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLC구분
            // 
            this.lblLC구분.Location = new System.Drawing.Point(3, 304);
            this.lblLC구분.Name = "lblLC구분";
            this.lblLC구분.Resizeble = true;
            this.lblLC구분.Size = new System.Drawing.Size(110, 18);
            this.lblLC구분.TabIndex = 8;
            this.lblLC구분.Tag = "FG_LC";
            this.lblLC구분.Text = "L/C구분";
            this.lblLC구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn매출전표처리
            // 
            this.btn매출전표처리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn매출전표처리.BackColor = System.Drawing.Color.White;
            this.btn매출전표처리.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn매출전표처리.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn매출전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn매출전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn매출전표처리.Location = new System.Drawing.Point(441, 3);
            this.btn매출전표처리.Name = "btn매출전표처리";
            this.btn매출전표처리.Size = new System.Drawing.Size(120, 24);
            this.btn매출전표처리.TabIndex = 134;
            this.btn매출전표처리.TabStop = false;
            this.btn매출전표처리.Text = "매출전표처리";
            this.btn매출전표처리.UseVisualStyleBackColor = false;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn매출전표처리);
            this.panelExt1.Controls.Add(this.btn선적내역);
            this.panelExt1.Controls.Add(this.btn판매경비등록);
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
            this.TitleText = "선적등록";
            this.Load += new System.EventHandler(this.P_TR_EXBL_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_TR_EXBL_Paint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp결제만기일)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp도착예정일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp선적일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기표일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDAYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur외화금액)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur기표금액)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur기표환율)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region ● 초기화 부분 - 사용자 정의

        private System.Windows.Forms.BindingManagerBase m_Manager;

        private string m_Today;
        private DataTable m_HeadTable;
        private DataTable m_CopyTable;

        private bool m_IsPageActivated = false;

    
    #endregion
    
        #region ● 초기화 부분


        /// <summary>
        /// 생성자
        /// </summary>
        public P_TR_EXBL_BAK()
        {
            try
            {
                InitializeComponent();

                this.txtDAYS.NumberFormatInfoObject.NumberDecimalDigits = 0;
                this.txtDAYS.DecimalValue = 0;

                this.cur기표환율.NumberFormatInfoObject.NumberDecimalDigits = 4;
                this.cur외화금액.NumberFormatInfoObject.NumberDecimalDigits = 4;
                this.cur기표금액.NumberFormatInfoObject.NumberDecimalDigits = 4;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 폼 로딩시 DD셋팅을 한다.
        /// 검색어로 검색을 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void P_TR_EXBL_Load(object sender, EventArgs e)
        {
            Show();

            try
            {
                // DD명 초기화
                this.InitDD();


                // 날짜 초기화
                this.InitDate();


                // 초기 테이블 생성
                SelectInit();

                // Control 비 활성화
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


                // 지불조건 콤보 박스 초기화
                this.InitComboBox();

                // 초기 테이블 추가 및 바인딩
                this.Append();

                // 버튼 설정
                this.SetButton();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        /// <summary>
        /// 버튼 초기화
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
        /// DD 설정
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

            btn매출전표처리.Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)btn매출전표처리.Tag);
            btn판매경비등록.Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)btn판매경비등록.Tag);
            btn선적내역.Text = GetDataDictionaryItem(DataDictionaryTypes.TRE, (string)btn선적내역.Tag);
        }

        /// <summary>
        /// 검색 날짜 텍스트 박스 초기화
        /// </summary>
        private void InitDate()
        {
            string ls_day = this.MainFrameInterface.GetStringToday;

            this.dtp기표일자.Text = ls_day;
            this.dtp선적일자.Text = ls_day;
            this.dtp도착예정일자.Text = ls_day;
            this.dtp결제만기일.Text = ls_day;

            this.m_Today = ls_day;

        }

        /// <summary>
        /// 각 ComboBox를 셋팅한다.
        /// </summary>
        private void InitComboBox()
        {
            //	* 셋팅할 Type 지정
            //	1. N : 공백없는 내용
            //	2. S : 공백있는 내용
            //	3. U : 사용자 정의

            string[] ls_args = new string[6];
            ls_args[0] = "N;TR_IM00016";// 선적구분
            ls_args[1] = "N;MA_B000005";// 통화
            ls_args[2] = "N;MA_B000020";// 도착국
            ls_args[3] = "N;TR_IM00004";// 결재형태
            ls_args[4] = "N;TR_IM00005";// L/C 구분
            ls_args[5] = "S;TR_IM00003";// 선적조건

            DataSet lds_Combo = (DataSet)GetComboData(ls_args);

            // 선적구분
            this.cbo선적구분.DataSource = lds_Combo.Tables[0];
            this.cbo선적구분.DisplayMember = "NAME";
            this.cbo선적구분.ValueMember = "CODE";

            // 통화
            this.cbo통화.DataSource = lds_Combo.Tables[1];
            this.cbo통화.DisplayMember = "NAME";
            this.cbo통화.ValueMember = "CODE";

            // 도착국
            this.cbo도착국.DataSource = lds_Combo.Tables[2];
            this.cbo도착국.DisplayMember = "NAME";
            this.cbo도착국.ValueMember = "CODE";

            // 결재형태
            this.cbo결제형태.DataSource = lds_Combo.Tables[3];
            this.cbo결제형태.DisplayMember = "NAME";
            this.cbo결제형태.ValueMember = "CODE";

            // L/C 구분
            this.cboLC구분.DataSource = lds_Combo.Tables[4];
            this.cboLC구분.DisplayMember = "NAME";
            this.cboLC구분.ValueMember = "CODE";

            // 선적 조건
            this.cbo선적조건.DataSource = lds_Combo.Tables[5];
            this.cbo선적조건.DisplayMember = "NAME";
            this.cbo선적조건.ValueMember = "CODE";

        }


        #endregion

        #region ● 일반 메소드

        /// <summary>
        /// 받아온 Panel 안의 Control들의 상태를 설정해 준다.(비활성으로 설정한다.)
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

            //this.btn참조신고번호.Enabled = true;

        }

        /// <summary>
        /// Panel 안이 컨트롤을 활성으로 한다.
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
        /// 콤보박스를 처음 상태로 돌린다.
        /// </summary>
        private void ComboResetting()
        {
            this.cbo선적구분.SelectedIndex = 0;
            this.cbo통화.SelectedIndex = 0;
            this.cbo도착국.SelectedIndex = 0;
            this.cbo결제형태.SelectedIndex = 0;
            this.cboLC구분.SelectedIndex = 0;
            this.cbo선적조건.SelectedIndex = 0;
        }

        #endregion

        #region ● 베이스 폼 override Method


        /// <summary>
        /// 조회 버튼
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
                    // 변경된 사항이 있습니다. 저장하시겠습니까?
                    // MA_M000073
                    string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
                    DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        // 이전 데이터를 저장한 후 새로운 데이터를 추가할 수 있게 한다.
                        if (this.Save())
                        {
                            // 정상 저장
                        }
                        else
                        {
                            // 저장시 에러 발생
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

                    // 코드, 명 변경을 위한 데이터 설정

                    // 거래처 코드 & 명칭 변수
                    this.bpc거래처.SetCodeValue(this.m_HeadTable.Rows[0]["CD_PARTNER"].ToString());
                    this.bpc거래처.SetCodeName(this.m_HeadTable.Rows[0]["NM_PARTNER"].ToString());

                    // 영업그룹 코드 & 명칭 변수
                    this.bpc영업그룹.SetCodeValue(this.m_HeadTable.Rows[0]["CD_SALEGRP"].ToString());
                    this.bpc영업그룹.SetCodeName(this.m_HeadTable.Rows[0]["NM_SALEGRP"].ToString());

                    // 담당자 코드 & 명칭 변수
                    this.bpc담당자.SetCodeValue(this.m_HeadTable.Rows[0]["NO_EMP"].ToString());
                    this.bpc담당자.SetCodeName(this.m_HeadTable.Rows[0]["NM_KOR"].ToString());

                    // 사업장 코드 & 명칭 변수
                    this.bpc사업장.SetCodeValue(this.m_HeadTable.Rows[0]["CD_BIZAREA"].ToString());
                    this.bpc사업장.SetCodeName(m_HeadTable.Rows[0]["NM_BIZAREA"].ToString());

                    // 수출자 코드 & 명칭 변수
                    this.bpc수출자.SetCodeValue(this.m_HeadTable.Rows[0]["CD_EXPORT"].ToString());
                    this.bpc수출자.SetCodeName(this.m_HeadTable.Rows[0]["NM_EXPORT"].ToString());

                    // 선사 코드 & 명칭 변수
                    this.bpc선사.SetCodeValue(this.m_HeadTable.Rows[0]["SHIP_CORP"].ToString());
                    this.bpc선사.SetCodeName(this.m_HeadTable.Rows[0]["NM_SHIP_CORP"].ToString());

                    this.m_IsPageActivated = true;
                    this.SetControlEnable();

                    this.m_HeadTable.AcceptChanges();

                    this.txt선적번호.Enabled = false;


                }
                obj.Dispose();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 추가 버튼
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
                    // 변경된 사항이 있습니다. 저장하시겠습니까?
                    // MA_M000073
                    //string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
                    DialogResult result = ShowMessage("QY2_001");

                    if (result == DialogResult.Yes)
                    {
                        // 이전 데이터를 저장한 후 새로운 데이터를 추가할 수 있게 한다.
                        if (this.Save())
                        {
                            // 정상 저장
                        }
                        else
                        {
                            // 저장시 에러 발생
                            return;
                        }
                    }
                }

                // 추가할수 있도록 새로운 Row추가
                this.Append();

                // Control 비 활성화
                this.SetControlDisable();

                this.txt선적번호.Enabled = true;
                this.txt선적번호.ReadOnly = false;
                this.txt선적번호.Focus();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 저장 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.m_IsPageActivated)
                    return;

                this.txt선적번호.Focus();

                this.m_Manager.Position = 0;

                string msg = null;

                DataTable ldt_temp = this.m_HeadTable.GetChanges();

                if (ldt_temp == null)
                {
                    // MA_M000017 ("변경된 데이터가 없습니다)
                    ShowMessage("IK1_013");
                    //msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000017");
                    //Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                this.Save();

                this.txt선적번호.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 삭제 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            string msg = null;

            if (!this.m_IsPageActivated)
                return;

            DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Added);

            // 삭제 할 수 없는 부분(추가된 데이터가 있다는 것은 새로운 데이터이므로 DB에는 없다는 말
            if (ldt_temp != null)
                return;

            try
            {

                // MA_M000016
                // 정말로 삭제 하겠습니까?
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
                            // 정상적으로 삭제 되었습니다.
                            ShowMessage("TR_M000033");
                            //msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000033");
                            //MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.m_HeadTable.AcceptChanges();
                            this.Append();

                            // Control 비 활성화
                            this.SetControlDisable();

                            this.txt선적번호.Enabled = true;
                            this.txt선적번호.ReadOnly = false;
                            this.txt선적번호.Focus();
                        }
                
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        /// <summary>
        /// 출력 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {

        }

        ///// <summary>
        ///// 종료 버튼
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        //{
        //    if (this.m_HeadTable == null || this.m_HeadTable.Rows.Count < 1)
        //        return true;

        //    DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
        //    if (ldt_temp == null) return true;

        //    // 데이터가 처음 로딩된 경우라든지 새로 추가된 경우 LC번호의 유무를 판단하여 저장할 것인지 물어본다.
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
        //                    // 자료를 저장하였습니다.
        //                    this.ShowMessage("IK1_001");
        //                    return true;
        //                }
        //                else
        //                {
        //                    //저장실패
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

        #region -> 종료버튼클릭

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {

            try
            {
                //변경사항 없을경우 종료
                if (this.m_HeadTable == null || this.m_HeadTable.Rows.Count < 1)
                    return true;

                DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
                if (ldt_temp == null) return true;


                // 변경된 사항이 있습니다. 저장하시겠습니까?
                DialogResult result = ShowMessage("MA_M000073", "IY3");

                switch (result)
                {
                    case DialogResult.Yes:

                        if (this.Save())
                        {
                            // 자료를 저장하였습니다.
                            this.ShowMessage("IK1_001");
                            return true;
                        }
                        else
                        {
                            //저장실패
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

        #region ● 초기 테이블 초기화 부분

        /// <summary>
        /// DataTable 생성
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
        /// 초기 바인딩 메니저 설정
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

            this.txt선적번호.DataBindings.Add("Text", this.m_HeadTable, "NO_BL");
            this.cbo선적구분.DataBindings.Add("SelectedValue", this.m_HeadTable, "FG_BL");
            //this.txt참조신고번호.DataBindings.Add("Text", this.m_HeadTable, "NO_TO");

            this.dtp기표일자.DataBindings.Add("Text", this.m_HeadTable, "DT_BALLOT");
            this.dtp선적일자.DataBindings.Add("Text", this.m_HeadTable, "DT_LOADING");
            this.dtp도착예정일자.DataBindings.Add("Text", this.m_HeadTable, "DT_ARRIVAL");

            this.cbo통화.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_EXCH");
            this.cur기표환율.DataBindings.Add("Text", this.m_HeadTable, "RT_EXCH");
            this.cur외화금액.DataBindings.Add("Text", this.m_HeadTable, "AM_EX");

            this.cbo도착국.DataBindings.Add("SelectedValue", this.m_HeadTable, "PORT_NATION");
            this.cbo선적조건.DataBindings.Add("SelectedValue", this.m_HeadTable, "COND_SHIPMENT");
            this.cur기표금액.DataBindings.Add("Text", this.m_HeadTable, "AM");
            this.cbo결제형태.DataBindings.Add("SelectedValue", this.m_HeadTable, "COND_PAY");
            this.txtDAYS.DataBindings.Add("Text", this.m_HeadTable, "COND_DAYS");
            this.dtp결제만기일.DataBindings.Add("Text", this.m_HeadTable, "DT_PAYABLE");

            this.txt송장번호.DataBindings.Add("Text", this.m_HeadTable, "NO_INV");
            this.cboLC구분.DataBindings.Add("SelectedValue", this.m_HeadTable, "FG_LC");
            this.txtVESSEL명.DataBindings.Add("Text", this.m_HeadTable, "NM_VESSEL");
            this.txt선적지.DataBindings.Add("Text", this.m_HeadTable, "PORT_LOADING");
            this.txt도착지.DataBindings.Add("Text", this.m_HeadTable, "PORT_ARRIVER");

            this.txt비고1.DataBindings.Add("Text", this.m_HeadTable, "REMARK1");
            this.txt비고2.DataBindings.Add("Text", this.m_HeadTable, "REMARK2");
            this.txt비고3.DataBindings.Add("Text", this.m_HeadTable, "REMARK3");

            this.m_Manager.Position = 0;
        }

        #endregion

        #region ●추가 부분

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

            // B/L 번호 포커스 이동 및 입력할 수 있게 한다.
            this.txt선적번호.ReadOnly = false;
            this.txt선적번호.Focus();


            // 코드, 명 변경을 위한 데이터 설정
            // 거래처 코드 & 명칭 변수
            this.bpc거래처.SetCodeValue("");
            this.bpc거래처.SetCodeName("");

            // 영업그룹 코드 & 명칭 변수
            this.bpc영업그룹.SetCodeValue("");
            this.bpc영업그룹.SetCodeName("");

            // 수출자 코드 & 명칭 변수
            this.bpc수출자.SetCodeValue("");
            this.bpc수출자.SetCodeName("");

            // 선사 코드 & 명칭 변수
            this.bpc선사.SetCodeValue("");
            this.bpc선사.SetCodeName("");

        }

        #endregion

        #region ● 저장 부분

        /// <summary>
        /// 저장
        /// </summary>
        /// <returns></returns>
         private bool Save()
         {
             try
             {

                 this.txt송장번호.Focus();

                 if (m_HeadTable == null) return true;
                 if (m_HeadTable.Rows.Count == 0) return true;

                 // 필수 입력 값 체크
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
                     // 자료를 저장하였습니다.
                     this.ShowMessage("CM_M000001");
                     m_HeadTable.AcceptChanges();
                     // 한번 저장하면 BL번호를 수정할 수 없게 한다.
                     this.txt선적번호.ReadOnly = true;
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

        #region ●저장전 작업

        /// <summary>
        /// 필수 입력사항 체크
        /// </summary>
        /// <returns></returns>
        private bool CheckRequiredValue()
        {
            //선적번호
            if (txt선적번호.Text.Trim() == "")
            {
                //선적번호 + 은(는) 필수 입력 항목입니다.
                ShowMessage("WK1_004", this.lbl선적번호.Text);
                txt선적번호.Focus();
                return false;
            }
            ////신고번호
            //if (txt참조신고번호.Text.Trim() == "")
            //{
            //    //신고번호 + 은(는) 필수 입력 항목입니다.
            //    ShowMessage("WK1_004", this.lbl참조신고번호.Text);
            //    txt참조신고번호.Focus();
            //    return false;
            //}
            //영업그룹
            if (bpc영업그룹.IsEmpty())
            {
                //영업그룹 + 은(는) 필수 입력 항목입니다.
                ShowMessage("WK1_004", this.lbl영업그룹.Text);
                bpc영업그룹.Focus();
                return false;
            }
            //사업장
            if (bpc사업장.IsEmpty())
            {
                //사업장 + 은(는) 필수 입력 항목입니다.
                ShowMessage("WK1_004", this.lbl사업장.Text);
                bpc사업장.Focus();
                return false;
            }
            //담당자
            if (bpc담당자.IsEmpty())
            {
                //담당자 + 은(는) 필수 입력 항목입니다.
                ShowMessage("WK1_004", this.lbl담당자.Text);
                bpc담당자.Focus();
                return false;
            }
            //기표일자
            if (dtp기표일자.Text.Trim() == "")
            {
                //기표일자 + 은(는) 필수 입력 항목입니다.
                ShowMessage("WK1_004", this.lbl기표일자.Text);
                dtp기표일자.Focus();
                return false;
            }
            //선적일자
            if (dtp선적일자.Text.Trim() == "")
            {
                //선적일자 + 은(는) 필수 입력 항목입니다.
                ShowMessage("WK1_004", this.lbl선적일자.Text);
                dtp선적일자.Focus();
                return false;
            }
            //기표환율
            if (cur기표환율.DecimalValue == 0)
            {
                ShowMessage("WK1_004",lbl기표환율.Text);
                cur기표환율.Focus();
                return false;
            }
            //외화금액
            if (cur외화금액.DecimalValue == 0)
            {
                ShowMessage("WK1_004",lbl외화금액.Text);
                cur외화금액.Focus();
                return false;
            }
            //기표금액
            if (cur기표금액.DecimalValue == 0)
            {
                ShowMessage("WK1_004",lbl기표금액.Text);
                cur기표금액.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region ● 참조 신고 번호 부분

        /// <summary>
        /// 통관 도움창에서 가져온 데이터를 HeadTable에 넣어 준다.
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

            // 코드, 명 변경을 위한 데이터 설정

            // 거래처 코드 & 명칭 변수
            this.bpc거래처.SetCodeValue(pdr_row["CD_PARTNER"].ToString());
            this.bpc거래처.SetCodeName(pdr_row["LN_PARTNER"].ToString());

            // 영업그룹 코드 & 명칭 변수
            this.bpc영업그룹.SetCodeValue(pdr_row["CD_SALEGRP"].ToString());
            this.bpc영업그룹.SetCodeName(pdr_row["NM_SALEGRP"].ToString());

            // 담당자 코드 & 명칭 변수
            this.bpc담당자.SetCodeValue(pdr_row["NO_EMP"].ToString());
            this.bpc담당자.SetCodeName(pdr_row["NM_KOR"].ToString());

            // 사업장 코드 & 명칭 변수
            this.bpc사업장.SetCodeValue(pdr_row["CD_BIZAREA"].ToString());
            this.bpc사업장.SetCodeName(pdr_row["NM_BIZAREA"].ToString());

            // 수출자 코드 & 명칭 변수
            this.bpc수출자.SetCodeValue(pdr_row["CD_EXPORT"].ToString());
            this.bpc수출자.SetCodeName(pdr_row["NM_EXPORT"].ToString());

            //this.txt참조신고번호.Text = pdr_row["NO_TO"].ToString();
        }



        #endregion

        #region ● 선적 내역 도움창 부분

        /// <summary>
        /// 선적 내역 도움창 
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

                trade.P_TR_EXBL_SUB obj = new trade.P_TR_EXBL_SUB(this.MainFrameInterface, this.m_HeadTable, cbo선적구분.Text, cbo통화.Text);

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

        #region ● 판매 경비 부분

        /// <summary>
        /// 판매 경비 도움창
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
                    // 작업하신 자료를 먼저 저장하셔야 합니다. 계속하시겠습니까?"
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

                // 경비발생구분, 관리번호, 기표일자, 기표사업장코드, 기표사업장명, 
                // 부서코드, 부서명, 담당자코드 ,담당자명, C/C 코드, C/C명
                string[] ls_args = new string[11];
                ls_args[0] = "선적";
                ls_args[1] = this.m_HeadTable.Rows[0]["NO_BL"].ToString();
                ls_args[2] = this.dtp기표일자.Text;	// 기표일자
                ls_args[3] = bpc사업장.CodeValue.ToString();
                ls_args[4] = this.bpc사업장.CodeName.ToString();
                ls_args[5] = "";
                ls_args[6] = "";
                ls_args[7] = this.bpc담당자.CodeValue.ToString();;
                ls_args[8] = this.bpc담당자.CodeName.ToString(); ;
                ls_args[9] = "";
                ls_args[10] = "";

                //public P_TR_EXCOST(string[] p_args, DataTable p_originTable)

                object[] args = new Object[3];
                args[0] = ls_args;
                args[1] = this.m_HeadTable;
                args[2] = 4;	// 송장 : 1 , L/C : 1, 3 : 통관, 4 : 선적

                // Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
                if (this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
                {
                    //- 특정 페이지 닫기
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

        #region ●도움창 부분


        /// <summary>
        /// 도움창을 연다.
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
                    if (box.Name == "m_btnToRefer")	// 참조 신고 번호
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

   

        #region ● 키 이벤트 처리 부분


        /// <summary>
        /// 콤보 박스 공통 키 이벤트 처리
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
        /// 공용 키 이벤트 처리
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
                        this.txt선적번호.Focus();
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