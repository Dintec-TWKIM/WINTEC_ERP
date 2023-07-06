namespace pur
{
    partial class P_PU_EVAL_REG
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_EVAL_REG));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dTP_To = new Duzon.Common.Controls.DatePicker();
            this.dTP_From = new Duzon.Common.Controls.DatePicker();
            this.lb_DY_EVAL = new Duzon.Common.Controls.LabelExt();
            this.label8 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.lb_NM_PLANT = new Duzon.Common.Controls.LabelExt();
            this.cbo_CD_PLANT = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.lb_DT_EVAL = new Duzon.Common.Controls.LabelExt();
            this.tb_DT_TODAY = new Duzon.Common.Controls.DatePicker();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.lb_NO_EMP = new Duzon.Common.Controls.LabelExt();
            this.tb_NO_EMP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.btn_INV_EVAL = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_send = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dTP_To)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTP_From)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_TODAY)).BeginInit();
            this.bpPanelControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            // 
            // _tlayMain
            // 
            this._tlayMain.AutoSize = true;
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this._flex, 0, 1);
            this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 2;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlayMain.Size = new System.Drawing.Size(827, 579);
            this._tlayMain.TabIndex = 8;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 73);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 503);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 4;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 64);
            this.oneGrid1.TabIndex = 5;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dTP_To);
            this.bpPanelControl2.Controls.Add(this.dTP_From);
            this.bpPanelControl2.Controls.Add(this.lb_DY_EVAL);
            this.bpPanelControl2.Controls.Add(this.label8);
            this.bpPanelControl2.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dTP_To
            // 
            this.dTP_To.CalendarBackColor = System.Drawing.Color.White;
            this.dTP_To.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dTP_To.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dTP_To.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dTP_To.Location = new System.Drawing.Point(184, 1);
            this.dTP_To.Mask = "####/##";
            this.dTP_To.MaskBackColor = System.Drawing.SystemColors.Window;
            this.dTP_To.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dTP_To.MaximumSize = new System.Drawing.Size(0, 21);
            this.dTP_To.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dTP_To.Modified = true;
            this.dTP_To.Name = "dTP_To";
            this.dTP_To.NullCheck = false;
            this.dTP_To.PaddingCharacter = '_';
            this.dTP_To.PassivePromptCharacter = '_';
            this.dTP_To.PromptCharacter = '_';
            this.dTP_To.SelectedDayColor = System.Drawing.Color.White;
            this.dTP_To.ShowToDay = true;
            this.dTP_To.ShowTodayCircle = true;
            this.dTP_To.ShowUpDown = true;
            this.dTP_To.Size = new System.Drawing.Size(80, 21);
            this.dTP_To.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dTP_To.TabIndex = 125;
            this.dTP_To.TitleBackColor = System.Drawing.Color.White;
            this.dTP_To.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dTP_To.ToDayColor = System.Drawing.Color.Red;
            this.dTP_To.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dTP_To.UseKeyF3 = false;
            this.dTP_To.Value = new System.DateTime(((long)(0)));
            // 
            // dTP_From
            // 
            this.dTP_From.CalendarBackColor = System.Drawing.Color.White;
            this.dTP_From.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dTP_From.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dTP_From.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dTP_From.Location = new System.Drawing.Point(81, 1);
            this.dTP_From.Mask = "####/##";
            this.dTP_From.MaskBackColor = System.Drawing.SystemColors.Window;
            this.dTP_From.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dTP_From.MaximumSize = new System.Drawing.Size(0, 21);
            this.dTP_From.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dTP_From.Modified = true;
            this.dTP_From.Name = "dTP_From";
            this.dTP_From.NullCheck = false;
            this.dTP_From.PaddingCharacter = '_';
            this.dTP_From.PassivePromptCharacter = '_';
            this.dTP_From.PromptCharacter = '_';
            this.dTP_From.SelectedDayColor = System.Drawing.Color.White;
            this.dTP_From.ShowToDay = true;
            this.dTP_From.ShowTodayCircle = true;
            this.dTP_From.ShowUpDown = true;
            this.dTP_From.Size = new System.Drawing.Size(80, 21);
            this.dTP_From.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dTP_From.TabIndex = 124;
            this.dTP_From.TitleBackColor = System.Drawing.Color.White;
            this.dTP_From.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dTP_From.ToDayColor = System.Drawing.Color.Red;
            this.dTP_From.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dTP_From.UseKeyF3 = false;
            this.dTP_From.Value = new System.DateTime(((long)(0)));
            // 
            // lb_DY_EVAL
            // 
            this.lb_DY_EVAL.Location = new System.Drawing.Point(0, 3);
            this.lb_DY_EVAL.Name = "lb_DY_EVAL";
            this.lb_DY_EVAL.Resizeble = true;
            this.lb_DY_EVAL.Size = new System.Drawing.Size(80, 16);
            this.lb_DY_EVAL.TabIndex = 2;
            this.lb_DY_EVAL.Tag = "재고평가기간";
            this.lb_DY_EVAL.Text = "재고평가기간";
            this.lb_DY_EVAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(167, 4);
            this.label8.Name = "label8";
            this.label8.Resizeble = true;
            this.label8.Size = new System.Drawing.Size(14, 15);
            this.label8.TabIndex = 123;
            this.label8.Text = "~";
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.lb_NM_PLANT);
            this.bpPanelControl1.Controls.Add(this.cbo_CD_PLANT);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // lb_NM_PLANT
            // 
            this.lb_NM_PLANT.Location = new System.Drawing.Point(0, 3);
            this.lb_NM_PLANT.Name = "lb_NM_PLANT";
            this.lb_NM_PLANT.Resizeble = true;
            this.lb_NM_PLANT.Size = new System.Drawing.Size(80, 16);
            this.lb_NM_PLANT.TabIndex = 2;
            this.lb_NM_PLANT.Tag = "공장명";
            this.lb_NM_PLANT.Text = "공장명";
            this.lb_NM_PLANT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo_CD_PLANT
            // 
            this.cbo_CD_PLANT.AutoDropDown = true;
            this.cbo_CD_PLANT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo_CD_PLANT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_PLANT.ItemHeight = 12;
            this.cbo_CD_PLANT.Location = new System.Drawing.Point(81, 1);
            this.cbo_CD_PLANT.Name = "cbo_CD_PLANT";
            this.cbo_CD_PLANT.ShowCheckBox = false;
            this.cbo_CD_PLANT.Size = new System.Drawing.Size(186, 20);
            this.cbo_CD_PLANT.TabIndex = 0;
            this.cbo_CD_PLANT.Tag = "FG_TRANS";
            this.cbo_CD_PLANT.UseKeyEnter = false;
            this.cbo_CD_PLANT.UseKeyF3 = false;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl4.TabIndex = 5;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.lb_DT_EVAL);
            this.bpPanelControl5.Controls.Add(this.tb_DT_TODAY);
            this.bpPanelControl5.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl5.TabIndex = 4;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // lb_DT_EVAL
            // 
            this.lb_DT_EVAL.Location = new System.Drawing.Point(0, 3);
            this.lb_DT_EVAL.Name = "lb_DT_EVAL";
            this.lb_DT_EVAL.Resizeble = true;
            this.lb_DT_EVAL.Size = new System.Drawing.Size(80, 16);
            this.lb_DT_EVAL.TabIndex = 2;
            this.lb_DT_EVAL.Tag = "평가일";
            this.lb_DT_EVAL.Text = "평가일";
            this.lb_DT_EVAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_DT_TODAY
            // 
            this.tb_DT_TODAY.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_TODAY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_DT_TODAY.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tb_DT_TODAY.Enabled = false;
            this.tb_DT_TODAY.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.tb_DT_TODAY.Location = new System.Drawing.Point(81, 1);
            this.tb_DT_TODAY.Mask = "####/##/##";
            this.tb_DT_TODAY.MaskBackColor = System.Drawing.SystemColors.Window;
            this.tb_DT_TODAY.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_TODAY.MaximumSize = new System.Drawing.Size(0, 21);
            this.tb_DT_TODAY.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_TODAY.Modified = false;
            this.tb_DT_TODAY.Name = "tb_DT_TODAY";
            this.tb_DT_TODAY.NullCheck = false;
            this.tb_DT_TODAY.PaddingCharacter = '_';
            this.tb_DT_TODAY.PassivePromptCharacter = '_';
            this.tb_DT_TODAY.PromptCharacter = '_';
            this.tb_DT_TODAY.SelectedDayColor = System.Drawing.Color.White;
            this.tb_DT_TODAY.ShowToDay = false;
            this.tb_DT_TODAY.ShowTodayCircle = false;
            this.tb_DT_TODAY.ShowUpDown = false;
            this.tb_DT_TODAY.Size = new System.Drawing.Size(84, 21);
            this.tb_DT_TODAY.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.tb_DT_TODAY.TabIndex = 4;
            this.tb_DT_TODAY.TitleBackColor = System.Drawing.Color.White;
            this.tb_DT_TODAY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tb_DT_TODAY.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_TODAY.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_TODAY.UseKeyF3 = false;
            this.tb_DT_TODAY.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.lb_NO_EMP);
            this.bpPanelControl6.Controls.Add(this.tb_NO_EMP);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl6.TabIndex = 3;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // lb_NO_EMP
            // 
            this.lb_NO_EMP.Location = new System.Drawing.Point(0, 3);
            this.lb_NO_EMP.Name = "lb_NO_EMP";
            this.lb_NO_EMP.Resizeble = true;
            this.lb_NO_EMP.Size = new System.Drawing.Size(80, 16);
            this.lb_NO_EMP.TabIndex = 3;
            this.lb_NO_EMP.Tag = "담당자";
            this.lb_NO_EMP.Text = "담당자";
            this.lb_NO_EMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.tb_NO_EMP.IsCodeValueToUpper = true;
            this.tb_NO_EMP.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.tb_NO_EMP.Location = new System.Drawing.Point(81, 1);
            this.tb_NO_EMP.MaximumSize = new System.Drawing.Size(0, 21);
            this.tb_NO_EMP.Name = "tb_NO_EMP";
            this.tb_NO_EMP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NO_EMP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NO_EMP.SearchCode = true;
            this.tb_NO_EMP.SelectCount = 0;
            this.tb_NO_EMP.SetDefaultValue = false;
            this.tb_NO_EMP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NO_EMP.Size = new System.Drawing.Size(186, 21);
            this.tb_NO_EMP.TabIndex = 3;
            this.tb_NO_EMP.TabStop = false;
            // 
            // btn_INV_EVAL
            // 
            this.btn_INV_EVAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_INV_EVAL.BackColor = System.Drawing.Color.White;
            this.btn_INV_EVAL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_INV_EVAL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_INV_EVAL.Location = new System.Drawing.Point(737, 14);
            this.btn_INV_EVAL.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_INV_EVAL.Name = "btn_INV_EVAL";
            this.btn_INV_EVAL.Size = new System.Drawing.Size(90, 19);
            this.btn_INV_EVAL.TabIndex = 3;
            this.btn_INV_EVAL.TabStop = false;
            this.btn_INV_EVAL.Tag = "재고평가";
            this.btn_INV_EVAL.Text = "재고평가";
            this.btn_INV_EVAL.UseVisualStyleBackColor = false;
            this.btn_INV_EVAL.Click += new System.EventHandler(this.btn_INV_EVAL_Click);
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.BackColor = System.Drawing.Color.White;
            this.btn_send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.Location = new System.Drawing.Point(643, 14);
            this.btn_send.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(90, 19);
            this.btn_send.TabIndex = 4;
            this.btn_send.TabStop = false;
            this.btn_send.Tag = "";
            this.btn_send.Text = "전송처리";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Visible = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // P_PU_EVAL_REG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_INV_EVAL);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_PU_EVAL_REG";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn_INV_EVAL, 0);
            this.Controls.SetChildIndex(this.btn_send, 0);
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this._tlayMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dTP_To)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTP_From)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_TODAY)).EndInit();
            this.bpPanelControl6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.BpControls.BpCodeTextBox tb_NO_EMP;
        private Duzon.Common.Controls.LabelExt lb_DY_EVAL;
        private Duzon.Common.Controls.LabelExt lb_DT_EVAL;
        private Duzon.Common.Controls.LabelExt lb_NM_PLANT;
        private Duzon.Common.Controls.LabelExt lb_NO_EMP;
        private Duzon.Common.Controls.LabelExt label8;
        private Duzon.Common.Controls.DropDownComboBox cbo_CD_PLANT;
        private Duzon.Common.Controls.DatePicker tb_DT_TODAY;
        private Duzon.Common.Controls.RoundedButton btn_INV_EVAL;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.DatePicker dTP_From;
        private Duzon.Common.Controls.DatePicker dTP_To;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.RoundedButton btn_send;
    }
}
