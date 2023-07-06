//********************************************************************
// 작   성   자 : 
// 작   성   일 : 2006-06-12
// 모   듈   명 : 무역
// 시 스  템 명 : 무역관리
// 서브시스템명 : 수출관리
// 페 이 지  명 : 송장등록
// 프로젝트  명 : P_TR_EXINV
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
		#region ★ 자동 생성 변수

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
        private Duzon.Common.Controls.DropDownComboBox cbo거래구분;
		private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel11;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel12;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.TextBoxExt txt인도조건;
		private Duzon.Common.Controls.TextBoxExt txt최종목적지;
		private Duzon.Common.Controls.TextBoxExt txt선적지;
		private Duzon.Common.Controls.TextBoxExt txt종료CT번호;
        private Duzon.Common.Controls.DropDownComboBox cbo중량단위;
        private Duzon.Common.Controls.DropDownComboBox cbo운송방법;
        private Duzon.Common.Controls.DropDownComboBox cbo통화;
        private Duzon.Common.Controls.TextBoxExt txt비고1;
		private Duzon.Common.Controls.TextBoxExt txt비고5;
		private Duzon.Common.Controls.TextBoxExt txt비고4;
		private Duzon.Common.Controls.TextBoxExt txt비고2;
        private Duzon.Common.Controls.TextBoxExt txt비고3;
        private Duzon.Common.Controls.TextBoxExt txt도착지;
		private Duzon.Common.Controls.TextBoxExt txtVESSEL명;
        private Duzon.Common.Controls.TextBoxExt txt시작CT번호;
        private Duzon.Common.Controls.DropDownComboBox cbo포장형태;
        private Duzon.Common.Controls.DropDownComboBox cbo운송형태;
		private Duzon.Common.Controls.LabelExt lbl인도조건;
		private Duzon.Common.Controls.LabelExt lbl최종목적지;
		private Duzon.Common.Controls.LabelExt lbl선적지;
		private Duzon.Common.Controls.LabelExt lbl외화금액;
		private Duzon.Common.Controls.LabelExt lbl종료CT번호;
		private Duzon.Common.Controls.LabelExt lbl순중량;
		private Duzon.Common.Controls.LabelExt lbl중량단위;
		private Duzon.Common.Controls.LabelExt lbl운송방법;
		private Duzon.Common.Controls.LabelExt lbl통관예정일;
		private Duzon.Common.Controls.LabelExt lbl선적예정일;
		private Duzon.Common.Controls.LabelExt lbl대행자;
		private Duzon.Common.Controls.LabelExt lbl수출자;
		private Duzon.Common.Controls.LabelExt lbl사업장;
		private Duzon.Common.Controls.LabelExt lbl참조SO번호;
		private Duzon.Common.Controls.LabelExt lbl거래구분;
		private Duzon.Common.Controls.LabelExt lbl발행일자;
		private Duzon.Common.Controls.TextBoxExt txt송장번호;
		private Duzon.Common.Controls.LabelExt lbl통화;
		private Duzon.Common.Controls.LabelExt lbl비고;
		private Duzon.Common.Controls.LabelExt lbl선사;
		private Duzon.Common.Controls.LabelExt lblVESSEL명;
		private Duzon.Common.Controls.LabelExt lbl도착지;
		private Duzon.Common.Controls.LabelExt lbl착하통지처;
		private Duzon.Common.Controls.LabelExt lbl시작CT번호;
		private Duzon.Common.Controls.LabelExt lbl총중량;
		private Duzon.Common.Controls.LabelExt lbl포장형태;
		private Duzon.Common.Controls.LabelExt lbl운송형태;
		private Duzon.Common.Controls.LabelExt lbl원산지;
		private Duzon.Common.Controls.LabelExt lbl제조자;
		private Duzon.Common.Controls.LabelExt lbl담당자;
		private Duzon.Common.Controls.LabelExt lbl영업그룹;
		private Duzon.Common.Controls.LabelExt lbl거래처;
		private Duzon.Common.Controls.LabelExt lbl참조신용장번호;
        private Duzon.Common.Controls.LabelExt lbl송장번호;
        private Duzon.Common.Controls.DropDownComboBox cbo원산지;
		private Duzon.Common.Controls.CurrencyTextBox cur외화금액;
		private Duzon.Common.Controls.RoundedButton btn내역등록;
		private Duzon.Common.Controls.RoundedButton btn판매경비등록;
		private Duzon.Common.Controls.CurrencyTextBox cur총중량;
        private Duzon.Common.Controls.CurrencyTextBox cur순중량;
        private Duzon.Common.BpControls.BpCodeTextBox bpc사업장;
        private Duzon.Common.BpControls.BpCodeTextBox bpc담당자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc거래처;
        private Duzon.Common.BpControls.BpCodeTextBox bbpc영업그룹;
        private Duzon.Common.BpControls.BpCodeTextBox bpc수출자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc제조자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc대행자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc선사;
        private Duzon.Common.BpControls.BpCodeTextBox bpc착하통지처;
        private DatePicker dtp발행일자;
        private DatePicker dtp통관예정일;
        private DatePicker dtp선적에정일;
        private PanelExt panelExt1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextButton tbtn참조SO번호;
        private TextButton tbtn참조신용장번호;
		private System.ComponentModel.IContainer components;

		#endregion

		#region ★ 소멸 부분

        /// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
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
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXINV_BAK));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.tbtn참조SO번호 = new Duzon.Common.Controls.TextButton();
            this.tbtn참조신용장번호 = new Duzon.Common.Controls.TextButton();
            this.dtp선적에정일 = new Duzon.Common.Controls.DatePicker();
            this.dtp통관예정일 = new Duzon.Common.Controls.DatePicker();
            this.dtp발행일자 = new Duzon.Common.Controls.DatePicker();
            this.bpc착하통지처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc선사 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc대행자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc제조자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc수출자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bbpc영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cur순중량 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur총중량 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur외화금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cbo원산지 = new Duzon.Common.Controls.DropDownComboBox();
            this.txt인도조건 = new Duzon.Common.Controls.TextBoxExt();
            this.txt최종목적지 = new Duzon.Common.Controls.TextBoxExt();
            this.txt선적지 = new Duzon.Common.Controls.TextBoxExt();
            this.txt종료CT번호 = new Duzon.Common.Controls.TextBoxExt();
            this.cbo중량단위 = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo운송방법 = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo통화 = new Duzon.Common.Controls.DropDownComboBox();
            this.txt비고1 = new Duzon.Common.Controls.TextBoxExt();
            this.txt비고5 = new Duzon.Common.Controls.TextBoxExt();
            this.txt비고4 = new Duzon.Common.Controls.TextBoxExt();
            this.txt비고2 = new Duzon.Common.Controls.TextBoxExt();
            this.txt비고3 = new Duzon.Common.Controls.TextBoxExt();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.txt도착지 = new Duzon.Common.Controls.TextBoxExt();
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
            this.txtVESSEL명 = new Duzon.Common.Controls.TextBoxExt();
            this.txt시작CT번호 = new Duzon.Common.Controls.TextBoxExt();
            this.cbo포장형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo운송형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo거래구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lbl인도조건 = new Duzon.Common.Controls.LabelExt();
            this.lbl최종목적지 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적지 = new Duzon.Common.Controls.LabelExt();
            this.lbl외화금액 = new Duzon.Common.Controls.LabelExt();
            this.lbl종료CT번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl순중량 = new Duzon.Common.Controls.LabelExt();
            this.lbl중량단위 = new Duzon.Common.Controls.LabelExt();
            this.lbl운송방법 = new Duzon.Common.Controls.LabelExt();
            this.lbl통관예정일 = new Duzon.Common.Controls.LabelExt();
            this.lbl선적예정일 = new Duzon.Common.Controls.LabelExt();
            this.lbl대행자 = new Duzon.Common.Controls.LabelExt();
            this.lbl수출자 = new Duzon.Common.Controls.LabelExt();
            this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
            this.lbl참조SO번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl참조신용장번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl거래구분 = new Duzon.Common.Controls.LabelExt();
            this.txt송장번호 = new Duzon.Common.Controls.TextBoxExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.lbl통화 = new Duzon.Common.Controls.LabelExt();
            this.lbl비고 = new Duzon.Common.Controls.LabelExt();
            this.lbl선사 = new Duzon.Common.Controls.LabelExt();
            this.lblVESSEL명 = new Duzon.Common.Controls.LabelExt();
            this.lbl도착지 = new Duzon.Common.Controls.LabelExt();
            this.lbl착하통지처 = new Duzon.Common.Controls.LabelExt();
            this.lbl시작CT번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl총중량 = new Duzon.Common.Controls.LabelExt();
            this.lbl포장형태 = new Duzon.Common.Controls.LabelExt();
            this.lbl운송형태 = new Duzon.Common.Controls.LabelExt();
            this.lbl원산지 = new Duzon.Common.Controls.LabelExt();
            this.lbl제조자 = new Duzon.Common.Controls.LabelExt();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.lbl송장번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl발행일자 = new Duzon.Common.Controls.LabelExt();
            this.btn내역등록 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn판매경비등록 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp선적에정일)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp통관예정일)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp발행일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur순중량)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur총중량)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur외화금액)).BeginInit();
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
            this.panel4.Controls.Add(this.tbtn참조SO번호);
            this.panel4.Controls.Add(this.tbtn참조신용장번호);
            this.panel4.Controls.Add(this.dtp선적에정일);
            this.panel4.Controls.Add(this.dtp통관예정일);
            this.panel4.Controls.Add(this.dtp발행일자);
            this.panel4.Controls.Add(this.bpc착하통지처);
            this.panel4.Controls.Add(this.bpc선사);
            this.panel4.Controls.Add(this.bpc대행자);
            this.panel4.Controls.Add(this.bpc제조자);
            this.panel4.Controls.Add(this.bpc수출자);
            this.panel4.Controls.Add(this.bbpc영업그룹);
            this.panel4.Controls.Add(this.bpc거래처);
            this.panel4.Controls.Add(this.bpc담당자);
            this.panel4.Controls.Add(this.bpc사업장);
            this.panel4.Controls.Add(this.cur순중량);
            this.panel4.Controls.Add(this.cur총중량);
            this.panel4.Controls.Add(this.cur외화금액);
            this.panel4.Controls.Add(this.cbo원산지);
            this.panel4.Controls.Add(this.txt인도조건);
            this.panel4.Controls.Add(this.txt최종목적지);
            this.panel4.Controls.Add(this.txt선적지);
            this.panel4.Controls.Add(this.txt종료CT번호);
            this.panel4.Controls.Add(this.cbo중량단위);
            this.panel4.Controls.Add(this.cbo운송방법);
            this.panel4.Controls.Add(this.cbo통화);
            this.panel4.Controls.Add(this.txt비고1);
            this.panel4.Controls.Add(this.txt비고5);
            this.panel4.Controls.Add(this.txt비고4);
            this.panel4.Controls.Add(this.txt비고2);
            this.panel4.Controls.Add(this.txt비고3);
            this.panel4.Controls.Add(this.panel12);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel11);
            this.panel4.Controls.Add(this.txt도착지);
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
            this.panel4.Controls.Add(this.txtVESSEL명);
            this.panel4.Controls.Add(this.txt시작CT번호);
            this.panel4.Controls.Add(this.cbo포장형태);
            this.panel4.Controls.Add(this.cbo운송형태);
            this.panel4.Controls.Add(this.cbo거래구분);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.txt송장번호);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 525);
            this.panel4.TabIndex = 124;
            // 
            // tbtn참조SO번호
            // 
            this.tbtn참조SO번호.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tbtn참조SO번호.ButtonImage")));
            this.tbtn참조SO번호.Location = new System.Drawing.Point(510, 29);
            this.tbtn참조SO번호.Name = "tbtn참조SO번호";
            this.tbtn참조SO번호.Size = new System.Drawing.Size(250, 21);
            this.tbtn참조SO번호.TabIndex = 142;
            this.tbtn참조SO번호.Tag = "NO_SO";
            this.tbtn참조SO번호.Text = "textButton2";
            // 
            // tbtn참조신용장번호
            // 
            this.tbtn참조신용장번호.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tbtn참조신용장번호.ButtonImage")));
            this.tbtn참조신용장번호.Location = new System.Drawing.Point(510, 2);
            this.tbtn참조신용장번호.Name = "tbtn참조신용장번호";
            this.tbtn참조신용장번호.Size = new System.Drawing.Size(250, 21);
            this.tbtn참조신용장번호.TabIndex = 141;
            this.tbtn참조신용장번호.Tag = "NO_LC";
            this.tbtn참조신용장번호.Text = "textButton1";
            // 
            // dtp선적에정일
            // 
            this.dtp선적에정일.BackColor = System.Drawing.Color.White;
            this.dtp선적에정일.CalendarBackColor = System.Drawing.Color.White;
            this.dtp선적에정일.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp선적에정일.FriDayColor = System.Drawing.Color.Blue;
            this.dtp선적에정일.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp선적에정일.Location = new System.Drawing.Point(510, 178);
            this.dtp선적에정일.Mask = "####/##/##";
            this.dtp선적에정일.MaskBackColor = System.Drawing.Color.White;
            this.dtp선적에정일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp선적에정일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp선적에정일.Modified = false;
            this.dtp선적에정일.Name = "dtp선적에정일";
            this.dtp선적에정일.PaddingCharacter = '_';
            this.dtp선적에정일.PassivePromptCharacter = '_';
            this.dtp선적에정일.PromptCharacter = '_';
            this.dtp선적에정일.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp선적에정일.ShowToDay = true;
            this.dtp선적에정일.ShowTodayCircle = true;
            this.dtp선적에정일.ShowUpDown = false;
            this.dtp선적에정일.Size = new System.Drawing.Size(87, 21);
            this.dtp선적에정일.SunDayColor = System.Drawing.Color.Red;
            this.dtp선적에정일.TabIndex = 15;
            this.dtp선적에정일.Tag = "DT_LOADING";
            this.dtp선적에정일.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp선적에정일.TitleForeColor = System.Drawing.Color.Black;
            this.dtp선적에정일.ToDayColor = System.Drawing.Color.Red;
            this.dtp선적에정일.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp선적에정일.UseKeyF3 = true;
            this.dtp선적에정일.Value = new System.DateTime(((long)(0)));
            this.dtp선적에정일.Click += new System.EventHandler(this.dtp선적에정일_Click);
            // 
            // dtp통관예정일
            // 
            this.dtp통관예정일.BackColor = System.Drawing.Color.White;
            this.dtp통관예정일.CalendarBackColor = System.Drawing.Color.White;
            this.dtp통관예정일.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp통관예정일.FriDayColor = System.Drawing.Color.Blue;
            this.dtp통관예정일.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp통관예정일.Location = new System.Drawing.Point(510, 203);
            this.dtp통관예정일.Mask = "####/##/##";
            this.dtp통관예정일.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp통관예정일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp통관예정일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp통관예정일.Modified = false;
            this.dtp통관예정일.Name = "dtp통관예정일";
            this.dtp통관예정일.PaddingCharacter = '_';
            this.dtp통관예정일.PassivePromptCharacter = '_';
            this.dtp통관예정일.PromptCharacter = '_';
            this.dtp통관예정일.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp통관예정일.ShowToDay = true;
            this.dtp통관예정일.ShowTodayCircle = true;
            this.dtp통관예정일.ShowUpDown = false;
            this.dtp통관예정일.Size = new System.Drawing.Size(87, 21);
            this.dtp통관예정일.SunDayColor = System.Drawing.Color.Red;
            this.dtp통관예정일.TabIndex = 17;
            this.dtp통관예정일.Tag = "DT_TO";
            this.dtp통관예정일.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp통관예정일.TitleForeColor = System.Drawing.Color.Black;
            this.dtp통관예정일.ToDayColor = System.Drawing.Color.Red;
            this.dtp통관예정일.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp통관예정일.UseKeyF3 = true;
            this.dtp통관예정일.Value = new System.DateTime(((long)(0)));
            // 
            // dtp발행일자
            // 
            this.dtp발행일자.BackColor = System.Drawing.Color.White;
            this.dtp발행일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp발행일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp발행일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp발행일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp발행일자.Location = new System.Drawing.Point(120, 28);
            this.dtp발행일자.Mask = "####/##/##";
            this.dtp발행일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp발행일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp발행일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp발행일자.Modified = false;
            this.dtp발행일자.Name = "dtp발행일자";
            this.dtp발행일자.PaddingCharacter = '_';
            this.dtp발행일자.PassivePromptCharacter = '_';
            this.dtp발행일자.PromptCharacter = '_';
            this.dtp발행일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp발행일자.ShowToDay = true;
            this.dtp발행일자.ShowTodayCircle = true;
            this.dtp발행일자.ShowUpDown = false;
            this.dtp발행일자.Size = new System.Drawing.Size(87, 21);
            this.dtp발행일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp발행일자.TabIndex = 2;
            this.dtp발행일자.Tag = "DT_BALLOT";
            this.dtp발행일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp발행일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp발행일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp발행일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp발행일자.UseKeyF3 = false;
            this.dtp발행일자.Value = new System.DateTime(((long)(0)));
            // 
            // bpc착하통지처
            // 
            this.bpc착하통지처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc착하통지처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc착하통지처.ButtonImage")));
            this.bpc착하통지처.ChildMode = "";
            this.bpc착하통지처.CodeName = "";
            this.bpc착하통지처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc착하통지처.CodeValue = "";
            this.bpc착하통지처.ComboCheck = true;
            this.bpc착하통지처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc착하통지처.ItemBackColor = System.Drawing.Color.White;
            this.bpc착하통지처.Location = new System.Drawing.Point(120, 327);
            this.bpc착하통지처.Name = "bpc착하통지처";
            this.bpc착하통지처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc착하통지처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc착하통지처.SearchCode = true;
            this.bpc착하통지처.SelectCount = 0;
            this.bpc착하통지처.SetDefaultValue = false;
            this.bpc착하통지처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc착하통지처.Size = new System.Drawing.Size(268, 21);
            this.bpc착하통지처.TabIndex = 26;
            this.bpc착하통지처.Tag = "CD_CUST_IN";
            this.bpc착하통지처.Text = "bpCodeTextBox1";
            this.bpc착하통지처.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc선사.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc선사.ItemBackColor = System.Drawing.Color.White;
            this.bpc선사.Location = new System.Drawing.Point(120, 203);
            this.bpc선사.Name = "bpc선사";
            this.bpc선사.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc선사.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc선사.SearchCode = true;
            this.bpc선사.SelectCount = 0;
            this.bpc선사.SetDefaultValue = false;
            this.bpc선사.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc선사.Size = new System.Drawing.Size(268, 21);
            this.bpc선사.TabIndex = 16;
            this.bpc선사.Tag = "SHIP_CORP";
            this.bpc선사.Text = "bpCodeTextBox1";
            this.bpc선사.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc대행자
            // 
            this.bpc대행자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc대행자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc대행자.ButtonImage")));
            this.bpc대행자.ChildMode = "";
            this.bpc대행자.CodeName = "";
            this.bpc대행자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc대행자.CodeValue = "";
            this.bpc대행자.ComboCheck = true;
            this.bpc대행자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc대행자.ItemBackColor = System.Drawing.Color.White;
            this.bpc대행자.Location = new System.Drawing.Point(510, 128);
            this.bpc대행자.Name = "bpc대행자";
            this.bpc대행자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc대행자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc대행자.SearchCode = true;
            this.bpc대행자.SelectCount = 0;
            this.bpc대행자.SetDefaultValue = false;
            this.bpc대행자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc대행자.Size = new System.Drawing.Size(268, 21);
            this.bpc대행자.TabIndex = 11;
            this.bpc대행자.Tag = "CD_AGENT";
            this.bpc대행자.Text = "bpCodeTextBox1";
            this.bpc대행자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpc제조자
            // 
            this.bpc제조자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc제조자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc제조자.ButtonImage")));
            this.bpc제조자.ChildMode = "";
            this.bpc제조자.CodeName = "";
            this.bpc제조자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc제조자.CodeValue = "";
            this.bpc제조자.ComboCheck = true;
            this.bpc제조자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc제조자.ItemBackColor = System.Drawing.Color.White;
            this.bpc제조자.Location = new System.Drawing.Point(120, 128);
            this.bpc제조자.Name = "bpc제조자";
            this.bpc제조자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc제조자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc제조자.SearchCode = true;
            this.bpc제조자.SelectCount = 0;
            this.bpc제조자.SetDefaultValue = false;
            this.bpc제조자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc제조자.Size = new System.Drawing.Size(268, 21);
            this.bpc제조자.TabIndex = 10;
            this.bpc제조자.Tag = "CD_PRODUCT;NM_PRODUCT";
            this.bpc제조자.Text = "bpCodeTextBox1";
            this.bpc제조자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc수출자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc수출자.ItemBackColor = System.Drawing.Color.White;
            this.bpc수출자.Location = new System.Drawing.Point(510, 103);
            this.bpc수출자.Name = "bpc수출자";
            this.bpc수출자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc수출자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc수출자.SearchCode = true;
            this.bpc수출자.SelectCount = 0;
            this.bpc수출자.SetDefaultValue = false;
            this.bpc수출자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc수출자.Size = new System.Drawing.Size(268, 21);
            this.bpc수출자.TabIndex = 9;
            this.bpc수출자.Tag = "CD_EXPORT";
            this.bpc수출자.Text = "bpCodeTextBox1";
            this.bpc수출자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bbpc영업그룹
            // 
            this.bbpc영업그룹.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bbpc영업그룹.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bbpc영업그룹.ButtonImage")));
            this.bbpc영업그룹.ChildMode = "";
            this.bbpc영업그룹.CodeName = "";
            this.bbpc영업그룹.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bbpc영업그룹.CodeValue = "";
            this.bbpc영업그룹.ComboCheck = true;
            this.bbpc영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bbpc영업그룹.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bbpc영업그룹.Location = new System.Drawing.Point(120, 78);
            this.bbpc영업그룹.Name = "bbpc영업그룹";
            this.bbpc영업그룹.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bbpc영업그룹.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bbpc영업그룹.SearchCode = true;
            this.bbpc영업그룹.SelectCount = 0;
            this.bbpc영업그룹.SetDefaultValue = true;
            this.bbpc영업그룹.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bbpc영업그룹.Size = new System.Drawing.Size(268, 21);
            this.bbpc영업그룹.TabIndex = 6;
            this.bbpc영업그룹.Tag = "CD_SALEGRP;NM_SALEGRP";
            this.bbpc영업그룹.Text = "bpCodeTextBox1";
            this.bbpc영업그룹.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc거래처.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc거래처.Location = new System.Drawing.Point(120, 53);
            this.bpc거래처.Name = "bpc거래처";
            this.bpc거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc거래처.SearchCode = true;
            this.bpc거래처.SelectCount = 0;
            this.bpc거래처.SetDefaultValue = true;
            this.bpc거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc거래처.Size = new System.Drawing.Size(268, 21);
            this.bpc거래처.TabIndex = 4;
            this.bpc거래처.Tag = "CD_PARTNER;NM_PARTNER";
            this.bpc거래처.Text = "bpCodeTextBox1";
            this.bpc거래처.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc담당자.Location = new System.Drawing.Point(120, 103);
            this.bpc담당자.Name = "bpc담당자";
            this.bpc담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc담당자.SearchCode = true;
            this.bpc담당자.SelectCount = 0;
            this.bpc담당자.SetDefaultValue = true;
            this.bpc담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc담당자.Size = new System.Drawing.Size(268, 21);
            this.bpc담당자.TabIndex = 8;
            this.bpc담당자.Tag = "NO_EMP;NM_KOR";
            this.bpc담당자.Text = "bpCodeTextBox1";
            this.bpc담당자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpc사업장.Location = new System.Drawing.Point(511, 78);
            this.bpc사업장.Name = "bpc사업장";
            this.bpc사업장.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc사업장.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc사업장.SearchCode = true;
            this.bpc사업장.SelectCount = 0;
            this.bpc사업장.SetDefaultValue = true;
            this.bpc사업장.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc사업장.Size = new System.Drawing.Size(268, 21);
            this.bpc사업장.TabIndex = 7;
            this.bpc사업장.Tag = "CD_BIZAREA";
            this.bpc사업장.Text = "bpCodeTextBox1";
            this.bpc사업장.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // cur순중량
            // 
            this.cur순중량.BackColor = System.Drawing.Color.White;
            this.cur순중량.CurrencyDecimalDigits = 4;
            this.cur순중량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur순중량.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur순중량.Location = new System.Drawing.Point(510, 277);
            this.cur순중량.Mask = null;
            this.cur순중량.MaxLength = 22;
            this.cur순중량.Name = "cur순중량";
            this.cur순중량.NullString = "0";
            this.cur순중량.PositiveColor = System.Drawing.Color.Black;
            this.cur순중량.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur순중량.Size = new System.Drawing.Size(100, 21);
            this.cur순중량.TabIndex = 23;
            this.cur순중량.Tag = "NET_WEIGHT";
            this.cur순중량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur순중량.UseKeyEnter = true;
            this.cur순중량.UseKeyF3 = true;
            this.cur순중량.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cur총중량
            // 
            this.cur총중량.BackColor = System.Drawing.Color.White;
            this.cur총중량.CurrencyDecimalDigits = 4;
            this.cur총중량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur총중량.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur총중량.Location = new System.Drawing.Point(120, 277);
            this.cur총중량.Mask = null;
            this.cur총중량.MaxLength = 22;
            this.cur총중량.Name = "cur총중량";
            this.cur총중량.NullString = "0";
            this.cur총중량.PositiveColor = System.Drawing.Color.Black;
            this.cur총중량.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur총중량.Size = new System.Drawing.Size(100, 21);
            this.cur총중량.TabIndex = 22;
            this.cur총중량.Tag = "GROSS_WEIGHT";
            this.cur총중량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur총중량.UseKeyEnter = true;
            this.cur총중량.UseKeyF3 = true;
            this.cur총중량.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
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
            this.cur외화금액.Enabled = false;
            this.cur외화금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur외화금액.Location = new System.Drawing.Point(510, 153);
            this.cur외화금액.Mask = null;
            this.cur외화금액.MaxLength = 22;
            this.cur외화금액.Name = "cur외화금액";
            this.cur외화금액.NullString = "0";
            this.cur외화금액.PositiveColor = System.Drawing.Color.Black;
            this.cur외화금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur외화금액.Size = new System.Drawing.Size(150, 21);
            this.cur외화금액.TabIndex = 17;
            this.cur외화금액.Tag = "AM_EX";
            this.cur외화금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur외화금액.UseKeyEnter = true;
            this.cur외화금액.UseKeyF3 = true;
            this.cur외화금액.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cbo원산지
            // 
            this.cbo원산지.AutoDropDown = true;
            this.cbo원산지.BackColor = System.Drawing.Color.White;
            this.cbo원산지.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo원산지.Location = new System.Drawing.Point(120, 178);
            this.cbo원산지.Name = "cbo원산지";
            this.cbo원산지.ShowCheckBox = false;
            this.cbo원산지.Size = new System.Drawing.Size(268, 20);
            this.cbo원산지.TabIndex = 14;
            this.cbo원산지.Tag = "CD_ORIGIN";
            this.cbo원산지.UseKeyEnter = true;
            this.cbo원산지.UseKeyF3 = true;
            this.cbo원산지.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt인도조건
            // 
            this.txt인도조건.BackColor = System.Drawing.Color.White;
            this.txt인도조건.Location = new System.Drawing.Point(510, 377);
            this.txt인도조건.MaxLength = 50;
            this.txt인도조건.Name = "txt인도조건";
            this.txt인도조건.SelectedAllEnabled = false;
            this.txt인도조건.Size = new System.Drawing.Size(268, 21);
            this.txt인도조건.TabIndex = 31;
            this.txt인도조건.Tag = "COND_TRANS";
            this.txt인도조건.UseKeyEnter = true;
            this.txt인도조건.UseKeyF3 = true;
            this.txt인도조건.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt최종목적지
            // 
            this.txt최종목적지.BackColor = System.Drawing.Color.White;
            this.txt최종목적지.Location = new System.Drawing.Point(510, 352);
            this.txt최종목적지.MaxLength = 50;
            this.txt최종목적지.Name = "txt최종목적지";
            this.txt최종목적지.SelectedAllEnabled = false;
            this.txt최종목적지.Size = new System.Drawing.Size(268, 21);
            this.txt최종목적지.TabIndex = 29;
            this.txt최종목적지.Tag = "DESTINATION";
            this.txt최종목적지.UseKeyEnter = true;
            this.txt최종목적지.UseKeyF3 = true;
            this.txt최종목적지.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt선적지
            // 
            this.txt선적지.BackColor = System.Drawing.Color.White;
            this.txt선적지.Location = new System.Drawing.Point(510, 327);
            this.txt선적지.MaxLength = 50;
            this.txt선적지.Name = "txt선적지";
            this.txt선적지.SelectedAllEnabled = false;
            this.txt선적지.Size = new System.Drawing.Size(268, 21);
            this.txt선적지.TabIndex = 27;
            this.txt선적지.Tag = "PORT_LOADING";
            this.txt선적지.UseKeyEnter = true;
            this.txt선적지.UseKeyF3 = true;
            this.txt선적지.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt종료CT번호
            // 
            this.txt종료CT번호.BackColor = System.Drawing.Color.White;
            this.txt종료CT번호.Location = new System.Drawing.Point(510, 302);
            this.txt종료CT번호.MaxLength = 10;
            this.txt종료CT번호.Name = "txt종료CT번호";
            this.txt종료CT번호.SelectedAllEnabled = false;
            this.txt종료CT번호.Size = new System.Drawing.Size(150, 21);
            this.txt종료CT번호.TabIndex = 25;
            this.txt종료CT번호.Tag = "NO_ECT";
            this.txt종료CT번호.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt종료CT번호.UseKeyEnter = true;
            this.txt종료CT번호.UseKeyF3 = true;
            this.txt종료CT번호.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cbo중량단위
            // 
            this.cbo중량단위.AutoDropDown = true;
            this.cbo중량단위.BackColor = System.Drawing.Color.White;
            this.cbo중량단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo중량단위.Location = new System.Drawing.Point(510, 253);
            this.cbo중량단위.Name = "cbo중량단위";
            this.cbo중량단위.ShowCheckBox = false;
            this.cbo중량단위.Size = new System.Drawing.Size(268, 20);
            this.cbo중량단위.TabIndex = 21;
            this.cbo중량단위.Tag = "CD_WEIGHT";
            this.cbo중량단위.UseKeyEnter = true;
            this.cbo중량단위.UseKeyF3 = true;
            this.cbo중량단위.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo운송방법
            // 
            this.cbo운송방법.AutoDropDown = true;
            this.cbo운송방법.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo운송방법.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo운송방법.Location = new System.Drawing.Point(510, 229);
            this.cbo운송방법.Name = "cbo운송방법";
            this.cbo운송방법.ShowCheckBox = false;
            this.cbo운송방법.Size = new System.Drawing.Size(268, 20);
            this.cbo운송방법.TabIndex = 19;
            this.cbo운송방법.Tag = "TP_TRANS";
            this.cbo운송방법.UseKeyEnter = true;
            this.cbo운송방법.UseKeyF3 = true;
            this.cbo운송방법.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo통화
            // 
            this.cbo통화.AutoDropDown = true;
            this.cbo통화.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo통화.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo통화.Location = new System.Drawing.Point(120, 153);
            this.cbo통화.Name = "cbo통화";
            this.cbo통화.ShowCheckBox = false;
            this.cbo통화.Size = new System.Drawing.Size(268, 20);
            this.cbo통화.TabIndex = 12;
            this.cbo통화.Tag = "CD_EXCH";
            this.cbo통화.UseKeyEnter = true;
            this.cbo통화.UseKeyF3 = true;
            this.cbo통화.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // txt비고1
            // 
            this.txt비고1.BackColor = System.Drawing.Color.White;
            this.txt비고1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고1.Location = new System.Drawing.Point(120, 401);
            this.txt비고1.MaxLength = 100;
            this.txt비고1.Name = "txt비고1";
            this.txt비고1.SelectedAllEnabled = false;
            this.txt비고1.Size = new System.Drawing.Size(660, 21);
            this.txt비고1.TabIndex = 32;
            this.txt비고1.Tag = "REMARK1";
            this.txt비고1.UseKeyEnter = true;
            this.txt비고1.UseKeyF3 = true;
            this.txt비고1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt비고5
            // 
            this.txt비고5.BackColor = System.Drawing.Color.White;
            this.txt비고5.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고5.Location = new System.Drawing.Point(120, 500);
            this.txt비고5.MaxLength = 100;
            this.txt비고5.Name = "txt비고5";
            this.txt비고5.SelectedAllEnabled = false;
            this.txt비고5.Size = new System.Drawing.Size(660, 21);
            this.txt비고5.TabIndex = 36;
            this.txt비고5.Tag = "REMARK5";
            this.txt비고5.UseKeyEnter = true;
            this.txt비고5.UseKeyF3 = true;
            this.txt비고5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt비고4
            // 
            this.txt비고4.BackColor = System.Drawing.Color.White;
            this.txt비고4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고4.Location = new System.Drawing.Point(120, 476);
            this.txt비고4.MaxLength = 100;
            this.txt비고4.Name = "txt비고4";
            this.txt비고4.SelectedAllEnabled = false;
            this.txt비고4.Size = new System.Drawing.Size(660, 21);
            this.txt비고4.TabIndex = 35;
            this.txt비고4.Tag = "REMARK4";
            this.txt비고4.UseKeyEnter = true;
            this.txt비고4.UseKeyF3 = true;
            this.txt비고4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt비고2
            // 
            this.txt비고2.BackColor = System.Drawing.Color.White;
            this.txt비고2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt비고2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt비고2.Location = new System.Drawing.Point(120, 426);
            this.txt비고2.MaxLength = 100;
            this.txt비고2.Name = "txt비고2";
            this.txt비고2.SelectedAllEnabled = false;
            this.txt비고2.Size = new System.Drawing.Size(660, 21);
            this.txt비고2.TabIndex = 33;
            this.txt비고2.Tag = "REMARK2";
            this.txt비고2.UseKeyEnter = true;
            this.txt비고2.UseKeyF3 = true;
            this.txt비고2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
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
            this.txt비고3.TabIndex = 34;
            this.txt비고3.Tag = "REMARK3";
            this.txt비고3.UseKeyEnter = true;
            this.txt비고3.UseKeyF3 = true;
            this.txt비고3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel12.Location = new System.Drawing.Point(116, 497);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(664, 1);
            this.panel12.TabIndex = 140;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel7.Location = new System.Drawing.Point(116, 473);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(664, 1);
            this.panel7.TabIndex = 139;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel8.Location = new System.Drawing.Point(116, 448);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(664, 1);
            this.panel8.TabIndex = 138;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
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
            // txt도착지
            // 
            this.txt도착지.BackColor = System.Drawing.Color.White;
            this.txt도착지.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt도착지.Location = new System.Drawing.Point(120, 352);
            this.txt도착지.MaxLength = 50;
            this.txt도착지.Name = "txt도착지";
            this.txt도착지.SelectedAllEnabled = false;
            this.txt도착지.Size = new System.Drawing.Size(268, 21);
            this.txt도착지.TabIndex = 28;
            this.txt도착지.Tag = "PORT_ARRIVER";
            this.txt도착지.UseKeyEnter = true;
            this.txt도착지.UseKeyF3 = true;
            this.txt도착지.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
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
            // txtVESSEL명
            // 
            this.txtVESSEL명.BackColor = System.Drawing.Color.White;
            this.txtVESSEL명.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVESSEL명.Location = new System.Drawing.Point(120, 377);
            this.txtVESSEL명.MaxLength = 50;
            this.txtVESSEL명.Name = "txtVESSEL명";
            this.txtVESSEL명.SelectedAllEnabled = false;
            this.txtVESSEL명.Size = new System.Drawing.Size(268, 21);
            this.txtVESSEL명.TabIndex = 30;
            this.txtVESSEL명.Tag = "NM_VESSEL";
            this.txtVESSEL명.UseKeyEnter = true;
            this.txtVESSEL명.UseKeyF3 = true;
            this.txtVESSEL명.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // txt시작CT번호
            // 
            this.txt시작CT번호.BackColor = System.Drawing.Color.White;
            this.txt시작CT번호.Location = new System.Drawing.Point(120, 302);
            this.txt시작CT번호.MaxLength = 10;
            this.txt시작CT번호.Name = "txt시작CT번호";
            this.txt시작CT번호.SelectedAllEnabled = false;
            this.txt시작CT번호.Size = new System.Drawing.Size(150, 21);
            this.txt시작CT번호.TabIndex = 24;
            this.txt시작CT번호.Tag = "NO_SCT";
            this.txt시작CT번호.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt시작CT번호.UseKeyEnter = true;
            this.txt시작CT번호.UseKeyF3 = true;
            this.txt시작CT번호.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // cbo포장형태
            // 
            this.cbo포장형태.AutoDropDown = true;
            this.cbo포장형태.BackColor = System.Drawing.Color.White;
            this.cbo포장형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo포장형태.Location = new System.Drawing.Point(120, 253);
            this.cbo포장형태.Name = "cbo포장형태";
            this.cbo포장형태.ShowCheckBox = false;
            this.cbo포장형태.Size = new System.Drawing.Size(268, 20);
            this.cbo포장형태.TabIndex = 20;
            this.cbo포장형태.Tag = "TP_PACKING";
            this.cbo포장형태.UseKeyEnter = true;
            this.cbo포장형태.UseKeyF3 = true;
            this.cbo포장형태.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo운송형태
            // 
            this.cbo운송형태.AutoDropDown = true;
            this.cbo운송형태.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo운송형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo운송형태.Location = new System.Drawing.Point(120, 228);
            this.cbo운송형태.Name = "cbo운송형태";
            this.cbo운송형태.ShowCheckBox = false;
            this.cbo운송형태.Size = new System.Drawing.Size(268, 20);
            this.cbo운송형태.TabIndex = 18;
            this.cbo운송형태.Tag = "TP_TRANSPORT";
            this.cbo운송형태.UseKeyEnter = true;
            this.cbo운송형태.UseKeyF3 = true;
            this.cbo운송형태.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cbo거래구분
            // 
            this.cbo거래구분.AutoDropDown = true;
            this.cbo거래구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo거래구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo거래구분.Location = new System.Drawing.Point(510, 54);
            this.cbo거래구분.Name = "cbo거래구분";
            this.cbo거래구분.ShowCheckBox = false;
            this.cbo거래구분.Size = new System.Drawing.Size(268, 20);
            this.cbo거래구분.TabIndex = 5;
            this.cbo거래구분.Tag = "FG_LC";
            this.cbo거래구분.UseKeyEnter = true;
            this.cbo거래구분.UseKeyF3 = true;
            this.cbo거래구분.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lbl인도조건);
            this.panel6.Controls.Add(this.lbl최종목적지);
            this.panel6.Controls.Add(this.lbl선적지);
            this.panel6.Controls.Add(this.lbl외화금액);
            this.panel6.Controls.Add(this.lbl종료CT번호);
            this.panel6.Controls.Add(this.lbl순중량);
            this.panel6.Controls.Add(this.lbl중량단위);
            this.panel6.Controls.Add(this.lbl운송방법);
            this.panel6.Controls.Add(this.lbl통관예정일);
            this.panel6.Controls.Add(this.lbl선적예정일);
            this.panel6.Controls.Add(this.lbl대행자);
            this.panel6.Controls.Add(this.lbl수출자);
            this.panel6.Controls.Add(this.lbl사업장);
            this.panel6.Controls.Add(this.lbl참조SO번호);
            this.panel6.Controls.Add(this.lbl참조신용장번호);
            this.panel6.Controls.Add(this.lbl거래구분);
            this.panel6.Location = new System.Drawing.Point(391, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(115, 399);
            this.panel6.TabIndex = 4;
            // 
            // lbl인도조건
            // 
            this.lbl인도조건.Location = new System.Drawing.Point(3, 379);
            this.lbl인도조건.Name = "lbl인도조건";
            this.lbl인도조건.Resizeble = true;
            this.lbl인도조건.Size = new System.Drawing.Size(110, 18);
            this.lbl인도조건.TabIndex = 21;
            this.lbl인도조건.Tag = "COND_TRANS";
            this.lbl인도조건.Text = "인도조건";
            this.lbl인도조건.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl최종목적지
            // 
            this.lbl최종목적지.Location = new System.Drawing.Point(3, 355);
            this.lbl최종목적지.Name = "lbl최종목적지";
            this.lbl최종목적지.Resizeble = true;
            this.lbl최종목적지.Size = new System.Drawing.Size(110, 18);
            this.lbl최종목적지.TabIndex = 20;
            this.lbl최종목적지.Tag = "DESTINATION";
            this.lbl최종목적지.Text = "최종목적지";
            this.lbl최종목적지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적지
            // 
            this.lbl선적지.Location = new System.Drawing.Point(3, 329);
            this.lbl선적지.Name = "lbl선적지";
            this.lbl선적지.Resizeble = true;
            this.lbl선적지.Size = new System.Drawing.Size(110, 18);
            this.lbl선적지.TabIndex = 19;
            this.lbl선적지.Tag = "SHIP_PORT";
            this.lbl선적지.Text = "선적지";
            this.lbl선적지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl외화금액
            // 
            this.lbl외화금액.Location = new System.Drawing.Point(3, 155);
            this.lbl외화금액.Name = "lbl외화금액";
            this.lbl외화금액.Resizeble = true;
            this.lbl외화금액.Size = new System.Drawing.Size(110, 18);
            this.lbl외화금액.TabIndex = 12;
            this.lbl외화금액.Tag = "AMT_EX";
            this.lbl외화금액.Text = "외화금액";
            this.lbl외화금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl종료CT번호
            // 
            this.lbl종료CT번호.Location = new System.Drawing.Point(3, 304);
            this.lbl종료CT번호.Name = "lbl종료CT번호";
            this.lbl종료CT번호.Resizeble = true;
            this.lbl종료CT번호.Size = new System.Drawing.Size(110, 18);
            this.lbl종료CT번호.TabIndex = 11;
            this.lbl종료CT번호.Tag = "NO_ECT";
            this.lbl종료CT번호.Text = "종료C/T번호";
            this.lbl종료CT번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl순중량
            // 
            this.lbl순중량.Location = new System.Drawing.Point(3, 279);
            this.lbl순중량.Name = "lbl순중량";
            this.lbl순중량.Resizeble = true;
            this.lbl순중량.Size = new System.Drawing.Size(110, 18);
            this.lbl순중량.TabIndex = 10;
            this.lbl순중량.Tag = "NET_WEIGHT";
            this.lbl순중량.Text = "순중량";
            this.lbl순중량.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl중량단위
            // 
            this.lbl중량단위.Location = new System.Drawing.Point(3, 254);
            this.lbl중량단위.Name = "lbl중량단위";
            this.lbl중량단위.Resizeble = true;
            this.lbl중량단위.Size = new System.Drawing.Size(110, 18);
            this.lbl중량단위.TabIndex = 9;
            this.lbl중량단위.Tag = "UNIT_WEIGHT";
            this.lbl중량단위.Text = "중량단위";
            this.lbl중량단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl운송방법
            // 
            this.lbl운송방법.Location = new System.Drawing.Point(3, 230);
            this.lbl운송방법.Name = "lbl운송방법";
            this.lbl운송방법.Resizeble = true;
            this.lbl운송방법.Size = new System.Drawing.Size(110, 18);
            this.lbl운송방법.TabIndex = 8;
            this.lbl운송방법.Tag = "CARRY";
            this.lbl운송방법.Text = "운송방법";
            this.lbl운송방법.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl통관예정일
            // 
            this.lbl통관예정일.Location = new System.Drawing.Point(3, 205);
            this.lbl통관예정일.Name = "lbl통관예정일";
            this.lbl통관예정일.Resizeble = true;
            this.lbl통관예정일.Size = new System.Drawing.Size(110, 18);
            this.lbl통관예정일.TabIndex = 7;
            this.lbl통관예정일.Tag = "DT_ONTO";
            this.lbl통관예정일.Text = "통관예정일";
            this.lbl통관예정일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선적예정일
            // 
            this.lbl선적예정일.Location = new System.Drawing.Point(3, 180);
            this.lbl선적예정일.Name = "lbl선적예정일";
            this.lbl선적예정일.Resizeble = true;
            this.lbl선적예정일.Size = new System.Drawing.Size(110, 18);
            this.lbl선적예정일.TabIndex = 6;
            this.lbl선적예정일.Tag = "DT_ONSHIP";
            this.lbl선적예정일.Text = "선적예정일";
            this.lbl선적예정일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl대행자
            // 
            this.lbl대행자.Location = new System.Drawing.Point(3, 129);
            this.lbl대행자.Name = "lbl대행자";
            this.lbl대행자.Resizeble = true;
            this.lbl대행자.Size = new System.Drawing.Size(110, 18);
            this.lbl대행자.TabIndex = 5;
            this.lbl대행자.Tag = "AGENT_TRAN";
            this.lbl대행자.Text = "대행자";
            this.lbl대행자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl수출자
            // 
            this.lbl수출자.Location = new System.Drawing.Point(3, 104);
            this.lbl수출자.Name = "lbl수출자";
            this.lbl수출자.Resizeble = true;
            this.lbl수출자.Size = new System.Drawing.Size(110, 18);
            this.lbl수출자.TabIndex = 4;
            this.lbl수출자.Tag = "EXPORT_TRAN";
            this.lbl수출자.Text = "수출자";
            this.lbl수출자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl사업장
            // 
            this.lbl사업장.Location = new System.Drawing.Point(3, 80);
            this.lbl사업장.Name = "lbl사업장";
            this.lbl사업장.Resizeble = true;
            this.lbl사업장.Size = new System.Drawing.Size(110, 18);
            this.lbl사업장.TabIndex = 3;
            this.lbl사업장.Tag = "BALLOT";
            this.lbl사업장.Text = "사업장";
            this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl참조SO번호
            // 
            this.lbl참조SO번호.Location = new System.Drawing.Point(3, 30);
            this.lbl참조SO번호.Name = "lbl참조SO번호";
            this.lbl참조SO번호.Resizeble = true;
            this.lbl참조SO번호.Size = new System.Drawing.Size(110, 18);
            this.lbl참조SO번호.TabIndex = 2;
            this.lbl참조SO번호.Tag = "SO_REFER";
            this.lbl참조SO번호.Text = "참조S/O번호";
            this.lbl참조SO번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl참조신용장번호
            // 
            this.lbl참조신용장번호.Location = new System.Drawing.Point(3, 4);
            this.lbl참조신용장번호.Name = "lbl참조신용장번호";
            this.lbl참조신용장번호.Resizeble = true;
            this.lbl참조신용장번호.Size = new System.Drawing.Size(110, 18);
            this.lbl참조신용장번호.TabIndex = 1;
            this.lbl참조신용장번호.Tag = "LC_REFER";
            this.lbl참조신용장번호.Text = "참조신용장 번호";
            this.lbl참조신용장번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl거래구분
            // 
            this.lbl거래구분.Location = new System.Drawing.Point(3, 55);
            this.lbl거래구분.Name = "lbl거래구분";
            this.lbl거래구분.Resizeble = true;
            this.lbl거래구분.Size = new System.Drawing.Size(110, 18);
            this.lbl거래구분.TabIndex = 1;
            this.lbl거래구분.Tag = "FG_TRANS";
            this.lbl거래구분.Text = "거래구분";
            this.lbl거래구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt송장번호
            // 
            this.txt송장번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.txt송장번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt송장번호.Location = new System.Drawing.Point(120, 2);
            this.txt송장번호.MaxLength = 20;
            this.txt송장번호.Name = "txt송장번호";
            this.txt송장번호.SelectedAllEnabled = false;
            this.txt송장번호.Size = new System.Drawing.Size(150, 21);
            this.txt송장번호.TabIndex = 0;
            this.txt송장번호.Tag = "NO_INV";
            this.txt송장번호.UseKeyEnter = true;
            this.txt송장번호.UseKeyF3 = true;
            this.txt송장번호.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonTextBox_KeyEvent);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lbl통화);
            this.panel5.Controls.Add(this.lbl비고);
            this.panel5.Controls.Add(this.lbl선사);
            this.panel5.Controls.Add(this.lblVESSEL명);
            this.panel5.Controls.Add(this.lbl도착지);
            this.panel5.Controls.Add(this.lbl착하통지처);
            this.panel5.Controls.Add(this.lbl시작CT번호);
            this.panel5.Controls.Add(this.lbl총중량);
            this.panel5.Controls.Add(this.lbl포장형태);
            this.panel5.Controls.Add(this.lbl운송형태);
            this.panel5.Controls.Add(this.lbl원산지);
            this.panel5.Controls.Add(this.lbl제조자);
            this.panel5.Controls.Add(this.lbl담당자);
            this.panel5.Controls.Add(this.lbl영업그룹);
            this.panel5.Controls.Add(this.lbl거래처);
            this.panel5.Controls.Add(this.lbl송장번호);
            this.panel5.Controls.Add(this.lbl발행일자);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(115, 520);
            this.panel5.TabIndex = 0;
            // 
            // lbl통화
            // 
            this.lbl통화.Location = new System.Drawing.Point(3, 155);
            this.lbl통화.Name = "lbl통화";
            this.lbl통화.Resizeble = true;
            this.lbl통화.Size = new System.Drawing.Size(110, 18);
            this.lbl통화.TabIndex = 21;
            this.lbl통화.Tag = "CD_CURRENCY";
            this.lbl통화.Text = "통화";
            this.lbl통화.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl비고
            // 
            this.lbl비고.Location = new System.Drawing.Point(3, 403);
            this.lbl비고.Name = "lbl비고";
            this.lbl비고.Resizeble = true;
            this.lbl비고.Size = new System.Drawing.Size(110, 18);
            this.lbl비고.TabIndex = 20;
            this.lbl비고.Tag = "REMARK";
            this.lbl비고.Text = "비고";
            this.lbl비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl선사
            // 
            this.lbl선사.Location = new System.Drawing.Point(3, 204);
            this.lbl선사.Name = "lbl선사";
            this.lbl선사.Resizeble = true;
            this.lbl선사.Size = new System.Drawing.Size(110, 18);
            this.lbl선사.TabIndex = 17;
            this.lbl선사.Tag = "SHIP_CORP";
            this.lbl선사.Text = "선사";
            this.lbl선사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVESSEL명
            // 
            this.lblVESSEL명.Location = new System.Drawing.Point(3, 378);
            this.lblVESSEL명.Name = "lblVESSEL명";
            this.lblVESSEL명.Resizeble = true;
            this.lblVESSEL명.Size = new System.Drawing.Size(110, 18);
            this.lblVESSEL명.TabIndex = 16;
            this.lblVESSEL명.Tag = "VESSEL";
            this.lblVESSEL명.Text = "VESSEL명";
            this.lblVESSEL명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl도착지
            // 
            this.lbl도착지.Location = new System.Drawing.Point(3, 354);
            this.lbl도착지.Name = "lbl도착지";
            this.lbl도착지.Resizeble = true;
            this.lbl도착지.Size = new System.Drawing.Size(110, 18);
            this.lbl도착지.TabIndex = 14;
            this.lbl도착지.Tag = "ARRIVAL_PORT";
            this.lbl도착지.Text = "도착지";
            this.lbl도착지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl착하통지처
            // 
            this.lbl착하통지처.Location = new System.Drawing.Point(3, 329);
            this.lbl착하통지처.Name = "lbl착하통지처";
            this.lbl착하통지처.Resizeble = true;
            this.lbl착하통지처.Size = new System.Drawing.Size(110, 18);
            this.lbl착하통지처.TabIndex = 13;
            this.lbl착하통지처.Tag = "CD_ARTRAN";
            this.lbl착하통지처.Text = "착하통지처";
            this.lbl착하통지처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl시작CT번호
            // 
            this.lbl시작CT번호.Location = new System.Drawing.Point(3, 304);
            this.lbl시작CT번호.Name = "lbl시작CT번호";
            this.lbl시작CT번호.Resizeble = true;
            this.lbl시작CT번호.Size = new System.Drawing.Size(110, 18);
            this.lbl시작CT번호.TabIndex = 11;
            this.lbl시작CT번호.Tag = "NO_SCT";
            this.lbl시작CT번호.Text = "시작C/T번호";
            this.lbl시작CT번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl총중량
            // 
            this.lbl총중량.Location = new System.Drawing.Point(3, 278);
            this.lbl총중량.Name = "lbl총중량";
            this.lbl총중량.Resizeble = true;
            this.lbl총중량.Size = new System.Drawing.Size(110, 18);
            this.lbl총중량.TabIndex = 10;
            this.lbl총중량.Tag = "GROSS_WEIGHT";
            this.lbl총중량.Text = "총중량";
            this.lbl총중량.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl포장형태
            // 
            this.lbl포장형태.Location = new System.Drawing.Point(3, 253);
            this.lbl포장형태.Name = "lbl포장형태";
            this.lbl포장형태.Resizeble = true;
            this.lbl포장형태.Size = new System.Drawing.Size(110, 18);
            this.lbl포장형태.TabIndex = 9;
            this.lbl포장형태.Tag = "NM_PACKING";
            this.lbl포장형태.Text = "포장형태";
            this.lbl포장형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl운송형태
            // 
            this.lbl운송형태.Location = new System.Drawing.Point(3, 229);
            this.lbl운송형태.Name = "lbl운송형태";
            this.lbl운송형태.Resizeble = true;
            this.lbl운송형태.Size = new System.Drawing.Size(110, 18);
            this.lbl운송형태.TabIndex = 8;
            this.lbl운송형태.Tag = "TRANS_TYPE";
            this.lbl운송형태.Text = "운송형태";
            this.lbl운송형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl원산지
            // 
            this.lbl원산지.Location = new System.Drawing.Point(3, 179);
            this.lbl원산지.Name = "lbl원산지";
            this.lbl원산지.Resizeble = true;
            this.lbl원산지.Size = new System.Drawing.Size(110, 18);
            this.lbl원산지.TabIndex = 6;
            this.lbl원산지.Tag = "CO_NATION";
            this.lbl원산지.Text = "원산지";
            this.lbl원산지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl제조자
            // 
            this.lbl제조자.Location = new System.Drawing.Point(3, 130);
            this.lbl제조자.Name = "lbl제조자";
            this.lbl제조자.Resizeble = true;
            this.lbl제조자.Size = new System.Drawing.Size(110, 18);
            this.lbl제조자.TabIndex = 5;
            this.lbl제조자.Tag = "PRODUCT_TRAN";
            this.lbl제조자.Text = "제조자";
            this.lbl제조자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl담당자
            // 
            this.lbl담당자.Location = new System.Drawing.Point(3, 104);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Resizeble = true;
            this.lbl담당자.Size = new System.Drawing.Size(110, 18);
            this.lbl담당자.TabIndex = 4;
            this.lbl담당자.Tag = "NM_EMP";
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl영업그룹
            // 
            this.lbl영업그룹.Location = new System.Drawing.Point(3, 79);
            this.lbl영업그룹.Name = "lbl영업그룹";
            this.lbl영업그룹.Resizeble = true;
            this.lbl영업그룹.Size = new System.Drawing.Size(110, 18);
            this.lbl영업그룹.TabIndex = 3;
            this.lbl영업그룹.Tag = "GROUP_ISUL";
            this.lbl영업그룹.Text = "영업그룹";
            this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl거래처
            // 
            this.lbl거래처.Location = new System.Drawing.Point(3, 55);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Resizeble = true;
            this.lbl거래처.Size = new System.Drawing.Size(110, 18);
            this.lbl거래처.TabIndex = 2;
            this.lbl거래처.Tag = "CD_TRANS";
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl송장번호
            // 
            this.lbl송장번호.Location = new System.Drawing.Point(3, 4);
            this.lbl송장번호.Name = "lbl송장번호";
            this.lbl송장번호.Resizeble = true;
            this.lbl송장번호.Size = new System.Drawing.Size(110, 18);
            this.lbl송장번호.TabIndex = 0;
            this.lbl송장번호.Tag = "NO_INV";
            this.lbl송장번호.Text = "송장번호";
            this.lbl송장번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl발행일자
            // 
            this.lbl발행일자.Location = new System.Drawing.Point(3, 30);
            this.lbl발행일자.Name = "lbl발행일자";
            this.lbl발행일자.Resizeble = true;
            this.lbl발행일자.Size = new System.Drawing.Size(110, 18);
            this.lbl발행일자.TabIndex = 0;
            this.lbl발행일자.Tag = "DT_ISSUE";
            this.lbl발행일자.Text = "발행일자";
            this.lbl발행일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn내역등록
            // 
            this.btn내역등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn내역등록.BackColor = System.Drawing.Color.White;
            this.btn내역등록.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn내역등록.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn내역등록.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn내역등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn내역등록.Location = new System.Drawing.Point(685, 0);
            this.btn내역등록.Name = "btn내역등록";
            this.btn내역등록.Size = new System.Drawing.Size(100, 24);
            this.btn내역등록.TabIndex = 126;
            this.btn내역등록.TabStop = false;
            this.btn내역등록.Tag = "MOD_TEXT";
            this.btn내역등록.Text = "내역등록";
            this.btn내역등록.UseVisualStyleBackColor = false;
            this.btn내역등록.Click += new System.EventHandler(this.m_btnModText_Click);
            // 
            // btn판매경비등록
            // 
            this.btn판매경비등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn판매경비등록.BackColor = System.Drawing.Color.White;
            this.btn판매경비등록.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn판매경비등록.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn판매경비등록.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn판매경비등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn판매경비등록.Location = new System.Drawing.Point(563, 0);
            this.btn판매경비등록.Name = "btn판매경비등록";
            this.btn판매경비등록.Size = new System.Drawing.Size(120, 24);
            this.btn판매경비등록.TabIndex = 127;
            this.btn판매경비등록.TabStop = false;
            this.btn판매경비등록.Tag = "INPUT_COST";
            this.btn판매경비등록.Text = "판매경비등록";
            this.btn판매경비등록.UseVisualStyleBackColor = false;
            this.btn판매경비등록.Click += new System.EventHandler(this.m_btnInputCost_Click);
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn판매경비등록);
            this.panelExt1.Controls.Add(this.btn내역등록);
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
            this.TitleText = "송장등록";
            this.Load += new System.EventHandler(this.P_TR_EXINV_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_TR_EXINV_Paint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp선적에정일)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp통관예정일)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp발행일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur순중량)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur총중량)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur외화금액)).EndInit();
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

		// 거래처 코드 & 명칭 변수
		private string m_CdPartner = "";
		private string m_NmPartner = "";

		// 영업그룹 코드 & 명칭 변수
		private string m_CdGroup = "";
		private string m_NmGroup = "";

		// 담당자 코드 & 명칭 변수
		private string m_CdEmp = "";
		private string m_NmEmp = "";

		// 사업장 코드 변수
		private string m_CdBizarea;

		// 수출자 코드 & 명칭 변수
		private string m_CdExport = "";
		private string m_NmExport = "";

		// 제조자 코드 & 명칭 변수
		private string m_CdPRODUCT = "";
		private string m_NmPRODUCT = "";

		// 대행자 코드 & 명칭 변수
		private string m_CdAgent = "";
		private string m_NmAgent = "";

		// 선사 코드 & 명칭 변수
		private string m_CdShipCorp = "";
		private string m_NmShipCorp = "";

		// 착하 통지처
		private string m_CdCustIn = "";
		private string m_NmCustIn = "";

		private bool m_IsPageActivated = false;
		private bool m_IsSave = true;

		#region ● 초기화 부분

		/// <summary>
		/// 생성자
		/// </summary>
		public P_TR_EXINV_BAK()
		{
			try
			{

				InitializeComponent();

				// Currency Box 설정
				this.cur외화금액.NumberFormatInfoObject.NumberDecimalDigits = 4;
				this.cur총중량.NumberFormatInfoObject.NumberDecimalDigits = 4;
				this.cur순중량.NumberFormatInfoObject.NumberDecimalDigits = 4;

				this.cur외화금액.Text = "0";
				this.cur총중량.Text = "0";
				this.cur순중량.Text = "0";

			}
			catch(Exception ex)
			{

				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		/// 폼 로딩시 DD셋팅을 한다.
		/// 검색어로 검색을 한다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void P_TR_EXINV_Load(object sender, EventArgs e)
		{
			Show();

			try
			{
				// DD명 초기화
				this.InitDD();

				// 날짜 초기화
				this.InitDate();

				// Control 비 활성화
				SetControlDisable();
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}


		/// <summary>
		/// Paint  이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void P_TR_EXINV_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				this.Paint -= new System.Windows.Forms.PaintEventHandler(this.P_TR_EXINV_Paint);

				Application.DoEvents();
				// 콤보 박스 초기화
				this.InitComboBox();			

				// 초기 테이블 Setting.
				this.SelectInit();
				
				// 초기 테이블 추가 및 바인딩
				this.Append();

				// 버튼 설정
				this.SetButton();

			}
			catch(System.Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}



		/// <summary>
		/// 버튼 초기화
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
		/// DD 설정
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

            btn판매경비등록.Text = this.MainFrameInterface.GetDataDictionaryItem("TR", (string)btn판매경비등록.Tag);
			btn내역등록.Text = this.MainFrameInterface.GetDataDictionaryItem("TR", (string)btn내역등록.Tag);
		}
		
		/// <summary>
		/// 검색 날짜 텍스트 박스 초기화
		/// </summary>
		private void InitDate()
		{
			string ls_day = this.MainFrameInterface.GetStringToday;


            this.dtp발행일자.Text = ls_day;
            this.dtp선적에정일.Text = ls_day;
            this.dtp통관예정일.Text = ls_day;

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

			string[] ls_args = new string[7];
			ls_args[0] = "N;TR_IM00005";// 거래구분
			ls_args[1] = "N;MA_B000005";// 통화
			ls_args[2] = "N;TR_IM00009";// 운송형태
			ls_args[3] = "S;MA_B000004";// 중량단위?????
			ls_args[4] = "N;TR_IM00008";// 운송방법
			ls_args[5] = "S;MA_B000020";// 원산지
			ls_args[6] = "S;TR_IM00011";// 포장형태

            DataSet lds_Combo = GetComboData(ls_args);

            // 거래구분
            this.cbo거래구분.DataSource = lds_Combo.Tables[0];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";

            // 통화
            this.cbo통화.DataSource = lds_Combo.Tables[1];
            this.cbo통화.DisplayMember = "NAME";
            this.cbo통화.ValueMember = "CODE";

            // 운송형태
            this.cbo운송형태.DataSource = lds_Combo.Tables[2];
            this.cbo운송형태.DisplayMember = "NAME";
            this.cbo운송형태.ValueMember = "CODE";

            // 중량단위
            this.cbo중량단위.DataSource = lds_Combo.Tables[3];
            this.cbo중량단위.DisplayMember = "NAME";
            this.cbo중량단위.ValueMember = "CODE";

            // 운송방법
            this.cbo운송방법.DataSource = lds_Combo.Tables[4];
            this.cbo운송방법.DisplayMember = "NAME";
            this.cbo운송방법.ValueMember = "CODE";

            // 원산지
            this.cbo원산지.DataSource = lds_Combo.Tables[5];
            this.cbo원산지.DisplayMember = "NAME";
            this.cbo원산지.ValueMember = "CODE";

            // 포장형태
            this.cbo포장형태.DataSource = lds_Combo.Tables[6];
            this.cbo포장형태.DisplayMember = "NAME";
            this.cbo포장형태.ValueMember = "CODE";

		}

		#endregion

		#region ● 일반 메소드

		/// <summary>
		/// 받아온 Panel 안의 Control들의 상태를 설정해 준다.(비활성으로 설정한다.)
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

			txt송장번호.ReadOnly = false;
			//this.m_btnSoRefer.Enabled = true;
			//this.m_btnLcRefer.Enabled = true;
		}

		/// <summary>
		/// Panel 안이 컨트롤을 활성으로 한다.
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
		/// 콤보박스를 처음 상태로 돌린다.
		/// </summary>
		private void ComboResetting()
		{
            this.cbo거래구분.SelectedIndex = 0;
            this.cbo통화.SelectedIndex = 0;
            this.cbo운송형태.SelectedIndex = 0;
            this.cbo중량단위.SelectedIndex = 0;
            this.cbo운송방법.SelectedIndex = 0;
            this.cbo원산지.SelectedIndex = 0;
            this.cbo포장형태.SelectedIndex = 0;
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
				DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);

				if(ldt_temp != null)
				{		
					// 변경된 사항이 있습니다. 저장하시겠습니까?
					// MA_M000073
					string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
					DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if(result == DialogResult.Yes)
					{
						// 이전 데이터를 저장한 후 새로운 데이터를 추가할 수 있게 한다.
						if(this.Save())
						{
							// 정상 저장
						}
						else
						{
							// 저장시 에러 발생
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

                    // 코드, 명 변경을 위한 데이터 설정

                    // 거래처 코드 & 명칭 변수
                    this.m_CdPartner = this.m_HeadTable.Rows[0]["CD_PARTNER"].ToString();
                    this.m_NmPartner = this.m_HeadTable.Rows[0]["NM_PARTNER"].ToString();
                    this.bpc거래처.CodeName = this.m_NmPartner;
                    this.bpc거래처.CodeValue = this.m_CdPartner;


                    // 영업그룹 코드 & 명칭 변수
                    this.m_CdGroup = this.m_HeadTable.Rows[0]["CD_SALEGRP"].ToString();
                    this.m_NmGroup = this.m_HeadTable.Rows[0]["NM_SALEGRP"].ToString();
                    this.bbpc영업그룹.CodeName = this.m_NmGroup;
                    this.bbpc영업그룹.CodeValue = this.m_CdGroup;

                    // 담당자 코드 & 명칭 변수
                    this.m_CdEmp = this.m_HeadTable.Rows[0]["NO_EMP"].ToString();
                    this.m_NmEmp = this.m_HeadTable.Rows[0]["NM_KOR"].ToString();
                    this.bpc담당자.CodeName = this.m_NmEmp;
                    this.bpc담당자.CodeValue = this.m_CdEmp;

                    // 사업장 코드 변수
                    this.m_CdBizarea = this.m_HeadTable.Rows[0]["CD_BIZAREA"].ToString();
                    this.bpc사업장.CodeName = this.m_CdBizarea;

                    // 수출자 코드 & 명칭 변수
                    this.m_CdExport = this.m_HeadTable.Rows[0]["CD_EXPORT"].ToString();
                    this.m_NmExport = this.m_HeadTable.Rows[0]["NM_EXPORT"].ToString();
                    this.bpc수출자.CodeName = this.m_NmExport;
                    this.bpc수출자.CodeValue = this.m_CdExport;

                    // 제조자 코드 & 명칭 변수
                    this.m_CdPRODUCT = this.m_HeadTable.Rows[0]["CD_PRODUCT"].ToString();
                    this.m_NmPRODUCT = this.m_HeadTable.Rows[0]["NM_PRODUCT"].ToString();
                    this.bpc제조자.CodeName = this.m_NmPRODUCT;
                    this.bpc제조자.CodeValue = this.m_CdPRODUCT;

                    // 대행자 코드 & 명칭 변수
                    this.m_CdAgent = this.m_HeadTable.Rows[0]["CD_AGENT"].ToString();
                    this.m_NmAgent = this.m_HeadTable.Rows[0]["NM_AGENT"].ToString();
                    this.bpc대행자.CodeName = this.m_NmAgent;
                    this.bpc대행자.CodeValue = this.m_CdAgent;

                    // 선사 코드 & 명칭 변수
                    this.m_CdShipCorp = this.m_HeadTable.Rows[0]["SHIP_CORP"].ToString();
                    this.m_NmShipCorp = this.m_HeadTable.Rows[0]["NM_SHIP_CORP"].ToString();
                    this.bpc선사.CodeName = this.m_NmShipCorp;
                    this.bpc선사.CodeValue = this.m_CdShipCorp;

                    // 착하 통지처
                    this.m_CdCustIn = this.m_HeadTable.Rows[0]["CD_CUST_IN"].ToString();
                    this.m_NmCustIn = this.m_HeadTable.Rows[0]["NM_CUST_IN"].ToString();
                    this.bpc착하통지처.CodeName = this.m_NmCustIn;
                    this.bpc착하통지처.CodeValue = this.m_CdCustIn;

                    this.m_IsPageActivated = true;
                    this.SetControlEnable();

                    this.txt송장번호.Enabled = false;

                    ////참조 여부에 따라 막는다.(2003-08-14)
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
		/// 추가 버튼
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
					// 변경된 사항이 있습니다. 저장하시겠습니까?
					// MA_M000073
					string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
					DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

					if(result == DialogResult.Yes)
					{
						// 이전 데이터를 저장한 후 새로운 데이터를 추가할 수 있게 한다.
						if(this.Save())
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

				// Control 비 활성화
				SetControlDisable();

				this.Append();

				this.txt송장번호.Enabled = true;
				this.txt송장번호.Focus();

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
		/// 저장 버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if(!this.m_IsPageActivated)
					return;

				this.txt송장번호.Focus();

				this.m_Manager.Position = 0;

				string msg = null;

				DataTable ldt_temp = this.m_HeadTable.GetChanges();

				if(ldt_temp == null)
				{
					// MA_M000017 ("변경된 데이터가 없습니다)
					msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000017");
					Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);

					return;
				}

				this.Save();

				this.txt송장번호.Enabled = false;
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		/// <summary>
		/// 삭제 버튼
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

				// 삭제 할 수 없는 부분(추가된 데이터가 있다는 것은 새로운 데이터이므로 DB에는 없다는 말
				if(ldt_temp != null)
					return;
			
				// MA_M000016
				// 정말로 삭제 하겠습니까?
				msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000016");

				if(DialogResult.Yes == MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
				{
					Application.DoEvents();

					//object[] args = new object[1]{this.m_HeadTable};
					//this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_INVIN", "CC_TR_INVIN.rem", "SaveDelete", args);
                    SaveDelete(this.m_HeadTable);

					// TR_M000033
					// 정상적으로 삭제 되었습니다.
					msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000033");
					MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.Append();

					this.txt송장번호.Enabled = true;
					this.txt송장번호.Focus();
				}
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

///////////////////////
		/// <summary>
		/// 작성자 : 김봉회
		/// 호출모듈 : 무역(수출)
		/// 호출 UI : P_TR_EXINV(송장 등록)
		/// 내용 : TR_INVH에 데이터를 삭제한다.
		/// </summary>
		/// <param name="pdt_table"></param>
		/// <returns></returns>
		public void SaveDelete(DataTable pdt_table)
		{
			try
			{

				if(this.DeleteINVH(pdt_table.Rows[0]["CD_COMPANY"].ToString(), pdt_table.Rows[0]["NO_INV"].ToString()) < 0)
				{
					// 삭제될 데이터가 존재하지 않습니다.
					// TR_M000058
					System.ApplicationException app = new System.ApplicationException("TR_M000058");
					app.Source = "100000";
					app.HelpLink = "삭제될 데이터가 존재하지 않습니다.";
					throw app;

				}

				if(this.DeleteINVL(pdt_table.Rows[0]["CD_COMPANY"].ToString(), pdt_table.Rows[0]["NO_INV"].ToString()) < 0)
				{
					// 삭제될 데이터가 존재하지 않습니다.
					// TR_M000058
					System.ApplicationException app = new System.ApplicationException("TR_M000058");
					app.Source = "100000";
					app.HelpLink = "삭제될 데이터가 존재하지 않습니다.";
					throw app;

				}

				if(this.DeletePacking(pdt_table.Rows[0]["CD_COMPANY"].ToString(), pdt_table.Rows[0]["NO_INV"].ToString()) < 0)
				{
					// 삭제될 데이터가 존재하지 않습니다.
					// TR_M000058
					System.ApplicationException app = new System.ApplicationException("TR_M000058");
					app.Source = "100000";
					app.HelpLink = "삭제될 데이터가 존재하지 않습니다.";
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
		/// 작성자 : 김봉회
		/// 호출모듈 : 무역(수출)
		/// 호출 CC : CC_TR_INVIN(송장 등록)
		/// 내용 : TR_INVH에 데이터를 삭제한다.
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
		/// 작성자 : 김봉회
		/// 호출모듈 : 무역(수출)
		/// 호출 CC : CC_TR_INVIN(송장 등록)
		/// 내용 : TR_INVL에 데이터를 삭제한다.
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
		/// 작성자 : 김봉회
		/// 호출모듈 : 무역(수출)
		/// 호출 CC : CC_TR_INVIN(송장 등록)
		/// 내용 : TR_PACKING에 데이터를 삭제한다.
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
		/// 출력 버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 종료 버튼
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

				// 데이터가 처음 로딩된 경우라든지 새로 추가된 경우 LC번호의 유무를 판단하여 저장할 것인지 물어본다.
				DataTable ldt_table = this.m_HeadTable.GetChanges(DataRowState.Added);
			
				if(this.txt송장번호.Text != "" && ldt_table != null)
				{
					lb_isSave = true;
				}


				if(lb_isSave)
				{
					// 변경된 사항이 있습니다. 저장하시겠습니까?
					// MA_M000073
					msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000073");
					DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if(result == DialogResult.Yes)
					{
						// 이전 데이터를 저장한 후 새로운 데이터를 추가할 수 있게 한다.
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

		#region ● 초기 테이블 초기화 부분

		/// <summary>
		/// DB에서 메인에서 사용할 빈 데이터를 가져온다.
		/// InvokeRemoteMethod 대신 빈 Table을 생성시킴 (2003-08-13)
		/// </summary>
		private void SelectInit()
		{
			//DataSet lds_Result = (DataSet)this.MainFrameInterface.InvokeRemoteMethod("TradeExport_NTX", "trade.CC_TR_INVIN_NTX", "CC_TR_INVIN_NTX.rem", "SelectInvH", null);
			//this.m_HeadTable = lds_Result.Tables[0];

			//Head Table생성 및 디폴트 값 부여
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

			// DefaultValue 설정
			this.m_HeadTable.Columns["NO_INV"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_COMPANY"].DefaultValue = this.MainFrameInterface.LoginInfo.CompanyCode;
			this.m_HeadTable.Columns["DT_BALLOT"].DefaultValue = this.m_Today;
            this.m_HeadTable.Columns["CD_BIZAREA"].DefaultValue = this.bpc사업장.CodeValue.ToString();
			this.m_HeadTable.Columns["CD_SALEGRP"].DefaultValue = "";
			this.m_HeadTable.Columns["NO_EMP"].DefaultValue = "";
			this.m_HeadTable.Columns["FG_LC"].DefaultValue = this.cbo거래구분.SelectedValue;
			this.m_HeadTable.Columns["CD_PARTNER"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_EXCH"].DefaultValue = this.cbo통화.SelectedValue;
			this.m_HeadTable.Columns["AM_EX"].DefaultValue = 0;

			this.m_HeadTable.Columns["DT_LOADING"].DefaultValue = this.m_Today;
			this.m_HeadTable.Columns["CD_ORIGIN"].DefaultValue = this.cbo원산지.SelectedValue;
			this.m_HeadTable.Columns["CD_AGENT"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_EXPORT"].DefaultValue = "";
			this.m_HeadTable.Columns["CD_PRODUCT"].DefaultValue = "";
			this.m_HeadTable.Columns["SHIP_CORP"].DefaultValue = "";
			this.m_HeadTable.Columns["NM_VESSEL"].DefaultValue = "";
			this.m_HeadTable.Columns["COND_TRANS"].DefaultValue = "";
			this.m_HeadTable.Columns["TP_TRANSPORT"].DefaultValue = this.cbo운송방법.SelectedValue;
			this.m_HeadTable.Columns["TP_TRANS"].DefaultValue = this.cbo운송형태.SelectedValue;

			this.m_HeadTable.Columns["TP_PACKING"].DefaultValue = this.cbo포장형태.SelectedValue;
			this.m_HeadTable.Columns["CD_WEIGHT"].DefaultValue = this.cbo중량단위.SelectedValue;
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
		/// 초기 바인딩 메니저 설정
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
		
			
			this.txt송장번호.DataBindings.Add("Text", this.m_HeadTable, "NO_INV");
			this.cbo거래구분.DataBindings.Add("SelectedValue", this.m_HeadTable, "FG_LC");
			this.cbo통화.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_EXCH");	// Mask
            this.dtp선적에정일.DataBindings.Add("Text", this.m_HeadTable, "DT_LOADING");	// Mask
			this.cbo운송형태.DataBindings.Add("SelectedValue", this.m_HeadTable, "TP_TRANS");
			this.cbo중량단위.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_WEIGHT"); //2003-08-14추가
			
			this.txt시작CT번호.DataBindings.Add("Text", this.m_HeadTable, "NO_SCT");
			this.txt선적지.DataBindings.Add("Text", this.m_HeadTable, "PORT_LOADING");
			this.txt도착지.DataBindings.Add("Text", this.m_HeadTable, "PORT_ARRIVER");
			this.txt최종목적지.DataBindings.Add("Text", this.m_HeadTable, "DESTINATION");
			this.txtVESSEL명.DataBindings.Add("Text", this.m_HeadTable, "NM_VESSEL");
			
			this.txt인도조건.DataBindings.Add("Text", this.m_HeadTable, "COND_TRANS");
			this.txt비고1.DataBindings.Add("Text", this.m_HeadTable, "REMARK1");
			this.txt비고2.DataBindings.Add("Text", this.m_HeadTable, "REMARK2");
			this.txt비고3.DataBindings.Add("Text", this.m_HeadTable, "REMARK3");
			this.txt비고4.DataBindings.Add("Text", this.m_HeadTable, "REMARK4");
			this.txt비고5.DataBindings.Add("Text", this.m_HeadTable, "REMARK5");

            this.dtp발행일자.DataBindings.Add("Text", this.m_HeadTable, "DT_BALLOT");
			this.cur외화금액.DataBindings.Add("Text", this.m_HeadTable, "AM_EX");	// Mask
			this.cbo운송방법.DataBindings.Add("SelectedValue", this.m_HeadTable, "TP_TRANSPORT");
			this.cur총중량.DataBindings.Add("Text", this.m_HeadTable, "GROSS_WEIGHT");
			this.txt종료CT번호.DataBindings.Add("Text", this.m_HeadTable, "NO_ECT");	
			
			//this.m_txtLcRefer.DataBindings.Add("Text", this.m_HeadTable, "NO_LC");	// Mask
			//this.m_txtSoRefer.DataBindings.Add("Text", this.m_HeadTable, "NO_SO");
			this.cbo원산지.DataBindings.Add("SelectedValue", this.m_HeadTable, "CD_ORIGIN");
            this.dtp통관예정일.DataBindings.Add("Text", this.m_HeadTable, "DT_TO");
			this.cbo포장형태.DataBindings.Add("SelectedValue", this.m_HeadTable, "TP_PACKING");
			this.cur순중량.DataBindings.Add("Text", this.m_HeadTable, "NET_WEIGHT");// 적용환율
			//this.m_txtArtran.DataBindings.Add("Text", this.m_HeadTable, "CD_CUST_IN");
			
			this.m_Manager.Position = 0;
		}

		#endregion

		#region ● S/O 참조 적용 부분 

//		/// <summary>
//		/// S/O 참보 도움창을 연다.
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
//				// 작업을 정상적으로 처리하지 못했습니다.(도움창)
//				this.MainFrameInterface.ShowMessageBox(109, ex.Message);
//			}
//		}
//
//
//		/// <summary>
//		/// P_TR_SASO_SUB에서 가져온 데이터의 수주번호로 해당데이터를 DB에서 가져온다.
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
//				// null이 아니면 TR_EXSOH에서 데이터를 가져오고, NULL이면 현재ROW에서 데이터를 넣어 준다.
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
//				// 작업을 정상적으로 처리하지 못했습니다.(조회)
//				this.MainFrameInterface.ShowMessageBox(109, "[SelectSOH] " + ex.Message);
//			}
//		}
//
//		/// <summary>
//		/// SA_SOH에서 가져온 데이터를 넣는다.
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
//			// 코드, 명 변경을 위한 데이터 설정
//					
//			// 거래처 코드 & 명칭 변수
//			this.m_CdPartner = pdr_row["CD_PARTNER"].ToString();
//			this.m_NmPartner = pdr_row["NM_PARTNER"].ToString();
//			this.m_txtCdTrans.Text = this.m_NmPartner;
//
//			// 영업그룹 코드 & 명칭 변수
//			this.m_CdGroup = pdr_row["CD_SALEGRP"].ToString();
//			this.m_NmGroup = pdr_row["NM_SALEGRP"].ToString();
//			this.m_txtGroupIsul.Text = this.m_NmGroup;
//
//			// 담당자 코드 & 명칭 변수
//			this.m_CdEmp = pdr_row["NO_EMP"].ToString();
//			this.m_NmEmp = pdr_row["NM_KOR"].ToString();
//			this.m_txtNmEmp.Text = this.m_NmEmp;
//
//			// 사업장 코드 & 명칭 변수
//			this.m_CdBizarea = pdr_row["CD_BIZAREA"].ToString();
//			this.m_NmBizarea = pdr_row["NM_BIZAREA"].ToString();
//			this.m_txtBallot.Text = this.m_NmBizarea;
//
//
//
//		}
//
//		/// <summary>
//		/// TR_EXSOH에서 가져온 데이터를 넣는다.
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
//			this.m_HeadTable.Rows[0]["INSPECTION"] = pdt_table.Rows[0]["INSPECTION"];	// 없는 부분
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
//			// 거래처 부분
//			this.m_HeadTable.Rows[0]["NM_PARTNER"] = pdt_table.Rows[0]["NM_PARTNER"];
//			// 통지은행
//			this.m_HeadTable.Rows[0]["NM_BANK_NOTICE"] = pdt_table.Rows[0]["NM_BANK"];
//			// 담당자
//			this.m_HeadTable.Rows[0]["NM_KOR"] = pdt_table.Rows[0]["NM_KOR"];
//			this.m_HeadTable.Rows[0]["NM_SALEGRP"] = pdt_table.Rows[0]["NM_SALEGRP"];
//			this.m_HeadTable.Rows[0]["NM_BIZAREA"] = pdt_table.Rows[0]["NM_BIZAREA"];
//
//
//			// 개설은행
//
//			// 코드, 명 변경을 위한 데이터 설정
//					
//			// 거래처 코드 & 명칭 변수
//			this.m_CdPartner = pdt_table.Rows[0]["CD_PARTNER"].ToString();
//			this.m_NmPartner = pdt_table.Rows[0]["NM_PARTNER"].ToString();		
//			this.m_txtCdTrans.Text = this.m_NmPartner;
//
//			// 영업그룹 코드 & 명칭 변수
//			this.m_CdGroup = pdt_table.Rows[0]["CD_SALEGRP"].ToString();
//			this.m_NmGroup = pdt_table.Rows[0]["NM_SALEGRP"].ToString();
//			this.m_txtGroupIsul.Text = this.m_NmGroup;
//
////			// 개설 은행 코드 & 명칭 변수
////			this.m_CdOpenBank = pdt_table.Rows[0["CD_BANK_LC"].ToString();
////			this.m_NmOpenBank = pdt_table.Rows[0["NM_BANK_LC"].ToString();
////			this.m_txtOpenBank.Text = this.m_NmOpenBank;
//
//			// 통지 은행 코드 & 명칭 변수
//			this.m_CdNoticeBank = pdt_table.Rows[0]["CD_BANK"].ToString();
//			this.m_NmNoticeBank = pdt_table.Rows[0]["NM_BANK"].ToString();
//			this.m_txtNoticeBank.Text = this.m_NmNoticeBank;
//
//			// 담당자 코드 & 명칭 변수
//			this.m_CdEmp = pdt_table.Rows[0]["NO_EMP"].ToString();
//			this.m_NmEmp = pdt_table.Rows[0]["NM_KOR"].ToString();
//			this.m_txtNmEmp.Text = this.m_NmEmp;
//
//			// 사업장 코드 & 명칭 변수
//			this.m_CdBizarea = pdt_table.Rows[0]["CD_BIZAREA"].ToString();
//			this.m_NmBizarea = pdt_table.Rows[0]["NM_BIZAREA"].ToString();
//			this.m_txtBallot.Text = this.m_NmBizarea;
//
//
//			
//
//		}


		#endregion

		#region ● 추가 부분

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

            //this.ComboResetting(); --- 나중에

			// 코드, 명 변경을 위한 데이터 설정
			// 거래처 코드 & 명칭 변수
			this.m_CdPartner = "";
			this.m_NmPartner = "";
            this.bpc거래처.CodeName = m_NmPartner;
            this.bpc거래처.CodeValue = m_CdPartner;

			// 영업그룹 코드 & 명칭 변수
			this.m_CdGroup = "";
			this.m_NmGroup = "";
            this.bbpc영업그룹.CodeName = m_NmGroup;
            this.bbpc영업그룹.CodeName = m_CdGroup;

            // 담당자 코드 & 명칭 변수
			this.m_CdEmp = "";
			this.m_NmEmp = "";

			// 사업장 코드 & 명칭 변수
            m_CdBizarea = bpc사업장.CodeValue.ToString();

			// 수출자 코드 & 명칭 변수
			this.m_CdExport = "";
			this.m_NmExport = "";
            this.bpc수출자.CodeName = "";
            this.bpc수출자.CodeValue = "";

			// 제조자 코드 & 명칭 변수
			this.m_CdPRODUCT = "";
			this.m_NmPRODUCT = "";
            this.bpc제조자.CodeName = m_NmPRODUCT;
            this.bpc제조자.CodeValue = m_CdPRODUCT;

			// 대행자 코드 & 명칭 변수
			this.m_CdAgent = "";
			this.m_NmAgent = "";
            this.bpc대행자.CodeName = "";
            this.bpc대행자.CodeValue = "";

			// 선사 코드 & 명칭 변수
			this.m_CdShipCorp = "";
			this.m_NmShipCorp = "";
            this.bpc선사.CodeName = "";
            this.bpc선사.CodeValue = "";

            // 착하통지처
			m_CdCustIn = "";
			m_NmCustIn = "";
            this.bpc착하통지처.CodeName = "";
            this.bpc착하통지처.CodeValue = "";

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
				this.Cursor = Cursors.WaitCursor;
				Cursor.Current = Cursors.WaitCursor;

				string msg = null;

				this.txt송장번호.Focus();

				// Validating 검사하여 에러가 발생하면 리턴하기 위함
				if(this.m_IsSave == false)
				{
					this.m_IsSave = true;
					return false;
				}


				// 필수 입력 값 체크
				if(!this.CheckRequiredValue())
					return false;

				object[] args = new object[1]{this.m_HeadTable};

				//DataTable ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Added);
				//if(ldt_temp != null)
                if (this.m_HeadTable != null)
				{
					
					// 입력가능한 날짜로 변경한다.
					this.ReWriteDate();

					//this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_INVIN", "CC_TR_INVIN.rem", "SaveAppendH", args);
                    SaveAppendH(this.m_HeadTable);
				}

//여기 내가 막음. 위에꺼로 통합시킴... 김용수 2006/07/05                
                //ldt_temp = this.m_HeadTable.GetChanges(DataRowState.Modified);
                //if(ldt_temp != null)
                //{
				
                //    // 입력가능한 날짜로 변경한다.
                //    this.ReWriteDate();

                //    //this.MainFrameInterface.InvokeRemoteMethod("TradeExport", "trade.CC_TR_INVIN", "CC_TR_INVIN.rem", "SaveUpdateH", args);
                //}
		
				// CM_M000001
				// 자료를 저장하였습니다.
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
        /// 작성자 : 김봉회
        /// 호출모듈 : 무역(수출)
        /// 호출 UI : P_TR_EXINV(송장 등록)
        /// 내용 : TR_INVH에 데이터를 추가한다.
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
                //    // 저장되지 않았습니다. 다시 시도해 보시기 바랍니다.
                //    // TR_M000058
                //    System.ApplicationException app = new System.ApplicationException("TR_M000058");
                //    app.Source = "100000";
                //    app.HelpLink = "저장되지 않았습니다. 다시 시도해 보시기 바랍니다.";
                //    throw app;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



		#endregion

		#region ● 저장전 작업


		/// <summary>
		/// 날짜의 /를 제거해 준다.
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
		/// 필수 입력사항 체크
		/// </summary>
		/// <returns></returns>
		private bool CheckRequiredValue()
		{
			bool isok = true;

			if(!this.DetailCheckRequiredValue(this.txt송장번호))
				isok = false;
            else if (dtp발행일자.Text == string.Empty)
                isok = false;
            else if (bpc거래처.IsEmpty())
				isok = false;
			else if(bbpc영업그룹.IsEmpty())
				isok = false;
            else if (bpc사업장.IsEmpty())
				isok = false;

			if(isok == false)
			{
				// 필수 입력사항이 빠졌습니다.
				this.MainFrameInterface.ShowMessageBox(102, "");
				return false;
			}

            ////신용장번호든 S/O번호든 둘중 하나는 참조되어야한다.(2003-08-14)
            //if((m_txtSoRefer.Text == "") && (m_txtLcRefer.Text == ""))
            //{
            //    //참조번호가 설정되지 않았습니다.(신용장번호 or S/O번호)
            //    MessageBoxEx.Show(GetMessageDictionaryItem("TR_M000080"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    m_txtLcRefer.Focus();
            //    return false;
            //}

			return isok;	
		}


		/// <summary>
		/// 각 컨트롤의 필수 입력값을 체크한다.
		/// </summary>
		/// <param name="ps_control"></param>
		/// <returns></returns>
		private bool DetailCheckRequiredValue(object ps_control)
		{
			if(ps_control is Duzon.Common.Controls.MaskedEditBox)
			{
				Duzon.Common.Controls.MaskedEditBox ctrl = (Duzon.Common.Controls.MaskedEditBox)ps_control;
				// 날짜 유효성 체크
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

		#region ● 참조 신용장 번호 부분
			
		/// <summary>
		/// L/C 번호 도움창에서 데이터를 가져온다.
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
			this.m_HeadTable.Rows[0]["TP_TRANS"] = pdr_row["TP_TRANS"];	// 없는 부분
	
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

			// 코드, 명 변경을 위한 데이터 설정
					
			// 거래처 코드 & 명칭 변수
			this.m_CdPartner = pdr_row["CD_PARTNER"].ToString();
			this.m_NmPartner = pdr_row["NM_PARTNER"].ToString();
            this.bpc거래처.CodeName = this.m_NmPartner;
            this.bpc거래처.CodeValue = this.m_CdPartner;

			// 영업그룹 코드 & 명칭 변수
			this.m_CdGroup = pdr_row["CD_SALEGRP"].ToString();
			this.m_NmGroup = pdr_row["NM_SALEGRP"].ToString();
	//		this.m_txtGroupIsul.Text = this.m_NmGroup;

			// 담당자 코드 & 명칭 변수
			this.m_CdEmp = pdr_row["NO_EMP"].ToString();
			this.m_NmEmp = pdr_row["NM_EMP"].ToString();
            this.bpc사업장.CodeName = this.m_NmEmp;
            this.bpc사업장.CodeValue = this.m_CdEmp;

			// 사업장 코드 & 명칭 변수
			this.m_CdBizarea = pdr_row["CD_BIZAREA"].ToString();
            this.bpc사업장.CodeValue = this.m_CdBizarea;
			m_HeadTable.Rows[0].EndEdit();
		}



		#endregion

		#region ● 참조 S/O 번호 부분

		/// <summary>
		/// S/O 도움창에서 데어터를 얻어온다.
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
			this.m_HeadTable.Rows[0]["TP_TRANS"] = pdt_table.Rows[0]["TP_TRANS"];	// 없는 부분
	
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

			// 코드, 명 변경을 위한 데이터 설정
					
			// 거래처 코드 & 명칭 변수
			this.m_CdPartner = pdt_table.Rows[0]["CD_PARTNER"].ToString();
			this.m_NmPartner = pdt_table.Rows[0]["LN_PARTNER"].ToString();
            this.bpc거래처.CodeName = this.m_NmPartner;
            this.bpc거래처.CodeValue = this.m_CdPartner;

			// 영업그룹 코드 & 명칭 변수
			this.m_CdGroup = pdt_table.Rows[0]["CD_SALEGRP"].ToString();
			this.m_NmGroup = pdt_table.Rows[0]["NM_SALEGRP"].ToString();
            this.bbpc영업그룹.CodeName = this.m_NmGroup;
            this.bbpc영업그룹.CodeValue = this.m_CdGroup;

            // 담당자 코드 & 명칭 변수
			this.m_CdEmp = pdt_table.Rows[0]["NO_EMP"].ToString();
			this.m_NmEmp = pdt_table.Rows[0]["NM_KOR"].ToString();
			this.bpc담당자.CodeName = this.m_NmEmp;
            this.bpc담당자.CodeValue = this.m_CdEmp;

			// 사업장 코드 & 명칭 변수
			this.m_CdBizarea = pdt_table.Rows[0]["CD_BIZAREA"].ToString();
            this.bpc사업장.CodeValue = this.m_CdBizarea;
			m_HeadTable.Rows[0].EndEdit();
		}

		#endregion

		#region ● 도움창 부분

		/// <summary>
		/// 도움창을 연다.
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

					// 참조 신용장 번호
					if(box.Name == "m_btnLcRefer")
					{

                        trade.P_TR_EXLCNO_SUB obj = new trade.P_TR_EXLCNO_SUB(this.MainFrameInterface);

                        if (obj.ShowDialog() == DialogResult.OK)
                        {
                            this.ApplyLCRefer(obj.GetResultRow);
                            this.m_IsPageActivated = true;
                            this.SetControlEnable();
                            ////신용장 번호를 참조했기 때문에 S/O번호를 막는다.(2003-08-13)
                            //m_txtSoRefer.Enabled = false;
                            //m_txtSoRefer.BackColor = System.Drawing.SystemColors.Control;
                            //m_btnSoRefer.Enabled = false;
                            //m_bpCD_EXPORT.Focus();
                        }
                        obj.Dispose();

					}
					else if(box.Name == "m_btnSoRefer")	// 참조 S/O 번호
					{
                        // 요거는 알맞게 수정해야 함

                        ////trade.P_TR_EXSONO_SUB obj = new trade.P_TR_EXSONO_SUB(this.MainFrameInterface, "P_TR_EXINV");
                        //trade.P_TR_EXSONO_SUB obj = new trade.P_TR_EXSONO_SUB(this.MainFrameInterface, "P_TR_EXINV");

                        //if (obj.ShowDialog() == DialogResult.OK)
                        //{
                        //    this.ApplySORefer(obj.GetSOHeadTable);
                        //    this.m_IsPageActivated = true;
                        //    this.SetControlEnable();
                        //    //S/O번호을 참조했기 때문에 신용장 번호를 막는다.(2003-08-13)
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

		#region ● TaxtBox KeyDown 이벤트

		/// <summary>
		/// TextBox KeyDown 이벤트 처리
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
                            ////참조신용장번호
                            //case "m_txtLcRefer":
                            //    OpenSubPage(m_btnLcRefer, e);
                            //    break;

                            ////참조S/O번호
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

        #region ♣ 도움창 이벤트 / 메소드

 
      /// <summary>
      /// 만기일 유효성 체크
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void OnDateValidating(object sender, System.ComponentModel.CancelEventArgs e)
      {
          Duzon.Common.Controls.MaskedEditBox maskBox = (Duzon.Common.Controls.MaskedEditBox)sender;

          if (maskBox.ClipText.Trim().Length == 0)
              return;

          // 날짜 유효성 체크
          try
          {
              Convert.ToDateTime(maskBox.Text);
          }
          catch
          {
              // 저장할 때 Validating 체크할 때 에러 날 경우 저장하지 않기 위함.
              this.m_IsSave = false;

              // MA_M000084
              // 날짜 형식이 잘못되었습니다.
              string msg = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000084");
              DialogResult result = Duzon.Common.Controls.MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);

              e.Cancel = true;
              maskBox.Text = "";
              return;
          }
      }



        #endregion

		#region ● 내역등록 부분

		/// <summary>
		/// 내역등록 부분
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
					// 작업하신 자료를 먼저 저장하셔야 합니다. 계속하시겠습니까?"
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

				DataRow row = ((DataRowView)this.cbo거래구분.SelectedItem).Row;
				args[2] = row["NAME"].ToString();
				
				row = ((DataRowView)this.cbo통화.SelectedItem).Row;
				args[3] = row["NAME"].ToString();

				row = ((DataRowView)this.cbo중량단위.SelectedItem).Row;
				args[4] = row["NAME"].ToString();

	
			
				// Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
				if(this.MainFrameInterface.IsExistPage("P_TR_EXINVL", false))
				{
					//- 특정 페이지 닫기
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
		///  내역등록에서 외화금액과 원화금액을 다시 계산할 때 호출한다.
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

				if(!this.m_IsPageActivated)
					return;

				if(this.m_HeadTable == null)
					return;

				this.m_Manager.Position = 0;

				DataTable ldt_table = this.m_HeadTable.GetChanges();
				if(ldt_table != null)
				{
					// TR_M000036
					// 작업하신 자료를 먼저 저장하셔야 합니다. 계속하시겠습니까?"
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

				// 경비발생구분, 관리번호, 기표일자, 기표사업장코드,
				// 부서코드, 부서명, 담당자코드 ,담당자명, C/C 코드, C/C명
				string[] ls_args = new string[11];
				ls_args[0] = "송장";
				ls_args[1] = this.m_HeadTable.Rows[0]["NO_INV"].ToString();
                ls_args[2] = this.dtp발행일자.Text;	// 발행일자
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
				args[2] = 2;	// 송장 : 1 , L/C : 1
			
				// Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
				if(this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
				{
					//- 특정 페이지 닫기
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

		#region ● 키 이벤트 처리 부분


		//++++++++++++++++++++++++++++++++++++++++++++++++++++<< 콤보박스 키 이벤트 처리 >>
		
		/// <summary>
		/// 콤보 박스 공통 키 이벤트 처리
		/// </summary>
		/// <param name="e"></param>
		/// <param name="backControl"></param>
		/// <param name="nextControl"></param>
		private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
		}
		
		
		
		//+++++++++++++++++++++++++++++++++++++++++++++++++++++<< 일반 텍스트 박스 키 이벤트 >>

		/// <summary>
		/// 공용 키 이벤트 처리
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


        #region ● control 키 이벤트 후 내용 적용

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

        private void dtp선적에정일_Click(object sender, EventArgs e)
        {

        }

	}
}
